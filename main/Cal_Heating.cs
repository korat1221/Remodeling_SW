using main.subcontents.EquipmentList;
using main.subcontents;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;

namespace main
{
    internal class Heating
    {
        public String HeatingNum;
        public string HeatingName; public String SelectZone_nonsplit; String SelectAHU_nonsplit;
        public String SystemLoacation, SLRL, Complex, MainSystem, Sub1System, Sub2System;
        public String SelectBoiler_nonsplit, BoilerNum_nonsplit;
        public String SelectABS_nonsplit, ABSNum_nonsplit;
        public String SelectDH_nonsplit;
        public String SelectSolar_nonsplit, SolarNum_nonsplit, SolarDirection_nonsplit, SolarDegree_nonsplit; public String SelectFC_nonsplit, FCNum_nonsplit, FCElecInstall_nonsplit, FCElecHeat_nonsplit;
        public String[] SelectHP_nonsplit = new String[3], HPNum_nonsplit = new String[3], HPSupply_nonsplit = new String[3], HPControl_nonsplit = new String[3]; //외기/지열/지하수 순 
        String PumpUse, PumpMethod, Pump1, Pump2, Pump1Valve, Pump2Valve, Pump1Control, Pump2Control; int Pump1Count, Pump2Count; double Pump1Volume, Pump2Volume,Pump1Head,Pump2Head;
        String GPumpMethod, GPump1, GPump2, GPump1Valve, GPump2Valve, GPump1Control, GPump2Control; int GPump1Count, GPump2Count; double GPump1Volume, GPump2Volume, GPump1Head, GPump2Head;
        public String ce1Type, ce2Type; int ce_SelectRow;
        public ArrayList ce_Type1 = new ArrayList(); public ArrayList ce_Type2 = new ArrayList(); public ArrayList Pump = new ArrayList();
        String StorageUse, StoragePumpUse, StoragePump; public double Vs;
        String[] SystemType = { "보일러", "히트펌프", "흡수식온수기", "지역난방", "태양열시스템,연료전지" };
        String[] ceType = { "실내기", "방열기", "팬코일유닛", "파워팬유닛", "복사난방" };
        double PipeD, PipeInsD, PipeIns_Ramda;
        String PipeIns;
        int ZoneCount;
        public ArrayList SelectZone_split = new ArrayList(); public ArrayList SelectAHU_split = new ArrayList(); public ArrayList SelectBoiler_split = new ArrayList(); public ArrayList BoilerNum_split = new ArrayList(); public ArrayList SelectABS_split = new ArrayList(); public ArrayList ABSNum_split = new ArrayList(); public ArrayList SelectDH_split = new ArrayList();
        public double[] Qhb_mth_sum = new double[12];  public double[] theta_ih_avg = new double[12]; public double[] theta_e = new double[12]; public double[] theta_u = new double[12];
        public double Qh_max_sum, Qh_a_sum, th_op_day_avg, theta_i_h_set_avg; public double[] th_avg = new double[12]; public double[] dop_mth_avg = new double[12];
        double theta_SL, theta_RL;
        double[] dmth = new double[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public double[] thrL = new double[12], thrL_day = new double[12], dhrB = new double[12], fLNA = new double[12], fLwe = new double[12];
        public double[] beta_h_ce = new double[12], beta_h_d = new double[12], beta_h_s = new double[12], beta_h_gen = new double[12];
        public double[] theta_av_ce = new double[12], theta_av_d = new double[12], theta_av_s = new double[12], theta_av_gen = new double[12];
        public double[] dtheta_ce = new double[12], dtheta_d = new double[12], dtheta_s = new double[12], dtheta_gen = new double[12];
        public double[] Qh_ce = new double[12], Qh_d = new double[12], Qh_s = new double[12], Qh_gen = new double[12], Qh_outg = new double[12], Qh_f = new double[12];
        public double[] Wh_ce = new double[12], Wh_d = new double[12], Wh_s = new double[12], Wh_g = new double[12];
        public double dtheta_ce1, dtheta_ce2, Psi_pipe, PipeL, Qs_po_day;
        public double[] Qh_gen_day = new double[12], Pgen_Pn = new double[12], Pgen_Pint = new double[12], Pgen_P0 = new double[12], eta_gen_Pn = new double[12], eta_gen_Pint = new double[12];
        public double[] fpint = new double[12];  public double[,] COPpint = new double[3, 12], Qh_outg_sng = new double[3, 12];
        public String Carrier; 
        public ArrayList SelectAirHP_split = new ArrayList(); ArrayList SelectGroundHP_split = new ArrayList(); ArrayList SelectGWHP_split = new ArrayList();
        public ArrayList AirHPSupply_split = new ArrayList(); ArrayList GroundHPSupply_split = new ArrayList(); ArrayList GWHPSupply_split = new ArrayList();
        public ArrayList AirHPControl_split = new ArrayList(); ArrayList GroundHPControl_split = new ArrayList(); ArrayList GWHPControl_split = new ArrayList();
        public ArrayList AirHPNum_split = new ArrayList(); ArrayList GroundHPNum_split = new ArrayList(); ArrayList GWHPNum_split = new ArrayList();
        public ArrayList SelectSolar_split = new ArrayList(); ArrayList SolarNum_split = new ArrayList(); ArrayList SolarDirection_split = new ArrayList(); ArrayList SolarDegree_split = new ArrayList();
        public ArrayList SelectFC_split = new ArrayList(); ArrayList FCNum_split = new ArrayList(); ArrayList FCElecInstall_split = new ArrayList(); ArrayList FCElecHeat_split = new ArrayList();
        public double[] Qhb_z = new double[12], Qh_ce_z = new double[12], Qh_d_z = new double[12], Qh_s_z = new double[12], Qh_outg_z = new double[12];
        public double[] Wh_ce_z = new double[12], Wh_d_z = new double[12], Wh_s_z = new double[12];
        public double[] Qhb_ahu = new double[12], Qh_ce_ahu = new double[12], Qh_d_ahu = new double[12], Qh_s_ahu = new double[12], Qh_outg_ahu = new double[12];
        public double[] Wh_ce_ahu = new double[12], Wh_d_ahu = new double[12], Wh_s_ahu = new double[12];
        public  double[] Eth_gen_out = new double[12];// 연료전지 열 생산량
        public double[] Eel_gen_out = new double[12];//연료전지 전기생산량
        public double[] Egen_in = new double[12];//연료전지 연료소비량 

        public double[] Qh_sol = new double[12];
        string[][] 프로젝트번호 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호");
        string[][] 지역, 외기온도;
        public Heating(String HeatingNum)
        {
            this.HeatingNum = HeatingNum;
            지역 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
            외기온도 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_온도습도", "기간,온도", "지역명 ='" + 지역[0][0] + "'");
            int i = -1;
            if (외기온도.Length > 0)
            {
                while (++i < 12)
                {
                    theta_e[i] = Convert.ToDouble(외기온도[i][1]);
                    theta_u[i] = theta_ih_avg[i] - 0.8 * (theta_ih_avg[i] - theta_e[i]);
                }
            }
        }

        public void Load_Zonedata(string ProjNum)
        {
            
            Boolean Now_Check = true ; 
            if(ProjNum == 프로젝트번호[0][0])
            {  Now_Check = true; }
            else
            { Now_Check = false;   }

            string[][] Value = Program.DB.getValue(ProjNum, "HeatingSystem_Form", "명칭,존", "번호 = '" + HeatingNum + "'");           
            if (Value.Length > 0)
            {
                HeatingName = Value[0][0];
                SelectZone_nonsplit = Value[0][1];
                SelectZone_split =  Split_(SelectZone_nonsplit);
            }
            string[][] Value_ce = null;
            if (Now_Check == true)
            {
                Value_ce = Program.DB.getValue(ProjNum, "Heating_ce_Form", "공급설비,존번호,부하율", "난방시스템 = '" + HeatingNum + "'and (Not 공급설비종류='CAV유닛' and Not 공급설비종류='VAV유닛') ");
            }
            else
            {
                Value_ce = Program.DB.getValue(DB.type.ProjDB, "Heating_ce_Form_Element", "공급설비,존번호,부하율", "난방시스템 = '" + HeatingNum + "'and (Not 공급설비종류='CAV유닛' and Not 공급설비종류='VAV유닛') ");
            }
            if (Value_ce.Length > 0)
            {
                double[,] Qhb_mth = new double[Value_ce.Length, 12];
                double[,] theta_ih = new double[Value_ce.Length, 12];
                double[,] th = new double[Value_ce.Length, 12];
                double[] Qh_a = new double[Value_ce.Length];
                double[,] dop_mth = new double[Value_ce.Length, 12];
                double[] th_op_day = new double[Value_ce.Length];
                double[] theta_i_h_set = new double[Value_ce.Length];
                double[] Qh_max = new double[SelectZone_split.Count];
                for (int n = 0; n < Value_ce.Length; n++)
                {
                    Zone zone = null; 
                    if (Now_Check == true)
                    {
                        zone = Program.CALC.getZone(Value_ce[n][1]);
                        for (int mth = 0; mth < 12; mth++)
                        {
                            Cal_Zone_data_(zone, Value_ce, n, Qhb_mth, theta_ih, th, dop_mth, Qh_a, th_op_day, theta_i_h_set, zone.Qb_mth[0, 1, mth], mth);
                        }
                    }
                    else
                    {
                        string[][] PostZone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,기존존", "");
                        if (PostZone.Length >0)
                        {
                            for (int j = 0; j < PostZone.Length; j++)
                            {
                                ArrayList split = Split_(PostZone[j][1]);
                                for (int m = 0; m < split.Count; m++)
                                {
                                    if (split[m].ToString() == Value_ce[n][1])
                                    {
                                        zone = Program.CALC.getZone(PostZone[j][0]);
                                        for (int mth = 0; mth < 12; mth++)
                                        {
                                            Cal_Zone_data_(zone, Value_ce, n, Qhb_mth, theta_ih, th,  dop_mth, Qh_a, th_op_day, theta_i_h_set, zone.Qb_mth[0, 1, mth],mth);
                                        }
                                    }
                                }                           
                            }
                        }                        
                    }                   
                }
                for (int k = 0; k < SelectZone_split.Count; k++)
                {
                    Zone zone = null;
                    if (Now_Check == true)
                    {
                        zone = Program.CALC.getZone(SelectZone_split[k].ToString());
                        Cal_Zone_Qmax_(zone, k, Qh_max);
                    }
                    else
                    {
                        string[][] PostZone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,기존존", "");
                        if (PostZone.Length > 0)
                        {
                            for (int j = 0; j < PostZone.Length; j++)
                            {
                                ArrayList split = Split_(PostZone[j][1]);
                                for (int m = 0; m < split.Count; m++)
                                {
                                    if (split[m].ToString() == SelectZone_split[k].ToString())
                                    {
                                        zone = Program.CALC.getZone(PostZone[j][0]);
                                        Cal_Zone_Qmax_(zone, k, Qh_max);
                                    }
                                }                           
                            }
                        }
                    }                    
                }
                for (int n = 0; n < Value_ce.Length; n++)
                {
                    Qh_a_sum += Qh_a[n];
                    //요구량 가중
                    th_op_day_avg += (th_op_day[n] * Qh_a[n]);
                    theta_i_h_set_avg += (theta_i_h_set[n] * Qh_a[n]);
                }
                th_op_day_avg = th_op_day_avg / Qh_a_sum;
                theta_i_h_set_avg = theta_i_h_set_avg / Qh_a_sum;

                for (int mth = 0; mth < 12; mth++)
                {
                    for (int n = 0; n < Value_ce.Length; n++)
                    {
                        Qhb_z[mth] += Qhb_mth[n, mth];
                        Qhb_mth_sum[mth] += Qhb_mth[n, mth];
                        //요구량 가중
                        theta_ih_avg[mth] += (theta_ih[n, mth] * Qh_a[n]);
                        th_avg[mth] += (th[n, mth] * Qh_a[n]);
                        dop_mth_avg[mth] += (dop_mth[n, mth] * Qh_a[n]);
                    }
                    theta_ih_avg[mth] = theta_ih_avg[mth] / Qh_a_sum;
                    th_avg[mth] = th_avg[mth] / Qh_a_sum;
                    dop_mth_avg[mth] = dop_mth_avg[mth] / Qh_a_sum;
                }
            }
        }



        public void Load_AHUdata(string ProjNum)
        {

            Boolean Now_Check = true;
            if (ProjNum == 프로젝트번호[0][0])
            { Now_Check = true; }
            else
            { Now_Check = false; }

            string[][] Value = Program.DB.getValue(ProjNum, "HeatingSystem_Form", "명칭,공조기", "번호 = '" + HeatingNum + "'");
            if (Value.Length > 0)
            {
                HeatingName = Value[0][0];
                SelectAHU_nonsplit = Value[0][1];
                SelectAHU_split = Split_(SelectAHU_nonsplit);
            }
            string[][] Value_ce = null;
            if (Now_Check == true)
            {
                Value_ce = Program.DB.querySQL(ProjNum, "Select a.공급설비,a.존번호,a.부하율,b.선택열회수기 From Heating_ce_Form as a Inner Join ZoneGeneral_Form as b  on a.존번호=b.존번호 Where a.난방시스템 = '" + HeatingNum + "' and (a.공급설비종류='CAV유닛' or a.공급설비종류='VAV유닛') ");
            }
            else
            {
                Value_ce = Program.DB.querySQL(DB.type.ProjDB, "Select a.공급설비,a.존번호,a.부하율,b.선택열회수기 From Heating_ce_Form as a Inner Join ZoneGeneral_Form as b  on a.존번호=b.존번호 Where a.난방시스템 = '" + HeatingNum + "' and (a.공급설비종류='CAV유닛' or a.공급설비종류='VAV유닛') ");
            }
            if (Value_ce.Length > 0)
            {
                double[,] Qhb_mth = new double[Value_ce.Length, 12];
                double[,] theta_ih = new double[Value_ce.Length, 12];
                double[,] th = new double[Value_ce.Length, 12];
                double[] Qh_a = new double[Value_ce.Length];
                double[,] dop_mth = new double[Value_ce.Length, 12];
                double[] th_op_day = new double[Value_ce.Length];
                double[] theta_i_h_set = new double[Value_ce.Length];
                double[] Qh_max = new double[SelectZone_split.Count];
                for (int n = 0; n < Value_ce.Length; n++)
                {
                    Zone zone = null;
                    AHU ahu = null; 
                    if (Now_Check == true)
                    {
                        zone = Program.CALC.getZone(Value_ce[n][1]);
                        ahu = Program.CALC.getAHU(Value_ce[n][3]);
                        double percent = Cal_AHUneed_percent(ahu, zone);
                        for (int mth = 0; mth < 12; mth++)
                        {
                            Cal_Zone_data_(zone,ahu, Value_ce, n, Qhb_mth, theta_ih, th, dop_mth, Qh_a, th_op_day, theta_i_h_set, percent * ahu.Qv_b[0,mth], mth);
                        }
                    }
                    else
                    {
                        string[][] PostZone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,기존존,선택열회수기", "");
                        if (PostZone.Length > 0)
                        {
                            for (int j = 0; j < PostZone.Length; j++)
                            {
                                ArrayList split = Split_(PostZone[j][1]);
                                for (int m = 0; m < split.Count; m++)
                                {
                                    if (split[m].ToString() == Value_ce[n][1])
                                    {
                                        zone = Program.CALC.getZone(PostZone[j][0]);
                                        ahu = Program.CALC.getAHU(PostZone[j][2]);
                                        double percent = Cal_AHUneed_percent(ahu, zone);
                                        for (int mth = 0; mth < 12; mth++)
                                        {
                                            Cal_Zone_data_(zone, ahu, Value_ce, n, Qhb_mth, theta_ih, th, dop_mth, Qh_a, th_op_day, theta_i_h_set, percent * ahu.Qv_b[0, mth], mth);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                for (int k = 0; k < SelectZone_split.Count; k++)
                {
                    Zone zone = null;
                    if (Now_Check == true)
                    {
                        zone = Program.CALC.getZone(SelectZone_split[k].ToString());
                        string[][] value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "선택열회수기", "존번호='" + zone.zoneName+"'");
                        if (value.Length > 0  && value[0][0]!="")
                        {
                            AHU ahu = Program.CALC.getAHU(value[0][0]);
                            Cal_Zone_Qmax_(ahu, k, Qh_max);
                        }
                    }
                    else
                    {
                        string[][] PostZone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,기존존", "");
                        if (PostZone.Length > 0)
                        {
                            for (int j = 0; j < PostZone.Length; j++)
                            {
                                ArrayList split = Split_(PostZone[j][1]);
                                for (int m = 0; m < split.Count; m++)
                                {
                                    if (split[m].ToString() == SelectZone_split[k].ToString())
                                    {
                                        zone = Program.CALC.getZone(PostZone[j][0]);
                                        string[][] value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "선택열회수기", "존번호='" + zone.zoneName + "'");
                                        if (value.Length > 0 && value[0][0] != "")
                                        {
                                            AHU ahu = Program.CALC.getAHU(value[0][0]);
                                            Cal_Zone_Qmax_(ahu, k, Qh_max);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                for (int n = 0; n < Value_ce.Length; n++)
                {
                    Qh_a_sum += Qh_a[n];
                    //요구량 가중
                    th_op_day_avg += (th_op_day[n] * Qh_a[n]);
                    theta_i_h_set_avg += (theta_i_h_set[n] * Qh_a[n]);
                }
                th_op_day_avg = th_op_day_avg / Qh_a_sum;
                theta_i_h_set_avg = theta_i_h_set_avg / Qh_a_sum;

                for (int mth = 0; mth < 12; mth++)
                {
                    for (int n = 0; n < Value_ce.Length; n++)
                    {
                        Qhb_ahu[mth] += Qhb_mth[n, mth];
                        Qhb_mth_sum[mth] += Qhb_mth[n, mth];
                        //요구량 가중
                        theta_ih_avg[mth] += (theta_ih[n, mth] * Qh_a[n]);
                        th_avg[mth] += (th[n, mth] * Qh_a[n]);
                        dop_mth_avg[mth] += (dop_mth[n, mth] * Qh_a[n]);
                    }
                    theta_ih_avg[mth] = theta_ih_avg[mth] / Qh_a_sum;
                    th_avg[mth] = th_avg[mth] / Qh_a_sum;
                    dop_mth_avg[mth] = dop_mth_avg[mth] / Qh_a_sum;
                }
            }
        }

        private double Cal_AHUneed_percent(AHU ahu, Zone zone)
        {
            double percent = 0;
            double sum = 0;
           for(int a =0; a< ahu.SelectZone_split.Count; a++)
            {
                Zone zone2 = Program.CALC.getZone(ahu.SelectZone_split[a].ToString());
                sum += zone2.Qb_a[0];
            }
            percent = zone.Qb_a[0] / sum; 
           return percent;
        }

        private void Cal_Zone_data_(Zone zone, string[][] Value_ce, int n, double[,] Qhb_mth, double[,] theta_ih, double[,] th, double[,] dop_mth, double[] Qh_a, double[] th_op_day, double[] theta_i_h_set, double Qhb_mth_, int mth)
        {
            if (zone != null)
            {
                Qhb_mth[n, mth] += Qhb_mth_ * Convert.ToDouble(Value_ce[n][2]);
                theta_ih[n, mth] = zone.theta_i[0, 1, mth]; //이용일 난방
                th[n, mth] = zone.t_max[0, mth]; // 난방 시간                             
                dop_mth[n, mth] = zone.dwd_mth[mth];
                Qh_a[n] += zone.Qb_a[0] * Convert.ToDouble(Value_ce[n][2]); //연간 난방요구량
                th_op_day[n] = zone.th_op_d;
                theta_i_h_set[n] = zone.theta_i_h_set;
            }
        }
        private void Cal_Zone_data_(Zone zone, AHU ahu, string[][] Value_ce, int n, double[,] Qhb_mth, double[,] theta_ih, double[,] th, double[,] dop_mth, double[] Qh_a, double[] th_op_day, double[] theta_i_h_set, double Qhb_mth_, int mth)
        {
            if (zone != null)
            {
                Qhb_mth[n, mth] += Qhb_mth_ * Convert.ToDouble(Value_ce[n][2]);
                theta_ih[n, mth] = zone.theta_i[0, 1, mth]; //이용일 난방
                th[n, mth] = zone.t_max[0, mth]; // 난방 시간                             
                dop_mth[n, mth] = zone.dwd_mth[mth];
                Qh_a[n] += ahu.Qh_a_tot * Convert.ToDouble(Value_ce[n][2]); //연간 난방요구량
                th_op_day[n] = zone.th_op_d;
                theta_i_h_set[n] = zone.theta_i_h_set;
            }
        }
        private void Cal_Zone_Qmax_(Zone zone, int k, double[] Qh_max)
        {            
            if (zone != null)
            {
                Qh_max[k] = zone.Q_max[0];//최대부하 
                Qh_max_sum += Qh_max[k];
            }
        }
        private void Cal_Zone_Qmax_(AHU ahu, int k, double[] Qh_max)
        {
            if (ahu!= null)
            {
                Qh_max[k] = ahu.Qmax_tot[0];//최대부하 
                Qh_max_sum += Qh_max[k];
            }
        }

        //난방설비 일반정보 불러오기 
        public void Load_HeatingGeneral(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "HeatingSystem_Form", "설치위치,공급환수온도,복합설비유무,주요설비,보조설비1,보조설비2", "번호 = '" + HeatingNum + "'");
            if (Value.Length > 0)
            {
                SystemLoacation = Value[0][0];
                SLRL = Value[0][1];
                if (SLRL != null && SLRL != "")
                {
                    string[][] Value2 = Program.DB.getValue(DB.type.BaseDB_Heating, "공급환수온도", "공급온도,환수온도", "공급환수온도 = '" + SLRL + "'");
                    if (Value2.Length > 0)
                    {
                        theta_SL = Convert.ToDouble(Value2[0][0]);
                        theta_RL = Convert.ToDouble(Value2[0][1]);
                    }
                }

                Complex = Value[0][2];
                MainSystem = Value[0][3];
                Sub1System = Value[0][4];
                Sub2System = Value[0][5];
            }
        }

        //보일러 정보 불러오기
        public void Load_Boiler_general(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "HeatingSystem_Form", "보일러종류,보일러대수", "번호 = '" + HeatingNum + "'");
            if (Value.Length > 0)
            {
                SelectBoiler_nonsplit = Value[0][0];
                SelectBoiler_split = Split_(SelectBoiler_nonsplit);

                BoilerNum_nonsplit = Value[0][1];
                BoilerNum_split = Split_(BoilerNum_nonsplit);
            }
        }
        public void Load_Solar_general(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "HeatingSystem_Form", "태양열번호,모듈개수,모듈방위,모듈기울기", "번호 = '" + HeatingNum + "'");
            if (Value.Length > 0)
            {
                SelectSolar_nonsplit = Value[0][0];
                SelectSolar_split = Split_(SelectSolar_nonsplit);

                SolarNum_nonsplit = Value[0][1];
                SolarNum_split =Split_(SolarNum_nonsplit);

                SolarDirection_nonsplit = Value[0][2];
                SolarDirection_split = Split_(SolarDirection_nonsplit);

                SolarDegree_nonsplit = Value[0][3];
                SolarDegree_split = Split_(SolarDegree_nonsplit);
            }
        }
        public void Load_FC_general(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "HeatingSystem_Form", "연료전지번호,연료전지대수,연료전지설치유형,연료전지생산유형", "번호 = '" + HeatingNum + "'");
            if (Value.Length > 0)
            {
                SelectFC_nonsplit = Value[0][0];
                SelectFC_split = Split_(SelectFC_nonsplit);

                FCNum_nonsplit = Value[0][1];
                FCNum_split = Split_(FCNum_nonsplit);

                FCElecInstall_nonsplit = Value[0][2];
                FCElecInstall_split = Split_(FCElecInstall_nonsplit);

                FCElecHeat_nonsplit = Value[0][3];
                FCElecHeat_split = Split_(FCElecHeat_nonsplit);
            }
        }

        public void Load_PumpData(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "HeatingSystem_Form", "펌프유무,펌프방식,펌프1종류,펌프2종류,펌프1밸브,펌프2밸브,펌프1제어,펌프2제어,펌프1대수,펌프2대수,펌프1유량,펌프2유량,펌프1양정,펌프2양정", "번호 = '" + HeatingNum + "'");
            if (Value.Length > 0)
            {
                PumpUse = Value[0][0];
                if (PumpUse == "펌프 있음")
                {
                    ArrayList arr = new ArrayList();
                    arr = Split_(Value[0][1]);
                    PumpMethod = arr[0].ToString();
                    if (arr.Count > 1)
                    {
                        GPumpMethod = arr[1].ToString();
                    }
                    ///
                    arr = Split_(Value[0][2]);
                    if (arr.Count > 0)
                    {
                        Pump1 = arr[0].ToString();
                    }
                    if (arr.Count > 1)
                    {
                        GPump1 = arr[1].ToString();
                    }
                    ///
                    arr = Split_(Value[0][3]);
                    if (arr.Count > 0)
                    {
                        Pump2 = arr[0].ToString();
                    }
                    if (arr.Count > 1)
                    {
                        GPump2 = arr[1].ToString();
                    }
                    ///
                    arr = Split_(Value[0][4]);
                    if (arr.Count > 0)
                    {
                        Pump1Valve = arr[0].ToString();
                    }
                    if (arr.Count > 1)
                    {
                        GPump1Valve = arr[1].ToString();
                    }
                    ///
                    arr = Split_(Value[0][5]);
                    if (arr.Count > 0)
                    {
                        Pump2Valve = arr[0].ToString();
                    }
                    if (arr.Count > 1)
                    {
                        GPump2Valve = arr[1].ToString();
                    }
                    ///
                    arr = Split_(Value[0][6]);
                    if (arr.Count > 0)
                    {
                        Pump1Control = arr[0].ToString();
                    }
                    if (arr.Count > 1)
                    {
                        GPump1Control = arr[1].ToString();
                    }
                    ///
                    arr = Split_(Value[0][7]);
                    if (arr.Count > 0)
                    {
                        Pump2Control = arr[0].ToString();
                    }
                    if (arr.Count > 1)
                    {
                        GPump2Control = arr[1].ToString();
                    }
                    ///
                    arr = Split_(Value[0][8]);
                    if (arr.Count > 0)
                    {
                        Pump1Count = Convert.ToInt16(arr[0].ToString());
                    }
                    if (arr.Count > 1)
                    {
                        GPump1Count = Convert.ToInt16(arr[1].ToString());
                    }
                    ///
                    arr = Split_(Value[0][9]);
                    if (arr.Count > 0)
                    {
                        Pump2Count = Convert.ToInt16(arr[0].ToString());
                    }
                    if (arr.Count > 1)
                    {
                        GPump2Count = Convert.ToInt16(arr[1].ToString());
                    }
                    ///
                    arr = Split_(Value[0][10]);
                    if (arr.Count > 0)
                    {
                        Pump1Volume = Convert.ToDouble(arr[0].ToString());
                    }
                    if (arr.Count > 1)
                    {
                        GPump1Volume = Convert.ToDouble(arr[1].ToString());
                    }
                    ///
                    arr = Split_(Value[0][11]);
                    if (arr.Count > 0)
                    {
                        Pump2Volume = Convert.ToDouble(arr[0].ToString());
                    }
                    if (arr.Count > 1)
                    {
                        GPump2Volume = Convert.ToDouble(arr[1].ToString());
                    }
                    ///
                    arr = Split_(Value[0][12]);
                    if (arr.Count > 0)
                    {
                        Pump1Head = Convert.ToDouble(arr[0].ToString());
                    }
                    if (arr.Count > 1)
                    {
                        GPump1Head = Convert.ToDouble(arr[1].ToString());
                    }
                    ///
                    arr = Split_(Value[0][13]);
                    if (arr.Count > 0)
                    {
                        Pump2Head = Convert.ToDouble(arr[0].ToString());
                    }
                    if (arr.Count > 1)
                    {
                        GPump2Head = Convert.ToDouble(arr[1].ToString());
                    }
                    ///
                }
            }
        }

        public void Load_ceData(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "HeatingSystem_Form", "공급설비1종류,공급설비2종류", "번호 = '" + HeatingNum + "'");
            if (Value.Length > 0)
            {
                ce1Type = Value[0][0];
                ce2Type = Value[0][1];
            }
        }

        public void Load_StorageData(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "HeatingSystem_Form", "축열유무,축열펌프유무,축열펌프,축열용량", "번호 = '" + HeatingNum + "'");
            if (Value.Length > 0)
            {
                StorageUse = Value[0][0];
                StoragePumpUse = Value[0][1];
                StoragePump = Value[0][2];
                if (Value[0][3] != null && Value[0][3] != "")
                {
                    Vs = Convert.ToDouble(Value[0][3]);
                }
            }
        }

        public void Load_PipeData(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "HeatingSystem_Form", "배관관경,배관보온두께,보온열전도율,배관보온재,노출배관길이", "번호 = '" + HeatingNum + "'");
            if (Value.Length > 0)
            {
                PipeD = Convert.ToDouble(Value[0][0]);
                PipeInsD = Convert.ToDouble(Value[0][1]);
                PipeIns_Ramda = Convert.ToDouble(Value[0][2]);
                PipeIns = Value[0][3];
                if (Value[0][4] == "" || Value[0][4] == null) { PipeL = 0; }
                else { PipeL = Convert.ToDouble(Value[0][4]); }
            }
        }
        //외기 히트펌프 정보 불러오기 
        public void Load_AirHP_general(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "HeatingSystem_Form", "외기히트펌프번호,외기히트펌프공급방식,외기히트펌프제어방식,외기히트펌프대수", "번호 = '" + HeatingNum + "'");
            if (Value.Length > 0)
            {
                String HeatSource = "외기";
                SelectHP_nonsplit[0] = Value[0][0];
                SelectAirHP_split = Split_(SelectHP_nonsplit[0]);

                HPSupply_nonsplit[0] = Value[0][1];
                AirHPSupply_split = Split_(HPSupply_nonsplit[0]);

                HPControl_nonsplit[0] = Value[0][2];
                AirHPControl_split = Split_(HPControl_nonsplit[0]);

                HPNum_nonsplit[0] = Value[0][3];
                AirHPNum_split = Split_(HPNum_nonsplit[0]);
            }
        }

        //지열 히트펌프 정보 불러오기 
        public void Load_GroundHP_general(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "HeatingSystem_Form", "지열히트펌프번호,지열히트펌프공급방식,지열히트펌프제어방식,지열히트펌프대수", "번호 = '" + HeatingNum + "'");
            if (Value.Length > 0)
            {
                String HeatSource = "지열";
                SelectHP_nonsplit[1] = Value[0][0];
                SelectGroundHP_split = Split_(SelectHP_nonsplit[1]);

                HPSupply_nonsplit[1] = Value[0][1];
                GroundHPSupply_split = Split_(HPSupply_nonsplit[1]);

                HPControl_nonsplit[1] = Value[0][2];
                GroundHPControl_split = Split_(HPControl_nonsplit[1]);

                HPNum_nonsplit[1] = Value[0][3];
                GroundHPNum_split  = Split_(HPNum_nonsplit[1]);
            }
        }

        //지하수 히트펌프 정보 불러오기 
        public void Load_GWHP_general(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "HeatingSystem_Form", "지하수히트펌프번호,지하수히트펌프공급방식,지하수히트펌프제어방식,지하수히트펌프대수", "번호 = '" + HeatingNum + "'");
            if (Value.Length > 0)
            {
                String HeatSource = "지하수";
                SelectHP_nonsplit[2] = Value[0][0];
                SelectGWHP_split = Split_(SelectHP_nonsplit[2]);

                HPSupply_nonsplit[2] = Value[0][1];
                GWHPSupply_split = Split_(HPSupply_nonsplit[2]);

                HPControl_nonsplit[2] = Value[0][2];
                GWHPControl_split = Split_(HPControl_nonsplit[2]);

                HPNum_nonsplit[2] = Value[0][3];
                GWHPNum_split = Split_(HPNum_nonsplit[2]);
            }
        }

        //흡수식온수기 정보 불러오기
        public void Load_ABS_general(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "HeatingSystem_Form", "흡수식온수기번호,흡수식온수기대수", "번호 = '" + HeatingNum + "'");
            if (Value.Length > 0)
            {
                SelectABS_nonsplit = Value[0][0];
                SelectABS_split = Split_(SelectABS_nonsplit);

                ABSNum_nonsplit = Value[0][1];
                ABSNum_split = Split_(ABSNum_nonsplit);
            }
        }

        //지역난방 정보 불러오기
        public void Load_DH_general(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "HeatingSystem_Form", "지역난방번호", "번호 = '" + HeatingNum + "'");
            if (Value.Length > 0)
            {
                SelectDH_nonsplit = Value[0][0];
                SelectDH_split = Split_(SelectDH_nonsplit);
            }
        }
        private ArrayList Split_(String nonSplit)
        {
            ArrayList split = new ArrayList();
            if (nonSplit != null && nonSplit !="")
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
                split.Clear() ;
            }
            return split;
        }

