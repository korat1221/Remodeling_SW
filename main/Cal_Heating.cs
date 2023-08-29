using System;
using System.Collections;
using System.Collections.Generic;
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
        String StorageUse, StoragePumpUse, StoragePump; double Vs;
        String[] SystemType = { "보일러", "히트펌프", "흡수식온수기", "지역난방", "태양열시스템" };
        String[] ceType = { "실내기", "방열기", "팬코일유닛", "파워팬유닛", "복사난방" };
        int ZoneCount;
        ArrayList SelectZone_split = new ArrayList(); ArrayList SelectBoiler_split = new ArrayList();
        public double[] Qhb_mth_sum = new double[12]; public double[] theta_ih_avg = new double[12]; public double[] theta_e = new double[12]; public double[] theta_u = new double[12];
        double SL, RL;
        public Cal_Heating(String HeatingNum) 
        {
            this.HeatingNum = HeatingNum;
            double[,] Qhb_mth; double[,] theta_ih;

            //존 정보 불러오기
            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "명칭,존", "번호 = '" + HeatingNum + "'");

                HeatingName = Value[0][0];
                SelectZone_nonsplit = Value[0][1];
                Split_Zone(SelectZone_nonsplit);
                Qhb_mth = new double[SelectZone_split.Count, 12];
                theta_ih = new double[SelectZone_split.Count, 12];
                for (int n = 0; n < SelectZone_split.Count;  n++)
                {
                    Zone zone = Program.CALC.getZone(SelectZone_split[n].ToString());
                    if (zone != null)
                    {
                        for(int mth = 0 ; mth < 12; mth++)
                        {
                            Qhb_mth[n,mth] = zone.Qhb_mth[mth];
                            theta_ih[n, mth]= zone.theta_i[1,0,mth]; //이용일 난방
                        }                        
                    }
                    ZoneCount = ZoneCount + 1; 
                }
                for (int mth = 0; mth < 12; mth++)
                {
                    for (int n = 0; n < ZoneCount; n++)
                    {
                        Qhb_mth_sum[mth] += Qhb_mth[n, mth];
                        theta_ih_avg[mth] += theta_ih[n, mth];

                    }
                    theta_ih_avg[mth] = theta_ih_avg[mth] / ZoneCount;                   
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

            if (ce1Type != null && ce1Type != "")
            {
                try
                {
                   string[][] CE_Value = Program.DB.getValue(DB.type.ProjDB, "Heating_ce_Form", "설치위치", "난방시스템 = '" + HeatingNum + "' And 공급설비종류 = '" + ce1Type + "'");                   
                }
                catch { }
            }

            if (ce2Type != null && ce2Type != "")
            {
                try
                {
                    string[][] CE_Value = Program.DB.getValue(DB.type.ProjDB, "Heating_ce_Form", "설치위치", "난방시스템 = '" + HeatingNum + "' And 공급설비종류 = '" + ce2Type + "'");
                }
                catch { }
            }

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

    }
}