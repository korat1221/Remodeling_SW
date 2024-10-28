using Eagle._Components.Public;
using main.subcontents.ConstructionBlind;
using main.subcontents.ConstructionFloor;
using System;
using System.Collections;
using System.Security.AccessControl;
using System.Security.Policy;
using static System.Windows.Forms.MonthCalendar;


namespace main
{

    internal class Cal_RESystem
    {
        public string Num;
        #region 태양광시스템
        public double PVPpk_kW; //태양광 최대출력 
        public double[] Qf_elec = new double[12]; //월별 전기소요량 
        public double[] PVEelpvoutm_kWh = new double[12]; //월별 전기생산량 
        public string PVType; //계통연계유형 

        private string PVBatteryNumber, BatteryType;
        private double Cnenm; //배터리 정격 용량 
        private double ηDoD, ηBatt; //배터리 최대 방전 깊이, 배터리 시스템효율
        private double[] γQ = new double[12];//배터리 규격에 대한 지수(소요량 대비 최대성능 계수)
        private double Ceff; //배터리 용량(배터리 타입에 따른 방전 깊이 고려) 
        private double[] CQ = new double[12];//배터리 규격에 대한 지수 (소요량 대비 배터리 용량 계수)
        public double[] fmatch = new double[12];//매칭계수
        private double[] fBatt = new double[12]; //배터리 수정계수 
        public double[] Qbatt_loss = new double[12];// 배터리 손실

        public double[] Qf_nutz_linked = new double[12];//계통연계형 월별 태양광사용량 
        public double[] Qf_nutz_nonlinked = new double[12]; //독립형 월별 태양광사용량 
        public double[] Qf_nutz_PV = new double[12]; //최종 월별 태양광사용량 
        #endregion
        public Cal_RESystem(string Num) { this.Num = Num; }

        public void Load_PVdata()
        {
            string[][] Value;
            for (int mth = 0; mth < 12; mth++)
            {
                Value = Program.DB.getValue(DB.type.ProjDB, "PV_Result", "최대성능,전기생산량", "번호='" + Num + "' And 월 ='" + (mth + 1).ToString() + "월'");
                if (Value.Length > 0)
                {
                    PVPpk_kW = Convert.ToDouble(Value[0][0]);
                    PVEelpvoutm_kWh[mth] = Convert.ToDouble(Value[0][1]);
                }
            }
            Value = Program.DB.getValue(DB.type.ProjDB, "PV_Form", "계통유형", "번호='" + Num + "'");
            if (Value.Length > 0)
            {
                PVType = Value[0][0];
            }
            String[][] Battery = Program.DB.getValue(DB.type.ProjDB, "PV_Form", "배터리번호,배터리용량", "번호='" + Num + "'");
            if (Battery.Length > 0)
            {
                PVBatteryNumber = Battery[0][0];
                Cnenm = Convert.ToDouble(Battery[0][1]);
            }

            Battery = Program.DB.getValue(DB.type.ProjDB, "User_PVBattery", "배터리타입", "번호 ='" + PVBatteryNumber + "'");
            if (Battery.Length > 0)
            {
                BatteryType = Battery[0][0];
            }
            Battery = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광배터리계수", "최대방전깊이,시스템효율", "배터리타입 ='" + BatteryType + "'");
            if (Battery.Length > 0)
            {
                ηDoD = Convert.ToDouble(Battery[0][0]);
                ηBatt = Convert.ToDouble(Battery[0][1]);
            }

        }

