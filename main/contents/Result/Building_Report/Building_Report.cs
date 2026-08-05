using main.contents.Result.Element_Report;
using main.subcontents.CoolingSystem;
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

namespace main.contents.Result.Building_Report
{
    public partial class Building_Report : Form
    {
        bool scriptable = false;
        public Building_Report()
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
                string s = args.TryGetWebMessageAsString();
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
        public void load_List()
        {
            List<object> MainMenu = new List<object>();

            MainMenu.Add(new { text = "전기소요량", id = "{\\\"formID\\\":52,\\\"ID\\\":\\\"Result_6\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            string[][] Final = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "연료", "not 연료='전기'");
            if (Final.Length > 0)
            {
                MainMenu.Add(new { text = Final[0][0]+"소요량", id = "{\\\"formID\\\":68,\\\"ID\\\":\\\"Result_7\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            }

            Program.UTIL.resetMainTree(6, 0, MainMenu.ToArray(), "56"); // 예시 코드: 메인 메뉴 동적 할당
           
        }

        public void LoadData(string ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();
            string[][] 프로젝트유형 = Program.DB.querySQL(DB.type.ProjListDB, "Select type from projects where current = '1'");
            if (프로젝트유형[0][0] == "1" || 프로젝트유형[0][0] == "4")
            {
                Report_Before();
            }
            else
            {
                Report_After();
            }
        }

        private void Report_Before()
        {
            string s, s2;
            double d;
            string sp;
            string[][] 번호 = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");

            List<object> items = new List<object>();
            List<object> items2 = new List<object>();
            List<object> data = new List<object>();
            List<string> chart_난방소요량 = new List<string>();
            List<string> chart_냉방소요량 = new List<string>();
            List<string> chart_급탕소요량 = new List<string>();
            List<string> chart_조명소요량 = new List<string>();
            List<string> chart_공조소요량 = new List<string>();
            List<string> chart_총소요량 = new List<string>();
            List<object>[] __data = new List<object>[700];
            List<object>[] __Edata = new List<object>[700];

            int i = -1, n;
            while (++i < 700)
            {
                __data[i] = new List<object>();
                __Edata[i] = new List<object>();
            }


            string charts = "";

            i = -1;
            while (++i < 번호.Length)
            {
                #region 건물정보
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트명,주소,지역,지역구분,준공시기,연면적,건축면적,지상층수,지하층수,작성자회사,작성자,작성시기,프로젝트번호,프로젝트유형,건물용도");
                if (Value.Length > 0)
                {
                    __data[0].Add(new { idx = i, val = Value[0][0] }); //프로젝트명
                    __data[1].Add(new { idx = i, val = Value[0][1] }); //주소
                    __data[2].Add(new { idx = i, val = Value[0][2] }); //지역
                    __data[3].Add(new { idx = i, val = Value[0][3] }); //지역구분
                    __data[4].Add(new { idx = i, val = Value[0][4] }); //준공시기
                    __data[5].Add(new { idx = i, val = Program.UTIL.ToDoubleOrZero(Value[0][5]).ToString("0.00") }); //연면적
                    __data[6].Add(new { idx = i, val = Program.UTIL.ToDoubleOrZero(Value[0][6]).ToString("0.00") }); //건축면적
                    __data[7].Add(new { idx = i, val = Value[0][7] }); //지상층수
                    __data[8].Add(new { idx = i, val = Value[0][8] }); //지하층수
                    __data[9].Add(new { idx = i, val = Value[0][9] }); //작성자회사
                    __data[10].Add(new { idx = i, val = Value[0][10] }); //작성자
                    __data[11].Add(new { idx = i, val = Value[0][11] }); //작성시기      
                    __data[136].Add(new { idx = i, val = Value[0][12] }); //프로젝트번호
                    __data[137].Add(new { idx = i, val = Value[0][0]+ " 검토보고서" }); //프로젝트 명칭 
                    __data[138].Add(new { idx = i, val = Value[0][13] }); //프로젝트유형
                    __data[139].Add(new { idx = i, val = Value[0][14] }); //건물용도   
                }
                ////////////////////////////////////////////////////////////////////
                data.Add(new { cname = "projectName", data = __data[0] });
                data.Add(new { cname = "buildinglocation", data = __data[1] });
                data.Add(new { cname = "climate", data = __data[2] });
                data.Add(new { cname = "bylawclimate", data = __data[3] });
                data.Add(new { cname = "constructiondate", data = __data[4] });
                data.Add(new { cname = "grossarea", data = __data[5] });
                data.Add(new { cname = "buildingarea", data = __data[6] });
                data.Add(new { cname = "aboveground", data = __data[7] });
                data.Add(new { cname = "underground", data = __data[8] });
                data.Add(new { cname = "reviewercompany", data = __data[9] });
                data.Add(new { cname = "reviewername", data = __data[10] });
                data.Add(new { cname = "reviewdate", data = __data[11] });
                data.Add(new { cname = "projectnum", data = __data[136] });
                data.Add(new { cname = "projectName2", data = __data[137] });
                data.Add(new { cname = "projectType", data = __data[138] });
                data.Add(new { cname = "buildingType", data = __data[139] });
                #endregion
                #region 온실가스정보

                // 단위면적당CO2  
                
                string[][] 존정보 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호, 일일급탕요구량,순바닥면적","");

                //총순바닥면적 구하기
                double 순바닥면적 = 0;
                
                for(int l =0; l < 존정보.Length; l++)
                {
                    순바닥면적 += Program.UTIL.ToDoubleOrZero(존정보[l][2]);
                }
                //요구량값 가져오기
                double[] 난방요구량 = new double[12], 냉방요구량 = new double[12], 급탕요구량 = new double[12], 조명요구량 = new double[12], 공조요구량 = new double[12];
                             
                for (int mt = 0; mt < 12; mt++)
                {
                    string[][] heat = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_mth", "난방_냉방 = '난방' and 비이용일_이용일='이용일' and 월 ='" + (mt + 1).ToString() + "월'");
                    if (heat.Length > 0)
                    {
                        for (int h = 0; h < heat.Length; h++)
                        {
                            난방요구량[mt] += Program.UTIL.ToDoubleOrZero(heat[h][0]);
                        }
                    }
                    string[][] cool = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_mth", "난방_냉방 = '냉방' and 비이용일_이용일='이용일' and 월 ='" + (mt + 1).ToString() + "월'");
                    if (cool.Length > 0)
                    {
                        for (int h = 0; h < cool.Length; h++)
                        {
                             냉방요구량[mt] += Program.UTIL.ToDoubleOrZero(cool[h][0]);
                        }
                    }
                    string[][] hotw = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Zone_HCneed_Result", "번호, dwd_mth", "비이용일_이용일='이용일' and 월 ='" + (mt + 1).ToString() + "월'");
                    if (hotw.Length > 0)
                    {
                        for(int aaa = 0; aaa<hotw.Length ; aaa++)
                        {
                            for (int k = 0; k < 존정보.Length; k++)
                            {
                                if (존정보[k][0] == hotw[aaa][0])
                                {
                                    급탕요구량[mt] += Program.UTIL.ToDoubleOrZero(hotw[aaa][1]) * Program.UTIL.ToDoubleOrZero(존정보[k][1]);
                                }
                            }
                        }
                    }
                   
                    string[][] 요구량2 = Program.DB.getValue(DB.type.ProjDB, "Zone_LightResult", "Final_kWh", "월 ='" + (mt + 1).ToString() + "월'");
                    if (요구량2.Length > 0)
                    {
                        for (int h = 0; h < 요구량2.Length; h++)
                        {
                            조명요구량[mt] += Program.UTIL.ToDoubleOrZero(요구량2[h][0]);
                        }
                    }
                    string[][] 요구량3 = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "공조요구량,가습요구량", "월 ='" + (mt + 1).ToString() + "월'");
                    if (요구량3.Length > 0)
                    {
                        for (int h = 0; h < 요구량3.Length; h++)
                        {
                            공조요구량[mt] += Program.UTIL.ToDoubleOrZero(요구량3[h][0]) + Program.UTIL.ToDoubleOrZero(요구량3[h][1]);
                        }
                    }
                }
                //요구량값 구하기
                double 난방요 = 0, 냉방요 = 0, 급탕요 = 0, 조명요 = 0, 공조요 = 0, 탄소배출량=0, 요구량합계=0;
                for(int val = 0; val<12; val++)
                {
                    난방요 += 난방요구량[val];
                    냉방요 += 냉방요구량[val];
                    급탕요 += 급탕요구량[val];
                    조명요 += 조명요구량[val];
                    공조요 += 공조요구량[val];
                }
                요구량합계 = (난방요 + 냉방요 + 급탕요 + 조명요 + 공조요) / 순바닥면적;
                __data[140].Add(new { idx = i, val = (난방요 / 순바닥면적).ToString("0.0")}); //난방에너지요구량
                __data[141].Add(new { idx = i, val = (냉방요 / 순바닥면적).ToString("0.0")}); //냉방에너지요구량
                __data[142].Add(new { idx = i, val = (급탕요 / 순바닥면적).ToString("0.0")}); //급탕에너지요구량
                __data[143].Add(new { idx = i, val = (조명요 / 순바닥면적).ToString("0.0") }); //조명에너지요구량
                __data[144].Add(new { idx = i, val = (공조요 / 순바닥면적).ToString("0.0") }); //공조에너지요구량
                __data[145].Add(new { idx = i, val = 순바닥면적.ToString("0.00") }); //순바닥면적
                __data[146].Add(new { idx = i, val = 요구량합계.ToString("#,##0") }); //요구량합계

                data.Add(new { cname = "heatingNeeds", data = __data[140] });
                data.Add(new { cname = "coolingNeeds", data = __data[141] });
                data.Add(new { cname = "hotwaterNeeds", data = __data[142] });
                data.Add(new { cname = "lightNeeds", data = __data[143] });
                data.Add(new { cname = "ventNeeds", data = __data[144] });
                data.Add(new { cname = "energyArea", data = __data[145] });
                data.Add(new { cname = "sumNeeds", data = __data[146] });

                #endregion
                #region 외벽정보
                Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='외벽'");
                if (Value.Length > 0)
                {
                    __Edata[0].Add(new { idx = i, val = Value.Length }); //외벽 유형 개수
                    __Edata[1].Add(new { idx = i, val = Value.Length }); //외벽 유형 개수
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호");
                if (Value.Length > 0)
                {
                    double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                    for (int k = 0; k < Value.Length; k++)
                    {
                        Total_Area += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                        Uvalue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                        RuleValue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                    }
                    Uvalue = Uvalue / Total_Area;
                    RuleValue = RuleValue / Total_Area;
                    __Edata[2].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //외벽 면적
                    __Edata[3].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //외벽 면적
                    __Edata[4].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //외벽 유효 열관류율
                    __Edata[5].Add(new { idx = i, val = RuleValue.ToString("0.00") }); //외벽 법규 열관류율
                    d = RuleValue / Uvalue * 100; if (d >= 100) { d = 100; }
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __Edata[6].Add(new { idx = i, val = sp }); //법규 열관류율                     
                }
                ////////////////////////////////////////////////////////////////////
                data.Add(new { cname = "wall_count", data = __Edata[0] });
                data.Add(new { cname = "wall_count2", data = __Edata[1] });
                data.Add(new { cname = "wall_area", data = __Edata[2] }); ;
                data.Add(new { cname = "wall_area2", data = __Edata[3] });
                data.Add(new { cname = "wall_uvalue", data = __Edata[4] });
                data.Add(new { cname = "wall_rulevalue", data = __Edata[5] });
                data.Add(new { cname = "wall_rulevalue_point", data = __Edata[6] });
                #endregion
                #region 지붕정보
                Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='지붕'");
                if (Value.Length > 0)
                {
                    __Edata[7].Add(new { idx = i, val = Value.Length }); //지붕 유형 개수
                    __Edata[8].Add(new { idx = i, val = Value.Length }); //지붕 유형 개수
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionRoof AS b ON a.구조체번호 = b.번호");
                if (Value.Length > 0)
                {
                    double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                    for (int k = 0; k < Value.Length; k++)
                    {
                        Total_Area += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                        Uvalue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                        RuleValue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                    }
                    Uvalue = Uvalue / Total_Area;
                    RuleValue = RuleValue / Total_Area;
                    __Edata[9].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //지붕 면적
                    __Edata[10].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //지붕 면적
                    __Edata[11].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //지붕 유효 열관류율
                    __Edata[12].Add(new { idx = i, val = RuleValue.ToString("0.00") }); //지붕 법규 열관류율     
                    d = RuleValue / Uvalue * 100; if (d >= 100) { d = 100; }
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __Edata[13].Add(new { idx = i, val = sp }); //법규 열관류율                
                }
                ////////////////////////////////////////////////////////////////////
                data.Add(new { cname = "roof_count", data = __Edata[7] });
                data.Add(new { cname = "roof_count2", data = __Edata[8] });
                data.Add(new { cname = "roof_area", data = __Edata[9] });
                data.Add(new { cname = "roof_area2", data = __Edata[10] });
                data.Add(new { cname = "roof_uvalue", data = __Edata[11] });
                data.Add(new { cname = "roof_rulevalue", data = __Edata[12] });
                data.Add(new { cname = "roof_rulevalue_point", data = __Edata[13] });
                #endregion
                #region 최하층바닥정보
                Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='최하층바닥'");
                if (Value.Length > 0)
                {
                    __Edata[14].Add(new { idx = i, val = Value.Length }); //바닥 유형 
                    __Edata[15].Add(new { idx = i, val = Value.Length }); //바닥 유형 개수
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionFloor AS b ON a.구조체번호 = b.번호");
                if (Value.Length > 0)
                {
                    double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                    for (int k = 0; k < Value.Length; k++)
                    {
                        Total_Area += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                        Uvalue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                        RuleValue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                    }
                    Uvalue = Uvalue / Total_Area;
                    RuleValue = RuleValue / Total_Area;
                    __Edata[16].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //바닥 면적
                    __Edata[17].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //바닥 면적
                    __Edata[18].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //바닥 유효 열관류율
                    __Edata[19].Add(new { idx = i, val = RuleValue.ToString("0.00") }); //바닥 법규 열관류율  
                    d = RuleValue / Uvalue * 100; if (d >= 100) { d = 100; }
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __Edata[20].Add(new { idx = i, val = sp }); //법규 열관류율                   
                }
                ////////////////////////////////////////////////////////////////////
                data.Add(new { cname = "floor_count", data = __Edata[14] });
                data.Add(new { cname = "floor_count2", data = __Edata[15] });
                data.Add(new { cname = "floor_area", data = __Edata[16] });
                data.Add(new { cname = "floor_area2", data = __Edata[17] });
                data.Add(new { cname = "floor_uvalue", data = __Edata[18] });
                data.Add(new { cname = "floor_rulevalue", data = __Edata[19] });
                data.Add(new { cname = "floor_rulevalue_point", data = __Edata[20] });
                #endregion
                #region 창호정보
                Value = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='창호'");
                if (Value.Length > 0)
                {
                    __Edata[21].Add(new { idx = i, val = Value.Length }); //창호 유형 개수
                    __Edata[22].Add(new { idx = i, val = Value.Length }); //창호 유형 개수
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.창호유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체번호 = b.번호");
                if (Value.Length > 0)
                {
                    double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                    for (int k = 0; k < Value.Length; k++)
                    {
                        Total_Area += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                        Uvalue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                        RuleValue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                    }
                    Uvalue = Uvalue / Total_Area;
                    RuleValue = RuleValue / Total_Area;
                    __Edata[23].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //창호 면적
                    __Edata[24].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //창호 면적
                    __Edata[25].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //창호 유효 열관류율
                    __Edata[26].Add(new { idx = i, val = RuleValue.ToString("0.00") }); //창호 법규 열관류율  
                    d = RuleValue / Uvalue * 100; if (d >= 100) { d = 100; }
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __Edata[27].Add(new { idx = i, val = sp }); //법규 열관류율               
                }
                ////////////////////////////////////////////////////////////////////
                data.Add(new { cname = "win_count", data = __Edata[21] });
                data.Add(new { cname = "win_count2", data = __Edata[22] });
                data.Add(new { cname = "win_area", data = __Edata[23] });
                data.Add(new { cname = "win_area2", data = __Edata[24] });
                data.Add(new { cname = "win_uvalue", data = __Edata[25] });
                data.Add(new { cname = "win_rulevalue", data = __Edata[26] });
                data.Add(new { cname = "win_rulevalue_point", data = __Edata[27] });
                #endregion
                #region 커튼월창정보
                Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='커튼월창'");

                __Edata[28].Add(new { idx = i, val = Value.Length }); //창호 유형 개수
                __Edata[29].Add(new { idx = i, val = Value.Length }); //창호 유형 개수

                double Total_Area_CW = 0, Uvalue_CW = 0, RuleValue_CW = 0;
                Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.유리부분유효열관류율,b.법규유리부분열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionCW AS b ON a.구조체번호 = b.번호 where a.커튼월부위 ='유리부분'");
                for (int k = 0; k < Value.Length; k++)
                {
                    if (Value.Length > 0)
                    {
                        Total_Area_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                        Uvalue_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                        RuleValue_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                    }
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.패널부분유효열관류율,b.법규패널부분열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionCW AS b ON a.구조체번호 = b.번호 where a.커튼월부위 ='패널부분'");
                for (int k = 0; k < Value.Length; k++)
                {
                    if (Value.Length > 0)
                    {
                        Total_Area_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                        Uvalue_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                        RuleValue_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                    }
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.출입문부분유효열관류율,b.법규출입문부분열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionCW AS b ON a.구조체번호 = b.번호 where a.커튼월부위 ='출입문부분'");
                for (int k = 0; k < Value.Length; k++)
                {
                    if (Value.Length > 0)
                    {
                        Total_Area_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                        Uvalue_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                        RuleValue_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                    }
                }
                Uvalue_CW = double.IsNaN(Uvalue_CW / Total_Area_CW) ? 0 : Uvalue_CW / Total_Area_CW;
                RuleValue_CW = double.IsNaN(RuleValue_CW / Total_Area_CW) ? 0 : RuleValue_CW / Total_Area_CW;
                __Edata[30].Add(new { idx = i, val = Total_Area_CW.ToString("0.0") }); //커튼월창 면적
                __Edata[31].Add(new { idx = i, val = Total_Area_CW.ToString("0.0") }); //커튼월창 면적
                __Edata[32].Add(new { idx = i, val = Uvalue_CW.ToString("0.00") }); //커튼월창 유효 열관류율
                __Edata[33].Add(new { idx = i, val = RuleValue_CW.ToString("0.00") }); //커튼월창 법규 열관류율 
                d = RuleValue_CW / Uvalue_CW * 100; if (d >= 100) { d = 100; }
                if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                __Edata[34].Add(new { idx = i, val = sp }); //법규 열관류율   

                data.Add(new { cname = "cw_count", data = __Edata[28] });
                data.Add(new { cname = "cw_count2", data = __Edata[29] });
                data.Add(new { cname = "cw_area", data = __Edata[30] });
                data.Add(new { cname = "cw_area2", data = __Edata[31] });
                data.Add(new { cname = "cw_uvalue", data = __Edata[32] });
                data.Add(new { cname = "cw_rulevalue", data = __Edata[33] });
                data.Add(new { cname = "cw_rulevalue_point", data = __Edata[34] });
                #endregion
                #region 출입문정보
                Value = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='외부출입문'");
                __Edata[35].Add(new { idx = i, val = Value.Length }); //출입문 유형 개수
                __Edata[36].Add(new { idx = i, val = Value.Length }); //출입문 유형 개수

                Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.문유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionDoor AS b ON a.구조체번호 = b.번호");
                if (Value.Length > 0)
                {
                    double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                    for (int k = 0; k < Value.Length; k++)
                    {
                        Total_Area += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                        Uvalue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                        RuleValue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                    }
                    Uvalue = double.IsNaN(Uvalue / Total_Area) ? 0 : Uvalue / Total_Area;
                    RuleValue = double.IsNaN(RuleValue / Total_Area) ? 0 : RuleValue / Total_Area;
                    __Edata[37].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //출입문 면적
                    __Edata[38].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //출입문 면적
                    __Edata[39].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //출입문 유효 열관류율
                    __Edata[40].Add(new { idx = i, val = RuleValue.ToString("0.00") }); //출입문 법규 열관류율   
                    d = RuleValue / Uvalue * 100; if (d >= 100) { d = 100; }
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __Edata[41].Add(new { idx = i, val = sp }); //법규 열관류율     
                }
                ////////////////////////////////////////////////////////////////////
                data.Add(new { cname = "door_count", data = __Edata[35] });
                data.Add(new { cname = "door_count2", data = __Edata[36] });
                data.Add(new { cname = "door_area", data = __Edata[37] });
                data.Add(new { cname = "door_area2", data = __Edata[38] });
                data.Add(new { cname = "door_uvalue", data = __Edata[39] });
                data.Add(new { cname = "door_rulevalue", data = __Edata[40] });
                data.Add(new { cname = "door_rulevalue_point", data = __Edata[41] });
                #endregion
                #region 기밀
                double n50 = 0;
                string[][] nValue = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "n50");
                if (nValue.Length > 0)
                {
                    n50 = Program.UTIL.ToDoubleOrZero(nValue[0][0]);
                    __Edata[42].Add(new { idx = i, val =n50.ToString("0.00") }); //n50
                    __Edata[43].Add(new { idx = i, val = (0.6).ToString("0.00") }); //패시브하우스 n50
                }
                double CMH = 0;
                double CMH_rule = 0;
                double Volume = 0;
                string[][] ZoneV = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적,천장고", "");
                if (ZoneV.Length > 0)
                {
                    for (int a = 0; a < ZoneV.Length; a++)
                    {
                        Volume += Program.UTIL.ToDoubleOrZero(ZoneV[a][0]) * Program.UTIL.ToDoubleOrZero(ZoneV[a][1]);
                    }
                    CMH = n50 * Volume;
                    CMH_rule = 0.6 * Volume;
                    __Edata[44].Add(new { idx = i, val = CMH.ToString("0") }); 
                    __Edata[45].Add(new { idx = i, val = CMH_rule.ToString("0") });
                }
                d = 0.6 / n50 * 100; if (d >= 100) { d = 100; }
                if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                __Edata[46].Add(new { idx = i, val = sp }); // 패시브 0.6회
                data.Add(new { cname = "n50", data = __Edata[42] });
                data.Add(new { cname = "n50_rule", data = __Edata[43] });
                data.Add(new { cname = "cmh", data = __Edata[44] });
                data.Add(new { cname = "cmh_rule", data = __Edata[45] });
                data.Add(new { cname = "n50_rulevalue_point", data = __Edata[46] });


                #endregion
                #region 열교
                double utb = 0, area_wall = 0, area_roof = 0, area_floor = 0, area_sum=0;
                string[][] tValue = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "외벽dUtb,지붕dUtb,바닥dUtb");
                if (tValue.Length > 0)
                {
                    string[][] ZoneE = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "Sum(면적)", "외피유형='외벽'");
                    if (ZoneE.Length > 0)
                    {
                        utb += Program.UTIL.ToDoubleOrZero(tValue[0][0]) * Program.UTIL.ToDoubleOrZero(ZoneE[0][0]);
                        area_sum +=  Program.UTIL.ToDoubleOrZero(ZoneE[0][0]);
                    }
                    ZoneE = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "Sum(면적)", "외피유형='지붕'");
                    if (ZoneE.Length > 0)
                    {
                        utb += Program.UTIL.ToDoubleOrZero(tValue[0][1]) * Program.UTIL.ToDoubleOrZero(ZoneE[0][0]);
                        area_sum += Program.UTIL.ToDoubleOrZero(ZoneE[0][0]);
                    }
                    ZoneE = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "Sum(면적)", "외피유형='최하층바닥'");
                    if (ZoneE.Length > 0)
                    {
                        utb += Program.UTIL.ToDoubleOrZero(tValue[0][2]) * Program.UTIL.ToDoubleOrZero(ZoneE[0][0]);
                        area_sum += Program.UTIL.ToDoubleOrZero(ZoneE[0][0]);
                    }
                    utb = utb / area_sum;
                    __Edata[47].Add(new { idx = i, val = utb.ToString("0.00") });
                    __Edata[48].Add(new { idx = i, val = (0.1).ToString("0.00") });
                }
                d = 0.1 / utb * 100; if (d >= 100) { d = 100; }
                if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                __Edata[49].Add(new { idx = i, val = sp }); //0.1 W/m2k
                data.Add(new { cname = "utb", data = __Edata[47] });
                data.Add(new { cname = "utb_rule", data = __Edata[48] });
                data.Add(new { cname = "utb_rulevalue_point", data = __Edata[49] });
                #endregion
                #region 난방설비
                string[][] Hvalue = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "번호,명칭,주요설비,보일러종류,외기히트펌프번호,흡수식온수기번호,지역난방번호,태양열번호,지열히트펌프번호,지하수히트펌프번호", "");
                string[][] count_ = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "번호,명칭,주요설비,보일러대수,외기히트펌프대수,흡수식온수기대수,지역난방번호,모듈개수,지열히트펌프대수,지하수히트펌프대수", "");
                if (Hvalue.Length > 0 && count_.Length > 0)
                {
                    double power = 0, power_tot =0, eta=0, eta_rule = 0; string unit="W/W"; 
                    string[][] SystemValue;
                    for (int a = 0; a < Hvalue.Length; a++)
                    {
                        //"보일러", "외기 히트펌프", "지열 히트펌프", "지하수 히트펌프", "태양열 융합 히트펌프", "흡수식온수기", "지역난방", "태양열시스템" 
                        if (Hvalue[a][2] == "보일러")
                        {
                            SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "용량,전부하효율", "번호 ='" + Hvalue[a][3] + "'");
                            if (SystemValue.Length > 0)
                            {
                                power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][3]);
                                power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][3]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][3]) : power ;
                                eta = power== Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][3]) ? Program.UTIL.ToDoubleOrZero(SystemValue[0][1]) : eta;
                                eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][3]) ? 90 : eta_rule ;
                                unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][3]) ? "%" : unit;
                            }
                        }
                        else if (Hvalue[a][2] == "외기 히트펌프")
                        {
                            SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", "난방정격용량,난방정격COP", "번호 ='" + Hvalue[a][4] + "'");
                            if (SystemValue.Length > 0)
                            {
                                power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][4]);
                                power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][4]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][4]) : power;
                                eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][4]) ? Program.UTIL.ToDoubleOrZero(SystemValue[0][1]) : eta;
                                eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][4]) ? 3.8 : eta_rule;
                                unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][4]) ? "W/W" : unit;
                            }
                        }
                        else if (Hvalue[a][2] == "흡수식온수기")
                        {
                            SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_ABS", "난방용량,난방성능", "번호 ='" + Hvalue[a][5] + "'");
                            if (SystemValue.Length > 0)
                            {
                                power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][5]);
                                power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][5]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][5]) : power;
                                eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][5]) ? Program.UTIL.ToDoubleOrZero(SystemValue[0][1]) : eta;
                                eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][5]) ? 1.2 : eta_rule;
                                unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][5]) ? "W/W" : unit;
                            }
                        }
                        else if (Hvalue[a][2] == "지역난방")
                        {
                            SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_DH", "용량", "번호 ='" + Hvalue[a][6] + "'");
                            if (SystemValue.Length > 0)
                            {
                                power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]);
                                power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) : power;
                                eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) ? 100 : eta;
                                eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) ? 100 : eta_rule;
                                unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) ? "%" : unit;
                            }
                        }
                        else if (Hvalue[a][2] == "지열 히트펌프")
                        {
                            SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_GroundHP", "난방정격용량,난방정격COP", "번호 ='" + Hvalue[a][8] + "'");
                            if (SystemValue.Length > 0)
                            {
                                power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][8]);
                                power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][8]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][8]) : power;
                                eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][8]) ? Program.UTIL.ToDoubleOrZero(SystemValue[0][1]) : eta;
                                eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][8]) ? 3.8 : eta_rule;
                                unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][8]) ? "W/W" : unit;
                            }
                        }
                        else if (Hvalue[a][2] == "지하수 히트펌프")
                        {
                            SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_GroundWHP", "난방정격용량,난방정격COP", "번호 ='" + Hvalue[a][9] + "'");
                            if (SystemValue.Length > 0)
                            {
                                power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][9]);
                                power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][9]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][9]) : power;
                                eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][9]) ? Program.UTIL.ToDoubleOrZero(SystemValue[0][1]) : eta;
                                eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][9]) ? 3.8 : eta_rule;
                                unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][9]) ? "W/W" : unit;
                            }
                        }
                    }
                    __Edata[50].Add(new { idx = i, val = Hvalue.Length }); //개수
                    __Edata[51].Add(new { idx = i, val = Hvalue.Length }); //개수
                    __Edata[52].Add(new { idx = i, val = power_tot.ToString("0.0") }); //용량
                    __Edata[53].Add(new { idx = i, val = power_tot.ToString("0.0") }); //용량
                    __Edata[54].Add(new { idx = i, val = eta.ToString("0.0") }); //효율
                    __Edata[55].Add(new { idx = i, val = eta_rule.ToString("0.0") }); //권장효율
                    __Edata[56].Add(new { idx = i, val = unit }); //효율 단위
                    __Edata[57].Add(new { idx = i, val = unit }); //권장효율 단위
                    d = eta / eta_rule * 100; if (d >= 100) { d = 100; }
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __Edata[58].Add(new { idx = i, val = sp });

                    data.Add(new { cname = "h_count", data = __Edata[50] });
                    data.Add(new { cname = "h_count2", data = __Edata[51] });
                    data.Add(new { cname = "h_power", data = __Edata[52] });
                    data.Add(new { cname = "h_power2", data = __Edata[53] });
                    data.Add(new { cname = "h_eta", data = __Edata[54] });
                    data.Add(new { cname = "h_eta_rule", data = __Edata[55] });
                    data.Add(new { cname = "h_unit", data = __Edata[56] });
                    data.Add(new { cname = "h_unit2", data = __Edata[57] });
                    data.Add(new { cname = "h_point", data = __Edata[58] });
                }

                #endregion
                #region 급탕설비
                string[][] Dvalue = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "번호,명칭,주요설비,보일러종류,히트펌프번호,지역난방번호", "");
                string[][] count__= Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "번호,명칭,주요설비,보일러대수,히트펌프대수,지역난방번호", "");
                if (Dvalue.Length > 0 && count__.Length > 0)
                {
                    double power = 0, power_tot = 0, eta = 0, eta_rule = 0; string unit = "W/W";
                    string[][] SystemValue;
                    for (int a = 0; a < Dvalue.Length; a++)
                    {
                        //"보일러", "외기 히트펌프", "지열 히트펌프", "지하수 히트펌프", "태양열 융합 히트펌프", "흡수식온수기", "지역난방", "태양열시스템" 
                        if (Dvalue[a][2] == "보일러")
                        {
                            SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "용량,전부하효율", "번호 ='" + Dvalue[a][3] + "'");
                            if (SystemValue.Length > 0)
                            {
                                power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][3]);
                                power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][3]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][3]) : power;
                                eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][3]) ? Program.UTIL.ToDoubleOrZero(SystemValue[0][1]) : eta;
                                eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][3]) ? 90 : eta_rule;
                                unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][3]) ? "%" : unit;
                            }
                        }
                        else if (Dvalue[a][2] == "외기 히트펌프")
                        {
                            SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_DHWHP", "급탕정격용량,급탕정격COP", "번호 ='" + Dvalue[a][4] + "'");
                            if (SystemValue.Length > 0)
                            {
                                power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][4]);
                                power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][4]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][4]) : power;
                                eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][4]) ? Program.UTIL.ToDoubleOrZero(SystemValue[0][1]) : eta;
                                eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][4]) ? 3.8 : eta_rule;
                                unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][4]) ? "W/W" : unit;
                            }
                        }
                        else if (Dvalue[a][2] == "지역난방")
                        {
                            SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_DH", "용량", "번호 ='" + Dvalue[a][5] + "'");
                            if (SystemValue.Length > 0)
                            {
                                power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]);
                                power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) : power;
                                eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) ? 100 : eta;
                                eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) ? 100 : eta_rule;
                                unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) ? "%" : unit;
                            }
                        }
                    }
                    __Edata[60].Add(new { idx = i, val = Dvalue.Length }); //개수
                    __Edata[61].Add(new { idx = i, val = Dvalue.Length }); //개수
                    __Edata[62].Add(new { idx = i, val = power_tot.ToString("0.0") }); //용량
                    __Edata[63].Add(new { idx = i, val = power_tot.ToString("0.0") }); //용량
                    __Edata[64].Add(new { idx = i, val = eta.ToString("0.0") }); //효율
                    __Edata[65].Add(new { idx = i, val = eta_rule.ToString("0.0") }); //권장효율
                    __Edata[66].Add(new { idx = i, val = unit }); //효율 단위
                    __Edata[67].Add(new { idx = i, val = unit }); //권장효율 단위
                    d = eta / eta_rule * 100; if (d >= 100) { d = 100; }
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __Edata[68].Add(new { idx = i, val = sp });

                    data.Add(new { cname = "w_count", data = __Edata[60] });
                    data.Add(new { cname = "w_count2", data = __Edata[61] });
                    data.Add(new { cname = "w_power", data = __Edata[62] });
                    data.Add(new { cname = "w_power2", data = __Edata[63] });
                    data.Add(new { cname = "w_eta", data = __Edata[64] });
                    data.Add(new { cname = "w_eta_rule", data = __Edata[65] });
                    data.Add(new { cname = "w_unit", data = __Edata[66] });
                    data.Add(new { cname = "w_unit2", data = __Edata[67] });
                    data.Add(new { cname = "w_point", data = __Edata[68] });
                }

                #endregion
                #region 냉방설비
                string[][] Cvalue = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "냉방출력,냉방성능", "");
                if(Cvalue.Length > 0)
                {
                    double power_tot = 0, power_max = 0, eta =0, eta_rule =3.4 ;
                    for(int a =0; a< Cvalue.Length; a++)
                    {
                        power_tot += Program.UTIL.ToDoubleOrZero(Cvalue[a][0]);
                        power_max =  power_max < Program.UTIL.ToDoubleOrZero(Cvalue[a][0]) ? Program.UTIL.ToDoubleOrZero(Cvalue[0][0]) : power_max;
                        eta = power_max == Program.UTIL.ToDoubleOrZero(Cvalue[a][0]) ? Program.UTIL.ToDoubleOrZero(Cvalue[0][1]) : eta;
                    }
                    __Edata[70].Add(new { idx = i, val = Cvalue.Length }); //개수
                    __Edata[71].Add(new { idx = i, val = Cvalue.Length }); //개수
                    __Edata[72].Add(new { idx = i, val = power_tot.ToString("0.0") }); //용량
                    __Edata[73].Add(new { idx = i, val = power_tot.ToString("0.0") }); //용량
                    __Edata[74].Add(new { idx = i, val = eta.ToString("0.0") }); //효율
                    __Edata[75].Add(new { idx = i, val = eta_rule.ToString("0.0") }); //권장효율
                    d = eta / eta_rule * 100; if (d >= 100) { d = 100; }
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __Edata[76].Add(new { idx = i, val = sp });
                    data.Add(new { cname = "c_count", data = __Edata[70] });
                    data.Add(new { cname = "c_count2", data = __Edata[71] });
                    data.Add(new { cname = "c_power", data = __Edata[72] });
                    data.Add(new { cname = "c_power2", data = __Edata[73] });
                    data.Add(new { cname = "c_eta", data = __Edata[74] });
                    data.Add(new { cname = "c_eta_rule", data = __Edata[75] });
                    data.Add(new { cname = "c_point", data = __Edata[76] });
                }
                #endregion
                #region 조명정보   
                string light_count; double light_density = 0; double light_eta_avg = 0; double light_eta_rule = 0; double light_point = 0; double light_area = 0;
                Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneLighting_form", "조명번호");
                if (Value.Length > 0)
                {
                    light_count = "-";
                    for (int a = 0; a < Value.Length; a++)
                    {
                        string[][] 조명존 = Program.DB.querySQL(DB.type.ProjDB, "Select a.번호,a.순바닥면적,a.조명밀도,a.등기구명칭,a.광효율,a.자연채광유형,b.기존존 From ZoneLighting_form as a Inner Join ZoneGeneral_Form as b on a.번호 =b.존번호 where a.조명번호='" + Value[a][0] + "'");
                        if (조명존.Length > 0)
                        {
                            for (int aa = 0; aa < 조명존.Length; aa++)
                            {
                                light_area = Program.UTIL.ToDoubleOrZero(조명존[aa][1]);
                                light_density = Program.UTIL.ToDoubleOrZero(조명존[aa][1]) * Program.UTIL.ToDoubleOrZero(조명존[aa][2]);
                                light_eta_avg = Program.UTIL.ToDoubleOrZero(조명존[aa][1]) * Program.UTIL.ToDoubleOrZero(조명존[aa][4]);
                            }
                        }
                    }
                    //light_density = light_density / light_area;
                    light_eta_avg = light_eta_avg / light_area;
                    light_eta_rule = 70;
                    light_point = Math.Min(100, light_eta_avg / light_eta_rule * 100);
                    __Edata[80].Add(new { idx = i, val = light_count }); //개수 
                    __Edata[81].Add(new { idx = i, val = light_count }); //개수 
                    __Edata[82].Add(new { idx = i, val = light_density.ToString("0.0")  }); //용량
                    __Edata[83].Add(new { idx = i, val = light_density.ToString("0.0") }); //용량
                    __Edata[84].Add(new { idx = i, val = light_eta_avg.ToString("0")  }); //성능
                    __Edata[85].Add(new { idx = i, val = light_eta_rule.ToString("0")}); //권장 성능                
                    d = light_point;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __Edata[86].Add(new { idx = i, val = sp }); //성능점수       
                    data.Add(new { cname = "l_count", data = __Edata[80] });
                    data.Add(new { cname = "l_count2", data = __Edata[81] });
                    data.Add(new { cname = "l_power", data = __Edata[82] });
                    data.Add(new { cname = "l_power2", data = __Edata[83] });
                    data.Add(new { cname = "l_eta", data = __Edata[84] });
                    data.Add(new { cname = "l_eta_rule", data = __Edata[85] });
                    data.Add(new { cname = "l_point", data = __Edata[86] });
                }

                #endregion
                #region 태양광정보   
                double pv_count = 0; double pv_power = 0; double pv_eta_avg = 0; string pv_eta_rule = "-"; double pv_point = 0;
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select a.번호,a.명칭,a.모듈번호,a.개수,a.개수,a.용량,a.면적,b.CELLTYPE,b.Kpk From PV_Form as a inner Join User_PV as b on a.모듈번호=b.번호");
                if (Value.Length > 0)
                {

                    for (int a = 0; a < Value.Length; a++)
                    {
                        pv_count += Program.UTIL.ToDoubleOrZero(Value[a][3]);
                        pv_power += Program.UTIL.ToDoubleOrZero(Value[a][5]);
                        pv_eta_avg += Program.UTIL.ToDoubleOrZero(Value[a][8]) * Program.UTIL.ToDoubleOrZero(Value[a][5])*100;
                    }
                    pv_eta_avg = pv_eta_avg / pv_power;
                    pv_point = 100;
                    __Edata[90].Add(new { idx = i, val = pv_count.ToString("0") }); //개수 
                    __Edata[91].Add(new { idx = i, val = pv_count.ToString("0") }); //개수 
                    __Edata[92].Add(new { idx = i, val = pv_power.ToString("0.0") }); //용량
                    __Edata[93].Add(new { idx = i, val = pv_power.ToString("0.0") }); //용량
                    __Edata[94].Add(new { idx = i, val = pv_eta_avg.ToString("0.0")  }); //성능
                    __Edata[95].Add(new { idx = i, val = pv_eta_rule.ToString() }); //권장 성능                
                    d = pv_point;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __Edata[96].Add(new { idx = i, val = sp }); //성능점수    
                }
                data.Add(new { cname = "pv_count", data = __Edata[90] });
                data.Add(new { cname = "pv_count2", data = __Edata[91] });
                data.Add(new { cname = "pv_power", data = __Edata[92] });
                data.Add(new { cname = "pv_power2", data = __Edata[93] });
                data.Add(new { cname = "pv_eta_avg", data = __Edata[94] });
                data.Add(new { cname = "pv_eta_rule", data = __Edata[95] });
                data.Add(new { cname = "pv_point", data = __Edata[96] });
                #endregion
                #region 연료전지정보
                double FC_power = 0;
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select a.연료전지번호,a.연료전지대수,b.열출력,b.전기출력 From HeatingSystem_Form as a inner Join User_FC as b on a.연료전지번호=b.번호 Where Not a.연료전지번호=''");
                if(Value.Length >0)
                {
                    for (int a = 0; a < Value.Length; a++)
                    {
                        FC_power += (Program.UTIL.ToDoubleOrZero(Value[a][2])+ Program.UTIL.ToDoubleOrZero(Value[a][3])) * Program.UTIL.ToDoubleOrZero(Value[a][1]);
                    }
                    __Edata[200].Add(new { idx = i, val = FC_power.ToString("0.0") }); //용량
                }
                data.Add(new { cname = "fc_power", data = __Edata[200] });
                #endregion
                #region 풍력정보
                double WP_power = 0;
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select a.번호,a.설치대수,b.정격출력 From WindPower_Form as a inner Join User_WP as b on a.풍력=b.번호");
                if (Value.Length > 0)
                {
                    for (int a = 0; a < Value.Length; a++)
                    {
                        WP_power += Program.UTIL.ToDoubleOrZero(Value[a][1])  * Program.UTIL.ToDoubleOrZero(Value[a][2])/1000;
                    }
                    __Edata[201].Add(new { idx = i, val = WP_power.ToString("0.0") }); //용량
                }
                data.Add(new { cname = "wp_power", data = __Edata[201] });
                #endregion

                #region 소요량
                double[] 난방 = new double[12], 냉방 = new double[12], 급탕 = new double[12], 조명 = new double[12], 공조 = new double[12], 기저 = new double[12], 신재생 = new double[12], 총전기 = new double[12], 총가스 = new double[12], 총소요량 = new double[12];
                double 연간소요량 = 0, 연간전기 = 0, 연간가스 = 0;
                for (int mth = 0; mth < 12; mth++)
                {
                    string[][] RES1 = Program.DB.getValue(DB.type.ProjDB,"RESystem_Result","SUM(총에너지)", "생산유형='전기'and 월 ='" + (mth + 1).ToString() + "월'");
                    string[][] RES2 = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "SUM(총에너지)", "not 생산유형='전기'and 월 ='" + (mth + 1).ToString() + "월'");
                    string[][] Final1 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량", "연료='전기' and 월 ='" + (mth + 1).ToString() + "월'");
                    string[][] Final2 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량", "not 연료='전기' and not 연료='전체'  and 월 ='" + (mth + 1).ToString() + "월'");
                    if (Final1.Length > 0)
                    {
                        난방[mth] = Program.UTIL.ToDoubleOrZero(Final1[0][0]);
                        냉방[mth] = Program.UTIL.ToDoubleOrZero(Final1[0][1]);
                        급탕[mth] = Program.UTIL.ToDoubleOrZero(Final1[0][2]);
                        조명[mth] = Program.UTIL.ToDoubleOrZero(Final1[0][3]);
                        공조[mth] = Program.UTIL.ToDoubleOrZero(Final1[0][4]);
                        if (Final1[0][5] != null && Final1[0][5] != "")
                        {
                            기저[mth] = Program.UTIL.ToDoubleOrZero(Final1[0][5]);
                        }
                        if (RES1.Length > 0 && RES1[0][0]!="")
                        { 신재생[mth] = Program.UTIL.ToDoubleOrZero(RES1[0][0]); }
                        총전기[mth] = Program.UTIL.ToDoubleOrZero(Final1[0][7]) - 기저[mth] ;
                    }
                    if (Final2.Length > 0)
                    {
                        난방[mth] = 난방[mth] + Program.UTIL.ToDoubleOrZero(Final2[0][0]);
                        냉방[mth] = 냉방[mth] + Program.UTIL.ToDoubleOrZero(Final2[0][1]);
                        급탕[mth] = 급탕[mth] + Program.UTIL.ToDoubleOrZero(Final2[0][2]);
                        조명[mth] = 조명[mth] + Program.UTIL.ToDoubleOrZero(Final2[0][3]);
                        공조[mth] = 공조[mth] + Program.UTIL.ToDoubleOrZero(Final2[0][4]);
                        if (Final2[0][5] != null && Final2[0][5] != "")
                        {
                            기저[mth] =  Program.UTIL.ToDoubleOrZero(Final2[0][5]);
                        }
                        if (RES2.Length > 0 && RES2[0][0] != "")
                        {
                            신재생[mth] = 신재생[mth] + Program.UTIL.ToDoubleOrZero(RES2[0][0]);
                        }
                        총가스[mth] = Program.UTIL.ToDoubleOrZero(Final2[0][7]) - 기저[mth];
                    }

                    난방[mth] = double.IsNaN(난방[mth]) ? 0 : 난방[mth];
                    냉방[mth] = double.IsNaN(냉방[mth]) ? 0 : 냉방[mth];
                    급탕[mth] = double.IsNaN(급탕[mth]) ? 0 : 급탕[mth];
                    조명[mth] = double.IsNaN(조명[mth]) ? 0 : 조명[mth];
                    공조[mth] = double.IsNaN(공조[mth]) ? 0 : 공조[mth];
                    신재생[mth] = double.IsNaN(신재생[mth]) ? 0 : 신재생[mth];
                    기저[mth] = double.IsNaN(기저[mth]) ? 0 : 기저[mth];
                    총가스[mth] = double.IsNaN(총가스[mth]) ? 0 : 총가스[mth];
                    총전기[mth] = double.IsNaN(총전기[mth]) ? 0 : 총전기[mth];

                    총소요량[mth] = 총전기[mth] + 총가스[mth];

                }


                double 난방소 = 0, 냉방소 = 0, 급탕소 = 0, 조명소 = 0, 공조소 = 0, 소요량합계 = 0; //단위면적당값
                for (int mth = 0; mth < 12; mth++)
                {
                    연간전기 += 총전기[mth];
                    연간가스 += 총가스[mth];
                    연간소요량 += 총소요량[mth];
                    __data[36].Add(new { idx = i * 12 + mth, val = 난방[mth].ToString("#,##0") });
                    __data[37].Add(new { idx = i * 12 + mth, val = 냉방[mth].ToString("#,##0") });
                    __data[38].Add(new { idx = i * 12 + mth, val = 급탕[mth].ToString("#,##0") });
                    __data[39].Add(new { idx = i * 12 + mth, val = 조명[mth].ToString("#,##0") });
                    __data[40].Add(new { idx = i * 12 + mth, val = 공조[mth].ToString("#,##0") });
                    __data[41].Add(new { idx = i * 12 + mth, val = 신재생[mth].ToString("#,##0") });
                    __data[42].Add(new { idx = i * 12 + mth, val = 총소요량[mth].ToString("#,##0") });
                    난방소 += 난방[mth];
                    냉방소 += 냉방[mth];
                    급탕소 += 급탕[mth];
                    조명소 += 조명[mth];
                    공조소 += 공조[mth];
                }
                double tCO2 = 연간전기 * 0.4747 / 1000000 * 1000 + 연간가스 / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                double TOE = 연간전기 * 0.00023 + 연간가스 / 38.9 / 0.277778 * 0.00103;
                double 연간1차 = 연간전기 * 2.75 + 연간가스  *1.1;

                소요량합계 += 난방소 + 냉방소 + 급탕소 + 조명소 + 공조소;
                난방소 = 난방소 / 순바닥면적;
                냉방소 = 냉방소 / 순바닥면적;
                급탕소 = 급탕소 / 순바닥면적;
                조명소 = 조명소 / 순바닥면적;
                공조소 = 공조소 / 순바닥면적;
                소요량합계 = 소요량합계 / 순바닥면적;

                //1차에너지소요량 계산식: 지역난방,전기,가스,기름 [임시작성]
                double 난방1차=0, 냉방1차=0, 급탕1차=0, 조명1차=0, 공조1차 = 0, 전기1차=0, 열1차=0, 총소요1차=0;
                double[] 난방1 = new double[12], 냉방1 = new double[12], 급탕1 = new double[12], 조명1 = new double[12], 공조1 = new double[12], 신재생1 = new double[12], 전기1 = new double[12], 열1 = new double[12], 총소요1 = new double[12];
                for (int mth = 0; mth < 12; mth++)
                {
                    string[][] RES1 = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "SUM(총에너지)", "생산유형='전기'and 생산소비='생산' and 월 ='" + (mth + 1).ToString() + "월'");
                    string[][] RES2 = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "SUM(총에너지)", "not 생산유형='전기' and 생산소비='생산' and 월 ='" + (mth + 1).ToString() + "월'");
                    string[][] Fi1 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,신재생에너지,총에너지소요량", "연료='전기' and 월 ='" + (mth + 1).ToString() + "월'");
                    string[][] Fi2 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,신재생에너지,총에너지소요량", "(연료='가스' OR 연료='기름')  and 월 ='" + (mth + 1).ToString() + "월'");
                    string[][] Fi3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,신재생에너지,총에너지소요량", "연료='지역난방' and 월 ='" + (mth + 1).ToString() + "월'");
                    if (Fi1.Length > 0)
                    {
                        난방1[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi1[0][0]) * 2.75) ? 0 : Program.UTIL.ToDoubleOrZero(Fi1[0][0]) * 2.75;
                        냉방1[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi1[0][1]) * 2.75) ? 0 : Program.UTIL.ToDoubleOrZero(Fi1[0][1]) * 2.75;
                        급탕1[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi1[0][2]) * 2.75) ? 0 : Program.UTIL.ToDoubleOrZero(Fi1[0][2]) * 2.75;
                        조명1[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi1[0][3]) * 2.75) ? 0 : Program.UTIL.ToDoubleOrZero(Fi1[0][3]) * 2.75;
                        공조1[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi1[0][4]) * 2.75) ? 0 : Program.UTIL.ToDoubleOrZero(Fi1[0][4]) * 2.75;
                        if (RES1.Length > 0 && RES1[0][0] != "")
                        {
                            전기1[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(RES1[0][0]) * 2.75) ? 0 : Program.UTIL.ToDoubleOrZero(RES1[0][0]) * 2.75;
                        }
                        총소요1[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi1[0][6]) * 2.75) ? 0 : Program.UTIL.ToDoubleOrZero(Fi1[0][6]) * 2.75;
                    }
                    if (Fi2.Length > 0)
                    {
                        난방1[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi2[0][0]) *1.1) ? 0 : Program.UTIL.ToDoubleOrZero(Fi2[0][0]) *1.1;
                        냉방1[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi2[0][1]) *1.1) ? 0 : Program.UTIL.ToDoubleOrZero(Fi2[0][1]) *1.1;
                        급탕1[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi2[0][2]) *1.1) ? 0 : Program.UTIL.ToDoubleOrZero(Fi2[0][2]) *1.1;
                        조명1[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi2[0][3]) *1.1) ? 0 : Program.UTIL.ToDoubleOrZero(Fi2[0][3]) *1.1;
                        공조1[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi2[0][4]) *1.1) ? 0 : Program.UTIL.ToDoubleOrZero(Fi2[0][4]) * 1.1;
                        if (RES2.Length > 0 && RES2[0][0] != "")
                        {
                            열1[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(RES2[0][0]) * 1.1) ? 0 : Program.UTIL.ToDoubleOrZero(RES2[0][0]) * 1.1;
                        }
                        총소요1[mth] = double.IsNaN(총소요1[mth] + Program.UTIL.ToDoubleOrZero(Fi2[0][6]) * 1.1) ?  0 : 총소요1[mth] + Program.UTIL.ToDoubleOrZero(Fi2[0][6]) * 1.1;
                    }
                    if (Fi3.Length > 0)
                    {
                        난방1[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi3[0][0]) * 0.728) ? 0 : Program.UTIL.ToDoubleOrZero(Fi3[0][0]) *0.728;
                        냉방1[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi3[0][1]) *0.728) ? 0 : Program.UTIL.ToDoubleOrZero(Fi3[0][1]) *0.728;
                        급탕1[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi3[0][2]) *0.728) ? 0 : Program.UTIL.ToDoubleOrZero(Fi3[0][2]) *0.728;
                        조명1[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi3[0][3]) *0.728) ? 0 : Program.UTIL.ToDoubleOrZero(Fi3[0][3]) *0.728;
                        공조1[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi3[0][4]) *0.728) ? 0 : Program.UTIL.ToDoubleOrZero(Fi3[0][4]) * 0.728;
                        총소요1[mth] = double.IsNaN(총소요1[mth] + Program.UTIL.ToDoubleOrZero(Fi3[0][6]) * 0.728) ?  0: 총소요1[mth] + Program.UTIL.ToDoubleOrZero(Fi3[0][6]) * 0.728;
                    }
                }

                for(int g= 0; g < 12; g++)
                {
                    난방1차 += 난방1[g];
                    냉방1차 += 냉방1[g];
                    급탕1차 += 급탕1[g];
                    조명1차 += 조명1[g];
                    공조1차 += 공조1[g];
                    전기1차 += 전기1[g];
                    열1차 += 열1[g];
                    총소요1차 += 총소요1[g];
                }

                __data[155].Add(new { idx = i, val = (난방1차/순바닥면적).ToString("0.0") });
                __data[156].Add(new { idx = i, val = (냉방1차/순바닥면적).ToString("0.0") });
                __data[157].Add(new { idx = i, val = (급탕1차/순바닥면적).ToString("0.0") });
                __data[158].Add(new { idx = i, val = (조명1차/순바닥면적).ToString("0.0") });
                __data[159].Add(new { idx = i, val = (공조1차/순바닥면적).ToString("0.0") });
                __data[160].Add(new { idx = i, val = (총소요1차/순바닥면적).ToString("#,##0") });
                __data[161].Add(new { idx = i, val = (전기1차 / 순바닥면적).ToString("#,##0") });
                __data[162].Add(new { idx = i, val = (열1차 / 순바닥면적).ToString("#,##0") });
                __data[163].Add(new { idx = i, val = ((전기1차+열1차)/ 총소요1차 ).ToString("P1") });


                탄소배출량 = tCO2 / 순바닥면적 * 1000;
                __data[147].Add(new { idx = i, val = 탄소배출량.ToString("0.00") });
                __data[148].Add(new { idx = i, val = 순바닥면적.ToString("0.00") });

                __data[149].Add(new { idx = i, val = 난방소.ToString("0.00") });
                __data[150].Add(new { idx = i, val = 냉방소.ToString("0.00") });
                __data[151].Add(new { idx = i, val = 급탕소.ToString("0.00") });
                __data[152].Add(new { idx = i, val = 조명소.ToString("0.00") });
                __data[153].Add(new { idx = i, val = 공조소.ToString("0.00") });
                __data[154].Add(new { idx = i, val = 소요량합계.ToString("#,##0") });

                __data[43].Add(new { idx = i, val = (연간소요량).ToString("#,##0") }); 
                __data[44].Add(new { idx = i, val = (연간소요량 / 순바닥면적).ToString("0.0") });
                __data[45].Add(new { idx = i, val = tCO2.ToString("0.0") });
                __data[46].Add(new { idx = i, val = TOE.ToString("0.0") });
                __data[53].Add(new { idx = i, val = (연간1차).ToString("#,##0") });
                __data[54].Add(new { idx = i, val = (연간1차 / 순바닥면적).ToString("0.0") });
                ////////////////////////////////////////////////////////////////////
                data.Add(new { cname = "qh_mth", data = __data[36] }); //난방에너지소요량
                data.Add(new { cname = "qc_mth", data = __data[37] }); //냉방에너지소요량
                data.Add(new { cname = "qw_mth", data = __data[38] }); //급탕에너지소요량
                data.Add(new { cname = "ql_mth", data = __data[39] }); //조명에너지소요량
                data.Add(new { cname = "qv_mth", data = __data[40] }); //공조에너지소요량
                data.Add(new { cname = "qreg_mth", data = __data[41] }); //신재생에너지생산량
                data.Add(new { cname = "qf_mth", data = __data[42] });  //총에너지소요량
                data.Add(new { cname = "qfa", data = __data[43] });
                data.Add(new { cname = "qfa_area", data = __data[44] });
                data.Add(new { cname = "tco2", data = __data[45] });
                data.Add(new { cname = "toe", data = __data[46] });
                data.Add(new { cname = "qpa", data = __data[53] });
                data.Add(new { cname = "qpa_area", data = __data[54] });

                data.Add(new { cname = "tco2Area", data = __data[147] }); //단위면적당 CO2 배출량
                data.Add(new { cname = "energyArea", data = __data[148] }); //단위면적당 CO2 배출량
                
                data.Add(new { cname = "heatingEnd", data = __data[149] }); //단위면적당 난방에너지소요량
                data.Add(new { cname = "coolingEnd", data = __data[150] }); //단위면적당 냉방에너지소요량
                data.Add(new { cname = "hotwaterEnd", data = __data[151] }); //단위면적당 급탕에너지소요량
                data.Add(new { cname = "lightEnd", data = __data[152] }); //단위면적당 조명에너지소요량
                data.Add(new { cname = "ventEnd", data = __data[153] }); //단위면적당 공조에너지소요량
                data.Add(new { cname = "sumEnd", data = __data[154] }); //단위면적당 총에너지소요량

                data.Add(new { cname = "heatingPri", data = __data[155] }); //1차난방에너지소요량
                data.Add(new { cname = "coolingPri", data = __data[156] }); //1차냉방에너지소요량
                data.Add(new { cname = "hotwaterPri", data = __data[157] }); //1차급탕에너지소요량
                data.Add(new { cname = "lightPri", data = __data[158] }); //1차조명에너지소요량
                data.Add(new { cname = "ventPri", data = __data[159] }); //1차공조에너지소요량
                data.Add(new { cname = "sumPri", data = __data[160] }); //1차총에너지소요량
                data.Add(new { cname = "elecProd", data = __data[161] }); //1차전기에너지생산량
                data.Add(new { cname = "heatProd", data = __data[162] }); //1차열에너지생산량
                data.Add(new { cname = "rer", data = __data[163] }); //자립률

                //에너지등급
                 
                string 등급;
                if (총소요1차/순바닥면적 <= 140)
                {
                    if((전기1차 + 열1차) / 총소요1차 < 0.2) 등급 = "none";
                    else if ((전기1차 + 열1차) / 총소요1차 <0.4) 등급 = "ZEB 5등급";
                    else if ((전기1차 + 열1차) / 총소요1차 <0.6) 등급 = "ZEB 4등급";
                    else if ((전기1차 + 열1차) / 총소요1차 < 0.8) 등급 = "ZEB 3등급";
                    else if ((전기1차 + 열1차) / 총소요1차 < 1) 등급 = "ZEB 2등급";
                    else if ((전기1차 + 열1차) / 총소요1차 >= 1) 등급 = "ZEB 1등급";
                    else 등급 = "None";
                }
                else 등급 = "None";
                
                __data[164].Add(new { idx = i, val = 등급 });
                data.Add(new { cname = "zebLevel", data = __data[164] }); //등급

                List<object> 난방소요량chart = new List<object>();
                List<object> 냉방소요량chart = new List<object>();
                List<object> 급탕소요량chart = new List<object>();
                List<object> 조명소요량chart = new List<object>();
                List<object> 공조소요량chart = new List<object>();
                List<object> 기저소요량chart = new List<object>();
                for (int mth = 0; mth < 12; mth++)
                {
                    난방소요량chart.Add(Math.Round(double.Parse(Program.UTIL.asFixed(난방[mth].ToString())), 0) + 0);
                    냉방소요량chart.Add(Math.Round(double.Parse(Program.UTIL.asFixed(냉방[mth].ToString())), 0) + 0);
                    급탕소요량chart.Add(Math.Round(double.Parse(Program.UTIL.asFixed(급탕[mth].ToString())), 0) + 0);
                    조명소요량chart.Add(Math.Round(double.Parse(Program.UTIL.asFixed(조명[mth].ToString())), 0) + 0);
                    공조소요량chart.Add(Math.Round(double.Parse(Program.UTIL.asFixed(공조[mth].ToString())), 0) + 0);
                }
                chart_난방소요량.Add(System.Text.Json.JsonSerializer.Serialize(난방소요량chart.ToArray()));
                chart_냉방소요량.Add(System.Text.Json.JsonSerializer.Serialize(냉방소요량chart.ToArray()));
                chart_급탕소요량.Add(System.Text.Json.JsonSerializer.Serialize(급탕소요량chart.ToArray()));
                chart_조명소요량.Add(System.Text.Json.JsonSerializer.Serialize(조명소요량chart.ToArray()));
                chart_공조소요량.Add(System.Text.Json.JsonSerializer.Serialize(공조소요량chart.ToArray()));
                chart_공조소요량.Add(System.Text.Json.JsonSerializer.Serialize(공조소요량chart.ToArray()));
                #endregion


                items.Add("buildingform_one.html");
                s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
                System.Text.Json.JsonSerializer.Serialize(__data[10].ToArray());
                double max = 0;
                for (int mth = 0; mth < 12; mth++)
                {
                    if (max < 총소요량[mth])
                    {
                        max = 총소요량[mth];
                    }
                }
                Debug.Print("start");
                if (charts != "") charts += ",";
                charts += "{data:[" +
                "{type:\"bar\",barPercentage:0.4,label:\"급탕 에너지소요량 [kWh]\",data:" + chart_급탕소요량[i] + ",borderColor:\"#A9D18E\",backgroundColor:\"#A9D18E\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"공조 에너지소요량 [kWh]\",data:" + chart_공조소요량[i] + ",borderColor:\"#70AD47\",backgroundColor:\"#70AD47\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"조명 에너지소요량 [kWh]\",data:" + chart_조명소요량[i] + ",borderColor:\"#FFD966\",backgroundColor:\"#FFD966\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"난방 에너지소요량 [kWh]\",data:" + chart_난방소요량[i] + ",borderColor:\"#F4B183\",backgroundColor:\"#F4B183\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"냉방 에너지소요량 [kWh]\",data:" + chart_냉방소요량[i] + ",borderColor:\"#9DC3E6\",backgroundColor:\"#9DC3E6\",dash:false}," +
                "],max:" + (Math.Round(max / 1000) * 1000 + 500).ToString("0") + ",step:100,legend:true,stacked:true}";
                runScript("init(" + s + "," + s2 + "," + "[" + charts + "])");
             }
        }

        private void Report_After()
        {
            string s, s2;
            double d;
            string sp;
            string[][] 번호 = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
            string[][] res = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기존프로젝트");
            List<object> items = new List<object>();
            List<object> items2 = new List<object>();
            List<object> data = new List<object>();
            List<string> chart_난방소요량 = new List<string>();
            List<string> chart_냉방소요량 = new List<string>();
            List<string> chart_급탕소요량 = new List<string>();
            List<string> chart_조명소요량 = new List<string>();
            List<string> chart_공조소요량 = new List<string>();
            List<string> chart_총소요량 = new List<string>();
            List<object>[] __data = new List<object>[300];
            List<object>[] __Edata = new List<object>[300];
            List<object>[] __data2 = new List<object>[300];
            List<object>[] __Edata2 = new List<object>[300];

            List<object>[] __saving = new List<object>[300];

            int i = -1, n;
            while (++i < 300)
            {
                __data[i] = new List<object>();
                __Edata[i] = new List<object>();
                __data2[i] = new List<object>();
                __Edata2[i] = new List<object>();
                __saving[i] = new List<object>();
            }


            string charts = "";

            i = -1;
            while (++i < 번호.Length)
            {
                if (res.Length > 0 && res[0][0]!="")
                {
                    #region 건물정보
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트명,주소,지역,지역구분,준공시기,연면적,건축면적,지상층수,지하층수,작성자회사,작성자,작성시기,프로젝트번호,프로젝트유형,건물용도");
                    if (Value.Length > 0)
                    {
                        __data[0].Add(new { idx = i, val = Value[0][0] }); //프로젝트명
                        __data[1].Add(new { idx = i, val = Value[0][1] }); //주소
                        __data[2].Add(new { idx = i, val = Value[0][2] }); //지역
                        __data[3].Add(new { idx = i, val = Value[0][3] }); //지역구분
                        __data[4].Add(new { idx = i, val = Value[0][4] }); //준공시기
                        __data[5].Add(new { idx = i, val = Program.UTIL.ToDoubleOrZero(Value[0][5]).ToString("0.00") }); //연면적
                        __data[6].Add(new { idx = i, val = Program.UTIL.ToDoubleOrZero(Value[0][6]).ToString("0.00") }); //건축면적
                        __data[7].Add(new { idx = i, val = Value[0][7] }); //지상층수
                        __data[8].Add(new { idx = i, val = Value[0][8] }); //지하층수
                        __data[9].Add(new { idx = i, val = Value[0][9] }); //작성자회사
                        __data[10].Add(new { idx = i, val = Value[0][10] }); //작성자
                        __data[11].Add(new { idx = i, val = Value[0][11] }); //작성시기      
                        __data[136].Add(new { idx = i, val = Value[0][12] }); //프로젝트번호
                        __data[137].Add(new { idx = i, val = Value[0][0] + " 검토보고서" }); //프로젝트 명칭 
                        __data[138].Add(new { idx = i, val = Value[0][13] }); //프로젝트유형
                        __data[139].Add(new { idx = i, val = Value[0][14] }); //건물용도   
                    }
                    ////////////////////////////////////////////////////////////////////
                    data.Add(new { cname = "projectName", data = __data[0] });
                    data.Add(new { cname = "buildinglocation", data = __data[1] });
                    data.Add(new { cname = "climate", data = __data[2] });
                    data.Add(new { cname = "bylawclimate", data = __data[3] });
                    data.Add(new { cname = "constructiondate", data = __data[4] });
                    data.Add(new { cname = "grossarea", data = __data[5] });
                    data.Add(new { cname = "buildingarea", data = __data[6] });
                    data.Add(new { cname = "aboveground", data = __data[7] });
                    data.Add(new { cname = "underground", data = __data[8] });
                    data.Add(new { cname = "reviewercompany", data = __data[9] });
                    data.Add(new { cname = "reviewername", data = __data[10] });
                    data.Add(new { cname = "reviewdate", data = __data[11] });
                    data.Add(new { cname = "projectnum", data = __data[136] });
                    data.Add(new { cname = "projectName2", data = __data[137] });
                    data.Add(new { cname = "projectType", data = __data[138] });
                    data.Add(new { cname = "buildingType", data = __data[139] });
                    #endregion
                    #region 요구량정보

                    // 단위면적당CO2  

                    string[][] 존정보 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호, 일일급탕요구량,순바닥면적", "");
                    string[][] 존정보2 = Program.DB.getValue(res[0][0], "ZoneGeneral_Form", "존번호, 일일급탕요구량,순바닥면적", "");
                    //총순바닥면적 구하기
                    double 순바닥면적 = 0;
                    double 순바닥면적2 = 0;

                    for (int l = 0; l < 존정보.Length; l++)
                    {
                        순바닥면적 += Program.UTIL.ToDoubleOrZero(존정보[l][2]);
                    }
                    for (int l = 0; l < 존정보2.Length; l++)
                    {
                        순바닥면적2 += Program.UTIL.ToDoubleOrZero(존정보2[l][2]);
                    }
                    //요구량값 가져오기
                    double[] 난방요구량 = new double[12], 냉방요구량 = new double[12], 급탕요구량 = new double[12], 조명요구량 = new double[12], 공조요구량 = new double[12];
                    double[] 난방요구량2 = new double[12], 냉방요구량2 = new double[12], 급탕요구량2 = new double[12], 조명요구량2 = new double[12], 공조요구량2 = new double[12];
                    #region 리모델링 후 요구량
                    for (int mt = 0; mt < 12; mt++)
                    {
                        string[][] heat = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_mth", "난방_냉방 = '난방' and 비이용일_이용일='이용일' and 월 ='" + (mt + 1).ToString() + "월'");
                        if (heat.Length > 0)
                        {
                            for (int h = 0; h < heat.Length; h++)
                            {
                                난방요구량[mt] += Program.UTIL.ToDoubleOrZero(heat[h][0]);
                            }
                        }
                        string[][] cool = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_mth", "난방_냉방 = '냉방' and 비이용일_이용일='이용일' and 월 ='" + (mt + 1).ToString() + "월'");
                        if (cool.Length > 0)
                        {
                            for (int h = 0; h < cool.Length; h++)
                            {
                                냉방요구량[mt] += Program.UTIL.ToDoubleOrZero(cool[h][0]);
                            }
                        }
                        string[][] hotw = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Zone_HCneed_Result", "번호, dwd_mth", "비이용일_이용일='이용일' and 월 ='" + (mt + 1).ToString() + "월'");
                        if (hotw.Length > 0)
                        {
                            for (int aaa = 0; aaa < hotw.Length; aaa++)
                            {
                                for (int k = 0; k < 존정보.Length; k++)
                                {
                                    if (존정보[k][0] == hotw[aaa][0])
                                    {
                                        급탕요구량[mt] += Program.UTIL.ToDoubleOrZero(hotw[aaa][1]) * Program.UTIL.ToDoubleOrZero(존정보[k][1]);
                                    }
                                }
                            }
                        }

                        string[][] 요구량2 = Program.DB.getValue(DB.type.ProjDB, "Zone_LightResult", "Final_kWh", "월 ='" + (mt + 1).ToString() + "월'");
                        if (요구량2.Length > 0)
                        {
                            for (int h = 0; h < 요구량2.Length; h++)
                            {
                                조명요구량[mt] += Program.UTIL.ToDoubleOrZero(요구량2[h][0]);
                            }
                        }
                        string[][] 요구량3 = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "공조요구량,가습요구량", "월 ='" + (mt + 1).ToString() + "월'");
                        if (요구량3.Length > 0)
                        {
                            for (int h = 0; h < 요구량3.Length; h++)
                            {
                                공조요구량[mt] += Program.UTIL.ToDoubleOrZero(요구량3[h][0]) + Program.UTIL.ToDoubleOrZero(요구량3[h][1]);
                            }
                        }
                    }

                    double 난방요 = 0, 냉방요 = 0, 급탕요 = 0, 조명요 = 0, 공조요 = 0, 탄소배출량 = 0, 요구량합계 = 0;
                    for (int val = 0; val < 12; val++)
                    {
                        난방요 += 난방요구량[val];
                        냉방요 += 냉방요구량[val];
                        급탕요 += 급탕요구량[val];
                        조명요 += 조명요구량[val];
                        공조요 += 공조요구량[val];
                    }
                    요구량합계 = (난방요 + 냉방요 + 급탕요 + 조명요 + 공조요) / 순바닥면적;
                    #endregion

                    #region 리모델링 전 요구량
                    for (int mt = 0; mt < 12; mt++)
                    {
                        string[][] heat = Program.DB.getValue(res[0][0], "Zone_HCneed_Result", "Qb_mth", "난방_냉방 = '난방' and 비이용일_이용일='이용일' and 월 ='" + (mt + 1).ToString() + "월'");
                        if (heat.Length > 0)
                        {
                            for (int h = 0; h < heat.Length; h++)
                            {
                                난방요구량2[mt] += Program.UTIL.ToDoubleOrZero(heat[h][0]);
                            }
                        }
                        string[][] cool = Program.DB.getValue(res[0][0], "Zone_HCneed_Result", "Qb_mth", "난방_냉방 = '냉방' and 비이용일_이용일='이용일' and 월 ='" + (mt + 1).ToString() + "월'");
                        if (cool.Length > 0)
                        {
                            for (int h = 0; h < cool.Length; h++)
                            {
                                냉방요구량2[mt] += Program.UTIL.ToDoubleOrZero(cool[h][0]);
                            }
                        }
                        string[][] hotw = Program.DB.getValue_SameCheck(res[0][0], "Zone_HCneed_Result", "번호, dwd_mth", "비이용일_이용일='이용일' and 월 ='" + (mt + 1).ToString() + "월'");
                        if (hotw.Length > 0)
                        {
                            for (int aaa = 0; aaa < hotw.Length; aaa++)
                            {
                                for (int k = 0; k < 존정보2.Length; k++)
                                {
                                    if (존정보2[k][0] == hotw[aaa][0])
                                    {
                                        급탕요구량2[mt] += Program.UTIL.ToDoubleOrZero(hotw[aaa][1]) * Program.UTIL.ToDoubleOrZero(존정보2[k][1]);
                                    }
                                }
                            }
                        }

                        string[][] 요구량2 = Program.DB.getValue(res[0][0], "Zone_LightResult", "Final_kWh", "월 ='" + (mt + 1).ToString() + "월'");
                        if (요구량2.Length > 0)
                        {
                            for (int h = 0; h < 요구량2.Length; h++)
                            {
                                조명요구량2[mt] += Program.UTIL.ToDoubleOrZero(요구량2[h][0]);
                            }
                        }
                        string[][] 요구량3 = Program.DB.getValue(res[0][0], "AHUSystem_Result", "공조요구량,가습요구량", "월 ='" + (mt + 1).ToString() + "월'");
                        if (요구량3.Length > 0)
                        {
                            for (int h = 0; h < 요구량3.Length; h++)
                            {
                                공조요구량2[mt] += Program.UTIL.ToDoubleOrZero(요구량3[h][0]) + Program.UTIL.ToDoubleOrZero(요구량3[h][1]);
                            }
                        }
                    }

                    double 난방요2 = 0, 냉방요2 = 0, 급탕요2 = 0, 조명요2 = 0, 공조요2 = 0, 탄소배출량2 = 0, 요구량합계2 = 0;
                    for (int val = 0; val < 12; val++)
                    {
                        난방요2 += 난방요구량2[val];
                        냉방요2 += 냉방요구량2[val];
                        급탕요2 += 급탕요구량2[val];
                        조명요2 += 조명요구량2[val];
                        공조요2 += 공조요구량2[val];
                    }
                    요구량합계2 = (난방요2 + 냉방요2 + 급탕요2 + 조명요2 + 공조요2) / 순바닥면적2;
                    #endregion


                    __data[140].Add(new { idx = i, val = (난방요 / 순바닥면적).ToString("0.0") }); //난방에너지요구량
                    __data[141].Add(new { idx = i, val = (냉방요 / 순바닥면적).ToString("0.0") }); //냉방에너지요구량
                    __data[142].Add(new { idx = i, val = (급탕요 / 순바닥면적).ToString("0.0") }); //급탕에너지요구량
                    __data[143].Add(new { idx = i, val = (조명요 / 순바닥면적).ToString("0.0") }); //조명에너지요구량
                    __data[144].Add(new { idx = i, val = (공조요 / 순바닥면적).ToString("0.0") }); //공조에너지요구량
                    __data[145].Add(new { idx = i, val = 순바닥면적.ToString("0.00") }); //순바닥면적
                    __data[146].Add(new { idx = i, val = 요구량합계.ToString("#,##0") }); //요구량합계


                    __data2[140].Add(new { idx = i, val = (난방요2 / 순바닥면적2).ToString("0.0") }); //난방에너지요구량
                    __data2[141].Add(new { idx = i, val = (냉방요2 / 순바닥면적2).ToString("0.0") }); //냉방에너지요구량
                    __data2[142].Add(new { idx = i, val = (급탕요2 / 순바닥면적2).ToString("0.0") }); //급탕에너지요구량
                    __data2[143].Add(new { idx = i, val = (조명요2 / 순바닥면적2).ToString("0.0") }); //조명에너지요구량
                    __data2[144].Add(new { idx = i, val = (공조요2 / 순바닥면적2).ToString("0.0") }); //공조에너지요구량
                    __data2[145].Add(new { idx = i, val = 순바닥면적2.ToString("0.00") }); //순바닥면적
                    __data2[146].Add(new { idx = i, val = 요구량합계2.ToString("#,##0") }); //요구량합계

                    data.Add(new { cname = "heatingneeds", data = __data[140] });
                    data.Add(new { cname = "coolingneeds", data = __data[141] });
                    data.Add(new { cname = "hotwaterneeds", data = __data[142] });
                    data.Add(new { cname = "lightneeds", data = __data[143] });
                    data.Add(new { cname = "ventneeds", data = __data[144] });
                    data.Add(new { cname = "energyarea", data = __data[145] });
                    data.Add(new { cname = "sumneeds", data = __data[146] });

                    data.Add(new { cname = "heatingneeds2", data = __data2[140] });
                    data.Add(new { cname = "coolingneeds2", data = __data2[141] });
                    data.Add(new { cname = "hotwaterneeds2", data = __data2[142] });
                    data.Add(new { cname = "lightneeds2", data = __data2[143] });
                    data.Add(new { cname = "ventneeds2", data = __data2[144] });
                    data.Add(new { cname = "energyarea2", data = __data2[145] });
                    data.Add(new { cname = "sumneeds2", data = __data2[146] });

                    #endregion

                    #region 외벽정보
                    {
                       // 리모델링후
                        Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='외벽'");
                        if (Value.Length > 0)
                        {
                            __Edata[0].Add(new { idx = i, val = Value.Length }); //외벽 유형 개수
                            __Edata[1].Add(new { idx = i, val = Value.Length }); //외벽 유형 개수
                        }
                        Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호");
                        if (Value.Length > 0)
                        {
                            double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                            for (int k = 0; k < Value.Length; k++)
                            {
                                Total_Area += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                                Uvalue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                                RuleValue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                            }
                            Uvalue = Uvalue / Total_Area;
                            RuleValue = RuleValue / Total_Area;
                            __Edata[2].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //외벽 면적
                            __Edata[3].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //외벽 면적
                            __Edata[4].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //외벽 유효 열관류율
                            __Edata[5].Add(new { idx = i, val = RuleValue.ToString("0.00") }); //외벽 법규 열관류율
                            d = RuleValue / Uvalue * 100; if (d >= 100) { d = 100; }
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            __Edata[6].Add(new { idx = i, val = sp }); //법규 열관류율                     
                        }
                        ////////////////////////////////////////////////////////////////////
                        data.Add(new { cname = "wall_count", data = __Edata[0] });
                        data.Add(new { cname = "wall_area", data = __Edata[2] }); ;
                        data.Add(new { cname = "wall_uvalue", data = __Edata[4] });
                        data.Add(new { cname = "wall_rulevalue", data = __Edata[5] });
                        data.Add(new { cname = "wall_rulevalue_point", data = __Edata[6] });
                    }
                    {
                        //리모델링전
                        Value = Program.DB.getValue_SameCheck(res[0][0], "ZoneEnvelope_3D", "구조체번호", "외피유형='외벽'");
                        if (Value.Length > 0)
                        {
                            __Edata2[0].Add(new { idx = i, val = Value.Length }); //외벽 유형 개수
                            __Edata2[1].Add(new { idx = i, val = Value.Length }); //외벽 유형 개수
                        }
                        Value = Program.DB.querySQL(res[0][0], "select a.면적,b.유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호");
                        if (Value.Length > 0)
                        {
                            double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                            for (int k = 0; k < Value.Length; k++)
                            {
                                Total_Area += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                                Uvalue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                                RuleValue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                            }
                            Uvalue = Uvalue / Total_Area;
                            RuleValue = RuleValue / Total_Area;
                            __Edata2[2].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //외벽 면적
                            __Edata2[3].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //외벽 면적
                            __Edata2[4].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //외벽 유효 열관류율
                            __Edata2[5].Add(new { idx = i, val = RuleValue.ToString("0.00") }); //외벽 법규 열관류율                                        
                        }
                        ////////////////////////////////////////////////////////////////////
                        data.Add(new { cname = "wall_count2", data = __Edata2[0] });
                        data.Add(new { cname = "wall_area2", data = __Edata2[2] }); ;
                        data.Add(new { cname = "wall_uvalue2", data = __Edata2[4] });
                        data.Add(new { cname = "wall_rulevalue2", data = __Edata2[5] });
                    }

                    #endregion
                    #region 지붕정보
                    {
                        //리모델링 후 
                        Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='지붕'");
                        if (Value.Length > 0)
                        {
                            __Edata[7].Add(new { idx = i, val = Value.Length }); //지붕 유형 개수
                            __Edata[8].Add(new { idx = i, val = Value.Length }); //지붕 유형 개수
                        }
                        Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionRoof AS b ON a.구조체번호 = b.번호");
                        if (Value.Length > 0)
                        {
                            double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                            for (int k = 0; k < Value.Length; k++)
                            {
                                Total_Area += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                                Uvalue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                                RuleValue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                            }
                            Uvalue = Uvalue / Total_Area;
                            RuleValue = RuleValue / Total_Area;
                            __Edata[9].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //지붕 면적
                            __Edata[10].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //지붕 면적
                            __Edata[11].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //지붕 유효 열관류율
                            __Edata[12].Add(new { idx = i, val = RuleValue.ToString("0.00") }); //지붕 법규 열관류율     
                            d = RuleValue / Uvalue * 100; if (d >= 100) { d = 100; }
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            __Edata[13].Add(new { idx = i, val = sp }); //법규 열관류율                
                        }
                        ////////////////////////////////////////////////////////////////////
                        data.Add(new { cname = "roof_count", data = __Edata[7] });
                        data.Add(new { cname = "roof_area", data = __Edata[9] });
                        data.Add(new { cname = "roof_uvalue", data = __Edata[11] });
                        data.Add(new { cname = "roof_rulevalue", data = __Edata[12] });
                        data.Add(new { cname = "roof_rulevalue_point", data = __Edata[13] });
                    }
                    {
                        //리모델링 전 
                        Value = Program.DB.getValue_SameCheck(res[0][0], "ZoneEnvelope_3D", "구조체번호", "외피유형='지붕'");
                        if (Value.Length > 0)
                        {
                            __Edata2[7].Add(new { idx = i, val = Value.Length }); //지붕 유형 개수
                            __Edata2[8].Add(new { idx = i, val = Value.Length }); //지붕 유형 개수
                        }
                        Value = Program.DB.querySQL(res[0][0], "select a.면적,b.유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionRoof AS b ON a.구조체번호 = b.번호");
                        if (Value.Length > 0)
                        {
                            double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                            for (int k = 0; k < Value.Length; k++)
                            {
                                Total_Area += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                                Uvalue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                                RuleValue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                            }
                            Uvalue = Uvalue / Total_Area;
                            RuleValue = RuleValue / Total_Area;
                            __Edata2[9].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //지붕 면적
                            __Edata2[10].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //지붕 면적
                            __Edata2[11].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //지붕 유효 열관류율
                            __Edata2[12].Add(new { idx = i, val = RuleValue.ToString("0.00") }); //지붕 법규 열관류율  
                        }
                        ////////////////////////////////////////////////////////////////////
                        data.Add(new { cname = "roof_count2", data = __Edata2[7] });
                        data.Add(new { cname = "roof_area2", data = __Edata2[9] });
                        data.Add(new { cname = "roof_uvalue2", data = __Edata2[11] });
                        data.Add(new { cname = "roof_rulevalue2", data = __Edata2[12] });
                    }
                    #endregion
                    #region 최하층바닥정보
                    {
                        //리모델링후
                        Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='최하층바닥'");
                        if (Value.Length > 0)
                        {
                            __Edata[14].Add(new { idx = i, val = Value.Length }); //바닥 유형 
                            __Edata[15].Add(new { idx = i, val = Value.Length }); //바닥 유형 개수
                        }
                        Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionFloor AS b ON a.구조체번호 = b.번호");
                        if (Value.Length > 0)
                        {
                            double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                            for (int k = 0; k < Value.Length; k++)
                            {
                                Total_Area += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                                Uvalue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                                RuleValue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                            }
                            Uvalue = Uvalue / Total_Area;
                            RuleValue = RuleValue / Total_Area;
                            __Edata[16].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //바닥 면적
                            __Edata[17].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //바닥 면적
                            __Edata[18].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //바닥 유효 열관류율
                            __Edata[19].Add(new { idx = i, val = RuleValue.ToString("0.00") }); //바닥 법규 열관류율  
                            d = RuleValue / Uvalue * 100; if (d >= 100) { d = 100; }
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            __Edata[20].Add(new { idx = i, val = sp }); //법규 열관류율                   
                        }
                        ////////////////////////////////////////////////////////////////////
                        data.Add(new { cname = "floor_count", data = __Edata[14] });
                        data.Add(new { cname = "floor_area", data = __Edata[16] });
                        data.Add(new { cname = "floor_uvalue", data = __Edata[18] });
                        data.Add(new { cname = "floor_rulevalue", data = __Edata[19] });
                        data.Add(new { cname = "floor_rulevalue_point", data = __Edata[20] });
                    }
                    {
                        //리모델링전
                        Value = Program.DB.getValue_SameCheck(res[0][0], "ZoneEnvelope_3D", "구조체번호", "외피유형='최하층바닥'");
                        if (Value.Length > 0)
                        {
                            __Edata2[14].Add(new { idx = i, val = Value.Length }); //바닥 유형 
                            __Edata2[15].Add(new { idx = i, val = Value.Length }); //바닥 유형 개수
                        }
                        Value = Program.DB.querySQL(res[0][0], "select a.면적,b.유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionFloor AS b ON a.구조체번호 = b.번호");
                        if (Value.Length > 0)
                        {
                            double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                            for (int k = 0; k < Value.Length; k++)
                            {
                                Total_Area += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                                Uvalue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                                RuleValue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                            }
                            Uvalue = Uvalue / Total_Area;
                            RuleValue = RuleValue / Total_Area;
                            __Edata2[16].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //바닥 면적
                            __Edata2[17].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //바닥 면적
                            __Edata2[18].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //바닥 유효 열관류율
                            __Edata2[19].Add(new { idx = i, val = RuleValue.ToString("0.00") }); //바닥 법규 열관류율  
                        }
                        ////////////////////////////////////////////////////////////////////
                        data.Add(new { cname = "floor_count2", data = __Edata2[14] });
                        data.Add(new { cname = "floor_area2", data = __Edata2[16] });
                        data.Add(new { cname = "floor_uvalue2", data = __Edata2[18] });
                        data.Add(new { cname = "floor_rulevalue2", data = __Edata2[19] });
                    }
                    #endregion
                    #region 창호정보
                    {
                        //리모델링 후
                        Value = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='창호'");
                        if (Value.Length > 0)
                        {
                            __Edata[21].Add(new { idx = i, val = Value.Length }); //창호 유형 개수
                            __Edata[22].Add(new { idx = i, val = Value.Length }); //창호 유형 개수
                        }
                        Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.창호유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체번호 = b.번호");
                        if (Value.Length > 0)
                        {
                            double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                            for (int k = 0; k < Value.Length; k++)
                            {
                                Total_Area += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                                Uvalue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                                RuleValue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                            }
                            Uvalue = Uvalue / Total_Area;
                            RuleValue = RuleValue / Total_Area;
                            __Edata[23].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //창호 면적
                            __Edata[24].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //창호 면적
                            __Edata[25].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //창호 유효 열관류율
                            __Edata[26].Add(new { idx = i, val = RuleValue.ToString("0.00") }); //창호 법규 열관류율  
                            d = RuleValue / Uvalue * 100; if (d >= 100) { d = 100; }
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            __Edata[27].Add(new { idx = i, val = sp }); //법규 열관류율               
                        }
                        ////////////////////////////////////////////////////////////////////
                        data.Add(new { cname = "win_count", data = __Edata[21] });
                        data.Add(new { cname = "win_area", data = __Edata[23] });
                        data.Add(new { cname = "win_uvalue", data = __Edata[25] });
                        data.Add(new { cname = "win_rulevalue", data = __Edata[26] });
                        data.Add(new { cname = "win_rulevalue_point", data = __Edata[27] });
                    }
                    {
                        //리모델링 전
                        Value = Program.DB.getValue(res[0][0], "ZoneEnvelope_3D", "구조체번호", "외피유형='창호'");
                        if (Value.Length > 0)
                        {
                            __Edata2[21].Add(new { idx = i, val = Value.Length }); //창호 유형 개수
                            __Edata2[22].Add(new { idx = i, val = Value.Length }); //창호 유형 개수
                        }
                        Value = Program.DB.querySQL(res[0][0], "select a.면적,b.창호유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체번호 = b.번호");
                        if (Value.Length > 0)
                        {
                            double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                            for (int k = 0; k < Value.Length; k++)
                            {
                                Total_Area += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                                Uvalue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                                RuleValue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                            }
                            Uvalue = Uvalue / Total_Area;
                            RuleValue = RuleValue / Total_Area;
                            __Edata2[23].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //창호 면적
                            __Edata2[24].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //창호 면적
                            __Edata2[25].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //창호 유효 열관류율
                            __Edata2[26].Add(new { idx = i, val = RuleValue.ToString("0.00") }); //창호 법규 열관류율  
                        }
                        ////////////////////////////////////////////////////////////////////
                        data.Add(new { cname = "win_count2", data = __Edata2[21] });
                        data.Add(new { cname = "win_area2", data = __Edata2[23] });
                        data.Add(new { cname = "win_uvalue2", data = __Edata2[25] });
                        data.Add(new { cname = "win_rulevalue2", data = __Edata2[26] });
                    }

                    #endregion
                    #region 커튼월창정보
                    {
                        //리모델링 후 
                        Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='커튼월창'");

                        __Edata[28].Add(new { idx = i, val = Value.Length }); //창호 유형 개수
                        __Edata[29].Add(new { idx = i, val = Value.Length }); //창호 유형 개수

                        double Total_Area_CW = 0, Uvalue_CW = 0, RuleValue_CW = 0;
                        Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.유리부분유효열관류율,b.법규유리부분열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionCW AS b ON a.구조체번호 = b.번호 where a.커튼월부위 ='유리부분'");
                        for (int k = 0; k < Value.Length; k++)
                        {
                            if (Value.Length > 0)
                            {
                                Total_Area_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                                Uvalue_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                                RuleValue_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                            }
                        }
                        Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.패널부분유효열관류율,b.법규패널부분열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionCW AS b ON a.구조체번호 = b.번호 where a.커튼월부위 ='패널부분'");
                        for (int k = 0; k < Value.Length; k++)
                        {
                            if (Value.Length > 0)
                            {
                                Total_Area_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                                Uvalue_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                                RuleValue_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                            }
                        }
                        Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.출입문부분유효열관류율,b.법규출입문부분열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionCW AS b ON a.구조체번호 = b.번호 where a.커튼월부위 ='출입문부분'");
                        for (int k = 0; k < Value.Length; k++)
                        {
                            if (Value.Length > 0)
                            {
                                Total_Area_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                                Uvalue_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                                RuleValue_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                            }
                        }
                        Uvalue_CW = double.IsNaN(Uvalue_CW / Total_Area_CW) ? 0 : Uvalue_CW / Total_Area_CW;
                        RuleValue_CW = double.IsNaN(RuleValue_CW / Total_Area_CW) ? 0 : RuleValue_CW / Total_Area_CW;
                        __Edata[30].Add(new { idx = i, val = Total_Area_CW.ToString("0.0") }); //커튼월창 면적
                        __Edata[31].Add(new { idx = i, val = Total_Area_CW.ToString("0.0") }); //커튼월창 면적
                        __Edata[32].Add(new { idx = i, val = Uvalue_CW.ToString("0.00") }); //커튼월창 유효 열관류율
                        __Edata[33].Add(new { idx = i, val = RuleValue_CW.ToString("0.00") }); //커튼월창 법규 열관류율 
                        d = RuleValue_CW / Uvalue_CW * 100; if (d >= 100) { d = 100; }
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        __Edata[34].Add(new { idx = i, val = sp }); //법규 열관류율   

                        data.Add(new { cname = "cw_count", data = __Edata[28] });
                        data.Add(new { cname = "cw_area", data = __Edata[30] });
                        data.Add(new { cname = "cw_uvalue", data = __Edata[32] });
                        data.Add(new { cname = "cw_rulevalue", data = __Edata[33] });
                        data.Add(new { cname = "cw_rulevalue_point", data = __Edata[34] });
                    }
                    {
                        //리모델링 전
                        Value = Program.DB.getValue_SameCheck(res[0][0], "ZoneEnvelope_3D", "구조체번호", "외피유형='커튼월창'");

                        __Edata2[28].Add(new { idx = i, val = Value.Length }); //창호 유형 개수
                        __Edata2[29].Add(new { idx = i, val = Value.Length }); //창호 유형 개수

                        double Total_Area_CW = 0, Uvalue_CW = 0, RuleValue_CW = 0;
                        Value = Program.DB.querySQL(res[0][0], "select a.면적,b.유리부분유효열관류율,b.법규유리부분열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionCW AS b ON a.구조체번호 = b.번호 where a.커튼월부위 ='유리부분'");
                        for (int k = 0; k < Value.Length; k++)
                        {
                            if (Value.Length > 0)
                            {
                                Total_Area_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                                Uvalue_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                                RuleValue_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                            }
                        }
                        Value = Program.DB.querySQL(res[0][0], "select a.면적,b.패널부분유효열관류율,b.법규패널부분열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionCW AS b ON a.구조체번호 = b.번호 where a.커튼월부위 ='패널부분'");
                        for (int k = 0; k < Value.Length; k++)
                        {
                            if (Value.Length > 0)
                            {
                                Total_Area_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                                Uvalue_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                                RuleValue_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                            }
                        }
                        Value = Program.DB.querySQL(res[0][0], "select a.면적,b.출입문부분유효열관류율,b.법규출입문부분열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionCW AS b ON a.구조체번호 = b.번호 where a.커튼월부위 ='출입문부분'");
                        for (int k = 0; k < Value.Length; k++)
                        {
                            if (Value.Length > 0)
                            {
                                Total_Area_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                                Uvalue_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                                RuleValue_CW += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                            }
                        }
                        Uvalue_CW = double.IsNaN(Uvalue_CW / Total_Area_CW) ? 0 : Uvalue_CW / Total_Area_CW;
                        RuleValue_CW = double.IsNaN(RuleValue_CW / Total_Area_CW) ? 0 : RuleValue_CW / Total_Area_CW;
                        __Edata2[30].Add(new { idx = i, val = Total_Area_CW.ToString("0.0") }); //커튼월창 면적
                        __Edata2[31].Add(new { idx = i, val = Total_Area_CW.ToString("0.0") }); //커튼월창 면적
                        __Edata2[32].Add(new { idx = i, val = Uvalue_CW.ToString("0.00") }); //커튼월창 유효 열관류율
                        __Edata2[33].Add(new { idx = i, val = RuleValue_CW.ToString("0.00") }); //커튼월창 법규 열관류율 

                        data.Add(new { cname = "cw_count2", data = __Edata2[28] });
                        data.Add(new { cname = "cw_area2", data = __Edata2[30] });
                        data.Add(new { cname = "cw_uvalue2", data = __Edata2[32] });
                        data.Add(new { cname = "cw_rulevalue2", data = __Edata2[33] });
                    }
                    #endregion
                    #region 출입문정보
                    {
                        //리모델링후
                        Value = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='외부출입문'");
                        __Edata[35].Add(new { idx = i, val = Value.Length }); //출입문 유형 개수
                        __Edata[36].Add(new { idx = i, val = Value.Length }); //출입문 유형 개수

                        Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.문유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionDoor AS b ON a.구조체번호 = b.번호");
                        if (Value.Length > 0)
                        {
                            double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                            for (int k = 0; k < Value.Length; k++)
                            {
                                Total_Area += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                                Uvalue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                                RuleValue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                            }
                            Uvalue = double.IsNaN(Uvalue / Total_Area) ? 0 : Uvalue / Total_Area;
                            RuleValue = double.IsNaN(RuleValue / Total_Area) ? 0 : RuleValue / Total_Area;
                            __Edata[37].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //출입문 면적
                            __Edata[38].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //출입문 면적
                            __Edata[39].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //출입문 유효 열관류율
                            __Edata[40].Add(new { idx = i, val = RuleValue.ToString("0.00") }); //출입문 법규 열관류율   
                            d = RuleValue / Uvalue * 100; if (d >= 100) { d = 100; }
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            __Edata[41].Add(new { idx = i, val = sp }); //법규 열관류율     
                        }
                        ////////////////////////////////////////////////////////////////////
                        data.Add(new { cname = "door_count", data = __Edata[35] });
                        data.Add(new { cname = "door_area", data = __Edata[37] });
                        data.Add(new { cname = "door_uvalue", data = __Edata[39] });
                        data.Add(new { cname = "door_rulevalue", data = __Edata[40] });
                        data.Add(new { cname = "door_rulevalue_point", data = __Edata[41] });
                    }
                    {
                        //리모델링 전
                        Value = Program.DB.getValue(res[0][0], "ZoneEnvelope_3D", "구조체번호", "외피유형='외부출입문'");
                        __Edata2[35].Add(new { idx = i, val = Value.Length }); //출입문 유형 개수
                        __Edata2[36].Add(new { idx = i, val = Value.Length }); //출입문 유형 개수

                        Value = Program.DB.querySQL(res[0][0], "select a.면적,b.문유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionDoor AS b ON a.구조체번호 = b.번호");
                        if (Value.Length > 0)
                        {
                            double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                            for (int k = 0; k < Value.Length; k++)
                            {
                                Total_Area += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                                Uvalue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                                RuleValue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][2]);
                            }
                            Uvalue = double.IsNaN(Uvalue / Total_Area) ? 0 : Uvalue / Total_Area;
                            RuleValue = double.IsNaN(RuleValue / Total_Area) ? 0 : RuleValue / Total_Area;
                            __Edata2[37].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //출입문 면적
                            __Edata2[38].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //출입문 면적
                            __Edata2[39].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //출입문 유효 열관류율
                            __Edata2[40].Add(new { idx = i, val = RuleValue.ToString("0.00") }); //출입문 법규 열관류율  
                        }
                        ////////////////////////////////////////////////////////////////////
                        data.Add(new { cname = "door_count2", data = __Edata2[35] });
                        data.Add(new { cname = "door_area2", data = __Edata2[37] });
                        data.Add(new { cname = "door_uvalue2", data = __Edata2[39] });
                        data.Add(new { cname = "door_rulevalue2", data = __Edata2[40] });
                    }
                    #endregion
                    #region 기밀
                    {
                        //리모델링 후 
                        double n50 = 0;
                        string[][] nValue = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "n50");
                        if (nValue.Length > 0)
                        {
                            n50 = Program.UTIL.ToDoubleOrZero(nValue[0][0]);
                            __Edata[42].Add(new { idx = i, val = n50.ToString("0.00") }); //n50
                            __Edata[43].Add(new { idx = i, val = (0.6).ToString("0.00") }); //패시브하우스 n50
                        }
                        double CMH = 0;
                        double CMH_rule = 0;
                        double Volume = 0;
                        string[][] ZoneV = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적,천장고", "");
                        if (ZoneV.Length > 0)
                        {
                            for (int a = 0; a < ZoneV.Length; a++)
                            {
                                Volume += Program.UTIL.ToDoubleOrZero(ZoneV[a][0]) * Program.UTIL.ToDoubleOrZero(ZoneV[a][1]);
                            }
                            CMH = n50 * Volume;
                            CMH_rule = 0.6 * Volume;
                            __Edata[44].Add(new { idx = i, val = CMH.ToString("0") });
                            __Edata[45].Add(new { idx = i, val = CMH_rule.ToString("0") });
                        }
                        d = 0.6 / n50 * 100; if (d >= 100) { d = 100; }
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        __Edata[46].Add(new { idx = i, val = sp }); // 패시브 0.6회
                        data.Add(new { cname = "n50", data = __Edata[42] });
                        data.Add(new { cname = "n50_rule", data = __Edata[43] });
                        data.Add(new { cname = "cmh", data = __Edata[44] });
                        data.Add(new { cname = "cmh_rule", data = __Edata[45] });
                        data.Add(new { cname = "n50_rulevalue_point", data = __Edata[46] });
                    }
                    {
                        //리모델링 전 
                        double n50 = 0;
                        string[][] nValue = Program.DB.getValue(res[0][0], "BuildingGeneral", "n50");
                        if (nValue.Length > 0)
                        {
                            n50 = Program.UTIL.ToDoubleOrZero(nValue[0][0]);
                            __Edata2[42].Add(new { idx = i, val = n50.ToString("0.00") }); //n50
                            __Edata2[43].Add(new { idx = i, val = (0.6).ToString("0.00") }); //패시브하우스 n50
                        }
                        double CMH = 0;
                        double CMH_rule = 0;
                        double Volume = 0;
                        string[][] ZoneV = Program.DB.getValue(res[0][0], "ZoneGeneral_Form", "순바닥면적,천장고", "");
                        if (ZoneV.Length > 0)
                        {
                            for (int a = 0; a < ZoneV.Length; a++)
                            {
                                Volume += Program.UTIL.ToDoubleOrZero(ZoneV[a][0]) * Program.UTIL.ToDoubleOrZero(ZoneV[a][1]);
                            }
                            CMH = n50 * Volume;
                            CMH_rule = 0.6 * Volume;
                            __Edata2[44].Add(new { idx = i, val = CMH.ToString("0") });
                            __Edata2[45].Add(new { idx = i, val = CMH_rule.ToString("0") });
                        }
                        data.Add(new { cname = "n502", data = __Edata2[42] });
                        data.Add(new { cname = "n50_rule2", data = __Edata2[43] });
                        data.Add(new { cname = "cmh2", data = __Edata2[44] });
                        data.Add(new { cname = "cmh_rule2", data = __Edata2[45] });
                    }
                    #endregion
                    #region 열교
                    {
                        //리모델링 후 
                        double utb = 0, area_wall = 0, area_roof = 0, area_floor = 0, area_sum = 0;
                        string[][] tValue = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "외벽dUtb,지붕dUtb,바닥dUtb");
                        if (tValue.Length > 0)
                        {
                            string[][] ZoneE = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "Sum(면적)", "외피유형='외벽'");
                            if (ZoneE.Length > 0)
                            {
                                utb += Program.UTIL.ToDoubleOrZero(tValue[0][0]) * Program.UTIL.ToDoubleOrZero(ZoneE[0][0]);
                                area_sum += Program.UTIL.ToDoubleOrZero(ZoneE[0][0]);
                            }
                            ZoneE = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "Sum(면적)", "외피유형='지붕'");
                            if (ZoneE.Length > 0)
                            {
                                utb += Program.UTIL.ToDoubleOrZero(tValue[0][1]) * Program.UTIL.ToDoubleOrZero(ZoneE[0][0]);
                                area_sum += Program.UTIL.ToDoubleOrZero(ZoneE[0][0]);
                            }
                            ZoneE = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "Sum(면적)", "외피유형='최하층바닥'");
                            if (ZoneE.Length > 0)
                            {
                                utb += Program.UTIL.ToDoubleOrZero(tValue[0][2]) * Program.UTIL.ToDoubleOrZero(ZoneE[0][0]);
                                area_sum += Program.UTIL.ToDoubleOrZero(ZoneE[0][0]);
                            }
                            utb = utb / area_sum;
                            __Edata[47].Add(new { idx = i, val = utb.ToString("0.00") });
                            __Edata[48].Add(new { idx = i, val = (0.1).ToString("0.00") });
                        }
                        d = 0.1 / utb * 100; if (d >= 100) { d = 100; }
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        __Edata[49].Add(new { idx = i, val = sp }); //0.1 W/m2k
                        data.Add(new { cname = "utb", data = __Edata[47] });
                        data.Add(new { cname = "utb_rule", data = __Edata[48] });
                        data.Add(new { cname = "utb_rulevalue_point", data = __Edata[49] });
                    }
                    {
                        //리모델링 전 
                        double utb = 0, area_wall = 0, area_roof = 0, area_floor = 0, area_sum = 0;
                        string[][] tValue = Program.DB.getValue(res[0][0], "BuildingGeneral", "외벽dUtb,지붕dUtb,바닥dUtb");
                        if (tValue.Length > 0)
                        {
                            string[][] ZoneE = Program.DB.getValue(res[0][0], "ZoneEnvelope_3D", "Sum(면적)", "외피유형='외벽'");
                            if (ZoneE.Length > 0)
                            {
                                utb += Program.UTIL.ToDoubleOrZero(tValue[0][0]) * Program.UTIL.ToDoubleOrZero(ZoneE[0][0]);
                                area_sum += Program.UTIL.ToDoubleOrZero(ZoneE[0][0]);
                            }
                            ZoneE = Program.DB.getValue(res[0][0], "ZoneEnvelope_3D", "Sum(면적)", "외피유형='지붕'");
                            if (ZoneE.Length > 0)
                            {
                                utb += Program.UTIL.ToDoubleOrZero(tValue[0][1]) * Program.UTIL.ToDoubleOrZero(ZoneE[0][0]);
                                area_sum += Program.UTIL.ToDoubleOrZero(ZoneE[0][0]);
                            }
                            ZoneE = Program.DB.getValue(res[0][0], "ZoneEnvelope_3D", "Sum(면적)", "외피유형='최하층바닥'");
                            if (ZoneE.Length > 0)
                            {
                                utb += Program.UTIL.ToDoubleOrZero(tValue[0][2]) * Program.UTIL.ToDoubleOrZero(ZoneE[0][0]);
                                area_sum += Program.UTIL.ToDoubleOrZero(ZoneE[0][0]);
                            }
                            utb = utb / area_sum;
                            __Edata2[47].Add(new { idx = i, val = utb.ToString("0.00") });
                            __Edata2[48].Add(new { idx = i, val = (0.1).ToString("0.00") });
                        }
                        data.Add(new { cname = "utb2", data = __Edata2[47] });
                        data.Add(new { cname = "utb_rule2", data = __Edata2[48] });
                    }

                    #endregion
                    #region 난방설비
                    {
                        //리모델링 후
                        string[][] Hvalue = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "번호,명칭,주요설비,보일러종류,외기히트펌프번호,흡수식온수기번호,지역난방번호,태양열번호,지열히트펌프번호,지하수히트펌프번호", "");
                        string[][] count_ = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "번호,명칭,주요설비,보일러대수,외기히트펌프대수,흡수식온수기대수,지역난방번호,모듈개수,지열히트펌프대수,지하수히트펌프대수", "");
                        if (Hvalue.Length > 0 && count_.Length > 0)
                        {
                            double power = 0, power_tot = 0, eta = 0, eta_rule = 0; string unit = "W/W";
                            string[][] SystemValue;
                            for (int a = 0; a < Hvalue.Length; a++)
                            {
                                //"보일러", "외기 히트펌프", "지열 히트펌프", "지하수 히트펌프", "태양열 융합 히트펌프", "흡수식온수기", "지역난방", "태양열시스템" 
                                if (Hvalue[a][2] == "보일러")
                                {
                                    SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "용량,전부하효율", "번호 ='" + Hvalue[a][3] + "'");
                                    if (SystemValue.Length > 0)
                                    {
                                        power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][3]);
                                        power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][3]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][3]) : power;
                                        eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][3]) ? Program.UTIL.ToDoubleOrZero(SystemValue[0][1]) : eta;
                                        eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][3]) ? 90 : eta_rule;
                                        unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][3]) ? "%" : unit;
                                    }
                                }
                                else if (Hvalue[a][2] == "외기 히트펌프")
                                {
                                    SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", "난방정격용량,난방정격COP", "번호 ='" + Hvalue[a][4] + "'");
                                    if (SystemValue.Length > 0)
                                    {
                                        power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][4]);
                                        power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][4]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][4]) : power;
                                        eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][4]) ? Program.UTIL.ToDoubleOrZero(SystemValue[0][1]) : eta;
                                        eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][4]) ? 3.8 : eta_rule;
                                        unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][4]) ? "W/W" : unit;
                                    }
                                }
                                else if (Hvalue[a][2] == "흡수식온수기")
                                {
                                    SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_ABS", "난방용량,난방성능", "번호 ='" + Hvalue[a][5] + "'");
                                    if (SystemValue.Length > 0)
                                    {
                                        power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][5]);
                                        power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][5]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][5]) : power;
                                        eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][5]) ? Program.UTIL.ToDoubleOrZero(SystemValue[0][1]) : eta;
                                        eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][5]) ? 1.2 : eta_rule;
                                        unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][5]) ? "W/W" : unit;
                                    }
                                }
                                else if (Hvalue[a][2] == "지역난방")
                                {
                                    SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_DH", "용량", "번호 ='" + Hvalue[a][6] + "'");
                                    if (SystemValue.Length > 0)
                                    {
                                        power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]);
                                        power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) : power;
                                        eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) ? 100 : eta;
                                        eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) ? 100 : eta_rule;
                                        unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) ? "%" : unit;
                                    }
                                }
                                else if (Hvalue[a][2] == "지열 히트펌프")
                                {
                                    SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_GroundHP", "난방정격용량,난방정격COP", "번호 ='" + Hvalue[a][8] + "'");
                                    if (SystemValue.Length > 0)
                                    {
                                        power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][8]);
                                        power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][8]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][8]) : power;
                                        eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][8]) ? Program.UTIL.ToDoubleOrZero(SystemValue[0][1]) : eta;
                                        eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][8]) ? 3.8 : eta_rule;
                                        unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][8]) ? "W/W" : unit;
                                    }
                                }
                                else if (Hvalue[a][2] == "지하수 히트펌프")
                                {
                                    SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_GroundWHP", "난방정격용량,난방정격COP", "번호 ='" + Hvalue[a][9] + "'");
                                    if (SystemValue.Length > 0)
                                    {
                                        power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][9]);
                                        power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][9]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][9]) : power;
                                        eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][9]) ? Program.UTIL.ToDoubleOrZero(SystemValue[0][1]) : eta;
                                        eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][9]) ? 3.8 : eta_rule;
                                        unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][9]) ? "W/W" : unit;
                                    }
                                }
                            }
                            __Edata[50].Add(new { idx = i, val = Hvalue.Length }); //개수
                            __Edata[51].Add(new { idx = i, val = Hvalue.Length }); //개수
                            __Edata[52].Add(new { idx = i, val = power_tot.ToString("0.0") }); //용량
                            __Edata[53].Add(new { idx = i, val = power_tot.ToString("0.0") }); //용량
                            __Edata[54].Add(new { idx = i, val = eta.ToString("0.0") }); //효율
                            __Edata[55].Add(new { idx = i, val = eta_rule.ToString("0.0") }); //권장효율
                            __Edata[56].Add(new { idx = i, val = unit }); //효율 단위
                            __Edata[57].Add(new { idx = i, val = unit }); //권장효율 단위
                            d = eta / eta_rule * 100; if (d >= 100) { d = 100; }
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            __Edata[58].Add(new { idx = i, val = sp });

                            data.Add(new { cname = "h_count", data = __Edata[50] });
                            data.Add(new { cname = "h_power", data = __Edata[52] });
                            data.Add(new { cname = "h_eta", data = __Edata[54] });
                            data.Add(new { cname = "h_eta_rule", data = __Edata[55] });
                            data.Add(new { cname = "h_unit", data = __Edata[56] });
                            data.Add(new { cname = "h_point", data = __Edata[58] });
                        }
                    }
                    {
                        //리모델링 전
                        string[][] Hvalue = Program.DB.getValue(res[0][0], "HeatingSystem_Form", "번호,명칭,주요설비,보일러종류,외기히트펌프번호,흡수식온수기번호,지역난방번호,태양열번호,지열히트펌프번호,지하수히트펌프번호", "");
                        string[][] count_ = Program.DB.getValue(res[0][0], "HeatingSystem_Form", "번호,명칭,주요설비,보일러대수,외기히트펌프대수,흡수식온수기대수,지역난방번호,모듈개수,지열히트펌프대수,지하수히트펌프대수", "");
                        if (Hvalue.Length > 0 && count_.Length > 0)
                        {
                            double power = 0, power_tot = 0, eta = 0, eta_rule = 0; string unit = "W/W";
                            string[][] SystemValue;
                            for (int a = 0; a < Hvalue.Length; a++)
                            {
                                //"보일러", "외기 히트펌프", "지열 히트펌프", "지하수 히트펌프", "태양열 융합 히트펌프", "흡수식온수기", "지역난방", "태양열시스템" 
                                if (Hvalue[a][2] == "보일러")
                                {
                                    SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "용량,전부하효율", "번호 ='" + Hvalue[a][3] + "'");
                                    if (SystemValue.Length > 0)
                                    {
                                        power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][3]);
                                        power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][3]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][3]) : power;
                                        eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][3]) ? Program.UTIL.ToDoubleOrZero(SystemValue[0][1]) : eta;
                                        eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][3]) ? 90 : eta_rule;
                                        unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][3]) ? "%" : unit;
                                    }
                                }
                                else if (Hvalue[a][2] == "외기 히트펌프")
                                {
                                    SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", "난방정격용량,난방정격COP", "번호 ='" + Hvalue[a][4] + "'");
                                    if (SystemValue.Length > 0)
                                    {
                                        power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][4]);
                                        power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][4]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][4]) : power;
                                        eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][4]) ? Program.UTIL.ToDoubleOrZero(SystemValue[0][1]) : eta;
                                        eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][4]) ? 3.8 : eta_rule;
                                        unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][4]) ? "W/W" : unit;
                                    }
                                }
                                else if (Hvalue[a][2] == "흡수식온수기")
                                {
                                    SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_ABS", "난방용량,난방성능", "번호 ='" + Hvalue[a][5] + "'");
                                    if (SystemValue.Length > 0)
                                    {
                                        power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][5]);
                                        power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][5]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][5]) : power;
                                        eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][5]) ? Program.UTIL.ToDoubleOrZero(SystemValue[0][1]) : eta;
                                        eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][5]) ? 1.2 : eta_rule;
                                        unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][5]) ? "W/W" : unit;
                                    }
                                }
                                else if (Hvalue[a][2] == "지역난방")
                                {
                                    SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_DH", "용량", "번호 ='" + Hvalue[a][6] + "'");
                                    if (SystemValue.Length > 0)
                                    {
                                        power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]);
                                        power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) : power;
                                        eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) ? 100 : eta;
                                        eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) ? 100 : eta_rule;
                                        unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) ? "%" : unit;
                                    }
                                }
                                else if (Hvalue[a][2] == "지열 히트펌프")
                                {
                                    SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_GroundHP", "난방정격용량,난방정격COP", "번호 ='" + Hvalue[a][8] + "'");
                                    if (SystemValue.Length > 0)
                                    {
                                        power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][8]);
                                        power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][8]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][8]) : power;
                                        eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][8]) ? Program.UTIL.ToDoubleOrZero(SystemValue[0][1]) : eta;
                                        eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][8]) ? 3.8 : eta_rule;
                                        unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][8]) ? "W/W" : unit;
                                    }
                                }
                                else if (Hvalue[a][2] == "지하수 히트펌프")
                                {
                                    SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_GroundWHP", "난방정격용량,난방정격COP", "번호 ='" + Hvalue[a][9] + "'");
                                    if (SystemValue.Length > 0)
                                    {
                                        power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][9]);
                                        power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][9]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][9]) : power;
                                        eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][9]) ? Program.UTIL.ToDoubleOrZero(SystemValue[0][1]) : eta;
                                        eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][9]) ? 3.8 : eta_rule;
                                        unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count_[a][9]) ? "W/W" : unit;
                                    }
                                }
                            }
                            __Edata2[50].Add(new { idx = i, val = Hvalue.Length }); //개수
                            __Edata2[51].Add(new { idx = i, val = Hvalue.Length }); //개수
                            __Edata2[52].Add(new { idx = i, val = power_tot.ToString("0.0") }); //용량
                            __Edata2[53].Add(new { idx = i, val = power_tot.ToString("0.0") }); //용량
                            __Edata2[54].Add(new { idx = i, val = eta.ToString("0.0") }); //효율
                            __Edata2[55].Add(new { idx = i, val = eta_rule.ToString("0.0") }); //권장효율
                            __Edata2[56].Add(new { idx = i, val = unit }); //효율 단위
                            __Edata2[57].Add(new { idx = i, val = unit }); //권장효율 단위


                            data.Add(new { cname = "h_count2", data = __Edata2[50] });
                            data.Add(new { cname = "h_power2", data = __Edata2[52] });
                            data.Add(new { cname = "h_eta2", data = __Edata2[54] });
                            data.Add(new { cname = "h_eta_rule2", data = __Edata2[55] });
                            data.Add(new { cname = "h_unit2", data = __Edata2[56] });

                        }
                    }
                    #endregion
                    #region 급탕설비
                    {
                        //리모델링 후
                        string[][] Dvalue = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "번호,명칭,주요설비,보일러종류,히트펌프번호,지역난방번호", "");
                        string[][] count__ = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "번호,명칭,주요설비,보일러대수,히트펌프대수,지역난방번호", "");
                        if (Dvalue.Length > 0 && count__.Length > 0)
                        {
                            double power = 0, power_tot = 0, eta = 0, eta_rule = 0; string unit = "W/W";
                            string[][] SystemValue;
                            for (int a = 0; a < Dvalue.Length; a++)
                            {
                                //"보일러", "외기 히트펌프", "지열 히트펌프", "지하수 히트펌프", "태양열 융합 히트펌프", "흡수식온수기", "지역난방", "태양열시스템" 
                                if (Dvalue[a][2] == "보일러")
                                {
                                    SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "용량,전부하효율", "번호 ='" + Dvalue[a][3] + "'");
                                    if (SystemValue.Length > 0)
                                    {
                                        power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][3]);
                                        power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][3]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][3]) : power;
                                        eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][3]) ? Program.UTIL.ToDoubleOrZero(SystemValue[0][1]) : eta;
                                        eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][3]) ? 90 : eta_rule;
                                        unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][3]) ? "%" : unit;
                                    }
                                }
                                else if (Dvalue[a][2] == "외기 히트펌프")
                                {
                                    SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_DHWHP", "급탕정격용량,급탕정격COP", "번호 ='" + Dvalue[a][4] + "'");
                                    if (SystemValue.Length > 0)
                                    {
                                        power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][4]);
                                        power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][4]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][4]) : power;
                                        eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][4]) ? Program.UTIL.ToDoubleOrZero(SystemValue[0][1]) : eta;
                                        eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][4]) ? 3.8 : eta_rule;
                                        unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][4]) ? "W/W" : unit;
                                    }
                                }
                                else if (Dvalue[a][2] == "지역난방")
                                {
                                    SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_DH", "용량", "번호 ='" + Dvalue[a][5] + "'");
                                    if (SystemValue.Length > 0)
                                    {
                                        power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]);
                                        power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) : power;
                                        eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) ? 100 : eta;
                                        eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) ? 100 : eta_rule;
                                        unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) ? "%" : unit;
                                    }
                                }
                            }
                            __Edata[60].Add(new { idx = i, val = Dvalue.Length }); //개수
                            __Edata[61].Add(new { idx = i, val = Dvalue.Length }); //개수
                            __Edata[62].Add(new { idx = i, val = power_tot.ToString("0.0") }); //용량
                            __Edata[63].Add(new { idx = i, val = power_tot.ToString("0.0") }); //용량
                            __Edata[64].Add(new { idx = i, val = eta.ToString("0.0") }); //효율
                            __Edata[65].Add(new { idx = i, val = eta_rule.ToString("0.0") }); //권장효율
                            __Edata[66].Add(new { idx = i, val = unit }); //효율 단위
                            __Edata[67].Add(new { idx = i, val = unit }); //권장효율 단위
                            d = eta / eta_rule * 100; if (d >= 100) { d = 100; }
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            __Edata[68].Add(new { idx = i, val = sp });

                            data.Add(new { cname = "w_count", data = __Edata[60] });
                            data.Add(new { cname = "w_power", data = __Edata[62] });
                            data.Add(new { cname = "w_eta", data = __Edata[64] });
                            data.Add(new { cname = "w_eta_rule", data = __Edata[65] });
                            data.Add(new { cname = "w_unit", data = __Edata[66] });
                            data.Add(new { cname = "w_point", data = __Edata[68] });
                        }
                    }
                    {
                        //리모델링 전
                        string[][] Dvalue = Program.DB.getValue(res[0][0], "DHWSystem_Form", "번호,명칭,주요설비,보일러종류,히트펌프번호,지역난방번호", "");
                        string[][] count__ = Program.DB.getValue(res[0][0], "DHWSystem_Form", "번호,명칭,주요설비,보일러대수,히트펌프대수,지역난방번호", "");
                        if (Dvalue.Length > 0 && count__.Length > 0)
                        {
                            double power = 0, power_tot = 0, eta = 0, eta_rule = 0; string unit = "W/W";
                            string[][] SystemValue;
                            for (int a = 0; a < Dvalue.Length; a++)
                            {
                                //"보일러", "외기 히트펌프", "지열 히트펌프", "지하수 히트펌프", "태양열 융합 히트펌프", "흡수식온수기", "지역난방", "태양열시스템" 
                                if (Dvalue[a][2] == "보일러")
                                {
                                    SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "용량,전부하효율", "번호 ='" + Dvalue[a][3] + "'");
                                    if (SystemValue.Length > 0)
                                    {
                                        power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][3]);
                                        power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][3]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][3]) : power;
                                        eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][3]) ? Program.UTIL.ToDoubleOrZero(SystemValue[0][1]) : eta;
                                        eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][3]) ? 90 : eta_rule;
                                        unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][3]) ? "%" : unit;
                                    }
                                }
                                else if (Dvalue[a][2] == "외기 히트펌프")
                                {
                                    SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_DHWHP", "급탕정격용량,급탕정격COP", "번호 ='" + Dvalue[a][4] + "'");
                                    if (SystemValue.Length > 0)
                                    {
                                        power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][4]);
                                        power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][4]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][4]) : power;
                                        eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][4]) ? Program.UTIL.ToDoubleOrZero(SystemValue[0][1]) : eta;
                                        eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][4]) ? 3.8 : eta_rule;
                                        unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) * Program.UTIL.ToDoubleOrZero(count__[a][4]) ? "W/W" : unit;
                                    }
                                }
                                else if (Dvalue[a][2] == "지역난방")
                                {
                                    SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_DH", "용량", "번호 ='" + Dvalue[a][5] + "'");
                                    if (SystemValue.Length > 0)
                                    {
                                        power_tot += Program.UTIL.ToDoubleOrZero(SystemValue[0][0]);
                                        power = Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) > power ? Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) : power;
                                        eta = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) ? 100 : eta;
                                        eta_rule = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) ? 100 : eta_rule;
                                        unit = power == Program.UTIL.ToDoubleOrZero(SystemValue[0][0]) ? "%" : unit;
                                    }
                                }
                            }
                            __Edata2[60].Add(new { idx = i, val = Dvalue.Length }); //개수
                            __Edata2[61].Add(new { idx = i, val = Dvalue.Length }); //개수
                            __Edata2[62].Add(new { idx = i, val = power_tot.ToString("0.0") }); //용량
                            __Edata2[63].Add(new { idx = i, val = power_tot.ToString("0.0") }); //용량
                            __Edata2[64].Add(new { idx = i, val = eta.ToString("0.0") }); //효율
                            __Edata2[65].Add(new { idx = i, val = eta_rule.ToString("0.0") }); //권장효율
                            __Edata2[66].Add(new { idx = i, val = unit }); //효율 단위
                            __Edata2[67].Add(new { idx = i, val = unit }); //권장효율 단위

                            data.Add(new { cname = "w_count2", data = __Edata2[60] });
                            data.Add(new { cname = "w_power2", data = __Edata2[62] });
                            data.Add(new { cname = "w_eta2", data = __Edata2[64] });
                            data.Add(new { cname = "w_eta_rule2", data = __Edata2[65] });
                            data.Add(new { cname = "w_unit2", data = __Edata2[66] });
                        }
                    }
                    #endregion
                    #region 냉방설비
                    {
                        // 리모델링 후 
                        string[][] Cvalue = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "냉방출력,냉방성능", "");
                        if (Cvalue.Length > 0)
                        {
                            double power_tot = 0, power_max = 0, eta = 0, eta_rule = 3.4;
                            for (int a = 0; a < Cvalue.Length; a++)
                            {
                                power_tot += Program.UTIL.ToDoubleOrZero(Cvalue[a][0]);
                                power_max = power_max < Program.UTIL.ToDoubleOrZero(Cvalue[a][0]) ? Program.UTIL.ToDoubleOrZero(Cvalue[0][0]) : power_max;
                                eta = power_max == Program.UTIL.ToDoubleOrZero(Cvalue[a][0]) ? Program.UTIL.ToDoubleOrZero(Cvalue[0][1]) : eta;
                            }
                            __Edata[70].Add(new { idx = i, val = Cvalue.Length }); //개수
                            __Edata[71].Add(new { idx = i, val = Cvalue.Length }); //개수
                            __Edata[72].Add(new { idx = i, val = power_tot.ToString("0.0") }); //용량
                            __Edata[73].Add(new { idx = i, val = power_tot.ToString("0.0") }); //용량
                            __Edata[74].Add(new { idx = i, val = eta.ToString("0.0") }); //효율
                            __Edata[75].Add(new { idx = i, val = eta_rule.ToString("0.0") }); //권장효율
                            d = eta / eta_rule * 100; if (d >= 100) { d = 100; }
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            __Edata[76].Add(new { idx = i, val = sp });
                            data.Add(new { cname = "c_count", data = __Edata[70] });
                            data.Add(new { cname = "c_power", data = __Edata[72] });
                            data.Add(new { cname = "c_eta", data = __Edata[74] });
                            data.Add(new { cname = "c_eta_rule", data = __Edata[75] });
                            data.Add(new { cname = "c_point", data = __Edata[76] });
                        }
                    }
                    {
                        // 리모델링 전 
                        string[][] Cvalue = Program.DB.getValue(res[0][0], "CoolingSystem_Form", "냉방출력,냉방성능", "");
                        if (Cvalue.Length > 0)
                        {
                            double power_tot = 0, power_max = 0, eta = 0, eta_rule = 3.4;
                            for (int a = 0; a < Cvalue.Length; a++)
                            {
                                power_tot += Program.UTIL.ToDoubleOrZero(Cvalue[a][0]);
                                power_max = power_max < Program.UTIL.ToDoubleOrZero(Cvalue[a][0]) ? Program.UTIL.ToDoubleOrZero(Cvalue[0][0]) : power_max;
                                eta = power_max == Program.UTIL.ToDoubleOrZero(Cvalue[a][0]) ? Program.UTIL.ToDoubleOrZero(Cvalue[0][1]) : eta;
                            }
                            __Edata2[70].Add(new { idx = i, val = Cvalue.Length }); //개수
                            __Edata2[71].Add(new { idx = i, val = Cvalue.Length }); //개수
                            __Edata2[72].Add(new { idx = i, val = power_tot.ToString("0.0") }); //용량
                            __Edata2[73].Add(new { idx = i, val = power_tot.ToString("0.0") }); //용량
                            __Edata2[74].Add(new { idx = i, val = eta.ToString("0.0") }); //효율
                            __Edata2[75].Add(new { idx = i, val = eta_rule.ToString("0.0") }); //권장효율
                            data.Add(new { cname = "c_count2", data = __Edata2[70] });
                            data.Add(new { cname = "c_power2", data = __Edata2[72] });
                            data.Add(new { cname = "c_eta2", data = __Edata2[74] });
                            data.Add(new { cname = "c_eta_rule2", data = __Edata2[75] });
                        }
                    }
                    #endregion
                    #region 조명정보   
                    { 
                        //리모델링 후 
                    string light_count; double light_density = 0; double light_eta_avg = 0; double light_eta_rule = 0; double light_point = 0; double light_area = 0;
                    Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneLighting_form", "조명번호");
                        if (Value.Length > 0)
                        {
                            light_count = "-";
                            for (int a = 0; a < Value.Length; a++)
                            {
                                string[][] 조명존 = Program.DB.querySQL(DB.type.ProjDB, "Select a.번호,a.순바닥면적,a.조명밀도,a.등기구명칭,a.광효율,a.자연채광유형,b.기존존 From ZoneLighting_form as a Inner Join ZoneGeneral_Form as b on a.번호 =b.존번호 where a.조명번호='" + Value[a][0] + "'");
                                if (조명존.Length > 0)
                                {
                                    for (int aa = 0; aa < 조명존.Length; aa++)
                                    {
                                        light_area = Program.UTIL.ToDoubleOrZero(조명존[aa][1]);
                                        light_density = Program.UTIL.ToDoubleOrZero(조명존[aa][1]) * Program.UTIL.ToDoubleOrZero(조명존[aa][2]);
                                        light_eta_avg = Program.UTIL.ToDoubleOrZero(조명존[aa][1]) * Program.UTIL.ToDoubleOrZero(조명존[aa][4]);
                                    }
                                }
                            }
                            //light_density = light_density / light_area;
                            light_eta_avg = light_eta_avg / light_area;
                            light_eta_rule = 70;
                            light_point = Math.Min(100, light_eta_avg / light_eta_rule * 100);
                            __Edata[80].Add(new { idx = i, val = light_count }); //개수 
                            __Edata[81].Add(new { idx = i, val = light_count }); //개수 
                            __Edata[82].Add(new { idx = i, val = light_density.ToString("0.0") }); //용량
                            __Edata[83].Add(new { idx = i, val = light_density.ToString("0.0") }); //용량
                            __Edata[84].Add(new { idx = i, val = light_eta_avg.ToString("0") }); //성능
                            __Edata[85].Add(new { idx = i, val = light_eta_rule.ToString("0") }); //권장 성능                
                            d = light_point;
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            __Edata[86].Add(new { idx = i, val = sp }); //성능점수       
                            data.Add(new { cname = "l_count", data = __Edata[80] });
                            data.Add(new { cname = "l_power", data = __Edata[82] });;
                            data.Add(new { cname = "l_eta", data = __Edata[84] });
                            data.Add(new { cname = "l_eta_rule", data = __Edata[85] });
                            data.Add(new { cname = "l_point", data = __Edata[86] });
                        }
                    }
                    {
                        //리모델링 전 
                        string light_count; double light_density = 0; double light_eta_avg = 0; double light_eta_rule = 0; double light_point = 0; double light_area = 0;
                        Value = Program.DB.getValue_SameCheck(res[0][0], "ZoneLighting_form", "조명번호");
                        if (Value.Length > 0)
                        {
                            light_count = "-";
                            for (int a = 0; a < Value.Length; a++)
                            {
                                string[][] 조명존 = Program.DB.querySQL(res[0][0], "Select a.번호,a.순바닥면적,a.조명밀도,a.등기구명칭,a.광효율,a.자연채광유형,b.기존존 From ZoneLighting_form as a Inner Join ZoneGeneral_Form as b on a.번호 =b.존번호 where a.조명번호='" + Value[a][0] + "'");
                                if (조명존.Length > 0)
                                {
                                    for (int aa = 0; aa < 조명존.Length; aa++)
                                    {
                                        light_area = Program.UTIL.ToDoubleOrZero(조명존[aa][1]);
                                        light_density = Program.UTIL.ToDoubleOrZero(조명존[aa][1]) * Program.UTIL.ToDoubleOrZero(조명존[aa][2]);
                                        light_eta_avg = Program.UTIL.ToDoubleOrZero(조명존[aa][1]) * Program.UTIL.ToDoubleOrZero(조명존[aa][4]);
                                    }
                                }
                            }
                            //light_density = light_density / light_area;
                            light_eta_avg = light_eta_avg / light_area;
                            light_eta_rule = 70;
                            light_point = Math.Min(100, light_eta_avg / light_eta_rule * 100);
                            __Edata2[80].Add(new { idx = i, val = light_count }); //개수 
                            __Edata2[81].Add(new { idx = i, val = light_count }); //개수 
                            __Edata2[82].Add(new { idx = i, val = light_density.ToString("0.0") }); //용량
                            __Edata2[83].Add(new { idx = i, val = light_density.ToString("0.0") }); //용량
                            __Edata2[84].Add(new { idx = i, val = light_eta_avg.ToString("0") }); //성능
                            __Edata2[85].Add(new { idx = i, val = light_eta_rule.ToString("0") }); //권장 성능  
                            data.Add(new { cname = "l_count2", data = __Edata2[80] });
                            data.Add(new { cname = "l_power2", data = __Edata2[82] }); ;
                            data.Add(new { cname = "l_eta2", data = __Edata2[84] });
                            data.Add(new { cname = "l_eta_rule2", data = __Edata2[85] });
                        }
                    }

                    #endregion
                    #region 태양광정보 
                    {
                        //리모델링 후
                        double pv_count = 0; double pv_power = 0; double pv_eta_avg = 0; string pv_eta_rule = "-"; double pv_point = 0;
                        Value = Program.DB.querySQL(DB.type.ProjDB, "Select a.번호,a.명칭,a.모듈번호,a.개수,a.개수,a.용량,a.면적,b.CELLTYPE,b.Kpk From PV_Form as a inner Join User_PV as b on a.모듈번호=b.번호");
                        if (Value.Length > 0)
                        {

                            for (int a = 0; a < Value.Length; a++)
                            {
                                pv_count += Program.UTIL.ToDoubleOrZero(Value[a][3]);
                                pv_power += Program.UTIL.ToDoubleOrZero(Value[a][5]);
                                pv_eta_avg += Program.UTIL.ToDoubleOrZero(Value[a][8]) * Program.UTIL.ToDoubleOrZero(Value[a][5]) * 100;
                            }
                            pv_eta_avg = pv_eta_avg / pv_power;
                            pv_point = 100;
                            __Edata[90].Add(new { idx = i, val = pv_count.ToString("0") }); //개수 
                            __Edata[91].Add(new { idx = i, val = pv_count.ToString("0") }); //개수 
                            __Edata[92].Add(new { idx = i, val = pv_power.ToString("0.0") }); //용량
                            __Edata[93].Add(new { idx = i, val = pv_power.ToString("0.0") }); //용량
                            __Edata[94].Add(new { idx = i, val = pv_eta_avg.ToString("0.0") }); //성능
                            __Edata[95].Add(new { idx = i, val = pv_eta_rule.ToString() }); //권장 성능                
                            d = pv_point;
                            if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                            else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                            else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 205) / 100) + "px'></div>"; }
                            sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                            __Edata[96].Add(new { idx = i, val = sp }); //성능점수    
                        }
                        data.Add(new { cname = "pv_count", data = __Edata[90] });
                        data.Add(new { cname = "pv_power", data = __Edata[92] });
                        data.Add(new { cname = "pv_eta_avg", data = __Edata[94] });
                        data.Add(new { cname = "pv_eta_rule", data = __Edata[95] });
                        data.Add(new { cname = "pv_point", data = __Edata[96] });
                    }

                    {
                        //리모델링 전
                        double pv_count = 0; double pv_power = 0; double pv_eta_avg = 0; string pv_eta_rule = "-"; double pv_point = 0;
                        Value = Program.DB.querySQL(res[0][0], "Select a.번호,a.명칭,a.모듈번호,a.개수,a.개수,a.용량,a.면적,b.CELLTYPE,b.Kpk From PV_Form as a inner Join User_PV as b on a.모듈번호=b.번호");
                        if (Value.Length > 0)
                        {

                            for (int a = 0; a < Value.Length; a++)
                            {
                                pv_count += Program.UTIL.ToDoubleOrZero(Value[a][3]);
                                pv_power += Program.UTIL.ToDoubleOrZero(Value[a][5]);
                                pv_eta_avg += Program.UTIL.ToDoubleOrZero(Value[a][8]) * Program.UTIL.ToDoubleOrZero(Value[a][5]) * 100;
                            }
                            pv_eta_avg = pv_eta_avg / pv_power;
                            pv_point = 100;
                            __Edata2[90].Add(new { idx = i, val = pv_count.ToString("0") }); //개수 
                            __Edata2[91].Add(new { idx = i, val = pv_count.ToString("0") }); //개수 
                            __Edata2[92].Add(new { idx = i, val = pv_power.ToString("0.0") }); //용량
                            __Edata2[93].Add(new { idx = i, val = pv_power.ToString("0.0") }); //용량
                            __Edata2[94].Add(new { idx = i, val = pv_eta_avg.ToString("0.0") }); //성능
                            __Edata2[95].Add(new { idx = i, val = pv_eta_rule.ToString() }); //권장 성능   
                        }
                        data.Add(new { cname = "pv_count2", data = __Edata2[90] });
                        data.Add(new { cname = "pv_power2", data = __Edata2[92] });
                        data.Add(new { cname = "pv_eta_avg2", data = __Edata2[94] });
                        data.Add(new { cname = "pv_eta_rule2", data = __Edata2[95] });
                    }
                    #endregion

                    #region 리모델링 후 소요량
                    double[] 난방 = new double[12], 냉방 = new double[12], 급탕 = new double[12], 조명 = new double[12], 공조 = new double[12], 기저 = new double[12], 신재생 = new double[12], 총전기 = new double[12], 총가스 = new double[12], 총소요량 = new double[12];
                    double 연간소요량 = 0, 연간전기 = 0, 연간가스 = 0;

                    double 난방소 = 0, 냉방소 = 0, 급탕소 = 0, 조명소 = 0, 공조소 = 0, 소요량합계 = 0; //단위면적당값
                    double tCO2 = 0, TOE = 0, 연간1차 = 0;
                    double 난방1차 = 0, 냉방1차 = 0, 급탕1차 = 0, 조명1차 = 0, 공조1차 = 0, 전기1차 = 0, 열1차 = 0, 총소요1차 = 0;
                    double[] 난방1 = new double[12], 냉방1 = new double[12], 급탕1 = new double[12], 조명1 = new double[12], 공조1 = new double[12], 신재생1 = new double[12], 전기1 = new double[12], 열1 = new double[12], 총소요1 = new double[12];

                    {
                       
                        for (int mth = 0; mth < 12; mth++)
                        {
                            string[][] RES1 = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "SUM(총에너지)", "생산유형='전기'and 월 ='" + (mth + 1).ToString() + "월'");
                            string[][] RES2 = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "SUM(총에너지)", "not 생산유형='전기'and 월 ='" + (mth + 1).ToString() + "월'");
                            string[][] Final1 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량", "연료='전기' and 월 ='" + (mth + 1).ToString() + "월'");
                            string[][] Final2 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량", "not 연료='전기' and not 연료='전체'  and 월 ='" + (mth + 1).ToString() + "월'");
                            if (Final1.Length > 0)
                            {
                                난방[mth] = Program.UTIL.ToDoubleOrZero(Final1[0][0]);
                                냉방[mth] = Program.UTIL.ToDoubleOrZero(Final1[0][1]);
                                급탕[mth] = Program.UTIL.ToDoubleOrZero(Final1[0][2]);
                                조명[mth] = Program.UTIL.ToDoubleOrZero(Final1[0][3]);
                                공조[mth] = Program.UTIL.ToDoubleOrZero(Final1[0][4]);
                                if (Final1[0][5] != null && Final1[0][5] != "")
                                {
                                    기저[mth] = Program.UTIL.ToDoubleOrZero(Final1[0][5]);
                                }
                                if (RES1.Length > 0 && RES1[0][0] != "")
                                { 신재생[mth] = Program.UTIL.ToDoubleOrZero(RES1[0][0]); }
                                총전기[mth] = Program.UTIL.ToDoubleOrZero(Final1[0][7]) - 기저[mth];
                            }
                            if (Final2.Length > 0)
                            {
                                난방[mth] = 난방[mth] + Program.UTIL.ToDoubleOrZero(Final2[0][0]);
                                냉방[mth] = 냉방[mth] + Program.UTIL.ToDoubleOrZero(Final2[0][1]);
                                급탕[mth] = 급탕[mth] + Program.UTIL.ToDoubleOrZero(Final2[0][2]);
                                조명[mth] = 조명[mth] + Program.UTIL.ToDoubleOrZero(Final2[0][3]);
                                공조[mth] = 공조[mth] + Program.UTIL.ToDoubleOrZero(Final2[0][4]);
                                if (Final2[0][5] != null && Final2[0][5] != "")
                                {
                                    기저[mth] = Program.UTIL.ToDoubleOrZero(Final2[0][5]);
                                }
                                if (RES2.Length > 0 && RES2[0][0] != "")
                                {
                                    신재생[mth] = 신재생[mth] + Program.UTIL.ToDoubleOrZero(RES2[0][0]);
                                }
                                총가스[mth] = Program.UTIL.ToDoubleOrZero(Final2[0][7]) - 기저[mth];
                            }

                            난방[mth] = double.IsNaN(난방[mth]) ? 0 : 난방[mth];
                            냉방[mth] = double.IsNaN(냉방[mth]) ? 0 : 냉방[mth];
                            급탕[mth] = double.IsNaN(급탕[mth]) ? 0 : 급탕[mth];
                            조명[mth] = double.IsNaN(조명[mth]) ? 0 : 조명[mth];
                            공조[mth] = double.IsNaN(공조[mth]) ? 0 : 공조[mth];
                            신재생[mth] = double.IsNaN(신재생[mth]) ? 0 : 신재생[mth];
                            기저[mth] = double.IsNaN(기저[mth]) ? 0 : 기저[mth];
                            총가스[mth] = double.IsNaN(총가스[mth]) ? 0 : 총가스[mth];
                            총전기[mth] = double.IsNaN(총전기[mth]) ? 0 : 총전기[mth];

                            총소요량[mth] = 총전기[mth] + 총가스[mth];

                        }

                        for (int mth = 0; mth < 12; mth++)
                        {
                            연간전기 += 총전기[mth];
                            연간가스 += 총가스[mth];
                            연간소요량 += 총소요량[mth];
                            __data[36].Add(new { idx = i * 12 + mth, val = 난방[mth].ToString("#,##0") });
                            __data[37].Add(new { idx = i * 12 + mth, val = 냉방[mth].ToString("#,##0") });
                            __data[38].Add(new { idx = i * 12 + mth, val = 급탕[mth].ToString("#,##0") });
                            __data[39].Add(new { idx = i * 12 + mth, val = 조명[mth].ToString("#,##0") });
                            __data[40].Add(new { idx = i * 12 + mth, val = 공조[mth].ToString("#,##0") });
                            __data[41].Add(new { idx = i * 12 + mth, val = 신재생[mth].ToString("#,##0") });
                            __data[42].Add(new { idx = i * 12 + mth, val = 총소요량[mth].ToString("#,##0") });
                            난방소 += 난방[mth];
                            냉방소 += 냉방[mth];
                            급탕소 += 급탕[mth];
                            조명소 += 조명[mth];
                            공조소 += 공조[mth];
                        }
                        tCO2 = 연간전기 * 0.4747 / 1000000 * 1000 + 연간가스 / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                        TOE = 연간전기 * 0.00023 + 연간가스 / 38.9 / 0.277778 * 0.00103;
                        연간1차 = 연간전기 * 2.75 + 연간가스 * 1.1;

                        소요량합계 += 난방소 + 냉방소 + 급탕소 + 조명소 + 공조소;
                        난방소 = 난방소 / 순바닥면적;
                        냉방소 = 냉방소 / 순바닥면적;
                        급탕소 = 급탕소 / 순바닥면적;
                        조명소 = 조명소 / 순바닥면적;
                        공조소 = 공조소 / 순바닥면적;
                        소요량합계 = 소요량합계 / 순바닥면적;

                        //1차에너지소요량 계산식: 지역난방,전기,가스,기름 [임시작성]
                        for (int mth = 0; mth < 12; mth++)
                        {
                            string[][] RES1 = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "SUM(총에너지)", "생산유형='전기'and 월 ='" + (mth + 1).ToString() + "월'");
                            string[][] RES2 = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "SUM(총에너지)", "not 생산유형='전기'and 월 ='" + (mth + 1).ToString() + "월'");
                            string[][] Fi1 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,신재생에너지,총에너지소요량", "연료='전기' and 월 ='" + (mth + 1).ToString() + "월'");
                            string[][] Fi2 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,신재생에너지,총에너지소요량", "(연료='가스' OR 연료='기름')  and 월 ='" + (mth + 1).ToString() + "월'");
                            string[][] Fi3 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,신재생에너지,총에너지소요량", "연료='지역난방' and 월 ='" + (mth + 1).ToString() + "월'");
                            if (Fi1.Length > 0)
                            {
                                난방1[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi1[0][0]) * 2.75) ? 0 : Program.UTIL.ToDoubleOrZero(Fi1[0][0]) * 2.75;
                                냉방1[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi1[0][1]) * 2.75) ? 0 : Program.UTIL.ToDoubleOrZero(Fi1[0][1]) * 2.75;
                                급탕1[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi1[0][2]) * 2.75) ? 0 : Program.UTIL.ToDoubleOrZero(Fi1[0][2]) * 2.75;
                                조명1[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi1[0][3]) * 2.75) ? 0 : Program.UTIL.ToDoubleOrZero(Fi1[0][3]) * 2.75;
                                공조1[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi1[0][4]) * 2.75) ? 0 : Program.UTIL.ToDoubleOrZero(Fi1[0][4]) * 2.75;
                                if (RES1.Length > 0 && RES1[0][0] != "")
                                {
                                    전기1[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(RES1[0][0]) * 2.75) ? 0 : Program.UTIL.ToDoubleOrZero(RES1[0][0]) * 2.75;
                                }
                                총소요1[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi1[0][6]) * 2.75) ? 0 : Program.UTIL.ToDoubleOrZero(Fi1[0][6]) * 2.75;
                            }
                            if (Fi2.Length > 0)
                            {
                                난방1[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi2[0][0]) * 1.1) ? 0 : Program.UTIL.ToDoubleOrZero(Fi2[0][0]) * 1.1;
                                냉방1[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi2[0][1]) * 1.1) ? 0 : Program.UTIL.ToDoubleOrZero(Fi2[0][1]) * 1.1;
                                급탕1[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi2[0][2]) * 1.1) ? 0 : Program.UTIL.ToDoubleOrZero(Fi2[0][2]) * 1.1;
                                조명1[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi2[0][3]) * 1.1) ? 0 : Program.UTIL.ToDoubleOrZero(Fi2[0][3]) * 1.1;
                                공조1[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi2[0][4]) * 1.1) ? 0 : Program.UTIL.ToDoubleOrZero(Fi2[0][4]) * 1.1;
                                if (RES2.Length > 0 && RES2[0][0] != "")
                                {
                                    열1[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(RES2[0][0]) * 1.1) ? 0 : Program.UTIL.ToDoubleOrZero(RES2[0][0]) * 1.1;
                                }
                                총소요1[mth] = double.IsNaN(총소요1[mth] + Program.UTIL.ToDoubleOrZero(Fi2[0][6]) * 1.1) ? 0 : 총소요1[mth] + Program.UTIL.ToDoubleOrZero(Fi2[0][6]) * 1.1;
                            }
                            if (Fi3.Length > 0)
                            {
                                난방1[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi3[0][0]) * 0.728) ? 0 : Program.UTIL.ToDoubleOrZero(Fi3[0][0]) * 0.728;
                                냉방1[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi3[0][1]) * 0.728) ? 0 : Program.UTIL.ToDoubleOrZero(Fi3[0][1]) * 0.728;
                                급탕1[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi3[0][2]) * 0.728) ? 0 : Program.UTIL.ToDoubleOrZero(Fi3[0][2]) * 0.728;
                                조명1[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi3[0][3]) * 0.728) ? 0 : Program.UTIL.ToDoubleOrZero(Fi3[0][3]) * 0.728;
                                공조1[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi3[0][4]) * 0.728) ? 0 : Program.UTIL.ToDoubleOrZero(Fi3[0][4]) * 0.728;
                                총소요1[mth] = double.IsNaN(총소요1[mth] + Program.UTIL.ToDoubleOrZero(Fi3[0][6]) * 0.728) ? 0 : 총소요1[mth] + Program.UTIL.ToDoubleOrZero(Fi3[0][6]) * 0.728;
                            }
                        }

                        for (int g = 0; g < 12; g++)
                        {
                            난방1차 += 난방1[g];
                            냉방1차 += 냉방1[g];
                            급탕1차 += 급탕1[g];
                            조명1차 += 조명1[g];
                            공조1차 += 공조1[g];
                            전기1차 += 전기1[g];
                            열1차 += 열1[g];
                            총소요1차 += 총소요1[g];
                        }

                        __data[155].Add(new { idx = i, val = (난방1차 / 순바닥면적).ToString("0.0") });
                        __data[156].Add(new { idx = i, val = (냉방1차 / 순바닥면적).ToString("0.0") });
                        __data[157].Add(new { idx = i, val = (급탕1차 / 순바닥면적).ToString("0.0") });
                        __data[158].Add(new { idx = i, val = (조명1차 / 순바닥면적).ToString("0.0") });
                        __data[159].Add(new { idx = i, val = (공조1차 / 순바닥면적).ToString("0.0") });
                        __data[160].Add(new { idx = i, val = (총소요1차 / 순바닥면적).ToString("#,##0") });
                        __data[161].Add(new { idx = i, val = (전기1차 / 순바닥면적).ToString("#,##0") });
                        __data[162].Add(new { idx = i, val = (열1차 / 순바닥면적).ToString("#,##0") });
                        __data[163].Add(new { idx = i, val = ((전기1차 + 열1차) / 총소요1차).ToString("P1") });


                        탄소배출량 = tCO2 / 순바닥면적 * 1000;
                        __data[147].Add(new { idx = i, val = 탄소배출량.ToString("0.00") });
                        __data[148].Add(new { idx = i, val = 순바닥면적.ToString("0.00") });

                        __data[149].Add(new { idx = i, val = 난방소.ToString("0.00") });
                        __data[150].Add(new { idx = i, val = 냉방소.ToString("0.00") });
                        __data[151].Add(new { idx = i, val = 급탕소.ToString("0.00") });
                        __data[152].Add(new { idx = i, val = 조명소.ToString("0.00") });
                        __data[153].Add(new { idx = i, val = 공조소.ToString("0.00") });
                        __data[154].Add(new { idx = i, val = 소요량합계.ToString("#,##0") });

                        __data[43].Add(new { idx = i, val = (연간소요량).ToString("#,##0") });
                        __data[44].Add(new { idx = i, val = (연간소요량 / 순바닥면적).ToString("0.0") });
                        __data[45].Add(new { idx = i, val = tCO2.ToString("0.0") });
                        __data[46].Add(new { idx = i, val = TOE.ToString("0.0") });
                        __data[53].Add(new { idx = i, val = (연간1차).ToString("#,##0") });
                        __data[54].Add(new { idx = i, val = (연간1차 / 순바닥면적).ToString("0.0") });
                        ////////////////////////////////////////////////////////////////////
                        data.Add(new { cname = "qh_mth", data = __data[36] }); //난방에너지소요량
                        data.Add(new { cname = "qc_mth", data = __data[37] }); //냉방에너지소요량
                        data.Add(new { cname = "qw_mth", data = __data[38] }); //급탕에너지소요량
                        data.Add(new { cname = "ql_mth", data = __data[39] }); //조명에너지소요량
                        data.Add(new { cname = "qv_mth", data = __data[40] }); //공조에너지소요량
                        data.Add(new { cname = "qreg_mth", data = __data[41] }); //신재생에너지생산량
                        data.Add(new { cname = "qf_mth", data = __data[42] });  //총에너지소요량
                        data.Add(new { cname = "qfa", data = __data[43] });
                        data.Add(new { cname = "qfa_area", data = __data[44] });
                        data.Add(new { cname = "tco2", data = __data[45] });
                        data.Add(new { cname = "toe", data = __data[46] });
                        data.Add(new { cname = "qpa", data = __data[53] });
                        data.Add(new { cname = "qpa_area", data = __data[54] });

                        data.Add(new { cname = "tco2area", data = __data[147] }); //단위면적당 CO2 배출량
                        data.Add(new { cname = "energyArea", data = __data[148] }); //단위면적당 CO2 배출량

                        data.Add(new { cname = "heatingend", data = __data[149] }); //단위면적당 난방에너지소요량
                        data.Add(new { cname = "coolingend", data = __data[150] }); //단위면적당 냉방에너지소요량
                        data.Add(new { cname = "hotwaterend", data = __data[151] }); //단위면적당 급탕에너지소요량
                        data.Add(new { cname = "lightend", data = __data[152] }); //단위면적당 조명에너지소요량
                        data.Add(new { cname = "ventend", data = __data[153] }); //단위면적당 공조에너지소요량
                        data.Add(new { cname = "sumend", data = __data[154] }); //단위면적당 총에너지소요량

                        data.Add(new { cname = "heatingpri", data = __data[155] }); //1차난방에너지소요량
                        data.Add(new { cname = "coolingpri", data = __data[156] }); //1차냉방에너지소요량
                        data.Add(new { cname = "hotwaterpri", data = __data[157] }); //1차급탕에너지소요량
                        data.Add(new { cname = "lightpri", data = __data[158] }); //1차조명에너지소요량
                        data.Add(new { cname = "ventpri", data = __data[159] }); //1차공조에너지소요량
                        data.Add(new { cname = "sumpri", data = __data[160] }); //1차총에너지소요량
                        data.Add(new { cname = "elecprod", data = __data[161] }); //1차전기에너지생산량
                        data.Add(new { cname = "heatprod", data = __data[162] }); //1차열에너지생산량
                        data.Add(new { cname = "rer", data = __data[163] }); //자립률

                        //에너지등급

                        string 등급;
                        if (총소요1차 / 순바닥면적 <= 140)
                        {
                            if ((전기1차 + 열1차) / 총소요1차 < 0.2) 등급 = "none";
                            else if ((전기1차 + 열1차) / 총소요1차 < 0.4) 등급 = "ZEB 5등급";
                            else if ((전기1차 + 열1차) / 총소요1차 < 0.6) 등급 = "ZEB 4등급";
                            else if ((전기1차 + 열1차) / 총소요1차 < 0.8) 등급 = "ZEB 3등급";
                            else if ((전기1차 + 열1차) / 총소요1차 < 1) 등급 = "ZEB 2등급";
                            else if ((전기1차 + 열1차) / 총소요1차 >= 1) 등급 = "ZEB 1등급";
                            else 등급 = "None";
                        }
                        else 등급 = "None";

                        __data[180].Add(new { idx = i, val = 등급 });
                        data.Add(new { cname = "zebLevel", data = __data[180] }); //등급

                        List<object> 난방소요량chart = new List<object>();
                        List<object> 냉방소요량chart = new List<object>();
                        List<object> 급탕소요량chart = new List<object>();
                        List<object> 조명소요량chart = new List<object>();
                        List<object> 공조소요량chart = new List<object>();
                        List<object> 기저소요량chart = new List<object>();
                        for (int mth = 0; mth < 12; mth++)
                        {
                            난방소요량chart.Add(Math.Round(double.Parse(Program.UTIL.asFixed(난방[mth].ToString())), 0) + 0);
                            냉방소요량chart.Add(Math.Round(double.Parse(Program.UTIL.asFixed(냉방[mth].ToString())), 0) + 0);
                            급탕소요량chart.Add(Math.Round(double.Parse(Program.UTIL.asFixed(급탕[mth].ToString())), 0) + 0);
                            조명소요량chart.Add(Math.Round(double.Parse(Program.UTIL.asFixed(조명[mth].ToString())), 0) + 0);
                            공조소요량chart.Add(Math.Round(double.Parse(Program.UTIL.asFixed(공조[mth].ToString())), 0) + 0);
                        }
                        chart_난방소요량.Add(System.Text.Json.JsonSerializer.Serialize(난방소요량chart.ToArray()));
                        chart_냉방소요량.Add(System.Text.Json.JsonSerializer.Serialize(냉방소요량chart.ToArray()));
                        chart_급탕소요량.Add(System.Text.Json.JsonSerializer.Serialize(급탕소요량chart.ToArray()));
                        chart_조명소요량.Add(System.Text.Json.JsonSerializer.Serialize(조명소요량chart.ToArray()));
                        chart_공조소요량.Add(System.Text.Json.JsonSerializer.Serialize(공조소요량chart.ToArray()));
                        chart_공조소요량.Add(System.Text.Json.JsonSerializer.Serialize(공조소요량chart.ToArray()));
                        double max = 0;
                        for (int mth = 0; mth < 12; mth++)
                        {
                            if (max < 총소요량[mth])
                            {
                                max = 총소요량[mth];
                            }
                        }
                        if (charts != "") charts += ",";
                        charts += "{data:[" +
                        "{type:\"bar\",barPercentage:0.4,label:\"급탕 에너지소요량 [kWh]\",data:" + chart_급탕소요량[i] + ",borderColor:\"#A9D18E\",backgroundColor:\"#A9D18E\",dash:false}," +
                        "{type:\"bar\",barPercentage:0.4,label:\"공조 에너지소요량 [kWh]\",data:" + chart_공조소요량[i] + ",borderColor:\"#70AD47\",backgroundColor:\"#70AD47\",dash:false}," +
                        "{type:\"bar\",barPercentage:0.4,label:\"조명 에너지소요량 [kWh]\",data:" + chart_조명소요량[i] + ",borderColor:\"#FFD966\",backgroundColor:\"#FFD966\",dash:false}," +
                        "{type:\"bar\",barPercentage:0.4,label:\"난방 에너지소요량 [kWh]\",data:" + chart_난방소요량[i] + ",borderColor:\"#F4B183\",backgroundColor:\"#F4B183\",dash:false}," +
                        "{type:\"bar\",barPercentage:0.4,label:\"냉방 에너지소요량 [kWh]\",data:" + chart_냉방소요량[i] + ",borderColor:\"#9DC3E6\",backgroundColor:\"#9DC3E6\",dash:false}," +
                        "],max:" + (Math.Round(max / 1000) * 1000 + 500).ToString("0") + ",step:100,legend:true,stacked:true}";
                    }
                    #endregion

                    #region 리모델링 전 소요량
                    double[] 난방2 = new double[12], 냉방2 = new double[12], 급탕2 = new double[12], 조명2 = new double[12], 공조2 = new double[12], 기저2 = new double[12], 신재생2 = new double[12], 총전기2 = new double[12], 총가스2 = new double[12], 총소요량2 = new double[12];
                    double 연간소요량2 = 0, 연간전기2 = 0, 연간가스2 = 0;

                    double 난방소2 = 0, 냉방소2 = 0, 급탕소2 = 0, 조명소2 = 0, 공조소2 = 0, 소요량합계2 = 0; //단위면적당값
                    double tCO22 = 0, TOE2 = 0, 연간1차2 = 0;
                    double 난방1차2 = 0, 냉방1차2 = 0, 급탕1차2 = 0, 조명1차2 = 0, 공조1차2 = 0, 전기1차2 = 0, 열1차2 = 0, 총소요1차2 = 0;
                    double[] 난방12 = new double[12], 냉방12 = new double[12], 급탕12 = new double[12], 조명12 = new double[12], 공조12 = new double[12], 신재생12 = new double[12], 전기12 = new double[12], 열12 = new double[12], 총소요12 = new double[12];
                    {
                    
                        for (int mth = 0; mth < 12; mth++)
                        {
                            string[][] RES1 = Program.DB.getValue(res[0][0], "RESystem_Result", "SUM(총에너지)", "생산유형='전기'and 월 ='" + (mth + 1).ToString() + "월'");
                            string[][] RES2 = Program.DB.getValue(res[0][0], "RESystem_Result", "SUM(총에너지)", "not 생산유형='전기'and 월 ='" + (mth + 1).ToString() + "월'");
                            string[][] Final1 = Program.DB.getValue(res[0][0], "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량", "연료='전기' and 월 ='" + (mth + 1).ToString() + "월'");
                            string[][] Final2 = Program.DB.getValue(res[0][0], "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량", "not 연료='전기' and not 연료='전체'  and 월 ='" + (mth + 1).ToString() + "월'");
                            if (Final1.Length > 0)
                            {
                                난방2[mth] = Program.UTIL.ToDoubleOrZero(Final1[0][0]);
                                냉방2[mth] = Program.UTIL.ToDoubleOrZero(Final1[0][1]);
                                급탕2[mth] = Program.UTIL.ToDoubleOrZero(Final1[0][2]);
                                조명2[mth] = Program.UTIL.ToDoubleOrZero(Final1[0][3]);
                                공조2[mth] = Program.UTIL.ToDoubleOrZero(Final1[0][4]);
                                if (Final1[0][5] != null && Final1[0][5] != "")
                                {
                                    기저2[mth] = Program.UTIL.ToDoubleOrZero(Final1[0][5]);
                                }
                                if (RES1.Length > 0 && RES1[0][0] != "")
                                { 신재생2[mth] = Program.UTIL.ToDoubleOrZero(RES1[0][0]); }
                                총전기2[mth] = Program.UTIL.ToDoubleOrZero(Final1[0][7]) - 기저2[mth];
                            }
                            if (Final2.Length > 0)
                            {
                                난방2[mth] = 난방2[mth] + Program.UTIL.ToDoubleOrZero(Final2[0][0]);
                                냉방2[mth] = 냉방2[mth] + Program.UTIL.ToDoubleOrZero(Final2[0][1]);
                                급탕2[mth] = 급탕2[mth] + Program.UTIL.ToDoubleOrZero(Final2[0][2]);
                                조명2[mth] = 조명2[mth] + Program.UTIL.ToDoubleOrZero(Final2[0][3]);
                                공조2[mth] = 공조2[mth] + Program.UTIL.ToDoubleOrZero(Final2[0][4]);
                                if (Final2[0][5] != null && Final2[0][5] != "")
                                {
                                    기저2[mth] = Program.UTIL.ToDoubleOrZero(Final2[0][5]);
                                }
                                if (RES2.Length > 0 && RES2[0][0] != "")
                                {
                                    신재생2[mth] = 신재생2[mth] + Program.UTIL.ToDoubleOrZero(RES2[0][0]);
                                }
                                총가스2[mth] = Program.UTIL.ToDoubleOrZero(Final2[0][7]) - 기저2[mth];
                            }

                            난방2[mth] = double.IsNaN(난방2[mth]) ? 0 : 난방2[mth];
                            냉방2[mth] = double.IsNaN(냉방2[mth]) ? 0 : 냉방2[mth];
                            급탕2[mth] = double.IsNaN(급탕2[mth]) ? 0 : 급탕2[mth];
                            조명2[mth] = double.IsNaN(조명2[mth]) ? 0 : 조명2[mth];
                            공조2[mth] = double.IsNaN(공조2[mth]) ? 0 : 공조2[mth];
                            신재생2[mth] = double.IsNaN(신재생2[mth]) ? 0 : 신재생2[mth];
                            기저2[mth] = double.IsNaN(기저2[mth]) ? 0 : 기저2[mth];
                            총가스2[mth] = double.IsNaN(총가스2[mth]) ? 0 : 총가스2[mth];
                            총전기2[mth] = double.IsNaN(총전기2[mth]) ? 0 : 총전기2[mth];

                            총소요량2[mth] = 총전기2[mth] + 총가스2[mth];

                        }


                        for (int mth = 0; mth < 12; mth++)
                        {
                            연간전기2 += 총전기2[mth];
                            연간가스2 += 총가스2[mth];
                            연간소요량2 += 총소요량2[mth];
                            난방소2 += 난방2[mth];
                            냉방소2 += 냉방2[mth];
                            급탕소2 += 급탕2[mth];
                            조명소2 += 조명2[mth];
                            공조소2 += 공조2[mth];
                        }
                        tCO22 = 연간전기2 * 0.4747 / 1000000 * 1000 + 연간가스2 / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                        TOE2 = 연간전기2 * 0.00023 + 연간가스2 / 38.9 / 0.277778 * 0.00103;
                        연간1차2 = 연간전기2 * 2.75 + 연간가스2 * 1.1;

                        소요량합계2 += 난방소2 + 냉방소2 + 급탕소2 + 조명소2 + 공조소2;
                        난방소2 = 난방소2 / 순바닥면적2;
                        냉방소2 = 냉방소2 / 순바닥면적2;
                        급탕소2 = 급탕소2 / 순바닥면적2;
                        조명소2 = 조명소2 / 순바닥면적2;
                        공조소2 = 공조소2 / 순바닥면적2;
                        소요량합계2 = 소요량합계2 / 순바닥면적2;

                        //1차에너지소요량 계산식: 지역난방,전기,가스,기름 [임시작성]
                        for (int mth = 0; mth < 12; mth++)
                        {
                            string[][] RES1 = Program.DB.getValue(res[0][0], "RESystem_Result", "SUM(총에너지)", "생산유형='전기'and 월 ='" + (mth + 1).ToString() + "월'");
                            string[][] RES2 = Program.DB.getValue(res[0][0], "RESystem_Result", "SUM(총에너지)", "not 생산유형='전기'and 월 ='" + (mth + 1).ToString() + "월'");
                            string[][] Fi1 = Program.DB.getValue(res[0][0], "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,신재생에너지,총에너지소요량", "연료='전기' and 월 ='" + (mth + 1).ToString() + "월'");
                            string[][] Fi2 = Program.DB.getValue(res[0][0], "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,신재생에너지,총에너지소요량", "(연료='가스' OR 연료='기름')  and 월 ='" + (mth + 1).ToString() + "월'");
                            string[][] Fi3 = Program.DB.getValue(res[0][0], "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,신재생에너지,총에너지소요량", "연료='지역난방' and 월 ='" + (mth + 1).ToString() + "월'");
                            if (Fi1.Length > 0)
                            {
                                난방12[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi1[0][0]) * 2.75) ? 0 : Program.UTIL.ToDoubleOrZero(Fi1[0][0]) * 2.75;
                                냉방12[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi1[0][1]) * 2.75) ? 0 : Program.UTIL.ToDoubleOrZero(Fi1[0][1]) * 2.75;
                                급탕12[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi1[0][2]) * 2.75) ? 0 : Program.UTIL.ToDoubleOrZero(Fi1[0][2]) * 2.75;
                                조명12[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi1[0][3]) * 2.75) ? 0 : Program.UTIL.ToDoubleOrZero(Fi1[0][3]) * 2.75;
                                공조12[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi1[0][4]) * 2.75) ? 0 : Program.UTIL.ToDoubleOrZero(Fi1[0][4]) * 2.75;
                                if (RES1.Length > 0 && RES1[0][0] != "")
                                {
                                    전기12[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(RES1[0][0]) * 2.75) ? 0 : Program.UTIL.ToDoubleOrZero(RES1[0][0]) * 2.75;
                                }
                                총소요12[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi1[0][6]) * 2.75) ? 0 : Program.UTIL.ToDoubleOrZero(Fi1[0][6]) * 2.75;
                            }
                            if (Fi2.Length > 0)
                            {
                                난방12[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi2[0][0]) * 1.1) ? 0 : Program.UTIL.ToDoubleOrZero(Fi2[0][0]) * 1.1;
                                냉방12[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi2[0][1]) * 1.1) ? 0 : Program.UTIL.ToDoubleOrZero(Fi2[0][1]) * 1.1;
                                급탕12[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi2[0][2]) * 1.1) ? 0 : Program.UTIL.ToDoubleOrZero(Fi2[0][2]) * 1.1;
                                조명12[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi2[0][3]) * 1.1) ? 0 : Program.UTIL.ToDoubleOrZero(Fi2[0][3]) * 1.1;
                                공조12[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi2[0][4]) * 1.1) ? 0 : Program.UTIL.ToDoubleOrZero(Fi2[0][4]) * 1.1;
                                if (RES2.Length > 0 && RES2[0][0] != "")
                                {
                                    열12[mth] = double.IsNaN(Program.UTIL.ToDoubleOrZero(RES2[0][0]) * 1.1) ? 0 : Program.UTIL.ToDoubleOrZero(RES2[0][0]) * 1.1;
                                }
                                총소요12[mth] = double.IsNaN(총소요12[mth] + Program.UTIL.ToDoubleOrZero(Fi2[0][6]) * 1.1) ? 0 : 총소요12[mth] + Program.UTIL.ToDoubleOrZero(Fi2[0][6]) * 1.1;
                            }
                            if (Fi3.Length > 0)
                            {
                                난방12[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi3[0][0]) * 0.728) ? 0 : Program.UTIL.ToDoubleOrZero(Fi3[0][0]) * 0.728;
                                냉방12[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi3[0][1]) * 0.728) ? 0 : Program.UTIL.ToDoubleOrZero(Fi3[0][1]) * 0.728;
                                급탕12[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi3[0][2]) * 0.728) ? 0 : Program.UTIL.ToDoubleOrZero(Fi3[0][2]) * 0.728;
                                조명12[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi3[0][3]) * 0.728) ? 0 : Program.UTIL.ToDoubleOrZero(Fi3[0][3]) * 0.728;
                                공조12[mth] += double.IsNaN(Program.UTIL.ToDoubleOrZero(Fi3[0][4]) * 0.728) ? 0 : Program.UTIL.ToDoubleOrZero(Fi3[0][4]) * 0.728;
                                총소요12[mth] = double.IsNaN(총소요12[mth] + Program.UTIL.ToDoubleOrZero(Fi3[0][6]) * 0.728) ? 0 : 총소요12[mth] + Program.UTIL.ToDoubleOrZero(Fi3[0][6]) * 0.728;
                            }
                        }

                        for (int g = 0; g < 12; g++)
                        {
                            난방1차2 += 난방12[g];
                            냉방1차2 += 냉방12[g];
                            급탕1차2 += 급탕12[g];
                            조명1차2 += 조명12[g];
                            공조1차2 += 공조12[g];
                            전기1차2 += 전기12[g];
                            열1차2 += 열12[g];
                            총소요1차2 += 총소요12[g];
                        }

                        __data2[155].Add(new { idx = i, val = (난방1차2 / 순바닥면적2).ToString("0.0") });
                        __data2[156].Add(new { idx = i, val = (냉방1차2 / 순바닥면적2).ToString("0.0") });
                        __data2[157].Add(new { idx = i, val = (급탕1차2/ 순바닥면적2).ToString("0.0") });
                        __data2[158].Add(new { idx = i, val = (조명1차2 / 순바닥면적2).ToString("0.0") });
                        __data2[159].Add(new { idx = i, val = (공조1차2 / 순바닥면적2).ToString("0.0") });
                        __data2[160].Add(new { idx = i, val = (총소요1차2 / 순바닥면적2).ToString("#,##0") });
                        __data2[161].Add(new { idx = i, val = (전기1차2 / 순바닥면적2).ToString("#,##0") });
                        __data2[162].Add(new { idx = i, val = (열1차2 / 순바닥면적2).ToString("#,##0") });
                        __data2[163].Add(new { idx = i, val = ((전기1차2 + 열1차2) / 총소요1차2).ToString("P1") });


                        탄소배출량2 = tCO22 / 순바닥면적2 * 1000;
                        __data2[147].Add(new { idx = i, val = 탄소배출량2.ToString("0.00") });
                        __data2[148].Add(new { idx = i, val = 순바닥면적2.ToString("0.00") });

                        __data2[149].Add(new { idx = i, val = 난방소2.ToString("0.00") });
                        __data2[150].Add(new { idx = i, val = 냉방소2.ToString("0.00") });
                        __data2[151].Add(new { idx = i, val = 급탕소2.ToString("0.00") });
                        __data2[152].Add(new { idx = i, val = 조명소2.ToString("0.00") });
                        __data2[153].Add(new { idx = i, val = 공조소2.ToString("0.00") });
                        __data2[154].Add(new { idx = i, val = 소요량합계2.ToString("#,##0") });

                        __data2[43].Add(new { idx = i, val = (연간소요량2).ToString("#,##0") });
                        __data2[44].Add(new { idx = i, val = (연간소요량2 / 순바닥면적2).ToString("0.0") });
                        __data2[45].Add(new { idx = i, val = tCO22.ToString("0.0") });
                        __data2[46].Add(new { idx = i, val = TOE2.ToString("0.0") });
                        __data2[53].Add(new { idx = i, val = (연간1차2).ToString("#,##0") });
                        __data2[54].Add(new { idx = i, val = (연간1차2 / 순바닥면적2).ToString("0.0") });
                        ////////////////////////////////////////////////////////////////////
                        data.Add(new { cname = "qfa2", data = __data2[43] });
                        data.Add(new { cname = "qfa_area2", data = __data2[44] });
                        data.Add(new { cname = "tco22", data = __data2[45] });
                        data.Add(new { cname = "toe2", data = __data2[46] });
                        data.Add(new { cname = "qpa2", data = __data2[53] });
                        data.Add(new { cname = "qpa_area2", data = __data2[54] });

                        data.Add(new { cname = "tco2area2", data = __data2[147] }); //단위면적당 CO2 배출량
                        data.Add(new { cname = "energyArea2", data = __data2[148] }); //단위면적당 CO2 배출량

                        data.Add(new { cname = "heatingend2", data = __data2[149] }); //단위면적당 난방에너지소요량
                        data.Add(new { cname = "coolingend2", data = __data2[150] }); //단위면적당 냉방에너지소요량
                        data.Add(new { cname = "hotwaterend2", data = __data2[151] }); //단위면적당 급탕에너지소요량
                        data.Add(new { cname = "lightend2", data = __data2[152] }); //단위면적당 조명에너지소요량
                        data.Add(new { cname = "ventend2", data = __data2[153] }); //단위면적당 공조에너지소요량
                        data.Add(new { cname = "sumend2", data = __data2[154] }); //단위면적당 총에너지소요량

                        data.Add(new { cname = "heatingpri2", data = __data2[155] }); //1차난방에너지소요량
                        data.Add(new { cname = "coolingpri2", data = __data2[156] }); //1차냉방에너지소요량
                        data.Add(new { cname = "hotwaterpri2", data = __data2[157] }); //1차급탕에너지소요량
                        data.Add(new { cname = "lightpri2", data = __data2[158] }); //1차조명에너지소요량
                        data.Add(new { cname = "ventpri2", data = __data2[159] }); //1차공조에너지소요량
                        data.Add(new { cname = "sumpri2", data = __data2[160] }); //1차총에너지소요량
                        data.Add(new { cname = "elecprod2", data = __data2[161] }); //1차전기에너지생산량
                        data.Add(new { cname = "heatprod2", data = __data2[162] }); //1차열에너지생산량
                        data.Add(new { cname = "rer2", data = __data2[163] }); //자립률

                    }
                    #endregion

                    #region 절감량 

                    ////////////////////////////////////////////////////////////////////
