using System;
using System.Collections;

namespace main
{
    internal class AHU
    {
        public String AHUNum, AHUOptions;
        double[] theta_e = new double[12], X_e = new double[12];
        double[] theta_e_hr = new double[8760], X_e_hr = new double[8760];
        //덕트
        double OALength, EALength, SALength, RALength, DuctDiameter, DuctInsulationThickness, DuctInsulationConductivity;
        //공조기장비일람표
        string AHUType, HRVType;
        double[] eta_temp = new double[2], eta_all = new double[2], eta_humi = new double[2];
        double Phi_c_coil, theta_c_coil_in, theta_c_coil_in_web, theta_c_coil_out, theta_c_coil_out_web, Phi_h_coil, theta_h_coil_in, theta_h_coil_out;
        double chi_c_coil_in, chi_c_coil_out; // χc,coil,in / χc,coil,out — 스펙상 입력값
        string HU_Type, HU_Control,HU_HULevel;
        double HU_Volume;
        double Volume_SA_ztot, Volume_RA_ztot, FanPower_SA, FanPower_EA;
        // double Pressure_SA, Pressure_EA; // 계산식 미사용(장비일람표에서도 숨김 처리)
        double Volume_SA, Volume_EA;
        // double eta_fan; // η_fan — dP_preh가 항상 0이라 dP_term에서 무의미해짐(장비일람표 모터유형/팬효율도 숨김 처리)
        double chi_fan; // χ — 팬제어계수(Annex C.5-4, MotorControl 기준 조회)
        string MotorControl;
        //공조기일반정보
        public string AHULocation;
        string AHULeakageLevel, AHUVolumeControl, DuctLeakageLevel;
        double AHUInsulationThickness;
        public double flea_du, flea_ahu, fins_ahu;
        double f_flow_ctrl; // f_flow,ctrl — 공조기 제어계수(Annex C.5-3, AHUVolumeControl 기준 조회)
        //예열예냉
        public double theta_defrost;
        public double[] dtheta_prh = new double[12];
        string PrehPrecOptions, GroundOptions, CooltubeMaterial;
        double CooltubeDiameter, CooltubeLength, GroundDepth, CooltubeThicknessh;
        double PrehPower;
        double Preh_Ppump, Preh_Pcoil; // 온수예열기 펌프 정격출력[kW], 코일 정격출력[kW] — AHUSystem_form "온수예열기펌프출력,온수예열기코일출력" 컬럼(kW 단위 입력)
        //존정보
        public ArrayList SelectZone_split = new ArrayList();
        public double Vmin_tot, ANF_tot, Qc_a_tot, Qh_a_tot;
        public double[] tvmech_avg = new double[2];
        public double[,] dvmechmth_avg = new double[2, 12];
        public double[,] Qb_mth_tot = new double[2, 12];
        public double[] QDHU_mth_tot = new double[12]; //존 제습에너지요구량 합산 (기술서 3.3.1 VAV 냉방풍량식 분자용)
        public double[] theta_i_set = new double[2], Qmax_tot = new double[2];
        public double X_i_max, X_i_min;
        public double[] X_i_set = new double[2];
        public double[,] X_i = new double[2,12]; // χi,c — 월별 실내 절대습도(Part2, ZoneDHU.x_int_a 월평균 가중)
        //계산 :  난방/냉방,월
        public double[,] theta_vmech = new double[2, 12], Vvmech = new double[2, 12];

        public double[] Q_gnd = new double[12]; // 쿨튜브
        public double[] Wpreh_k = new double[12]; //프리히팅
        public double[] theta_SA_prh = new double[12]; // 쿨튜브 or 프리히팅

        public double[,] dtheta_du_OA = new double[2, 12], theta_OA_du = new double[2, 12], Q_loss_OA_du = new double[2, 12]; //OA 덕트 열손실
        public double[,] dtheta_du_RA = new double[2, 12], theta_RA_du = new double[2, 12], Q_loss_RA_du = new double[2, 12]; //RA 덕트 열손실
        public double[,] dtheta_du_EA = new double[2, 12], Q_loss_EA_du = new double[2, 12];//EA 덕트 열손실
        public double[,] dtheta_du_SA = new double[2, 12], theta_SA_du = new double[2, 12], Q_loss_SA_du = new double[2, 12]; //SA 덕트 열손실
        public double[,] theta_sur_nc = new double[2, 12];
        public double[,] Hduct_OA = new double[2, 12], Hduct_EA = new double[2, 12];
        public double[,] Hduct_SA = new double[2, 12], Hduct_RA = new double[2, 12];
        double Fx;

        public double[,] dtheta_hr = new double[2, 12], theta_SA_hr = new double[2, 12], theta_EA_hr = new double[2, 12]; //열회수기
        public double[,] dtheta_rca = new double[2, 12], theta_SA_rca = new double[2,12]; //재순환

        public double[,] Qv_b = new double[2, 12]; public double[] Qhu_b = new double[12];
        public double[,] Q_ce = new double[2, 12]; // 8.1/8.2 공조 공급 열손실
        public double[,] Qstar_b = new double[2, 12]; // 8.1/8.2 공조 에너지요구량 (Qh*,b/Qc*,b)
        public double[] X_SA_prh = new double[12], X_SA_rca = new double[12];
        public double[,] X_SA_hr = new double[2, 12];
        public double[] X_vmech = new double[12]; // χv,mech — 3.2.3/3.3.2 급기 절대습도

        public double[] Ev_gen_fan_SA = new double[12], Ev_gen_fan_EA = new double[12], fpl_HU = new double[12], W_HU_aux = new double[12], Wv_aux_preh = new double[12];


        double ca = 0.000279; // 공기비열 [kWh/(kg·K)]
        double rhoA = 1.204; // 공기밀도 [kg/m3]
        double rw = 0.680; // 증발잠열 [kWh/kg]
        static readonly int[] MonthStartHour = { 0, 744, 1416, 2160, 2880, 3624, 4344, 5088, 5832, 6552, 7296, 8016 }; // i월이 시작되는 연중 시간 인덱스(0부터)


