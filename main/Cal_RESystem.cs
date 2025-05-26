using main.subcontents.CoolingSystem;
using main.subcontents.HeatingSystem;

namespace main
{
    internal class Cal_RESystem
    {
        public string Num,  프로젝트유형, 프로젝트번호;

        public double PVPpk_kW; //태양광 최대출력 
        public double[] Qf_elec = new double[12]; //월별 전기소요량 
        public double[] Qfpvm_kWh = new double[12]; //월별 전기생산량 
        public double[] Qfpvm_m2_kWh = new double[12]; //월별 단위면적당 전기생산량 
        public double Qfpva_kWh ; //연간 전기생산량 
        public double[] Qf_nutz_grid = new double[12]; //그리드망공급
        public double[] Qf_nutz_build = new double[12]; //건물내 이용
        public string PVType;
        public List<string> PVdata = new List<string>();

        private string PVBatteryNumber;
        private double Cnenm; //배터리 정격 용량 
        private double ηDoD, ηBatt; //배터리 최대 방전 깊이, 배터리 시스템효율
        private double[] γQ = new double[12];//배터리 규격에 대한 지수(소요량 대비 최대성능 계수)
        private double Ceff; //배터리 용량(배터리 타입에 따른 방전 깊이 고려) 
        private double[] CQ = new double[12];//배터리 규격에 대한 지수 (소요량 대비 배터리 용량 계수)
        public double[] fmatch = new double[12];//매칭계수
        private double[] fBatt = new double[12]; //배터리 손실 
        public double[] Qbatt_loss = new double[12];// 배터리 손실량

        //전체PV계산
        double[] Qfkwh_totalBattery = new double[12], Qfkwh_totalGrid = new double[12]; //계통연계형합계, 독립형합계

        //기후데이터
        public double[] Esol = new double[12];//일사량
        public double[] PVαsol = new double[12];//고도각


        public Cal_RESystem(string Num) { this.Num = Num; }

        #region PV별 계산
        public void PVcalReady( )
        {
            string[][] buildinginfo = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호,프로젝트유형번호,지역", "");
            프로젝트번호 = buildinginfo[0][0].ToString();
            프로젝트유형 = buildinginfo[0][1].ToString();
            string ort = buildinginfo[0][2].ToString();
            string[][] PVformdata = Program.DB.getValue(DB.type.ProjDB, "PV_Form", "방위,기울기,fperf,인버터효율,지형물거리,지형물높이,어레이높이,계통유형,면적,용량,배터리번호", "번호='" + Num +"'");
            string orientation, slope;
            orientation = PVformdata[0][0].ToString();
            slope = PVformdata[0][1].ToString() + "˚";
            double[] dmth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            if (slope == "0˚" || orientation == "수평")
            {
                orientation = "수평";
                slope = "0˚";
            }
            for (int mth = 0; mth < 12; mth++)
            {
                 string[][] token = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_전일사량", "일사량", "지역명 ='" + ort + "' AND 방향 ='" + orientation + "' AND  각도 = '" + slope + "' and 기간 ='" + (mth + 1).ToString() + "월'");
                //태양고도각 불러오기
                 string[][] token3 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_고도각", "고도각", "지역명 ='" + ort + "' AND 방향 ='" + orientation + "' AND  각도 = '" + slope + "' and 기간 ='" + (mth + 1).ToString() + "월'");
                 Esol[mth] = Convert.ToDouble(token[0][0]) * 0.024 * dmth[mth];
                 PVαsol[mth] = Convert.ToDouble(token3[0][0]);
            }
           
            for(int k = 0; k < 10; k++)
            {
               //this.PVdata.Add(PVdata[0][k + 1]);
                this.PVdata.Add(PVformdata[0][k + 1]);
            }
        }