;
                    __saving[0].Add(new { idx = i, val = Math.Max(0, tCO22 - tCO2).ToString("0.0") });
                    __saving[1].Add(new { idx = i, val = Math.Max(0, TOE2 - TOE).ToString("0.0") });
                    __saving[2].Add(new { idx = i, val = Math.Max(0, 탄소배출량2 - 탄소배출량).ToString("0.00") });


                    __saving[3].Add(new { idx = i, val =Math.Max(0, (난방1차2 / 순바닥면적2) - (난방1차 / 순바닥면적)).ToString("0.0") });
                    __saving[4].Add(new { idx = i, val = Math.Max(0, (냉방1차2 / 순바닥면적2) - (냉방1차 / 순바닥면적)).ToString("0.0") });
                    __saving[5].Add(new { idx = i, val = Math.Max(0, (급탕1차2 / 순바닥면적2) - (급탕1차 / 순바닥면적)).ToString("0.0") });
                    __saving[6].Add(new { idx = i, val = Math.Max(0, (조명1차2 / 순바닥면적2) - (조명1차 / 순바닥면적)).ToString("0.0") });
                    __saving[7].Add(new { idx = i, val = Math.Max(0, (공조1차2 / 순바닥면적2) - (공조1차 / 순바닥면적)).ToString("0.0") });
                    __saving[8].Add(new { idx = i, val = Math.Max(0, (총소요1차2 / 순바닥면적2) - (총소요1차 / 순바닥면적)).ToString("0.0") });

                    __saving[9].Add(new { idx = i, val = Math.Max(0, (전기1차 / 순바닥면적) - (전기1차2 / 순바닥면적2)).ToString("#,##0") });
                    __saving[10].Add(new { idx = i, val = Math.Max(0, (열1차 / 순바닥면적) - (열1차2 / 순바닥면적2)).ToString("#,##0") });
                    __saving[11].Add(new { idx = i, val = Math.Max(0, ((전기1차 + 열1차) / 총소요1차) - ((전기1차2 + 열1차2) / 총소요1차2)).ToString("P1") });


                    data.Add(new { cname = "tco2_saving", data = __saving[0] });
                    data.Add(new { cname = "toe_saving", data = __saving[1] });
                    data.Add(new { cname = "tco2area_saving", data = __saving[2] }); //단위면적당 CO2 배출량
                    data.Add(new { cname = "heatingpri_saving", data = __saving[3] }); //1차난방에너지소요량
                    data.Add(new { cname = "coolingpri_saving", data = __saving[4] }); //1차냉방에너지소요량
                    data.Add(new { cname = "hotwaterpri_saving", data = __saving[5] }); //1차급탕에너지소요량
                    data.Add(new { cname = "lightpri_saving", data = __saving[6] }); //1차조명에너지소요량
                    data.Add(new { cname = "ventpri_saving", data = __saving[7] }); //1차공조에너지소요량
                    data.Add(new { cname = "sumpri_saving", data = __saving[8] }); //1차총에너지소요량
                    data.Add(new { cname = "elecprod_plus", data = __saving[9] }); //1차전기에너지생산량
                    data.Add(new { cname = "heatprod_plus", data = __saving[10] }); //1차열에너지생산량
                    data.Add(new { cname = "rer_plus", data = __saving[11] }); //자립률

                    #endregion 
                    items.Add("buildingform.htm");
                    s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                    s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
                    System.Text.Json.JsonSerializer.Serialize(__data[10].ToArray());
                    
                    Debug.Print("start");
                   
                    runScript("init(" + s + "," + s2 + "," + "[" + charts + "])");
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            webView21.CoreWebView2.ShowPrintUI();
        }
    }
}