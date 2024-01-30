using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    internal class Cal_FinalEnergy
    {
        public double[] Qhf_gas = new double[12], Qhf_elec = new double[12];
        public double[] Qcf_gas = new double[12], Qcf_elec = new double[12];
        public double[] Qwf_gas = new double[12], Qwf_elec = new double[12];
        public double[] Qlf_elec = new double[12];
        public double[] Qvf_gas = new double[12], Qvf_elec = new double[12]; //공조
        public double[] Qreg_elec = new double[12];//신재생
        public double[] Qbase_gas = new double[12], Qbase_elec = new double[12];
        public double[] Qf_gas_tot1 = new double[12], Qf_elec_tot1 = new double[12];
        public double[] Qf_gas_tot_mth = new double[12], Qf_elec_tot_mth = new double[12];
        public double Qf_gas_tot_a, Qf_elec_tot_a; 
        public double[] Quse_gas_mth = new double[12], Quse_elec_mth = new double[12];
        public double Quse_gas_a, Quse_elec_a;
        public double[] Error_gas_mth = new double[12], Error_elec_mth = new double[12];
        public double Error_gas_a, Error_elec_a;
        public Cal_FinalEnergy()
        {
            //난방
            for (int mth = 0; mth < 12; mth++)
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Result", "연료,Qh_f,Wh_ce,Wh_d,Wh_s,Wh_g", "월='" + (mth + 1).ToString() + "월'");
                if (Value.Length > 0)
                {
                    for (int i = 0; i < Value.Length; i++) //시스템별 
                    {
                        if (Value[i][0].ToString() == "전기")
                        {
                            Qhf_elec[mth] += (Convert.ToDouble(Value[i][1]) + Convert.ToDouble(Value[i][2]) + Convert.ToDouble(Value[i][3]) + Convert.ToDouble(Value[i][4]) + Convert.ToDouble(Value[i][5]));
                        }
                        else
                        {
                            Qhf_elec[mth] += (Convert.ToDouble(Value[i][2]) + Convert.ToDouble(Value[i][3]) + Convert.ToDouble(Value[i][4]) + Convert.ToDouble(Value[i][5]));
                            Qhf_gas[mth] += Convert.ToDouble(Value[i][1]);
                        }
                    }
                }
            }
            //급탕 
            for (int mth = 0; mth < 12; mth++)
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Result", "연료,Qw_f,Ww_d,Ww_s,Ww_g", "월='" + (mth + 1).ToString() + "월'");
                if (Value.Length > 0)
                {
                    for (int i = 0; i < Value.Length; i++) //시스템별
                    {
                        if (Value[i][0].ToString() == "전기")
                        {
                            Qwf_elec[mth] += (Convert.ToDouble(Value[i][1]) + Convert.ToDouble(Value[i][2]) + Convert.ToDouble(Value[i][3]) + Convert.ToDouble(Value[i][4]));
                        }
                        else
                        {
                            Qwf_elec[mth] += (Convert.ToDouble(Value[i][2]) + Convert.ToDouble(Value[i][3]) + Convert.ToDouble(Value[i][4]));
                            Qwf_gas[mth] += Convert.ToDouble(Value[i][1]);
                        }
                    }
                }
            }
            //조명 
            for (int mth = 0; mth < 12; mth++)
            {
                String[][] Value = Program.DB.querySQL(DB.type.ProjDB, "select Sum(Final_kWh) From Zone_LightResult where 월 = '" + (mth + 1).ToString() + "월'");
                if (Value.Length > 0)
                {
                    Qlf_elec[mth] += Convert.ToDouble(Value[0][0]);
                }
            }
            //신재생
            for (int mth = 0; mth < 12; mth++)
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "PV_Result", "전기생산량", "월 ='" + (mth + 1).ToString() + "월'");
                if (Value.Length > 0)
                {
                    Qreg_elec[mth] += Convert.ToDouble(Value[0][0]);
                }
            }
            for (int mth = 0; mth < 12; mth++)
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Result", "Fuel,QC_ce,QC_d,QC_s,QC_out,QC_f", "월='" + (mth + 1).ToString() + "월'");
                if (Value.Length > 0)
                {
                    for (int i = 0; i < Value.Length; i++) //시스템별
                    {
                        if (Value[i][0].ToString() == "전기")
                        {
                            Qcf_elec[mth] += Convert.ToDouble(Value[i][5]); //나중에 보조설비 에너지 합산 해야함 
                        }
                        else
                        {
                            Qcf_elec[mth] += 0; //나중에 보조설비 에너지 합산 해야함 
                            Qcf_gas[mth] += 0;
                        }
                    }
                }
            }


           //에너지사용량
           string[][] Value1 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "사용시작일", "연료='전기'");
            string[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "사용시작일", "연료='가스'");
            if (Value1.Length > 0)
            {
                if (Convert.ToDouble(Value1[0][0]) > 1)
                {
                    string[][] Elec1, Elec2;
                    for (int mth = 0; mth < 11; mth++)
                    {
                        Elec1 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (mth + 1).ToString() + "월' AND 연료='전기'");
                        Elec2 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (mth + 2).ToString() + "월' AND 연료='전기'");
                        if (Elec1.Length > 0 && Elec2.Length > 0)
                        {
                            for (int i = 0; i < Elec1.Length; i++) //연도별
                            {
                                Quse_elec_mth[mth] += (Convert.ToDouble(Elec1[i][0]) * Convert.ToDouble(Value1[0][0]) / 30 + Convert.ToDouble(Elec2[i][0]) * (30 - Convert.ToDouble(Value1[0][0])) / 30);
                            }
                        }
                        Quse_elec_mth[mth] = Quse_elec_mth[mth] / Elec1.Length;
                    }

                    Elec1 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (12).ToString() + "월' AND 연료='전기'");
                    Elec2 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (1).ToString() + "월' AND 연료='전기'");
                    if (Elec1.Length > 0 && Elec2.Length > 0)
                    {
                        for (int i = 0; i < Elec1.Length; i++) //연도별
                        {
                            Quse_elec_mth[12] += (Convert.ToDouble(Elec1[i][0]) * Convert.ToDouble(Value1[0][0]) / 30 + Convert.ToDouble(Elec2[i][0]) * (30 - Convert.ToDouble(Value1[0][0])) / 30);
                        }
                    }
                    Quse_elec_mth[12] = Quse_elec_mth[12] / Elec1.Length;

                }
                else
                {
                    for (int mth = 0; mth < 12; mth++)
                    {
                        string[][] Elec = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (mth + 1).ToString() + "월' AND 연료='전기'");
                        if (Elec.Length > 0)
                        {
                            for (int i = 0; i < Elec.Length; i++) //연도별
                            {
                                Quse_elec_mth[mth] += Convert.ToDouble(Elec[i][0]);
                            }
                        }
                        Quse_elec_mth[mth] = Quse_elec_mth[mth] / Elec.Length;
                    }

                }
            }
            if (Value2.Length > 0)
            {
                if (Convert.ToDouble(Value2[0][0]) > 1)
                {
                    string[][] Gas1, Gas2;
                    for (int mth = 0; mth < 11; mth++)
                    {
                        Gas1 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (mth + 1).ToString() + "월' AND 연료='가스' AND 단위 ='kWh'");
                        Gas2 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (mth + 2).ToString() + "월' AND 연료='가스' AND 단위 ='kWh'");
                        if (Gas1.Length > 0 && Gas2.Length > 0)
                        {
                            for (int i = 0; i < Gas1.Length; i++) //연도별
                            {
                                Quse_gas_mth[mth] += (Convert.ToDouble(Gas1[i][0]) * Convert.ToDouble(Value2[0][0]) / 30 + Convert.ToDouble(Gas2[i][0]) * (30 - Convert.ToDouble(Value2[0][0])) / 30);
                            }
                        }
                        Quse_gas_mth[mth] = Quse_gas_mth[mth] / Gas1.Length;
                    }

                    Gas1 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (12).ToString() + "월' AND 연료='가스' AND 단위 ='kWh'");
                    Gas2 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (1).ToString() + "월' AND 연료='가스' AND 단위 ='kWh'");
                    if (Gas1.Length > 0 && Gas2.Length > 0)
                    {
                        for (int i = 0; i < Gas1.Length; i++) //연도별
                        {
                            Quse_gas_mth[12] += (Convert.ToDouble(Gas1[i][0]) * Convert.ToDouble(Value2[0][0]) / 30 + Convert.ToDouble(Gas2[i][0]) * (30 - Convert.ToDouble(Value2[0][0])) / 30);
                        }
                    }
                    Quse_gas_mth[12] = Quse_gas_mth[12] / Gas1.Length;


                }
                else
                {
                    for (int mth = 0; mth < 12; mth++)
                    {
                        string[][] Gas = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (mth + 1).ToString() + "월' AND 연료='가스' AND 단위 ='kWh'");
                        if (Gas.Length > 0)
                        {
                            for (int i = 0; i < Gas.Length; i++) //연도별
                            {
                                Quse_gas_mth[mth] += Convert.ToDouble(Gas[i][0]);
                            }
                        }
                        Quse_gas_mth[mth] = Quse_gas_mth[mth] / Gas.Length;
                    }

                }
            }

            for (int mth = 0; mth < 12; mth++)
            {
                Qf_elec_tot1[mth] = Qhf_elec[mth] + Qcf_elec[mth] + Qwf_elec[mth] + Qlf_elec[mth] + Qvf_elec[mth];
                Qf_gas_tot1[mth] = Qhf_gas[mth] + Qcf_gas[mth] + Qwf_gas[mth] + Qvf_gas[mth];
            }
        }

        public void Calc_Qbase_elec()
        {
            double beta, alpha;
            double[] x = new double[12];
            double[] y = new double[12];

            double[] Error_elec = new double[12];
            for (int mth = 0; mth < 12; mth++)
            {
                Error_elec[mth] = Quse_elec_mth[mth] - Qf_elec_tot1[mth] + Qreg_elec[mth];
                x[mth] = Qhf_elec[mth]; y[mth] = Error_elec[mth];
            }

            double a = 0, b = 0, c = 0, d = 0, e = 0;

            for (int mth = 0; mth < 12; mth++)
            {
                a += (x[mth] * y[mth]);  //시그마 (xy)
                b += x[mth]; // 시그마 x
                c += y[mth]; // 시그마 y
                d += (x[mth] * x[mth]); // 시그마 x^2
                e = b * b; //(시그마 x)^2
            }

            beta = (12 * a - b * c) / (12 * d - e);
            alpha = c / 12 - beta * b / 12;

            for (int mth = 0; mth < 12; mth++)
            {
                Qbase_elec[mth] = alpha + beta * x[mth];

                if (Qbase_elec[mth] < 0)
                {
                    Qbase_elec[mth] = 0;
                }
                else
                {
                    Qbase_elec[mth] = Qbase_elec[mth];
                }
            }

            for (int mth = 0; mth < 12; mth++)
            {
                Qf_elec_tot_mth[mth] = Qf_elec_tot1[mth] + Qbase_elec[mth];
            }
        }
        public void Calc_Qbase_gas()
        {
            double beta, alpha;
            double[] x = new double[12];
            double[] y = new double[12];

            double[] Error_gas = new double[12];
            for (int mth = 0; mth < 12; mth++)
            {
                Error_gas[mth] = Quse_gas_mth[mth] - Qf_gas_tot1[mth];
                x[mth] = Qhf_gas[mth]; y[mth] = Error_gas[mth];
            }

            double a = 0, b = 0, c = 0, d = 0, e = 0;

            for (int mth = 0; mth < 12; mth++)
            {
                a += (x[mth] * y[mth]);
                b += x[mth];
                c += y[mth];
                d += (x[mth] * x[mth]);
                e = b * b;
            }

            beta = (12 * a - b * c) / (12 * d - e);
            alpha = c / 12 - beta * b / 12;

            for (int mth = 0; mth < 12; mth++)
            {
                Qbase_gas[mth] = alpha + beta * x[mth];

                if (Qbase_gas[mth] < 0)
                {
                    Qbase_gas[mth] = 0;
                }
                else
                {
                    Qbase_gas[mth] = Qbase_gas[mth];
                }
            }
            for (int mth = 0; mth < 12; mth++)
            {
                Qf_gas_tot_mth[mth] = Qf_gas_tot1[mth] + Qbase_gas[mth];
            }
        }

        public void Calc_Error()
        {
            
           for(int mth  =0; mth < 12;mth++)
            {
                Error_elec_mth[mth] = Math.Abs(Quse_elec_mth[mth] - Qf_elec_tot_mth[mth]);
                Error_gas_mth[mth] = Math.Abs(Quse_gas_mth[mth] - Qf_gas_tot_mth[mth]);

                Quse_elec_a += Quse_elec_mth[mth];
                Quse_gas_a += Quse_gas_mth[mth];
                Qf_elec_tot_a += Qf_elec_tot_mth[mth];
                Qf_gas_tot_a += Qf_gas_tot_mth[mth];
                Error_elec_a = Math.Abs(Quse_elec_a - Qf_elec_tot_a);
                Error_gas_a = Math.Abs(Quse_gas_a - Qf_gas_tot_a); 
            }
        }
    }
}