        public AHU(String AHUNum)
        {
            this.AHUNum = AHUNum;
        }
        public void Load_Climate()
        {
            string[][] loc = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
            if (loc.Length == 0) { return; }
            string location = loc[0][0];

            string[][] hourlyT = Program.DB.getValue(DB.type.BaseDB_RESystem, "시간별기후데이터", "건구온도", "지역='" + location + "' ORDER BY 월, 일, 시;");
            string[][] hourlyRH = Program.DB.getValue(DB.type.BaseDB_RESystem, "시간별기후데이터", "상대습도", "지역='" + location + "' ORDER BY 월, 일, 시;");
            for (int t = 0; t < hourlyT.Length; t++)
            {
                double T = Program.UTIL.ToDoubleOrZero(hourlyT[t][0]);
                double RH = Program.UTIL.ToDoubleOrZero(hourlyRH[t][0]) / 100;
                theta_e_hr[t] = T;
                X_e_hr[t] = 0.622 * (611.2 * Math.Pow(Math.E, 17.62 * T / (243.12 + T)) * RH) / (101325 - (611.2 * Math.Pow(Math.E, 17.62 * T / (243.12 + T))) * RH);
            }

            for (int mth = 0; mth < 12; mth++)
            {
                string[][] monthAvg = Program.DB.getValue(DB.type.BaseDB_RESystem, "시간별기후데이터", "AVG(건구온도),AVG(상대습도)", "지역='" + location + "' AND 월=" + (mth + 1));
                if (monthAvg.Length > 0)
                {
                    theta_e[mth] = Program.UTIL.ToDoubleOrZero(monthAvg[0][0]);
                    double rh = Program.UTIL.ToDoubleOrZero(monthAvg[0][1]) / 100;
                    X_e[mth] = 0.622 * (611.2 * Math.Pow(Math.E, 17.62 * theta_e[mth] / (243.12 + theta_e[mth])) * rh) / (101325 - (611.2 * Math.Pow(Math.E, 17.62 * theta_e[mth] / (243.12 + theta_e[mth]))) * rh);
                }
            }
        }
        public void Load_ZoneData(string ProjNum)
        {
            string[][] AHUValue = Program.DB.getValue(ProjNum, "AHUSystem_form", "유형", "번호='" + AHUNum + "'");
            if (AHUValue.Length > 0)
            {
                AHUOptions = AHUValue[0][0];
            }
            SelectZone_split.Clear();
            string[][] value = Program.DB.getValue(ProjNum, "ZoneGeneral_Form", "존번호", "선택열회수기 = '" + AHUNum + "' and 환기유무='True'");
            if (value.Length > 0)
            {
                for (int k = 0; k < value.Length; k++)
                {
                    SelectZone_split.Add(value[k][0]);
                }
            }

          for (int n = 0; n < SelectZone_split.Count; n++)
                {
                    string[][] ZoneValue = Program.DB.getValue(ProjNum, "ZoneGeneral_form", "용도프로필,이용일환기량,순바닥면적,공조시간,냉방습도,난방습도", "존번호='" + SelectZone_split[n] + "'");
                    if (ZoneValue.Length > 0)
                    {
                        Vmin_tot += Program.UTIL.ToDoubleOrZero(ZoneValue[0][1]);
                        ANF_tot += Program.UTIL.ToDoubleOrZero(ZoneValue[0][2]);
                        Zone zone = Program.CALC.getZone(SelectZone_split[n].ToString());
                        Qh_a_tot += zone.Qb_a[0];
                        Qc_a_tot += zone.Qb_a[1];
                        Qmax_tot[0] += zone.Q_max[0] /1000;
                        Qmax_tot[1] += zone.Q_max[1] /1000;
                        tvmech_avg[0] += Program.UTIL.ToDoubleOrZero(ZoneValue[0][3]) * zone.Qb_a[0];
                        tvmech_avg[1] += Program.UTIL.ToDoubleOrZero(ZoneValue[0][3]) * zone.Qb_a[1];
                        for (int mth = 0; mth < 12; mth++)
                    {
                        Qb_mth_tot[0, mth] += zone.Qb_mth[0, mth];
                        Qb_mth_tot[1, mth] += zone.Qb_mth[1, mth];
                        QDHU_mth_tot[mth] += zone.Q_DHU_tot[mth];
                        dvmechmth_avg[0, mth] += zone.dwd_mth[mth] * zone.Qb_a[0];
                        dvmechmth_avg[1, mth] += zone.dwd_mth[mth] * zone.Qb_a[1];
                    }
                        string[][] Usage = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "난방설정온도,냉방설정온도,공조운전시부재율,공조냉방부분운전계수", "용도명='" + ZoneValue[0][0] + "'");
                        if (Usage.Length > 0)
                        {
                            theta_i_set[0] += Program.UTIL.ToDoubleOrZero(Usage[0][0]) * zone.Qb_a[0];
                            theta_i_set[1] += Program.UTIL.ToDoubleOrZero(Usage[0][1]) * zone.Qb_a[1];
                        }
                        string[][] HumidC = Program.DB.getValue(DB.type.BaseDB_HCneed, "습도설정", "냉방설정습도", "등급='" + ZoneValue[0][4] + "'");
                        if (HumidC.Length > 0)
                        {
                            X_i_max += Program.UTIL.ToDoubleOrZero(HumidC[0][0]) / 1000 * zone.Qb_a[1];
                        }
                        string[][] HumidH = Program.DB.getValue(DB.type.BaseDB_HCneed, "습도설정", "난방설정습도", "등급='" + ZoneValue[0][5] + "'");
                        if (HumidH.Length > 0)
                        {
                            X_i_min += Program.UTIL.ToDoubleOrZero(HumidH[0][0]) / 1000 * zone.Qb_a[0];
                        }

                        // χi,c(월별 실내 절대습도) — ZoneDHU에서 미리 집계한 값(냉방/제습 트랙)을 존 가중평균
                        ZoneDHU zoneDHU = Program.CALC.getZoneDHU(SelectZone_split[n].ToString());
                        if (zoneDHU != null)
                        {
                            for (int mth = 0; mth < 12; mth++)
                            {
                                X_i[0,mth] += zoneDHU.X_i[0, mth] * zone.Qb_a[0];
                                X_i[1,mth] += zoneDHU.X_i[1, mth] * zone.Qb_a[1];

                            }
                        }
                    }
                }
                theta_i_set[0] = Qh_a_tot > 0 ? theta_i_set[0] / Qh_a_tot : 0;
                theta_i_set[1] = Qc_a_tot > 0 ? theta_i_set[1] / Qc_a_tot : 0;
                tvmech_avg[0] = Qh_a_tot > 0 ? tvmech_avg[0] / Qh_a_tot : 0;
                tvmech_avg[1] = Qc_a_tot > 0 ? tvmech_avg[1] / Qc_a_tot : 0;
                X_i_max = Qc_a_tot > 0 ? X_i_max / Qc_a_tot : 0;
                X_i_min = Qh_a_tot > 0 ? X_i_min / Qh_a_tot : 0;
                X_i_set[1] = X_i_max;
                X_i_set[0] = X_i_min;
                for (int mth = 0; mth < 12; mth++)
                {
                    dvmechmth_avg[0, mth] = Qh_a_tot > 0 ? dvmechmth_avg[0, mth] / Qh_a_tot : 0;
                    dvmechmth_avg[1, mth] = Qc_a_tot > 0 ? dvmechmth_avg[1, mth] / Qc_a_tot : 0;
                    X_i[0,mth] = Qh_a_tot > 0 ? X_i[0,mth] / Qh_a_tot : 0;
                    X_i[1, mth] = Qc_a_tot > 0 ? X_i[1, mth] / Qc_a_tot : 0;
            }


        }
        // AHUZoneVent_Form 기준, 이 설비(AHUNum)에 연결된 모든 존의 급기량/배기량 합계로 실제
        // 운전풍량을 산정 — 카탈로그 정격값 대신 실측 배분량 사용. 이 폼 입력은 이제 필수이므로
        // 데이터가 없을 때의 폴백은 두지 않음.
        private void Load_ZoneVentVolume(string ProjNum)
        {
            double saSum = 0, eaSum = 0;
            for (int n = 0; n < SelectZone_split.Count; n++)
            {
                string[][] Value = Program.DB.getValue(ProjNum, "AHUZoneVent_Form", "급기량,배기량", "설비 = '" + AHUNum + "' And 존 = '" + SelectZone_split[n] + "'");
                if (Value.Length > 0)
                {
                    saSum += Program.UTIL.ToDoubleOrZero(Value[0][0]);
                    eaSum += Program.UTIL.ToDoubleOrZero(Value[0][1]);
                }
            }
            Volume_SA_ztot = saSum;
            Volume_RA_ztot = eaSum;
        }
        public void Load_GeneralData(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "AHUSystem_form", "유형,설치위치,누기등급2,풍량제어,공조기단열두께,덕트누기수준", "번호='" + AHUNum + "'");
            if (Value.Length > 0)
            {

                AHULocation = Value[0][1];
                AHULeakageLevel = Value[0][2];
                AHUVolumeControl = Value[0][3];
                AHUInsulationThickness = Program.UTIL.ToDoubleOrZero(Value[0][4]);
                DuctLeakageLevel = Value[0][5];

                if (AHULocation == "단열외피 내부") { Fx = 0.1; }
                else if (AHULocation == "단열외피 외부") { Fx = 0.4; }
                else { Fx = 1; }
                for (int mth = 0; mth < 12; mth++)
                {
                    for (int hc = 0; hc < 2; hc++)
                    {
                        theta_sur_nc[hc, mth] = theta_i_set[hc] - Fx * (theta_i_set[hc] - theta_e[mth]);
                    }
                }

                string[][] Value2 = Program.DB.getValue(DB.type.BaseDB_AHU, "덕트누기계수", "계수", "건물유형='" + DuctLeakageLevel + "'");
                if (Value2.Length > 0)
                {
                    flea_du = Program.UTIL.ToDoubleOrZero(Value2[0][0]);
                }
                Value2 = Program.DB.getValue(DB.type.BaseDB_AHU, "공조기누기계수", "계수", "누기율등급='" + AHULeakageLevel + "'");
                if (Value2.Length > 0)
                {
                    flea_ahu = Program.UTIL.ToDoubleOrZero(Value2[0][0]);
                }
                Value2 = Program.DB.getValue(DB.type.BaseDB_AHU, "공조기단열계수", "계수", "단열두께 ='" + AHUInsulationThickness.ToString() + "'");
                if (Value2.Length > 0)
                {
                    fins_ahu = Program.UTIL.ToDoubleOrZero(Value2[0][0]);
                }
                Value2 = Program.DB.getValue(DB.type.BaseDB_AHU, "풍량제어계수", "계수", "제어유형1='" + AHUVolumeControl + "'");
                if (Value2.Length > 0)
                {
                    f_flow_ctrl = Program.UTIL.ToDoubleOrZero(Value2[0][0]);
                }
            }


        }
        public void Load_AHUData(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "User_AHU", "공조방식,열회수유형,온도교환효율_냉방,온도교환효율_난방,전열교환효율_냉방,전열교환효율_난방,습도교환효율_냉방,습도교환효율_난방,냉각코일출력,냉각코일_입구_건구온도,냉각코일_입구_습구온도,냉각코일_출구_건구온도,냉각코일_출구_습구온도,난방코일출력,난방코일_입구온도,난방코일_출구온도,가습기유형,가습기제어유형,가습기습도수준,가습기용량,급기풍량,배기풍량,급기정압,배기정압,급기팬동력,배기팬동력,모터제어,팬효율", "번호 = '" + AHUNum + "'");
            if (Value.Length > 0)
            {
                AHUType = Value[0][0];
                HRVType = Value[0][1];
                eta_temp[1] = Program.UTIL.ToDoubleOrZero(Value[0][2]) / 100;
                eta_temp[0] = Program.UTIL.ToDoubleOrZero(Value[0][3]) / 100;
                eta_all[1] = Program.UTIL.ToDoubleOrZero(Value[0][4]) / 100;
                eta_all[0] = Program.UTIL.ToDoubleOrZero(Value[0][5]) / 100;
                eta_humi[1] = Program.UTIL.ToDoubleOrZero(Value[0][6]) / 100;
                eta_humi[0] = Program.UTIL.ToDoubleOrZero(Value[0][7]) / 100;
                Phi_c_coil = Program.UTIL.ToDoubleOrZero(Value[0][8]);
                theta_c_coil_in = Program.UTIL.ToDoubleOrZero(Value[0][9]);
                theta_c_coil_in_web = Program.UTIL.ToDoubleOrZero(Value[0][10]);
                theta_c_coil_out = Program.UTIL.ToDoubleOrZero(Value[0][11]);
                theta_c_coil_out_web = Program.UTIL.ToDoubleOrZero(Value[0][12]);
                Phi_h_coil = Program.UTIL.ToDoubleOrZero(Value[0][13]);
                theta_h_coil_in = Program.UTIL.ToDoubleOrZero(Value[0][14]);
                theta_h_coil_out = Program.UTIL.ToDoubleOrZero(Value[0][15]);

                HU_Type = Value[0][16];
                HU_Control = Value[0][17];
                HU_HULevel = Value[0][18];
                HU_Volume = Program.UTIL.ToDoubleOrZero(Value[0][19]);

                Volume_SA = Program.UTIL.ToDoubleOrZero(Value[0][20]);
                Volume_EA = Program.UTIL.ToDoubleOrZero(Value[0][21]);

                Load_ZoneVentVolume(ProjNum);
                // Pressure_SA = Program.UTIL.ToDoubleOrZero(Value[0][22]); // 급기정압 — 계산식 미사용(장비일람표에서도 숨김 처리)
                // Pressure_EA = Program.UTIL.ToDoubleOrZero(Value[0][23]); // 배기정압 — 계산식 미사용(장비일람표에서도 숨김 처리)
                FanPower_SA = Program.UTIL.ToDoubleOrZero(Value[0][24]);
                FanPower_EA = Program.UTIL.ToDoubleOrZero(Value[0][25]);
                MotorControl = Value[0][26];
                // eta_fan = Program.UTIL.ToDoubleOrZero(Value[0][27]); // dP_term에서 무의미해짐(장비일람표에서도 숨김 처리)
            }
            string[][] Value2 = Program.DB.getValue(DB.type.BaseDB_AHU, "결빙방지온도", "결빙방지온도", "열회수기유형='" + HRVType+ "' And 건물유형 ='비주거건물'");
            if (Value2.Length > 0)
            { theta_defrost = Program.UTIL.ToDoubleOrZero(Value2[0][0]); }
            Value2 = Program.DB.getValue(DB.type.BaseDB_AHU, "팬모터제어계수", "계수", "제어유형='" + MotorControl + "'");
            if (Value2.Length > 0)
            { chi_fan = Program.UTIL.ToDoubleOrZero(Value2[0][0]); }
        }
        public void Load_HRVData(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "User_HRV", "열회수유형,온도교환효율_냉방,온도교환효율_난방,전열교환효율_냉방,전열교환효율_난방,습도교환효율_냉방,습도교환효율_난방,팬풍량,팬동력,모터제어,팬효율", "번호 = '" + AHUNum + "'");
            if (Value.Length > 0)
            {
                HRVType = Value[0][0];
                eta_temp[1] = Program.UTIL.ToDoubleOrZero(Value[0][1]) / 100;
                eta_temp[0] = Program.UTIL.ToDoubleOrZero(Value[0][2]) / 100;
                eta_all[1] = Program.UTIL.ToDoubleOrZero(Value[0][3]) / 100;
                eta_all[0] = Program.UTIL.ToDoubleOrZero(Value[0][4]) / 100;
                eta_humi[1] = Program.UTIL.ToDoubleOrZero(Value[0][5]) / 100;
                eta_humi[0] = Program.UTIL.ToDoubleOrZero(Value[0][6]) / 100;
                Volume_SA = Program.UTIL.ToDoubleOrZero(Value[0][7]);
                Volume_EA = Program.UTIL.ToDoubleOrZero(Value[0][7]);
                Load_ZoneVentVolume(ProjNum);
                FanPower_SA = Program.UTIL.ToDoubleOrZero(Value[0][8]) / 2 / 1000;
                FanPower_EA = Program.UTIL.ToDoubleOrZero(Value[0][8]) / 2 / 1000;
                MotorControl = Value[0][9];
                // eta_fan = Program.UTIL.ToDoubleOrZero(Value[0][10]); // dP_term에서 무의미해짐(장비일람표에서도 숨김 처리)
            }
            string[][] Value2 = Program.DB.getValue(DB.type.BaseDB_AHU, "결빙방지온도", "결빙방지온도", "열회수기유형='" + HRVType + "' And 건물유형 ='비주거건물'");
            if (Value2.Length > 0)
            { theta_defrost = Program.UTIL.ToDoubleOrZero(Value2[0][0]); }
            Value2 = Program.DB.getValue(DB.type.BaseDB_AHU, "팬모터제어계수", "계수", "제어유형='" + MotorControl + "'");
            if (Value2.Length > 0)
            { chi_fan = Program.UTIL.ToDoubleOrZero(Value2[0][0]); }
        }

        public void Load_DuctData(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "AHUSystem_form", "OA덕트길이,EA덕트길이,SA덕트길이,RA덕트길이,덕트단열두께,덕트관경,덕트단열재,덕트단열재열전도율", "번호='" + AHUNum + "'");
            if (Value.Length > 0)
            {
                OALength = Program.UTIL.ToDoubleOrZero(Value[0][0]);
                EALength = Program.UTIL.ToDoubleOrZero(Value[0][1]);
                SALength = Program.UTIL.ToDoubleOrZero(Value[0][2]);
                RALength = Program.UTIL.ToDoubleOrZero(Value[0][3]);
                DuctInsulationThickness = Program.UTIL.ToDoubleOrZero(Value[0][4]);
                DuctDiameter = Program.UTIL.ToDoubleOrZero(Value[0][5]);
                DuctInsulationConductivity = Program.UTIL.ToDoubleOrZero(Value[0][7]);
            }
        }
        public void Load_PrehData(string ProjNum)
        {
            string[][] Value = Program.DB.getValue(ProjNum, "AHUSystem_form", "예열예냉유형,프리히터용량", "번호='" + AHUNum + "'");
            if(Value.Length > 0)
            {
                PrehPrecOptions = Value[0][0];
                if (Value[0][0] == "전기예열기")
                {
                    PrehPower = Program.UTIL.ToDoubleOrZero(Value[0][1]);
                }
                else if (Value[0][0] == "온수예열기")
                {
                    string[][] Value4 = Program.DB.getValue(ProjNum, "AHUSystem_form", "온수예열기펌프출력,온수예열기코일출력", "번호='" + AHUNum + "'");
                    if (Value4.Length > 0)
                    {
                        Preh_Ppump = Program.UTIL.ToDoubleOrZero(Value4[0][0]);
                        Preh_Pcoil = Program.UTIL.ToDoubleOrZero(Value4[0][1]);
                    }
                }
            }
        }
       

        public void Cal_SASet()
        {
            // 3.1.1 급기 난방/냉방 출력 [kW] — ΔT[K]·ca[kWh/(kg·K)]·ρa[kg/m3]·V[m3/h] = kWh/h = kW
            double phi_h_coil = (theta_h_coil_out - theta_h_coil_in) * ca * rhoA * Volume_SA_ztot;
            double phi_c_coil = (theta_c_coil_in - theta_c_coil_out) * ca * rhoA * Volume_SA_ztot;

            // 3.1.2 냉·난방 기준 급기온도
            double theta_h_SA_ref = theta_i_set[0] + phi_h_coil / (ca * rhoA * Volume_SA_ztot);
            double theta_c_SA_ref = theta_i_set[1] - phi_c_coil / (ca * rhoA * Volume_SA_ztot);

            // 3.1.3 냉방 기준 급기습도 — χi,c,set - [Vmax·ρa·rw·(χc,coil,in-χc,coil,out)] / [Vmax·ρa·rw]
            double X_c_SA_ref = X_i_set[1] - (Volume_SA_ztot * rhoA * rw * (chi_c_coil_in - chi_c_coil_out)) / (Volume_SA_ztot * rhoA * rw);

            bool isVAV = AHUOptions == "공조기" && AHUType == "변풍량";
            bool isCAV = AHUOptions == "공조기" && !isVAV;

            if (isCAV) // 3.2.1 정풍량 급기풍량
            {
                // ② 난방 순급기풍량(CAV), ⑫ 냉방 순급기풍량(CAV)
                double vh_mech_cav = Math.Max(Vmin_tot, Qmax_tot[0]  / (ca * rhoA * (theta_h_SA_ref - theta_i_set[0])));
                double vc_mech_cav = Math.Max(Vmin_tot, Qmax_tot[1]  / (ca * rhoA * (theta_i_set[1] - theta_c_SA_ref) + rhoA * rw * (X_i_set[1] - X_c_SA_ref)));
                for (int mth = 0; mth < 12; mth++)
                {
                    // ① 난방 급기풍량, ⑪ 냉방 급기풍량 — 누기계수 반영
                    Vvmech[0, mth] = vh_mech_cav * flea_ahu * flea_du;
                    Vvmech[1, mth] = vc_mech_cav * flea_ahu * flea_du;

                    // 3.2.2 정풍량 급기온도 — 고정풍량 기준으로 월별 부하에 맞춰 급기온도가 변함
                    if (Vvmech[0, mth] > 0)
                    { theta_vmech[0, mth] = Qb_mth_tot[0, mth] / (ca * rhoA * Vvmech[0, mth] * tvmech_avg[0] * dvmechmth_avg[0, mth]) + theta_i_set[0]; }

                    if (Vvmech[1, mth] > 0)
                    { theta_vmech[1, mth] = theta_i_set[1] - Qb_mth_tot[1, mth] / (ca * rhoA * Vvmech[1, mth] * tvmech_avg[1] * dvmechmth_avg[1, mth]); }

                    // 3.2.3 정풍량 급기습도 — χv,mech = χi,c − QDHU,b,mth/(Vv,mech·ρa·rw·tv,mech)
                    if (Vvmech[1, mth] > 0)
                    { X_vmech[mth] = X_i[1, mth] - QDHU_mth_tot[mth] / (Vvmech[1, mth] * rhoA * rw * tvmech_avg[1] * dvmechmth_avg[1, mth]); }
                }
            }
            else if (isVAV) // 3.3.1 변풍량 급기풍량
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    // ② 난방 순급기풍량(VAV), ⑬ 냉방 순급기풍량(VAV)
                    double v_vh_mech_vav = Math.Max(Vmin_tot, Qb_mth_tot[0, mth] / (ca * rhoA * (theta_h_SA_ref - theta_i_set[0]) * tvmech_avg[0] * dvmechmth_avg[0, mth]));
                    double v_vc_mech_vav = Math.Max(Vmin_tot, (Qb_mth_tot[1, mth] + QDHU_mth_tot[mth]) / ((ca * rhoA * (theta_i_set[1] - theta_c_SA_ref) + rhoA * rw * (X_i_set[1] - X_c_SA_ref)) * tvmech_avg[1] * dvmechmth_avg[1, mth]));
                    // ① 난방 급기풍량, ⑫ 냉방 급기풍량 — 누기계수 반영
                    Vvmech[0, mth] = v_vh_mech_vav * flea_ahu * flea_du;
                    Vvmech[1, mth] = v_vc_mech_vav * flea_ahu * flea_du;
                }
                // 3.3.2 변풍량 급기온도 및 습도 — θv,h,mech=θh,SA,ref, θv,c,mech=θc,SA,ref, χv,c,mech=χc,SA,ref
                for (int mth = 0; mth < 12; mth++)
                {
                    theta_vmech[0, mth] = theta_h_SA_ref;
                    theta_vmech[1, mth] = theta_c_SA_ref;
                    X_vmech[mth] = X_c_SA_ref;
                }
            }
            else // 열회수기 — 급기풍량은 실제 급기설정풍량(AHU_SA_Volume) 고정 (스펙 미기재, 코일 없어 부하기반 산정 불가)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    Vvmech[0, mth] = Volume_SA_ztot * flea_ahu * flea_du;
                    Vvmech[1, mth] = Volume_SA_ztot * flea_ahu * flea_du;
                }
            }
        }
        public void Cal_Duct()
        {
            for (int hc = 0; hc < 2; hc++)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    Hduct_OA[hc, mth] = Hduct_k(OALength, Vmin_tot, theta_e[mth]);
                    Hduct_EA[hc, mth] = Hduct_k(EALength, Vmin_tot, theta_e[mth]);
                    Hduct_RA[hc, mth] = Hduct_k(RALength, Vvmech[hc, mth], theta_e[mth]);
                    Hduct_SA[hc, mth] = Hduct_k(SALength, Vvmech[hc, mth], theta_e[mth]);
                }
            }
        }
       
        public double Hduct_k(double L_du, double V_ck, double theta_e)
        {
            // 4.1 구간별(OA/EA/RA/SA) 덕트 열전달계수 H_du,k [W/K] — EN 16798-5-1:2017 <식 C.5>
            double d_du = DuctDiameter / 1000;
            double d_insul = d_du + DuctInsulationThickness / 1000;

            if (L_du <= 0 || V_ck <= 0) { return 0; }

            // ⑥ 덕트 내부풍속 [m/s] — 관 단면적(π/4·d_du²) 기준
            double V_du = (V_ck / 3600) / (Math.PI / 4 * Math.Pow(d_du, 2));

            // ④ 관내측 표면열전달율 [W/(m2·K)]
            double h_i = (4.13 + 0.23 * (theta_e/ 100) - 0.0077 * Math.Pow(theta_e / 100, 2)) * Math.Pow(V_du, 0.75) / Math.Pow(d_du, 0.25);

            // ⑩⑪ 관외측 표면열전달율 — h_c=5, h_e=h_c·π·d_insul (기술서 원문)
            double h_c = 5;
            double h_e = h_c * Math.PI * d_insul;

            // ③ 덕트 열저항 [(m2·K)/W]
            double R_du = 1 / h_i + (1 / (2 * Math.PI)) * (1 / DuctInsulationConductivity) * Math.Log(d_insul / d_du) + 1 / h_e;

            // ② 덕트 열관류율, ⑫ 덕트설치면적, ① 덕트 열전달계수
            double U_du = 1 / R_du;
            double A_du = 2 * Math.PI * d_insul * L_du;
            return U_du * A_du;
        }

        // ⑮ 구간별 덕트 온도차
        public double dtheta_du_k(double theta_vk, double theta_surnc, double H_duct, double V_vk)
        {
            double dtheta_du = (theta_vk - theta_surnc) * (1 - Math.Exp(-H_duct / (rhoA * ca * V_vk)));
            return dtheta_du;
        }
        // ㉕ 구간별 덕트 열손실
        public double Qls_du_k(double V_vk, double dtheta_du, double t_vmech)
        {
            double Qls_du = rhoA * ca * V_vk * dtheta_du * t_vmech;
            return Qls_du;
        }

        public void Cal_DuctLoss_OA()
        {
            for (int hc = 0; hc < 2; hc++)
            {
                for (int mth = 0; mth < 12; mth++)
                {

                    dtheta_du_OA[hc, mth] = dtheta_du_k(theta_e[mth], theta_sur_nc[hc, mth], Hduct_OA[hc, mth], Vmin_tot);

                    // ⑭ 덕트 출구 온도
                    theta_OA_du[hc, mth] = theta_e[mth] + dtheta_du_OA[hc, mth];

                    Q_loss_OA_du[hc, mth] = Qls_du_k(Vmin_tot, dtheta_du_OA[hc, mth], tvmech_avg[hc] * dvmechmth_avg[hc, mth]);
                }
            }
        }


        public void Cal_Preheating()
        {
            for (int mth = 0; mth < 12; mth++)
            {
                double theta_OA_preh = theta_OA_du[0, mth];

                if (PrehPrecOptions == "전기예열기" || PrehPrecOptions == "온수예열기")
                {
                    int hourStart = MonthStartHour[mth];
                    int hourEnd = mth < 11 ? MonthStartHour[mth + 1] : 8760;
                    double sum = 0;
                    int count = 0;
                    for (int h = hourStart; h < hourEnd; h++)
                    {
                        if (theta_e_hr[h] < theta_defrost)
                        {
                            sum += theta_e_hr[h];
                            count++;
                        }
                    }

                    if (count > 0)
                    {
                        // ④ 예열기 가동시 월평균 온도
                        double theta_e_preh_mth = sum / count;

                        // ② 예열기 상승온도
                        dtheta_prh[mth] = theta_defrost - theta_e_preh_mth;
                    }
                    else
                    {
                        dtheta_prh[mth] = 0;
                    }
                }
                else
                {
                    dtheta_prh[mth] = 0;
                }

                // ① 예열기 출구 온도
                theta_SA_prh[mth] = theta_OA_preh + dtheta_prh[mth];
            }
        }
       
        public void Cal_DuctLoss_RA()
        {
            for (int hc = 0; hc < 2; hc++)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    dtheta_du_RA[hc, mth] = dtheta_du_k(theta_i_set[hc], theta_sur_nc[hc, mth], Hduct_RA[hc, mth], Vvmech[hc, mth]);

                    // ⑭ 덕트 출구 온도
                    theta_RA_du[hc, mth] = theta_i_set[hc] + dtheta_du_RA[hc, mth];

                    Q_loss_RA_du[hc, mth] = Qls_du_k(Vvmech[hc, mth], dtheta_du_RA[hc, mth], tvmech_avg[hc] * dvmechmth_avg[hc, mth]);
                }
            }
        }
        public void Cal_HeatRecovery()
        {
            for (int hc = 0; hc < 2; hc++)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    // ③ 열회수 후 온도차
                    dtheta_hr[hc, mth] = (eta_temp[hc] - (flea_ahu - 1) - fins_ahu) * (theta_RA_du[hc, mth] - theta_SA_prh[mth]);

                    // ① 열회수 후 급기 온도
                    theta_SA_hr[hc, mth] = theta_SA_prh[mth] + dtheta_hr[hc, mth];

                    // ② 열회수 후 배기 온도
                    theta_EA_hr[hc, mth] = theta_RA_du[hc, mth] - dtheta_hr[hc, mth];

                    // ⑨ 열회수 후 급기 습도 — χRA=χi,h/c,set(X_i_set[hc]), ηhr=습도교환효율(보정 없음)
                    X_SA_hr[hc, mth] = X_e[mth] + eta_humi[hc] * (X_i_set[hc] - X_e[mth]);
                }
            }
        }
        public void Cal_DuctLoss_EA()
        {
            for (int hc = 0; hc < 2; hc++)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    dtheta_du_EA[hc, mth] = dtheta_du_k(theta_EA_hr[hc, mth], theta_sur_nc[hc, mth], Hduct_EA[hc, mth], Vmin_tot);

                    Q_loss_EA_du[hc, mth] = Qls_du_k(Vmin_tot, dtheta_du_EA[hc, mth], tvmech_avg[hc] * dvmechmth_avg[hc, mth]);
                }
            }
        }
        public void Cal_DuctLoss_SA()
        {
            for (int hc = 0; hc < 2; hc++)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    dtheta_du_SA[hc, mth] = dtheta_du_k(theta_vmech[hc, mth], theta_sur_nc[hc, mth], Hduct_SA[hc, mth], Vvmech[hc, mth]);

                    // ⑭ 덕트 출구 온도
                    theta_SA_du[hc, mth] = theta_vmech[hc, mth] + dtheta_du_SA[hc, mth];

                    Q_loss_SA_du[hc, mth] = Qls_du_k(Vvmech[hc, mth], dtheta_du_SA[hc, mth], tvmech_avg[hc] * dvmechmth_avg[hc, mth]);
                }
            }
        }
        public void Cal_Qv_b()
        {
            // Qvh,b/Qvc,b(공조 순 냉난방 에너지요구량, Part 2 참조) — Cal_HCneed.cs의 존 열수지 계산이
            // 이미 AHU.theta_SA_hr(덕트/예열/열회수 보정된 급기온도)를 반영해서 만든 값이라 그대로 씀
            for (int hc = 0; hc < 2; hc++)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    Qv_b[hc, mth] = Qb_mth_tot[hc, mth];
                }
            }
        }
        public void Cal_CoilLoss()
        {
            for (int mth = 0; mth < 12; mth++)
            {
                // 8.1 ④⑤ 공조 난방 공급 열손실 — 코일 공급 열손실 계수 0.9
                Q_ce[0, mth] = (1 - 0.9) * Qv_b[0, mth];

                // 8.2 ⑨⑩ 공조 냉방 공급 열손실 — 코일 공급 열손실 계수 1(냉방은 0으로 산정)
                Q_ce[1, mth] = (1 - 1) * Qv_b[1, mth];
            }
        }
        public void Cal_Qstar_b()
        {
            for (int hc = 0; hc < 2; hc++)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    // 덕트열전달량 — 단열외피 내부: EA 구간만, 그 외(단열외피 외부/외기): SA 구간만
                    double Q_d = AHULocation == "단열외피 내부" ? Q_loss_EA_du[hc, mth] : Q_loss_SA_du[hc, mth];

                    // 8.1 Qh*,b = Qvh,b+Qvh,d+Qvh,ce / 8.2 Qc*,b = Qvc,b+Qvc,d+Qvc,ce+Q_DHU
                    Qstar_b[hc, mth] = Qv_b[hc, mth] + Q_d + Q_ce[hc, mth] + (hc == 1 ? QDHU_mth_tot[mth] : 0);
                }
            }
        }
        public void Cal_HU()
        {

        }
        public void Cal_Wfan()
        {
            for (int mth = 0; mth < 12; mth++)
            {
                // Wv,d 자체엔 hc 구분이 없어서, 그 달 급기풍량이 더 큰 쪽(hc)을 대표값으로 씀
                int hc = Vvmech[1, mth] >= Vvmech[0, mth] ? 1 : 0;

                // ⑦t_v,op,d·⑪d_wd — 공조기 가동시간[h]×월별 이용일수[d] (다른 월별 계산과 동일하게 tvmech_avg×dvmechmth_avg로 산정)
                double t_v_op_d = tvmech_avg[hc] * dvmechmth_avg[hc, mth];

                // ⑥V_j,d — 급기(SUP): 그 달 대표 hc의 실제 급기풍량, 배기(ETA): 최소외기도입량(열회수 후 실외로 배기되는 신선외기 상당분)
                double V_SUP_d = Vvmech[hc, mth];
                double V_ETA_d = Vvmech[hc, mth];

                // ②SPI — 풍량 당 팬 소비전력
                double SPI_SUP = Volume_SA > 0 ? FanPower_SA / Volume_SA : 0;
                double SPI_ETA = Volume_EA > 0 ? FanPower_EA / Volume_EA : 0;

                // ⑧2.78e-7·ΔP_preh/η_fan — dP_preh가 항상 0이라 η_fan 값과 무관하게 0으로 고정됨
                // double dP_term = eta_fan > 0 ? 2.78e-7 * dP_preh / eta_fan : 0;
                double dP_term = 0;

                // ①Wv,d = V_j,d·(SPI+2.78e-7·ΔP_preh/η_fan)·f_flow,ctrl^χ·d_wd·t_v,op,d , j=SUP(급기),ETA(배기)
                Ev_gen_fan_SA[mth] = V_SUP_d * (SPI_SUP + dP_term) * Math.Pow(f_flow_ctrl, chi_fan) * t_v_op_d;
                Ev_gen_fan_EA[mth] = V_ETA_d * (SPI_ETA + dP_term) * Math.Pow(f_flow_ctrl, chi_fan) * t_v_op_d;
            }
        }
        public void Cal_Wpreh()
        {
            if (PrehPrecOptions != "전기예열기") { return; }

            double[] dmth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            // ◯8 θe,preh = θODA,fp − Φpreh/(ρa·ca·qv,ODA) — 정격용량(Φpreh)으로 θODA,fp까지 데울 수 있는 최저 외기온도. PrehPower는 W 입력이라 /1000
            double theta_e_preh = Vmin_tot > 0 ? theta_defrost - (PrehPower / 1000) / (rhoA * ca * Vmin_tot) : theta_defrost;

            for (int mth = 0; mth < 12; mth++)
            {
                double Wv_preh_el_all = 0;
                int hourStart = MonthStartHour[mth];
                int hourEnd = mth < 11 ? MonthStartHour[mth + 1] : 8760;
                for (int h = hourStart; h < hourEnd; h++)
                {
                    // ◯7 θe,preh ≤ θe,i < θODA,fp 구간만 반영 — θe,i < θe,preh(극저온, 정격용량 초과)는 계산범위에서 제외
                    if (theta_e_hr[h] < theta_defrost && theta_e_hr[h] >= theta_e_preh)
                    {
                        // ◯2 Σρa·ca·Vv,min·(θODA,fp−θe,i) — 시간별 1h치 누적
                        Wv_preh_el_all += rhoA * ca * Vmin_tot * (theta_defrost - theta_e_hr[h]);
                    }
                }

                // ⑱f_day = d_wd/d_mth — 이 AHU 담당 존들의 난방 가중평균 이용일수(dvmechmth_avg[0,mth]) / 월 전체일수
                double f_day = dvmechmth_avg[0, mth] / dmth[mth];

                // ◯1 Wv,preh,el = Wv,preh,el,all · f_day
                Wv_aux_preh[mth] = Wv_preh_el_all * f_day;
            }
        }
        public void Cal_Wpreh_th()
        {
            if (PrehPrecOptions != "온수예열기") { return; }

            double[] dmth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            // θe,preh,th = θODA,fp − Ph,coil/(ρa·ca·qv,ODA) — ◯8과 동일 논리, Ph,coil(코일 정격출력)로 산정한 코일측 한계온도
            double theta_e_preh_th = Vmin_tot > 0 ? theta_defrost - Preh_Pcoil / (rhoA * ca * Vmin_tot) : theta_defrost;

            for (int mth = 0; mth < 12; mth++)
            {
                double Qpreh_th = 0, t_ci_preh_th = 0;
                int hourStart = MonthStartHour[mth];
                int hourEnd = mth < 11 ? MonthStartHour[mth + 1] : 8760;
                for (int h = hourStart; h < hourEnd; h++)
                {
                    // θe,preh,th ≤ θe,i < θODA,fp 구간만 반영 — 그 이하(코일 정격용량 초과)는 계산범위에서 제외
                    if (theta_e_hr[h] < theta_defrost && theta_e_hr[h] >= theta_e_preh_th)
                    {
                        // ◯24 Σρa·ca·qv,ODA·(θODA,fp−θe,i) — 온수예열기 필요열량
                        Qpreh_th += rhoA * ca * Vmin_tot * (theta_defrost - theta_e_hr[h]);
                        // ◯30 온수예열기 가동시간
                        t_ci_preh_th += 1;
                    }
                }

                // ◯23 β = Qpreh,th / (t_op,day · Ph,coil) — t_op,day는 tvmech_avg[0](난방모드 1일 공조가동시간) 재사용
                double beta = (tvmech_avg[0] > 0 && Preh_Pcoil > 0) ? Qpreh_th / (tvmech_avg[0] * Preh_Pcoil) : 0;

                // ◯22 Wv,preh,th,all = Ppump · β · t_ci,preh,th
                double Wv_preh_th_all = Preh_Ppump * beta * t_ci_preh_th;

                // ⑱f_day = d_wd/d_mth
                double f_day = dvmechmth_avg[0, mth] / dmth[mth];

                // ◯21 Wv,preh,th = Wv,preh,th,all · f_day
                Wv_aux_preh[mth] = Wv_preh_th_all * f_day;
            }
        }

        public void Cal_CoolTube()
        {

            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_form", "예열예냉유형,프리히터제어유형,프리히터용량,토양유형,지중깊이,쿨튜브관경,쿨튜브두께,쿨튜브길이,쿨튜브재질", "번호='" + AHUNum + "' and 예열예냉유형 ='쿨튜브'");
            if (Value.Length > 0)
            {
                GroundOptions = Value[0][3]; //토양유형
                GroundDepth = Program.UTIL.ToDoubleOrZero(Value[0][4]); //지중깊이
                CooltubeDiameter = Program.UTIL.ToDoubleOrZero(Value[0][5]) / 2; //쿨튜브 반지름
                CooltubeThicknessh = Program.UTIL.ToDoubleOrZero(Value[0][6]); //쿨튜브두께
                CooltubeLength = Program.UTIL.ToDoubleOrZero(Value[0][7]); //쿨튜브길이
                CooltubeMaterial = Value[0][8]; //쿨튜브재질
            }

            if (PrehPrecOptions == "쿨튜브")
            {
                double[] dmth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                double[] tmth = new double[12], tan = new double[12];
                double a = 0;
                for (int mth = 0; mth < 12; mth++)
                {
                    tmth[mth] = dmth[mth] * 24;
                    tan[mth] = tmth[mth] / 2 + a;
                    a += tmth[mth];
                }

                double theta_e_mn_an = theta_e.Average();
                double theta_e_max_an = theta_e.Max();

                string[][] ground = Program.DB.getValue(DB.type.BaseDB_AHU, "토양특성", "열전도율,밀도,비열", "토양유형='" + GroundOptions + "'");

                double Gound_conductivity = Program.UTIL.ToDoubleOrZero(ground[0][0].ToString()); //토양열전도율
                double Ground_density = Program.UTIL.ToDoubleOrZero(ground[0][1].ToString());  //토양밀도
                double Ground_C = Program.UTIL.ToDoubleOrZero(ground[0][2].ToString()); //토양비열

                double Ground_factor = GroundDepth * Math.Sqrt(Math.PI * Ground_density * Ground_C / (Gound_conductivity * 8760 * 3600));
                double ft = Math.PI * (2 * tan[11] / 8760 + 1);

                string[][] tube = Program.DB.getValue(DB.type.BaseDB_AHU, "쿨튜브열전도율", "열전도율", "재질 ='" + CooltubeMaterial + "'");
                double CooltubeConductivity = Program.UTIL.ToDoubleOrZero(tube[0][0].ToString()); //쿨튜브열전도율

                double Vdu = Vmin_tot / 3600 / (Math.PI * Math.Pow(CooltubeDiameter / 1000, 2));
                double As = 2 * Math.PI * CooltubeDiameter / 1000 * CooltubeLength; //쿨튜브 외표 면적
                double[] theta_gnd = new double[12], hi = new double[12], Udu = new double[12];
                double[] Pp_oa_gnd = new double[12], X_gnd = new double[12], XOA_gnd = new double[12], dX_gnd = new double[12];

                for (int mth = 0; mth < 12; mth++)
                {
                    //지중온도 = 연평균온도+(월중최대온도-연평균온도)x지중특성값xcos(2pix연중 매달 시간-지중특성값 - 특성겂)
                    theta_gnd[mth] = theta_e_mn_an + (theta_e_max_an - theta_e_mn_an) * Math.Pow(Math.E, -Ground_factor) * Math.Cos(2 * Math.PI * tan[mth] / 8760 - Ground_factor - ft);
                    hi[mth] = (4.13 + 0.23 * theta_e[mth] / 100 - 0.0077 * Math.Pow((theta_e[mth] / 100), 2)) * Math.Pow(Vdu, 0.75) / Math.Pow(2 * (CooltubeDiameter - CooltubeThicknessh), 0.25);

                    Udu[mth] = 1 / (1 / (2 * Math.PI) * 1 / CooltubeConductivity * Math.Log((CooltubeDiameter / 1000) / ((CooltubeDiameter - CooltubeThicknessh) / 1000)) + 1 / hi[mth]);

                    dtheta_prh[mth] = (theta_gnd[mth] - theta_e[mth]) * (1 - Math.Pow(Math.E, -(Udu[mth] * As / (Vmin_tot * 0.34)))); //지중열교환후 온도차

                    theta_SA_prh[mth] = theta_e[mth] + dtheta_prh[mth]; //열교환후 온도

                    Pp_oa_gnd[mth] = 611.2 * Math.Pow(Math.E, 17.62 * theta_SA_prh[mth] / (243.12 + theta_SA_prh[mth])); //열교환후 수증기압
                    X_gnd[mth] = 0.622 * Pp_oa_gnd[mth] / (101325 - Pp_oa_gnd[mth]); // 열교환후 온도에 대해 100%포화상태로 가정한 절대습도(kg/kg')
                    XOA_gnd[mth] = Math.Min(X_e[mth], X_gnd[mth]); //절대습도는 외기와 비교하여 작은값을 적용하는게 적합함

                    dX_gnd[mth] = X_e[mth] - XOA_gnd[mth]; //열교환후 절대습도차
                    X_SA_prh[mth] = X_e[mth] + dX_gnd[mth]; //가습없이 제습으로만 작동함

                    Q_gnd[mth] = 0.34 * Vmin_tot * dtheta_prh[mth] * tvmech_avg[1] * dvmechmth_avg[1, mth] / 1000; //예열기를 통한 열획득량, 예냉기를 통한 열손실량 (기존 냉방가중치 그대로 유지)
                }

            }
            else
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    theta_SA_prh[mth] = theta_e[mth];
                    X_SA_prh[mth] = X_e[mth];
                    Q_gnd[mth] = 0;
                }
            }

        }
}
}
