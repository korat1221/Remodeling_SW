using Eagle._Components.Public;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main
{
    internal class Cal_AHU
    {
        String AHUNum, AHUOptions;
        double[] theta_e = new double[12], RelativeHumidity = new double[12], X_e = new double[12];
        double[] theta_e_min = new double[12], theta_e_max = new double[12], theta_e_std = new double[12];
        double X_i_max, X_i_min; 
        double[] X_iset;
        //덕트
        public double OALength, EALength, SALength, RALength, DuctDiameter, DuctInsulationThickness, DuctInsulationConductivity;
        //공조기장비일람표
        public string AHU_Type, AHU_HRVType;
        public double[] AHU_eta_temp = new double[2], AHU_eta_all = new double[2], AHU_eta_humidity = new double[2];
        public double AHU_Cooling_Power, AHU_Cooling_in_drytemp, AHU_Cooling_in_webtemp, AHU_Cooling_out_drytemp, AHU_Cooling_out_webtemp, AHU_Heating_Power, AHU_Heating_in_temp, AHU_Heating_out_temp;
        public string AHU_HU_Type, AHU_HU_Control,AHU_HU_HULevel;
        public double AHU_HU_Volume;
        public double AHU_SA_Volume, AHU_EA_Volume, AHU_SA_Pressure, AHU_RA_Pressure, AHU_SA_FanPower, AHU_EA_FanPower;
        public string AHU_MotorControl;
        //공조기일반정보
        public string AHULocation, AHULeakageLevel, AHUVolumeControl, DuctLeakageLevel;
        public double AHUInsulationThickness;
        public double flea_du, flea_ahu, fins_ahu;
        //예열예냉
        public double theta_defrost;
        public string PrehPrecOptions, GroundOptions, CooltubeMaterial, PrehControlOptions;
        public double CooltubeDiameter, CooltubeLength, GroundDepth, CooltubeThicknessh;
        public double PrehPower, fdefrost_ctrl;
        //존정보
        ArrayList SelectZone_split = new ArrayList();
        public double Vmin_tot, ANF_tot, Qc_a_tot, Qh_a_tot, tvmech_avg;
        public double[] dvmechmth_avg = new double[12];
        public double[,] Qb_mth_tot = new double[2, 12];
        public double[] theta_iset_avg = new double[2], Qmax_tot = new double[2];
        //계산 :  난방/냉방,월
        public double[,] theta_vmech = new double[2, 12], Vvmech = new double[2, 12], Vvmech_leak = new double[2, 12];

        public double[,] Q_cooltube = new double[2, 12]; // 쿨튜브 
        public double[] Wpreh_k = new double[12]; //프리히팅
        public double[] theta_SA_prh = new double[12]; // 쿨튜브 or 프리히팅 

        public double[,] dtheta_du_OA = new double[2, 12], theta_OA_du = new double[2, 12], Qls_V_dis_OA = new double[2, 12]; //OA 덕트 열손실
        public double[,] dtheta_du_RA = new double[2, 12], theta_RA_du = new double[2, 12];//RA 덕트 열손실
        public double[,] dtheta_du_EA = new double[2, 12], Qls_V_dis_EA = new double[2, 12];//EA 덕트 열손실
        public double[,] dtheta_du_SA = new double[2, 12], theta_SA_du = new double[2, 12], Qls_V_dis_SA = new double[2, 12]; //SA 덕트 열손실
        double[,] theta_sur_nc = new double[2, 12];
        double[] Hduct_OA = new double[12], Hduct_EA = new double[12];
        double[] Hduct_SA = new double[12], Hduct_RA = new double[12];
        double Fx;

        public double[,] dtheta_hr = new double[2, 12], theta_SA_hr = new double[2, 12], theta_EA_hr = new double[2, 12]; //열회수기
        public double[,] dtheta_rca = new double[2, 12], theta_SA_rca = new double[2,12]; //재순환

        public double[,] Qvc_b = new double[2, 12];
        public double[] X_SA_prh = new double[12], X_SA_hr = new double[12], X_SA_rca = new double[12];

        public Cal_AHU(String AHUNum)
        {
            this.AHUNum = AHUNum;
            string[][] Location = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
            if (Location.Length > 0)
            {
                for (int mth = 1; mth < 13; mth++)
                {
                    string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_온도습도", "온도,상대습도,최소온도,최대온도,표준편차", "지역명 ='" + Location[0][0] + "'And 기간 = '" + mth + "월'");
                    if (Value.Length > 0)
                    {
                        theta_e[mth - 1] = Convert.ToDouble(Value[0][0]);
                        RelativeHumidity[mth - 1] = Convert.ToDouble(Value[0][1]);
                        X_e[mth - 1] = 0.622 * (611.2 * Math.Pow(Math.E, 17.62 * theta_e[mth -1] / (243.12 + theta_e[mth - 1])) * RelativeHumidity[mth - 1]) / (101325 - (611.2 * Math.Pow(Math.E, 17.62 * theta_e[mth - 1] / (243.12 + theta_e[mth - 1]))) * RelativeHumidity[mth - 1]);
                        theta_e_min[mth - 1] = Convert.ToDouble(Value[0][2]);
                        theta_e_max[mth - 1] = Convert.ToDouble(Value[0][3]);
                        theta_e_std[mth - 1] = Convert.ToDouble(Value[0][4]);
                    }
                }
            }
        }
        public void Load_ZoneData()
        {
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_form", "존", "번호='" + AHUNum + "'");
            if (Value.Length > 0)
            {
                Split_Zone(Value[0][0]);
                for (int n = 0; n < SelectZone_split.Count; n++)
                {
                    string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_form", "용도프로필,이용일환기량,순바닥면적,공조시간", "존번호='" + SelectZone_split[n] + "'");
                    if (ZoneValue.Length > 0)
                    {
                        Vmin_tot += Convert.ToDouble(ZoneValue[0][1]);
                        ANF_tot += Convert.ToDouble(ZoneValue[0][2]);
                        Zone zone = Program.CALC.getZone(SelectZone_split[n].ToString());
                        Qh_a_tot += zone.Qb_a[0];
                        Qc_a_tot += zone.Qb_a[1];
                        Qmax_tot[0] += zone.Q_max[0];
                        Qmax_tot[1] += zone.Q_max[1];
                        tvmech_avg += Convert.ToDouble(ZoneValue[0][3]) * zone.Qb_a[1];
                        for (int mth = 0; mth < 12; mth++)
                        {
                            Qb_mth_tot[0,mth] += zone.Qb_mth[0, 1, mth];
                            Qb_mth_tot[1, mth] += zone.Qb_mth[1, 1, mth];
                           // theta_ih_avg[mth] += zone.theta_i[0, 1, mth] * zone.Qb_a[0];
                           // theta_ic_avg[mth] += zone.theta_i[1, 1, mth] * zone.Qb_a[1];
                            dvmechmth_avg[mth] += zone.dwd_mth[mth] * zone.Qb_a[1];
                        }


                        string[][] Usage = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "난방설정온도,냉방설정온도,공조운전시부재율,공조냉방부분운전계수", "용도명='" + ZoneValue[0][0]+"'");
                        if (Usage.Length > 0)
                        {
                            theta_iset_avg[0] += Convert.ToDouble(Usage[0][0]) * zone.Qb_a[0];
                            theta_iset_avg[1] += Convert.ToDouble(Usage[0][1]) * zone.Qb_a[1];
                        }

                    }
                }

                theta_iset_avg[0] = theta_iset_avg[0] / Qh_a_tot;
                theta_iset_avg[1] = theta_iset_avg[1] / Qc_a_tot;
                tvmech_avg = tvmech_avg / Qc_a_tot;
                for (int mth = 0; mth < 12; mth++)
                {
                    dvmechmth_avg[mth] = dvmechmth_avg[mth] / Qc_a_tot;
                }
            }
        }
        private void Split_Zone(String nonSplit)
        {
            String 내용;
            if (nonSplit != null)
            {
                if (nonSplit.Contains("+"))
                {
                    string[] token = nonSplit.Split('+');
                    SelectZone_split.Clear();
                    foreach (var item in token)
                    {
                        SelectZone_split.Add(item.ToString());
                    }
                    내용 = SelectZone_split[0].ToString() + " 외 " + (SelectZone_split.Count - 1).ToString() + "개";
                }
                else
                {
                    SelectZone_split.Clear();
                    SelectZone_split.Add(nonSplit);
                    내용 = SelectZone_split[0].ToString();
                }
            }
            else { 내용 = ""; }
        }
        public void Load_AHUData()
        {
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_AHU", "공조방식,열회수유형,온도교환효율_냉방,온도교환효율_난방,전열교환효율_냉방,전열교환효율_난방,습도교환효율_냉방,습도교환효율_난방,냉각코일출력,냉각코일_입구_건구온도,냉각코일_입구_습구온도,냉각코일_출구_건구온도,냉각코일_출구_습구온도,난방코일출력,난방코일_입구온도,난방코일_출구온도,가습기유형,가습기제어유형,가습기습도수준,가습기용량,급기풍량,배기풍량,급기정압,배기정압,급기팬동력,배기팬동력,모터제어", "번호 = '" + AHUNum  + "'");
            if (Value.Length > 0)
            {
                AHU_Type = Value[0][0];
                AHU_HRVType = Value[0][1];
                AHU_eta_temp[1] = Convert.ToDouble(Value[0][2]);
                AHU_eta_temp[0] = Convert.ToDouble(Value[0][3]);
                AHU_eta_all[1] = Convert.ToDouble(Value[0][4]);
                AHU_eta_all[0] = Convert.ToDouble(Value[0][5]);
                AHU_eta_humidity[1] = Convert.ToDouble(Value[0][6]);
                AHU_eta_humidity[0] = Convert.ToDouble(Value[0][7]);
                AHU_Cooling_Power = Convert.ToDouble(Value[0][8]);
                AHU_Cooling_in_drytemp = Convert.ToDouble(Value[0][9]);
                AHU_Cooling_in_webtemp = Convert.ToDouble(Value[0][10]);
                AHU_Cooling_out_drytemp = Convert.ToDouble(Value[0][11]);
                AHU_Cooling_out_webtemp = Convert.ToDouble(Value[0][12]);
                AHU_Heating_Power = Convert.ToDouble(Value[0][13]);
                AHU_Heating_in_temp = Convert.ToDouble(Value[0][14]);
                AHU_Heating_out_temp = Convert.ToDouble(Value[0][15]);

                AHU_HU_Type = Value[0][16];
                AHU_HU_Control = Value[0][17];
                AHU_HU_HULevel = Value[0][18];
                AHU_HU_Volume = Convert.ToDouble(Value[0][19]);

                AHU_SA_Volume = Convert.ToDouble(Value[0][20]);
                AHU_EA_Volume = Convert.ToDouble(Value[0][21]);
                AHU_SA_Pressure = Convert.ToDouble(Value[0][22]);
                AHU_RA_Pressure = Convert.ToDouble(Value[0][23]);
                AHU_SA_FanPower = Convert.ToDouble(Value[0][24]);
                AHU_EA_FanPower = Convert.ToDouble(Value[0][25]);
                AHU_MotorControl = Value[0][26];
            }
            string[][] Value2 = Program.DB.getValue(DB.type.BaseDB_AHU, "결빙방지온도", "결빙방지온도", "열회수기유형='" + AHU_HRVType+ "' And 건물유형 ='비주거건물'");
            if (Value2.Length > 0)
            { theta_defrost = Convert.ToDouble(Value2[0][0]); }

            Value2 = Program.DB.getValue(DB.type.BaseDB_AHU, "실내설정습도", "제습기준습도", "구분='" + AHU_HU_HULevel + "' And 건물유형 ='비주거건물'");
            if (Value2.Length > 0)
            { theta_defrost = Convert.ToDouble(Value2[0][0]); }
        }
        public void Load_AHUGeneralData()
        {
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_form", "유형,설치위치,누기등급2,풍량제어,공조기단열두께", "번호='" + AHUNum + "'");
            //string[][] Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_form", "유형,설치위치,누기등급2,풍량제어,공조기단열두께,덕트누기수준", "번호='" + AHUNum + "'");
            if (Value.Length > 0)
            {
                AHUOptions = Value[0][0];
                AHULocation = Value[0][1];                              
                AHULeakageLevel = Value[0][2];
                AHUVolumeControl = Value[0][3];
                AHUInsulationThickness = Convert.ToDouble(Value[0][4]);
                DuctLeakageLevel = "기존건물";
                //DuctLeakageLevel = Value[0][5];

                if (AHULocation == "단열외피안") { Fx = 0.1; }
                else if (AHULocation == "단열외피밖") { Fx = 0.4; }
                else { Fx = 1; }
                for (int mth = 0; mth < 12; mth++)
                {
                    for (int hc = 0; hc < 2; hc++)
                    {
                        theta_sur_nc[hc, mth] = theta_iset_avg[hc] - Fx * (theta_iset_avg[hc] - theta_e[mth]);
                    }
                }

                string[][] Value2 = Program.DB.getValue(DB.type.BaseDB_AHU, "덕트누기계수", "계수", "건물유형='" + DuctLeakageLevel + "'");
                if(Value2.Length >0)
                {
                    flea_du = Convert.ToDouble(Value2[0][0]);
                }
                Value2 = Program.DB.getValue(DB.type.BaseDB_AHU, "공조기누기계수", "계수", "누기율등급='" + AHULeakageLevel + "'");
                if (Value2.Length > 0)
                {
                    flea_ahu = Convert.ToDouble(Value2[0][0]);
                }
                Value2 = Program.DB.getValue(DB.type.BaseDB_AHU, "공조기단열계수", "계수", "단열두께 ='" + AHUInsulationThickness.ToString() + "'");
                if (Value2.Length > 0)
                {
                    fins_ahu = Convert.ToDouble(Value2[0][0]);
                }
            }

            
        }
        public void Load_DuctData()
        {
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_form", "OA덕트길이,EA덕트길이,SA덕트길이,RA덕트길이,덕트단열두께,덕트관경,덕트단열재,덕트단열재열전도율", "번호='" + AHUNum + "'");
            if (Value.Length > 0)
            {
                if (Value[0][0]!= "")
                { OALength = Convert.ToDouble(Value[0][0]); }
                else { OALength = 0; }

                if (Value[0][1] != "")
                { EALength = Convert.ToDouble(Value[0][1]); }
                else { EALength = 0; }

                if (Value[0][2] != "")
                { SALength = Convert.ToDouble(Value[0][2]); }
                else { SALength = 0; }

                if (Value[0][3] != "")
                { RALength = Convert.ToDouble(Value[0][3]); }
                else { RALength = 0; }

                DuctInsulationThickness = Convert.ToDouble(Value[0][4]);
                DuctDiameter = Convert.ToDouble(Value[0][5]);
                DuctInsulationConductivity = Convert.ToDouble(Value[0][7]);
            }
        }
        public void Load_PrehPrecData()
        {
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_form", "예열예냉유형,프리히터제어유형,프리히터용량,토양유형,지중깊이,쿨튜브관경,쿨튜브두께,쿨튜브길이,쿨튜브재질", "번호='" + AHUNum + "'");
            if(Value.Length > 0)
            {
                PrehPrecOptions = Value[0][0];
                if (Value[0][0] == "프리히터")
                {
                    PrehControlOptions = Value[0][1];
                    PrehPower = Convert.ToDouble(Value[0][2]);
                    string[][] Value2 = Program.DB.getValue(DB.type.BaseDB_AHU, "예열기제어계수", "계수", "제어유형='" + PrehControlOptions + "'");
                    if (Value2.Length > 0)
                    {
                        fdefrost_ctrl = Convert.ToDouble(Value2[0][0]);
                    }                    
                }
                else if (Value[0][0] == "쿨튜브")
                {                    
                    GroundOptions = Value[0][3];
                    GroundDepth = Convert.ToDouble(Value[0][4]);
                    CooltubeDiameter = Convert.ToDouble(Value[0][5]);
                    CooltubeThicknessh = Convert.ToDouble(Value[0][6]);
                    CooltubeLength = Convert.ToDouble(Value[0][7]);
                    CooltubeMaterial = Value[0][8];
                }
                else
                {

                }
            }            
        }
        public void Cal_Coil()
        {
            double 냉각코일_입구_상대습도 = (AHU_Cooling_in_webtemp + 5.809 - 0.697 * AHU_Cooling_in_drytemp) / ((0.058 + 0.003 * AHU_Cooling_in_drytemp) * 100);
            double 냉각코일_출구_상대습도 = (AHU_Cooling_out_webtemp + 5.809 - 0.697 * AHU_Cooling_out_drytemp) / ((0.058 + 0.003 * AHU_Cooling_out_drytemp) * 100);

            double 냉각코일_입구_절대습도 = 0.622 * (611.2 * Math.Pow(Math.E, 17.62 * AHU_Cooling_in_drytemp / (243.12 + AHU_Cooling_in_drytemp))) * 냉각코일_입구_상대습도 / (101325 - (611.2 * Math.Pow(Math.E, 17.62 * AHU_Cooling_in_drytemp / (243.12 + AHU_Cooling_in_drytemp))) * 냉각코일_입구_상대습도);
            double 냉각코일_입구_엔탈피 = 1.006 * AHU_Cooling_in_drytemp + 냉각코일_입구_절대습도 * (2500 + 1.86 * AHU_Cooling_in_drytemp);

            double 냉각코일_출구_절대습도 = 0.622 * (611.2 * Math.Pow(Math.E, 17.62 * AHU_Cooling_out_drytemp / (243.12 + AHU_Cooling_out_drytemp))) * 냉각코일_출구_상대습도 / (101325 - (611.2 * Math.Pow(Math.E, 17.62 * AHU_Cooling_out_drytemp / (243.12 + AHU_Cooling_out_drytemp))) * 냉각코일_출구_상대습도);
            double 냉각코일_출구_엔탈피 = 1.006 * AHU_Cooling_out_drytemp + 냉각코일_출구_절대습도 * (2500 + 1.86 * AHU_Cooling_out_drytemp);

            double 계산된_냉방출력 = (냉각코일_입구_엔탈피 - 냉각코일_출구_엔탈피) * 1.204 * AHU_SA_Volume / 3600;
            double 계산된_난방출력 = (AHU_Heating_in_temp - AHU_Heating_out_temp) * 0.34 * AHU_SA_Volume / 3600;

        }

        public void Cal_SA_set()
        {
            for (int mth = 0; mth < 12; mth++)
            {
                theta_vmech[1, mth] = theta_iset_avg[1] - (AHU_Cooling_in_drytemp - AHU_Cooling_out_drytemp) * 0.34 * AHU_SA_Volume / 3600;
                theta_vmech[0, mth] = theta_iset_avg[0] + (AHU_Heating_in_temp - AHU_Heating_out_temp) * 0.34 * AHU_SA_Volume / 3600;

            }

            if (AHU_Type == "변풍량")
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    Vvmech[0, mth] = Math.Max(Vmin_tot, Qb_mth_tot[0, mth] * 1000 / (0.34 * (theta_vmech[0, mth] - theta_iset_avg[0]) * tvmech_avg * dvmechmth_avg[mth]));
                    Vvmech[1, mth] = Math.Max(Vmin_tot, Qb_mth_tot[1, mth] * 1000 / (0.34 * (theta_iset_avg[1] - theta_vmech[1, mth]) * tvmech_avg * dvmechmth_avg[mth]));
                    Vvmech_leak[0, mth] = Vvmech[0, mth] * flea_ahu * flea_du;
                    Vvmech_leak[1, mth] = Vvmech[1, mth] * flea_ahu * flea_du;
                }

            }
            else
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    Vvmech[0, mth] = Math.Max(Vmin_tot, Qmax_tot[0] * 1000 / (0.34 * (theta_vmech[0, mth] - theta_iset_avg[0])));
                    Vvmech_leak[0, mth] = Vvmech[0, mth] * flea_ahu * flea_du;
                    theta_vmech[0, mth] = theta_iset_avg[0] + Qb_mth_tot[0, mth] / (0.34 * Vvmech_leak[0, mth] * tvmech_avg * dvmechmth_avg[mth] / 1000);

                    Vvmech[1, mth] = Math.Max(Vmin_tot, Qmax_tot[1] * 1000 / (0.34 * (theta_iset_avg[1] - theta_vmech[1, mth])));
                    Vvmech_leak[1, mth] = Vvmech[1, mth] * flea_ahu * flea_du;
                    theta_vmech[1, mth] = theta_iset_avg[1] - Qb_mth_tot[1, mth] / (0.34 * Vvmech_leak[1, mth] * tvmech_avg * dvmechmth_avg[mth] / 1000);
                }
            }

        }
            
        public void Cal_CoolTube()
        {
            double[] dmth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            double[] tmth = new double[12], tan = new double[12];
            for (int mth = 0; mth < 12; mth++)
            {
                tmth[mth] = dmth[mth] * 24;
            }
            tan[0] = dmth[0] / 2;
            for (int mth = 1; mth < 12; mth++)
            {
                tan[mth] = dmth[mth - 1] + dmth[mth] / 2;
            }

            double theta_e_mn_an = theta_e.Average();
            double theta_e_max_an = theta_e.Max();

            double Ground_density = 1500; //나중에 테이블로 바꿔야 함
            double Ground_C = 1200;//나중에 테이블로 바꿔야 함
            double Gound_conductivity = 1.9;//나중에 테이블로 바꿔야 함
            double Ground_factor = GroundDepth * Math.Sqrt(Math.PI * Ground_density * Ground_C / (Gound_conductivity * 8760 * 3600));
            double ft = Math.PI * (2 * tan[11] / 8760 + 1);
            double CooltubeConductivity = 0.33; //나중에 테이블로 바꿔야 함

            double Vdu = Vmin_tot / 3600 / (Math.PI * CooltubeDiameter / 1000 * CooltubeDiameter / 1000);
            double As = 2 * Math.PI * 0.5 * CooltubeDiameter / 1000 * CooltubeLength;
            double[] theta_gnd = new double[12], hi = new double[12], Udu = new double[12];
            double[] dtheta_gnd = new double[12];
            double[] Pp_oa_gnd = new double[12], X_gnd = new double[12], XOA_gnd = new double[12], dX_gnd = new double[12];
            if (PrehPrecOptions == "쿨튜브")
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    theta_gnd[mth] = theta_e_mn_an + (theta_e_max_an - theta_e_mn_an) * Math.Pow(Math.E, -Ground_factor) * Math.Cos(2 * Math.PI * tan[mth] / 8760 - Ground_factor - ft);
                    hi[mth] = (4.13 + 0.23 * theta_e[mth] / 100 - 0.0077 * Math.Pow((theta_e[mth] / 100), 2)) * Math.Pow(Vdu, 0.75) / Math.Pow(CooltubeDiameter, 0.25);
                    Udu[mth] = 1 / (1 / (2 * Math.PI) * 1 / CooltubeConductivity * Math.Log((0.5 * CooltubeDiameter / 1000) / (0.5 * (CooltubeDiameter - CooltubeThicknessh) / 1000), Math.E) + 1 / hi[mth]);

                    dtheta_gnd[mth] = (theta_gnd[mth] - theta_e[mth]) * (1 - Math.Pow(Math.E, -(Udu[mth] * As / (Vmin_tot * 0.34))));

                    theta_SA_prh[mth] = theta_e[mth] + theta_gnd[mth]; //열교환후 온도 
                    Pp_oa_gnd[mth] = 611.2 * Math.Pow(Math.E, 17.62 * theta_SA_prh[mth] / (243.12 + theta_SA_prh[mth]));
                    X_gnd[mth] = 0.622 * Pp_oa_gnd[mth] / (101325 - Pp_oa_gnd[mth]);
                    XOA_gnd[mth] = Math.Min(X_e[mth], X_gnd[mth]);
                    dX_gnd[mth] = Math.Max(0, X_e[mth] - XOA_gnd[mth]);
                    X_SA_prh[mth] = X_e[mth] + dX_gnd[mth];

                    Q_cooltube[0, mth] = -0.34 * Vmin_tot * dtheta_gnd[mth] * tvmech_avg * dvmechmth_avg[mth] / 1000;
                    Q_cooltube[1, mth] = 0.34 * Vmin_tot * dtheta_gnd[mth] * tvmech_avg * dvmechmth_avg[mth] / 1000;
                }
            }

        }
        public void Cal_Preheating()
        {
           
            double[,] Bin = new double[Convert.ToInt32(theta_defrost) - Convert.ToInt32(theta_e_min.Min()) + 1, 12];

            for (int mth = 0; mth < 12; mth++)
            {
                for (int k = 0; k < Convert.ToInt32(theta_defrost) - Convert.ToInt32(theta_e_min.Min()) + 1; k++)
                {
                    double theta_1 = k + Convert.ToInt32(theta_e_min.Min());
                    double theta_2 = k + Convert.ToInt32(theta_e_min.Min()) + 1;
                    if (theta_1 < theta_e_min[mth])
                    {
                        Bin[k, mth] = 0;
                    }
                    else
                    {
                        Bin[k, mth] = Math.Max(0, (NORM_DIST(theta_2, theta_e[mth], theta_e_std[mth]) - NORM_DIST(theta_1, theta_e[mth], theta_e_std[mth])) * dvmechmth_avg[mth] * tvmech_avg * (theta_defrost - theta_1));
                    }
                }
            }
            double Ppreh_default = 0.34 * Vmin_tot * (theta_defrost - (theta_e.Min()));
            double[] Gpreh_t_mth = new double[12];
            double[] top_preh_mth = new double[12];
            double[] dtheta_pre_max_k = new double[12];
            double[] dtheta_preh_k = new double[12];
            if (PrehPrecOptions == "프리히터")
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    for (int k = 0; k < Convert.ToInt32(theta_defrost) - Convert.ToInt32(theta_e_min.Min()) + 1; k++)
                    {
                        Gpreh_t_mth[mth] += Bin[k, mth];
                    }
                    top_preh_mth[mth] = Math.Max(NORM_DIST(theta_defrost, theta_e[mth], theta_e_std[mth]) * tvmech_avg * dvmechmth_avg[mth], 0);

                    dtheta_pre_max_k[mth] = Gpreh_t_mth[mth] / top_preh_mth[mth];
                    if (double.IsNaN(dtheta_pre_max_k[mth])) { dtheta_pre_max_k[mth] = 0; }

                    dtheta_preh_k[mth] = Math.Min(dtheta_pre_max_k[mth], PrehPower * dtheta_pre_max_k[mth] / Ppreh_default);
                    theta_SA_prh[mth] = theta_e[mth] + dtheta_preh_k[mth];
                    Wpreh_k[mth] = 0.34 * Vmin_tot * dtheta_preh_k[mth] * top_preh_mth[mth] / 1000 * fdefrost_ctrl;
                }
            }

        }
        private double NORM_DIST(double X, double M, double sigma)
        {
            double zValue = (X - M) / sigma;
            const double b1 = 0.319381530;
            const double b2 = -0.356563782;
            const double b3 = 1.781477937;
            const double b4 = -1.821255978;
            const double b5 = 1.330274429;
            const double p = 0.2316419;
            const double c = 0.39894228;

            if (zValue >= 0.0)
            {
                double t = 1.0 / (1.0 + p * zValue);
                return (1.0 - c * Math.Exp(-zValue * zValue / 2.0) * t * (t * (t * (t * (t * b5 + b4) + b3) + b2) + b1));
            }
            else
            {
                double t = 1.0 / (1.0 - p * zValue);
                return (c * Math.Exp(-zValue * zValue / 2.0) * t * (t * (t * (t * (t * b5 + b4) + b3) + b2) + b1));
            }
        }
        public void Cal_Duct()
        {
            //덕트열전달계수 계산 
            double Duct_R = Math.Log(((DuctDiameter / 2 + DuctInsulationThickness) / 1000) / (DuctDiameter / 2 / 1000), Math.E) / 2 / Math.PI / DuctInsulationConductivity;
            double He = 5 + 0.15 * 5.67 / 100000000 * 4 * 1000;
            double Re = 1 / (He * 2 * Math.PI * (DuctDiameter / 2 + DuctInsulationThickness) / 1000);
            double v = AHU_SA_Volume / Math.Pow(Math.PI * (DuctDiameter / 2 / 1000), 2) / 3600;
            double[] Ri = new double[12], Uduct = new double[12];

            for (int mth = 0; mth < 12; mth++)
            {
                Ri[mth] = 1 / ((4.13 + 0.23 * theta_e[mth] / 100 - 0.0077 * Math.Pow((theta_e[mth] / 100), 2)) * Math.Pow(v, 0.75) / Math.Pow((DuctDiameter / 1000), 0.25));
                Uduct[mth] = 1 / (Duct_R + Re + Ri[mth]);
                Hduct_OA[mth] = OALength * Uduct[mth] * 2 * Math.PI * DuctDiameter / 2 / 1000;
                Hduct_EA[mth] = EALength * Uduct[mth] * 2 * Math.PI * DuctDiameter / 2 / 1000;
                Hduct_SA[mth] = SALength * Uduct[mth] * 2 * Math.PI * DuctDiameter / 2 / 1000;
                Hduct_RA[mth] = RALength * Uduct[mth] * 2 * Math.PI * DuctDiameter / 2 / 1000;
            }
        }
        public void Cal_DuctLoss_OA()
        {
            for (int mth = 0; mth < 12; mth++)
            {
                if(Hduct_OA[mth] > 0)
                {
                    for (int hc = 0; hc < 2; hc++)
                    {
                        dtheta_du_OA[hc, mth] = (theta_SA_prh[mth] - theta_sur_nc[hc, mth]) * (1 - Math.Pow(Math.E, (Hduct_OA[mth]) / (0.34 * Vmin_tot)));
                        theta_OA_du[hc, mth] = theta_SA_prh[mth] + dtheta_du_OA[hc, mth];
                    }

                    if (Qb_mth_tot[0, mth] == 0)
                    {
                        Qls_V_dis_OA[0, mth] = 0;
                    }
                    else
                    {
                        Qls_V_dis_OA[0, mth] = Math.Max(0, -0.34 * Vmin_tot * dtheta_du_OA[0, mth] * tvmech_avg * dvmechmth_avg[mth] / 1000);
                    }
                    if (Qb_mth_tot[1, mth] == 0)
                    {
                        Qls_V_dis_OA[1, mth] = 0;
                    }
                    else
                    {
                        Qls_V_dis_OA[1, mth] = Math.Max(0, 0.34 * Vmin_tot * dtheta_du_OA[1, mth] * tvmech_avg * dvmechmth_avg[mth] / 1000);
                    }

                }
               
            }
        }
        public void Cal_DuctLoss_RA()
        {
            for (int mth = 0; mth < 12; mth++)
            {     
                if(Hduct_RA[mth]>0)
                {
                    for (int hc = 0; hc < 2; hc++)
                    {
                        dtheta_du_RA[hc, mth] = (theta_iset_avg[hc] - theta_sur_nc[0, mth]) * (1 - Math.Pow(Math.E, (Hduct_RA[mth]) / (0.34 * Vvmech_leak[0, mth])));
                        theta_RA_du[hc, mth] = (1 / flea_du * (theta_iset_avg[hc]) + (flea_du - 1) / flea_du * theta_sur_nc[0, mth]) + dtheta_du_RA[0, mth];
                    }
                }
                             
            }
        }

        public void Cal_HeatRecovery()
        {
            for(int mth = 0; mth < 12; mth++)
            {
                for(int hc =0; hc < 2; hc++)
                {
                    dtheta_hr[hc, mth] = (AHU_eta_temp[hc]/100 - (flea_du - 1) - fins_ahu) * (theta_RA_du[hc, mth] - theta_OA_du[hc, mth]);
                    theta_SA_hr[hc, mth] = theta_OA_du[hc, mth] + dtheta_hr[hc, mth];
                    theta_EA_hr[hc, mth] = theta_RA_du[hc, mth] - dtheta_hr[hc, mth];
                  //  X_SA_hr[mth] = X_SA_prh[mth] + (eta_humidity[0] - (flea_ahu - 1) - fins_ahu) * (X_iset - X_SA_prh[mth]);
                }
            }            
        }

        public void Cal_DuctLoss_EA()
        {
            for (int mth = 0; mth < 12; mth++)
            {
                if(Hduct_EA[mth] > 0)
                {
                    for (int hc = 0; hc < 2; hc++)
                    {
                        dtheta_du_EA[hc, mth] = (theta_EA_hr[hc, mth] - theta_sur_nc[hc, mth]) * (1 - Math.Pow(Math.E, (Hduct_EA[mth]) / (0.34 * Vmin_tot)));
                        theta_RA_du[hc, mth] = (1 / flea_du * (theta_iset_avg[hc]) + (flea_du - 1) / flea_du * theta_sur_nc[0, mth]) + dtheta_du_RA[0, mth];
                        if (Qb_mth_tot[0, mth] == 0)
                        {
                            Qls_V_dis_EA[0, mth] = 0;
                        }
                        else
                        {
                            Qls_V_dis_EA[0, mth] = Math.Max(0, 0.34 * Vmin_tot * dtheta_du_EA[0, mth] * tvmech_avg * dvmechmth_avg[mth] / 1000);
                        }
                    }
                }               
            }
        }
        public void Cal_RCA()
        {
            for (int mth = 0; mth < 12; mth++)
            {
                for (int hc = 0; hc < 2; hc++)
                {
                    dtheta_rca[hc, mth] = (Vvmech_leak[hc, mth] - Vvmech[hc, mth]) / Vvmech_leak[hc, mth] * (theta_RA_du[hc, mth] - theta_SA_hr[hc, mth]);
                    theta_SA_rca[hc, mth] = dtheta_rca[hc, mth] + theta_SA_hr[hc, mth];
                }
            }
        }
        public void Cal_DuctLoss_SA()
        {
            for (int mth = 0; mth < 12; mth++)
            {
                for (int hc = 0; hc < 2; hc++)
                {
                    if(AHUOptions =="열회수기")
                    {
                        dtheta_du_SA[hc, mth] = (theta_SA_rca[hc,mth] - theta_sur_nc[hc, mth]) * (1 - Math.Pow(Math.E, (Hduct_SA[mth]) / (0.34 * Vvmech_leak[hc, mth])));
                        theta_SA_du[hc, mth] = theta_SA_rca[hc, mth] + dtheta_du_SA[hc, mth];
                    }
                    else
                    {
                        dtheta_du_SA[hc, mth] = (theta_vmech[hc, mth] - theta_sur_nc[hc, mth]) * (1 - Math.Pow(Math.E, (Hduct_SA[mth]) / (0.34 * Vvmech_leak[hc, mth])));
                        theta_SA_du[hc, mth] = theta_vmech[hc, mth] + dtheta_du_SA[hc, mth]; 
                    }
                    
                    if (Qb_mth_tot[0, mth] == 0)
                    {
                        Qls_V_dis_EA[0, mth] = 0;
                    }
                    else
                    {
                        Qls_V_dis_EA[hc, mth] = Math.Max(0, 0.34 * (Vvmech[hc,mth] * dtheta_du_SA[hc, mth] + (Vvmech_leak[hc, mth] - Vvmech[hc, mth]) * (theta_sur_nc[hc,mth] - theta_SA_du[hc,mth])) * tvmech_avg * dvmechmth_avg[mth] / 1000);
                    }
                }
            }
        }

        public void Cal_Qvc_b() 
        { 
            for(int mth =0; mth<12; mth++)
            {
                if (Qb_mth_tot[0, mth] == 0)
                {
                    Qvc_b[0, mth] = 0;
                }
                else
                {
                    Qvc_b[0, mth] = Qb_mth_tot[0,mth] + Q_cooltube[0,mth] + Qls_V_dis_OA[0,mth] + Qls_V_dis_EA[0, mth] + Qls_V_dis_SA[0, mth];
                }
                if (Qb_mth_tot[1, mth] == 0)
                {
                    Qvc_b[1, mth] = 0;
                }
                else
                {
                    Qvc_b[1, mth] = Qb_mth_tot[1, mth] + Q_cooltube[1, mth] + Qls_V_dis_OA[1, mth]  + Qls_V_dis_SA[1, mth];
                }
            }
        }

        public void Cal_HU()
        {
            for(int mth = 0; mth < 12; mth++)
            {
                for(int hc =0; hc < 2; hc++)
                {
                  
                }
            }
            
        }
    }
}
