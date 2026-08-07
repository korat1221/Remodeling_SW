using System.Collections;

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

        public void Save_Alt(Final final1, string 검토유형)
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
                Qreg_elec_a += final1.Qreg_elec_tot[mth];
            }
            Qf_elec_tot_a = Qhf_elec_a + Qcf_elec_a + Qwf_elec_a + Qlf_elec_a + Qvf_elec_a + Qbase_elec_a - Qreg_elec_a;
            Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result_Element", "프로젝트번호,프로젝트유형,검토유형,번호,월,연료," +
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
            Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result_Element", "프로젝트번호,프로젝트유형,검토유형,번호,월,연료," +
                    "난방,냉방,급탕,조명,공조,기저에너지,총에너지소요량",
                    "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + 검토유형 + "','" + PNum[0][0] + "','" + "연간" + "','" + Carrier + "','" +
                    Qhf_gas_a + "','" + Qcf_gas_a + "','" + Qwf_gas_a + "','" + "0" + "','" +
                    "0" + "','" + Qbase_gas_a + "','" + Qf_gas_tot_a
                    + "'", "검토유형,번호,월,연료");
            #endregion
            #region 전체           

            Program.DB.setValue(DB.type.ProjDB, "FinalEnergy_Result_Element", "프로젝트번호,프로젝트유형,검토유형,번호,월,연료," +
                   "난방,냉방,급탕,조명,공조,기저에너지,신재생에너지,총에너지소요량",
                   "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + 검토유형 + "','" + PNum[0][0] + "','" + "연간" + "','" + "전체" + "','" +
                   (Qhf_elec_a + Qhf_gas_a) + "','" + (Qcf_elec_a + Qcf_gas_a) + "','" + (Qwf_elec_a + Qwf_gas_a) + "','" + Qlf_elec_a + "','" +
                   Qvf_elec_a + "','" + (Qbase_elec_a + Qbase_gas_a) + "','" + Qreg_elec_a + "','" + (Qf_elec_tot_a + Qf_gas_tot_a)
                   + "'", "검토유형,번호,월,연료");
            #endregion

            Program.DB.saveProject();
        }


        public void Calc_Element(string 검토유형)
        {
            Calc_Qb_Element(검토유형);
            Calc_System_element(검토유형);
        }

        public void Calc_Qb_Element(string 검토유형)
        {
            CALC.Zone_Init();

            // 웜업 패스: PreElement 로드 + 검토유형별 재로드까지 끝낸 뒤 Zone_bztu()로 b_ztu 등
            // 자기완결적 값을 확정 — 재로드로 바뀐 값까지 반영해야 다른 존이 읽는 b_ztu가 본계산과 일치함
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
                    case "기밀":
                        zone1.LoadData_q50();
                        break;
                    case "기밀+열회수기":
                        zone1.LoadData_q50();
                        zone1.LoadData_Ventil();
                        break;
                    case "열교":
                        zone1.LoadData_dUtb_2D();
                        break;
                    case "조명":
                        zonelight1.LoadData_LightSystem();
                        break;
                }
                zone1.Zone_bztu();
                CALC.Zone_Warmup_Gain(zone1, zonelight1);
            }

            for (int k = 0; k < CALC.zone.Count; k++)
            {
                Zone zone1 = (Zone)CALC.zone[k];
                ZoneLight zonelight1 = (ZoneLight)CALC.zonelight[k];
                CALC.Zone_Calc(zone1, zonelight1); // 로드+재로드+일사·내부발열 자기몫은 이미 웜업에서 끝남
                if (검토유형 == "조닝" || 검토유형 == "조명") { Save_Qlf_Element(zonelight1, 검토유형); }
            }
        }
        public void Zone_LoadData_PreElement(Zone zone1, ZoneLight zonelight1)
        {
            string[][] 증축 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "증축여부", "존번호 = '" + zone1.ZoneNum + "'");
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
            for (int i = 0; i < split_Zone.Count; i++)
            {
                string[][] ValueA = Program.DB.getValue(PreProjNum[0][0], "ZoneLighting_form", "조명밀도,조명예상전력,재실계수,조도제어계수,광효율,대기전력,조명개수", "번호='" + split_Zone[i] + "'");
                if (ValueA.Length > 0)
                {
                    zonelight1.Pj = Program.UTIL.ToDoubleOrZero(ValueA[0][0]);
                    zonelight1.Pn = Program.UTIL.ToDoubleOrZero(ValueA[0][1]);
                    zonelight1.Fo = Program.UTIL.ToDoubleOrZero(ValueA[0][2]);
                    zonelight1.Fc = Program.UTIL.ToDoubleOrZero(ValueA[0][3]);
                    zonelight1.lm_W = Program.UTIL.ToDoubleOrZero(ValueA[0][4]);
                    zonelight1.wsp = Program.UTIL.ToDoubleOrZero(ValueA[0][5]);
                    zonelight1.N = Program.UTIL.ToDoubleOrZero(ValueA[0][6]);
                }
            }

        }
        private void Load_Pre_q50(Zone zone1)
        {
            string[][] Value2 = Program.DB.getValue(PreProjNum[0][0], "BuildingGeneral", "기밀측정여부,출입문q50,창호q50,외벽q50,지붕q50", "");
            if (Value2.Length > 0)
            {
                zone1.Door_q50 = Program.UTIL.ToDoubleOrZero(Value2[0][1]);
                zone1.Win_q50 = Program.UTIL.ToDoubleOrZero(Value2[0][2]);
                zone1.Wall_q50 = Program.UTIL.ToDoubleOrZero(Value2[0][3]);
                zone1.Roof_q50 = Program.UTIL.ToDoubleOrZero(Value2[0][4]);
            }
        }
        private void Load_Pre_Ventil(Zone zone1)
        {//존 환기정보 가져오기 
            ArrayList split_Zone = new ArrayList(); string[][] Zone_Pre = null;
            string matchedPreZone = null; // goto 이후에도 "몇 번째 이전존이 매칭됐는지" 알 수 있도록 별도 저장
            string[][] Zone_Post = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_form", "비이용일환기량,이용일환기량,기존존", "존번호='" + zone1.ZoneNum + "'");
            if (Zone_Post.Length > 0)
            {
                if (Zone_Post[0][2] != "")
                { split_Zone = Split_(Zone_Post[0][2]); }
            }
            for (int i = 0; i < split_Zone.Count; i++)
            {
                Zone_Pre = Program.DB.getValue(PreProjNum[0][0], "ZoneGeneral_form", "환기유무,환기방식,비이용일환기량,이용일환기량,선택열회수기", "존번호='" + split_Zone[i] + "'");
                if (Zone_Pre.Length > 0)
                {
                    if (Zone_Pre[0][0] == "True")
                    {
                        matchedPreZone = split_Zone[i].ToString();
                        goto load_ventil;
                    }
                }
            }

        load_ventil:
            if (Zone_Pre == null)
            {

            }
            else
            {

                if (Convert.ToBoolean(Zone_Pre[0][0]))
                {
                    if (Zone_Pre[0][1] == "열회수기")
                    {
                        zone1.Vmech_SUP = Program.UTIL.ToDoubleOrZero(Zone_Pre[0][3]);
                        zone1.Vmech_ETA = Program.UTIL.ToDoubleOrZero(Zone_Pre[0][3]);
                        zone1.SelectHRV = Zone_Pre[0][4];
                        string[][] value = Program.DB.getValue(PreProjNum[0][0], "User_HRV", "온도교환효율_난방,온도교환효율_냉방,습도교환효율_난방,습도교환효율_냉방", "번호='" + zone1.SelectHRV + "'");
                        if (value.Length > 0)
                        {
                            zone1.eta_V_mech[0] = Program.UTIL.ToDoubleOrZero(value[0][0]) / 100;
                            zone1.eta_V_mech[1] = Program.UTIL.ToDoubleOrZero(value[0][1]) / 100;
                            zone1.eta_χV_mech[0] = Program.UTIL.ToDoubleOrZero(value[0][2]) / 100;
                            zone1.eta_χV_mech[1] = Program.UTIL.ToDoubleOrZero(value[0][3]) / 100;
                        }

                    }
                    else if (Zone_Pre[0][1] == "공조기")
                    {
                        zone1.Vmech_SUP = Program.UTIL.ToDoubleOrZero(Zone_Pre[0][3]);
                        zone1.Vmech_ETA = Program.UTIL.ToDoubleOrZero(Zone_Pre[0][3]);
                        zone1.SelectHRV = Zone_Pre[0][4];
                        string[][] value = Program.DB.getValue(PreProjNum[0][0], "User_AHU", "온도교환효율_난방,온도교환효율_냉방,습도교환효율_난방,습도교환효율_냉방", "번호='" + zone1.SelectHRV + "'");
                        if (value.Length > 0)
                        {
                            zone1.eta_V_mech[0] = Program.UTIL.ToDoubleOrZero(value[0][0]) / 100;
                            zone1.eta_V_mech[1] = Program.UTIL.ToDoubleOrZero(value[0][1]) / 100;
                            zone1.eta_χV_mech[0] = Program.UTIL.ToDoubleOrZero(value[0][2]) / 100;
                            zone1.eta_χV_mech[1] = Program.UTIL.ToDoubleOrZero(value[0][3]) / 100;
                        }
                    }
                    else if (Zone_Pre[0][1] == "배기환기(3종)")
                    {
                        zone1.Vmech_SUP = 0; //배기환기(3종)는 급기 자체가 없음, Cal_HCneed.cs와 동일
                        zone1.Vmech_ETA = Program.UTIL.ToDoubleOrZero(Zone_Pre[0][3]); //이용일환기량 기준(Cal_HCneed.cs와 동일)
                    }
                    else
                    {
                        // 환기방식 UI 콤보박스엔 열회수기/공조기/배기환기(3종) 세 개뿐이라 이 분기는
                        // 정상 경로로는 도달 불가능(레거시 데이터 등 예외 상황) — 환기 없음으로 안전 처리
                        zone1.Vmech_SUP = 0;
                        zone1.Vmech_ETA = 0;
                    }
                }
                else
                {
                    zone1.Vmech_SUP = 0;
                    zone1.Vmech_ETA = 0;
                }
                // 이전 프로젝트(PreProjNum)의 AHUZoneVent_Form에서 실측치 그대로 가져옴 — LoadData_Ventil()과 동일 방식.
                string[][] OutgoingZV = Program.DB.getValue(PreProjNum[0][0], "AHUZoneVent_Form", "인접존배기량", "존 = '" + matchedPreZone + "' And 인접존 <> ''");
                double outgoingZ = 0;
                for (int i = 0; i < OutgoingZV.Length; i++)
                {
                    double.TryParse(OutgoingZV[i][0], out double v);
                    outgoingZ += v;
                }
                string[][] IncomingZV = Program.DB.getValue(PreProjNum[0][0], "AHUZoneVent_Form", "인접존배기량", "인접존 = '" + matchedPreZone + "'");
                double incomingZ = 0;
                for (int i = 0; i < IncomingZV.Length; i++)
                {
                    double.TryParse(IncomingZV[i][0], out double v);
                    incomingZ += v;
                }
                zone1.V_SUP_z = incomingZ;
                zone1.V_ETA_z = outgoingZ;
                zone1.ρacp_a = 0.34;

            }
        }
        private void Load_Pre_Wall(Zone zone1)
        {
            zone1.zoneWall.Clear();
            zone1.zoneGWall.Clear();
            String[][] ZoneW = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.유효열관류율,b.흡수율,b.직접간접,a.방위,a.기울기,b.Type,b.기존외벽,b.명칭 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "' And  NOT b.직접간접 = '지면'");
            if (ZoneW.Length > 0)
            {
                int i = -1;
                while (++i < ZoneW.Length)
                {
                    double Uvalue = 0;

                    if (ZoneW[i][8] == "신규")
                    {
                        String[][] Date = Program.DB.getValue(PreProjNum[0][0], "BuildingGeneral", "법규시기,지역구분", "");
                        String[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율", "구조체 = '외벽' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND 직접간접 =  '" + ZoneW[i][5] + "'");
                        if (Value.Length > 0)
                        { Uvalue = Program.UTIL.ToDoubleOrZero(Value[0][0]); }
                    }
                    else if (ZoneW[i][8] == "기존외벽")
                    {
                        String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionWall", "유효열관류율", "명칭 ='" + ZoneW[i][10] + "'");
                        if (Pre.Length > 0)
                        { Uvalue = Program.UTIL.ToDoubleOrZero(Pre[0][0]); }
                    }
                    else
                    {
                        String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionWall", "유효열관류율", "명칭 ='" + ZoneW[i][9] + "'");
                        if (Pre.Length > 0)
                        { Uvalue = Program.UTIL.ToDoubleOrZero(Pre[0][0]); }
                    }

                    Wall wall = new Wall(ZoneW[i][0], ZoneW[i][2], Program.UTIL.ToDoubleOrZero(ZoneW[i][1]), Uvalue, Program.UTIL.ToDoubleOrZero(ZoneW[i][4]), ZoneW[i][5], ZoneW[i][6], ZoneW[i][7]);
                    zone1.zoneWall.Add(wall);
                }
            }
            String[][] ZoneG = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.유효열관류율,b.직접간접,b.Type,b.기존외벽,b.명칭 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "' And  b.직접간접 = '지면'");
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
                        { Uvalue = Program.UTIL.ToDoubleOrZero(Value[0][0]); }
                    }
                    else if (ZoneG[i][5] == "기존외벽")
                    {
                        String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionWall", "유효열관류율", "명칭 ='" + ZoneG[i][7] + "'");
                        if (Pre.Length > 0)
                        { Uvalue = Program.UTIL.ToDoubleOrZero(Pre[0][0]); }
                    }
                    else
                    {
                        String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionWall", "유효열관류율", "명칭 ='" + ZoneG[i][6] + "'");
                        if (Pre.Length > 0)
                        { Uvalue = Program.UTIL.ToDoubleOrZero(Pre[0][0]); }
                    }
                    double fx_f = 0.8;
                    if (Program.UTIL.ToDoubleOrZero(Uvalue) >= 3)
                    { fx_f = 0.35; }
                    else if (Program.UTIL.ToDoubleOrZero(Uvalue) >= 1)
                    { fx_f = 0.55; }
                    else if (Program.UTIL.ToDoubleOrZero(Uvalue) > 0.3)
                    { fx_f = 0.65; }
                    else { fx_f = 0.75; }

                    GWall gwall = new GWall(ZoneG[i][0], ZoneG[i][2], Program.UTIL.ToDoubleOrZero(ZoneG[i][1]), Uvalue, fx_f);
                    zone1.zoneGWall.Add(gwall);
                }
            }
        }
        private void Load_Pre_Roof(Zone zone1)
        {
            zone1.zoneRoof.Clear();
            String[][] ZoneR = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.유효열관류율,b.흡수율,b.직접간접,a.방위,a.기울기,b.Type,b.기존지붕,b.명칭 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionRoof AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
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
                        { Uvalue = Program.UTIL.ToDoubleOrZero(Value[0][0]); }
                    }
                    else if (ZoneR[i][8] == "기존지붕")
                    {
                        String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionRoof", "유효열관류율", "명칭 ='" + ZoneR[i][10] + "'");
                        if (Pre.Length > 0)
                        { Uvalue = Program.UTIL.ToDoubleOrZero(Pre[0][0]); }
                    }
                    else
                    {
                        String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionRoof", "유효열관류율", "명칭 ='" + ZoneR[i][9] + "'");
                        if (Pre.Length > 0)
                        { Uvalue = Program.UTIL.ToDoubleOrZero(Pre[0][0]); }
                    }
                    Roof roof = new Roof(ZoneR[i][0], ZoneR[i][2], Program.UTIL.ToDoubleOrZero(ZoneR[i][1]), Uvalue, Program.UTIL.ToDoubleOrZero(ZoneR[i][4]), ZoneR[i][5], ZoneR[i][6], ZoneR[i][7]);
                    zone1.zoneRoof.Add(roof);
                }
            }
        }
        private void Load_Pre_Floor(Zone zone1)
        {
            zone1.zoneFloor.Clear();
            String[][] ZoneF = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.유효열관류율,b.직접간접,b.기초설치,b.Type,b.기존바닥,b.명칭 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionFloor AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
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
                        { Uvalue = Program.UTIL.ToDoubleOrZero(Value[0][0]); }
                    }
                    else if (ZoneF[i][6] == "기존바닥")
                    {
                        String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionFloor", "유효열관류율", "명칭 ='" + ZoneF[i][8] + "'");
                        if (Pre.Length > 0)
                        { Uvalue = Program.UTIL.ToDoubleOrZero(Pre[0][0]); }
                    }
                    else
                    {
                        String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionFloor", "유효열관류율", "명칭 ='" + ZoneF[i][7] + "'");
                        if (Pre.Length > 0)
                        { Uvalue = Program.UTIL.ToDoubleOrZero(Pre[0][0]); }
                    }
                    double fx_f = 0.8;
                    switch (ZoneF[i][5].ToString())
                    {
                        case "지면위":
                            {
                                if (Program.UTIL.ToDoubleOrZero(Uvalue) >= 3)
                                { fx_f = 0.3; }
                                else if (Program.UTIL.ToDoubleOrZero(Uvalue) >= 1)
                                { fx_f = 0.55; }
                                else if (Program.UTIL.ToDoubleOrZero(Uvalue) > 0.3)
                                { fx_f = 0.7; }
                                else { fx_f = 0.8; }
                                break;
                            }
                        case "단열지하":
                            {
                                if (Program.UTIL.ToDoubleOrZero(Uvalue) >= 3)
                                { fx_f = 0.2; }
                                else if (Program.UTIL.ToDoubleOrZero(Uvalue) >= 1)
                                { fx_f = 0.45; }
                                else if (Program.UTIL.ToDoubleOrZero(Uvalue) > 0.3)
                                { fx_f = 0.55; }
                                else { fx_f = 0.7; }
                                break;
                            }
                        case "비단열지하":
                            {
                                if (Program.UTIL.ToDoubleOrZero(Uvalue) >= 3)
                                { fx_f = 0.45; }
                                else if (Program.UTIL.ToDoubleOrZero(Uvalue) >= 1)
                                { fx_f = 0.75; }
                                else if (Program.UTIL.ToDoubleOrZero(Uvalue) > 0.3)
                                { fx_f = 0.8; }
                                else { fx_f = 0.85; }
                                break;
                            }
                    }


                    Floor floor = new Floor(ZoneF[i][0], ZoneF[i][2], Program.UTIL.ToDoubleOrZero(ZoneF[i][1]), Uvalue, ZoneF[i][5], fx_f);
                    zone1.zoneFloor.Add(floor);
                }
            }
        }
        private void Load_Pre_Win(Zone zone1)
        {
            zone1.zoneWin.Clear();
            String[][] ZoneWin = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.창호열관류율,b.설치열교가산치,b.창호유효열관류율,b.유리면적비,b.상위창호번호,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
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
                            { Uvalue = Program.UTIL.ToDoubleOrZero(Value[0][0]); }
                            dU = Program.UTIL.ToDoubleOrZero(ZoneWin[i][4]);
                        }
                        else if (ZoneWin_P[0][3] == "기존창호")
                        {
                            String[][] Pre = Program.DB.querySQL(PreProjNum[0][0], "select avg(a.창호열관류율), avg(a.설치열교가산치) From SubWindow as a inner join ConstructionWindow as b on a.상위창호번호 = b.번호 where b.창호명칭 ='" + ZoneWin_P[0][5] + "'");
                            if (Pre.Length > 0)
                            { Uvalue = Program.UTIL.ToDoubleOrZero(Pre[0][0]); }
                        }
                        else
                        {
                            String[][] Pre = Program.DB.querySQL(PreProjNum[0][0], "select avg(a.창호열관류율), avg(a.설치열교가산치) From SubWindow as a inner join ConstructionWindow as b on a.상위창호번호 = b.번호 where b.창호명칭 ='" + ZoneWin_P[0][4] + "'");
                            if (Pre.Length > 0)
                            { Uvalue = Program.UTIL.ToDoubleOrZero(Pre[0][0]); }
                        }

                        if (Blind.Length > 0)
                        {
                            Window win = new Window(ZoneWin[i][0], ZoneWin[i][7], ZoneWin[i][2], Program.UTIL.ToDoubleOrZero(ZoneWin[i][1]), Uvalue, dU, ZoneWin_P[0][0], Program.UTIL.ToDoubleOrZero(ZoneWin[i][6]), Program.UTIL.ToDoubleOrZero(ZoneWin_P[0][1]), Program.UTIL.ToDoubleOrZero(ZoneWin_P[0][2]), Program.UTIL.ToDoubleOrZero(Blind[0][0]), Program.UTIL.ToDoubleOrZero(Blind[0][1]), ZoneWin[i][8], ZoneWin[i][9]);
                            zone1.zoneWin.Add(win);
                        }
                        else
                        {
                            Window win = new Window(ZoneWin[i][0], ZoneWin[i][7], ZoneWin[i][2], Program.UTIL.ToDoubleOrZero(ZoneWin[i][1]), Uvalue, dU, ZoneWin_P[0][0], Program.UTIL.ToDoubleOrZero(ZoneWin[i][6]), Program.UTIL.ToDoubleOrZero(ZoneWin_P[0][1]), Program.UTIL.ToDoubleOrZero(ZoneWin_P[0][2]), 0, 0, ZoneWin[i][8], ZoneWin[i][9]);
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
                                { Uvalue = Program.UTIL.ToDoubleOrZero(Value[0][0]); }
                                dU = Program.UTIL.ToDoubleOrZero(CW_g[0][4]);
                            }
                            else if (CW_g[0][5] == "기존 커튼월창")
                            {
                                String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionCW", "유리부분열관류율,설치열교가산치", "명칭 ='" + CW_g[0][7] + "'");
                                if (Pre.Length > 0)
                                {
                                    Uvalue = Program.UTIL.ToDoubleOrZero(Pre[0][0]);
                                    dU = Program.UTIL.ToDoubleOrZero(Pre[0][1]);
                                }
                            }
                            else
                            {
                                String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionCW", "유리부분열관류율,설치열교가산치", "명칭 ='" + CW_g[0][6] + "'");
                                if (Pre.Length > 0)
                                {
                                    if (Pre[0][0] == "" || Pre[0][0] == null || double.IsNaN(Program.UTIL.ToDoubleOrZero(Pre[0][0])))
                                    {
                                        String[][] Date = Program.DB.getValue(PreProjNum[0][0], "BuildingGeneral", "법규시기,지역구분", "");
                                        String[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '창호' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND  직접간접 =  '직접외기'");
                                        if (Value.Length > 0)
                                        { Uvalue = Program.UTIL.ToDoubleOrZero(Value[0][0]); }
                                        dU = Program.UTIL.ToDoubleOrZero(CW_g[0][4]);
                                    }
                                    else
                                    {
                                        Uvalue = Program.UTIL.ToDoubleOrZero(Pre[0][0]);
                                        dU = Program.UTIL.ToDoubleOrZero(Pre[0][1]);
                                    }
                                }
                            }

                            if (Blind.Length > 0)

                            {
                                CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], Program.UTIL.ToDoubleOrZero(ZoneCW[i][1]), Uvalue, Program.UTIL.ToDoubleOrZero(CW_g[0][1]), Program.UTIL.ToDoubleOrZero(CW_g[0][2]), Program.UTIL.ToDoubleOrZero(Blind[0][0]), Program.UTIL.ToDoubleOrZero(CW_g[0][3]), Program.UTIL.ToDoubleOrZero(Blind[0][1]), 0, 0, 0, 0, 0, 0, 0, 0, Program.UTIL.ToDoubleOrZero(ZoneCW[i][1]), dU, ZoneCW[i][4], ZoneCW[i][5], "유리부분");
                                zone1.zoneCW.Add(cw);
                            }
                            else
                            {
                                CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], Program.UTIL.ToDoubleOrZero(ZoneCW[i][1]), Uvalue, Program.UTIL.ToDoubleOrZero(CW_g[0][1]), Program.UTIL.ToDoubleOrZero(CW_g[0][2]), 0, Program.UTIL.ToDoubleOrZero(CW_g[0][3]), 0, 0, 0, 0, 0, 0, 0, 0, 0, Program.UTIL.ToDoubleOrZero(ZoneCW[i][1]), dU, ZoneCW[i][4], ZoneCW[i][5], "유리부분");
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
                                { Uvalue = Program.UTIL.ToDoubleOrZero(Value[0][0]); }
                                dU = Program.UTIL.ToDoubleOrZero(CW_p[0][2]);
                            }
                            else if (CW_p[0][3] == "기존 커튼월창")
                            {
                                String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionCW", "패널부분열관류율,설치열교가산치", "명칭 ='" + CW_p[0][5] + "'");
                                if (Pre.Length > 0)
                                {
                                    Uvalue = Program.UTIL.ToDoubleOrZero(Pre[0][0]);
                                    dU = Program.UTIL.ToDoubleOrZero(Pre[0][1]);
                                }
                            }
                            else
                            {
                                String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionCW", "패널부분열관류율,설치열교가산치", "명칭 ='" + CW_p[0][4] + "'");
                                if (Pre.Length > 0)
                                {
                                    if (Pre[0][0] == "" || Pre[0][0] == null || double.IsNaN(Program.UTIL.ToDoubleOrZero(Pre[0][0])))
                                    {
                                        String[][] Date = Program.DB.getValue(PreProjNum[0][0], "BuildingGeneral", "법규시기,지역구분", "");
                                        String[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '외벽' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND  직접간접 =  '직접외기'");
                                        if (Value.Length > 0)
                                        { Uvalue = Program.UTIL.ToDoubleOrZero(Value[0][0]); }
                                        dU = Program.UTIL.ToDoubleOrZero(CW_p[0][2]);
                                    }
                                    else
                                    {
                                        Uvalue = Program.UTIL.ToDoubleOrZero(Pre[0][0]);
                                        dU = Program.UTIL.ToDoubleOrZero(Pre[0][1]);
                                    }
                                }
                            }

                            CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], 0, 0, 0, 0, 0, 0, 0, Program.UTIL.ToDoubleOrZero(ZoneCW[i][1]), Uvalue, Program.UTIL.ToDoubleOrZero(CW_p[0][1]), 0, 0, 0, 0, 0, Program.UTIL.ToDoubleOrZero(ZoneCW[i][1]), dU, ZoneCW[i][4], ZoneCW[i][5], "패널부분");
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
                                { Uvalue = Program.UTIL.ToDoubleOrZero(Value[0][0]); }
                                dU = Program.UTIL.ToDoubleOrZero(CW_d[0][4]);
                            }
                            else if (CW_d[0][5] == "기존 커튼월창")
                            {
                                String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionCW", "출입문부분열관류율,설치열교가산치", "명칭 ='" + CW_d[0][7] + "'");
                                if (Pre.Length > 0)
                                {
                                    Uvalue = Program.UTIL.ToDoubleOrZero(Pre[0][0]);
                                    dU = Program.UTIL.ToDoubleOrZero(Pre[0][1]);
                                }
                            }
                            else
                            {
                                String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionCW", "출입문부분열관류율,설치열교가산치", "명칭 ='" + CW_d[0][6] + "'");
                                if (Pre.Length > 0)
                                {
                                    if (Pre[0][0] == "" || Pre[0][0] == null || double.IsNaN(Program.UTIL.ToDoubleOrZero(Pre[0][0])))
                                    {
                                        String[][] Date = Program.DB.getValue(PreProjNum[0][0], "BuildingGeneral", "법규시기,지역구분", "");
                                        String[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '창호' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND  직접간접 =  '직접외기'");
                                        if (Value.Length > 0)
                                        { Uvalue = Program.UTIL.ToDoubleOrZero(Value[0][0]); }
                                        dU = Program.UTIL.ToDoubleOrZero(CW_d[0][4]);
                                    }
                                    else
                                    {
                                        Uvalue = Program.UTIL.ToDoubleOrZero(Pre[0][0]);
                                        dU = Program.UTIL.ToDoubleOrZero(Pre[0][1]);
                                    }
                                }
                            }

                            CW cw = new CW(ZoneCW[i][0], ZoneCW[i][3], 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, Program.UTIL.ToDoubleOrZero(ZoneCW[i][1]), Uvalue, Program.UTIL.ToDoubleOrZero(CW_d[0][1]), Program.UTIL.ToDoubleOrZero(CW_d[0][2]), Program.UTIL.ToDoubleOrZero(CW_d[0][3]), Program.UTIL.ToDoubleOrZero(ZoneCW[i][1]), dU, ZoneCW[i][4], ZoneCW[i][5], "출입문부분");
                            zone1.zoneCW.Add(cw);
                        }
                    }
                }
            }
        }
        private void Load_Pre_Door(Zone zone1)
        {
            zone1.zoneDoor.Clear();
            String[][] ZoneD = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.문유효열관류율,b.흡수율,b.직접간접,a.방위,a.기울기,b.Type,b.기존출입문,b.명칭 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionDoor AS b ON a.구조체번호 = b.번호 where a.존 = '" + zone1.ZoneNum + "'");
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
                        { Uvalue = Program.UTIL.ToDoubleOrZero(Value[0][0]); }
                    }
                    else if (ZoneD[i][8] == "기존 출입문")
                    {
                        String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionDoor", "문유효열관류율", "명칭 ='" + ZoneD[i][10] + "'");
                        if (Pre.Length > 0)
                        { Uvalue = Program.UTIL.ToDoubleOrZero(Pre[0][0]); }
                    }
                    else
                    {
                        String[][] Pre = Program.DB.getValue(PreProjNum[0][0], "ConstructionDoor", "문유효열관류율", "명칭 ='" + ZoneD[i][9] + "'");
                        if (Pre.Length > 0)
                        { Uvalue = Program.UTIL.ToDoubleOrZero(Pre[0][0]); }
                    }
                    Door door = new Door(ZoneD[i][0], ZoneD[i][2], Program.UTIL.ToDoubleOrZero(ZoneD[i][1]), Uvalue, Program.UTIL.ToDoubleOrZero(ZoneD[i][4]), ZoneD[i][5], ZoneD[i][6], ZoneD[i][7]);
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
            { Cal_Qfh_Pre(PreProjNum[0][0], 검토유형); }
            else { Cal_Qfh_Now(NowProjNum[0][0], 검토유형); }


            if (검토유형 != "냉방")
            { Cal_Qfc_Pre(PreProjNum[0][0], 검토유형); }
            else { Cal_Qfc_Now(NowProjNum[0][0], 검토유형); }

            if (검토유형 != "급탕")
            { Cal_Qfw(PreProjNum[0][0], 검토유형); }
            else { Cal_Qfw(NowProjNum[0][0], 검토유형); }

           

            #region 파이널계산
            Final final1;
            if (검토유형 == "난방")
            {
                final1 = new Final(PreProjNum[0][0]);
                final1.Load_Heating_Final(NowProjNum[0][0]);
                final1.Load_Cooling_Final(PreProjNum[0][0]);
                final1.Load_DHW_Final(PreProjNum[0][0]);
                final1.Load_AHU_Final(PreProjNum[0][0]);
            }
            else if (검토유형 == "냉방")
            {
                final1 = new Final(PreProjNum[0][0]);
                final1.Load_Heating_Final(PreProjNum[0][0]);
                final1.Load_Cooling_Final(NowProjNum[0][0]);
                final1.Load_DHW_Final(PreProjNum[0][0]);
                final1.Load_AHU_Final(PreProjNum[0][0]);
            }
            else if (검토유형 == "기밀+열회수기")
            {
                final1 = new Final(PreProjNum[0][0]);
                final1.Load_Heating_Final(PreProjNum[0][0]);
                final1.Load_Cooling_Final(PreProjNum[0][0]);
                final1.Load_DHW_Final(PreProjNum[0][0]);
                final1.Load_AHU_Final(NowProjNum[0][0]);
            }
            else if (검토유형 == "공조")
            {
                final1 = new Final(PreProjNum[0][0]);
                final1.Load_Heating_Final(PreProjNum[0][0]);
                final1.Load_Cooling_Final(PreProjNum[0][0]);
                final1.Load_DHW_Final(PreProjNum[0][0]);
                final1.Load_AHU_Final(NowProjNum[0][0]);
            }
            else if (검토유형 == "태양광" || 검토유형=="풍력")
            {
                final1 = new Final(PreProjNum[0][0]);
                final1.Load_Heating_Final(PreProjNum[0][0]);
                final1.Load_Cooling_Final(PreProjNum[0][0]);
                final1.Load_DHW_Final(PreProjNum[0][0]);
                final1.Load_AHU_Final(PreProjNum[0][0]);
            }
            else if (검토유형 == "급탕")
            {
                final1 = new Final(PreProjNum[0][0]);
                final1.Load_Heating_Final(PreProjNum[0][0]);
                final1.Load_Cooling_Final(PreProjNum[0][0]);
                final1.Load_DHW_Final(NowProjNum[0][0]);
                final1.Load_AHU_Final(PreProjNum[0][0]);
            }
            else
            {
                final1 = new Final(PreProjNum[0][0]);
                final1.Load_Heating_Final(PreProjNum[0][0]);
                final1.Load_Cooling_Final(PreProjNum[0][0]);
                final1.Load_DHW_Final(PreProjNum[0][0]);
                final1.Load_AHU_Final(PreProjNum[0][0]);
            }

            if (검토유형 == "태양광" )
            {
                CALC.Final_Calc(final1, NowProjNum[0][0], false); //소요량 계산 > 신재생 계산 > 신재생분배 > 파이널 계산
                CALC.PVCalc(NowProjNum[0][0]);
                CALC.WPCalc(PreProjNum[0][0]);

                final1 = new Final(NowProjNum[0][0]);
                final1.Load_Heating_Final(PreProjNum[0][0]);
                final1.Load_Cooling_Final(PreProjNum[0][0]);
                final1.Load_DHW_Final(PreProjNum[0][0]);
                final1.Load_AHU_Final(PreProjNum[0][0]);
                CALC.Final_Calc(final1, NowProjNum[0][0], true);
            }
            else if(검토유형 == "풍력")
            {
                CALC.Final_Calc(final1, NowProjNum[0][0], false); //소요량 계산 > 신재생 계산 > 신재생분배 > 파이널 계산
                CALC.PVCalc(PreProjNum[0][0]);
                CALC.WPCalc(NowProjNum[0][0]);

                final1 = new Final(NowProjNum[0][0]);
                final1.Load_Heating_Final(PreProjNum[0][0]);
                final1.Load_Cooling_Final(PreProjNum[0][0]);
                final1.Load_DHW_Final(PreProjNum[0][0]);
                final1.Load_AHU_Final(PreProjNum[0][0]);
                CALC.Final_Calc(final1, NowProjNum[0][0], true);

            }
            else
            {
                CALC.Final_Calc(final1, PreProjNum[0][0], false);
            }

            Save_Alt(final1, 검토유형);

            Program.DB.saveProject();

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
                        calc1_AHU1.Load_Climate();
                        AHU_Load_ZoneData_pre(calc1_AHU1, ProjNum);
                        calc1_AHU1.Load_GeneralData(ProjNum);
                        calc1_AHU1.Load_AHUData(ProjNum);
                        calc1_AHU1.Load_DuctData(ProjNum);
                        calc1_AHU1.Load_PrehData(ProjNum);
                        CALC.AHUSystem_PreCalc(calc1_AHU1);
                    }
                    else
                    {
                        AHU calc1_HRV1 = new AHU(Num[i][0]);
                        CALC.AHUs[Num[i][0]] = calc1_HRV1;
                        calc1_HRV1.Load_Climate();
                        AHU_Load_ZoneData_pre(calc1_HRV1, ProjNum);
                        calc1_HRV1.Load_GeneralData(ProjNum);
                        calc1_HRV1.Load_HRVData(ProjNum);
                        calc1_HRV1.Load_DuctData(ProjNum);
                        calc1_HRV1.Load_PrehData(ProjNum);
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
                        calc2_AHU1.Load_Climate();
                        AHU_Load_ZoneData_pre(calc2_AHU1, ProjNum);
                        calc2_AHU1.Load_GeneralData(ProjNum);
                        calc2_AHU1.Load_AHUData(ProjNum);
                        calc2_AHU1.Load_DuctData(ProjNum);
                        calc2_AHU1.Load_PrehData(ProjNum);
                        CALC.AHUSystem_PostCalc(calc2_AHU1);
                    }
                    else
                    {
                        AHU calc2_HRV1 = new AHU(Num[i][0]);
                        CALC.AHUs[Num[i][0]] = calc2_HRV1;
                        calc2_HRV1.Load_Climate();
                        AHU_Load_ZoneData_pre(calc2_HRV1, ProjNum);
                        calc2_HRV1.Load_GeneralData(ProjNum);
                        calc2_HRV1.Load_HRVData(ProjNum);
                        calc2_HRV1.Load_DuctData(ProjNum);
                        calc2_HRV1.Load_PrehData(ProjNum);
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

            string[][] value = Program.DB.getValue(ProjNum, "ZoneGeneral_Form", "존번호", "선택열회수기 = '" + ahu1.AHUNum + "' and 환기유무='True'");
            string[][] PostZone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,기존존", "");
            if (value.Length > 0 && PostZone.Length > 0)
            {
                for (int k = 0; k < value.Length; k++)
                {
                    for (int i = 0; i < PostZone.Length; i++)
                    {
                        ArrayList splitzone = new ArrayList();
                        splitzone = Split_(PostZone[i][1]);

                        for (int ii = 0; ii < splitzone.Count; ii++)
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
                    string[][] ZoneValue = Program.DB.getValue(ProjNum, "ZoneGeneral_form", "용도프로필,이용일환기량,순바닥면적,공조시간,냉방습도,난방습도", "존번호='" + ahu1.SelectZone_split[n] + "'");
                    if (ZoneValue.Length > 0)
                    {
                        ahu1.Vmin_tot += Program.UTIL.ToDoubleOrZero(ZoneValue[0][1]);
                        ahu1.ANF_tot += Program.UTIL.ToDoubleOrZero(ZoneValue[0][2]);
                        Zone zone = Program.CALC.getZone(ahu1.SelectZone_split[n].ToString());
                        ahu1.Qh_a_tot += zone.Qb_a[0];
                        ahu1.Qc_a_tot += zone.Qb_a[1];
                        ahu1.Qmax_tot[0] += zone.Q_max[0] / 1000;
                        ahu1.Qmax_tot[1] += zone.Q_max[1] / 1000;
                        ahu1.tvmech_avg[0] += Program.UTIL.ToDoubleOrZero(ZoneValue[0][3]) * zone.Qb_a[0];
                        ahu1.tvmech_avg[1] += Program.UTIL.ToDoubleOrZero(ZoneValue[0][3]) * zone.Qb_a[1];
                        for (int mth = 0; mth < 12; mth++)
                        {
                            ahu1.Qb_mth_tot[0, mth] += zone.Qb_mth[0, mth];
                            ahu1.Qb_mth_tot[1, mth] += zone.Qb_mth[1, mth];
                            ahu1.QDHU_mth_tot[mth] += zone.Q_DHU_tot[mth];
                            ahu1.dvmechmth_avg[0, mth] += zone.dwd_mth[mth] * zone.Qb_a[0];
                            ahu1.dvmechmth_avg[1, mth] += zone.dwd_mth[mth] * zone.Qb_a[1];
                        }
                        string[][] Usage = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "난방설정온도,냉방설정온도,공조운전시부재율,공조냉방부분운전계수", "용도명='" + ZoneValue[0][0] + "'");
                        if (Usage.Length > 0)
                        {
                            ahu1.theta_i_set[0] += Program.UTIL.ToDoubleOrZero(Usage[0][0]) * zone.Qb_a[0];
                            ahu1.theta_i_set[1] += Program.UTIL.ToDoubleOrZero(Usage[0][1]) * zone.Qb_a[1];
                        }
                        string[][] HumidC = Program.DB.getValue(DB.type.BaseDB_HCneed, "습도설정", "냉방설정습도", "등급='" + ZoneValue[0][4] + "'");
                        if (HumidC.Length > 0)
                        {
                            ahu1.X_i_max += Program.UTIL.ToDoubleOrZero(HumidC[0][0]) / 1000 * zone.Qb_a[1];
                        }
                        string[][] HumidH = Program.DB.getValue(DB.type.BaseDB_HCneed, "습도설정", "난방설정습도", "등급='" + ZoneValue[0][5] + "'");
                        if (HumidH.Length > 0)
                        {
                            ahu1.X_i_min += Program.UTIL.ToDoubleOrZero(HumidH[0][0]) / 1000 * zone.Qb_a[0];
                        }

                        // χi,c(월별 실내 절대습도) — ZoneDHU에서 미리 집계한 값(냉방/제습 트랙)을 존 가중평균
                        ZoneDHU zoneDHU = Program.CALC.getZoneDHU(ahu1.SelectZone_split[n].ToString());
                        if (zoneDHU != null)
                        {
                            for (int mth = 0; mth < 12; mth++)
                            {
                                ahu1.X_i[0, mth] += zoneDHU.X_i[0, mth] * zone.Qb_a[0];
                                ahu1.X_i[1, mth] += zoneDHU.X_i[1, mth] * zone.Qb_a[1];
                            }
                        }
                    }
                }
                ahu1.theta_i_set[0] = ahu1.Qh_a_tot > 0 ? ahu1.theta_i_set[0] / ahu1.Qh_a_tot : 0;
                ahu1.theta_i_set[1] = ahu1.Qc_a_tot > 0 ? ahu1.theta_i_set[1] / ahu1.Qc_a_tot : 0;
                ahu1.tvmech_avg[0] = ahu1.Qh_a_tot > 0 ? ahu1.tvmech_avg[0] / ahu1.Qh_a_tot : 0;
                ahu1.tvmech_avg[1] = ahu1.Qc_a_tot > 0 ? ahu1.tvmech_avg[1] / ahu1.Qc_a_tot : 0;
                ahu1.X_i_max = ahu1.Qc_a_tot > 0 ? ahu1.X_i_max / ahu1.Qc_a_tot : 0;
                ahu1.X_i_min = ahu1.Qh_a_tot > 0 ? ahu1.X_i_min / ahu1.Qh_a_tot : 0;
                ahu1.X_i_set[1] = ahu1.X_i_max;
                ahu1.X_i_set[0] = ahu1.X_i_min;
                for (int mth = 0; mth < 12; mth++)
                {
                    ahu1.dvmechmth_avg[0, mth] = ahu1.Qh_a_tot > 0 ? ahu1.dvmechmth_avg[0, mth] / ahu1.Qh_a_tot : 0;
                    ahu1.dvmechmth_avg[1, mth] = ahu1.Qc_a_tot > 0 ? ahu1.dvmechmth_avg[1, mth] / ahu1.Qc_a_tot : 0;
                    ahu1.X_i[0, mth] = ahu1.Qh_a_tot > 0 ? ahu1.X_i[0, mth] / ahu1.Qh_a_tot : 0;
                    ahu1.X_i[1, mth] = ahu1.Qc_a_tot > 0 ? ahu1.X_i[1, mth] / ahu1.Qc_a_tot : 0;
                }
            }
            else
            {
                for (int n = 0; n < ahu1.SelectZone_split.Count; n++)
                {
                    string[][] ZoneValue = Program.DB.getValue(ProjNum, "ZoneGeneral_form", "용도프로필,이용일환기량,순바닥면적,공조시간,주이용일,냉방습도,난방습도", "존번호='" + ahu1.SelectZone_split[n] + "'");
                    if (ZoneValue.Length > 0)
                    {
                        ahu1.Vmin_tot += Program.UTIL.ToDoubleOrZero(ZoneValue[0][1]);
                        ahu1.ANF_tot += Program.UTIL.ToDoubleOrZero(ZoneValue[0][2]);
                        Zone zone = Program.CALC.getZone(ahu1.SelectZone_split[n].ToString());
                        ahu1.Qh_a_tot += zone.Qb_a[0];
                        ahu1.Qc_a_tot += zone.Qb_a[1];

                        string[][] HumidC = Program.DB.getValue(DB.type.BaseDB_HCneed, "습도설정", "냉방설정습도", "등급='" + ZoneValue[0][5] + "'");
                        if (HumidC.Length > 0)
                        {
                            ahu1.X_i_max += Program.UTIL.ToDoubleOrZero(HumidC[0][0]) / 1000 * zone.Qb_a[1];
                        }
                        string[][] HumidH = Program.DB.getValue(DB.type.BaseDB_HCneed, "습도설정", "난방설정습도", "등급='" + ZoneValue[0][6] + "'");
                        if (HumidH.Length > 0)
                        {
                            ahu1.X_i_min += Program.UTIL.ToDoubleOrZero(HumidH[0][0]) / 1000 * zone.Qb_a[0];
                        }
                        ahu1.tvmech_avg[0] += Program.UTIL.ToDoubleOrZero(ZoneValue[0][3]) * zone.Qb_a[0];
                        ahu1.tvmech_avg[1] += Program.UTIL.ToDoubleOrZero(ZoneValue[0][3]) * zone.Qb_a[1];
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
                                ahu1.dvmechmth_avg[0, mth] += Program.UTIL.ToDoubleOrZero(ValueK[0][0]) * zone.Qb_a[0];
                                ahu1.dvmechmth_avg[1, mth] += Program.UTIL.ToDoubleOrZero(ValueK[0][0]) * zone.Qb_a[1];
                            }
                        }

                        string[][] Usage = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "난방설정온도,냉방설정온도,공조운전시부재율,공조냉방부분운전계수", "용도명='" + ZoneValue[0][0] + "'");
                        if (Usage.Length > 0)
                        {
                            ahu1.theta_i_set[0] += Program.UTIL.ToDoubleOrZero(Usage[0][0]) * zone.Qb_a[0];
                            ahu1.theta_i_set[1] += Program.UTIL.ToDoubleOrZero(Usage[0][1]) * zone.Qb_a[1];
                        }

                    }
                }

                ahu1.theta_i_set[0] = ahu1.Qh_a_tot > 0 ? ahu1.theta_i_set[0] / ahu1.Qh_a_tot : 0;
                ahu1.theta_i_set[1] = ahu1.Qc_a_tot > 0 ? ahu1.theta_i_set[1] / ahu1.Qc_a_tot : 0;
                ahu1.tvmech_avg[0] = ahu1.Qh_a_tot > 0 ? ahu1.tvmech_avg[0] / ahu1.Qh_a_tot : 0;
                ahu1.tvmech_avg[1] = ahu1.Qc_a_tot > 0 ? ahu1.tvmech_avg[1] / ahu1.Qc_a_tot : 0;
                ahu1.X_i_max = ahu1.Qc_a_tot > 0 ? ahu1.X_i_max / ahu1.Qc_a_tot : 0;
                ahu1.X_i_min = ahu1.Qh_a_tot > 0 ? ahu1.X_i_min / ahu1.Qh_a_tot : 0;
                ahu1.X_i_set[1] = ahu1.X_i_max;
                ahu1.X_i_set[0] = ahu1.X_i_min;
                for (int mth = 0; mth < 12; mth++)
                {
                    ahu1.dvmechmth_avg[0, mth] = ahu1.Qh_a_tot > 0 ? ahu1.dvmechmth_avg[0, mth] / ahu1.Qh_a_tot : 0;
                    ahu1.dvmechmth_avg[1, mth] = ahu1.Qc_a_tot > 0 ? ahu1.dvmechmth_avg[1, mth] / ahu1.Qc_a_tot : 0;
                }
            }
        }
        #endregion
        #region 난방
        public void Cal_Qfh_Pre(string ProjNum, string 검토유형)
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
                    if (검토유형 == "조닝")
                    {
                        Save_Qfh_result_byZone(Heating1, 검토유형, ProjNum);
                    }
                    //  CALC.Heating_Save(Heating1);
                }
            }
        }
        public void Cal_Qfh_Now(string ProjNum, string 검토유형)
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
                    if (검토유형 == "난방")
                    {
                        Save_Qfh_result_byZone(Heating1, 검토유형, ProjNum);
                    }
                    //   CALC.Heating_Save(Heating1);
                }
            }
        }

        private void Save_Qfh_result_byZone(Heating Heating1, string 검토유형, string ProjNum)
        {
            double Qhf_elec = 0, Qhf_gas = 0;
            for (int mth = 0; mth < 12; mth++)
            {
                if (Heating1.Carrier == "전기")
                {
                    Qhf_elec += (Heating1.Qh_f[mth] + Heating1.Wh_ce[mth] + Heating1.Wh_d[mth] + Heating1.Wh_s[mth] + Heating1.Wh_g[mth]);
                }
                else
                {
                    Qhf_elec += (Heating1.Wh_ce[mth] + Heating1.Wh_d[mth] + Heating1.Wh_s[mth] + Heating1.Wh_g[mth]);
                    Qhf_gas += Heating1.Qh_f[mth];
                }
            }

            string[][] 프로젝트번호 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호");
            Boolean Now_Check = true;
            if (ProjNum == 프로젝트번호[0][0])
            { Now_Check = true; }
            else
            { Now_Check = false; }

            string[][] Value_ce = null;
            if (Now_Check == true)
            {
                Value_ce = Program.DB.getValue(ProjNum, "Heating_ce_Form", "공급설비,존번호,부하율", "난방시스템 = '" + Heating1.HeatingNum + "'");
            }
            else
            {
                Value_ce = Program.DB.getValue(DB.type.ProjDB, "Heating_ce_Form_Element", "공급설비,존번호,부하율", "난방시스템 = '" + Heating1.HeatingNum + "'");
            }

            Zone zone = null;

            if (Value_ce.Length > 0)
            {
                double Qb_a_sum = 0;
                for (int n = 0; n < Value_ce.Length; n++)
                {

                    if (Now_Check == true)
                    {
                        zone = Program.CALC.getZone(Value_ce[n][1]);
                        string[][] PostZone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "기존존", "존번호='" + zone.ZoneNum + "'");
                        if (PostZone.Length > 0)
                        {
                            Qb_a_sum += Program.UTIL.ToDoubleOrZero(Value_ce[n][2]) * zone.Qb_a[0];
                        }
                    }
                    else
                    {
                        string[][] PostZone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,기존존", "");
                        if (PostZone.Length > 0)
                        {
                            for (int j = 0; j < PostZone.Length; j++)
                            {
                                ArrayList split = Split_(PostZone[j][1]);
                                for (int m = 0; m < split.Count; m++)
                                {
                                    if (split[m].ToString() == Value_ce[n][1])
                                    {
                                        zone = Program.CALC.getZone(PostZone[j][0]);
                                        Qb_a_sum += Program.UTIL.ToDoubleOrZero(Value_ce[n][2]) * zone.Qb_a[0];
                                    }
                                }
                            }
                        }
                    }
                }

                for (int n = 0; n < Value_ce.Length; n++)
                {
                    if (Now_Check == true)
                    {
                        zone = Program.CALC.getZone(Value_ce[n][1]);
                        string[][] PostZone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "기존존", "존번호='" + zone.ZoneNum + "'");
                        if (PostZone.Length > 0)
                        {
                            Program.DB.setValue(DB.type.ProjDB, "Heating_Result_Element", "검토유형,난방시스템,기존존번호,계획존번호,공급설비,부하율,연료,난방소요량",
                                       "'" + 검토유형 + "','" + Heating1.HeatingNum + "','" + PostZone[0][0] + "','" + zone.ZoneNum + "','" + Value_ce[n][0] + "','" + Value_ce[n][2] + "','" + "전기" + "','" + Qhf_elec / Qb_a_sum * Program.UTIL.ToDoubleOrZero(Value_ce[n][2]) * zone.Qb_a[0] + "'"
                                    , "검토유형,난방시스템,기존존번호,계획존번호,공급설비,부하율,연료");
                            if (Heating1.Carrier != "전기")
                            {
                                Program.DB.setValue(DB.type.ProjDB, "Heating_Result_Element", "검토유형,난방시스템,기존존번호,계획존번호,공급설비,부하율,연료,난방소요량",
                                         "'" + 검토유형 + "','" + Heating1.HeatingNum + "','" + PostZone[0][0] + "','" + zone.ZoneNum + "','" + Value_ce[n][0] + "','" + Value_ce[n][2] + "','" + Heating1.Carrier + "','" + Qhf_gas / Qb_a_sum * Program.UTIL.ToDoubleOrZero(Value_ce[n][2]) * zone.Qb_a[0] + "'"
                                      , "검토유형,난방시스템,기존존번호,계획존번호,공급설비,부하율,연료");
                            }
                        }
                    }
                    else
                    {
                        string[][] PostZone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,기존존", "");
                        if (PostZone.Length > 0)
                        {
                            for (int j = 0; j < PostZone.Length; j++)
                            {
                                ArrayList split = Split_(PostZone[j][1]);
                                for (int m = 0; m < split.Count; m++)
                                {
                                    if (split[m].ToString() == Value_ce[n][1])
                                    {
                                        zone = Program.CALC.getZone(PostZone[j][0]);
                                        Program.DB.setValue(DB.type.ProjDB, "Heating_Result_Element", "검토유형,난방시스템,기존존번호,계획존번호,공급설비,부하율,연료,난방소요량",
                                                       "'" + 검토유형 + "','" + Heating1.HeatingNum + "','" + PostZone[j][1] + "','" + zone.ZoneNum + "','" + Value_ce[n][0] + "','" + Value_ce[n][2] + "','" + "전기" + "','" + Qhf_elec / Qb_a_sum * Program.UTIL.ToDoubleOrZero(Value_ce[n][2]) * zone.Qb_a[0] + "'"
                                                    , "검토유형,난방시스템,기존존번호,계획존번호,공급설비,부하율,연료");
                                        if (Heating1.Carrier != "전기")
                                        {
                                            Program.DB.setValue(DB.type.ProjDB, "Heating_Result_Element", "검토유형,난방시스템,기존존번호,계획존번호,공급설비,부하율,연료,난방소요량",
                                                     "'" + 검토유형 + "','" + Heating1.HeatingNum + "','" + PostZone[j][1] + "','" + zone.ZoneNum + "','" + Value_ce[n][0] + "','" + Value_ce[n][2] + "','" + Heating1.Carrier + "','" + Qhf_gas / Qb_a_sum * Program.UTIL.ToDoubleOrZero(Value_ce[n][2]) * zone.Qb_a[0] + "'"
                                                  , "검토유형,난방시스템,기존존번호,계획존번호,공급설비,부하율,연료");
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }
        #endregion
        #region 냉방
        public void Cal_Qfc_Pre(string ProjNum, string 검토유형)
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
                    if (검토유형 == "조닝")
                    {
                        Save_Qfc_result_byZone(Cooling1, 검토유형, ProjNum);
                    }
                    // CALC.Cooling_Save(Cooling1);
                }
            }
        }
        public void Cal_Qfc_Now(string ProjNum, string 검토유형)
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
                    if (검토유형 == "냉방")
                    {
                        Save_Qfc_result_byZone(Cooling1, 검토유형, ProjNum);
                    }
                    //CALC.Cooling_Save(Cooling1);
                }
            }
        }

        private void Save_Qfc_result_byZone(Cal_Cooling Cooling1, string 검토유형, string ProjNum)
        {
            double Qcf_elec = 0, Qcf_gas = 0;
            for (int mth = 0; mth < 12; mth++)
            {
                if (Cooling1.Carrier == "전기")
                {
                    Qcf_elec += (Cooling1.QC_f[mth] + Cooling1.W[mth]);
                }
                else
                {
                    Qcf_elec += Cooling1.W[mth];
                    Qcf_gas += Cooling1.QC_f[mth];
                }
            }

            string[][] 프로젝트번호 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호");

            Boolean Now_Check = true;
            if (ProjNum == 프로젝트번호[0][0])
            { Now_Check = true; }
            else
            { Now_Check = false; }
            string[][] Value_ce;
            if (Now_Check == true)
            { Value_ce = Program.DB.getValue_SameCheck(ProjNum, "Cooling_ce_Form", "공급설비,존번호,부하율", "냉방시스템 = '" + Cooling1.CoolingNum + "'"); }
            else
            {
                Value_ce = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Cooling_ce_Form_Element", "공급설비,존번호,부하율", "냉방시스템 = '" + Cooling1.CoolingNum + "'");
            }

            Zone zone = null;
            if (Value_ce.Length > 0)
            {
                double Qb_a_sum = 0;
                for (int n = 0; n < Value_ce.Length; n++)
                {

                    if (Now_Check == true)
                    {
                        zone = Program.CALC.getZone(Value_ce[n][1]);
                        string[][] PostZone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "기존존", "존번호='" + zone.ZoneNum + "'");
                        if (PostZone.Length > 0)
                        {
                            Qb_a_sum += Program.UTIL.ToDoubleOrZero(Value_ce[n][2]) * zone.Qb_a[1];
                        }
                    }
                    else
                    {
                        string[][] PostZone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,기존존", "");
                        if (PostZone.Length > 0)
                        {
                            for (int j = 0; j < PostZone.Length; j++)
                            {
                                ArrayList split = Split_(PostZone[j][1]);
                                for (int m = 0; m < split.Count; m++)
                                {
                                    if (split[m].ToString() == Value_ce[n][1])
                                    {
                                        zone = Program.CALC.getZone(PostZone[j][0]);
                                        Qb_a_sum += Program.UTIL.ToDoubleOrZero(Value_ce[n][2]) * zone.Qb_a[1];
                                    }
                                }
                            }
                        }
                    }
                }

                for (int n = 0; n < Value_ce.Length; n++)
                {

                    if (Now_Check == true)
                    {
                        zone = Program.CALC.getZone(Value_ce[n][1]);
                        string[][] PostZone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "기존존", "존번호='" + zone.ZoneNum + "'");
                        if (PostZone.Length > 0)
                        {
                            Program.DB.setValue(DB.type.ProjDB, "Cooling_Result_Element", "검토유형,냉방시스템,기존존번호,계획존번호,공급설비,부하율,연료,냉방소요량",
                                       "'" + 검토유형 + "','" + Cooling1.CoolingNum + "','" + PostZone[0][0] + "','" + zone.ZoneNum + "','" + Value_ce[n][0] + "','" + Value_ce[n][2] + "','" + "전기" + "','" + Qcf_elec / Qb_a_sum * Program.UTIL.ToDoubleOrZero(Value_ce[n][2]) * zone.Qb_a[1] + "'"
                                    , "검토유형,난방시스템,기존존번호,계획존번호,공급설비,부하율,연료");
                            if (Cooling1.Carrier != "전기")
                            {
                                Program.DB.setValue(DB.type.ProjDB, "Cooling_Result_Element", "검토유형,냉방시스템,기존존번호,계획존번호,공급설비,부하율,연료,냉방소요량",
                                         "'" + 검토유형 + "','" + Cooling1.CoolingNum + "','" + PostZone[0][0] + "','" + zone.ZoneNum + "','" + Value_ce[n][0] + "','" + Value_ce[n][2] + "','" + Cooling1.Carrier + "','" + Qcf_gas / Qb_a_sum * Program.UTIL.ToDoubleOrZero(Value_ce[n][2]) * zone.Qb_a[1] + "'"
                                      , "검토유형,난방시스템,기존존번호,계획존번호,공급설비,부하율,연료");
                            }
                        }
                    }
                    else
                    {
                        string[][] PostZone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,기존존", "");
                        if (PostZone.Length > 0)
                        {
                            for (int j = 0; j < PostZone.Length; j++)
                            {
                                ArrayList split = Split_(PostZone[j][1]);
                                for (int m = 0; m < split.Count; m++)
                                {
                                    if (split[m].ToString() == Value_ce[n][1])
                                    {
                                        zone = Program.CALC.getZone(PostZone[j][0]);
                                        Program.DB.setValue(DB.type.ProjDB, "Cooling_Result_Element", "검토유형,냉방시스템,기존존번호,계획존번호,공급설비,부하율,연료,냉방소요량",
                                                       "'" + 검토유형 + "','" + Cooling1.CoolingNum + "','" + PostZone[j][1] + "','" + zone.ZoneNum + "','" + Value_ce[n][0] + "','" + Value_ce[n][2] + "','" + "전기" + "','" + Qcf_elec / Qb_a_sum * Program.UTIL.ToDoubleOrZero(Value_ce[n][2]) * zone.Qb_a[1] + "'"
                                                    , "검토유형,난방시스템,기존존번호,계획존번호,공급설비,부하율,연료");
                                        if (Cooling1.Carrier != "전기")
                                        {
                                            Program.DB.setValue(DB.type.ProjDB, "Cooling_Result_Element", "검토유형,냉방시스템,기존존번호,계획존번호,공급설비,부하율,연료,냉방소요량",
                                                     "'" + 검토유형 + "','" + Cooling1.CoolingNum + "','" + PostZone[j][1] + "','" + zone.ZoneNum + "','" + Value_ce[n][0] + "','" + Value_ce[n][2] + "','" + Cooling1.Carrier + "','" + Qcf_gas / Qb_a_sum * Program.UTIL.ToDoubleOrZero(Value_ce[n][2]) * zone.Qb_a[1] + "'"
                                                  , "검토유형,난방시스템,기존존번호,계획존번호,공급설비,부하율,연료");
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }
        #endregion
        #region 급탕
        public void Cal_Qfw(string ProjNum, string 검토유형)
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
                    if (검토유형 == "조닝" || 검토유형 == "급탕")
                    {
                        Save_Qfw_result_byZone(DHW1, 검토유형, ProjNum);
                    }
                }
            }
        }
        private void Save_Qfw_result_byZone(DHW DHW1, string 검토유형, string ProjNum)
        {
            double Qwf_elec = 0, Qwf_gas = 0;

            for (int mth = 0; mth < 12; mth++)
            {
                if (DHW1.Carrier == "전기")
                {
                    Qwf_elec += (DHW1.Qw_f[mth] + DHW1.Ww_d[mth] + DHW1.Ww_s[mth] + DHW1.Ww_g[mth]);
                }
                else
                {
                    Qwf_elec += (DHW1.Ww_d[mth] + DHW1.Ww_s[mth] + DHW1.Ww_g[mth]);
                    Qwf_gas += DHW1.Qw_f[mth];
                }
            }

            string[][] 프로젝트번호 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호");

            double[,] Qwb_mth; double[,] theta_ih; double[,] dop_mth; double[] th_op_day; double[] Qwb_a; double[] theta_i_h_set;
            Boolean Now_Check = true;
            if (ProjNum == 프로젝트번호[0][0])
            { Now_Check = true; }
            else
            { Now_Check = false; }

            Qwb_mth = new double[DHW1.SelectZone_split.Count, 12];
            Qwb_a = new double[DHW1.SelectZone_split.Count];
            double[] dop_a = new double[DHW1.SelectZone_split.Count];
            for (int n = 0; n < DHW1.SelectZone_split.Count; n++)
            {
                Zone zone = null; double Qwb_day = 0;
                if (Now_Check == true)
                {
                    zone = Program.CALC.getZone(DHW1.SelectZone_split[n].ToString());
                    string[][] kk = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "일일급탕요구량", "존번호 = '" + zone.ZoneNum + "'");
                    if (kk.Length > 0)
                    { Qwb_day += Program.UTIL.ToDoubleOrZero(kk[0][0]); }
                }
                else
                {
                    string[][] PostZone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,기존존", "");
                    if (PostZone.Length > 0)
                    {
                        for (int j = 0; j < PostZone.Length; j++)
                        {
                            ArrayList split = Split_(PostZone[j][1]);
                            for (int m = 0; m < split.Count; m++)
                            {
                                if (split[m].ToString() == DHW1.SelectZone_split[n].ToString())
                                {
                                    zone = Program.CALC.getZone(PostZone[j][0]);
                                    string[][] kk = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "일일급탕요구량", "존번호 = '" + zone.ZoneNum + "'");
                                    if (kk.Length > 0)
                                    { Qwb_day += Program.UTIL.ToDoubleOrZero(kk[0][0]); }
                                }
                            }
                        }
                    }
                }
                if (zone != null)
                {
                    for (int mth = 0; mth < 12; mth++)
                    {
                        dop_a[n] += zone.dwd_mth[mth];
                    }
                    for (int mth = 0; mth < 12; mth++)
                    {
                        Qwb_mth[n, mth] = Qwb_day * dop_a[n] * DHW1.dmth[mth] / 365 * (-0.02 * DHW1.theta_e[mth] + 1.25);
                        Qwb_a[n] += Qwb_mth[n, mth]; //연간 요구량
                    }
                }
            }
            double Qwb_a_sum = 0;
            for (int n = 0; n < DHW1.SelectZone_split.Count; n++)
            {
                Qwb_a_sum += Qwb_a[n];
            }
            for (int n = 0; n < DHW1.SelectZone_split.Count; n++)
            {
                Zone zone = null;
                if (Now_Check == true)
                {
                    string[][] PostZone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "기존존", "존번호='" + DHW1.SelectZone_split[n].ToString() + "'");
                    if (PostZone.Length > 0)
                    {
                        Program.DB.setValue(DB.type.ProjDB, "DHWSystem_Result_Element", "검토유형,급탕시스템,기존존번호,계획존번호,연료,급탕소요량",
                                   "'" + 검토유형 + "','" + DHW1.DHWNum + "','" + PostZone[0][0] + "','" + DHW1.SelectZone_split[n].ToString() + "','" + "전기" + "','" + Qwf_elec / Qwb_a_sum * Qwb_a[n] + "'"
                                , "검토유형,급탕시스템,기존존번호,계획존번호,연료");
                        if (DHW1.Carrier != "전기")
                        {
                            Program.DB.setValue(DB.type.ProjDB, "DHWSystem_Result_Element", "검토유형,급탕시스템,기존존번호,계획존번호,연료,급탕소요량",
                                     "'" + 검토유형 + "','" + DHW1.DHWNum + "','" + PostZone[0][0] + "','" + DHW1.SelectZone_split[n].ToString() + "','" + DHW1.Carrier + "','" + Qwf_gas / Qwb_a_sum * Qwb_a[n] + "'"
                                  , "검토유형,급탕시스템,기존존번호,계획존번호,연료");
                        }
                    }
                }
                else
                {
                    string[][] PostZone = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,기존존", "");
                    if (PostZone.Length > 0)
                    {
                        for (int j = 0; j < PostZone.Length; j++)
                        {
                            ArrayList split = Split_(PostZone[j][1]);
                            for (int m = 0; m < split.Count; m++)
                            {
                                if (split[m].ToString() == DHW1.SelectZone_split[n].ToString())
                                {
                                    zone = Program.CALC.getZone(PostZone[j][0]);
                                    Program.DB.setValue(DB.type.ProjDB, "DHWSystem_Result_Element", "검토유형,급탕시스템,기존존번호,계획존번호,연료,급탕소요량",
                                                   "'" + 검토유형 + "','" + DHW1.DHWNum + "','" + PostZone[j][1] + "','" + zone.ZoneNum + "','" + "전기" + "','" + Qwf_elec / Qwb_a_sum * Qwb_a[n] + "'"
                                                , "검토유형,급탕시스템,기존존번호,계획존번호,연료");
                                    if (DHW1.Carrier != "전기")
                                    {
                                        Program.DB.setValue(DB.type.ProjDB, "DHWSystem_Result_Element", "검토유형,급탕시스템,기존존번호,계획존번호,연료,급탕소요량",
                                                 "'" + 검토유형 + "','" + DHW1.DHWNum + "','" + PostZone[j][1] + "','" + zone.ZoneNum + "','" + DHW1.Carrier + "','" + Qwf_gas / Qwb_a_sum * Qwb_a[n] + "'"
                                              , "검토유형,급탕시스템,기존존번호,계획존번호,연료");
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }
        #endregion 
        #region 조명
        private void Save_Qlf_Element(ZoneLight light1, string 검토유형)
        {
            string 조명번호 = null; string[][] Value = null;
            if (검토유형 == "조닝")
            {
                string[][] 기존존 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "기존존", "존번호='" + light1.ZoneNum + "'");
                if (기존존.Length > 0)
                {
                    ArrayList prezone = new ArrayList();
                    prezone = Split_(기존존[0][0]);
                    if (prezone.Count > 0)
                    {
                        Value = Program.DB.getValue(PreProjNum[0][0], "ZoneLighting_form", "조명번호", "번호='" + prezone[0].ToString() + "'");
                    }
                }
            }
            else
            {
                Value = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "조명번호", "번호='" + light1.ZoneNum + "'");
            }
            if (Value.Length > 0)
            {
                조명번호 = Value[0][0];
            }
            double Qlf_a = 0;
            for (int mth = 0; mth < 12; mth++)
            {
                Qlf_a += light1.Zone_Final_kWh[mth];
            }
            Program.DB.setValue(DB.type.ProjDB, "Light_Result_Element", "검토유형,존번호,조명번호,조명소요량",
                               "'" + 검토유형 + "','" + light1.ZoneNum + "','" + 조명번호 + "','" + Qlf_a + "'"
                            , "검토유형,존번호");
        }
        #endregion
    }
}
