using main.contents.Result.Element_Report;
using Microsoft.Web.WebView2.Core;
using System.Collections;
using System.Diagnostics;

namespace main.contents.Result
{
    public partial class Element_Chiler : Form
    {
        bool scriptable = false;
        public Element_Chiler()
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
            List<object> items = new List<object>();
            List<object> items2 = new List<object>();
            List<object> data = new List<object>();
            List<object>[] Air_data = new List<object>[700];
            List<object>[] Water_data = new List<object>[700];
            double d;
            string sp;
            int i = -1, n;
            while (++i < 700)
            {
                Air_data[i] = new List<object>();
                Water_data[i] = new List<object>();
            }
            string charts = "";
            i = -1;
            while (++i < 번호.Length)
            {
                #region 공냉식냉동기   

                double Total_Energy_pre = 0;
                string[][] value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='공냉식냉동기'");
                string[][] value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                double air_total_saving = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    Total_Energy_pre = Program.UTIL.ToDoubleOrZero(value3[0][0]);
                    air_total_saving = Math.Max(0, Program.UTIL.ToDoubleOrZero(value3[0][0]) - Program.UTIL.ToDoubleOrZero(value[0][0]));
                }

                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전기' and 검토유형='공냉식냉동기'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전기'");
                double air_total_elec = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    air_total_elec = Math.Max(0, Program.UTIL.ToDoubleOrZero(value3[0][0]) - Program.UTIL.ToDoubleOrZero(value[0][0]));
                }
                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='공냉식냉동기'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                double air_total_gas = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    air_total_gas = Math.Max(0, Program.UTIL.ToDoubleOrZero(value3[0][0]) - Program.UTIL.ToDoubleOrZero(value[0][0]));
                }

                string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.냉방유닛,a.명칭,b.공급존,a.냉방출력,a.EER,b.번호,b.설치대수,b.압축기 From User_AirCooler as a Inner Join CoolingSystem_Form as b ON a.번호 = b.냉방유닛");
                string[] Air_Name = new string[18]; string[] Air_Zone_text = new string[18];
                double[] Air_Power = new double[18]; double[] Air_COP_New = new double[18]; double[] Air_Point = new double[18]; double[] Air_COP_Rule = new double[18];

                ArrayList Air_Zones_split = new ArrayList();
                if (Value.Length > 0)
                {
                    for (int a = 0; a < Value.Length; a++)
                    {
                        Air_Name[a] = Value[a][1];

                        ArrayList splitzone = new ArrayList();
                        splitzone = Split_(Value[a][2]);
                        if (splitzone.Count > 1) { Air_Zone_text[a] = splitzone[0].ToString() + " 외 " + (splitzone.Count - 1).ToString() + "개"; }
                        else { Air_Zone_text[a] = splitzone[0].ToString(); }

                        for (int aa = 0; aa < splitzone.Count; aa++)
                        {
                            if (Air_Zones_split.Contains(splitzone[aa]))
                            { }
                            else { Air_Zones_split.Add(splitzone[aa]); }
                        }

                        Air_Power[a] = Program.UTIL.ToDoubleOrZero(Value[a][3]) * Program.UTIL.ToDoubleOrZero(Value[a][6]);
                        Air_COP_New[a] = Program.UTIL.ToDoubleOrZero(Value[a][4]);
                        string[][] kkk = Program.DB.getValue(DB.type.BaseDB_Cooling, "AirCooler", "EER", "압축기= '" + Value[a][7] + "' And 냉매='R134a' And 냉수출구온도 = '14' And 평균증발기온도='8'");
                        if (kkk.Length > 0)
                        {
                            Air_COP_Rule[a] = Program.UTIL.ToDoubleOrZero(kkk[0][0]);
                        }
                        Air_Point[a] = Math.Min(100, Air_COP_New[a] / Air_COP_Rule[a] * 100);
                    }
                }

                double Air_COP_New_total = 0; double Air_Point_total = 0; double Air_COP_Rule_total = 0;

                for (int a = 0; a < 18; a++)
                {
                    Air_data[a].Add(new { idx = i, val = Air_Name[a] });//명칭
                    data.Add(new { cname = "air_name" + a, data = Air_data[a] });
                    if (Air_Name[a] != null & Air_Name[a] != "")
                    {
                        Air_data[18 + a].Add(new { idx = i, val = Air_Zone_text[a] });//존
                        data.Add(new { cname = "air_zone" + a, data = Air_data[18 + a] });

                        Air_data[72 + a].Add(new { idx = i, val = Air_Power[a].ToString("0.0") });//냉방용량
                        data.Add(new { cname = "air_power" + a, data = Air_data[72 + a] });

                        Air_data[90 + a].Add(new { idx = i, val = Air_COP_New[a].ToString("0.0") });//냉방COP
                        data.Add(new { cname = "air_cop_new" + a, data = Air_data[90 + a] });

                        d = Air_Point[a];
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        Air_data[198 + a].Add(new { idx = i, val = sp });//냉방 성능점수
                        data.Add(new { cname = "air_point" + a, data = Air_data[198 + a] });
                    }

                    //가중평균 
                    Air_COP_New_total += Air_COP_New[a] * Air_Power[a];
                    Air_Point_total += Air_Point[a] * Air_Power[a];
                    Air_COP_Rule_total += Air_COP_Rule[a] * Air_Power[a];
                }
                if (Air_Power.Sum() > 0)
                {
                    Air_COP_New_total = Air_COP_New_total / Air_Power.Sum();
                    Air_Point_total = Math.Min(100, Air_Point_total / Air_Power.Sum());
                    Air_COP_Rule_total = Air_COP_Rule_total / Air_Power.Sum();
                }

                double v = double.IsNaN(air_total_saving) ? 0 : air_total_saving;
                Air_data[216].Add(new { idx = i, val = v.ToString("#,##0") });//절감량 전체 
                data.Add(new { cname = "air_saving_total", data = Air_data[216] });

                v = double.IsNaN(air_total_saving / Total_Energy_pre * 100) ? 0 : air_total_saving / Total_Energy_pre * 100;
                Air_data[217].Add(new { idx = i, val = v.ToString("0.0") + " %" });//절감률 전체 
                data.Add(new { cname = "air_saving_percent", data = Air_data[217] });

                v = double.IsNaN(air_total_elec * 0.4747 / 1000000 * 1000 + air_total_gas / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000) ? 0 : air_total_elec * 0.4747 / 1000000 * 1000 + air_total_gas / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                Air_data[218].Add(new { idx = i, val = v.ToString("0.0") });//tco2
                data.Add(new { cname = "air_tco2", data = Air_data[218] });

                v = double.IsNaN(air_total_elec * 0.00023 + air_total_gas / 38.9 / 0.277778 * 0.00103) ? 0 : air_total_elec * 0.00023 + air_total_gas / 38.9 / 0.277778 * 0.00103;
                Air_data[219].Add(new { idx = i, val = v.ToString("0.0") });//절감량 전체 
                data.Add(new { cname = "air_toe", data = Air_data[219] });

                d = (air_total_saving / Total_Energy_pre * 100);
                charts += "{donut:" + d + "},";

                //합산 계 
                Air_data[225].Add(new { idx = i, val = Air_Power.Sum().ToString("0.0") });//냉방 용량 합계  
                data.Add(new { cname = "air_power_total", data = Air_data[225] });
                Air_data[227].Add(new { idx = i, val = Air_COP_New_total.ToString("0.0") });//냉방 COP 평균  
                data.Add(new { cname = "air_cop_new_total", data = Air_data[227] });
                Air_data[228].Add(new { idx = i, val = (air_total_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });//냉방 절감량 합계  
                data.Add(new { cname = "air_saving_total2", data = Air_data[228] });
                d = Air_Point_total;
                if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                Air_data[229].Add(new { idx = i, val = sp });//냉방 성능수준 평균  
                data.Add(new { cname = "air_point_total", data = Air_data[229] });

                double Air_Qmax_c = 0; double Air_ZoneArea = 0;
                for (int a = 0; a < Air_Zones_split.Count; a++)
                {
                    string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호='" + Air_Zones_split[a].ToString() + "' And 난방_냉방='냉방' and 비이용일_이용일='이용일' and 월='8월'");
                    if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                    {
                        Air_Qmax_c += Program.UTIL.ToDoubleOrZero(ZoneValue[0][0]);
                    }
                    ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "존번호='" + Air_Zones_split[a].ToString() + "'");
                    if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                    {
                        Air_ZoneArea += Program.UTIL.ToDoubleOrZero(ZoneValue[0][0]);
                    }
                }
                Air_data[231].Add(new { idx = i, val = (Air_Qmax_c / 1000).ToString("0.0") });//냉방부하 
                data.Add(new { cname = "air_qmax", data = Air_data[231] });
                Air_data[232].Add(new { idx = i, val = Air_ZoneArea.ToString("0.0") });//존면적
                data.Add(new { cname = "air_zonearea", data = Air_data[232] });
                Air_data[233].Add(new { idx = i, val = Air_Zones_split.Count.ToString() });//존개수 
                data.Add(new { cname = "air_zonecount", data = Air_data[233] });
                if (Air_COP_Rule_total > 0)
                {
                    Air_data[234].Add(new { idx = i, val = "* DIN V 18599-7 표준 EER : " + Air_COP_Rule_total.ToString("0.0") + " 기준" });//냉방 법규 평균  
                    data.Add(new { cname = "air_cop_rule", data = Air_data[234] });
                }

                #endregion

                #region 수냉식냉동기    
                Total_Energy_pre = 0;
                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='수냉식냉동기'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                double water_total_saving = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    Total_Energy_pre = Program.UTIL.ToDoubleOrZero(value3[0][0]);
                    water_total_saving = Math.Max(0, Program.UTIL.ToDoubleOrZero(value3[0][0]) - Program.UTIL.ToDoubleOrZero(value[0][0]));
                }

                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전기' and 검토유형='수냉식냉동기'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전기'");
                double water_total_elec = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    water_total_elec = Math.Max(0, Program.UTIL.ToDoubleOrZero(value3[0][0]) - Program.UTIL.ToDoubleOrZero(value[0][0]));
                }
                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='수냉식냉동기'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                double water_total_gas = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    water_total_gas = Math.Max(0, Program.UTIL.ToDoubleOrZero(value3[0][0]) - Program.UTIL.ToDoubleOrZero(value[0][0]));
                }

                Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.냉방유닛,a.명칭,b.공급존,a.냉방출력,a.EER,b.번호,b.설치대수,b.압축기 From User_WaterCooler as a Inner Join CoolingSystem_Form as b ON a.번호 = b.냉방유닛");
                string[] Water_Name = new string[18]; string[] Water_Zone_text = new string[18];
                double[] Water_Power = new double[18]; double[] Water_COP_New = new double[18]; double[] Water_Point = new double[18]; double[] Water_COP_Rule = new double[18];

                ArrayList Water_Zones_split = new ArrayList();

                if (Value.Length > 0)
                {
                    for (int a = 0; a < Value.Length; a++)
                    {
                        Water_Name[a] = Value[a][1];

                        ArrayList splitzone = new ArrayList();
                        splitzone = Split_(Value[a][2]);
                        if (splitzone.Count > 1) { Water_Zone_text[a] = splitzone[0].ToString() + " 외 " + (splitzone.Count - 1).ToString() + "개"; }
                        else { Water_Zone_text[a] = splitzone[0].ToString(); }

                        for (int aa = 0; aa < splitzone.Count; aa++)
                        {
                            if (Water_Zones_split.Contains(splitzone[aa]))
                            { }
                            else { Water_Zones_split.Add(splitzone[aa]); }
                        }
                        Water_Power[a] = Program.UTIL.ToDoubleOrZero(Value[a][3]) * Program.UTIL.ToDoubleOrZero(Value[a][6]);
                        Water_COP_New[a] = Program.UTIL.ToDoubleOrZero(Value[a][4]);
                        string[][] kkk = Program.DB.getValue(DB.type.BaseDB_Cooling, "WaterCooler", "EER", "압축기= '" + Value[a][7] + "' And 냉매='R134a' And 냉수출구온도 = '14' And 냉각수입구온도='27'");
                        if (kkk.Length > 0)
                        {
                            Water_COP_Rule[a] = Program.UTIL.ToDoubleOrZero(kkk[0][0]);
                        }
                        Water_Point[a] = Math.Min(100, Water_COP_New[a] / Water_COP_Rule[a] * 100);
                    }
                }
                double Water_COP_New_total = 0; double Water_Point_total = 0; double Water_COP_Rule_total = 0;
                for (int a = 0; a < 18; a++)
                {
                    Water_data[a].Add(new { idx = i, val = Water_Name[a] });//명칭
                    data.Add(new { cname = "water_name" + a, data = Water_data[a] });
                    if (Water_Name[a] != null & Water_Name[a] != "")
                    {
                        Water_data[18 + a].Add(new { idx = i, val = Water_Zone_text[a] });//존
                        data.Add(new { cname = "water_zone" + a, data = Water_data[18 + a] });

                        Water_data[72 + a].Add(new { idx = i, val = Water_Power[a].ToString("0.0") });//냉방용량
                        data.Add(new { cname = "water_power" + a, data = Water_data[72 + a] });

                        Water_data[90 + a].Add(new { idx = i, val = Water_COP_New[a].ToString("0.0") });//냉방COP
                        data.Add(new { cname = "water_cop_new" + a, data = Water_data[90 + a] });

                        d = Water_Point[a];
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        Water_data[198 + a].Add(new { idx = i, val = sp });//냉방 성능점수
                        data.Add(new { cname = "water_point" + a, data = Water_data[198 + a] });
                    }

                    //가중평균 
                    Water_COP_New_total += Water_COP_New[a] * Water_Power[a];
                    Water_Point_total += Water_Point[a] * Water_Power[a];
                    Water_COP_Rule_total += Water_COP_Rule[a] * Water_Power[a];
                }
                if (Water_Power.Sum() > 0)
                {
                    Water_COP_New_total = Water_COP_New_total / Water_Power.Sum();
                    Water_Point_total = Math.Min(100, Water_Point_total / Water_Power.Sum());
                    Water_COP_Rule_total = Water_COP_Rule_total / Water_Power.Sum();
                }

                double v2 = double.IsNaN(water_total_saving) ? 0 : water_total_saving;
                Water_data[216].Add(new { idx = i, val = v2.ToString("#,##0") });//절감량 전체 
                data.Add(new { cname = "water_saving_total", data = Water_data[216] });

                v2 = double.IsNaN(water_total_saving / Total_Energy_pre * 100) ? 0 : water_total_saving / Total_Energy_pre * 100;
                Water_data[217].Add(new { idx = i, val = v2.ToString("0.0") + " %" });//절감률 전체 
                data.Add(new { cname = "water_saving_percent", data = Water_data[217] });

                v2 = double.IsNaN(water_total_elec * 0.4747 / 1000000 * 1000 + water_total_gas / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000) ? 0 : water_total_elec * 0.4747 / 1000000 * 1000 + water_total_gas / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                Water_data[218].Add(new { idx = i, val = v2.ToString("0.0") });//tco2
                data.Add(new { cname = "water_tco2", data = Water_data[218] });

                v2 = double.IsNaN(water_total_elec * 0.00023 + water_total_gas / 38.9 / 0.277778 * 0.00103) ? 0 : water_total_elec * 0.00023 + water_total_gas / 38.9 / 0.277778 * 0.00103;
                Water_data[219].Add(new { idx = i, val = (water_total_elec * 0.00023 + water_total_gas / 38.9 / 0.277778 * 0.00103).ToString("0.0") });//절감량 전체 
                data.Add(new { cname = "water_toe", data = Water_data[219] });

                d = (air_total_saving / Total_Energy_pre * 100);
                charts += "{donut:" + d + "},";

                //합산 계 
                Water_data[225].Add(new { idx = i, val = Water_Power.Sum().ToString("0.0") });//냉방 용량 합계  
                data.Add(new { cname = "water_power_total", data = Water_data[225] });
                Water_data[227].Add(new { idx = i, val = Water_COP_New_total.ToString("0.0") });//냉방 COP 평균  
                data.Add(new { cname = "water_cop_new_total", data = Water_data[227] });
                Water_data[228].Add(new { idx = i, val = (water_total_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });//냉방 절감량 합계  
                data.Add(new { cname = "water_saving_total2", data = Water_data[228] });
                d = Water_Point_total;
                if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                Water_data[229].Add(new { idx = i, val = sp });//냉방 성능수준 평균  
                data.Add(new { cname = "water_point_total", data = Water_data[229] });

                double Water_Qmax_c = 0; double Water_ZoneArea = 0;
                for (int a = 0; a < Water_Zones_split.Count; a++)
                {
                    string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호='" + Water_Zones_split[a].ToString() + "' And 난방_냉방='냉방' and 비이용일_이용일='이용일' and 월='8월'");
                    if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                    {
                        Water_Qmax_c += Program.UTIL.ToDoubleOrZero(ZoneValue[0][0]);
                    }
                    ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "존번호='" + Water_Zones_split[a].ToString() + "'");
                    if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                    {
                        Water_ZoneArea += Program.UTIL.ToDoubleOrZero(ZoneValue[0][0]);
                    }
                }
                Water_data[231].Add(new { idx = i, val = (Water_Qmax_c / 1000).ToString("0.0") });//냉방부하 
                data.Add(new { cname = "water_qmax", data = Water_data[231] });
                Water_data[232].Add(new { idx = i, val = Water_ZoneArea.ToString("0.0") });//존면적
                data.Add(new { cname = "water_zonearea", data = Water_data[232] });
                Water_data[233].Add(new { idx = i, val = Water_Zones_split.Count.ToString() });//존개수 
                data.Add(new { cname = "water_zonecount", data = Water_data[233] });
                if (Water_COP_Rule_total > 0)
                {
                    Water_data[234].Add(new { idx = i, val = "* DIN V 18599-7 표준 EER : " + Water_COP_Rule_total.ToString("0.0") + " 기준" });//냉방 법규 평균  
                    data.Add(new { cname = "water_cop_rule", data = Water_data[234] });
                }
                #endregion

                items.Add("Element_Chiler2.htm");
                s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
                System.Text.Json.JsonSerializer.Serialize(Air_data[10].ToArray());

                Debug.Print("start");

                script = "init(" + s + "," + s2 + "," + "[" + charts + "])";
                return script;
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
            List<object>[] Air_data = new List<object>[700];
            List<object>[] Water_data = new List<object>[700];
            double d;
            string sp;
            int i = -1, n;
            while (++i < 700)
            {
                Air_data[i] = new List<object>();
                Water_data[i] = new List<object>();
            }
            string charts = "";
            i = -1;
            while (++i < 번호.Length)
            {
                if (res.Length > 0)
                {

                    #region 냉방 절약 : 모든 요소기술 적용 절감량 중    
                    int j_cooling = 0;
                    for (int a = 0; a < ElementAlt.Length; a++)
                    {
                        if (ElementAlt[a] == "냉방")
                        {
                            j_cooling = a; break;
                        }
                    }
                    double cooling_saving = Element_EnergySaving[j_cooling];
                    double cooling_saving_elec = Element_ElecSaving[j_cooling];
                    double cooling_saving_noelec = Element_GasSaving[j_cooling];
                    #endregion


                    #region 냉방 절약 : 각 냉방설비별
                    double cooling_saving_total = 0;
                    for (int aa = 0; aa < CoolingGroup.Count; aa++)
                    {
                        Cooling_New_Old cc = (Cooling_New_Old)CoolingGroup[aa];
                        cooling_saving_total += cc.Before_Energy() - cc.After_Energy();
                    }

                    double[] cooling_element_saving = new double[CoolingGroup.Count];
                    double[] cooling_element_saving_elec = new double[CoolingGroup.Count];
                    double[] cooling_element_saving_gas = new double[CoolingGroup.Count];
                    for (int aa = 0; aa < CoolingGroup.Count; aa++)
                    {
                        Cooling_New_Old cc = (Cooling_New_Old)CoolingGroup[aa];
                        cooling_element_saving[aa] = cooling_saving * (cc.Before_Energy() - cc.After_Energy()) / cooling_saving_total;
                        cooling_element_saving_elec[aa] = cooling_saving_elec * (cc.Before_Energy() - cc.After_Energy()) / cooling_saving_total;
                        cooling_element_saving_gas[aa] = cooling_saving_noelec * (cc.Before_Energy() - cc.After_Energy()) / cooling_saving_total;
                    }
                    #endregion


                    #region 공냉식냉동기   
                    string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.냉방유닛,a.명칭,b.공급존,a.냉방출력,a.EER,b.번호,b.설치대수,b.압축기 From User_AirCooler as a Inner Join CoolingSystem_Form as b ON a.번호 = b.냉방유닛");
                    string[] Air_Name = new string[18]; string[] Air_Zone_text = new string[18];
                    double[] Air_Power = new double[18]; double[] Air_COP_Old = new double[18]; double[] Air_COP_New = new double[18]; double[] Air_Saving = new double[18]; double[] Air_Point = new double[18]; double[] Air_COP_Rule = new double[18];

                    double[] Air_elec = new double[18]; double[] Air_gas = new double[18];
                    ArrayList Air_Zones_split = new ArrayList();

                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            for (int aa = 0; aa < CoolingGroup.Count; aa++)
                            {
                                Cooling_New_Old cc = (Cooling_New_Old)CoolingGroup[aa];
                                if (Value[0][5] == cc.Num_New())
                                {
                                    Air_Saving[a] = cooling_element_saving[aa];
                                    Air_elec[a] = cooling_element_saving_elec[aa];
                                    Air_gas[a] = cooling_element_saving_gas[aa];
                                    for (int aaa = 0; aaa < cc.Num_Old().Count; aaa++)
                                    {
                                        string[][] OldSystem = Program.DB.querySQL(res[0][0], "Select a.냉방정격COP From User_AirHP as a Inner Join CoolingSystem_Form as b ON a.번호 = b.냉방유닛 Where b.번호 ='" + cc.Num_Old()[aaa] + "'");
                                        if (OldSystem.Length > 0)
                                        {
                                            if (Program.UTIL.ToDoubleOrZero(OldSystem[0][0]) == Program.UTIL.ToDoubleOrZero(Value[a][4]))
                                            { Air_COP_Old[a] = Program.UTIL.ToDoubleOrZero(Value[a][4]); break; }
                                            else if (Program.UTIL.ToDoubleOrZero(OldSystem[0][0]) < Air_COP_Old[a]) { Air_COP_Old[a] = Program.UTIL.ToDoubleOrZero(OldSystem[0][0]); }
                                            else if (Air_COP_Old[a] == 0) { Air_COP_Old[a] = Program.UTIL.ToDoubleOrZero(OldSystem[0][0]); }
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
                            Air_Name[a] = Value[a][1];

                            ArrayList splitzone = new ArrayList();
                            splitzone = Split_(Value[a][2]);
                            if (splitzone.Count > 1) { Air_Zone_text[a] = splitzone[0].ToString() + " 외 " + (splitzone.Count - 1).ToString() + "개"; }
                            else { Air_Zone_text[a] = splitzone[0].ToString(); }

                            for (int aa = 0; aa < splitzone.Count; aa++)
                            {
                                if (Air_Zones_split.Contains(splitzone[aa]))
                                { }
                                else { Air_Zones_split.Add(splitzone[aa]); }
                            }

                            Air_Power[a] = Program.UTIL.ToDoubleOrZero(Value[a][3]) * Program.UTIL.ToDoubleOrZero(Value[a][6]);
                            Air_COP_New[a] = Program.UTIL.ToDoubleOrZero(Value[a][4]);
                            string[][] kkk = Program.DB.getValue(DB.type.BaseDB_Cooling, "AirCooler", "EER", "압축기= '" + Value[a][7] + "' And 냉매='R134a' And 냉수출구온도 = '14' And 평균증발기온도='8'");
                            if (kkk.Length > 0)
                            {
                                Air_COP_Rule[a] = Program.UTIL.ToDoubleOrZero(kkk[0][0]);
                            }
                            Air_Point[a] = Math.Min(100, Air_COP_New[a] / Air_COP_Rule[a] * 100);
                        }
                    }

                    for (int a = 0; a < 18; a++)
                    {
                        if (Air_Saving[a] < 0) { Air_Saving[a] = 0; }
                        if (Air_elec[a] < 0) { Air_elec[a] = 0; }
                        if (Air_gas[a] < 0) { Air_gas[a] = 0; }
                    }

                    double air_total_saving = 0; double air_total_elec = 0; double air_total_gas = 0;
                    double Air_COP_New_total = 0; double Air_COP_Old_total = 0; double Air_Point_total = 0; double Air_COP_Rule_total = 0;

                    for (int a = 0; a < 18; a++)
                    {
                        Air_data[a].Add(new { idx = i, val = Air_Name[a] });//명칭
                        data.Add(new { cname = "air_name" + a, data = Air_data[a] });
                        if (Air_Name[a] != null & Air_Name[a] != "")
                        {
                            Air_data[18 + a].Add(new { idx = i, val = Air_Zone_text[a] });//존
                            data.Add(new { cname = "air_zone" + a, data = Air_data[18 + a] });

                            Air_data[72 + a].Add(new { idx = i, val = Air_Power[a].ToString("0.0") });//냉방용량
                            data.Add(new { cname = "air_power" + a, data = Air_data[72 + a] });

                            Air_data[90 + a].Add(new { idx = i, val = Air_COP_New[a].ToString("0.0") });//냉방COP
                            data.Add(new { cname = "air_cop_new" + a, data = Air_data[90 + a] });

                            if (Air_COP_Old[a] != 0)
                            { Air_data[126 + a].Add(new { idx = i, val = Air_COP_Old[a].ToString("0.0") }); }//냉방 기존 COP
                            else { Air_data[126 + a].Add(new { idx = i, val = "Not Chiler" }); }
                            data.Add(new { cname = "air_cop_old" + a, data = Air_data[126 + a] });
                            Air_data[162 + a].Add(new { idx = i, val = (Air_Saving[a] / Total_Energy_pre * 100).ToString("0.0") + " %" });//냉방 절감률
                            data.Add(new { cname = "air_saving" + a, data = Air_data[162 + a] });

                            d = Air_Point[a];
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            Air_data[198 + a].Add(new { idx = i, val = sp });//냉방 성능점수
                            data.Add(new { cname = "air_point" + a, data = Air_data[198 + a] });
                        }

                        //가중평균 
                        Air_COP_New_total += Air_COP_New[a] * Air_Power[a];
                        Air_COP_Old_total += Air_COP_Old[a] * Air_Power[a];
                        Air_Point_total += Air_Point[a] * Air_Power[a];
                        Air_COP_Rule_total += Air_COP_Rule[a] * Air_Power[a];
                    }
                    if (Air_Power.Sum() > 0)
                    {
                        Air_COP_New_total = Air_COP_New_total / Air_Power.Sum();
                        Air_COP_Old_total = Air_COP_Old_total / Air_Power.Sum();
                        Air_Point_total = Math.Min(100, Air_Point_total / Air_Power.Sum());
                        Air_COP_Rule_total = Air_COP_Rule_total / Air_Power.Sum();
                    }

                    for (int a = 0; a < 18; a++)
                    {
                        air_total_saving += Air_Saving[a];
                        air_total_elec += Air_elec[a];
                        air_total_gas += Air_gas[a];
                    }

                    Air_data[216].Add(new { idx = i, val = air_total_saving.ToString("#,##0") });//절감량 전체 
                    data.Add(new { cname = "air_saving_total", data = Air_data[216] });
                    Air_data[217].Add(new { idx = i, val = (air_total_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });//절감률 전체 
                    data.Add(new { cname = "air_saving_percent", data = Air_data[217] });
                    Air_data[218].Add(new { idx = i, val = (air_total_elec * 0.4747 / 1000000 * 1000 + air_total_gas / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000).ToString("0.0") });//tco2
                    data.Add(new { cname = "air_tco2", data = Air_data[218] });
                    Air_data[219].Add(new { idx = i, val = (air_total_elec * 0.00023 + air_total_gas / 38.9 / 0.277778 * 0.00103).ToString("0.0") });//절감량 전체 
                    data.Add(new { cname = "air_toe", data = Air_data[219] });

                    d = (air_total_saving / Total_Energy_pre * 100);
                    charts += "{donut:" + d + "},";

                    //합산 계 
                    Air_data[225].Add(new { idx = i, val = Air_Power.Sum().ToString("0.0") });//냉방 용량 합계  
                    data.Add(new { cname = "air_power_total", data = Air_data[225] });
                    Air_data[226].Add(new { idx = i, val = Air_COP_Old_total.ToString("0.0") });//냉방 기존 COP 평균  
                    data.Add(new { cname = "air_cop_old_total", data = Air_data[226] });
                    Air_data[227].Add(new { idx = i, val = Air_COP_New_total.ToString("0.0") });//냉방 COP 평균  
                    data.Add(new { cname = "air_cop_new_total", data = Air_data[227] });
                    Air_data[228].Add(new { idx = i, val = (air_total_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });//냉방 절감량 합계  
                    data.Add(new { cname = "air_saving_total2", data = Air_data[228] });
                    d = Air_Point_total;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    Air_data[229].Add(new { idx = i, val = sp });//냉방 성능수준 평균  
                    data.Add(new { cname = "air_point_total", data = Air_data[229] });

                    double Air_Qmax_c = 0; double Air_ZoneArea = 0;
                    for (int a = 0; a < Air_Zones_split.Count; a++)
                    {
                        string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호='" + Air_Zones_split[a].ToString() + "' And 난방_냉방='냉방' and 비이용일_이용일='이용일' and 월='8월'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                        {
                            Air_Qmax_c += Program.UTIL.ToDoubleOrZero(ZoneValue[0][0]);
                        }
                        ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "존번호='" + Air_Zones_split[a].ToString() + "'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                        {
                            Air_ZoneArea += Program.UTIL.ToDoubleOrZero(ZoneValue[0][0]);
                        }
                    }
                    Air_data[231].Add(new { idx = i, val = (Air_Qmax_c / 1000).ToString("0.0") });//냉방부하 
                    data.Add(new { cname = "air_qmax", data = Air_data[231] });
                    Air_data[232].Add(new { idx = i, val = Air_ZoneArea.ToString("0.0") });//존면적
                    data.Add(new { cname = "air_zonearea", data = Air_data[232] });
                    Air_data[233].Add(new { idx = i, val = Air_Zones_split.Count.ToString() });//존개수 
                    data.Add(new { cname = "air_zonecount", data = Air_data[233] });
                    if (Air_COP_Rule_total > 0)
                    {
                        Air_data[234].Add(new { idx = i, val = "* DIN V 18599-7 표준 EER : " + Air_COP_Rule_total.ToString("0.0") + " 기준" });//냉방 법규 평균  
                        data.Add(new { cname = "air_cop_rule", data = Air_data[234] });
                    }

                    #endregion

                    #region 수냉식냉동기    
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.냉방유닛,a.명칭,b.공급존,a.냉방출력,a.EER,b.번호,b.설치대수,b.압축기 From User_WaterCooler as a Inner Join CoolingSystem_Form as b ON a.번호 = b.냉방유닛");
                    string[] Water_Name = new string[18]; string[] Water_Zone_text = new string[18];
                    double[] Water_Power = new double[18]; double[] Water_COP_Old = new double[18]; double[] Water_COP_New = new double[18]; double[] Water_Saving = new double[18]; double[] Water_Point = new double[18]; double[] Water_COP_Rule = new double[18];

                    double[] Water_elec = new double[18]; double[] Water_gas = new double[18];
                    ArrayList Water_Zones_split = new ArrayList();

                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            for (int aa = 0; aa < CoolingGroup.Count; aa++)
                            {
                                Cooling_New_Old cc = (Cooling_New_Old)CoolingGroup[aa];
                                if (Value[0][5] == cc.Num_New())
                                {
                                    Water_Saving[a] = cooling_element_saving[aa];
                                    Water_elec[a] = cooling_element_saving_elec[aa];
                                    Water_gas[a] = cooling_element_saving_gas[aa];
                                    for (int aaa = 0; aaa < cc.Num_Old().Count; aaa++)
                                    {
                                        string[][] OldSystem = Program.DB.querySQL(res[0][0], "Select a.냉방정격COP From User_AirHP as a Inner Join CoolingSystem_Form as b ON a.번호 = b.냉방유닛 Where b.번호 ='" + cc.Num_Old()[aaa] + "'");
                                        if (OldSystem.Length > 0)
                                        {
                                            if (Program.UTIL.ToDoubleOrZero(OldSystem[0][0]) == Program.UTIL.ToDoubleOrZero(Value[a][4]))
                                            { Water_COP_Old[a] = Program.UTIL.ToDoubleOrZero(Value[a][4]); break; }
                                            else if (Program.UTIL.ToDoubleOrZero(OldSystem[0][0]) < Water_COP_Old[a]) { Water_COP_Old[a] = Program.UTIL.ToDoubleOrZero(OldSystem[0][0]); }
                                            else if (Water_COP_Old[a] == 0) { Water_COP_Old[a] = Program.UTIL.ToDoubleOrZero(OldSystem[0][0]); }
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
                            Water_Name[a] = Value[a][1];

                            ArrayList splitzone = new ArrayList();
                            splitzone = Split_(Value[a][2]);
                            if (splitzone.Count > 1) { Water_Zone_text[a] = splitzone[0].ToString() + " 외 " + (splitzone.Count - 1).ToString() + "개"; }
                            else { Water_Zone_text[a] = splitzone[0].ToString(); }

                            for (int aa = 0; aa < splitzone.Count; aa++)
                            {
                                if (Water_Zones_split.Contains(splitzone[aa]))
                                { }
                                else { Water_Zones_split.Add(splitzone[aa]); }
                            }
                            Water_Power[a] = Program.UTIL.ToDoubleOrZero(Value[a][3]) * Program.UTIL.ToDoubleOrZero(Value[a][6]);
                            Water_COP_New[a] = Program.UTIL.ToDoubleOrZero(Value[a][4]);
                            string[][] kkk = Program.DB.getValue(DB.type.BaseDB_Cooling, "WaterCooler", "EER", "압축기= '" + Value[a][7] + "' And 냉매='R134a' And 냉수출구온도 = '14' And 냉각수입구온도='27'");
                            if (kkk.Length > 0)
                            {
                                Water_COP_Rule[a] = Program.UTIL.ToDoubleOrZero(kkk[0][0]);
                            }
                            Water_Point[a] = Math.Min(100, Water_COP_New[a] / Water_COP_Rule[a] * 100);
                        }
                    }
                    for (int a = 0; a < 18; a++)
                    {
                        if (Water_Saving[a] < 0) { Water_Saving[a] = 0; }
                        if (Water_elec[a] < 0) { Water_elec[a] = 0; }
                        if (Water_gas[a] < 0) { Water_gas[a] = 0; }
                    }
                    double water_total_saving = 0; double water_total_elec = 0; double water_total_gas = 0;
                    double Water_COP_New_total = 0; double Water_COP_Old_total = 0; double Water_Point_total = 0; double Water_COP_Rule_total = 0;
                    for (int a = 0; a < 18; a++)
                    {
                        Water_data[a].Add(new { idx = i, val = Water_Name[a] });//명칭
                        data.Add(new { cname = "water_name" + a, data = Water_data[a] });
                        if (Water_Name[a] != null & Water_Name[a] != "")
                        {
                            Water_data[18 + a].Add(new { idx = i, val = Water_Zone_text[a] });//존
                            data.Add(new { cname = "water_zone" + a, data = Water_data[18 + a] });

                            Water_data[72 + a].Add(new { idx = i, val = Water_Power[a].ToString("0.0") });//냉방용량
                            data.Add(new { cname = "water_power" + a, data = Water_data[72 + a] });

                            Water_data[90 + a].Add(new { idx = i, val = Water_COP_New[a].ToString("0.0") });//냉방COP
                            data.Add(new { cname = "water_cop_new" + a, data = Water_data[90 + a] });

                            if (Water_COP_Old[a] != 0)
                            { Water_data[126 + a].Add(new { idx = i, val = Water_COP_Old[a].ToString("0.0") }); }//냉방 기존 COP
                            else { Water_data[126 + a].Add(new { idx = i, val = "Not Chiler" }); }
                            data.Add(new { cname = "water_cop_old" + a, data = Water_data[126 + a] });
                            Water_data[162 + a].Add(new { idx = i, val = (Water_Saving[a] / Total_Energy_pre * 100).ToString("0.0") + " %" });//냉방 절감률            
                            data.Add(new { cname = "water_saving" + a, data = Water_data[162 + a] });

                            d = Water_Point[a];
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            Water_data[198 + a].Add(new { idx = i, val = sp });//냉방 성능점수
                            data.Add(new { cname = "water_point" + a, data = Water_data[198 + a] });
                        }

                        //가중평균 
                        Water_COP_New_total += Water_COP_New[a] * Water_Power[a];
                        Water_COP_Old_total += Water_COP_Old[a] * Water_Power[a];
                        Water_Point_total += Water_Point[a] * Water_Power[a];
                        Water_COP_Rule_total += Water_COP_Rule[a] * Water_Power[a];
                    }
                    if (Water_Power.Sum() > 0)
                    {
                        Water_COP_New_total = Water_COP_New_total / Water_Power.Sum();
                        Water_COP_Old_total = Water_COP_Old_total / Water_Power.Sum();
                        Water_Point_total = Math.Min(100, Water_Point_total / Water_Power.Sum());
                        Water_COP_Rule_total = Water_COP_Rule_total / Water_Power.Sum();
                    }

                    for (int a = 0; a < 18; a++)
                    {
                        water_total_saving += Water_Saving[a];
                        water_total_elec += Water_elec[a];
                        water_total_gas += Water_gas[a];
                    }

                    Water_data[216].Add(new { idx = i, val = water_total_saving.ToString("#,##0") });//절감량 전체 
                    data.Add(new { cname = "water_saving_total", data = Water_data[216] });
                    Water_data[217].Add(new { idx = i, val = (water_total_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });//절감률 전체 
                    data.Add(new { cname = "water_saving_percent", data = Water_data[217] });
                    Water_data[218].Add(new { idx = i, val = (water_total_elec * 0.4747 / 1000000 * 1000 + water_total_gas / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000).ToString("0.0") });//tco2
                    data.Add(new { cname = "water_tco2", data = Water_data[218] });
                    Water_data[219].Add(new { idx = i, val = (water_total_elec * 0.00023 + water_total_gas / 38.9 / 0.277778 * 0.00103).ToString("0.0") });//절감량 전체 
                    data.Add(new { cname = "water_toe", data = Water_data[219] });

                    d = (air_total_saving / Total_Energy_pre * 100);
                    charts += "{donut:" + d + "},";

                    //합산 계 
                    Water_data[225].Add(new { idx = i, val = Water_Power.Sum().ToString("0.0") });//냉방 용량 합계  
                    data.Add(new { cname = "water_power_total", data = Water_data[225] });
                    Water_data[226].Add(new { idx = i, val = Water_COP_Old_total.ToString("0.0") });//냉방 기존 COP 평균  
                    data.Add(new { cname = "water_cop_old_total", data = Water_data[226] });
                    Water_data[227].Add(new { idx = i, val = Water_COP_New_total.ToString("0.0") });//냉방 COP 평균  
                    data.Add(new { cname = "water_cop_new_total", data = Water_data[227] });
                    Water_data[228].Add(new { idx = i, val = (water_total_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });//냉방 절감량 합계  
                    data.Add(new { cname = "water_saving_total2", data = Water_data[228] });
                    d = Water_Point_total;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    Water_data[229].Add(new { idx = i, val = sp });//냉방 성능수준 평균  
                    data.Add(new { cname = "water_point_total", data = Water_data[229] });

                    double Water_Qmax_c = 0; double Water_ZoneArea = 0;
                    for (int a = 0; a < Water_Zones_split.Count; a++)
                    {
                        string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호='" + Water_Zones_split[a].ToString() + "' And 난방_냉방='냉방' and 비이용일_이용일='이용일' and 월='8월'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                        {
                            Water_Qmax_c += Program.UTIL.ToDoubleOrZero(ZoneValue[0][0]);
                        }
                        ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "존번호='" + Water_Zones_split[a].ToString() + "'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                        {
                            Water_ZoneArea += Program.UTIL.ToDoubleOrZero(ZoneValue[0][0]);
                        }
                    }
                    Water_data[231].Add(new { idx = i, val = (Water_Qmax_c / 1000).ToString("0.0") });//냉방부하 
                    data.Add(new { cname = "water_qmax", data = Water_data[231] });
                    Water_data[232].Add(new { idx = i, val = Water_ZoneArea.ToString("0.0") });//존면적
                    data.Add(new { cname = "water_zonearea", data = Water_data[232] });
                    Water_data[233].Add(new { idx = i, val = Water_Zones_split.Count.ToString() });//존개수 
                    data.Add(new { cname = "water_zonecount", data = Water_data[233] });
                    if (Water_COP_Rule_total > 0)
                    {
                        Water_data[234].Add(new { idx = i, val = "* DIN V 18599-7 표준 EER : " + Water_COP_Rule_total.ToString("0.0") + " 기준" });//냉방 법규 평균  
                        data.Add(new { cname = "water_cop_rule", data = Water_data[234] });
                    }
                    #endregion

                    items.Add("Element_Chiler.htm");
                    s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                    s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
                    System.Text.Json.JsonSerializer.Serialize(Air_data[10].ToArray());

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