using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace main
{

    internal class Zone
    {
        public String ZoneNum;
        public String zoneName;
        public String zoneUsage, zoneHC, Mode_night, Mode_we; double n_Weekday;//주이용일수
        public double Peope_Num, t_c_op_d, dtheta_i_NA, Fx, Fx_Floor, Fx_GWall, theta_s_c, theta_i_h_min, theta_i_c_max, theta_SUP_Wi;
        public double[] theta_i_set = new double[2];
        public double twd_d, th_op_d_we, th_op_d, dwd_a;
        public double zoneArea, zoneHeight;
        public double qI_p, qI_fac, Cwirk_A;
        public double VA_we, VA_wd, n50, e, f, Vmech_SUP, Vmech_ETA, xi_c_set, xi_h_set, H_winter, H_summer, V_SUP_z, V_ETA_z, ρacp_a;
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
        public double[] Zone_HT_tot = new double[2]; public double Zone_HT_Di_tot;
        public double Zone_HT_Wall, Zone_HT_Roof, Zone_HT_Floor, Zone_HT_GWall, Zone_HT_Door, Zone_HT_Win, Zone_HT_CW;
        public double[] Zone_HT_Inwall = new double[2], Zone_HT_Slab = new double[2];
        public double Zone_HT_Di_Wall, Zone_HT_Indi_Wall, Zone_HT_Di_Roof, Zone_HT_Indi_Roof, Zone_HT_Di_Win, Zone_HT_Indi_Win, Zone_HT_Di_Door, Zone_HT_Indi_Door;
        public double Zone_HT_TB_tot, Zone_HT_TB_Wall, Zone_HT_TB_Roof, Zone_HT_TB_Floor, Zone_HT_TB_GWall, Zone_HT_TB_Win, Zone_HT_TB_Door, Zone_HT_TB_CW;
        public double nmech, nz, ninf, nwin;
        public double Zone_HV_inf, Zone_HV_win, HV_tot_max;
        public double[] Zone_HV_z = new double[2]; // 인접존 b가 hc별로 달라질 수 있어 배열
        public double[,] Zone_HV_mech = new double[2, 12], Zone_HV_tot = new double[2, 12]; // b_mech가 월별로 달라져서 배열
        public double[,] theta_v_mech = new double[2, 12]; // ZoneQV()/ZoneQV2()가 공유해서 씀(ZoneHV()에서 한 번만 계산)
        public double[,] Zone_H_tot = new double[2, 12];
        public double[,] tao = new double[2, 12]; double tao_max;
        public double[] theta_e = new double[12]; // 냉난방 무관하게 항상 같은 실외온도라 hc 구분 없이 단일 배열
        public double[] dmth = new double[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public double[,] theta_i = new double[2, 12];
        public double[] dwd_mth = new double[12];
        //[난방/냉방,비이용일/이용일,mth] = [h/c,we/wd,mth]=[0/1,0/1,12]
        //QT
        public double[,] QTsink_tot = new double[2, 12], QTsink_Wall = new double[2, 12], QTsink_Roof = new double[2, 12], QTsink_Floor = new double[2, 12], QTsink_GWall = new double[2, 12], QTsink_Door = new double[2, 12], QTsink_Win = new double[2, 12], QTsink_CW = new double[2, 12], QTsink_Inwall = new double[2, 12], QTsink_Slab = new double[2, 12];
        //public double[,] QTsource_tot = new double[2, 12], QTsource_Wall = new double[2, 12], QTsource_Roof = new double[2, 12], QTsource_Floor = new double[2, 12], QTsource_GWall = new double[2, 12], QTsource_Door = new double[2, 12], QTsource_Win = new double[2, 12], QTsource_CW = new double[2, 12], QTsource_Inwall = new double[2, 12], QTsource_Slab = new double[2, 12];
        public double[,] QTsink_TB = new double[2, 12]; //QTsource_TB = new double[2, 12];
        public double QTsink_tot_max, QTsink_Wall_max, QTsink_Roof_max, QTsink_Floor_max, QTsink_GWall_max, QTsink_Door_max, QTsink_Win_max, QTsink_CW_max, QTsink_TB_max;
        public double QTsink_tot_Cmax, QTsink_Wall_Cmax, QTsink_Roof_Cmax, QTsink_Floor_Cmax, QTsink_GWall_Cmax, QTsink_Door_Cmax, QTsink_Win_Cmax, QTsink_CW_Cmax, QTsink_TB_Cmax;
        public double QTsource_tot_Cmax, QTsource_Wall_Cmax, QTsource_Roof_Cmax, QTsource_Floor_Cmax, QTsource_GWall_Cmax, QTsource_Door_Cmax, QTsource_Win_Cmax, QTsource_CW_Cmax, QTsource_TB_Cmax;
        //QS
        public double[,] QS_rad_tot = new double[2, 12], QSopsource_tot = new double[2, 12], QStr_tot = new double[2, 12], QStr_own = new double[2, 12];
        public double[] QSopsource_Wall = new double[12], QSopsource_Roof = new double[12], QSopsource_Door = new double[12], QSopsource_CW_p = new double[12];
        public double[] QS_rad_Wall = new double[12], QS_rad_Roof = new double[12], QS_rad_Door = new double[12], QS_rad_CW_p = new double[12]; public double[,] QS_rad_Win = new double[2, 12], QS_rad_CW = new double[2, 12];
        public double[,] QStr_Win = new double[2, 12], QStr_CW = new double[2, 12]; public double QStr_Win_max, QStr_CW_max;
        public double QSopsink_tot_Cmax, QSopsource_tot_Cmax, QStr_tot_Cmax;
        //QV
        public double[,] QVsink_tot = new double[2, 12], QV_inf_sink = new double[2, 12], QV_win_sink = new double[2, 12], QV_z_sink = new double[2, 12], QV_mech_sink = new double[2, 12];
        // public double[,] QVsource_tot = new double[2, 12], QV_inf_source = new double[2, 12], QV_win_source = new double[2, 12], QV_z_source = new double[2, 12], QV_mech_source = new double[2, 12];
        public double QVsink_tot_max, QV_inf_sink_max, QV_win_sink_max, QV_z_sink_max, QV_mech_sink_max;
        public double QVsink_tot_Cmax, QV_inf_sink_Cmax, QV_win_sink_Cmax, QVsource_tot_Cmax, QV_inf_source_Cmax, QV_win_source_Cmax;
        //QI
        public double[,] QI_tot = new double[2, 12], QI_L = new double[2, 12], QI_own = new double[2, 12]; public double[] QI_Humidity = new double[12];
        public double[] QI_P = new double[12], QI_fac = new double[12];
        //
        public double[,] Qsink = new double[2, 12], Qsource = new double[2, 12], gamma = new double[2, 12], a = new double[2, 12], eta = new double[2, 12];

       

        public double[,] Qb_mth = new double[2, 12]; public double[] Qb_a = new double[2];
        public double[] b_ztu = new double[2];
        string[][] Location; public string[][] 검토유형;
        public double theta_i_c_max_d, theta_e_min, theta_e_max, X_e_max; String[,] Is_max = new String[9, 2]; //수평,남,남동,남서,동,서,북서,북동,북 
        public double[] Q_max = new double[2]; public double[,] t_max = new double[2, 12];
        double[,] theta_u = new double[2, 12]; public double[] Utb = new double[3];//외벽, 지붕, 바닥 열교 
        public double Door_q50 = 0, Win_q50 = 0, CW_q50 = 0, Wall_q50 = 0, Roof_q50 = 0;

        //배기환기소요량
        public double[] Q_fan = new double[12];

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
                Peope_Num = Program.UTIL.ToDoubleOrZero(ZoneG[0][3]);
                t_c_op_d = Program.UTIL.ToDoubleOrZero(ZoneG[0][4]);
            }

            //존 용도프로필 정보 가져오기 
            String[][] ZoneU = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "난방설정온도,냉방설정온도,허용셋백온도,난방최저온도,최저상대습도,최고상대습도,겨울습기발생량,여름습기발생량,냉방최고온도", "용도명='" + zoneUsage + "'");
            if (ZoneU.Length > 0)
            {
                theta_i_set[0] = Program.UTIL.ToDoubleOrZero(ZoneU[0][0]);
                theta_i_set[1] = Program.UTIL.ToDoubleOrZero(ZoneU[0][1]);
                dtheta_i_NA = Program.UTIL.ToDoubleOrZero(ZoneU[0][2]);
                Fx = 0.8;
                Fx_Floor = 0.5; //임의값 넣음 나중에 계산해야함 
                Fx_GWall = 0.5;//임의값 넣음 나중에 계산해야함 
                theta_s_c = 18;
                theta_i_h_min = Program.UTIL.ToDoubleOrZero(ZoneU[0][3]);
                theta_i_c_max = Program.UTIL.ToDoubleOrZero(ZoneU[0][8]);
                theta_SUP_Wi = 18;
                Mode_night = "운전정지";
                Mode_we = "운전정지";
                xi_c_set = 611.2 * Math.Exp(17.62 * theta_i_set[1] / (243.12 + theta_i_set[1])) / 461.51 / (273.15 + theta_i_set[1]) / 1.2 * (Program.UTIL.ToDoubleOrZero(ZoneU[0][5]) / 100);
                xi_h_set = 611.2 * Math.Exp(17.62 * theta_i_set[0] / (243.12 + theta_i_set[0])) / 461.51 / (273.15 + theta_i_set[0]) / 1.2 * (Program.UTIL.ToDoubleOrZero(ZoneU[0][4]) / 100);
                H_winter = Program.UTIL.ToDoubleOrZero(ZoneU[0][6]);
                H_summer = Program.UTIL.ToDoubleOrZero(ZoneU[0][7]);
            }

            //존 일반정보 가져오기
            // ZoneG = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_form", "사용시간,냉난방시간,연이용일수,순바닥면적,천장고, 면적당인체발열, 면적당기기발열, 존축열성능, 비이용일환기량,이용일환기량,주이용일", "존번호='" + ZoneNum + "'");
            ZoneG = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_form", "사용시간,냉난방시간,연이용일수,순바닥면적,천장고, 일일인체발열, 일일기기발열, 존축열성능, 비이용일환기량,이용일환기량,주이용일", "존번호='" + ZoneNum + "'");
            if (ZoneG.Length > 0)
            {
                twd_d = Program.UTIL.ToDoubleOrZero(ZoneG[0][0]);
                th_op_d_we = 0;
                th_op_d = Program.UTIL.ToDoubleOrZero(ZoneG[0][1]);
                dwd_a = Program.UTIL.ToDoubleOrZero(ZoneG[0][2]);
                zoneArea = Program.UTIL.ToDoubleOrZero(ZoneG[0][3]);
                zoneHeight = Program.UTIL.ToDoubleOrZero(ZoneG[0][4]);
                qI_p = Program.UTIL.ToDoubleOrZero(ZoneG[0][5]);
                qI_fac = Program.UTIL.ToDoubleOrZero(ZoneG[0][6]);
                Cwirk_A = Program.UTIL.ToDoubleOrZero(ZoneG[0][7]);
                VA_we = Program.UTIL.ToDoubleOrZero(ZoneG[0][8]) / zoneArea; //단위면적당 값 
                VA_wd = Program.UTIL.ToDoubleOrZero(ZoneG[0][9]) / zoneArea;//단위면적당 값 

                e = 0.05;
                f = 15;

                n_Weekday = Program.UTIL.ToDoubleOrZero(ZoneG[0][10]);
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
                        dwd_mth[mth] = Program.UTIL.ToDoubleOrZero(ValueK[0][0]);
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
                    theta_e[m] = Program.UTIL.ToDoubleOrZero(OTemp[m][1]); //실외온도(냉난방 공통)
                }
            }
            //부하 관련 데이터 불러오기 
            string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_부하 ", "해석설계외기온도,최고온도,최고절대습도", "지역명='" + Location[0][0] + "'");
            if (Value.Length > 0)
            {
                theta_i_c_max_d = (theta_i_set[1] + theta_i_c_max - 2) / 2;
                theta_e_min = Program.UTIL.ToDoubleOrZero(Value[0][0]);
                theta_e_max = Program.UTIL.ToDoubleOrZero(Value[0][1]);
                X_e_max = Program.UTIL.ToDoubleOrZero(Value[0][2]);
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
        }
        public void LoadData_dUtb_2D()
        {
            string[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "외벽dUtb,지붕dUtb,바닥dUtb", "");
            if (Value2.Length > 0)
            {
                Utb[0] = Program.UTIL.ToDoubleOrZero(Value2[0][0]);
                Utb[1] = Program.UTIL.ToDoubleOrZero(Value2[0][1]);
                Utb[2] = Program.UTIL.ToDoubleOrZero(Value2[0][2]);
            }
        }
        public void LoadData_Shade(string ZoneNum)
        {
            string[][] Win = Program.DB.querySQL(DB.type.ProjDB, "Select 번호 From ZoneEnvelope_3D Where (외피유형 = '창호' or 외피유형 = '커튼월창') and 존='" + ZoneNum + "' Order by 번호");
            if (Win.Length > 0)
            {
                for (int k = 0; k < Win.Length; k++)
                {
                    string[][] shade = Program.DB.getValue(DB.type.ProjDB, "Shade_3D", "음영계수", "번호='" + Win[k][0] + "'");
                    if (shade.Length > 0 && shade[0][0] != "")
                    {

                    }
                    else
                    {
                        ZoneShade zoneshade = new ZoneShade(Win[k][0]);
                        zoneshade.Calc_방위각();
                        zoneshade.Calc_지형물음영();

                        zoneshade.Calc_상부음영();
                        zoneshade.Calc_좌측음영();
                        zoneshade.Calc_우측음영();
                        zoneshade.Calc_음영계수();
                        zoneshade.Save();
                    }

                }
            }
        }
        public void LoadData_q50()
        {
            string[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기밀측정여부,출입문q50,창호q50,외벽q50,지붕q50", "");
            if (Value2.Length > 0)
            {
                Door_q50 = Program.UTIL.ToDoubleOrZero(Value2[0][1]);
                Win_q50 = Program.UTIL.ToDoubleOrZero(Value2[0][2]);
                Wall_q50 = Program.UTIL.ToDoubleOrZero(Value2[0][3]);
                Roof_q50 = Program.UTIL.ToDoubleOrZero(Value2[0][4]);
            }
        }
        public void LoadData_Ventil()
        {//존 환기정보 가져오기 
            string[][] ZoneG = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_form", "환기유무,환기방식,비이용일환기량,이용일환기량,선택열회수기", "존번호='" + ZoneNum + "'");
            if (ZoneG.Length > 0)
            {
                if (Convert.ToBoolean(ZoneG[0][0]))
                {
                    SelectHRV = ZoneG[0][4];

                    // 이용일환기량(필요환기량) 기반 추정치가 기본값이고, 이 존의 실제 급배기량이
                    // AHUZoneVent_Form에 있으면(=설비 화면에서 입력 완료됨) 그걸 우선 사용 —
                    // "존 > 요구량 계산 > 설비 입력 > 요구량 재계산" 순서상, 설비 입력 전에도
                    // 요구량 계산이 돌아가야 하므로 그 시점엔 추정치로 폴백해야 함.
                    // 아래 각 분기의 Q_fan 등 풍량을 쓰는 계산에도 실측치가 일관되게 반영되도록
                    // 분기 진입 전에 먼저 확정해둠.
                    // getValue()가 빈 문자열을 돌려줄 수 있는데 Convert.ToDouble("")은 예외를 던지므로
                    // (Convert.ToDouble(null)만 안전하게 0을 반환함), double.TryParse로 안전하게 파싱.
                    // 급기량만 비우고 배기량만 입력하는 경우(배기환기 등)가 흔해서, 두 필드를 && 로
                    // 묶지 않고 따로 파싱 — 한쪽이 비어도 다른 쪽 실제 입력값을 버리지 않게 함.
                    // hasActualSAEA는 "행 자체가 있는지"(=설비 입력 단계를 거쳤는지)만 의미하고,
                    // 그 안에서 비어있는 개별 필드는 0(값 없음)으로 처리.
                    string[][] ActualSAEA = Program.DB.getValue(DB.type.ProjDB, "AHUZoneVent_Form", "급기량,배기량", "설비 = '" + SelectHRV + "' And 존 = '" + ZoneNum + "'");
                    bool hasActualSAEA = ActualSAEA.Length > 0;
                    double actualSA = 0, actualEA = 0;
                    if (hasActualSAEA)
                    {
                        double.TryParse(ActualSAEA[0][0], out actualSA);
                        double.TryParse(ActualSAEA[0][1], out actualEA);
                    }

                    if (ZoneG[0][1] == "열회수기")
                    {
                        Vmech_SUP = hasActualSAEA ? actualSA : Program.UTIL.ToDoubleOrZero(ZoneG[0][3]);
                        Vmech_ETA = hasActualSAEA ? actualEA : Program.UTIL.ToDoubleOrZero(ZoneG[0][3]);
                        string[][] value = Program.DB.getValue(DB.type.ProjDB, "User_HRV", "온도교환효율_난방,온도교환효율_냉방,습도교환효율_난방,습도교환효율_냉방", "번호='" + SelectHRV + "'");
                        if (value.Length > 0)
                        {
                            eta_V_mech[0] = Program.UTIL.ToDoubleOrZero(value[0][0]) / 100;
                            eta_V_mech[1] = Program.UTIL.ToDoubleOrZero(value[0][1]) / 100;
                            eta_χV_mech[0] = Program.UTIL.ToDoubleOrZero(value[0][2]) / 100;
                            eta_χV_mech[1] = Program.UTIL.ToDoubleOrZero(value[0][3]) / 100;
                        }

                    }
                    else if (ZoneG[0][1] == "공조기")
                    {
                        Vmech_SUP = hasActualSAEA ? actualSA : Program.UTIL.ToDoubleOrZero(ZoneG[0][3]);
                        Vmech_ETA = hasActualSAEA ? actualEA : Program.UTIL.ToDoubleOrZero(ZoneG[0][3]);
                        string[][] value = Program.DB.getValue(DB.type.ProjDB, "User_AHU", "온도교환효율_난방,온도교환효율_냉방,습도교환효율_난방,습도교환효율_냉방", "번호='" + SelectHRV + "'");
                        if (value.Length > 0)
                        {
                            eta_V_mech[0] = Program.UTIL.ToDoubleOrZero(value[0][0]) / 100;
                            eta_V_mech[1] = Program.UTIL.ToDoubleOrZero(value[0][1]) / 100;
                            eta_χV_mech[0] = Program.UTIL.ToDoubleOrZero(value[0][2]) / 100;
                            eta_χV_mech[1] = Program.UTIL.ToDoubleOrZero(value[0][3]) / 100;
                        }
                    }
                    else if (ZoneG[0][1] == "배기환기(3종)")
                    {
                        Vmech_SUP =  0;
                        Vmech_ETA = hasActualSAEA ? actualEA : Program.UTIL.ToDoubleOrZero(ZoneG[0][3]);

                        string[][] value = Program.DB.getValue(DB.type.ProjDB, "User_Fan", "풍량,모터제어,소비전력", "번호='" + SelectHRV + "'");
                        if (value.Length > 0)
                        {
                            double volum = Program.UTIL.ToDoubleOrZero(value[0][0].ToString());
                            double fan_elec = Program.UTIL.ToDoubleOrZero(value[0][2].ToString()) / volum; //W/cmh
                            double control_factor = 0;
                            string control = value[0][1];
                            string[][] value2 = Program.DB.getValue(DB.type.BaseDB_AHU, "팬모터제어계수", "계수", "제어유형='" + control + "'");
                            control_factor = Program.UTIL.ToDoubleOrZero(value2[0][0].ToString());

                            for (int mth = 0; mth < 12; mth++)
                            {
                                Q_fan[mth] = th_op_d * dwd_mth[mth] * Math.Min(Vmech_ETA, volum) * fan_elec * Math.Pow(0.65, control_factor) / 1000; //kWh/mth 배기팬의 경우 필요시 작동하는 방식으로 개별제어값을 적용함
                            }
                        }
                    }
                    else
                    {
                        // 환기방식 UI 콤보박스(ZoneGeneral.cs)엔 열회수기/공조기/배기환기(3종) 세 개뿐이라
                        // 이 분기는 정상 경로로는 도달 불가능(레거시 데이터 등 예외 상황) — 모르는 값이면
                        // 환기 없음으로 안전하게 처리
                        Vmech_SUP = 0;
                        Vmech_ETA = 0;
                    }
                }
                else
                {
                    Vmech_SUP = 0;
                    Vmech_ETA = 0;
                }

                // ISO 식(109)/(111) 추정식은 인접존이 여럿이면 부족분이 중복 계산돼 안 씀 — AHUZoneVent_Form
                // 실측값만 반영. 방향: 존=보내는 존, 인접존=받는 존 → 존=ZoneNum 합=유출, 인접존=ZoneNum 합=유입.
                string[][] OutgoingZV = Program.DB.getValue(DB.type.ProjDB, "AHUZoneVent_Form", "인접존배기량", "존 = '" + ZoneNum + "' And 인접존 <> ''");
                double outgoingZ = 0;
                for (int i = 0; i < OutgoingZV.Length; i++)
                {
                    double.TryParse(OutgoingZV[i][0], out double v);
                    outgoingZ += v;
                }
                string[][] IncomingZV = Program.DB.getValue(DB.type.ProjDB, "AHUZoneVent_Form", "인접존배기량", "인접존 = '" + ZoneNum + "'");
                double incomingZ = 0;
                for (int i = 0; i < IncomingZV.Length; i++)
                {
                    double.TryParse(IncomingZV[i][0], out double v);
                    incomingZ += v;
                }
                V_SUP_z = incomingZ;
                V_ETA_z = outgoingZ;
                ρacp_a = 0.34;
            }
        }
        public void LoadData_InWall()
        {
            //존 내벽 정보 가져오기
            zoneInWall.Clear(); // 두 번 호출돼도(웜업 패스+본계산) 중복 누적되지 않도록
            String[][] ZoneInW = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,인접존,면적", "존 = '" + ZoneNum + "' And  외피유형 = '내벽'");
            int i = -1;
            if (ZoneInW.Length > 0)
            {
                while (++i < ZoneInW.Length)
                {
                    Zone zone1 = Program.CALC.getZone(ZoneInW[i][1].ToString());
                    double R = (0.1 / 2.3) + 0.13 + 0.13;
                    double U = 1 / R;
                    InWall Inwall = new InWall(ZoneInW[i][0], ZoneInW[i][1], Program.UTIL.ToDoubleOrZero(ZoneInW[i][2]), U);
                    zoneInWall.Add(Inwall);

                }
            }
        }
        public void LoadData_SL()
        { //존 층간바닥 정보 가져오기
            zoneSlab.Clear(); // 두 번 호출돼도(웜업 패스+본계산) 중복 누적되지 않도록
            String[][] ZoneSL = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,인접존,면적", "존 = '" + ZoneNum + "' And  외피유형 = '층간바닥'");
            int i = -1;
            if (ZoneSL.Length > 0)
            {
                while (++i < ZoneSL.Length)
                {
                    Zone zone1 = Program.CALC.getZone(ZoneSL[i][1].ToString());
                    double R = (0.15 / 2.3) + (30 / 1000 / 0.035) + 0.13 + 0.13;
                    double U = 1 / R;
                    Slab slab = new Slab(ZoneSL[i][0], ZoneSL[i][1], Program.UTIL.ToDoubleOrZero(ZoneSL[i][2]), U);
                    zoneSlab.Add(slab);
                }
            }
        }
        public void LoadData_Wall()
        {//존 외벽 정보 가져오기
            zoneWall.Clear(); // 두 번 호출돼도(웜업 패스+본계산) 중복 누적되지 않도록
            String[][] ZoneW = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.유효열관류율,b.흡수율,b.직접간접,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + ZoneNum + "' And  NOT b.직접간접 = '지면'");

            int i = -1;
            if (ZoneW.Length > 0)
            {
                while (++i < ZoneW.Length)
                {
                    Wall wall = new Wall(ZoneW[i][0], ZoneW[i][2], Program.UTIL.ToDoubleOrZero(ZoneW[i][1]), Program.UTIL.ToDoubleOrZero(ZoneW[i][3]), Program.UTIL.ToDoubleOrZero(ZoneW[i][4]), ZoneW[i][5], ZoneW[i][6], ZoneW[i][7]);
                    zoneWall.Add(wall);
                }
            }
        }
        public void LoadData_Roof()
        { //존 지붕 정보 가져오기
            zoneRoof.Clear(); // 두 번 호출돼도(웜업 패스+본계산) 중복 누적되지 않도록
            String[][] ZoneR = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.유효열관류율,b.흡수율,b.직접간접,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionRoof AS b ON a.구조체번호 = b.번호 where a.존 = '" + ZoneNum + "'");

            if (ZoneR.Length > 0)
            {
                int i = -1;
                while (++i < ZoneR.Length)
                {
                    Roof roof = new Roof(ZoneR[i][0], ZoneR[i][2], Program.UTIL.ToDoubleOrZero(ZoneR[i][1]), Program.UTIL.ToDoubleOrZero(ZoneR[i][3]), Program.UTIL.ToDoubleOrZero(ZoneR[i][4]), ZoneR[i][5], ZoneR[i][6], ZoneR[i][7]);
                    zoneRoof.Add(roof);
                }
            }
        }
        public void LoadData_Floor()
        {     //존 바닥 정보 가져오기
            zoneFloor.Clear(); // 두 번 호출돼도(웜업 패스+본계산) 중복 누적되지 않도록
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
                                if (Program.UTIL.ToDoubleOrZero(ZoneF[i][3]) >= 3)
                                { fx_f = 0.3; }
                                else if (Program.UTIL.ToDoubleOrZero(ZoneF[i][3]) >= 1)
                                { fx_f = 0.55; }
                                else if (Program.UTIL.ToDoubleOrZero(ZoneF[i][3]) > 0.3)
                                { fx_f = 0.7; }
                                else { fx_f = 0.8; }
                                break;
                            }
                        case "단열지하":
                            {
                                if (Program.UTIL.ToDoubleOrZero(ZoneF[i][3]) >= 3)
                                { fx_f = 0.2; }
                                else if (Program.UTIL.ToDoubleOrZero(ZoneF[i][3]) >= 1)
                                { fx_f = 0.45; }
                                else if (Program.UTIL.ToDoubleOrZero(ZoneF[i][3]) > 0.3)
                                { fx_f = 0.55; }
                                else { fx_f = 0.7; }
                                break;
                            }
                        case "비단열지하":
                            {
                                if (Program.UTIL.ToDoubleOrZero(ZoneF[i][3]) >= 3)
                                { fx_f = 0.45; }
                                else if (Program.UTIL.ToDoubleOrZero(ZoneF[i][3]) >= 1)
                                { fx_f = 0.75; }
                                else if (Program.UTIL.ToDoubleOrZero(ZoneF[i][3]) > 0.3)
                                { fx_f = 0.8; }
                                else { fx_f = 0.85; }
                                break;
                            }
                    }

                    Floor floor = new Floor(ZoneF[i][0], ZoneF[i][2], Program.UTIL.ToDoubleOrZero(ZoneF[i][1]), Program.UTIL.ToDoubleOrZero(ZoneF[i][3]), ZoneF[i][5], fx_f);
                    zoneFloor.Add(floor);
                }
            }

        }
        public void LoadData_GWall()
        {   //존 지하벽 정보 가져오기
            zoneGWall.Clear(); // 두 번 호출돼도(웜업 패스+본계산) 중복 누적되지 않도록
            String[][] ZoneG = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.유효열관류율,b.직접간접 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + ZoneNum + "' And  b.직접간접 = '지면'");

            if (ZoneG.Length > 0)
            {
                int i = -1;
                while (++i < ZoneG.Length)
                {
                    double fx_f = 1;
                    if (Program.UTIL.ToDoubleOrZero(ZoneG[i][3]) >= 3)
                    { fx_f = 0.35; }
                    else if (Program.UTIL.ToDoubleOrZero(ZoneG[i][3]) >= 1)
                    { fx_f = 0.55; }
                    else if (Program.UTIL.ToDoubleOrZero(ZoneG[i][3]) > 0.3)
                    { fx_f = 0.65; }
                    else { fx_f = 0.75; }

                    GWall gwall = new GWall(ZoneG[i][0], ZoneG[i][2], Program.UTIL.ToDoubleOrZero(ZoneG[i][1]), Program.UTIL.ToDoubleOrZero(ZoneG[i][3]), fx_f);
                    zoneGWall.Add(gwall);
                }
            }

        }
        public void LoadData_Door()
        {
            zoneDoor.Clear(); // 두 번 호출돼도(웜업 패스+본계산) 중복 누적되지 않도록
            String[][] ZoneD = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.문유효열관류율,b.흡수율,b.직접간접,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionDoor AS b ON a.구조체번호 = b.번호 where a.존 = '" + ZoneNum + "'");

            if (ZoneD.Length > 0)
            {
                int i = -1;
                while (++i < ZoneD.Length)
                {
                    Door door = new Door(ZoneD[i][0], ZoneD[i][2], Program.UTIL.ToDoubleOrZero(ZoneD[i][1]), Program.UTIL.ToDoubleOrZero(ZoneD[i][3]), Program.UTIL.ToDoubleOrZero(ZoneD[i][4]), ZoneD[i][5], ZoneD[i][6], ZoneD[i][7]);
                    zoneDoor.Add(door);
                }
            }
        }
        public void LoadData_Win()
        {
            //존 창문 정보 가져오기
            zoneWin.Clear(); // 두 번 호출돼도(웜업 패스+본계산) 중복 누적되지 않도록
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
                            Window win = new Window(ZoneWin[i][0], ZoneWin[i][7], ZoneWin[i][2], Program.UTIL.ToDoubleOrZero(ZoneWin[i][1]), Program.UTIL.ToDoubleOrZero(ZoneWin[i][3]), Program.UTIL.ToDoubleOrZero(ZoneWin[i][4]), ZoneWin_P[0][0], Program.UTIL.ToDoubleOrZero(ZoneWin[i][6]), Program.UTIL.ToDoubleOrZero(ZoneWin_P[0][1]), Program.UTIL.ToDoubleOrZero(ZoneWin_P[0][2]), Program.UTIL.ToDoubleOrZero(Blind[0][0]), Program.UTIL.ToDoubleOrZero(Blind[0][1]), ZoneWin[i][8], ZoneWin[i][9]);
                            zoneWin.Add(win);
                        }
                        else
                        {
                            Window win = new Window(ZoneWin[i][0], ZoneWin[i][7], ZoneWin[i][2], Program.UTIL.ToDoubleOrZero(ZoneWin[i][1]), Program.UTIL.ToDoubleOrZero(ZoneWin[i][3]), Program.UTIL.ToDoubleOrZero(ZoneWin[i][4]), ZoneWin_P[0][0], Program.UTIL.ToDoubleOrZero(ZoneWin[i][6]), Program.UTIL.ToDoubleOrZero(ZoneWin_P[0][1]), Program.UTIL.ToDoubleOrZero(ZoneWin_P[0][2]), 0, 0, ZoneWin[i][8], ZoneWin[i][9]);
                            zoneWin.Add(win);
                        }
                    }
                }
            }
        }
        public void LoadData_CW()
        {
            //존 커튼월 정보 가져오기
            zoneCW.Clear(); // 두 번 호출돼도(웜업 패스+본계산) 중복 누적되지 않도록
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
                                CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], Program.UTIL.ToDoubleOrZero(ZoneCW[i][1]), Program.UTIL.ToDoubleOrZero(CW_g[0][0]), Program.UTIL.ToDoubleOrZero(CW_g[0][1]), Program.UTIL.ToDoubleOrZero(CW_g[0][2]), Program.UTIL.ToDoubleOrZero(Blind[0][0]), Program.UTIL.ToDoubleOrZero(CW_g[0][3]), Program.UTIL.ToDoubleOrZero(Blind[0][1]), 0, 0, 0, 0, 0, 0, 0, 0, Program.UTIL.ToDoubleOrZero(ZoneCW[i][1]), Program.UTIL.ToDoubleOrZero(CW_g[0][4]), ZoneCW[i][4], ZoneCW[i][5], "유리부분");
                                zoneCW.Add(cw);
                            }
                            else
                            {
                                CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], Program.UTIL.ToDoubleOrZero(ZoneCW[i][1]), Program.UTIL.ToDoubleOrZero(CW_g[0][0]), Program.UTIL.ToDoubleOrZero(CW_g[0][1]), Program.UTIL.ToDoubleOrZero(CW_g[0][2]), 0, Program.UTIL.ToDoubleOrZero(CW_g[0][3]), 0, 0, 0, 0, 0, 0, 0, 0, 0, Program.UTIL.ToDoubleOrZero(ZoneCW[i][1]), Program.UTIL.ToDoubleOrZero(CW_g[0][4]), ZoneCW[i][4], ZoneCW[i][5], "유리부분");
                                zoneCW.Add(cw);
                            }
                        }

                    }
                    else if (ZoneCW[i][2] == "패널부분")
                    {
                        String[][] CW_p = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "패널부분열관류율,패널흡수율,설치열교가산치", "번호 = '" + ZoneCW[i][3] + "'");
                        if (CW_p.Length > 0)
                        {
                            CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], 0, 0, 0, 0, 0, 0, 0, Program.UTIL.ToDoubleOrZero(ZoneCW[i][1]), Program.UTIL.ToDoubleOrZero(CW_p[0][0]), Program.UTIL.ToDoubleOrZero(CW_p[0][1]), 0, 0, 0, 0, 0, Program.UTIL.ToDoubleOrZero(ZoneCW[i][1]), Program.UTIL.ToDoubleOrZero(CW_p[0][2]), ZoneCW[i][4], ZoneCW[i][5], "패널부분");
                            zoneCW.Add(cw);
                        }
                    }
                    else
                    {
                        String[][] CW_d = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "출입문부분열관류율,출입문부분유리면적비,출입문태양열취득률,출입문빛투과율,설치열교가산치", "번호 = '" + ZoneCW[i][3] + "'");
                        if (CW_d.Length > 0)
                        {
                            CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, Program.UTIL.ToDoubleOrZero(ZoneCW[i][1]), Program.UTIL.ToDoubleOrZero(CW_d[0][0]), Program.UTIL.ToDoubleOrZero(CW_d[0][1]), Program.UTIL.ToDoubleOrZero(CW_d[0][2]), Program.UTIL.ToDoubleOrZero(CW_d[0][3]), Program.UTIL.ToDoubleOrZero(ZoneCW[i][1]), Program.UTIL.ToDoubleOrZero(CW_d[0][4]), ZoneCW[i][4], ZoneCW[i][5], "출입문부분");
                            zoneCW.Add(cw);
                        }
                    }
                }
            }
        }



        // 이 존 자기 자신의 데이터만으로 계산되는 값들(b_ztu, Zone_HT_Di_tot, Inwall_f, Slab_f 등) 전담.
        // 인접존을 전혀 조회하지 않으므로 처리순서와 무관하게 항상 정확 — 웜업 패스가 모든 존에 대해
        // 먼저 이 메서드를 호출해, 아래 ZoneHT()가 다른 존의 이 값들을 읽을 때 항상 준비돼 있도록 함.
        // 두 번(웜업+본계산) 호출돼도 안전하도록 += 로 누적되는 필드를 전부 리셋.
        public void Zone_bztu()
        {
            Zone_HT_TB_Wall = 0; Zone_HT_Di_Wall = 0; Zone_HT_Indi_Wall = 0;
            Zone_HT_TB_Roof = 0; Zone_HT_Di_Roof = 0; Zone_HT_Indi_Roof = 0;
            Zone_HT_Floor = 0; Zone_HT_TB_Floor = 0;
            Zone_HT_GWall = 0; Zone_HT_TB_GWall = 0;
            Zone_HT_Di_Door = 0; Zone_HT_Indi_Door = 0;
            Zone_HT_Di_Win = 0; Zone_HT_Indi_Win = 0; Zone_HT_TB_Win = 0;
            Zone_HT_CW = 0; Zone_HT_TB_CW = 0;

            // b_ztu 전용 집계 — 관류 손실용 Zone_HT_Di_tot/Zone_HT_Indi_*와는 별개(할인 없이 순수 UA만 사용).
            // H_ue_air: 외기와 공기로 접하는 요소(벽/지붕/문/창/커튼월, 직접·간접·통기층 구분 없음, 열교 포함) — 1.5배 스케일링 대상
            // H_ue_ground: 지반과 접하는 요소(바닥의 지면위/단열지하/비단열지하, 지하벽) — 침기 영향 없어 1.5배 제외
            double H_ue_air = 0, H_ue_ground = 0;

            //외벽 HT
            for (int i = 0; i < zoneWall.Count; i++)
            {
                Wall zonewall = (Wall)zoneWall[i]; //List를 class 객체로 변환 
                HTCalc htcalc = new HTCalc();

                double[] zoneWall_HT = new double[zoneWall.Count];
                double[] zoneWall_HT_TB = new double[zoneWall.Count];

                zoneWall_HT[i] = htcalc.Calc(zonewall.Ueff(), zonewall.Area());
                zoneWall_HT_TB[i] = htcalc.Calc(Utb[0], zonewall.Area());

                Zone_HT_TB_Wall += zoneWall_HT_TB[i];
                H_ue_air += zoneWall_HT[i] + zoneWall_HT_TB[i]; // b_ztu용: 직접/간접/통기층 구분 없이 raw UA + 열교

                if (zonewall.DiIndi() == "직접외기")
                {
                    Zone_HT_Di_Wall += zoneWall_HT[i];

                }
                else if (zonewall.DiIndi() == "통기층")
                {
                    Zone_HT_Di_Wall += zoneWall_HT[i];
                }
                else if (zonewall.DiIndi() == "간접외기")
                {
                    Zone_HT_Indi_Wall += 0.8 * zoneWall_HT[i]; // H = b * U *A //간접외기  b= 0.8
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
                zoneRoof_HT_TB[i] = htcalc.Calc(Utb[1], zoneroof.Area());

                Zone_HT_TB_Roof += zoneRoof_HT_TB[i];
                H_ue_air += zoneRoof_HT[i] + zoneRoof_HT_TB[i]; // b_ztu용: 직접/간접/통기층 구분 없이 raw UA + 열교


                if (zoneroof.DiIndi() == "직접외기")
                {
                    Zone_HT_Di_Roof += zoneRoof_HT[i];

                }
                else if (zoneroof.DiIndi() == "통기층")
                {
                    Zone_HT_Di_Roof += zoneRoof_HT[i];

                }
                else if (zoneroof.DiIndi() == "간접외기")
                {
                    Zone_HT_Indi_Roof += 0.8 * zoneRoof_HT[i]; // H = b * U *A //간접외기  b= 0.8
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
                zoneFloor_HT_TB[i] = htcalc.Calc(Utb[2], zonefloor.Area());


                Zone_HT_Floor += zoneFloor_HT[i];
                Zone_HT_TB_Floor += zoneFloor_HT_TB[i];

                // b_ztu용: 바닥은 GroundType()이 직접외기/간접외기(차고 등 공기접촉)면 H_ue_air,
                // 지면위/단열지하/비단열지하(지반접촉)면 H_ue_ground — 둘 다 순수 UA(할인 없음)
                if (zonefloor.GroundType() == "직접외기" || zonefloor.GroundType() == "간접외기")
                {
                    H_ue_air += zoneFloor_HT[i] + zoneFloor_HT_TB[i];
                }
                else
                {
                    H_ue_ground += zoneFloor_HT[i] + zoneFloor_HT_TB[i];
                }
            }

            //지하벽 HT
            for (int i = 0; i < zoneGWall.Count; i++)
            {
                GWall zonegwall = (GWall)zoneGWall[i]; //List를 class 객체로 변환 
                HTCalc htcalc = new HTCalc();

                double[] zoneGWall_HT = new double[zoneGWall.Count];
                double[] zoneGWall_HT_TB = new double[zoneGWall.Count];

                zoneGWall_HT[i] = htcalc.Calc(zonegwall.Ueff(), zonegwall.Area());
                zoneGWall_HT_TB[i] = htcalc.Calc(Utb[2], zonegwall.Area());

                Zone_HT_GWall += zoneGWall_HT[i];
                Zone_HT_TB_GWall += zoneGWall_HT_TB[i];
                H_ue_ground += zoneGWall_HT[i] + zoneGWall_HT_TB[i]; // b_ztu용: 지하벽은 항상 지반 접촉, 순수 UA
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
                    Zone_HT_Indi_Door += 0.8 * zoneDoor_HT[i]; // // H = b * U *A //간접외기  b= 0.8
                }
                Zone_HT_Door = Zone_HT_Di_Door + Zone_HT_Indi_Door; //나중에 설치열교 관류열전달계수 적용 해야함
                H_ue_air += zoneDoor_HT[i]; // b_ztu용: 직접/간접 구분 없이 raw UA
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
                    Zone_HT_Indi_Win += 0.8 * zoneWin_HT[i]; // H = b * U *A //간접외기  b= 0.8
                }
                double[] zoneWin_HT_TB = new double[zoneWin.Count];
                zoneWin_HT_TB[i] = htcalc.Calc(zonewin.Uinst(), zonewin.Area());
                Zone_HT_TB_Win += zoneWin_HT_TB[i];
                Zone_HT_Win = Zone_HT_Di_Win + Zone_HT_Indi_Win;
                H_ue_air += zoneWin_HT[i] + zoneWin_HT_TB[i]; // b_ztu용: 직접/간접 구분 없이 raw UA + 설치열교
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
                H_ue_air += zoneCW_HT_g[i] + zoneCW_HT_p[i] + zoneCW_HT_d[i] + zoneCW_HT_TB[i]; // b_ztu용: 순수 UA + 설치열교
            }

            Zone_HT_TB_tot = Zone_HT_TB_Wall + Zone_HT_TB_Roof + Zone_HT_TB_Floor + Zone_HT_TB_GWall + Zone_HT_TB_Win + Zone_HT_TB_Door + Zone_HT_TB_CW;

            for (int hc = 0; hc < 2; hc++)
            {
                double HT_InwallSlab = 0;
                for (int i = 0; i < zoneInWall.Count; i++)
                {
                    InWall zoneInwall = (InWall)zoneInWall[i];
                    HT_InwallSlab += zoneInwall.U() * zoneInwall.Area();
                }
                for (int i = 0; i < zoneSlab.Count; i++)
                {
                    Slab zoneslab = (Slab)zoneSlab[i];
                    HT_InwallSlab += zoneslab.U() * zoneslab.Area();
                }
                Zone_HT_Di_tot = Zone_HT_Di_Wall + Zone_HT_Di_Roof + Zone_HT_Di_Win + Zone_HT_Di_Door + Zone_HT_CW + Zone_HT_TB_tot; //직접외기 바닥 포함시켜야 함 
                Zone_HT_tot[hc] = Zone_HT_Di_tot + Zone_HT_Indi_Wall + Zone_HT_Indi_Roof + Zone_HT_Indi_Win + Zone_HT_Indi_Door + Zone_HT_Floor;
                
                // b_ztu 전용 집계(H_ue_air/H_ue_ground, 순수 UA)로 계산 — 관류손실용 Zone_HT_Di_tot/Zone_HT_tot와는 별개.
                // HT_InwallSlab(내벽/슬래브, 인접 조닝존 쪽)이 H_iu, H_ue_air만 1.5배(52016 식(6), 침기 영향 반영),
                // H_ue_ground(지반 접촉, 침기 없음)는 분자·분모 모두 ×1로만 참여.
                b_ztu[hc] = (1.5 * H_ue_air + H_ue_ground) / (HT_InwallSlab + H_ue_ground + 1.5 * H_ue_air);

                for (int i = 0; i < zoneInWall.Count; i++)
                {
                    InWall zoneInwall = (InWall)zoneInWall[i];
                    zoneInwall.Inwall_f = zoneInwall.U() * zoneInwall.Area() / HT_InwallSlab;
                }
                for (int i = 0; i < zoneSlab.Count; i++)
                {
                    Slab zoneslab = (Slab)zoneSlab[i];
                    zoneslab.Slab_f = zoneslab.U() * zoneslab.Area() / HT_InwallSlab;
                }
            }
        }

        // 인접존(zoneZTU)이 이 존(this) 기준으로 ZTU(비냉난방 또는 냉난방 모드 불일치)로 취급돼야
        // 하는지 판정 — 관류(HT)/일사(Qstr_ztu)/내부발열(QI_ztu)/환기(HV_adjzone) 전부 동일 기준 사용.
        private bool IsAdjacentZTU(Zone zoneZTU, int hc)
        {
            return (zoneHC == "난방" && (zoneZTU.zoneHC == "비냉난방" || zoneZTU.zoneHC == "냉방")) ||
                   (zoneHC == "냉방" && (zoneZTU.zoneHC == "비냉난방" || zoneZTU.zoneHC == "난방")) ||
                   (zoneHC == "냉난방" && hc == 0 && (zoneZTU.zoneHC == "비냉난방" || zoneZTU.zoneHC == "냉방")) ||
                   (zoneHC == "냉난방" && hc == 1 && (zoneZTU.zoneHC == "비냉난방" || zoneZTU.zoneHC == "난방"));
        }

        // 인접 ZTU존의 자기완결적 값(b_ztu, Zone_HT_Di_tot, Inwall_f/Slab_f — 전부 Zone_bztu()가 채워둠)을
        // 실제로 읽어서 이 존의 Zone_HT_Inwall/Zone_HT_Slab를 계산하는 부분만 담당.
        // 웜업 패스에서 모든 존의 Zone_bztu()가 먼저 끝난 뒤 본계산에서 호출돼야 안전.
        // 두 번 호출돼도 안전하도록 += 로 누적되는 필드를 리셋.
        public void ZoneHT() //관류 HT 계산
        {
            Zone_HT_Inwall[0] = 0; Zone_HT_Inwall[1] = 0;
            Zone_HT_Slab[0] = 0; Zone_HT_Slab[1] = 0;

            //내벽 HT
            ArrayList processedZTU_InWall_HT = new ArrayList(); // 이 존이 이미 계산한 인접 ZTU존 번호 목록 (세그먼트 중복 방지)
            for (int i = 0; i < zoneInWall.Count; i++)
            {
                InWall zoneInwall = (InWall)zoneInWall[i];
                Zone zoneZTU = Program.CALC.getZone(zoneInwall.SideZone());

                if (!processedZTU_InWall_HT.Contains(zoneInwall.SideZone()))
                {
                    processedZTU_InWall_HT.Add(zoneInwall.SideZone());

                    // 이 ZTU존에서 이 존(this)으로 연결된 세그먼트들의 배분계수는 합산(sum)해서 사용
                    double f_total = 0;
                    for (int a = 0; a < zoneZTU.zoneInWall.Count; a++)
                    {
                        InWall ztuInwall = (InWall)zoneZTU.zoneInWall[a];
                        if (ztuInwall.SideZone() == this.ZoneNum)
                        {
                            f_total += ztuInwall.f_ztc_ztu();
                        }
                    }

                    for (int hc = 0; hc < 2; hc++)
                    {
                        bool isAdjacentZTU = IsAdjacentZTU(zoneZTU, hc);

                        // ztu 존은 이미 "조닝된 비냉난방존"이므로 항상 internal type — 식 111: H = (1 - b_ztu) × U_외벽 × A_외벽, ZTU존당 1회만 반영
                        double contribution = isAdjacentZTU
                            ? (1 - zoneZTU.b_ztu[hc]) * zoneZTU.Zone_HT_Di_tot * f_total
                            : 0;

                        Zone_HT_Inwall[hc] += contribution;
                    }
                }
            }

            // 층간바닥 HT
            ArrayList processedZTU_Slab_HT = new ArrayList(); // 이 존이 이미 계산한 인접 ZTU존 번호 목록 (세그먼트 중복 방지)
            for (int i = 0; i < zoneSlab.Count; i++)
            {
                Slab zoneslab = (Slab)zoneSlab[i];
                Zone zoneZTU = Program.CALC.getZone(zoneslab.SideZone());

                if (!processedZTU_Slab_HT.Contains(zoneslab.SideZone()))
                {
                    processedZTU_Slab_HT.Add(zoneslab.SideZone());

                    // 이 ZTU존에서 이 존(this)으로 연결된 세그먼트들의 배분계수는 합산(sum)해서 사용
                    double f_total = 0;
                    for (int a = 0; a < zoneZTU.zoneSlab.Count; a++)
                    {
                        Slab ztuslab = (Slab)zoneZTU.zoneSlab[a];
                        if (ztuslab.SideZone() == this.ZoneNum)
                        {
                            f_total += ztuslab.f_ztc_ztu();
                        }
                    }

                    for (int hc = 0; hc < 2; hc++)
                    {
                        bool isAdjacentZTU = IsAdjacentZTU(zoneZTU, hc);

                        // 위와 동일 — 항상 internal type, 식 111, ZTU존당 1회만 반영
                        double contribution = isAdjacentZTU
                            ? (1 - zoneZTU.b_ztu[hc]) * zoneZTU.Zone_HT_Di_tot * f_total
                            : 0;

                        Zone_HT_Slab[hc] += contribution;
                    }
                }
            }
        }


        public void Zone_n50()
        {
            // "존별 표준값 적용" 방식이면 ZoneEnvelope.cs의 Show_ZoneN50()이 이미 계산해서
            // ZoneGeneral_Form.n50에 저장해둔 값을 그대로 씀 — 아래 구조체별 분배 계산은 스킵
            string[][] Method = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기밀적용방식", "");
            if (Method.Length > 0 && Method[0][0] == "존별")
            {
                string[][] SavedN50 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "n50", "존번호 = '" + ZoneNum + "'");
                n50 = SavedN50.Length > 0 && SavedN50[0][0] != "" ? Program.UTIL.ToDoubleOrZero(SavedN50[0][0]) : 0;
                return;
            }

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
                        AreaDirect_tot += Program.UTIL.ToDoubleOrZero(ZoneE[n][3]);
                        if (ZoneE[n][2] == "출입문부분")
                        { CMH += Program.UTIL.ToDoubleOrZero(ZoneE[n][3]) * Door_q50; }
                        else
                        {
                            CMH += Program.UTIL.ToDoubleOrZero(ZoneE[n][3]) * Win_q50;
                        }
                    }
                    else if (ZoneE[n][1] == "외벽")
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "직접간접", "번호='" + ZoneE[n][5] + "'");
                        if (Value.Length > 0)
                        {
                            if (Value[0][0] == "직접외기")
                            {
                                AreaDirect_tot += Program.UTIL.ToDoubleOrZero(ZoneE[n][3]);
                                CMH += Program.UTIL.ToDoubleOrZero(ZoneE[n][3]) * Wall_q50;
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
                                AreaDirect_tot += Program.UTIL.ToDoubleOrZero(ZoneE[n][3]);
                                CMH += Program.UTIL.ToDoubleOrZero(ZoneE[n][3]) * Roof_q50;
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
                                AreaDirect_tot += Program.UTIL.ToDoubleOrZero(ZoneE[n][3]);
                                CMH += Program.UTIL.ToDoubleOrZero(ZoneE[n][3]) * Win_q50;
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
                                AreaDirect_tot += Program.UTIL.ToDoubleOrZero(ZoneE[n][3]);
                                CMH += Program.UTIL.ToDoubleOrZero(ZoneE[n][3]) * Door_q50;
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
            double V = zoneArea * zoneHeight;

            // nSUP/nETA를 여기서 한 번만 계산해서 아래 각 함수에 그대로 넘김(중복 계산 제거).
            // V_SUP_z/V_ETA_z는 파라미터가 아니라 이 Zone의 필드 — LoadData_Ventil()이 이미
            // AHUZoneVent_Form에서 읽어 채워둠(Zone_LoadData()가 Zone_Calc()보다 먼저 실행되므로
            // ZoneHV()가 불릴 때는 항상 준비돼 있음).
            double nmech_SUP = Vmech_SUP / V;
            double nmech_ETA = Vmech_ETA / V;
            double nz_SUP = V_SUP_z / V;
            double nz_ETA = V_ETA_z / V;
            double nSUP = nmech_SUP + nz_SUP;
            double nETA = nmech_ETA + nz_ETA;

            // n_inf,0(식 64, 순수 n50×e)과 fe(기계환기로 인한 침기 저감계수) — ninf_Calc()/nwin_Calc()
            // 둘 다 공유해서 씀(예전엔 각자 따로 계산해서 중복이었음)
            double n_inf0 = n50 * e;
            double fe = 1;
            if (nSUP != 0 && n50 != 0) // n50=0일 때 0으로 나누는 것 방지
            { fe = 1 / (1 + f / e * Math.Pow(((nETA - nSUP) / n50), 2)); }

            nmech = hvcalc.nmech_Calc(nmech_SUP, th_op_d);
            // Zone_HV_mech는 b_mech(월별로 다름)가 적용된 [hc,mth] 배열이라 여기서 안 만들고
            // ZoneQV()/ZoneQV2()에서 그때그때 채움(QTsink_Wall과 동일한 1차/2차 패스 패턴)

            // theta_v_mech — ZoneQV()/ZoneQV2() 둘 다 공유해서 쓰는 공급공기온도. theta_i_set이나
            // AHU 실측치만 쓰고 theta_i[hc,mth](동적값)는 필요 없어서 여기서 한 번만 계산해두면 됨
            for (int mth = 0; mth < 12; mth++)
            {
                if (SelectHRV != null)
                {
                    AHU AHU1 = Program.CALC.getAHU(SelectHRV);
                    if (AHU1 != null)
                    {
                        theta_v_mech[0, mth] = AHU1.theta_SA_hr[0, mth];
                        theta_v_mech[1, mth] = AHU1.theta_SA_hr[1, mth];
                    }
                    else
                    {
                        theta_v_mech[0, mth] = theta_e[mth] + eta_V_mech[0] * (theta_i_set[0] - theta_e[mth]);
                        theta_v_mech[1, mth] = theta_e[mth] + eta_V_mech[1] * (theta_i_set[1] - theta_e[mth]);
                    }
                }
                else
                {
                    theta_v_mech[0, mth] = theta_e[mth] + eta_V_mech[0] * (theta_i_set[0] - theta_e[mth]);
                    theta_v_mech[1, mth] = theta_e[mth] + eta_V_mech[1] * (theta_i_set[1] - theta_e[mth]);
                }
            }

            // nz는 리포트 저장용(Zone_HCneed_Result.nz, 인접존 유입 총량 기준 환기횟수 참고값)으로만
            // 유지 — 실제 열전달 계산(Zone_HV_z)에는 안 씀
            nz = hvcalc.nz_Calc(nz_SUP, th_op_d);

            // 인접존 열전달은 "받는" 쪽(인접존=ZoneNum)만 기여 — 들어오는 공기 온도는 보내는 존의 b_ztu로
            // 정해지므로 연결별로 b를 곱한 뒤 합산(hc별로 b가 다를 수 있어 따로 계산).
            string[][] AdjConn = Program.DB.getValue(DB.type.ProjDB, "AHUZoneVent_Form", "존,인접존배기량", "인접존 = '" + ZoneNum + "'");
            for (int hc = 0; hc < 2; hc++)
            {
                double n_z = 0;
                for (int i = 0; i < AdjConn.Length; i++)
                {
                    double.TryParse(AdjConn[i][1], out double q);
                    Zone zoneZTU = Program.CALC.getZone(AdjConn[i][0]); // AdjConn[i][0] = 존(보내는 쪽)
                    bool isAdjacentZTU = IsAdjacentZTU(zoneZTU, hc);
                    double b = isAdjacentZTU ? zoneZTU.b_ztu[hc] : 0;
                    n_z += b * (q / V) * th_op_d / 24;
                }
                Zone_HV_z[hc] = hvcalc.HV_Calc(n_z, V);
            }

            ninf = hvcalc.ninf_Calc(n_inf0, fe, th_op_d);
            Zone_HV_inf = hvcalc.HV_Calc(ninf, V);

            nwin = hvcalc.nwin_Calc(nSUP, nETA, th_op_d, twd_d, (VA_wd / zoneHeight), n_inf0, fe);

            Zone_HV_win = hvcalc.HV_Calc(nwin, V);

            // Zone_HV_tot[hc,mth]는 Zone_HV_mech[hc,mth]가 확정된 뒤(ZoneQV()/ZoneQV2()에서)에나
            // 합산 가능해서 여기선 계산 안 함
            HV_tot_max = hvcalc.HV_Calc(0.1, V) + hvcalc.HV_Calc(n50 * e, V);
        }


        public void ZoneQT()//관류 열전달 계산
        {

            QTCalc qtcalc = new QTCalc();
            {
                //외벽 QT계산
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {
                        QTsink_Wall[hc, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i_set[hc], Zone_HT_Wall, dmth[mth]);
                        QTsink_TB[hc, mth] += qtcalc.Calc_sink(theta_e[mth], theta_i_set[hc], Zone_HT_TB_Wall, dmth[mth]);
                    }
                }

                QTsink_Wall_max = (Zone_HT_Wall * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_Wall_Cmax = (Zone_HT_Wall * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_Wall_Cmax = (Zone_HT_Wall * (theta_e_max - theta_i_c_max_d)); }

                QTsink_TB_max += (Zone_HT_TB_Wall * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_TB_Cmax += (Zone_HT_TB_Wall * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_TB_Cmax += (Zone_HT_TB_Wall * (theta_e_max - theta_i_c_max_d)); }

            }

            {
                //지붕 QT계산
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {
                        QTsink_Roof[hc, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i_set[hc], Zone_HT_Roof, dmth[mth]);
                        QTsink_TB[hc, mth] += qtcalc.Calc_sink(theta_e[mth], theta_i_set[hc], Zone_HT_TB_Roof, dmth[mth]);
                    }
                }

                QTsink_Roof_max = (Zone_HT_Roof * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_Roof_Cmax = (Zone_HT_Roof * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_Roof_Cmax = (Zone_HT_Roof * (theta_e_max - theta_i_c_max_d)); }

                QTsink_TB_max += (Zone_HT_TB_Roof * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_TB_Cmax += (Zone_HT_TB_Roof * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_TB_Cmax += (Zone_HT_TB_Roof * (theta_e_max - theta_i_c_max_d)); }

            }



            //바닥 QT계산
            double[,,] zoneFloors_QTsink = new double[zoneFloor.Count, 2, 12];
            double[,,] zoneFloors_QTsource = new double[zoneFloor.Count, 2, 12];
            double[,,] zoneFloors_QTsink_TB = new double[zoneFloor.Count, 2, 12];
            double[,,] zoneFloors_QTsource_TB = new double[zoneFloor.Count, 2, 12];
            int i = -1;
            while (++i < zoneFloor.Count)
            {
                Floor zonefloor = (Floor)zoneFloor[i];
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {

                        // theta_s(지반 등가온도) 대신 theta_e 기준 + Fx 가중 H로 계산(수학적으로 동일, Calc_sink가 (Ti-Te)*H 선형이라 Fx를 H에 곱하나 온도에 녹이나 같음)
                        zoneFloors_QTsink[i, hc, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i_set[hc], zonefloor.Fx() * zonefloor.Ueff() * zonefloor.Area(), dmth[mth]);
                        zoneFloors_QTsink_TB[i, hc, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i_set[hc], Utb[2] * zonefloor.Area(), dmth[mth]);


                        QTsink_Floor[hc, mth] += zoneFloors_QTsink[i, hc, mth];
                        //QTsource_Floor[hc, wewd, mth] += zoneFloors_QTsource[i, hc, wewd, mth];
                        QTsink_TB[hc, mth] += zoneFloors_QTsink_TB[i, hc, mth];
                        //QTsource_TB[hc, wewd, mth] += zoneFloors_QTsource_TB[i, hc, wewd, mth
                    }
                }

                QTsink_Floor_max += (zonefloor.Ueff() * zonefloor.Area() * (theta_i_h_min - theta_e_min));

                QTsink_TB_max += (Utb[2] * zonefloor.Area() * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_Floor_Cmax += (zonefloor.Ueff() * zonefloor.Area() * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_Floor_Cmax += (zonefloor.Ueff() * zonefloor.Area() * (theta_e_max - theta_i_c_max_d)); }

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_TB_Cmax += (Utb[2] * zonefloor.Area() * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_TB_Cmax += (Utb[2] * zonefloor.Area() * (theta_e_max - theta_i_c_max_d)); }
            }

            //지하벽 QT계산    
            i = -1;
            double[,,] zoneGWalls_QTsink = new double[zoneGWall.Count, 2, 12];
            double[,,] zoneGWalls_QTsource = new double[zoneGWall.Count, 2, 12];
            double[,,] zoneGWalls_QTsink_TB = new double[zoneGWall.Count, 2, 12];
            double[,,] zoneGWalls_QTsource_TB = new double[zoneGWall.Count, 2, 12];
            while (++i < zoneGWall.Count)
            {
                GWall zonegwall = (GWall)zoneGWall[i];
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {

                        // theta_s_GWall 대신 theta_e 기준 + Fx 가중 H (바닥과 동일한 이유로 수학적으로 동일)
                        zoneGWalls_QTsink[i, hc, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i_set[hc], zonegwall.Fx() * zonegwall.Ueff() * zonegwall.Area(), dmth[mth]);
                        zoneGWalls_QTsink_TB[i, hc, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i_set[hc], zonegwall.Fx() * Utb[2] * zonegwall.Area(), dmth[mth]);


                        QTsink_GWall[hc, mth] += zoneGWalls_QTsink[i, hc, mth];
                        //  QTsource_GWall[hc, wewd, mth] += zoneGWalls_QTsource[i, hc, wewd, mth];
                        QTsink_TB[hc, mth] += zoneGWalls_QTsink_TB[i, hc, mth];
                        //QTsource_TB[hc, wewd, mth] += zoneGWalls_QTsource_TB[i, hc, wewd, mth];
                    }
                }

                QTsink_GWall_max += (zonegwall.Ueff() * zonegwall.Area() * (theta_i_h_min - theta_e_min));

                QTsink_TB_max += (Utb[2] * zonegwall.Area() * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_GWall_Cmax += (zonegwall.Ueff() * zonegwall.Area() * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_GWall_Cmax += (zonegwall.Ueff() * zonegwall.Area() * (theta_e_max - theta_i_c_max_d)); }

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_TB_Cmax += (Utb[2] * zonegwall.Area() * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_TB_Cmax += (Utb[2] * zonegwall.Area() * (theta_e_max - theta_i_c_max_d)); }
            }

            {
                //출입문 QT계산
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {
                        QTsink_Door[hc, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i_set[hc], Zone_HT_Door, dmth[mth]);
                        QTsink_TB[hc, mth] += qtcalc.Calc_sink(theta_e[mth], theta_i_set[hc], Zone_HT_TB_Door, dmth[mth]); //rce_TB[hc, wewd, mth] = qtcalc.Calc_source(theta_e[mth], theta_i[hc, wewd, mth], Zone_HT_TB_Door);

                    }
                }

                QTsink_Door_max = (Zone_HT_Door * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_Door_Cmax = (Zone_HT_Door * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_Door_Cmax = (Zone_HT_Door * (theta_e_max - theta_i_c_max_d)); }

                QTsink_TB_max += (Zone_HT_TB_Door * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_TB_Cmax += (Zone_HT_TB_Door * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_TB_Cmax += (Zone_HT_TB_Door * (theta_e_max - theta_i_c_max_d)); }

            }
            {
                //커튼월창 QT계산
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {
                        QTsink_CW[hc, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i_set[hc], Zone_HT_CW, dmth[mth]);
                        QTsink_TB[hc, mth] += qtcalc.Calc_sink(theta_e[mth], theta_i_set[hc], Zone_HT_TB_CW, dmth[mth]);
                    }
                }

                QTsink_CW_max = (Zone_HT_CW * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_CW_Cmax = (Zone_HT_CW * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_CW_Cmax = (Zone_HT_CW * (theta_e_max - theta_i_c_max_d)); }

                QTsink_TB_max += (Zone_HT_TB_CW * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_TB_Cmax += (Zone_HT_TB_CW * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_TB_Cmax += (Zone_HT_TB_CW * (theta_e_max - theta_i_c_max_d)); }

            }
            {
                //창호 QT계산
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {
                        QTsink_Win[hc, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i_set[hc], Zone_HT_Win, dmth[mth]);
                        QTsink_TB[hc, mth] += qtcalc.Calc_sink(theta_e[mth], theta_i_set[hc], Zone_HT_TB_Win, dmth[mth]);
                    }
                }

                QTsink_Win_max = (Zone_HT_Win * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_Win_Cmax = (Zone_HT_Win * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_Win_Cmax = (Zone_HT_Win * (theta_e_max - theta_i_c_max_d)); }

                QTsink_TB_max += (Zone_HT_TB_Win * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_TB_Cmax += (Zone_HT_TB_Win * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_TB_Cmax += (Zone_HT_TB_Win * (theta_e_max - theta_i_c_max_d)); }

            }
            {
                //내벽 QT계산
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {
                        QTsink_Inwall[hc, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i_set[hc], Zone_HT_Inwall[hc], dmth[mth]);
                    }
                }
            }
            {
                //층간바닥 QT계산
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {
                        QTsink_Slab[hc, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i_set[hc], Zone_HT_Slab[hc], dmth[mth]);
                    }
                }
            }



            // QT_tot계산
            for (int hc = 0; hc <= 1; hc++)
            {
                for (int mth = 0; mth <= 11; mth++)
                {

                    QTsink_tot[hc, mth] = QTsink_TB[hc, mth] + QTsink_Wall[hc, mth] + QTsink_Roof[hc, mth] + QTsink_Door[hc, mth] + QTsink_Win[hc, mth] + QTsink_CW[hc, mth] + QTsink_Floor[hc, mth] + QTsink_GWall[hc, mth] + QTsink_Inwall[hc, mth] + QTsink_Slab[hc, mth];
                    //  QTsource_tot[hc, wewd, mth] = QTsource_TB[hc, wewd, mth] + QTsource_Wall[hc, wewd, mth] + QTsource_Roof[hc, wewd, mth] + QTsource_Door[hc, wewd, mth] + QTsource_Win[hc, wewd, mth] + QTsource_CW[hc, wewd, mth] + QTsource_Floor[hc, wewd, mth] + QTsource_GWall[hc, wewd, mth] + QTsource_Inwall[hc, wewd, mth] + QTsource_Slab[hc, wewd, mth];

                }
            }
            QTsink_tot_max = QTsink_TB_max + QTsink_Wall_max + QTsink_Roof_max + QTsink_Door_max + QTsink_Win_max + QTsink_CW_max + QTsink_Floor_max + QTsink_GWall_max;
            QTsink_tot_Cmax = QTsink_TB_Cmax + QTsink_Wall_Cmax + QTsink_Roof_Cmax + QTsink_Door_Cmax + QTsink_Win_Cmax + QTsink_CW_Cmax + QTsink_Floor_Cmax + QTsink_GWall_Cmax;
            QTsource_tot_Cmax = QTsource_TB_Cmax + QTsource_Wall_Cmax + QTsource_Roof_Cmax + QTsource_Door_Cmax + QTsource_Win_Cmax + QTsource_CW_Cmax + QTsource_Floor_Cmax + QTsource_GWall_Cmax;
        }
        public void ZoneQV() //환기 열전달 계산
        {
            HVCalc hvcalc = new HVCalc();
            double V = zoneArea * zoneHeight;
            double H_mech_raw = hvcalc.HV_Calc(nmech, V); // b 적용 전 순수 H(nmech×V×0.34)

            for (int hc = 0; hc <= 1; hc++)
            {
                for (int mth = 0; mth <= 11; mth++)
                {
                    QVCalc qvcalc = new QVCalc();
                    QV_inf_sink[hc, mth] = qvcalc.Calc_sink(theta_e[mth], theta_i_set[hc], Zone_HV_inf, dmth[mth]);
                    QV_z_sink[hc, mth] = qvcalc.Calc_sink(theta_e[mth], theta_i_set[hc], Zone_HV_z[hc], dmth[mth]);
                    QV_win_sink[hc, mth] = qvcalc.Calc_sink(theta_e[mth], theta_i_set[hc], Zone_HV_win, dmth[mth]);

                    // 다른 환기 항목처럼 θe 기준으로 통일 — b_mech(식 115: (θi-θsup)/(θi-θe))를 H_mech에
                    // 접어넣어서 QV_z_sink 등과 동일한 Calc_sink(θe, ...) 패턴을 씀. 분모가 0(θi_set=θe인
                    // 달)이면 b_mech=0(그 항 기여 없음)으로 처리. theta_v_mech는 ZoneHV()가 미리 채워둔 필드.
                    double b_mech = (theta_i_set[hc] - theta_e[mth] == 0) ? 0 : (theta_i_set[hc] - theta_v_mech[hc, mth]) / (theta_i_set[hc] - theta_e[mth]);
                    Zone_HV_mech[hc, mth] = H_mech_raw * b_mech; // b_mech를 H 안에 포함(1차 패스, θi_set 기준)
                    Zone_HV_tot[hc, mth] = Zone_HV_mech[hc, mth] + Zone_HV_z[hc] + Zone_HV_inf + Zone_HV_win;
                    QV_mech_sink[hc, mth] = qvcalc.Calc_sink(theta_e[mth], theta_i_set[hc], Zone_HV_mech[hc, mth], dmth[mth]);
                    QVsink_tot[hc, mth] = QV_inf_sink[hc, mth] + QV_win_sink[hc, mth] + QV_z_sink[hc, mth] + QV_mech_sink[hc, mth];
                }
            }

            QV_inf_sink_max = Zone_HV_inf * (theta_i_h_min - theta_e_min);
            QV_win_sink_max = Zone_HV_win * (theta_i_h_min - theta_e_min);
            QV_z_sink_max = Zone_HV_z[0] * (theta_i_h_min - theta_i_h_min); // 원래도 (a-a) 형태라 hc 무관하게 항상 0
            QV_mech_sink_max = H_mech_raw * (theta_i_h_min - (theta_e_min + eta_V_mech[1] * (theta_i_h_min - theta_e_min))); // 설계조건 최대치라 b 적용 없이 원래 방식 그대로
            QVsink_tot_max = QV_inf_sink_max + QV_win_sink_max + QV_z_sink_max; //기계환기 제외

            if (theta_i_c_max_d > theta_e_max)
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

        public void ZoneQSop()// 불투명 일사 계산
        {
            //외벽 일사 계산
            double[,] zoneWalls_Is = new double[zoneWall.Count, 12];
            double[,] zoneWalls_Qssource = new double[zoneWall.Count, 12];
            double[,] zoneWalls_Qrad = new double[zoneWall.Count, 12];
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
                    int wall_Degree = (int)Math.Round(Program.UTIL.ToDoubleOrZero(zonewall.Degree()));
                    int wall_Direction = zonewall.Direction_angle();
                    double[] wall_itot = CALC.Itot_mth.GetValueOrDefault((wall_Degree, wall_Direction));
                    string[][] token2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_부하", "일사량", "지역명 ='" + Location[0][0] + "' AND 방향 ='" + zonewall.Direction() + "'");
                    if (zonewall.DiIndi() != "간접외기") //직접외기 벽만 일사 계산
                    {
                        for (int mth = 0; mth <= 11; mth++)
                        {
                            QSopCalc qsopcalc = new QSopCalc();
                            QS_rad qs_rad = new QS_rad();
                            QTCalc qtcalc = new QTCalc();

                            zoneWalls_Qrad[i, mth] = qs_rad.Calc(zonewall.Ueff(), zonewall.Area(), 0.5, dmth[mth]);

                            if (wall_itot != null) // 요구량 — Itot_mth에 이 (기울기,방위각) 캐시가 있을 때만
                            {
                                zoneWalls_Is[i, mth] = wall_itot[mth];
                                zoneWalls_Qssource[i, mth] = qsopcalc.Calc(zonewall.Ueff(), zonewall.Area(), zonewall.α(), zoneWalls_Is[i, mth], dmth[mth]);
                            }

                            if (token2.Length > 0) // 부하 — 요구량과 별개로 독립 판단
                            {
                                if (0.5 * 4.5 * 10 >= zonewall.α() * Program.UTIL.ToDoubleOrZero(token2[0][0]))
                                {
                                    zoneWalls_Qssink_Cmax[i] = qsopcalc.Calc_max(zonewall.Ueff(), zonewall.Area(), zonewall.α(), Program.UTIL.ToDoubleOrZero(token2[0][0]), 0.5);
                                }
                                else
                                {
                                    zoneWalls_Qssource_Cmax[i] = qsopcalc.Calc_max(zonewall.Ueff(), zonewall.Area(), zonewall.α(), Program.UTIL.ToDoubleOrZero(token2[0][0]), 0.5);
                                }
                            }

                            QSopsource_Wall[mth] += zoneWalls_Qssource[i, mth];
                            QS_rad_Wall[mth] += zoneWalls_Qrad[i, mth];

                            // Program.DB.querySQL(DB.type.ProjDB, "UPDATE Zone_Envelope_Result SET QSsink='" + zoneWalls_Qssink[i, mth].ToString() + "', QSsource ='" + zoneWalls_Qssource[i, mth].ToString() + "' where 외피번호 = '" + zonewall.Num() + "'AND 난방_냉방 ='" + HC + "'  AND 비이용일_이용일 ='" + WEWD + "' AND 월 ='" + (mth + 1).ToString() + "월'");
                        }
                        QSopsink_tot_Cmax += zoneWalls_Qssink_Cmax[i];
                        QSopsource_tot_Cmax += zoneWalls_Qssource_Cmax[i];
                    }
                }
            }


            //지붕 일사 계산
            double[,] zoneRoofs_Is = new double[zoneRoof.Count, 12];
            double[,] zoneRoofs_Qssource = new double[zoneRoof.Count, 12];
            double[,] zoneRoofs_Qrad = new double[zoneRoof.Count, 12];
            double[] zoneRoofs_Qssink_Cmax = new double[zoneRoof.Count];
            double[] zoneRoofs_Qssource_Cmax = new double[zoneRoof.Count];

            {
                int i = -1;
                while (++i < zoneRoof.Count)
                {
                    Roof zoneroof = (Roof)zoneRoof[i];
                    int roof_Degree = (int)Math.Round(Program.UTIL.ToDoubleOrZero(zoneroof.Degree()));
                    int roof_Direction = zoneroof.Direction_angle();
                    double[] roof_itot = CALC.Itot_mth.GetValueOrDefault((roof_Degree, roof_Direction));
                    string[][] token2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_부하", "일사량", "지역명 ='" + Location[0][0] + "' AND 방향 ='" + zoneroof.Direction() + "'");
                    if (zoneroof.DiIndi() != "간접외기") //직접외기 지붕만 일사 계산
                    {
                        for (int mth = 0; mth <= 11; mth++)
                        {
                            QSopCalc qsopcalc = new QSopCalc();
                            QS_rad qs_rad = new QS_rad();
                            QTCalc qtcalc = new QTCalc();

                            zoneRoofs_Qrad[i, mth] = qs_rad.Calc(zoneroof.Ueff(), zoneroof.Area(), 1, dmth[mth]);

                            if (roof_itot != null) // 요구량
                            {
                                zoneRoofs_Is[i, mth] = roof_itot[mth];
                                zoneRoofs_Qssource[i, mth] = qsopcalc.Calc(zoneroof.Ueff(), zoneroof.Area(), zoneroof.α(), zoneRoofs_Is[i, mth], dmth[mth]);
                            }

                            if (token2.Length > 0) // 부하
                            {
                                if (1 * 4.5 * 10 >= zoneroof.α() * Program.UTIL.ToDoubleOrZero(token2[0][0]))
                                {
                                    zoneRoofs_Qssink_Cmax[i] = qsopcalc.Calc_max(zoneroof.Ueff(), zoneroof.Area(), zoneroof.α(), Program.UTIL.ToDoubleOrZero(token2[0][0]), 1);
                                }
                                else
                                {
                                    zoneRoofs_Qssource_Cmax[i] = qsopcalc.Calc_max(zoneroof.Ueff(), zoneroof.Area(), zoneroof.α(), Program.UTIL.ToDoubleOrZero(token2[0][0]), 1);
                                }
                            }

                            QSopsource_Roof[mth] += zoneRoofs_Qssource[i, mth];
                            QS_rad_Roof[mth] += zoneRoofs_Qrad[i, mth];

                            //  Program.DB.querySQL(DB.type.ProjDB, "UPDATE Zone_Envelope_Result SET QSsink='" + zoneRoofs_Qssink[i, mth].ToString() + "', QSsource ='" + zoneRoofs_Qssource[i, mth].ToString() + "' where 외피번호 = '" + zoneroof.Num() + "'AND 난방_냉방 ='" + HC + "'  AND 비이용일_이용일 ='" + WEWD + "' AND 월 ='" + (mth + 1).ToString() + "월'");
                        }

                        QSopsink_tot_Cmax += zoneRoofs_Qssink_Cmax[i];
                        QSopsource_tot_Cmax += zoneRoofs_Qssource_Cmax[i];
                    }
                }
            }


            //출입문 일사 계산
            double[,] zoneDoors_Is = new double[zoneDoor.Count, 12];
            double[,] zoneDoors_Qssource = new double[zoneDoor.Count, 12];
            double[,] zoneDoors_Qrad = new double[zoneDoor.Count, 12];
            double[] zoneDoors_Qssink_Cmax = new double[zoneDoor.Count];
            double[] zoneDoors_Qssource_Cmax = new double[zoneDoor.Count];
            {
                int i = -1;
                while (++i < zoneDoor.Count)
                {
                    Door zonedoor = (Door)zoneDoor[i];
                    int door_Degree = (int)Math.Round(Program.UTIL.ToDoubleOrZero(zonedoor.Degree()));
                    int door_Direction = zonedoor.Direction_angle();
                    double[] door_itot = CALC.Itot_mth.GetValueOrDefault((door_Degree, door_Direction));
                    string[][] token2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_부하", "일사량", "지역명 ='" + Location[0][0] + "' AND 방향 ='" + zonedoor.Direction() + "'");
                    if (zonedoor.DiIndi() != "간접외기") //직접외기 벽만 일사 계산
                    {
                        for (int mth = 0; mth < 12; mth++)
                        {
                            QSopCalc qsopcalc = new QSopCalc();
                            QS_rad qs_rad = new QS_rad();
                            QTCalc qtcalc = new QTCalc();

                            zoneDoors_Qrad[i, mth] = qs_rad.Calc(zonedoor.Ueff(), zonedoor.Area(), 0.5, dmth[mth]);

                            if (door_itot != null) // 요구량
                            {
                                zoneDoors_Is[i, mth] = door_itot[mth];
                                zoneDoors_Qssource[i, mth] = qsopcalc.Calc(zonedoor.Ueff(), zonedoor.Area(), zonedoor.α(), zoneDoors_Is[i, mth], dmth[mth]);
                            }

                            if (token2.Length > 0) // 부하
                            {
                                if (0.5 * 4.5 * 10 >= zonedoor.α() * Program.UTIL.ToDoubleOrZero(token2[0][0]))
                                {
                                    zoneDoors_Qssink_Cmax[i] = qsopcalc.Calc_max(zonedoor.Ueff(), zonedoor.Area(), zonedoor.α(), Program.UTIL.ToDoubleOrZero(token2[0][0]), 0.5);
                                }
                                else
                                {
                                    zoneDoors_Qssource_Cmax[i] = qsopcalc.Calc_max(zonedoor.Ueff(), zonedoor.Area(), zonedoor.α(), Program.UTIL.ToDoubleOrZero(token2[0][0]), 0.5);
                                }
                            }

                            QSopsource_Door[mth] += zoneDoors_Qssource[i, mth];
                            QS_rad_Door[mth] += zoneDoors_Qrad[i, mth];
                            //   Program.DB.querySQL(DB.type.ProjDB, "UPDATE Zone_Envelope_Result SET QSsink='" + zoneDoors_Qssink[i, mth].ToString() + "', QSsource ='" + zoneDoors_Qssource[i, mth].ToString() + "' where 외피번호 = '" + zonedoor.Num() + "'AND 난방_냉방 ='" + HC + "'  AND 비이용일_이용일 ='" + WEWD + "' AND 월 ='" + (mth + 1).ToString() + "월'");
                        }
                        QSopsink_tot_Cmax += zoneDoors_Qssink_Cmax[i];
                        QSopsource_tot_Cmax += zoneDoors_Qssource_Cmax[i];
                    }
                }
            }

            //커튼월 패널 일사 계산
            double[,] zoneCWs_Is = new double[zoneCW.Count, 12];
            double[,] zoneCWs_Qssource = new double[zoneCW.Count, 12];
            double[,] zoneCWs_Qrad = new double[zoneCW.Count, 12];
            double[] zoneCWs_Qssink_Cmax = new double[zoneCW.Count];
            double[] zoneCWs_Qssource_Cmax = new double[zoneCW.Count];
            {
                int i = -1;
                while (++i < zoneCW.Count)
                {
                    CW zonecw = (CW)zoneCW[i];
                    if (zonecw.CWType() == "패널부분")
                    {
                        int cwp_Degree = (int)Math.Round(Program.UTIL.ToDoubleOrZero(zonecw.Degree()));
                        int cwp_Direction = zonecw.Direction_angle();
                        double[] cwp_itot = CALC.Itot_mth.GetValueOrDefault((cwp_Degree, cwp_Direction));
                        string[][] token2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_부하", "일사량", "지역명 ='" + Location[0][0] + "' AND 방향 ='" + zonecw.Direction() + "'");
                        for (int mth = 0; mth <= 11; mth++)
                        {
                            QSopCalc qsopcalc = new QSopCalc();
                            QS_rad qs_rad = new QS_rad();
                            QTCalc qtcalc = new QTCalc();

                            zoneCWs_Qrad[i, mth] = qs_rad.Calc(zonecw.Uvalue_p(), zonecw.Area_p(), 0.5, dmth[mth]);

                            if (cwp_itot != null) // 요구량
                            {
                                zoneCWs_Is[i, mth] = cwp_itot[mth];
                                zoneCWs_Qssource[i, mth] = qsopcalc.Calc(zonecw.Uvalue_p(), zonecw.Area_p(), zonecw.α_p(), zoneCWs_Is[i, mth], dmth[mth]);
                            }

                            if (token2.Length > 0) // 부하
                            {
                                if (0.5 * 4.5 * 10 >= zonecw.α_p() * Program.UTIL.ToDoubleOrZero(token2[0][0]))
                                {
                                    zoneCWs_Qssink_Cmax[i] = qsopcalc.Calc_max(zonecw.Uvalue_p(), zonecw.Area_p(), zonecw.α_p(), Program.UTIL.ToDoubleOrZero(token2[0][0]), 0.5);
                                }
                                else
                                {
                                    zoneCWs_Qssource_Cmax[i] = qsopcalc.Calc_max(zonecw.Uvalue_p(), zonecw.Area_p(), zonecw.α_p(), Program.UTIL.ToDoubleOrZero(token2[0][0]), 0.5);
                                }
                            }
                            QSopsource_CW_p[mth] += zoneCWs_Qssource[i, mth];
                            QS_rad_CW_p[mth] += zoneCWs_Qrad[i, mth];

                            //   Program.DB.querySQL(DB.type.ProjDB, "UPDATE Zone_Envelope_Result SET QSsink='" + zoneCWs_Qssink[i, mth].ToString() + "', QSsource ='" + zoneCWs_Qssource[i, mth].ToString() + "' where 외피번호 = '" + zonecw.Num() + "'AND 난방_냉방 ='" + HC + "'  AND 비이용일_이용일 ='" + WEWD + "' AND 월 ='" + (mth + 1).ToString() + "월' AND 커튼월유형 ='패널부분'");
                        }

                        QSopsink_tot_Cmax += zoneCWs_Qssink_Cmax[i];
                        QSopsource_tot_Cmax += zoneCWs_Qssource_Cmax[i];
                    }
                }

            }

            //불투명일사 합계 계산
            for (int hc = 0; hc <= 1; hc++)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    QS_rad_tot[hc, mth] = QS_rad_Wall[mth] + QS_rad_Roof[mth] + QS_rad_Door[mth] + QS_rad_CW_p[mth];
                    QSopsource_tot[hc, mth] = QSopsource_Wall[mth] + QSopsource_Roof[mth] + QSopsource_Door[mth] + QSopsource_CW_p[mth];
                }
            }
        }

        public void ZoneQStr_Win() //창호 일사 계산
        {
            double[,] zoneWins_Is = new double[zoneWin.Count, 12];
            double[] zoneWins_Is_max = new double[zoneWin.Count];
            double[,] zoneWins_Fs = new double[zoneWin.Count, 12];
            double[,] zoneWins_a = new double[zoneWin.Count, 12];
            double[,] zoneWins_geff = new double[zoneWin.Count, 12];
            double[,] zoneWins_Qs = new double[zoneWin.Count, 12];
            double[,] zoneWins_Qrad = new double[zoneWin.Count, 12];
            double[] zoneWins_geff_max = new double[zoneWin.Count];
            double[] zoneWins_Qs_max = new double[zoneWin.Count];
            String[] HC = { "난방", "냉방" };

            //존의 창별 일사정보 가져오기
            int i = -1;

            while (++i < zoneWin.Count)
            {
                Window zonewin = (Window)zoneWin[i];
                int win_Degree = (int)Math.Round(Program.UTIL.ToDoubleOrZero(zonewin.Degree()));
                int win_Direction = zonewin.Direction_angle();
                double[] win_itot = CALC.Itot_mth.GetValueOrDefault((win_Degree, win_Direction));
                string[][] token2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_부하", "일사량", "지역명 ='" + Location[0][0] + "' AND 방향 ='" + zonewin.Direction() + "'");

                if (win_itot != null) // 요구량
                {
                    for (int mth = 0; mth < 12; mth++)
                    {
                        zoneWins_Is[i, mth] = win_itot[mth];
                    }
                }
                if (token2.Length > 0) // 부하 — 요구량과 별개로 독립 판단
                {
                    zoneWins_Is_max[i] = Program.UTIL.ToDoubleOrZero(token2[0][0]);
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
                    { zoneWins_Fs[i, mth] = Program.UTIL.ToDoubleOrZero(Shade[0][0]); }
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
                        { zoneWins_a[i, mth] = Program.UTIL.ToDoubleOrZero(Blind_a[0][0]); }
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
                    Window zonewin = (Window)zoneWin[i];
                    for (int mth = 0; mth < 12; mth++)
                    {
                        GeffCalc geffcalc = new GeffCalc();
                        QStrCalc qstrcalc = new QStrCalc();
                        QS_rad qs_rad = new QS_rad();
                        zoneWins_geff[i, mth] = geffcalc.Calc(zonewin.g(), zoneWins_Fs[i, mth]);
                        zoneWins_geff[i, mth] = geffcalc.Calc(zonewin.g(), zoneWins_Fs[i, mth], zonewin.gtot(), zoneWins_a[i, mth]);
                        zoneWins_geff_max[i] = geffcalc.Calc(zonewin.g(), 1);
                        if (zonewin.DiIndi() == "간접외기")
                        {   //직접외기 창만 일사 계산      

                        }
                        else
                        {

                            zoneWins_Qs[i, mth] = qstrcalc.Calc(zonewin.Ff(), zonewin.Area(), zoneWins_geff[i, mth], zoneWins_Is[i, mth], dmth[mth]);
                            zoneWins_Qrad[i, mth] = qs_rad.Calc(zonewin.Uvalue(), zonewin.Area(), 0.5, dmth[mth]);

                            zoneWins_Qs_max[i] = qstrcalc.Calc_max(zonewin.Ff(), zonewin.Area(), zoneWins_geff_max[i], zoneWins_Is_max[i]);

                        }

                        //  Program.DB.querySQL(DB.type.ProjDB, "UPDATE Zone_Envelope_Result SET QSsink='" + 0.ToString() + "', QSsource ='" + zoneWins_Qs[i, wewd, mth].ToString() + "' where 외피번호 = '" + zonewin.Num() + "'AND 난방_냉방 ='" + HC + "'  AND 비이용일_이용일 ='" + WEWD + "' AND 월 ='" + (mth + 1).ToString() + "월'");
                    }
                    for (int mth = 0; mth < 12; mth++)
                    {
                        QStr_Win[0, mth] += zoneWins_Qs[i, mth];
                        QStr_Win[1, mth] += zoneWins_Qs[i, mth];
                        QS_rad_Win[0, mth] += zoneWins_Qrad[i, mth];
                        QS_rad_Win[1, mth] += zoneWins_Qrad[i, mth];
                    }
                    QStr_Win_max += zoneWins_Qs_max[i];
                }
            }
        }
        public void ZoneQStr_CW_own() //투명구조체 일사 계산 (자기 몫만 — 인접 ZTU 배분 전, 웜업 패스에서 실행)
        {
            double[,] zoneCWs_Is = new double[zoneCW.Count, 12];
            double[,] zoneCWs_Fs = new double[zoneCW.Count, 12];
            double[,] zoneCWs_a = new double[zoneCW.Count, 12];
            double[,] zoneCWs_g_geff = new double[zoneCW.Count, 12];
            double[,] zoneCWs_d_geff = new double[zoneCW.Count, 12];
            double[,] zoneCWs_g_Qs = new double[zoneCW.Count, 12];
            double[,] zoneCWs_d_Qs = new double[zoneCW.Count, 12];
            double[,] zoneCWs_g_Qrad = new double[zoneCW.Count, 12];
            double[,] zoneCWs_d_Qrad = new double[zoneCW.Count, 12];

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
                int cwg_Degree = (int)Math.Round(Program.UTIL.ToDoubleOrZero(zonecw.Degree()));
                int cwg_Direction = zonecw.Direction_angle();
                double[] cwg_itot = CALC.Itot_mth.GetValueOrDefault((cwg_Degree, cwg_Direction));
                string[][] token2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_부하", "일사량", "지역명 ='" + Location[0][0] + "' AND 방향 ='" + zonecw.Direction() + "'");

                if (cwg_itot != null) // 요구량
                {
                    for (int mth = 0; mth < 12; mth++)
                    {
                        zoneCWs_Is[i, mth] = cwg_itot[mth];
                    }
                }
                if (token2.Length > 0) // 부하 — 요구량과 별개로 독립 판단
                {
                    zoneCWs_Is_max[i] = Program.UTIL.ToDoubleOrZero(token2[0][0]);
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
                        { zoneCWs_Fs[i, mth] = Program.UTIL.ToDoubleOrZero(Shade[0][0]); }
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
                    if (BlindValue.Length > 0)
                    {
                        for (int mth = 0; mth < 12; mth++)
                        {
                            if (BlindValue.Length > 0)
                            {
                                string[][] Blind_a = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_차양가동계수_" + BlindValue[0][0], "계수", "지역명= '" + Location[0][0] + "' And 방향 ='" + zonecw.Direction() + "' And 기간 = '" + (mth + 1).ToString() + "월'");
                                if (Blind_a.Length > 0)
                                { zoneCWs_a[i, mth] = Program.UTIL.ToDoubleOrZero(Blind_a[0][0]); }
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

                for (int mth = 0; mth < 12; mth++)
                {
                    GeffCalc geffcalc = new GeffCalc();
                    QStrCalc qstrcalc = new QStrCalc();
                    QS_rad qs_rad = new QS_rad();
                    QTCalc qtcalc = new QTCalc();
                    zoneCWs_g_geff[i, mth] = geffcalc.Calc(zonecw.g_g(), zoneCWs_Fs[i, mth]);    //이용일 차양없을 경우	
                    zoneCWs_g_geff[i, mth] = geffcalc.Calc(zonecw.g_g(), zoneCWs_Fs[i, mth], zonecw.gtot_g(), zoneCWs_a[i, mth]);     //이용일 차양있을 경우
                    zoneCWs_d_geff[i, mth] = geffcalc.Calc(zonecw.g_d(), zoneCWs_Fs[i, mth]); //출입문

                    zoneCWs_g_geff_max[i] = geffcalc.Calc(zonecw.g_g(), 1);    //부하   
                    zoneCWs_d_geff_max[i] = geffcalc.Calc(zonecw.g_d(), 1);    //부하   

                    zoneCWs_g_Qs[i, mth] = qstrcalc.Calc(zonecw.Ff_g(), zonecw.Area_g(), zoneCWs_g_geff[i, mth], zoneCWs_Is[i, mth], dmth[mth]);
                    zoneCWs_d_Qs[i, mth] = qstrcalc.Calc(zonecw.Ff_d(), zonecw.Area_d(), zoneCWs_d_geff[i, mth], zoneCWs_Is[i, mth], dmth[mth]);
                    zoneCWs_g_Qrad[i, mth] = qs_rad.Calc(zonecw.Uvalue_g(), zonecw.Area_g(), 0.5, dmth[mth]);
                    zoneCWs_d_Qrad[i, mth] = qs_rad.Calc(zonecw.Uvalue_d(), zonecw.Area_d(), 0.5, dmth[mth]);
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
                for (int mth = 0; mth < 12; mth++)
                {
                    QStr_CW[0, mth] += (zoneCWs_g_Qs[i, mth] + zoneCWs_d_Qs[i, mth]);
                    QStr_CW[1, mth] += (zoneCWs_g_Qs[i, mth] + zoneCWs_d_Qs[i, mth]);
                    QS_rad_CW[0, mth] += (zoneCWs_g_Qrad[i, mth] + zoneCWs_d_Qrad[i, mth]);
                    QS_rad_CW[1, mth] += (zoneCWs_g_Qrad[i, mth] + zoneCWs_d_Qrad[i, mth]);
                }
                QStr_CW_max += (zoneCWs_g_Qs_max[i] + zoneCWs_d_Qs_max[i]);
            }

            for (int hc = 0; hc <= 1; hc++)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    QStr_own[hc, mth] = QStr_Win[hc, mth] + QStr_CW[hc, mth];
                }
            }


            QStr_tot_Cmax = QStr_Win_max + QStr_CW_max;
        }

        // 인접 ZTU 배분(Qstr_ztu, 인접존의 QStr_own을 읽음) + 최종 QStr_tot 조립 — 본계산 패스에서 실행.
        // 모든 존의 QStr_own이 웜업에서 이미 준비돼 있어야 안전.
        // QS_rad_tot도 여기서 조립 — ZoneQSop()(불투명 일사, QS_rad_tot을 Wall+Roof+Door+CW_p로 덮어씀)이
        // 이 함수보다 먼저(Zone_Calc() 순서상) 실행된 뒤라야 Win/CW분을 안전하게 더할 수 있음.
        public void ZoneQStr_CW_finalize()
        {
            double[,] Qstr_Ztu = Qstr_ztu();
            for (int hc = 0; hc <= 1; hc++)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    QStr_tot[hc, mth] = QStr_own[hc, mth] + Qstr_Ztu[hc, mth];
                    QS_rad_tot[hc, mth] = QS_rad_tot[hc, mth] + QS_rad_Win[hc, mth] + QS_rad_CW[hc, mth];
                }
            }
        }



        public double[,] Qstr_ztu()
        {
            double[,] Qs_Inwall_ztu = new double[2, 12];
            double[,] Qs_Slab_ztu = new double[2, 12];
            double[,] Qs_ztu = new double[2, 12];

            ArrayList processedZTU_InWall_Qs = new ArrayList(); // 이 존이 이미 계산한 인접 ZTU존 번호 목록 (세그먼트 중복 방지)
            for (int i = 0; i < zoneInWall.Count; i++)
            {
                InWall zoneInwall = (InWall)zoneInWall[i]; //List를 class 객체로 변환

                Zone zoneZTU = Program.CALC.getZone(zoneInwall.SideZone());

                if (!processedZTU_InWall_Qs.Contains(zoneInwall.SideZone()))
                {
                    processedZTU_InWall_Qs.Add(zoneInwall.SideZone());

                    // 이 ZTU존에서 이 존(this)으로 연결된 세그먼트들의 배분계수는 합산(sum)해서 사용
                    double f_total = 0;
                    for (int a = 0; a < zoneZTU.zoneInWall.Count; a++)
                    {
                        InWall ztuInwall = (InWall)zoneZTU.zoneInWall[a];
                        if (ztuInwall.SideZone() == this.ZoneNum)
                        {
                            f_total += ztuInwall.f_ztc_ztu();
                        }
                    }

                    for (int hc = 0; hc < 2; hc++)
                    {
                        bool isAdjacentZTU = IsAdjacentZTU(zoneZTU, hc);

                        for (int mth = 0; mth < 12; mth++)
                        {
                            // ZTU존당 1회만 반영
                            double contribution = isAdjacentZTU
                                ? (1 - zoneZTU.b_ztu[hc]) * zoneZTU.QStr_own[hc, mth] * f_total
                                : 0;

                            Qs_Inwall_ztu[hc, mth] += contribution;
                        }
                    }
                }
            }

            ArrayList processedZTU_Slab_Qs = new ArrayList(); // 이 존이 이미 계산한 인접 ZTU존 번호 목록 (세그먼트 중복 방지)
            for (int i = 0; i < zoneSlab.Count; i++)
            {
                Slab zoneslab = (Slab)zoneSlab[i];//List를 class 객체로 변환

                Zone zoneZTU = Program.CALC.getZone(zoneslab.SideZone());

                if (!processedZTU_Slab_Qs.Contains(zoneslab.SideZone()))
                {
                    processedZTU_Slab_Qs.Add(zoneslab.SideZone());

                    // 이 ZTU존에서 이 존(this)으로 연결된 세그먼트들의 배분계수는 합산(sum)해서 사용
                    double f_total = 0;
                    for (int a = 0; a < zoneZTU.zoneSlab.Count; a++)
                    {
                        Slab ztuSlab = (Slab)zoneZTU.zoneSlab[a];
                        if (ztuSlab.SideZone() == this.ZoneNum)
                        {
                            f_total += ztuSlab.f_ztc_ztu();
                        }
                    }

                    for (int hc = 0; hc < 2; hc++)
                    {
                        bool isAdjacentZTU = IsAdjacentZTU(zoneZTU, hc);

                        for (int mth = 0; mth < 12; mth++)
                        {
                            // ZTU존당 1회만 반영
                            double contribution = isAdjacentZTU
                                ? (1 - zoneZTU.b_ztu[hc]) * zoneZTU.QStr_own[hc, mth] * f_total
                                : 0;

                            Qs_Slab_ztu[hc, mth] += contribution;
                        }
                    }
                }
            }

            for (int hc = 0; hc < 2; hc++)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    Qs_ztu[hc, mth] = Qs_Inwall_ztu[hc, mth] + Qs_Slab_ztu[hc, mth];
                }
            }

            return Qs_ztu;
        }



        public void ZoneQI_L() //조명내부발열 계산
        {
            ZoneLight zonelight1 = Program.CALC.getZoneLight(ZoneNum.ToString());
            for (int hc = 0; hc <= 1; hc++)
            {
                for (int mth = 0; mth <= 11; mth++)
                {
                    QI_L[hc, mth] = zonelight1.Zone_Final_kWh[mth];

                }
            }
        }

        public void ZoneQI_own() //내부발열 계산 (자기 몫만 — 인접 ZTU 배분 전, 웜업 패스에서 실행)
        {
            double t_person = 0;
            string[][] value = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "사람일일이용시간", "용도명='" + zoneUsage + "'");
            if (value.Length > 0)
            {
                t_person = Program.UTIL.ToDoubleOrZero(value[0][0]);
            }
            for (int mth = 0; mth < 12; mth++)
            {

                QI_P[mth] = qI_p * zoneArea * dmth[mth] / 1000;
                QI_fac[mth] = qI_fac * zoneArea * dmth[mth] / 1000;
            }

            //이용일

            double[] h_summer = new double[12];
            for (int mth = 0; mth < 12; mth++)
            {
                h_summer[mth] = (H_winter - H_summer) / (theta_e[2] - theta_e[5]) * (theta_e[mth] - theta_e[2]) + H_winter;
            }

            for (int hc = 0; hc <= 1; hc++)
            {
                for (int mth = 0; mth <= 11; mth++)
                {
                  // QI_Humidity[mth] = t_person * h_summer[mth] * Peope_Num * 2260 / 3600 * dmth[mth] / 1000;

                    if (hc == 1)
                    {
                        QI_own[hc, mth] = QI_P[mth] + QI_fac[mth] + QI_L[hc, mth] + QI_Humidity[mth];
                    }
                    else { QI_own[hc, mth] = QI_P[mth] + QI_fac[mth] + QI_L[hc, mth]; }


                }
            }
        }

        // 인접 ZTU 배분(QI_ztu, 인접존의 QI_own을 읽음) + 최종 QI_tot 조립 — 본계산 패스에서 실행.
        // 모든 존의 QI_own이 웜업에서 이미 준비돼 있어야 안전.
        public void ZoneQI_finalize()
        {
            double[,] QI_Ztu = QI_ztu();

            for (int hc = 0; hc <= 1; hc++)
            {
                for (int mth = 0; mth <= 11; mth++)
                {
                    QI_tot[hc, mth] = QI_own[hc, mth] + QI_Ztu[hc, mth];
                }
            }
        }
        public double[,] QI_ztu()
        {
            double[,] QI_Inwall_ztu = new double[2, 12];
            double[,] QI_Slab_ztu = new double[2, 12];
            double[,] QI_ztu = new double[2, 12];

            ArrayList processedZTU_InWall_QI = new ArrayList(); // 이 존이 이미 계산한 인접 ZTU존 번호 목록 (세그먼트 중복 방지)
            for (int i = 0; i < zoneInWall.Count; i++)
            {
                InWall zoneInwall = (InWall)zoneInWall[i]; //List를 class 객체로 변환

                Zone zoneZTU = Program.CALC.getZone(zoneInwall.SideZone());

                if (!processedZTU_InWall_QI.Contains(zoneInwall.SideZone()))
                {
                    processedZTU_InWall_QI.Add(zoneInwall.SideZone());

                    // 이 ZTU존에서 이 존(this)으로 연결된 세그먼트들의 배분계수는 합산(sum)해서 사용
                    double f_total = 0;
                    for (int a = 0; a < zoneZTU.zoneInWall.Count; a++)
                    {
                        InWall ztuInwall = (InWall)zoneZTU.zoneInWall[a];
                        if (ztuInwall.SideZone() == this.ZoneNum)
                        {
                            f_total += ztuInwall.f_ztc_ztu();
                        }
                    }

                    for (int hc = 0; hc < 2; hc++)
                    {
                        bool isAdjacentZTU = IsAdjacentZTU(zoneZTU, hc);

                        for (int mth = 0; mth < 12; mth++)
                        {
                            // ZTU존당 1회만 반영
                            double contribution = isAdjacentZTU
                                ? (1 - zoneZTU.b_ztu[hc]) * zoneZTU.QI_own[hc, mth] * f_total
                                : 0;

                            QI_Inwall_ztu[hc, mth] += contribution;
                        }
                    }
                }
            }

            ArrayList processedZTU_Slab_QI = new ArrayList(); // 이 존이 이미 계산한 인접 ZTU존 번호 목록 (세그먼트 중복 방지)
            for (int i = 0; i < zoneSlab.Count; i++)
            {
                Slab zoneslab = (Slab)zoneSlab[i];//List를 class 객체로 변환

                Zone zoneZTU = Program.CALC.getZone(zoneslab.SideZone());

                if (!processedZTU_Slab_QI.Contains(zoneslab.SideZone()))
                {
                    processedZTU_Slab_QI.Add(zoneslab.SideZone());

                    // 이 ZTU존에서 이 존(this)으로 연결된 세그먼트들의 배분계수는 합산(sum)해서 사용
                    double f_total = 0;
                    for (int a = 0; a < zoneZTU.zoneSlab.Count; a++)
                    {
                        Slab ztuSlab = (Slab)zoneZTU.zoneSlab[a];
                        if (ztuSlab.SideZone() == this.ZoneNum)
                        {
                            f_total += ztuSlab.f_ztc_ztu();
                        }
                    }

                    for (int hc = 0; hc < 2; hc++)
                    {
                        bool isAdjacentZTU = IsAdjacentZTU(zoneZTU, hc);

                        for (int mth = 0; mth < 12; mth++)
                        {
                            // ZTU존당 1회만 반영
                            double contribution = isAdjacentZTU
                                ? (1 - zoneZTU.b_ztu[hc]) * zoneZTU.QI_own[hc, mth] * f_total
                                : 0;

                            QI_Slab_ztu[hc, mth] += contribution;
                        }
                    }
                }
            }

            for (int hc = 0; hc < 2; hc++)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    QI_ztu[hc, mth] = QI_Inwall_ztu[hc, mth] + QI_Slab_ztu[hc, mth];
                }
            }

            return QI_ztu;
        }

        public void Zonetao()//시간상수 계산
        {
            for (int hc = 0; hc < 2; hc++)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    Zone_H_tot[hc, mth] = Zone_HT_tot[hc] + Zone_HV_tot[hc, mth];
                    theta_iCalc calc = new theta_iCalc();
                    tao[hc, mth] = calc.tao_Calc(Cwirk_A * zoneArea, Zone_H_tot[hc, mth]);
                    tao_max = calc.tao_Calc(Cwirk_A * zoneArea, (Zone_HT_tot[hc] + HV_tot_max));
                }
            }
        }



        public void ZoneGamma1()//열획득손실비
        {
            eta_Calc eta_calc = new eta_Calc();
            for (int hc = 0; hc <= 1; hc++)
            {
                for (int mth = 0; mth <= 11; mth++)
                {
                    Qsink[hc, mth] = QTsink_tot[hc, mth] + QVsink_tot[hc, mth];
                    Qsource[hc, mth] = (QSopsource_tot[hc, mth] + QStr_tot[hc, mth] - QS_rad_tot[hc, mth]) + QI_tot[hc, mth];

                    gamma[hc, mth] = Qsource[hc, mth] / Qsink[hc, mth];
                }
            }
        }

        public void Zonethetai()//실내기준온도 계산
        {
            theta_iCalc calc = new theta_iCalc();
            double[] night_setback = new double[12];
            double[] daytime_setback = new double[12];
            double[] weekend_setback = new double[12];

            double t_day =0, n_day =0, t_night = 0, n_night = 0, t_we = 0, n_we = 0;
            if (Mode_night != "지속운전")
            {
                t_night = 24 - twd_d;
                if (Mode_we != "지속운전")
                {
                    n_night = n_Weekday;
                    t_we = 24;
                    n_we = 7 - n_Weekday;
                }
                else
                {
                    n_night = 7;
                }
            }

            for (int mth = 0; mth < 12; mth++)
            {

                //[hc, wewd, mth]	
                daytime_setback[mth] = calc.Setback(gamma[0, mth], theta_e[mth], theta_i_set[0], tao[0, mth], t_day, n_day, dtheta_i_NA, "지속운전");
                night_setback[mth] = calc.Setback(gamma[0, mth], theta_e[mth], theta_i_set[0], tao[0, mth], t_night, n_night, dtheta_i_NA, Mode_night);
                weekend_setback[mth] = calc.Setback(gamma[0, mth], theta_e[mth], theta_i_set[0], tao[0, mth], t_we, n_we, dtheta_i_NA, Mode_we);

                double[] a_H_red = new double[12];
                a_H_red[mth] = 1 - (1 - night_setback[mth]) - (1 - daytime_setback[mth]) - (1 - weekend_setback[mth]);

                theta_i[0, mth] = a_H_red[mth] * (theta_i_set[0] - theta_e[mth]) + theta_e[mth];
                theta_i[1, mth] = theta_i_set[1];
            }
        }
        public void ZoneQT2()//관류 열전달 계산
        {
            for (int hc = 0; hc < 2; hc++)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    QTsink_TB[hc, mth] = 0;
                }
            }

            QTCalc qtcalc = new QTCalc();
            {
                //외벽 QT계산
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {
                        QTsink_Wall[hc, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i[hc, mth], Zone_HT_Wall, dmth[mth]);
                        QTsink_TB[hc, mth] += qtcalc.Calc_sink(theta_e[mth], theta_i[hc, mth], Zone_HT_TB_Wall, dmth[mth]);
                    }
                }

                QTsink_Wall_max = (Zone_HT_Wall * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_Wall_Cmax = (Zone_HT_Wall * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_Wall_Cmax = (Zone_HT_Wall * (theta_e_max - theta_i_c_max_d)); }

                QTsink_TB_max += (Zone_HT_TB_Wall * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_TB_Cmax += (Zone_HT_TB_Wall * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_TB_Cmax += (Zone_HT_TB_Wall * (theta_e_max - theta_i_c_max_d)); }

            }

            {
                //지붕 QT계산
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {
                        QTsink_Roof[hc, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i[hc, mth], Zone_HT_Roof, dmth[mth]);
                        QTsink_TB[hc, mth] += qtcalc.Calc_sink(theta_e[mth], theta_i[hc, mth], Zone_HT_TB_Roof, dmth[mth]);
                    }
                }

                QTsink_Roof_max = (Zone_HT_Roof * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_Roof_Cmax = (Zone_HT_Roof * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_Roof_Cmax = (Zone_HT_Roof * (theta_e_max - theta_i_c_max_d)); }

                QTsink_TB_max += (Zone_HT_TB_Roof * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_TB_Cmax += (Zone_HT_TB_Roof * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_TB_Cmax += (Zone_HT_TB_Roof * (theta_e_max - theta_i_c_max_d)); }

            }



            //바닥 QT계산
            double[,,] zoneFloors_QTsink = new double[zoneFloor.Count, 2, 12];
            double[,,] zoneFloors_QTsource = new double[zoneFloor.Count, 2, 12];
            double[,,] zoneFloors_QTsink_TB = new double[zoneFloor.Count, 2, 12];
            double[,,] zoneFloors_QTsource_TB = new double[zoneFloor.Count, 2, 12];
            int i = -1;
            for (int hc = 0; hc < 2; hc++)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    QTsink_Floor[hc, mth] = 0;
                    QTsink_GWall[hc, mth] = 0;
                }
            }
            while (++i < zoneFloor.Count)
            {
                Floor zonefloor = (Floor)zoneFloor[i];
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {

                        // theta_s 대신 theta_e 기준 + Fx 가중 H (ZoneQT()와 동일한 이유로 수학적으로 동일 —
                        // 기존엔 theta_s 자체는 theta_i_set으로 계산해놓고 바깥 Ti는 theta_i(동적)를 써서
                        // 서로 안 맞았는데, 이 방식으로 바꾸면서 그 불일치도 같이 해소됨)
                        zoneFloors_QTsink[i, hc, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i[hc, mth], zonefloor.Fx() * zonefloor.Ueff() * zonefloor.Area(), dmth[mth]);
                        zoneFloors_QTsink_TB[i, hc, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i[hc, mth], Utb[2] * zonefloor.Area(), dmth[mth]);


                        QTsink_Floor[hc, mth] += zoneFloors_QTsink[i, hc, mth];
                        //QTsource_Floor[hc, wewd, mth] += zoneFloors_QTsource[i, hc, wewd, mth];
                        QTsink_TB[hc, mth] += zoneFloors_QTsink_TB[i, hc, mth];
                        //QTsource_TB[hc, wewd, mth] += zoneFloors_QTsource_TB[i, hc, wewd, mth];
                    }
                }

                QTsink_Floor_max += (zonefloor.Ueff() * zonefloor.Area() * (theta_i_h_min - theta_e_min));

                QTsink_TB_max += (Utb[2] * zonefloor.Area() * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_Floor_Cmax += (zonefloor.Ueff() * zonefloor.Area() * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_Floor_Cmax += (zonefloor.Ueff() * zonefloor.Area() * (theta_e_max - theta_i_c_max_d)); }

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_TB_Cmax += (Utb[2] * zonefloor.Area() * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_TB_Cmax += (Utb[2] * zonefloor.Area() * (theta_e_max - theta_i_c_max_d)); }
            }

            //지하벽 QT계산    
            i = -1;
            double[,,] zoneGWalls_QTsink = new double[zoneGWall.Count, 2, 12];
            double[,,] zoneGWalls_QTsource = new double[zoneGWall.Count, 2, 12];
            double[,,] zoneGWalls_QTsink_TB = new double[zoneGWall.Count, 2, 12];
            double[,,] zoneGWalls_QTsource_TB = new double[zoneGWall.Count, 2, 12];
            while (++i < zoneGWall.Count)
            {
                GWall zonegwall = (GWall)zoneGWall[i];
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {

                        // theta_s_GWall 대신 theta_e 기준 + Fx 가중 H (바닥과 동일)
                        zoneGWalls_QTsink[i, hc, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i[hc, mth], zonegwall.Fx() * zonegwall.Ueff() * zonegwall.Area(), dmth[mth]);
                        zoneGWalls_QTsink_TB[i, hc, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i[hc, mth], zonegwall.Fx() * Utb[2] * zonegwall.Area(), dmth[mth]);


                        QTsink_GWall[hc, mth] += zoneGWalls_QTsink[i, hc, mth];
                        //  QTsource_GWall[hc, wewd, mth] += zoneGWalls_QTsource[i, hc, wewd, mth];
                        QTsink_TB[hc, mth] += zoneGWalls_QTsink_TB[i, hc, mth];
                        //QTsource_TB[hc, wewd, mth] += zoneGWalls_QTsource_TB[i, hc, wewd, mth];
                    }
                }

                QTsink_GWall_max += (zonegwall.Ueff() * zonegwall.Area() * (theta_i_h_min - theta_e_min));

                QTsink_TB_max += (Utb[2] * zonegwall.Area() * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_GWall_Cmax += (zonegwall.Ueff() * zonegwall.Area() * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_GWall_Cmax += (zonegwall.Ueff() * zonegwall.Area() * (theta_e_max - theta_i_c_max_d)); }

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_TB_Cmax += (Utb[2] * zonegwall.Area() * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_TB_Cmax += (Utb[2] * zonegwall.Area() * (theta_e_max - theta_i_c_max_d)); }
            }

            {
                //출입문 QT계산
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {
                        QTsink_Door[hc, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i[hc, mth], Zone_HT_Door, dmth[mth]);
                        QTsink_TB[hc, mth] += qtcalc.Calc_sink(theta_e[mth], theta_i[hc, mth], Zone_HT_TB_Door, dmth[mth]); //rce_TB[hc, wewd, mth] = qtcalc.Calc_source(theta_e[mth], theta_i[hc, wewd, mth], Zone_HT_TB_Door);

                    }
                }

                QTsink_Door_max = (Zone_HT_Door * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_Door_Cmax = (Zone_HT_Door * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_Door_Cmax = (Zone_HT_Door * (theta_e_max - theta_i_c_max_d)); }

                QTsink_TB_max += (Zone_HT_TB_Door * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_TB_Cmax += (Zone_HT_TB_Door * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_TB_Cmax += (Zone_HT_TB_Door * (theta_e_max - theta_i_c_max_d)); }

            }
            {
                //커튼월창 QT계산
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {
                        QTsink_CW[hc, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i[hc, mth], Zone_HT_CW, dmth[mth]);
                        QTsink_TB[hc, mth] += qtcalc.Calc_sink(theta_e[mth], theta_i[hc, mth], Zone_HT_TB_CW, dmth[mth]);
                    }
                }

                QTsink_CW_max = (Zone_HT_CW * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_CW_Cmax = (Zone_HT_CW * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_CW_Cmax = (Zone_HT_CW * (theta_e_max - theta_i_c_max_d)); }

                QTsink_TB_max += (Zone_HT_TB_CW * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_TB_Cmax += (Zone_HT_TB_CW * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_TB_Cmax += (Zone_HT_TB_CW * (theta_e_max - theta_i_c_max_d)); }

            }
            {
                //창호 QT계산
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {
                        QTsink_Win[hc, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i[hc, mth], Zone_HT_Win, dmth[mth]);
                        QTsink_TB[hc, mth] += qtcalc.Calc_sink(theta_e[mth], theta_i[hc, mth], Zone_HT_TB_Win, dmth[mth]);
                    }
                }

                QTsink_Win_max = (Zone_HT_Win * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_Win_Cmax = (Zone_HT_Win * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_Win_Cmax = (Zone_HT_Win * (theta_e_max - theta_i_c_max_d)); }

                QTsink_TB_max += (Zone_HT_TB_Win * (theta_i_h_min - theta_e_min));

                if (theta_i_c_max_d > theta_e_max)
                { QTsink_TB_Cmax += (Zone_HT_TB_Win * (theta_i_c_max_d - theta_e_max)); }
                else { QTsource_TB_Cmax += (Zone_HT_TB_Win * (theta_e_max - theta_i_c_max_d)); }

            }
            {
                //내벽 QT계산
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {
                        QTsink_Inwall[hc, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i[hc, mth], Zone_HT_Inwall[hc], dmth[mth]);
                    }
                }
            }
            {
                //층간바닥 QT계산
                for (int hc = 0; hc <= 1; hc++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {
                        QTsink_Slab[hc, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i[hc, mth], Zone_HT_Slab[hc], dmth[mth]);
                    }
                }
            }



            // QT_tot계산
            for (int hc = 0; hc <= 1; hc++)
            {
                for (int mth = 0; mth <= 11; mth++)
                {

                    QTsink_tot[hc, mth] = QTsink_TB[hc, mth] + QTsink_Wall[hc, mth] + QTsink_Roof[hc, mth] + QTsink_Door[hc, mth] + QTsink_Win[hc, mth] + QTsink_CW[hc, mth] + QTsink_Floor[hc, mth] + QTsink_GWall[hc, mth] + QTsink_Inwall[hc, mth] + QTsink_Slab[hc, mth];
                    //  QTsource_tot[hc, wewd, mth] = QTsource_TB[hc, wewd, mth] + QTsource_Wall[hc, wewd, mth] + QTsource_Roof[hc, wewd, mth] + QTsource_Door[hc, wewd, mth] + QTsource_Win[hc, wewd, mth] + QTsource_CW[hc, wewd, mth] + QTsource_Floor[hc, wewd, mth] + QTsource_GWall[hc, wewd, mth] + QTsource_Inwall[hc, wewd, mth] + QTsource_Slab[hc, wewd, mth];

                }
            }
            QTsink_tot_max = QTsink_TB_max + QTsink_Wall_max + QTsink_Roof_max + QTsink_Door_max + QTsink_Win_max + QTsink_CW_max + QTsink_Floor_max + QTsink_GWall_max;
            QTsink_tot_Cmax = QTsink_TB_Cmax + QTsink_Wall_Cmax + QTsink_Roof_Cmax + QTsink_Door_Cmax + QTsink_Win_Cmax + QTsink_CW_Cmax + QTsink_Floor_Cmax + QTsink_GWall_Cmax;
            QTsource_tot_Cmax = QTsource_TB_Cmax + QTsource_Wall_Cmax + QTsource_Roof_Cmax + QTsource_Door_Cmax + QTsource_Win_Cmax + QTsource_CW_Cmax + QTsource_Floor_Cmax + QTsource_GWall_Cmax;
        }
        public void ZoneQV2() //환기 열전달 계산
        {
            HVCalc hvcalc = new HVCalc();
            double V = zoneArea * zoneHeight;
            double H_mech_raw = hvcalc.HV_Calc(nmech, V); // b 적용 전 순수 H(nmech×V×0.34)

            for (int hc = 0; hc <= 1; hc++)
            {
                for (int mth = 0; mth <= 11; mth++)
                {
                    QVCalc qvcalc = new QVCalc();
                    QV_inf_sink[hc, mth] = qvcalc.Calc_sink(theta_e[mth], theta_i[hc, mth], Zone_HV_inf, dmth[mth]);
                    QV_z_sink[hc, mth] = qvcalc.Calc_sink(theta_e[mth], theta_i[hc, mth], Zone_HV_z[hc], dmth[mth]);
                    QV_win_sink[hc, mth] = qvcalc.Calc_sink(theta_e[mth], theta_i[hc, mth], Zone_HV_win, dmth[mth]);

                    // ZoneQV()와 동일한 이유로 θe 기준 + b_mech로 통일 (여기선 Ti가 theta_i[hc,mth]).
                    // theta_v_mech는 ZoneHV()가 미리 채워둔 필드를 재사용(2차 패스라 Zone_HV_mech/
                    // Zone_HV_tot는 1차 패스인 ZoneQV()가 남긴 값을 여기서 덮어씀)
                    double b_mech = (theta_i[hc, mth] - theta_e[mth] == 0) ? 0 : (theta_i[hc, mth] - theta_v_mech[hc, mth]) / (theta_i[hc, mth] - theta_e[mth]);
                    Zone_HV_mech[hc, mth] = H_mech_raw * b_mech;
                    Zone_HV_tot[hc, mth] = Zone_HV_mech[hc, mth] + Zone_HV_z[hc] + Zone_HV_inf + Zone_HV_win;
                    QV_mech_sink[hc, mth] = qvcalc.Calc_sink(theta_e[mth], theta_i[hc, mth], Zone_HV_mech[hc, mth], dmth[mth]);
                    QVsink_tot[hc, mth] = QV_inf_sink[hc, mth] + QV_win_sink[hc, mth] + QV_z_sink[hc, mth] + QV_mech_sink[hc, mth];
                    //QVsource_tot[hc, wewd, mth] = QV_inf_source[hc, wewd, mth] + QV_win_source[hc, wewd, mth] + QV_z_source[hc, wewd, mth] + QV_mech_source[hc, wewd, mth];
                }
            }

            QV_inf_sink_max = Zone_HV_inf * (theta_i_h_min - theta_e_min);
            QV_win_sink_max = Zone_HV_win * (theta_i_h_min - theta_e_min);
            QV_z_sink_max = Zone_HV_z[0] * (theta_i_h_min - theta_i_h_min); // 원래도 (a-a) 형태라 hc 무관하게 항상 0
            QV_mech_sink_max = H_mech_raw * (theta_i_h_min - (theta_e_min + eta_V_mech[1] * (theta_i_h_min - theta_e_min))); // 설계조건 최대치라 b 적용 없이 원래 방식 그대로
            QVsink_tot_max = QV_inf_sink_max + QV_win_sink_max + QV_z_sink_max; //기계환기 제외

            if (theta_i_c_max_d > theta_e_max)
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

        public void Zoneeta()//이용계수 계산
        {
            eta_Calc eta_calc = new eta_Calc();

            for (int hc = 0; hc <= 1; hc++)
            {
                for (int mth = 0; mth <= 11; mth++)
                {
                    Qsink[hc, mth] = QTsink_tot[hc, mth] + QVsink_tot[hc, mth];
                    Qsource[hc, mth] = (QSopsource_tot[hc, mth] + QStr_tot[hc, mth] - QS_rad_tot[hc, mth]) + QI_tot[hc, mth];

                    gamma[hc, mth] = Qsource[hc, mth] / Qsink[hc, mth];

                    a[hc, mth] = 1 + tao[hc, mth] / 15;
                    if (hc == 0)
                    { eta[0, mth] = eta_calc.eta_h_Calc(gamma[0, mth], a[0, mth], Qsource[0, mth]); }
                    else
                    {
                        eta[1, mth] = eta_calc.eta_c_Calc(gamma[1, mth], a[1, mth]);
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



                if (gamma[0, mth] <= 0 && Qsource[0, mth] > 0)
                {
                    Qb_mth[0, mth] = 0;
                }
                else if (gamma[0, mth] > 2)
                {
                    Qb_mth[0, mth] = 0;
                }
                else
                {
                    Qb_mth[0, mth] = qbcalc.Qhb_Calc(Qsink[0, mth], eta[0, mth], Qsource[0, mth]);
                }

                // Qcb_we_day[mth] = qbcalc.Qcb_Calc(eta[1, 0, mth], Qsource[1, 0, mth]);

                if (1 / gamma[1, mth] > 1.5)
                {
                    Qb_mth[1, mth] = 0;
                }
                else
                {
                    // Qb_mth[1, mth] = qbcalc.Qcb_Calc(Qsource[1, mth], eta[1, mth], Qsink[1, mth]) + Q_DHU_tot[mth];
                    Qb_mth[1, mth] = qbcalc.Qcb_Calc(Qsource[1, mth], eta[1, mth], Qsink[1, mth]);
                }



                Qb_mth[0, mth] = double.IsNaN(Qb_mth[0, mth]) || Qb_mth[0, mth] < 0 ? 0 : Qb_mth[0, mth];
                Qb_mth[1, mth] = double.IsNaN(Qb_mth[1, mth]) || Qb_mth[1, mth] < 0 ? 0 : Qb_mth[1, mth];



                Qb_a[0] += Qb_mth[0, mth];
                Qb_a[1] += Qb_mth[1, mth];


                if (zoneHC == "비냉난방")
                {
                    Qb_mth[0, mth] = 0;
                    Qb_mth[1, mth] = 0;
                    Qb_a[0] = 0;
                    Qb_a[1] = 0;
                }
                else if (zoneHC == "냉방")
                {
                    Qb_mth[0, mth] = 0;
                    Qb_a[0] = 0;
                }
                else if (zoneHC == "난방")
                {
                    Qb_mth[1, mth] = 0;
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
                t_person = Program.UTIL.ToDoubleOrZero(value[0][0]);
            }

            Qsource_max = QTsource_tot_Cmax + QVsource_tot_Cmax + QSopsource_tot_Cmax + QStr_tot_Cmax + (qI_p * zoneArea + qI_fac * zoneArea) / t_c_op_d + Peope_Num * H_summer * twd_d * 2260 / 3600 / twd_d;
            Qsink_max = QTsink_tot_Cmax + QVsink_tot_Cmax + QSopsink_tot_Cmax;

            Q_max[1] = 0.8 * (Qsource_max - Qsink_max) * (1 + 0.3 * Math.Exp(-tao_max / 120)) - Cwirk_A * zoneArea / 60 * (dtheta_i_NA - 2) + Cwirk_A * zoneArea / 40 * (12 / t_c_op_d - 1);

            double[,] beta_h = new double[2, 12]; double[,] beta_c = new double[2, 12]; double[,] t_mth = new double[2, 12]; double[,] th_mth = new double[2, 12]; double[,] tc_mth = new double[2, 12]; //wewd,mth

            for (int wewd = 0; wewd < 2; wewd++)
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    beta_h[0, mth] = 0;
                    beta_c[0, mth] = 0;
                    beta_h[1, mth] = Qb_mth[0, mth] / (Q_max[0] * 24);
                    beta_c[1, mth] = Qb_mth[1, mth] / (Q_max[1] * t_c_op_d);
                    if (beta_h[wewd, mth] > 1)
                    {
                        beta_h[wewd, mth] = 1;
                    }

                    t_mth[0, mth] = 0 * 24;
                    t_mth[1, mth] = dmth[mth] * 24;

                    if (beta_h[wewd, mth] > 0.05)
                    {
                        th_mth[wewd, mth] = t_mth[wewd, mth];
                    }
                    else
                    {
                        th_mth[wewd, mth] = t_mth[wewd, mth] * beta_h[wewd, mth] / 0.05;
                    }
                    if (beta_c[wewd, mth] > 0.15)
                    {
                        tc_mth[wewd, mth] = dmth[mth] * t_c_op_d;
                    }
                    else
                    {
                        tc_mth[wewd, mth] = dmth[mth] * t_c_op_d * beta_c[wewd, mth] / 0.15;
                    }
                }
            }
            for (int mth = 0; mth < 12; mth++)
            {
                t_max[0, mth] = th_mth[0, mth] + th_mth[1, mth];
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
        public double Inwall_f; // 배분계수(ISO 식(4) F_ztc,i;ztu) — 생성 시점엔 값이 없고, Zone.Zone_bztu()에서만 계산/대입됨

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

        public double f_ztc_ztu()
        {
            return Inwall_f;
        }
    }

    public class Slab
    {
        String Slab_Num;
        String Slab_SideZone;
        double Slab_Area;
        double Slab_U;
        public double Slab_f; // 배분계수(ISO 식(4) F_ztc,i;ztu) — 생성 시점엔 값이 없고, Zone.Zone_bztu()에서만 계산/대입됨

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
        public double f_ztc_ztu()
        {
            return Slab_f;
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

        public Wall(String EnvelopeNum, String ConstructionNum, double Area, double Ueff, double α, string DiIndi, String Direction, String Degree)
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
        public int Direction_angle() // ZoneEnvelope_3D.방위각을 번호로 그때그때 조회, CALC.ConvertDirectionAngle()로 γic 변환까지 — 생성자를 거치는 모든 곳(Cal_Alt/Optimal/Rule 포함)을 고칠 필요 없음
        {
            string[][] rows = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "방위각", "번호 = '" + wall_Num + "'");
            return rows.Length > 0 ? CALC.ConvertDirectionAngle(Program.UTIL.ToDoubleOrZero(rows[0][0])) : 0;
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
        public int Direction_angle() // ZoneEnvelope_3D.방위각을 번호로 그때그때 조회, CALC.ConvertDirectionAngle()로 γic 변환까지
        {
            string[][] rows = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "방위각", "번호 = '" + Roof_Num + "'");
            return rows.Length > 0 ? CALC.ConvertDirectionAngle(Program.UTIL.ToDoubleOrZero(rows[0][0])) : 0;
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
        public int Direction_angle() // ZoneEnvelope_3D.방위각을 번호로 그때그때 조회, CALC.ConvertDirectionAngle()로 γic 변환까지
        {
            string[][] rows = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "방위각", "번호 = '" + Window_Num + "'");
            return rows.Length > 0 ? CALC.ConvertDirectionAngle(Program.UTIL.ToDoubleOrZero(rows[0][0])) : 0;
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

        public CW(String EnvelopeNum, String ConstructionNum, double Area_g, double Uvalue_g, double Ff_g, double g_g, double gtot_g, double tao_g, double taotot_g, double Area_p, double Uvalue_p, double α_p, double Area_d, double Uvalue_d, double Ff_d, double g_d, double tao_d, double Area_tot, double Uinst, String Direction, String Degree, string cWType)
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
        public int Direction_angle() // ZoneEnvelope_3D.방위각을 번호로 그때그때 조회, CALC.ConvertDirectionAngle()로 γic 변환까지
        {
            string[][] rows = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "방위각", "번호 = '" + CW_Num + "'");
            return rows.Length > 0 ? CALC.ConvertDirectionAngle(Program.UTIL.ToDoubleOrZero(rows[0][0])) : 0;
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
        public int Direction_angle() // ZoneEnvelope_3D.방위각을 번호로 그때그때 조회, CALC.ConvertDirectionAngle()로 γic 변환까지
        {
            string[][] rows = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "방위각", "번호 = '" + Door_Num + "'");
            return rows.Length > 0 ? CALC.ConvertDirectionAngle(Program.UTIL.ToDoubleOrZero(rows[0][0])) : 0;
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
        public double nmech_Calc(double nmech_SUP, double tV_mech)
        {
            double nmech = nmech_SUP * tV_mech / 24;
            return nmech;
        }
        public double nz_Calc(double nz_SUP, double tV_mech)
        {
            double nz_d = nz_SUP * tV_mech / 24;
            return nz_d;
        }
        public double ninf_Calc(double n_inf0, double fe, double tV_mech)
        {
            double ninf;
            if (fe != 0)
            {
                ninf = n_inf0 * (1 + (fe - 1) * tV_mech / 24);
            }
            else
            {
                ninf = n_inf0;
            }
            return ninf;
        }
        public double nwin_Calc(double nSUP, double nETA, double tV_mech, double twd, double nwd, double n_inf0, double fe)
        {
            double Δnwin_mech_0, Δnwin_mech, Δnwin, nwin;

            // n_inf,0(식 64)과 fe는 이제 파라미터로 받음(ZoneHV()가 ninf_Calc()랑 공유해서 한 번만 계산) —
            // Δnwin_mech_0/Δnwin/Δnwin_mech(식 79/80/83-88)는 전부 이 n_inf,0을 씀

            //Δnwin_mech_0계산
            if (nwd < 1.2)
            {
                Δnwin_mech_0 = Math.Max(0, nwd - (nwd - 0.2) * n_inf0 * fe - 0.1);
            }
            else
            {
                Δnwin_mech_0 = Math.Max(0, nwd - n_inf0 * fe - 0.1);
            }

            //Δnwin_mech 계산
            if ((Δnwin_mech_0 <= nSUP) && (nETA <= (nSUP + n_inf0)))
            {
                Δnwin_mech = 0;
            }
            else if ((Δnwin_mech_0 <= nSUP) && (nETA > (nSUP + n_inf0)))
            {
                Δnwin_mech = nETA - nSUP - n_inf0;
            }
            else if ((Δnwin_mech_0 > nSUP) && (nETA <= (nSUP + n_inf0)))
            {
                Δnwin_mech = Δnwin_mech_0 - nSUP;
            }
            else
            {
                Δnwin_mech = nETA - nSUP - n_inf0;
            }


            //Δnwin 계산
            if (nwd < 1.2)
            {
                Δnwin = Math.Max(0, nwd - (nwd - 0.2) * n_inf0 - 0.1);
            }
            else
            {
                Δnwin = Math.Max(0, nwd - n_inf0 - 0.1);
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

        public double Setback(double gamma, double theta_e, double theta_i_set, double tao, double dt_red, double n_red, double dtheta_i_NA, string mode)
        {
            double f_H_red_y = (dt_red * n_red) / (24 * 7);
            double dtheta_float = 1;
            if (theta_i_set > theta_e)
            {
                dtheta_float = Math.Max(Math.Min(gamma, 1), 0);
            }
            else
            {
                dtheta_float = 1;
            }

            double dtH_red_y_tao_H = dt_red / tao;

            double dtheta_red_mn_y = dtheta_float + (1 - dtheta_float) * (1 - Math.Exp(-dtH_red_y_tao_H)) / dtH_red_y_tao_H;

            double dtheta_set_H_low_y = 0;
            if ((theta_i_set- dtheta_i_NA) <= theta_e)
            {
                dtheta_set_H_low_y = 0;
            }
            else if (theta_i_set <= theta_e)
            {
                dtheta_set_H_low_y = 1;
            }
            else
            {
                dtheta_set_H_low_y = (theta_i_set - dtheta_i_NA - theta_e) / (theta_i_set - theta_e);
            }

            double dt_red_low_y_tao_H = 1;
            double fH_red_low_y = 1;
            if (dtheta_set_H_low_y <= dtheta_float || mode == "운전정지")
            {
                dt_red_low_y_tao_H = 1;
                fH_red_low_y = 1;
            }
            else if (dtheta_float == 1)
            {
                dt_red_low_y_tao_H = 0;
                fH_red_low_y = 0;
            }
            else
            {
                dt_red_low_y_tao_H = -Math.Log((dtheta_set_H_low_y - dtheta_float) / (1 - dtheta_float));
                fH_red_low_y = dt_red_low_y_tao_H / dtH_red_y_tao_H;
            }

            double dtheta_H_red_mn_y = 1;
            if (fH_red_low_y >= 1)
            {
                dtheta_H_red_mn_y = dtheta_red_mn_y;
            }
            else
            {
                dtheta_H_red_mn_y = ((1 - dtheta_set_H_low_y) / dtH_red_y_tao_H + fH_red_low_y * dtheta_float + (1 - fH_red_low_y) * dtheta_set_H_low_y);
            }

            double a_H_red_y = (1 - f_H_red_y) + f_H_red_y * dtheta_H_red_mn_y;

            a_H_red_y = double.IsNaN(a_H_red_y) ? 1 : a_H_red_y;

            return a_H_red_y;
        }


    }

    public class QTCalc
    {
        public double Calc_sink(double Te, double Ti, double HT, double dmth)
        {
            double QT_sink;
            QT_sink = (Ti - Te) * HT * 24 * dmth / 1000;
            return QT_sink;
        }

        public double Calc_sink_max(double Te, double Ti, double HT)
        {
            double QT_sink;
            QT_sink = (Ti - Te) * HT;
            return QT_sink;
        }

        public double Calc_source_max(double Te, double Ti, double HT)
        {
            double QT_source;
            QT_source = (Te - Ti) * HT;
            return QT_source;
        }
    }

    public class QSopCalc
    {
        double Rse = 0.04;
        double Uvalue;
        double Area;
        double α;
        double IS;
        double hr = 4.5;
        double dtheta_er = 10;

        public double Calc(double Uvalue, double Area, double α, double IS, double dmth)
        {
            double QSop_source;
            QSop_source = Rse * Uvalue * Area * α * IS * 24 * dmth / 1000;
            return QSop_source;
        }

        public double Calc_max(double Uvalue, double Area, double α, double IS, double Ff)
        {
            double QSop_sink, QSop_source;
            if (Ff * hr * dtheta_er >= α * IS)
            {
                QSop_sink = Rse * Uvalue * Area * (Ff * hr * dtheta_er - α * IS);
                QSop_source = 0;
                return QSop_sink;
            }
            else
            {
                QSop_sink = 0;
                QSop_source = Rse * Uvalue * Area * (α * IS - Ff * hr * dtheta_er);
                return QSop_source;
            }
        }
    }

    public class QS_rad
    {
        double Rse = 0.04;
        double Uvalue;
        double Area;
        double hr = 4.5;
        double dtheta_er = 10;

        public double Calc(double Uvalue, double Area, double Ff, double dmth)
        {
            double QSop_sink, QSop_source;
            QSop_sink = Rse * Uvalue * Area * Ff * hr * dtheta_er * 24 * dmth / 1000;
            return QSop_sink;
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

        public double Calc(double Ff, double Area, double geff, double Is, double dmth)
        {
            double QS;
            QS = Ff * Area * geff * Is * 24 * dmth / 1000;
            return QS;
        }
        public double Calc_max(double Ff, double Area, double geff, double Is)
        {
            double QS;
            QS = Ff * Area * geff * Is;
            return QS;
        }


    }

    public class QVCalc
    {
        public double Calc_sink(double Te, double Ti, double HV, double dmth)
        {
            double QV_sink;
            QV_sink = (Ti - Te) * HV * 24 * dmth / 1000;
            return QV_sink;
        }


        public double Calc_sink_max(double Te, double Ti, double HV)
        {
            double QV_sink;
            QV_sink = (Ti - Te) * HV;
            return QV_sink;
        }

        public double Calc_source_max(double Te, double Ti, double HV)
        {
            double QV_source;
            QV_source = (Te - Ti) * HV;
            return QV_source;
        }
    }

    public class eta_Calc
    {
        public double eta_h_Calc(double gamma, double a, double Qsource)
        {
            double eta;

            if (gamma == 1)
            {
                eta = a / (a + 1);
            }
            else if (gamma > 0 && gamma != 1)
            {
                eta = (1 - Math.Pow(gamma, a)) / (1 - Math.Pow(gamma, a + 1));
            }
            else if (gamma <= 0 && Qsource > 0)
            {
                eta = 1 / gamma;
            }
            else
            {
                eta = 1;
            }


            if (double.IsNaN(eta) || eta < 0)
            {
                eta = 0;
            }
            else { eta = eta; }

            return eta;
        }

        public double eta_c_Calc(double gamma, double a)
        {
            double eta;

            if (gamma == 1)
            {
                eta = a / (a + 1);
            }
            else if (gamma > 0 && gamma != 1)
            {
                eta = (1 - Math.Pow(gamma, -a)) / (1 - Math.Pow(gamma, -(a + 1)));
            }
            else
            {
                eta = 1;
            }

            if (double.IsNaN(eta) || eta < 0)
            {
                eta = 0;
            }
            else { eta = eta; }

            return eta;
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

        public double Qcb_Calc(double Qsource, double η, double Qsink)
        {
            double Qcb;
            Qcb = (Qsource - η * Qsink);
            if (double.IsNaN(Qcb) || Qcb < 0)
            {
                Qcb = 0;
            }
            else { Qcb = Qcb; }
            return Qcb;
        }
    }

}
