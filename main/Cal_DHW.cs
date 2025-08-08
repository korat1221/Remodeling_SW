using System;
using System.Collections;
using System.Drawing;
using System.Runtime.Intrinsics.Arm;
using System.Security.Policy;
using System.Windows.Forms;

namespace main
{
    internal class DHW
    {
        public String DHWNum, DHWName; String SelectZone_nonsplit;
        String SystemLoacation, SLRL, Complex, MainSystem, Sub1System, Sub2System;
        String SelectBoiler_nonsplit, BoilerNum_nonsplit;
        String SelectSolar_nonsplit, SolarNum_nonsplit, SolarDirection_nonsplit, SolarDegree_nonsplit; String SelectFC_nonsplit, FCNum_nonsplit, FCElecInstall_nonsplit, FCElecHeat_nonsplit;
        String SelectHP_nonsplit, HPNum_nonsplit, HPControl_nonsplit;
        String SelectDH_nonsplit;
        String PumpUse, PumpMethod, Pump1, Pump2, Pump1Valve, Pump2Valve, Pump1Control, Pump2Control; int Pump1Count, Pump2Count; double Pump1Volume, Pump2Volume, Pump1Head, Pump2Head;
        public ArrayList Pump = new ArrayList();
        String StorageUse, StoragePumpUse, StoragePump,StorageType; public double Vs;
        String[] SystemType = { "보일러","지역난방", "태양열시스템","연료전지" };
        double PipeD, PipeInsD, PipeIns_Ramda;
        String PipeIns;
        int ZoneCount;
        public ArrayList SelectZone_split = new ArrayList(); public ArrayList SelectBoiler_split = new ArrayList(); public ArrayList BoilerNum_split = new ArrayList(); public ArrayList SelectDH_split = new ArrayList();
        public double[] Qwb_mth_sum = new double[12]; public double[] theta_ih_avg = new double[12]; public double[] theta_e = new double[12]; public double[] theta_u = new double[12];
        public double Qw_a_sum, th_op_day_avg, theta_i_h_set_avg; public double[] dop_mth_avg = new double[12];
        double SL, RL;
        public double[] dmth = new double[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public double[] Qw_d = new double[12], Qw_s = new double[12], Qw_gen = new double[12], Qw_outg = new double[12], Qw_f = new double[12];
        public double[] Ww_d = new double[12], Ww_s = new double[12], Ww_g = new double[12];
        public double Psi_pipe, PipeL, Qs_po_day;
        public double[] Qw_gen_day = new double[12], Qw_gen_p0_day = new double[12], eta_pn_w = new double[12];
        public String Carrier;
        public ArrayList SelectSolar_split = new ArrayList(); public ArrayList SolarNum_split = new ArrayList(); public ArrayList SolarDirection_split = new ArrayList(); ArrayList SolarDegree_split = new ArrayList();
        public ArrayList SelectHP_split = new ArrayList(); public ArrayList HPNum_split = new ArrayList(); public ArrayList HPControl_split = new ArrayList();
        public ArrayList SelectFC_split = new ArrayList(); ArrayList FCNum_split = new ArrayList(); ArrayList FCElecInstall_split = new ArrayList(); ArrayList FCElecHeat_split = new ArrayList();

        public double[] Eth_gen_out = new double[12];// 연료전지 열 생산량
        public double[] Eel_gen_out = new double[12];//연료전지 전기생산량
        public double[] Egen_in = new double[12];//연료전지 연료소비량 

        public double[] Qw_sol = new double[12];
        string[][] 프로젝트번호 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호");

        string[][] 지역, 외기온도;
        public DHW(String Num)
        {
            this.DHWNum = Num;
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


        public void Load_Zonedata(string ProjNum)
        {
            double[,] Qwb_mth; double[,] theta_ih;double[,] dop_mth; double[] th_op_day;  double[] Qwb_a; double[] theta_i_h_set;
            Boolean Now_Check = true;
            if (ProjNum == 프로젝트번호[0][0])
            { Now_Check = true; }
            else
            { Now_Check = false; }

            //존 정보 불러오기
            string[][] Value = Program.DB.getValue(ProjNum, "DHWSystem_Form", "명칭,존", "번호 = '" + DHWNum + "'");
            if (Value.Length > 0)
            {
                DHWName = Value[0][0];
                SelectZone_nonsplit = Value[0][1];
                SelectZone_split = Split_(SelectZone_nonsplit);
                Qwb_mth = new double[SelectZone_split.Count, 12];
                theta_ih = new double[SelectZone_split.Count, 12];
                Qwb_a = new double[SelectZone_split.Count];
                dop_mth = new double[SelectZone_split.Count, 12];
                th_op_day = new double[SelectZone_split.Count];
                theta_i_h_set = new double[SelectZone_split.Count];
                double[] dop_a = new double[SelectZone_split.Count];
                for (int n = 0; n < SelectZone_split.Count; n++)
                {
                    Zone zone = null; double Qwb_day = 0;
                    if (Now_Check == true)
                    {
                        zone = Program.CALC.getZone(SelectZone_split[n].ToString());
                        string[][] kk = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "일일급탕요구량", "존번호 = '" + zone.ZoneNum + "'");
                        if (kk.Length > 0)
                        { Qwb_day += Convert.ToDouble(kk[0][0]); }
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
                                    if (split[m].ToString() == SelectZone_split[n].ToString())
                                    {
                                        zone = Program.CALC.getZone(PostZone[j][0]);
                                        string[][] kk = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "일일급탕요구량", "존번호 = '" + zone.ZoneNum + "'");
                                        if (kk.Length > 0)
                                        { Qwb_day += Convert.ToDouble(kk[0][0]); }
                                    }
                                }
                            }
                        }
                    }

