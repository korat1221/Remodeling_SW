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

            MainMenu.Add(new { text = "연료별소요량", id = "{\\\"formID\\\":52,\\\"ID\\\":\\\"Result_6\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당

            Program.UTIL.resetMainTree(5, 0, MainMenu.ToArray(), "56"); // 예시 코드: 메인 메뉴 동적 할당
        }

        public void LoadData(string ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();
            string[][] 프로젝트유형 = Program.DB.querySQL(DB.type.ProjListDB, "Select type from projects where current = '1'");
            if (프로젝트유형[0][0] == "1")
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

            int i = -1, n;
            while (++i < 700)
            {
                __data[i] = new List<object>();
            }


            string charts = "";

            i = -1;
            while (++i < 번호.Length)
            {
                #region 건물정보
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트명,주소,지역,지역구분,준공시기,연면적,건축면적,지상층수,지하층수,작성자회사,작성자,작성시기,프로젝트번호");
                if (Value.Length > 0)
                {
                    __data[0].Add(new { idx = i, val = Value[0][0] }); //프로젝트명
                    __data[1].Add(new { idx = i, val = Value[0][1] }); //주소
                    __data[2].Add(new { idx = i, val = Value[0][2] }); //지역
                    __data[3].Add(new { idx = i, val = Value[0][3] }); //지역구분
                    __data[4].Add(new { idx = i, val = Value[0][4] }); //준공시기
                    __data[5].Add(new { idx = i, val = Value[0][5] }); //연면적
                    __data[6].Add(new { idx = i, val = Value[0][6] }); //건축면적
                    __data[7].Add(new { idx = i, val = Value[0][7] }); //지상층수
                    __data[8].Add(new { idx = i, val = Value[0][8] }); //지하층수
                    __data[9].Add(new { idx = i, val = Value[0][9] }); //작성자회사
                    __data[10].Add(new { idx = i, val = Value[0][10] }); //작성자
                    __data[11].Add(new { idx = i, val = Value[0][11] }); //작성시기      
                    __data[136].Add(new { idx = i, val = Value[0][12] }); //프로젝트번호
                    __data[137].Add(new { idx = i, val = "1F_Zone002" }); //프로젝트번호
                }
                ////////////////////////////////////////////////////////////////////
                data.Add(new { cname = "projectName", data = __data[0] });
                data.Add(new { cname = "buildinglocation", data = __data[1] });
                data.Add(new { cname = "climate", data = __data[2] });
                data.Add(new { cname = "bylawclimate", data = __data[3] });
                data.Add(new { cname = "construcitondate", data = __data[4] });
                data.Add(new { cname = "grossarea", data = __data[5] });
                data.Add(new { cname = "buildingarea", data = __data[6] });
                data.Add(new { cname = "aboveground", data = __data[7] });
                data.Add(new { cname = "underground", data = __data[8] });
                data.Add(new { cname = "reviewercompany", data = __data[9] });
                data.Add(new { cname = "reviewername", data = __data[10] });
                data.Add(new { cname = "reviewdate", data = __data[11] });
                data.Add(new { cname = "projectnum", data = __data[136] });
                data.Add(new { cname = "zonenum", data = __data[137] });
                #endregion

                #region 외벽정보
                Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='외벽'");
                if (Value.Length > 0)
                {
                    __data[12].Add(new { idx = i, val = Value.Length }); //외벽 유형 개수
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호");
                if (Value.Length > 0)
                {
                    double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                    for (int k = 0; k < Value.Length; k++)
                    {
                        Total_Area += Convert.ToDouble(Value[k][0]);
                        Uvalue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                        RuleValue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][2]);
                    }
                    Uvalue = Uvalue / Total_Area;
                    RuleValue = RuleValue / Total_Area;
                    __data[13].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //외벽 면적
                    __data[14].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //외벽 유효 열관류율
                    __data[15].Add(new { idx = i, val = RuleValue.ToString("0.00") }); //외벽 법규 열관류율
                    d = RuleValue / Uvalue * 100;if(d>=100){d=100;}
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __data[47].Add(new { idx = i, val = sp }); //법규 열관류율                     
                }
                ////////////////////////////////////////////////////////////////////
                data.Add(new { cname = "wall_count", data = __data[12] });
                data.Add(new { cname = "wall_area", data = __data[13] });
                data.Add(new { cname = "wall_uvalue", data = __data[14] });
                data.Add(new { cname = "wall_rulevalue", data = __data[15] });
                data.Add(new { cname = "wall_rulevalue_point", data = __data[47] });
                #endregion

                #region 지붕정보
                Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='지붕'");
                if (Value.Length > 0)
                {
                    __data[16].Add(new { idx = i, val = Value.Length }); //지붕 유형 개수
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionRoof AS b ON a.구조체번호 = b.번호");
                if (Value.Length > 0)
                {
                    double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                    for (int k = 0; k < Value.Length; k++)
                    {
                        Total_Area += Convert.ToDouble(Value[k][0]);
                        Uvalue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                        RuleValue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][2]);
                    }
                    Uvalue = Uvalue / Total_Area;
                    RuleValue = RuleValue / Total_Area;
                    __data[17].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //지붕 면적
                    __data[18].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //지붕 유효 열관류율
                    __data[19].Add(new { idx = i, val = RuleValue.ToString("0.00") }); //지붕 법규 열관류율     
                    d = RuleValue / Uvalue * 100;if(d>=100){d=100;}
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __data[48].Add(new { idx = i, val = sp }); //법규 열관류율                
                }
                ////////////////////////////////////////////////////////////////////
                data.Add(new { cname = "roof_count", data = __data[16] });
                data.Add(new { cname = "roof_area", data = __data[17] });
                data.Add(new { cname = "roof_uvalue", data = __data[18] });
                data.Add(new { cname = "roof_rulevalue", data = __data[19] });
                data.Add(new { cname = "roof_rulevalue_point", data = __data[48] });
                #endregion

                #region 최하층바닥정보
                Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='최하층바닥'");
                if (Value.Length > 0)
                {
                    __data[20].Add(new { idx = i, val = Value.Length }); //바닥 유형 개수
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionFloor AS b ON a.구조체번호 = b.번호");
                if (Value.Length > 0)
                {
                    double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                    for (int k = 0; k < Value.Length; k++)
                    {
                        Total_Area += Convert.ToDouble(Value[k][0]);
                        Uvalue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                        RuleValue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][2]);
                    }
                    Uvalue = Uvalue / Total_Area;
                    RuleValue = RuleValue / Total_Area;
                    __data[21].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //바닥 면적
                    __data[22].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //바닥 유효 열관류율
                    __data[23].Add(new { idx = i, val = RuleValue.ToString("0.00") }); //바닥 법규 열관류율  
                    d = RuleValue / Uvalue * 100;if(d>=100){d=100;}
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __data[49].Add(new { idx = i, val = sp }); //법규 열관류율                   
                }
                ////////////////////////////////////////////////////////////////////
                data.Add(new { cname = "floor_count", data = __data[20] });
                data.Add(new { cname = "floor_area", data = __data[21] });
                data.Add(new { cname = "floor_uvalue", data = __data[22] });
                data.Add(new { cname = "floor_rulevalue", data = __data[23] });
                data.Add(new { cname = "floor_rulevalue_point", data = __data[49] });
                #endregion

                #region 창호정보
                Value = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='창호'");
                if (Value.Length > 0)
                {
                    __data[24].Add(new { idx = i, val = Value.Length }); //창호 유형 개수
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.창호유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체번호 = b.번호");
                if (Value.Length > 0)
                {
                    double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                    for (int k = 0; k < Value.Length; k++)
                    {
                        Total_Area += Convert.ToDouble(Value[k][0]);
                        Uvalue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                        RuleValue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][2]);
                    }
                    Uvalue = Uvalue / Total_Area;
                    RuleValue = RuleValue / Total_Area;
                    __data[25].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //창호 면적
                    __data[26].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //창호 유효 열관류율
                    __data[27].Add(new { idx = i, val = RuleValue.ToString("0.00") }); //창호 법규 열관류율  
                    d = RuleValue / Uvalue * 100;if(d>=100){d=100;}
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __data[50].Add(new { idx = i, val = sp }); //법규 열관류율               
                }
                ////////////////////////////////////////////////////////////////////
                data.Add(new { cname = "win_count", data = __data[24] });
                data.Add(new { cname = "win_area", data = __data[25] });
                data.Add(new { cname = "win_uvalue", data = __data[26] });
                data.Add(new { cname = "win_rulevalue", data = __data[27] });
                data.Add(new { cname = "win_rulevalue_point", data = __data[50] });
                #endregion

                #region 커튼월창정보
                Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='커튼월창'");
                if (Value.Length > 0)
                {
                    __data[28].Add(new { idx = i, val = Value.Length }); //창호 유형 개수
                }
                double Total_Area_CW = 0, Uvalue_CW = 0, RuleValue_CW = 0;
                Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.유리부분유효열관류율,b.법규유리부분열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionCW AS b ON a.구조체번호 = b.번호 where a.커튼월부위 ='유리부분'");
                for (int k = 0; k < Value.Length; k++)
                {
                    if (Value.Length > 0)
                    {
                        Total_Area_CW += Convert.ToDouble(Value[k][0]);
                        Uvalue_CW += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                        RuleValue_CW += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][2]);
                    }
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.패널부분유효열관류율,b.법규패널부분열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionCW AS b ON a.구조체번호 = b.번호 where a.커튼월부위 ='패널부분'");
                for (int k = 0; k < Value.Length; k++)
                {
                    if (Value.Length > 0)
                    {
                        Total_Area_CW += Convert.ToDouble(Value[k][0]);
                        Uvalue_CW += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                        RuleValue_CW += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][2]);
                    }
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.출입문부분유효열관류율,b.법규출입문부분열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionCW AS b ON a.구조체번호 = b.번호 where a.커튼월부위 ='출입문부분'");
                for (int k = 0; k < Value.Length; k++)
                {
                    if (Value.Length > 0)
                    {
                        Total_Area_CW += Convert.ToDouble(Value[k][0]);
                        Uvalue_CW += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                        RuleValue_CW += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][2]);
                    }
                }
                Uvalue_CW = Uvalue_CW / Total_Area_CW;
                RuleValue_CW = RuleValue_CW / Total_Area_CW;
                __data[29].Add(new { idx = i, val = Total_Area_CW.ToString("0.0") }); //커튼월창 면적
                __data[30].Add(new { idx = i, val = Uvalue_CW.ToString("0.00") }); //커튼월창 유효 열관류율
                __data[31].Add(new { idx = i, val = RuleValue_CW.ToString("0.00") }); //커튼월창 법규 열관류율 
                d = RuleValue_CW / Uvalue_CW * 100; if (d >= 100) { d = 100; }
                if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }
                sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                __data[51].Add(new { idx = i, val = sp }); //법규 열관류율   

                data.Add(new { cname = "cw_count", data = __data[28] });
                data.Add(new { cname = "cw_area", data = __data[29] });
                data.Add(new { cname = "cw_uvalue", data = __data[30] });
                data.Add(new { cname = "cw_rulevalue", data = __data[31] });
                data.Add(new { cname = "cw_rulevalue_point", data = __data[51] });
                #endregion

                #region 출입문정보
                Value = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='외부출입문'");
                if (Value.Length > 0)
                {
                    __data[32].Add(new { idx = i, val = Value.Length }); //출입문 유형 개수
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.문유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionDoor AS b ON a.구조체번호 = b.번호");
                if (Value.Length > 0)
                {
                    double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                    for (int k = 0; k < Value.Length; k++)
                    {
                        Total_Area += Convert.ToDouble(Value[k][0]);
                        Uvalue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                        RuleValue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][2]);
                    }
                    Uvalue = Uvalue / Total_Area;
                    RuleValue = RuleValue / Total_Area;
                    __data[33].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //출입문 면적
                    __data[34].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //출입문 유효 열관류율
                    __data[35].Add(new { idx = i, val = RuleValue.ToString("0.00") }); //출입문 법규 열관류율   
                    d = RuleValue / Uvalue * 100;if(d>=100){d=100;}
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __data[52].Add(new { idx = i, val = sp }); //법규 열관류율     
                }
                ////////////////////////////////////////////////////////////////////
                data.Add(new { cname = "door_count", data = __data[32] });
                data.Add(new { cname = "door_area", data = __data[33] });
                data.Add(new { cname = "door_uvalue", data = __data[34] });
                data.Add(new { cname = "door_rulevalue", data = __data[35] });
                data.Add(new { cname = "door_rulevalue_point", data = __data[52] });
                #endregion

                #region 소요량
                double[] 난방 = new double[12], 냉방 = new double[12], 급탕 = new double[12], 조명 = new double[12], 공조 = new double[12], 신재생 = new double[12], 총전기 = new double[12], 총가스 = new double[12], 총소요량 = new double[12];
                double 연간소요량 = 0, 연간전기 = 0, 연간가스 = 0;
                for (int mth = 0; mth < 12; mth++)
                {
                    string[][] Final1 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량", "연료='전기' and 월 ='" + (mth + 1).ToString() + "월'");
                    string[][] Final2 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량", "not 연료='전기' and not 연료='전체'  and 월 ='" + (mth + 1).ToString() + "월'");
                    if (Final1.Length > 0)
                    {
                        난방[mth] = Convert.ToDouble(Final1[0][0]);
                        냉방[mth] = Convert.ToDouble(Final1[0][1]);
                        급탕[mth] = Convert.ToDouble(Final1[0][2]);
                        조명[mth] = Convert.ToDouble(Final1[0][3]);
                        공조[mth] = Convert.ToDouble(Final1[0][4]);
                        신재생[mth] = Convert.ToDouble(Final1[0][6]);
                        총전기[mth] = Convert.ToDouble(Final1[0][7]) - Convert.ToDouble(Final1[0][5]);
                    }
                    if (Final2.Length > 0)
                    {
                        난방[mth] = 난방[mth] + Convert.ToDouble(Final2[0][0]);
                        냉방[mth] = 냉방[mth] + Convert.ToDouble(Final2[0][1]);
                        급탕[mth] = 급탕[mth] + Convert.ToDouble(Final2[0][2]);
                        조명[mth] = 조명[mth] + Convert.ToDouble(Final2[0][3]);
                        공조[mth] = 공조[mth] + Convert.ToDouble(Final2[0][4]);
                        신재생[mth] = 신재생[mth] + Convert.ToDouble(Final2[0][6]);
                        총가스[mth] = Convert.ToDouble(Final2[0][7]) - Convert.ToDouble(Final2[0][5]);
                    }

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
                }
                double tCO2 = 연간전기 * 0.4747 / 1000000 * 1000 + 연간가스 / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                double TOE = 연간전기 * 0.00023 + 연간가스 / 43.1 / 0.277778 * 0.00103;
                double 연간1차 = 연간전기 * 2.75 + 연간가스  * 1.1;
                double Area = 0;
                string[][] A = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "냉난방유무 <> '비냉난방'");
                if (A.Length > 0)
                {
                    for (int a = 0; a < A.Length; a++)
                    {
                        Area += Convert.ToDouble(A[a][0]);
                    }
                }
                __data[43].Add(new { idx = i, val = (연간소요량).ToString("#,##0") });
                __data[44].Add(new { idx = i, val = (연간소요량 / Area).ToString("0.0") });
                __data[45].Add(new { idx = i, val = tCO2.ToString("0.0") });
                __data[46].Add(new { idx = i, val = TOE.ToString("0.0") });
                __data[53].Add(new { idx = i, val = (연간1차).ToString("#,##0") });
                __data[54].Add(new { idx = i, val = (연간1차 / Area).ToString("0.0") });
                ////////////////////////////////////////////////////////////////////
                data.Add(new { cname = "qh_mth", data = __data[36] });
                data.Add(new { cname = "qc_mth", data = __data[37] });
                data.Add(new { cname = "qw_mth", data = __data[38] });
                data.Add(new { cname = "ql_mth", data = __data[39] });
                data.Add(new { cname = "qv_mth", data = __data[40] });
                data.Add(new { cname = "qreg_mth", data = __data[41] });
                data.Add(new { cname = "qf_mth", data = __data[42] });
                data.Add(new { cname = "qfa", data = __data[43] });
                data.Add(new { cname = "qfa_area", data = __data[44] });
                data.Add(new { cname = "tco2", data = __data[45] });
                data.Add(new { cname = "toe", data = __data[46] });
                data.Add(new { cname = "qpa", data = __data[53] });
                data.Add(new { cname = "qpa_area", data = __data[54] });

                List<object> 난방소요량chart = new List<object>();
                List<object> 냉방소요량chart = new List<object>();
                List<object> 급탕소요량chart = new List<object>();
                List<object> 조명소요량chart = new List<object>();
                List<object> 공조소요량chart = new List<object>();
                List<object> 기저소요량chart = new List<object>();
                for (int mth = 0; mth < 12; mth++)
                {
                    난방소요량chart.Add(Math.Round(double.Parse(Program.UTIL.asFixed(난방[mth].ToString())), 3) + 0);
                    냉방소요량chart.Add(Math.Round(double.Parse(Program.UTIL.asFixed(냉방[mth].ToString())), 3) + 0);
                    급탕소요량chart.Add(Math.Round(double.Parse(Program.UTIL.asFixed(급탕[mth].ToString())), 3) + 0);
                    조명소요량chart.Add(Math.Round(double.Parse(Program.UTIL.asFixed(조명[mth].ToString())), 3) + 0);
                    공조소요량chart.Add(Math.Round(double.Parse(Program.UTIL.asFixed(공조[mth].ToString())), 3) + 0);
                }
                chart_난방소요량.Add(System.Text.Json.JsonSerializer.Serialize(난방소요량chart.ToArray()));
                chart_냉방소요량.Add(System.Text.Json.JsonSerializer.Serialize(냉방소요량chart.ToArray()));
                chart_급탕소요량.Add(System.Text.Json.JsonSerializer.Serialize(급탕소요량chart.ToArray()));
                chart_조명소요량.Add(System.Text.Json.JsonSerializer.Serialize(조명소요량chart.ToArray()));
                chart_공조소요량.Add(System.Text.Json.JsonSerializer.Serialize(공조소요량chart.ToArray()));
                chart_공조소요량.Add(System.Text.Json.JsonSerializer.Serialize(공조소요량chart.ToArray()));
                #endregion

                #region 보일러정보   
                double boiler_count = 0; double boiler_power = 0; double boiler_eta_avg = 0; double boiler_eta_rule = 0; double boiler_point = 0;
                string[][] Value1 = Program.DB.querySQL(DB.type.ProjDB, "Select b.보일러종류,a.명칭,b.존,a.용량,a.전부하효율,a.난방급탕,b.번호,b.보일러대수 From User_Boiler as a Inner Join HeatingSystem_Form as b ON a.번호 = b.보일러종류 Where a.난방급탕='난방+급탕'");
                if (Value1.Length> 0)
                {
                    for(int a=0; a<Value1.Length;a++)
                    {
                        boiler_count += Convert.ToDouble(Value1[a][7]);
                        boiler_power += Convert.ToDouble(Value1[a][3]) * Convert.ToDouble(Value1[a][7]);
                        boiler_eta_avg += Convert.ToDouble(Value1[a][4]) * Convert.ToDouble(Value1[a][3]) * Convert.ToDouble(Value1[a][7]);
                    }
                }
                string[][] Value2 = Program.DB.querySQL(DB.type.ProjDB, "Select b.보일러종류,a.명칭,b.존,a.용량,a.전부하효율,a.난방급탕,b.번호,b.보일러대수 From User_Boiler as a Inner Join HeatingSystem_Form as b ON a.번호 = b.보일러종류 Where a.난방급탕='난방'");
                if (Value2.Length > 0)
                {
                    for (int a = 0; a < Value2.Length; a++)
                    {
                        boiler_count += Convert.ToDouble(Value2[a][7]);
                        boiler_power += Convert.ToDouble(Value2[a][3]) * Convert.ToDouble(Value2[a][7]);
                        boiler_eta_avg += Convert.ToDouble(Value2[a][4]) * Convert.ToDouble(Value2[a][3]) * Convert.ToDouble(Value2[a][7]);
                    }
                }
                string[][] Value3 = Program.DB.querySQL(DB.type.ProjDB, "Select b.보일러종류,a.명칭,b.존,a.용량,a.전부하효율,a.난방급탕,b.번호,b.보일러대수 From User_Boiler as a Inner Join DHWSystem_Form as b ON a.번호 = b.보일러종류 Where a.난방급탕='급탕'");
                if (Value3.Length > 0)
                {
                    for (int a = 0; a < Value3.Length; a++)
                    {
                        boiler_count += Convert.ToDouble(Value3[a][7]);
                        boiler_power += Convert.ToDouble(Value3[a][3])* Convert.ToDouble(Value3[a][7]);
                        boiler_eta_avg += Convert.ToDouble(Value3[a][4]) * Convert.ToDouble(Value3[a][3])* Convert.ToDouble(Value3[a][7]);
                    }
                }
                if (boiler_count > 0)
                {
                    boiler_eta_avg = boiler_eta_avg / boiler_power;
                    boiler_eta_rule = 90;
                    boiler_point = Math.Min(100, boiler_eta_avg / boiler_eta_rule * 100);
                    __data[55].Add(new { idx = i, val = boiler_count.ToString("0") }); //보일러 개수 
                    __data[56].Add(new { idx = i, val = boiler_power.ToString("0.0") }); //보일러 용량
                    __data[57].Add(new { idx = i, val = boiler_eta_avg.ToString("0.0") + " %" }); //보일러 효율
                    __data[58].Add(new { idx = i, val = boiler_eta_rule.ToString("0.0") + " %" }); //보일러 권장 효율 
                    d = boiler_point;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __data[59].Add(new { idx = i, val = sp }); //성능점수       
                }
                else
                {
                    __data[55].Add(new { idx = i, val = "-" }); //보일러 개수 
                    __data[56].Add(new { idx = i, val = "-" }); //보일러 용량
                    __data[57].Add(new { idx = i, val = "-" }); //보일러 효율
                    __data[58].Add(new { idx = i, val = "-" }); //보일러 권장 효율    
                    __data[59].Add(new { idx = i, val = "-" }); //성능점수       
                }               
                data.Add(new { cname = "boiler_count", data = __data[55] });
                data.Add(new { cname = "boiler_power", data = __data[56] });
                data.Add(new { cname = "boiler_eta_avg", data = __data[57] });
                data.Add(new { cname = "boiler_eta_rule", data = __data[58] });
                data.Add(new { cname = "boiler_point", data = __data[59] });
                #endregion
                #region 냉난방EHP정보   
                double ehp_count = 0; double ehp_power = 0; double ehp_cop_avg = 0; double ehp_cop_rule = 0; double ehp_point = 0;
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.외기히트펌프번호,a.명칭,b.존,a.난방정격용량,a.난방정격COP,a.냉방정격용량,a.냉방정격COP,b.번호,b.외기히트펌프대수 From User_AirHP as a Inner Join HeatingSystem_Form as b ON a.번호 = b.외기히트펌프번호 Where a.난방냉방='냉난방' And a.연료='전기'");
                if (Value.Length > 0)
                {
                    for (int a = 0; a < Value.Length; a++)
                    {
                        ehp_count += Convert.ToDouble(Value[a][8]);
                        ehp_power += Convert.ToDouble(Value[a][3])* Convert.ToDouble(Value[a][8]);
                        ehp_cop_avg += Convert.ToDouble(Value[a][4]) * Convert.ToDouble(Value[a][3])* Convert.ToDouble(Value[a][8]);
                    }
                    ehp_cop_avg = ehp_cop_avg / ehp_power;
                    ehp_cop_rule = 5.5;
                    ehp_point = Math.Min(100, ehp_cop_avg / ehp_cop_rule * 100);
                    __data[60].Add(new { idx = i, val = ehp_count.ToString("0") }); //개수 
                    __data[61].Add(new { idx = i, val = ehp_power.ToString("0.0") }); //용량
                    __data[62].Add(new { idx = i, val = ehp_cop_avg.ToString("0.0") + " W/W" }); //성능
                    __data[63].Add(new { idx = i, val = ehp_cop_rule.ToString("0.0") + " W/W" }); //권장 성능                
                    d = ehp_point;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __data[64].Add(new { idx = i, val = sp }); //성능점수       
                    
                }
                else
                {
                    __data[60].Add(new { idx = i, val = "-" }); //개수 
                    __data[61].Add(new { idx = i, val = "-" }); //용량
                    __data[62].Add(new { idx = i, val = "-" }); //성능
                    __data[63].Add(new { idx = i, val = "-" }); //권장 성능  
                    __data[64].Add(new { idx = i, val = "-" }); //성능점수       
                }
                data.Add(new { cname = "ehp_count", data = __data[60] });
                data.Add(new { cname = "ehp_power", data = __data[61] });
                data.Add(new { cname = "ehp_cop_avg", data = __data[62] });
                data.Add(new { cname = "ehp_cop_rule", data = __data[63] });
                data.Add(new { cname = "ehp_point", data = __data[64] });
                #endregion
                #region 냉난방GHP정보   
                double ghp_count = 0; double ghp_power = 0; double ghp_cop_avg = 0; double ghp_cop_rule = 0; double ghp_point = 0;
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.외기히트펌프번호,a.명칭,b.존,a.난방정격용량,a.난방정격COP,a.냉방정격용량,a.냉방정격COP,b.번호,b.외기히트펌프대수  From User_AirHP as a Inner Join HeatingSystem_Form as b ON a.번호 = b.외기히트펌프번호 Where a.난방냉방='냉난방' And NOT a.연료='전기'");
                if (Value.Length > 0)
                {
                    for (int a = 0; a < Value.Length; a++)
                    {
                        ghp_count += Convert.ToDouble(Value[a][8]);
                        ghp_power += Convert.ToDouble(Value[a][3])* Convert.ToDouble(Value[a][8]);
                        ghp_cop_avg += Convert.ToDouble(Value[a][4]) * Convert.ToDouble(Value[a][3])* Convert.ToDouble(Value[a][8]);
                    }
                    ghp_cop_avg = ghp_cop_avg / ghp_power;
                    ghp_cop_rule = 5.5;
                    ghp_point = Math.Min(100, ghp_cop_avg / ghp_cop_rule * 100);
                    __data[65].Add(new { idx = i, val = ghp_count.ToString("0") }); //개수 
                    __data[66].Add(new { idx = i, val = ghp_power.ToString("0.0") }); //용량
                    __data[67].Add(new { idx = i, val = ghp_cop_avg.ToString("0.0") + " W/W" }); //성능
                    __data[68].Add(new { idx = i, val = ghp_cop_rule.ToString("0.0") + " W/W" }); //권장 성능                
                    d = ghp_point;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __data[69].Add(new { idx = i, val = sp }); //성능점수       
                   
                }
                else
                {
                    __data[65].Add(new { idx = i, val = "-" }); //개수 
                    __data[66].Add(new { idx = i, val = "-" }); //용량
                    __data[67].Add(new { idx = i, val = "-" }); //성능
                    __data[68].Add(new { idx = i, val = "-" }); //권장 성능      
                    __data[69].Add(new { idx = i, val = "-" }); //성능점수       
                }
                data.Add(new { cname = "ghp_count", data = __data[65] });
                data.Add(new { cname = "ghp_power", data = __data[66] });
                data.Add(new { cname = "ghp_cop_avg", data = __data[67] });
                data.Add(new { cname = "ghp_cop_rule", data = __data[68] });
                data.Add(new { cname = "ghp_point", data = __data[69] });
                #endregion
                #region 흡수식냉온수기정보   
                double abs_count = 0; double abs_power = 0; double abs_cop_avg = 0; double abs_cop_rule = 0; double abs_point = 0;
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.흡수식온수기번호,a.명칭,b.존,a.난방용량,a.난방성능,a.냉방용량,a.냉방성능,b.번호,b.흡수식온수기대수 From User_ABS as a Inner Join HeatingSystem_Form as b ON a.번호 = b.흡수식온수기번호 Where a.난방냉방='냉난방'");
                if (Value.Length > 0)
                {
                    for (int a = 0; a < Value.Length; a++)
                    {
                        abs_count += Convert.ToDouble(Value[a][8]);
                        abs_power += Convert.ToDouble(Value[a][3])* Convert.ToDouble(Value[a][8]);
                        abs_cop_avg += Convert.ToDouble(Value[a][4]) * Convert.ToDouble(Value[a][3])* Convert.ToDouble(Value[a][8]);
                    }
                    abs_cop_avg = abs_cop_avg / abs_power;
                    abs_cop_rule = 5.5;
                    abs_point = Math.Min(100, abs_cop_avg / abs_cop_rule * 100);
                    __data[70].Add(new { idx = i, val = abs_count.ToString("0") }); //개수 
                    __data[71].Add(new { idx = i, val = abs_power.ToString("0.0") }); //용량
                    __data[72].Add(new { idx = i, val = abs_cop_avg.ToString("0.0") + " W/W" }); //성능
                    __data[73].Add(new { idx = i, val = abs_cop_rule.ToString("0.0") + " W/W" }); //권장 성능                
                    d = abs_point;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __data[74].Add(new { idx = i, val = sp }); //성능점수                         
                }
                else
                {
                    __data[70].Add(new { idx = i, val = "-" }); //개수 
                    __data[71].Add(new { idx = i, val = "-" }); //용량
                    __data[72].Add(new { idx = i, val = "-" }); //성능
                    __data[73].Add(new { idx = i, val = "-" }); //권장 성능      
                    __data[74].Add(new { idx = i, val = "-" }); //성능점수                 
                }
                data.Add(new { cname = "abs_count", data = __data[70] });
                data.Add(new { cname = "abs_power", data = __data[71] });
                data.Add(new { cname = "abs_cop_avg", data = __data[72] });
                data.Add(new { cname = "abs_cop_rule", data = __data[73] });
                data.Add(new { cname = "abs_point", data = __data[74] });
                #endregion
                #region 에어컨정보   
                double airc_count = 0; double airc_power = 0; double airc_cop_avg = 0; double airc_cop_rule = 0; double airc_point = 0;
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.냉방유닛,a.명칭,b.공급존,a.난방정격용량,a.난방정격COP,a.냉방정격용량,a.냉방정격COP,b.번호,b.설치대수 From User_AirHP as a Inner Join CoolingSystem_Form as b ON a.번호 = b.냉방유닛 Where a.난방냉방='냉방' And a.연료='전기'");
                if (Value.Length > 0)
                {
                    for (int a = 0; a < Value.Length; a++)
                    {
                        airc_count += Convert.ToDouble(Value[a][8]);
                        airc_power += Convert.ToDouble(Value[a][5])* Convert.ToDouble(Value[a][8]);
                        airc_cop_avg += Convert.ToDouble(Value[a][6]) * Convert.ToDouble(Value[a][5])* Convert.ToDouble(Value[a][8]);
                    }
                    airc_cop_avg = airc_cop_avg / airc_power;
                    airc_cop_rule = 5.5;
                    airc_point = Math.Min(100, airc_cop_avg / airc_cop_rule * 100);
                    __data[75].Add(new { idx = i, val = airc_count.ToString("0") }); //개수 
                    __data[76].Add(new { idx = i, val = airc_power.ToString("0.0") }); //용량
                    __data[77].Add(new { idx = i, val = airc_cop_avg.ToString("0.0") + " W/W" }); //성능
                    __data[78].Add(new { idx = i, val = airc_cop_rule.ToString("0.0") + " W/W" }); //권장 성능                
                    d = airc_point;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __data[79].Add(new { idx = i, val = sp }); //성능점수    
                }
                else
                {
                    __data[75].Add(new { idx = i, val = "-" }); //개수 
                    __data[76].Add(new { idx = i, val = "-" }); //용량
                    __data[77].Add(new { idx = i, val = "-" }); //성능
                    __data[78].Add(new { idx = i, val = "-" }); //권장 성능       
                    __data[79].Add(new { idx = i, val = "-" }); //성능점수             
                }
                data.Add(new { cname = "airc_count", data = __data[75] });
                data.Add(new { cname = "airc_power", data = __data[76] });
                data.Add(new { cname = "airc_cop_avg", data = __data[77] });
                data.Add(new { cname = "airc_cop_rule", data = __data[78] });
                data.Add(new { cname = "airc_point", data = __data[79] });
                #endregion
                #region 냉동기정보   
                double chiler_count = 0; double chiler_power = 0; double chiler_cop_avg = 0; double chiler_cop_rule = 0; double chiler_point = 0;
                string[][] Value11 = Program.DB.querySQL(DB.type.ProjDB, "Select b.냉방유닛,a.명칭,b.공급존,a.냉방출력,a.EER,b.번호,b.설치대수 From User_AirCooler as a Inner Join CoolingSystem_Form as b ON a.번호 = b.냉방유닛");
                if (Value1.Length > 0)
                { 
                    for (int a = 0; a < Value11.Length; a++)
                    {
                        chiler_count += Convert.ToDouble(Value11[a][6]);
                        chiler_power += Convert.ToDouble(Value11[a][3])* Convert.ToDouble(Value11[a][6]);
                        chiler_cop_avg += Convert.ToDouble(Value11[a][4]) * Convert.ToDouble(Value11[a][3])* Convert.ToDouble(Value11[a][6]);
                    }
                }
                string[][] Value12 = Program.DB.querySQL(DB.type.ProjDB, "Select b.냉방유닛,a.명칭,b.공급존,a.냉방출력,a.EER,b.번호,b.설치대수 From User_WaterCooler as a Inner Join CoolingSystem_Form as b ON a.번호 = b.냉방유닛");
                if (Value2.Length > 0)
                {
                    for (int a = 0; a < Value12.Length; a++)
                    {
                        chiler_count += Convert.ToDouble(Value12[a][6]);
                        chiler_power += Convert.ToDouble(Value12[a][3])* Convert.ToDouble(Value12[a][6]);
                        chiler_cop_avg += Convert.ToDouble(Value12[a][4]) * Convert.ToDouble(Value12[a][3])* Convert.ToDouble(Value12[a][6]);
                    }
                }
                if (Value11.Length > 0 || Value12.Length >0)
                {
                    chiler_cop_avg = chiler_cop_avg / chiler_power;
                    chiler_cop_rule = 5.5;
                    chiler_point = Math.Min(100, chiler_cop_avg / chiler_cop_rule * 100);
                    __data[80].Add(new { idx = i, val = chiler_count.ToString("0") }); //개수 
                    __data[81].Add(new { idx = i, val = chiler_power.ToString("0.0") }); //용량
                    __data[82].Add(new { idx = i, val = chiler_cop_avg.ToString("0.0") + " W/W" }); //성능
                    __data[83].Add(new { idx = i, val = chiler_cop_rule.ToString("0.0") + " W/W" }); //권장 성능                
                    d = chiler_point;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __data[84].Add(new { idx = i, val = sp }); //성능점수    
                }
                else
                {
                    __data[80].Add(new { idx = i, val = "-" }); //개수 
                    __data[81].Add(new { idx = i, val = "-" }); //용량
                    __data[82].Add(new { idx = i, val = "-" }); //성능
                    __data[83].Add(new { idx = i, val = "-" }); //권장 성능 
                    __data[84].Add(new { idx = i, val = "-" }); //성능점수    
                }
                data.Add(new { cname = "chiler_count", data = __data[81] });
                data.Add(new { cname = "chiler_power", data = __data[82] });
                data.Add(new { cname = "chiler_cop_avg", data = __data[83] });
                data.Add(new { cname = "chiler_cop_rule", data = __data[84] });
                data.Add(new { cname = "chiler_point", data = __data[85] });
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
                                light_area = Convert.ToDouble(조명존[aa][1]);
                                light_density = Convert.ToDouble(조명존[aa][1]) * Convert.ToDouble(조명존[aa][2]);
                                light_eta_avg = Convert.ToDouble(조명존[aa][1]) * Convert.ToDouble(조명존[aa][4]);
                            }
                        }
                    }
                    light_density = light_density / light_area;
                    light_eta_avg = light_eta_avg / light_area;
                    light_eta_rule = 70;
                    light_point = Math.Min(100, light_eta_avg / light_eta_rule * 100);
                    __data[85].Add(new { idx = i, val = light_count}); //개수 
                    __data[86].Add(new { idx = i, val = light_density.ToString("0.0")+ " W/m"+ Program.UTIL.Subscript(2, true) }); //용량
                    __data[87].Add(new { idx = i, val = light_eta_avg.ToString("0")+ " lm/W" }); //성능
                    __data[88].Add(new { idx = i, val = light_eta_rule.ToString("0") + " lm/W" }); //권장 성능                
                    d = light_point;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __data[89].Add(new { idx = i, val = sp }); //성능점수       
                    data.Add(new { cname = "light_count", data = __data[85] });
                    data.Add(new { cname = "light_power", data = __data[86] });
                    data.Add(new { cname = "light_eta_avg", data = __data[87] });
                    data.Add(new { cname = "light_eta_rule", data = __data[88] });
                    data.Add(new { cname = "light_point", data = __data[89] });
                }

                #endregion
                #region 태양광정보   
                double pv_count = 0; double pv_power = 0; double pv_eta_avg = 0; string pv_eta_rule = "-"; double pv_point = 0;
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select a.번호,a.명칭,a.모듈번호,a.개수,a.개수,a.용량,a.면적,b.CELLTYPE,b.Kpk From PV_Form as a inner Join User_PV as b on a.모듈번호=b.번호");
                if (Value.Length > 0)
                {
                   
                    for (int a = 0; a < Value.Length; a++)
                    {
                        pv_count += Convert.ToDouble(Value[a][3]) ;
                        pv_power += Convert.ToDouble(Value[a][5]);
                        pv_eta_avg += Convert.ToDouble(Value[a][8]) * Convert.ToDouble(Value[a][5]);
                    }
                    pv_eta_avg = pv_eta_avg / pv_power;
                    pv_point = 100;
                    __data[80].Add(new { idx = i, val = pv_count.ToString("0") }); //개수 
                    __data[81].Add(new { idx = i, val = pv_power.ToString("0.0") }); //용량
                    __data[82].Add(new { idx = i, val = pv_eta_avg.ToString("0.00")+ " kW/m"+ Program.UTIL.Subscript(2, true) }); //성능
                    __data[83].Add(new { idx = i, val = pv_eta_rule.ToString() }); //권장 성능                
                    d = pv_point;
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 235) / 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __data[84].Add(new { idx = i, val = sp }); //성능점수    
                }
                else
                {
                    __data[81].Add(new { idx = i, val = "-" }); //개수 
                    __data[82].Add(new { idx = i, val = "-" }); //용량
                    __data[83].Add(new { idx = i, val = "-" }); //성능
                    __data[84].Add(new { idx = i, val = "-" }); //권장 성능       
                    __data[85].Add(new { idx = i, val = "-" }); //성능점수             
                }
                data.Add(new { cname = "pv_count", data = __data[81] });
                data.Add(new { cname = "pv_power", data = __data[82] });
                data.Add(new { cname = "pv_eta_avg", data = __data[83] });
                data.Add(new { cname = "pv_eta_rule", data = __data[84] });
                data.Add(new { cname = "pv_point", data = __data[85] });
                #endregion


                items.Add("MainReport_Before.htm");
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
                "],max:" + (Math.Round(max / 1000) * 1000 + 500).ToString() + ",step:100,legend:true,stacked:true}";
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
            List<object>[] __data = new List<object>[700];

            int i = -1, n;
            while (++i < 700)
            {
                __data[i] = new List<object>();
            }


            string charts = "";

            i = -1;
            while (++i < 번호.Length)
            {
                if (res.Length > 0)
                {
                    #region 건물정보
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트명,주소,지역,지역구분,준공시기,연면적,건축면적,지상층수,지하층수,작성자회사,작성자,작성시기,프로젝트번호");
                    if (Value.Length > 0)
                    {
                        __data[0].Add(new { idx = i, val = Value[0][0] }); //프로젝트명
                        __data[1].Add(new { idx = i, val = Value[0][1] }); //주소
                        __data[2].Add(new { idx = i, val = Value[0][2] }); //지역
                        __data[3].Add(new { idx = i, val = Value[0][3] }); //지역구분
                        __data[4].Add(new { idx = i, val = Value[0][4] }); //준공시기
                        __data[5].Add(new { idx = i, val = Value[0][5] }); //연면적
                        __data[6].Add(new { idx = i, val = Value[0][6] }); //건축면적
                        __data[7].Add(new { idx = i, val = Value[0][7] }); //지상층수
                        __data[8].Add(new { idx = i, val = Value[0][8] }); //지하층수
                        __data[9].Add(new { idx = i, val = Value[0][9] }); //작성자회사
                        __data[10].Add(new { idx = i, val = Value[0][10] }); //작성자
                        __data[11].Add(new { idx = i, val = Value[0][11] }); //작성시기
                        __data[136].Add(new { idx = i, val = Value[0][12] }); //프로젝트번호
                        __data[137].Add(new { idx = i, val = "1F_Zone002" }); //프로젝트번호

                    }
                    ////////////////////////////////////////////////////////////////////
                    data.Add(new { cname = "projectName", data = __data[0] });
                    data.Add(new { cname = "buildinglocation", data = __data[1] });
                    data.Add(new { cname = "climate", data = __data[2] }); 
                    data.Add(new { cname = "bylawclimate", data = __data[3] });
                    data.Add(new { cname = "construcitondate", data = __data[4] });
                    data.Add(new { cname = "grossarea", data = __data[5] });
                    data.Add(new { cname = "buildingarea", data = __data[6] });
                    data.Add(new { cname = "aboveground", data = __data[7] });
                    data.Add(new { cname = "underground", data = __data[8] });
                    data.Add(new { cname = "reviewercompany", data = __data[9] });
                    data.Add(new { cname = "reviewername", data = __data[10] });
                    data.Add(new { cname = "reviewdate", data = __data[11] });
                    data.Add(new { cname = "projectnum", data = __data[136] });
                    data.Add(new { cname = "zonenum", data = __data[137] });
                    #endregion

                    #region 외벽정보
                    //리모델링후
                    Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='외벽'");
                    if (Value.Length > 0)
                    {
                        __data[12].Add(new { idx = i, val = Value.Length }); //외벽 유형 개수
                    }
                    Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호");
                    if (Value.Length > 0)
                    {
                        double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                        for (int k = 0; k < Value.Length; k++)
                        {
                            Total_Area += Convert.ToDouble(Value[k][0]);
                            Uvalue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                            RuleValue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][2]);
                        }
                        Uvalue = Uvalue / Total_Area;
                        RuleValue = RuleValue / Total_Area;
                        __data[13].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //외벽 면적
                        __data[14].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //외벽 유효 열관류율
                        d = RuleValue / Uvalue * 100;if(d>=100){d=100;}
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 175)/ 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 175)/ 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 175)/ 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        __data[15].Add(new { idx = i, val = sp }); //법규 열관류율             
                    }
                    data.Add(new { cname = "wall_count_after", data = __data[12] });
                    data.Add(new { cname = "wall_area_after", data = __data[13] });
                    data.Add(new { cname = "wall_uvalue_after", data = __data[14] });
                    data.Add(new { cname = "wall_rulevalue", data = __data[15] });
                   
                    ////////////////////////////////////////////////////////////////////
                    //리모델링전
                    Value = Program.DB.querySQL(res[0][0], "SELECT DISTINCT  구조체번호 FROM ZoneEnvelope_3D WHERE 외피유형 = '외벽'");
                    if (Value.Length > 0)
                    {
                        __data[16].Add(new { idx = i, val = Value.Length }); //외벽 유형 개수
                    }
                    Value = Program.DB.querySQL(res[0][0], "select a.면적,b.유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호");
                    if (Value.Length > 0)
                    {
                        double Total_Area = 0, Uvalue = 0;
                        for (int k = 0; k < Value.Length; k++)
                        {
                            Total_Area += Convert.ToDouble(Value[k][0]);
                            Uvalue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                        }
                        Uvalue = Uvalue / Total_Area;
                        __data[17].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //외벽 면적
                        __data[18].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //외벽 유효 열관류율                  
                    }
                    data.Add(new { cname = "wall_count_before", data = __data[16] });
                    data.Add(new { cname = "wall_area_before", data = __data[17] });
                    data.Add(new { cname = "wall_uvalue_before", data = __data[18] });
                    #endregion

                    #region 지붕정보
                    //리모델링 후 
                    Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='지붕'");
                    if (Value.Length > 0)
                    {
                        __data[19].Add(new { idx = i, val = Value.Length }); //지붕 유형 개수
                    }
                    Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionRoof AS b ON a.구조체번호 = b.번호");
                    if (Value.Length > 0)
                    {
                        double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                        for (int k = 0; k < Value.Length; k++)
                        {
                            Total_Area += Convert.ToDouble(Value[k][0]);
                            Uvalue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                            RuleValue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][2]);
                        }
                        Uvalue = Uvalue / Total_Area;
                        RuleValue = RuleValue / Total_Area;
                        __data[20].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //지붕 면적
                        __data[21].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //지붕 유효 열관류율
                        d = RuleValue / Uvalue * 100;if(d>=100){d=100;}
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 175)/ 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 175)/ 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 175)/ 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        __data[22].Add(new { idx = i, val = sp }); //법규 열관류율                 
                    }
                    data.Add(new { cname = "roof_count_after", data = __data[19] });
                    data.Add(new { cname = "roof_area_after", data = __data[20] });
                    data.Add(new { cname = "roof_uvalue_after", data = __data[21] });
                    data.Add(new { cname = "roof_rulevalue", data = __data[22] });
                    ////////////////////////////////////////////////////////////////////
                    ///리모델링 전
                    Value = Program.DB.querySQL(res[0][0], "SELECT DISTINCT  구조체번호 FROM ZoneEnvelope_3D WHERE 외피유형 = '지붕'");
                    if (Value.Length > 0)
                    {
                        __data[23].Add(new { idx = i, val = Value.Length }); //지붕 유형 개수
                    }
                    Value = Program.DB.querySQL(res[0][0], "select a.면적,b.유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionRoof AS b ON a.구조체번호 = b.번호");
                    if (Value.Length > 0)
                    {
                        double Total_Area = 0, Uvalue = 0;
                        for (int k = 0; k < Value.Length; k++)
                        {
                            Total_Area += Convert.ToDouble(Value[k][0]);
                            Uvalue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                        }
                        Uvalue = Uvalue / Total_Area;
                        __data[24].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //지붕 면적
                        __data[25].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //지붕 유효 열관류율             
                    }
                    data.Add(new { cname = "roof_count_before", data = __data[23] });
                    data.Add(new { cname = "roof_area_before", data = __data[24] });
                    data.Add(new { cname = "roof_uvalue_before", data = __data[25] });

                    #endregion

                    #region 최하층바닥정보
                    //리모델링 후
                    Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='최하층바닥'");
                    if (Value.Length > 0)
                    {
                        __data[26].Add(new { idx = i, val = Value.Length }); //바닥 유형 개수
                    }
                    Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionFloor AS b ON a.구조체번호 = b.번호");
                    if (Value.Length > 0)
                    {
                        double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                        for (int k = 0; k < Value.Length; k++)
                        {
                            Total_Area += Convert.ToDouble(Value[k][0]);
                            Uvalue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                            RuleValue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][2]);
                        }
                        Uvalue = Uvalue / Total_Area;
                        RuleValue = RuleValue / Total_Area;
                        __data[27].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //바닥 면적
                        __data[28].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //바닥 유효 열관류율
                        d = RuleValue / Uvalue * 100;if(d>=100){d=100;}
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 175)/ 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 175)/ 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 175)/ 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        __data[29].Add(new { idx = i, val = sp }); //법규 열관류율              
                    }
                    data.Add(new { cname = "floor_count_after", data = __data[26] });
                    data.Add(new { cname = "floor_area_after", data = __data[27] });
                    data.Add(new { cname = "floor_uvalue_after", data = __data[28] });
                    data.Add(new { cname = "floor_rulevalue", data = __data[29] });
                    ////////////////////////////////////////////////////////////////////
                    ///리모델링 전
                    Value = Program.DB.querySQL(res[0][0], "SELECT DISTINCT  구조체번호 FROM ZoneEnvelope_3D WHERE 외피유형 = '최하층바닥'");
                    if (Value.Length > 0)
                    {
                        __data[30].Add(new { idx = i, val = Value.Length }); //바닥 유형 개수
                    }
                    Value = Program.DB.querySQL(res[0][0], "select a.면적,b.유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionFloor AS b ON a.구조체번호 = b.번호");
                    if (Value.Length > 0)
                    {
                        double Total_Area = 0, Uvalue = 0;
                        for (int k = 0; k < Value.Length; k++)
                        {
                            Total_Area += Convert.ToDouble(Value[k][0]);
                            Uvalue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                        }
                        Uvalue = Uvalue / Total_Area;
                        __data[31].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //바닥 면적
                        __data[32].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //바닥 유효 열관류율                  
                    }
                    data.Add(new { cname = "floor_count_before", data = __data[30] });
                    data.Add(new { cname = "floor_area_before", data = __data[31] });
                    data.Add(new { cname = "floor_uvalue_before", data = __data[32] });
                    #endregion

                    #region 창호정보
                    //리모델링 후
                    Value = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='창호'");
                    if (Value.Length > 0)
                    {
                        __data[33].Add(new { idx = i, val = Value.Length }); //창호 유형 개수
                    }
                    Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.창호유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체번호 = b.번호");
                    if (Value.Length > 0)
                    {
                        double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                        for (int k = 0; k < Value.Length; k++)
                        {
                            Total_Area += Convert.ToDouble(Value[k][0]);
                            Uvalue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                            RuleValue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][2]);
                        }
                        Uvalue = Uvalue / Total_Area;
                        RuleValue = RuleValue / Total_Area;
                        __data[34].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //창호 면적
                        __data[35].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //창호 유효 열관류율열관류율
                        d = RuleValue / Uvalue * 100;if(d>=100){d=100;}
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 175)/ 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 175)/ 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 175)/ 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        __data[36].Add(new { idx = i, val = sp }); //법규 열관류율                                    
                    }
                    data.Add(new { cname = "win_count_after", data = __data[33] });
                    data.Add(new { cname = "win_area_after", data = __data[34] });
                    data.Add(new { cname = "win_uvalue_after", data = __data[35] });
                    data.Add(new { cname = "win_rulevalue", data = __data[36] });
                    ////////////////////////////////////////////////////////////////////
                    /// 리모델링 전 
                    Value = Program.DB.querySQL(res[0][0], "SELECT  구조체번호 FROM ZoneEnvelope_3D WHERE 외피유형 = '창호'");
                    if (Value.Length > 0)
                    {
                        __data[37].Add(new { idx = i, val = Value.Length }); //창호 유형 개수
                    }
                    Value = Program.DB.querySQL(res[0][0], "select a.면적,b.창호유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체번호 = b.번호");
                    if (Value.Length > 0)
                    {
                        double Total_Area = 0, Uvalue = 0;
                        for (int k = 0; k < Value.Length; k++)
                        {
                            Total_Area += Convert.ToDouble(Value[k][0]);
                            Uvalue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                        }
                        Uvalue = Uvalue / Total_Area;
                        __data[38].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //창호 면적
                        __data[39].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //창호 유효 열관류율열관류율                              
                    }
                    data.Add(new { cname = "win_count_before", data = __data[37] });
                    data.Add(new { cname = "win_area_before", data = __data[38] });
                    data.Add(new { cname = "win_uvalue_before", data = __data[39] });
                    #endregion

                    #region 커튼월창정보
                    //리모델링 후 
                    Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='커튼월창'");
                    if (Value.Length > 0)
                    {
                        __data[40].Add(new { idx = i, val = Value.Length }); //창호 유형 개수
                    }
                    double Total_Area_CW_후 = 0, Uvalue_CW_후 = 0, RuleValue_CW_후 = 0;
                    Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.유리부분유효열관류율,b.법규유리부분열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionCW AS b ON a.구조체번호 = b.번호 where a.커튼월부위 ='유리부분'");
                    for (int k = 0; k < Value.Length; k++)
                    {
                        if (Value.Length > 0)
                        {
                            Total_Area_CW_후 += Convert.ToDouble(Value[k][0]);
                            Uvalue_CW_후 += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                            RuleValue_CW_후 += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][2]);
                        }
                    }
                    Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.패널부분유효열관류율,b.법규패널부분열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionCW AS b ON a.구조체번호 = b.번호 where a.커튼월부위 ='패널부분'");
                    for (int k = 0; k < Value.Length; k++)
                    {
                        if (Value.Length > 0)
                        {
                            Total_Area_CW_후 += Convert.ToDouble(Value[k][0]);
                            Uvalue_CW_후 += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                            RuleValue_CW_후 += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][2]);
                        }
                    }
                    Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.출입문부분유효열관류율,b.법규출입문부분열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionCW AS b ON a.구조체번호 = b.번호 where a.커튼월부위 ='출입문부분'");
                    for (int k = 0; k < Value.Length; k++)
                    {
                        if (Value.Length > 0)
                        {
                            Total_Area_CW_후 += Convert.ToDouble(Value[k][0]);
                            Uvalue_CW_후 += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                            RuleValue_CW_후 += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][2]);
                        }
                    }
                    Uvalue_CW_후 = Uvalue_CW_후 / Total_Area_CW_후;
                    RuleValue_CW_후 = RuleValue_CW_후 / Total_Area_CW_후;
                    __data[41].Add(new { idx = i, val = Total_Area_CW_후.ToString("0.0") }); //커튼월창 면적
                    __data[42].Add(new { idx = i, val = Uvalue_CW_후.ToString("0.00") }); //커튼월창 유효 열관류율
                    d = RuleValue_CW_후 / Uvalue_CW_후 * 100; if (d >= 100) { d = 100; }
                    if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 175)/ 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 175)/ 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                    else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 175)/ 100) + "px'></div>"; }
                    sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                    __data[43].Add(new { idx = i, val = sp }); //법규 열관류율                                                                                        
                    data.Add(new { cname = "cw_count_after", data = __data[40] });
                    data.Add(new { cname = "cw_area_after", data = __data[41] });
                    data.Add(new { cname = "cw_uvalue_after", data = __data[42] });
                    data.Add(new { cname = "cw_rulevalue", data = __data[43] });
                    ////////////////////////////////////////////////////////////////////
                    ///리모델링전
                    Value = Program.DB.querySQL(res[0][0], "SELECT DISTINCT  구조체번호 FROM ZoneEnvelope_3D WHERE 외피유형 = '커튼월창'");
                    if (Value.Length > 0)
                    {
                        __data[44].Add(new { idx = i, val = Value.Length }); //창호 유형 개수
                    }
                    double Total_Area_CW_전 = 0, Uvalue_CW_전 = 0, RuleValue_CW_전 = 0;
                    Value = Program.DB.querySQL(res[0][0], "select a.면적,b.유리부분유효열관류율,b.법규유리부분열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionCW AS b ON a.구조체번호 = b.번호 where a.커튼월부위 ='유리부분'");
                    for (int k = 0; k < Value.Length; k++)
                    {
                        if (Value.Length > 0)
                        {
                            Total_Area_CW_전 += Convert.ToDouble(Value[k][0]);
                            Uvalue_CW_전 += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                            RuleValue_CW_전 += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][2]);
                        }
                    }
                    Value = Program.DB.querySQL(res[0][0], "select a.면적,b.패널부분유효열관류율,b.법규패널부분열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionCW AS b ON a.구조체번호 = b.번호 where a.커튼월부위 ='패널부분'");
                    for (int k = 0; k < Value.Length; k++)
                    {
                        if (Value.Length > 0)
                        {
                            Total_Area_CW_전 += Convert.ToDouble(Value[k][0]);
                            Uvalue_CW_전 += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                            RuleValue_CW_전 += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][2]);
                        }
                    }
                    Value = Program.DB.querySQL(res[0][0], "select a.면적,b.출입문부분유효열관류율,b.법규출입문부분열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionCW AS b ON a.구조체번호 = b.번호 where a.커튼월부위 ='출입문부분'");
                    for (int k = 0; k < Value.Length; k++)
                    {
                        if (Value.Length > 0)
                        {
                            Total_Area_CW_전 += Convert.ToDouble(Value[k][0]);
                            Uvalue_CW_전 += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                            RuleValue_CW_전 += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][2]);
                        }
                    }
                    Uvalue_CW_전 = Uvalue_CW_전 / Total_Area_CW_전;
                    __data[45].Add(new { idx = i, val = Total_Area_CW_전.ToString("0.0") }); //커튼월창 면적
                    __data[46].Add(new { idx = i, val = Uvalue_CW_전.ToString("0.00") }); //커튼월창 유효 열관류율                                                                                          
                    data.Add(new { cname = "cw_count_before", data = __data[44] });
                    data.Add(new { cname = "cw_area_before", data = __data[45] });
                    data.Add(new { cname = "cw_uvalue_before", data = __data[46] });
                    #endregion

                    #region 출입문정보
                    //리모델링 후 
                    Value = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "외피유형='외부출입문'");
                    if (Value.Length > 0)
                    {
                        __data[47].Add(new { idx = i, val = Value.Length }); //외벽 유형 개수
                    }
                    Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.문유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionDoor AS b ON a.구조체번호 = b.번호");
                    if (Value.Length > 0)
                    {
                        double Total_Area = 0, Uvalue = 0, RuleValue = 0;
                        for (int k = 0; k < Value.Length; k++)
                        {
                            Total_Area += Convert.ToDouble(Value[k][0]);
                            Uvalue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                            RuleValue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][2]);
                        }
                        Uvalue = Uvalue / Total_Area;
                        RuleValue = RuleValue / Total_Area;
                        __data[48].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //출입문 면적
                        __data[49].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //출입문 유효 열관류율
                        d = RuleValue / Uvalue * 100;if(d>=100){d=100;}
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 175)/ 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 175)/ 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 175)/ 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        __data[50].Add(new { idx = i, val = sp }); //법규 열관류율                      
                    }
                    data.Add(new { cname = "door_count_after", data = __data[47] });
                    data.Add(new { cname = "door_area_after", data = __data[48] });
                    data.Add(new { cname = "door_uvalue_after", data = __data[49] });
                    data.Add(new { cname = "door_rulevalue_after", data = __data[50] });
                    ////////////////////////////////////////////////////////////////////
                    ///리모델링 전
                    Value = Program.DB.querySQL(res[0][0], "SELECT  구조체번호 FROM ZoneEnvelope_3D WHERE 외피유형 = '외부출입문'");
                    if (Value.Length > 0)
                    {
                        __data[51].Add(new { idx = i, val = Value.Length }); //외벽 유형 개수
                    }
                    Value = Program.DB.querySQL(res[0][0], "select a.면적,b.문유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionDoor AS b ON a.구조체번호 = b.번호");
                    if (Value.Length > 0)
                    {
                        double Total_Area = 0, Uvalue = 0;
                        for (int k = 0; k < Value.Length; k++)
                        {
                            Total_Area += Convert.ToDouble(Value[k][0]);
                            Uvalue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                        }
                        Uvalue = Uvalue / Total_Area;
                        __data[52].Add(new { idx = i, val = Total_Area.ToString("0.0") }); //출입문 면적
                        __data[53].Add(new { idx = i, val = Uvalue.ToString("0.00") }); //출입문 유효 열관류율         
                    }
                    data.Add(new { cname = "door_count_before", data = __data[51] });
                    data.Add(new { cname = "door_area_before", data = __data[52] });
                    data.Add(new { cname = "door_uvalue_before", data = __data[53] });
                    #endregion

                    #region 소요량
                    //리모델링 후 
                    double[] 난방_후 = new double[12], 냉방_후 = new double[12], 급탕_후 = new double[12], 조명_후 = new double[12], 공조_후 = new double[12], 신재생_후 = new double[12], 총전기_후 = new double[12], 총가스_후 = new double[12], 총소요량_후 = new double[12];
                    double 연간소요량_후 = 0, 연간전기_후 = 0, 연간가스_후 = 0;
                    for (int mth = 0; mth < 12; mth++)
                    {
                        string[][] Final1 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량", "연료='전기' and 월 ='" + (mth + 1).ToString() + "월'");
                        string[][] Final2 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량", "not 연료='전기' and not 연료='전체' and 월 ='" + (mth + 1).ToString() + "월'");
                        if (Final1.Length > 0)
                        {
                            난방_후[mth] = Convert.ToDouble(Final1[0][0]);
                            냉방_후[mth] = Convert.ToDouble(Final1[0][1]);
                            급탕_후[mth] = Convert.ToDouble(Final1[0][2]);
                            조명_후[mth] = Convert.ToDouble(Final1[0][3]);
                            공조_후[mth] = Convert.ToDouble(Final1[0][4]);
                            신재생_후[mth] = Convert.ToDouble(Final1[0][6]);
                            총전기_후[mth] = Convert.ToDouble(Final1[0][7]) - Convert.ToDouble(Final1[0][5]);
                        }
                        if (Final2.Length > 0)
                        {
                            난방_후[mth] = 난방_후[mth] + Convert.ToDouble(Final2[0][0]);
                            냉방_후[mth] = 냉방_후[mth] + Convert.ToDouble(Final2[0][1]);
                            급탕_후[mth] = 급탕_후[mth] + Convert.ToDouble(Final2[0][2]);
                            조명_후[mth] = 조명_후[mth] + Convert.ToDouble(Final2[0][3]);
                            공조_후[mth] = 공조_후[mth] + Convert.ToDouble(Final2[0][4]);
                            신재생_후[mth] = 신재생_후[mth] + Convert.ToDouble(Final2[0][6]);
                            총가스_후[mth] = Convert.ToDouble(Final2[0][7]) - Convert.ToDouble(Final2[0][5]);
                        }

                        총소요량_후[mth] = 총전기_후[mth] + 총가스_후[mth];
                    }
                    for (int mth = 0; mth < 12; mth++)
                    {
                        연간전기_후 += 총전기_후[mth];
                        연간가스_후 += 총가스_후[mth];
                        연간소요량_후 += 총소요량_후[mth];
                        __data[54].Add(new { idx = i * 12 + mth, val = 난방_후[mth].ToString("#,##0") });
                        __data[55].Add(new { idx = i * 12 + mth, val = 냉방_후[mth].ToString("#,##0") });
                        __data[56].Add(new { idx = i * 12 + mth, val = 급탕_후[mth].ToString("#,##0") });
                        __data[57].Add(new { idx = i * 12 + mth, val = 조명_후[mth].ToString("#,##0") });
                        __data[58].Add(new { idx = i * 12 + mth, val = 공조_후[mth].ToString("#,##0") });
                        __data[59].Add(new { idx = i * 12 + mth, val = 신재생_후[mth].ToString("#,##0") });
                        __data[60].Add(new { idx = i * 12 + mth, val = 총소요량_후[mth].ToString("#,##0") });
                    }

                    //리모델링 전 
                    double[] 난방_전 = new double[12], 냉방_전 = new double[12], 급탕_전 = new double[12], 조명_전 = new double[12], 공조_전 = new double[12], 신재생_전 = new double[12], 총전기_전 = new double[12], 총가스_전 = new double[12], 총소요량_전 = new double[12];
                    double 연간소요량_전 = 0, 연간전기_전 = 0, 연간가스_전 = 0;
                    for (int mth = 0; mth < 12; mth++)
                    {
                        string[][] Final1 = Program.DB.querySQL(res[0][0], "Select 난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량 from FinalEnergy_Result Where 연료='전기' and 월 ='" + (mth + 1).ToString() + "월'");
                        string[][] Final2 = Program.DB.querySQL(res[0][0], "Select 난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량 from FinalEnergy_Result Where not 연료='전기' and not 연료='전체' and 월 ='" + (mth + 1).ToString() + "월'");
                        if (Final1.Length > 0)
                        {
                            난방_전[mth] = Convert.ToDouble(Final1[0][0]);
                            냉방_전[mth] = Convert.ToDouble(Final1[0][1]);
                            급탕_전[mth] = Convert.ToDouble(Final1[0][2]);
                            조명_전[mth] = Convert.ToDouble(Final1[0][3]);
                            공조_전[mth] = Convert.ToDouble(Final1[0][4]);
                            신재생_전[mth] = Convert.ToDouble(Final1[0][6]);
                            총전기_전[mth] = Convert.ToDouble(Final1[0][7]) - Convert.ToDouble(Final1[0][5]);
                        }
                        if (Final2.Length > 0)
                        {
                            난방_전[mth] = 난방_전[mth] + Convert.ToDouble(Final2[0][0]);
                            냉방_전[mth] = 냉방_전[mth] + Convert.ToDouble(Final2[0][1]);
                            급탕_전[mth] = 급탕_전[mth] + Convert.ToDouble(Final2[0][2]);
                            조명_전[mth] = 조명_전[mth] + Convert.ToDouble(Final2[0][3]);
                            공조_전[mth] = 공조_전[mth] + Convert.ToDouble(Final2[0][4]);
                            신재생_전[mth] = 신재생_전[mth] + Convert.ToDouble(Final2[0][6]);
                            총가스_전[mth] =  Convert.ToDouble(Final2[0][7]) - Convert.ToDouble(Final2[0][5]);
                        }

                        총소요량_전[mth] = 총전기_전[mth] + 총가스_전[mth];
                    }
                    for (int mth = 0; mth < 12; mth++)
                    {
                        연간전기_전 += 총전기_전[mth];
                        연간가스_전 += 총가스_전[mth];
                        연간소요량_전 += 총소요량_전[mth];
                    }


                    double tCO2 = (연간전기_전 - 연간전기_후) * 0.4747 / 1000000 * 1000 + (연간가스_전 - 연간가스_후) / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    double TOE = (연간전기_전 - 연간전기_후) * 0.00023 + (연간가스_전 - 연간가스_후) / 43.1 / 0.277778 * 0.00103;
                    double 전_1차 = (연간전기_전 ) * 2.75 + (연간가스_전 ) * 1.1;
                    double 후_1차 = (연간전기_후) * 2.75 + (연간가스_후) * 1.1;
                    double Area = 0;
                    string[][] A = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "냉난방유무 <> '비냉난방'");
                    if (A.Length > 0)
                    {
                        for (int a = 0; a < A.Length; a++)
                        {
                            Area += Convert.ToDouble(A[a][0]);
                        }
                    }
                    __data[61].Add(new { idx = i, val = ((연간소요량_전 - 연간소요량_후) ).ToString("#,##0") });
                    __data[62].Add(new { idx = i, val = ((연간소요량_전 - 연간소요량_후) / Area).ToString("0.0") });
                    __data[63].Add(new { idx = i, val = tCO2.ToString("0.0") });
                    __data[64].Add(new { idx = i, val = TOE.ToString("0.0") });
                    __data[65].Add(new { idx = i, val = ((연간소요량_전 - 연간소요량_후) / 연간소요량_전 * 100).ToString("0") + " %" });
                    __data[66].Add(new { idx = i, val = (연간소요량_전 ).ToString("#,##0") });
                    __data[67].Add(new { idx = i, val = (연간소요량_후 ).ToString("#,##0") });
                    __data[68].Add(new { idx = i, val = (연간소요량_전 / Area).ToString("0.0") });
                    __data[69].Add(new { idx = i, val = (연간소요량_후 / Area).ToString("0.0") });
                    __data[70].Add(new { idx = i, val = (전_1차 ).ToString("0") });
                    __data[71].Add(new { idx = i, val = (후_1차 ).ToString("0") });
                    __data[72].Add(new { idx = i, val = (전_1차 / Area).ToString("0.0") });
                    __data[73].Add(new { idx = i, val = (후_1차 / Area).ToString("0.0") });
                    //double m = (연간소요량_전 - 연간소요량_후) / 연간소요량_전 * 100;
                    //charts += "{donut:" + m + ",size:120,fontSize:'21px'},"; // size를 100으로 설정
                   
                    ////////////////////////////////////////////////////////////////////
                    data.Add(new { cname = "qh_mth", data = __data[54] });
                    data.Add(new { cname = "qc_mth", data = __data[55] });
                    data.Add(new { cname = "qw_mth", data = __data[56] });
                    data.Add(new { cname = "ql_mth", data = __data[57] });
                    data.Add(new { cname = "qv_mth", data = __data[58] });
                    data.Add(new { cname = "qreg_mth", data = __data[59] });
                    data.Add(new { cname = "qf_mth", data = __data[60] });
                    data.Add(new { cname = "qfa", data = __data[61] });
                    data.Add(new { cname = "qfa_area", data = __data[62] });
                    data.Add(new { cname = "tco2", data = __data[63] });
                    data.Add(new { cname = "toe", data = __data[64] });
                    data.Add(new { cname = "savingpercent", data = __data[65] });
                    data.Add(new { cname = "qfa_pre", data = __data[66] });
                    data.Add(new { cname = "qfa_post", data = __data[67] });
                    data.Add(new { cname = "qfa_area_pre", data = __data[68] });
                    data.Add(new { cname = "qfa_area_post", data = __data[69] });
                    data.Add(new { cname = "qpa_pre", data = __data[70] });
                    data.Add(new { cname = "qpa_post", data = __data[71] });
                    data.Add(new { cname = "qpa_area_pre", data = __data[72] });
                    data.Add(new { cname = "qpa_area_post", data = __data[73] });
                    List<object> 난방소요량chart = new List<object>();
                    List<object> 냉방소요량chart = new List<object>();
                    List<object> 급탕소요량chart = new List<object>();
                    List<object> 조명소요량chart = new List<object>();
                    List<object> 공조소요량chart = new List<object>();
                    List<object> 기저소요량chart = new List<object>();
                    for (int mth = 0; mth < 12; mth++)
                    {
                        난방소요량chart.Add(Math.Round(double.Parse(Program.UTIL.asFixed(난방_후[mth].ToString())), 3) + 0);
                        냉방소요량chart.Add(Math.Round(double.Parse(Program.UTIL.asFixed(냉방_후[mth].ToString())), 3) + 0);
                        급탕소요량chart.Add(Math.Round(double.Parse(Program.UTIL.asFixed(급탕_후[mth].ToString())), 3) + 0);
                        조명소요량chart.Add(Math.Round(double.Parse(Program.UTIL.asFixed(조명_후[mth].ToString())), 3) + 0);
                        공조소요량chart.Add(Math.Round(double.Parse(Program.UTIL.asFixed(공조_후[mth].ToString())), 3) + 0);
                    }
                    chart_난방소요량.Add(System.Text.Json.JsonSerializer.Serialize(난방소요량chart.ToArray()));
                    chart_냉방소요량.Add(System.Text.Json.JsonSerializer.Serialize(냉방소요량chart.ToArray()));
                    chart_급탕소요량.Add(System.Text.Json.JsonSerializer.Serialize(급탕소요량chart.ToArray()));
                    chart_조명소요량.Add(System.Text.Json.JsonSerializer.Serialize(조명소요량chart.ToArray()));
                    chart_공조소요량.Add(System.Text.Json.JsonSerializer.Serialize(공조소요량chart.ToArray()));
                    chart_공조소요량.Add(System.Text.Json.JsonSerializer.Serialize(공조소요량chart.ToArray()));
                    #endregion
                    #region 보일러정보   
                    //리모델링 후 
                    double boiler_count_post = 0; double boiler_power_post = 0; double boiler_eta_post = 0; double boiler_eta_rule = 0; double boiler_point = 0;
                    string[][] Value1 = Program.DB.querySQL(DB.type.ProjDB, "Select b.보일러종류,a.명칭,b.존,a.용량,a.전부하효율,a.난방급탕,b.번호,b.보일러대수 From User_Boiler as a Inner Join HeatingSystem_Form as b ON a.번호 = b.보일러종류 Where a.난방급탕='난방+급탕'");
                    if (Value1.Length > 0)
                    {
                        for (int a = 0; a < Value1.Length; a++)
                        {
                            boiler_count_post += Convert.ToDouble(Value1[a][7]);
                            boiler_power_post += Convert.ToDouble(Value1[a][3]) * Convert.ToDouble(Value1[a][7]);
                            boiler_eta_post += Convert.ToDouble(Value1[a][4]) * Convert.ToDouble(Value1[a][3]) * Convert.ToDouble(Value1[a][7]);
                        }
                    }
                    string[][] Value2 = Program.DB.querySQL(DB.type.ProjDB, "Select b.보일러종류,a.명칭,b.존,a.용량,a.전부하효율,a.난방급탕,b.번호,b.보일러대수 From User_Boiler as a Inner Join HeatingSystem_Form as b ON a.번호 = b.보일러종류 Where a.난방급탕='난방'");
                    if (Value2.Length > 0)
                    {
                        for (int a = 0; a < Value2.Length; a++)
                        {
                            boiler_count_post += Convert.ToDouble(Value2[a][7]);
                            boiler_power_post += Convert.ToDouble(Value2[a][3]) * Convert.ToDouble(Value2[a][7]);
                            boiler_eta_post += Convert.ToDouble(Value2[a][4]) * Convert.ToDouble(Value2[a][3]) * Convert.ToDouble(Value2[a][7]);
                        }
                    }
                    string[][] Value3 = Program.DB.querySQL(DB.type.ProjDB, "Select b.보일러종류,a.명칭,b.존,a.용량,a.전부하효율,a.난방급탕,b.번호,b.보일러대수 From User_Boiler as a Inner Join DHWSystem_Form as b ON a.번호 = b.보일러종류 Where a.난방급탕='급탕'");
                    if (Value3.Length > 0)
                    {
                        for (int a = 0; a < Value3.Length; a++)
                        {
                            boiler_count_post += Convert.ToDouble(Value3[a][7]);
                            boiler_power_post += Convert.ToDouble(Value3[a][3])* Convert.ToDouble(Value3[a][7]);
                            boiler_eta_post += Convert.ToDouble(Value3[a][4]) * Convert.ToDouble(Value3[a][3])* Convert.ToDouble(Value3[a][7]);
                        }
                    }
                    if (boiler_count_post > 0)
                    {
                        boiler_eta_post = boiler_eta_post / boiler_power_post;
                        boiler_eta_rule = 90;
                        boiler_point = Math.Min(100, boiler_eta_post / boiler_eta_rule * 100);
                        __data[74].Add(new { idx = i, val = boiler_count_post.ToString("0") }); //보일러 개수 
                        __data[75].Add(new { idx = i, val = boiler_power_post.ToString("0.0") }); //보일러 용량
                        __data[76].Add(new { idx = i, val = boiler_eta_post.ToString("0.0") + " %" }); //보일러 효율
                        __data[77].Add(new { idx = i, val = boiler_eta_rule.ToString("0.0") + " %" }); //보일러 권장 효율 
                        d = boiler_point;
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        __data[78].Add(new { idx = i, val = sp }); //성능점수       
                    }
                    else
                    {
                        __data[74].Add(new { idx = i, val = "-" }); //보일러 개수 
                        __data[75].Add(new { idx = i, val = "-" }); //보일러 용량
                        __data[76].Add(new { idx = i, val = "-" }); //보일러 효율
                        __data[77].Add(new { idx = i, val = "-" }); //보일러 권장 효율    
                        __data[78].Add(new { idx = i, val = "-" }); //성능점수       
                    }
                    data.Add(new { cname = "boiler_count_post", data = __data[74] });
                    data.Add(new { cname = "boiler_power_post", data = __data[75] });
                    data.Add(new { cname = "boiler_eta_post", data = __data[76] });
                    data.Add(new { cname = "boiler_eta_rule", data = __data[77] });
                    data.Add(new { cname = "boiler_point", data = __data[78] });
                    //리모델링 전
                    double boiler_count_pre = 0; double boiler_power_pre = 0; double boiler_eta_pre = 0; 
                    Value1 = Program.DB.querySQL(res[0][0], "Select b.보일러종류,a.명칭,b.존,a.용량,a.전부하효율,a.난방급탕,b.번호,b.보일러대수 From User_Boiler as a Inner Join HeatingSystem_Form as b ON a.번호 = b.보일러종류 Where a.난방급탕='난방+급탕'");
                    if (Value1.Length > 0)
                    {
                        boiler_count_pre = Value1.Length;
                        for (int a = 0; a < Value1.Length; a++)
                        {
                            boiler_count_pre += Convert.ToDouble(Value1[a][7]);
                            boiler_power_pre += Convert.ToDouble(Value1[a][3]) * Convert.ToDouble(Value1[a][7]);
                            boiler_eta_pre += Convert.ToDouble(Value1[a][4]) * Convert.ToDouble(Value1[a][3]) * Convert.ToDouble(Value1[a][7]);
                        }
                    }
                    Value2 = Program.DB.querySQL(res[0][0], "Select b.보일러종류,a.명칭,b.존,a.용량,a.전부하효율,a.난방급탕,b.번호,b.보일러대수 From User_Boiler as a Inner Join HeatingSystem_Form as b ON a.번호 = b.보일러종류 Where a.난방급탕='난방'");
                    if (Value2.Length > 0)
                    {
                        for (int a = 0; a < Value2.Length; a++)
                        {
                            boiler_count_pre += Convert.ToDouble(Value2[a][7]);
                            boiler_power_pre += Convert.ToDouble(Value2[a][3])* Convert.ToDouble(Value2[a][7]);
                            boiler_eta_pre += Convert.ToDouble(Value2[a][4]) * Convert.ToDouble(Value2[a][3])* Convert.ToDouble(Value2[a][7]);
                        }
                    }
                    Value3 = Program.DB.querySQL(res[0][0], "Select b.보일러종류,a.명칭,b.존,a.용량,a.전부하효율,a.난방급탕,b.번호,b.보일러대수 From User_Boiler as a Inner Join DHWSystem_Form as b ON a.번호 = b.보일러종류 Where a.난방급탕='급탕'");
                    if (Value3.Length > 0)
                    {
                        for (int a = 0; a < Value3.Length; a++)
                        {
                            boiler_count_pre += Convert.ToDouble(Value3[a][7]);
                            boiler_power_pre += Convert.ToDouble(Value3[a][3])* Convert.ToDouble(Value3[a][7]);
                            boiler_eta_pre += Convert.ToDouble(Value3[a][4]) * Convert.ToDouble(Value3[a][3])* Convert.ToDouble(Value3[a][7]);
                        }
                    }
                    if (boiler_count_pre > 0)
                    {
                        boiler_eta_pre = boiler_eta_pre / boiler_power_pre;
                        __data[79].Add(new { idx = i, val = boiler_count_pre.ToString("0") }); //보일러 개수 
                        __data[80].Add(new { idx = i, val = boiler_power_pre.ToString("0.0") }); //보일러 용량
                        __data[81].Add(new { idx = i, val = boiler_eta_pre.ToString("0.0") + " %" }); //보일러 효율
                    }
                    else
                    {
                        __data[79].Add(new { idx = i, val = "-" }); //보일러 개수 
                        __data[80].Add(new { idx = i, val = "-" }); //보일러 용량
                        __data[81].Add(new { idx = i, val = "-" }); //보일러 효율
                    }
                    data.Add(new { cname = "boiler_count_pre", data = __data[79] });
                    data.Add(new { cname = "boiler_power_pre", data = __data[80] });
                    data.Add(new { cname = "boiler_eta_pre", data = __data[81] });
                    #endregion
                    #region 냉난방EHP정보   
                    //리모델링 후
                    double ehp_count_post = 0; double ehp_power_post = 0; double ehp_cop_post = 0; double ehp_cop_rule = 0; double ehp_point = 0;
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.외기히트펌프번호,a.명칭,b.존,a.난방정격용량,a.난방정격COP,a.냉방정격용량,a.냉방정격COP,b.번호,b.외기히트펌프대수 From User_AirHP as a Inner Join HeatingSystem_Form as b ON a.번호 = b.외기히트펌프번호 Where a.난방냉방='냉난방' And a.연료='전기'");
                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            ehp_count_post += Convert.ToDouble(Value[a][8]);
                            ehp_power_post += Convert.ToDouble(Value[a][3])* Convert.ToDouble(Value[a][8]);
                            ehp_cop_post += Convert.ToDouble(Value[a][4]) * Convert.ToDouble(Value[a][3])* Convert.ToDouble(Value[a][8]);
                        }
                        ehp_cop_post = ehp_cop_post / ehp_power_post;
                        ehp_cop_rule = 5.5;
                        ehp_point = Math.Min(100, ehp_cop_post / ehp_cop_rule * 100);
                        __data[82].Add(new { idx = i, val = ehp_count_post.ToString("0") }); //개수 
                        __data[83].Add(new { idx = i, val = ehp_power_post.ToString("0.0") }); //용량
                        __data[84].Add(new { idx = i, val = ehp_cop_post.ToString("0.0")+" W/W" }); //성능
                        __data[85].Add(new { idx = i, val = ehp_cop_rule.ToString("0.0") }); //권장 성능                
                        d = ehp_point;
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        __data[86].Add(new { idx = i, val = sp }); //성능점수       

                    }
                    else
                    {
                        __data[82].Add(new { idx = i, val = "-" }); //개수 
                        __data[83].Add(new { idx = i, val = "-" }); //용량
                        __data[84].Add(new { idx = i, val = "-" }); //성능
                        __data[85].Add(new { idx = i, val = "-" }); //권장 성능  
                        __data[86].Add(new { idx = i, val = "-" }); //성능점수       
                    }
                    data.Add(new { cname = "ehp_count_post", data = __data[82] });
                    data.Add(new { cname = "ehp_power_post", data = __data[83] });
                    data.Add(new { cname = "ehp_cop_post", data = __data[84] });
                    data.Add(new { cname = "ehp_cop_rule", data = __data[85] });
                    data.Add(new { cname = "ehp_point", data = __data[86] });
                    //리모델링 전
                    double ehp_count_pre = 0; double ehp_power_pre = 0; double ehp_cop_pre = 0; 
                    Value = Program.DB.querySQL(res[0][0], "Select b.외기히트펌프번호,a.명칭,b.존,a.난방정격용량,a.난방정격COP,a.냉방정격용량,a.냉방정격COP,b.번호,b.외기히트펌프대수 From User_AirHP as a Inner Join HeatingSystem_Form as b ON a.번호 = b.외기히트펌프번호 Where a.난방냉방='냉난방' And a.연료='전기'");
                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            ehp_count_pre += Convert.ToDouble(Value[a][8]);
                            ehp_power_pre += Convert.ToDouble(Value[a][3])* Convert.ToDouble(Value[a][8]);
                            ehp_cop_pre += Convert.ToDouble(Value[a][4]) * Convert.ToDouble(Value[a][3])* Convert.ToDouble(Value[a][8]);
                        }
                        ehp_cop_pre = ehp_cop_pre / ehp_power_pre;
                        __data[87].Add(new { idx = i, val = ehp_count_pre.ToString("0") }); //개수 
                        __data[88].Add(new { idx = i, val = ehp_power_pre.ToString("0.0") }); //용량
                        __data[89].Add(new { idx = i, val = ehp_cop_pre.ToString("0.0")+" W/W" }); //성능       
                    }
                    else
                    {
                        __data[87].Add(new { idx = i, val = "-" }); //개수 
                        __data[88].Add(new { idx = i, val = "-" }); //용량
                        __data[89].Add(new { idx = i, val = "-" }); //성능    
                    }
                    data.Add(new { cname = "ehp_count_pre", data = __data[87] });
                    data.Add(new { cname = "ehp_power_pre", data = __data[88] });
                    data.Add(new { cname = "ehp_cop_pre", data = __data[89] });
                    #endregion
                    #region 냉난방GHP정보   
                    //리모델링후
                    double ghp_count_post = 0; double ghp_power_post = 0; double ghp_cop_post = 0; double ghp_cop_rule = 0; double ghp_point = 0;
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.외기히트펌프번호,a.명칭,b.존,a.난방정격용량,a.난방정격COP,a.냉방정격용량,a.냉방정격COP,b.번호,b.외기히트펌프대수  From User_AirHP as a Inner Join HeatingSystem_Form as b ON a.번호 = b.외기히트펌프번호 Where a.난방냉방='냉난방' And NOT a.연료='전기'");
                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            ghp_count_post += Convert.ToDouble(Value[a][8]);
                            ghp_power_post += Convert.ToDouble(Value[a][3]) * Convert.ToDouble(Value[a][8]);
                            ghp_cop_post += Convert.ToDouble(Value[a][4]) * Convert.ToDouble(Value[a][3]) * Convert.ToDouble(Value[a][8]);
                        }
                        ghp_cop_post = ghp_cop_post / ghp_power_post;
                        ghp_cop_rule = 5.5;
                        ghp_point = Math.Min(100, ghp_cop_post / ghp_cop_rule * 100);
                        __data[90].Add(new { idx = i, val = ghp_count_post.ToString("0") }); //개수 
                        __data[91].Add(new { idx = i, val = ghp_power_post.ToString("0.0") }); //용량
                        __data[92].Add(new { idx = i, val = ghp_cop_post.ToString("0.0") + " W/W" }); //성능
                        __data[93].Add(new { idx = i, val = ghp_cop_rule.ToString("0.0") }); //권장 성능                
                        d = ghp_point;
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        __data[94].Add(new { idx = i, val = sp }); //성능점수
                    }
                    else
                    {
                        __data[90].Add(new { idx = i, val = "-" }); //개수 
                        __data[91].Add(new { idx = i, val = "-" }); //용량
                        __data[92].Add(new { idx = i, val = "-" }); //성능
                        __data[93].Add(new { idx = i, val = "-" }); //권장 성능      
                        __data[94].Add(new { idx = i, val = "-" }); //성능점수       
                    }
                    data.Add(new { cname = "ghp_count_post", data = __data[90] });
                    data.Add(new { cname = "ghp_power_post", data = __data[91] });
                    data.Add(new { cname = "ghp_cop_post", data = __data[92] });
                    data.Add(new { cname = "ghp_cop_rule", data = __data[93] });
                    data.Add(new { cname = "ghp_point", data = __data[94] });
                    //리모델링전
                    double ghp_count_pre = 0; double ghp_power_pre = 0; double ghp_cop_pre = 0; 
                    Value = Program.DB.querySQL(res[0][0], "Select b.외기히트펌프번호,a.명칭,b.존,a.난방정격용량,a.난방정격COP,a.냉방정격용량,a.냉방정격COP,b.번호,b.외기히트펌프대수  From User_AirHP as a Inner Join HeatingSystem_Form as b ON a.번호 = b.외기히트펌프번호 Where a.난방냉방='냉난방' And NOT a.연료='전기'");
                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            ghp_count_pre += Convert.ToDouble(Value[a][8]);
                            ghp_power_pre += Convert.ToDouble(Value[a][3])* Convert.ToDouble(Value[a][8]);
                            ghp_cop_pre += Convert.ToDouble(Value[a][4]) * Convert.ToDouble(Value[a][3])* Convert.ToDouble(Value[a][8]);
                        }
                        ghp_cop_pre = ghp_cop_pre / ghp_power_pre;
                        __data[95].Add(new { idx = i, val = ghp_count_pre.ToString("0") }); //개수 
                        __data[96].Add(new { idx = i, val = ghp_power_pre.ToString("0.0") }); //용량         
                        __data[97].Add(new { idx = i, val = ghp_cop_pre.ToString("0.0") + " W/W" }); //성능        
                    }
                    else
                    {
                        __data[95].Add(new { idx = i, val = "-" }); //개수 
                        __data[96].Add(new { idx = i, val = "-" }); //용량
                        __data[97].Add(new { idx = i, val = "-" }); //성능
                    }
                    data.Add(new { cname = "ghp_count_pre", data = __data[95] });
                    data.Add(new { cname = "ghp_power_pre", data = __data[96] });
                    data.Add(new { cname = "ghp_cop_pre", data = __data[97] });
                    #endregion
                    #region 흡수식냉온수기정보 
                    //리모델링후
                    double abs_count_post = 0; double abs_power_post = 0; double abs_cop_post = 0; double abs_cop_rule = 0; double abs_point = 0;
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.흡수식온수기번호,a.명칭,b.존,a.난방용량,a.난방성능,a.냉방용량,a.냉방성능,b.번호,b.흡수식온수기대수 From User_ABS as a Inner Join HeatingSystem_Form as b ON a.번호 = b.흡수식온수기번호 Where a.난방냉방='냉난방'");
                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            abs_count_post += Convert.ToDouble(Value[a][8]);
                            abs_power_post += Convert.ToDouble(Value[a][3]) * Convert.ToDouble(Value[a][8]);
                            abs_cop_post += Convert.ToDouble(Value[a][4]) * Convert.ToDouble(Value[a][3]) * Convert.ToDouble(Value[a][8]);
                        }
                        abs_cop_post = abs_cop_post / abs_power_post;
                        abs_cop_rule = 5.5;
                        abs_point = Math.Min(100, abs_cop_post / abs_cop_rule * 100);
                        __data[98].Add(new { idx = i, val = abs_count_post.ToString("0") }); //개수 
                        __data[99].Add(new { idx = i, val = abs_power_post.ToString("0.0") }); //용량
                        __data[100].Add(new { idx = i, val = abs_cop_post.ToString("0.0") + " W/W" }); //성능
                        __data[101].Add(new { idx = i, val = abs_cop_rule.ToString("0.0") }); //권장 성능                
                        d = abs_point;
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        __data[102].Add(new { idx = i, val = sp }); //성능점수                         
                    }
                    else
                    {
                        __data[98].Add(new { idx = i, val = "-" }); //개수 
                        __data[99].Add(new { idx = i, val = "-" }); //용량
                        __data[100].Add(new { idx = i, val = "-" }); //성능
                        __data[101].Add(new { idx = i, val = "-" }); //권장 성능      
                        __data[102].Add(new { idx = i, val = "-" }); //성능점수                 
                    }
                    data.Add(new { cname = "abs_count_post", data = __data[98] });
                    data.Add(new { cname = "abs_power_post", data = __data[99] });
                    data.Add(new { cname = "abs_cop_post", data = __data[100] });
                    data.Add(new { cname = "abs_cop_rule", data = __data[101] });
                    data.Add(new { cname = "abs_point", data = __data[102] });
                    //리모델링전
                    double abs_count_pre = 0; double abs_power_pre = 0; double abs_cop_pre = 0;
                    Value = Program.DB.querySQL(res[0][0], "Select b.흡수식온수기번호,a.명칭,b.존,a.난방용량,a.난방성능,a.냉방용량,a.냉방성능,b.번호,b.흡수식온수기대수 From User_ABS as a Inner Join HeatingSystem_Form as b ON a.번호 = b.흡수식온수기번호 Where a.난방냉방='냉난방'");
                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            abs_count_pre += Convert.ToDouble(Value[a][8]);
                            abs_power_pre += Convert.ToDouble(Value[a][3])* Convert.ToDouble(Value[a][8]);
                            abs_cop_pre += Convert.ToDouble(Value[a][4]) * Convert.ToDouble(Value[a][3])* Convert.ToDouble(Value[a][8]);
                        }
                        abs_cop_pre = abs_cop_pre / abs_power_pre;
                        __data[103].Add(new { idx = i, val = abs_count_pre.ToString("0") }); //개수 
                        __data[104].Add(new { idx = i, val = abs_power_pre.ToString("0.0") }); //용량
                        __data[105].Add(new { idx = i, val = abs_cop_pre.ToString("0.0") + " W/W" }); //성능    
                    }
                    else
                    {
                        __data[103].Add(new { idx = i, val = "-" }); //개수 
                        __data[104].Add(new { idx = i, val = "-" }); //용량
                        __data[105].Add(new { idx = i, val = "-" }); //성능              
                    }
                    data.Add(new { cname = "abs_count_pre", data = __data[103] });
                    data.Add(new { cname = "abs_power_pre", data = __data[104] });
                    data.Add(new { cname = "abs_cop_pre", data = __data[105] });
                    #endregion
                    #region 에어컨정보   
                    //리모델링후
                    double airc_count_post = 0; double airc_power_post = 0; double airc_cop_post = 0; double airc_cop_rule = 0; double airc_point = 0;
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select b.냉방유닛,a.명칭,b.공급존,a.난방정격용량,a.난방정격COP,a.냉방정격용량,a.냉방정격COP,b.번호,b.설치대수 From User_AirHP as a Inner Join CoolingSystem_Form as b ON a.번호 = b.냉방유닛 Where a.난방냉방='냉방' And a.연료='전기'");
                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            airc_count_post += Convert.ToDouble(Value[a][8]);
                            airc_power_post += Convert.ToDouble(Value[a][5])* Convert.ToDouble(Value[a][8]);
                            airc_cop_post += Convert.ToDouble(Value[a][6]) * Convert.ToDouble(Value[a][5])* Convert.ToDouble(Value[a][8]);
                        }
                        airc_cop_post = airc_cop_post / airc_power_post;
                        airc_cop_rule = 5.5;
                        airc_point = Math.Min(100, airc_cop_post / airc_cop_rule * 100);
                        __data[106].Add(new { idx = i, val = airc_count_post.ToString("0") }); //개수 
                        __data[107].Add(new { idx = i, val = airc_power_post.ToString("0.0") }); //용량
                        __data[108].Add(new { idx = i, val = airc_cop_post.ToString("0.0") + " W/W" }); //성능
                        __data[109].Add(new { idx = i, val = airc_cop_rule.ToString("0.0") }); //권장 성능                
                        d = airc_point;
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        __data[110].Add(new { idx = i, val = sp }); //성능점수    
                    }
                    else
                    {
                        __data[106].Add(new { idx = i, val = "-" }); //개수 
                        __data[107].Add(new { idx = i, val = "-" }); //용량
                        __data[108].Add(new { idx = i, val = "-" }); //성능
                        __data[109].Add(new { idx = i, val = "-" }); //권장 성능       
                        __data[110].Add(new { idx = i, val = "-" }); //성능점수             
                    }
                    data.Add(new { cname = "airc_count_post", data = __data[106] });
                    data.Add(new { cname = "airc_power_post", data = __data[107] });
                    data.Add(new { cname = "airc_cop_post", data = __data[108] });
                    data.Add(new { cname = "airc_cop_rule", data = __data[109] });
                    data.Add(new { cname = "airc_point", data = __data[110] });
                    //리모델링전
                    double airc_count_pre = 0; double airc_power_pre = 0; double airc_cop_pre = 0;
                    Value = Program.DB.querySQL(res[0][0], "Select b.냉방유닛,a.명칭,b.공급존,a.난방정격용량,a.난방정격COP,a.냉방정격용량,a.냉방정격COP,b.번호,b.설치대수 From User_AirHP as a Inner Join CoolingSystem_Form as b ON a.번호 = b.냉방유닛 Where a.난방냉방='냉방' And a.연료='전기'");
                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            airc_count_pre += Convert.ToDouble(Value[a][8]);
                            airc_power_pre += Convert.ToDouble(Value[a][5])* Convert.ToDouble(Value[a][8]);
                            airc_cop_pre += Convert.ToDouble(Value[a][6]) * Convert.ToDouble(Value[a][5])* Convert.ToDouble(Value[a][8]);
                        }
                        airc_cop_pre = airc_cop_pre / airc_power_pre;
                        __data[111].Add(new { idx = i, val = airc_count_pre.ToString("0") }); //개수 
                        __data[112].Add(new { idx = i, val = airc_power_pre.ToString("0.0") }); //용량
                        __data[113].Add(new { idx = i, val = airc_cop_pre.ToString("0.0") + " W/W" }); //성능
                    }
                    else
                    {
                        __data[111].Add(new { idx = i, val = "-" }); //개수 
                        __data[112].Add(new { idx = i, val = "-" }); //용량
                        __data[113].Add(new { idx = i, val = "-" }); //성능           
                    }
                    data.Add(new { cname = "airc_count_pre", data = __data[111] });
                    data.Add(new { cname = "airc_power_pre", data = __data[112] });
                    data.Add(new { cname = "airc_cop_pre", data = __data[113] });
                    #endregion
                    #region 냉동기정보   
                    //리모델링후
                    double chiler_count_post = 0; double chiler_power_post = 0; double chiler_cop_post = 0; double chiler_cop_rule = 0; double chiler_point = 0;
                    string[][] Value11 = Program.DB.querySQL(DB.type.ProjDB, "Select b.냉방유닛,a.명칭,b.공급존,a.냉방출력,a.EER,b.번호,b.설치대수 From User_AirCooler as a Inner Join CoolingSystem_Form as b ON a.번호 = b.냉방유닛");
                    if (Value1.Length > 0)
                    {
                        for (int a = 0; a < Value11.Length; a++)
                        {
                            chiler_count_post += Convert.ToDouble(Value11[a][6]);
                            chiler_power_post += Convert.ToDouble(Value11[a][3])* Convert.ToDouble(Value11[a][6]);
                            chiler_cop_post += Convert.ToDouble(Value11[a][4]) * Convert.ToDouble(Value11[a][3])* Convert.ToDouble(Value11[a][6]);
                        }
                    }
                    string[][] Value12 = Program.DB.querySQL(DB.type.ProjDB, "Select b.냉방유닛,a.명칭,b.공급존,a.냉방출력,a.EER,b.번호,b.설치대수 From User_WaterCooler as a Inner Join CoolingSystem_Form as b ON a.번호 = b.냉방유닛");
                    if (Value2.Length > 0)
                    {
                        for (int a = 0; a < Value12.Length; a++)
                        {
                            chiler_count_post += Convert.ToDouble(Value12[a][6]);
                            chiler_power_post += Convert.ToDouble(Value12[a][3])* Convert.ToDouble(Value12[a][6]);
                            chiler_cop_post += Convert.ToDouble(Value12[a][4]) * Convert.ToDouble(Value12[a][3])* Convert.ToDouble(Value12[a][6]);
                        }
                    }
                    if (Value11.Length > 0 || Value12.Length > 0)
                    {
                        chiler_cop_post = chiler_cop_post / chiler_power_post;
                        chiler_cop_rule = 5.5;
                        chiler_point = Math.Min(100, chiler_cop_post / chiler_cop_rule * 100);
                        __data[114].Add(new { idx = i, val = chiler_count_post.ToString("0") }); //개수 
                        __data[115].Add(new { idx = i, val = chiler_power_post.ToString("0.0") }); //용량
                        __data[116].Add(new { idx = i, val = chiler_cop_post.ToString("0.0") + " W/W" }); //성능
                        __data[117].Add(new { idx = i, val = chiler_cop_rule.ToString("0.0") }); //권장 성능                
                        d = chiler_point;
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        __data[118].Add(new { idx = i, val = sp }); //성능점수    
                    }
                    else
                    {
                        __data[114].Add(new { idx = i, val = "-" }); //개수 
                        __data[115].Add(new { idx = i, val = "-" }); //용량
                        __data[116].Add(new { idx = i, val = "-" }); //성능
                        __data[117].Add(new { idx = i, val = "-" }); //권장 성능 
                        __data[118].Add(new { idx = i, val = "-" }); //성능점수    
                    }
                    data.Add(new { cname = "chiler_count_post", data = __data[114] });
                    data.Add(new { cname = "chiler_power_post", data = __data[115] });
                    data.Add(new { cname = "chiler_cop_post", data = __data[116] });
                    data.Add(new { cname = "chiler_cop_rule", data = __data[117] });
                    data.Add(new { cname = "chiler_point", data = __data[118] });
                    //리모델링전
                    double chiler_count_pre = 0; double chiler_power_pre = 0; double chiler_cop_pre = 0;
                    Value11 = Program.DB.querySQL(res[0][0], "Select b.냉방유닛,a.명칭,b.공급존,a.냉방출력,a.EER,b.번호,b.설치대수 From User_AirCooler as a Inner Join CoolingSystem_Form as b ON a.번호 = b.냉방유닛");
                    if (Value1.Length > 0)
                    {
                        for (int a = 0; a < Value11.Length; a++)
                        {
                            chiler_count_pre += Convert.ToDouble(Value11[a][6]);
                            chiler_power_pre += Convert.ToDouble(Value11[a][3]) * Convert.ToDouble(Value11[a][6]);
                            chiler_cop_pre += Convert.ToDouble(Value11[a][4]) * Convert.ToDouble(Value11[a][3]) * Convert.ToDouble(Value11[a][6]);
                        }
                    }
                    Value12 = Program.DB.querySQL(res[0][0], "Select b.냉방유닛,a.명칭,b.공급존,a.냉방출력,a.EER,b.번호,b.설치대수 From User_WaterCooler as a Inner Join CoolingSystem_Form as b ON a.번호 = b.냉방유닛");
                    if (Value2.Length > 0)
                    {
                        for (int a = 0; a < Value12.Length; a++)
                        {
                            chiler_count_pre += Convert.ToDouble(Value12[a][6]);
                            chiler_power_pre += Convert.ToDouble(Value12[a][3])* Convert.ToDouble(Value12[a][6]);
                            chiler_cop_pre += Convert.ToDouble(Value12[a][4]) * Convert.ToDouble(Value12[a][3])* Convert.ToDouble(Value12[a][6]);
                        }
                    }
                    if (Value11.Length > 0 || Value12.Length > 0)
                    {
                        chiler_cop_pre = chiler_cop_pre / chiler_power_pre;
                        __data[117].Add(new { idx = i, val = chiler_count_pre.ToString("0") }); //개수 
                        __data[118].Add(new { idx = i, val = chiler_power_pre.ToString("0.0") }); //용량
                        __data[119].Add(new { idx = i, val = chiler_cop_pre.ToString("0.0") + " W/W" }); //성능
                    }
                    else
                    {
                        __data[117].Add(new { idx = i, val = "-" }); //개수 
                        __data[118].Add(new { idx = i, val = "-" }); //용량
                        __data[119].Add(new { idx = i, val = "-" }); //성능
                    }
                    data.Add(new { cname = "chiler_count_pre", data = __data[117] });
                    data.Add(new { cname = "chiler_power_pre", data = __data[118] });
                    data.Add(new { cname = "chiler_cop_pre", data = __data[119] });


                    #endregion
                    #region 조명정보   
                    //리모델링후
                    string light_count_post; double light_density_post = 0; double light_eta_post = 0; double light_eta_rule = 0; double light_point = 0; double light_area_post = 0;
                    Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneLighting_form", "조명번호");
                    if (Value.Length > 0)
                    {
                        light_count_post = "-";
                        for (int a = 0; a < Value.Length; a++)
                        {
                            string[][] 조명존 = Program.DB.querySQL(DB.type.ProjDB, "Select a.번호,a.순바닥면적,a.조명밀도,a.등기구명칭,a.광효율,a.자연채광유형,b.기존존 From ZoneLighting_form as a Inner Join ZoneGeneral_Form as b on a.번호 =b.존번호 where a.조명번호='" + Value[a][0] + "'");
                            if (조명존.Length > 0)
                            {
                                for (int aa = 0; aa < 조명존.Length; aa++)
                                {
                                    light_area_post = Convert.ToDouble(조명존[aa][1]);
                                    light_density_post = Convert.ToDouble(조명존[aa][1]) * Convert.ToDouble(조명존[aa][2]);
                                    light_eta_post = Convert.ToDouble(조명존[aa][1]) * Convert.ToDouble(조명존[aa][4]);
                                }
                            }
                        }
                        light_density_post = light_density_post / light_area_post;
                        light_eta_post = light_eta_post / light_area_post;
                        light_eta_rule = 70;
                        light_point = Math.Min(100, light_eta_post / light_eta_rule * 100);
                        __data[120].Add(new { idx = i, val = light_count_post }); //개수 
                        __data[121].Add(new { idx = i, val = light_density_post.ToString("0.0") + " W/m"+ Program.UTIL.Subscript(2, true) }); //용량
                        __data[122].Add(new { idx = i, val = light_eta_post.ToString("0") + " lm/W" }); //성능
                        __data[123].Add(new { idx = i, val = light_eta_rule.ToString("0") + " lm/W" }); //권장 성능                
                        d = light_point;
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        __data[124].Add(new { idx = i, val = sp }); //성능점수       
                        data.Add(new { cname = "light_count_post", data = __data[120] });
                        data.Add(new { cname = "light_power_post", data = __data[121] });
                        data.Add(new { cname = "light_eta_post", data = __data[122] });
                        data.Add(new { cname = "light_eta_rule", data = __data[123] });
                        data.Add(new { cname = "light_point", data = __data[124] });
                    }
                    //리모델링전
                    string light_count_pre; double light_density_pre = 0; double light_eta_pre = 0; double light_area_pre = 0;
                    Value = Program.DB.getValue_SameCheck(res[0][0], "ZoneLighting_form", "조명번호");
                    if (Value.Length > 0)
                    {
                        light_count_pre = "-";
                        for (int a = 0; a < Value.Length; a++)
                        {
                            string[][] 조명존 = Program.DB.querySQL(res[0][0], "Select a.번호,a.순바닥면적,a.조명밀도,a.등기구명칭,a.광효율,a.자연채광유형,b.기존존 From ZoneLighting_form as a Inner Join ZoneGeneral_Form as b on a.번호 =b.존번호 where a.조명번호='" + Value[a][0] + "'");
                            if (조명존.Length > 0)
                            {
                                for (int aa = 0; aa < 조명존.Length; aa++)
                                {
                                    light_area_pre = Convert.ToDouble(조명존[aa][1]);
                                    light_density_pre = Convert.ToDouble(조명존[aa][1]) * Convert.ToDouble(조명존[aa][2]);
                                    light_eta_pre = Convert.ToDouble(조명존[aa][1]) * Convert.ToDouble(조명존[aa][4]);
                                }
                            }
                        }
                        light_density_pre = light_density_pre / light_area_pre;
                        light_eta_pre = light_eta_pre / light_area_pre;
                        __data[125].Add(new { idx = i, val = light_count_pre }); //개수 
                        __data[126].Add(new { idx = i, val = light_density_pre.ToString("0.0") + " W/m"+ Program.UTIL.Subscript(2, true) }); //용량
                        __data[127].Add(new { idx = i, val = light_eta_pre.ToString("0") + " lm/W" }); //성능
                        data.Add(new { cname = "light_count_pre", data = __data[125] });
                        data.Add(new { cname = "light_power_pre", data = __data[126] });
                        data.Add(new { cname = "light_eta_pre", data = __data[127] });
                    }
                    #endregion
                    #region 태양광정보   
                    double pv_count_post = 0; double pv_power_post = 0; double pv_eta_post = 0; string pv_eta_rule = "-"; double pv_point = 0;
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select a.번호,a.명칭,a.모듈번호,a.개수,a.개수,a.용량,a.면적,b.CELLTYPE,b.Kpk From PV_Form as a inner Join User_PV as b on a.모듈번호=b.번호");
                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            pv_count_post += Convert.ToDouble(Value[a][3]);
                            pv_power_post += Convert.ToDouble(Value[a][5]);
                            pv_eta_post += Convert.ToDouble(Value[a][8]) * Convert.ToDouble(Value[a][5]);
                        }
                        pv_eta_post = pv_eta_post / pv_power_post;
                        pv_point = 100;
                        __data[128].Add(new { idx = i, val = pv_count_post.ToString("0") }); //개수 
                        __data[129].Add(new { idx = i, val = pv_power_post.ToString("0.0") }); //용량
                        __data[130].Add(new { idx = i, val = pv_eta_post.ToString("0.00") + " kW/m"+ Program.UTIL.Subscript(2, true) }); //성능
                        __data[131].Add(new { idx = i, val = pv_eta_rule.ToString() }); //권장 성능                
                        d = pv_point;
                        if (d >= 100) { sp = "<div class='cls-sparkline-blue' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else if (d <= 30) { sp = "<div class='cls-sparkline-red' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }//성능 수준 중 가장 큰 값을을 100로 가정, 235는 픽셀 최대 크기
                        else { sp = "<div class='cls-sparkline' style='width:" + (int)((d * 175) / 100) + "px'></div>"; }
                        sp += "<div class='cls-sparkline-text'>" + d.ToString("0") + " 점</div>";
                        __data[132].Add(new { idx = i, val = sp }); //성능점수    
                    }
                    else
                    {
                        __data[128].Add(new { idx = i, val = "-" }); //개수 
                        __data[129].Add(new { idx = i, val = "-" }); //용량
                        __data[130].Add(new { idx = i, val = "-" }); //성능
                        __data[131].Add(new { idx = i, val = "-" }); //권장 성능       
                        __data[132].Add(new { idx = i, val = "-" }); //성능점수             
                    }
                    data.Add(new { cname = "pv_count_post", data = __data[128] });
                    data.Add(new { cname = "pv_power_post", data = __data[129] });
                    data.Add(new { cname = "pv_eta_post", data = __data[130] });
                    data.Add(new { cname = "pv_eta_rule", data = __data[131] });
                    data.Add(new { cname = "pv_point", data = __data[132] });
                    //리모델링전
                    double pv_count_pre = 0; double pv_power_pre = 0; double pv_eta_pre = 0;
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select a.번호,a.명칭,a.모듈번호,a.개수,a.개수,a.용량,a.면적,b.CELLTYPE,b.Kpk From PV_Form as a inner Join User_PV as b on a.모듈번호=b.번호");
                    if (Value.Length > 0)
                    {
                        for (int a = 0; a < Value.Length; a++)
                        {
                            pv_count_pre += Convert.ToDouble(Value[a][3]);
                            pv_power_pre += Convert.ToDouble(Value[a][5]);
                            pv_eta_pre += Convert.ToDouble(Value[a][8]) * Convert.ToDouble(Value[a][5]);
                        }
                        pv_eta_pre = pv_eta_pre / pv_power_pre;
                        __data[133].Add(new { idx = i, val = pv_count_pre.ToString("0") }); //개수 
                        __data[134].Add(new { idx = i, val = pv_power_pre.ToString("0.0") }); //용량
                        __data[135].Add(new { idx = i, val = pv_eta_pre.ToString("0.00") + " kW/m"+ Program.UTIL.Subscript(2, true) }); //성능       
                    }
                    else
                    {
                        __data[133].Add(new { idx = i, val = "-" }); //개수 
                        __data[134].Add(new { idx = i, val = "-" }); //용량
                        __data[134].Add(new { idx = i, val = "-" }); //성능             
                    }
                    data.Add(new { cname = "pv_count_pre", data = __data[133] });
                    data.Add(new { cname = "pv_power_pre", data = __data[134] });
                    data.Add(new { cname = "pv_eta_pre", data = __data[135] });
                    #endregion

                    items.Add("MainReport_After.htm");
                    s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                    s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
                    System.Text.Json.JsonSerializer.Serialize(__data[10].ToArray());
                    double max = 0;
                    for (int mth = 0; mth < 12; mth++)
                    {
                        if (max < 총소요량_후[mth])
                        {
                            max = 총소요량_후[mth];
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
                    "],max:" + (Math.Round(max / 1000) * 1000 + 500).ToString() + ",step:100,legend:true,stacked:true}";
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