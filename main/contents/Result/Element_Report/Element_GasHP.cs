using main.contents.Result.Element_Report;
using Microsoft.Web.WebView2.Core;
using System.Collections;
using System.Diagnostics;

namespace main.contents.Result
{
    public partial class Element_GasHP : Form
    {
        bool scriptable = false;
        public Element_GasHP()
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
            List<object>[] ghp_data = new List<object>[700];
            List<object>[] abs_data = new List<object>[700];
            double d;
            string sp;
            int i = -1, n;
            while (++i < 700)
            {
                ghp_data[i] = new List<object>();
                abs_data[i] = new List<object>();
            }
            string charts = "";
            i = -1;
            while (++i < 번호.Length)
            {

                #region 냉난방 가스히트펌프   

                double Total_Energy_pre = 0;
                string[][] value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='냉난방GHP'");
                string[][] value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                double ghp_total_saving = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    Total_Energy_pre = Program.UTIL.ToDoubleOrZero(value3[0][0]);
                    ghp_total_saving = Math.Max(0, Program.UTIL.ToDoubleOrZero(value3[0][0]) - Program.UTIL.ToDoubleOrZero(value[0][0]));
                }

                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전기' and 검토유형='냉난방GHP'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전기'");
                double ghp_total_elec = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    ghp_total_elec = Math.Max(0, Program.UTIL.ToDoubleOrZero(value3[0][0]) - Program.UTIL.ToDoubleOrZero(value[0][0]));
                }
                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='냉난방GHP'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                double ghp_total_gas = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    ghp_total_gas = Math.Max(0, Program.UTIL.ToDoubleOrZero(value3[0][0]) - Program.UTIL.ToDoubleOrZero(value[0][0]));
                }

                string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.외기히트펌프번호,a.명칭,b.존,a.난방정격용량,a.난방정격COP,a.냉방정격용량,a.냉방정격COP,b.번호,b.외기히트펌프대수 From User_AirHP as a Inner Join HeatingSystem_Form as b ON a.번호 = b.외기히트펌프번호 Where a.난방냉방='냉난방' And NOT a.연료='전기'");
                string[] ghp_Name = new string[18]; string[] ghp_Zone_text = new string[18];
                double[] ghp_Power_H = new double[18]; double[] ghp_COP_New_H = new double[18]; double[] ghp_Saving_H = new double[18]; double[] ghp_Point_H = new double[18]; double[] ghp_COP_Rule_H = new double[18];
                double[] ghp_Power_C = new double[18]; double[] ghp_COP_New_C = new double[18]; double[] ghp_Saving_C = new double[18]; double[] ghp_Point_C = new double[18]; double[] ghp_COP_Rule_C = new double[18];

                double[] ghp_elec_H = new double[18]; double[] ghp_elec_C = new double[18];
                double[] ghp_gas_H = new double[18]; double[] ghp_gas_C = new double[18];
                ArrayList ghp_Zones_split = new ArrayList();

                if (Value.Length > 0)
                {
                    for (int a = 0; a < Value.Length; a++)
                    {
                        ghp_Name[a] = Value[a][1];

                        ArrayList splitzone = new ArrayList();
                        splitzone = Split_(Value[a][2]);
                        if (splitzone.Count > 1) { ghp_Zone_text[a] = splitzone[0].ToString() + " 외 " + (splitzone.Count - 1).ToString() + "개"; }
                        else { ghp_Zone_text[a] = splitzone[0].ToString(); }

                        for (int aa = 0; aa < splitzone.Count; aa++)
                        {
                            if (ghp_Zones_split.Contains(splitzone[aa]))
                            { }
                            else { ghp_Zones_split.Add(splitzone[aa]); }
                        }


                        ghp_Power_H[a] = Program.UTIL.ToDoubleOrZero(Value[a][3]) * Program.UTIL.ToDoubleOrZero(Value[a][8]);
                        ghp_COP_New_H[a] = Program.UTIL.ToDoubleOrZero(Value[a][4]);

                        ghp_Power_C[a] = Program.UTIL.ToDoubleOrZero(Value[a][5]) * Program.UTIL.ToDoubleOrZero(Value[a][8]);
                        ghp_COP_New_C[a] = Program.UTIL.ToDoubleOrZero(Value[a][6]);

                        double Rule = 0; string[][] kkk = Program.DB.getValue(DB.type.BaseDB_Cooling, "AirCooler", "EER", "압축기= '스크롤' And 냉매='R134a' And 냉수출구온도 = '14' And 평균증발기온도='8'");
                        if (kkk.Length > 0)
                        {
                            Rule = Program.UTIL.ToDoubleOrZero(kkk[0][0]);
                        }
                        ghp_COP_Rule_H[a] = 3.8; //DIN V 18599-5 table C.1
                        ghp_COP_Rule_C[a] = Rule; //DIN V 18599-7 table 27

                        ghp_Point_H[a] = Math.Min(100, ghp_COP_New_H[a] / ghp_COP_Rule_H[a] * 100);
                        ghp_Point_C[a] = Math.Min(100, ghp_COP_New_H[a] / ghp_COP_Rule_C[a] * 100);
                    }
                }

                for (int a = 0; a < 18; a++)
                {
                    if (ghp_Saving_H[a] < 0) { ghp_Saving_H[a] = 0; }
                    if (ghp_elec_H[a] < 0) { ghp_elec_H[a] = 0; }
                    if (ghp_gas_H[a] < 0) { ghp_gas_H[a] = 0; }

                    if (ghp_Saving_C[a] < 0) { ghp_Saving_C[a] = 0; }
                    if (ghp_elec_C[a] < 0) { ghp_elec_C[a] = 0; }
                    if (ghp_gas_C[a] < 0) { ghp_gas_C[a] = 0; }
                }
                double ghp_COP_New_H_total = 0; double ghp_Point_H_total = 0; double ghp_COP_Rule_H_total = 0;
                double ghp_COP_New_C_total = 0; double ghp_Point_C_total = 0; double ghp_COP_Rule_C_total = 0;
                for (int a = 0; a < 18; a++)
                {
                    ghp_data[a].Add(new { idx = i, val = ghp_Name[a] });//명칭
                    data.Add(new { cname = "ghp_name" + a, data = ghp_data[a] });
                    if (ghp_Name[a] != null & ghp_Name[a] != "")
                    {
                        ghp_data[18 + a].Add(new { idx = i, val = ghp_Zone_text[a] });//존
                        data.Add(new { cname = "ghp_zone" + a, data = ghp_data[18 + a] });

                        ghp_data[36 + a].Add(new { idx = i, val = ghp_Power_H[a].ToString("0.0") });//난방용량
                        data.Add(new { cname = "ghp_power_h" + a, data = ghp_data[36 + a] });

                        ghp_data[54 + a].Add(new { idx = i, val = ghp_COP_New_H[a].ToString("0.0") });//난방COP
                        data.Add(new { cname = "ghp_cop_new_h" + a, data = ghp_data[54 + a] });

                        ghp_data[72 + a].Add(new { idx = i, val = ghp_Power_C[a].ToString("0.0") });//냉방용량
                        data.Add(new { cname = "ghp_power_c" + a, data = ghp_data[72 + a] });

                        ghp_data[90 + a].Add(new { idx = i, val = ghp_COP_New_C[a].ToString("0.0") });//냉방COP
                        data.Add(new { cname = "ghp_cop_new_c" + a, data = ghp_data[90 + a] });

                        d = ghp_Point_H[a];
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        ghp_data[180 + a].Add(new { idx = i, val = sp });//난방 성능점수
                        data.Add(new { cname = "ghp_point_h" + a, data = ghp_data[180 + a] });

                        d = ghp_Point_C[a];
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        ghp_data[198 + a].Add(new { idx = i, val = sp });//냉방 성능점수
                        data.Add(new { cname = "ghp_point_c" + a, data = ghp_data[198 + a] });
                    }

                    //가중평균 
                    ghp_COP_New_H_total += ghp_COP_New_H[a] * ghp_Power_H[a];
                    ghp_Point_H_total += ghp_Point_H[a] * ghp_Power_H[a];
                    ghp_COP_New_C_total += ghp_COP_New_C[a] * ghp_Power_C[a];
                    ghp_Point_C_total += ghp_Point_C[a] * ghp_Power_C[a];
                    ghp_COP_Rule_H_total += ghp_COP_Rule_H[a] * ghp_Power_H[a];
                    ghp_COP_Rule_C_total += ghp_COP_Rule_C[a] * ghp_Power_C[a];
                }
                if (ghp_Power_H.Sum() > 0 && ghp_Power_C.Sum() > 0)
                {
                    ghp_COP_New_H_total = ghp_COP_New_H_total / ghp_Power_H.Sum();
                    ghp_Point_H_total = Math.Min(100, ghp_Point_H_total / ghp_Power_H.Sum());
                    ghp_COP_New_C_total = ghp_COP_New_C_total / ghp_Power_C.Sum();
                    ghp_Point_C_total = Math.Min(100, ghp_Point_C_total / ghp_Power_C.Sum());
                    ghp_COP_Rule_H_total = ghp_COP_Rule_H_total / ghp_Power_H.Sum();
                    ghp_COP_Rule_C_total = ghp_COP_Rule_C_total / ghp_Power_C.Sum();
                }

                for (int a = 0; a < 18; a++)
                {
                    ghp_total_saving += ghp_Saving_H[a] + ghp_Saving_C[a];
                    ghp_total_elec += ghp_elec_H[a] + ghp_elec_C[a];
                    ghp_total_gas += ghp_gas_H[a] + ghp_gas_C[a];
                }
                
                double v = double.IsNaN(ghp_total_saving) ? 0: ghp_total_saving;
                ghp_data[216].Add(new { idx = i, val = v.ToString("#,##0") });//절감량 전체 
                data.Add(new { cname = "ghp_saving_total", data = ghp_data[216] });

                v = double.IsNaN(ghp_total_saving / Total_Energy_pre * 100) ? 0 : ghp_total_saving / Total_Energy_pre * 100;
                ghp_data[217].Add(new { idx = i, val = (v).ToString("0.0") + " %" });//절감률 전체 
                data.Add(new { cname = "ghp_saving_percent", data = ghp_data[217] });

                v = double.IsNaN(ghp_total_elec * 0.4747 / 1000000 * 1000 + ghp_total_gas / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000) ? 0 : ghp_total_elec * 0.4747 / 1000000 * 1000 + ghp_total_gas / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                ghp_data[218].Add(new { idx = i, val = v.ToString("0.0") });//tco2
                data.Add(new { cname = "ghp_tco2", data = ghp_data[218] });

                v = double.IsNaN(ghp_total_elec * 0.00023 + ghp_total_gas / 38.9 / 0.277778 * 0.00103) ? 0 : ghp_total_elec * 0.00023 + ghp_total_gas / 38.9 / 0.277778 * 0.00103;
                ghp_data[219].Add(new { idx = i, val = (v).ToString("0.0") });//절감량 전체 
                data.Add(new { cname = "ghp_toe", data = ghp_data[219] });

                d = (ghp_total_saving / Total_Energy_pre * 100);
                charts += "{donut:" + d + "},";

                //합산 계 
                ghp_data[220].Add(new { idx = i, val = ghp_Power_H.Sum().ToString("0.0") });//난방 용량 합계  
                data.Add(new { cname = "ghp_power_h_total", data = ghp_data[220] });
                ghp_data[222].Add(new { idx = i, val = ghp_COP_New_H_total.ToString("0.0") });//난방 COP 평균  
                data.Add(new { cname = "ghp_cop_new_h_total", data = ghp_data[222] });
                ghp_data[223].Add(new { idx = i, val = (ghp_Saving_H.Sum() / Total_Energy_pre * 100).ToString("0.0") + " %" });//난방 절감량 합계  
                data.Add(new { cname = "ghp_saving_h_total", data = ghp_data[223] });
                d = ghp_Point_H_total;
                if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                ghp_data[224].Add(new { idx = i, val = sp });//난방 성능수준 평균  
                data.Add(new { cname = "ghp_point_h_total", data = ghp_data[224] });

                ghp_data[225].Add(new { idx = i, val = ghp_Power_C.Sum().ToString("0.0") });//냉방 용량 합계  
                data.Add(new { cname = "ghp_power_c_total", data = ghp_data[225] });
                ghp_data[227].Add(new { idx = i, val = ghp_COP_New_C_total.ToString("0.0") });//냉방 COP 평균  
                data.Add(new { cname = "ghp_cop_new_c_total", data = ghp_data[227] });
                ghp_data[228].Add(new { idx = i, val = (ghp_Saving_C.Sum() / Total_Energy_pre * 100).ToString("0.0") + " %" });//냉방 절감량 합계  
                data.Add(new { cname = "ghp_saving_c_total", data = ghp_data[228] });
                d = ghp_Point_C_total;
                if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                ghp_data[229].Add(new { idx = i, val = sp });//냉방 성능수준 평균  
                data.Add(new { cname = "ghp_point_c_total", data = ghp_data[229] });

                double ghp_Qmax_h = 0; double ghp_Qmax_c = 0; double ghp_ZoneArea = 0;
                for (int a = 0; a < ghp_Zones_split.Count; a++)
                {
                    string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호='" + ghp_Zones_split[a].ToString() + "' And 난방_냉방='난방' and 비이용일_이용일='이용일' and 월='1월'");
                    if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                    {
                        ghp_Qmax_h += Program.UTIL.ToDoubleOrZero(ZoneValue[0][0]);
                    }
                    ZoneValue = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호='" + ghp_Zones_split[a].ToString() + "' And 난방_냉방='냉방' and 비이용일_이용일='이용일' and 월='8월'");
                    if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                    {
                        ghp_Qmax_c += Program.UTIL.ToDoubleOrZero(ZoneValue[0][0]);
                    }
                    ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "존번호='" + ghp_Zones_split[a].ToString() + "'");
                    if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                    {
                        ghp_ZoneArea += Program.UTIL.ToDoubleOrZero(ZoneValue[0][0]);
                    }
                }
                ghp_data[230].Add(new { idx = i, val = (ghp_Qmax_h / 1000).ToString("0.0") });//난방부하 
                data.Add(new { cname = "ghp_qmax_h", data = ghp_data[230] });
                ghp_data[231].Add(new { idx = i, val = (ghp_Qmax_c / 1000).ToString("0.0") });//냉방부하 
                data.Add(new { cname = "ghp_qmax_c", data = ghp_data[231] });
                ghp_data[232].Add(new { idx = i, val = ghp_ZoneArea.ToString("0.0") });//존면적
                data.Add(new { cname = "ghp_zonearea", data = ghp_data[232] });
                ghp_data[233].Add(new { idx = i, val = ghp_Zones_split.Count.ToString() });//존개수 
                data.Add(new { cname = "ghp_zonecount", data = ghp_data[233] });
                if (ghp_COP_Rule_H_total > 0)
                {
                    ghp_data[234].Add(new { idx = i, val = "* DIN V 18599-5 표준 COP : " + ghp_COP_Rule_H_total.ToString("0.0") + " 기준" });//난방 법규 평균  
                    data.Add(new { cname = "ghp_cop_rule_h", data = ghp_data[234] });
                }

                if (ghp_COP_Rule_C_total > 0)
                {
                    ghp_data[235].Add(new { idx = i, val = "* DIN V 18599-7 표준 EER : " + ghp_COP_Rule_C_total.ToString("0.0") + " 기준" });//냉방 법규 평균  
                    data.Add(new { cname = "ghp_cop_rule_c", data = ghp_data[235] });
                }
                #endregion


                #region 흡수식냉온수기   
                Total_Energy_pre = 0;
                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='흡수식냉온수기'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                double abs_total_saving = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    Total_Energy_pre = Program.UTIL.ToDoubleOrZero(value3[0][0]);
                    abs_total_saving = Math.Max(0, Program.UTIL.ToDoubleOrZero(value3[0][0]) - Program.UTIL.ToDoubleOrZero(value[0][0]));
                }

                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전기' and 검토유형='흡수식냉온수기'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전기'");
                double abs_total_elec = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    abs_total_elec = Math.Max(0, Program.UTIL.ToDoubleOrZero(value3[0][0]) - Program.UTIL.ToDoubleOrZero(value[0][0]));
                }
                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='흡수식냉온수기'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                double abs_total_gas = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    abs_total_gas = Math.Max(0, Program.UTIL.ToDoubleOrZero(value3[0][0]) - Program.UTIL.ToDoubleOrZero(value[0][0]));
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.흡수식온수기번호,a.명칭,b.존,a.난방용량,a.난방성능,a.냉방용량,a.냉방성능,b.번호,b.흡수식온수기대수 From User_ABS as a Inner Join HeatingSystem_Form as b ON a.번호 = b.흡수식온수기번호 Where a.난방냉방='냉난방'");
                string[] abs_Name = new string[18]; string[] abs_Zone_text = new string[18];
                double[] abs_Power_H = new double[18]; double[] abs_COP_New_H = new double[18]; double[] abs_Point_H = new double[18]; double[] abs_COP_Rule_H = new double[18];
                double[] abs_Power_C = new double[18]; double[] abs_COP_New_C = new double[18]; double[] abs_Point_C = new double[18]; double[] abs_COP_Rule_C = new double[18];

                ArrayList abs_Zones_split = new ArrayList();

                if (Value.Length > 0)
                {
                    for (int a = 0; a < Value.Length; a++)
                    {
                        abs_Name[a] = Value[a][1];

                        ArrayList splitzone = new ArrayList();
                        splitzone = Split_(Value[a][2]);
                        if (splitzone.Count > 1) { abs_Zone_text[a] = splitzone[0].ToString() + " 외 " + (splitzone.Count - 1).ToString() + "개"; }
                        else { abs_Zone_text[a] = splitzone[0].ToString(); }

                        for (int aa = 0; aa < splitzone.Count; aa++)
                        {
                            if (abs_Zones_split.Contains(splitzone[aa]))
                            { }
                            else { abs_Zones_split.Add(splitzone[aa]); }
                        }


                        abs_Power_H[a] = Program.UTIL.ToDoubleOrZero(Value[a][3]) * Program.UTIL.ToDoubleOrZero(Value[a][8]);
                        abs_COP_New_H[a] = Program.UTIL.ToDoubleOrZero(Value[a][4]);

                        abs_Power_C[a] = Program.UTIL.ToDoubleOrZero(Value[a][5]) * Program.UTIL.ToDoubleOrZero(Value[a][8]);
                        abs_COP_New_C[a] = Program.UTIL.ToDoubleOrZero(Value[a][6]);

                        abs_COP_Rule_H[a] = 1.2; //EPI 1점 
                        abs_COP_Rule_C[a] = 1.2;

                        abs_Point_H[a] = Math.Min(100, abs_COP_New_H[a] / abs_COP_Rule_H[a] * 100);
                        abs_Point_C[a] = Math.Min(100, abs_COP_New_H[a] / abs_COP_Rule_C[a] * 100);
                    }
                }

                double abs_COP_New_H_total = 0; double abs_Point_H_total = 0;
                double abs_COP_New_C_total = 0; double abs_Point_C_total = 0;
                for (int a = 0; a < 18; a++)
                {
                    abs_data[a].Add(new { idx = i, val = abs_Name[a] });//명칭
                    data.Add(new { cname = "abs_name" + a, data = abs_data[a] });
                    if (abs_Name[a] != null & abs_Name[a] != "")
                    {
                        abs_data[18 + a].Add(new { idx = i, val = abs_Zone_text[a] });//존
                        data.Add(new { cname = "abs_zone" + a, data = abs_data[18 + a] });

                        abs_data[36 + a].Add(new { idx = i, val = abs_Power_H[a].ToString("0.0") });//난방용량
                        data.Add(new { cname = "abs_power_h" + a, data = abs_data[36 + a] });

                        abs_data[54 + a].Add(new { idx = i, val = abs_COP_New_H[a].ToString("0.0") });//난방COP
                        data.Add(new { cname = "abs_cop_new_h" + a, data = abs_data[54 + a] });

                        abs_data[72 + a].Add(new { idx = i, val = abs_Power_C[a].ToString("0.0") });//냉방용량
                        data.Add(new { cname = "abs_power_c" + a, data = abs_data[72 + a] });

                        abs_data[90 + a].Add(new { idx = i, val = abs_COP_New_C[a].ToString("0.0") });//냉방COP
                        data.Add(new { cname = "abs_cop_new_c" + a, data = abs_data[90 + a] });


                        d = abs_Point_H[a];
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        abs_data[180 + a].Add(new { idx = i, val = sp });//난방 성능점수
                        data.Add(new { cname = "abs_point_h" + a, data = abs_data[180 + a] });

                        d = abs_Point_C[a];
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        abs_data[198 + a].Add(new { idx = i, val = sp });//냉방 성능점수
                        data.Add(new { cname = "abs_point_c" + a, data = abs_data[198 + a] });
                    }

                    //가중평균 
                    abs_COP_New_H_total += abs_COP_New_H[a] * abs_Power_H[a];
                    abs_Point_H_total += abs_Point_H[a] * abs_Power_H[a];
                    abs_COP_New_C_total += abs_COP_New_C[a] * abs_Power_C[a];
                    abs_Point_C_total += abs_Point_C[a] * abs_Power_C[a];
                }
                if (abs_Power_H.Sum() > 0 && abs_Power_C.Sum() > 0)
                {
                    abs_COP_New_H_total = abs_COP_New_H_total / abs_Power_H.Sum();
                    abs_Point_H_total = Math.Min(100, abs_Point_H_total / abs_Power_H.Sum());
                    abs_COP_New_C_total = abs_COP_New_C_total / abs_Power_C.Sum();
                    abs_Point_C_total = Math.Min(100, abs_Point_C_total / abs_Power_C.Sum());
                }

                double v2 = double.IsNaN(abs_total_saving) ? 0: abs_total_saving;
                abs_data[216].Add(new { idx = i, val = v2.ToString("#,##0") });//절감량 전체 
                data.Add(new { cname = "abs_saving_total", data = abs_data[216] });

                v2 = double.IsNaN(abs_total_saving / Total_Energy_pre * 100 )? 0 : abs_total_saving / Total_Energy_pre * 100;
                abs_data[217].Add(new { idx = i, val = (v2).ToString("0.0") + " %" });//절감률 전체 
                data.Add(new { cname = "abs_saving_percent", data = abs_data[217] });

                v2 = double.IsNaN(abs_total_elec * 0.4747 / 1000000 * 1000 + abs_total_gas / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000 )? 0 : abs_total_elec * 0.4747 / 1000000 * 1000 + abs_total_gas / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                abs_data[218].Add(new { idx = i, val = (v2).ToString("0.0") });//tco2
                data.Add(new { cname = "abs_tco2", data = abs_data[218] });

                v2 = double.IsNaN(abs_total_elec * 0.00023 + abs_total_gas / 38.9 / 0.277778 * 0.00103 ) ? 0 : abs_total_elec * 0.00023 + abs_total_gas / 38.9 / 0.277778 * 0.00103;
                abs_data[219].Add(new { idx = i, val = (v2).ToString("0.0") });//절감량 전체 
                data.Add(new { cname = "abs_toe", data = abs_data[219] });

                d = (abs_total_saving / Total_Energy_pre * 100);
                charts += "{donut:" + d + "},";

                //합산 계 
                abs_data[220].Add(new { idx = i, val = abs_Power_H.Sum().ToString("0.0") });//난방 용량 합계  
                data.Add(new { cname = "abs_power_h_total", data = abs_data[220] });
                abs_data[222].Add(new { idx = i, val = abs_COP_New_H_total.ToString("0.0") });//난방 COP 평균  
                data.Add(new { cname = "abs_cop_new_h_total", data = abs_data[222] });
                d = abs_Point_H_total;
                if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                abs_data[224].Add(new { idx = i, val = sp });//난방 성능수준 평균  
                data.Add(new { cname = "abs_point_h_total", data = abs_data[224] });

                abs_data[225].Add(new { idx = i, val = abs_Power_C.Sum().ToString("0.0") });//냉방 용량 합계  
                data.Add(new { cname = "abs_power_c_total", data = abs_data[225] });
                abs_data[227].Add(new { idx = i, val = abs_COP_New_C_total.ToString("0.0") });//냉방 COP 평균  
                data.Add(new { cname = "abs_cop_new_c_total", data = abs_data[227] });
                d = abs_Point_C_total;
                if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                abs_data[229].Add(new { idx = i, val = sp });//냉방 성능수준 평균  
                data.Add(new { cname = "abs_point_c_total", data = abs_data[229] });

                double abs_Qmax_h = 0; double abs_Qmax_c = 0; double abs_ZoneArea = 0;
                for (int a = 0; a < abs_Zones_split.Count; a++)
                {
                    string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호='" + abs_Zones_split[a].ToString() + "' And 난방_냉방='난방' and 비이용일_이용일='이용일' and 월='1월'");
                    if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                    {
                        abs_Qmax_h += Program.UTIL.ToDoubleOrZero(ZoneValue[0][0]);
                    }
                    ZoneValue = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호='" + abs_Zones_split[a].ToString() + "' And 난방_냉방='냉방' and 비이용일_이용일='이용일' and 월='8월'");
                    if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                    {
                        abs_Qmax_c += Program.UTIL.ToDoubleOrZero(ZoneValue[0][0]);
                    }
                    ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "존번호='" + abs_Zones_split[a].ToString() + "'");
                    if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                    {
                        abs_ZoneArea += Program.UTIL.ToDoubleOrZero(ZoneValue[0][0]);
                    }
                }
                abs_data[230].Add(new { idx = i, val = (abs_Qmax_h / 1000).ToString("0.0") });//난방부하 
                data.Add(new { cname = "abs_qmax_h", data = abs_data[230] });
                abs_data[231].Add(new { idx = i, val = (abs_Qmax_c / 1000).ToString("0.0") });//냉방부하 
                data.Add(new { cname = "abs_qmax_c", data = abs_data[231] });
                abs_data[232].Add(new { idx = i, val = abs_ZoneArea.ToString("0.0") });//존면적
                data.Add(new { cname = "abs_zonearea", data = abs_data[232] });
                abs_data[233].Add(new { idx = i, val = abs_Zones_split.Count.ToString() });//존개수 
                data.Add(new { cname = "abs_zonecount", data = abs_data[233] });

                #endregion
                items.Add("Element_GasHP2.htm");
                s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
                System.Text.Json.JsonSerializer.Serialize(ghp_data[10].ToArray());

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
            List<object>[] ghp_data = new List<object>[700];
            List<object>[] abs_data = new List<object>[700];
            double d;
            string sp;
            int i = -1, n;
            while (++i < 700)
            {
                ghp_data[i] = new List<object>();
                abs_data[i] = new List<object>();
            }
            string charts = "";
            i = -1;
            while (++i < 번호.Length)
            {
                if (res.Length > 0)
                {

                    #region 냉난방 절약 : 모든 요소기술 적용 절감량 중                                
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


                    #region 냉난방 가스히트펌프   
                    string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.외기히트펌프번호,a.명칭,b.존,a.난방정격용량,a.난방정격COP,a.냉방정격용량,a.냉방정격COP,b.번호,b.외기히트펌프대수 From User_AirHP as a Inner Join HeatingSystem_Form as b ON a.번호 = b.외기히트펌프번호 Where a.난방냉방='냉난방' And NOT a.연료='전기'");
                    string[] ghp_Name = new string[18]; string[] ghp_Zone_text = new string[18];
                    double[] ghp_Power_H = new double[18]; double[] ghp_COP_Old_H = new double[18]; double[] ghp_COP_New_H = new double[18]; double[] ghp_Saving_H = new double[18]; double[] ghp_Point_H = new double[18]; double[] ghp_COP_Rule_H = new double[18];
                    double[] ghp_Power_C = new double[18]; double[] ghp_COP_Old_C = new double[18]; double[] ghp_COP_New_C = new double[18]; double[] ghp_Saving_C = new double[18]; double[] ghp_Point_C = new double[18]; double[] ghp_COP_Rule_C = new double[18];

                    double[] ghp_elec_H = new double[18]; double[] ghp_elec_C = new double[18];
                    double[] ghp_gas_H = new double[18]; double[] ghp_gas_C = new double[18];
                    ArrayList ghp_Zones_split = new ArrayList();

                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            for (int aa = 0; aa < HeatingGroup.Count; aa++)
                            {
                                Heating_New_Old hh = (Heating_New_Old)HeatingGroup[aa];
                                if (Value[a][7] == hh.Num_New())
                                {
                                    ghp_Saving_H[a] = heating_element_saving[aa];
                                    ghp_elec_H[a] = heating_element_saving_elec[aa];
                                    ghp_gas_H[a] = heating_element_saving_gas[aa];
                                    for (int aaa = 0; aaa < hh.Num_Old().Count; aaa++)
                                    {
                                        string[][] OldSystem = Program.DB.querySQL(res[0][0], "Select a.난방정격COP From User_AirHP as a Inner Join HeatingSystem_Form as b ON a.번호 = b.외기히트펌프번호 Where b.번호 ='" + hh.Num_Old()[aaa] + "'");
                                        if (OldSystem.Length > 0)
                                        {
                                            if (Program.UTIL.ToDoubleOrZero(OldSystem[0][0]) == Program.UTIL.ToDoubleOrZero(Value[a][4]))
                                            { ghp_COP_Old_H[a] = Program.UTIL.ToDoubleOrZero(Value[a][4]); break; }
                                            else if (Program.UTIL.ToDoubleOrZero(OldSystem[0][0]) < ghp_COP_Old_H[a]) { ghp_COP_Old_H[a] = Program.UTIL.ToDoubleOrZero(OldSystem[0][0]); }
                                            else if (ghp_COP_Old_H[a] == 0) { ghp_COP_Old_H[a] = Program.UTIL.ToDoubleOrZero(OldSystem[0][0]); }
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
                                                Q_sol_a += Program.UTIL.ToDoubleOrZero(Solar[0][0]);
                                            }
                                        }
                                        ghp_Saving_H[a] = ghp_Saving_H[a] - Q_sol_a;
                                        if (ghp_gas_H[a] > ghp_elec_H[a]) { ghp_gas_H[a] = ghp_gas_H[a] - Q_sol_a; }
                                        else { ghp_elec_H[a] = ghp_elec_H[a] - Q_sol_a; }
                                    }
                                }
                            }
                            string[][] coolingvalue = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "번호", "냉방유닛='" + Value[a][0] + "'");
                            if (coolingvalue.Length > 0)
                            {
                                for (int aa = 0; aa < CoolingGroup.Count; aa++)
                                {
                                    Cooling_New_Old cc = (Cooling_New_Old)CoolingGroup[aa];
                                    if (coolingvalue[0][0] == cc.Num_New())
                                    {
                                        ghp_Saving_C[a] = cooling_element_saving[aa];
                                        ghp_elec_C[a] = cooling_element_saving_elec[aa];
                                        ghp_gas_C[a] = cooling_element_saving_gas[aa];
                                        for (int aaa = 0; aaa < cc.Num_Old().Count; aaa++)
                                        {
                                            string[][] OldSystem = Program.DB.querySQL(res[0][0], "Select a.냉방정격COP From User_AirHP as a Inner Join CoolingSystem_Form as b ON a.번호 = b.냉방유닛 Where b.번호 ='" + cc.Num_Old()[aaa] + "'");
                                            if (OldSystem.Length > 0)
                                            {
                                                if (Program.UTIL.ToDoubleOrZero(OldSystem[0][0]) == Program.UTIL.ToDoubleOrZero(Value[a][6]))
                                                { ghp_COP_Old_C[a] = Program.UTIL.ToDoubleOrZero(Value[a][6]); break; }
                                                else if (Program.UTIL.ToDoubleOrZero(OldSystem[0][0]) < ghp_COP_Old_C[a]) { ghp_COP_Old_C[a] = Program.UTIL.ToDoubleOrZero(OldSystem[0][0]); }
                                                else if (ghp_COP_Old_C[a] == 0) { ghp_COP_Old_C[a] = Program.UTIL.ToDoubleOrZero(OldSystem[0][0]); }
                                            }
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
                            ghp_Name[a] = Value[a][1];

                            ArrayList splitzone = new ArrayList();
                            splitzone = Split_(Value[a][2]);
                            if (splitzone.Count > 1) { ghp_Zone_text[a] = splitzone[0].ToString() + " 외 " + (splitzone.Count - 1).ToString() + "개"; }
                            else { ghp_Zone_text[a] = splitzone[0].ToString(); }

                            for (int aa = 0; aa < splitzone.Count; aa++)
                            {
                                if (ghp_Zones_split.Contains(splitzone[aa]))
                                { }
                                else { ghp_Zones_split.Add(splitzone[aa]); }
                            }


                            ghp_Power_H[a] = Program.UTIL.ToDoubleOrZero(Value[a][3]) * Program.UTIL.ToDoubleOrZero(Value[a][8]);
                            ghp_COP_New_H[a] = Program.UTIL.ToDoubleOrZero(Value[a][4]);

                            ghp_Power_C[a] = Program.UTIL.ToDoubleOrZero(Value[a][5]) * Program.UTIL.ToDoubleOrZero(Value[a][8]);
                            ghp_COP_New_C[a] = Program.UTIL.ToDoubleOrZero(Value[a][6]);

                            double Rule = 0; string[][] kkk = Program.DB.getValue(DB.type.BaseDB_Cooling, "AirCooler", "EER", "압축기= '스크롤' And 냉매='R134a' And 냉수출구온도 = '14' And 평균증발기온도='8'");
                            if (kkk.Length > 0)
                            {
                                Rule = Program.UTIL.ToDoubleOrZero(kkk[0][0]);
                            }
                            ghp_COP_Rule_H[a] = 3.8; //DIN V 18599-5 table C.1
                            ghp_COP_Rule_C[a] = Rule; //DIN V 18599-7 table 27
                            ghp_Point_H[a] = Math.Min(100, ghp_COP_New_H[a] / ghp_COP_Rule_H[a] * 100);
                            ghp_Point_C[a] = Math.Min(100, ghp_COP_New_H[a] / ghp_COP_Rule_C[a] * 100);
                        }
                    }

                    for (int a = 0; a < 18; a++)
                    {
                        if (ghp_Saving_H[a] < 0) { ghp_Saving_H[a] = 0; }
                        if (ghp_elec_H[a] < 0) { ghp_elec_H[a] = 0; }
                        if (ghp_gas_H[a] < 0) { ghp_gas_H[a] = 0; }

                        if (ghp_Saving_C[a] < 0) { ghp_Saving_C[a] = 0; }
                        if (ghp_elec_C[a] < 0) { ghp_elec_C[a] = 0; }
                        if (ghp_gas_C[a] < 0) { ghp_gas_C[a] = 0; }
                    }
                    double ghp_total_saving = 0; double ghp_total_elec = 0; double ghp_total_gas = 0;
                    double ghp_COP_New_H_total = 0; double ghp_COP_Old_H_total = 0; double ghp_Point_H_total = 0;
                    double ghp_COP_New_C_total = 0; double ghp_COP_Old_C_total = 0; double ghp_Point_C_total = 0;
                    double ghp_COP_Rule_H_total = 0;
                    double ghp_COP_Rule_C_total = 0;
                    for (int a = 0; a < 18; a++)
                    {
                        ghp_data[a].Add(new { idx = i, val = ghp_Name[a] });//명칭
                        data.Add(new { cname = "ghp_name" + a, data = ghp_data[a] });
                        if (ghp_Name[a] != null & ghp_Name[a] != "")
                        {
                            ghp_data[18 + a].Add(new { idx = i, val = ghp_Zone_text[a] });//존
                            data.Add(new { cname = "ghp_zone" + a, data = ghp_data[18 + a] });

                            ghp_data[36 + a].Add(new { idx = i, val = ghp_Power_H[a].ToString("0.0") });//난방용량
                            data.Add(new { cname = "ghp_power_h" + a, data = ghp_data[36 + a] });

                            ghp_data[54 + a].Add(new { idx = i, val = ghp_COP_New_H[a].ToString("0.0") });//난방COP
                            data.Add(new { cname = "ghp_cop_new_h" + a, data = ghp_data[54 + a] });

                            ghp_data[72 + a].Add(new { idx = i, val = ghp_Power_C[a].ToString("0.0") });//냉방용량
                            data.Add(new { cname = "ghp_power_c" + a, data = ghp_data[72 + a] });

                            ghp_data[90 + a].Add(new { idx = i, val = ghp_COP_New_C[a].ToString("0.0") });//냉방COP
                            data.Add(new { cname = "ghp_cop_new_c" + a, data = ghp_data[90 + a] });

                            if (ghp_COP_Old_H[a] != 0)
                            { ghp_data[108 + a].Add(new { idx = i, val = ghp_COP_Old_H[a].ToString("0.0") }); }//난방 기존 COP
                            else { ghp_data[108 + a].Add(new { idx = i, val = "Not GHP" }); }
                            data.Add(new { cname = "ghp_cop_old_h" + a, data = ghp_data[108 + a] });

                            if (ghp_COP_Old_C[a] != 0)
                            { ghp_data[126 + a].Add(new { idx = i, val = ghp_COP_Old_C[a].ToString("0.0") }); }//냉방 기존 COP
                            else { ghp_data[126 + a].Add(new { idx = i, val = "Not GHP" }); }
                            data.Add(new { cname = "ghp_cop_old_c" + a, data = ghp_data[126 + a] });

                            ghp_data[144 + a].Add(new { idx = i, val = (ghp_Saving_H[a] / Total_Energy_pre * 100).ToString("0.0") + " %" });//난방 절감률
                            data.Add(new { cname = "ghp_saving_h" + a, data = ghp_data[144 + a] });

                            ghp_data[162 + a].Add(new { idx = i, val = (ghp_Saving_C[a] / Total_Energy_pre * 100).ToString("0.0") + " %" });//냉방 절감률
                            data.Add(new { cname = "ghp_saving_c" + a, data = ghp_data[162 + a] });

                            d = ghp_Point_H[a];
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            ghp_data[180 + a].Add(new { idx = i, val = sp });//난방 성능점수
                            data.Add(new { cname = "ghp_point_h" + a, data = ghp_data[180 + a] });

                            d = ghp_Point_C[a];
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            ghp_data[198 + a].Add(new { idx = i, val = sp });//냉방 성능점수
                            data.Add(new { cname = "ghp_point_c" + a, data = ghp_data[198 + a] });
                        }

                        //가중평균 
                        ghp_COP_New_H_total += ghp_COP_New_H[a] * ghp_Power_H[a];
                        ghp_COP_Old_H_total += ghp_COP_Old_H[a] * ghp_Power_H[a];
                        ghp_Point_H_total += ghp_Point_H[a] * ghp_Power_H[a];
                        ghp_COP_New_C_total += ghp_COP_New_C[a] * ghp_Power_C[a];
                        ghp_COP_Old_C_total += ghp_COP_Old_C[a] * ghp_Power_C[a];
                        ghp_Point_C_total += ghp_Point_C[a] * ghp_Power_C[a];
                        ghp_COP_Rule_H_total += ghp_COP_Rule_H[a] * ghp_Power_H[a];
                        ghp_COP_Rule_C_total += ghp_COP_Rule_C[a] * ghp_Power_C[a];
                    }
                    if (ghp_Power_H.Sum() > 0 && ghp_Power_C.Sum() > 0)
                    {
                        ghp_COP_New_H_total = ghp_COP_New_H_total / ghp_Power_H.Sum();
                        ghp_COP_Old_H_total = ghp_COP_Old_H_total / ghp_Power_H.Sum();
                        ghp_Point_H_total = Math.Min(100, ghp_Point_H_total / ghp_Power_H.Sum());
                        ghp_COP_New_C_total = ghp_COP_New_C_total / ghp_Power_C.Sum();
                        ghp_COP_Old_C_total = ghp_COP_Old_C_total / ghp_Power_C.Sum();
                        ghp_Point_C_total = Math.Min(100, ghp_Point_C_total / ghp_Power_C.Sum());
                        ghp_COP_Rule_H_total = ghp_COP_Rule_H_total / ghp_Power_H.Sum();
                        ghp_COP_Rule_C_total = ghp_COP_Rule_C_total / ghp_Power_C.Sum();
                    }

                    for (int a = 0; a < 18; a++)
                    {
                        ghp_total_saving += ghp_Saving_H[a] + ghp_Saving_C[a];
                        ghp_total_elec += ghp_elec_H[a] + ghp_elec_C[a];
                        ghp_total_gas += ghp_gas_H[a] + ghp_gas_C[a];
                    }

                    double v3 = double.IsNaN(ghp_total_saving) ? 0 : ghp_total_saving;
                    ghp_data[216].Add(new { idx = i, val = v3.ToString("#,##0") });//절감량 전체 
                    data.Add(new { cname = "ghp_saving_total", data = ghp_data[216] });

                    v3 = double.IsNaN(ghp_total_saving / Total_Energy_pre * 100) ? 0 : ghp_total_saving / Total_Energy_pre * 100;
                    ghp_data[217].Add(new { idx = i, val = (v3).ToString("0.0") + " %" });//절감률 전체 
                    data.Add(new { cname = "ghp_saving_percent", data = ghp_data[217] });

                    v3 = double.IsNaN(ghp_total_elec * 0.4747 / 1000000 * 1000 + ghp_total_gas / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000) ? 0 : ghp_total_elec * 0.4747 / 1000000 * 1000 + ghp_total_gas / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    ghp_data[218].Add(new { idx = i, val = (v3).ToString("0.0") });//tco2
                    data.Add(new { cname = "ghp_tco2", data = ghp_data[218] });

                    v3 = double.IsNaN(ghp_total_elec * 0.00023 + ghp_total_gas / 38.9 / 0.277778 * 0.00103) ? 0 : ghp_total_elec * 0.00023 + ghp_total_gas / 38.9 / 0.277778 * 0.00103;
                    ghp_data[219].Add(new { idx = i, val = (v3).ToString("0.0") });//절감량 전체 
                    data.Add(new { cname = "ghp_toe", data = ghp_data[219] });

                    d = (ghp_total_saving / Total_Energy_pre * 100);
                    charts += "{donut:" + d + "},";

                    //합산 계 
                    ghp_data[220].Add(new { idx = i, val = ghp_Power_H.Sum().ToString("0.0") });//난방 용량 합계  
                    data.Add(new { cname = "ghp_power_h_total", data = ghp_data[220] });
                    ghp_data[221].Add(new { idx = i, val = ghp_COP_Old_H_total.ToString("0.0") });//난방 기존 COP 평균  
                    data.Add(new { cname = "ghp_cop_old_h_total", data = ghp_data[221] });
                    ghp_data[222].Add(new { idx = i, val = ghp_COP_New_H_total.ToString("0.0") });//난방 COP 평균  
                    data.Add(new { cname = "ghp_cop_new_h_total", data = ghp_data[222] });
                    ghp_data[223].Add(new { idx = i, val = (ghp_Saving_H.Sum() / Total_Energy_pre * 100).ToString("0.0") + " %" });//난방 절감량 합계  
                    data.Add(new { cname = "ghp_saving_h_total", data = ghp_data[223] });
                    d = ghp_Point_H_total;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    ghp_data[224].Add(new { idx = i, val = sp });//난방 성능수준 평균  
                    data.Add(new { cname = "ghp_point_h_total", data = ghp_data[224] });

                    ghp_data[225].Add(new { idx = i, val = ghp_Power_C.Sum().ToString("0.0") });//냉방 용량 합계  
                    data.Add(new { cname = "ghp_power_c_total", data = ghp_data[225] });
                    ghp_data[226].Add(new { idx = i, val = ghp_COP_Old_C_total.ToString("0.0") });//냉방 기존 COP 평균  
                    data.Add(new { cname = "ghp_cop_old_c_total", data = ghp_data[226] });
                    ghp_data[227].Add(new { idx = i, val = ghp_COP_New_C_total.ToString("0.0") });//냉방 COP 평균  
                    data.Add(new { cname = "ghp_cop_new_c_total", data = ghp_data[227] });
                    ghp_data[228].Add(new { idx = i, val = (ghp_Saving_C.Sum() / Total_Energy_pre * 100).ToString("0.0") + " %" });//냉방 절감량 합계  
                    data.Add(new { cname = "ghp_saving_c_total", data = ghp_data[228] });
                    d = ghp_Point_C_total;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    ghp_data[229].Add(new { idx = i, val = sp });//냉방 성능수준 평균  
                    data.Add(new { cname = "ghp_point_c_total", data = ghp_data[229] });

                    double ghp_Qmax_h = 0; double ghp_Qmax_c = 0; double ghp_ZoneArea = 0;
                    for (int a = 0; a < ghp_Zones_split.Count; a++)
                    {
                        string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호='" + ghp_Zones_split[a].ToString() + "' And 난방_냉방='난방' and 비이용일_이용일='이용일' and 월='1월'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                        {
                            ghp_Qmax_h += Program.UTIL.ToDoubleOrZero(ZoneValue[0][0]);
                        }
                        ZoneValue = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호='" + ghp_Zones_split[a].ToString() + "' And 난방_냉방='냉방' and 비이용일_이용일='이용일' and 월='8월'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                        {
                            ghp_Qmax_c += Program.UTIL.ToDoubleOrZero(ZoneValue[0][0]);
                        }
                        ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "존번호='" + ghp_Zones_split[a].ToString() + "'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                        {
                            ghp_ZoneArea += Program.UTIL.ToDoubleOrZero(ZoneValue[0][0]);
                        }
                    }
                    ghp_data[230].Add(new { idx = i, val = (ghp_Qmax_h / 1000).ToString("0.0") });//난방부하 
                    data.Add(new { cname = "ghp_qmax_h", data = ghp_data[230] });
                    ghp_data[231].Add(new { idx = i, val = (ghp_Qmax_c / 1000).ToString("0.0") });//냉방부하 
                    data.Add(new { cname = "ghp_qmax_c", data = ghp_data[231] });
                    ghp_data[232].Add(new { idx = i, val = ghp_ZoneArea.ToString("0.0") });//존면적
                    data.Add(new { cname = "ghp_zonearea", data = ghp_data[232] });
                    ghp_data[233].Add(new { idx = i, val = ghp_Zones_split.Count.ToString() });//존개수 
                    data.Add(new { cname = "ghp_zonecount", data = ghp_data[233] });
                    if (ghp_COP_Rule_H_total > 0)
                    {
                        ghp_data[234].Add(new { idx = i, val = "* DIN V 18599-5 표준 COP : " + ghp_COP_Rule_H_total.ToString("0.0") + " 기준" });//난방 법규 평균  
                        data.Add(new { cname = "ghp_cop_rule_h", data = ghp_data[234] });
                    }

                    if (ghp_COP_Rule_C_total > 0)
                    {
                        ghp_data[235].Add(new { idx = i, val = "* DIN V 18599-7 표준 EER : " + ghp_COP_Rule_C_total.ToString("0.0") + " 기준" });//냉방 법규 평균  
                        data.Add(new { cname = "ghp_cop_rule_c", data = ghp_data[235] });
                    }
                    #endregion


                    #region 흡수식냉온수기   
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.흡수식온수기번호,a.명칭,b.존,a.난방용량,a.난방성능,a.냉방용량,a.냉방성능,b.번호,b.흡수식온수기대수 From User_ABS as a Inner Join HeatingSystem_Form as b ON a.번호 = b.흡수식온수기번호 Where a.난방냉방='냉난방'");
                    string[] abs_Name = new string[18]; string[] abs_Zone_text = new string[18];
                    double[] abs_Power_H = new double[18]; double[] abs_COP_Old_H = new double[18]; double[] abs_COP_New_H = new double[18]; double[] abs_Saving_H = new double[18]; double[] abs_Point_H = new double[18]; double[] abs_COP_Rule_H = new double[18];
                    double[] abs_Power_C = new double[18]; double[] abs_COP_Old_C = new double[18]; double[] abs_COP_New_C = new double[18]; double[] abs_Saving_C = new double[18]; double[] abs_Point_C = new double[18]; double[] abs_COP_Rule_C = new double[18];

                    double[] abs_elec_H = new double[18]; double[] abs_elec_C = new double[18];
                    double[] abs_gas_H = new double[18]; double[] abs_gas_C = new double[18];
                    ArrayList abs_Zones_split = new ArrayList();

                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            for (int aa = 0; aa < HeatingGroup.Count; aa++)
                            {
                                Heating_New_Old hh = (Heating_New_Old)HeatingGroup[aa];
                                if (Value[a][7] == hh.Num_New())
                                {
                                    abs_Saving_H[a] = heating_element_saving[aa];
                                    abs_elec_H[a] = heating_element_saving_elec[aa];
                                    abs_gas_H[a] = heating_element_saving_gas[aa];
                                    for (int aaa = 0; aaa < hh.Num_Old().Count; aaa++)
                                    {
                                        string[][] OldSystem = Program.DB.querySQL(res[0][0], "Select a.난방정격COP From User_AirHP as a Inner Join HeatingSystem_Form as b ON a.번호 = b.흡수식온수기번호 Where b.번호 ='" + hh.Num_Old()[aaa] + "'");
                                        if (OldSystem.Length > 0)
                                        {
                                            if (Program.UTIL.ToDoubleOrZero(OldSystem[0][0]) == Program.UTIL.ToDoubleOrZero(Value[a][4]))
                                            { abs_COP_Old_H[a] = Program.UTIL.ToDoubleOrZero(Value[a][4]); break; }
                                            else if (Program.UTIL.ToDoubleOrZero(OldSystem[0][0]) < abs_COP_Old_H[a]) { abs_COP_Old_H[a] = Program.UTIL.ToDoubleOrZero(OldSystem[0][0]); }
                                            else if (abs_COP_Old_H[a] == 0) { abs_COP_Old_H[a] = Program.UTIL.ToDoubleOrZero(OldSystem[0][0]); }
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
                                                Q_sol_a += Program.UTIL.ToDoubleOrZero(Solar[0][0]);
                                            }
                                        }
                                        abs_Saving_H[a] = abs_Saving_H[a] - Q_sol_a;
                                        if (abs_gas_H[a] > abs_elec_H[a]) { abs_gas_H[a] = abs_gas_H[a] - Q_sol_a; }
                                        else { abs_elec_H[a] = abs_elec_H[a] - Q_sol_a; }
                                    }
                                }
                            }
                            string[][] coolingvalue = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "번호", "냉방유닛='" + Value[a][0] + "'");
                            if (coolingvalue.Length > 0)
                            {
                                for (int aa = 0; aa < CoolingGroup.Count; aa++)
                                {
                                    Cooling_New_Old cc = (Cooling_New_Old)CoolingGroup[aa];
                                    if (coolingvalue[0][0] == cc.Num_New())
                                    {
                                        abs_Saving_C[a] = cooling_element_saving[aa];
                                        abs_elec_C[a] = cooling_element_saving_elec[aa];
                                        abs_gas_C[a] = cooling_element_saving_gas[aa];
                                        for (int aaa = 0; aaa < cc.Num_Old().Count; aaa++)
                                        {
                                            string[][] OldSystem = Program.DB.querySQL(res[0][0], "Select a.냉방정격COP From User_AirHP as a Inner Join CoolingSystem_Form as b ON a.번호 = b.냉방유닛 Where b.번호 ='" + cc.Num_Old()[aaa] + "'");
                                            if (OldSystem.Length > 0)
                                            {
                                                if (Program.UTIL.ToDoubleOrZero(OldSystem[0][0]) == Program.UTIL.ToDoubleOrZero(Value[a][6]))
                                                { abs_COP_Old_C[a] = Program.UTIL.ToDoubleOrZero(Value[a][6]); break; }
                                                else if (Program.UTIL.ToDoubleOrZero(OldSystem[0][0]) < abs_COP_Old_C[a]) { abs_COP_Old_C[a] = Program.UTIL.ToDoubleOrZero(OldSystem[0][0]); }
                                                else if (abs_COP_Old_C[a] == 0) { abs_COP_Old_C[a] = Program.UTIL.ToDoubleOrZero(OldSystem[0][0]); }
                                            }
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
                            abs_Name[a] = Value[a][1];

                            ArrayList splitzone = new ArrayList();
                            splitzone = Split_(Value[a][2]);
                            if (splitzone.Count > 1) { abs_Zone_text[a] = splitzone[0].ToString() + " 외 " + (splitzone.Count - 1).ToString() + "개"; }
                            else { abs_Zone_text[a] = splitzone[0].ToString(); }

                            for (int aa = 0; aa < splitzone.Count; aa++)
                            {
                                if (abs_Zones_split.Contains(splitzone[aa]))
                                { }
                                else { abs_Zones_split.Add(splitzone[aa]); }
                            }


                            abs_Power_H[a] = Program.UTIL.ToDoubleOrZero(Value[a][3]) * Program.UTIL.ToDoubleOrZero(Value[a][8]);
                            abs_COP_New_H[a] = Program.UTIL.ToDoubleOrZero(Value[a][4]);

                            abs_Power_C[a] = Program.UTIL.ToDoubleOrZero(Value[a][5]) * Program.UTIL.ToDoubleOrZero(Value[a][8]);
                            abs_COP_New_C[a] = Program.UTIL.ToDoubleOrZero(Value[a][6]);

                            abs_COP_Rule_H[a] = 1.2; //EPI 1점 
                            abs_COP_Rule_C[a] = 1.2;

                            abs_Point_H[a] = Math.Min(100, abs_COP_New_H[a] / abs_COP_Rule_H[a] * 100);
                            abs_Point_C[a] = Math.Min(100, abs_COP_New_H[a] / abs_COP_Rule_C[a] * 100);
                        }
                    }
                    for (int a = 0; a < 18; a++)
                    {
                        if (abs_Saving_H[a] < 0) { abs_Saving_H[a] = 0; }
                        if (abs_elec_H[a] < 0) { abs_elec_H[a] = 0; }
                        if (abs_gas_H[a] < 0) { abs_gas_H[a] = 0; }

                        if (abs_Saving_C[a] < 0) { abs_Saving_C[a] = 0; }
                        if (abs_elec_C[a] < 0) { abs_elec_C[a] = 0; }
                        if (abs_gas_C[a] < 0) { abs_gas_C[a] = 0; }
                    }

                    double abs_total_saving = 0; double abs_total_elec = 0; double abs_total_gas = 0;
                    double abs_COP_New_H_total = 0; double abs_COP_Old_H_total = 0; double abs_Point_H_total = 0;
                    double abs_COP_New_C_total = 0; double abs_COP_Old_C_total = 0; double abs_Point_C_total = 0;
                    for (int a = 0; a < 18; a++)
                    {
                        abs_data[a].Add(new { idx = i, val = abs_Name[a] });//명칭
                        data.Add(new { cname = "abs_name" + a, data = abs_data[a] });
                        if (abs_Name[a] != null & abs_Name[a] != "")
                        {
                            abs_data[18 + a].Add(new { idx = i, val = abs_Zone_text[a] });//존
                            data.Add(new { cname = "abs_zone" + a, data = abs_data[18 + a] });

                            abs_data[36 + a].Add(new { idx = i, val = abs_Power_H[a].ToString("0.0") });//난방용량
                            data.Add(new { cname = "abs_power_h" + a, data = abs_data[36 + a] });

                            abs_data[54 + a].Add(new { idx = i, val = abs_COP_New_H[a].ToString("0.0") });//난방COP
                            data.Add(new { cname = "abs_cop_new_h" + a, data = abs_data[54 + a] });

                            abs_data[72 + a].Add(new { idx = i, val = abs_Power_C[a].ToString("0.0") });//냉방용량
                            data.Add(new { cname = "abs_power_c" + a, data = abs_data[72 + a] });

                            abs_data[90 + a].Add(new { idx = i, val = abs_COP_New_C[a].ToString("0.0") });//냉방COP
                            data.Add(new { cname = "abs_cop_new_c" + a, data = abs_data[90 + a] });

                            if (abs_COP_Old_H[a] != 0)
                            { abs_data[108 + a].Add(new { idx = i, val = abs_COP_Old_H[a].ToString("0.0") }); }//난방 기존 COP
                            else { abs_data[108 + a].Add(new { idx = i, val = "Not ABS" }); }
                            data.Add(new { cname = "abs_cop_old_h" + a, data = abs_data[108 + a] });

                            if (abs_COP_Old_C[a] != 0)
                            { abs_data[126 + a].Add(new { idx = i, val = abs_COP_Old_C[a].ToString("0.0") }); }//냉방 기존 COP
                            else { abs_data[126 + a].Add(new { idx = i, val = "Not ABS" }); }
                            data.Add(new { cname = "abs_cop_old_c" + a, data = abs_data[126 + a] });

                            abs_data[144 + a].Add(new { idx = i, val = (abs_Saving_H[a] / Total_Energy_pre * 100).ToString("0.0") + " %" });//난방 절감률
                            data.Add(new { cname = "abs_saving_h" + a, data = abs_data[144 + a] });

                            abs_data[162 + a].Add(new { idx = i, val = (abs_Saving_C[a] / Total_Energy_pre * 100).ToString("0.0") + " %" });//냉방 절감률
                            data.Add(new { cname = "abs_saving_c" + a, data = abs_data[162 + a] });

                            d = abs_Point_H[a];
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            abs_data[180 + a].Add(new { idx = i, val = sp });//난방 성능점수
                            data.Add(new { cname = "abs_point_h" + a, data = abs_data[180 + a] });

                            d = abs_Point_C[a];
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            abs_data[198 + a].Add(new { idx = i, val = sp });//냉방 성능점수
                            data.Add(new { cname = "abs_point_c" + a, data = abs_data[198 + a] });
                        }

                        //가중평균 
                        abs_COP_New_H_total += abs_COP_New_H[a] * abs_Power_H[a];
                        abs_COP_Old_H_total += abs_COP_Old_H[a] * abs_Power_H[a];
                        abs_Point_H_total += abs_Point_H[a] * abs_Power_H[a];
                        abs_COP_New_C_total += abs_COP_New_C[a] * abs_Power_C[a];
                        abs_COP_Old_C_total += abs_COP_Old_C[a] * abs_Power_C[a];
                        abs_Point_C_total += abs_Point_C[a] * abs_Power_C[a];
                    }
                    if (abs_Power_H.Sum() > 0 && abs_Power_C.Sum() > 0)
                    {
                        abs_COP_New_H_total = abs_COP_New_H_total / abs_Power_H.Sum();
                        abs_COP_Old_H_total = abs_COP_Old_H_total / abs_Power_H.Sum();
                        abs_Point_H_total = Math.Min(100, abs_Point_H_total / abs_Power_H.Sum());
                        abs_COP_New_C_total = abs_COP_New_C_total / abs_Power_C.Sum();
                        abs_COP_Old_C_total = abs_COP_Old_C_total / abs_Power_C.Sum();
                        abs_Point_C_total = Math.Min(100, abs_Point_C_total / abs_Power_C.Sum());
                    }

                    for (int a = 0; a < 18; a++)
                    {
                        abs_total_saving += abs_Saving_H[a] + abs_Saving_C[a];
                        abs_total_elec += abs_elec_H[a] + abs_elec_C[a];
                        abs_total_gas += abs_gas_H[a] + abs_gas_C[a];
                    }

                    double v4 = double.IsNaN(abs_total_saving ) ? 0 : abs_total_saving;
                    abs_data[216].Add(new { idx = i, val = v4.ToString("#,##0") });//절감량 전체 
                    data.Add(new { cname = "abs_saving_total", data = abs_data[216] });

                    v4 = double.IsNaN(abs_total_saving / Total_Energy_pre * 100) ? 0 : abs_total_saving / Total_Energy_pre * 100;
                    abs_data[217].Add(new { idx = i, val = (v4).ToString("0.0") + " %" });//절감률 전체 
                    data.Add(new { cname = "abs_saving_percent", data = abs_data[217] });

                    v4 = double.IsNaN(abs_total_elec * 0.4747 / 1000000 * 1000 + abs_total_gas / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000) ? 0 : abs_total_elec * 0.4747 / 1000000 * 1000 + abs_total_gas / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    abs_data[218].Add(new { idx = i, val = (v4).ToString("0.0") });//tco2
                    data.Add(new { cname = "abs_tco2", data = abs_data[218] });

                    v4 = double.IsNaN(abs_total_elec * 0.00023 + abs_total_gas / 38.9 / 0.277778 * 0.00103) ? 0 : abs_total_elec * 0.00023 + abs_total_gas / 38.9 / 0.277778 * 0.00103;
                    abs_data[219].Add(new { idx = i, val = (v4).ToString("0.0") });//절감량 전체 
                    data.Add(new { cname = "abs_toe", data = abs_data[219] });

                    d = (abs_total_saving / Total_Energy_pre * 100);
                    charts += "{donut:" + d + "},";

                    //합산 계 
                    abs_data[220].Add(new { idx = i, val = abs_Power_H.Sum().ToString("0.0") });//난방 용량 합계  
                    data.Add(new { cname = "abs_power_h_total", data = abs_data[220] });
                    abs_data[221].Add(new { idx = i, val = abs_COP_Old_H_total.ToString("0.0") });//난방 기존 COP 평균  
                    data.Add(new { cname = "abs_cop_old_h_total", data = abs_data[221] });
                    abs_data[222].Add(new { idx = i, val = abs_COP_New_H_total.ToString("0.0") });//난방 COP 평균  
                    data.Add(new { cname = "abs_cop_new_h_total", data = abs_data[222] });
                    abs_data[223].Add(new { idx = i, val = (abs_Saving_H.Sum() / Total_Energy_pre * 100).ToString("0.0") + " %" });//난방 절감량 합계  
                    data.Add(new { cname = "abs_saving_h_total", data = abs_data[223] });
                    d = abs_Point_H_total;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    abs_data[224].Add(new { idx = i, val = sp });//난방 성능수준 평균  
                    data.Add(new { cname = "abs_point_h_total", data = abs_data[224] });

                    abs_data[225].Add(new { idx = i, val = abs_Power_C.Sum().ToString("0.0") });//냉방 용량 합계  
                    data.Add(new { cname = "abs_power_c_total", data = abs_data[225] });
                    abs_data[226].Add(new { idx = i, val = abs_COP_Old_C_total.ToString("0.0") });//냉방 기존 COP 평균  
                    data.Add(new { cname = "abs_cop_old_c_total", data = abs_data[226] });
                    abs_data[227].Add(new { idx = i, val = abs_COP_New_C_total.ToString("0.0") });//냉방 COP 평균  
                    data.Add(new { cname = "abs_cop_new_c_total", data = abs_data[227] });
                    abs_data[228].Add(new { idx = i, val = (abs_Saving_C.Sum() / Total_Energy_pre * 100).ToString("0.0") + " %" });//냉방 절감량 합계  
                    data.Add(new { cname = "abs_saving_c_total", data = abs_data[228] });
                    d = abs_Point_C_total;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    abs_data[229].Add(new { idx = i, val = sp });//냉방 성능수준 평균  
                    data.Add(new { cname = "abs_point_c_total", data = abs_data[229] });

                    double abs_Qmax_h = 0; double abs_Qmax_c = 0; double abs_ZoneArea = 0;
                    for (int a = 0; a < abs_Zones_split.Count; a++)
                    {
                        string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호='" + abs_Zones_split[a].ToString() + "' And 난방_냉방='난방' and 비이용일_이용일='이용일' and 월='1월'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                        {
                            abs_Qmax_h += Program.UTIL.ToDoubleOrZero(ZoneValue[0][0]);
                        }
                        ZoneValue = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호='" + abs_Zones_split[a].ToString() + "' And 난방_냉방='냉방' and 비이용일_이용일='이용일' and 월='8월'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                        {
                            abs_Qmax_c += Program.UTIL.ToDoubleOrZero(ZoneValue[0][0]);
                        }
                        ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "존번호='" + abs_Zones_split[a].ToString() + "'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                        {
                            abs_ZoneArea += Program.UTIL.ToDoubleOrZero(ZoneValue[0][0]);
                        }
                    }
                    abs_data[230].Add(new { idx = i, val = (abs_Qmax_h / 1000).ToString("0.0") });//난방부하 
                    data.Add(new { cname = "abs_qmax_h", data = abs_data[230] });
                    abs_data[231].Add(new { idx = i, val = (abs_Qmax_c / 1000).ToString("0.0") });//냉방부하 
                    data.Add(new { cname = "abs_qmax_c", data = abs_data[231] });
                    abs_data[232].Add(new { idx = i, val = abs_ZoneArea.ToString("0.0") });//존면적
                    data.Add(new { cname = "abs_zonearea", data = abs_data[232] });
                    abs_data[233].Add(new { idx = i, val = abs_Zones_split.Count.ToString() });//존개수 
                    data.Add(new { cname = "abs_zonecount", data = abs_data[233] });

                    #endregion
                    items.Add("Element_GasHP.htm");
                    s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                    s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
                    System.Text.Json.JsonSerializer.Serialize(ghp_data[10].ToArray());

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