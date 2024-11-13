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
    public partial class Algorithm_Cooling : Form
    {
        bool scriptable = false;
        public Algorithm_Cooling()
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
            string s, s2;
            string[][] 번호 = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "번호", "");
            List<object> items = new List<object>();
            List<object> data = new List<object>();
            List<object>[] FormData = new List<object>[30];
            List<object>[] ZoneData = new List<object>[30];
            List<object>[] ZahuData = new List<object>[30];
            List<object>[] SourceData = new List<object>[30];
            List<object>[] AnnualData = new List<object>[30];
            List<object>[] ZoneMthData = new List<object>[100];
            List<object>[] ZahuMthData = new List<object>[100];
            List<object>[] MthData = new List<object>[100];
            List<string> chart_data = new List<string>();
            int i = -1;
            while (++i < 30)
            {
                FormData[i] = new List<object>();
                ZoneData[i] = new List<object>();
                ZahuData[i] = new List<object>();
                SourceData[i] = new List<object>();
                AnnualData[i] = new List<object>();
            }
            i = -1;
            while (++i < 100)
            {
                ZoneMthData[i] = new List<object>();
                ZahuMthData[i] = new List<object>();
                MthData[i] = new List<object>();
            }

            i = -1;
            while (++i < 번호.Length)
            {
                string Num = 번호[i][0];
                items.Add("Algorithm_Cooling.htm"); // 예시 코드: 메인 메뉴 동적 할당
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호");
                if (Value.Length > 0)
                {
                    FormData[0].Add(new { idx = i, val = Value[0][0] }); //프로젝트번호
                    data.Add(new { cname = "projectnum", data = FormData[0] });
                }
                FormData[1].Add(new { idx = i, val = Num }); //그림번호
                data.Add(new { cname = "coolingnum", data = FormData[1] });
                FormData[2].Add(new { idx = i, val = Num }); //번호
                data.Add(new { cname = "coolingnum2", data = FormData[2] });
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select 명칭,냉방설비,냉방출력,냉방성능,압축기,제어유형,외기냉방시스템,설치대수  From CoolingSystem_Form Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    FormData[3].Add(new { idx = i, val = Value[0][0] });
                    data.Add(new { cname = "coolingname", data = FormData[3] });
                    FormData[4].Add(new { idx = i, val = Value[0][1] });
                    data.Add(new { cname = "coolingtype", data = FormData[4] });
                    if (double.TryParse(Value[0][2], out double result1))
                    {
                        FormData[5].Add(new { idx = i, val = Convert.ToDouble(Value[0][2]).ToString("0.0") });
                        data.Add(new { cname = "power", data = FormData[5] });
                    }
                    if (double.TryParse(Value[0][3], out double result2))
                    {
                        FormData[6].Add(new { idx = i, val = Convert.ToDouble(Value[0][3]).ToString("0.0") });
                        data.Add(new { cname = "cop", data = FormData[6] });
                    }
                    FormData[7].Add(new { idx = i, val = Value[0][4] });
                    data.Add(new { cname = "compressor", data = FormData[7] });
                    FormData[8].Add(new { idx = i, val = Value[0][5] });
                    data.Add(new { cname = "control", data = FormData[8] });
                    FormData[9].Add(new { idx = i, val = Value[0][6] });
                    data.Add(new { cname = "freecooling", data = FormData[9] });
                    FormData[10].Add(new { idx = i, val = Value[0][7] });
                    data.Add(new { cname = "coolingcount", data = FormData[10] });
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct 개수_z, QCb_a_z,QC_Max_z, 공급설비1_z, 공급설비2_z From CoolingSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    ZoneData[0].Add(new { idx = i, val = Value[0][0] });
                    data.Add(new { cname = "zone_count", data = ZoneData[0] });
                    if (double.TryParse(Value[0][1], out double result1))
                    {
                        ZoneData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][1]) });
                        data.Add(new { cname = "zone_qcba", data = ZoneData[1] });
                    }
                    if (double.TryParse(Value[0][2], out double result2))
                    {
                        ZoneData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][2]) });
                        data.Add(new { cname = "zone_qcmax", data = ZoneData[2] });
                    }
                    ZoneData[3].Add(new { idx = i, val = Value[0][3] });
                    data.Add(new { cname = "zone_ce1", data = ZoneData[3] });
                    ZoneData[4].Add(new { idx = i, val = Value[0][4] });
                    data.Add(new { cname = "zone_ce2", data = ZoneData[4] });
                    FormData[5].Add(new { idx = i, val = Num+ ". 냉방 에너지소요량 검토 보고서" }); //title
                    data.Add(new { cname = "title", data = FormData[5] });
                }

                Value = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct 개수_ahu, QCb_a_ahu,QC_Max_ahu, 공급설비1_ahu, 공급설비2_ahu From CoolingSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    ZahuData[0].Add(new { idx = i, val = Value[0][0] });
                    data.Add(new { cname = "zahu_count", data = ZahuData[0] });
                    if (double.TryParse(Value[0][1], out double result1))
                    {
                        ZahuData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][1]) });
                        data.Add(new { cname = "zahu_qcba", data = ZahuData[1] });
                    }
                    if (double.TryParse(Value[0][2], out double result2))
                    {
                        ZahuData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][2]) });
                        data.Add(new { cname = "zahu_qcmax", data = ZahuData[2] });
                    }
                    ZahuData[3].Add(new { idx = i, val = Value[0][3] });
                    data.Add(new { cname = "zahu_ce1", data = ZahuData[3] });
                    ZahuData[4].Add(new { idx = i, val = Value[0][4] });
                    data.Add(new { cname = "zahu_ce2", data = ZahuData[4] });
                }

                Value = Program.DB.querySQL(DB.type.ProjDB, "Select 열원설비,냉각탑 From CoolingSystem_Form Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    SourceData[0].Add(new { idx = i, val = Value[0][0] });
                    data.Add(new { cname = "heatsource", data = SourceData[0] });
                    if(Value[0][1]!="")
                    {
                        string[][] Value2 = Program.DB.querySQL(DB.type.ProjDB, "Select 냉각능력,입구온도,출구온도 From User_CoolingTop Where 번호='" + Value[0][1] + "'");
                        if (Value2.Length > 0)
                        {
                            if (double.TryParse(Value[0][1], out double result1))
                            {
                                SourceData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(Value2[0][0]) });
                                data.Add(new { cname = "top_power", data = SourceData[1] });
                            }
                            SourceData[2].Add(new { idx = i, val = Value[0][1] });
                            data.Add(new { cname = "top_in", data = SourceData[2] });
                            SourceData[3].Add(new { idx = i, val = Value[0][2] });
                            data.Add(new { cname = "top_out", data = SourceData[3] });
                        }
                    }
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct 개수_ahu, QCb_a_ahu,QC_Max_ahu, 공급설비1_ahu, 공급설비2_ahu From CoolingSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    ZahuData[0].Add(new { idx = i, val = Value[0][0] });
                    data.Add(new { cname = "zahu_count", data = ZahuData[0] });
                    if (double.TryParse(Value[0][1], out double result1))
                    {
                        ZahuData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][1]) });
                        data.Add(new { cname = "zahu_qcba", data = ZahuData[1] });
                    }
                    if (double.TryParse(Value[0][2], out double result2))
                    {
                        ZahuData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][2]) });
                        data.Add(new { cname = "zahu_qcmax", data = ZahuData[2] });
                    }
                    ZahuData[3].Add(new { idx = i, val = Value[0][3] });
                    data.Add(new { cname = "zahu_ce1", data = ZahuData[3] });
                    ZahuData[4].Add(new { idx = i, val = Value[0][4] });
                    data.Add(new { cname = "zahu_ce2", data = ZahuData[4] });
                }

                Value = Program.DB.querySQL(DB.type.ProjDB, "Select  sum(QC_nd),  sum(QC_ce),  sum(QC_d), sum(QC_s),  sum(QC_out), sum(QC_f),sum(W), Fuel From CoolingSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    AnnualData[0].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][0]) });
                    data.Add(new { cname = "annual_nd", data = AnnualData[0] });
                    AnnualData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][1]) });
                    data.Add(new { cname = "annual_ce", data = AnnualData[1] });
                    AnnualData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][2]) });
                    data.Add(new { cname = "annual_d", data = AnnualData[2] });
                    AnnualData[3].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][3]) });
                    data.Add(new { cname = "annual_s", data = AnnualData[3] });
                    AnnualData[4].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][4]) });
                    data.Add(new { cname = "annual_outg", data = AnnualData[4] });
                    AnnualData[5].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][5]) });
                    data.Add(new { cname = "annual_f", data = AnnualData[5] });
                    AnnualData[6].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][6]) });
                    data.Add(new { cname = "annual_w", data = AnnualData[6] });
                    double primary = 0, tco2 = 0; 
                    if(Value[0][7]=="전기")
                    {
                        primary = (Convert.ToDouble(Value[0][5]) + Convert.ToDouble(Value[0][6])) * 2.75;
                        tco2 = (Convert.ToDouble(Value[0][5]) + Convert.ToDouble(Value[0][6])) * 0.4747 / 1000000 * 1000;
                    }
                    else
                    {
                        primary = Convert.ToDouble(Value[0][5]) * 1.1 + Convert.ToDouble(Value[0][6]) *2.75;
                        tco2 = Convert.ToDouble(Value[0][5]) * 0.4747 / 1000000 * 1000 + Convert.ToDouble(Value[0][6]) / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    }
                    AnnualData[7].Add(new { idx = i, val = primary.ToString("#,##0") });
                    data.Add(new { cname = "annual_p", data = AnnualData[7] });
                    AnnualData[8].Add(new { idx = i, val = tco2.ToString("0.0") });
                    data.Add(new { cname = "annual_tco2", data = AnnualData[8] });
                }                
                
                for(int mth =0; mth < 12; mth++)
                {
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select  QC_out_z,  QC_ce_z,  QC_d_z, QC_s_z, QC_nd_z  From CoolingSystem_Result Where 번호='" + Num + "' and 월='" + mth + "월'");
                    if(Value.Length > 0)
                    {
                        ZoneMthData[0].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][0]) });
                        data.Add(new { cname = "zone_mth_outg", data = ZoneMthData[0] });
                        ZoneMthData[1].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][1]) });
                        data.Add(new { cname = "zone_mth_ce", data = ZoneMthData[1] });
                        ZoneMthData[2].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][2]) });
                        data.Add(new { cname = "zone_mth_d", data = ZoneMthData[2] });
                        ZoneMthData[3].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][3]) });
                        data.Add(new { cname = "zone_mth_s", data = ZoneMthData[3] });
                        ZoneMthData[4].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][4]) });
                        data.Add(new { cname = "zone_mth_nd", data = ZoneMthData[4] });
                    }                    
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select  sum(QC_out_z),   sum(QC_ce_z),   sum(QC_d_z),  sum(QC_s_z),  sum(QC_nd_z)  From CoolingSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    ZoneMthData[5].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][0]) });
                    data.Add(new { cname = "zone_mth_outg", data = ZoneMthData[5] });
                    ZoneMthData[6].Add(new { idx = i * 13 + 12 , val = Program.UTIL.doubleComa(Value[0][1]) });
                    data.Add(new { cname = "zone_mth_ce", data = ZoneMthData[6] });
                    ZoneMthData[7].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][2]) });
                    data.Add(new { cname = "zone_mth_d", data = ZoneMthData[7] });
                    ZoneMthData[8].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][3]) });
                    data.Add(new { cname = "zone_mth_s", data = ZoneMthData[8] });
                    ZoneMthData[9].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][4]) });
                    data.Add(new { cname = "zone_mth_nd", data = ZoneMthData[9] });
                }

                for (int mth = 0; mth < 12; mth++)
                {
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select  QC_out_ahu,  QC_ce_ahu,  QC_d_ahu, QC_s_ahu, QC_nd_ahu  From CoolingSystem_Result Where 번호='" + Num + "' and 월='" + mth + "월'");
                    if (Value.Length > 0)
                    {
                        ZahuMthData[0].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][0]) });
                        data.Add(new { cname = "zahu_mth_outg", data = ZahuMthData[0] });
                        ZahuMthData[1].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][1]) });
                        data.Add(new { cname = "zahu_mth_ce", data = ZahuMthData[1] });
                        ZahuMthData[2].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][2]) });
                        data.Add(new { cname = "zahu_mth_d", data = ZahuMthData[2] });
                        ZahuMthData[3].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][3]) });
                        data.Add(new { cname = "zahu_mth_s", data = ZahuMthData[3] });
                        ZahuMthData[4].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][4]) });
                        data.Add(new { cname = "zahu_mth_nd", data = ZahuMthData[4] });
                    }
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select  sum(QC_out_ahu),   sum(QC_ce_ahu),   sum(QC_d_ahu),  sum(QC_s_ahu),  sum(QC_nd_ahu)  From CoolingSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    ZahuMthData[5].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][0]) });
                    data.Add(new { cname = "zahu_mth_outg", data = ZahuMthData[5] });
                    ZahuMthData[6].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][1]) });
                    data.Add(new { cname = "zahu_mth_ce", data = ZahuMthData[6] });
                    ZahuMthData[7].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][2]) });
                    data.Add(new { cname = "zahu_mth_d", data = ZahuMthData[7] });
                    ZahuMthData[8].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][3]) });
                    data.Add(new { cname = "zahu_mth_s", data = ZahuMthData[8] });
                    ZahuMthData[9].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][4]) });
                    data.Add(new { cname = "zahu_mth_nd", data = ZahuMthData[9] });
                }

            }
            s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
            s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());

            string s3 = "";
            Debug.Print("start");
            runScript("init(" + s + "," + s2 + "," + "[" + s3 + "])");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webView21.CoreWebView2.ShowPrintUI();
        }

    }
}