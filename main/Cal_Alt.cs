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
    internal class Cal_Alt
    {        
        public ArrayList zone = new ArrayList();
        public ArrayList zonelight = new ArrayList();
        string[][] 지역구분 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역구분", "");
      
        private void Save_Alt_Qb(Zone zone1,string 검토유형)
        {
            String[] HC = { "난방", "냉방" };
            String[] WEWD = { "비이용일", "이용일" };
            String[] MTH = { "1월", "2월", "3월", "4월", "5월", "6월", "7월", "8월", "9월", "10월", "11월", "12월" };
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
                              "'" + 검토유형 + "','" + zone1.ZoneNum + "','" + zone1.zoneName + "','" +
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
        private void Save_Final(Final final1, string 검토유형)
        {
            String MTH;
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            string[][] PNum = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
            for (int mth = 0; mth <= 11; mth++)
            {
                MTH = (mth + 1).ToString() + "월";
                Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result_Alt", "프로젝트번호,프로젝트유형,검토유형,번호,월,연료," +
                    "난방,냉방,급탕,조명,공조,기저에너지,총에너지소요량",
                    "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + 검토유형 + "','" + PNum[0][0] + "','" + MTH + "','" + "전기" + "','" +
                    final1.Qhf_elec[mth] + "','" + final1.Qcf_elec[mth] + "','" + final1.Qwf_elec[mth] + "','" + final1.Qlf_elec[mth] + "','" +
                    final1.Qvf_elec[mth] + "','" + final1.Qbase_elec[mth] + "','" + final1.Qf_elec_tot_mth[mth]
                    + "'", "검토유형,번호,월,연료"); ;

            }
            string Carrier = "";
            if (final1.Carrier_h != "" && final1.Carrier_h != null) { Carrier = final1.Carrier_h; } else if (final1.Carrier_w != "" && final1.Carrier_w != null) { Carrier = final1.Carrier_w; } else if (final1.Carrier_c != "" && final1.Carrier_c != null) { Carrier = final1.Carrier_c; }
            if (Carrier == "LNG" || Carrier == "LPG") { Carrier = "가스"; }
            if (Carrier != "")
            {
                for (int mth = 0; mth <= 11; mth++)
                {
                    MTH = (mth + 1).ToString() + "월";
                    Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result_Alt", "프로젝트번호,프로젝트유형,검토유형,번호,월,연료," +
                        "난방,냉방,급탕,조명,공조,기저에너지,총에너지소요량",
                        "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + 검토유형 + "','" + PNum[0][0] + "','" + MTH + "','" + Carrier + "','" +
                        final1.Qhf_gas[mth] + "','" + final1.Qcf_gas[mth] + "','" + final1.Qwf_gas[mth] + "','" + "0" + "','" +
                        "0" + "','" + final1.Qbase_gas[mth] + "','" + final1.Qf_gas_tot_mth[mth]
                        + "'", "검토유형,번호,월,연료"); ;
                }
            }

        }
        private void Calc_System(string 검토유형)
        {
            int i;
            #region 공조계산
            string[][] Num = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "번호,유형");
            if (Num.Length > 0)
            {
                i = -1;
                while (++i < Num.Length)
                {
                    AHU Pre_AHU1 = new AHU(Num[i][0]);
                    CALC.AHUs[Num[i][0]] = Pre_AHU1;
                    CALC.AHUSystem_LaodData(Pre_AHU1);
                    CALC.AHUSystem_PreCalc(Pre_AHU1);
                }
                CALC.Cal_Qb();
                i = -1;
                while (++i < Num.Length)
                {
                    if (Num[i][1] == "공조기")
                    {
                        AHU Post_AHU1 = new AHU(Num[i][0]);
                        CALC.AHUs[Num[i][0]] = Post_AHU1;
                        CALC.AHUSystem_LaodData(Post_AHU1);
                        CALC.AHUSystem_PostCalc(Post_AHU1);
                    }
                    else
                    {
                        AHU Post_HRV1 = new AHU(Num[i][0]);
                        CALC.AHUs[Num[i][0]] = Post_HRV1;
                        CALC.AHUSystem_LaodData(Post_HRV1);
                        CALC.HRV_PostCalc(Post_HRV1);
                    }

                }
            }
            #endregion

            #region 난방계산
            CALC.Heating_ce_zone_calc();
            string[][] HeatingNum = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "번호");
            i = -1;
            while (++i < HeatingNum.Length)
            {
                Heating Heating1 = new Heating(HeatingNum[i][0]);
                CALC.Heatings[HeatingNum[i][0]] = Heating1;
                CALC.Heating_LoadData(Heating1);
                CALC.Heating_Calc(Heating1);
            }
            #endregion

            CALC.CoolingSystemCalc();

            #region 급탕계산
            string[][] DHWNum = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "번호");
            if (DHWNum.Length > 0)
            {
                i = -1;
                while (++i < DHWNum.Length)
                {
                    DHW DHW1 = new DHW(DHWNum[i][0]);
                    CALC.DHWs[DHWNum[i][0]] = DHW1;
                    CALC.DHW_LoadData(DHW1);
                    CALC.DHW_Calc(DHW1);
                }
            }
            #endregion

            #region 파이널계산
            string[][] PNum = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
            if(PNum.Length > 0)
            {
                Final final1 = new Final();
                string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
                if (프로젝트유형[0][0] == "1")
                {
                    for (int mth = 0; mth < 12; mth++)
                    {
                        string[][] Final2 = Program.DB.querySQL(DB.type.ProjDB, "SELECT 기저에너지 FROM FinalEnergy_Result where 연료 = '전기' and 월 = '" + (mth + 1).ToString() + "월'");
                        if (Final2.Length > 0)
                        {
                            final1.Qbase_elec[mth] = Convert.ToDouble(Final2[0][0]);
                        }

                        Final2 = Program.DB.querySQL(DB.type.ProjDB, "SELECT 기저에너지 FROM FinalEnergy_Result where 연료 != '전기' and 월 = '" + (mth + 1).ToString() + "월'");
                        if (Final2.Length > 0)
                        {
                            final1.Qbase_gas[mth] = Convert.ToDouble(Final2[0][0]);
                        }
                    }
                }
                else
                {
                    string[][] res = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기존프로젝트");
                    if (res.Length > 0 && res[0][0] != "")
                    {
                        for (int mth = 0; mth < 12; mth++)
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

                for (int mth = 0; mth < 12; mth++)
                {
                    final1.Qf_elec_tot_mth[mth] = final1.Qhf_elec[mth] + final1.Qcf_elec[mth] + final1.Qwf_elec[mth] + final1.Qlf_elec[mth] + final1.Qvf_elec[mth] + final1.Qbase_elec[mth];
                    final1.Qf_gas_tot_mth[mth] = final1.Qhf_gas[mth] + final1.Qcf_gas[mth] + final1.Qwf_gas[mth] + final1.Qbase_gas[mth];
                }
                final1.Calc_Error();
                Save_Final(final1, 검토유형);
            }
            #endregion
            
            CALC.RESystemCalc();            
        }
        public void Calc_RuleAlt(string 검토유형) 
        {
            CALC.Zone_Arrange();
            for (int k = 0; k < CALC.zone.Count; k++)
            {
                Zone zone1 = (Zone)CALC.zone[k];
                ZoneLight zonelight1 = (ZoneLight)CALC.zonelight[k];
                CALC.Zone_LoadData(zone1, zonelight1);

                switch (검토유형)
                {
                    case "법규_외벽":
                        Load_Rule_Wall(zone1);
                        break;
                    case "법규_지붕":
                        Load_Rule_Roof(zone1);
                        break;
                    case "법규_최하층바닥":
                        Load_Rule_Floor(zone1);
                        break;
                    case "법규_창호":
                        Load_Rule_Win(zone1);
                        break;
                    case "법규_커튼월창":
                        Load_Rule_CW(zone1);
                        break;
                    case "법규_외부출입문":
                        Load_Rule_Door(zone1);
                        break;
                    case "법규_전체":
                        Load_Rule_All(zone1);
                        break;
                }

                CALC.Zone_Calc(zone1, zonelight1);
                Save_Alt_Qb(zone1, 검토유형);
            }
            Calc_System(검토유형);
        }
        #region 법규기반
        private void Load_Rule_Wall(Zone zone1)
        {
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
        }
        private void Load_Rule_Roof(Zone zone1)
        {
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
        }
        private void Load_Rule_Floor(Zone zone1)
        {
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
        }
        private void Load_Rule_Win(Zone zone1)
        {
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
        }
        private void Load_Rule_CW(Zone zone1)
        {
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
        }
        private void Load_Rule_Door(Zone zone1)
        {
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
        }
        private void Load_Rule_All(Zone zone1)
        {
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
        }
        #endregion

        #region 기여율검토
        #endregion
    }
}
