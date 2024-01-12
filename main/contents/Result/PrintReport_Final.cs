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
    public partial class PrintReport_Final : Form
    {
        bool scriptable = false;
        public PrintReport_Final()
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
      

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {

            string[][] 프로젝트유형  = Program.DB.querySQL(DB.type.ProjListDB, "Select type from projects where current = '1'");
            if (프로젝트유형[0][0] == "1")
            {
                Error_Report();
            }
            else
            {
                Saving_Report();
            }

            

        }

        private void Error_Report()
        {
            string s, s2;
            string[][] 번호 = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");

            List<object> items = new List<object>();
            List<object> items2 = new List<object>();
            List<object> data = new List<object>();

            List<object>[] __data = new List<object>[700];

            List<string> chart_전기사용량1 = new List<string>();
            List<string> chart_전기사용량2 = new List<string>();
            List<string> chart_전기사용량3 = new List<string>();
            List<string> chart_전기사용량4 = new List<string>();

            List<string> chart_난방전기소요량 = new List<string>();
            List<string> chart_냉방전기소요량 = new List<string>();
            List<string> chart_급탕전기소요량 = new List<string>();
            List<string> chart_조명전기소요량 = new List<string>();
            List<string> chart_공조전기소요량 = new List<string>();
            List<string> chart_기저전기소요량 = new List<string>();
            List<string> chart_총전기소요량 = new List<string>();

            List<string> chart_전기사용량 = new List<string>();
            List<string> chart_전기소요량 = new List<string>();
            List<string> chart_전기오차율 = new List<string>();

            List<string> chart_가스사용량1 = new List<string>();
            List<string> chart_가스사용량2 = new List<string>();
            List<string> chart_가스사용량3 = new List<string>();
            List<string> chart_가스사용량4 = new List<string>();

            List<string> chart_난방가스소요량 = new List<string>();
            List<string> chart_냉방가스소요량 = new List<string>();
            List<string> chart_급탕가스소요량 = new List<string>();
            List<string> chart_조명가스소요량 = new List<string>();
            List<string> chart_공조가스소요량 = new List<string>();
            List<string> chart_기저가스소요량 = new List<string>();
            List<string> chart_총가스소요량 = new List<string>();

            List<string> chart_가스사용량 = new List<string>();
            List<string> chart_가스소요량 = new List<string>();
            List<string> chart_가스오차율 = new List<string>();

            int i = -1, n;
            double[,] Quse_elec_mth = new double[4, 12]; double[] Quse_elec_a = new double[4];
            double[,] Quse_gas_mth = new double[4, 12]; double[] Quse_gas_a = new double[4];
            string[] year_elec = new string[3]; string[] year_gas = new string[3];
            while (++i < 700)
            {
                __data[i] = new List<object>();
            }

           
            i = -1;
            while (++i < 번호.Length)
            {
                #region 전기
                List<object> 전기사용량chart1 = new List<object>();
                List<object> 전기사용량chart2 = new List<object>();
                List<object> 전기사용량chart3 = new List<object>();
                List<object> 전기사용량chart4 = new List<object>();

                List<object> 난방전기소요량chart = new List<object>();
                List<object> 냉방전기소요량chart = new List<object>();
                List<object> 급탕전기소요량chart = new List<object>();
                List<object> 조명전기소요량chart = new List<object>();
                List<object> 공조전기소요량chart = new List<object>();
                List<object> 기저전기소요량chart = new List<object>();
                List<object> 총전기소요량chart = new List<object>();


                List<object> 전기사용량chart = new List<object>();
                List<object> 전기소요량chart = new List<object>();
                List<object> 전기오차율chart = new List<object>();

                try
                {
                    string[][] 연도 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "BuildingEnergyUse", "연도", "연료 = '전기'");
                    __data[0].Add(new { idx = i, val = 연도[0][0] + "년 전기 에너지사용량" }); //연도 표기 
                    __data[3].Add(new { idx = i, val = 연도[0][0] + "년 전기 에너지사용량" }); //연도 표기 
                    year_elec[0] = 연도[0][0]; 

                    __data[1].Add(new { idx = i, val = 연도[1][0] + "년 전기 에너지사용량" }); //연도 표기 
                    __data[4].Add(new { idx = i, val = 연도[1][0] + "년 전기 에너지사용량" }); //연도 표기 
                    year_elec[1] = 연도[1][0];

                    __data[2].Add(new { idx = i, val = 연도[2][0] + "년 전기 에너지사용량" }); //연도 표기 
                    __data[5].Add(new { idx = i, val = 연도[2][0] + "년 전기 에너지사용량" }); //연도 표기 
                    year_elec[2] = 연도[2][0];
                }
                catch { }
                double Area = 0;
                string[][] A = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "냉난방유무 <> '비냉난방'");
                for (int a = 0; a < A.Length; a++)
                {
                    Area += Convert.ToDouble(A[a][0]);
                }
                string[][] Value_사용시작일_전기 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "사용시작일", "연료='전기'");
                int yearnum = 0;
                if (Value_사용시작일_전기.Length > 0)
                {
                    if (Convert.ToDouble(Value_사용시작일_전기[0][0]) > 1)
                    {
                        string[][] Elec1, Elec2;
                        for (int mth = 0; mth < 11; mth++)
                        {
                            Elec1 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (mth + 1).ToString() + "월' AND 연료='전기'");
                            Elec2 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (mth + 2).ToString() + "월' AND 연료='전기'");
                            for (int k = 0; k < Elec1.Length; k++) //연도별
                            {
                                Quse_elec_mth[k, mth] = (Convert.ToDouble(Elec1[k][0]) * Convert.ToDouble(Value_사용시작일_전기[0][0]) / 30 + Convert.ToDouble(Elec2[k][0]) * (30 - Convert.ToDouble(Value_사용시작일_전기[0][0])) / 30);
                            }
                            yearnum = Elec1.Length;
                        }

                        Elec1 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (12).ToString() + "월' AND 연료='전기'");
                        Elec2 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (1).ToString() + "월' AND 연료='전기'");
                        for (int k = 0; k < Elec1.Length; i++) //연도별
                        {
                            Quse_elec_mth[k, 12] = (Convert.ToDouble(Elec1[k][0]) * Convert.ToDouble(Value_사용시작일_전기[0][0]) / 30 + Convert.ToDouble(Elec2[k][0]) * (30 - Convert.ToDouble(Value_사용시작일_전기[0][0])) / 30);
                        }

                    }
                    else
                    {
                        for (int mth = 0; mth < 12; mth++)
                        {
                            string[][] Elec = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (mth + 1).ToString() + "월' AND 연료='전기'");
                            for (int k = 0; k < Elec.Length; k++) //연도별
                            {
                                Quse_elec_mth[k, mth] = Convert.ToDouble(Elec[k][0]);
                            }
                            yearnum = Elec.Length;
                        }
                    }
                }

                for (int mth = 0; mth < 12; mth++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        Quse_elec_mth[3, mth] += Quse_elec_mth[k, mth];
                    }
                    Quse_elec_mth[3, mth] = Quse_elec_mth[3, mth] / yearnum;
                    Quse_elec_a[0] += Quse_elec_mth[0, mth];
                    Quse_elec_a[1] += Quse_elec_mth[1, mth];
                    Quse_elec_a[2] += Quse_elec_mth[2, mth];
                    Quse_elec_a[3] += Quse_elec_mth[3, mth];
                }

                __data[6].Add(new { idx = i, val = Quse_elec_a[0] }); //연간 에너지사용량
                __data[7].Add(new { idx = i, val = Quse_elec_a[1] });
                __data[8].Add(new { idx = i, val = Quse_elec_a[2] });
                __data[9].Add(new { idx = i, val = Quse_elec_a[3] });

                __data[28].Add(new { idx = i, val = (Quse_elec_a[0] / Area).ToString("0.0") }); //바닥면적당 연간 에너지사용량
                __data[29].Add(new { idx = i, val = (Quse_elec_a[1] / Area).ToString("0.0") });
                __data[30].Add(new { idx = i, val = (Quse_elec_a[2] / Area).ToString("0.0") });
                __data[31].Add(new { idx = i, val = (Quse_elec_a[3] / Area).ToString("0.0") });

                for (int mth = 0; mth < 12; mth++)
                {
                    __data[10].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Quse_elec_mth[0, mth].ToString()) }); //월별 에너지사용량 
                    __data[11].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Quse_elec_mth[1, mth].ToString()) });
                    __data[12].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Quse_elec_mth[2, mth].ToString()) });
                    __data[13].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Quse_elec_mth[3, mth].ToString()) });


                    전기사용량chart1.Add(Math.Round(Double.Parse(Quse_elec_mth[0, mth].ToString()), 3) + 0);
                    전기사용량chart2.Add(Math.Round(Double.Parse(Quse_elec_mth[1, mth].ToString()), 3) + 0);
                    전기사용량chart3.Add(Math.Round(Double.Parse(Quse_elec_mth[2, mth].ToString()), 3) + 0);
                    전기사용량chart4.Add(Math.Round(Double.Parse(Quse_elec_mth[3, mth].ToString()), 3) + 0);
                    전기사용량chart.Add(Math.Round(Double.Parse(Quse_elec_mth[3, mth].ToString()), 3) + 0);
                }
                double Qh_a_전기 = 0, Qc_a_전기 = 0, Qw_a_전기 = 0, Ql_a_전기 = 0, Qv_a_전기 = 0, Qbase_a_전기 = 0, Qtot_a_전기 = 0;
                double[] Qtot_mth_전기 = new double[12];
                double Error_mth_avg_전기 = 0;
                for (int mth = 0; mth < 12; mth++)
                {
                    string[][] Final = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,기저에너지", "연료='전기' and 월 ='" + (mth + 1).ToString() + "월'");
                    __data[14].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][0]) }); //월별 난방 
                    __data[15].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][1]) }); //월별 냉방 
                    __data[16].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][2]) }); //월별 급탕 
                    __data[17].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][3]) }); //월별 조명 
                    __data[18].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][4]) }); //월별 공조
                    __data[19].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][5]) }); //월별 기저 
                    난방전기소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][0])), 3) + 0);
                    냉방전기소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][1])), 3) + 0);
                    급탕전기소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][2])), 3) + 0);
                    조명전기소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][3])), 3) + 0);
                    공조전기소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][4])), 3) + 0);
                    기저전기소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][5])), 3) + 0);


                    Qtot_mth_전기[mth] = Convert.ToDouble(Final[0][0]) + Convert.ToDouble(Final[0][1]) + Convert.ToDouble(Final[0][2]) + Convert.ToDouble(Final[0][3]) + Convert.ToDouble(Final[0][4]) + Convert.ToDouble(Final[0][5]);
                    __data[20].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Qtot_mth_전기[mth].ToString()) }); //월별 전기 에너지소요량 
                    총전기소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Qtot_mth_전기[mth].ToString())), 3) + 0);
                    전기소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Qtot_mth_전기[mth].ToString())), 3) + 0);

                    __data[39].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(((Qtot_mth_전기[mth] - Quse_elec_mth[3, mth]) / Quse_elec_mth[3, mth] * 100).ToString()) }); //오차율
                    Error_mth_avg_전기 += Math.Abs((Qtot_mth_전기[mth] - Quse_elec_mth[3, mth]) / Quse_elec_mth[3, mth] * 100);
                    전기오차율chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Math.Abs(((Qtot_mth_전기[mth] - Quse_elec_mth[3, mth]) / Quse_elec_mth[3, mth])).ToString())), 3) + 0);  /// >>> 백분율 단위로 표시 필요 

                    Qh_a_전기 += Convert.ToDouble(Final[0][0]);
                    Qc_a_전기 += Convert.ToDouble(Final[0][1]);
                    Qw_a_전기 += Convert.ToDouble(Final[0][2]);
                    Ql_a_전기 += Convert.ToDouble(Final[0][3]);
                    Qv_a_전기 += Convert.ToDouble(Final[0][4]);
                    Qbase_a_전기 += Convert.ToDouble(Final[0][5]);

                }
                Qtot_a_전기 = Qh_a_전기 + Qc_a_전기 + Qw_a_전기 + Ql_a_전기 + Qv_a_전기 + Qbase_a_전기;

                __data[21].Add(new { idx = i, val = Qh_a_전기 });
                __data[22].Add(new { idx = i, val = Qc_a_전기 });
                __data[23].Add(new { idx = i, val = Qw_a_전기 });
                __data[24].Add(new { idx = i, val = Ql_a_전기 });
                __data[25].Add(new { idx = i, val = Qv_a_전기 });
                __data[26].Add(new { idx = i, val = Qbase_a_전기 });
                __data[27].Add(new { idx = i, val = Qtot_a_전기 });


                __data[32].Add(new { idx = i, val = (Qh_a_전기 / Area).ToString("0.0") });
                __data[33].Add(new { idx = i, val = (Qc_a_전기 / Area).ToString("0.0") });
                __data[34].Add(new { idx = i, val = (Qw_a_전기 / Area).ToString("0.0") });
                __data[35].Add(new { idx = i, val = (Ql_a_전기 / Area).ToString("0.0") });
                __data[36].Add(new { idx = i, val = (Qv_a_전기 / Area).ToString("0.0") });
                __data[37].Add(new { idx = i, val = (Qbase_a_전기 / Area).ToString("0.0") });
                __data[38].Add(new { idx = i, val = (Qtot_a_전기 / Area).ToString("0.0") });

                Error_mth_avg_전기 = Error_mth_avg_전기 / 12;
                __data[40].Add(new { idx = i, val = Error_mth_avg_전기.ToString("0.0") + "%" });

                double Error_a_전기 = (Quse_elec_a[3] - Qtot_a_전기) / Quse_elec_a[3] * 100;
                __data[41].Add(new { idx = i, val = Error_a_전기.ToString("0.0") + "%" });
                chart_전기사용량1.Add(System.Text.Json.JsonSerializer.Serialize(전기사용량chart1.ToArray()));
                chart_전기사용량2.Add(System.Text.Json.JsonSerializer.Serialize(전기사용량chart2.ToArray()));
                chart_전기사용량3.Add(System.Text.Json.JsonSerializer.Serialize(전기사용량chart3.ToArray()));
                chart_전기사용량4.Add(System.Text.Json.JsonSerializer.Serialize(전기사용량chart4.ToArray()));


                chart_난방전기소요량.Add(System.Text.Json.JsonSerializer.Serialize(난방전기소요량chart.ToArray()));
                chart_냉방전기소요량.Add(System.Text.Json.JsonSerializer.Serialize(냉방전기소요량chart.ToArray()));
                chart_급탕전기소요량.Add(System.Text.Json.JsonSerializer.Serialize(급탕전기소요량chart.ToArray()));
                chart_조명전기소요량.Add(System.Text.Json.JsonSerializer.Serialize(조명전기소요량chart.ToArray()));
                chart_공조전기소요량.Add(System.Text.Json.JsonSerializer.Serialize(공조전기소요량chart.ToArray()));
                chart_기저전기소요량.Add(System.Text.Json.JsonSerializer.Serialize(기저전기소요량chart.ToArray()));

                chart_전기사용량.Add(System.Text.Json.JsonSerializer.Serialize(전기사용량chart.ToArray()));
                chart_전기소요량.Add(System.Text.Json.JsonSerializer.Serialize(전기소요량chart.ToArray()));
                chart_전기오차율.Add(System.Text.Json.JsonSerializer.Serialize(전기오차율chart.ToArray()));

                ////////////////////////////////////////////////////////////////////
                data.Add(new { cname = "yeartitle1", data = __data[0] });
                data.Add(new { cname = "yeartitle2", data = __data[1] });
                data.Add(new { cname = "yeartitle3", data = __data[2] });

                data.Add(new { cname = "yeartitle4", data = __data[3] });
                data.Add(new { cname = "yeartitle5", data = __data[4] });
                data.Add(new { cname = "yeartitle6", data = __data[5] });

                data.Add(new { cname = "quse_elec_a1", data = __data[6] });
                data.Add(new { cname = "quse_elec_a2", data = __data[7] });
                data.Add(new { cname = "quse_elec_a3", data = __data[8] });
                data.Add(new { cname = "quse_elec_a4", data = __data[9] });

                data.Add(new { cname = "quse_elec_mth1", data = __data[10] });
                data.Add(new { cname = "quse_elec_mth2", data = __data[11] });
                data.Add(new { cname = "quse_elec_mth3", data = __data[12] });
                data.Add(new { cname = "quse_elec_mth4", data = __data[13] });

                data.Add(new { cname = "qhf_mth", data = __data[14] });
                data.Add(new { cname = "qcf_mth", data = __data[15] });
                data.Add(new { cname = "qwf_mth", data = __data[16] });
                data.Add(new { cname = "qlf_mth", data = __data[17] });
                data.Add(new { cname = "qvf_mth", data = __data[18] });
                data.Add(new { cname = "qbasef_mth", data = __data[19] });
                data.Add(new { cname = "qf_tot_mth", data = __data[20] });

                data.Add(new { cname = "qhf_a", data = __data[21] });
                data.Add(new { cname = "qcf_a", data = __data[22] });
                data.Add(new { cname = "qwf_a", data = __data[23] });
                data.Add(new { cname = "qlf_a", data = __data[24] });
                data.Add(new { cname = "qvf_a", data = __data[25] });
                data.Add(new { cname = "qbasef_a", data = __data[26] });
                data.Add(new { cname = "qf_tot_a", data = __data[27] });

                data.Add(new { cname = "quse_elec_a1_area", data = __data[28] });
                data.Add(new { cname = "quse_elec_a2_area", data = __data[29] });
                data.Add(new { cname = "quse_elec_a3_area", data = __data[30] });
                data.Add(new { cname = "quse_elec_a4_area", data = __data[31] });

                data.Add(new { cname = "qhf_a_area", data = __data[32] });
                data.Add(new { cname = "qcf_a_area", data = __data[33] });
                data.Add(new { cname = "qwf_a_area", data = __data[34] });
                data.Add(new { cname = "qlf_a_area", data = __data[35] });
                data.Add(new { cname = "qvf_a_area", data = __data[36] });
                data.Add(new { cname = "qbasef_a_area", data = __data[37] });
                data.Add(new { cname = "qf_tot_a_area", data = __data[38] });

                data.Add(new { cname = "error_mth", data = __data[39] });
                data.Add(new { cname = "error_mth_avg", data = __data[40] });
                data.Add(new { cname = "error_a", data = __data[41] });
                #endregion

                #region 가스
                List<object> 가스사용량chart1 = new List<object>();
                List<object> 가스사용량chart2 = new List<object>();
                List<object> 가스사용량chart3 = new List<object>();
                List<object> 가스사용량chart4 = new List<object>();

                List<object> 난방가스소요량chart = new List<object>();
                List<object> 냉방가스소요량chart = new List<object>();
                List<object> 급탕가스소요량chart = new List<object>();
                List<object> 조명가스소요량chart = new List<object>();
                List<object> 공조가스소요량chart = new List<object>();
                List<object> 기저가스소요량chart = new List<object>();
                List<object> 총가스소요량chart = new List<object>();


                List<object> 가스사용량chart = new List<object>();
                List<object> 가스소요량chart = new List<object>();
                List<object> 가스오차율chart = new List<object>();

                try
                {
                    string[][] 연도 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "BuildingEnergyUse", "연도", "연료 = '가스'");
                    __data[50].Add(new { idx = i, val = 연도[0][0] + "년 가스 에너지사용량" }); //연도 표기 
                    __data[53].Add(new { idx = i, val = 연도[0][0] + "년 가스 에너지사용량" }); //연도 표기 
                    year_gas[0] = 연도[0][0];

                    __data[51].Add(new { idx = i, val = 연도[1][0] + "년 가스 에너지사용량" }); //연도 표기 
                    __data[54].Add(new { idx = i, val = 연도[1][0] + "년 가스 에너지사용량" }); //연도 표기 
                    year_gas[1] = 연도[1][0];

                    __data[52].Add(new { idx = i, val = 연도[2][0] + "년 가스 에너지사용량" }); //연도 표기 
                    __data[55].Add(new { idx = i, val = 연도[2][0] + "년 가스 에너지사용량" }); //연도 표기 
                    year_gas[2] = 연도[2][0];
                }
                catch { }
                
                string[][] Value_사용시작일_가스 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "사용시작일", "연료='전기'");
                int yearnum_가스 = 0;
                if (Value_사용시작일_가스.Length > 0)
                {
                    if (Convert.ToDouble(Value_사용시작일_가스[0][0]) > 1)
                    {
                        string[][] Gas1, Gas2;
                        for (int mth = 0; mth < 11; mth++)
                        {
                            Gas1 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (mth + 1).ToString() + "월' AND 연료='가스'");
                            Gas2 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (mth + 2).ToString() + "월' AND 연료='가스'");
                            for (int k = 0; k < Gas1.Length; k++) //연도별
                            {
                                Quse_gas_mth[k, mth] = (Convert.ToDouble(Gas1[k][0]) * Convert.ToDouble(Value_사용시작일_가스[0][0]) / 30 + Convert.ToDouble(Gas2[k][0]) * (30 - Convert.ToDouble(Value_사용시작일_가스[0][0])) / 30);
                            }
                            yearnum = Gas1.Length;
                        }

                        Gas1 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (12).ToString() + "월' AND 연료='가스'");
                        Gas2 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (1).ToString() + "월' AND 연료='가스'");
                        for (int k = 0; k < Gas1.Length; i++) //연도별
                        {
                            Quse_gas_mth[k, 12] = (Convert.ToDouble(Gas1[k][0]) * Convert.ToDouble(Value_사용시작일_가스[0][0]) / 30 + Convert.ToDouble(Gas2[k][0]) * (30 - Convert.ToDouble(Value_사용시작일_가스[0][0])) / 30);
                        }

                    }
                    else
                    {
                        for (int mth = 0; mth < 12; mth++)
                        {
                            string[][] Gas = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (mth + 1).ToString() + "월' AND 연료='가스'");
                            for (int k = 0; k < Gas.Length; k++) //연도별
                            {
                                Quse_gas_mth[k, mth] = Convert.ToDouble(Gas[k][0]);
                            }
                            yearnum_가스 = Gas.Length;
                        }
                    }
                }

                for (int mth = 0; mth < 12; mth++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        Quse_gas_mth[3, mth] += Quse_gas_mth[k, mth];
                    }
                    Quse_gas_mth[3, mth] = Quse_gas_mth[3, mth] / yearnum_가스;
                    Quse_gas_a[0] += Quse_gas_mth[0, mth];
                    Quse_gas_a[1] += Quse_gas_mth[1, mth];
                    Quse_gas_a[2] += Quse_gas_mth[2, mth];
                    Quse_gas_a[3] += Quse_gas_mth[3, mth];
                }

                __data[56].Add(new { idx = i, val = Quse_gas_a[0] }); //연간 에너지사용량
                __data[57].Add(new { idx = i, val = Quse_gas_a[1] });
                __data[58].Add(new { idx = i, val = Quse_gas_a[2] });
                __data[59].Add(new { idx = i, val = Quse_gas_a[3] });

                __data[78].Add(new { idx = i, val = (Quse_gas_a[0] / Area).ToString("0.0") }); //바닥면적당 연간 에너지사용량
                __data[79].Add(new { idx = i, val = (Quse_gas_a[1] / Area).ToString("0.0") });
                __data[80].Add(new { idx = i, val = (Quse_gas_a[2] / Area).ToString("0.0") });
                __data[81].Add(new { idx = i, val = (Quse_gas_a[3] / Area).ToString("0.0") });

                for (int mth = 0; mth < 12; mth++)
                {
                    __data[60].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Quse_gas_mth[0, mth].ToString()) }); //월별 에너지사용량 
                    __data[61].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Quse_gas_mth[1, mth].ToString()) });
                    __data[62].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Quse_gas_mth[2, mth].ToString()) });
                    __data[63].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Quse_gas_mth[3, mth].ToString()) });


                    가스사용량chart1.Add(Math.Round(Double.Parse(Quse_gas_mth[0, mth].ToString()), 3) + 0);
                    가스사용량chart2.Add(Math.Round(Double.Parse(Quse_gas_mth[1, mth].ToString()), 3) + 0);
                    가스사용량chart3.Add(Math.Round(Double.Parse(Quse_gas_mth[2, mth].ToString()), 3) + 0);
                    가스사용량chart4.Add(Math.Round(Double.Parse(Quse_gas_mth[3, mth].ToString()), 3) + 0);
                    가스사용량chart.Add(Math.Round(Double.Parse(Quse_gas_mth[3, mth].ToString()), 3) + 0);
                }
                double Qh_a_가스 = 0, Qc_a_가스 = 0, Qw_a_가스 = 0, Ql_a_가스 = 0, Qv_a_가스 = 0, Qbase_a_가스 = 0, Qtot_a_가스 = 0;
                double[] Qtot_mth_가스 = new double[12];
                double Error_mth_avg_가스 = 0;
                for (int mth = 0; mth < 12; mth++)
                {
                    string[][] Final = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,기저에너지", "연료='가스' and 월 ='" + (mth + 1).ToString() + "월'");
                    __data[64].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][0]) }); //월별 난방 
                    __data[65].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][1]) }); //월별 냉방 
                    __data[66].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][2]) }); //월별 급탕 
                    __data[67].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][3]) }); //월별 조명 
                    __data[68].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][4]) }); //월별 공조
                    __data[69].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][5]) }); //월별 기저 
                    난방가스소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][0])), 3) + 0);
                    냉방가스소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][1])), 3) + 0);
                    급탕가스소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][2])), 3) + 0);
                    조명가스소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][3])), 3) + 0);
                    공조가스소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][4])), 3) + 0);
                    기저가스소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][5])), 3) + 0);


                    Qtot_mth_가스[mth] = Convert.ToDouble(Final[0][0]) + Convert.ToDouble(Final[0][1]) + Convert.ToDouble(Final[0][2]) + Convert.ToDouble(Final[0][3]) + Convert.ToDouble(Final[0][4]) + Convert.ToDouble(Final[0][5]);
                    __data[70].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Qtot_mth_가스[mth].ToString()) }); //월별 가스 에너지소요량 
                    총가스소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Qtot_mth_가스[mth].ToString())), 3) + 0);
                    가스소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Qtot_mth_가스[mth].ToString())), 3) + 0);

                    __data[89].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(((Qtot_mth_가스[mth] - Quse_gas_mth[3, mth]) / Quse_gas_mth[3, mth] * 100).ToString()) }); //오차율
                    Error_mth_avg_가스 += Math.Abs((Qtot_mth_가스[mth] - Quse_gas_mth[3, mth]) / Quse_gas_mth[3, mth] * 100);
                    가스오차율chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(((Qtot_mth_가스[mth] - Quse_gas_mth[3, mth]) / Quse_gas_mth[3, mth]).ToString())), 3) + 0);  /// >>> 백분율 단위로 표시 필요 

                    Qh_a_가스 += Convert.ToDouble(Final[0][0]);
                    Qc_a_가스 += Convert.ToDouble(Final[0][1]);
                    Qw_a_가스 += Convert.ToDouble(Final[0][2]);
                    Ql_a_가스 += Convert.ToDouble(Final[0][3]);
                    Qv_a_가스 += Convert.ToDouble(Final[0][4]);
                    Qbase_a_가스 += Convert.ToDouble(Final[0][5]);

                }
                Qtot_a_가스 = Qh_a_가스 + Qc_a_가스 + Qw_a_가스 + Ql_a_가스 + Qv_a_가스 + Qbase_a_가스;

                __data[71].Add(new { idx = i, val = Qh_a_가스 });
                __data[72].Add(new { idx = i, val = Qc_a_가스 });
                __data[73].Add(new { idx = i, val = Qw_a_가스 });
                __data[74].Add(new { idx = i, val = Ql_a_가스 });
                __data[75].Add(new { idx = i, val = Qv_a_가스 });
                __data[76].Add(new { idx = i, val = Qbase_a_가스 });
                __data[77].Add(new { idx = i, val = Qtot_a_가스 });


                __data[82].Add(new { idx = i, val = (Qh_a_가스 / Area).ToString("0.0") });
                __data[83].Add(new { idx = i, val = (Qc_a_가스 / Area).ToString("0.0") });
                __data[84].Add(new { idx = i, val = (Qw_a_가스 / Area).ToString("0.0") });
                __data[85].Add(new { idx = i, val = (Ql_a_가스 / Area).ToString("0.0") });
                __data[86].Add(new { idx = i, val = (Qv_a_가스 / Area).ToString("0.0") });
                __data[87].Add(new { idx = i, val = (Qbase_a_가스 / Area).ToString("0.0") });
                __data[88].Add(new { idx = i, val = (Qtot_a_가스 / Area).ToString("0.0") });

                Error_mth_avg_가스 = Error_mth_avg_가스 / 12;
                __data[90].Add(new { idx = i, val = Error_mth_avg_가스.ToString("0.0") + "%" });

                double Error_a_가스 = (Quse_gas_a[3] - Qtot_a_가스) / Quse_gas_a[3] * 100;
                __data[91].Add(new { idx = i, val = Error_a_가스.ToString("0.0") + "%" });
                chart_가스사용량1.Add(System.Text.Json.JsonSerializer.Serialize(가스사용량chart1.ToArray()));
                chart_가스사용량2.Add(System.Text.Json.JsonSerializer.Serialize(가스사용량chart2.ToArray()));
                chart_가스사용량3.Add(System.Text.Json.JsonSerializer.Serialize(가스사용량chart3.ToArray()));
                chart_가스사용량4.Add(System.Text.Json.JsonSerializer.Serialize(가스사용량chart4.ToArray()));


                chart_난방가스소요량.Add(System.Text.Json.JsonSerializer.Serialize(난방가스소요량chart.ToArray()));
                chart_냉방가스소요량.Add(System.Text.Json.JsonSerializer.Serialize(냉방가스소요량chart.ToArray()));
                chart_급탕가스소요량.Add(System.Text.Json.JsonSerializer.Serialize(급탕가스소요량chart.ToArray()));
                chart_조명가스소요량.Add(System.Text.Json.JsonSerializer.Serialize(조명가스소요량chart.ToArray()));
                chart_공조가스소요량.Add(System.Text.Json.JsonSerializer.Serialize(공조가스소요량chart.ToArray()));
                chart_기저가스소요량.Add(System.Text.Json.JsonSerializer.Serialize(기저가스소요량chart.ToArray()));

                chart_가스사용량.Add(System.Text.Json.JsonSerializer.Serialize(가스사용량chart.ToArray()));
                chart_가스소요량.Add(System.Text.Json.JsonSerializer.Serialize(가스소요량chart.ToArray()));
                chart_가스오차율.Add(System.Text.Json.JsonSerializer.Serialize(가스오차율chart.ToArray()));

                ////////////////////////////////////////////////////////////////////
             
                #endregion
            }

            items.Add("Error_Report.htm");
            s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
            s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
            System.Text.Json.JsonSerializer.Serialize(__data[10].ToArray());

            string s3 = "", s4;
            i = -1;
            double[] max_elec = new double[4]; double max_graph_elec =0;
            double[] max_gas = new double[4]; double max_graph_gas = 0;

            for (int k = 0; k < 4; k++)
            {
                for (int mth = 0; mth < 11; mth++)
                {
                    if (max_elec[k] < Quse_elec_mth[k, mth])
                    {
                        max_elec[k] = Quse_elec_mth[k, mth ];
                    }
                }
            }
            for (int k = 0; k < 4; k++)
            {
                if (max_graph_elec < max_elec[k])
                {
                    max_graph_elec = max_elec[k];
                }
            }

            Debug.Print("start");

                if (s3 != "") s3 += ",";

                s4 = "{data:[{type:\"line\",label:\""+year_elec[0]+"\",data:" + chart_전기사용량1[0] + ",borderColor:\"#5B9BD5\",backgroundColor:\"#5B9BD5\",dash:false}," +
                "{type:\"line\",label:\""+ year_elec[1]+"\",data:" + chart_전기사용량2[0] + ",borderColor:\"#70AD47\",backgroundColor:\"#70AD47\",dash:false}," +
                "{type:\"line\",label:\""+year_elec[2]+"\",data:" + chart_전기사용량3[0] + ",borderColor:\"#4472C4\",backgroundColor:\"#4472C4\",dash:false}," +
                "{type:\"line\",label:\"평균\",data:" + chart_전기사용량4[0] + ",borderColor:\"#ED7D31\",backgroundColor:\"#ED7D31\",dash:false}," +
                "],max:" + (Math.Round(max_graph_elec / 1000) * 1000 + 500).ToString() + ",step:100,legend:true}";
                s3 += s4;

            
           runScript("init(" + s + "," + s2 + "," + "[" + s3 + "])");
        }

        private void Saving_Report()
        {
            string s, s2;
            string[][] 번호 = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");

            List<object> items = new List<object>();
            List<object> items2 = new List<object>();
            List<object> data = new List<object>();

            List<object>[] __data = new List<object>[700];

            int i = -1, n;


            while (++i < 700)
            {
                __data[i] = new List<object>();
            }

            i = -1;

            while (++i < 번호.Length)
            {

                string[][] res = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기존프로젝트");

                if (res.Length > 0)
                {
                    double Qh_a2 = 0, Qc_a2 = 0, Qw_a2 = 0, Ql_a2 = 0, Qv_a2 = 0, Qbase_a2 = 0, Qtot_a2 = 0;
                    double[] Qtot2_mth = new double[12];
                    for (int mth = 0; mth < 12; mth++)
                    { //리모델링전 전기 소요량 
                        string[][] Final = Program.DB.querySQL(res[0][0], "SELECT 난방,냉방,급탕,조명,공조,기저에너지 FROM FinalEnergy_Result where 연료 = '전기' and 월 = '" + (mth + 1).ToString() + "월'");


                        __data[0].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][0]) }); //월별 난방 
                        __data[1].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][1]) }); //월별 냉방 
                        __data[2].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][2]) }); //월별 급탕 
                        __data[3].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][3]) }); //월별 조명 
                        __data[4].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][4]) }); //월별 공조
                        __data[5].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][5]) }); //월별 기저 

                        Qtot2_mth[mth] = Convert.ToDouble(Final[0][0]) + Convert.ToDouble(Final[0][1]) + Convert.ToDouble(Final[0][2]) + Convert.ToDouble(Final[0][3]) + Convert.ToDouble(Final[0][4]) + Convert.ToDouble(Final[0][5]);
                        __data[6].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Qtot2_mth[mth].ToString()) }); //월별 전기 에너지소요량 

                        Qh_a2 += Convert.ToDouble(Final[0][0]);
                        Qc_a2 += Convert.ToDouble(Final[0][1]);
                        Qw_a2 += Convert.ToDouble(Final[0][2]);
                        Ql_a2 += Convert.ToDouble(Final[0][3]);
                        Qv_a2 += Convert.ToDouble(Final[0][4]);
                        Qbase_a2 += Convert.ToDouble(Final[0][5]);
                    }
                    Qtot_a2 = Qh_a2 + Qc_a2 + Qw_a2 + Ql_a2 + Qv_a2 + Qbase_a2;
                    __data[7].Add(new { idx = i, val = Qh_a2 });
                    __data[8].Add(new { idx = i, val = Qc_a2 });
                    __data[9].Add(new { idx = i, val = Qw_a2 });
                    __data[10].Add(new { idx = i, val = Ql_a2 });
                    __data[11].Add(new { idx = i, val = Qv_a2 });
                    __data[12].Add(new { idx = i, val = Qbase_a2 });
                    __data[13].Add(new { idx = i, val = Qtot_a2 });

                    double Area2 = 0;
                    string[][] A2 = Program.DB.querySQL(res[0][0], "Select 순바닥면적 From ZoneGeneral_Form where 냉난방유무 <> '비냉난방'");
                    for (int a = 0; a < A2.Length; a++)
                    {
                        Area2 += Convert.ToDouble(A2[a][0]);
                    }

                    __data[14].Add(new { idx = i, val = (Qh_a2 / Area2).ToString("0.0") });
                    __data[15].Add(new { idx = i, val = (Qc_a2 / Area2).ToString("0.0") });
                    __data[16].Add(new { idx = i, val = (Qw_a2 / Area2).ToString("0.0") });
                    __data[17].Add(new { idx = i, val = (Ql_a2 / Area2).ToString("0.0") });
                    __data[18].Add(new { idx = i, val = (Qv_a2 / Area2).ToString("0.0") });
                    __data[19].Add(new { idx = i, val = (Qbase_a2 / Area2).ToString("0.0") });
                    __data[20].Add(new { idx = i, val = (Qtot_a2 / Area2).ToString("0.0") });


                    double Qh_a = 0, Qc_a = 0, Qw_a = 0, Ql_a = 0, Qv_a = 0, Qbase_a = 0, Qtot_a = 0;
                    double[] Qtot_mth = new double[12];
                    for (int mth = 0; mth < 12; mth++)
                    { //리모델링후 전기 소요량 
                        string[][] Final = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,기저에너지", "연료='전기' and 월 ='" + (mth + 1).ToString() + "월'");
                        __data[21].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][0]) }); //월별 난방 
                        __data[22].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][1]) }); //월별 냉방 
                        __data[23].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][2]) }); //월별 급탕 
                        __data[24].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][3]) }); //월별 조명 
                        __data[25].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][4]) }); //월별 공조
                        __data[26].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Final[0][5]) }); //월별 기저 

                        Qtot_mth[mth] = Convert.ToDouble(Final[0][0]) + Convert.ToDouble(Final[0][1]) + Convert.ToDouble(Final[0][2]) + Convert.ToDouble(Final[0][3]) + Convert.ToDouble(Final[0][4]) + Convert.ToDouble(Final[0][5]);
                        __data[27].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Qtot_mth[mth].ToString()) }); //월별 전기 에너지소요량 


                        Qh_a += Convert.ToDouble(Final[0][0]);
                        Qc_a += Convert.ToDouble(Final[0][1]);
                        Qw_a += Convert.ToDouble(Final[0][2]);
                        Ql_a += Convert.ToDouble(Final[0][3]);
                        Qv_a += Convert.ToDouble(Final[0][4]);
                        Qbase_a += Convert.ToDouble(Final[0][5]);
                    }
                    Qtot_a = Qh_a + Qc_a + Qw_a + Ql_a + Qv_a + Qbase_a;
                    __data[28].Add(new { idx = i, val = Qh_a });
                    __data[29].Add(new { idx = i, val = Qc_a });
                    __data[30].Add(new { idx = i, val = Qw_a });
                    __data[31].Add(new { idx = i, val = Ql_a });
                    __data[32].Add(new { idx = i, val = Qv_a });
                    __data[33].Add(new { idx = i, val = Qbase_a });
                    __data[34].Add(new { idx = i, val = Qtot_a });

                    double Area = 0;
                    string[][] A = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "냉난방유무 <> '비냉난방'");
                    for (int a = 0; a < A.Length; a++)
                    {
                        Area += Convert.ToDouble(A[a][0]);
                    }

                    __data[35].Add(new { idx = i, val = (Qh_a / Area).ToString("0.0") });
                    __data[36].Add(new { idx = i, val = (Qc_a / Area).ToString("0.0") });
                    __data[37].Add(new { idx = i, val = (Qw_a / Area).ToString("0.0") });
                    __data[38].Add(new { idx = i, val = (Ql_a / Area).ToString("0.0") });
                    __data[39].Add(new { idx = i, val = (Qv_a / Area).ToString("0.0") });
                    __data[40].Add(new { idx = i, val = (Qbase_a / Area).ToString("0.0") });
                    __data[41].Add(new { idx = i, val = (Qtot_a / Area).ToString("0.0") });

                    double SavingPercent_mth_avg = 0; double Saving_mth_avg = 0;
                    for (int mth = 0; mth < 12; mth++)
                    { //전기소요량 월별 절감량 
                        Saving_mth_avg += (Qtot2_mth[mth] - Qtot_mth[mth]);
                        SavingPercent_mth_avg += (Qtot2_mth[mth] - Qtot_mth[mth]) / Qtot2_mth[mth] * 100;
                    }
                    Saving_mth_avg = Saving_mth_avg / 12; 
                    SavingPercent_mth_avg = SavingPercent_mth_avg / 12;
                    __data[43].Add(new { idx = i, val = (((Qh_a2 - Qh_a) / Qh_a2) * 100).ToString("0.0") + "%" });
                    __data[44].Add(new { idx = i, val = SavingPercent_mth_avg.ToString("0.0") + "%" }); 

                    ///////////////////////////////////////////////////////////
                    data.Add(new { cname = "qhf_mth_old", data = __data[0] });
                    data.Add(new { cname = "qcf_mth_old", data = __data[1] });
                    data.Add(new { cname = "qwf_mth_old", data = __data[2] });
                    data.Add(new { cname = "qlf_mth_old", data = __data[3] });
                    data.Add(new { cname = "qvf_mth_old", data = __data[4] });
                    data.Add(new { cname = "qbasef_mth_old", data = __data[5] });
                    data.Add(new { cname = "qf_tot_mth_old", data = __data[6] });

                    data.Add(new { cname = "qhf_a_old", data = __data[7] });
                    data.Add(new { cname = "qcf_a_old", data = __data[8] });
                    data.Add(new { cname = "qwf_a_old", data = __data[9] });
                    data.Add(new { cname = "qlf_a_old", data = __data[10] });
                    data.Add(new { cname = "qvf_a_old", data = __data[11] });
                    data.Add(new { cname = "qbasef_a_old", data = __data[12] });
                    data.Add(new { cname = "qf_tot_a_old", data = __data[13] });

                    data.Add(new { cname = "qhf_a_area_old", data = __data[14] });
                    data.Add(new { cname = "qcf_a_area_old", data = __data[15] });
                    data.Add(new { cname = "qwf_a_area_old", data = __data[16] });
                    data.Add(new { cname = "qlf_a_area_old", data = __data[17] });
                    data.Add(new { cname = "qvf_a_area_old", data = __data[18] });
                    data.Add(new { cname = "qbasef_a_area_old", data = __data[19] });
                    data.Add(new { cname = "qf_tot_a_area_old", data = __data[20] });

                    data.Add(new { cname = "qhf_mth_new", data = __data[21] });
                    data.Add(new { cname = "qcf_mth_new", data = __data[22] });
                    data.Add(new { cname = "qwf_mth_new", data = __data[23] });
                    data.Add(new { cname = "qlf_mth_new", data = __data[24] });
                    data.Add(new { cname = "qvf_mth_new", data = __data[25] });
                    data.Add(new { cname = "qbasef_mth_new", data = __data[26] });
                    data.Add(new { cname = "qf_tot_mth_new", data = __data[27] });

                    data.Add(new { cname = "qhf_a_new", data = __data[28] });
                    data.Add(new { cname = "qcf_a_new", data = __data[29] });
                    data.Add(new { cname = "qwf_a_new", data = __data[30] });
                    data.Add(new { cname = "qlf_a_new", data = __data[31] });
                    data.Add(new { cname = "qvf_a_new", data = __data[32] });
                    data.Add(new { cname = "qbasef_a_new", data = __data[33] });
                    data.Add(new { cname = "qf_tot_a_new", data = __data[34] });

                    data.Add(new { cname = "qhf_a_area_new", data = __data[35] });
                    data.Add(new { cname = "qcf_a_area_new", data = __data[36] });
                    data.Add(new { cname = "qwf_a_area_new", data = __data[37] });
                    data.Add(new { cname = "qlf_a_area_new", data = __data[38] });
                    data.Add(new { cname = "qvf_a_area_new", data = __data[39] });
                    data.Add(new { cname = "qbasef_a_area_new", data = __data[40] });
                    data.Add(new { cname = "qf_tot_a_area_new", data = __data[41] });
                }
            }

            items.Add("Saving_Report.htm");

            s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
            s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
            System.Text.Json.JsonSerializer.Serialize(__data[16].ToArray());

            string s3 = "", s4;

            i = -1;

            double Max_outg = 0; double Max_f = 0, max = 0;
            string[][] AA; double[] Value1 = new double[12], Value2 = new double[12];

            runScript("init(" + s + "," + s2 + ")");

           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            webView21.CoreWebView2.ShowPrintUI();
        }
    }
}