using System;

namespace main
{
    internal class Cal_Climate
    {
        const double TZ = 9; // 표준시간대[h] — KIAEBS S-19 고정값(14)
        const int days_per_year = 365;
        const int hours_per_year = 8760;

        public double[] delta = new double[days_per_year];  // δ, 태양 적위[°] — n_day(1~365) 기준
        public double[] omega = new double[hours_per_year]; // ω, 태양시각[°] — n_hour(1~8760) 기준

        public double[] theta_z = new double[hours_per_year];   // θz, 천정각[°]
        public double[] alpha_sol = new double[hours_per_year]; // αsol, 태양 고도각[°]
        public double[] m = new double[hours_per_year];         // m, 공기 질량[-]

        public double[] Gsol_d = new double[hours_per_year]; // Gsol,d, 산란일사량[Wh/m2] — 시간별기후데이터 원자료
        public double[] Gsol_b = new double[hours_per_year]; // Gsol,b, 직달일사량[Wh/m2] — 시간별기후데이터 원자료

        public double[] epsilon = new double[hours_per_year];   // ε, 청명도 매개변수[-]
        public double[] Delta_sky = new double[hours_per_year]; // Δ, 하늘 밝기 매개변수[-]
        public double[] b = new double[hours_per_year];         // b, 천정각 보정계수[-]
        public double[] I_ext = new double[days_per_year];      // Iext, 대기권 태양일사량[W/m2] — n_day 기준

        public double[] F1 = new double[hours_per_year]; // F1, 천공에서 태양 주변부 밝기계수[-]
        public double[] F2 = new double[hours_per_year]; // F2, 지평선 밝기계수[-]

        // f11,f12,f13,f21,f22,f23 — 청명도 매개변수(ε) 구간별, ISO 52010-1 <표 8> / Annex J-1 <표 2>
        // basedb_resystem.sqlite의 청명도밝기계수 테이블에서 로드(인덱스 1~8 순서, 8행 상한은 사실상 무한대인 9999)
        double[] BrightnessLimit = new double[8]; // 인덱스별 ε 상한
        double[,] BrightnessCoeff = new double[8, 6]; // 인덱스별 f11~f23

        // 기준DB 조회(사용자 입력 아님) — LoadData 아님
       
        public double lambda_w; // λw, 대지 경도[°] — 관측소 좌표가 아닌 대지 고유 입력값
        public double phi_w;    // φw, 대지 위도[°] — 관측소 좌표가 아닌 대지 고유 입력값


        // Gsol,d·Gsol,b 시간별 원자료 — 기준DB 조회(사용자 입력 아님) — LoadData 아님
        // 지역 선택(BuildingGeneral.지역)은 조회할 지역을 고르는 데만 쓰고, 값 자체는 기준DB에서 옴
        public void LoadData_Weather()
        {
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "경도,위도", "");
            if (Value.Length > 0)
            {
                lambda_w = Program.UTIL.ToDoubleOrZero(Value[0][0]);
                phi_w = Program.UTIL.ToDoubleOrZero(Value[0][1]);
            }

            string[][] region = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
            if (region.Length == 0) return;

            string[][] rows = Program.DB.getValue(DB.type.BaseDB_RESystem, "시간별기후데이터", "산란일사량,직달일사량",
                "지역 = '" + region[0][0] + "' ORDER BY 월,일,시");

            for (int i = 0; i < hours_per_year && i < rows.Length; i++)
            {
                Gsol_d[i] = Program.UTIL.ToDoubleOrZero(rows[i][0]);
                Gsol_b[i] = Program.UTIL.ToDoubleOrZero(rows[i][1]);
            }
        }

