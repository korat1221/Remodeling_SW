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
    public partial class Element_Win : Form
    {
        bool scriptable = false;
        public Element_Win()
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
            string[][] res = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기존프로젝트");
            List<object> items = new List<object>();
            List<object> items2 = new List<object>();
            List<object> data = new List<object>();
            List<object>[] Win_data = new List<object>[700];
            List<object>[] CW_data = new List<object>[700];
            List<object>[] Door_data = new List<object>[700];
            double d;
            string sp;
            int i = -1, n;
            while (++i < 700)
            {
                Win_data[i] = new List<object>();
                CW_data[i] = new List<object>();
                Door_data[i] = new List<object>();
            }
            string charts = "";
            i = -1;
            while (++i < 번호.Length)
            {
                if (res.Length > 0)
                {
                    #region 창호                                
                    double Total_Energy_pre = 0;
                    string[][] value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='창호'");
                    string[][] value4 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                    double win_saving = 0;
                    if (value.Length > 0 && value4.Length > 0)
                    {
                        Total_Energy_pre = Convert.ToDouble(value4[0][0]);
                        win_saving = Math.Max(0, Convert.ToDouble(value4[0][0]) - Convert.ToDouble(value[0][0]));
                    }

                    value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전기' and 검토유형='창호'");
                    value4 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전기'");
                    double win_saving_elec = 0;
                    if (value.Length > 0 && value4.Length > 0)
                    {
                        win_saving_elec = Math.Max(0, Convert.ToDouble(value4[0][0]) - Convert.ToDouble(value[0][0]));
                    }
                    value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='창호'");
                    value4 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                    double win_saving_noelec = 0;
                    if (value.Length > 0 && value4.Length > 0)
                    {
                        win_saving_noelec = Math.Max(0, Convert.ToDouble(value4[0][0]) - Convert.ToDouble(value[0][0]));
                    }
                    d = (win_saving / Total_Energy_pre * 100);
                    Win_data[0].Add(new { idx = i, val = win_saving.ToString("#,##0") }); ; //절감량 
                    Win_data[1].Add(new { idx = i, val = (win_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "win_saving", data = Win_data[0] });
                    data.Add(new { cname = "win_savingpercent", data = Win_data[1] });

                    charts += "{donut:" + d + "},";
                    double win_tCO2_elec = win_saving_elec * 0.4747 / 1000000 * 1000;
                    double win_TOE_elec = win_saving_elec * 0.00023;
                    double win_tCO2_noelec = win_saving_noelec / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    double win_TOE_noelec = win_saving_noelec / 43.1 / 0.277778 * 0.00103;
                    double win_tCO2 = win_tCO2_elec + win_tCO2_noelec;
                    double win_TOE = win_TOE_elec + win_TOE_noelec;
                    Win_data[2].Add(new { idx = i, val = win_tCO2.ToString("0.0") });  //tco2
                    Win_data[3].Add(new { idx = i, val = win_TOE.ToString("0.0") });  //TOE 
                    data.Add(new { cname = "win_tco2", data = Win_data[2] });
                    data.Add(new { cname = "win_toe", data = Win_data[3] });
                    string[][] 상위창호 = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "번호", "");
                    if (상위창호.Length > 0)
                    {
                        double[] Ueff_avg_상위창호 = new double[상위창호.Length];
                        for (int a = 0; a < 상위창호.Length; a++)
                        {
                            double sum_면적 = 0;
                            string[][] Value2 = Program.DB.querySQL(DB.type.ProjDB, "SELECT 창호유효열관류율,창호면적 From SubWindow where 상위창호번호='" + 상위창호[a][0] + "'");
                            if (Value2.Length == 0)
                            {
                                Value2 = Program.DB.querySQL(res[0][0], "SELECT 창호유효열관류율,창호면적 From SubWindow where 상위창호번호='" + 상위창호[a][0] + "'");
                            }

                            for (int b = 0; b < Value2.Length; b++)
                            {
                                Ueff_avg_상위창호[a] += Convert.ToDouble(Value2[b][0]) * Convert.ToDouble(Value2[b][1]);
                                sum_면적 += Convert.ToDouble(Value2[b][1]);
                            }
                            Ueff_avg_상위창호[a] = Ueff_avg_상위창호[a] / sum_면적;
                        }

                        string[][] kk = Program.DB.querySQL(DB.type.ProjDB, "SELECT DISTINCT a.상위창호번호 From SubWindow as a  Inner Join ZoneEnvelope_3D as b  on a.번호 = b.구조체번호  where b.외피유형 ='창호' Order by a.창호유효열관류율 DESC");
                        string[] win_name = new string[8]; double[] win_ueff = new double[8]; double[] win_ueff_law = new double[8]; double[] win_area = new double[8]; double[] win_count = new double[8];
                        string[] win_frame = new string[8]; string[] win_glass = new string[8]; double[] win_shgc = new double[8]; string[] win_retype = new string[8];
                        double win_area_sum = 0; double win_count_sum = 0; double win_area_sum_law = 0;
                        if (kk.Length > 0)
                        {
                            for (int k = 0; k < kk.Length; k++)
                            {
                                string[][] main_value = Program.DB.querySQL(DB.type.ProjDB, "SELECT 창호명칭,기존창호,유리종류,프레임유형,태양열취득률,Type,법규열관류율 From ConstructionWindow where 번호 ='" + kk[k][0] + "'");
                                win_name[k] = main_value[0][0];
                                win_glass[k] = main_value[0][2];
                                win_frame[k] = main_value[0][3];
                                win_shgc[k] = Convert.ToDouble(main_value[0][4]);
                                win_retype[k] = main_value[0][5];
                                for (int a = 0; a < 상위창호.Length; a++)
                                {
                                    if (kk[k][0] == 상위창호[a][0])
                                    {
                                        win_ueff[k] = Ueff_avg_상위창호[a];
                                    }
                                }
                                win_ueff_law[k] = Convert.ToDouble(main_value[0][6]);
                                string[][] valuek = Program.DB.querySQL(DB.type.ProjDB, "SELECT b.면적 From ZoneEnvelope_3D as b  Inner Join SubWindow as a  on a.번호 = b.구조체번호  where b.외피유형 = '창호' And a.상위창호번호 ='" + kk[k][0] + "'");
                                if (valuek.Length > 0)
                                {
                                    for (int a = 0; a < valuek.Length; a++)
                                    {
                                        win_area[k] += Convert.ToDouble(valuek[a][0]);
                                    }
                                    win_count[k] = valuek.Length;
                                }
                            }

                            for (int a = 0; a < 8; a++)
                            {
                                Win_data[4 + a].Add(new { idx = i, val = win_name[a] });//명칭
                                Win_data[12 + a].Add(new { idx = i, val = win_glass[a] });//유리
                                Win_data[20 + a].Add(new { idx = i, val = win_frame[a] });//프레임
                                if (win_shgc[a] != 0)
                                { Win_data[28 + a].Add(new { idx = i, val = win_shgc[a].ToString("0.00") }); }//태양열취득률
                                else { Win_data[28 + a].Add(new { idx = i, val = "" }); }
                                data.Add(new { cname = "win_name" + a, data = Win_data[4 + a] });
                                data.Add(new { cname = "win_glass" + a, data = Win_data[12 + a] });
                                data.Add(new { cname = "win_frame" + a, data = Win_data[20 + a] });
                                data.Add(new { cname = "win_shgc" + a, data = Win_data[28 + a] });

                                if (win_name[a] != null && win_name[a] != "")
                                {
                                    Win_data[36 + a].Add(new { idx = i, val = win_count[a].ToString("0") });//개수
                                    data.Add(new { cname = "win_count" + a, data = Win_data[36 + a] });
                                    win_area_sum += win_area[a];
                                    win_area_sum_law += win_area[a];
                                    win_count_sum += win_count[a];
                                }
                            }
                            Win_data[44].Add(new { idx = i, val = win_count_sum.ToString("0") });//개수합계
                            if (win_area_sum < 0)
                            {

                                Win_data[45].Add(new { idx = i, val = 0.ToString("0") + " %" });//면적율합계
                            }
                            else
                            {
                                Win_data[45].Add(new { idx = i, val = (win_area_sum / win_area_sum * 100).ToString("0") + " %" });//면적율합계
                            }

                            data.Add(new { cname = "win_count_sum", data = Win_data[44] });
                            data.Add(new { cname = "win_area_sum_percent", data = Win_data[45] });

                            for (int a = 0; a < 8; a++)
                            {
                                if (win_name[a] != null && win_name[a] != "")
                                {
                                    Win_data[46 + a].Add(new { idx = i, val = (win_area[a] / win_area_sum * 100).ToString("0") + " %" });//면적율
                                    data.Add(new { cname = "win_area_percent" + a, data = Win_data[46 + a] });
                                }
                            }

                            for (int a = 0; a < 8; a++)
                            {
                                if (win_name[a] != null && win_name[a] != "")
                                {
                                    Win_data[54 + a].Add(new { idx = i, val = win_ueff[a].ToString("0.00") });//계획열관류율
                                    data.Add(new { cname = "win_ueff" + a, data = Win_data[54 + a] });
                                    Win_data[62 + a].Add(new { idx = i, val = win_ueff_law[a].ToString("0.00") });
                                    data.Add(new { cname = "win_ueff_law" + a, data = Win_data[62 + a] });//법규열관류율
                                }
                            }
                            double win_ueff_avg = 0;
                            double win_ueff_law_avg = 0;
                            double win_shgc_avg = 0;
                            for (int a = 0; a < 8; a++)
                            {
                                win_ueff_avg += win_ueff[a] * win_area[a] / win_area_sum;
                                win_ueff_law_avg += win_ueff_law[a] * win_area[a] / win_area_sum_law;
                                win_shgc_avg += win_shgc[a] * win_area[a] / win_area_sum;
                            }
                            Win_data[70].Add(new { idx = i, val = win_ueff_avg.ToString("0.00") });//계획열관류율 평균
                            Win_data[71].Add(new { idx = i, val = win_ueff_law_avg.ToString("0.00") });//법규열관류율 평균
                            Win_data[72].Add(new { idx = i, val = win_shgc_avg.ToString("0.00") });//
                            data.Add(new { cname = "win_ueff_avg", data = Win_data[70] });
                            data.Add(new { cname = "win_ueff_law_avg", data = Win_data[71] });
                            data.Add(new { cname = "win_shgc_avg", data = Win_data[72] });

                            double win_law_avg = 0;
                            for (int a = 0; a < 8; a++)
                            {
                                if (win_name[a] != null && win_name[a] != "")
                                {
                                    string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "법규열관류율", "번호 ='" + kk[a][0] + "'");
                                    if (value2.Length > 0)
                                    {
                                        d = Math.Min(100, Convert.ToDouble(value2[0][0]) / win_ueff[a] * 100);
                                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";

                                        Win_data[89 + a].Add(new { idx = i, val = sp });//법규대비 성능점수
                                        data.Add(new { cname = "win_law_point" + a, data = Win_data[89 + a] });
                                        win_law_avg += Convert.ToDouble(value2[0][0]) * win_area[a] / win_area_sum;
                                    }
                                }
                            }
                            d = Math.Min(100, (win_law_avg / win_ueff_avg * 100));
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";

                            Win_data[97].Add(new { idx = i, val = sp });//법규대비 성능점수 평균
                            data.Add(new { cname = "win_law_point_avg", data = Win_data[97] });

                            double east = 0, west = 0, south = 0, north = 0;
                            double east_p = 0, west_p = 0, south_p = 0, north_p = 0;

                            string[][] area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='동' and 외피유형='창호'");
                            if (area.Length > 0 && area[0][0] != "")
                            {
                                east = Convert.ToDouble(area[0][0]);
                            }
                            area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='서' and 외피유형='창호'");
                            if (area.Length > 0 && area[0][0] != "")
                            {
                                west = Convert.ToDouble(area[0][0]);
                            }
                            area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='남' or 방위 ='남동' or 방위 ='남서') and 외피유형='창호'");
                            if (area.Length > 0 && area[0][0] != "")
                            {
                                south = Convert.ToDouble(area[0][0]);
                            }
                            area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='북' or 방위 ='북동' or 방위 ='북서')and 외피유형='창호'");
                            if (area.Length > 0 && area[0][0] != "")
                            {
                                north = Convert.ToDouble(area[0][0]);
                            }

                            area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='동' and not 외피유형='최하층바닥'and not 외피유형='층간바닥' and not 외피유형='내벽'and not 외피유형='지붕'");
                            if (area.Length > 0 && area[0][0] != "")
                            {
                                east_p = east * 100 / Convert.ToDouble(area[0][0]);
                            }
                            area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='서' and not 외피유형='최하층바닥'and not 외피유형='층간바닥' and not 외피유형='내벽'and not 외피유형='지붕'");
                            if (area.Length > 0 && area[0][0] != "")
                            {
                                west_p = west * 100 / Convert.ToDouble(area[0][0]);
                            }
                            area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='남' or 방위 ='남동' or 방위 ='남서') and not 외피유형='최하층바닥'and not 외피유형='층간바닥' and not 외피유형='내벽'and not 외피유형='지붕'");
                            if (area.Length > 0 && area[0][0] != "")
                            {
                                south_p = south * 100 / Convert.ToDouble(area[0][0]);
                            }
                            area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='북' or 방위 ='북동' or 방위 ='북서') and not 외피유형='최하층바닥'and not 외피유형='층간바닥' and not 외피유형='내벽'and not 외피유형='지붕'");
                            if (area.Length > 0 && area[0][0] != "")
                            {
                                north_p = north * 100 / Convert.ToDouble(area[0][0]);
                            }

                            Win_data[98].Add(new { idx = i, val = east.ToString("0.0") + " m"+ Program.UTIL.Subscript(2, true) });
                            data.Add(new { cname = "win_east", data = Win_data[98] });
                            Win_data[99].Add(new { idx = i, val = west.ToString("0.0") + " m"+ Program.UTIL.Subscript(2, true) });
                            data.Add(new { cname = "win_west", data = Win_data[99] });
                            Win_data[100].Add(new { idx = i, val = south.ToString("0.0") + " m"+ Program.UTIL.Subscript(2, true) });
                            data.Add(new { cname = "win_south", data = Win_data[100] });
                            Win_data[101].Add(new { idx = i, val = north.ToString("0.0") + " m"+ Program.UTIL.Subscript(2, true) });
                            data.Add(new { cname = "win_north", data = Win_data[101] });

                            Win_data[102].Add(new { idx = i, val = east_p.ToString("0.0") + " %" });
                            data.Add(new { cname = "win_east_p", data = Win_data[102] });
                            Win_data[103].Add(new { idx = i, val = west_p.ToString("0.0") + " %" });
                            data.Add(new { cname = "win_west_p", data = Win_data[103] });
                            Win_data[104].Add(new { idx = i, val = south_p.ToString("0.0") + " %" });
                            data.Add(new { cname = "win_south_p", data = Win_data[104] });
                            Win_data[105].Add(new { idx = i, val = north_p.ToString("0.0") + " %" });
                            data.Add(new { cname = "win_north_p", data = Win_data[105] });

                            double totalarea = 0;
                            area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where not 외피유형='최하층바닥'and not 외피유형='층간바닥' and not 외피유형='내벽'");
                            if (area.Length > 0 && area[0][0] != "")
                            {
                                totalarea = Convert.ToDouble(area[0][0]);
                            }
                            Win_data[106].Add(new { idx = i, val = (win_area_sum / totalarea * 100).ToString("0.0") });
                            data.Add(new { cname = "win_openpercent", data = Win_data[106] });

                        }
                    }

                    #endregion

                    #region 커튼월창
                    value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='커튼월창'");
                    value4 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                    double cw_saving = 0;
                    if (value.Length > 0 && value4.Length > 0)
                    {
                        cw_saving = Math.Max(0, Convert.ToDouble(value4[0][0]) - Convert.ToDouble(value[0][0]));
                    }

                    value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전기' and 검토유형='커튼월창'");
                    value4 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전기'");
                    double cw_saving_elec = 0;
                    if (value.Length > 0 && value4.Length > 0)
                    {
                        cw_saving_elec = Math.Max(0, Convert.ToDouble(value4[0][0]) - Convert.ToDouble(value[0][0]));
                    }
                    value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='커튼월창'");
                    value4 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                    double cw_saving_noelec = 0;
                    if (value.Length > 0 && value4.Length > 0)
                    {
                        cw_saving_noelec = Math.Max(0, Convert.ToDouble(value4[0][0]) - Convert.ToDouble(value[0][0]));
                    }
                    d = (cw_saving / Total_Energy_pre * 100);
                    CW_data[0].Add(new { idx = i, val = cw_saving.ToString("#,##0") }); ; //절감량 
                    CW_data[1].Add(new { idx = i, val = (cw_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "cw_saving", data = CW_data[0] });
                    data.Add(new { cname = "cw_savingpercent", data = CW_data[1] });
                    charts += "{donut:" + d + "},";
                    double cw_tCO2_elec = cw_saving_elec * 0.4747 / 1000000 * 1000;
                    double cw_TOE_elec = cw_saving_elec * 0.00023;

                    double cw_tCO2_noelec = cw_saving_noelec / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    double cw_TOE_noelec = cw_saving_noelec / 43.1 / 0.277778 * 0.00103;
                    double cw_tCO2 = cw_tCO2_elec + cw_tCO2_noelec;
                    double cw_TOE = cw_TOE_elec + cw_TOE_noelec;
                    CW_data[2].Add(new { idx = i, val = cw_tCO2.ToString("0.0") });  //tco2
                    CW_data[3].Add(new { idx = i, val = cw_TOE.ToString("0.0") });  //TOE 
                    data.Add(new { cname = "cw_tco2", data = CW_data[2] });
                    data.Add(new { cname = "cw_toe", data = CW_data[3] });

                    string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "SELECT DISTINCT a.명칭,a.유리부분유효열관류율,a.기존커튼월,a.번호,b.커튼월부위,a.Type From ConstructionCW as a  Inner Join ZoneEnvelope_3D as b  on a.번호 = b.구조체번호  where b.외피유형 ='커튼월창' Order by a.커튼월창유효열관류율 DESC");
                    string[] cw_num = new string[8]; string[] cw_name = new string[8]; double[] cw_ueff = new double[8]; double[] cw_ueff_law = new double[8]; double[] cw_area = new double[8]; double[] cw_shgc = new double[8]; string[] cw_frame = new string[8]; string[] cw_glass = new string[8]; string[] cw_part = new string[8];
                    double cw_area_sum = 0; string[] cw_retype = new string[8]; double cw_area_sum_law = 0;
                    if (Value.Length > 0)
                    {
                        for (int k = 0; k < Value.Length; k++)
                        {
                            cw_name[k] = Value[k][0] + "_" + Value[k][4];
                            cw_retype[k] = Value[k][5];
                            cw_num[k] = Value[k][3];
                            cw_part[k] = Value[k][4];
                            if (cw_part[k] == "유리부분")
                            {
                                string[][] value3 = Program.DB.querySQL(DB.type.ProjDB, "SELECT 유리부분유효열관류율,프레임유형,태양열취득률,고정유리종류,법규유리부분열관류율 From ConstructionCW  where 번호='" + Value[k][3] + "'");
                                cw_ueff[k] = Convert.ToDouble(value3[0][0]);
                                cw_frame[k] = value3[0][1];
                                cw_shgc[k] = Convert.ToDouble(value3[0][2]);
                                cw_glass[k] = value3[0][3];
                                cw_ueff_law[k] = Convert.ToDouble(value3[0][4]);
                            }
                            else if (cw_part[k] == "패널부분")
                            {
                                string[][] value3 = Program.DB.querySQL(DB.type.ProjDB, "SELECT 패널부분유효열관류율,프레임유형,패널유리종류,법규패널부분열관류율 From ConstructionCW  where 번호='" + Value[k][3] + "'");
                                cw_ueff[k] = Convert.ToDouble(value3[0][0]);
                                cw_frame[k] = value3[0][1];
                                cw_glass[k] = value3[0][2];
                                cw_ueff_law[k] = Convert.ToDouble(value3[0][3]);
                            }
                            else
                            {
                                string[][] value3 = Program.DB.querySQL(DB.type.ProjDB, "SELECT 출입문부분유효열관류율,프레임유형,출입문태양열취득률,패널유리종류,법규출입문부분열관류율 From ConstructionCW  where 번호='" + Value[k][3] + "'");
                                cw_ueff[k] = Convert.ToDouble(value3[0][0]);
                                cw_frame[k] = value3[0][1];
                                cw_shgc[k] = Convert.ToDouble(value3[0][2]);
                                cw_glass[k] = value3[0][3];
                                cw_ueff_law[k] = Convert.ToDouble(value3[0][4]);
                            }
                            string[][] valuek = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "외피유형='커튼월창' And 구조체번호='" + Value[k][3] + "'");
                            if (valuek.Length > 0)
                            {
                                for (int a = 0; a < valuek.Length; a++)
                                { cw_area[k] += Convert.ToDouble(valuek[a][0]); }
                            }
                        }

                        for (int a = 0; a < 8; a++)
                        {
                            CW_data[4 + a].Add(new { idx = i, val = cw_name[a] });//명칭
                            data.Add(new { cname = "cw_name" + a, data = CW_data[4 + a] });
                            CW_data[12 + a].Add(new { idx = i, val = cw_frame[a] });//프레임
                            data.Add(new { cname = "cw_frame" + a, data = CW_data[12 + a] });
                            CW_data[20 + a].Add(new { idx = i, val = cw_glass[a] });//유리
                            data.Add(new { cname = "cw_glass" + a, data = CW_data[20 + a] });

                            if (cw_name[a] != null && cw_name[a] != "")
                            {
                                CW_data[28 + a].Add(new { idx = i, val = cw_area[a].ToString("0.0") });//면적
                                data.Add(new { cname = "cw_area" + a, data = CW_data[28 + a] });
                                cw_area_sum += cw_area[a];
                                cw_area_sum_law += cw_area[a];
                            }
                        }
                        CW_data[36].Add(new { idx = i, val = cw_area_sum.ToString("0.0") });//면적합계
                        CW_data[37].Add(new { idx = i, val = "100 %" });//면적율합계
                        data.Add(new { cname = "cw_area_sum", data = CW_data[36] });
                        data.Add(new { cname = "cw_area_sum_percent", data = CW_data[37] });

                        for (int a = 0; a < 8; a++)
                        {
                            if (cw_name[a] != null && cw_name[a] != "")
                            {
                                CW_data[45 + a].Add(new { idx = i, val = (cw_area[a] / cw_area_sum * 100).ToString("0") + " %" });//면적율
                                data.Add(new { cname = "cw_area_percent" + a, data = CW_data[45 + a] });
                            }
                        }

                        for (int a = 0; a < 8; a++)
                        {
                            if (cw_name[a] != null && cw_name[a] != "")
                            {
                                CW_data[53 + a].Add(new { idx = i, val = cw_ueff[a].ToString("0.00") });//계획열관류율
                                data.Add(new { cname = "cw_ueff" + a, data = CW_data[53 + a] });
                                CW_data[61 + a].Add(new { idx = i, val = cw_ueff_law[a].ToString("0.00") });
                                data.Add(new { cname = "cw_ueff_law" + a, data = CW_data[61 + a] });//법규열관류율
                                CW_data[69 + a].Add(new { idx = i, val = cw_shgc[a].ToString("0.00") });//태양열취득률
                                data.Add(new { cname = "cw_shgc" + a, data = CW_data[69 + a] });
                            }
                        }
                        double cw_ueff_avg = 0;
                        double cw_ueff_law_avg = 0;
                        double cw_shgc_avg = 0;
                        double area_shgc = 0;
                        for (int a = 0; a < 8; a++)
                        {
                            cw_ueff_avg += cw_ueff[a] * cw_area[a] / cw_area_sum;
                            cw_ueff_law_avg += cw_ueff_law[a] * cw_area[a] / cw_area_sum_law;
                            if (cw_part[a] != "패널부분")
                            {
                                area_shgc += cw_area[a];
                                cw_shgc_avg += cw_shgc[a] * cw_area[a];
                            }
                        }
                        if (area_shgc != 0)
                        { cw_shgc_avg = cw_shgc_avg / area_shgc; }

                        CW_data[77].Add(new { idx = i, val = cw_ueff_avg.ToString("0.00") });//계획열관류율 평균
                        CW_data[78].Add(new { idx = i, val = cw_ueff_law_avg.ToString("0.00") });//법규열관류율 평균
                        CW_data[79].Add(new { idx = i, val = cw_shgc_avg.ToString("0.00") });//태양열취득률 평균
                        data.Add(new { cname = "cw_ueff_avg", data = CW_data[77] });
                        data.Add(new { cname = "cw_ueff_law_avg", data = CW_data[78] });
                        data.Add(new { cname = "cw_shgc_avg", data = CW_data[79] });


                        double cw_law_avg = 0;
                        for (int a = 0; a < 8; a++)
                        {
                            if (cw_name[a] != null && cw_name[a] != "")
                            {
                                d = Math.Min(100, (cw_ueff_law[a] / cw_ueff[a] * 100));
                                if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                                sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";

                                CW_data[89 + a].Add(new { idx = i, val = sp });//법규대비 성능점수
                                data.Add(new { cname = "cw_law_point" + a, data = CW_data[89 + a] });
                                cw_law_avg += cw_ueff_law[a] * cw_area[a] / cw_area_sum;
                            }
                        }
                        d = Math.Min(100, (cw_law_avg / cw_ueff_avg * 100));
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        CW_data[97].Add(new { idx = i, val = sp });//법규대비 성능점수 평균
                        data.Add(new { cname = "cw_law_point_avg", data = CW_data[97] });

                        double east = 0, west = 0, south = 0, north = 0;
                        double east_p = 0, west_p = 0, south_p = 0, north_p = 0;
                        string[][] area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='동' and 외피유형='커튼월창'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            east = Convert.ToDouble(area[0][0]);
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='서' and 외피유형='커튼월창'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            west = Convert.ToDouble(area[0][0]);
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='남' or 방위 ='남동' or 방위 ='남서') and 외피유형='커튼월창'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            south = Convert.ToDouble(area[0][0]);
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='북' or 방위 ='북동' or 방위 ='북서')and 외피유형='커튼월창'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            north = Convert.ToDouble(area[0][0]);
                        }


                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='동'and not 외피유형='최하층바닥'and not 외피유형='층간바닥' and not 외피유형='내벽'and not 외피유형='지붕'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            east_p = east / Convert.ToDouble(area[0][0]) * 100;
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='서'and not 외피유형='최하층바닥'and not 외피유형='층간바닥' and not 외피유형='내벽'and not 외피유형='지붕'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            west_p = west / Convert.ToDouble(area[0][0]) * 100;
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='남' or 방위 ='남동' or 방위 ='남서')and not 외피유형='최하층바닥'and not 외피유형='층간바닥' and not 외피유형='내벽'and not 외피유형='지붕'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            south_p = south / Convert.ToDouble(area[0][0]) * 100;
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='북' or 방위 ='북동' or 방위 ='북서')and not 외피유형='최하층바닥'and not 외피유형='층간바닥' and not 외피유형='내벽'and not 외피유형='지붕'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            north_p = north / Convert.ToDouble(area[0][0]) * 100;
                        }
                        CW_data[98].Add(new { idx = i, val = east.ToString("0.0") + " m"+ Program.UTIL.Subscript(2, true) });
                        data.Add(new { cname = "cw_east", data = CW_data[98] });
                        CW_data[99].Add(new { idx = i, val = west.ToString("0.0") + " m"+ Program.UTIL.Subscript(2, true) });
                        data.Add(new { cname = "cw_west", data = CW_data[99] });
                        CW_data[100].Add(new { idx = i, val = south.ToString("0.0") + " m"+ Program.UTIL.Subscript(2, true) });
                        data.Add(new { cname = "cw_south", data = CW_data[100] });
                        CW_data[101].Add(new { idx = i, val = north.ToString("0.0") + " m"+ Program.UTIL.Subscript(2, true) });
                        data.Add(new { cname = "cw_north", data = CW_data[101] });

                        CW_data[102].Add(new { idx = i, val = east_p.ToString("0.0") + " %" });
                        data.Add(new { cname = "cw_east_p", data = CW_data[102] });
                        CW_data[103].Add(new { idx = i, val = west_p.ToString("0.0") + " %" });
                        data.Add(new { cname = "cw_west_p", data = CW_data[103] });
                        CW_data[104].Add(new { idx = i, val = south_p.ToString("0.0") + " %" });
                        data.Add(new { cname = "cw_south_p", data = CW_data[104] });
                        CW_data[105].Add(new { idx = i, val = north_p.ToString("0.0") + " %" });
                        data.Add(new { cname = "cw_north_p", data = CW_data[105] });

                        double totalarea = 0;
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where not 외피유형='최하층바닥'and not 외피유형='층간바닥' and not 외피유형='내벽'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            totalarea = Convert.ToDouble(area[0][0]);
                        }
                        CW_data[106].Add(new { idx = i, val = (cw_area_sum / totalarea * 100).ToString("0.0") });
                        data.Add(new { cname = "cw_openpercent", data = CW_data[106] });

                    }

                    #endregion


                    #region 외부출입문
                    value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='외부출입문'");
                    value4 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                    double door_saving = 0;
                    if (value.Length > 0 && value4.Length > 0)
                    {
                        door_saving = Math.Max(0, Convert.ToDouble(value4[0][0]) - Convert.ToDouble(value[0][0]));
                    }

                    value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전기' and 검토유형='외부출입문'");
                    value4 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전기'");
                    double door_saving_elec = 0;
                    if (value.Length > 0 && value4.Length > 0)
                    {
                        door_saving_elec = Math.Max(0, Convert.ToDouble(value4[0][0]) - Convert.ToDouble(value[0][0]));
                    }
                    value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' and 검토유형='외부출입문'");
                    value4 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                    double door_saving_noelec = 0;
                    if (value.Length > 0 && value4.Length > 0)
                    {
                        door_saving_noelec = Math.Max(0, Convert.ToDouble(value4[0][0]) - Convert.ToDouble(value[0][0]));
                    }
                    d = (door_saving / Total_Energy_pre * 100);
                    Door_data[0].Add(new { idx = i, val = door_saving.ToString("#,##0") }); ; //절감량 
                    Door_data[1].Add(new { idx = i, val = (door_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "door_saving", data = Door_data[0] });
                    data.Add(new { cname = "door_savingpercent", data = Door_data[1] });
                    charts += "{donut:" + d + "},";
                    double door_tCO2_elec = door_saving_elec * 0.4747 / 1000000 * 1000;
                    double door_TOE_elec = door_saving_elec * 0.00023;

                    double door_tCO2_noelec = door_saving_noelec / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    double door_TOE_noelec = door_saving_noelec / 43.1 / 0.277778 * 0.00103;
                    double door_tCO2 = door_tCO2_elec + door_tCO2_noelec;
                    double door_TOE = door_TOE_elec + door_TOE_noelec;
                    Door_data[2].Add(new { idx = i, val = door_tCO2.ToString("0.0") });  //tco2
                    Door_data[3].Add(new { idx = i, val = door_TOE.ToString("0.0") });  //TOE 
                    data.Add(new { cname = "door_tco2", data = Door_data[2] });
                    data.Add(new { cname = "door_toe", data = Door_data[3] });

                    Value = Program.DB.querySQL(DB.type.ProjDB, "SELECT DISTINCT a.명칭,a.문유효열관류율,a.법규열관류율,a.번호,a.문면적,a.출입문재질, a.문짝내부유형,a.Type From ConstructionDoor as a  Inner Join ZoneEnvelope_3D as b  on a.번호 = b.구조체번호  where b.외피유형 ='외부출입문' Order by a.문유효열관류율 DESC");
                    string[] door_num = new string[8]; string[] door_name = new string[8]; double[] door_ueff = new double[8]; double[] door_ueff_law = new double[8]; double[] door_area = new double[8]; double[] door_count = new double[8]; string[] door_type = new string[8];
                    double door_area_sum = 0; double door_count_sum = 0; string[] door_retype = new string[8]; double door_area_sum_law = 0;
                    if (Value.Length > 0)
                    {
                        for (int k = 0; k < Value.Length; k++)
                        {
                            door_name[k] = Value[k][0];
                            door_retype[k] = Value[k][7];
                            door_ueff[k] = Convert.ToDouble(Value[k][1]);
                            door_area[k] = Convert.ToDouble(Value[k][4]);
                            if (Value[k][5] != "")
                            { door_type[k] = Value[k][5] + "_" + Value[k][6]; }
                            else { door_type[k] = ""; }
                            door_ueff_law[k] = Convert.ToDouble(Value[0][2]);
                            door_num[k] = Value[k][3];
                            string[][] valuek = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "외피유형='외부출입문' And 구조체번호='" + Value[k][3] + "'");
                            if (valuek.Length > 0)
                            {
                                door_count[k] = valuek.Length;
                            }
                        }

                        for (int a = 0; a < 8; a++)
                        {
                            Door_data[4 + a].Add(new { idx = i, val = door_name[a] });//명칭
                            data.Add(new { cname = "door_name" + a, data = Door_data[4 + a] });
                            if (door_name[a] != null && door_name[a] != "")
                            {
                                Door_data[12 + a].Add(new { idx = i, val = door_area[a].ToString("0.0") });//면적
                                data.Add(new { cname = "door_area" + a, data = Door_data[12 + a] });
                                door_area_sum += door_area[a] * door_count[a];
                                door_area_sum_law += door_area[a] * door_count[a];

                                Door_data[20 + a].Add(new { idx = i, val = door_count[a].ToString("0") });//개수
                                data.Add(new { cname = "door_count" + a, data = Door_data[20 + a] });
                                door_count_sum += door_count[a];

                                Door_data[28 + a].Add(new { idx = i, val = door_type[a].ToString() });//특징
                                data.Add(new { cname = "door_type" + a, data = Door_data[28 + a] });
                            }
                        }
                        Door_data[36].Add(new { idx = i, val = door_count_sum.ToString("0") });//면적합계
                        Door_data[37].Add(new { idx = i, val = "100 %" });//면적율합계
                        data.Add(new { cname = "door_count_sum", data = Door_data[36] });
                        data.Add(new { cname = "door_area_sum_percent", data = Door_data[37] });

                        for (int a = 0; a < 8; a++)
                        {
                            if (door_name[a] != null && door_name[a] != "")
                            {
                                Door_data[38 + a].Add(new { idx = i, val = ((door_area[a] * door_count[a]) / door_area_sum * 100).ToString("0") + " %" });//면적율
                                data.Add(new { cname = "door_area_percent" + a, data = Door_data[38 + a] });
                            }
                        }

                        for (int a = 0; a < 8; a++)
                        {
                            if (door_name[a] != null && door_name[a] != "")
                            {
                                Door_data[46 + a].Add(new { idx = i, val = door_ueff[a].ToString("0.00") });//계획열관류율
                                data.Add(new { cname = "door_ueff" + a, data = Door_data[46 + a] });
                                Door_data[54 + a].Add(new { idx = i, val = door_ueff_law[a].ToString("0.00") });
                                data.Add(new { cname = "door_ueff_law" + a, data = Door_data[54 + a] });//법규열관류율

                            }
                        }
                        double door_ueff_avg = 0;
                        double door_ueff_law_avg = 0;
                        for (int a = 0; a < 8; a++)
                        {
                            door_ueff_avg += door_ueff[a] * (door_area[a] * door_count[a]) / door_area_sum;
                            door_ueff_law_avg += door_ueff_law[a] * (door_area[a] * door_count[a]) / door_area_sum_law;
                        }
                        Door_data[62].Add(new { idx = i, val = door_ueff_avg.ToString("0.00") });//계획열관류율 평균
                        Door_data[63].Add(new { idx = i, val = door_ueff_law_avg.ToString("0.00") });//법규열관류율 평균
                        data.Add(new { cname = "door_ueff_avg", data = Door_data[62] });
                        data.Add(new { cname = "door_ueff_law_avg", data = Door_data[63] });

                        double door_law_avg = 0;
                        for (int a = 0; a < 8; a++)
                        {
                            if (door_name[a] != null && door_name[a] != "")
                            {
                                string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "ConstructionDoor", "법규열관류율", "번호 ='" + door_num[a] + "'");
                                if (value2.Length > 0)
                                {
                                    d = Math.Min(100, (Convert.ToDouble(value2[0][0]) / door_ueff[a] * 100));
                                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                                    Door_data[73 + a].Add(new { idx = i, val = sp });//법규대비 성능점수
                                    data.Add(new { cname = "door_law_point" + a, data = Door_data[73 + a] });
                                    door_law_avg += Convert.ToDouble(value2[0][0]) * (door_area[a] * door_count[a]) / door_area_sum;
                                }
                            }
                        }
                        d = Math.Min(100, (door_law_avg / door_ueff_avg * 100));
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        Door_data[81].Add(new { idx = i, val = sp });//법규대비 성능점수 평균
                        data.Add(new { cname = "door_law_point_avg", data = Door_data[81] });

                        double east = 0, west = 0, south = 0, north = 0;
                        string[][] area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='동' and 외피유형='외부출입문'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            east = Convert.ToDouble(area[0][0]);
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='서' and 외피유형='외부출입문'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            west = Convert.ToDouble(area[0][0]);
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='남' or 방위 ='남동' or 방위 ='남서') and 외피유형='외부출입문'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            south = Convert.ToDouble(area[0][0]);
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='북' or 방위 ='북동' or 방위 ='북서')and 외피유형='외부출입문'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            north = Convert.ToDouble(area[0][0]);
                        }
                        Door_data[82].Add(new { idx = i, val = east.ToString("0.0") });
                        data.Add(new { cname = "door_east", data = Door_data[82] });
                        Door_data[83].Add(new { idx = i, val = west.ToString("0.0") });
                        data.Add(new { cname = "door_west", data = Door_data[83] });
                        Door_data[84].Add(new { idx = i, val = south.ToString("0.0") });
                        data.Add(new { cname = "door_south", data = Door_data[84] });
                        Door_data[85].Add(new { idx = i, val = north.ToString("0.0") });
                        data.Add(new { cname = "door_north", data = Door_data[85] });

                    }

                    #endregion


                    items.Add("Element_Win2.htm");
                    s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                    s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
                    System.Text.Json.JsonSerializer.Serialize(Win_data[10].ToArray());

                    Debug.Print("start");
                    if (charts != "") charts += ",";
                    script = "init(" + s + "," + s2 + "," + "[" + charts + "])";
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


            string script = null;
            string s, s2;
            string[][] 번호 = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
            string[][] res = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기존프로젝트");
            List<object> items = new List<object>();
            List<object> items2 = new List<object>();
            List<object> data = new List<object>();
            List<object>[] Win_data = new List<object>[700];
            List<object>[] CW_data = new List<object>[700];
            List<object>[] Door_data = new List<object>[700];
            double d;
            string sp;
            int i = -1, n;
            while (++i < 700)
            {
                Win_data[i] = new List<object>();
                CW_data[i] = new List<object>();
                Door_data[i] = new List<object>();
            }
            string charts = "";
            i = -1;
            while (++i < 번호.Length)
            {
                if (res.Length > 0)
                {
                    #region 창호                                
                    int j_창호 = 0;
                    for (int a = 0; a < ElementAlt.Length; a++)
                    {
                        if (ElementAlt[a] == "창호")
                        {
                            j_창호 = a; break;
                        }
                    }

                    double win_saving = Math.Max(Element_EnergySaving[j_창호], 0);
                    d = (win_saving / Total_Energy_pre * 100);
                    Win_data[0].Add(new { idx = i, val = win_saving.ToString("#,##0") }); ; //절감량 
                    Win_data[1].Add(new { idx = i, val = (win_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "win_saving", data = Win_data[0] });
                    data.Add(new { cname = "win_savingpercent", data = Win_data[1] });

                    charts += "{donut:" + d + "},";

                    double win_saving_elec = Math.Max(Element_ElecSaving[j_창호], 0);
                    double win_saving_noelec = Math.Max(Element_GasSaving[j_창호], 0);

                    double win_tCO2_elec = win_saving_elec * 0.4747 / 1000000 * 1000;
                    double win_TOE_elec = win_saving_elec * 0.00023;

                    double win_tCO2_noelec = win_saving_noelec / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    double win_TOE_noelec = win_saving_noelec / 43.1 / 0.277778 * 0.00103;
                    double win_tCO2 = win_tCO2_elec + win_tCO2_noelec;
                    double win_TOE = win_TOE_elec + win_TOE_noelec;
                    Win_data[2].Add(new { idx = i, val = win_tCO2.ToString("0.0") });  //tco2
                    Win_data[3].Add(new { idx = i, val = win_TOE.ToString("0.0") });  //TOE 
                    data.Add(new { cname = "win_tco2", data = Win_data[2] });
                    data.Add(new { cname = "win_toe", data = Win_data[3] });
                    string[][] 상위창호 = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "번호", "");
                    if (상위창호.Length > 0)
                    {
                        double[] Ueff_avg_상위창호 = new double[상위창호.Length];
                        for (int a = 0; a < 상위창호.Length; a++)
                        {
                            double sum_면적 = 0;
                            string[][] Value2 = Program.DB.querySQL(DB.type.ProjDB, "SELECT 창호유효열관류율,창호면적 From SubWindow where 상위창호번호='" + 상위창호[a][0] + "'");
                            if (Value2.Length == 0)
                            {
                                Value2 = Program.DB.querySQL(res[0][0], "SELECT 창호유효열관류율,창호면적 From SubWindow where 상위창호번호='" + 상위창호[a][0] + "'");
                            }

                            for (int b = 0; b < Value2.Length; b++)
                            {
                                Ueff_avg_상위창호[a] += Convert.ToDouble(Value2[b][0]) * Convert.ToDouble(Value2[b][1]);
                                sum_면적 += Convert.ToDouble(Value2[b][1]);
                            }
                            Ueff_avg_상위창호[a] = Ueff_avg_상위창호[a] / sum_면적;
                        }

                        string[][] kk = Program.DB.querySQL(DB.type.ProjDB, "SELECT DISTINCT a.상위창호번호 From SubWindow as a  Inner Join ZoneEnvelope_3D as b  on a.번호 = b.구조체번호  where b.외피유형 ='창호' Order by a.창호유효열관류율 DESC");
                        string[] win_name = new string[8]; double[] win_ueff = new double[8]; double[] win_ueff_old = new double[8]; double[] win_area = new double[8]; double[] win_count = new double[8]; double[] win_saving_element = new double[8];
                        string[] win_frame = new string[8]; string[] win_glass = new string[8]; double[] win_shgc = new double[8]; string[] win_retype = new string[8];
                        double win_area_sum = 0; double win_count_sum = 0; double win_area_sum_old = 0;
                        if (kk.Length > 0)
                        {
                            for (int k = 0; k < kk.Length; k++)
                            {
                                string[][] main_value = Program.DB.querySQL(DB.type.ProjDB, "SELECT 창호명칭,기존창호,유리종류,프레임유형,태양열취득률,Type From ConstructionWindow where 번호 ='" + kk[k][0] + "'");
                                win_name[k] = main_value[0][0];
                                win_glass[k] = main_value[0][2];
                                win_frame[k] = main_value[0][3];
                                win_shgc[k] = Convert.ToDouble(main_value[0][4]);
                                win_retype[k] = main_value[0][5];
                                for (int a = 0; a < 상위창호.Length; a++)
                                {
                                    if (kk[k][0] == 상위창호[a][0])
                                    {
                                        win_ueff[k] = Ueff_avg_상위창호[a];
                                    }
                                }
                                if (win_retype[k] != "신규")
                                {
                                    if (main_value[0][1] != "")
                                    {
                                        string[][] prewin = Program.DB.querySQL(res[0][0], "SELECT 번호 From ConstructionWindow where 창호명칭 ='" + main_value[0][1] + "'");
                                        for (int a = 0; a < 상위창호.Length; a++)
                                        {
                                            if (prewin[0][0] == 상위창호[a][0])
                                            {
                                                win_ueff_old[k] = Ueff_avg_상위창호[a];
                                            }
                                        }
                                    }
                                    else
                                    {
                                        win_ueff_old[k] = win_ueff[k];
                                    }
                                }
                                string[][] valuek = Program.DB.querySQL(DB.type.ProjDB, "SELECT b.면적 From ZoneEnvelope_3D as b  Inner Join SubWindow as a  on a.번호 = b.구조체번호  where b.외피유형 = '창호' And a.상위창호번호 ='" + kk[k][0] + "'");
                                if (valuek.Length > 0)
                                {
                                    for (int a = 0; a < valuek.Length; a++)
                                    {
                                        win_area[k] += Convert.ToDouble(valuek[a][0]);
                                    }
                                    win_count[k] = valuek.Length;
                                }
                            }

                            for (int a = 0; a < 8; a++)
                            {
                                Win_data[4 + a].Add(new { idx = i, val = win_name[a] });//명칭
                                Win_data[12 + a].Add(new { idx = i, val = win_glass[a] });//유리
                                Win_data[20 + a].Add(new { idx = i, val = win_frame[a] });//프레임
                                if (win_shgc[a] != 0)
                                { Win_data[28 + a].Add(new { idx = i, val = win_shgc[a].ToString("0.00") }); }//태양열취득률
                                else { Win_data[28 + a].Add(new { idx = i, val = "" }); }
                                data.Add(new { cname = "win_name" + a, data = Win_data[4 + a] });
                                data.Add(new { cname = "win_glass" + a, data = Win_data[12 + a] });
                                data.Add(new { cname = "win_frame" + a, data = Win_data[20 + a] });
                                data.Add(new { cname = "win_shgc" + a, data = Win_data[28 + a] });

                                if (win_name[a] != null && win_name[a] != "")
                                {
                                    Win_data[36 + a].Add(new { idx = i, val = win_count[a].ToString("0") });//개수
                                    data.Add(new { cname = "win_count" + a, data = Win_data[36 + a] });
                                    win_area_sum += win_area[a];
                                    if (win_retype[a] != "신규") { win_area_sum_old += win_area[a]; }
                                    win_count_sum += win_count[a];
                                }
                            }
                            Win_data[44].Add(new { idx = i, val = win_count_sum.ToString("0") });//개수합계
                            if (win_area_sum < 0)
                            {

                                Win_data[45].Add(new { idx = i, val = 0.ToString("0") + " %" });//면적율합계
                            }
                            else
                            {
                                Win_data[45].Add(new { idx = i, val = (win_area_sum / win_area_sum * 100).ToString("0") + " %" });//면적율합계
                            }

                            data.Add(new { cname = "win_count_sum", data = Win_data[44] });
                            data.Add(new { cname = "win_area_sum_percent", data = Win_data[45] });

                            for (int a = 0; a < 8; a++)
                            {
                                if (win_name[a] != null && win_name[a] != "")
                                {
                                    Win_data[46 + a].Add(new { idx = i, val = (win_area[a] / win_area_sum * 100).ToString("0") + " %" });//면적율
                                    data.Add(new { cname = "win_area_percent" + a, data = Win_data[46 + a] });

                                    if (win_ueff_old[a] != 0)
                                    {
                                        win_saving_element[a] = win_area[a] / win_area_sum * (win_ueff_old[a] - win_ueff[a]);
                                    }
                                }
                            }

                            for (int a = 0; a < 8; a++)
                            {
                                if (win_name[a] != null && win_name[a] != "")
                                {
                                    Win_data[54 + a].Add(new { idx = i, val = win_ueff[a].ToString("0.00") });//계획열관류율
                                    data.Add(new { cname = "win_ueff" + a, data = Win_data[54 + a] });
                                    if (win_retype[a] != "신규")
                                    { Win_data[62 + a].Add(new { idx = i, val = win_ueff_old[a].ToString("0.00") }); }
                                    else { Win_data[62 + a].Add(new { idx = i, val = "-" }); }
                                    data.Add(new { cname = "win_ueff_old" + a, data = Win_data[62 + a] });//기존열관류율
                                }
                            }
                            double win_ueff_avg = 0;
                            double win_ueff_old_avg = 0;
                            double win_shgc_avg = 0;
                            for (int a = 0; a < 8; a++)
                            {
                                win_ueff_avg += win_ueff[a] * win_area[a] / win_area_sum;
                                win_ueff_old_avg += win_ueff_old[a] * win_area[a] / win_area_sum_old;
                                win_shgc_avg += win_shgc[a] * win_area[a] / win_area_sum;
                            }
                            Win_data[70].Add(new { idx = i, val = win_ueff_avg.ToString("0.00") });//계획열관류율 평균
                            Win_data[71].Add(new { idx = i, val = win_ueff_old_avg.ToString("0.00") });//기존열관류율 평균
                            Win_data[72].Add(new { idx = i, val = win_shgc_avg.ToString("0.00") });//기존열관류율 평균
                            data.Add(new { cname = "win_ueff_avg", data = Win_data[70] });
                            data.Add(new { cname = "win_ueff_old_avg", data = Win_data[71] });
                            data.Add(new { cname = "win_shgc_avg", data = Win_data[72] });

                            double sum = 0;
                            for (int a = 0; a < 8; a++)
                            {
                                sum += win_saving_element[a];
                            }
                            for (int a = 0; a < 8; a++)
                            {
                                if (win_name[a] != null && win_name[a] != "")
                                {
                                    if (sum != 0)
                                    {
                                        Win_data[73 + a].Add(new { idx = i, val = ((win_saving / Total_Energy_pre) * (win_saving_element[a] / sum) * 100).ToString("0.0") + " %" });//요소기술별 에너지절감률
                                    }
                                    else
                                    {
                                        Win_data[73 + a].Add(new { idx = i, val = (0).ToString("0.0") + " %" });//요소기술별 에너지절감률
                                    }
                                    data.Add(new { cname = "win_saving_element" + a, data = Win_data[73 + a] });
                                }
                            }
                            Win_data[81].Add(new { idx = i, val = (win_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });  //요소기술별 절감률 합계
                            data.Add(new { cname = "win_saving_element_sum", data = Win_data[81] });

                            double win_law_avg = 0;
                            for (int a = 0; a < 8; a++)
                            {
                                if (win_name[a] != null && win_name[a] != "")
                                {
                                    string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "법규열관류율", "번호 ='" + kk[a][0] + "'");
                                    if (value2.Length > 0)
                                    {
                                        d = Math.Min(100, Convert.ToDouble(value2[0][0]) / win_ueff[a] * 100);
                                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";

                                        Win_data[89 + a].Add(new { idx = i, val = sp });//법규대비 성능점수
                                        data.Add(new { cname = "win_law_point" + a, data = Win_data[89 + a] });
                                        win_law_avg += Convert.ToDouble(value2[0][0]) * win_area[a] / win_area_sum;
                                    }
                                }
                            }
                            d = Math.Min(100, (win_law_avg / win_ueff_avg * 100));
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";

                            Win_data[97].Add(new { idx = i, val = sp });//법규대비 성능점수 평균
                            data.Add(new { cname = "win_law_point_avg", data = Win_data[97] });

                            double east = 0, west = 0, south = 0, north = 0;
                            double east_p = 0, west_p = 0, south_p = 0, north_p = 0;

                            string[][] area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='동' and 외피유형='창호'");
                            if (area.Length > 0 && area[0][0] != "")
                            {
                                east = Convert.ToDouble(area[0][0]);
                            }
                            area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='서' and 외피유형='창호'");
                            if (area.Length > 0 && area[0][0] != "")
                            {
                                west = Convert.ToDouble(area[0][0]);
                            }
                            area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='남' or 방위 ='남동' or 방위 ='남서') and 외피유형='창호'");
                            if (area.Length > 0 && area[0][0] != "")
                            {
                                south = Convert.ToDouble(area[0][0]);
                            }
                            area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='북' or 방위 ='북동' or 방위 ='북서')and 외피유형='창호'");
                            if (area.Length > 0 && area[0][0] != "")
                            {
                                north = Convert.ToDouble(area[0][0]);
                            }

                            area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='동' and not 외피유형='최하층바닥'and not 외피유형='층간바닥' and not 외피유형='내벽'and not 외피유형='지붕'");
                            if (area.Length > 0 && area[0][0] != "")
                            {
                                east_p = east * 100 / Convert.ToDouble(area[0][0]);
                            }
                            area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='서' and not 외피유형='최하층바닥'and not 외피유형='층간바닥' and not 외피유형='내벽'and not 외피유형='지붕'");
                            if (area.Length > 0 && area[0][0] != "")
                            {
                                west_p = west * 100 / Convert.ToDouble(area[0][0]);
                            }
                            area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='남' or 방위 ='남동' or 방위 ='남서') and not 외피유형='최하층바닥'and not 외피유형='층간바닥' and not 외피유형='내벽'and not 외피유형='지붕'");
                            if (area.Length > 0 && area[0][0] != "")
                            {
                                south_p = south * 100 / Convert.ToDouble(area[0][0]);
                            }
                            area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='북' or 방위 ='북동' or 방위 ='북서') and not 외피유형='최하층바닥'and not 외피유형='층간바닥' and not 외피유형='내벽'and not 외피유형='지붕'");
                            if (area.Length > 0 && area[0][0] != "")
                            {
                                north_p = north * 100 / Convert.ToDouble(area[0][0]);
                            }

                            Win_data[98].Add(new { idx = i, val = east.ToString("0.0") + " m"+ Program.UTIL.Subscript(2, true) });
                            data.Add(new { cname = "win_east", data = Win_data[98] });
                            Win_data[99].Add(new { idx = i, val = west.ToString("0.0") + " m"+ Program.UTIL.Subscript(2, true) });
                            data.Add(new { cname = "win_west", data = Win_data[99] });
                            Win_data[100].Add(new { idx = i, val = south.ToString("0.0") + " m"+ Program.UTIL.Subscript(2, true) });
                            data.Add(new { cname = "win_south", data = Win_data[100] });
                            Win_data[101].Add(new { idx = i, val = north.ToString("0.0") + " m"+ Program.UTIL.Subscript(2, true) });
                            data.Add(new { cname = "win_north", data = Win_data[101] });

                            Win_data[102].Add(new { idx = i, val = east_p.ToString("0.0") + " %" });
                            data.Add(new { cname = "win_east_p", data = Win_data[102] });
                            Win_data[103].Add(new { idx = i, val = west_p.ToString("0.0") + " %" });
                            data.Add(new { cname = "win_west_p", data = Win_data[103] });
                            Win_data[104].Add(new { idx = i, val = south_p.ToString("0.0") + " %" });
                            data.Add(new { cname = "win_south_p", data = Win_data[104] });
                            Win_data[105].Add(new { idx = i, val = north_p.ToString("0.0") + " %" });
                            data.Add(new { cname = "win_north_p", data = Win_data[105] });

                            double totalarea = 0;
                            area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where not 외피유형='최하층바닥'and not 외피유형='층간바닥' and not 외피유형='내벽'");
                            if (area.Length > 0 && area[0][0] != "")
                            {
                                totalarea = Convert.ToDouble(area[0][0]);
                            }
                            Win_data[106].Add(new { idx = i, val = (win_area_sum / totalarea * 100).ToString("0.0") });
                            data.Add(new { cname = "win_openpercent", data = Win_data[106] });

                        }
                    }

                    #endregion

                    #region 커튼월창
                    int j_커튼월창 = 0;
                    for (int a = 0; a < ElementAlt.Length; a++)
                    {
                        if (ElementAlt[a] == "커튼월창")
                        {
                            j_커튼월창 = a; break;
                        }
                    }
                    double cw_saving = Math.Max(Element_EnergySaving[j_커튼월창], 0);
                    d = (cw_saving / Total_Energy_pre * 100);
                    CW_data[0].Add(new { idx = i, val = cw_saving.ToString("#,##0") }); ; //절감량 
                    CW_data[1].Add(new { idx = i, val = (cw_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "cw_saving", data = CW_data[0] });
                    data.Add(new { cname = "cw_savingpercent", data = CW_data[1] });

                    charts += "{donut:" + d + "},";

                    double cw_saving_elec = Math.Max(Element_ElecSaving[j_커튼월창], 0);
                    double cw_saving_noelec = Math.Max(Element_GasSaving[j_커튼월창], 0);

                    double cw_tCO2_elec = cw_saving_elec * 0.4747 / 1000000 * 1000;
                    double cw_TOE_elec = cw_saving_elec * 0.00023;

                    double cw_tCO2_noelec = cw_saving_noelec / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    double cw_TOE_noelec = cw_saving_noelec / 43.1 / 0.277778 * 0.00103;
                    double cw_tCO2 = cw_tCO2_elec + cw_tCO2_noelec;
                    double cw_TOE = cw_TOE_elec + cw_TOE_noelec;
                    CW_data[2].Add(new { idx = i, val = cw_tCO2.ToString("0.0") });  //tco2
                    CW_data[3].Add(new { idx = i, val = cw_TOE.ToString("0.0") });  //TOE 
                    data.Add(new { cname = "cw_tco2", data = CW_data[2] });
                    data.Add(new { cname = "cw_toe", data = CW_data[3] });

                    string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "SELECT DISTINCT a.명칭,a.유리부분유효열관류율,a.기존커튼월,a.번호,b.커튼월부위,a.Type From ConstructionCW as a  Inner Join ZoneEnvelope_3D as b  on a.번호 = b.구조체번호  where b.외피유형 ='커튼월창' Order by a.커튼월창유효열관류율 DESC");
                    string[] cw_num = new string[8]; string[] cw_name = new string[8]; double[] cw_ueff = new double[8]; double[] cw_ueff_old = new double[8]; double[] cw_area = new double[8]; double[] cw_saving_element = new double[8]; double[] cw_shgc = new double[8]; string[] cw_frame = new string[8]; string[] cw_glass = new string[8]; string[] cw_part = new string[8];
                    double cw_area_sum = 0; string[] cw_retype = new string[8]; double cw_area_sum_old = 0; double[] cw_ueff_law = new double[8];
                    if (Value.Length > 0)
                    {
                        for (int k = 0; k < Value.Length; k++)
                        {
                            cw_name[k] = Value[k][0] + "_" + Value[k][4];
                            cw_retype[k] = Value[k][5];
                            cw_num[k] = Value[k][3];
                            cw_part[k] = Value[k][4];
                            if (cw_part[k] == "유리부분")
                            {
                                string[][] value3 = Program.DB.querySQL(DB.type.ProjDB, "SELECT 유리부분유효열관류율,프레임유형,태양열취득률,고정유리종류,법규유리부분열관류율 From ConstructionCW  where 번호='" + Value[k][3] + "'");
                                cw_ueff[k] = Convert.ToDouble(value3[0][0]);
                                cw_frame[k] = value3[0][1];
                                cw_shgc[k] = Convert.ToDouble(value3[0][2]);
                                cw_glass[k] = value3[0][3];
                                cw_ueff_law[k] = Convert.ToDouble(value3[0][4]);
                            }
                            else if (cw_part[k] == "패널부분")
                            {
                                string[][] value3 = Program.DB.querySQL(DB.type.ProjDB, "SELECT 패널부분유효열관류율,프레임유형,패널유리종류,법규패널부분열관류율 From ConstructionCW  where 번호='" + Value[k][3] + "'");
                                cw_ueff[k] = Convert.ToDouble(value3[0][0]);
                                cw_frame[k] = value3[0][1];
                                cw_glass[k] = value3[0][2];
                                cw_ueff_law[k] = Convert.ToDouble(value3[0][3]);
                            }
                            else
                            {
                                string[][] value3 = Program.DB.querySQL(DB.type.ProjDB, "SELECT 출입문부분유효열관류율,프레임유형,출입문태양열취득률,패널유리종류,법규출입문부분열관류율 From ConstructionCW  where 번호='" + Value[k][3] + "'");
                                cw_ueff[k] = Convert.ToDouble(value3[0][0]);
                                cw_frame[k] = value3[0][1];
                                cw_shgc[k] = Convert.ToDouble(value3[0][2]);
                                cw_glass[k] = value3[0][3];
                                cw_ueff_law[k] = Convert.ToDouble(value3[0][4]);
                            }
                            if (cw_retype[k] != "신규 커튼월창")
                            {
                                if (Value[k][2] != "")
                                {
                                    string[][] value2;
                                    if (Value[k][4] == "유리부분")
                                    {
                                        value2 = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "유리부분유효열관류율", "명칭 ='" + Value[k][2] + "'");
                                    }
                                    else if (Value[k][4] == "패널부분")
                                    {
                                        value2 = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "패널부분유효열관류율", "명칭 ='" + Value[k][2] + "'");
                                    }
                                    else
                                    {
                                        value2 = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "출입문부분유효열관류율", "명칭 ='" + Value[k][2] + "'");
                                    }

                                    if (value2.Length > 0)
                                    {
                                        cw_ueff_old[k] = Convert.ToDouble(value2[0][0]);
                                    }
                                }
                                else
                                {
                                    cw_ueff_old[k] = cw_ueff[k];
                                }
                            }

                            string[][] valuek = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "외피유형='커튼월창' And 구조체번호='" + Value[k][3] + "'");
                            if (valuek.Length > 0)
                            {
                                for (int a = 0; a < valuek.Length; a++)
                                { cw_area[k] += Convert.ToDouble(valuek[a][0]); }
                            }
                        }

                        for (int a = 0; a < 8; a++)
                        {
                            CW_data[4 + a].Add(new { idx = i, val = cw_name[a] });//명칭
                            data.Add(new { cname = "cw_name" + a, data = CW_data[4 + a] });
                            CW_data[12 + a].Add(new { idx = i, val = cw_frame[a] });//프레임
                            data.Add(new { cname = "cw_frame" + a, data = CW_data[12 + a] });
                            CW_data[20 + a].Add(new { idx = i, val = cw_glass[a] });//유리
                            data.Add(new { cname = "cw_glass" + a, data = CW_data[20 + a] });

                            if (cw_name[a] != null && cw_name[a] != "")
                            {
                                CW_data[28 + a].Add(new { idx = i, val = cw_area[a].ToString("0.0") });//면적
                                data.Add(new { cname = "cw_area" + a, data = CW_data[28 + a] });
                                cw_area_sum += cw_area[a];
                                if (cw_retype[a] != "신규 커튼월창")
                                {
                                    cw_area_sum_old += cw_area[a];
                                }
                            }
                        }
                        CW_data[36].Add(new { idx = i, val = cw_area_sum.ToString("0.0") });//면적합계
                        CW_data[37].Add(new { idx = i, val = "100 %" });//면적율합계
                        data.Add(new { cname = "cw_area_sum", data = CW_data[36] });
                        data.Add(new { cname = "cw_area_sum_percent", data = CW_data[37] });

                        for (int a = 0; a < 8; a++)
                        {
                            if (cw_name[a] != null && cw_name[a] != "")
                            {
                                CW_data[45 + a].Add(new { idx = i, val = (cw_area[a] / cw_area_sum * 100).ToString("0") + " %" });//면적율
                                data.Add(new { cname = "cw_area_percent" + a, data = CW_data[45 + a] });

                                if (cw_ueff_old[a] != 0)
                                {
                                    cw_saving_element[a] = cw_area[a] / cw_area_sum * (cw_ueff_old[a] - cw_ueff[a]);
                                }
                            }
                        }

                        for (int a = 0; a < 8; a++)
                        {
                            if (cw_name[a] != null && cw_name[a] != "")
                            {
                                CW_data[53 + a].Add(new { idx = i, val = cw_ueff[a].ToString("0.00") });//계획열관류율
                                data.Add(new { cname = "cw_ueff" + a, data = CW_data[53 + a] });
                                if (cw_retype[a] != "신규 커튼월창")
                                { CW_data[61 + a].Add(new { idx = i, val = cw_ueff_old[a].ToString("0.00") }); }
                                else { CW_data[61 + a].Add(new { idx = i, val = "-" }); }
                                data.Add(new { cname = "cw_ueff_old" + a, data = CW_data[61 + a] });//기존열관류율
                                CW_data[69 + a].Add(new { idx = i, val = cw_shgc[a].ToString("0.00") });//태양열취득률
                                data.Add(new { cname = "cw_shgc" + a, data = CW_data[69 + a] });
                            }
                        }
                        double cw_ueff_avg = 0;
                        double cw_ueff_old_avg = 0;
                        double cw_shgc_avg = 0;
                        double area_shgc = 0;
                        for (int a = 0; a < 8; a++)
                        {
                            cw_ueff_avg += cw_ueff[a] * cw_area[a] / cw_area_sum;
                            cw_ueff_old_avg += cw_ueff_old[a] * cw_area[a] / cw_area_sum_old;
                            if (cw_part[a] != "패널부분")
                            {
                                area_shgc += cw_area[a];
                                cw_shgc_avg += cw_shgc[a] * cw_area[a];
                            }
                        }
                        if (area_shgc != 0)
                        { cw_shgc_avg = cw_shgc_avg / area_shgc; }

                        CW_data[77].Add(new { idx = i, val = cw_ueff_avg.ToString("0.00") });//계획열관류율 평균
                        CW_data[78].Add(new { idx = i, val = cw_ueff_old_avg.ToString("0.00") });//기존열관류율 평균
                        CW_data[79].Add(new { idx = i, val = cw_shgc_avg.ToString("0.00") });//태양열취득률 평균
                        data.Add(new { cname = "cw_ueff_avg", data = CW_data[77] });
                        data.Add(new { cname = "cw_ueff_old_avg", data = CW_data[78] });
                        data.Add(new { cname = "cw_shgc_avg", data = CW_data[79] });

                        double sum = 0;
                        for (int a = 0; a < 8; a++)
                        {
                            sum += cw_saving_element[a];
                        }
                        for (int a = 0; a < 8; a++)
                        {
                            if (cw_name[a] != null && cw_name[a] != "")
                            {
                                if (sum != 0)
                                {
                                    CW_data[80 + a].Add(new { idx = i, val = ((cw_saving / Total_Energy_pre) * (cw_saving_element[a] / sum) * 100).ToString("0.0") + " %" });//요소기술별 에너지절감률
                                }
                                else
                                {
                                    CW_data[80 + a].Add(new { idx = i, val = (0).ToString("0.0") + " %" });//요소기술별 에너지절감률
                                }
                                data.Add(new { cname = "cw_saving_element" + a, data = CW_data[80 + a] });
                            }
                        }
                        CW_data[88].Add(new { idx = i, val = (cw_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });  //요소기술별 절감률 합계
                        data.Add(new { cname = "cw_saving_element_sum", data = CW_data[88] });

                        double cw_law_avg = 0;
                        for (int a = 0; a < 8; a++)
                        {
                            if (cw_name[a] != null && cw_name[a] != "")
                            {
                                d = Math.Min(100, (cw_ueff_law[a] / cw_ueff[a] * 100));
                                if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                                sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";

                                CW_data[89 + a].Add(new { idx = i, val = sp });//법규대비 성능점수
                                data.Add(new { cname = "cw_law_point" + a, data = CW_data[89 + a] });
                                cw_law_avg += cw_ueff_law[a] * cw_area[a] / cw_area_sum;
                            }
                        }
                        d = Math.Min(100, (cw_law_avg / cw_ueff_avg * 100));
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        CW_data[97].Add(new { idx = i, val = sp });//법규대비 성능점수 평균
                        data.Add(new { cname = "cw_law_point_avg", data = CW_data[97] });

                        double east = 0, west = 0, south = 0, north = 0;
                        double east_p = 0, west_p = 0, south_p = 0, north_p = 0;
                        string[][] area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='동' and 외피유형='커튼월창'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            east = Convert.ToDouble(area[0][0]);
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='서' and 외피유형='커튼월창'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            west = Convert.ToDouble(area[0][0]);
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='남' or 방위 ='남동' or 방위 ='남서') and 외피유형='커튼월창'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            south = Convert.ToDouble(area[0][0]);
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='북' or 방위 ='북동' or 방위 ='북서')and 외피유형='커튼월창'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            north = Convert.ToDouble(area[0][0]);
                        }


                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='동'and not 외피유형='최하층바닥'and not 외피유형='층간바닥' and not 외피유형='내벽'and not 외피유형='지붕'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            east_p = east / Convert.ToDouble(area[0][0]) * 100;
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='서'and not 외피유형='최하층바닥'and not 외피유형='층간바닥' and not 외피유형='내벽'and not 외피유형='지붕'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            west_p = west / Convert.ToDouble(area[0][0]) * 100;
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='남' or 방위 ='남동' or 방위 ='남서')and not 외피유형='최하층바닥'and not 외피유형='층간바닥' and not 외피유형='내벽'and not 외피유형='지붕'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            south_p = south / Convert.ToDouble(area[0][0]) * 100;
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='북' or 방위 ='북동' or 방위 ='북서')and not 외피유형='최하층바닥'and not 외피유형='층간바닥' and not 외피유형='내벽'and not 외피유형='지붕'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            north_p = north / Convert.ToDouble(area[0][0]) * 100;
                        }
                        CW_data[98].Add(new { idx = i, val = east.ToString("0.0") + " m"+ Program.UTIL.Subscript(2, true) });
                        data.Add(new { cname = "cw_east", data = CW_data[98] });
                        CW_data[99].Add(new { idx = i, val = west.ToString("0.0") + " m"+ Program.UTIL.Subscript(2, true) });
                        data.Add(new { cname = "cw_west", data = CW_data[99] });
                        CW_data[100].Add(new { idx = i, val = south.ToString("0.0") + " m"+ Program.UTIL.Subscript(2, true) });
                        data.Add(new { cname = "cw_south", data = CW_data[100] });
                        CW_data[101].Add(new { idx = i, val = north.ToString("0.0") + " m"+ Program.UTIL.Subscript(2, true) });
                        data.Add(new { cname = "cw_north", data = CW_data[101] });

                        CW_data[102].Add(new { idx = i, val = east_p.ToString("0.0") + " %" });
                        data.Add(new { cname = "cw_east_p", data = CW_data[102] });
                        CW_data[103].Add(new { idx = i, val = west_p.ToString("0.0") + " %" });
                        data.Add(new { cname = "cw_west_p", data = CW_data[103] });
                        CW_data[104].Add(new { idx = i, val = south_p.ToString("0.0") + " %" });
                        data.Add(new { cname = "cw_south_p", data = CW_data[104] });
                        CW_data[105].Add(new { idx = i, val = north_p.ToString("0.0") + " %" });
                        data.Add(new { cname = "cw_north_p", data = CW_data[105] });

                        double totalarea = 0;
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where not 외피유형='최하층바닥'and not 외피유형='층간바닥' and not 외피유형='내벽'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            totalarea = Convert.ToDouble(area[0][0]);
                        }
                        CW_data[106].Add(new { idx = i, val = (cw_area_sum / totalarea * 100).ToString("0.0") });
                        data.Add(new { cname = "cw_openpercent", data = CW_data[106] });

                    }

                    #endregion


                    #region 외부출입문
                    int j_외부출입문 = 0;
                    for (int a = 0; a < ElementAlt.Length; a++)
                    {
                        if (ElementAlt[a] == "외부출입문")
                        {
                            j_외부출입문 = a; break;
                        }
                    }
                    double door_saving = Math.Max(Element_EnergySaving[j_외부출입문], 0);
                    d = (door_saving / Total_Energy_pre * 100);
                    Door_data[0].Add(new { idx = i, val = door_saving.ToString("#,##0") }); ; //절감량 
                    Door_data[1].Add(new { idx = i, val = (door_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "door_saving", data = Door_data[0] });
                    data.Add(new { cname = "door_savingpercent", data = Door_data[1] });

                    charts += "{donut:" + d + "},";
                    double door_saving_elec = Math.Max(Element_ElecSaving[j_외부출입문], 0);
                    double door_saving_noelec = Math.Max(Element_GasSaving[j_외부출입문], 0);

                    double door_tCO2_elec = door_saving_elec * 0.4747 / 1000000 * 1000;
                    double door_TOE_elec = door_saving_elec * 0.00023;

                    double door_tCO2_noelec = door_saving_noelec / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    double door_TOE_noelec = door_saving_noelec / 43.1 / 0.277778 * 0.00103;
                    double door_tCO2 = door_tCO2_elec + door_tCO2_noelec;
                    double door_TOE = door_TOE_elec + door_TOE_noelec;
                    Door_data[2].Add(new { idx = i, val = door_tCO2.ToString("0.0") });  //tco2
                    Door_data[3].Add(new { idx = i, val = door_TOE.ToString("0.0") });  //TOE 
                    data.Add(new { cname = "door_tco2", data = Door_data[2] });
                    data.Add(new { cname = "door_toe", data = Door_data[3] });

                    Value = Program.DB.querySQL(DB.type.ProjDB, "SELECT DISTINCT a.명칭,a.문유효열관류율,a.기존출입문,a.번호,a.문면적,a.출입문재질, a.문짝내부유형,a.Type From ConstructionDoor as a  Inner Join ZoneEnvelope_3D as b  on a.번호 = b.구조체번호  where b.외피유형 ='외부출입문' Order by a.문유효열관류율 DESC");
                    string[] door_num = new string[8]; string[] door_name = new string[8]; double[] door_ueff = new double[8]; double[] door_ueff_old = new double[8]; double[] door_area = new double[8]; double[] door_saving_element = new double[8]; double[] door_count = new double[8]; string[] door_type = new string[8];
                    double door_area_sum = 0; double door_count_sum = 0; string[] door_retype = new string[8]; double door_area_sum_old = 0;
                    if (Value.Length > 0)
                    {
                        for (int k = 0; k < Value.Length; k++)
                        {
                            door_name[k] = Value[k][0];
                            door_retype[k] = Value[k][7];
                            door_ueff[k] = Convert.ToDouble(Value[k][1]);
                            door_area[k] = Convert.ToDouble(Value[k][4]);
                            if (Value[k][5] != "")
                            { door_type[k] = Value[k][5] + "_" + Value[k][6]; }
                            else { door_type[k] = ""; }
                            if (door_retype[k] != "신규 출입문")
                            {
                                if (Value[k][2] != "")
                                {
                                    string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "ConstructionDoor", "문유효열관류율", "명칭 ='" + Value[k][2] + "'");
                                    if (value2.Length > 0)
                                    {
                                        door_ueff_old[k] = Convert.ToDouble(value2[0][0]);
                                    }
                                }
                                else
                                {
                                    door_ueff_old[k] = door_ueff[k];
                                }
                            }
                            door_num[k] = Value[k][3];
                            string[][] valuek = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "외피유형='외부출입문' And 구조체번호='" + Value[k][3] + "'");
                            if (valuek.Length > 0)
                            {
                                door_count[k] = valuek.Length;
                            }
                        }

                        for (int a = 0; a < 8; a++)
                        {
                            Door_data[4 + a].Add(new { idx = i, val = door_name[a] });//명칭
                            data.Add(new { cname = "door_name" + a, data = Door_data[4 + a] });
                            if (door_name[a] != null && door_name[a] != "")
                            {
                                Door_data[12 + a].Add(new { idx = i, val = door_area[a].ToString("0.0") });//면적
                                data.Add(new { cname = "door_area" + a, data = Door_data[12 + a] });
                                door_area_sum += door_area[a] * door_count[a];
                                if (door_retype[a] != "신규 출입문")
                                {
                                    door_area_sum_old += door_area[a] * door_count[a];
                                }

                                Door_data[20 + a].Add(new { idx = i, val = door_count[a].ToString("0") });//개수
                                data.Add(new { cname = "door_count" + a, data = Door_data[20 + a] });
                                door_count_sum += door_count[a];

                                Door_data[28 + a].Add(new { idx = i, val = door_type[a].ToString() });//특징
                                data.Add(new { cname = "door_type" + a, data = Door_data[28 + a] });
                            }
                        }
                        Door_data[36].Add(new { idx = i, val = door_count_sum.ToString("0") });//면적합계
                        Door_data[37].Add(new { idx = i, val = "100 %" });//면적율합계
                        data.Add(new { cname = "door_count_sum", data = Door_data[36] });
                        data.Add(new { cname = "door_area_sum_percent", data = Door_data[37] });

                        for (int a = 0; a < 8; a++)
                        {
                            if (door_name[a] != null && door_name[a] != "")
                            {
                                Door_data[38 + a].Add(new { idx = i, val = ((door_area[a] * door_count[a]) / door_area_sum * 100).ToString("0") + " %" });//면적율
                                data.Add(new { cname = "door_area_percent" + a, data = Door_data[38 + a] });

                                if (door_ueff_old[a] != 0)
                                {
                                    door_saving_element[a] = door_area[a] / door_area_sum * (door_ueff_old[a] - door_ueff[a]);
                                }
                            }
                        }

                        for (int a = 0; a < 8; a++)
                        {
                            if (door_name[a] != null && door_name[a] != "")
                            {
                                Door_data[46 + a].Add(new { idx = i, val = door_ueff[a].ToString("0.00") });//계획열관류율
                                data.Add(new { cname = "door_ueff" + a, data = Door_data[46 + a] });
                                if (door_retype[a] != "신규 출입문")
                                { Door_data[54 + a].Add(new { idx = i, val = door_ueff_old[a].ToString("0.00") }); }
                                else { Door_data[54 + a].Add(new { idx = i, val = "-" }); }
                                data.Add(new { cname = "door_ueff_old" + a, data = Door_data[54 + a] });//기존열관류율

                            }
                        }
                        double door_ueff_avg = 0;
                        double door_ueff_old_avg = 0;
                        for (int a = 0; a < 8; a++)
                        {
                            door_ueff_avg += door_ueff[a] * (door_area[a] * door_count[a]) / door_area_sum;
                            door_ueff_old_avg += door_ueff_old[a] * (door_area[a] * door_count[a]) / door_area_sum_old;
                        }
                        Door_data[62].Add(new { idx = i, val = door_ueff_avg.ToString("0.00") });//계획열관류율 평균
                        Door_data[63].Add(new { idx = i, val = door_ueff_old_avg.ToString("0.00") });//기존열관류율 평균
                        data.Add(new { cname = "door_ueff_avg", data = Door_data[62] });
                        data.Add(new { cname = "door_ueff_old_avg", data = Door_data[63] });

                        double sum = 0;
                        for (int a = 0; a < 8; a++)
                        {
                            sum += door_saving_element[a];
                        }
                        for (int a = 0; a < 8; a++)
                        {
                            if (door_name[a] != null && door_name[a] != "")
                            {
                                if (sum != 0)
                                {
                                    Door_data[64 + a].Add(new { idx = i, val = ((door_saving / Total_Energy_pre) * (door_saving_element[a] / sum) * 100).ToString("0.0") + " %" });//요소기술별 에너지절감률
                                }
                                else
                                {
                                    Door_data[64 + a].Add(new { idx = i, val = (0).ToString("0.0") + " %" });//요소기술별 에너지절감률
                                }
                                data.Add(new { cname = "door_saving_element" + a, data = Door_data[64 + a] });
                            }
                        }
                        Door_data[72].Add(new { idx = i, val = (door_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });  //요소기술별 절감률 합계
                        data.Add(new { cname = "door_saving_element_sum", data = Door_data[72] });

                        double door_law_avg = 0;
                        for (int a = 0; a < 8; a++)
                        {
                            if (door_name[a] != null && door_name[a] != "")
                            {
                                string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "ConstructionDoor", "법규열관류율", "번호 ='" + door_num[a] + "'");
                                if (value2.Length > 0)
                                {
                                    d = Math.Min(100, (Convert.ToDouble(value2[0][0]) / door_ueff[a] * 100));
                                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                                    Door_data[73 + a].Add(new { idx = i, val = sp });//법규대비 성능점수
                                    data.Add(new { cname = "door_law_point" + a, data = Door_data[73 + a] });
                                    door_law_avg += Convert.ToDouble(value2[0][0]) * (door_area[a] * door_count[a]) / door_area_sum;
                                }
                            }
                        }
                        d = Math.Min(100, (door_law_avg / door_ueff_avg * 100));
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        Door_data[81].Add(new { idx = i, val = sp });//법규대비 성능점수 평균
                        data.Add(new { cname = "door_law_point_avg", data = Door_data[81] });

                        double east = 0, west = 0, south = 0, north = 0;
                        string[][] area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='동' and 외피유형='외부출입문'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            east = Convert.ToDouble(area[0][0]);
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where 방위 ='서' and 외피유형='외부출입문'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            west = Convert.ToDouble(area[0][0]);
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='남' or 방위 ='남동' or 방위 ='남서') and 외피유형='외부출입문'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            south = Convert.ToDouble(area[0][0]);
                        }
                        area = Program.DB.querySQL(DB.type.ProjDB, "SELECT SUM(면적) From ZoneEnvelope_3D  where (방위 ='북' or 방위 ='북동' or 방위 ='북서')and 외피유형='외부출입문'");
                        if (area.Length > 0 && area[0][0] != "")
                        {
                            north = Convert.ToDouble(area[0][0]);
                        }
                        Door_data[82].Add(new { idx = i, val = east.ToString("0.0") });
                        data.Add(new { cname = "door_east", data = Door_data[82] });
                        Door_data[83].Add(new { idx = i, val = west.ToString("0.0") });
                        data.Add(new { cname = "door_west", data = Door_data[83] });
                        Door_data[84].Add(new { idx = i, val = south.ToString("0.0") });
                        data.Add(new { cname = "door_south", data = Door_data[84] });
                        Door_data[85].Add(new { idx = i, val = north.ToString("0.0") });
                        data.Add(new { cname = "door_north", data = Door_data[85] });

                    }

                    #endregion


                    items.Add("Element_Win.htm");
                    s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                    s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
                    System.Text.Json.JsonSerializer.Serialize(Win_data[10].ToArray());

                    Debug.Print("start");
                    if (charts != "") charts += ",";
                    script = "init(" + s + "," + s2 + "," + "[" + charts + "])";
                }
            }
            return script;
        }

    }
}