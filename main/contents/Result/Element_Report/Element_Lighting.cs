using main.contents.Result.Element_Report;
using Microsoft.Web.WebView2.Core;
using System.Collections;
using System.Diagnostics;

namespace main.contents.Result
{
    public partial class Element_Lighting : Form
    {
        bool scriptable = false;
        public Element_Lighting()
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
            string s, s2;
            string[][] 번호 = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
            string[][] res = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기존프로젝트");
            List<object> items = new List<object>();
            List<object> items2 = new List<object>();
            List<object> data = new List<object>();
            List<object>[] Light_data = new List<object>[700];
            List<object>[] Infil_data = new List<object>[700];
            List<object>[] Ventil_data = new List<object>[700];
            double d;
            string sp;
            int i = -1, n;
            while (++i < 700)
            {
                Light_data[i] = new List<object>();
                Infil_data[i] = new List<object>();
                Ventil_data[i] = new List<object>();
            }
            string charts = "";
            i = -1;
            while (++i < 번호.Length)
            {
                if (res.Length > 0)
                {

                    #region 조명   

                    double Total_Energy_pre = 0;
                    string[][] value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='조명'");
                    string[][] value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                    double light_saving = 0;
                    if (value.Length > 0 && value3.Length > 0)
                    {
                        Total_Energy_pre = Convert.ToDouble(value3[0][0]);
                        light_saving = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value[0][0]));
                    }