        public void Calc_thrL()
        {
            double dop_a = 0;
            for (int mth = 0; mth < 12; mth++)
            {
                dop_a += dop_mth_avg[mth];
            }

            for (int mth = 0; mth < 12; mth++)
            {
                fLNA[mth] = 1;
                fLwe[mth] = 1;
                thrL_day[mth] = 24 - fLNA[mth] * (24 - th_op_day_avg);
                dhrB[mth] = dmth[mth] * (365 - fLwe[mth] * (365 - dop_a)) / 365 * th_avg[mth] / (dmth[mth] * 24);
                if (double.IsNaN(dhrB[mth]))
                {
                    dhrB[mth] = 0;
                }
                thrL[mth] = thrL_day[mth] * dhrB[mth];
            }
        }

        public void Calc_beta_ce()
        {
            double[] theta_SL_beta = new double[12], theta_RL_beta = new double[12];

            for (int mth = 0; mth < 12; mth++)
            {
                beta_h_ce[mth] = Qhb_mth_sum[mth] / (Qh_max_sum / 1000 * th_avg[mth]);
                if (double.IsNaN(beta_h_ce[mth]))
                {
                    beta_h_ce[mth] = 0;
                }
                theta_SL_beta[mth] = (theta_SL - theta_i_h_set_avg) * Math.Pow(beta_h_ce[mth], 1 / 1.3) + theta_i_h_set_avg;
                theta_RL_beta[mth] = (theta_RL - theta_i_h_set_avg) * Math.Pow(beta_h_ce[mth], 1 / 1.3) + theta_i_h_set_avg;

                dtheta_ce[mth] = theta_SL_beta[mth] - theta_RL_beta[mth];
                theta_av_ce[mth] = 0.5 * (theta_SL_beta[mth] + theta_RL_beta[mth]);
                if (double.IsNaN(dtheta_ce[mth]))
                {
                    dtheta_ce[mth] = 0;
                }
                if (double.IsNaN(theta_av_ce[mth]))
                {
                    theta_av_ce[mth] = 0;
                }
            }
        }

