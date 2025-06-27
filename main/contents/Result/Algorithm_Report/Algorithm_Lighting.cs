using Microsoft.Web.WebView2.Core;
using System.Diagnostics;

namespace main.contents.Result
{
    public partial class Algorithm_Lighting : Form
    {
        bool scriptable = false;
        public Algorithm_Lighting()
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
            string charts = "";
            string[][] 번호 = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "번호", "");
            List<object> items = new List<object>();
            List<object> data = new List<object>();
            List<object>[] zoneData = new List<object>[30]; //존정보
            List<object>[] daylightData = new List<object>[30]; //주광정보
            List<object>[] lightData = new List<object>[30]; //인공조명
            List<object>[] AnnualData = new List<object>[30]; //연간정보
            List<object>[] renewData = new List<object>[30]; //집광채광
            List<object>[] dData = new List<object>[30]; //자연채광
            List<object>[] zoneMthData = new List<object>[100]; //자연채광월정보
            List<object>[] renewMthData = new List<object>[100]; //집광채광 월정보
            List<object>[] MthData = new List<object>[100]; //조명에너지
            List<object>[] FormData = new List<object>[30];

            List<string> chart_nd = new List<string>();
            List<string> chart_ce = new List<string>();
            List<string> chart_d = new List<string>();
            List<string> chart_s = new List<string>();
            List<string> chart_f = new List<string>();
            int i = -1;
            while (++i < 30)
            {
                zoneData[i] = new List<object>(); //존정보
                daylightData[i] = new List<object>(); //주광정보
                lightData[i] = new List<object>(); //인공조명
                AnnualData[i] = new List<object>(); //연간 결과정보
                renewData[i] = new List<object>(); //집광채광
                dData[i] = new List<object>(); //자연채광
                FormData[i] = new List<object>();//이미지정보
            }
            i = -1;
            while (++i < 100)
            {
                zoneMthData[i] = new List<object>();
                renewMthData[i] = new List<object>();
                MthData[i] = new List<object>();
            }

            i = -1;
            while (++i < 번호.Length)
            {
                string Num = 번호[i][0];
                items.Add("lighting_report.html"); // 예시 코드: 메인 메뉴 동적 할당, 공간계수는 조명설치높이임
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "번호,기준조도,너비,길이,순바닥면적,상인방높이,작업면높이,공간계수", "번호 = '" + Num + "'");
                double k = 0, Wr = 0, Lr = 0, hm = 0;

