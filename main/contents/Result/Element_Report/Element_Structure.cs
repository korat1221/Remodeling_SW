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
    public partial class Element_Structure : Form
    {
        bool scriptable = false;
        public Element_Structure()
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
            List<object>[] Wall_data = new List<object>[700];
            List<object>[] Roof_data = new List<object>[700];
            List<object>[] Floor_data = new List<object>[700];
            double d;
            string sp;
            int i = -1, n;
            while (++i < 700)
            {
                Wall_data[i] = new List<object>();
                Roof_data[i] = new List<object>();
                Floor_data[i] = new List<object>();
            }
            string charts = "";
            i = -1;
            while (++i < 번호.Length)
            {

                #region 외벽              
                double Total_Energy_pre = 0;
                string[][] value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='외벽'");
                string[][] value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                double wall_saving = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    Total_Energy_pre = Convert.ToDouble(value3[0][0]);
                    wall_saving = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value[0][0]));
                }

                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전기' and 검토유형='외벽'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전기'");
                double wall_saving_elec = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    wall_saving_elec = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value[0][0]));
                }
                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='외벽'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                double wall_saving_noelec = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    wall_saving_noelec = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value[0][0]));
                }

                d = (wall_saving / Total_Energy_pre * 100);
                Wall_data[0].Add(new { idx = i, val = wall_saving.ToString("#,##0") }); ; //절감량 
                Wall_data[1].Add(new { idx = i, val = d.ToString("0.0") + " %" }); ; //절감률
                data.Add(new { cname = "wall_saving", data = Wall_data[0] });
                data.Add(new { cname = "wall_savingpercent", data = Wall_data[1] });
                charts += "{donut:" + d + "},";
                double wall_tCO2_elec = wall_saving_elec * 0.4747 / 1000000 * 1000;
                double wall_TOE_elec = wall_saving_elec * 0.00023;

                double wall_tCO2_noelec = wall_saving_noelec / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                double wall_TOE_noelec = wall_saving_noelec / 43.1 / 0.277778 * 0.00103;
                double wall_tCO2 = wall_tCO2_elec + wall_tCO2_noelec;
                double wall_TOE = wall_TOE_elec + wall_TOE_noelec;
                Wall_data[2].Add(new { idx = i, val = wall_tCO2.ToString("0.0") });  //tco2
                Wall_data[3].Add(new { idx = i, val = wall_TOE.ToString("0.0") });  //TOE 
                data.Add(new { cname = "wall_tco2", data = Wall_data[2] });
                data.Add(new { cname = "wall_toe", data = Wall_data[3] });

                string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "SELECT DISTINCT a.명칭,a.유효열관류율,a.법규열관류율,a.번호,a.단열재두께,a.U적용방법,a.Type From ConstructionWall as a  Inner Join ZoneEnvelope_3D as b  on a.번호 = b.구조체번호  where b.외피유형 ='외벽' Order by a.유효열관류율 DESC");
                string[] wall_num = new string[8]; string[] wall_name = new string[8]; double[] wall_ueff = new double[8]; double[] wall_ueff_law = new double[8]; double[] wall_area = new double[8]; double[] wall_saving_element = new double[8]; string[] wall_feature = new string[8]; string[] wall_retype = new string[8];
                double wall_area_sum = 0; double wall_area_sum_law = 0;
                if (Value.Length > 0)
                {
                    for (int k = 0; k < Value.Length; k++)
                    {
                        wall_name[k] = Value[k][0];
                        wall_retype[k] = Value[k][6];
                        wall_ueff[k] = Convert.ToDouble(Value[k][1]);
                        wall_ueff_law[k] = Convert.ToDouble(Value[k][2]);
                        wall_num[k] = Value[k][3];
                        string[][] valuek = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "외피유형='외벽' And 구조체번호='" + Value[k][3] + "'");
                        if (valuek.Length > 0)
                        {
                            for (int a = 0; a < valuek.Length; a++)
                            { wall_area[k] += Convert.ToDouble(valuek[a][0]); }
                        }

                        if (Value[k][5] == "법규") { wall_feature[k] = "-"; }
                        else
                        {
                            if (Convert.ToDouble(Value[k][4]) > 0)
                            {
                                wall_feature[k] = "단열두께 " + Convert.ToDouble(Value[k][4]).ToString("") + "mm";
                            }
                            else
                            {
                                wall_feature[k] = "미단열";
                            }
                        }
                    }

                    for (int a = 0; a < 8; a++)
                    {
                        Wall_data[4 + a].Add(new { idx = i, val = wall_name[a] });//명칭
                        data.Add(new { cname = "wall_name" + a, data = Wall_data[4 + a] });
                        if (wall_name[a] != null && wall_name[a] != "")
                        {
                            Wall_data[12 + a].Add(new { idx = i, val = wall_area[a].ToString("0.0") });//면적
                            data.Add(new { cname = "wall_area" + a, data = Wall_data[12 + a] });
                            wall_area_sum += wall_area[a];
                            wall_area_sum_law += wall_area[a];
                            Wall_data[20 + a].Add(new { idx = i, val = wall_feature[a] });//특징
                            data.Add(new { cname = "wall_feature" + a, data = Wall_data[20 + a] });
                        }
                    }
                    Wall_data[28].Add(new { idx = i, val = wall_area_sum.ToString("0.0") });//면적합계
                    Wall_data[29].Add(new { idx = i, val = "100 %" });//면적율합계
                    data.Add(new { cname = "wall_area_sum", data = Wall_data[28] });
                    data.Add(new { cname = "wall_area_sum_percent", data = Wall_data[29] });

                    for (int a = 0; a < 8; a++)
                    {
                        if (wall_name[a] != null && wall_name[a] != "")
                        {
                            Wall_data[30 + a].Add(new { idx = i, val = (wall_area[a] / wall_area_sum * 100).ToString("0") + " %" });//면적율
                            data.Add(new { cname = "wall_area_percent" + a, data = Wall_data[30 + a] });

                            if (wall_ueff_law[a] != 0)
                            {
                                wall_saving_element[a] = wall_area[a] / wall_area_sum * (wall_ueff_law[a] - wall_ueff[a]);
                            }
                        }
                    }

                    for (int a = 0; a < 8; a++)
                    {
                        if (wall_name[a] != null && wall_name[a] != "")
                        {
                            Wall_data[38 + a].Add(new { idx = i, val = wall_ueff[a].ToString("0.00") });//계획열관류율
                            data.Add(new { cname = "wall_ueff" + a, data = Wall_data[38 + a] });
                            Wall_data[46 + a].Add(new { idx = i, val = wall_ueff_law[a].ToString("0.00") });
                            data.Add(new { cname = "wall_ueff_law" + a, data = Wall_data[46 + a] });//법규열관류율

                        }
                    }
                    double wall_ueff_avg = 0;
                    double wall_ueff_law_avg = 0;
                    for (int a = 0; a < 8; a++)
                    {
                        wall_ueff_avg += wall_ueff[a] * wall_area[a] / wall_area_sum;
                        wall_ueff_law_avg += wall_ueff_law[a] * wall_area[a] / wall_area_sum_law;
                    }
                    Wall_data[54].Add(new { idx = i, val = wall_ueff_avg.ToString("0.00") });//계획열관류율 평균
                    Wall_data[55].Add(new { idx = i, val = wall_ueff_law_avg.ToString("0.00") });//법규열관류율 평균
                    data.Add(new { cname = "wall_ueff_avg", data = Wall_data[54] });
                    data.Add(new { cname = "wall_ueff_law_avg", data = Wall_data[55] });

                    double wall_law_avg = 0;

                    for (int a = 0; a < 8; a++)
                    {
                        if (wall_name[a] != null && wall_name[a] != "")
                        {
                            string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "법규열관류율", "번호 ='" + wall_num[a] + "'");
                            if (value2.Length > 0)
                            {
                                d = Math.Min(100, (Convert.ToDouble(value2[0][0]) / wall_ueff[a] * 100));
                                if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                                sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                                Wall_data[65 + a].Add(new { idx = i, val = sp });//법규대비 성능점수
                                data.Add(new { cname = "wall_law_point" + a, data = Wall_data[65 + a] });
                                wall_law_avg += Convert.ToDouble(value2[0][0]) * wall_area[a] / wall_area_sum;
                            }
                        }
                    }
                    d = Math.Min(100, (wall_law_avg / wall_ueff_avg * 100));
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";

                    Wall_data[73].Add(new { idx = i, val = sp });//법규대비 성능점수 평균
                    data.Add(new { cname = "wall_law_point_avg", data = Wall_data[73] });

                    double east = 0, west = 0, south = 0, north = 0;
                    string[][] area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='동' and 외피유형='외벽'");
                    if (area.Length > 0 && area[0][0] != "")
                    {
                        east = Convert.ToDouble(area[0][0]);
                    }
                    area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='서' and 외피유형='외벽'");
                    if (area.Length > 0 && area[0][0] != "")
                    {
                        west = Convert.ToDouble(area[0][0]);
                    }
                    area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='남' or 방위 ='남동' or 방위 ='남서') and 외피유형='외벽'");
                    if (area.Length > 0 && area[0][0] != "")
                    {
                        south = Convert.ToDouble(area[0][0]);
                    }
                    area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='북' or 방위 ='북동' or 방위 ='북서')and 외피유형='외벽'");
                    if (area.Length > 0 && area[0][0] != "")
                    {
                        north = Convert.ToDouble(area[0][0]);
                    }
                    Wall_data[74].Add(new { idx = i, val = east.ToString("0.0") });
                    data.Add(new { cname = "wall_east", data = Wall_data[74] });
                    Wall_data[75].Add(new { idx = i, val = west.ToString("0.0") });
                    data.Add(new { cname = "wall_west", data = Wall_data[75] });
                    Wall_data[76].Add(new { idx = i, val = south.ToString("0.0") });
                    data.Add(new { cname = "wall_south", data = Wall_data[76] });
                    Wall_data[77].Add(new { idx = i, val = north.ToString("0.0") });
                    data.Add(new { cname = "wall_north", data = Wall_data[77] });
                }

                #endregion

                #region 지붕
                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='지붕'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                double roof_saving = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    roof_saving = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value[0][0]));
                }

                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전기' and 검토유형='지붕'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전기'");
                double roof_saving_elec = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    roof_saving_elec = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value[0][0]));
                }
                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='지붕'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                double roof_saving_noelec = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    roof_saving_noelec = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value[0][0]));
                }

                d = (roof_saving / Total_Energy_pre * 100);
                Roof_data[0].Add(new { idx = i, val = roof_saving.ToString("#,##0") }); ; //절감량 
                Roof_data[1].Add(new { idx = i, val = d.ToString("0.0") + " %" }); ; //절감률
                data.Add(new { cname = "roof_saving", data = Roof_data[0] });
                data.Add(new { cname = "roof_savingpercent", data = Roof_data[1] });
                charts += "{donut:" + d + "},";
                double roof_tCO2_elec = roof_saving_elec * 0.4747 / 1000000 * 1000;
                double roof_TOE_elec = roof_saving_elec * 0.00023;
                double roof_tCO2_noelec = roof_saving_noelec / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                double roof_TOE_noelec = roof_saving_noelec / 43.1 / 0.277778 * 0.00103;
                double roof_tCO2 = roof_tCO2_elec + roof_tCO2_noelec;
                double roof_TOE = roof_TOE_elec + roof_TOE_noelec;
                Roof_data[2].Add(new { idx = i, val = roof_tCO2.ToString("0.0") });  //tco2
                Roof_data[3].Add(new { idx = i, val = roof_TOE.ToString("0.0") });  //TOE 
                data.Add(new { cname = "roof_tco2", data = Roof_data[2] });
                data.Add(new { cname = "roof_toe", data = Roof_data[3] });

                Value = Program.DB.querySQL(DB.type.ProjDB, "SELECT DISTINCT a.명칭,a.유효열관류율,a.법규열관류율,a.번호,a.단열재두께,a.U적용방법,a.Type  From ConstructionRoof as a  Inner Join ZoneEnvelope_3D as b  on a.번호 = b.구조체번호  where b.외피유형 ='지붕' Order by a.유효열관류율 DESC");
                string[] roof_num = new string[8]; string[] roof_name = new string[8]; double[] roof_ueff = new double[8]; double[] roof_ueff_law = new double[8]; double[] roof_area = new double[8]; double[] roof_saving_element = new double[8]; string[] roof_feature = new string[8];
                double roof_area_sum = 0; string[] roof_retype = new string[8]; double roof_area_sum_law = 0;
                if (Value.Length > 0)
                {
                    for (int k = 0; k < Value.Length; k++)
                    {
                        roof_name[k] = Value[k][0];
                        roof_retype[k] = Value[k][6];
                        roof_ueff[k] = Convert.ToDouble(Value[k][1]);
                        roof_ueff_law[k] = Convert.ToDouble(Value[k][2]);

                        roof_num[k] = Value[k][3];
                        string[][] valuek = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "외피유형='지붕' And 구조체번호='" + Value[k][3] + "'");
                        if (valuek.Length > 0)
                        {
                            for (int a = 0; a < valuek.Length; a++)
                            { roof_area[k] += Convert.ToDouble(valuek[a][0]); }
                        }

                        if (Value[k][5] == "법규") { roof_feature[k] = "-"; }
                        else
                        {
                            if (Convert.ToDouble(Value[k][4]) > 0)
                            {
                                roof_feature[k] = "단열두께 " + Convert.ToDouble(Value[k][4]).ToString("") + "mm";
                            }
                            else
                            {
                                roof_feature[k] = "미단열";
                            }
                        }
                    }

                    for (int a = 0; a < 8; a++)
                    {
                        Roof_data[4 + a].Add(new { idx = i, val = roof_name[a] });//명칭
                        data.Add(new { cname = "roof_name" + a, data = Roof_data[4 + a] });
                        if (roof_name[a] != null && roof_name[a] != "")
                        {
                            Roof_data[12 + a].Add(new { idx = i, val = roof_area[a].ToString("0.0") });//면적
                            data.Add(new { cname = "roof_area" + a, data = Roof_data[12 + a] });
                            roof_area_sum += roof_area[a];
                            roof_area_sum_law += roof_area[a];
                            Roof_data[20 + a].Add(new { idx = i, val = roof_feature[a] });//특징
                            data.Add(new { cname = "roof_feature" + a, data = Roof_data[20 + a] });
                        }
                    }
                    Roof_data[28].Add(new { idx = i, val = roof_area_sum.ToString("0.0") });//면적합계
                    Roof_data[29].Add(new { idx = i, val = "100 %" });//면적율합계
                    data.Add(new { cname = "roof_area_sum", data = Roof_data[28] });
                    data.Add(new { cname = "roof_area_sum_percent", data = Roof_data[29] });

                    for (int a = 0; a < 8; a++)
                    {
                        if (roof_name[a] != null && roof_name[a] != "")
                        {
                            Roof_data[30 + a].Add(new { idx = i, val = (roof_area[a] / roof_area_sum * 100).ToString("0") + " %" });//면적율
                            data.Add(new { cname = "roof_area_percent" + a, data = Roof_data[30 + a] });

                            if (roof_ueff_law[a] != 0)
                            {
                                roof_saving_element[a] = roof_area[a] / roof_area_sum * (roof_ueff_law[a] - roof_ueff[a]);
                            }
                        }
                    }

                    for (int a = 0; a < 8; a++)
                    {
                        if (roof_name[a] != null && roof_name[a] != "")
                        {
                            Roof_data[38 + a].Add(new { idx = i, val = roof_ueff[a].ToString("0.00") });//계획열관류율
                            data.Add(new { cname = "roof_ueff" + a, data = Roof_data[38 + a] });
                            Roof_data[46 + a].Add(new { idx = i, val = roof_ueff_law[a].ToString("0.00") });
                            data.Add(new { cname = "roof_ueff_law" + a, data = Roof_data[46 + a] });//법규열관류율
                        }
                    }
                    double roof_ueff_avg = 0;
                    double roof_ueff_law_avg = 0;
                    for (int a = 0; a < 8; a++)
                    {
                        roof_ueff_avg += roof_ueff[a] * roof_area[a] / roof_area_sum;
                        roof_ueff_law_avg += roof_ueff_law[a] * roof_area[a] / roof_area_sum_law;
                    }
                    Roof_data[54].Add(new { idx = i, val = roof_ueff_avg.ToString("0.00") });//계획열관류율 평균
                    Roof_data[55].Add(new { idx = i, val = roof_ueff_law_avg.ToString("0.00") });//법규열관류율 평균
                    data.Add(new { cname = "roof_ueff_avg", data = Roof_data[54] });
                    data.Add(new { cname = "roof_ueff_law_avg", data = Roof_data[55] });

                    double roof_law_avg = 0;
                    for (int a = 0; a < 8; a++)
                    {
                        if (roof_name[a] != null && roof_name[a] != "")
                        {
                            string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "법규열관류율", "번호 ='" + roof_num[a] + "'");
                            if (value2.Length > 0)
                            {
                                d = Math.Min(100, (Convert.ToDouble(value2[0][0]) / roof_ueff[a] * 100));
                                if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                                sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";

                                Roof_data[65 + a].Add(new { idx = i, val = sp });//법규대비 성능점수
                                data.Add(new { cname = "roof_law_point" + a, data = Roof_data[65 + a] });
                                roof_law_avg += Convert.ToDouble(value2[0][0]) * roof_area[a] / roof_area_sum;
                            }
                        }
                    }
                    d = Math.Min(100, (roof_law_avg / roof_ueff_avg * 100));
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    Roof_data[73].Add(new { idx = i, val = sp });//법규대비 성능점수 평균
                    data.Add(new { cname = "roof_law_point_avg", data = Roof_data[73] });

                    double east = 0, west = 0, south = 0, north = 0, horizontal = 0;
                    string[][] area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='동' and 외피유형='지붕'");
                    if (area.Length > 0 && area[0][0] != "")
                    {
                        east = Convert.ToDouble(area[0][0]);
                    }
                    area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='서' and 외피유형='지붕'");
                    if (area.Length > 0 && area[0][0] != "")
                    {
                        west = Convert.ToDouble(area[0][0]);
                    }
                    area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='남' or 방위 ='남동' or 방위 ='남서') and 외피유형='지붕'");
                    if (area.Length > 0 && area[0][0] != "")
                    {
                        south = Convert.ToDouble(area[0][0]);
                    }
                    area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='북' or 방위 ='북동' or 방위 ='북서')and 외피유형='지붕'");
                    if (area.Length > 0 && area[0][0] != "")
                    {
                        north = Convert.ToDouble(area[0][0]);
                    }
                    area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='수평' and 외피유형='지붕'");
                    if (area.Length > 0 && area[0][0] != "")
                    {
                        horizontal = Convert.ToDouble(area[0][0]);
                    }
                    Roof_data[74].Add(new { idx = i, val = east.ToString("0.0") });
                    data.Add(new { cname = "roof_east", data = Roof_data[74] });
                    Roof_data[75].Add(new { idx = i, val = west.ToString("0.0") });
                    data.Add(new { cname = "roof_west", data = Roof_data[75] });
                    Roof_data[76].Add(new { idx = i, val = south.ToString("0.0") });
                    data.Add(new { cname = "roof_south", data = Roof_data[76] });
                    Roof_data[77].Add(new { idx = i, val = north.ToString("0.0") });
                    data.Add(new { cname = "roof_north", data = Roof_data[77] });
                    Roof_data[78].Add(new { idx = i, val = horizontal.ToString("0.0") });
                    data.Add(new { cname = "roof_horizontal", data = Roof_data[78] });

                }

                #endregion

                #region 최하층바닥
                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='최하층바닥'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                double floor_saving = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    floor_saving = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value[0][0]));
                }

                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전기' and 검토유형='최하층바닥'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전기'");
                double floor_saving_elec = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    floor_saving_elec = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value[0][0]));
                }
                value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='최하층바닥'");
                value3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                double floor_saving_noelec = 0;
                if (value.Length > 0 && value3.Length > 0)
                {
                    floor_saving_noelec = Math.Max(0, Convert.ToDouble(value3[0][0]) - Convert.ToDouble(value[0][0]));
                }
                d = (floor_saving / Total_Energy_pre * 100);
                Floor_data[0].Add(new { idx = i, val = floor_saving.ToString("#,##0") }); ; //절감량 
                Floor_data[1].Add(new { idx = i, val = d.ToString("0.0") + " %" }); ; //절감률
                data.Add(new { cname = "floor_saving", data = Floor_data[0] });
                data.Add(new { cname = "floor_savingpercent", data = Floor_data[1] });
                charts += "{donut:" + d + "},";
                double floor_tCO2_elec = floor_saving_elec * 0.4747 / 1000000 * 1000;
                double floor_TOE_elec = floor_saving_elec * 0.00023;
                double floor_tCO2_noelec = floor_saving_noelec / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                double floor_TOE_noelec = floor_saving_noelec / 43.1 / 0.277778 * 0.00103;
                double floor_tCO2 = floor_tCO2_elec + floor_tCO2_noelec;
                double floor_TOE = floor_TOE_elec + floor_TOE_noelec;
                Floor_data[2].Add(new { idx = i, val = floor_tCO2.ToString("0.0") });  //tco2
                Floor_data[3].Add(new { idx = i, val = floor_TOE.ToString("0.0") });  //TOE 
                data.Add(new { cname = "floor_tco2", data = Floor_data[2] });
                data.Add(new { cname = "floor_toe", data = Floor_data[3] });

                Value = Program.DB.querySQL(DB.type.ProjDB, "SELECT DISTINCT a.명칭,a.유효열관류율,a.법규열관류율,a.번호,a.단열재두께,a.U적용방법,a.Type  From ConstructionFloor as a  Inner Join ZoneEnvelope_3D as b  on a.번호 = b.구조체번호  where b.외피유형 ='최하층바닥' Order by a.유효열관류율 DESC");
                string[] floor_num = new string[8]; string[] floor_name = new string[8]; double[] floor_ueff = new double[8]; double[] floor_ueff_law = new double[8]; double[] floor_area = new double[8]; double[] floor_saving_element = new double[8]; string[] floor_feature = new string[8];
                double floor_area_sum = 0; string[] floor_retype = new string[8]; double floor_area_sum_law = 0;
                if (Value.Length > 0)
                {
                    for (int k = 0; k < Value.Length; k++)
                    {
                        floor_name[k] = Value[k][0];
                        floor_retype[k] = Value[k][6];
                        floor_ueff[k] = Convert.ToDouble(Value[k][1]);
                        floor_ueff_law[k] = Convert.ToDouble(Value[k][2]);

                        floor_num[k] = Value[k][3];
                        string[][] valuek = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "외피유형='최하층바닥' And 구조체번호='" + Value[k][3] + "'");
                        if (valuek.Length > 0)
                        {
                            for (int a = 0; a < valuek.Length; a++)
                            { floor_area[k] += Convert.ToDouble(valuek[a][0]); }
                        }
                        if (Value[k][5] == "법규") { floor_feature[k] = "-"; }
                        else
                        {
                            if (Convert.ToDouble(Value[k][4]) > 0)
                            {
                                floor_feature[k] = "단열두께 " + Convert.ToDouble(Value[k][4]).ToString("") + "mm";
                            }
                            else
                            {
                                floor_feature[k] = "미단열";
                            }
                        }
                    }

                    for (int a = 0; a < 8; a++)
                    {
                        Floor_data[4 + a].Add(new { idx = i, val = floor_name[a] });//명칭
                        data.Add(new { cname = "floor_name" + a, data = Floor_data[4 + a] });
                        if (floor_name[a] != null && floor_name[a] != "")
                        {
                            Floor_data[12 + a].Add(new { idx = i, val = floor_area[a].ToString("0.0") });//면적
                            data.Add(new { cname = "floor_area" + a, data = Floor_data[12 + a] });
                            floor_area_sum += floor_area[a];
                            floor_area_sum_law += floor_area[a];
                            Floor_data[20 + a].Add(new { idx = i, val = floor_feature[a] });//특징
                            data.Add(new { cname = "floor_feature" + a, data = Floor_data[20 + a] });
                        }
                    }
                    Floor_data[28].Add(new { idx = i, val = floor_area_sum.ToString("0.0") });//면적합계
                    Floor_data[29].Add(new { idx = i, val = "100 %" });//면적율합계
                    data.Add(new { cname = "floor_area_sum", data = Floor_data[28] });
                    data.Add(new { cname = "floor_area_sum_percent", data = Floor_data[29] });

                    for (int a = 0; a < 8; a++)
                    {
                        if (floor_name[a] != null && floor_name[a] != "")
                        {
                            Floor_data[30 + a].Add(new { idx = i, val = (floor_area[a] / floor_area_sum * 100).ToString("0") + " %" });//면적율
                            data.Add(new { cname = "floor_area_percent" + a, data = Floor_data[30 + a] });

                            if (floor_ueff_law[a] != 0)
                            {
                                floor_saving_element[a] = floor_area[a] / floor_area_sum * (floor_ueff_law[a] - floor_ueff[a]);
                            }
                        }
                    }

                    for (int a = 0; a < 8; a++)
                    {
                        if (floor_name[a] != null && floor_name[a] != "")
                        {
                            Floor_data[38 + a].Add(new { idx = i, val = floor_ueff[a].ToString("0.00") });//계획열관류율
                            data.Add(new { cname = "floor_ueff" + a, data = Floor_data[38 + a] });
                            Floor_data[46 + a].Add(new { idx = i, val = floor_ueff_law[a].ToString("0.00") });
                            data.Add(new { cname = "floor_ueff_law" + a, data = Floor_data[46 + a] }); //법규열관류율
                        }
                    }
                    double floor_ueff_avg = 0;
                    double floor_ueff_law_avg = 0;
                    for (int a = 0; a < 8; a++)
                    {
                        floor_ueff_avg += floor_ueff[a] * floor_area[a] / floor_area_sum;
                        floor_ueff_law_avg += floor_ueff_law[a] * floor_area[a] / floor_area_sum_law;
                    }
                    Floor_data[54].Add(new { idx = i, val = floor_ueff_avg.ToString("0.00") });//계획열관류율 평균
                    Floor_data[55].Add(new { idx = i, val = floor_ueff_law_avg.ToString("0.00") });//법규열관류율 평균
                    data.Add(new { cname = "floor_ueff_avg", data = Floor_data[54] });
                    data.Add(new { cname = "floor_ueff_law_avg", data = Floor_data[55] });

                    double floor_law_avg = 0;
                    for (int a = 0; a < 8; a++)
                    {
                        if (floor_name[a] != null && floor_name[a] != "")
                        {
                            string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "ConstructionFloor", "법규열관류율", "번호 ='" + floor_num[a] + "'");
                            if (value2.Length > 0)
                            {
                                d = Math.Min(100, (Convert.ToDouble(value2[0][0]) / floor_ueff[a] * 100));
                                if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                                sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                                Floor_data[65 + a].Add(new { idx = i, val = sp });//법규대비 성능점수
                                data.Add(new { cname = "floor_law_point" + a, data = Floor_data[65 + a] });
                                floor_law_avg += Convert.ToDouble(value2[0][0]) * floor_area[a] / floor_area_sum;
                            }
                        }
                    }
                    d = Math.Min(100, (floor_law_avg / floor_ueff_avg * 100));
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    Floor_data[73].Add(new { idx = i, val = sp });//법규대비 성능점수 평균
                    data.Add(new { cname = "floor_law_point_avg", data = Floor_data[73] });

                    double east = 0, west = 0, south = 0, north = 0;
                    string[][] area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(a.면적) From ZoneEnvelope_3D as a  Inner Join ConstructionFloor as b on a.구조체번호 = b.번호 where a.외피유형='최하층바닥' and  b.기초설치 ='지면위'");
                    if (area.Length > 0 && area[0][0] != "")
                    {
                        east = Convert.ToDouble(area[0][0]);
                    }
                    area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(a.면적) From ZoneEnvelope_3D as a  Inner Join ConstructionFloor as b on a.구조체번호 = b.번호 where a.외피유형='최하층바닥' and  b.기초설치 ='단열지하실'");
                    if (area.Length > 0 && area[0][0] != "")
                    {
                        west = Convert.ToDouble(area[0][0]);
                    }
                    area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(a.면적) From ZoneEnvelope_3D as a  Inner Join ConstructionFloor as b on a.구조체번호 = b.번호 where a.외피유형='최하층바닥' and  b.기초설치 ='비단열지하실'");
                    if (area.Length > 0 && area[0][0] != "")
                    {
                        south = Convert.ToDouble(area[0][0]);
                    }
                    area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(a.면적) From ZoneEnvelope_3D as a  Inner Join ConstructionFloor as b on a.구조체번호 = b.번호 where a.외피유형='최하층바닥' and  b.기초설치 ='바닥(외기)'");
                    if (area.Length > 0 && area[0][0] != "")
                    {
                        north = Convert.ToDouble(area[0][0]);
                    }
                    Floor_data[74].Add(new { idx = i, val = east.ToString("0.0") });
                    data.Add(new { cname = "floor_east", data = Floor_data[74] });
                    Floor_data[75].Add(new { idx = i, val = west.ToString("0.0") });
                    data.Add(new { cname = "floor_west", data = Floor_data[75] });
                    Floor_data[76].Add(new { idx = i, val = south.ToString("0.0") });
                    data.Add(new { cname = "floor_south", data = Floor_data[76] });
                    Floor_data[77].Add(new { idx = i, val = north.ToString("0.0") });
                    data.Add(new { cname = "floor_north", data = Floor_data[77] });

                }

                #endregion


                items.Add("Element_structure2.htm");
                s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
                System.Text.Json.JsonSerializer.Serialize(Wall_data[10].ToArray());

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

            string script = null;
            string s, s2;
            string[][] 번호 = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
            string[][] res = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기존프로젝트");
            List<object> items = new List<object>();
            List<object> items2 = new List<object>();
            List<object> data = new List<object>();
            List<object>[] Wall_data = new List<object>[700];
            List<object>[] Roof_data = new List<object>[700];
            List<object>[] Floor_data = new List<object>[700];
            double d;
            string sp;
            int i = -1, n;
            while (++i < 700)
            {
                Wall_data[i] = new List<object>();
                Roof_data[i] = new List<object>();
                Floor_data[i] = new List<object>();
            }
            string charts = "";
            i = -1;
            while (++i < 번호.Length)
            {
                if (res.Length > 0)
                {
                    #region 외벽                                
                    int j_외벽 = 0;
                    for (int a = 0; a < ElementAlt.Length; a++)
                    {
                        if (ElementAlt[a] == "외벽")
                        {
                            j_외벽 = a; break;
                        }
                    }

                    double wall_saving = Math.Max(Element_EnergySaving[j_외벽], 0);

                    d = (wall_saving / Total_Energy_pre * 100);
                    Wall_data[0].Add(new { idx = i, val = wall_saving.ToString("#,##0") }); ; //절감량 
                    Wall_data[1].Add(new { idx = i, val = d.ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "wall_saving", data = Wall_data[0] });
                    data.Add(new { cname = "wall_savingpercent", data = Wall_data[1] });

                    charts += "{donut:" + d + "},";

                    double wall_saving_elec = Math.Max(Element_ElecSaving[j_외벽], 0);
                    double wall_saving_noelec = Math.Max(Element_GasSaving[j_외벽], 0);

                    double wall_tCO2_elec = wall_saving_elec * 0.4747 / 1000000 * 1000;
                    double wall_TOE_elec = wall_saving_elec * 0.00023;

                    double wall_tCO2_noelec = wall_saving_noelec / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    double wall_TOE_noelec = wall_saving_noelec / 43.1 / 0.277778 * 0.00103;
                    double wall_tCO2 = wall_tCO2_elec + wall_tCO2_noelec;
                    double wall_TOE = wall_TOE_elec + wall_TOE_noelec;
                    Wall_data[2].Add(new { idx = i, val = wall_tCO2.ToString("0.0") });  //tco2
                    Wall_data[3].Add(new { idx = i, val = wall_TOE.ToString("0.0") });  //TOE 
                    data.Add(new { cname = "wall_tco2", data = Wall_data[2] });
                    data.Add(new { cname = "wall_toe", data = Wall_data[3] });

                    string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "SELECT DISTINCT a.명칭,a.유효열관류율,a.기존외벽,a.번호,a.단열재두께,a.U적용방법,a.Type From ConstructionWall as a  Inner Join ZoneEnvelope_3D as b  on a.번호 = b.구조체번호  where b.외피유형 ='외벽' Order by a.유효열관류율 DESC");
                    string[] wall_num = new string[8]; string[] wall_name = new string[8]; double[] wall_ueff = new double[8]; double[] wall_ueff_old = new double[8]; double[] wall_area = new double[8]; double[] wall_saving_element = new double[8]; string[] wall_feature = new string[8]; string[] wall_retype = new string[8];
                    double wall_area_sum = 0; double wall_area_sum_old = 0;
                    if (Value.Length > 0)
                    {
                        for (int k = 0; k < Value.Length; k++)
                        {
                            wall_name[k] = Value[k][0];
                            wall_retype[k] = Value[k][6];
                            wall_ueff[k] = Convert.ToDouble(Value[k][1]);
                            if (wall_retype[k] != "신규")
                            {
                                if (Value[k][2] != "")
                                {
                                    string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "유효열관류율", "명칭 ='" + Value[k][2] + "'");
                                    if (value2.Length > 0)
                                    {
                                        wall_ueff_old[k] = Convert.ToDouble(value2[0][0]);
                                    }
                                }
                                else
                                {
                                    wall_ueff_old[k] = wall_ueff[k];
                                }
                            }

                            wall_num[k] = Value[k][3];
                            string[][] valuek = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "외피유형='외벽' And 구조체번호='" + Value[k][3] + "'");
                            if (valuek.Length > 0)
                            {
                                for (int a = 0; a < valuek.Length; a++)
                                { wall_area[k] += Convert.ToDouble(valuek[a][0]); }
                            }

                            if (Value[k][5] == "법규") { wall_feature[k] = "-"; }
                            else
                            {
                                if (Convert.ToDouble(Value[k][4]) > 0)
                                {
                                    wall_feature[k] = "단열두께 " + Convert.ToDouble(Value[k][4]).ToString("") + "mm";
                                }
                                else
                                {
                                    wall_feature[k] = "미단열";
                                }
                            }
                        }

                        for (int a = 0; a < 8; a++)
                        {
                            Wall_data[4 + a].Add(new { idx = i, val = wall_name[a] });//명칭
                            data.Add(new { cname = "wall_name" + a, data = Wall_data[4 + a] });
                            if (wall_name[a] != null && wall_name[a] != "")
                            {
                                Wall_data[12 + a].Add(new { idx = i, val = wall_area[a].ToString("0.0") });//면적
                                data.Add(new { cname = "wall_area" + a, data = Wall_data[12 + a] });
                                wall_area_sum += wall_area[a];
                                if (wall_retype[a] != "신규") { wall_area_sum_old += wall_area[a]; }
                                Wall_data[20 + a].Add(new { idx = i, val = wall_feature[a] });//특징
                                data.Add(new { cname = "wall_feature" + a, data = Wall_data[20 + a] });
                            }
                        }
                        Wall_data[28].Add(new { idx = i, val = wall_area_sum.ToString("0.0") });//면적합계
                        Wall_data[29].Add(new { idx = i, val = "100 %" });//면적율합계
                        data.Add(new { cname = "wall_area_sum", data = Wall_data[28] });
                        data.Add(new { cname = "wall_area_sum_percent", data = Wall_data[29] });

                        for (int a = 0; a < 8; a++)
                        {
                            if (wall_name[a] != null && wall_name[a] != "")
                            {
                                Wall_data[30 + a].Add(new { idx = i, val = (wall_area[a] / wall_area_sum * 100).ToString("0") + " %" });//면적율
                                data.Add(new { cname = "wall_area_percent" + a, data = Wall_data[30 + a] });

                                if (wall_ueff_old[a] != 0)
                                {
                                    wall_saving_element[a] = wall_area[a] / wall_area_sum * (wall_ueff_old[a] - wall_ueff[a]);
                                }
                            }
                        }

                        for (int a = 0; a < 8; a++)
                        {
                            if (wall_name[a] != null && wall_name[a] != "")
                            {
                                Wall_data[38 + a].Add(new { idx = i, val = wall_ueff[a].ToString("0.00") });//계획열관류율
                                data.Add(new { cname = "wall_ueff" + a, data = Wall_data[38 + a] });
                                if (wall_retype[a] != "신규")
                                { Wall_data[46 + a].Add(new { idx = i, val = wall_ueff_old[a].ToString("0.00") }); }
                                else { Wall_data[46 + a].Add(new { idx = i, val = "-" }); }
                                data.Add(new { cname = "wall_ueff_old" + a, data = Wall_data[46 + a] });//기존열관류율

                            }
                        }
                        double wall_ueff_avg = 0;
                        double wall_ueff_old_avg = 0;
                        for (int a = 0; a < 8; a++)
                        {
                            wall_ueff_avg += wall_ueff[a] * wall_area[a] / wall_area_sum;
                            wall_ueff_old_avg += wall_ueff_old[a] * wall_area[a] / wall_area_sum_old;
                        }
                        Wall_data[54].Add(new { idx = i, val = wall_ueff_avg.ToString("0.00") });//계획열관류율 평균
                        Wall_data[55].Add(new { idx = i, val = wall_ueff_old_avg.ToString("0.00") });//기존열관류율 평균
                        data.Add(new { cname = "wall_ueff_avg", data = Wall_data[54] });
                        data.Add(new { cname = "wall_ueff_old_avg", data = Wall_data[55] });

                        double sum = 0;
                        for (int a = 0; a < 8; a++)
                        {
                            sum += wall_saving_element[a];
                        }
                        for (int a = 0; a < 8; a++)
                        {
                            if (wall_name[a] != null && wall_name[a] != "")
                            {
                                if (sum != 0)
                                {
                                    Wall_data[56 + a].Add(new { idx = i, val = ((wall_saving / Total_Energy_pre) * (wall_saving_element[a] / sum) * 100).ToString("0.0") + " %" });//요소기술별 에너지절감률
                                }
                                else
                                {
                                    Wall_data[56 + a].Add(new { idx = i, val = (0).ToString("0.0") + " %" });//요소기술별 에너지절감률
                                }
                                data.Add(new { cname = "wall_saving_element" + a, data = Wall_data[56 + a] });
                            }
                        }
                        Wall_data[64].Add(new { idx = i, val = (wall_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });  //요소기술별 절감률 합계
                        data.Add(new { cname = "wall_saving_element_sum", data = Wall_data[64] });

                        double wall_law_avg = 0;

                        for (int a = 0; a < 8; a++)
                        {
                            if (wall_name[a] != null && wall_name[a] != "")
                            {
                                string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "법규열관류율", "번호 ='" + wall_num[a] + "'");
                                if (value2.Length > 0)
                                {
                                    d = Math.Min(100, (Convert.ToDouble(value2[0][0]) / wall_ueff[a] * 100));
                                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                                    Wall_data[65 + a].Add(new { idx = i, val = sp });//법규대비 성능점수
                                    data.Add(new { cname = "wall_law_point" + a, data = Wall_data[65 + a] });
                                    wall_law_avg += Convert.ToDouble(value2[0][0]) * wall_area[a] / wall_area_sum;
                                }
                            }
                        }
                        d = Math.Min(100, (wall_law_avg / wall_ueff_avg * 100));
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";

                        Wall_data[73].Add(new { idx = i, val = sp });//법규대비 성능점수 평균
                        data.Add(new { cname = "wall_law_point_avg", data = Wall_data[73] });

                        double east = 0, west = 0, south = 0, north = 0;
                        string[][] area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='동' and 외피유형='외벽'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            east = Convert.ToDouble(area[0][0]);
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='서' and 외피유형='외벽'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            west = Convert.ToDouble(area[0][0]);
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='남' or 방위 ='남동' or 방위 ='남서') and 외피유형='외벽'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            south = Convert.ToDouble(area[0][0]);
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='북' or 방위 ='북동' or 방위 ='북서')and 외피유형='외벽'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            north = Convert.ToDouble(area[0][0]);
                        }
                        Wall_data[74].Add(new { idx = i, val = east.ToString("0.0") });
                        data.Add(new { cname = "wall_east", data = Wall_data[74] });
                        Wall_data[75].Add(new { idx = i, val = west.ToString("0.0") });
                        data.Add(new { cname = "wall_west", data = Wall_data[75] });
                        Wall_data[76].Add(new { idx = i, val = south.ToString("0.0") });
                        data.Add(new { cname = "wall_south", data = Wall_data[76] });
                        Wall_data[77].Add(new { idx = i, val = north.ToString("0.0") });
                        data.Add(new { cname = "wall_north", data = Wall_data[77] });
                    }

                    #endregion

                    #region 지붕
                    int j_지붕 = 0;
                    for (int a = 0; a < ElementAlt.Length; a++)
                    {
                        if (ElementAlt[a] == "지붕")
                        {
                            j_지붕 = a; break;
                        }
                    }
                    double roof_saving = Math.Max(Element_EnergySaving[j_지붕], 0);

                    d = (roof_saving / Total_Energy_pre * 100);

                    Roof_data[0].Add(new { idx = i, val = roof_saving.ToString("#,##0") }); ; //절감량 
                    Roof_data[1].Add(new { idx = i, val = d.ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "roof_saving", data = Roof_data[0] });
                    data.Add(new { cname = "roof_savingpercent", data = Roof_data[1] });

                    charts += "{donut:" + d + "},";

                    double roof_saving_elec = Math.Max(Element_ElecSaving[j_지붕], 0);
                    double roof_saving_noelec = Math.Max(Element_GasSaving[j_지붕], 0);

                    double roof_tCO2_elec = roof_saving_elec * 0.4747 / 1000000 * 1000;
                    double roof_TOE_elec = roof_saving_elec * 0.00023;

                    double roof_tCO2_noelec = roof_saving_noelec / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    double roof_TOE_noelec = roof_saving_noelec / 43.1 / 0.277778 * 0.00103;
                    double roof_tCO2 = roof_tCO2_elec + roof_tCO2_noelec;
                    double roof_TOE = roof_TOE_elec + roof_TOE_noelec;
                    Roof_data[2].Add(new { idx = i, val = roof_tCO2.ToString("0.0") });  //tco2
                    Roof_data[3].Add(new { idx = i, val = roof_TOE.ToString("0.0") });  //TOE 
                    data.Add(new { cname = "roof_tco2", data = Roof_data[2] });
                    data.Add(new { cname = "roof_toe", data = Roof_data[3] });

                    Value = Program.DB.querySQL(DB.type.ProjDB, "SELECT DISTINCT a.명칭,a.유효열관류율,a.기존지붕,a.번호,a.단열재두께,a.U적용방법,a.Type  From ConstructionRoof as a  Inner Join ZoneEnvelope_3D as b  on a.번호 = b.구조체번호  where b.외피유형 ='지붕' Order by a.유효열관류율 DESC");
                    string[] roof_num = new string[8]; string[] roof_name = new string[8]; double[] roof_ueff = new double[8]; double[] roof_ueff_old = new double[8]; double[] roof_area = new double[8]; double[] roof_saving_element = new double[8]; string[] roof_feature = new string[8];
                    double roof_area_sum = 0; string[] roof_retype = new string[8]; double roof_area_sum_old = 0;
                    if (Value.Length > 0)
                    {
                        for (int k = 0; k < Value.Length; k++)
                        {
                            roof_name[k] = Value[k][0];
                            roof_retype[k] = Value[k][6];
                            roof_ueff[k] = Convert.ToDouble(Value[k][1]);
                            if (roof_retype[k] != "신규")
                            {
                                if (Value[k][2] != "")
                                {
                                    string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "유효열관류율", "명칭 ='" + Value[k][2] + "'");
                                    if (value2.Length > 0)
                                    {
                                        roof_ueff_old[k] = Convert.ToDouble(value2[0][0]);
                                    }
                                }
                                else
                                {
                                    roof_ueff_old[k] = roof_ueff[k];
                                }
                            }

                            roof_num[k] = Value[k][3];
                            string[][] valuek = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "외피유형='지붕' And 구조체번호='" + Value[k][3] + "'");
                            if (valuek.Length > 0)
                            {
                                for (int a = 0; a < valuek.Length; a++)
                                { roof_area[k] += Convert.ToDouble(valuek[a][0]); }
                            }

                            if (Value[k][5] == "법규") { roof_feature[k] = "-"; }
                            else
                            {
                                if (Convert.ToDouble(Value[k][4]) > 0)
                                {
                                    roof_feature[k] = "단열두께 " + Convert.ToDouble(Value[k][4]).ToString("") + "mm";
                                }
                                else
                                {
                                    roof_feature[k] = "미단열";
                                }
                            }
                        }

                        for (int a = 0; a < 8; a++)
                        {
                            Roof_data[4 + a].Add(new { idx = i, val = roof_name[a] });//명칭
                            data.Add(new { cname = "roof_name" + a, data = Roof_data[4 + a] });
                            if (roof_name[a] != null && roof_name[a] != "")
                            {
                                Roof_data[12 + a].Add(new { idx = i, val = roof_area[a].ToString("0.0") });//면적
                                data.Add(new { cname = "roof_area" + a, data = Roof_data[12 + a] });
                                roof_area_sum += roof_area[a];
                                if (roof_retype[a] != "신규") { roof_area_sum_old += roof_area[a]; }

                                Roof_data[20 + a].Add(new { idx = i, val = roof_feature[a] });//특징
                                data.Add(new { cname = "roof_feature" + a, data = Roof_data[20 + a] });
                            }
                        }
                        Roof_data[28].Add(new { idx = i, val = roof_area_sum.ToString("0.0") });//면적합계
                        Roof_data[29].Add(new { idx = i, val = "100 %" });//면적율합계
                        data.Add(new { cname = "roof_area_sum", data = Roof_data[28] });
                        data.Add(new { cname = "roof_area_sum_percent", data = Roof_data[29] });

                        for (int a = 0; a < 8; a++)
                        {
                            if (roof_name[a] != null && roof_name[a] != "")
                            {
                                Roof_data[30 + a].Add(new { idx = i, val = (roof_area[a] / roof_area_sum * 100).ToString("0") + " %" });//면적율
                                data.Add(new { cname = "roof_area_percent" + a, data = Roof_data[30 + a] });

                                if (roof_ueff_old[a] != 0)
                                {
                                    roof_saving_element[a] = roof_area[a] / roof_area_sum * (roof_ueff_old[a] - roof_ueff[a]);
                                }
                            }
                        }

                        for (int a = 0; a < 8; a++)
                        {
                            if (roof_name[a] != null && roof_name[a] != "")
                            {
                                Roof_data[38 + a].Add(new { idx = i, val = roof_ueff[a].ToString("0.00") });//계획열관류율
                                data.Add(new { cname = "roof_ueff" + a, data = Roof_data[38 + a] });
                                if (roof_retype[a] != "신규")
                                { Roof_data[46 + a].Add(new { idx = i, val = roof_ueff_old[a].ToString("0.00") }); }
                                else { Roof_data[46 + a].Add(new { idx = i, val = "-" }); }
                                data.Add(new { cname = "roof_ueff_old" + a, data = Roof_data[46 + a] });//기존열관류율
                            }
                        }
                        double roof_ueff_avg = 0;
                        double roof_ueff_old_avg = 0;
                        for (int a = 0; a < 8; a++)
                        {
                            roof_ueff_avg += roof_ueff[a] * roof_area[a] / roof_area_sum;
                            roof_ueff_old_avg += roof_ueff_old[a] * roof_area[a] / roof_area_sum_old;
                        }
                        Roof_data[54].Add(new { idx = i, val = roof_ueff_avg.ToString("0.00") });//계획열관류율 평균
                        Roof_data[55].Add(new { idx = i, val = roof_ueff_old_avg.ToString("0.00") });//기존열관류율 평균
                        data.Add(new { cname = "roof_ueff_avg", data = Roof_data[54] });
                        data.Add(new { cname = "roof_ueff_old_avg", data = Roof_data[55] });

                        double sum = 0;
                        for (int a = 0; a < 8; a++)
                        {
                            sum += roof_saving_element[a];
                        }
                        for (int a = 0; a < 8; a++)
                        {
                            if (roof_name[a] != null && roof_name[a] != "")
                            {
                                if (sum != 0)
                                {
                                    Roof_data[56 + a].Add(new { idx = i, val = ((roof_saving / Total_Energy_pre) * (roof_saving_element[a] / sum) * 100).ToString("0.0") + " %" });//요소기술별 에너지절감률
                                }
                                else
                                {
                                    Roof_data[56 + a].Add(new { idx = i, val = (0).ToString("0.0") + " %" });//요소기술별 에너지절감률
                                }
                                data.Add(new { cname = "roof_saving_element" + a, data = Roof_data[56 + a] });
                            }
                        }
                        Roof_data[64].Add(new { idx = i, val = (roof_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });  //요소기술별 절감률 합계
                        data.Add(new { cname = "roof_saving_element_sum", data = Roof_data[64] });

                        double roof_law_avg = 0;
                        for (int a = 0; a < 8; a++)
                        {
                            if (roof_name[a] != null && roof_name[a] != "")
                            {
                                string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "법규열관류율", "번호 ='" + roof_num[a] + "'");
                                if (value2.Length > 0)
                                {
                                    d = Math.Min(100, (Convert.ToDouble(value2[0][0]) / roof_ueff[a] * 100));
                                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";

                                    Roof_data[65 + a].Add(new { idx = i, val = sp });//법규대비 성능점수
                                    data.Add(new { cname = "roof_law_point" + a, data = Roof_data[65 + a] });
                                    roof_law_avg += Convert.ToDouble(value2[0][0]) * roof_area[a] / roof_area_sum;
                                }
                            }
                        }
                        d = Math.Min(100, (roof_law_avg / roof_ueff_avg * 100));
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        Roof_data[73].Add(new { idx = i, val = sp });//법규대비 성능점수 평균
                        data.Add(new { cname = "roof_law_point_avg", data = Roof_data[73] });

                        double east = 0, west = 0, south = 0, north = 0, horizontal = 0;
                        string[][] area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='동' and 외피유형='지붕'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            east = Convert.ToDouble(area[0][0]);
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='서' and 외피유형='지붕'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            west = Convert.ToDouble(area[0][0]);
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='남' or 방위 ='남동' or 방위 ='남서') and 외피유형='지붕'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            south = Convert.ToDouble(area[0][0]);
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='북' or 방위 ='북동' or 방위 ='북서')and 외피유형='지붕'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            north = Convert.ToDouble(area[0][0]);
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='수평' and 외피유형='지붕'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            horizontal = Convert.ToDouble(area[0][0]);
                        }
                        Roof_data[74].Add(new { idx = i, val = east.ToString("0.0") });
                        data.Add(new { cname = "roof_east", data = Roof_data[74] });
                        Roof_data[75].Add(new { idx = i, val = west.ToString("0.0") });
                        data.Add(new { cname = "roof_west", data = Roof_data[75] });
                        Roof_data[76].Add(new { idx = i, val = south.ToString("0.0") });
                        data.Add(new { cname = "roof_south", data = Roof_data[76] });
                        Roof_data[77].Add(new { idx = i, val = north.ToString("0.0") });
                        data.Add(new { cname = "roof_north", data = Roof_data[77] });
                        Roof_data[78].Add(new { idx = i, val = horizontal.ToString("0.0") });
                        data.Add(new { cname = "roof_horizontal", data = Roof_data[78] });

                    }

                    #endregion

                    #region 최하층바닥
                    int j_최하층바닥 = 0;
                    for (int a = 0; a < ElementAlt.Length; a++)
                    {
                        if (ElementAlt[a] == "최하층바닥")
                        {
                            j_최하층바닥 = a; break;
                        }
                    }
                    double floor_saving = Math.Max(Element_EnergySaving[j_최하층바닥], 0);

                    d = (floor_saving / Total_Energy_pre * 100);
                    Floor_data[0].Add(new { idx = i, val = floor_saving.ToString("#,##0") }); ; //절감량 
                    Floor_data[1].Add(new { idx = i, val = d.ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "floor_saving", data = Floor_data[0] });
                    data.Add(new { cname = "floor_savingpercent", data = Floor_data[1] });

                    charts += "{donut:" + d + "},";

                    double floor_saving_elec = Math.Max(Element_ElecSaving[j_최하층바닥], 0);
                    double floor_saving_noelec = Math.Max(Element_GasSaving[j_최하층바닥], 0);

                    double floor_tCO2_elec = floor_saving_elec * 0.4747 / 1000000 * 1000;
                    double floor_TOE_elec = floor_saving_elec * 0.00023;

                    double floor_tCO2_noelec = floor_saving_noelec / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    double floor_TOE_noelec = floor_saving_noelec / 43.1 / 0.277778 * 0.00103;
                    double floor_tCO2 = floor_tCO2_elec + floor_tCO2_noelec;
                    double floor_TOE = floor_TOE_elec + floor_TOE_noelec;
                    Floor_data[2].Add(new { idx = i, val = floor_tCO2.ToString("0.0") });  //tco2
                    Floor_data[3].Add(new { idx = i, val = floor_TOE.ToString("0.0") });  //TOE 
                    data.Add(new { cname = "floor_tco2", data = Floor_data[2] });
                    data.Add(new { cname = "floor_toe", data = Floor_data[3] });

                    Value = Program.DB.querySQL(DB.type.ProjDB, "SELECT DISTINCT a.명칭,a.유효열관류율,a.기존바닥,a.번호,a.단열재두께,a.U적용방법,a.Type  From ConstructionFloor as a  Inner Join ZoneEnvelope_3D as b  on a.번호 = b.구조체번호  where b.외피유형 ='최하층바닥' Order by a.유효열관류율 DESC");
                    string[] floor_num = new string[8]; string[] floor_name = new string[8]; double[] floor_ueff = new double[8]; double[] floor_ueff_old = new double[8]; double[] floor_area = new double[8]; double[] floor_saving_element = new double[8]; string[] floor_feature = new string[8];
                    double floor_area_sum = 0; string[] floor_retype = new string[8]; double floor_area_sum_old = 0;
                    if (Value.Length > 0)
                    {
                        for (int k = 0; k < Value.Length; k++)
                        {
                            floor_name[k] = Value[k][0];
                            floor_retype[k] = Value[k][6];
                            floor_ueff[k] = Convert.ToDouble(Value[k][1]);
                            if (floor_retype[k] != "신규")
                            {
                                if (Value[k][2] != "")
                                {
                                    string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "ConstructionFloor", "유효열관류율", "명칭 ='" + Value[k][2] + "'");
                                    if (value2.Length > 0)
                                    {
                                        floor_ueff_old[k] = Convert.ToDouble(value2[0][0]);
                                    }
                                }
                                else
                                {
                                    floor_ueff_old[k] = floor_ueff[k];
                                }
                            }

                            floor_num[k] = Value[k][3];
                            string[][] valuek = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "외피유형='최하층바닥' And 구조체번호='" + Value[k][3] + "'");
                            if (valuek.Length > 0)
                            {
                                for (int a = 0; a < valuek.Length; a++)
                                { floor_area[k] += Convert.ToDouble(valuek[a][0]); }
                            }
                            if (Value[k][5] == "법규") { floor_feature[k] = "-"; }
                            else
                            {
                                if (Convert.ToDouble(Value[k][4]) > 0)
                                {
                                    floor_feature[k] = "단열두께 " + Convert.ToDouble(Value[k][4]).ToString("") + "mm";
                                }
                                else
                                {
                                    floor_feature[k] = "미단열";
                                }
                            }
                        }

                        for (int a = 0; a < 8; a++)
                        {
                            Floor_data[4 + a].Add(new { idx = i, val = floor_name[a] });//명칭
                            data.Add(new { cname = "floor_name" + a, data = Floor_data[4 + a] });
                            if (floor_name[a] != null && floor_name[a] != "")
                            {
                                Floor_data[12 + a].Add(new { idx = i, val = floor_area[a].ToString("0.0") });//면적
                                data.Add(new { cname = "floor_area" + a, data = Floor_data[12 + a] });
                                floor_area_sum += floor_area[a];
                                if (floor_retype[a] != "신규") { floor_area_sum_old += floor_area[a]; }
                                Floor_data[20 + a].Add(new { idx = i, val = floor_feature[a] });//특징
                                data.Add(new { cname = "floor_feature" + a, data = Floor_data[20 + a] });
                            }
                        }
                        Floor_data[28].Add(new { idx = i, val = floor_area_sum.ToString("0.0") });//면적합계
                        Floor_data[29].Add(new { idx = i, val = "100 %" });//면적율합계
                        data.Add(new { cname = "floor_area_sum", data = Floor_data[28] });
                        data.Add(new { cname = "floor_area_sum_percent", data = Floor_data[29] });

                        for (int a = 0; a < 8; a++)
                        {
                            if (floor_name[a] != null && floor_name[a] != "")
                            {
                                Floor_data[30 + a].Add(new { idx = i, val = (floor_area[a] / floor_area_sum * 100).ToString("0") + " %" });//면적율
                                data.Add(new { cname = "floor_area_percent" + a, data = Floor_data[30 + a] });

                                if (floor_ueff_old[a] != 0)
                                {
                                    floor_saving_element[a] = floor_area[a] / floor_area_sum * (floor_ueff_old[a] - floor_ueff[a]);
                                }
                            }
                        }

                        for (int a = 0; a < 8; a++)
                        {
                            if (floor_name[a] != null && floor_name[a] != "")
                            {
                                Floor_data[38 + a].Add(new { idx = i, val = floor_ueff[a].ToString("0.00") });//계획열관류율
                                data.Add(new { cname = "floor_ueff" + a, data = Floor_data[38 + a] });
                                if (floor_retype[a] != "신규")
                                { Floor_data[46 + a].Add(new { idx = i, val = floor_ueff_old[a].ToString("0.00") }); }
                                else { Floor_data[46 + a].Add(new { idx = i, val = "-" }); }
                                data.Add(new { cname = "floor_ueff_old" + a, data = Floor_data[46 + a] }); //기존열관류율
                            }
                        }
                        double floor_ueff_avg = 0;
                        double floor_ueff_old_avg = 0;
                        for (int a = 0; a < 8; a++)
                        {
                            floor_ueff_avg += floor_ueff[a] * floor_area[a] / floor_area_sum;
                            floor_ueff_old_avg += floor_ueff_old[a] * floor_area[a] / floor_area_sum_old;
                        }
                        Floor_data[54].Add(new { idx = i, val = floor_ueff_avg.ToString("0.00") });//계획열관류율 평균
                        Floor_data[55].Add(new { idx = i, val = floor_ueff_old_avg.ToString("0.00") });//기존열관류율 평균
                        data.Add(new { cname = "floor_ueff_avg", data = Floor_data[54] });
                        data.Add(new { cname = "floor_ueff_old_avg", data = Floor_data[55] });

                        double sum = 0;
                        for (int a = 0; a < 8; a++)
                        {
                            sum += floor_saving_element[a];
                        }
                        for (int a = 0; a < 8; a++)
                        {
                            if (floor_name[a] != null && floor_name[a] != "")
                            {
                                if (sum != 0)
                                {
                                    Floor_data[56 + a].Add(new { idx = i, val = ((floor_saving / Total_Energy_pre) * (floor_saving_element[a] / sum) * 100).ToString("0.0") + " %" });//요소기술별 에너지절감률
                                }
                                else
                                {
                                    Floor_data[56 + a].Add(new { idx = i, val = (0).ToString("0.0") + " %" });//요소기술별 에너지절감률
                                }
                                data.Add(new { cname = "floor_saving_element" + a, data = Floor_data[56 + a] });
                            }
                        }
                        Floor_data[64].Add(new { idx = i, val = (floor_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });  //요소기술별 절감률 합계
                        data.Add(new { cname = "floor_saving_element_sum", data = Floor_data[64] });

                        double floor_law_avg = 0;
                        for (int a = 0; a < 8; a++)
                        {
                            if (floor_name[a] != null && floor_name[a] != "")
                            {
                                string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "ConstructionFloor", "법규열관류율", "번호 ='" + floor_num[a] + "'");
                                if (value2.Length > 0)
                                {
                                    d = Math.Min(100, (Convert.ToDouble(value2[0][0]) / floor_ueff[a] * 100));
                                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                                    Floor_data[65 + a].Add(new { idx = i, val = sp });//법규대비 성능점수
                                    data.Add(new { cname = "floor_law_point" + a, data = Floor_data[65 + a] });
                                    floor_law_avg += Convert.ToDouble(value2[0][0]) * floor_area[a] / floor_area_sum;
                                }
                            }
                        }
                        d = Math.Min(100, (floor_law_avg / floor_ueff_avg * 100));
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        Floor_data[73].Add(new { idx = i, val = sp });//법규대비 성능점수 평균
                        data.Add(new { cname = "floor_law_point_avg", data = Floor_data[73] });

                        double east = 0, west = 0, south = 0, north = 0;
                        string[][] area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(a.면적) From ZoneEnvelope_3D as a  Inner Join ConstructionFloor as b on a.구조체번호 = b.번호 where a.외피유형='최하층바닥' and  b.기초설치 ='지면위'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            east = Convert.ToDouble(area[0][0]);
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(a.면적) From ZoneEnvelope_3D as a  Inner Join ConstructionFloor as b on a.구조체번호 = b.번호 where a.외피유형='최하층바닥' and  b.기초설치 ='단열지하실'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            west = Convert.ToDouble(area[0][0]);
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(a.면적) From ZoneEnvelope_3D as a  Inner Join ConstructionFloor as b on a.구조체번호 = b.번호 where a.외피유형='최하층바닥' and  b.기초설치 ='비단열지하실'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            south = Convert.ToDouble(area[0][0]);
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(a.면적) From ZoneEnvelope_3D as a  Inner Join ConstructionFloor as b on a.구조체번호 = b.번호 where a.외피유형='최하층바닥' and  b.기초설치 ='바닥(외기)'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            north = Convert.ToDouble(area[0][0]);
                        }
                        Floor_data[74].Add(new { idx = i, val = east.ToString("0.0") });
                        data.Add(new { cname = "floor_east", data = Floor_data[74] });
                        Floor_data[75].Add(new { idx = i, val = west.ToString("0.0") });
                        data.Add(new { cname = "floor_west", data = Floor_data[75] });
                        Floor_data[76].Add(new { idx = i, val = south.ToString("0.0") });
                        data.Add(new { cname = "floor_south", data = Floor_data[76] });
                        Floor_data[77].Add(new { idx = i, val = north.ToString("0.0") });
                        data.Add(new { cname = "floor_north", data = Floor_data[77] });

                    }

                    #endregion


                    items.Add("Element_structure.htm");
                    s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                    s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
                    System.Text.Json.JsonSerializer.Serialize(Wall_data[10].ToArray());

                    Debug.Print("start");

                    script = "init(" + s + "," + s2 + "," + "[" + charts + "])";
                    return script;
                }
            }
            return script;
        }

    }
}