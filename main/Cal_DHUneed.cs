using System;
using System.Collections;

namespace main
{
    internal class ZoneDHU
    {
        public String ZoneNum;
        public double x_set_max, x_set_min;
        public double zoneArea, zoneHeight;
        public double OccDensity;
        public int StartHour, EndHour;
        public double n50, e = 0.05, f = 15;
        public double Vmech_SUP, Vmech_ETA;
        public double[] eta_χV_mech = new double[2];
        public double V_SUP_z, V_ETA_z;
        public ArrayList IncomingZoneNums = new ArrayList();
        public ArrayList IncomingZoneFlows = new ArrayList();
        public double nSUP, nETA;
        public double[] dwd_mth = new double[12];
        public static string Location;
        public static double[] dmth = new double[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        // MonthStartHour[i] = (i+1)월이 시작되는 연중 시간 인덱스(0부터) — idx로부터 월을 역산할 때 씀
        public static readonly int[] MonthStartHour = { 0, 744, 1416, 2160, 2880, 3624, 4344, 5088, 5832, 6552, 7296, 8016 };
        public static double[] x_e = new double[8760];
        public double VA_wd;
        public string SelectHRV;
        public double[,] x_int_a = new double[2, 8760];
        public double[] Q_DHU_mth = new double[12];
        public double[] Q_HU_mth = new double[12];
        public double[,] X_i = new double[2, 12];
        public double rho_a = 1.204;
        public int idx;
        public double[] G_e = new double[2];
        public double G_int;
        public double G_abs;
        public double[] G_stor = new double[2];

        public ZoneDHU(String zoneNum)
        {
            this.ZoneNum = zoneNum;
        }

        public void LoadData()
        {
            string[][] ZoneG = null;
            string[][] hc = null;
            string[][] hh = null;
            string[][] ActualSAEA = null;
            bool hasActual = false;
            double actualSA = 0, actualEA = 0;
            string 환기방식 = "";
            string table = "";
            string[][] value = null;
            string[][] OutgoingZV = null;
            double outgoingZ = 0;
            double v = 0;
            string[][] IncomingZV = null;
            double incomingZ = 0;
            double V = 0;
            Zone zoneRef = null;
            string 주이용일 = "";
            string[][] ValueK = null;

            ZoneG = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form",
                "냉방습도,난방습도,순바닥면적,천장고,재실밀도,시작시간,종료시간,환기유무,환기방식,비이용일환기량,이용일환기량,선택열회수기,주이용일,냉난방시간,사용시간",
                "존번호 = '" + ZoneNum + "'");
            if (ZoneG.Length == 0) return;

            hc = Program.DB.getValue(DB.type.BaseDB_HCneed, "습도설정", "냉방설정습도", "등급='" + ZoneG[0][0] + "'");
            if (hc.Length > 0) { x_set_max = Program.UTIL.ToDoubleOrZero(hc[0][0]) / 1000; }

            hh = Program.DB.getValue(DB.type.BaseDB_HCneed, "습도설정", "난방설정습도", "등급='" + ZoneG[0][1] + "'");
            if (hh.Length > 0) { x_set_min = Program.UTIL.ToDoubleOrZero(hh[0][0]) / 1000; }

            zoneArea = Program.UTIL.ToDoubleOrZero(ZoneG[0][2]);
            zoneHeight = Program.UTIL.ToDoubleOrZero(ZoneG[0][3]);
            OccDensity = Program.UTIL.ToDoubleOrZero(ZoneG[0][4]);
            StartHour = Convert.ToInt32(ZoneG[0][5].Split(':')[0]);
            EndHour = Convert.ToInt32(ZoneG[0][6].Split(':')[0]);
            VA_wd = Program.UTIL.ToDoubleOrZero(ZoneG[0][10]) / zoneArea;

            SelectHRV = ZoneG[0][11];
            if (Convert.ToBoolean(ZoneG[0][7]))
            {
                ActualSAEA = Program.DB.getValue(DB.type.ProjDB, "AHUZoneVent_Form", "급기량,배기량", "설비 = '" + SelectHRV + "' And 존 = '" + ZoneNum + "'");
                hasActual = ActualSAEA.Length > 0;
                if (hasActual)
                {
                    double.TryParse(ActualSAEA[0][0], out actualSA);
                    double.TryParse(ActualSAEA[0][1], out actualEA);
                }

                환기방식 = ZoneG[0][8];
                if (환기방식 == "열회수기" || 환기방식 == "공조기")
                {
                    Vmech_SUP = hasActual ? actualSA : Program.UTIL.ToDoubleOrZero(ZoneG[0][10]);
                    Vmech_ETA = hasActual ? actualEA : Program.UTIL.ToDoubleOrZero(ZoneG[0][10]);
                    table = 환기방식 == "열회수기" ? "User_HRV" : "User_AHU";
                    value = Program.DB.getValue(DB.type.ProjDB, table, "습도교환효율_난방,습도교환효율_냉방", "번호='" + SelectHRV + "'");
                    if (value.Length > 0)
                    {
                        eta_χV_mech[0] = Program.UTIL.ToDoubleOrZero(value[0][0]) / 100;
                        eta_χV_mech[1] = Program.UTIL.ToDoubleOrZero(value[0][1]) / 100;
                    }
                }
                else if (환기방식 == "배기환기(3종)")
                {
                    Vmech_SUP = 0;
                    Vmech_ETA = hasActual ? actualEA : Program.UTIL.ToDoubleOrZero(ZoneG[0][10]);
                }
            }

            OutgoingZV = Program.DB.getValue(DB.type.ProjDB, "AHUZoneVent_Form", "인접존배기량", "존 = '" + ZoneNum + "' And 인접존 <> ''");
            outgoingZ = 0;
            for (int i = 0; i < OutgoingZV.Length; i++)
            {
                double.TryParse(OutgoingZV[i][0], out v);
                outgoingZ += v;
            }
            IncomingZV = Program.DB.getValue(DB.type.ProjDB, "AHUZoneVent_Form", "존,인접존배기량", "인접존 = '" + ZoneNum + "'");
            incomingZ = 0;
            IncomingZoneNums.Clear();
            IncomingZoneFlows.Clear();
            for (int i = 0; i < IncomingZV.Length; i++)
            {
                double.TryParse(IncomingZV[i][1], out v);
                incomingZ += v;
                IncomingZoneNums.Add(IncomingZV[i][0]);
                IncomingZoneFlows.Add(v);
            }
            V_SUP_z = incomingZ;
            V_ETA_z = outgoingZ;

            V = zoneArea * zoneHeight;
            if (V != 0)
            {
                nSUP = (Vmech_SUP + V_SUP_z) / V;
                nETA = (Vmech_ETA + V_ETA_z) / V;
            }

            zoneRef = Program.CALC.getZone(ZoneNum);
            if (zoneRef != null) { n50 = zoneRef.n50; }

            주이용일 = ZoneG[0][12];
            for (int mth = 0; mth < 12; mth++)
            {
                ValueK = Program.DB.getValue(DB.type.BaseDB_HCneed, "이용일수", "이용일수", "월='" + (mth + 1) + "월' AND 주간일수 ='주 " + 주이용일 + ".0 일 근무'");
                if (ValueK.Length > 0) { dwd_mth[mth] = Program.UTIL.ToDoubleOrZero(ValueK[0][0]); }
            }
        }