                string titleName = Num + " 조명에너지소요량 검토보고서";
                zoneData[0].Add(new { idx = i, val = titleName }); //명칭
                if (Value.Length > 0)
                {
                    //공간계수 구하기
                    Wr = Convert.ToDouble(Value[0][2].ToString());
                    Lr = Convert.ToDouble(Value[0][3].ToString());
                    hm = Convert.ToDouble(Value[0][7].ToString());
                    k = Lr * Wr / (hm * (Lr + Wr));
                }
                string[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존이름,용도프로필,시작시간,종료시간,주이용일,연이용일수,천장고", "존번호 = '" + Num + "'");
                if (Value2.Length > 0)
                {
                    zoneData[1].Add(new { idx = i, val = Value2[0][0] }); //존명칭
                    zoneData[2].Add(new { idx = i, val = Value2[0][1] }); //용도프로필
                    zoneData[3].Add(new { idx = i, val = Value[0][1] }); //조도
                    zoneData[4].Add(new { idx = i, val = Value2[0][2] }); //사용시작시간
                    zoneData[5].Add(new { idx = i, val = Value2[0][3] }); //사용종료시간
                    zoneData[6].Add(new { idx = i, val = Value2[0][4] }); //주간이용일수
                    zoneData[7].Add(new { idx = i, val = Value2[0][5] }); //연간이용일수
                    zoneData[8].Add(new { idx = i, val = string.Format("{0:N2}", Lr) }); //파사드길이
                    zoneData[9].Add(new { idx = i, val = string.Format("{0:N2}", k) }); //공간계수
                    zoneData[10].Add(new { idx = i, val = string.Format("{0:N2}", Convert.ToDouble(Value[0][4].ToString())) });  //바닥 면적
                    zoneData[11].Add(new { idx = i, val = string.Format("{0:N2}", Convert.ToDouble(Value[0][5].ToString())) }); //상인방 높이
                    zoneData[12].Add(new { idx = i, val = string.Format("{0:N2}", Convert.ToDouble(Value[0][6].ToString())) }); //작업면 높이
                    zoneData[13].Add(new { idx = i, val = string.Format("{0:N2}", Convert.ToDouble(Value2[0][6].ToString())) }); //천장 높이
                    zoneData[14].Add(new { idx = i, val = string.Format("{0:N2}", Convert.ToDouble(Value[0][7].ToString())) }); //조명 설치 높이
                }

                string[][] daylight = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_Form", "주광길이,주광깊이,주광면적,비주광면적,디밍유형,기준조도", "번호 = '" + Num + "'");
                string[][] daylight2 = Program.DB.getValue(DB.type.ProjDB, "Zone_LightResult", "f_dclass,f_DCA", "번호 = '" + Num + "'");//주광등급,주광율
                string[][] daylight3 = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_주광제어계수", "주광제어계수", "디밍유형 = '" + daylight[0][4].ToString() + "' And 주광이용가능성 = '" + daylight2[0][0].ToString() + "' And Em = '" + daylight[0][5].ToString() + "'");
                double 주광율 = 0;
                if (daylight2.Length > 0)
                {
                    for (int h = 0; h < 12; h++)
                    {
                        주광율 += Convert.ToDouble(daylight2[h][1].ToString());
                    }
                    주광율 = 주광율 / 12;
                }
                string 주광제어계수;
                if (daylight3.Length > 0)
                {
                    주광제어계수 = daylight3[0][0];
                }
                else 주광제어계수 = "해당없음";

                daylightData[0].Add(new { idx = i, val = Program.UTIL.doubleComa(daylight[0][0].ToString(), 2) });//주광길이
                daylightData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(daylight[0][1].ToString(), 2) } ); //주광깊이
                daylightData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(daylight[0][2].ToString(), 2) });//주광면적
                daylightData[3].Add(new { idx = i, val = Program.UTIL.doubleComa(daylight[0][3].ToString(), 2) });//비주광면적
                daylightData[4].Add(new { idx = i, val = daylight2[0][0] }); //주광등급
                daylightData[5].Add(new { idx = i, val = 주광제어계수 }); //주광제어계수
                daylightData[6].Add(new { idx = i, val = string.Format("{0:N2}", 주광율) }); //주광율



                //FormData[1].Add(new { idx = i, val = Num }); //그림번호
                //FormData[2].Add(new { idx = i, val = Num }); //번호
                //Value = Program.DB.querySQL(DB.type.ProjDB, "Select 명칭,냉방설비,냉방출력,냉방성능,압축기,제어유형,외기냉방시스템,설치대수  From CoolingSystem_Form Where 번호='" + Num + "'");
                //if (Value.Length > 0)
                //{
                //    FormData[3].Add(new { idx = i, val = Value[0][0] });
                //    FormData[4].Add(new { idx = i, val = Value[0][1] });
                //            if (double.TryParse(Value[0][2], out double result1))
                //            {
                //                FormData[5].Add(new { idx = i, val = Convert.ToDouble(Value[0][2]).ToString("0.0") });
                //            }
                //            if (double.TryParse(Value[0][3], out double result2))
                //            {
                //                FormData[6].Add(new { idx = i, val = Convert.ToDouble(Value[0][3]).ToString("0.0") });
                //            }
                //            FormData[7].Add(new { idx = i, val = Value[0][4] });
                //            FormData[8].Add(new { idx = i, val = Value[0][5] });
                //            FormData[9].Add(new { idx = i, val = Value[0][6] });
                //            FormData[10].Add(new { idx = i, val = Value[0][7] });
                //        }
                //        Value = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct 개수_z, QCb_a_z,QC_Max_z, 공급설비1_z, 공급설비2_z From CoolingSystem_Result Where 번호='" + Num + "'");
                //        if (Value.Length > 0)
                //        {
                //            ZoneData[0].Add(new { idx = i, val = Value[0][0] });
                //            if (double.TryParse(Value[0][1], out double result1))
                //            {
                //                ZoneData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                //            }
                //            if (double.TryParse(Value[0][2], out double result2))
                //            {
                //                ZoneData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                //            }
                //            ZoneData[3].Add(new { idx = i, val = Value[0][3] });
                //            ZoneData[4].Add(new { idx = i, val = Value[0][4] });
                //            ZoneData[5].Add(new { idx = i, val = Num + ". 냉방 에너지소요량 검토 보고서" }); //title
                //            //면적 및 zone 개수 배열 작성
                //        }

                //        Value = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct 개수_ahu, QCb_a_ahu,QC_Max_ahu, 공급설비1_ahu, 공급설비2_ahu From CoolingSystem_Result Where 번호='" + Num + "'");
                //        if (Value.Length > 0)
                //        {
                //            ZahuData[0].Add(new { idx = i, val = Value[0][0] });
                //            if (double.TryParse(Value[0][1], out double result1))
                //            {
                //                ZahuData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                //            }
                //            if (double.TryParse(Value[0][2], out double result2))
                //            {
                //                ZahuData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                //            }
                //            ZahuData[3].Add(new { idx = i, val = Value[0][3] });
                //            ZahuData[4].Add(new { idx = i, val = Value[0][4] });
                //        }
                //        else
                //        {
                //            ZahuData[0].Add(new { idx = i, val = "-" });
                //            ZahuData[1].Add(new { idx = i, val = "-" });
                //            ZahuData[2].Add(new { idx = i, val = "-" });
                //            ZahuData[3].Add(new { idx = i, val = "-" });
                //            ZahuData[4].Add(new { idx = i, val = "-" });
                //        }
                //        List<string> zonelist = new List<string>();
                //        List<string> zahulist = new List<string>();
                //        double totArea = 0;
                //        string[][] checkarea = Program.DB.querySQL(DB.type.ProjDB, "Select 공급존, 공급AHU, 냉수펌프1, 냉수펌프2 From CoolingSystem_Form Where 번호='" + Num + "'");
                //        if (checkarea.Length > 0)
                //        {
                //            if (checkarea[0][0] != "" && checkarea[0][0] != null)
                //            {
                //                Split(checkarea[0][0], zonelist);
                //            }
                //            if (checkarea[0][1] != "" && checkarea[0][1] != null)
                //            {
                //                Split(checkarea[0][1], zahulist);
                //            }
                //        }
                //        //공조결과값에서 존을 찾기
                //        if (zahulist.Count > 0)
                //        {
                //            string[][] zonecheck = Program.DB.querySQL(DB.type.ProjDB, "Select 공급존, 공급AHU, 냉수펌프1, 냉수펌프2 From CoolingSystem_Form Where 번호='" + Num + "'");
                //        }









                //        Value = Program.DB.querySQL(DB.type.ProjDB, "Select 열원설비,냉각탑 From CoolingSystem_Form Where 번호='" + Num + "'");
                //        if (Value.Length > 0)
                //        {
                //            SourceData[0].Add(new { idx = i, val = Value[0][0] });
                //            if (Value[0][1] != "")
                //            {
                //                string[][] Value2 = Program.DB.querySQL(DB.type.ProjDB, "Select 냉각능력,입구온도,출구온도 From User_CoolingTop Where 번호='" + Value[0][1] + "'");
                //                if (Value2.Length > 0)
                //                {
                //                    if (double.TryParse(Value2[0][0], out double result1))
                //                    {
                //                        SourceData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(Value2[0][0], 0) });
                //                    }
                //                    SourceData[2].Add(new { idx = i, val = Value2[0][1] });
                //                    SourceData[3].Add(new { idx = i, val = Value2[0][2] });
                //                }
                //            }
                //        }
                //        Value = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct 개수_ahu, QCb_a_ahu,QC_Max_ahu, 공급설비1_ahu, 공급설비2_ahu From CoolingSystem_Result Where 번호='" + Num + "'");
                //        if (Value.Length > 0)
                //        {
                //            ZahuData[0].Add(new { idx = i, val = Value[0][0] });
                //            if (double.TryParse(Value[0][1], out double result1))
                //            {
                //                ZahuData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                //            }
                //            if (double.TryParse(Value[0][2], out double result2))
                //            {
                //                ZahuData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                //            }
                //            ZahuData[3].Add(new { idx = i, val = Value[0][3] });
                //            ZahuData[4].Add(new { idx = i, val = Value[0][4] });
                //        }

                //        Value = Program.DB.querySQL(DB.type.ProjDB, "Select  sum(QC_nd),  sum(QC_ce),  sum(QC_d), sum(QC_s),  sum(QC_out), sum(QC_f),sum(W), Fuel From CoolingSystem_Result Where 번호='" + Num + "'");
                //        if (Value.Length > 0)
                //        {
                //            AnnualData[0].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                //            AnnualData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                //            AnnualData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                //            AnnualData[3].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                //            AnnualData[4].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                //            AnnualData[5].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][5], 0) });
                //            AnnualData[6].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][6], 0) });
                //            double primary = 0, tco2 = 0;
                //            if (Value[0][7] == "전기")
                //            {
                //                primary = (Convert.ToDouble(Value[0][5]) + Convert.ToDouble(Value[0][6])) * 2.75;
                //                tco2 = (Convert.ToDouble(Value[0][5]) + Convert.ToDouble(Value[0][6])) * 0.4747 / 1000000 * 1000;
                //            }
                //            else
                //            {
                //                primary = Convert.ToDouble(Value[0][5]) * 1.1 + Convert.ToDouble(Value[0][6]) * 2.75;
                //                tco2 = Convert.ToDouble(Value[0][5]) * 0.4747 / 1000000 * 1000 + Convert.ToDouble(Value[0][6]) / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                //            }
                //            AnnualData[7].Add(new { idx = i, val = primary.ToString("#,##0") });
                //            AnnualData[8].Add(new { idx = i, val = tco2.ToString("0.0") });
                //        }

                //        for (int mth = 0; mth < 12; mth++)
                //        {
                //            Value = Program.DB.querySQL(DB.type.ProjDB, "Select  QC_out_z,  QC_ce_z,  QC_d_z, QC_s_z, QC_nd_z  From CoolingSystem_Result Where 번호='" + Num + "' and 월='" + (mth + 1) + "월'");
                //            if (Value.Length > 0)
                //            {
                //                ZoneMthData[0].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                //                ZoneMthData[1].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                //                ZoneMthData[2].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                //                ZoneMthData[3].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                //                ZoneMthData[4].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                //            }
                //        }
                //        Value = Program.DB.querySQL(DB.type.ProjDB, "Select  sum(QC_out_z),   sum(QC_ce_z),   sum(QC_d_z),  sum(QC_s_z),  sum(QC_nd_z)  From CoolingSystem_Result Where 번호='" + Num + "'");
                //        if (Value.Length > 0)
                //        {
                //            ZoneMthData[0].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                //            ZoneMthData[1].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                //            ZoneMthData[2].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                //            ZoneMthData[3].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                //            ZoneMthData[4].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                //        }

                //        for (int mth = 0; mth < 12; mth++)
                //        {
                //            Value = Program.DB.querySQL(DB.type.ProjDB, "Select  QC_out_ahu,  QC_ce_ahu,  QC_d_ahu, QC_s_ahu, QC_nd_ahu  From CoolingSystem_Result Where 번호='" + Num + "' and 월='" + (mth + 1) + "월'");
                //            if (Value.Length > 0)
                //            {
                //                ZahuMthData[0].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                //                ZahuMthData[1].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                //                ZahuMthData[2].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                //                ZahuMthData[3].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                //                ZahuMthData[4].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                //            }
                //        }
                //        Value = Program.DB.querySQL(DB.type.ProjDB, "Select  sum(QC_out_ahu),   sum(QC_ce_ahu),   sum(QC_d_ahu),  sum(QC_s_ahu),  sum(QC_nd_ahu)  From CoolingSystem_Result Where 번호='" + Num + "'");
                //        if (Value.Length > 0)
                //        {
                //            ZahuMthData[0].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                //            ZahuMthData[1].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                //            ZahuMthData[2].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                //            ZahuMthData[3].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                //            ZahuMthData[4].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                //        }

                //        for (int mth = 0; mth < 12; mth++)
                //        {
                //            Value = Program.DB.querySQL(DB.type.ProjDB, "Select  QC_f,  SEER_c,  EER_c, QC_out, QC_ce, QC_d, QC_s From CoolingSystem_Result Where 번호='" + Num + "' and 월='" + (mth + 1) + "월'");
                //            if (Value.Length > 0)
                //            {
                //                MthData[0].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                //                MthData[1].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][1], 1) });
                //                MthData[2].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][2], 1) });
                //                MthData[3].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                //                MthData[4].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                //                MthData[5].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][5], 0) });
                //                MthData[6].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][6], 0) });
                //                MthData[7].Add(new { idx = i * 13 + mth, val = 0 });
                //            }
                //        }
                //        Value = Program.DB.querySQL(DB.type.ProjDB, "Select  Sum(QC_f),  AVG(SEER_c),  AVG(EER_c), Sum(QC_out), sum(QC_ce), sum(QC_d), sum(QC_s)  From CoolingSystem_Result Where 번호='" + Num + "'");
                //        if (Value.Length > 0)
                //        {
                //            MthData[0].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                //            MthData[1].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][1], 1) });
                //            MthData[2].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][2], 1) });
                //            MthData[3].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                //            MthData[4].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                //            MthData[5].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][5], 0) });
                //            MthData[6].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][6], 0) });
                //            MthData[7].Add(new { idx = i * 13 + 12, val = 0 });
                //        }

                //        for (int mth = 0; mth < 12; mth++)
                //        {
                //            Value = Program.DB.querySQL(DB.type.ProjDB, "Select W, W_ce, W_d, W_s, W_g From CoolingSystem_Result Where 번호='" + Num + "' and 월='" + (mth + 1) + "월'");
                //            if (Value.Length > 0)
                //            {
                //                WMthData[0].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                //                WMthData[1].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                //                WMthData[2].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                //                WMthData[3].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                //                WMthData[4].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                //            }
                //        }
                //        Value = Program.DB.querySQL(DB.type.ProjDB, "Select  sum(W), sum(W_ce), sum(W_d), sum(W_s), sum(W_g)  From CoolingSystem_Result Where 번호='" + Num + "'");
                //        if (Value.Length > 0)
                //        {
                //            WMthData[0].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                //            WMthData[1].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                //            WMthData[2].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                //            WMthData[3].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                //            WMthData[4].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                //        }

                data.Add(new { cname = "title", data = zoneData[0] });
                data.Add(new { cname = "zone_num", data = zoneData[1] });
                data.Add(new { cname = "zone_profile", data = zoneData[2] });
                data.Add(new { cname = "zone_lux", data = zoneData[3] });
                data.Add(new { cname = "zone_starttime", data = zoneData[4] });
                data.Add(new { cname = "zone_endtime", data = zoneData[5] });
                data.Add(new { cname = "zone_weekuseday", data = zoneData[6] });
                data.Add(new { cname = "zone_yearuseday", data = zoneData[7] });
                data.Add(new { cname = "zone_length", data = zoneData[8] });
                data.Add(new { cname = "zone_sfactor", data = zoneData[9] });
                data.Add(new { cname = "zone_area", data = zoneData[10] });
                data.Add(new { cname = "zone_wintopheight", data = zoneData[11] });
                data.Add(new { cname = "zone_workheight", data = zoneData[12] });
                data.Add(new { cname = "zone_cellingheight", data = zoneData[13] });
                data.Add(new { cname = "zone_lightheight", data = zoneData[14] });

                data.Add(new { cname = "daylight_length", data = daylightData[0] });
                data.Add(new { cname = "daylight_depth", data = daylightData[1] });
                data.Add(new { cname = "daylight_area", data = daylightData[2] });
                data.Add(new { cname = "light_area", data = daylightData[3] });
                data.Add(new { cname = "daylight_class", data = daylightData[4] });
                data.Add(new { cname = "daylight_controalrate", data = daylightData[5] });
                data.Add(new { cname = "daylight_userate", data = daylightData[6] });

                //        data.Add(new { cname = "zahu_count", data = ZahuData[0] });
                //        data.Add(new { cname = "zahu_qcba", data = ZahuData[1] });
                //        data.Add(new { cname = "zahu_qcmax", data = ZahuData[2] });
                //        data.Add(new { cname = "zahu_ce1", data = ZahuData[3] });
                //        data.Add(new { cname = "zahu_ce2", data = ZahuData[4] });


                //        data.Add(new { cname = "heatsource", data = SourceData[0] });
                //        data.Add(new { cname = "top_power", data = SourceData[1] });
                //        data.Add(new { cname = "top_in", data = SourceData[2] });
                //        data.Add(new { cname = "top_out", data = SourceData[3] });


                //        data.Add(new { cname = "annual_nd", data = AnnualData[0] });
                //        data.Add(new { cname = "annual_ce", data = AnnualData[1] });
                //        data.Add(new { cname = "annual_d", data = AnnualData[2] });
                //        data.Add(new { cname = "annual_s", data = AnnualData[3] });
                //        data.Add(new { cname = "annual_outg", data = AnnualData[4] });
                //        data.Add(new { cname = "annual_f", data = AnnualData[5] });
                //        data.Add(new { cname = "annual_w", data = AnnualData[6] });
                //        data.Add(new { cname = "annual_p", data = AnnualData[7] });
                //        data.Add(new { cname = "annual_tco2", data = AnnualData[8] });


                //        data.Add(new { cname = "zone_mth_outg", data = ZoneMthData[0] });
                //        data.Add(new { cname = "zone_mth_ce", data = ZoneMthData[1] });
                //        data.Add(new { cname = "zone_mth_d", data = ZoneMthData[2] });
                //        data.Add(new { cname = "zone_mth_s", data = ZoneMthData[3] });
                //        data.Add(new { cname = "zone_mth_nd", data = ZoneMthData[4] });

                //        //data.Add(new { cname = "zone_mth_outg", data = ZoneMthData[5] });
                //        //data.Add(new { cname = "zone_mth_ce", data = ZoneMthData[6] });
                //        //data.Add(new { cname = "zone_mth_d", data = ZoneMthData[7] });
                //        //data.Add(new { cname = "zone_mth_s", data = ZoneMthData[8] });
                //        //data.Add(new { cname = "zone_mth_nd", data = ZoneMthData[9] });

                //        data.Add(new { cname = "zahu_mth_outg", data = ZahuMthData[0] });
                //        data.Add(new { cname = "zahu_mth_ce", data = ZahuMthData[1] });
                //        data.Add(new { cname = "zahu_mth_d", data = ZahuMthData[2] });
                //        data.Add(new { cname = "zahu_mth_s", data = ZahuMthData[3] });
                //        data.Add(new { cname = "zahu_mth_nd", data = ZahuMthData[4] });

                //        //data.Add(new { cname = "zahu_mth_outg", data = ZahuMthData[5] });
                //        //data.Add(new { cname = "zahu_mth_ce", data = ZahuMthData[6] });
                //        //data.Add(new { cname = "zahu_mth_d", data = ZahuMthData[7] });
                //        //data.Add(new { cname = "zahu_mth_s", data = ZahuMthData[8] });
                //        //data.Add(new { cname = "zahu_mth_nd", data = ZahuMthData[9] });

                //        data.Add(new { cname = "mth_f", data = MthData[0] });
                //        data.Add(new { cname = "mth_seer", data = MthData[1] });
                //        data.Add(new { cname = "mth_eer", data = MthData[2] });
                //        data.Add(new { cname = "mth_outg", data = MthData[3] });
                //        data.Add(new { cname = "mth_ce", data = MthData[4] });
                //        data.Add(new { cname = "mth_d", data = MthData[5] });
                //        data.Add(new { cname = "mth_s", data = MthData[6] });
                //        data.Add(new { cname = "mth_g", data = MthData[7] });

                //        //data.Add(new { cname = "mth_f", data = MthData[8] });
                //        //data.Add(new { cname = "mth_seer", data = MthData[9] });
                //        //data.Add(new { cname = "mth_eer", data = MthData[10] });
                //        //data.Add(new { cname = "mth_outg", data = MthData[11] });
                //        //data.Add(new { cname = "mth_ce", data = MthData[12] });
                //        //data.Add(new { cname = "mth_d", data = MthData[13] });
                //        //data.Add(new { cname = "mth_s", data = MthData[14] });
                //        //data.Add(new { cname = "mth_g", data = MthData[15] });

                //        data.Add(new { cname = "w", data = WMthData[0] });
                //        data.Add(new { cname = "w_ce", data = WMthData[1] });
                //        data.Add(new { cname = "w_d", data = WMthData[2] });
                //        data.Add(new { cname = "w_s", data = WMthData[3] });
                //        data.Add(new { cname = "w_g", data = WMthData[4] });

                //        //data.Add(new { cname = "w", data = WMthData[5] });
                //        //data.Add(new { cname = "w_ce", data = WMthData[6] });
                //        //data.Add(new { cname = "w_d", data = WMthData[7] });
                //        //data.Add(new { cname = "w_s", data = WMthData[8] });
                //        //data.Add(new { cname = "w_g", data = WMthData[9] });


                //        List<object> nd_chart = new List<object>();
                //        List<object> ce_chart = new List<object>();
                //        List<object> d_chart = new List<object>();
                //        List<object> s_chart = new List<object>();
                //        List<object> f_chart = new List<object>();
                //        for (int mth = 0; mth < 12; mth++)
                //        {
                //            Value = Program.DB.querySQL(DB.type.ProjDB, "Select QC_nd, QC_ce, QC_d, QC_s, QC_f From CoolingSystem_Result Where 번호='" + Num + "' and 월='" + (mth + 1) + "월'");
                //            if (Value.Length > 0)
                //            {
                //                nd_chart.Add(Convert.ToDouble(Program.UTIL.doubleComa(Value[0][0], 0)));
                //                ce_chart.Add(Convert.ToDouble(Program.UTIL.doubleComa(Value[0][1], 0)));
                //                d_chart.Add(Convert.ToDouble(Program.UTIL.doubleComa(Value[0][2], 0)));
                //                s_chart.Add(Convert.ToDouble(Program.UTIL.doubleComa(Value[0][3], 0)));
                //                f_chart.Add(Convert.ToDouble(Program.UTIL.doubleComa(Value[0][4], 0)));
                //            }
                //        }
                //        chart_nd.Add(System.Text.Json.JsonSerializer.Serialize(nd_chart.ToArray()));
                //        chart_ce.Add(System.Text.Json.JsonSerializer.Serialize(ce_chart.ToArray()));
                //        chart_d.Add(System.Text.Json.JsonSerializer.Serialize(d_chart.ToArray()));
                //        chart_s.Add(System.Text.Json.JsonSerializer.Serialize(s_chart.ToArray()));
                //        chart_f.Add(System.Text.Json.JsonSerializer.Serialize(f_chart.ToArray()));
                //        double max = 0;
                //        Value = Program.DB.querySQL(DB.type.ProjDB, "Select QC_out From CoolingSystem_Result Where 번호='" + Num + "'");
                //        if (Value.Length > 0)
                //        {
                //            max = Convert.ToDouble(Value[0][0]);
                //            for (int a = 1; a < Value.Length; a++)
                //            {
                //                if (max < Convert.ToDouble(Value[a][0]))
                //                {
                //                    max = Convert.ToDouble(Value[a][0]);
                //                }
                //            }
                //        }
                //        int n = ((int)max).ToString().Length;
                //        max = Convert.ToDouble(String.Format("{0:F0}", max / Math.Pow(10, n - 1))) * Math.Pow(10, n - 1) + Math.Pow(10, n - 1);
                //        if (charts != "") charts += ",";
                //        charts += "{data:[" +
                //        "{type:\"bar\",barPercentage:0.4,label:\"에너지요구량 [kWh]\",data:" + chart_nd[i] + ",borderColor:\"#A9D18E\",backgroundColor:\"#A9D18E\",dash:false}," +
                //        "{type:\"bar\",barPercentage:0.4,label:\"공급열손실 [kWh]\",data:" + chart_ce[i] + ",borderColor:\"#70AD47\",backgroundColor:\"#70AD47\",dash:false}," +
                //        "{type:\"bar\",barPercentage:0.4,label:\"분배열손실 [kWh]\",data:" + chart_d[i] + ",borderColor:\"#FFD966\",backgroundColor:\"#FFD966\",dash:false}," +
                //        "{type:\"bar\",barPercentage:0.4,label:\"분배열손실 [kWh]\",data:" + chart_d[i] + ",borderColor:\"#FFD966\",backgroundColor:\"#FFD966\",dash:false}," +
                //        "{type:\"bar\",barPercentage:0.4,label:\"저장열손실 [kWh]\",data:" + chart_s[i] + ",borderColor:\"#9DC3E6\",backgroundColor:\"#9DC3E6\",dash:false}," +
                //        "{type:\"line\",yAxisID: 'y',label:\"에너지소요량 [kWh]\",data:" + chart_f[i] + ",borderColor:\"#ED7D31\",backgroundColor:\"#ED7D31\",dash:false, tension: 0.4}," +
                //        "],max:" + max.ToString() + ",step:100,legend:true,stacked:true}";


                s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                    s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());

                    Debug.Print("start");

                    runScript("init(" + s + "," + s2 + "," + "[" + charts + "])");
                
            }
        }
    }
}

