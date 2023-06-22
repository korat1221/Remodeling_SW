using System;
using System.IO;
using System.Linq;
using System.Text;

namespace main
{

    internal class ZoneLight
    {
        //CSV 파일 불러오기 
        //다른 클래스(하위) 객체화해서 Calc

        public double Wr, Lr, A, hR, hm, Zone_hLi, Zone_hTa, K;  //존 일반정보 csv 변수

        public string Location;  //존 용도프로필 csv 변수
        public double Em, KA, FA;

        
        public double[] daytime = new double[12];   //존 낮시간 csv 변수 
        public double[] nighttime = new double[12];  //존 밤시간 csv 변수
        public double Pj, Pn, Fo, Fc, lm_W, wsp; //존 인공조명 csv 변수

        public string facade_di, glass1, facade_shade, facade_dimming; //파사드정보1 csv 변수
        public double Zone_f_Aca, Zone_f_a, Zone_f_b, Zone_f_AD, f_τD65_SNA, K1, K2, K3, γSh_lsh, γSh_hA, γSh_vA;
        public double Zone_Calc_Ish; //파사드 최종 음영 계수 변수

        public double[] f_shade = new double[12];  //파사드 음영계수 csv 및 계산 변수 
        public double[] trel_D_SA = new double[12];   //파사드 trel_D_SA csv 변수
        public double[] trel_D_SNA = new double[12];  //파사드 trel_D_SNA csv 변수

        //public String[,] 파사드월별분배 = new string[8, 13];  //파사드 월별 분배계수
        public double[] facade_Vmonth = new double[12];   //파사드 Vmonth_i csv 변수
        public double[] find_facade_Vmonth = new double[12];  //조건에 맞는 월별 분배계수를 찾기 위한 변수

        public double aIn_At, bIn_At, hIn_At, τSh_In_At_D65, Ksh_In_At_1, Ksh_In_At_2, Ksh_In_At_3;  //중정 아트리움 csv 변수
        public string glass2;

        public string glass3; //이중외피 csv 변수 
        public double τSh_In_GDF_D65, Ksh_GDF_1, Ksh_GDF_2, Ksh_GDF_3;

        public string Main, Middle, Sub;  //자연채광 csv 변수 

        public double nearEm_SNA;//FD_S_SNA 용 기준조도 근사값 구하는 변수



        public string roof_di, roof_glass, roof_shade, roof_dimming;  //천창1정보 csv 변수
        public double r_Aca, r_aD, r_bD, r_AD, γF, γW, As, Bs, hs, hw, hg, Da, r_τD65_SNA, r_τD65_SA, Kobl_1, Kobl_2, Kobl_3;

        public double[] r_shade = new double[12];   //천창 음영계수 csv 변수



        public string energy_type, energy_di;  //신재생에너지1 csv 변수
        public double energy_inc, energy_area, energy_eff;

        public double[] ext = new double[12];   //외부조도 csv 변수


        //public String[,] 파사드차양미가동주광공급계수테이블 = new string[400, 4];  //차양 미가동시 주광공급계수 csv 변수 
        //public String[,] 파사드차양가동주광공급계수테이블 = new string[16, 3];  //차양 가동시 주광공급계수 csv 변수
        //public String[,] 주광제어테이블 = new string[96, 4];  //주광제어 csv 변수

        public double Zone_ITr, Zone_IRD; //facade_general ITr Calc 객체 변수 
        public double Zone_ISh_Ish, Zone_ISh_hA, Zone_ISh_vA, Zone_Wi, Zone_Ish_In_At, Zone_Ish_GDF; //facade_shade 객체 변수   

        public double Zone_τeff_SNA_j, Zone_D, Zone_nearD, Zone_DCA, Zone_FDS; //facade_FDS 객체 변수 
        public string dclass;

        public double[] Zone_Facade_FD = new double[12]; //파사드 최종 FD



        public double find_fd_sna, find_fd_sa, find_fd_c;  //각 조건들에 일치하는 테이블 값을 구하기 위한 변수

    
        //public double final_fd;


        public double Zone_as_bs, Zone_hs_bs, Zone_hg_hw;  //천창 길이 비 

        //public String[,] 일반돔형천창계수테이블 = new string[180, 5];  //일반형 및 돔형 천창 ηR
        //public String[,] 톱니형천창계수테이블 = new string[260, 5];  //톱니형 천창 ηR
        //public String[,] 천창차양장치가동시간 = new string[33, 4];  //천창 trel,D,SA,j / trel,D,SNA,j


        public double find_normal_ηR, find_saw_ηR; //일반형 및 돔형 천창 ηR 테이블에서 찾기
        public double find_roof_trel_D_SA, find_roof_trel_D_SNA; // 차양장치 가동 및 미가동 시간 비율 테이블에서 찾기 

        public double Zone_Roof_DSNA, Zone_Roof_DSA; //차양 유무에 따른 평균 주광률 구하기 위한 변수
        public string roof_dclass;


        public double Zone_Roof_FDS;//FDS 구하는 변수 


        public double near_fds_em; // 천창 fds 용 기준조도 근사값 구하기

        //public String[,] 천창차양미가동주광공급계수테이블 = new string[660, 5];  //천창 FD_S_SNA 테이블 
        //public String[,] 천창차양가동주광공급계수테이블 = new string[660, 5];  //천창 FD_S_SA 테이블 

        
        public double find_roof_fd_sna, find_roof_fd_sa, find_roof_fd_c; // 천창 FD_S_SNA 및 FD_S_SA 및 FDC 찾기 위한 변수
        //public double roof_FDS, final_roof_fd ;
        public double[] roof_Vmonth = new double[12];   //천창 Vmonth_i csv 변수

        public double[] Zone_Roof_FD = new double[12]; //천창 최종 FD

        public double[] Zone_Sunlight_SCW = new double[12];
        public double[] Zone_Sunlight_PjSC = new double[12];
  




        public double[] W_re_yes = new double[12]; // 신재생 이용시 최종 조명에너지 소요량 
        public double[] W_re_no = new double[12]; // 신재생 미이용시 최종 조명에너지 소요량 
        public double[] Zone_Final_W = new double[12]; //최종 조명에너지소요량





