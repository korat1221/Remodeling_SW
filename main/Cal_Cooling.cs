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
        string CoolingNum, CoolingName, InstallType, CGType, CSource, CompType, ArtType, ArtNumber, Cout; // A 및 숫자에 대한 지정값 Cout : 직팽식, 냉수
        string Control_f, Econo_f, Fuel_f;
        int Number_f, ZoneNumber_f, AhuNumber_f; //설비개수, 존개수


        //설비정보
        double Power_f, EER_f, Pctrl_f;
        List<double> Power = new List<double>(), EER = new List<double>(), Pctrl_el = new List<double>(); //공통
        List<int> Number = new List<int>(); //설치대수


        //기후데이터 작성
        public double[] OutdoorTemperature = new double[12], HumidityTemperature = new double[12];
        
        //냉방존
        List<zonemake> ZoneNameList = new List<zonemake>();
        public double[] QC_nd_z = new double[12], dwd_z = new double[12], theta_z = new double[12], betac_z = new double[12], tmth_wd_z = new double[12], BC_z = new double[12];
        public double[] tC_op_z = new double[12], PL_Rate_z = new double[12], fC_PL_z = new double[12];
        public double top_c_z, Qc_max_z, Qc_p_z, Beta_grenz_z, QC_a_z; //QC_p_z 공조기와 파워나누기
        string SCZoneType_z;

        //공조기
        List<ahumake> AHUNameList = new List<ahumake>();
        public double[] QC_nd_ahu = new double[12], dwd_ahu = new double[12], theta_ahu = new double[12], betac_ahu = new double[12], tmth_wd_ahu = new double[12], BC_ahu = new double[12];
        public double[] tC_op_ahu = new double[12], PL_Rate_ahu = new double[12], fC_PL_ahu = new double[12];
        public double top_c_ahu, Qc_max_ahu, Qc_p_ahu, Beta_grenz_ahu, QC_a_ahu; //QC_p_z 공조기와 파워나누기
        string SCZoneType_ahu;

        //공급/분배/저장열손실         
        double nc_ce_sens, nc_ce, nc_d, nc_s, fSP;
        string Sto_Tank, Sto_Type;
        public double[] QC_ce_z = new double[12], QC_d_z = new double[12], QC_s_z = new double[12], QC_out_z = new double[12];

        //냉방부분부하계산 및 최종 계산 결과값
        public double fC_mult, Theta_Around, ThetaC_gen_hr_req_in, ThetaC_gen_req_out, Theta_cond, Theta_evad;
        public double[] td_i = new double[12], feer_corr = new double[12], EER_c = new double[12], SEER_c = new double[12], Theta_IC = new double[12];
        public double[] QC_ce = new double[12], QC_d = new double[12], QC_s = new double[12], QC_out = new double[12], QC_f = new double[12];



        string Install, Fuel, Economizer; //공통
        double ColdWInput, ColdWOutput; //실외기 제외 모든유형 공통
        string Control; //흡수식 제외 모든항목
        List<string> Comp = new List<string>(); //공냉식, 수냉식, 지열히트펌프 유형
        string LoadSupply; //공냉식
        string Refriger; double PartLoad; //흡수식
        double CoolingWInput, CoolingWOutput; //지열히트펌프


        //존정보관련
        string SelectedZone, MultiZone;

        //펌프정의
        string SLRL, Complex, MainSystem, Sub1System, Sub2System, PumpUse, PumpMethod, Pump1, Pump2, Pump1Valve, Pump2Valve, Pump1Control, Pump2Control, ce1Type, ce2Type, ce3Type;
        int Pump1Num, Pump2Num;


        //공급설비정의
        string[] ceType = { "공조기", "실내기", "팬코일유닛", "복사냉방(천장)", "복사냉방(벽)", "바닥매립형컨백터" };
        ArrayList SelectAirConditioning = new ArrayList(); ArrayList SelectPump = new ArrayList(); ArrayList Selectce1Zone = new ArrayList(); ArrayList Selectce2Zone = new ArrayList();
        int ce_SelectRow;

       
        public Cal_Cooling(String _CoolingNum)
        {
            this.CoolingNum = _CoolingNum;
            generator();
            climate();
            zone();
            zonetotal();
            CalQC_ce();
            CalQC_d();
            CalQC_s();
            SCZone();
            Cal_CS();
        }
        #region
        public void report()
        {
           // string[][] value = Program.DB.setValue()
        }
        
        public void generator()
        {
            string[][] value = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_form",
                                        "명칭,설치,냉방설비,열원설비,냉방출력,냉방성능,설치대수,제어유형,연료,외기냉방시스템,대기전력,저장탱크,저장유형", "번호 = '" + CoolingNum + "'");
            CGType = value[0][2];
            switch (CGType)
            {
                case "실외기12kW":
                    CoolingName = value[0][0];
                    InstallType = value[0][1];
                    CGType = value[0][2];
                    CSource = value[0][3];
                    Power_f = Convert.ToDouble(value[0][4]);
                    EER_f = Convert.ToDouble(value[0][5]);
                    Number_f = Convert.ToInt32(value[0][6]);
                    Control_f = value[0][7];
                    Fuel_f = value[0][8];
                    Econo_f = value[0][9];
                    Pctrl_f = Convert.ToDouble(value[0][10]);
                    Sto_Tank = value[0][11];
                    Sto_Type = value[0][12];
                    Cout = "직팽식";
                    //계산함수
                    break;
                case "공냉식냉동기":
                    
                    
                    
                    
                    break;
                default:
                    break;
            }
            
        }


        public void climate()//기후데이터
        {
            try
            {
                string[][] 지역 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
                string[][] OutdoorClimate = Program.DB.getValue(DB.type.BaseDB_HCneed," 기후데이터_온도습도", "온도,상대습도","지역명 ='서울'"); //기후데이터 저장
                for(int i = 0; i < OutdoorTemperature.Length; i++) 
                {
                    OutdoorTemperature[i] = Convert.ToDouble(OutdoorClimate[i][0]);
                    HumidityTemperature[i] = humidityhemperature(Convert.ToDouble(OutdoorClimate[i][0]), Convert.ToDouble(OutdoorClimate[i][1]));
                }
            }
            catch { }

        }
        public double humidityhemperature(double _temperature, double _relativehumidity) //습구온도 계산
        {
            double _humiditytemperature = -5.809 + 0.058 * _relativehumidity * 100 + 0.697 * _temperature + 0.003 * _relativehumidity * _temperature * 100;
            return _humiditytemperature;
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
                    sum_dwd[i] += value.QC_nd_zt_j[i] * value.dwd[i];
                    sum_theta[i] += value.QC_nd_zt_j[i] * value.θi_c[i];
                }
                dwd_z[i] = sum_dwd[i] / QC_nd_z[i];
                theta_z[i] = sum_theta[i] / QC_nd_z[i];

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

        public void CalQC_ce()
        {
            //냉방존인 경우

            QC_a_z = 0;
            string[][] value = Program.DB.getValue(DB.type.BaseDB_Cooling, "공급분배손실계수", "nc_ce_sens,nc_ce", " 공급온도 = '직팽식' And 공급유형 = '냉방존'");
            nc_ce_sens = Convert.ToDouble(value[0][0]);
            nc_ce = Convert.ToDouble(value[0][1]);
            for(int i = 0; i < 12; i++)
            {
                QC_ce_z[i] = ((1 - nc_ce_sens) + (1 - nc_ce)) * QC_nd_z[i];
                QC_a_z += QC_nd_z[i];
            }

            //공조기인 경우
        }
        public void CalQC_d()
        {
            //냉방존인 경우
            string[][] value = Program.DB.getValue(DB.type.BaseDB_Cooling, "공급분배손실계수", "nc_d", " 공급온도 = '직팽식' And 공급유형 = '냉방존'");
            nc_d = Convert.ToDouble(value[0][0]);
            for (int i = 0; i < 12; i++)
            {
                QC_d_z[i] = (1 - nc_d) * QC_nd_z[i];
            }
           
            //공조기인 경우
        }
                                                
        public void CalQC_s()
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

        public void f_PL_k()
        {
            if( CGType == "실외기12kW")
            {
                string[][] value = Program.DB.getValue(DB.type.BaseDB_Cooling, "부분부하계수", "P1,P2,P3,P4,P5,P6,P7,P8,P9,P10,fC_M", " 설비유형= '" + CGType + "' And 제어유형 = '" + Control_f + "' And 공급유형 = '" + SCZoneType_z + "'");
                if(Number_f >= 2)
                {
                    fC_mult = Convert.ToDouble(value[0][10]);
                }else fC_mult = 1;
                
                double BetaRate = 0.05;
                
                for(int i = 0; i< 12 ; i++) //냉방존 검토
                {
                    for(int h = 0; h < 10; h++)
                    {
                        if (BC_z[i] < 0.05)
                        {
                            fC_PL_z[i] = 1;
                            break;

                        }else  if (BC_z[i] < BetaRate)
                        {
                            fC_PL_z[i] = Convert.ToDouble(value[0][h]);
                            break;
                        }
                        else
                        {
                            BetaRate = BetaRate + 0.1;
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
                        else if (BC_ahu[i] < BetaRate)
                        {
                            fC_PL_ahu[i] = Convert.ToDouble(value[0][h]);
                            break;
                        }
                        else
                        {
                            BetaRate = BetaRate + 0.1;
                        }
                    }

                }

            }
            else
            {
                string[][] value = Program.DB.getValue(DB.type.BaseDB_Cooling, "부분부하계수", "P1,P2,P3,P4,P5,P6,P7,P8,P9,P10", " 설비유형= '" + CGType + "' And 제어유형 = '" + Control_f + "' And 공급유형 = '" + CompType +"'");
            }
            
        }

        //냉방에너지소요량 계산
        #endregion
        public void Cal_CS()
        {
            string[][] v1 = Program.DB.getValue(DB.type.BaseDB_Cooling, "실외온도보정", "req_in, req_out, cond, evad", "냉방설비= '" + CGType + "' And 구분 = '" + Cout + "'");
            ThetaC_gen_hr_req_in = Convert.ToDouble(v1[0][0]);
            ThetaC_gen_req_out = Convert.ToDouble(v1[0][1]);
            Theta_cond = Convert.ToDouble(v1[0][2]);
            Theta_evad = Convert.ToDouble(v1[0][3]);

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
                    Theta_IC[i] = theta_ahu[i];
                }
                else
                {
                    Theta_IC[i] = QC_nd_z[i] * theta_z[i] + QC_nd_ahu[i] * theta_ahu[i] / (theta_z[i] * theta_ahu[i]);
                    feer_corr[i] = ((273 + Theta_IC[i] -Theta_cond) / ((273 + OutdoorTemperature[i] + Theta_Around + Theta_cond)-(273 + Theta_IC[i]-Theta_evad))) / 
                                   ((ThetaC_gen_hr_req_in - Theta_evad) / ((ThetaC_gen_req_out - Theta_cond)-(ThetaC_gen_hr_req_in-Theta_evad))); 
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

            }

        }

        public void Cal_Save()
        {
            for (int i = 0; i < 12; i++)
            {
               //설비,존정보
               //설비,공조기정보
               //연간결과벙보
               //열원설비정보
               //보조설비정보
               //월간정보
                Program.DB.setValue(DB.type.ProjDB, "Cooling_Result", "시스템번호, 시스템이름, 개수_z, QCb_a_z, QC_Max_z, 공급설비1_z, 공급설비2_z, 개수_ahu, QCb_a_ahu, QC_Max_ahu, 공급설비1_ahu, 공급설비2_ahu, 펌프1,펌프2,부하펌프제어,펌프3,펌프4,열원펌프제어, 월, QCb_z, QCCE_z, QCD_z, QCS_z, QCOUT_z, QCb_ahu, QCCE_ahu, QCD_ahu, QCS_ahu, QCOUT_ahu, QCB, QCCE, QCD, QCS, QCOUT, EER, SEER, QCF, AuxCE, AuxD, AuxS, AuxG",
               "'" + CoolingNum + "','" + CoolingName + "', '" + ZoneNumber_f + "','" + QC_a_z + "','" + Qc_max_z + "','" + QC_nd_z[i] + "','\" + QC_nd_z[i] + \"'", "시스템번호,월");
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


