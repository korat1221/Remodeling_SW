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
    public partial class Algorithm_DHW : Form
    {
        bool scriptable = false;
        public Algorithm_DHW()
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
            string[][] 번호 = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "번호", "");
            List<object> items = new List<object>();
            List<object> data = new List<object>();
            List<object>[] FormData = new List<object>[30];
            List<object>[] ZoneData = new List<object>[30];
            List<object>[] ZahuData = new List<object>[30];
            List<object>[] PumpData = new List<object>[30];
            List<object>[] AnnualData = new List<object>[30];
            List<object>[] ZoneMthData = new List<object>[100];
            List<object>[] ZahuMthData = new List<object>[100];
            List<object>[] MthData = new List<object>[100];
            List<object>[] WMthData = new List<object>[100];
            List<string> chart_nd = new List<string>();
            List<string> chart_ce = new List<string>();
            List<string> chart_d = new List<string>();
            List<string> chart_s = new List<string>();
            List<string> chart_g = new List<string>();
            List<string> chart_f = new List<string>();
            int i = -1;
            while (++i < 30)
            {
                FormData[i] = new List<object>();
                ZoneData[i] = new List<object>();
                PumpData[i] = new List<object>();
                AnnualData[i] = new List<object>();
                PumpData[i] = new List<object>();
            }
            i = -1;
            while (++i < 100)
            {
                ZoneMthData[i] = new List<object>();
                MthData[i] = new List<object>();
                WMthData[i] = new List<object>();
            }

            i = -1;
            while (++i < 번호.Length)
            {
                string Num = 번호[i][0]; string MainSystem = "", sub1 = "-", sub2 = "-";
                double power = 0; double count = 0; double eta = 0; string systemnum = ""; string etaunit = "";
                items.Add("dhwReport_new.html"); // 예시 코드: 메인 메뉴 동적 할당
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호");
                if (Value.Length > 0)
                {
                    FormData[0].Add(new { idx = i, val = Value[0][0] }); //프로젝트번호
                }
                FormData[1].Add(new { idx = i, val = Num }); //그림번호
                FormData[2].Add(new { idx = i, val = Num }); //번호
                #region 주요정보
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select 명칭,주요설비,보조설비1,보조설비2,공급환수온도,노출배관길이  From DHWSystem_Form Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    FormData[3].Add(new { idx = i, val = Value[0][0] });
                    MainSystem = Value[0][1];
                    FormData[4].Add(new { idx = i, val = MainSystem });
                    sub1 = Value[0][2] != "" ? Value[0][2] : "-";
                    FormData[5].Add(new { idx = i, val = sub1 });
                    sub2 = Value[0][3] != "" ? Value[0][3] : "-";
                    FormData[6].Add(new { idx = i, val = sub2 });
                    FormData[7].Add(new { idx = i, val = Value[0][4] });
                    FormData[8].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][5], 1) });
                }
                string[][] List = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "번호,명칭,주요설비,보일러종류,히트펌프번호,지역난방번호,태양열번호", "번호='" + Num + "'");
                string[][] count_ = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "번호,명칭,주요설비,보일러대수,히트펌프대수,지역난방번호,모듈개수", "번호='" + Num + "'");
                if (List.Length > 0 && count_.Length > 0)
                {

                    string[][] SystemValue = null;
                    if (MainSystem == "보일러")
                    {
                        systemnum = List[0][3];
                        etaunit = "%";
                        SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "용량,전부하효율", "번호 ='" + systemnum + "'");
                        count = count_[0][3] != "" ? Convert.ToDouble(count_[0][3]) : 0;
                    }
                    else if (MainSystem == "외기 히트펌프")
                    {
                        systemnum = List[0][4];
                        etaunit = "W/W";
                        SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_DHWHP", "급탕정격용량,급탕정격COP", "번호 ='" + systemnum + "'");
                        count = count_[0][4] != "" ? Convert.ToDouble(count_[0][4]) : 0;
                    }
                    else if (MainSystem == "지역난방")
                    {
                        systemnum = List[0][5];
                        SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_DH", "용량", "번호 ='" + systemnum + "'");
                        count = count_[0][5] != "" ? Convert.ToDouble(count_[0][5]) : 0;
                    }

                    if (SystemValue.Length > 0)
                    {
                        power = SystemValue[0][0] != "" ? Convert.ToDouble(SystemValue[0][0]) : 0;
                        if (SystemValue[0].Length > 1)
                        {
                            eta = SystemValue[0][1] != "" ? Convert.ToDouble(SystemValue[0][1]) : 0;
                        }
                    }
                }
                FormData[9].Add(new { idx = i, val = Program.UTIL.doubleComa((power * count).ToString(), 1) });
                FormData[10].Add(new { idx = i, val = Program.UTIL.doubleComa(power.ToString(), 1) });
                FormData[11].Add(new { idx = i, val = Program.UTIL.doubleComa(eta.ToString(), 1) });
                FormData[12].Add(new { idx = i, val = count.ToString("0") });
                FormData[13].Add(new { idx = i, val = systemnum });
                FormData[14].Add(new { idx = i, val = etaunit });
                FormData[15].Add(new { idx = i, val = Num + ". 급탕 에너지소요량 검토 보고서" }); //title
                #endregion

                ArrayList splitzone = new ArrayList();
                #region 존정보
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select 존 From DHWSystem_Form Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    splitzone = Split_(Value[0][0]);
                    ZoneData[0].Add(new { idx = i, val = splitzone.Count });
                    string[][] ZoneValue = Program.DB.querySQL(DB.type.ProjDB, "Select  sum(Qwb_mth_sum) From DHWSystem_Result Where 번호='" + Num + "'");
                    if (ZoneValue.Length > 0)
                    {
                        ZoneData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(ZoneValue[0][0], 0) });
                    }
                    double Zmax = 0; double ZArea = 0.00000001; string MainZone="", MainProfile="";
                    List<double> Qwb = new List<double>();
                    for (int a = 0; a < splitzone.Count; a++)
                    {
                        ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "일일급탕요구량,용도프로필", "존번호 = '" + splitzone[a].ToString() + "'");
                       
                        if (ZoneValue.Length > 0 && ZoneValue[0][0] != "" && ZoneValue[0][1] != "")
                        {
                            Qwb.Add(Convert.ToDouble(ZoneValue[0][0]));
                            string[][] Usage = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "급탕시간당비율", "용도명 = '" + ZoneValue[0][1] + "'");
                            if (Usage.Length > 0)
                            { Zmax += Convert.ToDouble(ZoneValue[0][0]) * Convert.ToDouble(Usage[0][0]); }
                        }

                        ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "존번호='" + splitzone[a].ToString() + "'");
                        if (ZoneValue.Length > 0)
                        {
                            ZArea += Convert.ToDouble(ZoneValue[0][0]);
                        }
                        ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존이름,용도프로필", "일일급탕요구량 = '" + Qwb.Max() + "'");
                        if(ZoneValue.Length >0)
                        {
                            MainZone = ZoneValue[0][0];
                            MainProfile = ZoneValue[0][1];
                        }
                    }
                    ZoneData[2].Add(new { idx = i, val = Program.UTIL.doubleComa((Zmax ).ToString(), 1) });
                    ZoneData[4].Add(new { idx = i, val = Program.UTIL.doubleComa((ZArea).ToString(), 1) });
                    ZoneData[5].Add(new { idx = i, val = MainZone });
                    ZoneData[6].Add(new { idx = i, val = MainProfile });

                }
                #endregion
                #region 연간정보 
                double Area = 0.000000011;
                for (int a = 0; a < splitzone.Count; a++)
                {
                    string[][] Z = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "존번호='" + splitzone[a].ToString() + "'");
                    if (Z.Length > 0)
                    {
                        Area += Convert.ToDouble(Z[0][0]);
                    }
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select  sum(Qwb_mth_sum),  sum(Qw_d), sum(Qw_s),  sum(Qw_gen), sum(Qw_f),sum(Ww_d),sum(Ww_s),sum(Ww_g),연료 From DHWSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    double d = 0, st = 0, g = 0, f = 0;

                    d = (Convert.ToDouble(Value[0][1]) + Convert.ToDouble(Value[0][5])) / Area;
                    st = (Convert.ToDouble(Value[0][2]) + Convert.ToDouble(Value[0][6])) / Area;
                    g = (Convert.ToDouble(Value[0][3]) + Convert.ToDouble(Value[0][7])) / Area;
                    AnnualData[0].Add(new { idx = i, val = Program.UTIL.doubleComa((Convert.ToDouble(Value[0][0]) / Area).ToString(), 1) });
                    AnnualData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(d.ToString(), 1) });
                    AnnualData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(st.ToString(), 1) });
                    AnnualData[3].Add(new { idx = i, val = Program.UTIL.doubleComa(g.ToString(), 1) });
                    double w = 0;
                    for (int a = 5; a < 8; a++)
                    {
                        w += Value[0][a] != "" ? Convert.ToDouble(Value[0][a]) : 0;
                    }
                    w = w / Area;
                    f = (Convert.ToDouble(Value[0][4]) + w) / Area;
                    AnnualData[4].Add(new { idx = i, val = Program.UTIL.doubleComa(f.ToString(), 1) });

                    AnnualData[5].Add(new { idx = i, val = Program.UTIL.doubleComa(w.ToString(), 0) });
                    double primary = 0, kgco2 = 0;
                    if (Value[0][8] == "전기")
                    {
                        primary = (Convert.ToDouble(Value[0][4]) + w) * 2.75;
                        kgco2 = (Convert.ToDouble(Value[0][4]) + w) * 0.4747 / 1000000 * 1000 * 1000;
                    }
                    else
                    {
                        primary = Convert.ToDouble(Value[0][4]) * 1.1 + w * 2.75;
                        kgco2 = Convert.ToDouble(Value[0][4]) * 0.4747 / 1000000 * 1000 * 1000 + w / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000 * 1000;
                    }
                    primary = primary / Area;
                    kgco2 = kgco2 / Area;
                    AnnualData[6].Add(new { idx = i, val = primary.ToString("0.0") });
                    AnnualData[7].Add(new { idx = i, val = kgco2.ToString("0.0") });
                }
                #endregion

                #region 존월별 
                for (int mth = 0; mth < 12; mth++)
                {
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select  Qw_outg,  Qw_d, Qw_s, Qwb_mth_sum  From DHWSystem_Result Where 번호='" + Num + "' and 월='" + (mth + 1) + "월'");
                    if (Value.Length > 0)
                    {
                        ZoneMthData[0].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                        ZoneMthData[1].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                        ZoneMthData[2].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                        ZoneMthData[3].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                    }
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select  sum(Qw_outg),    sum(Qw_d),  sum(Qw_s),  sum(Qwb_mth_sum)  From DHWSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    ZoneMthData[0].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                    ZoneMthData[1].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                    ZoneMthData[2].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                    ZoneMthData[3].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                }
                #endregion
           

                double[] ce_mth = new double[12], d_mth = new double[12], s_mth = new double[12], g_mth = new double[12], f_mth = new double[12];
                double ce_a = 0, d_a = 0, s_a = 0, g_a = 0, f_a = 0;
                #region 보조에너지
                for (int mth = 0; mth < 12; mth++)
                {
                    double w = 0;
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select Ww_d, Ww_s, Ww_g From DHWSystem_Result Where 번호='" + Num + "' and 월='" + (mth + 1) + "월'");
                    if (Value.Length > 0)
                    {
                        d_mth[mth] = Convert.ToDouble(Value[0][0]); s_mth[mth] = Convert.ToDouble(Value[0][1]); g_mth[mth] = Convert.ToDouble(Value[0][2]);
                        f_mth[mth] = Convert.ToDouble(Value[0][0]) + Convert.ToDouble(Value[0][1]) + Convert.ToDouble(Value[0][2]);

                        WMthData[1].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][0], 1) });
                        WMthData[2].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][1], 1) });
                        WMthData[3].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][2], 1) });
                        for (int a = 0; a < Value[0].Length; a++)
                        {
                            w += Value[0][a] != "" ? Convert.ToDouble(Value[0][a]) : 0;
                        }
                        WMthData[0].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(w.ToString(), 1) });
                    }
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select  sum(Ww_d), sum(Ww_s), sum(Ww_g)  From DHWSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    double w = 0;
                    d_a = Convert.ToDouble(Value[0][0]); s_a = Convert.ToDouble(Value[0][1]); g_a = Convert.ToDouble(Value[0][2]);
                    f_a = Convert.ToDouble(Value[0][0]) + Convert.ToDouble(Value[0][1]) + Convert.ToDouble(Value[0][2]);
                    WMthData[1].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][0], 1) });
                    WMthData[2].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][1], 1) });
                    WMthData[3].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][2], 1) });
                    for (int a = 0; a < Value[0].Length; a++)
                    {
                        w += Value[0][a] != "" ? Convert.ToDouble(Value[0][a]) : 0;
                    }
                    WMthData[0].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(w.ToString(), 1) });
                }
                #endregion 
                #region 에너지소요량 월별 
                for (int mth = 0; mth < 12; mth++)
                {
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select  Qw_f,  Qw_outg, Qw_d, Qw_s, Qw_gen, Qwb_mth_sum From DHWSystem_Result Where 번호='" + Num + "' and 월='" + (mth + 1) + "월'");
                    if (Value.Length > 0)
                    {
                        d_mth[mth] = d_mth[mth] + Convert.ToDouble(Value[0][2]); s_mth[mth] = s_mth[mth] + Convert.ToDouble(Value[0][3]); g_mth[mth] = g_mth[mth] + Convert.ToDouble(Value[0][4]);
                        f_mth[mth] = Convert.ToDouble(Value[0][0]) + f_mth[mth];
                        MthData[0].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(f_mth[mth].ToString(), 0) });
                        MthData[1].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                        MthData[2].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(d_mth[mth].ToString(), 0) });
                        MthData[3].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(s_mth[mth].ToString(), 0) });
                        MthData[4].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(g_mth[mth].ToString(), 0) });
                        MthData[5].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][5], 0) });
                    }
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select  Sum(Qw_f), Sum(Qw_outg), sum(Qw_d), sum(Qw_s),sum(Qw_gen), sum(Qwb_mth_sum)  From DHWSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                     d_a = d_a + Convert.ToDouble(Value[0][2]); s_a = s_a + Convert.ToDouble(Value[0][3]); g_a = g_a + Convert.ToDouble(Value[0][4]);
                    f_a = Convert.ToDouble(Value[0][0]) + f_a;
                    MthData[0].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(f_a.ToString(), 0) });
                    MthData[1].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                    MthData[2].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(d_a.ToString(), 0) });
                    MthData[3].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(s_a.ToString(), 0) });
                    MthData[4].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(g_a.ToString(), 0) });
                    MthData[5].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][5].ToString(), 0) });
                }
                #endregion
                #region 펌프
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select  a.펌프1제어,b.동력  From DHWSystem_Form as a Inner Join  User_Pump as b on a.펌프1종류 =b.번호 Where a.번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    PumpData[0].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][1], 1) });
                    PumpData[2].Add(new { idx = i, val = Value[0][0] });
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select  a.펌프2종류,b.동력  From DHWSystem_Form as a Inner Join  User_Pump as b on a.펌프2종류 =b.번호 Where a.번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    PumpData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][1], 1) });
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select  a.축열용량,b.동력  From DHWSystem_Form as a Inner Join  User_Pump as b on a.축열펌프 =b.번호 Where a.번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    PumpData[3].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][0], 1) });
                    PumpData[4].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][1], 1) });
                }
                #endregion
                data.Add(new { cname = "projectnum", data = FormData[0] });
                data.Add(new { cname = "dhwnum", data = FormData[1] });
                data.Add(new { cname = "dhwnum2", data = FormData[2] });
                data.Add(new { cname = "dhwname", data = FormData[3] });
                data.Add(new { cname = "mainsystem", data = FormData[4] });
                data.Add(new { cname = "subsystem1", data = FormData[5] });
                data.Add(new { cname = "subsystem2", data = FormData[6] });
                data.Add(new { cname = "supplyreturntemper", data = FormData[7] });
                data.Add(new { cname = "pipelength", data = FormData[8] });
                data.Add(new { cname = "totpower", data = FormData[9] });
                data.Add(new { cname = "power", data = FormData[10] });
                data.Add(new { cname = "eta", data = FormData[11] });
                data.Add(new { cname = "systemcount", data = FormData[12] });
                data.Add(new { cname = "systemnum", data = FormData[13] });
                data.Add(new { cname = "etaunit", data = FormData[14] });
                data.Add(new { cname = "title", data = FormData[15] });


                data.Add(new { cname = "zone_count", data = ZoneData[0] });
                data.Add(new { cname = "zone_need", data = ZoneData[1] });
                data.Add(new { cname = "zone_max", data = ZoneData[2] });
                data.Add(new { cname = "zone_area", data = ZoneData[4] });
                data.Add(new { cname = "zone_main", data = ZoneData[5] });
                data.Add(new { cname = "zone_profile", data = ZoneData[6] });


                data.Add(new { cname = "heatsource", data = PumpData[0] });
                data.Add(new { cname = "top_power", data = PumpData[1] });
                data.Add(new { cname = "top_in", data = PumpData[2] });
                data.Add(new { cname = "top_out", data = PumpData[3] });


                data.Add(new { cname = "annual_nd", data = AnnualData[0] });
                data.Add(new { cname = "annual_d", data = AnnualData[1] });
                data.Add(new { cname = "annual_s", data = AnnualData[2] });
                data.Add(new { cname = "annual_g", data = AnnualData[3] });
                data.Add(new { cname = "annual_f", data = AnnualData[4] });
                data.Add(new { cname = "annual_w", data = AnnualData[5] });
                data.Add(new { cname = "annual_p", data = AnnualData[6] });
                data.Add(new { cname = "annual_kgco2", data = AnnualData[7] });


                data.Add(new { cname = "zone_mth_outg", data = ZoneMthData[0] });
                data.Add(new { cname = "zone_mth_d", data = ZoneMthData[1] });
                data.Add(new { cname = "zone_mth_s", data = ZoneMthData[2] });
                data.Add(new { cname = "zone_mth_nd", data = ZoneMthData[3] });


                data.Add(new { cname = "mth_f", data = MthData[0] });
                data.Add(new { cname = "mth_outg", data = MthData[1] });
                data.Add(new { cname = "mth_d", data = MthData[2] });
                data.Add(new { cname = "mth_s", data = MthData[3] });
                data.Add(new { cname = "mth_g", data = MthData[4] });
                data.Add(new { cname = "mth_nd", data = MthData[5] });

                data.Add(new { cname = "w", data = WMthData[0] });
                data.Add(new { cname = "w_d", data = WMthData[1] });
                data.Add(new { cname = "w_s", data = WMthData[2] });
                data.Add(new { cname = "w_g", data = WMthData[3] });

                data.Add(new { cname = "pump1_power", data = PumpData[0] });
                data.Add(new { cname = "pump2_power", data = PumpData[1] });
                data.Add(new { cname = "pump_control", data = PumpData[2] });
                data.Add(new { cname = "volume_s", data = PumpData[3] });
                data.Add(new { cname = "pumps_power", data = PumpData[4] });

                List<object> nd_chart = new List<object>();
                List<object> d_chart = new List<object>();
                List<object> s_chart = new List<object>();
                List<object> g_chart = new List<object>();
                List<object> f_chart = new List<object>();
                for (int mth = 0; mth < 12; mth++)
                {
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select Qwb_mth_sum, Qw_d, Qw_s, Qw_gen,Qw_f From DHWSystem_Result Where 번호='" + Num + "' and 월='" + (mth + 1) + "월'");
                    if (Value.Length > 0)
                    {
                        nd_chart.Add(Convert.ToDouble(Program.UTIL.doubleComa(Value[0][0], 0)));
                        d_chart.Add(Convert.ToDouble(Program.UTIL.doubleComa(Value[0][1], 0)));
                        s_chart.Add(Convert.ToDouble(Program.UTIL.doubleComa(Value[0][2], 0)));
                        g_chart.Add(Convert.ToDouble(Program.UTIL.doubleComa(Value[0][3], 0)));
                        f_chart.Add(Convert.ToDouble(Program.UTIL.doubleComa(Value[0][4], 0)));
                    }
                }
                chart_nd.Add(System.Text.Json.JsonSerializer.Serialize(nd_chart.ToArray()));
                chart_d.Add(System.Text.Json.JsonSerializer.Serialize(d_chart.ToArray()));
                chart_s.Add(System.Text.Json.JsonSerializer.Serialize(s_chart.ToArray()));
                chart_g.Add(System.Text.Json.JsonSerializer.Serialize(g_chart.ToArray()));
                chart_f.Add(System.Text.Json.JsonSerializer.Serialize(f_chart.ToArray()));
                double max = 0;
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select Max(Qwb_mth_sum), (Qw_outg), Max(Qw_f) From DHWSystem_Result Where 번호='" + Num + "' and 월='1월'");
                if (Value.Length > 0)
                {
                    max = Convert.ToDouble(Value[0][0]) > Convert.ToDouble(Value[0][1]) ? Convert.ToDouble(Value[0][0]) : Convert.ToDouble(Value[0][1]);   
                }
                int n = ((int)max).ToString().Length;
                max = Convert.ToDouble(String.Format("{0:F0}", max / Math.Pow(10, n - 1))) * Math.Pow(10, n - 1) + Math.Pow(10, n - 1);
                if (charts != "") charts += ",";
                charts += "{data:[" +
                "{type:\"bar\",barPercentage:0.4,label:\"에너지요구량 [kWh]\",data:" + chart_nd[i] + ",borderColor:\"#FFE0B2\",backgroundColor:\"#FFE0B2\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"분배열손실 [kWh]\",data:" + chart_d[i] + ",borderColor:\"#F06500\",backgroundColor:\"#F06500\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"저장열손실 [kWh]\",data:" + chart_s[i] + ",borderColor:\"#9DC3E6\",backgroundColor:\"#9DC3E6\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"생산열손실 [kWh]\",data:" + chart_g[i] + ",borderColor:\"#FFCCFF\",backgroundColor:\"#FFCCFF\",dash:false}," +
                "{type:\"line\",yAxisID: 'y',label:\"에너지소요량 [kWh]\",data:" + chart_f[i] + ",borderColor:\"#ED7D31\",backgroundColor:\"#ED7D31\",dash:false, tension: 0.4}," +
                "],max:" + max.ToString() + ",step:100,legend:true,stacked:true}";

            }
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

    }
}