using Microsoft.Web.WebView2.Core;
using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Net.Sockets;
using System.Linq;
namespace main.contents.Result
{
    public partial class Algorithm_AHU : Form
    {
        bool scriptable = false;
        public Algorithm_AHU()
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


        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            string s, s2;
            string charts = "";
            string[][] 번호 = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "번호", "유형 = '공조기'");

            List<object> items = new List<object>();
            List<object> data = new List<object>();
            List<object>[] FormData = new List<object>[30];
            List<object>[] AhuData = new List<object>[30];
            List<object>[] AnnualData = new List<object>[30];
            List<object>[] PreData = new List<object>[100];
            List<object>[] MthData = new List<object>[100];
            List<object> AHULayersData = new List<object>();

            List<string> chart_hnd = new List<string>();
            List<string> chart_cnd = new List<string>();
            List<string> chart_w = new List<string>();

            int i = -1;
            while (++i < 30)
            {
                FormData[i] = new List<object>();
                AhuData[i] = new List<object>();
                AnnualData[i] = new List<object>();
            }
            i = -1;
            while (++i < 100)
            {
                MthData[i] = new List<object>();
                PreData[i] = new List<object>();
            }
            i = -1;

            while (++i < 번호.Length)
            {
                string Num = 번호[i][0];
                items.Add("Algorithm_AHU.html"); // 예시 코드: 메인 메뉴 동적 할당
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호");
                string 공조기유형, 설치위치;
                if (Value.Length > 0)
                {
                    FormData[0].Add(new { idx = i, val = Value[0][0] }); //프로젝트번호
                }
                
                string titleName = Num + " 공조에너지요구량 검토보고서";


                FormData[1].Add(new { idx = i, val = titleName }); //제목
                FormData[2].Add(new { idx = i, val = Num }); //공조기번호

                string[][] Value2 = Program.DB.getValue(DB.type.ProjDB,"AHUSystem_Form","명칭,유형,풍량제어,설치위치","번호 = '"+Num+"'");
                
                if (Value2.Length > 0)
                {
                    공조기유형 = Value2[0][1];
                    설치위치 = Value2[0][3];
                    FormData[3].Add(new { idx = i, val = Value2[0][0] }); //이름
                    FormData[4].Add(new { idx = i, val = Value2[0][1] }); //설비유형
                    if (공조기유형 == "열회수기")
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "User_HRV", "열회수유형,팬풍량,팬동력,모터제어", "번호 = '" + Num + "'");
                        FormData[5].Add(new { idx = i, val = Value[0][0] }); //열회수기유형
                        FormData[6].Add(new { idx = i, val = Value[0][1] }); //팬풍량
                        FormData[7].Add(new { idx = i, val = Value[0][2] }); //팬동력
                        FormData[8].Add(new { idx = i, val = Value[0][3] }); //모터제어
                    }
                    else
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "User_AHU", "열회수유형,급기풍량,급기팬동력,배기팬동력,모터제어", "번호 = '" + Num + "'");
                        double 동력 = 0;
                        동력 = (Program.UTIL.ToDoubleOrZero(Value[0][2].ToString()) + Program.UTIL.ToDoubleOrZero(Value[0][3].ToString()))*1000;
                        FormData[5].Add(new { idx = i, val = Value[0][0] }); //열회수기유형
                        FormData[6].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][1].ToString(), 0) });  //팬풍량
                        FormData[7].Add(new { idx = i, val = Program.UTIL.doubleComa(동력.ToString(), 0) });//팬동력
                        FormData[8].Add(new { idx = i, val = Value[0][4] }); //모터제어
                    }
                    FormData[9].Add(new { idx = i, val = Value2[0][2] }); //실제어유형
                    FormData[10].Add(new { idx = i, val = Value2[0][3] }); //설치위치
                    FormData[11].Add(new { idx = i, val = 설치위치 == "단열외피 내부" ? "EA 덕트 영향" : "SA 덕트 영향" });
                    //subdata 16개 만들기

                    if(설치위치 == "단열외피 내부")
                    {
                        AhuData[0].Add(new { idx = i, val = "OA" }); // OA_SA 제목
                        AhuData[1].Add(new { idx = i, val = "EA" }); // EA_RA 제목

                        Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "OA덕트길이,EA덕트길이,덕트관경", "번호 = '" + Num + "'");
                        AhuData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][0].ToString(), 1) }); // OA덕트길이
                        AhuData[3].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][1].ToString(), 1) }); //  EA 덕트길이

                        Value2 = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "Hduct_OA,Hduct_EA", "번호 = '" + Num + "' And 난방_냉방 = '난방' And 월 = '1월'");
                        AhuData[4].Add(new { idx = i, val = Program.UTIL.doubleComa(Value2[0][0].ToString(), 3) }); // OA 열관류율
                        AhuData[5].Add(new { idx = i, val = Program.UTIL.doubleComa(Value2[0][1].ToString(), 3) }); // EA 열관류율
                    }
                    else
                    {
                        AhuData[0].Add(new { idx = i, val = "SA" }); // OA_SA 제목
                        AhuData[1].Add(new { idx = i, val = "RA" }); // EA_RA 제목

                        Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "SA덕트길이,RA덕트길이,덕트관경", "번호 = '" + Num + "'");
                        AhuData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][0].ToString(), 1) }); // SA덕트길이
                        AhuData[3].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][1].ToString(), 1) }); //  RA 덕트길이
                        
                        Value2 = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "Hduct_SA,Hduct_RA", "번호 = '" + Num + "' And 난방_냉방 = '난방' And 월 = '1월'");
                        AhuData[4].Add(new { idx = i, val = Program.UTIL.doubleComa(Value2[0][0].ToString(), 3) }); // OA 열관류율
                        AhuData[5].Add(new { idx = i, val = Program.UTIL.doubleComa(Value2[0][1].ToString(), 3) }); // EA 열관류율
                    }

                    AhuData[6].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][2].ToString(), 0) }); // 덕트관경
                    AhuData[7].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][2].ToString(), 0) }); // 덕트관경

                    if (공조기유형 == "열회수기") //효율값 8~16번 항목
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "User_HRV", "온도교환효율_난방, 온도교환효율_냉방, 전열교환효율_난방, 전열교환효율_냉방", "번호 = '" + Num + "'");
                        AhuData[8].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][0].ToString(), 0) }); // 온도교환효율_난방
                        AhuData[9].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][1].ToString(), 0) }); // 온도교환효율_냉방
                        AhuData[10].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][2].ToString(), 0) }); // 전열교환효율_난방
                        AhuData[11].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][3].ToString(), 0) }); // 전열교환효율_냉방
                        AhuData[12].Add(new { idx = i, val = "-" }); // 난방코일출력
                        AhuData[13].Add(new { idx = i, val = "-" }); // 냉방코일출력
                        AhuData[14].Add(new { idx = i, val = "-" }); // 난방입구온도
                        AhuData[15].Add(new { idx = i, val = "-" }); // 냉방입구온도
                        AhuData[16].Add(new { idx = i, val = "-" }); // 난방출구온도
                        AhuData[17].Add(new { idx = i, val = "-" }); // 냉방출구온도

                        Value2 = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "X_iset", "번호 = '" + Num + "' And 난방_냉방 = '냉방' And 월 = '1월'");
                        double maxhumid = Value2.Length > 0 ? Program.UTIL.ToDoubleOrZero(Value2[0][0]) * 1000 : 0; // 실내 설정습도(계산값, kg/kg'→g/kg')
                        Value2 = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "X_iset", "번호 = '" + Num + "' And 난방_냉방 = '난방' And 월 = '1월'");
                        double minhumid = Value2.Length > 0 ? Program.UTIL.ToDoubleOrZero(Value2[0][0]) * 1000 : 0;
                        AhuData[18].Add(new { idx = i, val = Program.UTIL.doubleComa(maxhumid.ToString(), 2) }); // 최대습도
                        AhuData[19].Add(new { idx = i, val = Program.UTIL.doubleComa(minhumid.ToString(), 2) }); // 최소습도
                        AhuData[20].Add(new { idx = i, val = "-" }); // 가습기유형
                        AhuData[21].Add(new { idx = i, val = "-" }); // 가습기제어유형
                        AhuData[22].Add(new { idx = i, val = "-" }); // 가습기용량
                    }
                    else
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "User_AHU", "온도교환효율_난방, 온도교환효율_냉방, 전열교환효율_난방, 전열교환효율_냉방,난방코일출력,냉각코일출력,난방코일_입구온도,냉각코일_입구_건구온도,난방코일_출구온도,냉각코일_출구_건구온도", "번호 = '" + Num + "'");
                        AhuData[8].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][0].ToString(), 0) }); // 온도교환효율_난방
                        AhuData[9].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][1].ToString(), 0) }); // 온도교환효율_냉방
                        AhuData[10].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][2].ToString(), 0) }); // 전열교환효율_난방
                        AhuData[11].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][3].ToString(), 0) }); // 전열교환효율_냉방

                        for(int k = 0; k < 6; k++)// 난방코일출력,냉방코일출력,난방입구온도,냉방입구온도,난방출구온도,냉방출구온도
                        {
                            if (double.TryParse(Value[0][4+k], out double result)) // 난방코일출력
                            {
                                AhuData[12+k].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][4+k], 1) });
                            }
                            else AhuData[12+k].Add(new { idx = i, val = "-" });
                        }
                        Value = Program.DB.getValue(DB.type.ProjDB, "User_AHU", "가습기유형,가습기제어유형,가습기용량", "번호 = '" + Num + "'");
                        Value2 = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "X_iset", "번호 = '" + Num + "' And 난방_냉방 = '냉방' And 월 = '1월'");
                        double maxhumid = Value2.Length > 0 ? Program.UTIL.ToDoubleOrZero(Value2[0][0]) * 1000 : 0; // 실내 설정습도(계산값, kg/kg'→g/kg')
                        Value2 = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "X_iset", "번호 = '" + Num + "' And 난방_냉방 = '난방' And 월 = '1월'");
                        double minhumid = Value2.Length > 0 ? Program.UTIL.ToDoubleOrZero(Value2[0][0]) * 1000 : 0;
                        AhuData[18].Add(new { idx = i, val = Program.UTIL.doubleComa(maxhumid.ToString(), 2) }); // 최대습도
                        AhuData[19].Add(new { idx = i, val = Program.UTIL.doubleComa(minhumid.ToString(), 2) }); // 최소습도
                        AhuData[20].Add(new { idx = i, val = Value[0][0].ToString() }); // 가습기유형
                        AhuData[21].Add(new { idx = i, val = Value[0][1].ToString() });  // 가습기제어유형
                        AhuData[22].Add(new { idx = i, val = Value[0][2].ToString() });  // 가습기용량
                    }

                    Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "예열예냉유형", "번호 = '" + Num + "'");
                    string pretype = Value[0][0].ToString();
                    PreData[0].Add(new { idx = i, val = pretype });  // 예열예냉유형
                    if (pretype == "전기예열기")
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "프리히터용량", "번호 = '" + Num + "'");
                        PreData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][0].ToString(), 0) }); //용량
                    }
                    else
                    {
                        PreData[1].Add(new { idx = i, val = "-" });   //용량
                    }

                    AHULayersData.Add(new { idx = i, val = BuildAhuImageLayers(Num, 설치위치, pretype) });

                    //존난방,냉방,제습요구량

                    Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호", "선택열회수기 = '" + Num + "'");
                    List<string> zonecheck = new List<string>();
                    double[] heat = new double[13], cool = new double[13], dehum = new double[13];
                    double[] heatduct = new double[13], coolduct = new double[13];
                    double[] aux_sa = new double[13], aux_ra = new double[13], aux_pre = new double[13], aux_hum = new double[13];
                    double[] aux_ctrl = new double[13], aux_rotor = new double[13], aux_total = new double[13];
                    double[] AHU_heat = new double[13], AHU_cool = new double[13], AHU_heat_use = new double[13], AHU_cool_use = new double[13];
                    double area = 0;
                    string zone;
                    foreach (string[] k in Value)
                    {
                        zonecheck.Add(k[0]);
                    }
                    for (int j = 0; j < 12; j++)
                    {
                        heat[j] = Cal_Qb(zonecheck, j,"난방");
                        heat[12] += heat[j];
                        cool[j] = Cal_Qb(zonecheck, j, "냉방");
                        cool[12] += cool[j];
                        dehum[j] = Cal_Qb(zonecheck, j, "제습");
                        dehum[12] += dehum[j];

                        heatduct[j] = Cal_Qv("난방덕트", Num,j);
                        heatduct[12] += heatduct[j];
                        coolduct[j] = Cal_Qv("냉방덕트", Num,j);
                        coolduct[12] += coolduct[j];

                        aux_sa[j] = Cal_Qv("급기팬", Num,j);
                        aux_sa[12] += aux_sa[j];
                        aux_ra[j] = Cal_Qv("배기팬", Num, j);
                        aux_ra[12] += aux_ra[j];
                        aux_pre[j] = Cal_Qv("프리히터기", Num, j);
                        aux_pre[12] += aux_pre[j];
                        aux_hum[j] = Cal_Qv("가습기", Num, j);
                        aux_hum[12] += aux_hum[j];
                        aux_ctrl[j] = Cal_Qv("제어", Num, j);
                        aux_ctrl[12] += aux_ctrl[j];
                        aux_rotor[j] = Cal_Qv("로터모터", Num, j);
                        aux_rotor[12] += aux_rotor[j];

                        aux_total[j] = aux_sa[j] + aux_ra[j] + aux_pre[j] + aux_hum[j] + aux_ctrl[j] + aux_rotor[j];
                        aux_total[12] += aux_total[j];


                        AHU_heat[j] = Cal_Qv("난방", Num, j);
                        AHU_heat[12] += AHU_heat[j];
                        AHU_cool[j] = Cal_Qv("냉방", Num, j);
                        AHU_cool[12] += AHU_cool[j];
                        AHU_heat_use[j] = Cal_Qv("난방소요량", Num, j);
                        AHU_heat_use[12] += AHU_heat_use[j];
                        AHU_cool_use[j] = Cal_Qv("냉방소요량", Num, j);
                        AHU_cool_use[12] += AHU_cool_use[j];

                    }

                    zone = zonecheck[0] + "외 " + (zonecheck.Count() - 1).ToString() + "개";
                    area = Cal_Qb(zonecheck, 1, "면적");

                    //존정보 및 설치 면적
                    AnnualData[0].Add(new { idx = i, val = zone }); //존 정보
                    AnnualData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(area.ToString(), 2) }); //면적 정보
                    AnnualData[2].Add(new { idx = i, val = Program.UTIL.doubleComa((AHU_heat[12]/area).ToString(), 0) }); //연간난방에너지요구량
                    AnnualData[3].Add(new { idx = i, val = Program.UTIL.doubleComa((AHU_cool[12]/area).ToString(), 0) }); //연간냉방에너지요구량
                    AnnualData[4].Add(new { idx = i, val = Program.UTIL.doubleComa((dehum[12]/area).ToString(), 0) }); //연간제습에너지요구량
                    AnnualData[5].Add(new { idx = i, val = Program.UTIL.doubleComa((aux_total[12]/area).ToString(), 0) }); //연간보조에너지소요량
                    for (int mth = 0; mth < 13; mth++)
                    {
                        MthData[0].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(heat[mth].ToString(), 0) }); //난방
                        MthData[1].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(cool[mth].ToString(), 0) }); //냉방(제습제외)
                        MthData[2].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(dehum[mth].ToString(), 0) }); //제습
                        MthData[5].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(heatduct[mth].ToString(), 0) }); //난방덕트절감량
                        MthData[6].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(coolduct[mth].ToString(), 0) }); //냉방덕트절갈량
                        MthData[7].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(aux_total[mth].ToString(), 0) }); //총보조에너지
                        MthData[8].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(aux_sa[mth].ToString(), 0) }); //급기팬보조에너지
                        MthData[9].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(aux_ra[mth].ToString(), 0) }); //배기팬보조에너지
                        MthData[10].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(aux_hum[mth].ToString(), 0) }); //가습기보조에너지
                        MthData[11].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(aux_pre[mth].ToString(), 0) }); //예열기보조에너지
                        MthData[12].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(AHU_heat[mth].ToString(), 0) }); //공조난방에너지요구량
                        MthData[13].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(AHU_cool[mth].ToString(), 0) }); //공조냉방에너지요구량
                        MthData[15].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(aux_ctrl[mth].ToString(), 0) }); //제어보조에너지
                        MthData[16].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(aux_rotor[mth].ToString(), 0) }); //로터모터보조에너지
                        MthData[17].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(AHU_heat_use[mth].ToString(), 0) }); //공조난방에너지소요량
                        MthData[18].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(AHU_cool_use[mth].ToString(), 0) }); //공조냉방에너지소요량
                    }

                    data.Add(new { cname = "projectnum", data = FormData[0] });
                    data.Add(new { cname = "title", data = FormData[1] });
                    data.Add(new { cname = "ahu_num", data = FormData[2] });
                    data.Add(new { cname = "ahu_name", data = FormData[3] });
                    data.Add(new { cname = "ahu_type", data = FormData[4] });
                    data.Add(new { cname = "ahu_HRV", data = FormData[5] });
                    data.Add(new { cname = "ahu_cmh", data = FormData[6] });
                    data.Add(new { cname = "ahu_power", data = FormData[7] });
                    data.Add(new { cname = "ahu_motor", data = FormData[8] });
                    data.Add(new { cname = "ahu_control", data = FormData[9] });
                    data.Add(new { cname = "ahu_location", data = FormData[10] });
                    data.Add(new { cname = "duct_title", data = FormData[11] });


                    data.Add(new { cname = "OA_SA", data = AhuData[0] });
                    data.Add(new { cname = "EA_RA", data = AhuData[1] });
                    data.Add(new { cname = "oa_leng", data = AhuData[2] });
                    data.Add(new { cname = "ea_leng", data = AhuData[3] });
                    data.Add(new { cname = "oa_u", data = AhuData[4] });
                    data.Add(new { cname = "ea_u", data = AhuData[5] });
                    data.Add(new { cname = "oa_dia", data = AhuData[6] });
                    data.Add(new { cname = "ea_dia", data = AhuData[7] });
                    data.Add(new { cname = "eff_Ht", data = AhuData[8] });
                    data.Add(new { cname = "eff_Ct", data = AhuData[9] });
                    data.Add(new { cname = "eff_He", data = AhuData[10] });
                    data.Add(new { cname = "eff_Ce", data = AhuData[11] });
                    data.Add(new { cname = "coil_heatpower", data = AhuData[12] });
                    data.Add(new { cname = "coil_coolpower", data = AhuData[13] });
                    data.Add(new { cname = "coil_heatint", data = AhuData[14] });
                    data.Add(new { cname = "coil_coolint", data = AhuData[15] });
                    data.Add(new { cname = "coil_heatout", data = AhuData[16] });
                    data.Add(new { cname = "coil_coolout", data = AhuData[17] });

                    data.Add(new { cname = "ahu_hummax", data = AhuData[18] });
                    data.Add(new { cname = "ahu_hummin", data = AhuData[19] });
                    data.Add(new { cname = "hum_type", data = AhuData[20] });
                    data.Add(new { cname = "hum_control", data = AhuData[21] });
                    data.Add(new { cname = "hum_power", data = AhuData[22] });

                    data.Add(new { cname = "pre_type", data = PreData[0] });
                    data.Add(new { cname = "pre_power", data = PreData[1] });
                    data.Add(new { cname = "ahu_layers", data = AHULayersData });

                    data.Add(new { cname = "zone_hndmth", data = MthData[0] });
                    data.Add(new { cname = "zone_cndmth", data = MthData[1] });
                    data.Add(new { cname = "zone_dehumth", data = MthData[2] });

                    data.Add(new { cname = "duct_h", data = MthData[5] });
                    data.Add(new { cname = "duct_c", data = MthData[6] });
                    data.Add(new { cname = "w_total", data = MthData[7] });
                    data.Add(new { cname = "w_safan", data = MthData[8] });
                    data.Add(new { cname = "w_rafan", data = MthData[9] });
                    data.Add(new { cname = "w_hum", data = MthData[10] });
                    data.Add(new { cname = "w_pre", data = MthData[11] });
                    data.Add(new { cname = "w_ctrl", data = MthData[15] });
                    data.Add(new { cname = "w_rotor", data = MthData[16] });

                    data.Add(new { cname = "mth_hnd", data = MthData[12] });
                    data.Add(new { cname = "mth_cnd", data = MthData[13] });
                    data.Add(new { cname = "qvf_h", data = MthData[17] });
                    data.Add(new { cname = "qvf_c", data = MthData[18] });

                    data.Add(new { cname = "zoneinfo", data = AnnualData[0] });
                    data.Add(new { cname = "zonearea", data = AnnualData[1] });
                    data.Add(new { cname = "year_h", data = AnnualData[2] });
                    data.Add(new { cname = "year_c", data = AnnualData[3] });
                    data.Add(new { cname = "year_hum", data = AnnualData[4] });
                    data.Add(new { cname = "year_aux", data = AnnualData[5] });

                    List<object> hnd_chart = new List<object>();
                    List<object> cnd_chart = new List<object>();
                    List<object> w_chart = new List<object>();

                    for (int mth = 0; mth < 12; mth++)
                    {
                        hnd_chart.Add(Program.UTIL.ToDoubleOrZero(Program.UTIL.doubleComa(AHU_heat[mth].ToString(), 0)));
                        cnd_chart.Add(Program.UTIL.ToDoubleOrZero(Program.UTIL.doubleComa(AHU_cool[mth].ToString(), 0)));
                        w_chart.Add(Program.UTIL.ToDoubleOrZero(Program.UTIL.doubleComa(aux_total[mth].ToString(), 0)));
                    }
                    chart_hnd.Add(System.Text.Json.JsonSerializer.Serialize(hnd_chart.ToArray()));
                    chart_cnd.Add(System.Text.Json.JsonSerializer.Serialize(cnd_chart.ToArray()));
                    chart_w.Add(System.Text.Json.JsonSerializer.Serialize(w_chart.ToArray()));

                    double max = 0;

                    max = Enumerable.Range(0, 12)
                        .Select(mth => AHU_heat[mth] + AHU_cool[mth] + aux_total[mth])
                        .Max() * 1.05;

                    if (max <= 0)
                    {
                        max = 100;
                    }

                    int n = ((int)max).ToString().Length;
                    max = Program.UTIL.ToDoubleOrZero(String.Format("{0:F0}", max / Math.Pow(10, n - 1))) * Math.Pow(10, n - 1) + Math.Pow(10, n - 1);
                    if (charts != "") charts += ",";
                    charts += "{data:[" +
                    "{type:\"bar\",barPercentage:0.55,label:\"보조설비소요량 [kWh]\",data:" + chart_w[i] + ",borderColor:\"#ED7D31\",backgroundColor:\"#ED7D31\",dash:false}," +
                    "{type:\"bar\",barPercentage:0.55,label:\"공조난방요구량 [kWh]\",data:" + chart_hnd[i] + ",borderColor:\"#FFD966\",backgroundColor:\"#FFD966\",dash:false}," +
                    "{type:\"bar\",barPercentage:0.55,label:\"공조냉방요구량 [kWh]\",data:" + chart_cnd[i] + ",borderColor:\"#9DC3E6\",backgroundColor:\"#9DC3E6\",dash:false}," +
                    "],max:" + max.ToString() + ",step:100,legend:true,stacked:true}";


                }

                s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());

                Debug.Print("start");
                runScript("init(" + s + "," + s2 + "," + "[" + charts + "])");
            }
        }

        private List<object> BuildAhuImageLayers(string num, string location, string pretype)
        {
            const double parentX = 100;
            List<object> layers = new List<object>();

            string[][] ahu = Program.DB.getValue(DB.type.ProjDB, "User_AHU",
                "열회수유형,냉각코일출력,난방코일출력,가습기용량,공조방식",
                "번호 ='" + num + "'");
            if (ahu.Length == 0)
            {
                return layers;
            }

            string hrvType = ahu[0][0] == "판형" ? "판형" :
                ahu[0][0] == "없음" ? "없음" : "회전형";

            void AddDbImage(string condition, double x, double y, double width, double height)
            {
                string[][] image = Program.DB.getValue(DB.type.BaseDB_AHU, "공조시스템이미지", "이미지", condition);
                if (image.Length > 0 && !string.IsNullOrWhiteSpace(image[0][0]))
                {
                    layers.Add(new { s = image[0][0], x, y, w = width, h = height });
                }
            }

            // ImagePanel에서 LocationpictureBox가 (100, 0), MainpictureBox와 부품들이 그 자식으로 배치된다.
            AddDbImage("항목유형 = '공조기' And 설비유형 = '" + location + "'", parentX, 0, 790, 365);
            AddDbImage("항목유형 = '공조기' And 설비유형='" + hrvType + "'", parentX, 0, 790, 365);

            if (pretype == "전기예열기")
            {
                AddDbImage("항목유형 = '예열예냉' And 설비유형='전기예열기'", parentX + 220, 190, 40, 80);
            }

            double coolingCoil = Program.UTIL.ToDoubleOrZero(ahu[0][1]);
            double heatingCoil = Program.UTIL.ToDoubleOrZero(ahu[0][2]);
            double humidifier = Program.UTIL.ToDoubleOrZero(ahu[0][3]);
            bool plateType = hrvType == "판형";

            if (coolingCoil > 0 && hrvType != "없음")
            {
                AddDbImage("항목유형 = '냉방코일' And 설비유형='" + hrvType + "'",
                    parentX + 395, plateType ? 100 : 188, 40, 80);
            }
            if (heatingCoil > 0 && hrvType != "없음")
            {
                AddDbImage("항목유형 = '난방코일' And 설비유형='" + hrvType + "'",
                    parentX + 427, plateType ? 100 : 188, 40, 80);
            }
            if (humidifier > 0 && hrvType != "없음")
            {
                AddDbImage("항목유형 = '가습기' And 설비유형='" + hrvType + "'",
                    parentX + 459, plateType ? 100 : 188, 30, 80);
            }
            if (ahu[0][4] == "변풍량" && hrvType != "없음")
            {
                AddDbImage("항목유형 = 'VAV' And 설비유형 = '변풍량'",
                    parentX + 608, plateType ? 122 : 196, 118, 55);
            }

            return layers;
        }

        double Cal_Qb(List<string> SelectZone, int g, string HC)
        {
            double Need = 0;
            string mth = (g + 1).ToString() + "월";
            if (HC == "난방")
            {
                for (int i = 0; i < SelectZone.Count; i++)
                {
                    string[][] 난방 = Program.DB.getValue(DB.type.ProjDB, "Zone_52016_Result", "Qb_mth", "존번호 ='" + SelectZone[i] + "' AND 난방_냉방 = '난방' AND 월 = '" + mth + "'");
                    Need += Program.UTIL.ToDoubleOrZero(난방[0][0]);
                }
            }
            else if (HC == "제습")
            {
                for (int i = 0; i < SelectZone.Count; i++)
                {
                    string[][] 제습 = Program.DB.getValue(DB.type.ProjDB, "Zone_52016_Result", "Q_DHU_tot, dwd_mth", "존번호 ='" + SelectZone[i] + "' AND 난방_냉방 = '냉방' AND 월 = '" + mth + "'");
                    Need += Program.UTIL.ToDoubleOrZero(제습[0][0]) * Program.UTIL.ToDoubleOrZero(제습[0][1]) / 1000;
                }
            }
            else if (HC == "냉방")
            {
                for (int i = 0; i < SelectZone.Count; i++)
                {
                    string[][] 냉방 = Program.DB.getValue(DB.type.ProjDB, "Zone_52016_Result", "Qb_mth,Q_DHU_tot, dwd_mth", "존번호 ='" + SelectZone[i] + "' AND 난방_냉방 = '냉방' AND 월 = '" + mth + "'");
                    Need += Program.UTIL.ToDoubleOrZero(냉방[0][0]) - Program.UTIL.ToDoubleOrZero(냉방[0][1]) * Program.UTIL.ToDoubleOrZero(냉방[0][2]) / 1000;
                }
            }
            else if (HC == "면적")
            {
                for (int i = 0; i < SelectZone.Count; i++)
                {
                    string[][] 면적 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "존번호 = '" + SelectZone[i] + "'");
                    Need += Program.UTIL.ToDoubleOrZero(면적[0][0].ToString());
                }
            }
            return Need;
        }
        
        double Cal_Qv(string type, string num, int g)
        {
            double vla = 0;
            string mth = (g + 1).ToString() + "월";
            if (type == "난방예열기")
            {
                string[][] var = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "Q_gnd,Q_prh", "번호 = '" + num + "' AND 난방_냉방 = '난방' AND 월 = '" + mth + "'");
                vla = Math.Max(0,Program.UTIL.ToDoubleOrZero(var[0][0])) + Math.Max(0,Program.UTIL.ToDoubleOrZero(var[0][1])); //양수값만 반영함
            }
            else if(type == "냉방예열기")
            {
                string[][] var = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "Q_gnd", "번호 ='" + num + "' AND 난방_냉방 = '냉방' AND 월 = '" + mth + "'");
                vla = Math.Max(0, -(Math.Min(0,Program.UTIL.ToDoubleOrZero(var[0][0]))));
            }
            else if(type == "난방덕트")
            {
                string[][] location = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "설치위치", "번호 ='" + num + "'");
                string ductColumn = location.Length > 0 && location[0][0] == "단열외피 내부" ? "Q_loss_EA_du" : "Q_loss_SA_du";
                string[][] var = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "순공조요구량," + ductColumn, "번호 ='" + num + "' AND 난방_냉방 = '난방' AND 월 = '" + mth + "'");
                if (var.Length > 0 && Program.UTIL.ToDoubleOrZero(var[0][0]) != 0)
                {
                    vla = Math.Max(0, Program.UTIL.ToDoubleOrZero(var[0][1]));
                }
            } 
            else if(type == "냉방덕트")
            {
                string[][] location = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "설치위치", "번호 ='" + num + "'");
                string ductColumn = location.Length > 0 && location[0][0] == "단열외피 내부" ? "Q_loss_EA_du" : "Q_loss_SA_du";
                string[][] var = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "순공조요구량," + ductColumn, "번호 ='" + num + "' AND 난방_냉방 = '냉방' AND 월 = '" + mth + "'");
                if (var.Length > 0 && Program.UTIL.ToDoubleOrZero(var[0][0]) != 0)
                {
                    vla = Math.Max(0, -Program.UTIL.ToDoubleOrZero(var[0][1]));
                }
            }
            else if (type == "급기팬")
            {
                string[][] var = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "급기팬보조에너지", "번호 ='" + num + "' AND 난방_냉방 = '난방' AND 월 = '" + mth + "'");
                vla = (Program.UTIL.ToDoubleOrZero(var[0][0]));
            }
            else if (type == "배기팬")
            {
                string[][] var = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "배기팬보조에너지", "번호 ='" + num + "' AND 난방_냉방 = '난방' AND 월 = '" + mth + "'");
                vla = (Program.UTIL.ToDoubleOrZero(var[0][0]));
            }
            else if (type == "프리히터기")
            {
                string[][] var = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "프리히팅보조에너지", "번호 ='" + num + "' AND 난방_냉방 = '난방' AND 월 = '" + mth + "'");
                vla = (Program.UTIL.ToDoubleOrZero(var[0][0]));
            }
            else if (type == "가습기")
            {
                string[][] var = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "가습보조에너지", "번호 ='" + num + "' AND 난방_냉방 = '난방' AND 월 = '" + mth + "'");
                vla = (Program.UTIL.ToDoubleOrZero(var[0][0]));
            }
            else if (type == "제어")
            {
                string[][] var = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "제어보조에너지", "번호 ='" + num + "' AND 난방_냉방 = '난방' AND 월 = '" + mth + "'");
                vla = Program.UTIL.ToDoubleOrZero(var[0][0]);
            }
            else if (type == "로터모터")
            {
                string[][] var = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "로터모터보조에너지", "번호 ='" + num + "' AND 난방_냉방 = '난방' AND 월 = '" + mth + "'");
                vla = Program.UTIL.ToDoubleOrZero(var[0][0]);
            }
            else if (type == "난방")
            {
                string[][] var = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "공조요구량", "번호 ='" + num + "' AND 난방_냉방 = '난방' AND 월 = '" + mth + "'");
                vla = (Program.UTIL.ToDoubleOrZero(var[0][0]));
            }
            else if (type == "냉방")
            {
                string[][] var = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "공조요구량", "번호 ='" + num + "' AND 난방_냉방 = '냉방' AND 월 = '" + mth + "'");
                vla = (Program.UTIL.ToDoubleOrZero(var[0][0]));
            }
            else if (type == "난방소요량")
            {
                string[][] var = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "공조소요량", "번호 ='" + num + "' AND 난방_냉방 = '난방' AND 월 = '" + mth + "'");
                vla = Program.UTIL.ToDoubleOrZero(var[0][0]);
            }
            else if (type == "냉방소요량")
            {
                string[][] var = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "공조소요량", "번호 ='" + num + "' AND 난방_냉방 = '냉방' AND 월 = '" + mth + "'");
                vla = Program.UTIL.ToDoubleOrZero(var[0][0]);
            }

            return vla;
        }
    }
}
