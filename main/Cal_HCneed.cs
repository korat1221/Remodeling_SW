using System.Collections;
using System.Security.AccessControl;

namespace main
{
   
    internal class Zone
    {
        public String zoneName;
        public String zoneUsage, zoneHC, Mode_night, Mode_we;
        public double theta_i_h_set, theta_i_c_set, dtheta_i_NA, Fx, Fx_Floor, Fx_GWall, theta_s_c, theta_i_h_min, theta_e_min, theta_SUP_Wi;
        public double twd_d, th_op_d_we, th_op_d, dwd_a;
        public double zoneArea, zoneHeight;
        public double qI_p, qI_fac, Cwirk_A;
        public double VA_we, VA_wd, n50, e, f, Vmech_SUP_we, Vmech_SUP_wd, Vmech_ETA_we, Vmech_ETA_wd, eta_V_mech, eta_χV_mech, xi_c_set, xi_h_set, Vmech_SUP_z, Vmech_ETA_z, ρacp_a;
        public ArrayList zoneWall = new ArrayList(); 
        public ArrayList zoneRoof = new ArrayList(); 
        public ArrayList zoneFloor = new ArrayList(); 
        public ArrayList zoneGWall = new ArrayList();
        public ArrayList zoneDoor = new ArrayList();
        public ArrayList zoneWin = new ArrayList();
        public ArrayList zoneCW = new ArrayList();
        public double Zone_HT_tot, Zone_HT_Wall, Zone_HT_Roof, Zone_HT_Floor, Zone_HT_GWall, Zone_HT_Door, Zone_HT_Win, Zone_HT_CW;
        public double Zone_HT_Di_Wall, Zone_HT_Indi_Wall, Zone_HT_Di_Roof, Zone_HT_Indi_Roof, Zone_HT_Di_Win, Zone_HT_Indi_Win, Zone_HT_Di_Door, Zone_HT_Indi_Door;
        public double Zone_HT_TB_tot, Zone_HT_TB_Wall, Zone_HT_TB_Roof, Zone_HT_TB_Floor, Zone_HT_TB_Gwall, Zone_HT_TB_Win, Zone_HT_TB_Door, Zone_HT_TB_CW;
        public double[] Zone_HV_tot = new double[2], Zone_HV_inf = new double[2], Zone_HV_win = new double[2], Zone_HV_z = new double[2], Zone_HV_mech = new double[2]; //[비이용일/이용일] = [we/wd]=[0/1]
        public double[] Zone_H_tot = new double[2]; //[비이용일/이용일] = [we/wd]=[0/1]
        public double[] tao = new double[2]; //[비이용일/이용일] = [we/wd]=[0/1]
        public double[] theta_e = new double[12], dwe_mth = new double[12], dwd_mth = new double[12];
        public double[] dmth = new double[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public double[,,] theta_i = new double[2, 2, 12];

        //[난방/냉방,비이용일/이용일,mth] = [h/c,we/wd,mth]=[0/1,0/1,12]
        //QT
        public double[,,] QTsink_tot = new double[2, 2, 12], QTsink_Wall = new double[2, 2, 12], QTsink_Roof = new double[2, 2, 12], QTsink_Floor = new double[2, 2, 12], QTsink_GWall = new double[2, 2, 12], QTsink_Door = new double[2, 2, 12], QTsink_Win = new double[2, 2, 12], QTsink_CW = new double[2, 2, 12];
        public double[,,] QTsource_tot = new double[2, 2, 12], QTsource_Wall = new double[2, 2, 12], QTsource_Roof = new double[2, 2, 12], QTsource_Floor = new double[2, 2, 12], QTsource_GWall = new double[2, 2, 12], QTsource_Door = new double[2, 2, 12], QTsource_Win = new double[2, 2, 12], QTsource_CW = new double[2, 2, 12];
        //QS
        public double[,,] QSopsink_tot = new double[2, 2, 12], QSopsource_tot = new double[2, 2, 12], QStr_tot = new double[2, 2, 12];
        public double[] QSopsink_Wall = new double[12], QSopsink_Roof = new double[12], QSopsink_Door = new double[12], QSopsink_CW_p = new double[12];
        public double[] QSopsource_Wall = new double[12], QSopsource_Roof = new double[12], QSopsource_Door = new double[12], QSopsource_CW_p = new double[12];
        public double[,] QStr_Win = new double[2, 12], QStr_CW = new double[2, 12];
        //QV
        public double[,,] QVsink_tot = new double[2, 2, 12], QV_inf_sink = new double[2, 2, 12], QV_win_sink = new double[2, 2, 12], QV_z_sink = new double[2, 2, 12], QV_mech_sink = new double[2, 2, 12];
        public double[,,] QVsource_tot = new double[2, 2, 12], QV_inf_source = new double[2, 2, 12], QV_win_source = new double[2, 2, 12], QV_z_source = new double[2, 2, 12], QV_mech_source = new double[2, 2, 12];
        //QI
        public double[,,] QI_tot = new double[2, 2, 12], QI_L = new double[2, 2, 12];
        public double[] QI_P = new double[2], QI_fac = new double[2];
        //
        public double[,,] Qsink = new double[2, 2, 12], Qsource = new double[2, 2, 12], gamma = new double[2, 2, 12], a = new double[2, 2, 12], eta = new double[2, 2, 12], dQc_b = new double[2, 2, 12], dQc_sink = new double[2, 2, 12];
        public double[] Qhb_we_day = new double[12], Qhb_wd_day = new double[12], Qcb_we_day = new double[12], Qcb_wd_day = new double[12];
        public double[] Qhb_mth = new double[12], Qcb_mth = new double[12], Qhb_we_mth = new double[12], Qhb_wd_mth = new double[12], Qcb_we_mth = new double[12], Qcb_wd_mth = new double[12];
        public double Qhb_a, Qcb_a, Qhb_we_a, Qhb_wd_a, Qcb_we_a, Qcb_wd_a;


        public Zone(String zoneNum)
        {
            {
                string[][] 지역 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
                string[][] OTemp = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_온도습도", "기간,온도", "지역명 ='" + 지역[0][0] + "'");
                int i = -1;
                while (++i < OTemp.Length)
                {
                    theta_e[i] = Convert.ToDouble(OTemp[i][1]);
                }
            }

            //존 정보 가져오기
            {
                string[][] ZoneG = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_form", "존이름,용도프로필,냉난방유무", "존번호='" + zoneNum + "'");
               
                  

                if (ZoneG.Length > 0)
                {
                    zoneName = ZoneG[0][0];
                    zoneUsage = ZoneG[0][1];
                    zoneHC = ZoneG[0][2];

                    String[][] ZoneU = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "난방설정온도,냉방설정온도,허용셋백온도,난방최저온도,최저상대습도,최고상대습도", "용도명='" + ZoneG[0][1] + "'");
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
                        theta_e_min = -12;
                        theta_SUP_Wi = 18;
                        Mode_night = "운전정지";
                        Mode_we = "운전정지";
                        xi_c_set = 611.2 * Math.Exp(17.62 * theta_i_c_set / (243.12 + theta_i_c_set)) / 461.51 / (273.15 + theta_i_c_set) / 1.2 * (Convert.ToDouble(ZoneU[0][5])/100);
                        xi_h_set = 611.2 * Math.Exp(17.62 * theta_i_h_set / (243.12 + theta_i_h_set)) / 461.51 / (273.15 + theta_i_h_set) / 1.2 * (Convert.ToDouble(ZoneU[0][4])/100);
                    }

                    ZoneG = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_form", "사용시간,냉난방시간,연이용일수,순바닥면적,천장고, 면적당인체발열, 면적당기기발열, 존축열성능, 비이용일환기량,이용일환기량,n50", "존번호='" + zoneNum + "'");

                    twd_d = Convert.ToDouble(ZoneG[0][0]);
                    th_op_d_we = 0;
                    th_op_d = Convert.ToDouble(ZoneG[0][1]);
                    dwd_a = Convert.ToDouble(ZoneG[0][2]);
                    zoneArea = Convert.ToDouble(ZoneG[0][3]);
                    zoneHeight = Convert.ToDouble(ZoneG[0][4]);
                    qI_p = Convert.ToDouble(ZoneG[0][5]);
                    qI_fac = Convert.ToDouble(ZoneG[0][6]);
                    Cwirk_A = Convert.ToDouble(ZoneG[0][7]);
                    VA_we = Convert.ToDouble(ZoneG[0][8])/ zoneArea; //단위면적당 값 
                    VA_wd = Convert.ToDouble(ZoneG[0][9])/ zoneArea;//단위면적당 값 
                    n50 = Convert.ToDouble(ZoneG[0][10]);
                    e = 0.07;
                    f = 15;

                    ZoneG = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_form", "환기유무,환기방식,비이용일환기량,이용일환기량,온도교환효율,전열교환효율", "존번호='" + zoneNum + "'");
                    if(Convert.ToBoolean(ZoneG[0][0]))
                    {
                        if (ZoneG[0][1] == "열회수환기")
                        {
                            Vmech_SUP_we = Convert.ToDouble(ZoneG[0][2]);
                            Vmech_ETA_we = Convert.ToDouble(ZoneG[0][2]);
                            Vmech_SUP_wd = Convert.ToDouble(ZoneG[0][3]);
                            Vmech_ETA_wd = Convert.ToDouble(ZoneG[0][3]);
                            eta_V_mech = Convert.ToDouble(ZoneG[0][4]);
                            eta_χV_mech = Convert.ToDouble(ZoneG[0][5]); //나중에 습도교환효율로 바꿔야함 
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
            //존 외벽 정보 가져오기
            { 
                String[][] ZoneW = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.유효열관류율,b.흡수율,b.직접간접 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + zoneNum + "' And  NOT b.직접간접 = '지면'");
               // string[][] ZoneW = Program.DB.querySQL(DB.type.ProjDB, "ZoneWall", "Area,Ueff,α,DirectInDirect", "zoneNum='" + zoneNum + "'");
                int i = -1;
                while (++i < ZoneW.Length)
                {
                    Wall wall = new Wall(Convert.ToDouble(ZoneW[i][1]), Convert.ToDouble(ZoneW[i][3]), Convert.ToDouble(ZoneW[i][4]), ZoneW[i][5]);
                    zoneWall.Add(wall);
                }
            }

            //존 지붕 정보 가져오기
            {
                String[][] ZoneR = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.유효열관류율,b.흡수율,b.직접간접 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionRoof AS b ON a.구조체번호 = b.번호 where a.존 = '" + zoneNum + "'");
                // string[][] ZoneR = Program.DB.getValue(DB.type.ProjDB, "ZoneRoof", "Area,Ueff,α,DirectInDirect", "zoneNum='" + zoneNum + "'");
                int i = -1;
                while (++i < ZoneR.Length)
                {
                    Roof roof = new Roof(Convert.ToDouble(ZoneR[i][1]), Convert.ToDouble(ZoneR[i][3]), Convert.ToDouble(ZoneR[i][4]), ZoneR[i][5]);
                    zoneRoof.Add(roof);
                }
            }
           
            //존 바닥 정보 가져오기
            {
                String[][] ZoneF = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.유효열관류율,b.직접간접 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionFloor AS b ON a.구조체번호 = b.번호 where a.존 = '" + zoneNum + "'");
                // string[][] ZoneF = Program.DB.getValue(DB.type.ProjDB, "ZoneFloor", "Area,Ueff", "zoneNum='" + zoneNum + "'");
                int i = -1;
                while (++i < ZoneF.Length)
                {
                    Floor floor = new Floor(Convert.ToDouble(ZoneF[i][0]), Convert.ToDouble(ZoneF[i][3]));
                    zoneFloor.Add(floor);
                }
            }
            //존 지하벽 정보 가져오기
            {
                String[][] ZoneG = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.유효열관류율,b.직접간접 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + zoneNum + "' And  b.직접간접 = '지면'");
                //string[][] ZoneG = Program.DB.getValue(DB.type.ProjDB, "ZoneGWall", "Area,Ueff", "zoneNum='" + zoneNum + "'");
                int i = -1;
                while (++i < ZoneG.Length)
                {
                    GWall gwall = new GWall(Convert.ToDouble(ZoneG[i][1]), Convert.ToDouble(ZoneG[i][3]));
                    zoneGWall.Add(gwall);
                }
            }

            //존 문 정보 가져오기<<나중에 문으로 바꿔야 함 
            {
                String[][] ZoneD = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.유효열관류율,b.흡수율,b.직접간접 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + zoneNum + "'");
                //string[][] ZoneD = Program.DB.getValue(DB.type.ProjDB, "ZoneDoor", "Area,Ueff,α,DirectInDirect", "zoneNum='" + zoneNum + "'");
                int i = -1;
                while (++i < ZoneD.Length)
                {
                    Door door = new Door(Convert.ToDouble(ZoneD[i][1]), Convert.ToDouble(ZoneD[i][3]), Convert.ToDouble(ZoneD[i][4]), ZoneD[i][5]);
                    zoneDoor.Add(door);
                }
            }
            //존 창문 정보 가져오기
            {
                String[][] ZoneWin = Program.DB.querySQL(DB.type.ProjDB, "select a.번호,a.면적,b.번호,b.창호열관류율,b.설치열교가산치,b.창호유효열관류율,b.유리면적비,b.상위창호번호 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체번호 = b.번호 where a.존 = '" + zoneNum + "'");
                //string[][] ZoneWin = Program.DB.getValue(DB.type.ProjDB, "ZoneWin", "Area,Uvalue,Uinst,DirectInDirect,Ff,g,τ,gtot,τtot", "zoneNum='" + zoneNum + "'");
                int i = -1;
                while (++i < ZoneWin.Length)
                {
                    String[][] ZoneWin_P = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "직접간접,태양열취득률,빛투과율", "번호='" + ZoneWin[i][7] + "'");
                    Window win = new Window(Convert.ToDouble(ZoneWin[i][1]), Convert.ToDouble(ZoneWin[i][3]), Convert.ToDouble(ZoneWin[i][4]), ZoneWin_P[i][0], Convert.ToDouble(ZoneWin[i][6]), Convert.ToDouble(ZoneWin[i][1]), Convert.ToDouble(ZoneWin[i][2]));
                    zoneWin.Add(win);
                    //나중에 창호포함 태양열취득률, 빛투과율 반영해야 함
                }
            }

