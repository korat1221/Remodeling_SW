using main
    ;
using main.subcontents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main
{
    internal class Cal_Cooling
    {
        //설비지정
        string CoolingNum, 프로젝트유형, CoolingName, InstallType, CGType; //InstallType는 기존/신규임
        double CWin, CWout; //냉수공급시

        //냉각탑
        double CTpower, CSWin, CSWout; //냉각탑 및 지열등 냉각수관련

        

        //설비정보
        double Power_f, EER_f, Pctrl_f;
        List<double> Power = new List<double>(), EER = new List<double>(), Pctrl_el = new List<double>(); //공통
        List<int> Number = new List<int>(); //설치대수


        //기후데이터 작성
        public double[] OutdoorTemperature = new double[12], Humidity = new double[12], WetTemperature = new double[12];
        string[] mth = new string[12];
        
        //냉방존
        List<zonemake> ZoneNameList = new List<zonemake>();
        public double[] QC_nd_z = new double[12], dwd_z = new double[12], theta_z = new double[12], betac_z = new double[12], tmth_wd_z = new double[12], BC_z = new double[12];
        public double[] tC_op_z = new double[12], PL_Rate_z = new double[12], fC_PL_z = new double[12];
        public double top_c_z, Qc_max_z, Qc_p_z, Beta_grenz_z, QC_a_z, A_z; //QC_p_z 공조기와 파워나누기
        string SCZoneType_z;
        public double[] QC_ce_z = new double[12], QC_d_z = new double[12], QC_s_z = new double[12], QC_out_z = new double[12];
        public double[] W_ce_z = new double[12], W_d_z = new double[12], W_s_z = new double[12], W_g_z = new double[12];

       
        //공조기
        List<ahumake> AHUNameList = new List<ahumake>();
        public double[] QC_nd_ahu = new double[12], dwd_ahu = new double[12], theta_ahu = new double[12], betac_ahu = new double[12], tmth_wd_ahu = new double[12], BC_ahu = new double[12];
        public double[] tC_op_ahu = new double[12], PL_Rate_ahu = new double[12], fC_PL_ahu = new double[12];
        public double top_c_ahu, Qc_max_ahu, Qc_p_ahu, Beta_grenz_ahu, QC_a_ahu, A_ahu; //QC_p_z 공조기와 파워나누기
        string SCZoneType_ahu;
        public double[] QC_ce_ahu = new double[12], QC_d_ahu = new double[12], QC_s_ahu = new double[12], QC_out_ahu = new double[12];
        public double[] W_ce_ahu = new double[12], W_d_ahu = new double[12], W_s_ahu = new double[12], W_g_ahu = new double[12];


        //분배/저장열손실         
        double nc_ce_sens, nc_ce, nc_d, nc_s, fSP;
        string Sto_Tank, Sto_Type;
       

        //냉방부분부하계산 및 최종 계산 결과값
        public double fC_mult, Theta_Around, ThetaC_gen_hr_req_in, ThetaC_gen_req_out, Theta_cond, Theta_evad;
        public double[] td_i = new double[12], feer_corr = new double[12], EER_c = new double[12], SEER_c = new double[12], Theta_IC = new double[12];
        public double[] QC_nd = new double[12], QC_ce = new double[12], QC_d = new double[12], QC_s = new double[12], QC_out = new double[12], QC_f = new double[12];
        public double QCa_nd, QCa_ce, QCa_d, QCa_s, QCa_out, QCa_f, QCa_p, QCa_CO2; //정의 필요
        public double[] W_ce = new double[12], W_d = new double[12], W_s = new double[12], W_g = new double[12],  W = new double[12]; //정의 필요

        double ColdWInput, ColdWOutput; //실외기 제외 모든유형 공통(지열히트펌프)
       
        List<string> Comp = new List<string>(); //공냉식, 수냉식, 지열히트펌프 유형
        string LoadSupply; //공냉식
        string Refriger; double PartLoad; //흡수식
       
        string CSource, CompType, ArtType, ArtNumber, Cout; // A 및 숫자에 대한 지정값 Cout : 직팽식, 수방식
        string Control_f, Econo_f ;
        int Number_f, ZoneNumber_f, AhuNumber_f; //설비개수, 존개수
        public string Carrier; ///연료 
        
        //존정보관련
        string SelectedZone, MultiZone;

        //펌프정의
        string SLRL, Complex, MainSystem, Sub1System, Sub2System, PumpUse, PumpMethod, Pump1, Pump2, Pump1Valve, Pump2Valve, Pump1Control, Pump2Control, ce1Type, ce2Type, ce3Type, ce4Type;
        int Pump1Num, Pump2Num;

        string SPValve;
        double P1power, P2power, SP1power, SP2power; //SP는 냉각수 및 지열열원 펌프를 지칭함 Source pump


        //공급설비정의
        string[] ceType = { "공조기", "실내기", "팬코일유닛", "복사냉방(천장)", "복사냉방(벽)", "바닥매립형컨백터" };
        ArrayList SelectAirConditioning = new ArrayList(); ArrayList SelectPump = new ArrayList(); ArrayList Selectce1Zone = new ArrayList(); ArrayList Selectce2Zone = new ArrayList();
        int ce_SelectRow;



        public Cal_Cooling(String _CoolingNum)
        {
            this.CoolingNum = _CoolingNum;
            
            generator();
            climate();
            
            //냉방존
            zone();
            zonetotal();
            CalQC_ced_z();
            CalQC_s_z();
            SCZone();
            
            //공조존필요함

            Cal_CS();
            Cal_Save();
        }
        #region
        public void report()
        {
           // string[][] value = Program.DB.setValue()
        }
        
        public void generator()
        {
            string[][] value = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_form",
              "명칭,냉방설비,냉방출력,냉방성능,대기전력,설치대수,연료,ZoneNumber_f,QC_a_z,QC_max_z,공급설비1종류,공급설비2종류,A_z,AhuNumber_f",
              "번호 = '" + CoolingNum + "'");

            CoolingName = value[0][0]; //명칭
            CGType = value[0][1]; //냉방설비
            Power_f = Convert.ToDouble(value[0][2]); //냉방출력
            EER_f = Convert.ToDouble(value[0][3]) * 0.7; //국내 냉방성능 시험은 EN 규정보다 높게 평가 됨
            Pctrl_f = Convert.ToDouble(value[0][4]); //대기전력
            Number_f = Convert.ToInt32(value[0][5]); //설치대수
            Carrier = value[0][6]; //연료
            ZoneNumber_f = Convert.ToInt32(value[0][7]); //존개수
            QC_a_z = Convert.ToDouble(value[0][8]); //존연간에너지요구량
            Qc_max_z = Convert.ToDouble(value[0][9]); //존최대냉방부하
            ce1Type = value[0][10]; //존공급설비1
            ce2Type = value[0][11]; //존공급설비2
            A_z = Convert.ToDouble(value[0][12]); //존 바닥면적
            AhuNumber_f = Convert.ToInt32(value[0][13]); //공조기 개수

            string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_form",
             "QC_a_Ahu,QC_max_Ahu,공급설비3종류,공급설비4종류,A_Ahu,열원설비,저장탱크,저장유형,제어유형,외기냉방시스템,부하측열원공급설비,설치",
             "번호 = '" + CoolingNum + "'");

            QC_a_ahu = Convert.ToDouble(value2[0][0]);
            Qc_max_ahu = Convert.ToDouble(value2[0][1]);
            ce3Type = value2[0][2];
            ce4Type = value2[0][3];
            A_ahu = Convert.ToDouble(value2[0][4]);
            CSource= value2[0][5];
            Sto_Tank = value2[0][6];
            Sto_Type = value2[0][7];
            Control_f = value2[0][8];
            Econo_f = value2[0][9];
            Cout = value2[0][10];
            InstallType = value2[0][11];

            if (CGType == "공냉식냉동기")
            {
                string[][] value3 = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_form",
                "압축기종류,냉수출구온도,펌프1밸브", "번호 = '" + CoolingNum + "'");
                CompType = value3[0][0];
                CWout = Convert.ToDouble(value3[0][1]);
                Pump1Valve = value3[0][2];

            }
                    
        }

        public void climate()//기후데이터
        {
            try
            {
                double Ref = 0.95;
                string[][] 지역 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
                string[][] OutdoorClimate = Program.DB.getValue(DB.type.BaseDB_HCneed," 기후데이터_온도습도", "온도,상대습도,기간","지역명 ='서울'"); //기후데이터 저장
                for(int i = 0; i < OutdoorTemperature.Length; i++) 
                {
                    OutdoorTemperature[i] = Convert.ToDouble(OutdoorClimate[i][0]);
                    Humidity[i] = Convert.ToDouble(OutdoorClimate[i][1]);
                    WetTemperature[i] = -5.809 + 0.058 * Ref*100 + 0.697 * OutdoorTemperature[i] + 0.003 * Ref * OutdoorTemperature[i]*100;
                    mth[i] = OutdoorClimate[i][2];
                }

                string[][] v = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉동기설치위치", "상승온도차", "위치 = '"+ CSource +"'");
                Theta_Around = Convert.ToDouble(v[0][0]);

            }
            catch { }

        }

        public void zone() //존정보작성, 공조기 정보 작성
        {
            string[][] ZoneList = Program.DB.getValue(DB.type.ProjDB, " CoolingZone", "존번호", "번호 ='" + CoolingNum + "'");
            for(int j = 0; j < ZoneList.Length; j++)
            {
                zonemake zoneinfo = new zonemake(ZoneList[j][0]);
                ZoneNameList.Add(zoneinfo);
            }
            ZoneNumber_f = ZoneList.Length;

            // string[][] AhuList = 
        }

        public void zonetotal() //존합계 작성
        {
            double[] sum_dwd = new double[12];
            double[] sum_theta = new double[12];
            double sumtop = 0;
            
            for (int i = 0; i < 12; i++)
            {
                foreach (zonemake value in ZoneNameList)
                {
                    QC_nd_z[i] += value.QC_nd_zt_j[i];
                }
                if (QC_nd_z[i] == 0)
                {
                    foreach (zonemake value in ZoneNameList)
                    {
                        sum_dwd[i] += value.dwd[i];
                        sum_theta[i] += value.θi_c[i];
                    }
                    dwd_z[i] = sum_dwd[i] / ZoneNameList.Count;
                    theta_z[i] = sum_theta[i] / ZoneNameList.Count;
                }
                else 
                {
                    foreach (zonemake value in ZoneNameList)
                    {
                        sum_dwd[i] += value.dwd[i]* QC_nd_z[i];
                        sum_theta[i] += value.θi_c[i] * QC_nd_z[i];
                    }
                    dwd_z[i] = sum_dwd[i] / QC_nd_z[i];
                    theta_z[i] = sum_theta[i] / QC_nd_z[i];
                }
                
                foreach (zonemake value in ZoneNameList)
                {
                    value.QC_rate[i] = value.QC_nd_zt_j[i] / QC_nd_z[i]; 
                }

                betac_z[i] = QC_nd_z[i] / (Qc_max_z * top_c_z * dwd_z[i]); //부하율계산
            }

            foreach (zonemake value in ZoneNameList)
            {
                Qc_max_z += value.QC_max;
                sumtop += value.QC_max * value.tC_op;
            }
            top_c_z = sumtop / Qc_max_z;
        }

        public void CalQC_ced_z()
        {
            //냉방존인 경우
            QC_a_z = 0;
            if(Cout == "직팽식")
            {
                string[][] value = Program.DB.getValue(DB.type.BaseDB_Cooling, "공급분배손실계수", "nc_ce_sens,nc_ce,nc_d", " 공급온도 = '" + Cout + "' And 공급유형 = '냉방존'");
                nc_ce_sens = Convert.ToDouble(value[0][0]);
                nc_ce = Convert.ToDouble(value[0][1]);
                nc_d = Convert.ToDouble(value[0][2]);

            }
            else
            {
                if (CWout <= 6)
                {
                    nc_ce_sens = 0.87;
                    nc_ce = 1;
                    nc_d = 0.9;
                }
                else if(CWout < 14)
                {
                    // x0 =6 x1= 14    y0= 0.87  y1= 1  x= CWout     y=y0 + (y1-y0)/(x1-x0) x (CWout - x0)
                    nc_ce_sens = 0.87 + (1-0.87) / (14-6) * (CWout-6);
                    nc_ce = 1;
                    nc_d = 0.9 + (1 - 0.9) / (14 - 6) * (CWout - 6);
                }
                else
                {
                    nc_ce_sens = 1;
                    nc_ce = 1;
                    nc_d = 1;
                }

            }

            for(int i = 0; i < 12; i++)
            {
                QC_ce_z[i] = ((1 - nc_ce_sens) + (1 - nc_ce)) * QC_nd_z[i];
                QC_a_z += QC_nd_z[i];
                QC_d_z[i] = (1 - nc_d) * QC_nd_z[i];
            }

            //공조기인 경우
        }
                                                       
        public void CalQC_s_z()
        {
            string[][] value = Program.DB.getValue(DB.type.BaseDB_Cooling, "저장제어운영계수", "이용계수", " 항목= '" + Sto_Tank + "' And 종류 = '" + Sto_Type + "'");

            if (value.Length > 0)
            {
                nc_s = Convert.ToDouble(value[0][0]);
                for (int i = 0; i < 12; i++)
                {
                    QC_s_z[i] = (1 - nc_s) * QC_nd_z[i];
                    QC_out_z[i] = QC_nd_z[i] + QC_ce_z[i] + QC_d_z[i] + QC_s_z[i];
                }
            }
            else 
            {
                for (int i = 0; i < 12; i++)
                {
                    QC_s_z[i] = 0;
                    QC_out_z[i] = QC_nd_z[i] + QC_ce_z[i] + QC_d_z[i] + QC_s_z[i];
                }
            }
        }

        //냉방존 설비계산
        public void SCZone() //냉방존 계산
        {
            if (ZoneNumber_f >= 2)
            {
                SCZoneType_z = "멀티존";
            }
            else SCZoneType_z = "단일존";

            //공조기 및 냉방존 부하율 검토
            Qc_p_z = Qc_max_z / (Qc_max_ahu + Qc_max_z) * Power_f;
            if (Econo_f == "있음")
            {
                Beta_grenz_z = 0.6;
            }
            else Beta_grenz_z = 0.3;
            
            for (int i = 0;i<12 ; i++)
            {
                tmth_wd_z[i] = top_c_z * dwd_z[i];
                if (QC_nd_z[i] == 0)
                {
                    BC_z[i] = 0;
                    tC_op_z[i] = 0;
                }
                else 
                {
                    BC_z[i] = QC_out_z[i] / (Qc_p_z * tmth_wd_z[i]);
                    if (BC_z[i] <= Beta_grenz_z)
                    {
                        tC_op_z[i] = tmth_wd_z[i] * BC_z[i] / Beta_grenz_z;
                    }
                    tC_op_z[i] = tmth_wd_z[i];
                }
            }
            f_PL_k();     
        }

        public void f_PL_k() //부하율, 종류, 번호
        {
            if(CGType == "실외기12kW")
            {
                string[][] value = Program.DB.getValue(DB.type.BaseDB_Cooling, "부분부하계수", "P1,P2,P3,P4,P5,P6,P7,P8,P9,P10,fC_M", " 설비유형= '" + CGType + "' And 제어유형 = '" + Control_f + "' And 공급유형 = '" + SCZoneType_z + "'");
                if (Number_f >= 2)
                {
                    fC_mult = Convert.ToDouble(value[0][10]);
                }
                else fC_mult = 1;

                //double BetaRate = 0.05;
                

                for (int i = 0; i < 12; i++) //냉방존 검토
                {
                    double B3 = 12;
                    double OutT = OutdoorTemperature[i] + Theta_Around;
                    
                    for (int h = 0; h < 10; h++)
                    {
                        if (OutT <= 12)
                        {
                            fC_PL_z[i] = Convert.ToDouble(value[0][0]);
                            break;
                        }
                        else if (OutT <= B3)
                        {
                            fC_PL_z[i] = Convert.ToDouble(value[0][h]);
                            break;
                        }
                        else if(OutT >= 31.8)
                        {
                            fC_PL_z[i] = Convert.ToDouble(value[0][9]);
                            break;
                        }
                        else
                        {
                            B3 = B3 + 2.2;
                        }
                    }
                }

                //for (int i = 0; i < 12; i++) //공조기 검토
                //{
                //    for (int h = 0; h < 10; h++)
                //    {
                //        if (BC_ahu[i] < 0.05)
                //        {
                //            fC_PL_ahu[i] = 1;
                //            break;
                //        }
                //        else if (BC_ahu[i] < BetaRate)
                //        {
                //            fC_PL_ahu[i] = Convert.ToDouble(value[0][h]);
                //            break;
                //        }
                //        else
                //        {
                //            BetaRate = BetaRate + 0.1;
                //        }
                //    }
                //}
                
            }
            else //그외의 경우(공냉식,수냉식등)
            {
                string[][] v2 = Program.DB.getValue(DB.type.BaseDB_Cooling, "부분부하계수", "P1,P2,P3,P4,P5,P6,P7,P8,P9,P10,종류,번호,fC_M", " 설비유형= '" + CGType + "' And 제어유형 = '" + Control_f + "' And 공급유형 = '" + CompType + "'");
                
                
                if (Number_f >= 2)
                {
                    fC_mult = Convert.ToDouble(v2[0][12]);
                }
                else fC_mult = 1;

                ArtType = v2[0][10];
                ArtNumber = v2[0][11];
                
                double B2 = 0.05;

                for (int i = 0; i < 12; i++) //냉방존 검토
                {
                    for (int h = 0; h < 10; h++)
                    {
                        if (BC_z[i] < 0.05)
                        {
                            fC_PL_z[i] = 1;
                            break;
                        }
                        else if (BC_z[i] < B2)
                        {
                            fC_PL_z[i] = Convert.ToDouble(v2[0][h]);
                            break;
                        }
                        else
                        {
                            B2 = B2 + 0.1;
                        }
                    }

                }
                   

                for (int i = 0; i < 12; i++) //공조기 검토
                {
                    for (int h = 0; h < 10; h++)
                    {
                        if (BC_ahu[i] < 0.05)
                        {
                            fC_PL_ahu[i] = 1;
                            break;
                        }
                        else if (BC_ahu[i] < B2)
                        {
                            fC_PL_ahu[i] = Convert.ToDouble(v2[0][h]);
                            break;
                        }
                        else
                        {
                            B2 = B2 + 0.1;
                        }
                    }
                    
                }
               
            }

        }

        //냉방에너지소요량 계산
        #endregion
        public void Cal_CS()
        {
            ThetaC_gen_hr_req_in = 0;
            ThetaC_gen_req_out = 0;
            Theta_cond = 0;
            Theta_evad = 0;


            string[][] v1;
            if (CGType == "공냉식냉동기")
            {
                v1 = Program.DB.getValue(DB.type.BaseDB_Cooling, "실외온도보정", "req_in, req_out, cond, evad", "냉방설비= '" + CGType + "' And 구분 = '" + Cout + "'");
                ThetaC_gen_hr_req_in = Convert.ToDouble(v1[0][0]);
                ThetaC_gen_req_out = Convert.ToDouble(v1[0][1]);
                Theta_cond = Convert.ToDouble(v1[0][2]);
                Theta_evad = Convert.ToDouble(v1[0][3]);
            }                                                                                                                                                                            
            else if (CGType == "흡수식냉동기")
            {
                //부분부하반영 성능계수 입력
                //ThetaC_gen_hr_req_in = Convert.ToDouble(v1[0][0]);
                //ThetaC_gen_req_out = Convert.ToDouble(v1[0][1]);
                //Theta_cond = Convert.ToDouble(v1[0][2]);
                //Theta_evad = Convert.ToDouble(v1[0][3]);
            }
            else if (CGType == "지열히트펌프")
            {
                v1 = Program.DB.getValue(DB.type.BaseDB_Cooling, "실외온도보정", "req_in, req_out, cond, evad", "냉방설비= '수냉식냉동기'");
                ThetaC_gen_hr_req_in = Convert.ToDouble(v1[0][0]);
                ThetaC_gen_req_out = Convert.ToDouble(v1[0][1]);
                Theta_cond = Convert.ToDouble(v1[0][2]);
                Theta_evad = Convert.ToDouble(v1[0][3]);
            }
            else
            {
                v1 = Program.DB.getValue(DB.type.BaseDB_Cooling, "실외온도보정", "req_in, req_out, cond, evad", "냉방설비= '" + CGType + "'");
                ThetaC_gen_hr_req_in = Convert.ToDouble(v1[0][0]);
                ThetaC_gen_req_out = Convert.ToDouble(v1[0][1]);
                Theta_cond = Convert.ToDouble(v1[0][2]);
                Theta_evad = Convert.ToDouble(v1[0][3]);
            }
               
            
            string[][] v2 = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉동기설치위치", "상승온도차", " 위치= '" + CSource + "'");
            Theta_Around = Convert.ToDouble(v2[0][0]);

            for(int i = 0; i<12 ;i++) 
            {
                if (theta_z[i] * theta_ahu[i] == 0)
                {
                    if (theta_z[i] > theta_ahu[i])
                    {
                        Theta_IC[i] = theta_z[i];
                    }
                    else
                    {
                        Theta_IC[i] = theta_ahu[i];
                    }                    
                }
                else
                {
                    Theta_IC[i] = QC_nd_z[i] * theta_z[i] + QC_nd_ahu[i] * theta_ahu[i] / (theta_z[i] * theta_ahu[i]);               
                }
                
                if(CGType=="실외기12kW")
                {
                    if (CSource == "패시브쿨링DEC")
                    {
                        double son1 = 273 + Theta_IC[i] - Theta_evad;
                        double son2 = 273 + WetTemperature[i] + Theta_Around + Theta_cond - (273 + Theta_IC[i] - Theta_evad);

                        double mutter1 = ThetaC_gen_hr_req_in - Theta_evad;
                        double mutter2 = (ThetaC_gen_req_out + Theta_cond) - (ThetaC_gen_hr_req_in - Theta_evad);

                        feer_corr[i] = (son1 / son2) / (mutter1 / mutter2);
                    }
                    else
                    {
                        double son1 = 273 + Theta_IC[i] - Theta_evad;
                        double son2 = 273 + OutdoorTemperature[i] + Theta_Around + Theta_cond - (273 + Theta_IC[i] - Theta_evad);

                        double mutter1 = ThetaC_gen_hr_req_in - Theta_evad;
                        double mutter2 = (ThetaC_gen_req_out + Theta_cond) - (ThetaC_gen_hr_req_in - Theta_evad);

                        feer_corr[i] = (son1 / son2) / (mutter1 / mutter2);
                    }
                } else if (CGType=="공냉식냉동기")
                {
                    if (Cout == "수방식")
                    {
                        if (CSource == "패시브쿨링DEC")
                        {
                            double son1 = 273 + CWout - Theta_evad;
                            double son2 = 273 + WetTemperature[i] + Theta_Around + Theta_cond - (273 + CWout - Theta_evad);

                            double mutter1 = ThetaC_gen_hr_req_in - Theta_evad;
                            double mutter2 = (ThetaC_gen_req_out + Theta_cond) - (ThetaC_gen_hr_req_in - Theta_evad);

                            feer_corr[i] = (son1 / son2) / (mutter1 / mutter2);
                        }
                        else
                        {
                            double son1 = 273 + CWout - Theta_evad;
                            double son2 = 273 + OutdoorTemperature[i] + Theta_Around + Theta_cond - (273 + CWout - Theta_evad);

                            double mutter1 = ThetaC_gen_hr_req_in - Theta_evad;
                            double mutter2 = (ThetaC_gen_req_out + Theta_cond) - (ThetaC_gen_hr_req_in - Theta_evad);

                            feer_corr[i] = (son1 / son2) / (mutter1 / mutter2);
                        }
                    }
                    else
                    {
                        if (CSource == "패시브쿨링DEC")
                        {
                            double son1 = 273 + Theta_IC[i] - Theta_evad;
                            double son2 = 273 + WetTemperature[i] + Theta_Around + Theta_cond - (273 + Theta_IC[i] - Theta_evad);

                            double mutter1 = ThetaC_gen_hr_req_in - Theta_evad;
                            double mutter2 = (ThetaC_gen_req_out + Theta_cond) - (ThetaC_gen_hr_req_in - Theta_evad);

                            feer_corr[i] = (son1 / son2) / (mutter1 / mutter2);
                        }
                        else
                        {
                            double son1 = 273 + Theta_IC[i] - Theta_evad;
                            double son2 = 273 + OutdoorTemperature[i] + Theta_Around - Theta_cond - (273 + Theta_IC[i] - Theta_evad);

                            double mutter1 = ThetaC_gen_hr_req_in - Theta_evad;
                            double mutter2 = (ThetaC_gen_req_out + Theta_cond) - (ThetaC_gen_hr_req_in - Theta_evad);

                            feer_corr[i] = (son1 / son2) / (mutter1 / mutter2);
                        }
                    }
                   
                }
                
               
                EER_c[i] = EER_f * feer_corr[i];
                SEER_c[i] = EER_c[i] * fC_PL_z[i] * fC_mult; //공조기를 추가해야함
                QC_ce[i] = QC_ce_z[i]; //공조기를 추가해야함
                QC_d[i] = QC_d_z[i]; //공조기를 추가해야함
                QC_s[i] = QC_s_z[i]; //공조기를 추가해야함
                QC_out[i] = QC_out_z[i]; //공조기를 추가해야함
                
                if (SEER_c[i] == 0)
                {
                    QC_f[i] = 0;
                
                }
                else QC_f[i] = QC_out[i] / SEER_c[i]; //공조기를 추가해야함
                if(double.IsNaN(QC_f[i]))
                {
                    QC_f[i] = 0;
                }
                else { }

            }

        }

        public void Cal_Save()
        {
            //설비정보와 보조설비정보는 따로따로
            QCa_nd = QC_a_ahu + QC_a_z;
            QCa_ce = 0;
            QCa_d = 0;
            QCa_s =0;
            QCa_out =0;
            QCa_f = 0;
            QCa_p = 0;

            for (int j =0; j<12; j++)
            {
                QC_ce[j] = QC_ce_z[j] + QC_ce_ahu[j];
                QC_d[j] = QC_d_z[j] + QC_d_ahu[j];
                QC_s[j] = QC_s_z[j] + QC_s_ahu[j]; ;
                QC_out[j] = QC_out_z[j] + QC_out_ahu[j];
                QCa_ce += QC_ce[j];
                QCa_d += QC_d[j];
                QCa_s += QC_s[j];
                QCa_out += QC_out[j];
            }
            

            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            for(int i=0; i<12 ; i++)
            {
                Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "프로젝트유형,프로젝트번호, 번호, 명칭, 냉방설비, 냉방출력, 냉방성능, 대기전력, 설치대수, Fuel,월",
                            "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + CoolingNum + "','" + CoolingName + "','" + CGType + "','" + Power_f + "','" + EER_f + "','" + Pctrl_f + "','" + Number_f + "','" + Carrier + "','" + mth[i] + "'", "번호,월");

                
                Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,QCb_a,QCa_ce,QCa_d,QCa_s,QCa_out,Sto_Tank,Sto_Type",
                          "'" + CoolingNum + "','" + mth[i] + "','" + QCa_nd + "','" + QCa_ce + "','" + QCa_d + "','" + QCa_s + "','" + QCa_out + "','" + Sto_Tank + "','" + Sto_Type + "'", "번호,월");

                
                Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,QC_f, SEER_c, EER_c,QC_out,QC_ce,QC_d,QC_s,QC_nd",
                           "'" + CoolingNum + "','" + mth[i] + "','" + QC_f[i] + "','" + SEER_c[i] + "','" + EER_c[i] + "','" + QC_out[i] + "','" + QC_ce[i] + "','" + QC_d[i] + "','" + QC_s[i] + "','" + QC_nd[i] +"'", "번호,월");


                if (ZoneNameList.Count > 0)
                {
                    Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,개수_z,QCb_a_z,QC_Max_z,공급설비1_z,공급설비2_z,A_z,열원설비",
                           "'" + CoolingNum + "','" + mth[i] + "','" + ZoneNumber_f + "','" + QC_a_z + "','" + Qc_max_z + "','" + ce1Type + "','" + ce2Type + "','" + A_z + "','" + CSource + "'", "번호,월");

                    Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,QC_out_z, QC_ce_z, QC_d_z, QC_s_z, QC_nd_z",
                               "'" + CoolingNum + "','" + mth[i] + "','" + QCa_nd + "','" + QC_out_z[i] + "','" + QC_ce_z[i] + "','" + QC_d_z[i] + "','" + QC_s_z[i] + "','" + QC_nd_z[i] + "'", "번호,월");

                }
                if (AHUNameList.Count > 0)
                {
                    Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,개수_ahu,QCb_a_ahu,QC_Max_ahu,공급설비1_ahu,공급설비2_ahu,A_ahu",
                           "'" + CoolingNum + "','" + mth[i] + "','" + QCa_nd + "','" + AhuNumber_f + "','" + QC_a_ahu + "','" + Qc_max_ahu + "','" + ce3Type + "','" + ce4Type + "','" + A_ahu + "'", "번호,월");

                   
                    Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,QC_out_ahu, QC_ce_ahu, QC_d_ahu, QC_s_ahu, QC_nd_ahu",
                              "'" + CoolingNum + "','" + mth[i] + "','" + QCa_nd + "','" + QC_out_ahu[i] + "','" + QC_ce_ahu[i] + "','" + QC_d_ahu[i] + "','" + QC_s_ahu[i] + "','" + QC_nd_ahu[i] + "'", "번호,월");

                }

                //반송설비 부분 입력

            }
           
        }
    }

    class zonemake //냉방존 만들기 
    {
        public string ZoneName, ZoneNum;
        public double Anf, tC_op, QC_max, Qb_a; //
        public double[] QC_nd_zt_j = new double[12], dwd = new double[12], θi_c = new double[12]; //getvalue로 값을 가져옴
        public double[] QC_rate = new double[12]; //존결정이 완료 후 계산됨
        public zonemake(string zonenum)
        {
            this.ZoneNum = zonenum;
            string[][] v1 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적,존이름,공조시간", "존번호= '" + zonenum + "'");
            string[][] v2 = Program.DB.getValue(DB.type.ProjDB, " Zone_HCneed_Result", " Q_max,  Qb_a, Qb_mth, dwd_mth, theta_i", "번호= '" + zonenum + "' AND 비이용일_이용일 = '이용일' And 난방_냉방 = '냉방'");
            Anf = Convert.ToDouble(v1[0][0]);
            ZoneName = v1[0][1];
            tC_op = Convert.ToDouble(v1[0][2]);
            QC_max = Convert.ToDouble(v2[7][0]);
            Qb_a = Convert.ToDouble(v2[7][1]);

            for (int i = 0; i < v2.Length; i++)
            {
                QC_nd_zt_j[i] = Convert.ToDouble(v2[i][2]);
                dwd[i] = Convert.ToDouble(v2[i][3]);
                θi_c[i] = Convert.ToDouble(v2[i][4]);
            }
        }
    }
    class ahumake
    {

    }
        
}