        public void Cal_Qf_elec()
        {
            string[][] Final;
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "PV_Result", "최대성능", "월 ='1월'");
            double PVPpk_kW_total = 0;
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    PVPpk_kW_total += Convert.ToDouble(Value[n][0]);
                }
            }
            for (int mth = 0; mth < 12; mth++)
            {
                Final = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "연료='전기' And 월 ='" + (mth + 1).ToString() + "월'");
                if (Final.Length > 0)
                {
                    Qf_elec[mth] = Convert.ToDouble(Final[0][0]) * PVPpk_kW / PVPpk_kW_total;
                }
            }

        }

        public void Cal_Battery()
        {
            Ceff = Cnenm * ηDoD;
            for (int mth = 0; mth < 12; mth++)
            {
                CQ[mth] = Ceff / Qf_elec[mth] * 100;
                γQ[mth] = PVPpk_kW / Qf_elec[mth] * 100;
                fBatt[mth] = Math.Max(1, (0.2 * Math.Log(γQ[mth], Math.E) + 1.85) * Math.Pow(CQ[mth], (0.1 * Math.Log(γQ[mth], Math.E) + 0.25)));
            }
        }

        public void Cal_fmatch()
        {
            double[] x = new double[12];
            for (int mth = 0; mth < 12; mth++)
            {
                x[mth] = PVEelpvoutm_kWh[mth] / Qf_elec[mth];
                fmatch[mth] = (x[mth] + 1 / x[mth] - 1) / (x[mth] + 1 / x[mth]);
            }
        }

        public void Cal_Qf_pv()
        {
            for (int mth = 0; mth < 12; mth++)
            {
                Qf_nutz_linked[mth] = fmatch[mth] * PVEelpvoutm_kWh[mth];
                Qbatt_loss[mth] = Qf_nutz_linked[mth] * (1 - ηBatt) * (fBatt[mth] - 1);
                Qf_nutz_nonlinked[mth] = Math.Max(Qf_nutz_linked[mth], Math.Min(PVEelpvoutm_kWh[mth], Qf_nutz_linked[mth] * fBatt[mth]) - Qbatt_loss[mth]);
                if (PVType == "독립형")
                {
                    Qf_nutz_PV[mth] = Qf_nutz_nonlinked[mth];
                }
                else //계통연계형 
                {
                    Qf_nutz_PV[mth] = Qf_nutz_linked[mth];
                }
            }
        }


        #region//연료전지

        public double[] Qf_fc = new double[12], Qf_fc_ele = new double[12], Qf_fc_heat = new double[12], Qoutg = new double[12];
        private double[] UseHour = new double[12], pth_gen_out = new double[12], pele_gen_out = new double[12]; //이용시간과 열출력
        private double[] cal_month = new double[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        string _Num, _ProjectNum; // 연료전지시스템번호, 프로젝트 번호
        double FCtime, FCday, heatpower, elepower;
        string FCName, FCNumnonsplit, FCType, FCWsystemnonsplit, FCHsystemnonsplit, FCnumbernonsplit;
        List<string> FCW = new List<string>(), FCH = new List<string>(), FCNum = new List<string>(), FCNumber=new List<string>();

        public void Cal_FC(string _number, string _projectnumber) //연료전지 시스템번호, 프로젝트 번호
        {
            //월사용시간 UseHour
            //전기 및 열 출력과 시간
            FCreset();
            _Num = _number;
            _ProjectNum = _projectnumber;
            string[][] FCvalue = Program.DB.getValue(DB.type.ProjDB, "FuelCell_Form", "명칭,연료전지,생산유형,사용시간,주이용일,설치대수,급탕설비,난방설비", "번호 ='" + _number + "' and 프로젝트유형 = '" + _projectnumber + "'");
            FCName = FCvalue[0][0].ToString();
            FCNumnonsplit = FCvalue[0][1].ToString();
            FCNum = CalSplit(FCNumnonsplit);
            
            FCType = FCvalue[0][2].ToString();
            FCtime = Convert.ToDouble(FCvalue[0][3]);
            FCday = Convert.ToDouble(FCvalue[0][4]);
            FCnumbernonsplit = FCvalue[0][5].ToString();
            FCNumber = CalSplit(FCnumbernonsplit);

            FCWsystemnonsplit = FCvalue[0][6].ToString();
            FCHsystemnonsplit = FCvalue[0][7].ToString();

            for (int i = 0; 1 < 12; i++)
            {
                UseHour[i] = FCtime * cal_month[i] * FCday / 7; // h/mth
            }
            
            FCW = CalSplit(FCWsystemnonsplit);
            FCH = CalSplit(FCHsystemnonsplit);
            Cal_FC_Heatoutg();
            Cal_Qf_fc_heat(); //열생산량
            Cal_Qf_fc_elec(); //전기생산량
            Cal_Qf_fc(); //연료소비량
        }

        private List<string> CalSplit(string nonSplit)
        {
            List<string> type = new List<string>();
            type.Clear();
            if (nonSplit != null)
            {
                string[] token = nonSplit.Split('+');
                foreach (string item in token)
                {
                    string _item = item.Trim();
                    type.Add(_item);
                }
            }
            return type;
        }

        private void Cal_FC_Heatoutg()
        {
            //급탕설비 열공급량, 난방설비 열공급량
            //열생산량

            double[] HotWater = new double[12], Heating = new double[12];

            if (FCW.Count > 0) //급탕설비 공급량합
            {
                double val = 0;
                foreach (string item in FCW)
                {
                    string[][] FCvalue = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Result", "Qw_outg,월", "번호 = '" + item + "' and 프로젝트유형 = '" + _ProjectNum + "'");
                    for (int i = 0; i < 12; i++)
                    {
                        val = Convert.ToDouble(FCvalue[0][i]);
                        if (val <= 0)
                        {
                            val = 0;
                        }
                        HotWater[i] += val;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    HotWater[i] = 0;
                }
            }

            if (FCH.Count > 0) //난방설비 공급량합
            {
                double val = 0;
                foreach (string item in FCH)
                {
                    string[][] FCvalue = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Result", "Qh_outg,월", "번호 = '" + item + "' and 프로젝트유형 = '" + _ProjectNum + "'");
                    for (int i = 0; i < 12; i++)
                    {
                        val = Convert.ToDouble(FCvalue[0][i]);
                        if (val <= 0)
                        {
                            val = 0;
                        }
                        Heating[i] += val;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    Heating[i] = 0;
                }
            }

            for (int k = 0; k < 12; k++) //급탕설비와 난방설비 공급량 합계
            {
                Qoutg[k] = HotWater[k] + Heating[k];
            }
        }

        private void Cal_Qf_fc_heat() //열생산량
        {

            heatpower = 0;
            elepower = 0;
            for(int i = 0; i < FCNum.Count; i++)
            {
                string[][] value = Program.DB.getValue(DB.type.ProjDB, "User_FC", "열출력,전기출력", "번호 = '" + FCNum[i] + "'");
                heatpower += Convert.ToDouble(value[0][0]) * Convert.ToDouble(FCNumber[i]);
                elepower += Convert.ToDouble(value[0][1]) * Convert.ToDouble(FCNumber[i]);
            }

            //string[][] value = Program.DB.getValue(DB.type.ProjDB, "User_FC", "열출력,전기출력", "번호 = '" + FCNum + "'");
            //heatpower = Convert.ToDouble(value[0][0]);
            //elepower = Convert.ToDouble(value[0][1]);
            for(int i = 0;i<12; i++)
            {
                double val = 0;
               
                val = Qoutg[i] / UseHour[i];
                if (heatpower <= val)
                {
                    pth_gen_out[i] = heatpower;
                }
                else
                {
                    pth_gen_out[i] = val;
                }

                Qf_fc_heat[i] = pth_gen_out[i] * UseHour[i];
            }
        }

        public void Cal_Qf_fc_elec() //전기생산량
        {
            //열생산출력 pth_gen_out 하고, 최대 생산출력(heatpower)과 비교함
            for(int i =0;i<12; i++)
            {
                if (pth_gen_out[i] < heatpower)
                {
                    pele_gen_out[i] = elepower / heatpower * pth_gen_out[i];
                }
                else
                {
                    pele_gen_out[i] = heatpower;
                }
                
                Qf_fc_ele[i] = pele_gen_out[i] * UseHour[i];
            }
        }

        public void Cal_Qf_fc() //연료소비량
        {
            for(int i = 0; i < 12; i++)
            {
                Qf_fc[i] = (pth_gen_out[i] + pele_gen_out[i]) * UseHour[i];
            }
        }

        private void FCreset()
        {
            for (int i = 0; i < 12; i++)
            {
                Qf_fc[i] = 0;
                Qf_fc_ele[i] = 0;
                Qf_fc_heat[i] = 0;
                Qoutg[i] = 0;
                UseHour[i] = 0;
                pth_gen_out[i] = 0;
                pele_gen_out[i] = 0;
            }
            _Num = null;
            _ProjectNum = null;
            FCtime = 0;
            FCday = 0;
            heatpower = 0;
            elepower = 0;
            heatpower = 0;
            elepower = 0;
            
            FCName = null;
            FCNumnonsplit = null;
            FCType = null;
            FCWsystemnonsplit = null;
            FCHsystemnonsplit = null;
            FCnumbernonsplit = null;
            FCW.Clear();
            FCH.Clear();
            FCNum.Clear();
            FCNumber.Clear();
        }
        #endregion
    }

}

