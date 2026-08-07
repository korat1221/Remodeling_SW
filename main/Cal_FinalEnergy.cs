using main.contents;
using System;
using System.Collections;
using System.Collections.Generic;

namespace main
{
    internal class Final
    {
        public string Carrier_h, Carrier_w, Carrier_c;
        public double[] Qhf_gas = new double[12], Qhf_elec = new double[12];
        public double[] Qcf_gas = new double[12], Qcf_elec = new double[12];
        public double[] Qwf_gas = new double[12], Qwf_elec = new double[12];
        public double[] Qlf_gas = new double[12], Qlf_elec = new double[12];
        public double[] Qvf_gas = new double[12], Qvf_elec = new double[12];
        public double[] Qbase_gas = new double[12], Qbase_elec = new double[12];
        public double[] Qf_gas_tot1 = new double[12], Qf_elec_tot1 = new double[12];
        public double[] Qf_gas_tot_mth = new double[12], Qf_elec_tot_mth = new double[12];
        public double Qf_gas_tot_a, Qf_elec_tot_a; 
        public double[] Quse_gas_mth = new double[12], Quse_elec_mth = new double[12];
        public double Quse_gas_a, Quse_elec_a;
        public double[] Qreg_elec_tot = new double[12];//신재생

        public double[] Qreg_elec_prod_h = new double[12]; //용도별 전기생산량
        public double[] Qreg_elec_prod_c = new double[12];
        public double[] Qreg_elec_prod_w = new double[12];
        public double[] Qreg_elec_prod_l = new double[12];
        public double[] Qreg_elec_prod_v = new double[12];

        public double[] Qreg_gas_cons_h = new double[12]; //용도별 연료소비량
        public double[] Qreg_gas_cons_c = new double[12];
        public double[] Qreg_gas_cons_w = new double[12];
        public double[] Qreg_gas_cons_l = new double[12];
        public double[] Qreg_gas_cons_v = new double[12];

