using main;
using main.subcontents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace main
{
    internal class CALC
    {
        public static ArrayList zone = new ArrayList();
        public static ArrayList zonelight = new ArrayList();

        public static void Run_Zone()
        {
            Cal_Qb();
        }
        public static void Run_All()
        {
            Cal_Qb();
            Cal_Qahu();
            Cal_Qfh();
            CoolingSystemCalc();
            Cal_Qfw();
            Cal_Qf();
            RESystemCalc();
            MessageBox.Show("계산되었습니다.");
        }
        public static void Cal_Qb()
        {
            Program.DB.deleteTable(DB.type.ProjDB, "Zone_LightResult");
            Program.DB.initTable(DB.type.ProjDB, "Zone_LightResult");
            Program.DB.deleteTable(DB.type.ProjDB, "Zone_HCneed_Result");
            Program.DB.initTable(DB.type.ProjDB, "Zone_HCneed_Result");
            Program.DB.initTable(DB.type.ProjDB, "Zone_Envelope_Result");
            Zone_Arrange();
            for (int k = 0; k < zone.Count; k++)
            {
                Zone zone1 = (Zone)zone[k];
                ZoneLight zonelight1 = (ZoneLight)zonelight[k];
                Zone_LoadData(zone1, zonelight1);
                Zone_Calc(zone1, zonelight1);
                Zone_Save(zone1, zonelight1);
            }
        }
        private static void Cal_Qahu()
        {
            Program.DB.deleteTable(DB.type.ProjDB, "AHUSystem_Result");
            Program.DB.initTable(DB.type.ProjDB, "AHUSystem_Result");
            string[][] Num = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "번호,유형");
            if (Num.Length > 0)
            {
                int i = -1;
                while (++i < Num.Length)
                {
                    AHU Pre_AHU1 = new AHU(Num[i][0]);
                    AHUs[Num[i][0]] = Pre_AHU1;
                    AHUSystem_LaodData(Pre_AHU1);
                    AHUSystem_PreCalc(Pre_AHU1);
                }
                Cal_Qb();
                i = -1;
                while (++i < Num.Length)
                {
                    if (Num[i][1] == "공조기")
                    {
                        AHU Post_AHU1 = new AHU(Num[i][0]);
                        AHUs[Num[i][0]] = Post_AHU1;
                        AHUSystem_LaodData(Post_AHU1);
                        AHUSystem_PostCalc(Post_AHU1);
                        AHUSystem_PostSave(Post_AHU1);
                    }
                    else
                    {
                        AHU Post_HRV1 = new AHU(Num[i][0]);
                        AHUs[Num[i][0]] = Post_HRV1;
                        AHUSystem_LaodData(Post_HRV1);
                        HRV_PostCalc(Post_HRV1);
                        HRV_PostSave(Post_HRV1);
                    }

                }
            }

        }
        private static void Cal_Qfh()
        {
            Program.DB.deleteTable(DB.type.ProjDB, "HeatingSystem_Result");
            Program.DB.initTable(DB.type.ProjDB, "HeatingSystem_Result");
            Heating_ce_zone_calc();
            string[][] HeatingNum = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "번호");
            int i = -1;
            while (++i < HeatingNum.Length)
            {
                Heating Heating1 = new Heating(HeatingNum[i][0]);
                Heatings[HeatingNum[i][0]] = Heating1;
                Heating_LoadData(Heating1);
                Heating_Calc(Heating1);
                Heating_Save(Heating1);
            }
        }
        private static void Cal_Qfw()
        {
            Program.DB.deleteTable(DB.type.ProjDB, "DHWSystem_Result");
            Program.DB.initTable(DB.type.ProjDB, "DHWSystem_Result");
            string[][] DHWNum = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "번호");
            if(DHWNum.Length > 0)
            {
                int i = -1;
                while(++i < DHWNum.Length)
                {
                    DHW DHW1 = new DHW(DHWNum[i][0]);
                    DHWs[DHWNum[i][0]] = DHW1;
                    DHW_LoadData(DHW1);
                    DHW_Calc(DHW1);
                    DHW_Save(DHW1);
                }
            }
        }
        private static void Cal_Qf()
        {
            string[][] PNum = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
            if (PNum.Length > 0)
            {
                Final final1 = new Final();
                CALC.Final_Calc(final1);
                CALC.Final_Save(final1);
            }
        }

        #region 요구량
        public static void Zone_Arrange()
        {
            zone.Clear(); zonelight.Clear();
            string[][] zones = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,냉난방유무");
            String[,] zones_arr = new String[zones.Length, 2];//존번호, 냉난방유무
            String[] zones_순번 = new String[zones.Length];// 계산 순서대로 존번호
            int N_비냉난방 = 0, N_난방 = 0, N_냉방 = 0, N_냉난방 = 0; //순번 카운팅 
            int T_비냉난방 = 0, T_난방 = 0, T_냉방 = 0, T_냉난방 = 0; //총계 카운팅 

            if (zones.Length > 0)
            {
                for (int n = 0; n < zones.Length; n++) //배열로 바꿈 : 존번호, 냉난방유무
                {
                    zones_arr[n, 0] = zones[n][0];
                    zones_arr[n, 1] = zones[n][1];
                }
                for (int n = 0; n < zones.Length; n++)
                {
                    if (zones_arr[n, 1] == "비냉난방")
                    {
                        T_비냉난방++;
                    }

                    if (zones_arr[n, 1] == "난방")
                    {
                        T_난방++;
                    }

                    if (zones_arr[n, 1] == "냉방")
                    {
                        T_냉방++;
                    }

                    if (zones_arr[n, 1] == "냉난방")
                    {
                        T_냉난방++;
                    }
                }

                for (int n = 0; n < zones.Length; n++)
                {
                    if (zones_arr[n, 1] == "비냉난방")
                    {
                        N_비냉난방++;
                        if (N_비냉난방 > 0)
                        { zones_순번[N_비냉난방 - 1] = zones_arr[n, 0]; }
                    }
                }
                N_난방 = N_비냉난방;
                for (int n = 0; n < zones.Length; n++)
                {
                    if (zones_arr[n, 1] == "난방")
                    {
                        N_난방++;
                        if (N_난방 > 0)
                        { zones_순번[N_난방 - 1] = zones_arr[n, 0]; }

                    }

                }

                N_냉방 = N_난방;
                for (int n = 0; n < zones.Length; n++)
                {
                    if (zones_arr[n, 1] == "냉방")
                    {
                        N_냉방++;
                        if (N_냉방 > 0)
                        { zones_순번[N_냉방 - 1] = zones_arr[n, 0]; }
                    }
                }

                N_냉난방 = N_냉방;
                for (int n = 0; n < zones.Length; n++)
                {
                    if (zones_arr[n, 1] == "냉난방")
                    {
                        N_냉난방++;
                        if (N_냉난방 > 0)
                        { zones_순번[N_냉난방 - 1] = zones_arr[n, 0]; }
                    }
                }

            }
            int i = -1;
            for (int k = 0; k < zones_순번.Length; k++)
            {
                ZoneLights[zones_순번[k]] = null;
                Zones[zones_순번[k]] = null;
            }
            while (++i < zones.Length)
            {
                ZoneLight zonelight1 = new ZoneLight(zones_순번[i]);
                zonelight.Add(zonelight1);
                ZoneLights[zones_순번[i]] = zonelight1;
                Zone zone1 = new Zone(zones_순번[i]);
                zone.Add(zone1);
                Zones[zones_순번[i]] = zone1;
            }

        }
        public static void Zone_LoadData(Zone zone1, ZoneLight zonelight1)
        {
            zonelight1.LoadData_LightGeneral();
            zonelight1.LoadData_LightSystem();
            zonelight1.LoadData_NaturalLight();
            zonelight1.LoadData_Renew();
            zone1.LoadData_ZoneGeneral();
            zone1.LoadData_Ventil();
            zone1.LoadData_InWall();
            zone1.LoadData_SL();
            zone1.LoadData_Wall();
            zone1.LoadData_Roof();
            zone1.LoadData_Floor();
            zone1.LoadData_GWall();
            zone1.LoadData_Door();
            zone1.LoadData_Win();
            zone1.LoadData_CW();
        }
        private static void Zone_Save(Zone zone1, ZoneLight zonelight1)
        {
            String HC, WEWD, MTH;
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            for (int mth = 0; mth <= 11; mth++)
            {
                MTH = (mth + 1).ToString() + "월";

                Program.DB.setValue(DB.type.ProjDB, "Zone_LightResult", "프로젝트번호,프로젝트유형,번호,월," +
                    "ITr,IRD,ISh_Ish,Ish_In_At,Wi,Ish_GDF,Ish," +
                    "f_τeff_SNA,f_D,f_nearD,f_DCA,f_dclass,f_nearEm_SNA,f_fd_sna,f_fd_sa,f_nearEm_DC,f_fd_c,f_FDS,f_FD," +
                    "as_bs,hs_bs,hg_hw," +
                    "normal_ηR,saw_ηR,r_DSNA,r_DSA,r_dclass," +
                    "r_nearEm_FDS,r_fd_sna,r_fd_sa,r_nearEm_DC,r_fd_c,r_FDS,r_FD," +
                    "Sunlight_SCW,Sunlight_PjSC,Final_kWh",

                "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + zonelight1.ZoneNum + "','" + MTH + "','" +
                 zonelight1.Zone_ITr.ToString() + "','" + zonelight1.Zone_IRD.ToString() + "','" + zonelight1.Zone_Ish[mth].ToString() + "','" + zonelight1.Zone_Ish_In_At.ToString() + "','" + zonelight1.Zone_Wi.ToString() + "','" + zonelight1.Zone_Ish_GDF.ToString() + "','" + zonelight1.Zone_Calc_Ish[mth].ToString() + "','" +
                 zonelight1.Zone_τeff_SNA_j.ToString() + "','" + zonelight1.Zone_D[mth].ToString() + "','" + zonelight1.Zone_nearD[mth].ToString() + "','" + zonelight1.Zone_DCA[mth].ToString() + "','" + zonelight1.dclass[mth] + "','" + zonelight1.f_nearEm_SNA.ToString() + "','" + zonelight1.find_fd_sna[mth].ToString() + "','" + zonelight1.find_fd_sa[mth].ToString() + "','" + zonelight1.f_naerEm_DC.ToString() + "','" + zonelight1.find_fd_c[mth].ToString() + "','" + zonelight1.Zone_FDS[mth].ToString() + "','" + zonelight1.Zone_Facade_FD[mth].ToString() + "','" +
                 zonelight1.Zone_as_bs.ToString() + "','" + zonelight1.Zone_hs_bs.ToString() + "','" + zonelight1.Zone_hg_hw.ToString() + "','" +
                 zonelight1.find_normal_ηR.ToString() + "','" + zonelight1.find_saw_ηR.ToString() + "','" + zonelight1.Zone_Roof_DSNA.ToString() + "','" + zonelight1.Zone_Roof_DSA.ToString() + "','" + zonelight1.roof_dclass + "','" +
                 zonelight1.r_nearEm_FDS.ToString() + "','" + zonelight1.find_roof_fd_sna.ToString() + "','" + zonelight1.find_roof_fd_sa.ToString() + "','" + zonelight1.r_nearEm_DC.ToString() + "','" + zonelight1.find_roof_fd_c.ToString() + "','" + zonelight1.Zone_Roof_FDS[mth].ToString() + "','" + zonelight1.Zone_Roof_FD[mth].ToString() + "','" +
                 zonelight1.Zone_Sunlight_SCW[mth].ToString() + "','" + zonelight1.Zone_Sunlight_PjSC[mth].ToString() + "','" + zonelight1.Zone_Final_kWh[mth].ToString()
                 + "'", "번호,월");
            }

            //[난방/냉방,비이용일/이용일,mth] = [h/c,we/wd,mth]=[0/1,0/1,12]
            for (int hc = 0; hc <= 1; hc++)
            {
                if (hc == 0)
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

                        Program.DB.setValue(DB.type.ProjDB, "Zone_HCneed_Result", "프로젝트번호,프로젝트유형,번호,이름," +
                             "난방_냉방,비이용일_이용일,월," +
                             "HT_tot,HT_InWall,HT_Slab,HT_Wall,HT_Roof,HT_Floor,HT_GWall,HT_Door,HT_Win,HT_CW," +
                             "HT_Di_Wall,HT_Indi_Wall,HT_Di_Roof,HT_Indi_Roof,HT_Di_Win,HT_Indi_Win,HT_Di_Door,HT_Indi_Door," +
                             "HT_TB_tot,HT_TB_Wall,HT_TB_Roof,HT_TB_Floor,HT_TB_Gwall,HT_TB_Win,HT_TB_Door,HT_TB_CW," +
                             "nmech,nz,ninf,nwin," +
                             "HV_tot,HV_inf,HV_win,HV_z,HV_mech," +
                             "H_tot,tao,dwe_mth,dwd_mth,theta_i,theta_e," +
                             "QTsink_tot,QT_u_sink,QTsink_Wall,QTsink_Roof,QTsink_Floor,QTsink_GWall,QTsink_Door,QTsink_Win,QTsink_CW," +
                             "QTsource_tot,QT_u_source,QTsource_Wall,QTsource_Roof,QTsource_Floor,QTsource_GWall,QTsource_Door,QTsource_Win,QTsource_CW," +
                             "QSopsink_tot,QSopsource_tot,QStr_tot," +
                             "QSopsink_Wall,QSopsink_Roof,QSopsink_Door,QSopsink_CW_p," +
                             "QSopsource_Wall,QSopsource_Roof,QSopsource_Door,QSopsource_CW_p," +
                             "QStr_Win,QStr_CW," +
                             "QVsink_tot,QV_inf_sink,QV_win_sink,QV_z_sink,QV_mech_sink," +
                             "QVsource_tot,QV_inf_source,QV_win_source,QV_z_source,QV_mech_source," +
                             "Q_DHU_win,Q_DHU_mech,Q_DHU_tot," +
                             "QI_tot,QI_L," +
                             "QI_P,QI_fac,QI_Humidity," +
                             "Qsink,Qsource,gamma,a,eta,dQc_b,dQc_sink," +
                             "Qb_day," +
                             "Qb_mth," +
                             "Qb_a,Q_max, t_max,비냉난방존온도",
                              "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + zone1.ZoneNum + "','" + zone1.zoneName + "','" +
                              HC + "','" + WEWD + "','" + MTH + "','" +
                              zone1.Zone_HT_tot[hc, wewd, mth].ToString() + "','" + zone1.Zone_HT_Inwall[hc, wewd, mth].ToString() + "','" + zone1.Zone_HT_Slab[hc, wewd, mth].ToString() + "','" + zone1.Zone_HT_Wall.ToString() + "','" + zone1.Zone_HT_Roof.ToString() + "','" + zone1.Zone_HT_Floor.ToString() + "','" + zone1.Zone_HT_GWall.ToString() + "','" + zone1.Zone_HT_Door.ToString() + "','" + zone1.Zone_HT_Win.ToString() + "','" + zone1.Zone_HT_CW.ToString() + "','" +
                              zone1.Zone_HT_Di_Wall.ToString() + "','" + zone1.Zone_HT_Indi_Wall.ToString() + "','" + zone1.Zone_HT_Di_Roof.ToString() + "','" + zone1.Zone_HT_Indi_Roof.ToString() + "','" + zone1.Zone_HT_Di_Win.ToString() + "','" + zone1.Zone_HT_Indi_Win.ToString() + "','" + zone1.Zone_HT_Di_Door.ToString() + "','" + zone1.Zone_HT_Indi_Door.ToString() + "','" +
                              zone1.Zone_HT_TB_tot.ToString() + "','" + zone1.Zone_HT_TB_Wall.ToString() + "','" + zone1.Zone_HT_TB_Roof.ToString() + "','" + zone1.Zone_HT_TB_Floor.ToString() + "','" + zone1.Zone_HT_TB_GWall.ToString() + "','" + zone1.Zone_HT_TB_Win.ToString() + "','" + zone1.Zone_HT_TB_Door.ToString() + "','" + zone1.Zone_HT_TB_CW.ToString() + "','" +
                              zone1.nmech[wewd].ToString() + "','" + zone1.nz[wewd].ToString() + "','" + zone1.ninf[wewd].ToString() + "','" + zone1.nwin[wewd].ToString() + "','" +
                              zone1.Zone_HV_tot[wewd].ToString() + "','" + zone1.Zone_HV_inf[wewd].ToString() + "','" + zone1.Zone_HV_win[wewd].ToString() + "','" + zone1.Zone_HV_z[wewd].ToString() + "','" + zone1.Zone_HV_mech[wewd].ToString() + "','" +
                              zone1.Zone_H_tot[hc, wewd, mth].ToString() + "','" + zone1.tao[hc, wewd, mth].ToString() + "','" + zone1.dwe_mth[mth].ToString() + "','" + zone1.dwd_mth[mth].ToString() + "','" + zone1.theta_i[hc, wewd, mth].ToString() + "','" + zone1.theta_e[mth].ToString() + "','" +
                              zone1.QTsink_tot[hc, wewd, mth].ToString() + "','" + zone1.QT_u_sink[hc, wewd, mth].ToString() + "','" + zone1.QTsink_Wall[hc, wewd, mth].ToString() + "','" + zone1.QTsink_Roof[hc, wewd, mth].ToString() + "','" + zone1.QTsink_Floor[hc, wewd, mth].ToString() + "','" + zone1.QTsink_GWall[hc, wewd, mth].ToString() + "','" + zone1.QTsink_Door[hc, wewd, mth].ToString() + "','" + zone1.QTsink_Win[hc, wewd, mth].ToString() + "','" + zone1.QTsink_CW[hc, wewd, mth].ToString() + "','" +
                              zone1.QTsource_tot[hc, wewd, mth].ToString() + "','" + zone1.QT_u_source[hc, wewd, mth].ToString() + "','" + zone1.QTsource_Wall[hc, wewd, mth].ToString() + "','" + zone1.QTsource_Roof[hc, wewd, mth].ToString() + "','" + zone1.QTsource_Floor[hc, wewd, mth].ToString() + "','" + zone1.QTsource_GWall[hc, wewd, mth].ToString() + "','" + zone1.QTsource_Door[hc, wewd, mth].ToString() + "','" + zone1.QTsource_Win[hc, wewd, mth].ToString() + "','" + zone1.QTsource_CW[hc, wewd, mth].ToString() + "','" +
                              zone1.QSopsink_tot[hc, wewd, mth].ToString() + "','" + zone1.QSopsource_tot[hc, wewd, mth].ToString() + "','" + zone1.QStr_tot[hc, wewd, mth].ToString() + "','" +
                              zone1.QSopsink_Wall[mth].ToString() + "','" + zone1.QSopsink_Roof[mth].ToString() + "','" + zone1.QSopsink_Door[mth].ToString() + "','" + zone1.QSopsink_CW_p[mth].ToString() + "','" +
                              zone1.QSopsource_Wall[mth].ToString() + "','" + zone1.QSopsource_Roof[mth].ToString() + "','" + zone1.QSopsource_Door[mth].ToString() + "','" + zone1.QSopsource_CW_p[mth].ToString() + "','" +
                              zone1.QStr_Win[wewd, mth].ToString() + "','" + zone1.QStr_CW[wewd, mth].ToString() + "','" +
                              zone1.QVsink_tot[hc, wewd, mth].ToString() + "','" + zone1.QV_inf_sink[hc, wewd, mth].ToString() + "','" + zone1.QV_win_sink[hc, wewd, mth].ToString() + "','" + zone1.QV_z_sink[hc, wewd, mth].ToString() + "','" + zone1.QV_mech_sink[hc, wewd, mth].ToString() + "','" +
                              zone1.QVsource_tot[hc, wewd, mth].ToString() + "','" + zone1.QV_inf_source[hc, wewd, mth].ToString() + "','" + zone1.QV_win_source[hc, wewd, mth].ToString() + "','" + zone1.QV_z_source[hc, wewd, mth].ToString() + "','" + zone1.QV_mech_source[hc, wewd, mth].ToString() + "','" +
                              zone1.Q_DHU_win[wewd, mth].ToString() + "','" + zone1.Q_DHU_mech[mth].ToString() + "','" + zone1.Q_DHU_tot[wewd, mth].ToString() + "','" +
                              zone1.QI_tot[hc, wewd, mth].ToString() + "','" + zone1.QI_L[hc, wewd, mth].ToString() + "','" +
                              zone1.QI_P[wewd].ToString() + "','" + zone1.QI_fac[wewd].ToString() + "','" + zone1.QI_Humidity[mth].ToString() + "','" +
                              zone1.Qsink[hc, wewd, mth].ToString() + "','" + zone1.Qsource[hc, wewd, mth].ToString() + "','" + zone1.gamma[hc, wewd, mth].ToString() + "','" + zone1.a[hc, wewd, mth].ToString() + "','" + zone1.eta[hc, wewd, mth].ToString() + "','" + zone1.dQc_b[hc, wewd, mth].ToString() + "','" + zone1.dQc_sink[hc, wewd, mth].ToString() + "','" +
                              zone1.Qb_day[hc, wewd, mth].ToString() + "','" +
                              zone1.Qb_mth[hc, wewd, mth].ToString() + "','" +
                              zone1.Qb_a[hc].ToString() + "','" + zone1.Q_max[hc].ToString() + "','" + zone1.t_max[hc, mth].ToString() + "','" +
                              zone1.Theta_U[hc, wewd, mth].ToString()
                              + "'", "번호,난방_냉방,비이용일_이용일,월");
                    }
                }
            }

        }
        public static void Zone_Calc(Zone zone1, ZoneLight zonelight1)
        {
            zonelight1.Calc_time(zone1.ZoneNum);
            zonelight1.Calc_Facade_general();
            zonelight1.Calc_Facade_shade();
            zonelight1.Calc_Facade_FDS();
            zonelight1.Calc_Facade_FD();
            zonelight1.Calc_Roof_general();
            zonelight1.Calc_Roof_FDS();
            zonelight1.Calc_Roof_FD();
            zonelight1.Calc_Sunlight_SCW();
            zonelight1.Calc_Sunlight_Pj_SC();
            zonelight1.Calc_kWh();

            zone1.ZoneHT();
            zone1.ZoneHV();
            zone1.Zonetao();
            zone1.Zonethetai();
            zone1.ZoneQT_u();
            zone1.ZoneQT();
            zone1.ZoneQV();
            zone1.ZoneQSop(zone1.ZoneNum);
            zone1.ZoneQStr(zone1.ZoneNum);
            zone1.ZoneQ_DHU();
            zone1.ZoneQI_L();
            zone1.ZoneQI();
            zone1.Zone_Theta_U();
            zone1.Zoneeta();
            zone1.ZoneQb();
            zone1.ZoneQmax();
        }
        #endregion

        #region 공조
        public static void AHUSystem_LaodData(AHU AHU1)
        {
            AHU1.Load_ZoneData();
            AHU1.Load_GeneralData();
            AHU1.Load_AHUData();
            AHU1.Load_DuctData();
            AHU1.Load_PrehPrecData();            
        }
        public static void AHUSystem_PreCalc(AHU AHU1)
        {
            AHU1.Cal_CoolTube();
            AHU1.Cal_Preheating();
            AHU1.Cal_Duct();
            AHU1.Cal_DuctLoss_OA();
            AHU1.Cal_DuctLoss_RA();
            AHU1.Cal_HeatRecovery();
        }
        public static void AHUSystem_PostCalc(AHU AHU1)
        {
            AHU1.Cal_CoolTube();
            AHU1.Cal_Preheating();
            AHU1.Cal_Duct();
            AHU1.Cal_DuctLoss_OA();
            AHU1.Cal_DuctLoss_RA();
            AHU1.Cal_HeatRecovery();
            AHU1.Cal_DuctLoss_EA();
            AHU1.Cal_SA_set();
            AHU1.Cal_RCA();
            AHU1.Cal_DuctLoss_SA();
            AHU1.Cal_Qv_b();
            AHU1.Cal_HU();
            AHU1.Cal_W();
        }
        public static void HRV_PostCalc(AHU HRV1)
        {
            HRV1.Cal_CoolTube();
            HRV1.Cal_Preheating();
            HRV1.Cal_Duct();
            HRV1.Cal_DuctLoss_OA();
            HRV1.Cal_DuctLoss_RA();
            HRV1.Cal_HeatRecovery();
            HRV1.Cal_DuctLoss_EA();
            HRV1.Cal_SA_set();
            HRV1.Cal_RCA();
            HRV1.Cal_DuctLoss_SA();
            HRV1.Cal_W();
        }
        private static void AHUSystem_PostSave(AHU AHU1)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            Program.DB.deleteValue(DB.type.ProjDB, "AHUSystem_Result", "번호='" + AHU1.AHUNum + "'And 프로젝트번호 ='" + 프로젝트유형[0][1] + "'");
            String MTH, 난방냉방;
            for (int hc = 0; hc <= 1; hc++)
            {
                for (int mth = 0; mth <= 11; mth++)
                {

                    if (hc == 0) { 난방냉방 = "난방"; } else { 난방냉방 = "냉방"; }
                    MTH = (mth + 1).ToString() + "월";
                    Program.DB.setValue(DB.type.ProjDB, "AHUSystem_Result", "프로젝트번호,프로젝트유형,번호," +
                             "난방_냉방,월," +
                             "공조요구량,가습요구량,급기팬보조에너지,배기팬보조에너지,가습보조에너지,프리히팅보조에너지," +
                             "theta_vmech,Vvmech,Vvmech_leak," +
                             "theta_SA_prh,theta_OA_du,theta_RA_du,theta_SA_hr,theta_SA_rca,theta_SA_du,X_iset," +
                             "X_SA_prh,X_SA_hr,X_SA_rca," +
                             "Vmin_tot,Qb_mth_tot,Qmax_tot,theta_iset_avg,dvmech_avg,tvmech_avg," +
                             "Q_gnd,Q_prh,Q_loss_OA_du,Q_loss_EA_du,Q_loss_SA_du," +
                             "dtheta_prh,dtheta_du_OA,dtheta_du_RA,dtheta_hr,dtheta_rca,dtheta_du_EA,dtheta_du_SA," +
                             "flea_du,flea_ahu,fins_ahu,theta_defrost,theta_sur_nc,Hduct_OA,Hduct_RA,Hduct_EA,Hduct_SA",
                             "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + AHU1.AHUNum + "','" + 난방냉방 + "','" + MTH + "','" +
                             AHU1.Qv_b[hc, mth] + "','" + AHU1.Qhu_b[mth] + "','" + AHU1.Ev_gen_fan_SA[mth] + "','" + AHU1.Ev_gen_fan_EA[mth] + "','" + AHU1.W_HU_aux[mth] + "','" + AHU1.Wv_aux_preh[mth] + "','" +
                             AHU1.theta_vmech[hc, mth] + "','" + AHU1.Vvmech[hc, mth] + "','" + AHU1.Vvmech_leak[hc, mth] + "','" +
                             AHU1.theta_SA_prh[mth] + "','" + AHU1.theta_OA_du[hc, mth] + "','" + AHU1.theta_RA_du[hc, mth] + "','" + AHU1.theta_SA_hr[hc, mth] + "','" + AHU1.theta_SA_rca[hc, mth] + "','" + AHU1.theta_SA_du[hc, mth] + "','" + AHU1.X_iset[mth] + "','" +
                             AHU1.X_SA_prh[mth] + "','" + AHU1.X_SA_hr[mth] + "','" + AHU1.X_SA_rca[mth] + "','" +
                             AHU1.Vmin_tot + "','" + AHU1.Qb_mth_tot[hc, mth] + "','" + AHU1.Qmax_tot[hc] + "','" + AHU1.theta_iset_avg[hc] + "','" + AHU1.dvmechmth_avg[mth] + "','" + AHU1.tvmech_avg + "','" +
                             AHU1.Q_gnd[mth] + "','" + AHU1.Wpreh_k[mth] + "','" + AHU1.Q_loss_OA_du[hc, mth] + "','" + AHU1.Q_loss_EA_du[hc, mth] + "','" + AHU1.Q_loss_SA_du[hc, mth] + "','" +
                             AHU1.dtheta_prh[mth] + "','" + AHU1.dtheta_du_OA[hc, mth] + "','" + AHU1.dtheta_du_RA[hc, mth] + "','" + AHU1.dtheta_hr[hc, mth] + "','" + AHU1.dtheta_rca[hc, mth] + "','" + AHU1.dtheta_du_EA[hc, mth] + "','" + AHU1.dtheta_du_SA[hc, mth] + "','" +
                             AHU1.flea_du + "','" + AHU1.flea_ahu + "','" + AHU1.fins_ahu + "','" + AHU1.theta_defrost + "','" + AHU1.theta_sur_nc[hc, mth] + "','" + AHU1.Hduct_OA[mth] + "','" + AHU1.Hduct_RA[mth] + "','" + AHU1.Hduct_EA[mth] + "','" + AHU1.Hduct_SA[mth]
                              + "'", "번호,난방_냉방,월");
                }
            }
        }
        private static void HRV_PostSave(AHU HRV1)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            String MTH, 난방냉방;
            for (int hc = 0; hc <= 1; hc++)
            {
                for (int mth = 0; mth <= 11; mth++)
                {
                    if (hc == 0) { 난방냉방 = "난방"; } else { 난방냉방 = "냉방"; }
                    MTH = (mth + 1).ToString() + "월";
                    Program.DB.setValue(DB.type.ProjDB, "AHUSystem_Result", "프로젝트번호,프로젝트유형,번호," +
                             "난방_냉방,월," +
                             "공조요구량,가습요구량,급기팬보조에너지,배기팬보조에너지,가습보조에너지,프리히팅보조에너지," +
                             "theta_vmech,Vvmech,Vvmech_leak," +
                             "theta_SA_prh,theta_OA_du,theta_RA_du,theta_SA_hr,theta_SA_rca,theta_SA_du,X_iset," +
                             "X_SA_prh,X_SA_hr,X_SA_rca," +
                             "Vmin_tot,Qb_mth_tot,Qmax_tot,theta_iset_avg,dvmech_avg,tvmech_avg," +
                             "Q_gnd,Q_prh,Q_loss_OA_du,Q_loss_EA_du,Q_loss_SA_du," +
                             "dtheta_prh,dtheta_du_OA,dtheta_du_RA,dtheta_hr,dtheta_rca,dtheta_du_EA,dtheta_du_SA," +
                             "flea_du,flea_ahu,fins_ahu,theta_defrost,theta_sur_nc,Hduct_OA,Hduct_RA,Hduct_EA,Hduct_SA",
                             "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + HRV1.AHUNum + "','" + 난방냉방 + "','" + MTH + "','" +
                             HRV1.Qv_b[hc, mth] + "','" + HRV1.Qhu_b[mth] + "','" + HRV1.Ev_gen_fan_SA[mth] + "','" + HRV1.Ev_gen_fan_EA[mth] + "','" + HRV1.W_HU_aux[mth] + "','" + HRV1.Wv_aux_preh[mth] + "','" +
                             HRV1.theta_vmech[hc, mth] + "','" + HRV1.Vvmech[hc, mth] + "','" + HRV1.Vvmech_leak[hc, mth] + "','" +
                             HRV1.theta_SA_prh[mth] + "','" + HRV1.theta_OA_du[hc, mth] + "','" + HRV1.theta_RA_du[hc, mth] + "','" + HRV1.theta_SA_hr[hc, mth] + "','" + HRV1.theta_SA_rca[hc, mth] + "','" + HRV1.theta_SA_du[hc, mth] + "','" + HRV1.X_iset[mth] + "','" +
                             HRV1.X_SA_prh[mth] + "','" + HRV1.X_SA_hr[mth] + "','" + HRV1.X_SA_rca[mth] + "','" +
                             HRV1.Vmin_tot + "','" + HRV1.Qb_mth_tot[hc, mth] + "','" + HRV1.Qmax_tot[hc] + "','" + HRV1.theta_iset_avg[hc] + "','" + HRV1.dvmechmth_avg[mth] + "','" + HRV1.tvmech_avg + "','" +
                             HRV1.Q_gnd[mth] + "','" + HRV1.Wpreh_k[mth] + "','" + HRV1.Q_loss_OA_du[hc, mth] + "','" + HRV1.Q_loss_EA_du[hc, mth] + "','" + HRV1.Q_loss_SA_du[hc, mth] + "','" +
                             HRV1.dtheta_prh[mth] + "','" + HRV1.dtheta_du_OA[hc, mth] + "','" + HRV1.dtheta_du_RA[hc, mth] + "','" + HRV1.dtheta_hr[hc, mth] + "','" + HRV1.dtheta_rca[hc, mth] + "','" + HRV1.dtheta_du_EA[hc, mth] + "','" + HRV1.dtheta_du_SA[hc, mth] + "','" +
                             HRV1.flea_du + "','" + HRV1.flea_ahu + "','" + HRV1.fins_ahu + "','" + HRV1.theta_defrost + "','" + HRV1.theta_sur_nc[hc, mth] + "','" + HRV1.Hduct_OA[mth] + "','" + HRV1.Hduct_RA[mth] + "','" + HRV1.Hduct_EA[mth] + "','" + HRV1.Hduct_SA[mth]
                              + "'", "번호,난방_냉방,월");
                }
            }

        }
        public static void Heating_ce_zone_calc()
        {
            string[][] HeatingNum = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "번호");
            int i = -1;
            String MTH;
            string[][] Zone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호", "냉난방유무 ='냉난방' OR 냉난방유무 = '난방'");
            while (++i < HeatingNum.Length)
            {
                if (Zone.Length > 0)
                {
                    for (int n = 0; n < Zone.Length; n++)
                    {
                        Zone zone = Program.CALC.getZone(Zone[n][0].ToString());
                        string[][] ce = Program.DB.getValue(DB.type.ProjDB, "Heating_ce_Form", "공급설비,공급설비종류,가동시간", "존번호 = '" + Zone[n][0] + "'");

                        double[] 가동비율 = new double[ce.Length];
                        double 가동비율_tot = 0;

                        for (int a = 0; a < ce.Length; a++)
                        {
                            string[][] ce2 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "용량_난방", "번호='" + ce[a][0].Substring(0, 4) + "'");
                            if (ce[a][1] != "복사난방")
                            {
                                가동비율[a] = Convert.ToDouble(ce[a][2]) * Convert.ToDouble(ce2[0][0]);
                                가동비율_tot += 가동비율[a];
                            }
                            else
                            {

                                가동비율[a] = Convert.ToDouble(ce[a][2]) * zone.Q_max[0] / 1000;
                                가동비율_tot += 가동비율[a];
                            }
                        }


                        for (int a = 0; a < ce.Length; a++)
                        {
                            double[] Qhb_mth = new double[12];
                            for (int mth = 0; mth < 12; mth++)
                            {
                                Qhb_mth[mth] = zone.Qb_mth[0, 1, mth] * 가동비율[a] / 가동비율_tot;
                            }
                            Zone_HeatingCE zone_heatingce = new Zone_HeatingCE(Zone[n][0], HeatingNum[i][0], ce[a][0], Qhb_mth);
                            Zone_HeatingCEs[Zone[n][0] + "_" + HeatingNum[i][0] + "_" + ce[a][0]] = zone_heatingce;
                        }
                    }
                }
            }
        }
        #endregion

        #region 난방
        public static void Heating_LoadData(Heating Heating1)
        {
            Heating1.Load_Zonedata();
            Heating1.Load_HeatingGeneral();
            Heating1.Load_Boiler();
            Heating1.Load_Solar();
            Heating1.Load_PumpData();
            Heating1.Load_ceData();
            Heating1.Load_StorageData();
            Heating1.Load_PipeData();
            Heating1.Load_AirHP();
            Heating1.Load_GroundHP();
            Heating1.Load_GWHP();
        }
        public static void Heating_Calc(Heating Heating1)
        {
            Heating1.Calc_thrL();
            Heating1.Calc_beta_ce();
            Heating1.Calc_Qce();
            Heating1.Calc_beta_d();
            Heating1.Calc_Qd();
            Heating1.Calc_beta_s();
            Heating1.Calc_Qh_s();
            Heating1.Calc_beta_gen();
            Heating1.Calc_Qh_gen_Boiler();
            Heating1.Calc_Solar();
            Heating1.Calc_Q_Air_HP();
            Heating1.nan();
        }
        private static void Heating_Save(Heating Heating1)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            for (int mth = 0; mth <= 11; mth++)
            {

                string MTH = (mth + 1).ToString() + "월";
                Program.DB.setValue(DB.type.ProjDB, "HeatingSystem_Result", "프로젝트번호,프로젝트유형,번호," +
                         "월," +
                         " Qhb_mth_sum, Qh_max_sum,Qh_a_sum,th_op_day_avg, theta_i_h_set_avg,th_avg,dop_mth_avg," +
                         "thrL,thrL_day,dhrB,fLNA,fLwe," +
                         "beta_h_ce,beta_h_d,beta_h_s,beta_h_gen," +
                         "theta_av_ce,theta_av_d,theta_av_s,theta_av_gen," +
                         "dtheta_ce,dtheta_d,dtheta_s,dtheta_gen," +
                         "dtheta_ce1,dtheta_ce2,Psi_pipe,L,Qs_po_day,Vs," +
                         "Qh_gen_day,Pgen_Pn,Pgen_Pint,Pgen_P0,eta_gen_Pn,eta_gen_Pint," +
                         "fpint_Air,Qh_outg_sngminus7,Qh_outg_sng2,Qh_outg_sng7,COPminus7,COP2,COP7," +
                         "Qh_ce,Qh_d,Qh_s,Qh_gen,Qh_outg,Qh_f," +
                         "Wh_ce,Wh_d,Wh_s,Wh_g,연료",
                         "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + Heating1.HeatingNum + "','" + MTH + "','" +
                          Heating1.Qhb_mth_sum[mth] + "','" + Heating1.Qh_max_sum + "','" + Heating1.Qh_a_sum + "','" + Heating1.th_op_day_avg + "','" + Heating1.theta_i_h_set_avg + "','" + Heating1.th_avg[mth] + "','" + Heating1.dop_mth_avg[mth] + "','" +
                          Heating1.thrL[mth] + "','" + Heating1.thrL_day[mth] + "','" + Heating1.dhrB[mth] + "','" + Heating1.fLNA[mth] + "','" + Heating1.fLwe[mth] + "','" +
                          Heating1.beta_h_ce[mth] + "','" + Heating1.beta_h_d[mth] + "','" + Heating1.beta_h_s[mth] + "','" + Heating1.beta_h_gen[mth] + "','" +
                          Heating1.theta_av_ce[mth] + "','" + Heating1.theta_av_d[mth] + "','" + Heating1.theta_av_s[mth] + "','" + Heating1.theta_av_gen[mth] + "','" +
                          Heating1.dtheta_ce[mth] + "','" + Heating1.dtheta_d[mth] + "','" + Heating1.dtheta_s[mth] + "','" + Heating1.dtheta_gen[mth] + "','" +
                          Heating1.dtheta_ce1 + "','" + Heating1.dtheta_ce2 + "','" + Heating1.Psi_pipe + "','" + Heating1.L + "','" + Heating1.Qs_po_day + "','" + Heating1.Vs + "','" +
                          Heating1.Qh_gen_day[mth] + "','" + Heating1.Pgen_Pn[mth] + "','" + Heating1.Pgen_Pint[mth] + "','" + Heating1.Pgen_P0[mth] + "','" + Heating1.eta_gen_Pn[mth] + "','" + Heating1.eta_gen_Pint[mth] + "','" +
                          Heating1.fpint[mth] + "','" + Heating1.Qh_outg_sng[0, mth] + "','" + Heating1.Qh_outg_sng[1, mth] + "','" + Heating1.Qh_outg_sng[2, mth] + "','" + Heating1.COPpint[0, mth] + "','" + Heating1.COPpint[1, mth] + "','" + Heating1.COPpint[2, mth] + "','" +
                          Heating1.Qh_ce[mth] + "','" + Heating1.Qh_d[mth] + "','" + Heating1.Qh_s[mth] + "','" + Heating1.Qh_gen[mth] + "','" + Heating1.Qh_outg[mth] + "','" + Heating1.Qh_f[mth] + "','" +
                          Heating1.Wh_ce[mth] + "','" + Heating1.Wh_d[mth] + "','" + Heating1.Wh_s[mth] + "','" + Heating1.Wh_g[mth] + "','" + Heating1.Carrier
                          + "'", "번호,월"); ;
            }
        }
        #endregion

        #region 냉방
        public static bool CoolingSystemCalc() //작성 필요함
        {
            string[][] CoolingNum = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "번호");

            Program.DB.deleteTable(DB.type.ProjDB, "CoolingSystem_Result");
            Program.DB.initTable(DB.type.ProjDB, "CoolingSystem_Result");

            for (int i = 0; i < CoolingNum.Length; i++)
            {
                Cal_Cooling cc1 = new Cal_Cooling(CoolingNum[i][0]);
            }
            return true;

        }
        #endregion

        #region 급탕
        public static void DHW_LoadData(DHW DHW1)
        {
            DHW1.Load_Zonedata();
            DHW1.Load_DHWGeneral();
            DHW1.Load_Boiler();
            DHW1.Load_Solar();
            DHW1.Load_PumpData();
            DHW1.Load_StorageData();
            DHW1.Load_PipeData();
        }
        public static void DHW_Calc(DHW DHW1)
        {
            DHW1.Calc_Qd();
            DHW1.Calc_Qh_s();
            DHW1.Calc_Qh_gen_Boiler();
            DHW1.Calc_Solar();
            DHW1.nan();
        }
        private static void DHW_Save(DHW DHW1)
        {
            
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            int i = -1;
            String MTH;
            
            for (int mth = 0; mth <= 11; mth++)
            {

                MTH = (mth + 1).ToString() + "월";
                Program.DB.setValue(DB.type.ProjDB, "DHWSystem_Result", "프로젝트번호,프로젝트유형,번호," +
                         "월," +
                         "Qwb_mth_sum,theta_ih_avg,Qw_a_sum,th_op_day_avg,theta_i_h_set_avg,dop_mth_avg," +
                         "Qw_d,Qw_s,Qw_gen,Qw_outg,Qw_f," +
                         "Ww_d,Ww_s,Ww_g," +
                         "Qw_gen_day,Qw_gen_p0_day,eta_pn_w,연료",
                         "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + DHW1.DHWNum + "','" + MTH + "','" +
                         DHW1.Qwb_mth_sum[mth] + "','" + DHW1.theta_ih_avg[mth] + "','" + DHW1.Qw_a_sum + "','" + DHW1.th_op_day_avg + "','" + DHW1.theta_i_h_set_avg + "','" + DHW1.dop_mth_avg[mth] + "','" +
                         DHW1.Qw_d[mth] + "','" + DHW1.Qw_s[mth] + "','" + DHW1.Qw_gen[mth] + "','" + DHW1.Qw_outg[mth] + "','" + DHW1.Qw_f[mth] + "','" +
                         DHW1.Ww_d[mth] + "','" + DHW1.Ww_s[mth] + "','" + DHW1.Ww_g[mth] + "','" +
                         DHW1.Qw_gen_day[mth] + "','" + DHW1.Qw_gen_p0_day[mth] + "','" + DHW1.eta_pn_w[mth] + "','" + DHW1.Carrier
                          + "'", "번호,월"); ;
            }
        }
        #endregion

        #region 파이널       
        public static void Final_Calc(Final final1)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            if (프로젝트유형[0][0] =="1")
            {
                final1.Calc_Qbase_elec();
                final1.Calc_Qbase_gas();
            }
            else
            {
                string[][] res = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기존프로젝트");
                if (res.Length > 0 && res[0][0] != "")
                {
                    for(int mth = 0; mth < 12; mth++)
                    {
                        string[][] Final2 = Program.DB.querySQL(res[0][0], "SELECT 기저에너지 FROM FinalEnergy_Result where 연료 = '전기' and 월 = '" + (mth + 1).ToString() + "월'");
                        if (Final2.Length > 0)
                        {
                            final1.Qbase_elec[mth] = Convert.ToDouble(Final2[0][0]);
                        }

                       Final2 = Program.DB.querySQL(res[0][0], "SELECT 기저에너지 FROM FinalEnergy_Result where 연료 != '전기' and 월 = '" + (mth + 1).ToString() + "월'");
                        if (Final2.Length > 0)
                        {
                            final1.Qbase_gas[mth] = Convert.ToDouble(Final2[0][0]);
                        }
                    }                    
                }
                
            }
            final1.Calc_Error();
        }
        private static void Final_Save(Final final1)
        {
            String MTH;
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            string[][] PNum = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
            for (int mth = 0; mth <= 11; mth++)
            {
                MTH = (mth + 1).ToString() + "월";
                Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result", "프로젝트번호,프로젝트유형,번호,월,연료," +
                    "난방,냉방,급탕,조명,공조,기저에너지,총에너지소요량",
                    "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + PNum[0][0] + "','" + MTH + "','" + "전기" + "','" +
                    final1.Qhf_elec[mth] + "','" + final1.Qcf_elec[mth] + "','" + final1.Qwf_elec[mth] + "','" + final1.Qlf_elec[mth] + "','" +
                    final1.Qvf_elec[mth] + "','" + final1.Qbase_elec[mth] + "','" + final1.Qf_elec_tot_mth[mth]
                    + "'", "번호,월,연료"); ;

            }
            string Carrier = "";
            if (final1.Carrier_h != "" && final1.Carrier_h != null) { Carrier = final1.Carrier_h; } else if (final1.Carrier_w != "" && final1.Carrier_w != null) { Carrier = final1.Carrier_w; } else if (final1.Carrier_c != "" && final1.Carrier_c != null) { Carrier = final1.Carrier_c; }
            if (Carrier == "LNG" || Carrier == "LPG") { Carrier = "가스"; }
            if (Carrier != "")
            {
                for (int mth = 0; mth <= 11; mth++)
                {
                    MTH = (mth + 1).ToString() + "월";
                    Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result", "프로젝트번호,프로젝트유형,번호,월,연료," +
                        "난방,냉방,급탕,조명,공조,기저에너지,총에너지소요량",
                        "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + PNum[0][0] + "','" + MTH + "','" + Carrier + "','" +
                        final1.Qhf_gas[mth] + "','" + final1.Qcf_gas[mth] + "','" + final1.Qwf_gas[mth] + "','" + "0" + "','" +
                        "0" + "','" + final1.Qbase_gas[mth] + "','" + final1.Qf_gas_tot_mth[mth]
                        + "'", "번호,월,연료"); ;
                }
            }
        }
        #endregion

        #region 신재생
        public static bool RESystemCalc()
        {
            string[][] PVNum = Program.DB.getValue(DB.type.ProjDB, "PV_Form", "번호");
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            int i = -1;
            String MTH;
            while (++i < PVNum.Length)
            {
                Cal_RESystem PV = new Cal_RESystem(PVNum[i][0]);
                PV.Load_PVdata();
                PV.Cal_Qf_elec();
                PV.Cal_Battery();
                PV.Cal_fmatch();
                PV.Cal_Qf_pv();

                for (int mth = 0; mth <= 11; mth++)
                {
                    MTH = (mth + 1).ToString() + "월";
                    Program.DB.setValue(DB.type.ProjDB, "PV_Result", "프로젝트번호,프로젝트유형,번호," +
                             "월," +
                             "매칭계수,배터리손실,계통연계형사용량,독립형사용량,최종사용량",
                             "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + PVNum[i][0] + "','" + MTH + "','" +
                             PV.fmatch[mth] + "','" + PV.Qbatt_loss[mth] + "','" + PV.Qf_nutz_linked[mth] + "','" + PV.Qf_nutz_nonlinked[mth] + "','" + PV.Qf_nutz_PV[mth]
                              + "'", "번호,월"); ;
                }
            }
            return true;
        }
        #endregion


        public static bool AltCalc()
        {
            string[] RuleAlt = { "법규_외벽", "법규_지붕" , "법규_최하층바닥" , "법규_커튼월창" , "법규_창호" , "법규_외부출입문" , "법규_전체" };
            Program.DB.deleteTable(DB.type.ProjDB, "Zone_Alt_Result");
            Program.DB.initTable(DB.type.ProjDB, "Zone_Alt_Result");
            Cal_Alt cal = new Cal_Alt();
            for(int  i = 0; i < RuleAlt.Length; i++)
            {
                cal.Calc_RuleAlt(RuleAlt[i]);
            }            
            MessageBox.Show("법규 기반 우선순위 계산되었습니다.");
            return true;
        }
        /////////////////////////////////////////////////////////////////////////////////////

        private Dictionary<string, Delegate> _calculations = new Dictionary<string, Delegate>();
        public static Dictionary<string, Zone> Zones = new Dictionary<string, Zone>();
        public static Dictionary<string, ZoneLight> ZoneLights = new Dictionary<string, ZoneLight>();
        public static Dictionary<string, Zone_HeatingCE> Zone_HeatingCEs = new Dictionary<string, Zone_HeatingCE>();
        public static Dictionary<string, Heating> Heatings = new Dictionary<string, Heating>();
        public static Dictionary<string, AHU> AHUs = new Dictionary<string, AHU>();
        public static Dictionary<string, DHW> DHWs = new Dictionary<string, DHW>();
        public static Dictionary<string, Final> Finals = new Dictionary<string, Final>();

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
        public Zone_HeatingCE getZone_HeatingCE(string ID)
        {
            if (Zone_HeatingCEs.ContainsKey(ID))
            {
                return Zone_HeatingCEs[ID];
            }
            else return null;
        }
        public Heating getHeating(string HeatingNum)
        {
            if (Heatings.ContainsKey(HeatingNum))
            {
                return Heatings[HeatingNum];
            }
            else return null;
        }
        public AHU getAHU(string AHUNum)
        {
            if (AHUs.ContainsKey(AHUNum))
            {
                return AHUs[AHUNum];
            }
            else return null;
        }
        public DHW getDHW(string DHWNum)
        {
            if (DHWs.ContainsKey(DHWNum))
            {
                return DHWs[DHWNum];
            }
            else return null;
        }
        public Final getFinal(string Num)
        {
            if (Finals.ContainsKey(Num))
            {
                return Finals[Num];
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