        public void PVcal()
        {
            double Slope, fPerf, InverterEff, shLength, shHeight, Arrayheight, tan, totalArea, Kpk;
           
            Slope = Convert.ToDouble(PVdata[0]);
            fPerf = Convert.ToDouble(PVdata[1]);
            InverterEff = Convert.ToDouble(PVdata[2])/100;
            shLength = Convert.ToDouble(PVdata[3]);
            shHeight = Convert.ToDouble(PVdata[4]);
            Arrayheight = Convert.ToDouble(PVdata[5]);

            double[] hshobst = new double[12], hshobstwi = new double[12], hsh = new double[12], AreaC = new double[12];

            PVType = PVdata[6];
            totalArea = Convert.ToDouble(PVdata[7]);
            Kpk = Convert.ToDouble(PVdata[8]) / totalArea;

            fPerf = fPerf - (1-InverterEff);

            tan = Math.Tan(Slope * Math.PI / 180.0);


            for(int c = 0; c < 12; c++)
            {
                double x = 0, y = 0;
                hshobst[c] = 0;
                hshobstwi[c] = 0;
                hsh[c] = 0;
                AreaC[c] = 0;
                hshobst[c] = Math.Min(Arrayheight, Math.Max(0, shHeight - shLength * Math.Tan(PVαsol[c] * Math.PI / 180.0))); //수지길이  a
                hshobstwi[c] = hshobst[c] / Math.Tan(PVαsol[c] * Math.PI / 180.0); //수평길이  b
                x = hshobst[c] / (tan + hshobst[c] / hshobstwi[c]);
                y = tan * x;

                if (Math.Sqrt(Math.Pow(hshobst[c], 2) + Math.Pow(hshobstwi[c], 2)) <= 0)
                {
                    hsh[c] = 0;
                }
                else hsh[c] = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));

                AreaC[c] = (totalArea / Arrayheight) * (Arrayheight - hsh[c]);
            }

