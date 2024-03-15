using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    internal class Cal_Alt_Rule
    {
        private static Dictionary<string, Zone> Zones = new Dictionary<string, Zone>();
        public ArrayList zone = new ArrayList();
        public ArrayList zonelight = new ArrayList();
        string[][] 지역구분 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역구분", "");
        String[] HC = { "난방", "냉방" };
        String[] WEWD = { "비이용일", "이용일" };
        String[] MTH = { "1월", "2월", "3월", "4월", "5월", "6월", "7월", "8월", "9월", "10월", "11월", "12월" };
       
        private void CreateZone()
        {
            zone.Clear();
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
            while (++i < zones.Length)
            {
                ZoneLight zonelight1 = new ZoneLight(zones_순번[i]);
                zonelight.Add(zonelight1);
                Zone zone1 = new Zone(zones_순번[i]);
                zone.Add(zone1);
            }
        }
        private void LoadData_InWall(string 검토유형, Zone zone1)
        {
            //존 내벽 정보 가져오기
            String[][] ZoneInW = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,인접존,면적", "존 = '" + zone1.ZoneNum + "' And  외피유형 = '내벽'");
            int i = -1;
            if (ZoneInW.Length > 0)
            {
                while (++i < ZoneInW.Length)
                {
                    double R = (0.1 / 2.3) + 0.13 + 0.13;
                    double U = 1 / R;
                    InWall Inwall = new InWall(ZoneInW[i][0], ZoneInW[i][1], Convert.ToDouble(ZoneInW[i][2]), U);
                    zone1.zoneInWall.Add(Inwall);

                    string 난방냉방, 비이;
                    double thetaiset;
                    for (int hc = 0; hc < 2; hc++)
                    {
                        if (hc == 0) { 난방냉방 = "난방"; thetaiset = zone1.theta_i_h_set; } else { 난방냉방 = "냉방"; thetaiset = zone1.theta_i_c_set; }
                        for (int wewd = 0; wewd < 2; wewd++)
                        {
                            if (wewd == 0) { 비이 = "비이용일"; } else { 비이 = "이용일"; }
                            for (int mth = 0; mth < 12; mth++)
                            {
                                string[][] theta_u = Program.DB.getValue(DB.type.ProjDB, "Zone_Alt_Result", "비냉난방존온도", "번호 = '" + ZoneInW[i][1] + "' and 난방_냉방 = '" + 난방냉방 + "' and 비이용일_이용일 ='" + 비이 + "'and 월 ='" + (mth + 1) + "월' And 검토유형='" + 검토유형 + "'");
                                if (theta_u.Length > 0)
                                {
                                    if (Math.Abs(Convert.ToDouble(theta_u[0][0])) > thetaiset + 4)
                                    { zone1.Zone_HT_Inwall[hc, wewd, mth] += U * Convert.ToDouble(ZoneInW[i][2]); }
                                    else { }
                                }
                                zone1.Zone_HT_tot[hc, wewd, mth] = zone1.Zone_HT_Inwall[hc, wewd, mth];
                            }
                        }
                    }
                }
            }
        }
        private void LoadData_SL(string 검토유형, Zone zone1)
        {
            //존 층간바닥 정보 가져오기
            String[][] ZoneSL = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,인접존,면적", "존 = '" + zone1.ZoneNum + "' And  외피유형 = '층간바닥'");
            int i = -1;
            if (ZoneSL.Length > 0)
            {
                while (++i < ZoneSL.Length)
                {
                    double R = (0.15 / 2.3) + 0.13 + 0.13;
                    double U = 1 / R;
                    Slab slab = new Slab(ZoneSL[i][0], ZoneSL[i][1], Convert.ToDouble(ZoneSL[i][2]), U);
                    zone1.zoneSlab.Add(slab);

                    string 난방냉방, 비이;
                    double thetaiset;
                    for (int hc = 0; hc < 2; hc++)
                    {
                        if (hc == 0) { 난방냉방 = "난방"; thetaiset = zone1.theta_i_h_set; } else { 난방냉방 = "냉방"; thetaiset = zone1.theta_i_c_set; }
                        for (int wewd = 0; wewd < 2; wewd++)
                        {
                            if (wewd == 0) { 비이 = "비이용일"; } else { 비이 = "이용일"; }
                            for (int mth = 0; mth < 12; mth++)
                            {
                                string[][] theta_u = Program.DB.getValue(DB.type.ProjDB, "Zone_Alt_Result", "비냉난방존온도", "번호 = '" + ZoneSL[i][1] + "' and 난방_냉방 = '" + 난방냉방 + "' and 비이용일_이용일 ='" + 비이 + "'and 월 ='" + (mth + 1) + "월' And 검토유형='" + 검토유형 + "'");
                                if (theta_u.Length > 0)
                                {
                                    if (Math.Abs(Convert.ToDouble(theta_u[0][0])) > thetaiset + 4)
                                    { zone1.Zone_HT_Slab[hc, wewd, mth] += U * Convert.ToDouble(ZoneSL[i][2]); }
                                    else { }
                                }
                                zone1.Zone_HT_tot[hc, wewd, mth] = zone1.Zone_HT_tot[hc, wewd, mth] + zone1.Zone_HT_Slab[hc, wewd, mth];
                            }
                        }
                    }
                }
            }
        }
        private void Calc_Lighting(Zone zone1)
        {
            ZoneLight zonelight1 = new ZoneLight(zone1.ZoneNum);
            zonelight1.LoadData_LightGeneral();
            zonelight1.LoadData_LightSystem();
            zonelight1.LoadData_NaturalLight();
            zonelight1.LoadData_Renew();
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

            for (int hc = 0; hc <= 1; hc++)
            {
                for (int wewd = 0; wewd <= 1; wewd++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {
                        if (wewd == 0) { zone1.QI_L[hc, wewd, mth] = 0; }
                        else { zone1.QI_L[hc, wewd, mth] = zonelight1.Zone_Final_kWh[mth]* 1000 / zone1.dwd_mth[mth]; }
                    }
                }
            }
        }
        private void Calc_QT_u(string 검토유형, Zone zone1)
        {
            //내벽 
            if (zone1.zoneInWall.Count > 0)
            {
                double[,,,] QT_u_sink_i = new double[zone1.zoneInWall.Count, 2, 2, 12]; double[,,,] QT_u_source_i = new double[zone1.zoneInWall.Count, 2, 2, 12];
                double[,] zoneInWall_HT = new double[2, zone1.zoneInWall.Count];

                for (int i = 0; i < zone1.zoneInWall.Count; i++)
                {
                    InWall zoneInwall = (InWall)zone1.zoneInWall[i]; //List를 class 객체로 변환 
                    HTCalc htcalc = new HTCalc();

                    String[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "냉난방유무", "존번호 = '" + zoneInwall.SideZone() + "'");
                    if (Value.Length > 0)
                    {
                        if (zone1.zoneHC == "난방")
                        {
                            if (Value[0][0] == "비냉난방" || Value[0][0] == "냉방")
                            {
                                zoneInWall_HT[0, i] = htcalc.Calc(zoneInwall.U(), zoneInwall.Area());
                            }
                            else { zoneInWall_HT[0, i] = 0; }
                        }
                        else if (zone1.zoneHC == "냉방")
                        {
                            if (Value[0][0] == "비냉난방" || Value[0][0] == "난방")
                            {
                                zoneInWall_HT[1, i] = htcalc.Calc(zoneInwall.U(), zoneInwall.Area());
                            }
                            else { zoneInWall_HT[1, i] = 0; }
                        }
                        else if (zone1.zoneHC == "냉난방")
                        {
                            if (Value[0][0] == "비냉난방" || Value[0][0] == "냉방")
                            {
                                zoneInWall_HT[0, i] = htcalc.Calc(zoneInwall.U(), zoneInwall.Area());
                            }
                            else { zoneInWall_HT[0, i] = 0; }

                            if (Value[0][0] == "비냉난방" || Value[0][0] == "난방")
                            {
                                zoneInWall_HT[1, i] = htcalc.Calc(zoneInwall.U(), zoneInwall.Area());
                            }
                            else { zoneInWall_HT[1, i] = 0; }
                        }
                        else
                        {
                            zoneInWall_HT[0, i] = 0;
                            zoneInWall_HT[1, i] = 0;
                        }

                    }


                    String 난방냉방, 비이;
                    String[][] theta_u;
                    QTCalc qtcalc = new QTCalc();


                    for (int hc = 0; hc < 2; hc++)
                    {
                        if (hc == 0) { 난방냉방 = "난방"; } else { 난방냉방 = "냉방"; }

                        for (int wewd = 0; wewd < 2; wewd++)
                        {
                            if (wewd == 0) { 비이 = "비이용일"; } else { 비이 = "이용일"; }

                            for (int mth = 0; mth < 12; mth++)
                            {
                                theta_u = Program.DB.getValue(DB.type.ProjDB, "Zone_Alt_Result", "비냉난방존온도", "번호 = '" + zoneInwall.SideZone() + "' and 난방_냉방 = '" + 난방냉방 + "' and 비이용일_이용일 ='" + 비이 + "' and 월 ='" + (mth + 1) + "월' And 검토유형 ='"+검토유형+"'");

                                if (theta_u.Length > 0 && theta_u[0][0] != "")
                                {
                                    if (zone1.theta_i[hc, wewd, mth] > Convert.ToDouble(theta_u[0][0]))
                                    {
                                        QT_u_sink_i[i, hc, wewd, mth] = qtcalc.Calc_sink(Convert.ToDouble(theta_u[0][0]), zone1.theta_i[hc, wewd, mth], zoneInWall_HT[hc, i]);
                                        QT_u_source_i[i, hc, wewd, mth] = 0;
                                    }
                                    else
                                    {
                                        QT_u_source_i[i, hc, wewd, mth] = qtcalc.Calc_source(Convert.ToDouble(theta_u[0][0]), zone1.theta_i[hc, wewd, mth], zoneInWall_HT[hc, i]);
                                        QT_u_sink_i[i, hc, wewd, mth] = 0;
                                    }
                                }
                                else
                                {
                                    QT_u_source_i[i, hc, wewd, mth] = 0;
                                    QT_u_sink_i[i, hc, wewd, mth] = 0;
                                }

                                zone1.QT_u_sink[hc, wewd, mth] += QT_u_sink_i[i, hc, wewd, mth];
                                zone1.QT_u_source[hc, wewd, mth] += QT_u_source_i[i, hc, wewd, mth];
                            }
                        }
                    }
                    zone1.QT_u_sink_max += zoneInWall_HT[0, i] * (zone1.theta_i_h_min - (zone1.theta_i_h_min - 0.5 * (zone1.theta_i_h_min - zone1.theta_e_min)));

                    if (zone1.theta_i_c_max_d > (zone1.theta_i_c_max_d - 0.5 * (zone1.theta_i_c_max_d - zone1.theta_e_max)))
                    { zone1.QT_u_sink_Cmax += (zoneInWall_HT[1, i] * (zone1.theta_i_c_max_d - (zone1.theta_i_c_max_d - 0.5 * (zone1.theta_i_c_max_d - zone1.theta_e_max)))); }
                    else { zone1.QT_u_source_Cmax += (zoneInWall_HT[1, i] * ((zone1.theta_i_c_max_d - 0.5 * (zone1.theta_i_c_max_d - zone1.theta_e_max)) - zone1.theta_i_c_max_d)); }


                }
            }

            //층간바닥
            if (zone1.zoneSlab.Count > 0)
            {
                double[,,,] QT_u_sink_i = new double[zone1.zoneSlab.Count, 2, 2, 12]; double[,,,] QT_u_source_i = new double[zone1.zoneSlab.Count, 2, 2, 12];
                double[,] zoneSlab_HT = new double[2, zone1.zoneSlab.Count];

                for (int i = 0; i < zone1.zoneSlab.Count; i++)
                {
                    Slab zoneslab = (Slab)zone1.zoneSlab[i]; //List를 class 객체로 변환 
                    HTCalc htcalc = new HTCalc();

                    String[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "냉난방유무", "존번호 = '" + zoneslab.SideZone() + "'");
                    if (Value.Length > 0)
                    {
                        if (zone1.zoneHC == "난방")
                        {
                            if (Value[0][0] == "비냉난방" || Value[0][0] == "냉방")
                            {
                                zoneSlab_HT[0, i] = htcalc.Calc(zoneslab.U(), zoneslab.Area());
                            }
                            else { zoneSlab_HT[0, i] = 0; }
                        }
                        else if (zone1.zoneHC == "냉방")
                        {
                            if (Value[0][0] == "비냉난방" || Value[0][0] == "난방")
                            {
                                zoneSlab_HT[1, i] = htcalc.Calc(zoneslab.U(), zoneslab.Area());
                            }
                            else { zoneSlab_HT[1, i] = 0; }
                        }
                        else if (zone1.zoneHC == "냉난방")
                        {
                            if (Value[0][0] == "비냉난방" || Value[0][0] == "냉방")
                            {
                                zoneSlab_HT[0, i] = htcalc.Calc(zoneslab.U(), zoneslab.Area());
                            }
                            else { zoneSlab_HT[0, i] = 0; }

                            if (Value[0][0] == "비냉난방" || Value[0][0] == "난방")
                            {
                                zoneSlab_HT[1, i] = htcalc.Calc(zoneslab.U(), zoneslab.Area());
                            }
                            else { zoneSlab_HT[1, i] = 0; }
                        }
                        else
                        {
                            zoneSlab_HT[0, i] = 0;
                            zoneSlab_HT[1, i] = 0;
                        }
                    }


                    String 난방냉방, 비이;
                    String[][] theta_u;
                    QTCalc qtcalc = new QTCalc();


                    for (int hc = 0; hc < 2; hc++)
                    {
                        if (hc == 0) { 난방냉방 = "난방"; } else { 난방냉방 = "냉방"; }

                        for (int wewd = 0; wewd < 2; wewd++)
                        {
                            if (wewd == 0) { 비이 = "비이용일"; } else { 비이 = "이용일"; }

                            for (int mth = 0; mth < 12; mth++)
                            {
                                theta_u = Program.DB.getValue(DB.type.ProjDB, "Zone_Alt_Result", "비냉난방존온도", "번호 = '" + zoneslab.SideZone() + "' and 난방_냉방 = '" + 난방냉방 + "' and 비이용일_이용일 ='" + 비이 + "' AND 월 ='" + (mth + 1) + "월' and 검토유형 ='"+검토유형+"'");


                                if (theta_u.Length > 0 && theta_u[0][0] != "")
                                {
                                    if (zone1.theta_i[hc, wewd, mth] > Convert.ToDouble(theta_u[0][0]))
                                    {
                                        QT_u_sink_i[i, hc, wewd, mth] = qtcalc.Calc_sink(Convert.ToDouble(theta_u[0][0]), zone1.theta_i[hc, wewd, mth], zoneSlab_HT[hc, i]);
                                        QT_u_source_i[i, hc, wewd, mth] = 0;
                                    }
                                    else
                                    {
                                        QT_u_source_i[i, hc, wewd, mth] = qtcalc.Calc_source(Convert.ToDouble(theta_u[0][0]), zone1.theta_i[hc, wewd, mth], zoneSlab_HT[hc, i]);
                                        QT_u_sink_i[i, hc, wewd, mth] = 0;
                                    }
                                }
                                else
                                {
                                    QT_u_source_i[i, hc, wewd, mth] = 0;
                                    QT_u_sink_i[i, hc, wewd, mth] = 0;
                                }

                                zone1.QT_u_sink[hc, wewd, mth] += QT_u_sink_i[i, hc, wewd, mth];
                                zone1.QT_u_source[hc, wewd, mth] += QT_u_source_i[i, hc, wewd, mth];
                            }
                        }
                    }
                    zone1.QT_u_sink_max += zoneSlab_HT[0, i] * (zone1.theta_i_h_min - (zone1.theta_i_h_min - 0.5 * (zone1.theta_i_h_min - zone1.theta_e_min)));

                    if (zone1.theta_i_c_max_d > (zone1.theta_i_c_max_d - 0.5 * (zone1.theta_i_c_max_d - zone1.theta_e_max)))
                    { zone1.QT_u_sink_Cmax += (zoneSlab_HT[1, i] * (zone1.theta_i_c_max_d - (zone1.theta_i_c_max_d - 0.5 * (zone1.theta_i_c_max_d - zone1.theta_e_max)))); }
                    else { zone1.QT_u_source_Cmax += zoneSlab_HT[1, i] * ((zone1.theta_i_c_max_d - 0.5 * (zone1.theta_i_c_max_d - zone1.theta_e_max)) - zone1.theta_i_c_max_d); }

                }
            }
        }
        public void Zone_LoadData(string 검토유형, Zone zone1)
        {
            zone1.검토유형[0][0] = 검토유형;
            zone1.LoadData_ZoneGeneral();
            zone1.LoadData_Ventil();
            LoadData_InWall(검토유형, zone1);
            LoadData_SL(검토유형, zone1);
            zone1.LoadData_Wall();
            zone1.LoadData_Roof();
            zone1.LoadData_Floor(); 
            zone1.LoadData_GWall();
            zone1.LoadData_Door();
            zone1.LoadData_Win();
            zone1.LoadData_CW();
            #region ZoneQI_L
            Calc_Lighting(zone1);
            #endregion
        }
        public void Zone_CalcTotal(string 검토유형, Zone zone1)
        {           
            zone1.ZoneHT();
            zone1.ZoneHV();
            zone1.Zonetao();
            zone1.Zonethetai();
            Calc_QT_u(검토유형, zone1);
            zone1.ZoneQT();
            zone1.ZoneQV();
            zone1.ZoneQSop(zone1.ZoneNum);
            zone1.ZoneQStr(zone1.ZoneNum);
            zone1.ZoneQ_DHU();            
            zone1.ZoneQI();
            zone1.Zone_Theta_U();
            zone1.Zoneeta();
            zone1.ZoneQb();
            zone1.ZoneQmax();
            for (int hc = 0; hc <= 1; hc++)
            {
                for (int wewd = 0; wewd <= 1; wewd++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {

                        Program.DB.setValue(DB.type.ProjDB, "Zone_Alt_Result", "검토유형,번호,이름," +
                             "난방_냉방,비이용일_이용일,월," +
                             "Qb_day," +
                             "Qb_mth," +
                             "Qb_a,Q_max, t_max,비냉난방존온도",
                              "'" + zone1.검토유형[0][0] + "','" + zone1.ZoneNum + "','" + zone1.zoneName + "','" +
                              HC[hc] + "','" + WEWD[wewd] + "','" + MTH[mth] + "','" +
                              zone1.Qb_day[hc, wewd, mth].ToString() + "','" +
                              zone1.Qb_mth[hc, wewd, mth].ToString() + "','" +
                              zone1.Qb_a[hc].ToString() + "','" + zone1.Q_max[hc].ToString() + "','" + zone1.t_max[hc, mth].ToString() + "','" +
                              zone1.Theta_U[hc, wewd, mth].ToString()
                              + "'", "번호,난방_냉방,비이용일_이용일,월,검토유형");
                    }
                }
            }
        }
       
        public void Calc_Alt_Wall()
        {
            CreateZone();
            for (int k = 0; k < zone.Count; k++)
            {
                Zone zone1 = (Zone)zone[k];
                Zone_LoadData("법규_외벽", zone1);

                #region LoadData_Wall
                zone1.zoneWall.Clear();
                zone1.zoneGWall.Clear();
                String[][] ZoneW = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.법규열관류율,b.흡수율,b.직접간접,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "' And  NOT b.직접간접 = '지면'");
                if (ZoneW.Length > 0)
                {
                    int i = -1;
                    while (++i < ZoneW.Length)
                    {                        
                        Wall wall = new Wall(ZoneW[i][0], ZoneW[i][2], Convert.ToDouble(ZoneW[i][1]), Convert.ToDouble(ZoneW[0][3]), Convert.ToDouble(ZoneW[i][4]), ZoneW[i][5], ZoneW[i][6], ZoneW[i][7]);
                        zone1.zoneWall.Add(wall);
                    }
                }
                String[][] ZoneG = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.법규열관류율,b.직접간접 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "' And  b.직접간접 = '지면'");
                if (ZoneG.Length > 0)
                {
                    int i = -1;
                    while (++i < ZoneG.Length)
                    {
                        double fx_f = 1;
                        if (Convert.ToDouble(ZoneG[0][3]) >= 3)
                        { fx_f = 0.35; }
                        else if (Convert.ToDouble(ZoneG[0][3]) >= 1)
                        { fx_f = 0.55; }
                        else if (Convert.ToDouble(ZoneG[0][3]) > 0.3)
                        { fx_f = 0.65; }
                        else { fx_f = 0.75; }
                        break;

                        GWall gwall = new GWall(ZoneG[i][0], ZoneG[i][2], Convert.ToDouble(ZoneG[i][1]), Convert.ToDouble(ZoneG[0][3]), fx_f);
                        zone1.zoneGWall.Add(gwall);
                    }
                }
                #endregion

                Zone_CalcTotal("법규_외벽", zone1);
            }
        }
        public void Calc_Alt_Roof()
        {
            CreateZone();
            for (int k = 0; k < zone.Count; k++)
            {
                Zone zone1 = (Zone)zone[k];
                Zone_LoadData("법규_지붕", zone1);

                #region LoadData_Roof
                zone1.zoneRoof.Clear();
                String[][] ZoneR = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.법규열관류율,b.흡수율,b.직접간접,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionRoof AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
                if (ZoneR.Length > 0)
                {
                    int i = -1;
                    while (++i < ZoneR.Length)
                    {
                        Roof roof = new Roof(ZoneR[i][0], ZoneR[i][2], Convert.ToDouble(ZoneR[i][1]), Convert.ToDouble(ZoneR[0][3]), Convert.ToDouble(ZoneR[i][4]), ZoneR[i][5], ZoneR[i][6], ZoneR[i][7]);
                        zone1.zoneRoof.Add(roof);
                    }
                }
                #endregion

                Zone_CalcTotal("법규_지붕", zone1);
            }
        }
        public void Calc_Alt_Floor()
        {
            CreateZone();
            for (int k = 0; k < zone.Count; k++)
            {
                Zone zone1 = (Zone)zone[k];
                Zone_LoadData("법규_최하층바닥", zone1);

                #region LoadData_Floor
                zone1.zoneFloor.Clear();
                String[][] ZoneF = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.법규열관류율,b.직접간접,b.기초설치 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionFloor AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
                if (ZoneF.Length > 0)
                {
                    int i = -1;
                    while (++i < ZoneF.Length)
                    {
                        double fx_f = 1;
                        switch (ZoneF[i][5].ToString())
                        {
                            case "지면위":
                                {
                                    if (Convert.ToDouble(ZoneF[0][3]) >= 3)
                                    { fx_f = 0.3; }
                                    else if (Convert.ToDouble(ZoneF[0][3]) >= 1)
                                    { fx_f = 0.55; }
                                    else if (Convert.ToDouble(ZoneF[0][3]) > 0.3)
                                    { fx_f = 0.7; }
                                    else { fx_f = 0.8; }
                                    break;
                                }
                            case "단열지하":
                                {
                                    if (Convert.ToDouble(ZoneF[0][3]) >= 3)
                                    { fx_f = 0.2; }
                                    else if (Convert.ToDouble(ZoneF[0][3]) >= 1)
                                    { fx_f = 0.45; }
                                    else if (Convert.ToDouble(ZoneF[0][3]) > 0.3)
                                    { fx_f = 0.55; }
                                    else { fx_f = 0.7; }
                                    break;
                                }
                            case "비단열지하":
                                {
                                    if (Convert.ToDouble(ZoneF[0][3]) >= 3)
                                    { fx_f = 0.45; }
                                    else if (Convert.ToDouble(ZoneF[0][3]) >= 1)
                                    { fx_f = 0.75; }
                                    else if (Convert.ToDouble(ZoneF[0][3]) > 0.3)
                                    { fx_f = 0.8; }
                                    else { fx_f = 0.85; }
                                    break;
                                }
                        }
                        Floor floor = new Floor(ZoneF[i][0], ZoneF[i][2], Convert.ToDouble(ZoneF[i][1]), Convert.ToDouble(ZoneF[0][3]), ZoneF[i][5], fx_f);
                        zone1.zoneFloor.Add(floor);
                    }
                }
                #endregion

                Zone_CalcTotal("법규_최하층바닥", zone1);
            }
        }
        public void Calc_Alt_Win()
        {
            CreateZone();
            for (int k = 0; k < zone.Count; k++)
            {
                Zone zone1 = (Zone)zone[k];
                Zone_LoadData("법규_창호", zone1);

                #region LoadData_Win
                zone1.zoneWin.Clear();
                String[][] ZoneWin = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.법규열관류율,b.설치열교가산치,b.창호유효열관류율,b.유리면적비,b.상위창호번호,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
                if (ZoneWin.Length > 0)
                {
                    int i = -1;
                    while (++i < ZoneWin.Length)
                    {
                        String[][] ZoneWin_P = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "직접간접,태양열취득률,빛투과율", "번호='" + ZoneWin[i][7] + "'");
                        string[][] Blind = Program.DB.getValue(DB.type.ProjDB, "Blind_3D", "차양포함태양열취득률,차양포함빛투과율", "번호='" + ZoneWin[i][0] + "'");
                        if (ZoneWin_P.Length > 0)
                        {
                            if (Blind.Length > 0)
                            {
                                Window win = new Window(ZoneWin[i][0], ZoneWin[i][7], ZoneWin[i][2], Convert.ToDouble(ZoneWin[i][1]), Convert.ToDouble(ZoneWin[i][3]), Convert.ToDouble(ZoneWin[i][4]), ZoneWin_P[0][0], Convert.ToDouble(ZoneWin[i][6]), Convert.ToDouble(ZoneWin_P[0][1]), Convert.ToDouble(ZoneWin_P[0][2]), Convert.ToDouble(Blind[0][0]), Convert.ToDouble(Blind[0][1]), ZoneWin[i][8], ZoneWin[i][9]);
                                zone1.zoneWin.Add(win);
                            }
                            else
                            {
                                Window win = new Window(ZoneWin[i][0], ZoneWin[i][7], ZoneWin[i][2], Convert.ToDouble(ZoneWin[i][1]), Convert.ToDouble(ZoneWin[i][3]), Convert.ToDouble(ZoneWin[i][4]), ZoneWin_P[0][0], Convert.ToDouble(ZoneWin[i][6]), Convert.ToDouble(ZoneWin_P[0][1]), Convert.ToDouble(ZoneWin_P[0][2]), 0, 0, ZoneWin[i][8], ZoneWin[i][9]);
                                zone1.zoneWin.Add(win);
                            }
                        }
                    }
                }
                #endregion

                Zone_CalcTotal("법규_창호", zone1);
            }           
        }
        public void Calc_Alt_CW()
        {
            CreateZone();
            for (int k = 0; k < zone.Count; k++)
            {
                Zone zone1 = (Zone)zone[k];
                Zone_LoadData("법규_커튼월창", zone1);

                #region LoadDat_CW
                zone1.zoneCW.Clear();
                String[][] ZoneCW = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,면적,커튼월부위,구조체번호,방위,기울기", "존 = '" + zone1.ZoneNum + "' AND 외피유형 = '커튼월창'");
                if (ZoneCW.Length > 0)
                {
                    int i = -1;
                    while (++i < ZoneCW.Length)
                    { //유리부분면적,유리부분열관류율,유리부분유리면적비,태양열취득률,빛투과율, 패널부분 면적, 패널부분흡수율, 출입문부분면적, 출입문부분열관류율,출입문부분유리면적비, 출입문부분태양열취득률, 출입문부분빛투과율, 커튼월창면적, 설치열교가산치
                       
                        if (ZoneCW[i][2] == "유리부분")
                        {
                            String[][] CW_g = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "법규유리부분열관류율,유리부분유리면적비,태양열취득률,빛투과율,설치열교가산치", "번호 = '" + ZoneCW[i][3] + "'");
                            string[][] Blind = Program.DB.getValue(DB.type.ProjDB, "Blind_3D", "차양포함태양열취득률,차양포함빛투과율", "번호='" + ZoneCW[i][3] + "'");
                            if (CW_g.Length > 0)
                            {
                                if (Blind.Length > 0)

                                {
                                    CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_g[0][0]), Convert.ToDouble(CW_g[0][1]), Convert.ToDouble(CW_g[0][2]), Convert.ToDouble(Blind[0][0]), Convert.ToDouble(CW_g[0][3]), Convert.ToDouble(Blind[0][1]), 0, 0, 0, 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_g[0][4]), ZoneCW[i][4], ZoneCW[i][5], "유리부분");
                                    zone1.zoneCW.Add(cw);
                                }
                                else
                                {
                                    CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_g[0][0]), Convert.ToDouble(CW_g[0][1]), Convert.ToDouble(CW_g[0][2]), 0, Convert.ToDouble(CW_g[0][3]), 0, 0, 0, 0, 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_g[0][4]), ZoneCW[i][4], ZoneCW[i][5], "유리부분");
                                    zone1.zoneCW.Add(cw);
                                }
                            }

                        }
                        else if (ZoneCW[i][2] == "패널부분")
                        {
                            String[][] CW_p = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "법규패널부분열관류율,패널흡수율,설치열교가산치", "번호 = '" + ZoneCW[i][3] + "'");
                            if (CW_p.Length > 0)
                            {
                                CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], 0, 0, 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_p[0][0]), Convert.ToDouble(CW_p[0][1]), 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_p[0][2]), ZoneCW[i][4], ZoneCW[i][5], "패널부분");
                                zone1.zoneCW.Add(cw);
                            }
                        }
                        else
                        {
                            String[][] CW_d = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "법규출입문부분열관류율,출입문부분유리면적비,출입문태양열취득률,출입문빛투과율,설치열교가산치", "번호 = '" + ZoneCW[i][3] + "'");
                            if (CW_d.Length > 0)
                            {
                                CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_d[0][0]), Convert.ToDouble(CW_d[0][1]), Convert.ToDouble(CW_d[0][2]), Convert.ToDouble(CW_d[0][3]), Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_d[0][4]), ZoneCW[i][4], ZoneCW[i][5], "출입문부분");
                                zone1.zoneCW.Add(cw);
                            }
                        }
                    }
                }
                #endregion

                Zone_CalcTotal("법규_커튼월창", zone1);
            }          
        }
        public void Calc_Alt_Door()
        {
            CreateZone();
            for (int k = 0; k < zone.Count; k++)
            {
                Zone zone1 = (Zone)zone[k];
                Zone_LoadData("법규_외부출입문", zone1);

                #region LoadData_Door
                zone1.zoneDoor.Clear();
                String[][] ZoneD = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.법규열관류율,b.흡수율,b.직접간접,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionDoor AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");

                if (ZoneD.Length > 0)
                {
                    int i = -1;
                    while (++i < ZoneD.Length)
                    {                       
                        Door door = new Door(ZoneD[i][0], ZoneD[i][2], Convert.ToDouble(ZoneD[i][1]), Convert.ToDouble(ZoneD[i][3]), Convert.ToDouble(ZoneD[i][4]), ZoneD[i][5], ZoneD[i][6], ZoneD[i][7]);
                        zone1.zoneDoor.Add(door);
                    }
                }
                #endregion

                Zone_CalcTotal("법규_외부출입문", zone1);
            }            
        }
        public void Calc_Alt_All()
        {

            CreateZone();
            for (int k = 0; k < zone.Count; k++)
            {
                Zone zone1 = (Zone)zone[k];
                Zone_LoadData("법규_전체", zone1);

                #region LoadData_Wall
                zone1.zoneWall.Clear();
                zone1.zoneGWall.Clear();
                String[][] ZoneW = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.법규열관류율,b.흡수율,b.직접간접,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "' And  NOT b.직접간접 = '지면'");
                if (ZoneW.Length > 0)
                {
                    int i = -1;
                    while (++i < ZoneW.Length)
                    {
                        Wall wall = new Wall(ZoneW[i][0], ZoneW[i][2], Convert.ToDouble(ZoneW[i][1]), Convert.ToDouble(ZoneW[0][3]), Convert.ToDouble(ZoneW[i][4]), ZoneW[i][5], ZoneW[i][6], ZoneW[i][7]);
                        zone1.zoneWall.Add(wall);
                    }
                }
                String[][] ZoneG = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.법규열관류율,b.직접간접 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "' And  b.직접간접 = '지면'");
                if (ZoneG.Length > 0)
                {
                    int i = -1;
                    while (++i < ZoneG.Length)
                    {
                        double fx_f = 1;
                        if (Convert.ToDouble(ZoneG[0][3]) >= 3)
                        { fx_f = 0.35; }
                        else if (Convert.ToDouble(ZoneG[0][3]) >= 1)
                        { fx_f = 0.55; }
                        else if (Convert.ToDouble(ZoneG[0][3]) > 0.3)
                        { fx_f = 0.65; }
                        else { fx_f = 0.75; }
                        break;

                        GWall gwall = new GWall(ZoneG[i][0], ZoneG[i][2], Convert.ToDouble(ZoneG[i][1]), Convert.ToDouble(ZoneG[0][3]), fx_f);
                        zone1.zoneGWall.Add(gwall);
                    }
                }
                #endregion
                #region LoadData_Roof
                zone1.zoneRoof.Clear();
                String[][] ZoneR = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.법규열관류율,b.흡수율,b.직접간접,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionRoof AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
                if (ZoneR.Length > 0)
                {
                    int i = -1;
                    while (++i < ZoneR.Length)
                    {
                        Roof roof = new Roof(ZoneR[i][0], ZoneR[i][2], Convert.ToDouble(ZoneR[i][1]), Convert.ToDouble(ZoneR[0][3]), Convert.ToDouble(ZoneR[i][4]), ZoneR[i][5], ZoneR[i][6], ZoneR[i][7]);
                        zone1.zoneRoof.Add(roof);
                    }
                }
                #endregion
                #region LoadData_Floor
                zone1.zoneFloor.Clear();
                String[][] ZoneF = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.법규열관류율,b.직접간접,b.기초설치 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionFloor AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
                if (ZoneF.Length > 0)
                {
                    int i = -1;
                    while (++i < ZoneF.Length)
                    {
                        double fx_f = 1;
                        switch (ZoneF[i][5].ToString())
                        {
                            case "지면위":
                                {
                                    if (Convert.ToDouble(ZoneF[0][3]) >= 3)
                                    { fx_f = 0.3; }
                                    else if (Convert.ToDouble(ZoneF[0][3]) >= 1)
                                    { fx_f = 0.55; }
                                    else if (Convert.ToDouble(ZoneF[0][3]) > 0.3)
                                    { fx_f = 0.7; }
                                    else { fx_f = 0.8; }
                                    break;
                                }
                            case "단열지하":
                                {
                                    if (Convert.ToDouble(ZoneF[0][3]) >= 3)
                                    { fx_f = 0.2; }
                                    else if (Convert.ToDouble(ZoneF[0][3]) >= 1)
                                    { fx_f = 0.45; }
                                    else if (Convert.ToDouble(ZoneF[0][3]) > 0.3)
                                    { fx_f = 0.55; }
                                    else { fx_f = 0.7; }
                                    break;
                                }
                            case "비단열지하":
                                {
                                    if (Convert.ToDouble(ZoneF[0][3]) >= 3)
                                    { fx_f = 0.45; }
                                    else if (Convert.ToDouble(ZoneF[0][3]) >= 1)
                                    { fx_f = 0.75; }
                                    else if (Convert.ToDouble(ZoneF[0][3]) > 0.3)
                                    { fx_f = 0.8; }
                                    else { fx_f = 0.85; }
                                    break;
                                }
                        }
                        Floor floor = new Floor(ZoneF[i][0], ZoneF[i][2], Convert.ToDouble(ZoneF[i][1]), Convert.ToDouble(ZoneF[0][3]), ZoneF[i][5], fx_f);
                        zone1.zoneFloor.Add(floor);
                    }
                }
                #endregion
                #region LoadData_Win
                zone1.zoneWin.Clear();
                String[][] ZoneWin = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.법규열관류율,b.설치열교가산치,b.창호유효열관류율,b.유리면적비,b.상위창호번호,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
                if (ZoneWin.Length > 0)
                {
                    int i = -1;
                    while (++i < ZoneWin.Length)
                    {
                        String[][] ZoneWin_P = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "직접간접,태양열취득률,빛투과율", "번호='" + ZoneWin[i][7] + "'");
                        string[][] Blind = Program.DB.getValue(DB.type.ProjDB, "Blind_3D", "차양포함태양열취득률,차양포함빛투과율", "번호='" + ZoneWin[i][0] + "'");
                        if (ZoneWin_P.Length > 0)
                        {
                            if (Blind.Length > 0)
                            {
                                Window win = new Window(ZoneWin[i][0], ZoneWin[i][7], ZoneWin[i][2], Convert.ToDouble(ZoneWin[i][1]), Convert.ToDouble(ZoneWin[i][3]), Convert.ToDouble(ZoneWin[i][4]), ZoneWin_P[0][0], Convert.ToDouble(ZoneWin[i][6]), Convert.ToDouble(ZoneWin_P[0][1]), Convert.ToDouble(ZoneWin_P[0][2]), Convert.ToDouble(Blind[0][0]), Convert.ToDouble(Blind[0][1]), ZoneWin[i][8], ZoneWin[i][9]);
                                zone1.zoneWin.Add(win);
                            }
                            else
                            {
                                Window win = new Window(ZoneWin[i][0], ZoneWin[i][7], ZoneWin[i][2], Convert.ToDouble(ZoneWin[i][1]), Convert.ToDouble(ZoneWin[i][3]), Convert.ToDouble(ZoneWin[i][4]), ZoneWin_P[0][0], Convert.ToDouble(ZoneWin[i][6]), Convert.ToDouble(ZoneWin_P[0][1]), Convert.ToDouble(ZoneWin_P[0][2]), 0, 0, ZoneWin[i][8], ZoneWin[i][9]);
                                zone1.zoneWin.Add(win);
                            }
                        }
                    }
                }
                #endregion
                #region LoadDat_CW
                zone1.zoneCW.Clear();
                String[][] ZoneCW = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,면적,커튼월부위,구조체번호,방위,기울기", "존 = '" + zone1.ZoneNum + "' AND 외피유형 = '커튼월창'");
                if (ZoneCW.Length > 0)
                {
                    int i = -1;
                    while (++i < ZoneCW.Length)
                    { //유리부분면적,유리부분열관류율,유리부분유리면적비,태양열취득률,빛투과율, 패널부분 면적, 패널부분흡수율, 출입문부분면적, 출입문부분열관류율,출입문부분유리면적비, 출입문부분태양열취득률, 출입문부분빛투과율, 커튼월창면적, 설치열교가산치

                        if (ZoneCW[i][2] == "유리부분")
                        {
                            String[][] CW_g = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "법규유리부분열관류율,유리부분유리면적비,태양열취득률,빛투과율,설치열교가산치", "번호 = '" + ZoneCW[i][3] + "'");
                            string[][] Blind = Program.DB.getValue(DB.type.ProjDB, "Blind_3D", "차양포함태양열취득률,차양포함빛투과율", "번호='" + ZoneCW[i][3] + "'");
                            if (CW_g.Length > 0)
                            {
                                if (Blind.Length > 0)

                                {
                                    CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_g[0][0]), Convert.ToDouble(CW_g[0][1]), Convert.ToDouble(CW_g[0][2]), Convert.ToDouble(Blind[0][0]), Convert.ToDouble(CW_g[0][3]), Convert.ToDouble(Blind[0][1]), 0, 0, 0, 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_g[0][4]), ZoneCW[i][4], ZoneCW[i][5], "유리부분");
                                    zone1.zoneCW.Add(cw);
                                }
                                else
                                {
                                    CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_g[0][0]), Convert.ToDouble(CW_g[0][1]), Convert.ToDouble(CW_g[0][2]), 0, Convert.ToDouble(CW_g[0][3]), 0, 0, 0, 0, 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_g[0][4]), ZoneCW[i][4], ZoneCW[i][5], "유리부분");
                                    zone1.zoneCW.Add(cw);
                                }
                            }

                        }
                        else if (ZoneCW[i][2] == "패널부분")
                        {
                            String[][] CW_p = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "법규패널부분열관류율,패널흡수율,설치열교가산치", "번호 = '" + ZoneCW[i][3] + "'");
                            if (CW_p.Length > 0)
                            {
                                CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], 0, 0, 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_p[0][0]), Convert.ToDouble(CW_p[0][1]), 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_p[0][2]), ZoneCW[i][4], ZoneCW[i][5], "패널부분");
                                zone1.zoneCW.Add(cw);
                            }
                        }
                        else
                        {
                            String[][] CW_d = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "법규출입문부분열관류율,출입문부분유리면적비,출입문태양열취득률,출입문빛투과율,설치열교가산치", "번호 = '" + ZoneCW[i][3] + "'");
                            if (CW_d.Length > 0)
                            {
                                CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_d[0][0]), Convert.ToDouble(CW_d[0][1]), Convert.ToDouble(CW_d[0][2]), Convert.ToDouble(CW_d[0][3]), Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_d[0][4]), ZoneCW[i][4], ZoneCW[i][5], "출입문부분");
                                zone1.zoneCW.Add(cw);
                            }
                        }
                    }
                }
                #endregion
                #region LoadData_Door
                zone1.zoneDoor.Clear();
                String[][] ZoneD = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.법규열관류율,b.흡수율,b.직접간접,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionDoor AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");

                if (ZoneD.Length > 0)
                {
                    int i = -1;
                    while (++i < ZoneD.Length)
                    {
                        Door door = new Door(ZoneD[i][0], ZoneD[i][2], Convert.ToDouble(ZoneD[i][1]), Convert.ToDouble(ZoneD[i][3]), Convert.ToDouble(ZoneD[i][4]), ZoneD[i][5], ZoneD[i][6], ZoneD[i][7]);
                        zone1.zoneDoor.Add(door);
                    }
                }
                #endregion

                Zone_CalcTotal("법규_전체", zone1);
            }
        }          
    }
}