        public static void LoadClimate()
        {
            string[][] loc = null;
            string[][] climT = null;
            string[][] climRH = null;
            double T = 0, RH = 0;

            loc = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
            if (loc.Length == 0) { return; }
            Location = loc[0][0];

            climT = Program.DB.getValue(DB.type.BaseDB_RESystem, "시간별기후데이터", "건구온도", "지역='" + Location + "' ORDER BY 월, 일, 시;");
            climRH = Program.DB.getValue(DB.type.BaseDB_RESystem, "시간별기후데이터", "상대습도", "지역='" + Location + "' ORDER BY 월, 일, 시;");
            if (climT.Length > 0 && climRH.Length > 0)
            {
                for (int t = 0; t < climT.Length; t++)
                {
                    T = Program.UTIL.ToDoubleOrZero(climT[t][0]);
                    RH = Program.UTIL.ToDoubleOrZero(climRH[t][0]);
                    x_e[t] = 611.2 * Math.Exp(17.62 * T / (243.12 + T)) / 461.51 / (273.15 + T) / 1.2 * (RH / 100);
                }
            }
        }

        private double n_inf_t(bool mechOn)
        {
            double n_inf0 = n50 * e;
            double fe = (nSUP != 0 && n50 != 0) ? 1 / (1 + f / e * Math.Pow((nETA - nSUP) / n50, 2)) : 1;
            return mechOn ? n_inf0 * fe : n_inf0;
        }