            if(PVType == "계통연계형")
            {
                for (int b = 0; b < 12; b++)
                {
                    Qfpvm_kWh[b] = 0;
                    Qfpvm_m2_kWh[b] = 0;
                    Qfpvm_kWh[b] = Esol[b] * Kpk * AreaC[b] * 0.9 * fPerf;
                    Qfpva_kWh += Qfpvm_kWh[b];
                    Qfpvm_m2_kWh[b] = Esol[b] * Kpk * AreaC[b] * 0.9 * fPerf / totalArea;                    
                }
            }
            else if(PVType == "독립형")
            {
                string batteryType;
                double Cnenm, ηDoD, ηBatt;
                PVBatteryNumber = PVdata[9];
                string[][] battery = Program.DB.getValue(DB.type.ProjDB, "User_PVBattery", "정격전력,배터리타입", "번호='" + PVBatteryNumber + "'");

                Cnenm = Convert.ToDouble(battery[0][0]);
                batteryType = battery[0][1].ToString();
                
                string[][] Binfo = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광배터리계수", "최대방전깊이,시스템효율", "배터리타입 ='" + batteryType + "'");

                ηDoD = Convert.ToDouble(Binfo[0][0]);
                ηBatt = Convert.ToDouble(Binfo[0][1]);
                            
                Cal_Battery();

                for (int b = 0; b < 12; b++)
                {
                    Qfpvm_kWh[b] = 0;
                    Qfpvm_m2_kWh[b] = 0;
                    Qfpvm_kWh[b] = Esol[b] * Kpk * AreaC[b] * 0.9 * fPerf * fBatt[b];
                    Qfpva_kWh += Qfpvm_kWh[b];
                    Qfpvm_m2_kWh[b] = Esol[b] * Kpk * AreaC[b] * 0.9 * fPerf / totalArea;
                }
            }
        }

        public void Cal_Battery()
        {
            for(int j=0; j < 12; j++)
            {
                Qf_elec[j] = 0;
                string k = (j + 1).ToString() + "월";
                string[][] value = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "연료='전기' And 월='"+k+"'");
                if (value.Length > 0)
                {
                    Qf_elec[j] = Convert.ToDouble(value[0][0]);
                }
            }
            
            Ceff = Cnenm * ηDoD;
            for (int mth = 0; mth < 12; mth++)
            {
                CQ[mth] = Ceff / Qf_elec[mth] * 100;
                γQ[mth] = PVPpk_kW / Qf_elec[mth] * 100;
                fBatt[mth] = Math.Max(1, (0.2 * Math.Log(γQ[mth], Math.E) + 1.85) * Math.Pow(CQ[mth], (0.1 * Math.Log(γQ[mth], Math.E) + 0.25)));
            }
        }

        #endregion

        #region PV별 저장
        public void PVsave()
        {
            string[] month = new string[12];

            if (PVType == "계통연계형")
            {
                for (int a = 0; a < 12; a++)
                {
                    month[a] = (a + 1).ToString() + "월";
                    Program.DB.setValue(DB.type.ProjDB, "PV_Result", "프로젝트번호,프로젝트유형,번호,월,일사량,PV생산량", "'" + 프로젝트번호 + "','" + 프로젝트유형 + "','" + Num + "','" +
                   month[a] + "','" + Esol[a] + "','" + Qfpvm_kWh[a] + "'", "번호, 월");
                }
            }
            else if (PVType == "독립형") 
            { 
                for (int a = 0; a < 12; a++)
                {
                    month[a] = (a + 1).ToString() + "월";
                    Program.DB.setValue(DB.type.ProjDB, "PV_Result", "프로젝트번호,프로젝트유형,번호,월,일사량,PV생산량,배터리손실", "'" + 프로젝트번호 + "','" + 프로젝트유형 + "','" + Num + "','" +
                   month[a] + "','" + Esol[a] + "','" + Qfpvm_kWh[a] + "','" + fBatt[a] +"'", "번호, 월");
                }
            }

            string RESystemNum = "";
            string[][] value = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "번호", "신재생시스템='" + Num + "'");
            if (value.Length > 0)
            {
                RESystemNum = value[0][0];
            }
            else
            {
                RESystemNum = Program.UTIL.CreateNum("RESystem_Result", "번호", "RE");
            }
            for (int mth = 0; mth <= 11; mth++)
            {
                string MTH = (mth + 1).ToString() + "월";
                Program.DB.setValue(DB.type.ProjDB, "RESystem_Result", "프로젝트번호,프로젝트유형,번호," +
                 "월," +
                 "신재생시스템,신재생시스템유형,생산소비,생산유형,총에너지",
                 "'" + 프로젝트번호  + "','" + 프로젝트유형 + "','" + RESystemNum + "','" + MTH + "','" +
                Num + "','태양광시스템','생산','전기','" +
                Qfpvm_kWh[mth]
                  + "'", "번호,월,생산소비,생산유형"); ;
            }

        }
        #endregion

        #region PV전체 계산 및 저장
       
        public void Cal_totalPV()
        {
            string[][] valuecount = Program.DB.getValue_SameCheck(DB.type.ProjDB, "PV_Result", "번호", "프로젝트유형 = '" + 프로젝트유형 + "' And 프로젝트번호 = '" + 프로젝트번호 + "'");
            List<string> va = new List<string>();
            for (int j = 0; j < valuecount.Length; j++)
            {
                va.Add(valuecount[j][0]);
            }
            Cal_totalQfkWh(va);
            Cal_totalBattery(va);
        }

        public void Cal_totalQfkWh(List<string> _pvname )//ISO인경우 
        {
            for(int k = 0; k < 12; k++)
            {
                Qfkwh_totalBattery[k] = 0;
                Qfkwh_totalGrid[k] = 0;
                int a = k;
                string m = a.ToString()+"월";
                for(int j=0; j < _pvname.Count; j++)
                {
                    string[][] value = Program.DB.querySQL(DB.type.ProjDB, "select a.PV생산량, b.계통유형 from PV_Result as a Inner Join PV_Form as b on a.번호= b.번호 where a.번호='" + _pvname[j] + "' And 월 ='" + m + "'");
                    if (value[0][1] == "계통연계형")
                    {
                        Qfkwh_totalGrid[k] = Convert.ToDouble(value[0][0]);
                    }
                    else
                    {
                        Qfkwh_totalBattery[k] = Convert.ToDouble(value[0][0]);
                    }
                }
            }
        }

        public void Cal_totalBattery(List<string> _pvname)
        {
            for (int j = 0; j < 12; j++)
            {
                Qf_elec[j] = 0;
                string k = (j + 1).ToString() + "월";
                string[][] value = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "연료='전기' And 월='" + k + "'");
                Qf_elec[j] = Convert.ToDouble(value[0][0]);
            }
            //배터리 방전깊이와 용량 그리고 효율
            //대표타입에 따른 방전깊이 지정
            //용량합 그리고 용량가중 효율 지정
            
            List<string> names = new List<string>();
            List<double> capacity = new List<double>();
            ηDoD = 0; //방전깊이
            Cnenm = 0; //총용량
            ηBatt = 0; //배터리효율

            for(int a=0; a<_pvname.Count; a++)
            {
                string[][] batt = Program.DB.getValue(DB.type.ProjDB, "PV_Form", "배터리번호,배터리용량", "번호='" + _pvname[a] + "'");
                if (batt[0][0] != null || batt[0][0] != "")
                {
                    string[][] typ = Program.DB.getValue(DB.type.ProjDB, "User_PVBattery", "배터리타입,정격전력", "번호 = '" + batt[0][0] +"'");
                    names.Add(typ[0][0]);
                    capacity.Add(Convert.ToDouble(typ[0][1]));
                }
            }

            if (names.Count > 0)
            {
                string type = names[capacity.IndexOf(capacity.Max())];
                string[][] va = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광배터리계수", "최대방전깊이,시스템효율,방전시간", "배터리타입 = '" + type + "'");
                ηDoD = Convert.ToDouble(va[0][0]);
                ηBatt = Convert.ToDouble(va[0][1]);

                for (int b = 0; b < capacity.Count; b++)
                {
                    Cnenm += capacity[b];
                }

                Ceff = Cnenm * ηDoD;
                for (int mth = 0; mth < 12; mth++)
                {
                    CQ[mth] = Ceff / Qf_elec[mth] * 100;
                    γQ[mth] = PVPpk_kW / Qf_elec[mth] * 100;
                    fBatt[mth] = Math.Max(1, (0.2 * Math.Log(γQ[mth], Math.E) + 1.85) * Math.Pow(CQ[mth], (0.1 * Math.Log(γQ[mth], Math.E) + 0.25)));
                }
            }
            else
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    fBatt[mth] = 1;
                }
                ηBatt = 1;
            }
        }

        public void Cal_fmatch() //ISO인경우 
        {
            double[] x = new double[12];
            for (int mth = 0; mth < 12; mth++)
            {
                x[mth] = (Qfkwh_totalBattery[mth] + Qfkwh_totalGrid[mth]) / Qf_elec[mth];
                fmatch[mth] = (x[mth] + 1 / x[mth] - 1) / (x[mth] + 1 / x[mth]);
            }
        }

        public void Cal_totalQf( )
        {
            for (int mth = 0; mth < 12; mth++)
            {
                Qf_nutz_grid[mth] = 0;
                Qbatt_loss[mth] = 0;
                Qf_nutz_build[mth] = 0;

                Qf_nutz_grid[mth] = fmatch[mth] * (Qfkwh_totalBattery[mth] + Qfkwh_totalGrid[mth]); //송전량
                Qbatt_loss[mth] = Qf_nutz_grid[mth] * (1 - ηBatt) * (fBatt[mth] - 1);
                Qf_nutz_build[mth] = (Qfkwh_totalBattery[mth] + Qfkwh_totalGrid[mth] - Qf_nutz_grid[mth] ) * fBatt[mth] - Qbatt_loss[mth]; //건물사용량
            }

        }
        public void PVtotalsave()
        {

        }

        #endregion

        #region 풍력

        string Num_WP; // 풍력번호 ID

        public string WP, condition, Inverter, Inverter_num; //풍력  UWP , 인버터제품명, 인버터 UIV
        public double[] h = new double[33];
        public double[] h_mth = new double[12]; //월별 가동시간
        public double[] V1 = new double[33];
        public double[] V2 = new double[33];
        //public double[] V2_near = new double[33];
        public double h1, h2, a, Euro;
        public string[][] 지역;
        public double p = 1.225; //kg/m3
        //V1
        public double Arotor, P;
        public double[] vwk = new double[33];
        public double[] Pwindwk_sub = new double[33];
        public double[] Pwindwk = new double[33];
        public double[] Pwindwk_mth = new double[12]; //월별 풍속 구간별 풍력 출력 합

        public double Vsvk, Vmvk, Vlvk, Cpmin, Cpop, Cpmax;
        public double[] Cp = new double[33];

        public double[] Pwps = new double[33];
        public double[] Pwps_mth = new double[12];//월별 풍속 구간별 풍력발전 시스템 출력

        public double[,] twk = new double[33, 12];

        public double[,] Qfwps = new double[33, 12];
        public double[] Qfwps_mth = new double[12];

        public double install;

        //풍속 구하기
        //V2 구하기 


        public void Cal_WF(string number) //풍력번호
        {
            Num_WP = number;
        }
        public void WF_LoadData()
        {

            지역 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");

            //지역, 풍속 33구간별, 월별 가동시간 가져오기 
            for (int mth = 0; mth < 12; mth++)
            {
                for (int k = 0; k < 33; k++)
                {
                    string[][] Valueaaa = Program.DB.querySQL(DB.type.BaseDB_HCneed, "Select 시간 From 기후데이터_풍력가동시간 where 지역명 = '" + 지역[0][0] + "' and 기간 = '" + (mth + 1) + "월'");
                    if (Valueaaa.Length > 0)
                    {
                        h[k] = Convert.ToDouble(Valueaaa[k][0]);
                    }

                    h_mth[mth] += h[k];  //월별 가동시간 합산 (레포트)

                }
            }

            string[][] ValueB = Program.DB.getValue(DB.type.ProjDB, "WindPower_Form", "풍력, 설치높이, 주변환경, 인버터제품, 인버터, 설치대수", "번호 ='" + Num + "'");
            // 풍속고도분포지수가 아니라 주변환경 가져와서 풍속고도분포지수 값 DB에서 찾기
            if (ValueB.Length > 0)
            {
                WP = ValueB[0][0].ToString();
                h2 = Convert.ToDouble(ValueB[0][1]);
                condition = ValueB[0][2].ToString();
                Inverter = ValueB[0][3].ToString();
                Inverter_num = ValueB[0][4].ToString();
                install = Convert.ToDouble(ValueB[0][5].ToString());
            }

            string[][] ValueA = Program.DB.getValue(DB.type.ProjDB, "User_WP", "회전면적, 정격출력", "번호 ='" + WP + "'");

            if (ValueA.Length > 0)
            {
                Arotor = Convert.ToDouble(ValueA[0][0]);
                P = Convert.ToDouble(ValueA[0][1]);
            }
            string[][] ValueC = Program.DB.getValue(DB.type.ProjDB, "User_WP", "시동풍속, 최적풍속, 종단풍속, 시동풍속전력계수, 최적풍속전력계수, 종단풍속전력계수", "번호 ='" + WP + "'");

            if (ValueC.Length > 0)
            {
                Vsvk = Convert.ToDouble(ValueC[0][0]);
                Vmvk = Convert.ToDouble(ValueC[0][1]);
                Vlvk = Convert.ToDouble(ValueC[0][2]);
                Cpmin = Convert.ToDouble(ValueC[0][3]);
                Cpop = Convert.ToDouble(ValueC[0][4]);
                Cpmax = Convert.ToDouble(ValueC[0][5]);
            }
            //인버터 효율 
            if (Inverter_num.Contains("U"))
            {
                string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "User_WPInverter", "EURO효율", "제품명='" + Inverter + "'");
                if (value2.Length > 0)
                {
                    Euro = Convert.ToDouble(value2[0][0]);
                }
            }
            else
            {
                string[][] value3 = Program.DB.getValue(DB.type.BaseDB_RESystem, "풍력인버터DB", "EURO효율", "제품명='" + Inverter + "'");
                if (value3.Length > 0)
                {
                    Euro = Convert.ToDouble(value3[0][0]);
                }
            }
        }

        public void WF_Calc_V2()
        {
            //허브높이는 계산에 영향 X 
            //h1 = 관측장비 높이 = 지역에 따른 값
            //h2 = 설치 높이 = 폼 직접입력값
            //a = 풍속고도분포지수 = 폼 콤보박스 선택 
            string[][] ValueB = Program.DB.querySQL(DB.type.BaseDB_HCneed, "Select Distinct CAST(풍속 AS FLOAT) AS 풍속 from 기후데이터_풍력가동시간 Order by 풍속");
            int i = -1;
            if (ValueB.Length > 0)
            {
                while (++i < ValueB.Length)
                {
                    V1[i] = Convert.ToDouble(ValueB[i][0]);
                }
            }

            string[][] ValueA = Program.DB.getValue(DB.type.BaseDB_RESystem, "풍력관측소높이", "높이", " 지역 = '" + 지역[0][0] + "'");

            if (ValueA.Length > 0)
            {
                h1 = Convert.ToDouble(ValueA[0][0]);
            }

            string[][] ValueC = Program.DB.getValue(DB.type.BaseDB_RESystem, "풍력풍속고도분포지수", "풍속고도분포지수", "지역='" + condition + "'");
            if (ValueC.Length > 0)
            {
                a = Convert.ToDouble(ValueC[0][0]);
            }

            for (int k = 0; k < 33; k++)
            {
                V2[k] = V1[k] * Math.Pow(h2 / h1, a);
            }
        }

        //풍속 구간별 풍력 출력
        public void WF_Calc_Pwind()
        {
            //Arotor = 회전면적
            //vwk = v1 = 지정풍속 (0~16 / 0.5단위), P = 정격출력
            //풍속구간 배열 중복 제거하기
            for (int mth = 0; mth < 12; mth++)
            {
                for (int i = 0; i < 33; i++)
                {
                    Pwindwk_sub[i] = 0.5 * p * Arotor * Math.Pow(V2[i], 3); //W 단위로 환산

                    if (Pwindwk_sub[i] > P)
                    {
                        Pwindwk[i] = P * 1000;
                    }
                    else
                    {
                        Pwindwk[i] = Pwindwk_sub[i];
                    }

                    Pwindwk_mth[mth] += Pwindwk[i]/1000;
                }
            }

        }


        //풍속 전력계수
        public void WF_Calc_Cp() //풍속 구간별 전력계수
        {
            //구간별 풍속(vwk)
            //시동풍속(Vsvk), 최적풍속(Vmvk), 종단풍속(Vlvk)
            //시동풍속 지점 전력계수(Cpmin), 최적풍속 지점 전력계수(Cpop), 종단풍속 지점 전력계수(Cpmax)

            for (int k = 0; k < 33; k++)
            {
                if (Vsvk > V1[k] || Vlvk < V1[k]) // 시동풍속(Vsvk)이 구간별 풍속(vwk)보다 크거나, 종단풍속(Vlvk)이 구간별 풍속(vwk)보다 작을 때
                {
                    Cp[k] = 0;
                }
                else if (Vmvk >= V1[k]) // 최적풍속(Vmvk)이 구간별 풍속(vwk)보다 크거나 같을 때 
                {
                    Cp[k] = Cpmin + (Cpop - Cpmin) * ((V1[k] - Vsvk) / (Vmvk - Vsvk));
                }
                else if (Vmvk < V1[k]) // 최적풍속(Vmvk)이 구간별 풍속(vwk)보다 작을 때
                {
                    Cp[k] = Cpop + (Cpmax - Cpop) * ((V1[k] - Vmvk) / (Vlvk - Vmvk));
                }
            }
        }

        //풍속 구간별 풍력발전 시스템 출력
        public void WF_Calc_Pwps()
        {
            for (int mth = 0; mth < 12; mth++)
            {
                for (int k = 0; k < 33; k++)
                {
                    Pwps[k] = Pwindwk[k] * Cp[k];

                    Pwps_mth[mth] += Pwps[k]/1000;
                }
            }
        }
    

        //월별 풍력발전 에너지 생산량 
        //인버터 효율 곱해주기 //대수 곱해주기
        public void WF_Calc_Qfwps()
        {
            //twk(가동시간)
            for (int mth = 0; mth < 12; mth++)
            {
                지역 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
               

                for (int k = 0; k < 33; k++)
                {
                    if (4 <= V1[k] || 16 > V1[k])
                    {
                        string[][] ValueA = Program.DB.querySQL(DB.type.BaseDB_HCneed, "Select 시간 From 기후데이터_풍력가동시간 where 지역명 = '" + 지역[0][0] + "' and 기간 = '" + (mth + 1) + "월'");
                        if (ValueA.Length > 0)
                        {
                            twk[k, mth] = Convert.ToDouble(ValueA[0][0]);
                            Qfwps[k, mth] = ((twk[k, mth] * Pwps[k] * (Euro / 100))/1000) * install ;
                        }
                    }
                    else
                    {
                        Qfwps[k, mth] = 0;
                    }
                }
                for (int k = 0; k < 33; k++)
                {
                    Qfwps_mth[mth] += Qfwps[k, mth];        
                }
            }
        }

        #endregion
    }

    public class RESystem
    {
        public string RE_Num, RE_RESystem_Num, RE_RESystem_Type, RE_Production_Consumption, RE_Production_Type, RE_Consumption_Carrier, RE_Heating_Num, RE_Cooling_Num, RE_DHW_Num;
        public double[] RE_TotalE = new double[12], RE_HeatingE = new double[12], RE_CoolingE = new double[12], RE_DHWE = new double[12], RE_LightingE = new double[12], RE_AHUE = new double[12];
        public RESystem(string Num, string RESystem_Num, string RESystem_Type, string Production_Consumption, string Production_Type, string Consumption_Carrier, string Heating_Num, string Cooling_Num, string DHW_Num, double[] TotalE, double[] HeatingE, double[] CoolingE, double[] DHWE, double[] LightingE, double[] AHUE)
        {
            this.RE_Num = Num;
            this.RE_RESystem_Num = RESystem_Num;
            this.RE_RESystem_Type = RESystem_Type;
            this.RE_Production_Type = Production_Type;
            this.RE_Production_Consumption = Production_Consumption;
            this.RE_Consumption_Carrier = Consumption_Carrier;
            this.RE_Heating_Num = Heating_Num;
            this.RE_DHW_Num = DHW_Num;
            this.RE_Cooling_Num = Cooling_Num;
            this.RE_TotalE = TotalE;
            this.RE_HeatingE = HeatingE;
            this.RE_CoolingE = CoolingE;
            this.RE_DHWE = DHWE;
            this.RE_LightingE = LightingE;
            this.RE_AHUE = AHUE;
        }
        public String Num()
        {
            return RE_Num;
        }
        public String RESystem_Num()
        {
            return RE_RESystem_Num;
        }
        public String RESystem_Type()
        {
            return RE_RESystem_Type;
        }
        public String Production_Consumption()
        {
            return RE_Production_Consumption;
        }
        public String Production_Type()
        {
            return RE_Production_Type;
        }
        public String Consumption_Carrier()
        {
            return RE_Consumption_Carrier;
        }
        public String Heating_Num()
        {
            return RE_Heating_Num;
        }
        public String Cooling_Num()
        {
            return RE_Cooling_Num;
        }
        public String DHW_Num()
        {
            return RE_DHW_Num;
        }
        public double[] TotalE()
        {
            return RE_TotalE;
        }
        public double[] HeatingE()
        {
            return RE_HeatingE;
        }
        public double[] CoolingE()
        {
            return RE_CoolingE;
        }
        public double[] DHWE()
        {
            return RE_DHWE;
        }
        public double[] LightingE()
        {
            return RE_LightingE;
        }
        public double[] AHUE()
        {
            return RE_AHUE;
        }
    }
}