        // 2.2.1 태양시각(ω) — ISO 52010-1
        public void Cal_SolarTime()
        {
            for (int i = 0; i < days_per_year; i++)
            {
                int n_day = i + 1; // 스펙상 n_day는 1~365 — R_dc 계산식에 값 자체가 쓰임
                double R_dc = 360.0 / days_per_year * n_day; // 지구 궤도 편차[°], 8

                double r1 = DegToRad(R_dc), r2 = DegToRad(2 * R_dc), r3 = DegToRad(3 * R_dc);
                delta[i] = 0.33281 // δ, 태양 적위[°], 7, ISO 52010-1 <식 1>
                     - 22.984 * Math.Cos(r1) - 0.3499 * Math.Cos(r2) - 0.1398 * Math.Cos(r3)
                     + 3.7872 * Math.Sin(r1) + 0.03205 * Math.Sin(r2) + 0.07187 * Math.Sin(r3); // ISO 원문 0.07187 — 기술서(KIAEBS) 0.7187은 오타
            }

            double t_schift = TZ - lambda_w / 15; // 지연시간차[h], 13

            for (int i = 0; i < hours_per_year; i++)
            {
                int n_hour = i + 1; // 스펙상 n_hour는 1~8760 — t_sol 계산식에 값 자체가 쓰임
                int n_day = i / 24 + 1;

                double t_eq; // 균시차[h], 12, ISO 52010-1 <식 3>~<식 7>
                if (n_day < 21) t_eq = 2.6 + 0.44 * n_day;
                else if (n_day < 136) t_eq = 5.2 + 9 * Math.Cos((n_day - 43) * 0.0357);
                else if (n_day < 241) t_eq = 1.4 - 5 * Math.Cos((n_day - 135) * 0.0449);
                else if (n_day < 336) t_eq = -6.3 - 10 * Math.Cos((n_day - 306) * 0.036); // 기술서 원문 360 → 306 오타 보정(균시차 최소값 시점 11/3과 일치)
                else t_eq = 0.45 * (n_day - 359);

                double t_sol = n_hour - t_eq / 60 - t_schift; // 태양시, 11

                double w = 180.0 / 12 * (12.5 - t_sol); // 태양시각[°], 10
                if (w > 180) w = w - 360;
                if (w < -180) w = w + 360;
                omega[i] = w;
            }
        }

        // 2.2.2 임의 경사면과 태양위치 — 대지 전역값(태양 고도각·천정각·공기질량) — ISO 52010-1
        public void Cal_SolarPosition()
        {
            for (int i = 0; i < hours_per_year; i++)
            {
                int n_day = i / 24 + 1;

                double alpha = RadToDeg(Math.Asin( // αsol, 태양 고도각[°], 18, ISO 52010-1 <식 11>
                      Math.Sin(DegToRad(delta[n_day - 1])) * Math.Sin(DegToRad(phi_w))
                    + Math.Cos(DegToRad(delta[n_day - 1])) * Math.Cos(DegToRad(phi_w)) * Math.Cos(DegToRad(omega[i]))));
                if (alpha < 0.0001) alpha = 0;
                alpha_sol[i] = alpha;

                theta_z[i] = 90 - alpha; // 천정각[°], 17

                m[i] = alpha >= 10 // 공기 질량[-], 22, ISO 52010-1 <식 20>~<식 21>
                    ? 1 / Math.Sin(DegToRad(alpha))
                    : 1 / (Math.Sin(DegToRad(alpha)) + 0.15 * Math.Pow(alpha + 3.885, -1.253));
            }
        }

        // 2.2.3 천공지수와 임의 경사면 직달일사량 — 대지 전역값(청명도·하늘밝기·천정각보정) — ISO 52010-1
        public void Cal_SkyClearness()
        {
            const double K = 1.014;     // 청명도 계산식의 상수[-], 30
            const double Gsol_c = 1370; // 태양상수[W/m2], 41

            for (int i = 0; i < days_per_year; i++)
            {
                int n_day = i + 1;
                I_ext[i] = Gsol_c * (1 + 0.033 * Math.Cos(DegToRad(360.0 / days_per_year * n_day))); // Iext, 40, ISO 52010-1 <식 27>
            }

            for (int i = 0; i < hours_per_year; i++)
            {
                int n_day = i / 24 + 1;

                b[i] = Math.Max(Math.Cos(DegToRad(85)), Math.Cos(DegToRad(theta_z[i]))); // 천정각 보정계수, 31, ISO 52010-1 <식 29>

                if (Gsol_d[i] == 0)
                {
                    epsilon[i] = 999; // 28
                }
                else
                {
                    double k3 = K * Math.Pow(DegToRad(alpha_sol[i]), 3);
                    epsilon[i] = ((Gsol_d[i] + Gsol_b[i]) / Gsol_d[i] + k3) / (1 + k3); // 청명도 매개변수, 28, ISO 52010-1 <식 30>
                }

                Delta_sky[i] = m[i] * Gsol_d[i] / I_ext[n_day - 1]; // 하늘 밝기 매개변수, 35, ISO 52010-1 <식 31>
            }
        }