        string[][] 프로젝트유형 = null;
        public Final(string ProjNum)
        {
            프로젝트유형 = Program.DB.getValue(ProjNum, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            #region 조명
            string[][] Num = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호");
            if (Num.Length > 0)
            {
                int i = -1;
                while (++i < Num.Length)
                {
                    ZoneLight zoneLight1 = Program.CALC.getZoneLight(Num[i][0]);
                    for (int mth = 0; mth < 12; mth++)
                    {
                        Qlf_elec[mth] += zoneLight1.Zone_Final_kWh[mth];
                    }
                }
            }
            #endregion 

            //에너지사용량
            string[][] Value1 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "사용시작일", "연료='전기'");
            string[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "사용시작일", "not 연료='전기' and not 연료='전체'");
            double[] dmth = new double[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (Value1.Length > 0)
            {
                if (Program.UTIL.ToDoubleOrZero(Value1[0][0]) > 1)
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
                                Quse_elec_mth[mth] += (Program.UTIL.ToDoubleOrZero(Elec1[i][0]) * Program.UTIL.ToDoubleOrZero(Value1[0][0]) / dmth[mth] + Program.UTIL.ToDoubleOrZero(Elec2[i][0]) * (dmth[mth] - Program.UTIL.ToDoubleOrZero(Value1[0][0])) / dmth[mth]);
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
                            Quse_elec_mth[11] += (Program.UTIL.ToDoubleOrZero(Elec1[i][0]) * Program.UTIL.ToDoubleOrZero(Value1[0][0]) / dmth[11] + Program.UTIL.ToDoubleOrZero(Elec2[i][0]) * (dmth[11] - Program.UTIL.ToDoubleOrZero(Value1[0][0])) / dmth[11]);
                        }
                    }
                    Quse_elec_mth[11] = Quse_elec_mth[11] / Elec1.Length;

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
                                Quse_elec_mth[mth] += Program.UTIL.ToDoubleOrZero(Elec[i][0]);
                            }
                        }
                        Quse_elec_mth[mth] = Quse_elec_mth[mth] / Elec.Length;
                    }

                }
            }
            if (Value2.Length > 0)
            {
                if (Program.UTIL.ToDoubleOrZero(Value2[0][0]) > 1)
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
                                Quse_gas_mth[mth] += (Program.UTIL.ToDoubleOrZero(Gas1[i][0]) * Program.UTIL.ToDoubleOrZero(Value2[0][0]) / dmth[mth] + Program.UTIL.ToDoubleOrZero(Gas2[i][0]) * (dmth[mth] - Program.UTIL.ToDoubleOrZero(Value2[0][0])) / dmth[mth]);
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
                            Quse_gas_mth[11] += (Program.UTIL.ToDoubleOrZero(Gas1[i][0]) * Program.UTIL.ToDoubleOrZero(Value2[0][0]) / dmth[11] + Program.UTIL.ToDoubleOrZero(Gas2[i][0]) * (dmth[11] - Program.UTIL.ToDoubleOrZero(Value2[0][0])) / dmth[11]);
                        }
                    }
                    Quse_gas_mth[11] = Quse_gas_mth[11] / Gas1.Length;
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
                                Quse_gas_mth[mth] += Program.UTIL.ToDoubleOrZero(Gas[i][0]);
                            }
                        }
                        Quse_gas_mth[mth] = Quse_gas_mth[mth] / Gas.Length;
                    }

                }
            }
           
        }
        public void Load_Heating_Final(string ProjNum)
        {
            string[][] HeatingNum = Program.DB.getValue(ProjNum, "HeatingSystem_Form", "번호");
            if (HeatingNum.Length > 0)
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
        }
        public void Load_DHW_Final(string ProjNum)
        {
            string[][] DHWNum = Program.DB.getValue(ProjNum, "DHWSystem_Form", "번호");
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
        }
        public void Load_Cooling_Final(string ProjNum)
        {
            string[][] Num = Program.DB.getValue(ProjNum, "CoolingSystem_Form", "번호");
            if (Num.Length > 0)
            {
                int i = -1;
                while (++i < Num.Length)
                {
                    Cal_Cooling Cooling1 = Program.CALC.getCooling(Num[i][0]);
                    for (int mth = 0; mth < 12; mth++)
                    {
                        if (Cooling1.Carrier == "전기")
                        {
                            Qcf_elec[mth] += (Cooling1.QC_f[mth] + Cooling1.W[mth]);
                        }
                        else
                        {
                            Carrier_c = Cooling1.Carrier;
                            Qcf_elec[mth] += (Cooling1.W[mth]);
                            Qcf_gas[mth] += Cooling1.QC_f[mth];
                        }
                    }
                }
            }
        }
        public void Load_AHU_Final(string ProjNum)
        {
            string[][] Num = Program.DB.getValue(ProjNum, "AHUSystem_Form", "번호");
            if (Num.Length > 0)
            {
                int i = -1;
                while (++i < Num.Length)
                {
                    AHU AHU1 = Program.CALC.getAHU(Num[i][0]);
                    for (int mth = 0; mth < 12; mth++)
                    {
                        if (AHU1 != null)
                        { Qvf_elec[mth] += AHU1.W_tot[mth]; }
                    }
                }
            }
        }
        public void Calc_Qtot(string ProjNum)
        {
            for (int mth = 0; mth < 12; mth++)
            {
                Qf_elec_tot1[mth] = Qhf_elec[mth] + Qcf_elec[mth] + Qwf_elec[mth] + Qlf_elec[mth] + Qvf_elec[mth];
                Qf_gas_tot1[mth] = Qhf_gas[mth] + Qcf_gas[mth] + Qwf_gas[mth];
            }
        }

        public void reg_분배(string ProjNum)
        {
             ArrayList arr_renum = new ArrayList();
            foreach (var system in CALC.RESystems.Values)
            {
                if (!arr_renum.Contains(system.RE_Num))
                {
                    arr_renum.Add(system.RE_Num);
                }
            }

            foreach (var system in CALC.RESystems.Values)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    for(int i =0; i< arr_renum.Count; i++)
                    {
                        string renum = "RE0" + (i + 1);
                        if (system != null && system.RE_Num == renum && system.RE_Production_Consumption == "생산" && system.RE_Production_Type == "전기")
                        {
                            // 각 변수에 대한 계산
                            double h = system.RE_TotalE[mth] * Qhf_elec[mth] / Qf_elec_tot1[mth]; // 난방 계산
                            double c = system.RE_TotalE[mth] * Qcf_elec[mth] / Qf_elec_tot1[mth]; // 냉방 계산
                            double w = system.RE_TotalE[mth] * Qwf_elec[mth] / Qf_elec_tot1[mth]; // 급탕 계산
                            double l = system.RE_TotalE[mth] * Qlf_elec[mth] / Qf_elec_tot1[mth]; // 조명 계산
                            double v = system.RE_TotalE[mth] * Qvf_elec[mth] / Qf_elec_tot1[mth]; // 공조 계산

                            h = double.IsNaN(h) ? 0 : h;
                            c = double.IsNaN(c) ? 0 : c;
                            w = double.IsNaN(w) ? 0 : w;
                            l = double.IsNaN(l) ? 0 : l;
                            v = double.IsNaN(v) ? 0 : v;


                            // 해당 system 객체의 속성에 값을 할당
                            system.RE_HeatingE[mth] = h;  // 난방
                            system.RE_CoolingE[mth] = c;  // 냉방
                            system.RE_DHWE[mth] = w;  // 급탕
                            system.RE_LightingE[mth] = l;  // 조명
                            system.RE_AHUE[mth] = v;  // 공조

                            Qreg_elec_prod_h[mth] += h;
                            Qreg_elec_prod_c[mth] += c;
                            Qreg_elec_prod_w[mth] += w;
                            Qreg_elec_prod_l[mth] += l;
                            Qreg_elec_prod_v[mth] += v;
                        }
                    }
                   
                }   
            }


            foreach (var system in CALC.RESystems.Values)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    for (int i = 0; i < arr_renum.Count; i++)
                    {
                        string renum = "RE0" + (i + 1);
                        if (system != null && system.RE_Num == renum && system.RE_Production_Consumption == "소비" && system.RE_Consumption_Carrier == "가스")
                        {
                            // 각 변수에 대한 계산
                            double h = system.RE_TotalE[mth] * Qhf_elec[mth] / Qf_elec_tot1[mth]; // 난방 계산
                            double c = system.RE_TotalE[mth] * Qcf_elec[mth] / Qf_elec_tot1[mth]; // 냉방 계산
                            double w = system.RE_TotalE[mth] * Qwf_elec[mth] / Qf_elec_tot1[mth]; // 급탕 계산
                            double l = system.RE_TotalE[mth] * Qlf_elec[mth] / Qf_elec_tot1[mth]; // 조명 계산
                            double v = system.RE_TotalE[mth] * Qvf_elec[mth] / Qf_elec_tot1[mth]; // 공조 계산

                            h = double.IsNaN(h) ? 0 : h;
                            c = double.IsNaN(c) ? 0 : c;
                            w = double.IsNaN(w) ? 0 : w;
                            l = double.IsNaN(l) ? 0 : l;
                            v = double.IsNaN(v) ? 0 : v;

                            // 해당 system 객체의 속성에 값을 할당
                            system.RE_HeatingE[mth] = h;  // 난방
                            system.RE_CoolingE[mth] = c;  // 냉방
                            system.RE_DHWE[mth] = w;  // 급탕
                            system.RE_LightingE[mth] = l;  // 조명
                            system.RE_AHUE[mth] = v;  // 공조

                            Qreg_gas_cons_h[mth] += h;
                            Qreg_gas_cons_c[mth] += c;
                            Qreg_gas_cons_w[mth] += w;
                            Qreg_gas_cons_l[mth] += l;
                            Qreg_gas_cons_v[mth] += v;
                        }
                    }
                }
            }
        }

        public void reg_빼기(string ProjNum)
        {
            for (int mth = 0; mth < 12; mth++)
            {
                
                    Qhf_elec[mth] = Math.Max(0, Qhf_elec[mth] - Qreg_elec_prod_h[mth]);
                    Qcf_elec[mth] = Math.Max(0, Qcf_elec[mth] - Qreg_elec_prod_c[mth]);
                    Qwf_elec[mth] = Math.Max(0, Qwf_elec[mth] - Qreg_elec_prod_w[mth]);
                    Qlf_elec[mth] = Math.Max(0, Qlf_elec[mth] - Qreg_elec_prod_l[mth]);
                    Qvf_elec[mth] = Math.Max(0, Qvf_elec[mth] - Qreg_elec_prod_v[mth]);
                
                Qf_elec_tot_mth[mth] = Qhf_elec[mth] + Qcf_elec[mth] + Qwf_elec[mth] + Qlf_elec[mth] + Qvf_elec[mth] ;


            }
            for (int mth = 0; mth < 12; mth++)
            {
                Qhf_gas[mth] = Qhf_gas[mth] + Qreg_gas_cons_h[mth];
                Qcf_gas[mth] = Qcf_gas[mth] + Qreg_gas_cons_c[mth];
                Qwf_gas[mth] = Qwf_gas[mth] + Qreg_gas_cons_w[mth];
                Qlf_gas[mth] = Qlf_gas[mth] + Qreg_gas_cons_l[mth];
                Qvf_gas[mth] = Qvf_gas[mth] + Qreg_gas_cons_v[mth];
                Qf_gas_tot_mth[mth] = Qhf_gas[mth] + Qcf_gas[mth] + Qwf_gas[mth] + Qlf_gas[mth] + Qvf_gas[mth] ;
            }
        }

        public void Calc_Qbase_elec(string ProjNum)
          {
            
            double beta, alpha;
            double[] x = new double[12];
            double[] y = new double[12];

            double[] Error_elec = new double[12];
            for (int mth = 0; mth < 12; mth++)
            {
                Error_elec[mth] = Quse_elec_mth[mth] - Qf_elec_tot1[mth];
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
                Qf_elec_tot_mth[mth] = Qhf_elec[mth] + Qcf_elec[mth] + Qwf_elec[mth] + Qlf_elec[mth] + Qvf_elec[mth] + Qbase_elec[mth];
            }
        }


        public void Calc_Qbase_gas(string ProjNum)
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
                Qf_gas_tot_mth[mth] = Qhf_gas[mth] + Qcf_gas[mth] + Qwf_gas[mth] + Qlf_gas[mth] + Qvf_gas[mth] + Qbase_gas[mth];
            }
        }

    }
}
