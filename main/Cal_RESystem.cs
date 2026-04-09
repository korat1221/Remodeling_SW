using main.subcontents.CoolingSystem;
using main.subcontents.HeatingSystem;
using System;
using System.Collections;
using System.Security.Cryptography;

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
        public void PVsave(string ProjNum)
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

            Save_Memory_PV();
        }
        public void Save_Memory_PV()
        {
            string RESystemNum = null;

            if (CALC.RESystems.Count == 0)
            {
                RESystemNum = "RE01";
            }


            ArrayList arr_renum = new ArrayList();
            int i = 0;
            foreach (var system in CALC.RESystems.Values)
            { 
                if(!arr_renum.Contains(system.RE_Num))
                {
                    arr_renum.Add(system.RE_Num);
                    i++;
                }
            }

            RESystemNum = "RE0" + (i + 1);
            
            foreach (var system in CALC.RESystems.Values)
            {
                if (system != null && system.RESystem_Num() == Num)
                {
                    RESystemNum = system.Num();
                    break; // 찾았으면 더 이상 반복하지 않음
                }
            }

            RESystem news = new RESystem(RESystemNum,"생산","전기","");
            news.RE_Production_Consumption = "생산";
            news.RE_Production_Type = "전기";
            news.RE_RESystem_Num = Num;
            news.RE_RESystem_Type = "태양광시스템";
            news.RE_TotalE = Qfpvm_kWh;

            string[] sy = new string[4];
            sy[0] = news.Num();
            sy[1] = "생산";
            sy[2] = "전기";
            sy[3] = "";
            if (news.Num() != "")
            {
                CALC.RESystems[sy] = news;
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
        string 지역;
        double[] dmth = {31,28,31,30,31,30,31,31,30,31,30,31};
        double[,] Vr = new double[12, 744]; double[,] V1 = new double[12, 744]; double[,] V2 = new double[12, 744];
        double[,] t_wkn = new double[12, 17];
        public double[] Qfwps = new double[12];
        double 회전면적, 허브높이, 정격출력, 정격출력풍속, 설치높이, 설치대수, h1 = 0;
        int 시동풍속, 종단풍속;
        ArrayList 풍속구간출력 = new ArrayList();
        string 적용유형, 풍속구간출력_nonsplit, 주변환경;
        double raw = 1.225;
        public void WP_LoadData()
        {
            string[][]  Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
            if(Value.Length >0)
            {
                지역 = Value[0][0];
                string[][] Value2 = Program.DB.getValue(DB.type.BaseDB_RESystem, "시간별기후데이터", "풍속", "지역='" + 지역 + "' ORDER BY 월, 일, 시;");
                if (Value2.Length > 0)
                {
                    int t = 0;
                    for (int mth = 1; mth < 13; mth++)
                    {
                        for (int day = 1; day <= dmth[mth - 1]; day++)
                        {
                            for (int time = 1; time <= 24; time++)
                            {
                                Vr[mth - 1, (day - 1) * 24 + time - 1] = Convert.ToDouble(Value2[t][0]);
                                t++;
                            }
                        }
                    }
                }
            }
            Value= Program.DB.querySQL(DB.type.ProjDB, "Select  b.회전면적, b.허브높이, b.시동풍속, b.종단풍속, b.정격출력, b.정격출력풍속, b.적용유형, b.풍속구간별출력, a.주변환경,a.설치높이,a.설치대수 From  WindPower_Form as a inner join  User_WP as b on a.풍력=b.번호 Where a.번호='"+Num+"'");
            if (Value.Length >0)
            {
                회전면적 = Convert.ToDouble(Value[0][0]);
                허브높이 = Convert.ToDouble(Value[0][1]);
                시동풍속 = Convert.ToInt32(Value[0][2]);
                종단풍속 = Convert.ToInt32(Value[0][3]);
                정격출력 = Convert.ToDouble(Value[0][4]);
                정격출력풍속 = Convert.ToDouble(Value[0][5]);
                적용유형 = Value[0][6];
                풍속구간출력_nonsplit = Value[0][7];
                if(풍속구간출력_nonsplit.Contains('+'))
                {
                    풍속구간출력 = Split_(풍속구간출력_nonsplit);
                }
                else
                {
                    풍속구간출력.Clear();
                    for(int  v= 시동풍속; v<=종단풍속; v++)
                    {
                        double 출력 = 0.5 * 회전면적 * 1.225 * 0.2 * Math.Pow(v, 3);
                        풍속구간출력.Add(출력);
                    }                    
                }
                주변환경 = Value[0][8];
                설치높이 = Convert.ToDouble(Value[0][9]);
                설치대수 = Convert.ToDouble(Value[0][10]);
            }
        }

        public void WP_Calc_V1()
        {
            double KR=0, Z0=0, Zmin=0,  CR=0, CT=1;
            string[][] Value = Program.DB.getValue(DB.type.BaseDB_RESystem, "지면거칠기계수", "KR,Z0,Zmin", "지형구분='" + 주변환경 + "'");
            if(Value.Length >0)
            {
                KR = Convert.ToDouble(Value[0][0]);
                Z0 = Convert.ToDouble(Value[0][1]);
                Zmin = Convert.ToDouble(Value[0][2]);
            }
            Value = Program.DB.getValue(DB.type.BaseDB_RESystem, "관측장비지상높이", "관측장비지상높이", "지역='" + 지역 + "'");
            if(Value.Length >0)
            {
                h1 = Convert.ToDouble(Value[0][0]); 
            }

           if(h1 >=Zmin)
            {
                CR = KR * Math.Log(h1 / Z0);
            }
            else
            {
                CR = KR * Math.Log(Zmin / Z0);
            }
           // V1계산 
            for (int mth = 1; mth < 13; mth++)
            {
                for (int day = 1; day <= dmth[mth - 1]; day++)
                {
                    for (int time = 1; time <= 24; time++)
                    {
                        V1[mth - 1, (day - 1) * 24 + time - 1] = Vr[mth - 1, (day - 1) * 24 + time - 1] * CR * CT;
                        
                    }
                }
            }
        }

        public void WP_Calc_V2()
        {
            double hc = 0, h2 = 0, α = 0.14;
            hc = 설치높이;
            if (hc == 0)
            {
                if (회전면적 <= 3.5)
                {
                    h2 = 6 + (20 - 6) * (회전면적 - 0) / (3.5 - 0);
                }
                else if (회전면적 > 3.5 && 회전면적 <= 40)
                {
                    h2 = 12 + (30 - 12) * (회전면적 - 3.5) / (40 - 3.5);
                }
                else
                {
                    h2 = 20 + (50 - 20) * (회전면적 - 40) / (200 - 40);
                }
            }
            else
            {
                h2 = hc + 허브높이;
            }
            // V2계산 
            for (int mth = 1; mth < 13; mth++)
            {
                for (int day = 1; day <= dmth[mth - 1]; day++)
                {
                    for (int time = 1; time <= 24; time++)
                    {
                        V2[mth - 1, (day - 1) * 24 + time - 1] = V1[mth - 1, (day - 1) * 24 + time - 1] * Math.Pow(h2 / h1, α);

                    }
                }
            }
        }

        public void WP_Calc_t_wkn()
        {
            for(int mth =0; mth < 12; mth ++)
            {
                int[] count = new int[17];

                for(int v =0; v< 17; v++)
                {
                    count[v] = 0;
                }
                #region 풍속 0인 구간
                for (int i = 0; i < V2.GetLength(1); i++)
                {
                    if (V2[mth, i] < 0.5)
                    {
                        count[0]++;
                    }
                }
                t_wkn[mth, 0] = count[0];
                #endregion

                #region 풍속 1~15인 구간
                for (int v =1; v < 16; v ++)
                {
                    for (int i = 0; i < V2.GetLength(1); i++)
                    {
                        if (V2[mth, i] > v -0.5 && V2[mth,i]<= v + 0.5)
                        {
                            count[v]++;
                        }
                    }
                    t_wkn[mth, v] = count[v];
                }
                #endregion

                #region 풍속 16인 구간
                for (int i = 0; i < V2.GetLength(1); i++)
                {
                    if (V2[mth, i] > 15.5)
                    {
                        count[16]++;
                    }
                }
                t_wkn[mth, 16] = count[16];
                #endregion 
            }

        }
        public void WP_Calc_Qfwps()
        {
           
            for(int mth =0;  mth < 12; mth++)
            {
                int a = 0;
                for (int v= 시동풍속; v <= 종단풍속; v++ )
                {
                    Qfwps[mth] += t_wkn[mth, v] * Convert.ToDouble(풍속구간출력[a]) /1000 * 설치대수;
                    a++;
                } 
            }
        }

        private ArrayList Split_(String nonSplit)
        {
            ArrayList split = new ArrayList();
            if (nonSplit != null && nonSplit != "")
            {
                if (nonSplit.Contains('+'))
                {
                    string[] token = nonSplit.Split('+');
                    split.Clear();
                    foreach (var item in token)
                    {
                        split.Add(item.ToString());
                    }
                }
                else
                {
                    split.Clear();
                    split.Add(nonSplit);
                }
            }
            else
            {
                split.Clear();
            }
            return split;
        }

        #endregion
    }

    public class RESystem
    {
        public string RE_Num, RE_RESystem_Num, RE_RESystem_Type, RE_Production_Consumption, RE_Production_Type, RE_Consumption_Carrier, RE_Heating_Num, RE_Cooling_Num, RE_DHW_Num;
        public double[] RE_TotalE = new double[12], RE_HeatingE = new double[12], RE_CoolingE = new double[12], RE_DHWE = new double[12], RE_LightingE = new double[12], RE_AHUE = new double[12];
        public RESystem(string Num, string Production_Consumption, string Production_Type, string Consumption_Carrier)
        {
            this.RE_Num = Num;
            this.RE_Production_Consumption = Production_Consumption;
            this.RE_Production_Type = Production_Type;
            this.RE_Consumption_Carrier = Consumption_Carrier;
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