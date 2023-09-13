using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace main
{
    internal class Cal_Heating
    {
        String HeatingNum, HeatingName; String SelectZone_nonsplit;
        String SystemLoacation, SLRL, Complex, MainSystem, Sub1System, Sub2System;
        String SelectBoiler_nonsplit, BoilerNum_nonsplit;
        String PumpUse, PumpMethod, Pump1, Pump2, Pump1Valve, Pump2Valve, Pump1Control, Pump2Control; int Pump1Count, Pump2Count;
        String ce1Type, ce2Type; int ce_SelectRow;
        public ArrayList ce_Type1 = new ArrayList();        public ArrayList ce_Type2 = new ArrayList(); public ArrayList Pump = new ArrayList();
        String StorageUse, StoragePumpUse, StoragePump; double Vs;
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
        double[] thrL = new double[12]; double[] thrL_day = new double[12]; double[] dhrB= new double[12]; double[] fLNA = new double[12];double[] fLwe = new double[12];
        double[] beta_h_ce = new double[12]; double[] beta_h_d = new double[12]; double[] beta_h_s = new double[12]; double[] beta_h_gen = new double[12];
        double[] theta_av_ce = new double[12]; double[] theta_av_d = new double[12]; double[] theta_av_s = new double[12]; double[] theta_av_gen = new double[12];
        double[] dtheta_ce = new double[12]; double[] dtheta_d = new double[12]; double[] dtheta_s = new double[12]; double[] dtheta_gen = new double[12];
        double[] Qh_ce = new double[12], Qh_d = new double[12], Qh_s = new double[12], Qh_gen = new double[12], Qh_outg = new double[12], Qh_f = new double[12];
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
                for (int n = 0; n < ZoneCount; n++)
                {
                    Qh_max_sum += Qh_max[n];
                    Qh_a_sum += Qh_a[n];

                    //요구량 가중
                    th_op_day_avg += (th_op_day[n] * Qh_a[n]);
                    theta_i_h_set_avg += (theta_i_h_set[n] * Qh_a[n]);
                }
                th_op_day_avg = th_op_day_avg / Qh_a_sum;
                theta_i_h_set_avg = theta_i_h_set_avg / Qh_a_sum;

                for (int mth = 0; mth < 12; mth++)
                {
                    for (int n = 0; n < ZoneCount; n++)
                    {
                        Qhb_mth_sum[mth] += Qhb_mth[n, mth];

                        //요구량 가중
                        theta_ih_avg[mth] += (theta_ih[n, mth] * Qh_a[n]);
                        th_avg[mth] += (th[n, mth] * Qh_a[n]);
                        dop_mth_avg[mth] += (dop_mth[n, mth] * Qh_a[n]);
                    }
                    theta_ih_avg[mth] = theta_ih_avg[mth] / Qh_a_sum; 
                    th_avg[mth]= th_avg[mth] / Qh_a_sum;
                    dop_mth_avg[mth] = dop_mth_avg[mth] / Qh_a_sum;
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
                Pump1Count = Convert.ToInt16(Value[0][8]);
                Pump2Count = Convert.ToInt16(Value[0][9]);               
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
                PipeIns_Ramda = Convert.ToDouble(Value[0][2]);
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
                    string[] token = nonSplit.Split(',');
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
            double dop_a=0;
         for (int mth = 0; mth < 12; mth++)
         {
                dop_a += dop_mth_avg[mth];
         }

        for (int mth =0; mth < 12;mth++)
        {
             fLNA[mth] = 1;
             fLwe[mth] = 1;
             thrL_day[mth] = 24 - fLNA[mth]*(24 - th_op_day_avg);
             dhrB[mth] = dmth[mth] * (365 - fLwe[mth] * (365 - dop_a) )/365* th_avg[mth] / (dmth[mth] * 24);
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
                beta_h_ce[mth] = Qhb_mth_sum[mth] / (Qh_max_sum/1000 * th_avg[mth]);
                if (double.IsNaN(beta_h_ce[mth]))
                {
                    beta_h_ce[mth] = 0;
                }
                theta_SL_beta[mth] = (SL - theta_i_h_set_avg) * Math.Pow(beta_h_ce[mth],1/1.3) + theta_i_h_set_avg;
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
                        Qh_ce[mth - 1] += Math.Max(Convert.ToDouble(Value[0][0]) * ce.theta_ce() / (Convert.ToDouble(Value[0][1]) - theta_e[mth]), 0);
                        if(double.IsNaN(Qh_ce[mth - 1]))
                        {
                            Qh_ce[mth - 1] = 0;
                        }

                         string[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "소비전력", "번호 = '" + ce.ceNum() + "'");
                        Wh_ce[mth - 1] += Math.Max(Convert.ToDouble(Value2[0][0]) * thrL[mth-1],0);
                        if (double.IsNaN(Wh_ce[mth - 1]))
                        {
                            Wh_ce[mth - 1] = 0;
                        }
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
                        Qh_ce[mth - 1] += Math.Max(Convert.ToDouble(Value[0][0]) * ce.theta_ce() / (Convert.ToDouble(Value[0][1]) - theta_e[mth]),0);
                        if (double.IsNaN(Wh_ce[mth - 1]))
                        {
                            Qh_ce[mth - 1] = 0;
                        }

                        string[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "소비전력", "번호 = '" + ce.ceNum() + "'");
                        Wh_ce[mth - 1] += Math.Max(Convert.ToDouble(Value2[0][0]) * thrL[mth], 0);
                        if (double.IsNaN(Wh_ce[mth - 1]))
                        {
                            Wh_ce[mth - 1] = 0;
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
                beta_h_d[mth] =(Qhb_mth_sum[mth]+Qh_ce[mth]) / (Qh_max_sum/1000 * th_avg[mth]);
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
            double R_pipe, R_se, Ramda_se, Psi_pipe,L, L1=0,L2=0; 

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
                L = L1+ L2;
                for (int mth = 0; mth < 12; mth++)
                {

                    Qh_d[mth] = Math.Max(Psi_pipe * L * (theta_av_d[mth] - theta_ih_avg[mth]) * thrL[mth] / 1000, 0);
                    if (double.IsNaN(Qh_d[mth])) { Qh_d[mth] = 0;   }
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
                        double Cp1, Cp2, Ppump, fhydr = 1, dPz,f_dpm;
                        double[] Vz = new double[12], P_hydr = new double[12], fe = new double[12], e_hydr = new double[12], Wh_hydr = new double[12];
                        double theta;
                        Num_pump = Pump1;
                        A_pump = Convert.ToDouble(Value[0][0]);
                        B_pump = Convert.ToDouble(Value[0][1]);
                        V_pump = Convert.ToDouble(Value[0][2]);
                        Power_pump = Convert.ToDouble(Value[0][3]);
                        H_pump = Convert.ToDouble(Value[0][4]);
                        count_pump = Convert.ToDouble(Value[0][5]);
                        Pump pump1 = new Pump(Num_pump,A_pump,B_pump,V_pump,Power_pump,H_pump, Pump1Count, Pump1Valve, Pump1Control);;
                        Pump.Add(pump1);
                        string[][] Value_Control = Program.DB.getValue(DB.type.BaseDB_Heating, "펌프제어", "Cp1,Cp2", "펌프제어 = '" + Pump1Control + "'");
                        Cp1 = Convert.ToDouble(Value_Control[0][0]);
                        Cp2 = Convert.ToDouble(Value_Control[0][1]);
                        if(Pump1Valve == "있음")
                        {
                            fhydr = 1;
                        }
                        else
                        {
                            fhydr = 1.25; 
                        }
                        if(Pump1 == null || Pump1 == "")
                        {
                            f_dpm = 1;
                        }
                        else
                        {
                            f_dpm = 0.45;
                        }
                        dPz = H_pump * 1000 * 9.81;
                        for(int mth =0; mth < 12; mth++)
                        {
                            if (Value2.Length >0)
                            { Vz[mth] = Qh_max_sum/1000 * Convert.ToDouble(Value[0][3]) * Pump1Count / (Convert.ToDouble(Value[0][3])*Pump1Count + Convert.ToDouble(Value2[0][3])*Pump2Count) * 3.6 / (dtheta_d[mth] * 4.18); } //2개일 경우 펌프 파워별로 나눠서 분담한 것으로 계산 
                            else
                            { Vz[mth] = Qh_max_sum/1000 * 3.6 / (dtheta_d[mth] * 4.18); } //2개일 경우 펌프 파워별로 나눠서 분담한 것으로 계산 
                            P_hydr[mth] = dPz* Vz[mth] / 3600;
                            fe[mth] = (1.25 + 200 / P_hydr[mth]) * 2;
                            e_hydr[mth] = fe[mth] * (Cp1 + Cp2 /beta_h_d[mth] ) * 0.25 / 0.25;
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
                            if (Value2.Length >0)
                            { Vz[mth] = Qh_max_sum/1000 * Convert.ToDouble(Value2[0][3]) * Pump2Count / (Convert.ToDouble(Value[0][3]) * Pump1Count + Convert.ToDouble(Value2[0][3]) * Pump2Count) * 3.6 / (dtheta_d[mth] * 4.18); } //2개일 경우 펌프 파워별로 나눠서 분담한 것으로 계산 
                            else
                            { Vz[mth] = Qh_max_sum/1000 * 3.6 / (dtheta_d[mth] * 4.18); } //2개일 경우 펌프 파워별로 나눠서 분담한 것으로 계산 
                            P_hydr[mth] = dPz * Vz[mth] / 3600;
                            fe[mth] = (1.25 + 200 / P_hydr[mth]) * 2;
                            e_hydr[mth] = fe[mth] * (Cp1 + Cp2 / beta_h_d[mth] ) * 0.25 / 0.25;
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
                beta_h_s[mth] = (Qhb_mth_sum[mth] + Qh_ce[mth]+ Qh_d[mth]) / (Qh_max_sum/1000 * th_avg[mth]);
                if (double.IsNaN(beta_h_s[mth])) { beta_h_s[mth]=0; }

                theta_SL_beta[mth] = (SL - theta_i_h_set_avg) * Math.Pow(beta_h_s[mth], 1 / 1.3) + theta_i_h_set_avg;
                theta_RL_beta[mth] = (RL - theta_i_h_set_avg) * Math.Pow(beta_h_s[mth], 1 / 1.3) + theta_i_h_set_avg;

                dtheta_s[mth] = theta_SL_beta[mth] - theta_RL_beta[mth];
                theta_av_s[mth] = 0.5 * (theta_SL_beta[mth] + theta_RL_beta[mth]);
                if (double.IsNaN(dtheta_s[mth])) { dtheta_s[mth] = 0; }
                if (double.IsNaN(theta_av_s[mth])) { theta_av_s[mth]=0; }
            }
        }   
    public void Calc_Qh_s()
     {
            double Qs_po_day =0;
            double[] thetai = new double[12];
            if(Vs> 0)
            {
                Qs_po_day = 0.4 + 0.14 * Math.Pow(Vs, 0.5);
            }
            for(int mth = 0; mth<12; mth++)
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
                for(int mth = 0;mth<12; mth++)
                {
                    double tPu = beta_h_s[mth] * 24 * dhrB[mth];
                    Wh_s[mth] = Convert.ToDouble(Value[0][0]) * tPu/1000;
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
                beta_h_gen[mth] = (Qhb_mth_sum[mth] + Qh_ce[mth] + Qh_d[mth] + Qh_s[mth]) / (Qh_max_sum/1000 * th_avg[mth]);
                if (double.IsNaN(beta_h_gen[mth])) { beta_h_gen[mth]=0; }
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
                    String Carrier = Value[0][2];
                    String Type = Value[0][3];
                    double Power = Convert.ToDouble(Value[0][4]);
                    double eta_Pn = Convert.ToDouble(Value[0][5])/100;
                    double eta_Pint = Convert.ToDouble(Value[0][6])/100;
                    double W = Convert.ToDouble(Value[0][7]);
                    double W_0 = Convert.ToDouble(Value[0][8]);
                    double count = Convert.ToDouble(BoilerNum_split[n]);
                    Value = Program.DB.getValue(DB.type.BaseDB_Heating, "보일러", "온도보정계수K,온도보정계수L,대기상태열손실E,대기상태열손실F,보조설비G_Pn,보조설비H_Pn,보조설비n_Pn,보조설비G_Pint,보조설비H_Pint,보조설비n_Pint", "종류 = '" + Type+ "'");
                    double K= Convert.ToDouble(Value[0][0]); 
                    double L = Convert.ToDouble(Value[0][1]);
                    double E = Convert.ToDouble(Value[0][2]); 
                    double F = Convert.ToDouble(Value[0][3]); 
                    double G_Pn = Convert.ToDouble(Value[0][4]);
                    double H_Pn = Convert.ToDouble(Value[0][5]);
                    double n_Pn = Convert.ToDouble(Value[0][6]);
                    double G_Pint = Convert.ToDouble(Value[0][7]);
                    double H_Pint = Convert.ToDouble(Value[0][8]);
                    double n_Pint = Convert.ToDouble(Value[0][9]);
                    Boiler boiler = new Boiler(Num, Combi, Carrier, Type, Power, eta_Pn, eta_Pint, W, W_0, count , K, L, E, F, G_Pn, H_Pn, n_Pn, G_Pint, H_Pint, n_Pint);
                    double theta_pn, theta_pint, theta_con;
                    theta_pn = (80 + 60) / 2;
                    theta_pint = (80 + 30) / 2;
                    theta_con = (50 + 30) / 2;
                    Value = Program.DB.getValue(DB.type.BaseDB_Heating, "연소난방비", "연소난방비", "연료 = '" + Carrier + "'");
                    double fHN_HI = Convert.ToDouble(Value[0][0]);
                    double qP0_70 = E*Math.Pow(Power, F)/100;
                    double tw_Pn_day = 1; //나중에 급탕과 연결 해야 함 
                    double[] Pd_in = new double[12], Pgen_Pn = new double[12], Pgen_Pint = new double[12], Pgen_P0 = new double[12], eta_gen_Pn = new double[12], eta_gen_Pint = new double[12];
                    double beta_gen_pint = 0.3, qp0_theta;
                    double[] Qh_gen_day = new double[12]; double[] Qh_gen_mth = new double[12];
                   
                    for (int mth =0; mth <12; mth++)
                    {
                        Pd_in[mth] = Qh_outg[mth] / th_avg[mth];

                        eta_gen_Pn[mth] = eta_Pn + K*(theta_pn - theta_av_gen[mth]);
                        Pgen_Pn[mth] = Math.Max((fHN_HI - eta_gen_Pn[mth]) / eta_gen_Pn[mth] * Pd_in[mth],0);

                        eta_gen_Pint[mth] = eta_Pint + L * (theta_pint - theta_av_gen[mth]);
                        Pgen_Pint[mth] = Math.Max((fHN_HI - eta_gen_Pint[mth]) / eta_gen_Pint[mth] *beta_gen_pint* Pd_in[mth],0);

                        qp0_theta = Math.Max(qP0_70 * (theta_av_gen[mth] - theta_ih_avg[mth]) / 50, 0);
                        Pgen_P0[mth] = qp0_theta * Pd_in[mth]/ eta_Pn * fHN_HI;
                        if (beta_h_gen[mth] <= beta_gen_pint)
                        {
                            Qh_gen_day[mth] = (beta_h_gen[mth] / beta_gen_pint * (Pgen_Pint[mth] - Pgen_P0[mth]) + Pgen_P0[mth]) * (thrL_day[mth] - tw_Pn_day);
                        }
                        else
                        {
                            Qh_gen_day[mth] = ((beta_h_gen[mth] - beta_gen_pint) / (1 - beta_gen_pint) * (Pgen_Pn[mth] - Pgen_Pint[mth]) + Pgen_Pint[mth]) * (thrL_day[mth] - tw_Pn_day);
                        }

                        Qh_gen_mth[mth]  = Qh_gen_day[mth] * dhrB[mth];
                        Qh_gen[mth] += Qh_gen_mth[mth];
                        Qh_f[mth] = Qh_outg[mth] + Qh_gen[mth];
                    }

                    double Paux_Pn, Paux_Pint; 
                    Paux_Pn = Math.Min((G_Pn + H_Pn * Math.Pow(Power, n_Pn)) / 1000, W/1000);
                    Paux_Pint = Math.Min((G_Pint + H_Pint * Math.Pow(Power, n_Pint)) / 1000, W/1000);
                    double[] Ph_gen_aux = new double[12], Wh_g_i = new double[12];
                    for (int mth  = 0; mth < 12;mth++)
                    {
                        Ph_gen_aux[mth] = (beta_h_gen[mth] / beta_gen_pint * (Paux_Pint - W_0/1000) + W_0/1000);
                        Ph_gen_aux[mth] = ((beta_h_gen[mth] - beta_gen_pint) / (1 - beta_gen_pint) * (Paux_Pn - Paux_Pint) + Paux_Pint);
                        Wh_g_i[mth] = Ph_gen_aux[mth] * (thrL[mth] - tw_Pn_day * dop_mth_avg[mth]) + W_0/1000 * (24 * dmth[mth] - thrL[mth]);
                        Wh_g[mth] += Wh_g_i[mth];
                    }

                }
                catch { }
               
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
        public Pump(String Num, double A, double B, double V, double Power, double H, double count, String Valve, String Control )
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
        String Num_Boiler; string combi_Boiler; String Carrier_Boiler;String Type_Boiler; double Power_Boiler; double eta_Pn_Boiler; double eta_Pint_Boiler; double W_Boiler; double W_0_Boiler; double count_Boiler;
        double K_Boiler, L_Boiler, E_Boiler, F_Boiler, G_pn_Boiler, H_pn_Boiler, n_pn_Boiler, G_pint_Boiler, H_pint_Boiler, n_pint_Boiler;
        public Boiler(String Num, String Combi,String Carrier, String Type, double Power, double eta_Pn, double eta_Pint, double W, double W_0, double count, double K, double  L, double  E, double  F, double  G_pn, double  H_pn, double n_pn, double  G_pint, double  H_pint, double  n_pint_Boiler)
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
            this.H_pn_Boiler= H_pn;
            this.n_pn_Boiler = n_pn;
            this.G_pint_Boiler = G_pint;
            this.H_pint_Boiler= H_pint;
            this.n_pint_Boiler =n_pint_Boiler;
        }

        public string Num() { return this.Num_Boiler; }
        public string carreir() { return this.Carrier_Boiler; }
        public string Type() { return this.Type_Boiler; }
        public double Power() {return this.Power_Boiler; }
        public double eta_Pn() { return this.eta_Pn_Boiler; }
        public double eta_Pint() { return this.eta_Pint_Boiler;}
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


 }