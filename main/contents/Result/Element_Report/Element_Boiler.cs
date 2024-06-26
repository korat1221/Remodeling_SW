using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace main.contents.Result.Element_Report
{
    internal class Element_Boiler
    {
        public string Report_Before()
        {
            string script=null;
            return script;
        }

        public string Report_After()
        {
            Element_Saving element_saving = new Element_Saving();
            element_saving.Calc_Element_Saving();
            string[] ElementAlt = element_saving.ElementAlt;
            double[] Element_ElecSum = element_saving.Element_ElecSum;
            double[] Element_GasSum = element_saving.Element_GasSum;
            double[] Element_EnergySum = element_saving.Element_EnergySum;
            double[] Element_ElecSaving = element_saving.Element_ElecSaving;
            double[] Element_GasSaving = element_saving.Element_GasSaving;
            double[] Element_EnergySaving = element_saving.Element_EnergySaving;
            double Total_Energy_pre = element_saving.Total_Energy_pre;
            double Total_EnergySaving = element_saving.Total_EnergySaving;
            double Total_ElecSaving = element_saving.Total_ElecSaving;
            double Total_GasSaving = element_saving.Total_GasSaving;
            element_saving.Calc_Heating_Saving();
            element_saving.Calc_DHW_Saving();
            ArrayList HeatingGroup = element_saving.HeatingGroup;
            ArrayList DHWGroup = element_saving.DHWGroup;

            string script=null; 
            string s, s2;
            string[][] 번호 = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
            string[][] res = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기존프로젝트");
            List<object> items = new List<object>();
            List<object> items2 = new List<object>();
            List<object> data = new List<object>();
            List<object>[] Boiler_data = new List<object>[700];
            List<object>[] Solar_data = new List<object>[700];
            List<object>[] WHP_data = new List<object>[700];
            double d;
            string sp;
            int i = -1, n;
            while (++i < 700)
            {
                Boiler_data[i] = new List<object>();
                Solar_data[i] = new List<object>();
                WHP_data[i] = new List<object>();
            }
            string charts = "";
            i = -1;
            while (++i < 번호.Length)
            {
                if (res.Length > 0)
                {

                    #region 난방+급탕 절약 : 모든 요소기술 적용 절감량 중                                
                    int j_heating = 0;
                    for (int a = 0; a < ElementAlt.Length; a++)
                    {
                        if (ElementAlt[a] == "난방")
                        {
                            j_heating = a; break;
                        }
                    }
                    double heating_saving = Element_EnergySaving[j_heating];
                    double heating_saving_elec = Element_ElecSaving[j_heating];
                    double heating_saving_noelec = Element_GasSaving[j_heating];

                    int j_dhw = 0;
                    for (int a = 0; a < ElementAlt.Length; a++)
                    {
                        if (ElementAlt[a] == "급탕")
                        {
                            j_dhw = a; break;
                        }
                    }
                    double dhw_saving = Element_EnergySaving[j_dhw];
                    double dhw_saving_elec = Element_ElecSaving[j_dhw];
                    double dhw_saving_noelec = Element_GasSaving[j_dhw];
                    #endregion

                    #region 난방 절약 : 각 난방설비별
                    double heating_saving_total = 0;
                    for (int aa = 0; aa < HeatingGroup.Count; aa++)
                    {
                        Heating_New_Old hh = (Heating_New_Old)HeatingGroup[aa];
                        heating_saving_total += hh.Before_Energy() - hh.After_Energy();
                    }

                    double[] heating_element_saving = new double[HeatingGroup.Count];
                    double[] heating_element_saving_elec = new double[HeatingGroup.Count];
                    double[] heating_element_saving_gas = new double[HeatingGroup.Count];
                    for (int aa = 0; aa < HeatingGroup.Count; aa++)
                    {
                        Heating_New_Old hh = (Heating_New_Old)HeatingGroup[aa];
                        heating_element_saving[aa] = heating_saving * (hh.Before_Energy() - hh.After_Energy()) / heating_saving_total;
                        heating_element_saving_elec[aa] = heating_saving_elec * (hh.Before_Energy() - hh.After_Energy()) / heating_saving_total;
                        heating_element_saving_gas[aa] = heating_saving_noelec * (hh.Before_Energy() - hh.After_Energy()) / heating_saving_total;
                    }
                    #endregion

                    #region 급탕 절약 : 각 급탕설비별
                    double dhw_saving_total = 0;
                    for (int aa = 0; aa < DHWGroup.Count; aa++)
                    {
                        DHW_New_Old ww = (DHW_New_Old)DHWGroup[aa];
                        dhw_saving_total += ww.Before_Energy() - ww.After_Energy();
                    }

                    double[] dhw_element_saving = new double[DHWGroup.Count];
                    double[] dhw_element_saving_elec = new double[DHWGroup.Count];
                    double[] dhw_element_saving_gas = new double[DHWGroup.Count];
                    for (int aa = 0; aa < DHWGroup.Count; aa++)
                    {
                        DHW_New_Old ww = (DHW_New_Old)DHWGroup[aa];
                        dhw_element_saving[aa] = dhw_saving * (ww.Before_Energy() - ww.After_Energy()) /dhw_saving_total;
                        dhw_element_saving_elec[aa] = dhw_saving_elec * (ww.Before_Energy() - ww.After_Energy()) / dhw_saving_total;
                        dhw_element_saving_gas[aa] = dhw_saving_noelec * (ww.Before_Energy() - ww.After_Energy()) / dhw_saving_total;
                    }
                    #endregion

                    #region 보일러 
                    string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.보일러종류,a.명칭,b.존,a.용량,a.전부하효율,a.난방급탕,b.번호,b.태양열번호,b.보일러대수 From User_Boiler as a Inner Join HeatingSystem_Form as b ON a.번호 = b.보일러종류 Where a.난방급탕='난방+급탕'");
                    string[] Boiler_Name = new string[8]; string[] Boiler_Zone_text = new string[8];
                    double[] Boiler_Power = new double[8]; double[] Boiler_eta_Old = new double[8]; double[] Boiler_eta_New = new double[8]; double[] Boiler_Saving = new double[8]; double[] Boiler_Point = new double[8]; double[] Boiler_eta_Rule = new double[8];
                    string[] Boiler_H_W = new string[8];
                    double[] Boiler_elec = new double[8];double[] Boiler_gas = new double[8];
                    ArrayList Boiler_Zones_split_H_W = new ArrayList(); ArrayList Boiler_Zones_split_W = new ArrayList(); ArrayList Boiler_Zones_split_H = new ArrayList();
                    int Boiler_HW_count = 0; int Boiler_H_count = 0; int Boiler_W_count = 0;
                    if (Value.Length > 0)
                    {
                        Boiler_HW_count = Value.Length;
                        for (int a = 0; a < Value.Length; a++)
                        {
                            for (int aa = 0; aa < HeatingGroup.Count; aa++)
                            {
                                Heating_New_Old hh = (Heating_New_Old)HeatingGroup[aa];

                                if (Value[a][6] == hh.Num_New())
                                {
                                    Boiler_Saving[a] = heating_element_saving[aa];
                                    Boiler_elec[a] = heating_element_saving_elec[aa];
                                    Boiler_gas[a] = heating_element_saving_gas[aa];
                                    for (int aaa = 0; aaa < hh.Num_Old().Count; aaa++)
                                    {
                                        string[][] OldSystem = Program.DB.querySQL(res[0][0], "Select a.전부하효율 From User_Boiler as a Inner Join HeatingSystem_Form as b ON a.번호 = b.보일러종류 Where b.번호 ='" + hh.Num_Old()[aaa] + "'");
                                        if (OldSystem.Length > 0)
                                        {
                                            if (Convert.ToDouble(OldSystem[0][0]) == Convert.ToDouble(Value[a][4]))
                                            { Boiler_eta_Old[a] = Convert.ToDouble(Value[a][4]); break; }
                                            else if (Convert.ToDouble(OldSystem[0][0]) < Boiler_eta_Old[a]) { Boiler_eta_Old[a] = Convert.ToDouble(OldSystem[0][0]); }
                                            else if (Boiler_eta_Old[a] == 0) { Boiler_eta_Old[a] = Convert.ToDouble(OldSystem[0][0]); }
                                        }
                                    }
                                    if (Value[a][7]!="")
                                    {
                                        double Q_sol_a = 0; 
                                        for(int mth =1; mth < 13; mth ++)
                                        {
                                            string[][] Solar = Program.DB.querySQL(DB.type.ProjDB, "Select a.Qh_sol From HeatingSystem_Result as a Inner Join HeatingSystem_Form as b ON a.번호 = b.번호 Where b.태양열번호 ='" + Value[a][7] + "' and 월='"+mth+"월'");
                                            if (Solar.Length > 0)
                                            {
                                                Q_sol_a += Convert.ToDouble(Solar[0][0]);
                                            }
                                        }
                                        Boiler_Saving[a] = Boiler_Saving[a] - Q_sol_a;
                                        if (Boiler_gas[a] > Boiler_elec[a]) { Boiler_gas[a] = Boiler_gas[a] - Q_sol_a; }
                                        else { Boiler_elec[a] = Boiler_elec[a] - Q_sol_a; }
                                    }
                                }
                            }

                            string[][] dhwvalue = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "번호", "보일러종류='" + Value[a][0] + "'");
                            if (dhwvalue.Length > 0)
                            {
                                for (int aa = 0; aa < DHWGroup.Count; aa++)
                                {
                                    DHW_New_Old cc = (DHW_New_Old)DHWGroup[aa];
                                    if (dhwvalue[0][0] == cc.Num_New())
                                    {
                                        Boiler_Saving[a] = Boiler_Saving[a] + dhw_element_saving[aa];
                                        Boiler_elec[a] = Boiler_elec[a] + dhw_element_saving_elec[aa];
                                        Boiler_gas[a] = Boiler_gas[a] + dhw_element_saving_gas[aa];
                                    }
                                }
                            }
                        }
                    }

                    if (Value.Length > 0)
                    {
                        for(int a = 0; a < Value.Length; a++)
                        {
                            Boiler_Name[a] = Value[a][1];
                            Boiler_H_W[a] = Value[a][5];
                            ArrayList splitzone = new ArrayList();
                            splitzone = Split_(Value[a][2]);
                            if (splitzone.Count > 1) { Boiler_Zone_text[a] = splitzone[0].ToString() + " 외 " + (splitzone.Count - 1).ToString() + "개"; }
                            else { Boiler_Zone_text[a] = splitzone[0].ToString(); }

                            for (int aa =0; aa < splitzone.Count; aa++)
                            {
                                if (Boiler_Zones_split_H_W.Contains(splitzone[aa]))
                                { }
                                else { Boiler_Zones_split_H_W.Add(splitzone[aa]); }
                                if (Boiler_Zones_split_H.Contains(splitzone[aa]))
                                { }
                                else { Boiler_Zones_split_H.Add(splitzone[aa]); }
                                if (Boiler_Zones_split_W.Contains(splitzone[aa]))
                                { }
                                else { Boiler_Zones_split_W.Add(splitzone[aa]); }
                            }
                            Boiler_Power[a] = Convert.ToDouble(Value[a][3]) * Convert.ToDouble(Value[a][8]);
                            Boiler_eta_New[a] = Convert.ToDouble(Value[a][4]);
                            Boiler_eta_Rule[a] = 90;
                            Boiler_Point[a] = Math.Min(100, Boiler_eta_New[a]/ Boiler_eta_Rule[a] * 100);
                        }                       
                    }

                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.보일러종류,a.명칭,b.존,a.용량,a.전부하효율,a.난방급탕,b.번호,b.태양열번호,b.보일러대수 From User_Boiler as a Inner Join HeatingSystem_Form as b ON a.번호 = b.보일러종류 Where a.난방급탕='난방'");

                    if (Value.Length > 0)
                    {
                        Boiler_H_count = Value.Length;
                        for (int a = 0; a < Value.Length; a++)
                        {
                            for (int aa = 0; aa < HeatingGroup.Count; aa++)
                            {
                                Heating_New_Old hh = (Heating_New_Old)HeatingGroup[aa];
                                if (Value[a][6] == hh.Num_New())
                                {
                                    Boiler_Saving[a + Boiler_HW_count] = heating_element_saving[aa];
                                    Boiler_elec[a + Boiler_HW_count] = heating_element_saving_elec[aa];
                                    Boiler_gas[a + Boiler_HW_count] = heating_element_saving_gas[aa];
                                    for (int aaa = 0; aaa < hh.Num_Old().Count; aaa++)
                                    {
                                        string[][] OldSystem = Program.DB.querySQL(res[0][0], "Select a.전부하효율 From User_Boiler as a Inner Join HeatingSystem_Form as b ON a.번호 = b.보일러종류 Where b.번호 ='" + hh.Num_Old()[aaa] + "'");
                                        if (OldSystem.Length > 0)
                                        {
                                            if (Convert.ToDouble(OldSystem[0][0]) == Convert.ToDouble(Value[a][4]))
                                            { Boiler_eta_Old[a+ Boiler_HW_count] = Convert.ToDouble(Value[a][4]); break; }
                                            else if (Convert.ToDouble(OldSystem[0][0]) < Boiler_eta_Old[a + Boiler_HW_count]) { Boiler_eta_Old[a + Boiler_HW_count] = Convert.ToDouble(OldSystem[0][0]); }
                                            else if (Boiler_eta_Old[a + Boiler_HW_count] == 0) { Boiler_eta_Old[a+ Boiler_HW_count] = Convert.ToDouble(OldSystem[0][0]); }
                                        }
                                    }
                                    if (Value[a][7] != "")
                                    {
                                        double Q_sol_a = 0;
                                        for (int mth = 1; mth < 13; mth++)
                                        {
                                            string[][] Solar = Program.DB.querySQL(DB.type.ProjDB, "Select a.Qh_sol From HeatingSystem_Result as a Inner Join HeatingSystem_Form as b ON a.번호 = b.번호 Where b.태양열번호 ='" + Value[a][7] + "' and 월='" + mth + "월'");
                                            if (Solar.Length > 0)
                                            {
                                                Q_sol_a += Convert.ToDouble(Solar[0][0]);
                                            }
                                        }
                                        Boiler_Saving[a] = Boiler_Saving[a] - Q_sol_a;
                                        if (Boiler_gas[a] > Boiler_elec[a]) { Boiler_gas[a] = Boiler_gas[a] - Q_sol_a; }
                                        else { Boiler_elec[a] = Boiler_elec[a] - Q_sol_a; }
                                    }
                                }
                            }
                        }
                    }

                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            Boiler_Name[a + Boiler_HW_count] = Value[a][1];
                            Boiler_H_W[a + Boiler_HW_count] = Value[a][5];

                            ArrayList splitzone = new ArrayList();
                            splitzone = Split_(Value[a][2]);
                            if (splitzone.Count > 1) { Boiler_Zone_text[a + Boiler_HW_count] = splitzone[0].ToString() + " 외 " + (splitzone.Count - 1).ToString() + "개"; }
                            else { Boiler_Zone_text[a + Boiler_HW_count] = splitzone[0].ToString(); }

                            for (int aa = 0; aa < splitzone.Count; aa++)
                            {
                                if (Boiler_Zones_split_H_W.Contains(splitzone[aa]))
                                { }
                                else { Boiler_Zones_split_H_W.Add(splitzone[aa]); }
                                if (Boiler_Zones_split_H.Contains(splitzone[aa]))
                                { }
                                else { Boiler_Zones_split_H.Add(splitzone[aa]); }
                            }


                            Boiler_Power[a + Boiler_HW_count] = Convert.ToDouble(Value[a][3]) * Convert.ToDouble(Value[a][8]);
                            Boiler_eta_New[a+ Boiler_HW_count] = Convert.ToDouble(Value[a][4]);
                            Boiler_eta_Rule[a + Boiler_HW_count] = 90;
                            Boiler_Point[a + Boiler_HW_count] = Math.Min(100, Boiler_eta_New[a + Boiler_HW_count] / Boiler_eta_Rule[a + Boiler_HW_count] * 100);
                        }
                    }

                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.보일러종류,a.명칭,b.존,a.용량,a.전부하효율,a.난방급탕,b.번호,b.태양열번호,b.보일러대수 From User_Boiler as a Inner Join DHWSystem_Form as b ON a.번호 = b.보일러종류 Where a.난방급탕='급탕'");

                    if (Value.Length > 0)
                    {
                        Boiler_W_count = Value.Length;
                        for (int a = 0; a < Value.Length; a++)
                        {
                            for (int aa = 0; aa < DHWGroup.Count; aa++)
                            {
                                DHW_New_Old ww = (DHW_New_Old)DHWGroup[aa];
                                if (Value[a][6] == ww.Num_New())
                                {
                                    Boiler_Saving[a + Boiler_HW_count + Boiler_H_count] = dhw_element_saving[aa];
                                    Boiler_elec[a + Boiler_HW_count + Boiler_H_count] = dhw_element_saving_elec[aa];
                                    Boiler_gas[a + Boiler_HW_count + Boiler_H_count] = dhw_element_saving_gas[aa];
                                    for (int aaa = 0; aaa < ww.Num_Old().Count; aaa++)
                                    {
                                        string[][] OldSystem = Program.DB.querySQL(res[0][0], "Select a.전부하효율 From User_Boiler as a Inner Join DHWSystem_Form as b ON a.번호 = b.보일러종류 Where b.번호 ='" + ww.Num_Old()[aaa] + "'");
                                        if (OldSystem.Length > 0)
                                        {
                                            if (Convert.ToDouble(OldSystem[0][0]) == Convert.ToDouble(Value[a][4]))
                                            { Boiler_eta_Old[a + Boiler_HW_count + Boiler_H_count] = Convert.ToDouble(Value[a][4]); break; }
                                            else if (Convert.ToDouble(OldSystem[0][0]) < Boiler_eta_Old[a + Boiler_HW_count + Boiler_H_count]) { Boiler_eta_Old[a + Boiler_HW_count + Boiler_H_count] = Convert.ToDouble(OldSystem[0][0]); }
                                            else if (Boiler_eta_Old[a + Boiler_HW_count + Boiler_H_count] == 0) { Boiler_eta_Old[a + Boiler_HW_count + Boiler_H_count] = Convert.ToDouble(OldSystem[0][0]); }
                                        }
                                    }
                                    if (Value[a][7] != "")
                                    {
                                        double Q_sol_a = 0;
                                        for (int mth = 1; mth < 13; mth++)
                                        {
                                            string[][] Solar = Program.DB.querySQL(DB.type.ProjDB, "Select a.Qw_sol From DHWSystem_Result as a Inner Join DHWSystem_Form as b ON a.번호 = b.번호 Where b.태양열번호 ='" + Value[a][7] + "' and 월='" + mth + "월'");
                                            if (Solar.Length > 0)
                                            {
                                                Q_sol_a += Convert.ToDouble(Solar[0][0]);
                                            }
                                        }
                                        Boiler_Saving[a] = Boiler_Saving[a] - Q_sol_a;
                                        if (Boiler_gas[a] > Boiler_elec[a]) { Boiler_gas[a] = Boiler_gas[a] - Q_sol_a; }
                                        else { Boiler_elec[a] = Boiler_elec[a] - Q_sol_a; }
                                    }
                                }
                            }
                        }
                    }

                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            Boiler_Name[a + Boiler_HW_count + Boiler_H_count] = Value[a][1];
                            Boiler_H_W[a + Boiler_HW_count + Boiler_H_count] = Value[a][5];

                            ArrayList splitzone = new ArrayList();
                            splitzone = Split_(Value[a][2]);
                            if (splitzone.Count > 1) { Boiler_Zone_text[a + Boiler_HW_count + Boiler_H_count] = splitzone[0].ToString() + " 외 " + (splitzone.Count - 1).ToString() + "개"; }
                            else { Boiler_Zone_text[a + Boiler_HW_count + Boiler_H_count] = splitzone[0].ToString(); }

                            for (int aa = 0; aa < splitzone.Count; aa++)
                            {
                                if (Boiler_Zones_split_H_W.Contains(splitzone[aa]))
                                { }
                                else { Boiler_Zones_split_H_W.Add(splitzone[aa]); }
                                if (Boiler_Zones_split_W.Contains(splitzone[aa]))
                                { }
                                else { Boiler_Zones_split_W.Add(splitzone[aa]); }
                            }
                            Boiler_Power[a + Boiler_HW_count + Boiler_H_count] = Convert.ToDouble(Value[a][3]) * Convert.ToDouble(Value[a][8]);
                            Boiler_eta_New[a + Boiler_HW_count + Boiler_H_count] = Convert.ToDouble(Value[a][4]);
                            Boiler_eta_Rule[a + Boiler_HW_count + Boiler_H_count] = 90;
                            Boiler_Point[a + Boiler_HW_count + Boiler_H_count] = Math.Min(100, Boiler_eta_New[a + Boiler_HW_count + Boiler_H_count] / Boiler_eta_Rule[a + Boiler_HW_count + Boiler_H_count] * 100);
                        }
                    }
                    for (int a = 0; a < 8; a++)
                    {
                        if (Boiler_Saving[a] < 0) { Boiler_Saving[a] = 0; }
                        if (Boiler_elec[a] < 0) { Boiler_elec[a] = 0; }
                        if (Boiler_gas[a] < 0) { Boiler_gas[a] = 0; }
                    }
                    double boiler_total_saving = 0; double boiler_total_elec = 0; double boiler_total_gas = 0;
                    double Boiler_eta_New_total = 0; double Boiler_eta_Old_total = 0; double Boiler_Point_total = 0;
                    for (int a = 0; a < 8; a++)
                    {
                        Boiler_data[a].Add(new { idx = i, val = Boiler_Name[a] });//명칭
                        data.Add(new { cname = "boiler_name" + a, data = Boiler_data[a] });
                        if(Boiler_Name[a]!= null & Boiler_Name[a]!="")
                        {
                            Boiler_data[8 + a].Add(new { idx = i, val = Boiler_Zone_text[a] });//존
                            data.Add(new { cname = "boiler_zone" + a, data = Boiler_data[8 + a] });

                            Boiler_data[16 + a].Add(new { idx = i, val = Boiler_Power[a].ToString("0.0") });//용량
                            data.Add(new { cname = "boiler_power" + a, data = Boiler_data[16 + a] });

                            Boiler_data[24 + a].Add(new { idx = i, val = Boiler_eta_New[a].ToString("0.0") });//효율
                            data.Add(new { cname = "boiler_eta_new" + a, data = Boiler_data[24 + a] });

                            if (Boiler_eta_Old[a] != 0)
                            { Boiler_data[32 + a].Add(new { idx = i, val = Boiler_eta_Old[a].ToString("0.0") }); }//기존 효율
                            else { Boiler_data[32 + a].Add(new { idx = i, val = "Not Boiler" }); }
                            data.Add(new { cname = "boiler_eta_old" + a, data = Boiler_data[32 + a] });
                            Boiler_data[40 + a].Add(new { idx = i, val = (Boiler_Saving[a] / Total_Energy_pre * 100).ToString("0.0") + " %" });//절감률
                            data.Add(new { cname = "boiler_saving" + a, data = Boiler_data[40 + a] });

                            d = Boiler_Point[a];
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 139) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 139는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 139) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 139는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 139) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            Boiler_data[48 + a].Add(new { idx = i, val = sp});//성능점수
                            data.Add(new { cname = "boiler_point" + a, data = Boiler_data[48 + a] });
                            if (Boiler_H_W[a] == "난방+급탕") { Boiler_data[56 + a].Add(new { idx = i, val = "난방급탕" }); }
                            else { Boiler_data[56 + a].Add(new { idx = i, val = Boiler_H_W[a] }); }//난방급탕여부
                            data.Add(new { cname = "boiler_h_w" + a, data = Boiler_data[56 + a] });
                        }

                        //가중평균 
                        Boiler_eta_New_total += Boiler_eta_New[a]* Boiler_Power[a];
                        Boiler_eta_Old_total += Boiler_eta_Old[a]* Boiler_Power[a];
                        Boiler_Point_total += Boiler_Point[a]* Boiler_Power[a];
                    }
                    if (Boiler_Power.Sum() > 0 )
                    {
                        Boiler_eta_New_total = Boiler_eta_New_total / Boiler_Power.Sum();
                        Boiler_eta_Old_total = Boiler_eta_Old_total / Boiler_Power.Sum();
                        Boiler_Point_total = Math.Min(100, Boiler_Point_total / Boiler_Power.Sum());
                    }                    

                    for (int a = 0; a < 8; a++)
                    {
                        boiler_total_saving += Boiler_Saving[a];
                        boiler_total_elec += Boiler_elec[a];
                        boiler_total_gas += Boiler_gas[a];
                    }

                    Boiler_data[64].Add(new { idx = i, val = boiler_total_saving.ToString("#,##0") });//절감량 전체 
                    data.Add(new { cname = "boiler_saving_total" , data = Boiler_data[64] });
                    Boiler_data[65].Add(new { idx = i, val = (boiler_total_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });//절감률 전체 
                    data.Add(new { cname = "boiler_saving_percent", data = Boiler_data[65] });
                    Boiler_data[66].Add(new { idx = i, val =(boiler_total_elec * 0.4747 / 1000000 * 1000 + boiler_total_gas / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000).ToString("0.0") });//tco2
                    data.Add(new { cname = "boiler_tco2", data = Boiler_data[66] });
                    Boiler_data[67].Add(new { idx = i, val = (boiler_total_elec * 0.00023 + boiler_total_gas / 43.1 / 0.277778 * 0.00103).ToString("0.0") });//절감량 전체 
                    data.Add(new { cname = "boiler_toe", data = Boiler_data[67] });

                    d = (boiler_total_saving / Total_Energy_pre * 100);
                    charts += "{donut:" + d + "},";

                    //합산 계 
                    Boiler_data[68].Add(new { idx = i, val = Boiler_Power.Sum().ToString("0.0") });//용량 합계  
                    data.Add(new { cname = "boiler_power_total", data = Boiler_data[68] });
                    Boiler_data[69].Add(new { idx = i, val = Boiler_eta_Old_total.ToString("0.0") });//기존 효율 평균  
                    data.Add(new { cname = "boiler_eta_old_total", data = Boiler_data[69] });
                    Boiler_data[70].Add(new { idx = i, val = Boiler_eta_New_total.ToString("0.0") });//효율 평균  
                    data.Add(new { cname = "boiler_eta_new_total", data = Boiler_data[70] });                  
                    Boiler_data[71].Add(new { idx = i, val = (boiler_total_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });//절감량 합계  
                    data.Add(new { cname = "boiler_saving_total2", data = Boiler_data[71] });
                    d = Boiler_Point_total;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 139) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 139는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 139) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 139는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 139) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    Boiler_data[72].Add(new { idx = i, val = sp });//성능수준 평균  
                    data.Add(new { cname = "boiler_point_total", data = Boiler_data[72] });


                    double Boiler_Qmax_h = 0; double Boiler_Qmax_w = 0; double Boiler_ZoneArea = 0; 
                    for(int a=0; a < Boiler_Zones_split_H_W.Count; a++)
                    {
                        string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "존번호='" + Boiler_Zones_split_H_W[a].ToString() + "'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                        {
                            Boiler_ZoneArea += Convert.ToDouble(ZoneValue[0][0]);
                        }
                    }
                    for (int a = 0; a < Boiler_Zones_split_H.Count; a++)
                    {
                        string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호='" + Boiler_Zones_split_H[a].ToString() + "' And 난방_냉방='난방' and 비이용일_이용일='이용일' and 월='1월'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                        {
                            Boiler_Qmax_h += Convert.ToDouble(ZoneValue[0][0]);
                        }
                    }
                    for (int a = 0; a < Boiler_Zones_split_W.Count; a++)
                    {
                        string[][]ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "일일급탕요구량,용도프로필", "존번호 = '" + Boiler_Zones_split_W[a].ToString() + "'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "" && ZoneValue[0][1] != "")
                        {
                            string[][] Usage = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "급탕시간당비율", "용도명 = '" + ZoneValue[0][1] + "'");
                            if (Usage.Length > 0)
                            { Boiler_Qmax_w += Convert.ToDouble(ZoneValue[0][0]) * Convert.ToDouble(Usage[0][0]); }
                        }
                    }
                    Boiler_data[73].Add(new { idx = i, val = (Boiler_Qmax_h/1000).ToString("0.0") });//난방부하 
                    data.Add(new { cname = "boiler_qmax_h", data = Boiler_data[73] });
                    Boiler_data[74].Add(new { idx = i, val = (Boiler_Qmax_w ).ToString("0.0") });//급탕부하 
                    data.Add(new { cname = "boiler_qmax_w", data = Boiler_data[74] });
                    Boiler_data[75].Add(new { idx = i, val = Boiler_ZoneArea.ToString("0.0") });//존면적
                    data.Add(new { cname = "boiler_zonearea", data = Boiler_data[75] });
                    Boiler_data[76].Add(new { idx = i, val = Boiler_Zones_split_H_W.Count.ToString() });//존개수 
                    data.Add(new { cname = "boiler_zonecount", data = Boiler_data[76] });

                    #endregion

                    #region 태양열
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.태양열번호,a.명칭,b.존,a.모듈면적,a.효율,a.난방급탕,b.번호,b.모듈개수 From User_Solar as a Inner Join DHWSystem_Form as b ON a.번호 = b.태양열번호 Where a.난방급탕='난방+급탕'");
                    string[] Solar_Name = new string[8]; string[] Solar_Zone_text = new string[8];
                    double[] Solar_marea_new = new double[8]; double[] Solar_marea_Old = new double[8]; double[] Solar_eta = new double[8]; double[] Solar_Saving = new double[8]; double[] Solar_Point = new double[8]; double[] Solar_eta_Rule = new double[8];
                    string[] Solar_H_W = new string[8];
                    double[] Solar_elec = new double[8]; double[] Solar_gas = new double[8];
                    ArrayList Solar_Zones_split_H_W = new ArrayList(); ArrayList Solar_Zones_split_W = new ArrayList(); ArrayList Solar_Zones_split_H = new ArrayList();
                    int Solar_HW_count = 0; int Solar_H_count = 0; int Solar_W_count = 0;
                    if (Value.Length > 0)
                    {
                        Solar_HW_count = Value.Length;
                        for (int a = 0; a < Value.Length; a++)
                        {
                            for (int aa = 0; aa < DHWGroup.Count; aa++)
                            {
                                DHW_New_Old cc = (DHW_New_Old)DHWGroup[aa];

                                if (Value[a][6] == cc.Num_New())
                                {
                                    Solar_Saving[a] = dhw_element_saving[aa];
                                    Solar_elec[a] = dhw_element_saving_elec[aa];
                                    Solar_gas[a] = dhw_element_saving_gas[aa];
                                    double Q_sol_a = 0;
                                    for (int mth = 1; mth < 13; mth++)
                                    {
                                        string[][] Solar = Program.DB.querySQL(DB.type.ProjDB, "Select Qw_sol From DHWSystem_Result  Where 번호 ='" + Value[a][6] + "' and 월='" + mth + "월'");
                                        if (Solar.Length > 0)
                                        {
                                            Q_sol_a += Convert.ToDouble(Solar[0][0]);
                                        }
                                    }
                                    if (Solar_Saving[a] > Q_sol_a)
                                    {
                                        Solar_Saving[a] = Q_sol_a;
                                        if(Solar_gas[a] > Solar_elec[a]) { Solar_gas[a] = Q_sol_a; Solar_elec[a] = 0; }
                                        else { Solar_elec[a] = Q_sol_a; Solar_gas[a] = 0; }
                                    }
                                    for (int aaa = 0; aaa < cc.Num_Old().Count; aaa++)
                                    {
                                        string[][] OldSystem = Program.DB.querySQL(res[0][0], "Select a.모듈면적,b.모듈개수,a.효율 From User_Solar as a Inner Join DHWSystem_Form as b ON a.번호 = b.태양열번호 Where b.번호 ='" + cc.Num_Old()[aaa] + "'");
                                        if (OldSystem.Length > 0)
                                        {
                                            Solar_marea_Old[a] = Convert.ToDouble(OldSystem[0][0]) * Convert.ToDouble(OldSystem[0][1]); 
                                        }
                                    }
                                }
                            }

                            string[][] heatingvalue = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "번호", "태양열번호 ='" + Value[a][0] + "'");
                            if (heatingvalue.Length > 0)
                            {
                                for (int aa = 0; aa < HeatingGroup.Count; aa++)
                                {
                                    Heating_New_Old hh = (Heating_New_Old)HeatingGroup[aa];
                                    if (heatingvalue[0][0] == hh.Num_New())
                                    {
                                        double Q_sol_a = 0;
                                        for (int mth = 1; mth < 13; mth++)
                                        {
                                            string[][] Solar = Program.DB.querySQL(DB.type.ProjDB, "Select a.Qh_sol From HeatingSystem_Result as a Inner Join HeatingSystem_Form as b ON a.번호 = b.번호 Where b.태양열번호 ='" + Value[a][0] + "' and 월='" + mth + "월'");
                                            if (Solar.Length > 0)
                                            {
                                                Q_sol_a += Convert.ToDouble(Solar[0][0]);
                                            }
                                        }
                                        if (heating_element_saving[aa] > Q_sol_a)
                                        {
                                            Solar_Saving[a] = Solar_Saving[a] + Q_sol_a;
                                            if (heating_element_saving_gas[aa] > heating_element_saving_elec[aa]) { Solar_gas[a] = Solar_gas[a]  + Q_sol_a; }
                                            else { Solar_elec[a] = Solar_elec[a] + Q_sol_a; }
                                        }
                                        else
                                        {
                                            Solar_Saving[a] = Solar_Saving[a] + heating_element_saving[aa];
                                            Solar_elec[a] = Solar_elec[a] + heating_element_saving_elec[aa];
                                            Solar_gas[a] = Solar_gas[a] + heating_element_saving_gas[aa];
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            Solar_Name[a] = Value[a][1];
                            Solar_H_W[a] = Value[a][5];
                            ArrayList splitzone = new ArrayList();
                            splitzone = Split_(Value[a][2]);
                            if (splitzone.Count > 1) { Solar_Zone_text[a] = splitzone[0].ToString() + " 외 " + (splitzone.Count - 1).ToString() + "개"; }
                            else { Solar_Zone_text[a] = splitzone[0].ToString(); }

                            for (int aa = 0; aa < splitzone.Count; aa++)
                            {
                                if (Solar_Zones_split_H_W.Contains(splitzone[aa]))
                                { }
                                else { Solar_Zones_split_H_W.Add(splitzone[aa]); }
                                if (Solar_Zones_split_H.Contains(splitzone[aa]))
                                { }
                                else { Solar_Zones_split_H.Add(splitzone[aa]); }
                                if (Solar_Zones_split_W.Contains(splitzone[aa]))
                                { }
                                else { Solar_Zones_split_W.Add(splitzone[aa]); }
                            }
                            Solar_marea_new[a] = Convert.ToDouble(Value[a][3]) * Convert.ToDouble(Value[a][7]);
                            Solar_eta[a] = Convert.ToDouble(Value[a][4]);
                            Solar_eta_Rule[a] = 0.88;
                            Solar_Point[a] = Math.Min(100, Solar_eta_Rule[a] / Solar_eta[a] *100);
                        }
                    }

                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.태양열번호,a.명칭,b.존,a.모듈면적,a.효율,a.난방급탕,b.번호,b.모듈개수 From User_Solar as a Inner Join HeatingSystem_Form as b ON a.번호 = b.태양열번호 Where a.난방급탕='난방'");

                    if (Value.Length > 0)
                    {
                        Solar_H_count = Value.Length;
                        for (int a = 0; a < Value.Length; a++)
                        {
                            for (int aa = 0; aa < HeatingGroup.Count; aa++)
                            {
                                Heating_New_Old hh = (Heating_New_Old)HeatingGroup[aa];
                                if (Value[a][6] == hh.Num_New())
                                {
                                    Solar_Saving[a + Solar_HW_count] = heating_element_saving[aa];
                                    Solar_elec[a + Solar_HW_count] = heating_element_saving_elec[aa];
                                    Solar_gas[a + Solar_HW_count] = heating_element_saving_gas[aa];
                                    double Q_sol_a = 0;
                                    for (int mth = 1; mth < 13; mth++)
                                    {
                                        string[][] Solar = Program.DB.querySQL(DB.type.ProjDB, "Select Qh_sol From HeatingSystem_Result  Where 번호 ='" + Value[a][6] + "' and 월='" + mth + "월'");
                                        if (Solar.Length > 0)
                                        {
                                            Q_sol_a += Convert.ToDouble(Solar[0][0]);
                                        }
                                    }
                                    if (Solar_Saving[a + Solar_HW_count] > Q_sol_a)
                                    {
                                        Solar_Saving[a + Solar_HW_count] = Q_sol_a;
                                        if (Solar_gas[a + Solar_HW_count] > Solar_elec[a + Solar_HW_count]) { Solar_gas[a + Solar_HW_count] = Q_sol_a; Solar_elec[a + Solar_HW_count] = 0; }
                                        else { Solar_elec[a + Solar_HW_count] = Q_sol_a; Solar_gas[a + Solar_HW_count] = 0; }
                                    }
                                    for (int aaa = 0; aaa < hh.Num_Old().Count; aaa++)
                                    {
                                        string[][] OldSystem = Program.DB.querySQL(res[0][0], "Select a.모듈면적,b.모듈개수,a.효율 From User_Solar as a Inner Join HeatingSystem_Form as b ON a.번호 = b.태양열번호 Where b.번호 ='" + hh.Num_Old()[aaa] + "'");
                                        if (OldSystem.Length > 0)
                                        {
                                            Solar_marea_Old[a + Solar_HW_count] = Convert.ToDouble(OldSystem[0][0]) * Convert.ToDouble(OldSystem[0][1]);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            Solar_Name[a + Solar_HW_count] = Value[a][1];
                            Solar_H_W[a + Solar_HW_count] = Value[a][5];

                            ArrayList splitzone = new ArrayList();
                            splitzone = Split_(Value[a][2]);
                            if (splitzone.Count > 1) { Solar_Zone_text[a + Solar_HW_count] = splitzone[0].ToString() + " 외 " + (splitzone.Count - 1).ToString() + "개"; }
                            else { Solar_Zone_text[a + Solar_HW_count] = splitzone[0].ToString(); }

                            for (int aa = 0; aa < splitzone.Count; aa++)
                            {
                                if (Solar_Zones_split_H_W.Contains(splitzone[aa]))
                                { }
                                else { Solar_Zones_split_H_W.Add(splitzone[aa]); }
                                if (Solar_Zones_split_H.Contains(splitzone[aa]))
                                { }
                                else { Solar_Zones_split_H.Add(splitzone[aa]); }
                            }


                            Solar_marea_new[a + Solar_HW_count] = Convert.ToDouble(Value[a][3]) * Convert.ToDouble(Value[a][7]);
                            Solar_eta[a + Solar_HW_count] = Convert.ToDouble(Value[a][4]);
                            Solar_eta_Rule[a + Solar_HW_count] = 0.88;
                            Solar_Point[a + Solar_HW_count] = Math.Min(100, Solar_eta_Rule[a + Solar_HW_count] / Solar_eta[a + Solar_HW_count] * 100);
                        }
                    }

                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.태양열번호,a.명칭,b.존,a.모듈면적,a.효율,a.난방급탕,b.번호,b.모듈개수 From User_Solar as a Inner Join DHWSystem_Form as b ON a.번호 = b.태양열번호 Where a.난방급탕='급탕'");

                    if (Value.Length > 0)
                    {
                        Solar_W_count = Value.Length;
                        for (int a = 0; a < Value.Length; a++)
                        {
                            for (int aa = 0; aa < DHWGroup.Count; aa++)
                            {
                                DHW_New_Old ww = (DHW_New_Old)DHWGroup[aa];
                                if (Value[a][6] == ww.Num_New())
                                {
                                    Solar_Saving[a + Solar_HW_count + Solar_H_count] = dhw_element_saving[aa];
                                    Solar_elec[a + Solar_HW_count + Solar_H_count] = dhw_element_saving_elec[aa];
                                    Solar_gas[a + Solar_HW_count + Solar_H_count] = dhw_element_saving_gas[aa];
                                    double Q_sol_a = 0;
                                    for (int mth = 1; mth < 13; mth++)
                                    {
                                        string[][] Solar = Program.DB.querySQL(DB.type.ProjDB, "Select Qw_sol From DHWSystem_Result  Where 번호 ='" + Value[a][6] + "' and 월='" + mth + "월'");
                                        if (Solar.Length > 0)
                                        {
                                            Q_sol_a += Convert.ToDouble(Solar[0][0]);
                                        }
                                    }
                                    if (Solar_Saving[a + Solar_HW_count + Solar_H_count] > Q_sol_a)
                                    {
                                        Solar_Saving[a + Solar_HW_count + Solar_H_count] = Q_sol_a;
                                        if (Solar_gas[a + Solar_HW_count + Solar_H_count] > Solar_elec[a + Solar_HW_count + Solar_H_count]) { Solar_gas[a + Solar_HW_count + Solar_H_count] = Q_sol_a; Solar_elec[a + Solar_HW_count + Solar_H_count] = 0; }
                                        else { Solar_elec[a + Solar_HW_count + Solar_H_count] = Q_sol_a; Solar_gas[a + Solar_HW_count + Solar_H_count] = 0; }
                                    }
                                    for (int aaa = 0; aaa < ww.Num_Old().Count; aaa++)
                                    {
                                        string[][] OldSystem = Program.DB.querySQL(res[0][0], "Select a.모듈면적,b.모듈개수,a.효율 From User_Solar as a Inner Join DHWSystem_Form as b ON a.번호 = b.태양열번호 Where b.번호 ='" + ww.Num_Old()[aaa] + "'");
                                        if (OldSystem.Length > 0)
                                        {
                                            Solar_marea_Old[a + Solar_HW_count + Solar_H_count] = Convert.ToDouble(OldSystem[0][0]) * Convert.ToDouble(OldSystem[0][1]);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            Solar_Name[a + Solar_HW_count + Solar_H_count] = Value[a][1];
                            Solar_H_W[a + Solar_HW_count + Solar_H_count] = Value[a][5];

                            ArrayList splitzone = new ArrayList();
                            splitzone = Split_(Value[a][2]);
                            if (splitzone.Count > 1) { Solar_Zone_text[a + Solar_HW_count + Solar_H_count] = splitzone[0].ToString() + " 외 " + (splitzone.Count - 1).ToString() + "개"; }
                            else { Solar_Zone_text[a + Solar_HW_count + Solar_H_count] = splitzone[0].ToString(); }

                            for (int aa = 0; aa < splitzone.Count; aa++)
                            {
                                if (Solar_Zones_split_H_W.Contains(splitzone[aa]))
                                { }
                                else { Solar_Zones_split_H_W.Add(splitzone[aa]); }
                                if (Solar_Zones_split_W.Contains(splitzone[aa]))
                                { }
                                else { Solar_Zones_split_W.Add(splitzone[aa]); }
                            }
                            Solar_marea_new[a + Solar_HW_count + Solar_H_count] = Convert.ToDouble(Value[a][3]) * Convert.ToDouble(Value[a][7]);
                            Solar_eta[a + Solar_HW_count + Solar_H_count] = Convert.ToDouble(Value[a][4]);
                            Solar_eta_Rule[a + Solar_HW_count + Solar_H_count] = 0.88;
                            Solar_Point[a + Solar_HW_count + Solar_H_count] = Math.Min(100, Solar_eta_Rule[a + Solar_HW_count + Solar_H_count] / Solar_eta[a + Solar_HW_count + Solar_H_count] * 100);
                        }
                    }

                    double solar_total_saving = 0; double solar_total_elec = 0; double solar_total_gas = 0;
                    double Solar_eta_New_total = 0; double Solar_eta_Old_total = 0; double Solar_Point_total = 0;
                    for (int a = 0; a < 8; a++)
                    {
                        Solar_data[a].Add(new { idx = i, val = Solar_Name[a] });//명칭
                        data.Add(new { cname = "solar_name" + a, data = Solar_data[a] });
                        if (Solar_Name[a] != null & Solar_Name[a] != "")
                        {
                            Solar_data[8 + a].Add(new { idx = i, val = Solar_Zone_text[a] });//존
                            data.Add(new { cname = "solar_zone" + a, data = Solar_data[8 + a] });

                            Solar_data[16 + a].Add(new { idx = i, val = Solar_eta[a].ToString("0.0") });//효율
                            data.Add(new { cname = "solar_eta" + a, data = Solar_data[16 + a] });

                            Solar_data[24 + a].Add(new { idx = i, val = Solar_marea_new[a].ToString("0.0") });//면적
                            data.Add(new { cname = "solar_marea_new" + a, data = Solar_data[24 + a] });

                            Solar_data[32 + a].Add(new { idx = i, val = Solar_marea_Old[a].ToString("0.0") }); //기존 면적
                            data.Add(new { cname = "solar_marea_old" + a, data = Solar_data[32 + a] });

                            Solar_data[40 + a].Add(new { idx = i, val = (Solar_Saving[a] / Total_Energy_pre * 100).ToString("0.0") + " %" });//절감률
                            data.Add(new { cname = "solar_saving" + a, data = Solar_data[40 + a] });

                            d = Solar_Point[a];
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 139) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 139는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 139) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 139는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 139) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            Solar_data[48 + a].Add(new { idx = i, val = sp});//성능점수
                            data.Add(new { cname = "solar_point" + a, data = Solar_data[48 + a] });

                            if (Solar_H_W[a] == "난방+급탕") { Solar_data[56 + a].Add(new { idx = i, val = "난방급탕" }); }
                            else { Solar_data[56 + a].Add(new { idx = i, val = Solar_H_W[a] }); }//난방급탕여부
                            data.Add(new { cname = "solar_h_w" + a, data = Solar_data[56 + a] });
                        }

                        //가중평균 
                        Solar_eta_New_total += Solar_eta[a] * Solar_marea_new[a];
                        Solar_eta_Old_total += Solar_marea_Old[a] * Solar_marea_new[a];
                        Solar_Point_total += Solar_Point[a] * Solar_marea_new[a];
                    }
                    if (Solar_marea_new.Sum() > 0)
                    {
                        Solar_eta_New_total = Solar_eta_New_total / Solar_marea_new.Sum();
                        Solar_eta_Old_total = Solar_eta_Old_total / Solar_marea_new.Sum();
                        Solar_Point_total = Math.Min(100, Solar_Point_total / Solar_marea_new.Sum());
                    }

                    for (int a = 0; a < 8; a++)
                    {
                        solar_total_saving += Solar_Saving[a];
                        solar_total_elec += Solar_elec[a];
                        solar_total_gas += Solar_gas[a];
                    }

                    Solar_data[64].Add(new { idx = i, val = solar_total_saving.ToString("#,##0") });//절감량 전체 
                    data.Add(new { cname = "solar_saving_total", data = Solar_data[64] });
                    Solar_data[65].Add(new { idx = i, val = (solar_total_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });//절감률 전체 
                    data.Add(new { cname = "solar_saving_percent", data = Solar_data[65] });
                    Solar_data[66].Add(new { idx = i, val = (solar_total_elec * 0.4747 / 1000000 * 1000 + solar_total_gas / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000).ToString("0.0") });//tco2
                    data.Add(new { cname = "solar_tco2", data = Solar_data[66] });
                    Solar_data[67].Add(new { idx = i, val = (solar_total_elec * 0.00023 + solar_total_gas / 43.1 / 0.277778 * 0.00103).ToString("0.0") });//절감량 전체 
                    data.Add(new { cname = "solar_toe", data = Solar_data[67] });

                    d = (solar_total_saving / Total_Energy_pre * 100);
                    charts += "{donut:" + d + "},";

                    //합산 계 
                    Solar_data[68].Add(new { idx = i, val = Solar_marea_new.Sum().ToString("0.0") });//용량 합계  
                    data.Add(new { cname = "solar_power_total", data = Solar_data[68] });
                    Solar_data[69].Add(new { idx = i, val = Solar_eta_Old_total.ToString("0.0") });//기존 효율 평균  
                    data.Add(new { cname = "solar_eta_old_total", data = Solar_data[69] });
                    Solar_data[70].Add(new { idx = i, val = Solar_eta_New_total.ToString("0.0") });//효율 평균  
                    data.Add(new { cname = "solar_eta_new_total", data = Solar_data[70] });
                    Solar_data[71].Add(new { idx = i, val = (Solar_Saving.Sum() / Total_Energy_pre * 100).ToString("0.0") + " %" });//절감량 합계  
                    data.Add(new { cname = "solar_saving_total2", data = Solar_data[71] });
                    d = Solar_Point_total;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 139) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 139는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 139) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 139는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 139) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    Solar_data[72].Add(new { idx = i, val = sp});//성능수준 평균  
                    data.Add(new { cname = "solar_point_total", data = Solar_data[72] });


                    double Solar_Qmax_h = 0; double Solar_Qmax_w = 0; double Solar_ZoneArea = 0;
                    for (int a = 0; a < Solar_Zones_split_H_W.Count; a++)
                    {
                        string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "존번호='" + Solar_Zones_split_H_W[a].ToString() + "'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                        {
                            Solar_ZoneArea += Convert.ToDouble(ZoneValue[0][0]);
                        }
                    }
                    for (int a = 0; a < Solar_Zones_split_H.Count; a++)
                    {
                        string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호='" + Solar_Zones_split_H[a].ToString() + "' And 난방_냉방='난방' and 비이용일_이용일='이용일' and 월='1월'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                        {
                            Solar_Qmax_h += Convert.ToDouble(ZoneValue[0][0]);
                        }
                    }
                    for (int a = 0; a < Solar_Zones_split_W.Count; a++)
                    {
                        string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "일일급탕요구량,용도프로필", "존번호 = '" + Solar_Zones_split_W[a].ToString() + "'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "" && ZoneValue[0][1] != "")
                        {
                            string[][] Usage = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "급탕시간당비율", "용도명 = '" + ZoneValue[0][1] + "'");
                            if (Usage.Length > 0)
                            { Solar_Qmax_w += Convert.ToDouble(ZoneValue[0][0]) * Convert.ToDouble(Usage[0][0]); }
                        }
                    }
                    Solar_data[73].Add(new { idx = i, val = (Solar_Qmax_h / 1000).ToString("0.0") });//난방부하 
                    data.Add(new { cname = "solar_qmax_h", data = Solar_data[73] });
                    Solar_data[74].Add(new { idx = i, val = (Solar_Qmax_w).ToString("0.0") });//급탕부하 
                    data.Add(new { cname = "solar_qmax_w", data = Solar_data[74] });
                    Solar_data[75].Add(new { idx = i, val = Solar_ZoneArea.ToString("0.0") });//존면적
                    data.Add(new { cname = "solar_zonearea", data = Solar_data[75] });
                    Solar_data[76].Add(new { idx = i, val = Solar_Zones_split_H_W.Count.ToString() });//존개수 
                    data.Add(new { cname = "solar_zonecount", data = Solar_data[76] });
                    #endregion

                    #region 급탕히트펌프 
                    string[] WHP_Name = new string[8]; string[] WHP_Zone_text = new string[8];
                    double[] WHP_Power = new double[8]; double[] WHP_COP_Old = new double[8]; double[] WHP_COP_New = new double[8]; double[] WHP_Saving = new double[8]; double[] WHP_Point = new double[8];
                    string[] WHP_H_W = new string[8];
                    double[] WHP_elec = new double[8]; double[] WHP_gas = new double[8];
                    ArrayList WHP_Zones_split_H_W = new ArrayList(); ArrayList WHP_Zones_split_W = new ArrayList(); ArrayList WHP_Zones_split_H = new ArrayList();
                  
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.히트펌프번호,a.명칭,b.존,a.급탕정격용량,a.급탕정격COP,a.난방급탕,b.번호,b.태양열번호,b.보일러대수 From User_DHWHP as a Inner Join DHWSystem_Form as b ON a.번호 = b.히트펌프번호 Where a.난방급탕='급탕'");

                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            for (int aa = 0; aa < DHWGroup.Count; aa++)
                            {
                                DHW_New_Old ww = (DHW_New_Old)DHWGroup[aa];
                                if (Value[a][6] == ww.Num_New())
                                {
                                    WHP_Saving[a] = dhw_element_saving[aa];
                                    WHP_elec[a] = dhw_element_saving_elec[aa];
                                    WHP_gas[a] = dhw_element_saving_gas[aa];
                                    for (int aaa = 0; aaa < ww.Num_Old().Count; aaa++)
                                    {
                                        string[][] OldSystem = Program.DB.querySQL(res[0][0], "Select a.급탕정격COP From User_DHWHP as a Inner Join DHWSystem_Form as b ON a.번호 = b.히트펌프번호 Where b.번호 ='" + ww.Num_Old()[aaa] + "'");
                                        if (OldSystem.Length > 0)
                                        {
                                            if (Convert.ToDouble(OldSystem[0][0]) == Convert.ToDouble(Value[a][4]))
                                            { WHP_COP_Old[a] = Convert.ToDouble(Value[a][4]); break; }
                                            else if (Convert.ToDouble(OldSystem[0][0]) < WHP_COP_Old[a]) { WHP_COP_Old[a] = Convert.ToDouble(OldSystem[0][0]); }
                                            else if (WHP_COP_Old[a] == 0) { WHP_COP_Old[a] = Convert.ToDouble(OldSystem[0][0]); }
                                        }
                                    }
                                    if (Value[a][7] != "")
                                    {
                                        double Q_sol_a = 0;
                                        for (int mth = 1; mth < 13; mth++)
                                        {
                                            string[][] Solar = Program.DB.querySQL(DB.type.ProjDB, "Select a.Qw_sol From DHWSystem_Result as a Inner Join DHWSystem_Form as b ON a.번호 = b.번호 Where b.태양열번호 ='" + Value[a][7] + "' and 월='" + mth + "월'");
                                            if (Solar.Length > 0)
                                            {
                                                Q_sol_a += Convert.ToDouble(Solar[0][0]);
                                            }
                                        }
                                        WHP_Saving[a] = WHP_Saving[a] - Q_sol_a;
                                        if (WHP_gas[a] > WHP_elec[a]) { WHP_gas[a] = WHP_gas[a] - Q_sol_a; }
                                        else { WHP_elec[a] = WHP_elec[a] - Q_sol_a; }
                                    }
                                }
                            }
                        }
                    }

                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            WHP_Name[a] = Value[a][1];
                            WHP_H_W[a] = Value[a][5];

                            ArrayList splitzone = new ArrayList();
                            splitzone = Split_(Value[a][2]);
                            if (splitzone.Count > 1) { WHP_Zone_text[a] = splitzone[0].ToString() + " 외 " + (splitzone.Count - 1).ToString() + "개"; }
                            else { WHP_Zone_text[a] = splitzone[0].ToString(); }

                            for (int aa = 0; aa < splitzone.Count; aa++)
                            {
                                if (WHP_Zones_split_H_W.Contains(splitzone[aa]))
                                { }
                                else { WHP_Zones_split_H_W.Add(splitzone[aa]); }
                                if (WHP_Zones_split_W.Contains(splitzone[aa]))
                                { }
                                else { WHP_Zones_split_W.Add(splitzone[aa]); }
                            }
                            WHP_Power[a] = Convert.ToDouble(Value[a][3]) * Convert.ToDouble(Value[a][8]);
                            WHP_COP_New[a] = Convert.ToDouble(Value[a][4]);
                        }
                    }
                    for (int a = 0; a < 8; a++)
                    {
                        if (WHP_Saving[a] < 0) { WHP_Saving[a] = 0; }
                        if (WHP_elec[a] < 0) { WHP_elec[a] = 0; }
                        if (WHP_gas[a] < 0) { WHP_gas[a] = 0; }
                    }
                    double WHP_total_saving = 0; double WHP_total_elec = 0; double WHP_total_gas = 0;
                    double WHP_eta_New_total = 0; double WHP_eta_Old_total = 0; double WHP_Point_total = 0;
                    for (int a = 0; a < 8; a++)
                    {
                        WHP_data[a].Add(new { idx = i, val = WHP_Name[a] });//명칭
                        data.Add(new { cname = "whp_name" + a, data = WHP_data[a] });
                        if (WHP_Name[a] != null & WHP_Name[a] != "")
                        {
                            WHP_data[8 + a].Add(new { idx = i, val = WHP_Zone_text[a] });//존
                            data.Add(new { cname = "whp_zone" + a, data = WHP_data[8 + a] });

                            WHP_data[16 + a].Add(new { idx = i, val = WHP_Power[a].ToString("0.0") });//용량
                            data.Add(new { cname = "whp_power" + a, data = WHP_data[16 + a] });

                            WHP_data[24 + a].Add(new { idx = i, val = WHP_COP_New[a].ToString("0.0") });//효율
                            data.Add(new { cname = "whp_cop_new" + a, data = WHP_data[24 + a] });

                            if (WHP_COP_Old[a] != 0)
                            { WHP_data[32 + a].Add(new { idx = i, val = WHP_COP_Old[a].ToString("0.0") }); }//기존 효율
                            else { WHP_data[32 + a].Add(new { idx = i, val = "Not HP" }); }
                            data.Add(new { cname = "whp_cop_old" + a, data = WHP_data[32 + a] });
                            WHP_data[40 + a].Add(new { idx = i, val = (WHP_Saving[a] / Total_Energy_pre * 100).ToString("0.0") + " %" });//절감률             
                            data.Add(new { cname = "whp_saving" + a, data = WHP_data[40 + a] });

                            d = WHP_Point[a];
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 139) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 139는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 139) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 139는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 139) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            WHP_data[48 + a].Add(new { idx = i, val = sp });//성능점수
                            data.Add(new { cname = "whp_point" + a, data = WHP_data[48 + a] });
                            if (WHP_H_W[a] == "난방+급탕") { WHP_data[56 + a].Add(new { idx = i, val = "난방급탕" }); }
                            else { WHP_data[56 + a].Add(new { idx = i, val = WHP_H_W[a] }); }//난방급탕여부
                            data.Add(new { cname = "whp_h_w" + a, data = WHP_data[56 + a] });
                        }

                        //가중평균 
                        WHP_eta_New_total += WHP_COP_New[a] * WHP_Power[a];
                        WHP_eta_Old_total += WHP_COP_Old[a] * WHP_Power[a];
                        WHP_Point_total += WHP_Point[a] * WHP_Power[a];
                    }
                    if (WHP_Power.Sum() > 0)
                    {
                        WHP_eta_New_total = WHP_eta_New_total / WHP_Power.Sum();
                        WHP_eta_Old_total = WHP_eta_Old_total / WHP_Power.Sum();
                        WHP_Point_total = WHP_Point_total / WHP_Power.Sum();
                    }

                    for (int a = 0; a < 8; a++)
                    {
                        WHP_total_saving += WHP_Saving[a];
                        WHP_total_elec += WHP_elec[a];
                        WHP_total_gas += WHP_gas[a];
                    }

                    WHP_data[64].Add(new { idx = i, val = WHP_total_saving.ToString("#,##0") });//절감량 전체 
                    data.Add(new { cname = "whp_saving_total", data = WHP_data[64] });
                    WHP_data[65].Add(new { idx = i, val = (WHP_total_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });//절감률 전체 
                    data.Add(new { cname = "whp_saving_percent", data = WHP_data[65] });
                    WHP_data[66].Add(new { idx = i, val = (WHP_total_elec * 0.4747 / 1000000 * 1000 + WHP_total_gas / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000).ToString("0.0") });//tco2
                    data.Add(new { cname = "whp_tco2", data = WHP_data[66] });
                    WHP_data[67].Add(new { idx = i, val = (WHP_total_elec * 0.00023 + WHP_total_gas / 43.1 / 0.277778 * 0.00103).ToString("0.0") });//절감량 전체 
                    data.Add(new { cname = "whp_toe", data = WHP_data[67] });

                    d = (WHP_total_saving / Total_Energy_pre * 100);
                    charts += "{donut:" + d + "},";

                    //합산 계 
                    WHP_data[68].Add(new { idx = i, val = WHP_Power.Sum().ToString("0.0") });//용량 합계  
                    data.Add(new { cname = "whp_power_total", data = WHP_data[68] });
                    WHP_data[69].Add(new { idx = i, val = WHP_eta_Old_total.ToString("0.0") });//기존 효율 평균  
                    data.Add(new { cname = "whp_eta_old_total", data = WHP_data[69] });
                    WHP_data[70].Add(new { idx = i, val = WHP_eta_New_total.ToString("0.0") });//효율 평균  
                    data.Add(new { cname = "whp_eta_new_total", data = WHP_data[70] });
                    WHP_data[71].Add(new { idx = i, val = (WHP_total_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });//절감량 합계  
                    data.Add(new { cname = "whp_saving_total2", data = WHP_data[71] });
                    d = WHP_Point_total;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 139) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 139는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 139) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 139는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 139) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    WHP_data[72].Add(new { idx = i, val =sp});//성능수준 평균  
                    data.Add(new { cname = "whp_point_total", data = WHP_data[72] });


                    double WHP_Qmax_h = 0; double WHP_Qmax_w = 0; double WHP_ZoneArea = 0;
                    for (int a = 0; a < WHP_Zones_split_H_W.Count; a++)
                    {
                        string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "존번호='" + WHP_Zones_split_H_W[a].ToString() + "'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                        {
                            WHP_ZoneArea += Convert.ToDouble(ZoneValue[0][0]);
                        }
                    }
                    for (int a = 0; a < WHP_Zones_split_H.Count; a++)
                    {
                        string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호='" + WHP_Zones_split_H[a].ToString() + "' And 난방_냉방='난방' and 비이용일_이용일='이용일' and 월='1월'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                        {
                            WHP_Qmax_h += Convert.ToDouble(ZoneValue[0][0]);
                        }
                    }
                    for (int a = 0; a < WHP_Zones_split_W.Count; a++)
                    {
                        string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "일일급탕요구량,용도프로필", "존번호 = '" + WHP_Zones_split_W[a].ToString() + "'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "" && ZoneValue[0][1] != "")
                        {
                            string[][] Usage = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "급탕시간당비율", "용도명 = '" + ZoneValue[0][1] + "'");
                            if (Usage.Length > 0)
                            { WHP_Qmax_w += Convert.ToDouble(ZoneValue[0][0]) * Convert.ToDouble(Usage[0][0]); }
                        }
                    }
                    WHP_data[73].Add(new { idx = i, val = (WHP_Qmax_h / 1000).ToString("0.0") });//난방부하 
                    data.Add(new { cname = "whp_qmax_h", data = WHP_data[73] });
                    WHP_data[74].Add(new { idx = i, val = (WHP_Qmax_w).ToString("0.0") });//급탕부하 
                    data.Add(new { cname = "whp_qmax_w", data = WHP_data[74] });
                    WHP_data[75].Add(new { idx = i, val = WHP_ZoneArea.ToString("0.0") });//존면적
                    data.Add(new { cname = "whp_zonearea", data = WHP_data[75] });
                    WHP_data[76].Add(new { idx = i, val = WHP_Zones_split_H_W.Count.ToString() });//존개수 
                    data.Add(new { cname = "whp_zonecount", data = WHP_data[76] });

                    #endregion

                    items.Add("Element_Boiler.htm");
                    s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                    s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
                    System.Text.Json.JsonSerializer.Serialize(Boiler_data[10].ToArray());

                    Debug.Print("start");

                    script = "init(" + s + "," + s2 + "," + "[" + charts + "])";
                    return script;
                }
            }
            return script;
        }
        private string Split_Zone(String nonSplit)
        {
            String 내용;
            ArrayList SelectZone_split = new ArrayList();
            if (nonSplit != null)
            {
                if (nonSplit.Contains("+"))
                {
                    string[] token = nonSplit.Split('+');
                    foreach (var item in token)
                    {
                        SelectZone_split.Add(item.ToString());
                    }
                    내용 = SelectZone_split[0].ToString() + " 외 " + (SelectZone_split.Count - 1).ToString() + "개";
                }
                else
                {
                    SelectZone_split.Clear();
                    SelectZone_split.Add(nonSplit);
                    내용 = SelectZone_split[0].ToString();
                }
            }
            else { 내용 = ""; }

            return 내용; 
        }
        private  ArrayList Split_(String nonSplit)
        {
            ArrayList split = new ArrayList();
            if (nonSplit != null)
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
            return split;
        }

    }
}
