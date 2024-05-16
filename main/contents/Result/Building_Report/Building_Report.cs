using main.subcontents.CoolingSystem;
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
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트명,주소,지역,지역구분,준공시기,연면적,건축면적,지상층수,지하층수,작성자회사,작성자,작성시기");
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
                    __data[47].Add(new { idx = i, val = (RuleValue / Uvalue * 100).ToString("0") + " 점" }); //외벽 법규 열관류율                     
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
                    __data[48].Add(new { idx = i, val = (RuleValue / Uvalue * 100).ToString("0") + " 점" }); //지붕 법규 열관류율                   
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
                    __data[49].Add(new { idx = i, val = (RuleValue / Uvalue * 100).ToString("0") + " 점" }); //바닥 법규 열관류율                      
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
                    __data[50].Add(new { idx = i, val = (RuleValue / Uvalue * 100).ToString("0") + " 점" }); //창호 법규 열관류율                      
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
                __data[51].Add(new { idx = i, val = (RuleValue_CW / Uvalue_CW * 100).ToString("0") + " 점" }); //커튼월창 법규 열관류율     
                                                                                                              ////////////////////////////////////////////////////////////////////
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
                    __data[52].Add(new { idx = i, val = (RuleValue / Uvalue * 100).ToString("0") + " 점" }); //출입문 법규 열관류율                     
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
                        총전기[mth] = Convert.ToDouble(Final1[0][7]);
                    }
                    if (Final2.Length > 0)
                    {
                        난방[mth] = 난방[mth] + Convert.ToDouble(Final2[0][0]);
                        냉방[mth] = 냉방[mth] + Convert.ToDouble(Final2[0][1]);
                        급탕[mth] = 급탕[mth] + Convert.ToDouble(Final2[0][2]);
                        조명[mth] = 조명[mth] + Convert.ToDouble(Final2[0][3]);
                        공조[mth] = 공조[mth] + Convert.ToDouble(Final2[0][4]);
                        신재생[mth] = 신재생[mth] + Convert.ToDouble(Final2[0][6]);
                        총가스[mth] = Convert.ToDouble(Final2[0][7]);
                    }

                    총소요량[mth] = 총전기[mth] + 총가스[mth];
                }
                for (int mth = 0; mth < 12; mth++)
                {
                    연간전기 += 총전기[mth];
                    연간가스 += 총가스[mth];
                    연간소요량 += 총소요량[mth];
                    __data[36].Add(new { idx = i * 12 + mth, val = 난방[mth].ToString("0.0") });
                    __data[37].Add(new { idx = i * 12 + mth, val = 냉방[mth].ToString("0.0") });
                    __data[38].Add(new { idx = i * 12 + mth, val = 급탕[mth].ToString("0.0") });
                    __data[39].Add(new { idx = i * 12 + mth, val = 조명[mth].ToString("0.0") });
                    __data[40].Add(new { idx = i * 12 + mth, val = 공조[mth].ToString("0.0") });
                    __data[41].Add(new { idx = i * 12 + mth, val = 신재생[mth].ToString("0.0") });
                    __data[42].Add(new { idx = i * 12 + mth, val = 총소요량[mth].ToString("0.0") });
                }
                double tCO2 = 연간전기 * 0.4747 / 1000000 * 1000 + 연간가스 / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                double TOE = 연간전기 * 0.00023 + 연간가스 / 43.1 / 0.277778 * 0.00103;
                double Area = 0;
                string[][] A = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "냉난방유무 <> '비냉난방'");
                if (A.Length > 0)
                {
                    for (int a = 0; a < A.Length; a++)
                    {
                        Area += Convert.ToDouble(A[a][0]);
                    }
                }
                __data[43].Add(new { idx = i, val = 연간소요량.ToString("0.0") });
                __data[44].Add(new { idx = i, val = (연간소요량 / Area).ToString("0.0") });
                __data[45].Add(new { idx = i, val = tCO2.ToString("0.0") });
                __data[46].Add(new { idx = i, val = TOE.ToString("0.0") });
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
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트명,주소,지역,지역구분,준공시기,연면적,건축면적,지상층수,지하층수,작성자회사,작성자,작성시기");
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
                        __data[15].Add(new { idx = i, val = (RuleValue / Uvalue * 100).ToString("0") + " 점" }); //외벽 법규 열관류율                   
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
                        __data[22].Add(new { idx = i, val = (RuleValue / Uvalue * 100).ToString("0") + " 점" }); //지붕 법규 열관류율                      
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
                        __data[29].Add(new { idx = i, val = (RuleValue / Uvalue * 100).ToString("0") + " 점" }); //바닥 법규 열관류율                    
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
                        __data[36].Add(new { idx = i, val = (RuleValue / Uvalue * 100).ToString("0") + " 점" }); //창호 법규 열관류율                                     
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
                    __data[43].Add(new { idx = i, val = (RuleValue_CW_후 / Uvalue_CW_후 * 100).ToString("0") + " 점" }); //커튼월창 법규 열관류율                                                                                           
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
                        __data[50].Add(new { idx = i, val = (RuleValue_CW_후 / Uvalue_CW_후 * 100).ToString("0") + " 점" }); //출입문 법규 열관류율                        
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
                            총전기_후[mth] = Convert.ToDouble(Final1[0][7]);
                        }
                        if (Final2.Length > 0)
                        {
                            난방_후[mth] = 난방_후[mth] + Convert.ToDouble(Final2[0][0]);
                            냉방_후[mth] = 냉방_후[mth] + Convert.ToDouble(Final2[0][1]);
                            급탕_후[mth] = 급탕_후[mth] + Convert.ToDouble(Final2[0][2]);
                            조명_후[mth] = 조명_후[mth] + Convert.ToDouble(Final2[0][3]);
                            공조_후[mth] = 공조_후[mth] + Convert.ToDouble(Final2[0][4]);
                            신재생_후[mth] = 신재생_후[mth] + Convert.ToDouble(Final2[0][6]);
                            총가스_후[mth] = Convert.ToDouble(Final2[0][7]);
                        }

                        총소요량_후[mth] = 총전기_후[mth] + 총가스_후[mth];
                    }
                    for (int mth = 0; mth < 12; mth++)
                    {
                        연간전기_후 += 총전기_후[mth];
                        연간가스_후 += 총가스_후[mth];
                        연간소요량_후 += 총소요량_후[mth];
                        __data[54].Add(new { idx = i * 12 + mth, val = 난방_후[mth].ToString("0.0") });
                        __data[55].Add(new { idx = i * 12 + mth, val = 냉방_후[mth].ToString("0.0") });
                        __data[56].Add(new { idx = i * 12 + mth, val = 급탕_후[mth].ToString("0.0") });
                        __data[57].Add(new { idx = i * 12 + mth, val = 조명_후[mth].ToString("0.0") });
                        __data[58].Add(new { idx = i * 12 + mth, val = 공조_후[mth].ToString("0.0") });
                        __data[59].Add(new { idx = i * 12 + mth, val = 신재생_후[mth].ToString("0.0") });
                        __data[60].Add(new { idx = i * 12 + mth, val = 총소요량_후[mth].ToString("0.0") });
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
                            총전기_전[mth] = Convert.ToDouble(Final1[0][7]);
                        }
                        if (Final2.Length > 0)
                        {
                            난방_전[mth] = 난방_전[mth] + Convert.ToDouble(Final2[0][0]);
                            냉방_전[mth] = 냉방_전[mth] + Convert.ToDouble(Final2[0][1]);
                            급탕_전[mth] = 급탕_전[mth] + Convert.ToDouble(Final2[0][2]);
                            조명_전[mth] = 조명_전[mth] + Convert.ToDouble(Final2[0][3]);
                            공조_전[mth] = 공조_전[mth] + Convert.ToDouble(Final2[0][4]);
                            신재생_전[mth] = 신재생_전[mth] + Convert.ToDouble(Final2[0][6]);
                            총가스_전[mth] =  Convert.ToDouble(Final2[0][7]);
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
                    double Area = 0;
                    string[][] A = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "냉난방유무 <> '비냉난방'");
                    if (A.Length > 0)
                    {
                        for (int a = 0; a < A.Length; a++)
                        {
                            Area += Convert.ToDouble(A[a][0]);
                        }
                    }
                    __data[61].Add(new { idx = i, val = (연간소요량_전 - 연간소요량_후).ToString("0.0") });
                    __data[62].Add(new { idx = i, val = ((연간소요량_전 - 연간소요량_후) / Area).ToString("0.0") });
                    __data[63].Add(new { idx = i, val = tCO2.ToString("0.0") });
                    __data[64].Add(new { idx = i, val = TOE.ToString("0.0") });
                    __data[65].Add(new { idx = i, val = ((연간소요량_전 - 연간소요량_후) / 연간소요량_전 * 100).ToString("0") + " %" });

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