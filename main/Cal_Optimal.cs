using Eagle._Components.Public;
using Eagle._Constants;
using Eagle._Interfaces.Public;
using main.subcontents.Alt;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace main
{
    internal class Cal_Optimal
    {
        string[][] PreProjNum = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기존프로젝트", "");
        string[][] NowProjNum = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
        string[][] 지역구분 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역구분", "");

        public bool Calc_Optimal_Wall()
        {
            Program.DB.deleteValue(DB.type.ProjDB, "FinalEnergy_Result_Optimal", "검토유형 ='외벽'");
            Program.DB.UseCaches(true);
            string[][] Value = Program.DB.getValue_SameCheck(DB.type.BaseDB_Optimal,"최적안_외벽_인덱스", "구분,외벽유형", "");
            if(Value.Length > 0)
            {
                for(int a= 0; a<Value.Length; a++)
                {
                    string[][] RValue = Program.DB.getValue_SameCheck(DB.type.BaseDB_Optimal, "최적안_외벽", "열저항합계", "구분='" + Value[a][1] +"'");
                    if(RValue.Length > 0)
                    {
                        Calc_Optimal("외벽", Value[a][0], Convert.ToDouble(RValue[0][0]));
                    }
                }
            }
            Program.DB.UseCaches(false);
            return true;
        }
        public void Calc_Optimal(string 검토유형,string 리모델링안, double Optimal_Value)
        {
            Calc_Qb_Optimal(검토유형, 리모델링안, Optimal_Value);
            Calc_System_Optimal(검토유형,리모델링안, Optimal_Value);
        }
        #region 요구량
        public void Calc_Qb_Optimal(string 검토유형, string 리모델링안, double Optimal_Value)
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
                        Load_Optimal_Wall(zone1, 리모델링안, Optimal_Value);
                        break;
                    case "지붕":
                        Load_Optimal_Roof(zone1);
                        break;
                    case "최하층바닥":
                        Load_Optimal_Floor(zone1);
                        break;
                    case "창호":
                        Load_Optimal_Win(zone1);
                        break;
                    case "커튼월창":
                        Load_Optimal_CW(zone1);
                        break;
                    case "외부출입문":
                        Load_Optimal_Door(zone1);
                        break;
                    case "기밀":
                        Load_Optimal_q50(zone1);
                        break;
                    case "기밀+열회수기":
                        Load_Optimal_q50(zone1);
                        Load_Optimal_Ventil(zone1);
                        break;
                    case "조명":
                        Load_Optimal_Light(zonelight1);
                        break;
                }
                CALC.Zone_Calc(zone1, zonelight1);
            }
        }
        private double Get_Wall_Utb(string 리모델링안)
        {
            double dU = 0; double d_Ins = 0; 
            string[][] Value1 = Program.DB.getValue(DB.type.BaseDB_Optimal, "최적안_외벽", "열전도율,두께", "구분='" + 리모델링안 + "'");
            if (Value1.Length > 0)
            {
                for (int aa = 0; aa < Value1.Length; aa++)
                {
                    if (Value1[aa][0] != "" && Convert.ToDouble(Value1[aa][0]) < 0.04)
                    {
                        d_Ins = Convert.ToDouble(Value1[aa][1]);
                    }
                }
            }
            string[][] Value2 = Program.DB.getValue_SameCheck(DB.type.BaseDB_Optimal, "최적안_외벽_인덱스", "열교유형", "외벽유형='" + 리모델링안 + "'");
            if (Value2.Length > 0 && Value2[0][0] != "")
            {
                if (Value2[0][0] == "직접고정" || Value2[0][0] == "트러스(점형)")
                {
                    string[][] TB = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽점형열교", "A,B,C,수직간격,수평간격", "열교유형 ='" + Value2[0][0] + "' and 제품명='단열앙카'");
                    if (TB.Length > 0)
                    {
                        double A = Convert.ToDouble(TB[0][0]);
                        double B = Convert.ToDouble(TB[0][1]);
                        double C = Convert.ToDouble(TB[0][2]);
                        double Kai = (A * Math.Pow(d_Ins, 2) + B * d_Ins + C) / 1000;
                        double PerArea = 0;
                        if (Value2[0][0] == "직접고정")
                        {
                            PerArea = 2 * (Convert.ToDouble(TB[0][3]) / 1000) * (Convert.ToDouble(TB[0][4]) / 1000);
                        }
                        else
                        {
                            PerArea = 1 / (Convert.ToDouble(TB[0][3]) / 1000) / (Convert.ToDouble(TB[0][4]) / 1000);
                        }
                        dU = Kai * PerArea;
                    }
                }
                else
                {
                    string[][] TB = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽선형열교", "A,B,C,수직간격,수평간격", "제품명 = '" + Value2[0][0] + "'");
                    if (TB.Length > 0)
                    {
                        double A = Convert.ToDouble(TB[0][0]);
                        double B = Convert.ToDouble(TB[0][1]);
                        double C = Convert.ToDouble(TB[0][2]);
                        double Psi = (A * Math.Pow(d_Ins, 2) + B * d_Ins + C) / 1000;
                        double PerArea = 0;
                        PerArea = 1 / (Convert.ToDouble(TB[0][3]) / 1000 + Convert.ToDouble(TB[0][4]) / 1000);
                        dU = Psi * PerArea;
                    }
                }
            }
            else { }
            return dU;
        }
        private void Load_Optimal_Wall(Zone zone1,string 리모델링안, double dR)
        {
            double dR_wall = dR; double dU_wall = Get_Wall_Utb(리모델링안);  //지상 외벽 열저항, 추가열교가산치
            double dR_gWall = 0; double dU_gWall = 0;  //지상 외벽 열저항, 추가열교가산치

            string[][] value = Program.DB.getValue(DB.type.BaseDB_Optimal, "최적안_외벽", "리모델링유형", "구분='" + 리모델링안 + "'");
            if(value.Length > 0)
            {
               if(value[0][0] =="내부덧댐")
                {
                    dR_gWall = dR_wall;
                    dU_gWall = dU_wall;
                }
            }

            zone1.zoneWall.Clear();
            zone1.zoneGWall.Clear();
            String[][] ZoneW = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.열관류율,b.흡수율,b.직접간접,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "' And  NOT b.직접간접 = '지면'");
            if (ZoneW.Length > 0)
            {
                int i = -1;
                while (++i < ZoneW.Length)
                {
                    Wall wall = new Wall(ZoneW[i][0], ZoneW[i][2], Convert.ToDouble(ZoneW[i][1]), 1/(1/Convert.ToDouble(ZoneW[i][3]) + dR_wall) +dU_wall, Convert.ToDouble(ZoneW[i][4]), ZoneW[i][5], ZoneW[i][6], ZoneW[i][7]);
                    zone1.zoneWall.Add(wall);
                }
            }
            String[][] ZoneG = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.열관류율,b.직접간접 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "' And  b.직접간접 = '지면'");
            if (ZoneG.Length > 0)
            {
                int i = -1;
                while (++i < ZoneG.Length)
                {
                    double U_gwall = 1 / (1 / Convert.ToDouble(ZoneW[i][3]) + dR_gWall) + dU_gWall; 
                    double fx_f = 1;
                    if (U_gwall >= 3)
                    { fx_f = 0.35; }
                    else if (U_gwall >= 1)
                    { fx_f = 0.55; }
                    else if (U_gwall > 0.3)
                    { fx_f = 0.65; }
                    else { fx_f = 0.75; }
                    break;

                    GWall gwall = new GWall(ZoneG[i][0], ZoneG[i][2], Convert.ToDouble(ZoneG[i][1]), U_gwall, fx_f);
                    zone1.zoneGWall.Add(gwall);
                }
            }
        }
        private void Load_Optimal_Roof(Zone zone1)
        {
            zone1.zoneRoof.Clear();
            String[][] ZoneR = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.법규열관류율,b.흡수율,b.직접간접,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionRoof AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
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
        private void Load_Optimal_Floor(Zone zone1)
        {
            zone1.zoneFloor.Clear();
            String[][] ZoneF = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.법규열관류율,b.직접간접,b.기초설치 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionFloor AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
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
        private void Load_Optimal_Win(Zone zone1)
        {
            zone1.zoneWin.Clear();
            String[][] ZoneWin = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.법규열관류율,b.설치열교가산치,b.창호유효열관류율,b.유리면적비,b.상위창호번호,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
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
        private void Load_Optimal_CW(Zone zone1)
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
        private void Load_Optimal_Door(Zone zone1)
        {
            zone1.zoneDoor.Clear();
            String[][] ZoneD = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.법규열관류율,b.흡수율,b.직접간접,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionDoor AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
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
        private void Load_Optimal_q50(Zone zone1)
        {
            string[] Construction = { "외부출입문", "외벽", "지붕", "창호" };
            for (int a = 0; a < Construction.Length; a++)
            {
                string[][] value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기밀", "기밀시공", "구조체 ='" + Construction[a] + "'");
                if (value.Length > 0)
                {
                    switch (Construction[a])
                    {
                        case "외부출입문":
                            zone1.Door_q50 = Convert.ToDouble(value[0][0]);
                            break;
                        case "외벽":
                            zone1.Wall_q50 = Convert.ToDouble(value[0][0]);
                            break;
                        case "창호":
                            zone1.Win_q50 = Convert.ToDouble(value[0][0]);
                            zone1.CW_q50 = Convert.ToDouble(value[0][0]);
                            break;
                        case "지붕":
                            zone1.Roof_q50 = Convert.ToDouble(value[0][0]);
                            break;
                    }
                }
            }
        }
        private void Load_Optimal_Ventil(Zone zone1)
        {
            //ssk SD-250 참조하여 적용  온도_난방 :95%, 온도_냉방 = 76%, 유효전열 =87%, 유효전열 =57%
            string[][] zoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_form", "환기유무,환기방식,비이용일환기량,이용일환기량", "존번호='" + zone1.ZoneNum + "'");
            zone1.Vmech_SUP_we = Convert.ToDouble(zoneValue[0][2]);
            zone1.Vmech_ETA_we = Convert.ToDouble(zoneValue[0][2]);
            zone1.Vmech_SUP_wd = Convert.ToDouble(zoneValue[0][3]);
            zone1.Vmech_ETA_wd = Convert.ToDouble(zoneValue[0][3]);
            zone1.eta_V_mech[0] = 0.95;
            zone1.eta_V_mech[1] = 0.76;
            zone1.eta_χV_mech[0] = 0.104;
            zone1.eta_χV_mech[1] = 0.714;
            zone1.SelectHRV = "열회수기_법규";
        }
        private void Load_Optimal_Light(ZoneLight zonelight1)
        {
            double LightFL = 64.0 / 70.0; //LED 조명
            double Em = 0, KA = 0, Pj_lx = 0, K = 0, A = 0; string Method = null;

            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "기준조도,조명방식,공간계수", "번호='" + zonelight1.ZoneNum + "'");
            if (Value.Length > 0)
            {
                Em = Convert.ToDouble(Value[0][0]);
                Method = Value[0][1];
                K = Convert.ToDouble(Value[0][2]);
            }

            Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "용도프로필,순바닥면적", "존번호='" + zonelight1.ZoneNum + "'");
            if (Value.Length > 0)
            {
                A = Convert.ToDouble(Value[0][1]);
                string[][] ValueB = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "이용영역계수", "용도명 = '" + Value[0][0] + "'");
                if (ValueB.Length > 0)
                {
                    KA = Convert.ToDouble(ValueB[0][0]);
                }
            }
            Value = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_럭스당조명밀도", "값,UFF", "조명방식='" + Method + "' AND K = '" + K + "'");
            if (Value.Length > 0)
            {
                Pj_lx = Convert.ToDouble(Value[0][0]);
            }

            zonelight1.Pj = Em * KA * LightFL * (0.8 / 0.67) * Pj_lx;
            zonelight1.Pn = zonelight1.Pj * A;
        }
        #endregion
        #region 소요량
        private void Calc_System_Optimal(string 검토유형,string 리모델링안, double Optimal_Value)
        {
            Cal_Qv_Now(NowProjNum[0][0], 검토유형, Optimal_Value);

            Cal_Qfh_Optimal(NowProjNum[0][0], 검토유형);

            Cal_Qfc_Optimal(NowProjNum[0][0], 검토유형);

            Cal_Qfw_Optimal(NowProjNum[0][0], 검토유형);

            CALC.RESystemCalc(NowProjNum[0][0]);

            #region 파이널계산
            Final final1;
            final1 = new Final(NowProjNum[0][0]);
            final1.Load_Heating_Final(NowProjNum[0][0]);
            final1.Load_Cooling_Final(NowProjNum[0][0]);
            final1.Load_DHW_Final(NowProjNum[0][0]);
            final1.Load_AHU_Final(NowProjNum[0][0]);
            final1.Load_REG_Final(NowProjNum[0][0]);
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
            
            Save_Alt(final1, 검토유형,리모델링안, Optimal_Value);

            #endregion

        }
        #region 공조
        public void Cal_Qv_Now(string 검토유형, string 리모델링안, double Optimal_Value)
        {
            string[][] Num = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "번호,유형");
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
                Calc_Qb_Optimal(검토유형,리모델링안,Optimal_Value);
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
        }

        #endregion
        #region 난방      
        public void Cal_Qfh_Optimal(string ProjNum, string 검토유형)
        {
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
                    Heating1.Calc_thrL();
                    Heating1.Calc_beta_ce();
                    Heating1.Calc_Qce(ProjNum);
                    Heating1.Calc_beta_d();
                    Heating1.Calc_Qd(ProjNum);
                    Heating1.Calc_beta_s();
                    Heating1.Calc_Qh_s(ProjNum);
                    Heating1.Calc_beta_gen();
                    Heating1.LoadCalc_Solar(ProjNum);
                    if (검토유형 != "보일러")
                    { Heating1.LoadCalc_Boiler(ProjNum); }
                    else
                    {
                        LoadCalc_Boiler_Heating(Heating1);
                    }
                    if (검토유형 != "냉난방EHP")
                    {
                        Heating1.LoadCalc_AirHP(ProjNum);
                    }
                    else
                    {
                        LoadCalc_AirHP_Heating(Heating1);
                    }
                    Heating1.nan();
                }
            }
        }
        public void LoadCalc_Boiler_Heating(Heating Heating1)
        {
            double Optimal = 90.0;
            for (int n = 0; n < Heating1.SelectBoiler_split.Count; n++)
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "번호,난방급탕,연료,Type,용량,전부하효율,부분부하효율,소비전력,대기전력", "번호 = '" + Heating1.SelectBoiler_split[n] + "'");
                if (Value.Length > 0)
                {
                    String Num = Value[0][0];
                    String Combi = Value[0][1];
                    Heating1.Carrier = Value[0][2];
                    String Type = Value[0][3];
                    double Power = Convert.ToDouble(Value[0][4]) * Convert.ToDouble(Heating1.BoilerNum_split[n]);
                    double eta_Pn = Optimal / 100;
                    double eta_Pint = Optimal / 100;
                    double W = Convert.ToDouble(Value[0][7]);
                    double W_0 = Convert.ToDouble(Value[0][8]);
                    double count = Convert.ToDouble(Heating1.BoilerNum_split[n]);
                    Heating1.Calc_Qh_gen_Boiler(Num, Combi, Type, Power, eta_Pn, eta_Pint, W, W_0, count);
                }
            }
        }
        public void LoadCalc_AirHP_Heating(Heating Heating1)
        {
            double Optimal = 5.5;
            for (int n = 0; n < Heating1.SelectAirHP_split.Count; n++)
            {
                string[][] airHP = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", "번호,연료,공급유형,난방정격용량,난방정격COP,난방정격소비전력,한랭지용량,한랭지COP,한랭지소비전력", "번호 = '" + Heating1.SelectAirHP_split[n] + "'");
                String Num = null;
                Heating1.Carrier = null;
                String SupplyType = null;
                double Pi_nom = 0; //정격용량
                double COP_nom = 0; //정격COP
                double W_nom = 0; //정격소비전력 
                double Pi_15 = 0; //정격용량
                double COP_15 = 0; //정격COP
                double W_15 = 0; //정격소비전력 
                if (airHP.Length > 0)
                {
                    Num = airHP[0][0];
                    Heating1.Carrier = airHP[0][1];
                    SupplyType = airHP[0][2];
                    Pi_nom = Convert.ToDouble(airHP[0][3]) * Convert.ToDouble(Heating1.AirHPNum_split[n]); ; //정격용량
                    COP_nom = Optimal; //정격COP
                    W_nom = Pi_nom / COP_nom;
                    Pi_15 = Convert.ToDouble(airHP[0][6]) * Convert.ToDouble(Heating1.AirHPNum_split[n]); //정격용량
                    COP_15 = Optimal; //정격COP
                    W_15 = Pi_15 / COP_15;
                    Heating1.Calc_Q_Air_HP(Num, SupplyType, Pi_nom, COP_nom, W_nom, Pi_15, COP_15, W_15);
                }
            }
        }

        #endregion
        #region 냉방       
        public void Cal_Qfc_Optimal(string ProjNum, string 검토유형)
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
                    Cooling1.Cooling_Generator(ProjNum);
                    Cooling1.Generator_Sum();
                    Cooling1.Cal_CLRate();
                    Cooling1.Cal_ZoneAhu(ProjNum);
                    Cooling1.Cal_Zone(ProjNum);
                    Cooling1.Cal_Ahu();
                    Cooling1.Find_Climate();
                    Cooling1.Cal_Load();
                    if (검토유형 == "수냉식냉동기" && Cooling1.CG == "수냉식냉동기")
                    {
                        Cal_CS(Cooling1);
                    }
                    else if (검토유형 == "공냉식냉동기" && Cooling1.CG == "공냉식냉동기")
                    {
                        if (Cooling1.SelectCG.Count > 0)
                        {
                            string[][] Value = Program.DB.getValue(ProjNum, "User_AirCooler", "번호", "번호 = '" + Cooling1.SelectCG[0] + "'");
                            if (Value.Length > 0)
                            {
                                Cal_CS(Cooling1);
                            }
                            else { Cooling1.Cal_CS(); }
                        }
                    }
                    else if (검토유형 == "냉난방EHP" && (Cooling1.CG == "실외기12kW" || Cooling1.CG == "공냉식냉동기"))
                    {
                        if (Cooling1.SelectCG.Count > 0)
                        {
                            string[][] Value = Program.DB.getValue(ProjNum, "User_AirHP", "난방냉방", "번호 = '" + Cooling1.SelectCG[0] + "'");
                            if (Value.Length > 0)
                            {
                                if (Value[0][0] == "냉난방")
                                { Cal_CS(Cooling1); }
                                else { Cooling1.Cal_CS(); }
                            }
                            else { Cooling1.Cal_CS(); }
                        }

                    }
                    else if (검토유형 == "냉방EHP" && (Cooling1.CG == "실외기12kW" || Cooling1.CG == "공냉식냉동기"))
                    {
                        if (Cooling1.SelectCG.Count > 0)
                        {
                            string[][] Value = Program.DB.getValue(ProjNum, "User_AirHP", "난방냉방", "번호 = '" + Cooling1.SelectCG[0] + "'");
                            if (Value.Length > 0)
                            {
                                if (Value[0][0] == "냉방")
                                {
                                    Cal_CS(Cooling1);
                                }
                                else { Cooling1.Cal_CS(); }
                            }
                            else { Cooling1.Cal_CS(); }
                        }

                    }
                    else { Cooling1.Cal_CS(); }
                    Cooling1.Cal_AuxSum(ProjNum);
                }
            }
        }

        public void Cal_CS(Cal_Cooling Cooling1)
        {
            double Optimal = 5.5;
            Cooling1.Cal_feerCorr();
            Cooling1.Cal_MultiFactor(); //fC_M 작성

            //저장제어운영계수중 운영계수 반영
            string[][] value = Program.DB.getValue(DB.type.BaseDB_Cooling, "저장제어운영계수", "운영계수", " 항목= '" + Cooling1.Sto_Tank + "' And 종류 = '" + Cooling1.Sto_Type + "' And 번호='" + Cooling1.ArtNumber + "'");
            if (value.Length > 0)
            {
                Cooling1.fSP = Convert.ToDouble(value[0][0]);
            }
            else Cooling1.fSP = 1;

            double a = 0;
            for (int i = 0; i < 12; i++)
            {
                Cooling1.EER_c[i] = Optimal * Cooling1.feer_corr[i];
                Cooling1.SEER_c[i] = Cooling1.EER_c[i] * Cooling1.fC_PL[i] * Cooling1.fC_M * Cooling1.fSP;

                if (Cooling1.SEER_c[i] == 0)
                {
                    Cooling1.QC_f[i] = 0;

                }
                else Cooling1.QC_f[i] = Cooling1.QC_out[i] / Cooling1.SEER_c[i];
                a += Cooling1.QC_f[i];
            }
            a = a;
        }

        #endregion
        #region 급탕
        public void Cal_Qfw_Optimal(string ProjNum, string 검토유형)
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
                    DHW dhw1 = new DHW(DHWNum[i][0]);
                    CALC.DHWs[DHWNum[i][0]] = dhw1;
                    CALC.DHW_LoadData(dhw1, ProjNum);
                    dhw1.Calc_Qd(ProjNum);
                    dhw1.Calc_Qh_s(ProjNum);
                    dhw1.LoadCalc_Solar(ProjNum);

                    if (검토유형 != "보일러")
                    { dhw1.LoadCalc_Boiler(ProjNum); }
                    else { LoadCalc_Boiler_DHW(dhw1); }

                    dhw1.LoadCalc_HP(ProjNum);
                    dhw1.nan();
                }
            }
        }
        public void LoadCalc_Boiler_DHW(DHW dhw1)
        {
            double Optimal = 90.0 / 100.0;
            for (int n = 0; n < dhw1.SelectBoiler_split.Count; n++)
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "번호,난방급탕,연료,Type,용량,전부하효율,부분부하효율,소비전력,대기전력", "번호 = '" + dhw1.SelectBoiler_split[n] + "'");
                if (Value.Length > 0)
                {
                    String Num = Value[0][0];
                    String Combi = Value[0][1];
                    dhw1.Carrier = Value[0][2];
                    String Type = Value[0][3];
                    double Power = Convert.ToDouble(Value[0][4]);
                    double eta_Pn = Optimal;
                    double eta_Pint = Optimal;
                    double W = Convert.ToDouble(Value[0][7]);
                    double W_0 = Convert.ToDouble(Value[0][8]);
                    double count = Convert.ToDouble(dhw1.BoilerNum_split[n]);
                    dhw1.Calc_Qh_gen_Boiler(Num, Combi, Type, Power, eta_Pn, eta_Pint, W, W_0, count);
                }
            }
        }
        public void LoadCalc_AirHP_DHW(DHW dhw1)
        {
            double Optimal = 5.5;
            for (int k = 0; k < dhw1.SelectHP_split.Count; k++)
            {
                double Pi_gen_combi_corr; double Pi_gen_sng_corr; double COPw_sng_corr; double COPw_combi_corr;
                string[][] value = Program.DB.getValue(DB.type.ProjDB, "User_DHWHP", "급탕정격용량,급탕정격COP", "번호='" + dhw1.SelectHP_split[0] + "'");
                if (value.Length > 0)
                {
                    Pi_gen_combi_corr = Convert.ToDouble(value[0][0]);
                    Pi_gen_sng_corr = Convert.ToDouble(value[0][0]);
                    COPw_sng_corr = Convert.ToDouble(value[0][1]);
                    COPw_combi_corr = Convert.ToDouble(value[0][1]);
                    dhw1.Carrier = "전기";
                    dhw1.Calc_HP(Pi_gen_combi_corr, Pi_gen_sng_corr, COPw_sng_corr, COPw_combi_corr);
                }
            }
        }

        #endregion
        #region 파이널
        public void Save_Alt(Final final1, string 검토유형,string 리모델링안,double dR)
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
            Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result_Optimal", "프로젝트번호,프로젝트유형,검토유형,리모델링안,리모델링값,월,연료," +
                    "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량",
                    "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + 검토유형 + "','" + 리모델링안 + "','" + dR.ToString() + "','" + "연간" + "','" + "전기" + "','" +
                    Qhf_elec_a + "','" + Qcf_elec_a + "','" + Qwf_elec_a + "','" + Qlf_elec_a + "','" +
                    Qvf_elec_a + "','" + Qbase_elec_a + "','" + Qreg_elec_a + "','" + Qf_elec_tot_a
                    + "'", "검토유형,리모델링안,월,연료");
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
            Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result_Optimal", "프로젝트번호,프로젝트유형,검토유형,리모델링안,리모델링값,월,연료," +
                    "난방,냉방,급탕,조명,공조,기저에너지,총에너지소요량",
                    "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + 검토유형 + "','" + 리모델링안 + "','" + dR.ToString() + "','" + "연간" + "','" + Carrier + "','" +
                    Qhf_gas_a + "','" + Qcf_gas_a + "','" + Qwf_gas_a + "','" + "0" + "','" +
                    "0" + "','" + Qbase_gas_a + "','" + Qf_gas_tot_a
                    + "'", "검토유형,리모델링안,월,연료");
            #endregion
            #region 전체           

            Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result_Optimal", "프로젝트번호,프로젝트유형,검토유형,리모델링안,리모델링값,월,연료," +
                   "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량",
                   "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + 검토유형 + "','" + 리모델링안 + "','" + dR.ToString() + "','" + "연간" + "','" + "전체" + "','" +
                   (Qhf_elec_a + Qhf_gas_a) + "','" + (Qcf_elec_a + Qcf_gas_a) + "','" + (Qwf_elec_a + Qwf_gas_a) + "','" + Qlf_elec_a + "','" +
                   Qvf_elec_a + "','" + (Qbase_elec_a + Qbase_gas_a) + "','" + Qreg_elec_a + "','" + (Qf_elec_tot_a + Qf_gas_tot_a)
                   + "'", "검토유형,리모델링안,월,연료");
            #endregion
        }
        #endregion 
        #endregion
    }
}
