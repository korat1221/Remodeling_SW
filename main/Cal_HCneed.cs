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

    internal class Zone
    {
        public String ZoneNum;
        public String zoneName;
        public String zoneUsage, zoneHC, Mode_night, Mode_we;
        public double Peope_Num, t_c_op_d, theta_i_h_set, theta_i_c_set, dtheta_i_NA, Fx, Fx_Floor, Fx_GWall, theta_s_c, theta_i_h_min, theta_i_c_max, theta_SUP_Wi;
        public double twd_d, th_op_d_we, th_op_d, dwd_a;
        public double zoneArea, zoneHeight;
        public double qI_p, qI_fac, Cwirk_A;
        public double VA_we, VA_wd, n50, e, f, Vmech_SUP_we, Vmech_SUP_wd, Vmech_ETA_we, Vmech_ETA_wd, xi_c_set, xi_h_set, H_winter, H_summer, Vmech_SUP_z, Vmech_ETA_z, ρacp_a;
        public double[] eta_V_mech = new double[2], eta_χV_mech = new double[2];
        public string SelectHRV;
        public ArrayList zoneWall = new ArrayList();
        public ArrayList zoneRoof = new ArrayList();
        public ArrayList zoneFloor = new ArrayList();
        public ArrayList zoneGWall = new ArrayList();
        public ArrayList zoneDoor = new ArrayList();
        public ArrayList zoneWin = new ArrayList();
        public ArrayList zoneCW = new ArrayList();
        public ArrayList zoneInWall = new ArrayList();
        public ArrayList zoneSlab = new ArrayList();
        public double[,,] Zone_HT_tot = new double [2,2,12];
        public double Zone_HT_Wall, Zone_HT_Roof, Zone_HT_Floor, Zone_HT_GWall, Zone_HT_Door, Zone_HT_Win, Zone_HT_CW;
        public double[,,]Zone_HT_Inwall = new double[2,2,12], Zone_HT_Slab = new double [2,2,12];
        public double Zone_HT_Di_Wall, Zone_HT_Indi_Wall, Zone_HT_Di_Roof, Zone_HT_Indi_Roof, Zone_HT_Di_Win, Zone_HT_Indi_Win, Zone_HT_Di_Door, Zone_HT_Indi_Door;
        public double Zone_HT_TB_tot, Zone_HT_TB_Wall, Zone_HT_TB_Roof, Zone_HT_TB_Floor, Zone_HT_TB_GWall, Zone_HT_TB_Win, Zone_HT_TB_Door, Zone_HT_TB_CW;
        public double[] nmech = new double[2]; public double[] nz = new double[2]; public double[] ninf = new double[2]; public double[] nwin = new double[2];//[비이용일/이용일] = [we/wd]=[0/1]
        public double[] Zone_HV_tot = new double[2], Zone_HV_inf = new double[2], Zone_HV_win = new double[2], Zone_HV_z = new double[2], Zone_HV_mech = new double[2]; public double HV_tot_max; //[비이용일/이용일] = [we/wd]=[0/1]
        public double[,,] Zone_H_tot = new double[2,2,12]; //[비이용일/이용일] = [we/wd]=[0/1]
        public double[,,] tao = new double[2,2,12]; double tao_max; //[비이용일/이용일] = [we/wd]=[0/1]
        public double[,] theta_e = new double[2, 12]; public double[] dwe_mth = new double[12], dwd_mth = new double[12];
        public double[] dmth = new double[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public double[,,] theta_i = new double[2, 2, 12];

        //[난방/냉방,비이용일/이용일,mth] = [h/c,we/wd,mth]=[0/1,0/1,12]
        //QT
        public double[,,] QTsink_tot = new double[2, 2, 12], QTsink_Wall = new double[2, 2, 12], QTsink_Roof = new double[2, 2, 12], QTsink_Floor = new double[2, 2, 12], QTsink_GWall = new double[2, 2, 12], QTsink_Door = new double[2, 2, 12], QTsink_Win = new double[2, 2, 12], QTsink_CW = new double[2, 2, 12];
        public double[,,] QTsource_tot = new double[2, 2, 12], QTsource_Wall = new double[2, 2, 12], QTsource_Roof = new double[2, 2, 12], QTsource_Floor = new double[2, 2, 12], QTsource_GWall = new double[2, 2, 12], QTsource_Door = new double[2, 2, 12], QTsource_Win = new double[2, 2, 12], QTsource_CW = new double[2, 2, 12];
        public double[,,] QTsink_TB = new double[2, 2, 12], QTsource_TB = new double[2, 2, 12];
        public double QTsink_tot_max, QTsink_Wall_max, QTsink_Roof_max, QTsink_Floor_max, QTsink_GWall_max, QTsink_Door_max, QTsink_Win_max, QTsink_CW_max, QTsink_TB_max, QT_u_sink_max;
        public double QTsink_tot_Cmax, QTsink_Wall_Cmax, QTsink_Roof_Cmax, QTsink_Floor_Cmax, QTsink_GWall_Cmax, QTsink_Door_Cmax, QTsink_Win_Cmax, QTsink_CW_Cmax, QTsink_TB_Cmax, QT_u_sink_Cmax;
        public double QTsource_tot_Cmax, QTsource_Wall_Cmax, QTsource_Roof_Cmax, QTsource_Floor_Cmax, QTsource_GWall_Cmax, QTsource_Door_Cmax, QTsource_Win_Cmax, QTsource_CW_Cmax, QTsource_TB_Cmax, QT_u_source_Cmax;
        //QS
        public double[,,] QSopsink_tot = new double[2, 2, 12], QSopsource_tot = new double[2, 2, 12], QStr_tot = new double[2, 2, 12];
        public double[] QSopsink_Wall = new double[12], QSopsink_Roof = new double[12], QSopsink_Door = new double[12], QSopsink_CW_p = new double[12];
        public double[] QSopsource_Wall = new double[12], QSopsource_Roof = new double[12], QSopsource_Door = new double[12], QSopsource_CW_p = new double[12];
        public double[,] QStr_Win = new double[2, 12], QStr_CW = new double[2, 12]; public double QStr_Win_max, QStr_CW_max;
        public double QSopsink_tot_Cmax, QSopsource_tot_Cmax, QStr_tot_Cmax;
        //QV
        public double[,,] QVsink_tot = new double[2, 2, 12], QV_inf_sink = new double[2, 2, 12], QV_win_sink = new double[2, 2, 12], QV_z_sink = new double[2, 2, 12], QV_mech_sink = new double[2, 2, 12];
        public double[,,] QVsource_tot = new double[2, 2, 12], QV_inf_source = new double[2, 2, 12], QV_win_source = new double[2, 2, 12], QV_z_source = new double[2, 2, 12], QV_mech_source = new double[2, 2, 12];
        public double QVsink_tot_max , QV_inf_sink_max, QV_win_sink_max, QV_z_sink_max ,QV_mech_sink_max;
        public double QVsink_tot_Cmax, QV_inf_sink_Cmax, QV_win_sink_Cmax, QVsource_tot_Cmax, QV_inf_source_Cmax, QV_win_source_Cmax;
        //QI
        public double[,,] QI_tot = new double[2, 2, 12], QI_L = new double[2, 2, 12]; public double[] QI_Humidity = new double[12];
        public double[,] QI_P = new double[2,12], QI_fac = new double[2,12];
        //
        public double[,,] Qsink = new double[2, 2, 12], Qsource = new double[2, 2, 12], gamma = new double[2, 2, 12], a = new double[2, 2, 12], eta = new double[2, 2, 12], dQc_b = new double[2, 2, 12], dQc_sink = new double[2, 2, 12];
        public double[] Qhb_we_day = new double[12], Qhb_wd_day = new double[12], Qcb_we_day = new double[12], Qcb_wd_day = new double[12];
        public  double[,] Q_DHU_win = new double[2, 12]; public double[] Q_DHU_mech = new double[12]; public double[,] Q_DHU_tot = new double[2, 12];public double Q_DHU_max; //wewd, mth
        public double[] Qhb_mth = new double[12], Qcb_mth = new double[12], Qhb_we_mth = new double[12], Qhb_wd_mth = new double[12], Qcb_we_mth = new double[12], Qcb_wd_mth = new double[12];
        public double Qhb_a, Qcb_a, Qhb_we_a, Qhb_wd_a, Qcb_we_a, Qcb_wd_a;
        public double[,,] Qb_day = new double[2, 2, 12]; public double[,,] Qb_mth = new double[2, 2, 12]; public double[] Qb_a = new double[2];
        public double[,,] Theta_U = new double[2, 2, 12]; public double[,,] QT_u_sink = new double[2, 2, 12]; public double[,,] QT_u_source = new double[2, 2, 12];
        string[][] Location; public string[][] 검토유형;
        public double theta_i_c_max_d, theta_e_min, theta_e_max, X_e_max; String[,] Is_max = new String[9, 2]; //수평,남,남동,남서,동,서,북서,북동,북 
        public double[] Q_max = new double [2]; public double[,] t_max = new double[2,12];
        double[,,] theta_u = new double[2, 2, 12]; double Utb;
        public double Door_q50 = 0, Win_q50 = 0, CW_q50 = 0, Wall_q50 = 0, Roof_q50 = 0;

        public Zone(String zoneNum)
        {
            this.ZoneNum = zoneNum;
           
        }

        public void LoadData_ZoneGeneral()
        {  //존 사용 정보 가져오기            
            string[][] ZoneG = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_form", "존이름,용도프로필,냉난방유무,재실자수,냉난방시간", "존번호='" + ZoneNum + "'");            
            if (ZoneG.Length > 0)
            {
                zoneName = ZoneG[0][0];
                zoneUsage = ZoneG[0][1];
                zoneHC = ZoneG[0][2];
                Peope_Num = Convert.ToDouble(ZoneG[0][3]);
                t_c_op_d = Convert.ToDouble(ZoneG[0][4]);
            }

            //존 용도프로필 정보 가져오기 
            String[][] ZoneU = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "난방설정온도,냉방설정온도,허용셋백온도,난방최저온도,최저상대습도,최고상대습도,겨울습기발생량,여름습기발생량,냉방최고온도", "용도명='" + zoneUsage + "'");
            if (ZoneU.Length > 0)
            {
                theta_i_h_set = Convert.ToDouble(ZoneU[0][0]);
                theta_i_c_set = Convert.ToDouble(ZoneU[0][1]);
                dtheta_i_NA = Convert.ToDouble(ZoneU[0][2]);
                Fx = 0.8;
                Fx_Floor = 0.5; //임의값 넣음 나중에 계산해야함 
                Fx_GWall = 0.5;//임의값 넣음 나중에 계산해야함 
                theta_s_c = 18;
                theta_i_h_min = Convert.ToDouble(ZoneU[0][3]);
                theta_i_c_max = Convert.ToDouble(ZoneU[0][8]);
                theta_SUP_Wi = 18;
                Mode_night = "운전정지";
                Mode_we = "운전정지";
                xi_c_set = 611.2 * Math.Exp(17.62 * theta_i_c_set / (243.12 + theta_i_c_set)) / 461.51 / (273.15 + theta_i_c_set) / 1.2 * (Convert.ToDouble(ZoneU[0][5]) / 100);
                xi_h_set = 611.2 * Math.Exp(17.62 * theta_i_h_set / (243.12 + theta_i_h_set)) / 461.51 / (273.15 + theta_i_h_set) / 1.2 * (Convert.ToDouble(ZoneU[0][4]) / 100);
                H_winter = Convert.ToDouble(ZoneU[0][6]);
                H_summer = Convert.ToDouble(ZoneU[0][7]);
            }

            //존 일반정보 가져오기
            // ZoneG = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_form", "사용시간,냉난방시간,연이용일수,순바닥면적,천장고, 면적당인체발열, 면적당기기발열, 존축열성능, 비이용일환기량,이용일환기량,주이용일", "존번호='" + ZoneNum + "'");
            ZoneG = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_form", "사용시간,냉난방시간,연이용일수,순바닥면적,천장고, 일일인체발열, 일일기기발열, 존축열성능, 비이용일환기량,이용일환기량,주이용일", "존번호='" + ZoneNum + "'");
            if (ZoneG.Length > 0)
            {
                twd_d = Convert.ToDouble(ZoneG[0][0]);
                th_op_d_we = 0;
                th_op_d = Convert.ToDouble(ZoneG[0][1]);
                dwd_a = Convert.ToDouble(ZoneG[0][2]);
                zoneArea = Convert.ToDouble(ZoneG[0][3]);
                zoneHeight = Convert.ToDouble(ZoneG[0][4]);
                qI_p = Convert.ToDouble(ZoneG[0][5]);
                qI_fac = Convert.ToDouble(ZoneG[0][6]);
                Cwirk_A = Convert.ToDouble(ZoneG[0][7]);
                VA_we = Convert.ToDouble(ZoneG[0][8]) / zoneArea; //단위면적당 값 
                VA_wd = Convert.ToDouble(ZoneG[0][9]) / zoneArea;//단위면적당 값 

                e = 0.05;
                f = 15;

                for (int mth = 0; mth < 12; mth++)
                {
                    string[][] ValueK;
                    if (ZoneG[0][10] != "5.5")
                    {
                        ValueK = Program.DB.getValue(DB.type.BaseDB_HCneed, "이용일수", "이용일수", "월='" + (mth + 1) + "월' AND 주간일수 ='주 " + ZoneG[0][10] + ".0 일 근무'");
                    }
                    else { ValueK = Program.DB.getValue(DB.type.BaseDB_HCneed, "이용일수", "이용일수", "월='" + (mth + 1) + "월' AND 주간일수 ='주 5.5 일 근무'"); }
                    if (ValueK.Length > 0)
                    {
                        dwd_mth[mth] = Convert.ToDouble(ValueK[0][0]);
                    }
                }
            }

            //외기온도 데이터 불러오기 
            Location = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
            string[][] OTemp = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_온도습도", "기간,온도", "지역명 ='" + Location[0][0] + "'");
            int m = -1;
            if (OTemp.Length > 0)
            {
                
                while (++m < 12)
                {
                    theta_e[0, m] = Convert.ToDouble(OTemp[m][1]); //난방 실외온도 
                    theta_e[1, m] = Convert.ToDouble(OTemp[m][1]); //냉방 실외온도
                    if(3<= m && m <= 9)
                    {
                        theta_e[1, m] = theta_e[1, m] + 2;
                    }
                }
            }
            //부하 관련 데이터 불러오기 
            string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_부하 ", "해석설계외기온도,최고온도,최고절대습도", "지역명='" + Location[0][0] + "'");
            if (Value.Length > 0)
            {
                theta_i_c_max_d = (theta_i_c_set + theta_i_c_max - 2) / 2;
                theta_e_min = Convert.ToDouble(Value[0][0]);
                theta_e_max = Convert.ToDouble(Value[0][1]);
                X_e_max = Convert.ToDouble(Value[0][2]);
            }
            String[] a = { "수평", "남", "남동", "남서", "동", "서", "북서", "북동", "북" };
            for (int k = 0; k < a.Length; k++)
            {
                Is_max[k, 0] = a[k];
                Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_부하 ", "일사량", "지역명='" + Location[0][0] + "' AND 방향 ='" + a[k] + "'");
                if (Value.Length > 0)
                {
                    Is_max[k, 1] = Value[0][0];
                }
            }


            {////////////////////////기밀, 열교 임시 나중에 지워야 함
                String[][] 기존신규 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형");
                if (기존신규.Length > 0)
                {
                    if (기존신규[0][0] == "기존")
                    {
                        Utb = 0.15;
                    }
                    else
                    {
                        Utb = 0.1;
                    }
                }
            }
        }
        public void LoadData_q50()
        {
            string[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기밀측정여부,출입문q50,창호q50,외벽q50,지붕q50", "");
            if (Value2.Length > 0)
            {
                Door_q50 = Convert.ToDouble(Value2[0][1]);
                Win_q50 = Convert.ToDouble(Value2[0][2]);
                Wall_q50 = Convert.ToDouble(Value2[0][3]);
                Roof_q50 = Convert.ToDouble(Value2[0][4]);
            }            
        }
        public void LoadData_Ventil()
        {//존 환기정보 가져오기 
            string[][] ZoneG = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_form", "환기유무,환기방식,비이용일환기량,이용일환기량,선택열회수기", "존번호='" + ZoneNum + "'");
            if (ZoneG.Length > 0)
            {
                if (Convert.ToBoolean(ZoneG[0][0]))
                {
                    if (ZoneG[0][1] == "열회수기" )
                    {
                        Vmech_SUP_we = Convert.ToDouble(ZoneG[0][2]);
                        Vmech_ETA_we = Convert.ToDouble(ZoneG[0][2]);
                        Vmech_SUP_wd = Convert.ToDouble(ZoneG[0][3]);
                        Vmech_ETA_wd = Convert.ToDouble(ZoneG[0][3]);
                        SelectHRV = ZoneG[0][4];
                        string[][] value = Program.DB.getValue(DB.type.ProjDB, "User_HRV", "온도교환효율_난방,온도교환효율_냉방,습도교환효율_난방,습도교환효율_냉방", "번호='" + SelectHRV + "'");
                        if(value.Length > 0)
                        {
                            eta_V_mech[0] = Convert.ToDouble(value[0][0]) / 100;
                            eta_V_mech[1] = Convert.ToDouble(value[0][1]) / 100;
                            eta_χV_mech[0] = Convert.ToDouble(value[0][2]) / 100;
                            eta_χV_mech[1] = Convert.ToDouble(value[0][3]) / 100;
                        }
                       
                    }
                    else if(ZoneG[0][1] == "공조기")
                    {
                        Vmech_SUP_we = Convert.ToDouble(ZoneG[0][2]);
                        Vmech_ETA_we = Convert.ToDouble(ZoneG[0][2]);
                        Vmech_SUP_wd = Convert.ToDouble(ZoneG[0][3]);
                        Vmech_ETA_wd = Convert.ToDouble(ZoneG[0][3]);
                        SelectHRV = ZoneG[0][4];
                        string[][] value = Program.DB.getValue(DB.type.ProjDB, "User_AHU", "온도교환효율_난방,온도교환효율_냉방,습도교환효율_난방,습도교환효율_냉방", "번호='" + SelectHRV + "'");
                        if (value.Length > 0)
                        {
                            eta_V_mech[0] = Convert.ToDouble(value[0][0]) / 100;
                            eta_V_mech[1] = Convert.ToDouble(value[0][1]) / 100;
                            eta_χV_mech[0] = Convert.ToDouble(value[0][2]) / 100;
                            eta_χV_mech[1] = Convert.ToDouble(value[0][3]) / 100;
                        }
                    }
                    else
                    {
                        Vmech_SUP_wd = 0;
                        Vmech_ETA_wd = Convert.ToDouble(ZoneG[0][2]); ; //배기환기는 다 비이용일환기량으로 함 
                        Vmech_SUP_we = 0;
                        Vmech_ETA_we = Convert.ToDouble(ZoneG[0][2]);
                    }
                }
                else
                {
                    Vmech_SUP_we = 0;
                    Vmech_SUP_wd = 0;
                    Vmech_ETA_we = 0;
                    Vmech_ETA_wd = 0;
                }
                Vmech_SUP_z = 0;
                Vmech_ETA_z = 0;
                ρacp_a = 0.34;
            }
        }
        public void LoadData_InWall()
        {
            //존 내벽 정보 가져오기
            String[][] ZoneInW = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,인접존,면적", "존 = '" + ZoneNum + "' And  외피유형 = '내벽'");
            int i = -1;
            if (ZoneInW.Length > 0)
            {
                while (++i < ZoneInW.Length)
                {
                    Zone zone1 = Program.CALC.getZone(ZoneInW[i][1].ToString());
                    double R = (0.1 / 2.3) + 0.13 + 0.13;
                    double U = 1 / R;
                    InWall Inwall = new InWall(ZoneInW[i][0], ZoneInW[i][1], Convert.ToDouble(ZoneInW[i][2]), U);
                    zoneInWall.Add(Inwall);

                    string 난방냉방, 비이;
                    double thetaiset;

                    for (int hc = 0; hc < 2; hc++)
                    {
                        if (hc == 0) { 난방냉방 = "난방"; thetaiset = theta_i_h_set; } else { 난방냉방 = "냉방"; thetaiset = theta_i_c_set; }
                        for (int wewd = 0; wewd < 2; wewd++)
                        {
                            if (wewd == 0) { 비이 = "비이용일"; } else { 비이 = "이용일"; }
                            for (int mth = 0; mth < 12; mth++)
                            {
                                if (zone1 != null)
                                {
                                    if (Math.Abs(zone1.Theta_U[hc, wewd, mth] - thetaiset) > 6)
                                    { Zone_HT_Inwall[hc, wewd, mth] += U * Convert.ToDouble(ZoneInW[i][2]); }
                                    else { }
                                }
                                Zone_HT_tot[hc, wewd, mth] = Zone_HT_Inwall[hc, wewd, mth];
                            }
                        }
                    }
                }
            }
        }
        public void LoadData_SL()
        { //존 층간바닥 정보 가져오기
            String[][] ZoneSL = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,인접존,면적", "존 = '" + ZoneNum + "' And  외피유형 = '층간바닥'");
            int i = -1;
            if (ZoneSL.Length > 0)
            {
                while (++i < ZoneSL.Length)
                {
                    Zone zone1 = Program.CALC.getZone(ZoneSL[i][1].ToString());
                    double R = (0.15 / 2.3) + 0.13 + 0.13;
                    double U = 1 / R;
                    Slab slab = new Slab(ZoneSL[i][0], ZoneSL[i][1], Convert.ToDouble(ZoneSL[i][2]), U);
                    zoneSlab.Add(slab);

                    string 난방냉방, 비이;
                    double thetaiset;
                    for (int hc = 0; hc < 2; hc++)
                    {
                        if (hc == 0) { 난방냉방 = "난방"; thetaiset = theta_i_h_set; } else { 난방냉방 = "냉방"; thetaiset = theta_i_c_set; }
                        for (int wewd = 0; wewd < 2; wewd++)
                        {
                            if (wewd == 0) { 비이 = "비이용일"; } else { 비이 = "이용일"; }
                            for (int mth = 0; mth < 12; mth++)
                            {
                                if (zone1 != null)
                                {
                                    if (Math.Abs(zone1.Theta_U[hc, wewd, mth] - thetaiset) > 6)
                                    { Zone_HT_Slab[hc, wewd, mth] += U * Convert.ToDouble(ZoneSL[i][2]); }
                                    else { }
                                }
                                Zone_HT_tot[hc, wewd, mth] = Zone_HT_tot[hc, wewd, mth] + Zone_HT_Slab[hc, wewd, mth];
                            }
                        }
                    }
                }
            }
        }
        public void LoadData_Wall()
        {//존 외벽 정보 가져오기           
            String[][] ZoneW = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.유효열관류율,b.흡수율,b.직접간접,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + ZoneNum + "' And  NOT b.직접간접 = '지면'");
           
            int i = -1;
            if (ZoneW.Length > 0)
            {
                while (++i < ZoneW.Length)
                {
                    Wall wall = new Wall(ZoneW[i][0], ZoneW[i][2], Convert.ToDouble(ZoneW[i][1]), Convert.ToDouble(ZoneW[i][3]), Convert.ToDouble(ZoneW[i][4]), ZoneW[i][5], ZoneW[i][6], ZoneW[i][7]);
                    zoneWall.Add(wall);
                }
            }
        }
        public void LoadData_Roof()
        { //존 지붕 정보 가져오기
            String[][] ZoneR = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.유효열관류율,b.흡수율,b.직접간접,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionRoof AS b ON a.구조체번호 = b.번호 where a.존 = '" + ZoneNum + "'");
           
            if (ZoneR.Length > 0)
            {
                int i = -1;
                while (++i < ZoneR.Length)
                {
                    Roof roof = new Roof(ZoneR[i][0], ZoneR[i][2], Convert.ToDouble(ZoneR[i][1]), Convert.ToDouble(ZoneR[i][3]), Convert.ToDouble(ZoneR[i][4]), ZoneR[i][5], ZoneR[i][6], ZoneR[i][7]);
                    zoneRoof.Add(roof);
                }
            }
        }
        public void LoadData_Floor()
        {     //존 바닥 정보 가져오기
            String[][] ZoneF = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.유효열관류율,b.직접간접,b.기초설치 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionFloor AS b ON a.구조체번호 = b.번호 where a.존 = '" + ZoneNum + "'");
            if (ZoneF.Length > 0)
            {
                int i = -1;
                while (++i < ZoneF.Length)
                {
                    double fx_f = 0.8;

                    switch (ZoneF[i][5].ToString())
                    {
                        case "지면위":
                            {
                                if (Convert.ToDouble(ZoneF[i][3]) >= 3)
                                { fx_f = 0.3; }
                                else if (Convert.ToDouble(ZoneF[i][3]) >= 1)
                                { fx_f = 0.55; }
                                else if (Convert.ToDouble(ZoneF[i][3]) > 0.3)
                                { fx_f = 0.7; }
                                else { fx_f = 0.8; }
                                break;
                            }
                        case "단열지하":
                            {
                                if (Convert.ToDouble(ZoneF[i][3]) >= 3)
                                { fx_f = 0.2; }
                                else if (Convert.ToDouble(ZoneF[i][3]) >= 1)
                                { fx_f = 0.45; }
                                else if (Convert.ToDouble(ZoneF[i][3]) > 0.3)
                                { fx_f = 0.55; }
                                else { fx_f = 0.7; }
                                break;
                            }
                        case "비단열지하":
                            {
                                if (Convert.ToDouble(ZoneF[i][3]) >= 3)
                                { fx_f = 0.45; }
                                else if (Convert.ToDouble(ZoneF[i][3]) >= 1)
                                { fx_f = 0.75; }
                                else if (Convert.ToDouble(ZoneF[i][3]) > 0.3)
                                { fx_f = 0.8; }
                                else { fx_f = 0.85; }
                                break;
                            }
                    }

                    Floor floor = new Floor(ZoneF[i][0], ZoneF[i][2], Convert.ToDouble(ZoneF[i][1]), Convert.ToDouble(ZoneF[i][3]), ZoneF[i][5], fx_f);
                    zoneFloor.Add(floor);
                }
            }

        }
        public void LoadData_GWall()
        {   //존 지하벽 정보 가져오기
            String[][] ZoneG = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.유효열관류율,b.직접간접 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + ZoneNum + "' And  b.직접간접 = '지면'");
           
            if (ZoneG.Length > 0)
            {
                int i = -1;
                while (++i < ZoneG.Length)
                {
                    double fx_f = 1;
                    if (Convert.ToDouble(ZoneG[i][3]) >= 3)
                    { fx_f = 0.35; }
                    else if (Convert.ToDouble(ZoneG[i][3]) >= 1)
                    { fx_f = 0.55; }
                    else if (Convert.ToDouble(ZoneG[i][3]) > 0.3)
                    { fx_f = 0.65; }
                    else { fx_f = 0.75; }
                    break;

                    GWall gwall = new GWall(ZoneG[i][0], ZoneG[i][2], Convert.ToDouble(ZoneG[i][1]), Convert.ToDouble(ZoneG[i][3]), fx_f);
                    zoneGWall.Add(gwall);
                }
            }

        }
        public void LoadData_Door()
        {
            String[][] ZoneD = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.문유효열관류율,b.흡수율,b.직접간접,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionDoor AS b ON a.구조체번호 = b.번호 where a.존 = '" + ZoneNum + "'");
         
            if (ZoneD.Length > 0)
            {
                int i = -1;
                while (++i < ZoneD.Length)
                {
                    Door door = new Door(ZoneD[i][0], ZoneD[i][2], Convert.ToDouble(ZoneD[i][1]), Convert.ToDouble(ZoneD[i][3]), Convert.ToDouble(ZoneD[i][4]), ZoneD[i][5], ZoneD[i][6], ZoneD[i][7]);
                    zoneDoor.Add(door);
                }
            }
        }
        public void LoadData_Win()
        {
            //존 창문 정보 가져오기
            String[][] ZoneWin = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.창호열관류율,b.설치열교가산치,b.창호유효열관류율,b.유리면적비,b.상위창호번호,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체번호 = b.번호 where a.존 = '" + ZoneNum + "'");
            
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
                            Window win = new Window(ZoneWin[i][0], ZoneWin[i][7], ZoneWin[i][2], Convert.ToDouble(ZoneWin[i][1]), Convert.ToDouble(ZoneWin[i][3]) , Convert.ToDouble(ZoneWin[i][4]), ZoneWin_P[0][0], Convert.ToDouble(ZoneWin[i][6]), Convert.ToDouble(ZoneWin_P[0][1]), Convert.ToDouble(ZoneWin_P[0][2]), Convert.ToDouble(Blind[0][0]), Convert.ToDouble(Blind[0][1]), ZoneWin[i][8], ZoneWin[i][9]);
                            zoneWin.Add(win);
                        }
                        else
                        {
                            Window win = new Window(ZoneWin[i][0], ZoneWin[i][7], ZoneWin[i][2], Convert.ToDouble(ZoneWin[i][1]), Convert.ToDouble(ZoneWin[i][3]) ,Convert.ToDouble(ZoneWin[i][4]), ZoneWin_P[0][0], Convert.ToDouble(ZoneWin[i][6]), Convert.ToDouble(ZoneWin_P[0][1]), Convert.ToDouble(ZoneWin_P[0][2]), 0, 0, ZoneWin[i][8], ZoneWin[i][9]);
                            zoneWin.Add(win);
                        }
                    }
                }
            }
        }
        public void LoadData_CW()
        {
            //존 커튼월 정보 가져오기
            String[][] ZoneCW = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,면적,커튼월부위,구조체번호,방위,기울기", "존 = '" + ZoneNum + "' AND 외피유형 = '커튼월창'");
            if (ZoneCW.Length > 0)
            {              
                int i = -1;
                while (++i < ZoneCW.Length)
                { //유리부분면적,유리부분열관류율,유리부분유리면적비,태양열취득률,빛투과율, 패널부분 면적, 패널부분흡수율, 출입문부분면적, 출입문부분열관류율,출입문부분유리면적비, 출입문부분태양열취득률, 출입문부분빛투과율, 커튼월창면적, 설치열교가산치 
                    if (ZoneCW[i][2] == "유리부분")
                    {                        
                        String[][] CW_g = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "유리부분열관류율,유리부분유리면적비,태양열취득률,빛투과율,설치열교가산치", "번호 = '" + ZoneCW[i][3] + "'");                        
                        string[][] Blind = Program.DB.getValue(DB.type.ProjDB, "Blind_3D", "차양포함태양열취득률,차양포함빛투과율", "번호='" + ZoneCW[i][3] + "'");
                        if (CW_g.Length > 0)
                        {
                            if (Blind.Length > 0)

                            {
                                CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_g[0][0]), Convert.ToDouble(CW_g[0][1]), Convert.ToDouble(CW_g[0][2]), Convert.ToDouble(Blind[0][0]), Convert.ToDouble(CW_g[0][3]), Convert.ToDouble(Blind[0][1]), 0, 0, 0, 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_g[0][4]), ZoneCW[i][4], ZoneCW[i][5], "유리부분");
                                zoneCW.Add(cw);
                            }
                            else
                            {
                                CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_g[0][0]), Convert.ToDouble(CW_g[0][1]), Convert.ToDouble(CW_g[0][2]), 0, Convert.ToDouble(CW_g[0][3]), 0, 0, 0, 0, 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_g[0][4]), ZoneCW[i][4], ZoneCW[i][5], "유리부분");
                                zoneCW.Add(cw);
                            }
                        }

                    }
                    else if (ZoneCW[i][2] == "패널부분")
                    {
                        String[][] CW_p = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "패널부분열관류율,패널흡수율,설치열교가산치", "번호 = '" + ZoneCW[i][3] + "'");
                        if (CW_p.Length > 0)
                        {
                            CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], 0, 0, 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_p[0][0]), Convert.ToDouble(CW_p[0][1]), 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_p[0][2]), ZoneCW[i][4], ZoneCW[i][5], "패널부분");
                            zoneCW.Add(cw);
                        }
                    }
                    else
                    {
                        String[][] CW_d = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "출입문부분열관류율,출입문부분유리면적비,출입문태양열취득률,출입문빛투과율,설치열교가산치", "번호 = '" + ZoneCW[i][3] + "'");
                        if (CW_d.Length > 0)
                        {
                            CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_d[0][0]), Convert.ToDouble(CW_d[0][1]), Convert.ToDouble(CW_d[0][2]), Convert.ToDouble(CW_d[0][3]), Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_d[0][4]), ZoneCW[i][4], ZoneCW[i][5], "출입문부분");
                            zoneCW.Add(cw);
                        }
                    } 
                }
            }
        }    



        public void ZoneHT() //관류 HT 계산
        {

            //외벽 HT
            for (int i = 0; i < zoneWall.Count; i++)
            {
                Wall zonewall = (Wall)zoneWall[i]; //List를 class 객체로 변환 
                HTCalc htcalc = new HTCalc();

                double[] zoneWall_HT = new double[zoneWall.Count];
                double[] zoneWall_HT_TB = new double[zoneWall.Count];

                zoneWall_HT[i] = htcalc.Calc(zonewall.Ueff(), zonewall.Area());
                zoneWall_HT_TB[i] = htcalc.Calc(Utb, zonewall.Area());

                Zone_HT_TB_Wall += zoneWall_HT_TB[i];

                if (zonewall.DiIndi() == "직접외기")
                {
                    Zone_HT_Di_Wall += zoneWall_HT[i];

                }
                else if (zonewall.DiIndi() == "간접외기")
                {
                    Zone_HT_Indi_Wall += zoneWall_HT[i];
                }
                Zone_HT_Wall = Zone_HT_Di_Wall + Zone_HT_Indi_Wall;


            }

            //지붕 HT
            for (int i = 0; i < zoneRoof.Count; i++)
            {
                Roof zoneroof = (Roof)zoneRoof[i]; //List를 class 객체로 변환 
                HTCalc htcalc = new HTCalc();

                double[] zoneRoof_HT = new double[zoneRoof.Count];
                double[] zoneRoof_HT_TB = new double[zoneRoof.Count];

                zoneRoof_HT[i] = htcalc.Calc(zoneroof.Ueff(), zoneroof.Area());
                zoneRoof_HT_TB[i] = htcalc.Calc(Utb, zoneroof.Area());

                Zone_HT_TB_Roof += zoneRoof_HT_TB[i];


                if (zoneroof.DiIndi() == "직접외기")
                {
                    Zone_HT_Di_Roof += zoneRoof_HT[i];

                }
                else if (zoneroof.DiIndi() == "간접외기")
                {
                    Zone_HT_Indi_Roof += zoneRoof_HT[i];
                }
                Zone_HT_Roof = Zone_HT_Di_Roof + Zone_HT_Indi_Roof;
            }

            //바닥 HT
            for (int i = 0; i < zoneFloor.Count; i++)
            {
                Floor zonefloor = (Floor)zoneFloor[i]; //List를 class 객체로 변환 
                HTCalc htcalc = new HTCalc();

                double[] zoneFloor_HT = new double[zoneFloor.Count];
                double[] zoneFloor_HT_TB = new double[zoneFloor.Count];
                zoneFloor_HT[i] = htcalc.Calc(zonefloor.Ueff(), zonefloor.Area());
                zoneFloor_HT_TB[i] = htcalc.Calc(Utb, zonefloor.Area());


                Zone_HT_Floor += zoneFloor_HT[i];
                Zone_HT_TB_Floor += zoneFloor_HT_TB[i];
            }

            //지하벽 HT
            for (int i = 0; i < zoneGWall.Count; i++)
            {
                GWall zonegwall = (GWall)zoneGWall[i]; //List를 class 객체로 변환 
                HTCalc htcalc = new HTCalc();

                double[] zoneGWall_HT = new double[zoneGWall.Count];
                double[] zoneGWall_HT_TB = new double[zoneGWall.Count];

                zoneGWall_HT[i] = htcalc.Calc(zonegwall.Ueff(), zonegwall.Area());
                zoneGWall_HT_TB[i] = htcalc.Calc(Utb, zonegwall.Area());

                Zone_HT_GWall += zoneGWall_HT[i];
                Zone_HT_TB_GWall += zoneGWall_HT_TB[i];
            }


            //문 HT
            for (int i = 0; i < zoneDoor.Count; i++)
            {
                Door zonedoor = (Door)zoneDoor[i]; //List를 class 객체로 변환 
                HTCalc htcalc = new HTCalc();
                double[] zoneDoor_HT = new double[zoneDoor.Count];
                zoneDoor_HT[i] = htcalc.Calc(zonedoor.Ueff(), zonedoor.Area());
                if (zonedoor.DiIndi() == "직접외기")
                {
                    Zone_HT_Di_Door += zoneDoor_HT[i];

                }
                else if (zonedoor.DiIndi() == "간접외기")
                {
                    Zone_HT_Indi_Door += zoneDoor_HT[i];
                }
                Zone_HT_Door = Zone_HT_Di_Door + Zone_HT_Indi_Door; //나중에 설치열교 관류열전달계수 적용 해야함 
            }

            //창 HT
            for (int i = 0; i < zoneWin.Count; i++)
            {
                Window zonewin = (Window)zoneWin[i]; //List를 class 객체로 변환 
                HTCalc htcalc = new HTCalc();
                double[] zoneWin_HT = new double[zoneWin.Count];
                zoneWin_HT[i] = htcalc.Calc(zonewin.Uvalue(), zonewin.Area());
                if (zonewin.DiIndi() == "직접외기")
                {
                    Zone_HT_Di_Win += zoneWin_HT[i];
                }
                else if (zonewin.DiIndi() == "간접외기")
                {
                    Zone_HT_Indi_Win += zoneWin_HT[i];
                }
                double[] zoneWin_HT_TB = new double[zoneWin.Count];
                zoneWin_HT_TB[i] = htcalc.Calc(zonewin.Uinst(), zonewin.Area());
                Zone_HT_TB_Win += zoneWin_HT_TB[i];
                Zone_HT_Win = Zone_HT_Di_Win + Zone_HT_Indi_Win;
            }

            //커튼월 HT
            for (int i = 0; i < zoneCW.Count; i++)
            {
                CW zonecw = (CW)zoneCW[i]; //List를 class 객체로 변환 
                HTCalc htcalc = new HTCalc();
                double[] zoneCW_HT_g = new double[zoneCW.Count]; double[] zoneCW_HT_p = new double[zoneCW.Count]; double[] zoneCW_HT_d = new double[zoneCW.Count];
                zoneCW_HT_g[i] = htcalc.Calc(zonecw.Uvalue_g(), zonecw.Area_g());
                zoneCW_HT_p[i] = htcalc.Calc(zonecw.Uvalue_p(), zonecw.Area_p());
                zoneCW_HT_d[i] = htcalc.Calc(zonecw.Uvalue_d(), zonecw.Area_d());
                Zone_HT_CW += (zoneCW_HT_g[i] + zoneCW_HT_p[i] + zoneCW_HT_d[i]);
                double[] zoneCW_HT_TB = new double[zoneCW.Count];
                zoneCW_HT_TB[i] = htcalc.Calc(zonecw.Uinst(), zonecw.Area_tot());
                Zone_HT_TB_CW += zoneCW_HT_TB[i];
            }

            //접합부열교 

            Zone_HT_TB_tot = Zone_HT_TB_Wall + Zone_HT_TB_Roof + Zone_HT_TB_Floor + Zone_HT_TB_GWall + Zone_HT_TB_Win + Zone_HT_TB_Door + Zone_HT_TB_CW;
            for(int hc  = 0; hc < 2; hc++)
            {
                for(int wewd =0; wewd < 2; wewd++)
                {
                    for(int mth =0; mth < 12; mth++)
                    {
                        Zone_HT_tot[hc, wewd, mth] = Zone_HT_tot[hc,wewd,mth] + Zone_HT_TB_tot + Zone_HT_Wall + Zone_HT_Roof + Zone_HT_Floor + Zone_HT_GWall + Zone_HT_Win + Zone_HT_Door + Zone_HT_CW;
                    }
                }
            }
           
        }

        public void Zone_n50()
        {
            double q50_tot;
            double CMH = 0;
            double AreaDirect_tot = 0;
            string[][] Value;

            string[][] ZoneE = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,외피유형,커튼월부위,면적,구조체,구조체번호,층", "존='" + ZoneNum + "'");
            if (ZoneE.Length > 0)
            {
                for (int n = 0; n < ZoneE.Length; n++)
                {
                    if (ZoneE[n][1] == "커튼월창")
                    {
                        AreaDirect_tot += Convert.ToDouble(ZoneE[n][3]);
                        if (ZoneE[n][1] == "출입문부분")
                        { CMH += Convert.ToDouble(ZoneE[n][3]) * Door_q50; }
                        else
                        {
                            CMH += Convert.ToDouble(ZoneE[n][3]) * Win_q50;
                        }
                    }
                    else if (ZoneE[n][1] == "외벽")
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "직접간접", "번호='" + ZoneE[n][5] + "'");
                        if (Value.Length > 0)
                        {
                            if (Value[0][0] == "직접외기")
                            {
                                AreaDirect_tot += Convert.ToDouble(ZoneE[n][3]);
                                CMH += Convert.ToDouble(ZoneE[n][3]) * Wall_q50;
                            }
                        }
                    }
                    else if (ZoneE[n][1] == "지붕")
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "직접간접", "번호='" + ZoneE[n][5] + "'");
                        if (Value.Length > 0)
                        {
                            if (Value[0][0] == "직접외기")
                            {
                                AreaDirect_tot += Convert.ToDouble(ZoneE[n][3]);
                                CMH += Convert.ToDouble(ZoneE[n][3]) * Roof_q50;
                            }
                        }
                    }
                    else if (ZoneE[n][1] == "창호")
                    {
                        Value = Program.DB.querySQL(DB.type.ProjDB, "select a.직접간접 FROM ConstructionWindow AS a INNER JOIN SubWindow AS b ON a.번호 = b.상위창호번호 where b.번호 = '" + ZoneE[n][5] + "'");
                        if (Value.Length > 0)
                        {
                            if (Value[0][0] == "직접외기")
                            {
                                AreaDirect_tot += Convert.ToDouble(ZoneE[n][3]);
                                CMH += Convert.ToDouble(ZoneE[n][3]) * Win_q50;
                            }
                        }
                    }
                    else if (ZoneE[n][1] == "외부출입문")
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionDoor", "직접간접", "번호='" + ZoneE[n][5] + "'");
                        if (Value.Length > 0)
                        {
                            if (Value[0][0] == "직접외기")
                            {
                                AreaDirect_tot += Convert.ToDouble(ZoneE[n][3]);
                                CMH += Convert.ToDouble(ZoneE[n][3]) * Door_q50;
                            }
                        }
                    }
                    else
                    {

                    }
                }

            }
            if (AreaDirect_tot == 0)
            {
                q50_tot = 0;
                n50 = 0;
            }
            else
            {
                q50_tot = CMH / AreaDirect_tot;
                n50 = CMH / (zoneArea * zoneHeight);
            }
        }
        public void ZoneHV()  //환기 HV계산
        {
            HVCalc hvcalc = new HVCalc();


            nmech[0] = hvcalc.nmech_Calc(Vmech_SUP_we, th_op_d_we, (zoneArea * zoneHeight));
            nmech[1] = hvcalc.nmech_Calc(Vmech_SUP_wd, th_op_d, (zoneArea * zoneHeight));
            Zone_HV_mech[0] = hvcalc.HV_Calc(nmech[0], (zoneArea * zoneHeight));
            Zone_HV_mech[1] = hvcalc.HV_Calc(nmech[1], (zoneArea * zoneHeight));

            nz[0] = hvcalc.nz_Calc(Vmech_SUP_we, Vmech_ETA_we, th_op_d_we, (zoneArea * zoneHeight));
            nz[1] = hvcalc.nz_Calc(Vmech_SUP_wd, Vmech_ETA_wd, th_op_d, (zoneArea * zoneHeight));
            Zone_HV_z[0] = hvcalc.HV_Calc(nz[0], (zoneArea * zoneHeight));
            Zone_HV_z[1] = hvcalc.HV_Calc(nz[1], (zoneArea * zoneHeight));

            ninf[0] = hvcalc.ninf_Calc(Vmech_SUP_we, Vmech_ETA_we, Vmech_SUP_z, Vmech_ETA_z, th_op_d_we, n50, (zoneArea * zoneHeight), e, f);
            ninf[1] = hvcalc.ninf_Calc(Vmech_SUP_wd, Vmech_ETA_wd, Vmech_SUP_z, Vmech_ETA_z, th_op_d, n50, (zoneArea * zoneHeight), e, f);
            Zone_HV_inf[0] = hvcalc.HV_Calc(ninf[0], (zoneArea * zoneHeight));
            Zone_HV_inf[1] = hvcalc.HV_Calc(ninf[1], (zoneArea * zoneHeight));

            nwin[0] = 0.1;
            nwin[1] = hvcalc.nwin_Calc(Vmech_SUP_wd, Vmech_ETA_wd, Vmech_SUP_z, Vmech_ETA_z, th_op_d, twd_d, n50, (VA_wd / zoneHeight), (zoneArea * zoneHeight), e, f);
            Zone_HV_win[0] = hvcalc.HV_Calc(nwin[0], (zoneArea * zoneHeight));
            Zone_HV_win[1] = hvcalc.HV_Calc(nwin[1], (zoneArea * zoneHeight));

            Zone_HV_tot[0] = Zone_HV_mech[0] + Zone_HV_z[0] + Zone_HV_inf[0] + Zone_HV_win[0];
            Zone_HV_tot[1] = Zone_HV_mech[1] + Zone_HV_z[1] + Zone_HV_inf[1] + Zone_HV_win[1];
            HV_tot_max = hvcalc.HV_Calc(0.1, (zoneArea * zoneHeight)) + hvcalc.HV_Calc(n50*e, (zoneArea * zoneHeight));
        }

        public void Zonetao()//시간상수 계산
        {
            for(int hc =0; hc < 2; hc ++)
            {
                for(int wewd =0; wewd < 2; wewd ++)
                {
                    for(int mth  =0; mth < 2; mth ++)
                    {
                        Zone_H_tot[hc,wewd,mth] = Zone_HT_tot[hc,wewd,mth] + Zone_HV_tot[0];
                        theta_iCalc calc = new theta_iCalc();
                        tao[hc,wewd,mth] = calc.tao_Calc(Cwirk_A * zoneArea, Zone_H_tot[hc,wewd,mth]);
                        tao_max = calc.tao_Calc(Cwirk_A * zoneArea, (Zone_HT_tot[hc,wewd,mth] + HV_tot_max));
                    }
                }
            }
        }

        public void Zonethetai()//실내기준온도 계산
        {
            theta_iCalc calc = new theta_iCalc();
            for (int hc = 0; hc < 2; hc++)
            {
                for (int wewd = 0; wewd < 2; wewd++)
                {
                    for (int mth = 0; mth < 12; mth++)
                    {
                        //[hc, wewd, mth]	
                        theta_i[0, 0, mth] = calc.theta_ihwe_Calc(tao[hc,wewd,mth], Mode_we, theta_e[hc, mth], theta_i_h_set, dtheta_i_NA);
                        theta_i[0, 1, mth] = calc.theta_ihwd_Calc(tao[hc,wewd,mth], Mode_night, (24 - th_op_d), theta_e[hc, mth], theta_i_h_set, dtheta_i_NA);
                        theta_i[1, 0, mth] = calc.theta_ic_Calc(theta_i_c_set);
                        theta_i[1, 1, mth] = calc.theta_ic_Calc(theta_i_c_set);

                        theta_u[hc, wewd, mth] = theta_i[wewd, hc, mth] - 0.8 * (theta_i[0, 0, mth] - theta_e[hc, mth]);


                    }
                }
            }
        }

        public void ZoneQT_u() //인접한 비냉난방존과의 관류열전달 
        {
            //내벽 
            if (zoneInWall.Count > 0)
            {
                double[,,,] QT_u_sink_i = new double[zoneInWall.Count, 2, 2, 12]; double[,,,] QT_u_source_i = new double[zoneInWall.Count, 2, 2, 12];
                double[,] zoneInWall_HT = new double[2, zoneInWall.Count];

                for (int i = 0; i < zoneInWall.Count; i++)
                {
                    InWall zoneInwall = (InWall)zoneInWall[i]; //List를 class 객체로 변환 
                    HTCalc htcalc = new HTCalc();

                    String[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "냉난방유무", "존번호 = '" + zoneInwall.SideZone() + "'");
                    if (Value.Length > 0)
                    {
                        if (zoneHC == "난방")
                        {
                            if (Value[0][0] == "비냉난방" || Value[0][0] == "냉방")
                            {
                                zoneInWall_HT[0, i] = htcalc.Calc(zoneInwall.U(), zoneInwall.Area());
                            }
                            else { zoneInWall_HT[0, i] = 0; }
                        }
                        else if (zoneHC == "냉방")
                        {
                            if (Value[0][0] == "비냉난방" || Value[0][0] == "난방")
                            {
                                zoneInWall_HT[1, i] = htcalc.Calc(zoneInwall.U(), zoneInwall.Area());
                            }
                            else { zoneInWall_HT[1, i] = 0; }
                        }
                        else if (zoneHC == "냉난방")
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
                    QTCalc qtcalc = new QTCalc();

                    Zone zone1 = Program.CALC.getZone(zoneInwall.SideZone());
                    for (int hc = 0; hc < 2; hc++)
                    {
                        if (hc == 0) { 난방냉방 = "난방"; } else { 난방냉방 = "냉방"; }

                        for (int wewd = 0; wewd < 2; wewd++)
                        {
                            if (wewd == 0) { 비이 = "비이용일"; } else { 비이 = "이용일"; }

                            for (int mth = 0; mth < 12; mth++)
                            {
                                if (zone1 != null)
                                {
                                    if (Math.Abs(theta_i[hc, wewd, mth] - zone1.Theta_U[hc, wewd, mth]) >6 && theta_i[hc, wewd, mth]> zone1.Theta_U[hc, wewd, mth])
                                    {
                                        QT_u_sink_i[i, hc, wewd, mth] = qtcalc.Calc_sink(zone1.Theta_U[hc, wewd, mth], theta_i[hc, wewd, mth], zoneInWall_HT[hc, i]);
                                        QT_u_source_i[i, hc, wewd, mth] = 0;
                                    }
                                    else if (Math.Abs(theta_i[hc, wewd, mth] - zone1.Theta_U[hc, wewd, mth]) > 6 && theta_i[hc, wewd, mth] < zone1.Theta_U[hc, wewd, mth])
                                    {
                                        QT_u_source_i[i, hc, wewd, mth] = qtcalc.Calc_source(zone1.Theta_U[hc, wewd, mth], theta_i[hc, wewd, mth], zoneInWall_HT[hc, i]);
                                        QT_u_sink_i[i, hc, wewd, mth] = 0;
                                    }
                                    else
                                    {
                                        QT_u_source_i[i, hc, wewd, mth] = 0;
                                        QT_u_sink_i[i, hc, wewd, mth] = 0;
                                    }
                                }
                                else
                                {
                                    QT_u_source_i[i, hc, wewd, mth] = 0;
                                    QT_u_sink_i[i, hc, wewd, mth] = 0;
                                }

                                QT_u_sink[hc, wewd, mth] += QT_u_sink_i[i, hc, wewd, mth];
                                QT_u_source[hc, wewd, mth] += QT_u_source_i[i, hc, wewd, mth];
                            }
                        }
                    }
                    QT_u_sink_max +=  zoneInWall_HT[0, i] * (theta_i_h_min - (theta_i_h_min - 0.5 * (theta_i_h_min - theta_e_min)));

                    if (theta_i_c_max_d > (theta_i_c_max_d - 0.5 * (theta_i_c_max_d - theta_e_max)))
                    { QT_u_sink_Cmax += (zoneInWall_HT[1, i] * (theta_i_c_max_d - (theta_i_c_max_d - 0.5 * (theta_i_c_max_d - theta_e_max)))); }
                    else { QT_u_source_Cmax += (zoneInWall_HT[1, i] * ((theta_i_c_max_d - 0.5 * (theta_i_c_max_d - theta_e_max)) - theta_i_c_max_d)); }

                  
                }
            }

            //층간바닥
            if (zoneSlab.Count > 0)
            {
                double[,,,] QT_u_sink_i = new double[zoneSlab.Count, 2, 2, 12]; double[,,,] QT_u_source_i = new double[zoneSlab.Count, 2, 2, 12];
                double[,] zoneSlab_HT = new double[2, zoneSlab.Count];

                for (int i = 0; i < zoneSlab.Count; i++)
                {
                    Slab zoneslab = (Slab)zoneSlab[i]; //List를 class 객체로 변환 
                    HTCalc htcalc = new HTCalc();

                    String[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "냉난방유무", "존번호 = '" + zoneslab.SideZone() + "'");
                    if (Value.Length > 0)
                    {
                        if (zoneHC == "난방")
                        {
                            if (Value[0][0] == "비냉난방" || Value[0][0] == "냉방")
                            {
                                zoneSlab_HT[0, i] = htcalc.Calc(zoneslab.U(), zoneslab.Area());
                            }
                            else { zoneSlab_HT[0, i] = 0; }
                        }
                        else if (zoneHC == "냉방")
                        {
                            if (Value[0][0] == "비냉난방" || Value[0][0] == "난방")
                            {
                                zoneSlab_HT[1, i] = htcalc.Calc(zoneslab.U(), zoneslab.Area());
                            }
                            else { zoneSlab_HT[1, i] = 0; }
                        }
                        else if (zoneHC == "냉난방")
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
                    QTCalc qtcalc = new QTCalc();
                    Zone zone1 = Program.CALC.getZone(zoneslab.SideZone());
                    for (int hc = 0; hc < 2; hc++)
                    {
                        if (hc == 0) { 난방냉방 = "난방"; } else { 난방냉방 = "냉방"; }

                        for (int wewd = 0; wewd < 2; wewd++)
                        {
                            if (wewd == 0) { 비이 = "비이용일"; } else { 비이 = "이용일"; }
                            for (int mth = 0; mth < 12; mth++)
                            {
                                if (zone1 != null)
                                {
                                    if (Math.Abs(theta_i[hc, wewd, mth] - zone1.Theta_U[hc, wewd, mth]) > 6 && theta_i[hc, wewd, mth] > zone1.Theta_U[hc, wewd, mth])
                                    {
                                        QT_u_sink_i[i, hc, wewd, mth] = qtcalc.Calc_sink(zone1.Theta_U[hc, wewd, mth], theta_i[hc, wewd, mth], zoneSlab_HT[hc, i]);
                                        QT_u_source_i[i, hc, wewd, mth] = 0;
                                    }
                                    else if (Math.Abs(theta_i[hc, wewd, mth] - zone1.Theta_U[hc, wewd, mth]) > 6 && theta_i[hc, wewd, mth] < zone1.Theta_U[hc, wewd, mth])
                                    {
                                        QT_u_source_i[i, hc, wewd, mth] = qtcalc.Calc_source(zone1.Theta_U[hc, wewd, mth], theta_i[hc, wewd, mth], zoneSlab_HT[hc, i]);
                                        QT_u_sink_i[i, hc, wewd, mth] = 0;
                                    }
                                    else
                                    {
                                        QT_u_source_i[i, hc, wewd, mth] = 0;
                                        QT_u_sink_i[i, hc, wewd, mth] = 0;
                                    }
                                }
                                else
                                {
                                    QT_u_source_i[i, hc, wewd, mth] = 0;
                                    QT_u_sink_i[i, hc, wewd, mth] = 0;
                                }

                                QT_u_sink[hc, wewd, mth] += QT_u_sink_i[i, hc, wewd, mth];
                                QT_u_source[hc, wewd, mth] += QT_u_source_i[i, hc, wewd, mth];
                            }
                        }
                    }
                    QT_u_sink_max +=  zoneSlab_HT[0, i] * (theta_i_h_min - (theta_i_h_min - 0.5 * (theta_i_h_min - theta_e_min)));

                    if (theta_i_c_max_d > (theta_i_c_max_d - 0.5 * (theta_i_c_max_d - theta_e_max)))
                    { QT_u_sink_Cmax += (zoneSlab_HT[1, i] * (theta_i_c_max_d - (theta_i_c_max_d - 0.5 * (theta_i_c_max_d - theta_e_max)))); }
                    else { QT_u_source_Cmax += zoneSlab_HT[1, i] * ((theta_i_c_max_d - 0.5 * (theta_i_c_max_d - theta_e_max)) - theta_i_c_max_d); }
                    
                }
            }
        }

        public void ZoneQT()//관류 열전달 계산
        {

            //외벽 QT계산
            QTCalc qtcalc = new QTCalc();
            String[] HC = { "난방", "냉방" };
            String[] WEWD = { "비이용일", "이용일" };
            String MTH;
            double[,,,] zoneWalls_QTsink = new double[zoneWall.Count, 2, 2, 12];
            double[,,,] zoneWalls_QTsource = new double[zoneWall.Count, 2, 2, 12];
            double[,,,] zoneWalls_QTsink_TB = new double[zoneWall.Count, 2, 2, 12];
            double[,,,] zoneWalls_QTsource_TB = new double[zoneWall.Count, 2, 2, 12];
            int i = -1;
            while (++i < zoneWall.Count)
            {
                Wall zonewall = (Wall)zoneWall[i];
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int wewd = 0; wewd <= 1; wewd++)
                    {
                        for (int mth = 0; mth <= 11; mth++)
                        {
                            if (zonewall.DiIndi() == "간접외기")
                            {
                                if (theta_i[hc, wewd, mth] >= theta_u[hc, wewd, mth])
                                {
                                    zoneWalls_QTsink[i, hc, wewd, mth] = qtcalc.Calc_sink(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], zonewall.Ueff() * zonewall.Area());
                                    zoneWalls_QTsink_TB[i, hc, wewd, mth] = qtcalc.Calc_sink(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], Utb * zonewall.Area());
                                }
                                else
                                {
                                    zoneWalls_QTsource[i, hc, wewd, mth] = qtcalc.Calc_source(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], zonewall.Ueff() * zonewall.Area());
                                    zoneWalls_QTsource_TB[i, hc, wewd, mth] = qtcalc.Calc_source(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], Utb * zonewall.Area());
                                }
                            }
                            else
                            {
                                if (theta_i[hc, wewd, mth] >= theta_e[hc, mth])
                                {
                                    zoneWalls_QTsink[i, hc, wewd, mth] = qtcalc.Calc_sink(theta_e[hc, mth], theta_i[hc, wewd, mth], zonewall.Ueff() * zonewall.Area());
                                    zoneWalls_QTsink_TB[i, hc, wewd, mth] = qtcalc.Calc_sink(theta_e[hc, mth], theta_i[hc, wewd, mth], Utb * zonewall.Area());
                                }
                                else
                                {
                                    zoneWalls_QTsource[i, hc, wewd, mth] = qtcalc.Calc_source(theta_e[hc, mth], theta_i[hc, wewd, mth], zonewall.Ueff() * zonewall.Area());
                                    zoneWalls_QTsource_TB[i, hc, wewd, mth] = qtcalc.Calc_source(theta_e[hc, mth], theta_i[hc, wewd, mth], Utb * zonewall.Area());
                                }
                            }
                            MTH = (mth + 1).ToString() + "월";

                                //Program.DB.setValue(DB.type.ProjDB, "Zone_Envelope_Result", "프로젝트유형,외피번호,존번호,구조체번호,외피유형,직접간접," +
                                //   "난방_냉방,비이용일_이용일,월," +
                                //   "HT,HT_TB," +
                                //   "QTsink,QTsource," +
                                //   "QT_TB_sink,QT_TB_source," +
                                //   "QTsink_tot,QTsource_tot",
                                //   "'1','" + zonewall.Num() + "','" + ZoneNum + "','" + zonewall.CNum() + "','" + "외벽" + "','" + zonewall.DiIndi() + "','" +
                                //    HC[hc] + "','" + WEWD[wewd] + "','" + MTH + "','" +
                                //   (zonewall.Ueff() * zonewall.Area()).ToString() + "','" + (Utb * zonewall.Area()).ToString() + "','" +
                                //  zoneWalls_QTsink[i, hc, wewd, mth].ToString() + "','" + zoneWalls_QTsource[i, hc, wewd, mth].ToString() + "','" +
                                //  zoneWalls_QTsink_TB[i, hc, wewd, mth].ToString() + "','" + zoneWalls_QTsource_TB[i, hc, wewd, mth].ToString() + "','" +
                                //  (zoneWalls_QTsink[i, hc, wewd, mth] + zoneWalls_QTsink_TB[i, hc, wewd, mth]).ToString() + "','" + (zoneWalls_QTsource[i, hc, wewd, mth] + zoneWalls_QTsource_TB[i, hc, wewd, mth]).ToString() + "'", "외피번호,난방_냉방,비이용일_이용일,월");

                           

                            QTsink_Wall[hc, wewd, mth] += zoneWalls_QTsink[i, hc, wewd, mth];
                            QTsource_Wall[hc, wewd, mth] += zoneWalls_QTsource[i, hc, wewd, mth];
                            QTsink_TB[hc, wewd, mth] += zoneWalls_QTsink_TB[i, hc, wewd, mth];
                            QTsource_TB[hc, wewd, mth] += zoneWalls_QTsource_TB[i, hc, wewd, mth];
                        }
                    }
                }
                if (zonewall.DiIndi() == "간접외기")
                {
                    QTsink_Wall_max += (zonewall.Ueff() * zonewall.Area() * (theta_i_h_min - (theta_i_h_min - 0.8 * (theta_i_h_min - theta_e_min))));

                    if (theta_i_c_max_d > (theta_i_c_max_d - 0.8 * (theta_i_c_max_d - theta_e_max)))
                   { QTsink_Wall_Cmax += (zonewall.Ueff() * zonewall.Area() * (theta_i_c_max_d - (theta_i_c_max_d - 0.8 * (theta_i_c_max_d - theta_e_max)))); }
                    else { QTsource_Wall_Cmax += (zonewall.Ueff() * zonewall.Area() * ((theta_i_c_max_d - 0.8 * (theta_i_c_max_d - theta_e_max))- theta_i_c_max_d)); }
                }
                else
                {
                    QTsink_Wall_max += (zonewall.Ueff() * zonewall.Area() * (theta_i_h_min - theta_e_min));

                    if (theta_i_c_max_d > theta_e_max)
                    { QTsink_Wall_Cmax += (zonewall.Ueff() * zonewall.Area() * (theta_i_c_max_d -  theta_e_max)); }
                    else { QTsource_Wall_Cmax += (zonewall.Ueff() * zonewall.Area() * (theta_e_max - theta_i_c_max_d)); }
                }

                QTsink_TB_max += (Utb * zonewall.Area() * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_TB_Cmax += (Utb * zonewall.Area() * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_TB_Cmax += (Utb * zonewall.Area() * (theta_e_max - theta_i_c_max_d)); }
            }

            //지붕 QT계산
            double[,,,] zoneRoofs_QTsink = new double[zoneRoof.Count, 2, 2, 12];
            double[,,,] zoneRoofs_QTsource = new double[zoneRoof.Count, 2, 2, 12];
            double[,,,] zoneRoofs_QTsink_TB = new double[zoneRoof.Count, 2, 2, 12];
            double[,,,] zoneRoofs_QTsource_TB = new double[zoneRoof.Count, 2, 2, 12];
            i = -1;
            while (++i < zoneRoof.Count)
            {
                Roof zoneroof = (Roof)zoneRoof[i];
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int wewd = 0; wewd <= 1; wewd++)
                    {
                        for (int mth = 0; mth <= 11; mth++)
                        {
                            if (zoneroof.DiIndi() == "간접외기")
                            {
                                if (theta_i[hc, wewd, mth] >= theta_u[hc, wewd, mth])
                                {
                                    zoneRoofs_QTsink[i, hc, wewd, mth] = qtcalc.Calc_sink(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], zoneroof.Ueff() * zoneroof.Area());
                                    zoneRoofs_QTsink_TB[i, hc, wewd, mth] = qtcalc.Calc_sink(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], Utb * zoneroof.Area());
                                }
                                else
                                {
                                    zoneRoofs_QTsource[i, hc, wewd, mth] = qtcalc.Calc_source(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], zoneroof.Ueff() * zoneroof.Area());
                                    zoneRoofs_QTsource_TB[i, hc, wewd, mth] = qtcalc.Calc_source(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], Utb * zoneroof.Area());
                                }
                            }
                            else
                            {
                                if (theta_i[hc, wewd, mth] >= theta_e[hc, mth])
                                {
                                    zoneRoofs_QTsink[i, hc, wewd, mth] = qtcalc.Calc_sink(theta_e[hc, mth], theta_i[hc, wewd, mth], zoneroof.Ueff() * zoneroof.Area());
                                    zoneRoofs_QTsink_TB[i, hc, wewd, mth] = qtcalc.Calc_sink(theta_e[hc, mth], theta_i[hc, wewd, mth], Utb * zoneroof.Area());
                                }
                                else
                                {
                                    zoneRoofs_QTsource[i, hc, wewd, mth] = qtcalc.Calc_source(theta_e[hc, mth], theta_i[hc, wewd, mth], zoneroof.Ueff() * zoneroof.Area());
                                    zoneRoofs_QTsource_TB[i, hc, wewd, mth] = qtcalc.Calc_source(theta_e[hc, mth], theta_i[hc, wewd, mth], Utb * zoneroof.Area());
                                }
                            }
                            MTH = (mth + 1).ToString() + "월";

                                // Program.DB.setValue(DB.type.ProjDB, "Zone_Envelope_Result", "프로젝트유형,외피번호,존번호,구조체번호,외피유형,직접간접," +
                                //"난방_냉방,비이용일_이용일,월," +
                                //"HT,HT_TB," +
                                //"QTsink,QTsource," +
                                //"QT_TB_sink,QT_TB_source," +
                                //"QTsink_tot,QTsource_tot",
                                //"'1','" + zoneroof.Num() + "','" + ZoneNum + "','" + zoneroof.CNum() + "','" + "지붕" + "','" + zoneroof.DiIndi() + "','" +
                                // HC[hc] + "','" + WEWD[wewd] + "','" + MTH + "','" +
                                //(zoneroof.Ueff() * zoneroof.Area()).ToString() + "','" + (Utb * zoneroof.Area()).ToString() + "','" +
                                //zoneRoofs_QTsink[i, hc, wewd, mth].ToString() + "','" + zoneRoofs_QTsource[i, hc, wewd, mth].ToString() + "','" +
                                //zoneRoofs_QTsink_TB[i, hc, wewd, mth].ToString() + "','" + zoneRoofs_QTsource_TB[i, hc, wewd, mth].ToString() + "','" +
                                //(zoneRoofs_QTsink[i, hc, wewd, mth] + zoneRoofs_QTsink_TB[i, hc, wewd, mth]).ToString() + "','" + (zoneRoofs_QTsource[i, hc, wewd, mth] + zoneRoofs_QTsource_TB[i, hc, wewd, mth]).ToString() + "'", "외피번호,난방_냉방,비이용일_이용일,월");
                            
                            QTsink_Roof[hc, wewd, mth] += zoneRoofs_QTsink[i, hc, wewd, mth];
                            QTsource_Roof[hc, wewd, mth] += zoneRoofs_QTsource[i, hc, wewd, mth];
                            QTsink_TB[hc, wewd, mth] += zoneRoofs_QTsink_TB[i, hc, wewd, mth];
                            QTsource_TB[hc, wewd, mth] += zoneRoofs_QTsource_TB[i, hc, wewd, mth];
                        }
                    }
                }
                if (zoneroof.DiIndi() == "간접외기")
                {
                    QTsink_Roof_max += (zoneroof.Ueff() * zoneroof.Area() * (theta_i_h_min - (theta_i_h_min - 0.8 * (theta_i_h_min - theta_e_min))));

                    if (theta_i_c_max_d > (theta_i_c_max_d - 0.8 * (theta_i_c_max_d - theta_e_max)))
                    { QTsink_Roof_Cmax += (zoneroof.Ueff() * zoneroof.Area() * (theta_i_c_max_d - (theta_i_c_max_d - 0.8 * (theta_i_c_max_d - theta_e_max)))); }
                    else { QTsource_Roof_Cmax += (zoneroof.Ueff() * zoneroof.Area() * ((theta_i_c_max_d - 0.8 * (theta_i_c_max_d - theta_e_max)) - theta_i_c_max_d)); }
                }
                else
                {
                    QTsink_Roof_max += (zoneroof.Ueff() * zoneroof.Area() * (theta_i_h_min - theta_e_min));

                    if (theta_i_c_max_d > theta_e_max)
                    { QTsink_Roof_Cmax += (zoneroof.Ueff() * zoneroof.Area() * (theta_i_c_max_d - theta_e_max)); }
                    else { QTsource_Roof_Cmax += (zoneroof.Ueff() * zoneroof.Area() * (theta_e_max - theta_i_c_max_d)); }
                }
                QTsink_TB_max += (Utb * zoneroof.Area() * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_TB_Cmax += (Utb * zoneroof.Area() * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_TB_Cmax += (Utb * zoneroof.Area() * (theta_e_max - theta_i_c_max_d)); }
            }

            //바닥 QT계산
            double[,,,] zoneFloors_QTsink = new double[zoneFloor.Count, 2, 2, 12];
            double[,,,] zoneFloors_QTsource = new double[zoneFloor.Count, 2, 2, 12];
            double[,,,] zoneFloors_QTsink_TB = new double[zoneFloor.Count, 2, 2, 12];
            double[,,,] zoneFloors_QTsource_TB = new double[zoneFloor.Count, 2, 2, 12];
            i = -1;
            while (++i < zoneFloor.Count)
            {
                Floor zonefloor = (Floor)zoneFloor[i];
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int wewd = 0; wewd <= 1; wewd++)
                    {
                        for (int mth = 0; mth <= 11; mth++)
                        {

                            double[,,] theta_s = new double[2, 2, 12];
                            theta_s[0, wewd, mth] = theta_i[hc, wewd, mth] - zonefloor.Fx() * (theta_i[hc, wewd, mth] - theta_e[hc, mth]);
                            theta_s[1, wewd, mth] = theta_i[hc, wewd, mth] - zonefloor.Fx() * (theta_i[hc, wewd, mth] - theta_e[hc, mth]);
                            if (theta_i[hc, wewd, mth] >= theta_s[hc, wewd, mth])
                            {
                                zoneFloors_QTsink[i, hc, wewd, mth] = qtcalc.Calc_sink(theta_s[hc, wewd, mth], theta_i[hc, wewd, mth], zonefloor.Ueff() * zonefloor.Area());
                                zoneFloors_QTsink_TB[i, hc, wewd, mth] = qtcalc.Calc_sink(theta_e[hc, mth], theta_i[hc, wewd, mth], Utb * zonefloor.Area());
                            }
                            else
                            {
                                zoneFloors_QTsource[i, hc, wewd, mth] = qtcalc.Calc_source(theta_s[hc, wewd, mth], theta_i[hc, wewd, mth], zonefloor.Ueff() * zonefloor.Area());
                                zoneFloors_QTsource_TB[i, hc, wewd, mth] = qtcalc.Calc_source(theta_e[hc, mth], theta_i[hc, wewd, mth], Utb * zonefloor.Area());
                            }


                            //    Program.DB.setValue(DB.type.ProjDB, "Zone_Envelope_Result", "프로젝트유형,외피번호,존번호,구조체번호,외피유형,직접간접," +
                            //    "난방_냉방,비이용일_이용일,월," +
                            //    "HT,HT_TB," +
                            //    "QTsink,QTsource," +
                            //    "QT_TB_sink,QT_TB_source," +
                            //    "QTsink_tot,QTsource_tot",
                            //    "'1','" + zonefloor.Num() + "','" + ZoneNum + "','" + zonefloor.CNum() + "','" + "최하층바닥" + "','" + zonefloor.GroundType() + "','" +
                            //     HC[hc] + "','" + WEWD[wewd] + "','" + (mth + 1).ToString() + "월" + "','" +
                            //    (zonefloor.Ueff() * zonefloor.Area()).ToString() + "','" + (Utb * zonefloor.Area()).ToString() + "','" +
                            //    zoneFloors_QTsink[i, hc, wewd, mth].ToString() + "','" + zoneFloors_QTsource[i, hc, wewd, mth].ToString() + "','" +
                            //    zoneFloors_QTsink_TB[i, hc, wewd, mth].ToString() + "','" + zoneFloors_QTsource_TB[i, hc, wewd, mth].ToString() + "','" +
                            //   (zoneFloors_QTsink_TB[i, hc, wewd, mth] + zoneFloors_QTsink[i, hc, wewd, mth]).ToString() + "','" + (zoneFloors_QTsource[i, hc, wewd, mth] + zoneFloors_QTsource_TB[i, hc, wewd, mth]).ToString() + "'", "외피번호,난방_냉방,비이용일_이용일,월");

                            QTsink_Floor[hc, wewd, mth] += zoneFloors_QTsink[i, hc, wewd, mth];
                            QTsource_Floor[hc, wewd, mth] += zoneFloors_QTsource[i, hc, wewd, mth];
                            QTsink_TB[hc, wewd, mth] += zoneFloors_QTsink_TB[i, hc, wewd, mth];
                            QTsource_TB[hc, wewd, mth] += zoneFloors_QTsource_TB[i, hc, wewd, mth];
                        }
                    }
                }
                
                QTsink_Floor_max += (zonefloor.Ueff() * zonefloor.Area() * (theta_i_h_min - theta_e_min));               

                QTsink_TB_max += (Utb * zonefloor.Area() * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_Floor_Cmax += (zonefloor.Ueff() * zonefloor.Area() * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_Floor_Cmax += (zonefloor.Ueff() * zonefloor.Area() * (theta_e_max - theta_i_c_max_d)); }

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_TB_Cmax += (Utb * zonefloor.Area() * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_TB_Cmax += (Utb * zonefloor.Area() * (theta_e_max - theta_i_c_max_d)); }
            }

            //지하벽 QT계산    
            i = -1;
            double[,,,] zoneGWalls_QTsink = new double[zoneGWall.Count, 2, 2, 12];
            double[,,,] zoneGWalls_QTsource = new double[zoneGWall.Count, 2, 2, 12];
            double[,,,] zoneGWalls_QTsink_TB = new double[zoneGWall.Count, 2, 2, 12];
            double[,,,] zoneGWalls_QTsource_TB = new double[zoneGWall.Count, 2, 2, 12];
            while (++i < zoneGWall.Count)
            {
                GWall zonegwall = (GWall)zoneGWall[i];
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int wewd = 0; wewd <= 1; wewd++)
                    {
                        for (int mth = 0; mth <= 11; mth++)
                        {

                            double[,,] theta_s_GWall = new double[2, 2, 12];
                            theta_s_GWall[0, wewd, mth] = theta_i[0, wewd, mth] - zonegwall.Fx() * (theta_i[0, wewd, mth] - theta_e[hc, mth]);
                            theta_s_GWall[1, wewd, mth] = theta_i[0, wewd, mth] - zonegwall.Fx() * (theta_i[0, wewd, mth] - theta_e[hc, mth]);
                            if (theta_i[hc, wewd, mth] >= theta_s_GWall[hc, wewd, mth])
                            {
                                zoneGWalls_QTsink[i, hc, wewd, mth] = qtcalc.Calc_sink(theta_s_GWall[hc, wewd, mth], theta_i[hc, wewd, mth], zonegwall.Ueff() * zonegwall.Area());
                                zoneGWalls_QTsink_TB[i, hc, wewd, mth] = qtcalc.Calc_sink(theta_s_GWall[hc, wewd, mth], theta_i[hc, wewd, mth], Utb * zonegwall.Area());
                            }
                            else if (theta_i[hc, wewd, mth] < theta_s_GWall[hc, wewd, mth])
                            {
                                zoneGWalls_QTsource[i, hc, wewd, mth] = qtcalc.Calc_source(theta_s_GWall[hc, wewd, mth], theta_i[hc, wewd, mth], zonegwall.Ueff() * zonegwall.Area());
                                zoneGWalls_QTsource_TB[i, hc, wewd, mth] = qtcalc.Calc_source(theta_s_GWall[hc, wewd, mth], theta_i[hc, wewd, mth], Utb * zonegwall.Area());
                            }
 
                                MTH = (mth + 1).ToString() + "월";
                                //Program.DB.setValue(DB.type.ProjDB, "Zone_Envelope_Result", "프로젝트유형,외피번호,존번호,구조체번호,외피유형,직접간접," +
                                //"난방_냉방,비이용일_이용일,월," +
                                //"HT,HT_TB," +
                                //"QTsink,QTsource," +
                                //"QT_TB_sink,QT_TB_source," +
                                //"QTsink_tot,QTsource_tot",
                                //"'1','" + zonegwall.Num() + "','" + ZoneNum + "','" + zonegwall.CNum() + "','" + "외벽" + "','" + "지면" + "','" +
                                // HC[hc] + "','" + WEWD[wewd] + "','" + MTH + "','" +
                                //(zonegwall.Ueff() * zonegwall.Area()).ToString() + "','" + (Utb * zonegwall.Area()).ToString() + "','" +
                                //zoneGWalls_QTsink[i, hc, wewd, mth].ToString() + "','" + zoneGWalls_QTsource[i, hc, wewd, mth].ToString() + "','" +
                                //zoneGWalls_QTsink_TB[i, hc, wewd, mth].ToString() + "','" + zoneGWalls_QTsource_TB[i, hc, wewd, mth].ToString() + "','" +
                                //(zoneGWalls_QTsink[i, hc, wewd, mth] + zoneGWalls_QTsink_TB[i, hc, wewd, mth]).ToString() + "','" + (zoneGWalls_QTsource[i, hc, wewd, mth] + zoneGWalls_QTsource_TB[i, hc, wewd, mth]).ToString() + "'", "외피번호,난방_냉방,비이용일_이용일,월");
                            

                            QTsink_GWall[hc, wewd, mth] += zoneGWalls_QTsink[i, hc, wewd, mth];
                            QTsource_GWall[hc, wewd, mth] += zoneGWalls_QTsource[i, hc, wewd, mth];
                            QTsink_TB[hc, wewd, mth] += zoneGWalls_QTsink_TB[i, hc, wewd, mth];
                            QTsource_TB[hc, wewd, mth] += zoneGWalls_QTsource_TB[i, hc, wewd, mth];
                        }
                    }
                }

                QTsink_GWall_max += (zonegwall.Ueff() * zonegwall.Area() * (theta_i_h_min - theta_e_min));

                QTsink_TB_max += (Utb * zonegwall.Area() * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_GWall_Cmax += (zonegwall.Ueff() * zonegwall.Area() * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_GWall_Cmax += (zonegwall.Ueff() * zonegwall.Area() * (theta_e_max - theta_i_c_max_d)); }

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_TB_Cmax += (Utb * zonegwall.Area() * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_TB_Cmax += (Utb * zonegwall.Area() * (theta_e_max - theta_i_c_max_d)); }
            }

            //출입문 QT계산
            double[,,,] zoneDoors_QTsink = new double[zoneDoor.Count, 2, 2, 12];
            double[,,,] zoneDoors_QTsource = new double[zoneDoor.Count, 2, 2, 12];
            double[,,,] zoneDoors_QTsink_TB = new double[zoneDoor.Count, 2, 2, 12];
            double[,,,] zoneDoors_QTsource_TB = new double[zoneDoor.Count, 2, 2, 12];
            i = -1;
            while (++i < zoneDoor.Count)
            {
                Door zonedoor = (Door)zoneDoor[i];
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int wewd = 0; wewd <= 1; wewd++)
                    {
                        for (int mth = 0; mth <= 11; mth++)
                        {
                            if (zonedoor.DiIndi() == "간접외기")
                            {
                                if (theta_i[hc, wewd, mth] >= theta_u[hc, wewd, mth])
                                {
                                    zoneDoors_QTsink[i, hc, wewd, mth] = qtcalc.Calc_sink(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], zonedoor.Ueff() * zonedoor.Area());
                                    zoneDoors_QTsink_TB[i, hc, wewd, mth] = qtcalc.Calc_sink(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], Utb * zonedoor.Area());
                                }
                                else
                                {
                                    zoneDoors_QTsource[i, hc, wewd, mth] = qtcalc.Calc_source(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], zonedoor.Ueff() * zonedoor.Area());
                                    zoneDoors_QTsource_TB[i, hc, wewd, mth] = qtcalc.Calc_source(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], Utb * zonedoor.Area());
                                }
                            }
                            else
                            {
                                if (theta_i[hc, wewd, mth] >= theta_e[hc, mth])
                                {
                                    zoneDoors_QTsink[i, hc, wewd, mth] = qtcalc.Calc_sink(theta_e[hc, mth], theta_i[hc, wewd, mth], zonedoor.Ueff() * zonedoor.Area());
                                    zoneDoors_QTsink_TB[i, hc, wewd, mth] = qtcalc.Calc_sink(theta_e[hc, mth], theta_i[hc, wewd, mth], Utb * zonedoor.Area());
                                }
                                else
                                {
                                    zoneDoors_QTsource[i, hc, wewd, mth] = qtcalc.Calc_source(theta_e[hc, mth], theta_i[hc, wewd, mth], zonedoor.Ueff() * zonedoor.Area());
                                    zoneDoors_QTsource_TB[i, hc, wewd, mth] = qtcalc.Calc_source(theta_e[hc, mth], theta_i[hc, wewd, mth], Utb * zonedoor.Area());
                                }
                            }
                            MTH = (mth + 1).ToString() + "월";

                                //    Program.DB.setValue(DB.type.ProjDB, "Zone_Envelope_Result", "프로젝트유형,외피번호,존번호,구조체번호,외피유형,직접간접," +
                                //"난방_냉방,비이용일_이용일,월," +
                                //"HT,HT_TB," +
                                //"QTsink,QTsource," +
                                //"QT_TB_sink,QT_TB_source," +
                                //"QTsink_tot,QTsource_tot",
                                //"'1','" + zonedoor.Num() + "','" + ZoneNum + "','" + zonedoor.CNum() + "','" + "외부출입문" + "','" + zonedoor.DiIndi() + "','" +
                                // HC[hc] + "','" + WEWD[wewd] + "','" + MTH + "','" +
                                //(zonedoor.Ueff() * zonedoor.Area()).ToString() + "','" + (Utb * zonedoor.Area()).ToString() + "','" +
                                //zoneDoors_QTsink[i, hc, wewd, mth].ToString() + "','" + zoneDoors_QTsource[i, hc, wewd, mth].ToString() + "','" +
                                //zoneDoors_QTsink_TB[i, hc, wewd, mth].ToString() + "','" + zoneDoors_QTsource_TB[i, hc, wewd, mth].ToString() + "','" +
                                //(zoneDoors_QTsink[i, hc, wewd, mth] + zoneDoors_QTsink_TB[i, hc, wewd, mth]).ToString() + "','" + (zoneDoors_QTsource[i, hc, wewd, mth] + zoneDoors_QTsource_TB[i, hc, wewd, mth]).ToString() + "'", "외피번호,난방_냉방,비이용일_이용일,월");
                            

                            QTsink_Door[hc, wewd, mth] += zoneDoors_QTsink[i, hc, wewd, mth];
                            QTsource_Door[hc, wewd, mth] += zoneDoors_QTsource[i, hc, wewd, mth];
                            QTsink_TB[hc, wewd, mth] += zoneDoors_QTsink_TB[i, hc, wewd, mth];
                            QTsource_TB[hc, wewd, mth] += zoneDoors_QTsource_TB[i, hc, wewd, mth];
                        }
                    }
                }
                if (zonedoor.DiIndi() == "간접외기")
                {
                    QTsink_Door_max += (zonedoor.Ueff() * zonedoor.Area() * (theta_i_h_min - (theta_i_h_min - 0.8 * (theta_i_h_min - theta_e_min))));

                    if (theta_i_c_max_d > (theta_i_c_max_d - 0.8 * (theta_i_c_max_d - theta_e_max)))
                    { QTsink_Door_Cmax += (zonedoor.Ueff() * zonedoor.Area() * (theta_i_c_max_d - (theta_i_c_max_d - 0.8 * (theta_i_c_max_d - theta_e_max)))); }
                    else { QTsource_Door_Cmax += (zonedoor.Ueff() * zonedoor.Area() * ((theta_i_c_max_d - 0.8 * (theta_i_c_max_d - theta_e_max)) - theta_i_c_max_d)); }
                }
                else
                {
                    QTsink_Door_max += (zonedoor.Ueff() * zonedoor.Area() * (theta_i_h_min - theta_e_min));

                    if (theta_i_c_max_d > theta_e_max)
                    { QTsink_Door_Cmax += (zonedoor.Ueff() * zonedoor.Area() * (theta_i_c_max_d - theta_e_max)); }
                    else { QTsource_Door_Cmax += (zonedoor.Ueff() * zonedoor.Area() * (theta_e_max - theta_i_c_max_d)); }
                }
                QTsink_TB_max += (Utb * zonedoor.Area() * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_TB_Cmax += (Utb * zonedoor.Area() * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_TB_Cmax += (Utb * zonedoor.Area() * (theta_e_max - theta_i_c_max_d)); }
            }

            //커튼월창 QT계산            
            double[,,,] zoneCWs_QTsink = new double[zoneCW.Count, 2, 2, 12];
            double[,,,] zoneCWs_QTsource = new double[zoneCW.Count, 2, 2, 12];
            double[,,,] zoneCWs_QTsink_TB = new double[zoneCW.Count, 2, 2, 12];
            double[,,,] zoneCWs_QTsource_TB = new double[zoneCW.Count, 2, 2, 12];
            i = -1;
            while (++i < zoneCW.Count)
            {
                double U, A, Uinst;
                CW zonecw = (CW)zoneCW[i];
                Uinst = zonecw.Uinst();
                if (zonecw.CWType() == "유리부분")
                {
                    U = zonecw.Uvalue_g();
                    A = zonecw.Uvalue_g();
                }
                else if (zonecw.CWType() == "출입문부분")
                {
                    U = zonecw.Uvalue_d();
                    A = zonecw.Uvalue_d();

                }
                else
                {
                    U = zonecw.Uvalue_p();
                    A = zonecw.Uvalue_p();
                }
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int wewd = 0; wewd <= 1; wewd++)
                    {
                        for (int mth = 0; mth <= 11; mth++)
                        {
                            if (theta_i[hc, wewd, mth] >= theta_e[hc, mth])
                            {

                                zoneCWs_QTsink[i, hc, wewd, mth] = qtcalc.Calc_sink(theta_e[hc, mth], theta_i[hc, wewd, mth], U * A);
                                zoneCWs_QTsink_TB[i, hc, wewd, mth] = qtcalc.Calc_sink(theta_e[hc, mth], theta_i[hc, wewd, mth], Uinst * A);
                            }
                            else
                            {
                                zoneCWs_QTsource[i, hc, wewd, mth] = qtcalc.Calc_source(theta_e[hc, mth], theta_i[hc, wewd, mth], U * A);
                                zoneCWs_QTsource_TB[i, hc, wewd, mth] = qtcalc.Calc_source(theta_e[hc, mth], theta_i[hc, wewd, mth], Uinst * A);
                            }

                            MTH = (mth + 1).ToString() + "월";
      
                               // Program.DB.setValue(DB.type.ProjDB, "Zone_Envelope_Result", "프로젝트유형,외피번호,존번호,구조체번호,외피유형,직접간접,커튼월유형," +
                               //"난방_냉방,비이용일_이용일,월," +
                               // "HT,HT_TB," +
                               // "QTsink,QTsource," +
                               // "QT_TB_sink,QT_TB_source," +
                               // "QTsink_tot,QTsource_tot",
                               // "'1','" + zonecw.Num() + "','" + ZoneNum + "','" + zonecw.CNum() + "','" + "커튼월창" + "','" + "직접외기" + "','" + zonecw.CWType() + "','" +
                               // HC[hc] + "','" + WEWD[wewd] + "','" + (mth + 1).ToString() + "월" + "','" +
                               //(zonecw.Uvalue_g() * zonecw.Area_g()).ToString() + "','" + (zonecw.Uinst() * zonecw.Area_g()).ToString() + "','" +
                               //zoneCWs_QTsink[i, hc, wewd, mth].ToString() + "','" + zoneCWs_QTsource[i, hc, wewd, mth].ToString() + "','" +
                               //zoneCWs_QTsink_TB[i, hc, wewd, mth].ToString() + "','" + zoneCWs_QTsource_TB[i, hc, wewd, mth].ToString() + "','" +
                               //(zoneCWs_QTsink[i, hc, wewd, mth] + zoneCWs_QTsink_TB[i, hc, wewd, mth]).ToString() + "','" + (zoneCWs_QTsource[i, hc, wewd, mth] + zoneCWs_QTsource_TB[i, hc, wewd, mth]).ToString() + "'", "외피번호,난방_냉방,비이용일_이용일,월");
                            

                            QTsink_CW[hc, wewd, mth] += zoneCWs_QTsink[i, hc, wewd, mth];
                            QTsource_CW[hc, wewd, mth] += zoneCWs_QTsource[i, hc, wewd, mth];
                            QTsink_TB[hc, wewd, mth] += zoneCWs_QTsink_TB[i, hc, wewd, mth];
                            QTsource_TB[hc, wewd, mth] += zoneCWs_QTsource_TB[i, hc, wewd, mth];
                        }
                    }
                }
                QTsink_CW_max += (U * A * (theta_i_h_min - theta_e_min));

                QTsink_TB_max += (zonecw.Uinst() * A * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_CW_Cmax += (U*A * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_CW_Cmax += (U * A * (theta_e_max - theta_i_c_max_d)); }

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_TB_Cmax += (zonecw.Uinst() * A * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_TB_Cmax += (zonecw.Uinst() * A * (theta_e_max - theta_i_c_max_d)); }
            }

            //창호 QT계산
            double[,,,] zoneWins_QTsink = new double[zoneWin.Count, 2, 2, 12];
            double[,,,] zoneWins_QTsource = new double[zoneWin.Count, 2, 2, 12];
            double[,,,] zoneWins_QTsink_TB = new double[zoneWin.Count, 2, 2, 12];
            double[,,,] zoneWins_QTsource_TB = new double[zoneWin.Count, 2, 2, 12];
            i = -1;
            while (++i < zoneWin.Count)
            {
                Window zonewin = (Window)zoneWin[i];
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int wewd = 0; wewd <= 1; wewd++)
                    {
                        for (int mth = 0; mth <= 11; mth++)
                        {
                            if (zonewin.DiIndi() == "간접외기")
                            {
                                if (theta_i[hc, wewd, mth] >= theta_u[hc, wewd, mth])
                                {
                                    zoneWins_QTsink[i, hc, wewd, mth] = qtcalc.Calc_sink(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], zonewin.Uvalue() * zonewin.Area());
                                    zoneWins_QTsink_TB[i, hc, wewd, mth] = qtcalc.Calc_sink(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], zonewin.Uinst() * zonewin.Area());
                                }
                                else
                                {
                                    zoneWins_QTsource[i, hc, wewd, mth] = qtcalc.Calc_source(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], zonewin.Uvalue() * zonewin.Area());
                                    zoneWins_QTsource_TB[i, hc, wewd, mth] = qtcalc.Calc_source(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], zonewin.Uinst() * zonewin.Area());
                                }
                            }
                            else
                            {
                                if (theta_i[hc, wewd, mth] >= theta_e[hc, mth])
                                {
                                    zoneWins_QTsink[i, hc, wewd, mth] = qtcalc.Calc_sink(theta_e[hc, mth], theta_i[hc, wewd, mth], zonewin.Uvalue() * zonewin.Area());
                                    zoneWins_QTsink_TB[i, hc, wewd, mth] = qtcalc.Calc_sink(theta_e[hc, mth], theta_i[hc, wewd, mth], zonewin.Uinst() * zonewin.Area());
                                }
                                else
                                {
                                    zoneWins_QTsource[i, hc, wewd, mth] = qtcalc.Calc_source(theta_e[hc, mth], theta_i[hc, wewd, mth], zonewin.Uvalue() * zonewin.Area());
                                    zoneWins_QTsource_TB[i, hc, wewd, mth] = qtcalc.Calc_source(theta_e[hc, mth], theta_i[hc, wewd, mth], zonewin.Uinst() * zonewin.Area());
                                }
                            }
                            MTH = (mth + 1).ToString() + "월";

          
                                //    Program.DB.setValue(DB.type.ProjDB, "Zone_Envelope_Result", "프로젝트유형,외피번호,존번호,구조체번호,외피유형,직접간접," +
                                //"난방_냉방,비이용일_이용일,월," +
                                //"HT,HT_TB," +
                                //"QTsink,QTsource," +
                                //"QT_TB_sink,QT_TB_source," +
                                //"QTsink_tot,QTsource_tot",
                                //"'1','" + zonewin.Num() + "','" + ZoneNum + "','" + zonewin.CNum() + "','" + "창호" + "','" + zonewin.DiIndi() + "','" +
                                // HC[hc] + "','" + WEWD[wewd] + "','" + MTH + "','" +
                                //(zonewin.Uvalue() * zonewin.Area()).ToString() + "','" + (Utb * zonewin.Area()).ToString() + "','" +
                                //zoneWins_QTsink[i, hc, wewd, mth].ToString() + "','" + zoneWins_QTsource[i, hc, wewd, mth].ToString() + "','" +
                                //zoneWins_QTsink_TB[i, hc, wewd, mth].ToString() + "','" + zoneWins_QTsource_TB[i, hc, wewd, mth].ToString() + "','" +
                                //zoneWins_QTsink[i, hc, wewd, mth].ToString() + "','" + zoneWins_QTsource[i, hc, wewd, mth].ToString() + "'", "외피번호,난방_냉방,비이용일_이용일,월");
                            

                            QTsink_Win[hc, wewd, mth] += zoneWins_QTsink[i, hc, wewd, mth];
                            QTsource_Win[hc, wewd, mth] += zoneWins_QTsource[i, hc, wewd, mth];
                            QTsink_TB[hc, wewd, mth] += zoneWins_QTsink_TB[i, hc, wewd, mth];
                            QTsource_TB[hc, wewd, mth] += zoneWins_QTsource_TB[i, hc, wewd, mth];
                        }
                    }
                }
                if (zonewin.DiIndi() == "간접외기")
                {
                    QTsink_Win_max += (zonewin.Uvalue() * zonewin.Area() * (theta_i_h_min - (theta_i_h_min - 0.8 * (theta_i_h_min - theta_e_min))));

                    if (theta_i_c_max_d > (theta_i_c_max_d - 0.8 * (theta_i_c_max_d - theta_e_max)))
                    { QTsink_Win_Cmax += (zonewin.Uvalue() * zonewin.Area() * (theta_i_c_max_d - (theta_i_c_max_d - 0.8 * (theta_i_c_max_d - theta_e_max)))); }
                    else { QTsource_Win_Cmax += (zonewin.Uvalue() * zonewin.Area() * ((theta_i_c_max_d - 0.8 * (theta_i_c_max_d - theta_e_max)) - theta_i_c_max_d)); }
                }
                else
                {
                    QTsink_Win_max += (zonewin.Uvalue() * zonewin.Area() * (theta_i_h_min - theta_e_min));

                    if (theta_i_c_max_d > theta_e_max)
                    { QTsink_Win_Cmax += (zonewin.Uvalue() * zonewin.Area() * (theta_i_c_max_d - theta_e_max)); }
                    else { QTsource_Win_Cmax += (zonewin.Uvalue() * zonewin.Area() * (theta_e_max - theta_i_c_max_d)); }
                }
                QTsink_TB_max += (zonewin.Uinst() * zonewin.Area() * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_TB_Cmax += (zonewin.Uinst() * zonewin.Area() * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_TB_Cmax += (zonewin.Uinst() * zonewin.Area() * (theta_e_max - theta_i_c_max_d)); }
            }


            // QT_tot계산
            for (int hc = 0; hc <= 1; hc++)
            {
                for (int wewd = 0; wewd <= 1; wewd++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {
                        
                        QTsink_tot[hc, wewd, mth] = QTsink_TB[hc, wewd, mth] + QTsink_Wall[hc, wewd, mth] + QTsink_Roof[hc, wewd, mth] + QTsink_Door[hc, wewd, mth] + QTsink_Win[hc, wewd, mth] + QTsink_CW[hc, wewd, mth] + QTsink_Floor[hc, wewd, mth] + QTsink_GWall[hc, wewd, mth] ;
                        if( QTsink_tot[hc, wewd, mth] > 0)
                        {
                            QTsink_tot[hc, wewd, mth] = QTsink_tot[hc, wewd, mth] + QT_u_sink[hc, wewd, mth];
                        }
                        else
                        {
                            QT_u_sink[hc, wewd, mth] = 0;
                            QTsink_tot[hc, wewd, mth] = QTsink_tot[hc, wewd, mth] + QT_u_sink[hc, wewd, mth];
                        }
                        
                        QTsource_tot[hc, wewd, mth] = QTsource_TB[hc, wewd, mth] + QTsource_Wall[hc, wewd, mth] + QTsource_Roof[hc, wewd, mth] + QTsource_Door[hc, wewd, mth] + QTsource_Win[hc, wewd, mth] + QTsource_CW[hc, wewd, mth] + QTsource_Floor[hc, wewd, mth] + QTsource_GWall[hc, wewd, mth] ;
                        if (QTsource_tot[hc, wewd, mth] > 0)
                        {
                            QTsource_tot[hc, wewd, mth] = QTsource_tot[hc, wewd, mth] +  QT_u_source[hc, wewd, mth];
                        }
                        else
                        {
                            QTsource_tot[hc, wewd, mth] = 0;
                            QTsource_tot[hc, wewd, mth] = QTsource_tot[hc, wewd, mth] + QT_u_source[hc, wewd, mth];
                        }
                    }
                }
            }
            QTsink_tot_max = QTsink_TB_max + QTsink_Wall_max + QTsink_Roof_max + QTsink_Door_max + QTsink_Win_max + QTsink_CW_max + QTsink_Floor_max + QTsink_GWall_max + QT_u_sink_max;
            QTsink_tot_Cmax = QTsink_TB_Cmax + QTsink_Wall_Cmax + QTsink_Roof_Cmax + QTsink_Door_Cmax + QTsink_Win_Cmax + QTsink_CW_Cmax +  QTsink_Floor_Cmax + QTsink_GWall_Cmax + QT_u_sink_Cmax;
            QTsource_tot_Cmax = QTsource_TB_Cmax + QTsource_Wall_Cmax + QTsource_Roof_Cmax + QTsource_Door_Cmax + QTsource_Win_Cmax + QTsource_CW_Cmax + QTsource_Floor_Cmax + QTsource_GWall_Cmax + QT_u_source_Cmax;
        }

        public void ZoneQSop()// 불투명 일사 계산
        {
            //외벽 일사 계산
            double[,] zoneWalls_Is = new double[zoneWall.Count, 12];
            double[,] zoneWalls_Qssink = new double[zoneWall.Count, 12];
            double[,] zoneWalls_Qssource = new double[zoneWall.Count, 12];
            double[] zoneWalls_Qssink_Cmax = new double[zoneWall.Count];
            double[] zoneWalls_Qssource_Cmax = new double[zoneWall.Count];
            String[] HC = { "난방", "냉방" };
            String[] WEWD = { "비이용일", "이용일" };
            String MTH;

            {
                int i = -1;
                while (++i < zoneWall.Count)
                {
                    Wall zonewall = (Wall)zoneWall[i];
                    string[][] token = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_전일사량", "일사량", "지역명 ='" + Location[0][0] + "' AND 방향 ='" + zonewall.Direction() + "' AND  각도 = '" + zonewall.Degree() + "˚" + "'");
                    string[][] token2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_부하", "일사량", "지역명 ='" + Location[0][0] + "' AND 방향 ='" + zonewall.Direction() + "'");
                    if (token.Length > 0 && token2.Length > 0)
                    {
                        for (int mth = 0; mth <= 11; mth++)
                        {
                            zoneWalls_Is[i, mth] = Convert.ToDouble(token[mth][0]);
                            QSopCalc qsopcalc = new QSopCalc();
                            QTCalc qtcalc = new QTCalc();
                            if (zonewall.DiIndi() == "간접외기")
                            {   //직접외기 벽만 일사 계산      

                            }
                            else
                            {
                                if (0.5 * 4.5 * 10 >= zonewall.α() * zoneWalls_Is[i, mth])
                                {
                                    zoneWalls_Qssink[i, mth] = qsopcalc.Calc(zonewall.Ueff(), zonewall.Area(), zonewall.α(), zoneWalls_Is[i, mth], 0.5);

                                    zoneWalls_Qssource[i, mth] = 0;
                                }
                                else
                                {
                                    zoneWalls_Qssink[i, mth] = 0;
                                    zoneWalls_Qssource[i, mth] = qsopcalc.Calc(zonewall.Ueff(), zonewall.Area(), zonewall.α(), zoneWalls_Is[i, mth], 0.5);
                                }
                                if (0.5 * 4.5 * 10 >= zonewall.α() * Convert.ToDouble(token2[0][0]))
                                {
                                    zoneWalls_Qssink_Cmax[i] = qsopcalc.Calc_max(zonewall.Ueff(), zonewall.Area(), zonewall.α(), Convert.ToDouble(token2[0][0]), 0.5);
                                }
                                else
                                {
                                    zoneWalls_Qssource_Cmax[i] = qsopcalc.Calc_max(zonewall.Ueff(), zonewall.Area(), zonewall.α(), Convert.ToDouble(token2[0][0]), 0.5);
                                }

                            }
                            QSopsink_Wall[mth] += zoneWalls_Qssink[i, mth];
                            QSopsource_Wall[mth] += zoneWalls_Qssource[i, mth];

                           // Program.DB.querySQL(DB.type.ProjDB, "UPDATE Zone_Envelope_Result SET QSsink='" + zoneWalls_Qssink[i, mth].ToString() + "', QSsource ='" + zoneWalls_Qssource[i, mth].ToString() + "' where 외피번호 = '" + zonewall.Num() + "'AND 난방_냉방 ='" + HC + "'  AND 비이용일_이용일 ='" + WEWD + "' AND 월 ='" + (mth + 1).ToString() + "월'");
                        }
                        QSopsink_tot_Cmax += zoneWalls_Qssink_Cmax[i];
                        QSopsource_tot_Cmax += zoneWalls_Qssource_Cmax[i];
                    }
                }
            }


            //지붕 일사 계산
            double[,] zoneRoofs_Is = new double[zoneRoof.Count, 12];
            double[,] zoneRoofs_Qssink = new double[zoneRoof.Count, 12];
            double[,] zoneRoofs_Qssource = new double[zoneRoof.Count, 12];
            double[] zoneRoofs_Qssink_Cmax = new double[zoneRoof.Count];
            double[] zoneRoofs_Qssource_Cmax = new double[zoneRoof.Count];

            {
                int i = -1;
                while (++i < zoneRoof.Count)
                {
                    Roof zoneroof = (Roof)zoneRoof[i];
                    string[][] token = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_전일사량", "일사량", "지역명 ='" + Location[0][0] + "' AND 방향 ='" + zoneroof.Direction() + "' AND  각도 = '" + zoneroof.Degree() + "˚" + "'");
                    string[][] token2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_부하", "일사량", "지역명 ='" + Location[0][0] + "' AND 방향 ='" + zoneroof.Direction() + "'");
                    if (token.Length > 0 && token2.Length > 0)
                    {
                        for (int mth = 0; mth <= 11; mth++)
                        {
                            zoneRoofs_Is[i, mth] = Convert.ToDouble(token[mth][0]);
                            QSopCalc qsopcalc = new QSopCalc();
                            QTCalc qtcalc = new QTCalc();
                            if (zoneroof.DiIndi() == "간접외기")
                            {   //직접외기 지붕만 일사 계산

                            }
                            else
                            {
                                if (1 * 4.5 * 10 >= zoneroof.α() * zoneRoofs_Is[i, mth])
                                {
                                    zoneRoofs_Qssink[i, mth] = qsopcalc.Calc(zoneroof.Ueff(), zoneroof.Area(), zoneroof.α(), zoneRoofs_Is[i, mth], 1);
                                    zoneRoofs_Qssource[i, mth] = 0;
                                }
                                else
                                {
                                    zoneRoofs_Qssink[i, mth] = 0;
                                    zoneRoofs_Qssource[i, mth] = qsopcalc.Calc(zoneroof.Ueff(), zoneroof.Area(), zoneroof.α(), zoneRoofs_Is[i, mth], 1);
                                }
                                if (1 * 4.5 * 10 >= zoneroof.α() * Convert.ToDouble(token2[0][0]))
                                {
                                    zoneRoofs_Qssink_Cmax[i] = qsopcalc.Calc_max(zoneroof.Ueff(), zoneroof.Area(), zoneroof.α(), Convert.ToDouble(token2[0][0]), 1);
                                }
                                else
                                {
                                    zoneRoofs_Qssource_Cmax[i] = qsopcalc.Calc_max(zoneroof.Ueff(), zoneroof.Area(), zoneroof.α(), Convert.ToDouble(token2[0][0]), 1);
                                }

                            }
                            QSopsink_Roof[mth] += zoneRoofs_Qssink[i, mth];
                            QSopsource_Roof[mth] += zoneRoofs_Qssource[i, mth];

                          //  Program.DB.querySQL(DB.type.ProjDB, "UPDATE Zone_Envelope_Result SET QSsink='" + zoneRoofs_Qssink[i, mth].ToString() + "', QSsource ='" + zoneRoofs_Qssource[i, mth].ToString() + "' where 외피번호 = '" + zoneroof.Num() + "'AND 난방_냉방 ='" + HC + "'  AND 비이용일_이용일 ='" + WEWD + "' AND 월 ='" + (mth + 1).ToString() + "월'");
                        }

                        QSopsink_tot_Cmax += zoneRoofs_Qssink_Cmax[i];
                        QSopsource_tot_Cmax += zoneRoofs_Qssource_Cmax[i];
                    }
                }
            }


            //출입문 일사 계산
            double[,] zoneDoors_Is = new double[zoneDoor.Count, 12];
            double[,] zoneDoors_Qssink = new double[zoneDoor.Count, 12];
            double[,] zoneDoors_Qssource = new double[zoneDoor.Count, 12];
            double[] zoneDoors_Qssink_Cmax = new double[zoneDoor.Count];
            double[] zoneDoors_Qssource_Cmax = new double[zoneDoor.Count];
            {
                int i = -1;
                while (++i < zoneDoor.Count)
                {
                    Door zonedoor = (Door)zoneDoor[i];
                    string[][] token = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_전일사량", "일사량", "지역명 ='" + Location[0][0] + "'  AND 방향 ='" + zonedoor.Direction() + "' AND  각도 = '" + zonedoor.Degree() + "˚" + "'");
                    string[][] token2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_부하", "일사량", "지역명 ='" + Location[0][0] + "' AND 방향 ='" + zonedoor.Direction() + "'");
                    if (token.Length > 0 && token2.Length > 0)
                    {
                        for (int mth = 0; mth < 12; mth++)
                        {
                            zoneDoors_Is[i, mth] = Convert.ToDouble(token[mth][0]);
                            QSopCalc qsopcalc = new QSopCalc();
                            QTCalc qtcalc = new QTCalc();
                            if (zonedoor.DiIndi() == "간접외기")
                            {   //직접외기 벽만 일사 계산    

                            }
                            else
                            {
                                if (0.5 * 4.5 * 10 >= zonedoor.α() * zoneDoors_Is[i, mth])
                                {
                                    zoneDoors_Qssink[i, mth] = qsopcalc.Calc(zonedoor.Ueff(), zonedoor.Area(), zonedoor.α(), zoneDoors_Is[i, mth], 0.5);
                                    zoneDoors_Qssource[i, mth] = 0;
                                }
                                else
                                {
                                    zoneDoors_Qssink[i, mth] = 0;
                                    zoneDoors_Qssource[i, mth] = qsopcalc.Calc(zonedoor.Ueff(), zonedoor.Area(), zonedoor.α(), zoneDoors_Is[i, mth], 0.5);
                                }
                                if (0.5 * 4.5 * 10 >= zonedoor.α() * Convert.ToDouble(token2[0][0]))
                                {
                                    zoneDoors_Qssink_Cmax[i] = qsopcalc.Calc_max(zonedoor.Ueff(), zonedoor.Area(), zonedoor.α(), Convert.ToDouble(token2[0][0]), 0.5);
                                }
                                else
                                {
                                    zoneDoors_Qssource_Cmax[i] = qsopcalc.Calc_max(zonedoor.Ueff(), zonedoor.Area(), zonedoor.α(), Convert.ToDouble(token2[0][0]), 0.5);
                                }

                            }
                            QSopsink_Door[mth] += zoneDoors_Qssink[i, mth];
                            QSopsource_Door[mth] += zoneDoors_Qssource[i, mth];
                         //   Program.DB.querySQL(DB.type.ProjDB, "UPDATE Zone_Envelope_Result SET QSsink='" + zoneDoors_Qssink[i, mth].ToString() + "', QSsource ='" + zoneDoors_Qssource[i, mth].ToString() + "' where 외피번호 = '" + zonedoor.Num() + "'AND 난방_냉방 ='" + HC + "'  AND 비이용일_이용일 ='" + WEWD + "' AND 월 ='" + (mth + 1).ToString() + "월'");
                        }
                        QSopsink_tot_Cmax += zoneDoors_Qssink_Cmax[i];
                        QSopsource_tot_Cmax += zoneDoors_Qssource_Cmax[i];
                    }
                }
            }

            //커튼월 패널 일사 계산
            double[,] zoneCWs_Is = new double[zoneCW.Count, 12];
            double[,] zoneCWs_Qssink = new double[zoneCW.Count, 12];
            double[,] zoneCWs_Qssource = new double[zoneCW.Count, 12];
            double[] zoneCWs_Qssink_Cmax = new double[zoneCW.Count];
            double[] zoneCWs_Qssource_Cmax = new double[zoneCW.Count];
            {
                int i = -1;
                while (++i < zoneCW.Count)
                {
                    CW zonecw = (CW)zoneCW[i];
                    if (zonecw.CWType() == "패널부분")
                    {
                        string[][] token = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_전일사량", "일사량", "지역명 ='" + Location[0][0] + "'  AND 방향 ='" + zonecw.Direction() + "' AND  각도 = '" + zonecw.Degree() + "˚" + "'");
                        string[][] token2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_부하", "일사량", "지역명 ='" + Location[0][0] + "' AND 방향 ='" + zonecw.Direction() + "'");
                        if (token.Length > 0 && token2.Length > 0)
                        {
                            for (int mth = 0; mth <= 11; mth++)
                            {
                                zoneCWs_Is[i, mth] = Convert.ToDouble(token[mth][0]);
                                QSopCalc qsopcalc = new QSopCalc();
                                QTCalc qtcalc = new QTCalc();
                                if (0.5 * 4.5 * 10 >= zonecw.α_p() * zoneCWs_Is[i, mth])
                                {
                                    zoneCWs_Qssink[i, mth] = qsopcalc.Calc(zonecw.Uvalue_p(), zonecw.Area_p(), zonecw.α_p(), zoneCWs_Is[i, mth], 0.5);
                                    zoneCWs_Qssource[i, mth] = 0;
                                }
                                else
                                {
                                    zoneCWs_Qssink[i, mth] = 0;
                                    zoneCWs_Qssource[i, mth] = qsopcalc.Calc(zonecw.Uvalue_p(), zonecw.Area_p(), zonecw.α_p(), zoneCWs_Is[i, mth], 0.5);
                                }
                                if (0.5 * 4.5 * 10 >= zonecw.α_p() * Convert.ToDouble(token2[0][0]))
                                {
                                    zoneCWs_Qssink_Cmax[i] = qsopcalc.Calc_max(zonecw.Uvalue_p(), zonecw.Area_p(), zonecw.α_p(), Convert.ToDouble(token2[0][0]), 0.5);
                                }
                                else
                                {
                                    zoneCWs_Qssource_Cmax[i] = qsopcalc.Calc_max(zonecw.Uvalue_p(), zonecw.Area_p(), zonecw.α_p(), Convert.ToDouble(token2[0][0]), 0.5);
                                }
                                QSopsink_CW_p[mth] += zoneCWs_Qssink[i, mth];
                                QSopsource_CW_p[mth] += zoneCWs_Qssource[i, mth];

                             //   Program.DB.querySQL(DB.type.ProjDB, "UPDATE Zone_Envelope_Result SET QSsink='" + zoneCWs_Qssink[i, mth].ToString() + "', QSsource ='" + zoneCWs_Qssource[i, mth].ToString() + "' where 외피번호 = '" + zonecw.Num() + "'AND 난방_냉방 ='" + HC + "'  AND 비이용일_이용일 ='" + WEWD + "' AND 월 ='" + (mth + 1).ToString() + "월' AND 커튼월유형 ='패널부분'");
                            }

                        }
                        QSopsink_tot_Cmax += zoneCWs_Qssink_Cmax[i];
                        QSopsource_tot_Cmax += zoneCWs_Qssource_Cmax[i];
                    }
                }

            }

            //불투명일사 합계 계산
            for (int hc = 0; hc <= 1; hc++)
            {
                for (int wewd = 0; wewd <= 1; wewd++)
                {
                    for (int mth = 0; mth < 12; mth++)
                    {
                        QSopsink_tot[hc, wewd, mth] = QSopsink_Wall[mth] + QSopsink_Roof[mth] + QSopsink_Door[mth] + QSopsink_CW_p[mth];
                        QSopsource_tot[hc, wewd, mth] = QSopsource_Wall[mth] + QSopsource_Roof[mth] + QSopsource_Door[mth] + QSopsource_CW_p[mth];
                    }
                }
            }
        }

        public void ZoneQStr_Win() //창호 일사 계산
        {
            double[,] zoneWins_Is = new double[zoneWin.Count, 12];
            double[] zoneWins_Is_max = new double[zoneWin.Count];
            double[,] zoneWins_Fs = new double[zoneWin.Count, 12];
            double[,] zoneWins_a = new double[zoneWin.Count, 12];
            double[,,] zoneWins_geff = new double[zoneWin.Count, 2, 12];
            double[,,] zoneWins_Qs = new double[zoneWin.Count, 2, 12];
            double[] zoneWins_geff_max = new double[zoneWin.Count];
            double[] zoneWins_Qs_max = new double[zoneWin.Count];
            String[] HC = { "난방", "냉방" };
            String[] WEWD = { "비이용일", "이용일" };

            //존의 창별 일사정보 가져오기
            int i = -1;

            while (++i < zoneWin.Count)
            {
                Window zonewin = (Window)zoneWin[i];
                string[][] token = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_전일사량", "일사량", "지역명 ='" + Location[0][0] + "' AND 방향 ='" + zonewin.Direction() + "' AND  각도 = '" + zonewin.Degree() + "˚" + "'");
                string[][] token2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_부하", "일사량", "지역명 ='" + Location[0][0] + "' AND 방향 ='" + zonewin.Direction() + "'");
                if (token.Length > 0 && token2.Length > 0)
                {
                    for (int mth = 0; mth < 12; mth++)
                    {
                        zoneWins_Is[i, mth] = Convert.ToDouble(token[mth][0]);
                        zoneWins_Is_max[i] = Convert.ToDouble(token2[0][0]);
                    }
                }
            }

            //존의 창별 음영정보 가져오기
            i = -1;

            while (++i < zoneWin.Count)
            {
                Window zonewin = (Window)zoneWin[i];
                for (int mth = 0; mth < 12; mth++)
                {
                    string[][] Shade = Program.DB.querySQL(DB.type.ProjDB, "Select a.음영계수 From Shade_3D AS a INNER JOIN ZoneEnvelope_3D AS b on a.번호 = b.아이디 where b.번호= '" + zonewin.Num() + "' And 유형 ='최종음영' And 월 = '" + (mth + 1).ToString() + "월'");
                    if (Shade.Length > 0)
                    { zoneWins_Fs[i, mth] = Convert.ToDouble(Shade[0][0]); }
                    else
                    {
                        zoneWins_Fs[i, mth] = 1;
                    }

                }
            }

            //존의 창별 가동계수정보 가져오기
            i = -1;

            while (++i < zoneWin.Count)
            {
                Window zonewin = (Window)zoneWin[i];
                String[][] BlindValue = Program.DB.querySQL(DB.type.ProjDB, "select a.제어방식2 FROM ConstructionBlind AS a INNER JOIN Blind_3D AS b ON a.번호 = b.차양번호 where b.번호 = '" + zonewin.Num() + "'");

                if (BlindValue.Length > 0)
                {
                    for (int mth = 0; mth < 12; mth++)
                    {
                        string[][] Blind_a = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_차양가동계수_" + BlindValue[0][0], "계수", "지역명= '" + Location[0][0] + "' And 방향 ='" + zonewin.Direction() + "' And 기간 = '" + (mth + 1).ToString() + "월'");
                        if (Blind_a.Length > 0)
                        { zoneWins_a[i, mth] = Convert.ToDouble(Blind_a[0][0]); }
                        else
                        {
                            zoneWins_a[i, mth] = 0;
                        }
                    }
                }
            }

            // 창 일사 계산 
            {
                i = -1;
                while (++i < zoneWin.Count)
                {
                    for (int wewd = 0; wewd <= 1; wewd++)
                    {
                        Window zonewin = (Window)zoneWin[i];
                        for (int mth = 0; mth < 12; mth++)
                        {
                            GeffCalc geffcalc = new GeffCalc();
                            QStrCalc qstrcalc = new QStrCalc();
                            zoneWins_geff[i, 0, mth] = geffcalc.Calc(zonewin.g(), zoneWins_Fs[i, mth]);
                            zoneWins_geff[i, 1, mth] = geffcalc.Calc(zonewin.g(), zoneWins_Fs[i, mth]);
                            zoneWins_geff[i, 1, mth] = geffcalc.Calc(zonewin.g(), zoneWins_Fs[i, mth], zonewin.gtot(), zoneWins_a[i, mth]);
                            zoneWins_geff_max[i] = geffcalc.Calc(zonewin.g(), 1);
                            if (zonewin.DiIndi() == "간접외기")
                            {   //직접외기 창만 일사 계산      

                            }
                            else
                            {
                                zoneWins_Qs[i, 0, mth] = qstrcalc.Calc(zonewin.Ff(), zonewin.Area(), zoneWins_geff[i, 0, mth], zoneWins_Is[i, mth]);
                                zoneWins_Qs[i, 1, mth] = qstrcalc.Calc(zonewin.Ff(), zonewin.Area(), zoneWins_geff[i, 1, mth], zoneWins_Is[i, mth]);
                                zoneWins_Qs_max[i] = qstrcalc.Calc_max(zonewin.Ff(), zonewin.Area(), zoneWins_geff_max[i], zoneWins_Is_max[i]);

                            }

                            //  Program.DB.querySQL(DB.type.ProjDB, "UPDATE Zone_Envelope_Result SET QSsink='" + 0.ToString() + "', QSsource ='" + zoneWins_Qs[i, wewd, mth].ToString() + "' where 외피번호 = '" + zonewin.Num() + "'AND 난방_냉방 ='" + HC + "'  AND 비이용일_이용일 ='" + WEWD + "' AND 월 ='" + (mth + 1).ToString() + "월'");
                        }
                    }
                    for (int mth = 0; mth < 12; mth++)
                    {
                        QStr_Win[0, mth] += zoneWins_Qs[i, 0, mth];
                        QStr_Win[1, mth] += zoneWins_Qs[i, 1, mth];
                    }
                    QStr_Win_max += zoneWins_Qs_max[i];
                }
            }
        }
        public void ZoneQStr_CW() //투명구조체 일사 계산
        {
            double[,] zoneCWs_Is = new double[zoneCW.Count, 12];
            double[,] zoneCWs_Fs = new double[zoneCW.Count, 12];
            double[,] zoneCWs_a = new double[zoneCW.Count, 12];
            double[,,] zoneCWs_g_geff = new double[zoneCW.Count, 2, 12];
            double[,] zoneCWs_d_geff = new double[zoneCW.Count, 12];
            double[,,] zoneCWs_g_Qs = new double[zoneCW.Count, 2, 12];
            double[,] zoneCWs_d_Qs = new double[zoneCW.Count, 12];

            double[] zoneCWs_Is_max = new double[zoneCW.Count];
            double[] zoneCWs_g_geff_max = new double[zoneCW.Count];
            double[] zoneCWs_d_geff_max = new double[zoneCW.Count];
            double[] zoneCWs_g_Qs_max = new double[zoneCW.Count];
            double[] zoneCWs_d_Qs_max = new double[zoneCW.Count];

            //존의 커튼월별 일사정보 가져오기
            int i = -1;
            while (++i < zoneCW.Count)
            {
                CW zonecw = (CW)zoneCW[i];
                string[][] token = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_전일사량", "일사량", "지역명 ='" + Location[0][0] + "' AND 방향 ='" + zonecw.Direction() + "' AND  각도 = '" + zonecw.Degree() + "˚" + "'");
                string[][] token2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_부하", "일사량", "지역명 ='" + Location[0][0] + "' AND 방향 ='" + zonecw.Direction() + "'");
                if (token.Length > 0 && token2.Length > 0)
                {
                    for (int mth = 0; mth < 12; mth++)
                    {
                        zoneCWs_Is[i, mth] = Convert.ToDouble(token[mth][0]);
                        zoneCWs_Is_max[i] = Convert.ToDouble(token2[0][0]);
                    }
                }
            }

            //존의 커튼월별 음영정보 가져오기
            {
                i = -1;

                while (++i < zoneCW.Count)
                {
                    CW zonecw = (CW)zoneCW[i];
                    for (int mth = 0; mth < 12; mth++)
                    {
                        string[][] Shade = Program.DB.querySQL(DB.type.ProjDB, "Select a.음영계수 From Shade_3D AS a INNER JOIN ZoneEnvelope_3D AS b on a.번호 = b.아이디 where b.번호= '" + zonecw.Num() + "' And 유형 ='최종음영' And 월 = '" + (mth + 1).ToString() + "월'");
                        if (Shade.Length > 0)
                        { zoneCWs_Fs[i, mth] = Convert.ToDouble(Shade[0][0]); }
                        else
                        {
                            zoneCWs_Fs[i, mth] = 1;
                        }
                    }
                }
            }
            //존의 커튼월별 가동계수정보 가져오기
            {
                i = -1;

                while (++i < zoneCW.Count)
                {
                    CW zonecw = (CW)zoneCW[i];
                    String[][] BlindValue = Program.DB.querySQL(DB.type.ProjDB, "select a.제어방식2 FROM ConstructionBlind AS a INNER JOIN Blind_3D AS b ON a.번호 = b.차양번호 where b.번호 = '" + zonecw.Num() + "'");
                    if(BlindValue.Length > 0) 
                    {
                        for (int mth = 0; mth < 12; mth++)
                        {
                            if (BlindValue.Length > 0)
                            {
                                string[][] Blind_a = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_차양가동계수_" + BlindValue[0][0], "계수", "지역명= '" + Location[0][0] + "' And 방향 ='" + zonecw.Direction() + "' And 기간 = '" + (mth + 1).ToString() + "월'");
                                if (Blind_a.Length > 0)
                                { zoneCWs_a[i, mth] = Convert.ToDouble(Blind_a[0][0]); }
                                else
                                {
                                    zoneCWs_a[i, mth] = 0;
                                }
                            }
                            else
                            {
                                zoneCWs_a[i, mth] = 0;
                            }
                        }
                    }
                }
            }

            // 커튼월 일사 계산 
            for (i = 0; i < zoneCW.Count; i++)
            {
                CW zonecw = (CW)zoneCW[i];

                for (int wewd = 0; wewd <= 1; wewd++)
                {
                    for (int mth = 0; mth < 12; mth++)
                    {
                        GeffCalc geffcalc = new GeffCalc();
                        QStrCalc qstrcalc = new QStrCalc();
                        QTCalc qtcalc = new QTCalc();
                        zoneCWs_g_geff[i, 0, mth] = geffcalc.Calc(zonecw.g_g(), zoneCWs_Fs[i, mth]);    //비이용일   
                        zoneCWs_g_geff[i, 1, mth] = geffcalc.Calc(zonecw.g_g(), zoneCWs_Fs[i, mth]);    //이용일 차양없을 경우	
                        zoneCWs_g_geff[i, 1, mth] = geffcalc.Calc(zonecw.g_g(), zoneCWs_Fs[i, mth], zonecw.gtot_g(), zoneCWs_a[i, mth]);     //이용일 차양있을 경우
                        zoneCWs_d_geff[i, mth] = geffcalc.Calc(zonecw.g_d(), zoneCWs_Fs[i, mth]); //출입문

                        zoneCWs_g_geff_max[i] = geffcalc.Calc(zonecw.g_g(), 1);    //부하   
                        zoneCWs_d_geff_max[i] = geffcalc.Calc(zonecw.g_d(), 1);    //부하   

                        zoneCWs_g_Qs[i, 0, mth] = qstrcalc.Calc(zonecw.Ff_g(), zonecw.Area_g(), zoneCWs_g_geff[i, 0, mth], zoneCWs_Is[i, mth]);
                        zoneCWs_g_Qs[i, 1, mth] = qstrcalc.Calc(zonecw.Ff_g(), zonecw.Area_g(), zoneCWs_g_geff[i, 1, mth], zoneCWs_Is[i, mth]);
                        zoneCWs_d_Qs[i, mth] = qstrcalc.Calc(zonecw.Ff_d(), zonecw.Area_d(), zoneCWs_d_geff[i, mth], zoneCWs_Is[i, mth]);
                        zoneCWs_g_Qs_max[i] = qstrcalc.Calc_max(zonecw.Ff_g(), zonecw.Area_g(), zoneCWs_g_geff_max[i], zoneCWs_Is_max[i]);
                        zoneCWs_d_Qs_max[i] = qstrcalc.Calc_max(zonecw.Ff_d(), zonecw.Area_d(), zoneCWs_d_geff_max[i], zoneCWs_Is_max[i]);

                        if (zonecw.CWType() == "유리부분")
                        {
                         //   Program.DB.querySQL(DB.type.ProjDB, "UPDATE Zone_Envelope_Result SET QSsink='" + 0.ToString() + "', QSsource ='" + zoneCWs_g_Qs[i, wewd, mth].ToString() + "' where 외피번호 = '" + zonecw.Num() + "'AND 난방_냉방 ='" + HC + "'  AND 비이용일_이용일 ='" + WEWD + "' AND 월 ='" + (mth + 1).ToString() + "월' AND 커튼월유형 = '유리부분'");
                        }
                        else if (zonecw.CWType() == "출입문부분")
                        {
                          //  Program.DB.querySQL(DB.type.ProjDB, "UPDATE Zone_Envelope_Result SET QSsink='" + 0.ToString() + "', QSsource ='" + zoneCWs_d_Qs[i, mth].ToString() + "' where 외피번호 = '" + zonecw.Num() + "'AND 난방_냉방 ='" + HC + "'  AND 비이용일_이용일 ='" + WEWD + "' AND 월 ='" + (mth + 1).ToString() + "월' AND 커튼월유형 = '출입문부분'");
                        }
                        else { }
                    }
                }
                for (int mth = 0; mth < 12; mth++)
                {
                    QStr_CW[0, mth] += (zoneCWs_g_Qs[i, 0, mth] + zoneCWs_d_Qs[i, mth]);
                    QStr_CW[1, mth] += (zoneCWs_g_Qs[i, 1, mth] + zoneCWs_d_Qs[i, mth]);
                }
                QStr_CW_max += (zoneCWs_g_Qs_max[i] + zoneCWs_d_Qs_max[i]);
            }

            for (int hc = 0; hc <= 1; hc++)
            {
                for (int wewd = 0; wewd <= 1; wewd++)
                {
                    for (int mth = 0; mth < 12; mth++)
                    {
                        QStr_tot[hc, wewd, mth] = QStr_Win[wewd, mth] + QStr_CW[wewd, mth];
                    }
                }
            }
            QStr_tot_Cmax = QStr_Win_max + QStr_CW_max;
        }

        public void ZoneQV() //환기 열전달 계산
        {
            for (int hc = 0; hc <= 1; hc++)
            {
                for (int wewd = 0; wewd <= 1; wewd++)
                {
                    double[,] theta_v_mech = new double[2, 12];


                    for (int mth = 0; mth <= 11; mth++)
                    {
                        if(SelectHRV != null)
                        {
                            AHU AHU1 = Program.CALC.getAHU(SelectHRV);
                            if (AHU1 != null)
                            {
                                theta_v_mech[0, mth] = AHU1.theta_SA_hr[0, mth];
                                theta_v_mech[1, mth] = AHU1.theta_SA_hr[1, mth];
                            }
                            else
                            {
                                theta_v_mech[0, mth] = theta_e[hc, mth] + eta_V_mech[0] * (theta_i_h_set - theta_e[hc, mth]);
                                theta_v_mech[1, mth] = theta_e[hc, mth] + eta_V_mech[1] * (theta_i_h_set - theta_e[hc, mth]);
                            }
                        }
                        else
                        {
                            theta_v_mech[0, mth] = theta_e[hc, mth] + eta_V_mech[0] * (theta_i_h_set - theta_e[hc, mth]);
                            theta_v_mech[1, mth] = theta_e[hc, mth] + eta_V_mech[1] * (theta_i_h_set - theta_e[hc, mth]);
                        }
                        
                        QVCalc qvcalc = new QVCalc();
                        if (theta_i[hc, wewd, mth] >= theta_e[hc, mth])
                        {
                            QV_inf_sink[hc, wewd, mth] = qvcalc.Calc_sink(theta_e[hc, mth], theta_i[hc, wewd, mth], Zone_HV_inf[wewd]);
                            QV_z_sink[hc, wewd, mth] = qvcalc.Calc_sink(theta_e[hc, mth], theta_i[hc, wewd, mth], Zone_HV_z[wewd]);
                            QV_win_sink[hc, wewd, mth] = qvcalc.Calc_sink(theta_e[hc, mth], theta_i[hc, wewd, mth], Zone_HV_win[wewd]);
                        }
                        else if (theta_i[hc, wewd, mth] < theta_e[hc, mth])
                        {
                            QV_inf_source[hc, wewd, mth] = qvcalc.Calc_source(theta_e[hc, mth], theta_i[hc, wewd, mth], Zone_HV_inf[wewd]);
                            QV_z_source[0, 1, mth] = qvcalc.Calc_source(theta_e[hc, mth], theta_i[hc, wewd, mth], Zone_HV_z[wewd]);
                            QV_win_source[hc, wewd, mth] = qvcalc.Calc_source(theta_e[hc, mth], theta_i[hc, wewd, mth], Zone_HV_win[wewd]);
                        }

                        if (theta_i[hc, wewd, mth] >= theta_v_mech[hc, mth])
                        {
                            QV_mech_sink[hc, wewd, mth] = qvcalc.Calc_sink(theta_v_mech[hc, mth], theta_i[hc, wewd, mth], Zone_HV_mech[wewd]);
                        }
                        else
                        {
                            QV_mech_source[hc, wewd, mth] = qvcalc.Calc_source(theta_v_mech[hc, mth], theta_i[hc, wewd, mth], Zone_HV_mech[wewd]);
                        }
                        QVsink_tot[hc, wewd, mth] = QV_inf_sink[hc, wewd, mth] + QV_win_sink[hc, wewd, mth] + QV_z_sink[hc, wewd, mth] + QV_mech_sink[hc, wewd, mth];
                        QVsource_tot[hc, wewd, mth] = QV_inf_source[hc, wewd, mth] + QV_win_source[hc, wewd, mth] + QV_z_source[hc, wewd, mth] + QV_mech_source[hc, wewd, mth];
                    }
                }
            }

            QV_inf_sink_max = Zone_HV_inf[1] * (theta_i_h_min - theta_e_min);
            QV_win_sink_max = Zone_HV_win[1] * (theta_i_h_min - theta_e_min);
            QV_z_sink_max = Zone_HV_z[1] * (theta_i_h_min - theta_i_h_min);
            QV_mech_sink_max = Zone_HV_mech[1] * (theta_i_h_min - (theta_e_min + eta_V_mech[1] * (theta_i_h_min - theta_e_min)));
            QVsink_tot_max = QV_inf_sink_max + QV_win_sink_max + QV_z_sink_max ; //기계환기 제외

            HVCalc hvcalc = new HVCalc();
            if(theta_i_c_max_d > theta_e_max)
            {
                QV_inf_sink_Cmax = hvcalc.HV_Calc(n50 * e, (zoneArea * zoneHeight)) * (theta_i_c_max_d - theta_e_max);
                QV_win_sink_Cmax = hvcalc.HV_Calc(0.1, (zoneArea * zoneHeight)) * (theta_i_c_max_d - theta_e_max);
            }
            else
            {
                QV_inf_source_Cmax = hvcalc.HV_Calc(n50 * e, (zoneArea * zoneHeight)) * (theta_e_max - theta_i_c_max_d);
                QV_win_source_Cmax = hvcalc.HV_Calc(0.1, (zoneArea * zoneHeight)) * (theta_e_max - theta_i_c_max_d);
            }

            QVsink_tot_Cmax = QV_inf_sink_Cmax + QV_win_sink_Cmax;
            QVsource_tot_Cmax = QV_inf_source_Cmax + QV_win_source_Cmax;

        }
        public void ZoneQ_DHU()
        {
            double[,] X_t = new double[24, 12]; //월별 시간별 절대습도
            double[] dX_mth = new double[12]; //월별 실내외 습도차 누적

            double[,] X_mech = new double[24, 12]; //기계환기 후 급기 습도
            double[] dX_mech = new double[12]; //기계환기 급기와 실내 습도 차이 누적



            for (int mth = 1; mth < 13; mth++)
            {
                for (int h = 1; h < 25; h++)
                {
                    string[][] T_Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_시간별_외기온도", "온도", "지역명 ='" + Location[0][0] + "' And 시간 = '" + h + "' And 기간 ='" + mth + "월'");
                    string[][] X_Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_시간별_상대습도", "습도", "지역명 ='" + Location[0][0] + "' And 시간 = '" + h + "' And 기간 ='" + mth + "월'");
                    if (T_Value.Length > 0 && X_Value.Length > 0)
                    {
                        X_t[h - 1, mth - 1] = 611.2 * Math.Exp(17.62 * Convert.ToDouble(T_Value[0][0]) / (243.12 + Convert.ToDouble(T_Value[0][0]))) / 461.51 / (273.15 + Convert.ToDouble(T_Value[0][0])) / 1.2 * Convert.ToDouble(X_Value[0][0]);
                        X_mech[h - 1, mth - 1] = X_t[h - 1, mth - 1] + eta_χV_mech[1]* (xi_c_set - X_t[h - 1, mth - 1]);
                    }
                }
            }
            for (int mth = 1; mth < 13; mth++)
            {
                for (int h = 1; h < 25; h++)
                {
                    if (X_t[h - 1, mth - 1] >= xi_c_set)
                    {
                        dX_mth[mth - 1] += (X_t[h - 1, mth - 1] - xi_c_set);
                    }
                    if (X_mech[h - 1, mth - 1] >= xi_c_set)
                    {
                        dX_mech[mth - 1] += (X_mech[h - 1, mth - 1] - xi_c_set);
                    }
                }
            }

            for (int wewd = 0; wewd < 2; wewd++)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    Q_DHU_win[wewd, mth] = dX_mth[mth] * (ninf[wewd] + nwin[wewd]) * (zoneArea * zoneHeight) * 2501 * 1.2 / 3600 * 1000;
                    Q_DHU_mech[mth] = dX_mech[mth] * nmech[wewd] * (zoneArea * zoneHeight) * 2501 * 1.2 / 3600 * 1000;
                    Q_DHU_tot[wewd, mth] = Q_DHU_win[wewd, mth] + Q_DHU_mech[mth];
                }
            }
            Q_DHU_max = (X_e_max - xi_c_set) * (0.1 + n50 * e) * zoneArea * zoneHeight * 1204 * 0.68;
        }

        public void ZoneQI_L() //조명내부발열 계산
        {
            ZoneLight zonelight1 = Program.CALC.getZoneLight(ZoneNum.ToString());
            for (int hc = 0; hc <= 1; hc++)
            {
                for (int wewd = 0; wewd <= 1; wewd++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {
                        if (wewd == 0) { QI_L[hc, wewd, mth] = 0; }
                        else { QI_L[hc, wewd, mth] = zonelight1.Zone_Final_kWh[mth] * 1000 / dwd_mth[mth]; }
                       
                    }
                }
            }
        }

        public void ZoneQI() //내부발열 계산
        {
            double t_person = 0;
            string[][] value = Program.DB.getValue(DB.type.BaseDB_HCneed,"용도프로필","사람일일이용시간", "용도명='" + zoneUsage + "'");
            if(value.Length >0)
            {
                t_person = Convert.ToDouble(value[0][0]);
            }
            //비이용일
            for (int mth =0; mth <12; mth++)
            {
                
                QI_P[0,mth] = 0;
                QI_fac[0,mth] = 0;
                QI_P[1,mth] = qI_p * zoneArea * dwd_mth[mth]/1000 * t_person / twd_d;
                QI_fac[1,mth] = qI_fac * zoneArea * dwd_mth[mth] /1000;
            }
           
            //이용일

            double[] h_summer = new double[12];
            for(int mth =0; mth< 12; mth++)
            {
                h_summer[mth] = (H_winter - H_summer) / (theta_e[1,2] - theta_e[1,5]) * (theta_e[1, mth] - theta_e[1,2]) + H_winter;
            }
            

            for (int hc = 0; hc <= 1; hc++)
            {
                for (int wewd = 0; wewd <= 1; wewd++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {
                        QI_Humidity[mth] = t_person * h_summer[mth] * Peope_Num * 2260 / 3600 * dwd_mth[mth] / 1000;

                        if (hc == 1 && wewd == 1)
                        {
                            QI_tot[hc, wewd, mth] = QI_P[wewd,mth] + QI_fac[wewd, mth] + QI_L[hc, wewd, mth] + QI_Humidity[mth];
                        }
                        else { QI_tot[hc, wewd, mth] = QI_P[wewd, mth] + QI_fac[wewd, mth] + QI_L[hc, wewd, mth]; }
                    }
                }
            }
        }
        public void Zone_Theta_U() //비냉난방온도 계산 
        {
            // = IFERROR(IF(OR("비냉난방" = "냉방+난방", "비냉난방" = "난방"), "",
            //(SUM(일사, 내부발열) / 24 + 직접외기관류 * 외기온도
            //+ 간접외기 * (난방설정온도 - 0.8 * (난방설정온도 - 외기온도))
            //+ 지면위바닥 * (난방설정온도 - fx * (난방설정온도 - 외기온도))
            //+ 단열지하바닥 * (난방설정온도 - fx * (난방설정온도 - 외기온도))
            //+ 비단열지하바닥 * (난방설정온도 - fx * (난방설정온도 - 외기온도))
            //+ 지중벽체 * (난방설정온도 - fx * (난방설정온도 - 외기온도))
            //+ 인접존 * 난방설정온도 + 비냉난방존환기 * 외기) / (SUM(열전달계수합계))), "")

            double[] Theta_set = new double[2];//난방/냉방
            Theta_set[0] = theta_i_h_set; Theta_set[1] = theta_i_c_set;
            double[,] Theta_Indi = new double[2, 12];
            double[,] H_Theta_F = new double[2, 12]; //바닥 관류* 온도 누적
            double[,] H_Theta_G = new double[2, 12]; //지하벽 관류 *온도 누적

            double[] HT_z = new double[2]; //난방/냉방
            double[,,] Qsource_h = new double[2, 2, 12];
            double HT_Di_tot, HT_Indi_tot, HV_u;


            if (zoneHC == "냉방" || zoneHC == "비냉난방") //비난방이면 
            {
                HT_z[0] = 0;
                for (int i = 0; i < zoneInWall.Count; i++)
                {
                    InWall zoneInwall = (InWall)zoneInWall[i]; //List를 class 객체로 변환 
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "냉난방유무", "존번호 = '" + zoneInwall.SideZone() + "'");

                    if (Value.Length > 0)
                    {
                        if (Value[0][0] == "냉난방" || Value[0][0] == "난방") //인접한 난방존 
                        {
                            HT_z[0] += (zoneInwall.Area() * zoneInwall.U());
                        }
                    }

                }

                for (int i = 0; i < zoneSlab.Count; i++)
                {
                    Slab zoneslab = (Slab)zoneSlab[i]; //List를 class 객체로 변환 
                    string[][] Value_s = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "냉난방유무", "존번호 = '" + zoneslab.SideZone() + "'");

                    if (Value_s.Length > 0)
                    {
                        if (Value_s[0][0] == "냉난방" || Value_s[0][0] == "난방") //인접한 난방존
                        {
                            HT_z[0] += (zoneslab.Area() * zoneslab.U());
                        }
                    }
                }
            }
            if (zoneHC == "난방" || zoneHC == "비냉난방") //비냉방이면 
            {
                HT_z[1] = 0;
                for (int i = 0; i < zoneInWall.Count; i++)
                {
                    InWall zoneInwall = (InWall)zoneInWall[i]; //List를 class 객체로 변환 
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "냉난방유무", "존번호 = '" + zoneInwall.SideZone() + "'");

                    if (Value.Length > 0)
                    {
                        if (Value[0][0] == "냉난방" || Value[0][0] == "냉방") //인접한 냉방존
                        {
                            HT_z[1] += (zoneInwall.Area() * zoneInwall.U());
                        }
                    }
                }

                for (int i = 0; i < zoneSlab.Count; i++)
                {
                    Slab zoneslab = (Slab)zoneSlab[i]; //List를 class 객체로 변환 
                    string[][] Value_s = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "냉난방유무", "존번호 = '" + zoneslab.SideZone() + "'");
                    if (Value_s.Length > 0)
                    {
                        if (Value_s[0][0] == "냉난방" || Value_s[0][0] == "냉방") //인접한 냉방존
                        {
                            HT_z[1] += (zoneslab.Area() * zoneslab.U());
                        }
                    }
                }
            }

            for (int hc = 0; hc < 2; hc++)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    Theta_Indi[hc, mth] = Theta_set[hc] - 0.8 * (Theta_set[hc] - theta_e[hc, mth]);
                }
            }

            for (int i = 0; i < zoneFloor.Count; i++)
            {
                Floor zonefloor = (Floor)zoneFloor[i];
                double Theta_s;
                for (int hc = 0; hc < 2; hc++)
                {
                    for (int mth = 0; mth < 12; mth++)
                    {
                        Theta_s = Theta_set[hc] - zonefloor.Fx() * (Theta_set[hc] - theta_e[hc, mth]);
                        H_Theta_F[hc, mth] += (Theta_s * zonefloor.Ueff() * zonefloor.Area());
                    }
                }
            }
            for (int i = 0; i < zoneGWall.Count; i++)
            {
                GWall zonegwall = (GWall)zoneGWall[i];
                double Theta_g;
                for (int hc = 0; hc < 2; hc++)
                {
                    for (int mth = 0; mth < 12; mth++)
                    {
                        Theta_g = Theta_set[hc] - zonegwall.Fx() * (Theta_set[hc] - theta_e[hc, mth]);
                        H_Theta_G[hc, mth] += (Theta_g * zonegwall.Ueff() * zonegwall.Area());
                    }
                }
            }

            for (int hc = 0; hc < 2; hc++)
            {
                for (int wewd = 0; wewd < 2; wewd++)
                {
                    for (int mth = 0; mth < 12; mth++)
                    {
                        Qsource_h[hc, wewd, mth] = (QSopsource_tot[hc, wewd, mth] + QStr_tot[hc, wewd, mth] + QI_tot[hc, wewd, mth]) / 24;
                        HT_Di_tot = (Zone_HT_Di_Wall + Zone_HT_Di_Roof + Zone_HT_Di_Win + Zone_HT_Di_Door + Zone_HT_CW + Zone_HT_TB_tot); //직접외기 바닥 포함시켜야 함 
                        HT_Indi_tot = (Zone_HT_Indi_Wall + Zone_HT_Indi_Roof + Zone_HT_Indi_Win + Zone_HT_Indi_Door); //간접외기 
                        HV_u = 0.6 * (zoneArea * zoneHeight) * 0.34;
                        
                            Theta_U[hc, wewd, mth] = (Qsource_h[hc, wewd, mth] + HT_Di_tot * theta_e[hc, mth] + HT_Indi_tot * Theta_Indi[hc, mth] + H_Theta_F[hc, mth] + H_Theta_G[hc, mth] + HT_z[hc] * Theta_set[hc] + HV_u * theta_e[hc, mth]) / (HT_Di_tot + HT_Indi_tot + Zone_HT_Floor + Zone_HT_GWall + HT_z[hc] + HV_u);
                       if(Double.IsInfinity(Theta_U[hc, wewd, mth]))
                        {
                            Theta_U[0, wewd, mth] = theta_i_h_set;
                            Theta_U[1, wewd, mth] = theta_i_c_set;
                        }
                                                   

                    }
                }
            }
            for (int wewd = 0; wewd < 2; wewd++)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    if (zoneHC =="냉난방")
                    {
                         for (int hc = 0; hc < 2; hc++)
                         {
                            Theta_U[hc, wewd, mth] = theta_i[hc, wewd, mth];
                         }
                    }
                    else if (zoneHC == "난방")
                    {
                            Theta_U[0, wewd, mth] = theta_i[0, wewd, mth];
                      
                    }
                    else if (zoneHC == "냉방")
                    {
                        Theta_U[1, wewd, mth] = theta_i[1, wewd, mth];

                    }
                    else { }
                }
            }
        }


        public void Zoneeta()//이용계수 계산
        {
            eta_Calc eta_calc = new eta_Calc();
            dQc_bCalc dQc_bcalc = new dQc_bCalc();
            double awe = (1 - dwd_a / 365) * 7;

            //대차축열량 및 축열열손실 계산 
            for (int mth = 0; mth <= 11; mth++)
            {

                dwe_mth[mth] = dmth[mth] - dwd_mth[mth];

                Qsink[0, 0, mth] = QTsink_tot[0, 0, mth] + QVsink_tot[0, 0, mth] + QSopsink_tot[0, 0, mth];
                if (double.IsNaN(Qsink[0, 0, mth]) || Qsink[0, 0, mth] < 0)
                {
                    Qsink[0, 0, mth] = 0;
                }
                else { Qsink[0, 0, mth] = Qsink[0, 0, mth]; }

                Qsource[0, 0, mth] = QTsource_tot[0, 0, mth] + QVsource_tot[0, 0, mth] + QSopsource_tot[0, 0, mth] + QStr_tot[0, 0, mth] + QI_tot[0, 0, mth];
                if (double.IsNaN(Qsource[0, 0, mth]) || Qsource[0, 0, mth] < 0)
                {
                    Qsource[0, 0, mth] = 0;
                }
                else { Qsource[0, 0, mth] = Qsource[0, 0, mth]; }

                gamma[0, 0, mth] = Qsource[0, 0, mth] / Qsink[0, 0, mth];
                if (double.IsNaN(gamma[0, 0, mth]) || gamma[0, 0, mth] < 0)
                {
                    gamma[0, 0, mth] = 0;
                }
                else { gamma[0, 0, mth] = gamma[0, 0, mth]; }

                a[0, 0, mth] = 1 + tao[0,0,mth] / 16;
                if (double.IsNaN(a[0, 0, mth]) || a[0, 0, mth] < 0)
                {
                    a[0, 0, mth] = 0;
                }
                else { a[0, 0, mth] = a[0, 0, mth]; }

                eta[0, 0, mth] = eta_calc.eta_h_Calc(gamma[0, 0, mth], a[0, 0, mth]);
                if (double.IsNaN(eta[0, 0, mth]) || eta[0, 0, mth] < 0)
                {
                    eta[0, 0, mth] = 0;
                }
                else { eta[0, 0, mth] = eta[0, 0, mth]; }

                dQc_b[0, 0, mth] = dQc_bcalc.Calc(Cwirk_A * zoneArea, theta_i_h_set, theta_i[0, 0, mth], awe, dtheta_i_NA, Qsink[0, 0, mth], eta[0, 0, mth], Qsource[0, 0, mth]);
                if (double.IsNaN(dQc_b[0, 0, mth]) || dQc_b[0, 0, mth] < 0)
                {
                    dQc_b[0, 0, mth] = 0;
                }
                else { dQc_b[0, 0, mth] = dQc_b[0, 0, mth]; }

                dQc_sink[0, 1, mth] = dQc_b[0, 0, mth] * dwe_mth[mth] / dwd_mth[mth];
                if (double.IsNaN(dQc_sink[0, 1, mth]) || dQc_sink[0, 1, mth] < 0)
                {
                    dQc_sink[0, 1, mth] = 0;
                }
                else { dQc_sink[0, 1, mth] = dQc_sink[0, 1, mth]; }
            }

            for (int hc = 0; hc <= 1; hc++)
            {
                for (int wewd = 0; wewd <= 1; wewd++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {
                        Qsink[hc, wewd, mth] = QTsink_tot[hc, wewd, mth] + QVsink_tot[hc, wewd, mth] + QSopsink_tot[hc, wewd, mth];
                        Qsource[hc, wewd, mth] = QTsource_tot[hc, wewd, mth] + QVsource_tot[hc, wewd, mth] + QSopsource_tot[hc, wewd, mth] + QStr_tot[hc, wewd, mth] + QI_tot[hc, wewd, mth];

                        if (Qsink[hc, wewd, mth] == 0)
                        {
                            gamma[hc, wewd, mth] = Qsource[hc, wewd, mth] / 1;
                        }
                        else
                        {
                            gamma[hc, wewd, mth] = Qsource[hc, wewd, mth] / Qsink[hc, wewd, mth];
                        }
                        a[hc, wewd, mth] = 1 + tao[hc,wewd,mth] / 16;
                        eta[0, wewd, mth] = eta_calc.eta_h_Calc(gamma[0, wewd, mth], a[0, wewd, mth]);
                        eta[1, wewd, mth] = eta_calc.eta_c_Calc(gamma[1, wewd, mth], a[1, wewd, mth]);
                    }
                }
            }
        }

        public void ZoneQb()//요구량 계산
        {
            QbCalc qbcalc = new QbCalc();
            for (int mth = 0; mth <= 11; mth++)
            {
                //  Qhb_we_day[mth] = qbcalc.Qhb_Calc(Qsink[0, 0, mth], eta[0, 0, mth], Qsource[0, 0, mth]);
                Qhb_we_day[mth] = 0;
                Qhb_wd_day[mth] = qbcalc.Qhb_Calc(Qsink[0, 1, mth], eta[0, 1, mth], Qsource[0, 1, mth]);
                // Qcb_we_day[mth] = qbcalc.Qcb_Calc(eta[1, 0, mth], Qsource[1, 0, mth]);
                Qcb_we_day[mth] = 0;
                Qcb_wd_day[mth] = qbcalc.Qcb_Calc(eta[1, 1, mth], Qsource[1, 1, mth]) + Q_DHU_tot[1, mth];

                if (double.IsNaN(Qhb_we_day[mth]) || Qhb_we_day[mth] < 0)
                {
                    Qhb_we_day[mth] = 0;
                }
                else { Qhb_we_day[mth] = Qhb_we_day[mth]; }

                if (double.IsNaN(Qhb_wd_day[mth]) || Qhb_wd_day[mth] < 0)
                {
                    Qhb_wd_day[mth] = 0;
                }
                else { Qhb_wd_day[mth] = Qhb_wd_day[mth]; }

                if (double.IsNaN(Qcb_we_day[mth]) || Qcb_we_day[mth] < 0)
                {
                    Qcb_we_day[mth] = 0;
                }
                else { Qcb_we_day[mth] = Qcb_we_day[mth]; }

                if (double.IsNaN(Qcb_wd_day[mth]) || Qcb_wd_day[mth] < 0)
                {
                    Qcb_wd_day[mth] = 0;
                }
                else { Qcb_wd_day[mth] = Qcb_wd_day[mth]; }

                //  Qhb_we_mth[mth] = (Qhb_we_day[mth] * dwe_mth[mth] - dQc_b[0, 0, mth]) / 1000; //kWh 단위
                Qhb_we_mth[mth] = 0;
                Qhb_wd_mth[mth] = (Qhb_wd_day[mth] * dwd_mth[mth] + dQc_sink[0, 1, mth]) / 1000;
                // Qcb_we_mth[mth] = (Qcb_we_day[mth] * dwe_mth[mth]) / 1000;
                Qcb_we_mth[mth] = 0;
                Qcb_wd_mth[mth] = (Qcb_wd_day[mth] * dwd_mth[mth]) / 1000;

                Qhb_mth[mth] = Qhb_wd_mth[mth] + Qhb_we_mth[mth];
                Qcb_mth[mth] = Qcb_wd_mth[mth] + Qcb_we_mth[mth];


                Qhb_we_a += Qhb_we_mth[mth];
                Qhb_wd_a += Qhb_wd_mth[mth];
                Qcb_we_a += Qcb_we_mth[mth];
                Qcb_wd_a += Qcb_wd_mth[mth];
                Qhb_a += (Qhb_we_mth[mth] + Qhb_wd_mth[mth]);
                Qcb_a += (Qcb_we_mth[mth] + Qcb_wd_mth[mth]);

                Qb_day[0, 0, mth] = Qhb_we_day[mth];
                Qb_day[0, 1, mth] = Qhb_wd_day[mth];
                Qb_day[1, 0, mth] = Qcb_we_day[mth];
                Qb_day[1, 1, mth] = Qcb_wd_day[mth];

                Qb_mth[0, 0, mth] = Qhb_we_mth[mth];
                Qb_mth[0, 1, mth] = Qhb_wd_mth[mth];
                Qb_mth[1, 0, mth] = Qcb_we_mth[mth];
                Qb_mth[1, 1, mth] = Qcb_wd_mth[mth];

                Qb_a[0] = Qhb_a;
                Qb_a[1] = Qcb_a;

                if(zoneHC =="비냉난방" )
                {
                    Qb_mth[0, 0, mth] = 0;
                    Qb_mth[0, 1, mth] = 0;
                    Qb_mth[1, 0, mth] = 0;
                    Qb_mth[1, 1, mth] = 0;
                    Qb_a[0] = 0;
                    Qb_a[1] = 0;
                }
                else if ( zoneHC == "냉방")
                {
                    Qb_mth[0, 0, mth] = 0;
                    Qb_mth[0, 1, mth] = 0;
                    Qb_a[0] = 0;
                }
                else if (zoneHC == "난방")
                {
                    Qb_mth[1, 0, mth] = 0;
                    Qb_mth[1, 1, mth] = 0;
                    Qb_a[1] = 0;
                }
                else { }
            }
        }

        public void ZoneQmax()
        {
            Q_max[0] = QTsink_tot_max + 0.5 * QVsink_tot_max + QV_mech_sink_max;

            double Qsource_max, Qsink_max;

            double t_person = 0;
            string[][] value = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "사람일일이용시간", "용도명='" + zoneUsage + "'");
            if (value.Length > 0)
            {
                t_person = Convert.ToDouble(value[0][0]);
            }

            Qsource_max = QTsource_tot_Cmax + QVsource_tot_Cmax + QSopsource_tot_Cmax + QStr_tot_Cmax + (qI_p * zoneArea * t_person / twd_d + qI_fac * zoneArea) / t_c_op_d+ Peope_Num*H_summer*twd_d*2260/3600/twd_d;
            Qsink_max = QTsink_tot_Cmax + QVsink_tot_Cmax + QSopsink_tot_Cmax;

            Q_max[1] = 0.8 * (Qsource_max - Qsink_max) * (1 + 0.3 * Math.Exp(-tao_max / 120)) - Cwirk_A * zoneArea / 60 * (dtheta_i_NA - 2) + Cwirk_A * zoneArea / 40 * (12 / t_c_op_d - 1) +Q_DHU_max;

            double[,] beta_h = new double[2,12]; double[,] beta_c = new double[2, 12]; double[,] t_mth = new double[2, 12]; double[,] th_mth = new double[2, 12]; double[,] tc_mth = new double[2, 12]; //wewd,mth
           
            for (int wewd = 0; wewd < 2; wewd++)
            {
                for(int mth =0; mth<12;mth++)
                {
                    beta_h[wewd, mth] = Qb_day[0, wewd, mth] / (Q_max[0] * 24);
                    beta_c[wewd, mth] = Qb_day[1, wewd, mth] / (Q_max[1] * t_c_op_d);

                    if (beta_h[wewd,mth]>1)
                    {
                        beta_h[wewd, mth] = 1;
                    }

                    t_mth[0, mth] = dwe_mth[mth] * 24;
                    t_mth[1, mth] = dwd_mth[mth] * 24;

                    if (beta_h[wewd, mth] > 0.05)
                    {
                        th_mth[wewd, mth] = t_mth[wewd, mth];
                    }
                    else
                    {
                        th_mth[wewd, mth] = t_mth[wewd, mth] *beta_h[wewd,mth]/0.05;
                    }
                    if (beta_c[wewd,mth] > 0.15)
                    {
                        tc_mth[wewd, mth] = dwd_mth[mth] * t_c_op_d;
                    }
                    else
                    {
                        tc_mth[wewd, mth] = dwd_mth[mth] * t_c_op_d * beta_c[wewd, mth] / 0.15;
                    }
                }
            }
            for (int mth = 0; mth < 12; mth++)
            {
                t_max[0,mth] = th_mth[0, mth] + th_mth[1, mth];
                t_max[1, mth] = tc_mth[0, mth] + tc_mth[1, mth];
            }

            if (zoneHC == "비냉난방")
            {
                Q_max[0] = 0;
                Q_max[1] = 0;
                for (int mth = 0; mth < 12; mth++)
                {
                    t_max[0, mth] = 0;
                    t_max[1, mth] = 0;
                }
            }
            else if (zoneHC == "냉방")
            {
                Q_max[0] = 0;
                for (int mth = 0; mth < 12; mth++)
                {
                    t_max[0, mth] = 0;
                }
            }
            else if (zoneHC == "난방")
            {
                Q_max[1] = 0;
                for (int mth = 0; mth < 12; mth++)
                {
                    t_max[1, mth] = 0;
                }
            }
            else { }

        }
        
    }

    public class InWall
    {
        String InWall_Num;
        String InWall_SideZone;
        double Inwall_Area;
        double Inwall_U;

        public InWall(String InwallNum, String SideZone, double Area, double U)
        {
            this.InWall_Num = InwallNum;
            this.InWall_SideZone = SideZone;
            this.Inwall_Area = Area;
            this.Inwall_U = U;
        }
        public String Num()
        {
            return InWall_Num;
        }
        public String SideZone()
        {
            return InWall_SideZone;
        }
        public double Area()
        {
            return Inwall_Area;
        }
        public double U()
        {
            return Inwall_U;
        }
    }

    public class Slab
    {
        String Slab_Num;
        String Slab_SideZone;
        double Slab_Area;
        double Slab_U;

        public Slab(String InwallNum, String SideZone, double Area, double U)
        {
            this.Slab_Num = InwallNum;
            this.Slab_SideZone = SideZone;
            this.Slab_Area = Area;
            this.Slab_U = U;
        }
        public String Num()
        {
            return Slab_Num;
        }
        public String SideZone()
        {
            return Slab_SideZone;
        }
        public double Area()
        {
            return Slab_Area;
        }
        public double U()
        {
            return Slab_U;
        }
    }
    public class Wall
    {
        String wall_Num;
        String wall_ConstructionNum;
        double wall_Area;
        double wall_Ueff;
        double wall_α;
        string wall_DiIndi;
        String wall_Direction;
        String wall_Degree;

        public Wall(String EnvelopeNum, String ConstructionNum, double Area, double Ueff, double α, string DiIndi,String Direction, String Degree)
        {
            this.wall_Num = EnvelopeNum;
            this.wall_ConstructionNum = ConstructionNum;
            this.wall_Area = Area;
            this.wall_Ueff = Ueff;
            this.wall_α = α;
            this.wall_DiIndi = DiIndi;
            this.wall_Direction = Direction;
            this.wall_Degree = Degree;
        }

        public String Num()
        {
            return wall_Num;
        }

        public String CNum()
        {
            return wall_ConstructionNum;
        }

        public double Area()
        {
            return wall_Area;
        }
        public double Ueff()
        {
            return wall_Ueff;
        }

        public double α()
        {
            return wall_α;
        }

        public String DiIndi()
        {
            return wall_DiIndi;
        }

        public String Direction()
        {
            return wall_Direction;
        }
        public String Degree()
        {
            return wall_Degree;
        }

    }

    public class Roof
    {
        String Roof_Num;
        String Roof_ConstructionNum;
        double Roof_Area;
        double Roof_Ueff;
        double Roof_α;
        String Roof_DiIndi;
        String Roof_Direction;
        String Roof_Degree;

        public Roof(String EnvelopeNum, String ConstructionNum, double Area, double Ueff, double α, String DiIndi, String Direction, String Degree)
        {
            this.Roof_Num = EnvelopeNum;
            this.Roof_ConstructionNum = ConstructionNum;
            this.Roof_Area = Area;
            this.Roof_Ueff = Ueff;
            this.Roof_α = α;
            this.Roof_DiIndi = DiIndi;
            this.Roof_Direction = Direction;
            this.Roof_Degree = Degree;
        }
        public String Num()
        {
            return Roof_Num;
        }

        public String CNum()
        {
            return Roof_ConstructionNum;
        }
        public double Area()
        {
            return Roof_Area;
        }
        public double Ueff()
        {
            return Roof_Ueff;
        }

        public double α()
        {
            return Roof_α;
        }

        public String DiIndi()
        {
            return Roof_DiIndi;
        }
        public String Direction()
        {
            return Roof_Direction;
        }
        public String Degree()
        {
            return Roof_Degree;
        }
    }

    public class Window
    {
        String Window_Num;
        String Window_ConstructionNum;
        String Window_SubConstructionNum;
        double Window_Area;
        double Window_Uvalue;
        double Window_Uinst;
        String Window_DiIndi;
        double Window_Ff;
        double Window_g;
        double Window_tao;
        double Window_gtot;
        double Window_taotot;
        String Window_Direction;
        String Window_Degree;

        public Window(String EnvelopeNum, String ConstructionNum, String SubConstructionNum, double Area, double Uvalue, double Uinst, String DiIndi, double Ff, double g, double tao, double gtot, double taotot, String Direction, String Degree)
        {
            this.Window_Num = EnvelopeNum;
            this.Window_ConstructionNum = ConstructionNum;
            this.Window_SubConstructionNum = SubConstructionNum;
            this.Window_Area = Area;
            this.Window_Uvalue = Uvalue;
            this.Window_Uinst = Uinst;
            this.Window_DiIndi = DiIndi;
            this.Window_Ff = Ff;
            this.Window_g = g;
            this.Window_tao = tao;
            this.Window_gtot = gtot;
            this.Window_taotot = taotot;
            this.Window_Direction = Direction;
            this.Window_Degree = Degree;
            //this.Window_gtot = gtot;
            // this.Window_taotot = taotot;
        }

        public String Num()
        {
            return Window_Num;
        }

        public String CNum()
        {
            return Window_ConstructionNum;
        }
        public String SubCNum()
        {
            return Window_SubConstructionNum;
        }

        public double Area()
        {
            return Window_Area;
        }
        public double Uvalue()
        {
            return Window_Uvalue;
        }
        public double Uinst()
        {
            return Window_Uinst;
        }

        public String DiIndi()
        {
            return Window_DiIndi;
        }
        public double Ff()
        {
            return Window_Ff;
        }
        public double g()
        {
            return Window_g;
        }
        public double tao()
        {
            return Window_tao;
        }
        public double gtot()
        {
            return Window_gtot;
        }
        public double taotot()
        {
            return Window_taotot;
        }

        public String Direction()
        {
            return Window_Direction;
        }
        public String Degree()
        {
            return Window_Degree;
        }
    }

    public class CW
    {
        String CW_Num;
        String CW_ConstructionNum;
        double CW_Area_g;
        double CW_Uvalue_g;
        double CW_Ff_g;
        double CW_g_g;
        double CW_gtot_g;
        double CW_tao_g;
        double CW_taotot_g;
        double CW_Area_p;
        double CW_Uvalue_p;
        double CW_α_p;
        double CW_Area_d;
        double CW_Uvalue_d;
        double CW_Ff_d;
        double CW_g_d;
        double CW_tao_d;
        double CW_Area_tot;
        double CW_Uinst;
        String CW_Direction;
        String CW_Degree;
        String CW_CWType;

        public CW(String EnvelopeNum, String ConstructionNum, double Area_g, double Uvalue_g, double Ff_g, double g_g, double gtot_g, double tao_g, double taotot_g,double Area_p, double Uvalue_p, double α_p, double Area_d, double Uvalue_d, double Ff_d, double g_d, double tao_d, double Area_tot, double Uinst, String Direction, String Degree, string cWType)
        {
            this.CW_Num = EnvelopeNum;
            this.CW_ConstructionNum = ConstructionNum;
            this.CW_Area_g = Area_g;
            this.CW_Uvalue_g = Uvalue_g;
            this.CW_Ff_g = Ff_g;
            this.CW_g_g = g_g;
            this.CW_gtot_g = gtot_g;
            this.CW_tao_g = tao_g;
            this.CW_taotot_g = taotot_g;
            this.CW_Area_p = Area_p;
            this.CW_Uvalue_p = Uvalue_p;
            this.CW_α_p = α_p;
            this.CW_Area_d = Area_d;
            this.CW_Uvalue_d = Uvalue_d;
            this.CW_Ff_d = Ff_d;
            this.CW_g_d = g_d;
            this.CW_tao_d = tao_d;
            this.CW_Area_tot = Area_tot;
            this.CW_Uinst = Uinst;
            this.CW_Direction = Direction;
            this.CW_Degree = Degree;
            this.CW_CWType = cWType;
        }

        public String Num()
        {
            return CW_Num;
        }

        public String CNum()
        {
            return CW_ConstructionNum;
        }
        public double Area_g()
        {
            return CW_Area_g;
        }
        public double Uvalue_g()
        {
            return CW_Uvalue_g;
        }
        public double Ff_g()
        {
            return CW_Ff_g;
        }
        public double g_g()
        {
            return CW_g_g;
        }
        public double gtot_g()
        {
            return CW_gtot_g;
        }
        public double tao_g()
        {
            return CW_tao_g;
        }
        public double taotot_g()
        {
            return CW_taotot_g;
        }
        public double Area_p()
        {
            return CW_Area_p;
        }
        public double Uvalue_p()
        {
            return CW_Uvalue_p;
        }
        public double α_p()
        {
            return CW_α_p;
        }
        public double Area_d()
        {
            return CW_Area_d;
        }
        public double Uvalue_d()
        {
            return CW_Uvalue_d;
        }
        public double Ff_d()
        {
            return CW_Ff_d;
        }
        public double g_d()
        {
            return CW_g_d;
        }
        public double tao_d()
        {
            return CW_tao_d;
        }
        public double Area_tot()
        {
            return CW_Area_tot;
        }
        public double Uinst()
        {
            return CW_Uinst;
        }
        public String Direction()
        {
            return CW_Direction;
        }
        public String Degree()
        {
            return CW_Degree;
        }
        public String CWType()
        {
            return CW_CWType;
        }
    }

    public class Door
    {
        String Door_Num;
        String Door_ConstructionNum;
        double Door_Area;
        double Door_Ueff;
        double Door_α;
        String Door_DiIndi;
        String Door_Direction;
        String Door_Degree;

        public Door(String EnvelopeNum, String ConstructionNum, double Area, double Ueff, double α, String DiIndi, String Direction, String Degree)
        {
            this.Door_Num = EnvelopeNum;
            this.Door_ConstructionNum = ConstructionNum;
            this.Door_Area = Area;
            this.Door_Ueff = Ueff;
            this.Door_α = α;
            this.Door_DiIndi = DiIndi;
            this.Door_Direction = Direction;
            this.Door_Degree = Degree;
        }
        public String Num()
        {
            return Door_Num;
        }

        public String CNum()
        {
            return Door_ConstructionNum;
        }

        public double Area()
        {
            return Door_Area;
        }
        public double Ueff()
        {
            return Door_Ueff;
        }

        public double α()
        {
            return Door_α;
        }

        public String DiIndi()
        {
            return Door_DiIndi;
        }
        public String Direction()
        {
            return Door_Direction;
        }
        public String Degree()
        {
            return Door_Degree;
        }

    }

    public class Floor
    {
        String Floor_Num;
        String Floor_ConstructionNum;
        double Floor_Area;
        double Floor_Ueff;
        String Floor_GroundType;
        double Floor_Fx;

        public Floor(String EnvelopeNum, String ConstructionNum, double Area, double Ueff, String GroundType, double Fx)
        {
            this.Floor_Num = EnvelopeNum;
            this.Floor_ConstructionNum = ConstructionNum;
            this.Floor_Area = Area;
            this.Floor_Ueff = Ueff;
            this.Floor_GroundType = GroundType;
            this.Floor_Fx = Fx;
        }

        public String Num()
        {
            return Floor_Num;
        }

        public String CNum()
        {
            return Floor_ConstructionNum;
        }
        public double Area()
        {
            return Floor_Area;
        }
        public double Ueff()
        {
            return Floor_Ueff;
        }
        public String GroundType()
        {
            return Floor_GroundType;
        }
        public double Fx()
        {
            return Floor_Fx;
        }
    }

    public class GWall
    {
        String GWall_Num;
        String GWall_ConstructionNum;
        double GWall_Area;
        double GWall_Ueff;
        double GWall_Fx;

        public GWall(String EnvelopeNum, String ConstructionNum, double Area, double Ueff, double Fx)
        {
            this.GWall_Num = EnvelopeNum;
            this.GWall_ConstructionNum = ConstructionNum;
            this.GWall_Area = Area;
            this.GWall_Ueff = Ueff;
            this.GWall_Fx = Fx;
        }
        public String Num()
        {
            return GWall_Num;
        }

        public String CNum()
        {
            return GWall_ConstructionNum;
        }

        public double Area()
        {
            return GWall_Area;
        }
        public double Ueff()
        {
            return GWall_Ueff;
        }
        public double Fx()
        {
            return GWall_Fx;
        }
    }

    public class HTCalc
    {
        public double Calc(double uvalue, double area)
        {
            double HT;
            HT = uvalue * area;
            return HT;
        }

    }

    public class HVCalc
    {
        public double cpaρa = 0.34;
        public double HV_Calc(double n, double V)
        {
            double HV = n * V * cpaρa;
            return HV;
        }
        public double nmech_Calc(double Vmech_SUP, double tV_mech, double V)
        {
            double nmech_SUP = Vmech_SUP / V;
            double nmech = nmech_SUP * tV_mech / 24;
            return nmech;
        }
        public double nz_Calc(double Vmech_SUP, double Vmech_ETA, double tV_mech, double V)
        {
            double nmech_SUP = Vmech_SUP / V;
            double nmech_ETA = Vmech_ETA / V;
            double nz_SUP = nmech_ETA - nmech_SUP;
            double nz_d = nz_SUP * tV_mech / 24;
            return nz_d ;
        }
        public double ninf_Calc(double Vmech_SUP, double Vmech_ETA, double Vmech_SUP_z, double Vmech_ETA_z, double tV_mech, double n50, double V, double e, double f)
        {
            double nmech_SUP = Vmech_SUP / V;
            double nmech_ETA = Vmech_ETA / V;
            double nz_SUP = nmech_ETA - nmech_SUP;
            double nz_ETA = (Vmech_ETA_z - Vmech_SUP_z) / V;
            double nSUP = nmech_SUP + nz_SUP;
            double nETA = nmech_ETA + nz_ETA;
            double ninf, fe = 1;
            if (nSUP == 0)
            {
                ninf = n50 * e;
            }
            else
            {
                if (n50 != 0)
                { fe = 1 / (1 + f / e * Math.Pow(((nETA - nSUP) / n50), 2)); }
                ninf = n50 * e * (1 + (fe - 1) * tV_mech / 24);
            }
            return ninf;
        }
        public double nwin_Calc(double Vmech_SUP, double Vmech_ETA, double Vmech_SUP_z, double Vmech_ETA_z, double tV_mech, double twd, double n50, double nwd, double V, double e, double f)
        {
            double nmech_SUP = Vmech_SUP / V;
            double nmech_ETA = Vmech_ETA / V;
            double nz_SUP = nmech_ETA - nmech_SUP;
            double nz_ETA = (Vmech_ETA_z - Vmech_SUP_z) / V;
            double nSUP = nmech_SUP + nz_SUP;
            double nETA = nmech_ETA + nz_ETA;
            double ninf, fe;
            double Δnwin_mech_0, Δnwin_mech, Δnwin, nwin;

            //ninf계산
            if (nSUP == 0)
            {
                ninf = n50 * e;
                fe = 1;
            }
            else
            {
                fe = 1 / (1 + f / e * Math.Pow(((nETA - nSUP) / n50), 2));
                ninf = n50 * e * (1 + (fe - 1) * tV_mech / 24);
            }

            //Δnwin_mech_0계산 
            if (nwd < 1.2)
            {
                Δnwin_mech_0 = Math.Max(0, nwd - (nwd - 0.2) * ninf * fe - 0.1);
            }
            else
            {
                Δnwin_mech_0 = Math.Max(0, nwd - ninf * fe - 0.1);
            }

            //Δnwin_mech 계산 
            if ((Δnwin_mech_0 <= nSUP) && (nETA <= (nSUP + ninf)))
            {
                Δnwin_mech = 0;
            }
            else if ((Δnwin_mech_0 <= nSUP) && (nETA > (nSUP + ninf)))
            {
                Δnwin_mech = nETA - nSUP - ninf;
            }
            else if ((Δnwin_mech_0 > nSUP) && (nETA <= (nSUP + ninf)))
            {
                Δnwin_mech = Δnwin_mech_0 - nSUP;
            }
            else
            {
                Δnwin_mech = nETA - nSUP - ninf;
            }


            //Δnwin 계산 
            if (nwd < 1.2)
            {
                Δnwin = Math.Max(0, nwd - (nwd - 0.2) * ninf - 0.1);
            }
            else
            {
                Δnwin = Math.Max(0, nwd - ninf - 0.1);
            }


            //nwin 계산 
            if (nSUP == 0)
            {
                nwin = 0.1 + Δnwin * twd / 24;
            }
            else
            {
                nwin = Math.Max(0, 0.1 + Δnwin * Math.Max((twd - tV_mech), 0) / 24 + Δnwin_mech * tV_mech / 24);
            }

            return nwin;
        }

    }

    public class theta_iCalc
    {
        public double tao_Calc(double cwirk, double H)
        {
            double tao;
            tao = cwirk / H;
            return tao;
        }


        public double theta_ihwe_Calc(double tao, String Mode_we, double theta_e, double theta_i_h_set, double dthetai_NA)
        {
            double theta_i_h_we;
            double f_we;

            if (Mode_we == "감소운전")
            {
                f_we = 0.2 * (1 - 0.4 * tao / 250);
            }
            else if (Mode_we == "운전정지")
            {
                f_we = 0.3 * (1 - 0.2 * tao / 250);
            }
            else
            {
                f_we = 0;

            }
            theta_i_h_we = Math.Max(theta_i_h_set - f_we * (theta_i_h_set - theta_e), theta_i_h_set - dthetai_NA);

            return theta_i_h_we;
        }


        public double theta_ihwd_Calc(double tao, String Mode_wd, double tNA, double theta_e, double theta_i_h_set, double dthetai_NA)
        {
            double theta_i_h_wd;
            double f_wd;

            if (Mode_wd == "감소운전")
            {
                f_wd = 0.13 * tNA / 24 * Math.Exp((-tao / 250));
            }
            else if (Mode_wd == "운전정지")
            {
                f_wd = 0.26 * tNA / 24 * Math.Exp((-tao / 250));
            }
            else
            {
                f_wd = 0;

            }
            theta_i_h_wd = Math.Max(theta_i_h_set - f_wd * (theta_i_h_set - theta_e), theta_i_h_set - dthetai_NA * tNA / 24);

            return theta_i_h_wd;
        }

        public double theta_ic_Calc(double theta_i_c_set)
        {
            double theta_i_c = theta_i_c_set;

            return theta_i_c;
        }


    }

    public class QTCalc
    {
        public double Calc_sink(double Te, double Ti, double HT)
        {
            double QT_sink;
            QT_sink = (Ti - Te) * HT * 24;
            return QT_sink;
        }

        public double Calc_source(double Te, double Ti, double HT)
        {
            double QT_source;
            QT_source = (Te - Ti) * HT * 24;
            return QT_source;
        }
        public double Calc_sink_max(double Te, double Ti, double HT)
        {
            double QT_sink;
            QT_sink = (Ti - Te) * HT ;
            return QT_sink;
        }

        public double Calc_source_max(double Te, double Ti, double HT)
        {
            double QT_source;
            QT_source = (Te - Ti) * HT ;
            return QT_source;
        }
    }

    public class QSopCalc
    {
        double Rse = 0.04;
        double Uvalue;
        double Area;
        double hr = 4.5;
        double dtheta_er = 10;
        double α;
        double IS;

        public double Calc(double Uvalue, double Area, double α, double IS, double Ff)
        {
            double QSop_sink, QSop_source;
            if (Ff * hr * dtheta_er >= α * IS)
            {
                QSop_sink = Rse * Uvalue * Area * (Ff * hr * dtheta_er - α * IS) * 24;
                QSop_source = 0;
                return QSop_sink;
            }
            else
            {
                QSop_sink = 0;
                QSop_source = Rse * Uvalue * Area * (α * IS - Ff * hr * dtheta_er) * 24;
                return QSop_source;
            }
        }
        public double Calc_max(double Uvalue, double Area, double α, double IS, double Ff)
        {
            double QSop_sink, QSop_source;
            if (Ff * hr * dtheta_er >= α * IS)
            {
                QSop_sink = Rse * Uvalue * Area * (Ff * hr * dtheta_er - α * IS) ;
                QSop_source = 0;
                return QSop_sink;
            }
            else
            {
                QSop_sink = 0;
                QSop_source = Rse * Uvalue * Area * (α * IS - Ff * hr * dtheta_er) ;
                return QSop_source;
            }
        }
    }

    public class GeffCalc
    {
        double g;
        double Fs;
        double Fw = 0.9;
        double Fv = 0.9;
        double gtot;
        double a;
        public double Calc(double g, double Fs)
        {
            double geff;
            geff = Fs * Fw * Fv * g;
            return geff;
        }
        public double Calc(double g, double Fs, double gtot, double a)
        {
            double geff;
            if (a * gtot + (1 - a) * g > Fs * g)
            {
                geff = Fs * Fw * Fv * g;
            }
            else
            {
                geff = Fw * Fv * (a * gtot + (1 - a) * g);
            }
            return geff;
        }
    }

    public class QStrCalc
    {
        double Ff;
        double Area;
        double geff;
        double Is;

        public double Calc(double Ff, double Area, double geff, double Is)
        {
            double QS;
            QS = Ff * Area * geff * Is * 24;
            return QS;
        }
        public double Calc_max(double Ff, double Area, double geff, double Is)
        {
            double QS;
            QS = Ff * Area * geff * Is ;
            return QS;
        }


    }

    public class QVCalc
    {
        public double Calc_sink(double Te, double Ti, double HV)
        {
            double QV_sink;
            QV_sink = (Ti - Te) * HV * 24;
            return QV_sink;
        }

        public double Calc_source(double Te, double Ti, double HV)
        {
            double QV_source;
            QV_source = (Te - Ti) * HV * 24;
            return QV_source;
        }

        public double Calc_sink_max(double Te, double Ti, double HV)
        {
            double QV_sink;
            QV_sink = (Ti - Te) * HV ;
            return QV_sink;
        }

        public double Calc_source_max(double Te, double Ti, double HV)
        {
            double QV_source;
            QV_source = (Te - Ti) * HV ;
            return QV_source;
        }
    }

    public class eta_Calc
    {
        public double eta_h_Calc(double gamma, double a)
        {
            double eta_1;
            double eta_2;
            double eta;

            if (gamma == 1)
            {
                eta_1 = a / (a + 1);
            }
            else
            {
                eta_1 = (1 - Math.Pow(gamma, a)) / (1 - Math.Pow(gamma, a + 1));
            }

            if ((1 - eta_1 * gamma) < 0.01)
            {
                eta_2 = 1 / gamma;
            }
            else
            {
                eta_2 = eta_1;
            }
            eta = Math.Max(eta_1, eta_2);

            if (double.IsNaN(eta) || eta < 0)
            {
                eta = 0;
            }
            else { eta = eta; }

            return eta;
        }

        public double eta_c_Calc(double gamma, double a)
        {
            double eta_1;
            double eta_2;
            double eta;

            if (gamma == 1)
            {
                eta_1 = a / (a + 1);
            }
            else
            {
                eta_1 = (1 - Math.Pow(gamma, a)) / (1 - Math.Pow(gamma, a + 1));
            }
            if ((1 - eta_1) * gamma < 0.2)
            {
                eta_2 = 1;
            }
            else
            {
                eta_2 = eta_1;
            }
            eta = Math.Max(eta_1, eta_2);

            if (double.IsNaN(eta) || eta < 0)
            {
                eta = 0;
            }
            else { eta = eta; }

            return eta;
        }

    }

    public class dQc_bCalc
    {
        public double Calc(double Cwirk, double theta_i_h_set, double theta_i_h, double awe, double Δtheta_i_NA, double Qsink, double η, double Qsource)
        {
            double dQc_b;
            if (awe != 0)
            { dQc_b = Math.Min(Math.Min((Cwirk * 2 * (theta_i_h_set - theta_i_h) / awe), (Cwirk * Δtheta_i_NA / awe)), (Qsink - η * Qsource)); }
            else {  dQc_b = 0; }

            if(double.IsNaN(dQc_b) || dQc_b < 0)
            {
                dQc_b = 0;
            }
            else { dQc_b = dQc_b; }

                return dQc_b;
        }

    }

    public class QbCalc
    {
        public double Qhb_Calc(double Qsink, double η, double Qsource)
        {
            double Qhb;
            Qhb = Qsink - η * Qsource;

            if (double.IsNaN(Qhb) || Qhb < 0)
            {
                Qhb = 0;
            }
            else { Qhb = Qhb; }
            return Qhb;
        }

        public double Qcb_Calc(double η, double Qsource)
        {
            double Qcb;
            Qcb = (1 - η) * Qsource;
            if (double.IsNaN(Qcb)  || Qcb < 0)
            {
                Qcb = 0;
            }
            else { Qcb = Qcb; }
            return Qcb;
        }
    }

}
