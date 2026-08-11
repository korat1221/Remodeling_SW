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

        public double lambda_w; // λw, 대지 경도[°] — 관측소 좌표가 아닌 대지 고유 입력값, 호출 전에 세팅

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

        static double Cal_delta(double R_dc) // δ, ISO 52010-1 <식 1>
        {
            double delta = 0;

            double r1 = DegToRad(R_dc);
            double r2 = DegToRad(2 * R_dc);
            double r3 = DegToRad(3 * R_dc);

            delta = 0.33281
                 - 22.984 * Math.Cos(r1) - 0.3499 * Math.Cos(r2) - 0.1398 * Math.Cos(r3)
                 + 3.7872 * Math.Sin(r1) + 0.03205 * Math.Sin(r2) + 0.7187 * Math.Sin(r3);

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

        static double DegToRad(double deg) => deg * Math.PI / 180;
    }
}