        public ZoneLight(String zoneNum) //Zone 생성자 생성
        {
            string[][] ValueA = Program.DB.getValue(DB.type.ProjDB, "ZoneLightgeneral", "zoneNum,Wr,Lr,A,hR,hm,hLi,hTa,K", "zoneNum='" + zoneNum + "'");
            int kk = -1;
            while (++kk < ValueA.Length)
            {
                Wr = Convert.ToDouble(ValueA[kk][1]);
                Lr =Convert.ToDouble(ValueA[kk][2]);
                A =Convert.ToDouble(ValueA[kk][3]);
                hR =Convert.ToDouble(ValueA[kk][4]);
                hm =Convert.ToDouble(ValueA[kk][5]);
                Zone_hLi =Convert.ToDouble(ValueA[kk][6]);
                Zone_hTa =Convert.ToDouble(ValueA[kk][7]);
                K =Convert.ToDouble(ValueA[kk][8]);
            }

            


            //존조명일반정보가져오기
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ZoneLightgeneral.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n2 = 0;
            //            while (!sr2.EndOfStream)
            //            {
            //                //Facade _facade = new Facade();
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n2 == 0)
            //                {
            //                }
            //                else
            //                {
            //                    Wr = Convert.ToDouble(token2[1]);
            //                    Lr = Convert.ToDouble(token2[2]);
            //                    A = Convert.ToDouble(token2[3]);
            //                    hR = Convert.ToDouble(token2[4]);
            //                    hm = Convert.ToDouble(token2[5]);
            //                    Zone_hLi = Convert.ToDouble(token2[6]);
            //                    Zone_hTa = Convert.ToDouble(token2[7]);
            //                    K = Convert.ToDouble(token2[8]);
            //                }
            //                n2++;

            //            }

            //            sr2.Close();

            //        }
            //    }

            //}

            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}


            ValueA = Program.DB.getValue(DB.type.ProjDB, "ZoneLightprofile", "Location,Em,KA,FA", "zoneNum='" + zoneNum + "'");
            kk = -1;
            while (++kk < ValueA.Length)
            {
                Location = ValueA[kk][0];
                Em =  Convert.ToDouble(ValueA[kk][1]);
                KA =  Convert.ToDouble(ValueA[kk][2]);
                FA =  Convert.ToDouble(ValueA[kk][3]);
            }
            //MessageBox.Show(FA.ToString());



