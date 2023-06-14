using main;
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

        private static string zoneNum = "";

        public void init()
        {
            _calculations["셈플: CSV 를 메모리DB에 로딩..."] = new Func<bool>(LoadMemDB_example);

            _calculations["존 계산"] = new Func<bool>(ZoneCalc);
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

                                                Program.DB.setValue(DB.type.ProjDB, "ZoneWall", "zoneNum, Name, Area,Ueff,DirectInDirect,Direction,α,Degree",
                                                    "'" + zoneNum + "','" + token2[0] + "','" + token2[1] + "', '" + token2[2] + "','" + token2[3] + "','" + token2[4] + "','" + token2[5] + "','" + token2[6] + "'", "zoneNum, Name");                                               

                                                //         Wall wall = new Wall(Convert.ToDouble(token[1]), Convert.ToDouble(token[2]), Convert.ToDouble(token[5]), token[3]);
                                                //       zoneWall.Add(wall);
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

                            //외벽 일사 계산
                            try
                            {
                                string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ZoneWall_Solar.csv";
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
                                                int i = 0;
                                                while (++i < token2.Length)
                                                {
                                                    Program.DB.setValue(DB.type.ProjDB, "ZoneWall_Solar", "zoneNum,구조체,월,value", "'" + zoneNum + "','" + token2[0] + "','" + i + "','" + token2[i] + "'", "zoneNum,구조체,월");
                                                }

                                                //두번째 행부터 계산 	  
                                                //Wall zonewall = (Wall)zoneWall[n - 1];
                                                //for (int mth = 0; mth < 12; mth++)
                                                //{
                                                //    zoneWalls_Is[n - 1, mth] = Convert.ToDouble(token[mth + 1]);
                                                //    QSopCalc qsopcalc = new QSopCalc();
                                                //    if (zonewall.DiIndi() == "Indirection")
                                                //    {   //직접외기 벽만 일사 계산      
                                                //    }
                                                //    else
                                                //    {
                                                //        if (0.5 * 4.5 * 10 >= zonewall.α() * zoneWalls_Is[n - 1, mth])
                                                //        {
                                                //            zoneWalls_Qssink[n - 1, mth] = qsopcalc.Calc(zonewall.Ueff(), zonewall.Area(), zonewall.α(), zoneWalls_Is[n - 1, mth]);
                                                //            zoneWalls_Qssource[n - 1, mth] = 0;
                                                //        }
                                                //        else
                                                //        {
                                                //            zoneWalls_Qssink[n - 1, mth] = 0;
                                                //            zoneWalls_Qssource[n - 1, mth] = qsopcalc.Calc(zonewall.Ueff(), zonewall.Area(), zonewall.α(), zoneWalls_Is[n - 1, mth]);
                                                //        }
                                                //    }
                                                //    QSopsink_Wall[mth] += zoneWalls_Qssink[n - 1, mth];
                                                //    QSopsource_Wall[mth] += zoneWalls_Qssource[n - 1, mth];

                                                //}
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

                            //지붕 일사 계산
                            try
                            {
                                string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ZoneRoof_Solar.csv";
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
                                                int i = 0;
                                                while (++i < token2.Length)
                                                {
                                                    Program.DB.setValue(DB.type.ProjDB, "ZoneRoof_Solar", "zoneNum,구조체,월,value", "'" + zoneNum + "','" + token2[0] + "','" + i + "','" + token2[i] + "'", "zoneNum,구조체,월");
                                                }

                                                //Roof zoneroof = (Roof)zoneRoof[n - 1];
                                                //for (int mth = 0; mth < 12; mth++)
                                                //{
                                                //    zoneRoofs_Is[n - 1, mth] = Convert.ToDouble(token[mth + 1]);
                                                //    QSopCalc qsopcalc = new QSopCalc();
                                                //    if (zoneroof.DiIndi() == "Indirection")
                                                //    {   //직접외기 지붕만 일사 계산      
                                                //    }
                                                //    else
                                                //    {
                                                //        if (0.5 * 4.5 * 10 >= zoneroof.α() * zoneRoofs_Is[n - 1, mth])
                                                //        {
                                                //            zoneRoofs_Qssink[n - 1, mth] = qsopcalc.Calc(zoneroof.Ueff(), zoneroof.Area(), zoneroof.α(), zoneRoofs_Is[n - 1, mth]);
                                                //            zoneRoofs_Qssource[n - 1, mth] = 0;
                                                //        }
                                                //        else
                                                //        {
                                                //            zoneRoofs_Qssink[n - 1, mth] = 0;
                                                //            zoneRoofs_Qssource[n - 1, mth] = qsopcalc.Calc(zoneroof.Ueff(), zoneroof.Area(), zoneroof.α(), zoneRoofs_Is[n - 1, mth]);
                                                //        }
                                                //    }
                                                //    QSopsink_Roof[mth] += zoneRoofs_Qssink[n - 1, mth];
                                                //    QSopsource_Roof[mth] += zoneRoofs_Qssource[n - 1, mth];
                                                //}
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

                            //출입문 일사 계산
                            try
                            {
                                string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ZoneDoor_Solar.csv";
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
                                                int i = 0;
                                                while (++i < token2.Length)
                                                {
                                                    Program.DB.setValue(DB.type.ProjDB, "ZoneDoor_Solar", "zoneNum,구조체,월,value", "'" + zoneNum + "','" + token2[0] + "','" + i + "','" + token2[i] + "'", "zoneNum,구조체,월");
                                                }

                                                //Door zonedoor = (Door)zoneDoor[n - 1];
                                                //for (int mth = 0; mth < 12; mth++)
                                                //{
                                                //    zoneDoors_Is[n - 1, mth] = Convert.ToDouble(token[mth + 1]);
                                                //    QSopCalc qsopcalc = new QSopCalc();
                                                //    if (zonedoor.DiIndi() == "Indirection")
                                                //    {   //직접외기 벽만 일사 계산      
                                                //    }
                                                //    else
                                                //    {
                                                //        if (0.5 * 4.5 * 10 >= zonedoor.α() * zoneDoors_Is[n - 1, mth])
                                                //        {
                                                //            zoneDoors_Qssink[n - 1, mth] = qsopcalc.Calc(zonedoor.Ueff(), zonedoor.Area(), zonedoor.α(), zoneDoors_Is[n - 1, mth]);
                                                //            zoneDoors_Qssource[n - 1, mth] = 0;
                                                //        }
                                                //        else
                                                //        {
                                                //            zoneDoors_Qssink[n - 1, mth] = 0;
                                                //            zoneDoors_Qssource[n - 1, mth] = qsopcalc.Calc(zonedoor.Ueff(), zonedoor.Area(), zonedoor.α(), zoneDoors_Is[n - 1, mth]);
                                                //        }
                                                //    }
                                                //    QSopsink_Door[mth] += zoneDoors_Qssink[n - 1, mth];
                                                //    QSopsource_Door[mth] += zoneDoors_Qssource[n - 1, mth];
                                                //       }
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

                            //커튼월 패널 일사 계산
                            try
                            {
                                string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ZoneCW_Solar.csv";
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
                                                int i = 0;
                                                while (++i < token2.Length)
                                                {
                                                    Program.DB.setValue(DB.type.ProjDB, "ZoneCW_Solar", "zoneNum,구조체,월,value", "'" + zoneNum + "','" + token2[0] + "','" + i + "','" + token2[i] + "'", "zoneNum,구조체,월");
                                                }

                                                //CW zonecw = (CW)zoneCW[n - 1];
                                                //for (int mth = 0; mth < 12; mth++)
                                                //{
                                                //    zoneCWs_Is[n - 1, mth] = Convert.ToDouble(token[mth + 1]);
                                                //    QSopCalc qsopcalc = new QSopCalc();
                                                //    if (0.5 * 4.5 * 10 >= zonecw.α_p() * zoneCWs_Is[n - 1, mth])
                                                //    {
                                                //        zoneCWs_Qssink[n - 1, mth] = qsopcalc.Calc(zonecw.Uvalue_p(), zonecw.Area_p(), zonecw.α_p(), zoneCWs_Is[n - 1, mth]);
                                                //        zoneCWs_Qssource[n - 1, mth] = 0;
                                                //    }
                                                //    else
                                                //    {
                                                //        zoneCWs_Qssink[n - 1, mth] = 0;
                                                //        zoneCWs_Qssource[n - 1, mth] = qsopcalc.Calc(zonecw.Uvalue_p(), zonecw.Area_p(), zonecw.α_p(), zoneCWs_Is[n - 1, mth]);
                                                //    }
                                                //    QSopsink_CW_p[mth] += zoneCWs_Qssink[n - 1, mth];
                                                //    QSopsource_CW_p[mth] += zoneCWs_Qssource[n - 1, mth];
                                                //}
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

                            try
                            {
                                string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ZoneWin_Solar.csv";
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
                                                int i = 0;
                                                while (++i < token2.Length)
                                                {
                                                    Program.DB.setValue(DB.type.ProjDB, "ZoneWin_Solar", "zoneNum,구조체,월,value", "'" + zoneNum + "','" + token2[0] + "','" + i + "','" + token2[i] + "'", "zoneNum,구조체,월");
                                                }

                                                //Window zonewin = (Window)zoneWin[n - 1];
                                                //for (int mth = 0; mth < 12; mth++)
                                                //{
                                                //    zoneWins_Is[n - 1, mth] = Convert.ToDouble(token[mth + 1]);
                                                //}
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

                            //존의 창별 음영정보 가져오기
                            try
                            {
                                string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ZoneWin_Shadow.csv";
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
                                                int i = 0;
                                                while (++i < token2.Length)
                                                {
                                                    Program.DB.setValue(DB.type.ProjDB, "ZoneWin_Shadow", "zoneNum,구조체,월,value", "'" + zoneNum + "','" + token2[0] + "','" + i + "','" + token2[i] + "'", "zoneNum,구조체,월");
                                                }

                                                //Window zonewin = (Window)zoneWin[n - 1];
                                                //for (int mth = 0; mth < 12; mth++)
                                                //{
                                                //    zoneWins_Fs[n - 1, mth] = Convert.ToDouble(token[mth + 1]);
                                                //}
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

                            //존의 창별 가동계수정보 가져오기
                            try
                            {
                                string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ZoneWin_a.csv";
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
                                                int i = 0;
                                                while (++i < token2.Length)
                                                {
                                                    Program.DB.setValue(DB.type.ProjDB, "ZoneWin_a", "zoneNum,구조체,월,value", "'" + zoneNum + "','" + token2[0] + "','" + i + "','" + token2[i] + "'", "zoneNum,구조체,월");
                                                }

                                                //Window zonewin = (Window)zoneWin[n - 1];
                                                //for (int mth = 0; mth < 12; mth++)
                                                //{
                                                //    zoneWins_a[n - 1, mth] = Convert.ToDouble(token[mth + 1]);
                                                //}
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

                            //존의 커튼월별 음영정보 가져오기
                            try
                            {
                                string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ZoneCW_shadow.csv";
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
                                                int i = 0;
                                                while (++i < token2.Length)
                                                {
                                                    Program.DB.setValue(DB.type.ProjDB, "ZoneCW_shadow", "zoneNum,구조체,월,value", "'" + zoneNum + "','" + token2[0] + "','" + i + "','" + token2[i] + "'", "zoneNum,구조체,월");
                                                }

                                                //CW zonecw = (CW)zoneCW[n - 1];
                                                //for (int mth = 0; mth < 12; mth++)
                                                //{
                                                //    zoneCWs_Fs[n - 1, mth] = Convert.ToDouble(token[mth + 1]);
                                                //}
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

                            //존의 커튼월별 가동계수정보 가져오기
                            try
                            {
                                string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ZoneCW_a.csv";
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
                                                int i = 0;
                                                while (++i < token2.Length)
                                                {
                                                    Program.DB.setValue(DB.type.ProjDB, "ZoneCW_a", "zoneNum,구조체,월,value", "'" + zoneNum + "','" + token2[0] + "','" + i + "','" + token2[i] + "'", "zoneNum,구조체,월");
                                                }

                                                //CW zonecw = (CW)zoneCW[n - 1];
                                                //for (int mth = 0; mth < 12; mth++)
                                                //{
                                                //    zoneCWs_a[n - 1, mth] = Convert.ToDouble(token[mth + 1]);
                                                //}
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


                            //******************************************************************************************************************************************************************//

                            //조명 존일반정보가져오기
                            try
                            {
                                string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ZoneLightgeneral.csv";
                                using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
                                {
                                    using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
                                    {
                                        int n2 = 0;
                                        while (!sr2.EndOfStream)
                                        {
                                            //Facade _facade = new Facade();
                                            string[] token2 = sr2.ReadLine().Split(',');
                                            if (n2 == 0)
                                            {
                                            }
                                            else
                                            {
                                                int i = 0;
                                                string s = "";
                                                while (++i < 8)
                                                {
                                                    s += "'" + token2[i] + "',";
                                                }
                                                Program.DB.setValue(DB.type.ProjDB, "ZoneLightgeneral", "ZoneNum,Wr,Lr,A,hR,hm,hLi,hTa,K", "'" + zoneNum + "'," + s +"'" + token2[8] + "'", "zoneNum"); 
                                                //Wr = Convert.ToDouble(token2[1]);
                                                //Lr = Convert.ToDouble(token2[2]);
                                                //A = Convert.ToDouble(token2[3]);
                                                //hR = Convert.ToDouble(token2[4]);
                                                //hm = Convert.ToDouble(token2[5]);
                                                //Zone_hLi = Convert.ToDouble(token2[6]);
                                                //Zone_hTa = Convert.ToDouble(token2[7]);
                                                //K = Convert.ToDouble(token2[8]);
                                            }
                                            n2++;

                                        }

                                        sr2.Close();

                                    }
                                }

                            }

                            catch (IOException e)
                            {
                                if (e.Source != null)
                                    Console.WriteLine("IOException source: {0}", e.Source);
                                throw;
                            }

                            //조명 용도프로필 가져오기
                            try
                            {
                                string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\ZoneLightprofile.csv";
                                using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
                                {
                                    using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
                                    {
                                        int n2 = 0;
                                        while (sr2.EndOfStream == false)
                                        {
                                            string[] token2 = sr2.ReadLine().Split(',');
                                            if (n2 == 0)
                                            {
                                            }
                                            else
                                            {

                                                int i = -1;
                                                string s = "";
                                                while (++i < 3)
                                                {
                                                    s += "'" + token2[i] + "',";
                                                }
                                                Program.DB.setValue(DB.type.ProjDB, "ZoneLightprofile", "ZoneNum,Location,Em,KA,FA", "'" + zoneNum + "'," + s + "'" + token2[3] + "'", "zoneNum");
                                                //Location = token2[0];
                                                //Em = Convert.ToDouble(token2[1]);
                                                //KA = Convert.ToDouble(token2[2]);
                                                //FA = Convert.ToDouble(token2[3]);


                                            }
                                            n2++;

                                        }
                                        sr2.Close();

                                    }
                                }

                            }

                            catch (IOException e)
                            {
                                if (e.Source != null)
                                    Console.WriteLine("IOException source: {0}", e.Source);
                                throw;
                            }

                            //조명 낮시간 가져오기
                            try 
                            {
                                string filePath2 = Program.gPath + "calculations\\" + zoneNum + "\\Zonedaytime.csv";
                                using (FileStream fileReader2 = new FileStream(filePath2, FileMode.Open))
                                {
                                    using (StreamReader sr2 = new StreamReader(fileReader2, Encoding.UTF8, false))
                                    {
                                        int n2 = 0;
                                        while (sr2.EndOfStream == false)
                                        {
                                            string[] token2 = sr2.ReadLine().Split(',');
                                            if (n2 == 0)
                                            {
                                            }
                                            else
                                            {

                                                int i = -1;
                                                while (++i < 12)
                                                {
                                                    Program.DB.setValue(DB.type.ProjDB, "Zonedaytime", "zoneNum,월,value", "'" + zoneNum + "','" +(i+1).ToString() + "','" + token2[i+1] + "'", "zoneNum,월");
                                                }

                                            }
                                            n2++;

                                        }
                                        sr2.Close();


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
        private static bool ZoneCalc()
        {
            int i = -1;
            string[][] zones = Program.DB.getValue(DB.type.ProjDB, "Zone", "zoneNum");

            while(++i < zones.Length)
            {
                Zone zone1 = new Zone(zones[i][0]);
                zone1.ZoneHT();
                zone1.ZoneHV();
                zone1.Zonetao();
                zone1.Zonethetai();
                zone1.ZoneQT();
                zone1.ZoneQV();
                zone1.ZoneQSop(zones[i][0]);
                zone1.ZoneQStr(zones[i][0]);
                zone1.ZoneQI();
                zone1.Zoneeta();
                zone1.ZoneQb();
                MessageBox.Show(zoneNum + ":  " + zone1.zoneName + ":  " + zone1.Qhb_a);
                /*
                ZoneLight zonelight = new ZoneLight(zones[i][0]);
                zonelight.Calc_Facade_general();
                zonelight.Calc_Facade_shade();
                zonelight.Calc_Facade_FDS();
                zonelight.Calc_Facade_FD();

                zonelight.Calc_Roof_general();

                zonelight.Calc_Roof_ηR();
                zonelight.Calc_Roof_FDS();
                zonelight.Calc_Roof_FD();

                zonelight.Calc_Sunlight_SCW();
                zonelight.Calc_Sunlight_Pj_SC();

                zonelight.Calc_W();*/

            }

          
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
