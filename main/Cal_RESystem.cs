using main.contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
                ηBatt =Convert.ToDouble(Battery[0][1]);
            }

        }

        public void Cal_Qf_elec()
        {
            string[][] Final;
            string[][]  Value = Program.DB.getValue(DB.type.ProjDB, "PV_Result", "최대성능", "월 ='1월'");
            double PVPpk_kW_total = 0;
            if(Value.Length > 0)
            {
                for(int n=0; n<Value.Length; n++)
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
            for(int mth = 0;mth < 12; mth++)
            {
                CQ[mth] = Ceff / Qf_elec[mth] * 100;
                γQ[mth] = PVPpk_kW / Qf_elec[mth] * 100;
                fBatt[mth] = Math.Max(1, (0.2 * Math.Log(γQ[mth], Math.E) + 1.85) * Math.Pow(CQ[mth] , (0.1 * Math.Log(γQ[mth], Math.E) + 0.25)));
                
            }            
        }

        public void Cal_fmatch()
        {
            double[] x = new double[12];
            for(int mth =0; mth < 12;mth++)
            {
                x[mth] = PVEelpvoutm_kWh[mth] / Qf_elec[mth];
                fmatch[mth] = (x[mth] + 1 / x[mth] - 1) / (x[mth] + 1 / x[mth]);
            }
        }

        public void Cal_Qf_pv()
        {
            for(int mth = 0; mth < 12;mth++)
            {
                Qf_nutz_linked[mth]= fmatch[mth]* PVEelpvoutm_kWh[mth];
                Qbatt_loss[mth] = Qf_nutz_linked[mth] * (1 - ηBatt) * (fBatt[mth] - 1);
                Qf_nutz_nonlinked[mth] = Math.Max(Qf_nutz_linked[mth], Math.Min(PVEelpvoutm_kWh[mth], Qf_nutz_linked[mth] * fBatt[mth]) - Qbatt_loss[mth]);
                if(PVType== "독립형")
                {
                    Qf_nutz_PV[mth] = Qf_nutz_nonlinked[mth]; 
                }
                else //계통연계형 
                {
                    Qf_nutz_PV[mth] = Qf_nutz_linked[mth];
                }
            }
        }

    }  
}
