using main;
using main.subcontents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    internal class CALC
    {
        /////////////////////////////////////////////////////////////////////////////////////
        // calculation functions start

        private static string zoneNum = "";

        public void init()
        {

            _calculations["존 계산"] = new Func<bool>(ZoneCalc);

            _calculations["존 계산 결과 저장"] = new Func<bool>(ZoneCalcSave);
        }

        private static bool ZoneCalc()
        {
            int i = -1;
            string[][] zones = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호");

            while (++i < zones.Length)
            {
                  Zone zone1 = new Zone("1F_Zone001");
                //   Zone zone1 = new Zone(zones[i][0]);

                Zones[zones[i][0]] = zone1;

                zone1.ZoneHT();
                zone1.ZoneHV();
                zone1.Zonetao();
                zone1.Zonethetai();
                zone1.ZoneQT();
                zone1.ZoneQV();
                zone1.ZoneQSop(zones[i][0]);
                zone1.ZoneQStr(zones[i][0]);
                zone1.ZoneQ_DHU();
                zone1.ZoneQI();
                zone1.Zoneeta();
                zone1.ZoneQb();

                //[난방/냉방,비이용일/이용일,mth] = [h/c,we/wd,mth]=[0/1,0/1,12]
                String HC, WEWD, MTH;
                for (int hc = 0; hc <= 1; hc++) 
                {
                    if( hc == 0 )
                    {
                        HC = "난방";
                    }
                    else
                    {
                        HC = "냉방";
                    }

                    for (int wewd = 0; wewd <= 1; wewd++)
                    {
                        if (wewd == 0)
                        {
                            WEWD = "비이용일";
                        }
                        else
                        {
                            WEWD = "이용일";
                        }

                        for (int mth = 0; mth <= 11; mth++)
                        {
                            MTH = (mth + 1).ToString() + "월";

                            Program.DB.setValue(DB.type.ProjDB, "Zone_HCneed", "번호,이름," +
                                 "난방_냉방,비이용일_이용일,월," +
                                 "HT_tot,HT_Wall,HT_Roof,HT_Floor,HT_GWall,HT_Door,HT_Win,HT_CW," +
                                 "HT_Di_Wall,HT_Indi_Wall,HT_Di_Roof,HT_Indi_Roof,HT_Di_Win,HT_Indi_Win,HT_Di_Door,HT_Indi_Door," +
                                 "HT_TB_tot,HT_TB_Wall,HT_TB_Roof,HT_TB_Floor,HT_TB_Gwall,HT_TB_Win,HT_TB_Door,HT_TB_CW," +
                                 "HV_tot,HV_inf,HV_win,HV_z,HV_mech," +
                                 "H_tot,tao,dwe_mth,dwd_mth,theta_i,theta_e," +
                                 "QTsink_tot,QTsink_Wall,QTsink_Roof,QTsink_Floor,QTsink_GWall,QTsink_Door,QTsink_Win,QTsink_CW," +
                                 "QTsource_tot,QTsource_Wall,QTsource_Roof,QTsource_Floor,QTsource_GWall,QTsource_Door,QTsource_Win,QTsource_CW," +
                                 "QSopsink_tot,QSopsource_tot,QStr_tot," +
                                 "QSopsink_Wall,QSopsink_Roof,QSopsink_Door,QSopsink_CW_p," +
                                 "QSopsource_Wall,QSopsource_Roof,QSopsource_Door,QSopsource_CW_p," +
                                 "QStr_Win,QStr_CW," +
                                 "QVsink_tot,QV_inf_sink,QV_win_sink,QV_z_sink,QV_mech_sink," +
                                 "QVsource_tot,QV_inf_source,QV_win_source,QV_z_source,QV_mech_source," +
                                 "QI_tot,QI_L," +
                                 "QI_P,QI_fac," +
                                 "Qsink,Qsource,gamma,a,eta,dQc_b,dQc_sink," +
                                 "Qhb_we_day,Qhb_wd_day,Qcb_we_day,Qcb_wd_day," +
                                 "Qhb_mth,Qcb_mth,Qhb_we_mth,Qhb_wd_mth,Qcb_we_mth,Qcb_wd_mth," +
                                 "Qhb_a, Qcb_a, Qhb_we_a, Qhb_wd_a, Qcb_we_a, Qcb_wd_a",
                                  "'" + zones[i][0] + "','" + zone1.zoneName + "','" +
                                  HC + "','" + WEWD + "','" + MTH + "','" +
                                  zone1.Zone_HT_tot.ToString() + "','" + zone1.Zone_HT_Wall.ToString()+ "','" + zone1.Zone_HT_Roof.ToString()+ "','" + zone1.Zone_HT_Floor.ToString()+ "','" + zone1.Zone_HT_GWall.ToString()+ "','" + zone1.Zone_HT_Door.ToString()+ "','" + zone1.Zone_HT_Win.ToString() + "','" +zone1.Zone_HT_CW.ToString() + "','" +
                                  zone1.Zone_HT_Di_Wall.ToString() + "','" + zone1.Zone_HT_Indi_Wall.ToString() + "','" + zone1.Zone_HT_Di_Roof.ToString() + "','" + zone1.Zone_HT_Indi_Roof.ToString() + "','" + zone1.Zone_HT_Di_Win.ToString() + "','" + zone1.Zone_HT_Indi_Win.ToString() + "','" + zone1.Zone_HT_Di_Door.ToString() + "','" + zone1.Zone_HT_Indi_Door.ToString() + "','" +
                                  zone1.Zone_HT_TB_tot.ToString() + "','" + zone1.Zone_HT_TB_Wall.ToString() + "','" + zone1.Zone_HT_TB_Roof.ToString() + "','" + zone1.Zone_HT_TB_Floor.ToString() + "','" + zone1.Zone_HT_TB_GWall.ToString() + "','" + zone1.Zone_HT_TB_Win.ToString() + "','" + zone1.Zone_HT_TB_Door.ToString() + "','" + zone1.Zone_HT_TB_CW.ToString() + "','" +
                                  zone1.Zone_HV_tot[wewd].ToString() + "','" + zone1.Zone_HV_inf[wewd].ToString() + "','" + zone1.Zone_HV_win[wewd].ToString() + "','" + zone1.Zone_HV_z[wewd].ToString() + "','" + zone1.Zone_HV_mech[wewd].ToString() + "','" +
                                  zone1.Zone_H_tot[wewd].ToString() + "','" + zone1.tao[wewd].ToString() + "','" + zone1.dwe_mth[mth].ToString() + "','" + zone1.dwd_mth[mth].ToString() + "','" + zone1.theta_i[hc,wewd,mth].ToString() + "','" + zone1.theta_e[mth].ToString() + "','" +
                                  zone1.QTsink_tot[hc, wewd, mth].ToString() + "','" + zone1.QTsink_Wall[hc, wewd, mth].ToString() + "','" + zone1.QTsink_Roof[hc, wewd, mth].ToString() + "','" + zone1.QTsink_Floor[hc, wewd, mth].ToString() + "','" + zone1.QTsink_GWall[hc, wewd, mth].ToString() + "','" + zone1.QTsink_Door[hc, wewd, mth].ToString() + "','" + zone1.QTsink_Win[hc, wewd, mth].ToString() + "','" + zone1.QTsink_CW[hc, wewd, mth].ToString() + "','" +
                                  zone1.QTsource_tot[hc, wewd, mth].ToString() + "','" + zone1.QTsource_Wall[hc, wewd, mth].ToString() + "','" + zone1.QTsource_Roof[hc, wewd, mth].ToString() + "','" + zone1.QTsource_Floor[hc, wewd, mth].ToString() + "','" + zone1.QTsource_GWall[hc, wewd, mth].ToString() + "','" + zone1.QTsource_Door[hc, wewd, mth].ToString() + "','" + zone1.QTsource_Win[hc, wewd, mth].ToString() + "','" + zone1.QTsource_CW[hc, wewd, mth].ToString() + "','" +
                                  zone1.QSopsink_tot[hc, wewd, mth].ToString() + "','" + zone1.QSopsource_tot[hc, wewd, mth].ToString() + "','" + zone1.QStr_tot[hc, wewd, mth].ToString() + "','" +
                                  zone1.QSopsink_Wall[mth].ToString() + "','" + zone1.QSopsink_Roof[mth].ToString() + "','" + zone1.QSopsink_Door[mth].ToString() + "','" + zone1.QSopsink_CW_p[mth].ToString() + "','" +
                                  zone1.QSopsource_Wall[mth].ToString() + "','" + zone1.QSopsource_Roof[mth].ToString() + "','" + zone1.QSopsource_Door[mth].ToString() + "','" + zone1.QSopsource_CW_p[mth].ToString() + "','" +
                                  zone1.QStr_Win[wewd, mth].ToString() + "','" + zone1.QStr_CW[wewd, mth].ToString() + "','" +
                                  zone1.QVsink_tot[hc, wewd, mth].ToString() + "','" + zone1.QV_inf_sink[hc, wewd, mth].ToString() + "','" + zone1.QV_win_sink[hc, wewd, mth].ToString() + "','" + zone1.QV_z_sink[hc, wewd, mth].ToString() + "','" + zone1.QV_mech_sink[hc, wewd, mth].ToString() + "','" +
                                  zone1.QVsource_tot[hc, wewd, mth].ToString() + "','" + zone1.QV_inf_source[hc, wewd, mth].ToString() + "','" + zone1.QV_win_source[hc, wewd, mth].ToString() + "','" + zone1.QV_z_source[hc, wewd, mth].ToString() + "','" + zone1.QV_mech_source[hc, wewd, mth].ToString() + "','" +
                                  zone1.QI_tot[hc, wewd, mth].ToString() + "','" + zone1.QI_L[hc, wewd, mth].ToString() + "','" +
                                  zone1.QI_P[wewd].ToString() + "','" + zone1.QI_fac[wewd].ToString() + "','" +
                                  zone1.Qsink[hc, wewd, mth].ToString() + "','" + zone1.Qsource[hc, wewd, mth].ToString() + "','" + zone1.gamma[hc, wewd, mth].ToString() + "','" + zone1.a[hc, wewd, mth].ToString() + "','" + zone1.eta[hc, wewd, mth].ToString() + "','" + zone1.dQc_b[hc, wewd, mth].ToString() + "','" + zone1.dQc_sink[hc, wewd, mth].ToString() + "','" +
                                  zone1.Qhb_we_day[mth].ToString() + "','" + zone1.Qhb_wd_day[mth].ToString() + "','" + zone1.Qcb_we_day[mth].ToString() + "','" + zone1.Qcb_wd_day[mth].ToString() + "','" +
                                  zone1.Qhb_mth[mth].ToString() + "','" + zone1.Qcb_mth[mth].ToString() + "','" + zone1.Qhb_we_mth[mth].ToString() + "','" + zone1.Qhb_wd_mth[mth].ToString() + "','" + zone1.Qcb_we_mth[mth].ToString() + "','" + zone1.Qcb_wd_mth[mth].ToString() + "','" +
                                  zone1.Qhb_a.ToString() + "','" + zone1.Qcb_a.ToString() + "','" + zone1.Qhb_we_a.ToString() + "','" + zone1.Qhb_wd_a.ToString() + "','" + zone1.Qcb_we_a.ToString() + "','" + zone1.Qcb_wd_a.ToString()
                                  + "'", "번호,난방_냉방,비이용일_이용일,월");
                        }
                    }
                }

                ZoneLight zonelight1 = new ZoneLight(zones[i][0]);
                ZoneLights[zones[i][0]] = zonelight1;
                zonelight1.Calc_time(zones[i][0]);                
                zonelight1.Calc_Facade_general();
                zonelight1.Calc_Facade_shade();
                zonelight1.Calc_Facade_FDS();
                zonelight1.Calc_Facade_FD();
                zonelight1.Calc_Roof_general();
                zonelight1.Calc_Roof_FDS();
                zonelight1.Calc_Roof_FD();
                zonelight1.Calc_Sunlight_SCW();
                zonelight1.Calc_Sunlight_Pj_SC();
                zonelight1.Calc_W();

                //월별 조명
                for (int mth = 0; mth <= 11; mth++)
                {
                    //zonename 가져와야할까? 
                    MTH = (mth + 1).ToString() + "월";

                    Program.DB.setValue(DB.type.ProjDB, "Zone_LightResult", "번호,월," +
                        "ITr,IRD,ISh_Ish,ISh_hA,Ish_vA,Ish_In_At,Wi,Ish_GDF,Ish," +
                        "f_τeff_SNA,f_D,f_nearD,f_DCA,f_dclass,f_nearEm_SNA,f_fd_sna,f_fd_sa,f_nearEm_DC,f_fd_c,f_FDS,f_FD," +
                        "as_bs,hs_bs,hg_hw," +
                        "normal_ηR,saw_ηR,r_DSNA,r_DSA,r_dclass," +
                        "r_nearEm_FDS,r_fd_sna,r_fd_sa,r_nearEm_DC,r_fd_c,r_FDS,r_FD," +
                        "Sunlight_SCW,Sunlight_PjSC,Final_W",

                    "'" + zones[i][0] + "','" + MTH + "','" +
                     zonelight1.Zone_ITr.ToString() + "','" + zonelight1.Zone_IRD.ToString() + "','" + zonelight1.Zone_ISh_Ish.ToString() + "','" + zonelight1.Zone_ISh_hA.ToString() + "','" + zonelight1.Zone_ISh_vA.ToString() + "','" + zonelight1.Zone_Ish_In_At.ToString() + "','" + zonelight1.Zone_Wi.ToString() + "','" + zonelight1.Zone_Ish_GDF.ToString() + "','" + zonelight1.Zone_Calc_Ish.ToString() + "','" +
                     zonelight1.Zone_τeff_SNA_j.ToString() + "','" + zonelight1.Zone_D.ToString() + "','" + zonelight1.Zone_nearD.ToString() + "','" + zonelight1.Zone_DCA.ToString() + "','" + zonelight1.dclass + "','" + zonelight1.f_nearEm_SNA.ToString() + "','" + zonelight1.find_fd_sna.ToString() + "','" + zonelight1.find_fd_sa.ToString() + "','" + zonelight1.f_naerEm_DC.ToString() + "','" + zonelight1.find_fd_c.ToString() + "','" + zonelight1.Zone_FDS[mth].ToString() + "','" + zonelight1.Zone_Facade_FD[mth].ToString() + "','" +
                     zonelight1.Zone_as_bs.ToString() + "','" + zonelight1.Zone_hs_bs.ToString() + "','" + zonelight1.Zone_hg_hw.ToString() + "','" +
                     zonelight1.find_normal_ηR.ToString() + "','" + zonelight1.find_saw_ηR.ToString() + "','" + zonelight1.Zone_Roof_DSNA.ToString() + "','" + zonelight1.Zone_Roof_DSA.ToString() + "','" + zonelight1.roof_dclass + "','" +
                     zonelight1.r_nearEm_FDS.ToString() + "','" + zonelight1.find_roof_fd_sna.ToString() + "','" + zonelight1.find_roof_fd_sa.ToString() + "','" + zonelight1.r_nearEm_DC.ToString() + "','" + zonelight1.find_roof_fd_c.ToString() + "','" + zonelight1.Zone_Roof_FDS[mth].ToString() + "','" + zonelight1.Zone_Roof_FD[mth].ToString() + "','" +
                     zonelight1.Zone_Sunlight_SCW[mth].ToString() + "','" + zonelight1.Zone_Sunlight_PjSC[mth].ToString() + "','" + zonelight1.Zone_Final_W[mth].ToString()
                     + "'", "번호,월");
                }
            }
            //Cal_Heating heating = new Cal_Heating("HS01");
            //MessageBox.Show(heating.Qhb_mth_sum[0].ToString());

            MessageBox.Show("계산되었습니다.");
            return true;
        }

        private static bool ZoneCalcSave()
        {

            return true;
        }

        // calculation functions end
        /////////////////////////////////////////////////////////////////////////////////////

        private Dictionary<string, Delegate> _calculations = new Dictionary<string, Delegate>();
        private static Dictionary<string, Zone> Zones = new Dictionary<string, Zone>();
        private static Dictionary<string, ZoneLight> ZoneLights = new Dictionary<string, ZoneLight>();

        public Zone getZone(string zoneNum)
        {
            if (Zones.ContainsKey(zoneNum))
            {
                return Zones[zoneNum];
            }
            else return null;
        }
        public ZoneLight getZoneLight(string zoneNum)
        {
            if (ZoneLights.ContainsKey(zoneNum))
            {
                return ZoneLights[zoneNum];
            }
            else return null;
        }
        public bool run(string[] calculations)
        {
            foreach (string calc in calculations)
            {
                _calculations[calc].DynamicInvoke();
            }

            return true;
        }
    }
}