                    if (zone != null)
                    {

                        for (int mth = 0; mth < 12; mth++)
                        {
                            dop_mth[n, mth] = zone.dwd_mth[mth];
                            dop_a[n] += zone.dwd_mth[mth];
                        }
                        for (int mth = 0; mth < 12; mth++)
                        {
                            Qwb_mth[n, mth] = Qwb_day * dop_a[n] * dmth[mth] / 365 * (-0.02 * theta_e[mth] + 1.25);
                            theta_ih[n, mth] = zone.theta_i[1, 0, mth]; //이용일 난방
                            Qwb_a[n] += Qwb_mth[n, mth]; //연간 요구량
                            th_op_day[n] = zone.th_op_d;
                            theta_i_h_set[n] = zone.theta_i_h_set;
                        }
                    }
                    ZoneCount = ZoneCount + 1;
                }

                for (int n = 0; n < ZoneCount; n++)
                {

                    Qw_a_sum += Qwb_a[n];

                    //요구량 가중
                    th_op_day_avg += (th_op_day[n] * Qwb_a[n]);
                    theta_i_h_set_avg += (theta_i_h_set[n] * Qwb_a[n]);
                }
                th_op_day_avg = th_op_day_avg / Qw_a_sum;
                theta_i_h_set_avg = theta_i_h_set_avg / Qw_a_sum;