        private double n_win_t(bool mechOn)
        {
            double n_inf0 = n50 * e;
            double fe = (nSUP != 0 && n50 != 0) ? 1 / (1 + f / e * Math.Pow((nETA - nSUP) / n50, 2)) : 1;
            double nwd = 0;
            double dw0 = 0;
            double dwMech = 0;

            if (!mechOn) { return 0.1; }

            nwd = VA_wd / zoneHeight;
            dw0 = nwd < 1.2 ? Math.Max(0, nwd - (nwd - 0.2) * n_inf0 * fe - 0.1) : Math.Max(0, nwd - n_inf0 * fe - 0.1);
            if (dw0 <= nSUP && nETA <= nSUP + n_inf0) { dwMech = 0; }
            else if (dw0 <= nSUP) { dwMech = nETA - nSUP - n_inf0; }
            else if (nETA <= nSUP + n_inf0) { dwMech = dw0 - nSUP; }
            else { dwMech = nETA - nSUP - n_inf0; }
            return 0.1 + dwMech;
        }

        // 5.3.1 실내 절대습도 χ_int,a,ztc,t — 식(80). hc: 1=제습평가트랙(냉방효율), 0=가습평가트랙(난방효율)
        public double x_int_a_ztc_t(int hu_dhu)
        {
            int h = idx % 24 + 1;
            bool mechOn = h >= StartHour && h < EndHour;
            double V = zoneArea * zoneHeight;
            double x_prev = 0;
            double q_win = 0, qx_win = 0;
            double q_inf = 0, qx_inf = 0;
            double q_mech = 0, qx_mech = 0;
            double q_z = 0, qx_z = 0;
            AHU ahu = null;
            double x_SA = 0;
            ZoneDHU zoneZ = null;
            double x_iz = 0, flow = 0;
            double sumQ = 0, sumQX = 0;            
            double numerator = 0, denominator = 0;
            double x_int_a_ztc_t = 0;
            int mth = 0;

            for (int m = 1; m <= 11; m++)
            {
                if (idx >= MonthStartHour[m]) { mth++; }
            }

            if (idx == 0) { x_prev = x_e[idx]; }
            else { x_prev = x_int_a[hu_dhu, idx - 1]; }

            q_win = n_win_t(mechOn) * V;
            qx_win = q_win * x_e[idx];

            q_inf = n_inf_t(mechOn) * V;
            qx_inf = q_inf * x_e[idx];

            if (mechOn)
            {
                ahu = Program.CALC.getAHU(SelectHRV);
                x_SA = ahu != null ? ahu.X_SA_hr[hu_dhu, mth] : x_e[idx] + eta_χV_mech[hu_dhu] * (x_prev - x_e[idx]);
                q_mech = Vmech_SUP;
                qx_mech = q_mech * x_SA;

                for (int i = 0; i < IncomingZoneNums.Count; i++)
                {
                    zoneZ = Program.CALC.getZoneDHU((string)IncomingZoneNums[i]);
                    if (zoneZ != null)
                    {
                        if (idx == 0) { x_iz = x_e[idx]; }
                        else { x_iz = zoneZ.x_int_a[hu_dhu, idx - 1]; }
                        flow = (double)IncomingZoneFlows[i];
                        q_z += flow;
                        qx_z += flow * x_iz;
                    }
                }
            }

            sumQ = q_win + q_inf + q_mech + q_z;
            sumQX = qx_win + qx_inf + qx_mech + qx_z;

            G_int_ztc_t();
            G_abs_ztc_t();

            numerator = rho_a * sumQX + G_int - G_abs + rho_a * V * x_prev;
            denominator = rho_a * sumQ + rho_a * V;

            x_int_a_ztc_t = denominator != 0 ? numerator / denominator : x_prev;
            x_int_a[hu_dhu, idx] = x_int_a_ztc_t;
            return x_int_a_ztc_t;
        }

