using main
    ;
using main.subcontents;
using Microsoft.Office.Interop.Excel;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace main
{
    internal class Cal_Cooling
    {
        //임시 데이터
        string SelectAHU_nonsplit, SelectCG_nonsplit, SelectCGC_nonsplit, SelectCGE_nonsplit, SelectCGN_nonsplit, SelectCGComp_nonsplit;
        string SelectCT_nonsplit, SelectCTN_nonsplit;
        string SelectZone_nonsplit;
        string SelectAhu_nonsplit;

        List<string> SelectZone = new List<string>(), SelectAhu = new List<string>(), SelectCG = new List<string>(), SelectCGE = new List<string>(), SelectCGN = new List<string>(); //공통사항
        List<string> SelectCGC = new List<string>(), SelectCGComp = new List<string>(), SelectCT = new List<string>(), SelectCTN = new List<string>(); //선택사항

     
        //설비정보
        string CoolingNum, 프로젝트유형, CoolingName, InstallType, CG; //InstallType는 기존/신규임
        string Comp_f, Control_f, Econo_f;
        double Power_f, EER_f, Pctrl_f;
        List<double> Power = new List<double>(), EER = new List<double>(); //공통
        List<string> Comp = new List<string>(); //공냉식, 수냉식, 지열히트펌프 유형 
        double CWin, CWout; //냉수공급시
        string CSource, ArtType, ArtNumber, Cout; // A 및 숫자에 대한 지정값 Cout : 직팽식, 수방식, fC_M 멀티보정계수
        double fC_M;
        int Number_f, ZoneNumber_f, AhuNumber_f; //설비개수, 존개수
        string Carrier; ///연료

        enum _TYPE {실외기12kW,공냉식냉동기,수냉식냉동기,지열히트펌프,흡수식냉동기,지하수히트펌프};
        
        //냉각탑
        List<CoolTop> CT_Sum = new List<CoolTop>();
        double CSWin, CSWout; //냉동기로 유입되는 온도, 출구되는 온도 실외기 제외 모든유형 공통(지열히트펌프)
        double CTPower_f, CTfhrPL_f, CTPhrel_f, CTPctrlel_f;
        string CTControl_f, CTtype_f, CTmeth_f, CTFanType;
        int CTNum_f;


        //기후데이터 작성
        public double[] OutdoorTemperature = new double[12], Humidity = new double[12], WetTemperature = new double[12];
        string[] mth = new string[12];
        
        //냉방존
        List<zonemake> ZoneNameList = new List<zonemake>();
        public double[] QC_nd_z = new double[12], dwd_z = new double[12], theta_z = new double[12], tmth_z = new double[12], BC_z = new double[12];
        public double[] tC_op_z = new double[12], PL_Rate_z = new double[12], fC_PL_z = new double[12];
        public double top_z, Qc_max_z, QC_p_z, Beta_grenz_z, QC_a_z, A_z; //QC_p_z 공조기와 파워나누기, top_c_z는 공조기 가동시간임
        string SCZoneType_z;
        public double[] QC_ce_z = new double[12], QC_d_z = new double[12], QC_s_z = new double[12], QC_out_z = new double[12];
        public double[] W_ce_z = new double[12]; //W_g에 대기전력 포함시킴

       
        //공조기
        List<ahumake> AhuNameList = new List<ahumake>();
        public double[] QC_nd_ahu = new double[12], dwd_ahu = new double[12], theta_ahu = new double[12], tmth_ahu = new double[12], BC_ahu = new double[12];
        public double[] tC_op_ahu = new double[12], PL_Rate_ahu = new double[12], fC_PL_ahu = new double[12];
        public double top_ahu, Qc_max_ahu, QC_p_ahu, Beta_grenz_ahu, QC_a_ahu, A_ahu; //QC_p_z 공조기와 파워나누기
        string SCZoneType_ahu, Install_ahu; //SCZoneType_ahu는 멀티존, 단일존임 Install 은 공조기 설치위치로 (단열외피 외부, 단열외피 내부)
        public double[] QC_ce_ahu = new double[12], QC_d_ahu = new double[12], QC_s_ahu = new double[12], QC_out_ahu = new double[12];
        public double[] W_ce_ahu = new double[12];


        //분배/저장열손실         
        double nc_ce_sens, nc_ce, nc_d, nc_s, fSP;
        string Sto_Tank, Sto_Type;
       

        //냉방부분부하계산 및 최종 계산 결과값
        public double Theta_Around, ThetaC_gen_hr_req_in, ThetaC_gen_req_out, Theta_cond, Theta_evad, Anf, top;
        public double[] tC_op = new double[12], feer_corr = new double[12], EER_c = new double[12], SEER_c = new double[12], Theta_IC = new double[12], tmth = new double[12], fC_PL = new double[12];
        public double[] QC_nd = new double[12], QC_ce = new double[12], QC_d = new double[12], QC_s = new double[12], QC_out = new double[12], QC_f = new double[12];
        public double QCa_nd, QCa_ce, QCa_d, QCa_s, QCa_out, QCa_f, QCa_p, QCa_CO2; //정의 필요
        public double[] W_ce = new double[12], W_d = new double[12], W_s = new double[12], W_g = new double[12],  W = new double[12]; //정의 필요
        

        
       
            
        //펌프정의
        string SelectPump1_nonsplit, SelectPump2_nonsplit, SelectSPump1_nonsplit, SelectSPump2_nonsplit, PumpControl, SPumpControl, 펌프유무;
        double P1power, P2power, SP1power, SP2power; //H는 양정임 SP는 냉각수 및 지열열원 펌프를 지칭함 Source pump
        CoolPump Pump1 = new CoolPump(), Pump2 = new CoolPump(), SPump1 = new CoolPump(), SPump2 = new CoolPump();



        //공급설비정의
        List<CoolingCE> SelectCE = new List<CoolingCE>();
        string CE1_z, CE2_z, CE1_ahu, CE2_ahu;

        //설비정의
        List<CoolingGeneratorMake> CGM_Sum = new List<CoolingGeneratorMake>();

        public Cal_Cooling(String _CoolingNum)
        {
            this.CoolingNum = _CoolingNum;

            //coolingsystem_form에서 설비 항목 검토 진행
            Generator_Check();
            
            //냉방 설비 만들기
            Cooling_Generator();

            //냉방 설비 종합
            Generator_Sum();
            
            //공급설비 기준 부하율 반영
            Cal_CLRate();

            //최대부하,연간요구량,일일작동시간, 면적
            Cal_ZoneAhu();
            //냉방존
            Cal_Zone();
            //공조존         
            Cal_Ahu();

            //계산시작
            Find_Climate();
         
            //냉방존과 공조존을 합치기
            Cal_Load();
          
            //에너지소요량 계산
            Cal_CS();

            //보조설비에너지소요량 계산
            Cal_AuxSum();

            //저장
            Cal_Save();
        }
       
        private void Split(string nonSplit, List<string> type)
        {
            type.Clear();
            if (nonSplit != null)
            {
                string[] token = nonSplit.Split('+');
                foreach (string item in token)
                {
                    type.Add(item);
                }
            }
        }

        public void Generator_Check()
        {
            string[][] DefaultValue = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_form",
              "명칭,냉방설비,열원설비,공급존,공급AHU,냉방유닛,제어유형,외기냉방시스템,설치대수,저장탱크,저장유형,펌프유무,냉수펌프1,냉수펌프2,냉각수펌프1,냉각수펌프2", "번호 = '" + CoolingNum + "'");
            CoolingName = DefaultValue[0][0]; //명칭
            CG = DefaultValue[0][1];
            CSource = DefaultValue[0][2]; //열원설비

            SelectZone_nonsplit = DefaultValue[0][3];
            if (SelectZone_nonsplit != "" && SelectZone_nonsplit != null)
            {
                Split(SelectZone_nonsplit, SelectZone);
            }
            SelectAhu_nonsplit = DefaultValue[0][4];
            if (SelectAhu_nonsplit != "" && SelectAhu_nonsplit != null)
            {
                Split(SelectAhu_nonsplit, SelectAhu);
            }
            SelectCG_nonsplit = DefaultValue[0][5]; //냉방유닛
            SelectCGC_nonsplit = DefaultValue[0][6]; //제어유형
            SelectCGE_nonsplit = DefaultValue[0][7]; //외기냉방시스템
            SelectCGN_nonsplit = DefaultValue[0][8]; //설치대수

            //저장탱크
            if (DefaultValue[0][8] == "" || DefaultValue[0][9] == null)
            {
                Sto_Tank = "축냉탱크없음";
                Sto_Type = null;
            }
            else
            {
                Sto_Tank = DefaultValue[0][9];
                Sto_Type = DefaultValue[0][10];
            }
            펌프유무 = DefaultValue[0][11];
            //펌프
            if (펌프유무 == "펌프 있음") //펌프는 최대부하가 결정된 후에 진행됨
            {
                if (DefaultValue[0][12] != "" && DefaultValue[0][12] != null)//냉수펌프1
                {
                   SelectPump1_nonsplit = DefaultValue[0][12];
                   PumpSlite(SelectPump1_nonsplit, Pump1);
                }
                else SelectPump1_nonsplit = null;

                if (DefaultValue[0][13] != "" && DefaultValue[0][13] != null) //냉수펌프2
                {
                    SelectPump2_nonsplit = DefaultValue[0][13];
                    PumpSlite(SelectPump2_nonsplit, Pump2);
                }
                else SelectPump2_nonsplit = null;

                if (DefaultValue[0][14] != "" && DefaultValue[0][14] != null)//냉각수펌프1
                {
                    SelectSPump1_nonsplit = DefaultValue[0][14];
                    PumpSlite(SelectSPump1_nonsplit, SPump1);
                }
                else SelectSPump1_nonsplit = null;
                if (DefaultValue[0][15] != "" && DefaultValue[0][15] != null)//냉각수펌프2
                {
                    SelectSPump2_nonsplit = DefaultValue[0][15];
                    PumpSlite(SelectSPump2_nonsplit, SPump2);
                }
                else SelectSPump2_nonsplit = null;
            }

            if (CG == nameof(_TYPE.공냉식냉동기))
            {
                string[][] 공냉식 = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_form", "압축기", "번호 = '" + CoolingNum + "'");
                SelectCGComp_nonsplit = 공냉식[0][0];
                Split(SelectCGComp_nonsplit, SelectCGComp);
            }
            else if (CG == nameof(_TYPE.수냉식냉동기) || CG == nameof(_TYPE.흡수식냉동기))
            {
                string[][] 냉각탑 = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_form", "냉각탑,냉각탑개수", "번호 = '" + CoolingNum + "'");
                SelectCT_nonsplit = 냉각탑[0][0];
                Split(SelectCT_nonsplit, SelectCT);
                SelectCTN_nonsplit = 냉각탑[0][1];
                Split(SelectCTN_nonsplit, SelectCTN);
            }

            //공급설비
            string[][] 공급설비종류 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Cooling_ce_Form", "공급설비종류", "냉방시스템 = '" + CoolingNum + "'"); 
            int 공조개수 = 0;
            int 실내냉방개수 = 0;
            
            foreach (string[] 타입 in 공급설비종류)
            {
                if (타입[0] == "VAV유닛" || 타입[0] == "CAV유닛" || 타입[0] == "팬파워유닛")
                {
                    공조개수++;
                    if(공조개수 == 1)
                    {
                        CE1_ahu = 타입[0];
                    }
                    else if(공조개수 == 2)
                    {
                        CE2_ahu = 타입[0];
                    }  
                }
                else
                {
                    실내냉방개수++;
                    if (실내냉방개수 == 1)
                    {
                        CE1_z = 타입[0];
                    }
                    else if (실내냉방개수 == 2)
                    {
                        CE2_z = 타입[0];
                    }
                }
            }
                        
            string[][] 공급설비 = Program.DB.getValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호,공급설비종류,공급설비,가동시간,용량,소비전력", "냉방시스템 = '" + CoolingNum + "'");

            for (int k = 0; k < 공급설비.Length; k++)
            {
                CoolingCE ce = new CoolingCE();
                ce._zonenum = 공급설비[k][0];
                ce._cetype = 공급설비[k][1];
                ce._ceNum = 공급설비[k][2];
                if (공급설비[k][0] == "" || 공급설비[k][0] == null)
                {
                    ce._operhour = 0;
                    ce._cePower = 0;
                    ce._ceElec = 0;
                }
                else
                {
                    ce._operhour = Convert.ToInt32(공급설비[k][3]);
                    ce._cePower = Convert.ToDouble(공급설비[k][4]);
                    ce._ceElec = Convert.ToDouble(공급설비[k][5]);
                }
                SelectCE.Add(ce);
            }

        }

        public CoolPump PumpSlite(string _selectpump, CoolPump _pump)
        {
            List<string> pumpinfo = new List<string>();
            if (_selectpump != null && _selectpump != "")
            {
                Split(_selectpump, pumpinfo);
                _pump._pumpNum = pumpinfo[0];
                _pump._pumpname = pumpinfo[1];
                _pump._number = Convert.ToInt32(pumpinfo[2]);
                _pump._valve = pumpinfo[3];
                _pump._control = pumpinfo[4];
            }
            else return null;
            return _pump;
        }

        #region Cooling_Generator()
        public void Cooling_Generator()
        {
            Split(SelectCG_nonsplit, SelectCG);   //냉방유닛
            Split(SelectCGN_nonsplit, SelectCGN); //설치대수
            Split(SelectCGE_nonsplit, SelectCGE); //외기냉방
            if(SelectCGC_nonsplit !="" && SelectCGC_nonsplit != null) //제어방식
            {
                Split(SelectCGC_nonsplit, SelectCGC);
            }
                                
            for(int i = 0; i < SelectCG.Count; i++)
            {
                CoolingGeneratorMake CGM = CGfind(CG, SelectCG[i]);
                CGM._num = SelectCG[i];
                CGM._number = Convert.ToInt32(SelectCGN[i]);
                CGM._econo = SelectCGE[i];
                if(CG == nameof(_TYPE.공냉식냉동기))
                {
                    CGM._comp = SelectCGComp[i];
                }
                if(SelectCGC.Count > 0)
                {
                    CGM._control = SelectCGC[i];
                }
                CGM_Sum.Add(CGM);
            }

            //냉각탑 적용
            if (SelectCT.Count > 0)
            {
                for(int j =0;j<SelectCT.Count; j++)
                {
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_CoolingTop", "번호,명칭,형식,냉각능력,냉각수량,입구온도,출구온도,냉방전력소비계수,대기전력,설치,제어유형,팬유형", "번호 = '" + SelectCT[j] + "'");
                    CoolTop CT = new CoolTop();
                    CT._ctnum = Value[0][0];
                    CT._ctname = Value[0][1];
                    CT._cttype = Value[0][2].Substring(0, 3);
                    CT._ctmeth = Value[0][2].Substring(3, 2);
                    CT._power = Convert.ToDouble(Value[0][3]);
                    CT._quantity = Convert.ToDouble(Value[0][4]);
                    CT._cswin = Convert.ToDouble(Value[0][5]);
                    CT._cswout = Convert.ToDouble(Value[0][6]);
                    CT._phr_el = Convert.ToDouble(Value[0][7]);
                    CT._pctrl_el = Convert.ToDouble(Value[0][8]);
                    CT._number = Convert.ToInt32(SelectCTN[j]);
                    CT._install = Value[0][9];
                    CT._ctctrl = Value[0][10];
                    CT._ctfan = Value[0][11];
                    CT_Sum.Add(CT);
                }
            }
        }

        public CoolingGeneratorMake CGfind(string _CG, string _SelectCG)
        {
            CoolingGeneratorMake CGM = new CoolingGeneratorMake();
            switch (CG)
            {
                case nameof(_TYPE.실외기12kW):
                    string[][] DefaultValue = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", "냉방정격용량,냉방정격COP,대기전력,연료,설치", "번호 = '" + _SelectCG + "'");
                    CGM._install = DefaultValue[0][4];
                    CGM._power = Convert.ToDouble(DefaultValue[0][0]);
                    if (CGM._install == "기존")
                    {
                        CGM._eer = Convert.ToDouble(DefaultValue[0][1]) * 0.9; //10%성능저하 적용
                    }
                    else CGM._eer = Convert.ToDouble(DefaultValue[0][1]);
                    CGM._pctrl = Convert.ToDouble(DefaultValue[0][2]);
                    CGM._fuel = DefaultValue[0][3];
                    CGM._cout = "직팽식";
                    break;
                case nameof(_TYPE.공냉식냉동기):
                    string[][] 공냉식1 = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", "냉방정격용량,냉방정격COP,대기전력,연료,설치", "번호 = '" + _SelectCG + "'");
                    if(공냉식1.Length > 0)
                    {
                        CGM._install = 공냉식1[0][4];
                        CGM._power = Convert.ToDouble(공냉식1[0][0]);
                        if (CGM._install == "기존")
                        {
                            CGM._eer = Convert.ToDouble(공냉식1[0][1]) * 0.9;
                        }
                        else CGM._eer = Convert.ToDouble(공냉식1[0][1]);
                        CGM._pctrl = Convert.ToDouble(공냉식1[0][2]);
                        CGM._cwin = 0;
                        CGM._cwout = 0;
                        CGM._fuel = 공냉식1[0][3];
                        CGM._cout = "직팽식";
                    }
                    else
                    {
                        string[][] 공냉식2 = Program.DB.getValue(DB.type.ProjDB, "User_AirCooler", "냉방출력,EERP,대기전력,연료,설치,증발기", "번호 = '" + _SelectCG + "'");
                        CGM._install = 공냉식2[0][4];
                        CGM._power = Convert.ToDouble(공냉식2[0][0]);
                        if(CGM._install == "기존")
                        {
                            CGM._eer = Convert.ToDouble(공냉식2[0][1])*0.9;
                        }
                        else CGM._eer = Convert.ToDouble(공냉식2[0][1]);
                        CGM._pctrl = Convert.ToDouble(공냉식2[0][2]);
                        CGM._fuel = 공냉식2[0][3];
                        CGM._install = 공냉식2[0][4];
                        if (공냉식2[0][5] == "직팽식")
                        {
                            CGM._cwin = 0;
                            CGM._cwout = 0;
                            CGM._cout = "직팽식";
                        }
                        else if(공냉식2[0][5] == "수방식")
                        {
                            string[][] 공냉식3 = Program.DB.getValue(DB.type.ProjDB, "User_AirCooler", "냉수입구온도,냉수출구온도", "번호 = '" + _SelectCG + "'");
                            CGM._cwin = Convert.ToDouble(공냉식3[0][0]);
                            CGM._cwout = Convert.ToDouble(공냉식3[0][1]);
                            CGM._cout = "수방식";
                        }
                    }         
                    break;
                case nameof(_TYPE.수냉식냉동기):
                    string[][] 수냉식 = Program.DB.getValue(DB.type.ProjDB, "User_WaterCooler", "냉방출력,EER,대기전력,냉수입구온도,냉수출구온도,압축기,연료,설치", "번호 = '" + _SelectCG + "'");
                    CGM._install = 수냉식[0][7];
                    CGM._power = Convert.ToDouble(수냉식[0][0]);
                    if(CGM._install == "기존")
                    {
                        CGM._eer = Convert.ToDouble(수냉식[0][1]) * 0.9;
                    }
                    else CGM._eer = Convert.ToDouble(수냉식[0][1]);
                    CGM._pctrl = Convert.ToDouble(수냉식[0][2]);
                    CGM._cwin = Convert.ToDouble(수냉식[0][3]);
                    CGM._cwout = Convert.ToDouble(수냉식[0][4]);
                    CGM._comp = 수냉식[0][5];
                    CGM._fuel = 수냉식[0][6];
                    CGM._cout = "수방식";
                    break;
                case nameof(_TYPE.지열히트펌프):
                    string[][] 지열 = Program.DB.getValue(DB.type.ProjDB, "User_GroundHP", "냉방용량,냉방EER,대기전력,냉수입구온도,냉수출구온도,압축기,연료,설치,공급유형", "번호 = '" + _SelectCG + "'");
                    CGM._install = 지열[0][7];
                    CGM._power = Convert.ToDouble(지열[0][0]);
                    if (CGM._install == "기존")
                    {
                        CGM._eer = Convert.ToDouble(지열[0][1]) * 0.9;
                    }
                    else CGM._eer = Convert.ToDouble(지열[0][1]);
                    CGM._pctrl = Convert.ToDouble(지열[0][2]);
                    CGM._cwin = Convert.ToDouble(지열[0][3]);
                    CGM._cwout = Convert.ToDouble(지열[0][4]);
                    CGM._comp = 지열[0][5];
                    CGM._fuel = 지열[0][6];
                    CGM._cout = 지열[0][8];
                    break;
                case nameof(_TYPE.지하수히트펌프):
                    string[][] 지하수 = Program.DB.getValue(DB.type.ProjDB, "User_GroundWHP", "냉방용량,냉방EER,대기전력,냉수입구온도,냉수출구온도,압축기,연료,설치,공급유형", "번호 = '" + _SelectCG + "'");
                    CGM._install = 지하수[0][7];
                    CGM._power = Convert.ToDouble(지하수[0][0]);
                    if (CGM._install == "기존")
                    {
                        CGM._eer = Convert.ToDouble(지하수[0][1]) * 0.9;
                    }
                    else CGM._eer = Convert.ToDouble(지하수[0][1]);
                    CGM._pctrl = Convert.ToDouble(지하수[0][2]);
                    CGM._cwin = Convert.ToDouble(지하수[0][3]);
                    CGM._cwout = Convert.ToDouble(지하수[0][4]);
                    CGM._comp = 지하수[0][5];
                    CGM._fuel = 지하수[0][6];
                    CGM._cout = 지하수[0][8];
                    break;
                case nameof(_TYPE.흡수식냉동기):
                    string[][] 흡수식 = Program.DB.getValue(DB.type.ProjDB, "User_ABS", "냉방용량,냉방성능,대기전력,냉수입구온도,냉수출구온도,연료,설치", "번호 = '" + _SelectCG + "'");
                    CGM._install = 흡수식[0][6];
                    CGM._power = Convert.ToDouble(흡수식[0][0]);
                    if (CGM._install == "기존")
                    {
                        CGM._eer = Convert.ToDouble(흡수식[0][1]) * 0.9;
                    }
                    else CGM._eer = Convert.ToDouble(흡수식[0][1]);
                    CGM._pctrl = Convert.ToDouble(흡수식[0][2]);
                    CGM._cwin = Convert.ToDouble(흡수식[0][3]);
                    CGM._cwout = Convert.ToDouble(흡수식[0][4]);
                    CGM._comp = null;
                    CGM._fuel = 흡수식[0][5];
                    break;
            }
            return CGM;
        }
        #endregion
        public void Generator_Sum()
        {
            //설비항목 종합
            Power_f = 0;
            Pctrl_f = 0;
            EER_f = 0;
            Number_f = 0;
            CWin = 0; CWout = 0;
            foreach (CoolingGeneratorMake CGM in CGM_Sum)
            {
                Power_f += CGM._power * CGM._number; //kW
                Pctrl_f += CGM._pctrl * CGM._number; //W
                Number_f += CGM._number;
                Power.Add(CGM._power * CGM._number);
                EER_f += CGM._eer * CGM._number * CGM._power;
                Comp.Add(CGM._comp);
                if(CGM._cwin !=null && CGM._cwin != 0)
                {
                    CWin += CGM._cwin * CGM._number * CGM._power;
                    CWout += CGM._cwout * CGM._number * CGM._power;
                }
            }

            for (int i = 0; i < Power.Count; i++)
            {
                if (Power.Max() == Power[i])
                {
                    Carrier = CGM_Sum[i]._fuel;
                    Control_f = CGM_Sum[i]._control;
                    Econo_f = CGM_Sum[i]._econo;
                    Comp_f = CGM_Sum[i]._comp;
                    Cout = CGM_Sum[i]._cout;
                }
            }
            EER_f = EER_f / Power_f;
            if (CWin >0)
            {
                CWin = CWin / Power_f /Number_f;
                CWout = CWout / Power_f / Number_f;
            }

            //냉각탑 적용시
            if (CT_Sum.Count > 0)
            {
                CSWin = 0; CSWout = 0; CTPower_f = 0; CTfhrPL_f = 0; CTPhrel_f = 0; CTPctrlel_f = 0; CTNum_f = 0;
                List<double> pow = new List<double>();
                foreach (CoolTop CT in CT_Sum)
                {
                    CSWin += CT._power * CT._number * CT._cswin;
                    CSWout += CT._power * CT._number * CT._cswout;
                    CTPower_f += CT._power * CT._number;
                    CTPhrel_f += CT._phr_el * CT._number;
                    CTPctrlel_f += CT._pctrl_el * CT._number;
                    CTNum_f += CT._number;
                    pow.Add(CT._power);
                }
                CSWin = CSWin / CTPower_f * CTNum_f;
                CSWout = CSWout / CTPower_f * CTNum_f;
                foreach (CoolTop CT in CT_Sum)
                {
                    if (pow.Max() == CT._power)
                    {
                        CTControl_f = CT._ctctrl; //제어유형
                        CTmeth_f = CT._ctmeth;//밀폐개방
                        CTtype_f = CT._cttype;//건습식
                        CTFanType = CT._ctfan;//팬유형
                    }
                }
                string[][] 부분부하계수 = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉각탑", "부분부하계수"," 방식 = '" + CTtype_f + "' And 유형 = '"+ CTmeth_f + "' And 팬 = '" + CTFanType + "' And 제어유형 = '" + CTControl_f + "'");
                CTfhrPL_f = Convert.ToDouble(부분부하계수[0][0]);
            }

            //지열히트펌프 및 지하수히트펌프인경우
            if(CG == nameof(_TYPE.지열히트펌프) || CG == nameof(_TYPE.지하수히트펌프))
            {
                CSWin = 30; //향후 KS기준 적용필요
                CSWout = 25; //향후 KS기준 적용필요
            }

        }

        public void Cal_CLRate()//공급설비기준 요구량 반영을 위해 부하율을 적용함, 공조기 부분은 공조기에서 완료한 후 가져오기 때문에 100%로 적용함
        {
            //존에 해당하는 부하율을 입력함 
            for (int i = 0; i < SelectZone.Count; i++) //존번호별 계산
            {
                double sum = 0, val = 0;

                for (int h = 0; h < SelectCE.Count; h++) // 5개항목이 있다면
                {
                    if (SelectZone[i] == SelectCE[h]._zonenum && SelectCE[h]._type(SelectCE[h]._cetype) != "공조기") //7개와 12개
                    {
                        sum += SelectCE[h]._cePower * SelectCE[h]._operhour;
                    }
                }
                for (int k = 0; k < SelectCE.Count; k++)
                {
                    if(SelectZone[i] == SelectCE[k]._zonenum && SelectCE[k]._type(SelectCE[k]._cetype) != "공조기")
                    {
                        val = SelectCE[k]._cePower * SelectCE[k]._operhour / sum;
                        Program.DB.setValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호,공급설비,냉방시스템,부하율", "'" + SelectZone[i] + "','" + SelectCE[k]._ceNum + "','" + CoolingNum + "','" + val + "'", "존번호,공급설비,냉방시스템");
                    }
                }            
            }
            ZoneNumber_f = SelectZone.Count;
            if (ZoneNumber_f == 1)
            {
                SCZoneType_z = "단일존";
            }
            else if (ZoneNumber_f > 1) SCZoneType_z = "멀티존";
            else SCZoneType_z = null;

            //공조기에 대한 부하율 입력

            for (int i = 0; i < SelectZone.Count; i++) //존번호별 계산
            {
                double sum = 0, val = 0;

                for (int h = 0; h < SelectCE.Count; h++) // 5개항목이 있다면
                {
                    if (SelectZone[i] == SelectCE[h]._zonenum && SelectCE[h]._type(SelectCE[h]._cetype) == "공조기") //7개와 12개
                    {
                        sum += SelectCE[h]._cePower * SelectCE[h]._operhour;
                    }
                }
                for (int k = 0; k < SelectCE.Count; k++)
                {
                    if (SelectZone[i] == SelectCE[k]._zonenum && SelectCE[k]._type(SelectCE[k]._cetype) == "공조기")
                    {
                        val = SelectCE[k]._cePower * SelectCE[k]._operhour / sum;
                        Program.DB.setValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호,공급설비,냉방시스템,부하율", "'" + SelectZone[i] + "','" + SelectCE[k]._ceNum + "','" + CoolingNum + "','" + val + "'", "존번호,공급설비,냉방시스템");
                    }
                }
            }
            AhuNumber_f = SelectAhu.Count;
            if (AhuNumber_f == 1)
            {
                SCZoneType_ahu = "단일존";
            }
            else if (AhuNumber_f > 1) SCZoneType_ahu = "멀티존";
            else SCZoneType_ahu = null;
        }

        #region  존계산
        public void Cal_Zone() // 여러존 정보작성
        {
            if (ZoneNameList.Count > 0)
            {
                Cal_ZoneSum();
                Cal_CED_Z();
                Cal_S_Z();
                Cal_Oper_Z();
                Cal_fPL("Z");
            }
            
            else
            {
                QC_a_z = 0;
                for (int i = 0; i < 12; i++)
                {
                    QC_nd_z[i] = 0;
                    dwd_z[i] = 0;
                    theta_z[i] = 0;
                    QC_ce_z[i] = 0;
                    QC_d_z[i] = 0;
                    QC_s_z[i] = 0;
                    QC_out_z[i] = 0;
                    tC_op_z[i] = 0;
                }
            }
        }

        public void Cal_ZoneAhu() //최대부하,평균일일시간,총면적
        {
            //존산정
            Qc_max_z = 0;
            
            if (SelectZone.Count > 0)
            {
                for (int j = 0; j < SelectZone.Count; j++)
                {
                    zonemake zoneinfo = new zonemake(SelectZone[j], CoolingNum);
                    ZoneNameList.Add(zoneinfo);
                }
                foreach (zonemake value in ZoneNameList)
                {
                    Qc_max_z += value.QC_max / 1000;
                    top_z += value.tC_op * value.QC_max / 1000;
                    A_z += value.Anf;
                }
                
                top_z = top_z / Qc_max_z; //가동시간을 최대부하가중
            }
            else
            {
                top_z = 0;
                A_z = 0;
            }
            //공조기산정
            Qc_max_ahu = 0;
            
            if (SelectAhu.Count > 0)
            {
                for (int j = 0; j < SelectAhu.Count; j++)
                {
                    ahumake ahuinfo = new ahumake(SelectAhu[j], CoolingNum);
                    AhuNameList.Add(ahuinfo);
                }
                foreach (ahumake value in AhuNameList)
                {
                    Qc_max_ahu += value.QC_max / 1000;
                    top_ahu += value.tC_op * value.QC_max / 1000;
                    A_ahu += value.Anf;
                }
                top_ahu = top_ahu / Qc_max_ahu; //가동시간을 최대부하가중
            }
            else
            {
                top_ahu = 0;
                A_ahu = 0;
            }
        }


        public void Cal_ZoneSum() //연간냉방에너지요구량,월이용일수,월실내온도,월냉방에너지요구량
        {
            double[] dwd_sum = new double[12];
            double[] theta_sum = new double[12];
            QC_a_z = 0;
           
            for (int i = 0; i < 12; i++)
            {
                foreach (zonemake value in ZoneNameList)
                {
                    QC_nd_z[i] += value.Q_nd[i]; //공급설비 부하율을 반영한 요구량 산정
                }
                if (QC_nd_z[i] == 0)
                {
                    foreach (zonemake value in ZoneNameList)
                    {
                        dwd_sum[i] += value.dwd[i] * value.Anf;
                        theta_sum[i] += value.θi_c[i] * value.Anf;
                    }
                    dwd_z[i] = dwd_sum[i] / A_z; //요구량이 없으므로 면적가중으로 산정함
                    theta_z[i] = theta_sum[i] / A_z; //요구량이 없으므로 면적가중으로 산정함
                }
                else
                {
                    foreach (zonemake value in ZoneNameList)
                    {
                        dwd_sum[i] += value.dwd[i] * value.Q_nd[i]; // 요구량 가중하여 산정함
                        theta_sum[i] += value.θi_c[i] * value.Q_nd[i]; //요구량 가중하여 산정함
                    }
                    dwd_z[i] = dwd_sum[i] / QC_nd_z[i];
                    theta_z[i] = theta_sum[i] / QC_nd_z[i];
                }
                QC_a_z += QC_nd_z[i];
            }
           
        }

        public void Cal_CED_Z()
        {

            if (Cout == "직팽식")
            {
                string[][] value = Program.DB.getValue(DB.type.BaseDB_Cooling, "공급분배손실계수", "nc_ce_sens,nc_ce,nc_d", " 공급온도 = '" + Cout + "' And 공급유형 = '냉방존'");
                nc_ce_sens = Convert.ToDouble(value[0][0]);
                nc_ce = Convert.ToDouble(value[0][1]);
                nc_d = Convert.ToDouble(value[0][2]);
            }
            else
            {
                if (CWout <= 6)
                {
                    nc_ce_sens = 0.87;
                    nc_ce = 1;
                    nc_d = 0.9;
                }
                else if (CWout < 14)
                {
                    nc_ce_sens = 0.87 + (1 - 0.87) / (14 - 6) * (CWout - 6);
                    nc_ce = 1;
                    nc_d = 0.9 + (1 - 0.9) / (14 - 6) * (CWout - 6);
                }
                else
                {
                    nc_ce_sens = 1;
                    nc_ce = 1;
                    nc_d = 1;
                }

            }

            for (int i = 0; i < 12; i++)
            {
                QC_ce_z[i] = ((1 - nc_ce_sens) + (1 - nc_ce)) * QC_nd_z[i];
                QC_d_z[i] = (1 - nc_d) * QC_nd_z[i];
            }
        }

        public void Cal_S_Z()
        {
            string[][] value = Program.DB.getValue(DB.type.BaseDB_Cooling, "저장제어운영계수", "이용계수", " 항목= '" + Sto_Tank + "' And 종류 = '" + Sto_Type + "'");

            if (value.Length > 0)
            {
                nc_s = Convert.ToDouble(value[0][0]);
                for (int i = 0; i < 12; i++)
                {
                    QC_s_z[i] = (1 - nc_s) * QC_nd_z[i];
                    QC_out_z[i] = QC_nd_z[i] + QC_ce_z[i] + QC_d_z[i] + QC_s_z[i];
                }
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    QC_s_z[i] = 0;
                    QC_out_z[i] = QC_nd_z[i] + QC_ce_z[i] + QC_d_z[i] + QC_s_z[i];
                }
            }
        }

        public void Cal_Oper_Z() //냉방존 계산
        {
            //냉방존 부하율 검토
            QC_p_z = Power_f * Qc_max_z / (Qc_max_ahu + Qc_max_z);

            if (Econo_f == "있음")
            {
                Beta_grenz_z = 0.6;
            }
            else Beta_grenz_z = 0.3;

            if (CG == "실외기12kW")
            {
                Cal_BC_z();
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    tmth_z[i] = top_z * dwd_z[i]; //냉방이용시간 작성
                    if (QC_nd_z[i] == 0)
                    {
                        BC_z[i] = 0;
                    }
                    else BC_z[i] = QC_out_z[i] / (QC_p_z * tmth_z[i]);

                    if (BC_z[i] <= Beta_grenz_z)
                    {
                        tC_op_z[i] = tmth_z[i] * BC_z[i] / Beta_grenz_z;
                    }
                    else tC_op_z[i] = tmth_z[i]; //냉방작동시간 작성
                }
            }
            

        }

        public void Cal_BC_z() //냉방존 계산
        {
            double[] 외기 = new double[12];
            string[][] 지역 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
            string[][] OutTemp = Program.DB.getValue(DB.type.BaseDB_HCneed, " 기후데이터_온도습도", "온도", "지역명 = '" + 지역[0][0] + "'"); //기후데이터 저장
            string[][] check = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉동기설치위치", "상승온도차", "위치 = '" + CSource + "'");
            for (int i = 0; i < 12; i++)
            {
                double x0 = 0, x1 = 0, y0 = 0, y1 = 0;

                외기[i] = Convert.ToDouble(OutTemp[i][0]) + Convert.ToDouble(check[0][0]);
                if (외기[i] >= 32) BC_z[i] = 1.0;
                else if (외기[i] >= 29.8)
                {
                    x0 = 29.8; x1 = 32; y0 = 0.9; y1 = 1.0;
                    BC_z[i] = Math.Min(1,y0 + (y1 - y0) / (x1 - x0) * (외기[i] - x0));
                }
                else if (외기[i] >= 27.6)
                {
                    x0 = 27.6; x1 = 29.8; y0 = 0.8; y1 = 0.9;
                    BC_z[i] = y0 + (y1 - y0) / (x1 - x0) * (외기[i] - x0);
                }
                else if (외기[i] >= 25.3)
                {
                    x0 = 25.3; x1 = 27.6; y0 = 0.7; y1 = 0.8;
                    BC_z[i] = y0 + (y1 - y0) / (x1 - x0) * (외기[i] - x0);
                }
                else if (외기[i] >= 23.1)
                {
                    x0 = 23.1; x1 = 25.3; y0 = 0.6; y1 = 0.7;
                    BC_z[i] = y0 + (y1 - y0) / (x1 - x0) * (외기[i] - x0);
                }
                else if (외기[i] >= 20.9)
                {
                    x0 = 20.9; x1 = 23.1; y0 = 0.5; y1 = 0.6;
                    BC_z[i] = y0 + (y1 - y0) / (x1 - x0) * (외기[i] - x0);
                }
                else if (외기[i] >= 18.7)
                {
                    x0 = 18.7; x1 = 20.9; y0 = 0.4; y1 = 0.5;
                    BC_z[i] = y0 + (y1 - y0) / (x1 - x0) * (외기[i] - x0);
                }
                else if (외기[i] >= 16.5)
                {
                    x0 = 16.5; x1 = 18.7; y0 = 0.3; y1 = 0.4;
                    BC_z[i] = y0 + (y1 - y0) / (x1 - x0) * (외기[i] - x0);
                }
                else if (외기[i] >= 14.2)
                {
                    x0 = 14.2; x1 = 16.5; y0 = 0.2; y1 = 0.3;
                    BC_z[i] = y0 + (y1 - y0) / (x1 - x0) * (외기[i] - x0);
                }
                else if (외기[i] >= 12.0)
                {
                    x0 = 12.0; x1 = 14.2; y0 = 0.1; y1 = 0.2;
                    BC_z[i] = y0 + (y1 - y0) / (x1 - x0) * (외기[i] - x0);
                }
                else BC_z[i] = 0.1;
            }
        }

        #endregion

        #region //공조존 계산
        public void Cal_Ahu() // 여러공조기 정보작성
        {
            if (AhuNameList.Count > 0)
            {
                Cal_AhuSum();
                Cal_CED_Ahu();
                Cal_S_Ahu();
                Cal_Oper_Ahu(); //작동시간
                Cal_fPL("Ahu");
            }
            else
            {
                QC_a_ahu = 0;
                for (int i=0; i < 12; i++)
                {
                    QC_nd_ahu[i] = 0;
                    dwd_ahu[i] = 0;
                    theta_ahu[i] = 0;
                    QC_ce_ahu[i] = 0;
                    QC_d_ahu[i] = 0;
                    QC_s_ahu[i] = 0;
                    QC_out_ahu[i] = 0;
                    tC_op_ahu[i] = 0;
                }
                Install_ahu = null;
            }
            
        }
        public void Cal_AhuSum() //연간냉방에너지요구량,월이용일수,월실내온도,월냉방에너지요구량,설치위치
        {
            double[] dwd_sum = new double[12];
            double[] theta_sum = new double[12];
            QC_a_ahu = 0;

            for (int i = 0; i < 12; i++)
            {
                foreach (ahumake value in AhuNameList)
                {
                    QC_nd_ahu[i] += value.Q_nd[i]; //공급설비 부하율을 반영한 요구량 산정
                }
                if (QC_nd_ahu[i] == 0)
                {
                    foreach (ahumake value in AhuNameList)
                    {
                        dwd_sum[i] += value.dwd[i] * value.Anf;
                        theta_sum[i] += value.θi_c[i] * value.Anf;
                    }
                    dwd_ahu[i] = dwd_sum[i] / A_ahu; //요구량이 없으므로 면적가중으로 산정함
                    theta_ahu[i] = theta_sum[i] / A_ahu; //요구량이 없으므로 면적가중으로 산정함
                }
                else
                {
                    foreach (ahumake value in AhuNameList)
                    {
                        dwd_sum[i] += value.dwd[i] * value.Q_nd[i]; // 요구량 가중하여 산정함
                        theta_sum[i] += value.θi_c[i] * value.Q_nd[i]; //요구량 가중하여 산정함
                    }
                    dwd_ahu[i] = dwd_sum[i] / QC_nd_ahu[i];
                    theta_ahu[i] = theta_sum[i] / QC_nd_ahu[i];
                }
                QC_a_ahu += QC_nd_ahu[i];
            }
            List<double> pow = new List<double>();
            foreach (ahumake value in AhuNameList)
            {
              pow.Add(value.QC_max); //공급설비 부하율을 반영한 요구량 산정
            }
            foreach(ahumake value in AhuNameList)
            {
                if(pow.Max() == value.QC_max)
                {
                    Install_ahu = value.Install;
                }
            }
        }

        public void Cal_CED_Ahu() 
        {
            if (Cout == "직팽식")
            {
                string[][] value = Program.DB.getValue(DB.type.BaseDB_Cooling, "공급분배손실계수", "nc_ce_sens,nc_ce,nc_d", " 공급온도 = '" + Cout + "' And AHU설치위치 = '" + Install_ahu + "' And 공급유형 = 'AHU'");
                nc_ce_sens = Convert.ToDouble(value[0][0]);
                nc_ce = Convert.ToDouble(value[0][1]);
                nc_d = Convert.ToDouble(value[0][2]);
            }
            else
            {
                int x0 = 0, x1 = 0;
                double t0 = 0, t1 = 0, y0 = 0, y1 = 0, u0 = 0, u1 = 0;
                
                if (CWout <= 6)
                {
                    x0 = 6;
                    x1 = 6;
          
                }
                else if (CWout <= 14)
                {
                    x0 = 6;
                    x1 = 14;
                }
                else if (CWout <= 16)
                {
                    x0 = 14;
                    x1 = 16;
                }
                else
                {
                    x0 = 20;
                    x1 = 20;
                }
                t0 = ced_valuefind(x0, Install_ahu)[0];
                y0 = ced_valuefind(x0, Install_ahu)[1];
                u0 = ced_valuefind(x0, Install_ahu)[2];
                t1 = ced_valuefind(x1, Install_ahu)[0];
                y1 = ced_valuefind(x1, Install_ahu)[1];
                u1 = ced_valuefind(x1, Install_ahu)[2];
                
                nc_ce_sens = t0 + (t1 - t0) / (x1 - x0) * (CWout - x0);
                nc_ce = y0 + (y1 - y0) / (x1 - x0) * (CWout - x0);
                nc_d = u0 + (u1 - u0) / (x1 - x0) * (CWout - x0);
            }
            
            for (int i = 0; i < 12; i++)
            {
                QC_ce_ahu[i] = ((1 - nc_ce_sens) + (1 - nc_ce)) * QC_nd_ahu[i];
                QC_d_ahu[i] = (1 - nc_d) * QC_nd_ahu[i];
            }

        }
        public double[] ced_valuefind(int 공급온도, string _install)
        {
            
            double[] ced = new double[3];
            string[][] value = Program.DB.getValue(DB.type.BaseDB_Cooling, "공급분배손실계수", "nc_ce_sens,nc_ce,nc_d", " 공급온도 = '" + 공급온도 + "' And AHU설치위치 = '" + _install + "' And 공급유형 = 'AHU'");
            ced[0] = Convert.ToDouble(value[0][0]);
            ced[1] = Convert.ToDouble(value[0][1]);
            ced[2] = Convert.ToDouble(value[0][2]);
            return ced;
        }

        public void Cal_S_Ahu()//완료
        {
            string check;

            if (CG == "실외기12kW")
            {
                check = SCZoneType_ahu;
            }
            else check = Comp_f;

            string[][] number = Program.DB.getValue(DB.type.BaseDB_Cooling, "부분부하계수", "번호", " 설비유형= '" + CG + "' And 제어유형 = '" + Control_f + "' And 공급유형 = '" + check + "'");
            ArtNumber = number[0][0];
            
            string[][] value = Program.DB.getValue(DB.type.BaseDB_Cooling, "저장제어운영계수", "이용계수", " 항목= '" + Sto_Tank + "' And 종류 = '" + Sto_Type + "' And 번호='"+ ArtNumber +"'");

            if (value.Length > 0)
            {
                nc_s = Convert.ToDouble(value[0][0]);
                for (int i = 0; i < 12; i++)
                {
                    QC_s_ahu[i] = (1 - nc_s) * QC_nd_ahu[i];
                    QC_out_ahu[i] = QC_nd_ahu[i] + QC_ce_ahu[i] + QC_d_ahu[i] + QC_s_ahu[i];
                }
                
            }

            else
            {
                for (int i = 0; i < 12; i++)
                {
                    QC_s_ahu[i] = 0;
                    QC_out_ahu[i] = QC_nd_ahu[i] + QC_ce_ahu[i] + QC_d_ahu[i] + QC_s_ahu[i];
                }
            }
        }
        public void Cal_Oper_Ahu() //완료
        {
            //냉방존 부하율 검토
            QC_p_ahu = Power_f * Qc_max_ahu / (Qc_max_ahu + Qc_max_z); //공조기 부분의 출력값
            
            if (Econo_f == "있음")
            {
                Beta_grenz_ahu = 0.6;
            }
            else Beta_grenz_ahu = 0.3;

            if (CG == "실외기12kW")
            {
                Cal_BC_ahu();
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    tmth_ahu[i] = top_ahu * dwd_ahu[i]; //냉방이용시간 작성
                    if (QC_nd_ahu[i] == 0)
                    {
                        BC_ahu[i] = 0;
                    }
                    else BC_ahu[i] = QC_out_ahu[i] / (QC_p_ahu * tmth_ahu[i]);

                    if (BC_ahu[i] <= Beta_grenz_ahu)
                    {
                        tC_op_ahu[i] = tmth_ahu[i] * BC_ahu[i] / Beta_grenz_ahu;
                    }
                    else tC_op_ahu[i] = tmth_ahu[i]; //냉방작동시간 작성
                }
            }
        }

        public void Cal_BC_ahu() //냉방존 부하율 계산
        {
            double[] 외기 = new double[12];
            string[][] 지역 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
            string[][] OutTemp = Program.DB.getValue(DB.type.BaseDB_HCneed, " 기후데이터_온도습도", "온도", "지역명 = '" + 지역[0][0] + "'"); //기후데이터 저장
            string[][] check = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉동기설치위치", "상승온도차", "위치 = '" + CSource + "'");
            for (int i = 0; i < 12; i++)
            {
                double x0 = 0, x1 = 0, y0 = 0, y1 = 0;

                외기[i] = Convert.ToDouble(OutTemp[i][0]) + Convert.ToDouble(check[0][0]);
                if (외기[i] >= 32) BC_ahu[i] = 1.0;
                else if (외기[i] >= 29.8)
                {
                    x0 = 29.8; x1 = 32; y0 = 0.9; y1 = 1.0;
                    BC_ahu[i] = y0 + (y1 - y0) / (x1 - x0) * (외기[i] - x0);
                }
                else if (외기[i] >= 27.6)
                {
                    x0 = 27.6; x1 = 29.8; y0 = 0.8; y1 = 0.9;
                    BC_ahu[i] = y0 + (y1 - y0) / (x1 - x0) * (외기[i] - x0);
                }
                else if (외기[i] >= 25.3)
                {
                    x0 = 25.3; x1 = 27.6; y0 = 0.7; y1 = 0.8;
                    BC_ahu[i] = y0 + (y1 - y0) / (x1 - x0) * (외기[i] - x0);
                }
                else if (외기[i] >= 23.1)
                {
                    x0 = 23.1; x1 = 25.3; y0 = 0.6; y1 = 0.7;
                    BC_ahu[i] = y0 + (y1 - y0) / (x1 - x0) * (외기[i] - x0);
                }
                else if (외기[i] >= 20.9)
                {
                    x0 = 20.9; x1 = 23.1; y0 = 0.5; y1 = 0.6;
                    BC_ahu[i] = y0 + (y1 - y0) / (x1 - x0) * (외기[i] - x0);
                }
                else if (외기[i] >= 18.7)
                {
                    x0 = 18.7; x1 = 20.9; y0 = 0.4; y1 = 0.5;
                    BC_ahu[i] = y0 + (y1 - y0) / (x1 - x0) * (외기[i] - x0);
                }
                else if (외기[i] >= 16.5)
                {
                    x0 = 16.5; x1 = 18.7; y0 = 0.3; y1 = 0.4;
                    BC_ahu[i] = y0 + (y1 - y0) / (x1 - x0) * (외기[i] - x0);
                }
                else if (외기[i] >= 14.2)
                {
                    x0 = 14.2; x1 = 16.5; y0 = 0.2; y1 = 0.3;
                    BC_ahu[i] = y0 + (y1 - y0) / (x1 - x0) * (외기[i] - x0);
                }
                else if (외기[i] >= 12.0)
                {
                    x0 = 12.0; x1 = 14.2; y0 = 0.1; y1 = 0.2;
                    BC_ahu[i] = y0 + (y1 - y0) / (x1 - x0) * (외기[i] - x0);
                }
                else BC_ahu[i] = 0.1;
            }
        }


        #endregion

        public void Find_Climate()//기후데이터
        {
            double Ref = 0.95;
            double[] 외기온도 = new double[12];
            double[] 수직지열온도 = { 4.4, 3.9, 6.5, 10.6, 15.1, 18.7, 20.8, 21, 19.2, 15.6, 10.6, 5 };
            string[][] 지역 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
            string[][] OutdoorClimate = Program.DB.getValue(DB.type.BaseDB_HCneed, " 기후데이터_온도습도", "온도,상대습도,기간", "지역명 = '" + 지역[0][0] + "'"); //기후데이터 저장
            for (int i = 0; i < OutdoorTemperature.Length; i++)
            {
                외기온도[i] = Convert.ToDouble(OutdoorClimate[i][0]);
                Humidity[i] = Convert.ToDouble(OutdoorClimate[i][1]);
                WetTemperature[i] = -5.809 + 0.058 * Ref * 100 + 0.697 * 외기온도[i] + 0.003 * Ref * 외기온도[i] * 100;
                mth[i] = OutdoorClimate[i][2];
            }
            string[][] v = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉동기설치위치", "상승온도차", "위치 = '" + CSource + "'");
            if (v.Length > 0)
            {
                Theta_Around = Convert.ToDouble(v[0][0]);
            }else Theta_Around = 0;

            for (int j = 0; j < 12; j++)
            {
                if (CG == nameof(_TYPE.지열히트펌프))
                {
                    OutdoorTemperature[j] = 수직지열온도[j];
                }
                else if (CG == nameof(_TYPE.지하수히트펌프))
                {
                    OutdoorTemperature[j] = 0.134 * 외기온도[j] + 9.32;
                }
                else OutdoorTemperature[j] = 외기온도[j] + Theta_Around;
            } 
        }

        public string[] type(string type) //종류,번호,fC_M 지정
        {
            string[] v = new string[3];
            string[][] value = Program.DB.getValue(DB.type.BaseDB_Cooling, " 부분부하계수", "종류,번호", "설비유형 = '" + CG + "' And 제어유형 = '" + Control_f + "' And 공급유형 = '" + type + "'");
            v[0] = value[0][0]; //ArtType
            v[1] = value[0][1]; //ArtNumber
            
            return v;
        }

        public void Cal_fPL(string Type) //부하율, 종류, 번호
        {
            string 공급유형 = null;
            
            if (Type == "Z")
            {
                if (CG == "실외기12kW")
                {
                    공급유형 = SCZoneType_z;
                }
                else if (CG == "공냉식냉동기" || CG == "수냉식냉동기" || CG == "지열히트펌프")//그외의 경우(공냉식,수냉식등)
                {
                    공급유형 = Comp_f;
                }
                string[][] val = Program.DB.getValue(DB.type.BaseDB_Cooling, "부분부하계수", "P1,P2,P3,P4,P5,P6,P7,P8,P9,P10", " 설비유형= '" + CG + "' And 제어유형 = '" + Control_f + "' And 공급유형 = '" + 공급유형 + "'");
                for (int i = 0; i < 12; i++)
                {
                    double B2 = 0.15;
                    for (int h = 0; h < 10; h++)
                    {
                        if (BC_z[i] < 0.05)
                        {
                            fC_PL_z[i] = 1;
                            break;
                        }
                        else if (BC_z[i] < B2)
                        {
                            fC_PL_z[i] = Convert.ToDouble(val[0][h]);
                            break;
                        }
                        else B2 = B2 + 0.1;
                    }
                }    
            }
            else if(Type == "Ahu")
            {
                if (CG == "실외기12kW")
                {
                    공급유형 = SCZoneType_ahu;
                }
                else if (CG == "공냉식냉동기" || CG == "수냉식냉동기" || CG == "지열히트펌프")//그외의 경우(공냉식,수냉식등)
                {
                    공급유형 = Comp_f;
                }
                string[][] val = Program.DB.getValue(DB.type.BaseDB_Cooling, "부분부하계수", "P1,P2,P3,P4,P5,P6,P7,P8,P9,P10", " 설비유형= '" + CG + "' And 제어유형 = '" + Control_f + "' And 공급유형 = '" + 공급유형 + "'");
                for (int i = 0; i < 12; i++)
                {
                    double B2 = 0.05;
                    for (int h = 0; h < 10; h++)
                    {
                        if (BC_ahu[i] < 0.05)
                        {
                            fC_PL_ahu[i] = 1;
                            break;
                        }
                        else if (BC_ahu[i] < B2)
                        {
                            fC_PL_ahu[i] = Convert.ToDouble(val[0][h]);
                            break;
                        }
                        else B2 = B2 + 0.1;
                    }
                }
            }
        }

        public void Cal_Load() //냉방존과 공조존 합치기
        {
            //QC_ce,QC_d,QC_s,QC_out 합치기, 부하보정계수, 작동시간
            top = (top_z * QC_a_z + top_ahu * QC_a_ahu) / (QC_a_z + QC_a_ahu); //일일평균 가동시간

            for (int i = 0; i < 12; i++)
            {
                QC_nd[i] = QC_nd_z[i] + QC_nd_ahu[i];
                QC_ce[i] = QC_ce_z[i] + QC_ce_ahu[i];
                QC_d[i] = QC_d_z[i] + QC_d_ahu[i];
                QC_s[i] = QC_s_z[i] + QC_s_ahu[i];
                QC_out[i] = QC_out_z[i] + QC_out_ahu[i];
                                
                
                if (QC_out_z[i] + QC_out_ahu[i] == 0)
                {
                    double[] check = new double[2];
                    double[] check2 = new double[2];
                    check[0] = tmth_z[i];
                    check[1] = tmth_ahu[i];
                    check2[0] = theta_z[i];
                    check2[1] = theta_ahu[i];

                    fC_PL[i] = 1;
                    tmth[i] = check.Max();
                    Theta_IC[i] = check2.Max();
                    tC_op[i] = 0;
                }
                else 
                {
                    fC_PL[i] = (fC_PL_z[i] * QC_out_z[i] + fC_PL_ahu[i]*QC_out_ahu[i]) / (QC_out_z[i] + QC_out_ahu[i]);
                    tmth[i] = (tmth_z[i] * QC_out_z[i] + tmth_ahu[i] * QC_out_ahu[i]) / (QC_out_z[i] + QC_out_ahu[i]);
                    tC_op[i] = (tC_op_z[i] * QC_out_z[i] + tC_op_ahu[i] * QC_out_ahu[i]) / (QC_out_z[i] + QC_out_ahu[i]);
                    Theta_IC[i] = (theta_z[i] * QC_out_z[i] + theta_ahu[i] * QC_out_ahu[i]) / (QC_out_z[i] + QC_out_ahu[i]);
                }
               
            }
        }
        //냉방에너지소요량
        public void Cal_CS()
        { 
            Cal_feerCorr();
            Cal_MultiFactor(); //fC_M 작성

            //저장제어운영계수중 운영계수 반영
            string[][] value = Program.DB.getValue(DB.type.BaseDB_Cooling, "저장제어운영계수", "운영계수", " 항목= '" + Sto_Tank + "' And 종류 = '" + Sto_Type + "' And 번호='" + ArtNumber + "'");
            if (value.Length > 0)
            {
                fSP = Convert.ToDouble(value[0][0]);
            }
            else fSP = 1;
            

            for (int i = 0; i < 12; i++)
            {
                EER_c[i] = EER_f * feer_corr[i];
                SEER_c[i] = EER_c[i] * fC_PL[i] * fC_M * fSP;
               

                if (SEER_c[i] == 0)
                {
                    QC_f[i] = 0;

                }
                else QC_f[i] = QC_out[i] / SEER_c[i];
            }

        }
        public void Cal_feerCorr()
        {
            
            string[][] v = Program.DB.getValue(DB.type.BaseDB_Cooling, "실외온도보정", "req_in, req_out, cond, evad", "냉방설비= '" + CG + "' And 구분 = '"+ Cout + "'"); //수방식, 직팽식 중 선택
            ThetaC_gen_hr_req_in = Convert.ToDouble(v[0][0]);//A
            ThetaC_gen_req_out = Convert.ToDouble(v[0][1]);//B
            Theta_cond = Convert.ToDouble(v[0][2]);//C
            Theta_evad = Convert.ToDouble(v[0][3]);//D

            double son1, son2, mam1, mam2;
            double[] Tempout = new double[12], Tempin = new double[12]; //열원측, 부하측

            switch (CG)
            {
                case "실외기12kW":
                    
                    for(int i =0; i < 12; i++)
                    {
                        if (CSource == "패시브쿨링DEC")
                        {
                            Tempin[i] = 273 + Theta_IC[i];
                            Tempout[i] = 273 + WetTemperature[i] + Theta_Around;
                        }
                        else
                        {
                            Tempin[i] = 273 + Theta_IC[i];
                            Tempout[i] = 273 + OutdoorTemperature[i]; //상승온도는 이미 반영하였슴
                        }
                    }
                    break;
                
                case "공냉식냉동기":

                    for (int i = 0; i < 12; i++)
                    {
                        if( Cout == "수방식")
                        {
                            if (CSource == "패시브쿨링DEC")
                            {
                                Tempin[i] = 273 + CWout;
                                Tempout[i] = 273 + WetTemperature[i] + Theta_Around;
                            }
                            else
                            {
                                Tempin[i] = 273 + CWout;
                                Tempout[i] = 273 + OutdoorTemperature[i]; //상승온도는 이미 반영하였슴
                            }
                        }
                        else //직팽식인 경우임
                        {
                            if (CSource == "패시브쿨링DEC")
                            {
                                Tempin[i] = 273 + Theta_IC[i];
                                Tempout[i] = 273 + WetTemperature[i] + Theta_Around;
                            }
                            else
                            {
                                Tempin[i] = 273 + Theta_IC[i];
                                Tempout[i] = 273 + OutdoorTemperature[i];//상승온도는 이미 반영하였슴
                            }
                        } 
                    }
                    break;

                case "수냉식냉동기":
                    for (int i = 0; i < 12; i++)
                    {
                        Tempin[i] = 273 + CWout;
                        Tempout[i] = 273 + CSWout; //응축기내 냉각수유입온도[실제로는 변하는 값이지만 일단 기준에 따라 작성함
                    }
                        break;
                case "지열히트펌프":
                    for (int i = 0; i < 12; i++)
                    {
                        Tempin[i] = 273 + CWout;
                        Tempout[i] = 273 + CSWout; //응축기내 냉각수유입온도[실제로는 변하는 값이지만 일단 기준에 따라 작성함
                    }

                    break;
                case "흡수식냉동기":
                    for (int i = 0; i < 12; i++)
                    {
                        Tempin[i] = 273 + CWout;
                        Tempout[i] = 273 + CSWout; //응축기내 냉각수유입온도[실제로는 변하는 값이지만 일단 기준에 따라 작성함
                    }

                    break;
                
                default:
                    break;

            }


            for (int j = 0;j<12; j++)
            {
                son1 = (Tempin[j] - Theta_evad);
                son2 = (Tempout[j] + Theta_cond) - (Tempin[j] - Theta_evad);

                mam1 = ThetaC_gen_hr_req_in - Theta_evad;
                mam2 = (ThetaC_gen_req_out + Theta_cond) - (ThetaC_gen_hr_req_in - Theta_evad);
                feer_corr[j] = (son1 / son2) / (mam1 / mam2);
            }
        }
        public void Cal_MultiFactor()
        {
            string 공급유형;
            if (CG == nameof(_TYPE.실외기12kW))
            {
                int a = ZoneNumber_f + AhuNumber_f;
                if (a > 1)
                {
                    공급유형 = "멀티존";
                }
                else 공급유형 = "단일존";
            }
            else
            {
                공급유형 = Comp_f;
            }

            if (Number_f > 1)
            {
                string[][] fcM_value = Program.DB.getValue(DB.type.BaseDB_Cooling, "부분부하계수", "fC_M", "설비유형= '" + CG + "' And 제어유형 = '" + Control_f + "' And 공급유형 = '" + 공급유형 + "'");
                fC_M = Convert.ToDouble(fcM_value[0][0]);
            }
            else fC_M = 1;
        }

        #region 보조설비
        public void Cal_AuxSum() //output W[12]
        {
            CalW_ce();
            CalW_d();
            CalW_s();
            CalW_g();
            for(int i = 0;i<12 ;i++)
            {
                W[i] = W_ce[i] + W_d[i] + W_s[i] + W_g[i];
            }
        }
        // ///////////////////////////////////////////////공급 보조설비 에너지소요량 계산/////////////////////////////////////////////
        public void CalW_ce() //공조기 포함해서 한번에 계산함
        {
            double sum = 0;
            foreach (CoolingCE ce in SelectCE)
            {
                if (ce._cetype != "실내기")
                {
                    int index = ce._ceNum.IndexOf("_");
                    sum += Convert.ToInt32(ce._ceNum.Substring(index+1)) * ce._ceElec; //개수 x 소비전력
                }
            }
            for (int i = 0; i < 12; i++)
            {
                W_ce[i] = sum * tC_op[i];
            } 
        }

        // ///////////////////////////////////////////////////분배 보조설비 에너지 소요량 계산//////////////////////////////////////////////
        public void CalW_d()//output W_d[12]
        {
            double[] 냉수펌프1 = new double[12], 냉수펌프2 = new double[12], 냉각수펌프1 = new double[12], 냉각수펌프2 = new double[12];
            double num1, num2, num3, num4;
            if (SelectPump1_nonsplit != null)
            {
                냉수펌프1 = PumpCalc(Pump1, "냉수");
                num1 = Pump1._number;
                P1power = Pump1.동력 * num1;
                PumpControl = Pump1._control;
            }
            else
            {
                for (int k = 0; k < 12; k++)
                {
                    냉수펌프1[k] = 0;
                }
                num1 = 0;
            }
            
            if (SelectPump2_nonsplit != null)
            {
                냉수펌프2 = PumpCalc(Pump2, "냉수");
                num2 = Pump2._number;
                P2power = Pump2.동력 * num2;
            }
            else
            {
                for (int k = 0; k < 12; k++)
                {
                    냉수펌프2[k] = 0;
                }
                num2 = 0;
            }
            
            if (SelectSPump1_nonsplit != null)
            {
                냉각수펌프1 = PumpCalc(SPump1, "냉수");
                num3 = SPump1._number;
                SP1power = SPump1.동력 * num3;
                SPumpControl = SPump1._control;
            }
            else
            {
                for (int k = 0; k < 12; k++)
                {
                    냉각수펌프1[k] = 0;
                }
                num3 = 0;
            }

            if (SelectSPump2_nonsplit != null)
            {
                냉각수펌프2 = PumpCalc(SPump2, "냉수");
                num4 = SPump2._number;
                SP2power = SPump2.동력 * num4;
            }
            else
            {
                for (int k = 0; k < 12; k++)
                {
                    냉각수펌프2[k] = 0;
                }
                num4 = 0;
            }

            for (int i = 0; i < 12; i++)
            {
                W_d[i] = 냉수펌프1[i] + 냉수펌프2[i] + 냉각수펌프1[i] + 냉각수펌프2[i];
            }

        }
        
        public double[] PumpCalc(CoolPump _pump, string type) //냉수, 냉각수
        {
            double Vz = 0, DeltaPz, Phydr, fe1, CP11, CP21, fHydr;
            double[] ed = new double[12], Beta = new double[12], Wd_hydr = new double[12], W_d = new double[12];
            
            string[][] pumpvalue = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "B효율,유량,동력,양정", "번호= '" + _pump._pumpNum + "'");
            _pump.B효율 = Convert.ToDouble(pumpvalue[0][0]);
            _pump.유량 = Convert.ToDouble(pumpvalue[0][1]);
            _pump.동력 = Convert.ToDouble(pumpvalue[0][2]);
            _pump.양정 = Convert.ToDouble(pumpvalue[0][3]);
            
            if (type == "냉수")
            {
                Vz = 3.6 * Power_f / ((CWout - CWin) * 4.18);
            }
            else if (type == "냉각수")
            {
                Vz = 3.6 * CTPower_f / ((CSWin - CSWout) * 4.18);
            }
                
            DeltaPz = _pump.양정 * 1000 * 9.81; //kPa단위임
            Phydr = DeltaPz * Vz / 3600;
            fe1 = _pump.동력 / Phydr;

            string[][] pumpfactor = Program.DB.getValue(DB.type.BaseDB_Cooling, "펌프제어", "CP1,CP2,fHydr", "펌프제어 = '" + _pump._control + "' And 정유량밸브 = '" + _pump._valve + "'");
            CP11 = Convert.ToDouble(pumpfactor[0][0]);
            CP21 = Convert.ToDouble(pumpfactor[0][1]);
            fHydr = Convert.ToDouble(pumpfactor[0][2]);
            for (int i = 0;i < 12 ;i++)
            {
                if (tC_op_z[i] == 0)
                {
                    W_d[i] = 0;
                }
                else 
                {
                    Beta[i] = QC_out[i] / (tC_op_z[i] * Power_f);
                    ed[i] = fe1 * (CP11 + CP21 / Beta[i]);
                    Wd_hydr[i] = Phydr / 1000 * tC_op[i] * Beta[i] * fHydr;
                    W_d[i] = Wd_hydr[i] * ed[i] * _pump._number; //설치대수까지 포함
                }
            }
            return W_d;
        }

        
        // //////////////////////////////////////////////저장보조설비 에너지소요량 계산//////////////////////////////////

        public void CalW_s()//output W_s[12]
        {
            for (int i = 0; i < 12; i++)
            {
                W_s[i] = 0;
            }

        }
        // //////////////////////////////////////////////생산보조설비 에너지소요량 계산//////////////////////////////////

        public void CalW_g()//output W_g[12]
        {
            double[] G_stanby = new double[12], CTopfan = new double[12], CT_stanby = new double[12];
            string[][] days = Program.DB.getValue(DB.type.BaseDB_HCneed, "이용일수", "일", "주간일수= '주 7.0 일 근무'");
            for (int i = 0; i < 12; i++)
            {
                //대기전력
                if (QC_out[i] != 0)
                {
                    G_stanby[i] = (Convert.ToDouble(days[i][0]) * 24 - tC_op[i]) * Pctrl_f / 1000; //냉방인경우로 국한함

                }else G_stanby[i] = 0;

                if (CT_Sum.Count > 0 && QC_out[i] != 0)
                {
                    CT_stanby[i] = (Convert.ToDouble(days[i][0]) * 24 - tC_op[i]) * CTPctrlel_f / 1000;
                    CTopfan[i] = QC_out[i] * CTPhrel_f * CTfhrPL_f;
                }
                else
                {
                    CT_stanby[i] = 0;
                    CTopfan[i] = 0;
                }

                W_g[i] = G_stanby[i] + +CT_stanby[i] + CTopfan[i];
            }

        }
        #endregion

        public void Cal_Save() 
        {
            //설비정보와 보조설비정보는 따로따로
            QCa_nd = 0;
            QCa_ce = 0;
            QCa_d = 0;
            QCa_s =0;
            QCa_out =0;
            QCa_f = 0;
            QCa_p = 0;

            for (int i =0; i<12; i++)
            {
                QCa_nd += QC_nd[i];
                QCa_ce += QC_ce[i];
                QCa_d +=  QC_d[i];
                QCa_s += QC_s[i];
                QCa_out += QC_out[i];
                QCa_f += QC_f[i];
                
            }
            
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            for(int i=0; i<12 ; i++)
            {

                Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "프로젝트유형,프로젝트번호, 번호, 명칭, 냉방설비, 냉방출력, 냉방성능, 대기전력, 설치대수, Fuel,월,열원설비",
                            "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','" + CoolingNum + "','" + CoolingName + "','" + CG + "','" + Power_f + "','" + EER_f + "','" + Pctrl_f + "','" + Number_f + "','" + Carrier + "','" + mth[i] + "','" + CSource + "'", "번호,월"); 

                Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,QCb_a,QCa_ce,QCa_d,QCa_s,QCa_out,QCa_f,Sto_Tank,Sto_Type",
                          "'" + CoolingNum + "','" + mth[i] + "','" + QCa_nd + "','" + QCa_ce + "','" + QCa_d + "','" + QCa_s + "','" + QCa_out + "','"+QCa_f+"','" + Sto_Tank + "','" + Sto_Type + "'", "번호,월");

                
                Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,QC_f, SEER_c, EER_c,QC_out,QC_ce,QC_d,QC_s,QC_nd",
                           "'" + CoolingNum + "','" + mth[i] + "','" + QC_f[i] + "', '"+ SEER_c[i] +"','" + EER_c[i] + "','" + QC_out[i] + "','" + QC_ce[i] + "','" + QC_d[i] + "','" + QC_s[i] + "','" + QC_nd[i] +"'", "번호,월");
                
                Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,W,W_g,W_ce,W_d,W_s",
                           "'" + CoolingNum + "','" + mth[i] + "','" + W[i] + "', '" + W_g[i] + "','" + W_ce[i] + "','" + W_d[i] + "','" + W_s[i] + "'", "번호,월");


                if (ZoneNameList.Count > 0)
                {
                    Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,개수_z,QCb_a_z,QC_Max_z,공급설비1_z,공급설비2_z,A_z",
                           "'" + CoolingNum + "','" + mth[i] + "','" + ZoneNumber_f + "','" + QC_a_z + "','" + Qc_max_z + "','" + CE1_z + "','" + CE2_z + "','" + A_z + "'", "번호,월");

                    Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,QC_out_z, QC_ce_z, QC_d_z, QC_s_z, QC_nd_z",
                               "'" + CoolingNum + "','" + mth[i] + "','" + QC_out_z[i] + "','" + QC_ce_z[i] + "','" + QC_d_z[i] + "','" + QC_s_z[i] + "','" + QC_nd_z[i] + "'", "번호,월");

                }
                if (AhuNameList.Count > 0)
                {
                    Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,개수_ahu,QCb_a_ahu,QC_Max_ahu,공급설비1_ahu,공급설비2_ahu,A_ahu",
                           "'" + CoolingNum + "','" + mth[i] + "','" + AhuNumber_f + "','" + QC_a_ahu + "','" + Qc_max_ahu + "','" + CE1_ahu + "','" + CE2_ahu + "','" + A_ahu + "'", "번호,월");

                   
                    Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,QC_out_ahu, QC_ce_ahu, QC_d_ahu, QC_s_ahu, QC_nd_ahu",
                              "'" + CoolingNum + "','" + mth[i] + "','" + QC_out_ahu[i] + "','" + QC_ce_ahu[i] + "','" + QC_d_ahu[i] + "','" + QC_s_ahu[i] + "','" + QC_nd_ahu[i] + "'", "번호,월");

                }
                if(CG != "실외기12kW")
                {
                    Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,압축기종류",
                                               "'" + CoolingNum + "','" + mth[i] + "','" + Comp_f + "','" + CWout + "'", "번호,월");

                }
                if(펌프유무 == "펌프 있음") //펌프유무
                {
                    Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,CSWin,CSWout,P1power,P2power,Pump1Valve,SP1power,SP2power,SPValve,냉수출구온도",
                                               "'" + CoolingNum + "','" + mth[i] + "','" + CSWin + "','" + CSWout + "','"+P1power+ "','"+P2power+"','"+PumpControl+"','"+SP1power+"','"+SP2power+"','"+SPumpControl+"','"+CWout+"'", "번호,월");
                }
                if(CG=="수냉식냉동기" || CG == "흡수식냉동기")
                {
                    Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Result", "번호,월,CTPower",
                                               "'" + CoolingNum + "','" + mth[i] + "','" + CTPower_f + "'", "번호,월");
                }
            }
        }

        //냉방설비 작동시간 계산
        public void CS_t() //작동시간 계산함
        {
            for (int i = 0; i < 12; i++)
            {

                if(QC_out_z[i] + QC_out_ahu[i] == 0)
                {
                    tC_op[i] = 0;
                }else
                tC_op[i] = (tC_op_z[i] * QC_out_z[i] + tC_op_ahu[i] * QC_out_ahu[i]) / (QC_out_z[i]+ QC_out_ahu[i]);

                if (QC_out_z[i] + QC_out_ahu[i] == 0)
                {
                    tC_op[i] = 0;
                }
                else tmth[i] = (tmth_z[i] * QC_out_z[i] + tmth_ahu[i] * QC_out_ahu[i]) / (QC_out_z[i] + QC_out_ahu[i]);

            }
        }  
    }

    class zonemake //냉방존 만들기 
    {
        public string ZoneName, ZoneNum;
        public double Anf, tC_op, QC_max; //
        public double[] Q_nd = new double[12], dwd = new double[12], θi_c = new double[12]; //getvalue로 값을 가져옴

        public zonemake(string _ZoneNum, string _CoolingNum )
        {
            ZoneName = null;
            ZoneNum = null;
            tC_op = 0; Anf = 0; QC_max = 0;
            double load_sum = 0;
            this.ZoneNum = _ZoneNum;
            
            string[][] v1 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존이름,공조시간,순바닥면적", "존번호= '" + ZoneNum + "'");
            ZoneName = v1[0][0];
            tC_op = Convert.ToDouble(v1[0][1]);
            Anf = Convert.ToDouble(v1[0][2]);
            
            string[][] v2 = Program.DB.getValue(DB.type.ProjDB, "Cooling_ce_Form", "부하율", " 존번호 = '" + ZoneNum + "' AND 냉방시스템 = '" + _CoolingNum + "' ");
            for (int i = 0; i < v2.Length; i++)
            {
                load_sum += Convert.ToDouble(v2[i][0]);
            }
            
            string[][] v3 = Program.DB.getValue(DB.type.ProjDB, " Zone_HCneed_Result", " Q_max,  Qb_mth, dwd_mth, theta_i", "번호= '" + ZoneNum + "' AND 비이용일_이용일 = '이용일' And 난방_냉방 = '냉방'");
            QC_max = Convert.ToDouble(v3[6][0]); //7월달걸로 적용함
            
            for (int i = 0; i < 12; i++)
            {
                Q_nd[i] = Convert.ToDouble(v3[i][1]) * load_sum; //냉방요구량
                if (Q_nd[i] == 0)
                {
                    dwd[i] = 0;
                }else dwd[i] = Convert.ToDouble(v3[i][2]);  //냉방사용일수
                
                θi_c[i] = Convert.ToDouble(v3[i][3]); //실내온도
            }
        }
        
    }
    class ahumake
    {
        public string AhuName, AhuNum, Install;
        public double Anf, tC_op, QC_max; //
        public double[] Q_nd = new double[12], dwd = new double[12], θi_c = new double[12]; //getvalue로 값을 가져옴
        private List<string> zn = new List<string>();
        public ahumake(string _AhuNum, string _CoolingNum)
        {
            AhuName = null;
            AhuNum = null;
            Anf = 0; tC_op = 0; QC_max = 0;
            double load_sum = 0;
                        
            AhuNum = _AhuNum; //공조기번호
            string[][] 면적 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적, 존번호,냉난방유무", "선택열회수기 ='" + AhuNum + "'");

            for (int i = 0; i < 면적.Length; i++)
            {
                if (면적[i][2] == "냉난방" || 면적[i][2] == "냉방")
                {
                    Anf += Convert.ToDouble(면적[i][0]); //해당설비 면적 다 더하기
                    zn.Add(면적[i][1].ToString());
                }
            }
            string[][] 정보 = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "명칭,설치위치", " 번호 = '" + AhuNum + "'");
            AhuName = 정보[0][0];
            if (정보[0][1] == "단열외피 외부")
            {
                Install = "건물외부설치";
            }
            else Install = "건물내부설치";
            
            string[][] 부하 = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "tvmech_avg, dvmech_avg, Qmax_tot, 공조요구량", " 번호 = '" + AhuNum + "' And 난방_냉방 ='냉방'");
            QC_max = Convert.ToDouble(부하[7][2]); //7월값 적용함
            tC_op = Convert.ToDouble(부하[0][0]);
            for (int i = 0; i < 12; i++)
            {
                Q_nd[i] = Convert.ToDouble(부하[i][3]);
                if (Q_nd[i] == 0)
                {
                    dwd[i] = 0;
                }
                else dwd[i] = Convert.ToDouble(부하[i][1]);
            }

            double Mload = 0;
            double[] 온도 = new double[12];
            foreach(string val in zn) //존이3개있다면
            {
                string[][] 존정보 = Program.DB.getValue(DB.type.ProjDB, " Zone_HCneed_Result", "theta_i, Q_max", "번호= '" + val + "' AND 비이용일_이용일 = '이용일' And 난방_냉방 = '냉방'");
                for (int j = 0; j<12 ; j ++)
                {
                    온도[j] = Convert.ToDouble(존정보[j][0]) * Convert.ToDouble(존정보[0][1]);
                }
                Mload += Convert.ToDouble(존정보[0][1]);
            }
            for (int j = 0; j < 12; j++)
            {
                θi_c[j] = 온도[j] / Mload; // 존의실내온도
            }
        }
    }

    class LoadCalc
    {
        public string CeNum, ZoneNum; //공급설비, 존번호
        public double power, useTime;
    }

    class CoolingGeneratorMake
    {
        public string _num, _control, _fuel, _econo, _install; 
        public double _power, _eer, _pctrl;
        public int _number;

        public string _comp;
        public double _cwin, _cwout;
        public string _cout; //직팽식, 수방식
    }
    class CoolPump
    {
        public string _pumpNum, _pumpname, _valve, _control;
        public int _number;
        public double B효율, 유량, 동력, 양정;
    }

    class CoolingCE
    {
        public string _zonenum, _cetype, _ceNum;
        public int _operhour;
        public double _cePower, _ceElec, _ceRate;
        
        public string _type(string _ct)
        {
            string t;
            if(_ct == "VAV유닛"|| _ct == "CAV유닛" || _ct == "팬파워유닛" )
            {
                t = "공조기";
                return t;
            }
            else
            {
                t = "실내";
                return t;
            }
        }
    }
    class CoolTop
    {
        public string _ctnum, _ctname, _cttype, _ctmeth, _install, _ctctrl, _ctfan; //_cttype는 습식건식, _ctmeth는 밀폐형,개방형을 지칭함
        public double _power, _quantity, _phr_el, _cswin, _cswout, _pctrl_el; 
        public int _number;
    }
}