                for (int mth = 0; mth < 12; mth++)
                {
                    for (int n = 0; n < ZoneCount; n++)
                    {
                        Qwb_mth_sum[mth] += Qwb_mth[n, mth];

                        //요구량 가중
                        theta_ih_avg[mth] += (theta_ih[n, mth] * Qwb_a[n]);
                        dop_mth_avg[mth] += (dop_mth[n, mth] * Qwb_a[n]);
                    }

                    theta_ih_avg[mth] = theta_ih_avg[mth] / Qw_a_sum;
                    dop_mth_avg[mth] = dop_mth_avg[mth] / Qw_a_sum;
                }
            }
        }

        //일반정보 불러오기 
        public void Load_DHWGeneral(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "DHWSystem_Form", "설치위치,공급환수온도,복합설비유무,주요설비,보조설비1,보조설비2", "번호 = '" + DHWNum + "'");
            if (Value.Length > 0)
            {
                SystemLoacation = Value[0][0];
                SLRL = Value[0][1];
                if (SLRL != null && SLRL != "")
                {
                    string[][] Value2 = Program.DB.getValue(DB.type.BaseDB_Heating, "공급환수온도", "공급온도,환수온도", "공급환수온도 = '" + SLRL + "'");
                    if (Value2.Length > 0)
                    {
                        SL = Convert.ToDouble(Value2[0][0]);
                        RL = Convert.ToDouble(Value2[0][1]);
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
            string[][] Value = Program.DB.getValue(ProjNum, "DHWSystem_Form", "보일러종류,보일러대수", "번호 = '" + DHWNum + "'");
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
            string[][] Value = Program.DB.getValue(ProjNum, "DHWSystem_Form", "태양열번호,모듈개수,모듈방위,모듈기울기", "번호 = '" + DHWNum + "'");
            if (Value.Length > 0)
            {
                SelectSolar_nonsplit = Value[0][0];
                SelectSolar_split = Split_(SelectSolar_nonsplit);

                SolarNum_nonsplit = Value[0][1];
                SolarNum_split = Split_(SolarNum_nonsplit);

                SolarDirection_nonsplit = Value[0][2];
                SolarDirection_split = Split_(SolarDirection_nonsplit);

                SolarDegree_nonsplit = Value[0][3];
                SolarDegree_split = Split_(SolarDegree_nonsplit);
            }
        }

        public void Load_FC_general(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "DHWSystem_Form", "연료전지번호,연료전지대수,연료전지설치유형,연료전지생산유형", "번호 = '" + DHWNum + "'");
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

        public void Load_HP_general(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "DHWSystem_Form", "히트펌프번호,히트펌프제어방식,히트펌프대수", "번호 = '" + DHWNum + "'");
            if (Value.Length > 0)
            {
                SelectHP_nonsplit = Value[0][0];
                SelectHP_split = Split_(SelectHP_nonsplit);

                HPNum_nonsplit = Value[0][1];
                HPNum_split =  Split_(HPNum_nonsplit);

                HPControl_nonsplit = Value[0][2];
                HPControl_split =  Split_(HPControl_nonsplit);

            }
        }
        public void Load_DH_general(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "DHWSystem_Form", "지역난방번호", "번호 = '" + DHWNum + "'");
            if (Value.Length > 0)
            {
                SelectDH_nonsplit = Value[0][0];
                SelectDH_split = Split_(SelectDH_nonsplit);
            }
        }
        public void Load_PumpData(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "DHWSystem_Form", "펌프유무,펌프방식,펌프1종류,펌프2종류,펌프1밸브,펌프2밸브,펌프1제어,펌프2제어,펌프1대수,펌프2대수,펌프1유량,펌프2유량,펌프1양정,펌프2양정", "번호 = '" + DHWNum + "'");
            if (Value.Length > 0)
            {
                PumpUse = Value[0][0];
                PumpMethod = Value[0][1];

                Pump1 = Value[0][2];
                Pump2 = Value[0][3];
                Pump1Valve = Value[0][4];
                Pump2Valve = Value[0][5];
                Pump1Control = Value[0][6];
                Pump2Control = Value[0][7];
                Pump1Count = Convert.ToInt16(Value[0][8]);
                Pump2Count = Convert.ToInt16(Value[0][9]);
                Pump1Volume = Value[0][10] == "" || Value[0][10] == null ? 0 : Convert.ToDouble(Value[0][10]);
                Pump2Volume = Value[0][11] == "" || Value[0][11] == null ? 0 : Convert.ToDouble(Value[0][11]);
                Pump1Head = Value[0][12] == "" || Value[0][12] == null ? 0 : Convert.ToDouble(Value[0][12]);
                Pump2Head = Value[0][13] == "" || Value[0][13] == null ? 0 : Convert.ToDouble(Value[0][13]);
            }
        }
       

        public void Load_StorageData(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "DHWSystem_Form", "축열유무,축열펌프유무,축열펌프,축열용량,축열유형", "번호 = '" + DHWNum + "'");
            if (Value.Length > 0)
            {
                StorageUse = Value[0][0];
                StoragePumpUse = Value[0][1];
                StoragePump = Value[0][2];
                if (Value[0][3] != null && Value[0][3] != "")
                {
                    Vs = Convert.ToDouble(Value[0][3]);
                }
                StorageType = Value[0][4];
            }
        }

        public void Load_PipeData(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "DHWSystem_Form", "배관관경,배관보온두께,보온열전도율,배관보온재,노출배관길이", "번호 = '" + DHWNum + "'");
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
       
        public void Calc_Qd(string ProjNum)
        {
            double R_pipe, R_se, Ramda_se, L1 = 0, L2 = 0;

            //배관 열저항
            
                R_pipe = Math.Log(((PipeD / 2 + PipeInsD) / 1000) / (PipeD / 2 / 1000)) / 2 / Math.PI / PipeIns_Ramda;
                Ramda_se = 5 + 0.15 * 5.67 / 100000000 * 4 * 1000;
                R_se = 1 / (Ramda_se * 2 * Math.PI * (PipeD / 2 + PipeInsD) / 1000);
                Psi_pipe = 1 / (R_pipe + R_se);     

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

                Qw_d[mth] = Math.Max(Psi_pipe * PipeL * (57.5 - theta_i[mth]) * dop_mth_avg[mth]*th_op_day_avg / 1000, 0);
                    if (double.IsNaN(Qw_d[mth])) { Qw_d[mth] = 0; }
                }

            //펌프
            string[][] Value2 = Program.DB.getValue(ProjNum, "User_Pump", "B효율,동력", "번호 = '" + Pump1 + "'");
            Pump.Clear();
            if (Value2.Length > 0)
            {
                for (int n = 0; n < Value2.Length; n++)
                {
                    String Num_pump; double A_pump; double B_pump; double V_pump; double Power_pump; double H_pump; double count_pump;
                    double Cp1, Cp2, Ppump, fhydr = 1, dP, f_dpm;
                    double[] Vz = new double[12], P_hydr = new double[12], fe = new double[12], e_hydr = new double[12], Wh_hydr = new double[12];
                    double theta;
                    Num_pump = Pump1;
                    B_pump = Convert.ToDouble(Value2[0][0]);
                    V_pump = Pump1Volume;
                    Power_pump = Convert.ToDouble(Value2[0][1]);
                    H_pump = Pump1Head;
                    count_pump = Pump1Count;
                    DHW_Pump pump1 = new DHW_Pump(Num_pump,B_pump, V_pump, Power_pump, H_pump, Pump1Count, Pump1Valve, Pump1Control); ;
                    Pump.Add(pump1);
                    string[][] Value_Control = Program.DB.getValue(DB.type.BaseDB_Heating, "펌프제어", "Cp1,Cp2", "펌프제어 = '" + Pump1Control + "'");
                    Cp1 = Convert.ToDouble(Value_Control[0][0]);
                    Cp2 = Convert.ToDouble(Value_Control[0][1]);
                    if (Pump1Valve == "있음")
                    {
                        fhydr = 1;
                    }
                    else
                    {
                        fhydr = 1.25;
                    }
                    if (Pump1 == null || Pump1 == "")
                    {
                        f_dpm = 1;
                    }
                    else
                    {
                        f_dpm = 0.45;
                    }
                    dP = H_pump * 1000 * 9.81;
                    for (int mth = 0; mth < 12; mth++)
                    {
                        Vz[mth] = Psi_pipe * PipeL * (57.5 - theta_i_h_set_avg) / (1.15 * 5 * 1000);
                        P_hydr[mth] = dP * Vz[mth] / 3600;
                        fe[mth] = (Power_pump / P_hydr[mth]) ;
                        e_hydr[mth] = fe[mth] * (Cp1 + Cp2) * 0.25 / 0.25;
                        Wh_hydr[mth] = P_hydr[mth] / 1000 * dop_mth_avg[mth] * th_op_day_avg;
                        Ww_d[mth] = Wh_hydr[mth] * e_hydr[mth];
                    }
                }
            }
        }
    
        public void Calc_Qh_s(string ProjNum)
        {
            
            double[] thetai = new double[12];
            if (Vs > 0)
            {
                if (StorageType == "2단 구분 축열탱크")
                {
                   
                }
                else if (StorageType == "전기 직접식")
                {
                    Qs_po_day = 0.29 + 0.019 * Math.Pow(Vs, 0.8);
                }
                else if(StorageType =="가스 직접식")
                {
                    Qs_po_day = 2.0 + 0.033 * Math.Pow(Vs, 1.1);
                }
                else
                {
                    if (Vs > 1000)
                    { Qs_po_day = 0.5 + 0.39 * Math.Pow(Vs, 0.35); }
                    else { Qs_po_day = 0.8 + 0.02 * Math.Pow(Vs, 0.77); }
                }
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
                Qw_s[mth] = (50 - thetai[mth]) / 45 * dop_mth_avg[mth] * Qs_po_day;
                if (double.IsNaN(Qw_s[mth])) { Qw_s[mth] = 0; }
                Qw_outg[mth] = Qwb_mth_sum[mth] + Qw_d[mth] + Qw_s[mth];
            }

            string[][] Value = Program.DB.getValue(ProjNum, "User_Pump", "동력", "번호 = '" + StoragePump + "'");
            string[][] Value2= null;
            if(SelectBoiler_split.Count>0 )
            { Value2 = Program.DB.getValue(ProjNum, "User_Boiler", "용량", "번호 = '" + SelectBoiler_split[0] + "'");  }
            else if (SelectHP_split.Count >0)
            {
                Value2 = Program.DB.getValue(ProjNum, "User_DHWHP", "급탕정격용량", "번호 = '" + SelectHP_split[0] + "'");
            }

            if (Value.Length > 0 && Value2!= null && Value2.Length > 0)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    double tPu = Qw_outg[mth] * 1.1 / Convert.ToDouble(Value2[0][0]);
                    Ww_s[mth] = Convert.ToDouble(Value[0][0]) * tPu / 1000;
                    if (double.IsNaN(Ww_s[mth])) { Ww_s[mth] = 0; }
                }
            }
        }

        public void LoadCalc_Solar(string ProjNum)
        {

            for (int k = 0; k < SelectSolar_split.Count; k++)
            {
                string[][] Solarvalue = Program.DB.getValue(ProjNum, "User_Solar", "번호,모듈면적,효율,열손실계수1차,열손실계수2차,입사각50도,유효열용량", "번호 ='" + SelectSolar_split[k] + "'");
                if (Solarvalue.Length > 0)
                {
                    DHW_Solar solar = new DHW_Solar(Solarvalue[0][0], Convert.ToDouble(Solarvalue[0][1]), Convert.ToDouble(Solarvalue[0][2]), Convert.ToDouble(Solarvalue[0][3]), Convert.ToDouble(Solarvalue[0][4]), Convert.ToDouble(Solarvalue[0][5]), Convert.ToDouble(Solarvalue[0][6]), Convert.ToDouble(SolarNum_split[k]), SolarDirection_split[k].ToString(), SolarDegree_split[k].ToString());
                    Calc_Solar(solar, ProjNum, SolarDirection_split[k].ToString(), SolarDegree_split[k].ToString());
                }
            }
        }

        public void Calc_Solar(DHW_Solar solar, string ProjNum, string direction, string degree)
        {
            double qsol_HN_d, dtheta_korr;
            double[] qsol_HN_mth= new double[12], eta = new double[12], qsol_mth = new double[12], Qsol_mth = new double[12], Qh_sol = new double[12], Ww_gen = new double[12];
            string[][] Solarvalue;
            double Ac;

            for (int mth = 0; mth < 12; mth++)
            {
                string[][] value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_전일사량", "일사량", "지역명 ='" + 지역[0][0] + "'and 방향='" + direction + "' and 각도 ='" + degree + "' and 기간 ='" + (mth + 1) + "월'");
                qsol_HN_d = Convert.ToDouble(value[0][0]);
                qsol_HN_mth[mth] = qsol_HN_d * dmth[mth] * 24 / 1000;

                string[][] value2 = Program.DB.querySQL(DB.type.BaseDB_HCneed, "Select Max(일사량) from 기후데이터_전일사량 where 지역명 = '" + 지역[0][0] + "' and 방향 = '" + direction + "' and 각도 = '" + degree + "'");

                Ac = Qw_outg[mth] * 2 * 1.03 * 1.03 / Convert.ToDouble(value2[0][0]) / 24 * 1000;
                if (solar.M_Area() * solar.M_Count() / Ac < 1)
                {
                    dtheta_korr = Math.Min(-20 + 20 * solar.M_Area() * solar.M_Count() / Ac, 0);
                }
                else
                {
                    dtheta_korr = Math.Min(-14 + 14 * solar.M_Area() * solar.M_Count() / Ac, 0);
                }
                value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_태양열", "온도차", "지역명 ='" + 지역[0][0] + "' and 방위='" + direction+ "' and 기간 ='" + (mth + 1) + "월'");
                eta[mth] = solar.eta() * solar._50() - solar.K1() * Convert.ToDouble(value[0][0]) / qsol_HN_d - solar.K2() * Convert.ToDouble(value[0][0]) * Convert.ToDouble(value[0][0]) / qsol_HN_d;
                if (eta[mth] < 0) { eta[mth] = solar.eta(); }
                qsol_mth[mth] = eta[mth] * qsol_HN_mth[mth];
                Qsol_mth[mth] = qsol_mth[mth] * solar.M_Area() * solar.M_Count() / 1.03 / 1.03;

                Qw_sol[mth] = Math.Min(Qsol_mth[mth], Qw_outg[mth]);

                Ww_gen[mth] = 0.025 * Qw_sol[mth];
            }
            for (int mth = 0; mth < 12; mth++)
            {
                Ww_g[mth] = Ww_g[mth] + Ww_gen[mth];
                Qw_outg[mth] = Qw_outg[mth] - Qw_sol[mth];
            }
            Save_Solar(ProjNum, solar.Num());
        }

        private void Save_Solar(string ProjNum, string SolarNum)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            if (프로젝트유형[0][1] == ProjNum)
            {
                string RESystemNum = "";
                bool cache = Program.DB.isCaching();
                Program.DB.UseCaches(false);
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
                    //열 생산
                    string MTH = (mth + 1).ToString() + "월";
                    string[][] result = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "번호", "번호 = '" + RESystemNum + "' AND " + "월 = '" + MTH + "' AND " + "생산소비 = '생산' AND " + "생산유형 = '열'");

                    if (result.Length > 0)
                    {
                        // === UPDATE ===
                        Program.DB.executeSQL(DB.type.ProjDB,
                            "UPDATE RESystem_Result SET " +
                            "프로젝트번호 = '" + 프로젝트유형[0][1] + "', " +
                            "프로젝트유형 = '" + 프로젝트유형[0][0] + "', " +
                            "급탕설비 = '" + DHWNum + "', " +
                            "신재생시스템 = '" + SolarNum + "', " +
                            "신재생시스템유형 = '태양열시스템', " +
                            "총에너지 = '" + Qw_sol[mth] + "' " +
                            "WHERE 번호 = '" + RESystemNum + "' AND " +
                            "월 = '" + MTH + "' AND " +
                            "생산소비 = '생산' AND " +
                            "생산유형 = '열'"
                        );
                    }
                    else
                    {
                        // === INSERT ===
                        Program.DB.executeSQL(DB.type.ProjDB,
                            "INSERT INTO RESystem_Result (" +
                            "프로젝트번호, 프로젝트유형, 번호, 월, " +
                            "급탕설비, 신재생시스템, 신재생시스템유형, 생산소비, 생산유형, 총에너지" +
                            ") VALUES (" +
                            "'" + 프로젝트유형[0][1] + "', " +
                            "'" + 프로젝트유형[0][0] + "', " +
                            "'" + RESystemNum + "', " +
                            "'" + MTH + "', " +
                            "'" + DHWNum + "', " +
                            "'" + SolarNum + "', " +
                            "'태양열시스템', " +
                            "'생산', " +
                            "'열', " +
                            "'" + Qw_sol[mth] + "'" +
                            ")"
                        );
                    }

                }
                for (int mth = 0; mth <= 11; mth++)
                {
                    //전기 소비
                    string MTH = (mth + 1).ToString() + "월";

                    string[][] result = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "번호", "번호 = '" + RESystemNum + "' AND " + "월 = '" + MTH + "' AND " + "생산소비 = '소비' AND " + "소비연료 = '전기'");

                    if (result.Length > 0)
                    {
                        // === UPDATE ===
                        Program.DB.executeSQL(DB.type.ProjDB,
                            "UPDATE RESystem_Result SET " +
                            "프로젝트번호 = '" + 프로젝트유형[0][1] + "', " +
                            "프로젝트유형 = '" + 프로젝트유형[0][0] + "', " +
                            "급탕설비 = '" + DHWNum + "', " +
                            "신재생시스템 = '" + SolarNum + "', " +
                            "신재생시스템유형 = '태양열시스템', " +
                            "총에너지 = '" + (0.025 * Qw_sol[mth]) + "' " +
                            "WHERE 번호 = '" + RESystemNum + "' AND " +
                            "월 = '" + MTH + "' AND " +
                            "생산소비 = '소비' AND " +
                            "소비연료 = '전기'"
                        );
                    }
                    else
                    {
                        // === INSERT ===
                        Program.DB.executeSQL(DB.type.ProjDB,
                            "INSERT INTO RESystem_Result (" +
                            "프로젝트번호, 프로젝트유형, 번호, 월, " +
                            "급탕설비, 신재생시스템, 신재생시스템유형, 생산소비, 소비연료, 총에너지" +
                            ") VALUES (" +
                            "'" + 프로젝트유형[0][1] + "', " +
                            "'" + 프로젝트유형[0][0] + "', " +
                            "'" + RESystemNum + "', " +
                            "'" + MTH + "', " +
                            "'" + DHWNum + "', " +
                            "'" + SolarNum + "', " +
                            "'태양열시스템', " +
                            "'소비', " +
                            "'전기', " +
                            "'" + (0.025 * Qw_sol[mth]) + "'" +
                            ")"
                        );
                    }
                }
                Program.DB.saveProject();
                Program.DB.UseCaches(cache);
            }
          
        }

        public void LoadCalc_FC(string ProjNum)
        {
            for (int n = 0; n < SelectFC_split.Count; n++)
            {
                string[][] Value = Program.DB.getValue(ProjNum, "User_FC", "번호, 명칭, 연료, 전기출력, 전기효율, 열출력, 열효율", "번호 = '" + SelectFC_split[n].ToString() + "'");
                if (Value.Length > 0)
                {
                    int FC_nea = Convert.ToInt16(FCNum_split[n]);
                    double power_el = Convert.ToDouble(Value[0][3]);
                    double eta_el = Convert.ToDouble(Value[0][4]) / 100;
                    double power_th = Convert.ToDouble(Value[0][5]);
                    double eta_th = Convert.ToDouble(Value[0][6]) / 100;
                    double eta_tot = eta_el + eta_th;

                    double Pfc_th = power_th * FC_nea;
                    double Pfc_el = power_el * FC_nea;
                    Calc_FC(ProjNum, SelectFC_split[n].ToString(), Pfc_th, Pfc_el, eta_th, eta_el, eta_tot, FCElecInstall_split[n].ToString(), FCElecHeat_split[n].ToString(), FC_nea);
                }
            }
        }

        private void Calc_FC(string ProjNum, string FCNum, double Pfc_th, double Pfc_el, double eta_th, double eta_el, double eta_tot, string FCElecInstall, string FCElecHeat, int FC_nea)
        {
            double top = 0;
            double Pth_min = 0, Pls_sb = 0, Pth_sb = 0, Pel_out_sb = 0, Paux_sb = 0, Ppilot = 0;
            double[] QCHW_gen_out = new double[12];
            double[] dop = new double[12], Pth_gen_out = new double[12];
            double[] Eth_gen_out_h = new double[12], Eth_gen_out_w = new double[12];
            double[] Pel_gen_out = new double[12];
            double Pgen_ls_sb = 0, Pgen_in_chp = 0, Pgen_ls_chp = 0;
            double[] pgen_ls = new double[12], Qgen_ls = new double[12];
            double[] Pgen_in = new double[12];
           
            for (int mth = 0; mth < 12; mth++)
            {
               
                QCHW_gen_out[mth] = Qw_outg[mth];

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
                Eth_gen_out_h[mth] = Eth_gen_out[mth] * Qw_outg[mth] / QCHW_gen_out[mth];
            }
            for (int mth = 0; mth < 12; mth++)
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

                if (FCElecHeat == "전기와 열")
                {
                    Qw_outg[mth] = Qw_outg[mth] - Eth_gen_out[mth];
                }
            }
            Boolean Now_Check = true;
            if (ProjNum == 프로젝트번호[0][0])
            { Now_Check = true; }
            else
            { Now_Check = false; }
            if (Now_Check)
            {
                Save_FC(ProjNum, FCNum, Eth_gen_out_h, Eth_gen_out_w);
            }
        }
        private void Save_FC(string ProjNum,  string FCNum, double[] Eth_gen_out_h, double[] Eth_gen_out_w)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            if (프로젝트유형[0][1] == ProjNum)
            {
                bool cache = Program.DB.isCaching();
                Program.DB.UseCaches(false);
                string RESystemNum = "";
                string[][] value = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "번호", "급탕설비='" + DHWNum + "' and 신재생시스템='" + FCNum + "'");
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
                    //열 생산
                    string MTH = (mth + 1).ToString() + "월";
                    string[][] result = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "번호", "번호 = '" + RESystemNum + "' AND " + "월 = '" + MTH + "' AND " + "생산소비 = '생산' AND 생산유형 = '열'");
                    if (result.Length > 0)
                    {
                        Program.DB.executeSQL(DB.type.ProjDB,
                            "UPDATE RESystem_Result SET " +
                            "프로젝트번호 = '" + 프로젝트유형[0][1] + "', " +
                            "프로젝트유형 = '" + 프로젝트유형[0][0] + "', " +
                            "급탕설비 = '" + DHWNum + "', " +
                            "신재생시스템 = '" + FCNum + "', " +
                            "신재생시스템유형 = '연료전지', " +
                            "총에너지 = '" + Eth_gen_out[mth] + "', " +
                            "급탕 = '" + Eth_gen_out_w[mth] + "' " +
                            "WHERE 번호 = '" + RESystemNum + "' AND " +
                            "월 = '" + MTH + "' AND " +
                            "생산소비 = '생산' AND " +
                            "생산유형 = '열'"
                            );
                    }
                    else
                    {
                        Program.DB.executeSQL(DB.type.ProjDB,
                            "INSERT INTO RESystem_Result (" +
                            "프로젝트번호, 프로젝트유형, 번호, 월, " +
                            "급탕설비, 신재생시스템, 신재생시스템유형, 생산소비, 생산유형, " +
                            "총에너지, 급탕" +
                            ") VALUES (" +
                            "'" + 프로젝트유형[0][1] + "', " +
                            "'" + 프로젝트유형[0][0] + "', " +
                            "'" + RESystemNum + "', " +
                            "'" + MTH + "', " +
                            "'" + DHWNum + "', " +
                            "'" + FCNum + "', " +
                            "'연료전지', " +
                            "'생산', " +
                            "'열', " +
                            "'" + Eth_gen_out[mth] + "', " +
                            "'" + Eth_gen_out_w[mth] + "'" +
                            ")");
                    }

                }
                for (int mth = 0; mth <= 11; mth++)
                {
                    //전기 생산 
                    string MTH = (mth + 1).ToString() + "월";
                    string[][] result = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "번호", "번호 = '" + RESystemNum + "' AND " + "월 = '" + MTH + "' AND " + "생산소비 = '생산' AND " + "생산유형 = '전기'");

                    if (result.Length > 0)
                    {
                        // UPDATE
                        Program.DB.executeSQL(DB.type.ProjDB,
                            "UPDATE RESystem_Result SET " +
                            "프로젝트번호 = '" + 프로젝트유형[0][1] + "', " +
                            "프로젝트유형 = '" + 프로젝트유형[0][0] + "', " +
                            "급탕설비 = '" + DHWNum + "', " +
                            "신재생시스템 = '" + FCNum + "', " +
                            "신재생시스템유형 = '연료전지', " +
                            "총에너지 = '" + Eel_gen_out[mth] + "' " +
                            "WHERE 번호 = '" + RESystemNum + "' AND " +
                            "월 = '" + MTH + "' AND " +
                            "생산소비 = '생산' AND " +
                            "생산유형 = '전기'"
                        );
                    }
                    else
                    {
                        // INSERT
                        Program.DB.executeSQL(DB.type.ProjDB,
                            "INSERT INTO RESystem_Result (" +
                            "프로젝트번호, 프로젝트유형, 번호, 월, " +
                            "급탕설비, 신재생시스템, 신재생시스템유형, 생산소비, 생산유형, " +
                            "총에너지" +
                            ") VALUES (" +
                            "'" + 프로젝트유형[0][1] + "', " +
                            "'" + 프로젝트유형[0][0] + "', " +
                            "'" + RESystemNum + "', " +
                            "'" + MTH + "', " +
                            "'" + DHWNum + "', " +
                            "'" + FCNum + "', " +
                            "'연료전지', " +
                            "'생산', " +
                            "'전기', " +
                            "'" + Eel_gen_out[mth] + "'" +
                            ")"
                        );
                    }

                }
                for (int mth = 0; mth <= 11; mth++)
                {
                    //가스 소비 
                    string MTH = (mth + 1).ToString() + "월";
                    string[][] result = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "번호", "번호 = '" + RESystemNum + "' AND " + "월 = '" + MTH + "' AND " + "생산소비 = '소비' AND " + "소비연료 = '가스'");

                    if (result.Length > 0)
                    {
                        // === UPDATE ===
                        Program.DB.executeSQL(DB.type.ProjDB,
                            "UPDATE RESystem_Result SET " +
                            "프로젝트번호 = '" + 프로젝트유형[0][1] + "', " +
                            "프로젝트유형 = '" + 프로젝트유형[0][0] + "', " +
                            "급탕설비 = '" + DHWNum + "', " +
                            "신재생시스템 = '" + FCNum + "', " +
                            "신재생시스템유형 = '연료전지', " +
                            "총에너지 = '" + Egen_in[mth] + "' " +
                            "WHERE 번호 = '" + RESystemNum + "' AND " +
                            "월 = '" + MTH + "' AND " +
                            "생산소비 = '소비' AND " +
                            "소비연료 = '가스'"
                        );
                    }
                    else
                    {
                        // === INSERT ===
                        Program.DB.executeSQL(DB.type.ProjDB,
                            "INSERT INTO RESystem_Result (" +
                            "프로젝트번호, 프로젝트유형, 번호, 월, " +
                            "급탕설비, 신재생시스템, 신재생시스템유형, 생산소비, 소비연료, 총에너지" +
                            ") VALUES (" +
                            "'" + 프로젝트유형[0][1] + "', " +
                            "'" + 프로젝트유형[0][0] + "', " +
                            "'" + RESystemNum + "', " +
                            "'" + MTH + "', " +
                            "'" + DHWNum + "', " +
                            "'" + FCNum + "', " +
                            "'연료전지', " +
                            "'소비', " +
                            "'가스', " +
                            "'" + Egen_in[mth] + "'" +
                            ")"
                        );
                    }
                }
                Program.DB.saveProject();
                Program.DB.UseCaches(cache); ;
            }
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
                    double Power = Convert.ToDouble(Value[0][4]);
                    double eta_Pn = Convert.ToDouble(Value[0][5]) / 100 * 0.95;
                    double eta_Pint = Convert.ToDouble(Value[0][6]) / 100 * 0.95;
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
            if (Value.Length > 0)
            {
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
                double[] tw_Pn_day = new double[12]; //나중에 급탕과 연결 해야 함 
                double[] Pd_in = new double[12];
                double beta_gen_pint = 0.3, qp0_theta;
                double[] Qh_gen_mth = new double[12];

                for (int mth = 0; mth < 12; mth++)
                {
                    eta_pn_w[mth] = eta_Pn + K * (50 - 55);
                    qp0_theta = Math.Max(qP0_70 * (50 - theta_ih_avg[mth]) / 50, 0);
                    tw_Pn_day[mth] = Qw_outg[mth] / (Power * dop_mth_avg[mth]);
                    Qw_gen_day[mth] = (fHN_HI - eta_pn_w[mth]) / eta_pn_w[mth] * Qw_outg[mth] / (tw_Pn_day[mth] * dop_mth_avg[mth]);
                    Qw_gen_p0_day[mth] = qp0_theta / eta_pn_w[mth] * (th_op_day_avg - tw_Pn_day[mth]) * fHN_HI;
                    Qw_gen[mth] = Qw_gen_day[mth] * tw_Pn_day[mth] * dop_mth_avg[mth];
                    Qw_f[mth] = Qw_outg[mth] + Qw_gen[mth];
                }

                double Paux_Pn, Paux_Pint;
                Paux_Pn = Math.Min((G_Pn + H_Pn * Math.Pow(Power, n_Pn)) / 1000, W / 1000);
                Paux_Pint = Math.Min((G_Pint + H_Pint * Math.Pow(Power, n_Pint)) / 1000, W / 1000);
                double[] Ph_gen_aux = new double[12], Wh_g_i = new double[12];
                for (int mth = 0; mth < 12; mth++)
                {

                    Wh_g_i[mth] = Paux_Pn * tw_Pn_day[mth] * dop_mth_avg[mth];
                    Ww_g[mth] += Wh_g_i[mth];
                }
            }
        }
        public void LoadCalc_HP(string ProjNum)
        {
            for (int k = 0; k < SelectHP_split.Count; k++)
            {
                double Pi_gen_combi_corr; double Pi_gen_sng_corr; double COPw_sng_corr; double COPw_combi_corr;
                string[][] value = Program.DB.getValue(ProjNum, "User_DHWHP", "급탕정격용량,급탕정격COP", "번호='" + SelectHP_split[0] + "'");
                if (value.Length > 0)
                {
                    Pi_gen_combi_corr = Convert.ToDouble(value[0][0]);
                    Pi_gen_sng_corr = Convert.ToDouble(value[0][0]);
                    COPw_sng_corr = Convert.ToDouble(value[0][1]);
                    COPw_combi_corr = Convert.ToDouble(value[0][1]);
                    Carrier = "전기";
                    Calc_HP(Pi_gen_combi_corr, Pi_gen_sng_corr, COPw_sng_corr, COPw_combi_corr);
                }
            }
        }
        public void Calc_HP(double Pi_gen_combi_corr, double Pi_gen_sng_corr, double COPw_sng_corr, double COPw_combi_corr)
        {
            double theta_upper_hp = 60;
            double[] Qw_outg_bu = new double[12], Qw_outg_bu_t = new double[12], kbu_w = new double[12];
            double[] tw_gen_prel_combi = new double[12], top_max = new double[12], tw_gen_op_combi = new double[12], tw_gen_op_sng = new double[12];

            for (int mth = 0; mth < 12; mth++)
            {
                Qw_outg_bu[mth] = Math.Max(Qw_outg[mth] * (SL - theta_upper_hp) / (SL - 10), 0);
                tw_gen_prel_combi[mth] = (Qw_outg[mth] - Qw_outg_bu[mth]) / Pi_gen_combi_corr;
                top_max[mth] = dop_mth_avg[mth] * th_op_day_avg;
                if (top_max[mth] >= tw_gen_prel_combi[mth])
                { tw_gen_op_combi[mth] = tw_gen_prel_combi[mth]; }
                else
                {
                    tw_gen_op_combi[mth] = Math.Max((Qw_outg[mth] - Qw_outg_bu[mth] - Pi_gen_sng_corr * top_max[mth]) / Pi_gen_combi_corr, 0);
                }
                tw_gen_op_sng[mth] = Math.Min((Qw_outg[mth] - Qw_outg_bu[mth] - Pi_gen_combi_corr * tw_gen_op_combi[mth]) / Pi_gen_sng_corr, top_max[mth]);
                Qw_outg_bu_t[mth] = Math.Max(0, Qw_outg[mth] - Qw_outg_bu[mth] - Pi_gen_sng_corr * tw_gen_op_sng[mth] - Pi_gen_combi_corr * tw_gen_op_combi[mth]);
                Qw_f[mth] = Pi_gen_combi_corr * tw_gen_op_sng[mth] / COPw_sng_corr + Pi_gen_combi_corr * tw_gen_op_combi[mth] / COPw_combi_corr;
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
                    Calc_Qw_DH(Num, Power, SL_1, SL_2);
                }
            }

            for (int mth = 0; mth < 12; mth++)
            {
                Qw_f[mth] = Qw_outg[mth] + Qw_gen[mth];
            }
        }
        public void Calc_Qw_DH(String Num, double Power, double theta_prime, double theta_sek)
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
                Qh_outg_a += Qw_outg[mth];
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
                Qh_gen_DH[mth] = H_DS * Qw_outg[mth] / Qh_outg_a * (theta_DS - theta_i[mth]);
            }
            for (int mth = 0; mth < 12; mth++)
            {
                Qw_gen[mth] = Qw_gen[mth] + Qh_gen_DH[mth];
            }
        }
        public void nan()
        {
            for(int mth =0; mth < 12; mth++)
            {
                if (double.IsNaN(Qw_d[mth]) ) { Qw_d[mth] = 0; }
                if (double.IsNaN(Qw_s[mth])) { Qw_s[mth] = 0; }
                if (double.IsNaN(Qw_gen[mth])) { Qw_gen[mth] = 0; }
                if (double.IsNaN(Qw_f[mth]) ) { Qw_f[mth] = 0; }

                if (double.IsNaN(Ww_d[mth])) { Ww_d[mth] = 0; }
                if (double.IsNaN(Ww_s[mth])) { Ww_s[mth] = 0; }
                if (double.IsNaN(Ww_g[mth])) { Ww_g[mth] = 0; }

                if (double.IsNaN(Qw_gen_day[mth])){ Qw_gen_day[mth] = 0; }
                if (double.IsNaN(eta_pn_w[mth])) { eta_pn_w[mth]=0; }

                if (Qw_d[mth] < 0) { Qw_d[mth] = 0; }
                if (Qw_s[mth] < 0) { Qw_s[mth] = 0; }
                if (Qw_gen[mth] < 0) { Qw_gen[mth] = 0; }
                if (Qw_f[mth] < 0) { Qw_f[mth] = 0; }

                if (Ww_d[mth] < 0) { Ww_d[mth] = 0; }
                if (Ww_s[mth] < 0) { Ww_s[mth] = 0; }
                if (Ww_g[mth] < 0) { Ww_g[mth] = 0; }
            }
          
        }
    }

    public class DHW_Pump
    {
        String Num_pump; double B_pump; double V_pump; double Power_pump; double H_pump; double count_pump; String Valve_pump; String Control_pump;
        public DHW_Pump(String Num, double B, double V, double Power, double H, double count, String Valve, String Control)
        {
            this.Num_pump = Num;
            this.B_pump = B;
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
        public double B()
        {
            return this.B_pump;
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

    public class DHW_Boiler
    {
        String Num_Boiler; string combi_Boiler; String Carrier_Boiler; String Type_Boiler; double Power_Boiler; double eta_Pn_Boiler; double eta_Pint_Boiler; double W_Boiler; double W_0_Boiler; double count_Boiler;
        double K_Boiler, L_Boiler, E_Boiler, F_Boiler, G_pn_Boiler, H_pn_Boiler, n_pn_Boiler, G_pint_Boiler, H_pint_Boiler, n_pint_Boiler;
        public DHW_Boiler(String Num, String Combi, String Carrier, String Type, double Power, double eta_Pn, double eta_Pint, double W, double W_0, double count, double K, double L, double E, double F, double G_pn, double H_pn, double n_pn, double G_pint, double H_pint, double n_pint_Boiler)
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

    public class DHW_Solar
    {
        String Solar_Num, Solar_Direction, Solar_Degree;
        double SolarM_Area, Solar_eta, Solar_K1, Solar_K2, Solar_50, Solar_C, SolarM_Count; 
        public DHW_Solar(String Num, double M_Area, double eta, double K1, double K2, double _50, double C, double M_Count, String Direction, String Degree)
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
        public double C() { return this.Solar_C;}
        public double M_Count() {return this.SolarM_Count;}
        public string Direction() { return this.Solar_Direction;}
        public string Degree() { return this.Solar_Degree;} 


    }
}