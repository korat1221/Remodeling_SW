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
        string[][] 지역구분;
        String[] HC = { "난방", "냉방" };
        String[] WEWD = { "비이용일", "이용일" };
        String[] MTH = { "1월", "2월", "3월", "4월", "5월", "6월", "7월", "8월", "9월", "10월", "11월", "12월" };

        private void CreateZone()
        {
            string[][] zones = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,냉난방유무");
            String[,] zones_arr = new String[zones.Length, 2];//존번호, 냉난방유무
            String[] zones_순번 = new String[zones.Length];// 계산 순서대로 존번호
            int N_비냉난방 = 0, N_난방 = 0, N_냉방 = 0, N_냉난방 = 0; //순번 카운팅 
            int T_비냉난방 = 0, T_난방 = 0, T_냉방 = 0, T_냉난방 = 0; //총계 카운팅 
            try
            {
                지역구분 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역구분", "");
            }
            catch { }
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
                //  Zone zone1 = new Zone("1F_Zone001");
                Zone zone1 = new Zone(zones_순번[i]);
                zone.Add(zone1);
            }
        }
        public void Calc_Alt_Wall()
        {
            CreateZone();
            for (int k = 0; k < zone.Count; k++)
            {
                Zone zone1 = (Zone)zone[k];
                zone1.검토유형[0][0] = "법규_외벽";
                zone1.LoadData_ZoneGeneral();
                zone1.LoadData_Ventil();
                zone1.LoadData_InWall();
                zone1.LoadData_SL();
                zone1.LoadData_Roof();
                zone1.LoadData_Floor();

                try
                {
                    String[][] ZoneW = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.유효열관류율,b.흡수율,b.직접간접,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "' And  NOT b.직접간접 = '지면'");
                    // string[][] ZoneW = Program.DB.querySQL(DB.type.ProjDB, "ZoneWall", "Area,Ueff,α,DirectInDirect", "ZoneNum='" + ZoneNum + "'");
                    int i = -1;
                    while (++i < ZoneW.Length)
                    {
                        String DiIndi_;

                        if (ZoneW[i][5] == "직접외기" || ZoneW[i][5] == "간접외기")
                        {
                            DiIndi_ = ZoneW[i][5];
                        }
                        else
                        {
                            DiIndi_ = "간접외기";
                        }
                        string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율", "구조체 = '외벽' And 시기 = '2018.09' AND  지역 ='" + 지역구분[0][0] + "'  AND 직접간접 =  '" + DiIndi_ + "'");
                        Wall wall = new Wall(ZoneW[i][0], ZoneW[i][2], Convert.ToDouble(ZoneW[i][1]), Convert.ToDouble(Value[0][0]), Convert.ToDouble(ZoneW[i][4]), ZoneW[i][5], ZoneW[i][6], ZoneW[i][7]);
                        zone1.zoneWall.Add(wall);
                    }
                }
                catch { }

                
                try
                {
                    String[][] ZoneG = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.유효열관류율,b.직접간접 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "' And  b.직접간접 = '지면'");
                    //string[][] ZoneG = Program.DB.getValue(DB.type.ProjDB, "ZoneGWall", "Area,Ueff", "ZoneNum='" + ZoneNum + "'");
                    int i = -1;
                    while (++i < ZoneG.Length)
                    {
                        String DiIndi_;

                        if (ZoneG[i][4] == "직접외기" || ZoneG[i][4] == "간접외기")
                        {
                            DiIndi_ = ZoneG[i][5];
                        }
                        else
                        {
                            DiIndi_ = "간접외기";
                        }
                        string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '외벽' And 시기 = '2018.09' AND  지역 ='" + 지역구분[0][0] + "'  AND 직접간접 =  '" + DiIndi_ + "'");
                        double fx_f = 1;
                        if (Convert.ToDouble(Value[0][0]) >= 3)
                        { fx_f = 0.35; }
                        else if (Convert.ToDouble(Value[0][0]) >= 1)
                        { fx_f = 0.55; }
                        else if (Convert.ToDouble(Value[0][0]) > 0.3)
                        { fx_f = 0.65; }
                        else { fx_f = 0.75; }
                        break;

                        GWall gwall = new GWall(ZoneG[i][0], ZoneG[i][2], Convert.ToDouble(ZoneG[i][1]), Convert.ToDouble(Value[0][0]), fx_f);
                        zone1.zoneGWall.Add(gwall);
                    }
                }
                catch { }
                zone1.LoadData_Door();
                zone1.LoadData_Win();
                zone1.LoadData_CW();
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
                                  "'" + zone1.검토유형[0][0] + "','" + zone1.ZoneNum+ "','" + zone1.zoneName + "','" +
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
            zone.Clear();
        }
        public void Calc_Alt_Roof()
        {
            zone.Clear();
            CreateZone();
            for (int k = 0; k < zone.Count; k++)
            {
                Zone zone1 = (Zone)zone[k];
                zone1.검토유형[0][0] = "법규_지붕";
                zone1.LoadData_ZoneGeneral();
                zone1.LoadData_Ventil();
                zone1.LoadData_InWall();
                zone1.LoadData_SL();
                zone1.LoadData_Wall();

                try
                {
                    String[][] ZoneR = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.유효열관류율,b.흡수율,b.직접간접,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionRoof AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
                    // string[][] ZoneR = Program.DB.getValue(DB.type.ProjDB, "ZoneRoof", "Area,Ueff,α,DirectInDirect", "ZoneNum='" + ZoneNum + "'");
                    int i = -1;
                    while (++i < ZoneR.Length)
                    {
                        string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율", "구조체 = '외벽' And 시기 = '2018.09' AND  지역 ='" + 지역구분[0][0] + "'  AND 직접간접 =  '" + ZoneR[i][5] + "'");
                        Roof roof = new Roof(ZoneR[i][0], ZoneR[i][2], Convert.ToDouble(ZoneR[i][1]), Convert.ToDouble(Value[0][0]), Convert.ToDouble(ZoneR[i][4]), ZoneR[i][5], ZoneR[i][6], ZoneR[i][7]);
                        zone1.zoneRoof.Add(roof);
                    }
                }
                catch { }
                zone1.LoadData_Floor();
                zone1.LoadData_GWall();
                zone1.LoadData_Door();
                zone1.LoadData_Win();
                zone1.LoadData_CW();
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
            zone.Clear();
        }
        public void Calc_Alt_Floor()
        {
            CreateZone();
            for (int k = 0; k < zone.Count; k++)
            {
                Zone zone1 = (Zone)zone[k];
                zone1.검토유형[0][0] = "법규_최하층바닥";
                zone1.LoadData_ZoneGeneral();
                zone1.LoadData_Ventil();
                zone1.LoadData_InWall();
                zone1.LoadData_SL();
                zone1.LoadData_Wall();
                zone1.LoadData_Roof();
                try
                {
                    String[][] ZoneF = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.유효열관류율,b.직접간접,b.기초설치 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionFloor AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
                   

                    int i = -1;
                    while (++i < ZoneF.Length)
                    {
                        double fx_f = 1;
                        String DiIndi_;
                        if (ZoneF[i][4] == "직접외기" || ZoneF[0][4] == "간접외기")
                        {
                            DiIndi_ = ZoneF[0][4];
                        }
                        else
                        {
                            DiIndi_ = "간접외기";
                        }
                        string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '바닥' And 시기 = '2018.09' AND  지역 ='" + 지역구분[0][0] + "'  AND 직접간접 =  '" + DiIndi_ + "'");
                        switch (ZoneF[i][5].ToString())
                        {
                            case "지면위":
                                {
                                    if (Convert.ToDouble(Value[0][0]) >= 3)
                                    { fx_f = 0.3; }
                                    else if (Convert.ToDouble(Value[0][0]) >= 1)
                                    { fx_f = 0.55; }
                                    else if (Convert.ToDouble(Value[0][0]) > 0.3)
                                    { fx_f = 0.7; }
                                    else { fx_f = 0.8; }
                                    break;
                                }
                            case "단열지하":
                                {
                                    if (Convert.ToDouble(Value[0][0]) >= 3)
                                    { fx_f = 0.2; }
                                    else if (Convert.ToDouble(Value[0][0]) >= 1)
                                    { fx_f = 0.45; }
                                    else if (Convert.ToDouble(Value[0][0]) > 0.3)
                                    { fx_f = 0.55; }
                                    else { fx_f = 0.7; }
                                    break;
                                }
                            case "비단열지하":
                                {
                                    if (Convert.ToDouble(Value[0][0]) >= 3)
                                    { fx_f = 0.45; }
                                    else if (Convert.ToDouble(Value[0][0]) >= 1)
                                    { fx_f = 0.75; }
                                    else if (Convert.ToDouble(Value[0][0]) > 0.3)
                                    { fx_f = 0.8; }
                                    else { fx_f = 0.85; }
                                    break;
                                }
                        }

                        Floor floor = new Floor(ZoneF[i][0], ZoneF[i][2], Convert.ToDouble(ZoneF[i][1]), Convert.ToDouble(Value[0][0]), ZoneF[i][5], fx_f);
                       zone1.zoneFloor.Add(floor);
                    }
                }
                catch { }
                zone1.LoadData_GWall();
                zone1.LoadData_Door();
                zone1.LoadData_Win();
                zone1.LoadData_CW();
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
            zone.Clear();
        }
        public void Calc_Alt_Win()
        {
            CreateZone();
            for (int k = 0; k < zone.Count; k++)
            {
                Zone zone1 = (Zone)zone[k];
                zone1.검토유형[0][0] = "법규_창호";
                zone1.LoadData_ZoneGeneral();
                zone1.LoadData_Ventil();
                zone1.LoadData_InWall();
                zone1.LoadData_SL();
                zone1.LoadData_Wall();
                zone1.LoadData_Roof();
                zone1.LoadData_Floor();
                zone1.LoadData_GWall();
                zone1.LoadData_Door(); 
                
                try
                {
                    String[][] ZoneWin = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.창호열관류율,b.설치열교가산치,b.창호유효열관류율,b.유리면적비,b.상위창호번호,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
                    //string[][] ZoneWin = Program.DB.getValue(DB.type.ProjDB, "ZoneWin", "Area,Uvalue,Uinst,DirectInDirect,Ff,g,τ,gtot,τtot", "ZoneNum='" + ZoneNum + "'");
                    int i = -1;
                    while (++i < ZoneWin.Length)
                    {
                        String[][] ZoneWin_P = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "직접간접,태양열취득률,빛투과율", "번호='" + ZoneWin[i][7] + "'");
                        string[][] Blind = Program.DB.getValue(DB.type.ProjDB, "Blind_3D", "차양포함태양열취득률,차양포함빛투과율", "번호='" + ZoneWin[i][0] + "'");
                        String[][] Uvalue = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '창호' And 시기 = '2018.09' AND  지역 ='" + 지역구분[0][0] + "'  AND 직접간접 =  '" + ZoneWin_P[i][0] + "'");
                        Window win = new Window(ZoneWin[i][0], ZoneWin[i][7], ZoneWin[i][2], Convert.ToDouble(ZoneWin[i][1]), Convert.ToDouble(Uvalue[0][0]), Convert.ToDouble(ZoneWin[i][4]), ZoneWin_P[0][0], Convert.ToDouble(ZoneWin[i][6]), Convert.ToDouble(ZoneWin_P[0][1]), Convert.ToDouble(ZoneWin_P[0][2]), Convert.ToDouble(Blind[0][0]), Convert.ToDouble(Blind[0][1]), ZoneWin[i][8], ZoneWin[i][9]);
                       zone1. zoneWin.Add(win);
                        //나중에 차양포함 태양열취득률, 빛투과율 반영해야 함
                    }
                }
                catch { }

                zone1.LoadData_CW();
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
            zone.Clear();
        }
        public void Calc_Alt_CW()
        {
            CreateZone();
            for (int k = 0; k < zone.Count; k++)
            {
                Zone zone1 = (Zone)zone[k];
                zone1.검토유형[0][0] = "법규_커튼월창";
                zone1.LoadData_ZoneGeneral();
                zone1.LoadData_Ventil();
                zone1.LoadData_InWall();
                zone1.LoadData_SL();
                zone1.LoadData_Wall();
                zone1.LoadData_Floor();
                zone1.LoadData_GWall();
                zone1.LoadData_Door();
                zone1.LoadData_Win(); 
                try
                {
                    String[][] ZoneCW = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,면적,커튼월부위,구조체번호,방위,기울기.", "존 = '" + zone1.ZoneNum + "' AND 외피유형 = '커튼월창'");
                    // string[][] ZoneCW = Program.DB.getValue(DB.type.ProjDB, "ZoneCW", "Area_g,Uvalue_g,Ff_g,g_g,gtot_g,τ_g,τtot_g,Area_p,Uvalue_p,α_p,Area_d,Uvalue_d,Ff_d,g_d,τ_d,Area_tot,Uinst", "ZoneNum='" + ZoneNum + "'");
                    int i = -1;
                    while (++i < ZoneCW.Length)
                    { //유리부분면적,유리부분열관류율,유리부분유리면적비,태양열취득률,빛투과율, 패널부분 면적, 패널부분흡수율, 출입문부분면적, 출입문부분열관류율,출입문부분유리면적비, 출입문부분태양열취득률, 출입문부분빛투과율, 커튼월창면적, 설치열교가산치 

                        string[][] Uvalue = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '창호' And 시기 = '2018.09' AND  지역 ='" + 지역구분[0][0] + "'  AND 직접간접 =  '직접외기'");
                        string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율", "구조체 = '외벽' And 시기 = '2018.09' AND  지역 ='" + 지역구분[0][0] + "'  AND 직접간접 =  '직접외기'");
                        if (ZoneCW[i][2] == "유리부분")
                        {
                            String[][] CW_g = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "유리부분열관류율,유리부분유리면적비,태양열취득률,빛투과율,설치열교가산치", "번호 = '" + ZoneCW[i][3] + "'");
                            string[][] Blind = Program.DB.getValue(DB.type.ProjDB, "Blind_3D", "차양포함태양열취득률,차양포함빛투과율", "번호='" + ZoneCW[i][3] + "'");
                            CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(Uvalue[0][0]), Convert.ToDouble(CW_g[i][1]), Convert.ToDouble(CW_g[i][2]), Convert.ToDouble(Blind[0][0]), Convert.ToDouble(CW_g[i][3]), Convert.ToDouble(Blind[0][1]), 0, 0, 0, 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_g[i][4]), ZoneCW[i][4], ZoneCW[i][5], "유리부분");
                            zone1.zoneCW.Add(cw);
                        }
                        else if (ZoneCW[i][2] == "패널부분")
                        {
                            String[][] CW_p = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "패널부분열관류율,패널흡수율,설치열교가산치", "번호 = '" + ZoneCW[i][3] + "'");
                            CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], 0, 0, 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(Value[0][0]), Convert.ToDouble(CW_p[i][1]), 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_p[i][2]), ZoneCW[i][4], ZoneCW[i][5], "패널부분");
                            zone1.zoneCW.Add(cw);
                        }
                        else
                        {
                            String[][] CW_d = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "출입문부분열관류율,출입문부분유리면적비,출입문태양열취득률,출입문빛투과율,설치열교가산치", "번호 = '" + ZoneCW[i][3] + "'");
                            CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(Uvalue[0][0]), Convert.ToDouble(CW_d[i][1]), Convert.ToDouble(CW_d[i][2]), Convert.ToDouble(CW_d[i][3]), Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_d[i][4]), ZoneCW[i][4], ZoneCW[i][5], "출입문부분");
                            zone1.zoneCW.Add(cw);
                        }  //나중에 차양포함 태양열취득률, 빛투과율 반영해야 함
                    }
                }
                catch { }
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
            zone.Clear();
        }

        public void Calc_Alt_All()
        {
            zone.Clear();
            CreateZone();
            for (int k = 0; k < zone.Count; k++)
            {
                Zone zone1 = (Zone)zone[k];
                zone1.검토유형[0][0] = "법규_전체";
                zone1.LoadData_ZoneGeneral();
                zone1.LoadData_Ventil();
                zone1.LoadData_InWall();
                zone1.LoadData_SL();

                /////////////////////////외벽//////////////////////
                try
                {
                    String[][] ZoneW = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.유효열관류율,b.흡수율,b.직접간접,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "' And  NOT b.직접간접 = '지면'");
                    // string[][] ZoneW = Program.DB.querySQL(DB.type.ProjDB, "ZoneWall", "Area,Ueff,α,DirectInDirect", "ZoneNum='" + ZoneNum + "'");
                    int i = -1;
                    while (++i < ZoneW.Length)
                    {
                        String DiIndi_;

                        if (ZoneW[i][5] == "직접외기" || ZoneW[i][5] == "간접외기")
                        {
                            DiIndi_ = ZoneW[i][5];
                        }
                        else
                        {
                            DiIndi_ = "간접외기";
                        }
                        string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율", "구조체 = '외벽' And 시기 = '2018.09' AND  지역 ='" + 지역구분[0][0] + "'  AND 직접간접 =  '" + DiIndi_ + "'");
                        Wall wall = new Wall(ZoneW[i][0], ZoneW[i][2], Convert.ToDouble(ZoneW[i][1]), Convert.ToDouble(Value[0][0]), Convert.ToDouble(ZoneW[i][4]), ZoneW[i][5], ZoneW[i][6], ZoneW[i][7]);
                        zone1.zoneWall.Add(wall);
                    }
                }
                catch { }


                try
                {
                    String[][] ZoneG = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.유효열관류율,b.직접간접 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "' And  b.직접간접 = '지면'");
                    //string[][] ZoneG = Program.DB.getValue(DB.type.ProjDB, "ZoneGWall", "Area,Ueff", "ZoneNum='" + ZoneNum + "'");
                    int i = -1;
                    while (++i < ZoneG.Length)
                    {
                        String DiIndi_;

                        if (ZoneG[i][4] == "직접외기" || ZoneG[i][4] == "간접외기")
                        {
                            DiIndi_ = ZoneG[i][5];
                        }
                        else
                        {
                            DiIndi_ = "간접외기";
                        }
                        string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '외벽' And 시기 = '2018.09' AND  지역 ='" + 지역구분[0][0] + "'  AND 직접간접 =  '" + DiIndi_ + "'");
                        double fx_f = 1;
                        if (Convert.ToDouble(Value[0][0]) >= 3)
                        { fx_f = 0.35; }
                        else if (Convert.ToDouble(Value[0][0]) >= 1)
                        { fx_f = 0.55; }
                        else if (Convert.ToDouble(Value[0][0]) > 0.3)
                        { fx_f = 0.65; }
                        else { fx_f = 0.75; }
                        break;

                        GWall gwall = new GWall(ZoneG[i][0], ZoneG[i][2], Convert.ToDouble(ZoneG[i][1]), Convert.ToDouble(Value[0][0]), fx_f);
                        zone1.zoneGWall.Add(gwall);
                    }
                }
                catch { }
                /////////////////////////지붕///////////////////////
                try
                {
                    String[][] ZoneR = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.유효열관류율,b.흡수율,b.직접간접,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionRoof AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
                    // string[][] ZoneR = Program.DB.getValue(DB.type.ProjDB, "ZoneRoof", "Area,Ueff,α,DirectInDirect", "ZoneNum='" + ZoneNum + "'");
                    int i = -1;
                    while (++i < ZoneR.Length)
                    {
                        string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율", "구조체 = '외벽' And 시기 = '2018.09' AND  지역 ='" + 지역구분[0][0] + "'  AND 직접간접 =  '" + ZoneR[i][5] + "'");
                        Roof roof = new Roof(ZoneR[i][0], ZoneR[i][2], Convert.ToDouble(ZoneR[i][1]), Convert.ToDouble(Value[0][0]), Convert.ToDouble(ZoneR[i][4]), ZoneR[i][5], ZoneR[i][6], ZoneR[i][7]);
                        zone1.zoneRoof.Add(roof);
                    }
                }
                catch { }
                //////////////////바닥//////////////////////////
                try
                {
                    String[][] ZoneF = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.유효열관류율,b.직접간접,b.기초설치 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionFloor AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");


                    int i = -1;
                    while (++i < ZoneF.Length)
                    {
                        double fx_f = 1;
                        String DiIndi_;
                        if (ZoneF[i][4] == "직접외기" || ZoneF[0][4] == "간접외기")
                        {
                            DiIndi_ = ZoneF[0][4];
                        }
                        else
                        {
                            DiIndi_ = "간접외기";
                        }
                        string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '바닥' And 시기 = '2018.09' AND  지역 ='" + 지역구분[0][0] + "'  AND 직접간접 =  '" + DiIndi_ + "'");
                        switch (ZoneF[i][5].ToString())
                        {
                            case "지면위":
                                {
                                    if (Convert.ToDouble(Value[0][0]) >= 3)
                                    { fx_f = 0.3; }
                                    else if (Convert.ToDouble(Value[0][0]) >= 1)
                                    { fx_f = 0.55; }
                                    else if (Convert.ToDouble(Value[0][0]) > 0.3)
                                    { fx_f = 0.7; }
                                    else { fx_f = 0.8; }
                                    break;
                                }
                            case "단열지하":
                                {
                                    if (Convert.ToDouble(Value[0][0]) >= 3)
                                    { fx_f = 0.2; }
                                    else if (Convert.ToDouble(Value[0][0]) >= 1)
                                    { fx_f = 0.45; }
                                    else if (Convert.ToDouble(Value[0][0]) > 0.3)
                                    { fx_f = 0.55; }
                                    else { fx_f = 0.7; }
                                    break;
                                }
                            case "비단열지하":
                                {
                                    if (Convert.ToDouble(Value[0][0]) >= 3)
                                    { fx_f = 0.45; }
                                    else if (Convert.ToDouble(Value[0][0]) >= 1)
                                    { fx_f = 0.75; }
                                    else if (Convert.ToDouble(Value[0][0]) > 0.3)
                                    { fx_f = 0.8; }
                                    else { fx_f = 0.85; }
                                    break;
                                }
                        }

                        Floor floor = new Floor(ZoneF[i][0], ZoneF[i][2], Convert.ToDouble(ZoneF[i][1]), Convert.ToDouble(Value[0][0]), ZoneF[i][5], fx_f);
                        zone1.zoneFloor.Add(floor);
                    }
                }
                catch { }

                ////////////////////////////창호//////////////////////////////

                try
                {
                    String[][] ZoneWin = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.창호열관류율,b.설치열교가산치,b.창호유효열관류율,b.유리면적비,b.상위창호번호,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
                    //string[][] ZoneWin = Program.DB.getValue(DB.type.ProjDB, "ZoneWin", "Area,Uvalue,Uinst,DirectInDirect,Ff,g,τ,gtot,τtot", "ZoneNum='" + ZoneNum + "'");
                    int i = -1;
                    while (++i < ZoneWin.Length)
                    {
                        String[][] ZoneWin_P = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "직접간접,태양열취득률,빛투과율", "번호='" + ZoneWin[i][7] + "'");
                        string[][] Blind = Program.DB.getValue(DB.type.ProjDB, "Blind_3D", "차양포함태양열취득률,차양포함빛투과율", "번호='" + ZoneWin[i][0] + "'");
                        String[][] Uvalue = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '창호' And 시기 = '2018.09' AND  지역 ='" + 지역구분[0][0] + "'  AND 직접간접 =  '" + ZoneWin_P[i][0] + "'");
                        Window win = new Window(ZoneWin[i][0], ZoneWin[i][7], ZoneWin[i][2], Convert.ToDouble(ZoneWin[i][1]), Convert.ToDouble(Uvalue[0][0]), Convert.ToDouble(ZoneWin[i][4]), ZoneWin_P[0][0], Convert.ToDouble(ZoneWin[i][6]), Convert.ToDouble(ZoneWin_P[0][1]), Convert.ToDouble(ZoneWin_P[0][2]), Convert.ToDouble(Blind[0][0]), Convert.ToDouble(Blind[0][1]), ZoneWin[i][8], ZoneWin[i][9]);
                        zone1.zoneWin.Add(win);
                        //나중에 차양포함 태양열취득률, 빛투과율 반영해야 함
                    }
                }
                catch { }

                ///////////////커튼월창/////////////////////////////
                try
                {
                    String[][] ZoneCW = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,면적,커튼월부위,구조체번호,방위,기울기.", "존 = '" + zone1.ZoneNum + "' AND 외피유형 = '커튼월창'");
                    // string[][] ZoneCW = Program.DB.getValue(DB.type.ProjDB, "ZoneCW", "Area_g,Uvalue_g,Ff_g,g_g,gtot_g,τ_g,τtot_g,Area_p,Uvalue_p,α_p,Area_d,Uvalue_d,Ff_d,g_d,τ_d,Area_tot,Uinst", "ZoneNum='" + ZoneNum + "'");
                    int i = -1;
                    while (++i < ZoneCW.Length)
                    { //유리부분면적,유리부분열관류율,유리부분유리면적비,태양열취득률,빛투과율, 패널부분 면적, 패널부분흡수율, 출입문부분면적, 출입문부분열관류율,출입문부분유리면적비, 출입문부분태양열취득률, 출입문부분빛투과율, 커튼월창면적, 설치열교가산치 

                        string[][] Uvalue = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '창호' And 시기 = '2018.09' AND  지역 ='" + 지역구분[0][0] + "'  AND 직접간접 =  '직접외기'");
                        string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율", "구조체 = '외벽' And 시기 = '2018.09' AND  지역 ='" + 지역구분[0][0] + "'  AND 직접간접 =  '직접외기'");
                        if (ZoneCW[i][2] == "유리부분")
                        {
                            String[][] CW_g = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "유리부분열관류율,유리부분유리면적비,태양열취득률,빛투과율,설치열교가산치", "번호 = '" + ZoneCW[i][3] + "'");
                            string[][] Blind = Program.DB.getValue(DB.type.ProjDB, "Blind_3D", "차양포함태양열취득률,차양포함빛투과율", "번호='" + ZoneCW[i][3] + "'");
                            CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(Uvalue[0][0]), Convert.ToDouble(CW_g[i][1]), Convert.ToDouble(CW_g[i][2]), Convert.ToDouble(Blind[0][0]), Convert.ToDouble(CW_g[i][3]), Convert.ToDouble(Blind[0][1]), 0, 0, 0, 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_g[i][4]), ZoneCW[i][4], ZoneCW[i][5], "유리부분");
                            zone1.zoneCW.Add(cw);
                        }
                        else if (ZoneCW[i][2] == "패널부분")
                        {
                            String[][] CW_p = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "패널부분열관류율,패널흡수율,설치열교가산치", "번호 = '" + ZoneCW[i][3] + "'");
                            CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], 0, 0, 0, 0, 0,0,0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(Value[0][0]), Convert.ToDouble(CW_p[i][1]), 0, 0, 0, 0, 0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_p[i][2]), ZoneCW[i][4], ZoneCW[i][5], "패널부분");
                            zone1.zoneCW.Add(cw);
                        }
                        else
                        {
                            String[][] CW_d = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "출입문부분열관류율,출입문부분유리면적비,출입문태양열취득률,출입문빛투과율,설치열교가산치", "번호 = '" + ZoneCW[i][3] + "'");
                            CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], 0, 0, 0, 0, 0, 0, 0,0,0, 0, Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(Uvalue[0][0]), Convert.ToDouble(CW_d[i][1]), Convert.ToDouble(CW_d[i][2]), Convert.ToDouble(CW_d[i][3]), Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(CW_d[i][4]), ZoneCW[i][4], ZoneCW[i][5], "출입문부분");
                            zone1.zoneCW.Add(cw);
                        }  //나중에 차양포함 태양열취득률, 빛투과율 반영해야 함
                    }
                }
                catch { }
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
            zone.Clear();
        }

    }
}
