using Eagle._Components.Public;
using Eagle._Interfaces.Public;
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
    public partial class Element_Structure : Form
    {
        bool scriptable = false;
        public Element_Structure()
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
            List<object>[] Wall_data = new List<object>[700];
            List<object>[] Roof_data = new List<object>[700];
            List<object>[] Floor_data = new List<object>[700];
            string[] ElementAlt = CALC.ElementAlt;
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
                    #region 요소기술별 절감량 비율 계산 
                    double[] Element_ElecSum = new double[ElementAlt.Length];
                    double[] Element_GasSum = new double[ElementAlt.Length];
                    double[] Element_EnergySum = new double[ElementAlt.Length];
                    for (int a =0; a< ElementAlt.Length; a++)
                    {
                        string[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result_Element", "총에너지소요량", "검토유형='" + ElementAlt[a] + "' And 연료='전기'");
                        if (Value2.Length > 0)
                        {
                            Element_ElecSum[a] += Convert.ToDouble(Value2[0][0]);
                        }
                        Value2 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result_Element", "총에너지소요량", "검토유형='" + ElementAlt[a] + "' And Not 연료='전기' and Not 연료='전체'");
                        if (Value2.Length > 0)
                        {
                            Element_GasSum[a] += Convert.ToDouble(Value2[0][0]);
                        }

                        Element_EnergySum[a] = Element_ElecSum[a] + Element_GasSum[a];
                    }

                    double Total_Energy_pre = 0; 
                    double Total_EnergySaving = 0;
                    double Total_ElecSaving = 0;
                    double Total_GasSaving = 0;
                   
                        string[][] Final1 = Program.DB.querySQL(res[0][0], "Select 총에너지소요량 from FinalEnergy_Result Where 연료='전기' and 월 ='연간'");
                        string[][] Final2 = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 from FinalEnergy_Result Where 연료='전기' and 월 ='연간'");
                        if(Final1.Length > 0 &&Final2.Length >0)
                        {
                            Total_Energy_pre += Convert.ToDouble(Final1[0][0]);
                            Total_ElecSaving += (Convert.ToDouble(Final1[0][0]) - Convert.ToDouble(Final2[0][0]));
                        }

                         Final1 = Program.DB.querySQL(res[0][0], "Select 총에너지소요량 from FinalEnergy_Result Where Not 연료='전기' and Not 연료='전체' and 월 ='연간'");
                         Final2 = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 from FinalEnergy_Result Where Not 연료='전기' and Not 연료='전체' and 월 ='연간'");
                        if (Final1.Length > 0 && Final2.Length > 0)
                        {
                            Total_Energy_pre += Convert.ToDouble(Final1[0][0]);
                            Total_GasSaving += (Convert.ToDouble(Final1[0][0]) - Convert.ToDouble(Final2[0][0]));
                        }

                        Total_EnergySaving = Total_ElecSaving + Total_GasSaving;
                  
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
                        { Element_GasSaving[a] =0; }
                        else { Element_GasSaving[a] = Total_GasSaving * (Element_GasSum[0] - Element_GasSum[a]) / sum_gas; }
                        Element_EnergySaving[a] = Element_ElecSaving[a] + Element_GasSaving[a];
                    }

                    #endregion

                   

                    #region 외벽                                
                    int j_외벽 = 0;
                    for(int a =0; a< ElementAlt.Length; a++)
                    {
                        if (ElementAlt[a] =="외벽")
                        {
                            j_외벽 = a; break;
                        }
                    }

                    double wall_saving = Element_EnergySaving[j_외벽];
                    Wall_data[0].Add(new { idx = i, val = wall_saving.ToString("0.0") }); ; //절감량 
                    Wall_data[1].Add(new { idx = i, val = (wall_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "wall_saving", data = Wall_data[0] });
                    data.Add(new { cname = "wall_savingpercent", data = Wall_data[1] });

                    double wall_saving_elec = Element_ElecSaving[j_외벽];
                    double wall_saving_noelec = Element_GasSaving[j_외벽];

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

                    string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "SELECT DISTINCT a.명칭,a.유효열관류율,a.기존외벽,a.번호,a.단열재두께,a.U적용방법 From ConstructionWall as a  Inner Join ZoneEnvelope_3D as b  on a.번호 = b.구조체번호  where b.외피유형 ='외벽' Order by a.유효열관류율 DESC");
                    string[] wall_num = new string[8]; string[] wall_name = new string[8]; double[] wall_ueff = new double[8]; double[] wall_ueff_old = new double[8]; double[] wall_area = new double[8]; double[] wall_saving_element = new double[8]; string[] wall_feature = new string[8];
                    double wall_area_sum = 0;
                    if (Value.Length > 0)
                    {
                       for(int k =0; k < Value.Length; k++)
                        {
                            wall_name[k] = Value[k][0];
                            wall_ueff[k] = Convert.ToDouble(Value[k][1]);
                            if (Value[k][2] != "")
                            { string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "유효열관류율", "명칭 ='" + Value[k][2] + "'"); 
                                if(value2.Length > 0)
                                {
                                    wall_ueff_old[k] = Convert.ToDouble(value2[0][0]);
                                }
                            }
                            else
                            {
                                wall_ueff_old[k] = wall_ueff[k];
                            }

                            wall_num[k] = Value[k][3];
                            string[][] valuek = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "외피유형='외벽' And 구조체번호='" + Value[k][3] + "'");
                            if(valuek.Length > 0)
                            {
                                for (int a = 0; a < valuek.Length; a++)
                                { wall_area[k] += Convert.ToDouble(valuek[a][0]); }
                            }

                            if (Value[k][5] == "법규") { wall_feature[k] = "-"; }
                            else
                            {
                                if (Convert.ToDouble(Value[k][4]) >0)
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
                            if (wall_name[a] != null && wall_name[a]!="")
                            {
                                Wall_data[38 + a].Add(new { idx = i, val = wall_ueff[a].ToString("0.00") });//계획열관류율
                                data.Add(new { cname = "wall_ueff" + a, data = Wall_data[38 + a] });
                                Wall_data[46 + a].Add(new { idx = i, val = wall_ueff_old[a].ToString("0.00") });//기존열관류율
                                data.Add(new { cname = "wall_ueff_old" + a, data = Wall_data[46 + a] });

                            }
                        }
                        double wall_ueff_avg = 0;
                        double wall_ueff_old_avg = 0;
                        for (int a=0; a < 8; a++)
                        {
                            wall_ueff_avg += wall_ueff[a] * wall_area[a] / wall_area_sum;
                            wall_ueff_old_avg += wall_ueff_old[a] * wall_area[a] / wall_area_sum;
                        }
                        Wall_data[54].Add(new { idx = i, val = wall_ueff_avg.ToString("0.00") });//계획열관류율 평균
                        Wall_data[55].Add(new { idx = i, val = wall_ueff_old_avg.ToString("0.00") });//기존열관류율 평균
                        data.Add(new { cname = "wall_ueff_avg", data = Wall_data[54] });
                        data.Add(new { cname = "wall_ueff_old_avg", data = Wall_data[55] });

                        double sum = 0;
                        for(int a = 0;a < 8; a++)
                        {
                            sum += wall_saving_element[a];
                        }
                        for (int a = 0; a < 8; a++)
                        {
                            if (wall_name[a]!=null && wall_name[a]!="")
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
                                if(value2.Length > 0)
                                {
                                    Wall_data[65 + a].Add(new { idx = i, val = (Convert.ToDouble(value2[0][0])/wall_ueff[a] * 100).ToString("0") + " 점" });//법규대비 성능점수
                                    data.Add(new { cname = "wall_law_point" + a, data = Wall_data[65 + a] });
                                    wall_law_avg += Convert.ToDouble(value2[0][0]) * wall_area[a] / wall_area_sum;
                                }
                            }
                        }
                        Wall_data[73].Add(new { idx = i, val = (wall_law_avg / wall_ueff_avg * 100).ToString("0") + " 점" });//법규대비 성능점수 평균
                        data.Add(new { cname = "wall_law_point_avg", data = Wall_data[73] });                       

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
                    double roof_saving = Element_EnergySaving[j_지붕];
                    Roof_data[0].Add(new { idx = i, val = roof_saving.ToString("0.0") }); ; //절감량 
                    Roof_data[1].Add(new { idx = i, val = (roof_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "roof_saving", data = Roof_data[0] });
                    data.Add(new { cname = "roof_savingpercent", data = Roof_data[1] });

                    double roof_saving_elec = Element_ElecSaving[j_지붕];
                    double roof_saving_noelec = Element_GasSaving[j_지붕];

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

                    Value = Program.DB.querySQL(DB.type.ProjDB, "SELECT DISTINCT a.명칭,a.유효열관류율,a.기존지붕,a.번호,a.단열재두께,a.U적용방법 From ConstructionRoof as a  Inner Join ZoneEnvelope_3D as b  on a.번호 = b.구조체번호  where b.외피유형 ='지붕' Order by a.유효열관류율 DESC");
                    string[] roof_num = new string[8]; string[] roof_name = new string[8]; double[] roof_ueff = new double[8]; double[] roof_ueff_old = new double[8]; double[] roof_area = new double[8]; double[] roof_saving_element = new double[8]; string[] roof_feature = new string[8];
                    double roof_area_sum = 0;
                    if (Value.Length > 0)
                    {
                        for (int k = 0; k < Value.Length; k++)
                        {
                            roof_name[k] = Value[k][0];
                            roof_ueff[k] = Convert.ToDouble(Value[k][1]);
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

                            roof_num[k] = Value[k][3];
                            string[][] valuek = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "외피유형='지붕' And 구조체번호='" + Value[k][3] + "'");
                            if (valuek.Length > 0)
                            {
                                for (int a = 0; a < valuek.Length; a++)
                                { roof_area[k] += Convert.ToDouble(valuek[a][0]); }
                            }

                            if (Value[k][5] == "법규") {roof_feature[k] = "-"; }
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
                                Roof_data[46 + a].Add(new { idx = i, val = roof_ueff_old[a].ToString("0.00") });//기존열관류율
                                data.Add(new { cname = "roof_ueff_old" + a, data = Roof_data[46 + a] });

                            }
                        }
                        double roof_ueff_avg = 0;
                        double roof_ueff_old_avg = 0;
                        for (int a = 0; a < 8; a++)
                        {
                            roof_ueff_avg += roof_ueff[a] * roof_area[a] / roof_area_sum;
                            roof_ueff_old_avg += roof_ueff_old[a] * roof_area[a] / roof_area_sum;
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
                                    Roof_data[65 + a].Add(new { idx = i, val = (Convert.ToDouble(value2[0][0]) / roof_ueff[a] * 100).ToString("0") + " 점" });//법규대비 성능점수
                                    data.Add(new { cname = "roof_law_point" + a, data = Roof_data[65 + a] });
                                    roof_law_avg += Convert.ToDouble(value2[0][0]) * roof_area[a] / roof_area_sum;
                                }
                            }
                        }
                        Roof_data[73].Add(new { idx = i, val = (roof_law_avg / roof_ueff_avg * 100).ToString("0") + " 점" });//법규대비 성능점수 평균
                        data.Add(new { cname = "roof_law_point_avg", data = Roof_data[73] });

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
                    double floor_saving = Element_EnergySaving[j_최하층바닥];
                    Floor_data[0].Add(new { idx = i, val = floor_saving.ToString("0.0") }); ; //절감량 
                    Floor_data[1].Add(new { idx = i, val = (floor_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "floor_saving", data = Floor_data[0] });
                    data.Add(new { cname = "floor_savingpercent", data = Floor_data[1] });

                    double floor_saving_elec = Element_ElecSaving[j_최하층바닥];
                    double floor_saving_noelec = Element_GasSaving[j_최하층바닥];

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

                    Value = Program.DB.querySQL(DB.type.ProjDB, "SELECT DISTINCT a.명칭,a.유효열관류율,a.기존바닥,a.번호,a.단열재두께,a.U적용방법 From ConstructionFloor as a  Inner Join ZoneEnvelope_3D as b  on a.번호 = b.구조체번호  where b.외피유형 ='최하층바닥' Order by a.유효열관류율 DESC");
                    string[] floor_num = new string[8]; string[] floor_name = new string[8]; double[] floor_ueff = new double[8]; double[] floor_ueff_old = new double[8]; double[] floor_area = new double[8]; double[] floor_saving_element = new double[8]; string[] floor_feature = new string[8];
                    double floor_area_sum = 0;
                    if (Value.Length > 0)
                    {
                        for (int k = 0; k < Value.Length; k++)
                        {
                            floor_name[k] = Value[k][0];
                            floor_ueff[k] = Convert.ToDouble(Value[k][1]);
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
                                Floor_data[46 + a].Add(new { idx = i, val = floor_ueff_old[a].ToString("0.00") });//기존열관류율
                                data.Add(new { cname = "floor_ueff_old" + a, data = Floor_data[46 + a] });

                            }
                        }
                        double floor_ueff_avg = 0;
                        double floor_ueff_old_avg = 0;
                        for (int a = 0; a < 8; a++)
                        {
                            floor_ueff_avg += floor_ueff[a] * floor_area[a] / floor_area_sum;
                            floor_ueff_old_avg += floor_ueff_old[a] * floor_area[a] / floor_area_sum;
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
                                    Floor_data[65 + a].Add(new { idx = i, val = (Convert.ToDouble(value2[0][0]) / floor_ueff[a] * 100).ToString("0") + " 점" });//법규대비 성능점수
                                    data.Add(new { cname = "floor_law_point" + a, data = Floor_data[65 + a] });
                                    floor_law_avg += Convert.ToDouble(value2[0][0]) * floor_area[a] / floor_area_sum;
                                }
                            }
                        }
                        Floor_data[73].Add(new { idx = i, val = (floor_law_avg / floor_ueff_avg * 100).ToString("0") + " 점" });//법규대비 성능점수 평균
                        data.Add(new { cname = "floor_law_point_avg", data = Floor_data[73] });

                    }

                    #endregion


                    items.Add("Element_structure.htm");
                    s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                    s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
                    System.Text.Json.JsonSerializer.Serialize(Wall_data[10].ToArray());
                   
                    Debug.Print("start");
                    if (charts != "") charts += ",";                  
                    runScript("init(" + s + "," + s2 + "," + "[" + charts + "])");
                }
            }
        }
    }
}