        // 2.2.4 임의 경사면 산란일사량 — 대지 전역값(밝기계수) — ISO 52010-1
        public void Cal_BrightnessCoeff()
        {
            string[][] rows = Program.DB.getValue(DB.type.BaseDB_RESystem, "청명도밝기계수", "상한,f11,f12,f13,f21,f22,f23", "인덱스 >= 1 ORDER BY 인덱스");
            for (int idx = 0; idx < rows.Length; idx++)
            {
                BrightnessLimit[idx] = Program.UTIL.ToDoubleOrZero(rows[idx][0]);
                for (int col = 0; col < 6; col++)
                {
                    BrightnessCoeff[idx, col] = Program.UTIL.ToDoubleOrZero(rows[idx][col + 1]);
                }
            }

            for (int i = 0; i < hours_per_year; i++)
            {
                int idx = 0; // 청명도 매개변수(ε) 구간 인덱스, BrightnessCoeff 행 번호
                while (epsilon[i] >= BrightnessLimit[idx]) idx++;

                double f11 = BrightnessCoeff[idx, 0], f12 = BrightnessCoeff[idx, 1], f13 = BrightnessCoeff[idx, 2];
                double f21 = BrightnessCoeff[idx, 3], f22 = BrightnessCoeff[idx, 4], f23 = BrightnessCoeff[idx, 5];
                double theta_z_rad = DegToRad(theta_z[i]);

                F1[i] = Math.Max(0, f11 + f12 * Delta_sky[i] + f13 * theta_z_rad); // 천공에서 태양 주변부 밝기계수, 44, ISO 52010-1 <식 32>
                F2[i] = f21 + f22 * Delta_sky[i] + f23 * theta_z_rad;              // 지평선 밝기계수, 49, ISO 52010-1 <식 33>
            }
        }

        // 임의 경사면(벽·지붕)마다 βic·γic가 달라져 배열로 소유하지 않고, 표면별로 호출해 쓰는 계산
        // i는 시간 인덱스(0~8759) — 외피 정보가 아니라 이미 있는 시간별 필드에서 값 하나를 짚는 용도
        double Cal_beta_sol_ic(double beta_ic, int i) // βsol,ic, ISO 52010-1 <식 19>
        {
            double diff = beta_ic - theta_z[i];
            double beta_sol_ic;
            if (diff > 180) beta_sol_ic = diff - 360;
            else if (diff < -180) beta_sol_ic = diff + 360;
            else beta_sol_ic = diff;

            return beta_sol_ic;
        }

        double Cal_gamma_sol_ic(double gamma_ic, int i) // γsol,ic, ISO 52010-1 <식 18>
        {
            double diff = omega[i] - gamma_ic;
            double gamma_sol_ic;
            if (diff > 180) gamma_sol_ic = diff - 360;
            else if (diff < -180) gamma_sol_ic = diff + 360;
            else gamma_sol_ic = diff;

            return gamma_sol_ic;
        }

        double Cal_theta_sol_ic(double beta_ic, double gamma_ic, int i) // θsol,ic, ISO 52010-1 <식 17>
        {
            int n_day = i / 24 + 1;
            double d = DegToRad(delta[n_day - 1]), p = DegToRad(phi_w), b = DegToRad(beta_ic), g = DegToRad(gamma_ic), w = DegToRad(omega[i]);

            double cos_theta_sol_ic =
                  Math.Sin(d) * Math.Sin(p) * Math.Cos(b)
                - Math.Sin(d) * Math.Cos(p) * Math.Sin(b) * Math.Cos(g)
                + Math.Cos(d) * Math.Cos(p) * Math.Cos(b) * Math.Cos(w)
                + Math.Cos(d) * Math.Sin(p) * Math.Sin(b) * Math.Cos(g) * Math.Cos(w)
                + Math.Cos(d) * Math.Sin(b) * Math.Sin(g) * Math.Sin(w);

            double theta_sol_ic = RadToDeg(Math.Acos(cos_theta_sol_ic));

            return theta_sol_ic;
        }

