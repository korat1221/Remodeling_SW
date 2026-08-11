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

        public double lambda_w; // λw, 대지 경도[°] — 관측소 좌표가 아닌 대지 고유 입력값
        public double phi_w;    // φw, 대지 위도[°] — 관측소 좌표가 아닌 대지 고유 입력값

        public void LoadData_SiteCoord()
        {
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "경도,위도", "");
            if (Value.Length > 0)
            {
                lambda_w = Program.UTIL.ToDoubleOrZero(Value[0][0]);
                phi_w = Program.UTIL.ToDoubleOrZero(Value[0][1]);
            }
        }

        // 2.2.1 태양시각(ω) — ISO 52010-1
        public void Cal_SolarTime()
        {
            for (int i = 0; i < days_per_year; i++)
            {
                int n_day = i + 1; // 스펙상 n_day는 1~365 — R_dc 계산식에 값 자체가 쓰임
                double R_dc = 360.0 / days_per_year * n_day; // 지구 궤도 편차[°], 8
                delta[i] = Cal_delta(R_dc);
            }

            double t_schift = TZ - lambda_w / 15; // 지연시간차[h], 13

            for (int i = 0; i < hours_per_year; i++)
            {
                int n_hour = i + 1; // 스펙상 n_hour는 1~8760 — t_sol 계산식에 값 자체가 쓰임
                int n_day = i / 24 + 1;
                double t_eq = Cal_t_eq(n_day);       // 균시차[h], 12
                double t_sol = n_hour - t_eq / 60 - t_schift; // 태양시, 11

                double w = 180.0 / 12 * (12.5 - t_sol); // 태양시각[°], 10
                if (w > 180) w = w - 360;
                if (w < -180) w = w + 360;
                omega[i] = w;
            }
        }

        // 2.2.2 임의 경사면과 태양위치 — 대지 전역값(태양 고도각·천정각·공기질량) — ISO 52010-1
        public void Cal_SunPosition()
        {
            for (int i = 0; i < hours_per_year; i++)
            {
                int n_day = i / 24 + 1;
                alpha_sol[i] = Cal_alpha_sol(delta[n_day - 1], phi_w, omega[i]); // 태양 고도각, ◯18
                theta_z[i] = 90 - alpha_sol[i]; // 천정각, ◯17
                m[i] = Cal_m(alpha_sol[i]);     // 공기 질량, ◯22
            }
        }

        static double Cal_delta(double R_dc) // δ, ISO 52010-1 <식 1>
        {
            double delta = 0;

            double r1 = DegToRad(R_dc);
            double r2 = DegToRad(2 * R_dc);
            double r3 = DegToRad(3 * R_dc);

            delta = 0.33281
                 - 22.984 * Math.Cos(r1) - 0.3499 * Math.Cos(r2) - 0.1398 * Math.Cos(r3)
                 + 3.7872 * Math.Sin(r1) + 0.03205 * Math.Sin(r2) + 0.07187 * Math.Sin(r3); // ISO 원문 0.07187 — 기술서(KIAEBS) 0.7187은 오타

            return delta;
        }

        static double Cal_t_eq(int n_day) // t_eq, ISO 52010-1 <식 3>~<식 7>
        {
            double t_eq = 0;
            if (n_day < 21) t_eq = 2.6 + 0.44 * n_day;
            else if (n_day < 136) t_eq = 5.2 + 9 * Math.Cos((n_day - 43) * 0.0357);
            else if (n_day < 241) t_eq = 1.4 - 5 * Math.Cos((n_day - 135) * 0.0449);
            else if (n_day < 336) t_eq = -6.3 - 10 * Math.Cos((n_day - 306) * 0.036); // 기술서 원문 360 → 306 오타 보정(균시차 최소값 시점 11/3과 일치)
            else t_eq = 0.45 * (n_day - 359);

            return t_eq;
        }

        static double Cal_alpha_sol(double delta, double phi_w, double omega) // αsol, ISO 52010-1 <식 11>
        {
            double alpha_sol = RadToDeg(Math.Asin(
                  Math.Sin(DegToRad(delta)) * Math.Sin(DegToRad(phi_w))
                + Math.Cos(DegToRad(delta)) * Math.Cos(DegToRad(phi_w)) * Math.Cos(DegToRad(omega))));

            if (alpha_sol < 0.0001) alpha_sol = 0;

            return alpha_sol;
        }

        static double Cal_m(double alpha_sol) // m, ISO 52010-1 <식 20>~<식 21>
        {
            double m;
            if (alpha_sol >= 10) m = 1 / Math.Sin(DegToRad(alpha_sol));
            else m = 1 / (Math.Sin(DegToRad(alpha_sol)) + 0.15 * Math.Pow(alpha_sol + 3.885, -1.253));

            return m;
        }

        // 임의 경사면(벽·지붕)마다 βic·γic가 달라져 배열로 소유하지 않고, 표면별로 호출해 쓰는 스칼라 계산
        static double Cal_beta_sol_ic(double beta_ic, double theta_z) // βsol,ic, ISO 52010-1 <식 19>
        {
            double diff = beta_ic - theta_z;
            double beta_sol_ic;
            if (diff > 180) beta_sol_ic = diff - 360;
            else if (diff < -180) beta_sol_ic = diff + 360;
            else beta_sol_ic = diff;

            return beta_sol_ic;
        }

        static double Cal_gamma_sol_ic(double gamma_ic, double omega) // γsol,ic, ISO 52010-1 <식 18>
        {
            double diff = omega - gamma_ic;
            double gamma_sol_ic;
            if (diff > 180) gamma_sol_ic = diff - 360;
            else if (diff < -180) gamma_sol_ic = diff + 360;
            else gamma_sol_ic = diff;

            return gamma_sol_ic;
        }

        static double Cal_theta_sol_ic(double beta_ic, double gamma_ic, double delta, double phi_w, double omega) // θsol,ic, ISO 52010-1 <식 17>
        {
            double d = DegToRad(delta), p = DegToRad(phi_w), b = DegToRad(beta_ic), g = DegToRad(gamma_ic), w = DegToRad(omega);

            double cos_theta_sol_ic =
                  Math.Sin(d) * Math.Sin(p) * Math.Cos(b)
                - Math.Sin(d) * Math.Cos(p) * Math.Sin(b) * Math.Cos(g)
                + Math.Cos(d) * Math.Cos(p) * Math.Cos(b) * Math.Cos(w)
                + Math.Cos(d) * Math.Sin(p) * Math.Sin(b) * Math.Cos(g) * Math.Cos(w)
                + Math.Cos(d) * Math.Sin(b) * Math.Sin(g) * Math.Sin(w);

            double theta_sol_ic = RadToDeg(Math.Acos(cos_theta_sol_ic));

            return theta_sol_ic;
        }

        static double DegToRad(double deg) => deg * Math.PI / 180;
        static double RadToDeg(double rad) => rad * 180 / Math.PI;
    }
}