        public double Calc_theta_ce(String ceType, String SLRL, String 설치위치, String 제어방식)
        {
            double dtheta_str1 = 0.0, dtheta_str2 = 0.0, dtheta_ctr = 0.0, dtheta_im_ctr = 0.0, dtheta_roomaut = 0.0, dtheta_hydr = 0.4, theta_dash_str = 0.0;
            double dtheta_emb1 = 0.0, dtheta_emb2 = 0.0, dtheta_im_emt = 0.0, dtheta_rad = 0.0;
            double theta_ce =0;
            if (ceType == "방열기" || ceType == "실내기")
            {

                dtheta_emb1 = 0; dtheta_emb2 = 0; dtheta_im_emt = -0.3; dtheta_rad = 0; dtheta_im_ctr = 0;

                string[][] Value_str1 = Program.DB.getValue(DB.type.BaseDB_Heating, "공급설비온도", "값", "설비유형 = '" + ceType + "' AND 구분 ='" + SLRL + "' And 온도변수 = 'dtheta_str1'");
                if (Value_str1.Length > 0)
                { dtheta_str1 = Convert.ToDouble(Value_str1[0][0]); }

                string[][] Value_str2 = Program.DB.getValue(DB.type.BaseDB_Heating, "공급설비온도", "값", "설비유형 = '" + ceType + "' AND 구분 ='" + 설치위치 + "'And 온도변수 = 'dtheta_str2'");
                if (Value_str2.Length > 0)
                { dtheta_str2 = Convert.ToDouble(Value_str2[0][0]); }
            }
            else if (ceType == "팬코일유닛")
            {

                dtheta_emb1 = 0; dtheta_emb2 = 0; dtheta_im_emt = -0.3; dtheta_rad = 0; dtheta_im_ctr = 0;
                dtheta_str1 = 0.0;

                string[][] Value_str2 = Program.DB.getValue(DB.type.BaseDB_Heating, "공급설비온도", "값", "설비유형 = '" + ceType + "' AND 구분 ='" + 설치위치 + "'And 온도변수 = 'dtheta_str2'");
                if (Value_str2.Length > 0)
                { dtheta_str2 = Convert.ToDouble(Value_str2[0][0]); }
            }
            else if (ceType == "복사난방")
            {
                dtheta_im_emt = -0.2; dtheta_rad = 0; dtheta_im_ctr = 0;

                string[][] Value_str1 = Program.DB.getValue(DB.type.BaseDB_Heating, "공급설비온도", "값", "설비유형 = '" + ceType + "' AND 구분 ='" + 설치위치 + "'And 온도변수 = 'dtheta_str1'");
                if (Value_str1.Length > 0)
                { dtheta_str1 = Convert.ToDouble(Value_str1[0][0]); }
                dtheta_str2 = 0.0;
                string[][] Value_emb1 = Program.DB.getValue(DB.type.BaseDB_Heating, "공급설비온도", "값", "설비유형 = '" + ceType + "' AND 구분 ='" + 설치위치 + "'And 온도변수 = 'dtheta_emb1'");
                if (Value_emb1.Length > 0)
                { dtheta_emb1 = Convert.ToDouble(Value_emb1[0][0]); }
                dtheta_emb2 = 0.0;
            }
            else
            {
                dtheta_emb1 = 0; dtheta_emb2 = 0; dtheta_im_emt = -0.3; dtheta_rad = 0; dtheta_im_ctr = 0;

                string[][] Value = Program.DB.getValue(DB.type.BaseDB_Heating, "공급설비온도", "값", "설비유형 = '파워팬유닛' AND 구분 ='" + 제어방식 + "'And 온도변수 = 'theta_dash_str'");
                if (Value.Length > 0)
                { theta_dash_str = 10 * Convert.ToDouble(Value[0][0]) / (16 * (0.5 * 4 - 1.1)); }

            }

            string[][] Value_ctr = Program.DB.getValue(DB.type.BaseDB_Heating, "공급설비온도", "값", "구분 ='" + 제어방식 + "'And 온도변수 = 'dtheta_ctr'");
            if (Value_ctr.Length > 0)
            { dtheta_ctr = Convert.ToDouble(Value_ctr[0][0]); }

            string[][] Value_roomaut = Program.DB.getValue(DB.type.BaseDB_Heating, "공급설비온도", "값", "구분 ='" + 제어방식 + "'And 온도변수 = 'dtheta_roomaut'");
            if (Value_roomaut.Length > 0)
            { dtheta_roomaut = Convert.ToDouble(Value_roomaut[0][0]); }


            if (ceType == "방열기" || ceType == "실내기" || ceType == "팬코일유닛")
            {
                theta_ce = (dtheta_str1 + dtheta_str2) / 2 + (dtheta_ctr + dtheta_im_ctr + dtheta_roomaut + dtheta_hydr + theta_dash_str + dtheta_emb1 + dtheta_emb2 + dtheta_im_emt + dtheta_rad);
            }
            else if (ceType == "복사난방")
            {
                theta_ce = (dtheta_emb1 + dtheta_emb2) / 2 + (dtheta_str1 + dtheta_str2 + dtheta_ctr + dtheta_im_ctr + dtheta_roomaut + dtheta_hydr + theta_dash_str + dtheta_im_emt + dtheta_rad);
            }
            else if(ceType =="CAV유닛"|| ceType=="VAV유닛" || ceType == "바닥매립형컨백터")
            {
                string[][] value = Program.DB.getValue(DB.type.BaseDB_Heating, "공급설비온도", "값", "구분 ='" + 제어방식 + "'And 온도변수 = 'dtheta' and 설비유형='"+ceType+"'");
                if(value.Length >0)
                {
                    theta_ce = Convert.ToDouble(value[0][0]);
                }            
            }
            else
            {
                theta_ce = (theta_dash_str + dtheta_emb1 + dtheta_emb2 + dtheta_ctr + dtheta_im_ctr + dtheta_roomaut + dtheta_hydr + theta_dash_str + dtheta_im_emt + dtheta_rad);
            }

            return theta_ce;
        }