        double Cal_Idir(double theta_sol_ic, int i) // Idir, 임의 경사면 직달일사량[W/m2], 37, ISO 52010-1 <식 26>
        {
            return Math.Max(0, Gsol_b[i] * Math.Cos(DegToRad(theta_sol_ic)));
        }

        double Cal_Idif(double beta_ic, double gamma_ic, int i) // Idif, 임의 경사면 산란일사량[W/m2], 43, ISO 52010-1 <식 34>
        {
            double theta_sol_ic = Cal_theta_sol_ic(beta_ic, gamma_ic, i);
            double a = Math.Max(0, Math.Cos(DegToRad(theta_sol_ic))); // 입사각 보정계수, 33, ISO 52010-1 <식 28> — 기술서(KIAEBS)는 cos(αsol)로 오기재, ISO 원문은 cos(θsol,ic)

            return Gsol_d[i] * ((1 - F1[i]) * (1 + Math.Cos(DegToRad(beta_ic))) / 2
                              + F1[i] * a / b[i]
                              + F2[i] * Math.Sin(DegToRad(beta_ic)));
        }

        // 2.2.5 임의 경사면 총일사량 — 표면별, 시간별 (월별 집계 ◯70~72는 실제 표면 루프 연결할 때 캐싱 구조와 같이 처리 예정)
        double Cal_Idif_grnd(double beta_ic, int i) // Idif,grnd, 임의 경사면 반사 일사량[W/m2], ◯56, ISO 52010-1 <식 35>
        {
            const double rho_sol_grnd = 0.2; // 지면 반사율[-], 표준값, ISO 52010-1 <표 B.5>

            return (Gsol_d[i] + Gsol_b[i] * Math.Sin(DegToRad(alpha_sol[i]))) * rho_sol_grnd * (1 - Math.Cos(DegToRad(beta_ic))) / 2;
        }

        double Cal_Icircum(double beta_ic, double gamma_ic, int i) // Icircum, 태양 주변부 산란 일사량[W/m2], ◯63, ISO 52010-1 <식 36>
        {
            double theta_sol_ic = Cal_theta_sol_ic(beta_ic, gamma_ic, i);
            double a = Math.Max(0, Math.Cos(DegToRad(theta_sol_ic)));

            return Gsol_d[i] * F1[i] * a / b[i];
        }

        double Cal_Idir_tot(double beta_ic, double gamma_ic, int i) // Idir,tot, 임의 경사면 총직달일사량[W/m2], ◯68, ISO 52010-1 <식 37>
        {
            double theta_sol_ic = Cal_theta_sol_ic(beta_ic, gamma_ic, i);
            double Idir = Cal_Idir(theta_sol_ic, i);
            double Icircum = Cal_Icircum(beta_ic, gamma_ic, i);

            return Idir + Icircum;
        }

        double Cal_Idif_tot(double beta_ic, double gamma_ic, int i) // Idif,tot, 임의 경사면 총산란일사량[W/m2], ◯55, ISO 52010-1 <식 38>
        {
            double Idif = Cal_Idif(beta_ic, gamma_ic, i);
            double Icircum = Cal_Icircum(beta_ic, gamma_ic, i);
            double Idif_grnd = Cal_Idif_grnd(beta_ic, i);

            return Idif - Icircum + Idif_grnd;
        }

        double Cal_Itot(double beta_ic, double gamma_ic, int i) // Itot, 임의 경사면 총일사량[W/m2], ◯54, ISO 52010-1 <식 39>
        {
            double Idir_tot = Cal_Idir_tot(beta_ic, gamma_ic, i);
            double Idif_tot = Cal_Idif_tot(beta_ic, gamma_ic, i);

            return Idir_tot + Idif_tot;
        }

        static double DegToRad(double deg) => deg * Math.PI / 180;
        static double RadToDeg(double rad) => rad * 180 / Math.PI;
    }
}
