using main.contents.Result.Element_Report;
using Microsoft.Web.WebView2.Core;
using System.Collections;
using System.Diagnostics;

namespace main.contents.Result
{
    public partial class Element_RESystem : Form
    {
        bool scriptable = false;
        public Element_RESystem()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            InitializeAsync();
        }
        async void InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.WebMessageReceived += OnJSMessage;
            webView21.CoreWebView2.NavigationCompleted += OnNaviCompleted;
        }
        void OnJSMessage(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            try
            {
                String s = args.TryGetWebMessageAsString();
            }
            catch (Exception ex)
            {

            }
        }
        void OnNaviCompleted(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            scriptable = true;
        }
        public void runScript(string script)
        {
            if (scriptable)
            {
                webView21.CoreWebView2.ExecuteScriptAsync(script);
            }
        }

        public void LoadData(string ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            string script = null;
            string[][] 프로젝트유형 = Program.DB.querySQL(DB.type.ProjListDB, "Select type from projects where current = '1'");
            if(프로젝트유형.Length >0)
            {

                if (프로젝트유형[0][0] == "1" || 프로젝트유형[0][0] == "4")
                {
                    script = Report_Before();
                }
                else
                {
                    script = Report_After();
                }
            }           
            runScript(script);
        }
        public string Report_Before()
        {
            string script = null;
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

            string script = null;
            string s, s2;
            string[][] 번호 = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
            string[][] res = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기존프로젝트");
            List<object> items = new List<object>();
            List<object> items2 = new List<object>();
            List<object> data = new List<object>();
            List<object>[] solar_data = new List<object>[700];
            double d;
            string sp;
            int i = -1, n;
            while (++i < 700)
            {
                solar_data[i] = new List<object>();
            }
            string charts = "";
            i = -1;
            while (++i < 번호.Length)
            {
                if (res.Length > 0)
                {

                    #region 태양광
                    int j_태양광 = 0;
                    for (int a = 0; a < ElementAlt.Length; a++)
                    {
                        if (ElementAlt[a] == "태양광")
                        {
                            j_태양광 = a; break;
                        }
                    }
                    double solar_saving = Element_EnergySaving[j_태양광];
                    double solar_saving_elec = Element_ElecSaving[j_태양광];
                    double solar_saving_noelec = Element_GasSaving[j_태양광];

                    solar_data[0].Add(new { idx = i, val = solar_saving.ToString("#,##0") }); ; //절감량 
                    solar_data[1].Add(new { idx = i, val = (solar_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "solar_saving", data = solar_data[0] });
                    data.Add(new { cname = "solar_savingpercent", data = solar_data[1] });

                    d = (solar_saving / Total_Energy_pre * 100);
                    charts += "{donut:" + d + "},";

                    double solar_tCO2 = solar_saving_elec * 0.4747 / 1000000 * 1000 + solar_saving_noelec / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    double solar_TOE = solar_saving_elec * 0.00023 + solar_saving_noelec / 38.9 / 0.277778 * 0.00103;

                    solar_data[2].Add(new { idx = i, val = solar_tCO2.ToString("0.0") });  //tco2
                    solar_data[3].Add(new { idx = i, val = solar_TOE.ToString("0.0") });  //TOE 
                    data.Add(new { cname = "solar_tco2", data = solar_data[2] });
                    data.Add(new { cname = "solar_toe", data = solar_data[3] });

                    string[] solar_name = new string[8]; string[] solar_cell = new string[8]; double[] solar_eta = new double[8]; double[] solar_count = new double[8];
                    double[] solar_power = new double[8]; double[] solar_area_old = new double[8]; double[] solar_area_new = new double[8];
                    double[] solar_saving_element = new double[8]; double[] solar_point = new double[8];
                    double sum_old = 0; double sum_new = 0;
                    string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select a.번호,a.명칭,a.모듈번호,a.개수,a.개수,a.용량,a.면적,b.CELLTYPE,b.Kpk From PV_Form as a inner Join User_PV as b on a.모듈번호=b.번호");
                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            solar_name[a] = Value[a][1];
                            solar_cell[a] = Value[a][7];
                            solar_eta[a] = Convert.ToDouble(Value[a][8]);
                            solar_count[a] = Convert.ToDouble(Value[a][3]);
                            solar_power[a] = Convert.ToDouble(Value[a][5]);
                            solar_area_new[a] = Convert.ToDouble(Value[a][6]);
                            sum_new += Convert.ToDouble(Value[a][6]);
                            solar_point[a] = 100;
                        }
                    }
                    Value = Program.DB.getValue(res[0][0], "PV_Form", "면적");
                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            sum_old += Convert.ToDouble(Value[a][0]);
                        }
                        for (int a = 0; a < 8; a++)
                        {
                            if (sum_new > 0)
                            { solar_area_old[a] = sum_old * solar_area_new[a] / sum_new; }
                        }
                    }
                    double sum = 0;
                    for (int a = 0; a < 8; a++)
                    {
                        sum += (solar_area_new[a] - solar_area_old[a]);
                    }
                    for (int a = 0; a < 8; a++)
                    {
                        if (sum > 0)
                        {
                            solar_saving_element[a] = solar_saving * (solar_area_new[a] - solar_area_old[a]) / sum;
                        }
                    }
                    for (int a = 0; a < 8; a++)
                    {
                        solar_data[4 + a].Add(new { idx = i, val = solar_name[a] });//명칭
                        data.Add(new { cname = "solar_name" + a, data = solar_data[4 + a] });
                        if (solar_name[a] != null & solar_name[a] != "")
                        {
                            solar_data[12 + a].Add(new { idx = i, val = solar_cell[a] });//셀타입
                            data.Add(new { cname = "solar_cell" + a, data = solar_data[12 + a] });

                            solar_data[20 + a].Add(new { idx = i, val = solar_eta[a].ToString("0.00") });//효율
                            data.Add(new { cname = "solar_eta" + a, data = solar_data[20 + a] });

                            solar_data[28 + a].Add(new { idx = i, val = solar_count[a].ToString("0") });//개수
                            data.Add(new { cname = "solar_count" + a, data = solar_data[28 + a] });

                            solar_data[36 + a].Add(new { idx = i, val = solar_power[a].ToString("0.0") });//출력
                            data.Add(new { cname = "solar_power" + a, data = solar_data[36 + a] });

                            solar_data[44 + a].Add(new { idx = i, val = solar_area_old[a].ToString("0.0") });//기존면적
                            data.Add(new { cname = "solar_area_old" + a, data = solar_data[44 + a] });

                            solar_data[52 + a].Add(new { idx = i, val = solar_area_new[a].ToString("0.0") });//기존면적
                            data.Add(new { cname = "solar_area_new" + a, data = solar_data[52 + a] });

                            solar_data[60 + a].Add(new { idx = i, val = (solar_saving_element[a] / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                            data.Add(new { cname = "solar_saving_element" + a, data = solar_data[60 + a] });

                            d = solar_point[a];
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            solar_data[68 + a].Add(new { idx = i, val = sp });//성능점수
                            data.Add(new { cname = "solar_point" + a, data = solar_data[68 + a] });
                        }
                    }
                    double solar_eta_avg = 0, solar_count_sum = 0, solar_power_sum = 0, solar_area_old_sum = 0, solar_area_new_sum = 0, solar_point_avg = 0;
                    for (int a = 0; a < 8; a++)
                    {
                        solar_eta_avg += solar_eta[a] * solar_power[a];
                        solar_count_sum += solar_count[a];
                        solar_power_sum += solar_power[a];
                        solar_area_old_sum += solar_area_old[a];
                        solar_area_new_sum += solar_area_new[a];
                        solar_point_avg += solar_point[a] * solar_power[a];
                    }
                    solar_eta_avg = solar_eta_avg / solar_power_sum;
                    solar_point_avg = solar_point_avg / solar_power_sum;
                    solar_data[76].Add(new { idx = i, val = solar_eta_avg.ToString("0.00") });
                    solar_data[77].Add(new { idx = i, val = solar_count_sum.ToString("0") });
                    solar_data[78].Add(new { idx = i, val = solar_power_sum.ToString("0.0") });
                    solar_data[79].Add(new { idx = i, val = solar_area_old_sum.ToString("0.0") });
                    solar_data[80].Add(new { idx = i, val = solar_area_new_sum.ToString("0.0") });
                    solar_data[81].Add(new { idx = i, val = (solar_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    d = solar_point_avg;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    solar_data[82].Add(new { idx = i, val = sp });//성능수준 평균  
                    data.Add(new { cname = "solar_eta_avg", data = solar_data[76] });
                    data.Add(new { cname = "solar_count_sum", data = solar_data[77] });
                    data.Add(new { cname = "solar_power_sum", data = solar_data[78] });
                    data.Add(new { cname = "solar_area_old_sum", data = solar_data[79] });
                    data.Add(new { cname = "solar_area_new_sum", data = solar_data[80] });
                    data.Add(new { cname = "solar_saving_sum", data = solar_data[81] });
                    data.Add(new { cname = "solar_point_avg", data = solar_data[82] });

                    #endregion


                    items.Add("Element_RESystem.htm");
                    s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                    s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
                    System.Text.Json.JsonSerializer.Serialize(solar_data[0].ToArray());

                    Debug.Print("start");

                    script = "init(" + s + "," + s2 + "," + "[" + charts + "])";
                    return script;
                }
            }
            return script;
        }       
    }
}