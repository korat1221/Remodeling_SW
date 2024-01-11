using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main
{
    internal class ZoneShade
    {

        String ID;
        public double[] 태양고도각 = new double[12];
        public double[] 태양좌측방위각 = new double[12];
        public double[] 태양우측방위각 = new double[12];

        public double 창호세로길이, 창호가로길이, 주변지형물높이, 경사, 좌측돌출부길이, 좌측돌출부각도, 우측돌출부각도, 우측돌출부길이, 주변지형물각도, 상부돌출부길이, 상부돌출부각도, 방위각;
        public string 방위;
        public double 좌측돌출부이격거리, 우측돌출부이격거리, 상부돌출부이격거리, 지형물까지의거리;

        public double[] 설치면직달 = new double[12];
        public double[] 설치면산란 = new double[12];

        public double[] 지형물수직음영길이 = new double[12];
        public double[] 지형물수평음영길이 = new double[12];
        public double[] 지형물로인한음영길이 = new double[12];

        public double[] 상부돌출음영길이좌 = new double[12];
        public double[] 상부돌출음영길이우 = new double[12];
        public double[] 상부돌출부음영길이 = new double[12];
        public double[] 좌측돌출부음영길이 = new double[12];
        public double[] 우측돌출부음영길이 = new double[12];

        public double[] 수평음영길이 = new double[12];
        public double[] 수직음영길이 = new double[12];
        public double[] 직달일사감소 = new double[12];
        public double[] 최종음영계수 = new double[12];


        public ZoneShade(String WinNum) // 생성자 안에 trycatch
        {

            ID = WinNum;
            try
            {

                //string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,우측면돌출각도,좌측면돌출각도,상부돌출각도,주변요소음영각도,우측면돌출길이,좌측면돌출길이,상부돌출길이,주변요소음영길이,번호,방위,기울기,외피유형", "아이디 = '" + "S6_N_WIN_1" + "'");   //창호 혹은 커튼월 가로 세로 나와야함 (임시로 가로길이, 세로길이라고함)
                string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,우측면돌출각도,좌측면돌출각도,상부돌출각도,주변요소음영각도,우측면돌출길이,좌측면돌출길이,상부돌출길이,주변요소음영길이,번호,방위,기울기,외피유형", "번호 = '" + ID + "'");   //창호 혹은 커튼월 가로 세로 나와야함 (임시로 가로길이, 세로길이라고함)
                
                우측돌출부각도 = Convert.ToDouble(rec[0][1]);
                좌측돌출부각도 = Convert.ToDouble(rec[0][2]);
                상부돌출부각도 = Convert.ToDouble(rec[0][3]);
                주변지형물각도 = Convert.ToDouble(rec[0][4]);
                우측돌출부길이 = Convert.ToDouble(rec[0][5]);
                좌측돌출부길이 = Convert.ToDouble(rec[0][6]);
                상부돌출부길이 = Convert.ToDouble(rec[0][7]);
                주변지형물높이 = Convert.ToDouble(rec[0][8]);
                방위 = rec[0][10];
                경사 = Convert.ToDouble(rec[0][11]);
                창호가로길이 = 1;
                창호세로길이 = 2;
                //rec[0][12] = 창호가로길이.ToString();
                //rec[0][13] = 창호세로길이.ToString();

                //지역, 프로젝트 조건?
                string[][] ValueA = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");   

                //지역,방향,각도(경사)에 따른 월별 설치면직달일사세기 (기후데이터_직달일사량)
                for (int i = 0; i < 12; i++)
                {
                    string[][] aa = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_직달일사량", "일사량", "지역명 ='" + ValueA[0][0] + "' AND 방향 ='" + rec[0][10] + "' AND 각도 ='" + rec[0][11] + "˚" + "' AND 기간 ='" + (i + 1).ToString() + "월" + "'");
                    설치면직달[i] = Convert.ToDouble(aa[0][0]);
                }

                //지역,방향,각도에 따른 월별 설치면산란일사세기 (기후데이터_산란일사량)
                for (int i = 0; i < 12; i++)
                {
                    string[][] aa = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_산란일사량", "일사량", "지역명= '" + ValueA[0][0] + "' AND 방향 = '" + rec[0][10] + "' AND 각도 ='" + rec[0][11] + "˚" + "' AND 기간 ='" + (i + 1).ToString() + "월" + "'");
                    설치면산란[i] = Convert.ToDouble(aa[0][0]);
                }

                //지역,방향,각도에 따른 월별 태양고도각 (기후데이터_고도각)
                for (int i = 0; i < 12; i++)
                {
                    //string[][] aa = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_고도각", "고도각", "지역명= '" + ValueA[0][0] + "' AND 방향 = '" + rec[0][10] + "' AND 각도 ='" + rec[0][11] + "˚" + "' AND 기간 ='" + (i + 1).ToString() + "월" + "'");
                    string[][] aa = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_고도각", "고도각", "지역명= '" + ValueA[0][0] + "' AND 방향 = '" + "남" + "' AND 각도 ='" + "90" + "˚" + "' AND 기간 ='" + (i + 1).ToString() + "월" + "'");
                    태양고도각[i] = Convert.ToDouble(aa[0][0]);
                    //MessageBox.Show(태양고도각[i].ToString());

                    //int i = 0;
                    //string s = "";

                    //while (++i <= 12)
                    //{
                    //    if (s != "") s += ",";
                    //    s += "'" + i.ToString() + "월'";
                    //}

                    //string[][] aa2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_고도각", "고도각", "지역명= '" + ValueA[0][0] + "' AND 방향 = '" + "남" + "' AND 각도 ='" + "90" + "˚" + "' AND 기간 IN (" + s + ")");

                    ////i = i;
                    ////  태양고도각[i] = Convert.ToDouble(aa2[0][0]);
                    //MessageBox.Show(aa2[0][0].ToString());
                }

                //방위, 우측에 따른 태양 우측 방위각 (기후데이터_방위각)
                for (int i = 0; i < 12; i++)
                {
                    string[][] aa = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_방위각", "방위각", "방향 = '" + rec[0][10] + "' AND 각도 ='" + rec[0][11] + "˚" + "' AND 종류 = '" + "우측" + "' AND 기간 ='" + (i + 1).ToString() + "월" + "'");
                    태양우측방위각[i] = Convert.ToDouble(aa[0][0]);
                }

                //방위, 우측에 따른 태양 좌측 방위각 (기후데이터_방위각)
                for (int i = 0; i < 12; i++)
                {
                    string[][] aa = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_방위각", "방위각", "지역명= '" + ValueA[0][0] + "' AND 방향 = '" + rec[0][10] + "' AND 각도 ='" + rec[0][11] + "˚" + "' AND 종류 = '" + "좌측" + "' AND 기간 ='" + (i + 1).ToString() + "월" + "'");
                    태양좌측방위각[i] = Convert.ToDouble(aa[0][0]);
                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message.ToString());
            }

        }

        //////////////////////////////////////////////////////방위에 따른 방위각
        public void Calc_방위각()
        {
            if(방위 == "수평" || 방위 == "남")
            {
                방위각 = 0;
            }
            else if (방위 == "남동")
            {
                방위각 = 45;
            }
            else if (방위 == "남서")
            {
                방위각 = -45;
            }
            else if (방위 == "동")
            {
                방위각 = 90;
            }
            else if (방위 == "서")
            {
                방위각 = -90;
            }
            else if (방위 == "북서")
            {
                방위각 = -135;
            }
            else if (방위 == "북동")
            {
                방위각 = 135;
            }
            else if (방위 == "북")
            {
                방위각 = 180;
            }
            else
            {
                방위각 = 0;
            }
        }

        /////////////////////////////////////////////////////수직음영길이에 영향 메서드 (지형물에의한 음영길이, 상부돌출 음영길이)
        //지형물에의한 음영길이
        public void Calc_지형물음영() //창호세로길이, 지형물음영길이, 상부돌출부음영길이
        {
            지형물에의한음영길이 hsh_obst = new 지형물에의한음영길이();

            지형물까지의거리 = hsh_obst.지형물거리(주변지형물높이, 주변지형물각도);

            for (int i = 0; i < 12; i++)
            {
                지형물수직음영길이[i] = hsh_obst.수직음영길이(창호세로길이, 주변지형물높이, 지형물까지의거리, 태양고도각[i]);
                지형물수평음영길이[i] = hsh_obst.수평음영길이(지형물수직음영길이[i], 태양고도각[i]);
                지형물로인한음영길이[i] = hsh_obst.지형물음영거리(창호세로길이, 경사, 지형물수직음영길이[i], 지형물수평음영길이[i]);
            }
        }

        //상부돌출 음영길이
        public void Calc_상부음영()
        {
            상부음영길이 hk_ovh = new 상부음영길이();

            상부돌출부이격거리 = hk_ovh.상부돌출부이격거리(상부돌출부길이, 상부돌출부각도, 창호세로길이);

            for (int i = 0; i < 12; i++)
            {
                상부돌출음영길이좌[i] = hk_ovh.상부돌출부음영길이좌측(상부돌출부길이, 태양고도각[i], 태양좌측방위각[i], 방위각, 창호세로길이);
                상부돌출음영길이우[i] = hk_ovh.상부돌출부음영길이우측(상부돌출부길이, 태양고도각[i], 태양우측방위각[i], 방위각, 창호세로길이);
                상부돌출부음영길이[i] = hk_ovh.상부돌출부음영길이(상부돌출음영길이좌[i], 상부돌출음영길이우[i]);
            }
        }

        //좌측돌출 음영길이
        public void Calc_좌측음영()
        {
            좌측음영길이 wk_finl = new 좌측음영길이();

            좌측돌출부이격거리 = wk_finl.좌측돌출부이격거리(좌측돌출부길이, 좌측돌출부각도, 창호가로길이);

            for (int i = 0; i < 12; i++)
            {
                좌측돌출부음영길이[i] = wk_finl.좌측돌출부음영길이(좌측돌출부길이, 좌측돌출부이격거리, 태양우측방위각[i], 방위각, 창호가로길이);
            }
        }

        //우측돌출 음영길이
        public void Calc_우측음영()
        {
            우측음영길이 wk_finr = new 우측음영길이();

            우측돌출부이격거리 = wk_finr.우측돌출부이격거리(우측돌출부길이, 우측돌출부각도, 창호가로길이);

            for (int i = 0; i < 12; i++)
            {
                우측돌출부음영길이[i] = wk_finr.우측돌출부음영길이(우측돌출부길이, 우측돌출부이격거리, 태양우측방위각[i], 방위각, 창호가로길이);
            }
        }

        public void Calc_음영계수()
        {
            음영계수 Fsh_obs = new 음영계수();
            for (int i = 0; i < 12; i++)
            {
                수평음영길이[i] = Fsh_obs.수평(창호가로길이, 좌측돌출부음영길이[i], 우측돌출부음영길이[i]);
                수직음영길이[i] = Fsh_obs.수직(창호세로길이, 지형물로인한음영길이[i], 지형물로인한음영길이[i]);
                직달일사감소[i] = Fsh_obs.직달일사(수직음영길이[i], 수평음영길이[i], 창호세로길이, 창호가로길이);
                최종음영계수[i] = Fsh_obs.음영(직달일사감소[i], 설치면직달[i], 설치면산란[i]);
            }
        }


        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

        public class 지형물에의한음영길이
        {
            double 지형물수직음영길이, 지형물수평음영길이, 지형물까지거리;
            double 음영길이, 지형물로인한음영길이;

            //수직음영길이 
            public double 수직음영길이(double 창호세로길이, double 주변지형물높이, double 지형물까지거리, double 태양고도각)
            {
                지형물수직음영길이 = Math.Min(창호세로길이, Math.Max(0, 주변지형물높이 - 지형물까지거리 * Math.Tan(태양고도각 * Math.PI / 180.0)));
                return 지형물수직음영길이;
            }

            //수평음영길이
            public double 수평음영길이(double 지형물수직음영길이, double 태양고도각)
            {
                if (태양고도각 == 0)
                {
                    지형물수평음영길이 = 0;
                }
                else
                {

                    지형물수평음영길이 = 지형물수직음영길이 / Math.Tan(태양고도각 * Math.PI / 180.0);
                }
                return 지형물수평음영길이;
            }

            //지형물까지 거리 
            public double 지형물거리(double 주변지형물높이, double 주변지형물각도)
            {
                if(주변지형물각도 == 0)
                {
                    지형물까지거리 = 0;
                }
                else
                {
                    지형물까지거리 = 주변지형물높이 / Math.Tan(주변지형물각도 * Math.PI / 180.0);
                }
                return 지형물까지거리;
            }

            //최종 지형물에의한 음영길이 
            //창호세로길이, 경사, 수직음영길이, 수평음영길이
            public double 지형물음영거리(double 창호세로길이, double 경사, double 지형물수직음영길이, double 지형물수평음영길이)
            {
                if (지형물수평음영길이 == 0 || 경사 ==0)
                {
                    음영길이 = 0;
                }
                else
                {
                    음영길이 = Math.Sqrt(Math.Pow((지형물수직음영길이 / (Math.Tan(경사 * Math.PI / 180.0) + 지형물수직음영길이 / 지형물수평음영길이)), 2) + Math.Pow((Math.Tan(경사 * Math.PI / 180.0) * (지형물수직음영길이 / (Math.Tan(경사 * Math.PI / 180.0) + 지형물수직음영길이 / 지형물수평음영길이))), 2));
                }

                지형물로인한음영길이 = Math.Min(창호세로길이, 음영길이);
                return 지형물로인한음영길이;
            }
        }

        public class 상부음영길이
        {
            double 이격거리, 좌음영길이, 우음영길이, 좌측음영길이, 우측음영길이, 상부돌출음영길이;

            public double 상부돌출부이격거리(double 상부돌출부길이, double 상부돌출부각도, double 창호세로길이)
            {
                if (상부돌출부각도 == 0)
                {
                    이격거리 = 0;
                }
                else
                {
                    이격거리 = (상부돌출부길이 / Math.Tan(상부돌출부각도 * Math.PI / 180.0)) - (창호세로길이 / 2);
                }
                return 이격거리;
            }

            public double 상부돌출부음영길이좌측(double 상부돌출부길이, double 태양고도각, double 태양좌측방위각, double 방위각, double 창호세로길이)
            {
                좌음영길이 = Math.Abs(상부돌출부길이 * Math.Tan(태양고도각 * Math.PI / 180.0) / Math.Cos(태양좌측방위각 - 방위각)) - 이격거리;
                if (Math.Min(창호세로길이, 좌음영길이) < 0)
                {
                    좌측음영길이 = 0;
                }
                else
                {
                    좌측음영길이 = Math.Min(창호세로길이, 좌음영길이);
                }
                return 좌측음영길이;
            }
            public double 상부돌출부음영길이우측(double 상부돌출부길이, double 태양고도각, double 태양우측방위각, double 방위각, double 창호세로길이)
            {
                우음영길이 = Math.Abs(상부돌출부길이 * Math.Tan(태양고도각 * Math.PI / 180.0) / Math.Cos(태양우측방위각 - 방위각)) - 이격거리;
                if (Math.Min(창호세로길이, 우음영길이) < 0)
                {
                    우측음영길이 = 0;
                }
                else
                {
                    우측음영길이 = Math.Min(창호세로길이, 우음영길이);
                }
                return 우측음영길이;
            }
            public double 상부돌출부음영길이(double 좌측음영길이, double 우측음영길이)
            {
                상부돌출음영길이 = (좌측음영길이 + 우측음영길이) / 2;
                return 상부돌출음영길이;
            }
        }

        public class 좌측음영길이
        {
            double 이격거리, 좌측음영, 좌측돌출음영길이;
            public double 좌측돌출부이격거리(double 좌측돌출부길이, double 좌측돌출부각도, double 창호가로길이)
            {
                if (좌측돌출부각도 == 0)
                {
                    이격거리 = 0;
                }
                else
                {
                    이격거리 = (좌측돌출부길이 / Math.Tan(좌측돌출부각도 * Math.PI / 180.0)) - (창호가로길이 / 2);
                }
                return 이격거리;
            }
            public double 좌측돌출부음영길이(double 좌측돌출부길이, double 이격거리, double 태양우측방위각, double 방위각, double 창호가로길이)
            {
                좌측음영 = Math.Abs(좌측돌출부길이 * Math.Tan(태양우측방위각 * Math.PI / 180.0 - 방위각 * Math.PI / 180.0)) - 이격거리;

                if ((태양우측방위각 - 방위각) < 0)
                {
                    좌측돌출음영길이 = 0;
                }
                else
                {
                    if (좌측음영 < 0)
                    {
                        좌측돌출음영길이 = 0;
                    }
                    else
                    {
                        좌측돌출음영길이 = Math.Min(창호가로길이, 좌측음영);
                    }
                }
                return 좌측돌출음영길이;
            }
        }

        public class 우측음영길이
        {
            double 이격거리, 우측음영, 우측돌출음영길이;
            public double 우측돌출부이격거리(double 우측돌출부길이, double 우측돌출부각도, double 창호가로길이)
            {
                if (우측돌출부각도 == 0)
                {
                    이격거리 = 0;
                }
                else
                {
                    이격거리 = (우측돌출부길이 / Math.Tan(우측돌출부각도 * Math.PI / 180.0)) - (창호가로길이 / 2);
                }
                return 이격거리;
            }
            public double 우측돌출부음영길이(double 우측돌출부길이, double 이격거리, double 태양우측방위각, double 방위각, double 창호가로길이)
            {
                우측음영 = Math.Abs(우측돌출부길이 * Math.Tan(태양우측방위각 * Math.PI / 180.0 - 방위각 * Math.PI / 180.0)) - 이격거리;

                if ((태양우측방위각 - 방위각) > 0)
                {
                    우측돌출음영길이 = 0;
                }
                else
                {
                    if (우측음영 < 0)
                    {
                        우측돌출음영길이 = 0;
                    }
                    else
                    {
                        우측돌출음영길이 = Math.Min(창호가로길이, 우측음영);
                    }
                }
                return 우측돌출음영길이;
            }
        }

        public class 음영계수
        {
            double 수평음영, 수직음영, 직달일사감소계수, 최종음영계수;
            public double 수평(double 창호가로길이, double 좌측돌출음영, double 우측돌출음영)
            {
                수평음영 = Math.Max(0, 창호가로길이 - (좌측돌출음영 + 우측돌출음영));
                return 수평음영;
            }
            public double 수직(double 창호세로길이, double 지형물음영, double 상부돌출음영)
            {
                수직음영 = Math.Max(0, 창호세로길이 - (지형물음영 + 상부돌출음영));
                return 수직음영;
            }
            public double 직달일사(double 수직음영, double 수평음영, double 창호세로길이, double 창호가로길이)
            {
                if (창호세로길이 * 창호가로길이 == 0)
                {
                    직달일사감소계수 = 0;
                }
                else
                {
                    직달일사감소계수 = (수직음영 * 수평음영) / (창호세로길이 * 창호가로길이);
                }
                return 직달일사감소계수;
            }
            public double 음영(double 직달일사감소, double 설치면직달, double 설치면산란)
            {
                if (설치면직달 + 설치면산란 == 0)
                {
                    최종음영계수 = 0;
                }
                else
                {
                    최종음영계수 = (직달일사감소 * 설치면직달 + 설치면산란) / (설치면직달 + 설치면산란);
                }
                return 최종음영계수;
            }
        }

        public void Save()
        {
            for(int i = 0; i<12; i++)
            {
                string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
                if (프로젝트유형.Length > 0)
                {
                    Program.DB.setValue(DB.type.ProjDB, "Shade_3D", "번호,프로젝트유형,유형,각도,월,음영계수",
                   "'" + ID + "','" + 프로젝트유형[0][0] + "','" + "최종음영" + "','" + "" + "','" + (i + 1) + "월" + "','" + 최종음영계수[i].ToString()
                   + "'", "번호,월");
                }
            }
        }

    }
}

