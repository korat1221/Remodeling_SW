using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main.contents.Result.Building_Report
{
    internal class Error_Report_Gas
    {
        public String Error_Report()
        {
            string s, s2;
            string[][] 번호 = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
            Boolean check_use = false;
            List<object> items = new List<object>();
            List<object> items2 = new List<object>();
            List<object> data = new List<object>();

            List<object>[] __data = new List<object>[700];

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
            double[,] Quse_gas_mth = new double[4, 12]; double[] Quse_gas_a = new double[4];
            string[] year_gas = new string[3]; 
            double[] dmth = new double[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            while (++i < 700)
            {
                __data[i] = new List<object>();
            }


            string charts = "";

            i = -1;
            while (++i < 번호.Length)
            {
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

                
                string[][] 연도 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "BuildingEnergyUse", "연도", "not 연료 = '전기'");
                try
                {
                    if (연도.Length > 0)
                    {
                        check_use = true;
                        __data[0].Add(new { idx = i, val = 연도[0][0] }); //연도 표기 
                        __data[3].Add(new { idx = i, val = 연도[0][0] }); //연도 표기 
                        year_gas[0] = 연도[0][0];

                        __data[1].Add(new { idx = i, val = 연도[1][0] }); //연도 표기 
                        __data[4].Add(new { idx = i, val = 연도[1][0] }); //연도 표기 
                        year_gas[1] = 연도[1][0];

                        __data[2].Add(new { idx = i, val = 연도[2][0] }); //연도 표기 
                        __data[5].Add(new { idx = i, val = 연도[2][0] }); //연도 표기 
                        year_gas[2] = 연도[2][0];
                    }

                    if (check_use) { }
                    else
                    {
                        __data[0].Add(new { idx = i, val = "" }); //연도 표기 
                        __data[3].Add(new { idx = i, val = "" }); //연도 표기 
                        year_gas[0] = "";

                        __data[1].Add(new { idx = i, val = "" }); //연도 표기 
                        __data[4].Add(new { idx = i, val = "" }); //연도 표기 
                        year_gas[1] = "";

                        __data[2].Add(new { idx = i, val = "" }); //연도 표기 
                        __data[5].Add(new { idx = i, val = "" }); //연도 표기 
                        year_gas[2] = "";
                    }
                }
                catch { }



                double Area = 0;
                string[][] A = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "냉난방유무 <> '비냉난방'");
                if (A.Length > 0)
                {
                    for (int a = 0; a < A.Length; a++)
                    {
                        Area += Convert.ToDouble(A[a][0]);
                    }
                }
                string[][] Value_사용시작일_가스 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "사용시작일", "not 연료='전기' and 단위='kWh'");
                int yearnum = 0;
                if (Value_사용시작일_가스.Length > 0)
                {
                    if (Convert.ToDouble(Value_사용시작일_가스[0][0]) > 1)
                    {
                        string[][] Gas1, Gas2;
                        for (int mth = 0; mth < 11; mth++)
                        {
                            Gas1 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (mth + 1).ToString() + "월' AND not 연료='전기' and 단위='kWh'");
                            Gas2 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (mth + 2).ToString() + "월' AND not 연료='전기' and 단위='kWh'");
                            if (Gas1.Length > 0 && Gas2.Length > 0)
                            {
                                for (int k = 0; k < Gas1.Length; k++) //연도별
                                {
                                    Quse_gas_mth[k, mth] = (Convert.ToDouble(Gas1[k][0]) * Convert.ToDouble(Value_사용시작일_가스[0][0]) / dmth[mth] + Convert.ToDouble(Gas2[k][0]) * (dmth[mth] - Convert.ToDouble(Value_사용시작일_가스[0][0])) / dmth[mth]);
                                }
                                yearnum = Gas1.Length;
                            }
                        }

                        Gas1 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (12).ToString() + "월' AND not 연료='전기' and 단위='kWh'");
                        Gas2 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (1).ToString() + "월' AND not 연료='전기' and 단위='kWh'");
                        if (Gas1.Length > 0 && Gas2.Length > 0)
                        {
                            for (int k = 0; k < Gas1.Length; k++) //연도별
                            {
                                Quse_gas_mth[k, 11] = (Convert.ToDouble(Gas1[k][0]) * Convert.ToDouble(Value_사용시작일_가스[0][0]) / dmth[11] + Convert.ToDouble(Gas2[k][0]) * (dmth[11] - Convert.ToDouble(Value_사용시작일_가스[0][0])) / dmth[11]);
                            }
                        }
                    }
                    else
                    {
                        for (int mth = 0; mth < 12; mth++)
                        {
                            string[][] Gas = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (mth + 1).ToString() + "월' AND not 연료='전기' and 단위='kWh'");
                            if (Gas.Length > 0)
                            {
                                for (int k = 0; k < Gas.Length; k++) //연도별
                                {
                                    Quse_gas_mth[k, mth] = Convert.ToDouble(Gas[k][0]);
                                }
                                yearnum = Gas.Length;
                            }
                        }
                    }
                }
                for (int mth = 0; mth < 12; mth++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        Quse_gas_mth[3, mth] += Quse_gas_mth[k, mth];
                    }
                    Quse_gas_mth[3, mth] = Quse_gas_mth[3, mth] / yearnum;
                    Quse_gas_a[0] += Quse_gas_mth[0, mth];
                    Quse_gas_a[1] += Quse_gas_mth[1, mth];
                    Quse_gas_a[2] += Quse_gas_mth[2, mth];
                    Quse_gas_a[3] += Quse_gas_mth[3, mth];
                }

                if (check_use) { }
                else
                {
                    for (int k = 0; k < 4; k++)
                    {
                        for (int mth = 0; mth < 12; mth++)
                        {
                            Quse_gas_mth[k, mth] = 0;
                        }
                        Quse_gas_a[k] = 0;
                    }
                }
                __data[6].Add(new { idx = i, val = Convert.ToDouble(Quse_gas_a[0]).ToString("#,##0") }); //연간 에너지사용량
                __data[7].Add(new { idx = i, val = Convert.ToDouble(Quse_gas_a[1]).ToString("#,##0") });
                __data[8].Add(new { idx = i, val = Convert.ToDouble(Quse_gas_a[2]).ToString("#,##0") });
                __data[9].Add(new { idx = i, val = Convert.ToDouble(Quse_gas_a[3]).ToString("#,##0") });

                __data[28].Add(new { idx = i, val = (Quse_gas_a[0] / Area).ToString("0.0") }); //바닥면적당 연간 에너지사용량
                __data[29].Add(new { idx = i, val = (Quse_gas_a[1] / Area).ToString("0.0") });
                __data[30].Add(new { idx = i, val = (Quse_gas_a[2] / Area).ToString("0.0") });
                __data[31].Add(new { idx = i, val = (Quse_gas_a[3] / Area).ToString("0.0") });

                for (int mth = 0; mth < 12; mth++)
                {
                    __data[10].Add(new { idx = i * 13 + mth, val = Quse_gas_mth[0, mth].ToString("#,##0") }); //월별 에너지사용량 
                    __data[11].Add(new { idx = i * 13 + mth, val = Quse_gas_mth[1, mth].ToString("#,##0") });
                    __data[12].Add(new { idx = i * 13 + mth, val = Quse_gas_mth[2, mth].ToString("#,##0") });
                    __data[13].Add(new { idx = i * 13 + mth, val = Quse_gas_mth[3, mth].ToString("#,##0") });


                    가스사용량chart1.Add(Math.Round(Double.Parse(Quse_gas_mth[0, mth].ToString()), 3) + 0);
                    가스사용량chart2.Add(Math.Round(Double.Parse(Quse_gas_mth[1, mth].ToString()), 3) + 0);
                    가스사용량chart3.Add(Math.Round(Double.Parse(Quse_gas_mth[2, mth].ToString()), 3) + 0);
                    가스사용량chart4.Add(Math.Round(Double.Parse(Quse_gas_mth[3, mth].ToString()), 3) + 0);
                    가스사용량chart.Add(Math.Round(Double.Parse(Quse_gas_mth[3, mth].ToString()), 3) + 0);
                }
                __data[10].Add(new { idx = i * 13 + 12, val = Quse_gas_a[0].ToString("#,##0") }); //월별 에너지사용량 
                __data[11].Add(new { idx = i * 13 + 12, val = Quse_gas_a[1].ToString("#,##0") });
                __data[12].Add(new { idx = i * 13 + 12, val = Quse_gas_a[2].ToString("#,##0") });
                __data[13].Add(new { idx = i * 13 + 12, val = Quse_gas_a[3].ToString("#,##0") });

                double Qh_a_가스 = 0, Qc_a_가스 = 0, Qw_a_가스 = 0, Ql_a_가스 = 0, Qv_a_가스 = 0, Qbase_a_가스 = 0, Qreg_a_가스 = 0, Qtot_a_가스 = 0;
                double[] Qtot_mth_가스 = new double[12];
                double Error_mth_avg_가스 = 0;
                double max_use = 0;
                for (int mth = 1; mth < 12; mth++)
                {
                    if (Quse_gas_mth[3, mth] > max_use)
                    {
                        max_use = Quse_gas_mth[3, mth];
                    }
                }
                for (int mth = 0; mth < 12; mth++)
                {
                    string[][] Final = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,기저에너지,연료,신재생에너지,총에너지소요량", "not 연료='전기' and 월 ='" + (mth + 1).ToString() + "월'");
                    if (Final.Length > 0)
                    {
                        __data[14].Add(new { idx = i * 13 + mth, val = Convert.ToDouble(Final[0][0]).ToString("#,##0") }); //월별 난방 
                        __data[15].Add(new { idx = i * 13 + mth, val = Convert.ToDouble(Final[0][1]).ToString("#,##0") }); //월별 냉방 
                        __data[16].Add(new { idx = i * 13 + mth, val = Convert.ToDouble(Final[0][2]).ToString("#,##0") }); //월별 급탕 
                        __data[17].Add(new { idx = i * 13 + mth, val = Convert.ToDouble(Final[0][3]).ToString("#,##0") }); //월별 조명 
                        __data[18].Add(new { idx = i * 13 + mth, val = Convert.ToDouble(Final[0][4]).ToString("#,##0") }); //월별 공조
                        __data[19].Add(new { idx = i * 13 + mth, val = Convert.ToDouble(Final[0][5]).ToString("#,##0") }); //월별 기저 
                        난방가스소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][0])), 3) + 0);
                        냉방가스소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][1])), 3) + 0);
                        급탕가스소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][2])), 3) + 0);
                        조명가스소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][3])), 3) + 0);
                        공조가스소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][4])), 3) + 0);
                        기저가스소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Final[0][5])), 3) + 0);
                        string[][] PV = Program.DB.getValue(DB.type.ProjDB, "PV_Result", "PV생산량", "월 ='" + (mth + 1).ToString() + "월'");

                        __data[101].Add(new { idx = i * 13 + mth, val = Convert.ToDouble(Final[0][7]).ToString("#,##0") }); //월별 신재생 
                        Qtot_mth_가스[mth] = Convert.ToDouble(Final[0][8]);
                        __data[20].Add(new { idx = i * 13 + mth, val = Qtot_mth_가스[mth].ToString("#,##0") }); //월별 전기 에너지소요량 

                        총가스소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Qtot_mth_가스[mth].ToString())), 3) + 0);
                        가스소요량chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed(Qtot_mth_가스[mth].ToString())), 3) + 0);
                        if (check_use)
                        {
                            double error = Math.Abs(Qtot_mth_가스[mth] - Quse_gas_mth[3, mth]) / Quse_gas_mth[3, mth] * Quse_gas_mth[3, mth] / max_use * 100;
                            __data[39].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed((error).ToString("0.0")) }); //오차율
                            Error_mth_avg_가스 += error;
                            가스오차율chart.Add(Math.Round(Double.Parse(Program.UTIL.asFixed((error / 100).ToString())), 3) + 0);  /// >>> 백분율 단위로 표시 필요 
                        }
                        else
                        {
                            __data[39].Add(new { idx = i * 12 + mth, val = 0.ToString("0.0") }); //오차율
                            Error_mth_avg_가스 += 0;
                            가스오차율chart.Add(0);  /// >>> 백분율 단위로 표시 필요 
                        }
                        Qh_a_가스 += Convert.ToDouble(Final[0][0]);
                        Qc_a_가스 += Convert.ToDouble(Final[0][1]);
                        Qw_a_가스 += Convert.ToDouble(Final[0][2]);
                        Ql_a_가스 += Convert.ToDouble(Final[0][3]);
                        Qv_a_가스 += Convert.ToDouble(Final[0][4]);
                        Qbase_a_가스 += Convert.ToDouble(Final[0][5]);
                        Qreg_a_가스 += Convert.ToDouble(Final[0][7]);
                    }
                }

                Qtot_a_가스 = Qh_a_가스 + Qc_a_가스 + Qw_a_가스 + Ql_a_가스 + Qv_a_가스 + Qbase_a_가스 - Qreg_a_가스;
                double tCO2 = (Qtot_a_가스 - Qbase_a_가스) / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                double TOE = (Qtot_a_가스 - Qbase_a_가스 )/ 38.9 / 0.277778 * 0.00103;
                __data[104].Add(new { idx = i, val = (tCO2).ToString("0.0") }); //온실가스
                __data[105].Add(new { idx = i, val = (TOE).ToString("0.0") }); //온실가스

                __data[14].Add(new { idx = i * 13 + 12, val = Qh_a_가스.ToString("#,##0") }); //월별 난방 
                __data[15].Add(new { idx = i * 13 + 12, val = Qc_a_가스.ToString("#,##0") }); //월별 냉방 
                __data[16].Add(new { idx = i * 13 + 12, val = Qw_a_가스.ToString("#,##0") }); //월별 급탕 
                __data[17].Add(new { idx = i * 13 + 12, val = Ql_a_가스.ToString("#,##0") }); //월별 조명 
                __data[18].Add(new { idx = i * 13 + 12, val = Qv_a_가스.ToString("#,##0") }); //월별 공조
                __data[19].Add(new { idx = i * 13 + 12, val = Qbase_a_가스.ToString("#,##0") }); //월별 기저 
                __data[101].Add(new { idx = i * 13 + 12, val = Qreg_a_가스.ToString("#,##0") }); //월별 신재생 
                __data[20].Add(new { idx = i * 13 + 12, val = Qtot_a_가스.ToString("#,##0") }); //월별 전기 에너지소요량 

                __data[21].Add(new { idx = i, val = Qh_a_가스.ToString("#,##0") });
                __data[22].Add(new { idx = i, val = Qc_a_가스.ToString("#,##0") });
                __data[23].Add(new { idx = i, val = Qw_a_가스.ToString("#,##0") });
                __data[24].Add(new { idx = i, val = Ql_a_가스.ToString("#,##0") });
                __data[25].Add(new { idx = i, val = Qv_a_가스.ToString("#,##0") });
                __data[26].Add(new { idx = i, val = Qbase_a_가스.ToString("#,##0") });
                __data[27].Add(new { idx = i, val = Qtot_a_가스.ToString("#,##0") });
                __data[102].Add(new { idx = i, val = Qreg_a_가스.ToString("#,##0") });


                __data[32].Add(new { idx = i, val = (Qh_a_가스 / Area).ToString("0.0") });
                __data[33].Add(new { idx = i, val = (Qc_a_가스 / Area).ToString("0.0") });
                __data[34].Add(new { idx = i, val = (Qw_a_가스 / Area).ToString("0.0") });
                __data[35].Add(new { idx = i, val = (Ql_a_가스 / Area).ToString("0.0") }); 
                __data[36].Add(new { idx = i, val = (Qv_a_가스 / Area).ToString("0.0") });
                __data[37].Add(new { idx = i, val = (Qbase_a_가스 / Area).ToString("0.0") });
                __data[38].Add(new { idx = i, val = (Qtot_a_가스 / Area).ToString("0.0") });
                __data[103].Add(new { idx = i, val = (Qreg_a_가스 / Area).ToString("0.0") });

                double Error_a_가스 = 0;
                if (check_use)
                {
                    Error_mth_avg_가스 = Error_mth_avg_가스 / 12;
                    __data[40].Add(new { idx = i, val = Error_mth_avg_가스.ToString("0.0") + "%" });
                    Error_a_가스 = (Quse_gas_a[3] - Qtot_a_가스) / Quse_gas_a[3] * 100;
                    __data[41].Add(new { idx = i, val = Error_a_가스.ToString("0.0") + "%" });
                }
                else
                {
                    Error_mth_avg_가스 = 0;
                    __data[40].Add(new { idx = i, val = Error_mth_avg_가스.ToString("0.0") + "%" });
                    Error_a_가스 = 0;
                    __data[41].Add(new { idx = i, val = Error_a_가스.ToString("0.0") + "%" });

                }
                __data[40].Add(new { idx = i, val = Error_mth_avg_가스.ToString("0.0") + "%" });
                __data[41].Add(new { idx = i, val = Error_a_가스.ToString("0.0") + "%" });

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
                data.Add(new { cname = "yeartitle1", data = __data[0] });
                data.Add(new { cname = "yeartitle2", data = __data[1] });
                data.Add(new { cname = "yeartitle3", data = __data[2] });

                data.Add(new { cname = "yeartitle4", data = __data[3] });
                data.Add(new { cname = "yeartitle5", data = __data[4] });
                data.Add(new { cname = "yeartitle6", data = __data[5] });

                data.Add(new { cname = "quse_gas_a1", data = __data[6] });
                data.Add(new { cname = "quse_gas_a2", data = __data[7] });
                data.Add(new { cname = "quse_gas_a3", data = __data[8] });
                data.Add(new { cname = "quse_gas_a4", data = __data[9] });

                data.Add(new { cname = "quse_gas_mth1", data = __data[10] });
                data.Add(new { cname = "quse_gas_mth2", data = __data[11] });
                data.Add(new { cname = "quse_gas_mth3", data = __data[12] });
                data.Add(new { cname = "quse_gas_mth4", data = __data[13] });

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

                data.Add(new { cname = "quse_gas_a1_area", data = __data[28] });
                data.Add(new { cname = "quse_gas_a2_area", data = __data[29] });
                data.Add(new { cname = "quse_gas_a3_area", data = __data[30] });
                data.Add(new { cname = "quse_gas_a4_area", data = __data[31] });

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
                data.Add(new { cname = "tCO2", data = __data[104] });
                data.Add(new { cname = "toe", data = __data[105] });

            }
            items.Add("Error_Report_gas.htm");
            s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
            s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
            System.Text.Json.JsonSerializer.Serialize(__data[10].ToArray());
            string s4;
            i = -1;
            double max_graph_gas = 0;

            if(check_use )
            {
                string[][] Energyuse = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "Max(에너지사용량)", "not 연료='전기'");
                if (Energyuse.Length > 0)
                {
                    max_graph_gas = Convert.ToDouble(Energyuse[0][0]);
                }
            }

            string[][] Final2 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "Max(총에너지소요량)", "not 연료='전기' and not 월='연간'");
            if (Final2.Length > 0)
            {
                if (max_graph_gas < Convert.ToDouble(Final2[0][0]))
                {
                    max_graph_gas = Convert.ToDouble(Final2[0][0]);
                }
            }

            Debug.Print("start");
            i = -1;
            while (++i < 번호.Length)
            {
                if (charts != "") charts += ",";

                charts += "{data:[{type:\"line\",label:\"" + year_gas[0] + "\",data:" + chart_가스사용량1[i] + ",borderColor:\"#5B9BD5\",backgroundColor:\"#5B9BD5\",dash:false, tension: 0.4}," +
                "{type:\"line\",label:\"" + year_gas[1] + "\",data:" + chart_가스사용량2[i] + ",borderColor:\"#70AD47\",backgroundColor:\"#70AD47\",dash:false, tension: 0.4}," +
                "{type:\"line\",label:\"" + year_gas[2] + "\",data:" + chart_가스사용량3[i] + ",borderColor:\"#4472C4\",backgroundColor:\"#4472C4\",dash:false, tension: 0.4}," +
                "{type:\"line\",label:\"평균\",data:" + chart_가스사용량4[i] + ",borderColor:\"#ED7D31\",backgroundColor:\"#ED7D31\",dash:false, tension: 0.4}," +
                "],max:" + (Math.Round(max_graph_gas / 1000) * 1000 + 500).ToString() + ",step:100,legend:true}";

                charts += ",{data:[" +
                "{type:\"bar\",barPercentage:0.4,label:\"기저\",data:" + chart_기저가스소요량[i] + ",borderColor:\"#BFBFBF\",backgroundColor:\"#BFBFBF\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"급탕\",data:" + chart_급탕가스소요량[i] + ",borderColor:\"#A9D18E\",backgroundColor:\"#A9D18E\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"공조\",data:" + chart_공조가스소요량[i] + ",borderColor:\"#70AD47\",backgroundColor:\"#70AD47\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"조명\",data:" + chart_조명가스소요량[i] + ",borderColor:\"#FFD966\",backgroundColor:\"#FFD966\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"난방\",data:" + chart_난방가스소요량[i] + ",borderColor:\"#F4B183\",backgroundColor:\"#F4B183\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"냉방\",data:" + chart_냉방가스소요량[i] + ",borderColor:\"#9DC3E6\",backgroundColor:\"#9DC3E6\",dash:false}," +
                "],max:" + (Math.Round(max_graph_gas / 1000) * 1000 + 500).ToString() + ",step:100,legend:true,stacked:true}";

                charts += ",{data:[{type:\"line\",yAxisID: 'y',label:\"에너지사용량\",data:" + chart_가스사용량[i] + ",borderColor:\"#5B9BD5\",backgroundColor:\"#5B9BD5\",dash:false, tension: 0.4}," +
                "{type:\"line\",yAxisID: 'y',label:\"에너지소요량\",data:" + chart_가스소요량[i] + ",borderColor:\"#ED7D31\",backgroundColor:\"#ED7D31\",dash:false, tension: 0.4}," +
                "{type:\"bar\",yAxisID: 'y1',barPercentage:0.4,label:\"오차율\",data:" + chart_가스오차율[i] + ",borderColor:\"#A5A5A5\",backgroundColor:\"#A5A5A5\",dash:false}," +
                "],max:" + (Math.Round(max_graph_gas / 1000) * 1000 + 500).ToString() + ",step:100,legend:true}";
          
            }

            string script = "init(" + s + "," + s2 + "," + "[" + charts + "])";
            return script;
        }

    }
}
