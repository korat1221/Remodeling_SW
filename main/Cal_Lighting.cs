using main.contents;
using main.subcontents.ConstructionRoof;
using System;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace main
{

    internal class ZoneLight
    {
        //CSV 파일 불러오기 
        //다른 클래스(하위) 객체화해서 Calc
        public string ZoneNum;
        public double Wr, Lr, A, hR, hm, Zone_hLi, Zone_hTa, K;  //존 일반정보 csv 변수
        public string WinNum; //존이름과 일치하는 주창아이디
        public string Location;  //존 용도프로필 csv 변수
        public double Em, KA, FA;

        //public double[] ddaytime = new double[12];   //존 낮시간 csv 변수 
        //public double[] nnighttime = new double[12];  //존 밤시간 csv 변수
        public double Pj, Pn, Fo, Fc, lm_W, wsp,N; //존 인공조명 csv 변수
        public double[] Calc_wsp = new double[12]; //최종 대기전력

        public string facade_di, glass1, facade_shade, facade_dimming; //파사드정보1 csv 변수
        public double Zone_f_Aca, Zone_f_a, Zone_f_b, Zone_f_AD, f_τD65_SNA, K1, K2, K3;
        public double[] Zone_Calc_Ish = new double[12]; //파사드 최종 음영 계수 변수

        public double[] trel_D_SA = new double[12];   //파사드 trel_D_SA csv 변수
        public double[] trel_D_SNA = new double[12];  //파사드 trel_D_SNA csv 변수
        public double[] facade_Vmonth = new double[12];   //파사드 Vmonth_i csv 변수
        public double[] find_facade_Vmonth = new double[12];  //조건에 맞는 월별 분배계수를 찾기 위한 변수

        public double aIn_At, bIn_At, hIn_At, τSh_In_At_D65, Ksh_In_At_1, Ksh_In_At_2, Ksh_In_At_3;  //중정 아트리움 csv 변수
        public string glass2;

        public string glass3; //이중외피 csv 변수 
        public double τSh_In_GDF_D65, Ksh_GDF_1, Ksh_GDF_2, Ksh_GDF_3;

        public string Main, Middle, Sub;  //자연채광 csv 변수 

        public string roof_di, roof_glass, roof_shade, roof_dimming;  //천창1정보 csv 변수
        public double r_Aca, r_aD, r_bD, r_AD, γF, γW, As, Bs, hs, hw, hg, Da, r_τD65_SNA, r_τD65_SA, Kobl_1, Kobl_2, Kobl_3;

        public double[] r_shade = new double[12];   //천창 음영계수 csv 변수

        public string energy_type, energy_di;  //신재생에너지1 csv 변수
        public double energy_inc, energy_area, energy_eff, energy_slope;

        public double[] ext = new double[12];   //외부조도 csv 변수

        public double[] Zone_useofdays = new double[12], Zone_daytime = new double[12], Zone_nighttime = new double[12];
        public double useofdays; //Zone_useofdays 누적
        public double Zone_K, Zone_nearK;
        public double Zone_ITr, Zone_IRD; //facade_general ITr Calc 객체 변수 
        public double Zone_Wi, Zone_Ish_In_At, Zone_Ish_GDF; //facade_shade 객체 변수   
        public double[] Zone_Ish = new double[12];//월별 수직,수평,주변 음영계수 
       

        public double[] Zone_DCA = new double[12];  
        public double[] Zone_D = new double[12];
        public double[] Zone_nearD = new double[12];
        public double Zone_τeff_SNA_j; //facade_FDS 객체 변수 
        public string[] dclass = new string[12];

        public double f_nearEm_SNA;//FD_S_SNA 용 기준조도 근사값 구하는 변수
        public double f_naerEm_DC; //FDC용 기준조도 근사값 구하는 변수

        public double[] Zone_FDS = new double[12];
        public double[] Zone_Facade_FD = new double[12]; //파사드 최종 FD

        public double[] find_fd_sna = new double[12];
        public double[] find_fd_sa = new double[12];
        public double[] find_fd_c = new double[12];//각 조건들에 일치하는 테이블 값을 구하기 위한 변수 
        public double Zone_as_bs, Zone_hs_bs, Zone_hg_hw;  //천창 길이 비 

        public double find_normal_ηR, find_saw_ηR; //일반형 및 돔형 천창 ηR 테이블에서 찾기
        public double find_roof_trel_D_SA, find_roof_trel_D_SNA; // 차양장치 가동 및 미가동 시간 비율 테이블에서 찾기 

        public double Zone_Roof_DSNA, Zone_Roof_DSA; //차양 유무에 따른 평균 주광률 구하기 위한 변수
        public string roof_dclass;

        public double r_nearEm_FDS; // 천창 fds 용 기준조도 근사값 구하기
        public double r_nearEm_DC; //천창 fdc용 기준조도 근사값 구하기 

        public double find_roof_fd_sna, find_roof_fd_sa, find_roof_fd_c; // 천창 FD_S_SNA 및 FD_S_SA 및 FDC 찾기 위한 변수
        public double[] roof_Vmonth = new double[12];   //천창 Vmonth_i csv 변수
        public double[] Zone_Roof_FDS = new double[12];//FDS 구하는 변수 

        public double[] Zone_Roof_FD = new double[12]; //천창 최종 FD

        public double[] Zone_Sunlight_SCW = new double[12];
        public double[] Zone_Sunlight_PjSC = new double[12];
  
        public double[] Zone_Final_kWh = new double[12]; //최종 조명에너지소요량

        //public double[] monthday = new double[12], weekdays = new double[12], sunrise = new double[12], sunset = new double[12]; // 월별 일수, 평일수, 주이용일
        public double[] monthday = new double[12], weekdays = new double[12];
        //public string[] sunrise = new string[12], sunset = new string[12];
        public double[] sunrise = new double[12], sunset = new double[12];
        //public double dayofuse;
        //public string starttime, endtime;
        public double dayofuse, starttime, endtime;
        public string[][] 지역;


        public ZoneLight(String zoneNum) //Zone 생성자 생성
        {
            this.ZoneNum = zoneNum;

            지역 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", ""); 

            for (int i = 0; i < 12; i++)
            {
                //string[][] Valueaaaa = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_외부조도", "외부조도", " 지역명='" + 지역[0][0] + "' and 방향='" + energy_di + "' and 각도='" + energy_slope + "' and 기간 = '"+(i+1)+"월' ");
                String[][] Valueaaaa = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_외부조도", "외부조도", " 지역명 = '" + 지역[0][0] + "' and 방향 = '" + energy_di + "' and 각도 = '" + energy_slope + "' and 기간 = '" + (i + 1) + "월'");
                if (Valueaaaa.Length > 0)
                {
                    ext[i] = Convert.ToDouble(Valueaaaa[0][0]);
                }
            }


            //월별일수, 월별평일수
            for (int i = 0; i < 12; i++)
            {
                String[][] ValueAA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_사용시간", "월별일수,월별평일수,julianday(time(해뜨는시간)),julianday(time(해지는시간))", "ID = '" + (i + 1) + "'");
                if (ValueAA.Length > 0)
                {
                    monthday[i] = Convert.ToDouble(ValueAA[0][0]);
                    weekdays[i] = Convert.ToDouble(ValueAA[0][1]);
                    sunrise[i] = Convert.ToDouble(ValueAA[0][2]);
                    sunset[i] = Convert.ToDouble(ValueAA[0][3]);
                }


                //String[][] ValueAA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_사용시간", "월별일수,월별평일수,해뜨는시간,해지는시간", "ID = '" + (i + 1) + "'");
                //if (ValueAA.Length > 0)
                //{
                //    monthday[i] = Convert.ToDouble(ValueAA[0][0]);
                //    weekdays[i] = Convert.ToDouble(ValueAA[0][1]);
                //    sunrise[i] = Convert.ToDouble(ValueAA[0][2]);
                //    sunset[i] = Convert.ToDouble(ValueAA[0][3]);
                //    //sunrise[i] = Convert.ToDouble(ValueAA[0][2]);
                //    //sunset[i] = Convert.ToDouble(ValueAA[0][3]);
                //}

            }
        }
        public void LoadData_LightGeneral()
        {
            string[][] ValueA = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "번호,너비,길이,순바닥면적,상인방높이,작업면높이,공간계수,기준조도,주창아이디", "번호='" + ZoneNum + "'");
            int kk = -1;
            if (ValueA.Length > 0)
            {
                while (++kk < ValueA.Length)
                {
                    Wr = Convert.ToDouble(ValueA[kk][1]);
                    Lr = Convert.ToDouble(ValueA[kk][2]);
                    A = Convert.ToDouble(ValueA[kk][3]);
                    //hR = Convert.ToDouble(ValueA[kk][4]);
                    //hm = Convert.ToDouble(ValueA[kk][5]);
                    Zone_hLi = Convert.ToDouble(ValueA[kk][4]);
                    Zone_hTa = Convert.ToDouble(ValueA[kk][5]);
                    K = Convert.ToDouble(ValueA[kk][6]);
                    Em = Convert.ToDouble(ValueA[kk][7]);
                    WinNum = ValueA[kk][8];
                }
            }
        }
        public void LoadData_LightSystem()
        {
            string[][] ValueA = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "조명밀도,조명예상전력,재실계수,조도제어계수,광효율,대기전력,조명개수", "번호='" + ZoneNum + "'");
            int kk = -1;
            if (ValueA.Length > 0)
            {
                while (++kk < ValueA.Length)
                {
                    Pj = Convert.ToDouble(ValueA[kk][0]);
                    Pn = Convert.ToDouble(ValueA[kk][1]);
                    Fo = Convert.ToDouble(ValueA[kk][2]);
                    Fc = Convert.ToDouble(ValueA[kk][3]);
                    lm_W = Convert.ToDouble(ValueA[kk][4]);
                    wsp = Convert.ToDouble(ValueA[kk][5]);
                    N = Convert.ToDouble(ValueA[kk][6]);
                }
            }
        }
        public void LoadData_NaturalLight()
        {
            string[][] ValueA = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "번호,자연채광유형,서브유형,집광채광체크", "번호='" + ZoneNum + "'");
            int kk = -1;
            if (ValueA.Length > 0)
            {
                while (++kk < ValueA.Length)
                {
                    Main = ValueA[kk][1];
                    Middle = ValueA[kk][2];
                    Sub = ValueA[kk][3];
                }
            }

            ValueA = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "주향,주창면적합,주광깊이,주광길이,주광면적,주창유리빛투과율,주창유리면적비,차양,디밍유형", "번호='" + ZoneNum + "'");
            kk = -1;
            if (ValueA.Length > 0)
            {
                while (++kk < ValueA.Length)
                {if (ValueA[0][1] == "")
                    { }
                    else
                    {
                        facade_di = ValueA[0][0];
                        Zone_f_Aca = Convert.ToDouble(ValueA[0][1]);
                        Zone_f_a = Convert.ToDouble(ValueA[0][2]);
                        Zone_f_b = Convert.ToDouble(ValueA[0][3]);
                        Zone_f_AD = Convert.ToDouble(ValueA[0][4]);
                        roof_di = ValueA[0][0];
                        r_Aca = Convert.ToDouble(ValueA[0][1]);
                        r_aD = Convert.ToDouble(ValueA[0][2]);
                        r_bD = Convert.ToDouble(ValueA[0][3]);
                        r_AD = Convert.ToDouble(ValueA[0][4]);
                        //glass1 = ValueA[kk][5];
                        f_τD65_SNA = Convert.ToDouble(ValueA[0][5]);
                        r_τD65_SNA = Convert.ToDouble(ValueA[0][5]);
                        r_τD65_SA = Convert.ToDouble(ValueA[0][5]) * 0.5;

                        K1 = Convert.ToDouble(ValueA[0][6]);
                        K2 = 0.9;
                        K3 = 0.9;
                        Kobl_1 = Convert.ToDouble(ValueA[0][6]);
                        Kobl_2 = 0.9;
                        Kobl_3 = 0.9;
                        facade_shade = ValueA[0][7];
                        facade_dimming = ValueA[0][8];
                        roof_shade = ValueA[0][7];
                        roof_dimming = ValueA[0][8];
                    }
                }
            }

            ///////////////// 파사드 차양가동계수 맞는지 다시 확인해보기 
            if (Main == "파사드")
            {
                //차양가동계수
                String[][] Location = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
                //String[][] Blind = Program.DB.querySQL(DB.type.ProjDB, "select a.차양적용 From ZoneEnvelope_3D AS a INNER JOIN ZoneLighting_form AS b ON a.존 = b.번호 where a.존 = '" + ZoneNum + "' AND a.아이디 =  b.주창아이디");
                String[][] Blind = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "차양적용", "존='" + ZoneNum + "' And 번호 ='" + WinNum + "'");
                String[][] BlindValue = Program.DB.getValue(DB.type.ProjDB, "ConstructionBlind", "제어방식2", "번호='" + Blind[0][0] + "'");
                
                
                
                for (int i = 0; i < 12; i++)
                {
                    if (BlindValue.Length > 0)
                    {
                        ValueA = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_차양가동계수_" + BlindValue[0][0], "계수", "지역명= '" + Location[0][0] + "' And 방향 ='" + facade_di + "' And 기간 = '" + (i + 1).ToString() + "월'");
                        if (ValueA.Length > 0)
                        {
                            trel_D_SA[i] = Convert.ToDouble(ValueA[0][0]);
                            trel_D_SNA[i] = 1 - trel_D_SA[i];
                        }
                    }
                    else
                    {
                        trel_D_SA[i] = 1; //차양장치 미가동시 주광 공급계수를 0으로 만들어야 계산상 마이너스 안나옴, 따라서 trel_D_SNA[i]가 0이어야 함
                        trel_D_SNA[i] = 1 - trel_D_SA[i];
                    }
                }
                ValueA = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "번호,파사드길이,파사드너비,파사드높이,파사드유리빛투과율", "번호='" + ZoneNum + "'");
                kk = -1;
                if (ValueA.Length > 0)
                {
                    while (++kk < ValueA.Length)
                    {
                        aIn_At = Convert.ToDouble(ValueA[kk][1]);
                        bIn_At = Convert.ToDouble(ValueA[kk][2]);
                        hIn_At = Convert.ToDouble(ValueA[kk][3]);
                        //glass2 = ValueA[kk][4];
                        τSh_In_At_D65 = Convert.ToDouble(ValueA[kk][4]);
                        τSh_In_GDF_D65 = Convert.ToDouble(ValueA[kk][4]);
                        Ksh_In_At_1 = 0.7;
                        Ksh_In_At_2 = 0.9;
                        Ksh_In_At_3 = 0.9;
                        Ksh_GDF_1 = 0.7;
                        Ksh_GDF_2 = 0.9;
                        Ksh_GDF_3 = 0.9;
                    }
                }

            }
            else 
            {
                ValueA = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "번호,천창유리각,천창수평측면각,천창장변부길이,천창단변부길이,천창수평상부높이", "번호='" + ZoneNum + "'");
                kk = -1;
                if (ValueA.Length > 0)
                {
                    while (++kk < ValueA.Length)
                    {
                        if (ValueA[0][1] == "") { }
                        else
                        {
                            γF = Convert.ToDouble(ValueA[kk][1]);
                            γW = Convert.ToDouble(ValueA[kk][2]);
                            As = Convert.ToDouble(ValueA[kk][3]);
                            Bs = Convert.ToDouble(ValueA[kk][4]);
                            hs = Convert.ToDouble(ValueA[kk][5]);
                            hw = Convert.ToDouble(ValueA[kk][3]);
                            hg = Convert.ToDouble(ValueA[kk][4]);
                        }
                    }
                }

            }
        }
        public void LoadData_Renew()
        {
            string[][] ValueA = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "번호,집광채광번호,집광채광면적,사용자면적,집광채광효율,집광채광향,집광채광각도", "번호='" + ZoneNum + "'");
            int kk = -1;
            if (ValueA.Length > 0)
            {
                while (++kk < ValueA.Length)
                {
                    energy_type = ValueA[kk][1];
                    if (energy_type != null && energy_type != "")
                    {
                        if (energy_type.Contains("DL"))
                        {
                            if (ValueA[0][2] == "")
                            { energy_area = 0; }
                            else { energy_area = Convert.ToDouble(ValueA[0][2]); }
                        }
                        else
                        {
                            if (ValueA[0][3] == "")
                            { energy_area = 0; }
                            else { energy_area = Convert.ToDouble(ValueA[0][3]); }
                        }
                        if (ValueA[kk][4] == "")
                        {
                            energy_eff = 0;
                            energy_di = ValueA[kk][5];
                            energy_slope = 0;
                        }
                        else
                        {
                            energy_eff = Convert.ToDouble(ValueA[kk][4]);
                            energy_di = ValueA[kk][5];
                            energy_slope = Convert.ToDouble(ValueA[kk][6]);
                        }
                    }
                    else { }
                    
                    
                }
            }
        }

        //시간정보 계산
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

        public void Calc_time(String zoneNum)
        {
            Time time = new Time();

            //주이용일 
            String[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "주이용일,julianday(time(시작시간)),julianday(time(종료시간))", "존번호='" + zoneNum + "'");
            //String[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "주이용일,시작시간,종료시간", "존번호='" + zoneNum + "'");

            int kk = -1;
            if (Value.Length > 0)
            {
                while (++kk < Value.Length)
                {
                    dayofuse = Convert.ToDouble(Value[0][0]);
                    //starttime = Value[0][1];
                    //endtime = Value[0][2];
                    starttime = Convert.ToDouble(Value[0][1]);
                    endtime = Convert.ToDouble(Value[0][2]);

                    for (int i = 0; i < 12; i++)
                    {
                        string 주간일수;
                        if(dayofuse == 5.5) { 주간일수 = "주 "+dayofuse+" 일 근무"; }
                        else { 주간일수 = "주 " + dayofuse + ".0 일 근무"; }
                        string[][] value = Program.DB.getValue(DB.type.BaseDB_HCneed, "이용일수", "이용일수", "월= '" + (i+1) + "월' and 주간일수 ='" + 주간일수 + "'");
                        Zone_useofdays[i]  = Convert.ToDouble(value[0][0]);
                        //Zone_useofdays[i] = time.Calc_useofdays(dayofuse, monthday[i], weekdays[i]);
                        Zone_daytime[i] = time.Calc_daytime(starttime, endtime, sunrise[i], sunset[i], Zone_useofdays[i], dayofuse);
                        Zone_nighttime[i] = time.Calc_nighttime(starttime, endtime, sunrise[i], sunset[i], Zone_useofdays[i], dayofuse);
                    }
                }
            }
            //MessageBox.Show(Zone_useofdays[0].ToString());
            //MessageBox.Show(Zone_daytime[0].ToString());
            //MessageBox.Show(Zone_nighttime[0].ToString());
        }

      
        //파사드 계산 
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

        public void Calc_Facade_general()   //다른 클래스(밑에) 객체화 해서 Calc
        {
            if (Main == "파사드" || Main == "")
            {
                Facade_general general = new Facade_general();
                Zone_ITr = general.Calc_ITr(Zone_f_Aca, Zone_f_AD);
                Zone_IRD = general.Calc_IRD(Zone_f_a, Zone_hLi, Zone_hTa);
                //MessageBox.Show("투명도 계수 : " + Zone_ITr + "   " + "존 공간 계수 : " + Zone_IRD);
            }
            else return;
        }

        public void Calc_Facade_shade()

        {
            if (Main == "파사드" || Main == "")
            {
                Facade_shade shade = new Facade_shade();

                ////주변건물 음영 계수 계산
                //if (γSh_lsh < 60)
                //{
                //    Zone_ISh_Ish = shade.Calc_ISh_Ish(γSh_lsh);
                //}
                //else
                //{
                //    Zone_ISh_Ish = 0;
                //}
                ////상부 음영 계수 계산
                //if (γSh_hA < 67.5)
                //{
                //    Zone_ISh_hA = shade.Calc_ISh_hA(γSh_lsh);
                //}
                //else
                //{
                //    Zone_ISh_hA = 0;
                //}
                ////측면 음영 계수 계산
                //Zone_ISh_vA = shade.Calc_ISh_vA(γSh_vA);
                ////Console.WriteLine("주변건물 음영 계수 : " + Zone_ISh_Ish + "  " + "상부 음영 계수 : " + Zone_ISh_hA + "  " + "측면 음영 계수 : " + Zone_ISh_hA);

                //주변*상부*측면 음영계수 [월별상이]
                if (WinNum != null)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        string[][] Shade = Program.DB.getValue(DB.type.ProjDB, "Shade_3D", "음영계수", " 번호 = '" + WinNum + "' and 월 = '" + (i + 1) + "월'");
                        if (Shade.Length > 0)
                        { Zone_Ish[i] = Convert.ToDouble(Shade[0][0]); }
                    }
                }
                else
                {
                    for (int i = 0; i < 12; i++)
                    {
                        Zone_Ish[i] = 1;
                    }
                }

                //Wi 계산
                Zone_Wi = shade.Calc_Wi(hIn_At, aIn_At, bIn_At);
                //Console.WriteLine("중정 및 아트리움 공간 계수 : " + Zone_Wi);

                //Ish,In,At 계산
                if (Middle == "중정")
                {
                    Zone_Ish_In_At = shade.Calc_Ish_In_At(Zone_Wi);
                }
                else if (Middle == "아트리움")
                {
                    Zone_Ish_In_At = shade.Calc_Ish_In_At(τSh_In_At_D65, Ksh_In_At_1, Ksh_In_At_2, Ksh_In_At_3);
                }
                else if (Zone_Wi > 1.8)
                {
                    Zone_Ish_In_At = 0;
                }
                else  // 중정 및 아트리움이 아닌 경우
                {
                    Zone_Ish_In_At = 1;
                }
                //Console.WriteLine("중정 및 아트리움 음영 계수 : " + "  " + Zone_Ish_In_At);

                //Ish,GDF 계산

                if (Middle == "이중외피")
                {
                    Zone_Ish_GDF = shade.Calc_Ish_GDF(τSh_In_GDF_D65, Ksh_GDF_1, Ksh_GDF_2, Ksh_GDF_3);
                }
                else
                {
                    Zone_Ish_GDF = 1;
                }
                //Console.WriteLine("이중외피 음영 계수 : " + "  " + Zone_Ish_GDF);

                //최종 음영계수 계산
                for (int i = 0; i < 12; i++)
                {
                    Zone_Calc_Ish[i] = shade.Calc_Ish_j(Zone_Ish[i], Zone_Ish_In_At, Zone_Ish_GDF);    //최종 음영 계수, 월별로 shade값 다름 shade1~shade12
                    //Console.WriteLine("최종 음영 계수 : " + "  " + Zone_Calc_Ish);
                }


            }
            else return;
        }


        public void Calc_Facade_FDS()
        {
            if (Main == "파사드" || Main == "")
            {
                Facade_FDS FDS = new Facade_FDS();

                //유리 유효 투과율 계산
                Zone_τeff_SNA_j = FDS.Calc_τeff_SNA_j(f_τD65_SNA, K1, K2, K3);
                //Console.WriteLine("유리 유효 투과율 : " + "  " + Zone_τeff_SNA_j);
                
                for (int i = 0; i < 12; i++)
                {    //DCA 계산 
                    Zone_DCA[i] = FDS.Calc_Facade_DCA(Zone_ITr, Zone_IRD, Zone_Calc_Ish[i]);
                    //D 계산
                    Zone_D[i] = FDS.Calc_Facade_D(Zone_τeff_SNA_j, Zone_DCA[i]);
                    //D 근사값 계산
                    Zone_nearD[i] = FDS.Calc_Facade_nearD();
                }
                for (int i = 0; i < 12; i++)
                {
                    //조건에 맞는 값 가져오기
                    String[][] ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_파사드차양미가동주광공급계수", "값", "Em='" + Em + "' AND D = '" + Zone_nearD[i] + "' AND 방위 = '" + facade_di + "'");
                    int kk = -1;
                    if (ValueA.Length > 0)
                    {
                        while (++kk < ValueA.Length)
                        {
                            find_fd_sna[i] = Convert.ToDouble(ValueA[0][0]);
                        }
                    }
                }

                //Dclass
                for (int i = 0; i < 12; i++)
                {
                    if (Zone_DCA[i] < 2)
                    {
                        dclass[i] = "None";
                    }
                    else if (Zone_DCA[i] < 4 && Zone_DCA[i] >= 2)
                    {
                        dclass[i] = "Low";
                    }
                    else if (Zone_DCA[i] < 6 && Zone_DCA[i] >= 4)
                    {
                        dclass[i] = "Medium";
                    }
                    else if (Zone_DCA[i] >= 6)
                    {
                        dclass[i] = "Strong";
                    }
                    else
                    {
                        dclass[i] = "error";
                    }
                }

                for (int i = 0; i < 12; i++)
                {
                   
                    //조건에 맞는 값 가져오기
                    String[][] ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_파사드차양가동주광공급계수", "파사드차양가동주광공급계수", "차양시스템종류='" + facade_shade + "' AND 주광이용가능성 = '" + dclass[i] + "'");
                    int kk = -1;
                    if (ValueA.Length > 0)
                    {
                        while (++kk < ValueA.Length)
                        {
                            find_fd_sa[i] = Convert.ToDouble(ValueA[0][0]);
                        }
                    }
                    //MessageBox.Show(find_fd_sa.ToString());
                }

                //FD_S_SNA 용 기준조도 근사값
                if (Main == "파사드" || Main == "")
                {
                    double[] em_data = { 100, 300, 500, 750, 1000 };
                    double em_target = Em;
                    var em_min = em_data.Min(x => Math.Abs(x - em_target));
                    f_nearEm_SNA = em_data.First(y => Math.Abs(y - em_target) == em_min);
                }
                else
                {
                    f_nearEm_SNA = 0;
                }
                //Console.WriteLine("FDS_기준조도: " + "  " + nearEm_SNA);
                //Console.WriteLine("FD_S_SNA: " + "  " + find_fd_sna);

                //FDS 주광 공급 계수 계산 

                for (int i = 0; i < 12; i++)
                {
                    Zone_FDS[i] = FDS.Calc_Facade_FDS(trel_D_SNA[i], find_fd_sna[i], trel_D_SA[i], find_fd_sa[i]);
                    //Console.WriteLine("파사드" + " " + (i + 1) + "월 주광 공급 계수 : " + "  " + Zone_FDS);
                }

                //FD_C 용 기준조도 근사값 /
                double[] fdcem_data = { 50, 100, 150, 200, 300, 500, 750, 1000 };
                double fdcem_target = Em;
                var fdcem_min = fdcem_data.Min(x => Math.Abs(x - fdcem_target));
                 f_naerEm_DC = fdcem_data.First(y => Math.Abs(y - fdcem_target) == fdcem_min);
                //Console.WriteLine("FDC_기준조도: " + "  " + fdcnearEm_SNA);


                for (int i = 0; i < 12; i++)
                {

                    //조건에 맞는 값 가져오기
                    String[][] ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_주광제어계수", "주광제어계수", "디밍유형='" + facade_dimming + "' AND 주광이용가능성 = '" + dclass[i] + "' AND Em = '" + Em + "'");
                    int kk = -1;
                    if (ValueA.Length > 0)
                    {
                        while (++kk < ValueA.Length)
                        {
                            find_fd_c[i] = Convert.ToDouble(ValueA[0][0]);
                        }
                    }
                }
            }
            else return;
        }

        public void Calc_Facade_FD()
        {
            Facade_FD FD = new Facade_FD();

            //파사드 월별 분배 계수 가져오기 
            if (Main == "파사드")
            {
                for (int i = 0; i < 12; i++)
                {
                    string[][] ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_파사드월별보정", "값", "방위='" + facade_di + "' AND 월 ='" + (i + 1) + "월".ToString() + "'");
                    if (ValueA.Length > 0)
                    {
                        find_facade_Vmonth[i] = Convert.ToDouble(ValueA[0][0]);
                    }
                }
            }
            else if (Main == "" || Main == null)
            {
                for (int i = 0; i < 12; i++)
                {
                    find_facade_Vmonth[i] = 0;
                }
            }
            else;



            if (Main == "파사드" || Main == "" || Main == null)
            {
                //최종 FD 계산
                for (int i = 0; i < 12; i++)
                {
                    Zone_Facade_FD[i] = FD.Calc_Facade_FD(find_facade_Vmonth[i], Zone_FDS[i], find_fd_c[i]);
                    //Console.WriteLine("파사드" + " " + (i + 1) + "월 주광 점유 계수 : " + "  " + Zone_Facade_FD[i]);
                }
            }
            else return;


        }


        // 천창 계산
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

        public void Calc_Roof_general()   //다른 클래스(밑에) 객체화 해서 Calc /  csv 파일 요소 당연히 바꿔줘야지 
        {
            if (Main == "천창")
            {
                Roof_general general = new Roof_general();

                if (Middle == "일반형" || Middle == "돔형")
                {
                    Zone_as_bs = general.Calc_near_as_bs(As,Bs);
                    Zone_hs_bs = general.Calc_near_hs_bs(hs,Bs);
                }
                else if (Middle == "톱니형")
                {
                    Zone_hg_hw = general.Calc_near_hg_hw(hg,hw);  
                }
                else return;

                if (Main == "천창")
                {
                    if (Middle == "일반형" || Middle == "돔형")
                    {
                        //천창 효율 계수
                        //조건에 맞는 값 가져오기
                        string[][] ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_천창일반형ηR", "ηR", "K='" + K + "' AND hs_bs ='" + Zone_hs_bs + "' AND as_bs ='" + Zone_as_bs + "' AND γW ='" + γW + "'");
                        int kk = -1;
                        if (ValueA.Length > 0)
                        {
                            while (++kk < ValueA.Length)
                            {
                                find_normal_ηR = Convert.ToDouble(ValueA[0][0]);
                            }
                        }
                        //MessageBox.Show(find_normal_ηR.ToString());
                    }

                    else if (Middle == "톱니형")
                    {
                        //조건에 맞는 값 가져오기
                        string[][] ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_천창톱니형ηR", "ηR", "K='" + K + "' AND hg_hw ='" + Zone_hg_hw + "' AND γF ='" + γF + "' AND γW ='" + γW + "'");
                        int kk = -1;
                        if (ValueA.Length > 0)
                        {
                            while (++kk < ValueA.Length)
                            {
                                find_saw_ηR = Convert.ToDouble(ValueA[0][0]);
                            }
                        }
                        //MessageBox.Show(find_saw_ηR.ToString());
                    }
                    else return;
                }
            }
            else return;
        }

        public void Calc_Roof_FDS()
        {
            //조건에 맞는 값 가져오기
            String[][] ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_천창주광률", "주광률", "기울기='" + γF + "'");
            int kk = -1;
            if (ValueA.Length > 0)
            {
                while (++kk < ValueA.Length)
                {
                    if (ValueA[0][0] != null && ValueA[0][0] != "")
                    { Da = Convert.ToDouble(ValueA[0][0]); }
                }
            }
            //MessageBox.Show(find_fd_sna.ToString());

            if (Main == "천창")
            {
                Roof_FDS roof_fds = new Roof_FDS();
                //DSNA 계산
                if (Middle == "일반형" || Middle == "돔형")
                {
                    Zone_Roof_DSNA = roof_fds.Calc_Roof_DSNA(Da, r_τD65_SNA, Kobl_1, Kobl_2, Kobl_3, Zone_f_Aca, Zone_f_AD, find_normal_ηR);
                }

                else if (Middle == "톱니형")
                {
                    Zone_Roof_DSNA = roof_fds.Calc_Roof_DSNA(Da, r_τD65_SNA, Kobl_1, Kobl_2, Kobl_3, Zone_f_Aca, Zone_f_AD, find_saw_ηR);
                }
                else
                {
                    Zone_Roof_DSNA = 0;
                }
                //Console.WriteLine("차양이 없을 때 평균 주광률: " + "  " + Zone_Roof_DSNA);


                //DSA 계산

                if (Middle == "일반형" || Middle == "돔형")
                {
                    Zone_Roof_DSA = roof_fds.Calc_Roof_DSA(Da, r_τD65_SA, Kobl_1, Kobl_2, Kobl_3, Zone_f_Aca, Zone_f_AD, find_normal_ηR);
                }

                else if (Middle == "톱니형")
                {
                    Zone_Roof_DSA = roof_fds.Calc_Roof_DSA(Da, r_τD65_SA, Kobl_1, Kobl_2, Kobl_3, Zone_f_Aca, Zone_f_AD, find_saw_ηR);
                }

                else
                {
                    Zone_Roof_DSA = 0;
                }

                //Console.WriteLine("차양이 있을 때 평균 주광률: " + "  " + Zone_Roof_DSA);


                //Dclass판단

                if (roof_shade == "없음")
                {
                    if (Zone_Roof_DSNA < 0.02 && Zone_Roof_DSNA >= 0)
                    {
                        roof_dclass = "None";
                    }
                    else if (Zone_Roof_DSNA >= 0.02 && Zone_Roof_DSNA < 0.04)
                    {
                        roof_dclass = "Low";
                    }
                    else if (Zone_Roof_DSNA >= 0.04 && Zone_Roof_DSNA < 0.07)
                    {
                        roof_dclass = "Medium";
                    }
                    else if (Zone_Roof_DSNA >= 0.07)
                    {
                        roof_dclass = "Strong";
                    }
                    else return;

                }
                else
                {
                    if (Zone_Roof_DSA < 0.05 && Zone_Roof_DSA >= 0)
                    {
                        roof_dclass = "None";
                    }
                    else if (Zone_Roof_DSA >= 0.05 && Zone_Roof_DSA < 0.15)
                    {
                        roof_dclass = "Low";
                    }
                    else if (Zone_Roof_DSA >= 0.15 && Zone_Roof_DSA < 0.25)
                    {
                        roof_dclass = "Medium";
                    }
                    else if (Zone_Roof_DSA >= 0.25)
                    {
                        roof_dclass = "Strong";
                    }
                    else return;
                }

                //Console.WriteLine("천창 주광 이용 가능성: " + "  " + roof_dclass);



                //기준조도(FD,S,SNA,j / FD,S,SA,j용 근사값)

                double[] data = { 100, 300, 500, 750, 1000 };
                double target = Em;
                var min = data.Min(x => Math.Abs(x - target));
                r_nearEm_FDS = data.First(y => Math.Abs(y - target) == min);

                //Console.WriteLine("FDS 기준조도: " + " " + near_fds_em);


                //천창 차양 미가동 
                kk = -1;
                ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_천창차양시간", "trel_D_SNA_j", "방위 ='" + roof_di + "' AND γF ='" + γF + "'");
                if (ValueA.Length > 0)
                {
                    while (++kk < ValueA.Length)
                    {

                        find_roof_trel_D_SNA = Convert.ToDouble(ValueA[0][0]);
                    }
                }
                //MessageBox.Show(find_roof_trel_D_SNA.ToString() );


                //천창 차양 가동 
                kk = -1;
                 ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_천창차양시간", "trel_D_SA_j", "방위 ='" + roof_di + "' AND γF ='" + γF + "'");
                if (ValueA.Length > 0)
                {
                    while (++kk < ValueA.Length)
                    {

                        find_roof_trel_D_SA = Convert.ToDouble(ValueA[0][0]);
                    }
                }
                //MessageBox.Show(find_roof_trel_D_SA.ToString());


                //FDS_SNA 찾기 
                kk = -1;
                ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_천창차양미가동주광공급계수", "천창차양미가동주광공급계수", "방위 ='" + roof_di + "' AND 기울기 ='" + γF + "' AND 주광이용가능성 ='" + roof_dclass + "' AND Em ='" + Em + "'");
                if (ValueA.Length > 0)
                {
                    while (++kk < ValueA.Length)
                    {

                        find_roof_fd_sna = Convert.ToDouble(ValueA[0][0]);
                    }
                }
                //MessageBox.Show(find_roof_fd_sna.ToString());


                //FD_SA 찾기
                kk = -1;
                ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_천창차양가동주광공급계수", "천창차양가동주광공급계수", "방위 ='" + roof_di + "' AND 기울기 ='" + γF + "' AND 주광이용가능성 ='" + roof_dclass + "' AND Em ='" + Em + "'");
                if (ValueA.Length > 0)
                {
                    while (++kk < ValueA.Length)
                    {

                        find_roof_fd_sa = Convert.ToDouble(ValueA[0][0]);
                    }
                }
                //MessageBox.Show(find_roof_fd_sna.ToString());

                //FDS 계산 
                for (int i = 0; i < 12; i++)
                {
                    Zone_Roof_FDS[i] = roof_fds.Calc_Roof_FDS(find_roof_trel_D_SNA, find_roof_fd_sna, find_roof_trel_D_SA, find_roof_fd_sa);
                    //Console.WriteLine("천창" + " " + (i + 1) + "월 주광 점유 계수 : " + "  " + Zone_Roof_FD[i]);
                }
            }
            else return;
        }


        public void Calc_Roof_FD()
        {
            if (Main == "천창")
            {
                Roof_FD roof_fd = new Roof_FD();

                //FD_C 용 기준조도 근사값 /
                double[] rooffdcem_data = { 50, 100, 150, 200, 300, 500, 750, 1000 };
                double rooffdcem_target = Em;
                var rooffdcem_min = rooffdcem_data.Min(x => Math.Abs(x - rooffdcem_target));
                r_nearEm_DC = rooffdcem_data.First(y => Math.Abs(y - rooffdcem_target) == rooffdcem_min);
                //Console.WriteLine("FDC_기준조도: " + "  " + rooffdcnearEm_SNA);


                //조건에 맞는 값 가져오기
                string[][] ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_주광제어계수", "주광제어계수", "디밍유형='" + facade_dimming + "' AND 주광이용가능성 = '" + roof_dclass + "' AND Em = '" + Em + "'");
                int kk = -1;
                if (ValueA.Length > 0)
                {
                    while (++kk < ValueA.Length)
                    {
                        find_roof_fd_c = Convert.ToDouble(ValueA[0][0]);
                    }
                }
                //MessageBox.Show(find_fd_c.ToString());

                for (int i = 0; i < 12; i++)
                {
                    ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_천창월별보정값", "Vmonth", "월 ='" + (i + 1) + "월".ToString() + "'");
                    if (ValueA.Length > 0)
                    {
                        roof_Vmonth[i] = Convert.ToDouble(ValueA[0][0]);
                    }
                }
                //MessageBox.Show(roof_Vmonth[0].ToString());

                //최종 천창 FDS
                for (int i = 0; i < 12; i++)
                {
                    Zone_Roof_FD[i] = roof_fd.Calc_Roof_FD(roof_Vmonth[i], Zone_Roof_FDS[i], find_roof_fd_c);
                    //Console.WriteLine("천창" + " " + (i + 1) + "월 주광 점유 계수 : " + "  " + Zone_Roof_FD[i]);
                }
            }
            else return;
        }


        // 신재생에너지 계산
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
        
        public void Calc_Sunlight_SCW()
        {
            if (Sub == "True")
            {
                Sunlight_SCW sunlight_scw = new Sunlight_SCW();

                for (int i = 0; i < 12; i++)
                {
                    Zone_Sunlight_SCW[i] = sunlight_scw.Calc_SCW(energy_eff, ext[i], energy_area, lm_W);

                    //Console.WriteLine("집광채광" + " " + (i + 1) + "월 전력 : " + "  " + Zone_Sunlight_SCW[i]);
                }
            }
            else return;
        }

        public void Calc_Sunlight_Pj_SC()
        {
            if (Sub == "True")
            {
                Sunlight_PjSC sunlight_pjsc = new Sunlight_PjSC();

                for (int i = 0; i < 12; i++)
                {
                    Zone_Sunlight_PjSC[i] = sunlight_pjsc.Calc_Pj_SC(Pn, Zone_Sunlight_SCW[i], A);

                    //Console.WriteLine("집광채광" + " " + (i + 1) + "월 조명밀도 : " + "  " + Zone_Sunlight_PjSC[i]);
                }
            }
            else return;
        }


        // 최종 조명에너지 소요량 계산
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

        public void Calc_kWh()
        {
            Final_kW final_w = new Final_kW();

            for (int i = 0; i < 12; i++)
            {
                useofdays += Zone_useofdays[i];
            }
            for (int i = 0; i < 12; i++)
            {
                Calc_wsp[i] = ((wsp * N) /1000 / A )*(Zone_useofdays[i] / useofdays);
            }
            //Pci = (Convert.ToDouble(Pci_textBox.Text) * N)/1000/A;

            if (Sub == "True")
            {
                for (int i = 0; i < 12; i++)
                {
                    Zone_Final_kWh[i] = Math.Round(final_w.Calc_W_re_yes(Fc, Zone_Sunlight_PjSC[i], Pj, Fo, Zone_daytime[i], Zone_Facade_FD[i], Zone_Roof_FD[i], Zone_nighttime[i], Calc_wsp[i], A),3);
                    //MessageBox.Show((i + 1) + "월 조명에너지 소요량 : " + " " + Zone_Final_W[i]);
                }
            }

            else if (Sub == "False")
            {
                for (int i = 0; i < 12; i++)
                {
                    Zone_Final_kWh[i] = Math.Round(final_w.Calc_W_re_no(Fc, Pj, Fo, Zone_daytime[i], Zone_Facade_FD[i], Zone_Roof_FD[i], Zone_nighttime[i], Calc_wsp[i], A),3);
                    //MessageBox.Show((i + 1) + "월 조명에너지 소요량 : " + " " + Zone_Final_W[i]);
                }
            }
            else return;
        }
    }

    //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

    public class Time
    {
        double useday,daytime,Light_daytime,nighttime,Light_nighttime;

        //월별이용일수 
        public double Calc_useofdays(double dayofuse, double monthday, double weekdays)
        {
            
            if (dayofuse == 7)
            {
                useday = monthday;
            }
            else
            {
                useday = monthday / weekdays ;
            }
            return useday;
        }
        

        //조명 사용 낮시간 (시간 저거 더블로 가져오는게 맞겠지?)
        //public double Calc_daytime(double starttime, double endtime, double sunrisetime, double sunsettime, double dayofuse, double useday)
       // public double Calc_daytime(string starttime, string endtime, string sunrisetime, string sunsettime, double dayofuse, double useday)
        public double Calc_daytime(double starttime, double endtime, double sunrisetime, double sunsettime, double dayofuse, double useday)
        {
            // TimeSpan ts, ts2;

            // if (starttime == endtime)
            // {
            //     ts = DateTime.Parse(sunsettime) - DateTime.Parse(sunrisetime);
            //     daytime = Double.Parse(ts.Hours.ToString());

            // }
            // else if ((DateTime.Parse(starttime) < DateTime.Parse(sunrisetime)) && (DateTime.Parse(endtime) > DateTime.Parse(sunsettime)) || (DateTime.Parse(starttime)< DateTime.Parse(sunrisetime)) && (DateTime.Parse(endtime) < DateTime.Parse(sunrisetime)) || (DateTime.Parse(starttime) > DateTime.Parse(sunsettime)) && (DateTime.Parse(endtime) > DateTime.Parse(sunsettime)))
            //{
            //     if (DateTime.Parse(starttime) < DateTime.Parse(endtime))
            //     {
            //         ts = DateTime.Parse(sunsettime) - DateTime.Parse(sunrisetime);
            //         daytime = Double.Parse(ts.Hours.ToString());
            //     }
            //     else daytime = 0;
            // }
            // else if (DateTime.Parse(starttime) >= DateTime.Parse(sunrisetime) && DateTime.Parse(starttime) <= DateTime.Parse(sunsettime) && (DateTime.Parse(endtime) < DateTime.Parse(sunrisetime) || DateTime.Parse(endtime) > DateTime.Parse(sunsettime)))
            // {
            //     ts = DateTime.Parse(sunsettime) - DateTime.Parse(starttime);
            //     daytime = Double.Parse(ts.Hours.ToString());
            // }
            // else if (DateTime.Parse(endtime) >= DateTime.Parse(sunrisetime) && DateTime.Parse(endtime) <= DateTime.Parse(sunsettime) && (DateTime.Parse(starttime) < DateTime.Parse(sunrisetime) || DateTime.Parse(starttime) > DateTime.Parse(sunsettime)))
            // {
            //     ts = DateTime.Parse(endtime) - DateTime.Parse(starttime);
            //     daytime = Double.Parse(ts.Hours.ToString());
            // }
            // else if ((DateTime.Parse(starttime) >= DateTime.Parse(sunrisetime) && DateTime.Parse(starttime) <= DateTime.Parse(sunsettime)) && (DateTime.Parse(endtime) >= DateTime.Parse(sunrisetime) && DateTime.Parse(endtime) <= DateTime.Parse(sunsettime)))
            // {
            //     if (DateTime.Parse(starttime) < DateTime.Parse(endtime))
            //     {
            //         ts = DateTime.Parse(endtime) - DateTime.Parse(starttime);
            //         daytime = Double.Parse(ts.Hours.ToString());
            //     }
            //     else if (DateTime.Parse(starttime) > DateTime.Parse(endtime))
            //     {
            //         ts = DateTime.Parse(sunsettime) - DateTime.Parse(starttime);
            //         ts2 = DateTime.Parse(endtime) - DateTime.Parse(sunrisetime);
            //         daytime = Double.Parse(ts.Hours.ToString() + ts2.Hours.ToString());

            //     }
            //     else daytime = 0;
            // }

            if (starttime == endtime)
            {
                daytime = (sunsettime - sunrisetime) * 24;
            }
            else if ((starttime < sunrisetime) && (endtime > sunsettime) || (starttime < sunrisetime) && (endtime < sunrisetime) || (starttime > sunsettime) && (endtime > sunsettime))
            {
                if (starttime < endtime)
                {
                    daytime = (sunsettime - sunrisetime) * 24;
                }
                else daytime = 0;
            }
            else if (starttime >= sunrisetime && starttime <= sunsettime && (endtime < sunrisetime || endtime > sunsettime))
            {
                daytime = (sunsettime - starttime) * 24;
            }
            else if (endtime >= sunrisetime && endtime <= sunsettime && (starttime < sunrisetime || starttime > sunsettime))
            {
                daytime = (endtime - starttime) * 24;
            }
            else if ((starttime >= sunrisetime && starttime <= sunsettime) && (endtime >= sunrisetime && endtime <= sunsettime))
            {
                if (starttime < endtime)
                {
                    daytime = (endtime - starttime) * 24;
                }
                else if (starttime > endtime)
                {
                    daytime = ((sunsettime - starttime) + (endtime - sunrisetime)) * 24;
                }
                else daytime = 0;
            }

            // Light_daytime = daytime * 24 * dayofuse * (useday / 7);
            Light_daytime = daytime * dayofuse * (useday / 7);
            return Light_daytime;
        }
        //조명 사용 밤시간

        //public double Calc_nighttime(string starttime, string endtime, string sunrisetime, string sunsettime, double dayofuse, double useday)
        public double Calc_nighttime(double starttime, double endtime, double sunrisetime, double sunsettime, double dayofuse, double useday)
        {

            //TimeSpan ts, ts2;


            //if (starttime == endtime)
            //{
            //    ts = DateTime.Parse(sunrisetime) - DateTime.Parse(sunsettime);
            //    nighttime = Double.Parse(ts.Hours.ToString() + 24);
            //}
            //else if ((DateTime.Parse(starttime) < DateTime.Parse(sunrisetime)) && (DateTime.Parse(endtime) > DateTime.Parse(sunsettime)) || (DateTime.Parse(starttime) < DateTime.Parse(sunrisetime)) && (DateTime.Parse(endtime) < DateTime.Parse(sunrisetime)) || (DateTime.Parse(starttime) > DateTime.Parse(sunsettime)) && (DateTime.Parse(endtime) > DateTime.Parse(sunsettime)))
            //{
            //    if (DateTime.Parse(starttime) < DateTime.Parse(endtime))
            //    {
            //        ts = DateTime.Parse(sunrisetime) - DateTime.Parse(starttime);
            //        ts2 = DateTime.Parse(endtime) - DateTime.Parse(sunsettime);
            //        nighttime = Double.Parse(ts.Hours.ToString() + ts2.Hours.ToString());

            //    }
            //    else if (DateTime.Parse(starttime) > DateTime.Parse(endtime))
            //    {
            //        ts = -(DateTime.Parse(endtime) - DateTime.Parse(starttime));
            //        nighttime = Double.Parse(ts.Hours.ToString());
            //    }
            //}
            //else if (DateTime.Parse(starttime) >= DateTime.Parse(sunrisetime) && DateTime.Parse(starttime) <= DateTime.Parse(sunsettime) && (DateTime.Parse(endtime) < DateTime.Parse(sunrisetime) || DateTime.Parse(endtime) > DateTime.Parse(sunsettime)))
            //{
            //    if (DateTime.Parse(endtime) < DateTime.Parse(starttime))
            //    {
            //        ts = -(DateTime.Parse(endtime) - DateTime.Parse(sunsettime));
            //        nighttime = Double.Parse(ts.Hours.ToString());
            //    }
            //    else if (DateTime.Parse(endtime) > DateTime.Parse(starttime))
            //    {
            //        ts = (DateTime.Parse(endtime) - DateTime.Parse(sunsettime));
            //        nighttime = Double.Parse(ts.Hours.ToString());
            //    }
            //}
            //else if (DateTime.Parse(endtime) >= DateTime.Parse(sunrisetime) && DateTime.Parse(endtime) <= DateTime.Parse(sunsettime) && (DateTime.Parse(starttime) < DateTime.Parse(sunrisetime) || DateTime.Parse(starttime) > DateTime.Parse(sunsettime)))
            //{
            //    if (DateTime.Parse(endtime) < DateTime.Parse(starttime))
            //    {
            //        ts = -(DateTime.Parse(sunrisetime) - DateTime.Parse(starttime));
            //        nighttime = Double.Parse(ts.Hours.ToString());
            //    }
            //    else if (DateTime.Parse(endtime) > DateTime.Parse(starttime))
            //    {
            //        ts = (DateTime.Parse(sunrisetime) - DateTime.Parse(starttime));
            //        nighttime = Double.Parse(ts.Hours.ToString());
            //    }
            //}

            //else if ((DateTime.Parse(starttime) >= DateTime.Parse(sunrisetime) && DateTime.Parse(starttime) <= DateTime.Parse(sunsettime)) && (DateTime.Parse(endtime) >= DateTime.Parse(sunrisetime) && DateTime.Parse(endtime) <= DateTime.Parse(sunsettime)))
            //{
            //    if (DateTime.Parse(starttime) > DateTime.Parse(endtime))
            //    {
            //        ts = -(DateTime.Parse(sunrisetime) - DateTime.Parse(sunsettime));
            //        nighttime = Double.Parse(ts.Hours.ToString());
            //    }
            //    else nighttime = 0;
            //}


            if (starttime == endtime)
            {
                nighttime = 24 + ((sunrisetime - sunsettime) * 24);
            }
            else if ((starttime < sunrisetime) && (endtime > sunsettime) || (starttime < sunrisetime) && (endtime < sunrisetime) || (starttime > sunsettime) && (endtime > sunsettime))
            {
                if (starttime < endtime)
                {
                    nighttime = ((sunrisetime - starttime) + (endtime - sunsettime)) * 24;
                }
                else if (starttime > endtime)
                {
                    nighttime = -((endtime - starttime) * 24);
                }
            }
            else if (starttime >= sunrisetime && starttime <= sunsettime && (endtime < sunrisetime || endtime > sunsettime))
            {
                if (endtime < starttime)
                {
                    nighttime = -((endtime - sunsettime) * 24);
                }
                else if (endtime > starttime)
                {
                    nighttime = (endtime - sunsettime) * 24;
                }
            }
            else if (endtime >= sunrisetime && endtime <= sunsettime && (starttime < sunrisetime || starttime > sunsettime))
            {
                if (endtime < starttime)
                {
                    nighttime = -((sunrisetime - starttime) * 24);
                }
                else if (endtime > starttime)
                {
                    nighttime = (sunrisetime - starttime) * 24;
                }
            }
            else if ((starttime >= sunrisetime && starttime <= sunsettime) && (endtime >= sunrisetime && endtime <= sunsettime))
            {
                if (starttime > endtime)
                {
                    nighttime = -((sunrisetime - sunsettime) * 24);
                }
                else nighttime = 0;
            }
            // Light_nighttime = nighttime * 24 * dayofuse * (useday / 7);
            Light_nighttime = nighttime * dayofuse * (useday / 7);
            return Light_nighttime;
        }

    }

    //public class Form_general
    //{
    //    double K, near_K;
    //    //K계산
    //    public double Calc_K(double Wr, double Lr, double hm)
    //    {
    //        K = Lr * Wr / (hm * (Lr + Wr));
    //        return K;
    //    }

    //    //K근사값 계산
    //    public double Calc_nearK()
    //    {
    //        double[] data = { 0.6, 0.8, 1, 1.25, 1.5, 2, 2.5, 3, 4, 5 };
    //        double target = K;
    //        var min = data.Min(x => Math.Abs(x - target));
    //        near_K = data.First(y => Math.Abs(y - target) == min);
    //        return near_K;
    //    }
    //}

    public class Facade_general
    {
        public double Calc_ITr(double f_Aca, double f_AD)
        {
            if (f_Aca != 0)
            {
                double ITr;
                ITr = f_Aca / f_AD;
                return ITr;
            }
            else
            {
                double ITr;
                ITr = 0;
                return ITr;
            }
            
        }
        public double Calc_IRD(double f_aD, double f_hLi, double f_hTa)
        {
            if (f_aD != 0)
            {
                double IRD;
                IRD = f_aD / (f_hLi - f_hTa);
                return IRD;
            }
            else
            {
                double IRD;
                IRD = 0;
                return IRD;

            }
        }
    }

    public class Facade_shade
    {
        ////주변건물 음영 계수
        //public double Calc_ISh_Ish(double γSh_lsh)
        //{
        //    double ISh_Ish;
        //    ISh_Ish = Math.Cos(Math.PI / 180 *(1.5 * γSh_lsh));

        //    return ISh_Ish;
        //}
        ////상부 음영 계수
        //public double Calc_ISh_hA(double γSh_hA)
        //{
        //    double ISh_hA;
        //    ISh_hA = Math.Cos(Math.PI / 180 * (1.33 *γSh_hA));

        //    return ISh_hA;
        //}
        ////측면 음영 계수
        //public double Calc_ISh_vA(double γSh_vA)
        //{
        //    double ISh_vA;
        //    ISh_vA = 1- γSh_vA / 300;

        //    return ISh_vA;
        //}
        //  중정 및 아트리움 공간 계수
        public double Calc_Wi(double hIn_At, double aIn_At, double bIn_At)  //공간계수 계산
        {
            double Wi;
            Wi = (hIn_At * (aIn_At + bIn_At)) / (2 * aIn_At * bIn_At);

            return Wi;
        }


        //중정 및 아트리움 음영계수
        public double Calc_Ish_In_At(double Wi) //중정일 경우
        {
            double Ish_In_At;
            Ish_In_At = 1 - 0.85 * Wi;
            return Ish_In_At;
        }
        public double Calc_Ish_In_At(double τSh_In_At_D65, double Ksh_In_At_1, double Ksh_In_At_2, double Ksh_In_At_3) //아트리움일 경우
        {
            double Ish_In_At;
            Ish_In_At = τSh_In_At_D65 * Ksh_In_At_1 * Ksh_In_At_2 * Ksh_In_At_3;
            return Ish_In_At;
        }

        //이중외피 음영계수
        public double Calc_Ish_GDF(double τSh_In_GDF_D65, double Ksh_GDF_1, double Ksh_GDF_2, double Ksh_GDF_3)
        {
            double Ish_GDF;

            Ish_GDF = τSh_In_GDF_D65 * Ksh_GDF_1 * Ksh_GDF_2 * Ksh_GDF_3;
            return Ish_GDF;
        }

        //최종 음영 계수 
        public double Calc_Ish_j(double ISh_Ish, double Ish_In_At, double Ish_GDF)   //Ish_Ish_hA_vA는 월별 주변&상부&측면 음영계수 
        {
            double Ish_j;
            Ish_j = ISh_Ish * Ish_In_At * Ish_GDF;
            return Ish_j;
        }
    }


    public class Facade_FDS
    {
        double Dmonth;
        public double Calc_τeff_SNA_j(double τSh_D65_SNA, double K1, double K2, double K3)
        {
            double τeff_SNA_j;
            τeff_SNA_j = τSh_D65_SNA * K1 * K2 * K3;
            return τeff_SNA_j;
        }

        public double Calc_Facade_DCA(double ITr, double IRD, double Ish)
        {
            double DCA;
            //DCA 구하기 
            DCA = (4.13 + 20.0 * ITr - 1.36 * IRD) * Ish;
            return DCA; 
        }

        public double Calc_Facade_D(double τeff_SNA_j,double DCA)
        {
            Dmonth = τeff_SNA_j * DCA;
            return Dmonth;
        }

        public double Calc_Facade_nearD()
        {
            double[] data = { 0.125, 0.5, 1, 1.5, 2, 3, 5, 8, 12, 18 };
            double target = Dmonth;
            var min = data.Min(x => Math.Abs(x - target));
            var D_closet = data.First(y => Math.Abs(y - target) == min);
            return D_closet;
        }

        public double Calc_Facade_FDS(double trel_D_SNA, double fd_sna, double trel_D_SA, double fd_sa)
        {
            double FDS;
            FDS = trel_D_SNA * fd_sna + trel_D_SA * fd_sa;
            return FDS;
        }
    }


    public class Facade_FD
    {
        public double Calc_Facade_FD(double vmonth, double fds, double fdc)
        {
            double FD;
            FD = 1 - vmonth * fds * fdc;
            return FD;

        }
    }

    public class Roof_general
    {
        public double as_bs, near_as_bs;
        public double hs_bs, near_hs_bs;
        public double hg_hw, near_hg_hw;

        public double Calc_near_as_bs(double r_as, double r_bs)  //as/bs 근사값 구하기 
        {
            as_bs = r_as / r_bs;

            double[] data = { 1, 2, 5 };
            double target = as_bs;
            var min = data.Min(x => Math.Abs(x - target));
            near_as_bs = data.First(y => Math.Abs(y - target) == min);

            //Console.WriteLine(near_as_bs);
            return (near_as_bs);
        }

        public double Calc_near_hs_bs(double r_hs,double r_bs)  //hs/bs 근사값 구하기 
        {
            hs_bs = r_hs / r_bs;

            double[] data = { 0.25, 0.5 };
            double target = hs_bs;
            var min = data.Min(x => Math.Abs(x - target));
            near_hs_bs = data.First(y => Math.Abs(y - target) == min);

            //Console.WriteLine(near_hs_bs);
            return (near_hs_bs);
        }

        public double Calc_near_hg_hw(double r_hg, double r_hw)  //hg/hw 근사값 구하기 
        {
            hg_hw = r_hg / r_hw;

            double[] data = { 1, 0.5 };
            double target = hg_hw;
            var min = data.Min(x => Math.Abs(x - target));
            near_hg_hw = data.First(y => Math.Abs(y - target) == min);

            //Console.WriteLine(near_hg_hw);
            return (near_hg_hw);
        }
    }

    public class Roof_FDS
    {
        public double Calc_Roof_DSNA(double D, double D65_SNA, double K1, double K2, double K3, double Aca, double AD, double nr)
        {
            double DSNA;
            DSNA = D * D65_SNA * K1 * K2 * K3 * (Aca / AD) * nr;
            return DSNA;
        }


        public double Calc_Roof_DSA(double D, double D65_SA, double K1, double K2, double K3, double Aca, double AD, double nr)
        {
            double DSA;
            DSA = D * D65_SA * K1 * K2 * K3 * (Aca / AD) * nr;
            return DSA;
        }

        public double Calc_Roof_FDS(double trel_D_SNA, double FDS_SNA, double trel_D_SA, double FDS_SA)
        {
            double FDS;
            FDS = trel_D_SNA * FDS_SNA + trel_D_SA * FDS_SA;
            return FDS;
        }
    }

    public class Roof_FD
    {
        public double Calc_Roof_FD(double vmonth, double FDS, double FDC)
        {
            double FD;
            FD = 1 - vmonth * FDS * FDC;
            return FD;  
        }
    }

    public class Sunlight_SCW
    {
        public double Calc_SCW(double eff, double ext, double area, double lm_W)
        {
            double SCW;
            SCW = (eff * ext * area) / lm_W;
            return SCW;
        }
    }

    public class Sunlight_PjSC
    {
        public double Calc_Pj_SC(double Pn, double scw, double a)
        {
            double Pj;
            Pj = Math.Max((Pn - scw), 0) / a;
            return Pj;
        }
    }


    public class Final_kW
    {
        public double Calc_W_re_yes(double Fc, double Pj_SC, double P, double Fo, double daytime, double facade_FD, double roof_FD, double nighttime, double wsp, double A)
        {
            double W1;
            W1 = ((Fc * (Pj_SC / 1000)) * Fo * daytime * (facade_FD + roof_FD) +
                (Fc * (P / 1000) * Fo * nighttime) + wsp ) * A; 
            
            return W1;
        }
        public double Calc_W_re_no(double Fc, double P, double Fo, double daytime, double facade_FD, double roof_FD, double nighttime, double wsp, double A)
        {
            double W2;
            W2 = ((Fc * (P / 1000)) * Fo * daytime * (facade_FD + roof_FD) +
                (Fc * (P / 1000) * Fo * nighttime) + wsp) * A;
            return W2;
        }
    }
}












