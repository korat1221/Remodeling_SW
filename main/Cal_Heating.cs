using System;
using System.Collections;
using System.Security.Policy;

namespace main
{
    internal class Cal_Heating
    {
        String HeatingNum, HeatingName; String SelectZone_nonsplit;
        String SystemLoacation, SLRL, Complex, MainSystem, Sub1System, Sub2System;
        String SelectBoiler_nonsplit, BoilerNum_nonsplit;
        String SelectSolar_nonsplit, SolarNum_nonsplit, SolarDirection_nonsplit, SolarDegree_nonsplit;
        String[,] SelectHP_nonsplit = new String[3, 1], HPNum_nonsplit = new String[3, 1], HPSupply_nonsplit = new String[3, 1], HPControl_nonsplit = new String[3, 1]; //외기/지열/지하수 순 
        String PumpUse, PumpMethod, Pump1, Pump2, Pump1Valve, Pump2Valve, Pump1Control, Pump2Control; int Pump1Count, Pump2Count;
        String ce1Type, ce2Type; int ce_SelectRow;
        public ArrayList ce_Type1 = new ArrayList(); public ArrayList ce_Type2 = new ArrayList(); public ArrayList Pump = new ArrayList();
        String StorageUse, StoragePumpUse, StoragePump; public double Vs;
        String[] SystemType = { "보일러", "히트펌프", "흡수식온수기", "지역난방", "태양열시스템" };
        String[] ceType = { "실내기", "방열기", "팬코일유닛", "파워팬유닛", "복사난방" };
        double PipeD, PipeInsD, PipeIns_Ramda;
        String PipeIns;
        int ZoneCount;
        ArrayList SelectZone_split = new ArrayList(); ArrayList SelectBoiler_split = new ArrayList(); ArrayList BoilerNum_split = new ArrayList();
        public double[] Qhb_mth_sum = new double[12]; public double[] theta_ih_avg = new double[12]; public double[] theta_e = new double[12]; public double[] theta_u = new double[12];
        public double Qh_max_sum, Qh_a_sum, th_op_day_avg, theta_i_h_set_avg; public double[] th_avg = new double[12]; public double[] dop_mth_avg = new double[12];
        double SL, RL;
        double[] dmth = new double[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public double[] thrL = new double[12], thrL_day = new double[12], dhrB = new double[12], fLNA = new double[12], fLwe = new double[12];
        public double[] beta_h_ce = new double[12], beta_h_d = new double[12], beta_h_s = new double[12], beta_h_gen = new double[12];
        public double[] theta_av_ce = new double[12], theta_av_d = new double[12], theta_av_s = new double[12], theta_av_gen = new double[12];
        public double[] dtheta_ce = new double[12], dtheta_d = new double[12], dtheta_s = new double[12], dtheta_gen = new double[12];
        public double[] Qh_ce = new double[12], Qh_d = new double[12], Qh_s = new double[12], Qh_gen = new double[12], Qh_outg = new double[12], Qh_f = new double[12];
        public double[] Wh_ce = new double[12], Wh_d = new double[12], Wh_s = new double[12], Wh_g = new double[12];
        public double dtheta_ce1, dtheta_ce2, Psi_pipe, L, Qs_po_day;
        public double[] Qh_gen_day = new double[12], Pgen_Pn = new double[12], Pgen_Pint = new double[12], Pgen_P0 = new double[12], eta_gen_Pn = new double[12], eta_gen_Pint = new double[12];
        public double[] fpint = new double[12];  public double[,] COPpint = new double[3, 12], Qh_outg_sng = new double[3, 12];
        public String Carrier; 
        ArrayList SelectAirHP_split = new ArrayList(); ArrayList SelectGroundHP_split = new ArrayList(); ArrayList SelectGWHP_split = new ArrayList();
        ArrayList AirHPSupply_split = new ArrayList(); ArrayList GroundHPSupply_split = new ArrayList(); ArrayList GWHPSupply_split = new ArrayList();
        ArrayList AirHPControl_split = new ArrayList(); ArrayList GroundHPControl_split = new ArrayList(); ArrayList GWHPControl_split = new ArrayList();
        ArrayList AirHPNum_split = new ArrayList(); ArrayList GroundHPNum_split = new ArrayList(); ArrayList GWHPNum_split = new ArrayList();
        ArrayList SelectSolar_split = new ArrayList(); ArrayList SolarNum_split = new ArrayList(); ArrayList SolarDirection_split = new ArrayList(); ArrayList SolarDegree_split = new ArrayList();
        


        string[][] 지역, 외기온도;
        public Cal_Heating(String HeatingNum)
        {
            this.HeatingNum = HeatingNum;
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
            double[,] Qhb_mth; double[,] theta_ih; double[,] th; double[,] dop_mth; double[] th_op_day; double[] Qh_max; double[] Qh_a; double[] theta_i_h_set;

            //존 정보 불러오기
            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "명칭,존", "번호 = '" + HeatingNum + "'");
                HeatingName = Value[0][0];
                SelectZone_nonsplit = Value[0][1];
                Split_Zone(SelectZone_nonsplit);


                string[][] Value_ce = Program.DB.getValue(DB.type.ProjDB, "Heating_ce_Form", "공급설비,존번호", "난방시스템 = '" + HeatingNum + "'");
                Qhb_mth = new double[Value_ce.Length, 12];
                theta_ih = new double[Value_ce.Length, 12];
                th = new double[Value_ce.Length, 12];
                Qh_max = new double[SelectZone_split.Count];
                Qh_a = new double[Value_ce.Length];
                dop_mth = new double[Value_ce.Length, 12];
                th_op_day = new double[Value_ce.Length];
                theta_i_h_set = new double[Value_ce.Length];

                for (int n = 0; n < Value_ce.Length; n++)
                {
                    Zone zone = Program.CALC.getZone(Value_ce[n][1].ToString());
                    if (zone != null)
                    {
                        for (int mth = 0; mth < 12; mth++)
                        {
                            string[][] Qhb_ce = Program.DB.getValue(DB.type.ProjDB, "Heating_ce_Form", "요구량" + (mth + 1).ToString() + "월", "공급설비 = '" + Value_ce[n][0] + "' And 존번호 ='" + Value_ce[n][1]+"'");
                            Qhb_mth[n, mth] = Convert.ToDouble(Qhb_ce[0][0]);
                            theta_ih[n, mth] = zone.theta_i[1, 0, mth]; //이용일 난방
                            th[n, mth] = zone.t_max[0, mth]; // 난방 시간 
                            Qh_a[n] = zone.Qb_a[0]; //연간 난방요구량
                            dop_mth[n, mth] = zone.dwd_mth[mth];
                            th_op_day[n] = zone.th_op_d;
                            theta_i_h_set[n] = zone.theta_i_h_set;
                        }
                    }                 
                }
                for (int k = 0; k < SelectZone_split.Count; k++)
                {
                    Zone zone = Program.CALC.getZone(SelectZone_split[k].ToString());
                    Qh_max[k] = zone.Q_max[0];//최대부하 
                    Qh_max_sum += Qh_max[k];
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
            catch { }
        }

        //난방설비 일반정보 불러오기 
        public void Load_HeatingGeneral()
        {
            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "설치위치,공급환수온도,복합설비유무,주요설비,보조설비1,보조설비2", "번호 = '" + HeatingNum + "'");

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
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "보일러종류,보일러대수", "번호 = '" + HeatingNum + "'");
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
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "태양열번호,모듈개수,모듈방위,모듈기울기", "번호 = '" + HeatingNum + "'");
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
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "펌프유무,펌프방식,펌프1종류,펌프2종류,펌프1밸브,펌프2밸브,펌프1제어,펌프2제어,펌프1대수,펌프2대수", "번호 = '" + HeatingNum + "'");

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

        public void Load_ceData()
        {
            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "공급설비1종류,공급설비2종류", "번호 = '" + HeatingNum + "'");
                ce1Type = Value[0][0];
                ce2Type = Value[0][1];
            }
            catch { }

        }

        public void Load_StorageData()
        {
            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "축열유무,축열펌프유무,축열펌프,축열용량", "번호 = '" + HeatingNum + "'");
                StorageUse = Value[0][0];
                StoragePumpUse = Value[0][1];
                StoragePump = Value[0][2];
                if (Value[0][3] != null && Value[0][3] != "")
                {
                    Vs = Convert.ToDouble(Value[0][3]);
                }
            }
            catch { }

        }

        public void Load_PipeData()
        {
            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "배관관경,배관보온두께,보온열전도율,배관보온재", "번호 = '" + HeatingNum + "'");
                PipeD = Convert.ToDouble(Value[0][0]);
                PipeInsD = Convert.ToDouble(Value[0][1]);
                PipeIns_Ramda = Convert.ToDouble(Value[0][2]);
                PipeIns = Value[0][3];
            }
            catch { }
        }
        //외기 히트펌프 정보 불러오기 
        public void Load_AirHP()
        {
            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "외기히트펌프번호,외기히트펌프공급방식,외기히트펌프제어방식,외기히트펌프대수", "번호 = '" + HeatingNum + "'");

                String HeatSource = "외기";
                SelectHP_nonsplit[0, 0] = Value[0][0];
                Split_HP(SelectHP_nonsplit[0, 0], HeatSource);

                HPSupply_nonsplit[0, 0] = Value[0][1];
                Split_HPSupply(HPSupply_nonsplit[0, 0], HeatSource);

                HPControl_nonsplit[0, 0] = Value[0][2];
                Split_HPControl(HPControl_nonsplit[0, 0], HeatSource);

                HPNum_nonsplit[0, 0] = Value[0][3];
                Split_HPNum(HPNum_nonsplit[0, 0], HeatSource);

            }
            catch { }
        }

        //지열 히트펌프 정보 불러오기 
        public void Load_GroundHP()
        {
            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "지열히트펌프번호,지열히트펌프공급방식,지열히트펌프제어방식,지열히트펌프대수", "번호 = '" + HeatingNum + "'");

                String HeatSource = "지열";
                SelectHP_nonsplit[1, 0] = Value[0][0];
                Split_HP(SelectHP_nonsplit[1, 0], HeatSource);

                HPSupply_nonsplit[1, 0] = Value[0][1];
                Split_HPSupply(HPSupply_nonsplit[1, 0], HeatSource);

                HPControl_nonsplit[1, 0] = Value[0][2];
                Split_HPControl(HPControl_nonsplit[1, 0], HeatSource);

                HPNum_nonsplit[1, 0] = Value[0][3];
                Split_HPNum(HPNum_nonsplit[1, 0], HeatSource);

            }
            catch { }
        }

        //지하수 히트펌프 정보 불러오기 
        public void Load_GWHP()
        {
            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "지하수히트펌프번호,지하수히트펌프공급방식,지하수히트펌프제어방식,지하수히트펌프대수", "번호 = '" + HeatingNum + "'");

                String HeatSource = "지하수";
                SelectHP_nonsplit[2, 0] = Value[0][0];
                Split_HP(SelectHP_nonsplit[2, 0], HeatSource);

                HPSupply_nonsplit[2, 0] = Value[0][1];
                Split_HPSupply(HPSupply_nonsplit[2, 0], HeatSource);

                HPControl_nonsplit[2, 0] = Value[0][2];
                Split_HPControl(HPControl_nonsplit[2, 0], HeatSource);

                HPNum_nonsplit[2, 0] = Value[0][3];
                Split_HPNum(HPNum_nonsplit[2, 0], HeatSource);

            }
            catch { }
        }


        private void Split_HP(String nonSplit, String HeatSource)
        {
            if (nonSplit != "")
            {
                if (nonSplit.Contains('+'))
                {
                    string[] token = nonSplit.Split('+');
                    if (HeatSource == "외기")
                    { SelectAirHP_split.Clear(); }
                    else if (HeatSource == "지열")
                    { SelectGroundHP_split.Clear(); }
                    else { SelectGWHP_split.Clear(); }
                    foreach (var item in token)
                    {
                        if (HeatSource == "외기")
                        { SelectAirHP_split.Add(item.ToString()); }
                        else if (HeatSource == "지열")
                        { SelectGroundHP_split.Add(item.ToString()); }
                        else
                        { SelectGWHP_split.Add(item.ToString()); }
                    }
                }
                else
                {
                    if (HeatSource == "외기")
                    {
                        SelectAirHP_split.Clear();
                        SelectAirHP_split.Add(nonSplit.ToString());
                    }
                    else if (HeatSource == "지열")
                    {
                        SelectGroundHP_split.Clear();
                        SelectGroundHP_split.Add(nonSplit.ToString());
                    }
                    else
                    {
                        SelectGWHP_split.Clear();
                        SelectGWHP_split.Add(nonSplit.ToString());
                    }
                }
            }
            else { return; }
        }
        private void Split_HPSupply(String nonSplit, String HeatSource)
        {
            if (nonSplit != null)
            {
                if (nonSplit.Contains('+'))
                {
                    string[] token = nonSplit.Split('+');
                    if (HeatSource == "외기")
                    { AirHPSupply_split.Clear(); }
                    else if (HeatSource == "지열")
                    { GroundHPSupply_split.Clear(); }
                    else { GWHPSupply_split.Clear(); }
                    foreach (var item in token)
                    {
                        if (HeatSource == "외기")
                        { AirHPSupply_split.Add(item.ToString()); }
                        else if (HeatSource == "지열")
                        { GroundHPSupply_split.Add(item.ToString()); }
                        else
                        { GWHPSupply_split.Add(item.ToString()); }
                    }
                }
                else
                {
                    if (HeatSource == "외기")
                    { AirHPSupply_split.Add(nonSplit.ToString()); }
                    else if (HeatSource == "지열")
                    { GroundHPSupply_split.Add(nonSplit.ToString()); }
                    else
                    { GWHPSupply_split.Add(nonSplit.ToString()); }
                }
            }
            else { return; }
        }
        private void Split_HPControl(String nonSplit, String HeatSource)
        {
            if (nonSplit != null)
            {
                if (nonSplit.Contains('+'))
                {
                    string[] token = nonSplit.Split('+');
                    if (HeatSource == "외기")
                    { AirHPControl_split.Clear(); }
                    else if (HeatSource == "지열")
                    { GroundHPControl_split.Clear(); }
                    else { GWHPControl_split.Clear(); }
                    foreach (var item in token)
                    {
                        if (HeatSource == "외기")
                        { AirHPControl_split.Add(item.ToString()); }
                        else if (HeatSource == "지열")
                        { GroundHPControl_split.Add(item.ToString()); }
                        else
                        { GWHPControl_split.Add(item.ToString()); }
                    }
                }
                else
                {
                    if (HeatSource == "외기")
                    { AirHPControl_split.Add(nonSplit.ToString()); }
                    else if (HeatSource == "지열")
                    { GroundHPControl_split.Add(nonSplit.ToString()); }
                    else
                    { GWHPControl_split.Add(nonSplit.ToString()); }
                }
            }
            else { return; }
        }
        private void Split_HPNum(String nonSplit, String HeatSource)
        {
            if (nonSplit != null)
            {
                if (nonSplit.Contains('+'))
                {
                    string[] token = nonSplit.Split('+');
                    if (HeatSource == "외기")
                    { AirHPNum_split.Clear(); }
                    else if (HeatSource == "지열")
                    { GroundHPNum_split.Clear(); }
                    else { GWHPNum_split.Clear(); }
                    foreach (var item in token)
                    {
                        if (HeatSource == "외기")
                        { AirHPNum_split.Add(item.ToString()); }
                        else if (HeatSource == "지열")
                        { GroundHPNum_split.Add(item.ToString()); }
                        else
                        { GWHPNum_split.Add(item.ToString()); }
                    }
                }
                else
                {
                    if (HeatSource == "외기")
                    { AirHPNum_split.Add(nonSplit.ToString()); }
                    else if (HeatSource == "지열")
                    { GroundHPNum_split.Add(nonSplit.ToString()); }
                    else
                    { GWHPNum_split.Add(nonSplit.ToString()); }
                }
            }
            else { return; }
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
                theta_SL_beta[mth] = (SL - theta_i_h_set_avg) * Math.Pow(beta_h_ce[mth], 1 / 1.3) + theta_i_h_set_avg;
                theta_RL_beta[mth] = (RL - theta_i_h_set_avg) * Math.Pow(beta_h_ce[mth], 1 / 1.3) + theta_i_h_set_avg;

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

        private double Calc_theta_ce(String ceType, String SLRL, String 설치위치, String 제어방식)
        {
            double dtheta_str1 = 0.0, dtheta_str2 = 0.0, dtheta_ctr, dtheta_im_ctr, dtheta_roomaut, dtheta_hydr = 0.4, theta_dash_str = 0.0;
            double dtheta_emb1, dtheta_emb2, dtheta_im_emt, dtheta_rad;
            double theta_ce;
            if (ceType == "방열기" || ceType == "실내기")
            {

                dtheta_emb1 = 0; dtheta_emb2 = 0; dtheta_im_emt = -0.3; dtheta_rad = 0; dtheta_im_ctr = 0;

                string[][] Value_str1 = Program.DB.getValue(DB.type.BaseDB_Heating, "공급설비온도", "값", "설비유형 = '" + ceType + "' AND 구분 ='" + SLRL + "' And 온도변수 = 'dtheta_str1'");
                dtheta_str1 = Convert.ToDouble(Value_str1[0][0]);

                string[][] Value_str2 = Program.DB.getValue(DB.type.BaseDB_Heating, "공급설비온도", "값", "설비유형 = '" + ceType + "' AND 구분 ='" + 설치위치 + "'And 온도변수 = 'dtheta_str2'");
                dtheta_str2 = Convert.ToDouble(Value_str2[0][0]);
            }
            else if (ceType == "팬코일유닛")
            {

                dtheta_emb1 = 0; dtheta_emb2 = 0; dtheta_im_emt = -0.3; dtheta_rad = 0; dtheta_im_ctr = 0;
                dtheta_str1 = 0.0;

                string[][] Value_str2 = Program.DB.getValue(DB.type.BaseDB_Heating, "공급설비온도", "값", "설비유형 = '" + ceType + "' AND 구분 ='" + 설치위치 + "'And 온도변수 = 'dtheta_str2'");
                dtheta_str2 = Convert.ToDouble(Value_str2[0][0]);
            }
            else if (ceType == "복사난방")
            {
                dtheta_im_emt = -0.2; dtheta_rad = 0; dtheta_im_ctr = 0;

                string[][] Value_str1 = Program.DB.getValue(DB.type.BaseDB_Heating, "공급설비온도", "값", "설비유형 = '" + ceType + "' AND 구분 ='" + 설치위치 + "'And 온도변수 = 'dtheta_str1'");
                dtheta_str1 = Convert.ToDouble(Value_str1[0][0]);
                dtheta_str2 = 0.0;
                string[][] Value_emb1 = Program.DB.getValue(DB.type.BaseDB_Heating, "공급설비온도", "값", "설비유형 = '" + ceType + "' AND 구분 ='" + 설치위치 + "'And 온도변수 = 'dtheta_emb1'");
                dtheta_emb1 = Convert.ToDouble(Value_emb1[0][0]);
                dtheta_emb2 = 0.0;
            }
            else
            {
                dtheta_emb1 = 0; dtheta_emb2 = 0; dtheta_im_emt = -0.3; dtheta_rad = 0; dtheta_im_ctr = 0;

                string[][] Value = Program.DB.getValue(DB.type.BaseDB_Heating, "공급설비온도", "값", "설비유형 = '파워팬유닛' AND 구분 ='" + 제어방식 + "'And 온도변수 = 'theta_dash_str'");
                theta_dash_str = 10 * Convert.ToDouble(Value[0][0]) / (16 * (0.5 * 4 - 1.1));

            }

            string[][] Value_ctr = Program.DB.getValue(DB.type.BaseDB_Heating, "공급설비온도", "값", "구분 ='" + 제어방식 + "'And 온도변수 = 'dtheta_ctr'");
            dtheta_ctr = Convert.ToDouble(Value_ctr[0][0]);

            string[][] Value_roomaut = Program.DB.getValue(DB.type.BaseDB_Heating, "공급설비온도", "값", "구분 ='" + 제어방식 + "'And 온도변수 = 'dtheta_roomaut'");
            dtheta_roomaut = Convert.ToDouble(Value_roomaut[0][0]);


            if (ceType == "방열기" || ceType == "실내기" || ceType == "팬코일유닛")
            {
                theta_ce = (dtheta_str1 + dtheta_str2) / 2 + (dtheta_ctr + dtheta_im_ctr + dtheta_roomaut + dtheta_hydr + theta_dash_str + dtheta_emb1 + dtheta_emb2 + dtheta_im_emt + dtheta_rad);
            }
            else if (ceType == "복사난방")
            {
                theta_ce = (dtheta_emb1 + dtheta_emb2) / 2 + (dtheta_str1 + dtheta_str2 + dtheta_ctr + dtheta_im_ctr + dtheta_roomaut + dtheta_hydr + theta_dash_str + dtheta_im_emt + dtheta_rad);
            }
            else
            {
                theta_ce = (theta_dash_str + dtheta_emb1 + dtheta_emb2 + dtheta_ctr + dtheta_im_ctr + dtheta_roomaut + dtheta_hydr + theta_dash_str + dtheta_im_emt + dtheta_rad);
            }

            return theta_ce;
        }

        public void Calc_Qce()
        {
            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "Heating_ce_Form", "존번호,공급설비,설치위치", "난방시스템 = '" + HeatingNum + "' And 공급설비종류 = '" + ce1Type + "'");
                ce_Type1.Clear();
                for (int n = 0; n < Value.Length; n++)
                {
                    String Num, ce_ZoneNum, ceSystemNum, ceType, Location, Control;
                    double theta;
                    Num = Value[n][1];
                    ce_ZoneNum = Value[n][0];
                    ceSystemNum = Value[n][1].Substring(0, Value[n][1].IndexOf("_"));
                    ceType = ce1Type;
                    Location = Value[n][2];
                    string[][] 일람표정보 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "온도제어방식", "번호 = '" + ceSystemNum + "'");
                    Control = 일람표정보[0][0];
                    theta = Calc_theta_ce(ceType, SLRL, Location, Control);
                    dtheta_ce1 = theta;
                    CE ce = new CE(Num, ce_ZoneNum, ceSystemNum, ceType, Location, Control, theta);
                    ce_Type1.Add(ce);
                }
            }
            catch { }

            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "Heating_ce_Form", "존번호,공급설비,설치위치", "난방시스템 = '" + HeatingNum + "' And 공급설비종류 = '" + ce2Type + "'");
                ce_Type2.Clear();
                for (int n = 0; n < Value.Length; n++)
                {
                    String Num, ce_ZoneNum, ceSystemNum, ceType, Location, Control;
                    double theta;
                    Num = Value[n][1];
                    ce_ZoneNum = Value[n][0];
                    ceSystemNum = Value[n][1].Substring(0, Value[n][1].IndexOf("_"));
                    ceType = ce2Type;
                    Location = Value[n][2];
                    string[][] 일람표정보 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "온도제어방식", "번호 = '" + ceSystemNum + "'");
                    Control = 일람표정보[0][0];
                    theta = Calc_theta_ce(ceType, SLRL, Location, Control);
                    dtheta_ce2 = theta;
                    CE ce = new CE(Num, ce_ZoneNum, ceSystemNum, ceType, Location, Control, theta);
                    ce_Type2.Add(ce);
                }
            }
            catch { }


            for (int k = 0; k < ce_Type1.Count; k++)
            {
                CE ce = (CE)ce_Type1[k];

                for (int mth = 1; mth < 13; mth++)
                {
                    try
                     {
                  
                        string[][] ceValue = Program.DB.querySQL(DB.type.ProjDB, "select a.요구량" + mth + "월, b.theta_i FROM Heating_ce_Form AS a INNER JOIN Zone_HCneed_Result AS b ON a.존번호 = b.번호 where a.공급설비 = '" + ce.Num() + "' and 번호 = '" + ce.ZoneNum() + "' And 난방_냉방 = '난방' and 비이용일_이용일 ='이용일' and 월 ='" + mth + "월'");
                        //string[][] Value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_mth,theta_i, t_max", "번호 = '" + ce.ZoneNum() + "' And 난방_냉방 = '난방' and 비이용일_이용일 ='이용일' and 월 ='" + mth + "월'");
                        Qh_ce[mth - 1] += Math.Max(Convert.ToDouble(ceValue[0][0]) * ce.theta_ce() / (Convert.ToDouble(ceValue[0][1]) - theta_e[mth - 1]), 0);
                        if (double.IsNaN(Qh_ce[mth - 1]))
                        {
                            Qh_ce[mth - 1] = 0;
                        }
                    
                      }
                    catch { Qh_ce[mth - 1] = 0; }
                    try
                    {
                        string[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "소비전력_난방", "번호 = '" + ce.ceNum() + "'");
                        if (Value2.Length > 0)
                        {
                            // Wh_ce[mth - 1] += Math.Max(Convert.ToDouble(Value2[0][0]) * thrL[mth - 1], 0);
                            Wh_ce[mth - 1] += 0;
                            if (double.IsNaN(Wh_ce[mth - 1]))
                            {
                                Wh_ce[mth - 1] = 0;
                            }
                        }
                    }
                    catch { Wh_ce[mth - 1] = 0; }
                }
                


            }
            for (int k = 0; k < ce_Type2.Count; k++)
            {
                CE ce = (CE)ce_Type2[k];

                try
                {
                    for (int mth = 1; mth < 13; mth++)
                    {
                        string[][] ceValue = Program.DB.querySQL(DB.type.ProjDB, "select a.요구량" + mth + "월, b.theta_i FROM Heating_ce_Form AS a INNER JOIN Zone_HCneed_Result AS b ON a.존번호 = b.번호 where a.공급설비 = '" + ce.Num() + "' and 번호 = '" + ce.ZoneNum() + "' And 난방_냉방 = '난방' and 비이용일_이용일 ='이용일' and 월 ='" + mth + "월'");
                        // string[][] Value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_mth,theta_i", "번호 = '" + ce.ZoneNum() + "' And 난방_냉방 = '난방' and 비이용일_이용일 ='이용일' and 월 ='" + mth + "월'");
                        Qh_ce[mth - 1] += Math.Max(Convert.ToDouble(ceValue[0][0]) * ce.theta_ce() / (Convert.ToDouble(ceValue[0][1]) - theta_e[mth - 1]), 0);
                        if (double.IsNaN(Wh_ce[mth - 1]))
                        {
                            Qh_ce[mth - 1] = 0;
                        }
                        string[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "소비전력_난방", "번호 = '" + ce.ceNum() + "'");
                        if (Value2.Length > 0)
                        {
                            // Wh_ce[mth - 1] += Math.Max(Convert.ToDouble(Value2[0][0]) * thrL[mth], 0);
                            Wh_ce[mth - 1] += 0;
                            if (double.IsNaN(Wh_ce[mth - 1]))
                            {
                                Wh_ce[mth - 1] = 0;
                            }
                        }
                    }
                }
                catch { }
            }
        }

        public void Calc_beta_d()
        {
            double[] theta_SL_beta = new double[12], theta_RL_beta = new double[12];

            for (int mth = 0; mth < 12; mth++)
            {
                beta_h_d[mth] = (Qhb_mth_sum[mth] + Qh_ce[mth]) / (Qh_max_sum / 1000 * th_avg[mth]);
                if (double.IsNaN(beta_h_d[mth])) { beta_h_d[mth] = 0; }

                theta_SL_beta[mth] = (SL - theta_i_h_set_avg) * Math.Pow(beta_h_d[mth], 1 / 1.3) + theta_i_h_set_avg;
                theta_RL_beta[mth] = (RL - theta_i_h_set_avg) * Math.Pow(beta_h_d[mth], 1 / 1.3) + theta_i_h_set_avg;

                dtheta_d[mth] = theta_SL_beta[mth] - theta_RL_beta[mth];
                theta_av_d[mth] = 0.5 * (theta_SL_beta[mth] + theta_RL_beta[mth]);
                if (double.IsNaN(dtheta_d[mth])) { dtheta_d[mth] = 0; }
                if (double.IsNaN(theta_av_d[mth])) { theta_av_d[mth] = 0; }

            }
        }
        public void Calc_Qd()
        {


            double R_pipe, R_se, Ramda_se, L1 = 0, L2 = 0;

            //배관 열저항
            {
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

                    Qh_d[mth] = Math.Max(Psi_pipe * L * (theta_av_d[mth] - theta_ih_avg[mth]) * thrL[mth] / 1000, 0);
                    if (double.IsNaN(Qh_d[mth])) { Qh_d[mth] = 0; }
                }
            }
            //펌프
            {


                try
                {
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "A효율,B효율,유량,동력,양정,대수", "번호 = '" + Pump1 + "'");
                    string[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "A효율,B효율,유량,동력,양정,대수", "번호 = '" + Pump2 + "'");
                    Pump.Clear();
                    for (int n = 0; n < Value.Length; n++)
                    {
                        String Num_pump; double A_pump; double B_pump; double V_pump; double Power_pump; double H_pump; double count_pump;
                        double Cp1, Cp2, Ppump, fhydr = 1, dPz, f_dpm;
                        double[] Vz = new double[12], P_hydr = new double[12], fe = new double[12], e_hydr = new double[12], Wh_hydr = new double[12];
                        double theta;
                        Num_pump = Pump1;
                        A_pump = Convert.ToDouble(Value[0][0]);
                        B_pump = Convert.ToDouble(Value[0][1]);
                        V_pump = Convert.ToDouble(Value[0][2]);
                        Power_pump = Convert.ToDouble(Value[0][3]);
                        H_pump = Convert.ToDouble(Value[0][4]);
                        count_pump = Convert.ToDouble(Value[0][5]);
                        Pump pump1 = new Pump(Num_pump, A_pump, B_pump, V_pump, Power_pump, H_pump, Pump1Count, Pump1Valve, Pump1Control); ;
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
                        dPz = H_pump * 1000 * 9.81;
                        for (int mth = 0; mth < 12; mth++)
                        {
                            if (Value2.Length > 0)
                            { Vz[mth] = Qh_max_sum / 1000 * Convert.ToDouble(Value[0][3]) * Pump1Count / (Convert.ToDouble(Value[0][3]) * Pump1Count + Convert.ToDouble(Value2[0][3]) * Pump2Count) * 3.6 / (dtheta_d[mth] * 4.18); } //2개일 경우 펌프 파워별로 나눠서 분담한 것으로 계산 
                            else
                            { Vz[mth] = Qh_max_sum / 1000 * 3.6 / (dtheta_d[mth] * 4.18); } //2개일 경우 펌프 파워별로 나눠서 분담한 것으로 계산 
                            P_hydr[mth] = dPz * Vz[mth] / 3600;
                            fe[mth] = (1.25 + 200 / P_hydr[mth]) * 2;
                            e_hydr[mth] = fe[mth] * (Cp1 + Cp2 / beta_h_d[mth]) * 0.25 / 0.25;
                            Wh_hydr[mth] = P_hydr[mth] / 1000 * beta_h_d[mth] * th_avg[mth] * f_dpm * 1;
                            Wh_d[mth] = Wh_hydr[mth] * e_hydr[mth];
                        }
                    }
                    for (int n = 0; n < Value2.Length; n++)
                    {
                        String Num_pump; double A_pump; double B_pump; double V_pump; double Power_pump; double H_pump; double count_pump;
                        double Cp1, Cp2, Ppump, fhydr = 1, dPz, f_dpm;
                        double[] Vz = new double[12], P_hydr = new double[12], fe = new double[12], e_hydr = new double[12], Wh_hydr = new double[12];
                        double theta;
                        Num_pump = Pump1;
                        A_pump = Convert.ToDouble(Value2[0][0]);
                        B_pump = Convert.ToDouble(Value2[0][1]);
                        V_pump = Convert.ToDouble(Value2[0][2]);
                        Power_pump = Convert.ToDouble(Value2[0][3]);
                        H_pump = Convert.ToDouble(Value2[0][4]);
                        count_pump = Convert.ToDouble(Value2[0][5]);
                        Pump pump1 = new Pump(Num_pump, A_pump, B_pump, V_pump, Power_pump, H_pump, Pump1Count, Pump1Valve, Pump1Control); ;
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
                        dPz = H_pump * 1000 * 9.81;
                        for (int mth = 0; mth < 12; mth++)
                        {
                            if (Value2.Length > 0)
                            { Vz[mth] = Qh_max_sum / 1000 * Convert.ToDouble(Value2[0][3]) * Pump2Count / (Convert.ToDouble(Value[0][3]) * Pump1Count + Convert.ToDouble(Value2[0][3]) * Pump2Count) * 3.6 / (dtheta_d[mth] * 4.18); } //2개일 경우 펌프 파워별로 나눠서 분담한 것으로 계산 
                            else
                            { Vz[mth] = Qh_max_sum / 1000 * 3.6 / (dtheta_d[mth] * 4.18); } //2개일 경우 펌프 파워별로 나눠서 분담한 것으로 계산 
                            P_hydr[mth] = dPz * Vz[mth] / 3600;
                            fe[mth] = (1.25 + 200 / P_hydr[mth]) * 2;
                            e_hydr[mth] = fe[mth] * (Cp1 + Cp2 / beta_h_d[mth]) * 0.25 / 0.25;
                            Wh_hydr[mth] = P_hydr[mth] / 1000 * beta_h_d[mth] * th_avg[mth] * f_dpm * 1;
                            Wh_d[mth] += Wh_hydr[mth] * e_hydr[mth] + Wh_d[mth];
                        }
                    }
                }
                catch { }

            }
        }
        public void Calc_beta_s()
        {
            double[] theta_SL_beta = new double[12], theta_RL_beta = new double[12];

            for (int mth = 0; mth < 12; mth++)
            {
                beta_h_s[mth] = (Qhb_mth_sum[mth] + Qh_ce[mth] + Qh_d[mth]) / (Qh_max_sum / 1000 * th_avg[mth]);
                if (double.IsNaN(beta_h_s[mth])) { beta_h_s[mth] = 0; }

                theta_SL_beta[mth] = (SL - theta_i_h_set_avg) * Math.Pow(beta_h_s[mth], 1 / 1.3) + theta_i_h_set_avg;
                theta_RL_beta[mth] = (RL - theta_i_h_set_avg) * Math.Pow(beta_h_s[mth], 1 / 1.3) + theta_i_h_set_avg;

                dtheta_s[mth] = theta_SL_beta[mth] - theta_RL_beta[mth];
                theta_av_s[mth] = 0.5 * (theta_SL_beta[mth] + theta_RL_beta[mth]);
                if (double.IsNaN(dtheta_s[mth])) { dtheta_s[mth] = 0; }
                if (double.IsNaN(theta_av_s[mth])) { theta_av_s[mth] = 0; }
            }
        }
        public void Calc_Qh_s()
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

            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "동력", "번호 = '" + StoragePump + "'");
                for (int mth = 0; mth < 12; mth++)
                {
                    double tPu = beta_h_s[mth] * 24 * dhrB[mth];
                    Wh_s[mth] = Convert.ToDouble(Value[0][0]) * tPu / 1000;
                    if (double.IsNaN(Wh_s[mth])) { Wh_s[mth] = 0; }
                }
            }
            catch { }
        }
        public void Calc_beta_gen()
        {
            double[] theta_SL_beta = new double[12], theta_RL_beta = new double[12];

            for (int mth = 0; mth < 12; mth++)
            {
                beta_h_gen[mth] = (Qhb_mth_sum[mth] + Qh_ce[mth] + Qh_d[mth] + Qh_s[mth]) / (Qh_max_sum / 1000 * th_avg[mth]);
                if (double.IsNaN(beta_h_gen[mth])) { beta_h_gen[mth] = 0; }
                theta_SL_beta[mth] = (SL - theta_i_h_set_avg) * Math.Pow(beta_h_s[mth], 1 / 1.3) + theta_i_h_set_avg;
                theta_RL_beta[mth] = (RL - theta_i_h_set_avg) * Math.Pow(beta_h_s[mth], 1 / 1.3) + theta_i_h_set_avg;

                dtheta_gen[mth] = theta_SL_beta[mth] - theta_RL_beta[mth];
                theta_av_gen[mth] = 0.5 * (theta_SL_beta[mth] + theta_RL_beta[mth]);
                Qh_outg[mth] = Qhb_mth_sum[mth] + Qh_ce[mth] + Qh_d[mth] + Qh_s[mth];
                if (double.IsNaN(dtheta_gen[mth])) { dtheta_gen[mth] = 0; }
                if (double.IsNaN(theta_av_gen[mth])) { theta_av_gen[mth] = 0; }
                if (double.IsNaN(Qh_outg[mth])) { Qh_outg[mth] = 0; }
            }
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
                    string[][] 기존신규 = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "프로젝트유형", "번호 = '" + HeatingNum+ "'");
                    double eta_Pn = Convert.ToDouble(Value[0][5]) / 100 ;
                    double eta_Pint = Convert.ToDouble(Value[0][6]) / 100 ;
                    if (기존신규[0][0] =="1")
                    {
                        eta_Pn = eta_Pn * 0.95;
                        eta_Pint = eta_Pint * 0.95;
                    }
                   
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
                        Ph_gen_aux[mth] = (beta_h_gen[mth] / beta_gen_pint * (Paux_Pint - W_0 / 1000) + W_0 / 1000);
                        Ph_gen_aux[mth] = ((beta_h_gen[mth] - beta_gen_pint) / (1 - beta_gen_pint) * (Paux_Pn - Paux_Pint) + Paux_Pint);
                        Wh_g_i[mth] = Ph_gen_aux[mth] * (thrL[mth] - tw_Pn_day * dop_mth_avg[mth]) + W_0 / 1000 * (24 * dmth[mth] - thrL[mth]);
                        Wh_g[mth] += Wh_g_i[mth];
                    }

                }
                catch { }

            }
        }
        public void Calc_Solar()
        {
            double qsol_HN_d, dtheta_korr;
            double[] qsol_HN_mth = new double[12], eta = new double[12], qsol_mth = new double[12], Qsol_mth = new double[12], Qw_sol = new double[12], Qh_sol = new double[12], Ww_gen = new double[12];
            string[][] Solarvalue;
            double Ac;

            for (int k = 0; k < SelectSolar_split.Count; k++)
            {
                Solarvalue = Program.DB.getValue(DB.type.ProjDB, "User_Solar", "번호,모듈면적,효율,열손실계수1차,열손실계수2차,입사각50도,유효열용량", "번호 ='" + SelectSolar_split[k] + "'");
                Solar solar = new Solar(Solarvalue[0][0], Convert.ToDouble(Solarvalue[0][1]), Convert.ToDouble(Solarvalue[0][2]), Convert.ToDouble(Solarvalue[0][3]), Convert.ToDouble(Solarvalue[0][4]), Convert.ToDouble(Solarvalue[0][5]), Convert.ToDouble(Solarvalue[0][6]), Convert.ToDouble(SolarNum_split[k]), SolarDirection_split[k].ToString(), SolarDegree_split[k].ToString());

                for (int mth = 0; mth < 12; mth++)
                {
                    string[][] value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_전일사량", "일사량", "지역명 ='" + 지역[0][0] + "'방향='" + SolarDirection_split[k] + "' and 각도 ='" + SolarDegree_split[k] + "' and 기간 ='" + mth + 1 + "월'");
                    qsol_HN_d = Convert.ToDouble(value[0][0]);
                    qsol_HN_mth[mth] = qsol_HN_d * dmth[mth] * 24 / 1000;

                    string[][] value2 = Program.DB.querySQL(DB.type.BaseDB_HCneed, "Select Max(일사량) from 기후데이터_전일사량 where 지역명 = '" + 지역[0][0] + "'방향 = '" + SolarDirection_split[k] + "' and 각도 = '" + SolarDegree_split[k] + "'");

                    Ac = Qh_max_sum * 2 * 1.03 * 1.03 / Convert.ToDouble(value2[0][0]) / 24 * 1000;
                    if (solar.M_Area() * solar.M_Count() / Ac < 1)
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

                    Qw_sol[mth] = Math.Min(Qsol_mth[mth], Qh_outg[mth] * 2) * Qh_outg[mth] / (Qh_outg[mth] + Qh_outg[mth]);
                    Qh_sol[mth] = Math.Min(Qsol_mth[mth], Qh_outg[mth] * 2) * Qh_outg[mth] / (Qh_outg[mth] + Qh_outg[mth]);

                    Ww_gen[mth] = 0.025 * Qw_sol[mth];
                }
            }


        }
        public void Calc_Q_Air_HP()
        {
            for (int n = 0; n < SelectAirHP_split.Count; n++)
            {

                string[][] airHP = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", "번호,연료,공급유형,난방정격용량,난방정격COP,난방정격소비전력,한랭지용량,한랭지COP,한랭지소비전력", "번호 = '" + SelectAirHP_split[n] + "'");
                String Num = null;
                Carrier = null;
                String SupplyType = null;
                double Pi_nom = 0; //정격용량
                double COP_nom = 0; //정격COP
                double W_nom = 0; //정격소비전력 
                double Pi_15 = 0; //정격용량
                double COP_15 = 0; //정격COP
                double W_15 = 0; //정격소비전력 

                double Pi_2 = 0, Pi_7 = 0, COP_2 = 0, COP_7 = 0, W_2 = 0, W_7 = 0; //2도, -7도
                double[] 수방식_비율_Pi = { 0.64, 0.8, 0.95 };//-7,2,7
                double[] 직팽인버터_비율_Pi = { 0.81, 0.96, 1 };//-7,2,7,
                double[] 직팽없음_비율_Pi = { 0.81, 0.96, 1 };//-7,2,7,
                double[] COP_standard = new double[4];

                try
                {
                    Num = airHP[0][0];
                    Carrier = airHP[0][1];
                    SupplyType = airHP[0][2];
                    Pi_nom = Convert.ToDouble(airHP[0][3]); //정격용량
                    COP_nom = Convert.ToDouble(airHP[0][4]); //정격COP
                    W_nom = Convert.ToDouble(airHP[0][5]); //정격소비전력 
                    Pi_15 = Convert.ToDouble(airHP[0][6]); //정격용량
                    COP_15 = Convert.ToDouble(airHP[0][7]); //정격COP
                    W_15 = Convert.ToDouble(airHP[0][8]); //정격소비전력 

                    double themp_상수 = 10.00;
                    COP_standard[0] = ((-7 + 15 - themp_상수) * (-7 + 15 + 273.15) / 15 + SL + 7 - 15) / (SL - themp_상수) + (-7 + 273.15) / (SL - themp_상수) * Math.Log(Math.E, (themp_상수 + 7)) / 15; //-7일 경우,
                    COP_standard[1] = ((2 + 15 - themp_상수) * (2 + 15 + 273.15) / 15 + SL - 2 - 15) / (SL - themp_상수) + (2 + 273.15) / (SL - themp_상수) * Math.Log(Math.E, (themp_상수 - 2)) / 15; //2일 경우,
                    COP_standard[2] = ((7 + 15 - themp_상수) * (7 + 15 + 273.15) / 15 + SL - 7 - 15) / (SL - themp_상수) + (7 + 273.15) / (SL - themp_상수) * Math.Log(Math.E, (themp_상수 - 7)) / 15; //7일 경우,
                    COP_standard[3] = ((-15 + 15 - themp_상수) * (-15 + 15 + 273.15) / 15 + SL - (-15) - 15) / (SL - themp_상수) + (-15 + 273.15) / (SL - themp_상수) * Math.Log(Math.E, (themp_상수 - (-15))) / 15; //-15일 경우,

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
                }
                catch { }



                double[,] kbuh = new double[4, 12], DH = new double[3, 12], Wi = new double[3, 12], H = new double[5, 12], Wtime = new double[3, 12];
                double[] fLg = new double[12];
                try
                {
                    for (int mth = 1; mth <= 12; mth++)
                    {
                        for (int k = 1; k <= 4; k++)
                        {
                            string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_히트펌프_하이브리드", "빈도", "지역명 ='" + 지역[0][0] + "' and 유형 ='선택운전' And 기간  = '" + mth + "월' and 구분 ='온도등급" + k + "'");
                            kbuh[k - 1, mth - 1] = Convert.ToDouble(Value[0][0]);

                        }
                    }
                    for (int mth = 1; mth <= 12; mth++)
                    {
                        for (int k = 1; k <= 3; k++)
                        {
                            string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_히트펌프_일반", "빈도", "지역명 ='" + 지역[0][0] + "' and 단위 ='[Kh]' And 기간  = '" + mth + "월' and 구분 ='온도등급" + k + "'");
                            DH[k - 1, mth - 1] = Convert.ToDouble(Value[0][0]);
                        }

                        Wi[0, mth - 1] = Math.Max(DH[0, mth - 1] / (DH[0, mth - 1] + DH[1, mth - 1] + DH[2, mth - 1]), 0);
                        Wi[1, mth - 1] = Math.Max(DH[1, mth - 1] / (DH[0, mth - 1] + DH[1, mth - 1] + DH[2, mth - 1]), 0); 
                        Wi[2, mth - 1] = Math.Max(DH[2, mth - 1] / (DH[0, mth - 1] + DH[1, mth - 1] + DH[2, mth - 1]) ,0);
                       
                    }

                    for (int mth = 1; mth <= 12; mth++)
                    {
                        for (int k = 1; k <= 4; k++)
                        {
                            string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_히트펌프_일반", "빈도", "지역명 ='" + 지역[0][0] + "' and 단위 ='[h]' And 기간  = '" + mth + "월' and 구분 ='온도등급" + k + "'");
                            H[k - 1, mth - 1] = Convert.ToDouble(Value[0][0]);
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
                }
                catch { }

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
                        fpint[mth] = Convert.ToDouble(Valuef[0][0]);
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
                        fpint[mth] = Convert.ToDouble(Valuef[0][0]);
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

                if (Qh_ce[mth] < 0) { Qh_ce[mth] = 0; }
                if (Qh_d[mth] < 0) { Qh_d[mth] = 0; }
                if (Qh_s[mth] < 0) { Qh_s[mth] = 0; }
                if (Qh_gen[mth] < 0) { Qh_gen[mth] = 0; }
                if (Qh_f[mth] < 0) { Qh_f[mth] = 0; }

                if (Wh_ce[mth] < 0) { Wh_ce[mth] = 0; }
                if (Wh_d[mth] < 0) { Wh_d[mth] = 0; }
                if (Wh_s[mth] < 0) { Wh_s[mth] = 0; }
                if (Wh_g[mth] < 0) { Wh_g[mth] = 0; }
            }

        }
    }

    public class CE
    {
        String ce_Num, ce_ZoneNum, ce_ceNum, ce_ceType, ce_Location, ce_Control;
        double ce_theta;
        public CE(String Num, String ZoneNum, String ceNum, String ceType, String Location, String Control, double theta)
        {
            this.ce_Num = Num;
            this.ce_ZoneNum = ZoneNum;
            this.ce_ceNum = ceNum;
            this.ce_ceType = ceType;
            this.ce_Location = Location;
            this.ce_Control = Control;
            this.ce_theta = theta;
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
    }
    public class Pump
    {
        String Num_pump; double A_pump; double B_pump; double V_pump; double Power_pump; double H_pump; double count_pump; String Valve_pump; String Control_pump;
        public Pump(String Num, double A, double B, double V, double Power, double H, double count, String Valve, String Control)
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