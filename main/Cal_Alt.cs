using Eagle._Components.Public;
using Eagle._Constants;
using Eagle._Interfaces.Public;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace main
{
    internal class Cal_Alt
    {
        string[][] PreProjNum = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기존프로젝트", "");
        string[][] NowProjNum = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
        string[][] 지역구분 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역구분", "");
        ArrayList Heating_ces = new ArrayList();
        private static ArrayList Split_(String nonSplit)
        {
            ArrayList split = new ArrayList();
            if (nonSplit != null)
            {
                if (nonSplit.Contains('+'))
                {
                    string[] token = nonSplit.Split('+');
                    split.Clear();
                    foreach (var item in token)
                    {
                        split.Add(item.ToString());
                    }
                }
                else
                {
                    split.Clear();
                    split.Add(nonSplit);
                }
            }
            return split;
        }
      
        private void Save_Alt_Qb(Zone zone1,string 검토유형)
        {
            String[] HC = { "난방", "냉방" };
            String[] WEWD = { "비이용일", "이용일" };
            String[] MTH = { "1월", "2월", "3월", "4월", "5월", "6월", "7월", "8월", "9월", "10월", "11월", "12월" };
          
            for (int hc = 0; hc <= 1; hc++)
            {
                    Program.DB.setValue(DB.type.ProjDB, "Zone_Alt_Result", "검토유형,번호,이름," +
                             "난방_냉방,비이용일_이용일,월," +
                             "Qb_day," +
                             "Qb_mth," +
                             "Qb_a,Q_max, t_max,비냉난방존온도",
                              "'" + 검토유형 + "','" + zone1.ZoneNum + "','" + zone1.zoneName + "','" +
                              HC[hc] + "','" + WEWD[1] + "','" + MTH[0] + "','" +
                              zone1.Qb_day[hc, 1, 0].ToString() + "','" +
                              zone1.Qb_mth[hc, 1, 0].ToString() + "','" +
                              zone1.Qb_a[hc].ToString() + "','" + zone1.Q_max[hc].ToString() + "','" + zone1.t_max[hc, 0].ToString() + "','" +
                              zone1.Theta_U[hc, 1, 0].ToString()
                              + "'", "번호,난방_냉방,비이용일_이용일,월,검토유형");
                
            }
        }
        private void Save_Alt(Final final1, string 검토유형, string table명칭)
        {
            #region 전기
            String MTH;
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            string[][] PNum = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
          
            double Qhf_elec_a = 0, Qcf_elec_a = 0, Qwf_elec_a = 0, Qlf_elec_a = 0, Qvf_elec_a = 0, Qbase_elec_a = 0, Qreg_elec_a = 0, Qf_elec_tot_a = 0;

            for (int mth = 0; mth < 12; mth++)
            {
                Qhf_elec_a += final1.Qhf_elec[mth];
                Qcf_elec_a += final1.Qcf_elec[mth];
                Qwf_elec_a += final1.Qwf_elec[mth];
                Qlf_elec_a += final1.Qlf_elec[mth];
                Qvf_elec_a += final1.Qvf_elec[mth];
                Qbase_elec_a += final1.Qbase_elec[mth];
                Qreg_elec_a += final1.Qreg_elec[mth];
            }
            Qf_elec_tot_a = Qhf_elec_a + Qcf_elec_a + Qwf_elec_a + Qlf_elec_a + Qvf_elec_a + Qbase_elec_a - Qreg_elec_a;
            Program.DB.setValue(DB.type.ProjDB, table명칭, "프로젝트번호,프로젝트유형,검토유형,번호,월,연료," +
                    "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량",
                    "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + 검토유형 + "','" + PNum[0][0] + "','" + "연간" + "','" + "전기" + "','" +
                    Qhf_elec_a + "','" + Qcf_elec_a + "','" + Qwf_elec_a + "','" + Qlf_elec_a + "','" +
                    Qvf_elec_a + "','" + Qbase_elec_a + "','" + Qreg_elec_a + "','" + Qf_elec_tot_a
                    + "'", "검토유형,번호,월,연료");
            #endregion

            #region 가스
            string Carrier = "";
            if (final1.Carrier_h != "" && final1.Carrier_h != null) { Carrier = final1.Carrier_h; } else if (final1.Carrier_w != "" && final1.Carrier_w != null) { Carrier = final1.Carrier_w; } else if (final1.Carrier_c != "" && final1.Carrier_c != null) { Carrier = final1.Carrier_c; }
            if (Carrier == "LNG" || Carrier == "LPG") { Carrier = "가스"; }
            
            double Qhf_gas_a = 0, Qcf_gas_a = 0, Qwf_gas_a = 0, Qbase_gas_a = 0, Qf_gas_tot_a = 0;
            for (int mth = 0; mth < 12; mth++)
            {
                Qhf_gas_a += final1.Qhf_gas[mth];
                Qcf_gas_a += final1.Qcf_gas[mth];
                Qwf_gas_a += final1.Qwf_gas[mth];
                Qbase_gas_a += final1.Qbase_gas[mth];
            }
            Qf_gas_tot_a = Qhf_gas_a + Qcf_gas_a + Qwf_gas_a + Qbase_gas_a;
            Program.DB.setValue(DB.type.ProjDB, table명칭, "프로젝트번호,프로젝트유형,검토유형,번호,월,연료," +
                    "난방,냉방,급탕,조명,공조,기저에너지,총에너지소요량",
                    "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + 검토유형 + "','" + PNum[0][0] + "','" + "연간" + "','" + Carrier + "','" +
                    Qhf_gas_a + "','" + Qcf_gas_a + "','" + Qwf_gas_a + "','" + "0" + "','" +
                    "0" + "','" + Qbase_gas_a + "','" + Qf_gas_tot_a
                    + "'", "검토유형,번호,월,연료");
            #endregion
            #region 전체           

            Program.DB.setValue(DB.type.ProjDB, table명칭, "프로젝트번호,프로젝트유형,검토유형,번호,월,연료," +
                   "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량",
                   "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + 검토유형 + "','" + PNum[0][0] + "','" + "연간" + "','" + "전체" + "','" +
                   (Qhf_elec_a + Qhf_gas_a) + "','" + (Qcf_elec_a + Qcf_gas_a) + "','" + (Qwf_elec_a + Qwf_gas_a) + "','" + Qlf_elec_a + "','" +
                   Qvf_elec_a + "','" + (Qbase_elec_a + Qbase_gas_a) + "','" + Qreg_elec_a + "','" + (Qf_elec_tot_a + Qf_gas_tot_a)
                   + "'", "검토유형,번호,월,연료");
            #endregion
        }

        public void Calc_Rule(string 검토유형) 
        {
            Calc_Qb_Rule_Alt(검토유형);
            Calc_System_Rule(검토유형);
        }
        public void Calc_Element(string 검토유형)
        {
            Calc_Qb_Element(검토유형);
            Calc_System_element(검토유형);
        }

        #region 법규기반 검토
        public void Calc_Qb_Rule_Alt(string 검토유형)
        {
            CALC.Zone_Arrange();
            for (int k = 0; k < CALC.zone.Count; k++)
            {
                Zone zone1 = (Zone)CALC.zone[k];
                ZoneLight zonelight1 = (ZoneLight)CALC.zonelight[k];
                CALC.Zone_LoadData(zone1, zonelight1);

                switch (검토유형)
                {
                    case "외벽":
                        Load_Rule_Wall(zone1);
                        break;
                    case "지붕":
                        Load_Rule_Roof(zone1);
                        break;
                    case "최하층바닥":
                        Load_Rule_Floor(zone1);
                        break;
                    case "창호":
                        Load_Rule_Win(zone1);
                        break;
                    case "커튼월창":
                        Load_Rule_CW(zone1);
                        break;
                    case "외부출입문":
                        Load_Rule_Door(zone1);
                        break;
                    case "전체":
                        Load_Rule_All(zone1);
                        break;
                }
                CALC.Zone_Calc(zone1, zonelight1);
            }
        }
        private void Calc_System_Rule(string 검토유형)
        {
            int i;
            #region 공조계산
            string[][] Num = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "번호,유형");
            if (Num.Length > 0)
            {
                for (int k = 0; k < Num.Length; k++)
                {
                    CALC.AHUs[Num[k][0]] = null;
                }
                i = -1;
                while (++i < Num.Length)
                {
                    if (Num[i][1] == "공조기")
                    {
                        AHU Pre_AHU1 = new AHU(Num[i][0]);
                        CALC.AHUs[Num[i][0]] = Pre_AHU1;
                        CALC.AHUSystem_LaodData(Pre_AHU1, NowProjNum[0][0]);
                        CALC.AHUSystem_PreCalc(Pre_AHU1);
                    }
                    else
                    {
                        AHU Pre_HRV1 = new AHU(Num[i][0]);
                        CALC.AHUs[Num[i][0]] = Pre_HRV1;
                        CALC.HRV_LaodData(Pre_HRV1, NowProjNum[0][0]);
                        CALC.AHUSystem_PreCalc(Pre_HRV1);
                    }
                }

                Calc_Qb_Rule_Alt(검토유형);

                i = -1;
                for (int k = 0; k < Num.Length; k++)
                {
                    CALC.AHUs[Num[k][0]] = null;
                }
                while (++i < Num.Length)
                {
                    if (Num[i][1] == "공조기")
                    {
                        AHU Post_AHU1 = new AHU(Num[i][0]);
                        CALC.AHUs[Num[i][0]] = Post_AHU1;
                        CALC.AHUSystem_LaodData(Post_AHU1, NowProjNum[0][0]);
                        CALC.AHUSystem_PostCalc(Post_AHU1);
                    }
                    else
                    {
                        AHU Post_HRV1 = new AHU(Num[i][0]);
                        CALC.AHUs[Num[i][0]] = Post_HRV1;
                        CALC.HRV_LaodData(Post_HRV1, NowProjNum[0][0]);
                        CALC.HRV_PostCalc(Post_HRV1);
                    }

                }
            }
            #endregion

            Cal_Qfh_Now(NowProjNum[0][0]);
            Cal_Qfc_Now(NowProjNum[0][0]);
            Cal_Qfw(NowProjNum[0][0]);

            #region 파이널계산
            Final final1 = new Final(NowProjNum[0][0]);
            final1.Load_Heating_Final(NowProjNum[0][0]);
            final1.Load_Cooling_Final(NowProjNum[0][0]);
            final1.Load_DHW_Final(NowProjNum[0][0]);
            final1.Load_AHU_Final(NowProjNum[0][0]);
            final1.Load_REG_Final(NowProjNum[0][0]);
            final1.Calc_Qtot();
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
            Save_Alt(final1, 검토유형, "FinalEnergy_Result_Rule");

            #endregion

            CALC.RESystemCalc(PreProjNum[0][0]);
        }
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
                    Wall wall = new Wall(ZoneW[i][0], ZoneW[i][2], Convert.ToDouble(ZoneW[i][1]), Convert.ToDouble(ZoneW[i][3]), Convert.ToDouble(ZoneW[i][4]), ZoneW[i][5], ZoneW[i][6], ZoneW[i][7]);
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
                    if (Convert.ToDouble(ZoneG[i][3]) >= 3)
                    { fx_f = 0.35; }
                    else if (Convert.ToDouble(ZoneG[i][3]) >= 1)
                    { fx_f = 0.55; }
                    else if (Convert.ToDouble(ZoneG[i][3]) > 0.3)
                    { fx_f = 0.65; }
                    else { fx_f = 0.75; }
                    break;

                    GWall gwall = new GWall(ZoneG[i][0], ZoneG[i][2], Convert.ToDouble(ZoneG[i][1]), Convert.ToDouble(ZoneG[i][3]), fx_f);
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
                    Roof roof = new Roof(ZoneR[i][0], ZoneR[i][2], Convert.ToDouble(ZoneR[i][1]), Convert.ToDouble(ZoneR[i][3]), Convert.ToDouble(ZoneR[i][4]), ZoneR[i][5], ZoneR[i][6], ZoneR[i][7]);
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
                    Wall wall = new Wall(ZoneW[i][0], ZoneW[i][2], Convert.ToDouble(ZoneW[i][1]), Convert.ToDouble(ZoneW[i][3]), Convert.ToDouble(ZoneW[i][4]), ZoneW[i][5], ZoneW[i][6], ZoneW[i][7]);
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

                    GWall gwall = new GWall(ZoneG[i][0], ZoneG[i][2], Convert.ToDouble(ZoneG[i][1]), Convert.ToDouble(ZoneG[i][3]), fx_f);
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
                    Roof roof = new Roof(ZoneR[i][0], ZoneR[i][2], Convert.ToDouble(ZoneR[i][1]), Convert.ToDouble(ZoneR[i][3]), Convert.ToDouble(ZoneR[i][4]), ZoneR[i][5], ZoneR[i][6], ZoneR[i][7]);
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
                    Floor floor = new Floor(ZoneF[i][0], ZoneF[i][2], Convert.ToDouble(ZoneF[i][1]), Convert.ToDouble(ZoneF[i][3]), ZoneF[i][5], fx_f);
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
        #region 요소기술별 검토
        public void Calc_Qb_Element(string 검토유형)
        {
            CALC.Zone_Arrange();
            for (int k = 0; k < CALC.zone.Count; k++)
            {
                Zone zone1 = (Zone)CALC.zone[k];
                ZoneLight zonelight1 = (ZoneLight)CALC.zonelight[k];
                Zone_LoadData_PreElement(zone1, zonelight1);

                switch (검토유형)
                {                    
                    case "외벽":
                        zone1.zoneWall.Clear();
                        zone1.zoneGWall.Clear();
                        zone1.LoadData_Wall();
                        zone1.LoadData_GWall();
                        break;
                    case "지붕":
                        zone1.zoneRoof.Clear();
                        zone1.LoadData_Roof();
                        break;
                    case "최하층바닥":
                        zone1.zoneFloor.Clear();
                        zone1.LoadData_Floor();
                        break;
                    case "창호":
                        zone1.zoneWin.Clear();
                        zone1.LoadData_Win();
                        break;
                    case "커튼월창":
                        zone1.zoneCW.Clear();
                        zone1.LoadData_CW();
                        break;
                    case "외부출입문":
                        zone1.zoneDoor.Clear();
                        zone1.LoadData_Door();
                        break;
                    case "기밀+열회수기":
                        zone1.LoadData_q50();
                        zone1.LoadData_Ventil();
                        break;
                    case "조명":
                        zonelight1.LoadData_LightSystem();
                        break;
                }
                CALC.Zone_Calc(zone1, zonelight1);
                Save_Alt_Qb(zone1, 검토유형);
            }
        }
        public void Zone_LoadData_PreElement(Zone zone1, ZoneLight zonelight1)
        { 
            string[][] 증축 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "증축여부", "존번호 = '" +zone1.ZoneNum+ "'");
            if (Convert.ToBoolean(증축[0][0]))
            {
                CALC.Zone_LoadData(zone1, zonelight1);
            }
            else
            {
                zonelight1.LoadData_LightGeneral();
                Load_Pre_LightingSystem(zonelight1);
                zonelight1.LoadData_NaturalLight();
                zonelight1.LoadData_Renew();
                zone1.LoadData_ZoneGeneral();
                Load_Pre_q50(zone1);
                Load_Pre_Ventil(zone1);
                zone1.LoadData_InWall();
                zone1.LoadData_SL();
                Load_Pre_Wall(zone1);
                Load_Pre_Roof(zone1);
                Load_Pre_Floor(zone1);
                Load_Pre_Door(zone1);
                Load_Pre_Win(zone1);
                Load_Pre_CW(zone1); 
            }            
        }
        private void Load_Pre_LightingSystem(ZoneLight zonelight1)
        {
            ArrayList split_Zone = new ArrayList(); string[][] Zone_Pre = null;
            string[][] Zone_Post = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_form", "기존존", "존번호='" + zonelight1.ZoneNum + "'");
            if (Zone_Post.Length > 0)
            {
                if (Zone_Post[0][0] != "")
                { split_Zone = Split_(Zone_Post[0][0]); }
            }
            for(int i =0; i <  split_Zone.Count; i++)
            {
                string[][] ValueA = Program.DB.getValue(PreProjNum[0][0], "ZoneLighting_form", "조명밀도,조명예상전력,재실계수,조도제어계수,광효율,대기전력,조명개수", "번호='" + split_Zone[i] + "'");
                if (ValueA.Length > 0)
                {
                    zonelight1.Pj = Convert.ToDouble(ValueA[0][0]);
                    zonelight1.Pn = Convert.ToDouble(ValueA[0][1]);
                    zonelight1.Fo = Convert.ToDouble(ValueA[0][2]);
                    zonelight1.Fc = Convert.ToDouble(ValueA[0][3]);
                    zonelight1.lm_W = Convert.ToDouble(ValueA[0][4]);
                    zonelight1.wsp = Convert.ToDouble(ValueA[0][5]);
                    zonelight1.N = Convert.ToDouble(ValueA[0][6]);
                }
            }
            
        }
        private void Load_Pre_q50(Zone zone1)
        {
            string[][] Value2 = Program.DB.getValue(PreProjNum[0][0], "BuildingGeneral", "기밀측정여부,출입문q50,창호q50,외벽q50,지붕q50", "");
            if (Value2.Length > 0)
            {
                zone1.Door_q50 = Convert.ToDouble(Value2[0][1]);
                zone1.Win_q50 = Convert.ToDouble(Value2[0][2]);
                zone1.Wall_q50 = Convert.ToDouble(Value2[0][3]);
                zone1.Roof_q50 = Convert.ToDouble(Value2[0][4]);
            }
        }
        private void Load_Pre_Ventil(Zone zone1)
        {//존 환기정보 가져오기 
            ArrayList split_Zone = new ArrayList(); string[][] Zone_Pre =null;
            string[][] Zone_Post = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_form", "비이용일환기량,이용일환기량,기존존", "존번호='" + zone1.ZoneNum + "'");
            if (Zone_Post.Length > 0)
            {                
                if (Zone_Post[0][2] != "")
                { split_Zone = Split_(Zone_Post[0][2]); }
            }
            for(int i =0; i < split_Zone.Count; i++)
            {
               Zone_Pre = Program.DB.getValue(PreProjNum[0][0], "ZoneGeneral_form", "환기유무,환기방식,비이용일환기량,이용일환기량,선택열회수기", "존번호='" + split_Zone[i] + "'");
                if(Zone_Pre.Length >0)
                {
                    if (Zone_Pre[0][0] =="True")
                    {
                        goto load_ventil;
                    }
                }    
            }

            load_ventil: 
                if (Convert.ToBoolean(Zone_Pre[0][0]))
                {
                    if (Zone_Pre[0][1] == "열회수기")
                    {
                        zone1.Vmech_SUP_we = Convert.ToDouble(Zone_Pre[0][2]);
                        zone1.Vmech_ETA_we = Convert.ToDouble(Zone_Pre[0][2]);
                        zone1.Vmech_SUP_wd = Convert.ToDouble(Zone_Pre[0][3]);
                        zone1.Vmech_ETA_wd = Convert.ToDouble(Zone_Pre[0][3]);
                        zone1.SelectHRV = Zone_Pre[0][4];
                        string[][] value = Program.DB.getValue(PreProjNum[0][0], "User_HRV", "온도교환효율_난방,온도교환효율_냉방,습도교환효율_난방,습도교환효율_냉방", "번호='" + zone1.SelectHRV + "'");
                        if (value.Length > 0)
                        {
                            zone1.eta_V_mech[0] = Convert.ToDouble(value[0][0]) / 100;
                            zone1.eta_V_mech[1] = Convert.ToDouble(value[0][1]) / 100;
                            zone1.eta_χV_mech[0] = Convert.ToDouble(value[0][2]) / 100;
                            zone1.eta_χV_mech[1] = Convert.ToDouble(value[0][3]) / 100;
                        }

                    }
                    else if (Zone_Pre[0][1] == "공조기")
                    {
                        zone1.Vmech_SUP_we = Convert.ToDouble(Zone_Pre[0][2]);
                        zone1.Vmech_ETA_we = Convert.ToDouble(Zone_Pre[0][2]);
                        zone1.Vmech_SUP_wd = Convert.ToDouble(Zone_Pre[0][3]);
                        zone1.Vmech_ETA_wd = Convert.ToDouble(Zone_Pre[0][3]);
                        zone1.SelectHRV = Zone_Pre[0][4];
                        string[][] value = Program.DB.getValue(PreProjNum[0][0], "User_AHU", "온도교환효율_난방,온도교환효율_냉방,습도교환효율_난방,습도교환효율_냉방", "번호='" + zone1.SelectHRV + "'");
                        if (value.Length > 0)
                        {
                            zone1.eta_V_mech[0] = Convert.ToDouble(value[0][0]) / 100;
                            zone1.eta_V_mech[1] = Convert.ToDouble(value[0][1]) / 100;
                            zone1.eta_χV_mech[0] = Convert.ToDouble(value[0][2]) / 100;
                            zone1.eta_χV_mech[1] = Convert.ToDouble(value[0][3]) / 100;
                        }
                    }
                    else
                    {
                        zone1.Vmech_SUP_wd = 0;
                        zone1.Vmech_ETA_wd = Convert.ToDouble(Zone_Pre[0][2]); ; //배기환기는 다 비이용일환기량으로 함 
                        zone1.Vmech_SUP_we = 0;
                        zone1.Vmech_ETA_we = Convert.ToDouble(Zone_Pre[0][2]);
                    }
                }
                else
                {
                    zone1.Vmech_SUP_we = 0;
                    zone1.Vmech_SUP_wd = 0;
                    zone1.Vmech_ETA_we = 0;
                    zone1.Vmech_ETA_wd = 0;
                }
                zone1.Vmech_SUP_z = 0;
                zone1.Vmech_ETA_z = 0;
                zone1.ρacp_a = 0.34;
            
        }
        private void Load_Pre_Wall(Zone zone1)
        {
            zone1.zoneWall.Clear();
            zone1.zoneGWall.Clear();
            String[][] ZoneW = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.유효열관류율,b.흡수율,b.직접간접,a.방위,a.기울기,b.Type,b.기존외벽,b.명칭 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "' And  NOT b.직접간접 = '지면'");
            if (ZoneW.Length > 0)
            {               
                    int i = -1;
                while (++i < ZoneW.Length)
                {
                    double Uvalue = 0; 
                    
                    if (ZoneW[i][8] =="신규")
                    {
                        String[][] Date = Program.DB.getValue(PreProjNum[0][0], "BuildingGeneral", "법규시기,지역구분", "");
                        String[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율", "구조체 = '외벽' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND 직접간접 =  '" + ZoneW[i][5] + "'");
                        if (Value.Length > 0)
                        { Uvalue = Convert.ToDouble(Value[0][0]); }
                    }
                    else if(ZoneW[i][8] == "기존외벽")
                    {
                        String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionWall", "유효열관류율", "명칭 ='" + ZoneW[i][10] + "'");
                        if (Pre.Length > 0)
                        { Uvalue = Convert.ToDouble(Pre[0][0]); }
                    }
                    else
                    {
                        String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionWall", "유효열관류율", "명칭 ='" + ZoneW[i][9] + "'");
                        if (Pre.Length > 0)
                        { Uvalue = Convert.ToDouble(Pre[0][0]); }
                    }
                    
                    Wall wall = new Wall(ZoneW[i][0], ZoneW[i][2], Convert.ToDouble(ZoneW[i][1]), Uvalue, Convert.ToDouble(ZoneW[i][4]), ZoneW[i][5], ZoneW[i][6], ZoneW[i][7]);
                    zone1.zoneWall.Add(wall);
                }
            }
            String[][] ZoneG = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.유효열관류율,b.직접간접,b.Type,b.기존외벽,b.명칭 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "' And  b.직접간접 = '지면'");
            if (ZoneG.Length > 0)
            {
                int i = -1;
                while (++i < ZoneG.Length)
                {
                    double Uvalue = 0;
                    if (ZoneG[i][5] == "신규")
                    {
                        String[][] Date = Program.DB.getValue(PreProjNum[0][0], "BuildingGeneral", "법규시기,지역구분", "");
                        String[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율", "구조체 = '외벽' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND 직접간접 =  '간접외기'");
                        if (Value.Length > 0)
                        { Uvalue = Convert.ToDouble(Value[0][0]); }
                    }
                    else if(ZoneG[i][5] == "기존외벽")
                    {
                        String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionWall", "유효열관류율", "명칭 ='" + ZoneG[i][7] + "'");
                        if (Pre.Length > 0)
                        { Uvalue = Convert.ToDouble(Pre[0][0]); }
                    }
                    else
                    {
                        String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionWall", "유효열관류율", "명칭 ='" + ZoneG[i][6] + "'");
                        if (Pre.Length > 0)
                        { Uvalue = Convert.ToDouble(Pre[0][0]); }
                    }
                    double fx_f = 0.8;
                    if (Convert.ToDouble(Uvalue) >= 3)
                    { fx_f = 0.35; }
                    else if (Convert.ToDouble(Uvalue) >= 1)
                    { fx_f = 0.55; }
                    else if (Convert.ToDouble(Uvalue) > 0.3)
                    { fx_f = 0.65; }
                    else { fx_f = 0.75; }
                    break;

                    GWall gwall = new GWall(ZoneG[i][0], ZoneG[i][2], Convert.ToDouble(ZoneG[i][1]), Uvalue, fx_f);
                    zone1.zoneGWall.Add(gwall);
                }
            }
        }
        private void Load_Pre_Roof(Zone zone1)
        {
            zone1.zoneRoof.Clear();
            String[][] ZoneR = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.유효열관류율,b.흡수율,b.직접간접,a.방위,a.기울기,b.Type,b.기존지붕,b.명칭 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionRoof AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
            if (ZoneR.Length > 0)
            {
                int i = -1;
                while (++i < ZoneR.Length)
                {
                    double Uvalue = 0;

                    if (ZoneR[i][8] == "신규")
                    {
                        String[][] Date = Program.DB.getValue(PreProjNum[0][0], "BuildingGeneral", "법규시기,지역구분", "");
                        String[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '지붕' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND 직접간접 =  '" + ZoneR[i][5] + "'");
                        if (Value.Length > 0)
                        { Uvalue = Convert.ToDouble(Value[0][0]); }
                    }
                    else if(ZoneR[i][8] == "기존지붕")
                    {
                        String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionRoof", "유효열관류율", "명칭 ='" + ZoneR[i][10] + "'");
                        if (Pre.Length > 0)
                        { Uvalue = Convert.ToDouble(Pre[0][0]); }
                    }
                    else
                    {
                        String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionRoof", "유효열관류율", "명칭 ='" + ZoneR[i][9] + "'");
                        if (Pre.Length > 0)
                        { Uvalue = Convert.ToDouble(Pre[0][0]); }
                    }
                    Roof roof = new Roof(ZoneR[i][0], ZoneR[i][2], Convert.ToDouble(ZoneR[i][1]), Uvalue, Convert.ToDouble(ZoneR[i][4]), ZoneR[i][5], ZoneR[i][6], ZoneR[i][7]);
                    zone1.zoneRoof.Add(roof);
                }
            }
        }
        private void Load_Pre_Floor(Zone zone1)
        {
            zone1.zoneFloor.Clear();
            String[][] ZoneF = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.유효열관류율,b.직접간접,b.기초설치,b.Type,b.기존바닥,b.명칭 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionFloor AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
            if (ZoneF.Length > 0)
            {
                int i = -1;
                while (++i < ZoneF.Length)
                {
                    double Uvalue = 0;
                    
                    if (ZoneF[i][6] == "신규")
                    {
                        string DiIndi = ZoneF[i][4];
                        string DiIndi_;
                        if (DiIndi == "직접외기" || DiIndi == "간접외기")
                        {
                            DiIndi_ = DiIndi;
                        }
                        else
                        {
                            DiIndi_ = "간접외기";
                        }
                        String[][] Date = Program.DB.getValue(PreProjNum[0][0], "BuildingGeneral", "법규시기,지역구분", "");
                        String[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '바닥' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND 직접간접 =  '" + DiIndi_ + "'");
                        if (Value.Length > 0)
                        { Uvalue = Convert.ToDouble(Value[0][0]); }
                    }
                    else if (ZoneF[i][6] == "기존바닥")
                    {
                        String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionFloor", "유효열관류율", "명칭 ='" + ZoneF[i][8] + "'");
                        if (Pre.Length > 0)
                        { Uvalue = Convert.ToDouble(Pre[0][0]); }
                    }
                    else
                    {
                        String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionFloor", "유효열관류율", "명칭 ='" + ZoneF[i][7] + "'");
                        if (Pre.Length > 0)
                        { Uvalue = Convert.ToDouble(Pre[0][0]); }
                    }
                    double fx_f = 0.8;
                    switch (ZoneF[i][5].ToString())
                    {
                        case "지면위":
                            {
                                if (Convert.ToDouble(Uvalue) >= 3)
                                { fx_f = 0.3; }
                                else if (Convert.ToDouble(Uvalue) >= 1)
                                { fx_f = 0.55; }
                                else if (Convert.ToDouble(Uvalue) > 0.3)
                                { fx_f = 0.7; }
                                else { fx_f = 0.8; }
                                break;
                            }
                        case "단열지하":
                            {
                                if (Convert.ToDouble(Uvalue) >= 3)
                                { fx_f = 0.2; }
                                else if (Convert.ToDouble(Uvalue) >= 1)
                                { fx_f = 0.45; }
                                else if (Convert.ToDouble(Uvalue) > 0.3)
                                { fx_f = 0.55; }
                                else { fx_f = 0.7; }
                                break;
                            }
                        case "비단열지하":
                            {
                                if (Convert.ToDouble(Uvalue) >= 3)
                                { fx_f = 0.45; }
                                else if (Convert.ToDouble(Uvalue) >= 1)
                                { fx_f = 0.75; }
                                else if (Convert.ToDouble(Uvalue) > 0.3)
                                { fx_f = 0.8; }
                                else { fx_f = 0.85; }
                                break;
                            }
                    }
                   
                   
                    Floor floor = new Floor(ZoneF[i][0], ZoneF[i][2], Convert.ToDouble(ZoneF[i][1]), Uvalue, ZoneF[i][5], fx_f);
                    zone1.zoneFloor.Add(floor);
                }
            }
        }
        private void Load_Pre_Win(Zone zone1)
        {
            zone1.zoneWin.Clear();
            String[][] ZoneWin = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.창호열관류율,b.설치열교가산치,b.창호유효열관류율,b.유리면적비,b.상위창호번호,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
            if (ZoneWin.Length > 0)
            {
                int i = -1;
                while (++i < ZoneWin.Length)
                {
                    String[][] ZoneWin_P = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "직접간접,태양열취득률,빛투과율,Type,기존창호,창호명칭", "번호='" + ZoneWin[i][7] + "'");
                    string[][] Blind = Program.DB.getValue(DB.type.ProjDB, "Blind_3D", "차양포함태양열취득률,차양포함빛투과율", "번호='" + ZoneWin[i][0] + "'");
                    if (ZoneWin_P.Length > 0)
                    {
                        double Uvalue = 0;
                        double dU = 0;

                        if (ZoneWin_P[0][3] == "신규")
                        {
                            String[][] Date = Program.DB.getValue(PreProjNum[0][0], "BuildingGeneral", "법규시기,지역구분", "");
                            String[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '창호' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND 직접간접 =  '" + ZoneWin_P[0][0] + "'");
                            if (Value.Length > 0)
                            { Uvalue = Convert.ToDouble(Value[0][0]); }
                            dU = Convert.ToDouble(ZoneWin[i][4]);
                        }
                        else if(ZoneWin_P[0][3] == "기존창호")
                        {
                            String[][] Pre = Program.DB.querySQL(PreProjNum[0][0], "select avg(a.창호열관류율), avg(a.설치열교가산치) From SubWindow as a inner join ConstructionWindow as b on a.상위창호번호 = b.번호 where b.창호명칭 ='" + ZoneWin_P[0][5] + "'");
                            if (Pre.Length > 0)
                            { Uvalue = Convert.ToDouble(Pre[0][0]); }
                        }
                        else
                        {
                            String[][] Pre = Program.DB.querySQL(PreProjNum[0][0], "select avg(a.창호열관류율), avg(a.설치열교가산치) From SubWindow as a inner join ConstructionWindow as b on a.상위창호번호 = b.번호 where b.창호명칭 ='" + ZoneWin_P[0][4] + "'");
                            if (Pre.Length > 0)
                            { Uvalue = Convert.ToDouble(Pre[0][0]); }
                        }

                        if (Blind.Length > 0)
                        {
                            Window win = new Window(ZoneWin[i][0], ZoneWin[i][7], ZoneWin[i][2], Convert.ToDouble(ZoneWin[i][1]), Uvalue, dU, ZoneWin_P[0][0], Convert.ToDouble(ZoneWin[i][6]), Convert.ToDouble(ZoneWin_P[0][1]), Convert.ToDouble(ZoneWin_P[0][2]), Convert.ToDouble(Blind[0][0]), Convert.ToDouble(Blind[0][1]), ZoneWin[i][8], ZoneWin[i][9]);
                            zone1.zoneWin.Add(win);
                        }
                        else
                        {
                            Window win = new Window(ZoneWin[i][0], ZoneWin[i][7], ZoneWin[i][2], Convert.ToDouble(ZoneWin[i][1]), Uvalue, dU, ZoneWin_P[0][0], Convert.ToDouble(ZoneWin[i][6]), Convert.ToDouble(ZoneWin_P[0][1]), Convert.ToDouble(ZoneWin_P[0][2]), 0, 0, ZoneWin[i][8], ZoneWin[i][9]);
                            zone1.zoneWin.Add(win);
                        }
                    }
                }
            }
        }
        private void Load_Pre_CW(Zone zone1)
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
                        String[][] CW_g = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "유리부분열관류율,유리부분유리면적비,태양열취득률,빛투과율,설치열교가산치,Type,기존커튼월,명칭", "번호 = '" + ZoneCW[i][3] + "'");
                        string[][] Blind = Program.DB.getValue(DB.type.ProjDB, "Blind_3D", "차양포함태양열취득률,차양포함빛투과율", "번호='" + ZoneCW[i][3] + "'");
                        if (CW_g.Length > 0)
                        {
                            double Uvalue = 0;
                            double dU = 0;

                            if (CW_g[0][5] == "신규 커튼월창")
                            {
                                String[][] Date = Program.DB.getValue(PreProjNum[0][0], "BuildingGeneral", "법규시기,지역구분", "");
                                String[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '창호' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND  직접간접 =  '직접외기'");
                                if (Value.Length > 0)
                                { Uvalue = Convert.ToDouble(Value[0][0]); }
                                dU = Convert.ToDouble(CW_g[0][4]);
                            }
                            else if(CW_g[0][5] =="기존 커튼월창")
                            {
                                String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionCW", "유리부분열관류율,설치열교가산치", "명칭 ='" + CW_g[0][7] + "'");
                                if (Pre.Length > 0)
                                {
                                    Uvalue = Convert.ToDouble(Pre[0][0]);
                                    dU = Convert.ToDouble(Pre[0][1]);
                                }
                            }
                            else
                            {
                                String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionCW", "유리부분열관류율,설치열교가산치", "명칭 ='" + CW_g[0][6] + "'");
                                if (Pre.Length > 0)
                                { 
                                    if(Pre[0][0]==""|| Pre[0][0]==null || double.IsNaN(Convert.ToDouble(Pre[0][0])))
                                    {
                                        String[][] Date = Program.DB.getValue(PreProjNum[0][0], "BuildingGeneral", "법규시기,지역구분", "");
                                        String[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '창호' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND  직접간접 =  '직접외기'");
                                        if (Value.Length > 0)
                                        { Uvalue = Convert.ToDouble(Value[0][0]); }
                                        dU = Convert.ToDouble(CW_g[0][4]);
                                    }
                                    else
                                    {
                                        Uvalue = Convert.ToDouble(Pre[0][0]); 
                                        dU = Convert.ToDouble(Pre[0][1]);
                                    }
                                }                                    
                            }

                            if (Blind.Length > 0)

                            {
                                CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], Convert.ToDouble(ZoneCW[i][1]), Uvalue, Convert.ToDouble(CW_g[0][1]), Convert.ToDouble(CW_g[0][2]), Convert.ToDouble(Blind[0][0]), Convert.ToDouble(CW_g[0][3]), Convert.ToDouble(Blind[0][1]), 0, 0, 0, 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), dU, ZoneCW[i][4], ZoneCW[i][5], "유리부분");
                                zone1.zoneCW.Add(cw);
                            }
                            else
                            {
                                CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], Convert.ToDouble(ZoneCW[i][1]), Uvalue, Convert.ToDouble(CW_g[0][1]), Convert.ToDouble(CW_g[0][2]), 0, Convert.ToDouble(CW_g[0][3]), 0, 0, 0, 0, 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), dU, ZoneCW[i][4], ZoneCW[i][5], "유리부분");
                                zone1.zoneCW.Add(cw);
                            }
                        }

                    }
                    else if (ZoneCW[i][2] == "패널부분")
                    {
                        String[][] CW_p = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "패널부분열관류율,패널흡수율,설치열교가산치,Type,기존커튼월,명칭", "번호 = '" + ZoneCW[i][3] + "'");
                        if (CW_p.Length > 0)
                        {
                            double Uvalue = 0;
                            double dU = 0;
                            if (CW_p[0][3] == "신규 커튼월창")
                            {
                                String[][] Date = Program.DB.getValue(PreProjNum[0][0], "BuildingGeneral", "법규시기,지역구분", "");
                                String[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '외벽' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND  직접간접 =  '직접외기'");
                                if (Value.Length > 0)
                                { Uvalue = Convert.ToDouble(Value[0][0]); }
                                dU = Convert.ToDouble(CW_p[0][2]);
                            }
                            else if(CW_p[0][3] == "기존 커튼월창")
                            {
                                String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionCW", "패널부분열관류율,설치열교가산치", "명칭 ='" + CW_p[0][5] + "'");
                                if (Pre.Length > 0)
                                {
                                    Uvalue = Convert.ToDouble(Pre[0][0]);
                                    dU = Convert.ToDouble(Pre[0][1]);
                                }
                            }
                            else
                            {
                                String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionCW", "패널부분열관류율,설치열교가산치", "명칭 ='" + CW_p[0][4] + "'");
                                if (Pre.Length > 0)
                                {
                                    if (Pre[0][0] == "" || Pre[0][0] == null || double.IsNaN(Convert.ToDouble(Pre[0][0])))
                                    {
                                        String[][] Date = Program.DB.getValue(PreProjNum[0][0], "BuildingGeneral", "법규시기,지역구분", "");
                                        String[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '외벽' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND  직접간접 =  '직접외기'");
                                        if (Value.Length > 0)
                                        { Uvalue = Convert.ToDouble(Value[0][0]); }
                                        dU = Convert.ToDouble(CW_p[0][2]);
                                    }
                                    else
                                    {
                                        Uvalue = Convert.ToDouble(Pre[0][0]);
                                        dU = Convert.ToDouble(Pre[0][1]);
                                    }
                                }
                            }

                            CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], 0, 0, 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Uvalue, Convert.ToDouble(CW_p[0][1]), 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), dU, ZoneCW[i][4], ZoneCW[i][5], "패널부분");
                            zone1.zoneCW.Add(cw);
                        }
                    }
                    else
                    {
                        String[][] CW_d = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "출입문부분열관류율,출입문부분유리면적비,출입문태양열취득률,출입문빛투과율,설치열교가산치,Type,기존커튼월,명칭", "번호 = '" + ZoneCW[i][3] + "'");
                        if (CW_d.Length > 0)
                        {
                            double Uvalue = 0;
                            double dU = 0;

                            if (CW_d[0][5] == "신규 커튼월창")
                            {
                                String[][] Date = Program.DB.getValue(PreProjNum[0][0], "BuildingGeneral", "법규시기,지역구분", "");
                                String[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '창호' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND  직접간접 =  '직접외기'");
                                if (Value.Length > 0)
                                { Uvalue = Convert.ToDouble(Value[0][0]); }
                                dU = Convert.ToDouble(CW_d[0][4]);
                            }
                            else if (CW_d[0][5] == "기존 커튼월창")
                            {
                                String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionCW", "출입문부분열관류율,설치열교가산치", "명칭 ='" + CW_d[0][7] + "'");
                                if (Pre.Length > 0)
                                {
                                    Uvalue = Convert.ToDouble(Pre[0][0]);
                                    dU = Convert.ToDouble(Pre[0][1]);
                                }
                            }
                            else
                            {
                                String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionCW", "출입문부분열관류율,설치열교가산치", "명칭 ='" + CW_d[0][6] + "'");
                                if (Pre.Length > 0)
                                {
                                    if (Pre[0][0] == "" || Pre[0][0] == null || double.IsNaN(Convert.ToDouble(Pre[0][0])))
                                    {
                                        String[][] Date = Program.DB.getValue(PreProjNum[0][0], "BuildingGeneral", "법규시기,지역구분", "");
                                        String[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '창호' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND  직접간접 =  '직접외기'");
                                        if (Value.Length > 0)
                                        { Uvalue = Convert.ToDouble(Value[0][0]); }
                                        dU = Convert.ToDouble(CW_d[0][4]);
                                    }
                                    else
                                    {
                                        Uvalue = Convert.ToDouble(Pre[0][0]);
                                        dU = Convert.ToDouble(Pre[0][1]);
                                    }
                                }
                            }

                            CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Uvalue, Convert.ToDouble(CW_d[0][1]), Convert.ToDouble(CW_d[0][2]), Convert.ToDouble(CW_d[0][3]), Convert.ToDouble(ZoneCW[i][1]), dU, ZoneCW[i][4], ZoneCW[i][5], "출입문부분");
                            zone1.zoneCW.Add(cw);
                        }
                    }
                }
            }
        }
        private void Load_Pre_Door(Zone zone1)
        {
            zone1.zoneDoor.Clear();
            String[][] ZoneD = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.문유효열관류율,b.흡수율,b.직접간접,a.방위,a.기울기,b.Type,b.기존출입문,b.명칭 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionDoor AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
            if (ZoneD.Length > 0)
            {
                int i = -1;
                while (++i < ZoneD.Length)
                {
                    double Uvalue = 0;

                    if (ZoneD[i][8] == "신규 출입문")
                    {
                        String[][] Date = Program.DB.getValue(PreProjNum[0][0], "BuildingGeneral", "법규시기,지역구분", "");
                        String[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '문' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND  직접간접 =  '" + ZoneD[i][5] + "'");
                        if (Value.Length > 0)
                        { Uvalue = Convert.ToDouble(Value[0][0]); }
                    }
                    else if(ZoneD[i][8] == "기존 출입문")
                    {
                        String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionDoor", "문유효열관류율", "명칭 ='" + ZoneD[i][10] + "'");
                        if (Pre.Length > 0)
                        { Uvalue = Convert.ToDouble(Pre[0][0]); }
                    }
                    else
                    {
                        String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionDoor", "문유효열관류율", "명칭 ='" + ZoneD[i][9] + "'");
                        if (Pre.Length > 0)
                        { Uvalue = Convert.ToDouble(Pre[0][0]); }
                    }
                    Door door = new Door(ZoneD[i][0], ZoneD[i][2], Convert.ToDouble(ZoneD[i][1]),Uvalue, Convert.ToDouble(ZoneD[i][4]), ZoneD[i][5], ZoneD[i][6], ZoneD[i][7]);
                    zone1.zoneDoor.Add(door);
                }
            }
        }

        private void Calc_System_element(string 검토유형)
        {
            if (검토유형 == "공조")
            {
                Cal_AHU_Now(NowProjNum[0][0], 검토유형);
            }
            else if (검토유형 == "기밀+열회수기")
            {
                Cal_HRV_Now(NowProjNum[0][0], 검토유형);
            }
            else { Cal_Qv_Pre(PreProjNum[0][0], 검토유형); }

            if (검토유형 != "난방")
            { Cal_Qfh_Pre(PreProjNum[0][0]); }
            else { Cal_Qfh_Now(NowProjNum[0][0]); }


            if (검토유형 != "냉방")
            { Cal_Qfc_Pre(PreProjNum[0][0]); }
            else { Cal_Qfc_Now(NowProjNum[0][0]); }

            if (검토유형 != "급탕")
            { Cal_Qfw(PreProjNum[0][0]); }
            else { Cal_Qfw(NowProjNum[0][0]); }

            if (검토유형 != "신재생")
            { CALC.RESystemCalc(PreProjNum[0][0]); }
            else
            {
                CALC.RESystemCalc(NowProjNum[0][0]);
            }

            #region 파이널계산
            Final final1;
            if (검토유형 == "난방")
            {
                final1 = new Final(PreProjNum[0][0]);
                final1.Load_Heating_Final(NowProjNum[0][0]);
                final1.Load_Cooling_Final(PreProjNum[0][0]);
                final1.Load_DHW_Final(PreProjNum[0][0]);
                final1.Load_AHU_Final(PreProjNum[0][0]);
                final1.Load_REG_Final(PreProjNum[0][0]);
            }
            else if (검토유형 == "냉방")
            {
                final1 = new Final(PreProjNum[0][0]);
                final1.Load_Heating_Final(PreProjNum[0][0]);
                final1.Load_Cooling_Final(NowProjNum[0][0]);
                final1.Load_DHW_Final(PreProjNum[0][0]);
                final1.Load_AHU_Final(PreProjNum[0][0]);
                final1.Load_REG_Final(PreProjNum[0][0]);
            }
            else if (검토유형 == "기밀+열회수기")
            {
                final1 = new Final(PreProjNum[0][0]);
                final1.Load_Heating_Final(PreProjNum[0][0]);
                final1.Load_Cooling_Final(PreProjNum[0][0]);
                final1.Load_DHW_Final(PreProjNum[0][0]);
                final1.Load_AHU_Final(NowProjNum[0][0]);
                final1.Load_REG_Final(PreProjNum[0][0]);
            }
            else if (검토유형 == "공조")
            {
                final1 = new Final(PreProjNum[0][0]);
                final1.Load_Heating_Final(PreProjNum[0][0]);
                final1.Load_Cooling_Final(PreProjNum[0][0]);
                final1.Load_DHW_Final(PreProjNum[0][0]);
                final1.Load_AHU_Final(NowProjNum[0][0]);
                final1.Load_REG_Final(PreProjNum[0][0]);
            }
            else if (검토유형 == "신재생")
            {
                final1 = new Final(PreProjNum[0][0]);
                final1.Load_Heating_Final(PreProjNum[0][0]);
                final1.Load_Cooling_Final(PreProjNum[0][0]);
                final1.Load_DHW_Final(PreProjNum[0][0]);
                final1.Load_AHU_Final(PreProjNum[0][0]);
                final1.Load_REG_Final(NowProjNum[0][0]);
            }
            else if (검토유형 == "급탕")
            {
                final1 = new Final(PreProjNum[0][0]);
                final1.Load_Heating_Final(PreProjNum[0][0]);
                final1.Load_Cooling_Final(PreProjNum[0][0]);
                final1.Load_DHW_Final(NowProjNum[0][0]);
                final1.Load_AHU_Final(PreProjNum[0][0]);
                final1.Load_REG_Final(PreProjNum[0][0]);
            }
            else
            {
                final1 = new Final(PreProjNum[0][0]);
                final1.Load_Heating_Final(PreProjNum[0][0]);
                final1.Load_Cooling_Final(PreProjNum[0][0]);
                final1.Load_DHW_Final(PreProjNum[0][0]);
                final1.Load_AHU_Final(PreProjNum[0][0]);
                final1.Load_REG_Final(PreProjNum[0][0]);
            }

            final1.Calc_Qtot();

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

            for (int mth = 0; mth < 12; mth++)
            {
                final1.Qf_elec_tot_mth[mth] = final1.Qhf_elec[mth] + final1.Qcf_elec[mth] + final1.Qwf_elec[mth] + final1.Qlf_elec[mth] + final1.Qvf_elec[mth] + final1.Qbase_elec[mth] - final1.Qreg_elec[mth];
                final1.Qf_gas_tot_mth[mth] = final1.Qhf_gas[mth] + final1.Qcf_gas[mth] + final1.Qwf_gas[mth] + final1.Qbase_gas[mth];
            }
            Save_Alt(final1, 검토유형, "FinalEnergy_Result_Element");

            #endregion

        }
        #region 공조
        public void Cal_Qv_Pre(string ProjNum, string 검토유형)
        {
            string[][] Num = Program.DB.getValue(ProjNum, "AHUSystem_Form", "번호,유형");
            if (Num.Length > 0)
            {
                for (int k = 0; k < Num.Length; k++)
                {
                    CALC.AHUs[Num[k][0]] = null;
                }
                int i = -1;
                while (++i < Num.Length)
                {
                    if (Num[i][1] == "공조기")
                    {
                        AHU calc1_AHU1 = new AHU(Num[i][0]);
                        CALC.AHUs[Num[i][0]] = calc1_AHU1;
                        AHU_Load_ZoneData_pre(calc1_AHU1, ProjNum);
                        calc1_AHU1.Load_GeneralData(ProjNum);
                        calc1_AHU1.Load_AHUData(ProjNum);
                        calc1_AHU1.Load_DuctData(ProjNum);
                        calc1_AHU1.Load_PrehPrecData(ProjNum);
                        CALC.AHUSystem_PreCalc(calc1_AHU1);
                    }
                    else
                    {
                        AHU calc1_HRV1 = new AHU(Num[i][0]);
                        CALC.AHUs[Num[i][0]] = calc1_HRV1;
                        AHU_Load_ZoneData_pre(calc1_HRV1, ProjNum);
                        calc1_HRV1.Load_GeneralData(ProjNum);
                        calc1_HRV1.Load_HRVData(ProjNum);
                        calc1_HRV1.Load_DuctData(ProjNum);
                        calc1_HRV1.Load_PrehPrecData(ProjNum);
                        CALC.AHUSystem_PreCalc(calc1_HRV1);
                    }
                }

                Calc_Qb_Element(검토유형);

                i = -1;
                for (int k = 0; k < Num.Length; k++)
                {
                    CALC.AHUs[Num[k][0]] = null;
                }
                while (++i < Num.Length)
                {
                    if (Num[i][1] == "공조기")
                    {
                        AHU calc2_AHU1 = new AHU(Num[i][0]);
                        CALC.AHUs[Num[i][0]] = calc2_AHU1;
                        AHU_Load_ZoneData_pre(calc2_AHU1, ProjNum);
                        calc2_AHU1.Load_GeneralData(ProjNum);
                        calc2_AHU1.Load_AHUData(ProjNum);
                        calc2_AHU1.Load_DuctData(ProjNum);
                        calc2_AHU1.Load_PrehPrecData(ProjNum);
                        CALC.AHUSystem_PostCalc(calc2_AHU1);
                    }
                    else
                    {
                        AHU calc2_HRV1 = new AHU(Num[i][0]);
                        CALC.AHUs[Num[i][0]] = calc2_HRV1;
                        AHU_Load_ZoneData_pre(calc2_HRV1, ProjNum);
                        calc2_HRV1.Load_GeneralData(ProjNum);
                        calc2_HRV1.Load_HRVData(ProjNum);
                        calc2_HRV1.Load_DuctData(ProjNum);
                        calc2_HRV1.Load_PrehPrecData(ProjNum);
                        CALC.HRV_PostCalc(calc2_HRV1);
                    }

                }
            }
        }
        public void Cal_HRV_Now(string ProjNum, string 검토유형)
        {
            string[][] Num = Program.DB.getValue(ProjNum, "AHUSystem_Form", "번호,유형");
            if (Num.Length > 0)
            {
                for (int k = 0; k < Num.Length; k++)
                {
                    CALC.AHUs[Num[k][0]] = null;
                }
                int i = -1;
                while (++i < Num.Length)
                {
                    if (Num[i][1] != "공조기")
                    {
                        AHU Pre_HRV1 = new AHU(Num[i][0]);
                        CALC.AHUs[Num[i][0]] = Pre_HRV1;
                        CALC.HRV_LaodData(Pre_HRV1, ProjNum);
                        CALC.AHUSystem_PreCalc(Pre_HRV1);
                    }
                }

                Calc_Qb_Element(검토유형);

                i = -1;
                for (int k = 0; k < Num.Length; k++)
                {
                    CALC.AHUs[Num[k][0]] = null;
                }
                while (++i < Num.Length)
                {
                    if (Num[i][1] != "공조기")
                    {
                        AHU Post_HRV1 = new AHU(Num[i][0]);
                        CALC.AHUs[Num[i][0]] = Post_HRV1;
                        CALC.HRV_LaodData(Post_HRV1, ProjNum);
                        CALC.HRV_PostCalc(Post_HRV1);
                    }

                }
            }
        }
        public void Cal_AHU_Now(string ProjNum, string 검토유형)
        {
            string[][] Num = Program.DB.getValue(ProjNum, "AHUSystem_Form", "번호,유형");
            if (Num.Length > 0)
            {
                for (int k = 0; k < Num.Length; k++)
                {
                    CALC.AHUs[Num[k][0]] = null;
                }
                int i = -1;
                while (++i < Num.Length)
                {
                    if (Num[i][1] == "공조기")
                    {
                        AHU Pre_AHU1 = new AHU(Num[i][0]);
                        CALC.AHUs[Num[i][0]] = Pre_AHU1;
                        CALC.AHUSystem_LaodData(Pre_AHU1, ProjNum);
                        CALC.AHUSystem_PreCalc(Pre_AHU1);
                    }
                }

                Calc_Qb_Element(검토유형);

                i = -1;
                for (int k = 0; k < Num.Length; k++)
                {
                    CALC.AHUs[Num[k][0]] = null;
                }
                while (++i < Num.Length)
                {
                    if (Num[i][1] == "공조기")
                    {
                        AHU Post_AHU1 = new AHU(Num[i][0]);
                        CALC.AHUs[Num[i][0]] = Post_AHU1;
                        CALC.AHUSystem_LaodData(Post_AHU1, ProjNum);
                        CALC.AHUSystem_PostCalc(Post_AHU1);
                    }

                }
            }
        }
        private void AHU_Load_ZoneData_pre(AHU ahu1, string ProjNum)
        {
            string[][] AHUValue = Program.DB.getValue(ProjNum, "AHUSystem_form", "유형", "번호='" + ahu1.AHUNum + "'");
            if (AHUValue.Length > 0)
            {
                ahu1.AHUOptions = AHUValue[0][0];
            }
            ahu1.SelectZone_split.Clear();

            string[][] value = Program.DB.getValue(ProjNum, "ZoneGeneral_Form", "존번호", "선택열회수기 = '" + ahu1.AHUNum + "'");
            string[][] PostZone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,기존존", "");            
            if (value.Length > 0 && PostZone.Length > 0)
            {
                for (int k = 0; k < value.Length; k++)
                {
                    for(int i =0; i < PostZone.Length; i++)
                    {
                        ArrayList splitzone = new ArrayList();
                        splitzone = Split_(PostZone[i][1]);

                        for(int ii=0; ii<splitzone.Count; ii++)
                        {
                            if (value[k][0] == splitzone[ii].ToString())
                            {
                                ahu1.SelectZone_split.Add(PostZone[i][0]);
                            }
                        }
                    }
                }
            }

            if (ahu1.AHUOptions == "공조기")
            {
                for (int n = 0; n < ahu1.SelectZone_split.Count; n++)
                {
                    string[][] ZoneValue = Program.DB.getValue(ProjNum, "ZoneGeneral_form", "용도프로필,이용일환기량,순바닥면적,공조시간", "존번호='" + ahu1.SelectZone_split[n] + "'");
                    if (ZoneValue.Length > 0)
                    {
                        ahu1.Vmin_tot += Convert.ToDouble(ZoneValue[0][1]);
                        ahu1.ANF_tot += Convert.ToDouble(ZoneValue[0][2]);
                        Zone zone = Program.CALC.getZone(ahu1.SelectZone_split[n].ToString());
                        ahu1.Qh_a_tot += zone.Qb_a[0];
                        ahu1.Qc_a_tot += zone.Qb_a[1];
                        ahu1.Qmax_tot[0] += zone.Q_max[0];
                        ahu1.Qmax_tot[1] += zone.Q_max[1];
                        ahu1.tvmech_avg += Convert.ToDouble(ZoneValue[0][3]) * zone.Qb_a[1];
                        for (int mth = 0; mth < 12; mth++)
                        {
                            ahu1.Qb_mth_tot[0, mth] += zone.Qb_mth[0, 1, mth];
                            ahu1.Qb_mth_tot[1, mth] += zone.Qb_mth[1, 1, mth];
                            ahu1.dvmechmth_avg[mth] += zone.dwd_mth[mth] * zone.Qb_a[1];
                        }
                        string[][] Usage = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "난방설정온도,냉방설정온도,공조운전시부재율,공조냉방부분운전계수", "용도명='" + ZoneValue[0][0] + "'");
                        if (Usage.Length > 0)
                        {
                            ahu1.theta_iset_avg[0] += Convert.ToDouble(Usage[0][0]) * zone.Qb_a[0];
                            ahu1.theta_iset_avg[1] += Convert.ToDouble(Usage[0][1]) * zone.Qb_a[1];
                        }
                    }
                }
                ahu1.theta_iset_avg[0] = ahu1.theta_iset_avg[0] / ahu1.Qh_a_tot;
                ahu1.theta_iset_avg[1] = ahu1.theta_iset_avg[1] / ahu1.Qc_a_tot;
                ahu1.tvmech_avg = ahu1.tvmech_avg / ahu1.Qc_a_tot;
                for (int mth = 0; mth < 12; mth++)
                {
                    ahu1.dvmechmth_avg[mth] = ahu1.dvmechmth_avg[mth] / ahu1.Qc_a_tot;
                }
            }
            else
            {
                for (int n = 0; n < ahu1.SelectZone_split.Count; n++)
                {
                    string[][] ZoneValue = Program.DB.getValue(ProjNum, "ZoneGeneral_form", "용도프로필,이용일환기량,순바닥면적,공조시간,주이용일", "존번호='" + ahu1.SelectZone_split[n] + "'");
                    if (ZoneValue.Length > 0)
                    {
                        ahu1.Vmin_tot += Convert.ToDouble(ZoneValue[0][1]);
                        ahu1.ANF_tot += Convert.ToDouble(ZoneValue[0][2]);
                        ahu1.tvmech_avg += Convert.ToDouble(ZoneValue[0][3]);
                        for (int mth = 0; mth < 12; mth++)
                        {
                            string[][] ValueK;
                            if (ZoneValue[0][4] != "5.5")
                            {
                                ValueK = Program.DB.getValue(DB.type.BaseDB_HCneed, "이용일수", "이용일수", "월='" + (mth + 1) + "월' AND 주간일수 ='주 " + ZoneValue[0][4] + ".0 일 근무'");
                            }
                            else { ValueK = Program.DB.getValue(DB.type.BaseDB_HCneed, "이용일수", "이용일수", "월='" + (mth + 1) + "월' AND 주간일수 ='주 5.5 일 근무'"); }
                            if (ValueK.Length > 0)
                            {
                                ahu1.dvmechmth_avg[mth] += Convert.ToDouble(ValueK[0][0]);
                            }
                        }

                        string[][] Usage = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "난방설정온도,냉방설정온도,공조운전시부재율,공조냉방부분운전계수", "용도명='" + ZoneValue[0][0] + "'");
                        if (Usage.Length > 0)
                        {
                            ahu1.theta_iset_avg[0] += Convert.ToDouble(Usage[0][0]);
                            ahu1.theta_iset_avg[1] += Convert.ToDouble(Usage[0][1]);
                        }

                    }
                }

                ahu1.theta_iset_avg[0] = ahu1.theta_iset_avg[0] / ahu1.SelectZone_split.Count;
                ahu1.theta_iset_avg[1] = ahu1.theta_iset_avg[1] / ahu1.SelectZone_split.Count;
                ahu1.tvmech_avg = ahu1.tvmech_avg / ahu1.SelectZone_split.Count;
                for (int mth = 0; mth < 12; mth++)
                {
                    ahu1.dvmechmth_avg[mth] = ahu1.dvmechmth_avg[mth] / ahu1.SelectZone_split.Count;
                }
            }
        }
        #endregion

        #region 난방
        public void Cal_Qfh_Pre(string ProjNum)
        {
            CALC.Heating_ce_zone_calc_Element(ProjNum);
            string[][] HeatingNum = Program.DB.getValue(ProjNum, "HeatingSystem_Form", "번호");
            if (HeatingNum.Length > 0)
            {
                for (int k = 0; k < HeatingNum.Length; k++)
                {
                    CALC.Heatings[HeatingNum[k][0]] = null;
                }
                int i = -1;
                while (++i < HeatingNum.Length)
                {
                    Heating Heating1 = new Heating(HeatingNum[i][0]);
                    CALC.Heatings[HeatingNum[i][0]] = Heating1;
                    CALC.Heating_LoadData(Heating1, ProjNum);
                    CALC.Heating_Calc(Heating1, ProjNum);
                  //  CALC.Heating_Save(Heating1);
                }
            }
        }
        public void Cal_Qfh_Now(string ProjNum)
        {
            CALC.Heating_ce_zone_calc(ProjNum);
            string[][] HeatingNum = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "번호");
            if (HeatingNum.Length > 0)
            {
                for (int k = 0; k < HeatingNum.Length; k++)
                {
                    CALC.Heatings[HeatingNum[k][0]] = null;
                }
                int i = -1;
                while (++i < HeatingNum.Length)
                {
                    Heating Heating1 = new Heating(HeatingNum[i][0]);
                    CALC.Heatings[HeatingNum[i][0]] = Heating1;
                    CALC.Heating_LoadData(Heating1, ProjNum);
                    CALC.Heating_Calc(Heating1, ProjNum);
                 //   CALC.Heating_Save(Heating1);
                }
            }
        }
        #endregion

        #region 냉방
        public void Cal_Qfc_Pre(string ProjNum)
        {
            CALC.Cooling_ce_zone_calc_Element(ProjNum);
            string[][] CoolingNum = Program.DB.getValue(ProjNum, "CoolingSystem_Form", "번호");
            if (CoolingNum.Length > 0)
            {
                for (int k = 0; k < CoolingNum.Length; k++)
                {
                    CALC.Coolings[CoolingNum[k][0]] = null;
                }
                int i = -1;
                while (++i < CoolingNum.Length)
                {
                    Cal_Cooling Cooling1 = new Cal_Cooling(CoolingNum[i][0]);
                    CALC.Coolings[CoolingNum[i][0]] = Cooling1;
                    CALC.Cooling_LoadData(Cooling1, ProjNum);
                    CALC.Cooling_Calc(Cooling1, ProjNum);
                    //CALC.Cooling_Save(Cooling1);
                }
            }
        }
        public void Cal_Qfc_Now(string ProjNum)
        {
            CALC.Cooling_ce_zone_calc(ProjNum);
            string[][] CoolingNum = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "번호");
            if (CoolingNum.Length > 0)
            {
                for (int k = 0; k < CoolingNum.Length; k++)
                {
                    CALC.Coolings[CoolingNum[k][0]] = null;
                }
                int i = -1;
                while (++i < CoolingNum.Length)
                {
                    Cal_Cooling Cooling1 = new Cal_Cooling(CoolingNum[i][0]);
                    CALC.Coolings[CoolingNum[i][0]] = Cooling1;
                    CALC.Cooling_LoadData(Cooling1, ProjNum);
                    CALC.Cooling_Calc(Cooling1, ProjNum);
                    //CALC.Cooling_Save(Cooling1);
                }
            }
        }
        #endregion
        #region 급탕
        public void Cal_Qfw(string ProjNum)
        {
            string[][] DHWNum = Program.DB.getValue(ProjNum, "DHWSystem_Form", "번호");
            if (DHWNum.Length > 0)
            {
                for (int k = 0; k < DHWNum.Length; k++)
                {
                    CALC.DHWs[DHWNum[k][0]] = null;
                }
                int i = -1;
                while (++i < DHWNum.Length)
                {
                    DHW DHW1 = new DHW(DHWNum[i][0]);
                    CALC.DHWs[DHWNum[i][0]] = DHW1;
                    CALC.DHW_LoadData(DHW1, ProjNum);
                    CALC.DHW_Calc(DHW1, ProjNum);
                }
            }
        }
        #endregion 

        #endregion
        #region 최적안 검토
        public void Calc_Qb_Rule_Optimal(string 검토유형, string 리모델링안, string RemodelingType, double R, double dU)
        {
            CALC.Zone_Arrange();
            for (int k = 0; k < CALC.zone.Count; k++)
            {
                Zone zone1 = (Zone)CALC.zone[k];
                ZoneLight zonelight1 = (ZoneLight)CALC.zonelight[k];
                CALC.Zone_LoadData(zone1, zonelight1);

                switch (검토유형)
                {
                    case "외벽":
                        Load_Optimal_Wall(zone1, RemodelingType, R, dU);
                        break;
                    case "지붕":
                        Load_Rule_Roof(zone1);
                        break;
                    case "최하층바닥":
                        Load_Rule_Floor(zone1);
                        break;
                    case "창호":
                        Load_Rule_Win(zone1);
                        break;
                    case "커튼월창":
                        Load_Rule_CW(zone1);
                        break;
                    case "외부출입문":
                        Load_Rule_Door(zone1);
                        break;
                    case "전체":
                        Load_Rule_All(zone1);
                        break;
                }
                CALC.Zone_Calc(zone1, zonelight1);
            }
        }
        private void Calc_System_Optimal(string 검토유형, string 리모델링안, string RemodelingType, double R, double dU)
        {
            int i;
            #region 공조계산
            string[][] Num = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "번호,유형");
            if (Num.Length > 0)
            {
                for (int k = 0; k < Num.Length; k++)
                {
                    CALC.AHUs[Num[k][0]] = null;
                }
                i = -1;
                while (++i < Num.Length)
                {
                    if (Num[i][1] == "공조기")
                    {
                        AHU Pre_AHU1 = new AHU(Num[i][0]);
                        CALC.AHUs[Num[i][0]] = Pre_AHU1;
                        CALC.AHUSystem_LaodData(Pre_AHU1, NowProjNum[0][0]);
                        CALC.AHUSystem_PreCalc(Pre_AHU1);
                    }
                    else
                    {
                        AHU Pre_HRV1 = new AHU(Num[i][0]);
                        CALC.AHUs[Num[i][0]] = Pre_HRV1;
                        CALC.HRV_LaodData(Pre_HRV1, NowProjNum[0][0]);
                        CALC.AHUSystem_PreCalc(Pre_HRV1);
                    }
                }

                Calc_Qb_Rule_Optimal(검토유형, 리모델링안, RemodelingType, R, dU);

                i = -1;
                for (int k = 0; k < Num.Length; k++)
                {
                    CALC.AHUs[Num[k][0]] = null;
                }
                while (++i < Num.Length)
                {
                    if (Num[i][1] == "공조기")
                    {
                        AHU Post_AHU1 = new AHU(Num[i][0]);
                        CALC.AHUs[Num[i][0]] = Post_AHU1;
                        CALC.AHUSystem_LaodData(Post_AHU1, NowProjNum[0][0]);
                        CALC.AHUSystem_PostCalc(Post_AHU1);
                    }
                    else
                    {
                        AHU Post_HRV1 = new AHU(Num[i][0]);
                        CALC.AHUs[Num[i][0]] = Post_HRV1;
                        CALC.HRV_LaodData(Post_HRV1, NowProjNum[0][0]);
                        CALC.HRV_PostCalc(Post_HRV1);
                    }

                }
            }
            #endregion

            Cal_Qfh_Now(NowProjNum[0][0]);
           // CALC.CoolingSystemCalc();
            Cal_Qfw(NowProjNum[0][0]);

            #region 파이널계산
            Final final1 = new Final(NowProjNum[0][0]);

            final1.Calc_Qtot();
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
                final1.Qf_elec_tot_mth[mth] = final1.Qhf_elec[mth] + final1.Qcf_elec[mth] + final1.Qwf_elec[mth] + final1.Qlf_elec[mth] + final1.Qvf_elec[mth] + final1.Qbase_elec[mth] - final1.Qreg_elec[mth];
                final1.Qf_gas_tot_mth[mth] = final1.Qhf_gas[mth] + final1.Qcf_gas[mth] + final1.Qwf_gas[mth] + final1.Qbase_gas[mth];
            }
            Save_Final_Optimal(final1, 검토유형, 리모델링안);

            #endregion

            CALC.RESystemCalc(NowProjNum[0][0]);
        }
        private void Save_Final_Optimal(Final final1, string 검토유형, string 리모델링안)
        {
            #region 전기
            String MTH;
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            string[][] PNum = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
          
            double Qhf_elec_a = 0, Qcf_elec_a = 0, Qwf_elec_a = 0, Qlf_elec_a = 0, Qvf_elec_a = 0, Qbase_elec_a = 0, Qreg_elec_a = 0, Qf_elec_tot_a = 0;

            for (int mth = 0; mth < 12; mth++)
            {
                Qhf_elec_a += final1.Qhf_elec[mth];
                Qcf_elec_a += final1.Qcf_elec[mth];
                Qwf_elec_a += final1.Qwf_elec[mth];
                Qlf_elec_a += final1.Qlf_elec[mth];
                Qvf_elec_a += final1.Qvf_elec[mth];
                Qbase_elec_a += final1.Qbase_elec[mth];
                Qreg_elec_a += final1.Qreg_elec[mth];
            }
            Qf_elec_tot_a = Qhf_elec_a + Qcf_elec_a + Qwf_elec_a + Qlf_elec_a  + Qvf_elec_a + Qbase_elec_a - Qreg_elec_a;
            Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result_Optimal", "프로젝트번호,프로젝트유형,검토유형,리모델링안,월,연료," +
                "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량",
                "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + 검토유형 + "','" + 리모델링안 + "','" + "연간" + "','" + "전기" + "','" +
                Qhf_elec_a + "','" + Qcf_elec_a  + "','" + Qwf_elec_a  + "','" + Qlf_elec_a + "','" +
                Qvf_elec_a + "','" + Qbase_elec_a  + "','" + Qreg_elec_a + "','" + Qf_elec_tot_a
                + "'", "검토유형,리모델링안,월,연료");
            #endregion

            #region 가스           
            double Qhf_gas_a = 0, Qcf_gas_a = 0, Qwf_gas_a = 0, Qbase_gas_a = 0, Qf_gas_tot_a = 0;
            for (int mth = 0; mth < 12; mth++)
            {
                Qhf_gas_a += final1.Qhf_gas[mth];
                Qcf_gas_a += final1.Qcf_gas[mth];
                Qwf_gas_a += final1.Qwf_gas[mth];
                Qbase_gas_a += final1.Qbase_gas[mth];
            }
            Qf_gas_tot_a = Qhf_gas_a + Qcf_gas_a + Qwf_gas_a + Qbase_gas_a;
            string Carrier = "";
            if (final1.Carrier_h != "" && final1.Carrier_h != null) { Carrier = final1.Carrier_h; } else if (final1.Carrier_w != "" && final1.Carrier_w != null) { Carrier = final1.Carrier_w; } else if (final1.Carrier_c != "" && final1.Carrier_c != null) { Carrier = final1.Carrier_c; }
            if (Carrier == "LNG" || Carrier == "LPG") { Carrier = "가스"; }
            if (Carrier != "")
            {
                Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result_Optimal", "프로젝트번호,프로젝트유형,검토유형,리모델링안,월,연료," +
                  "난방,냉방,급탕,조명,공조,기저에너지,총에너지소요량",
                  "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + 검토유형 + "','" + 리모델링안 + "','" + "연간" + "','" + Carrier + "','" +
                  Qhf_gas_a + "','" + Qcf_gas_a + "','" + Qwf_gas_a + "','" + 0 + "','" +
                  0 + "','" + Qbase_gas_a + "','" + Qf_gas_tot_a
                  + "'", "검토유형,리모델링안,월,연료");
            }
            #endregion
            #region 전체
            Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result_Optimal", "프로젝트번호,프로젝트유형,검토유형,리모델링안,월,연료," +
                   "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량",
                   "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + 검토유형 + "','" + 리모델링안 + "','" + "연간" + "','" + "전체" + "','" +
                   (Qhf_elec_a + Qhf_gas_a) + "','" + (Qcf_elec_a + Qcf_gas_a) + "','" + (Qwf_elec_a + Qwf_gas_a) + "','" + Qlf_elec_a + "','" +
                   Qvf_elec_a + "','" + (Qbase_elec_a + Qbase_gas_a) + "','" + Qreg_elec_a + "','" + (Qf_elec_tot_a + Qf_gas_tot_a)
                   + "'", "검토유형,리모델링안,월,연료");
            #endregion

        }


        #region 외벽
        public void Get_Optimal_WallData(string RemodelingType, string ExType)
        {
            string[][] value = Program.DB.getValue(DB.type.BaseDB_Optimal, "최적안_외벽_인덱스", "구분,외벽유형", "리모델링유형='"+RemodelingType+ "' and 외부마감재대분류 ='"+ExType+"'");
            if(value.Length > 0)
            {
                for(int i =0;  i < value.Length; i++)
                {
                    double R = 0; double dU = 0; 
                    string[][] R_value = Program.DB.getValue(DB.type.BaseDB_Optimal, "최적안_외벽", "열저항합계", "구분='" + value[i][1] + "'");
                    if(R_value.Length > 0)
                    {
                        R= Convert.ToDouble(R_value[0][0]);
                    }
                    Calc_Qb_Rule_Optimal("외벽", value[i][0],RemodelingType,R,dU);
                    Calc_System_Optimal("외벽", value[i][0], RemodelingType, R, dU);
                }
                
            }

        }
        private void Load_Optimal_Wall(Zone zone1, string RemodelingType, double R, double dU)
        {
            zone1.zoneWall.Clear();
            zone1.zoneGWall.Clear();
            String[][] ZoneW = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.열관류율,b.흡수율,b.직접간접,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "' And  NOT b.직접간접 = '지면'");
            if (ZoneW.Length > 0)
            {
                int i = -1;
                while (++i < ZoneW.Length)
                {
                    double Uvalue = 1 / (1 / Convert.ToDouble(ZoneW[0][3]) + R) + dU; 
                    Wall wall = new Wall(ZoneW[i][0], ZoneW[i][2], Convert.ToDouble(ZoneW[i][1]), Uvalue, Convert.ToDouble(ZoneW[i][4]), ZoneW[i][5], ZoneW[i][6], ZoneW[i][7]);
                    zone1.zoneWall.Add(wall);
                }
            }
            String[][] ZoneG = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.열관류율,b.직접간접 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "' And  b.직접간접 = '지면'");
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
                    double Uvalue;
                    if (RemodelingType=="내부덧댐")
                    {
                        Uvalue = 1 / (1 / Convert.ToDouble(ZoneW[0][3]) + R) + dU;
                    }
                    else
                    {
                        Uvalue = Convert.ToDouble(ZoneW[0][3]);
                    }
                    GWall gwall = new GWall(ZoneG[i][0], ZoneG[i][2], Convert.ToDouble(ZoneG[i][1]), Uvalue, fx_f);
                    zone1.zoneGWall.Add(gwall);
                }
            }
        }
        #endregion
        #endregion
    }    

    public class Heating_ce
    {
        string Pre_Zone_ce; String Post_Zone_ce; string HeatingSystemNum_ce; string ceType_ce; string ceNum_ce; string Location_ce; double OperationTime_ce; double ZonePercent_ce;
        public Heating_ce(string Pre_Zone, String Post_Zone, string HeatingSystemNum, string ceType, string ceNum, string Location, double OperationTime, double ZonePercent)
        {
            this.Pre_Zone_ce = Pre_Zone; this.Post_Zone_ce = Post_Zone; this.HeatingSystemNum_ce = HeatingSystemNum; this.ceType_ce= ceType; this.ceNum_ce= ceNum; this.Location_ce= Location; this.OperationTime_ce = OperationTime; this.ZonePercent_ce = ZonePercent;
        }
    }
}
