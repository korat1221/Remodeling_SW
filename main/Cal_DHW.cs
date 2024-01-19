using System;
using System.Collections;
using System.Security.Policy;

namespace main
{
    internal class Cal_DHW
    {
        String DHWNum, DHWName; String SelectZone_nonsplit;
        String SystemLoacation, SLRL, Complex, MainSystem, Sub1System, Sub2System;
        String SelectBoiler_nonsplit, BoilerNum_nonsplit;
        String SelectSolar_nonsplit, SolarNum_nonsplit, SolarDirection_nonsplit, SolarDegree_nonsplit;      
        String PumpUse, PumpMethod, Pump1, Pump2, Pump1Valve, Pump2Valve, Pump1Control, Pump2Control; int Pump1Count, Pump2Count;
        public ArrayList Pump = new ArrayList();
        String StorageUse, StoragePumpUse, StoragePump,StorageType; public double Vs;
        String[] SystemType = { "보일러","지역난방", "태양열시스템" };
        double PipeD, PipeInsD, PipeIns_Ramda;
        String PipeIns;
        int ZoneCount;
        ArrayList SelectZone_split = new ArrayList(); ArrayList SelectBoiler_split = new ArrayList(); ArrayList BoilerNum_split = new ArrayList();
        public double[] Qwb_mth_sum = new double[12]; public double[] theta_ih_avg = new double[12]; public double[] theta_e = new double[12]; public double[] theta_u = new double[12];
        public double Qw_a_sum, th_op_day_avg, theta_i_h_set_avg; public double[] dop_mth_avg = new double[12];
        double SL, RL;
        double[] dmth = new double[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public double[] Qw_d = new double[12], Qw_s = new double[12], Qw_gen = new double[12], Qw_outg = new double[12], Qw_f = new double[12];
        public double[] Ww_d = new double[12], Ww_s = new double[12], Ww_g = new double[12];
        public double Psi_pipe, L, Qs_po_day;
        public double[] Qw_gen_day = new double[12], Qw_gen_p0_day = new double[12], eta_pn_w = new double[12];
        public String Carrier; 
        ArrayList SelectSolar_split = new ArrayList(); ArrayList SolarNum_split = new ArrayList(); ArrayList SolarDirection_split = new ArrayList(); ArrayList SolarDegree_split = new ArrayList();



        string[][] 지역, 외기온도;
        public Cal_DHW(String Num)
        {
            this.DHWNum = Num;
            try
            {
                지역 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
                외기온도 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_온도습도", "기간,온도", "지역명 ='" + 지역[0][0] + "'");
                int i = -1;
                while (++i < 12)
                {
                    theta_e[i] = Convert.ToDouble(외기온도[i][1]);
                    theta_u[i] = theta_ih_avg[i] - 0.8 * (theta_ih_avg[i] - theta_e[i]);
                }
            }
            catch { }

        }

        public void Load_Zonedata()
        {
            double[,] Qwb_mth; double[,] theta_ih;double[,] dop_mth; double[] th_op_day;  double[] Qwb_a; double[] theta_i_h_set;

            //존 정보 불러오기
            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "명칭,존", "번호 = '" + DHWNum + "'");
                DHWName = Value[0][0];
                SelectZone_nonsplit = Value[0][1];
                Split_Zone(SelectZone_nonsplit);
                Qwb_mth = new double[SelectZone_split.Count, 12];
                theta_ih = new double[SelectZone_split.Count, 12];
                Qwb_a = new double[SelectZone_split.Count];
                dop_mth = new double[SelectZone_split.Count, 12];
                th_op_day = new double[SelectZone_split.Count];
                theta_i_h_set = new double[SelectZone_split.Count];
                double[] dop_a = new double[SelectZone_split.Count];
                for (int n = 0; n < SelectZone_split.Count; n++)
                {
                    Zone zone = Program.CALC.getZone(SelectZone_split[n].ToString());
                    if (zone != null)
                    {
                       
                        for (int mth = 0; mth < 12; mth++)
                        { 
                            dop_mth[n, mth] = zone.dwd_mth[mth];
                            dop_a[n] += zone.dwd_mth[mth];
                        }
                        for (int mth = 0; mth < 12; mth++)
                        {
                            string[][] Qwb_day = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "일일급탕요구량", "존번호 = '" + SelectZone_split[n].ToString() + "'");

                            Qwb_mth[n, mth] = Convert.ToDouble(Qwb_day[0][0])* dop_a[n]* dmth[mth]/365 * (-0.02 * theta_e[mth] +1.25);
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
            catch { }
        }

        //일반정보 불러오기 
        public void Load_DHWGeneral()
        {
            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "설치위치,공급환수온도,복합설비유무,주요설비,보조설비1,보조설비2", "번호 = '" + DHWNum + "'");

                SystemLoacation = Value[0][0];
                SLRL = Value[0][1];
                if (SLRL != null && SLRL != "")
                {
                    string[][] Value2 = Program.DB.getValue(DB.type.BaseDB_Heating, "공급환수온도", "공급온도,환수온도", "공급환수온도 = '" + SLRL + "'");
                    SL = Convert.ToDouble(Value2[0][0]);
                    RL = Convert.ToDouble(Value2[0][1]);
                }

                Complex = Value[0][2];
                MainSystem = Value[0][3];
                Sub1System = Value[0][4];
                Sub2System = Value[0][5];
            }
            catch { }
        }

