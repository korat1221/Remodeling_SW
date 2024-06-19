using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main.contents.Result.Element_Report
{
    internal class Element_Saving
    {
        public string[] ElementAlt = CALC.ElementAlt;
        public double[] Element_ElecSum = new double[CALC.ElementAlt.Length];
        public double[] Element_GasSum = new double[CALC.ElementAlt.Length];
        public double[] Element_EnergySum = new double[CALC.ElementAlt.Length];
        public double[] Element_ElecSaving = new double[CALC.ElementAlt.Length];
        public double[] Element_GasSaving = new double[CALC.ElementAlt.Length];
        public double[] Element_EnergySaving = new double[CALC.ElementAlt.Length];
        public double Total_Energy_pre = 0;
        public double Total_EnergySaving = 0;
        public double Total_ElecSaving = 0;
        public double Total_GasSaving = 0;
        public ArrayList HeatingGroup = new ArrayList();
        public ArrayList CoolingGroup = new ArrayList();
        public ArrayList DHWGroup = new ArrayList();

        public void Calc_Element_Saving()
        {
            #region 요소기술별 절감량 비율 계산 
            string[][] res = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기존프로젝트");

            for (int a = 0; a < ElementAlt.Length; a++)
            {
                string[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result_Element", "총에너지소요량", "검토유형='" + ElementAlt[a] + "' And 연료='전기'");
                if (Value2.Length > 0)
                {
                    Element_ElecSum[a] += Convert.ToDouble(Value2[0][0]);
                }
                Value2 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result_Element", "총에너지소요량", "검토유형='" + ElementAlt[a] + "' And Not 연료='전기' and Not 연료='전체'");
                if (Value2.Length > 0)
                {
                    Element_GasSum[a] += Convert.ToDouble(Value2[0][0]);
                }

                Element_EnergySum[a] = Element_ElecSum[a] + Element_GasSum[a];
            }

            string[][] Final1 = Program.DB.querySQL(res[0][0], "Select 총에너지소요량 from FinalEnergy_Result Where 연료='전기' and 월 ='연간'");
            string[][] Final2 = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 from FinalEnergy_Result Where 연료='전기' and 월 ='연간'");
            if (Final1.Length > 0 && Final2.Length > 0)
            {
                Total_Energy_pre += Convert.ToDouble(Final1[0][0]);
                Total_ElecSaving += (Convert.ToDouble(Final1[0][0]) - Convert.ToDouble(Final2[0][0]));
            }

            Final1 = Program.DB.querySQL(res[0][0], "Select 총에너지소요량 from FinalEnergy_Result Where Not 연료='전기' and Not 연료='전체' and 월 ='연간'");
            Final2 = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 from FinalEnergy_Result Where Not 연료='전기' and Not 연료='전체' and 월 ='연간'");
            if (Final1.Length > 0 && Final2.Length > 0)
            {
                Total_Energy_pre += Convert.ToDouble(Final1[0][0]);
                Total_GasSaving += (Convert.ToDouble(Final1[0][0]) - Convert.ToDouble(Final2[0][0]));
            }

            Total_EnergySaving = Total_ElecSaving + Total_GasSaving;

            double sum_elec = 0;
            double sum_gas = 0;
            double sum_energy = 0;
            for (int a = 1; a < ElementAlt.Length; a++)
            {
                sum_elec += Element_ElecSum[0] - Element_ElecSum[a]; // 조닝 대비 절감량 
                sum_gas += Element_GasSum[0] - Element_GasSum[a];
                sum_energy += Element_EnergySum[0] - Element_EnergySum[a];
            }
            for (int a = 1; a < ElementAlt.Length; a++)
            {
                if (sum_elec == 0)
                { Element_ElecSaving[a] = 0; }
                else { Element_ElecSaving[a] = Total_ElecSaving * (Element_ElecSum[0] - Element_ElecSum[a]) / sum_elec; }
                if (sum_gas == 0)
                { Element_GasSaving[a] = 0; }
                else { Element_GasSaving[a] = Total_GasSaving * (Element_GasSum[0] - Element_GasSum[a]) / sum_gas; }
                Element_EnergySaving[a] = Element_ElecSaving[a] + Element_GasSaving[a];
            }
            #endregion

        }

        public void Calc_Heating_Saving()
        {
            string[][] HeatingNum = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "번호", "");
            if (HeatingNum.Length > 0)
            {
                HeatingGroup.Clear();
                #region 에너지
                for (int a = 0; a < HeatingNum.Length; a++)
                {
                    double before_energy = 0, after_energy = 0;
                    string[][] NewZone = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Heating_Result_Element", "계획존번호", "검토유형='난방' and 난방시스템='" + HeatingNum[a][0] + "'");
                    if (NewZone.Length > 0)
                    {
                        for (int aa = 0; aa < NewZone.Length; aa++)
                        {
                            string[][] NewSum_this = Program.DB.querySQL(DB.type.ProjDB, "Select Sum(난방소요량) From Heating_Result_Element where 검토유형='난방' and 계획존번호 ='" + NewZone[aa][0] + "' and 난방시스템 ='" + HeatingNum[a][0] + "'");
                            if (NewSum_this.Length > 0)
                            { after_energy += Convert.ToDouble(NewSum_this[0][0]); }
                            string[][] NewSum_All = Program.DB.querySQL(DB.type.ProjDB, "Select Sum(난방소요량) From Heating_Result_Element where 검토유형='난방' and 계획존번호 ='" + NewZone[aa][0] + "'");
                            string[][] Old_ = Program.DB.querySQL(DB.type.ProjDB, "Select Sum(난방소요량) From Heating_Result_Element where 검토유형='조닝' and 계획존번호 ='" + NewZone[aa][0] + "'");
                            if (Old_.Length > 0)
                            {
                                if (Old_[0][0] != "")
                                { before_energy += Convert.ToDouble(Old_[0][0]) * Convert.ToDouble(NewSum_this[0][0]) / Convert.ToDouble(NewSum_All[0][0]); }
                            }
                        }
                    }
                    #endregion                  

                    #region 기존시스템                   
                    ArrayList Num_Old = new ArrayList();
                    NewZone = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Heating_Result_Element", "계획존번호", "검토유형='난방' and 난방시스템='" + HeatingNum[a][0] + "' AND 연료='전기'");
                    if (NewZone.Length > 0)
                    {
                        for (int aa = 0; aa < NewZone.Length; aa++)
                        {
                            string[][] OldSystem = Program.DB.querySQL(DB.type.ProjDB, "SELECT DISTINCT 난방시스템 From Heating_Result_Element where 검토유형='조닝' and 계획존번호 ='" + NewZone[aa][0] + "'");
                            if (OldSystem.Length > 0)
                            {
                                for (int aaa = 0; aaa < OldSystem.Length; aaa++)
                                {
                                    if (Num_Old.Contains(OldSystem[aaa][0]))
                                    {

                                    }
                                    else
                                    {
                                        Num_Old.Add(OldSystem[aaa][0]);
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    Heating_New_Old heating = new Heating_New_Old(HeatingNum[a][0], Num_Old, before_energy, after_energy);
                    HeatingGroup.Add(heating);
                }

            }
        }
        public void Calc_Cooling_Saving()
        {
            string[][] CoolingNum = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "번호", "");
            if (CoolingNum.Length > 0)
            {
                CoolingGroup.Clear();
                #region 에너지
                for (int a = 0; a < CoolingNum.Length; a++)
                {
                    double before_energy = 0, after_energy = 0;
                    string[][] NewZone = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Cooling_Result_Element", "계획존번호", "검토유형='냉방' and 냉방시스템='" + CoolingNum[a][0] + "'");
                    if (NewZone.Length > 0)
                    {
                        for (int aa = 0; aa < NewZone.Length; aa++)
                        {
                            string[][] NewSum_this = Program.DB.querySQL(DB.type.ProjDB, "Select Sum(냉방소요량) From Cooling_Result_Element where 검토유형='냉방' and 계획존번호 ='" + NewZone[aa][0] + "' and 냉방시스템 ='" + CoolingNum[a][0] + "'");
                            if (NewSum_this.Length > 0)
                            { after_energy += Convert.ToDouble(NewSum_this[0][0]); }
                            string[][] NewSum_All = Program.DB.querySQL(DB.type.ProjDB, "Select Sum(냉방소요량) From Cooling_Result_Element where 검토유형='냉방' and 계획존번호 ='" + NewZone[aa][0] + "'");
                            string[][] Old_ = Program.DB.querySQL(DB.type.ProjDB, "Select Sum(냉방소요량) From Cooling_Result_Element where 검토유형='조닝' and 계획존번호 ='" + NewZone[aa][0] + "'");
                            if (Old_.Length > 0)
                            {
                                if (Old_[0][0] != "")
                                { before_energy += Convert.ToDouble(Old_[0][0]) * Convert.ToDouble(NewSum_this[0][0]) / Convert.ToDouble(NewSum_All[0][0]); }
                            }
                        }
                    }
                    #endregion                  

                    #region 기존시스템                   
                    ArrayList Num_Old = new ArrayList();
                    NewZone = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Cooling_Result_Element", "계획존번호", "검토유형='냉방' and 냉방시스템='" + CoolingNum[a][0] + "' AND 연료='전기'");
                    if (NewZone.Length > 0)
                    {
                        for (int aa = 0; aa < NewZone.Length; aa++)
                        {
                            string[][] OldSystem = Program.DB.querySQL(DB.type.ProjDB, "SELECT DISTINCT 냉방시스템 From Cooling_Result_Element where 검토유형='조닝' and 계획존번호 ='" + NewZone[aa][0] + "'");
                            if (OldSystem.Length > 0)
                            {
                                for (int aaa = 0; aaa < OldSystem.Length; aaa++)
                                {
                                    if (Num_Old.Contains(OldSystem[aaa][0]))
                                    {

                                    }
                                    else
                                    {
                                        Num_Old.Add(OldSystem[aaa][0]);
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    Cooling_New_Old Cooling = new Cooling_New_Old(CoolingNum[a][0], Num_Old, before_energy, after_energy);
                    CoolingGroup.Add(Cooling);
                }

            }
        }
        public void Calc_DHW_Saving()
        {
            string[][] DHWNum = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "번호", "");
            if (DHWNum.Length > 0)
            {
                DHWGroup.Clear();
                #region 에너지
                for (int a = 0; a < DHWNum.Length; a++)
                {
                    double before_energy = 0, after_energy = 0;
                    string[][] NewZone = Program.DB.getValue_SameCheck(DB.type.ProjDB, "DHWSystem_Result_Element", "계획존번호", "검토유형='급탕' and 급탕시스템='" + DHWNum[a][0] + "'");
                    if (NewZone.Length > 0)
                    {
                        for (int aa = 0; aa < NewZone.Length; aa++)
                        {
                            string[][] NewSum_this = Program.DB.querySQL(DB.type.ProjDB, "Select Sum(급탕소요량) From DHWSystem_Result_Element where 검토유형='급탕' and 계획존번호 ='" + NewZone[aa][0] + "' and 급탕시스템 ='" + DHWNum[a][0] + "'");
                            if (NewSum_this.Length > 0)
                            { after_energy += Convert.ToDouble(NewSum_this[0][0]); }
                            string[][] NewSum_All = Program.DB.querySQL(DB.type.ProjDB, "Select Sum(급탕소요량) From DHWSystem_Result_Element where 검토유형='급탕' and 계획존번호 ='" + NewZone[aa][0] + "'");
                            string[][] Old_ = Program.DB.querySQL(DB.type.ProjDB, "Select Sum(급탕소요량) From DHWSystem_Result_Element where 검토유형='조닝' and 계획존번호 ='" + NewZone[aa][0] + "'");
                            if (Old_.Length > 0)
                            {
                                if (Old_[0][0] != "")
                                { before_energy += Convert.ToDouble(Old_[0][0]) * Convert.ToDouble(NewSum_this[0][0]) / Convert.ToDouble(NewSum_All[0][0]); }
                            }
                        }
                    }
                    #endregion                  

                    #region 기존시스템                   
                    ArrayList Num_Old = new ArrayList();
                    NewZone = Program.DB.getValue_SameCheck(DB.type.ProjDB, "DHWSystem_Result_Element", "계획존번호", "검토유형='급탕' and 급탕시스템='" + DHWNum[a][0] + "' AND 연료='전기'");
                    if (NewZone.Length > 0)
                    {
                        for (int aa = 0; aa < NewZone.Length; aa++)
                        {
                            string[][] OldSystem = Program.DB.querySQL(DB.type.ProjDB, "SELECT DISTINCT 급탕시스템 From DHWSystem_Result_Element where 검토유형='조닝' and 계획존번호 ='" + NewZone[aa][0] + "'");
                            if (OldSystem.Length > 0)
                            {
                                for (int aaa = 0; aaa < OldSystem.Length; aaa++)
                                {
                                    if (Num_Old.Contains(OldSystem[aaa][0]))
                                    {

                                    }
                                    else
                                    {
                                        Num_Old.Add(OldSystem[aaa][0]);
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    DHW_New_Old dhw = new DHW_New_Old(DHWNum[a][0], Num_Old, before_energy, after_energy);
                    DHWGroup.Add(dhw);
                }

            }
        }
    }

    public class Heating_New_Old
    {
        string Num_New_h;
        double before_h, after_h;
        ArrayList Num_Old_h = new ArrayList();
        public Heating_New_Old(string Num_New, ArrayList Num_Old, double before_energy, double after_energy)
        {
            this.Num_New_h = Num_New;
            this.before_h = before_energy;
            this.after_h = after_energy;
            this.Num_Old_h = Num_Old;
        }

        public String Num_New()
        {
            return Num_New_h;
        }       
        public double Before_Energy()
        {
            return before_h;
        }
        public double After_Energy()
        {
            return after_h;
        }
              
        public ArrayList Num_Old()
        {
            return Num_Old_h;
        }
    }

    public class Cooling_New_Old
    {
        string Num_New_c;
        double before_c, after_c;
        ArrayList Num_Old_c = new ArrayList();
        public Cooling_New_Old(string Num_New, ArrayList Num_Old, double before_energy, double after_energy)
        {
            this.Num_New_c = Num_New;
            this.before_c = before_energy;
            this.after_c = after_energy;
            this.Num_Old_c = Num_Old;
        }

        public String Num_New()
        {
            return Num_New_c;
        }
        public double Before_Energy()
        {
            return before_c;
        }
        public double After_Energy()
        {
            return after_c;
        }

        public ArrayList Num_Old()
        {
            return Num_Old_c;
        }
    }
    public class DHW_New_Old
    {
        string Num_New_w;
        double before_w, after_w;
        ArrayList Num_Old_w = new ArrayList();
        public DHW_New_Old(string Num_New, ArrayList Num_Old, double before_energy, double after_energy)
        {
            this.Num_New_w = Num_New;
            this.before_w = before_energy;
            this.after_w = after_energy;
            this.Num_Old_w = Num_Old;
        }

        public String Num_New()
        {
            return Num_New_w;
        }
        public double Before_Energy()
        {
            return before_w;
        }
        public double After_Energy()
        {
            return after_w;
        }

        public ArrayList Num_Old()
        {
            return Num_Old_w;
        }
    }
}
