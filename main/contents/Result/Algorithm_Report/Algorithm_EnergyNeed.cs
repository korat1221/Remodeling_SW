using Eagle._Components.Public;
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
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.contents.Result
{
    public partial class Algorithm_EnergyNeed : Form
    {
        bool scriptable = false;
        public Algorithm_EnergyNeed()
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
            string[][] 번호 = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct 번호 From Zone_HCneed_Result Order by 번호");
            List<object> items = new List<object>();
            List<object> data = new List<object>();
            List<object>[] GeneralData = new List<object>[30];
            List<object>[] SystemData = new List<object>[30];
            List<object>[] VentilData = new List<object>[30];
            List<object>[] AnnualData = new List<object>[30];
            List<object>[] WallData = new List<object>[30];
            List<object>[] RoofData = new List<object>[30];
            List<object>[] FloorData = new List<object>[30];
            List<object>[] WinData = new List<object>[30];
            List<object>[] CWData = new List<object>[30];
            List<object>[] DoorData = new List<object>[30];
            List<object>[] MthData = new List<object>[30];
            List<object>[] HeatingMthData = new List<object>[30];
            List<object>[] CoolingMthData = new List<object>[30];
            List<string> chart_nd = new List<string>();
            List<string> chart_ce = new List<string>();
            List<string> chart_d = new List<string>();
            List<string> chart_s = new List<string>();
            List<string> chart_f = new List<string>();
            int i = -1;
            while (++i < 30)
            {
                GeneralData[i] = new List<object>();
                SystemData[i] = new List<object>();
                VentilData[i] = new List<object>();
                AnnualData[i] = new List<object>();
                WallData[i] = new List<object>();
                RoofData[i] = new List<object>();
                FloorData[i] = new List<object>();
                WinData[i] = new List<object>();
                CWData[i] = new List<object>();
                DoorData[i] = new List<object>();
                MthData[i] = new List<object>();
                HeatingMthData[i] = new List<object>();
                CoolingMthData[i] = new List<object>();
            }

            i = -1;
            while (++i < 번호.Length)
            {
                #region 일반정보
                string Num = 번호[i][0];
                double area = 0, height = 0, volume = 0;
                double[] theta_e = new double[12];
                items.Add("Algorithm_EnergyNeed.htm"); // 예시 코드: 메인 메뉴 동적 할당
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호,지역");
                if (Value.Length > 0)
                {
                    GeneralData[0].Add(new { idx = i, val = Value[0][0] }); //프로젝트번호
                }
                GeneralData[1].Add(new { idx = i, val = Num }); //그림번호
                GeneralData[2].Add(new { idx = i, val = Num }); //번호
                Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존이름,순바닥면적,천장고","존번호='"+Num+"'");
                if(Value.Length > 0)
                {
                    GeneralData[3].Add(new { idx = i, val = Num + ". "+ Value[0][0]+" 존 에너지요구량 검토 보고서" }); //title
                    GeneralData[4].Add(new { idx = i, val = Value[0][0] }); //명칭
                    area = Convert.ToDouble(Value[0][1]);
                    height = Convert.ToDouble(Value[0][2]);
                    volume = area * height;
                    GeneralData[5].Add(new { idx = i, val = Program.UTIL.doubleComa(area.ToString(), 1) }); //면적
                    GeneralData[6].Add(new { idx = i, val = Program.UTIL.doubleComa(volume.ToString(), 1) }); //체적
                    GeneralData[7].Add(new { idx = i, val = Program.UTIL.doubleComa(height.ToString(), 1) }); //천장고
                }
                #endregion
                #region 설비정보
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select a.난방시스템,b.명칭 From Heating_ce_Form as a Inner Join HeatingSystem_Form as b on a.난방시스템=b.번호 where a.존번호='" + Num + "'");
                if(Value.Length > 0)
                {
                    SystemData[0].Add(new { idx = i, val = Value[0][1] }); //난방
                    
                }
                else
                {
                    SystemData[0].Add(new { idx = i, val = "-" }); //난방
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select a.냉방시스템,b.명칭 From Cooling_ce_Form as a Inner Join CoolingSystem_Form as b on a.냉방시스템=b.번호 where a.존번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    SystemData[1].Add(new { idx = i, val = Value[0][1] }); //냉방
                }
                else
                {
                    SystemData[1].Add(new { idx = i, val = "-" }); //냉방
                }

                string dhw = "-";
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select 존,명칭 From DHWSystem_Form");
                if (Value.Length > 0)
                {
                    for (int a = 0; a < Value.Length; a++)
                    {
                        ArrayList splitzone = new ArrayList();
                        splitzone = Split_(Value[a][0]);
                        for (int aa = 0; aa < splitzone.Count; aa++)
                        {
                            if (splitzone[aa].ToString() == Num)
                            {
                                dhw = Value[a][1];
                                break;
                            }
                        }
                    }
                }
                SystemData[2].Add(new { idx = i, val = dhw }); //급탕
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select 등기구명칭 From ZoneLighting_form where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    SystemData[3].Add(new { idx = i, val = Value[0][0] }); //조명
                }
                else
                {
                    SystemData[3].Add(new { idx = i, val = "-" }); //조명
                }

                string ahu = "-";
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select a.선택열회수기, b.명칭 From ZoneGeneral_Form as a Inner Join AHUSystem_Form as b on a.선택열회수기=b.번호 where a.존번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    if (Value[0][0].Contains("AHU"))
                    { ahu = Value[0][1]; }
                }
                SystemData[4].Add(new { idx = i, val = ahu }); //공조
                string hrv = "-";
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select a.선택열회수기, b.명칭 From ZoneGeneral_Form as a Inner Join AHUSystem_Form as b on a.선택열회수기=b.번호 where a.존번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    if (Value[0][0].Contains("HRV"))
                    { hrv = Value[0][1]; }
                }
                SystemData[5].Add(new { idx = i, val = hrv }); //환기
                #endregion

                #region 환기정보
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct a.이용일환기량,b.ninf,b.nmech,b.nwin From ZoneGeneral_Form as a Inner Join Zone_HCneed_Result as b on a.존번호 = b.번호 Where a.존번호='" + Num + "' and b.비이용일_이용일='이용일'");
                if (Value.Length > 0)
                {
                    VentilData[0].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][0], 1) }); //필요환기량
                    VentilData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][1], 1) }); //침기
                    VentilData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][2], 1) }); //기계환기
                    VentilData[3].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][3], 1) }); //자연환기
                    VentilData[4].Add(new { idx = i, val = (Convert.ToDouble(Value[0][1]) /0.05).ToString("0.0")}); //n50
                }
                #endregion

                #region 연간정보
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct Qb_a, Q_max From Zone_HCneed_Result Where 번호='" + Num + "' and 비이용일_이용일='이용일' and 난방_냉방='난방'");
                double annual = 0;
                if (Value.Length > 0)
                {
                    annual = Convert.ToDouble(Value[0][0]) / area;
                    AnnualData[0].Add(new { idx = i, val = Program.UTIL.doubleComa(annual.ToString(), 1) }); //난방
                    annual = Convert.ToDouble(Value[0][1]) / area;
                    AnnualData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(annual.ToString(), 1) }); //난방부하
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct Qb_a, Q_max From Zone_HCneed_Result Where 번호='" + Num + "' and 비이용일_이용일='이용일' and 난방_냉방='냉방'");
                annual = 0;
                if (Value.Length > 0)
                {
                    annual = Convert.ToDouble(Value[0][0]) / area;
                    AnnualData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(annual.ToString(), 1) }); //냉방
                    annual = Convert.ToDouble(Value[0][1]) / area;
                    AnnualData[3].Add(new { idx = i, val = Program.UTIL.doubleComa(annual.ToString(), 1) }); //냉방부하
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select sum(Final_kWh) From Zone_LightResult Where 번호='" + Num + "'");
                annual = 0;
                if (Value.Length > 0)
                {
                    annual = Convert.ToDouble(Value[0][0]) / area;
                    AnnualData[4].Add(new { idx = i, val = Program.UTIL.doubleComa(annual.ToString(), 1) }); //조명
                }
                annual = 0; double dwd_a = 0; 
                double[] dmth = new double[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select sum(dwd_mth) From Zone_HCneed_Result Where 번호='" + Num + "' and 비이용일_이용일='이용일' and 난방_냉방='난방'");
                if (Value.Length > 0)
                {
                    dwd_a = Convert.ToDouble(Value[0][0]);
                }
                for (int mth =0; mth < 12; mth ++)
                {
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct a.일일급탕요구량,b.theta_e From ZoneGeneral_Form as a Inner Join Zone_HCneed_Result as b on a.존번호=b.번호 Where b.번호='" + Num + "' and b.비이용일_이용일='이용일' and b.난방_냉방='난방' and b.월='" + (mth +1) + "월'");
                    if (Value.Length > 0)
                    {
                        annual  += Convert.ToDouble(Value[0][0]) * dwd_a * dmth[mth] / 365 * (-0.02 * Convert.ToDouble(Value[0][1]) + 1.25);
                    }
                }
                annual = annual / area;
                AnnualData[5].Add(new { idx = i, val = Program.UTIL.doubleComa(annual.ToString(), 1) }); //급탕

                #endregion

                #region 외벽 성능정보
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select a.면적,b.열관류율,b.유효열관류율,b.흡수율 From ZoneEnvelope_3D  as a Inner Join ConstructionWall as b on a.구조체번호=b.번호 Where a.존='" + Num + "' and a.외피유형='외벽'");
                double wall_면적 = 0, wall_열관류율 = 0, wall_유효열관류율 = 0, wall_열교가산치 = 0, wall_흡수율 = 0;
                if (Value.Length > 0)
                {
                    for(int a=0 ; a<Value.Length; a++)
                    {
                        wall_면적 += Convert.ToDouble(Value[a][0]);
                        wall_열관류율 += Convert.ToDouble(Value[a][0]) * Convert.ToDouble(Value[a][1]);
                        wall_유효열관류율 += Convert.ToDouble(Value[a][0]) * Convert.ToDouble(Value[a][2]);
                        wall_흡수율 += Convert.ToDouble(Value[a][0]) * Convert.ToDouble(Value[a][3]);
                    }
                    wall_열관류율 = wall_열관류율 / wall_면적;
                    wall_유효열관류율 = wall_유효열관류율 / wall_면적;
                    wall_열교가산치 = Math.Max(0, wall_유효열관류율 - wall_열관류율);
                    wall_흡수율 = wall_흡수율 / wall_면적;
                }
                WallData[0].Add(new { idx = i, val = Program.UTIL.doubleComa(wall_면적.ToString(), 1) });
                WallData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(wall_열관류율.ToString(), 2) });
                WallData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(wall_열교가산치.ToString(), 2) });
                WallData[3].Add(new { idx = i, val = Program.UTIL.doubleComa(wall_유효열관류율.ToString(), 2) });
                WallData[4].Add(new { idx = i, val = Program.UTIL.doubleComa((wall_면적 * wall_유효열관류율).ToString(), 2) });
                WallData[5].Add(new { idx = i, val = "-" });
                WallData[6].Add(new { idx = i, val = Program.UTIL.doubleComa(wall_흡수율.ToString(), 1) });
                #endregion

                #region 지붕 성능정보
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select a.면적,b.열관류율,b.유효열관류율,b.흡수율 From ZoneEnvelope_3D  as a Inner Join ConstructionRoof as b on a.구조체번호=b.번호 Where a.존='" + Num + "' and a.외피유형='지붕'");
                double roof_면적 = 0, roof_열관류율 = 0, roof_유효열관류율 = 0, roof_열교가산치 = 0, roof_흡수율 = 0;
                if (Value.Length > 0)
                {
                    for (int a = 0; a < Value.Length; a++)
                    {
                        roof_면적 += Convert.ToDouble(Value[a][0]);
                        roof_열관류율 += Convert.ToDouble(Value[a][0]) * Convert.ToDouble(Value[a][1]);
                        roof_유효열관류율 += Convert.ToDouble(Value[a][0]) * Convert.ToDouble(Value[a][2]);
                        roof_흡수율 += Convert.ToDouble(Value[a][0]) * Convert.ToDouble(Value[a][3]);
                    }
                    roof_열관류율 = roof_열관류율 / roof_면적;
                    roof_유효열관류율 = roof_유효열관류율 / roof_면적;
                    roof_열교가산치 = Math.Max(0, roof_유효열관류율 - roof_열관류율);
                    roof_흡수율 = roof_흡수율 / roof_면적;
                }
                RoofData[0].Add(new { idx = i, val = Program.UTIL.doubleComa(roof_면적.ToString(), 1) });
                RoofData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(roof_열관류율.ToString(), 2) });
                RoofData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(roof_열교가산치.ToString(), 2) });
                RoofData[3].Add(new { idx = i, val = Program.UTIL.doubleComa(roof_유효열관류율.ToString(), 2) });
                RoofData[4].Add(new { idx = i, val = Program.UTIL.doubleComa((roof_면적 * roof_유효열관류율).ToString(), 2) });
                RoofData[5].Add(new { idx = i, val = "-" });
                RoofData[6].Add(new { idx = i, val = Program.UTIL.doubleComa(roof_흡수율.ToString(), 1) });
                #endregion

                #region 최하층바닥 성능정보
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select a.면적,b.열관류율,b.유효열관류율 From ZoneEnvelope_3D  as a Inner Join ConstructionFloor as b on a.구조체번호=b.번호 Where a.존='" + Num + "' and a.외피유형='최하층바닥'");
                double floor_면적 = 0, floor_열관류율 = 0, floor_유효열관류율 = 0, floor_열교가산치 = 0;
                if (Value.Length > 0)
                {
                    for (int a = 0; a < Value.Length; a++)
                    {
                        floor_면적 += Convert.ToDouble(Value[a][0]);
                        floor_열관류율 += Convert.ToDouble(Value[a][0]) * Convert.ToDouble(Value[a][1]);
                        floor_유효열관류율 += Convert.ToDouble(Value[a][0]) * Convert.ToDouble(Value[a][2]);
                    }
                    floor_열관류율 = floor_열관류율 / floor_면적;
                    floor_유효열관류율 = floor_유효열관류율 / floor_면적;
                    floor_열교가산치 = Math.Max(0, floor_유효열관류율 - floor_열관류율);
                }
                FloorData[0].Add(new { idx = i, val = Program.UTIL.doubleComa(floor_면적.ToString(), 1) });
                FloorData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(floor_열관류율.ToString(), 2) });
                FloorData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(floor_열교가산치.ToString(), 2) });
                FloorData[3].Add(new { idx = i, val = Program.UTIL.doubleComa(floor_유효열관류율.ToString(), 2) });
                FloorData[4].Add(new { idx = i, val = Program.UTIL.doubleComa((floor_면적 * floor_유효열관류율).ToString(), 2) });
                FloorData[5].Add(new { idx = i, val = "-" });
                FloorData[6].Add(new { idx = i, val = "-" });
                #endregion

                #region 창호 성능정보
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select a.면적,b.창호열관류율,b.창호유효열관류율,b.상위창호번호 From ZoneEnvelope_3D  as a Inner Join SubWindow as b on a.구조체번호=b.번호 Where a.존='" + Num + "' and a.외피유형='창호'");
                double win_면적 = 0, win_열관류율 = 0, win_유효열관류율 = 0, win_열교가산치 = 0, win_shgc = 0; 
                if (Value.Length > 0)
                {
                    for (int a = 0; a < Value.Length; a++)
                    {
                        win_면적 += Convert.ToDouble(Value[a][0]);
                        win_열관류율 += Convert.ToDouble(Value[a][0]) * Convert.ToDouble(Value[a][1]);
                        win_유효열관류율 += Convert.ToDouble(Value[a][0]) * Convert.ToDouble(Value[a][2]);
                        string[][] Value2 = Program.DB.querySQL(DB.type.ProjDB, "Select 태양열취득률 From ConstructionWindow  Where 번호='" + Value[a][3] + "'");
                        if(Value2.Length > 0)
                        {
                            win_shgc += Convert.ToDouble(Value[a][0]) * Convert.ToDouble(Value2[0][0]);
                        }
                    }
                    win_열관류율 = win_열관류율 / win_면적;
                    win_유효열관류율 = win_유효열관류율 / win_면적;
                    win_열교가산치 = Math.Max(0, win_유효열관류율 - win_열관류율);
                    win_shgc = win_shgc / win_면적;
                }
                WinData[0].Add(new { idx = i, val = Program.UTIL.doubleComa(win_면적.ToString(), 1) });
                WinData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(win_열관류율.ToString(), 2) });
                WinData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(win_열교가산치.ToString(), 2) });
                WinData[3].Add(new { idx = i, val = Program.UTIL.doubleComa(win_유효열관류율.ToString(), 2) });
                WinData[4].Add(new { idx = i, val = Program.UTIL.doubleComa((win_면적 * win_유효열관류율).ToString(), 2) });
                WinData[5].Add(new { idx = i, val = Program.UTIL.doubleComa(win_shgc.ToString(), 2) });
                WinData[6].Add(new { idx = i, val = "-" });
                #endregion

                #region 커튼월창 성능정보
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select 면적,커튼월부위,구조체번호 From ZoneEnvelope_3D  Where 존='" + Num + "' and 외피유형='커튼월창'");               
                double cw_면적 = 0, cw_열관류율 = 0, cw_유효열관류율 = 0, cw_열교가산치 = 0, cw_shgc = 0;
                if (Value.Length > 0)
                {
                    for (int a = 0; a < Value.Length; a++)
                    {
                        string[][] Value3 = null;
                        cw_면적 += Convert.ToDouble(Value[a][0]);
                        if (Value[a][1] == "패널부분")
                        {
                            Value3 = Program.DB.querySQL(DB.type.ProjDB, "Select 패널부분열관류율,패널부분유효열관류율,패널흡수율 From ConstructionCW Where 번호='" + Value[a][2] + "'");
                        }
                        if (Value[a][1] == "출입문부분")
                        {
                            Value3 = Program.DB.querySQL(DB.type.ProjDB, "Select 출입문부분열관류율,출입문부분유효열관류율,출입문태양열취득률 From ConstructionCW Where 번호='" + Value[a][2] + "'");
                        }
                        else
                        {
                            Value3 = Program.DB.querySQL(DB.type.ProjDB, "Select 유리부분열관류율,유리부분유효열관류율,태양열취득률 From ConstructionCW Where 번호='" + Value[a][2] + "'");
                        }
                        cw_열관류율 += Convert.ToDouble(Value[a][0]) * Convert.ToDouble(Value3[0][0]); 
                        cw_유효열관류율 += Convert.ToDouble(Value[a][0]) * Convert.ToDouble(Value3[0][1]);
                        cw_shgc += Convert.ToDouble(Value[a][0]) * Convert.ToDouble(Value3[0][2]);
                    }
                    cw_열관류율 = cw_열관류율 / cw_면적;
                    cw_유효열관류율 = cw_유효열관류율 / cw_면적;
                    cw_열교가산치 = Math.Max(0, cw_유효열관류율 - cw_열관류율);
                    cw_shgc = cw_shgc / cw_면적;
                }
                CWData[0].Add(new { idx = i, val = Program.UTIL.doubleComa(cw_면적.ToString(), 1) });
                CWData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(cw_열관류율.ToString(), 2) });
                CWData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(cw_열교가산치.ToString(), 2) });
                CWData[3].Add(new { idx = i, val = Program.UTIL.doubleComa(cw_유효열관류율.ToString(), 2) });
                CWData[4].Add(new { idx = i, val = Program.UTIL.doubleComa((cw_면적 * cw_유효열관류율).ToString(), 2) });
                CWData[5].Add(new { idx = i, val = Program.UTIL.doubleComa(cw_shgc.ToString(), 2) });
                CWData[6].Add(new { idx = i, val = "-" });
                #endregion

                #region 외부출입문 성능정보
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select a.면적,b.문짝열관류율,b.문유효열관류율,b.흡수율 From ZoneEnvelope_3D  as a Inner Join ConstructionDoor as b on a.구조체번호=b.번호 Where a.존='" + Num + "' and a.외피유형='외부출입문'");
                double door_면적 = 0, door_열관류율 = 0, door_유효열관류율 = 0, door_열교가산치 = 0, door_흡수율 = 0;
                if (Value.Length > 0)
                {
                    for (int a = 0; a < Value.Length; a++)
                    {
                        door_면적 += Convert.ToDouble(Value[a][0]);
                        door_열관류율 += Convert.ToDouble(Value[a][0]) * Convert.ToDouble(Value[a][1]);
                        door_유효열관류율 += Convert.ToDouble(Value[a][0]) * Convert.ToDouble(Value[a][2]);
                        door_흡수율 += Convert.ToDouble(Value[a][0]) * Convert.ToDouble(Value[a][3]);
                    }
                    door_열관류율 = door_열관류율 / door_면적;
                    door_유효열관류율 = door_유효열관류율 / door_면적;
                    door_열교가산치 = Math.Max(0, door_유효열관류율 - door_열관류율);
                    door_흡수율 = door_흡수율 / door_면적;
                }
                DoorData[0].Add(new { idx = i, val = Program.UTIL.doubleComa(door_면적.ToString(), 1) });
                DoorData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(door_열관류율.ToString(), 2) });
                DoorData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(door_열교가산치.ToString(), 2) });
                DoorData[3].Add(new { idx = i, val = Program.UTIL.doubleComa(door_유효열관류율.ToString(), 2) });
                DoorData[4].Add(new { idx = i, val = Program.UTIL.doubleComa((door_면적 * door_유효열관류율).ToString(), 2) });
                DoorData[5].Add(new { idx = i, val = "-" });
                DoorData[6].Add(new { idx = i, val = Program.UTIL.doubleComa(door_흡수율.ToString(), 1) });
                #endregion

                #region 난방 월간
                {
                    for (int mth = 0; mth < 12; mth++)
                    {
                        Value = Program.DB.querySQL(DB.type.ProjDB, "Select  QTsink_tot * dwd_mth /1000 as total_1, QVsink_tot* dwd_mth/1000 as total_2, QSopsink_tot* dwd_mth/1000 as total_3, Qsink* dwd_mth/1000 as total_4 From Zone_HCneed_Result Where 번호='" + Num + "' and 비이용일_이용일 ='이용일' and 난방_냉방='난방' and 월='" + (mth + 1).ToString() + "월'");
                       if(Value.Length >0)
                        {
                            HeatingMthData[0].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                            data.Add(new { cname = "qt_sink_h", data = HeatingMthData[0] });
                            HeatingMthData[1].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                            data.Add(new { cname = "qv_sink_h", data = HeatingMthData[1] });
                            HeatingMthData[2].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                            data.Add(new { cname = "qs_sink_h", data = HeatingMthData[2] });
                            HeatingMthData[3].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                            data.Add(new { cname = "qsink_tot_h", data = HeatingMthData[3] });
                        }
                        
                    }
                    for (int mth = 0; mth < 12; mth++)
                    {
                        Value = Program.DB.querySQL(DB.type.ProjDB, "Select QTsource_tot * dwd_mth /1000 as total_1, QVsource_tot * dwd_mth /1000 as total_2, QStr_Win * dwd_mth /1000 as total_3, QStr_CW * dwd_mth /1000 as total_4,QSopsource_tot * dwd_mth /1000 as total_5,QI_tot * dwd_mth /1000 as total_6,Qsource  * dwd_mth /1000 as total_7 From Zone_HCneed_Result Where 번호='" + Num + "' and 비이용일_이용일 ='이용일' and 난방_냉방='난방' and 월='" + (mth + 1).ToString() + "월'");
                        if (Value.Length > 0)
                        {
                            HeatingMthData[8].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                            data.Add(new { cname = "qt_source_h", data = HeatingMthData[8] });
                            HeatingMthData[9].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                            data.Add(new { cname = "qv_source_h", data = HeatingMthData[9] });
                            double qs = Convert.ToDouble(Value[0][2]) + Convert.ToDouble(Value[0][3]) + Convert.ToDouble(Value[0][4]);
                            HeatingMthData[10].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(qs.ToString(), 0) });
                            data.Add(new { cname = "qs_source_h", data = HeatingMthData[10] });
                            HeatingMthData[11].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(Value[0][5], 0) });
                            data.Add(new { cname = "qi_source_h", data = HeatingMthData[11] });
                            HeatingMthData[12].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(Value[0][6], 0) });
                            data.Add(new { cname = "qsource_tot_h", data = HeatingMthData[12] });
                        }
                    }
                    for (int mth = 0; mth < 12; mth++)
                    {
                        Value = Program.DB.querySQL(DB.type.ProjDB, "Select Qb_mth /1000, eta, dQc_b From Zone_HCneed_Result Where 번호='" + Num + "' and 비이용일_이용일 ='이용일' and 난방_냉방='난방' and 월='" + (mth + 1).ToString() + "월'");
                        if (Value.Length > 0)
                        {
                            HeatingMthData[13].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                            data.Add(new { cname = "qb_h", data = HeatingMthData[13] });
                            HeatingMthData[14].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(Value[0][1], 2) });
                            data.Add(new { cname = "eta_h", data = HeatingMthData[14] });
                            HeatingMthData[15].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                            data.Add(new { cname = "dqc_b", data = HeatingMthData[15] });
                        }
                    }
                }
                #endregion

                #region 냉방 월간
                {
                    for (int mth = 0; mth < 12; mth++)
                    {
                        Value = Program.DB.querySQL(DB.type.ProjDB, "Select  QTsink_tot * dwd_mth /1000 as total_1, QVsink_tot* dwd_mth/1000 as total_2, QSopsink_tot* dwd_mth/1000 as total_3, Qsink* dwd_mth/1000 as total_4 From Zone_HCneed_Result Where 번호='" + Num + "' and 비이용일_이용일 ='이용일' and 난방_냉방='냉방' and 월='" + (mth + 1).ToString() + "월'");
                        if (Value.Length > 0)
                        {
                            CoolingMthData[0].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                            CoolingMthData[1].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                            CoolingMthData[2].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                            CoolingMthData[3].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                        }

                    }
                    for (int mth = 0; mth < 12; mth++)
                    {
                        Value = Program.DB.querySQL(DB.type.ProjDB, "Select QTsource_tot * dwd_mth /1000 as total_1, QVsource_tot * dwd_mth /1000 as total_2, QStr_Win * dwd_mth /1000 as total_3, QStr_CW * dwd_mth /1000 as total_4,QSopsource_tot * dwd_mth /1000 as total_5,QI_tot * dwd_mth /1000 as total_6,Qsource  * dwd_mth /1000 as total_7 From Zone_HCneed_Result Where 번호='" + Num + "' and 비이용일_이용일 ='이용일' and 난방_냉방='냉방' and 월='" + (mth + 1).ToString() + "월'");
                        if (Value.Length > 0)
                        {
                            CoolingMthData[8].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                            CoolingMthData[9].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                            double qs = Convert.ToDouble(Value[0][2]) + Convert.ToDouble(Value[0][3]) + Convert.ToDouble(Value[0][4]);
                            CoolingMthData[10].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(qs.ToString(), 0) });
                            CoolingMthData[11].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(Value[0][5], 0) });
                            CoolingMthData[12].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(Value[0][6], 0) });
                        }
                    }
                    for (int mth = 0; mth < 12; mth++)
                    {

                        Value = Program.DB.querySQL(DB.type.ProjDB, "Select Qb_mth /1000, eta, Q_DHU_tot * dwd_mth /1000 as total_1 From Zone_HCneed_Result Where 번호='" + Num + "' and 비이용일_이용일 ='이용일' and 난방_냉방='난방' and 월='" + (mth + 1).ToString() + "월'");
                        if (Value.Length > 0)
                        {
                            CoolingMthData[13].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                            CoolingMthData[14].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(Value[0][1], 2) });
                            CoolingMthData[15].Add(new { idx = i * 12 + mth, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                        }
                    }
                }
                #endregion
            }
            data.Add(new { cname = "projectnum", data = GeneralData[0] });
            data.Add(new { cname = "zonenum", data = GeneralData[1] });
            data.Add(new { cname = "zonenum2", data = GeneralData[2] });
            data.Add(new { cname = "title", data = GeneralData[3] });
            data.Add(new { cname = "zonename", data = GeneralData[4] });
            data.Add(new { cname = "area", data = GeneralData[5] });
            data.Add(new { cname = "volume", data = GeneralData[6] });
            data.Add(new { cname = "height", data = GeneralData[7] });
            data.Add(new { cname = "heating", data = SystemData[0] });
            data.Add(new { cname = "cooling", data = SystemData[1] });
            data.Add(new { cname = "dhw", data = SystemData[2] });
            data.Add(new { cname = "lighting", data = SystemData[3] });
            data.Add(new { cname = "ahu", data = SystemData[4] });
            data.Add(new { cname = "hrv", data = SystemData[5] });
            data.Add(new { cname = "vwd", data = VentilData[0] });
            data.Add(new { cname = "ninf", data = VentilData[1] });
            data.Add(new { cname = "nmech", data = VentilData[2] });
            data.Add(new { cname = "nwin", data = VentilData[3] });
            data.Add(new { cname = "n50", data = VentilData[4] });
            data.Add(new { cname = "qhb_a", data = AnnualData[0] });
            data.Add(new { cname = "qhmax", data = AnnualData[1] });
            data.Add(new { cname = "qcb_a", data = AnnualData[2] });
            data.Add(new { cname = "qcmax", data = AnnualData[3] });
            data.Add(new { cname = "qlb_a", data = AnnualData[4] });
            data.Add(new { cname = "qwb_a", data = AnnualData[5] });
            data.Add(new { cname = "wall_area", data = WallData[0] });
            data.Add(new { cname = "wall_u", data = WallData[1] });
            data.Add(new { cname = "wall_du", data = WallData[2] });
            data.Add(new { cname = "wall_ueff", data = WallData[3] });
            data.Add(new { cname = "wall_ht", data = WallData[4] });
            data.Add(new { cname = "wall_shgc", data = WallData[5] });
            data.Add(new { cname = "wall_alpha", data = WallData[6] });
            data.Add(new { cname = "roof_area", data = RoofData[0] });
            data.Add(new { cname = "roof_u", data = RoofData[1] });
            data.Add(new { cname = "roof_du", data = RoofData[2] });
            data.Add(new { cname = "roof_ueff", data = RoofData[3] });
            data.Add(new { cname = "roof_ht", data = RoofData[4] });
            data.Add(new { cname = "roof_shgc", data = RoofData[5] });
            data.Add(new { cname = "roof_alpha", data = RoofData[6] });
            data.Add(new { cname = "floor_area", data = FloorData[0] });
            data.Add(new { cname = "floor_u", data = FloorData[1] });
            data.Add(new { cname = "floor_du", data = FloorData[2] });
            data.Add(new { cname = "floor_ueff", data = FloorData[3] });
            data.Add(new { cname = "floor_ht", data = FloorData[4] });
            data.Add(new { cname = "floor_shgc", data = FloorData[5] });
            data.Add(new { cname = "floor_alpha", data = FloorData[6] });
            data.Add(new { cname = "win_area", data = WinData[0] });
            data.Add(new { cname = "win_u", data = WinData[1] });
            data.Add(new { cname = "win_du", data = WinData[2] });
            data.Add(new { cname = "win_ueff", data = WinData[3] });
            data.Add(new { cname = "win_ht", data = WinData[4] });
            data.Add(new { cname = "win_shgc", data = WinData[5] });
            data.Add(new { cname = "win_alpha", data = WinData[6] });
            data.Add(new { cname = "cw_area", data = CWData[0] });
            data.Add(new { cname = "cw_u", data = CWData[1] });
            data.Add(new { cname = "cw_du", data = CWData[2] });
            data.Add(new { cname = "cw_ueff", data = CWData[3] });
            data.Add(new { cname = "cw_ht", data = CWData[4] });
            data.Add(new { cname = "cw_shgc", data = CWData[5] });
            data.Add(new { cname = "cw_alpha", data = CWData[6] });
            data.Add(new { cname = "door_area", data = DoorData[0] });
            data.Add(new { cname = "door_u", data = DoorData[1] });
            data.Add(new { cname = "door_du", data = DoorData[2] });
            data.Add(new { cname = "door_ueff", data = DoorData[3] });
            data.Add(new { cname = "door_ht", data = DoorData[4] });
            data.Add(new { cname = "door_shgc", data = DoorData[5] });
            data.Add(new { cname = "door_alpha", data = DoorData[6] });
            data.Add(new { cname = "qt_sink_c", data = CoolingMthData[0] });
            data.Add(new { cname = "qv_sink_c", data = CoolingMthData[1] });
            data.Add(new { cname = "qs_sink_c", data = CoolingMthData[2] });
            data.Add(new { cname = "qsink_tot_c", data = CoolingMthData[3] });
            data.Add(new { cname = "qt_source_c", data = CoolingMthData[8] });
            data.Add(new { cname = "qv_source_c", data = CoolingMthData[9] });
            data.Add(new { cname = "qs_source_c", data = CoolingMthData[10] });
            data.Add(new { cname = "qi_source_c", data = CoolingMthData[11] });
            data.Add(new { cname = "qsource_tot_c", data = CoolingMthData[12] });
            data.Add(new { cname = "qb_c", data = CoolingMthData[13] });
            data.Add(new { cname = "eta_c", data = CoolingMthData[14] });
            data.Add(new { cname = "qdhu", data = CoolingMthData[15] });
            
            s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
            s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());

            Debug.Print("start");

            runScript("init(" + s + "," + s2 + "," + "[" + charts + "])");
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
        private void button1_Click(object sender, EventArgs e)
        {
            webView21.CoreWebView2.ShowPrintUI();
        }

    }
}