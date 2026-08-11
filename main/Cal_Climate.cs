using System;

namespace main
{
    internal class Cal_Climate
    {
        const double TZ = 9; // 표준시간대[h] — KIAEBS S-19 고정값(◯14)
        const int DAYS_PER_YEAR = 365;
        const int HOURS_PER_YEAR = 8760;

        public double[] delta = new double[DAYS_PER_YEAR];  // δ, 태양 적위[°] — n_day(1~365) 기준
        public double[] omega = new double[HOURS_PER_YEAR]; // ω, 태양시각[°] — n_hour(1~8760) 기준

        // 2.2.1 태양시각(ω) — ISO 52010-1
        public void Cal_SolarTime(double lambda_w) // λw, 대지 경도[°] — 관측소 좌표가 아닌 대지 고유 입력값
        {
            for (int n_day = 1; n_day <= DAYS_PER_YEAR; n_day++)
            {
                double R_dc = 360.0 / DAYS_PER_YEAR * n_day; // 지구 궤도 편차[°], ◯8
                delta[n_day - 1] = SolarDeclination(R_dc);
            }

            double t_schift = TZ - lambda_w / 15; // 지연시간차[h], ◯13

            for (int n_hour = 1; n_hour <= HOURS_PER_YEAR; n_hour++)
            {
                int n_day = (n_hour - 1) / 24 + 1;
                double t_eq = EquationOfTime(n_day);       // 균시차[h], ◯12
                double t_sol = n_hour - t_eq / 60 - t_schift; // 태양시, ◯11

                double w = 180.0 / 12 * (12.5 - t_sol); // 태양시각[°], ◯10
                if (w > 180) w -= 360;
                if (w < -180) w += 360;
                omega[n_hour - 1] = w;
            }
        }

        static double SolarDeclination(double R_dc) // δ, ISO 52010-1 <식 1>
        {
            double r1 = DegToRad(R_dc), r2 = DegToRad(2 * R_dc), r3 = DegToRad(3 * R_dc);
            return 0.33281
                 - 22.984 * Math.Cos(r1) - 0.3499 * Math.Cos(r2) - 0.1398 * Math.Cos(r3)
                 + 3.7872 * Math.Sin(r1) + 0.03205 * Math.Sin(r2) + 0.7187 * Math.Sin(r3);
        }

        static double EquationOfTime(int n_day) // t_eq, ISO 52010-1 <식 3>~<식 7>
        {
            if (n_day < 21) return 2.6 + 0.44 * n_day;
            if (n_day < 136) return 5.2 + 9 * Math.Cos((n_day - 43) * 0.0357);
            if (n_day < 241) return 1.4 - 5 * Math.Cos((n_day - 135) * 0.0449);
            if (n_day < 336) return -6.3 - 10 * Math.Cos((n_day - 306) * 0.036); // 기술서 원문 360 → 306 오타 보정(균시차 최소값 시점 11/3과 일치)
            return 0.45 * (n_day - 359);
        }

        static double DegToRad(double deg) => deg * Math.PI / 180;
    }
}