            // 존 용도프로필 가져오기
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ZoneLightprofile.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n2 = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n2 == 0)
            //                {
            //                }
            //                else
            //                {
            //                    Location = token2[0];
            //                    Em = Convert.ToDouble(token2[1]);
            //                    KA = Convert.ToDouble(token2[2]);
            //                    FA = Convert.ToDouble(token2[3]);


            //                }
            //                n2++;

            //            }
            //            sr2.Close();


            //        }
            //    }

            //}

            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}



           
            kk = -1;
            while (++kk < ValueA.Length)
            {
                for (int i = 0; i < 12; i++)
                {
                    ValueA = Program.DB.getValue(DB.type.ProjDB, "Zonedaytime", "value", "zoneNum='" + zoneNum + "' AND 월 ='"+(i+1).ToString()+"'");
                    daytime[i] = Convert.ToDouble(ValueA[0][0]);
                }

            }
            //MessageBox.Show( daytime[0].ToString());
            //MessageBox.Show( daytime[5].ToString());



            // 존 낮시간 가져오기
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\Zonedaytime.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n2 = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n2 == 0)
            //                {
            //                }
            //                else
            //                {
            //                    for (int i = 0; i < 12; i++)
            //                    {
            //                        daytime[i] = Convert.ToDouble(token2[i + 1]);

            //                    }

            //                }
            //                n2++;

            //            }
            //            sr2.Close();


            //        }
            //    }

            //}

            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}


            kk = -1;
            while (++kk < ValueA.Length)
            {
                for (int i = 0; i < 12; i++)
                {
                    ValueA = Program.DB.getValue(DB.type.ProjDB, "Zonenighttime", "value", "zoneNum='" + zoneNum + "' AND 월 ='" + (i + 1).ToString() + "'");
                    nighttime[i] = Convert.ToDouble(ValueA[0][0]);
                }

            }
            //MessageBox.Show(nighttime[0].ToString() );
            //MessageBox.Show(nighttime[1].ToString());


            // 존 밤시간 가져오기
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\Zonenighttime.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n2 = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n2 == 0)
            //                {
            //                }
            //                else
            //                {
            //                    for (int i = 0; i < 12; i++)
            //                    {
            //                        nighttime[i] = Convert.ToDouble(token2[i + 1]);

            //                    }

            //                }
            //                n2++;

            //            }
            //            sr2.Close();


            //        }
            //    }

            //}

            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}



            ValueA = Program.DB.getValue(DB.type.ProjDB, "Lighting", "Pj,Pn,Fo,Fc,lm_W,Wsp", "zoneNum='" + zoneNum + "'");
            kk = -1;
            while (++kk < ValueA.Length)
            {

                Pj = Convert.ToDouble(ValueA[kk][0]);
                Pn = Convert.ToDouble(ValueA[kk][1]);
                Fo = Convert.ToDouble(ValueA[kk][2]);
                Fc = Convert.ToDouble(ValueA[kk][3]);
                lm_W = Convert.ToDouble(ValueA[kk][4]);
                wsp = Convert.ToDouble(ValueA[kk][5]);
                
            }
            //MessageBox.Show(Pj.ToString());



            // 존 인공조명 가져오기
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\Lighting.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n2 = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n2 == 0)
            //                {
            //                }
            //                else
            //                {
            //                    Pj = Convert.ToDouble(token2[1]);
            //                    Pn = Convert.ToDouble(token2[2]);
            //                    Fo = Convert.ToDouble(token2[3]);
            //                    Fc = Convert.ToDouble(token2[4]);
            //                    lm_W = Convert.ToDouble(token2[5]);
            //                    wsp = Convert.ToDouble(token2[6]);

            //                }
            //                n2++;

            //            }
            //            sr2.Close();


            //        }
            //    }

            //}

            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}



            ValueA = Program.DB.getValue(DB.type.ProjDB, "facade1", "direction,Aca,a,b,AD,glass,τD65_SNA,K1,K2,K3,shade,dimming,γSh_lsh,γSh_hA,γSh_vA", "zoneNum='" + zoneNum + "'");
            kk = -1;
            while (++kk < ValueA.Length)
            {

                facade_di = (ValueA[kk][0]);
                Zone_f_Aca = Convert.ToDouble(ValueA[kk][1]);
                Zone_f_a = Convert.ToDouble(ValueA[kk][2]);
                Zone_f_b = Convert.ToDouble(ValueA[kk][3]);
                Zone_f_AD = Convert.ToDouble(ValueA[kk][4]);
                glass1 = ValueA[kk][5];
                f_τD65_SNA = Convert.ToDouble(ValueA[kk][6]);
                K1 = Convert.ToDouble(ValueA[kk][7]);
                K2 = Convert.ToDouble(ValueA[kk][8]);
                K3 = Convert.ToDouble(ValueA[kk][9]);
                facade_shade = ValueA[kk][10];
                facade_dimming = ValueA[kk][11];
                γSh_lsh = Convert.ToDouble(ValueA[kk][12]);
                γSh_hA = Convert.ToDouble(ValueA[kk][13]);
                γSh_vA = Convert.ToDouble(ValueA[kk][14]);

            }
            //MessageBox.Show(facade_di.ToString());


            // 파사드정보1 가져오기
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\facade1.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n2 = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n2 == 0)
            //                {
            //                }
            //                else
            //                {

            //                    facade_di = (token2[1]);
            //                    Zone_f_Aca = Convert.ToDouble(token2[2]);
            //                    Zone_f_aD = Convert.ToDouble(token2[3]);
            //                    Zone_f_bD = Convert.ToDouble(token2[4]);
            //                    Zone_f_AD = Convert.ToDouble(token2[5]);
            //                    glass1 = (token2[6]);
            //                    f_τD65_SNA = Convert.ToDouble(token2[7]);
            //                    K1 = Convert.ToDouble(token2[8]);
            //                    K2 = Convert.ToDouble(token2[9]);
            //                    K3 = Convert.ToDouble(token2[10]);
            //                    facade_shade = (token2[11]);
            //                    facade_dimming = (token2[12]);
            //                    γSh_lsh = Convert.ToDouble(token2[13]);
            //                    γSh_hA = Convert.ToDouble(token2[14]);
            //                    γSh_vA = Convert.ToDouble(token2[15]);



            //                }
            //                n2++;

            //            }
            //            sr2.Close();


            //        }
            //    }

            //}

            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}


                for (int i = 0; i < 12; i++)
                {
                    ValueA = Program.DB.getValue(DB.type.ProjDB, "facade_shade", "value", "zoneNum='" + zoneNum + "' AND 월 ='" + (i + 1).ToString() + "'");
                      f_shade[i] = Convert.ToDouble(ValueA[0][0]);
                }
            //MessageBox.Show(f_shade[0].ToString());



            // 파사드 음영계수 가져오기
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\facade_shade.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n2 = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n2 == 0)
            //                {
            //                }
            //                else
            //                {


            //                    for (int i = 0; i < 12; i++)
            //                    {
            //                        f_shade[i] = Convert.ToDouble(token2[i + 1]);

            //                    }


            //                }
            //                n2++;

            //            }

            //            sr2.Close();


            //        }
            //    }

            //}

            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}



            for (int i = 0; i < 12; i++)
            {
                ValueA = Program.DB.getValue(DB.type.ProjDB, "facade_trel_D_SA", "value", "zoneNum='" + zoneNum + "' AND 월 ='" + (i + 1).ToString() + "'");
                trel_D_SA[i] = Convert.ToDouble(ValueA[0][0]);
            }
           // MessageBox.Show(trel_D_SA[0].ToString());




            // 파사드 trel_D_SA 가져오기
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\facade_trel_D_SA.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    for (int i = 0; i < 12; i++)
            //                    {
            //                        trel_D_SA[i] = Convert.ToDouble(token2[i + 1]);

            //                    }

            //                }
            //                n++;

            //            }
            //            sr2.Close();


            //        }
            //    }

            //}

            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}



            for (int i = 0; i < 12; i++)
            {
                ValueA = Program.DB.getValue(DB.type.ProjDB, "facade_trel_D_SNA", "value", "zoneNum='" + zoneNum + "' AND 월 ='" + (i + 1).ToString() + "'");
                trel_D_SNA[i] = Convert.ToDouble(ValueA[0][0]);
            }
            //MessageBox.Show(trel_D_SNA[0].ToString());



            // 파사드 trel_D_SNA 가져오기
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\facade_trel_D_SNA.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    for (int i = 0; i < 12; i++)
            //                    {
            //                        trel_D_SNA[i] = Convert.ToDouble(token2[i + 1]);

            //                    }

            //                }
            //                n++;

            //            }
            //            sr2.Close();


            //        }
            //    }

            //}

            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}


            ValueA = Program.DB.getValue(DB.type.ProjDB, "Courtyard_Atrium", "zoneNum,aIn_At,bIn_At,hIn_At,glasstype,τSh_In_At_D65,Ksh_In_At_1,Ksh_In_At_2,Ksh_In_At_3", "zoneNum='" + zoneNum + "'");
            kk = -1;
            while (++kk < ValueA.Length)
            {

                aIn_At = Convert.ToDouble(ValueA[kk][1]);
                bIn_At = Convert.ToDouble(ValueA[kk][2]);
                hIn_At = Convert.ToDouble(ValueA[kk][3]);
                glass2 = ValueA[kk][4];
                τSh_In_At_D65 = Convert.ToDouble(ValueA[kk][5]);
                Ksh_In_At_1 = Convert.ToDouble(ValueA[kk][6]);
                Ksh_In_At_2 = Convert.ToDouble(ValueA[kk][7]);
                Ksh_In_At_3 = Convert.ToDouble(ValueA[kk][8]);


            }
            //MessageBox.Show(aIn_At.ToString());



            // 중정 아트리움 정보 가져오기
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\Courtyard_Atrium.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n2 = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n2 == 0)
            //                {
            //                }
            //                else
            //                {


            //                    aIn_At = Convert.ToDouble(token2[1]);
            //                    bIn_At = Convert.ToDouble(token2[2]);
            //                    hIn_At = Convert.ToDouble(token2[3]);
            //                    glass2 = token2[4];
            //                    τSh_In_At_D65 = Convert.ToDouble(token2[5]);
            //                    Ksh_In_At_1 = Convert.ToDouble(token2[6]);
            //                    Ksh_In_At_2 = Convert.ToDouble(token2[7]);
            //                    Ksh_In_At_3 = Convert.ToDouble(token2[8]);

            //                }
            //                n2++;

            //            }
            //            sr2.Close();


            //        }
            //    }

            //}

            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}


            ValueA = Program.DB.getValue(DB.type.ProjDB, "Doubleskin", "zoneNum, glasstype,τSh_In_GDF_D65,Ksh_GDF_1,Ksh_GDF_2,Ksh_GDF_3", "zoneNum='" + zoneNum + "'");
            kk = -1;
            while (++kk < ValueA.Length)
            {
                glass3 = ValueA[kk][1];
                τSh_In_GDF_D65 = Convert.ToDouble(ValueA[kk][2]);
                Ksh_GDF_1 = Convert.ToDouble(ValueA[kk][3]);
                Ksh_GDF_2 = Convert.ToDouble(ValueA[kk][4]);
                Ksh_GDF_3 = Convert.ToDouble(ValueA[kk][5]);
            }

            //MessageBox.Show(glass3.ToString());




            // 이중외피 정보 가져오기
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\Doubleskin.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n2 = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n2 == 0)
            //                {
            //                }
            //                else
            //                {

            //                    glass3 = token2[1];
            //                    τSh_In_GDF_D65 = Convert.ToDouble(token2[2]);
            //                    Ksh_GDF_1 = Convert.ToDouble(token2[3]);
            //                    Ksh_GDF_2 = Convert.ToDouble(token2[4]);
            //                    Ksh_GDF_3 = Convert.ToDouble(token2[5]);

            //                }
            //                n2++;

            //            }
            //            sr2.Close();


            //        }
            //    }

            //}

            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}




            //파사드 차양 미가동시 주광공급계수 테이블 일치 값 정보 가져오기 FD_S_SNA
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\facade_FD_S_SNA.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n2 = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n2 == 0)
            //                {
            //                }
            //                else
            //                {
            //                    for (int k = 0; k < token2.Length; k++)
            //                    {
            //                      파사드차양미가동주광공급계수테이블[n2 - 1, k] = token2[k];
            //                    }
            //                }
            //                n2++;

            //            }

            //        }
            //    }


            //}
            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}



            //파사드 차양 가동시 주광공급계수 테이블 일치 값 정보 가져오기 FD_S_SA
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\facade_FD_S_SA.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    for (int k = 0; k < token2.Length; k++)
            //                    {
            //                        파사드차양가동주광공급계수테이블[n - 1, k] = token2[k];
            //                    }
            //                }
            //                n++;

            //            }


            //        }
            //    }


            //}
            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}






            //파사드 및 천창 주광제어 테이블 일치 값 정보 가져오기 FD_C
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\FD_C.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    for (int k = 0; k < token2.Length; k++)
            //                    {
            //                        주광제어테이블[n - 1, k] = token2[k];
            //                    }
            //                }
            //                n++;

            //            }


            //        }
            //    }


            //}
            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}


            //kk = -1;
            //while (++kk < ValueA.Length)
            //{
            //    for (int i = 0; i < 12; i++)
            //    {
            //        ValueA = Program.DB.getValue(DB.type.ProjDB, "Zonenighttime", "value", "zoneNum='" + zoneNum + "' AND 월 ='" + (i + 1).ToString() + "'");
            //        nighttime[i] = Convert.ToDouble(ValueA[0][0]);
            //    }

            //}


            


            //파사드 Vmonth 가져오기
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\facade_Vmonth_i.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n2 = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n2 == 0)
            //                {
            //                }
            //                else
            //                {
            //                    for (int k = 0; k < token2.Length; k++)
            //                    {
            //                        파사드월별분배[n2 - 1, k] = token2[k];
            //                    }
            //                }
            //                n2++;

            //            }

            //        }
            //    }


            //}
            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}






            ValueA = Program.DB.getValue(DB.type.ProjDB, "NaturalLighting", "zoneNum,Main,Middle,Sub", "zoneNum='" + zoneNum + "'");
            kk = -1;
            while (++kk < ValueA.Length)
            {


                Main = ValueA[kk][1];
                Middle = ValueA[kk][2];
                Sub = ValueA[kk][3];

            }
            //MessageBox.Show(Main.ToString());

            // 자연채광 정보 가져오기
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\NaturalLighting.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n2 = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n2 == 0)
            //                {
            //                }
            //                else
            //                {

            //                    Main = token2[1];
            //                    Middle = token2[2];
            //                    Sub = token2[3];


            //                }
            //                n2++;

            //            }
            //            sr2.Close();


            //        }
            //    }

            //}

            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}




            ValueA = Program.DB.getValue(DB.type.ProjDB, "rooflight1", "zoneNum,direction,Aca,a,b,AD,glasstype,γF,γW,a_s,b_s,hS,hw,hg,Da,τD65_SNA,τD65_SA,Kobl_1,Kobl_2,Kobl_3,shading,dimmingtype", "zoneNum='" + zoneNum + "'");
            kk = -1;
            while (++kk < ValueA.Length)
            {
                roof_di = ValueA[kk][1];
                r_Aca = Convert.ToDouble(ValueA[kk][2]);
                r_aD = Convert.ToDouble(ValueA[kk][3]);
                r_bD = Convert.ToDouble(ValueA[kk][4]);
                r_AD = Convert.ToDouble(ValueA[kk][5]);
                roof_glass = ValueA[kk][6];
                γF = Convert.ToDouble(ValueA[kk][7]);
                γW = Convert.ToDouble(ValueA[kk][8]);
                As = Convert.ToDouble(ValueA[kk][9]);
                Bs = Convert.ToDouble(ValueA[kk][10]);
                hs = Convert.ToDouble(ValueA[kk][11]);
                hw = Convert.ToDouble(ValueA[kk][12]);
                hg = Convert.ToDouble(ValueA[kk][13]);
                Da = Convert.ToDouble(ValueA[kk][14]);
                r_τD65_SNA = Convert.ToDouble(ValueA[kk][15]);
                r_τD65_SA = Convert.ToDouble(ValueA[kk][16]);
                Kobl_1 = Convert.ToDouble(ValueA[kk][17]);
                Kobl_2 = Convert.ToDouble(ValueA[kk][18]);
                Kobl_3 = Convert.ToDouble(ValueA[kk][19]);
                roof_shade = ValueA[kk][20];
                roof_dimming = ValueA[kk][21];

            }
            //MessageBox.Show(roof_dimming.ToString());


            // 천창1 정보 가져오기
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\rooflight1.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n2 = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n2 == 0)
            //                {
            //                }
            //                else
            //                {

            //                    roof_di = token2[1];
            //                    r_Aca = Convert.ToDouble(token2[2]);
            //                    r_aD = Convert.ToDouble(token2[3]);
            //                    r_bD = Convert.ToDouble(token2[4]);
            //                    r_AD = Convert.ToDouble(token2[5]);
            //                    roof_glass = token2[6];
            //                    γF = Convert.ToDouble(token2[7]);
            //                    γW = Convert.ToDouble(token2[8]);
            //                    As = Convert.ToDouble(token2[9]);
            //                    Bs = Convert.ToDouble(token2[10]);
            //                    hs = Convert.ToDouble(token2[11]);
            //                    hw = Convert.ToDouble(token2[12]);
            //                    hg = Convert.ToDouble(token2[13]);
            //                    Da = Convert.ToDouble(token2[14]);
            //                    r_τD65_SNA = Convert.ToDouble(token2[15]);
            //                    r_τD65_SA = Convert.ToDouble(token2[16]);
            //                    Kobl_1 = Convert.ToDouble(token2[17]);
            //                    Kobl_2 = Convert.ToDouble(token2[18]);
            //                    Kobl_3 = Convert.ToDouble(token2[19]);
            //                    roof_shade = token2[20];
            //                    roof_dimming = token2[21];



            //                }
            //                n2++;

            //            }
            //            sr2.Close();


            //        }
            //    }

            //}

            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}





            for (int i = 0; i < 12; i++)
            {
                ValueA = Program.DB.getValue(DB.type.ProjDB, "rooflight_shade", "value", "zoneNum='" + zoneNum + "' AND 월 ='" + (i + 1).ToString() + "'");
                r_shade[i] = Convert.ToDouble(ValueA[0][0]);
            }
            //MessageBox.Show(r_shade[0].ToString());


            //// 천창 음영계수 가져오기
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\rooflight_shade.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n2 = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n2 == 0)
            //                {
            //                }
            //                else
            //                {
            //                    for (int i = 0; i < 12; i++)
            //                    {
            //                        r_shade[i] = Convert.ToDouble(token2[i + 1]);

            //                    }

            //                }
            //                n2++;

            //            }
            //            sr2.Close();


            //        }
            //    }

            //}

            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}



            ValueA = Program.DB.getValue(DB.type.ProjDB, "renewable_energy_1", "zoneNum,energytype,direction,inc,area,eff", "zoneNum='" + zoneNum + "'");
            kk = -1;
            while (++kk < ValueA.Length)
            {

                energy_type = ValueA[kk][1];
                energy_di = ValueA[kk][2];
                energy_inc = Convert.ToDouble(ValueA[kk][3]);
                energy_area = Convert.ToDouble(ValueA[kk][4]);
                energy_eff = Convert.ToDouble(ValueA[kk][5]);


            }
            //MessageBox.Show(energy_type.ToString());

            // 신재생에너지1 가져오기
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\renewable_energy_1.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n2 = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n2 == 0)
            //                {
            //                }
            //                else
            //                {
            //                    energy_type = (token2[1]);
            //                    energy_di = (token2[2]);
            //                    energy_inc = Convert.ToDouble(token2[3]);
            //                    energy_area = Convert.ToDouble(token2[4]);
            //                    energy_eff = Convert.ToDouble(token2[5]);



            //                }
            //                n2++;

            //            }
            //            sr2.Close();


            //        }
            //    }

            //}

            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}



            for (int i = 0; i < 12; i++)
            {
                ValueA = Program.DB.getValue(DB.type.ProjDB, "ext_ill", "value", "zoneNum='" + zoneNum + "' AND 월 ='" + (i + 1).ToString() + "'");
                ext[i] = Convert.ToDouble(ValueA[0][0]);
            }
            //MessageBox.Show(ext[0].ToString());




            // 외부조도 가져오기
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ext_ill.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n2 = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n2 == 0)
            //                {
            //                }
            //                else
            //                {
            //                    for (int i = 0; i < 12; i++)
            //                    {
            //                        ext[i] = Convert.ToDouble(token2[i + 1]);

            //                    }

            //                }
            //                n2++;

            //            }
            //            sr2.Close();


            //        }
            //    }

            //}

            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}




           


            //일반형 및 돔형 천창 ηR
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ηR_normal.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    for (int k = 0; k < token2.Length; k++)
            //                    {
            //                        일반돔형천창계수테이블[n - 1, k] = token2[k];
            //                    }
            //                }
            //                n++;

            //            }

            //        }
            //    }


            //}
            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}





            //톱니형 천창 ηR
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ηR_saw.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    for (int k = 0; k < token2.Length; k++)
            //                    {
            //                        톱니형천창계수테이블[n - 1, k] = token2[k];
            //                    }
            //                }
            //                n++;

            //            }

            //        }
            //    }


            //}
            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}






            ////천창 trel,D,SNA,j & trel,D,SA,j
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\roof_trel_D.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    for (int k = 0; k < token2.Length; k++)
            //                    {
            //                        천창차양장치가동시간[n - 1, k] = token2[k];
            //                    }
            //                }
            //                n++;

            //            }


            //        }
            //    }


            //}
            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}



            //천창 FD_SNA 
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\roof_FD_SNA.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    for (int k = 0; k < token2.Length; k++)
            //                    {
            //                        천창차양미가동주광공급계수테이블[n - 1, k] = token2[k];
            //                    }
            //                }
            //                n++;

            //            }


            //        }
            //    }


            //}
            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}



            //천창 FD_SA 
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\roof_FD_SA.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    for (int k = 0; k < token2.Length; k++)
            //                    {
            //                        천창차양가동주광공급계수테이블[n - 1, k] = token2[k];
            //                    }
            //                }
            //                n++;

            //            }


            //        }
            //    }


            //}
            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        //Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}



            // 천창 Vmonth 가져오기
            //try
            //{
            //    string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\roof_Vmonth_i.csv";
            //    using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            while (sr2.EndOfStream == false)
            //            {
            //                string[] token2 = sr2.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    for (int i = 0; i < 12; i++)
            //                    {
            //                        roof_Vmonth[i] = Convert.ToDouble(token2[i + 1]);

            //                    }


            //                }
            //                n++;

            //            }
            //            sr2.Close();


            //        }
            //    }

            //}

            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}

        }







        //파사드 계산 
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

        public void Calc_Facade_general()   //다른 클래스(밑에) 객체화 해서 Calc

        {
            if (Main == "파사드")
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
            if (Main == "파사드")
            {
                Facade_shade shade = new Facade_shade();


                //주변건물 음영 계수 계산
                if (γSh_lsh < 60)
                {
                    Zone_ISh_Ish = shade.Calc_ISh_Ish(γSh_lsh);
                }
                else
                {
                    Zone_ISh_Ish = 0;
                }


                //상부 음영 계수 계산
                if (γSh_hA < 67.5)
                {
                    Zone_ISh_hA = shade.Calc_ISh_hA(γSh_hA);
                }
                else
                {
                    Zone_ISh_hA = 0;
                }

                //측면 음영 계수 계산
                Zone_ISh_vA = shade.Calc_ISh_vA(γSh_vA);

                //Console.WriteLine("주변건물 음영 계수 : " + Zone_ISh_Ish + "  " + "상부 음영 계수 : " + Zone_ISh_hA + "  " + "측면 음영 계수 : " + Zone_ISh_hA);





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

                Zone_Calc_Ish = shade.Calc_Ish_j(Zone_ISh_Ish, Zone_ISh_hA, Zone_ISh_vA, Zone_Ish_In_At, Zone_Ish_GDF);    //최종 음영 계수, 월별로 shade값 다름 shade1~shade12
                //Console.WriteLine("최종 음영 계수 : " + "  " + Zone_Calc_Ish);

            }
            else return;
        }


        




        public void Calc_Facade_FDS()
        {
            if (Main == "파사드")
            {
                Facade_FDS FDS = new Facade_FDS();


                //유리 유효 투과율 계산
                Zone_τeff_SNA_j = FDS.Calc_τeff_SNA_j(f_τD65_SNA, K1, K2, K3);
                //Console.WriteLine("유리 유효 투과율 : " + "  " + Zone_τeff_SNA_j);


                //DCA 계산 
                Zone_DCA = FDS.Calc_Facade_DCA(Zone_ITr, Zone_IRD, Zone_Calc_Ish);
                //Console.WriteLine("실외 주광률 : " + "  " + Zone_DCA);

                //D 계산
                Zone_D = FDS.Calc_Facade_D(Zone_τeff_SNA_j, Zone_DCA);
                //D 근사값 계산
                Zone_nearD = FDS.Calc_Facade_nearD();
                //Console.WriteLine("실내 주광률: " + "  " + Zone_nearD);


                //조건에 맞는 값 가져오기
                String[][] ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_파사드차양미가동주광공급계수", "값", "Em='" + Em + "' AND D = '" + Zone_nearD + "' AND 방위 = '" + facade_di + "'");
                int kk = -1;
                while (++kk < ValueA.Length)
                {
                    find_fd_sna = Convert.ToDouble(ValueA[0][0]);
                }
                //MessageBox.Show(find_fd_sna.ToString());


                //Dclass
                if (Zone_DCA < 2)
                {
                    dclass = "None";
                }

                else if (Zone_DCA < 4 && Zone_DCA >= 2)
                {
                    dclass = "Low";
                }

                else if (Zone_DCA < 6 && Zone_DCA >= 4)
                {
                    dclass = "Medium";
                }

                else if (Zone_DCA >= 6)
                {
                    dclass = "Strong";
                }

                else
                {
                    dclass = "error";
                }
                //Console.WriteLine("파사드 주광 이용 가능성: " + "  " + dclass);

          

                //조건에 맞는 값 가져오기
                ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_파사드차양가동주광공급계수", "파사드차양가동주광공급계수", "차양시스템종류='" + facade_shade + "' AND 주광이용가능성 = '" + dclass + "'");
                kk = -1;
                while (++kk < ValueA.Length)
                {
                    find_fd_sa = Convert.ToDouble(ValueA[0][0]);
                }
                //MessageBox.Show(find_fd_sa.ToString());




                //FD_S_SNA 용 기준조도 근사값

                if (Main == "파사드")
                {
                    double[] em_data = { 100, 300, 500, 750, 1000 };
                    double em_target = Em;
                    var em_min = em_data.Min(x => Math.Abs(x - em_target));
                    nearEm_SNA = em_data.First(y => Math.Abs(y - em_target) == em_min);

                }
                else
                {
                    nearEm_SNA = 0;
                }
                //Console.WriteLine("FDS_기준조도: " + "  " + nearEm_SNA);




                //Console.WriteLine("FD_S_SNA: " + "  " + find_fd_sna);



                //FD_S_SA 테이블 찾기 
                //for (int nn = 0; nn < 16; nn++)
                //{

                //    if (파사드차양가동주광공급계수테이블[nn, 0] == facade_shade.ToString() && 파사드차양가동주광공급계수테이블[nn, 1] == dclass.ToString())

                //        find_fd_sa = Convert.ToDouble(파사드차양가동주광공급계수테이블[nn, 2]);
                //}

                //Console.WriteLine("FD_S_SA: " + "  " + find_fd_sa);



                //FDS 주광 공급 계수 계산 

                for (int i = 0; i < 12; i++)
                {
                    Zone_FDS = FDS.Calc_Facade_FDS(trel_D_SNA[i], find_fd_sna, trel_D_SA[i], find_fd_sa);
                    //Console.WriteLine("파사드" + " " + (i + 1) + "월 주광 공급 계수 : " + "  " + Zone_FDS);

                }



                //FD_C 용 기준조도 근사값 /
                double[] fdcem_data = { 50, 100, 150, 200, 300, 500, 750, 1000 };
                double fdcem_target = Em;
                var fdcem_min = fdcem_data.Min(x => Math.Abs(x - fdcem_target));
                var fdcnearEm_SNA = fdcem_data.First(y => Math.Abs(y - fdcem_target) == fdcem_min);
                //Console.WriteLine("FDC_기준조도: " + "  " + fdcnearEm_SNA);



                //FD_C 테이블 찾기 
                //for (int nn = 0; nn < 96; nn++)
                //{

                //    if (주광제어테이블[nn, 0] == facade_dimming.ToString() && 주광제어테이블[nn, 1] == dclass.ToString() && 주광제어테이블[nn, 2] == fdcnearEm_SNA.ToString())

                //        find_fd_c = Convert.ToDouble(주광제어테이블[nn, 3]);
                //}

                //Console.WriteLine("FD_C: " + "  " + find_fd_c);


                //조건에 맞는 값 가져오기
                ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_주광제어계수", "주광제어계수", "디밍유형='" + facade_dimming + "' AND 주광이용가능성 = '" + dclass + "' AND Em = '" + Em + "'");
                kk = -1;
                while (++kk < ValueA.Length)
                {
                    find_fd_c = Convert.ToDouble(ValueA[0][0]);
                }
                //MessageBox.Show(find_fd_c.ToString());



            }

            else return;



        }

        public void Calc_Facade_FD()
        {


            if (Main == "파사드")
            {
                Facade_FD FD = new Facade_FD();


                

                //파사드 조건에 맞는 월별 분배계수 가져오기
                //for (int i = 0; i < 12; i++)
                //{
                //    for (int nn = 0; nn < 8; nn++)
                //    {
                //        if (파사드월별분배[nn, 0] == facade_di.ToString())
                //            find_facade_Vmonth[i] = Convert.ToDouble(파사드월별분배[nn, i + 1]);

                //    }
                //    //Console.WriteLine("파사드" + " " + (i + 1) + "월 분배계수 : " + "  " + find_facade_Vmonth[i]);
                //}



                //파사드 월별 분배 계수 가져오기 
                int kk = -1;
                
                //while (++kk < ValueA.Length)
                //{
                    for (int i = 0; i < 12; i++)
                    {
                        string[][] ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_파사드월별보정", "값", "방위='" + facade_di + "' AND 월 ='" + (i + 1) + "월".ToString() + "'");
                    find_facade_Vmonth[i] = Convert.ToDouble(ValueA[0][0]);
                    }

                //}
                //MessageBox.Show(find_facade_Vmonth[1].ToString());






                //최종 FD 계산

                for (int i = 0; i < 12; i++)
                {
                    if (Main == "파사드")
                    {
                        Zone_Facade_FD[i] = FD.Calc_Facade_FD(find_facade_Vmonth[i], Zone_FDS, find_fd_c);
                        //Console.WriteLine("파사드" + " " + (i + 1) + "월 주광 점유 계수 : " + "  " + Zone_Facade_FD[i]);
                    }

                    else
                    {
                        Zone_Facade_FD[i] = 1;
                    }

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
                    Zone_as_bs = general.Calc_near_as_bs();
                    Zone_hs_bs = general.Calc_near_hs_bs();

                }

                else if (Middle == "톱니형")
                {
                    Zone_hg_hw = general.Calc_near_hg_hw();

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

                        while (++kk < ValueA.Length)
                        {
                            find_normal_ηR = Convert.ToDouble(ValueA[0][0]);
                        }
                        //MessageBox.Show(find_normal_ηR.ToString());
                    }

                    else if (Middle == "톱니형")
                    {
                        //조건에 맞는 값 가져오기
                        string[][] ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_천창톱니형ηR", "ηR", "K='" + K + "' AND hg_hw ='" + Zone_hg_hw + "' AND γF ='" + γF + "' AND γW ='" + γW + "'");
                        int kk = -1;

                        while (++kk < ValueA.Length)
                        {
                            find_saw_ηR = Convert.ToDouble(ValueA[0][0]);
                        }
                        //MessageBox.Show(find_saw_ηR.ToString());
                    }

                    else return;
                       


                }



            }
            else return;

        }





        //천창 효율 계수 
        //public void Calc_Roof_ηR()

        //{
        //    if (Main == "천창")
        //    {
        //        if (Middle == "일반형" || Middle == "돔형")
        //        {
        //            for (int nn = 0; nn < 180; nn++)
        //            {

        //                if (일반돔형천창계수테이블[nn, 0] == K.ToString() && 일반돔형천창계수테이블[nn, 1] == γW.ToString() && 일반돔형천창계수테이블[nn, 2] == Zone_hs_bs.ToString() && 일반돔형천창계수테이블[nn, 3] == Zone_as_bs.ToString())

        //                find_normal_ηR = Convert.ToDouble(일반돔형천창계수테이블[nn, 4]);

        //            }
        //            //Console.WriteLine("천창 효율 계수 :" + " " + find_normal_ηR);
        //        }

        //        else if (Middle == "톱니형")
        //        {
        //            for (int nn = 0; nn < 260; nn++)
        //            {

        //                if (톱니형천창계수테이블[nn, 0] == K.ToString() && 톱니형천창계수테이블[nn, 1] == γF.ToString() && 톱니형천창계수테이블[nn, 2] == γW.ToString() && 톱니형천창계수테이블[nn, 3] == Zone_hg_hw.ToString())

        //                find_saw_ηR = Convert.ToDouble(톱니형천창계수테이블[nn, 4]);

        //            }
        //            //Console.WriteLine("천창 효율 계수 :" + " " + find_saw_ηR);
        //        }

        //        else return; 

        //    }

        //    else return;
        //}






        public void Calc_Roof_FDS()
        {
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

                else if (roof_shade == "있음")
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
                near_fds_em = data.First(y => Math.Abs(y - target) == min);

                //Console.WriteLine("FDS 기준조도: " + " " + near_fds_em);





                // 천창 차양 가동/미가동 시간 

                //for (int nn = 0; nn < 33; nn++)
                //{

                //    if (천창차양장치가동시간[nn, 0] == facade_di.ToString() && 천창차양장치가동시간[nn, 1] == γF.ToString())


                //        //Console.WriteLine("차양장치 가동 시간" + " " + 천창차양장치가동시간[nn, 2] + "  " + "차양장치 미가동 시간" + " " + 천창차양장치가동시간[nn, 3]);

                //    find_roof_trel_D_SA = Convert.ToDouble(천창차양장치가동시간[nn, 2]);
                //    find_roof_trel_D_SNA = Convert.ToDouble(천창차양장치가동시간[nn, 3]);

                //}


                //천창 차양 미가동 
                int kk = -1;
                string[][] ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_천창차양시간", "trel_D_SNA_j", "방위 ='" + roof_di + "' AND γF ='" + γF + "'");
                while (++kk < ValueA.Length)
                {

                        find_roof_trel_D_SNA = Convert.ToDouble(ValueA[0][0]);
                }
                //MessageBox.Show(find_roof_trel_D_SNA.ToString() );


                //천창 차양 가동 
                kk = -1;
                 ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_천창차양시간", "trel_D_SA_j", "방위 ='" + roof_di + "' AND γF ='" + γF + "'");
                while (++kk < ValueA.Length)
                {

                    find_roof_trel_D_SA = Convert.ToDouble(ValueA[0][0]);
                }
                //MessageBox.Show(find_roof_trel_D_SA.ToString());



                //FDS_SNA 찾기 
                kk = -1;
                ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_천창차양미가동주광공급계수", "천창차양미가동주광공급계수", "방위 ='" + roof_di + "' AND 기울기 ='" + γF + "' AND 주광이용가능성 ='" + roof_dclass + "' AND Em ='" + Em + "'");
                while (++kk < ValueA.Length)
                {

                    find_roof_fd_sna = Convert.ToDouble(ValueA[0][0]);
                }
                //MessageBox.Show(find_roof_fd_sna.ToString());


                //for (int nn = 0; nn < 660; nn++)
                //{

                //    if (천창차양미가동주광공급계수테이블[nn, 0] == facade_di.ToString() && 천창차양미가동주광공급계수테이블[nn, 1] == γF.ToString() && 천창차양미가동주광공급계수테이블[nn, 2] == roof_dclass.ToString() && 천창차양미가동주광공급계수테이블[nn, 3] == near_fds_em.ToString())

                //    find_roof_fd_sna = Convert.ToDouble(천창차양미가동주광공급계수테이블[nn, 4]);
                //}


                //FD_SA 찾기
                kk = -1;
                ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_천창차양가동주광공급계수", "천창차양가동주광공급계수", "방위 ='" + roof_di + "' AND 기울기 ='" + γF + "' AND 주광이용가능성 ='" + roof_dclass + "' AND Em ='" + Em + "'");
                while (++kk < ValueA.Length)
                {

                    find_roof_fd_sa = Convert.ToDouble(ValueA[0][0]);
                }
                //MessageBox.Show(find_roof_fd_sna.ToString());

                //for (int nn = 0; nn < 660; nn++)
                //{

                //    if (천창차양가동주광공급계수테이블[nn, 0] == facade_di.ToString() && 천창차양가동주광공급계수테이블[nn, 1] == γF.ToString() && 천창차양가동주광공급계수테이블[nn, 2] == roof_dclass.ToString() && 천창차양가동주광공급계수테이블[nn, 3] == near_fds_em.ToString())

                //    find_roof_fd_sa = Convert.ToDouble(천창차양가동주광공급계수테이블[nn, 4]);

                //}



                //FDS 계산 
                Zone_Roof_FDS = roof_fds.Calc_Roof_FDS(find_roof_trel_D_SNA, find_roof_fd_sna,  find_roof_trel_D_SA, find_roof_fd_sa);

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
                var rooffdcnearEm_SNA = rooffdcem_data.First(y => Math.Abs(y - rooffdcem_target) == rooffdcem_min);
                //Console.WriteLine("FDC_기준조도: " + "  " + rooffdcnearEm_SNA);



                //FD_C 테이블 찾기 
                //for (int nn = 0; nn < 96; nn++)
                //{

                //    if (주광제어테이블[nn, 0] == facade_dimming.ToString() && 주광제어테이블[nn, 1] == roof_dclass.ToString() && 주광제어테이블[nn, 2] == rooffdcnearEm_SNA.ToString())

                //        find_roof_fd_c = Convert.ToDouble(주광제어테이블[nn, 3]);
                //}

                //Console.WriteLine("FD_C: " + "  " + find_roof_fd_c);


                //조건에 맞는 값 가져오기
                string[][] ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_주광제어계수", "주광제어계수", "디밍유형='" + facade_dimming + "' AND 주광이용가능성 = '" + roof_dclass + "' AND Em = '" + Em + "'");
                int kk = -1;
                while (++kk < ValueA.Length)
                {
                    find_roof_fd_c = Convert.ToDouble(ValueA[0][0]);
                }
                //MessageBox.Show(find_fd_c.ToString());


                for (int i = 0; i < 12; i++)
                {
                    ValueA = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_천창월별보정값", "Vmonth", "월 ='" + (i + 1) + "월".ToString() + "'");
                    roof_Vmonth[i] = Convert.ToDouble(ValueA[0][0]);
                }
                //MessageBox.Show(roof_Vmonth[0].ToString());


                //최종 천창 FDS
                for (int i = 0; i < 12; i++)
                {
                    Zone_Roof_FD[i] = roof_fd.Calc_Roof_FD(roof_Vmonth[i], Zone_Roof_FDS, find_roof_fd_c);
                    //Console.WriteLine("천창" + " " + (i + 1) + "월 주광 점유 계수 : " + "  " + Zone_Roof_FD[i]);
                }

            }

            else return;

        }





        // 신재생에너지 계산
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
        
        public void Calc_Sunlight_SCW()
        {

            if (Sub == "집광채광 O")
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

            if (Sub == "집광채광 O")
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

        public void Calc_W()
        {
            Final_W final_w = new Final_W();

            if (Sub == "집광채광 O")
            {

                for (int i = 0; i < 12; i++)
                {
                    Zone_Final_W[i] = Math.Round(final_w.Calc_W_re_yes(Fc, Zone_Sunlight_PjSC[i], Pj, Fo, daytime[i], Zone_Facade_FD[i], Zone_Roof_FD[i], nighttime[i], wsp, A),3);
                    MessageBox.Show((i + 1) + "월 조명에너지 소요량 : " + " " + Zone_Final_W[i]);

                }

            }

            else if (Sub == "집광채광 X")
            {

                for (int i = 0; i < 12; i++)
                {
                    Zone_Final_W[i] = Math.Round(final_w.Calc_W_re_no(Fc, Pj, Fo, daytime[i], Zone_Facade_FD[i], Zone_Roof_FD[i], nighttime[i], wsp, A),3);
                    MessageBox.Show((i + 1) + "월 조명에너지 소요량 : " + " " + Zone_Final_W[i]);
                }
            }

            else return;

        }


    }




    public class Facade_general

    {
        public double Calc_ITr(double f_Aca, double f_AD)
        {
            double ITr;

            ITr = f_Aca / f_AD;

            return ITr;

        }


        public double Calc_IRD(double f_aD, double f_hLi, double f_hTa)
        {
            double IRD;

            IRD = f_aD / (f_hLi - f_hTa);

            return IRD;

        }
    }





    public class Facade_shade
    {
        
        //주변건물 음영 계수
        public double Calc_ISh_Ish(double γSh_lsh)
        {
            double ISh_Ish;
            ISh_Ish = Math.Cos(Math.PI / 180 *(1.5 * γSh_lsh));

            return ISh_Ish;
        }

        //상부 음영 계수
        public double Calc_ISh_hA(double γSh_hA)
        {
            double ISh_hA;
            ISh_hA = Math.Cos(Math.PI / 180 * (1.33 *γSh_hA));

            return ISh_hA;
        }

        //측면 음영 계수
        public double Calc_ISh_vA(double γSh_vA)
        {
            double ISh_vA;
            ISh_vA = 1- γSh_vA / 300;

            return ISh_vA;
        }



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
        public double Calc_Ish_j(double ISh_Ish, double ISh_hA, double Ish_vA, double Ish_In_At, double Ish_GDF)   //Ish_Ish_hA_vA는 월별 주변&상부&측면 음영계수 
        {
            double Ish_j;

            Ish_j = ISh_Ish  * ISh_hA * Ish_vA * Ish_In_At * Ish_GDF;

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


        public double Calc_as_bs(double r_as, double r_bs)
        {


            as_bs = r_as / r_bs;
            return as_bs;
        }



        public double Calc_near_as_bs()  //as/bs 근사값 구하기 
        {
            double[] data = { 1, 2, 5 };
            double target = as_bs;
            var min = data.Min(x => Math.Abs(x - target));
            near_as_bs = data.First(y => Math.Abs(y - target) == min);

            //Console.WriteLine(near_as_bs);
            return (near_as_bs);
        }




        public double Calc_hs_bs(double r_hs, double r_bs)
        {


            hs_bs = r_hs / r_bs;
            return hs_bs;
        }


        public double Calc_near_hs_bs()  //hs/bs 근사값 구하기 
        {
            double[] data = { 0.25, 0.5 };
            double target = hs_bs;
            var min = data.Min(x => Math.Abs(x - target));
            near_hs_bs = data.First(y => Math.Abs(y - target) == min);

            //Console.WriteLine(near_hs_bs);
            return (near_hs_bs);
        }





        public double Calc_hg_hw(double r_hg, double r_hw)
        {
            double hg_hw;

            hg_hw = r_hg / r_hw;
            return hg_hw;
        }

        public double Calc_near_hg_hw()  //hg/hw 근사값 구하기 
        {
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


    public class Final_W
    {
        public double Calc_W_re_yes(double Fc, double Pj_SC, double P, double Fo, double daytime, double facade_FD, double roof_FD, double nighttime, double wsp, double A)
        {
            double W1;
            W1 = (Fc * (Pj_SC / 1000)) * Fo * daytime * (facade_FD + roof_FD) +
                (Fc * (P / 1000) * Fo * nighttime + wsp ) * A; 
            
            return W1;
        }


        public double Calc_W_re_no(double Fc, double P, double Fo, double daytime, double facade_FD, double roof_FD, double nighttime, double wsp, double A)
        {
            double W2;
            W2 = (Fc * (P / 1000)) * Fo * daytime * (facade_FD + roof_FD) +
                (Fc * (P / 1000) * Fo * nighttime + wsp) * A;

            return W2;
        }




    }







}












