using Microsoft.Web.WebView2.Core;
using System.Diagnostics;
using System.Diagnostics.Contracts;

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

        private void Split(string nonSplit, List<string> type)
        {
            type.Clear();
            if (nonSplit != null)
            {
                string[] token = nonSplit.Split('+');
                foreach (string item in token)
                {
                    string _item = item.Trim();
                    type.Add(_item);
                }
            }
        }

        public void LoadData(string ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            string s, s2;
            string charts = "";
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
            List<object>[] WMthData = new List<object>[100];
            List<string> chart_nd = new List<string>();
            List<string> chart_ce = new List<string>();
            List<string> chart_d = new List<string>();
            List<string> chart_s = new List<string>();
            List<string> chart_f = new List<string>();

            List<object>[] AuxData = new List<object>[30];
            int i = -1;
            while (++i < 30)
            {
                FormData[i] = new List<object>();
                ZoneData[i] = new List<object>();
                ZahuData[i] = new List<object>();
                SourceData[i] = new List<object>();
                AnnualData[i] = new List<object>();
                AuxData[i] = new List<object>();
            }
            i = -1;
            while (++i < 100)
            {
                ZoneMthData[i] = new List<object>();
                ZahuMthData[i] = new List<object>();
                MthData[i] = new List<object>();
                WMthData[i] = new List<object>();
            }

            i = -1;
            while (++i < 번호.Length)
            {
                string Num = 번호[i][0];
                items.Add("CoolingReport_new.html"); // 예시 코드: 메인 메뉴 동적 할당
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호");
                if (Value.Length > 0)
                {
                    FormData[0].Add(new { idx = i, val = Value[0][0] }); //프로젝트번호
                }
                FormData[1].Add(new { idx = i, val = Num }); //그림번호
                FormData[2].Add(new { idx = i, val = Num }); //번호
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select 명칭,냉방설비,냉방출력,냉방성능,압축기,제어유형,외기냉방시스템,설치대수  From CoolingSystem_Form Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    FormData[3].Add(new { idx = i, val = Value[0][0] });
                    FormData[4].Add(new { idx = i, val = Value[0][1] });
                    if (double.TryParse(Value[0][2], out double result1))
                    {
                        FormData[5].Add(new { idx = i, val = Program.UTIL.ToDoubleOrZero(Value[0][2]).ToString("0.0") });
                    }
                    if (double.TryParse(Value[0][3], out double result2))
                    {
                        FormData[6].Add(new { idx = i, val = Program.UTIL.ToDoubleOrZero(Value[0][3]).ToString("0.0") });
                    }
                    FormData[7].Add(new { idx = i, val = Value[0][4] });
                    FormData[8].Add(new { idx = i, val = Value[0][5] });
                    FormData[9].Add(new { idx = i, val = Value[0][6] });
                    FormData[10].Add(new { idx = i, val = Value[0][7] });
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct 개수_z, QCb_a_z,QC_Max_z, 공급설비1_z, 공급설비2_z From CoolingSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    ZoneData[0].Add(new { idx = i, val = Value[0][0] });
                    if (double.TryParse(Value[0][1], out double result1))
                    {
                        ZoneData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][1], 0)});
                    }
                    if (double.TryParse(Value[0][2], out double result2))
                    {
                        ZoneData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                    }
                    ZoneData[3].Add(new { idx = i, val = Value[0][3] });
                    ZoneData[4].Add(new { idx = i, val = Value[0][4] });
                    ZoneData[5].Add(new { idx = i, val = Num+ ". 냉방 에너지소요량 검토 보고서" }); //title
                   
                    //면적 및 zone 개수 배열 작성
                }

                Value = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct 개수_ahu, QCb_a_ahu,QC_Max_ahu, 공급설비1_ahu, 공급설비2_ahu From CoolingSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    ZahuData[0].Add(new { idx = i, val = Value[0][0] });
                    if (double.TryParse(Value[0][1], out double result1))
                    {
                        ZahuData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                    }
                    if (double.TryParse(Value[0][2], out double result2))
                    {
                        ZahuData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                    }
                    ZahuData[3].Add(new { idx = i, val = Value[0][3] });
                    ZahuData[4].Add(new { idx = i, val = Value[0][4] });
                }
                else
                {
                    ZahuData[0].Add(new { idx = i, val = "-" });
                    ZahuData[1].Add(new { idx = i, val = "-" });
                    ZahuData[2].Add(new { idx = i, val = "-" });
                    ZahuData[3].Add(new { idx = i, val = "-" });
                    ZahuData[4].Add(new { idx = i, val = "-" });
                }
                
                //존면적 계산하기                
                List<string> zonelist = new List<string>(); //존리스트
                List<string> zahulist = new List<string>(); //공조기리스트
                List<string> zoneAhulist = new List<string>(); //공조기있는 존리스트
                List<string> totallist = new List<string>();
                double zoneArea=0, zahuArea=0, totalArea = 0;
                string[][] checkarea = Program.DB.querySQL(DB.type.ProjDB, "Select 공급존, 공급AHU, 냉수펌프1, 냉수펌프2 From CoolingSystem_Form Where 번호='" + Num + "'");
                if (checkarea.Length > 0)
                {
                    if (checkarea[0][0] != "" && checkarea[0][0] != null)
                    {
                        Split(checkarea[0][0], zonelist);
                    }
                    if (checkarea[0][1] != "" && checkarea[0][1] != null)
                    {
                        Split(checkarea[0][1], zahulist);
                    }
                }
                
                //1.냉방존 면적 찾기
                if(zonelist.Count > 0)
                {
                    foreach (string k in zonelist)
                    {
                        string[][] zonecheck = Program.DB.querySQL(DB.type.ProjDB, "Select 순바닥면적 From ZoneGeneral_Form Where 존번호 ='" + k + "'");
                        zoneArea += Program.UTIL.ToDoubleOrZero(zonecheck[0][0].ToString());
                    }
                    ZoneData[6].Add(new { idx = i, val = string.Format("{0:F2}", zoneArea) });
                }

                //2. 공조존 면적 찾기
                if (zahulist.Count > 0)
                {
                    foreach (string k in zahulist)
                    {
                        string[][] zonecheck = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form","존번호,순바닥면적", "선택열회수기 = '" + k + "'");
                        
                        for(int g = 0; g < zonecheck.Length; g++)
                        {
                            zahuArea += Program.UTIL.ToDoubleOrZero(zonecheck[g][1].ToString());
                            zoneAhulist.Add(zonecheck[g][0]);
                        }
                        ZahuData[5].Add(new { idx = i, val = string.Format("{0:N2}", zahuArea) });
                    }
                }
                //3. 총면적 찾기
                if (zonelist.Count > 0)
                {
                    foreach (string g in zonelist)
                    {
                        totallist.Add(g);
                    }
                    //공조존이 있는경우
                    if (zahulist.Count > 0)
                    {
                        foreach (string k in zoneAhulist)
                        {
                            totallist.Add(k);
                        }
                    }
                }
                else if (zahulist.Count > 0)
                {
                    foreach (string g in zoneAhulist)
                    {
                        totallist.Add(g);
                    }
                    //공조존이 있는경우
                    if (zonelist.Count > 0)
                    {
                        foreach (string k in zonelist)
                        {
                            totallist.Add(k);
                        }
                    }
                }

                HashSet<string> uniquelist = new HashSet<string>(totallist); //중복되는 존 명칭 제거
                foreach (string k in uniquelist)
                {
                    string[][] zonecheck = Program.DB.querySQL(DB.type.ProjDB, "Select 순바닥면적 From ZoneGeneral_Form Where 존번호 ='" + k + "'");
                    totalArea += Program.UTIL.ToDoubleOrZero(zonecheck[0][0].ToString());
                }

                //보조설비 항목 작성
                List<string> pump1 = new List<string>();
                List<string> pump2 = new List<string>();
                List<string> sourcepump1 = new List<string>();
                List<string> sourcepump2 = new List<string>();

                Value = Program.DB.querySQL(DB.type.ProjDB, "Select 저장탱크, 저장유형,냉수펌프1,냉수펌프2,냉각수펌프1,냉각수펌프2 From CoolingSystem_Form Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    for(int check =0; check<6 ; check++)
                    {
                        if (Value[0][check]!=""&& Value[0][check] != "")
                        {
                            switch (check)
                            {
                                case 0:
                                    AuxData[0].Add(new { idx = i, val = Value[0][0] }); // 축열탱크종류
                                    break;
                                case 1:
                                    AuxData[1].Add(new { idx = i, val = Value[0][1] }); //저장유형
                                    break;
                                case 2: //냉수펌프1, 제어유형
                                    Split(Value[0][2], pump1);
                                    if (pump1.Count > 0)
                                    {
                                        string[][] Value2 = Program.DB.querySQL(DB.type.ProjDB, "Select 동력 From  User_Pump Where 번호='" + pump1[0] + "'");
                                        if (Value2.Length > 0)
                                        {
                                            double val = Program.UTIL.ToDoubleOrZero(Value2[0][0].ToString()) * Program.UTIL.ToDoubleOrZero(pump1[2].ToString());
                                            AuxData[2].Add(new { idx = i, val = string.Format("{0:N2}", val)});
                                        }
                                        AuxData[4].Add(new { idx = i, val = pump1[4] });
                                    }
                                    break;
                                case 3: //냉수펌프2
                                    Split(Value[0][3], pump2);
                                    if (pump2.Count > 0)
                                    {
                                        string[][] Value2 = Program.DB.querySQL(DB.type.ProjDB, "Select 동력 From  User_Pump Where 번호='" + pump2[0] + "'");
                                        if (Value2.Length > 0)
                                        {
                                            double val = Program.UTIL.ToDoubleOrZero(Value2[0][0].ToString()) * Program.UTIL.ToDoubleOrZero(pump2[2].ToString());
                                            AuxData[3].Add(new { idx = i, val = string.Format("{0:N2}", val) });
                                        }
                                    }
                                    break;
                                case 4: //냉각수펌프1, 제어유형
                                    Split(Value[0][4], sourcepump1);
                                    if (sourcepump1.Count > 0)
                                    {
                                        string[][] Value2 = Program.DB.querySQL(DB.type.ProjDB, "Select 동력 From  User_Pump Where 번호='" + sourcepump1[0] + "'");
                                        if (Value2.Length > 0)
                                        {
                                            double val = Program.UTIL.ToDoubleOrZero(Value2[0][0].ToString()) * Program.UTIL.ToDoubleOrZero(sourcepump1[2].ToString());
                                            AuxData[5].Add(new { idx = i, val = string.Format("{0:N2}", val) });
                                        }
                                        AuxData[7].Add(new { idx = i, val = sourcepump1[4] });
                                    }
                                    break;
                                case 5: //냉각수펌프2
                                    Split(Value[0][4], sourcepump2);
                                    if (sourcepump2.Count > 0)
                                    {
                                        string[][] Value2 = Program.DB.querySQL(DB.type.ProjDB, "Select 동력 From  User_Pump Where 번호='" + sourcepump2[0] + "'");
                                        if (Value2.Length > 0)
                                        {
                                            double val = Program.UTIL.ToDoubleOrZero(Value2[0][0].ToString()) * Program.UTIL.ToDoubleOrZero(sourcepump2[2].ToString());
                                            AuxData[6].Add(new { idx = i, val = string.Format("{0:N2}", val) });
                                        }
                                    }
                                    break;
                                default:
                                    break;                                    
                            }
                        }
                    }                    
                    
                }

                Value = Program.DB.querySQL(DB.type.ProjDB, "Select 열원설비,냉각탑 From CoolingSystem_Form Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    SourceData[0].Add(new { idx = i, val = Value[0][0] });
                    if(Value[0][1]!="")
                    {
                        string[][] Value2 = Program.DB.querySQL(DB.type.ProjDB, "Select 냉각능력,입구온도,출구온도 From User_CoolingTop Where 번호='" + Value[0][1] + "'");
                        if (Value2.Length > 0)
                        {
                            if (double.TryParse(Value2[0][0], out double result1))
                            {
                                SourceData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(Value2[0][0], 0) });
                            }
                            SourceData[2].Add(new { idx = i, val = Value2[0][1] });
                            SourceData[3].Add(new { idx = i, val = Value2[0][2] });
                        }
                    }
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct 개수_ahu, QCb_a_ahu,QC_Max_ahu, 공급설비1_ahu, 공급설비2_ahu From CoolingSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    ZahuData[0].Add(new { idx = i, val = Value[0][0] });
                    if (double.TryParse(Value[0][1], out double result1))
                    {
                        ZahuData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                    }
                    if (double.TryParse(Value[0][2], out double result2))
                    {
                        ZahuData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                    }
                    ZahuData[3].Add(new { idx = i, val = Value[0][3] });
                    ZahuData[4].Add(new { idx = i, val = Value[0][4] });
                }

                Value = Program.DB.querySQL(DB.type.ProjDB, "Select  sum(QC_nd),  sum(QC_ce),  sum(QC_d), sum(QC_s),  sum(QC_out), sum(QC_f),sum(W), Fuel From CoolingSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    if (totalArea > 0)
                    {
                        AnnualData[0].Add(new { idx = i, val = string.Format("{0:N2}", Program.UTIL.ToDoubleOrZero(Value[0][0]) / totalArea) });
                        AnnualData[1].Add(new { idx = i, val = string.Format("{0:N2}", Program.UTIL.ToDoubleOrZero(Value[0][1]) / totalArea) });
                        AnnualData[2].Add(new { idx = i, val = string.Format("{0:N2}", Program.UTIL.ToDoubleOrZero(Value[0][2]) / totalArea) });
                        AnnualData[3].Add(new { idx = i, val = string.Format("{0:N2}", Program.UTIL.ToDoubleOrZero(Value[0][3]) / totalArea) });
                        AnnualData[4].Add(new { idx = i, val = string.Format("{0:N2}", Program.UTIL.ToDoubleOrZero(Value[0][4]) / totalArea) });
                        AnnualData[5].Add(new { idx = i, val = string.Format("{0:N2}", Program.UTIL.ToDoubleOrZero(Value[0][5]) / totalArea) });
                        AnnualData[6].Add(new { idx = i, val = string.Format("{0:N2}", Program.UTIL.ToDoubleOrZero(Value[0][6]) / totalArea) });
                    }
                    else
                    {
                        AnnualData[0].Add(new { idx = i, val = "-"});
                        AnnualData[1].Add(new { idx = i, val = "-" });
                        AnnualData[2].Add(new { idx = i, val = "-" });
                        AnnualData[3].Add(new { idx = i, val = "-" });
                        AnnualData[4].Add(new { idx = i, val = "-" });
                        AnnualData[5].Add(new { idx = i, val = "-" });
                        AnnualData[6].Add(new { idx = i, val = "-" });
                    }

                        double primary = 0, tco2 = 0; 
                    if(Value[0][7]=="전기")
                    {
                        primary = (Program.UTIL.ToDoubleOrZero(Value[0][5]) + Program.UTIL.ToDoubleOrZero(Value[0][6])) * 2.75;
                        tco2 = (Program.UTIL.ToDoubleOrZero(Value[0][5]) + Program.UTIL.ToDoubleOrZero(Value[0][6])) * 0.4747 / 1000000 * 1000;
                    }
                    else
                    {
                        primary = Program.UTIL.ToDoubleOrZero(Value[0][5]) * 1.1 + Program.UTIL.ToDoubleOrZero(Value[0][6]) *2.75;
                        tco2 = Program.UTIL.ToDoubleOrZero(Value[0][5]) * 0.4747 / 1000000 * 1000 + Program.UTIL.ToDoubleOrZero(Value[0][6]) / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    }

                    if (totalArea > 0)
                    {
                        AnnualData[7].Add(new { idx = i, val = string.Format("{0:N2}", Program.UTIL.ToDoubleOrZero(primary / totalArea))});
                        AnnualData[8].Add(new { idx = i, val = string.Format("{0:N2}", Program.UTIL.ToDoubleOrZero(tco2 / totalArea * 1000))}); //kgCO2로 변경함
                    }
                    else
                    {
                        AnnualData[7].Add(new { idx = i, val = "-" });
                        AnnualData[8].Add(new { idx = i, val = "-" });
                    }
                }                
                
                for(int mth =0; mth < 12; mth++)    
                {
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select  QC_out_z,  QC_ce_z,  QC_d_z, QC_s_z, QC_nd_z  From CoolingSystem_Result Where 번호='" + Num + "' and 월='" + (mth +1) + "월'");
                    if(Value.Length > 0)
                    {
                        ZoneMthData[0].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                        ZoneMthData[1].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                        ZoneMthData[2].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                        ZoneMthData[3].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                        ZoneMthData[4].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                    }                    
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select  sum(QC_out_z),   sum(QC_ce_z),   sum(QC_d_z),  sum(QC_s_z),  sum(QC_nd_z)  From CoolingSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)   
                {
                    ZoneMthData[0].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                    ZoneMthData[1].Add(new { idx = i * 13 + 12 , val = Program.UTIL.doubleComa(Value[0][1], 0) });
                    ZoneMthData[2].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                    ZoneMthData[3].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                    ZoneMthData[4].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                }

                for (int mth = 0; mth < 12; mth++)
                {
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select  QC_out_ahu,  QC_ce_ahu,  QC_d_ahu, QC_s_ahu, QC_nd_ahu  From CoolingSystem_Result Where 번호='" + Num + "' and 월='" + (mth +1) + "월'");
                    if (Value.Length > 0)
                    {
                        ZahuMthData[0].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                        ZahuMthData[1].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                        ZahuMthData[2].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                        ZahuMthData[3].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                        ZahuMthData[4].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                    }
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select  sum(QC_out_ahu),   sum(QC_ce_ahu),   sum(QC_d_ahu),  sum(QC_s_ahu),  sum(QC_nd_ahu)  From CoolingSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    ZahuMthData[0].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                    ZahuMthData[1].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                    ZahuMthData[2].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                    ZahuMthData[3].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                    ZahuMthData[4].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                }

                for (int mth = 0; mth < 12; mth++)
                {
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select  QC_f,  SEER_c,  EER_c, QC_out, QC_ce, QC_d, QC_s, QC_nd  From CoolingSystem_Result Where 번호='" + Num + "' and 월='" + (mth +1) + "월'");
                    if (Value.Length > 0)
                    {
                        MthData[0].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                        MthData[1].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][1], 1) });
                        MthData[2].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][2], 1) });
                        MthData[3].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                        MthData[4].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                        MthData[5].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][5], 0) });
                        MthData[6].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][6], 0) });
                        MthData[7].Add(new { idx = i * 13 + mth, val = 0});
                        MthData[8].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][7], 0) });
                    }
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select  Sum(QC_f),  AVG(SEER_c),  AVG(EER_c), Sum(QC_out), sum(QC_ce), sum(QC_d), sum(QC_s), sum(QC_nd)  From CoolingSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    MthData[0].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                    MthData[1].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][1], 1) });
                    MthData[2].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][2], 1) });
                    MthData[3].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                    MthData[4].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                    MthData[5].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][5], 0) });
                    MthData[6].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][6], 0) });
                    MthData[7].Add(new { idx = i * 13 + 12, val = 0 });
                    MthData[8].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][7], 0) });
                }

                for (int mth = 0; mth < 12; mth++)
                {
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select W, W_ce, W_d, W_s, W_g From CoolingSystem_Result Where 번호='" + Num + "' and 월='" + (mth + 1) + "월'");
                    if (Value.Length > 0)
                    {
                        WMthData[0].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                        WMthData[1].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                        WMthData[2].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                        WMthData[3].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                        WMthData[4].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                    }
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select  sum(W), sum(W_ce), sum(W_d), sum(W_s), sum(W_g)  From CoolingSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    WMthData[0].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                    WMthData[1].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                    WMthData[2].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                    WMthData[3].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                    WMthData[4].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][4], 0) });                   
                }
                data.Add(new { cname = "projectnum", data = FormData[0] });
                data.Add(new { cname = "coolingnum", data = FormData[1] });
                data.Add(new { cname = "coolingnum2", data = FormData[2] });
                data.Add(new { cname = "coolingname", data = FormData[3] });
                data.Add(new { cname = "coolingtype", data = FormData[4] });
                data.Add(new { cname = "power", data = FormData[5] });
                data.Add(new { cname = "cop", data = FormData[6] });
                data.Add(new { cname = "compressor", data = FormData[7] });
                data.Add(new { cname = "control", data = FormData[8] });
                data.Add(new { cname = "freecooling", data = FormData[9] });
                data.Add(new { cname = "coolingcount", data = FormData[10] });

                data.Add(new { cname = "zone_count", data = ZoneData[0] });
                data.Add(new { cname = "zone_qcba", data = ZoneData[1] });
                data.Add(new { cname = "zone_qcmax", data = ZoneData[2] });
                data.Add(new { cname = "zone_ce1", data = ZoneData[3] });
                data.Add(new { cname = "zone_ce2", data = ZoneData[4] });
                data.Add(new { cname = "title", data = ZoneData[5] });
                data.Add(new { cname = "zone_area", data = ZoneData[6] });


                data.Add(new { cname = "zahu_count", data = ZahuData[0] });
                data.Add(new { cname = "zahu_qcba", data = ZahuData[1] });
                data.Add(new { cname = "zahu_qcmax", data = ZahuData[2] });
                data.Add(new { cname = "zahu_ce1", data = ZahuData[3] });
                data.Add(new { cname = "zahu_ce2", data = ZahuData[4] });
                data.Add(new { cname = "zahu_area", data = ZahuData[5] });


                data.Add(new { cname = "heatsource", data = SourceData[0] });
                data.Add(new { cname = "top_power", data = SourceData[1] });
                data.Add(new { cname = "top_in", data = SourceData[2] });
                data.Add(new { cname = "top_out", data = SourceData[3] });

                data.Add(new { cname = "storage", data = AuxData[0] });
                data.Add(new { cname = "stotype", data = AuxData[1] });
                data.Add(new { cname = "load1_power", data = AuxData[2] });
                data.Add(new { cname = "load2_power", data = AuxData[3] });
                data.Add(new { cname = "loadp_type", data = AuxData[4] });
                data.Add(new { cname = "source1_power", data = AuxData[5] });
                data.Add(new { cname = "source2_power", data = AuxData[6] });
                data.Add(new { cname = "sourcep_type", data = AuxData[7] });


                data.Add(new { cname = "annual_nd", data = AnnualData[0] });
                data.Add(new { cname = "annual_ce", data = AnnualData[1] });
                data.Add(new { cname = "annual_d", data = AnnualData[2] });
                data.Add(new { cname = "annual_s", data = AnnualData[3] });
                data.Add(new { cname = "annual_outg", data = AnnualData[4] });
                data.Add(new { cname = "annual_f", data = AnnualData[5] });
                data.Add(new { cname = "annual_w", data = AnnualData[6] });
                data.Add(new { cname = "annual_p", data = AnnualData[7] });
                data.Add(new { cname = "annual_tco2", data = AnnualData[8] });


                data.Add(new { cname = "zone_mth_outg", data = ZoneMthData[0] });
                data.Add(new { cname = "zone_mth_ce", data = ZoneMthData[1] });
                data.Add(new { cname = "zone_mth_d", data = ZoneMthData[2] });
                data.Add(new { cname = "zone_mth_s", data = ZoneMthData[3] });
                data.Add(new { cname = "zone_mth_nd", data = ZoneMthData[4] });

                //data.Add(new { cname = "zone_mth_outg", data = ZoneMthData[5] });
                //data.Add(new { cname = "zone_mth_ce", data = ZoneMthData[6] });
                //data.Add(new { cname = "zone_mth_d", data = ZoneMthData[7] });
                //data.Add(new { cname = "zone_mth_s", data = ZoneMthData[8] });
                //data.Add(new { cname = "zone_mth_nd", data = ZoneMthData[9] });

                data.Add(new { cname = "zahu_mth_outg", data = ZahuMthData[0] });
                data.Add(new { cname = "zahu_mth_ce", data = ZahuMthData[1] });
                data.Add(new { cname = "zahu_mth_d", data = ZahuMthData[2] });
                data.Add(new { cname = "zahu_mth_s", data = ZahuMthData[3] });
                data.Add(new { cname = "zahu_mth_nd", data = ZahuMthData[4] });

                //data.Add(new { cname = "zahu_mth_outg", data = ZahuMthData[5] });
                //data.Add(new { cname = "zahu_mth_ce", data = ZahuMthData[6] });
                //data.Add(new { cname = "zahu_mth_d", data = ZahuMthData[7] });
                //data.Add(new { cname = "zahu_mth_s", data = ZahuMthData[8] });
                //data.Add(new { cname = "zahu_mth_nd", data = ZahuMthData[9] });

                data.Add(new { cname = "mth_f", data = MthData[0] });
                data.Add(new { cname = "mth_seer", data = MthData[1] });
                data.Add(new { cname = "mth_eer", data = MthData[2] });
                data.Add(new { cname = "mth_outg", data = MthData[3] });
                data.Add(new { cname = "mth_ce", data = MthData[4] });
                data.Add(new { cname = "mth_d", data = MthData[5] });
                data.Add(new { cname = "mth_s", data = MthData[6] });
                data.Add(new { cname = "mth_g", data = MthData[7] });
                data.Add(new { cname = "mth_nd", data = MthData[8] });

                //data.Add(new { cname = "mth_f", data = MthData[8] });
                //data.Add(new { cname = "mth_seer", data = MthData[9] });
                //data.Add(new { cname = "mth_eer", data = MthData[10] });
                //data.Add(new { cname = "mth_outg", data = MthData[11] });
                //data.Add(new { cname = "mth_ce", data = MthData[12] });
                //data.Add(new { cname = "mth_d", data = MthData[13] });
                //data.Add(new { cname = "mth_s", data = MthData[14] });
                //data.Add(new { cname = "mth_g", data = MthData[15] });

                data.Add(new { cname = "w", data = WMthData[0] });
                data.Add(new { cname = "w_ce", data = WMthData[1] });
                data.Add(new { cname = "w_d", data = WMthData[2] });
                data.Add(new { cname = "w_s", data = WMthData[3] });
                data.Add(new { cname = "w_g", data = WMthData[4] });

                //data.Add(new { cname = "w", data = WMthData[5] });
                //data.Add(new { cname = "w_ce", data = WMthData[6] });
                //data.Add(new { cname = "w_d", data = WMthData[7] });
                //data.Add(new { cname = "w_s", data = WMthData[8] });
                //data.Add(new { cname = "w_g", data = WMthData[9] });


                List<object> nd_chart = new List<object>();
                List<object> ce_chart = new List<object>();
                List<object> d_chart = new List<object>();
                List<object> s_chart = new List<object>();
                List<object> f_chart = new List<object>();
                for (int mth = 0; mth < 12; mth++)
                {
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select QC_nd, QC_ce, QC_d, QC_s, QC_f From CoolingSystem_Result Where 번호='" + Num + "' and 월='" + (mth + 1) + "월'");
                    if (Value.Length > 0)
                    {
                        nd_chart.Add(Program.UTIL.ToDoubleOrZero(Program.UTIL.doubleComa(Value[0][0], 0)));
                        ce_chart.Add(Program.UTIL.ToDoubleOrZero(Program.UTIL.doubleComa(Value[0][1], 0)));
                        d_chart.Add(Program.UTIL.ToDoubleOrZero(Program.UTIL.doubleComa(Value[0][2], 0)));
                        s_chart.Add(Program.UTIL.ToDoubleOrZero(Program.UTIL.doubleComa(Value[0][3], 0)));
                        f_chart.Add(Program.UTIL.ToDoubleOrZero(Program.UTIL.doubleComa(Value[0][4], 0)));
                    }
                }
                chart_nd.Add(System.Text.Json.JsonSerializer.Serialize(nd_chart.ToArray()));
                chart_ce.Add(System.Text.Json.JsonSerializer.Serialize(ce_chart.ToArray()));
                chart_d.Add(System.Text.Json.JsonSerializer.Serialize(d_chart.ToArray()));
                chart_s.Add(System.Text.Json.JsonSerializer.Serialize(s_chart.ToArray()));
                chart_f.Add(System.Text.Json.JsonSerializer.Serialize(f_chart.ToArray()));
                double max = 0;
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select QC_out From CoolingSystem_Result Where 번호='" + Num + "'");
                if (Value.Length >0)
                {
                    max = Program.UTIL.ToDoubleOrZero(Value[0][0]);
                    for (int a= 1; a<Value.Length; a++)
                    {
                        if(max < Program.UTIL.ToDoubleOrZero(Value[a][0]))
                        {
                            max = Program.UTIL.ToDoubleOrZero(Value[a][0]);
                        }                       
                    }                    
                }
                int n = ((int)max).ToString().Length;
                max = Program.UTIL.ToDoubleOrZero(String.Format("{0:F0}", max / Math.Pow(10, n - 1))) * Math.Pow(10, n - 1) + Math.Pow(10, n - 1);
                if (charts != "") charts += ",";
                charts += "{data:[" +
                "{type:\"bar\",barPercentage:0.4,label:\"에너지요구량 [kWh]\",data:" + chart_nd[i] + ",borderColor:\"#A9D18E\",backgroundColor:\"#A9D18E\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"공급열손실 [kWh]\",data:" + chart_ce[i] + ",borderColor:\"#70AD47\",backgroundColor:\"#70AD47\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"분배열손실 [kWh]\",data:" + chart_d[i] + ",borderColor:\"#FFD966\",backgroundColor:\"#FFD966\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"분배열손실 [kWh]\",data:" + chart_d[i] + ",borderColor:\"#FFD966\",backgroundColor:\"#FFD966\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"저장열손실 [kWh]\",data:" + chart_s[i] + ",borderColor:\"#9DC3E6\",backgroundColor:\"#9DC3E6\",dash:false}," +
                "{type:\"line\",yAxisID: 'y',label:\"에너지소요량 [kWh]\",data:" + chart_f[i] + ",borderColor:\"#ED7D31\",backgroundColor:\"#ED7D31\",dash:false, tension: 0.4}," +
                "],max:" + max.ToString() + ",step:100,legend:true,stacked:true}";

            }
            s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
            s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
            
            Debug.Print("start");
            
            runScript("init(" + s + "," + s2 + "," + "[" + charts + "])");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webView21.CoreWebView2.ShowPrintUI();
        }

    }
}