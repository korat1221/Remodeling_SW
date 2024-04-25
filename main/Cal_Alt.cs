using Eagle._Constants;
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
        public ArrayList zone = new ArrayList();
        public ArrayList zonelight = new ArrayList();
        public ArrayList PrePostZone_Heating_ce = new ArrayList();
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
            #region 전기
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
            double Qhf_elec_a = 0, Qcf_elec_a = 0, Qwf_elec_a = 0, Qlf_elec_a = 0, Qvf_elec_a = 0, Qbase_elec_a = 0, Qf_elec_tot_a = 0;

            for (int mth = 0; mth < 12; mth++)
            {
                Qhf_elec_a += final1.Qhf_elec[mth];
                Qcf_elec_a += final1.Qcf_elec[mth];
                Qwf_elec_a += final1.Qwf_elec[mth];
                Qlf_elec_a += final1.Qlf_elec[mth];
                Qvf_elec_a += final1.Qvf_elec[mth];
                Qbase_elec_a += final1.Qbase_elec[mth];
            }
            Qf_elec_tot_a = Qhf_elec_a + Qcf_elec_a + Qwf_elec_a + Qlf_elec_a + Qvf_elec_a + Qbase_elec_a;
            Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result_Alt", "프로젝트번호,프로젝트유형,검토유형,번호,월,연료," +
                     "난방,냉방,급탕,조명,공조,기저에너지,총에너지소요량",
                     "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + 검토유형 + "','" + PNum[0][0] + "','" + "연간" + "','" + "전기" + "','" +
                     Qhf_elec_a + "','" + Qcf_elec_a + "','" + Qwf_elec_a + "','" + Qlf_elec_a + "','" +
                     Qvf_elec_a + "','" + Qbase_elec_a + "','" + Qf_elec_tot_a
                     + "'", "검토유형,번호,월,연료");
            #endregion

            #region 가스
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
            double Qhf_gas_a = 0, Qcf_gas_a = 0, Qwf_gas_a = 0, Qbase_gas_a = 0, Qf_gas_tot_a = 0;
            for (int mth = 0; mth < 12; mth++)
            {
                Qhf_gas_a += final1.Qhf_gas[mth];
                Qcf_gas_a += final1.Qcf_gas[mth];
                Qwf_gas_a += final1.Qwf_gas[mth];
                Qbase_gas_a += final1.Qbase_gas[mth];
            }
            Qf_gas_tot_a = Qhf_gas_a + Qcf_gas_a + Qwf_gas_a + Qbase_gas_a;
            Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result_Alt", "프로젝트번호,프로젝트유형,검토유형,번호,월,연료," +
                     "난방,냉방,급탕,조명,공조,기저에너지,총에너지소요량",
                     "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + 검토유형 + "','" + PNum[0][0] + "','" + "연간" + "','" + Carrier + "','" +
                     Qhf_gas_a + "','" + Qcf_gas_a + "','" + Qwf_elec_a + "','" + "0" + "','" +
                     "0" + "','" + Qbase_gas_a + "','" + Qf_gas_tot_a
                     + "'", "검토유형,번호,월,연료");
            #endregion
            #region 전체
            for (int mth = 0; mth <= 11; mth++)
            {
                MTH = (mth + 1).ToString() + "월";
                Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result_Alt", "프로젝트번호,프로젝트유형,검토유형,번호,월,연료," +
                    "난방,냉방,급탕,조명,공조,기저에너지,총에너지소요량",
                    "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + 검토유형 + "','" + PNum[0][0] + "','" + MTH + "','" + "전체" + "','" +
                    (final1.Qhf_elec[mth] + final1.Qhf_gas[mth]) + "','" + (final1.Qcf_elec[mth] + final1.Qcf_gas[mth]) + "','" + (final1.Qwf_elec[mth] + final1.Qwf_gas[mth]) + "','" + final1.Qlf_elec[mth] + "','" +
                    final1.Qvf_elec[mth] + "','" + (final1.Qbase_elec[mth] + final1.Qbase_gas[mth]) + "','" + (final1.Qf_elec_tot_mth[mth] + final1.Qf_gas_tot_mth[mth])
                    + "'", "검토유형,번호,월,연료");
            }

            Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result_Alt", "프로젝트번호,프로젝트유형,검토유형,번호,월,연료," +
                   "난방,냉방,급탕,조명,공조,기저에너지,총에너지소요량",
                   "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + 검토유형 + "','" + PNum[0][0] + "','" + "연간" + "','" + "전체" + "','" +
                   (Qhf_elec_a + Qhf_gas_a) + "','" + (Qcf_elec_a + Qcf_gas_a) + "','" + (Qwf_elec_a + Qwf_elec_a) + "','" + Qlf_elec_a + "','" +
                   Qvf_elec_a + "','" + (Qbase_elec_a + Qbase_gas_a) + "','" + (Qf_elec_tot_a + Qf_gas_tot_a)
                   + "'", "검토유형,번호,월,연료");
            #endregion

        }


        public void Calc_Alt(string 검토유형) 
        {
            if (검토유형 == "법규_외벽" || 검토유형 == "법규_지붕" || 검토유형 == "법규_최하층바닥" || 검토유형 == "법규_창호" || 검토유형 == "법규_커튼월창" || 검토유형 == "법규_외부출입문" || 검토유형 == "법규_전체")
            {
                Calc_Qb_Rule_Alt(검토유형);
                Calc_System_Rule(검토유형);
            }
            else
            {
                Calc_Qb_Element_Alt(검토유형);
                Calc_System_element(검토유형);
            }           
        }


        #region 법규기반 검토
        public void Calc_Qb_Rule_Alt(string 검토유형)
        {
            CALC.Zone_Arrange();
            for (int k = 0; k < CALC.zone.Count; k++)
            {
                Zone zone1 = (Zone)CALC.zone[k];
                ZoneLight zonelight1 = (ZoneLight)CALC.zonelight[k];
                CALC.Zone_LoadData(zone1, zonelight1, NowProjNum[0][0]);

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
                CALC.Zone_Calc(zone1, zonelight1, NowProjNum[0][0]);
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
            CALC.CoolingSystemCalc();
            CALC.Cal_Qfw();

            #region 파이널계산
            Final final1 = new Final(NowProjNum[0][0]);
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

            #endregion

            CALC.RESystemCalc();
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

        #region 요소기술별 검토
        public void Calc_Qb_Element_Alt(string 검토유형)
        {
            CALC.Zone_Arrange();
            for (int k = 0; k < CALC.zone.Count; k++)
            {
                Zone zone1 = (Zone)CALC.zone[k];
                ZoneLight zonelight1 = (ZoneLight)CALC.zonelight[k];
                ArrayList split = new ArrayList();
                string[][] value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "기존존", "존번호='" + zone1.ZoneNum + "'");
                if(value.Length >0 )
                {
                    split = Split_Zone(value[0][0]);
                }
                for (int i = 0; i < split.Count; i++)
                {
                    PostZone[split[i].ToString()] = value[0][0];
                }
                switch (검토유형)
                {
                    case "조닝":
                        zonelight1.LoadData_LightGeneral();
                        zonelight1.LoadData_LightSystem();
                        zonelight1.LoadData_NaturalLight();
                        zonelight1.LoadData_Renew();
                        zone1.LoadData_ZoneGeneral();
                        zone1.LoadData_q50(PreProjNum[0][0]);
                        zone1.LoadData_InWall();
                        zone1.LoadData_SL();
                        for (int i = 0; i < split.Count; i++)
                        {
                            zone1.LoadData_Ventil(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Wall(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Roof(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Floor(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_GWall(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Door(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Win(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_CW(PreProjNum[0][0], split[i].ToString());
                        }
                        Calc_Zone_element(zone1, zonelight1, PreProjNum[0][0], PreProjNum[0][0]);
                        break;
                    case "요소기술_기밀":
                        zonelight1.LoadData_LightGeneral();
                        zonelight1.LoadData_LightSystem();
                        zonelight1.LoadData_NaturalLight();
                        zonelight1.LoadData_Renew();
                        zone1.LoadData_ZoneGeneral();
                        zone1.LoadData_q50(NowProjNum[0][0]);
                        zone1.LoadData_InWall();
                        zone1.LoadData_SL();
                        for (int i = 0; i < split.Count; i++)
                        {
                            zone1.LoadData_Ventil(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Wall(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Roof(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Floor(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_GWall(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Door(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Win(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_CW(PreProjNum[0][0], split[i].ToString());
                        }
                        Calc_Zone_element(zone1, zonelight1, PreProjNum[0][0], PreProjNum[0][0]);
                        break;
                    case "요소기술_열회수기":
                        zonelight1.LoadData_LightGeneral();
                        zonelight1.LoadData_LightSystem();
                        zonelight1.LoadData_NaturalLight();
                        zonelight1.LoadData_Renew();
                        zone1.LoadData_ZoneGeneral();
                        zone1.LoadData_q50(PreProjNum[0][0]);
                        zone1.LoadData_InWall();
                        zone1.LoadData_SL();
                        zone1.LoadData_Ventil(NowProjNum[0][0], zone1.ZoneNum);
                        for (int i = 0; i < split.Count; i++)
                        {
                            zone1.LoadData_Wall(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Roof(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Floor(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_GWall(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Door(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Win(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_CW(PreProjNum[0][0], split[i].ToString());
                        }
                        Calc_Zone_element(zone1, zonelight1, PreProjNum[0][0], PreProjNum[0][0]);
                        break;
                    case "요소기술_외벽":
                        zonelight1.LoadData_LightGeneral();
                        zonelight1.LoadData_LightSystem();
                        zonelight1.LoadData_NaturalLight();
                        zonelight1.LoadData_Renew();
                        zone1.LoadData_ZoneGeneral();
                        zone1.LoadData_q50(PreProjNum[0][0]);
                        zone1.LoadData_InWall();
                        zone1.LoadData_SL();
                        for (int i =0; i < split.Count; i++)
                        {
                            zone1.LoadData_Ventil(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Wall(NowProjNum[0][0], zone1.ZoneNum);
                            zone1.LoadData_Roof(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Floor(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_GWall(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Door(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Win(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_CW(PreProjNum[0][0], split[i].ToString());
                        }
                        Calc_Zone_element(zone1, zonelight1, PreProjNum[0][0], PreProjNum[0][0]);
                        break;
                    case "요소기술_지붕":
                        zonelight1.LoadData_LightGeneral();
                        zonelight1.LoadData_LightSystem();
                        zonelight1.LoadData_NaturalLight();
                        zonelight1.LoadData_Renew();
                        zone1.LoadData_ZoneGeneral();
                        zone1.LoadData_q50(PreProjNum[0][0]);
                        zone1.LoadData_InWall();
                        zone1.LoadData_SL();
                        for (int i = 0; i < split.Count; i++)
                        {
                            zone1.LoadData_Ventil(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Wall(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Roof(NowProjNum[0][0], zone1.ZoneNum);
                            zone1.LoadData_Floor(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_GWall(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Door(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Win(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_CW(PreProjNum[0][0], split[i].ToString());
                        }
                        Calc_Zone_element(zone1, zonelight1, PreProjNum[0][0], PreProjNum[0][0]);
                        break;
                    case "요소기술_최하층바닥":
                        zonelight1.LoadData_LightGeneral();
                        zonelight1.LoadData_LightSystem();
                        zonelight1.LoadData_NaturalLight();
                        zonelight1.LoadData_Renew();
                        zone1.LoadData_ZoneGeneral();
                        zone1.LoadData_q50(PreProjNum[0][0]);
                        zone1.LoadData_InWall();
                        zone1.LoadData_SL();
                        for (int i = 0; i < split.Count; i++)
                        {
                            zone1.LoadData_Ventil(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Wall(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Roof(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Floor(NowProjNum[0][0], zone1.ZoneNum);
                            zone1.LoadData_GWall(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Door(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Win(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_CW(PreProjNum[0][0], split[i].ToString());
                        }
                        Calc_Zone_element(zone1, zonelight1, PreProjNum[0][0], PreProjNum[0][0]);
                        break;
                    case "요소기술_외부출입문":
                        zonelight1.LoadData_LightGeneral();
                        zonelight1.LoadData_LightSystem();
                        zonelight1.LoadData_NaturalLight();
                        zonelight1.LoadData_Renew();
                        zone1.LoadData_ZoneGeneral();
                        zone1.LoadData_q50(PreProjNum[0][0]);
                        zone1.LoadData_InWall();
                        zone1.LoadData_SL();
                        for (int i = 0; i < split.Count; i++)
                        {
                            zone1.LoadData_Ventil(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Wall(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Roof(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Floor(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_GWall(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Door(NowProjNum[0][0], zone1.ZoneNum);
                            zone1.LoadData_Win(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_CW(PreProjNum[0][0], split[i].ToString());
                        }
                        Calc_Zone_element(zone1, zonelight1, PreProjNum[0][0], PreProjNum[0][0]);
                        break;
                    case "요소기술_창호":
                        zonelight1.LoadData_LightGeneral();
                        zonelight1.LoadData_LightSystem();
                        zonelight1.LoadData_NaturalLight();
                        zonelight1.LoadData_Renew();
                        zone1.LoadData_ZoneGeneral();
                        zone1.LoadData_q50(PreProjNum[0][0]);
                        zone1.LoadData_InWall();
                        zone1.LoadData_SL();
                        for (int i = 0; i < split.Count; i++)
                        {
                            zone1.LoadData_Ventil(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Wall(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Roof(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Floor(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_GWall(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Door(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Win(NowProjNum[0][0], zone1.ZoneNum);
                            zone1.LoadData_CW(PreProjNum[0][0], split[i].ToString());
                        }
                        Calc_Zone_element(zone1, zonelight1, NowProjNum[0][0], PreProjNum[0][0]);
                        break;
                    case "요소기술_커튼월창":
                        zonelight1.LoadData_LightGeneral();
                        zonelight1.LoadData_LightSystem();
                        zonelight1.LoadData_NaturalLight();
                        zonelight1.LoadData_Renew();
                        zone1.LoadData_ZoneGeneral();
                        zone1.LoadData_q50(PreProjNum[0][0]);
                        zone1.LoadData_InWall();
                        zone1.LoadData_SL();
                        for (int i = 0; i < split.Count; i++)
                        {
                            zone1.LoadData_Ventil(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Wall(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Roof(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Floor(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_GWall(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Door(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Win(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_CW(NowProjNum[0][0], zone1.ZoneNum);
                        }
                        Calc_Zone_element(zone1, zonelight1, PreProjNum[0][0], NowProjNum[0][0]);
                        break;
                    case "요소기술_난방":
                        zonelight1.LoadData_LightGeneral();
                        zonelight1.LoadData_LightSystem();
                        zonelight1.LoadData_NaturalLight();
                        zonelight1.LoadData_Renew();
                        zone1.LoadData_ZoneGeneral();
                        zone1.LoadData_q50(PreProjNum[0][0]);
                        zone1.LoadData_InWall();
                        zone1.LoadData_SL();
                        for (int i = 0; i < split.Count; i++)
                        {
                            zone1.LoadData_Ventil(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Wall(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Roof(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Floor(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_GWall(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Door(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Win(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_CW(PreProjNum[0][0], split[i].ToString());
                        }
                        Calc_Zone_element(zone1, zonelight1, PreProjNum[0][0], PreProjNum[0][0]);
                        break;
                    case "요소기술_공조":
                        zonelight1.LoadData_LightGeneral();
                        zonelight1.LoadData_LightSystem();
                        zonelight1.LoadData_NaturalLight();
                        zonelight1.LoadData_Renew();
                        zone1.LoadData_ZoneGeneral();
                        zone1.LoadData_q50(PreProjNum[0][0]);
                        zone1.LoadData_InWall();
                        zone1.LoadData_SL();
                        for (int i = 0; i < split.Count; i++)
                        {
                            zone1.LoadData_Ventil(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Wall(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Roof(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Floor(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_GWall(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Door(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_Win(PreProjNum[0][0], split[i].ToString());
                            zone1.LoadData_CW(PreProjNum[0][0], split[i].ToString());
                        }
                        Calc_Zone_element(zone1, zonelight1, PreProjNum[0][0], PreProjNum[0][0]);
                        break;
                };
            }
        }
        private void Calc_System_element(string 검토유형)
        {
            int i;
            if (검토유형 == "요소기술_공조")
            {
                Cal_AHU_Now(NowProjNum[0][0], 검토유형); 
            }
            else if (검토유형 == "요소기술_열회수기")
            {
                Cal_HRV_Now(NowProjNum[0][0], 검토유형);
            }
            else { Cal_Qv_Pre(NowProjNum[0][0], 검토유형); }

            if (검토유형 != "요소기술_난방")
            { Cal_Qfh_Pre(PreProjNum[0][0]); }
            else { Cal_Qfh_Now(NowProjNum[0][0]); }


            CALC.CoolingSystemCalc();
            CALC.Cal_Qfw();

            #region 파이널계산
            Final final1;
            if (검토유형 != "요소기술_난방")
            {final1 = new Final(PreProjNum[0][0]); }
            else  { 
                final1 = new Final(NowProjNum[0][0]);}
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

            #endregion

            CALC.RESystemCalc();
        }
        public void Calc_Zone_element(Zone zone1, ZoneLight zonelight1, string Win_proj, string CW_proj)
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
            zone1.Zone_n50();
            zone1.ZoneHV();
            zone1.Zonetao();
            zone1.Zonethetai();
            zone1.ZoneQT_u();
            zone1.ZoneQT();
            zone1.ZoneQV();
            zone1.ZoneQSop();
            zone1.ZoneQStr_Win(Win_proj);
            zone1.ZoneQStr_CW(CW_proj);
            zone1.ZoneQ_DHU();
            zone1.ZoneQI_L();
            zone1.ZoneQI();
            zone1.Zone_Theta_U();
            zone1.Zoneeta();
            zone1.ZoneQb();
            zone1.ZoneQmax();
        }       
        private ArrayList Split_Zone(String nonSplit)
        {
            ArrayList split = new ArrayList();
            if (nonSplit != null)
            {
                if (nonSplit.Contains(','))
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
        private Dictionary<string, string> PostZone = new Dictionary<string, string>();
        public string getPost(string PreZoneNum)
        {
            if (PostZone.ContainsKey(PreZoneNum))
            {
                return PostZone[PreZoneNum];
            }
            else return null;
        }

        #region 공조
        public void Cal_Qv_Pre(string ProjNum, string 검토유형 )
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
                        AHU_Load_ZoneData(calc1_AHU1,ProjNum);
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
                        AHU_Load_ZoneData(calc1_HRV1,ProjNum);
                        calc1_HRV1.Load_GeneralData(ProjNum);
                        calc1_HRV1.Load_HRVData(ProjNum);
                        calc1_HRV1.Load_DuctData(ProjNum);
                        calc1_HRV1.Load_PrehPrecData(ProjNum);
                        CALC.AHUSystem_PreCalc(calc1_HRV1);
                    }
                }

                Calc_Qb_Element_Alt(검토유형);

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
                        AHU_Load_ZoneData(calc2_AHU1,ProjNum);
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
                        AHU_Load_ZoneData(calc2_HRV1,ProjNum);
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
                    if (Num[i][1]!= "공조기")
                    {
                        AHU Pre_HRV1 = new AHU(Num[i][0]);
                        CALC.AHUs[Num[i][0]] = Pre_HRV1;
                        CALC.HRV_LaodData(Pre_HRV1, ProjNum);
                        CALC.AHUSystem_PreCalc(Pre_HRV1);
                    }
                }

                Calc_Qb_Element_Alt(검토유형);

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
                        CALC.AHUSystem_LaodData(Pre_AHU1,ProjNum);
                        CALC.AHUSystem_PreCalc(Pre_AHU1);
                    }
                }

                Calc_Qb_Element_Alt(검토유형);

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
        private void AHU_Load_ZoneData(AHU ahu1, string ProjNum)
        {
            string[][] AHUValue = Program.DB.getValue(ProjNum, "AHUSystem_form", "유형", "번호='" + ahu1.AHUNum + "'");
            if (AHUValue.Length > 0)
            {
                ahu1.AHUOptions = AHUValue[0][0];
            }
            ahu1.SelectZone_split.Clear();           

            string[][] value = Program.DB.getValue(ProjNum, "ZoneGeneral_Form", "존번호", "선택열회수기 = '" + ahu1.AHUNum + "'");
            if (value.Length > 0)
            {
                for (int k = 0; k < value.Length; k++)
                {
                    string NowZone = getPost(value[k][0]);
                    ahu1.SelectZone_split.Add(NowZone);
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
            Heating_ce_zone_calc(ProjNum);
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
                    Heating heating1 = new Heating(HeatingNum[i][0]);
                    CALC.Heatings[HeatingNum[i][0]] = heating1;

                    Heating_Load_Zonedata(heating1,ProjNum);
                    heating1.Load_HeatingGeneral(ProjNum);
                    heating1.Load_Boiler(ProjNum);
                    heating1.Load_Solar(ProjNum);
                    heating1.Load_PumpData(ProjNum);
                    heating1.Load_ceData(ProjNum);
                    heating1.Load_StorageData(ProjNum);
                    heating1.Load_PipeData(ProjNum);
                    heating1.Load_AirHP(ProjNum);
                    heating1.Load_GroundHP(ProjNum);
                    heating1.Load_GWHP(ProjNum);
                    Heating_Load_ce(heating1,ProjNum);

                    heating1.Calc_thrL();
                    heating1.Calc_beta_ce();
                    Heating_Calc_Qce(heating1,ProjNum);
                    heating1.Calc_beta_d();
                    heating1.Calc_Qd(ProjNum);
                    heating1.Calc_beta_s();
                    heating1.Calc_Qh_s(ProjNum);
                    heating1.Calc_beta_gen();
                    heating1.Calc_Qh_gen_Boiler(ProjNum);
                    heating1.Calc_Solar(ProjNum);
                    heating1.Calc_Q_Air_HP(ProjNum);
                    heating1.nan();
                }
            }
        }
        public void Cal_Qfh_Now(string ProjNum)
        {
            CALC.Heating_ce_zone_calc();
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
                }
            }
        }
        public void Heating_ce_zone_calc(string ProjNum)
        {
            string[][] HeatingNum = Program.DB.getValue(ProjNum, "HeatingSystem_Form", "번호");
            int i = -1;
            String MTH;
            string[][] Zone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,기존존", "냉난방유무 ='냉난방' OR 냉난방유무 = '난방'");
            PrePostZone_Heating_ce.Clear();
            while (++i < HeatingNum.Length)
            {
                if (Zone.Length > 0)
                {
                    for (int n = 0; n < Zone.Length; n++)
                    {
                        ArrayList split = new ArrayList();
                        split = Split_Zone(Zone[n][1]);                    
                        Zone zone = Program.CALC.getZone(Zone[n][0].ToString());

                        for(int k=0; k<split.Count; k++)
                        {
                            string[][] ce = Program.DB.getValue(ProjNum, "Heating_ce_Form", "공급설비,공급설비종류,가동시간,난방시스템", "존번호 = '" + split[k] + "'");

                            double[] 가동비율 = new double[ce.Length];
                            double 가동비율_tot = 0;
                           
                            for (int a = 0; a < ce.Length; a++)
                            {
                                string[][] ce2 = Program.DB.getValue(ProjNum, "User_ce", "용량_난방", "번호='" + ce[a][0].Substring(0, 4) + "'");
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
                                if (HeatingNum[i][0] == ce[a][3])
                                {
                                    PrePostZone_HeatingCE zone_heatingce = new PrePostZone_HeatingCE(split[k].ToString(), Zone[n][0], HeatingNum[i][0], ce[a][0], ce[a][1], Qhb_mth);
                                    PrePostZone_Heating_ce.Add(zone_heatingce);
                                }
                            }

                        }                        
                    }
                }
            }
        }
        private void Heating_Load_Zonedata(Heating heating1, string ProjNum)
        {
            double[,] Qhb_mth; double[,] theta_ih; double[,] th; double[,] dop_mth; double[] th_op_day; double[] Qh_max; double[] Qh_a; double[] theta_i_h_set;
            int count=0; 
            heating1.SelectZone_split.Clear();
            for (int k=0; k<PrePostZone_Heating_ce.Count; k++)
            {
                PrePostZone_HeatingCE zone_heatingce =(PrePostZone_HeatingCE)PrePostZone_Heating_ce[k];
                if(zone_heatingce.HeatingNum == heating1.HeatingNum)
                {
                    heating1.SelectZone_split.Add(zone_heatingce.PostZoneNum);
                    count += 1;
                }
            }
            Qh_max = new double[heating1.SelectZone_split.Count];
            for (int k = 0; k < heating1.SelectZone_split.Count; k++)
            {
                Zone zone = Program.CALC.getZone(heating1.SelectZone_split[k].ToString());
                Qh_max[k] = zone.Q_max[0];//최대부하 
                heating1.Qh_max_sum += Qh_max[k];
            }
                Qhb_mth = new double[count, 12];
                theta_ih = new double[count, 12];
                th = new double[count, 12];
                Qh_a = new double[count];
                dop_mth = new double[count, 12];
                th_op_day = new double[count];
                theta_i_h_set = new double[count];

                for (int n = 0; n < count; n++)
                {
                    for (int k = 0; k < PrePostZone_Heating_ce.Count; k++)
                    {
                        PrePostZone_HeatingCE zone_heatingce = (PrePostZone_HeatingCE)PrePostZone_Heating_ce[k];
                        if (zone_heatingce.HeatingNum == heating1.HeatingNum)
                        {
                            Zone zone = Program.CALC.getZone(zone_heatingce.PostZoneNum);
                            if (zone != null)
                            {
                                for (int mth = 0; mth < 12; mth++)
                                {
                                    Qhb_mth[n, mth] = zone_heatingce.Qb_mth[mth];
                                    theta_ih[n, mth] = zone.theta_i[1, 0, mth]; //이용일 난방
                                    th[n, mth] = zone.t_max[0, mth]; // 난방 시간 
                                    Qh_a[n] = zone.Qb_a[0]; //연간 난방요구량
                                    dop_mth[n, mth] = zone.dwd_mth[mth];
                                    th_op_day[n] = zone.th_op_d;
                                    theta_i_h_set[n] = zone.theta_i_h_set;
                                }
                            }
                        }
                    }               
                    
                }
               
                for (int n = 0; n < count; n++)
                {
                    heating1.Qh_a_sum += Qh_a[n];
                    //요구량 가중
                    heating1.th_op_day_avg += (th_op_day[n] * Qh_a[n]);
                    heating1.theta_i_h_set_avg += (theta_i_h_set[n] * Qh_a[n]);
                }
                heating1.th_op_day_avg = heating1.th_op_day_avg / heating1.Qh_a_sum;
                heating1.theta_i_h_set_avg = heating1.theta_i_h_set_avg / heating1.Qh_a_sum;

                for (int mth = 0; mth < 12; mth++)
                {
                    for (int n = 0; n < count; n++)
                    {
                        heating1.Qhb_mth_sum[mth] += Qhb_mth[n, mth];
                        //요구량 가중
                        heating1.theta_ih_avg[mth] += (theta_ih[n, mth] * Qh_a[n]);
                        heating1.th_avg[mth] += (th[n, mth] * Qh_a[n]);
                        heating1.dop_mth_avg[mth] += (dop_mth[n, mth] * Qh_a[n]);
                    }
                    heating1.theta_ih_avg[mth] = heating1.theta_ih_avg[mth] / heating1.Qh_a_sum;
                    heating1.th_avg[mth] = heating1.th_avg[mth] / heating1.Qh_a_sum;
                    heating1.dop_mth_avg[mth] = heating1.dop_mth_avg[mth] / heating1.Qh_a_sum;
                }
        }
        private void Heating_Load_ce(Heating heating1, string ProjNum)
        {
            heating1.ce_Type1.Clear();
            for (int k = 0; k < PrePostZone_Heating_ce.Count; k++)
            {
                PrePostZone_HeatingCE zone_heatingce = (PrePostZone_HeatingCE)PrePostZone_Heating_ce[k];
                if (zone_heatingce.HeatingNum == heating1.HeatingNum && zone_heatingce.ceType == heating1.ce1Type)
                {
                    String Num, ce_ZoneNum, ceSystemNum, ceType, Location, Control;
                    double theta;
                    Num = zone_heatingce.CENum;
                    ce_ZoneNum = zone_heatingce.PostZoneNum;
                    ceSystemNum = zone_heatingce.CENum.Substring(0, zone_heatingce.CENum.IndexOf("_"));
                    ceType = heating1.ce1Type;
                    string[][] Value = Program.DB.getValue(ProjNum, "Heating_ce_Form", "설치위치", "난방시스템 = '" + heating1.HeatingNum + "' And 공급설비종류 = '" + heating1.ce1Type + "' and 존번호='"+zone_heatingce.PreZoneNum+"'");
                    Location = Value[0][0];
                    string[][] 일람표정보 = Program.DB.getValue(ProjNum, "User_ce", "온도제어방식", "번호 = '" + ceSystemNum + "'");
                    Control = 일람표정보[0][0];
                    theta = heating1.Calc_theta_ce(ceType, heating1.SLRL, Location, Control);
                    heating1.dtheta_ce1 = theta;
                    CE ce = new CE(Num, ce_ZoneNum, ceSystemNum, ceType, Location, Control, theta);
                    heating1.ce_Type1.Add(ce);
                }

                if (zone_heatingce.HeatingNum == heating1.HeatingNum && zone_heatingce.ceType == heating1.ce2Type)
                {
                    String Num, ce_ZoneNum, ceSystemNum, ceType, Location, Control;
                    double theta;
                    Num = zone_heatingce.CENum;
                    ce_ZoneNum = zone_heatingce.PostZoneNum;
                    ceSystemNum = zone_heatingce.CENum.Substring(0, zone_heatingce.CENum.IndexOf("_"));
                    ceType = heating1.ce2Type;
                    string[][] Value = Program.DB.getValue(ProjNum, "Heating_ce_Form", "설치위치", "난방시스템 = '" + heating1.HeatingNum + "' And 공급설비종류 = '" + heating1.ce2Type + "' and 존번호='" + zone_heatingce.PreZoneNum + "'");
                    Location = Value[0][0];
                    string[][] 일람표정보 = Program.DB.getValue(ProjNum, "User_ce", "온도제어방식", "번호 = '" + ceSystemNum + "'");
                    Control = 일람표정보[0][0];
                    theta = heating1.Calc_theta_ce(ceType, heating1.SLRL, Location, Control);
                    heating1.dtheta_ce2 = theta;
                    CE ce = new CE(Num, ce_ZoneNum, ceSystemNum, ceType, Location, Control, theta);
                    heating1.ce_Type2.Add(ce);
                }
            }
        }
        private void Heating_Calc_Qce(Heating heating1, string ProjNum)
        {
            for (int n = 0; n < heating1.ce_Type1.Count; n++)
            {
                CE ce = (CE)heating1.ce_Type1[n];
                Zone zone = Program.CALC.getZone(ce.ZoneNum());
                for (int k = 0; k < PrePostZone_Heating_ce.Count; k++)
                {
                    PrePostZone_HeatingCE zone_heatingce = (PrePostZone_HeatingCE)PrePostZone_Heating_ce[k];
                    if (zone_heatingce.HeatingNum == heating1.HeatingNum && zone_heatingce.CENum == ce.Num() && zone_heatingce.PostZoneNum == ce.ZoneNum())
                    {
                        for (int mth = 0; mth < 12; mth++)
                        {

                            heating1.Qh_ce[mth] += Math.Max(zone_heatingce.Qb_mth[mth] * ce.theta_ce() / (zone.theta_i[0, 1, mth] - heating1.theta_e[mth]), 0);
                            if (double.IsNaN(heating1.Qh_ce[mth]))
                            {
                                heating1.Qh_ce[mth] = 0;
                            }
                            string[][] Value2 = Program.DB.getValue(ProjNum, "User_ce", "소비전력_난방", "번호 = '" + ce.ceNum() + "'");
                            if (Value2.Length > 0)
                            {
                                heating1.Wh_ce[mth] += 0;
                                if (double.IsNaN(heating1.Wh_ce[mth]))
                                {
                                    heating1.Wh_ce[mth] = 0;
                                }
                            }
                        }
                    }
                }
            }
            for (int n = 0; n < heating1.ce_Type2.Count; n++)
            {
                CE ce = (CE)heating1.ce_Type2[n];
                Zone zone = Program.CALC.getZone(ce.ZoneNum());
                for (int k = 0; k < PrePostZone_Heating_ce.Count; k++)
                {
                    PrePostZone_HeatingCE zone_heatingce = (PrePostZone_HeatingCE)PrePostZone_Heating_ce[k];
                    if (zone_heatingce.HeatingNum == heating1.HeatingNum && zone_heatingce.CENum == ce.Num() && zone_heatingce.PostZoneNum == ce.ZoneNum())
                    {
                        for (int mth = 0; mth < 12; mth++)
                        {

                            heating1.Qh_ce[mth] += Math.Max(zone_heatingce.Qb_mth[mth] * ce.theta_ce() / (zone.theta_i[0, 1, mth] - heating1.theta_e[mth]), 0);
                            if (double.IsNaN(heating1.Qh_ce[mth]))
                            {
                                heating1.Qh_ce[mth] = 0;
                            }
                            string[][] Value2 = Program.DB.getValue(ProjNum, "User_ce", "소비전력_난방", "번호 = '" + ce.ceNum() + "'");
                            if (Value2.Length > 0)
                            {
                                heating1.Wh_ce[mth] += 0;
                                if (double.IsNaN(heating1.Wh_ce[mth]))
                                {
                                    heating1.Wh_ce[mth] = 0;
                                }
                            }
                        }
                    }
                }
            }

        }
        #endregion

        #endregion
    }
    public class PrePostZone_HeatingCE
    {
        public string ID;
        public string PreZoneNum, PostZoneNum, HeatingNum, CENum,ceType;
        public double[] Qb_mth = new double[12];
        public PrePostZone_HeatingCE(string PreZoneNum, string PostZoneNum, string HeatingNum, string CENum,string ceType, double[] Qb_mth)
        {
            this.PreZoneNum = PreZoneNum;
            this.PostZoneNum = PostZoneNum;
            this.HeatingNum = HeatingNum;
            this.CENum = CENum;
            this.ceType = ceType;
            for (int mth = 0; mth < 12; mth++)
            {
                this.Qb_mth[mth] = Qb_mth[mth];
            }
        }
    }
}
