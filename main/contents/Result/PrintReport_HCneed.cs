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
    public partial class PrintReport_HCneed : Form
    {
        bool scriptable = false;
        public PrintReport_HCneed()
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
        public void load_List()
        {
            List<object> subMenu = new List<object>();

            subMenu.Add(new { text = "냉난방요구량", id = "{\\\"formID\\\":37,\\\"ID\\\":\\\"Result_0\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            subMenu.Add(new { text = "조명소요량", id = "{\\\"formID\\\":44,\\\"ID\\\":\\\"Result_1\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            subMenu.Add(new { text = "난방소요량", id = "{\\\"formID\\\":45,\\\"ID\\\":\\\"Result_2\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            subMenu.Add(new { text = "냉방소요량", id = "{\\\"formID\\\":46,\\\"ID\\\":\\\"Result_3\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            subMenu.Add(new { text = "급탕소요량", id = "{\\\"formID\\\":47,\\\"ID\\\":\\\"Result_4\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            subMenu.Add(new { text = "공조소요량", id = "{\\\"formID\\\":48,\\\"ID\\\":\\\"Result_5\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            subMenu.Add(new { text = "연료별소요량", id = "{\\\"formID\\\":52,\\\"ID\\\":\\\"Result_6\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당

            Program.UTIL.resetMainTree(5, 2, subMenu.ToArray(), "37"); // 예시 코드: 메인 메뉴 동적 할당
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();
            string s, s2;
            string[][] ZoneG = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,존이름,실제어방식,냉난방유무,환기유무,환기방식,온도교환효율_냉방,전열교환효율_냉방,용도프로필,순바닥면적, 천장고,시작시간,종료시간,주이용일,재실자수,기기발열수준,일일급탕요구량,냉난방시간,사용시간,공조시간,연이용일수,재실밀도,재실수준,일일인체발열,면적당인체발열,일일기기발열,면적당기기발열,순체적,환기횟수,이용일환기량,비이용일환기량,천장축열선택,외벽축열선택,내벽축열선택,바닥축열선택,천장축열,외벽축열,내벽축열,바닥축열,천장면적,외벽면적,내벽면적,바닥면적,존축열성능,존기밀타입,기밀적용유형,q50,n50");


            List<object> items = new List<object>();
            List<object> data = new List<object>();

            List<object>[] __data = new List<object>[700];

            int i = -1, n;

            while (++i < 700)
            {
                __data[i] = new List<object>();
            }

            i = -1;

            try
            {

          
            while (++i < ZoneG.Length)
            {
                string[][] value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "번호,이름,난방_냉방,비이용일_이용일,월,HT_tot,HT_InWall,HT_Slab,HT_Wall,HT_Roof,HT_Floor,HT_GWall,HT_Door,HT_Win,HT_CW,HT_Di_Wall,HT_Indi_Wall,HT_Di_Roof,HT_Indi_Roof,HT_Di_Win,HT_Indi_Win,HT_Di_Door,	HT_Indi_Door,HT_TB_tot,HT_TB_Wall,HT_TB_Roof,HT_TB_Floor,HT_TB_Gwall,HT_TB_Win,HT_TB_Door,HT_TB_CW,nmech	,nz,ninf,nwin,HV_tot,HV_inf,HV_win,HV_z,HV_mech,H_tot,tao,dwe_mth,dwd_mth,theta_i,theta_e,QTsink_tot,QT_u_sink,QTsink_Wall,QTsink_Roof,QTsink_Floor,QTsink_GWall,QTsink_Door,QTsink_Win,QTsink_CW,QTsource_tot,QT_u_source,QTsource_Wall,QTsource_Roof,QTsource_Floor,QTsource_GWall,QTsource_Door,QTsource_Win,QTsource_CW,QSopsink_tot,QSopsource_tot,QStr_tot,QSopsink_Wall,QSopsink_Roof,QSopsink_Door,QSopsink_CW_p,QSopsource_Wall,QSopsource_Roof,QSopsource_Door,QSopsource_CW_p,QStr_Win,QStr_CW,QVsink_tot,QV_inf_sink,QV_win_sink,QV_z_sink,QV_mech_sink,QVsource_tot,QV_inf_source,QV_win_source,QV_z_source,QV_mech_source,Q_DHU_win,Q_DHU_mech,Q_DHU_tot,QI_tot,QI_L,QI_P,QI_fac,QI_Humidity,Qsink,Qsource,gamma,a,eta,dQc_b,dQc_sink,Qb_day,Qb_mth,Qb_a,Q_max,t_max,비냉난방존온도", "번호 ='" + ZoneG[i][0] + "'");
                string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "Zone_LightResult", "번호,ITr,IRD,ISh_Ish,ISh_hA,Ish_vA,Ish_In_At,Wi,Ish_GDF,Ish,f_τeff_SNA,f_D,f_nearD,f_DCA,f_dclass,f_nearEm_SNA,f_fd_sna,f_fd_sa,f_nearEm_DC,f_fd_c,f_FDS,f_FD,as_bs,hs_bs,hg_hw,normal_ηR,saw_ηR,r_DSNA,r_DSA,r_dclass,r_nearEm_FDS,r_fd_sna,r_fd_sa,r_nearEm_DC,r_fd_c,r_FDS,r_FD,Sunlight_SCW,Sunlight_PjSC,Final_W", "번호 ='" + ZoneG[i][0] + "'");
                string[][] envelope = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,면적", "외피유형='" + "외벽" + "' AND 존 ='" + ZoneG[i][0] + "'");
                string[][] envelope2 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,면적", "외피유형='" + "커튼월창" + "' AND 존 ='" + ZoneG[i][0] + "'");
                string[][] envelope3 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,면적", "외피유형='" + "지붕" + "' AND 존 ='" + ZoneG[i][0] + "'");
                string[][] envelope4 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,면적", "외피유형='" + "창호" + "' AND 존 ='" + ZoneG[i][0] + "'");
                string[][] envelope5 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,면적", "외피유형='" + "최하층바닥" + "' AND 존 ='" + ZoneG[i][0] + "'");
                string[][] envelope_1 = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "HT", "직접간접= '직접외기' and 난방_냉방='난방' and 월='1월' and 비이용일_이용일 = '이용일' and 존번호='" + ZoneG[i][0] + "'");
                string[][] envelope_2 = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "HT", "직접간접= '간접외기' and 난방_냉방='난방' and 월='1월' and 비이용일_이용일 = '이용일' and 존번호='" + ZoneG[i][0] + "'");
                string[][] envelope_3 = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "HT", "직접간접= '지면위' and 난방_냉방='난방' and 월='1월' and 비이용일_이용일 = '이용일' and 존번호='" + ZoneG[i][0] + "'");

                items.Add("print9.html"); // 예시 코드: 메인 메뉴 동적 할당

                __data[0].Add(new { idx = i, val = ZoneG[i][8] });
                __data[1].Add(new { idx = i, val = Program.UTIL.asFixed(ZoneG[i][27]) });
                __data[2].Add(new { idx = i, val = Program.UTIL.asFixed(ZoneG[i][9]) }); // 
                __data[3].Add(new { idx = i, val = ZoneG[i][1] });
                __data[4].Add(new { idx = i, val = ZoneG[i][0] });
                __data[5].Add(new { idx = i, val = Program.UTIL.asFixed(ZoneG[i][43]) }); //축열용량
                __data[6].Add(new { idx = i, val = ZoneG[i][10] }); //층고 --> 천장고로 변경
                __data[7].Add(new { idx = i, val = ZoneG[i][9] }); //창면적율 계산 필요
                __data[13].Add(new { idx = i, val = ZoneG[i][46] });
                __data[80].Add(new { idx = i, val = ZoneG[i][46] });

                //W -> kW 
                double annualhneed = 0;
                double annualcneed = 0;

                double hneed = Convert.ToDouble(value[12][104]);
                annualhneed = hneed / 1000;
                __data[14].Add(new { idx = i, val = Program.UTIL.asFixed(annualhneed.ToString()) }); // 연간 난방 에너지 요구량

                double cneed = Convert.ToDouble(value[36][104]);
                annualcneed = cneed / 1000;

                __data[15].Add(new { idx = i, val = Program.UTIL.asFixed(annualcneed.ToString()) }); // 연간 냉방 에너지 요구량
                //data 16, 17, 18 //
                //
                __data[19].Add(new { idx = i, val = Program.UTIL.asFixed(value[12][105]) }); // 난방부하
                __data[20].Add(new { idx = i, val = Program.UTIL.asFixed(value[36][105]) }); // 냉방부하



                __data[69].Add(new { idx = i, val = Program.UTIL.asFixed(value[0][33]) }); //침기횟수
                __data[70].Add(new { idx = i, val = Program.UTIL.asFixed(value[0][34]) }); //환기횟수
                __data[71].Add(new { idx = i, val = Program.UTIL.asFixed(value[0][31]) }); //기계환기횟수

                __data[72].Add(new { idx = i, val = Program.UTIL.asFixed(value[0][36]) }); //침기 열전달계수
                __data[73].Add(new { idx = i, val = Program.UTIL.asFixed(value[0][37]) }); //자연환기 열전달계수
                __data[74].Add(new { idx = i, val = Program.UTIL.asFixed(value[0][39]) }); //기계환기 열전달계수

                ///data 75 부터 면적당 연간난방에너지요구량 // /
                double hneedarea = 0;
                double cneedarea = 0;

                double hloadarea = 0;
                double cloadarea = 0;
                double sinkarea = 0;
                double sourcearea = 0;

                double area = Convert.ToDouble(ZoneG[i][9]);
                double vol = Convert.ToDouble(ZoneG[i][26]);

                double hload = Convert.ToDouble(value[12][105]);
                double cload = Convert.ToDouble(value[36][105]);
                //double sink = Convert.ToDouble(value[12][95]);
                //double source = Convert.ToDouble(value[12][96]);

                hneedarea = hneed / area;
                hneedarea = hneedarea / 1000;
                cneedarea = cneed / area;
                cneedarea = cneedarea / 1000;

                hloadarea = hload / area;
                cloadarea = cload / area;
                //sinkarea = sink / area;
                //sourcearea = source / area;

                __data[76].Add(new { idx = i, val = Program.UTIL.asFixed(hneedarea.ToString()) });
                __data[77].Add(new { idx = i, val = Program.UTIL.asFixed(cneedarea.ToString()) });
                __data[78].Add(new { idx = i, val = Program.UTIL.asFixed(hloadarea.ToString()) });
                __data[79].Add(new { idx = i, val = Program.UTIL.asFixed(cloadarea.ToString()) });
                __data[86].Add(new { idx = i, val = Program.UTIL.asFixed(sinkarea.ToString()) });
                __data[87].Add(new { idx = i, val = Program.UTIL.asFixed(sourcearea.ToString()) });

                double htdtotal = 0;
                double htutotal = 0;
                double htstotal = 0;
                double httotal = 0;

                for (int a = 0; a < envelope_1.Length; a++)
                {

                    double wall = Convert.ToDouble(envelope_1[a][0]);
                    htdtotal += wall;

                }
                for (int a = 0; a < envelope_2.Length; a++)
                {

                    double wall2 = Convert.ToDouble(envelope_2[a][0]);
                    htutotal += wall2;
                }
                for (int a = 0; a < envelope_3.Length; a++)
                {

                    double wall3 = Convert.ToDouble(envelope_3[a][0]);
                    htstotal += wall3;
                }

                httotal = htdtotal + htutotal + htstotal;

                __data[8].Add(new { idx = i, val = Program.UTIL.asFixed(htdtotal.ToString()) });
                __data[9].Add(new { idx = i, val = Program.UTIL.asFixed(htutotal.ToString()) });
                __data[10].Add(new { idx = i, val = Program.UTIL.asFixed(htstotal.ToString()) });
                __data[88].Add(new { idx = i, val = Program.UTIL.asFixed(httotal.ToString()) });

                value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "번호", "번호 ='" + ZoneG[i][0] + "'");
                if (ZoneG[i][0] == value[0][0])
                {
                    n = -1;
                    double qsourcetot = 0;
                    double qsourcedwd = 0;
                    double qsourcedwe = 0;
                    double qsinktot = 0;
                    double qsinkdwd = 0;
                    double qsinkdwe = 0;
                    double qsourcearea = 0;
                    double qsinkarea = 0;




                    while (++n < 12)

                    {
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qsource", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        double qsource1 = Convert.ToDouble(value[0][0]);
                        qsourcedwd += qsource1;
                    }
                    n = -1;
                    while (++n < 12)

                    {
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qsource", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '비이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        double qsource2 = Convert.ToDouble(value[0][0]);
                        qsourcedwe += qsource2;
                    }

                    qsourcetot = qsourcedwd + qsourcedwe;
                    qsourcetot = qsourcetot / 1000;
                    qsourcearea = qsourcetot / area;

                    __data[100].Add(new { idx = i, val = Program.UTIL.asFixed(qsourcetot.ToString()) }); // 열획득량
                    __data[101].Add(new { idx = i, val = Program.UTIL.asFixed(qsourcearea.ToString()) }); //면적당 열획득량
                    ///연간획득량 완료 ///
                    n = -1;
                    while (++n < 12)
                    {
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qsink", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='냉방' AND 월 = '" + (n + 1) + "월'");
                        double qsink1 = Convert.ToDouble(value[0][0]);
                        qsinkdwd += qsink1;
                    }
                    n = -1;
                    while (++n < 12)
                    {
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qsink", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '비이용일' AND 난방_냉방 ='냉방' AND 월 = '" + (n + 1) + "월'");
                        double qsink2 = Convert.ToDouble(value[0][0]);
                        qsinkdwe += qsink2;
                    }
                    qsinktot = qsinkdwd + qsinkdwe;
                    qsinktot = qsinktot / 1000;
                    qsinkarea = qsinktot / area;

                    __data[102].Add(new { idx = i, val = Program.UTIL.asFixed(qsinktot.ToString()) }); // 열손실량
                    __data[103].Add(new { idx = i, val = Program.UTIL.asFixed(qsinkarea.ToString()) }); //면적당 열손실량

                }
                value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "번호", "번호 ='" + ZoneG[i][0] + "'");
                if (ZoneG[i][0] == value[0][0])
                {
                    n = -1;
                    double ventamount = 0;
                    double infamount = 0;
                    double mechamount = 0;

                    double losswin = 0;
                    double lossmech = 0;
                    double lossinf = 0;

                    double losswin2 = 0;
                    double lossmech2 = 0;
                    double lossinf2 = 0;

                    n = -1;
                    while (++n < ZoneG.Length)
                    {
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "nwin", "번호 ='" + ZoneG[i][0] + "'");
                        double nwin = Convert.ToDouble(value[0][0]);
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "ninf", "번호 ='" + ZoneG[i][0] + "'");
                        double ninf = Convert.ToDouble(value[0][0]);
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "nmech", "번호 ='" + ZoneG[i][0] + "'");
                        double nmech = Convert.ToDouble(value[0][0]);

                        ventamount = nwin * vol;
                        infamount = ninf * vol;
                        mechamount = nmech * vol;
                    }
                    __data[11].Add(new { idx = i, val = Program.UTIL.asFixed(ventamount.ToString()) });
                    __data[81].Add(new { idx = i, val = Program.UTIL.asFixed(infamount.ToString()) });
                    __data[82].Add(new { idx = i, val = Program.UTIL.asFixed(mechamount.ToString()) });

                    n = -1;
                    while (++n < 12)
                    {
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QV_win_sink", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        double losswinmth = Convert.ToDouble(value[0][0]);
                        losswin += losswinmth;
                        losswin2 = losswin / 1000;

                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QV_inf_sink", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        double lossinfmth = Convert.ToDouble(value[0][0]);
                        lossinf += lossinfmth;
                        lossinf2 = lossinf / 1000;

                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QV_mech_sink", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        double lossmechmth = Convert.ToDouble(value[0][0]);
                        lossmech += lossmechmth;
                        lossmech2 = lossmech / 1000;
                    }
                    __data[83].Add(new { idx = i, val = Program.UTIL.asFixed(losswin2.ToString()) });
                    __data[84].Add(new { idx = i, val = Program.UTIL.asFixed(lossinf2.ToString()) });
                    __data[85].Add(new { idx = i, val = Program.UTIL.asFixed(lossmech2.ToString()) });

                }

                value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "번호", "번호 ='" + ZoneG[i][0] + "'");

                if (ZoneG[i][0] == value[0][0])
                {
                    n = -1;

                    while (++n < 12)
                    {
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "dwd_mth", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[23].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 이용일
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_mth", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '비이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[50].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 비이용일 난방요구량
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "dQc_b", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '비이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[51].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 비이용일 대차축열량
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QTsink_tot", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '비이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[52].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 비이용일 관류열손실량
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QVsink_tot", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '비이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[53].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 비이용일 환기열손실량
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QI_tot", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '비이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[55].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 비이용일 내부발열량

                    }

                    n = -1;

                    while (++n < 12)
                    {

                        String[][] value_난방요구량 = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_mth", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        string 면작당난방요구 = (Convert.ToDouble(value_난방요구량[0][0]) / area).ToString();
                        __data[90].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(면작당난방요구) }); // 일사열획득량 (QSTR + SSOPSOURCE TOTAL) 
                        ///
                        String[][] value_냉방요구량 = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_mth", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='냉방' AND 월 = '" + (n + 1) + "월'");
                        string 면적당냉방요구 = (Convert.ToDouble(value_냉방요구량[0][0]) / area).ToString();
                        __data[91].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(면적당냉방요구) }); // 일사열획득량 (QSTR + SSOPSOURCE TOTAL) 

                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_mth", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[24].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 난방요구량

                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "theta_i", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[25].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 실내온도
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "theta_e", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[26].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 실외온도
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "eta", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[27].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 이용계수
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QTsink_tot", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[28].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 관류 열손실량
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QTsink_Wall", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[29].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 관류(벽체) 열손실량
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QTsink_Win", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[30].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 관류(창호) 열손실량
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QTsink_CW", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[31].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 관류(커튼월) 열손실량
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QTsink_Roof", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[32].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 관류(지붕) 열손실량
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QTsink_Door", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[33].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 관류(출입문) 열손실량
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QTsink_Floor", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[34].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 관류(바닥) 열손실량 
                        ///////data 35  --->  열교 해당 data
                        ///
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QVsink_tot", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[36].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 환기 열손실량 
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QV_inf_sink", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[37].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 침기 열손실량 
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QV_mech_sink", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[38].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 기계환기 열손실량 
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QV_win_sink", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[39].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 환기 (자연환기)열손실량 


                        ///
                        String[][] value_투명2 = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QStr_tot", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        string[][] value_불투명2 = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QSopsource_tot", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        string 일사열획득난방 = (Convert.ToDouble(value_투명2[0][0]) + Convert.ToDouble(value_불투명2[0][0])).ToString();
                        __data[40].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(일사열획득난방) }); // 일사열획득량 (QSTR + SSOPSOURCE TOTAL) 
                        ///

                        ///
                        String[][] value_투명3 = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QStr_tot", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '비용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        string[][] value_불투명3 = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QSopsource_tot", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '비용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        string 일사열획득난방비용일 = (Convert.ToDouble(value_투명2[0][0]) + Convert.ToDouble(value_불투명2[0][0])).ToString();
                        __data[54].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(일사열획득난방비용일) }); // 일사열획득량 (QSTR + SSOPSOURCE TOTAL) 
                        ///

                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QStr_Win", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[41].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 일사(창호)열획득량
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QStr_CW", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[42].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 일사(커튼월창)열획득량
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QSopsink_Wall", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[43].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 일사(외벽)열획득량
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QSopsink_Roof", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[44].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 일사(지붕)열획득량
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QSopsink_Door", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[45].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 일사(지붕)열획득량
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QI_tot", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[46].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 내부발열량
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QI_L", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[47].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 내부발열량(조명)
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QI_P", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[48].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 내부발열량(인체)
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QI_fac", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[49].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 내부발열량(기계)


                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_mth", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='냉방' AND 월 = '" + (n + 1) + "월'");
                        __data[57].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 이용일 냉방요구량
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_DHU_tot", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='냉방' AND 월 = '" + (n + 1) + "월'");
                        __data[58].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 제습요구량

                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "theta_i", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='냉방' AND 월 = '" + (n + 1) + "월'");
                        __data[59].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 냉방기준온도
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QTsource_tot", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='냉방' AND 월 = '" + (n + 1) + "월'");
                        __data[60].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 관류열획득량
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QVsource_tot", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='냉방' AND 월 = '" + (n + 1) + "월'");
                        __data[61].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 환기열획득량
                        ///
                        String[][] value_투명 = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QStr_tot", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='냉방' AND 월 = '" + (n + 1) + "월'");
                        string[][] value_불투명 = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "QSopsource_tot", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='냉방' AND 월 = '" + (n + 1) + "월'");
                        string 합계 = (Convert.ToDouble(value_투명[0][0]) + Convert.ToDouble(value_불투명[0][0])).ToString();
                        __data[62].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(합계) }); // 일사열획득량 (QSTR + SSOPSOURCE TOTAL) 
                        ///
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_mth", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '비이용일' AND 난방_냉방 ='냉방' AND 월 = '" + (n + 1) + "월'");
                        __data[63].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 비이용일 냉방요구량
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "theta_i", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '비이용일' AND 난방_냉방 ='냉방' AND 월 = '" + (n + 1) + "월'");
                        __data[64].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 비이용일 냉방기준온도
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[65].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 최대난방부하
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "t_max", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        __data[66].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 난방시간
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='냉방' AND 월 = '" + (n + 1) + "월'");
                        __data[67].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 최대난방부하
                        value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "t_max", "번호 ='" + ZoneG[i][0] + "' AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='냉방' AND 월 = '" + (n + 1) + "월'");
                        __data[68].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[0][0]) }); // 난방시간


                    }
                }
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            //List<object> _data = new List<object>();

            List<object> _data78 = new List<object>();


            List<object> _data590 = new List<object>();
            List<object> _data591 = new List<object>();
            List<object> _data592 = new List<object>();
            List<object> _data593 = new List<object>();
            List<object> _data594 = new List<object>();
            List<object> _data595 = new List<object>();
            List<object> _data596 = new List<object>();
            List<object> _data597 = new List<object>();
            List<object> _data598 = new List<object>();
            List<object> _data599 = new List<object>();
            List<object> _data600 = new List<object>();
            List<object> _data601 = new List<object>();
            List<object> _data602 = new List<object>();
            List<object> _data603 = new List<object>();
            List<object> _data604 = new List<object>();
            List<object> _data605 = new List<object>();
            List<object> _data606 = new List<object>();
            List<object> _data607 = new List<object>();
            List<object> _data608 = new List<object>();
            List<object> _data609 = new List<object>();
            List<object> _data610 = new List<object>();
            List<object> _data611 = new List<object>();
            List<object> _data612 = new List<object>();
            List<object> _data613 = new List<object>();
            List<object> _data614 = new List<object>();
            List<object> _data615 = new List<object>();
            List<object> _data616 = new List<object>();
            List<object> _data617 = new List<object>();
            List<object> _data618 = new List<object>();
            List<object> _data619 = new List<object>();
            List<object> _data620 = new List<object>();
            List<object> _data621 = new List<object>();
            List<object> _data622 = new List<object>();
            List<object> _data623 = new List<object>();
            List<object> _data624 = new List<object>();
            List<object> _data625 = new List<object>();
            List<object> _data626 = new List<object>();
            List<object> _data627 = new List<object>();
            List<object> _data628 = new List<object>();
            List<object> _data629 = new List<object>();
            List<object> _data630 = new List<object>();
            List<object> _data631 = new List<object>();
            List<object> _data632 = new List<object>();
            List<object> _data633 = new List<object>();
            List<object> _data634 = new List<object>();
            List<object> _data635 = new List<object>();
            List<object> _data636 = new List<object>();
            List<object> _data637 = new List<object>();
            List<object> _data638 = new List<object>();
            List<object> _data639 = new List<object>();
            List<object> _data640 = new List<object>();





            i = -1;

            while (++i < ZoneG.Length)
            {
                
                string[][] value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "번호,이름,난방_냉방,비이용일_이용일,월,HT_tot,HT_InWall,HT_Slab,HT_Wall,HT_Roof,HT_Floor,HT_GWall,HT_Door,HT_Win,HT_CW,HT_Di_Wall,HT_Indi_Wall,HT_Di_Roof,HT_Indi_Roof,HT_Di_Win,HT_Indi_Win,HT_Di_Door,	HT_Indi_Door,HT_TB_tot,HT_TB_Wall,HT_TB_Roof,HT_TB_Floor,HT_TB_Gwall,HT_TB_Win,HT_TB_Door,HT_TB_CW,nmech	,nz,ninf,nwin,HV_tot,HV_inf,HV_win,HV_z,HV_mech,H_tot,tao,dwe_mth,dwd_mth,theta_i,theta_e,QTsink_tot,QT_u_sink,QTsink_Wall,QTsink_Roof,QTsink_Floor,QTsink_GWall,QTsink_Door,QTsink_Win,QTsink_CW,QTsource_tot,QT_u_source,QTsource_Wall,QTsource_Roof,QTsource_Floor,QTsource_GWall,QTsource_Door,QTsource_Win,QTsource_CW,QSopsink_tot,QSopsource_tot,QStr_tot,QSopsink_Wall,QSopsink_Roof,QSopsink_Door,QSopsink_CW_p,QSopsource_Wall,QSopsource_Roof,QSopsource_Door,QSopsource_CW_p,QStr_Win,QStr_CW,QVsink_tot,QV_inf_sink,QV_win_sink,QV_z_sink,QV_mech_sink,QVsource_tot,QV_inf_source,QV_win_source,QV_z_source,QV_mech_source,Q_DHU_win,Q_DHU_mech,Q_DHU_tot,QI_tot,QI_L,QI_P,QI_fac,QI_Humidity,Qsink,Qsource,gamma,a,eta,dQc_b,dQc_sink,Qb_day,Qb_mth,Qb_a,Q_max,t_max,비냉난방존온도", "번호 ='" + ZoneG[i][0] + "'");
                string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "Zone_LightResult", "번호,ITr,IRD,ISh_Ish,ISh_hA,Ish_vA,Ish_In_At,Wi,Ish_GDF,Ish,f_τeff_SNA,f_D,f_nearD,f_DCA,f_dclass,f_nearEm_SNA,f_fd_sna,f_fd_sa,f_nearEm_DC,f_fd_c,f_FDS,f_FD,as_bs,hs_bs,hg_hw,normal_ηR,saw_ηR,r_DSNA,r_DSA,r_dclass,r_nearEm_FDS,r_fd_sna,r_fd_sa,r_nearEm_DC,r_fd_c,r_FDS,r_FD,Sunlight_SCW,Sunlight_PjSC,Final_W", "번호 ='" + ZoneG[i][0] + "'");
                string[][] envelope = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,면적", "외피유형='" + "외벽" + "' AND 존 ='" + ZoneG[i][0] + "'");
                string[][] envelope2 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,면적", "외피유형='" + "커튼월창" + "' AND 존 ='" + ZoneG[i][0] + "'");
                string[][] envelope3 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,면적", "외피유형='" + "지붕" + "' AND 존 ='" + ZoneG[i][0] + "'");
                string[][] envelope4 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,면적", "외피유형='" + "창호" + "' AND 존 ='" + ZoneG[i][0] + "'");
                string[][] envelope5 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,면적", "외피유형='" + "최하층바닥" + "' AND 존 ='" + ZoneG[i][0] + "'");
                //string[][] envelope6 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,면적", "외피유형='" + "출입문" + "' AND 존 ='" + ZoneG[i][0] + "'");

                string[][] envelope_1 = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "HT", "직접간접= '직접외기' and 난방_냉방='난방' and 월='1월' and 비이용일_이용일 = '이용일' and 존번호='" + ZoneG[i][0] + "'");
                string[][] envelope_2 = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "HT", "직접간접= '간접외기' and 난방_냉방='난방' and 월='1월' and 비이용일_이용일 = '이용일' and 존번호='" + ZoneG[i][0] + "'");
                string[][] envelope_3 = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "HT", "직접간접= '지면위' and 난방_냉방='난방' and 월='1월' and 비이용일_이용일 = '이용일' and 존번호='" + ZoneG[i][0] + "'");
                if (ZoneG[i][0] == value[12][0])
                {
                    n = -1;

                    while (++n < 12)
                    { }

                    double sum = 0;
                    double kkk;
                    for (int k = 0; k < 12; k++)
                    {
                        kkk = Convert.ToDouble(value[12 + k][92]);//theta (이용일 난방요구량_합산)
                        sum += kkk;//theta (이용일 난방요구량_합산)

                    }

                    _data78.Add(new { idx = i, val = sum });

                    //MessageBox.Show(sum.ToString());

                }
                else;

                //조명 요구량 
                //if (ZoneG[i][0] == value2[12 * i][0])
                //{
                //    n = -1;

                //    while (++n < 12)
                //    {
                //        __data[569].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value2[i * 12 + n][39]) });
                //    }

                //    double sum = 0;
                //    double kkkk;
                //    double lneedarea = 0;
                //    for (int k = 0; k < 12; k++)
                //    {
                //        kkkk = Convert.ToDouble(value2[12 * i + k][39]);//Finfal_W (조명에너지 요구량 합산)
                //        sum += kkkk;// Finfal_W (조명에너지 요구량 합산)
                //                    //MessageBox.Show(sum.ToString());

                //        double area = Convert.ToDouble(ZoneG[i][9]);
                //        lneedarea = sum / area;

                //    }
                //    //string result;
                //    //result = string.Format("{0:0.#0}", sum);
                //    //_data589.Add(new { idx = i, val = result });


                //    __data[589].Add(new { idx = i, val = Program.UTIL.asFixed(sum.ToString()) });
                //    __data[590].Add(new { idx = i, val = Program.UTIL.asFixed(lneedarea.ToString()) });
                //    //MessageBox.Show(sum.ToString());

                //}
                //else;

                int kk = -1;
                double totalwall = 0;
                double totalcw = 0;
                double totalroof = 0;
                double totalwin = 0;
                double totalfloor = 0;
                double winwallrate = 0;

                envelope = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "존 ='" + ZoneG[i][0] + "' AND 외피유형 = '외벽'");
                while (++kk < envelope.Length)
                {
                    double wall = Convert.ToDouble(envelope[kk][0]);
                    totalwall += wall;
                }
                kk = -1;
                envelope2 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "존 ='" + ZoneG[i][0] + "' AND 외피유형 = '커튼월'");
                while (++kk < envelope2.Length)
                {
                    double cw = Convert.ToDouble(envelope2[kk][0]);
                    totalcw += cw;
                }
                kk = -1;

                envelope3 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "존 ='" + ZoneG[i][0] + "' AND 외피유형 = '지붕'");
                while (++kk < envelope3.Length)
                {
                    double roof = Convert.ToDouble(envelope3[kk][0]);
                    totalroof += roof;
                }
                kk = -1;

                envelope4 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "존 ='" + ZoneG[i][0] + "' AND 외피유형 = '창호'");
                while (++kk < envelope4.Length)
                {
                    double win = Convert.ToDouble(envelope4[kk][0]);
                    totalwin += win;
                }
                kk = -1;

                envelope5 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "존 ='" + ZoneG[i][0] + "' AND 외피유형 = '최하층바닥'");
                while (++kk < envelope5.Length)
                {
                    double floor = Convert.ToDouble(envelope5[kk][0]);
                    totalfloor += floor;
                }

                winwallrate = (totalwin / totalwall) * 100;


                _data590.Add(new { idx = i, val = Program.UTIL.asFixed(totalwall.ToString()) }); //벽체 면적
                _data591.Add(new { idx = i, val = Program.UTIL.asFixed(totalcw.ToString()) });  //커튼월 면적
                _data592.Add(new { idx = i, val = Program.UTIL.asFixed(totalroof.ToString()) }); //지붕 면적
                _data593.Add(new { idx = i, val = Program.UTIL.asFixed(totalwin.ToString()) });  //창호 면적
                _data594.Add(new { idx = i, val = Program.UTIL.asFixed(totalfloor.ToString()) });  //바닥 면적
                _data596.Add(new { idx = i, val = Program.UTIL.asFixed(winwallrate.ToString()) });  //바닥 면적  
                //열관류율(벽체) 계산 파트
                kk = -1;
                double ht = 0;
                double htarea = 0;
                double httb = 0;
                String[][] uenvelope = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "외피번호,존번호,구조체번호,외피유형,커튼월유형,직접간접,난방_냉방,비이용일_이용일,월,HT,HT_TB,QTsink,QTsource,QT_TB_sink,QT_TB_source,QTsink_tot,QTsource_tot,QSsink,QSsource", "존번호='" + ZoneG[i][0] + "'");
                uenvelope = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "HT", "존번호 ='" + ZoneG[i][0] + "' AND 외피유형 = '외벽'AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + "1월'");
                n = -1;
                while (++n < uenvelope.Length)
                {
                    double ht1 = Convert.ToDouble(uenvelope[n][0]);
                    ht += ht1;
                    htarea = ht / totalwall;
                }
                double num = Convert.ToDouble(uenvelope.Length);


                ht = ht / num;
                htarea = htarea / num;
                _data605.Add(new { idx = i, val = Program.UTIL.asFixed(ht.ToString()) });
                _data595.Add(new { idx = i, val = Program.UTIL.asFixed(htarea.ToString()) });

                n = -1;

                while (++n < uenvelope.Length)
                {
                    uenvelope = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "HT_TB", "존번호 ='" + ZoneG[i][0] + "' AND 외피유형 = '외벽'AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + "1월'");
                    double httb1 = Convert.ToDouble(uenvelope[n][0]);
                    httb += httb1;
                }
                httb = httb / totalwall;
                httb = httb / num;
                _data606.Add(new { idx = i, val = Program.UTIL.asFixed(httb.ToString()) });

                n = -1;
                double qt = 0;
                while (++n < 12)
                {
                    uenvelope = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "QTsink", "존번호 ='" + ZoneG[i][0] + "' AND 외피유형 = '외벽'AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                    double qt1 = Convert.ToDouble(uenvelope[0][0]);
                    qt += qt1;
                    qt = qt / 1000;
                }
                _data607.Add(new { idx = i, val = Program.UTIL.asFixed(qt.ToString()) });

                //열관류율(지붕) 계산 파트
                double htr = 0;
                double htrarea = 0;

                uenvelope = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "HT", "존번호 ='" + ZoneG[i][0] + "' AND 외피유형 = '지붕'AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + "1월'");
                n = -1;
                if (uenvelope.Length > 0)
                {
                    while (++n < uenvelope.Length)
                    {
                        double htr1 = Convert.ToDouble(uenvelope[n][0]);
                        htr += htr1;
                        htrarea = htr / totalroof;
                    }
                    double numrf = Convert.ToDouble(uenvelope.Length);
                    htr = htr / numrf;
                    htrarea = htrarea / numrf;
                    _data611.Add(new { idx = i, val = Program.UTIL.asFixed(htr.ToString()) });
                    _data612.Add(new { idx = i, val = Program.UTIL.asFixed(htrarea.ToString()) });

                }
                else
                {

                };
                n = -1;
                double httbrf = 0;

                uenvelope = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "HT_TB", "존번호 ='" + ZoneG[i][0] + "' AND 외피유형 = '지붕'AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + "1월'");
                while (++n < uenvelope.Length)
                {
                    double httb1 = Convert.ToDouble(uenvelope[n][0]);
                    httbrf += httb1;
                    httbrf = httbrf / totalroof;
                    double numrf = Convert.ToDouble(uenvelope.Length);
                    httbrf = httbrf / numrf;
                }
                _data613.Add(new { idx = i, val = Program.UTIL.asFixed(httbrf.ToString()) });

                double qtroof = 0;
                n = -1;
                while (++n < uenvelope.Length)
                {
                    uenvelope = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "QTsink", "존번호 ='" + ZoneG[i][0] + "' AND 외피유형 = '지붕'AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                    double qt1 = Convert.ToDouble(uenvelope[0][0]);
                    qtroof += qt1;
                    qtroof = qtroof / 1000;
                }
                _data614.Add(new { idx = i, val = Program.UTIL.asFixed(qtroof.ToString()) });

                //열관류율(바닥) 계산 파트 
                double htf = 0;
                double htfarea = 0;

                uenvelope = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "HT", "존번호 ='" + ZoneG[i][0] + "' AND 외피유형 = '최하층바닥'AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + "1월'");
                n = -1;
                if (uenvelope.Length > 0)
                {
                    while (++n < uenvelope.Length)
                    {
                        double htf1 = Convert.ToDouble(uenvelope[n][0]);
                        htf += htf1;
                        htfarea = htf / totalfloor;
                    }
                    double numf2 = Convert.ToDouble(uenvelope.Length);
                    htf = htf / numf2;
                    htfarea = htfarea / numf2;
                    _data615.Add(new { idx = i, val = Program.UTIL.asFixed(htf.ToString()) });
                    _data616.Add(new { idx = i, val = Program.UTIL.asFixed(htfarea.ToString()) });
                }
                else
                {

                }

                uenvelope = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "HT_TB", "존번호 ='" + ZoneG[i][0] + "' AND 외피유형 = '최하층바닥'AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + "1월'");
                n = -1;
                double httbf = 0;
                if (uenvelope.Length > 0)
                {

                    while (++n < uenvelope.Length)
                    {
                        double httb1 = Convert.ToDouble(uenvelope[0][0]);
                        httbf += httb1;
                        httbf = httbf / totalfloor;
                        double numtb = Convert.ToDouble(uenvelope.Length);
                        httbf = httbf / numtb;
                    }
                    _data617.Add(new { idx = i, val = Program.UTIL.asFixed(httbf.ToString()) });
                }
                else
                {
                }
                double qtfloor = 0;
                n = -1;
                while (++n < uenvelope.Length)
                {
                    uenvelope = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "QTsink", "존번호 ='" + ZoneG[i][0] + "' AND 외피유형 = '최하층바닥'AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                    double qtfloor1 = Convert.ToDouble(uenvelope[0][0]);
                    qtfloor += qtfloor1;
                    qtfloor = qtfloor / 1000;
                }
                _data618.Add(new { idx = i, val = Program.UTIL.asFixed(qtfloor.ToString()) });

                //열관류율(창호) 계산 파트
                double htw = 0;
                double htwarea = 0;
                uenvelope = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "HT", "존번호 ='" + ZoneG[i][0] + "' AND 외피유형 = '창호'AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + "1월'");
                n = -1;
                if (uenvelope.Length > 0)
                {
                    while (++n < uenvelope.Length)
                    {
                        double htr1 = Convert.ToDouble(uenvelope[n][0]);
                        htw += htr1;
                        htwarea = htw / totalwin;
                        double numw = Convert.ToDouble(uenvelope.Length);

                        htw = htw / numw;
                        htwarea = htwarea / numw;
                    }

                    _data619.Add(new { idx = i, val = Program.UTIL.asFixed(htw.ToString()) });
                    _data620.Add(new { idx = i, val = Program.UTIL.asFixed(htwarea.ToString()) });
                }
                else
                {

                };
                n = -1;
                double httbw = 0;
                if (uenvelope.Length > 0)
                {
                    uenvelope = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "HT_TB", "존번호 ='" + ZoneG[i][0] + "' AND 외피유형 = '창호'AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + "1월'");
                    while (++n < uenvelope.Length)
                    {
                        double httb1 = Convert.ToDouble(uenvelope[n][0]);
                        httbw += httb1;
                        httbw = httbw / totalwin;
                        double numw = Convert.ToDouble(uenvelope.Length);
                        httbw = httbw / numw;
                    }
                    _data621.Add(new { idx = i, val = Program.UTIL.asFixed(httbw.ToString()) });
                }
                else
                {
                }
                double qtwin = 0;
                n = -1;
                while (++n < uenvelope.Length)
                {
                    uenvelope = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "QTsink", "존번호 ='" + ZoneG[i][0] + "' AND 외피유형 = '창호'AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                    double qtwin1 = Convert.ToDouble(uenvelope[n][0]);
                    qtwin += qtwin1;
                    qtwin = qtwin1 / 1000;
                }
                _data622.Add(new { idx = i, val = Program.UTIL.asFixed(qtwin.ToString()) });

                //커튼월 관류 열손실 계산 파트
                double htcw = 0;
                double htcwarea = 0;
                uenvelope = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "HT", "존번호 ='" + ZoneG[i][0] + "' AND 외피유형 = '커튼월'AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + "1월'");
                n = -1;
                if (uenvelope.Length > 0)
                {
                    while (++n < uenvelope.Length)
                    {
                        double htr1 = Convert.ToDouble(uenvelope[n][0]);
                        htcw += htr1;
                        htcwarea = htcw / totalcw;
                    }
                    double numcw = Convert.ToDouble(uenvelope.Length);
                    htcw = htcw / numcw;
                    htcwarea = htcwarea / numcw;
                    _data624.Add(new { idx = i, val = Program.UTIL.asFixed(htcw.ToString()) });
                    _data625.Add(new { idx = i, val = Program.UTIL.asFixed(htcwarea.ToString()) });
                }
                else
                {

                };
                n = -1;
                double httbcw = 0;
                uenvelope = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "HT_TB", "존번호 ='" + ZoneG[i][0] + "' AND 외피유형 = '커튼월'AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + "1월'");
                if (uenvelope.Length > 0)
                {
                    while (++n < uenvelope.Length)
                    {
                        double httb1 = Convert.ToDouble(uenvelope[n][0]);
                        httbcw += httb1;
                        httbcw = httbcw / totalcw;
                        double numcw = Convert.ToDouble(uenvelope.Length);
                        httbcw = httbcw / numcw;
                    }
                    _data626.Add(new { idx = i, val = Program.UTIL.asFixed(httbcw.ToString()) });

                    double qtcw = 0;
                    n = -1;
                    while (++n < uenvelope.Length)
                    {
                        uenvelope = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "QTsink", "존번호 ='" + ZoneG[i][0] + "' AND 외피유형 = '커튼월'AND 비이용일_이용일 = '이용일' AND 난방_냉방 ='난방' AND 월 = '" + (n + 1) + "월'");
                        double qt1 = Convert.ToDouble(uenvelope[0][0]);
                        qtcw += qt1;
                        qtcw = qtcw / 1000;
                    }
                    _data627.Add(new { idx = i, val = Program.UTIL.asFixed(qtcw.ToString()) });
                }
            }


            ////////////////////////////////////////////////////////////////////
            data.Add(new { cname = "cls-profile-name", data = __data[0] });
            data.Add(new { cname = "cls-volume", data = __data[1] });
            data.Add(new { cname = "cls-zone-anf", data = __data[2] });
            data.Add(new { cname = "cls-sub-name", data = __data[3] });
            data.Add(new { cname = "cls-zone-name", data = __data[4] });
            data.Add(new { cname = "cls-cwirk", data = __data[5] });
            data.Add(new { cname = "cls-floor-level", data = __data[6] });
            data.Add(new { cname = "cls-window-ratio", data = _data596 });
            data.Add(new { cname = "cls-zone-ht", data = __data[88] });
            data.Add(new { cname = "cls-htd", data = __data[8] });
            data.Add(new { cname = "cls-htu", data = __data[9] });
            data.Add(new { cname = "cls-hts", data = __data[10] });

            data.Add(new { cname = "cls-airtight", data = __data[13] });
            data.Add(new { cname = "cls-airtight2", data = __data[80] });
            data.Add(new { cname = "cls-hneed-annual", data = __data[14] });
            data.Add(new { cname = "cls-hneed-annual_area", data = __data[76] });
            data.Add(new { cname = "cls-cneed-annual", data = __data[15] });
            data.Add(new { cname = "cls-cneed-annual_area", data = __data[77] });
            data.Add(new { cname = "cls-hload-max", data = __data[19] });
            data.Add(new { cname = "cls-hload-max_area", data = __data[78] });
            data.Add(new { cname = "cls-cload1-max", data = __data[20] });
            data.Add(new { cname = "cls-cload1-max_area", data = __data[79] });
            data.Add(new { cname = "cls-hloss-annual", data = __data[102] });
            data.Add(new { cname = "cls-hgain-annual", data = __data[100] });
            data.Add(new { cname = "cls-hloss-annual_area", data = __data[103] });
            data.Add(new { cname = "cls-hgain-annual_area", data = __data[101] });


            data.Add(new { cname = "cls-hneed-wd", data = __data[24] });
            data.Add(new { cname = "cls-hneed-wd-area", data = __data[90] });
            data.Add(new { cname = "cls-day-wd", data = __data[23] });
            data.Add(new { cname = "cls-htemp", data = __data[25] });
            data.Add(new { cname = "cls-temp-wd", data = __data[26] });
            data.Add(new { cname = "cls-eta-wd", data = __data[27] });
            data.Add(new { cname = "cls-lossqt-wd", data = __data[28] });
            data.Add(new { cname = "cls-losswall-wd", data = __data[29] });
            data.Add(new { cname = "cls-losswin-wd", data = __data[30] });
            data.Add(new { cname = "cls-losscw-wd", data = __data[31] });
            data.Add(new { cname = "cls-lossroof-wd", data = __data[32] });
            data.Add(new { cname = "cls-lossdoor-wd", data = __data[33] });
            data.Add(new { cname = "cls-lossfloor-wd", data = __data[34] });
            data.Add(new { cname = "cls-lossvent-wd", data = __data[36] });
            data.Add(new { cname = "cls-lossinf-wd", data = __data[37] });
            data.Add(new { cname = "cls-lossmech-wd", data = __data[38] });
            data.Add(new { cname = "cls-losswind-wd", data = __data[39] });
            data.Add(new { cname = "cls-qstr-tot-wd", data = __data[40] });
            data.Add(new { cname = "cls-qstrwin-wd", data = __data[41] });
            data.Add(new { cname = "cls-qstrcw-wd", data = __data[42] });
            data.Add(new { cname = "cls-qswall-wd", data = __data[43] });
            data.Add(new { cname = "cls-qsroof-wd", data = __data[44] });
            data.Add(new { cname = "cls-qstrdoor-wd", data = __data[45] });
            data.Add(new { cname = "cls-indoor-wd", data = __data[46] });
            data.Add(new { cname = "cls-inlight-wd", data = __data[47] });
            data.Add(new { cname = "cls-inp-wd", data = __data[48] });
            data.Add(new { cname = "cls-ine-wd", data = __data[49] });
            data.Add(new { cname = "cls-hneed-we", data = __data[50] });
            data.Add(new { cname = "cls-lossqt-we", data = __data[51] });
            data.Add(new { cname = "cls-lossven-we", data = __data[52] });
            data.Add(new { cname = "cls-qstr-we", data = __data[53] });
            data.Add(new { cname = "cls-indoor-we", data = __data[54] });
            //data.Add(new { cname = "cls-cneed2-wd", data = __data[56] });
            data.Add(new { cname = "cls-cneed2-wd", data = __data[57] });
            data.Add(new { cname = "cls-cneed-wd", data = __data[91] });
            data.Add(new { cname = "cls-dneed-wd", data = __data[58] });
            data.Add(new { cname = "cls-ctemp-wd", data = __data[59] });
            data.Add(new { cname = "cls-qtsource-wd", data = __data[60] });
            data.Add(new { cname = "cls-qvsource-wd", data = __data[61] });
            data.Add(new { cname = "cls-qssource-wd", data = __data[62] });
            data.Add(new { cname = "cls-cneed-we", data = __data[63] });
            data.Add(new { cname = "cls-ctemp-we", data = __data[64] });
            data.Add(new { cname = "cls-hload-max", data = __data[65] });
            data.Add(new { cname = "cls-htime", data = __data[66] });
            data.Add(new { cname = "cls-cload-max", data = __data[67] });
            data.Add(new { cname = "cls-ctime", data = __data[68] });
            data.Add(new { cname = "cls-lneed", data = __data[569] });
            data.Add(new { cname = "cls-lneed-sum", data = __data[589] });
            data.Add(new { cname = "cls-lneed-annual", data = __data[589] });
            data.Add(new { cname = "cls-lneed-annual_area", data = __data[590] });

            //중간부분
            data.Add(new { cname = "cls-wall-area", data = _data590 });
            data.Add(new { cname = "cls-cwall-area", data = _data591 });
            data.Add(new { cname = "cls-roof-area", data = _data592 });
            data.Add(new { cname = "cls-window-area2", data = _data593 });
            data.Add(new { cname = "cls-floor-area", data = _data594 });

            data.Add(new { cname = "cls-wall-u", data = _data595 });
            data.Add(new { cname = "cls-wall-1d", data = _data606 });
            data.Add(new { cname = "cls-wall-hloss", data = _data607 });

            data.Add(new { cname = "cls-cwall-u", data = _data625 });
            data.Add(new { cname = "cls-cwall-1d", data = _data626 });
            data.Add(new { cname = "cls-cwall-hloss", data = _data627 });

            data.Add(new { cname = "cls-roof-u", data = _data612 });
            data.Add(new { cname = "cls-roof-1d", data = _data613 });
            data.Add(new { cname = "cls-roof-hloss", data = _data614 });

            data.Add(new { cname = "cls-window-u", data = _data620 });
            data.Add(new { cname = "cls-window-h", data = _data619 });
            data.Add(new { cname = "cls-window-1d", data = _data621 });
            data.Add(new { cname = "cls-window-hloss", data = _data622 });

            data.Add(new { cname = "cls-floor-u", data = _data616 });
            data.Add(new { cname = "cls-floor-h", data = _data615 });
            data.Add(new { cname = "cls-floor-1d", data = _data617 });
            data.Add(new { cname = "cls-floor-hloss", data = _data618 });

            data.Add(new { cname = "cls-ninf", data = __data[69] });
            data.Add(new { cname = "cls-nmech", data = __data[71] });
            data.Add(new { cname = "cls-nvent", data = __data[70] });

            data.Add(new { cname = "cls-vent-amount", data = __data[11] });
            data.Add(new { cname = "cls-inf-amount", data = __data[81] });
            data.Add(new { cname = "cls-nmech-amount", data = __data[82] });

            data.Add(new { cname = "cls-hinf", data = __data[72] });
            data.Add(new { cname = "cls-hvent", data = __data[73] });
            data.Add(new { cname = "cls-hmech", data = __data[74] });

            data.Add(new { cname = "cls-qvent", data = __data[83] });
            data.Add(new { cname = "cls-qmech", data = __data[85] });
            data.Add(new { cname = "cls-qinf", data = __data[84] });

            //현재 열관류율 시트가 따로없음
            data.Add(new { cname = "cls-wall-ueff", data = _data595 });
            data.Add(new { cname = "cls-cwall-ueff", data = _data625 });
            data.Add(new { cname = "cls-roof-ueff", data = _data612 });
            data.Add(new { cname = "cls-window-ueff", data = _data620 });
            data.Add(new { cname = "cls-floor-ueff", data = _data616 });

            data.Add(new { cname = "cls-wall-h", data = _data605 });
            data.Add(new { cname = "cls-cwall-h", data = _data624 });
            data.Add(new { cname = "cls-roof-h", data = _data611 });
            data.Add(new { cname = "cls-window-h", data = _data619 });


            data.Add(new { cname = "cls-window-area", data = _data593 });
            data.Add(new { cname = "cls-solar-wallarea", data = _data590 });
            data.Add(new { cname = "cls-solar-roofarea", data = _data592 });


            //data.Add(new { cname = "cls-solar-doorarea", data =  });




            s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
            s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());

            runScript("init(" + s + "," + s2 + ")");

            }
            catch { }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            webView21.CoreWebView2.ShowPrintUI();
        }
    }
}