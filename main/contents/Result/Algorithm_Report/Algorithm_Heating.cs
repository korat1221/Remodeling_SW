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
            string[][] 번호 = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "번호", "");
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
                WMthData[i] = new List<object>();
            }

            i = -1;
            while (++i < 번호.Length)
            {
                string Num = 번호[i][0]; string MainSystem = "", sub1 ="-", sub2 ="-";
                double power = 0; double count = 0; double eta = 0; string systemnum = ""; string etaunit = "";
                items.Add("Algorithm_Heating.htm"); // 예시 코드: 메인 메뉴 동적 할당
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호");
                if (Value.Length > 0)
                {
                    FormData[0].Add(new { idx = i, val = Value[0][0] }); //프로젝트번호
                }
                FormData[1].Add(new { idx = i, val = Num }); //그림번호
                FormData[2].Add(new { idx = i, val = Num }); //번호
                #region 주요정보
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select 명칭,주요설비,보조설비1,보조설비2,공급환수온도,노출배관길이  From HeatingSystem_Form Where 번호='" + Num + "'");
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
                    FormData[8].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][5],1) });
                }
                string[][] List = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "번호,명칭,주요설비,보일러종류,외기히트펌프번호,흡수식온수기번호,지역난방번호,태양열번호", "번호='" + Num + "'");
                string[][] count_ = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "번호,명칭,주요설비,보일러대수,외기히트펌프대수,흡수식온수기대수,지역난방번호,모듈개수", "번호='" + Num + "'");
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
                        SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", "난방정격용량,난방정격COP", "번호 ='" + systemnum + "'");
                        count = count_[0][4] != "" ? Convert.ToDouble(count_[0][4]) : 0;
                    }
                    else if (MainSystem == "흡수식온수기")
                    {
                        systemnum = List[0][5];
                        etaunit = "W/W";
                        SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_ABS", "난방용량,난방성능", "번호 ='" + systemnum + "'");
                        count = count_[0][5] != "" ? Convert.ToDouble(count_[0][5]) : 0; ;
                    }
                    else if (MainSystem == "지역난방")
                    {
                        systemnum = List[0][6];
                        SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_DH", "용량", "번호 ='" + systemnum + "'");
                        count = count_[0][6] != "" ? Convert.ToDouble(count_[0][6]) : 0;
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
                FormData[9].Add(new { idx = i, val = Program.UTIL.doubleComa((power*count).ToString(),1) });
                FormData[10].Add(new { idx = i, val = Program.UTIL.doubleComa(power.ToString(), 1) });
                FormData[11].Add(new { idx = i, val = Program.UTIL.doubleComa(eta.ToString(), 1) });
                FormData[12].Add(new { idx = i, val = count.ToString("0") });
                FormData[13].Add(new { idx = i, val = systemnum });
                FormData[14].Add(new { idx = i, val = etaunit });
                FormData[15].Add(new { idx = i, val = Num + ". 난방 에너지소요량 검토 보고서" }); //title
                #endregion
                #region 존정보
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select 존,공조기 From heatingSystem_Form Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    ArrayList splitzone = new ArrayList();
                    splitzone = Split_(Value[0][0]);
                    ZoneData[0].Add(new { idx = i, val = splitzone.Count });
                    string[][] ZoneValue = Program.DB.querySQL(DB.type.ProjDB, "Select Sum(Qhb_z) From heatingSystem_Result Where 번호='" + Num + "'");
                    if (ZoneValue.Length > 0)
                    {
                        ZoneData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(ZoneValue[0][0], 0) });
                    }
                    double Zmax = 0;
                    for(int a=0; a<splitzone.Count; a++)
                    {
                        ZoneValue = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct Q_max From Zone_HCneed_Result Where 번호='" + splitzone[a].ToString() + "' and 난방_냉방='난방' and 비이용일_이용일='이용일'");
                        if (ZoneValue.Length > 0)
                        {
                            Zmax += Convert.ToDouble(ZoneValue[0][0]);
                        }
                    }
                    ZoneData[2].Add(new { idx = i, val = Program.UTIL.doubleComa((Zmax/1000).ToString(), 1) });
                    ZoneValue = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct 공급설비종류 From Heating_ce_Form Where 난방시스템='" + Num + "' and (Not 공급설비종류='CAV유닛' and Not 공급설비종류 ='VAV유닛' and Not 공급설비종류='파워팬유닛')");
                   if(ZoneValue.Length > 0)
                    {
                        ZoneData[3].Add(new { idx = i, val = ZoneValue[0][0] });
                    }
                    if (ZoneValue.Length > 1)
                    {
                        ZoneData[4].Add(new { idx = i, val = ZoneValue[0][1] });
                    }
                }
                #endregion
                #region 공조존정보
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select 존,공조기 From heatingSystem_Form Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    ArrayList splitAHU = new ArrayList();
                    splitAHU = Split_(Value[0][1]);
                    ZahuData[0].Add(new { idx = i, val = splitAHU.Count });
                    string[][] ZoneValue = Program.DB.querySQL(DB.type.ProjDB, "Select Sum(Qhb_ahu) From heatingSystem_Result Where 번호='" + Num + "'");
                    if (ZoneValue.Length > 0)
                    {
                        ZahuData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(ZoneValue[0][0], 0) });
                    }
                    double Zmax = 0;
                    for (int a = 0; a < splitAHU.Count; a++)
                    {
                        ZoneValue = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct a.Q_max From Zone_HCneed_Result as a Inner Join ZoneGeneral_Form as b on a.번호=b.존번호 Where b.선택열회수기='" + splitAHU[a].ToString() + "' and a.난방_냉방='난방' and a.비이용일_이용일='이용일' and 월='1월' and not b.선택열회수기=''");
                        if (ZoneValue.Length > 0)
                        {
                            for(int aa=0; aa<ZoneValue.Length; aa++)
                            {
                                Zmax += Convert.ToDouble(ZoneValue[aa][0]);
                            }
                        }
                    }
                    ZahuData[2].Add(new { idx = i, val = Program.UTIL.doubleComa((Zmax / 1000).ToString(), 1) });
                    ZoneValue = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct 공급설비종류 From Heating_ce_Form Where 난방시스템='" + Num + "' and (공급설비종류='CAV유닛' OR 공급설비종류 ='VAV유닛' OR 공급설비종류='파워팬유닛')");
                    if (ZoneValue.Length > 0)
                    {
                        ZahuData[3].Add(new { idx = i, val = ZoneValue[0][0] });
                    }
                    if (ZoneValue.Length > 1)
                    {
                        ZahuData[4].Add(new { idx = i, val = ZoneValue[0][1] });
                    }
                }
                #endregion
                #region 연간정보 
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select  sum(Qhb_mth_sum),  sum(Qh_ce),  sum(Qh_d), sum(Qh_s),  sum(Qh_outg), sum(Qh_f),sum(Wh_ce),sum(Wh_d),sum(Wh_s),sum(Wh_g),연료 From HeatingSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    AnnualData[0].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                    AnnualData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                    AnnualData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                    AnnualData[3].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                    AnnualData[4].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                    AnnualData[5].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][5], 0) });
                    double w = 0;
                    for(int a=6; a<10; a++)
                    {
                        w += Value[0][a]!="" ? Convert.ToDouble(Value[0][a]) : 0;
                    }

                    AnnualData[6].Add(new { idx = i, val = Program.UTIL.doubleComa(w.ToString(), 0) });
                    double primary = 0, tco2 = 0;
                    if (Value[0][10] == "전기")
                    {
                        primary = (Convert.ToDouble(Value[0][5]) + w) * 2.75;
                        tco2 = (Convert.ToDouble(Value[0][5]) + w) * 0.4747 / 1000000 * 1000;
                    }
                    else
                    {
                        primary = Convert.ToDouble(Value[0][5]) * 1.1 + w * 2.75;
                        tco2 = Convert.ToDouble(Value[0][5]) * 0.4747 / 1000000 * 1000 + w / 38.9 / 0.277778 * 38.5 * 15.236 / 1000000 * 44 / 12 * 1000 / 1000;
                    }
                    AnnualData[7].Add(new { idx = i, val = primary.ToString("#,##0") });
                    AnnualData[8].Add(new { idx = i, val = tco2.ToString("0.0") });
                }
                #endregion

                #region 존월별 
                for (int mth = 0; mth < 12; mth++)
                {
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select  Qh_outg_z,  Qh_ce_z,  Qh_d_z, Qh_s_z, Qhb_z  From HeatingSystem_Result Where 번호='" + Num + "' and 월='" + (mth + 1) + "월'");
                    if (Value.Length > 0)
                    {
                        ZoneMthData[0].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                        ZoneMthData[1].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                        ZoneMthData[2].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                        ZoneMthData[3].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                        ZoneMthData[4].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                    }
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select  sum(Qh_outg_z),   sum(Qh_ce_z),   sum(Qh_d_z),  sum(Qh_s_z),  sum(Qhb_z)  From HeatingSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    ZoneMthData[0].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                    ZoneMthData[1].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                    ZoneMthData[2].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                    ZoneMthData[3].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                    ZoneMthData[4].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                }
                #endregion
                #region 공조존월별 
                for (int mth = 0; mth < 12; mth++)
                {
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select  Qh_outg_ahu,  Qh_ce_ahu,  Qh_d_ahu, Qh_s_ahu, Qhb_ahu  From HeatingSystem_Result Where 번호='" + Num + "' and 월='" + (mth + 1) + "월'");
                    if (Value.Length > 0)
                    {
                        ZahuMthData[0].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                        ZahuMthData[1].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                        ZahuMthData[2].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                        ZahuMthData[3].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                        ZahuMthData[4].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                    }
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select  sum(Qh_outg_ahu),   sum(Qh_ce_ahu),   sum(Qh_d_ahu),  sum(Qh_s_ahu),  sum(Qhb_ahu)  From HeatingSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    ZahuMthData[0].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                    ZahuMthData[1].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                    ZahuMthData[2].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                    ZahuMthData[3].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                    ZahuMthData[4].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                }
                #endregion

                #region 에너지소요량 월별 
                for (int mth = 0; mth < 12; mth++)
                {
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select  Qh_f,  Qh_outg, Qh_ce, Qh_d, Qh_s, Qh_gen From HeatingSystem_Result Where 번호='" + Num + "' and 월='" + (mth + 1) + "월'");
                    if (Value.Length > 0)
                    {
                        MthData[0].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                        MthData[1].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                        MthData[2].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                        MthData[3].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                        MthData[4].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                        MthData[5].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][5], 0) });
                    }
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select  Sum(Qh_f), Sum(Qh_outg), sum(Qh_ce), sum(Qh_d), sum(Qh_s),sum(Qh_gen)  From HeatingSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    MthData[0].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][0], 0) });
                    MthData[1].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][1], 0) });
                    MthData[2].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][2], 0) });
                    MthData[3].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][3], 0) });
                    MthData[4].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][4], 0) });
                    MthData[5].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][5], 0) });
                }
                #endregion

                for (int mth = 0; mth < 12; mth++)
                {
                    double w = 0;
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select Wh_ce, Wh_d, Wh_s, Wh_g From HeatingSystem_Result Where 번호='" + Num + "' and 월='" + (mth + 1) + "월'");
                    if (Value.Length > 0)
                    {
                        WMthData[1].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][0], 1) });
                        WMthData[2].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][1], 1) });
                        WMthData[3].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][2], 1) });
                        WMthData[4].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(Value[0][3], 1) });
                        for(int a=0; a < Value[0].Length; a++)
                        {
                            w += Value[0][a] != "" ? Convert.ToDouble(Value[0][a]) : 0;
                        }
                        WMthData[0].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(w.ToString(), 1) });
                    }
                }
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select  sum(Wh_ce), sum(Wh_d), sum(Wh_s), sum(Wh_g)  From HeatingSystem_Result Where 번호='" + Num + "'");
                if (Value.Length > 0)
                {
                    double w = 0;
                    WMthData[1].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][0], 1) });
                    WMthData[2].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][1], 1) });
                    WMthData[3].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][2], 1) });
                    WMthData[4].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(Value[0][3], 1) });
                    for (int a = 0; a < Value[0].Length; a++)
                    {
                        w += Value[0][a] != "" ? Convert.ToDouble(Value[0][a]) : 0;
                    }
                    WMthData[0].Add(new { idx = i * 13 + 12, val = Program.UTIL.doubleComa(w.ToString(), 1) });
                }

                data.Add(new { cname = "projectnum", data = FormData[0] });
                data.Add(new { cname = "num", data = FormData[1] });
                data.Add(new { cname = "heatingnum2", data = FormData[2] });
                data.Add(new { cname = "heatingname", data = FormData[3] });
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
                data.Add(new { cname = "zone_ce1", data = ZoneData[3] });
                data.Add(new { cname = "zone_ce2", data = ZoneData[4] });


                data.Add(new { cname = "zahu_count", data = ZahuData[0] });
                data.Add(new { cname = "zahu_need", data = ZahuData[1] });
                data.Add(new { cname = "zahu_max", data = ZahuData[2] });
                data.Add(new { cname = "zahu_ce1", data = ZahuData[3] });
                data.Add(new { cname = "zahu_ce2", data = ZahuData[4] });


                data.Add(new { cname = "heatsource", data = SourceData[0] });
                data.Add(new { cname = "top_power", data = SourceData[1] });
                data.Add(new { cname = "top_in", data = SourceData[2] });
                data.Add(new { cname = "top_out", data = SourceData[3] });


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

                data.Add(new { cname = "zahu_mth_outg", data = ZahuMthData[0] });
                data.Add(new { cname = "zahu_mth_ce", data = ZahuMthData[1] });
                data.Add(new { cname = "zahu_mth_d", data = ZahuMthData[2] });
                data.Add(new { cname = "zahu_mth_s", data = ZahuMthData[3] });
                data.Add(new { cname = "zahu_mth_nd", data = ZahuMthData[4] });

                data.Add(new { cname = "mth_f", data = MthData[0] });
                data.Add(new { cname = "mth_outg", data = MthData[1] });
                data.Add(new { cname = "mth_ce", data = MthData[2] });
                data.Add(new { cname = "mth_d", data = MthData[3] });
                data.Add(new { cname = "mth_s", data = MthData[4] });
                data.Add(new { cname = "mth_g", data = MthData[5] });

                data.Add(new { cname = "w", data = WMthData[0] });
                data.Add(new { cname = "w_ce", data = WMthData[1] });
                data.Add(new { cname = "w_d", data = WMthData[2] });
                data.Add(new { cname = "w_s", data = WMthData[3] });
                data.Add(new { cname = "w_g", data = WMthData[4] });

                List<object> nd_chart = new List<object>();
                List<object> ce_chart = new List<object>();
                List<object> d_chart = new List<object>();
                List<object> s_chart = new List<object>();
                List<object> f_chart = new List<object>();
                for (int mth = 0; mth < 12; mth++)
                {
                    Value = Program.DB.querySQL(DB.type.ProjDB, "Select Qhb_mth_sum, Qh_ce, Qh_d, Qh_s, Qh_f From HeatingSystem_Result Where 번호='" + Num + "' and 월='" + (mth + 1) + "월'");
                    if (Value.Length > 0)
                    {
                        nd_chart.Add(Convert.ToDouble(Program.UTIL.doubleComa(Value[0][0], 0)));
                        ce_chart.Add(Convert.ToDouble(Program.UTIL.doubleComa(Value[0][1], 0)));
                        d_chart.Add(Convert.ToDouble(Program.UTIL.doubleComa(Value[0][2], 0)));
                        s_chart.Add(Convert.ToDouble(Program.UTIL.doubleComa(Value[0][3], 0)));
                        f_chart.Add(Convert.ToDouble(Program.UTIL.doubleComa(Value[0][4], 0)));
                    }
                }
                chart_nd.Add(System.Text.Json.JsonSerializer.Serialize(nd_chart.ToArray()));
                chart_ce.Add(System.Text.Json.JsonSerializer.Serialize(ce_chart.ToArray()));
                chart_d.Add(System.Text.Json.JsonSerializer.Serialize(d_chart.ToArray()));
                chart_s.Add(System.Text.Json.JsonSerializer.Serialize(s_chart.ToArray()));
                chart_f.Add(System.Text.Json.JsonSerializer.Serialize(f_chart.ToArray()));
                double max = 0;
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select Max(Qh_outg), Max(Qh_f) From HeatingSystem_Result Where 번호='" + Num + "' and 월='1월'");
                if (Value.Length > 0)
                {
                    max = Convert.ToDouble(Value[0][0]) > Convert.ToDouble(Value[0][1]) ? Convert.ToDouble(Value[0][0]) : Convert.ToDouble(Value[0][1]);
                }
                int n = ((int)max).ToString().Length;
                max = Convert.ToDouble(String.Format("{0:F0}", max / Math.Pow(10, n - 1))) * Math.Pow(10, n - 1) + Math.Pow(10, n - 1);
                if (charts != "") charts += ",";
                charts += "{data:[" +
                "{type:\"bar\",barPercentage:0.4,label:\"에너지요구량 [kWh]\",data:" + chart_nd[i] + ",borderColor:\"#A9D18E\",backgroundColor:\"#A9D18E\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"공급열손실 [kWh]\",data:" + chart_ce[i] + ",borderColor:\"#70AD47\",backgroundColor:\"#70AD47\",dash:false}," +
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