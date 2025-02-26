using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main.contents.Result.Building_Report
{
    internal class Saving_Report_Gas
    {
        public String Saving_Report()
        {
            string s, s2; 
            string[][] 번호 = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");

            List<object> items = new List<object>();
            List<object> items2 = new List<object>();
            List<object> data = new List<object>();

            List<object>[] __data = new List<object>[700];

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

            double[] Qtot2_mth_가스 = new double[12];
            double[] Qtot_mth_가스 = new double[12];

            while (++i < 번호.Length)
            {

                string[][] res = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기존프로젝트");

                if (res.Length > 0)
                {
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

                    double Qh_a2_가스 = 0, Qc_a2_가스 = 0, Qw_a2_가스 = 0, Ql_a2_가스 = 0, Qv_a2_가스 = 0, Qbase_a2_가스 = 0, Qreg_a2_가스 = 0, Qtot_a2_가스 = 0;
                    for (int mth = 0; mth < 12; mth++)
                    { //리모델링전 가스 소요량 
                        string[][] Final = Program.DB.querySQL(res[0][0], "SELECT 난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량 FROM FinalEnergy_Result where not  연료 = '전기' and 월 = '" + (mth + 1).ToString() + "월'");
                        if (Final.Length > 0)
                        {
                            __data[0].Add(new { idx = i * 13 + mth, val = Convert.ToDouble(Final[0][0]).ToString("#,##0") }); //월별 난방 
                            __data[1].Add(new { idx = i * 13 + mth, val = Convert.ToDouble(Final[0][1]).ToString("#,##0") }); // 월별 냉방 
                            __data[2].Add(new { idx = i * 13 + mth, val = Convert.ToDouble(Final[0][2]).ToString("#,##0") }); //월별 급탕 
                            __data[3].Add(new { idx = i * 13 + mth, val = Convert.ToDouble(Final[0][3]).ToString("#,##0") }); //월별 조명 
                            __data[4].Add(new { idx = i * 13 + mth, val = Convert.ToDouble(Final[0][4]).ToString("#,##0") }); //월별 공조
                            if (Final[0][5] != null && Final[0][5] != "")
                            { __data[5].Add(new { idx = i * 13 + mth, val = Convert.ToDouble(Final[0][5]).ToString("#,##0") }); } //월별 기저            
                            __data[100].Add(new { idx = i * 13 + mth, val = Convert.ToDouble(Final[0][6]).ToString("#,##0") }); //월별 신재생 생산량 
                            Qtot2_mth_가스[mth] = Convert.ToDouble(Final[0][7]);
                            __data[6].Add(new { idx = i * 13 + mth, val = Qtot2_mth_가스[mth].ToString("#,##0") }); //월별 가스 에너지소요량 

                            전_난방_가스Chart.Add(Math.Round(Double.Parse(Final[0][0]), 3) + 0);
                            전_냉방_가스Chart.Add(Math.Round(Double.Parse(Final[0][1]), 3) + 0);
                            전_급탕_가스Chart.Add(Math.Round(Double.Parse(Final[0][2]), 3) + 0);
                            전_조명_가스Chart.Add(Math.Round(Double.Parse(Final[0][3]), 3) + 0);
                            전_공조_가스Chart.Add(Math.Round(Double.Parse(Final[0][4]), 3) + 0);
                            if (Final[0][5] != null && Final[0][5] != "")
                            { 전_기저_가스Chart.Add(Math.Round(Double.Parse(Final[0][5]), 3) + 0); }
                            전_총_가스Chart.Add(Math.Round(Double.Parse(Qtot2_mth_가스[mth].ToString()), 3) + 0);
                            전_가스Chart.Add(Math.Round(Double.Parse(Qtot2_mth_가스[mth].ToString()), 3) + 0);

                            Qh_a2_가스 += Convert.ToDouble(Final[0][0]);
                            Qc_a2_가스 += Convert.ToDouble(Final[0][1]);
                            Qw_a2_가스 += Convert.ToDouble(Final[0][2]);
                            Ql_a2_가스 += Convert.ToDouble(Final[0][3]);
                            Qv_a2_가스 += Convert.ToDouble(Final[0][4]);
                            Qbase_a2_가스 += Convert.ToDouble(Final[0][5]);
                            Qreg_a2_가스 += Convert.ToDouble(Final[0][6]);
                        }
                    }
                    Qtot_a2_가스 = Qh_a2_가스 + Qc_a2_가스 + Qw_a2_가스 + Ql_a2_가스 + Qv_a2_가스 + Qbase_a2_가스 - Qreg_a2_가스;
                    __data[7].Add(new { idx = i, val = Qh_a2_가스.ToString("#,##0") });
                    __data[8].Add(new { idx = i, val = Qc_a2_가스.ToString("#,##0") });
                    __data[9].Add(new { idx = i, val = Qw_a2_가스.ToString("#,##0") });
                    __data[10].Add(new { idx = i, val = Ql_a2_가스.ToString("#,##0") });
                    __data[11].Add(new { idx = i, val = Qv_a2_가스.ToString("#,##0") });
                    __data[12].Add(new { idx = i, val = Qbase_a2_가스.ToString("#,##0") });
                    __data[101].Add(new { idx = i, val = Qreg_a2_가스.ToString("#,##0") });
                    __data[13].Add(new { idx = i, val = Qtot_a2_가스.ToString("#,##0") });


                    __data[0].Add(new { idx = i * 13 + 12, val = Qh_a2_가스.ToString("#,##0") }); //월별 난방 
                    __data[1].Add(new { idx = i * 13 + 12, val = Qc_a2_가스.ToString("#,##0") }); //월별 냉방 
                    __data[2].Add(new { idx = i * 13 + 12, val = Qw_a2_가스.ToString("#,##0") }); //월별 급탕 
                    __data[3].Add(new { idx = i * 13 + 12, val = Ql_a2_가스.ToString("#,##0") }); //월별 조명 
                    __data[4].Add(new { idx = i * 13 + 12, val = Qv_a2_가스.ToString("#,##0") }); //월별 공조
                    __data[5].Add(new { idx = i * 13 + 12, val = Qbase_a2_가스.ToString("#,##0") }); //월별 기저 >>>리모델링 전 값 가져옴 
                    __data[100].Add(new { idx = i * 13 + 12, val = Qreg_a2_가스.ToString("#,##0") });  //월별 신재생 생산량 
                    __data[6].Add(new { idx = i * 13 + 12, val = Qtot_a2_가스.ToString("#,##0") }); //월별 가스 에너지소요량 

                    double Area2_가스 = 0;
                    string[][] A2 = Program.DB.querySQL(res[0][0], "select 순바닥면적 From ZoneGeneral_Form where 냉난방유무 <> '비냉난방'");
                    if (A2.Length > 0)
                    {
                        for (int a = 0; a < A2.Length; a++)
                        {
                            Area2_가스 += Convert.ToDouble(A2[a][0]);
                        }
                    }
                    __data[14].Add(new { idx = i, val = (Qh_a2_가스 / Area2_가스).ToString("0.0") });
                    __data[15].Add(new { idx = i, val = (Qc_a2_가스 / Area2_가스).ToString("0.0") });
                    __data[16].Add(new { idx = i, val = (Qw_a2_가스 / Area2_가스).ToString("0.0") });
                    __data[17].Add(new { idx = i, val = (Ql_a2_가스 / Area2_가스).ToString("0.0") });
                    __data[18].Add(new { idx = i, val = (Qv_a2_가스 / Area2_가스).ToString("0.0") });
                    __data[19].Add(new { idx = i, val = (Qbase_a2_가스 / Area2_가스).ToString("0.0") });
                    __data[102].Add(new { idx = i, val = (Qreg_a2_가스 / Area2_가스).ToString("0.0") });
                    __data[20].Add(new { idx = i, val = (Qtot_a2_가스 / Area2_가스).ToString("0.0") });


                    double Qh_a_가스 = 0, Qc_a_가스 = 0, Qw_a_가스 = 0, Ql_a_가스 = 0, Qv_a_가스 = 0, Qbase_a_가스 = 0, Qreg_a_가스 = 0, Qtot_a_가스 = 0;
                    for (int mth = 0; mth < 12; mth++)
                    { //리모델링후 가스 소요량 
                        string[][] Final = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량", "not 연료='전기' and 월 ='" + (mth + 1).ToString() + "월'");
                        string[][] Final2 = Program.DB.querySQL(res[0][0], "SELECT 기저에너지 FROM FinalEnergy_Result where not 연료 = '전기' and 월 = '" + (mth + 1).ToString() + "월'");
                        if (Final.Length > 0 && Final2.Length > 0)
                        {
                            __data[21].Add(new { idx = i * 13 + mth, val = Convert.ToDouble(Final[0][0]).ToString("#,##0") }); //월별 난방 
                            __data[22].Add(new { idx = i * 13 + mth, val = Convert.ToDouble(Final[0][1]).ToString("#,##0") }); //월별 냉방 
                            __data[23].Add(new { idx = i * 13 + mth, val = Convert.ToDouble(Final[0][2]).ToString("#,##0") }); //월별 급탕 
                            __data[24].Add(new { idx = i * 13 + mth, val = Convert.ToDouble(Final[0][3]).ToString("#,##0") }); //월별 조명 
                            __data[25].Add(new { idx = i * 13 + mth, val = Convert.ToDouble(Final[0][4]).ToString("#,##0") }); //월별 공조
                            if (Final2[0][0] != null && Final2[0][0] != "")
                            { __data[26].Add(new { idx = i * 13 + mth, val = Convert.ToDouble(Final2[0][0]).ToString("#,##0") }); } //월별 기저 >>>리모델링 전 값 가져옴 
                            if (Final[0][6] != null && Final[0][6] != "")
                            { __data[103].Add(new { idx = i * 13 + mth, val = Convert.ToDouble(Final[0][6]).ToString("#,##0") }); } //월별 신재생 생산량 
                            Qtot_mth_가스[mth] = Convert.ToDouble(Final[0][7]);
                            __data[27].Add(new { idx = i * 13 + mth, val = Qtot_mth_가스[mth].ToString("#,##0") }); //월별 가스 에너지소요량 

                            후_난방_가스Chart.Add(Math.Round(Double.Parse(Final[0][0]), 3) + 0);
                            후_냉방_가스Chart.Add(Math.Round(Double.Parse(Final[0][1]), 3) + 0);
                            후_급탕_가스Chart.Add(Math.Round(Double.Parse(Final[0][2]), 3) + 0);
                            후_조명_가스Chart.Add(Math.Round(Double.Parse(Final[0][3]), 3) + 0);
                            후_공조_가스Chart.Add(Math.Round(Double.Parse(Final[0][4]), 3) + 0);
                            if (Final2[0][0] != null && Final2[0][0] != "")
                            { 후_기저_가스Chart.Add(Math.Round(Double.Parse(Final2[0][0]), 3) + 0); }
                            후_총_가스Chart.Add(Math.Round(Double.Parse(Qtot_mth_가스[mth].ToString()), 3) + 0);
                            후_가스Chart.Add(Math.Round(Double.Parse(Qtot_mth_가스[mth].ToString()), 3) + 0);

                            Qh_a_가스 += Convert.ToDouble(Final[0][0]);
                            Qc_a_가스 += Convert.ToDouble(Final[0][1]);
                            Qw_a_가스 += Convert.ToDouble(Final[0][2]);
                            Ql_a_가스 += Convert.ToDouble(Final[0][3]);
                            Qv_a_가스 += Convert.ToDouble(Final[0][4]);
                            if (Final2[0][0] != null && Final2[0][0] != "")
                            { Qbase_a_가스 += Convert.ToDouble(Final2[0][0]); }//리모델링전 값 가져옴 
                            if (Final[0][6] != null && Final[0][6] != "")
                            { Qreg_a_가스 += Convert.ToDouble(Final[0][6]); }
                        }
                    }
                    Qtot_a_가스 = Qh_a_가스 + Qc_a_가스 + Qw_a_가스 + Ql_a_가스 + Qv_a_가스 + Qbase_a_가스 - Qreg_a_가스;
                    __data[28].Add(new { idx = i, val = Qh_a_가스.ToString("#,##0") });
                    __data[29].Add(new { idx = i, val = Qc_a_가스.ToString("#,##0") });
                    __data[30].Add(new { idx = i, val = Qw_a_가스.ToString("#,##0") });
                    __data[31].Add(new { idx = i, val = Ql_a_가스.ToString("#,##0") });
                    __data[32].Add(new { idx = i, val = Qv_a_가스.ToString("#,##0") });
                    __data[33].Add(new { idx = i, val = Qbase_a_가스.ToString("#,##0") });
                    __data[104].Add(new { idx = i, val = Qreg_a_가스.ToString("#,##0") });
                    __data[34].Add(new { idx = i, val = Qtot_a_가스.ToString("#,##0") });



                    __data[21].Add(new { idx = i * 13 + 12, val = Qh_a_가스.ToString("#,##0") }); //월별 난방 
                    __data[22].Add(new { idx = i * 13 + 12, val = Qc_a_가스.ToString("#,##0") }); //월별 냉방 
                    __data[23].Add(new { idx = i * 13 + 12, val = Qw_a_가스.ToString("#,##0") }); //월별 급탕 
                    __data[24].Add(new { idx = i * 13 + 12, val = Ql_a_가스.ToString("#,##0") }); //월별 조명 
                    __data[25].Add(new { idx = i * 13 + 12, val = Qv_a_가스.ToString("#,##0") }); //월별 공조
                    __data[26].Add(new { idx = i * 13 + 12, val = Qbase_a_가스.ToString("#,##0") }); //월별 기저 >>>리모델링 전 값 가져옴 
                    __data[103].Add(new { idx = i * 13 + 12, val = Qreg_a_가스.ToString("#,##0") });  //월별 신재생 생산량 
                    __data[27].Add(new { idx = i * 13 + 12, val = Qtot_a_가스.ToString("#,##0") }); //월별 가스 에너지소요량 

                    double Area_가스 = 0;
                    string[][] A = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "냉난방유무 <> '비냉난방'");
                    if (A.Length > 0)
                    {
                        for (int a = 0; a < A.Length; a++)
                        {
                            Area_가스 += Convert.ToDouble(A[a][0]);
                        }
                    }
                    __data[35].Add(new { idx = i, val = (Qh_a_가스 / Area_가스).ToString("0.0") });
                    __data[36].Add(new { idx = i, val = (Qc_a_가스 / Area_가스).ToString("0.0") });
                    __data[37].Add(new { idx = i, val = (Qw_a_가스 / Area_가스).ToString("0.0") });
                    __data[38].Add(new { idx = i, val = (Ql_a_가스 / Area_가스).ToString("0.0") });
                    __data[39].Add(new { idx = i, val = (Qv_a_가스 / Area_가스).ToString("0.0") });
                    __data[40].Add(new { idx = i, val = (Qbase_a_가스 / Area_가스).ToString("0.0") });
                    __data[105].Add(new { idx = i, val = (Qreg_a_가스 / Area_가스).ToString("0.0") });
                    __data[41].Add(new { idx = i, val = (Qtot_a_가스 / Area_가스).ToString("0.0") });

                    double SavingPercent_mth_avg_가스 = 0; double Saving_mth_avg_가스 = 0; double Saving_a_가스 = 0;
                    for (int mth = 0; mth < 12; mth++)
                    { //가스소요량 월별 절감량
                        Saving_a_가스 += (Qtot2_mth_가스[mth] - Qtot_mth_가스[mth]);
                        Saving_mth_avg_가스 += (Qtot2_mth_가스[mth] - Qtot_mth_가스[mth]);
                        SavingPercent_mth_avg_가스 += (Qtot2_mth_가스[mth] - Qtot_mth_가스[mth]) / Qtot2_mth_가스[mth] * 100;
                    }
                    Saving_mth_avg_가스 = Saving_mth_avg_가스 / 12;
                    SavingPercent_mth_avg_가스 = SavingPercent_mth_avg_가스 / 12;


                    __data[42].Add(new { idx = i, val = (Saving_a_가스).ToString("#,##0")  }); //연간 절감량
                    __data[43].Add(new { idx = i, val = Saving_mth_avg_가스.ToString("#,##0") }); //월평균 절감량
                    __data[44].Add(new { idx = i, val = ((Saving_a_가스 / Qtot_a2_가스) * 100).ToString("0.0") }); //연간 절감율
                    __data[45].Add(new { idx = i, val = SavingPercent_mth_avg_가스.ToString("0.0") }); //월평균 절감율 

                    double saing_tCO2 = Saving_a_가스 / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    double saing_TOE = Saving_a_가스 / 38.9 / 0.277778 * 0.00103;
                    __data[106].Add(new { idx = i, val = (saing_tCO2).ToString("0.0")  }); //온실가스
                    __data[107].Add(new { idx = i, val = (saing_TOE).ToString("0.0")  }); //온실가스
                    for (int mth = 0; mth < 12; mth++)
                    {
                        __data[46].Add(new { idx = i * 13 + mth, val = ((Qtot2_mth_가스[mth] - Qtot_mth_가스[mth])).ToString("#,##0") }); //월별 절감량 
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
                    data.Add(new { cname = "tCO2", data = __data[106] });
                    data.Add(new { cname = "toe", data = __data[107] });
                    #endregion

                }
            }

            items.Add("Saving_Report_Gas.htm");
            s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
            s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
            System.Text.Json.JsonSerializer.Serialize(__data[16].ToArray());

            string s3 = "", s4;

            i = -1;

            double max_graph_gas = 0;

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
                "{type:\"bar\",barPercentage:0.4,label:\"기저 [kWh]\",data:" + chart_전_기저_가스[i] + ",borderColor:\"#BFBFBF\",backgroundColor:\"#BFBFBF\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"급탕 [kWh]\",data:" + chart_전_급탕_가스[i] + ",borderColor:\"#A9D18E\",backgroundColor:\"#A9D18E\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"공조 [kWh]\",data:" + chart_전_공조_가스[i] + ",borderColor:\"#70AD47\",backgroundColor:\"#70AD47\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"조명 [kWh]\",data:" + chart_전_조명_가스[i] + ",borderColor:\"#FFD966\",backgroundColor:\"#FFD966\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"난방 [kWh]\",data:" + chart_전_난방_가스[i] + ",borderColor:\"#F4B183\",backgroundColor:\"#F4B183\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"냉방 [kWh]\",data:" + chart_전_냉방_가스[i] + ",borderColor:\"#9DC3E6\",backgroundColor:\"#9DC3E6\",dash:false}," +
                "],max:" + (Math.Round(max_graph_gas / 1000) * 1000 + 500).ToString() + ",step:100,legend:true,stacked:true}";

                charts += ",{data:[" +
                "{type:\"bar\",barPercentage:0.4,label:\"기저 [kWh]\",data:" + chart_후_기저_가스[i] + ",borderColor:\"#BFBFBF\",backgroundColor:\"#BFBFBF\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"급탕 [kWh]\",data:" + chart_후_급탕_가스[i] + ",borderColor:\"#A9D18E\",backgroundColor:\"#A9D18E\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"공조 [kWh]\",data:" + chart_후_공조_가스[i] + ",borderColor:\"#70AD47\",backgroundColor:\"#70AD47\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"조명 [kWh]\",data:" + chart_후_조명_가스[i] + ",borderColor:\"#FFD966\",backgroundColor:\"#FFD966\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"난방 [kWh]\",data:" + chart_후_난방_가스[i] + ",borderColor:\"#F4B183\",backgroundColor:\"#F4B183\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"냉방 [kWh]\",data:" + chart_후_냉방_가스[i] + ",borderColor:\"#9DC3E6\",backgroundColor:\"#9DC3E6\",dash:false}," +
                "],max:" + (Math.Round(max_graph_gas / 1000) * 1000 + 500).ToString() + ",step:100,legend:true,stacked:true}";

                charts += ",{data:[{type:\"line\",yAxisID: 'y',label:\"리모델링 전 [kWh]\",data:" + chart_전_가스[i] + ",borderColor:\"#5B9BD5\",backgroundColor:\"#5B9BD5\",dash:false, tension: 0.4}," +
                "{type:\"line\",yAxisID: 'y',label:\"리모델링 후 [kWh]\",data:" + chart_후_가스[i] + ",borderColor:\"#ED7D31\",backgroundColor:\"#ED7D31\",dash:false, tension: 0.4}," +
                "{type:\"bar\",yAxisID: 'y1',barPercentage:0.4,label:\"절감률 [%]\",data:" + chart_절감률_가스[i] + ",borderColor:\"#A5A5A5\",backgroundColor:\"#A5A5A5\",dash:false}," +
                "],max:" + (Math.Round(max_graph_gas / 1000) * 1000 + 500).ToString() + ",step:100,legend:true}";

            }

            string script = "init(" + s + "," + s2 + "," + "[" + charts + "])";
            return script;
        }
    }
}
