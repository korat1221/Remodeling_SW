using Microsoft.Office.Interop.Excel;
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


            string charts = "";

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

                __data[6].Add(new { idx = i, val = Convert.ToDouble(Quse_elec_a[0]).ToString("0.0") }); //연간 에너지사용량
                __data[7].Add(new { idx = i, val = Convert.ToDouble(Quse_elec_a[1]).ToString("0.0") });
                __data[8].Add(new { idx = i, val = Convert.ToDouble(Quse_elec_a[2]).ToString("0.0") });
                __data[9].Add(new { idx = i, val = Convert.ToDouble(Quse_elec_a[3]).ToString("0.0") });

                __data[28].Add(new { idx = i, val = (Quse_elec_a[0] / Area).ToString("0.0") }); //바닥면적당 연간 에너지사용량
                __data[29].Add(new { idx = i, val = (Quse_elec_a[1] / Area).ToString("0.0") });
                __data[30].Add(new { idx = i, val = (Quse_elec_a[2] / Area).ToString("0.0") });
                __data[31].Add(new { idx = i, val = (Quse_elec_a[3] / Area).ToString("0.0") });

                for (int mth = 0; mth < 12; mth++)
                {
                    __data[10].Add(new { idx = i * 12 + mth, val = Quse_elec_mth[0, mth].ToString("0.0") }); //월별 에너지사용량 
                    __data[11].Add(new { idx = i * 12 + mth, val = Quse_elec_mth[1, mth].ToString("0.0") });
                    __data[12].Add(new { idx = i * 12 + mth, val = Quse_elec_mth[2, mth].ToString("0.0") });
                    __data[13].Add(new { idx = i * 12 + mth, val = Quse_elec_mth[3, mth].ToString("0.0") });


                    전기사용량chart1.Add(Math.Round(Double.Parse(Quse_elec_mth[0, mth].ToString()), 3) + 0);
                    전기사용량chart2.Add(Math.Round(Double.Parse(Quse_elec_mth[1, mth].ToString()), 3) + 0);
                    전기사용량chart3.Add(Math.Round(Double.Parse(Quse_elec_mth[2, mth].ToString()), 3) + 0);
                    전기사용량chart4.Add(Math.Round(Double.Parse(Quse_elec_mth[3, mth].ToString()), 3) + 0);
                    전기사용량chart.Add(Math.Round(Double.Parse(Quse_elec_mth[3, mth].ToString()), 3) + 0);
                }
                double Qh_a_전기 = 0, Qc_a_전기 = 0, Qw_a_전기 = 0, Ql_a_전기 = 0, Qv_a_전기 = 0, Qbase_a_전기 = 0, Qreg_a_전기 =0, Qtot_a_전기 = 0;
                double[] Qreg_mth = new double[12];
                double[] Qtot_mth_전기 = new double[12];
                double Error_mth_avg_전기 = 0;
                for (int mth = 0; mth < 12; mth++)
                {
                    string[][] Final = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,기저에너지", "연료='전기' and 월 ='" + (mth + 1).ToString() + "월'");
                    __data[14].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][0]).ToString("0.0") }); //월별 난방 
                    __data[15].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][1]).ToString("0.0") }); //월별 냉방 
                    __data[16].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][2]).ToString("0.0") }); //월별 급탕 
                    __data[17].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][3]).ToString("0.0") }); //월별 조명 
                    __data[18].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][4]).ToString("0.0") }); //월별 공조
                    __data[19].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][5]).ToString("0.0") }); //월별 기저 
                    난방전기소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][0])), 3) + 0);
                    냉방전기소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][1])), 3) + 0);
                    급탕전기소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][2])), 3) + 0);
                    조명전기소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][3])), 3) + 0);
                    공조전기소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][4])), 3) + 0);
                    기저전기소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][5])), 3) + 0);

                    string[][] PV = Program.DB.getValue(DB.type.ProjDB, "PV_Result", "최종사용량", "월 ='" + (mth + 1).ToString() + "월'");
                    if(PV.Length > 0)
                    {
                        for(int a = 0; a < PV.Length;  a++)
                        {
                            Qreg_mth[mth] += Convert.ToDouble(PV[a][0]);
                        }
                    }
                    __data[101].Add(new { idx = i * 12 + mth, val = Qreg_mth[mth].ToString("0.0") }); //월별 신재생 
                    Qtot_mth_전기[mth] = Convert.ToDouble(Final[0][0]) + Convert.ToDouble(Final[0][1]) + Convert.ToDouble(Final[0][2]) + Convert.ToDouble(Final[0][3]) + Convert.ToDouble(Final[0][4]) + Convert.ToDouble(Final[0][5]) - Qreg_mth[mth];
                    __data[20].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Qtot_mth_전기[mth].ToString()) }); //월별 전기 에너지소요량 
                    총전기소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Qtot_mth_전기[mth].ToString())), 3) + 0);
                    전기소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Qtot_mth_전기[mth].ToString())), 3) + 0);

                    __data[39].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(((Qtot_mth_전기[mth] - Quse_elec_mth[3, mth]) / Quse_elec_mth[3, mth] * 100).ToString("0.0")) }); //오차율
                    Error_mth_avg_전기 += Math.Abs((Qtot_mth_전기[mth] - Quse_elec_mth[3, mth]) / Quse_elec_mth[3, mth] * 100);
                    전기오차율chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Math.Abs(((Qtot_mth_전기[mth] - Quse_elec_mth[3, mth]) / Quse_elec_mth[3, mth])).ToString())), 3) + 0);  /// >>> 백분율 단위로 표시 필요 

                    Qh_a_전기 += Convert.ToDouble(Final[0][0]);
                    Qc_a_전기 += Convert.ToDouble(Final[0][1]);
                    Qw_a_전기 += Convert.ToDouble(Final[0][2]);
                    Ql_a_전기 += Convert.ToDouble(Final[0][3]);
                    Qv_a_전기 += Convert.ToDouble(Final[0][4]);
                    Qbase_a_전기 += Convert.ToDouble(Final[0][5]);
                    Qreg_a_전기 += Qreg_mth[mth];
                }
                Qtot_a_전기 = Qh_a_전기 + Qc_a_전기 + Qw_a_전기 + Ql_a_전기 + Qv_a_전기 + Qbase_a_전기 - Qreg_a_전기;

                __data[21].Add(new { idx = i, val = Qh_a_전기.ToString("0.0") });
                __data[22].Add(new { idx = i, val = Qc_a_전기.ToString("0.0") });
                __data[23].Add(new { idx = i, val = Qw_a_전기.ToString("0.0") });
                __data[24].Add(new { idx = i, val = Ql_a_전기.ToString("0.0") });
                __data[25].Add(new { idx = i, val = Qv_a_전기.ToString("0.0") });
                __data[26].Add(new { idx = i, val = Qbase_a_전기.ToString("0.0") });
                __data[27].Add(new { idx = i, val = Qtot_a_전기.ToString("0.0") });
                __data[102].Add(new { idx = i, val = Qreg_a_전기.ToString("0.0") });


                __data[32].Add(new { idx = i, val = (Qh_a_전기 / Area).ToString("0.0") });
                __data[33].Add(new { idx = i, val = (Qc_a_전기 / Area).ToString("0.0") });
                __data[34].Add(new { idx = i, val = (Qw_a_전기 / Area).ToString("0.0") });
                __data[35].Add(new { idx = i, val = (Ql_a_전기 / Area).ToString("0.0") });
                __data[36].Add(new { idx = i, val = (Qv_a_전기 / Area).ToString("0.0") });
                __data[37].Add(new { idx = i, val = (Qbase_a_전기 / Area).ToString("0.0") });
                __data[38].Add(new { idx = i, val = (Qtot_a_전기 / Area).ToString("0.0") });
                __data[103].Add(new { idx = i, val = (Qreg_a_전기 / Area).ToString("0.0") });

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

                data.Add(new { cname = "qreg_mth", data = __data[101] }); ;
                data.Add(new { cname = "qreg_a", data = __data[102] });
                data.Add(new { cname = "qreg_a_area", data = __data[103] });
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
                
                string[][] Value_사용시작일_가스 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "사용시작일", "연료='가스' AND 단위 ='kWh'");
                int yearnum_가스 = 0;
                if (Value_사용시작일_가스.Length > 0)
                {
                    if (Convert.ToDouble(Value_사용시작일_가스[0][0]) > 1)
                    {
                        string[][] Gas1, Gas2;
                        for (int mth = 0; mth < 11; mth++)
                        {
                            Gas1 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (mth + 1).ToString() + "월' AND 연료='가스' AND 단위 ='kWh'");
                            Gas2 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (mth + 2).ToString() + "월' AND 연료='가스'AND 단위 ='kWh'");
                            for (int k = 0; k < Gas1.Length; k++) //연도별
                            {
                                Quse_gas_mth[k, mth] = (Convert.ToDouble(Gas1[k][0]) * Convert.ToDouble(Value_사용시작일_가스[0][0]) / 30 + Convert.ToDouble(Gas2[k][0]) * (30 - Convert.ToDouble(Value_사용시작일_가스[0][0])) / 30);
                            }
                            yearnum = Gas1.Length;
                        }

                        Gas1 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (12).ToString() + "월' AND 연료='가스'AND 단위 ='kWh'");
                        Gas2 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (1).ToString() + "월' AND 연료='가스'AND 단위 ='kWh'");
                        for (int k = 0; k < Gas1.Length; i++) //연도별
                        {
                            Quse_gas_mth[k, 12] = (Convert.ToDouble(Gas1[k][0]) * Convert.ToDouble(Value_사용시작일_가스[0][0]) / 30 + Convert.ToDouble(Gas2[k][0]) * (30 - Convert.ToDouble(Value_사용시작일_가스[0][0])) / 30);
                        }

                    }
                    else
                    {
                        for (int mth = 0; mth < 12; mth++)
                        {
                            string[][] Gas = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (mth + 1).ToString() + "월' AND 연료='가스'AND 단위 ='kWh'");
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

                __data[56].Add(new { idx = i, val = Quse_gas_a[0].ToString("0.0") }); //연간 에너지사용량
                __data[57].Add(new { idx = i, val = Quse_gas_a[1].ToString("0.0") });
                __data[58].Add(new { idx = i, val = Quse_gas_a[2].ToString("0.0") });
                __data[59].Add(new { idx = i, val = Quse_gas_a[3].ToString("0.0") });

                __data[78].Add(new { idx = i, val = (Quse_gas_a[0] / Area).ToString("0.0") }); //바닥면적당 연간 에너지사용량
                __data[79].Add(new { idx = i, val = (Quse_gas_a[1] / Area).ToString("0.0") });
                __data[80].Add(new { idx = i, val = (Quse_gas_a[2] / Area).ToString("0.0") });
                __data[81].Add(new { idx = i, val = (Quse_gas_a[3] / Area).ToString("0.0") });

                for (int mth = 0; mth < 12; mth++)
                {
                    __data[60].Add(new { idx = i * 12 + mth, val = Quse_gas_mth[0, mth].ToString("0.0") }); //월별 에너지사용량 
                    __data[61].Add(new { idx = i * 12 + mth, val = Quse_gas_mth[1, mth].ToString("0.0") });
                    __data[62].Add(new { idx = i * 12 + mth, val = Quse_gas_mth[2, mth].ToString("0.0") });
                    __data[63].Add(new { idx = i * 12 + mth, val = Quse_gas_mth[3, mth].ToString("0.0") });


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
                    __data[64].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][0]).ToString("0.0") }); //월별 난방 
                    __data[65].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][1]).ToString("0.0") }); //월별 냉방 
                    __data[66].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][2]).ToString("0.0") }); //월별 급탕 
                    __data[67].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][3]).ToString("0.0") }); //월별 조명 
                    __data[68].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][4]).ToString("0.0") }); //월별 공조
                    __data[69].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][5]).ToString("0.0") }); //월별 기저 
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

                    __data[89].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(((Qtot_mth_가스[mth] - Quse_gas_mth[3, mth]) / Quse_gas_mth[3, mth] * 100).ToString("0.0")) }); //오차율
                    Error_mth_avg_가스 += Math.Abs((Qtot_mth_가스[mth] - Quse_gas_mth[3, mth]) / Quse_gas_mth[3, mth] * 100);
                    가스오차율chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Math.Abs((Qtot_mth_가스[mth] - Quse_gas_mth[3, mth]) / Quse_gas_mth[3, mth]).ToString())), 3) + 0);  /// >>> 백분율 단위로 표시 필요 

                    Qh_a_가스 += Convert.ToDouble(Final[0][0]);
                    Qc_a_가스 += Convert.ToDouble(Final[0][1]);
                    Qw_a_가스 += Convert.ToDouble(Final[0][2]);
                    Ql_a_가스 += Convert.ToDouble(Final[0][3]);
                    Qv_a_가스 += Convert.ToDouble(Final[0][4]);
                    Qbase_a_가스 += Convert.ToDouble(Final[0][5]);

                }
                Qtot_a_가스 = Qh_a_가스 + Qc_a_가스 + Qw_a_가스 + Ql_a_가스 + Qv_a_가스 + Qbase_a_가스;

                __data[71].Add(new { idx = i, val = Qh_a_가스.ToString("0.0") });
                __data[72].Add(new { idx = i, val = Qc_a_가스.ToString("0.0") });
                __data[73].Add(new { idx = i, val = Qw_a_가스.ToString("0.0") });
                __data[74].Add(new { idx = i, val = Ql_a_가스.ToString("0.0") });
                __data[75].Add(new { idx = i, val = Qv_a_가스.ToString("0.0") });
                __data[76].Add(new { idx = i, val = Qbase_a_가스.ToString("0.0") });
                __data[77].Add(new { idx = i, val = Qtot_a_가스.ToString("0.0") });


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
                data.Add(new { cname = "yeartitle1_gas", data = __data[50] });
                data.Add(new { cname = "yeartitle2_gas", data = __data[51] });
                data.Add(new { cname = "yeartitle3_gas", data = __data[52] });

                data.Add(new { cname = "yeartitle4_gas", data = __data[53] });
                data.Add(new { cname = "yeartitle5_gas", data = __data[54] });
                data.Add(new { cname = "yeartitle6_gas", data = __data[55] });

                data.Add(new { cname = "quse_gas_a1", data = __data[56] });
                data.Add(new { cname = "quse_gas_a2", data = __data[57] });
                data.Add(new { cname = "quse_gas_a3", data = __data[58] });
                data.Add(new { cname = "quse_gas_a4", data = __data[59] });

                data.Add(new { cname = "quse_gas_mth1", data = __data[60] });
                data.Add(new { cname = "quse_gas_mth2", data = __data[61] });
                data.Add(new { cname = "quse_gas_mth3", data = __data[62] });
                data.Add(new { cname = "quse_gas_mth4", data = __data[63] });

                data.Add(new { cname = "qhf_mth_gas", data = __data[64] });
                data.Add(new { cname = "qcf_mth_gas", data = __data[65] });
                data.Add(new { cname = "qwf_mth_gas", data = __data[66] });
                data.Add(new { cname = "qlf_mth_gas", data = __data[67] });
                data.Add(new { cname = "qvf_mth_gas", data = __data[68] });
                data.Add(new { cname = "qbasef_mth_gas", data = __data[69] });
                data.Add(new { cname = "qf_tot_mth_gas", data = __data[70] });

                data.Add(new { cname = "qhf_a_gas", data = __data[71] });
                data.Add(new { cname = "qcf_a_gas", data = __data[72] });
                data.Add(new { cname = "qwf_a_gas", data = __data[73] });
                data.Add(new { cname = "qlf_a_gas", data = __data[74] });
                data.Add(new { cname = "qvf_a_gas", data = __data[75] });
                data.Add(new { cname = "qbasef_a_gas", data = __data[76] });
                data.Add(new { cname = "qf_tot_a_gas", data = __data[77] });

                data.Add(new { cname = "quse_gas_a1_area", data = __data[78] });
                data.Add(new { cname = "quse_gas_a2_area", data = __data[79] });
                data.Add(new { cname = "quse_gas_a3_area", data = __data[80] });
                data.Add(new { cname = "quse_gas_a4_area", data = __data[81] });

                data.Add(new { cname = "qhf_a_area_gas", data = __data[82] });
                data.Add(new { cname = "qcf_a_area_gas", data = __data[83] });
                data.Add(new { cname = "qwf_a_area_gas", data = __data[84] });
                data.Add(new { cname = "qlf_a_area_gas", data = __data[85] });
                data.Add(new { cname = "qvf_a_area_gas", data = __data[86] });
                data.Add(new { cname = "qbasef_a_area_gas", data = __data[87] });
                data.Add(new { cname = "qf_tot_a_area_gas", data = __data[88] });

                data.Add(new { cname = "error_mth_gas", data = __data[89] });
                data.Add(new { cname = "error_mth_avg_gas", data = __data[90] });
                data.Add(new { cname = "error_a_gas", data = __data[91] });
                #endregion
            }

            items.Add("Error_Report.htm");
            s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
            s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
            System.Text.Json.JsonSerializer.Serialize(__data[10].ToArray());

            string s4;
            i = -1;
            double[] max_elec = new double[4]; double max_graph_elec =0;
            double[] max_gas = new double[4]; double max_graph_gas = 0;

            for (int k = 0; k < 4; k++)
            {
                for (int mth = 0; mth < 12; mth++)
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

            for (int k = 0; k < 4; k++)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    if (max_gas[k] < Quse_gas_mth[k, mth])
                    {
                        max_gas[k] = Quse_gas_mth[k, mth];
                    }
                }
            }
            for (int k = 0; k < 4; k++)
            {
                if (max_graph_gas < max_gas[k])
                {
                    max_graph_gas = max_gas[k];
                }
            }
            Debug.Print("start");

            i = -1;
            while (++i < 번호.Length)
            {
                if (charts != "") charts += ",";

                charts += "{data:[{type:\"line\",label:\"" + year_elec[0] + "\",data:" + chart_전기사용량1[i] + ",borderColor:\"#5B9BD5\",backgroundColor:\"#5B9BD5\",dash:false, tension: 0.4}," +
                "{type:\"line\",label:\"" + year_elec[1] + "\",data:" + chart_전기사용량2[i] + ",borderColor:\"#70AD47\",backgroundColor:\"#70AD47\",dash:false, tension: 0.4}," +
                "{type:\"line\",label:\"" + year_elec[2] + "\",data:" + chart_전기사용량3[i] + ",borderColor:\"#4472C4\",backgroundColor:\"#4472C4\",dash:false, tension: 0.4}," +
                "{type:\"line\",label:\"평균\",data:" + chart_전기사용량4[i] + ",borderColor:\"#ED7D31\",backgroundColor:\"#ED7D31\",dash:false, tension: 0.4}," +
                "],max:" + (Math.Round(max_graph_elec / 1000) * 1000 + 500).ToString() + ",step:100,legend:true}";

                charts += ",{data:[" +
                "{type:\"bar\",barPercentage:0.4,label:\"기저 전기 에너지 소요량 [kWh]\",data:" + chart_기저전기소요량[i] + ",borderColor:\"#BFBFBF\",backgroundColor:\"#BFBFBF\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"급탕 전기 에너지 소요량 [kWh]\",data:" + chart_급탕전기소요량[i] + ",borderColor:\"#A9D18E\",backgroundColor:\"#A9D18E\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"공조 전기 에너지 소요량 [kWh]\",data:" + chart_공조전기소요량[i] + ",borderColor:\"#70AD47\",backgroundColor:\"#70AD47\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"조명 전기 에너지 소요량 [kWh]\",data:" + chart_조명전기소요량[i] + ",borderColor:\"#FFD966\",backgroundColor:\"#FFD966\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"난방 전기 에너지 소요량 [kWh]\",data:" + chart_난방전기소요량[i] + ",borderColor:\"#F4B183\",backgroundColor:\"#F4B183\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"냉방 전기 에너지 소요량 [kWh]\",data:" + chart_냉방전기소요량[i] + ",borderColor:\"#9DC3E6\",backgroundColor:\"#9DC3E6\",dash:false}," +
                "],max:" + (Math.Round(max_graph_elec / 1000) * 1000 + 500).ToString() + ",step:100,legend:true,stacked:true}";

                charts += ",{data:[{type:\"line\",yAxisID: 'y',label:\"전기 에너지 사용량 [kWh]\",data:" + chart_전기사용량[i] + ",borderColor:\"#5B9BD5\",backgroundColor:\"#5B9BD5\",dash:false, tension: 0.4}," +
                "{type:\"line\",yAxisID: 'y',label:\"전기 에너지 소요량 [kWh]\",data:" + chart_전기소요량[i] + ",borderColor:\"#ED7D31\",backgroundColor:\"#ED7D31\",dash:false, tension: 0.4}," +
                "{type:\"bar\",yAxisID: 'y1',barPercentage:0.4,label:\"오차율 [%]\",data:" + chart_전기오차율[i] + ",borderColor:\"#A5A5A5\",backgroundColor:\"#A5A5A5\",dash:false}," +
                "],max:" + (Math.Round(max_graph_elec / 1000) * 1000 + 500).ToString() + ",step:100,legend:true}";

                charts += ",{data:[{type:\"line\",label:\"" + year_gas[0] + "\",data:" + chart_가스사용량1[i] + ",borderColor:\"#5B9BD5\",backgroundColor:\"#5B9BD5\",dash:false, tension: 0.4}," +
               "{type:\"line\",label:\"" + year_gas[1] + "\",data:" + chart_가스사용량2[i] + ",borderColor:\"#70AD47\",backgroundColor:\"#70AD47\",dash:false, tension: 0.4}," +
               "{type:\"line\",label:\"" + year_gas[2] + "\",data:" + chart_가스사용량3[i] + ",borderColor:\"#4472C4\",backgroundColor:\"#4472C4\",dash:false, tension: 0.4}," +
               "{type:\"line\",label:\"평균\",data:" + chart_가스사용량4[i] + ",borderColor:\"#ED7D31\",backgroundColor:\"#ED7D31\",dash:false, tension: 0.4}," +
               "],max:" + (Math.Round(max_graph_gas / 1000) * 1000 + 500).ToString() + ",step:100,legend:true}";

                charts += ",{data:[" +
                "{type:\"bar\",barPercentage:0.4,label:\"기저 가스 에너지 소요량 [kWh]\",data:" + chart_기저가스소요량[i] + ",borderColor:\"#BFBFBF\",backgroundColor:\"#BFBFBF\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"급탕 가스 에너지 소요량 [kWh]\",data:" + chart_급탕가스소요량[i] + ",borderColor:\"#A9D18E\",backgroundColor:\"#A9D18E\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"공조 가스 에너지 소요량 [kWh]\",data:" + chart_공조가스소요량[i] + ",borderColor:\"#70AD47\",backgroundColor:\"#70AD47\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"난방 가스 에너지 소요량 [kWh]\",data:" + chart_난방가스소요량[i] + ",borderColor:\"#F4B183\",backgroundColor:\"#F4B183\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"냉방 가스 에너지 소요량 [kWh]\",data:" + chart_냉방가스소요량[i] + ",borderColor:\"#9DC3E6\",backgroundColor:\"#9DC3E6\",dash:false}," +
                "],max:" + (Math.Round(max_graph_gas / 1000) * 1000 + 500).ToString() + ",step:100,legend:true,stacked:true}";

                charts += ",{data:[{type:\"line\",yAxisID: 'y',label:\"가스 에너지 사용량 [kWh]\",data:" + chart_가스사용량[i] + ",borderColor:\"#5B9BD5\",backgroundColor:\"#5B9BD5\",dash:false, tension: 0.4}," +
                "{type:\"line\",yAxisID: 'y',label:\"가스 에너지 소요량 [kWh]\",data:" + chart_가스소요량[i] + ",borderColor:\"#ED7D31\",backgroundColor:\"#ED7D31\",dash:false, tension: 0.4}," +
                "{type:\"bar\",yAxisID: 'y1',barPercentage:0.4,label:\"오차율 [%]\",data:" + chart_가스오차율[i] + ",borderColor:\"#A5A5A5\",backgroundColor:\"#A5A5A5\",dash:false}," +
                "],max:" + (Math.Round(max_graph_gas / 1000) * 1000 + 500).ToString() + ",step:100,legend:true}";
            }

            runScript("init(" + s + "," + s2 + "," + "[" + charts + "])");
        }

        private void Saving_Report()
        {
            string s, s2;
            string[][] 번호 = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");

            List<object> items = new List<object>();
            List<object> items2 = new List<object>();
            List<object> data = new List<object>();

            List<object>[] __data = new List<object>[700];

            List<string> chart_전_난방_전기 = new List<string>();
            List<string> chart_전_냉방_전기 = new List<string>();
            List<string> chart_전_급탕_전기 = new List<string>();
            List<string> chart_전_조명_전기 = new List<string>();
            List<string> chart_전_공조_전기 = new List<string>();
            List<string> chart_전_기저_전기 = new List<string>();
            List<string> chart_전_총_전기 = new List<string>();

            List<string> chart_후_난방_전기 = new List<string>();
            List<string> chart_후_냉방_전기 = new List<string>();
            List<string> chart_후_급탕_전기 = new List<string>();
            List<string> chart_후_조명_전기 = new List<string>();
            List<string> chart_후_공조_전기 = new List<string>();
            List<string> chart_후_기저_전기 = new List<string>();
            List<string> chart_후_총_전기 = new List<string>();

            List<string> chart_전_전기 = new List<string>();
            List<string> chart_후_전기 = new List<string>();
            List<string> chart_절감률_전기 = new List<string>();

            List<string> chart_전_난방_가스 = new List<string>();
            List<string> chart_전_냉방_가스 = new List<string>();
            List<string> chart_전_급탕_가스 = new List<string>();
            List<string> chart_전_조명_가스 = new List<string>();
            List<string> chart_전_공조_가스 = new List<string>();
            List<string> chart_전_기저_가스 = new List<string>();
            List<string> chart_전_총_가스 = new List<string>();

            List<string> chart_후_난방_가스 = new List<string>();
            List<string> chart_후_냉방_가스 = new List<string>();
            List<string> chart_후_급탕_가스 = new List<string>();
            List<string> chart_후_조명_가스 = new List<string>();
            List<string> chart_후_공조_가스 = new List<string>();
            List<string> chart_후_기저_가스 = new List<string>();
            List<string> chart_후_총_가스 = new List<string>();

            List<string> chart_전_가스 = new List<string>();
            List<string> chart_후_가스 = new List<string>();
            List<string> chart_절감률_가스 = new List<string>();

            int i = -1, n;
            while (++i < 700)
            {
                __data[i] = new List<object>();
            }

            string charts = "";
            i = -1;

            double[] Qtot2_mth_전기 = new double[12];
            double[] Qtot_mth_전기 = new double[12];
            double[] Qtot2_mth_가스 = new double[12];
            double[] Qtot_mth_가스 = new double[12];

            while (++i < 번호.Length)
            {

                string[][] res = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기존프로젝트");

                if (res.Length > 0)
                {
                    #region 전기
                    List<object> 전_난방_전기Chart = new List<object>();
                    List<object> 전_냉방_전기Chart = new List<object>();
                    List<object> 전_급탕_전기Chart = new List<object>();
                    List<object> 전_조명_전기Chart = new List<object>();
                    List<object> 전_공조_전기Chart = new List<object>();
                    List<object> 전_기저_전기Chart = new List<object>();
                    List<object> 전_총_전기Chart = new List<object>();

                    List<object> 후_난방_전기Chart = new List<object>();
                    List<object> 후_냉방_전기Chart = new List<object>();
                    List<object> 후_급탕_전기Chart = new List<object>();
                    List<object> 후_조명_전기Chart = new List<object>();
                    List<object> 후_공조_전기Chart = new List<object>();
                    List<object> 후_기저_전기Chart = new List<object>();
                    List<object> 후_총_전기Chart = new List<object>();

                    List<object> 전_전기Chart = new List<object>();
                    List<object> 후_전기Chart = new List<object>();
                    List<object> 절감률_전기Chart = new List<object>();

                    double Qh_a2_전기 = 0, Qc_a2_전기 = 0, Qw_a2_전기 = 0, Ql_a2_전기 = 0, Qv_a2_전기 = 0, Qbase_a2_전기 = 0, Qreg_a2_전기 =0, Qtot_a2_전기 = 0;
                    double[] Qreg2 = new double[12]; double[] Qreg1 = new double[12];
                    for (int mth = 0; mth < 12; mth++)
                    { //리모델링전 전기 소요량 
                        string[][] Final = Program.DB.querySQL(res[0][0], "SELECT 난방,냉방,급탕,조명,공조,기저에너지 FROM FinalEnergy_Result where 연료 = '전기' and 월 = '" + (mth + 1).ToString() + "월'");

                       

                        __data[0].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][0]).ToString("0.0") }); //월별 난방 
                        __data[1].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][1]).ToString("0.0") }); // 월별 냉방 
                        __data[2].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][2]).ToString("0.0") }); //월별 급탕 
                        __data[3].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][3]).ToString("0.0") }); //월별 조명 
                        __data[4].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][4]).ToString("0.0") }); //월별 공조
                        __data[5].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][5]).ToString("0.0") }); //월별 기저
                                                                                                                        //
                        string[][] PV = Program.DB.querySQL(res[0][0], "SELECT 최종사용량 FROM PV_Result where 월 = '" + (mth + 1).ToString() + "월'");
                        if(PV.Length > 0 )
                        {
                            for(int a= 0; a<PV.Length; a++)
                            {
                                if (PV[a][0] == "")
                                { Qreg2[mth] += 0; }
                                else { Qreg2[mth] += Convert.ToDouble(PV[a][0]); }
                            }
                        }
                        __data[100].Add(new { idx = i * 12 + mth, val = Qreg2[mth].ToString("0.0") }); //월별 신재생 생산량 
                        Qtot2_mth_전기[mth] = Convert.ToDouble(Final[0][0]) + Convert.ToDouble(Final[0][1]) + Convert.ToDouble(Final[0][2]) + Convert.ToDouble(Final[0][3]) + Convert.ToDouble(Final[0][4]) + Convert.ToDouble(Final[0][5]) - Qreg2[mth];
                        __data[6].Add(new { idx = i * 12 + mth, val = Qtot2_mth_전기[mth].ToString("0.0") }); //월별 전기 에너지소요량 

                        전_난방_전기Chart.Add(Math.Round(Double.Parse(Final[0][0]), 3) + 0);
                        전_냉방_전기Chart.Add(Math.Round(Double.Parse(Final[0][1]), 3) + 0);
                        전_급탕_전기Chart.Add(Math.Round(Double.Parse(Final[0][2]), 3) + 0);
                        전_조명_전기Chart.Add(Math.Round(Double.Parse(Final[0][3]), 3) + 0);
                        전_공조_전기Chart.Add(Math.Round(Double.Parse(Final[0][4]), 3) + 0);
                        전_기저_전기Chart.Add(Math.Round(Double.Parse(Final[0][5]), 3) + 0);
                        전_총_전기Chart.Add(Math.Round(Double.Parse(Qtot2_mth_전기[mth].ToString()), 3) + 0);
                        전_전기Chart.Add(Math.Round(Double.Parse(Qtot2_mth_전기[mth].ToString()), 3) + 0);

                        Qh_a2_전기 += Convert.ToDouble(Final[0][0]);
                        Qc_a2_전기 += Convert.ToDouble(Final[0][1]);
                        Qw_a2_전기 += Convert.ToDouble(Final[0][2]);
                        Ql_a2_전기 += Convert.ToDouble(Final[0][3]);
                        Qv_a2_전기 += Convert.ToDouble(Final[0][4]);
                        Qbase_a2_전기 += Convert.ToDouble(Final[0][5]);
                        Qreg_a2_전기 += Qreg2[mth];
                    }
                    Qtot_a2_전기 = Qh_a2_전기 + Qc_a2_전기 + Qw_a2_전기 + Ql_a2_전기 + Qv_a2_전기 + Qbase_a2_전기 - Qreg_a2_전기;
                    __data[7].Add(new { idx = i, val = Qh_a2_전기.ToString("0.0") });
                    __data[8].Add(new { idx = i, val = Qc_a2_전기.ToString("0.0") });
                    __data[9].Add(new { idx = i, val = Qw_a2_전기.ToString("0.0") });
                    __data[10].Add(new { idx = i, val = Ql_a2_전기.ToString("0.0") });
                    __data[11].Add(new { idx = i, val = Qv_a2_전기.ToString("0.0") });
                    __data[12].Add(new { idx = i, val = Qbase_a2_전기.ToString("0.0") });
                    __data[101].Add(new { idx = i, val = Qreg_a2_전기.ToString("0.0") });
                    __data[13].Add(new { idx = i, val = Qtot_a2_전기.ToString("0.0") });

                    double Area2_전기 = 0;
                    string[][] A2 = Program.DB.querySQL(res[0][0], "Select 순바닥면적 From ZoneGeneral_Form where 냉난방유무 <> '비냉난방'");
                    for (int a = 0; a < A2.Length; a++)
                    {
                        Area2_전기 += Convert.ToDouble(A2[a][0]);
                    }

                    __data[14].Add(new { idx = i, val = (Qh_a2_전기 / Area2_전기).ToString("0.0") });
                    __data[15].Add(new { idx = i, val = (Qc_a2_전기 / Area2_전기).ToString("0.0") });
                    __data[16].Add(new { idx = i, val = (Qw_a2_전기 / Area2_전기).ToString("0.0") });
                    __data[17].Add(new { idx = i, val = (Ql_a2_전기 / Area2_전기).ToString("0.0") });
                    __data[18].Add(new { idx = i, val = (Qv_a2_전기 / Area2_전기).ToString("0.0") });
                    __data[19].Add(new { idx = i, val = (Qbase_a2_전기 / Area2_전기).ToString("0.0") });
                    __data[102].Add(new { idx = i, val = (Qreg_a2_전기 / Area2_전기).ToString("0.0") });
                    __data[20].Add(new { idx = i, val = (Qtot_a2_전기 / Area2_전기).ToString("0.0") });


                    double Qh_a_전기 = 0, Qc_a_전기 = 0, Qw_a_전기 = 0, Ql_a_전기 = 0, Qv_a_전기 = 0, Qbase_a_전기 = 0, Qreg_a_전기 =0, Qtot_a_전기 = 0;
                    for (int mth = 0; mth < 12; mth++)
                    { //리모델링후 전기 소요량 
                        string[][] Final = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,기저에너지", "연료='전기' and 월 ='" + (mth + 1).ToString() + "월'");
                        string[][] Final2 = Program.DB.querySQL(res[0][0], "SELECT 난방,냉방,급탕,조명,공조,기저에너지 FROM FinalEnergy_Result where 연료 = '전기' and 월 = '" + (mth + 1).ToString() + "월'");
                        __data[21].Add(new { idx = i * 12 + mth, val =Convert.ToDouble(Final[0][0]).ToString("0.0") }); //월별 난방 
                        __data[22].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][1]).ToString("0.0") }); //월별 냉방 
                        __data[23].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][2]).ToString("0.0") }); //월별 급탕 
                        __data[24].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][3]).ToString("0.0") }); //월별 조명 
                        __data[25].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][4]).ToString("0.0") }); //월별 공조
                        __data[26].Add(new { idx = i * 12 + mth, val =Convert.ToDouble(Final2[0][5]).ToString("0.0") }); //월별 기저 >>>리모델링 전 값 가져옴 

                        string[][] PV = Program.DB.getValue(DB.type.ProjDB, "PV_Result", "최종사용량", "월 ='" + (mth + 1).ToString() + "월'");
                        if (PV.Length > 0)
                        {
                            for (int a = 0; a < PV.Length; a++)
                            {
                                if(PV[a][0] =="")
                                {
                                    Qreg1[mth] += 0; 
                                }
                                else
                                {
                                    Qreg1[mth] += Convert.ToDouble(PV[a][0]);
                                }
                            }
                        }
                        __data[103].Add(new { idx = i * 12 + mth, val = Qreg1[mth].ToString("0.0") }); //월별 신재생 생산량 
                        Qtot_mth_전기[mth] = Convert.ToDouble(Final[0][0]) + Convert.ToDouble(Final[0][1]) + Convert.ToDouble(Final[0][2]) + Convert.ToDouble(Final[0][3]) + Convert.ToDouble(Final[0][4]) + Convert.ToDouble(Final2[0][5]) - Qreg1[mth];
                        __data[27].Add(new { idx = i * 12 + mth, val = Qtot_mth_전기[mth].ToString("0.0") }); //월별 전기 에너지소요량 

                        후_난방_전기Chart.Add(Math.Round(Double.Parse(Final[0][0]), 3) + 0);
                        후_냉방_전기Chart.Add(Math.Round(Double.Parse(Final[0][1]), 3) + 0);
                        후_급탕_전기Chart.Add(Math.Round(Double.Parse(Final[0][2]), 3) + 0);
                        후_조명_전기Chart.Add(Math.Round(Double.Parse(Final[0][3]), 3) + 0);
                        후_공조_전기Chart.Add(Math.Round(Double.Parse(Final[0][4]), 3) + 0);
                        후_기저_전기Chart.Add(Math.Round(Double.Parse(Final2[0][5]), 3) + 0);
                        후_총_전기Chart.Add(Math.Round(Double.Parse(Qtot_mth_전기[mth].ToString()), 3) + 0);
                        후_전기Chart.Add(Math.Round(Double.Parse(Qtot_mth_전기[mth].ToString()), 3) + 0);

                        Qh_a_전기 += Convert.ToDouble(Final[0][0]);
                        Qc_a_전기 += Convert.ToDouble(Final[0][1]);
                        Qw_a_전기 += Convert.ToDouble(Final[0][2]);
                        Ql_a_전기 += Convert.ToDouble(Final[0][3]);
                        Qv_a_전기 += Convert.ToDouble(Final[0][4]);
                        Qbase_a_전기 += Convert.ToDouble(Final2[0][5]); //리모델링전 값 가져옴 
                        Qreg_a_전기 += Qreg1[mth];
                    }
                    Qtot_a_전기 = Qh_a_전기 + Qc_a_전기 + Qw_a_전기 + Ql_a_전기 + Qv_a_전기 + Qbase_a_전기 - Qreg_a_전기;
                    __data[28].Add(new { idx = i, val = Qh_a_전기.ToString("0.0") });
                    __data[29].Add(new { idx = i, val = Qc_a_전기.ToString("0.0") });
                    __data[30].Add(new { idx = i, val = Qw_a_전기.ToString("0.0") });
                    __data[31].Add(new { idx = i, val = Ql_a_전기.ToString("0.0") });
                    __data[32].Add(new { idx = i, val = Qv_a_전기.ToString("0.0") });
                    __data[33].Add(new { idx = i, val = Qbase_a_전기.ToString("0.0") });
                    __data[104].Add(new { idx = i, val = Qreg_a_전기.ToString("0.0") });
                    __data[34].Add(new { idx = i, val = Qtot_a_전기.ToString("0.0") });

                    double Area_전기 = 0;
                    string[][] A = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "냉난방유무 <> '비냉난방'");
                    for (int a = 0; a < A.Length; a++)
                    {
                        Area_전기 += Convert.ToDouble(A[a][0]);
                    }

                    __data[35].Add(new { idx = i, val = (Qh_a_전기 / Area_전기).ToString("0.0") });
                    __data[36].Add(new { idx = i, val = (Qc_a_전기 / Area_전기).ToString("0.0") });
                    __data[37].Add(new { idx = i, val = (Qw_a_전기 / Area_전기).ToString("0.0") });
                    __data[38].Add(new { idx = i, val = (Ql_a_전기 / Area_전기).ToString("0.0") });
                    __data[39].Add(new { idx = i, val = (Qv_a_전기 / Area_전기).ToString("0.0") });
                    __data[40].Add(new { idx = i, val = (Qbase_a_전기 / Area_전기).ToString("0.0") });
                    __data[105].Add(new { idx = i, val = (Qreg_a_전기 / Area_전기).ToString("0.0") });
                    __data[41].Add(new { idx = i, val = (Qtot_a_전기 / Area_전기).ToString("0.0") });

                    double SavingPercent_mth_avg_전기 = 0; double Saving_mth_avg_전기 = 0; double Saving_a_전기 = 0;
                    for (int mth = 0; mth < 12; mth++)
                    { //전기소요량 월별 절감량
                        Saving_a_전기 += (Qtot2_mth_전기[mth] - Qtot_mth_전기[mth]);
                        Saving_mth_avg_전기 += (Qtot2_mth_전기[mth] - Qtot_mth_전기[mth]);
                        SavingPercent_mth_avg_전기 += (Qtot2_mth_전기[mth] - Qtot_mth_전기[mth]) / Qtot2_mth_전기[mth] * 100;
                    }
                    Saving_mth_avg_전기 = Saving_mth_avg_전기 / 12;
                    SavingPercent_mth_avg_전기 = SavingPercent_mth_avg_전기 / 12;

                    __data[42].Add(new { idx = i, val = (Saving_a_전기).ToString("0.0") }); //연간 절감량
                    __data[43].Add(new { idx = i, val = Saving_mth_avg_전기.ToString("0.0") }); //월평균 절감량
                    __data[44].Add(new { idx = i, val = ((Saving_a_전기 / Qtot_a2_전기) * 100).ToString("0.0") + "%" }); //연간 절감율
                    __data[45].Add(new { idx = i, val = SavingPercent_mth_avg_전기.ToString("0.0") + "%" }); //월평균 절감율 
                    for (int mth = 0; mth < 12; mth++)
                    {
                        __data[46].Add(new { idx = i * 12 + mth, val = ((Qtot2_mth_전기[mth] - Qtot_mth_전기[mth])).ToString("0.0") }); //월별 절감량 
                        절감률_전기Chart.Add(Math.Round(Double.Parse((Math.Abs(Qtot2_mth_전기[mth] - Qtot_mth_전기[mth]) / Qtot2_mth_전기[mth]).ToString()), 3) + 0);
                    }

                    chart_전_난방_전기.Add(System.Text.Json.JsonSerializer.Serialize(전_난방_전기Chart.ToArray()));
                    chart_전_냉방_전기.Add(System.Text.Json.JsonSerializer.Serialize(전_냉방_전기Chart.ToArray()));
                    chart_전_급탕_전기.Add(System.Text.Json.JsonSerializer.Serialize(전_급탕_전기Chart.ToArray()));
                    chart_전_조명_전기.Add(System.Text.Json.JsonSerializer.Serialize(전_조명_전기Chart.ToArray()));
                    chart_전_공조_전기.Add(System.Text.Json.JsonSerializer.Serialize(전_공조_전기Chart.ToArray()));
                    chart_전_기저_전기.Add(System.Text.Json.JsonSerializer.Serialize(전_기저_전기Chart.ToArray()));
                    chart_전_총_전기.Add(System.Text.Json.JsonSerializer.Serialize(전_총_전기Chart.ToArray()));


                    chart_후_난방_전기.Add(System.Text.Json.JsonSerializer.Serialize(후_난방_전기Chart.ToArray()));
                    chart_후_냉방_전기.Add(System.Text.Json.JsonSerializer.Serialize(후_냉방_전기Chart.ToArray()));
                    chart_후_급탕_전기.Add(System.Text.Json.JsonSerializer.Serialize(후_급탕_전기Chart.ToArray()));
                    chart_후_조명_전기.Add(System.Text.Json.JsonSerializer.Serialize(후_조명_전기Chart.ToArray()));
                    chart_후_공조_전기.Add(System.Text.Json.JsonSerializer.Serialize(후_공조_전기Chart.ToArray()));
                    chart_후_기저_전기.Add(System.Text.Json.JsonSerializer.Serialize(후_기저_전기Chart.ToArray()));
                    chart_후_총_전기.Add(System.Text.Json.JsonSerializer.Serialize(전_총_전기Chart.ToArray()));

                    chart_전_전기.Add(System.Text.Json.JsonSerializer.Serialize(전_전기Chart.ToArray()));
                    chart_후_전기.Add(System.Text.Json.JsonSerializer.Serialize(후_전기Chart.ToArray()));
                    chart_절감률_전기.Add(System.Text.Json.JsonSerializer.Serialize(절감률_전기Chart.ToArray()));
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

                    data.Add(new { cname = "Saving_a", data = __data[42] });
                    data.Add(new { cname = "Saving_mth_avg", data = __data[43] });
                    data.Add(new { cname = "SavingPercent_a", data = __data[44] });
                    data.Add(new { cname = "SavingPercent_mth_avg", data = __data[45] });
                    data.Add(new { cname = "Saving_mth", data = __data[46] });

                    data.Add(new { cname = "qreg_mth_old", data = __data[100] });
                    data.Add(new { cname = "qreg_a_old", data = __data[101] });
                    data.Add(new { cname = "qreg_a_area_old", data = __data[102] });
                    data.Add(new { cname = "qreg_mth_new", data = __data[103] });
                    data.Add(new { cname = "qreg_a_new", data = __data[104] });
                    data.Add(new { cname = "qreg_a_area_new", data = __data[105] });
                    #endregion

                    #region 가스
                    List<object> 전_난방_가스Chart = new List<object>();
                    List<object> 전_냉방_가스Chart = new List<object>();
                    List<object> 전_급탕_가스Chart = new List<object>();
                    List<object> 전_조명_가스Chart = new List<object>();
                    List<object> 전_공조_가스Chart = new List<object>();
                    List<object> 전_기저_가스Chart = new List<object>();
                    List<object> 전_총_가스Chart = new List<object>();

                    List<object> 후_난방_가스Chart = new List<object>();
                    List<object> 후_냉방_가스Chart = new List<object>();
                    List<object> 후_급탕_가스Chart = new List<object>();
                    List<object> 후_조명_가스Chart = new List<object>();
                    List<object> 후_공조_가스Chart = new List<object>();
                    List<object> 후_기저_가스Chart = new List<object>();
                    List<object> 후_총_가스Chart = new List<object>();

                    List<object> 전_가스Chart = new List<object>();
                    List<object> 후_가스Chart = new List<object>();
                    List<object> 절감률_가스Chart = new List<object>();
                    double Qh_a2_가스 = 0, Qc_a2_가스 = 0, Qw_a2_가스 = 0, Ql_a2_가스 = 0, Qv_a2_가스 = 0, Qbase_a2_가스 = 0, Qtot_a2_가스 = 0;
                    for (int mth = 0; mth < 12; mth++)
                    { //리모델링전 가스 소요량 
                        string[][] Final = Program.DB.querySQL(res[0][0], "SELECT 난방,냉방,급탕,조명,공조,기저에너지 FROM FinalEnergy_Result where 연료 = '가스' and 월 = '" + (mth + 1).ToString() + "월'");


                        __data[50].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][0]).ToString("0.0") }); //월별 난방 
                        __data[51].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][1]).ToString("0.0") }); //월별 냉방 
                        __data[52].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][2]).ToString("0.0") }); //월별 급탕 
                        __data[53].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][3]).ToString("0.0") }); //월별 조명 
                        __data[54].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][4]).ToString("0.0") }); //월별 공조
                        __data[55].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][5]).ToString("0.0") }); //월별 기저 
                        Qtot2_mth_가스[mth] = Convert.ToDouble(Final[0][0]) + Convert.ToDouble(Final[0][1]) + Convert.ToDouble(Final[0][2]) + Convert.ToDouble(Final[0][3]) + Convert.ToDouble(Final[0][4]) + Convert.ToDouble(Final[0][5]);
                        __data[56].Add(new { idx = i * 12 + mth, val = Qtot2_mth_가스[mth].ToString("0.0") }); //월별 가스 에너지소요량 

                        전_난방_가스Chart.Add(Math.Round(Double.Parse(Final[0][0]), 3) + 0);
                        전_냉방_가스Chart.Add(Math.Round(Double.Parse(Final[0][1]), 3) + 0);
                        전_급탕_가스Chart.Add(Math.Round(Double.Parse(Final[0][2]), 3) + 0);
                        전_조명_가스Chart.Add(Math.Round(Double.Parse(Final[0][3]), 3) + 0);
                        전_공조_가스Chart.Add(Math.Round(Double.Parse(Final[0][4]), 3) + 0);
                        전_기저_가스Chart.Add(Math.Round(Double.Parse(Final[0][5]), 3) + 0);
                        전_총_가스Chart.Add(Math.Round(Double.Parse(Qtot2_mth_가스[mth].ToString()), 3) + 0);
                        전_가스Chart.Add(Math.Round(Double.Parse(Qtot2_mth_가스[mth].ToString()), 3) + 0);


                        Qh_a2_가스 += Convert.ToDouble(Final[0][0]);
                        Qc_a2_가스 += Convert.ToDouble(Final[0][1]);
                        Qw_a2_가스 += Convert.ToDouble(Final[0][2]);
                        Ql_a2_가스 += Convert.ToDouble(Final[0][3]);
                        Qv_a2_가스 += Convert.ToDouble(Final[0][4]);
                        Qbase_a2_가스 += Convert.ToDouble(Final[0][5]);
                    }
                    Qtot_a2_가스 = Qh_a2_가스 + Qc_a2_가스 + Qw_a2_가스 + Ql_a2_가스 + Qv_a2_가스 + Qbase_a2_가스;
                    __data[57].Add(new { idx = i, val = Qh_a2_가스.ToString("0.0") });
                    __data[58].Add(new { idx = i, val = Qc_a2_가스.ToString("0.0") });
                    __data[59].Add(new { idx = i, val = Qw_a2_가스.ToString("0.0") });
                    __data[60].Add(new { idx = i, val = Ql_a2_가스.ToString("0.0") });
                    __data[61].Add(new { idx = i, val = Qv_a2_가스.ToString("0.0") });
                    __data[62].Add(new { idx = i, val = Qbase_a2_가스.ToString("0.0") });
                    __data[63].Add(new { idx = i, val = Qtot_a2_가스.ToString("0.0") });

                    double Area2_가스 = 0;
                    A2 = Program.DB.querySQL(res[0][0], "Select 순바닥면적 From ZoneGeneral_Form where 냉난방유무 <> '비냉난방'");
                    for (int a = 0; a < A2.Length; a++)
                    {
                        Area2_가스 += Convert.ToDouble(A2[a][0]);
                    }

                    __data[64].Add(new { idx = i, val = (Qh_a2_가스 / Area2_가스).ToString("0.0") });
                    __data[65].Add(new { idx = i, val = (Qc_a2_가스 / Area2_가스).ToString("0.0") });
                    __data[66].Add(new { idx = i, val = (Qw_a2_가스 / Area2_가스).ToString("0.0") });
                    __data[67].Add(new { idx = i, val = (Ql_a2_가스 / Area2_가스).ToString("0.0") });
                    __data[68].Add(new { idx = i, val = (Qv_a2_가스 / Area2_가스).ToString("0.0") });
                    __data[69].Add(new { idx = i, val = (Qbase_a2_가스 / Area2_가스).ToString("0.0") });
                    __data[70].Add(new { idx = i, val = (Qtot_a2_가스 / Area2_가스).ToString("0.0") });


                    double Qh_a_가스 = 0, Qc_a_가스 = 0, Qw_a_가스 = 0, Ql_a_가스 = 0, Qv_a_가스 = 0, Qbase_a_가스 = 0, Qtot_a_가스 = 0;
                    for (int mth = 0; mth < 12; mth++)
                    { //리모델링후 가스 소요량 
                        string[][] Final = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,기저에너지", "연료='가스' and 월 ='" + (mth + 1).ToString() + "월'");
                        string[][] Final2 = Program.DB.querySQL(res[0][0], "SELECT 난방,냉방,급탕,조명,공조,기저에너지 FROM FinalEnergy_Result where 연료 = '가스' and 월 = '" + (mth + 1).ToString() + "월'");
                        __data[71].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][0]).ToString("0.0") }); //월별 난방 
                        __data[72].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][1]).ToString("0.0") }); //월별 냉방 
                        __data[73].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][2]).ToString("0.0") }); //월별 급탕 
                        __data[74].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][3]).ToString("0.0") }); //월별 조명 
                        __data[75].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final[0][4]).ToString("0.0") }); //월별 공조
                        __data[76].Add(new { idx = i * 12 + mth, val = Convert.ToDouble(Final2[0][5]).ToString("0.0") }); //월별 기저 
                        Qtot_mth_가스[mth] = Convert.ToDouble(Final[0][0]) + Convert.ToDouble(Final[0][1]) + Convert.ToDouble(Final[0][2]) + Convert.ToDouble(Final[0][3]) + Convert.ToDouble(Final[0][4]) + Convert.ToDouble(Final2[0][5]);
                        __data[77].Add(new { idx = i * 12 + mth, val = Qtot_mth_가스[mth].ToString("0.0") }); //월별 가스 에너지소요량 

                        후_난방_가스Chart.Add(Math.Round(Double.Parse(Final[0][0]), 3) + 0);
                        후_냉방_가스Chart.Add(Math.Round(Double.Parse(Final[0][1]), 3) + 0);
                        후_급탕_가스Chart.Add(Math.Round(Double.Parse(Final[0][2]), 3) + 0);
                        후_조명_가스Chart.Add(Math.Round(Double.Parse(Final[0][3]), 3) + 0);
                        후_공조_가스Chart.Add(Math.Round(Double.Parse(Final[0][4]), 3) + 0);
                        후_기저_가스Chart.Add(Math.Round(Double.Parse(Final2[0][5]), 3) + 0);
                        후_총_가스Chart.Add(Math.Round(Double.Parse(Qtot_mth_가스[mth].ToString()), 3) + 0);
                        후_가스Chart.Add(Math.Round(Double.Parse(Qtot_mth_가스[mth].ToString()), 3) + 0);

                        Qh_a_가스 += Convert.ToDouble(Final[0][0]);
                        Qc_a_가스 += Convert.ToDouble(Final[0][1]);
                        Qw_a_가스 += Convert.ToDouble(Final[0][2]);
                        Ql_a_가스 += Convert.ToDouble(Final[0][3]);
                        Qv_a_가스 += Convert.ToDouble(Final[0][4]);
                        Qbase_a_가스 += Convert.ToDouble(Final2[0][5]);
                    }
                    Qtot_a_가스 = Qh_a_가스 + Qc_a_가스 + Qw_a_가스 + Ql_a_가스 + Qv_a_가스 + Qbase_a_가스;
                    __data[78].Add(new { idx = i, val = Qh_a_가스.ToString("0.0") });
                    __data[79].Add(new { idx = i, val = Qc_a_가스.ToString("0.0") });
                    __data[80].Add(new { idx = i, val = Qw_a_가스.ToString("0.0") });
                    __data[81].Add(new { idx = i, val = Ql_a_가스.ToString("0.0") });
                    __data[82].Add(new { idx = i, val = Qv_a_가스.ToString("0.0") });
                    __data[83].Add(new { idx = i, val = Qbase_a_가스.ToString("0.0") });
                    __data[84].Add(new { idx = i, val = Qtot_a_가스.ToString("0.0") });

                    double Area_가스 = 0;
                    A = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "냉난방유무 <> '비냉난방'");
                    for (int a = 0; a < A.Length; a++)
                    {
                        Area_가스 += Convert.ToDouble(A[a][0]);
                    }

                    __data[85].Add(new { idx = i, val = (Qh_a_가스 / Area_가스).ToString("0.0") });
                    __data[86].Add(new { idx = i, val = (Qc_a_가스 / Area_가스).ToString("0.0") });
                    __data[87].Add(new { idx = i, val = (Qw_a_가스 / Area_가스).ToString("0.0") });
                    __data[88].Add(new { idx = i, val = (Ql_a_가스 / Area_가스).ToString("0.0") });
                    __data[89].Add(new { idx = i, val = (Qv_a_가스 / Area_가스).ToString("0.0") });
                    __data[90].Add(new { idx = i, val = (Qbase_a_가스 / Area_가스).ToString("0.0") });
                    __data[91].Add(new { idx = i, val = (Qtot_a_가스 / Area_가스).ToString("0.0") });

                    double SavingPercent_mth_avg_가스 = 0; double Saving_mth_avg_가스 = 0; double Saving_a_가스 = 0;
                    for (int mth = 0; mth < 12; mth++)
                    { //가스소요량 월별 절감량
                        Saving_a_가스 += (Qtot2_mth_가스[mth] - Qtot_mth_가스[mth]);
                        Saving_mth_avg_가스 += (Qtot2_mth_가스[mth] - Qtot_mth_가스[mth]);
                        SavingPercent_mth_avg_가스 += (Qtot2_mth_가스[mth] - Qtot_mth_가스[mth]) / Qtot2_mth_가스[mth] * 100;
                    }
                    Saving_mth_avg_가스 = Saving_mth_avg_가스 / 12;
                    SavingPercent_mth_avg_가스 = SavingPercent_mth_avg_가스 / 12;

                    __data[92].Add(new { idx = i, val = (Saving_a_가스).ToString("0.0") }); //연간 절감량
                    __data[93].Add(new { idx = i, val = Saving_mth_avg_가스.ToString("0.0") }); //월평균 절감량
                    __data[94].Add(new { idx = i, val = ((Saving_a_가스 / Qtot_a2_가스) * 100).ToString("0.0") + "%" }); //연간 절감율
                    __data[95].Add(new { idx = i, val = SavingPercent_mth_avg_가스.ToString("0.0") + "%" }); //월평균 절감율 
                    for (int mth = 0; mth < 12; mth++)
                    {
                        __data[96].Add(new { idx = i * 12 + mth, val = ((Qtot2_mth_가스[mth] - Qtot_mth_가스[mth])).ToString("0.0") }); //월별 절감량 
                        절감률_가스Chart.Add(Math.Round(Double.Parse((Math.Abs(Qtot2_mth_가스[mth] - Qtot_mth_가스[mth]) / Qtot2_mth_가스[mth]).ToString()), 3) + 0);
                    }
                    chart_전_난방_가스.Add(System.Text.Json.JsonSerializer.Serialize(전_난방_가스Chart.ToArray()));
                    chart_전_냉방_가스.Add(System.Text.Json.JsonSerializer.Serialize(전_냉방_가스Chart.ToArray()));
                    chart_전_급탕_가스.Add(System.Text.Json.JsonSerializer.Serialize(전_급탕_가스Chart.ToArray()));
                    chart_전_조명_가스.Add(System.Text.Json.JsonSerializer.Serialize(전_조명_가스Chart.ToArray()));
                    chart_전_공조_가스.Add(System.Text.Json.JsonSerializer.Serialize(전_공조_가스Chart.ToArray()));
                    chart_전_기저_가스.Add(System.Text.Json.JsonSerializer.Serialize(전_기저_가스Chart.ToArray()));
                    chart_전_총_가스.Add(System.Text.Json.JsonSerializer.Serialize(전_총_가스Chart.ToArray()));


                    chart_후_난방_가스.Add(System.Text.Json.JsonSerializer.Serialize(후_난방_가스Chart.ToArray()));
                    chart_후_냉방_가스.Add(System.Text.Json.JsonSerializer.Serialize(후_냉방_가스Chart.ToArray()));
                    chart_후_급탕_가스.Add(System.Text.Json.JsonSerializer.Serialize(후_급탕_가스Chart.ToArray()));
                    chart_후_조명_가스.Add(System.Text.Json.JsonSerializer.Serialize(후_조명_가스Chart.ToArray()));
                    chart_후_공조_가스.Add(System.Text.Json.JsonSerializer.Serialize(후_공조_가스Chart.ToArray()));
                    chart_후_기저_가스.Add(System.Text.Json.JsonSerializer.Serialize(후_기저_가스Chart.ToArray()));
                    chart_후_총_가스.Add(System.Text.Json.JsonSerializer.Serialize(전_총_가스Chart.ToArray()));

                    chart_전_가스.Add(System.Text.Json.JsonSerializer.Serialize(전_가스Chart.ToArray()));
                    chart_후_가스.Add(System.Text.Json.JsonSerializer.Serialize(후_가스Chart.ToArray()));
                    chart_절감률_가스.Add(System.Text.Json.JsonSerializer.Serialize(절감률_가스Chart.ToArray()));
                    ///////////////////////////////////////////////////////////
                    data.Add(new { cname = "qhf_mth_old_gas", data = __data[50] });
                    data.Add(new { cname = "qcf_mth_old_gas", data = __data[51] });
                    data.Add(new { cname = "qwf_mth_old_gas", data = __data[52] });
                    data.Add(new { cname = "qlf_mth_old_gas", data = __data[53] });
                    data.Add(new { cname = "qvf_mth_old_gas", data = __data[54] });
                    data.Add(new { cname = "qbasef_mth_old_gas", data = __data[55] });
                    data.Add(new { cname = "qf_tot_mth_old_gas", data = __data[56] });

                    data.Add(new { cname = "qhf_a_old_gas", data = __data[57] });
                    data.Add(new { cname = "qcf_a_old_gas", data = __data[58] });
                    data.Add(new { cname = "qwf_a_old_gas", data = __data[59] });
                    data.Add(new { cname = "qlf_a_old_gas", data = __data[60] });
                    data.Add(new { cname = "qvf_a_old_gas", data = __data[61] });
                    data.Add(new { cname = "qbasef_a_old_gas", data = __data[62] });
                    data.Add(new { cname = "qf_tot_a_old_gas", data = __data[63] });

                    data.Add(new { cname = "qhf_a_area_old_gas", data = __data[64] });
                    data.Add(new { cname = "qcf_a_area_old_gas", data = __data[65] });
                    data.Add(new { cname = "qwf_a_area_old_gas", data = __data[66] });
                    data.Add(new { cname = "qlf_a_area_old_gas", data = __data[67] });
                    data.Add(new { cname = "qvf_a_area_old_gas", data = __data[68] });
                    data.Add(new { cname = "qbasef_a_area_old_gas", data = __data[69] });
                    data.Add(new { cname = "qf_tot_a_area_old_gas", data = __data[70] });

                    data.Add(new { cname = "qhf_mth_new_gas", data = __data[71] });
                    data.Add(new { cname = "qcf_mth_new_gas", data = __data[72] });
                    data.Add(new { cname = "qwf_mth_new_gas", data = __data[73] });
                    data.Add(new { cname = "qlf_mth_new_gas", data = __data[74] });
                    data.Add(new { cname = "qvf_mth_new_gas", data = __data[75] });
                    data.Add(new { cname = "qbasef_mth_new_gas", data = __data[76] });
                    data.Add(new { cname = "qf_tot_mth_new_gas", data = __data[77] });

                    data.Add(new { cname = "qhf_a_new_gas", data = __data[78] });
                    data.Add(new { cname = "qcf_a_new_gas", data = __data[79] });
                    data.Add(new { cname = "qwf_a_new_gas", data = __data[80] });
                    data.Add(new { cname = "qlf_a_new_gas", data = __data[81] });
                    data.Add(new { cname = "qvf_a_new_gas", data = __data[82] });
                    data.Add(new { cname = "qbasef_a_new_gas", data = __data[83] });
                    data.Add(new { cname = "qf_tot_a_new_gas", data = __data[84] });

                    data.Add(new { cname = "qhf_a_area_new_gas", data = __data[85] });
                    data.Add(new { cname = "qcf_a_area_new_gas", data = __data[86] });
                    data.Add(new { cname = "qwf_a_area_new_gas", data = __data[87] });
                    data.Add(new { cname = "qlf_a_area_new_gas", data = __data[88] });
                    data.Add(new { cname = "qvf_a_area_new_gas", data = __data[89] });
                    data.Add(new { cname = "qbasef_a_area_new_gas", data = __data[90] });
                    data.Add(new { cname = "qf_tot_a_area_new_gas", data = __data[91] });

                    data.Add(new { cname = "Saving_a_gas", data = __data[92] });
                    data.Add(new { cname = "Saving_mth_avg_gas", data = __data[93] });
                    data.Add(new { cname = "SavingPercent_a_gas", data = __data[94] });
                    data.Add(new { cname = "SavingPercent_mth_avg_gas", data = __data[95] });
                    data.Add(new { cname = "Saving_mth_gas", data = __data[96] });
                    #endregion
                }
            }

            items.Add("Saving_Report.htm");

            s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
            s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
            System.Text.Json.JsonSerializer.Serialize(__data[16].ToArray());

            string s3 = "", s4;

            i = -1;

            double max_graph_elec = 0;
            double max_graph_gas = 0;
 
            for (int mth = 0; mth < 12; mth++)
            {
                if (max_graph_elec < Qtot2_mth_전기[mth])
                {
                    max_graph_elec = Qtot2_mth_전기[mth];
                }
            }
            for (int mth = 0; mth < 12; mth++)
            {
                if (max_graph_gas < Qtot2_mth_가스[mth])
                {
                    max_graph_gas = Qtot2_mth_가스[mth];
                }
            }
            Debug.Print("start");

            i = -1;
            while (++i < 번호.Length)
            {
                if (charts != "") charts += ",";

                charts += "{data:[" +
                "{type:\"bar\",barPercentage:0.4,label:\"기저 에너지 소요량 [kWh]\",data:" + chart_전_기저_전기[i] + ",borderColor:\"#BFBFBF\",backgroundColor:\"#BFBFBF\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"급탕 에너지 소요량 [kWh]\",data:" + chart_전_급탕_전기[i] + ",borderColor:\"#A9D18E\",backgroundColor:\"#A9D18E\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"공조 에너지 소요량 [kWh]\",data:" + chart_전_공조_전기[i] + ",borderColor:\"#70AD47\",backgroundColor:\"#70AD47\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"조명 에너지 소요량 [kWh]\",data:" + chart_전_조명_전기[i] + ",borderColor:\"#FFD966\",backgroundColor:\"#FFD966\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"난방 에너지 소요량 [kWh]\",data:" + chart_전_난방_전기[i] + ",borderColor:\"#F4B183\",backgroundColor:\"#F4B183\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"냉방 에너지 소요량 [kWh]\",data:" + chart_전_냉방_전기[i] + ",borderColor:\"#9DC3E6\",backgroundColor:\"#9DC3E6\",dash:false}," +
                "],max:" + (Math.Round(max_graph_elec / 1000) * 1000 + 500).ToString() + ",step:100,legend:true,stacked:true}";

                charts += ",{data:[" +
                "{type:\"bar\",barPercentage:0.4,label:\"기저 에너지 소요량 [kWh]\",data:" + chart_후_기저_전기[i] + ",borderColor:\"#BFBFBF\",backgroundColor:\"#BFBFBF\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"급탕 에너지 소요량 [kWh]\",data:" + chart_후_급탕_전기[i] + ",borderColor:\"#A9D18E\",backgroundColor:\"#A9D18E\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"공조 에너지 소요량 [kWh]\",data:" + chart_후_공조_전기[i] + ",borderColor:\"#70AD47\",backgroundColor:\"#70AD47\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"조명 에너지 소요량 [kWh]\",data:" + chart_후_조명_전기[i] + ",borderColor:\"#FFD966\",backgroundColor:\"#FFD966\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"난방 에너지 소요량 [kWh]\",data:" + chart_후_난방_전기[i] + ",borderColor:\"#F4B183\",backgroundColor:\"#F4B183\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"냉방 에너지 소요량 [kWh]\",data:" + chart_후_냉방_전기[i] + ",borderColor:\"#9DC3E6\",backgroundColor:\"#9DC3E6\",dash:false}," +
                "],max:" + (Math.Round(max_graph_elec / 1000) * 1000 + 500).ToString() + ",step:100,legend:true,stacked:true}";

                charts += ",{data:[{type:\"line\",yAxisID: 'y',label:\"리모델링 전 소요량 [kWh]\",data:" + chart_전_전기[i] + ",borderColor:\"#5B9BD5\",backgroundColor:\"#5B9BD5\",dash:false, tension: 0.4}," +
                "{type:\"line\",yAxisID: 'y',label:\"리모델링 후 소요량 [kWh]\",data:" + chart_후_전기[i] + ",borderColor:\"#ED7D31\",backgroundColor:\"#ED7D31\",dash:false, tension: 0.4}," +
                "{type:\"bar\",yAxisID: 'y1',barPercentage:0.4,label:\"절감률 [%]\",data:" + chart_절감률_전기[i] + ",borderColor:\"#A5A5A5\",backgroundColor:\"#A5A5A5\",dash:false}," +
                "],max:" + (Math.Round(max_graph_elec / 1000) * 1000 + 500).ToString() + ",step:100,legend:true}";

                charts += ",{data:[" +
               "{type:\"bar\",barPercentage:0.4,label:\"기저 에너지 소요량 [kWh]\",data:" + chart_전_기저_가스[i] + ",borderColor:\"#BFBFBF\",backgroundColor:\"#BFBFBF\",dash:false}," +
               "{type:\"bar\",barPercentage:0.4,label:\"급탕 에너지 소요량 [kWh]\",data:" + chart_전_급탕_가스[i] + ",borderColor:\"#A9D18E\",backgroundColor:\"#A9D18E\",dash:false}," +
               "{type:\"bar\",barPercentage:0.4,label:\"공조 에너지 소요량 [kWh]\",data:" + chart_전_공조_가스[i] + ",borderColor:\"#70AD47\",backgroundColor:\"#70AD47\",dash:false}," +
               "{type:\"bar\",barPercentage:0.4,label:\"조명 에너지 소요량 [kWh]\",data:" + chart_전_조명_가스[i] + ",borderColor:\"#FFD966\",backgroundColor:\"#FFD966\",dash:false}," +
               "{type:\"bar\",barPercentage:0.4,label:\"난방 에너지 소요량 [kWh]\",data:" + chart_전_난방_가스[i] + ",borderColor:\"#F4B183\",backgroundColor:\"#F4B183\",dash:false}," +
               "{type:\"bar\",barPercentage:0.4,label:\"냉방 에너지 소요량 [kWh]\",data:" + chart_전_냉방_가스[i] + ",borderColor:\"#9DC3E6\",backgroundColor:\"#9DC3E6\",dash:false}," +
               "],max:" + (Math.Round(max_graph_gas / 1000) * 1000 + 500).ToString() + ",step:100,legend:true,stacked:true}";

                charts += ",{data:[" +
                "{type:\"bar\",barPercentage:0.4,label:\"기저 에너지 소요량 [kWh]\",data:" + chart_후_기저_가스[i] + ",borderColor:\"#BFBFBF\",backgroundColor:\"#BFBFBF\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"급탕 에너지 소요량 [kWh]\",data:" + chart_후_급탕_가스[i] + ",borderColor:\"#A9D18E\",backgroundColor:\"#A9D18E\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"공조 에너지 소요량 [kWh]\",data:" + chart_후_공조_가스[i] + ",borderColor:\"#70AD47\",backgroundColor:\"#70AD47\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"조명 에너지 소요량 [kWh]\",data:" + chart_후_조명_가스[i] + ",borderColor:\"#FFD966\",backgroundColor:\"#FFD966\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"난방 에너지 소요량 [kWh]\",data:" + chart_후_난방_가스[i] + ",borderColor:\"#F4B183\",backgroundColor:\"#F4B183\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"냉방 에너지 소요량 [kWh]\",data:" + chart_후_냉방_가스[i] + ",borderColor:\"#9DC3E6\",backgroundColor:\"#9DC3E6\",dash:false}," +
                "],max:" + (Math.Round(max_graph_gas / 1000) * 1000 + 500).ToString() + ",step:100,legend:true,stacked:true}";

                charts += ",{data:[{type:\"line\",yAxisID: 'y',label:\"리모델링 전 소요량 [kWh]\",data:" + chart_전_가스[i] + ",borderColor:\"#5B9BD5\",backgroundColor:\"#5B9BD5\",dash:false, tension: 0.4}," +
                "{type:\"line\",yAxisID: 'y',label:\"리모델링 후 소요량 [kWh]\",data:" + chart_후_가스[i] + ",borderColor:\"#ED7D31\",backgroundColor:\"#ED7D31\",dash:false, tension: 0.4}," +
                "{type:\"bar\",yAxisID: 'y1',barPercentage:0.4,label:\"절감률 [%]\",data:" + chart_절감률_가스[i] + ",borderColor:\"#A5A5A5\",backgroundColor:\"#A5A5A5\",dash:false}," +
                "],max:" + (Math.Round(max_graph_gas / 1000) * 1000 + 500).ToString() + ",step:100,legend:true}";
            }

            runScript("init(" + s + "," + s2 + "," + "[" + charts + "])");
            //  runScript("init(" + s + "," + s2 + ")");


        }
        private void button1_Click(object sender, EventArgs e)
        {
            webView21.CoreWebView2.ShowPrintUI();
        }
    }
}