using Eagle._Components.Public;
using Eagle._Interfaces.Public;
using main.contents.Result.Element_Report;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.contents.Result
{
    public partial class Element_ElecHP : Form
    {
        bool scriptable = false;
        public Element_ElecHP()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            webView21.Source = new Uri(Program.gPath + "threejs\\public\\report.html", true);
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
                if (프로젝트유형[0][0] == "1")
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
            List<object>[] EHP_data = new List<object>[700];
            List<object>[] AirC_data = new List<object>[700];
            double d;
            string sp;
            int i = -1, n;
            while (++i < 700)
            {
                EHP_data[i] = new List<object>();
                AirC_data[i] = new List<object>();
            }
            string charts = "";
            i = -1;
            while (++i < 번호.Length)
            {
                #region 냉난방 전기히트펌프   

                double Total_Energy_pre = 0;
                string[][] value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='냉난방EHP'");
                string[][] value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                double ehp_total_saving = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    Total_Energy_pre = Convert.ToDouble(value3[0][0]);
                    ehp_total_saving = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value[0][0]));
                }

                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전기' and 검토유형='냉난방EHP'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전기'");
                double ehp_total_elec = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    ehp_total_elec = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value[0][0]));
                }
                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='냉난방EHP'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                double ehp_total_gas = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    ehp_total_gas = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value[0][0]));
                }

                string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.외기히트펌프번호,a.명칭,b.존,a.난방정격용량,a.난방정격COP,a.냉방정격용량,a.냉방정격COP,b.번호,b.외기히트펌프대수 From User_AirHP as a Inner Join HeatingSystem_Form as b ON a.번호 = b.외기히트펌프번호 Where a.난방냉방='냉난방' And a.연료='전기'");
                string[] EHP_Name = new string[18]; string[] EHP_Zone_text = new string[18];
                double[] EHP_Power_H = new double[18]; double[] EHP_COP_New_H = new double[18]; double[] EHP_Point_H = new double[18]; double[] EHP_COP_Rule_H = new double[18];
                double[] EHP_Power_C = new double[18]; double[] EHP_COP_New_C = new double[18]; double[] EHP_Point_C = new double[18]; double[] EHP_COP_Rule_C = new double[18];

                double[] EHP_elec_H = new double[18]; double[] EHP_elec_C = new double[18];
                double[] EHP_gas_H = new double[18]; double[] EHP_gas_C = new double[18];
                ArrayList EHP_Zones_split = new ArrayList();

                if (Value.Length > 0)
                {
                    for (int a = 0; a < Value.Length; a++)
                    {
                        EHP_Name[a] = Value[a][1];

                        ArrayList splitzone = new ArrayList();
                        splitzone = Split_(Value[a][2]);
                        if (splitzone.Count > 1) { EHP_Zone_text[a] = splitzone[0].ToString() + " 외 " + (splitzone.Count - 1).ToString() + "개"; }
                        else { EHP_Zone_text[a] = splitzone[0].ToString(); }

                        for (int aa = 0; aa < splitzone.Count; aa++)
                        {
                            if (EHP_Zones_split.Contains(splitzone[aa]))
                            { }
                            else { EHP_Zones_split.Add(splitzone[aa]); }
                        }


                        EHP_Power_H[a] = Convert.ToDouble(Value[a][3]) * Convert.ToDouble(Value[a][8]);
                        EHP_COP_New_H[a] = Convert.ToDouble(Value[a][4]);

                        EHP_Power_C[a] = Convert.ToDouble(Value[a][5]) * Convert.ToDouble(Value[a][8]);
                        EHP_COP_New_C[a] = Convert.ToDouble(Value[a][6]);

                        double Rule = 0; string[][] kkk = Program.DB.getValue(DB.type.BaseDB_Cooling, "AirCooler", "EER", "압축기= '스크롤' And 냉매='R134a' And 냉수출구온도 = '14' And 평균증발기온도='8'");
                        if (kkk.Length > 0)
                        {
                            Rule = Convert.ToDouble(kkk[0][0]);
                        }
                        EHP_COP_Rule_H[a] = 3.8; //DIN V 18599-5 table C.1
                        EHP_COP_Rule_C[a] = Rule; //DIN V 18599-7 table 27

                        EHP_Point_H[a] = Math.Min(100, EHP_COP_New_H[a] / EHP_COP_Rule_H[a] * 100);
                        EHP_Point_C[a] = Math.Min(100, EHP_COP_New_H[a] / EHP_COP_Rule_C[a] * 100);
                    }
                }
                for (int a = 0; a < 18; a++)
                {
                    if (EHP_elec_H[a] < 0) { EHP_elec_H[a] = 0; }
                    if (EHP_gas_H[a] < 0) { EHP_gas_H[a] = 0; }

                    if (EHP_elec_C[a] < 0) { EHP_elec_C[a] = 0; }
                    if (EHP_gas_C[a] < 0) { EHP_gas_C[a] = 0; }

                }

                double EHP_COP_Rule_H_total = 0; double EHP_COP_New_H_total = 0; double EHP_Point_H_total = 0;
                double EHP_COP_Rule_C_total = 0; double EHP_COP_New_C_total = 0; double EHP_Point_C_total = 0;
                for (int a = 0; a < 18; a++)
                {
                    EHP_data[a].Add(new { idx = i, val = EHP_Name[a] });//명칭
                    data.Add(new { cname = "ehp_name" + a, data = EHP_data[a] });
                    if (EHP_Name[a] != null & EHP_Name[a] != "")
                    {
                        EHP_data[18 + a].Add(new { idx = i, val = EHP_Zone_text[a] });//존
                        data.Add(new { cname = "ehp_zone" + a, data = EHP_data[18 + a] });

                        EHP_data[36 + a].Add(new { idx = i, val = EHP_Power_H[a].ToString("0.0") });//난방용량
                        data.Add(new { cname = "ehp_power_h" + a, data = EHP_data[36 + a] });

                        EHP_data[54 + a].Add(new { idx = i, val = EHP_COP_New_H[a].ToString("0.0") });//난방COP
                        data.Add(new { cname = "ehp_cop_new_h" + a, data = EHP_data[54 + a] });

                        EHP_data[72 + a].Add(new { idx = i, val = EHP_Power_C[a].ToString("0.0") });//냉방용량
                        data.Add(new { cname = "ehp_power_c" + a, data = EHP_data[72 + a] });

                        EHP_data[90 + a].Add(new { idx = i, val = EHP_COP_New_C[a].ToString("0.0") });//냉방COP
                        data.Add(new { cname = "ehp_cop_new_c" + a, data = EHP_data[90 + a] });

                        d = EHP_Point_H[a];
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        EHP_data[180 + a].Add(new { idx = i, val = sp });//난방 성능점수
                        data.Add(new { cname = "ehp_point_h" + a, data = EHP_data[180 + a] });

                        d = EHP_Point_C[a];
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        EHP_data[198 + a].Add(new { idx = i, val = sp });//냉방 성능점수
                        data.Add(new { cname = "ehp_point_c" + a, data = EHP_data[198 + a] });
                    }

                    //가중평균 
                    EHP_COP_New_H_total += EHP_COP_New_H[a] * EHP_Power_H[a];
                    EHP_Point_H_total += EHP_Point_H[a] * EHP_Power_H[a];
                    EHP_COP_New_C_total += EHP_COP_New_C[a] * EHP_Power_C[a];
                    EHP_Point_C_total += EHP_Point_C[a] * EHP_Power_C[a];
                    EHP_COP_Rule_H_total += EHP_COP_Rule_H[a] * EHP_Power_H[a];
                    EHP_COP_Rule_C_total += EHP_COP_Rule_C[a] * EHP_Power_C[a];
                }
                if (EHP_Power_H.Sum() > 0 && EHP_Power_C.Sum() > 0)
                {
                    EHP_COP_New_H_total = EHP_COP_New_H_total / EHP_Power_H.Sum();
                    EHP_Point_H_total = Math.Min(100, EHP_Point_H_total / EHP_Power_H.Sum());
                    EHP_COP_New_C_total = EHP_COP_New_C_total / EHP_Power_C.Sum();
                    EHP_Point_C_total = Math.Min(100, EHP_Point_C_total / EHP_Power_C.Sum());
                    EHP_COP_Rule_H_total = EHP_COP_Rule_H_total / EHP_Power_H.Sum();
                    EHP_COP_Rule_C_total = EHP_COP_Rule_C_total / EHP_Power_C.Sum();
                }

                EHP_data[216].Add(new { idx = i, val = ehp_total_saving.ToString("#,##0") });//절감량 전체 
                data.Add(new { cname = "ehp_saving_total", data = EHP_data[216] });
                EHP_data[217].Add(new { idx = i, val = (ehp_total_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });//절감률 전체 
                data.Add(new { cname = "ehp_saving_percent", data = EHP_data[217] });
                EHP_data[218].Add(new { idx = i, val = (ehp_total_elec * 0.4747 / 1000000 * 1000 + ehp_total_gas / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000).ToString("0.0") });//tco2
                data.Add(new { cname = "ehp_tco2", data = EHP_data[218] });
                EHP_data[219].Add(new { idx = i, val = (ehp_total_elec * 0.00023 + ehp_total_gas / 43.1 / 0.277778 * 0.00103).ToString("0.0") });//절감량 전체 
                data.Add(new { cname = "ehp_toe", data = EHP_data[219] });

                d = (ehp_total_saving / Total_Energy_pre * 100);
                charts += "{donut:" + d + "},";

                //합산 계 
                EHP_data[220].Add(new { idx = i, val = EHP_Power_H.Sum().ToString("0.0") });//난방 용량 합계  
                data.Add(new { cname = "ehp_power_h_total", data = EHP_data[220] });
                EHP_data[222].Add(new { idx = i, val = EHP_COP_New_H_total.ToString("0.0") });//난방 COP 평균  
                data.Add(new { cname = "ehp_cop_new_h_total", data = EHP_data[222] });
                d = EHP_Point_H_total;
                if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                EHP_data[224].Add(new { idx = i, val = sp });//난방 성능수준 평균  
                data.Add(new { cname = "ehp_point_h_total", data = EHP_data[224] });

                EHP_data[225].Add(new { idx = i, val = EHP_Power_C.Sum().ToString("0.0") });//냉방 용량 합계  
                data.Add(new { cname = "ehp_power_c_total", data = EHP_data[225] });
                EHP_data[227].Add(new { idx = i, val = EHP_COP_New_C_total.ToString("0.0") });//냉방 COP 평균  
                data.Add(new { cname = "ehp_cop_new_c_total", data = EHP_data[227] });
                d = EHP_Point_C_total;
                if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                EHP_data[229].Add(new { idx = i, val = sp });//냉방 성능수준 평균  
                data.Add(new { cname = "ehp_point_c_total", data = EHP_data[229] });

                double EHP_Qmax_h = 0; double EHP_Qmax_c = 0; double EHP_ZoneArea = 0;
                for (int a = 0; a < EHP_Zones_split.Count; a++)
                {
                    string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호='" + EHP_Zones_split[a].ToString() + "' And 난방_냉방='난방' and 비이용일_이용일='이용일' and 월='1월'");
                    if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                    {
                        EHP_Qmax_h += Convert.ToDouble(ZoneValue[0][0]);
                    }
                    ZoneValue = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호='" + EHP_Zones_split[a].ToString() + "' And 난방_냉방='냉방' and 비이용일_이용일='이용일' and 월='8월'");
                    if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                    {
                        EHP_Qmax_c += Convert.ToDouble(ZoneValue[0][0]);
                    }
                    ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "존번호='" + EHP_Zones_split[a].ToString() + "'");
                    if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                    {
                        EHP_ZoneArea += Convert.ToDouble(ZoneValue[0][0]);
                    }
                }
                EHP_data[230].Add(new { idx = i, val = (EHP_Qmax_h / 1000).ToString("0.0") });//난방부하 
                data.Add(new { cname = "ehp_qmax_h", data = EHP_data[230] });
                EHP_data[231].Add(new { idx = i, val = (EHP_Qmax_c / 1000).ToString("0.0") });//냉방부하 
                data.Add(new { cname = "ehp_qmax_c", data = EHP_data[231] });
                EHP_data[232].Add(new { idx = i, val = EHP_ZoneArea.ToString("0.0") });//존면적
                data.Add(new { cname = "ehp_zonearea", data = EHP_data[232] });
                EHP_data[233].Add(new { idx = i, val = EHP_Zones_split.Count.ToString() });//존개수 
                data.Add(new { cname = "ehp_zonecount", data = EHP_data[233] });
                if (EHP_COP_Rule_H_total > 0)
                {
                    EHP_data[234].Add(new { idx = i, val = "* DIN V 18599-5 표준 COP : " + EHP_COP_Rule_H_total.ToString("0.0") + " 기준" });//난방 법규 평균  
                    data.Add(new { cname = "ehp_cop_rule_h", data = EHP_data[234] });
                }

                if (EHP_COP_Rule_C_total > 0)
                {
                    EHP_data[235].Add(new { idx = i, val = "* DIN V 18599-7 표준 EER : " + EHP_COP_Rule_C_total.ToString("0.0") + " 기준" });//냉방 법규 평균  
                    data.Add(new { cname = "ehp_cop_rule_c", data = EHP_data[235] });
                }

                #endregion


                #region 냉방 전기히트펌프   
                Total_Energy_pre = 0;
                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='냉방EHP'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                double airc_total_saving = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    Total_Energy_pre = Convert.ToDouble(value3[0][0]);
                    airc_total_saving = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value[0][0]));
                }

                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전기' and 검토유형='냉방EHP'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전기'");
                double airc_total_elec = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    airc_total_elec = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value[0][0]));
                }
                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='냉방EHP'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                double airc_total_gas = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    airc_total_gas = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value[0][0]));
                }

                Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.냉방유닛,a.명칭,b.공급존,a.난방정격용량,a.난방정격COP,a.냉방정격용량,a.냉방정격COP,b.번호,b.설치대수 From User_AirHP as a Inner Join CoolingSystem_Form as b ON a.번호 = b.냉방유닛 Where a.난방냉방='냉방' And a.연료='전기'");
                string[] AirC_Name = new string[18]; string[] AirC_Zone_text = new string[18];
                double[] AirC_Power = new double[18]; double[] AirC_COP_New = new double[18]; double[] AirC_Point = new double[18]; double[] AirC_COP_Rule = new double[18];

                double[] AirC_elec = new double[18]; double[] AirC_gas = new double[18];
                ArrayList AirC_Zones_split = new ArrayList();

                if (Value.Length > 0)
                {
                    for (int a = 0; a < Value.Length; a++)
                    {
                        AirC_Name[a] = Value[a][1];

                        ArrayList splitzone = new ArrayList();
                        splitzone = Split_(Value[a][2]);
                        if (splitzone.Count > 1) { AirC_Zone_text[a] = splitzone[0].ToString() + " 외 " + (splitzone.Count - 1).ToString() + "개"; }
                        else { AirC_Zone_text[a] = splitzone[0].ToString(); }

                        for (int aa = 0; aa < splitzone.Count; aa++)
                        {
                            if (AirC_Zones_split.Contains(splitzone[aa]))
                            { }
                            else { AirC_Zones_split.Add(splitzone[aa]); }
                        }
                        AirC_Power[a] = Convert.ToDouble(Value[a][5]) * Convert.ToDouble(Value[a][8]);
                        AirC_COP_New[a] = Convert.ToDouble(Value[a][6]);
                        double Rule = 0; string[][] kkk = Program.DB.getValue(DB.type.BaseDB_Cooling, "AirCooler", "EER", "압축기= '스크롤' And 냉매='R134a' And 냉수출구온도 = '14' And 평균증발기온도='8'");
                        if (kkk.Length > 0)
                        {
                            Rule = Convert.ToDouble(kkk[0][0]);
                        }
                        AirC_COP_Rule[a] = Rule; //DIN V 18599-7 table 27
                        AirC_Point[a] = Math.Min(100, AirC_COP_New[a] / AirC_COP_Rule[a] * 100);
                    }
                }
                for (int a = 0; a < 18; a++)
                {
                    if (AirC_elec[a] < 0) { AirC_elec[a] = 0; }
                    if (AirC_gas[a] < 0) { AirC_gas[a] = 0; }
                }
                double AirC_COP_Rule_C_total = 0;
                double AirC_COP_New_C_total = 0; double AirC_Point_C_total = 0;
                for (int a = 0; a < 18; a++)
                {
                    AirC_data[a].Add(new { idx = i, val = AirC_Name[a] });//명칭
                    data.Add(new { cname = "airc_name" + a, data = AirC_data[a] });
                    if (AirC_Name[a] != null & AirC_Name[a] != "")
                    {
                        AirC_data[18 + a].Add(new { idx = i, val = AirC_Zone_text[a] });//존
                        data.Add(new { cname = "airc_zone" + a, data = AirC_data[18 + a] });

                        AirC_data[72 + a].Add(new { idx = i, val = AirC_Power[a].ToString("0.0") });//냉방용량
                        data.Add(new { cname = "airc_power" + a, data = AirC_data[72 + a] });

                        AirC_data[90 + a].Add(new { idx = i, val = AirC_COP_New[a].ToString("0.0") });//냉방COP
                        data.Add(new { cname = "airc_cop_new" + a, data = AirC_data[90 + a] });

                        d = AirC_Point[a];
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        AirC_data[198 + a].Add(new { idx = i, val = sp });//냉방 성능점수
                        data.Add(new { cname = "airc_point" + a, data = AirC_data[198 + a] });
                    }

                    //가중평균 
                    AirC_COP_Rule_C_total += AirC_COP_Rule[a] * AirC_Power[a];
                    AirC_COP_New_C_total += AirC_COP_New[a] * AirC_Power[a];
                    AirC_Point_C_total += AirC_Point[a] * AirC_Power[a];
                }
                if (AirC_Power.Sum() > 0)
                {
                    AirC_COP_Rule_C_total = AirC_COP_Rule_C_total / AirC_Power.Sum();
                    AirC_COP_New_C_total = AirC_COP_New_C_total / AirC_Power.Sum();
                    AirC_Point_C_total = Math.Min(100, AirC_Point_C_total / AirC_Power.Sum());
                }


                AirC_data[216].Add(new { idx = i, val = airc_total_saving.ToString("#,##0") });//절감량 전체 
                data.Add(new { cname = "airc_saving_total", data = AirC_data[216] });
                AirC_data[217].Add(new { idx = i, val = (airc_total_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });//절감률 전체 
                data.Add(new { cname = "airc_saving_percent", data = AirC_data[217] });
                AirC_data[218].Add(new { idx = i, val = (airc_total_elec * 0.4747 / 1000000 * 1000 + airc_total_gas / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000).ToString("0.0") });//tco2
                data.Add(new { cname = "airc_tco2", data = AirC_data[218] });
                AirC_data[219].Add(new { idx = i, val = (airc_total_elec * 0.00023 + airc_total_gas / 43.1 / 0.277778 * 0.00103).ToString("0.0") });//절감량 전체 
                data.Add(new { cname = "airc_toe", data = AirC_data[219] });

                d = (airc_total_saving / Total_Energy_pre * 100);
                charts += "{donut:" + d + "},";

                //합산 계 
                AirC_data[225].Add(new { idx = i, val = AirC_Power.Sum().ToString("0.0") });//냉방 용량 합계  
                data.Add(new { cname = "airc_power_c_total", data = AirC_data[225] });
                AirC_data[227].Add(new { idx = i, val = AirC_COP_New_C_total.ToString("0.0") });//냉방 COP 평균  
                data.Add(new { cname = "airc_cop_new_c_total", data = AirC_data[227] });
                AirC_data[228].Add(new { idx = i, val = (airc_total_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });//냉방 절감량 합계  
                data.Add(new { cname = "airc_saving_c_total", data = AirC_data[228] });
                d = AirC_Point_C_total;
                if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                AirC_data[229].Add(new { idx = i, val = sp });//냉방 성능수준 평균  
                data.Add(new { cname = "airc_point_c_total", data = AirC_data[229] });

                double AirC_Qmax_c = 0; double AirC_ZoneArea = 0;
                for (int a = 0; a < AirC_Zones_split.Count; a++)
                {
                    string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호='" + AirC_Zones_split[a].ToString() + "' And 난방_냉방='냉방' and 비이용일_이용일='이용일' and 월='8월'");
                    if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                    {
                        AirC_Qmax_c += Convert.ToDouble(ZoneValue[0][0]);
                    }
                    ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "존번호='" + AirC_Zones_split[a].ToString() + "'");
                    if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                    {
                        AirC_ZoneArea += Convert.ToDouble(ZoneValue[0][0]);
                    }
                }
                AirC_data[231].Add(new { idx = i, val = (AirC_Qmax_c / 1000).ToString("0.0") });//냉방부하 
                data.Add(new { cname = "airc_qmax_c", data = AirC_data[231] });
                AirC_data[232].Add(new { idx = i, val = AirC_ZoneArea.ToString("0.0") });//존면적
                data.Add(new { cname = "airc_zonearea", data = AirC_data[232] });
                AirC_data[233].Add(new { idx = i, val = AirC_Zones_split.Count.ToString() });//존개수 
                data.Add(new { cname = "airc_zonecount", data = AirC_data[233] });
                if (AirC_COP_Rule_C_total > 0)
                {
                    AirC_data[234].Add(new { idx = i, val = "* DIN V 18599-7 표준 EER : " + AirC_COP_Rule_C_total.ToString("0.0") + " 기준" });//냉방 법규 평균  
                    data.Add(new { cname = "airc_cop_rule_c", data = AirC_data[234] });
                }


                #endregion

                items.Add("Element_ElecHP2.htm");
                s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
                System.Text.Json.JsonSerializer.Serialize(EHP_data[10].ToArray());

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
            List<object>[] EHP_data = new List<object>[700];
            List<object>[] AirC_data = new List<object>[700];
            double d;
            string sp;
            int i = -1, n;
            while (++i < 700)
            {
                EHP_data[i] = new List<object>();
                AirC_data[i] = new List<object>();
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


                    #region 냉난방 전기히트펌프   
                    string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.외기히트펌프번호,a.명칭,b.존,a.난방정격용량,a.난방정격COP,a.냉방정격용량,a.냉방정격COP,b.번호,b.외기히트펌프대수 From User_AirHP as a Inner Join HeatingSystem_Form as b ON a.번호 = b.외기히트펌프번호 Where a.난방냉방='냉난방' And a.연료='전기'");
                    string[] EHP_Name = new string[18]; string[] EHP_Zone_text = new string[18];
                    double[] EHP_Power_H = new double[18]; double[] EHP_COP_Old_H = new double[18]; double[] EHP_COP_New_H = new double[18]; double[] EHP_Saving_H = new double[18]; double[] EHP_Point_H = new double[18]; double[] EHP_COP_Rule_H = new double[18];
                    double[] EHP_Power_C = new double[18]; double[] EHP_COP_Old_C = new double[18]; double[] EHP_COP_New_C = new double[18]; double[] EHP_Saving_C = new double[18]; double[] EHP_Point_C = new double[18]; double[] EHP_COP_Rule_C = new double[18];

                    double[] EHP_elec_H = new double[18]; double[] EHP_elec_C = new double[18];
                    double[] EHP_gas_H = new double[18]; double[] EHP_gas_C = new double[18];
                    ArrayList EHP_Zones_split = new ArrayList();
                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            for (int aa = 0; aa < HeatingGroup.Count; aa++)
                            {
                                Heating_New_Old hh = (Heating_New_Old)HeatingGroup[aa];
                                if (Value[a][7] == hh.Num_New())
                                {
                                    EHP_Saving_H[a] = heating_element_saving[aa];
                                    EHP_elec_H[a] = heating_element_saving_elec[aa];
                                    EHP_gas_H[a] = heating_element_saving_gas[aa];
                                    for (int aaa = 0; aaa < hh.Num_Old().Count; aaa++)
                                    {
                                        string[][] OldSystem = Program.DB.querySQL(res[0][0], "Select a.난방정격COP From User_AirHP as a Inner Join HeatingSystem_Form as b ON a.번호 = b.외기히트펌프번호 Where b.번호 ='" + hh.Num_Old()[aaa] + "'");
                                        if (OldSystem.Length > 0)
                                        {
                                            if (Convert.ToDouble(OldSystem[0][0]) == Convert.ToDouble(Value[a][4]))
                                            { EHP_COP_Old_H[a] = Convert.ToDouble(Value[a][4]); break; }
                                            else if (Convert.ToDouble(OldSystem[0][0]) < EHP_COP_Old_H[a]) { EHP_COP_Old_H[a] = Convert.ToDouble(OldSystem[0][0]); }
                                            else if (EHP_COP_Old_H[a] == 0) { EHP_COP_Old_H[a] = Convert.ToDouble(OldSystem[0][0]); }
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
                                        EHP_Saving_H[a] = EHP_Saving_H[a] - Q_sol_a;
                                        if (EHP_gas_H[a] > EHP_elec_H[a]) { EHP_gas_H[a] = EHP_gas_H[a] - Q_sol_a; }
                                        else { EHP_elec_H[a] = EHP_elec_H[a] - Q_sol_a; }
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
                                        EHP_Saving_C[a] = cooling_element_saving[aa];
                                        EHP_elec_C[a] = cooling_element_saving_elec[aa];
                                        EHP_gas_C[a] = cooling_element_saving_gas[aa];
                                        for (int aaa = 0; aaa < cc.Num_Old().Count; aaa++)
                                        {
                                            string[][] OldSystem = Program.DB.querySQL(res[0][0], "Select a.냉방정격COP From User_AirHP as a Inner Join CoolingSystem_Form as b ON a.번호 = b.냉방유닛 Where b.번호 ='" + cc.Num_Old()[aaa] + "'");
                                            if (OldSystem.Length > 0)
                                            {
                                                if (Convert.ToDouble(OldSystem[0][0]) == Convert.ToDouble(Value[a][6]))
                                                { EHP_COP_Old_C[a] = Convert.ToDouble(Value[a][6]); break; }
                                                else if (Convert.ToDouble(OldSystem[0][0]) < EHP_COP_Old_C[a]) { EHP_COP_Old_C[a] = Convert.ToDouble(OldSystem[0][0]); }
                                                else if (EHP_COP_Old_C[a] == 0) { EHP_COP_Old_C[a] = Convert.ToDouble(OldSystem[0][0]); }
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
                            EHP_Name[a] = Value[a][1];

                            ArrayList splitzone = new ArrayList();
                            splitzone = Split_(Value[a][2]);
                            if (splitzone.Count > 1) { EHP_Zone_text[a] = splitzone[0].ToString() + " 외 " + (splitzone.Count - 1).ToString() + "개"; }
                            else { EHP_Zone_text[a] = splitzone[0].ToString(); }

                            for (int aa = 0; aa < splitzone.Count; aa++)
                            {
                                if (EHP_Zones_split.Contains(splitzone[aa]))
                                { }
                                else { EHP_Zones_split.Add(splitzone[aa]); }
                            }


                            EHP_Power_H[a] = Convert.ToDouble(Value[a][3]) * Convert.ToDouble(Value[a][8]);
                            EHP_COP_New_H[a] = Convert.ToDouble(Value[a][4]);

                            EHP_Power_C[a] = Convert.ToDouble(Value[a][5]) * Convert.ToDouble(Value[a][8]);
                            EHP_COP_New_C[a] = Convert.ToDouble(Value[a][6]);

                            double Rule = 0; string[][] kkk = Program.DB.getValue(DB.type.BaseDB_Cooling, "AirCooler", "EER", "압축기= '스크롤' And 냉매='R134a' And 냉수출구온도 = '14' And 평균증발기온도='8'");
                            if (kkk.Length > 0)
                            {
                                Rule = Convert.ToDouble(kkk[0][0]);
                            }
                            EHP_COP_Rule_H[a] = 3.8; //DIN V 18599-5 table C.1
                            EHP_COP_Rule_C[a] = Rule; //DIN V 18599-7 table 27

                            EHP_Point_H[a] = Math.Min(100, EHP_COP_New_H[a] / EHP_COP_Rule_H[a] * 100);
                            EHP_Point_C[a] = Math.Min(100, EHP_COP_New_H[a] / EHP_COP_Rule_C[a] * 100);
                        }
                    }
                    for (int a = 0; a < 18; a++)
                    {
                        if (EHP_Saving_H[a] < 0) { EHP_Saving_H[a] = 0; }
                        if (EHP_elec_H[a] < 0) { EHP_elec_H[a] = 0; }
                        if (EHP_gas_H[a] < 0) { EHP_gas_H[a] = 0; }

                        if (EHP_Saving_C[a] < 0) { EHP_Saving_C[a] = 0; }
                        if (EHP_elec_C[a] < 0) { EHP_elec_C[a] = 0; }
                        if (EHP_gas_C[a] < 0) { EHP_gas_C[a] = 0; }

                    }

                    double ehp_total_saving_h = 0; double ehp_total_saving_c = 0;
                    double ehp_total_saving = 0; double ehp_total_elec = 0; double ehp_total_gas = 0;
                    double EHP_COP_New_H_total = 0; double EHP_COP_Old_H_total = 0; double EHP_Point_H_total = 0;
                    double EHP_COP_New_C_total = 0; double EHP_COP_Old_C_total = 0; double EHP_Point_C_total = 0;
                    double EHP_COP_Rule_H_total = 0; double EHP_COP_Rule_C_total = 0;
                    for (int a = 0; a < 18; a++)
                    {
                        EHP_data[a].Add(new { idx = i, val = EHP_Name[a] });//명칭
                        data.Add(new { cname = "ehp_name" + a, data = EHP_data[a] });
                        if (EHP_Name[a] != null & EHP_Name[a] != "")
                        {
                            EHP_data[18 + a].Add(new { idx = i, val = EHP_Zone_text[a] });//존
                            data.Add(new { cname = "ehp_zone" + a, data = EHP_data[18 + a] });

                            EHP_data[36 + a].Add(new { idx = i, val = EHP_Power_H[a].ToString("0.0") });//난방용량
                            data.Add(new { cname = "ehp_power_h" + a, data = EHP_data[36 + a] });

                            EHP_data[54 + a].Add(new { idx = i, val = EHP_COP_New_H[a].ToString("0.0") });//난방COP
                            data.Add(new { cname = "ehp_cop_new_h" + a, data = EHP_data[54 + a] });

                            EHP_data[72 + a].Add(new { idx = i, val = EHP_Power_C[a].ToString("0.0") });//냉방용량
                            data.Add(new { cname = "ehp_power_c" + a, data = EHP_data[72 + a] });

                            EHP_data[90 + a].Add(new { idx = i, val = EHP_COP_New_C[a].ToString("0.0") });//냉방COP
                            data.Add(new { cname = "ehp_cop_new_c" + a, data = EHP_data[90 + a] });

                            if (EHP_COP_Old_H[a] != 0)
                            { EHP_data[108 + a].Add(new { idx = i, val = EHP_COP_Old_H[a].ToString("0.0") }); }//난방 기존 COP
                            else { EHP_data[108 + a].Add(new { idx = i, val = "Not EHP" }); }
                            data.Add(new { cname = "ehp_cop_old_h" + a, data = EHP_data[108 + a] });

                            if (EHP_COP_Old_C[a] != 0)
                            { EHP_data[126 + a].Add(new { idx = i, val = EHP_COP_Old_C[a].ToString("0.0") }); }//냉방 기존 COP
                            else { EHP_data[126 + a].Add(new { idx = i, val = "Not EHP" }); }
                            data.Add(new { cname = "ehp_cop_old_c" + a, data = EHP_data[126 + a] });
                            EHP_data[144 + a].Add(new { idx = i, val = (EHP_Saving_H[a] / Total_Energy_pre * 100).ToString("0.0") + " %" });//난방 절감률
                            data.Add(new { cname = "ehp_saving_h" + a, data = EHP_data[144 + a] });

                            EHP_data[162 + a].Add(new { idx = i, val = (EHP_Saving_C[a] / Total_Energy_pre * 100).ToString("0.0") + " %" });//냉방 절감률
                            data.Add(new { cname = "ehp_saving_c" + a, data = EHP_data[162 + a] });

                            d = EHP_Point_H[a];
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            EHP_data[180 + a].Add(new { idx = i, val = sp });//난방 성능점수
                            data.Add(new { cname = "ehp_point_h" + a, data = EHP_data[180 + a] });

                            d = EHP_Point_C[a];
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            EHP_data[198 + a].Add(new { idx = i, val = sp });//냉방 성능점수
                            data.Add(new { cname = "ehp_point_c" + a, data = EHP_data[198 + a] });
                        }

                        //가중평균 
                        EHP_COP_New_H_total += EHP_COP_New_H[a] * EHP_Power_H[a];
                        EHP_COP_Old_H_total += EHP_COP_Old_H[a] * EHP_Power_H[a];
                        EHP_Point_H_total += EHP_Point_H[a] * EHP_Power_H[a];
                        EHP_COP_New_C_total += EHP_COP_New_C[a] * EHP_Power_C[a];
                        EHP_COP_Old_C_total += EHP_COP_Old_C[a] * EHP_Power_C[a];
                        EHP_Point_C_total += EHP_Point_C[a] * EHP_Power_C[a];
                        EHP_COP_Rule_H_total += EHP_COP_Rule_H[a] * EHP_Power_H[a];
                        EHP_COP_Rule_C_total += EHP_COP_Rule_C[a] * EHP_Power_C[a];
                    }
                    if (EHP_Power_H.Sum() > 0 && EHP_Power_C.Sum() > 0)
                    {
                        EHP_COP_New_H_total = EHP_COP_New_H_total / EHP_Power_H.Sum();
                        EHP_COP_Old_H_total = EHP_COP_Old_H_total / EHP_Power_H.Sum();
                        EHP_Point_H_total = Math.Min(100, EHP_Point_H_total / EHP_Power_H.Sum());
                        EHP_COP_New_C_total = EHP_COP_New_C_total / EHP_Power_C.Sum();
                        EHP_COP_Old_C_total = EHP_COP_Old_C_total / EHP_Power_C.Sum();
                        EHP_Point_C_total = Math.Min(100, EHP_Point_C_total / EHP_Power_C.Sum());
                        EHP_COP_Rule_H_total = EHP_COP_Rule_H_total / EHP_Power_H.Sum();
                        EHP_COP_Rule_C_total = EHP_COP_Rule_C_total / EHP_Power_C.Sum();
                    }

                    for (int a = 0; a < 18; a++)
                    {
                        ehp_total_saving_h += EHP_Saving_H[a];
                        ehp_total_saving_c += EHP_Saving_C[a];
                        ehp_total_saving += EHP_Saving_H[a] + EHP_Saving_C[a];
                        ehp_total_elec += EHP_elec_H[a] + EHP_elec_C[a];
                        ehp_total_gas += EHP_gas_H[a] + EHP_gas_C[a];
                    }
                    EHP_data[216].Add(new { idx = i, val = ehp_total_saving.ToString("#,##0") });//절감량 전체 
                    data.Add(new { cname = "ehp_saving_total", data = EHP_data[216] });
                    EHP_data[217].Add(new { idx = i, val = (ehp_total_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });//절감률 전체 
                    data.Add(new { cname = "ehp_saving_percent", data = EHP_data[217] });
                    EHP_data[218].Add(new { idx = i, val = (ehp_total_elec * 0.4747 / 1000000 * 1000 + ehp_total_gas / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000).ToString("0.0") });//tco2
                    data.Add(new { cname = "ehp_tco2", data = EHP_data[218] });
                    EHP_data[219].Add(new { idx = i, val = (ehp_total_elec * 0.00023 + ehp_total_gas / 43.1 / 0.277778 * 0.00103).ToString("0.0") });//절감량 전체 
                    data.Add(new { cname = "ehp_toe", data = EHP_data[219] });

                    d = (ehp_total_saving / Total_Energy_pre * 100);
                    charts += "{donut:" + d + "},";

                    //합산 계 
                    EHP_data[220].Add(new { idx = i, val = EHP_Power_H.Sum().ToString("0.0") });//난방 용량 합계  
                    data.Add(new { cname = "ehp_power_h_total", data = EHP_data[220] });
                    EHP_data[221].Add(new { idx = i, val = EHP_COP_Old_H_total.ToString("0.0") });//난방 기존 COP 평균  
                    data.Add(new { cname = "ehp_cop_old_h_total", data = EHP_data[221] });
                    EHP_data[222].Add(new { idx = i, val = EHP_COP_New_H_total.ToString("0.0") });//난방 COP 평균  
                    data.Add(new { cname = "ehp_cop_new_h_total", data = EHP_data[222] });
                    EHP_data[223].Add(new { idx = i, val = (ehp_total_saving_h / Total_Energy_pre * 100).ToString("0.0") + " %" });//난방 절감량 합계  
                    data.Add(new { cname = "ehp_saving_h_total", data = EHP_data[223] });
                    d = EHP_Point_H_total;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    EHP_data[224].Add(new { idx = i, val = sp });//난방 성능수준 평균  
                    data.Add(new { cname = "ehp_point_h_total", data = EHP_data[224] });

                    EHP_data[225].Add(new { idx = i, val = EHP_Power_C.Sum().ToString("0.0") });//냉방 용량 합계  
                    data.Add(new { cname = "ehp_power_c_total", data = EHP_data[225] });
                    EHP_data[226].Add(new { idx = i, val = EHP_COP_Old_C_total.ToString("0.0") });//냉방 기존 COP 평균  
                    data.Add(new { cname = "ehp_cop_old_c_total", data = EHP_data[226] });
                    EHP_data[227].Add(new { idx = i, val = EHP_COP_New_C_total.ToString("0.0") });//냉방 COP 평균  
                    data.Add(new { cname = "ehp_cop_new_c_total", data = EHP_data[227] });
                    EHP_data[228].Add(new { idx = i, val = (ehp_total_saving_c / Total_Energy_pre * 100).ToString("0.0") + " %" });//냉방 절감량 합계  
                    data.Add(new { cname = "ehp_saving_c_total", data = EHP_data[228] });
                    d = EHP_Point_C_total;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    EHP_data[229].Add(new { idx = i, val = sp });//냉방 성능수준 평균  
                    data.Add(new { cname = "ehp_point_c_total", data = EHP_data[229] });

                    double EHP_Qmax_h = 0; double EHP_Qmax_c = 0; double EHP_ZoneArea = 0;
                    for (int a = 0; a < EHP_Zones_split.Count; a++)
                    {
                        string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호='" + EHP_Zones_split[a].ToString() + "' And 난방_냉방='난방' and 비이용일_이용일='이용일' and 월='1월'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                        {
                            EHP_Qmax_h += Convert.ToDouble(ZoneValue[0][0]);
                        }
                        ZoneValue = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호='" + EHP_Zones_split[a].ToString() + "' And 난방_냉방='냉방' and 비이용일_이용일='이용일' and 월='8월'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                        {
                            EHP_Qmax_c += Convert.ToDouble(ZoneValue[0][0]);
                        }
                        ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "존번호='" + EHP_Zones_split[a].ToString() + "'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                        {
                            EHP_ZoneArea += Convert.ToDouble(ZoneValue[0][0]);
                        }
                    }
                    EHP_data[230].Add(new { idx = i, val = (EHP_Qmax_h / 1000).ToString("0.0") });//난방부하 
                    data.Add(new { cname = "ehp_qmax_h", data = EHP_data[230] });
                    EHP_data[231].Add(new { idx = i, val = (EHP_Qmax_c / 1000).ToString("0.0") });//냉방부하 
                    data.Add(new { cname = "ehp_qmax_c", data = EHP_data[231] });
                    EHP_data[232].Add(new { idx = i, val = EHP_ZoneArea.ToString("0.0") });//존면적
                    data.Add(new { cname = "ehp_zonearea", data = EHP_data[232] });
                    EHP_data[233].Add(new { idx = i, val = EHP_Zones_split.Count.ToString() });//존개수 
                    data.Add(new { cname = "ehp_zonecount", data = EHP_data[233] });
                    if (EHP_COP_Rule_H_total > 0)
                    {
                        EHP_data[234].Add(new { idx = i, val = "* DIN V 18599-5 표준 COP : " + EHP_COP_Rule_H_total.ToString("0.0") + " 기준" });//난방 법규 평균  
                        data.Add(new { cname = "ehp_cop_rule_h", data = EHP_data[234] });
                    }

                    if (EHP_COP_Rule_C_total > 0)
                    {
                        EHP_data[235].Add(new { idx = i, val = "* DIN V 18599-7 표준 EER : " + EHP_COP_Rule_C_total.ToString("0.0") + " 기준" });//냉방 법규 평균  
                        data.Add(new { cname = "ehp_cop_rule_c", data = EHP_data[235] });
                    }

                    #endregion


                    #region 냉방 전기히트펌프   
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.냉방유닛,a.명칭,b.공급존,a.난방정격용량,a.난방정격COP,a.냉방정격용량,a.냉방정격COP,b.번호,b.설치대수 From User_AirHP as a Inner Join CoolingSystem_Form as b ON a.번호 = b.냉방유닛 Where a.난방냉방='냉방' And a.연료='전기'");
                    string[] AirC_Name = new string[18]; string[] AirC_Zone_text = new string[18];
                    double[] AirC_Power = new double[18]; double[] AirC_COP_Old = new double[18]; double[] AirC_COP_New = new double[18]; double[] AirC_Saving = new double[18]; double[] AirC_Point = new double[18]; double[] AirC_COP_Rule = new double[18];

                    double[] AirC_elec = new double[18]; double[] AirC_gas = new double[18];
                    ArrayList AirC_Zones_split = new ArrayList();

                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            string[][] coolingvalue = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "번호", "냉방유닛='" + Value[a][0] + "'");
                            if (coolingvalue.Length > 0)
                            {
                                for (int aa = 0; aa < CoolingGroup.Count; aa++)
                                {
                                    Cooling_New_Old cc = (Cooling_New_Old)CoolingGroup[aa];
                                    if (coolingvalue[0][0] == cc.Num_New())
                                    {
                                        AirC_Saving[a] = cooling_element_saving[aa];
                                        AirC_elec[a] = cooling_element_saving_elec[aa];
                                        AirC_gas[a] = cooling_element_saving_gas[aa];
                                        for (int aaa = 0; aaa < cc.Num_Old().Count; aaa++)
                                        {
                                            string[][] OldSystem = Program.DB.querySQL(res[0][0], "Select a.냉방정격COP From User_AirHP as a Inner Join CoolingSystem_Form as b ON a.번호 = b.냉방유닛 Where b.번호 ='" + cc.Num_Old()[aaa] + "'");
                                            if (OldSystem.Length > 0)
                                            {
                                                if (Convert.ToDouble(OldSystem[0][0]) == Convert.ToDouble(Value[a][6]))
                                                { AirC_COP_Old[a] = Convert.ToDouble(Value[a][6]); break; }
                                                else if (Convert.ToDouble(OldSystem[0][0]) < AirC_COP_Old[a]) { AirC_COP_Old[a] = Convert.ToDouble(OldSystem[0][0]); }
                                                else if (AirC_COP_Old[a] == 0) { AirC_COP_Old[a] = Convert.ToDouble(OldSystem[0][0]); }
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
                            AirC_Name[a] = Value[a][1];

                            ArrayList splitzone = new ArrayList();
                            splitzone = Split_(Value[a][2]);
                            if (splitzone.Count > 1) { AirC_Zone_text[a] = splitzone[0].ToString() + " 외 " + (splitzone.Count - 1).ToString() + "개"; }
                            else { AirC_Zone_text[a] = splitzone[0].ToString(); }

                            for (int aa = 0; aa < splitzone.Count; aa++)
                            {
                                if (AirC_Zones_split.Contains(splitzone[aa]))
                                { }
                                else { AirC_Zones_split.Add(splitzone[aa]); }
                            }
                            AirC_Power[a] = Convert.ToDouble(Value[a][5]) * Convert.ToDouble(Value[a][8]);
                            AirC_COP_New[a] = Convert.ToDouble(Value[a][6]);

                            double Rule = 0; string[][] kkk = Program.DB.getValue(DB.type.BaseDB_Cooling, "AirCooler", "EER", "압축기= '스크롤' And 냉매='R134a' And 냉수출구온도 = '14' And 평균증발기온도='8'");
                            if (kkk.Length > 0)
                            {
                                Rule = Convert.ToDouble(kkk[0][0]);
                            }
                            AirC_COP_Rule[a] = Rule; //DIN V 18599-7 table 27
                            AirC_Point[a] = Math.Min(100, AirC_COP_New[a] / AirC_COP_Rule[a] * 100);
                        }
                    }
                    for (int a = 0; a < 18; a++)
                    {
                        if (AirC_Saving[a] < 0) { AirC_Saving[a] = 0; }
                        if (AirC_elec[a] < 0) { AirC_elec[a] = 0; }
                        if (AirC_gas[a] < 0) { AirC_gas[a] = 0; }
                    }
                    double airc_total_saving = 0; double airc_total_elec = 0; double airc_total_gas = 0;
                    double AirC_COP_New_C_total = 0; double AirC_COP_Old_C_total = 0; double AirC_Point_C_total = 0; double AirC_COP_Rule_C_total = 0;
                    for (int a = 0; a < 18; a++)
                    {
                        AirC_data[a].Add(new { idx = i, val = AirC_Name[a] });//명칭
                        data.Add(new { cname = "airc_name" + a, data = AirC_data[a] });
                        if (AirC_Name[a] != null & AirC_Name[a] != "")
                        {
                            AirC_data[18 + a].Add(new { idx = i, val = AirC_Zone_text[a] });//존
                            data.Add(new { cname = "airc_zone" + a, data = AirC_data[18 + a] });

                            AirC_data[72 + a].Add(new { idx = i, val = AirC_Power[a].ToString("0.0") });//냉방용량
                            data.Add(new { cname = "airc_power" + a, data = AirC_data[72 + a] });

                            AirC_data[90 + a].Add(new { idx = i, val = AirC_COP_New[a].ToString("0.0") });//냉방COP
                            data.Add(new { cname = "airc_cop_new" + a, data = AirC_data[90 + a] });

                            if (AirC_COP_Old[a] != 0)
                            { AirC_data[126 + a].Add(new { idx = i, val = AirC_COP_Old[a].ToString("0.0") }); }//냉방 기존 COP
                            else { AirC_data[126 + a].Add(new { idx = i, val = "Not EHP" }); }
                            data.Add(new { cname = "airc_cop_old" + a, data = AirC_data[126 + a] });
                            AirC_data[162 + a].Add(new { idx = i, val = (AirC_Saving[a] / Total_Energy_pre * 100).ToString("0.0") + " %" });//냉방 절감률
                            data.Add(new { cname = "airc_saving" + a, data = AirC_data[162 + a] });

                            d = AirC_Point[a];
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            AirC_data[198 + a].Add(new { idx = i, val = sp });//냉방 성능점수
                            data.Add(new { cname = "airc_point" + a, data = AirC_data[198 + a] });
                        }

                        //가중평균 
                        AirC_COP_New_C_total += AirC_COP_New[a] * AirC_Power[a];
                        AirC_COP_Old_C_total += AirC_COP_Old[a] * AirC_Power[a];
                        AirC_Point_C_total += AirC_Point[a] * AirC_Power[a];
                        AirC_COP_Rule_C_total += AirC_COP_Rule[a] * AirC_Power[a];
                    }
                    if (AirC_Power.Sum() > 0)
                    {
                        AirC_COP_New_C_total = AirC_COP_New_C_total / AirC_Power.Sum();
                        AirC_COP_Old_C_total = AirC_COP_Old_C_total / AirC_Power.Sum();
                        AirC_Point_C_total = Math.Min(100, AirC_Point_C_total / AirC_Power.Sum());
                        AirC_COP_Rule_C_total = AirC_COP_Rule_C_total / AirC_Power.Sum();
                    }

                    for (int a = 0; a < 18; a++)
                    {
                        airc_total_saving += AirC_Saving[a];
                        airc_total_elec += AirC_elec[a];
                        airc_total_gas += AirC_gas[a];
                    }

                    AirC_data[216].Add(new { idx = i, val = airc_total_saving.ToString("#,##0") });//절감량 전체 
                    data.Add(new { cname = "airc_saving_total", data = AirC_data[216] });
                    AirC_data[217].Add(new { idx = i, val = (airc_total_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });//절감률 전체 
                    data.Add(new { cname = "airc_saving_percent", data = AirC_data[217] });
                    AirC_data[218].Add(new { idx = i, val = (airc_total_elec * 0.4747 / 1000000 * 1000 + airc_total_gas / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000).ToString("0.0") });//tco2
                    data.Add(new { cname = "airc_tco2", data = AirC_data[218] });
                    AirC_data[219].Add(new { idx = i, val = (airc_total_elec * 0.00023 + airc_total_gas / 43.1 / 0.277778 * 0.00103).ToString("0.0") });//절감량 전체 
                    data.Add(new { cname = "airc_toe", data = AirC_data[219] });

                    d = (airc_total_saving / Total_Energy_pre * 100);
                    charts += "{donut:" + d + "},";

                    //합산 계 
                    AirC_data[225].Add(new { idx = i, val = AirC_Power.Sum().ToString("0.0") });//냉방 용량 합계  
                    data.Add(new { cname = "airc_power_c_total", data = AirC_data[225] });
                    AirC_data[226].Add(new { idx = i, val = AirC_COP_Old_C_total.ToString("0.0") });//냉방 기존 COP 평균  
                    data.Add(new { cname = "airc_cop_old_c_total", data = AirC_data[226] });
                    AirC_data[227].Add(new { idx = i, val = AirC_COP_New_C_total.ToString("0.0") });//냉방 COP 평균  
                    data.Add(new { cname = "airc_cop_new_c_total", data = AirC_data[227] });
                    AirC_data[228].Add(new { idx = i, val = (airc_total_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });//냉방 절감량 합계  
                    data.Add(new { cname = "airc_saving_c_total", data = AirC_data[228] });
                    d = AirC_Point_C_total;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    AirC_data[229].Add(new { idx = i, val = sp });//냉방 성능수준 평균  
                    data.Add(new { cname = "airc_point_c_total", data = AirC_data[229] });

                    double AirC_Qmax_c = 0; double AirC_ZoneArea = 0;
                    for (int a = 0; a < AirC_Zones_split.Count; a++)
                    {
                        string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호='" + AirC_Zones_split[a].ToString() + "' And 난방_냉방='냉방' and 비이용일_이용일='이용일' and 월='8월'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                        {
                            AirC_Qmax_c += Convert.ToDouble(ZoneValue[0][0]);
                        }
                        ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "존번호='" + AirC_Zones_split[a].ToString() + "'");
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "")
                        {
                            AirC_ZoneArea += Convert.ToDouble(ZoneValue[0][0]);
                        }
                    }
                    AirC_data[231].Add(new { idx = i, val = (AirC_Qmax_c / 1000).ToString("0.0") });//냉방부하 
                    data.Add(new { cname = "airc_qmax_c", data = AirC_data[231] });
                    AirC_data[232].Add(new { idx = i, val = AirC_ZoneArea.ToString("0.0") });//존면적
                    data.Add(new { cname = "airc_zonearea", data = AirC_data[232] });
                    AirC_data[233].Add(new { idx = i, val = AirC_Zones_split.Count.ToString() });//존개수 
                    data.Add(new { cname = "airc_zonecount", data = AirC_data[233] });
                    if (AirC_COP_Rule_C_total > 0)
                    {
                        AirC_data[234].Add(new { idx = i, val = "* DIN V 18599-7 표준 EER : " + AirC_COP_Rule_C_total.ToString("0.0") + " 기준" });//냉방 법규 평균  
                        data.Add(new { cname = "airc_cop_rule_c", data = AirC_data[234] });
                    }

                    #endregion

                    items.Add("Element_ElecHP.htm");
                    s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                    s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
                    System.Text.Json.JsonSerializer.Serialize(EHP_data[10].ToArray());

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