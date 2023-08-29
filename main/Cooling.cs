using main.subcontents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main
{

    internal class Cooling
    {
        //기후데이터 작성
        public double[] OutdoorTemperature = new double[12], HumidityTemperature = new double[12]; 
        public void climate()
        {
            string[][] OutdoorClimate = Program.DB.getValue(DB.type.BaseDB_HCneed," 기후데이터_온도습도", "온도,상대습도","지역명 ='서울'"); //기후데이터 저장
            for(int i = 0; i < OutdoorTemperature.Length; i++) 
            {
                OutdoorTemperature[i] = Convert.ToDouble(OutdoorClimate[i][0]);
                HumidityTemperature[i] = humidityhemperature(Convert.ToDouble(OutdoorClimate[i][0]), Convert.ToDouble(OutdoorClimate[i][1]));
            }
            
        }
        public enum cooler { 실내기12kW, 공냉식냉동기, 수냉식냉동기, 흡수식냉동기, 흡수식냉온수기, 지열히트펌프 };
        public Cooling(cooler _cooler)
        {
            switch (_cooler)
            {
                case cooler.실내기12kW:
                    실내기12kW aircon = new 실내기12kW();
                    break;
                case cooler.공냉식냉동기:
                   // 공냉식냉동기();
                    break;
                case cooler.수냉식냉동기:
                   // 수냉식냉동기();
                    break;
                case cooler.흡수식냉동기:
                   // 흡수식냉동기();
                    break;
                case cooler.흡수식냉온수기:
                   // 흡수식냉온수기();
                    break;
                case cooler.지열히트펌프:
                   // 지열히트펌프();
                    break;
                default:
                    break;
            }
        }

        public double humidityhemperature(double _temperature, double _relativehumidity) //습구온도 계산
        {
            double _humiditytemperature = -5.809 + 0.058 * _relativehumidity * 100 + 0.697 * _temperature + 0.003 * _relativehumidity * _temperature * 100;
            return _humiditytemperature;
        }


    }
     class 실내기12kW
     {

        public string CoolingSystemName, Control, Location, EnergyMedium;
        public string Economizer, StorageCheck, StoreageType;
        public double CoolingPower, EERprod, Number;
        public double[] QC_f = new double[12], QC_ce = new double[12], QC_d = new double[12], QC_s = new double[12], QC_out = new double[12];
        public double[] WC_f = new double[12], WC_ce = new double[12], WC_d = new double[12];
        public double[] SEER = new double[12], EER = new double[12], fC_PL_k = new double[12], feer_corr = new double[12];
        public double fC_mult, Δθaround, θC_gen_hr_req_in, θC_gen_req_out, Δθcond, Δθevap;
        List<CoolingZone> CoolingZones = new List<CoolingZone>(); //냉방설비존리스트
        CoolingZoneSum _CoolingZoneSum = new CoolingZoneSum();
        List<CoolingZone> hvaccoolings = new List<CoolingZone>(); //공조기설비리스트
        List<CoolingGenerator> coolinggenerators = new List<CoolingGenerator>(); //장비리스트
        List<CoolingSupply> coolingsupplys = new List<CoolingSupply>(); //공급설비리스트
        CoolingGenerator _CoolingGenerator = new CoolingGenerator();

        public 실내기12kW() //
        {
            //냉방설비존 만들기, 공조존 만들기
            string[][] Cool = Program.DB.getValue(DB.type.ProjDB, " 기후데이터_온도습도", "온도,상대습도", "지역명 ='서울'");

        }

        public void CoolingZoneMaker() // 테이블2개(CoolingZone,Zone_HCneed 필요)
        {
           

        }

        public void CoolingSupply()
        {

        }
     }

    class CoolingSupply
    {
        public string CZType, CSType, CSName;//냉방인지공조인지, 
        public double fc_ce_aux, CSNumber;
        public CoolingSupply(string _CZType, string _CSType)
        {
            CZType = _CZType;
            CSType = _CSType;

        }
    }

    //class CoolingZone //냉방설비존/공조설비존 만들기
    //{
    //    public string ZoneName, CZType, CoolingSystemType, CoolingSystemNumber, CoolingSystemName;
    //    public double tC_op, QC_max; //
    //    public double[] QC_nd_zt_j = new double[12], dwd = new double[12], θi_c = new double[12]; //getvalue로 값을 가져옴
    //    public double[] QC_rate = new double[12]; //존결정이 완료 후 계산됨
    //    public CoolingZone()
    //    {

    //    }
    //    public double SumQC_nd_zt_j(double[] 에너지요구량)
    //    {
    //       double sum = 0;
    //       for(int i = 0; i < 12; i++) 
    //       {
    //           sum += QC_nd_zt_j[i];
    //       }
    //       return sum;
    //    }
    //    public double Averθi_c(double[] 실내온도)
    //    {
    //       double sum = 0;
    //       for (int i = 0; i < 12; i++)
    //       {
    //           sum += θi_c[i];
    //       }
    //       double aver = sum / 12;
    //       return aver;
    //    }         
    //}

    class CoolingZoneSum //냉방설비존합/계산결과값 만들기
    {
        public double top_C_mth, QC_max;//일일평균냉방시간, 최대난방부하(계산값)
        public double[] QC_nd_zt_j = new double[12], dwd = new double[12], θi_c = new double[12], BC_i = new double[12];
        public string SCZoneType, ZoneType;
        public double nc_ce_sens, nc_ce, nc_d, nc_s;
        public double[] Qc_ce_ls_i, Qc_d_ls_i, Qc_s, ls, i, Qc_out_i;
        public double Qc_P_part, BC_grenz;
        public double[] tmth_wd, BC, tC_op, fC_PL, k, PLmainRate, PLmaxRate, fC_PL_k_min, fC_PL_k_max;
        // public CoolingZoneSum() // 계산에서 진행할 것 getvalue로 값을 가져옴
        //{
        //    foreach (CoolingZone z in zc)
        //    {
        //        string[][] zone_need_wd = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed", "Qcb_we_mth,dwd_mth,theta_i,", "번호='" + z.ZoneName + "' AND 난방_냉방 = '" + "냉방" + "'  AND  비이용일_이용일 =  '" + "이용일" + "'");//이용일기준요구량
        //        string[][] zone_need_we = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed", "Qcb_wd_mth", "번호='" + z.ZoneName + "' AND 난방_냉방 = '" + "냉방" + "' AND 비이용일_이용일 =  '" + "비이용일" + "'");//비이용일기준요구량

        //        //존한개씩 값을 가져옴, 요구량, 부하, 실내온도, 일수, 가동시간
        //        double[] zc = new double[12];
        //        double[] dwd_i = new double[12];
        //        double[] t_i = new double[12];

        //        for (int j = 0; j < 12; j++)
        //        {
        //            zc[j] = Convert.ToDouble(zone_need_we[j][0]) + Convert.ToDouble(zone_need_wd[j][0]);
        //            dwd_i[j] = Convert.ToDouble(zone_need_wd[j][1]);
        //            t_i[j] = Convert.ToDouble(zone_need_wd[j][2]);
        //        }
        //        //각 존들의 값을 합함
        //    }




        //    top_C_mth += z.tC_op;
        //    this.QC_max += z.QC_max;

        //    for (int i = 0; i < 12; i++)
        //    {
        //        this.QC_nd_zt_j[i] += z.QC_nd_zt_j[i];
        //        this.dwd[i] += z.dwd[i] * z.QC_nd_zt_j[i];
        //        this.θi_c[i] += z.θi_c[i] * z.QC_nd_zt_j[i];
        //    }

        //}
        //this.top_C_mth = top_C_mth / zc.Count;

        //for(int j= 0; j < 12;)
        //{
        //    if (this.QC_nd_zt_j[j] == 0)
        //    {
        //        this.dwd[j] = 0;
        //        this.θi_c[j] = 0;
        //    }
        //    else
        //    {
        //        this.dwd[j] = this.dwd[j] * this.QC_nd_zt_j[j] / this.QC_nd_zt_j[j];
        //        this.θi_c[j] = this.θi_c[j];

        //    }
        //    this.dwd[j] += this.dwd[j] * this.QC_nd_zt_j[j] / this.QC_nd_zt_j[j];
        //}

    }

    class CoolingGenerator //private는 _로 시작할것
    {
        public string unit, EnergyMedium, Control, Location, Economizer;
        
        public double number, CoolingPower, HeatingPower, CoolingPowerConsumtion, HeatingPowerConsumtion, EER, COP;
    }
}
