using Microsoft.Web.WebView2.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.contents.Result
{
    public partial class Algorithm_Heating : Form
    {
        bool scriptable = false;
        public Algorithm_Heating()
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

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            string s, s2;
            string[][] HeatingValue = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "번호,존,보일러종류,공급설비1종류,공급설비2종류,명칭,주요설비,외기히트펌프번호", "");
            ArrayList Zone_split = new ArrayList();

            List<object> items = new List<object>();
            List<object> data = new List<object>();

            List<object>[] __data = new List<object>[700];

            List<string> chart_data1 = new List<string>();
            List<string> chart_data2 = new List<string>();
            List<string> chart_data3 = new List<string>();
            List<string> chart_data4 = new List<string>();
            List<string> chart_data5 = new List<string>();
            List<string> chart_data6 = new List<string>();

            int i = -1, n;

            while (++i < 700)
            {
                __data[i] = new List<object>();
            }

            i = -1;

            while (++i < HeatingValue.Length)
            {
                List<object> chart1 = new List<object>();
                List<object> chart2 = new List<object>();
                List<object> chart3 = new List<object>();
                List<object> chart4 = new List<object>();
                List<object> chart5 = new List<object>();
                List<object> chart6 = new List<object>();

                if (HeatingValue[i][1] != null) //존 나누기 
                {
                    if (HeatingValue[i][1].Contains("+"))
                    {
                        string[] token = HeatingValue[i][1].Split('+');
                        Zone_split.Clear();
                        foreach (var item in token)
                        {
                            Zone_split.Add(item.ToString());
                        }
                    }
                    else
                    {
                        Zone_split.Clear();
                        Zone_split.Add(HeatingValue[i][1]);
                    }
                }
                string[][] SystemValue = null;
                if (HeatingValue[i][6] == "보일러")
                {
                    SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "명칭,연료,Type,용량,전부하효율,부분부하효율,소비전력,대기전력,신규기존", "번호 ='" + HeatingValue[i][2] + "'");
                }
                else
                {
                    SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", "명칭,연료,연료,난방정격용량,난방정격COP,난방정격소비전력,설치", "번호 ='" + HeatingValue[i][7] + "'");
                }

                items.Add("Heating_Report.htm"); // 예시 코드: 메인 메뉴 동적 할당

                __data[0].Add(new { idx = i, val = HeatingValue[i][5] }); //타이틀 
                __data[1].Add(new { idx = i, val = HeatingValue[i][6] }); //생산설비 유형
                if (HeatingValue[i][6] == "보일러")
                {
                    __data[2].Add(new { idx = i, val = SystemValue[0][2] }); //생산설비 종류
                }
                else 
                {
                    __data[2].Add(new { idx = i, val = "외기 히트펌프" }); //생산설비 종류
                }
                __data[3].Add(new { idx = i, val = Program.UTIL.asFixed(SystemValue[0][3]) }); //정격용량

                double Q_max_tot = 0;
                string[][] ZoneValue;
                for (int k = 0; k < Zone_split.Count; ++k)
                {
                    ZoneValue = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호 ='" + Zone_split[k] + "' and 난방_냉방 = '난방'");
                    Q_max_tot += Convert.ToDouble(ZoneValue[0][0]);
                }

                double Percent = Convert.ToDouble(SystemValue[0][3]) / (Q_max_tot / 1000) * 100;
                __data[4].Add(new { idx = i, val = Program.UTIL.asFixed(Percent.ToString()) }); //용량비율 
                __data[5].Add(new { idx = i, val = Program.UTIL.asFixed(SystemValue[0][4]) }); //정격효율
                __data[6].Add(new { idx = i, val = SystemValue[0][1] }); //연료
                if (HeatingValue[i][6] == "보일러")
                {
                    __data[7].Add(new { idx = i, val = SystemValue[0][8] }); //리모델링Type
                }
                else
                {
                    __data[7].Add(new { idx = i, val = SystemValue[0][6] }); //리모델링Type

                }
                __data[8].Add(new { idx = i, val = HeatingValue[i][3] }); //공급설비1종류
                __data[9].Add(new { idx = i, val = HeatingValue[i][4] }); //공급설비2종류
                __data[10].Add(new { idx = i, val = Zone_split.Count.ToString() }); //존 개수 
                
                double[] tot = new double[6];
                for (int k = 0; k < Zone_split.Count; ++k)
                {
                    ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적,천장고,냉난방시간,연이용일수,용도프로필", "존번호 ='" + Zone_split[k] + "'");
                    string[][] UsageValue = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "난방설정온도", "용도명 ='" + ZoneValue[0][4] + "'");
                    string[][] Qba = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_a", "번호 ='" + Zone_split[k] + "' and 난방_냉방 = '난방'");
                    tot[0] += Convert.ToDouble(ZoneValue[0][0]); //순바닥면적
                    tot[1] += (Convert.ToDouble(ZoneValue[0][1]) * Convert.ToDouble(Qba[0][0]));//천장고*요구량
                    tot[2] += (Convert.ToDouble(UsageValue[0][0]) * Convert.ToDouble(Qba[0][0]));//난방설정온도*요구량
                    tot[3] += (Convert.ToDouble(ZoneValue[0][2]) * Convert.ToDouble(Qba[0][0]));//냉난방시간*요구량
                    tot[4] += (Convert.ToDouble(ZoneValue[0][3]) * Convert.ToDouble(Qba[0][0]));//연이용일수*요구량
                    tot[5] += Convert.ToDouble(Qba[0][0]); //요구량 
                }
                __data[11].Add(new { idx = i, val = Program.UTIL.asFixed(tot[0].ToString()) }); //순바닥면적합계
                __data[12].Add(new { idx = i, val = Program.UTIL.asFixed((tot[1] / tot[5]).ToString()) }); //평균천장고 
                __data[13].Add(new { idx = i, val = Program.UTIL.asFixed((tot[2] / tot[5]).ToString()) }); //난방설정온도
                __data[14].Add(new { idx = i, val = Program.UTIL.asFixed((tot[3] / tot[5]).ToString()) }); //평균냉난방시간 
                __data[15].Add(new { idx = i, val = Program.UTIL.asFixed((tot[4] / tot[5]).ToString()) }); //평균연이용일수 

                string[][] Result;
                for (int mth = 0; mth < 12; mth++)
                {
                    Result = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Result", "Qh_f", "번호 = '" + HeatingValue[i][0] + "' AND 월 ='" + (mth + 1).ToString() + "월'");
                    chart1.Add(Math.Round(Double.Parse(Result[0][0]), 3) + 0);
                    __data[16].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][0]) }); //난방 에너지소요량 
                    Result = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Result", "Qh_outg", "번호 = '" + HeatingValue[i][0] + "' AND 월 ='" + (mth + 1).ToString() + "월'");
                    chart2.Add(Math.Round(Double.Parse(Result[0][0]), 3) + 0);
                    __data[17].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][0]) }); //난방 에너지공급량 
                    Result = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Result", "Qhb_mth_sum", "번호 = '" + HeatingValue[i][0] + "' AND 월 ='" + (mth + 1).ToString() + "월'");
                    chart3.Add(Math.Round(Double.Parse(Result[0][0]), 3) + 0);
                    __data[18].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][0]) }); //에너지요구량 
                    Result = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Result", "Wh_ce, Wh_d, Wh_s, Wh_g", "번호 = '" + HeatingValue[i][0] + "' AND 월 ='" + (mth + 1).ToString() + "월'");
                    double Wh_tot = Convert.ToDouble(Result[0][0]) + Convert.ToDouble(Result[0][1]) + Convert.ToDouble(Result[0][2]) + Convert.ToDouble(Result[0][3]);
                    __data[20].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Wh_tot.ToString()) }); //난방 보조에너지 


                    Result = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Result", "beta_h_ce, beta_h_d, beta_h_s, beta_h_gen", "번호 = '" + HeatingValue[i][0] + "' AND 월 ='" + (mth + 1).ToString() + "월'");
                    __data[21].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][0]) }); //공급 부하율 
                    __data[22].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][1]) }); //분배 부하율 
                    __data[23].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][2]) }); //저장 부하율 
                    __data[24].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][3]) }); //생산 부하율 

                    Result = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Result", "theta_av_ce, theta_av_d, theta_av_s, theta_av_gen", "번호 = '" + HeatingValue[i][0] + "' AND 월 ='" + (mth + 1).ToString() + "월'");
                    __data[25].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][0]) }); //공급 온도
                    __data[26].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][1]) }); //분배 온도 
                    __data[27].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][2]) }); //저장 온도 
                    __data[28].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][3]) }); //생산 온도  

                    Result = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Result", "Qh_ce, dtheta_ce1, dtheta_ce2, Wh_ce", "번호 = '" + HeatingValue[i][0] + "' AND 월 ='" + (mth + 1).ToString() + "월'");
                    chart4.Add(Math.Round(Double.Parse(Result[0][0]), 3) + 0);
                    __data[29].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][0]) }); //공급 열손실
                    __data[30].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][1]) }); //공급 온도편차1
                    __data[31].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][2]) }); //공급 온도편차2
                    __data[32].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][3]) }); //공급 열손실

                    Result = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Result", "Qh_d, Psi_pipe, Wh_d", "번호 = '" + HeatingValue[i][0] + "' AND 월 ='" + (mth + 1).ToString() + "월'");
                    chart5.Add(Math.Round(Double.Parse(Result[0][0]), 3) + 0);
                    __data[33].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][0]) }); //분배 열손실
                    __data[34].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][1]) }); //배관 열관류율
                    __data[35].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][2]) }); //분배 보조에너지

                    Result = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Result", "Qh_s, Qs_po_day, Wh_s", "번호 = '" + HeatingValue[i][0] + "' AND 월 ='" + (mth + 1).ToString() + "월'");
                    chart6.Add(Math.Round(Double.Parse(Result[0][0]), 3) + 0);
                    __data[39].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][0]) }); //저장 열손실
                    __data[40].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][1]) }); //일일축열열손실
                    __data[41].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][2]) }); //저장 보조에너지
                    if (HeatingValue[i][6] == "보일러")
                    {
                        Result = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Result", "Qh_f, Qh_gen, Wh_s, Qh_gen_day, Pgen_Pn, Pgen_Pint, Pgen_P0, eta_gen_Pn, eta_gen_Pint", "번호 = '" + HeatingValue[i][0] + "' AND 월 ='" + (mth + 1).ToString() + "월'");
                        __data[42].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][0]) }); //보일러 에너지소요량
                        __data[43].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][1]) }); //보일러 열손실량
                        __data[44].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][3]) }); //일일 보일러 열손실
                        __data[45].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][4]) }); //정격성능에서의 손실
                        __data[46].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][5]) }); //부분부하에서의 손실
                        __data[47].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][6]) }); //대기상태에서의 손실
                        __data[48].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][7]) }); //전부하효율
                        __data[49].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][8]) }); //부분부하효율
                        __data[50].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][2]) }); //보일러 보조에너지

                        __data[71].Add(new { idx = i, val = "보일러 에너지소요량" });
                        __data[72].Add(new { idx = i, val = "난방 보일러 열손실량" });
                        __data[73].Add(new { idx = i, val = "일일 보일러 열손실" });
                        __data[74].Add(new { idx = i, val = "정격성능에서의 손실" });
                        __data[75].Add(new { idx = i, val = "부분부하에서의 손실" });
                        __data[76].Add(new { idx = i, val = "대기상태에서의 손실" });
                        __data[77].Add(new { idx = i, val = "전부하효율" });
                        __data[78].Add(new { idx = i, val = "부분부하효율" });
                        __data[79].Add(new { idx = i, val = "난방 보일러 보조에너지" });
                    }
                    else if(HeatingValue[i][6] == "외기 히트펌프")
                    {
                        Result = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Result", "Qh_f,fpint_Air,Qh_outg_sngminus7,Qh_outg_sng2,Qh_outg_sng7,COPminus7,COP2,COP7", "번호 = '" + HeatingValue[i][0] + "' AND 월 ='" + (mth + 1).ToString() + "월'");
                        __data[42].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][0]) }); //히트펌프 에너지소요량
                        __data[43].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][1]) }); //fpint
                        __data[44].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][2]) }); //에너지공급량[-7]
                        __data[45].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][3]) }); //에너지공급량[2]
                        __data[46].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][4]) }); //에너지공급량[-7]
                        __data[47].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][5]) }); //보정된 난방COP[-7]
                        __data[48].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][6]) }); //보정된 난방COP[2]
                        __data[49].Add(new { idx = i * 12 + mth, val = Program.UTIL.asFixed(Result[0][7]) }); //보정된 난방COP[7]
                        __data[50].Add(new { idx = i * 12 + mth, val = 0.0 }); //히트펌프 보조에너지 

                        __data[71].Add(new { idx = i, val = "히트펌프 에너지소요량" });
                        __data[72].Add(new { idx = i, val = "부분부하계수" });
                        __data[73].Add(new { idx = i, val = "에너지공급량[-7]" });
                        __data[74].Add(new { idx = i, val = "에너지공급량[2]" });
                        __data[75].Add(new { idx = i, val = "에너지공급량[-7]" });
                        __data[76].Add(new { idx = i, val = "보정된 난방COP[-7]" });
                        __data[77].Add(new { idx = i, val = "보정된 난방COP[2]" });
                        __data[78].Add(new { idx = i, val = "보정된 난방COP[7]" });
                        __data[79].Add(new { idx = i, val = "히트펌프 보조에너지" });
                    }
                }

                double[] Annual = { 0, 0, 0, 0, 0, 0, 0, 0 };
                for (int mth = 0; mth < 12; mth++)
                {
                    Result = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Result", "Qh_f, Qh_outg, Qhb_mth_sum, Qh_ce, Qh_d, Qh_s, Qh_gen", "번호 = '" + HeatingValue[i][0] + "' AND 월 ='" + (mth + 1).ToString() + "월'");

                    for (int a = 0; a < 7; a++)
                    {
                        Annual[a] += Convert.ToDouble(Result[0][a]);
                    }

                }
                __data[51].Add(new { idx = i, val = Program.UTIL.asFixed(Annual[0].ToString()) });//소요량
                __data[52].Add(new { idx = i, val = Program.UTIL.asFixed(Annual[1].ToString()) });//공급량
                __data[53].Add(new { idx = i, val = Program.UTIL.asFixed(Annual[2].ToString()) });//요구량
                __data[55].Add(new { idx = i, val = Program.UTIL.asFixed(Annual[3].ToString()) });//공급
                __data[56].Add(new { idx = i, val = Program.UTIL.asFixed(Annual[4].ToString()) });//분배
                __data[57].Add(new { idx = i, val = Program.UTIL.asFixed(Annual[5].ToString()) });//저장
                __data[58].Add(new { idx = i, val = Program.UTIL.asFixed(Annual[6].ToString()) });//생산
                                                                                                  //
                __data[61].Add(new { idx = i, val = Program.UTIL.asFixed((Annual[0] / tot[0]).ToString()) });//소요량 바닥면적당
                __data[62].Add(new { idx = i, val = Program.UTIL.asFixed((Annual[1] / tot[0]).ToString()) });//공급량 바닥면적당
                __data[63].Add(new { idx = i, val = Program.UTIL.asFixed((Annual[2] / tot[0]).ToString()) });//요구량 바닥면적당
                __data[65].Add(new { idx = i, val = Program.UTIL.asFixed((Annual[3] / tot[0]).ToString()) });//공급 바닥면적당
                __data[66].Add(new { idx = i, val = Program.UTIL.asFixed((Annual[4] / tot[0]).ToString()) });//분배 바닥면적당
                __data[67].Add(new { idx = i, val = Program.UTIL.asFixed((Annual[5] / tot[0]).ToString()) });//저장 바닥면적당
                __data[68].Add(new { idx = i, val = Program.UTIL.asFixed((Annual[6] / tot[0]).ToString()) });//생산 바닥면적당               

                for (int mth = 0; mth < 12; mth++)
                {
                    Result = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Result", "Wh_ce, Wh_d, Wh_s, Wh_g", "번호 = '" + HeatingValue[i][0] + "' AND 월 ='" + (mth + 1).ToString() + "월'");
                    double Wh_tot = Convert.ToDouble(Result[0][0]) + Convert.ToDouble(Result[0][1]) + Convert.ToDouble(Result[0][2]) + Convert.ToDouble(Result[0][3]);
                    Annual[7] += Wh_tot;
                }
                __data[59].Add(new { idx = i, val = Program.UTIL.asFixed(Annual[7].ToString()) }); //보조에너지
                __data[69].Add(new { idx = i, val = Program.UTIL.asFixed((Annual[7] / tot[0]).ToString()) }); //보조에너지 바닥면적당      

                Result = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Result", "Qh_max_sum", "번호 = '" + HeatingValue[i][0] + "'");
                __data[60].Add(new { idx = i, val = Program.UTIL.asFixed((Convert.ToDouble(Result[0][0]) / 1000).ToString()) }); //부하
                __data[70].Add(new { idx = i, val = Program.UTIL.asFixed((Convert.ToDouble(Result[0][0]) / tot[0] / 1000).ToString()) }); //부하 바닥면적당      

                chart_data1.Add(System.Text.Json.JsonSerializer.Serialize(chart1.ToArray()));
                chart_data2.Add(System.Text.Json.JsonSerializer.Serialize(chart2.ToArray()));
                chart_data3.Add(System.Text.Json.JsonSerializer.Serialize(chart3.ToArray()));
                chart_data4.Add(System.Text.Json.JsonSerializer.Serialize(chart4.ToArray()));
                chart_data5.Add(System.Text.Json.JsonSerializer.Serialize(chart5.ToArray()));
                chart_data6.Add(System.Text.Json.JsonSerializer.Serialize(chart6.ToArray()));
            }


            ////////////////////////////////////////////////////////////////////
            data.Add(new { cname = "heatingtitle", data = __data[0] });
            data.Add(new { cname = "systemtype", data = __data[1] });
            data.Add(new { cname = "systemname", data = __data[2] });
            data.Add(new { cname = "power", data = __data[3] });
            data.Add(new { cname = "powerpercent", data = __data[4] });
            data.Add(new { cname = "eta", data = __data[5] });
            data.Add(new { cname = "carrier", data = __data[6] });
            data.Add(new { cname = "remodelingtype", data = __data[7] });

            data.Add(new { cname = "cetype1", data = __data[8] });
            data.Add(new { cname = "cetype2", data = __data[9] });

            data.Add(new { cname = "zonecount", data = __data[10] });
            data.Add(new { cname = "zonearea", data = __data[11] });
            data.Add(new { cname = "zoneheight", data = __data[12] });
            data.Add(new { cname = "theta_ihset", data = __data[13] });
            data.Add(new { cname = "t_hopday", data = __data[14] });
            data.Add(new { cname = "d_opa", data = __data[15] });

            data.Add(new { cname = "qh_f", data = __data[16] });
            data.Add(new { cname = "qh_outg", data = __data[17] });
            data.Add(new { cname = "qhb_mth_sum", data = __data[18] });
            data.Add(new { cname = "wh_tot", data = __data[20] });

            data.Add(new { cname = "beta_h_ce", data = __data[21] });
            data.Add(new { cname = "beta_h_d", data = __data[22] });
            data.Add(new { cname = "beta_h_s", data = __data[23] });
            data.Add(new { cname = "beta_h_gen", data = __data[24] });

            data.Add(new { cname = "theta_av_ce", data = __data[25] });
            data.Add(new { cname = "theta_av_d", data = __data[26] });
            data.Add(new { cname = "theta_av_s", data = __data[27] });
            data.Add(new { cname = "theta_av_gen", data = __data[28] });

            data.Add(new { cname = "qh_ce", data = __data[29] });
            data.Add(new { cname = "dtheta_ce1", data = __data[30] });
            data.Add(new { cname = "dtheta_ce2", data = __data[31] });
            data.Add(new { cname = "wh_ce", data = __data[32] });

            data.Add(new { cname = "qh_d", data = __data[33] });
            data.Add(new { cname = "Ud", data = __data[34] });
            data.Add(new { cname = "wh_d", data = __data[35] });
            data.Add(new { cname = "v", data = __data[36] });
            data.Add(new { cname = "phydr", data = __data[37] });
            data.Add(new { cname = "e_hdaux", data = __data[38] }); ;

            data.Add(new { cname = "qh_s", data = __data[39] });
            data.Add(new { cname = "q_spoday", data = __data[40] });
            data.Add(new { cname = "wh_s", data = __data[41] });

            data.Add(new { cname = "system_value0", data = __data[42] });
            data.Add(new { cname = "system_value1", data = __data[43] });
            data.Add(new { cname = "system_value2", data = __data[44] });
            data.Add(new { cname = "system_value3", data = __data[45] });
            data.Add(new { cname = "system_value4", data = __data[46] });
            data.Add(new { cname = "system_value5", data = __data[47] });
            data.Add(new { cname = "system_value6", data = __data[48] });
            data.Add(new { cname = "system_value7", data = __data[49] });
            data.Add(new { cname = "system_value8", data = __data[50] });

            data.Add(new { cname = "qh_f_a", data = __data[51] });
            data.Add(new { cname = "qh_outg_a", data = __data[52] });
            data.Add(new { cname = "qhb_mth_sum_a", data = __data[53] });
            data.Add(new { cname = "qh_ce_a", data = __data[55] });
            data.Add(new { cname = "qh_d_a", data = __data[56] });
            data.Add(new { cname = "qh_s_a", data = __data[57] });
            data.Add(new { cname = "qh_gen_a", data = __data[58] });
            data.Add(new { cname = "wh_tot_a", data = __data[59] });
            data.Add(new { cname = "qh_max_sum", data = __data[60] });

            data.Add(new { cname = "qh_f_a_area", data = __data[61] });
            data.Add(new { cname = "qh_outg_a_area", data = __data[62] });
            data.Add(new { cname = "qhb_mth_sum_a_area", data = __data[63] });
            data.Add(new { cname = "qh_ce_a_area", data = __data[65] });
            data.Add(new { cname = "qh_d_a_area", data = __data[66] });
            data.Add(new { cname = "qh_s_a_area", data = __data[67] });
            data.Add(new { cname = "qh_gen_a_area", data = __data[68] });
            data.Add(new { cname = "wh_tot_a_area", data = __data[69] });
            data.Add(new { cname = "qh_max_sum_area", data = __data[70] });

            data.Add(new { cname = "system_value0_name", data = __data[71] });
            data.Add(new { cname = "system_value1_name", data = __data[72] });
            data.Add(new { cname = "system_value2_name", data = __data[73] });
            data.Add(new { cname = "system_value3_name", data = __data[74] });
            data.Add(new { cname = "system_value4_name", data = __data[75] });
            data.Add(new { cname = "system_value5_name", data = __data[76] });
            data.Add(new { cname = "system_value6_name", data = __data[77] });
            data.Add(new { cname = "system_value7_name", data = __data[78] });
            data.Add(new { cname = "system_value8_name", data = __data[79] });

            s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
            s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
            System.Text.Json.JsonSerializer.Serialize(__data[16].ToArray());

            string s3 = "", s4;

            i = -1;

            double Max_outg = 0; double Max_f = 0, max = 0; 
            string[][] AA; double[] Value1 = new double[12], Value2 = new double[12];

            while (++i < HeatingValue.Length)
            {
                Max_f = 0;
                Max_outg = 0;

                Debug.Print("start");

                for (int mth = 0; mth < 12; mth++)
                {
                    AA = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Result", "Qh_f,Qh_outg", "번호 = '" + HeatingValue[i][0] + "' AND 월 ='" + (mth + 1).ToString() + "월'");
                    Value1[mth] = Convert.ToDouble(AA[0][0]);
                    Value2[mth] = Convert.ToDouble(AA[0][1]);

                }

                bool done = false;
            for (int mth = 0; mth < 12; mth++)
            {
                if (Max_f < Value1[mth])
                {
                    Max_f = Value1[mth];
                }
                else
                {
                    Max_f = Max_f;
                }

                    done = true;
            }
            for (int mth = 0; mth < 12; mth++)
            {
                if (Max_outg < Value2[mth])
                {
                    Max_outg = Value2[mth];
                }
                else
                {
                    Max_outg = Max_outg;
                }
                    done = true;
                }

            if (!done)
                {
                    Debug.Print("END");
                }

                if (Max_f > Max_outg)
                {
                    max = Convert.ToInt32(Max_f);
                }
                else
                {
                    max = Convert.ToInt32(Max_outg);
                }
            
            
                if (s3 != "") s3 += ",";

                s4 = "{data:[{type:\"line\",label:\"난방 에너지 소요량\",data:" + chart_data1[i] + ",borderColor:\"#ED7D31\",backgroundColor:\"#ED7D31\",dash:false}," +
                "{type:\"line\",label:\"난방 에너지 공급량\",data:" + chart_data2[i] + ",borderColor:\"#FF00FF\",backgroundColor:\"#FF00FF\",dash:false}," +
                "{type:\"line\",label:\"난방 에너지 요구량\",data:" + chart_data3[i] + ",borderColor:\"#000000\",backgroundColor:\"#000000\",dash:false}," +
                "],max:"+ (Math.Round(max / 1000) * 1000+500).ToString() + ",step:100,legend:true}";
                s3 += s4;
                
            }
            runScript("init(" + s + "," + s2 + "," + "[" + s3 + "])");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webView21.CoreWebView2.ShowPrintUI();
        }
    }
}