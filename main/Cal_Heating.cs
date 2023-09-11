using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace main
{
    internal class Cal_Heating
    {
        String HeatingNum, HeatingName; String SelectZone_nonsplit;
        String SystemLoacation, SLRL, Complex, MainSystem, Sub1System, Sub2System;
        String SelectBoiler_nonsplit, BoilerNum_nonsplit;
        String PumpUse, PumpMethod, Pump1, Pump2, Pump1Valve, Pump2Valve, Pump1Control, Pump2Control; int Pump1Num, Pump2Num;
        String ce1Type, ce2Type; int ce_SelectRow;
        public ArrayList ce_Type1 = new ArrayList();        public ArrayList ce_Type2 = new ArrayList();
        String StorageUse, StoragePumpUse, StoragePump; double Vs;
        String[] SystemType = { "보일러", "히트펌프", "흡수식온수기", "지역난방", "태양열시스템" };
        String[] ceType = { "실내기", "방열기", "팬코일유닛", "파워팬유닛", "복사난방" };
        double PipeD, PipeInsD, PipeIns_Ramda;
        String PipeIns;
        int ZoneCount;
        ArrayList SelectZone_split = new ArrayList(); ArrayList SelectBoiler_split = new ArrayList();
        public double[] Qhb_mth_sum = new double[12]; public double[] theta_ih_avg = new double[12]; public double[] theta_e = new double[12]; public double[] theta_u = new double[12];
        public double Qh_max_sum, Qh_a_sum, th_op_day_avg, theta_i_h_set_avg; public double[] th_avg = new double[12]; public double[] dop_mth_avg = new double[12]; 
        double SL, RL;
        double[] dmth = new double[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        double[] thrL = new double[12]; double[] thrL_day = new double[12]; double[] dhrB= new double[12]; double[] fLNA = new double[12];double[] fLwe = new double[12];
        double[] beta_h_ce = new double[12]; double[] beta_h_d = new double[12]; double[] beta_h_s = new double[12]; double[] beta_h_gen = new double[12];
        double[] theta_av_ce = new double[12]; double[] theta_av_d = new double[12]; double[] theta_av_s = new double[12]; double[] theta_av_gen = new double[12];
        double[] dtheta_ce = new double[12]; double[] dtheta_d = new double[12]; double[] dtheta_s = new double[12]; double[] dtheta_gen = new double[12];
        double[] Qh_ce = new double[12], Qh_d = new double[12], Qh_s = new double[12], Qh_g = new double[12], Qh_outg = new double[12], Qh_f = new double[12];
        double[] Wh_ce = new double[12], Wh_d = new double[12], Wh_s = new double[12], Wh_g = new double[12];
        public Cal_Heating(String HeatingNum) 
        {
            this.HeatingNum = HeatingNum;
            double[,] Qhb_mth; double[,] theta_ih;double[,] th; double[,] dop_mth; double[] th_op_day; double[] Qh_max; double[] Qh_a; double[] theta_i_h_set;

            //존 정보 불러오기
            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "명칭,존", "번호 = '" + HeatingNum + "'");

                HeatingName = Value[0][0];
                SelectZone_nonsplit = Value[0][1];
                Split_Zone(SelectZone_nonsplit);
                Qhb_mth = new double[SelectZone_split.Count, 12];
                theta_ih = new double[SelectZone_split.Count, 12];
                th = new double[SelectZone_split.Count, 12];
                Qh_max = new double[SelectZone_split.Count];
                Qh_a = new double[SelectZone_split.Count];
                dop_mth = new double[SelectZone_split.Count, 12];
                th_op_day = new double[SelectZone_split.Count];
                theta_i_h_set = new double[SelectZone_split.Count];

                for (int n = 0; n < SelectZone_split.Count;  n++)
                {
                    Zone zone = Program.CALC.getZone(SelectZone_split[n].ToString());
                    if (zone != null)
                    {
                        for(int mth = 0 ; mth < 12; mth++)
                        {
                            Qhb_mth[n,mth] = zone.Qhb_mth[mth];
                            theta_ih[n, mth]= zone.theta_i[1,0,mth]; //이용일 난방
                            Qh_max[n] = zone.Q_max[0]; //난방부하
                            th[n,mth] = zone.t_max[0,mth]; // 난방 시간 
                            Qh_a[n] = zone.Qb_a[0]; //연간 난방요구량
                            dop_mth[n, mth] = zone.dwd_mth[mth];
                            th_op_day[n] = zone.th_op_d;
                            theta_i_h_set[n] = zone.theta_i_h_set;
                        }                        
                    }
                    ZoneCount = ZoneCount + 1; 
                }
                for (int mth = 0; mth < 12; mth++)
                {
                    for (int n = 0; n < ZoneCount; n++)
                    {
                        Qhb_mth_sum[mth] += Qhb_mth[n, mth];
                        Qh_max_sum += Qh_max[n];
                        Qh_a_sum += Qh_a[n];

                        //요구량 가중
                        theta_ih_avg[mth] += (theta_ih[n, mth] * Qh_a[n]);
                        th_avg[mth] += (th[n, mth] * Qh_a[n]);
                        dop_mth_avg[mth] += (dop_mth[n, mth] * Qh_a[n]);
                        th_op_day_avg += (th_op_day[n] * Qh_a[n]);
                        theta_i_h_set_avg = (theta_i_h_set[n] * Qh_a[n]);
                    }
                    theta_ih_avg[mth] = theta_ih_avg[mth] / Qh_a_sum; 
                    th_avg[mth]= th_avg[mth] / Qh_a_sum;
                    dop_mth_avg[mth] = dop_mth_avg[mth] / Qh_a_sum; 
                    th_op_day_avg = th_op_day_avg / Qh_a_sum;
                    theta_i_h_set_avg = theta_i_h_set_avg / Qh_a_sum;
                }  
            }
            catch { }

            //외기온도, 단열외피외 온도 불러오기
            try
            {
                string[][] 지역 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
                string[][] OTemp = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_온도습도", "기간,온도", "지역명 ='" + 지역[0][0] + "'");
                int i = -1;
                while (++i < 12)
                {
                    theta_e[i] = Convert.ToDouble(OTemp[i][1]);
                    theta_u[i] = theta_ih_avg[i] - 0.8 * (theta_ih_avg[i] - theta_e[i]);
                }
            }
            catch { }

            //난방설비 일반정보 불러오기 
            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "설치위치,공급환수온도,복합설비유무,주요설비,보조설비1,보조설비2", "번호 = '" + HeatingNum + "'");

                SystemLoacation = Value[0][0];
                SLRL = Value[0][1];
                if(SLRL != null && SLRL != "")
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

            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "보일러종류,보일러대수", "번호 = '" + HeatingNum + "'");
                SelectBoiler_nonsplit = Value[0][0];
                Split_Boiler(SelectBoiler_nonsplit);

                BoilerNum_nonsplit = Value[0][1];
                Split_BoilerNum(BoilerNum_nonsplit);
            }
            catch { }

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
                Pump1Num = Convert.ToInt16(Value[0][8]);
                Pump2Num = Convert.ToInt16(Value[0][9]);               
            }
            catch { }


            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "공급설비1종류,공급설비2종류", "번호 = '" + HeatingNum + "'");
                ce1Type = Value[0][0];
                ce2Type = Value[0][1];
            }
            catch { }

           
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
            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "배관관경,배관보온두께,보온열전도율,배관보온재", "번호 = '" + HeatingNum + "'");
                PipeD = Convert.ToDouble(Value[0][0]);
                PipeInsD = Convert.ToDouble(Value[0][1]);
                PipeInsD = Convert.ToDouble(Value[0][2]);
                PipeIns = Value[0][3];
            }
            catch { }


        }

    private void Split_Zone(String nonSplit)
    {
        if (nonSplit != null)
        {
            if (nonSplit.Contains(","))
            {
                string[] token = nonSplit.Split(',');
                SelectZone_split.Clear();
                foreach (var item in token)
                {
                    SelectZone_split.Add(item.ToString());
                }              
            }
            else
            {
                SelectZone_split.Clear();
                SelectZone_split.Add(SelectZone_split);               
            }
        }   
    }
    private void Split_Boiler(String nonSplit)
    {      
        if (nonSplit != null)
        {
            if (nonSplit.Contains(','))
            {
                string[] token = nonSplit.Split(',');
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
                    ArrayList BoilerNum_split = new ArrayList();

                    string[] token = nonSplit.Split(',');
                    BoilerNum_split.Clear();
                    foreach (var item in token)
                    {
                        BoilerNum_split.Add(item.ToString());
                    }                   
                }               
            }
            else { return; }
        }
        
        private void Calc_thrL()
        {
            for(int mth =0; mth < 12;mth++)
            {
                fLNA[mth] = 1;
                fLwe[mth] = 1;
                thrL_day[mth] = 24 - fLNA[mth]*(24 - th_op_day_avg);
                dhrB[mth] = dmth[mth] * 365 - fLwe[mth] * (dmth[mth] - dop_mth_avg[mth]) * th_avg[mth] / (dmth[mth] * 24);
                thrL[mth] = thrL_day[mth] * dhrB[mth];
            }
        }

        private void Calc_beta_ce()
        {
            double[] theta_SL_beta = new double[12], theta_RL_beta = new double[12];

            for (int mth = 0; mth < 12; mth++)
            {
                beta_h_ce[mth] = Qhb_mth_sum[mth] / (Qh_max_sum * th_avg[mth]);

                theta_SL_beta[mth] = (SL - theta_i_h_set_avg) * Math.Pow(beta_h_ce[mth],1/1.3) * theta_i_h_set_avg;
                theta_RL_beta[mth] = (RL - theta_i_h_set_avg) * Math.Pow(beta_h_ce[mth], 1 / 1.3) * theta_i_h_set_avg;

                dtheta_ce[mth] = theta_SL_beta[mth] - theta_RL_beta[mth];
                theta_av_ce[mth] = 0.5 * (theta_SL_beta[mth] + theta_RL_beta[mth]);

            }
        }

        private double Calc_theta_ce(String ceType, String SLRL, String 설치위치, String 제어방식)
        {
            double dtheta_str1 =0.0, dtheta_str2 = 0.0, dtheta_ctr, dtheta_im_ctr, dtheta_roomaut, dtheta_hydr = 0.4, theta_dash_str =0.0;
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
            else if(ceType == "팬코일유닛")
            {

                dtheta_emb1 = 0; dtheta_emb2 = 0; dtheta_im_emt = -0.3; dtheta_rad = 0; dtheta_im_ctr = 0;
                dtheta_str1 = 0.0;

                string[][] Value_str2 = Program.DB.getValue(DB.type.BaseDB_Heating, "공급설비온도", "값", "설비유형 = '" + ceType + "' AND 구분 ='" + 설치위치 + "'And 온도변수 = 'dtheta_str2'");
                dtheta_str2 = Convert.ToDouble(Value_str2[0][0]);
            }
            else if(ceType == "복사난방")
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
                theta_dash_str =10* Convert.ToDouble(Value[0][0])/(16*(0.5*4-1.1));

            }

            string[][] Value_ctr = Program.DB.getValue(DB.type.BaseDB_Heating, "공급설비온도", "값", "구분 ='" + 제어방식 + "'And 온도변수 = 'dtheta_ctr'");
            dtheta_ctr = Convert.ToDouble(Value_ctr[0][0]);

            string[][] Value_roomaut = Program.DB.getValue(DB.type.BaseDB_Heating, "공급설비온도", "값", "구분 ='" + 제어방식 + "'And 온도변수 = 'dtheta_roomaut'");
            dtheta_roomaut = Convert.ToDouble(Value_roomaut[0][0]);


            if(ceType == "방열기" || ceType == "실내기" || ceType == "팬코일유닛")
            {
                theta_ce = (dtheta_str1 + dtheta_str2) / 2 + (dtheta_ctr + dtheta_im_ctr + dtheta_roomaut + dtheta_hydr + theta_dash_str + dtheta_emb1 + dtheta_emb2 + dtheta_im_emt + dtheta_rad);
            }
            else if (ceType == "복사난방")
            {
                theta_ce = (dtheta_emb1 + dtheta_emb2) / 2 + (dtheta_str1 + dtheta_str2+ dtheta_ctr + dtheta_im_ctr + dtheta_roomaut + dtheta_hydr + theta_dash_str  + dtheta_im_emt + dtheta_rad);
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
                for (int n =0; n < Value.Length; n++)
                {
                    String Num, ce_ZoneNum, ceSystemNum, ceType, Location, Control;
                    double theta;
                    Num = Value[n][0] + "_" + Value[n][1];
                    ce_ZoneNum = Value[n][0];
                    ceSystemNum = Value[n][1].Substring(0, Value[n][1].IndexOf("_"));
                    ceType = ce1Type;
                    Location = Value[n][2];
                    string[][] 일람표정보 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "온도제어방식", "번호 = '" + ceSystemNum + "'");
                    Control = 일람표정보[0][0];
                    theta = Calc_theta_ce(ceType, SLRL, Location, Control);
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
                    Num = Value[n][0] + "_" + Value[n][1];
                    ce_ZoneNum = Value[n][0];
                    ceSystemNum = Value[n][1].Substring(0, Value[n][1].IndexOf("_"));
                    ceType = ce2Type;
                    Location = Value[n][2];
                    string[][] 일람표정보 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "온도제어방식", "번호 = '" + ceSystemNum + "'");
                    Control = 일람표정보[0][0];
                    theta = Calc_theta_ce(ceType, SLRL, Location, Control);
                    CE ce = new CE(Num, ce_ZoneNum, ceSystemNum, ceType, Location, Control, theta);
                    ce_Type2.Add(ce);
                }
            }
            catch { }
            for(int k =0; k<ce_Type1.Count; k++)
            {
                CE ce = ( CE )ce_Type1[k];

                try
                {
                    for(int mth =1; mth< 13; mth++)
                    {
                        string[][] Value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_mth,theta_i, t_max", "번호 = '" + ce.ZoneNum() + "' And 난방_냉방 = '난방' and 비이용일_이용일 ='이용일' and 월 ='" + mth +"월'");
                        Qh_ce[mth - 1] += Convert.ToDouble(Value[0][0]) * ce.theta_ce() / (Convert.ToDouble(Value[0][1]) - theta_e[mth]);

                         string[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "소비전력", "번호 = '" + ce.ceNum + "'");
                        Wh_ce[mth - 1] += Convert.ToDouble(Value2[0][0])* Convert.ToDouble(Value2[0][1]);
                    }                    
                }catch { }
               
            }
            for (int k = 0; k < ce_Type2.Count; k++)
            {
                CE ce = (CE)ce_Type2[k];

                try
                {
                    for (int mth = 1; mth < 13; mth++)
                    {
                        string[][] Value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_mth,theta_i", "번호 = '" + ce.ZoneNum() + "' And 난방_냉방 = '난방' and 비이용일_이용일 ='이용일' and 월 ='" + mth + "월'");
                        Qh_ce[mth - 1] += Convert.ToDouble(Value[0][0]) * ce.theta_ce() / (Convert.ToDouble(Value[0][1]) - theta_e[mth]);

                        string[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "소비전력", "번호 = '" + ce.ceNum + "'");
                        Wh_ce[mth - 1] += Convert.ToDouble(Value2[0][0]) * Convert.ToDouble(Value2[0][1]);
                    }
                }
                catch { }
            }            
        }

        public void Calc_Qd()
        {
            double R_pipe, R_se, Ramda_se, Psi_pipe,L, L1=0,L2=0; 

            //배관 열저항
            {
                R_pipe = Math.Log(((PipeD / 2 + PipeInsD) / 1000) / (PipeD / 2 / 1000)) / 2 / Math.PI / PipeIns_Ramda;
                Ramda_se = 5 + 0.15 * 5.67 / 100000000 * 4 * 1000;
                R_se = 1 / (Ramda_se * 2 * Math.PI * (PipeD / 2 + PipeInsD) / 1000);
                Psi_pipe = 1 / (R_pipe + R_se);
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "양정", "번호 = '" + Pump1 + "'");
                if (Value.Length > 0)
                {
                    L1 = Convert.ToDouble(Value[0][0]);
                }
                Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "양정", "번호 = '" + Pump2 + "'");
                if (Value.Length > 0)
                {
                    L2 = Convert.ToDouble(Value[0][0]);
                }
                L = L1+ L2;
                for (int mth = 0; mth < 12; mth++)
                {

                    Qh_d[mth] = Psi_pipe * (theta_av_d[mth] - theta_ih_avg[mth]) * thrL[mth] / 1000;
                }      
            }
            //펌프
            {
                double Cp1, Cp2, Ppump, H, fadapt, fhydr, dPz, Vz, P_hydr, fe, e_d, Wh_hydr;

                
                
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
        public Pump(String Num)
        {

        }
    }

 }