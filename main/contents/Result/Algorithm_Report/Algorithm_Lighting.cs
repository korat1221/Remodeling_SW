using Microsoft.Web.WebView2.Core;
using System;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace main.contents.Result
{
    public partial class Algorithm_Lighting : Form
    {
        bool scriptable = false;
        public Algorithm_Lighting()
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

        public double  daytime(double starttime, double endtime, double sunrisetime, double sunsettime)
        {
            double daytime = 0;
            if (sunsettime > sunrisetime && starttime == endtime)
            {
                daytime = (sunsettime - sunrisetime) * 24;
            }
            else
            {
                if (sunsettime > sunrisetime && starttime < sunrisetime)
                {
                    if (starttime > endtime && endtime < sunrisetime)
                    {
                        daytime = (sunsettime - sunrisetime) * 24;
                    }
                    else if (starttime < endtime && endtime < sunrisetime)
                    {
                        daytime = 0;
                    }
                    else if (starttime < endtime && endtime > sunrisetime && endtime < sunsettime)
                    {
                        daytime = (endtime - sunrisetime) * 24;
                    }
                    else if (starttime < endtime && endtime > sunsettime)
                    {
                        daytime = (sunsettime - sunrisetime) * 24;
                    }
                }
                else if (sunsettime > sunrisetime && starttime > sunrisetime && starttime < sunsettime)
                {
                    if (endtime < starttime && endtime < sunrisetime)
                    {
                        daytime = (sunsettime - starttime) * 24;
                    }
                    else if (endtime < starttime && endtime > sunrisetime && endtime < sunsettime)
                    {
                        daytime = ((sunsettime - sunrisetime) - Math.Abs(starttime - endtime)) * 24;
                    }
                    else if (endtime > starttime && endtime > sunrisetime && endtime < sunsettime)
                    {
                        daytime = (endtime - starttime) * 24;
                    }
                    else if (endtime > starttime && endtime > sunsettime)
                    {
                        daytime = (sunsettime - starttime) * 24;
                    }
                }
                else if (sunsettime > sunrisetime && starttime > sunsettime)
                {
                    if (endtime < starttime && endtime < sunrisetime)
                    {
                        daytime = 0;
                    }
                    else if (endtime < starttime && endtime > sunrisetime && endtime < sunsettime)
                    {
                        daytime = (endtime - sunrisetime) * 24;
                    }
                    else if (endtime < starttime && endtime > sunsettime)
                    {
                        daytime = (sunsettime - sunrisetime) * 24;
                    }
                    else if (endtime > starttime)
                    {
                        daytime = 0;
                    }
                }
            }
            return daytime;
        }
        public double nighttime(double starttime, double endtime, double sunrisetime, double sunsettime)
        {
            double nighttime = 0;
            if (sunsettime > sunrisetime && starttime == endtime)
            {
                nighttime = (1 - (sunsettime - sunrisetime)) * 24;
            }
            else
            {
                if (sunsettime > sunrisetime && starttime < sunrisetime)
                {
                    if (starttime > endtime && endtime < sunrisetime)
                    {
                        nighttime = (1 - (Math.Abs(endtime - starttime) + (sunsettime - sunrisetime))) * 24;
                    }
                    else if (starttime < endtime && endtime < sunrisetime)
                    {
                        nighttime = (endtime - starttime) * 24;
                    }
                    else if (starttime < endtime && endtime > sunrisetime && endtime < sunsettime)
                    {
                        nighttime = (sunrisetime - starttime) * 24;
                    }
                    else if (starttime < endtime && endtime > sunsettime)
                    {
                        nighttime = ((endtime - sunsettime) + (sunrisetime - starttime)) * 24;
                    }
                }
                else if (sunsettime > sunrisetime && starttime > sunrisetime && starttime < sunsettime)
                {
                    if (endtime < starttime && endtime < sunrisetime)
                    {
                        nighttime = (1 - ((sunrisetime - endtime) + (sunsettime - sunrisetime))) * 24;
                    }
                    else if (endtime < starttime && endtime > sunrisetime && endtime < sunsettime)
                    {
                        nighttime = (1 - (sunsettime - sunrisetime)) * 24;
                    }
                    else if (endtime > starttime && endtime > sunrisetime && endtime < sunsettime)
                    {
                        nighttime = 0;
                    }
                    else if (endtime > starttime && endtime > sunsettime)
                    {
                        nighttime = (endtime - sunsettime) * 24;
                    }
                }
                else if (sunsettime > sunrisetime && starttime > sunsettime)
                {
                    if (endtime < starttime && endtime < sunrisetime)
                    {
                        nighttime = (1 - ((sunrisetime - endtime) + (sunsettime - sunrisetime) + (starttime - sunsettime))) * 24;
                    }
                    else if (endtime < starttime && endtime > sunrisetime && endtime < sunsettime)
                    {
                        nighttime = (1 - ((sunsettime - sunrisetime) + (starttime - sunsettime))) * 24;
                    }
                    else if (endtime < starttime && endtime > sunsettime)
                    {
                        nighttime = (1 - (Math.Abs(starttime - endtime) + (sunsettime - sunrisetime))) * 24;
                    }
                    else if (endtime > starttime)
                    {
                        nighttime = (endtime - starttime) * 24;
                    }
                }
            }
            return nighttime;
        }

        public void LoadData(string ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            string s, s2;
            string charts = "";
            string[][] 번호 = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "번호", "");
            List<object> items = new List<object>();
            List<object> data = new List<object>();
            List<object>[] zoneData = new List<object>[30]; //존정보
            List<object>[] daylightData = new List<object>[30]; //주광정보
            List<object>[] lightData = new List<object>[30]; //인공조명
            List<object>[] AnnualData = new List<object>[30]; //연간정보
            List<object>[] renewData = new List<object>[30]; //집광채광
            List<object>[] dData = new List<object>[30]; //자연채광
            List<object>[] zoneMthData = new List<object>[100]; //자연채광월정보
            
            List<object>[] FormData = new List<object>[30];

            List<string> chart_final = new List<string>();
            List<string> chart_aux = new List<string>();
            List<string> chart_nd = new List<string>();
            List<string> chart_prod = new List<string>();
           
            int i = -1;
            while (++i < 30)
            {
                zoneData[i] = new List<object>(); //존정보
                daylightData[i] = new List<object>(); //주광정보
                lightData[i] = new List<object>(); //인공조명
                AnnualData[i] = new List<object>(); //연간 결과정보
                renewData[i] = new List<object>(); //집광채광
                dData[i] = new List<object>(); //자연채광
                FormData[i] = new List<object>();//이미지정보
            }
             i = -1;
            while (++i < 100)
            {
                zoneMthData[i] = new List<object>();
             }

            i = -1;
            while (++i < 번호.Length)
            {
                string Num = 번호[i][0];
                items.Add("lighting_report.html"); // 예시 코드: 메인 메뉴 동적 할당, 공간계수는 조명설치높이임
                
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호");
                if (Value.Length > 0)
                {
                    FormData[0].Add(new { idx = i, val = Value[0][0] }); //프로젝트번호
                }
                FormData[1].Add(new { idx = i, val = Num + "_light" }); //메인그림번호
                FormData[2].Add(new { idx = i, val = "L9" }); //높이그림번호 항상나오게함
                


                Value = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "번호,기준조도,너비,길이,순바닥면적,상인방높이,작업면높이,공간계수", "번호 = '" + Num + "'");
                double k = 0, Wr = 0, Lr = 0, hm = 0;

                string titleName = Num + " 조명에너지소요량 검토보고서";
                zoneData[0].Add(new { idx = i, val = titleName }); //명칭
                double area = Convert.ToDouble(Value[0][4].ToString());


                if (Value.Length > 0)
                {
                    //공간계수 구하기
                    Wr = Convert.ToDouble(Value[0][2].ToString());
                    Lr = Convert.ToDouble(Value[0][3].ToString());
                    hm = Convert.ToDouble(Value[0][7].ToString());
                    k = Lr * Wr / (hm * (Lr + Wr));
                }
                string[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존이름,용도프로필,시작시간,종료시간,주이용일,연이용일수,천장고", "존번호 = '" + Num + "'");
                if (Value2.Length > 0)
                {
                    zoneData[1].Add(new { idx = i, val = Value2[0][0] }); //존명칭
                    zoneData[2].Add(new { idx = i, val = Value2[0][1] }); //용도프로필
                    zoneData[3].Add(new { idx = i, val = Value[0][1] }); //조도
                    zoneData[4].Add(new { idx = i, val = Value2[0][2] }); //사용시작시간
                    zoneData[5].Add(new { idx = i, val = Value2[0][3] }); //사용종료시간
                    zoneData[6].Add(new { idx = i, val = Program.UTIL.doubleComa(Value2[0][4].ToString(), 1) }); //주간이용일수
                    zoneData[7].Add(new { idx = i, val = Program.UTIL.doubleComa(Value2[0][5].ToString(), 2) }); //연간이용일수
                    zoneData[8].Add(new { idx = i, val = Program.UTIL.doubleComa(Lr.ToString(), 2) }); //파사드길이
                    zoneData[9].Add(new { idx = i, val = Program.UTIL.doubleComa(k.ToString(), 2) }); //공간계수
                    zoneData[10].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][4].ToString(), 2) }); //바닥 면적
                    zoneData[11].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][5].ToString(), 2) }); //상인방 높이
                    zoneData[12].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][6].ToString(), 2) }); //작업면 높이
                    zoneData[13].Add(new { idx = i, val = Program.UTIL.doubleComa(Value2[0][6].ToString(), 2) }); //천장 높이
                    zoneData[14].Add(new { idx = i, val = Program.UTIL.doubleComa(Value[0][7].ToString(), 2) }); //조명 설치 높이
                }

                string[][] daylight = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "주광길이,주광깊이,주광면적,비주광면적,디밍유형,기준조도", "번호 = '" + Num + "'");
                string[][] daylight2 = Program.DB.getValue(DB.type.ProjDB, "Zone_LightResult", "f_dclass, f_DCA", "번호 = '" + Num + "'");//주광등급,주광율
                string[][] daylight3 = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_주광제어계수", "주광제어계수", "디밍유형 = '" + daylight[0][4].ToString() + "' And 주광이용가능성 = '" + daylight2[0][0].ToString() + "' And Em = '" + daylight[0][5].ToString() + "'");
                double 주광율 = 0;
                if (daylight2.Length > 0)
                {
                    for (int h = 0; h < 12; h++)
                    {
                        주광율 += Convert.ToDouble(daylight2[h][1].ToString()); // 주광율
                    }
                    주광율 = 주광율 / 12;
                }
                string 주광제어계수;
                if (daylight3.Length > 0)
                {
                    주광제어계수 = daylight3[0][0];
                }
                else 주광제어계수 = "해당없음";

                daylightData[0].Add(new { idx = i, val = Program.UTIL.doubleComa(daylight[0][0].ToString(), 2) });//주광길이
                daylightData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(daylight[0][1].ToString(), 2) } ); //주광깊이
                daylightData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(daylight[0][2].ToString(), 2) });//주광면적
                daylightData[3].Add(new { idx = i, val = Program.UTIL.doubleComa(daylight[0][3].ToString(), 2) });//비주광면적
                daylightData[4].Add(new { idx = i, val = daylight2[0][0] }); //주광등급
                daylightData[5].Add(new { idx = i, val = 주광제어계수 }); //주광제어계수
                daylightData[6].Add(new { idx = i, val = string.Format("{0:N2}", 주광율) }); //주광율

                string[][] light = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "램프유형,디밍유형,조명방식,광효율,대기전력,제어방식,조명계수,조명밀도,조도제어계수", "번호 = '" + Num + "'");
                lightData[0].Add(new { idx = i, val = light[0][0] });//등기구종류
                lightData[1].Add(new { idx = i, val = light[0][1] });//디밍유형
                lightData[2].Add(new { idx = i, val = light[0][2] });//설치방식
                lightData[3].Add(new { idx = i, val = Program.UTIL.doubleComa(light[0][3].ToString(), 2) });//광효율
                lightData[4].Add(new { idx = i, val = Program.UTIL.doubleComa(light[0][4].ToString(), 2) });//대기전력
                lightData[5].Add(new { idx = i, val = light[0][5] });//제어방식
                lightData[6].Add(new { idx = i, val = Program.UTIL.doubleComa(light[0][6].ToString(), 2) });//조명계수
                lightData[7].Add(new { idx = i, val = Program.UTIL.doubleComa(light[0][7].ToString(), 2) });//조명밀도
                lightData[8].Add(new { idx = i, val = Program.UTIL.doubleComa(light[0][8].ToString(), 2) });//조명제어계수

                light = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "집광채광종류,집광채광면적,집광채광효율,집광채광향,집광채광각도", "번호 = '" + Num + "'");

                if (light[0][0] == "")
                {
                    renewData[0].Add(new { idx = i, val = "-" });//종류
                    renewData[1].Add(new { idx = i, val = "-" }); ;//면적
                    renewData[2].Add(new { idx = i, val = "-" });//효율
                    renewData[3].Add(new { idx = i, val = "-" });//향
                    renewData[4].Add(new { idx = i, val = "-" });//각도
                }
                else
                {
                    renewData[0].Add(new { idx = i, val = light[0][0] });//종류
                    renewData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(light[0][1].ToString(), 2) }); ;//면적
                    renewData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(light[0][2].ToString(), 2) });//효율
                    renewData[3].Add(new { idx = i, val = light[0][3] });//향
                    renewData[4].Add(new { idx = i, val = light[0][4] });//각도
                }
                 
                
                light = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "주향,주창면적합,주창유리빛투과율,자연채광유형,서브유형,차양", "번호 = '" + Num + "'");
                
                dData[0].Add(new { idx = i, val = light[0][0] });//주향
                dData[1].Add(new { idx = i, val = Program.UTIL.doubleComa(light[0][1].ToString(), 2) }); //창면적합
                dData[2].Add(new { idx = i, val = Program.UTIL.doubleComa(light[0][2].ToString(), 2) }); //빛투과율
                dData[3].Add(new { idx = i, val = light[0][3] });//유형
                dData[4].Add(new { idx = i, val = light[0][4] });//세부유형
               
                string imagetype = null;;
                switch (light[0][4].ToString())
                {
                    case "해당없음":
                        imagetype = "L1";
                        break;
                    case "일반 파사드":
                        imagetype = "L2";
                        break;
                    case "이중외피":
                        imagetype = "L3";
                        break;
                    case "중정":
                        imagetype = "L4";
                        break;
                    case "아트리움":
                        imagetype = "L5";
                        break;
                    case "일반형":
                        imagetype = "L6";
                        break;
                    case "돔형":
                        imagetype = "L7";
                        break;
                    case "톱니형":
                        imagetype = "L8";
                        break;
                    default:
                        break;
                }
                FormData[3].Add(new { idx = i, val = imagetype }); //유형그림번호 서브유형으로 판단함

                dData[5].Add(new { idx = i, val = light[0][5] });//차양제품명

                light = Program.DB.getValue(DB.type.ProjDB, "Zone_LightResult", "f_fd_sa, f_fd_sna", "번호 = '" + Num + "' And 월 ='1월'");

                dData[6].Add(new { idx = i, val = Program.UTIL.doubleComa(light[0][0].ToString(), 2) });//차양가동시투과율
                dData[7].Add(new { idx = i, val = Program.UTIL.doubleComa(light[0][1].ToString(), 2) });//차양미가동시투과율


                light = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "julianday(time(시작시간)),julianday(time(종료시간))", "존번호='" + Num + "'");
                double starttime = Convert.ToDouble(light[0][0]);
                double endtime = Convert.ToDouble(light[0][1]);
                double[] useofdays = new double[12], daytimes = new double[13], nighttimes = new double[13];
                string[] mthday = new string[12], fds = new string[13];
                double[] aux = new double[13], prod = new double[13], outlux = new double[13], final = new double[13], nd = new double[13], lightpower = new double[13], lightload=new double[13];
                double fds_value = 0;
                for (int j = 0; j < 12; j++)
                {
                    double sunrise = 0, sunset = 0;
                    string mth = (j+1).ToString() + "월";
                    light = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "dwd_mth", " 번호 = '" + Num + "' And 난방_냉방 = '냉방' And 비이용일_이용일='이용일' And  월 ='"+mth+"'");
                    mthday[j] = light[0][0].ToString();
                    Value = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_사용시간", "julianday(time(해뜨는시간)),julianday(time(해지는시간))", "ID = '" + (j + 1) + "'");
                    sunrise = Convert.ToDouble(Value[0][0]);
                    sunset = Convert.ToDouble(Value[0][1]);
                    daytimes[j] = daytime(starttime, endtime, sunrise, sunset) * Convert.ToDouble(mthday[j]);
                    nighttimes[j] = nighttime(starttime, endtime, sunrise, sunset) * Convert.ToDouble(mthday[j]);
                    Value = Program.DB.getValue(DB.type.ProjDB, "Zone_LightResult", "f_FDS", "번호 = '" + Num + "' And 월 ='"+mth+"'");
                    fds[j] = Value[0][0].ToString();
                    daytimes[12] += daytimes[j];
                    nighttimes[12] += nighttimes[j];
                    fds_value += Convert.ToDouble(fds[j]);
                    
                    light = Program.DB.getValue(DB.type.ProjDB, "Zone_LightResult", "Aux_kWh,Prod_kWh,OutdoorLux,Final_kWh,Sunlight_SCW,Sunlight_PjSC", " 번호 = '" + Num + "' And  월 ='" + mth + "'");
                    aux[j] = Convert.ToDouble(light[0][0].ToString());
                    prod[j] = Convert.ToDouble(light[0][1].ToString());
                    outlux[j] = Convert.ToDouble(light[0][2].ToString());
                    final[j] = Convert.ToDouble(light[0][3].ToString());
                    lightpower[j] = Convert.ToDouble(light[0][4].ToString());
                    lightload[j] = Convert.ToDouble(light[0][5].ToString());
                    nd[j] = final[j] - aux[j];
                    
                    aux[12] += aux[j];
                    prod[12] += prod[j];
                    outlux[12] += outlux[j];
                    final[12] += final[j];
                    nd[12] += nd[j];
                    lightpower[12] += lightpower[j];
                    lightload[12] += lightload[j];
                }
                fds[12] = (fds_value / 12).ToString();
                outlux[12] = outlux[12] / 12;
                lightpower[12] = lightpower[12] / 12;
                lightload[12] = lightload[12] / 12;
                               

                for (int mth = 0; mth < 13; mth++)
                {
                    if(mth == 12)
                    {
                        zoneMthData[0].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(daytimes[mth].ToString(), 0) });
                        zoneMthData[1].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(nighttimes[mth].ToString(), 0) });
                        zoneMthData[2].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(fds[mth], 1) });
                        zoneMthData[3].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(outlux[mth].ToString(), 0) });
                        zoneMthData[4].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(lightpower[mth].ToString(), 0) });
                        zoneMthData[5].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(lightload[mth].ToString(), 1) });
                        zoneMthData[6].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(final[mth].ToString(), 0) });
                        zoneMthData[7].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(aux[mth].ToString(), 0) });
                        zoneMthData[8].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(nd[mth].ToString(), 0) });
                        zoneMthData[9].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(prod[mth].ToString(), 0) });
                    }
                    else
                    {
                        zoneMthData[0].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(daytimes[mth].ToString(), 1) });
                        zoneMthData[1].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(nighttimes[mth].ToString(), 1) });
                        zoneMthData[2].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(fds[mth], 2) });
                        zoneMthData[3].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(outlux[mth].ToString(), 0) });
                        zoneMthData[4].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(lightpower[mth].ToString(), 1) });
                        zoneMthData[5].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(lightload[mth].ToString(), 1) });
                        zoneMthData[6].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(final[mth].ToString(), 0) });
                        zoneMthData[7].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(aux[mth].ToString(), 1) });
                        zoneMthData[8].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(nd[mth].ToString(), 0) });
                        zoneMthData[9].Add(new { idx = i * 13 + mth, val = Program.UTIL.doubleComa(prod[mth].ToString(), 0) });
                    }
                }

                AnnualData[0].Add(new { idx = i, val = Program.UTIL.doubleComa((nd[12]/area).ToString(), 2) }); //요구량
                AnnualData[1].Add(new { idx = i, val = Program.UTIL.doubleComa((final[12]/area).ToString(), 2) }); //소요량
                AnnualData[2].Add(new { idx = i, val = Program.UTIL.doubleComa((final[12]*2.75/area).ToString(), 2) }); //1차에너지소요량
                AnnualData[3].Add(new { idx = i, val = Program.UTIL.doubleComa((final[12]*0.4747/area).ToString(), 3) }); //CO2배출량
                

                //html 작성
                data.Add(new { cname = "projectnum", data = FormData[0] });
                data.Add(new { cname = "lightingnum", data = FormData[1] });
                data.Add(new { cname = "lightingHeightnum", data = FormData[2] });
                data.Add(new { cname = "lightingType", data = FormData[3] });

                data.Add(new { cname = "title", data = zoneData[0] });
                data.Add(new { cname = "zone_num", data = zoneData[1] });
                data.Add(new { cname = "zone_profile", data = zoneData[2] });
                data.Add(new { cname = "zone_lux", data = zoneData[3] });
                data.Add(new { cname = "zone_starttime", data = zoneData[4] });
                data.Add(new { cname = "zone_endtime", data = zoneData[5] });
                data.Add(new { cname = "zone_weekuseday", data = zoneData[6] });
                data.Add(new { cname = "zone_yearuseday", data = zoneData[7] });
                data.Add(new { cname = "zone_length", data = zoneData[8] });
                data.Add(new { cname = "zone_sfactor", data = zoneData[9] });
                data.Add(new { cname = "zone_area", data = zoneData[10] });
                data.Add(new { cname = "zone_wintopheight", data = zoneData[11] });
                data.Add(new { cname = "zone_workheight", data = zoneData[12] });
                data.Add(new { cname = "zone_cellingheight", data = zoneData[13] });
                data.Add(new { cname = "zone_lightheight", data = zoneData[14] });

                data.Add(new { cname = "daylight_length", data = daylightData[0] });
                data.Add(new { cname = "daylight_depth", data = daylightData[1] });
                data.Add(new { cname = "daylight_area", data = daylightData[2] });
                data.Add(new { cname = "light_area", data = daylightData[3] });
                data.Add(new { cname = "daylight_class", data = daylightData[4] });
                data.Add(new { cname = "daylight_controalrate", data = daylightData[5] });
                data.Add(new { cname = "daylight_userate", data = daylightData[6] });

                data.Add(new { cname = "light_type", data = lightData[0] });
                data.Add(new { cname = "light_dimm", data = lightData[1] });
                data.Add(new { cname = "light_installtype", data = lightData[2] });
                data.Add(new { cname = "light_power", data = lightData[3] });
                data.Add(new { cname = "light_standby", data = lightData[4] });
                data.Add(new { cname = "light_controltype", data = lightData[5] });
                data.Add(new { cname = "light_flvalue", data = lightData[6] });
                data.Add(new { cname = "light_load", data = lightData[7] });
                data.Add(new { cname = "light_controlvalue", data = lightData[8] });

                data.Add(new { cname = "renew_type", data = renewData[0] });
                data.Add(new { cname = "renew_area", data = renewData[1] });
                data.Add(new { cname = "renew_rate", data = renewData[2] });
                data.Add(new { cname = "renew_direction", data = renewData[3] });
                data.Add(new { cname = "renew_slope", data = renewData[4] });

                data.Add(new { cname = "d_direction", data = dData[0] });
                data.Add(new { cname = "d_area", data = dData[1] });
                data.Add(new { cname = "d_visible", data = dData[2] });
                data.Add(new { cname = "d_type", data = dData[3] });
                data.Add(new { cname = "d_subtype", data = dData[4] });
                data.Add(new { cname = "sundevice_name", data = dData[5] });
                data.Add(new { cname = "sundevice_sa", data = dData[6] });
                data.Add(new { cname = "sundevice_sna", data = dData[7] });

                data.Add(new { cname = "mth_dh", data = zoneMthData[0] });
                data.Add(new { cname = "mth_nh", data = zoneMthData[1] });
                data.Add(new { cname = "mth_dluse", data = zoneMthData[2] });
                data.Add(new { cname = "mth_outlux", data = zoneMthData[3] });
                data.Add(new { cname = "mth_lightpower", data = zoneMthData[4] });
                data.Add(new { cname = "mth_lightload", data = zoneMthData[5] });
                data.Add(new { cname = "mth_final", data = zoneMthData[6] });
                data.Add(new { cname = "mth_aux", data = zoneMthData[7] });
                data.Add(new { cname = "mth_nd", data = zoneMthData[8] });
                data.Add(new { cname = "mth_prod", data = zoneMthData[9] });

                data.Add(new { cname = "annual_nd", data = AnnualData[0] });
                data.Add(new { cname = "annual_final", data = AnnualData[1] });
                data.Add(new { cname = "annual_primary", data = AnnualData[2] });
                data.Add(new { cname = "annual_co2", data = AnnualData[3] });


                //chart 작성

                List<object> final_chart = new List<object>(); //에너지소요량
                List<object> aux_chart = new List<object>(); //에너지소요량
                List<object> nd_chart = new List<object>(); //에너지소요량
                List<object> prod_chart = new List<object>(); //에너지소요량



                for (int mth = 0; mth < 12; mth++)
                {
                    final_chart.Add(Convert.ToDouble(Program.UTIL.doubleComa(final[mth].ToString(), 0)));
                    aux_chart.Add(Convert.ToDouble(Program.UTIL.doubleComa(aux[mth].ToString(), 0)));
                    nd_chart.Add(Convert.ToDouble(Program.UTIL.doubleComa(nd[mth].ToString(), 0)));
                    prod_chart.Add(Convert.ToDouble(Program.UTIL.doubleComa(prod[mth].ToString(), 0)));
                }
                chart_final.Add(System.Text.Json.JsonSerializer.Serialize(final_chart.ToArray()));
                chart_aux.Add(System.Text.Json.JsonSerializer.Serialize(aux_chart.ToArray()));
                chart_nd.Add(System.Text.Json.JsonSerializer.Serialize(nd_chart.ToArray()));
                chart_prod.Add(System.Text.Json.JsonSerializer.Serialize(prod_chart.ToArray()));

                double max = 0;
                var sortedmax = final.Distinct().OrderByDescending(x => x).ToArray();
                if(sortedmax.Length >= 2)
                {
                    max = sortedmax[1] *1.05;
                }
                
                


                int n = ((int)max).ToString().Length;
                max = Convert.ToDouble(String.Format("{0:F0}", max / Math.Pow(10, n - 1))) * Math.Pow(10, n - 1) + Math.Pow(10, n - 1);
                if (charts != "") charts += ",";
                charts += "{data:[" +
                "{type:\"bar\",barPercentage:0.4,label:\"에너지요구량 [kWh]\",data:" + chart_nd[i] + ",borderColor:\"#FFD966\",backgroundColor:\"#FFD966\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"분배설비소요량 [kWh]\",data:" + chart_aux[i] + ",borderColor:\"#9DC3E6\",backgroundColor:\"#9DC3E6\",dash:false}," +
                "{type:\"bar\",barPercentage:0.4,label:\"에너지생산량 [kWh]\",data:" + chart_prod[i] + ",borderColor:\"#A9D18E\",backgroundColor:\"#A9D18E\",dash:false}," +
                "{type:\"line\",yAxisID: 'y',label:\"에너지소요량 [kWh]\",data:" + chart_final[i] + ",borderColor:\"#ED7D31\",backgroundColor:\"#ED7D31\",dash:false, tension: 0.4}," +
                "],max:" + max.ToString() + ",step:100,legend:true,stacked:true}";


                s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
                s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());

                Debug.Print("start");
                
                runScript("init(" + s + "," + s2 + "," + "[" + charts + "])");
                
            }
        }
    }
}