        //보일러 정보 불러오기
        public void Load_Boiler()
        {
            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "보일러종류,보일러대수", "번호 = '" + DHWNum + "'");
                SelectBoiler_nonsplit = Value[0][0];
                Split_Boiler(SelectBoiler_nonsplit);

                BoilerNum_nonsplit = Value[0][1];
                Split_BoilerNum(BoilerNum_nonsplit);
            }
            catch { }

        }
        public void Load_Solar()
        {
            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "태양열번호,모듈개수,모듈방위,모듈기울기", "번호 = '" + DHWNum + "'");
                SelectSolar_nonsplit = Value[0][0];
                Split_Solar(SelectSolar_nonsplit);

                SolarNum_nonsplit = Value[0][1];
                Split_SolarNum(SolarNum_nonsplit);

                SolarDirection_nonsplit = Value[0][2];
                Split_SolarDirection(SolarDirection_nonsplit);

                SolarDegree_nonsplit = Value[0][3];
                Split_SolarDegree(SolarDegree_nonsplit);
            }
            catch { }
        }
        private void Split_Solar(String nonSplit)
        {
            if (nonSplit != null)
            {
                if (nonSplit.Contains('+'))
                {
                    string[] token = nonSplit.Split('+');
                    SelectSolar_split.Clear();
                    foreach (var item in token)
                    {
                        SelectSolar_split.Add(item.ToString());
                    }
                }
            }
            else { return; }

        }
        private void Split_SolarNum(String nonSplit)
        {
            if (nonSplit != null)
            {
                if (nonSplit.Contains('+'))
                {
                    string[] token = nonSplit.Split('+');
                    SolarNum_split.Clear();
                    foreach (var item in token)
                    {
                        SolarNum_split.Add(item.ToString());
                    }
                }
            }
            else { return; }

        }
        private void Split_SolarDirection(String nonSplit)
        {
            if (nonSplit != null)
            {
                if (nonSplit.Contains('+'))
                {
                    string[] token = nonSplit.Split('+');
                    SolarDirection_split.Clear();
                    foreach (var item in token)
                    {
                        SolarDirection_split.Add(item.ToString());
                    }
                }
            }
            else { return; }

        }
        private void Split_SolarDegree(String nonSplit)
        {
            if (nonSplit != null)
            {
                if (nonSplit.Contains('+'))
                {                   
                    string[] token = nonSplit.Split('+');
                    SolarDegree_split.Clear();
                    foreach (var item in token)
                    {
                        SolarDegree_split.Add(item.ToString());
                    }
                }
            }
            else { return; }

        }
        public void Load_PumpData()
        {


            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "펌프유무,펌프방식,펌프1종류,펌프2종류,펌프1밸브,펌프2밸브,펌프1제어,펌프2제어,펌프1대수,펌프2대수", "번호 = '" + DHWNum + "'");

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
            }
            catch { }

        }
       

        public void Load_StorageData()
        {
            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "축열유무,축열펌프유무,축열펌프,축열용량,축열유형", "번호 = '" + DHWNum + "'");
                StorageUse = Value[0][0];
                StoragePumpUse = Value[0][1];
                StoragePump = Value[0][2];
                if (Value[0][3] != null && Value[0][3] != "")
                {
                    Vs = Convert.ToDouble(Value[0][3]);
                }
                StorageType = Value[0][4];
            }
            catch { }

        }

        public void Load_PipeData()
        {
            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "배관관경,배관보온두께,보온열전도율,배관보온재", "번호 = '" + DHWNum + "'");
                PipeD = Convert.ToDouble(Value[0][0]);
                PipeInsD = Convert.ToDouble(Value[0][1]);
                PipeIns_Ramda = Convert.ToDouble(Value[0][2]);
                PipeIns = Value[0][3];
            }
            catch { }
        }
       
        private void Split_Zone(String nonSplit)
        {
            if (nonSplit != null)
            {
                if (nonSplit.Contains("+"))
                {
                    string[] token = nonSplit.Split('+');
                    SelectZone_split.Clear();
                    foreach (var item in token)
                    {
                        SelectZone_split.Add(item.ToString());
                    }
                }
                else
                {
                    SelectZone_split.Clear();
                    SelectZone_split.Add(nonSplit);
                }
            }
        }
        private void Split_Boiler(String nonSplit)
        {
            if (nonSplit != null)
            {
                if (nonSplit.Contains(','))
                {
                    string[] token = nonSplit.Split('+');
                    SelectBoiler_split.Clear();
                    foreach (var item in token)
                    {
                        SelectBoiler_split.Add(item.ToString());
                    }

                    string[][] BoilerName = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "명칭", "번호 = '" + SelectBoiler_split[0].ToString() + "'");
                }
                else
                {
                    SelectBoiler_split.Clear();
                    SelectBoiler_split.Add(nonSplit);
                    string[][] BoilerName = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "명칭", "번호 = '" + SelectBoiler_split[0].ToString() + "'");
                }
            }
        }
        private void Split_BoilerNum(String nonSplit)
        {
            if (nonSplit != null)
            {
                if (nonSplit.Contains(','))
                {
                    string[] token = nonSplit.Split('+');
                    BoilerNum_split.Clear();
                    foreach (var item in token)
                    {
                        BoilerNum_split.Add(item.ToString());
                    }
                }
                else
                {
                    BoilerNum_split.Clear();
                    BoilerNum_split.Add(nonSplit);
                }
            }

        }

       

        public void Calc_Qd()
        {
            

            double R_pipe, R_se, Ramda_se, L1 = 0, L2 = 0;

            //배관 열저항
            
                R_pipe = Math.Log(((PipeD / 2 + PipeInsD) / 1000) / (PipeD / 2 / 1000)) / 2 / Math.PI / PipeIns_Ramda;
                Ramda_se = 5 + 0.15 * 5.67 / 100000000 * 4 * 1000;
                R_se = 1 / (Ramda_se * 2 * Math.PI * (PipeD / 2 + PipeInsD) / 1000);
                Psi_pipe = 1 / (R_pipe + R_se);
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "양정", "번호 = '" + Pump1 + "'");
                if (Value.Length > 0)
                {
                    L1 = Convert.ToDouble(Value[0][0]);
                }
                Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "양정", "번호 = '" + Pump2 + "'");
                if (Value.Length > 0)
                {
                    L2 = Convert.ToDouble(Value[0][0]);
                }
                L = L1 + L2;
                for (int mth = 0; mth < 12; mth++)
                {

                    Qw_d[mth] = Math.Max(Psi_pipe * L * (57.5 - theta_ih_avg[mth]) * dop_mth_avg[mth]*th_op_day_avg / 1000, 0);
                    if (double.IsNaN(Qw_d[mth])) { Qw_d[mth] = 0; }
                }
            
            //펌프
            


                try
                {
                    string[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "A효율,B효율,유량,동력,양정,대수", "번호 = '" + Pump1 + "'");
                    Pump.Clear();
                    for (int n = 0; n < Value2.Length; n++)
                    {
                        String Num_pump; double A_pump; double B_pump; double V_pump; double Power_pump; double H_pump; double count_pump;
                        double Cp1, Cp2, Ppump, fhydr = 1, dP, f_dpm;
                        double[] Vz = new double[12], P_hydr = new double[12], fe = new double[12], e_hydr = new double[12], Wh_hydr = new double[12];
                        double theta;
                        Num_pump = Pump1;
                        A_pump = Convert.ToDouble(Value2[0][0]);
                        B_pump = Convert.ToDouble(Value2[0][1]);
                        V_pump = Convert.ToDouble(Value2[0][2]);
                        Power_pump = Convert.ToDouble(Value2[0][3]);
                        H_pump = Convert.ToDouble(Value2[0][4]);
                        count_pump = Convert.ToDouble(Value2[0][5]);
                        DHW_Pump pump1 = new DHW_Pump(Num_pump, A_pump, B_pump, V_pump, Power_pump, H_pump, Pump1Count, Pump1Valve, Pump1Control); ;
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
                            Vz[mth] = Psi_pipe * L * (57.5 - theta_i_h_set_avg) / (1.15 * 5 * 1000);
                            P_hydr[mth] = dP * Vz[mth] / 3600;
                            fe[mth] = (1.25 + 200 / P_hydr[mth]) * 2;
                            e_hydr[mth] = fe[mth] * (Cp1 + Cp2 ) * 0.25 / 0.25;
                            Wh_hydr[mth] = P_hydr[mth] / 1000 * dop_mth_avg[mth] * th_op_day_avg;
                            Ww_d[mth] = Wh_hydr[mth] * e_hydr[mth];
                        }
                    }
                    
                }
                catch { }

            
        }
    
        public void Calc_Qh_s()
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

            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "동력", "번호 = '" + StoragePump + "'");
                string[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "용량", "번호 = '" + SelectBoiler_split[0] + "'"); //수정 필요 
                for (int mth = 0; mth < 12; mth++)
                {
                    double tPu = Qw_outg[mth] * 1.1 / Convert.ToDouble(Value2[0][0]); 
                    Ww_s[mth] = Convert.ToDouble(Value[0][0]) * tPu / 1000;
                    if (double.IsNaN(Ww_s[mth])) { Ww_s[mth] = 0; }
                }
            }
            catch { }
        }
     
        public void Calc_Qh_gen_Boiler()
        {
            for (int n = 0; n < SelectBoiler_split.Count; n++)
            {
                try
                {
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "번호,난방급탕,연료,Type,용량,전부하효율,부분부하효율,소비전력,대기전력", "번호 = '" + SelectBoiler_split[n] + "'");
                    String Num = Value[0][0];
                    String Combi = Value[0][1];
                    Carrier = Value[0][2];
                    String Type = Value[0][3];
                    double Power = Convert.ToDouble(Value[0][4]);
                    double eta_Pn = Convert.ToDouble(Value[0][5]) / 100  *0.95;
                    double eta_Pint = Convert.ToDouble(Value[0][6]) / 100 * 0.95;
                    double W = Convert.ToDouble(Value[0][7]);
                    double W_0 = Convert.ToDouble(Value[0][8]);
                    double count = Convert.ToDouble(BoilerNum_split[n]);
                    Value = Program.DB.getValue(DB.type.BaseDB_Heating, "보일러", "온도보정계수K,온도보정계수L,대기상태열손실E,대기상태열손실F,보조설비G_Pn,보조설비H_Pn,보조설비n_Pn,보조설비G_Pint,보조설비H_Pint,보조설비n_Pint", "종류 = '" + Type + "'");
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
                    double[] tw_Pn_day = new double[12] ; //나중에 급탕과 연결 해야 함 
                    double[] Pd_in = new double[12];
                    double beta_gen_pint = 0.3, qp0_theta;
                    double[] Qh_gen_mth = new double[12];

                    for (int mth = 0; mth < 12; mth++)
                    {
                        
                        eta_pn_w[mth] = eta_Pn + K * (50-55);
                        qp0_theta = Math.Max(qP0_70 * (50 - theta_ih_avg[mth]) / 50, 0);
                        tw_Pn_day[mth] = Qw_outg[mth] / (Power * dop_mth_avg[mth]);
                        Qw_gen_day[mth] = (fHN_HI - eta_pn_w[mth]) / eta_pn_w[mth] * Qw_outg[mth] / (tw_Pn_day[mth] * dop_mth_avg[mth]);
                        Qw_gen_p0_day[mth] = qp0_theta / eta_pn_w[mth]*(th_op_day_avg - tw_Pn_day[mth])*fHN_HI;
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
                catch { }

            }
        }
        public void Calc_Solar()
        {
            double qsol_HN_d, dtheta_korr;
            double[] qsol_HN_mth= new double[12], eta = new double[12], qsol_mth = new double[12], Qsol_mth = new double[12], Qw_sol = new double[12], Qh_sol = new double[12], Ww_gen = new double[12];
            string[][] Solarvalue;
            double Ac; 

            for (int  k = 0; k < SelectSolar_split.Count; k++)
            {
                Solarvalue = Program.DB.getValue(DB.type.ProjDB, "User_Solar", "번호,모듈면적,효율,열손실계수1차,열손실계수2차,입사각50도,유효열용량", "번호 ='" + SelectSolar_split[k] + "'");
                DHW_Solar solar = new DHW_Solar(Solarvalue[0][0], Convert.ToDouble(Solarvalue[0][1]), Convert.ToDouble(Solarvalue[0][2]), Convert.ToDouble(Solarvalue[0][3]), Convert.ToDouble(Solarvalue[0][4]), Convert.ToDouble(Solarvalue[0][5]), Convert.ToDouble(Solarvalue[0][6]), Convert.ToDouble(SolarNum_split[k]), SolarDirection_split[k].ToString(), SolarDegree_split[k].ToString());
           
                for (int mth = 0; mth < 12; mth++)
                {
                   string[][] value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_전일사량", "일사량", "지역명 ='" + 지역[0][0] + "'방향='" + SolarDirection_split[k] + "' and 각도 ='" + SolarDegree_split[k] + "' and 기간 ='"+mth+1+"월'");
                    qsol_HN_d = Convert.ToDouble(value[0][0]);
                    qsol_HN_mth[mth] = qsol_HN_d * dmth[mth] * 24 / 1000;

                    string[][] value2 = Program.DB.querySQL(DB.type.BaseDB_HCneed, "Select Max(일사량) from 기후데이터_전일사량 where 지역명 = '" + 지역[0][0] + "'방향 = '" + SolarDirection_split[k] + "' and 각도 = '" + SolarDegree_split[k] + "'");

                    Ac = Qw_outg[mth] * 2 * 1.03 * 1.03 / Convert.ToDouble(value2[0][0]) / 24 * 1000;
                    if(solar.M_Area()* solar.M_Count() /Ac <1)
                    {
                        dtheta_korr = Math.Min(-20 + 20 * solar.M_Area() * solar.M_Count() / Ac, 0);
                    }
                    else
                    {
                        dtheta_korr = Math.Min(-14 + 14 * solar.M_Area() * solar.M_Count() / Ac, 0);
                    }
                    value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_태양열", "온도차", "지역명 ='" + 지역[0][0] + "'방위='" + SolarDirection_split[k] + "' and 기간 ='" + mth + 1 + "월'");
                    eta[mth] = solar.eta() * solar._50() - solar.K1() * Convert.ToDouble(value[0][0]) / qsol_HN_d - solar.K2() * Convert.ToDouble(value[0][0]) * Convert.ToDouble(value[0][0]) / qsol_HN_d;
                    qsol_mth[mth] = eta[mth] * qsol_HN_mth[mth];
                    Qsol_mth[mth] = qsol_mth[mth] * solar.M_Area() * solar.M_Count() / 1.03 / 1.03;

                    Qw_sol[mth] = Math.Min(Qsol_mth[mth], Qw_outg[mth] ) ;
                  
                    Ww_gen[mth] = 0.025 * Qw_sol[mth];
                }
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
        String Num_pump; double A_pump; double B_pump; double V_pump; double Power_pump; double H_pump; double count_pump; String Valve_pump; String Control_pump;
        public DHW_Pump(String Num, double A, double B, double V, double Power, double H, double count, String Valve, String Control)
        {
            this.Num_pump = Num;
            this.A_pump = A;
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
        public double A()
        {
            return this.A_pump;
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