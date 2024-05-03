using Eagle._Components.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    internal class Final
    {
        public string Carrier_h, Carrier_w, Carrier_c;
        public double[] Qhf_gas = new double[12], Qhf_elec = new double[12];
        public double[] Qcf_gas = new double[12], Qcf_elec = new double[12];
        public double[] Qwf_gas = new double[12], Qwf_elec = new double[12];
        public double[] Qlf_elec = new double[12];
        public double[] Qvf_elec = new double[12]; //공조
        public double[] Qreg_elec = new double[12];//신재생
        public double[] Qbase_gas = new double[12], Qbase_elec = new double[12];
        public double[] Qf_gas_tot1 = new double[12], Qf_elec_tot1 = new double[12];
        public double[] Qf_gas_tot_mth = new double[12], Qf_elec_tot_mth = new double[12];
        public double Qf_gas_tot_a, Qf_elec_tot_a; 
        public double[] Quse_gas_mth = new double[12], Quse_elec_mth = new double[12];
        public double Quse_gas_a, Quse_elec_a;
        public double[] Error_gas_mth = new double[12], Error_elec_mth = new double[12];
        public double Error_gas_a, Error_elec_a;
        public Final(string ProjNum)
        {
            #region 난방
            string[][] HeatingNum = Program.DB.getValue(ProjNum, "HeatingSystem_Form", "번호");
            if(HeatingNum.Length >0)
            {
                int i = -1;
                while (++i < HeatingNum.Length)
                {
                    Heating Heating1 = Program.CALC.getHeating(HeatingNum[i][0]);
                    for (int mth = 0; mth < 12; mth++)
                    {
                        if (Heating1.Carrier == "전기")
                        {
                            Qhf_elec[mth] += (Heating1.Qh_f[mth] + Heating1.Wh_ce[mth] + Heating1.Wh_d[mth] + Heating1.Wh_s[mth] + Heating1.Wh_g[mth]);
                        }
                        else
                        {
                            Carrier_h = Heating1.Carrier;
                            Qhf_elec[mth] += (Heating1.Wh_ce[mth] + Heating1.Wh_d[mth] + Heating1.Wh_s[mth] + Heating1.Wh_g[mth]);
                            Qhf_gas[mth] += Heating1.Qh_f[mth];
                        }
                    }
                }
            }
            #endregion 
            #region 급탕
            string[][] DHWNum = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "번호");
            if (DHWNum.Length > 0)
            {
                int i = -1;
                while (++i < DHWNum.Length)
                {
                    DHW DHW1 = Program.CALC.getDHW(DHWNum[i][0]);
                    for (int mth = 0; mth < 12; mth++)
                    {
                        if (DHW1.Carrier == "전기")
                        {
                            Qwf_elec[mth] += (DHW1.Qw_f[mth] + DHW1.Ww_d[mth] + DHW1.Ww_s[mth] + DHW1.Ww_g[mth]);
                        }
                        else
                        {
                            Carrier_w = DHW1.Carrier;
                            Qwf_elec[mth] += (DHW1.Ww_d[mth] + DHW1.Ww_s[mth] + DHW1.Ww_g[mth]);
                            Qwf_gas[mth] += DHW1.Qw_f[mth];
                        }
                    }
                }
            }
            #endregion 
            #region 조명
            string[][] ZoneNum = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호");
            if (ZoneNum.Length > 0)
            {
                int i = -1;
                while (++i < ZoneNum.Length)
                {
                    ZoneLight zoneLight1 = Program.CALC.getZoneLight(ZoneNum[i][0]);
                    for (int mth = 0; mth < 12; mth++)
                    {
                        Qlf_elec[mth] += zoneLight1.Zone_Final_kWh[mth];
                    }
                }
            }
            #endregion 
            //신재생
            for (int mth = 0; mth < 12; mth++)
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "PV_Result", "전기생산량", "월 ='" + (mth + 1).ToString() + "월'");
                if (Value.Length > 0)
                {
                    Qreg_elec[mth] += Convert.ToDouble(Value[0][0]);
                }
            }
            //냉방
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
                            Carrier_c = Value[i][0];
                            Qcf_elec[mth] += 0; //나중에 보조설비 에너지 합산 해야함 
                            Qcf_gas[mth] += 0;
                        }
                    }
                }
            }
            //공조 
            for (int mth = 0; mth < 12; mth++)
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "급기팬보조에너지,배기팬보조에너지,가습보조에너지,프리히팅보조에너지", "월='" + (mth + 1).ToString() + "월'");
                if (Value.Length > 0)
                {
                    for (int i = 0; i < Value.Length; i++) //시스템별
                    {
                        Qvf_elec[mth] += (Convert.ToDouble(Value[i][0]) + Convert.ToDouble(Value[i][1]) + Convert.ToDouble(Value[i][2]) + Convert.ToDouble(Value[i][3]));
                    }
                }
            }


            //에너지사용량
            string[][] Value1 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "사용시작일", "연료='전기'");
            string[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "사용시작일", "not 연료='전기' and not 연료='전체'");
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
                        Gas1 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (mth + 1).ToString() + "월' and not 연료='전기' and not 연료='전체' AND 단위 ='kWh'");
                        Gas2 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (mth + 2).ToString() + "월' AND not 연료='전기' and not 연료='전체' AND 단위 ='kWh'");
                        if (Gas1.Length > 0 && Gas2.Length > 0)
                        {
                            for (int i = 0; i < Gas1.Length; i++) //연도별
                            {
                                Quse_gas_mth[mth] += (Convert.ToDouble(Gas1[i][0]) * Convert.ToDouble(Value2[0][0]) / 30 + Convert.ToDouble(Gas2[i][0]) * (30 - Convert.ToDouble(Value2[0][0])) / 30);
                            }
                        }
                        Quse_gas_mth[mth] = Quse_gas_mth[mth] / Gas1.Length;
                    }

                    Gas1 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (12).ToString() + "월' and not 연료='전기' and not 연료='전체' AND 단위 ='kWh'");
                    Gas2 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (1).ToString() + "월' and not 연료='전기' and not 연료='전체' AND 단위 ='kWh'");
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
                        string[][] Gas = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "월 ='" + (mth + 1).ToString() + "월' and not 연료='전기' and not 연료='전체' AND 단위 ='kWh'");
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
                Qf_gas_tot1[mth] = Qhf_gas[mth] + Qcf_gas[mth] + Qwf_gas[mth];
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

                if (double.IsNaN(Qbase_elec[mth]) || Qbase_elec[mth] < 0)
                {
                    Qbase_elec[mth] = 0;
                }
                else
                {
                    Qbase_elec[mth] = Qbase_elec[mth];
                }
            }
            double Qbase_avg = Qbase_elec.Average();
            if (Qbase_elec[0] < Qbase_elec[7])
            {
                for(int mth = 0;mth < 12; mth++)
                {
                    Qbase_elec[mth] = Qbase_avg;
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

                if (double.IsNaN(Qbase_gas[mth]) || Qbase_gas[mth] < 0)
                {
                    Qbase_gas[mth] = 0;
                }
                else if (Qhf_gas[mth] <= 0)
                {
                    Qbase_gas[mth] = 0;
                }
                else
                {
                    Qbase_gas[mth] = Qbase_gas[mth];
                }
            }
            double Qbase_avg = Qbase_gas.Average();
            if (Qbase_gas[0] < Qbase_gas[7])
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    Qbase_gas[mth] = Qbase_avg;
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
