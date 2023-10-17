using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.contents.Result
{
    public partial class PrintReport : Form
    {
        bool scriptable = false;
        public PrintReport()
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

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            string s, s2;
            string[][] ZoneG = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,존이름,실제어방식,냉난방유무,환기유무,환기방식,온도교환효율,전열교환효율,용도프로필,천장고,시작시간,종료시간,주이용일,재실자수,기기발열수준,일일급탕요구량,냉난방시간,사용시간,공조시간,연이용일수,재실밀도,재실수준,일일인체발열,면적당인체발열,일일기기발열,면적당기기발열,순체적,환기횟수,이용일환기량,비이용일환기량,천장축열선택,외벽축열선택,내벽축열선택,바닥축열선택,천장축열,외벽축열,내벽축열,바닥축열,천장면적,외벽면적,내벽면적,바닥면적,존축열성능,존기밀타입,기밀적용유형,q50,n50");
            string[][] value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "번호,이름,난방_냉방,비이용일_이용일,월,HT_tot,HT_InWall,HT_Slab,HT_Wall,HT_Roof,HT_Floor,HT_GWall,HT_Door,HT_Win,HT_CW,HT_Di_Wall,HT_Indi_Wall,HT_Di_Roof,HT_Indi_Roof,HT_Di_Win,HT_Indi_Win,HT_Di_Door,	HT_Indi_Door,HT_TB_tot,HT_TB_Wall,HT_TB_Roof,HT_TB_Floor,HT_TB_Gwall,HT_TB_Win,HT_TB_Door,HT_TB_CW,nmech	,nz,ninf,nwin,HV_tot,HV_inf,HV_win,HV_z,HV_mech,H_tot,tao,dwe_mth,dwd_mth,theta_i,theta_e,QTsink_tot,QT_u_sink,QTsink_Wall,QTsink_Roof,QTsink_Floor,QTsink_GWall,QTsink_Door,QTsink_Win,QTsink_CW,QTsource_tot,QT_u_source,QTsource_Wall,QTsource_Roof,QTsource_Floor,QTsource_GWall,QTsource_Door,QTsource_Win,QTsource_CW,QSopsink_tot,QSopsource_tot,QStr_tot,QSopsink_Wall,QSopsink_Roof,QSopsink_Door,QSopsink_CW_p,QSopsource_Wall,QSopsource_Roof,QSopsource_Door,QSopsource_CW_p,QStr_Win,QStr_CW,QVsink_tot,QV_inf_sink,QV_win_sink,QV_z_sink,QV_mech_sink,QVsource_tot,QV_inf_source,QV_win_source,QV_z_source,QV_mech_source,Q_DHU_win,Q_DHU_mech,Q_DHU_tot,QI_tot,QI_L,QI_P,QI_fac,QI_Humidity,Qsink,Qsource,gamma,a,eta,dQc_b,dQc_sink,Qb_day,Qb_mth,Qb_a,Q_max,t_max,비냉난방존온도");
            string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "Zone_LightResult", "번호,ITr,IRD,ISh_Ish,ISh_hA,Ish_vA,Ish_In_At,Wi,Ish_GDF,Ish,f_τeff_SNA,f_D,f_nearD,f_DCA,f_dclass,f_nearEm_SNA,f_fd_sna,f_fd_sa,f_nearEm_DC,f_fd_c,f_FDS,f_FD,as_bs,hs_bs,hg_hw,normal_ηR,saw_ηR,r_DSNA,r_DSA,r_dclass,r_nearEm_FDS,r_fd_sna,r_fd_sa,r_nearEm_DC,r_fd_c,r_FDS,r_FD,Sunlight_SCW,Sunlight_PjSC,Final_W");
            string[][] envelope = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,면적", "외피유형='" + "외벽" + "'");
            string[][] envelope2 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,면적", "외피유형='" + "커튼월창" + "'");
            string[][] envelope3 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,면적", "외피유형='" + "지붕" + "'");
            string[][] envelope4 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,면적", "외피유형='" + "창호" + "'");
            string[][] envelope5 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,면적", "외피유형='" + "최하층바닥" + "'");

            List<object> items = new List<object>();
            List<object> data = new List<object>();

            List<object>[] __data = new List<object>[700];

            int i = -1, n;

            while (++i < 700)
            {
                __data[i] = new List<object>();
            }

            i = -1;

            while (++i < ZoneG.Length)
            {
                string[][] envelope_1 = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "HT", "직접간접= '직접외기' and 난방_냉방='난방' and 월='1월' and 비이용일_이용일 = '이용일' and 존번호='" + ZoneG[i][0] + "'");
                string[][] envelope_2 = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "HT", "직접간접= '간접외기' and 난방_냉방='난방' and 월='1월' and 비이용일_이용일 = '이용일' and 존번호='" + ZoneG[i][0] + "'");
                string[][] envelope_3 = Program.DB.getValue(DB.type.ProjDB, "Zone_Envelope_Result", "HT", "직접간접= '지면위' and 난방_냉방='난방' and 월='1월' and 비이용일_이용일 = '이용일' and 존번호='" + ZoneG[i][0] + "'");

                items.Add("print9.html"); // 예시 코드: 메인 메뉴 동적 할당

                __data[0].Add(new { idx = i, val = ZoneG[i][8] });
                __data[1].Add(new { idx = i, val = Program.UTIL.asFixed(ZoneG[i][26]) });
                __data[2].Add(new { idx = i, val = Program.UTIL.asFixed(ZoneG[i][9]) }); // 
                __data[3].Add(new { idx = i, val = ZoneG[i][1] });
                __data[4].Add(new { idx = i, val = ZoneG[i][0] });
                __data[5].Add(new { idx = i, val = Program.UTIL.asFixed(ZoneG[i][42]) }); //축열용량
                __data[6].Add(new { idx = i, val = ZoneG[i][9] }); //층고 --> 천장고로 변경
                __data[7].Add(new { idx = i, val = ZoneG[i][9] }); //창면적율 계산 필요
                __data[13].Add(new { idx = i, val = ZoneG[i][46] });
                __data[80].Add(new { idx = i, val = ZoneG[i][46] });


                __data[14].Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 48 + 12][104]) }); // 연간 난방 에너지 요구량
                __data[15].Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 48 + 36][104]) }); // 연간 냉방 에너지 요구량
                //data 16, 17, 18 //
                //
                __data[19].Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 48 + 12][105]) }); // 난방부하
                __data[20].Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 48 + 36][105]) }); // 냉방부하
                __data[21].Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 48][95]) }); // 열손실량 (임수현 팀장님 문의 필요 )
                __data[22].Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 48][96]) }); // 열획득량 (임수현 팀장님 문의 필요 )

                __data[69].Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 48][33]) }); //침기횟수
                __data[70].Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 48][34]) }); //환기횟수
                __data[71].Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 48][31]) }); //기계환기횟수

                __data[72].Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 48][36]) }); //침기 열전달계수
                __data[73].Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 48][37]) }); //자연환기 열전달계수
                __data[74].Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 48][39]) }); //기계환기 열전달계수

                ///data 75 부터 면적당 연간난방에너지요구량 // /
                double hneedarea = 0;
                double cneedarea = 0;

                double hloadarea = 0;
                double cloadarea = 0;
                double sinkarea = 0;
                double sourcearea = 0;


                double area = Convert.ToDouble(ZoneG[i][9]);
                double hneed = Convert.ToDouble(value[i * 48 + 12][104]);
                double cneed = Convert.ToDouble(value[i * 48 + 36][104]);
                double hload = Convert.ToDouble(value[i * 48 + 12][105]);
                double cload = Convert.ToDouble(value[i * 48 + 12][105]);
                double sink = Convert.ToDouble(value[i * 48 + 12][95]);
                double source = Convert.ToDouble(value[i * 48 + 12][96]);

                hneedarea = hneed / area;
                cneedarea = cneed / area;
                hloadarea = hload / area;
                cloadarea = cload / area;
                sinkarea = sink / area;
                sourcearea = source / area;

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
                //double wall3 = Convert.ToDouble(envelope_3[a][0]);
                //htstotal += wall3;

                __data[8].Add(new { idx = i, val = Program.UTIL.asFixed(htdtotal.ToString()) });
                __data[9].Add(new { idx = i, val = Program.UTIL.asFixed(htutotal.ToString()) });
                __data[10].Add(new { idx = i, val = Program.UTIL.asFixed(htstotal.ToString()) });
                __data[88].Add(new { idx = i, val = Program.UTIL.asFixed(httotal.ToString()) });
                if (ZoneG[i][0] == value[i * 48][0])
                {
                    n = -1;
                    double ventamount = 0;
                    double infamount = 0;
                    double mechamount = 0;

                    double losswin = 0;
                    double lossmech = 0;
                    double lossinf = 0;

                    while (++n < ZoneG.Length)
                    {
                        double nwin = Convert.ToDouble(value[i * 48][34]);
                        double ninf = Convert.ToDouble(value[i * 48][33]);
                        double nmech = Convert.ToDouble(value[i * 48][31]);
                        double vol = Convert.ToDouble(ZoneG[i][26]);
                        ventamount = nwin * vol;
                        infamount = ninf * vol;
                        mechamount = nmech * vol;
                    }
                    __data[11].Add(new { idx = i, val = Program.UTIL.asFixed(ventamount.ToString()) });
                    __data[81].Add(new { idx = i, val = Program.UTIL.asFixed(infamount.ToString()) });
                    __data[82].Add(new { idx = i, val = Program.UTIL.asFixed(mechamount.ToString()) });

                    while (++n < 12)
                    {
                        double losswinmth = Convert.ToDouble(value[i * 48 + n + 12][79]);
                        losswin += losswinmth;
                        double lossinfmth = Convert.ToDouble(value[i * 48 + n + 12][78]);
                        lossinf += lossinfmth;
                        double lossmechmth = Convert.ToDouble(value[i * 48 + n + 12][81]);
                        lossmech += lossmechmth;
                    }
                    __data[83].Add(new { idx = i, val = Program.UTIL.asFixed(losswin.ToString()) });
                    __data[84].Add(new { idx = i, val = Program.UTIL.asFixed(lossinf.ToString()) });
                    __data[85].Add(new { idx = i, val = Program.UTIL.asFixed(lossmech.ToString()) });

                }


                if (ZoneG[i][0] == value[i * 48][0])
                {
                    n = -1;

                    while (++n < 48)
                    {
                        __data[23].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n][43]) }); // 이용일
                        __data[50].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n][103]) }); // 비이용일 난방요구량
                        __data[51].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n][100]) }); // 비이용일 대차축열량
                        __data[52].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n][46]) }); // 비이용일 관류열손실량
                        __data[53].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n][77]) }); // 비이용일 환기열손실량
                        __data[54].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n][66]) }); // 비이용일 일사열획득량
                        __data[55].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n][90]) }); // 비이용일 내부발열량

                    }

                    n = -1;

                    while (++n < 12)
                    {
                        __data[24].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][103]) }); // 난방요구량 
                        __data[25].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][44]) }); // 실내온도
                        __data[26].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][45]) }); // 실외온도
                        __data[27].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][99]) }); // 이용계수
                        __data[28].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][46]) }); // 관류 열손실량
                        __data[29].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][48]) }); // 관류(벽체) 열손실량
                        __data[30].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][53]) }); // 관류(창호) 열손실량
                        __data[31].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][54]) }); // 관류(커튼월) 열손실량
                        __data[32].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][49]) }); // 관류(지붕) 열손실량
                        __data[33].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][52]) }); // 관류(출입문) 열손실량
                        __data[34].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][50]) }); // 관류(바닥) 열손실량 
                        ///////data 35  --->  열교 해당 data
                        __data[36].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][77]) }); // 환기 열손실량 
                        __data[37].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][78]) }); // 환기 (침기) 열손실량 
                        __data[38].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][81]) }); // 환기 (기계환기)열손실량 
                        __data[39].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][79]) }); // 환기 (자연환기)열손실량 
                        __data[40].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][66]) }); // 일사열획득량
                        __data[41].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][75]) }); // 일사(창호)열획득량
                        __data[42].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][76]) }); // 일사(커튼월창)열획득량
                        __data[43].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][67]) }); // 일사(외벽)열획득량
                        __data[44].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][68]) }); // 일사(지붕)열획득량
                        __data[45].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][69]) }); // 일사(출입문)열획득량
                        __data[46].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][90]) }); // 내부발열량
                        __data[47].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][91]) }); // 내부발열량(조명)
                        __data[48].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][92]) }); // 내부발열량(인체)
                        __data[49].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][93]) }); // 내부발열량기계
                        ///
                        __data[57].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 36][103]) }); // 이용일 냉방요구량 + 36이 안되는 이유 물어봐야댐
                        //__data[58].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + 36][103]) }); // 제습요구량
                        __data[59].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 36][44]) }); // 냉방기준온도
                        __data[60].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 36][55]) }); // 관류열획득량
                        __data[61].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 36][82]) }); // 환기열획드량
                        __data[62].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 36][66]) }); // 일사열획득량
                        __data[63].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 24][103]) }); // 비이용일 냉방요구량
                        __data[64].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 24][44]) }); // 비이용일 냉방기준온도
                        __data[65].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 36][105]) }); // 최대난방부하
                        __data[66].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 12][106]) }); // 난방시간
                        __data[67].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 36][105]) }); // 최대냉방부하 디버깅시 11개까지 들어오고 마지막 꺼는 안들어오는 현상발생 35로 해도안됨
                        __data[68].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value[i * 48 + n + 36][106]) }); // 냉방시간


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
                if (ZoneG[i][0] == value[48 * i + 12][0])
                {
                    n = -1;

                    while (++n < 12)
                    { }

                    double sum = 0;
                    double kkk;
                    for (int k = 0; k < 12; k++)
                    {
                        kkk = Convert.ToDouble(value[48 * i + 12 + k][92]);//theta (이용일 난방요구량_합산)
                        sum += kkk;//theta (이용일 난방요구량_합산)

                    }

                    _data78.Add(new { idx = i, val = sum });

                    //MessageBox.Show(sum.ToString());

                }
                else;

                //조명 요구량 
                if (ZoneG[i][0] == value2[12 * i][0])
                {
                    n = -1;

                    while (++n < 12)
                    {
                        __data[569].Add(new { idx = i * 12 + n, val = Program.UTIL.asFixed(value2[i * 12 + n][39]) });
                    }

                    double sum = 0;
                    double kkkk;
                    double lneedarea = 0;
                    for (int k = 0; k < 12; k++)
                    {
                        kkkk = Convert.ToDouble(value2[12 * i + k][39]);//Finfal_W (조명에너지 요구량 합산)
                        sum += kkkk;// Finfal_W (조명에너지 요구량 합산)
                                    //MessageBox.Show(sum.ToString());

                        double area = Convert.ToDouble(ZoneG[i][9]);
                        lneedarea = sum / area;

                    }
                    //string result;
                    //result = string.Format("{0:0.#0}", sum);
                    //_data589.Add(new { idx = i, val = result });


                    __data[589].Add(new { idx = i, val = Program.UTIL.asFixed(sum.ToString()) });
                    __data[590].Add(new { idx = i, val = Program.UTIL.asFixed(lneedarea.ToString()) });
                    //MessageBox.Show(sum.ToString());

                }
                else;


                int kk = -1;
                double totalwall = 0;
                double totalcw = 0;
                double totalroof = 0;
                double totalwin = 0;
                double totalfloor = 0;

                while (++kk < envelope.Length)
                {
                    if (ZoneG[i][0] == envelope[kk][0])
                    {
                        double wall = Convert.ToDouble(envelope[kk][1]);
                        totalwall += wall;
                    }
                    else
                    {
                    }
                }

                kk = -1;
                while (++kk < envelope2.Length)
                {
                    if (ZoneG[i][0] == envelope2[kk][0])
                    {
                        double cw = Convert.ToDouble(envelope2[kk][1]);
                        totalcw += cw;
                    }
                    else
                    {
                    }
                }


                kk = -1;
                while (++kk < envelope3.Length)
                {
                    if (ZoneG[i][0] == envelope3[kk][0])
                    {
                        double roof = Convert.ToDouble(envelope3[kk][1]);
                        totalroof += roof;
                    }
                    else
                    {
                    }
                }

                kk = -1;
                while (++kk < envelope4.Length)
                {
                    if (ZoneG[i][0] == envelope4[kk][0])
                    {
                        double win = Convert.ToDouble(envelope4[kk][1]);
                        totalwin += win;
                    }
                    else
                    {
                    }
                }


                kk = -1;
                while (++kk < envelope5.Length)
                {
                    if (ZoneG[i][0] == envelope5[kk][0])
                    {
                        double floor = Convert.ToDouble(envelope5[kk][1]);
                        totalfloor += floor;
                    }
                    else
                    {
                    }

                }


                _data590.Add(new { idx = i, val = Program.UTIL.asFixed(totalwall.ToString()) });
                _data591.Add(new { idx = i, val = Program.UTIL.asFixed(totalcw.ToString()) });
                _data592.Add(new { idx = i, val = Program.UTIL.asFixed(totalroof.ToString()) });
                _data593.Add(new { idx = i, val = Program.UTIL.asFixed(totalwin.ToString()) });
                _data594.Add(new { idx = i, val = Program.UTIL.asFixed(totalfloor.ToString()) });

                //열관류율(벽체) 계산 파트
                kk = -1;
                double Utwall_sum = 0;
                double U_wall = 0;
                //double Htwallarea = 0;


                String[][] uenvelope = Program.DB.querySQL(DB.type.ProjDB, "select a.존,a.면적,b.열관류율,b.유효열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + ZoneG[i][0] + "'");
                while (++kk < uenvelope.Length)
                {
                    if (ZoneG[i][0] == uenvelope[kk][0])
                    {
                        double areaueff = Convert.ToDouble(uenvelope[kk][1]) * Convert.ToDouble(uenvelope[kk][2]);
                        Utwall_sum += areaueff;
                        U_wall = Utwall_sum / totalwall;
                    }
                    else;
                }
                _data595.Add(new { idx = i, val = Program.UTIL.asFixed(U_wall.ToString()) });



                //열관류율(커튼월) 계산 파트
                kk = -1;
                double Utcwsum = 0;
                double U_cw = 0;
                //double Htwallarea = 0;
                String[][] uenvelope2 = Program.DB.querySQL(DB.type.ProjDB, "select a.존,a.면적,b.커튼월창열관류율,b.커튼월창유효열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionCW AS b ON a.구조체번호 = b.번호 where a.존 = '" + ZoneG[i][0] + "'");
                while (++kk < uenvelope2.Length)
                {
                    if (ZoneG[i][0] == uenvelope2[kk][0])
                    {
                        double areaueff = Convert.ToDouble(uenvelope2[kk][1]) * Convert.ToDouble(uenvelope2[kk][2]);
                        Utcwsum += areaueff;
                        U_cw = Utcwsum / totalcw;
                    }
                    else;
                }
                _data596.Add(new { idx = i, val = Program.UTIL.asFixed(U_cw.ToString()) });

                //열관류율(지붕) 계산 파트
                kk = -1;
                double Utroofsum = 0;
                double U_roof = 0;
                //double Htroofarea = 0;
                String[][] uenvelope3 = Program.DB.querySQL(DB.type.ProjDB, "select a.존,a.면적,b.열관류율,b.유효열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionRoof AS b ON a.구조체번호 = b.번호 where a.존 = '" + ZoneG[i][0] + "'");
                while (++kk < uenvelope3.Length)
                {
                    if (ZoneG[i][0] == uenvelope3[kk][0])
                    {
                        double areaueff = Convert.ToDouble(uenvelope3[kk][1]) * Convert.ToDouble(uenvelope3[kk][2]);
                        Utroofsum += areaueff;
                        U_roof = Utroofsum / totalroof;
                    }
                    else;
                }
                _data597.Add(new { idx = i, val = Program.UTIL.asFixed(U_roof.ToString()) });

                //열관류율(창호) 계산 파트
                kk = -1;
                double Utwinsum = 0;
                double U_win = 0;
                //double Htwinarea = 0;
                String[][] uenvelope4 = Program.DB.querySQL(DB.type.ProjDB, "select a.존,a.면적,b.창호열관류율,b.창호유효열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체번호 = b.번호 where a.존 = '" + ZoneG[i][0] + "'");
                while (++kk < uenvelope4.Length)
                {
                    if (ZoneG[i][0] == uenvelope4[kk][0])
                    {
                        double areaueff = Convert.ToDouble(uenvelope4[kk][1]) * Convert.ToDouble(uenvelope4[kk][2]);
                        Utwinsum += areaueff;
                        U_win = Utwinsum / totalwin;
                    }
                    else;
                }
                _data598.Add(new { idx = i, val = Program.UTIL.asFixed(U_win.ToString()) });

                //열관류율(바닥) 계산 파트
                kk = -1;
                double Utflsum = 0;
                double U_fl = 0;
                //double Htwinarea = 0;
                String[][] uenvelope5 = Program.DB.querySQL(DB.type.ProjDB, "select a.존,a.면적,b.열관류율,b.유효열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionFloor AS b ON a.구조체번호 = b.번호 where a.존 = '" + ZoneG[i][0] + "'");
                while (++kk < uenvelope5.Length)
                {
                    if (ZoneG[i][0] == uenvelope5[kk][0])
                    {
                        double areaueff = Convert.ToDouble(uenvelope5[kk][1]) * Convert.ToDouble(uenvelope5[kk][2]);
                        Utflsum += areaueff;
                        U_fl = Utflsum / totalfloor;

                    }
                    else;
                }
                _data599.Add(new { idx = i, val = Program.UTIL.asFixed(U_fl.ToString()) });







                //열전달계수(벽체) 계산 파트
                kk = -1;
                double Htwall_sum = 0;
                double h_wall = 0;
                //double Htwallarea = 0;


                String[][] henvelope = Program.DB.querySQL(DB.type.ProjDB, "select a.존,a.면적,b.열관류율,b.유효열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + ZoneG[i][0] + "'");
                while (++kk < henvelope.Length)
                {
                    if (ZoneG[i][0] == henvelope[kk][0])
                    {
                        double areaueff = Convert.ToDouble(henvelope[kk][3]) * Convert.ToDouble(henvelope[kk][2]);

                        Htwall_sum += areaueff;
                        h_wall = Htwall_sum;
                    }
                    else;
                }
                _data600.Add(new { idx = i, val = Program.UTIL.asFixed(h_wall.ToString()) });

                //열전달계수(커튼월) 계산 파트
                kk = -1;
                double Htcw = 0;

                while (++kk < uenvelope2.Length)
                {
                    if (ZoneG[i][0] == uenvelope2[kk][0])
                    {
                        double areaueff = Convert.ToDouble(uenvelope2[kk][1]) * Convert.ToDouble(uenvelope2[kk][2]);
                        Htcw += areaueff;
                    }
                    else;
                }
                _data601.Add(new { idx = i, val = Program.UTIL.asFixed(Htcw.ToString()) });

                //열전달계수(지붕) 계산 파트
                kk = -1;
                double Htroof = 0;

                while (++kk < uenvelope3.Length)
                {
                    if (ZoneG[i][0] == uenvelope3[kk][0])
                    {
                        double areaueff = Convert.ToDouble(uenvelope3[kk][1]) * Convert.ToDouble(uenvelope3[kk][2]);
                        Htroof += areaueff;
                    }
                    else;
                }
                _data602.Add(new { idx = i, val = Program.UTIL.asFixed(Htroof.ToString()) });

                //열전달계수(바닥) 계산 파트
                kk = -1;
                double Htfl = 0;

                while (++kk < uenvelope4.Length)
                {
                    if (ZoneG[i][0] == uenvelope4[kk][0])
                    {
                        double areaueff = Convert.ToDouble(uenvelope4[kk][1]) * Convert.ToDouble(uenvelope4[kk][2]);
                        Htfl += areaueff;
                    }
                    else;
                }
                _data603.Add(new { idx = i, val = Program.UTIL.asFixed(Htfl.ToString()) });

                //열전달계수(창호) 계산 파트
                kk = -1;
                double Htwin = 0;

                while (++kk < uenvelope5.Length)
                {
                    if (ZoneG[i][0] == uenvelope5[kk][0])
                    {
                        double areaueff = Convert.ToDouble(uenvelope5[kk][1]) * Convert.ToDouble(uenvelope5[kk][2]);
                        Htwin += areaueff;
                    }
                    else;
                }
                _data604.Add(new { idx = i, val = Program.UTIL.asFixed(Htwin.ToString()) });

            }


            ////////////////////////////////////////////////////////////////////
            data.Add(new { cname = "cls-profile-name", data = __data[0] });
            data.Add(new { cname = "cls-volume", data = __data[1] });
            data.Add(new { cname = "cls-zone-anf", data = __data[2] });
            data.Add(new { cname = "cls-sub-name", data = __data[3] });
            data.Add(new { cname = "cls-zone-name", data = __data[4] });
            data.Add(new { cname = "cls-cwirk", data = __data[5] });
            data.Add(new { cname = "cls-floor-level", data = __data[6] });
            //data.Add(new { cname = "cls-window-ratio", data = __data[7] });
            data.Add(new { cname = "cls-zone-ht", data = __data[88] });
            data.Add(new { cname = "cls-htd", data = __data[8] });
            data.Add(new { cname = "cls-htu", data = __data[9] });
            data.Add(new { cname = "cls-hts", data = __data[10] });
            data.Add(new { cname = "cls-vent-amount", data = __data[11] });
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
            data.Add(new { cname = "cls-hload-annual", data = __data[21] });
            data.Add(new { cname = "cls-hgain-annual", data = __data[22] });
            data.Add(new { cname = "cls-hload-annual_area", data = __data[86] });
            data.Add(new { cname = "cls-hgain-annual_area", data = __data[87] });


            data.Add(new { cname = "cls-hneed-wd", data = __data[24] });
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
            //data.Add(new { cname = "cls-cneed2-wd", data = __data[58] });
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
            data.Add(new { cname = "cls-window-area", data = _data593 });
            data.Add(new { cname = "cls-floor-area", data = _data594 });
            data.Add(new { cname = "cls-wall-u", data = _data595 });
            data.Add(new { cname = "cls-cwall-u", data = _data596 });
            data.Add(new { cname = "cls-roof-u", data = _data597 });
            data.Add(new { cname = "cls-win-u", data = _data598 });
            data.Add(new { cname = "cls-floor-u", data = _data599 });

            data.Add(new { cname = "cls-ninf", data = __data[69] });
            data.Add(new { cname = "cls-nmech", data = __data[71] });
            data.Add(new { cname = "cls-nvent", data = __data[70] });

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
            data.Add(new { cname = "cls-cwall-ueff", data = _data596 });
            data.Add(new { cname = "cls-roof-ueff", data = _data597 });
            data.Add(new { cname = "cls-window-ueff", data = _data598 });
            data.Add(new { cname = "cls-floor-ueff", data = _data599 });

            data.Add(new { cname = "cls-wall-h", data = _data600 });
            data.Add(new { cname = "cls-cwall-h", data = _data601 });
            data.Add(new { cname = "cls-roof-h", data = _data602 });
            data.Add(new { cname = "cls-window-h", data = _data603 });
            data.Add(new { cname = "cls-floor-h", data = _data604 });




            s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
            s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());

            runScript("init(" + s + "," + s2 + ")");


        }




        private void button1_Click(object sender, EventArgs e)
        {
            webView21.CoreWebView2.ShowPrintUI();
        }
    }
}
