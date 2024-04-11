using Eagle._Components.Public;
using Eagle._Interfaces.Public;
using Microsoft.Office.Interop.Excel;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.contents
{
    public partial class Element_Report : Form
    {
        bool scriptable = false;
        public Element_Report()
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

            MainMenu.Add(new { text = "불투명구조체", id = "{\\\"formID\\\":26,\\\"ID\\\":\\\"Result_1\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            MainMenu.Add(new { text = "투명구조체", id = "{\\\"formID\\\":26,\\\"ID\\\":\\\"Result_2\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            Program.UTIL.resetMainTree(5, 1, MainMenu.ToArray(), "26"); // 예시 코드: 메인 메뉴 동적 할당
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

        }

        private void Report_After()
        {
            string s, s2;
            string[][] 번호 = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
            string[][] res = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기존프로젝트");
            List<object> items = new List<object>();
            List<object> items2 = new List<object>();
            List<object> data = new List<object>();
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
                    #region 조닝
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result_Alt", "총에너지소요량", "검토유형='조닝'");
                    double zoning_sum = 0;//조닝만 한 경우 
                    if (Value.Length > 0)
                    {
                        for (int k = 0; k < Value.Length; k++)
                        {
                            zoning_sum += Convert.ToDouble(Value[k][0]);
                        }
                    }
                    Value = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result_Alt", "총에너지소요량", "검토유형='조닝' AND 연료='전기'");
                    double zoning_sum_elec = 0;//조닝만 한 경우 전기 
                    if (Value.Length > 0)
                    {
                        for (int k = 0; k < Value.Length; k++)
                        {
                            zoning_sum_elec += Convert.ToDouble(Value[k][0]);
                        }
                    }
                    Value = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result_Alt", "총에너지소요량", "검토유형='조닝' AND NOT 연료='전기'");
                    double zoning_sum_noelec = 0;//조닝만 한 경우 가스
                    if (Value.Length > 0)
                    {
                        for (int k = 0; k < Value.Length; k++)
                        {
                            zoning_sum_noelec += Convert.ToDouble(Value[k][0]);
                        }
                    }
                    #endregion

                    #region 외벽
                    Value = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result_Alt", "총에너지소요량", "검토유형='요소기술_외벽'");
                    double wall_sum = 0;//조닝+외벽만 한 경우 
                    if (Value.Length > 0)
                    {
                        for (int k = 0; k < Value.Length; k++)
                        {
                            wall_sum += Convert.ToDouble(Value[k][0]);
                        }
                    }
                    Value = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result_Alt", "총에너지소요량", "검토유형='요소기술_외벽'AND 연료='전기'");
                    double wall_sum_elec = 0;//조닝+외벽만 한 경우 
                    if (Value.Length > 0)
                    {
                        for (int k = 0; k < Value.Length; k++)
                        {
                            wall_sum_elec += Convert.ToDouble(Value[k][0]);
                        }
                    }
                    Value = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result_Alt", "총에너지소요량", "검토유형='요소기술_외벽'AND not 연료='전기'");
                    double wall_sum_noelec = 0;//조닝+외벽만 한 경우 
                    if (Value.Length > 0)
                    {
                        for (int k = 0; k < Value.Length; k++)
                        {
                            wall_sum_noelec += Convert.ToDouble(Value[k][0]);
                        }
                    }
                    double wall_saving = zoning_sum - wall_sum;
                    __data[0].Add(new { idx = i, val = wall_saving.ToString("0.0") }); ; //절감량 
                    __data[1].Add(new { idx = i, val = (wall_saving / zoning_sum * 100).ToString("0.0") + "%" }); ; //절감률
                    data.Add(new { cname = "wall_saving", data = __data[0] });
                    data.Add(new { cname = "wall_savingpercent", data = __data[1] });

                    double wall_saving_elec = zoning_sum_elec - wall_sum_elec;
                    double wall_saving_noelec = zoning_sum_noelec - wall_sum_noelec;

                    double wall_tCO2_elec = wall_saving_elec * 0.4747 / 1000000 * 1000;
                    double wall_TOE_elec = wall_saving_elec * 0.00023;

                    double wall_tCO2_noelec = wall_saving_noelec / 43.1 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    double wall_TOE_noelec = wall_saving_noelec / 43.1 / 0.277778 * 0.00103;
                    double wall_tCO2 = wall_tCO2_elec + wall_tCO2_noelec; 
                    double wall_TOE = wall_TOE_elec + wall_TOE_noelec;
                    __data[2].Add(new { idx = i, val = wall_tCO2.ToString("0.0") });  //tco2
                    __data[3].Add(new { idx = i, val = wall_TOE.ToString("0.0") });  //TOE 
                    data.Add(new { cname = "wall_tco2", data = __data[2] });
                    data.Add(new { cname = "wall_toe", data = __data[3] });

                    Value = Program.DB.querySQL(DB.type.ProjDB, "SELECT DISTINCT a.명칭,a.유효열관류율,a.기존외벽 From ConstructionWall as a  Inner Join ZoneEnvelope_3D as b  on a.번호 = b.구조체번호  where b.외피유형 ='외벽' Order by a.유효열관류율 DESC");
                    string[] wall_name = new string[8]; double[] wall_ueff = new double[8]; double[] wall_ueff_old = new double[8];
                    if (Value.Length > 0)
                    {
                       for(int k =0; k < Value.Length; k++)
                        {
                            wall_name[k] = Value[k][0];
                            wall_ueff[k] = Convert.ToDouble(Value[k][1]);
                            if (Value[k][2] != "")
                            { string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "유효열관류율", "명칭 ='" + Value[k][2] + "'"); 
                                if(value2.Length > 0)
                                {
                                    wall_ueff_old[k] = Convert.ToDouble(value2[0][0]);
                                }
                            }
                        }


                        __data[4].Add(new { idx = i, val = wall_name[0] });//명칭
                        __data[5].Add(new { idx = i, val = wall_name[1] });//명칭
                        __data[6].Add(new { idx = i, val = wall_name[2] });//명칭
                        __data[7].Add(new { idx = i, val = wall_name[3] });//명칭
                        __data[8].Add(new { idx = i, val = wall_name[4] });//명칭
                        __data[9].Add(new { idx = i, val = wall_name[5] });//명칭
                        __data[10].Add(new { idx = i, val = wall_name[6] });//명칭   
                        __data[11].Add(new { idx = i, val = wall_name[7] });//명칭
                        data.Add(new { cname = "wall_name0", data = __data[4] });
                        data.Add(new { cname = "wall_name1", data = __data[5] });
                        data.Add(new { cname = "wall_name2", data = __data[6] });
                        data.Add(new { cname = "wall_name3", data = __data[7] });
                        data.Add(new { cname = "wall_name4", data = __data[8] });
                        data.Add(new { cname = "wall_name5", data = __data[9] });
                        data.Add(new { cname = "wall_name6", data = __data[10] });
                        data.Add(new { cname = "wall_name7", data = __data[11] });

                        __data[12].Add(new { idx = i, val = wall_ueff[0].ToString("0.00") });//계획열관류율
                        __data[13].Add(new { idx = i, val = wall_ueff[1].ToString("0.00") });//계획열관류율
                        __data[14].Add(new { idx = i, val = wall_ueff[2].ToString("0.00") });//계획열관류율
                        __data[15].Add(new { idx = i, val = wall_ueff[3].ToString("0.00") });//계획열관류율
                        __data[16].Add(new { idx = i, val = wall_ueff[4].ToString("0.00") });//계획열관류율
                        __data[17].Add(new { idx = i, val = wall_ueff[5].ToString("0.00") });//계획열관류율
                        __data[18].Add(new { idx = i, val = wall_ueff[6].ToString("0.00") });//계획열관류율
                        __data[19].Add(new { idx = i, val = wall_ueff[7].ToString("0.00") });//계획열관류율
                        data.Add(new { cname = "wall_ueff0", data = __data[12] });
                        data.Add(new { cname = "wall_ueff1", data = __data[13] });
                        data.Add(new { cname = "wall_ueff2", data = __data[14] });
                        data.Add(new { cname = "wall_ueff3", data = __data[15] });
                        data.Add(new { cname = "wall_ueff4", data = __data[16] });
                        data.Add(new { cname = "wall_ueff5", data = __data[17] });
                        data.Add(new { cname = "wall_ueff6", data = __data[18] });
                        data.Add(new { cname = "wall_ueff7", data = __data[19] });
                    }

                    #endregion


                    items.Add("Element_structure.htm");
                    s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                    s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());
                    System.Text.Json.JsonSerializer.Serialize(__data[10].ToArray());
                   
                    Debug.Print("start");
                    if (charts != "") charts += ",";                  
                    runScript("init(" + s + "," + s2 + "," + "[" + charts + "])");
                }
            }
        }
    }
}
