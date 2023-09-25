using main.contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    internal class RESystem
    {
        #region Input Variable

        //연결 
        PV pv;

        //[일반 정보 변수]
        //화면
        public string ReEnergyNumber, PVname, RenewableEnergySourceType;
        public string PVtype, PVsystem, PVapbipv;

        //[태양광 모듈 변수]
        //화면
        public string PVModulename;
        public double PVwidthnum, PVheightnum;
        //database
        public string PVcelltype, PVmanu_year;
        public double PVwidth_m, PVheight_m, PVKpk_kW_m2, PVPn_W, PVnumber;
        //index
        public double PVmanuyearfa;
        //계산
        public double PVcapacity_Kw, PVArea_m2; //설치용량 수정

        //[인버터 변수]
        //화면
        public string PVInvertername;
        //database
        public double PVInverterηEU;

        //[설치 정보 변수]
        //화면
        public string PVlocal, PVα;
        public double PVβ;
        //계산
        public double PVfperf;

        //[일사량 정보 변수]
        //database
        public double[] PVIs_W_m2 = new double[12];
        public double[] PVdmth = { 31, 28, 31, 30, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public double PVIref_kW_m2 = 1;

        //[배터리 정보 변수]
        //화면
        public string PVbatteryname;
        //database
        public string PVbatterytype;
        public double PVV_V, PVAH_Ah;
        //index
        public double PVηDoD, PVtDIS, PVηBatt;
        //계산
        public double PVCnenn_kW;

        //[매칭계수 정보 변수]
        //database
        public double[] PVEPusel_kWh = new double[12];
        public double PVEPusel_kWh_a;
        //index
        public double PVκ = 1;
        public double PVn = 1;

        //음영계수

        public double PVLshobst_m, PVHshobst_m, PVLPVwid_m, PVLPVlen_m;
        public double[] PVIdirtot_W_m2 = new double[12];
        public double[] PVIdiftot_W_m2 = new double[12];
        public double[] PVαsol = new double[12];

        #endregion / Input Variable

        #region Calculation Variable

        //일사량_kWh/(m2.month)
        public double[] PVEsolm_kWh_m2 = new double[12];

        //최대출력
        public double PVPpk_kW;

        //생성된 전기에너지
        public double[] PVEelpvoutm_kWh = new double[12]; //월별
        public double[] PVEelpvoutm_kWh_m2 = new double[12]; //단위당
        public double PVEelpvouta_kWh; //연간
        public double PVefficiency; //평균효율

        //배터리
        public double PVγQ, PVCeff, PVCQ;

        //계통연계
        public double[] PVEprelusedEPus_kWh = new double[12];
        public double PVfmatch, PVx;


        //독립형
        public double[] PVQfnutzPVi_kWh = new double[12];
        public double PVfBatt, PVQbattlossa_kWh;

        //그리드이동 전기에너지
        public double[] PVEexpelgrid_kWh = new double[12];

        //음영감소계수
        public double[] PVFshobstpvt = new double[12];
        public double[] PVhshobst_m = new double[12];
        public double[] PVhshobstwid_m = new double[12];
        public double[] Ishdirtotpvt_W = new double[12];

        String[][] 지역;

        #endregion / Calculation Variable

        #region INPUT

        public RESystem() //생성자로 class 객체 생성시 INPUT 값 불러오기
        {

            //태양광 정보 불러오기

            //try //일반정보 불러오기
            //{
            //    string filePath = "C:\\Users\\hjyon\\Desktop\\Re_energy\\DBPV\\Re_PV_general.csv";
            //    using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
            //    {
            //        using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            while (!sr.EndOfStream)
            //            {
            //                string[] token = sr.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    ReEnergyNumber = token[1];
            //                    PVname = token[2];
            //                    RenewableEnergySourceType = token[3];
            //                    PVtype = token[4];
            //                    PVsystem = token[5];
            //                    PVapbipv = token[6];
            //                }
            //                n++;
            //            }
            //            sr.Close();

            //        }
            //    }

            //}

            //catch (IOException e)
            //{

            //    if (e.Source != null)
            //        Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}

            //try // 태양광 모듈 정보 불러오기
            //{
            //    string filePath = "C:\\Users\\hjyon\\Desktop\\Re_energy\\DBPV\\Re_PV_module.csv";
            //    using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
            //    {
            //        using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            while (sr.EndOfStream == false)
            //            {
            //                string[] token = sr.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    PVModulename = token[1];
            //                    PVcelltype = token[2];
            //                    PVmanu_year = token[3];
            //                    PVwidth_m = Convert.ToDouble(token[4]);
            //                    PVheight_m = Convert.ToDouble(token[5]);
            //                    PVKpk_kW_m2 = Convert.ToDouble(token[6]);
            //                    PVPn_W = Convert.ToDouble(token[7]);
            //                    PVwidthnum = Convert.ToDouble(token[8]);
            //                    PVheightnum = Convert.ToDouble(token[9]);
            //                    PVnumber = Convert.ToDouble(token[10]);
            //                }
            //                n++;
            //            }
            //            sr.Close();

            //        }
            //    }

            //}

            //catch (IOException e)
            //{

            //    if (e.Source != null)
            //        Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}

            //try // 인버터 정보 불러오기
            //{
            //    string filePath = "C:\\Users\\hjyon\\Desktop\\Re_energy\\DBPV\\Re_PV_INVERTER.csv";
            //    using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
            //    {
            //        using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            while (sr.EndOfStream == false)
            //            {
            //                string[] token = sr.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    PVInvertername = token[1];
            //                    PVInverterηEU = Convert.ToDouble(token[2]);
            //                }
            //                n++;
            //            }
            //            sr.Close();

            //        }
            //    }

            //}

            //catch (IOException e)
            //{

            //    if (e.Source != null)
            //        Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}

            //try // 설치 정보 불러오기
            //{
            //    string filePath = "C:\\Users\\hjyon\\Desktop\\Re_energy\\DBPV\\Re_PV_install.csv";
            //    using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
            //    {
            //        using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            while (sr.EndOfStream == false)
            //            {
            //                string[] token = sr.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    PVlocal = token[1];
            //                    PVα = token[2];
            //                    PVβ = Convert.ToDouble(token[3]);
            //                }
            //                n++;
            //            }
            //            sr.Close();

            //        }
            //    }

            //}

            //catch (IOException e)
            //{

            //    if (e.Source != null)
            //        Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}
           
            try // 전일사량 정보 불러오기
            {
                string filePath = "C:\\Users\\hjyon\\Desktop\\Re_energy\\DBPV\\Re_PV_Irradiance.csv";
                using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
                    {
                        int n = 0;
                        while (sr.EndOfStream == false)
                        {
                            string[] token = sr.ReadLine().Split(',');
                            if (n == 0)
                            {
                            }
                            else
                            {
                                for (int i = 0; i < 12; i++)
                                {
                                    PVIs_W_m2[i] = Convert.ToDouble(token[i + 1]);
                                }
                            }
                            n++;
                        }
                        sr.Close();

                    }
                }

            }

            catch (IOException e)
            {

                if (e.Source != null)
                    Console.WriteLine("IOException source: {0}", e.Source);
                throw;
            }

            //try // 배터리 정보 불러오기
            //{
            //    string filePath = "C:\\Users\\hjyon\\Desktop\\Re_energy\\DBPV\\Re_PV_battery.csv";
            //    using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
            //    {
            //        using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            while (sr.EndOfStream == false)
            //            {
            //                string[] token = sr.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    PVbatteryname = token[1];
            //                    PVbatterytype = token[2];
            //                    PVV_V = Convert.ToDouble(token[3]);
            //                    PVAH_Ah = Convert.ToDouble(token[4]);
            //                }
            //                n++;
            //            }
            //            sr.Close();

            //        }
            //    }

            //}

            //catch (IOException e)
            //{

            //    if (e.Source != null)
            //        Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}

            try // 연간 전기 에너지 소요량 정보 불러오기
            {
                string filePath = "C:\\Users\\hjyon\\Desktop\\Re_energy\\DBPV\\Re_PV_Epusel.csv";
                using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
                    {
                        int n = 0;
                        while (sr.EndOfStream == false)
                        {
                            string[] token = sr.ReadLine().Split(',');
                            if (n == 0)
                            {
                            }
                            else
                            {
                                for (int i = 0; i < 12; i++)
                                {
                                    PVEPusel_kWh[i] = Convert.ToDouble(token[i + 1]);
                                }
                            }
                            n++;
                        }
                        sr.Close();
                    }
                }

            }

            catch (IOException e)
            {

                if (e.Source != null)
                    Console.WriteLine("IOException source: {0}", e.Source);
                throw;
            }

            //try //음영계수 정보 불러오기
            //{
            //    string filePath = "C:\\Users\\hjyon\\Desktop\\Re_energy\\DBPV\\Re_PV_shfactor.csv";
            //    using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
            //    {
            //        using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            while (sr.EndOfStream == false)
            //            {
            //                string[] token = sr.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    PVLshobst_m = Convert.ToDouble(token[1]);
            //                    PVHshobst_m = Convert.ToDouble(token[2]);

            //                }
            //                n++;
            //            }
            //            sr.Close();

            //        }
            //    }

            //}

            //catch (IOException e)
            //{

            //    if (e.Source != null)
            //        Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}

            try // 직달일사량 정보 불러오기
            {
                string filePath = "C:\\Users\\hjyon\\Desktop\\Re_energy\\DBPV\\Re_PV_Idir.csv";
                using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
                    {
                        int n = 0;
                        while (sr.EndOfStream == false)
                        {
                            string[] token = sr.ReadLine().Split(',');
                            if (n == 0)
                            {
                            }
                            else
                            {
                                for (int i = 0; i < 12; i++)
                                {
                                    PVIdirtot_W_m2[i] = Convert.ToDouble(token[i + 1]);
                                }
                            }
                            n++;
                        }
                        sr.Close();

                    }
                }

            }

            catch (IOException e)
            {

                if (e.Source != null)
                    Console.WriteLine("IOException source: {0}", e.Source);
                throw;
            }

            try // 확산일사량 정보 불러오기
            {
                string filePath = "C:\\Users\\hjyon\\Desktop\\Re_energy\\DBPV\\Re_PV_Idif.csv";
                using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
                    {
                        int n = 0;
                        while (sr.EndOfStream == false)
                        {
                            string[] token = sr.ReadLine().Split(',');
                            if (n == 0)
                            {
                            }
                            else
                            {
                                for (int i = 0; i < 12; i++)
                                {
                                    PVIdiftot_W_m2[i] = Convert.ToDouble(token[i + 1]);
                                }
                            }
                            n++;
                        }
                        sr.Close();

                    }
                }

            }

            catch (IOException e)
            {

                if (e.Source != null)
                    Console.WriteLine("IOException source: {0}", e.Source);
                throw;
            }

            try // 태양고도각 정보 불러오기
            {
                string filePath = "C:\\Users\\hjyon\\Desktop\\Re_energy\\DBPV\\Re_PV_asol.csv";
                using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
                    {
                        int n = 0;
                        while (sr.EndOfStream == false)
                        {
                            string[] token = sr.ReadLine().Split(',');
                            if (n == 0)
                            {
                            }
                            else
                            {
                                for (int i = 0; i < 12; i++)
                                {
                                    PVαsol[i] = Convert.ToDouble(token[i + 1]);
                                }
                            }
                            n++;
                        }
                        sr.Close();

                    }
                }

            }

            catch (IOException e)
            {

                if (e.Source != null)
                    Console.WriteLine("IOException source: {0}", e.Source);
                throw;
            }
        }

        #endregion / INPUT

        #region Calculation


        Re_FC_Energy_kWh use = new Re_FC_Energy_kWh();

        //설치용량 구하기
        public void calculation_PVcapacity_Kw()
        {
            if (PVModulename == "단결정(Single Cry.Si.)" || PVModulename == "다결정(Poly Cry. Si.)" || PVModulename == "비결정질 Si 박막" || PVModulename == "그외 Si 박막" || PVModulename == "CIGS 박막" || PVModulename == "CdTe 박막")
            {
                PVcapacity_Kw = PVcapacity_Kw;
            }

            PVcapacity_Kw = PVPn_W * PVnumber / 1000;

        }

        //태양광 모듈의 전체 면적 구하기
        public void calculation_PVArea_m2()
        {
            PVArea_m2 = PVwidth_m * PVheight_m * PVwidthnum * PVheightnum; //면적 구하기

            if (PVModulename == "단결정(Single Cry.Si.)")
            {
                PVArea_m2 = PVcapacity_Kw / 0.15;
            }
            if (PVModulename == "다결정(Poly Cry. Si.)")
            {
                PVArea_m2 = PVcapacity_Kw / 0.12;
            }
            if (PVModulename == "비결정질 Si 박막")
            {
                PVArea_m2 = PVcapacity_Kw / 0.04;
            }
            if (PVModulename == "그외 Si 박막")
            {
                PVArea_m2 = PVcapacity_Kw / 0.035;
            }
            if (PVModulename == "CIGS 박막")
            {
                PVArea_m2 = PVcapacity_Kw / 0.105;
            }
            if (PVModulename == "CdTe 박막")
            {
                PVArea_m2 = PVcapacity_Kw / 0.095;
            }

        }

        //수직음영길이
        public void calculation_PVhshobst_m()
        {
            PVLPVlen_m = PVheight_m * PVheightnum;

            if (PVModulename == "단결정(Single Cry.Si.)" || PVModulename == "다결정(Poly Cry. Si.)" || PVModulename == "비결정질 Si 박막" || PVModulename == "그외 Si 박막" || PVModulename == "CIGS 박막" || PVModulename == "CdTe 박막")
            {
                PVLPVlen_m = Math.Sqrt(PVArea_m2);
            }

            PVLPVwid_m = PVArea_m2 / PVLPVlen_m;

            for (int i = 0; i < 12; i++)
            {
                PVhshobst_m[i] = use.Re_PV_hshobst_m(PVLPVlen_m, PVHshobst_m, PVLshobst_m, PVαsol[i]);
            }
        }

        //수평음영길이
        public void calculation_PVhshobstwid_m()
        {
            for (int i = 0; i < 12; i++)
            {
                PVhshobstwid_m[i] = use.Re_PV_hshobstwid_m(PVhshobst_m[i], PVαsol[i]);
            }
        }

        //직달일사 음영적용
        public void calculation_Ishdirtotpvt_W()
        {

            for (int i = 0; i < 12; i++)
            {
                Ishdirtotpvt_W[i] = use.Re_PV_Ishdir_m(PVArea_m2, PVLPVlen_m, PVhshobst_m[i], PVβ, PVhshobstwid_m[i], PVLPVwid_m, PVIdirtot_W_m2[i]);

            }

        }

        //음영계수
        public void calculation_PVFshobstpvt()
        {

            for (int i = 0; i < 12; i++)
            {
                PVFshobstpvt[i] = use.Re_PV_Fshobstpvt_(PVIdirtot_W_m2[i], PVIdiftot_W_m2[i], Ishdirtotpvt_W[i], PVArea_m2);
            }

        }

        //태양광 모듈에 들어오는 전일사량
        public void calculation_Esolm_kWh()
        {

            for (int i = 0; i < 12; i++)
            {
                PVEsolm_kWh_m2[i] = use.Re_PV_Esolm_kWh(PVIs_W_m2[i], PVdmth[i], PVFshobstpvt[i]);
            }

        }

        //표준-테스트-조건에서 최대성능 
        public void calculation_Ppk_kW()
        {
            if (PVmanu_year == "25년 이내")
            {
                PVmanuyearfa = 1;
            }
            if (PVmanu_year == "25년 이상")
            {
                PVmanuyearfa = 0.9;
            }
            PVPpk_kW = use.Re_PV_Ppk_kW(PVKpk_kW_m2, PVArea_m2, PVmanuyearfa);
        }

        //태양광 시스템에 의해 생성된 전기 에너지
        public void calculation_Eelpvoutm_kWh()
        {
            if (PVtype == "고정식" || PVtype == "추적식")
            {
                PVfperf = 0.82;
            }
            if (PVtype == "BIPV")
            {
                if (PVapbipv == "외벽" || PVapbipv == "지붕" || PVapbipv == "창호" || PVapbipv == "커튼월창")
                {
                    PVfperf = 0.80;
                }
                if (PVapbipv == "루버형" || PVapbipv == "블라인드형")
                {
                    PVfperf = 0.82;
                }
            }

            for (int i = 0; i < 12; i++)
            {
                PVEelpvoutm_kWh[i] = use.Re_PV_Eelpvoutm_kWh(PVEsolm_kWh_m2[i], PVPpk_kW, PVfperf, PVInverterηEU, PVIref_kW_m2);
            }
        }

        //태양광 시스템에 의해 생성된 전기 에너지(단위당)
        public void calculation_PVEelpvoutm_kWh_m2()
        {

            for (int i = 0; i < 12; i++)
            {
                PVEelpvoutm_kWh_m2[i] = PVEelpvoutm_kWh[i] / PVArea_m2;
            }

        }

        //태양광 시스템에 의해 생성된 전기 에너지 연간 전기 에너지
        public void calculation_PVEelpvouta_kWh()
        {
            for (int i = 0; i < 12; i++)
            {
                PVEelpvouta_kWh += PVEelpvoutm_kWh[i];
            }
        }

        //평균효율
        public void calculation_PVefficiency()
        {
            double PVEelpvoutm_kWhaver = PVEelpvoutm_kWh.Average();
            double PVEsolm_kWh_m2aver = PVEsolm_kWh_m2.Average();

            PVefficiency = PVEelpvoutm_kWhaver / PVEsolm_kWh_m2aver / PVArea_m2;

        }

        //발전기 규격에 대한 지수
        public void calculation_PVγQ()
        {
            if (PVsystem == "독립형")
            {
                for (int i = 0; i < 12; i++)
                {
                    PVEPusel_kWh_a += PVEPusel_kWh[i];
                }

                PVγQ = use.Re_PV_γQ_kW_MWh_a(PVPpk_kW, PVEPusel_kWh_a);
            }

        }

        //배터리 용량
        public void calculation_PVCeff()
        {

            if (PVsystem == "독립형")
            {
                if (PVbatterytype == "리튬 및 리튬 결합")
                {
                    PVηDoD = 0.83;
                }
                if (PVbatterytype == "니켈-철")
                {
                    PVηDoD = 0.7;
                }
                if (PVbatterytype == "납 및 납젤")
                {
                    PVηDoD = 0.48;
                }

                PVCnenn_kW = PVV_V * PVAH_Ah / 1000;

                PVCeff = use.Re_PV_Ceff_kWh(PVCnenn_kW, PVηDoD);
            }
        }

        //배터리 규격에 대한 지표
        public void calculation_PVCQ()
        {
            if (PVsystem == "독립형")
            {
                PVCQ = use.Re_PV_CQ_kWh(PVCeff, PVEPusel_kWh_a);
            }
        }

        //소요량에 대한 생산량의 비
        public void calculation_PVfmatch()
        {
            PVx = use.Re_PV_x_kWh(PVEelpvouta_kWh, PVEPusel_kWh_a);
            PVfmatch = use.Re_PV_fmatch_kWh(PVx, PVn, PVκ);
        }

        //'계통연계시 이용된 월별 에너지량
        public void calculation_PVEprelusedEPus_kWh()
        {
            for (int i = 0; i < 12; i++)
            {
                PVEprelusedEPus_kWh[i] = use.Re_PV_EprelusedEPus_kWh(PVfmatch, PVEelpvoutm_kWh[i]);
            }

        }

        //배터리에 대한 수정계수
        public void calculation_PVfBatt_kWh()
        {
            if (PVsystem == "독립형")
            {
                PVfBatt = use.Re_PV_fBatt_kWh(PVγQ, PVCQ);
            }
        }

        //독립형시 이용된 월별 에너지량
        public void calculation_PVQfnutzPVi_kWh()
        {
            if (PVsystem == "독립형")
            {
                for (int i = 0; i < 12; i++)
                {
                    PVQfnutzPVi_kWh[i] = Math.Min(use.Re_PV_QfnutzPVi_kWh(PVfBatt, PVEprelusedEPus_kWh[i]), PVEelpvoutm_kWh[i]);
                }
            }

        }

        //계통연계시 그리드로 이동하는 에너지량
        public void calculation_PVEexpelgrid_kWh()
        {
            if (PVsystem == "계통연계형")
            {
                for (int i = 0; i < 12; i++)
                {
                    PVEexpelgrid_kWh[i] = PVEelpvoutm_kWh[i] - PVEprelusedEPus_kWh[i];
                }
            }
        }

    }

    #endregion / Calculation

    #region method

    public class Re_FC_Energy_kWh
    {
        double Esolm, Ppk, Eelpvoutm, γQ, Ceff, CQ, fBatt, EprelusedEPus, fmatch, x, QfnutzPVi, Fshobstpvt, hshobst, hshobstwid, Ishdir;


        //일사량
        public double Re_PV_Esolm_kWh(double Ls, double dmth, double Fshobstpvt)
        {
            Esolm = Ls * dmth * 24 / 1000 * Fshobstpvt;

            return Esolm;
        }

        //최대 성능
        public double Re_PV_Ppk_kW(double Kpk, double A, double manu_year_fa)
        {

            Ppk = Kpk * A * manu_year_fa;

            return Ppk;

        }

        //태양광을 통해 생성된 전기 에너지
        public double Re_PV_Eelpvoutm_kWh(double Esolm, double Ppk, double Fperf, double ηEU, double Iref)
        {

            Eelpvoutm = Esolm * Ppk * Fperf * ηEU / Iref;

            return Eelpvoutm;

        }

        //발전기 규격에 대한 지수
        public double Re_PV_γQ_kW_MWh_a(double Ppk, double Qelges)
        {

            γQ = Ppk / Qelges * 1000;

            return γQ;

        }

        //배터리 용량
        public double Re_PV_Ceff_kWh(double Cnenm, double ηDoD)
        {
            Ceff = Cnenm * ηDoD;

            return Ceff;

        }

        //배터리 규격에 대한 지표
        public double Re_PV_CQ_kWh(double Ceff, double Qelgesa)
        {

            CQ = (Ceff / Qelgesa) * 1000;

            return CQ;

        }

        //배터리에 대한 수정계수
        public double Re_PV_fBatt_kWh(double γQ, double CQ)
        {

            fBatt = Math.Max(1, (0.2 * Math.Log(γQ) + 1.85) * Math.Pow(CQ, 0.1 * Math.Log(γQ) + 0.25));

            return fBatt;

        }

        //계통연계시 이용된 월별 에너지량
        public double Re_PV_EprelusedEPus_kWh(double fmatch, double Eprel)
        {

            EprelusedEPus = fmatch * Eprel;

            return EprelusedEPus;

        }

        //매칭계수
        public double Re_PV_fmatch_kWh(double x, double n, double k)
        {

            fmatch = (Math.Pow(x, n) + 1 / Math.Pow(x, n) - k) / (Math.Pow(x, n) + 1 / Math.Pow(x, n));

            return fmatch;

        }

        //소요량에 대한 생산량의 비
        public double Re_PV_x_kWh(double Eprel, double EPusel)
        {

            x = Eprel / EPusel;

            return x;

        }

        //독립형시 이용된 월별 에너지량
        public double Re_PV_QfnutzPVi_kWh(double fBatt, double QfnutzPVoBa)
        {

            QfnutzPVi = fBatt * QfnutzPVoBa;

            return QfnutzPVi;

        }

        //음영계수
        public double Re_PV_Fshobstpvt_(double Idir, double Idif, double Ishdir, double area)
        {

            Fshobstpvt = (Ishdir + Idif * area) / (Idir * area + Idif * area);

            return Fshobstpvt;

        }

        //수직음영길이
        public double Re_PV_hshobst_m(double LPVlen, double Hshobst, double Lshobst, double asol)
        {

            hshobst = Math.Min(LPVlen, Math.Max(0, Hshobst - Lshobst * Math.Tan(asol * Math.PI / 180.0)));

            return hshobst;

        }

        //수평음영길이
        public double Re_PV_hshobstwid_m(double hshobst, double asol)
        {

            hshobstwid = hshobst / Math.Tan(asol * Math.PI / 180.0);

            return hshobstwid;

        }

        //직달일사 음영 적용
        public double Re_PV_Ishdir_m(double area, double LPVlen, double hshobst, double β, double hshobstwid, double LPVwid, double Idir)
        {
            Ishdir = (area - Math.Min(LPVlen, Math.Sqrt(Math.Pow((hshobst / ((Math.Tan(β * Math.PI / 180.0) + hshobst / hshobstwid))), 2) + Math.Pow((Math.Tan(β * Math.PI / 180.0) * (hshobst / (Math.Tan(β * Math.PI / 180.0) + hshobst / hshobstwid))), 2))) * LPVwid) * Idir;

            if (hshobst == 0)
            {
                Ishdir = (area - Math.Min(LPVlen, 0) * LPVwid) * Idir;
            }

            return Ishdir;
        }
    }

    #endregion / method

}