                    value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전기' and 검토유형='조명'");
                    value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전기'");
                    double light_saving_elec = 0;
                    if (value.Length > 0 && value3.Length > 0)
                    {
                        light_saving_elec = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value[0][0]));
                    }
                    value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='조명'");
                    value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                    double light_saving_noelec = 0;
                    if (value.Length > 0 && value3.Length > 0)
                    {
                        light_saving_noelec = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value[0][0]));
                    }
                    Light_data[0].Add(new { idx = i, val = light_saving.ToString("#,##0") }); ; //절감량 
                    Light_data[1].Add(new { idx = i, val = (light_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "light_saving", data = Light_data[0] });
                    data.Add(new { cname = "light_savingpercent", data = Light_data[1] });

                    d = (light_saving / Total_Energy_pre * 100);
                    charts += "{donut:" + d + "},";

                    double light_tCO2 = Math.Max(0, light_saving_elec * 0.4747 / 1000000 * 1000);
                    double light_TOE = Math.Max(0, light_saving_elec * 0.00023);

                    Light_data[2].Add(new { idx = i, val = light_tCO2.ToString("0.0") });  //tco2
                    Light_data[3].Add(new { idx = i, val = light_TOE.ToString("0.0") });  //TOE 
                    data.Add(new { cname = "light_tco2", data = Light_data[2] });
                    data.Add(new { cname = "light_toe", data = Light_data[3] });

                    string[][] Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneLighting_form", "조명번호");
                    string[] Light_Name = new string[10]; string[] Light_Zone_text = new string[10]; double[] Light_eta = new double[10]; double[] Light_Density_Rule = new double[10];
                    double[] Light_Area_Old = new double[10]; double[] Light_Area_New = new double[10]; double[] Light_Density_New = new double[10]; double[] Light_Saving = new double[10]; double[] Light_Point = new double[10];
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
                                Light_Density_Rule[a] = 8;//에절계 조명부문 1번 1점
                                //면적, 조명밀도 
                                Boolean samecheck = false;
                                for (int aa = 0; aa < 조명존.Length; aa++)
                                {
                                    Light_Area_New[a] += Convert.ToDouble(조명존[aa][1]);
                                    Light_Density_New[a] += Convert.ToDouble(조명존[aa][1]) * Convert.ToDouble(조명존[aa][2]);
                                }
                                Light_Density_New[a] = Light_Density_New[a] / Light_Area_New[a];
                                Light_Point[a] = Math.Min(100, Light_Density_Rule[a] / Light_Density_New[a] * 100);
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

                            Light_data[54 + a].Add(new { idx = i, val = Light_Density_New[a].ToString("0.0") });//신규 조명밀도
                            data.Add(new { cname = "light_density_new" + a, data = Light_data[54 + a] });

                            d = Light_Point[a];
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            Light_data[64 + a].Add(new { idx = i, val = sp });//성능점수
                            data.Add(new { cname = "light_point" + a, data = Light_data[64 + a] });
                        }
                    }
                    double Light_Area_sum = 0, Light_eta_avg = 0, Light_Density_Old_avg = 0, Light_Density_New_avg = 0, Light_Point_avg = 0;
                    for (int a = 0; a < 10; a++)
                    {
                        Light_Area_sum += Light_Area_New[a];
                        Light_eta_avg += Light_eta[a] * Light_Area_New[a];
                        Light_Density_New_avg += Light_Density_New[a] * Light_Area_New[a];
                        Light_Point_avg += Light_Point[a] * Light_Area_New[a];
                    }
                    Light_eta_avg = Light_eta_avg / Light_Area_sum;
                    Light_Density_New_avg = Light_Density_New_avg / Light_Area_sum;
                    Light_Point_avg = Math.Min(100, Light_Point_avg / Light_Area_sum);
                    Light_data[84].Add(new { idx = i, val = Light_Area_sum.ToString("0.0") });  // 면적 합계
                    Light_data[85].Add(new { idx = i, val = Light_eta_avg.ToString("0.0") });  // 효율 평균
                    Light_data[87].Add(new { idx = i, val = Light_Density_New_avg.ToString("0.0") });  // 신규 밀도 평균
                    d = Light_Point_avg;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    Light_data[89].Add(new { idx = i, val = sp });//성능수준 평균  
                    data.Add(new { cname = "light_area_sum", data = Light_data[84] });
                    data.Add(new { cname = "light_eta_avg", data = Light_data[85] });
                    data.Add(new { cname = "light_density_new_avg", data = Light_data[87] });
                    data.Add(new { cname = "light_point_avg", data = Light_data[89] });


                    double facade_area = 0; double roof_area = 0; double facade_D = 0; double roof_D = 0; double light_zonecount = 0;
                    Value = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "번호,자연채광유형,순바닥면적");
                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            if (Value[a][1] == "파사드")
                            {
                                facade_area += Convert.ToDouble(Value[a][2]);

                                string[][] result = Program.DB.querySQL(DB.type.ProjDB, "Select AVG(f_D) From Zone_LightResult Where 번호='" + Value[a][0] + "'");
                                if (result.Length > 0)
                                {
                                    facade_D += Convert.ToDouble(Value[a][2]) * Convert.ToDouble(result[0][0]);
                                }
                            }
                            else if (Value[a][1] == "천창")
                            {
                                roof_area += Convert.ToDouble(Value[a][2]);

                                string[][] result = Program.DB.querySQL(DB.type.ProjDB, "Select AVG(r_DSNA) From Zone_LightResult Where 번호='" + Value[a][0] + "'");
                                if (result.Length > 0)
                                {
                                    roof_D += Convert.ToDouble(Value[a][2]) * Convert.ToDouble(result[0][0]);
                                }
                            }
                        }
                        if (facade_area > 0)
                        {
                            facade_D = facade_D / facade_area;
                        }

                        if (roof_area > 0)
                        {
                            roof_D = roof_D / roof_area;
                        }
                        light_zonecount = Value.Length;
                    }
                    Light_data[79].Add(new { idx = i, val = facade_D.ToString("0.0") });  // 파사드 주광률
                    Light_data[80].Add(new { idx = i, val = roof_D.ToString("0.0") });  //천창 주광률
                    Light_data[81].Add(new { idx = i, val = light_zonecount.ToString("0") });  //존 개수
                    data.Add(new { cname = "facade_d", data = Light_data[79] });
                    data.Add(new { cname = "roof_d", data = Light_data[80] });
                    data.Add(new { cname = "light_zonecount", data = Light_data[81] });
                    #endregion
                    #region 환기  
                    value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='기밀+열회수기'");
                    string[][] value2 = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='기밀'");
                    value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                    double ventil_saving = 0; double infil_saving = 0;
                    if (value.Length > 0 && value2.Length > 0 && value3.Length > 0)
                    {
                        ventil_saving = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value[0][0]));
                        infil_saving = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value2[0][0]));
                        ventil_saving = Math.Max(0, ventil_saving - infil_saving);
                    }

                    value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전기' and 검토유형='기밀+열회수기'");
                    value2 = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전기' and 검토유형='기밀'");
                    value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전기'");
                    double ventil_saving_elec = 0; double infil_saving_elec = 0;
                    if (value.Length > 0 && value2.Length > 0 && value3.Length > 0)
                    {
                        ventil_saving_elec = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value[0][0]));
                        infil_saving_elec = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value2[0][0]));
                        ventil_saving_elec = Math.Max(0, ventil_saving_elec - infil_saving_elec);
                    }
                    value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='기밀+열회수기'");
                    value2 = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='기밀'");
                    value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                    double ventil_saving_noelec = 0; double infil_saving_noelec = 0;
                    if (value.Length > 0 && value3.Length > 0)
                    {
                        ventil_saving_noelec = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value[0][0]));
                        infil_saving_noelec = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value2[0][0]));
                        ventil_saving_noelec = Math.Max(0, ventil_saving_noelec - infil_saving_noelec);
                    }

                    Ventil_data[0].Add(new { idx = i, val = ventil_saving.ToString("#,##0") }); ; //절감량 
                    Ventil_data[1].Add(new { idx = i, val = (ventil_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "ventil_saving", data = Ventil_data[0] });
                    data.Add(new { cname = "ventil_savingpercent", data = Ventil_data[1] });

                    d = (ventil_saving / Total_Energy_pre * 100);
                    charts += "{donut:" + d + "},";

                    double ventil_tCO2 = ventil_saving_elec * 0.4747 / 1000000 * 1000 + ventil_saving_noelec / 38.9  / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    double ventil_TOE = ventil_saving_elec * 0.00023 + ventil_saving_noelec / 38.9  / 0.277778 * 0.00103;

                    Ventil_data[2].Add(new { idx = i, val = ventil_tCO2.ToString("0.0") });  //tco2
                    Ventil_data[3].Add(new { idx = i, val = ventil_TOE.ToString("0.0") });  //TOE 
                    data.Add(new { cname = "ventil_tco2", data = Ventil_data[2] });
                    data.Add(new { cname = "ventil_toe", data = Ventil_data[3] });

                    string[] ventil_name = new string[10]; string[] ventil_zone_text = new string[10]; double[] ventil_eta_temp = new double[10]; double[] ventil_eta_humidity = new double[10];
                    double[] ventil_power = new double[10]; double[] ventil_volume_new = new double[10]; double[] ventil_point = new double[10];
                    double ventil_zone_area = 0, ventil_zone_netvolume = 0, ventil_zone_nmech = 0, ventil_zone_ninf = 0, ventil_zone_count = 0;
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select a.번호,a.명칭,b.온도교환효율_난방,b.습도교환효율_난방,b.팬동력 From AHUSystem_Form as a inner join User_HRV as b on a.번호 = b.번호 where a.유형='열회수기'");
                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            ventil_name[a] = Value[a][1];
                            ventil_eta_temp[a] = Convert.ToDouble(Value[a][2]);
                            ventil_eta_humidity[a] = Convert.ToDouble(Value[a][3]);
                            ventil_power[a] = Convert.ToDouble(Value[a][4]);

                            string[][] zonevalue = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct a.존번호,a.기존존,a.순바닥면적,a.순체적,a.이용일환기량,a.환기횟수,b.ninf From ZoneGeneral_Form as a inner join Zone_HCneed_Result as b on a.존번호 = b.번호 where a.선택열회수기='" + Value[a][0] + "' and b.비이용일_이용일='이용일' and b.난방_냉방='난방'");
                            if (zonevalue.Length > 0)
                            {
                                if (zonevalue.Length > 1) { ventil_zone_text[a] = zonevalue[0][0] + " 외 " + (zonevalue.Length - 1).ToString() + "개"; }
                                else { ventil_zone_text[a] = zonevalue[0][0]; }

                                for (int aa = 0; aa < zonevalue.Length; aa++)
                                {
                                    ventil_volume_new[a] += Convert.ToDouble(zonevalue[aa][4]);
                                    ventil_zone_area += Convert.ToDouble(zonevalue[aa][2]);
                                    ventil_zone_netvolume += Convert.ToDouble(zonevalue[aa][3]);
                                    ventil_zone_ninf += Convert.ToDouble(zonevalue[aa][3]) * Convert.ToDouble(zonevalue[aa][6]);
                                    ventil_zone_nmech += Convert.ToDouble(zonevalue[aa][3]) * Convert.ToDouble(zonevalue[aa][5]);
                                }
                                ventil_zone_count += zonevalue.Length;
                            }
                        }
                        for (int a = 0; a < 10; a++)
                        {
                            Ventil_data[4 + a].Add(new { idx = i, val = ventil_name[a] });//명칭
                            data.Add(new { cname = "ventil_name" + a, data = Ventil_data[4 + a] });
                            if (ventil_name[a] != null & ventil_name[a] != "")
                            {
                                Ventil_data[14 + a].Add(new { idx = i, val = ventil_zone_text[a] });//존
                                data.Add(new { cname = "ventil_zone" + a, data = Ventil_data[14 + a] });

                                Ventil_data[24 + a].Add(new { idx = i, val = ventil_eta_temp[a].ToString("0.0") });//온도교환효율
                                data.Add(new { cname = "ventil_eta_temp" + a, data = Ventil_data[24 + a] });

                                Ventil_data[34 + a].Add(new { idx = i, val = ventil_eta_humidity[a].ToString("0.0") });//습도교환효율
                                data.Add(new { cname = "ventil_eta_humidity" + a, data = Ventil_data[34 + a] });

                                Ventil_data[44 + a].Add(new { idx = i, val = ventil_power[a].ToString("0.0") });//환기 소비전력
                                data.Add(new { cname = "ventil_power" + a, data = Ventil_data[44 + a] });

                                Ventil_data[64 + a].Add(new { idx = i, val = ventil_volume_new[a].ToString("0.0") });//신규 풍량 
                                data.Add(new { cname = "ventil_volume_new" + a, data = Ventil_data[64 + a] });

                                ventil_point[a] = 100;
                                d = ventil_point[a];
                                if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                                sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                                Ventil_data[84 + a].Add(new { idx = i, val = sp });//성능점수
                                data.Add(new { cname = "ventil_point" + a, data = Ventil_data[84 + a] });
                            }
                        }

                        ventil_zone_ninf = ventil_zone_ninf / ventil_zone_netvolume;
                        ventil_zone_nmech = ventil_zone_nmech / ventil_zone_netvolume;
                        Ventil_data[94].Add(new { idx = i, val = ventil_zone_ninf.ToString("0.00") });  //침기횟수
                        Ventil_data[95].Add(new { idx = i, val = ventil_zone_nmech.ToString("0.00") });  //환기횟수
                        Ventil_data[96].Add(new { idx = i, val = ventil_zone_count.ToString("0") });  //존개수
                        Ventil_data[97].Add(new { idx = i, val = ventil_zone_area.ToString("0.0") });  //존면적
                        Ventil_data[98].Add(new { idx = i, val = ventil_zone_netvolume.ToString("0.0") });  //순체적
                        data.Add(new { cname = "ventil_zone_ninf", data = Ventil_data[94] });
                        data.Add(new { cname = "ventil_zone_nmech", data = Ventil_data[95] });
                        data.Add(new { cname = "ventil_zone_count", data = Ventil_data[96] });
                        data.Add(new { cname = "ventil_zone_area", data = Ventil_data[97] });
                        data.Add(new { cname = "ventil_zone_netvolume", data = Ventil_data[98] });

                        double ventil_eta_temp_avg = 0; double ventil_eta_humidity_avg = 0; double ventil_power_sum = 0; double ventil_volume_new_sum = 0; double ventil_point_avg = 0;
                        for (int a = 0; a < 10; a++)
                        {
                            ventil_power_sum += ventil_power[a];
                            ventil_volume_new_sum += ventil_volume_new[a];

                            ventil_eta_temp_avg += ventil_eta_temp[a] * ventil_volume_new[a];
                            ventil_eta_humidity_avg += ventil_eta_humidity[a] * ventil_volume_new[a];
                            ventil_point_avg += ventil_point[a] * ventil_volume_new[a];
                        }
                        ventil_eta_temp_avg = ventil_eta_temp_avg / ventil_volume_new_sum;
                        ventil_eta_humidity_avg = ventil_eta_humidity_avg / ventil_volume_new_sum;
                        ventil_point_avg = ventil_point_avg / ventil_volume_new_sum;


                        Ventil_data[99].Add(new { idx = i, val = ventil_eta_temp_avg.ToString("0.0") });
                        Ventil_data[100].Add(new { idx = i, val = ventil_eta_humidity_avg.ToString("0.0") });
                        Ventil_data[101].Add(new { idx = i, val = ventil_power_sum.ToString("0.0") });
                        Ventil_data[103].Add(new { idx = i, val = ventil_volume_new_sum.ToString("0.0") });
                        data.Add(new { cname = "ventil_eta_temp_avg", data = Ventil_data[99] });
                        data.Add(new { cname = "ventil_eta_humidity_avg", data = Ventil_data[100] });
                        data.Add(new { cname = "ventil_power_sum", data = Ventil_data[101] });
                        data.Add(new { cname = "ventil_volume_new_sum", data = Ventil_data[103] });
                        data.Add(new { cname = "ventil_point_sum", data = Ventil_data[105] });
                        d = ventil_point_avg;
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else if (d <= 50) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        Ventil_data[105].Add(new { idx = i, val = sp });//성능수준 평균  
                    }
                    #endregion

                    #region 기밀   



                    Infil_data[0].Add(new { idx = i, val = infil_saving.ToString("#,##0") }); ; //절감량 
                    Infil_data[1].Add(new { idx = i, val = (infil_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "infil_saving", data = Infil_data[0] });
                    data.Add(new { cname = "infil_savingpercent", data = Infil_data[1] });

                    d = (infil_saving / Total_Energy_pre * 100);
                    charts += "{donut:" + d + "},";

                    double infil_tCO2 = infil_saving_elec * 0.4747 / 1000000 * 1000 + infil_saving_noelec / 38.9  / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    double infil_TOE = infil_saving_elec * 0.00023 + infil_saving_noelec / 38.9  / 0.277778 * 0.00103;

                    Infil_data[2].Add(new { idx = i, val = infil_tCO2.ToString("0.0") });  //tco2
                    Infil_data[3].Add(new { idx = i, val = infil_TOE.ToString("0.0") });  //TOE 
                    data.Add(new { cname = "infil_tco2", data = Infil_data[2] });
                    data.Add(new { cname = "infil_toe", data = Infil_data[3] });

                    string door = null, win = null, wire = null, pipe = null;
                    double n50_new = 0, n50_rule = 0;
                    Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "출입문기밀여부,창호기밀여부,배선기밀여부,배관기밀여부,n50");
                    if (Value.Length > 0)
                    {
                        if (Convert.ToBoolean(Value[0][0])) { door = "시공"; } else { door = "미시공"; }
                        if (Convert.ToBoolean(Value[0][1])) { win = "시공"; } else { win = "미시공"; }
                        if (Convert.ToBoolean(Value[0][2])) { wire = "시공"; } else { wire = "미시공"; }
                        if (Convert.ToBoolean(Value[0][3])) { pipe = "시공"; } else { pipe = "미시공"; }
                        n50_new = Convert.ToDouble(Value[0][4]);
                    }
                    n50_rule = 0.6; //패시브하우스 기준 
                    Infil_data[4].Add(new { idx = i, val = door });  //출입문
                    Infil_data[5].Add(new { idx = i, val = win });  //창호
                    Infil_data[6].Add(new { idx = i, val = wire });  //배선
                    Infil_data[7].Add(new { idx = i, val = pipe });  //배관
                    Infil_data[8].Add(new { idx = i, val = n50_rule.ToString("0.0") });
                    Infil_data[9].Add(new { idx = i, val = n50_new.ToString("0.0") });  //신규 n50
                    data.Add(new { cname = "infil_door", data = Infil_data[4] });
                    data.Add(new { cname = "infil_win", data = Infil_data[5] });
                    data.Add(new { cname = "infil_wall", data = Infil_data[6] });
                    data.Add(new { cname = "infil_roof", data = Infil_data[7] });
                    data.Add(new { cname = "infil_n50_rule", data = Infil_data[8] });
                    data.Add(new { cname = "infil_n50_new", data = Infil_data[9] });

                    double infil_point = Math.Min(100, n50_rule / n50_new * 100);
                    d = infil_point;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    Infil_data[10].Add(new { idx = i, val = sp });//성능수준 
                    data.Add(new { cname = "infil_point", data = Infil_data[10] });
                    Infil_data[11].Add(new { idx = i, val = (infil_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "infil_saving_sum", data = Infil_data[11] });

                    #endregion

                    items.Add("Element_Lighting2.htm");
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
            List<object>[] Light_data = new List<object>[700];
            List<object>[] Infil_data = new List<object>[700];
            List<object>[] Ventil_data = new List<object>[700];
            double d;
            string sp;
            int i = -1, n;
            while (++i < 700)
            {
                Light_data[i] = new List<object>();
                Infil_data[i] = new List<object>();
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
                    double light_saving = Math.Max(Element_EnergySaving[j_조명], 0);


                    Light_data[0].Add(new { idx = i, val = light_saving.ToString("#,##0") }); ; //절감량 
                    Light_data[1].Add(new { idx = i, val = (light_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "light_saving", data = Light_data[0] });
                    data.Add(new { cname = "light_savingpercent", data = Light_data[1] });

                    d = (light_saving / Total_Energy_pre * 100);
                    charts += "{donut:" + d + "},";

                    double light_saving_elec = Element_ElecSaving[j_조명];
                    double light_saving_noelec = Element_GasSaving[j_조명];

                    double light_tCO2 = Math.Max(0, light_saving_elec * 0.4747 / 1000000 * 1000);
                    double light_TOE = Math.Max(0, light_saving_elec * 0.00023);

                    Light_data[2].Add(new { idx = i, val = light_tCO2.ToString("0.0") });  //tco2
                    Light_data[3].Add(new { idx = i, val = light_TOE.ToString("0.0") });  //TOE 
                    data.Add(new { cname = "light_tco2", data = Light_data[2] });
                    data.Add(new { cname = "light_toe", data = Light_data[3] });

                    string[][] Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneLighting_form", "조명번호");
                    string[] Light_Name = new string[10]; string[] Light_Zone_text = new string[10]; double[] Light_eta = new double[10]; double[] Light_Density_Rule = new double[10];
                    double[] Light_Area_Old = new double[10]; double[] Light_Area_New = new double[10]; double[] Light_Density_Old = new double[10]; double[] Light_Density_New = new double[10]; double[] Light_Saving = new double[10]; double[] Light_Point = new double[10];
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
                                Light_Density_Rule[a] = 8;//에절계 조명부문 1번 1점
                                //면적, 조명밀도 
                                Boolean samecheck = false;
                                for (int aa = 0; aa < 조명존.Length; aa++)
                                {
                                    Light_Area_New[a] += Convert.ToDouble(조명존[aa][1]);
                                    Light_Density_New[a] += Convert.ToDouble(조명존[aa][1]) * Convert.ToDouble(조명존[aa][2]);
                                    ArrayList prezone = new ArrayList();
                                    prezone = Split_(조명존[aa][6]);
                                    for (int aaa = 0; aaa < prezone.Count; aaa++)
                                    {
                                        string[][] 기존존 = Program.DB.querySQL(res[0][0], "Select 번호,순바닥면적,조명밀도,조명번호 From ZoneLighting_form where 번호='" + prezone[aaa].ToString() + "'");
                                        if (기존존.Length > 0)
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

                        for (int a = 0; a < Value.Length; a++)
                        {
                            Light_Point[a] = Math.Min(100, Light_Density_Rule[a] / Light_Density_New[a] * 100);
                            double pre_sum = 0; double post_sum = 0;
                            string[][] Result1 = Program.DB.querySQL(DB.type.ProjDB, "Select 존번호, 조명소요량 From Light_Result_Element Where 조명번호='" + Value[a][0] + "' And 검토유형='조명'");
                            for (int aa = 0; aa < Result1.Length; aa++)
                            {
                                post_sum += Convert.ToDouble(Result1[aa][1]);
                                string[][] Result2 = Program.DB.querySQL(DB.type.ProjDB, "Select 조명번호,조명소요량 From Light_Result_Element Where 존번호='" + Result1[aa][0] + "' And 검토유형='조닝'");
                                if (Result2.Length > 0)
                                {
                                    pre_sum += Convert.ToDouble(Result2[0][1]);
                                }
                            }
                            Light_Saving[a] = pre_sum - post_sum;
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

                            d = Light_Point[a];
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            Light_data[64 + a].Add(new { idx = i, val = sp });//성능점수
                            data.Add(new { cname = "light_point" + a, data = Light_data[64 + a] });
                        }
                    }

                    for (int a = 0; a < 10; a++)
                    {
                        if (Light_Saving[a] < 0) { Light_Saving[a] = 0; }
                    }

                    double sum = 0;
                    for (int a = 0; a < 10; a++)
                    {
                        sum += Light_Saving[a];
                    }

                    for (int a = 0; a < 10; a++)
                    {
                        if (Light_Name[a] != null && Light_Name[a] != "")
                        {
                            if (sum != 0)
                            {
                                Light_data[74 + a].Add(new { idx = i, val = ((light_saving / Total_Energy_pre) * (Light_Saving[a] / sum) * 100).ToString("0.0") + " %" });//요소기술별 에너지절감률
                            }
                            else
                            {
                                Light_data[74 + a].Add(new { idx = i, val = (0).ToString("0.0") + " %" });//요소기술별 에너지절감률
                            }
                            data.Add(new { cname = "light_saving" + a, data = Light_data[74 + a] });

                        }
                    }

                    double Light_Area_sum = 0, Light_eta_avg = 0, Light_Density_Old_avg = 0, Light_Density_New_avg = 0, Light_Point_avg = 0;
                    for (int a = 0; a < 10; a++)
                    {
                        Light_Area_sum += Light_Area_New[a];
                        Light_eta_avg += Light_eta[a] * Light_Area_New[a];
                        Light_Density_Old_avg += Light_Density_Old[a] * Light_Area_New[a];
                        Light_Density_New_avg += Light_Density_New[a] * Light_Area_New[a];
                        Light_Point_avg += Light_Point[a] * Light_Area_New[a];
                    }
                    Light_eta_avg = Light_eta_avg / Light_Area_sum;
                    Light_Density_Old_avg = Light_Density_Old_avg / Light_Area_sum;
                    Light_Density_New_avg = Light_Density_New_avg / Light_Area_sum;
                    Light_Point_avg = Math.Min(100, Light_Point_avg / Light_Area_sum);
                    Light_data[84].Add(new { idx = i, val = Light_Area_sum.ToString("0.0") });  // 면적 합계
                    Light_data[85].Add(new { idx = i, val = Light_eta_avg.ToString("0.0") });  // 효율 평균
                    Light_data[86].Add(new { idx = i, val = Light_Density_Old_avg.ToString("0.0") });  // 기존 밀도 평균
                    Light_data[87].Add(new { idx = i, val = Light_Density_New_avg.ToString("0.0") });  // 신규 밀도 평균
                    Light_data[88].Add(new { idx = i, val = (light_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });  //에너지절약 합계
                    d = Light_Point_avg;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    Light_data[89].Add(new { idx = i, val = sp });//성능수준 평균  
                    data.Add(new { cname = "light_area_sum", data = Light_data[84] });
                    data.Add(new { cname = "light_eta_avg", data = Light_data[85] });
                    data.Add(new { cname = "light_density_old_avg", data = Light_data[86] });
                    data.Add(new { cname = "light_density_new_avg", data = Light_data[87] });
                    data.Add(new { cname = "light_saving_sum", data = Light_data[88] });
                    data.Add(new { cname = "light_point_avg", data = Light_data[89] });


                    double facade_area = 0; double roof_area = 0; double facade_D = 0; double roof_D = 0; double light_zonecount = 0;
                    Value = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "번호,자연채광유형,순바닥면적");
                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            if (Value[a][1] == "파사드")
                            {
                                facade_area += Convert.ToDouble(Value[a][2]);

                                string[][] result = Program.DB.querySQL(DB.type.ProjDB, "Select AVG(f_D) From Zone_LightResult Where 번호='" + Value[a][0] + "'");
                                if (result.Length > 0)
                                {
                                    facade_D += Convert.ToDouble(Value[a][2]) * Convert.ToDouble(result[0][0]);
                                }
                            }
                            else if (Value[a][1] == "천창")
                            {
                                roof_area += Convert.ToDouble(Value[a][2]);

                                string[][] result = Program.DB.querySQL(DB.type.ProjDB, "Select AVG(r_DSNA) From Zone_LightResult Where 번호='" + Value[a][0] + "'");
                                if (result.Length > 0)
                                {
                                    roof_D += Convert.ToDouble(Value[a][2]) * Convert.ToDouble(result[0][0]);
                                }
                            }
                        }
                        if (facade_area > 0)
                        {
                            facade_D = facade_D / facade_area;
                        }

                        if (roof_area > 0)
                        {
                            roof_D = roof_D / roof_area;
                        }
                        light_zonecount = Value.Length;
                    }
                    Light_data[79].Add(new { idx = i, val = facade_D.ToString("0.0") });  // 파사드 주광률
                    Light_data[80].Add(new { idx = i, val = roof_D.ToString("0.0") });  //천창 주광률
                    Light_data[81].Add(new { idx = i, val = light_zonecount.ToString("0") });  //존 개수
                    data.Add(new { cname = "facade_d", data = Light_data[79] });
                    data.Add(new { cname = "roof_d", data = Light_data[80] });
                    data.Add(new { cname = "light_zonecount", data = Light_data[81] });
                    #endregion
                    #region 환기  
                    int j_기밀 = 0;
                    for (int a = 0; a < ElementAlt.Length; a++)
                    {
                        if (ElementAlt[a] == "기밀")
                        {
                            j_기밀 = a; break;
                        }
                    }
                    double infil_saving = Math.Max(Element_EnergySaving[j_기밀], 0);
                    double infil_saving_elec = Math.Max(Element_ElecSaving[j_기밀], 0);
                    double infil_saving_noelec = Math.Max(Element_GasSaving[j_기밀], 0);
                    int j_환기 = 0;
                    for (int a = 0; a < ElementAlt.Length; a++)
                    {
                        if (ElementAlt[a] == "기밀+열회수기")
                        {
                            j_환기 = a; break;
                        }
                    }
                    double ventil_saving = Math.Max(Element_EnergySaving[j_환기] - infil_saving, 0);
                    double ventil_saving_elec = Math.Max(Element_ElecSaving[j_환기] - infil_saving_elec, 0);
                    double ventil_saving_noelec = Math.Max(Element_GasSaving[j_환기] - infil_saving_noelec, 0);

                    Ventil_data[0].Add(new { idx = i, val = ventil_saving.ToString("#,##0") }); ; //절감량 
                    Ventil_data[1].Add(new { idx = i, val = (ventil_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "ventil_saving", data = Ventil_data[0] });
                    data.Add(new { cname = "ventil_savingpercent", data = Ventil_data[1] });

                    d = (ventil_saving / Total_Energy_pre * 100);
                    charts += "{donut:" + d + "},";

                    double ventil_tCO2 = ventil_saving_elec * 0.4747 / 1000000 * 1000 + ventil_saving_noelec / 38.9  / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    double ventil_TOE = ventil_saving_elec * 0.00023 + ventil_saving_noelec / 38.9  / 0.277778 * 0.00103;

                    Ventil_data[2].Add(new { idx = i, val = ventil_tCO2.ToString("0.0") });  //tco2
                    Ventil_data[3].Add(new { idx = i, val = ventil_TOE.ToString("0.0") });  //TOE 
                    data.Add(new { cname = "ventil_tco2", data = Ventil_data[2] });
                    data.Add(new { cname = "ventil_toe", data = Ventil_data[3] });

                    string[] ventil_name = new string[10]; string[] ventil_zone_text = new string[10]; double[] ventil_eta_temp = new double[10]; double[] ventil_eta_humidity = new double[10];
                    double[] ventil_power = new double[10]; double[] ventil_volume_old = new double[10]; double[] ventil_volume_new = new double[10];
                    double[] ventil_saving_element = new double[10]; double[] ventil_point = new double[10];
                    double ventil_zone_area = 0, ventil_zone_netvolume = 0, ventil_zone_nmech = 0, ventil_zone_ninf = 0, ventil_zone_count = 0;
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select a.번호,a.명칭,b.온도교환효율_난방,b.습도교환효율_난방,b.팬동력 From AHUSystem_Form as a inner join User_HRV as b on a.번호 = b.번호 where a.유형='열회수기'");
                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            ventil_name[a] = Value[a][1];
                            ventil_eta_temp[a] = Convert.ToDouble(Value[a][2]);
                            ventil_eta_humidity[a] = Convert.ToDouble(Value[a][3]);
                            ventil_power[a] = Convert.ToDouble(Value[a][4]);

                            string[][] zonevalue = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct a.존번호,a.기존존,a.순바닥면적,a.순체적,a.이용일환기량,a.환기횟수,b.ninf From ZoneGeneral_Form as a inner join Zone_HCneed_Result as b on a.존번호 = b.번호 where a.선택열회수기='" + Value[a][0] + "' and b.비이용일_이용일='이용일' and b.난방_냉방='난방'");
                            if (zonevalue.Length > 0)
                            {
                                if (zonevalue.Length > 1) { ventil_zone_text[a] = zonevalue[0][0] + " 외 " + (zonevalue.Length - 1).ToString() + "개"; }
                                else { ventil_zone_text[a] = zonevalue[0][0]; }

                                for (int aa = 0; aa < zonevalue.Length; aa++)
                                {
                                    ventil_volume_new[a] += Convert.ToDouble(zonevalue[aa][4]);

                                    ArrayList prezone = new ArrayList();
                                    prezone = Split_(zonevalue[aa][1]);
                                    for (int aaa = 0; aaa < prezone.Count; aaa++)
                                    {
                                        string[][] 기존존 = Program.DB.querySQL(res[0][0], "Select 선택열회수기,이용일환기량 From ZoneGeneral_Form where 존번호='" + prezone[aaa].ToString() + "'");
                                        if (기존존.Length > 0)
                                        {
                                            if (기존존[0][0] != "")
                                            {
                                                ventil_volume_old[a] += Convert.ToDouble(기존존[0][1]);
                                            }
                                        }
                                    }

                                    ventil_zone_area += Convert.ToDouble(zonevalue[aa][2]);
                                    ventil_zone_netvolume += Convert.ToDouble(zonevalue[aa][3]);
                                    ventil_zone_ninf += Convert.ToDouble(zonevalue[aa][3]) * Convert.ToDouble(zonevalue[aa][6]);
                                    ventil_zone_nmech += Convert.ToDouble(zonevalue[aa][3]) * Convert.ToDouble(zonevalue[aa][5]);
                                }
                                ventil_zone_count += zonevalue.Length;
                            }
                        }
                        double v_sum = 0;
                        for (int a = 0; a < 10; a++)
                        {
                            v_sum += ventil_volume_new[a] - ventil_volume_old[a];
                        }
                        for (int a = 0; a < 10; a++)
                        {
                            ventil_saving_element[a] = (ventil_saving / Total_Energy_pre * 100) * (ventil_volume_new[a] - ventil_volume_old[a]) / v_sum;
                        }
                        for (int a = 0; a < 10; a++)
                        {
                            Ventil_data[4 + a].Add(new { idx = i, val = ventil_name[a] });//명칭
                            data.Add(new { cname = "ventil_name" + a, data = Ventil_data[4 + a] });
                            if (ventil_name[a] != null & ventil_name[a] != "")
                            {
                                Ventil_data[14 + a].Add(new { idx = i, val = ventil_zone_text[a] });//존
                                data.Add(new { cname = "ventil_zone" + a, data = Ventil_data[14 + a] });

                                Ventil_data[24 + a].Add(new { idx = i, val = ventil_eta_temp[a].ToString("0.0") });//온도교환효율
                                data.Add(new { cname = "ventil_eta_temp" + a, data = Ventil_data[24 + a] });

                                Ventil_data[34 + a].Add(new { idx = i, val = ventil_eta_humidity[a].ToString("0.0") });//습도교환효율
                                data.Add(new { cname = "ventil_eta_humidity" + a, data = Ventil_data[34 + a] });

                                Ventil_data[44 + a].Add(new { idx = i, val = ventil_power[a].ToString("0.0") });//환기 소비전력
                                data.Add(new { cname = "ventil_power" + a, data = Ventil_data[44 + a] });

                                Ventil_data[54 + a].Add(new { idx = i, val = ventil_volume_old[a].ToString("0.0") });//기존 풍량 
                                data.Add(new { cname = "ventil_volume_old" + a, data = Ventil_data[54 + a] });

                                Ventil_data[64 + a].Add(new { idx = i, val = ventil_volume_new[a].ToString("0.0") });//신규 풍량 
                                data.Add(new { cname = "ventil_volume_new" + a, data = Ventil_data[64 + a] });

                                Ventil_data[74 + a].Add(new { idx = i, val = ventil_saving_element[a].ToString("0.0") + " %" });//에너지절감 
                                data.Add(new { cname = "ventil_saving_element" + a, data = Ventil_data[74 + a] });

                                ventil_point[a] = 100;
                                d = ventil_point[a];
                                if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                                sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                                Ventil_data[84 + a].Add(new { idx = i, val = sp });//성능점수
                                data.Add(new { cname = "ventil_point" + a, data = Ventil_data[84 + a] });
                            }
                        }

                        ventil_zone_ninf = ventil_zone_ninf / ventil_zone_netvolume;
                        ventil_zone_nmech = ventil_zone_nmech / ventil_zone_netvolume;
                        Ventil_data[94].Add(new { idx = i, val = ventil_zone_ninf.ToString("0.00") });  //침기횟수
                        Ventil_data[95].Add(new { idx = i, val = ventil_zone_nmech.ToString("0.00") });  //환기횟수
                        Ventil_data[96].Add(new { idx = i, val = ventil_zone_count.ToString("0") });  //존개수
                        Ventil_data[97].Add(new { idx = i, val = ventil_zone_area.ToString("0.0") });  //존면적
                        Ventil_data[98].Add(new { idx = i, val = ventil_zone_netvolume.ToString("0.0") });  //순체적
                        data.Add(new { cname = "ventil_zone_ninf", data = Ventil_data[94] });
                        data.Add(new { cname = "ventil_zone_nmech", data = Ventil_data[95] });
                        data.Add(new { cname = "ventil_zone_count", data = Ventil_data[96] });
                        data.Add(new { cname = "ventil_zone_area", data = Ventil_data[97] });
                        data.Add(new { cname = "ventil_zone_netvolume", data = Ventil_data[98] });

                        double ventil_eta_temp_avg = 0; double ventil_eta_humidity_avg = 0; double ventil_power_sum = 0; double ventil_volume_old_sum = 0; double ventil_volume_new_sum = 0; double ventil_point_avg = 0;
                        for (int a = 0; a < 10; a++)
                        {
                            ventil_power_sum += ventil_power[a];
                            ventil_volume_old_sum += ventil_volume_old[a];
                            ventil_volume_new_sum += ventil_volume_new[a];

                            ventil_eta_temp_avg += ventil_eta_temp[a] * ventil_volume_new[a];
                            ventil_eta_humidity_avg += ventil_eta_humidity[a] * ventil_volume_new[a];
                            ventil_point_avg += ventil_point[a] * ventil_volume_new[a];
                        }
                        ventil_eta_temp_avg = ventil_eta_temp_avg / ventil_volume_new_sum;
                        ventil_eta_humidity_avg = ventil_eta_humidity_avg / ventil_volume_new_sum;
                        ventil_point_avg = ventil_point_avg / ventil_volume_new_sum;


                        Ventil_data[99].Add(new { idx = i, val = ventil_eta_temp_avg.ToString("0.0") });
                        Ventil_data[100].Add(new { idx = i, val = ventil_eta_humidity_avg.ToString("0.0") });
                        Ventil_data[101].Add(new { idx = i, val = ventil_power_sum.ToString("0.0") });
                        Ventil_data[102].Add(new { idx = i, val = ventil_volume_old_sum.ToString("0.0") });
                        Ventil_data[103].Add(new { idx = i, val = ventil_volume_new_sum.ToString("0.0") });
                        Ventil_data[104].Add(new { idx = i, val = (ventil_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률 합계
                        data.Add(new { cname = "ventil_eta_temp_avg", data = Ventil_data[99] });
                        data.Add(new { cname = "ventil_eta_humidity_avg", data = Ventil_data[100] });
                        data.Add(new { cname = "ventil_power_sum", data = Ventil_data[101] });
                        data.Add(new { cname = "ventil_volume_old_sum", data = Ventil_data[102] });
                        data.Add(new { cname = "ventil_volume_new_sum", data = Ventil_data[103] });
                        data.Add(new { cname = "ventil_saving_sum", data = Ventil_data[104] });
                        data.Add(new { cname = "ventil_point_sum", data = Ventil_data[105] });
                        d = ventil_point_avg;
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else if (d <= 50) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        Ventil_data[105].Add(new { idx = i, val = sp });//성능수준 평균  
                    }
                    #endregion

                    #region 기밀   



                    Infil_data[0].Add(new { idx = i, val = infil_saving.ToString("#,##0") }); ; //절감량 
                    Infil_data[1].Add(new { idx = i, val = (infil_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "infil_saving", data = Infil_data[0] });
                    data.Add(new { cname = "infil_savingpercent", data = Infil_data[1] });

                    d = (infil_saving / Total_Energy_pre * 100);
                    charts += "{donut:" + d + "},";

                    double infil_tCO2 = infil_saving_elec * 0.4747 / 1000000 * 1000 + infil_saving_noelec / 38.9  / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    double infil_TOE = infil_saving_elec * 0.00023 + infil_saving_noelec / 38.9  / 0.277778 * 0.00103;

                    Infil_data[2].Add(new { idx = i, val = infil_tCO2.ToString("0.0") });  //tco2
                    Infil_data[3].Add(new { idx = i, val = infil_TOE.ToString("0.0") });  //TOE 
                    data.Add(new { cname = "infil_tco2", data = Infil_data[2] });
                    data.Add(new { cname = "infil_toe", data = Infil_data[3] });

                    string door = null, win = null, wire = null, pipe = null;
                    double n50_new = 0, n50_old = 0;
                    Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "출입문기밀여부,창호기밀여부,배선기밀여부,배관기밀여부,n50");
                    if (Value.Length > 0)
                    {
                        if (Convert.ToBoolean(Value[0][0])) { door = "시공"; } else { door = "미시공"; }
                        if (Convert.ToBoolean(Value[0][1])) { win = "시공"; } else { win = "미시공"; }
                        if (Convert.ToBoolean(Value[0][2])) { wire = "시공"; } else { wire = "미시공"; }
                        if (Convert.ToBoolean(Value[0][3])) { pipe = "시공"; } else { pipe = "미시공"; }
                        n50_new = Convert.ToDouble(Value[0][4]);
                    }
                    Value = Program.DB.getValue(res[0][0], "BuildingGeneral", "n50");
                    if (Value.Length > 0)
                    {
                        n50_old = Convert.ToDouble(Value[0][0]);
                    }
                    Infil_data[4].Add(new { idx = i, val = door });  //출입문
                    Infil_data[5].Add(new { idx = i, val = win });  //창호
                    Infil_data[6].Add(new { idx = i, val = wire });  //배선
                    Infil_data[7].Add(new { idx = i, val = pipe });  //배관
                    Infil_data[8].Add(new { idx = i, val = n50_old.ToString("0.0") });  //기존 n50
                    Infil_data[9].Add(new { idx = i, val = n50_new.ToString("0.0") });  //신규 n50
                    data.Add(new { cname = "infil_door", data = Infil_data[4] });
                    data.Add(new { cname = "infil_win", data = Infil_data[5] });
                    data.Add(new { cname = "infil_wall", data = Infil_data[6] });
                    data.Add(new { cname = "infil_roof", data = Infil_data[7] });
                    data.Add(new { cname = "infil_n50_old", data = Infil_data[8] });
                    data.Add(new { cname = "infil_n50_new", data = Infil_data[9] });

                    double n50_rule = 1.5;
                    double infil_point = Math.Min(100, n50_rule / n50_new * 100);
                    d = infil_point;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    Infil_data[10].Add(new { idx = i, val = sp });//성능수준 
                    data.Add(new { cname = "infil_point", data = Infil_data[10] });
                    Infil_data[11].Add(new { idx = i, val = (infil_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "infil_saving_sum", data = Infil_data[11] });

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
        private ArrayList Split_(String nonSplit)
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