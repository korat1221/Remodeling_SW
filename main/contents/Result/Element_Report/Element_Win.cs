using Eagle._Components.Public;
using Eagle._Interfaces.Public;
using main.subcontents.ConstructionWindow;
using main.subcontents.CoolingSystem;
using Microsoft.Office.Interop.Excel;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.contents
{
    public partial class Element_Win : Form
    {
        bool scriptable = false;
        public Element_Win()
        {
            InitializeComponent();

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
                string s = args.TryGetWebMessageAsString();
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
            string[][] 프로젝트유형 = Program.DB.querySQL(DB.type.ProjListDB, "Select type from projects where current = '1'");
            if (프로젝트유형[0][0] == "1")
            {
                Report_Before();
            }
            else
            {
                Report_After();
            }
        }

        private void Report_Before()
        {

        }

        private void Report_After()
        {
            string s, s2;
            string[][] 번호 = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
            string[][] res = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기존프로젝트");
            List<object> items = new List<object>();
            List<object> items2 = new List<object>();
            List<object> data = new List<object>();
            List<object>[] Win_data = new List<object>[700];
            List<object>[] CW_data = new List<object>[700];
            List<object>[] Door_data = new List<object>[700];
            string[] ElementAlt = CALC.ElementAlt;
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
                    #region 요소기술별 절감량 비율 계산 
                    double[] Element_ElecSum = new double[ElementAlt.Length];
                    double[] Element_GasSum = new double[ElementAlt.Length];
                    double[] Element_EnergySum = new double[ElementAlt.Length];
                    for (int a =0; a< ElementAlt.Length; a++)
                    {
                        string[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result_Element", "총에너지소요량", "검토유형='" + ElementAlt[a] + "' And 연료='전기'");
                        if (Value2.Length > 0)
                        {
                            for (int k = 0; k < Value2.Length; k++)
                            {
                                Element_ElecSum[a] += Convert.ToDouble(Value2[k][0]);
                            }
                        }
                        Value2 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result_Element", "총에너지소요량", "검토유형='" + ElementAlt[a] + "' And Not 연료='전기' and Not 연료='전체'");
                        if (Value2.Length > 0)
                        {
                            for (int k = 0; k < Value2.Length; k++)
                            {
                                Element_GasSum[a] += Convert.ToDouble(Value2[k][0]);
                            }
                        }

                        Element_EnergySum[a] = Element_ElecSum[a] + Element_GasSum[a];
                    }

                    double Total_Energy_pre = 0; 
                    double Total_EnergySaving = 0;
                    double Total_ElecSaving = 0;
                    double Total_GasSaving = 0;
                    for (int mth =0; mth <12; mth++)
                    {
                        string[][] Final1 = Program.DB.querySQL(res[0][0], "Select 총에너지소요량 from FinalEnergy_Result Where 연료='전기' and 월 ='" + (mth + 1).ToString() + "월'");
                        string[][] Final2 = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 from FinalEnergy_Result Where 연료='전기' and 월 ='" + (mth + 1).ToString() + "월'");
                        if(Final1.Length > 0 &&Final2.Length >0)
                        {
                            Total_Energy_pre += Convert.ToDouble(Final1[0][0]);
                            Total_ElecSaving += (Convert.ToDouble(Final1[0][0]) - Convert.ToDouble(Final2[0][0]));
                        }

                         Final1 = Program.DB.querySQL(res[0][0], "Select 총에너지소요량 from FinalEnergy_Result Where Not 연료='전기' and Not 연료='전체' and 월 ='" + (mth + 1).ToString() + "월'");
                         Final2 = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 from FinalEnergy_Result Where Not 연료='전기' and Not 연료='전체' and 월 ='" + (mth + 1).ToString() + "월'");
                        if (Final1.Length > 0 && Final2.Length > 0)
                        {
                            Total_Energy_pre += Convert.ToDouble(Final1[0][0]);
                            Total_GasSaving += (Convert.ToDouble(Final1[0][0]) - Convert.ToDouble(Final2[0][0]));
                        }

                        Total_EnergySaving = Total_ElecSaving + Total_GasSaving;
                    }
                  
                    double sum_elec = 0;
                    double sum_gas = 0;
                    double sum_energy = 0;
                    for (int a = 1; a < ElementAlt.Length; a++)
                    {
                      sum_elec += Element_ElecSum[0] - Element_ElecSum[a]; // 조닝 대비 절감량 
                      sum_gas += Element_GasSum[0] - Element_GasSum[a];
                      sum_energy += Element_EnergySum[0] - Element_EnergySum[a];
                    }
                    double[] Element_ElecSaving = new double[ElementAlt.Length];
                    double[] Element_GasSaving = new double[ElementAlt.Length];
                    double[] Element_EnergySaving = new double[ElementAlt.Length];
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

                   

                    #region 창호                                
                    int j_창호 = 0;
                    for(int a =0; a< ElementAlt.Length; a++)
                    {
                        if (ElementAlt[a] == "창호")
                        {
                            j_창호 = a; break;
                        }
                    }

                    double win_saving = Element_EnergySaving[j_창호];
                    Win_data[0].Add(new { idx = i, val = win_saving.ToString("0.0") }); ; //절감량 
                    Win_data[1].Add(new { idx = i, val = (win_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "win_saving", data = Win_data[0] });
                    data.Add(new { cname = "win_savingpercent", data = Win_data[1] });

                    double win_saving_elec = Element_ElecSaving[j_창호];
                    double win_saving_noelec = Element_GasSaving[j_창호];

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
                    string[][]상위창호 = Program.DB.getValue(DB.type.ProjDB,"ConstructionWindow", "번호","");
                    if (상위창호.Length > 0 )
                    {
                        double[] Ueff_avg_상위창호 = new double[상위창호.Length];
                        for(int a =0; a < 상위창호.Length; a++)
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
                    string[] win_name = new string[8]; double[] win_ueff = new double[8]; double[] win_ueff_old = new double[8]; double[] win_area = new double[8];double[] win_count = new double[8]; double[] win_saving_element = new double[8];
                    string[] win_frame = new string[8]; string[] win_glass = new string[8]; double[] win_shgc = new double[8]; 
                    double win_area_sum = 0; double win_count_sum = 0;
                    if (kk.Length > 0)
                    {
                            for (int k = 0; k < kk.Length; k++)
                            {
                                string[][] main_value = Program.DB.querySQL(DB.type.ProjDB, "SELECT 창호명칭,기존창호,유리종류,프레임유형,태양열취득률 From ConstructionWindow where 번호 ='" + kk[k][0] + "'");                                
                                win_name[k] = main_value[0][0];
                                win_glass[k] = main_value[0][2];
                                win_frame[k] = main_value[0][3];
                                win_shgc[k] = Convert.ToDouble(main_value[0][4]); 
                                for (int a = 0; a < 상위창호.Length; a++)
                                {
                                    if (kk[k][0] == 상위창호[a][0])
                                    {
                                        win_ueff[k] = Ueff_avg_상위창호[a];
                                    }
                                }

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

                                string[][] valuek = Program.DB.querySQL(DB.type.ProjDB, "SELECT b.면적 From ZoneEnvelope_3D as b  Inner Join SubWindow as a  on a.번호 = b.구조체번호  where b.외피유형 = '창호' And a.상위창호번호 ='" + kk[k][0] + "'");
                                if (valuek.Length > 0)
                                {
                                    for (int a = 0; a < valuek.Length; a++)
                                    { 
                                        win_area[k] += Convert.ToDouble(valuek[a][0]);
                                    }
                                    win_count[k] =valuek.Length;
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
                                    win_count_sum += win_count[a];
                                }
                            }
                            Win_data[44].Add(new { idx = i, val = win_count_sum.ToString("0") });//개수합계
                            Win_data[45].Add(new { idx = i, val = "100 %" });//면적율합계
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
                                    Win_data[62+ a].Add(new { idx = i, val = win_ueff_old[a].ToString("0.00") });//기존열관류율
                                    data.Add(new { cname = "win_ueff_old" + a, data = Win_data[62 + a] });

                                }
                            }
                            double win_ueff_avg = 0;
                            double win_ueff_old_avg = 0;
                            double win_shgc_avg = 0;
                            for (int a = 0; a < 8; a++)
                            {
                                win_ueff_avg += win_ueff[a] * win_area[a] / win_area_sum;
                                win_ueff_old_avg += win_ueff_old[a] * win_area[a] / win_area_sum;
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
                                        Win_data[89+ a].Add(new { idx = i, val = (Convert.ToDouble(value2[0][0]) / win_ueff[a] * 100).ToString("0") + " 점" });//법규대비 성능점수
                                        data.Add(new { cname = "win_law_point" + a, data = Win_data[89 + a] });
                                        win_law_avg += Convert.ToDouble(value2[0][0]) * win_area[a] / win_area_sum;
                                    }
                                }
                            }
                            Win_data[97].Add(new { idx = i, val = (win_law_avg / win_ueff_avg * 100).ToString("0") + " 점" });//법규대비 성능점수 평균
                            data.Add(new { cname = "win_law_point_avg", data = Win_data[97] });

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
                    double cw_saving = Element_EnergySaving[j_커튼월창];
                    CW_data[0].Add(new { idx = i, val = cw_saving.ToString("0.0") }); ; //절감량 
                    CW_data[1].Add(new { idx = i, val = (cw_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "cw_saving", data = CW_data[0] });
                    data.Add(new { cname = "cw_savingpercent", data = CW_data[1] });

                    double cw_saving_elec = Element_ElecSaving[j_커튼월창];
                    double cw_saving_noelec = Element_GasSaving[j_커튼월창];

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

                    string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "SELECT DISTINCT a.명칭,a.유리부분유효열관류율,a.기존커튼월,a.번호,b.커튼월부위 From ConstructionCW as a  Inner Join ZoneEnvelope_3D as b  on a.번호 = b.구조체번호  where b.외피유형 ='커튼월창' Order by a.커튼월창유효열관류율 DESC");
                    string[] cw_num = new string[8]; string[] cw_name = new string[8]; double[] cw_ueff = new double[8]; double[] cw_ueff_old = new double[8]; double[] cw_area = new double[8]; double[] cw_saving_element = new double[8]; double[] cw_shgc = new double[8]; string[] cw_frame = new string[8]; string[] cw_glass = new string[8]; string[] cw_part = new string[8];
                    double cw_area_sum = 0;
                    if (Value.Length > 0)
                    {
                        for (int k = 0; k < Value.Length; k++)
                        {
                            cw_name[k] = Value[k][0]+"_"+Value[k][4];
                            if (Value[k][4] == "유리부분")
                            {
                                string[][] value3 = Program.DB.querySQL(DB.type.ProjDB, "SELECT 유리부분유효열관류율,프레임유형,태양열취득률,고정유리종류 From ConstructionCW  where 번호='" + Value[k][3] +"'");
                                cw_ueff[k] = Convert.ToDouble(value3[0][0]);
                                cw_frame[k] = value3[0][1];
                                cw_shgc[k] = Convert.ToDouble(value3[0][2]);
                                cw_glass[k] = value3[0][3];
                            }
                            else if (Value[k][1] == "패널부분")
                            {
                                string[][] value3 = Program.DB.querySQL(DB.type.ProjDB, "SELECT 패널부분유효열관류율,프레임유형,패널유리종류 From ConstructionCW  where 번호='" + Value[k][3] + "'");
                                cw_ueff[k] = Convert.ToDouble(value3[0][0]);
                                cw_frame[k] = value3[0][1];
                                cw_glass[k] = value3[0][2];

                            }
                            else
                            {
                                string[][] value3 = Program.DB.querySQL(DB.type.ProjDB, "SELECT 출입문부분유효열관류율,프레임유형,출입문태양열취득률,패널유리종류 From ConstructionCW  where 번호='" + Value[k][3] + "'");
                                cw_ueff[k] = Convert.ToDouble(value3[0][0]);
                                cw_frame[k] = value3[0][1];
                                cw_shgc[k] = Convert.ToDouble(value3[0][2]);
                                cw_glass[k] = value3[0][3];
                            }

                            if (Value[k][2] != "")
                            {
                                string[][] value2;
                                if (Value[k][4] == "유리부분")
                                {
                                    value2 = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "유리부분유효열관류율", "명칭 ='" + Value[k][2] + "'");
                                }
                                else if (Value[k][4] == "유리부분")
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

                            cw_num[k] = Value[k][3];
                            cw_part[k] = Value[k][4];
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
                                CW_data[61 + a].Add(new { idx = i, val = cw_ueff_old[a].ToString("0.00") });//기존열관류율
                                data.Add(new { cname = "cw_ueff_old" + a, data = CW_data[61 + a] });
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
                            cw_ueff_old_avg += cw_ueff_old[a] * cw_area[a] / cw_area_sum;
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
                                string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "법규유리부분열관류율", "번호 ='" + cw_num[a] + "'");
                                if (value2.Length > 0)
                                {
                                    CW_data[89 + a].Add(new { idx = i, val = (Convert.ToDouble(value2[0][0]) / cw_ueff[a] * 100).ToString("0") + " 점" });//법규대비 성능점수
                                    data.Add(new { cname = "cw_law_point" + a, data = CW_data[89 + a] });
                                    cw_law_avg += Convert.ToDouble(value2[0][0]) * cw_area[a] / cw_area_sum;
                                }
                            }
                        }
                        CW_data[97].Add(new { idx = i, val = (cw_law_avg / cw_ueff_avg * 100).ToString("0") + " 점" });//법규대비 성능점수 평균
                        data.Add(new { cname = "cw_law_point_avg", data = CW_data[97] });

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
                    double door_saving = Element_EnergySaving[j_외부출입문];
                    Door_data[0].Add(new { idx = i, val = door_saving.ToString("0.0") }); ; //절감량 
                    Door_data[1].Add(new { idx = i, val = (door_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "door_saving", data = Door_data[0] });
                    data.Add(new { cname = "door_savingpercent", data = Door_data[1] });

                    double door_saving_elec = Element_ElecSaving[j_외부출입문];
                    double door_saving_noelec = Element_GasSaving[j_외부출입문];

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

                    Value = Program.DB.querySQL(DB.type.ProjDB, "SELECT DISTINCT a.명칭,a.문유효열관류율,a.기존출입문,a.번호,a.문면적,a.출입문재질, a.문짝내부유형 From ConstructionDoor as a  Inner Join ZoneEnvelope_3D as b  on a.번호 = b.구조체번호  where b.외피유형 ='외부출입문' Order by a.문유효열관류율 DESC");
                    string[] door_num = new string[8]; string[] door_name = new string[8]; double[] door_ueff = new double[8]; double[] door_ueff_old = new double[8]; double[] door_area = new double[8]; double[] door_saving_element = new double[8]; double[] door_count= new double[8]; string[] door_type = new string[8];
                    double door_area_sum = 0; double door_count_sum = 0;    
                    if (Value.Length > 0)
                    {
                        for (int k = 0; k < Value.Length; k++)
                        {
                            door_name[k] = Value[k][0];
                            door_ueff[k] = Convert.ToDouble(Value[k][1]);
                            door_area[k] = Convert.ToDouble(Value[k][4]);
                            if (Value[k][5] != "")
                            { door_type[k] = Value[k][5] + "_" + Value[k][6]; }
                            else { door_type[k] = ""; }
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
                                door_area_sum += door_area[a]*door_count[a];

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
                                Door_data[38 + a].Add(new { idx = i, val = ((door_area[a] * door_count[a] ) / door_area_sum * 100).ToString("0") + " %" });//면적율
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
                                Door_data[54 + a].Add(new { idx = i, val = door_ueff_old[a].ToString("0.00") });//기존열관류율
                                data.Add(new { cname = "door_ueff_old" + a, data = Door_data[54 + a] });

                            }
                        }
                        double door_ueff_avg = 0;
                        double door_ueff_old_avg = 0;
                        for (int a = 0; a < 8; a++)
                        {
                            door_ueff_avg += door_ueff[a] * (door_area[a] * door_count[a]) / door_area_sum;
                            door_ueff_old_avg += door_ueff_old[a] *( door_area[a] * door_count[a] ) / door_area_sum;
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
                                    Door_data[73 + a].Add(new { idx = i, val = (Convert.ToDouble(value2[0][0]) / door_ueff[a] * 100).ToString("0") + " 점" });//법규대비 성능점수
                                    data.Add(new { cname = "door_law_point" + a, data = Door_data[73 + a] });
                                    door_law_avg += Convert.ToDouble(value2[0][0]) * door_area[a] / door_area_sum;
                                }
                            }
                        }
                        Door_data[81].Add(new { idx = i, val = (door_law_avg / door_ueff_avg * 100).ToString("0") + " 점" });//법규대비 성능점수 평균
                        data.Add(new { cname = "door_law_point_avg", data = Door_data[81] });

                    }

                    #endregion
                    

                    items.Add("Element_Win.htm");
                    s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                    s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
                    System.Text.Json.JsonSerializer.Serialize(Win_data[10].ToArray());
                   
                    Debug.Print("start");
                    if (charts != "") charts += ",";                  
                    runScript("init(" + s + "," + s2 + "," + "[" + charts + "])");
                }
            }
        }
    }
}
