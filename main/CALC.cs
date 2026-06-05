using System;
using System.Collections;

namespace main
{
    internal class CALC
    {
        public static ArrayList zone = new ArrayList();
        public static ArrayList zonelight = new ArrayList();

        public CALC()
        {
            _calculations["모두계산"] = new Func<bool>(Run_All);
            _calculations["존계산"] = new Func<bool>(Run_Zone);
            _calculations["공조계산"] = new Func<bool>(Run_AHU);
            _calculations["요소기술계산"] = new Func<bool>(AltCalc);

        }

        public static bool Run_Zone()
        {
            Program.DB.deleteTable(DB.type.ProjDB, "Zone_LightResult");
            Program.DB.initTable(DB.type.ProjDB, "Zone_LightResult");

            Program.DB.deleteTable(DB.type.ProjDB, "Zone_HCneed_Result");
            Program.DB.initTable(DB.type.ProjDB, "Zone_HCneed_Result");

            Program.DB.deleteTable(DB.type.ProjDB, "Zone_Envelope_Result");
            Program.DB.initTable(DB.type.ProjDB, "Zone_Envelope_Result");
            Cal_Qb();

            Program.DB.saveProject();

            return true;
        }
        public static bool Run_AHU()
        {
            Program.DB.deleteTable(DB.type.ProjDB, "Zone_LightResult");
            Program.DB.initTable(DB.type.ProjDB, "Zone_LightResult");

            Program.DB.deleteTable(DB.type.ProjDB, "Zone_HCneed_Result");
            Program.DB.initTable(DB.type.ProjDB, "Zone_HCneed_Result");

            Program.DB.deleteTable(DB.type.ProjDB, "Zone_Envelope_Result");
            Program.DB.initTable(DB.type.ProjDB, "Zone_Envelope_Result");

            Program.DB.deleteTable(DB.type.ProjDB, "AHUSystem_Result");
            Program.DB.initTable(DB.type.ProjDB, "AHUSystem_Result");


            string[][] NowProjNum = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");


            Cal_Qb();
            Cal_Qahu(NowProjNum[0][0]);
            Program.DB.saveProject();


            return true;
        }
        public static bool Run_All()
        {
            Program.DB.deleteTable(DB.type.ProjDB, "Zone_LightResult");
            Program.DB.initTable(DB.type.ProjDB, "Zone_LightResult");

            Program.DB.deleteTable(DB.type.ProjDB, "Zone_HCneed_Result");
            Program.DB.initTable(DB.type.ProjDB, "Zone_HCneed_Result");

            Program.DB.deleteTable(DB.type.ProjDB, "Zone_Envelope_Result");
            Program.DB.initTable(DB.type.ProjDB, "Zone_Envelope_Result");

            Program.DB.deleteTable(DB.type.ProjDB, "AHUSystem_Result");
            Program.DB.initTable(DB.type.ProjDB, "AHUSystem_Result");

            Program.DB.deleteTable(DB.type.ProjDB, "DHWSystem_Result");
            Program.DB.initTable(DB.type.ProjDB, "DHWSystem_Result");

            Program.DB.deleteTable(DB.type.ProjDB, "HeatingSystem_Result");
            Program.DB.initTable(DB.type.ProjDB, "HeatingSystem_Result");

           Program.DB.deleteTable(DB.type.ProjDB, "CoolingSystem_Result");
           Program.DB.initTable(DB.type.ProjDB, "CoolingSystem_Result");

            Program.DB.deleteTable(DB.type.ProjDB, "WindPower_Result");
            Program.DB.initTable(DB.type.ProjDB, "WindPower_Result");

            Program.DB.deleteTable(DB.type.ProjDB, "FinalEnergy_Result");
           Program.DB.initTable(DB.type.ProjDB, "FinalEnergy_Result");

            Program.DB.deleteTable(DB.type.ProjDB, "RESystem_Result");
            Program.DB.initTable(DB.type.ProjDB, "RESystem_Result");

            string[][] NowProjNum = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");

            RESystems.Clear();
            Cal_Qb();
            Cal_Qahu(NowProjNum[0][0]);
            Cal_Qfw(NowProjNum[0][0]);
            Cal_Qfh(NowProjNum[0][0]);
            Cal_Qfc(NowProjNum[0][0]);
            Final final1 = new Final(NowProjNum[0][0]);
            Cal_Qf(final1, NowProjNum[0][0]);
            RESystemCalc(NowProjNum[0][0]);
            Cal_Qf(null, NowProjNum[0][0]);

            Program.DB.saveProject();
            return true;
        }
        public static void Cal_Qb()
        {
            Save_q50(Cal_q50(Load_n50()));
            Save_dUtb_2D(Cal_dUtb_2D());

            Zone_Arrange();
            for (int k = 0; k < zone.Count; k++)
            {
                Zone zone1 = (Zone)zone[k];
                ZoneLight zonelight1 = (ZoneLight)zonelight[k];
                Zone_LoadData(zone1, zonelight1);
                Zone_Calc(zone1, zonelight1);
                Zone_Save(zone1, zonelight1);
            }
        }
        private static void Cal_Qahu(string ProjNum)
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
                        AHUs[Num[i][0]] = Pre_AHU1;
                        AHUSystem_LaodData(Pre_AHU1, ProjNum);
                        AHUSystem_PreCalc(Pre_AHU1);
                    }
                    else
                    {
                        AHU Pre_HRV1 = new AHU(Num[i][0]);
                        AHUs[Num[i][0]] = Pre_HRV1;
                        HRV_LaodData(Pre_HRV1, ProjNum);
                        AHUSystem_PreCalc(Pre_HRV1);
                    }
                }
                Cal_Qb();
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
                        AHUs[Num[i][0]] = Post_AHU1;
                        AHUSystem_LaodData(Post_AHU1, ProjNum);
                        AHUSystem_PostCalc(Post_AHU1);
                        AHUSystem_PostSave(Post_AHU1);
                    }
                    else
                    {
                        AHU Post_HRV1 = new AHU(Num[i][0]);
                        AHUs[Num[i][0]] = Post_HRV1;
                        HRV_LaodData(Post_HRV1, ProjNum);
                        HRV_PostCalc(Post_HRV1);
                        HRV_PostSave(Post_HRV1);
                    }

                }
            }

        }
        public static void Cal_Qfh(string ProjNum)
        {
            Heating_ce_zone_calc(ProjNum);
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
                    Heatings[HeatingNum[i][0]] = Heating1;
                    Heating_LoadData(Heating1, ProjNum);
                    Heating_Calc(Heating1, ProjNum);
                    Heating_Save(Heating1);
                }
            }
        }
        public static void Cal_Qfc(string ProjNum)
        {
            Cooling_ce_zone_calc(ProjNum);
            string[][] CoolingNum = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "번호");

            for (int i = 0; i < CoolingNum.Length; i++)
            {
                Cal_Cooling cc1 = new Cal_Cooling(CoolingNum[i][0]);
                Coolings[CoolingNum[i][0]] = cc1;
                Cooling_LoadData(cc1, ProjNum);
                Cooling_Calc(cc1, ProjNum);
                Cooling_Save(cc1);
            }
        }
        public static void Cal_Qfw(string ProjNum)
        {
            string[][] DHWNum = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "번호");
            if(DHWNum.Length > 0)
            {
                for (int k = 0; k < DHWNum.Length; k++)
                {
                    CALC.DHWs[DHWNum[k][0]] = null;
                }
                int i = -1;
                while(++i < DHWNum.Length)
                {
                    DHW DHW1 = new DHW(DHWNum[i][0]);
                    DHWs[DHWNum[i][0]] = DHW1;
                    DHW_LoadData(DHW1, ProjNum);
                    DHW_Calc(DHW1, ProjNum);
                    DHW_Save(DHW1);
                }
            }
        }
        private static void Cal_Qf(Final final1, string ProjNum)
        {
            if(final1 == null)
            {
                final1 = new Final(ProjNum);
                final1.Load_Heating_Final(ProjNum);
                final1.Load_Cooling_Final(ProjNum);
                final1.Load_DHW_Final(ProjNum);
                final1.Load_AHU_Final(ProjNum);
                Final_Calc(final1, ProjNum, true); // true는 두번째 계산, 신재생 분배 포함
                Save_RESystem(ProjNum);
                Final_Save(final1);
            }
            else
            {
                {
                    final1.Load_Heating_Final(ProjNum);
                    final1.Load_Cooling_Final(ProjNum);
                    final1.Load_DHW_Final(ProjNum);
                    final1.Load_AHU_Final(ProjNum);
                    Final_Calc(final1, ProjNum, false);// false는 첫번째 계산, 신재생 분배 미포함
                }
            }
        }
        #region 기밀

        public static double Load_n50()
        {
            double n50 = 0;
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BUildingGeneral", "n50");
            if (Value.Length > 0 && Value[0][0] != "")
            {
                n50 = Convert.ToDouble(Value[0][0]);
            }
            return n50;
        }
        public static double[] Cal_q50(double n50)
        {
            double Area_tot = 0; //직접외기 외피면적
            double Area_q50 = 0; // 면적 * q50 합산 
            double[] q50_element = new double[4];
            double CMH_tot = 0;
            double Volume_tot = 0;

            string[][] ZoneV = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적,천장고", "");
            if(ZoneV.Length > 0)
            {
                for(int a=0; a<ZoneV.Length; a++)
                {
                    Volume_tot += Convert.ToDouble(ZoneV[a][0]) * Convert.ToDouble(ZoneV[a][1]);
                }
                CMH_tot = n50 * Volume_tot;
            }


            //출입문5,창호3,외벽2,지붕2 
            q50_element[0] = 5;
            q50_element[1] = 3;
            q50_element[2] = 2;
            q50_element[3] = 2;

            string[][] ZoneE = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,외피유형,면적,구조체번호,커튼월부위", "");
            if (ZoneE.Length > 0)
            {
                string[][] ss;
                for (int n = 0; n < ZoneE.Length; n++)
                {
                    if (ZoneE[n][1] == "외부출입문")
                    {
                        ss = Program.DB.getValue(DB.type.ProjDB, "ConstructionDoor", "직접간접", "번호='" + ZoneE[n][3] + "'");
                        if (ss.Length > 0)
                        {
                            if (ss[0][0] == "직접외기")
                            {
                                Area_tot += Convert.ToDouble(ZoneE[n][2]);
                                Area_q50 += Convert.ToDouble(ZoneE[n][2]) * q50_element[0];
                            }
                        }
                    }                   
                    else if (ZoneE[n][1] == "창호")
                    {
                        ss = Program.DB.querySQL(DB.type.ProjDB, "select a.직접간접 FROM ConstructionWindow AS a INNER JOIN SubWindow AS b ON a.번호 = b.상위창호번호 where b.번호 = '" + ZoneE[n][3] + "'");
                        if (ss.Length > 0)
                        {
                            if (ss[0][0] == "직접외기")
                            {
                                Area_tot += Convert.ToDouble(ZoneE[n][2]);
                                Area_q50 += Convert.ToDouble(ZoneE[n][2]) * q50_element[1];
                            }
                        }
                    }
                    else if (ZoneE[n][1] == "커튼월창")
                    {
                        if (ZoneE[n][4] != "출입문부분")
                        {
                            Area_tot += Convert.ToDouble(ZoneE[n][2]);
                            Area_q50 += Convert.ToDouble(ZoneE[n][2]) * q50_element[1];
                        }
                        else
                        {
                            Area_tot += Convert.ToDouble(ZoneE[n][2]);
                            Area_q50 += Convert.ToDouble(ZoneE[n][2]) * q50_element[0];
                        }
                    }
                    else if (ZoneE[n][1] == "외벽")
                    {
                        ss = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "직접간접", "번호='" + ZoneE[n][3] + "'");
                        if (ss.Length > 0)
                        {
                            if (ss[0][0] == "직접외기")
                            {
                                Area_tot += Convert.ToDouble(ZoneE[n][2]);
                                Area_q50 += Convert.ToDouble(ZoneE[n][2]) * q50_element[2];
                            }
                        }
                    }
                    else if (ZoneE[n][1] == "지붕")
                    {
                        ss = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "직접간접", "번호='" + ZoneE[n][3] + "'");
                        if (ss.Length > 0)
                        {
                            if (ss[0][0] == "직접외기")
                            {
                                Area_tot += Convert.ToDouble(ZoneE[n][2]);
                                Area_q50 += Convert.ToDouble(ZoneE[n][2]) * q50_element[3];
                            }
                        }
                    }
                }
            }

            q50_element[0] = q50_element[0] * CMH_tot / Area_q50;
            q50_element[1] = q50_element[1] * CMH_tot / Area_q50;
            q50_element[2] = q50_element[2] * CMH_tot / Area_q50;
            q50_element[3] = q50_element[3] * CMH_tot / Area_q50;

            return q50_element;
        }

        private static void Save_q50(double[] q50_element)
        {
            string[][] 번호 = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
            Program.DB.setValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호,출입문q50,창호q50,외벽q50,지붕q50",
                 "'" + 번호[0][0] + "','" + q50_element[0] + "','" + q50_element[1] + "','" + q50_element[2] + "','" + q50_element[3] + "'", "프로젝트번호");
        }

        public static double[] Cal_dUtb_2D()
        {
            double[] dUtb = new double[3]; //외벽, 지붕, 바닥 
            dUtb[0] = 0.15;
            dUtb[1] = 0.15;
            dUtb[2] = 0.15;
            
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            if(프로젝트유형.Length >0)
            {
                if (프로젝트유형[0][0] =="1")
                {
                    dUtb[0] = 0.15;
                    dUtb[1] = 0.15;
                    dUtb[2] = 0.15;
                }
                else
                {
                    string[][] Check = Program.DB.getValue(DB.type.ProjDB, "ThermalBridge_3D", "선택열교, 열교길이");
                    double check_ =0; 
                    if(Check.Length > 0)
                    {
                        for(int a=0; a<Check.Length; a++)
                        {
                            if (Check[a][0] == null || Check[a][0] =="")
                            {
                                check_ += Convert.ToDouble(Check[a][1]);
                            }
                        }
                        if(check_ >0)
                        {
                            string[][] check2 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "외벽dUtb,지붕dUtb,바닥dUtb");
                            if(check2.Length >0)
                            {

                            }
                            else
                            {
                                MessageBox.Show("접합부 열교를 입력하지 않아 내단열 기준 표준값 0.15로 검토됩니다.");
                                dUtb[0] = 0.15;
                                dUtb[1] = 0.15;
                                dUtb[2] = 0.15;
                            }
                            
                        }
                        else
                        {
                            dUtb[2] = 0;
                            //dUtb[2] = 0.15;
                            double Qwall = 0, Qroof = 0;

                            //외벽
                            string[][] WTB_Length = Program.DB.getValue(DB.type.ProjDB, "ThermalBridge_3D", "번호,열교길이,선택열교", "substr(번호,1,3)='WTB'");
                            if (WTB_Length.Length > 0)
                            {
                                for (int a = 0; a < WTB_Length.Length; a++)
                                {
                                    string[][] Psi = Program.DB.getValue(DB.type.BaseDB_HCneed, "접합부열교", "값", "번호 ='" + WTB_Length[a][2] + "'");
                                    if (Psi.Length == 0) { Psi = Program.DB.getValue(DB.type.ProjDB, "User_TB", "값", "번호 ='" + WTB_Length[a][2] + "'"); }
                                    if (Psi.Length > 0)
                                    {
                                        Qwall += Convert.ToDouble(WTB_Length[a][1]) * Convert.ToDouble(Psi[0][0]);
                                    }
                                }
                            }
                            string[][] ZoneE = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "Sum(면적)", "외피유형='외벽'");
                            if (ZoneE.Length > 0 && ZoneE[0][0] != "")
                            {
                                dUtb[0] = Qwall / Convert.ToDouble(ZoneE[0][0]);
                            }

                            //지붕
                            string[][] RTB_Length = Program.DB.getValue(DB.type.ProjDB, "ThermalBridge_3D", "번호,열교길이,선택열교", "substr(번호,1,3)='RTB'");
                            if (RTB_Length.Length > 0)
                            {
                                for (int a = 0; a < RTB_Length.Length; a++)
                                {
                                    string[][] Psi = Program.DB.getValue(DB.type.BaseDB_HCneed, "접합부열교", "값", "번호 ='" + RTB_Length[a][2] + "'");
                                    if (Psi.Length == 0) { Psi = Program.DB.getValue(DB.type.ProjDB, "User_TB", "값", "번호 ='" + RTB_Length[a][2] + "'"); }
                                    if (Psi.Length > 0)
                                    {
                                        Qroof += Convert.ToDouble(RTB_Length[a][1]) * Convert.ToDouble(Psi[0][0]);
                                    }
                                }
                            }
                            ZoneE = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "Sum(면적)", "외피유형='지붕'");
                            if (ZoneE.Length > 0 && ZoneE[0][0] != "")
                            {
                                dUtb[1] = Qroof / Convert.ToDouble(ZoneE[0][0]);
                            }
                        }



                    }               
                }

            }
           
            return dUtb;
        }

        private static void Save_dUtb_2D(double[] dUtb)
        {
            string[][] 번호 = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
            Program.DB.setValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호,외벽dUtb,지붕dUtb,바닥dUtb",
                 "'" + 번호[0][0] + "','" + dUtb[0] + "','" + dUtb[1] + "','" + dUtb[2] + "'", "프로젝트번호");
        }

        #endregion

        #region 요구량
        public static void Zone_Arrange()
        {
            zone.Clear(); zonelight.Clear();
            string[][] zones = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,냉난방유무");
            String[,] zones_arr = new String[zones.Length, 2];//존번호, 냉난방유무
            String[] zones_순번 = new String[zones.Length];// 계산 순서대로 존번호
            int N_비냉난방 = 0, N_난방 = 0, N_냉방 = 0, N_냉난방 = 0; //순번 카운팅 
            int T_비냉난방 = 0, T_난방 = 0, T_냉방 = 0, T_냉난방 = 0; //총계 카운팅 

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
            for (int k = 0; k < zones_순번.Length; k++)
            {
                ZoneLights[zones_순번[k]] = null;
                Zones[zones_순번[k]] = null;
            }
            while (++i < zones.Length)
            {
                ZoneLight zonelight1 = new ZoneLight(zones_순번[i]);
                zonelight.Add(zonelight1); //
                ZoneLights[zones_순번[i]] = zonelight1;
                Zone zone1 = new Zone(zones_순번[i]);
                zone.Add(zone1);
                Zones[zones_순번[i]] = zone1;
            }

        }
        public static void Zone_LoadData(Zone zone1, ZoneLight zonelight1)
        {
            zonelight1.LoadData_LightGeneral();
            zonelight1.LoadData_LightSystem();
            zonelight1.LoadData_NaturalLight();
            zonelight1.LoadData_Renew();
            zone1.LoadData_ZoneGeneral();
            //zone1.LoadData_Shade(zone1.ZoneNum);
            zone1.LoadData_q50();
            zone1.LoadData_dUtb_2D();
            zone1.LoadData_Ventil();
            zone1.LoadData_InWall();
            zone1.LoadData_SL();
            zone1.LoadData_Wall();
            zone1.LoadData_Roof();
            zone1.LoadData_Floor();
            zone1.LoadData_GWall();
            zone1.LoadData_Door();
            zone1.LoadData_Win();
            zone1.LoadData_CW();
        }
        public static void Zone_Calc(Zone zone1, ZoneLight zonelight1)
        {
            zonelight1.Calc_time();
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
            zone1.ZoneQT();
            zone1.ZoneQV();
            zone1.ZoneQSop();
            zone1.ZoneQStr_Win();
            zone1.ZoneQStr_CW();
            zone1.ZoneQ_DHU();
            zone1.ZoneQI_L();
            zone1.ZoneQI();
            zone1.Zonetao();
            zone1.ZoneGamma1();
            zone1.Zonethetai();
            zone1.ZoneQT2();
            zone1.ZoneQV2();
            zone1.Zoneeta();
            zone1.ZoneQb();
            zone1.ZoneQmax();
        }
        private static void Zone_Save(Zone zone1, ZoneLight zonelight1)
        {
            String HC, WEWD, MTH;
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            for (int mth = 0; mth <= 11; mth++)
            {
                MTH = (mth + 1).ToString() + "월";

                Program.DB.setValue(DB.type.ProjDB, "Zone_LightResult", "프로젝트번호,프로젝트유형,번호,월," +
                    "ITr,IRD,ISh_Ish,Ish_In_At,Wi,Ish_GDF,Ish," +
                    "f_τeff_SNA,f_D,f_nearD,f_DCA,f_dclass,f_nearEm_SNA,f_fd_sna,f_fd_sa,f_nearEm_DC,f_fd_c,f_FDS,f_FD," +
                    "as_bs,hs_bs,hg_hw," +
                    "normal_ηR,saw_ηR,r_DSNA,r_DSA,r_dclass," +
                    "r_nearEm_FDS,r_fd_sna,r_fd_sa,r_nearEm_DC,r_fd_c,r_FDS,r_FD," +
                    "Sunlight_SCW,Sunlight_PjSC,Final_kWh,Aux_kWh,Prod_kWh,OutdoorLux",

                "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + zonelight1.ZoneNum + "','" + MTH + "','" +
                 zonelight1.Zone_ITr.ToString() + "','" + zonelight1.Zone_IRD.ToString() + "','" + zonelight1.Zone_Ish[mth].ToString() + "','" + zonelight1.Zone_Ish_In_At.ToString() + "','" + zonelight1.Zone_Wi.ToString() + "','" + zonelight1.Zone_Ish_GDF.ToString() + "','" + zonelight1.Zone_Calc_Ish[mth].ToString() + "','" +
                 zonelight1.Zone_τeff_SNA_j.ToString() + "','" + zonelight1.Zone_D[mth].ToString() + "','" + zonelight1.Zone_nearD[mth].ToString() + "','" + zonelight1.Zone_DCA[mth].ToString() + "','" + zonelight1.dclass[mth] + "','" + zonelight1.f_nearEm_SNA.ToString() + "','" + zonelight1.find_fd_sna[mth].ToString() + "','" + zonelight1.find_fd_sa[mth].ToString() + "','" + zonelight1.f_naerEm_DC.ToString() + "','" + zonelight1.find_fd_c[mth].ToString() + "','" + zonelight1.Zone_FDS[mth].ToString() + "','" + zonelight1.Zone_Facade_FD[mth].ToString() + "','" +
                 zonelight1.Zone_as_bs.ToString() + "','" + zonelight1.Zone_hs_bs.ToString() + "','" + zonelight1.Zone_hg_hw.ToString() + "','" +
                 zonelight1.find_normal_ηR.ToString() + "','" + zonelight1.find_saw_ηR.ToString() + "','" + zonelight1.Zone_Roof_DSNA.ToString() + "','" + zonelight1.Zone_Roof_DSA.ToString() + "','" + zonelight1.roof_dclass + "','" +
                 zonelight1.r_nearEm_FDS.ToString() + "','" + zonelight1.find_roof_fd_sna.ToString() + "','" + zonelight1.find_roof_fd_sa.ToString() + "','" + zonelight1.r_nearEm_DC.ToString() + "','" + zonelight1.find_roof_fd_c.ToString() + "','" + zonelight1.Zone_Roof_FDS[mth].ToString() + "','" + zonelight1.Zone_Roof_FD[mth].ToString() + "','" +
                 zonelight1.Zone_Sunlight_SCW[mth].ToString() + "','" + zonelight1.Zone_Sunlight_PjSC[mth].ToString() + "','" + zonelight1.Zone_Final_kWh[mth].ToString()
                 + "','" + zonelight1.Zone_AuxLight[mth].ToString() + "','" + zonelight1.Zone_Prodlight[mth].ToString() + "','" + zonelight1.Zone_Lux[mth].ToString() + "'", "번호,월");
            }

            //[난방/냉방,비이용일/이용일,mth] = [h/c,we/wd,mth]=[0/1,0/1,12]
            for (int hc = 0; hc <= 1; hc++)
            {
                if (hc == 0)
                {
                    HC = "난방";
                }
                else
                {
                    HC = "냉방";
                }

                for (int mth = 0; mth <= 11; mth++)
                {
                    MTH = (mth + 1).ToString() + "월";

                    Program.DB.setValue(DB.type.ProjDB, "Zone_HCneed_Result", "프로젝트번호,프로젝트유형,번호,이름," +
                         "난방_냉방,비이용일_이용일,월," +
                         "HT_tot,HT_InWall,HT_Slab,HT_Wall,HT_Roof,HT_Floor,HT_GWall,HT_Door,HT_Win,HT_CW," +
                         "HT_Di_Wall,HT_Indi_Wall,HT_Di_Roof,HT_Indi_Roof,HT_Di_Win,HT_Indi_Win,HT_Di_Door,HT_Indi_Door," +
                         "HT_TB_tot,HT_TB_Wall,HT_TB_Roof,HT_TB_Floor,HT_TB_Gwall,HT_TB_Win,HT_TB_Door,HT_TB_CW," +
                         "nmech,nz,ninf,nwin," +
                         "HV_tot,HV_inf,HV_win,HV_z,HV_mech," +
                         "H_tot,tao,dwd_mth,theta_i,theta_e," +
                         "QTsink_tot,QT_u_sink,QTsink_Wall,QTsink_Roof,QTsink_Floor,QTsink_GWall,QTsink_Door,QTsink_Win,QTsink_CW," +
                         "QTsource_tot,QT_u_source,QTsource_Wall,QTsource_Roof,QTsource_Floor,QTsource_GWall,QTsource_Door,QTsource_Win,QTsource_CW," +
                         "QSopsink_tot,QSopsource_tot,QStr_tot," +
                         "QSopsink_Wall,QSopsink_Roof,QSopsink_Door,QSopsink_CW_p," +
                         "QSopsource_Wall,QSopsource_Roof,QSopsource_Door,QSopsource_CW_p," +
                         "QStr_Win,QStr_CW," +
                         "QVsink_tot,QV_inf_sink,QV_win_sink,QV_z_sink,QV_mech_sink," +
                         "QVsource_tot,QV_inf_source,QV_win_source,QV_z_source,QV_mech_source," +
                         "Q_DHU_win,Q_DHU_mech,Q_DHU_tot," +
                         "QI_tot,QI_L," +
                         "QI_P,QI_fac,QI_Humidity," +
                         "Qsink,Qsource,gamma,a,eta," +
                         "Qb_mth," +
                         "Qb_a,Q_max, t_max,배기팬에너지_kWh",
                          "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + zone1.ZoneNum + "','" + zone1.zoneName + "','" +
                          HC + "','" + "이용일" + "','" + MTH + "','" +
                          zone1.Zone_HT_tot[hc].ToString() + "','" + zone1.Zone_HT_Inwall[hc].ToString() + "','" + zone1.Zone_HT_Slab[hc].ToString() + "','" + zone1.Zone_HT_Wall.ToString() + "','" + zone1.Zone_HT_Roof.ToString() + "','" + zone1.Zone_HT_Floor.ToString() + "','" + zone1.Zone_HT_GWall.ToString() + "','" + zone1.Zone_HT_Door.ToString() + "','" + zone1.Zone_HT_Win.ToString() + "','" + zone1.Zone_HT_CW.ToString() + "','" +
                          zone1.Zone_HT_Di_Wall.ToString() + "','" + zone1.Zone_HT_Indi_Wall.ToString() + "','" + zone1.Zone_HT_Di_Roof.ToString() + "','" + zone1.Zone_HT_Indi_Roof.ToString() + "','" + zone1.Zone_HT_Di_Win.ToString() + "','" + zone1.Zone_HT_Indi_Win.ToString() + "','" + zone1.Zone_HT_Di_Door.ToString() + "','" + zone1.Zone_HT_Indi_Door.ToString() + "','" +
                          zone1.Zone_HT_TB_tot.ToString() + "','" + zone1.Zone_HT_TB_Wall.ToString() + "','" + zone1.Zone_HT_TB_Roof.ToString() + "','" + zone1.Zone_HT_TB_Floor.ToString() + "','" + zone1.Zone_HT_TB_GWall.ToString() + "','" + zone1.Zone_HT_TB_Win.ToString() + "','" + zone1.Zone_HT_TB_Door.ToString() + "','" + zone1.Zone_HT_TB_CW.ToString() + "','" +
                          zone1.nmech.ToString() + "','" + zone1.nz.ToString() + "','" + zone1.ninf.ToString() + "','" + zone1.nwin.ToString() + "','" +
                          zone1.Zone_HV_tot.ToString() + "','" + zone1.Zone_HV_inf.ToString() + "','" + zone1.Zone_HV_win.ToString() + "','" + zone1.Zone_HV_z.ToString() + "','" + zone1.Zone_HV_mech.ToString() + "','" +
                          zone1.Zone_H_tot[hc, mth].ToString() + "','" + zone1.tao[hc, mth].ToString() + "','" + zone1.dwd_mth[mth].ToString() + "','" + zone1.theta_i[hc, mth].ToString() + "','" + zone1.theta_e[hc, mth].ToString() + "','" +
                          zone1.QTsink_tot[hc, mth].ToString() + "','" + 0 + "','" + zone1.QTsink_Wall[hc, mth].ToString() + "','" + zone1.QTsink_Roof[hc, mth].ToString() + "','" + zone1.QTsink_Floor[hc, mth].ToString() + "','" + zone1.QTsink_GWall[hc, mth].ToString() + "','" + zone1.QTsink_Door[hc, mth].ToString() + "','" + zone1.QTsink_Win[hc, mth].ToString() + "','" + zone1.QTsink_CW[hc, mth].ToString() + "','" +
                          zone1.QTsink_tot[hc, mth].ToString() + "','" + 0 + "','" + zone1.QTsink_Wall[hc, mth].ToString() + "','" + zone1.QTsink_Roof[hc, mth].ToString() + "','" + zone1.QTsink_Floor[hc, mth].ToString() + "','" + zone1.QTsink_GWall[hc, mth].ToString() + "','" + zone1.QTsink_Door[hc, mth].ToString() + "','" + zone1.QTsink_Win[hc, mth].ToString() + "','" + zone1.QTsink_CW[hc, mth].ToString() + "','" +
                          zone1.QS_rad_tot[hc, mth].ToString() + "','" + zone1.QSopsource_tot[hc, mth].ToString() + "','" + zone1.QStr_tot[hc, mth].ToString() + "','" +
                          zone1.QS_rad_Wall[mth].ToString() + "','" + zone1.QS_rad_Roof[mth].ToString() + "','" + zone1.QS_rad_Door[mth].ToString() + "','" + zone1.QS_rad_CW_p[mth].ToString() + "','" +
                          zone1.QSopsource_Wall[mth].ToString() + "','" + zone1.QSopsource_Roof[mth].ToString() + "','" + zone1.QSopsource_Door[mth].ToString() + "','" + zone1.QSopsource_CW_p[mth].ToString() + "','" +
                          zone1.QStr_Win[hc, mth].ToString() + "','" + zone1.QStr_CW[hc, mth].ToString() + "','" +
                          zone1.QVsink_tot[hc, mth].ToString() + "','" + zone1.QV_inf_sink[hc, mth].ToString() + "','" + zone1.QV_win_sink[hc, mth].ToString() + "','" + zone1.QV_z_sink[hc, mth].ToString() + "','" + zone1.QV_mech_sink[hc, mth].ToString() + "','" +
                          zone1.QVsink_tot[hc, mth].ToString() + "','" + zone1.QV_inf_sink[hc, mth].ToString() + "','" + zone1.QV_win_sink[hc, mth].ToString() + "','" + zone1.QV_z_sink[hc, mth].ToString() + "','" + zone1.QV_mech_sink[hc, mth].ToString() + "','" +
                          zone1.Q_DHU_win[mth].ToString() + "','" + zone1.Q_DHU_mech[mth].ToString() + "','" + zone1.Q_DHU_tot[mth].ToString() + "','" +
                          zone1.QI_tot[hc, mth].ToString() + "','" + zone1.QI_L[hc, mth].ToString() + "','" +
                          zone1.QI_P[mth].ToString() + "','" + zone1.QI_fac[mth].ToString() + "','" + zone1.QI_Humidity[mth].ToString() + "','" +
                          zone1.Qsink[hc, mth].ToString() + "','" + zone1.Qsource[hc, mth].ToString() + "','" + zone1.gamma[hc, mth].ToString() + "','" + zone1.a[hc, mth].ToString() + "','" + zone1.eta[hc, mth].ToString() + "','" +
                          zone1.Qb_mth[hc, mth].ToString() + "','" +
                          zone1.Qb_a[hc].ToString() + "','" + zone1.Q_max[hc].ToString() + "','" + zone1.t_max[hc, mth].ToString() + "','" +
                          zone1.Q_fan[mth].ToString()
                          + "'", "번호,난방_냉방,비이용일_이용일,월");
                }
            }

        }
        #endregion

        #region 공조
        public static void AHUSystem_LaodData(AHU AHU1, string ProjNum)
        {
            AHU1.Load_ZoneData(ProjNum);
            AHU1.Load_GeneralData(ProjNum);
            AHU1.Load_AHUData(ProjNum);
            AHU1.Load_DuctData(ProjNum);
            AHU1.Load_PrehPrecData(ProjNum);            
        }
        public static void HRV_LaodData(AHU HRV1, string ProjNum)
        {
            HRV1.Load_ZoneData(ProjNum);
            HRV1.Load_GeneralData(ProjNum);
            HRV1.Load_HRVData(ProjNum);
            HRV1.Load_DuctData(ProjNum);
            HRV1.Load_PrehPrecData(ProjNum);
        }
        public static void AHUSystem_PreCalc(AHU AHU1)
        {
            AHU1.Cal_CoolTube();//
            AHU1.Cal_Preheating();//온도
            AHU1.Cal_Duct();//덕트열손실량
            AHU1.Cal_DuctLoss_OA();
            AHU1.Cal_DuctLoss_RA(); //온도만 계산함
            AHU1.Cal_HeatRecovery();
        }
        public static void AHUSystem_PostCalc(AHU AHU1)
        {
            AHU1.Cal_CoolTube();
            AHU1.Cal_Preheating();
            AHU1.Cal_Duct();
            AHU1.Cal_DuctLoss_OA();
            AHU1.Cal_DuctLoss_RA();//온도만 계산함
            AHU1.Cal_HeatRecovery();
            AHU1.Cal_DuctLoss_EA();
            AHU1.Cal_SA_set();//
            AHU1.Cal_RCA();//
            AHU1.Cal_DuctLoss_SA(); //RA덕트 열손실량을 포함하여 계산함
            AHU1.Cal_Qv_b();
            AHU1.Cal_HU();
            AHU1.Cal_W();
        }
        public static void HRV_PostCalc(AHU HRV1)
        {
            HRV1.Cal_CoolTube();//
            HRV1.Cal_Preheating();
            HRV1.Cal_Duct();
            HRV1.Cal_DuctLoss_OA();
            HRV1.Cal_DuctLoss_RA();//온도만 계산함
            HRV1.Cal_HeatRecovery();
            HRV1.Cal_DuctLoss_EA();
            HRV1.Cal_SA_set();//
            HRV1.Cal_RCA();//
            HRV1.Cal_DuctLoss_SA();//RA덕트 열손실량을 포함하여 계산함
            HRV1.Cal_W();
        }
        private static void AHUSystem_PostSave(AHU AHU1)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            Program.DB.deleteValue(DB.type.ProjDB, "AHUSystem_Result", "번호='" + AHU1.AHUNum + "'And 프로젝트번호 ='" + 프로젝트유형[0][1] + "'");
            String MTH, 난방냉방;
            for (int hc = 0; hc <= 1; hc++)
            {
                for (int mth = 0; mth <= 11; mth++)
                {

                    if (hc == 0) { 난방냉방 = "난방"; } else { 난방냉방 = "냉방"; }
                    MTH = (mth + 1).ToString() + "월";
                    Program.DB.setValue(DB.type.ProjDB, "AHUSystem_Result", "프로젝트번호,프로젝트유형,번호," +
                             "난방_냉방,월," +
                             "공조요구량,가습요구량,급기팬보조에너지,배기팬보조에너지,가습보조에너지,프리히팅보조에너지," +
                             "theta_vmech,Vvmech,Vvmech_leak," +
                             "theta_SA_prh,theta_OA_du,theta_RA_du,theta_SA_hr,theta_SA_rca,theta_SA_du,X_iset," +
                             "X_SA_prh,X_SA_hr,X_SA_rca," +
                             "Vmin_tot,Qb_mth_tot,Qmax_tot,theta_iset_avg,dvmech_avg,tvmech_avg," +
                             "Q_gnd,Q_prh,Q_loss_OA_du,Q_loss_EA_du,Q_loss_SA_du," +
                             "dtheta_prh,dtheta_du_OA,dtheta_du_RA,dtheta_hr,dtheta_rca,dtheta_du_EA,dtheta_du_SA," +
                             "flea_du,flea_ahu,fins_ahu,theta_defrost,theta_sur_nc,Hduct_OA,Hduct_RA,Hduct_EA,Hduct_SA",
                             "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + AHU1.AHUNum + "','" + 난방냉방 + "','" + MTH + "','" +
                             AHU1.Qv_b[hc, mth] + "','" + AHU1.Qhu_b[mth] + "','" + AHU1.Ev_gen_fan_SA[mth] + "','" + AHU1.Ev_gen_fan_EA[mth] + "','" + AHU1.W_HU_aux[mth] + "','" + AHU1.Wv_aux_preh[mth] + "','" +
                             AHU1.theta_vmech[hc, mth] + "','" + AHU1.Vvmech[hc, mth] + "','" + AHU1.Vvmech_leak[hc, mth] + "','" +
                             AHU1.theta_SA_prh[mth] + "','" + AHU1.theta_OA_du[hc, mth] + "','" + AHU1.theta_RA_du[hc, mth] + "','" + AHU1.theta_SA_hr[hc, mth] + "','" + AHU1.theta_SA_rca[hc, mth] + "','" + AHU1.theta_SA_du[hc, mth] + "','" + AHU1.X_iset[mth] + "','" +
                             AHU1.X_SA_prh[mth] + "','" + AHU1.X_SA_hr[mth] + "','" + AHU1.X_SA_rca[mth] + "','" +
                             AHU1.Vmin_tot + "','" + AHU1.Qb_mth_tot[hc, mth] + "','" + AHU1.Qmax_tot[hc] + "','" + AHU1.theta_iset_avg[hc] + "','" + AHU1.dvmechmth_avg[mth] + "','" + AHU1.tvmech_avg + "','" +
                             AHU1.Q_gnd[mth] + "','" + AHU1.Wpreh_k[mth] + "','" + AHU1.Q_loss_OA_du[hc, mth] + "','" + AHU1.Q_loss_EA_du[hc, mth] + "','" + (AHU1.Q_loss_SA_du[hc, mth] + AHU1.Q_loss_RA_du[hc, mth]) + "','" +
                             AHU1.dtheta_prh[mth] + "','" + AHU1.dtheta_du_OA[hc, mth] + "','" + AHU1.dtheta_du_RA[hc, mth] + "','" + AHU1.dtheta_hr[hc, mth] + "','" + AHU1.dtheta_rca[hc, mth] + "','" + AHU1.dtheta_du_EA[hc, mth] + "','" + AHU1.dtheta_du_SA[hc, mth] + "','" +
                             AHU1.flea_du + "','" + AHU1.flea_ahu + "','" + AHU1.fins_ahu + "','" + AHU1.theta_defrost + "','" + AHU1.theta_sur_nc[hc, mth] + "','" + AHU1.Hduct_OA[mth] + "','" + AHU1.Hduct_RA[mth] + "','" + AHU1.Hduct_EA[mth] + "','" + AHU1.Hduct_SA[mth]
                              + "'", "번호,난방_냉방,월");
                }
            }
        }
        private static void HRV_PostSave(AHU HRV1)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            String MTH, 난방냉방;
            for (int hc = 0; hc <= 1; hc++)
            {
                for (int mth = 0; mth <= 11; mth++)
                {
                    if (hc == 0) { 난방냉방 = "난방"; } else { 난방냉방 = "냉방"; }
                    MTH = (mth + 1).ToString() + "월";
                    Program.DB.setValue(DB.type.ProjDB, "AHUSystem_Result", "프로젝트번호,프로젝트유형,번호," +
                             "난방_냉방,월," +
                             "공조요구량,가습요구량,급기팬보조에너지,배기팬보조에너지,가습보조에너지,프리히팅보조에너지," +
                             "theta_vmech,Vvmech,Vvmech_leak," +
                             "theta_SA_prh,theta_OA_du,theta_RA_du,theta_SA_hr,theta_SA_rca,theta_SA_du,X_iset," +
                             "X_SA_prh,X_SA_hr,X_SA_rca," +
                             "Vmin_tot,Qb_mth_tot,Qmax_tot,theta_iset_avg,dvmech_avg,tvmech_avg," +
                             "Q_gnd,Q_prh,Q_loss_OA_du,Q_loss_EA_du,Q_loss_SA_du," +
                             "dtheta_prh,dtheta_du_OA,dtheta_du_RA,dtheta_hr,dtheta_rca,dtheta_du_EA,dtheta_du_SA," +
                             "flea_du,flea_ahu,fins_ahu,theta_defrost,theta_sur_nc,Hduct_OA,Hduct_RA,Hduct_EA,Hduct_SA",
                             "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + HRV1.AHUNum + "','" + 난방냉방 + "','" + MTH + "','" +
                             HRV1.Qv_b[hc, mth] + "','" + HRV1.Qhu_b[mth] + "','" + HRV1.Ev_gen_fan_SA[mth] + "','" + HRV1.Ev_gen_fan_EA[mth] + "','" + HRV1.W_HU_aux[mth] + "','" + HRV1.Wv_aux_preh[mth] + "','" +
                             HRV1.theta_vmech[hc, mth] + "','" + HRV1.Vvmech[hc, mth] + "','" + HRV1.Vvmech_leak[hc, mth] + "','" +
                             HRV1.theta_SA_prh[mth] + "','" + HRV1.theta_OA_du[hc, mth] + "','" + HRV1.theta_RA_du[hc, mth] + "','" + HRV1.theta_SA_hr[hc, mth] + "','" + HRV1.theta_SA_rca[hc, mth] + "','" + HRV1.theta_SA_du[hc, mth] + "','" + HRV1.X_iset[mth] + "','" +
                             HRV1.X_SA_prh[mth] + "','" + HRV1.X_SA_hr[mth] + "','" + HRV1.X_SA_rca[mth] + "','" +
                             HRV1.Vmin_tot + "','" + HRV1.Qb_mth_tot[hc, mth] + "','" + HRV1.Qmax_tot[hc] + "','" + HRV1.theta_iset_avg[hc] + "','" + HRV1.dvmechmth_avg[mth] + "','" + HRV1.tvmech_avg + "','" +
                             HRV1.Q_gnd[mth] + "','" + HRV1.Wpreh_k[mth] + "','" + HRV1.Q_loss_OA_du[hc, mth] + "','" + HRV1.Q_loss_EA_du[hc, mth] + "','" + (HRV1.Q_loss_SA_du[hc, mth] + HRV1.Q_loss_RA_du[hc, mth]) + "','" +
                             HRV1.dtheta_prh[mth] + "','" + HRV1.dtheta_du_OA[hc, mth] + "','" + HRV1.dtheta_du_RA[hc, mth] + "','" + HRV1.dtheta_hr[hc, mth] + "','" + HRV1.dtheta_rca[hc, mth] + "','" + HRV1.dtheta_du_EA[hc, mth] + "','" + HRV1.dtheta_du_SA[hc, mth] + "','" +
                             HRV1.flea_du + "','" + HRV1.flea_ahu + "','" + HRV1.fins_ahu + "','" + HRV1.theta_defrost + "','" + HRV1.theta_sur_nc[hc, mth] + "','" + HRV1.Hduct_OA[mth] + "','" + HRV1.Hduct_RA[mth] + "','" + HRV1.Hduct_EA[mth] + "','" + HRV1.Hduct_SA[mth]
                              + "'", "번호,난방_냉방,월");
                }
            }

        }
        #endregion

        #region 난방
        public static void Heating_ce_zone_calc(string ProjNum)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            string[][] HeatingNum = Program.DB.getValue(ProjNum, "HeatingSystem_Form", "번호");
            int i = -1;
            String MTH;
            string[][] Zone = Program.DB.getValue(ProjNum, "ZoneGeneral_Form", "존번호,기존존", "냉난방유무 ='냉난방' OR 냉난방유무 = '난방'");

            while (++i < HeatingNum.Length)
            {
                if (Zone.Length > 0)
                {
                    for (int n = 0; n < Zone.Length; n++)
                    {
                        Zone zone = Program.CALC.getZone(Zone[n][0].ToString());
                        string[][] ce = Program.DB.getValue(ProjNum, "Heating_ce_Form", "공급설비,공급설비종류,가동시간,난방시스템,설치위치", "존번호 = '" + Zone[n][0] + "'");
                        double[] 가동비율 = new double[ce.Length];
                        double 가동비율_tot = 0;

                        for (int a = 0; a < ce.Length; a++)
                        {
                            string[][] ce2 = Program.DB.getValue(ProjNum, "User_ce", "용량_난방", "번호='" + ce[a][0].Substring(0, 4) + "'");
                            if (ce2[0][0] != "")
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
                            if (HeatingNum[i][0] == ce[a][3])
                            {

                                Program.DB.setValue(DB.type.ProjDB, "Heating_ce_Form", "존번호,프로젝트유형,난방시스템,공급설비,부하율",
                            "'" + Zone[n][0] + "','" + 프로젝트유형[0][0] + "','"
                            + HeatingNum[i][0] + "','"
                            + ce[a][0] + "','"
                            + (가동비율[a] / 가동비율_tot) + "'", "존번호,난방시스템,공급설비");
                            }
                        }
                    }
                }
            }
        }

        public static void Heating_ce_zone_calc_Element(string ProjNum)
        {
            Program.DB.deleteTable(DB.type.ProjDB, "Heating_ce_Form_Element");
            Program.DB.initTable(DB.type.ProjDB, "Heating_ce_Form_Element");
            string[][] Zone = Program.DB.getValue(ProjNum, "ZoneGeneral_Form", "존번호,기존존", "냉난방유무 ='냉난방' OR 냉난방유무 = '난방'");
            string[][] PostZone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,기존존", "");
            if (Zone.Length > 0 && PostZone.Length >0)
            {
                double[] 가동비율_tot_Element = new double[PostZone.Length];
                for (int k = 0; k < PostZone.Length; k++)
                {
                    for (int n = 0; n < Zone.Length; n++)
                    {
                        string[][] ce = Program.DB.getValue(ProjNum, "Heating_ce_Form", "공급설비,공급설비종류,가동시간,난방시스템,설치위치", "존번호 = '" + Zone[n][0] + "'");
                        for (int a = 0; a < ce.Length; a++)
                        {

                            ArrayList split = Split_(PostZone[k][1]);
                            for (int x = 0; x < split.Count; x++)
                            {
                                if (split[x].ToString() == Zone[n][0])
                                {

                                    string[][] value = Program.DB.querySQL(ProjNum, "select a.Qb_a, b.부하율 from Zone_HCneed_Result as a Inner Join Heating_ce_Form as b on a.번호= b.존번호 where a.난방_냉방='난방' and a.비이용일_이용일 ='이용일' and 월='1월' and a.번호='" + split[x].ToString() + "' and b.공급설비='" + ce[a][0] + "'");
                                    if (value.Length > 0)
                                    {
                                        가동비율_tot_Element[k] += Convert.ToDouble(value[0][0]) * Convert.ToDouble(value[0][1]);
                                    }
                                    goto goto_End;
                                }
                            }
                         goto_End: a = a;
                        }
                    }                
                }
                for (int k = 0; k < PostZone.Length; k++)
                {
                    for (int n = 0; n < Zone.Length; n++)
                    {

                        string[][] ce = Program.DB.getValue(ProjNum, "Heating_ce_Form", "공급설비,공급설비종류,가동시간,난방시스템,설치위치", "존번호 = '" + Zone[n][0] + "'");
                        double[] 가동비율 = new double[ce.Length];
                        for (int a = 0; a < ce.Length; a++)
                        {

                            ArrayList split = Split_(PostZone[k][1]);
                            for (int x = 0; x < split.Count; x++)
                            {
                                if (split[x].ToString() == Zone[n][0])
                                {

                                    string[][] value = Program.DB.querySQL(ProjNum, "select a.Qb_a, b.부하율 from Zone_HCneed_Result as a Inner Join Heating_ce_Form as b on a.번호= b.존번호 where a.난방_냉방='난방' and a.비이용일_이용일 ='이용일' and 월='1월' and a.번호='" + split[x].ToString() + "' and b.공급설비='" + ce[a][0] + "'");
                                    if (value.Length > 0)
                                    {
                                        가동비율[a] = Convert.ToDouble(value[0][0]) * Convert.ToDouble(value[0][1]);
                                    }
                                    goto goto_End;
                                }
                            }
                         goto_End: a = a;
                        }
                        for (int a = 0; a < ce.Length; a++)
                        {
                            if(가동비율[a] >0 && 가동비율_tot_Element[k] >0)
                            {
                                Program.DB.setValue(DB.type.ProjDB, "Heating_ce_Form_Element", "존번호,난방시스템,공급설비종류,공급설비,설치위치,가동시간,부하율",
                            "'" + Zone[n][0] + "','"
                            + ce[a][3] + "','" + ce[a][1] + "','" + ce[a][0] + "','" + ce[a][4] + "','" + ce[a][2] + "','"
                            + (가동비율[a] / 가동비율_tot_Element[k]) + "'", "존번호,난방시스템,공급설비");
                            }                            
                        }
                    }
                }
            }
        }



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
        public static void Heating_LoadData(Heating Heating1, string ProjNum)
        {
            Heating1.Load_Zonedata(ProjNum);
            Heating1.Load_AHUdata(ProjNum); 
            Heating1.Load_HeatingGeneral(ProjNum);
            Heating1.Load_Boiler_general(ProjNum);
            Heating1.Load_Solar_general(ProjNum);
            Heating1.Load_FC_general(ProjNum);
            Heating1.Load_PumpData(ProjNum);
            Heating1.Load_ceData(ProjNum);
            Heating1.Load_StorageData(ProjNum);
            Heating1.Load_PipeData(ProjNum);
            Heating1.Load_AirHP_general(ProjNum);
            Heating1.Load_GroundHP_general(ProjNum);
            Heating1.Load_GWHP_general(ProjNum);
            Heating1.Load_ABS_general(ProjNum);
            Heating1.Load_DH_general(ProjNum);
            Heating1.Load_ce(ProjNum);
        }
        public static void Heating_Calc(Heating Heating1,string ProjNum)
        {
            Heating1.Calc_thrL();
            Heating1.Calc_beta_ce();
            Heating1.Calc_Qce(ProjNum);
            Heating1.Calc_beta_d();
            Heating1.Calc_Qd(ProjNum);
            Heating1.Calc_beta_s();
            Heating1.Calc_Qh_s(ProjNum);
            Heating1.Calc_beta_gen();
            Heating1.LoadCalc_Solar(ProjNum);
            Heating1.LoadCalc_FC(ProjNum);
            Heating1.LoadCalc_Boiler(ProjNum);
            Heating1.LoadCalc_AirHP(ProjNum);
            Heating1.LoadCalc_GroundHP(ProjNum);
            Heating1.LoadCalc_GWHP(ProjNum);
            Heating1.LoadCalc_ABS(ProjNum);
            Heating1.LoadCalc_DH(ProjNum);
            Heating1.nan();
        }
        public static void Heating_Save(Heating Heating1)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            for (int mth = 0; mth <= 11; mth++)
            {

                string MTH = (mth + 1).ToString() + "월";
                Program.DB.setValue(DB.type.ProjDB, "HeatingSystem_Result", "프로젝트번호,프로젝트유형,번호," +
                         "월," +
                         " Qhb_mth_sum, Qh_max_sum,Qh_a_sum,th_op_day_avg, theta_i_h_set_avg,th_avg,dop_mth_avg," +
                         "thrL,thrL_day,dhrB,fLNA,fLwe," +
                         "beta_h_ce,beta_h_d,beta_h_s,beta_h_gen," +
                         "theta_av_ce,theta_av_d,theta_av_s,theta_av_gen," +
                         "dtheta_ce,dtheta_d,dtheta_s,dtheta_gen," +
                         "dtheta_ce1,dtheta_ce2,Psi_pipe,L,Qs_po_day,Vs," +
                         "Qh_gen_day,Pgen_Pn,Pgen_Pint,Pgen_P0,eta_gen_Pn,eta_gen_Pint," +
                         "fpint_Air,Qh_outg_sngminus7,Qh_outg_sng2,Qh_outg_sng7,COPminus7,COP2,COP7," +
                         "Qh_ce,Qh_d,Qh_s,Qh_gen,Qh_outg,Qh_f," +
                         "Wh_ce,Wh_d,Wh_s,Wh_g,연료",
                         "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + Heating1.HeatingNum + "','" + MTH + "','" +
                          Heating1.Qhb_mth_sum[mth] + "','" + Heating1.Qh_max_sum + "','" + Heating1.Qh_a_sum + "','" + Heating1.th_op_day_avg + "','" + Heating1.theta_i_h_set_avg + "','" + Heating1.th_avg[mth] + "','" + Heating1.dop_mth_avg[mth] + "','" +
                          Heating1.thrL[mth] + "','" + Heating1.thrL_day[mth] + "','" + Heating1.dhrB[mth] + "','" + Heating1.fLNA[mth] + "','" + Heating1.fLwe[mth] + "','" +
                          Heating1.beta_h_ce[mth] + "','" + Heating1.beta_h_d[mth] + "','" + Heating1.beta_h_s[mth] + "','" + Heating1.beta_h_gen[mth] + "','" +
                          Heating1.theta_av_ce[mth] + "','" + Heating1.theta_av_d[mth] + "','" + Heating1.theta_av_s[mth] + "','" + Heating1.theta_av_gen[mth] + "','" +
                          Heating1.dtheta_ce[mth] + "','" + Heating1.dtheta_d[mth] + "','" + Heating1.dtheta_s[mth] + "','" + Heating1.dtheta_gen[mth] + "','" +
                          Heating1.dtheta_ce1 + "','" + Heating1.dtheta_ce2 + "','" + Heating1.Psi_pipe + "','" + Heating1.PipeL + "','" + Heating1.Qs_po_day + "','" + Heating1.Vs + "','" +
                          Heating1.Qh_gen_day[mth] + "','" + Heating1.Pgen_Pn[mth] + "','" + Heating1.Pgen_Pint[mth] + "','" + Heating1.Pgen_P0[mth] + "','" + Heating1.eta_gen_Pn[mth] + "','" + Heating1.eta_gen_Pint[mth] + "','" +
                          Heating1.fpint[mth] + "','" + Heating1.Qh_outg_sng[0, mth] + "','" + Heating1.Qh_outg_sng[1, mth] + "','" + Heating1.Qh_outg_sng[2, mth] + "','" + Heating1.COPpint[0, mth] + "','" + Heating1.COPpint[1, mth] + "','" + Heating1.COPpint[2, mth] + "','" +
                          Heating1.Qh_ce[mth] + "','" + Heating1.Qh_d[mth] + "','" + Heating1.Qh_s[mth] + "','" + Heating1.Qh_gen[mth] + "','" + Heating1.Qh_outg[mth] + "','" + Heating1.Qh_f[mth] + "','" +
                          Heating1.Wh_ce[mth] + "','" + Heating1.Wh_d[mth] + "','" + Heating1.Wh_s[mth] + "','" + Heating1.Wh_g[mth] + "','" + Heating1.Carrier
                          + "'", "번호,월"); ;
                Program.DB.setValue(DB.type.ProjDB, "HeatingSystem_Result", "프로젝트번호,프로젝트유형,번호," +
                       "월," +
                       "Qh_sol",
                       "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + Heating1.HeatingNum + "','" + MTH + "','" +
                        Heating1.Qh_sol[mth]
                        + "'", "번호,월"); ;
                Program.DB.setValue(DB.type.ProjDB, "HeatingSystem_Result", "프로젝트번호,프로젝트유형,번호," +
                      "월," +
                      "Qhb_z,Qh_ce_z,Qh_d_z,Qh_s_z,Qh_outg_z," +
                      "Qhb_ahu,Qh_ce_ahu,Qh_d_ahu,Qh_s_ahu,Qh_outg_ahu",
                      "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + Heating1.HeatingNum + "','" + MTH + "','" +
                      Heating1.Qhb_z[mth]+ "','" + Heating1.Qh_ce_z[mth] + "','" + Heating1.Qh_d_z[mth] + "','" + Heating1.Qh_s_z[mth] + "','" + Heating1.Qh_outg_z[mth]  + "','" +
                      Heating1.Qhb_ahu[mth] + "','" + Heating1.Qh_ce_ahu[mth] + "','" + Heating1.Qh_d_ahu[mth] + "','" + Heating1.Qh_s_ahu[mth] + "','" + Heating1.Qh_outg_ahu[mth]
                       + "'", "번호,월");
               
            }
        }
        #endregion
        #region 냉방 
        public static void Cooling_LoadData(Cal_Cooling cc1, string ProjNum)
        {
            cc1.Generator_Check(ProjNum);
            cc1.Load_CoolingZone(ProjNum);
            cc1.Cooling_CE_Zone(ProjNum);
        }
        public static void Cooling_Calc(Cal_Cooling cc1, string ProjNum)
        { //냉방 설비 만들기
            cc1.Cooling_Generator(ProjNum);
            //냉방 설비 종합
            cc1.Generator_Sum();
            //공급설비 기준 부하율 반영
            cc1.Cal_CLRate();
            //최대부하,연간요구량,일일작동시간, 면적
            cc1.Cal_ZoneAhu(ProjNum);
            //냉방존
            cc1.Cal_Zone(ProjNum);
            //공조존         
            cc1.Cal_Ahu(ProjNum);
            //계산시작
            cc1.Find_Climate();
            //냉방존과 공조존을 합치기
            cc1.Cal_Load();
            //에너지소요량 계산
            cc1.Cal_CS(ProjNum);
            //보조설비에너지소요량 계산
            cc1.Cal_AuxSum(ProjNum);

        }
        public static void Cooling_ce_zone_calc(string ProjNum)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            string[][] Num = Program.DB.getValue(ProjNum, "CoolingSystem_Form", "번호");
            int i = -1;
            String MTH;
            string[][] Zone = Program.DB.getValue(ProjNum, "ZoneGeneral_Form", "존번호,기존존", "냉난방유무 ='냉난방' OR 냉난방유무 = '냉방'");

            while (++i < Num.Length)
            {
                if (Zone.Length > 0)
                {
                    for (int n = 0; n < Zone.Length; n++)
                    {
                        Zone zone = Program.CALC.getZone(Zone[n][0].ToString());
                        string[][] ce = Program.DB.getValue(ProjNum, "Cooling_ce_Form", "공급설비,공급설비종류,가동시간,냉방시스템", "존번호 = '" + Zone[n][0] + "'");
                        double[] 가동비율 = new double[ce.Length];
                        double 가동비율_tot = 0;
                        double[] 용량 = new double[ce.Length]; double[] 소비전력 = new double[ce.Length];
                        for (int a = 0; a < ce.Length; a++)
                        {
                            string[][] ce2 = Program.DB.getValue(ProjNum, "User_ce", "용량_냉방,소비전력_냉방", "번호='" + ce[a][0].Substring(0, 4) + "'");
                            if (ce2[0][0] != "" && Convert.ToDouble(ce2[0][0]) > 0)
                            {
                                용량[a] = Convert.ToDouble(ce2[0][0]);
                                if (ce2[0][1] != "")
                                {
                                    소비전력[a] = Convert.ToDouble(ce2[0][1]);
                                }
                                가동비율[a] = Convert.ToDouble(ce[a][2]) * Convert.ToDouble(ce2[0][0]);
                                가동비율_tot += 가동비율[a];
                            }
                            else
                            {
                                if (ce2[0][1] != "")
                                {
                                    소비전력[a] = Convert.ToDouble(ce2[0][1]);
                                }
                                가동비율[a] = Convert.ToDouble(ce[a][2]) * zone.Q_max[1] / 1000;
                                가동비율_tot += 가동비율[a];
                            }
                        }

                        for (int a = 0; a < ce.Length; a++)
                        {
                            if (Num[i][0] == ce[a][3])
                            {

                                Program.DB.setValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호,프로젝트유형,냉방시스템,공급설비,용량,소비전력,부하율",
                            "'" + Zone[n][0] + "','" + 프로젝트유형[0][0] +  "','"
                            + Num[i][0] + "','"
                            + ce[a][0] + "','" + 용량[a] + "','" + 소비전력[a] + "','"
                            + (가동비율[a] / 가동비율_tot) + "'", "존번호,냉방시스템,공급설비");
                            }
                        }
                    }
                }
            }
        }

        public static void Cooling_ce_zone_calc_Element(string ProjNum)
        {
            Program.DB.deleteTable(DB.type.ProjDB, "Cooling_ce_Form_Element");
            Program.DB.initTable(DB.type.ProjDB, "Cooling_ce_Form_Element");
            string[][] Zone = Program.DB.getValue(ProjNum, "ZoneGeneral_Form", "존번호,기존존", "냉난방유무 ='냉난방' OR 냉난방유무 = '냉방'");
            string[][] PostZone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,기존존", "");
            if (Zone.Length > 0 && PostZone.Length > 0)
            {
                double[] 가동비율_tot_Element = new double[PostZone.Length];
                for (int k = 0; k < PostZone.Length; k++)
                {
                    for (int n = 0; n < Zone.Length; n++)
                    {
                        string[][] ce = Program.DB.getValue(ProjNum, "Cooling_ce_Form", "공급설비,공급설비종류,가동시간,냉방시스템,용량,소비전력", "존번호 = '" + Zone[n][0] + "'");
                        for (int a = 0; a < ce.Length; a++)
                        {

                            ArrayList split = Split_(PostZone[k][1]);
                            for (int x = 0; x < split.Count; x++)
                            {
                                if (split[x].ToString() == Zone[n][0])
                                {

                                    string[][] value = Program.DB.querySQL(ProjNum, "select a.Qb_a, b.부하율 from Zone_HCneed_Result as a Inner Join Cooling_ce_Form as b on a.번호= b.존번호 where a.난방_냉방='냉방' and a.비이용일_이용일 ='이용일' and 월='1월' and a.번호='" + split[x].ToString() + "' and b.공급설비='" + ce[a][0] + "'");
                                    if (value.Length > 0)
                                    {
                                        가동비율_tot_Element[k] += Convert.ToDouble(value[0][0]) * Convert.ToDouble(value[0][1]);
                                    }
                                    goto goto_End;
                                }
                            }
                        goto_End: a = a;
                        }
                    }
                }
                for (int k = 0; k < PostZone.Length; k++)
                {
                    for (int n = 0; n < Zone.Length; n++)
                    {

                        string[][] ce = Program.DB.getValue(ProjNum, "Cooling_ce_Form", "공급설비,공급설비종류,가동시간,냉방시스템,용량,소비전력", "존번호 = '" + Zone[n][0] + "'");
                        double[] 가동비율 = new double[ce.Length];
                        for (int a = 0; a < ce.Length; a++)
                        {

                            ArrayList split = Split_(PostZone[k][1]);
                            for (int x = 0; x < split.Count; x++)
                            {
                                if (split[x].ToString() == Zone[n][0])
                                {

                                    string[][] value = Program.DB.querySQL(ProjNum, "select a.Qb_a, b.부하율 from Zone_HCneed_Result as a Inner Join Cooling_ce_Form as b on a.번호= b.존번호 where a.난방_냉방='냉방' and a.비이용일_이용일 ='이용일' and 월='1월' and a.번호='" + split[x].ToString() + "' and b.공급설비='" + ce[a][0] + "'");
                                    if (value.Length > 0)
                                    {
                                        가동비율[a] = Convert.ToDouble(value[0][0]) * Convert.ToDouble(value[0][1]);
                                    }
                                    goto goto_End;
                                }
                            }
                        goto_End: a = a;
                        }
                        for (int a = 0; a < ce.Length; a++)
                        {
                            if (가동비율[a] > 0 && 가동비율_tot_Element[k] > 0)
                            {
                                Program.DB.setValue(DB.type.ProjDB, "Cooling_ce_Form_Element", "존번호,냉방시스템,공급설비종류,공급설비,가동시간,용량,소비전력,부하율",
                            "'" + Zone[n][0] + "','"
                            + ce[a][3] + "','" + ce[a][1] + "','" + ce[a][0] + "','" + ce[a][2] + "','" + ce[a][4] + "','" + ce[a][5] + "','"
                            + (가동비율[a] / 가동비율_tot_Element[k]) + "'", "존번호,냉방시스템,공급설비");
                            }
                        }
                    }
                }
            }
        }



        public static void Cooling_Save(Cal_Cooling cc1)
        {
            //설비정보와 보조설비정보는 따로따로
            cc1.QCa_nd = 0;
            cc1.QCa_ce = 0;
            cc1.QCa_d = 0;
            cc1.QCa_s = 0;
            cc1.QCa_out = 0;
            cc1.QCa_f = 0;
            cc1.QCa_p = 0;

            for (int i = 0; i < 12; i++)
            {
                cc1.QCa_nd += cc1.QC_nd[i];
                cc1.QCa_ce += cc1.QC_ce[i];
                cc1.QCa_d += cc1.QC_d[i];
                cc1.QCa_s += cc1.QC_s[i];
                cc1.QCa_out += cc1.QC_out[i];
                cc1.QCa_f += cc1.QC_f[i];

            }

            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            string[] mth = new string[12];
            for (int i = 0; i < 12; i++)
            {
                mth[i] = (i+1).ToString() + "월";

                Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "프로젝트유형,프로젝트번호, 번호, 명칭, 냉방설비, 냉방출력, 냉방성능, 대기전력, 설치대수, Fuel,월,열원설비",
                "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + cc1.CoolingNum + "','" + cc1.CoolingName + "','" + cc1.CG + "','" + cc1.Power_f + "','" + cc1.EER_f + "','" + cc1.Pctrl_f + "','" + cc1.Number_f + "','" + cc1.Carrier + "','" + mth[i] + "','" + cc1.CSource + "'", "번호,월");
                if (double.IsNaN(cc1.QCa_nd)) { cc1.QCa_nd = 0; }
                if (double.IsNaN(cc1.QCa_ce)) { cc1.QCa_ce = 0; }
                if (double.IsNaN(cc1.QCa_d)) { cc1.QCa_d = 0; }
                if (double.IsNaN(cc1.QCa_s)) { cc1.QCa_s = 0; }
                if (double.IsNaN(cc1.QCa_out)) { cc1.QCa_out = 0; }
                if (double.IsNaN(cc1.QCa_f)) { cc1.QCa_f = 0; }
                Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,QCb_a,QCa_ce,QCa_d,QCa_s,QCa_out,QCa_f,Sto_Tank,Sto_Type",
                          "'" + cc1.CoolingNum + "','" + mth[i] + "','" + cc1.QCa_nd + "','" + cc1.QCa_ce + "','" + cc1.QCa_d + "','" + cc1.QCa_s + "','" + cc1.QCa_out + "','" + cc1.QCa_f + "','" + cc1.Sto_Tank + "','" + cc1.Sto_Type + "'", "번호,월");
                if (double.IsNaN(cc1.QC_f[i])) { cc1.QC_f[i] = 0; }
                if (double.IsNaN(cc1.SEER_c[i])) { cc1.SEER_c[i] = 0; }
                if (double.IsNaN(cc1.EER_c[i])) { cc1.EER_c[i] = 0; }
                if (double.IsNaN(cc1.QC_out[i])) { cc1.QC_out[i] = 0; }
                if (double.IsNaN(cc1.QC_ce[i])) { cc1.QC_ce[i] = 0; }
                if (double.IsNaN(cc1.QC_d[i])) { cc1.QC_d[i] = 0; }
                if (double.IsNaN(cc1.QC_s[i])) { cc1.QC_s[i] = 0; }
                if (double.IsNaN(cc1.QC_nd[i])) { cc1.QC_nd[i] = 0; }
                Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,QC_f, SEER_c, EER_c,QC_out,QC_ce,QC_d,QC_s,QC_nd",
                           "'" + cc1.CoolingNum + "','" + mth[i] + "','" + cc1.QC_f[i] + "', '" + cc1.SEER_c[i] + "','" + cc1.EER_c[i] + "','" + cc1.QC_out[i] + "','" + cc1.QC_ce[i] + "','" + cc1.QC_d[i] + "','" + cc1.QC_s[i] + "','" + cc1.QC_nd[i] + "'", "번호,월");
                if (double.IsNaN(cc1.W[i])) { cc1.W[i] = 0; }
                if (double.IsNaN(cc1.W_ce[i])) { cc1.W_ce[i] = 0; }
                if (double.IsNaN(cc1.W_d[i])) { cc1.W_d[i] = 0; }
                if (double.IsNaN(cc1.W_s[i])) { cc1.W_s[i] = 0; }
                if (double.IsNaN(cc1.W_g[i])) { cc1.W_g[i] = 0; }
                Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,W,W_g,W_ce,W_d,W_s",
                           "'" + cc1.CoolingNum + "','" + mth[i] + "','" + cc1.W[i] + "', '" + cc1.W_g[i] + "','" + cc1.W_ce[i] + "','" + cc1.W_d[i] + "','" + cc1.W_s[i] + "'", "번호,월");


                if (cc1.ZoneNameList.Count > 0)
                {

                    if (double.IsNaN(cc1.ZoneNumber_f)) { cc1.ZoneNumber_f = 0; }
                    if (double.IsNaN(cc1.QC_a_z)) { cc1.QC_a_z = 0; }
                    if (double.IsNaN(cc1.Qc_max_z)) { cc1.Qc_max_z = 0; }
                    if (double.IsNaN(cc1.A_z)) { cc1.A_z = 0; }
                    Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,개수_z,QCb_a_z,QC_Max_z,공급설비1_z,공급설비2_z,A_z",
                           "'" + cc1.CoolingNum + "','" + mth[i] + "','" + cc1.ZoneNumber_f + "','" + cc1.QC_a_z + "','" + cc1.Qc_max_z + "','" + cc1.CE1_z + "','" + cc1.CE2_z + "','" + cc1.A_z + "'", "번호,월");

                    if (double.IsNaN(cc1.QC_out_z[i])) { cc1.QC_out_z[i] = 0; }
                    if (double.IsNaN(cc1.QC_ce_z[i])) { cc1.QC_ce_z[i] = 0; }
                    if (double.IsNaN(cc1.QC_d_z[i])) { cc1.QC_d_z[i] = 0; }
                    if (double.IsNaN(cc1.QC_s_z[i])) { cc1.QC_s_z[i] = 0; }
                    if (double.IsNaN(cc1.QC_nd_z[i])) { cc1.QC_nd_z[i] = 0; }
                    Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,QC_out_z, QC_ce_z, QC_d_z, QC_s_z, QC_nd_z",
                               "'" + cc1.CoolingNum + "','" + mth[i] + "','" + cc1.QC_out_z[i] + "','" + cc1.QC_ce_z[i] + "','" + cc1.QC_d_z[i] + "','" + cc1.QC_s_z[i] + "','" + cc1.QC_nd_z[i] + "'", "번호,월");

                }
                if (cc1.AhuNameList.Count > 0)
                {
                    if (double.IsNaN(cc1.AhuNumber_f)) { cc1.AhuNumber_f = 0; }
                    if (double.IsNaN(cc1.QC_a_ahu)) { cc1.QC_a_ahu = 0; }
                    if (double.IsNaN(cc1.Qc_max_ahu)) { cc1.Qc_max_ahu = 0; }
                    if (double.IsNaN(cc1.A_ahu)) { cc1.A_ahu = 0; }
                    Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,개수_ahu,QCb_a_ahu,QC_Max_ahu,공급설비1_ahu,공급설비2_ahu,A_ahu",
                           "'" + cc1.CoolingNum + "','" + mth[i] + "','" + cc1.AhuNumber_f + "','" + cc1.QC_a_ahu + "','" + cc1.Qc_max_ahu + "','" + cc1.CE1_ahu + "','" + cc1.CE2_ahu + "','" + cc1.A_ahu + "'", "번호,월");

                    if (double.IsNaN(cc1.QC_out_ahu[i])) { cc1.QC_out_ahu[i] = 0; }
                    if (double.IsNaN(cc1.QC_ce_ahu[i])) { cc1.QC_ce_ahu[i] = 0; }
                    if (double.IsNaN(cc1.QC_d_ahu[i])) { cc1.QC_d_ahu[i] = 0; }
                    if (double.IsNaN(cc1.QC_s_ahu[i])) { cc1.QC_s_ahu[i] = 0; }
                    if (double.IsNaN(cc1.QC_nd_ahu[i])) { cc1.QC_nd_ahu[i] = 0; }
                    Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,QC_out_ahu, QC_ce_ahu, QC_d_ahu, QC_s_ahu, QC_nd_ahu",
                              "'" + cc1.CoolingNum + "','" + mth[i] + "','" + cc1.QC_out_ahu[i] + "','" + cc1.QC_ce_ahu[i] + "','" + cc1.QC_d_ahu[i] + "','" + cc1.QC_s_ahu[i] + "','" + cc1.QC_nd_ahu[i] + "'", "번호,월");

                }
                if (cc1.CG != "실외기12kW")
                {

                    Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,압축기종류",
                                               "'" + cc1.CoolingNum + "','" + mth[i] + "','" + cc1.Comp_f + "','" + cc1.CWout + "'", "번호,월");
                }
                if (cc1.펌프유무 == "펌프 있음") //펌프유무
                {
                    if (double.IsNaN(cc1.CSWin)) { cc1.CSWin = 0; }
                    if (double.IsNaN(cc1.CSWout)) { cc1.CSWout = 0; }
                    if (double.IsNaN(cc1.P1power)) { cc1.P1power = 0; }
                    if (double.IsNaN(cc1.P2power)) { cc1.P2power = 0; }
                    if (double.IsNaN(cc1.SP1power)) { cc1.SP1power = 0; }
                    if (double.IsNaN(cc1.SP2power)) { cc1.SP2power = 0; }
                    if (double.IsNaN(cc1.CWout)) { cc1.CWout = 0; }
                    Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,CSWin,CSWout,P1power,P2power,Pump1Valve,SP1power,SP2power,SPValve,냉수출구온도",
                                               "'" + cc1.CoolingNum + "','" + mth[i] + "','" + cc1.CSWin + "','" + cc1.CSWout + "','" + cc1.P1power + "','" + cc1.P2power + "','" + cc1.PumpControl + "','" + cc1.SP1power + "','" + cc1.SP2power + "','" + cc1.SPumpControl + "','" + cc1.CWout + "'", "번호,월");
                }
                if (cc1.CG == "수냉식냉동기" || cc1.CG == "흡수식냉동기")
                {
                    if (double.IsNaN(cc1.CTPower_f)) { cc1.CTPower_f = 0; }
                    Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,CTPower",
                                               "'" + cc1.CoolingNum + "','" + mth[i] + "','" + cc1.CTPower_f + "'", "번호,월");
                }
            }
        }
        #endregion

        #region 급탕
        public static void DHW_LoadData(DHW DHW1, string ProjNum)
        {
            DHW1.Load_Zonedata(ProjNum);
            DHW1.Load_DHWGeneral(ProjNum);
            DHW1.Load_Boiler_general(ProjNum);
            DHW1.Load_Solar_general(ProjNum);
            DHW1.Load_FC_general(ProjNum);
            DHW1.Load_HP_general(ProjNum);
            DHW1.Load_DH_general(ProjNum);
            DHW1.Load_PumpData(ProjNum);
            DHW1.Load_StorageData(ProjNum);
            DHW1.Load_PipeData(ProjNum);
        }
        public static void DHW_Calc(DHW DHW1, string ProjNum)
        {
            DHW1.Calc_Qd(ProjNum);
            DHW1.Calc_Qh_s(ProjNum);
            DHW1.LoadCalc_Solar(ProjNum);
            DHW1.LoadCalc_FC(ProjNum);
            DHW1.LoadCalc_Boiler(ProjNum);
            DHW1.LoadCalc_HP(ProjNum);
            DHW1.LoadCalc_DH(ProjNum);
            DHW1.nan();
        }
        private static void DHW_Save(DHW DHW1)
        {
            
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            int i = -1;
            String MTH;
            
            for (int mth = 0; mth <= 11; mth++)
            {

                MTH = (mth + 1).ToString() + "월";
                Program.DB.setValue(DB.type.ProjDB, "DHWSystem_Result", "프로젝트번호,프로젝트유형,번호," +
                         "월," +
                         "Qwb_mth_sum,theta_ih_avg,Qw_a_sum,th_op_day_avg,theta_i_h_set_avg,dop_mth_avg," +
                         "Qw_d,Qw_s,Qw_gen,Qw_outg,Qw_f," +
                         "Ww_d,Ww_s,Ww_g," +
                         "Qw_gen_day,Qw_gen_p0_day,eta_pn_w,연료",
                         "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + DHW1.DHWNum + "','" + MTH + "','" +
                         DHW1.Qwb_mth_sum[mth] + "','" + DHW1.theta_ih_avg[mth] + "','" + DHW1.Qw_a_sum + "','" + DHW1.th_op_day_avg + "','" + DHW1.theta_i_h_set_avg + "','" + DHW1.dop_mth_avg[mth] + "','" +
                         DHW1.Qw_d[mth] + "','" + DHW1.Qw_s[mth] + "','" + DHW1.Qw_gen[mth] + "','" + DHW1.Qw_outg[mth] + "','" + DHW1.Qw_f[mth] + "','" +
                         DHW1.Ww_d[mth] + "','" + DHW1.Ww_s[mth] + "','" + DHW1.Ww_g[mth] + "','" +
                         DHW1.Qw_gen_day[mth] + "','" + DHW1.Qw_gen_p0_day[mth] + "','" + DHW1.eta_pn_w[mth] + "','" + DHW1.Carrier
                          + "'", "번호,월"); ;

                Program.DB.setValue(DB.type.ProjDB, "DHWSystem_Result", "프로젝트번호,프로젝트유형,번호," +
                        "월," +
                        "Qw_sol",
                        "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + DHW1.DHWNum + "','" + MTH + "','" +
                        DHW1.Qw_sol[mth]
                         + "'", "번호,월"); ;
            }
        }
        #endregion

        #region 파이널       
        public static void Final_Calc(Final final1, string ProjNum, Boolean check) //신재생 제외 에너지소요량 
        {

            final1.Calc_Qtot(ProjNum);
            if (check)
            { final1.reg_분배(ProjNum); }
            else { }
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            if (프로젝트유형[0][0] =="1" || 프로젝트유형[0][0] == "4")
            {
                final1.Calc_Qbase_elec(ProjNum);
                final1.Calc_Qbase_gas(ProjNum);
            }
            else
            {
                final1.reg_빼기(ProjNum);
                string[][] res = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기존프로젝트");
                if (res.Length > 0 && res[0][0] != "")
                {
                    for(int mth = 0; mth < 12; mth++)
                    {
                        string[][] Final2 = Program.DB.querySQL(res[0][0], "SELECT 기저에너지 FROM FinalEnergy_Result where 연료 = '전기' and 월 = '" + (mth + 1).ToString() + "월'");
                        if (Final2.Length > 0)
                        {
                            final1.Qbase_elec[mth] = Convert.ToDouble(Final2[0][0]);
                        }

                       Final2 = Program.DB.querySQL(res[0][0], "SELECT 기저에너지 FROM FinalEnergy_Result where not 연료 = '전기' and not 연료 ='전체' and 월 = '" + (mth + 1).ToString() + "월'");
                        if (Final2.Length > 0)
                        {
                            final1.Qbase_gas[mth] = Convert.ToDouble(Final2[0][0]);
                        }
                    }                    
                }
                for (int mth = 0; mth < 12; mth++)
                {
                    final1.Qf_elec_tot_mth[mth] = final1.Qf_elec_tot_mth[mth] + final1.Qbase_elec[mth] ;
                    final1.Qf_gas_tot_mth[mth] = final1.Qf_gas_tot_mth[mth] + final1.Qbase_gas[mth];
                }

            }
            
        }

        private static void Final_Save(Final final1)
        {
            #region 전기
            String MTH;
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            string[][] PNum = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
            for (int mth = 0; mth <= 11; mth++)
            {
                MTH = (mth + 1).ToString() + "월";
                Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result", "프로젝트번호,프로젝트유형,번호,월,연료," +
                    "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량",
                    "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + PNum[0][0] + "','" + MTH + "','" + "전기" + "','" +
                    final1.Qhf_elec[mth] + "','" + final1.Qcf_elec[mth] + "','" + final1.Qwf_elec[mth] + "','" + final1.Qlf_elec[mth] + "','" +
                    final1.Qvf_elec[mth] + "','" + final1.Qbase_elec[mth] + "','" + final1.Qreg_elec_tot[mth] + "','" + Math.Max( final1.Qf_elec_tot_mth[mth],0)
                    + "'", "번호,월,연료"); 

            }

            double Qhf_elec_a = 0, Qcf_elec_a = 0, Qwf_elec_a = 0, Qlf_elec_a = 0, Qvf_elec_a = 0, Qbase_elec_a = 0, Qreg_elec_a =0, Qf_elec_tot_a = 0;

            for (int mth = 0; mth < 12; mth++)
            {
                Qhf_elec_a += final1.Qhf_elec[mth];
                Qcf_elec_a += final1.Qcf_elec[mth];
                Qwf_elec_a += final1.Qwf_elec[mth];
                Qlf_elec_a += final1.Qlf_elec[mth];
                Qvf_elec_a += final1.Qvf_elec[mth];
                Qreg_elec_a += final1.Qreg_elec_tot[mth];
                Qbase_elec_a += final1.Qbase_elec[mth];
            }
            Qf_elec_tot_a = Qhf_elec_a + Qcf_elec_a + Qwf_elec_a + Qlf_elec_a + Qvf_elec_a + Qbase_elec_a - Qreg_elec_a;
            Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result", "프로젝트번호,프로젝트유형,번호,월,연료," +
                     "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량",
                     "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + PNum[0][0] + "','" + "연간" + "','" + "전기" + "','" +
                     Qhf_elec_a + "','" + Qcf_elec_a + "','" + Qwf_elec_a + "','" + Qlf_elec_a + "','" +
                     Qvf_elec_a + "','" + Qbase_elec_a + "','" + Qreg_elec_a + "','" + Qf_elec_tot_a
                     + "'", "번호,월,연료");
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
                    Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result", "프로젝트번호,프로젝트유형,번호,월,연료," +
                        "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량",
                        "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + PNum[0][0] + "','" + MTH + "','" + Carrier + "','" +
                        final1.Qhf_gas[mth] + "','" + final1.Qcf_gas[mth] + "','" + final1.Qwf_gas[mth] + "','" + final1.Qlf_gas[mth] + "','" +
                        final1.Qvf_gas[mth] + "','" + final1.Qbase_gas[mth] + "','" + "0" + "','" + final1.Qf_gas_tot_mth[mth]
                        + "'", "번호,월,연료"); 
                }
            }
            double Qhf_gas_a = 0, Qcf_gas_a = 0, Qwf_gas_a = 0, Qlf_gas_a = 0, Qvf_gas_a = 0, Qbase_gas_a = 0, Qf_gas_tot_a = 0;

            for (int mth = 0; mth < 12; mth++)
            {
                Qhf_gas_a += final1.Qhf_gas[mth];
                Qcf_gas_a += final1.Qcf_gas[mth];
                Qwf_gas_a += final1.Qwf_gas[mth];
                Qlf_gas_a += final1.Qlf_gas[mth];
                Qvf_gas_a += final1.Qvf_gas[mth];
                Qbase_gas_a += final1.Qbase_gas[mth];
            }
            Qf_gas_tot_a = Qhf_gas_a + Qcf_gas_a + Qwf_gas_a + Qbase_gas_a;
            Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result", "프로젝트번호,프로젝트유형,번호,월,연료," +
                     "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량",
                     "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + PNum[0][0] + "','" + "연간" + "','" + Carrier + "','" +
                     Qhf_gas_a + "','" + Qcf_gas_a + "','" + Qwf_gas_a + "','" + Qlf_gas_a + "','" +
                     Qvf_gas_a + "','" + Qbase_gas_a + "','" + "0" + "','" + Qf_gas_tot_a
                     + "'", "번호,월,연료");
            #endregion

            #region 전체
            for (int mth = 0; mth <= 11; mth++)
            {
                MTH = (mth + 1).ToString() + "월";
                Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result", "프로젝트번호,프로젝트유형,번호,월,연료," +
                    "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량",
                    "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + PNum[0][0] + "','" + MTH + "','" + "전체" + "','" +
                    (final1.Qhf_elec[mth]+ final1.Qhf_gas[mth]) + "','" + (final1.Qcf_elec[mth]+final1.Qcf_gas[mth]) + "','" + (final1.Qwf_elec[mth]+final1.Qwf_gas[mth]) + "','" + final1.Qlf_elec[mth] + "','" +
                    final1.Qvf_elec[mth] + "','" + (final1.Qbase_elec[mth]+final1.Qbase_gas[mth]) + "','" + final1.Qreg_elec_tot[mth]  + "','" + Math.Max((final1.Qf_elec_tot_mth[mth]+final1.Qf_gas_tot_mth[mth]),0)
                    + "'", "번호,월,연료"); 
            }

            Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result", "프로젝트번호,프로젝트유형,번호,월,연료," +
                   "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량",
                   "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + PNum[0][0] + "','" + "연간" + "','" + "전체" + "','" +
                   (Qhf_elec_a+Qhf_gas_a) + "','" + (Qcf_elec_a + Qcf_gas_a) + "','" + (Qwf_elec_a + Qwf_gas_a) + "','" + Qlf_elec_a + "','" +
                   Qvf_elec_a + "','" + (Qbase_elec_a + Qbase_gas_a) + "','" + Qreg_elec_a + "','" + Math.Max((Qf_elec_tot_a + Qf_gas_tot_a),0)
                   + "'", "번호,월,연료");
            #endregion

        }
        #endregion

        #region 신재생
        public static bool RESystemCalc(string ProjNum)
        {
            PVCalc(ProjNum);
            WPCalc(ProjNum);

            
            return true;
        }
        public static bool PVCalc(string ProjNum)
        {
            string[][] PVNum = Program.DB.getValue(ProjNum, "PV_Form", "번호");
            string[][] 프로젝트유형 = Program.DB.getValue(ProjNum, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            int i = -1;
            String MTH;
            while (++i < PVNum.Length)
            {
                Cal_RESystem PV = new Cal_RESystem(PVNum[i][0]);
                PV.PVcalReady();
                PV.PVcal();
                PV.PVsave(ProjNum);
            }
            return true;
        }
        public static bool WPCalc(string ProjNum)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(ProjNum, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            int i = -1;
            String MTH;
            string[][] WPNum = Program.DB.getValue(ProjNum, "WindPower_Form", "번호");
            while (++i < WPNum.Length)
            {
                Cal_RESystem WP = new Cal_RESystem(WPNum[i][0]);
                WP.WP_LoadData();
                WP.WP_Calc_V1();
                WP.WP_Calc_V2();
                WP.WP_Calc_t_wkn();
                WP.WP_Calc_Qfwps();

                for (int mth = 0; mth <= 11; mth++)
                {
                    MTH = (mth + 1).ToString() + "월";
                    Program.DB.setValue(DB.type.ProjDB, "WindPower_Result", "프로젝트번호,프로젝트유형,번호," +
                             "월," +
                             "Qfwps",
                             "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + WPNum[i][0] + "','" + MTH  + "','" + WP.Qfwps[mth]
                              + "'", "번호,월"); ;


                }
                Program.DB.saveProject();
                Save_Memory_WP(WPNum[i][0], WP.Qfwps);
            }

            return true;
        }
        public static void Save_Memory_WP(string WPNum, double[] WP_Q)
        {
            string RESystemNum = null;

            if (CALC.RESystems.Count == 0)
            {
                RESystemNum = "RE01";
            }


            ArrayList arr_renum = new ArrayList();
            int i = 0;
            foreach (var system in CALC.RESystems.Values)
            {
                if (!arr_renum.Contains(system.RE_Num))
                {
                    arr_renum.Add(system.RE_Num);
                    i++;
                }
            }

            RESystemNum = "RE0" + (i + 1);

            foreach (var system in CALC.RESystems.Values)
            {
                if (system != null && system.RESystem_Num() == WPNum)
                {
                    RESystemNum = system.Num();
                    break; // 찾았으면 더 이상 반복하지 않음
                }
            }

            RESystem news = new RESystem(RESystemNum, "생산", "전기", "");
            news.RE_Production_Consumption = "생산";
            news.RE_Production_Type = "전기";
            news.RE_RESystem_Num = WPNum;
            news.RE_RESystem_Type = "풍력시스템";
            news.RE_TotalE = WP_Q;
            string[] sy = new string[4];
            sy[0] = news.Num();
            sy[1] = "생산";
            sy[2] = "전기";
            sy[3] = "";
            if (news.Num() != "")
            {
                CALC.RESystems[sy] = news;
            }
        }
        public static void Save_RESystem(string ProjNum)
        {

            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            foreach (var s in RESystems.Values)
            {
                double[] tot, h, c, w, l, v = new double[12];
                tot = s.TotalE();
                h = s.HeatingE();
                c = s.CoolingE();
                w = s.DHWE();
                l = s.LightingE();
                v = s.AHUE();

                for (int mth = 0; mth < 12; mth++)
                {
                    Program.DB.setValue(DB.type.ProjDB, "RESystem_Result", "프로젝트번호,프로젝트유형,번호,신재생시스템,신재생시스템유형,생산소비,생산유형,소비연료,월,난방설비,냉방설비,급탕설비,총에너지,난방,냉방,급탕,조명,공조",
                "'" + ProjNum + "','" + 프로젝트유형[0][0] + "','" + s.Num() + "','" + s.RESystem_Num() + "','" + s.RESystem_Type() + "','" +
                s.Production_Consumption() + "','" + s.Production_Type() + "','" + s.Consumption_Carrier() + "','" +
                (mth + 1) + "월" + "','" + s.Heating_Num() + "','" + s.Cooling_Num() + "','" + s.DHW_Num() + "','" +
               tot[mth].ToString() + "','" + h[mth].ToString() + "','" + c[mth].ToString() + "','" + w[mth].ToString() + "','" + l[mth].ToString() + "','" + v[mth].ToString()
                + "'", "번호,신재생시스템,신재생시스템유형,생산소비,생산유형,소비연료,월,난방설비,냉방설비,급탕설비");
                }
            }
               
        }
        #endregion



        public static bool AltCalc()
        {
            ElementCalc();
            RuleCalc();
            return true;
        }
        public static bool ElementCalc()
        {
            Cal_Alt cal = new Cal_Alt();
            Program.DB.deleteTable(DB.type.ProjDB, "FinalEnergy_Result_Element");
            Program.DB.initTable(DB.type.ProjDB, "FinalEnergy_Result_Element");
            Program.DB.deleteTable(DB.type.ProjDB, "Zone_Alt_Result");
            Program.DB.initTable(DB.type.ProjDB, "Zone_Alt_Result");

            Program.DB.deleteTable(DB.type.ProjDB, "Heating_ce_Form_Element");
            Program.DB.initTable(DB.type.ProjDB, "Heating_ce_Form_Element");
            Program.DB.deleteTable(DB.type.ProjDB, "Heating_Result_Element");
            Program.DB.initTable(DB.type.ProjDB, "Heating_Result_Element");

            Program.DB.deleteTable(DB.type.ProjDB, "Cooling_ce_Form_Element");
            Program.DB.initTable(DB.type.ProjDB, "Cooling_ce_Form_Element");
            Program.DB.deleteTable(DB.type.ProjDB, "Cooling_Result_Element");
            Program.DB.initTable(DB.type.ProjDB, "Cooling_Result_Element");
            Program.DB.deleteTable(DB.type.ProjDB, "DHWSystem_Result_Element");
            Program.DB.initTable(DB.type.ProjDB, "DHWSystem_Result_Element");
            string[][] Type = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호", "");
            if (Type.Length > 0)
            {
                if (Type[0][0] != "1" && Type[0][0] != "4")
                {
                    for (int i = 0; i < ElementAlt.Length; i++)
                    {
                        RESystems.Clear();
                        cal.Calc_Element(ElementAlt[i]);
                    }
                }
            }
            return true;
        }
        public static bool RuleCalc()
        {
            Cal_Rule cal = new Cal_Rule();
            Program.DB.deleteTable(DB.type.ProjDB, "FinalEnergy_Result_Rule");
            Program.DB.initTable(DB.type.ProjDB, "FinalEnergy_Result_Rule");
            for (int i = 0; i < RuleAlt.Length; i++)
            {
                RESystems.Clear();
                cal.Calc_Rule(RuleAlt[i]);
            }
            return true;
        }
        /////////////////////////////////////////////////////////////////////////////////////

        public static Dictionary<string, Delegate> _calculations = new Dictionary<string, Delegate>();
        public static Dictionary<string, Zone> Zones = new Dictionary<string, Zone>();
        public static Dictionary<string, ZoneLight> ZoneLights = new Dictionary<string, ZoneLight>();
        public static Dictionary<string, Heating> Heatings = new Dictionary<string, Heating>();
        public static Dictionary<string, Cal_Cooling> Coolings = new Dictionary<string,Cal_Cooling >();
        public static Dictionary<string, AHU> AHUs = new Dictionary<string, AHU>();
        public static Dictionary<string, DHW> DHWs = new Dictionary<string, DHW>();
        public static Dictionary<string, Final> Finals = new Dictionary<string, Final>();
        public static Dictionary<string[], RESystem> RESystems = new Dictionary<string[], RESystem>();
        public static string[] ElementAlt = { "조닝", "외벽", "지붕", "최하층바닥", "창호", "커튼월창", "외부출입문", "기밀+열회수기", "난방", "냉방", "급탕", "조명", "공조", "태양광","풍력", "기밀" }; //기밀은 요소기술별 합계 계산 시 제외되어야 하므로 마지막 순서여야 함 
      //  public static string[] RuleAlt = { "기밀", "기밀+열회수기" };
        public static string[] RuleAlt = { "외벽", "지붕", "최하층바닥", "창호", "커튼월창", "외부출입문", "기밀", "기밀+열회수기", "조명", "보일러", "냉난방EHP", "냉방EHP", "공냉식냉동기", "수냉식냉동기", "냉난방GHP", "흡수식냉온수기", "태양광" };
        public Zone getZone(string zoneNum)
        {
            if (Zones.ContainsKey(zoneNum))
            {
                return Zones[zoneNum];
            }
            else return null;
        }
        public ZoneLight getZoneLight(string zoneNum)
        {
            if (ZoneLights.ContainsKey(zoneNum))
            {
                return ZoneLights[zoneNum];
            }
            else return null;
        }
        public Heating getHeating(string HeatingNum)
        {
            if (Heatings.ContainsKey(HeatingNum))
            {
                return Heatings[HeatingNum];
            }
            else return null;
        }
        public Cal_Cooling getCooling(string CoolingNum)
        {
            if (Coolings.ContainsKey(CoolingNum))
            {
                return Coolings[CoolingNum];
            }
            else return null;
        }
        public AHU getAHU(string AHUNum)
        {
            if (AHUs.ContainsKey(AHUNum))
            {
                return AHUs[AHUNum];
            }
            else return null;
        }
        public DHW getDHW(string DHWNum)
        {
            if (DHWs.ContainsKey(DHWNum))
            {
                return DHWs[DHWNum];
            }
            else return null;
        }
        public Final getFinal(string Num)
        {
            if (Finals.ContainsKey(Num))
            {
                return Finals[Num];
            }
            else return null;
        }
        public RESystem getRESystem(string[] keys)
        {
            if (RESystems.ContainsKey(keys))
            {
                return RESystems[keys];
            }
            else return null;
        }

        public static bool run(string[] calculations)
        {
            foreach (string calc in calculations)
            {
                _calculations[calc].DynamicInvoke();
            }

            return true;
        }
    }
}