        // 5.1 수증기 유입량 G_e,ztc,t (한 유입경로 k) — ISO 52016-1 식(72)의 항 하나
        public double G_e_ztc_t(double q_V, double x_sup, double x_set)
        {
            return rho_a * q_V * (x_set - x_sup);
        }

        // 5.1 수증기 유입량 합계 — 자연환기+침기+기계환기+인접존환기. hc: 1=냉방/제습평가(x_set_max), 0=난방/가습평가(x_set_min)
        public double G_e_ztc_t_tot(int hc)
        {
            int h = idx % 24 + 1;
            bool mechOn = h >= StartHour && h < EndHour;
            double x_set = hc == 1 ? x_set_max : x_set_min;
            double V = zoneArea * zoneHeight;
            double G_tot = 0, G_win = 0, G_inf = 0, G_mech = 0, G_z = 0;
            double x_int_prev = 0;
            AHU ahu = null;
            double x_SA = 0;
            ZoneDHU zoneZ = null;
            double x_iz = 0;
            int mth = 0;

            for (int m = 1; m <= 11; m++)
            {
                if (idx >= MonthStartHour[m]) { mth++; }
            }

            G_win = G_e_ztc_t(n_win_t(mechOn) * V, x_e[idx], x_set);
            G_inf = G_e_ztc_t(n_inf_t(mechOn) * V, x_e[idx], x_set);

            if (mechOn)
            {
                if (idx == 0) { x_int_prev = x_e[idx]; }
                else { x_int_prev = x_int_a[hc, idx - 1]; }

                ahu = Program.CALC.getAHU(SelectHRV);
                x_SA = ahu != null ? ahu.X_SA_hr[hc, mth] : x_e[idx] + eta_χV_mech[hc] * (x_int_prev - x_e[idx]);
                G_mech = G_e_ztc_t(Vmech_SUP, x_SA, x_set);

                for (int i = 0; i < IncomingZoneNums.Count; i++)
                {
                    zoneZ = Program.CALC.getZoneDHU((string)IncomingZoneNums[i]);
                    if (zoneZ != null)
                    {
                        if (idx == 0) { x_iz = x_e[idx]; }
                        else { x_iz = zoneZ.x_int_a[hc, idx - 1]; }
                        G_z += G_e_ztc_t((double)IncomingZoneFlows[i], x_iz, x_set);
                    }
                }
            }

            G_tot = G_win + G_inf + G_mech + G_z;

            return G_tot;
        }

        // 5.2 실내 수증기 발생량 G_int,ztc,t
        public double G_int_ztc_t()
        {
            int h = idx % 24 + 1;
            bool 재실 = h >= StartHour && h < EndHour;
            double 인체잠열 = 54;     // W/person, ASHRAE Handbook 기준
            double 증발잠열 = 0.680;  // kWh/kg, EN 16798-5-1 표27

            G_int = 재실 ? (인체잠열 * OccDensity * zoneArea / 1000) / 증발잠열 : 0;
            return G_int;
        }