            //존 커튼월 정보 가져오기
            {
                string[][] ZoneCW = Program.DB.getValue(DB.type.ProjDB, "ZoneCW", "Area_g,Uvalue_g,Ff_g,g_g,gtot_g,τ_g,τtot_g,Area_p,Uvalue_p,α_p,Area_d,Uvalue_d,Ff_d,g_d,τ_d,Area_tot,Uinst", "zoneNum='" + zoneNum + "'");
                int i = -1;
                while (++i < ZoneCW.Length)
                {
                    CW cw = new CW(Convert.ToDouble(ZoneCW[i][0]), Convert.ToDouble(ZoneCW[i][1]), Convert.ToDouble(ZoneCW[i][2]), Convert.ToDouble(ZoneCW[i][3]), Convert.ToDouble(ZoneCW[i][4]), Convert.ToDouble(ZoneCW[i][5]), Convert.ToDouble(ZoneCW[i][6]), Convert.ToDouble(ZoneCW[i][7]), Convert.ToDouble(ZoneCW[i][8]), Convert.ToDouble(ZoneCW[i][9]), Convert.ToDouble(ZoneCW[i][10]), Convert.ToDouble(ZoneCW[i][11]), Convert.ToDouble(ZoneCW[i][12]), Convert.ToDouble(ZoneCW[i][13]), Convert.ToDouble(ZoneCW[i][14]), Convert.ToDouble(ZoneCW[i][15]), Convert.ToDouble(ZoneCW[i][16]));
                    zoneCW.Add(cw);
                }
            }
            //try
            //{
            //    string filePath = "C:\\javalecture\\day0118\\day0118\\"+zoneNum+"\\ZoneCW.csv";
            //    using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
            //    {
            //        using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            while (!sr.EndOfStream)
            //            {
            //                string[] token = sr.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    CW cw = new CW(Convert.ToDouble(token[1]), Convert.ToDouble(token[2]), Convert.ToDouble(token[3]), Convert.ToDouble(token[4]), Convert.ToDouble(token[5]), Convert.ToDouble(token[6]), Convert.ToDouble(token[7]), Convert.ToDouble(token[8]), Convert.ToDouble(token[9]), Convert.ToDouble(token[10]), Convert.ToDouble(token[11]), Convert.ToDouble(token[12]), Convert.ToDouble(token[13]), Convert.ToDouble(token[14]), Convert.ToDouble(token[15]), Convert.ToDouble(token[16]), Convert.ToDouble(token[17]));
            //                    zoneCW.Add(cw);
            //                }
            //                n++;

            //            }
            //        }
            //    }


