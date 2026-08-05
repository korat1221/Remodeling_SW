using main.contents.Result.Element_Report;
using Microsoft.Web.WebView2.Core;
using System;
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
            element_saving.Calc_DHW_Saving();
            ArrayList HeatingGroup = element_saving.HeatingGroup;
            ArrayList DHWGroup = element_saving.DHWGroup;

            string script = null;
            string s, s2;
            string[][] 번호 = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
            string[][] res = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기존프로젝트");
            List<object> items = new List<object>();
            List<object> items2 = new List<object>();
            List<object> data = new List<object>();
            List<object>[] PV_data = new List<object>[700];
            List<object>[] fc_data = new List<object>[700];
            List<object>[] WP_data = new List<object>[700];
            double d;
            string sp;
            int i = -1, n;
            while (++i < 700)
            {
                PV_data[i] = new List<object>();
                WP_data[i] = new List<object>();
                fc_data[i] = new List<object>();
            }
            string charts = "";
            i = -1;
            while (++i < 번호.Length)
            {
                if (res.Length > 0)
                {
                    #region 난방+급탕 절약 : 모든 요소기술 적용 절감량 중                                
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

                    int j_dhw = 0;
                    for (int a = 0; a < ElementAlt.Length; a++)
                    {
                        if (ElementAlt[a] == "급탕")
                        {
                            j_dhw = a; break;
                        }
                    }
                    double dhw_saving = Element_EnergySaving[j_dhw];
                    double dhw_saving_elec = Element_ElecSaving[j_dhw];
                    double dhw_saving_noelec = Element_GasSaving[j_dhw];
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
                        if (heating_saving_total > 0)
                        {
                            heating_element_saving[aa] = heating_saving * (hh.Before_Energy() - hh.After_Energy()) / heating_saving_total;
                            heating_element_saving_elec[aa] = heating_saving_elec * (hh.Before_Energy() - hh.After_Energy()) / heating_saving_total;
                            heating_element_saving_gas[aa] = heating_saving_noelec * (hh.Before_Energy() - hh.After_Energy()) / heating_saving_total;
                        }

                    }
                    #endregion

                    #region 급탕 절약 : 각 급탕설비별
                    double dhw_saving_total = 0;
                    for (int aa = 0; aa < DHWGroup.Count; aa++)
                    {
                        DHW_New_Old ww = (DHW_New_Old)DHWGroup[aa];
                        dhw_saving_total += ww.Before_Energy() - ww.After_Energy();
                    }

                    double[] dhw_element_saving = new double[DHWGroup.Count];
                    double[] dhw_element_saving_elec = new double[DHWGroup.Count];
                    double[] dhw_element_saving_gas = new double[DHWGroup.Count];
                    for (int aa = 0; aa < DHWGroup.Count; aa++)
                    {
                        DHW_New_Old ww = (DHW_New_Old)DHWGroup[aa];
                        if (dhw_saving_total > 0)
                        {
                            dhw_element_saving[aa] = dhw_saving * (ww.Before_Energy() - ww.After_Energy()) / dhw_saving_total;
                            dhw_element_saving_elec[aa] = dhw_saving_elec * (ww.Before_Energy() - ww.After_Energy()) / dhw_saving_total;
                            dhw_element_saving_gas[aa] = dhw_saving_noelec * (ww.Before_Energy() - ww.After_Energy()) / dhw_saving_total;
                        }

                    }
                    #endregion


                    #region 태양광
                    int j_태양광 = 0;
                    for (int a = 0; a < ElementAlt.Length; a++)
                    {
                        if (ElementAlt[a] == "태양광")
                        {
                            j_태양광 = a; break;
                        }
                    }
                    double pv_saving = Element_EnergySaving[j_태양광];
                    double pv_saving_elec = Element_ElecSaving[j_태양광];
                    double pv_saving_noelec = Element_GasSaving[j_태양광];

                    double v = double.IsNaN(pv_saving) ? 0 : pv_saving;
                    PV_data[0].Add(new { idx = i, val = v.ToString("#,##0") }); ; //절감량 

                    v = double.IsNaN(pv_saving / Total_Energy_pre * 100) ? 0 : pv_saving / Total_Energy_pre * 100;
                    PV_data[1].Add(new { idx = i, val = (v).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "pv_saving", data = PV_data[0] });
                    data.Add(new { cname = "pv_savingpercent", data = PV_data[1] });

                    d = (pv_saving / Total_Energy_pre * 100);
                    charts += "{donut:" + d + "},";


                    double pv_tCO2 = double.IsNaN(pv_saving_elec * 0.4747 / 1000000 * 1000 + pv_saving_noelec / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000) ? 0 : pv_saving_elec * 0.4747 / 1000000 * 1000 + pv_saving_noelec / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000; 
                    double pv_TOE = double.IsNaN(pv_saving_elec * 0.00023 + pv_saving_noelec / 38.9 / 0.277778 * 0.00103) ? 0 : pv_saving_elec * 0.00023 + pv_saving_noelec / 38.9 / 0.277778 * 0.00103;

                    PV_data[2].Add(new { idx = i, val = pv_tCO2.ToString("0.0") });  //tco2
                    PV_data[3].Add(new { idx = i, val = pv_TOE.ToString("0.0") });  //TOE 
                    data.Add(new { cname = "pv_tco2", data = PV_data[2] });
                    data.Add(new { cname = "pv_toe", data = PV_data[3] });

                    string[] pv_name = new string[8]; string[] pv_cell = new string[8]; double[] pv_eta = new double[8]; double[] pv_count = new double[8];
                    double[] pv_power = new double[8]; double[] pv_area_old = new double[8]; double[] pv_area_new = new double[8];
                    double[] pv_saving_element = new double[8]; double[] pv_point = new double[8];
                    double sum_old = 0; double sum_new = 0;
                    string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select a.번호,a.명칭,a.모듈번호,a.개수,a.개수,a.용량,a.면적,b.CELLTYPE,b.Kpk From PV_Form as a inner Join User_PV as b on a.모듈번호=b.번호");
                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            pv_name[a] = Value[a][1];
                            pv_cell[a] = Value[a][7];
                            pv_eta[a] = Program.UTIL.ToDoubleOrZero(Value[a][8]);
                            pv_count[a] = Program.UTIL.ToDoubleOrZero(Value[a][3]);
                            pv_power[a] = Program.UTIL.ToDoubleOrZero(Value[a][5]);
                            pv_area_new[a] = Program.UTIL.ToDoubleOrZero(Value[a][6]);
                            sum_new += Program.UTIL.ToDoubleOrZero(Value[a][6]);
                            pv_point[a] = 100;
                        }
                    }
                    Value = Program.DB.getValue(res[0][0], "PV_Form", "면적");
                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            sum_old += Program.UTIL.ToDoubleOrZero(Value[a][0]);
                        }
                        for (int a = 0; a < 8; a++)
                        {
                            if (sum_new > 0)
                            { pv_area_old[a] = sum_old * pv_area_new[a] / sum_new; }
                        }
                    }
                    double sum = 0;
                    for (int a = 0; a < 8; a++)
                    {
                        sum += (pv_area_new[a] - pv_area_old[a]);
                    }
                    for (int a = 0; a < 8; a++)
                    {
                        if (sum > 0)
                        {
                            pv_saving_element[a] = pv_saving * (pv_area_new[a] - pv_area_old[a]) / sum;
                        }
                    }
                    for (int a = 0; a < 8; a++)
                    {
                        PV_data[4 + a].Add(new { idx = i, val = pv_name[a] });//명칭
                        data.Add(new { cname = "pv_name" + a, data = PV_data[4 + a] });
                        if (pv_name[a] != null & pv_name[a] != "")
                        {
                            PV_data[12 + a].Add(new { idx = i, val = pv_cell[a] });//셀타입
                            data.Add(new { cname = "pv_cell" + a, data = PV_data[12 + a] });

                            PV_data[20 + a].Add(new { idx = i, val = pv_eta[a].ToString("0.00") });//효율
                            data.Add(new { cname = "pv_eta" + a, data = PV_data[20 + a] });

                            PV_data[28 + a].Add(new { idx = i, val = pv_count[a].ToString("0") });//개수
                            data.Add(new { cname = "pv_count" + a, data = PV_data[28 + a] });

                            PV_data[36 + a].Add(new { idx = i, val = pv_power[a].ToString("0.0") });//출력
                            data.Add(new { cname = "pv_power" + a, data = PV_data[36 + a] });

                            PV_data[44 + a].Add(new { idx = i, val = pv_area_old[a].ToString("0.0") });//기존면적
                            data.Add(new { cname = "pv_area_old" + a, data = PV_data[44 + a] });

                            PV_data[52 + a].Add(new { idx = i, val = pv_area_new[a].ToString("0.0") });//기존면적
                            data.Add(new { cname = "pv_area_new" + a, data = PV_data[52 + a] });

                            PV_data[60 + a].Add(new { idx = i, val = (pv_saving_element[a] / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                            data.Add(new { cname = "pv_saving_element" + a, data = PV_data[60 + a] });

                            d = pv_point[a];
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            PV_data[68 + a].Add(new { idx = i, val = sp });//성능점수
                            data.Add(new { cname = "pv_point" + a, data = PV_data[68 + a] });
                        }
                    }
                    double pv_eta_avg = 0, pv_count_sum = 0, pv_power_sum = 0, pv_area_old_sum = 0, pv_area_new_sum = 0, pv_point_avg = 0;
                    for (int a = 0; a < 8; a++)
                    {
                        pv_eta_avg += pv_eta[a] * pv_power[a];
                        pv_count_sum += pv_count[a];
                        pv_power_sum += pv_power[a];
                        pv_area_old_sum += pv_area_old[a];
                        pv_area_new_sum += pv_area_new[a];
                        pv_point_avg += pv_point[a] * pv_power[a];
                    }
                    pv_eta_avg = pv_eta_avg / pv_power_sum;
                    pv_point_avg = pv_point_avg / pv_power_sum;
                    PV_data[76].Add(new { idx = i, val = pv_eta_avg.ToString("0.00") });
                    PV_data[77].Add(new { idx = i, val = pv_count_sum.ToString("0") });
                    PV_data[78].Add(new { idx = i, val = pv_power_sum.ToString("0.0") });
                    PV_data[79].Add(new { idx = i, val = pv_area_old_sum.ToString("0.0") });
                    PV_data[80].Add(new { idx = i, val = pv_area_new_sum.ToString("0.0") });
                    PV_data[81].Add(new { idx = i, val = (pv_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    d = pv_point_avg;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    PV_data[82].Add(new { idx = i, val = sp });//성능수준 평균  
                    data.Add(new { cname = "pv_eta_avg", data = PV_data[76] });
                    data.Add(new { cname = "pv_count_sum", data = PV_data[77] });
                    data.Add(new { cname = "pv_power_sum", data = PV_data[78] });
                    data.Add(new { cname = "pv_area_old_sum", data = PV_data[79] });
                    data.Add(new { cname = "pv_area_new_sum", data = PV_data[80] });
                    data.Add(new { cname = "pv_saving_sum", data = PV_data[81] });
                    data.Add(new { cname = "pv_point_avg", data = PV_data[82] });

                    #endregion
                    #region 풍력
                    int j_풍력 = 0;
                    for (int a = 0; a < ElementAlt.Length; a++)
                    {
                        if (ElementAlt[a] == "풍력")
                        {
                            j_풍력 = a; break;
                        }
                    }
                    double wp_saving = Element_EnergySaving[j_풍력];
                    double wp_saving_elec = Element_ElecSaving[j_풍력];
                    double wp_saving_noelec = Element_GasSaving[j_풍력];

                    v = double.IsNaN(wp_saving) ? 0 : wp_saving;
                    WP_data[0].Add(new { idx = i, val = v.ToString("#,##0") }); ; //절감량 

                    v = double.IsNaN(wp_saving / Total_Energy_pre * 100) ? 0 : wp_saving / Total_Energy_pre * 100;
                    WP_data[1].Add(new { idx = i, val = (v).ToString("0.0") + " %" }); ; //절감률
                    data.Add(new { cname = "wp_saving", data = WP_data[0] });
                    data.Add(new { cname = "wp_savingpercent", data = WP_data[1] });

                    d = (wp_saving / Total_Energy_pre * 100);
                    charts += "{donut:" + d + "},";


                    double wp_tCO2 = double.IsNaN(wp_saving_elec * 0.4747 / 1000000 * 1000 + wp_saving_noelec / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000) ? 0 : wp_saving_elec * 0.4747 / 1000000 * 1000 + wp_saving_noelec / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    double wp_TOE = double.IsNaN(wp_saving_elec * 0.00023 + wp_saving_noelec / 38.9 / 0.277778 * 0.00103) ? 0 : wp_saving_elec * 0.00023 + wp_saving_noelec / 38.9 / 0.277778 * 0.00103;

                    WP_data[2].Add(new { idx = i, val = wp_tCO2.ToString("0.0") });  //tco2
                    WP_data[3].Add(new { idx = i, val = wp_TOE.ToString("0.0") });  //TOE 
                    data.Add(new { cname = "wp_tco2", data = WP_data[2] });
                    data.Add(new { cname = "wp_toe", data = WP_data[3] });

                    string[] wp_name = new string[8]; string[] wp_type = new string[8]; double[] wp_area = new double[8]; double[] wp_height = new double[8];
                    double[] wp_power = new double[8]; double[] wp_count_old = new double[8]; double[] wp_count_new = new double[8];
                    double[] wp_saving_element = new double[8]; double[] wp_point = new double[8];
                    sum_old = 0; sum_new = 0;
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select a.번호,a.명칭,a.풍력,a.설치대수,a.설치대수,b.정격출력,b.회전면적,b.허브높이,b.세부타입 From WindPower_Form as a inner Join User_WP as b on a.풍력=b.번호");
                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            wp_name[a] = Value[a][1];
                            wp_type[a] = Value[a][8];
                            wp_area[a] = Program.UTIL.ToDoubleOrZero(Value[a][6]);
                            wp_height[a] = Program.UTIL.ToDoubleOrZero(Value[a][7]);
                            wp_power[a] = Program.UTIL.ToDoubleOrZero(Value[a][5]);
                            wp_count_new[a] = Program.UTIL.ToDoubleOrZero(Value[a][4]);
                            sum_new += Program.UTIL.ToDoubleOrZero(Value[a][4]);
                            wp_point[a] = 100;
                        }
                    }
                    Value = Program.DB.getValue(res[0][0], "WindPower_Form", "설치대수");
                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            sum_old += Program.UTIL.ToDoubleOrZero(Value[a][0]);
                        }
                        for (int a = 0; a < 8; a++)
                        {
                            if (sum_new > 0)
                            { wp_count_old[a] = sum_old * wp_count_new[a] / sum_new; }
                        }
                    }
                     sum = 0;
                    for (int a = 0; a < 8; a++)
                    {
                        sum += (wp_count_new[a] - wp_count_old[a]);
                    }
                    for (int a = 0; a < 8; a++)
                    {
                        if (sum > 0)
                        {
                            wp_saving_element[a] = wp_saving * (wp_count_new[a] - wp_count_old[a]) / sum;
                        }
                    }
                    for (int a = 0; a < 8; a++)
                    {
                        WP_data[4 + a].Add(new { idx = i, val = wp_name[a] });//명칭
                        data.Add(new { cname = "wp_name" + a, data = WP_data[4 + a] });
                        if (wp_name[a] != null & wp_name[a] != "")
                        {
                            WP_data[12 + a].Add(new { idx = i, val = wp_type[a] });//세부타입
                            data.Add(new { cname = "wp_type" + a, data = WP_data[12 + a] });

                            WP_data[20 + a].Add(new { idx = i, val = wp_area[a].ToString("0.00") });//허브면적
                            data.Add(new { cname = "wp_area" + a, data = WP_data[20 + a] });

                            WP_data[28 + a].Add(new { idx = i, val = wp_height[a].ToString("0") });//허브높이
                            data.Add(new { cname = "wp_height" + a, data = WP_data[28 + a] });

                            WP_data[36 + a].Add(new { idx = i, val = wp_power[a].ToString("0.0") });//출력
                            data.Add(new { cname = "wp_power" + a, data = WP_data[36 + a] });

                            WP_data[44 + a].Add(new { idx = i, val = wp_count_old[a].ToString("0.0") });//기존개수
                            data.Add(new { cname = "wp_count_old" + a, data = WP_data[44 + a] });

                            WP_data[52 + a].Add(new { idx = i, val = wp_count_new[a].ToString("0.0") });//신규개수
                            data.Add(new { cname = "wp_count_new" + a, data = WP_data[52 + a] });

                            WP_data[60 + a].Add(new { idx = i, val = (wp_saving_element[a] / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                            data.Add(new { cname = "wp_saving_element" + a, data = WP_data[60 + a] });

                            d = wp_point[a];
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            WP_data[68 + a].Add(new { idx = i, val = sp });//성능점수
                            data.Add(new { cname = "wp_point" + a, data = WP_data[68 + a] });
                        }
                    }
                    double wp_eta_avg = 0, wp_count_sum = 0, wp_power_sum = 0, wp_area_old_sum = 0, wp_area_new_sum = 0, wp_point_avg = 0;
                    for (int a = 0; a < 8; a++)
                    {
                        wp_eta_avg += wp_area[a] * wp_power[a];
                        wp_count_sum += wp_height[a];
                        wp_power_sum += wp_power[a];
                        wp_area_old_sum += wp_count_old[a];
                        wp_area_new_sum += wp_count_new[a];
                        wp_point_avg += wp_point[a] * wp_power[a];
                    }
                    wp_eta_avg = wp_eta_avg / wp_power_sum;
                    wp_point_avg = wp_point_avg / wp_power_sum;
                    WP_data[76].Add(new { idx = i, val = wp_eta_avg.ToString("0.00") });
                    WP_data[77].Add(new { idx = i, val = wp_count_sum.ToString("0") });
                    WP_data[78].Add(new { idx = i, val = wp_power_sum.ToString("0.0") });
                    WP_data[79].Add(new { idx = i, val = wp_area_old_sum.ToString("0.0") });
                    WP_data[80].Add(new { idx = i, val = wp_area_new_sum.ToString("0.0") });
                    WP_data[81].Add(new { idx = i, val = (wp_saving / Total_Energy_pre * 100).ToString("0.0") + " %" }); ; //절감률
                    d = wp_point_avg;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    WP_data[82].Add(new { idx = i, val = sp });//성능수준 평균  
                    data.Add(new { cname = "wp_area_avg", data = WP_data[76] });
                    data.Add(new { cname = "wp_height_sum", data = WP_data[77] });
                    data.Add(new { cname = "wp_power_sum", data = WP_data[78] });
                    data.Add(new { cname = "wp_count_old_sum", data = WP_data[79] });
                    data.Add(new { cname = "wp_count_new_sum", data = WP_data[80] });
                    data.Add(new { cname = "wp_saving_sum", data = WP_data[81] });
                    data.Add(new { cname = "wp_point_avg", data = WP_data[82] });

                    #endregion
                    #region 연료전지
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.번호,a.명칭,a.전기출력,a.전기효율,a.열출력,a.열효율,b.적용설비,c.번호,c.연료전지번호,b.연료전지대수 From User_fc as a Inner Join fc_Form as b ON a.번호 = b.연료전지번호 INNER JOIN DHWSystem_Form AS c ON b.연료전지번호 = c.연료전지번호 Where b.적용설비='급탕'");
                    string[] fc_Name = new string[8]; 
                    double[] fc_count_new = new double[8]; double[] fc_count_old = new double[8]; double[] fc_elec_eta = new double[8]; double[] fc_heat_eta = new double[8]; double[] fc_Saving = new double[8]; double[] fc_Point = new double[8]; double[] fc_eta_Rule = new double[8];
                    string[] fc_H_W = new string[8]; double[] fc_elec_power = new double[8]; double[] fc_heat_power = new double[8];
                    double[] fc_elec = new double[8]; double[] fc_gas = new double[8];
                    ArrayList fc_Zones_split_H_W = new ArrayList(); ArrayList fc_Zones_split_W = new ArrayList(); ArrayList fc_Zones_split_H = new ArrayList();
                    int fc_HW_count = 0; int fc_H_count = 0; int fc_W_count = 0;
                    if (Value.Length > 0)
                    {
                        fc_HW_count = Value.Length;
                        for (int a = 0; a < Value.Length; a++)
                        {
                            for (int aa = 0; aa < DHWGroup.Count; aa++)
                            {
                                DHW_New_Old cc = (DHW_New_Old)DHWGroup[aa];

                                if (Value[a][7] == cc.Num_New())
                                {
                                    fc_Saving[a] = dhw_element_saving[aa];
                                    fc_elec[a] = dhw_element_saving_elec[aa];
                                    fc_gas[a] = dhw_element_saving_gas[aa];
                                    string[][] fc_생산 = Program.DB.querySQL(DB.type.ProjDB, "Select sum(총에너지) From RESystem_Result  Where 신재생시스템 ='" + Value[a][8] + "' and 생산소비='생산' and 급탕설비='" + Value[a][7] + "'");
                                    string[][] fc_소비 = Program.DB.querySQL(DB.type.ProjDB, "Select sum(총에너지) From RESystem_Result  Where 신재생시스템 ='" + Value[a][8] + "' and 생산소비='소비'and 급탕설비='" + Value[a][7] + "'");
                                    if (fc_생산.Length > 0)
                                    {
                                        fc_Saving[a] = Program.UTIL.ToDoubleOrZero(fc_생산[0][0]) - Program.UTIL.ToDoubleOrZero(fc_소비[0][0]);
                                        fc_생산 = Program.DB.querySQL(DB.type.ProjDB, "Select sum(총에너지) From RESystem_Result  Where 신재생시스템 ='" + Value[a][8] + "' and 생산소비='생산' and 급탕설비='" + Value[a][7] + "' and 생산유형='전기'");
                                        fc_elec[a] = Program.UTIL.ToDoubleOrZero(fc_생산[0][0]);
                                        fc_gas[a] = fc_Saving[a] - fc_elec[a];
                                    }
                                    for (int aaa = 0; aaa < cc.Num_Old().Count; aaa++)
                                    {
                                        string[][] OldSystem = Program.DB.querySQL(res[0][0], "Select b.연료전지대수 From fc_Form INNER JOIN DHWSystem_Form AS c ON b.연료전지번호 = c.연료전지번호 Where c.번호 ='" + cc.Num_Old()[aaa] + "'");
                                        if (OldSystem.Length > 0)
                                        {
                                            fc_count_old[a] = Program.UTIL.ToDoubleOrZero(OldSystem[0][0]);
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
                            fc_Name[a] = Value[a][1];
                            fc_H_W[a] = Value[a][6];
                            fc_count_new[a] = Program.UTIL.ToDoubleOrZero(Value[a][9]);
                            fc_elec_power[a] = Program.UTIL.ToDoubleOrZero(Value[a][2]);
                            fc_elec_eta[a] = Program.UTIL.ToDoubleOrZero(Value[a][3]);
                            fc_heat_eta[a] = Program.UTIL.ToDoubleOrZero(Value[a][4]);
                            fc_heat_power[a] = Program.UTIL.ToDoubleOrZero(Value[a][5]);
                            fc_Point[a] = 100;
                        }

                    }

                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.번호,a.명칭,a.전기출력,a.전기효율,a.열출력,a.열효율,b.적용설비,c.번호,c.연료전지번호,b.연료전지대수 From User_fc as a Inner Join fc_Form as b ON a.번호 = b.연료전지번호 INNER JOIN HeatingSystem_Form AS c ON b.연료전지번호 = c.연료전지번호 Where b.적용설비='난방'");
                  
                    if (Value.Length > 0)
                    {
                        fc_HW_count = Value.Length;
                        for (int a = 0; a < Value.Length; a++)
                        {
                            for (int aa = 0; aa < DHWGroup.Count; aa++)
                            {
                                DHW_New_Old cc = (DHW_New_Old)DHWGroup[aa];

                                if (Value[a][7] == cc.Num_New())
                                {
                                    fc_Saving[a] = dhw_element_saving[aa];
                                    fc_elec[a] = dhw_element_saving_elec[aa];
                                    fc_gas[a] = dhw_element_saving_gas[aa];
                                    string[][] fc_생산 = Program.DB.querySQL(DB.type.ProjDB, "Select sum(총에너지) From RESystem_Result  Where 신재생시스템 ='" + Value[a][8] + "' and 생산소비='생산' and 난방설비='" + Value[a][7] + "'");
                                    string[][] fc_소비 = Program.DB.querySQL(DB.type.ProjDB, "Select sum(총에너지) From RESystem_Result  Where 신재생시스템 ='" + Value[a][8] + "' and 생산소비='소비'and 난방설비='" + Value[a][7] + "'");
                                    if (fc_생산.Length > 0)
                                    {
                                        fc_Saving[a] = Program.UTIL.ToDoubleOrZero(fc_생산[0][0]) - Program.UTIL.ToDoubleOrZero(fc_소비[0][0]);
                                        fc_생산 = Program.DB.querySQL(DB.type.ProjDB, "Select sum(총에너지) From RESystem_Result  Where 신재생시스템 ='" + Value[a][8] + "' and 생산소비='생산' and 난방설비='" + Value[a][7] + "' and 생산유형='전기'");
                                        fc_elec[a] = Program.UTIL.ToDoubleOrZero(fc_생산[0][0]);
                                        fc_gas[a] = fc_Saving[a] - fc_elec[a];
                                    }
                                    for (int aaa = 0; aaa < cc.Num_Old().Count; aaa++)
                                    {
                                        string[][] OldSystem = Program.DB.querySQL(res[0][0], "Select b.연료전지대수 From fc_Form INNER JOIN DHWSystem_Form AS c ON b.연료전지번호 = c.연료전지번호 Where c.번호 ='" + cc.Num_Old()[aaa] + "'");
                                        if (OldSystem.Length > 0)
                                        {
                                            fc_count_old[a] = Program.UTIL.ToDoubleOrZero(OldSystem[0][0]);
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
                            fc_Name[a] = Value[a][1];
                            fc_H_W[a] = Value[a][6];
                            fc_count_new[a] = Program.UTIL.ToDoubleOrZero(Value[a][9]);
                            fc_elec_power[a] = Program.UTIL.ToDoubleOrZero(Value[a][2]);
                            fc_elec_eta[a] = Program.UTIL.ToDoubleOrZero(Value[a][3]);
                            fc_heat_eta[a] = Program.UTIL.ToDoubleOrZero(Value[a][4]);
                            fc_heat_power[a] = Program.UTIL.ToDoubleOrZero(Value[a][5]);
                            fc_Point[a] = 100;
                        }

                    }

                    double fc_total_saving = 0; double fc_total_elec = 0; double fc_total_gas = 0;
                    double fc_elec_eta_total = 0; double fc_elec_power_total = 0; double fc_heat_eta_total = 0; double fc_heat_power_total = 0; double fc_Point_total = 0;
                    for (int a = 0; a < 8; a++)
                    {
                        fc_data[a].Add(new { idx = i, val = fc_Name[a] });//명칭
                        data.Add(new { cname = "fc_name" + a, data = fc_data[a] });
                        if (fc_Name[a] != null & fc_Name[a] != "")
                        {
                            fc_data[8 + a].Add(new { idx = i, val = fc_elec_power[a].ToString("0.0") });//전기효율
                            data.Add(new { cname = "fc_elec_power" + a, data = fc_data[8 + a] });

                            fc_data[16 + a].Add(new { idx = i, val = fc_elec_eta[a].ToString("0.0") });//전기효율
                            data.Add(new { cname = "fc_elec_eta" + a, data = fc_data[16 + a] });


                            fc_data[24 + a].Add(new { idx = i, val = fc_elec_power[a].ToString("0.0") });//전기효율
                            data.Add(new { cname = "fc_heat_power" + a, data = fc_data[24 + a] });

                            fc_data[32 + a].Add(new { idx = i, val = fc_elec_eta[a].ToString("0.0") });//전기효율
                            data.Add(new { cname = "fc_heat_eta" + a, data = fc_data[32 + a] });


                            fc_data[40 + a].Add(new { idx = i, val = fc_count_new[a].ToString("0.0") });//대수
                            data.Add(new { cname = "fc_count_new" + a, data = fc_data[40 + a] });

                            fc_data[48 + a].Add(new { idx = i, val = fc_count_old[a].ToString("0.0") }); //기존 대수
                            data.Add(new { cname = "fc_count_old" + a, data = fc_data[48 + a] });

                            fc_data[56 + a].Add(new { idx = i, val = (fc_Saving[a] / Total_Energy_pre * 100).ToString("0.0") + " %" });//절감률
                            data.Add(new { cname = "fc_saving" + a, data = fc_data[56 + a] });


                            d = fc_Point[a];
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            fc_data[64 + a].Add(new { idx = i, val = sp });//성능점수
                            data.Add(new { cname = "fc_point" + a, data = fc_data[64 + a] });

                        }

                        //가중평균 
                        fc_elec_eta_total += fc_elec_eta[a] * fc_count_new[a];
                        fc_elec_power_total += fc_elec_power[a] * fc_count_new[a];
                        fc_heat_eta_total += fc_heat_eta[a] * fc_count_new[a];
                        fc_heat_power_total += fc_heat_power[a] * fc_count_new[a];
                        fc_Point_total += fc_Point[a] * fc_count_new[a];
                    }
                    if (fc_count_new.Sum() > 0)
                    {
                        fc_elec_eta_total = fc_elec_eta_total / fc_count_new.Sum();
                        fc_elec_power_total = fc_elec_power_total / fc_count_new.Sum();
                        fc_heat_eta_total = fc_heat_eta_total / fc_count_new.Sum();
                        fc_heat_power_total = fc_heat_power_total / fc_count_new.Sum();
                        fc_Point_total = Math.Min(100, fc_Point_total / fc_count_new.Sum());
                    }

                    for (int a = 0; a < 8; a++)
                    {
                        fc_total_saving += fc_Saving[a];
                        fc_total_elec += fc_elec[a];
                        fc_total_gas += fc_gas[a];
                    }

                    fc_data[72].Add(new { idx = i, val = fc_total_saving.ToString("#,##0") });//절감량 전체 
                    data.Add(new { cname = "fc_saving_total", data = fc_data[72] });
                    fc_data[73].Add(new { idx = i, val = (fc_total_saving / Total_Energy_pre * 100).ToString("0.0") + " %" });//절감률 전체 
                    data.Add(new { cname = "fc_saving_percent", data = fc_data[73] });
                    fc_data[74].Add(new { idx = i, val = (fc_total_elec * 0.4747 / 1000000 * 1000 + fc_total_gas / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000).ToString("0.0") });//tco2
                    data.Add(new { cname = "fc_tco2", data = fc_data[74] });
                    fc_data[75].Add(new { idx = i, val = (fc_total_elec * 0.00023 + fc_total_gas / 38.9 / 0.277778 * 0.00103).ToString("0.0") });//절감량 전체 
                    data.Add(new { cname = "fc_toe", data = fc_data[75] });

                    d = (fc_total_saving / Total_Energy_pre * 100);
                    charts += "{donut:" + d + "},";

                    //합산 계 
                    fc_data[76].Add(new { idx = i, val = fc_count_new.Sum().ToString("0.0") });// 신규 대수 합계  
                    data.Add(new { cname = "fc_count_new_total", data = fc_data[76] });
                    fc_data[77].Add(new { idx = i, val = fc_count_old.Sum().ToString("0.0") });//기존 대수 합계  
                    data.Add(new { cname = "fc_count_old_total", data = fc_data[77] });
                    fc_data[78].Add(new { idx = i, val = fc_elec_eta_total.ToString("0.0") });//효율 평균  
                    data.Add(new { cname = "fc_elec_eta_total", data = fc_data[78] });
                    fc_data[79].Add(new { idx = i, val = fc_heat_eta_total.ToString("0.0") });//효율 평균  
                    data.Add(new { cname = "fc_heat_eta_total", data = fc_data[79] });
                    fc_data[80].Add(new { idx = i, val = fc_elec_power_total.ToString("0.0") });//power   
                    data.Add(new { cname = "fc_elec_power_total", data = fc_data[80] });
                    fc_data[81].Add(new { idx = i, val = fc_heat_power_total.ToString("0.0") });//power
                    data.Add(new { cname = "fc_heat_power_total", data = fc_data[81] });
                    fc_data[82].Add(new { idx = i, val = (fc_Saving.Sum() / Total_Energy_pre * 100).ToString("0.0") + " %" });//절감량 합계  
                    data.Add(new { cname = "fc_saving_total2", data = fc_data[82] });
                    d = fc_Point_total;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 117는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 117) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    fc_data[83].Add(new { idx = i, val = sp });//성능수준 평균  
                    data.Add(new { cname = "fc_point_total", data = fc_data[83] });


                    #endregion


                    items.Add("Element_RESystem.htm");
                    s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                    s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
                    System.Text.Json.JsonSerializer.Serialize(PV_data[0].ToArray());

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