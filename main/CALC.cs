using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    internal class CALC
    {
        /////////////////////////////////////////////////////////////////////////////////////
        // calculation functions start

        private static string zoneNum = "3F_Zone01";

        public void init()
        {
            _calculations["셈플: CSV 를 메모리DB에 로딩..."] = new Func<bool>(LoadMemDB_example);

            _calculations["존 HT"] = new Func<bool>(ZoneHT);
            _calculations["존 HV"] = new Func<bool>(ZoneHV);
            _calculations["존 tao"] = new Func<bool>(Zonetao);
            _calculations["존 thetai"] = new Func<bool>(Zonethetai);
            _calculations["존 QT"] = new Func<bool>(ZoneQT);
            _calculations["존 QV"] = new Func<bool>(ZoneQV);
            _calculations["존 QSop"] = new Func<bool>(ZoneQSop);
            _calculations["존 QStr"] = new Func<bool>(ZoneQStr);
            _calculations["존 QI"] = new Func<bool>(ZoneQI);
            _calculations["존 eta"] = new Func<bool>(Zoneeta);
            _calculations["존 Qb"] = new Func<bool>(ZoneQb);
        }
        private static bool LoadMemDB_example()
        {
            string filePath = Program.gPath + "calculations\\Zone.csv";
            using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
                {
                    int n = 0;
                    while (!sr.EndOfStream)
                    {
                        string[] token = sr.ReadLine().Split(',');
                        if (n == 0)
                        {
                        }
                        else
                        {
                            Program.DB.setValue(DB.type.ProjDB, "Zone", "zoneNum", "'" + token[0] + "'", "zoneNum");
                            zoneNum = token[0];

                            //외기온도 가져오기
                            try
                            {
                                string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\OutairTemperature.csv";
                                using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
                                {
                                    using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
                                    {
                                        int n2 = 0;
                                        while (!sr2.EndOfStream)
                                        {
                                            string[] token2 = sr2.ReadLine().Split(',');
                                            if (n2 == 0)
                                            {
                                            }
                                            else
                                            {
                                                Program.DB.setValue(DB.type.ProjDB, "OutairTemperature", "zoneNum,월,온도,일", "'" + zoneNum + "','" + token2[0] + "'," + token2[1] + "," + token2[2], "zoneNum,월");
                                            }
                                            n2++;

                                        }
                                    }
                                }
                            }
                            catch (IOException e)
                            {
                                if (e.Source != null)
                                    Console.WriteLine("IOException source: {0}", e.Source);
                                throw;
                            }

                            //존 정보 가져오기
                            try
                            {
                                string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\Zonegeneral.csv";
                                using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
                                {
                                    using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
                                    {
                                        int n2 = 0;
                                        while (!sr2.EndOfStream)
                                        {
                                            string[] token2 = sr2.ReadLine().Split(',');
                                            if (n2 == 0)
                                            {
                                            }
                                            else
                                            {
                                                int i = -1;
                                                string s = "";

                                                while(++i < 41)
                                                {
                                                    switch(i)
                                                    {
                                                        case 8:
                                                            token2[i] = "0.8";
                                                            break;
                                                        case 11:
                                                            token2[i] = "18";
                                                            break;
                                                        case 13:
                                                            token2[i] = "-12";
                                                            break;
                                                        case 14:
                                                            token2[i] = "18";
                                                            break;
                                                        case 18:
                                                            token2[i] = "0";
                                                            break;
                                                    }
                                                    s += "'" + token2[i] + "',";
                                                }

                                                s += "'0.34'";

                                                Program.DB.setValue(DB.type.ProjDB, "Zonegeneral", "구분,zoneNum,zoneName,zoneUsage,zoneHC,θi_h_set,θi_c_set,Δθi_NA,Fx,Fx_fl,Fx_wl,θs_c,θi_h_min,θe_min,θSUP_Wi,Mode_night,Mode_we,twd_d,th_op_d_we,th_op_d,dwd_a,ZoneArea,zoneHeight,qI_p,qI_fac,Cwirk_A,VA_we,VA_wd,n50,e,f,Vmech_SUP_we,Vmech_SUP_wd,Vmech_ETA_we,Vmech_ETA_wd,ηV_mech,ηχV_mech,χi_c_set,χi_h_set,Vmech_SUP_z,Vmech_ETA_z,ρacp_a",s, "zoneNum");

                                                //string[][] res = Program.DB.getValue(DB.type.ProjDB, "Zonegeneral", "구분,zoneNum");

                                                //if (res.Count() > 0 && res[0].Count() > 0)
                                                //{
                                                //    MessageBox.Show(res[0][0]);
                                                //}

                                                //zoneNum = token[1];
                                                //zoneName = token[2];
                                                //zoneUsage = token[3];
                                                //zoneHC = token[4];
                                                //theta_i_h_set = Convert.ToDouble(token[5]);
                                                //theta_i_c_set = Convert.ToDouble(token[6]);
                                                //dtheta_i_NA = Convert.ToDouble(token[7]);
                                                //Fx = 0.8;
                                                //Fx_Floor = Convert.ToDouble(token[9]);
                                                //Fx_GWall = Convert.ToDouble(token[10]);
                                                //theta_s_c = 18;
                                                //theta_i_h_min = Convert.ToDouble(token[12]);
                                                //theta_e_min = -12;
                                                //theta_SUP_Wi = 18;
                                                //Mode_night = token[15];
                                                //Mode_we = token[16];
                                                //twd_d = Convert.ToDouble(token[17]);
                                                //th_op_d_we = 0;
                                                //th_op_d = Convert.ToDouble(token[19]);
                                                //dwd_a = Convert.ToDouble(token[20]);
                                                //zoneArea = Convert.ToDouble(token[21]);
                                                //zoneHeight = Convert.ToDouble(token[22]);
                                                //qI_p = Convert.ToDouble(token[23]);
                                                //qI_fac = Convert.ToDouble(token[24]);
                                                //Cwirk_A = Convert.ToDouble(token[25]);
                                                //VA_we = Convert.ToDouble(token[26]);
                                                //VA_wd = Convert.ToDouble(token[27]);
                                                //n50 = Convert.ToDouble(token[28]);
                                                //e = Convert.ToDouble(token[29]);
                                                //f = Convert.ToDouble(token[30]);
                                                //Vmech_SUP_we = Convert.ToDouble(token[31]);
                                                //Vmech_SUP_wd = Convert.ToDouble(token[32]);
                                                //Vmech_ETA_we = Convert.ToDouble(token[33]);
                                                //Vmech_ETA_wd = Convert.ToDouble(token[34]);
                                                //eta_V_mech = Convert.ToDouble(token[35]);
                                                //eta_χV_mech = Convert.ToDouble(token[36]);
                                                //xi_c_set = Convert.ToDouble(token[37]);
                                                //xi_h_set = Convert.ToDouble(token[38]);
                                                //Vmech_SUP_z = Convert.ToDouble(token[39]);
                                                //Vmech_ETA_z = Convert.ToDouble(token[40]);
                                                //ρacp_a = 0.34;
                                            }
                                            n2++;

                                        }
                                    }
                                }


                            }
                            catch (IOException e)
                            {
                                if (e.Source != null)
                                    Console.WriteLine("IOException source: {0}", e.Source);
                                throw;
                            }

                            //존 외벽 정보 가져오기
                            try
                            {
                                string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ZoneWall.csv";
                                using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
                                {
                                    using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
                                    {
                                        int n2 = 0;
                                        while (!sr2.EndOfStream)
                                        {
                                            string[] token2 = sr2.ReadLine().Split(',');
                                            if (n2 == 0)
                                            {
                                            }
                                            else
                                            {

                                                Program.DB.setValue(DB.type.ProjDB, "ZoneWall", "zoneNum,Name,Area,Ueff,DirectInDirect,Direction,α,Degree",
                                                    "'" + zoneNum + "','" + token2[0] + "','" + token2[1] + "','" + token2[2] + "','" + token2[3] + "','" + token2[4] + "','" + token2[5] + "','" + token2[6] + "'", "zoneNum,Name");

                                                //         Wall wall = new Wall(Convert.ToDouble(token[1]), Convert.ToDouble(token[2]), Convert.ToDouble(token[5]), token[3]);
                                                //       zoneWall.Add(wall);
                                            }
                                            n++;

                                        }
                                    }
                                }


                            }
                            catch (IOException e)
                            {
                                if (e.Source != null)
                                    Console.WriteLine("IOException source: {0}", e.Source);
                                throw;
                            }

                            //존 지붕 정보 가져오기
                            try
                            {

                                string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ZoneRoof.csv";
                                using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
                                {
                                    using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
                                    {
                                        int n2 = 0;
                                        while (!sr2.EndOfStream)
                                        {
                                            string[] token2 = sr2.ReadLine().Split(',');
                                            if (n2 == 0)
                                            {
                                            }
                                            else
                                            {
                                                Program.DB.setValue(DB.type.ProjDB, "ZoneRoof", "zoneNum,Name,Area,Ueff,DirectInDirect,Direction,α,Degree",
                                                    "'" + zoneNum + "','" + token2[0] + "','" + token2[1] + "','" + token2[2] + "','" + token2[3] + "','" + token2[4] + "','" + token2[5] + "','" + token2[6] + "'", "zoneNum,Name");

                                                //      Roof roof = new Roof(Convert.ToDouble(token[1]), Convert.ToDouble(token[2]), Convert.ToDouble(token[5]), token[3]);
                                                //    zoneRoof.Add(roof);
                                            }
                                            n2++;

                                        }
                                    }
                                }


                            }
                            catch (IOException e)
                            {
                                if (e.Source != null)
                                    Console.WriteLine("IOException source: {0}", e.Source);
                                throw;
                            }

                            //존 바닥 정보 가져오기
                            try
                            {
                                string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ZoneFloor.csv";
                                using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
                                {
                                    using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
                                    {
                                        int n2 = 0;
                                        while (!sr2.EndOfStream)
                                        {
                                            string[] token2 = sr2.ReadLine().Split(',');
                                            if (n2 == 0)
                                            {
                                            }
                                            else
                                            {
                                                Program.DB.setValue(DB.type.ProjDB, "ZoneFloor", "zoneNum,Name,Area,Ueff", "'" + zoneNum + "','" + token2[0] + "','" + token2[1] + "','" + token2[2] + "'", "zoneNum,Name");
                                                //             Floor floor = new Floor(Convert.ToDouble(token[1]), Convert.ToDouble(token[2]));
                                                //           zoneFloor.Add(floor);
                                            }
                                            n2++;

                                        }
                                    }
                                }


                            }
                            catch (IOException e)
                            {
                                if (e.Source != null)
                                    Console.WriteLine("IOException source: {0}", e.Source);
                                throw;
                            }

                            //존 지하벽 정보 가져오기
                            try
                            {
                                string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ZoneGWall.csv";
                                using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
                                {
                                    using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
                                    {
                                        int n2 = 0;
                                        while (!sr2.EndOfStream)
                                        {
                                            string[] token2 = sr2.ReadLine().Split(',');
                                            if (n2 == 0)
                                            {
                                            }
                                            else
                                            {
                                                Program.DB.setValue(DB.type.ProjDB, "ZoneGWall", "zoneNum,Name,Area,Ueff", "'" + zoneNum + "','" + token2[0] + "','" + token2[1] + "','" + token2[2] + "'", "zoneNum,Name");
                                                //          GWall gwall = new GWall(Convert.ToDouble(token[1]), Convert.ToDouble(token[2]));
                                                //        zoneGWall.Add(gwall);
                                            }
                                            n2++;

                                        }
                                    }
                                }


                            }
                            catch (IOException e)
                            {
                                if (e.Source != null)
                                    Console.WriteLine("IOException source: {0}", e.Source);
                                throw;
                            }

                            //존 문 정보 가져오기
                            try
                            {
                                string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ZoneDoor.csv";
                                using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
                                {
                                    using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
                                    {
                                        int n2 = 0;
                                        while (!sr2.EndOfStream)
                                        {
                                            string[] token2 = sr2.ReadLine().Split(',');
                                            if (n2 == 0)
                                            {
                                            }
                                            else
                                            {
                                                Program.DB.setValue(DB.type.ProjDB, "ZoneDoor", "zoneNum,Name,Area,Ueff,DirectInDirect,Direction,α,Degree",
                                                    "'" + zoneNum + "','" + token2[0] + "','" + token2[1] + "','" + token2[2] + "','" + token2[3] + "','" + token2[4] + "','" + token2[5] + "','" + token2[6] + "'", "zoneNum,Name");

                                                //           Door door = new Door(Convert.ToDouble(token[1]), Convert.ToDouble(token[2]), Convert.ToDouble(token[5]), token[3]);
                                                //         zoneDoor.Add(door);
                                            }
                                            n2++;

                                        }
                                    }
                                }


                            }
                            catch (IOException e)
                            {
                                if (e.Source != null)
                                    Console.WriteLine("IOException source: {0}", e.Source);
                                throw;
                            }

                            //존 창문 정보 가져오기
                            try
                            {
                                string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ZoneWin.csv";
                                using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
                                {
                                    using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
                                    {
                                        int n2 = 0;
                                        while (!sr2.EndOfStream)
                                        {
                                            string[] token2 = sr2.ReadLine().Split(',');
                                            if (n2 == 0)
                                            {
                                            }
                                            else
                                            {
                                                Program.DB.setValue(DB.type.ProjDB, "ZoneWin", "zoneNum,Name,Area,Uvalue,Uinst,DirectInDirect,Direction,Ff,g,τ,gtot,τtot,degree",
                                                    "'" + zoneNum + "','" + token2[0] + "','" + token2[1] + "','" + token2[2] + "','" + token2[3] + "','" + token2[4] + "','" + token2[5] + "','" + token2[6] + "','" + token2[7] + "','" + token2[8] + "','" + token2[9] + "','" + token2[10] + "','" + token2[11] + "'",
                                                    "zoneNum,Name");

                                                //                                                Window win = new Window(Convert.ToDouble(token[1]), Convert.ToDouble(token[2]), Convert.ToDouble(token[3]), token[4], Convert.ToDouble(token[6]), Convert.ToDouble(token[7]), Convert.ToDouble(token[8]), Convert.ToDouble(token[9]), Convert.ToDouble(token[10]));
                                                //                                              zoneWin.Add(win);
                                            }
                                            n2++;

                                        }
                                    }
                                }


                            }
                            catch (IOException e)
                            {
                                if (e.Source != null)
                                    Console.WriteLine("IOException source: {0}", e.Source);
                                throw;
                            }

                            //존 커튼월 정보 가져오기
                            try
                            {
                                string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ZoneCW.csv";
                                using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
                                {
                                    using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
                                    {
                                        int n2 = 0;
                                        while (!sr2.EndOfStream)
                                        {
                                            var s = sr2.ReadLine();
                                            if (s != null)
                                            {
                                                string[] token2 = s.Split(',');
                                                if (n2 == 0)
                                                {
                                                }
                                                else
                                                {
                                                    Program.DB.setValue(DB.type.ProjDB, "ZoneCW", "zoneNum,Name,Area_g,Uvalue_g,Ff_g,g_g,gtot_g,τ_g,τtot_g,Area_p,Uvalue_p,α_p,Area_d,Uvalue_d,Ff_d,g_d,τ_d,Area_tot,Uinst",
                                                        "'" + zoneNum + "','" + token2[0] + "','" + token2[1] + "','" + token2[2] + "','" + token2[3] + "','" + token2[4] + "','" + token2[5] + "','" + token2[6] + "','" + token2[7] + "','" + token2[8] + "','" + token2[9] + "','" + token2[10] + "','" + token2[11] + 
                                                        "','" + token2[12] + "','" + token2[13] + "','" + token2[14] + "','" + token2[15] + "','" + token2[16] + "','" + token2[17] + "'",
                                                        "zoneNum,Name");

                                                    //                                                CW cw = new CW(Convert.ToDouble(token[1]), Convert.ToDouble(token[2]), Convert.ToDouble(token[3]), Convert.ToDouble(token[4]), Convert.ToDouble(token[5]), Convert.ToDouble(token[6]), Convert.ToDouble(token[7]), Convert.ToDouble(token[8]), Convert.ToDouble(token[9]), Convert.ToDouble(token[10]), Convert.ToDouble(token[11]), Convert.ToDouble(token[12]), Convert.ToDouble(token[13]), Convert.ToDouble(token[14]), Convert.ToDouble(token[15]), Convert.ToDouble(token[16]), Convert.ToDouble(token[17]));
                                                    //                                              zoneCW.Add(cw);
                                                }
                                            }
                                            n2++;

                                        }
                                    }
                                }


                            }
                            catch (IOException e)
                            {
                                if (e.Source != null)
                                    Console.WriteLine("IOException source: {0}", e.Source);
                                throw;
                            }
                        }
                        n++;
                    }
                }
            }

            return true;
        }

        private static bool ZoneHT()
        {
            Zone zone1 = new Zone(zoneNum);
            zone1.ZoneHT();

            return true;
        }
        private static bool ZoneHV()
        {
            Zone zone1 = new Zone(zoneNum);
            zone1.ZoneHT();
            zone1.ZoneHV();
            return true;
        }
        private static bool Zonetao()
        {
            Zone zone1 = new Zone(zoneNum);
            zone1.ZoneHT();
            zone1.ZoneHV();
            zone1.Zonetao();
            return true;
        }
        private static bool Zonethetai()
        {
            Zone zone1 = new Zone(zoneNum);
            zone1.ZoneHT();
            zone1.ZoneHV();
            zone1.Zonetao();
            zone1.Zonethetai();
            return true;
        }
        private static bool ZoneQT()
        {
            Zone zone1 = new Zone(zoneNum);
            zone1.ZoneHT();
            zone1.ZoneHV();
            zone1.Zonetao();
            zone1.Zonethetai();
            zone1.ZoneQT();
            return true;
        }
        private static bool ZoneQV()
        {
            Zone zone1 = new Zone(zoneNum);
            zone1.ZoneHT();
            zone1.ZoneHV();
            zone1.Zonetao();
            zone1.Zonethetai();
            zone1.ZoneQT();
            zone1.ZoneQV();
            return true;
        }
        private static bool ZoneQSop()
        {
            Zone zone1 = new Zone(zoneNum);
            zone1.ZoneHT();
            zone1.ZoneHV();
            zone1.Zonetao();
            zone1.Zonethetai();
            zone1.ZoneQT();
            zone1.ZoneQV();
            zone1.ZoneQSop(zoneNum);
            return true;
        }
        private static bool ZoneQStr()
        {
            Zone zone1 = new Zone(zoneNum);
            zone1.ZoneHT();
            zone1.ZoneHV();
            zone1.Zonetao();
            zone1.Zonethetai();
            zone1.ZoneQT();
            zone1.ZoneQV();
            zone1.ZoneQSop(zoneNum);
            zone1.ZoneQStr(zoneNum);
            return true;
        }
        private static bool ZoneQI()
        {
            Zone zone1 = new Zone(zoneNum);
            zone1.ZoneHT();
            zone1.ZoneHV();
            zone1.Zonetao();
            zone1.Zonethetai();
            zone1.ZoneQT();
            zone1.ZoneQV();
            zone1.ZoneQSop(zoneNum);
            zone1.ZoneQStr(zoneNum);
            zone1.ZoneQI();
            return true;
        }
        private static bool Zoneeta()
        {
            Zone zone1 = new Zone(zoneNum);
            zone1.ZoneHT();
            zone1.ZoneHV();
            zone1.Zonetao();
            zone1.Zonethetai();
            zone1.ZoneQT();
            zone1.ZoneQV();
            zone1.ZoneQSop(zoneNum);
            zone1.ZoneQStr(zoneNum);
            zone1.ZoneQI();
            zone1.Zoneeta();
            return true;
        }
        private static bool ZoneQb()
        {
            Zone zone1 = new Zone(zoneNum);
            zone1.ZoneHT();
            zone1.ZoneHV();
            zone1.Zonetao();
            zone1.Zonethetai();
            zone1.ZoneQT();
            zone1.ZoneQV();
            zone1.ZoneQSop(zoneNum);
            zone1.ZoneQStr(zoneNum);
            zone1.ZoneQI();
            zone1.Zoneeta();
            zone1.ZoneQb();
            return true;
        }

        // calculation functions end
        /////////////////////////////////////////////////////////////////////////////////////

        private Dictionary<string, Delegate> _calculations = new Dictionary<string, Delegate>();

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