            //}
            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}
        }


        public void ZoneHT() //관류 HT 계산
        {
            //외벽 HT
            for (int i = 0; i < zoneWall.Count; i++)
            {
                Wall zonewall = (Wall)zoneWall[i]; //List를 class 객체로 변환 
                HTCalc htcalc = new HTCalc();
                double[] zoneWall_HT = new double[zoneWall.Count];
                zoneWall_HT[i] = htcalc.Calc(zonewall.Ueff(), zonewall.Area());
                if (zonewall.DiIndi() == "Direction")
                {
                    Zone_HT_Di_Wall += zoneWall_HT[i];

                }
                else if (zonewall.DiIndi() == "Indirection")
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
                zoneRoof_HT[i] = htcalc.Calc(zoneroof.Ueff(), zoneroof.Area());
                if (zoneroof.DiIndi() == "Direction")
                {
                    Zone_HT_Di_Roof += zoneRoof_HT[i];

                }
                else if (zoneroof.DiIndi() == "Indirection")
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
                zoneFloor_HT[i] = htcalc.Calc(zonefloor.Ueff(), zonefloor.Area());
                Zone_HT_Floor += zoneFloor_HT[i];
            }

            //지하벽 HT
            for (int i = 0; i < zoneGWall.Count; i++)
            {
                GWall zonegwall = (GWall)zoneGWall[i]; //List를 class 객체로 변환 
                HTCalc htcalc = new HTCalc();
                double[] zoneGWall_HT = new double[zoneGWall.Count];
                zoneGWall_HT[i] = htcalc.Calc(zonegwall.Ueff(), zonegwall.Area());
                Zone_HT_GWall += zoneGWall_HT[i];
            }


            //문 HT
            for (int i = 0; i < zoneDoor.Count; i++)
            {
                Door zonedoor = (Door)zoneDoor[i]; //List를 class 객체로 변환 
                HTCalc htcalc = new HTCalc();
                double[] zoneDoor_HT = new double[zoneDoor.Count];
                zoneDoor_HT[i] = htcalc.Calc(zonedoor.Ueff(), zonedoor.Area());
                if (zonedoor.DiIndi() == "Direction")
                {
                    Zone_HT_Di_Door += zoneDoor_HT[i];

                }
                else if (zonedoor.DiIndi() == "Indirection")
                {
                    Zone_HT_Indi_Door += zoneDoor_HT[i];
                }
                Zone_HT_Door = Zone_HT_Di_Door + Zone_HT_Indi_Door;
            }

            //창 HT
            for (int i = 0; i < zoneWin.Count; i++)
            {
                Window zonewin = (Window)zoneWin[i]; //List를 class 객체로 변환 
                HTCalc htcalc = new HTCalc();
                double[] zoneWin_HT = new double[zoneWin.Count];
                zoneWin_HT[i] = htcalc.Calc(zonewin.Uvalue(), zonewin.Area());
                if (zonewin.DiIndi() == "Direction")
                {
                    Zone_HT_Di_Win += zoneWin_HT[i];
                }
                else if (zonewin.DiIndi() == "Indirection")
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
            Zone_HT_TB_tot = Zone_HT_TB_Wall + Zone_HT_TB_Roof + Zone_HT_TB_Floor + Zone_HT_TB_Gwall + Zone_HT_TB_Win + Zone_HT_TB_Door + Zone_HT_TB_CW;
            Zone_HT_tot = Zone_HT_TB_tot + Zone_HT_Wall + Zone_HT_Roof + Zone_HT_Floor + Zone_HT_GWall + Zone_HT_Win + Zone_HT_Door + Zone_HT_CW;
        }

        public void ZoneHV()  //환기 HV계산
        {
            HVCalc hvcalc = new HVCalc();
            Zone_HV_mech[0] = hvcalc.HV_mech_Calc(Vmech_SUP_we, th_op_d_we, (zoneArea * zoneHeight));
            Zone_HV_mech[1] = hvcalc.HV_mech_Calc(Vmech_SUP_wd, th_op_d, (zoneArea * zoneHeight));
            Zone_HV_z[0] = hvcalc.HV_z_Calc(Vmech_SUP_we, Vmech_ETA_we, th_op_d_we, (zoneArea * zoneHeight));
            Zone_HV_z[1] = hvcalc.HV_z_Calc(Vmech_SUP_wd, Vmech_ETA_wd, th_op_d, (zoneArea * zoneHeight));
            Zone_HV_inf[0] = hvcalc.HV_inf_Calc(Vmech_SUP_we, Vmech_ETA_we, Vmech_SUP_z, Vmech_ETA_z, th_op_d_we, n50, (zoneArea * zoneHeight), e, f);
            Zone_HV_inf[1] = hvcalc.HV_inf_Calc(Vmech_SUP_wd, Vmech_ETA_wd, Vmech_SUP_z, Vmech_ETA_z, th_op_d, n50, (zoneArea * zoneHeight), e, f);
            Zone_HV_win[0] = 0.1 * (zoneArea * zoneHeight) * 0.34;
            Zone_HV_win[1] = hvcalc.HV_win_Calc(Vmech_SUP_wd, Vmech_ETA_wd, Vmech_SUP_z, Vmech_ETA_z, th_op_d, twd_d, n50, (VA_wd / zoneHeight), (zoneArea * zoneHeight), e, f);
            Zone_HV_tot[0] = Zone_HV_mech[0] + Zone_HV_z[0] + Zone_HV_inf[0] + Zone_HV_win[0];
            Zone_HV_tot[1] = Zone_HV_mech[1] + Zone_HV_z[1] + Zone_HV_inf[1] + Zone_HV_win[1];
        }

        public void Zonetao()//시간상수 계산
        {
            Zone_H_tot[0] = Zone_HT_tot + Zone_HV_tot[0];
            Zone_H_tot[1] = Zone_HT_tot + Zone_HV_tot[1];
            theta_iCalc calc = new theta_iCalc();
            tao[0] = calc.tao_Calc(Cwirk_A * zoneArea, Zone_H_tot[0]);
            tao[1] = calc.tao_Calc(Cwirk_A * zoneArea, Zone_H_tot[1]);
        }

        public void Zonethetai()//실내기준온도 계산
        {
            theta_iCalc calc = new theta_iCalc();
            for (int mth = 0; mth < 12; mth++)
            {
                //[hc, wewd, mth]	
                theta_i[0, 0, mth] = calc.theta_ihwe_Calc(tao[0], Mode_we, theta_e[mth], theta_i_h_set, dtheta_i_NA);
                theta_i[0, 1, mth] = calc.theta_ihwd_Calc(tao[1], Mode_night, (24 - th_op_d), theta_e[mth], theta_i_h_set, dtheta_i_NA);
                theta_i[1, 0, mth] = calc.theta_ic_Calc(theta_i_c_set);
                theta_i[1, 1, mth] = calc.theta_ic_Calc(theta_i_c_set);
            }

        }

        public void ZoneQT()//관류 열전달 계산
        {
            double[,,] QTsink_Di_tot = new double[2, 2, 12], QTsink_Wall_Di = new double[2, 2, 12], QTsink_Roof_Di = new double[2, 2, 12], QTsink_Door_Di = new double[2, 2, 12], QTsink_Win_Di = new double[2, 2, 12], QTsink_CW = new double[2, 2, 12];
            double[,,] QTsink_Indi_tot = new double[2, 2, 12], QTsink_Wall_Indi = new double[2, 2, 12], QTsink_Roof_Indi = new double[2, 2, 12], QTsink_Door_Indi = new double[2, 2, 12], QTsink_Win_Indi = new double[2, 2, 12];
            double[,,] QTsource_Di_tot = new double[2, 2, 12], QTsource_Wall_Di = new double[2, 2, 12], QTsource_Roof_Di = new double[2, 2, 12], QTsource_Door_Di = new double[2, 2, 12], QTsource_Win_Di = new double[2, 2, 12], QTsource_CW = new double[2, 2, 12];
            double[,,] QTsource_Indi_tot = new double[2, 2, 12], QTsource_Wall_Indi = new double[2, 2, 12], QTsource_Roof_Indi = new double[2, 2, 12], QTsource_Door_Indi = new double[2, 2, 12], QTsource_Win_Indi = new double[2, 2, 12];
            double[,,] QTsink_TB = new double[2, 2, 12];
            double[,,] QTsource_TB = new double[2, 2, 12];
            QTCalc qtcalc = new QTCalc();


            for (int hc = 0; hc <= 1; hc++)
            {
                for (int wewd = 0; wewd <= 1; wewd++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {

                        //직접외기 QT계산 
                        if (theta_i[hc, wewd, mth] >= theta_e[mth])
                        {
                            QTsink_Wall_Di[hc, wewd, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i[hc, wewd, mth], Zone_HT_Di_Wall);  // 외기온도, 실내온도, 직접외기외벽의 관류열전달계수 
                            QTsink_Roof_Di[hc, wewd, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i[hc, wewd, mth], Zone_HT_Di_Roof);
                            QTsink_Door_Di[hc, wewd, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i[hc, wewd, mth], Zone_HT_Di_Door);
                            QTsink_Win_Di[hc, wewd, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i[hc, wewd, mth], Zone_HT_Di_Win);
                            QTsink_CW[hc, wewd, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i[hc, wewd, mth], Zone_HT_CW);
                            QTsink_TB[hc, wewd, mth] = qtcalc.Calc_sink(theta_e[mth], theta_i[hc, wewd, mth], Zone_HT_TB_tot);
                            QTsink_Di_tot[hc, wewd, mth] = QTsink_TB[hc, wewd, mth] + QTsink_Wall_Di[hc, wewd, mth] + QTsink_Roof_Di[hc, wewd, mth] + QTsink_Door_Di[hc, wewd, mth] + QTsink_Win_Di[hc, wewd, mth] + QTsink_CW[hc, wewd, mth];
                        }
                        else if (theta_i[hc, wewd, mth] < theta_e[mth])
                        {
                            QTsource_Wall_Di[hc, wewd, mth] = qtcalc.Calc_source(theta_e[mth], theta_i[hc, wewd, mth], Zone_HT_Di_Wall);
                            QTsource_Roof_Di[hc, wewd, mth] = qtcalc.Calc_source(theta_e[mth], theta_i[hc, wewd, mth], Zone_HT_Di_Roof);
                            QTsource_Door_Di[hc, wewd, mth] = qtcalc.Calc_source(theta_e[mth], theta_i[hc, wewd, mth], Zone_HT_Di_Door);
                            QTsource_Win_Di[hc, wewd, mth] = qtcalc.Calc_source(theta_e[mth], theta_i[hc, wewd, mth], Zone_HT_Di_Win);
                            QTsource_CW[hc, wewd, mth] = qtcalc.Calc_source(theta_e[mth], theta_i[hc, wewd, mth], Zone_HT_CW);
                            QTsource_TB[hc, wewd, mth] = qtcalc.Calc_source(theta_e[mth], theta_i[hc, wewd, mth], Zone_HT_TB_tot);
                            QTsource_Di_tot[hc, wewd, mth] = QTsource_TB[hc, wewd, mth] + QTsource_Wall_Di[hc, wewd, mth] + QTsource_Roof_Di[hc, wewd, mth] + QTsource_Door_Di[hc, wewd, mth] + QTsource_Win_Di[hc, wewd, mth] + QTsource_CW[hc, wewd, mth];
                        }

                        //간접외기 QT계산  
                        double[,,] theta_u = new double[2, 2, 12];
                        theta_u[hc, wewd, mth] = theta_i[hc, wewd, mth] - Fx * (theta_i[hc, wewd, mth] - theta_e[mth]);

                        if (theta_i[hc, wewd, mth] >= theta_u[hc, wewd, mth])
                        {
                            QTsink_Wall_Indi[hc, wewd, mth] = qtcalc.Calc_sink(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], Zone_HT_Indi_Wall);
                            QTsink_Roof_Indi[hc, wewd, mth] = qtcalc.Calc_sink(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], Zone_HT_Indi_Roof);
                            QTsink_Door_Indi[hc, wewd, mth] = qtcalc.Calc_sink(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], Zone_HT_Indi_Door);
                            QTsink_Win_Indi[hc, wewd, mth] = qtcalc.Calc_sink(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], Zone_HT_Indi_Win);
                            QTsink_Indi_tot[hc, wewd, mth] = QTsink_Wall_Indi[hc, wewd, mth] + QTsink_Roof_Indi[hc, wewd, mth] + QTsink_Door_Indi[hc, wewd, mth] + QTsink_Win_Indi[hc, wewd, mth];
                        }
                        else if (theta_i[hc, wewd, mth] < theta_u[hc, wewd, mth])
                        {
                            QTsource_Wall_Indi[hc, wewd, mth] = qtcalc.Calc_source(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], Zone_HT_Indi_Wall);
                            QTsource_Roof_Indi[hc, wewd, mth] = qtcalc.Calc_source(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], Zone_HT_Indi_Roof);
                            QTsource_Door_Indi[hc, wewd, mth] = qtcalc.Calc_source(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], Zone_HT_Indi_Door);
                            QTsource_Win_Indi[hc, wewd, mth] = qtcalc.Calc_source(theta_u[hc, wewd, mth], theta_i[hc, wewd, mth], Zone_HT_Indi_Win);
                            QTsource_Indi_tot[hc, wewd, mth] = QTsource_Wall_Indi[hc, wewd, mth] + QTsource_Roof_Indi[hc, wewd, mth] + QTsource_Door_Indi[hc, wewd, mth] + QTsource_Win_Indi[hc, wewd, mth];
                        }

                        //바닥 QT계산  
                        double[,,] theta_s_Floor = new double[2, 2, 12];
                        theta_s_Floor[0, wewd, mth] = theta_i[0, wewd, mth] - Fx_Floor * (theta_i[0, wewd, mth] - theta_e[mth]);
                        theta_s_Floor[1, wewd, mth] = theta_s_c;
                        if (theta_i[hc, wewd, mth] >= theta_s_Floor[hc, wewd, mth])
                        {
                            QTsink_Floor[hc, wewd, mth] = qtcalc.Calc_sink(theta_s_Floor[hc, wewd, mth], theta_i[hc, wewd, mth], Zone_HT_Floor);
                        }
                        else if (theta_i[hc, wewd, mth] < theta_s_Floor[hc, wewd, mth])
                        {
                            QTsource_Floor[hc, wewd, mth] = qtcalc.Calc_source(theta_s_Floor[hc, wewd, mth], theta_i[hc, wewd, mth], Zone_HT_Floor);
                        }

                        //지하벽 QT계산    
                        double[,,] theta_s_GWall = new double[2, 2, 12];
                        theta_s_GWall[0, wewd, mth] = theta_i[0, wewd, mth] - Fx_GWall * (theta_i[0, wewd, mth] - theta_e[mth]);
                        theta_s_GWall[1, wewd, mth] = theta_s_c;
                        if (theta_i[hc, wewd, mth] >= theta_s_GWall[hc, wewd, mth])
                        {
                            QTsink_GWall[hc, wewd, mth] = qtcalc.Calc_sink(theta_s_GWall[hc, wewd, mth], theta_i[hc, wewd, mth], Zone_HT_GWall);
                        }
                        else if (theta_i[hc, wewd, mth] < theta_s_GWall[hc, wewd, mth])
                        {
                            QTsource_GWall[hc, wewd, mth] = qtcalc.Calc_source(theta_s_GWall[hc, wewd, mth], theta_i[hc, wewd, mth], Zone_HT_GWall);
                        }

                        // QT_tot계산
                        QTsink_tot[hc, wewd, mth] = QTsink_Di_tot[hc, wewd, mth] + QTsink_Indi_tot[hc, wewd, mth] + QTsink_Floor[hc, wewd, mth] + QTsink_GWall[hc, wewd, mth];
                        QTsource_tot[hc, wewd, mth] = QTsource_Di_tot[hc, wewd, mth] + QTsource_Indi_tot[hc, wewd, mth] + QTsource_Floor[hc, wewd, mth] + QTsource_GWall[hc, wewd, mth];

                    }
                }
            }

        }

        public void ZoneQSop(String zoneNum)// 불투명 일사 계산
        {
            //외벽 일사 계산
            double[,] zoneWalls_Is = new double[zoneWall.Count, 12];
            double[,] zoneWalls_Qssink = new double[zoneWall.Count, 12];
            double[,] zoneWalls_Qssource = new double[zoneWall.Count, 12];

            {
                int i = -1;

                string[][] ZoneW_Name = Program.DB.getValue(DB.type.ProjDB, "ZoneWall", "Name", "zoneNum='" + zoneNum + "'");

                while (++i < zoneWall.Count)
                {
                    Wall zonewall = (Wall)zoneWall[i];
                    string[][] token = Program.DB.getValue(DB.type.ProjDB, "ZoneWall_Solar", "value", "zoneNum='" + zoneNum + "' AND 구조체='" + ZoneW_Name[i][0] + "'");

                    for (int mth = 0; mth < 12; mth++)
                    {
                        zoneWalls_Is[i, mth] = Convert.ToDouble(token[mth][0]);
                        QSopCalc qsopcalc = new QSopCalc();
                        if (zonewall.DiIndi() == "Indirection")
                        {   //직접외기 벽만 일사 계산      
                        }
                        else
                        {
                            if (0.5 * 4.5 * 10 >= zonewall.α() * zoneWalls_Is[i, mth])
                            {
                                zoneWalls_Qssink[i, mth] = qsopcalc.Calc(zonewall.Ueff(), zonewall.Area(), zonewall.α(), zoneWalls_Is[i, mth]);
                                zoneWalls_Qssource[i, mth] = 0;
                            }
                            else
                            {
                                zoneWalls_Qssink[i, mth] = 0;
                                zoneWalls_Qssource[i, mth] = qsopcalc.Calc(zonewall.Ueff(), zonewall.Area(), zonewall.α(), zoneWalls_Is[i, mth]);
                            }
                        }
                        QSopsink_Wall[mth] += zoneWalls_Qssink[i, mth];
                        QSopsource_Wall[mth] += zoneWalls_Qssource[i, mth];

                    }
                }
            }

            //try
            //{
            //    String line = "";
            //    string filePath = "C:\\javalecture\\day0118\\day0118\\"+zoneNum+"\\ZoneWall_Solar.csv";
            //    using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
            //    {
            //        using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
            //        {
            //            double[,] zoneWalls_Is = new double[zoneWall.Count, 12];
            //            double[,] zoneWalls_Qssink = new double[zoneWall.Count, 12];
            //            double[,] zoneWalls_Qssource = new double[zoneWall.Count, 12];
            //            int n = 0;
            //            while (!sr.EndOfStream)
            //            {
            //                string[] token = sr.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    //두번째 행부터 계산 	  
            //                    Wall zonewall = (Wall)zoneWall[n - 1];
            //                    for (int mth = 0; mth < 12; mth++)
            //                    {
            //                        zoneWalls_Is[n - 1,mth] = Convert.ToDouble(token[mth + 1]);
            //                        QSopCalc qsopcalc = new QSopCalc();
            //                        if (zonewall.DiIndi() == "Indirection")
            //                        {   //직접외기 벽만 일사 계산      
            //                        }
            //                        else
            //                        {
            //                            if (0.5 * 4.5 * 10 >= zonewall.α() * zoneWalls_Is[n - 1, mth])
            //                            {
            //                                zoneWalls_Qssink[n - 1, mth] = qsopcalc.Calc(zonewall.Ueff(), zonewall.Area(), zonewall.α(), zoneWalls_Is[n - 1, mth]);
            //                                zoneWalls_Qssource[n - 1, mth] = 0;
            //                            }
            //                            else
            //                            {
            //                                zoneWalls_Qssink[n - 1, mth] = 0;
            //                                zoneWalls_Qssource[n - 1, mth] = qsopcalc.Calc(zonewall.Ueff(), zonewall.Area(), zonewall.α(), zoneWalls_Is[n - 1, mth]);
            //                            }
            //                        }
            //                        QSopsink_Wall[mth] += zoneWalls_Qssink[n - 1, mth];
            //                        QSopsource_Wall[mth] += zoneWalls_Qssource[n - 1, mth];

            //                    }
            //                }

            //                n++;
            //            }


            //        }
            //    }
            //}
            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}

            //지붕 일사 계산
            double[,] zoneRoofs_Is = new double[zoneRoof.Count, 12];
            double[,] zoneRoofs_Qssink = new double[zoneRoof.Count, 12];
            double[,] zoneRoofs_Qssource = new double[zoneRoof.Count, 12];

            {



                int i = -1;

                string[][] ZoneR_Name = Program.DB.getValue(DB.type.ProjDB, "ZoneRoof", "Name", "zoneNum='" + zoneNum + "'");

                while (++i < zoneRoof.Count)
                {
                    Roof zoneroof = (Roof)zoneRoof[i];
                    string[][] token = Program.DB.getValue(DB.type.ProjDB, "ZoneRoof_Solar", "value", "zoneNum='" + zoneNum + "' AND 구조체='" + ZoneR_Name[i][0] + "'");

                    for (int mth = 0; mth < 12; mth++)
                    {
                        zoneRoofs_Is[i, mth] = Convert.ToDouble(token[mth][0]);
                        QSopCalc qsopcalc = new QSopCalc();
                        if (zoneroof.DiIndi() == "Indirection")
                        {   //직접외기 지붕만 일사 계산      
                        }
                        else
                        {
                            if (0.5 * 4.5 * 10 >= zoneroof.α() * zoneRoofs_Is[i, mth])
                            {
                                zoneRoofs_Qssink[i, mth] = qsopcalc.Calc(zoneroof.Ueff(), zoneroof.Area(), zoneroof.α(), zoneRoofs_Is[i, mth]);
                                zoneRoofs_Qssource[i, mth] = 0;
                            }
                            else
                            {
                                zoneRoofs_Qssink[i, mth] = 0;
                                zoneRoofs_Qssource[i, mth] = qsopcalc.Calc(zoneroof.Ueff(), zoneroof.Area(), zoneroof.α(), zoneRoofs_Is[i, mth]);
                            }
                        }
                        QSopsink_Roof[mth] += zoneRoofs_Qssink[i, mth];
                        QSopsource_Roof[mth] += zoneRoofs_Qssource[i, mth];
                    }
                }
            }


            //try
            //{
            //    String line = "";
            //    string filePath = "C:\\javalecture\\day0118\\day0118\\"+zoneNum+"\\ZoneRoof_Solar.csv";
            //    using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
            //    {
            //        using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
            //        {
            //            double[,] zoneRoofs_Is = new double[zoneRoof.Count, 12];
            //            double[,] zoneRoofs_Qssink = new double[zoneRoof.Count, 12];
            //            double[,] zoneRoofs_Qssource = new double[zoneRoof.Count, 12];
            //            int n = 0;
            //            while (!sr.EndOfStream)
            //            {
            //                string[] token = sr.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    Roof zoneroof = (Roof)zoneRoof[n - 1];
            //                    for (int mth = 0; mth < 12; mth++)
            //                    {
            //                        zoneRoofs_Is[n - 1, mth] = Convert.ToDouble(token[mth + 1]);
            //                        QSopCalc qsopcalc = new QSopCalc();
            //                        if (zoneroof.DiIndi() == "Indirection")
            //                        {   //직접외기 지붕만 일사 계산      
            //                        }
            //                        else
            //                        {
            //                            if (0.5 * 4.5 * 10 >= zoneroof.α() * zoneRoofs_Is[n - 1, mth])
            //                            {
            //                                zoneRoofs_Qssink[n - 1, mth] = qsopcalc.Calc(zoneroof.Ueff(), zoneroof.Area(), zoneroof.α(), zoneRoofs_Is[n - 1, mth]);
            //                                zoneRoofs_Qssource[n - 1, mth] = 0;
            //                            }
            //                            else
            //                            {
            //                                zoneRoofs_Qssink[n - 1, mth] = 0;
            //                                zoneRoofs_Qssource[n - 1, mth] = qsopcalc.Calc(zoneroof.Ueff(), zoneroof.Area(), zoneroof.α(), zoneRoofs_Is[n - 1, mth]);
            //                            }
            //                        }
            //                        QSopsink_Roof[mth] += zoneRoofs_Qssink[n - 1, mth];
            //                        QSopsource_Roof[mth] += zoneRoofs_Qssource[n - 1, mth];
            //                    }
            //                }
            //                n++;
            //            }


            //        }
            //    }
            //}
            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}

            //출입문 일사 계산
            double[,] zoneDoors_Is = new double[zoneDoor.Count, 12];
            double[,] zoneDoors_Qssink = new double[zoneDoor.Count, 12];
            double[,] zoneDoors_Qssource = new double[zoneDoor.Count, 12];

            {
                int i = -1;

                string[][] ZoneD_Name = Program.DB.getValue(DB.type.ProjDB, "ZoneDoor", "Name", "zoneNum='" + zoneNum + "'");

                while (++i < zoneDoor.Count)
                {
                    Door zonedoor = (Door)zoneDoor[i];
                    string[][] token = Program.DB.getValue(DB.type.ProjDB, "ZoneDoor_Solar", "value", "zoneNum='" + zoneNum + "' AND 구조체='" + ZoneD_Name[i][0] + "'");

                    for (int mth = 0; mth < 12; mth++)
                    {
                        zoneDoors_Is[i, mth] = Convert.ToDouble(token[mth][0]);
                        QSopCalc qsopcalc = new QSopCalc();
                        if (zonedoor.DiIndi() == "Indirection")
                        {   //직접외기 벽만 일사 계산      
                        }
                        else
                        {
                            if (0.5 * 4.5 * 10 >= zonedoor.α() * zoneDoors_Is[i, mth])
                            {
                                zoneDoors_Qssink[i, mth] = qsopcalc.Calc(zonedoor.Ueff(), zonedoor.Area(), zonedoor.α(), zoneDoors_Is[i, mth]);
                                zoneDoors_Qssource[i, mth] = 0;
                            }
                            else
                            {
                                zoneDoors_Qssink[i, mth] = 0;
                                zoneDoors_Qssource[i, mth] = qsopcalc.Calc(zonedoor.Ueff(), zonedoor.Area(), zonedoor.α(), zoneDoors_Is[i, mth]);
                            }
                        }
                        QSopsink_Door[mth] += zoneDoors_Qssink[i, mth];
                        QSopsource_Door[mth] += zoneDoors_Qssource[i, mth];
                    }
                }
            }

            //try
            //{
            //    String line = "";
            //    string filePath = "C:\\javalecture\\day0118\\day0118\\"+zoneNum+"\\ZoneDoor_Solar.csv";
            //    using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
            //    {
            //        using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
            //        {
            //            double[,] zoneDoors_Is = new double[zoneDoor.Count,12];
            //            double[,] zoneDoors_Qssink = new double[zoneDoor.Count,12];
            //            double[,] zoneDoors_Qssource = new double[zoneDoor.Count,12];
            //            int n = 0;
            //            while (!sr.EndOfStream)
            //            {
            //                string[] token = sr.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    Door zonedoor = (Door)zoneDoor[n - 1];
            //                    for (int mth = 0; mth < 12; mth++)
            //                    {
            //                        zoneDoors_Is[n - 1,mth] = Convert.ToDouble(token[mth + 1]);
            //                        QSopCalc qsopcalc = new QSopCalc();
            //                        if (zonedoor.DiIndi()=="Indirection")
            //                        {   //직접외기 벽만 일사 계산      
            //                        }
            //                        else
            //                        {
            //                            if (0.5 * 4.5 * 10 >= zonedoor.α() * zoneDoors_Is[n - 1,mth])
            //                            {
            //                                zoneDoors_Qssink[n - 1,mth] = qsopcalc.Calc(zonedoor.Ueff(), zonedoor.Area(), zonedoor.α(), zoneDoors_Is[n - 1,mth]);
            //                                zoneDoors_Qssource[n - 1,mth] = 0;
            //                            }
            //                            else
            //                            {
            //                                zoneDoors_Qssink[n - 1,mth] = 0;
            //                                zoneDoors_Qssource[n- 1,mth] = qsopcalc.Calc(zonedoor.Ueff(), zonedoor.Area(), zonedoor.α(), zoneDoors_Is[n - 1,mth]);
            //                            }
            //                        }
            //                        QSopsink_Door[mth] += zoneDoors_Qssink[n - 1,mth];
            //                        QSopsource_Door[mth] += zoneDoors_Qssource[n - 1,mth];
            //                    }
            //                }
            //                n++;
            //            }
            //        }
            //    }
            //}
            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}

            //커튼월 패널 일사 계산
            double[,] zoneCWs_Is = new double[zoneCW.Count, 12];
            double[,] zoneCWs_Qssink = new double[zoneCW.Count, 12];
            double[,] zoneCWs_Qssource = new double[zoneCW.Count, 12];

            {
                int i = -1;

                string[][] ZoneCW_Name = Program.DB.getValue(DB.type.ProjDB, "ZoneCW", "Name", "zoneNum='" + zoneNum + "'");

                while (++i < zoneCW.Count)
                {
                    CW zonecw = (CW)zoneCW[i];
                    string[][] token = Program.DB.getValue(DB.type.ProjDB, "ZoneCW_Solar", "value", "zoneNum='" + zoneNum + "' AND 구조체='" + ZoneCW_Name[i][0] + "'");

                    for (int mth = 0; mth < 12; mth++)
                    {
                        zoneCWs_Is[i, mth] = Convert.ToDouble(token[mth][0]);
                        QSopCalc qsopcalc = new QSopCalc();
                        if (0.5 * 4.5 * 10 >= zonecw.α_p() * zoneCWs_Is[i, mth])
                        {
                            zoneCWs_Qssink[i, mth] = qsopcalc.Calc(zonecw.Uvalue_p(), zonecw.Area_p(), zonecw.α_p(), zoneCWs_Is[i, mth]);
                            zoneCWs_Qssource[i, mth] = 0;
                        }
                        else
                        {
                            zoneCWs_Qssink[i, mth] = 0;
                            zoneCWs_Qssource[i, mth] = qsopcalc.Calc(zonecw.Uvalue_p(), zonecw.Area_p(), zonecw.α_p(), zoneCWs_Is[i, mth]);
                        }
                        QSopsink_CW_p[mth] += zoneCWs_Qssink[i, mth];
                        QSopsource_CW_p[mth] += zoneCWs_Qssource[i, mth];
                    }
                }
            }


            //try
            //{
            //    String line = "";
            //    string filePath = "C:\\javalecture\\day0118\\day0118\\"+zoneNum+"\\ZoneCW_Solar.csv";
            //    using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
            //    {
            //        using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            double[,] zoneCWs_Is = new double[zoneCW.Count,12];
            //            double[,] zoneCWs_Qssink = new double[zoneCW.Count,12];
            //            double[,] zoneCWs_Qssource = new double[zoneCW.Count,12];

            //            while (!sr.EndOfStream)
            //            {
            //                string[] token = sr.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    CW zonecw = (CW)zoneCW[n - 1];
            //                    for (int mth = 0; mth < 12; mth++)
            //                    {
            //                        zoneCWs_Is[n - 1,mth] = Convert.ToDouble(token[mth + 1]);
            //                        QSopCalc qsopcalc = new QSopCalc();
            //                        if (0.5 * 4.5 * 10 >= zonecw.α_p() * zoneCWs_Is[n - 1,mth])
            //                        {
            //                            zoneCWs_Qssink[n - 1,mth] = qsopcalc.Calc(zonecw.Uvalue_p(), zonecw.Area_p(), zonecw.α_p(), zoneCWs_Is[n - 1,mth]);
            //                            zoneCWs_Qssource[n - 1,mth] = 0;
            //                        }
            //                        else
            //                        {
            //                            zoneCWs_Qssink[n - 1,mth] = 0;
            //                            zoneCWs_Qssource[n - 1,mth] = qsopcalc.Calc(zonecw.Uvalue_p(), zonecw.Area_p(), zonecw.α_p(), zoneCWs_Is[n - 1,mth]);
            //                        }
            //                        QSopsink_CW_p[mth] += zoneCWs_Qssink[n - 1,mth];
            //                        QSopsource_CW_p[mth] += zoneCWs_Qssource[n - 1,mth];
            //                    }
            //                }
            //                n++;
            //            }
            //        }
            //    }
            //}
            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}

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

        public void ZoneQStr(String zoneNum) //투명구조체 일사 계산
        {
            double[,] zoneWins_Is = new double[zoneWin.Count, 12];
            double[,] zoneWins_Fs = new double[zoneWin.Count, 12];
            double[,] zoneWins_a = new double[zoneWin.Count, 12];
            double[,,] zoneWins_geff = new double[zoneWin.Count, 2, 12];
            double[,,] zoneWins_Qs = new double[zoneWin.Count, 2, 12];

            string[][] ZoneWin_Name = Program.DB.getValue(DB.type.ProjDB, "ZoneWin", "Name", "zoneNum='" + zoneNum + "'");
            string[][] ZoneCW_Name = Program.DB.getValue(DB.type.ProjDB, "ZoneCW", "Name", "zoneNum='" + zoneNum + "'");

            //존의 창별 일사정보 가져오기
            {
                int i = -1;

                while (++i < zoneWin.Count)
                {
                    string[][] token = Program.DB.getValue(DB.type.ProjDB, "ZoneWin_Solar", "value", "zoneNum='" + zoneNum + "' AND 구조체='" + ZoneWin_Name[i][0] + "'");

                    for (int mth = 0; mth < 12; mth++)
                    {
                        zoneWins_Is[i, mth] = Convert.ToDouble(token[mth][0]);
                    }
                }
            }

            //        try
            //        {
            //    String line = "";
            //    string filePath = "C:\\javalecture\\day0118\\day0118\\"+zoneNum+"\\ZoneWin_Solar.csv";
            //    using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
            //    {
            //        using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            while (!sr.EndOfStream)
            //            {
            //                string[] token = sr.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    Window zonewin = (Window)zoneWin[n - 1];
            //                    for (int mth = 0; mth < 12; mth++)
            //                    {
            //                        zoneWins_Is[n - 1, mth] = Convert.ToDouble(token[mth + 1]);
            //                    }
            //                }
            //                n++;
            //            }
            //        }
            //    }
            //}
            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}

            //존의 창별 음영정보 가져오기
            {
                int i = -1;

                while (++i < zoneWin.Count)
                {
                    string[][] token = Program.DB.getValue(DB.type.ProjDB, "ZoneWin_Shadow", "value", "zoneNum='" + zoneNum + "' AND 구조체='" + ZoneWin_Name[i][0] + "'");

                    for (int mth = 0; mth < 12; mth++)
                    {
                        zoneWins_Fs[i, mth] = Convert.ToDouble(token[mth][0]);
                    }
                }
            }

            //try
            //{
            //    String line = "";
            //    string filePath = "C:\\javalecture\\day0118\\day0118\\"+zoneNum+"\\ZoneWin_Shadow.csv";
            //    using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
            //    {
            //        using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            while (!sr.EndOfStream)
            //            {
            //                string[] token = sr.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    Window zonewin = (Window)zoneWin[n - 1];
            //                    for (int mth = 0; mth < 12; mth++)
            //                    {
            //                        zoneWins_Fs[n - 1, mth] = Convert.ToDouble(token[mth + 1]);
            //                    }
            //                }
            //                n++;
            //            }
            //        }
            //    }
            //}
            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}

            //존의 창별 가동계수정보 가져오기
            {
                int i = -1;

                while (++i < zoneWin.Count)
                {
                    string[][] token = Program.DB.getValue(DB.type.ProjDB, "ZoneWin_a", "value", "zoneNum='" + zoneNum + "' AND 구조체='" + ZoneWin_Name[i][0] + "'");

                    for (int mth = 0; mth < 12; mth++)
                    {
                        zoneWins_a[i, mth] = Convert.ToDouble(token[mth][0]);
                    }
                }
            }
            //try
            //{
            //    String line = "";
            //    string filePath = "C:\\javalecture\\day0118\\day0118\\"+zoneNum+"\\ZoneWin_a.csv";
            //    using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
            //    {
            //        using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            while (!sr.EndOfStream)
            //            {
            //                string[] token = sr.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    Window zonewin = (Window)zoneWin[n - 1];
            //                    for (int mth = 0; mth < 12; mth++)
            //                    {
            //                        zoneWins_a[n - 1, mth] = Convert.ToDouble(token[mth + 1]);
            //                    }
            //                }
            //                n++;
            //            }
            //        }
            //    }
            //}
            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}

            // 창 일사 계산 
            for (int i = 0; i < zoneWin.Count; i++)
            {
                Window zonewin = (Window)zoneWin[i];
                for (int mth = 0; mth < 12; mth++)
                {
                    GeffCalc geffcalc = new GeffCalc();
                    QStrCalc qstrcalc = new QStrCalc();
                    zoneWins_geff[i, 0, mth] = geffcalc.Calc(zonewin.g(), zoneWins_Fs[i, mth]);
                    zoneWins_geff[i, 1, mth] = geffcalc.Calc(zonewin.g(), zoneWins_Fs[i, mth]);
                    zoneWins_geff[i, 1, mth] = geffcalc.Calc(zonewin.g(), zoneWins_Fs[i, mth], zonewin.gtot(), zoneWins_a[i, mth]);
                    if (zonewin.DiIndi() == "Indirection")
                    {   //직접외기 창만 일사 계산      
                    }
                    else
                    {
                        zoneWins_Qs[i, 0, mth] = qstrcalc.Calc(zonewin.Ff(), zonewin.Area(), zoneWins_geff[i, 0, mth], zoneWins_Is[i, mth]);
                        zoneWins_Qs[i, 1, mth] = qstrcalc.Calc(zonewin.Ff(), zonewin.Area(), zoneWins_geff[i, 1, mth], zoneWins_Is[i, mth]);
                    }
                    QStr_Win[0, mth] += zoneWins_Qs[i, 0, mth];
                    QStr_Win[1, mth] += zoneWins_Qs[i, 1, mth];
                }

            }

            double[,] zoneCWs_Is = new double[zoneWin.Count, 12];
            double[,] zoneCWs_Fs = new double[zoneWin.Count, 12];
            double[,] zoneCWs_a = new double[zoneWin.Count, 12];
            double[,,] zoneCWs_g_geff = new double[zoneWin.Count, 2, 12];
            double[,] zoneCWs_d_geff = new double[zoneWin.Count, 12];
            double[,,] zoneCWs_g_Qs = new double[zoneWin.Count, 2, 12];
            double[,] zoneCWs_d_Qs = new double[zoneWin.Count, 12];

            //존의 커튼월별 일사정보 가져오기
            {
                int i = -1;

                while (++i < zoneCW.Count)
                {
                    string[][] token = Program.DB.getValue(DB.type.ProjDB, "ZoneCW_Solar", "value", "zoneNum='" + zoneNum + "' AND 구조체='" + ZoneCW_Name[i][0] + "'");

                    for (int mth = 0; mth < 12; mth++)
                    {
                        zoneCWs_Is[i, mth] = Convert.ToDouble(token[mth][0]);
                    }
                }
            }
            //try
            //{
            //    String line = "";
            //    string filePath = "C:\\javalecture\\day0118\\day0118\\"+zoneNum+"\\ZoneCW_Solar.csv";
            //    using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
            //    {
            //        using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            while (!sr.EndOfStream)
            //            {
            //                string[] token = sr.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    CW zonecw = (CW)zoneCW[n - 1];
            //                    for (int mth = 0; mth < 12; mth++)
            //                     {
            //                        zoneCWs_Is[n - 1,mth] = Convert.ToDouble(token[mth + 1]);
            //                     }
            //                }
            //                n++;
            //            }
            //        }
            //    }
            //}
            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}

            //존의 커튼월별 음영정보 가져오기
            {
                int i = -1;

                while (++i < zoneCW.Count)
                {
                    string[][] token = Program.DB.getValue(DB.type.ProjDB, "ZoneCW_shadow", "value", "zoneNum='" + zoneNum + "' AND 구조체='" + ZoneCW_Name[i][0] + "'");

                    for (int mth = 0; mth < 12; mth++)
                    {
                        zoneCWs_Fs[i, mth] = Convert.ToDouble(token[mth][0]);
                    }
                }
            }
            //try
            //{
            //    String line = "";
            //    string filePath = "C:\\javalecture\\day0118\\day0118\\"+zoneNum+"\\ZoneCW_shadow.csv";
            //    using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
            //    {
            //        using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
            //        {
            //            int n = 0;
            //            while (!sr.EndOfStream)
            //            {
            //                string[] token = sr.ReadLine().Split(',');
            //                if (n == 0)
            //                {
            //                }
            //                else
            //                {
            //                    CW zonecw = (CW)zoneCW[n - 1];
            //                    for (int mth = 0; mth < 12; mth++)
            //                     {
            //                       zoneCWs_Fs[n - 1,mth] = Convert.ToDouble(token[mth + 1]);
            //                     }
            //                 }
            //                n++;
            //        }
            //    }
            //}
            //}
            //catch (IOException e)
            //{
            //    if (e.Source != null)
            //        Console.WriteLine("IOException source: {0}", e.Source);
            //    throw;
            //}

            //존의 커튼월별 가동계수정보 가져오기
            {
                int i = -1;

                while (++i < zoneCW.Count)
                {
                    string[][] token = Program.DB.getValue(DB.type.ProjDB, "ZoneCW_a", "value", "zoneNum='" + zoneNum + "' AND 구조체='" + ZoneCW_Name[i][0] + "'");

                    for (int mth = 0; mth < 12; mth++)
                    {
                        zoneCWs_a[i, mth] = Convert.ToDouble(token[mth][0]);
                    }
                }
            }
            //-            try
            //            {
            //                String line = "";
            //                string filePath = "C:\\javalecture\\day0118\\day0118\\"+zoneNum+"\\ZoneCW_a.csv";
            //                using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
            //                {
            //                    using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
            //                    {
            //                        int n = 0;
            //                        while (!sr.EndOfStream)
            //                        {
            //                            string[] token = sr.ReadLine().Split(',');
            //                            if (n == 0)
            //                            {
            //                            }
            //                            else
            //                            {
            //                                CW zonecw = (CW)zoneCW[n - 1];
            //                                for (int mth = 0; mth < 12; mth++)
            //                                 {
            //                                  zoneCWs_a[n - 1,mth] = Convert.ToDouble(token[mth + 1]);
            //                                 }
            //                            }
            //                            n++;
            //                        }
            //                    }
            //                }
            //            }
            //            catch (IOException e)
            //            {
            //                if (e.Source != null)
            //                    Console.WriteLine("IOException source: {0}", e.Source);
            //                throw;
            //            }

            // 커튼월 일사 계산 
            for (int i = 0; i < zoneCW.Count; i++)
            {
                CW zonecw = (CW)zoneCW[i];
                for (int mth = 0; mth < 12; mth++)
                {
                    GeffCalc geffcalc = new GeffCalc();
                    QStrCalc qstrcalc = new QStrCalc();
                    zoneCWs_g_geff[i, 0, mth] = geffcalc.Calc(zonecw.g_g(), zoneCWs_Fs[i, mth]);    //비이용일   
                    zoneCWs_g_geff[i, 1, mth] = geffcalc.Calc(zonecw.g_g(), zoneCWs_Fs[i, mth]);    //이용일 차양없을 경우	
                    zoneCWs_g_geff[i, 1, mth] = geffcalc.Calc(zonecw.g_g(), zoneCWs_Fs[i, mth], zonecw.gtot_g(), zoneCWs_a[i, mth]);     //이용일 차양있을 경우

                    zoneCWs_d_geff[i, mth] = geffcalc.Calc(zonecw.g_d(), zoneCWs_Fs[i, mth]);
                    zoneCWs_g_Qs[i, 0, mth] = qstrcalc.Calc(zonecw.Ff_g(), zonecw.Area_g(), zoneCWs_g_geff[i, 0, mth], zoneCWs_Is[i, mth]);
                    zoneCWs_g_Qs[i, 1, mth] = qstrcalc.Calc(zonecw.Ff_g(), zonecw.Area_g(), zoneCWs_g_geff[i, 1, mth], zoneCWs_Is[i, mth]);
                    zoneCWs_d_Qs[i, mth] = qstrcalc.Calc(zonecw.Ff_d(), zonecw.Area_d(), zoneCWs_d_geff[i, mth], zoneCWs_Is[i, mth]);

                    QStr_CW[0, mth] += (zoneCWs_g_Qs[i, 0, mth] + zoneCWs_d_Qs[i, mth]);
                    QStr_CW[1, mth] += (zoneCWs_g_Qs[i, 1, mth] + zoneCWs_d_Qs[i, mth]);
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
            }

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
                        theta_v_mech[0, mth] = theta_e[mth] + eta_V_mech * (theta_i_h_set - theta_e[mth]);
                        theta_v_mech[1, mth] = theta_e[mth] + eta_V_mech * (theta_i_c_set - theta_e[mth]);

                        QVCalc qvcalc = new QVCalc();
                        if (theta_i[hc, wewd, mth] >= theta_e[mth])
                        {
                            QV_inf_sink[hc, wewd, mth] = qvcalc.Calc_sink(theta_e[mth], theta_i[hc, wewd, mth], Zone_HV_inf[wewd]);
                            QV_z_sink[hc, wewd, mth] = qvcalc.Calc_sink(theta_e[mth], theta_i[hc, wewd, mth], Zone_HV_z[wewd]);
                            QV_win_sink[hc, wewd, mth] = qvcalc.Calc_sink(theta_e[mth], theta_i[hc, wewd, mth], Zone_HV_win[wewd]);
                        }
                        else if (theta_i[hc, wewd, mth] < theta_e[mth])
                        {
                            QV_inf_source[hc, wewd, mth] = qvcalc.Calc_source(theta_e[mth], theta_i[hc, wewd, mth], Zone_HV_inf[wewd]);
                            QV_z_source[0, 1, mth] = qvcalc.Calc_source(theta_e[mth], theta_i[hc, wewd, mth], Zone_HV_z[wewd]);
                            QV_win_source[hc, wewd, mth] = qvcalc.Calc_source(theta_e[mth], theta_i[hc, wewd, mth], Zone_HV_win[wewd]);
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
        }

        public void ZoneQI() //내부발열 계산
        {
            //비이용일
            QI_P[0] = 0;
            QI_fac[0] = 0;
            //이용일
            QI_P[1] = qI_p * zoneArea;
            QI_fac[1] = qI_fac * zoneArea;

            for (int hc = 0; hc <= 1; hc++)
            {
                for (int wewd = 0; wewd <= 1; wewd++)
                {
                    for (int mth = 0; mth <= 11; mth++)
                    {
                        QI_L[hc, wewd, mth] = 0;
                        QI_tot[hc, wewd, mth] = QI_P[wewd] + QI_fac[wewd] + QI_L[hc, wewd, mth];
                    }
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
                dwd_mth[mth] = dmth[mth] * dwd_a / 365;
                dwe_mth[mth] = dmth[mth] - dwd_mth[mth];
                Qsink[0, 0, mth] = QTsink_tot[0, 0, mth] + QVsink_tot[0, 0, mth] + QSopsink_tot[0, 0, mth];
                Qsource[0, 0, mth] = QTsource_tot[0, 0, mth] + QVsource_tot[0, 0, mth] + QSopsource_tot[0, 0, mth] + QStr_tot[0, 0, mth] + QI_tot[0, 0, mth];
                gamma[0, 0, mth] = Qsource[0, 0, mth] / Qsink[0, 0, mth];
                a[0, 0, mth] = 1 + tao[0] / 16;
                eta[0, 0, mth] = eta_calc.eta_h_Calc(gamma[0, 0, mth], a[0, 0, mth]);
                dQc_b[0, 0, mth] = dQc_bcalc.Calc(Cwirk_A * zoneArea, theta_i_h_set, theta_i[0, 0, mth], awe, dtheta_i_NA, Qsink[0, 0, mth], eta[0, 0, mth], Qsource[0, 0, mth]);
                dQc_sink[0, 1, mth] = dQc_b[0, 0, mth] * dwe_mth[mth] / dwd_mth[mth];
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
                        a[hc, wewd, mth] = 1 + tao[wewd] / 16;
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
                Qhb_we_day[mth] = qbcalc.Qhb_Calc(Qsink[0, 0, mth], eta[0, 0, mth], Qsource[0, 0, mth]);
                Qhb_wd_day[mth] = qbcalc.Qhb_Calc(Qsink[0, 1, mth], eta[0, 1, mth], Qsource[0, 1, mth]);
                Qcb_we_day[mth] = qbcalc.Qcb_Calc(eta[1, 0, mth], Qsource[1, 0, mth]);
                Qcb_wd_day[mth] = qbcalc.Qcb_Calc(eta[1, 1, mth], Qsource[1, 1, mth]);

                Qhb_we_mth[mth] = (Qhb_we_day[mth] * dwe_mth[mth] - dQc_b[0, 0, mth]) / 1000; //kWh 단위
                Qhb_wd_mth[mth] = (Qhb_wd_day[mth] * dwd_mth[mth] + dQc_sink[0, 1, mth]) / 1000;
                Qcb_we_mth[mth] = (Qcb_we_day[mth] * dwe_mth[mth]) / 1000;
                Qcb_wd_mth[mth] = (Qcb_wd_day[mth] * dwd_mth[mth]) / 1000;

                if (Double.IsNaN(Qhb_we_mth[mth]))
                {
                    Qhb_we_mth[mth] = 0;
                }
                else
                {
                    Qhb_we_mth[mth] = Qhb_we_mth[mth];
                }
                if (Double.IsNaN(Qhb_wd_mth[mth]))
                {
                    Qhb_wd_mth[mth] = 0;
                }
                else
                {
                    Qhb_wd_mth[mth] = Qhb_wd_mth[mth];
                }
                if (Double.IsNaN(Qcb_we_mth[mth]))
                {
                    Qcb_we_mth[mth] = 0;
                }
                else
                {
                    Qcb_we_mth[mth] = Qcb_we_mth[mth];
                }
                if (Double.IsNaN(Qcb_wd_mth[mth]))
                {
                    Qcb_wd_mth[mth] = 0;
                }
                else
                {
                    Qcb_wd_mth[mth] = Qcb_wd_mth[mth];
                }
                Qhb_we_a += Qhb_we_mth[mth];
                Qhb_wd_a += Qhb_wd_mth[mth];
                Qcb_we_a += Qcb_we_mth[mth];
                Qcb_wd_a += Qcb_wd_mth[mth];
                Qhb_a += (Qhb_we_mth[mth] + Qhb_wd_mth[mth]);
                Qcb_a += (Qcb_we_mth[mth] + Qcb_wd_mth[mth]);
            }
        }


    }

    public class Wall
    {
        double wall_Area;
        double wall_Ueff;
        double wall_α;
        string wall_DiIndi;


        public Wall(double Area, double Ueff, double α, string DiIndi)
        {
            this.wall_Area = Area;
            this.wall_Ueff = Ueff;
            this.wall_α = α;
            this.wall_DiIndi = DiIndi;
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

    }

    public class Roof
    {
        double Roof_Area;
        double Roof_Ueff;
        double Roof_α;
        String Roof_DiIndi;

        public Roof(double Area, double Ueff, double α, String DiIndi)
        {
            this.Roof_Area = Area;
            this.Roof_Ueff = Ueff;
            this.Roof_α = α;
            this.Roof_DiIndi = DiIndi;
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

    }

    public class Window
    {
        double Window_Area;
        double Window_Uvalue;
        double Window_Uinst;
        String Window_DiIndi;
        double Window_Ff;
        double Window_g;
        double Window_tao;
        double Window_gtot;
        double Window_taotot;

        public Window(double Area, double Uvalue, double Uinst, String DiIndi, double Ff, double g, double tao)
        {
            this.Window_Area = Area;
            this.Window_Uvalue = Uvalue;
            this.Window_Uinst = Uinst;
            this.Window_DiIndi = DiIndi;
            this.Window_Ff = Ff;
            this.Window_g = g;
            this.Window_tao = tao;
            //this.Window_gtot = gtot;
           // this.Window_taotot = taotot;
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

    }

    public class CW
    {
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

        public CW(double Area_g, double Uvalue_g, double Ff_g, double g_g, double gtot_g, double tao_g, double taotot_g, double Area_p, double Uvalue_p, double α_p, double Area_d, double Uvalue_d, double Ff_d, double g_d, double tao_d, double Area_tot, double Uinst)
        {
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
    }

    public class Door
    {
        double Door_Area;
        double Door_Ueff;
        double Door_α;
        String Door_DiIndi;

        public Door(double Area, double Ueff, double α, String DiIndi)
        {
            this.Door_Area = Area;
            this.Door_Ueff = Ueff;
            this.Door_α = α;
            this.Door_DiIndi = DiIndi;
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

    }

    public class Floor
    {
        double Floor_Area;
        double Floor_Ueff;

        public Floor(double Area, double Ueff)
        {
            this.Floor_Area = Area;
            this.Floor_Ueff = Ueff;
        }

        public double Area()
        {
            return Floor_Area;
        }
        public double Ueff()
        {
            return Floor_Ueff;
        }
    }

    public class GWall
    {
        double GWall_Area;
        double GWall_Ueff;

        public GWall(double Area, double Ueff)
        {
            this.GWall_Area = Area;
            this.GWall_Ueff = Ueff;
        }

        public double Area()
        {
            return GWall_Area;
        }
        public double Ueff()
        {
            return GWall_Ueff;
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

        public double HV_mech_Calc(double Vmech_SUP, double tV_mech, double V)
        {
            double nmech_SUP = Vmech_SUP / V;
            double nmech = nmech_SUP * tV_mech / 24;
            double HV_mech = nmech * V * cpaρa;
            return HV_mech;
        }

        public double HV_z_Calc(double Vmech_SUP, double Vmech_ETA, double tV_mech, double V)
        {
            double nmech_SUP = Vmech_SUP / V;
            double nmech_ETA = Vmech_ETA / V;
            double nz_SUP = nmech_ETA - nmech_SUP;
            double nz_d = nz_SUP * tV_mech / 24;
            double HV_z = nz_d * V * cpaρa;
            return HV_z;
        }
        public double HV_inf_Calc(double Vmech_SUP, double Vmech_ETA, double Vmech_SUP_z, double Vmech_ETA_z, double tV_mech, double n50, double V, double e, double f)
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
                fe = 1 / (1 + f / e * Math.Pow(((nETA - nSUP) / n50), 2));
                ninf = n50 * e * (1 + (fe - 1) * tV_mech / 24);
            }
            double HV_inf = ninf * V * cpaρa;
            return HV_inf;
        }

        public double HV_win_Calc(double Vmech_SUP, double Vmech_ETA, double Vmech_SUP_z, double Vmech_ETA_z, double tV_mech, double twd, double n50, double nwd, double V, double e, double f)
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

            double HV_win = nwin * V * cpaρa;
            return HV_win;
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

            if (Mode_we == "reduced operation")
            {
                f_we = 0.2 * (1 - 0.4 * tao / 250);
            }
            else if (Mode_we == "stop operation")
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

            if (Mode_wd == "reduced operation")
            {
                f_wd = 0.13 * tNA / 24 * Math.Exp((-tao / 250));
            }
            else if (Mode_wd == "stop operation")
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
            double theta_i_c = theta_i_c_set - 2;

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
    }

    public class QSopCalc
    {
        double Rse = 0.04;
        double Uvalue;
        double Area;
        double Ff = 0.5;
        double hr = 4.5;
        double dtheta_er = 10;
        double α;
        double IS;

        public double Calc(double Uvalue, double Area, double α, double IS)
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
            return eta;
        }

    }

    public class dQc_bCalc
    {
        public double Calc(double Cwirk, double theta_i_h_set, double theta_i_h, double awe, double Δtheta_i_NA, double Qsink, double η, double Qsource)
        {
            double dQc_b;
            dQc_b = Math.Min(Math.Min((Cwirk * 2 * (theta_i_h_set - theta_i_h) / awe), (Cwirk * Δtheta_i_NA / awe)), (Qsink - η * Qsource));
            return dQc_b;
        }

    }

    public class QbCalc
    {
        public double Qhb_Calc(double Qsink, double η, double Qsource)
        {
            double Qhb;
            Qhb = Qsink - η * Qsource;
            return Qhb;
        }

        public double Qcb_Calc(double η, double Qsource)
        {
            double Qcb;
            Qcb = (1 - η) * Qsource;
            return Qcb;
        }
    }

}