        // 5.2 실내 흡습량 G_abs,ztc,t
        public double G_abs_ztc_t()
        {
            G_abs = 0;
            return G_abs;
        }

        // 5.3.2 실내 누적량 G_stor,ztc,t — hc: 1=제습평가(x_set_max), 0=가습평가(x_set_min)
        public double G_stor_ztc_t(int hu_dhu)
        {
            double x_prev = 0;
            double x_set = hu_dhu == 1 ? x_set_max : x_set_min;
            double V = zoneArea * zoneHeight;

            if (idx == 0) { x_prev = x_e[idx]; }
            else { x_prev = x_int_a[hu_dhu, idx - 1]; }

            return rho_a * V * (x_set - x_prev);
        }

        // 5.4.1 시간별 제습량 G_DHU;ld;ztc;t — ISO 52016-1 식(72)
        public double G_DHU_ld_ztc_t()
        {
            G_e[1] = G_e_ztc_t_tot(1);
            G_int = G_int_ztc_t();
            G_abs = G_abs_ztc_t();
            G_stor[1] = G_stor_ztc_t(1);
            return Math.Max(0, -G_e[1] + G_int - G_abs - G_stor[1]);
        }

        // 5.5.1 시간별 가습량 G_HU;ld;ztc;t — ISO 52016-1 식(71)
        public double G_HU_ld_ztc_t()
        {
            G_e[0] = G_e_ztc_t_tot(0);
            G_int = G_int_ztc_t();
            G_abs = G_abs_ztc_t();
            G_stor[0] = G_stor_ztc_t(0);
            return Math.Max(0, G_e[0] - G_int + G_abs + G_stor[0]);
        }

        // 5.4.2 시간별 제습 에너지요구량 Q_DHU,h
        public double Q_DHU_h()
        {
            double 증발잠열 = 0.680;
            return G_DHU_ld_ztc_t() * 1 * 증발잠열;
        }

        // 5.5.2 시간별 가습 에너지요구량 Q_HU,h
        public double Q_HU_h()
        {
            double 증발잠열 = 0.680;
            return G_HU_ld_ztc_t() * 1 * 증발잠열;
        }

        // CALC.cs가 idx(0~8759)/zone 순서로 매 시간 호출. 월 시작(리셋)/월 마감(가중치 f_wd) 처리까지 포함.
        public void Cal_hourly(int idx)
        {
            this.idx = idx;
            int mth = 0;
            int h = idx % 24 + 1;
            double f_wd = 0;
            bool isMonthEnd;

            for (int m = 1; m <= 11; m++)
            {
                if (idx >= MonthStartHour[m]) { mth++; }
            }
            isMonthEnd = mth == 11 ? idx == 8759 : idx == MonthStartHour[mth + 1] - 1;

            if (idx == MonthStartHour[mth])
            {
                Q_DHU_mth[mth] = 0;
                Q_HU_mth[mth] = 0;
                X_i[0, mth] = 0;
                X_i[1, mth] = 0;
            }

            for (int hu_dhu = 0; hu_dhu < 2; hu_dhu++)
            {
                x_int_a_ztc_t(hu_dhu);
                X_i[hu_dhu, mth] += x_int_a[hu_dhu, idx];
            }

            if (h >= StartHour && h < EndHour)
            {
                Q_DHU_mth[mth] += Q_DHU_h();
                Q_HU_mth[mth] += Q_HU_h();
            }

            if (isMonthEnd) //달의 마지막 idx 일 때 누적값 평균화 
            {
                f_wd = dwd_mth[mth] / dmth[mth];
                Q_DHU_mth[mth] *= f_wd;
                Q_HU_mth[mth] *= f_wd;
                X_i[0, mth] /= dmth[mth] * 24;
                X_i[1, mth] /= dmth[mth] * 24;
            }
        }
    }
}