        public void Load_ce(string ProjNum)
        {
            Boolean Now_Check = true;
            if (ProjNum == 프로젝트번호[0][0])
            { Now_Check = true; }
            else
            { Now_Check = false; }

            string[][] Value;
            if (Now_Check == true)
            { Value = Program.DB.getValue(ProjNum, "Heating_ce_Form", "존번호,공급설비,설치위치,부하율", "난방시스템 = '" + HeatingNum + "' And 공급설비종류 = '" + ce1Type + "'"); }
            else
            {
                Value = Program.DB.getValue(DB.type.ProjDB, "Heating_ce_Form_Element", "존번호,공급설비,설치위치,부하율", "난방시스템 = '" + HeatingNum + "' And 공급설비종류 = '" + ce1Type + "'");
            }
            ce_Type1.Clear();
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    String Num, ce_ZoneNum, ceSystemNum, ceType, Location, Control;
                    double theta;
                    Num = Value[n][1];
                    ce_ZoneNum = Value[n][0];
                    ceSystemNum = Value[n][1].Substring(0, Value[n][1].IndexOf("_"));
                    ceType = ce1Type;
                    Location = Value[n][2];
                    double Zone_Percent = Convert.ToDouble(Value[n][3]);
                    string[][] 일람표정보 = Program.DB.getValue(ProjNum, "User_ce", "온도제어방식", "번호 = '" + ceSystemNum + "'");
                    Control = 일람표정보[0][0];
                    theta = Calc_theta_ce(ceType, SLRL, Location, Control);
                    dtheta_ce1 = theta;
                    CE ce = new CE(Num, ce_ZoneNum, ceSystemNum, ceType, Location, Control, theta,Zone_Percent);
                    ce_Type1.Add(ce);
                }
            }
            if (Now_Check == true)
            {
                Value = Program.DB.getValue(ProjNum, "Heating_ce_Form", "존번호,공급설비,설치위치,부하율", "난방시스템 = '" + HeatingNum + "' And 공급설비종류 = '" + ce2Type + "'");
            }
            else
            {
                Value = Program.DB.getValue(DB.type.ProjDB, "Heating_ce_Form_Element", "존번호,공급설비,설치위치,부하율", "난방시스템 = '" + HeatingNum + "' And 공급설비종류 = '" + ce2Type + "'");
            }
            ce_Type2.Clear();
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    String Num, ce_ZoneNum, ceSystemNum, ceType, Location, Control;
                    double theta;
                    Num = Value[n][1];
                    ce_ZoneNum = Value[n][0];
                    ceSystemNum = Value[n][1].Substring(0, Value[n][1].IndexOf("_"));
                    ceType = ce2Type;
                    Location = Value[n][2];
                    double Zone_Percent = Convert.ToDouble(Value[n][3]);
                    string[][] 일람표정보 = Program.DB.getValue(ProjNum, "User_ce", "온도제어방식", "번호 = '" + ceSystemNum + "'");
                    Control = 일람표정보[0][0];
                    theta = Calc_theta_ce(ceType, SLRL, Location, Control);
                    dtheta_ce2 = theta;
                    CE ce = new CE(Num, ce_ZoneNum, ceSystemNum, ceType, Location, Control, theta, Zone_Percent);
                    ce_Type2.Add(ce);
                }
            }

        }
        public void Calc_Qce(string ProjNum)
        {
            Boolean Now_Check = true;
            if (ProjNum == 프로젝트번호[0][0])
            {
                Now_Check = true;
            }
            else
            {
                Now_Check = false;
            }
           
            for (int k = 0; k < ce_Type1.Count; k++)
            {
                CE ce = (CE)ce_Type1[k];
                Zone zone = null;
                if (Now_Check == true)
                {
                    zone = Program.CALC.getZone(ce.ZoneNum());
                    Cal_Qce_1(zone, ce, ProjNum);
                }
                else
                {
                    string[][] PostZone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,기존존", "");
                    if (PostZone.Length > 0)
                    {
                        for (int j = 0; j < PostZone.Length; j++)
                        {
                            ArrayList split = Split_(PostZone[j][1]);
                            for (int m = 0; m < split.Count; m++)
                            {
                                if (split[m].ToString() == ce.ZoneNum())
                                {
                                    zone = Program.CALC.getZone(PostZone[j][0]);
                                    Cal_Qce_1(zone, ce, ProjNum);
                                }
                            }
                        
                        }
                    }
                }
            }

            for (int k = 0; k < ce_Type2.Count; k++)
            {
                CE ce = (CE)ce_Type2[k];
                Zone zone = null;
                if (Now_Check == true)
                {
                    zone = Program.CALC.getZone(ce.ZoneNum());
                    Cal_Qce_1(zone,  ce, ProjNum);
                }
                else
                {
                    string[][] PostZone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,기존존", "");
                    if (PostZone.Length > 0)
                    {
                        for (int j = 0; j < PostZone.Length; j++)
                        {
                            ArrayList split = Split_(PostZone[j][1]);
                            for (int m = 0; m < split.Count; m++)
                            {
                                if (split[m].ToString() == ce.ZoneNum())
                                {
                                    zone = Program.CALC.getZone(PostZone[j][0]);
                                    Cal_Qce_1(zone, ce, ProjNum);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Cal_Qce_1(Zone zone,CE ce, string ProjNum)
        {
            if (zone != null)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    if ((zone.theta_i[0, 1, mth] - theta_e[mth]) > 1)
                    { Qh_ce[mth] += Math.Max(zone.Qb_mth[0, 1, mth] * ce.Zone_Percent() * ce.theta_ce() / (zone.theta_i[0, 1, mth] - theta_e[mth]), 0); }
                    if (double.IsNaN(Qh_ce[mth]))
                    {
                        Qh_ce[mth] = 0;
                    }
                    string[][] Value2 = Program.DB.getValue(ProjNum, "User_ce", "소비전력_난방", "번호 = '" + ce.ceNum() + "'");
                    if (Value2.Length > 0 && Value2[0][0] != "")
                    {
                        for (int n = 0; n < SelectAirHP_split.Count; n++)
                        {
                            string[][] airHP = Program.DB.getValue(ProjNum, "User_AirHP", "난방정격소비전력", "번호 = '" + SelectAirHP_split[n] + "'");
                            if(airHP.Length > 0 && airHP[0][0]!="")
                            {
                                if (Convert.ToDouble(Value2[0][0]).ToString("0") == Convert.ToDouble(airHP[0][0]).ToString("0") || Convert.ToDouble(Value2[0][0]) > 0.5)
                                { goto goto_; }
                            }
                            
                        }

                        if (Value2[0][0] != "")
                        {
                           Wh_ce[mth] += Convert.ToDouble(Value2[0][0]) * th_op_day_avg * dop_mth_avg[mth]; 
                        }
                        goto_: int a = 0; a = a;
                    }
                }
            }
        }
      
        public void Calc_beta_d()
        {
            double[] theta_SL_beta = new double[12], theta_RL_beta = new double[12];

            for (int mth = 0; mth < 12; mth++)
            {
                beta_h_d[mth] = (Qhb_mth_sum[mth] + Qh_ce[mth]) / (Qh_max_sum / 1000 * th_avg[mth]);
                if (double.IsNaN(beta_h_d[mth])) { beta_h_d[mth] = 0; }

                theta_SL_beta[mth] = (theta_SL - theta_i_h_set_avg) * Math.Pow(beta_h_d[mth], 1 / 1.3) + theta_i_h_set_avg;
                theta_RL_beta[mth] = (theta_RL - theta_i_h_set_avg) * Math.Pow(beta_h_d[mth], 1 / 1.3) + theta_i_h_set_avg;

                dtheta_d[mth] = theta_SL_beta[mth] - theta_RL_beta[mth];
                theta_av_d[mth] = 0.5 * (theta_SL_beta[mth] + theta_RL_beta[mth]);
                if (double.IsNaN(dtheta_d[mth])) { dtheta_d[mth] = 0; }
                if (double.IsNaN(theta_av_d[mth])) { theta_av_d[mth] = 0; }

            }
        }
        public void Calc_Qd(string ProjNum)
        {
            double R_pipe, R_se, Ramda_se, L1 = 0, L2 = 0;
            //배관 열저항
            {
                R_pipe = Math.Log(((PipeD / 2 + PipeInsD) / 1000) / (PipeD / 2 / 1000)) / 2 / Math.PI / PipeIns_Ramda;
                Ramda_se = 5 + 0.15 * 5.67 / 100000000 * 4 * 1000;
                R_se = 1 / (Ramda_se * 2 * Math.PI * (PipeD / 2 + PipeInsD) / 1000);
                

                double[] theta_i = new double[12];               

                for (int mth = 0; mth < 12; mth++)
                {
                    if (SystemLoacation == "단열외피 외부")
                    {
                        theta_i[mth] = theta_u[mth];
                    }
                    else if (SystemLoacation == "외기")
                    {
                        theta_i[mth] = theta_e[mth];
                    }
                    else
                    {
                        theta_i[mth] = theta_ih_avg[mth];
                    }

                    Qh_d[mth] = Math.Max(Psi_pipe * PipeL * (theta_av_d[mth] - theta_i[mth]) * thrL[mth] / 1000, 0);
                    if (double.IsNaN(Qh_d[mth])) { Qh_d[mth] = 0; }
                }
            }
            //펌프
            {
                
                Pump.Clear();
                string[][] Value = Program.DB.getValue(ProjNum, "User_Pump", "동력", "번호 = '" + Pump1 + "'");
                if (Value.Length > 0)
                { Cal_Pump(Pump1, Pump1Valve, Pump1Control, Pump1Count, Pump1Volume, Pump1Head, Convert.ToDouble(Value[0][0])); }
                Value = Program.DB.getValue(ProjNum, "User_Pump", "동력", "번호 = '" + Pump2 + "'");
                if (Value.Length > 0)
                { Cal_Pump(Pump2, Pump2Valve, Pump2Control, Pump2Count, Pump2Volume, Pump2Head, Convert.ToDouble(Value[0][0])); }
                Value = Program.DB.getValue(ProjNum, "User_Pump", "동력", "번호 = '" + GPump1 + "'");
                if (Value.Length > 0)
                { Cal_Pump(GPump1, GPump1Valve, GPump1Control, GPump1Count, GPump1Volume, GPump1Head, Convert.ToDouble(Value[0][0])); }
                Value = Program.DB.getValue(ProjNum, "User_Pump", "동력", "번호 = '" + GPump2 + "'");
                if (Value.Length > 0)
                { Cal_Pump(GPump2, GPump2Valve, GPump2Control, GPump2Count, GPump2Volume, GPump2Head, Convert.ToDouble(Value[0][0])); }
            }
        }
        private void Cal_Pump( string Pump, string PumpValve, string PumpControl, int PumpCount, double PumpVolume, double PumpHead, double PumpPower)
        {
            String Num_pump; 
            double Cp1, Cp2, Ppump, fhydr = 1, dPz, f_dpm;
            double[] Vz = new double[12], P_hydr = new double[12], fe = new double[12], e_hydr = new double[12], Wh_hydr = new double[12];
            double theta;
            Num_pump = Pump;
            Pump pump1 = new Pump(Num_pump, PumpVolume, PumpPower, PumpHead, PumpCount, PumpValve, PumpControl); ;
            this.Pump.Add(pump1);
            string[][] Value_Control = Program.DB.getValue(DB.type.BaseDB_Heating, "펌프제어", "Cp1,Cp2", "펌프제어 = '" + PumpControl + "'");
            Cp1 = Convert.ToDouble(Value_Control[0][0]);
            Cp2 = Convert.ToDouble(Value_Control[0][1]);
            if (PumpValve == "있음")
            {
                fhydr = 1;
            }
            else
            {
                fhydr = 1.25;
            }
            if (Pump == null || Pump == "")
            {
                f_dpm = 1;
            }
            else
            {
                f_dpm = 0.45;
            }
            dPz = PumpHead * 1000 * 9.81;
            for (int mth = 0; mth < 12; mth++)
            {
                Vz[mth] = Qh_max_sum / 1000 * 3.6 / (dtheta_d[mth] * 4.18);
                P_hydr[mth] = dPz * Vz[mth] / 3600;
                fe[mth] = (PumpPower * PumpCount / P_hydr[mth]);
                e_hydr[mth] = fe[mth] * (Cp1 + Cp2 / beta_h_d[mth]) * 0.25 / 0.25;
                Wh_hydr[mth] = P_hydr[mth] / 1000 * beta_h_d[mth] * th_avg[mth] * f_dpm * 1;
                Wh_d[mth] = Wh_hydr[mth] * e_hydr[mth];
            }
        }
        public void Calc_beta_s()
        {
            double[] theta_SL_beta = new double[12], theta_RL_beta = new double[12];

            for (int mth = 0; mth < 12; mth++)
            {
                beta_h_s[mth] = (Qhb_mth_sum[mth] + Qh_ce[mth] + Qh_d[mth]) / (Qh_max_sum / 1000 * th_avg[mth]);
                if (double.IsNaN(beta_h_s[mth])) { beta_h_s[mth] = 0; }

                theta_SL_beta[mth] = (theta_SL - theta_i_h_set_avg) * Math.Pow(beta_h_s[mth], 1 / 1.3) + theta_i_h_set_avg;
                theta_RL_beta[mth] = (theta_RL - theta_i_h_set_avg) * Math.Pow(beta_h_s[mth], 1 / 1.3) + theta_i_h_set_avg;

                dtheta_s[mth] = theta_SL_beta[mth] - theta_RL_beta[mth];
                theta_av_s[mth] = 0.5 * (theta_SL_beta[mth] + theta_RL_beta[mth]);
                if (double.IsNaN(dtheta_s[mth])) { dtheta_s[mth] = 0; }
                if (double.IsNaN(theta_av_s[mth])) { theta_av_s[mth] = 0; }
            }
        }
        public void Calc_Qh_s(string ProjNum)
        {

            double[] thetai = new double[12];
            if (Vs > 0)
            {
                Qs_po_day = 0.4 + 0.14 * Math.Pow(Vs, 0.5);
            }
            for (int mth = 0; mth < 12; mth++)
            {
                if (SystemLoacation == "단열외피 외부")
                {
                    thetai[mth] = theta_ih_avg[mth] - 0.8 * (theta_ih_avg[mth] - theta_e[mth]);
                }
                else if (SystemLoacation == "외기")
                {
                    thetai[mth] = theta_e[mth];
                }
                else
                {
                    thetai[mth] = theta_ih_avg[mth];
                }
                Qh_s[mth] = (theta_av_s[mth] - thetai[mth]) / 45 * dop_mth_avg[mth] * Qs_po_day;
                if (double.IsNaN(Qh_s[mth])) { Qh_s[mth] = 0; }
            }

            string[][] Value = Program.DB.getValue(ProjNum, "User_Pump", "동력", "번호 = '" + StoragePump + "'");
            if (Value.Length > 0)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    double tPu = beta_h_s[mth] * 24 * dhrB[mth];
                    Wh_s[mth] = Convert.ToDouble(Value[0][0]) * tPu / 1000;
                    if (double.IsNaN(Wh_s[mth])) { Wh_s[mth] = 0; }
                }
            }
        }
        public void Calc_beta_gen()
        {
            double[] theta_SL_beta = new double[12], theta_RL_beta = new double[12];

            for (int mth = 0; mth < 12; mth++)
            {
                beta_h_gen[mth] = (Qhb_mth_sum[mth] + Qh_ce[mth] + Qh_d[mth] + Qh_s[mth]) / (Qh_max_sum / 1000 * th_avg[mth]);
                if (double.IsNaN(beta_h_gen[mth])) { beta_h_gen[mth] = 0; }
                theta_SL_beta[mth] = (theta_SL - theta_i_h_set_avg) * Math.Pow(beta_h_s[mth], 1 / 1.3) + theta_i_h_set_avg;
                theta_RL_beta[mth] = (theta_RL - theta_i_h_set_avg) * Math.Pow(beta_h_s[mth], 1 / 1.3) + theta_i_h_set_avg;

                dtheta_gen[mth] = theta_SL_beta[mth] - theta_RL_beta[mth];
                theta_av_gen[mth] = 0.5 * (theta_SL_beta[mth] + theta_RL_beta[mth]);
                Qh_outg[mth] = Qhb_mth_sum[mth] + Qh_ce[mth] + Qh_d[mth] + Qh_s[mth];
                if (double.IsNaN(dtheta_gen[mth])) { dtheta_gen[mth] = 0; }
                if (double.IsNaN(theta_av_gen[mth])) { theta_av_gen[mth] = 0; }
                if (double.IsNaN(Qh_outg[mth])) { Qh_outg[mth] = 0; }
            }
        }
        public void LoadCalc_FC(string ProjNum)
        {
            for (int n = 0; n < SelectFC_split.Count; n++)
            {
                string[][] Value = Program.DB.getValue(ProjNum, "User_FC", "번호, 명칭, 연료, 전기출력, 전기효율, 열출력, 열효율", "번호 = '" + SelectFC_split[n].ToString() + "'");
                if(Value.Length > 0 )
                {
                    int FC_nea = Convert.ToInt16(FCNum_split[n]);
                    double power_el = Convert.ToDouble(Value[0][3]);
                    double eta_el = Convert.ToDouble(Value[0][4])/100;
                    double power_th = Convert.ToDouble(Value[0][5]);
                    double eta_th = Convert.ToDouble(Value[0][6])/100;
                    double eta_tot = eta_el + eta_th;

                    double Pfc_th = power_th * FC_nea;
                    double Pfc_el = power_el * FC_nea;
                    Calc_FC(ProjNum, SelectFC_split[n].ToString(), Pfc_th, Pfc_el, eta_th, eta_el, eta_tot, FCElecInstall_split[n].ToString(), FCElecHeat_split[n].ToString(), FC_nea);
                }
            }
        }

        private void Calc_FC(string ProjNum, string FCNum, double Pfc_th, double Pfc_el, double eta_th, double eta_el, double eta_tot, string FCElecInstall,string FCElecHeat, int FC_nea)
        {
            double top = 0;
            double Pth_min = 0, Pls_sb = 0, Pth_sb = 0, Pel_out_sb = 0, Paux_sb = 0, Ppilot = 0;
            double[] Qw_outg = new double[12]; string DHWNum = "";
            double[] QCHW_gen_out = new double[12];
            double[] dop = new double[12], Pth_gen_out = new double[12]; 
            double[] Eth_gen_out_h = new double[12],Eth_gen_out_w = new double[12];
            double[] Pel_gen_out = new double[12]; 
            double Pgen_ls_sb = 0, Pgen_in_chp = 0, Pgen_ls_chp = 0;
            double[] pgen_ls = new double[12], Qgen_ls = new double[12];
            double[] Pgen_in = new double[12];
            //string[][] DValue = Program.DB.querySQL(ProjNum, "Select b.번호 From DHWSystem_Result as a Inner Join DHWSystem_Form as b on a.번호=b.번호 Where a.연료전지번호='" + FCNum + "' and 월='" + mth + "월'");
            //if(DValue.Length >0)
            //{
            //    DHNum = DValue[0][0];
            //}
            for (int mth = 0; mth < 12; mth++)
            {
                //DValue = Program.DB.querySQL(ProjNum, "Select b.Qw_outg,b.번호 From DHWSystem_Result as a Inner Join DHWSystem_Form as b on a.번호=b.번호 Where a.연료전지번호='" + FCNum + "' and 월='" + mth + "월'");
                //if(DValue.Length >0)
                //{
                //    Qw_outg[mth]= Convert.ToDouble(DValue[0][0]);
                //}
                QCHW_gen_out[mth] = Qh_outg[mth] + Qw_outg[mth];

                top = th_op_day_avg;
                dop[mth] = dop_mth_avg[mth];
                if (FCElecInstall == "단독형" && FCElecHeat == "전기와 열")
                {
                    top = th_op_day_avg;
                    dop[mth] = dop_mth_avg[mth];
                }
                else
                {
                    top = 24;
                    dop = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
                }
                Pth_gen_out[mth] = Math.Min(Pfc_th, QCHW_gen_out[mth] / (top * dop[mth]));
                Eth_gen_out[mth] = Pth_gen_out[mth] * top * dop[mth];
                Eth_gen_out_h[mth] = Eth_gen_out[mth] * Qh_outg[mth] / QCHW_gen_out[mth]; 
            }
            for(int mth=0; mth < 12; mth ++)
            {
                Pel_gen_out[mth] = Pel_out_sb + (Pfc_el - Pel_out_sb) * ((Pth_gen_out[mth] - Pth_sb) / (Pfc_th / FC_nea - Pth_sb));
                Eel_gen_out[mth] = Pel_gen_out[mth] * top * dop[mth];
                Pgen_ls_sb = Pls_sb + Ppilot;
                Pgen_in_chp = Pfc_th / FC_nea / eta_th;
                Pgen_ls_chp = (1 - eta_th - eta_el) * Pgen_in_chp;
                pgen_ls[mth] = Pgen_ls_sb + (Pgen_ls_chp - Pgen_ls_sb) * ((Pth_gen_out[mth] - Pth_sb) / (Pfc_th / FC_nea - Pth_sb));
                Qgen_ls[mth] = pgen_ls[mth] * top * dop[mth];
                Pgen_in[mth] = Pth_gen_out[mth] + Pel_gen_out[mth] + pgen_ls[mth];
                Egen_in[mth] = Pgen_in[mth] * top * dop[mth];

                if(FCElecHeat =="전기와 열")
                {
                    Qh_outg[mth] = Qh_outg[mth] - Eth_gen_out[mth];
                }
            }
            Boolean Now_Check = true;
            if (ProjNum == 프로젝트번호[0][0])
            { Now_Check = true; }
            else
            { Now_Check = false; }
            if (Now_Check) {
              Save_FC(DHWNum,FCNum, Eth_gen_out_h, Eth_gen_out_w); 
            }
        }
        private void Save_FC(string DHWNum, string FCNum, double[] Eth_gen_out_h, double[] Eth_gen_out_w)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            string RESystemNum = "";
            string[][] value = Program.DB.getValue(DB.type.ProjDB,"RESystem_Result", "번호", "난방설비='" + HeatingNum + "' and 신재생시스템='"+FCNum+"'");
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
                 "난방설비,급탕설비,신재생시스템,신재생시스템유형,생산소비,생산유형,총에너지,난방,급탕",
                 "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + RESystemNum + "','" + MTH + "','" +
                HeatingNum + "','" + DHWNum + "','" + FCNum + "','연료전지','생산','열','" +
                Eth_gen_out[mth] + "','" + Eth_gen_out_h[mth] +"','" + Eth_gen_out_w[mth]
                  + "'", "번호,월,생산소비,생산유형"); ;
            }
            for (int mth = 0; mth <= 11; mth++)
            {
                string MTH = (mth + 1).ToString() + "월";
                Program.DB.setValue(DB.type.ProjDB, "RESystem_Result", "프로젝트번호,프로젝트유형,번호," +
                 "월," +
                 "난방설비,급탕설비,신재생시스템,신재생시스템유형,생산소비,생산유형,총에너지",
                 "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + RESystemNum + "','" + MTH + "','" +
                HeatingNum + "','" + DHWNum + "','" + FCNum + "','연료전지','생산','전기','" +
                Eel_gen_out[mth]
                  + "'", "번호,월,생산소비,생산유형"); ;
            }
            for (int mth = 0; mth <= 11; mth++)
            {
                string MTH = (mth + 1).ToString() + "월";
                Program.DB.setValue(DB.type.ProjDB, "RESystem_Result", "프로젝트번호,프로젝트유형,번호," +
                 "월," +
                 "난방설비,급탕설비,신재생시스템,신재생시스템유형,생산소비,소비연료,총에너지",
                 "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + RESystemNum + "','" + MTH + "','" +
                HeatingNum + "','" + DHWNum + "','" + FCNum + "','연료전지','소비','가스','" +
                Egen_in[mth]
                  + "'", "번호,월,생산소비,소비연료"); ;
            }
            Program.DB.saveProject();
        }
        public void LoadCalc_Boiler(string ProjNum)
        {
            for (int n = 0; n < SelectBoiler_split.Count; n++)
            {
                string[][] Value = Program.DB.getValue(ProjNum, "User_Boiler", "번호,난방급탕,연료,Type,용량,전부하효율,부분부하효율,소비전력,대기전력", "번호 = '" + SelectBoiler_split[n] + "'");
                if (Value.Length > 0)
                {
                    String Num = Value[0][0];
                    String Combi = Value[0][1];
                    Carrier = Value[0][2];
                    String Type = Value[0][3];
                    double Power = Convert.ToDouble(Value[0][4]) * Convert.ToDouble(BoilerNum_split[n]);
                    string[][] 기존신규 = Program.DB.getValue(ProjNum, "HeatingSystem_Form", "프로젝트유형", "번호 = '" + HeatingNum + "'");
                    double eta_Pn = Convert.ToDouble(Value[0][5]) / 100;
                    double eta_Pint = Convert.ToDouble(Value[0][6]) / 100;
                    if (기존신규[0][0] == "1")
                    {
                        eta_Pn = eta_Pn * 0.95;
                        eta_Pint = eta_Pint * 0.95;
                    }

                    double W = Convert.ToDouble(Value[0][7]);
                    double W_0 = Convert.ToDouble(Value[0][8]);
                    double count = Convert.ToDouble(BoilerNum_split[n]);
                    Calc_Qh_gen_Boiler(Num, Combi, Type, Power, eta_Pn, eta_Pint, W, W_0, count);
                }
            }
        }
        public void Calc_Qh_gen_Boiler(String Num, String Combi, String Type, double Power, double eta_Pn, double eta_Pint, double W, double W_0, double count)
        {
            string[][] Value = Program.DB.getValue(DB.type.BaseDB_Heating, "보일러", "온도보정계수K,온도보정계수L,대기상태열손실E,대기상태열손실F,보조설비G_Pn,보조설비H_Pn,보조설비n_Pn,보조설비G_Pint,보조설비H_Pint,보조설비n_Pint", "종류 = '" + Type + "'");
            double K = Convert.ToDouble(Value[0][0]);
            double L = Convert.ToDouble(Value[0][1]);
            double E = Convert.ToDouble(Value[0][2]);
            double F = Convert.ToDouble(Value[0][3]);
            double G_Pn = Convert.ToDouble(Value[0][4]);
            double H_Pn = Convert.ToDouble(Value[0][5]);
            double n_Pn = Convert.ToDouble(Value[0][6]);
            double G_Pint = Convert.ToDouble(Value[0][7]);
            double H_Pint = Convert.ToDouble(Value[0][8]);
            double n_Pint = Convert.ToDouble(Value[0][9]);
            Boiler boiler = new Boiler(Num, Combi, Carrier, Type, Power, eta_Pn, eta_Pint, W, W_0, count, K, L, E, F, G_Pn, H_Pn, n_Pn, G_Pint, H_Pint, n_Pint);
            double theta_pn, theta_pint, theta_con;
            theta_pn = (80 + 60) / 2;
            theta_pint = (80 + 30) / 2;
            theta_con = (50 + 30) / 2;
            Value = Program.DB.getValue(DB.type.BaseDB_Heating, "연소난방비", "연소난방비", "연료 = '" + Carrier + "'");
            double fHN_HI = Convert.ToDouble(Value[0][0]);
            double qP0_70 = E * Math.Pow(Power, F) / 100;
            double tw_Pn_day = 1; //나중에 급탕과 연결 해야 함 
            double[] Pd_in = new double[12];
            double beta_gen_pint = 0.3, qp0_theta;
            double[] Qh_gen_mth = new double[12];

            for (int mth = 0; mth < 12; mth++)
            {
                Pd_in[mth] = Qh_outg[mth] / th_avg[mth];

                eta_gen_Pn[mth] = eta_Pn + K * (theta_pn - theta_av_gen[mth]);
                Pgen_Pn[mth] = Math.Max((fHN_HI - eta_gen_Pn[mth]) / eta_gen_Pn[mth] * Pd_in[mth], 0);

                eta_gen_Pint[mth] = eta_Pint + L * (theta_pint - theta_av_gen[mth]);
                Pgen_Pint[mth] = Math.Max((fHN_HI - eta_gen_Pint[mth]) / eta_gen_Pint[mth] * beta_gen_pint * Pd_in[mth], 0);

                qp0_theta = Math.Max(qP0_70 * (theta_av_gen[mth] - theta_ih_avg[mth]) / 50, 0);
                Pgen_P0[mth] = qp0_theta * Pd_in[mth] / eta_Pn * fHN_HI;
                if (beta_h_gen[mth] <= beta_gen_pint)
                {
                    Qh_gen_day[mth] = (beta_h_gen[mth] / beta_gen_pint * (Pgen_Pint[mth] - Pgen_P0[mth]) + Pgen_P0[mth]) * (thrL_day[mth] - tw_Pn_day);
                }
                else
                {
                    Qh_gen_day[mth] = ((beta_h_gen[mth] - beta_gen_pint) / (1 - beta_gen_pint) * (Pgen_Pn[mth] - Pgen_Pint[mth]) + Pgen_Pint[mth]) * (thrL_day[mth] - tw_Pn_day);
                }

                Qh_gen_mth[mth] = Qh_gen_day[mth] * dhrB[mth];
                Qh_gen[mth] += Qh_gen_mth[mth];
                Qh_f[mth] = Qh_outg[mth] + Qh_gen[mth];
            }

            double Paux_Pn, Paux_Pint;
            Paux_Pn = Math.Min((G_Pn + H_Pn * Math.Pow(Power, n_Pn)) / 1000, W / 1000);
            Paux_Pint = Math.Min((G_Pint + H_Pint * Math.Pow(Power, n_Pint)) / 1000, W / 1000);
            double[] Ph_gen_aux = new double[12], Wh_g_i = new double[12];
            for (int mth = 0; mth < 12; mth++)
            {
                if (Qhb_mth_sum[mth] > 0)
                {
                    Ph_gen_aux[mth] = (beta_h_gen[mth] / beta_gen_pint * (Paux_Pint - W_0 / 1000) + W_0 / 1000);
                    Ph_gen_aux[mth] = ((beta_h_gen[mth] - beta_gen_pint) / (1 - beta_gen_pint) * (Paux_Pn - Paux_Pint) + Paux_Pint);
                    Wh_g_i[mth] = Ph_gen_aux[mth] * (thrL[mth] - tw_Pn_day * dop_mth_avg[mth]) + W_0 / 1000 * Math.Max(0,(24 * dmth[mth] - thrL[mth]));
                    Wh_g[mth] += Wh_g_i[mth];
                }
            }
        }
        public void LoadCalc_Solar(string ProjNum)
        {
            double qsol_HN_d, dtheta_korr;
            double[] qsol_HN_mth = new double[12], eta = new double[12], qsol_mth = new double[12], Qsol_mth = new double[12], Wh_gen = new double[12];
            string[][] Solarvalue;
            double Ac;

            for (int k = 0; k < SelectSolar_split.Count; k++)
            {
                Solarvalue = Program.DB.getValue(ProjNum, "User_Solar", "번호,모듈면적,효율,열손실계수1차,열손실계수2차,입사각50도,유효열용량", "번호 ='" + SelectSolar_split[k] + "'");
                if (Solarvalue.Length > 0)
                {
                    Solar solar = new Solar(Solarvalue[0][0], Convert.ToDouble(Solarvalue[0][1]), Convert.ToDouble(Solarvalue[0][2]), Convert.ToDouble(Solarvalue[0][3]), Convert.ToDouble(Solarvalue[0][4]), Convert.ToDouble(Solarvalue[0][5]), Convert.ToDouble(Solarvalue[0][6]), Convert.ToDouble(SolarNum_split[k]), SolarDirection_split[k].ToString(), SolarDegree_split[k].ToString());
                    Calc_Solar(solar, SolarDirection_split[k].ToString(), SolarDegree_split[k].ToString());
                }
            }
        }

        public void Calc_Solar(Solar solar,string direction, string degree)
        {
            

            double qsol_HN_d, dtheta_korr;
            double[] qsol_HN_mth = new double[12], eta = new double[12], qsol_mth = new double[12], Qsol_mth = new double[12],  Wh_gen = new double[12];
            string[][] Solarvalue;
            double Ac;
            double[] Qw_outg = new double[12]; string DHNum = "";
            string[][] DValue = Program.DB.querySQL(DB.type.ProjDB, "Select b.번호 From DHWSystem_Result as a Inner Join DHWSystem_Form as b on a.번호=b.번호 Where a.태양열번호='" + solar.Num + "'");
            if(DValue.Length >0)
            {
                DHNum = DValue[0][0];
            }
            for (int mth = 0; mth < 12; mth++)
            {
                 DValue = Program.DB.querySQL(DB.type.ProjDB, "Select b.Qw_outg,b.번호 From DHWSystem_Result as a Inner Join DHWSystem_Form as b on a.번호=b.번호 Where a.태양열번호='" + solar.Num + "' and 월='" + mth + "월'");
                if (DValue.Length > 0)
                {
                    Qw_outg[mth] = Convert.ToDouble(DValue[0][0]);
                }

                string[][] value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_전일사량", "일사량", "지역명 ='" + 지역[0][0] + "'and 방향='" + direction+ "' and 각도 ='" +degree + "' and 기간 ='" + (mth + 1) + "월'");
                qsol_HN_d = Convert.ToDouble(value[0][0]);
                qsol_HN_mth[mth] = qsol_HN_d * dmth[mth] * 24 / 1000;

                string[][] value2 = Program.DB.querySQL(DB.type.BaseDB_HCneed, "Select Max(일사량) from 기후데이터_전일사량 where 지역명 = '" + 지역[0][0] + "'and 방향 = '" + direction + "' and 각도 = '" + degree + "'");

                Ac = Qh_max_sum * 2 * 1.03 * 1.03 / Convert.ToDouble(value2[0][0]) / 24 * 1000;
                if (solar.M_Area() * solar.M_Count() / Ac < 1)
                {
                    dtheta_korr = Math.Min(-20 + 20 * solar.M_Area() * solar.M_Count() / Ac, 0);
                }
                else
                {
                    dtheta_korr = Math.Min(-14 + 14 * solar.M_Area() * solar.M_Count() / Ac, 0);
                }
                value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_태양열", "온도차", "지역명 ='" + 지역[0][0] + "'and 방위='" + direction + "' and 기간 ='" + (mth + 1) + "월'");
                eta[mth] = solar.eta() * solar._50() - solar.K1() * Convert.ToDouble(value[0][0]) / qsol_HN_d - solar.K2() * Convert.ToDouble(value[0][0]) * Convert.ToDouble(value[0][0]) / qsol_HN_d;
                if (eta[mth] < 0) { eta[mth] = solar.eta(); }
                qsol_mth[mth] = eta[mth] * qsol_HN_mth[mth];
                Qsol_mth[mth] = qsol_mth[mth] * solar.M_Area() * solar.M_Count() / 1.03 / 1.03;
                if (MainSystem != "태양열 융합 히트펌프")
                { Qh_sol[mth] = Math.Min(Qsol_mth[mth], (Qh_outg[mth] + Qw_outg[mth])); }

                Wh_gen[mth] = 0.025 * Qh_sol[mth];
            }
            for (int mth = 0; mth < 12; mth++)
            {
                Wh_g[mth] = Wh_g[mth] + Wh_gen[mth];
                Qh_outg[mth] = Qh_outg[mth] - Qh_sol[mth];
            }

            Save_Solar (DHNum, solar.Num());
        }

        private void Save_Solar(string DHWNum, string SolarNum)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            string RESystemNum = "";
            string[][] value = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "번호", "신재생시스템='" + SolarNum + "'");
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
                 "난방설비,급탕설비,신재생시스템,신재생시스템유형,생산소비,생산유형,총에너지",
                 "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + RESystemNum + "','" + MTH + "','" +
                HeatingNum + "','" + DHWNum + "','" + SolarNum + "','태양열시스템','생산','열','" +
                 Qh_sol[mth]
                  + "'", "번호,월,생산소비,생산유형"); ;
            }
            for (int mth = 0; mth <= 11; mth++)
            {
                string MTH = (mth + 1).ToString() + "월";
                Program.DB.setValue(DB.type.ProjDB, "RESystem_Result", "프로젝트번호,프로젝트유형,번호," +
                 "월," +
                 "난방설비,급탕설비,신재생시스템,신재생시스템유형,생산소비,소비연료,총에너지",
                 "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + RESystemNum + "','" + MTH + "','" +
                HeatingNum + "','" + DHWNum + "','" + SolarNum + "','태양열시스템','소비','전기','" +
                (0.025 * Qh_sol[mth])
                  + "'", "번호,월,생산소비,소비연료") ;
            }
            Program.DB.saveProject();
        }

        public void LoadCalc_AirHP(string ProjNum)
        {
            for (int n = 0; n < SelectAirHP_split.Count; n++)
            {
                string[][] airHP = Program.DB.getValue(ProjNum, "User_AirHP", "번호,연료,공급유형,난방정격용량,난방정격COP,난방정격소비전력,한랭지용량,한랭지COP,한랭지소비전력,대기전력", "번호 = '" + SelectAirHP_split[n] + "'");
                String Num = null;
                Carrier = null;
                String SupplyType = null;
                double Pi_nom = 0; //정격용량
                double COP_nom = 0; //정격COP
                double W_nom = 0; //정격소비전력 
                double Pi_15 = 0; //정격용량
                double COP_15 = 0; //정격COP
                double W_15 = 0; //정격소비전력 
                double W_0 = 0; //대기전력
                if (airHP.Length > 0)
                {
                    Num = airHP[0][0];
                    Carrier = airHP[0][1];
                    SupplyType = airHP[0][2];
                    Pi_nom = Convert.ToDouble(airHP[0][3]) * Convert.ToDouble(AirHPNum_split[n]); ; //정격용량
                    COP_nom = Convert.ToDouble(airHP[0][4]); //정격COP
                    W_nom = Pi_nom / COP_nom;
                    Pi_15 = Convert.ToDouble(airHP[0][6]) * Convert.ToDouble(AirHPNum_split[n]); //정격용량
                    COP_15 = Convert.ToDouble(airHP[0][7]); //정격COP
                    W_15 = Pi_15 / COP_15;
                    W_0 = Convert.ToDouble(airHP[0][9]);
                    Calc_Q_Air_HP(Num, SupplyType, Pi_nom, COP_nom, W_nom, Pi_15, COP_15, W_15, W_0);
                }
            }
        }
        public void Calc_Q_Air_HP(String Num, String SupplyType, double Pi_nom, double COP_nom, double W_nom, double Pi_15, double COP_15, double W_15, double W_0)
        {
            
                double Pi_2 = 0, Pi_7 = 0, COP_2 = 0, COP_7 = 0, W_2 = 0, W_7 = 0; //2도, -7도
                double[] 수방식_비율_Pi = { 0.64, 0.8, 0.95 };//-7,2,7
                double[] 직팽인버터_비율_Pi = { 0.81, 0.96, 1 };//-7,2,7,
                double[] 직팽없음_비율_Pi = { 0.81, 0.96, 1 };//-7,2,7,
                double[] COP_standard = new double[4];
               
                double themp_상수 = 10.00;
                COP_standard[0] = ((-7 + 15 - themp_상수) * (-7 + 15 + 273.15) / 15 + theta_SL + 7 - 15) / (theta_SL - themp_상수) + (-7 + 273.15) / (theta_SL - themp_상수) * Math.Log(Math.E, (themp_상수 + 7)) / 15; //-7일 경우,
                COP_standard[1] = ((2 + 15 - themp_상수) * (2 + 15 + 273.15) / 15 + theta_SL - 2 - 15) / (theta_SL - themp_상수) + (2 + 273.15) / (theta_SL - themp_상수) * Math.Log(Math.E, (themp_상수 - 2)) / 15; //2일 경우,
                COP_standard[2] = ((7 + 15 - themp_상수) * (7 + 15 + 273.15) / 15 + theta_SL - 7 - 15) / (theta_SL - themp_상수) + (7 + 273.15) / (theta_SL - themp_상수) * Math.Log(Math.E, (themp_상수 - 7)) / 15; //7일 경우,
                COP_standard[3] = ((-15 + 15 - themp_상수) * (-15 + 15 + 273.15) / 15 + theta_SL - (-15) - 15) / (theta_SL - themp_상수) + (-15 + 273.15) / (theta_SL - themp_상수) * Math.Log(Math.E, (themp_상수 - (-15))) / 15; //-15일 경우,

                if (Pi_15 > 0)
                {
                    Pi_2 = (Pi_nom - Pi_15) / 22 * 2 - (Pi_nom - Pi_15) / 22 * 7 + Pi_nom;
                    Pi_7 = (Pi_nom - Pi_15) / 22 * (-7) - (Pi_nom - Pi_15) / 22 * 7 + Pi_nom;

                    W_2 = (W_nom - W_15) / 22 * 2 - (W_nom - W_15) / 22 * 7 + W_nom;
                    W_7 = (W_nom - W_15) / 22 * (-7) - (W_nom - W_15) / 22 * 7 + W_nom;

                    COP_2 = Pi_2 / W_2;
                    COP_7 = Pi_7 / W_7;
                }
                else
                {
                    COP_7 = COP_nom * COP_standard[0] / COP_standard[2];
                    COP_2 = COP_nom * COP_standard[1] / COP_standard[2];

                    if (SupplyType == "수방식")
                    {
                        Pi_2 = Pi_nom * 수방식_비율_Pi[1] / 수방식_비율_Pi[2];
                        Pi_7 = Pi_nom * 수방식_비율_Pi[0] / 수방식_비율_Pi[2];
                    }
                    else if (AirHPControl_split[0] == "인버터제어")
                    {
                        Pi_2 = Pi_nom * 직팽인버터_비율_Pi[1] / 직팽인버터_비율_Pi[2];
                        Pi_7 = Pi_nom * 직팽인버터_비율_Pi[0] / 직팽인버터_비율_Pi[2];
                    }
                    else
                    {
                        Pi_2 = Pi_nom * 직팽없음_비율_Pi[1] / 직팽없음_비율_Pi[2];
                        Pi_7 = Pi_nom * 직팽없음_비율_Pi[0] / 직팽없음_비율_Pi[2];
                    }
                    W_7 = Pi_7 / COP_7;
                    W_2 = Pi_2 / COP_2;
                }


                double[,] kbuh = new double[4, 12], DH = new double[3, 12], Wi = new double[3, 12], H = new double[5, 12], Wtime = new double[3, 12];
                double[] fLg = new double[12];
                for (int mth = 1; mth <= 12; mth++)
                {
                    for (int k = 1; k <= 4; k++)
                    {
                        string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_히트펌프_하이브리드", "빈도", "지역명 ='" + 지역[0][0] + "' and 유형 ='선택운전' And 기간  = '" + mth + "월' and 구분 ='온도등급" + k + "'");
                        if (Value.Length > 0)
                        { kbuh[k - 1, mth - 1] = Convert.ToDouble(Value[0][0]); }

                    }
                }
                for (int mth = 1; mth <= 12; mth++)
                {
                    for (int k = 1; k <= 3; k++)
                    {
                        string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_히트펌프_일반", "빈도", "지역명 ='" + 지역[0][0] + "' and 단위 ='[Kh]' And 기간  = '" + mth + "월' and 구분 ='온도등급" + k + "'");
                        if (Value.Length > 0)
                        { DH[k - 1, mth - 1] = Convert.ToDouble(Value[0][0]); }
                    }

                    Wi[0, mth - 1] = Math.Max(DH[0, mth - 1] / (DH[0, mth - 1] + DH[1, mth - 1] + DH[2, mth - 1]), 0);
                    Wi[1, mth - 1] = Math.Max(DH[1, mth - 1] / (DH[0, mth - 1] + DH[1, mth - 1] + DH[2, mth - 1]), 0);
                    Wi[2, mth - 1] = Math.Max(DH[2, mth - 1] / (DH[0, mth - 1] + DH[1, mth - 1] + DH[2, mth - 1]), 0);
                    if(MainSystem =="태양열 융합 히트펌프")
                    {
                        Wi[0, mth - 1] = 1;
                        Wi[1, mth - 1] = 0;
                        Wi[2, mth - 1] = 0;
                    }

                }

                for (int mth = 1; mth <= 12; mth++)
                {
                    for (int k = 1; k <= 4; k++)
                    {
                        string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_히트펌프_일반", "빈도", "지역명 ='" + 지역[0][0] + "' and 단위 ='[h]' And 기간  = '" + mth + "월' and 구분 ='온도등급" + k + "'");
                        if (Value.Length > 0)
                        {
                            H[k - 1, mth - 1] = Convert.ToDouble(Value[0][0]);
                        }
                    }

                    Wtime[0, mth - 1] = H[0, mth - 1] / (H[0, mth - 1] + H[1, mth - 1] + H[2, mth - 1] + H[3, mth - 1] + H[4, mth - 1]);
                    Wtime[1, mth - 1] = H[1, mth - 1] / (H[0, mth - 1] + H[1, mth - 1] + H[2, mth - 1] + H[3, mth - 1] + H[4, mth - 1]);
                    Wtime[2, mth - 1] = H[2, mth - 1] / (H[0, mth - 1] + H[1, mth - 1] + H[2, mth - 1] + H[3, mth - 1] + H[4, mth - 1]);

                    if (SupplyType == "직팽식")
                    { fLg[mth - 1] = 1; }
                    else
                    {
                        fLg[mth - 1] = Math.Min(1, 1 - (Math.Max(theta_av_gen[mth], 60) - 60) / dtheta_gen[mth]);
                    }
                }

                double[,] Qh_outgi = new double[3, 12];

                for (int mth = 0; mth < 12; mth++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Qh_outgi[i, mth] = Qh_outg[mth] * Wi[i, mth];
                    }
                }

                double[] Pi_max = new double[3];//-7.2.7 
                double[] Pi_min = new double[3];
                double[] beta_ = new double[3], beta = new double[3];
                double[] COP_max = new double[3], COP_min = new double[3], COP_cal = new double[3];
                if (AirHPControl_split[0] != "인버터제어")
                {
                    Pi_max[0] = Pi_7;
                    Pi_max[1] = Pi_2;
                    Pi_max[2] = Pi_nom;
                    Pi_min[0] = Pi_7;
                    Pi_min[1] = Pi_2;
                    Pi_min[2] = Pi_nom;

                }
                else
                {
                    Pi_max[0] = (1 + 0.035 * ((-7) + 7) * Pi_7 / 0.9);
                    Pi_max[1] = (1 + 0.035 * ((2) + 7) * Pi_2 / 0.9);
                    Pi_max[2] = (1 + 0.035 * ((7) + 7) * Pi_nom / 0.9);
                    Pi_min[0] = Pi_max[0] * 0.2;
                    Pi_min[1] = Pi_max[1] * 0.2;
                    Pi_min[2] = Pi_max[2] * 0.2;
                }

                beta_[0] = Pi_7 / Pi_max[0];
                beta_[1] = Pi_2 / Pi_max[1];
                beta_[2] = Pi_nom / Pi_max[2];

                if (beta_[0] >= 0.8)
                { COP_max[0] = COP_7; }
                else { COP_max[0] = COP_7 - 4; }
                if (beta_[1] >= 0.8)
                { COP_max[1] = COP_2; }
                else { COP_max[1] = COP_2 - 4; }
                if (beta_[2] >= 0.8)
                { COP_max[2] = COP_7; }
                else { COP_max[2] = COP_7 - 4; }

                if (AirHPControl_split[0] != "인버터제어")
                {
                    COP_min[0] = COP_max[0];
                    COP_min[1] = COP_max[1];
                    COP_min[2] = COP_max[2];
                }
                else
                {
                    COP_min[0] = COP_max[0] - 2;
                    COP_min[1] = COP_max[1] - 2;
                    COP_min[2] = COP_max[2] - 2;
                }

                if (beta_[0] >= 0.8)
                {
                    COP_cal[0] = COP_7 + 0.2;
                    beta[0] = 0.6;
                }
                else
                {
                    COP_cal[0] = COP_7;
                    beta[0] = beta_[0];
                }
                if (beta_[1] >= 0.8)
                {
                    COP_cal[1] = COP_2 + 0.2;
                    beta[1] = 0.6;
                }
                else
                {
                    COP_cal[1] = COP_2;
                    beta[1] = beta_[1];
                }
                if (beta_[2] >= 0.8)
                {
                    COP_cal[2] = COP_nom + 0.2;
                    beta[2] = 0.6;
                }
                else
                {
                    COP_cal[2] = COP_nom;
                    beta[2] = beta_[2];
                }

                double[,] th_sng = new double[3, 12]; double[,] Qh_outg_prel_sng = new double[3, 12], Q_max = new double[3, 12], Q_min = new double[3, 12];
                double[,] th_op_sng = new double[3, 12], Pi_sng = new double[3, 12]; double[,] beta_hpi = new double[3, 12]; 
                double[,] COP_hp_pint = new double[3, 12];
                double[] FC = new double[12];
                for (int mth = 0; mth < 12; mth++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        th_sng[k, mth] = H[k, mth] * th_avg[mth] / (dmth[mth] * 24);
                        Qh_outg_prel_sng[k, mth] = Qh_outgi[k, mth] * fLg[mth] - Pi_max[k] * 0;
                        Q_max[k, mth] = Pi_max[k] * th_sng[k, mth];
                        Q_min[k, mth] = Pi_min[k] * th_sng[k, mth];


                        if (Qh_outg_prel_sng[k, mth] < Q_min[k, mth])
                        {
                            th_op_sng[k, mth] = Qh_outg_prel_sng[k, mth] / Pi_min[k];
                        }
                        else
                        {
                            th_op_sng[k, mth] = th_sng[k, mth];
                        }

                        if (th_op_sng[k, mth] < 0.001)
                        {
                            Pi_sng[k, mth] = 0;
                        }
                        else if (th_op_sng[k, mth] == th_sng[k, mth])
                        {
                            Pi_sng[k, mth] = Math.Min(Pi_max[k], Qh_outg_prel_sng[k, mth] / th_op_sng[k, mth]);
                        }
                        else
                        {
                            Pi_sng[k, mth] = Pi_min[k];
                        }

                        beta_hpi[k, mth] = Math.Max(Pi_sng[k, mth] / Pi_max[k], 0.2);

                        if (beta_[k] >= 0.8)
                        {
                            beta[k] = 0.6;
                        }
                        else
                        {
                            beta[k] = beta_[k];
                        }

                        Qh_outg_sng[k, mth] = Pi_sng[k, mth] * th_op_sng[k, mth];

                        if (beta[k] <= beta_hpi[k, mth])
                        {
                            COP_hp_pint[k, mth] = COP_cal[k] + (beta_hpi[k, mth] - beta[k]) / (1 - beta[k]) * (COP_max[k] - COP_cal[k]);
                        }
                        else
                        {
                            COP_hp_pint[k, mth] = COP_min[k] + (beta_hpi[k, mth] - 0.2) / (1 - 0.2) * (COP_cal[k] - COP_min[k]);
                        }
                    }
                    if (SupplyType == "직팽식")
                    {
                        FC[mth] = Math.Round((Qh_outg_sng[0, mth] + Qh_outg_sng[1, mth] + Qh_outg_sng[2, mth]) / (Pi_max[0] * th_op_sng[0, mth] + Pi_max[1] * th_op_sng[1, mth] + Pi_max[2] * th_op_sng[2, mth]), 1);
                        if (double.IsNaN(FC[mth]))
                        { FC[mth] = 0.1; }
                        else if(Qh_outg_sng[1, mth] == 0)
                        {
                            FC[mth] = 0.1;
                        }
                        string[][] Valuef = Program.DB.getValue(DB.type.BaseDB_Heating, "히트펌프부하계수", "값", "구분 ='" + AirHPSupply_split[0] + "' AND FC ='" + FC[mth] + "'");
                        if (Valuef.Length >0)
                        { fpint[mth] = Convert.ToDouble(Valuef[0][0]); }
                    }
                    else
                    {
                        FC[mth] = Math.Round(Math.Min(th_op_sng[0, mth] / (th_avg[mth]), 1), 1);
                        if (double.IsNaN(FC[mth]))
                        { FC[mth] = 0.1; }

                        string[][] Valuef;
                        if (ce1Type == "복사난방" || ce2Type == "복사난방")
                        {
                            Valuef = Program.DB.getValue(DB.type.BaseDB_Heating, "히트펌프부하계수", "값", "구분 ='바닥난방' AND FC ='" + FC[mth] + "'");
                        }
                        else
                        {
                            Valuef = Program.DB.getValue(DB.type.BaseDB_Heating, "히트펌프부하계수", "값", "구분 ='방열기' AND FC ='" + FC[mth] + "'");
                        }
                        if (Valuef.Length > 0)
                        { fpint[mth] = Convert.ToDouble(Valuef[0][0]); }
                    }

                    for (int k = 0; k < 3; k++)
                    {
                        if (SupplyType == "직팽식")
                        {
                            COPpint[k, mth] = fpint[mth] * COP_max[k];
                        }
                        else
                        {
                            if (FC[mth] < 1)
                            { COPpint[k, mth] = fpint[mth] * COP_min[k]; }
                            else { COPpint[k, mth] = fpint[mth] * COP_max[k]; }
                        }
                    }
                    if (Carrier == "전기")
                    { Qh_f[mth] = Qh_outg_sng[0, mth] / COP_hp_pint[0, mth] + Qh_outg_sng[1, mth] / COP_hp_pint[1, mth] + Qh_outg_sng[2, mth] / COP_hp_pint[2, mth]; }
                    else
                    {
                        Qh_f[mth] = (Qh_outg_sng[0, mth] / COP_hp_pint[0, mth] + Qh_outg_sng[1, mth] / COP_hp_pint[1, mth] + Qh_outg_sng[2, mth] / COP_hp_pint[2, mth]) * 1.11 / (1 + 0.1);
                    }

                    if (Qhb_mth_sum[mth] > 0)
                    {
                        Wh_g[mth] += W_0 * Math.Max(0,(24 * dmth[mth] - thrL[mth])) / 1000;
                    }
            }
        }

        public void LoadCalc_GroundHP(string ProjNum)
        {
            for (int n = 0; n < SelectGroundHP_split.Count; n++)
            {
                string[][] GroundHP = Program.DB.getValue(ProjNum, "User_GroundHP", "번호,연료,공급유형,난방정격용량,난방정격COP,난방정격소비전력,난방등급2용량,난방등급2COP,난방등급2소비전력,대기전력,수직수평", "번호 = '" + SelectGroundHP_split[n] + "'");
                String Num = null;
                Carrier = null;
                String SupplyType = null;
                double Pi_nom = 0; //정격용량
                double COP_nom = 0; //정격COP
                double W_nom = 0; //정격소비전력 
                double Pi_5 = 0; //정격용량
                double COP_5 = 0; //정격COP
                double W_5 = 0; //정격소비전력 
                double W_0 = 0; //대기전력
                string 수직수평 = "수직형";
                if (GroundHP.Length > 0)
                {
                    Num = GroundHP[0][0];
                    Carrier = GroundHP[0][1];
                    SupplyType = GroundHP[0][2];
                    Pi_nom = Convert.ToDouble(GroundHP[0][3]) * Convert.ToDouble(GroundHPNum_split[n]); ; //정격용량
                    COP_nom = Convert.ToDouble(GroundHP[0][4]); //정격COP
                    W_nom = Pi_nom / COP_nom;
                    Pi_5 = Convert.ToDouble(GroundHP[0][6]) * Convert.ToDouble(GroundHPNum_split[n]); //정격용량
                    COP_5 = Convert.ToDouble(GroundHP[0][7]); //정격COP
                    W_5 = Pi_5 / COP_5;
                    W_0 = Convert.ToDouble(GroundHP[0][9]);
                    수직수평 = GroundHP[0][10];
                    Calc_Q_Ground_HP(Num, SupplyType, Pi_nom, COP_nom, W_nom, Pi_5, COP_5, W_5, W_0,수직수평);
                }
            }
        }
        public void Calc_Q_Ground_HP(String Num, String SupplyType, double Pi_nom, double COP_nom, double W_nom, double Pi_5, double COP_5, double W_5, double W_0, string 수직수평)
        {
            double Pi__5=0, W__5=0, COP__5 = 0;
            double[] fLg = new double[12], theta_ground = new double[12], th_gen_op_sng_cor = new double[12];
            double[] COPc_standard = new double[12], COPc_eff = new double[12], COPcor_Tki_n = new double[12];
            double[] Pi_cor_Tki_n = new double[12], W_cor_Tki_n = new double[12], beta_hp_source_dash= new double[12], beta_hp_source = new double[12];
            double Pi_hp_source_max, Pi_hp_source_min;
            double[] COP_hp_source_max = new double[12],  COP_hp_source_min = new double[12], COP_hp_source= new double[12];
            double[] Qh_outg_sng_prel_i = new double[12], Qh_outg_sng_max = new double[12], Qh_outg_sng_min = new double[12], th_gen_op_sng_i = new double[12], Pi_hp_source_sng_i = new double[12];
            double[] beta_hp_i = new double[12];
            double[] Qh_outg_sng_i = new double[12], COPhp_pint_i = new double[12], FC= new double[12], fpint = new double[12], COPpint_i = new double[12];

            if (Pi_5 > 0 && W_5 > 0)
            {
                Pi__5 = Math.Max(0, (Pi_5 - Pi_nom) / (5 - 0) * (-5 - 0) + Pi_nom);
                W__5 = Math.Max(0, (W_5 - W_nom) / (5 - 0) * (-5 - 0) + W_nom);
                if (W__5 > 0)
                { COP__5 = Pi__5 / W__5; }
            }

            for (int n = 0; n < SelectGroundHP_split.Count; n++)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    if (SupplyType == "직팽식")
                    { fLg[mth] = 1; }
                    else
                    {
                        fLg[mth] = Math.Min(1, 1 - (Math.Max(theta_av_gen[mth], 60) - 60) / dtheta_gen[mth]);
                    }
                    if (수직수평 == "수직형")
                    {
                        theta_ground[mth] = 0.15 * theta_e[mth] + 1.5;
                    }
                    else
                    {
                        theta_ground[mth] = 0.15 * theta_e[mth] - 0.5;
                    }
                    th_gen_op_sng_cor[mth] = th_avg[mth];
                    COPc_standard[mth] = ((0 + 4 - 10) * (0 + 4 + 273.15) / 4 + theta_SL - 0 - 4) / (theta_SL - 10) + (0 + 273.15) / (theta_SL - 10) * Math.Log((theta_SL - 0) / 4);
                    COPc_eff[mth] = ((theta_ground[mth] + 4 - 10) * (theta_ground[mth] + 4 + 273.15) / 4 + theta_SL - theta_ground[mth] - 4) / (theta_SL - 10) + (theta_ground[mth] + 273.15) / (theta_SL - 10) * Math.Log((theta_SL - theta_ground[mth]) / 4);

                    if (COP__5 > 0 && COP_5 > 0)
                    {
                        Pi_cor_Tki_n[mth] = COP_nom;
                        COPcor_Tki_n[mth] = COP_nom * COPc_eff[mth] / COPc_standard[mth];
                    }
                    else
                    {
                        if (theta_ground[mth]< 0)
                        {
                            Pi_cor_Tki_n[mth] = (Pi_nom - Pi__5) / (0 - (-5)) * (theta_ground[mth]) - (Pi_nom - Pi__5) / (0 - (-5)) * 0 + Pi_nom;
                            W_cor_Tki_n[mth] = (W_nom - W__5) / (0 - (-5)) * (theta_ground[mth]) - (W_nom - W__5) / (0 - (-5)) * 0 + W_nom;
                        }
                        else
                        {
                            Pi_cor_Tki_n[mth] = (Pi_nom - Pi_5) / (0 - (5)) * (theta_ground[mth]) - (Pi_nom - Pi_5) / (0 - (5)) * 0 + Pi_nom;
                            W_cor_Tki_n[mth] = (W_nom - W_5) / (0 - (5)) * (theta_ground[mth]) - (W_nom - W_5) / (0 - (5)) * 0 + W_nom;
                        }
                        COPcor_Tki_n[mth] = Pi_cor_Tki_n[mth] / W_cor_Tki_n[mth];
                    }
                    if (GroundHPControl_split[n].ToString() != "인버터제어")
                    {
                        Pi_hp_source_max = Pi_nom;
                        Pi_hp_source_min = Pi_hp_source_max;
                    }
                    else
                    {
                        Pi_hp_source_max = Pi_nom /0.8;
                        Pi_hp_source_min = Pi_nom *0.2;
                    }
                    beta_hp_source_dash[mth] = Pi_cor_Tki_n[mth] / Pi_hp_source_max;

                    if (beta_hp_source_dash[mth] >= 0.8)
                    {
                        COP_hp_source_max[mth] = COPcor_Tki_n[mth];
                    }
                    else
                    {
                        COP_hp_source_max[mth] = COPcor_Tki_n[mth] - 0.4;
                    }
                    if (GroundHPControl_split[n].ToString() != "인버터제어")
                    {
                        COP_hp_source_min[mth] = COP_hp_source_max[mth];
                    }
                    else
                    {
                        COP_hp_source_min[mth] = COP_hp_source_max[mth] - 0.2;
                    }

                    if (beta_hp_source_dash[mth]>= 0.8)
                    {
                        COP_hp_source[mth] = COPcor_Tki_n[mth] + 0.2;
                        beta_hp_source[mth] = 0.6;
                    }
                    else
                    {
                        COP_hp_source[mth] = COPcor_Tki_n[mth];
                        beta_hp_source[mth] = beta_hp_source_dash[mth];
                    }
                    Qh_outg_sng_prel_i[mth] = Qh_outg[mth] * fLg[mth];
                    Qh_outg_sng_max[mth] = Pi_hp_source_max * th_gen_op_sng_cor[mth];
                    Qh_outg_sng_min[mth] = Pi_hp_source_min * th_gen_op_sng_cor[mth];

                    if (Qh_outg_sng_prel_i[mth] < Qh_outg_sng_min[mth])
                    {
                        th_gen_op_sng_i[mth] = Qh_outg_sng_prel_i[mth] / Pi_hp_source_min;
                    }
                    else
                    {
                        th_gen_op_sng_i[mth] = th_gen_op_sng_cor[mth];
                    }
                    if (th_gen_op_sng_i[mth] ==th_gen_op_sng_cor[mth])
                    {
                        Pi_hp_source_sng_i[mth] = Math.Min(Pi_hp_source_max, Qh_outg_sng_prel_i[mth] / th_gen_op_sng_i[mth]);
                    }
                    else
                    {
                        Pi_hp_source_sng_i[mth] = Pi_hp_source_min;
                    }
                    if (GroundHPControl_split[n].ToString() != "인버터제어")
                    {
                        beta_hp_i[mth] = Math.Max(Pi_hp_source_sng_i[mth] / Pi_hp_source_max, 1);
                    }
                    else
                    {
                        beta_hp_i[mth] = Math.Max(Pi_hp_source_sng_i[mth] / Pi_hp_source_max, 0.2);
                    }
                    Qh_outg_sng_i[mth] = Pi_hp_source_sng_i[mth] * th_gen_op_sng_i[mth];

                    if(beta_hp_source[mth] <= beta_hp_i[mth])
                    {
                        COPhp_pint_i[mth] = COP_hp_source[mth] + (beta_hp_i[mth] - beta_hp_source[mth]) / (1 - beta_hp_source[mth]) * (COP_hp_source_max[mth] - COP_hp_source[mth]);
                    }
                    else
                    {
                        COPhp_pint_i[mth] = COP_hp_source_min[mth] + (beta_hp_i[mth] - 0.2) / (beta_hp_source[mth] - 0.2) * (COP_hp_source[mth] - COP_hp_source_min[mth]);
                    }

                    if (SupplyType == "수방식")
                    {
                        FC[mth] = (th_gen_op_sng_i[mth]) / th_avg[mth]; 
                    }
                    else{
                        FC[mth] = Qh_outg_sng_i[mth] / Qh_outg_sng_max[mth];
                    }
                    if(ce1Type=="복사난방" || ce2Type=="복사난방")
                    {
                        fpint[mth] = 0.99;
                    }
                    else
                    {
                        if (FC[mth] >= 0.9)
                        { fpint[mth] = 0.98; }
                        else if (FC[mth] >= 0.8)
                        { fpint[mth] = 0.97; }
                        else if (FC[mth] >= 0.7)
                        { fpint[mth] = 0.96; }
                        else if (FC[mth] >= 0.6)
                        { fpint[mth] = 0.94; }
                        else
                        {
                            fpint[mth] = 0.89;
                        }
                    }
                    if (FC[mth]<1)
                    {
                        COPpint_i[mth]= fpint[mth]* COP_hp_source_min[mth];
                    }
                    else
                    {
                        COPpint_i[mth] = fpint[mth] * COP_hp_source_max[mth];
                    }
                    if(Carrier=="전기")
                    {
                        Qh_f[mth] = Qh_outg_sng_i[mth] / COPhp_pint_i[mth];
                    }
                    else
                    {
                        Qh_f[mth] = Qh_outg_sng_i[mth] / COPhp_pint_i[mth];
                    }

                }

                Save_GroundHP(SelectGroundHP_split[n].ToString(), "지열 히트펌프", Qh_outg_sng_i);
            }             
        }


        public void LoadCalc_GWHP(string ProjNum)
        {
            for (int n = 0; n < SelectGWHP_split.Count; n++)
            {
                string[][] GWHP = Program.DB.getValue(ProjNum, "User_GroundWHP", "번호,연료,공급유형,난방정격용량,난방정격COP,난방정격소비전력,난방등급2용량,난방등급2COP,난방등급2소비전력,대기전력,수직수평", "번호 = '" + SelectGWHP_split[n] + "'");
                String Num = null;
                Carrier = null;
                String SupplyType = null;
                double Pi_nom = 0; //정격용량
                double COP_nom = 0; //정격COP
                double W_nom = 0; //정격소비전력 
                double Pi_15 = 0; //정격용량
                double COP_15 = 0; //정격COP
                double W_15 = 0; //정격소비전력 
                double W_0 = 0; //대기전력
                string 수직수평 = "수직형";
                if (GWHP.Length > 0)
                {
                    Num = GWHP[0][0];
                    Carrier = GWHP[0][1];
                    SupplyType = GWHP[0][2];
                    Pi_nom = Convert.ToDouble(GWHP[0][3]) * Convert.ToDouble(GroundHPNum_split[n]); ; //정격용량
                    COP_nom = Convert.ToDouble(GWHP[0][4]); //정격COP
                    W_nom = Pi_nom / COP_nom;
                    Pi_15 = Convert.ToDouble(GWHP[0][6]) * Convert.ToDouble(GroundHPNum_split[n]); //정격용량
                    COP_15 = Convert.ToDouble(GWHP[0][7]); //정격COP
                    W_15 = Pi_15 / COP_15;
                    W_0 = Convert.ToDouble(GWHP[0][9]);
                    수직수평 = GWHP[0][10];
                    Calc_Q_GWHP(Num, SupplyType, Pi_nom, COP_nom, W_nom, Pi_15, COP_15, W_15, W_0);
                }
            }
        }
        public void Calc_Q_GWHP(String Num, String SupplyType, double Pi_nom, double COP_nom, double W_nom, double Pi_15, double COP_15, double W_15, double W_0)
        {
            double[] fLg = new double[12], theta_ground = new double[12], th_gen_op_sng_cor = new double[12];
            double[] COPc_standard = new double[12], COPc_eff = new double[12], COPcor_Tki_n = new double[12];
            double[] Pi_cor_Tki_n = new double[12], W_cor_Tki_n = new double[12], beta_hp_source_dash = new double[12], beta_hp_source = new double[12];
            double Pi_hp_source_max, Pi_hp_source_min;
            double[] COP_hp_source_max = new double[12], COP_hp_source_min = new double[12], COP_hp_source = new double[12];
            double[] Qh_outg_sng_prel_i = new double[12], Qh_outg_sng_max = new double[12], Qh_outg_sng_min = new double[12], th_gen_op_sng_i = new double[12], Pi_hp_source_sng_i = new double[12];
            double[] beta_hp_i = new double[12];
            double[] Qh_outg_sng_i = new double[12], COPhp_pint_i = new double[12], FC = new double[12], fpint = new double[12], COPpint_i = new double[12];


            for (int n = 0; n < SelectGWHP_split.Count; n++)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    if (SupplyType == "직팽식")
                    { fLg[mth] = 1; }
                    else
                    {
                        fLg[mth] = Math.Min(1, 1 - (Math.Max(theta_av_gen[mth], 60) - 60) / dtheta_gen[mth]);
                    }
                    theta_ground[mth] = 0.134 * theta_e[mth] + 9.32;

                    Pi_cor_Tki_n[mth] = (Pi_nom - Pi_15) / (10 - 15) * (theta_ground[mth]) - (Pi_nom - Pi_15) / (10 - 15) * 0 + Pi_nom;
                    W_cor_Tki_n[mth] = (W_nom - W_15) / (0 - 15) * (theta_ground[mth]) - (W_nom - W_15) / (0 - 15) * 0 + W_nom;

                    th_gen_op_sng_cor[mth] = th_avg[mth];
                    COPc_standard[mth] = ((0 + 4 - 10) * (0 + 4 + 273.15) / 4 + theta_SL - 0 - 4) / (theta_SL - 10) + (0 + 273.15) / (theta_SL - 10) * Math.Log((theta_SL - 0) / 4);
                    COPc_eff[mth] = ((theta_ground[mth] + 4 - 10) * (theta_ground[mth] + 4 + 273.15) / 4 + theta_SL - theta_ground[mth] - 4) / (theta_SL - 10) + (theta_ground[mth] + 273.15) / (theta_SL - 10) * Math.Log((theta_SL - theta_ground[mth]) / 4);
                    COPcor_Tki_n[mth] = COP_nom * COPc_eff[mth] / COPc_standard[mth];
                    
                    if (GroundHPControl_split[n].ToString() != "인버터제어")
                    {
                        Pi_hp_source_max = Pi_nom;
                        Pi_hp_source_min = Pi_hp_source_max;
                    }
                    else
                    {
                        Pi_hp_source_max = Pi_nom / 0.8;
                        Pi_hp_source_min = Pi_nom * 0.2;
                    }
                    beta_hp_source_dash[mth] = Pi_cor_Tki_n[mth] / Pi_hp_source_max;

                    if (beta_hp_source_dash[mth] >= 0.8)
                    {
                        COP_hp_source_max[mth] = COPcor_Tki_n[mth];
                    }
                    else
                    {
                        COP_hp_source_max[mth] = COPcor_Tki_n[mth] - 0.4;
                    }
                    if (GroundHPControl_split[n].ToString() != "인버터제어")
                    {
                        COP_hp_source_min[mth] = COP_hp_source_max[mth];
                    }
                    else
                    {
                        COP_hp_source_min[mth] = COP_hp_source_max[mth] - 0.2;
                    }

                    if (beta_hp_source_dash[mth] >= 0.8)
                    {
                        COP_hp_source[mth] = COPcor_Tki_n[mth] + 0.2;
                        beta_hp_source[mth] = 0.6;
                    }
                    else
                    {
                        COP_hp_source[mth] = COPcor_Tki_n[mth];
                        beta_hp_source[mth] = beta_hp_source_dash[mth];
                    }
                    Qh_outg_sng_prel_i[mth] = Qh_outg[mth] * fLg[mth];
                    Qh_outg_sng_max[mth] = Pi_hp_source_max * th_gen_op_sng_cor[mth];
                    Qh_outg_sng_min[mth] = Pi_hp_source_min * th_gen_op_sng_cor[mth];

                    if (Qh_outg_sng_prel_i[mth] < Qh_outg_sng_min[mth])
                    {
                        th_gen_op_sng_i[mth] = Qh_outg_sng_prel_i[mth] / Pi_hp_source_min;
                    }
                    else
                    {
                        th_gen_op_sng_i[mth] = th_gen_op_sng_cor[mth];
                    }
                    if (th_gen_op_sng_i[mth] == th_gen_op_sng_cor[mth])
                    {
                        Pi_hp_source_sng_i[mth] = Math.Min(Pi_hp_source_max, Qh_outg_sng_prel_i[mth] / th_gen_op_sng_i[mth]);
                    }
                    else
                    {
                        Pi_hp_source_sng_i[mth] = Pi_hp_source_min;
                    }
                    if (GroundHPControl_split[n].ToString() != "인버터제어")
                    {
                        beta_hp_i[mth] = Math.Max(Pi_hp_source_sng_i[mth] / Pi_hp_source_max, 1);
                    }
                    else
                    {
                        beta_hp_i[mth] = Math.Max(Pi_hp_source_sng_i[mth] / Pi_hp_source_max, 0.2);
                    }
                    Qh_outg_sng_i[mth] = Pi_hp_source_sng_i[mth] * th_gen_op_sng_i[mth];

                    if (beta_hp_source[mth] <= beta_hp_i[mth])
                    {
                        COPhp_pint_i[mth] = COP_hp_source[mth] + (beta_hp_i[mth] - beta_hp_source[mth]) / (1 - beta_hp_source[mth]) * (COP_hp_source_max[mth] - COP_hp_source[mth]);
                    }
                    else
                    {
                        COPhp_pint_i[mth] = COP_hp_source_min[mth] + (beta_hp_i[mth] - 0.2) / (beta_hp_source[mth] - 0.2) * (COP_hp_source[mth] - COP_hp_source_min[mth]);
                    }

                    if (SupplyType == "수방식")
                    {
                        FC[mth] = (th_gen_op_sng_i[mth]) / th_avg[mth];
                    }
                    else
                    {
                        FC[mth] = Qh_outg_sng_i[mth] / Qh_outg_sng_max[mth];
                    }
                    if (ce1Type == "복사난방" || ce2Type == "복사난방")
                    {
                        fpint[mth] = 0.99;
                    }
                    else
                    {
                        if (FC[mth] >= 0.9)
                        { fpint[mth] = 0.98; }
                        else if (FC[mth] >= 0.8)
                        { fpint[mth] = 0.97; }
                        else if (FC[mth] >= 0.7)
                        { fpint[mth] = 0.96; }
                        else if (FC[mth] >= 0.6)
                        { fpint[mth] = 0.94; }
                        else
                        {
                            fpint[mth] = 0.89;
                        }
                    }
                    if (FC[mth] < 1)
                    {
                        COPpint_i[mth] = fpint[mth] * COP_hp_source_min[mth];
                    }
                    else
                    {
                        COPpint_i[mth] = fpint[mth] * COP_hp_source_max[mth];
                    }
                    if (Carrier == "전기")
                    {
                        Qh_f[mth] = Qh_outg_sng_i[mth] / COPhp_pint_i[mth];
                    }
                    else
                    {
                        Qh_f[mth] = Qh_outg_sng_i[mth] / COPhp_pint_i[mth];
                    }

                }

                Save_GroundHP(SelectGWHP_split[n].ToString(), "지하수 히트펌프", Qh_outg_sng_i);
            }
        }



        private void Save_GroundHP(string Num, string 지열지하수, double[] Qh_outg)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            string RESystemNum = "";
            string DHWNum = "";
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
                 "난방설비,급탕설비,신재생시스템,신재생시스템유형,생산소비,생산유형,총에너지",
                 "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + RESystemNum + "','" + MTH + "','" +
                HeatingNum + "','" + DHWNum + "','" + Num + "','" + 지열지하수 + "','생산','열','" +
                 Qh_outg[mth]
                  + "'", "번호,월,생산소비,생산유형"); ;
            }
            for (int mth = 0; mth <= 11; mth++)
            {
                string MTH = (mth + 1).ToString() + "월";
                Program.DB.setValue(DB.type.ProjDB, "RESystem_Result", "프로젝트번호,프로젝트유형,번호," +
                 "월," +
                 "난방설비,급탕설비,신재생시스템,신재생시스템유형,생산소비,소비연료,총에너지",
                 "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + RESystemNum + "','" + MTH + "','" +
                HeatingNum + "','" + DHWNum + "','" + Num + "','" + 지열지하수 + "','소비','전기','" +
                Qh_f[mth]
                  + "'", "번호,월,생산소비,소비연료");
            }
            Program.DB.saveProject();
        }


        public void LoadCalc_ABS(string ProjNum)
        {
            for (int n = 0; n < SelectABS_split.Count; n++)
            {
                string[][] Value = Program.DB.getValue(ProjNum, "User_ABS", "번호,연료,난방용량,난방성능,대기전력,지역난방", "번호 = '" + SelectABS_split[n] + "'");
                if (Value.Length > 0)
                {
                    String Num = Value[0][0];
                    Carrier = Value[0][1];
                    double Power = Convert.ToDouble(Value[0][2]) * Convert.ToDouble(ABSNum_split[n]);
                    string[][] 기존신규 = Program.DB.getValue(ProjNum, "HeatingSystem_Form", "프로젝트유형", "번호 = '" + HeatingNum + "'");
                    double cop = Convert.ToDouble(Value[0][3]);
                    if (기존신규[0][0] == "1")
                    {
                        cop = cop * 0.95;
                    }
                    double W_0 = Convert.ToDouble(Value[0][4]);
                    double count = Convert.ToDouble(ABSNum_split[n]);
                    if (Carrier == "지역난방" &&  Value[0][5]!=null&& Value[0][5]!="")
                    {
                        SelectDH_nonsplit = Value[0][5];
                        SelectDH_split = Split_(SelectDH_nonsplit);
                        LoadCalc_DH(ProjNum);
                    }                    
                    Calc_Qh_ABS(Num, Power, cop, W_0, count);
                }
            }
        }
        public void Calc_Qh_ABS(String Num, double Power, double cop, double W_0, double count)
        { 
            double loss = 0; 
            string[][] Value =  Program.DB.querySQL(DB.type.BaseDB_Heating, "Select 용량, 열손실률 From 흡수식온수기열손실률 Order by 용량");
            if(Value.Length >0)
            {
               for(int a=0; a<Value.Length; a++)
                {
                    if (Power <= Convert.ToDouble(Value[a][0]))
                    {
                        loss = Convert.ToDouble(Value[a][1]);
                    }
                }
            }
            double[] Qh_gen_ABS = new double[12];
            for (int mth = 0; mth < 12; mth++)
            {
                Qh_gen_ABS[mth] = Qh_outg[mth] * loss;
            }
            for (int mth = 0; mth < 12; mth++)
            {
                Qh_gen[mth] = Qh_gen[mth] + Qh_gen_ABS[mth];
                Qh_f[mth] = Qh_outg[mth] / cop + Qh_gen[mth];                
                if (Qhb_mth_sum[mth] >0)
                {
                    Wh_g[mth] += W_0 * Math.Max(0,(24 * dmth[mth] - thrL[mth])) / 1000;
                }
            }

        }

        public void LoadCalc_DH(string ProjNum)
        {
            for (int n = 0; n < SelectDH_split.Count; n++)
            {
                string[][] Value = Program.DB.getValue(ProjNum, "User_DH", "번호,용량,공급온도1차,환수온도1차,공급온도2차,환수온도2차", "번호 = '" + SelectDH_split[n] + "'");
                if (Value.Length > 0)
                {
                    String Num = Value[0][0];
                    Carrier = "지역난방";
                    double Power = Convert.ToDouble(Value[0][1]);
                    double SL_1 = Convert.ToDouble(Value[0][2]);
                    double RL_1 = Convert.ToDouble(Value[0][3]);
                    double SL_2 = Convert.ToDouble(Value[0][4]);
                    double RL_2 = Convert.ToDouble(Value[0][5]);
                    Calc_Qh_DH(Num, Power, SL_1,SL_2);
                }
            }
        }
        public void Calc_Qh_DH(String Num, double Power, double theta_prime,double theta_sek)
        {
            // theta_prime = 105 > D_DS =0.6, theta_prime = 150 > D_DS =0.4 //DIN V 18599-5 table 58
            double D_DS = (0.4 - 0.6) / (150 - 105) * (theta_prime - 105) + 0.6;
            // theta_prime = 105 > B_DS =3.5, theta_prime = 150 > B_DS =3.1 //DIN V 18599-5 table 59
            double B_DS = (3.1 - 3.5) / (150 - 105) * (theta_prime - 105) + 3.5;
            double theta_DS = D_DS * theta_prime + (1 - D_DS) * theta_sek;
            double H_DS = B_DS * Math.Pow(Power, 1.0 / 3.0);
            double Qh_outg_a = 0;
            double[] theta_i = new double[12];
            double[] Qh_gen_DH = new double[12];
            for (int mth = 0; mth < 12; mth++)
            {
                Qh_outg_a += Qh_outg[mth];
                if (SystemLoacation == "단열외피 외부")
                {
                    theta_i[mth] = theta_u[mth];
                }
                else if (SystemLoacation == "외기")
                {
                    theta_i[mth] = theta_e[mth];
                }
                else
                {
                    theta_i[mth] = theta_ih_avg[mth];
                }
            }

            for (int mth = 0; mth < 12; mth++)
            {
                Qh_gen_DH[mth] = H_DS * Qh_outg[mth] / Qh_outg_a * (theta_DS - theta_i[mth]);
            }
            for (int mth = 0; mth < 12; mth++)
            {
                if (MainSystem != "흡수식온수기" && Sub1System != "흡수식온수기" && Sub2System != "흡수식온수기")
                {
                    Qh_gen[mth] = Qh_gen_DH[mth];
                    Qh_f[mth] = Qh_outg[mth] + Qh_gen[mth];
                }
                else
                {
                    Qh_gen[mth] = Qh_gen[mth] + Qh_gen_DH[mth];
                }
            }
        }
        public void nan()
        {
            for (int mth = 0; mth < 12; mth++)
            {
                if (double.IsNaN(Qh_ce[mth])) { Qh_ce[mth] = 0; }
                if (double.IsNaN(Qh_d[mth])) { Qh_d[mth] = 0; }
                if (double.IsNaN(Qh_s[mth])) { Qh_s[mth] = 0; }
                if (double.IsNaN(Qh_gen[mth])) { Qh_gen[mth] = 0; }
                if (double.IsNaN(Qh_f[mth])) { Qh_f[mth] = 0; }

                if (double.IsNaN(Wh_ce[mth])) { Wh_ce[mth] = 0; }
                if (double.IsNaN(Wh_d[mth])) { Wh_d[mth] = 0; }
                if (double.IsNaN(Wh_s[mth])) { Wh_s[mth] = 0; }
                if (double.IsNaN(Wh_g[mth])) { Wh_g[mth] = 0; }

                if (double.IsNaN(Qh_gen_day[mth])) { Qh_gen_day[mth] = 0; }
                if (double.IsNaN(Pgen_Pn[mth])) { Pgen_Pn[mth] = 0; }
                if (double.IsNaN(Pgen_Pint[mth])) { Pgen_Pint[mth] = 0; }
                if (double.IsNaN(Pgen_P0[mth])) { Pgen_P0[mth] = 0; }
                if (double.IsNaN(eta_gen_Pn[mth])) { eta_gen_Pn[mth] = 0; }
                if (double.IsNaN(eta_gen_Pint[mth])) { eta_gen_Pint[mth] = 0; }

                if (Qh_ce[mth] <= 0) { Qh_ce[mth] = 0; }
                if (Qh_d[mth] <= 0) { Qh_d[mth] = 0; }
                if (Qh_s[mth] <= 0) { Qh_s[mth] = 0; }
                if (Qh_gen[mth] <= 0) { Qh_gen[mth] = 0; }
                if (Qh_f[mth] <= 0) { Qh_f[mth] = 0; }

                if (Wh_ce[mth] <= 0) { Wh_ce[mth] = 0; }
                if (Wh_d[mth] <= 0) { Wh_d[mth] = 0; }
                if (Wh_s[mth] <= 0) { Wh_s[mth] = 0; }
                if (Wh_g[mth] <= 0) { Wh_g[mth] = 0; }

                if (Qhb_mth_sum[mth] <= 0.01) 
                {
                    Qh_ce[mth] = 0;
                    Qh_d[mth] = 0;
                    Qh_s[mth] = 0;
                    Qh_gen[mth] = 0;
                    Qh_f[mth] = 0;
                    Wh_ce[mth] = 0;
                    Wh_d[mth] = 0;
                    Wh_s[mth] = 0;
                    Wh_g[mth] = 0;
                }
                double sum = Qhb_z[mth] + Qhb_ahu[mth];
                if (sum >0)
                {
                    Qh_ce_z[mth] = Qh_ce[mth] * Qhb_z[mth]/sum;
                    Qh_ce_ahu[mth] = Qh_ce[mth] * Qhb_ahu[mth] / sum;

                    Qh_d_z[mth] = Qh_d[mth] * Qhb_z[mth] / sum;
                    Qh_d_ahu[mth] = Qh_d[mth] * Qhb_ahu[mth] / sum;

                    Qh_s_z[mth] = Qh_s[mth] * Qhb_z[mth] / sum;
                    Qh_s_ahu[mth] = Qh_s[mth] * Qhb_ahu[mth] / sum;

                    Qh_outg_z[mth] = Qh_outg[mth] * Qhb_z[mth] / sum;
                    Qh_outg_ahu[mth] = Qh_outg[mth] * Qhb_ahu[mth] / sum;

                    Wh_ce_z[mth] = Wh_ce[mth] * Qhb_z[mth] / sum;
                    Wh_ce_ahu[mth] = Wh_ce[mth] * Qhb_ahu[mth] / sum;

                    Wh_d_z[mth] = Wh_d[mth] * Qhb_z[mth] / sum;
                    Wh_d_ahu[mth] = Wh_d[mth] * Qhb_ahu[mth] / sum;

                    Wh_s_z[mth] = Wh_s[mth] * Qhb_z[mth] / sum;
                    Wh_s_ahu[mth] = Wh_s[mth] * Qhb_ahu[mth] / sum;
                }
            }

        }
    }
      
    public class CE
    {
        String ce_Num, ce_ZoneNum, ce_ceNum, ce_ceType, ce_Location, ce_Control;
        double ce_theta, ce_Zone_Percent;
        public CE(String Num, String ZoneNum, String ceNum, String ceType, String Location, String Control, double theta, double Zone_Percent)
        {
            this.ce_Num = Num;
            this.ce_ZoneNum = ZoneNum;
            this.ce_ceNum = ceNum;
            this.ce_ceType = ceType;
            this.ce_Location = Location;
            this.ce_Control = Control;
            this.ce_theta = theta;
            this.ce_Zone_Percent = Zone_Percent;
        }
        public String Num()
        {
            return this.ce_Num;
        }
        public String ZoneNum()
        {
            return this.ce_ZoneNum;
        }
        public String ceNum()
        {
            return this.ce_ceNum;
        }
        public String ceType()
        {
            return this.ce_ceType;
        }
        public String Location()
        {
            return this.ce_Location;
        }
        public String Control()
        {
            return this.ce_Control;
        }
        public double theta_ce()
        {
            return this.ce_theta;
        }
        public double Zone_Percent()
        {
            return this.ce_Zone_Percent;
        }
    }
    public class Pump
    {
        String Num_pump;  double V_pump; double Power_pump; double H_pump; double count_pump; String Valve_pump; String Control_pump;
        public Pump(String Num, double V, double Power, double H, double count, String Valve, String Control)
        {
            this.Num_pump = Num;
            this.V_pump = V;
            this.Power_pump = Power;
            this.H_pump = H;
            this.count_pump = count;
            this.Valve_pump = Valve;
            this.Control_pump = Control;
        }
        public String Num()
        {
            return this.Num_pump;
        }
        public double V()
        {
            return this.V_pump;
        }
        public double Power()
        {
            return this.Power_pump;
        }

        public double H()
        {
            return this.H_pump;
        }
        public double Count()
        {
            return this.count_pump;
        }
        public String Valve()
        {
            return this.Valve_pump;
        }
        public String Control()
        {
            return this.Control_pump;
        }
    }

    public class Boiler
    {
        String Num_Boiler; string combi_Boiler; String Carrier_Boiler; String Type_Boiler; double Power_Boiler; double eta_Pn_Boiler; double eta_Pint_Boiler; double W_Boiler; double W_0_Boiler; double count_Boiler;
        double K_Boiler, L_Boiler, E_Boiler, F_Boiler, G_pn_Boiler, H_pn_Boiler, n_pn_Boiler, G_pint_Boiler, H_pint_Boiler, n_pint_Boiler;
        public Boiler(String Num, String Combi, String Carrier, String Type, double Power, double eta_Pn, double eta_Pint, double W, double W_0, double count, double K, double L, double E, double F, double G_pn, double H_pn, double n_pn, double G_pint, double H_pint, double n_pint_Boiler)
        {
            this.Num_Boiler = Num;
            this.combi_Boiler = Combi;
            this.Carrier_Boiler = Carrier;
            this.Type_Boiler = Type;
            this.Power_Boiler = Power;
            this.eta_Pn_Boiler = eta_Pn;
            this.eta_Pint_Boiler = eta_Pint;
            this.W_Boiler = W;
            this.W_0_Boiler = W_0;
            this.count_Boiler = count;
            this.K_Boiler = K;
            this.L_Boiler = L;
            this.E_Boiler = E;
            this.F_Boiler = F;
            this.G_pn_Boiler = G_pn;
            this.H_pn_Boiler = H_pn;
            this.n_pn_Boiler = n_pn;
            this.G_pint_Boiler = G_pint;
            this.H_pint_Boiler = H_pint;
            this.n_pint_Boiler = n_pint_Boiler;
        }

        public string Num() { return this.Num_Boiler; }
        public string carreir() { return this.Carrier_Boiler; }
        public string Type() { return this.Type_Boiler; }
        public double Power() { return this.Power_Boiler; }
        public double eta_Pn() { return this.eta_Pn_Boiler; }
        public double eta_Pint() { return this.eta_Pint_Boiler; }
        public double W() { return this.W_Boiler; }
        public double W_0() { return this.W_0_Boiler; }
        public double count() { return this.count_Boiler; }

        public double K() { return this.K_Boiler; }
        public double L() { return this.L_Boiler; }
        public double E() { return this.E_Boiler; }
        public double F() { return this.F_Boiler; }
        public double G_pn() { return this.G_pn_Boiler; }
        public double H_pn() { return this.H_pn_Boiler; }
        public double n_pn() { return this.n_pn_Boiler; }
        public double G_pint() { return this.G_pint_Boiler; }
        public double H_pint() { return this.H_pint_Boiler; }
        public double n_pint() { return this.n_pint_Boiler; }
    }

    public class HP
    {
        String HP_Num, HP_Source, HP_Carrier, HP_Power, HP_COP, HP_W, HP_Power2, HP_COP2, HP_W2, HP_Count;
        public HP(String Num, String HeatSource, String Carrier, String Powr, String COP, String W, String Power2, String COP2, String W2, String Count)
        {
            this.HP_Num = Num;
            this.HP_Source = HeatSource;
            this.HP_Carrier = Carrier;
            this.HP_Power = Powr;
            this.HP_COP = COP;
            this.HP_W = W;
            this.HP_Power2 = Power2;
            this.HP_COP2 = COP2;
            this.HP_W2 = W2;
            this.HP_Count = Count;
        }
        public string Num() { return this.HP_Num; }
        public string HeatSource() { return this.HP_Source; }
        public string Carrier() { return this.HP_Carrier; }
        public string Power() { return this.HP_Power; }
        public string COP() { return this.HP_COP; }
        public string W() { return this.HP_W; }
        public string Power2() { return this.HP_Power2; }
        public string COP2() { return this.HP_COP2; }
        public string W2() { return this.HP_W2; }
        public string Count() { return this.HP_Count; }

    }

    public class Solar
    {
        String Solar_Num, Solar_Direction, Solar_Degree;
        double SolarM_Area, Solar_eta, Solar_K1, Solar_K2, Solar_50, Solar_C, SolarM_Count;
        public Solar(String Num, double M_Area, double eta, double K1, double K2, double _50, double C, double M_Count, String Direction, String Degree)
        {
            this.Solar_Num = Num;
            this.SolarM_Area = M_Area;
            this.Solar_eta = eta;
            this.Solar_K1 = K1;
            this.Solar_K2 = K2;
            this.Solar_50 = _50;
            this.Solar_C = C;
            this.SolarM_Count = M_Count;
            this.Solar_Direction = Direction;
            this.Solar_Degree = Degree;
        }
        public string Num() { return this.Solar_Num; }
        public double M_Area() { return this.SolarM_Area; }
        public double eta() { return this.Solar_eta; }
        public double K1() { return this.Solar_K1; }
        public double K2() { return this.Solar_K2; }
        public double _50() { return this.Solar_50; }
        public double C() { return this.Solar_C; }
        public double M_Count() { return this.SolarM_Count; }
        public string Direction() { return this.Solar_Direction; }
        public string Degree() { return this.Solar_Degree; }


    }
}