using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main.contents.Result.Element_Report
{
    internal class Element_Lighting
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
            element_saving.Calc_Cooling_Saving();
            ArrayList HeatingGroup = element_saving.HeatingGroup;
            ArrayList CoolingGroup = element_saving.CoolingGroup;
            
            string script=null; 
            string s, s2;
            string[][] 번호 = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
            string[][] res = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기존프로젝트");
            List<object> items = new List<object>();
            List<object> items2 = new List<object>();
            List<object> data = new List<object>();
            List<object>[] Light_data = new List<object>[700];
            List<object>[] Ventil_data = new List<object>[700];
            double d;
            string sp;
            int i = -1, n;
            while (++i < 700)
            {
                Light_data[i] = new List<object>();
                Ventil_data[i] = new List<object>();
            }
            string charts = "";
            i = -1;
            while (++i < 번호.Length)
            {
                if (res.Length > 0)
                {

                    #region 조명   
                    int j_조명 = 0;
                    for (int a = 0; a < ElementAlt.Length; a++)
                    {
                        if (ElementAlt[a] == "조명")
                        {
                            j_조명 = a; break;
                        }
                    }

                    double wall_saving = Element_EnergySaving[j_조명];

                    d = (wall_saving / Total_Energy_pre * 100);

                    Light_data[0].Add(new { idx = i, val = wall_saving.ToString("0.0") }); ; //절감량 
                    Light_data[1].Add(new { idx = i, val = d.ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "light_saving", data = Light_data[0] });
                    data.Add(new { cname = "light_savingpercent", data = Light_data[1] });

                    charts += "{donut:" + d + "},";

                    double light_saving_elec = Element_ElecSaving[j_조명];
                    double light_saving_noelec = Element_GasSaving[j_조명];

                    double light_tCO2 = light_saving_elec * 0.4747 / 1000000 * 1000;
                    double light_TOE = light_saving_elec * 0.00023;

                    Light_data[2].Add(new { idx = i, val = light_tCO2.ToString("0.0") });  //tco2
                    Light_data[3].Add(new { idx = i, val = light_TOE.ToString("0.0") });  //TOE 
                    data.Add(new { cname = "light_tco2", data = Light_data[2] });
                    data.Add(new { cname = "light_toe", data = Light_data[3] });

                    string[][] Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneLighting_form", "조명번호");
                    string[] Light_Name = new string[10]; string[] Light_Zone_text = new string[10]; double[] Light_eta = new double[10];
                    double[] Light_Area_Old = new double[10]; double[] Light_Area_New = new double[10]; double[] Light_Density_Old = new double[10]; double[] Light_Density_New = new double[10]; double[] Light_Saving = new double[10]; double[] Light_Point = new double[10];
                    double[] Light_elec = new double[10]; double[] Light_gas = new double[10]; 
                    ArrayList Light_Zones_split = new ArrayList();
                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {                           
                            string[][] 조명존 = Program.DB.querySQL(DB.type.ProjDB, "Select a.번호,a.순바닥면적,a.조명밀도,a.등기구명칭,a.광효율,a.자연채광유형,b.기존존 From ZoneLighting_form as a Inner Join ZoneGeneral_Form as b on a.번호 =b.존번호 where a.조명번호='" + Value[a][0] + "'");
                            if (조명존.Length > 0)
                            {
                                //명칭
                                Light_Name[a] = 조명존[0][3];

                                //공급존
                                if (조명존.Length > 1) { Light_Zone_text[a] = 조명존[0][0] + " 외 " + (조명존.Length - 1).ToString() + "개"; }
                                else { Light_Zone_text[a] = 조명존[0][0]; }
                                //효율
                                Light_eta[a] = Convert.ToDouble(조명존[0][4]);
                                //면적, 조명밀도 
                                Boolean samecheck = false;
                                for (int aa=0; aa < 조명존.Length; aa++)
                                {
                                    Light_Area_New[a] += Convert.ToDouble(조명존[aa][1]);
                                    Light_Density_New[a] += Convert.ToDouble(조명존[aa][1]) * Convert.ToDouble(조명존[aa][2]);
                                    ArrayList prezone = new ArrayList();
                                    prezone = Split_(조명존[0][6]);
                                    for(int aaa=0; aaa < prezone.Count; aaa++)
                                    {
                                        string[][] 기존존 = Program.DB.querySQL(res[0][0], "Select 번호,순바닥면적,조명밀도,조명번호 From ZoneLighting_form where 번호='" + prezone[aaa].ToString() + "'");
                                        if(기존존.Length > 0)
                                        {
                                            Light_Area_Old[a] += Convert.ToDouble(기존존[0][1]);
                                            Light_Density_Old[a] += Convert.ToDouble(기존존[0][1]) * Convert.ToDouble(기존존[0][2]);
                                            if (기존존[0][3] == Value[a][0])
                                            {
                                                samecheck = true;
                                            }
                                        }
                                    }
                                   
                                }
                                Light_Density_New[a] = Light_Density_New[a] / Light_Area_New[a];
                                if (samecheck)
                                {
                                    Light_Density_Old[a] = Light_Density_New[a];
                                }
                                else
                                {
                                    Light_Density_Old[a] = Light_Density_Old[a] / Light_Area_Old[a];
                                }            
                            }
                        }
                    }
                    for (int a = 0; a < 10; a++)
                    {
                        Light_data[4 + a].Add(new { idx = i, val = Light_Name[a] });//명칭
                        data.Add(new { cname = "light_name" + a, data = Light_data[4 + a] });
                        if (Light_Name[a] != null & Light_Name[a] != "")
                        {
                            Light_data[14 + a].Add(new { idx = i, val = Light_Zone_text[a] });//존
                            data.Add(new { cname = "light_zone" + a, data = Light_data[14 + a] });

                            Light_data[24 + a].Add(new { idx = i, val = Light_Area_New[a].ToString("0.0") });//면적
                            data.Add(new { cname = "light_area" + a, data = Light_data[24 + a] });

                            Light_data[34 + a].Add(new { idx = i, val = Light_eta[a].ToString("0.0") });//효율
                            data.Add(new { cname = "light_eta" + a, data = Light_data[34 + a] });

                            Light_data[44 + a].Add(new { idx = i, val = Light_Density_Old[a].ToString("0.0") });//기존 조명밀도
                            data.Add(new { cname = "light_density_old" + a, data = Light_data[44 + a] });

                            Light_data[54 + a].Add(new { idx = i, val = Light_Density_New[a].ToString("0.0") });//신규 조명밀도
                            data.Add(new { cname = "light_density_new" + a, data = Light_data[54 + a] });
                        }  
                    }

                    #endregion

                    items.Add("Element_Lighting.htm");
                    s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                    s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
                    System.Text.Json.JsonSerializer.Serialize(Light_data[0].ToArray());

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
