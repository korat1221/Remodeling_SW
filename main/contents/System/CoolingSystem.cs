using main.contentslist;
using main.subcontents;
using main.subcontents.CoolingSystem;
using main.subcontents.HeatingSystem;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Infrastructure;
using System.Data.SqlTypes;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static main.DB;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace main.contents
{
    public partial class CoolingSystem : Form
    {

        //만들어지느것
        List<string> ZoneNameList = new List<string>();
        List<string> AHUNameList = new List<string>();


        //생산설비
        string 프로젝트유형;
        string CG, Num, Name_f;

        string SelectZone_nonsplit, SelectAHU_nonsplit, SelectCG_nonsplit, SelectCGC_nonsplit, SelectCGE_nonsplit, SelectCGN_nonsplit;//저장항목설비,제어,외기냉방,
        List<string> SelectCG_split = new List<string>(), SelectCGC_split = new List<string>(), SelectCGE_split = new List<string>(), SelectCGN_split = new List<string>();
        double PowerTotal = 0, EERTotal = 0;
        //냉각탑관련
        List<string> SelectCT_split = new List<string>(), SelectCTN_split = new List<string>();
        string SelectCT_nonsplit, SelectCTN_nonsplit; //냉각탑부분

        //이미지작성
        string Install_f, Control_f, CSource, Fuel_f, Econo_f, Comp_f, Refri, EvaType_f; //부하측열원공급설비(Supp_f) 및 설치(기존,신규)가 추가됨


        //계산과정만 공유함


        //냉방존
        double ZoneNumber_f = 0, QC_a_z = 0, QC_max_z = 0, A_z = 0; //새로작성함

        //공조존
        double AhuNumber_f = 0, QC_a_Ahu = 0, QC_max_Ahu = 0, A_Ahu = 0; //새로작성함
        //냉방존 + 공조존 합계
        double SysNumber, QC_a, QC_max, A;


        //공냉식냉동기
        string SelectCGComp_nonsplit, SelectCGLoad_nonsplit; //저장항목, 압축기와 
        List<string> SelectCGComp_split = new List<string>(), SelectCGLoad_split = new List<string>();

        //장비관련
        List<double> Power = new List<double>(), EER = new List<double>();
        List<string> EvaType = new List<string>(); //공냉식, 수냉식, 지열히트펌프 유형
        List<double> SEER = new List<double>(); //  흡수식인 경우에 한함


        //저장설비
        string Stotype, StoSource;


        //펌프정의

        string[] Pump1 = new string[5], Pump2 = new string[5], CPump1 = new string[5], CPump2 = new string[5]; //명칭[0], 타입[1], 설치대수[2], 밸브[3], 제어[4] 
        string PumpUse, PumpMethod, CPumpMethod, Pump1_nonsplit, Pump2_nonsplit, CPump1_nonsplit, CPump2_nonsplit;



        //공급설비정의
        string ce1Type, ce2Type, ce1Ahu, ce2Ahu;
        string[] ceType = { "공조기", "실내기", "팬코일유닛", "복사냉방(천장)", "복사냉방(벽)", "바닥매립형컨백터", "" };
        string[] ceAhuType = { "VAV유닛", "파워팬유닛", "CAV유닛", "" }; //ce1/2Ahu에 대한 내용임


        ArrayList SelectAirConditioning = new ArrayList(); ArrayList SelectPump = new ArrayList(); ArrayList Selectce1Zone = new ArrayList(); ArrayList Selectce2Zone = new ArrayList();
        ArrayList Selectce1Ahu = new ArrayList(); ArrayList Selectce2Ahu = new ArrayList();
        int ce_SelectRow;

        enum PumpType { 냉수1차, 냉수2차, 냉각수1차, 냉각수2차 };


        public CoolingSystem()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '냉방시스템'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            string[][] 프로젝트 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            if (프로젝트.Length > 0)
            {
                프로젝트유형 = 프로젝트[0][0];
            }

            //시스템 콤보박스
            CoolingGeneratorSelect_comboBox.Items.Clear();
            CoolingGeneratorSelect_comboBox.Items.AddRange(Systemtype.ToArray());

            //저장 설비 콤보박스
            StorageList_comboBox.Items.AddRange(storagelist());

            //펌프 유무 콤보박스 
            PumpUse_comboBox.Items.Clear();
            PumpUse_comboBox.Items.Add("펌프 있음");
            PumpUse_comboBox.Items.Add("펌프 없음");
            PumpUse_comboBox.SelectedIndex = 1;

            //냉수펌프 방식 콤보박스
            PumpMethod_label.Visible = false;
            PumpMethod_comboBox.Visible = false;

            Pump1_label.Visible = false;
            Pump1_textBox.Visible = false;
            Pump1_button.Visible = false;

            Pump2_label.Visible = false;
            Pump2_textBox.Visible = false;
            Pump2_button.Visible = false;


            PumpMethod_comboBox.Items.Clear();
            PumpMethod_comboBox.Items.Add("1차펌프");
            PumpMethod_comboBox.Items.Add("1차폐회로+2차펌프");

            //냉각수 순환펌프 콤보박스
            CPumpMethod_comboBox.Items.Clear();
            CPumpMethod_comboBox.Items.Add("1차펌프");
            CPumpMethod_comboBox.Items.Add("1차폐회로+2차펌프");

            CPumpMethod_label.Visible = false;
            CPumpMethod_comboBox.Visible = false;

            CPump1_label.Visible = false;
            CPump1_textBox.Visible = false;
            CPump1_button.Visible = false;

            CPump2_label.Visible = false;
            CPump2_textBox.Visible = false;
            CPump2_button.Visible = false;


            //공급설비 콤보박스
            ce1Type_comboBox.Items.AddRange(ceType.ToArray());
            ce2Type_comboBox.Items.AddRange(ceType.ToArray());
            ce1Ahu_comboBox.Items.AddRange(ceAhuType.ToArray());
            ce2Ahu_comboBox.Items.AddRange(ceAhuType.ToArray());
        }

        private string[] storagelist()
        {
            string[] list = new string[5];
            string[][] DefaultDB_Value = Program.DB.getValue_SameCheck(DB.type.BaseDB_Cooling, "저장제어운영계수", "항목", "");
            if (DefaultDB_Value.Length > 0)
            {
                for (int i = 0; i < DefaultDB_Value.Length; i++)
                {
                    list[i] = (DefaultDB_Value[i][0]);
                }
            }
            return list; ;
        }

        //1. 명칭 작성
        private void CoolingSystemNameText_TextChanged(object sender, EventArgs e)
        {
            if (CoolingSystemNameText.Text != null)
            {
                Name_f = CoolingSystemNameText.Text.ToString();
            }
        }

        //3. 존 선택
        private void Zone_button_Click(object sender, EventArgs e)
        {
            string select = "Zone";
            if (Program.UTIL.fromCode)
            {
                Program.UTIL.fromCode = false;
                return;
            }

            if (CoolingSystemNameText.Text != null && CoolingSystemNameText.Text != "")
            {
                Name_f = CoolingSystemNameText.Text;

                Cooling_Zone ZC = new Cooling_Zone(Num, SelectZone_nonsplit, select);

                DialogResult result = ZC.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (ZC.SelectZone != null)
                    {
                        SelectZone_nonsplit = ZC.SelectZone;
                        Split(ZC.SelectZone, ZoneNameList);
                        SelectedZoneText.Text = ZoneNameList[0].ToString() + " 외 " + (ZoneNameList.Count - 1).ToString() + "개";
                        Zonemainwrite();
                    }
                    else
                    {
                        SelectZone_nonsplit = null;
                        ZoneNameList.Clear();
                        SelectedZoneText.Text = null;
                        Zonemainwrite();
                    }

                }
            }
            else
            {
                MessageBox.Show("먼저 명칭을 입력해 주세요!", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }
        private void Split(string nonSplit, List<string> type)
        {
            type.Clear();

            if (nonSplit != null)
            {
                string[] token = nonSplit.Split('+');
                foreach (string item in token)
                {
                    string _item = item.Trim();
                    type.Add(_item);
                }
            }
        }
        //Zone 매인 윗화면 작성항목 만들기
        private void Zonemainwrite()
        {
            A_z = 0;
            QC_a_z = 0;
            QC_max_z = 0;
            if (ZoneNameList.Count > 0)
            {
                foreach (string zone in ZoneNameList)
                {
                    string[][] 면적 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "존번호 ='" + zone + "'");
                    string[][] 부하 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_a,Q_max", "번호 ='" + zone + "' And 난방_냉방 ='냉방'");
                    if (부하.Length == null || 부하.Length == 0)
                    {
                        MessageBox.Show("요구량계산을 해주세요!");
                        return;
                    }
                    else
                    {
                        A_z += Convert.ToDouble(면적[0][0]);
                        QC_a_z += Convert.ToDouble(부하[0][0]);
                        QC_max_z += Convert.ToDouble(부하[0][1]) / 1000;
                    }
                }
            }
            else
            {
                A_z = 0;
                QC_a_z = 0;
                QC_max_z = 0;
            }
           
            if (ZoneNameList.Count > 1)
            {
                ZoneS_label.Visible = true;
                ZoneS_label.Text = "존 공급방식: 멀티존";
            }
            else if (ZoneNameList.Count == 1)
            {
                ZoneS_label.Visible = true;
                ZoneS_label.Text = "존 공급방식: 단일존";
            }
            else ZoneS_label.Visible = false;

            CZ_AnnualCoolingNeed_Textbox.Visible = true;
            CZ_FloorArea_Textbox.Visible = true;
            CZ_MaxCoolingLoad_Textbox.Visible = true;

            CZ_AnnualCoolingNeed_Textbox.Text = QC_a_z.ToString();
            Program.UTIL.textBox_doubleComa(CZ_AnnualCoolingNeed_Textbox, true, 0);
            CZ_FloorArea_Textbox.Text = A_z.ToString();
            Program.UTIL.textBox_doubleComa(CZ_FloorArea_Textbox, true, 2);
            CZ_MaxCoolingLoad_Textbox.Text = QC_max_z.ToString();
            Program.UTIL.textBox_doubleComa(CZ_MaxCoolingLoad_Textbox, true, 2);
        }

        private void Ahumainwrite()
        {
            A_Ahu = 0;
            QC_a_Ahu = 0;
            QC_max_Ahu = 0;

            if(AHUNameList.Count > 0)
            {

                foreach (string ahu in AHUNameList) //3개가 잡혔다. 각설비별 값을 입력해서 더해야함
                {
                    string[][] 면적 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "선택열회수기 ='" + ahu + "'");
                    for (int i = 0; i < 면적.Length; i++)
                    {
                        A_Ahu += Convert.ToDouble(면적[i][0]); //해당설비 면적 다 더하기
                    }
                    string[][] 부하 = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "공조요구량,Qmax_tot", " 번호 = '" + ahu + "' And 난방_냉방 ='냉방'");
                    for (int k = 0; k < 12; k++)
                    {
                        QC_a_Ahu += Convert.ToDouble(부하[k][0]);
                    }
                    QC_max_Ahu += Convert.ToDouble(부하[7][1]) / 1000; //7월로 한정함
                }
            }
            else
            {
                A_Ahu = 0;
                QC_a_Ahu = 0;
                QC_max_Ahu = 0;
            }

            if (AHUNameList.Count > 1)
            {
                AhuS_label.Visible = true;
                AhuS_label.Text = "공조기 공급방식: 멀티존";
            }
            else if (AHUNameList.Count == 1)
            {
                AhuS_label.Visible = true;
                AhuS_label.Text = "공조기 공급방식: 단일존";
            }
            else AhuS_label.Visible = false;

            CA_AnnualCoolingNeed_Textbox.Visible = true;
            CA_FloorArea_Textbox.Visible = true;
            CA_MaxCoolingLoad_Textbox.Visible = true;

            CA_AnnualCoolingNeed_Textbox.Text = QC_a_Ahu.ToString();
            Program.UTIL.textBox_doubleComa(CA_AnnualCoolingNeed_Textbox, true, 0);
            CA_FloorArea_Textbox.Text = A_Ahu.ToString();
            Program.UTIL.textBox_doubleComa(CA_FloorArea_Textbox, true, 2);
            CA_MaxCoolingLoad_Textbox.Text = QC_max_Ahu.ToString();
            Program.UTIL.textBox_doubleComa(CA_MaxCoolingLoad_Textbox, true, 2);
        }
        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }
        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        #region 4. 설비 선택
        public List<string> Systemtype //냉동기종류리스트
        {
            get
            {
                string[][] System = Program.DB.getValue_SameCheck(DB.type.BaseDB_Cooling, "냉방설비이미지", "설비유형",
                    "항목유형='생산설비'");
                List<string> _Systemtype = new List<string>();
                if (System.Length > 0)
                {
                    for (int i = 0; i < System.Length; i++)
                    {
                        _Systemtype.Add(System[i][0]);
                    }
                }
                return _Systemtype;
            }
            set { }
        }

        public List<string> Installtype(string type) //설치위치리스트
        {
            string[][] Intall = Program.DB.getValue_SameCheck(DB.type.BaseDB_Cooling, "냉방설비이미지", "설비유형",
                     "항목유형='" + type + "'");
            List<string> _Installtype = new List<string>();
            if (Intall.Length > 0)
            {
                for (int i = 0; i < Intall.Length; i++)
                {
                    _Installtype.Add(Intall[i][0]);
                }
            }
            return _Installtype;
        }

        private void CoolingGeneratorSelect_comboBox_SelectedIndexChanged(object sender, EventArgs e)//설비항목선택
        {
            CG = CoolingGeneratorSelect_comboBox.Text;
            SelectCG_nonsplit = null;
            SelectCGC_nonsplit = null;
            SelectCGE_nonsplit = null;
            SelectCGN_nonsplit = null;
            if (CG != "수냉식냉동기" && CG != "흡수식냉동기")
            {
                CoolingTop_dataGridView.Rows.Clear();
                CoolingTop_dataGridView.Columns.Clear();
                CoolingTop_dataGridView.Visible = false;
            }
            LoadtabPage(CG); //텝페이지 개시
        }

        private void LoadtabPage(string _CG) //탭활성화 및 열원설비 콤보박스
        {
            if (_CG == "실외기12kW")
            {
                Install_comboBox.Items.Clear();
                Install_comboBox.Text = null;
                SourcepictureBox.Visible = false;
                Install_comboBox.Enabled = true;
                Install_comboBox.Items.AddRange(Installtype("열원설비").ToArray());
                tabControl2.SelectedTab = tabControl2.TabPages["AirCon_tabPage"];
                CoolerTop_button.Visible = false;

            }
            else if (_CG == "공냉식냉동기")
            {
                Install_comboBox.Items.Clear();
                Install_comboBox.Text = null;
                SourcepictureBox.Visible = false;
                Install_comboBox.Enabled = true;
                Install_comboBox.Items.AddRange(Installtype("열원설비").ToArray());
                tabControl2.SelectedTab = tabControl2.TabPages["AirCooler_tabPage"];
                CoolerTop_button.Visible = false;
            }
            else if (_CG == "수냉식냉동기")
            {
                Install_comboBox.Items.Clear();
                Install_comboBox.Text = null;
                SourcepictureBox.Visible = false;
                Install_comboBox.Enabled = false;
                //Install_comboBox.Items.AddRange(Installtype("C열원설비").ToArray());
                tabControl2.SelectedTab = tabControl2.TabPages["WaterCooler_tabPage"];
                CoolerTop_button.Visible = true;
            }
            else if (_CG == "흡수식냉동기")
            {
                Install_comboBox.Items.Clear();
                Install_comboBox.Text = null;
                SourcepictureBox.Visible = false;
                Install_comboBox.Enabled = false;
                Install_comboBox.Items.AddRange(Installtype("C열원설비").ToArray());
                tabControl2.SelectedTab = tabControl2.TabPages["AbsorbCooler_tabPage"];
                CoolerTop_button.Visible = true;
            }

            else if (_CG == "지열히트펌프")
            {
                Install_comboBox.Items.Clear();
                Install_comboBox.Text = null;
                SourcepictureBox.Visible = false;
                Install_comboBox.Enabled = false;
                //Install_comboBox.Items.AddRange(Installtype("S열원설비").ToArray());
                tabControl2.SelectedTab = tabControl2.TabPages["SoilCooler_tabPage"];
                CoolerTop_button.Visible = false;

            }
            else if (_CG == "지하수히트펌프")
            {
                Install_comboBox.Items.Clear();
                Install_comboBox.Text = null;
                SourcepictureBox.Visible = false;
                Install_comboBox.Enabled = false;
                //Install_comboBox.Items.AddRange(Installtype("S열원설비").ToArray());
                tabControl2.SelectedTab = tabControl2.TabPages["SoilWaterCooler_tabPage"];
                CoolerTop_button.Visible = false;
            }
        }

        #region //그림작성
        private void Distribute_Image() // 1.분배설비 그림넣기
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉방설비이미지", "이미지", "항목유형 = '분배설비'");
            if (Image.Length > 0)
            {
                DistpictureBox.Size = new System.Drawing.Size(610, 254);
                DistpictureBox.Location = new Point(0, 25);
                DistpictureBox.Load(Program.gPath + Image[0][0]);
                DistpictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
        private void CoolingGeneratorImageSelect(string type, string install)//2.냉방설비 그림
        {
            if (Install_f == "" || Install_f == null)
            {
                MessageBox.Show("먼저 냉방설비를 선택해 주세요.", "주의", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string[][] image1 = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉방설비이미지", "이미지",
                "설비유형='" + type + "' And 설치유형='" + install + "'");
            SyspictureBox.Size = new System.Drawing.Size(110, 170);
            SyspictureBox.Location = new Point(0, 90);
            SyspictureBox.Load(Program.gPath + image1[0][0]);
            SyspictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void LoadPressImage(string Comp, string Install) //압축기 그림
        {
            string[][] ImageP = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉방설비이미지", "이미지", "항목유형 = '압축기' And 설비유형='" + Comp + "' And 설치유형='" + Install + "'");
            if (ImageP.Length > 0)
            {
                Press_pictureBox.Visible = true;
                Press_pictureBox.Size = new System.Drawing.Size(50, 40);
                Press_pictureBox.Location = new Point(10, 115);
                Press_pictureBox.Load(Program.gPath + ImageP[0][0]);
                Press_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                Press_pictureBox.BackColor = Color.Transparent;
                Press_pictureBox.Parent = SyspictureBox;
            }
        }

        private void LoadEvaImage(string Eva, string Install) //증발기 그림
        {
            string[][] ImageP = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉방설비이미지", "이미지", "항목유형 = '증발기' And 설비유형='" + Eva + "' And 설치유형='" + Install + "'");
            if (ImageP.Length > 0)
            {
                eva.Visible = true;
                eva.Size = new System.Drawing.Size(50, 40);
                eva.Location = new Point(60, 98);
                eva.Load(Program.gPath + ImageP[0][0]);
                eva.SizeMode = PictureBoxSizeMode.Zoom;
                eva.BackColor = Color.Transparent;
                eva.Parent = SyspictureBox;
            }
        }

        private void Install_comboBox_SelectedIndexChanged(object sender, EventArgs e)  // 열원 설비 그림
        {

            if (Program.UTIL.ffCode)
            {
                Program.UTIL.ffCode = false;
                return;
            }

            if (Install_f == "" || Install_f == null)
            {
                MessageBox.Show("냉방설비를 먼저 선택해 주세요.", "주의", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string contents = Install_comboBox.Text;
                Load_CSource(contents, Install_f);

            }

        }

        private void Load_CSource(string csource, string install)
        {
            string[][] image = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉방설비이미지", "이미지", "설비유형='" + csource + "' And 설치유형='" + install + "'");
            if (image.Length > 0)
            {
                SourcepictureBox.Visible = true;
                SourcepictureBox.Size = new System.Drawing.Size(250, 200);
                SourcepictureBox.Location = new Point(0, 60);
                SourcepictureBox.Load(Program.gPath + image[0][0]);
                SourcepictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                CSource = Install_comboBox.Text;
            }
        }

        private void StorageList_comboBox_SelectedIndexChanged(object sender, EventArgs e) //축냉탱크 그림
        {
            StorageType_comboBox.Items.Clear();
            Stotype = StorageList_comboBox.Text;
            if (Stotype == "축냉탱크없음")
            {
                StopictureBox.Visible = false;
            }
            else
            {
                StorageType_comboBox.Items.AddRange(new string[] { "수축열", "빙축열" });
                Load_StoType(Stotype, Install_f);
            }

        }
        private void Load_StoType(string _stotype, string _install_f)
        {
            string[][] stoimage = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉방설비이미지", "이미지", "항목유형='저장설비' And 설비유형 = '" + _stotype + "' And 설치유형='" + _install_f + "'");
            if (stoimage.Length > 0)
            {
                StopictureBox.Visible = true;
                StopictureBox.Size = new System.Drawing.Size(135, 135);
                StopictureBox.Location = new Point(12, 98);
                StopictureBox.Load(Program.gPath + stoimage[0][0]);
                StopictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                StopictureBox.BackColor = Color.Transparent;
                StopictureBox.Parent = DistpictureBox;
            }
        }

        private void pump_image() //펌프그림넣기
        {
            string[][] pumpimage = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉방설비이미지", "이미지", "항목유형='분배설비' And 설비유형 = '펌프'");
            if (pumpimage.Length > 0)
            {
                Pump_pictureBox.Visible = true;
                Pump_pictureBox.Size = new System.Drawing.Size(40, 50);
                Pump_pictureBox.Location = new Point(190, 186);
                Pump_pictureBox.Load(Program.gPath + pumpimage[0][0]);
                Pump_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                Pump_pictureBox.BackColor = Color.Transparent;
                Pump_pictureBox.Parent = DistpictureBox;
            }
        }

        private void StorageType_comboBox_SelectedIndexChanged(object sender, EventArgs e) //저장설비타입 그림
        {
            StoSource = StorageType_comboBox.Text;
            if (Stotype == null)
            {
                MessageBox.Show("저장설비를 먼저 선택하세요!");
            }
            else
            {
                Load_StoSource(StoSource);
            }
        }

        private void Load_StoSource(string _stosource)
        {
            string[][] stoTimage1 = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉방설비이미지", "이미지", "항목유형= '저장설비' And  설비유형= '" + _stosource + "'");
            if (stoTimage1.Length > 0)
            {
                StoType_pictureBox.Visible = true;
                StoType_pictureBox.Size = new System.Drawing.Size(50, 50);
                StoType_pictureBox.Location = new Point(60, 70);
                StoType_pictureBox.Load(Program.gPath + stoTimage1[0][0]);
                StoType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                StoType_pictureBox.BackColor = Color.Transparent;
                StoType_pictureBox.Parent = StopictureBox;
            }
        }

        private void imagemake(string _type, int _n) //공급설비 그림 넣기 ""를 `
        {
            string[][] image = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉방설비이미지", "이미지", "항목유형= '공급설비' And 설비유형='" + _type + "'");
            if (image.Length > 0)
            {
                if (_n == 1)
                {
                    ce1_pictureBox.Visible = true;
                    ce1_pictureBox.Size = new System.Drawing.Size(260, 60);
                    ce1_pictureBox.Location = new Point(250, 10);
                    ce1_pictureBox.Load(Program.gPath + image[0][0]);
                    ce1_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    ce1_pictureBox.BackColor = Color.Transparent;
                    ce1_pictureBox.Parent = DistpictureBox;
                }
                else if (_n == 2)
                {
                    ce2_pictureBox.Visible = true;
                    ce2_pictureBox.Size = new System.Drawing.Size(260, 60);
                    ce2_pictureBox.Location = new Point(250, 80);
                    ce2_pictureBox.Load(Program.gPath + image[0][0]);
                    ce2_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    ce2_pictureBox.BackColor = Color.Transparent;
                    ce2_pictureBox.Parent = DistpictureBox;
                }
                else if (_n == 3)
                {
                    ce3_pictureBox.Visible = true;
                    ce3_pictureBox.Size = new System.Drawing.Size(260, 60);
                    ce3_pictureBox.Location = new Point(250, 150);
                    ce3_pictureBox.Load(Program.gPath + image[0][0]);
                    ce3_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    ce3_pictureBox.BackColor = Color.Transparent;
                    ce3_pictureBox.Parent = DistpictureBox;
                }
            }
            else
            {
                if (_n == 1) ce1_pictureBox.Visible = false;
                if (_n == 2) ce2_pictureBox.Visible = false;
                if (_n == 3) ce3_pictureBox.Visible = false;
            }
        }
        #endregion


        private void CoolGeneratorSelect_Button_Click_1(object sender, EventArgs e)
        {
            if (CoolingGeneratorSelect_comboBox.Text != "")
            {
                CG = CoolingGeneratorSelect_comboBox.Text;

                switch (CG)
                {
                    case "실외기12kW":
                        AirCon();
                        break;
                    case "공냉식냉동기":
                        AirCooler();
                        break;
                    case "수냉식냉동기":
                        WaterCooler();
                        break;
                    case "흡수식냉동기":
                        AbsorbCooler();
                        break;
                    case "지열히트펌프":
                        SoilCooler();
                        break;

                    case "지하수히트펌프":
                        SoilWaterCooler();
                        break;

                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("냉방설비를 먼저 선택해 주세요.", "주의 필요", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
        private string installmake(int a, int b, int c) //a:기존, b:신규, c:보수
        {
            Program.UTIL.fromCode = true;

            string check = null;

            if (a > 0)
            {
                if (c == 0)
                {
                    radioButton1.Checked = true;
                    check = "기존";
                }
                else if (c > 0)
                {
                    radioButton3.Checked = true;
                    check = "기존";
                }
            }

            else if (a == 0)
            {
                if (b == 0)
                {
                    if (c > 0)
                    {
                        radioButton3.Checked = true;
                        check = "신규";
                    }
                }
                else if (b > 0)
                {
                    if (c == 0)
                    {
                        radioButton2.Checked = true;
                        check = "신규";
                    }

                    else if (c > 0)
                    {
                        radioButton3.Checked = true;
                        check = "신규";

                    }

                }
            }
            return check;
        }

        #region //A. AirCon 작성
        private void AirCon() //설비버튼 클릭시 작동
        {
            Cooling_AirCon AirCon_Load = new Cooling_AirCon(Num, SelectCG_nonsplit, SelectCGN_nonsplit);
            DialogResult result = AirCon_Load.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (AirCon_Load.SelectCG != null)
                {
                    this.SelectCG_nonsplit = AirCon_Load.SelectCG;
                    this.SelectCGN_nonsplit = AirCon_Load.SelectCGN;

                    Split(SelectCG_nonsplit, SelectCG_split);
                    Split(SelectCGN_nonsplit, SelectCGN_split);

                    AirCon_Table();
                    AirCon_List();
                }
                else
                {
                    MessageBox.Show("냉방설비가 선택되지않았습니다.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            //이미지 넣기
            Distribute_Image();
            CoolingGeneratorImageSelect(CG, Install_f);
        }
        private void AirCon_Table()//테이블 작성하기
        {
            List<string> Item = new List<string>();

            string[][] var = Program.DB.getValue_SameCheck(DB.type.BaseDB_Cooling, "CoolSystem", " 제어유형", "냉동기유형='" + CG + "'");
            for (int h = 0; h < var.Length; h++)
            {
                Item.Add(var[h][0].ToString());
            }

            AirCon_dataGridView.Rows.Clear();
            AirCon_dataGridView.Columns.Clear();
            new StackedHeaderDecorator(AirCon_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            AirCon_dataGridView.Columns.Add(checkBoxColumn);

            AirCon_dataGridView.Columns.Add("A1", "번호");
            AirCon_dataGridView.Columns.Add("A2", "명칭");
            AirCon_dataGridView.Columns.Add("A3", "DB유형");

            DataGridViewComboBoxColumn ControlcomboBox = new DataGridViewComboBoxColumn();
            ControlcomboBox.HeaderText = "제어유형";
            ControlcomboBox.Name = "control";
            for (int i = 0; i < Item.Count; i++)  //추가
            {
                ControlcomboBox.Items.Add(Item[i]);
            }
            AirCon_dataGridView.Columns.Add(ControlcomboBox);


            DataGridViewComboBoxColumn EconomcomboBox = new DataGridViewComboBoxColumn();
            EconomcomboBox.HeaderText = "외기냉방";
            EconomcomboBox.Name = "Economizer";
            EconomcomboBox.Items.AddRange(new string[] { "있음", "없음" });
            AirCon_dataGridView.Columns.Add(EconomcomboBox);

            AirCon_dataGridView.Columns.Add("A6", "설치대수");
            AirCon_dataGridView.Columns.Add("A7", "냉방출력.[kW]");
            AirCon_dataGridView.Columns.Add("A8", "소비전력.[kW]");
            AirCon_dataGridView.Columns.Add("A9", "냉방성능.[EER]");
            AirCon_dataGridView.Columns.Add("A10", "대기전력");
            AirCon_dataGridView.Columns.Add("A11", "연료");
            AirCon_dataGridView.Columns.Add("A12", "설치");
            AirCon_dataGridView.Columns[0].Width = 40;
            AirCon_dataGridView.Columns[1].Width = 50;
        }
        private void AirCon_List()//리스트작성하기
        {
            List<string> check = new List<string>();
            check.Clear(); //추가
            Power.Clear(); //추가
            EER.Clear(); //추가


            int A = 0, B = 0, C = 0;
            double EER_f = 0, Power_f = 0, Number_f = 0;

            for (int i = 0; i < SelectCG_split.Count; i++)
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", " 번호,명칭,DB유형,냉방정격용량,냉방정격소비전력,냉방정격COP,대기전력,연료,설치",
                               "번호='" + SelectCG_split[i] + "'"); ;
                AirCon_dataGridView.Rows.Add();
                AirCon_dataGridView.Rows[i].Cells[1].Value = Value[0][0];
                AirCon_dataGridView.Rows[i].Cells[2].Value = Value[0][1];
                AirCon_dataGridView.Rows[i].Cells[3].Value = Value[0][2];
                AirCon_dataGridView.Rows[i].Cells[6].Value = SelectCGN_split[i];
                AirCon_dataGridView.Rows[i].Cells[7].Value = Value[0][3]; // 냉방출력
                AirCon_dataGridView.Rows[i].Cells[8].Value = Value[0][4]; //소비전력
                AirCon_dataGridView.Rows[i].Cells[9].Value = Value[0][5]; //COP
                AirCon_dataGridView.Rows[i].Cells[10].Value = Value[0][6]; //대기전력
                AirCon_dataGridView.Rows[i].Cells[11].Value = Value[0][7]; //연료
                AirCon_dataGridView.Rows[i].Cells[12].Value = Value[0][8]; //설치
                Program.UTIL.dataGridView_doubleComa(AirCon_dataGridView, i, 6, 0);
                Program.UTIL.dataGridView_doubleComa(AirCon_dataGridView, i, 7, 1);
                Program.UTIL.dataGridView_doubleComa(AirCon_dataGridView, i, 8, 1);
                Program.UTIL.dataGridView_doubleComa(AirCon_dataGridView, i, 9, 1);
                Program.UTIL.dataGridView_doubleComa(AirCon_dataGridView, i, 10, 1);
                check.Add(Value[0][8].ToString());
                Power.Add(Program.UTIL.dataGridView_doubleComa(AirCon_dataGridView, i, 7, 1));
                EER.Add(Program.UTIL.dataGridView_doubleComa(AirCon_dataGridView, i, 9, 1)); // +변경함
            }

            for (int h = 0; h < check.Count; h++)
            {
                if (check[h] == "기존")
                {
                    A = 1 + A;
                }
                else if (check[h] == "신규")
                {
                    B = 1 + B;
                }
                else C = 1 + C;
            }

            Install_f = installmake(A, B, C);

            for (int h = 0; h < Power.Count; h++)
            {
                EER_f += Power[h] * EER[h] * Convert.ToDouble(SelectCGN_split[h]);
                Power_f += Power[h] * Convert.ToDouble(SelectCGN_split[h]);
                Number_f += Convert.ToDouble(SelectCGN_split[h]);
            }

            EER_f = EER_f / Power_f;

            PowerTotal = Power_f;
            EERTotal = EER_f;
            PowerTotal_textBox.Text = Power_f.ToString();
            Program.UTIL.textBox_doubleComa(PowerTotal_textBox, true, 1);
            EERTotal_textBox.Text = EER_f.ToString();
            Program.UTIL.textBox_doubleComa(EERTotal_textBox, true, 1);
            InstallTotal_textBox.Text = Number_f.ToString();
            Program.UTIL.textBox_doubleComa(InstallTotal_textBox, true, 1);

            G_label.Visible = true;
            G_label.Text = string.Format("설치대수: {0}", Number_f);
            ZoneS_label.Visible = true;

        }
        private void AirCon_ReList()//로드시리스트작성하기
        {
            Program.UTIL.ffCode = true;

            double EER_f = 0, Power_f = 0, Number_f = 0;
            if (SelectCG_nonsplit != null)
            {
                Split(SelectCG_nonsplit, SelectCG_split);
                Split(SelectCGC_nonsplit, SelectCGC_split);
                Split(SelectCGE_nonsplit, SelectCGE_split);
                Split(SelectCGN_nonsplit, SelectCGN_split);

                AirCon_Table();
                AirCon_List();

                for (int i = 0; i < SelectCG_split.Count; i++)
                {
                    foreach (DataGridViewRow row in AirCon_dataGridView.Rows)
                    {
                        row.Cells[4].Value = SelectCGC_split[i];
                        row.Cells[5].Value = SelectCGE_split[i];
                    }
                }
            }
            else
            {
                MessageBox.Show("냉방설비가 로드되지않았습니다.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            StorageList_comboBox.Text = Stotype;
            StorageType_comboBox.Text = StoSource;

            Distribute_Image();
            CoolingGeneratorImageSelect(CG, Install_f);
            Load_CSource(CSource, Install_f);
            Load_StoType(Stotype, Install_f);
            Load_StoSource(StoSource);



        }
        private void AirCon_Save()//추가입력사항 저정하기
        {
            //테이블에 해당하는 정보는 모두 지우고 시작할것
            SelectCG_nonsplit = null;
            SelectCGC_nonsplit = null;
            SelectCGE_nonsplit = null;
            SelectCGN_nonsplit = null;


            for (int k = 0; k < AirCon_dataGridView.Rows.Count; k++)
            {
                for (int i = 1; i < 7; i++)
                {
                    if (AirCon_dataGridView.Rows[k].Cells[i].Value.ToString() == null)
                    {
                        MessageBox.Show("선택항목을 완료해주세요.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                if (k == AirCon_dataGridView.Rows.Count - 1)
                {
                    SelectCG_nonsplit += AirCon_dataGridView.Rows[k].Cells[1].Value.ToString();
                    SelectCGC_nonsplit += AirCon_dataGridView.Rows[k].Cells[4].Value.ToString();
                    SelectCGE_nonsplit += AirCon_dataGridView.Rows[k].Cells[5].Value.ToString();
                    SelectCGN_nonsplit += (Program.UTIL.dataGridView_doubleComa(AirCon_dataGridView, k, 6, 0)).ToString(); 
                }
                else
                {
                    SelectCG_nonsplit += AirCon_dataGridView.Rows[k].Cells[1].Value.ToString() + " + ";
                    SelectCGC_nonsplit += AirCon_dataGridView.Rows[k].Cells[4].Value.ToString() + " + ";
                    SelectCGE_nonsplit += AirCon_dataGridView.Rows[k].Cells[5].Value.ToString() + " + ";
                    SelectCGN_nonsplit += (Program.UTIL.dataGridView_doubleComa(AirCon_dataGridView, k, 6, 0)).ToString() + " + ";
                }
            }
        }
        #endregion

        #region //B. AirCooler 작성

        private void AirCooler() //설비버튼 클릭시 작동
        {
            Cooling_AirCooler AirCooler_Load = new Cooling_AirCooler(Num, SelectCG_nonsplit, SelectCGComp_nonsplit, SelectCGN_nonsplit);
            DialogResult result = AirCooler_Load.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (AirCooler_Load.SelectCG != null)
                {
                    SelectCG_nonsplit = AirCooler_Load.SelectCG;
                    SelectCGComp_nonsplit = AirCooler_Load.SelectCGComp;
                    SelectCGN_nonsplit = AirCooler_Load.SelectCGN;

                    Split(SelectCG_nonsplit, SelectCG_split);
                    Split(SelectCGComp_nonsplit, SelectCGComp_split);
                    Split(SelectCGN_nonsplit, SelectCGN_split);

                    AirCooler_Table();
                    AirCooler_List();
                }
                else
                {
                    MessageBox.Show("냉방설비가 선택되지않았습니다.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //이미지 넣기
                Distribute_Image();
                CoolingGeneratorImageSelect(CG, Install_f);
                LoadPressImage(Comp_f, Install_f);

                if (EvaType_f != null)
                {
                    LoadEvaImage(EvaType_f, Install_f);
                }

            }
        }

        private void AirCooler_Table() //테이블 작성하기
        {
            List<string> Item = new List<string>();
            Item.Clear();
            string[][] var = Program.DB.getValue_SameCheck(DB.type.BaseDB_Cooling, "CoolSystem", " 제어유형", "냉동기유형='" + CG + "'");
            for (int h = 0; h < var.Length; h++) //보완 
            {
                Item.Add(var[h][0].ToString());
            }
            AirCooler_dataGridView.Rows.Clear();
            AirCooler_dataGridView.Columns.Clear();
            new StackedHeaderDecorator(AirCooler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            AirCooler_dataGridView.Columns.Add(checkBoxColumn);

            AirCooler_dataGridView.Columns.Add("A1", "번호");
            AirCooler_dataGridView.Columns.Add("A2", "명칭");
            AirCooler_dataGridView.Columns.Add("A3", "DB유형");

            DataGridViewComboBoxColumn ControlcomboBox = new DataGridViewComboBoxColumn();
            ControlcomboBox.HeaderText = "제어유형";
            ControlcomboBox.Name = "control";
            for (int i = 0; i < Item.Count; i++)  //추가
            {
                ControlcomboBox.Items.Add(Item[i]);
            }
            AirCooler_dataGridView.Columns.Add(ControlcomboBox);

            DataGridViewComboBoxColumn EconomcomboBox = new DataGridViewComboBoxColumn();
            EconomcomboBox.HeaderText = "외기냉방";
            EconomcomboBox.Name = "Economizer";
            EconomcomboBox.Items.AddRange(new string[] { "있음", "없음" });
            AirCooler_dataGridView.Columns.Add(EconomcomboBox);

            AirCooler_dataGridView.Columns.Add("A6", "설치대수");
            AirCooler_dataGridView.Columns.Add("A7", "냉방.출력.[kW]");
            AirCooler_dataGridView.Columns.Add("A8", "냉방.전력.[kW]");
            AirCooler_dataGridView.Columns.Add("A9", "냉방.EER.[W/W]");

            AirCooler_dataGridView.Columns.Add("A10", "대기전력");
            AirCooler_dataGridView.Columns.Add("A11", "연료");
            AirCooler_dataGridView.Columns.Add("A12", "압축기");
            AirCooler_dataGridView.Columns.Add("A13", "부하측.형식");
            AirCooler_dataGridView.Columns.Add("A14", "부하측.증발기");
            AirCooler_dataGridView.Columns.Add("A15", "냉수온도.출구");
            AirCooler_dataGridView.Columns.Add("A16", "설치");

            AirCooler_dataGridView.Columns[0].Width = 40;
            AirCooler_dataGridView.Columns[1].Width = 50;
        }

        private void AirCooler_List()//리스트작성하기
        {
            List<string> check = new List<string>();
            List<double> cwin = new List<double>(), cwout = new List<double>();

            check.Clear(); //추가
            Power.Clear();//추가
            EER.Clear();//추가
            cwin.Clear();//추가
            cwout.Clear();//추가

            int A = 0, B = 0, C = 0;
            double EER_f = 0, Power_f = 0, Number_f = 0, Cwin_f = 0, Cwout_f = 0;

            for (int i = 0; i < SelectCG_split.Count; i++)
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", " 번호,명칭,DB유형,냉방정격용량,냉방정격소비전력,냉방정격COP,대기전력,연료,설치",
                               "번호='" + SelectCG_split[i] + "'");
                string[][] Val = Program.DB.getValue(DB.type.ProjDB, "User_AirCooler", " 번호,명칭,DB유형,냉방출력,냉방소비전력,EER, 대기전력,연료,부하측공급형식,증발기,냉수출구온도,설치,냉수입구온도",
                               "번호='" + SelectCG_split[i] + "'");
                if (Value.Length > 0)
                {
                    AirCooler_dataGridView.Rows.Add();
                    AirCooler_dataGridView.Rows[i].Cells[1].Value = Value[0][0];
                    AirCooler_dataGridView.Rows[i].Cells[2].Value = Value[0][1];
                    AirCooler_dataGridView.Rows[i].Cells[3].Value = Value[0][2];
                    AirCooler_dataGridView.Rows[i].Cells[6].Value = SelectCGN_split[i];
                    AirCooler_dataGridView.Rows[i].Cells[7].Value = Value[0][3]; // 냉방출력
                    AirCooler_dataGridView.Rows[i].Cells[8].Value = Value[0][4]; //소비전력
                    AirCooler_dataGridView.Rows[i].Cells[9].Value = Value[0][5]; //COP
                    AirCooler_dataGridView.Rows[i].Cells[10].Value = Value[0][6]; //대기전력
                    AirCooler_dataGridView.Rows[i].Cells[11].Value = Value[0][7]; //연료
                    AirCooler_dataGridView.Rows[i].Cells[12].Value = SelectCGComp_split[i]; //압축기
                    AirCooler_dataGridView.Rows[i].Cells[13].Value = "직팽식";
                    AirCooler_dataGridView.Rows[i].Cells[14].Value = null;
                    AirCooler_dataGridView.Rows[i].Cells[15].Value = null;
                    AirCooler_dataGridView.Rows[i].Cells[16].Value = Value[0][8]; //설치
                    Program.UTIL.dataGridView_doubleComa(AirCooler_dataGridView, i, 6, 0);
                    Program.UTIL.dataGridView_doubleComa(AirCooler_dataGridView, i, 7, 1);
                    Program.UTIL.dataGridView_doubleComa(AirCooler_dataGridView, i, 8, 1);
                    Program.UTIL.dataGridView_doubleComa(AirCooler_dataGridView, i, 9, 1);
                    Program.UTIL.dataGridView_doubleComa(AirCooler_dataGridView, i, 10, 1);
                    check.Add(Value[0][8].ToString());
                    Power.Add(Program.UTIL.dataGridView_doubleComa(AirCooler_dataGridView, i, 7, 1));
                    EER.Add(Program.UTIL.dataGridView_doubleComa(AirCooler_dataGridView, i, 9, 1));

                }
                else if (Val.Length > 0)
                {
                    AirCooler_dataGridView.Rows.Add();
                    AirCooler_dataGridView.Rows[i].Cells[1].Value = Val[0][0];
                    AirCooler_dataGridView.Rows[i].Cells[2].Value = Val[0][1];
                    AirCooler_dataGridView.Rows[i].Cells[3].Value = Val[0][2];
                    AirCooler_dataGridView.Rows[i].Cells[6].Value = SelectCGN_split[i];
                    AirCooler_dataGridView.Rows[i].Cells[7].Value =Val[0][3]; // 냉방출력
                    AirCooler_dataGridView.Rows[i].Cells[8].Value = Val[0][4]; //소비전력
                    AirCooler_dataGridView.Rows[i].Cells[9].Value = Val[0][5]; //COP
                    AirCooler_dataGridView.Rows[i].Cells[10].Value = Val[0][6]; //대기전력
                    AirCooler_dataGridView.Rows[i].Cells[11].Value = Val[0][7]; //연료
                    AirCooler_dataGridView.Rows[i].Cells[12].Value = SelectCGComp_split[i];//압축기
                    AirCooler_dataGridView.Rows[i].Cells[13].Value = Val[0][8]; //부하측공급형식
                    AirCooler_dataGridView.Rows[i].Cells[14].Value = Val[0][9]; //증발기
                    AirCooler_dataGridView.Rows[i].Cells[15].Value = Val[0][10]; //냉수출구온도
                    AirCooler_dataGridView.Rows[i].Cells[16].Value = Val[0][11]; //설치
                    Program.UTIL.dataGridView_doubleComa(AirCooler_dataGridView, i, 6, 0);
                    Program.UTIL.dataGridView_doubleComa(AirCooler_dataGridView, i, 7, 1);
                    Program.UTIL.dataGridView_doubleComa(AirCooler_dataGridView, i, 8, 1);
                    Program.UTIL.dataGridView_doubleComa(AirCooler_dataGridView, i, 9, 1);
                    Program.UTIL.dataGridView_doubleComa(AirCooler_dataGridView, i, 10, 1);

                    check.Add(Val[0][11].ToString()); //수정
                    Power.Add(Program.UTIL.dataGridView_doubleComa(AirCooler_dataGridView, i, 7, 1));
                    EER.Add(Program.UTIL.dataGridView_doubleComa(AirCooler_dataGridView, i, 9, 1));
                    if (Val[0][8] == "수방식")  //수정
                    {
                        cwin.Add(Convert.ToDouble(Val[0][10]));
                        cwout.Add(Convert.ToDouble(Val[0][12]));
                    }

                }
            }
            for (int h = 0; h < check.Count; h++)
            {
                if (check[h] == "기존")
                {
                    A = 1 + A;
                }
                else if (check[h] == "신규")
                {
                    B = 1 + B;
                }
                else C = 1 + C;
            }
            Install_f = installmake(A, B, C);

            for (int h = 0; h < Power.Count; h++)
            {
                EER_f += Power[h] * EER[h] * Convert.ToDouble(SelectCGN_split[h]);
                Power_f += Power[h] * Convert.ToDouble(SelectCGN_split[h]);
                Number_f += Convert.ToDouble(SelectCGN_split[h]);
                //수정
                if (Power.Max() == Power[h])
                {
                    Comp_f = SelectCGComp_split[h];
                    if (AirCooler_dataGridView.Rows[h].Cells[14].Value != null)
                    { EvaType_f = AirCooler_dataGridView.Rows[h].Cells[14].Value.ToString(); } //수정
                }
            }

            EER_f = EER_f / Power_f;

            if (cwin.Count > 0)
            {
                for (int k = 0; k < cwin.Count; k++)
                {
                    Cwin_f += cwin[k] * Convert.ToDouble(SelectCGN_split[k]); //직팽식, 수방식에 대한 내용 체크
                    Cwout_f += cwout[k] * Convert.ToDouble(SelectCGN_split[k]);
                }
                Cwin_f = Cwin_f / Number_f;
                Cwout_f = Cwout_f / Number_f;
            }

            PowerTotal = Power_f;
            EERTotal = EER_f;
            PowerTotal_textBox.Text = Power_f.ToString();
            Program.UTIL.textBox_doubleComa(PowerTotal_textBox, true, 1);
            EERTotal_textBox.Text = EER_f.ToString();
            Program.UTIL.textBox_doubleComa(EERTotal_textBox, true, 1);
            InstallTotal_textBox.Text = Number_f.ToString();
            Program.UTIL.textBox_doubleComa(InstallTotal_textBox, true, 1);
        }

        private void AirCooler_ReList()//로드시리스트작성하기
        {
            double EER_f = 0, Power_f = 0, Number_f = 0;

            if (SelectCG_nonsplit != null)
            {
                Split(SelectCG_nonsplit, SelectCG_split);
                Split(SelectCGC_nonsplit, SelectCGC_split);
                Split(SelectCGE_nonsplit, SelectCGE_split);
                Split(SelectCGN_nonsplit, SelectCGN_split);
                Split(SelectCGComp_nonsplit, SelectCGComp_split);

                AirCooler_Table();
                AirCooler_List();

                for (int i = 0; i < SelectCG_split.Count; i++)
                {
                    foreach (DataGridViewRow row in AirCooler_dataGridView.Rows)
                    {
                        row.Cells[4].Value = SelectCGC_split[i];
                        row.Cells[5].Value = SelectCGE_split[i];
                    }
                }
            }
            else
            {
                MessageBox.Show("냉방설비가 로드되지않았습니다.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            StorageList_comboBox.Text = Stotype;
            StorageType_comboBox.Text = StoSource;

            //그림작성
            Distribute_Image();
            CoolingGeneratorImageSelect(CG, Install_f);
            Load_CSource(CSource, Install_f); //CSource는  
            LoadPressImage(Comp_f, Install_f);
            if (EvaType_f != null)
            {
                LoadEvaImage(EvaType_f, Install_f);
            }
            //축열 그림넣기
            Load_StoType(Stotype, Install_f);
            Load_StoSource(StoSource);
        }

        private void AirCooler_Save()
        {
            SelectCG_nonsplit = null;//추가
            SelectCGC_nonsplit = null;//추가
            SelectCGE_nonsplit = null;//추가
            SelectCGN_nonsplit = null; //추가
            SelectCGComp_nonsplit = null; //추가

            for (int k = 0; k < AirCooler_dataGridView.Rows.Count; k++)
            {
                for (int i = 4; i < 6; i++) //수정함
                {
                    if (AirCooler_dataGridView.Rows[k].Cells[i].Value.ToString() == null)
                    {
                        MessageBox.Show("선택항목을 완료해주세요.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                if (k == AirCooler_dataGridView.Rows.Count - 1)
                {
                    SelectCG_nonsplit += AirCooler_dataGridView.Rows[k].Cells[1].Value.ToString();
                    SelectCGC_nonsplit += AirCooler_dataGridView.Rows[k].Cells[4].Value.ToString();
                    SelectCGE_nonsplit += AirCooler_dataGridView.Rows[k].Cells[5].Value.ToString();
                    SelectCGN_nonsplit += (Program.UTIL.dataGridView_doubleComa(AirCooler_dataGridView, k, 6, 0)).ToString();
                    SelectCGComp_nonsplit += AirCooler_dataGridView.Rows[k].Cells[12].Value.ToString();
                }
                else
                {
                    SelectCG_nonsplit += AirCooler_dataGridView.Rows[k].Cells[1].Value.ToString() + " + ";
                    SelectCGC_nonsplit += AirCooler_dataGridView.Rows[k].Cells[4].Value.ToString() + " + ";
                    SelectCGE_nonsplit += AirCooler_dataGridView.Rows[k].Cells[5].Value.ToString() + " + ";
                    SelectCGN_nonsplit += (Program.UTIL.dataGridView_doubleComa(AirCooler_dataGridView, k, 6, 0)) + " + ";
                    SelectCGComp_nonsplit += AirCooler_dataGridView.Rows[k].Cells[12].Value.ToString() + " + ";
                }
            }
        }

        #endregion

        #region //C. WaterCooler 작성
        private void WaterCooler()
        {
            Cooling_WaterCooler WaterCooler_Load = new Cooling_WaterCooler(Num, SelectCG_nonsplit, SelectCGN_nonsplit);
            DialogResult result = WaterCooler_Load.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (WaterCooler_Load.SelectCG != null)
                {
                    SelectCG_nonsplit = WaterCooler_Load.SelectCG;
                    SelectCGN_nonsplit = WaterCooler_Load.SelectCGN;

                    Split(WaterCooler_Load.SelectCG, SelectCG_split);
                    Split(WaterCooler_Load.SelectCGN, SelectCGN_split);

                    WaterCooler_Table();
                    WaterCooler_List();
                }
                else
                {
                    MessageBox.Show("냉방설비가 선택되지않았습니다.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            //그림작성
            Distribute_Image();
            CoolingGeneratorImageSelect(CG, Install_f);
            LoadPressImage(Comp_f, Install_f);
            LoadEvaImage(EvaType_f, Install_f);
            //냉수공급온도, 냉수입구온도
        }
        private void WaterCooler_Table()
        {
            List<string> Item = new List<string>();
            Item.Clear();
            string[][] item = Program.DB.getValue_SameCheck(DB.type.BaseDB_Cooling, "CoolSystem", " 제어유형", "냉동기유형='" + CG + "'");
            foreach (string[] s in item)
            {
                Item.Add(s[0]);
            }
            WaterCooler_dataGridView.Rows.Clear();
            WaterCooler_dataGridView.Columns.Clear();
            new StackedHeaderDecorator(WaterCooler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            WaterCooler_dataGridView.Columns.Add(checkBoxColumn);

            WaterCooler_dataGridView.Columns.Add("A1", "번호");
            WaterCooler_dataGridView.Columns.Add("A2", "명칭");
            WaterCooler_dataGridView.Columns.Add("A3", "DB유형");
            WaterCooler_dataGridView.Columns.Add("A4", "제어유형");

            DataGridViewComboBoxColumn EconomcomboBox = new DataGridViewComboBoxColumn();
            EconomcomboBox.HeaderText = "외기냉방";
            EconomcomboBox.Name = "Economizer";
            EconomcomboBox.Items.AddRange(new string[] { "있음", "없음" });
            WaterCooler_dataGridView.Columns.Add(EconomcomboBox);

            WaterCooler_dataGridView.Columns.Add("A6", "설치대수");
            WaterCooler_dataGridView.Columns.Add("A7", "냉방.출력.[kW]");
            WaterCooler_dataGridView.Columns.Add("A8", "냉방.전력.[kW]");
            WaterCooler_dataGridView.Columns.Add("A9", "냉방.EER.[W/W]");

            WaterCooler_dataGridView.Columns.Add("A10", "대기전력");
            WaterCooler_dataGridView.Columns.Add("A11", "연료");
            WaterCooler_dataGridView.Columns.Add("A12", "압축기");
            WaterCooler_dataGridView.Columns.Add("A13", "부하측.증발기");
            WaterCooler_dataGridView.Columns.Add("A14", "냉수온도.출구.[℃]");
            WaterCooler_dataGridView.Columns.Add("A15", "냉수온도.입구.[℃]");
            WaterCooler_dataGridView.Columns.Add("A16", "설치");

            WaterCooler_dataGridView.Columns[0].Width = 40;
            WaterCooler_dataGridView.Columns[1].Width = 50;
        }
        private void WaterCooler_List()
        {
            List<string> check = new List<string>(); //설치
            List<double> cwin = new List<double>(), cwout = new List<double>();
            check.Clear();
            Power.Clear();
            EER.Clear();


            int A = 0, B = 0, C = 0;
            double EER_f = 0, Power_f = 0, Number_f = 0, Cwin_f = 0, Cwout_f = 0;
            for (int i = 0; i < SelectCG_split.Count; i++)
            {
                string[][] Val = Program.DB.getValue(DB.type.ProjDB, "User_WaterCooler", " 번호,명칭,DB유형,냉방출력,냉방소비전력,EER, 대기전력,연료,압축기,증발기,냉수출구온도,냉수입구온도,설치",
                                "번호='" + SelectCG_split[i] + "'");
                WaterCooler_dataGridView.Rows.Add();
                WaterCooler_dataGridView.Rows[i].Cells[1].Value = Val[0][0];
                WaterCooler_dataGridView.Rows[i].Cells[2].Value = Val[0][1];
                WaterCooler_dataGridView.Rows[i].Cells[3].Value = Val[0][2];

                DataGridViewComboBoxCell 제어comboBox = new DataGridViewComboBoxCell();
                string[][] 제어 = Program.DB.getValue(DB.type.BaseDB_Cooling, "부분부하계수", "제어유형", "설비유형 = '수냉식냉동기' AND 공급유형 ='" + Val[i][8] + "'");
                if (제어.Length > 0)
                {
                    for (int j = 0; j < 제어.Length; j++)
                    {
                        제어comboBox.Items.Add(제어[j][0]);
                    }
                }
                WaterCooler_dataGridView.Rows[i].Cells[4] = 제어comboBox;

                WaterCooler_dataGridView.Rows[i].Cells[6].Value = SelectCGN_split[i];
                WaterCooler_dataGridView.Rows[i].Cells[7].Value = Val[0][3]; // 냉방출력
                WaterCooler_dataGridView.Rows[i].Cells[8].Value = Val[0][4]; //소비전력
                WaterCooler_dataGridView.Rows[i].Cells[9].Value = Val[0][5]; //COP
                WaterCooler_dataGridView.Rows[i].Cells[10].Value = Val[0][6]; //대기전력
                WaterCooler_dataGridView.Rows[i].Cells[11].Value = Val[0][7]; //연료
                WaterCooler_dataGridView.Rows[i].Cells[12].Value = Val[0][8]; //압축기
                WaterCooler_dataGridView.Rows[i].Cells[13].Value = Val[0][9]; //증발기
                WaterCooler_dataGridView.Rows[i].Cells[14].Value = Val[0][10]; //냉수출구온도
                WaterCooler_dataGridView.Rows[i].Cells[15].Value = Val[0][11]; //냉수입구온도
                WaterCooler_dataGridView.Rows[i].Cells[16].Value = Val[0][12]; //설치
                Program.UTIL.dataGridView_doubleComa(WaterCooler_dataGridView, i, 6, 0);
                Program.UTIL.dataGridView_doubleComa(WaterCooler_dataGridView, i, 7, 1);
                Program.UTIL.dataGridView_doubleComa(WaterCooler_dataGridView, i, 8, 1);
                Program.UTIL.dataGridView_doubleComa(WaterCooler_dataGridView, i, 9, 1);
                Program.UTIL.dataGridView_doubleComa(WaterCooler_dataGridView, i, 10, 1);
                check.Add(Val[0][12].ToString());
                Power.Add(Program.UTIL.dataGridView_doubleComa(WaterCooler_dataGridView, i, 7, 1));
                EER.Add(Program.UTIL.dataGridView_doubleComa(WaterCooler_dataGridView, i, 9, 1));
                cwout.Add(Program.UTIL.dataGridView_doubleComa(WaterCooler_dataGridView, i, 14, 1)); //수정
                cwin.Add(Program.UTIL.dataGridView_doubleComa(WaterCooler_dataGridView, i, 15, 1)); //수정

                //압축기를 작성해야함
            }
            for (int h = 0; h < check.Count; h++)
            {
                if (check[h] == "기존")
                {
                    A = 1 + A;
                }
                else if (check[h] == "신규")
                {
                    B = 1 + B;
                }
                else C = 1 + C;
            }
            Install_f = installmake(A, B, C);

            for (int h = 0; h < Power.Count; h++)
            {
                EER_f += Power[h] * EER[h] * Convert.ToDouble(SelectCGN_split[h]);
                Power_f += Power[h] * Convert.ToDouble(SelectCGN_split[h]);
                Cwin_f += cwin[h] * Convert.ToDouble(SelectCGN_split[h]);
                Cwout_f += cwout[h] * Convert.ToDouble(SelectCGN_split[h]);
                Number_f += Convert.ToDouble(SelectCGN_split[h]);
                if (Power.Max() == Power[h])
                {
                    Comp_f = WaterCooler_dataGridView.Rows[h].Cells[12].Value.ToString();
                    EvaType_f = WaterCooler_dataGridView.Rows[h].Cells[13].Value.ToString();
                }
            }
            EER_f = EER_f / Power_f;
            Cwin_f = Cwin_f / Number_f;
            Cwout_f = Cwout_f / Number_f;

            PowerTotal = Power_f;
            EERTotal = EER_f;
            PowerTotal_textBox.Text = Power_f.ToString();
            Program.UTIL.textBox_doubleComa(PowerTotal_textBox, true, 1);
            EERTotal_textBox.Text = EER_f.ToString();
            Program.UTIL.textBox_doubleComa(EERTotal_textBox, true, 1);
            InstallTotal_textBox.Text = Number_f.ToString();
            Program.UTIL.textBox_doubleComa(InstallTotal_textBox, true, 1);
            //냉수입구온도, 출구온도 글씨 작성
        }

        private void WaterCooler_ReList()
        {
            if (SelectCG_nonsplit != null)
            {
                Split(SelectCG_nonsplit, SelectCG_split);
                Split(SelectCGC_nonsplit, SelectCGC_split);
                Split(SelectCGE_nonsplit, SelectCGE_split);
                Split(SelectCGN_nonsplit, SelectCGN_split);
                Split(SelectCT_nonsplit, SelectCT_split);
                Split(SelectCTN_nonsplit, SelectCTN_split);

                WaterCooler_Table();
                WaterCooler_List();

                for (int i = 0; i < SelectCG_split.Count; i++)
                {
                    foreach (DataGridViewRow row in WaterCooler_dataGridView.Rows)
                    {
                        row.Cells[4].Value = SelectCGC_split[i];
                        row.Cells[5].Value = SelectCGE_split[i];
                    }
                }
                CoolingTop_Table();
                CoolingTop_List();
            }
            else
            {
                MessageBox.Show("냉방설비가 로드되지않았습니다.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            StorageList_comboBox.Text = Stotype;
            StorageType_comboBox.Text = StoSource;

            //그림작성
            Distribute_Image();
            CoolingGeneratorImageSelect(CG, Install_f);
            Load_CSource(CSource, Install_f); //CSource는  
            LoadPressImage(Comp_f, Install_f);
            LoadEvaImage(EvaType_f, Install_f);
            Load_StoType(Stotype, Install_f);
            Load_StoSource(StoSource);
        }
        private void WaterCooler_Save()
        {
            SelectCG_nonsplit = null;
            SelectCGC_nonsplit = null;
            SelectCGE_nonsplit = null;
            SelectCGN_nonsplit = null;

            for (int k = 0; k < WaterCooler_dataGridView.Rows.Count; k++)
            {
                for (int i = 4; i < 6; i++)
                {
                    if (WaterCooler_dataGridView.Rows[k].Cells[i].Value.ToString() == null)
                    {
                        MessageBox.Show("선택항목을 완료해주세요.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                if (k == WaterCooler_dataGridView.Rows.Count - 1)
                {
                    SelectCG_nonsplit += WaterCooler_dataGridView.Rows[k].Cells[1].Value.ToString();
                    SelectCGC_nonsplit += WaterCooler_dataGridView.Rows[k].Cells[4].Value.ToString();
                    SelectCGE_nonsplit += WaterCooler_dataGridView.Rows[k].Cells[5].Value.ToString();
                    SelectCGN_nonsplit += (Program.UTIL.dataGridView_doubleComa(WaterCooler_dataGridView, k, 6, 0)).ToString();
                }
                else
                {
                    SelectCG_nonsplit += WaterCooler_dataGridView.Rows[k].Cells[1].Value.ToString() + " + ";
                    SelectCGC_nonsplit += WaterCooler_dataGridView.Rows[k].Cells[4].Value.ToString() + " + ";
                    SelectCGE_nonsplit += WaterCooler_dataGridView.Rows[k].Cells[5].Value.ToString() + " + ";
                    SelectCGN_nonsplit += (Program.UTIL.dataGridView_doubleComa(WaterCooler_dataGridView, k, 6, 0)).ToString() + " + ";
                }
            }
        }
        #endregion
        // ////////////////////////////////////////////////////////////////////////지열히트펌프///////////////////////////////////////////////////
        #region //D. SoilCooler 작성
        private void SoilCooler()
        {
            Cooling_SoilCooler SoilCooler_Load = new Cooling_SoilCooler(Num, SelectCG_nonsplit, SelectCGN_nonsplit);
            DialogResult result = SoilCooler_Load.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (SoilCooler_Load.SelectCG != null)
                {
                    SelectCG_nonsplit = SoilCooler_Load.SelectCG;
                    SelectCGN_nonsplit = SoilCooler_Load.SelectCGN;

                    Split(SoilCooler_Load.SelectCG, SelectCG_split);
                    Split(SoilCooler_Load.SelectCGN, SelectCGN_split);

                    SoilCooler_Table();
                    SoilCooler_List();
                }
                else
                {
                    MessageBox.Show("냉방설비가 선택되지않았습니다.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            //그림작성
            Distribute_Image();
            CoolingGeneratorImageSelect(CG, Install_f);
            LoadPressImage(Comp_f, Install_f);
            LoadEvaImage(EvaType_f, Install_f);
            //냉수공급온도, 냉수입구온도
        }
        private void SoilCooler_Table()
        {
            List<string> Item = new List<string>();
            Item.Clear();
            string[][] item = Program.DB.getValue_SameCheck(DB.type.BaseDB_Cooling, "CoolSystem", " 제어유형", "냉동기유형='" + CG + "'");
            foreach (string[] s in item)
            {
                Item.Add(s[0]);
            }
            SoilCooler_dataGridView.Rows.Clear();
            SoilCooler_dataGridView.Columns.Clear();
            new StackedHeaderDecorator(SoilCooler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            SoilCooler_dataGridView.Columns.Add(checkBoxColumn);

            SoilCooler_dataGridView.Columns.Add("A1", "번호");
            SoilCooler_dataGridView.Columns.Add("A2", "명칭");
            SoilCooler_dataGridView.Columns.Add("A3", "DB유형");
            SoilCooler_dataGridView.Columns.Add("A4", "제어유형");

            DataGridViewComboBoxColumn EconomcomboBox = new DataGridViewComboBoxColumn();
            EconomcomboBox.HeaderText = "외기냉방";
            EconomcomboBox.Name = "Economizer";
            EconomcomboBox.Items.AddRange(new string[] { "있음", "없음" });
            SoilCooler_dataGridView.Columns.Add(EconomcomboBox);
            SoilCooler_dataGridView.Columns.Add("A6", "설치대수");
            SoilCooler_dataGridView.Columns.Add("A7", "냉방.출력.[kW]");
            SoilCooler_dataGridView.Columns.Add("A8", "냉방.전력.[kW]");
            SoilCooler_dataGridView.Columns.Add("A9", "냉방.EER.[W/W]");

            SoilCooler_dataGridView.Columns.Add("A10", "대기전력");
            SoilCooler_dataGridView.Columns.Add("A11", "연료");
            SoilCooler_dataGridView.Columns.Add("A12", "압축기");
            SoilCooler_dataGridView.Columns.Add("A13", "부하측.증발기");
            SoilCooler_dataGridView.Columns.Add("A14", "냉수온도.출구.[℃]");
            SoilCooler_dataGridView.Columns.Add("A15", "냉수온도.입구.[℃]");
            SoilCooler_dataGridView.Columns.Add("A16", "설치");

            SoilCooler_dataGridView.Columns[0].Width = 40;
            SoilCooler_dataGridView.Columns[1].Width = 50;
        }
        private void SoilCooler_List()
        {
            List<string> check = new List<string>(), type = new List<string>();
            List<double> cwin = new List<double>(), cwout = new List<double>();
            check.Clear();
            Power.Clear();
            EER.Clear();


            int A = 0, B = 0, C = 0;
            double EER_f = 0, Power_f = 0, Number_f = 0, Cwin_f = 0, Cwout_f = 0;
            string source = null;
            for (int i = 0; i < SelectCG_split.Count; i++)
            {
                string[][] Val = Program.DB.getValue(DB.type.ProjDB, "User_GroundHP", " 번호,명칭,DB유형,냉방용량,냉방소비전력,냉방EER, 대기전력,연료,압축기,증발기,냉수출구온도,냉수입구온도,설치,수직수평",
                                "번호='" + SelectCG_split[i] + "'");
                SoilCooler_dataGridView.Rows.Add();
                SoilCooler_dataGridView.Rows[i].Cells[1].Value = Val[0][0];
                SoilCooler_dataGridView.Rows[i].Cells[2].Value = Val[0][1];
                SoilCooler_dataGridView.Rows[i].Cells[3].Value = Val[0][2];

                DataGridViewComboBoxCell 제어comboBox = new DataGridViewComboBoxCell();
                string[][] 제어 = Program.DB.getValue(DB.type.BaseDB_Cooling, "부분부하계수", "제어유형", "설비유형 = '지열히트펌프' AND 공급유형 ='" + Val[i][8] + "'");
                if (제어.Length > 0)
                {
                    for (int j = 0; j < 제어.Length; j++)
                    {
                        제어comboBox.Items.Add(제어[j][0]);
                    }
                }
                SoilCooler_dataGridView.Rows[i].Cells[4] = 제어comboBox;

                SoilCooler_dataGridView.Rows[i].Cells[6].Value = SelectCGN_split[i];
                SoilCooler_dataGridView.Rows[i].Cells[7].Value = Val[0][3]; // 냉방출력
                SoilCooler_dataGridView.Rows[i].Cells[8].Value = Val[0][4]; //소비전력`
                SoilCooler_dataGridView.Rows[i].Cells[9].Value = Val[0][5]; //COP
                SoilCooler_dataGridView.Rows[i].Cells[10].Value = Val[0][6]; //대기전력
                SoilCooler_dataGridView.Rows[i].Cells[11].Value = Val[0][7]; //연료
                SoilCooler_dataGridView.Rows[i].Cells[12].Value = Val[0][8]; //압축기
                SoilCooler_dataGridView.Rows[i].Cells[13].Value = Val[0][9]; //증발기
                SoilCooler_dataGridView.Rows[i].Cells[14].Value = Val[0][10]; //냉수출구온도
                SoilCooler_dataGridView.Rows[i].Cells[15].Value = Val[0][11]; //냉수입구온도
                SoilCooler_dataGridView.Rows[i].Cells[16].Value = Val[0][12]; //설치
                Program.UTIL.dataGridView_doubleComa(SoilCooler_dataGridView, i, 6, 0);
                Program.UTIL.dataGridView_doubleComa(SoilCooler_dataGridView, i, 7, 1);
                Program.UTIL.dataGridView_doubleComa(SoilCooler_dataGridView, i, 8, 1);
                Program.UTIL.dataGridView_doubleComa(SoilCooler_dataGridView, i, 9, 1);
                Program.UTIL.dataGridView_doubleComa(SoilCooler_dataGridView, i, 10, 1);
                check.Add(Val[0][12].ToString());
                type.Add(Val[0][13].ToString());
                Power.Add(Program.UTIL.dataGridView_doubleComa(SoilCooler_dataGridView, i, 7, 1));
                EER.Add(Program.UTIL.dataGridView_doubleComa(SoilCooler_dataGridView, i, 9, 1));
                cwout.Add(Program.UTIL.dataGridView_doubleComa(SoilCooler_dataGridView, i, 14, 1));
                cwin.Add(Program.UTIL.dataGridView_doubleComa(SoilCooler_dataGridView, i, 15, 1));

            }
            for (int h = 0; h < check.Count; h++)
            {
                if (check[h] == "기존")
                {
                    A = 1 + A;
                }
                else if (check[h] == "신규")
                {
                    B = 1 + B;
                }
                else C = 1 + C;
            }
            Install_f = installmake(A, B, C);

            for (int h = 0; h < Power.Count; h++)
            {
                EER_f += Power[h] * EER[h] * Convert.ToDouble(SelectCGN_split[h]);
                Power_f += Power[h] * Convert.ToDouble(SelectCGN_split[h]);
                Cwin_f += cwin[h] * Convert.ToDouble(SelectCGN_split[h]);
                Cwout_f += cwout[h] * Convert.ToDouble(SelectCGN_split[h]);
                Number_f += Convert.ToDouble(SelectCGN_split[h]);
                if (Power.Max() == Power[h])
                {
                    Comp_f = SoilCooler_dataGridView.Rows[h].Cells[12].Value.ToString();
                    EvaType_f = SoilCooler_dataGridView.Rows[h].Cells[13].Value.ToString();
                    source = type[h];
                }
            }
            EER_f = EER_f / Power_f;
            Cwin_f = Cwin_f / Number_f;
            Cwout_f = Cwout_f / Number_f;

            PowerTotal = Power_f;
            EERTotal = EER_f;
            PowerTotal_textBox.Text = Power_f.ToString();
            Program.UTIL.textBox_doubleComa(PowerTotal_textBox, true, 1);
            EERTotal_textBox.Text = EER_f.ToString();
            Program.UTIL.textBox_doubleComa(EERTotal_textBox, true, 1);
            InstallTotal_textBox.Text = Number_f.ToString();
            Program.UTIL.textBox_doubleComa(InstallTotal_textBox, true, 1);
            //냉수입구온도, 출구온도 글씨 작성
            if (source == "수평형")
            {
                CSource = "지열H";
                Install_comboBox.Text = CSource;
            }
            else if (source == "수직형")
            {
                CSource = "지열";
                Install_comboBox.Text = CSource;
            }
            Load_CSource(CSource, Install_f);
        }
        private void SoilCooler_ReList()
        {
            if (SelectCG_nonsplit != null)
            {
                Split(SelectCG_nonsplit, SelectCG_split);
                Split(SelectCGC_nonsplit, SelectCGC_split);
                Split(SelectCGE_nonsplit, SelectCGE_split);
                Split(SelectCGN_nonsplit, SelectCGN_split);


                SoilCooler_Table();
                SoilCooler_List();

                for (int i = 0; i < SelectCG_split.Count; i++)
                {
                    foreach (DataGridViewRow row in SoilCooler_dataGridView.Rows)
                    {
                        row.Cells[4].Value = SelectCGC_split[i];
                        row.Cells[5].Value = SelectCGE_split[i];
                    }
                }
            }
            else
            {
                MessageBox.Show("냉방설비가 로드되지않았습니다.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            StorageList_comboBox.Text = Stotype;
            StorageType_comboBox.Text = StoSource;

            //그림작성
            Distribute_Image();
            CoolingGeneratorImageSelect(CG, Install_f);
            //Load_CSource(CSource, Install_f);  // ?  
            LoadPressImage(Comp_f, Install_f);
            LoadEvaImage(EvaType_f, Install_f);
            Load_StoType(Stotype, Install_f);
            Load_StoSource(StoSource);
        }
        private void SoilCooler_Save()
        {
            SelectCG_nonsplit = null;
            SelectCGC_nonsplit = null;
            SelectCGE_nonsplit = null;
            SelectCGN_nonsplit = null;

            for (int k = 0; k < SoilCooler_dataGridView.Rows.Count; k++)
            {
                for (int i = 4; i < 6; i++)
                {
                    if (SoilCooler_dataGridView.Rows[k].Cells[i].Value.ToString() == null)
                    {
                        MessageBox.Show("선택항목을 완료해주세요.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                if (k == SoilCooler_dataGridView.Rows.Count - 1)
                {
                    SelectCG_nonsplit += SoilCooler_dataGridView.Rows[k].Cells[1].Value.ToString();
                    SelectCGC_nonsplit += SoilCooler_dataGridView.Rows[k].Cells[4].Value.ToString();
                    SelectCGE_nonsplit += SoilCooler_dataGridView.Rows[k].Cells[5].Value.ToString();
                    SelectCGN_nonsplit += SoilCooler_dataGridView.Rows[k].Cells[6].Value.ToString();
                    Program.UTIL.dataGridView_doubleComa(SoilCooler_dataGridView, k, 6, 0);
                }
                else
                {
                    SelectCG_nonsplit += SoilCooler_dataGridView.Rows[k].Cells[1].Value.ToString() + " + ";
                    SelectCGC_nonsplit += SoilCooler_dataGridView.Rows[k].Cells[4].Value.ToString() + " + ";
                    SelectCGE_nonsplit += SoilCooler_dataGridView.Rows[k].Cells[5].Value.ToString() + " + ";
                    SelectCGN_nonsplit += SoilCooler_dataGridView.Rows[k].Cells[6].Value.ToString() + " + ";
                    Program.UTIL.dataGridView_doubleComa(SoilCooler_dataGridView, k, 6, 0);
                }
            }
        }

        private void SoilSourceMake(string _Source, string _Install)
        {

        }

        #endregion
        // ///////////////////////////////////////////////////////////////지하수히트펌프//////////////////////////////////////////////////////
        #region //E. SoilWaterCooler 작성
        private void SoilWaterCooler()
        {
            Cooling_SoilWaterCooler SoilWaterCooler_Load = new Cooling_SoilWaterCooler(Num, SelectCG_nonsplit, SelectCGN_nonsplit);
            DialogResult result = SoilWaterCooler_Load.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (SoilWaterCooler_Load.SelectCG != null)
                {
                    SelectCG_nonsplit = SoilWaterCooler_Load.SelectCG;
                    SelectCGN_nonsplit = SoilWaterCooler_Load.SelectCGN;

                    Split(SoilWaterCooler_Load.SelectCG, SelectCG_split);
                    Split(SoilWaterCooler_Load.SelectCGN, SelectCGN_split);

                    SoilWaterCooler_Table();
                    SoilWaterCooler_List();
                }
                else
                {
                    MessageBox.Show("냉방설비가 선택되지않았습니다.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            //그림작성
            Distribute_Image();
            CoolingGeneratorImageSelect(CG, Install_f);
            LoadPressImage(Comp_f, Install_f);
            LoadEvaImage(EvaType_f, Install_f);
            //냉수공급온도, 냉수입구온
        }
        private void SoilWaterCooler_Table()
        {
            List<string> Item = new List<string>();
            Item.Clear();
            string[][] item = Program.DB.getValue_SameCheck(DB.type.BaseDB_Cooling, "CoolSystem", " 제어유형", "냉동기유형='" + CG + "'");
            foreach (string[] s in item)
            {
                Item.Add(s[0]);
            }
            SoilWaterCooler_dataGridView.Rows.Clear();
            SoilWaterCooler_dataGridView.Columns.Clear();
            new StackedHeaderDecorator(SoilWaterCooler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            SoilWaterCooler_dataGridView.Columns.Add(checkBoxColumn);

            SoilWaterCooler_dataGridView.Columns.Add("A1", "번호");
            SoilWaterCooler_dataGridView.Columns.Add("A2", "명칭");
            SoilWaterCooler_dataGridView.Columns.Add("A3", "DB유형");
            SoilWaterCooler_dataGridView.Columns.Add("A4", "제어유형");

            DataGridViewComboBoxColumn EconomcomboBox = new DataGridViewComboBoxColumn();
            EconomcomboBox.HeaderText = "외기냉방";
            EconomcomboBox.Name = "Economizer";
            EconomcomboBox.Items.AddRange(new string[] { "있음", "없음" });
            SoilWaterCooler_dataGridView.Columns.Add(EconomcomboBox);
            SoilWaterCooler_dataGridView.Columns.Add("A6", "설치대수");
            SoilWaterCooler_dataGridView.Columns.Add("A7", "냉방.출력.[kW]");
            SoilWaterCooler_dataGridView.Columns.Add("A8", "냉방.전력.[kW]");
            SoilWaterCooler_dataGridView.Columns.Add("A9", "냉방.EER.[W/W]");

            SoilWaterCooler_dataGridView.Columns.Add("A10", "대기전력");
            SoilWaterCooler_dataGridView.Columns.Add("A11", "연료");
            SoilWaterCooler_dataGridView.Columns.Add("A12", "압축기");
            SoilWaterCooler_dataGridView.Columns.Add("A13", "부하측.증발기");
            SoilWaterCooler_dataGridView.Columns.Add("A14", "냉수온도.출구.[℃]");
            SoilWaterCooler_dataGridView.Columns.Add("A15", "냉수온도.입구.[℃]");
            SoilWaterCooler_dataGridView.Columns.Add("A16", "설치");

            SoilWaterCooler_dataGridView.Columns[0].Width = 40;
            SoilWaterCooler_dataGridView.Columns[1].Width = 50;
        }
        private void SoilWaterCooler_List()
        {
            List<string> check = new List<string>(), type = new List<string>();
            List<double> cwin = new List<double>(), cwout = new List<double>();
            check.Clear();
            Power.Clear();
            EER.Clear();


            int A = 0, B = 0, C = 0;
            double EER_f = 0, Power_f = 0, Number_f = 0, Cwin_f = 0, Cwout_f = 0;
            string source = null;
            for (int i = 0; i < SelectCG_split.Count; i++)
            {
                string[][] Val = Program.DB.getValue(DB.type.ProjDB, "User_GroundWHP", " 번호,명칭,DB유형,냉방용량,냉방소비전력,냉방EER, 대기전력,연료,압축기,증발기,냉수출구온도,냉수입구온도,설치,수직수평",
                                "번호='" + SelectCG_split[i] + "'");
                SoilWaterCooler_dataGridView.Rows.Add();
                SoilWaterCooler_dataGridView.Rows[i].Cells[1].Value = Val[0][0];
                SoilWaterCooler_dataGridView.Rows[i].Cells[2].Value = Val[0][1];
                SoilWaterCooler_dataGridView.Rows[i].Cells[3].Value = Val[0][2];
                DataGridViewComboBoxCell 제어comboBox = new DataGridViewComboBoxCell();
                string[][] 제어 = Program.DB.getValue(DB.type.BaseDB_Cooling, "부분부하계수", "제어유형", "설비유형 = '지열히트펌프' AND 공급유형 ='" + Val[i][8] + "'");
                if (제어.Length > 0)
                {
                    for (int j = 0; j < 제어.Length; j++)
                    {
                        제어comboBox.Items.Add(제어[j][0]);
                    }
                }
                SoilWaterCooler_dataGridView.Rows[i].Cells[4] = 제어comboBox;

                SoilWaterCooler_dataGridView.Rows[i].Cells[6].Value = SelectCGN_split[i];
                SoilWaterCooler_dataGridView.Rows[i].Cells[7].Value = Val[0][3]; // 냉방출력
                SoilWaterCooler_dataGridView.Rows[i].Cells[8].Value = Val[0][4]; //소비전력`
                SoilWaterCooler_dataGridView.Rows[i].Cells[9].Value = Val[0][5]; //COP
                SoilWaterCooler_dataGridView.Rows[i].Cells[10].Value = Val[0][6]; //대기전력
                SoilWaterCooler_dataGridView.Rows[i].Cells[11].Value = Val[0][7]; //연료
                SoilWaterCooler_dataGridView.Rows[i].Cells[12].Value = Val[0][8]; //압축기
                SoilWaterCooler_dataGridView.Rows[i].Cells[13].Value = Val[0][9]; //증발기
                SoilWaterCooler_dataGridView.Rows[i].Cells[14].Value = Val[0][10]; //냉수출구온도
                SoilWaterCooler_dataGridView.Rows[i].Cells[15].Value = Val[0][11]; //냉수입구온도
                SoilWaterCooler_dataGridView.Rows[i].Cells[16].Value = Val[0][12]; //설치
                check.Add(Val[0][12].ToString());
                type.Add(Val[0][13].ToString());
                Program.UTIL.dataGridView_doubleComa(SoilWaterCooler_dataGridView, i, 6, 0);
                Program.UTIL.dataGridView_doubleComa(SoilWaterCooler_dataGridView, i, 7, 1);
                Program.UTIL.dataGridView_doubleComa(SoilWaterCooler_dataGridView, i, 8, 1);
                Program.UTIL.dataGridView_doubleComa(SoilWaterCooler_dataGridView, i, 9, 1);
                Program.UTIL.dataGridView_doubleComa(SoilWaterCooler_dataGridView, i, 10, 1);
                Power.Add(Program.UTIL.dataGridView_doubleComa(SoilWaterCooler_dataGridView, i, 7, 1));
                EER.Add(Program.UTIL.dataGridView_doubleComa(SoilWaterCooler_dataGridView, i, 9, 1));
                cwout.Add(Program.UTIL.dataGridView_doubleComa(SoilWaterCooler_dataGridView, i, 14, 1));
                cwin.Add(Program.UTIL.dataGridView_doubleComa(SoilWaterCooler_dataGridView, i, 15, 1));

            }
            for (int h = 0; h < check.Count; h++)
            {
                if (check[h] == "기존")
                {
                    A = 1 + A;
                }
                else if (check[h] == "신규")
                {
                    B = 1 + B;
                }
                else C = 1 + C;
            }
            Install_f = installmake(A, B, C);

            for (int h = 0; h < Power.Count; h++)
            {
                EER_f += Power[h] * EER[h] * Convert.ToDouble(SelectCGN_split[h]);
                Power_f += Power[h] * Convert.ToDouble(SelectCGN_split[h]);
                Cwin_f += cwin[h] * Convert.ToDouble(SelectCGN_split[h]);
                Cwout_f += cwout[h] * Convert.ToDouble(SelectCGN_split[h]);
                Number_f += Convert.ToDouble(SelectCGN_split[h]);
                if (Power.Max() == Power[h])
                {
                    Comp_f = SoilWaterCooler_dataGridView.Rows[h].Cells[12].Value.ToString();
                    EvaType_f = SoilWaterCooler_dataGridView.Rows[h].Cells[13].Value.ToString();
                    source = type[h];
                }
            }
            EER_f = EER_f / Power_f;
            Cwin_f = Cwin_f / Number_f;
            Cwout_f = Cwout_f / Number_f;

            PowerTotal = Power_f;
            EERTotal = EER_f;
            PowerTotal_textBox.Text = Power_f.ToString();
            Program.UTIL.textBox_doubleComa(PowerTotal_textBox, true, 1);
            EERTotal_textBox.Text = EER_f.ToString();
            Program.UTIL.textBox_doubleComa(EERTotal_textBox, true, 1);
            InstallTotal_textBox.Text = Number_f.ToString();
            Program.UTIL.textBox_doubleComa(InstallTotal_textBox, true, 1);
            //냉수입구온도, 출구온도 글씨 작성
            if (source == "수평형")
            {
                CSource = "지하수";
                Install_comboBox.Text = CSource;
            }
            else if (source == "수직형")
            {
                CSource = "지하수";
                Install_comboBox.Text = CSource;
            }
            Load_CSource(CSource, Install_f);
        }

        private void SoilWaterCooler_ReList()
        {
            if (SelectCG_nonsplit != null)
            {
                Split(SelectCG_nonsplit, SelectCG_split);
                Split(SelectCGC_nonsplit, SelectCGC_split);
                Split(SelectCGE_nonsplit, SelectCGE_split);
                Split(SelectCGN_nonsplit, SelectCGN_split);


                SoilWaterCooler_Table();
                SoilWaterCooler_List();

                for (int i = 0; i < SelectCG_split.Count; i++)
                {
                    foreach (DataGridViewRow row in SoilWaterCooler_dataGridView.Rows)
                    {
                        row.Cells[4].Value = SelectCGC_split[i];
                        row.Cells[5].Value = SelectCGE_split[i];
                    }
                }
            }
            else
            {
                MessageBox.Show("냉방설비가 로드되지않았습니다.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            StorageList_comboBox.Text = Stotype;
            StorageType_comboBox.Text = StoSource;

            //그림작성
            Distribute_Image();
            CoolingGeneratorImageSelect(CG, Install_f);
            //Load_CSource(CSource, Install_f);  // ?  
            LoadPressImage(Comp_f, Install_f);
            LoadEvaImage(EvaType_f, Install_f);
            Load_StoType(Stotype, Install_f);
            Load_StoSource(StoSource);
        }

        private void SoilWaterCooler_Save()
        {
            SelectCG_nonsplit = null;
            SelectCGC_nonsplit = null;
            SelectCGE_nonsplit = null;
            SelectCGN_nonsplit = null;

            for (int k = 0; k < SoilWaterCooler_dataGridView.Rows.Count; k++)
            {
                for (int i = 4; i < 6; i++)
                {
                    if (SoilWaterCooler_dataGridView.Rows[k].Cells[i].Value.ToString() == null)
                    {
                        MessageBox.Show("선택항목을 완료해주세요.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                if (k == SoilWaterCooler_dataGridView.Rows.Count - 1)
                {
                    SelectCG_nonsplit += SoilWaterCooler_dataGridView.Rows[k].Cells[1].Value.ToString();
                    SelectCGC_nonsplit += SoilWaterCooler_dataGridView.Rows[k].Cells[4].Value.ToString();
                    SelectCGE_nonsplit += SoilWaterCooler_dataGridView.Rows[k].Cells[5].Value.ToString();
                    SelectCGN_nonsplit += (Program.UTIL.dataGridView_doubleComa(SoilWaterCooler_dataGridView, 0, 6, 0)).ToString();
                    Program.UTIL.dataGridView_doubleComa(SoilWaterCooler_dataGridView, 0, 6, 0);
                }
                else
                {
                    SelectCG_nonsplit += SoilWaterCooler_dataGridView.Rows[k].Cells[1].Value.ToString() + " + ";
                    SelectCGC_nonsplit += SoilWaterCooler_dataGridView.Rows[k].Cells[4].Value.ToString() + " + ";
                    SelectCGE_nonsplit += SoilWaterCooler_dataGridView.Rows[k].Cells[5].Value.ToString() + " + ";
                    SelectCGN_nonsplit += (Program.UTIL.dataGridView_doubleComa(SoilWaterCooler_dataGridView, 0, 6, 0)).ToString() + " + ";
                }
            }
        }


        #endregion

        #region //F. 흡수식냉동기 작성
        private void AbsorbCooler()
        {
            Cooling_AbsorbCooler Absorb_Load = new Cooling_AbsorbCooler(Num, SelectCG_nonsplit, SelectCGN_nonsplit);
            DialogResult result = Absorb_Load.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (Absorb_Load.SelectCG != null)
                {
                    SelectCG_nonsplit = Absorb_Load.SelectCG;
                    SelectCGN_nonsplit = Absorb_Load.SelectCGN;

                    Split(Absorb_Load.SelectCG, SelectCG_split);
                    Split(Absorb_Load.SelectCGN, SelectCGN_split);

                    AbsorbCooler_Table();
                    AbsorbCooler_List();
                }
                else
                {
                    MessageBox.Show("냉방설비가 선택되지않았습니다.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            //그림작성
            Distribute_Image();
            CoolingGeneratorImageSelect(CG, Install_f);
            //LoadPressImage(Comp_f, Install_f);
            //LoadEvaImage(EvaType_f, Install_f);
            //냉수공급온도, 냉수입구온도
        }

        private void AbsorbCooler_Table()
        {

            AbsorbCooler_dataGridView.Visible = true;
            AbsorbCooler_dataGridView.Rows.Clear();
            AbsorbCooler_dataGridView.Columns.Clear();

            new StackedHeaderDecorator(AbsorbCooler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            AbsorbCooler_dataGridView.Columns.Add(checkBoxColumn);

            AbsorbCooler_dataGridView.Columns.Add("A1", "번호");
            AbsorbCooler_dataGridView.Columns.Add("A2", "명칭");
            AbsorbCooler_dataGridView.Columns.Add("A3", "DB유형");


            DataGridViewComboBoxColumn EconomcomboBox = new DataGridViewComboBoxColumn();
            EconomcomboBox.HeaderText = "외기냉방";
            EconomcomboBox.Name = "Economizer";
            EconomcomboBox.Items.AddRange(new string[] { "있음", "없음" });
            AbsorbCooler_dataGridView.Columns.Add(EconomcomboBox);
            AbsorbCooler_dataGridView.Columns.Add("A5", "설치대수");

            AbsorbCooler_dataGridView.Columns.Add("A6", "냉방.출력.[kW]");
            AbsorbCooler_dataGridView.Columns.Add("A7", "냉방.통합성능.[W/W]");
            AbsorbCooler_dataGridView.Columns.Add("A8", "냉방.정격성능.[W/W]");
            AbsorbCooler_dataGridView.Columns.Add("A9", "대기전력");
            AbsorbCooler_dataGridView.Columns.Add("A10", "연료");
            AbsorbCooler_dataGridView.Columns.Add("A11", "냉수온도.출구.[℃]");
            AbsorbCooler_dataGridView.Columns.Add("A12", "냉수온도.입구.[℃]");
            AbsorbCooler_dataGridView.Columns.Add("A13", "설치");

            AbsorbCooler_dataGridView.Columns[0].Width = 40;
            AbsorbCooler_dataGridView.Columns[1].Width = 50;

        }

        private void AbsorbCooler_List()
        {
            List<string> check = new List<string>(); //설치
            List<double> cwin = new List<double>(), cwout = new List<double>();
            check.Clear();
            Power.Clear();
            EER.Clear();
            SEER.Clear();


            int A = 0, B = 0, C = 0;
            double EER_f = 0, Power_f = 0, Number_f = 0, Cwin_f = 0, Cwout_f = 0, SEER_f = 0;
            for (int i = 0; i < SelectCG_split.Count; i++)
            {
                string[][] Val = Program.DB.getValue(DB.type.ProjDB, "User_ABS", " 번호,명칭,DB유형,냉방용량,통합성능,냉방성능,대기전력,연료,냉수출구온도,냉수입구온도,설치",
                                "번호='" + SelectCG_split[i] + "'");
                AbsorbCooler_dataGridView.Rows.Add();
                AbsorbCooler_dataGridView.Rows[i].Cells[1].Value = Val[0][0];
                AbsorbCooler_dataGridView.Rows[i].Cells[2].Value = Val[0][1];
                AbsorbCooler_dataGridView.Rows[i].Cells[3].Value = Val[0][2];
                AbsorbCooler_dataGridView.Rows[i].Cells[5].Value = SelectCGN_split[i];
                AbsorbCooler_dataGridView.Rows[i].Cells[6].Value = Val[0][3]; //냉방출력
                AbsorbCooler_dataGridView.Rows[i].Cells[7].Value = Val[0][4]; //통합성능
                AbsorbCooler_dataGridView.Rows[i].Cells[8].Value = Val[0][5]; //EER
                AbsorbCooler_dataGridView.Rows[i].Cells[9].Value = Val[0][6]; //대기전력
                AbsorbCooler_dataGridView.Rows[i].Cells[10].Value = Val[0][7]; //연료
                AbsorbCooler_dataGridView.Rows[i].Cells[11].Value = Val[0][8]; //냉수출구온도
                AbsorbCooler_dataGridView.Rows[i].Cells[12].Value = Val[0][9]; //냉수입구온도
                AbsorbCooler_dataGridView.Rows[i].Cells[13].Value = Val[0][10]; //설치
                Program.UTIL.dataGridView_doubleComa(AbsorbCooler_dataGridView, i, 5, 0);
                Program.UTIL.dataGridView_doubleComa(AbsorbCooler_dataGridView, i, 6, 1);
                Program.UTIL.dataGridView_doubleComa(AbsorbCooler_dataGridView, i, 7, 1);
                Program.UTIL.dataGridView_doubleComa(AbsorbCooler_dataGridView, i, 8, 1);
                Program.UTIL.dataGridView_doubleComa(AbsorbCooler_dataGridView, i, 9, 1);

                check.Add(Val[0][10].ToString());
                Power.Add(Program.UTIL.dataGridView_doubleComa(AbsorbCooler_dataGridView, i, 6, 1));
                SEER.Add(Program.UTIL.dataGridView_doubleComa(AbsorbCooler_dataGridView, i, 7, 1));
                EER.Add(Program.UTIL.dataGridView_doubleComa(AbsorbCooler_dataGridView, i, 8, 1));
                cwout.Add(Program.UTIL.dataGridView_doubleComa(AbsorbCooler_dataGridView, i, 11, 1)); //수정
                cwin.Add(Program.UTIL.dataGridView_doubleComa(AbsorbCooler_dataGridView, i, 12, 1)); //수정

                //압축기를 작성해야함
            }
            for (int h = 0; h < check.Count; h++)
            {
                if (check[h] == "기존")
                {
                    A = 1 + A;
                }
                else if (check[h] == "신규")
                {
                    B = 1 + B;
                }
                else C = 1 + C;
            }
            Install_f = installmake(A, B, C);

            for (int h = 0; h < Power.Count; h++)
            {
                EER_f += Power[h] * EER[h] * Convert.ToDouble(SelectCGN_split[h]);
                SEER_f += Power[h] * SEER[h] * Convert.ToDouble(SelectCGN_split[h]);
                Power_f += Power[h] * Convert.ToDouble(SelectCGN_split[h]);
                Cwin_f += cwin[h] * Convert.ToDouble(SelectCGN_split[h]);
                Cwout_f += cwout[h] * Convert.ToDouble(SelectCGN_split[h]);
                Number_f += Convert.ToDouble(SelectCGN_split[h]);
            }
            EER_f = EER_f / Power_f;
            SEER_f = SEER_f / Power_f;
            Cwin_f = Cwin_f / Number_f;
            Cwout_f = Cwout_f / Number_f;

            PowerTotal = Power_f;
            EERTotal = EER_f;
            PowerTotal_textBox.Text = Power_f.ToString();
            Program.UTIL.textBox_doubleComa(PowerTotal_textBox, true, 1);
            EERTotal_textBox.Text = EER_f.ToString();
            Program.UTIL.textBox_doubleComa(EERTotal_textBox, true, 1);
            InstallTotal_textBox.Text = Number_f.ToString();
            Program.UTIL.textBox_doubleComa(InstallTotal_textBox, true, 1);
            //냉수입구온도, 출구온도 글씨 작성
        }

        private void AbsorbCooler_ReList()
        {
            if (SelectCG_nonsplit != null)
            {
                Split(SelectCG_nonsplit, SelectCG_split);
                Split(SelectCGE_nonsplit, SelectCGE_split);
                Split(SelectCGN_nonsplit, SelectCGN_split);
                Split(SelectCT_nonsplit, SelectCT_split);
                Split(SelectCTN_nonsplit, SelectCTN_split);

                AbsorbCooler_Table();
                AbsorbCooler_List();

                for (int i = 0; i < SelectCG_split.Count; i++)
                {
                    foreach (DataGridViewRow row in AbsorbCooler_dataGridView.Rows)
                    {
                        row.Cells[4].Value = SelectCGE_split[i];
                    }
                }
                CoolingTop_Table();
                CoolingTop_List();
            }
            else
            {
                MessageBox.Show("냉방설비가 로드되지않았습니다.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            StorageList_comboBox.Text = Stotype;
            StorageType_comboBox.Text = StoSource;

            //그림작성
            Distribute_Image();
            CoolingGeneratorImageSelect(CG, Install_f);
            Load_CSource(CSource, Install_f); //CSource는  
            Load_StoType(Stotype, Install_f);
            Load_StoSource(StoSource);
        }

        private void AbsorbCooler_Save()
        {
            SelectCG_nonsplit = null;
            SelectCGE_nonsplit = null;
            SelectCGN_nonsplit = null;

            for (int k = 0; k < AbsorbCooler_dataGridView.Rows.Count; k++)
            {
                if (AbsorbCooler_dataGridView.Rows[k].Cells[4].Value.ToString() == null)
                {
                    MessageBox.Show("선택항목을 완료해주세요.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (k == AbsorbCooler_dataGridView.Rows.Count - 1)
                {
                    SelectCG_nonsplit += AbsorbCooler_dataGridView.Rows[k].Cells[1].Value.ToString();
                    SelectCGE_nonsplit += AbsorbCooler_dataGridView.Rows[k].Cells[4].Value.ToString();
                    SelectCGN_nonsplit += (Program.UTIL.dataGridView_doubleComa(AbsorbCooler_dataGridView, k, 5, 0)).ToString();

                }
                else
                {
                    SelectCG_nonsplit += AbsorbCooler_dataGridView.Rows[k].Cells[1].Value.ToString() + " + ";
                    SelectCGE_nonsplit += AbsorbCooler_dataGridView.Rows[k].Cells[4].Value.ToString() + " + ";
                    SelectCGN_nonsplit +=(Program.UTIL.dataGridView_doubleComa(AbsorbCooler_dataGridView, 0, 5, 0)).ToString() + " + ";
                }
            }
        }

        #endregion


        //그리드 디자인
        private Boolean datagridviewDesign(DataGridViewCell cell, int column, int row)
        {
            if (row % 2 == 1)
            {
                cell.Style.BackColor = SystemColors.InactiveBorder;
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = SystemColors.InactiveBorder;
                cell.Style.SelectionForeColor = Color.Black;
                return true;
            }
            else
            {
                cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                cell.Style.SelectionForeColor = Color.Black;
                return true;
            }
        }

        // ////////////////////////////////////////////////////////////////////////분배설비//////////////////////////////////////////////////

        #region H.펌프
        private void PumpUse_comboBox_SelectedIndexChanged(object sender, EventArgs e) //펌프유무
        {
            if (PumpUse_comboBox.SelectedItem != null)
            {
                PumpUse = PumpUse_comboBox.SelectedItem.ToString();

                if (PumpUse == "펌프 있음")
                {
                    pump_image();
                    PumpMethod_label.Visible = true;
                    PumpMethod_comboBox.Visible = true;
                    Pump_dataGridView.Visible = true;

                    if (CG == "수냉식냉동기" || CG == "흡수식냉동기")
                    {
                        CPumpMethod_label.Visible = true;
                        CPumpMethod_label.Text = "냉각탑 순환펌프 방식";
                        CPumpMethod_comboBox.Visible = true;
                    }
                    else if (CG == "지열히트펌프")
                    {
                        CPumpMethod_label.Visible = true;
                        CPumpMethod_label.Text = "지열 순환펌프 방식";
                        CPumpMethod_comboBox.Visible = true;
                    }
                    else if (CG == "지하수히트펌프")
                    {
                        CPumpMethod_label.Visible = true;
                        CPumpMethod_label.Text = "지하수 순환펌프 방식";
                        CPumpMethod_comboBox.Visible = true;
                    }
                    else
                    {
                        CPumpMethod_label.Visible = false;
                        CPumpMethod_comboBox.Visible = false;
                    }
                    Create_Pump_Table();
                }
                else
                {
                    Pump_pictureBox.Visible = false;

                    PumpMethod_label.Visible = false;
                    PumpMethod_comboBox.Visible = false;
                    PumpMethod_comboBox.SelectedItem = null;

                    CPumpMethod_label.Visible = false;
                    CPumpMethod_comboBox.Visible = false;
                    CPumpMethod_comboBox.SelectedItem = null;
                    Pump_dataGridView.Visible = false;
                }
            }
            else
            {
                PumpUse = null;
            }

        }
        private void Create_Pump_Table()
        {
            Pump_dataGridView.Columns.Clear();
            new StackedHeaderDecorator(Pump_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            DataGridViewCheckBoxColumn PumpcheckBox = new DataGridViewCheckBoxColumn();

            PumpcheckBox.HeaderText = "선택";
            PumpcheckBox.Name = "check";
            Pump_dataGridView.Columns.Add(PumpcheckBox);

            Pump_dataGridView.Columns.Add("A1", "구분"); //구분에서 해당 값에 대한 지정, (냉수1차,냉수2차,냉각수1차,냉각수2차), save ,텍스트는 바뀌도록
            Pump_dataGridView.Columns.Add("A2", "펌프번호"); //save
            Pump_dataGridView.Columns.Add("A3", "명칭");
            Pump_dataGridView.Columns.Add("A4", "종류");
            Pump_dataGridView.Columns.Add("A5", "A효율.[%]");
            Pump_dataGridView.Columns.Add("A6", "B효율.[%]");
            Pump_dataGridView.Columns.Add("A7", "유량.[CMH]");
            Pump_dataGridView.Columns.Add("A8", "동력.[W]");
            Pump_dataGridView.Columns.Add("A9", "양정.[m]");
            Pump_dataGridView.Columns.Add("A10", "정유량 밸브");
            Pump_dataGridView.Columns.Add("A11", "펌프 제어");
            Pump_dataGridView.Columns.Add("A12", "설치대수.[EA]"); //save

            Pump_dataGridView.Columns[0].Width = 50;
            Pump_dataGridView.Columns[1].Width = 100;
            Pump_dataGridView.Columns[2].Width = 60;
            Pump_dataGridView.Columns[3].Width = 60;
            Pump_dataGridView.Columns[4].Width = 130;
        }

        //1차 또는 1차+2차펌프
        private void PumpMethod_comboBox_SelectedIndexChanged(object sender, EventArgs e) //11차펌프, 2차펌프에 대한 시각적 ui결정
        {
            if (PumpMethod_comboBox.SelectedItem != null)
            {
                PumpMethod = PumpMethod_comboBox.SelectedItem.ToString();
                ChangeVisble_Pump(PumpMethod);
            }
            else
            {
                PumpMethod = null;
                ChangeVisble_Pump("");
            }
        }

        // 냉수 펌프
        private void ChangeVisble_Pump(String PumpMethod) //리스트 보기
        {
            if (PumpMethod == "1차펌프")
            {
                Pump1_label.Visible = true;
                Pump1_textBox.Visible = true;
                Pump1_button.Visible = true;

                Pump2_label.Visible = false;
                Pump2_textBox.Visible = false;
                Pump2_button.Visible = false;

                Pump2_textBox.Text = null;
            }
            else if (PumpMethod == "1차폐회로+2차펌프")
            {
                Pump1_label.Visible = true;
                Pump1_textBox.Visible = true;
                Pump1_button.Visible = true;

                Pump2_label.Visible = true;
                Pump2_textBox.Visible = true;
                Pump2_button.Visible = true;

            }
            else
            {
                Pump1_label.Visible = false;
                Pump1_textBox.Visible = false;
                Pump1_button.Visible = false;

                Pump2_label.Visible = false;
                Pump2_textBox.Visible = false;
                Pump2_button.Visible = false;

                Pump2_textBox.Text = null;
                Pump1_textBox.Text = null;
                Pump_dataGridView.Rows.Clear();
            }
        }
        private void Pump1_button_Click(object sender, EventArgs e)
        {
            pump_image();

            Pump1[1] = nameof(PumpType.냉수1차); //타입
            Cooling_Pump Pump = new Cooling_Pump(Pump1[0], Pump1[1], Pump1[2]);

            DialogResult result = Pump.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (Pump.SelectP != null)
                {
                    Pump1[0] = Pump.SelectP; //번호
                    Pump1[2] = Pump.SelectPN; //대수

                    Pump1_textBox.Text = Pump1[0];
                    Load_Pump_Table(Pump1[0], Pump1[1], Pump1[2]); //번호,타입,설치대수
                }
            }
        }

        private void Pump2_button_Click(object sender, EventArgs e)
        {
            Pump2[1] = nameof(PumpType.냉수2차);
            Cooling_Pump Pump = new Cooling_Pump(Pump2[1], Pump2[0], Pump2[4]);

            DialogResult result = Pump.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (Pump.SelectP != null)
                {
                    Pump2[0] = Pump.SelectP;
                    Pump2[2] = Pump.SelectPN;

                    Pump2_textBox.Text = Pump2[0];
                    Load_Pump_Table(Pump2[0], Pump2[1], Pump2[2]); //번호,타입,설치대수
                }
            }

        }

        // 냉각수 펌프
        private void ChangeVisible_CPump(string Method)
        {
            if (Method == "1차펌프")
            {
                CPump1_label.Visible = true;
                CPump1_textBox.Visible = true;
                CPump1_button.Visible = true;

                CPump2_label.Visible = false;
                CPump2_textBox.Visible = false;
                CPump2_button.Visible = false;
                Pump2_textBox.Text = null;
            }

            else if (Method == "1차폐회로+2차펌프")
            {
                CPump1_label.Visible = true;
                CPump1_textBox.Visible = true;
                CPump1_button.Visible = true;
                CPump2_label.Visible = true;
                CPump2_textBox.Visible = true;
                CPump2_button.Visible = true;
            }
            else
            {
                CPump1_label.Visible = false;
                CPump1_textBox.Visible = false;
                CPump1_button.Visible = false;
                CPump2_label.Visible = false;
                CPump2_textBox.Visible = false;
                CPump2_button.Visible = false;
            }
        }

        private void CPump1_button_Click(object sender, EventArgs e)
        {
            CPump1[1] = nameof(PumpType.냉각수1차);
            Cooling_Pump Pump = new Cooling_Pump(CPump1[0], CPump1[1], CPump1[2]);

            DialogResult result = Pump.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (Pump.SelectP != null)
                {
                    CPump1[0] = Pump.SelectP;
                    CPump1[2] = Pump.SelectPN;

                    CPump1_textBox.Text = CPump1[0];
                    Load_Pump_Table(CPump1[0], CPump1[1], CPump1[2]); //명칭,타입,설치대수
                }
            }
        }

        private void CPump2_button_Click(object sender, EventArgs e)
        {
            CPump2[1] = nameof(PumpType.냉각수2차);
            Cooling_Pump Pump = new Cooling_Pump(CPump2[0], CPump2[1], CPump2[2]);

            DialogResult result = Pump.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (Pump.SelectP != null)
                {
                    CPump2[0] = Pump.SelectP;
                    CPump2[2] = Pump.SelectPN;

                    CPump2_textBox.Text = CPump2[0];
                    Load_Pump_Table(CPump2[0], CPump2[1], CPump2[2]); //명칭,타입,설치대수
                }
            }
        }

        private void Load_Pump_Table(string P, string PT, string PN) //번호,타입,설치대수
        {
            for (int i = 0; i < Pump_dataGridView.Rows.Count; i++)
            {
                if (Pump_dataGridView.Rows[i].Cells[1].Value == PT) //구분임
                {
                    Pump_dataGridView.Rows.Remove(Pump_dataGridView.Rows[i]);
                }
            }
            int nRow = Pump_dataGridView.Rows.Add();
            DataGridViewComboBoxCell 정유량밸브comboBox = new DataGridViewComboBoxCell();
            정유량밸브comboBox.Items.Add("있음");
            정유량밸브comboBox.Items.Add("없음");
            Pump_dataGridView.Rows[nRow].Cells[10] = 정유량밸브comboBox;

            DataGridViewComboBoxCell 제어comboBox = new DataGridViewComboBoxCell();
            제어comboBox.Items.Add("대수제어");
            제어comboBox.Items.Add("인버터제어");
            제어comboBox.Items.Add("제어없음");
            Pump_dataGridView.Rows[nRow].Cells[11] = 제어comboBox;

            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "번호,명칭,종류,A효율,B효율,유량,동력,양정", "번호 = '" + P + "'");
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    Pump_dataGridView.Rows[nRow].Cells[1].Value = PT;
                    for (int a = 0; a < Value[0].Length; a++)
                    {
                        Pump_dataGridView.Rows[nRow].Cells[a + 2].Value = Value[0][a];
                    }
                    Program.UTIL.dataGridView_doubleComa(Pump_dataGridView, nRow, 5, 1);
                    Program.UTIL.dataGridView_doubleComa(Pump_dataGridView, nRow, 6, 1);
                    Program.UTIL.dataGridView_doubleComa(Pump_dataGridView, nRow, 7, 0);
                    Program.UTIL.dataGridView_doubleComa(Pump_dataGridView, nRow, 8, 0);
                    Program.UTIL.dataGridView_doubleComa(Pump_dataGridView, nRow, 9, 0);
                    Pump_dataGridView.Rows[nRow].Cells[12].Value = PN;
                }
            }
        }


        private void Save_Pump()
        {
            if (Pump_dataGridView.Rows.Count == 0) { return; }
            Pump1_nonsplit = null;
            Pump2_nonsplit = null;
            CPump1_nonsplit = null;
            CPump2_nonsplit = null;


            for (int k = 0; k < Pump_dataGridView.Rows.Count; k++)
            {
                switch (Pump_dataGridView.Rows[k].Cells[1].Value.ToString())
                {
                    case nameof(PumpType.냉수1차):
                        for (int i = 10; i < 13; i++)
                        {
                            if (Pump_dataGridView.Rows[k].Cells[i].Value == null)
                            {
                                MessageBox.Show("펌프의 선택항목을 완성하세요.");
                                return;
                            }
                        }
                        Pump1[0] = Pump_dataGridView.Rows[k].Cells[2].Value.ToString();
                        Pump1[1] = Pump_dataGridView.Rows[k].Cells[1].Value.ToString();
                        Pump1[2] = Pump_dataGridView.Rows[k].Cells[12].Value.ToString();
                        Pump1[3] = Pump_dataGridView.Rows[k].Cells[10].Value.ToString();
                        Pump1[4] = Pump_dataGridView.Rows[k].Cells[11].Value.ToString();

                        for (int j = 0; j < 5; j++)
                        {
                            if (j == 4)
                            {
                                Pump1_nonsplit += Pump1[j];
                            }
                            else
                            {
                                Pump1_nonsplit += Pump1[j] + "+";
                            }

                        }

                        break;

                    case nameof(PumpType.냉수2차):
                        for (int i = 9; i < 12; i++)
                        {
                            if (Pump_dataGridView.Rows[k].Cells[i].Value == null)
                            {
                                MessageBox.Show("펌프의 선택항목을 완성하세요.");
                                return;
                            }
                        }
                        Pump2[0] = Pump_dataGridView.Rows[k].Cells[2].Value.ToString();
                        Pump2[1] = Pump_dataGridView.Rows[k].Cells[1].Value.ToString();
                        Pump2[2] = Pump_dataGridView.Rows[k].Cells[12].Value.ToString();
                        Pump2[3] = Pump_dataGridView.Rows[k].Cells[10].Value.ToString();
                        Pump2[4] = Pump_dataGridView.Rows[k].Cells[11].Value.ToString();

                        for (int j = 0; j < 5; j++)
                        {
                            if (j == 4)
                            {
                                Pump2_nonsplit += Pump2[j];
                            }
                            else
                            {
                                Pump2_nonsplit += Pump2[j] + "+";
                            }

                        }
                        break;
                    case nameof(PumpType.냉각수1차):
                        for (int i = 10; i < 13; i++)
                        {
                            if (Pump_dataGridView.Rows[k].Cells[i].Value == null)
                            {
                                MessageBox.Show("펌프의 선택항목을 완성하세요.");
                                return;
                            }
                        }
                        CPump1[0] = Pump_dataGridView.Rows[k].Cells[2].Value.ToString();
                        CPump1[1] = Pump_dataGridView.Rows[k].Cells[1].Value.ToString();
                        CPump1[2] = Pump_dataGridView.Rows[k].Cells[12].Value.ToString();
                        CPump1[3] = Pump_dataGridView.Rows[k].Cells[10].Value.ToString();
                        CPump1[4] = Pump_dataGridView.Rows[k].Cells[11].Value.ToString();

                        for (int j = 0; j < 5; j++)
                        {
                            if (j == 4)
                            {
                                CPump1_nonsplit += CPump1[j];
                            }
                            else
                            {
                                CPump1_nonsplit += CPump1[j] + "+";
                            }

                        }
                        break;

                    case nameof(PumpType.냉각수2차):
                        for (int i = 10; i < 13; i++)
                        {
                            if (Pump_dataGridView.Rows[k].Cells[i].Value == null)
                            {
                                MessageBox.Show("펌프의 선택항목을 완성하세요.");
                                return;
                            }
                        }
                        CPump2[0] = Pump_dataGridView.Rows[k].Cells[2].Value.ToString();
                        CPump2[1] = Pump_dataGridView.Rows[k].Cells[1].Value.ToString();
                        CPump2[2] = Pump_dataGridView.Rows[k].Cells[12].Value.ToString();
                        CPump2[3] = Pump_dataGridView.Rows[k].Cells[10].Value.ToString();
                        CPump2[4] = Pump_dataGridView.Rows[k].Cells[11].Value.ToString();

                        for (int j = 0; j < 5; j++)
                        {
                            if (j == 4)
                            {
                                CPump2_nonsplit += CPump2[j];
                            }
                            else
                            {
                                CPump2_nonsplit += CPump2[j] + "+";
                            }

                        }

                        break;
                    default:
                        break;

                }
            }
        }

        //열원펌프 콤보박스 체크하기
        private void PumpMComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CPumpMethod_comboBox.SelectedItem != null)
            {
                CPumpMethod = CPumpMethod_comboBox.SelectedItem.ToString();
                ChangeVisible_CPump(CPumpMethod);
            }
            else
            {
                PumpMethod = null;
                ChangeVisible_CPump("");
            }
        }



        #endregion

        #region I.공급설비

        private void ce1Type_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int A = 1;
            ce1Type = ce1Type_comboBox.Text;
            imagemake(ce1Type, 1);
            if (ce1Type == "공조기")
            {
                ce1AhuText.Visible = true;
                ce1Ahu_comboBox.Visible = true;
                ce1AhuText2.Visible = true;
                ce1Ahu_textBox.Visible = true;
                ce1Ahu_button.Visible = true;

                ce1ZoneText.Visible = false;
                ce1Zone_textBox.Visible = false;
                ce1Zone_textBox.Text = null;
                ce1Zone_button.Visible = false;

            }
            else
            {
                ce1AhuText.Visible = false;
                ce1Ahu_comboBox.Visible = false;
                ce1AhuText2.Visible = false;
                ce1Ahu_textBox.Visible = false;
                ce1Ahu_textBox.Text = null;
                ce1Ahu_button.Visible = false;

                ce1ZoneText.Visible = true;
                ce1Zone_textBox.Visible = true;
                ce1Zone_button.Visible = true;
            }
        }

        private void ce2Type_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int A = 2;
            ce2Type = ce2Type_comboBox.Text;

            if (ce2Type == ce1Type)
            {
                MessageBox.Show("공급설비1과 다른 종류의 공급설비를 선택하세요.");
                return;
            }
            else
            {
                imagemake(ce2Type, 2);
            }
            if (ce2Type == "공조기")
            {
                ce2AhuText.Visible = true;
                ce2Ahu_comboBox.Visible = true;
                ce2AhuText2.Visible = true;
                ce2Ahu_textBox.Visible = true;
                ce2Ahu_button.Visible = true;

                ce2ZoneText.Visible = false;
                ce2Zone_textBox.Visible = false;
                ce2Zone_textBox.Text = null;
                ce2Zone_button.Visible = false;
            }
            else
            {
                ce2AhuText.Visible = false;
                ce2Ahu_comboBox.Visible = false;
                ce2AhuText2.Visible = false;
                ce2Ahu_textBox.Visible = false;
                ce2Ahu_textBox.Text = null;
                ce2Ahu_button.Visible = false;

                ce2ZoneText.Visible = true;
                ce2Zone_textBox.Visible = true;
                ce2Zone_button.Visible = true;
            }

        }

        private void Create_ce_Table()
        {
            ce_dataGridView.Columns.Clear();
            DataGridViewCheckBoxColumn ce_checkBoxColumn = new DataGridViewCheckBoxColumn();
            new StackedHeaderDecorator(ce_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, ce_datagridviewDesign);
            ce_checkBoxColumn.HeaderText = "선택";
            ce_checkBoxColumn.Name = "check";
            ce_dataGridView.Columns.Add(ce_checkBoxColumn);
            ce_dataGridView.Columns.Add("A1", "번호");
            ce_dataGridView.Columns.Add("A2", "종류");
            // ce_dataGridView.Columns.Add("A3", "일람표 번호");
            ce_dataGridView.Columns.Add("A3", "일람표 명칭");
            ce_dataGridView.Columns.Add("A4", "용량.[kW]");
            ce_dataGridView.Columns.Add("A5", "소비전력.[kW]");
            // ce_dataGridView.Columns.Add("A7", "적용 존.존번호");

            ce_dataGridView.Columns.Add("A6", "존/공조기 명칭");
            ce_dataGridView.Columns.Add("A7", "일일평균가동시간[hours/day].최대가동시간");
            ce_dataGridView.Columns.Add("A8", "일일평균가동시간[hours/day].실제가동시간");


            ce_dataGridView.Columns[0].Width = 30;
            ce_dataGridView.Columns[1].Width = 150;
            ce_dataGridView.Columns[2].Width = 80;
            ce_dataGridView.Columns[3].Width = 80;
            ce_dataGridView.Columns[6].Width = 120;

        }
        private void ce1Zone_button_Click(object sender, EventArgs e)
        {
            if (ce_dataGridView.Columns.Count == 0)
            {
                Create_ce_Table();
            }
            Cooling_ceZone ceZone = new Cooling_ceZone(Num, SelectZone_nonsplit, ce1Type);
            DialogResult result = ceZone.ShowDialog();
            if (result == DialogResult.OK)
            {
                Load_ce();
                Load_ce1Zone(ce1Type, "Zone");
            }
        }

        private void ce2Zone_button_Click(object sender, EventArgs e)
        {
            Cooling_ceZone ceZone = new Cooling_ceZone(Num, SelectZone_nonsplit, ce2Type);
            DialogResult result = ceZone.ShowDialog();
            if (result == DialogResult.OK)
            {
                ce_dataGridView.Rows.Clear();
                Load_ce();
                Load_ce2Zone(ce2Type, "Zone");
            }
        }

        private void Load_ce1Zone(string ce1, string _TYPE)
        {
            if (_TYPE == "Zone")
            {
                string[][] Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Cooling_ce_Form", "존번호", "냉방시스템 = '" + Num + "' And 공급설비종류 = '" + ce1 + "'");
                if (Value.Length > 0)
                {
                    if (Value.Length == 1)
                    {
                        ce1Zone_textBox.Text = Value[0][0];
                    }
                    else
                    {
                        ce1Zone_textBox.Text = Value[0][0] + "외 " + (Value.Length - 1) + "개 존";
                    }
                }
            }
            else if (_TYPE == "Ahu")
            {
                string[][] Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Cooling_ce_Form", "존번호", "냉방시스템 = '" + Num + "' And 공급설비종류 = '" + ce1 + "'");
                if (Value.Length > 0)
                {
                    if (Value.Length == 1)
                    {
                        ce1Ahu_textBox.Text = Value[0][0];
                    }
                    else
                    {
                        ce1Ahu_textBox.Text = Value[0][0] + "외 " + (Value.Length - 1) + "개 존";
                    }
                }
            }
            else return;
        }

        private void Load_ce2Zone(string ce2, string _TYPE)
        {
            string[][] Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Cooling_ce_Form", "존번호", "냉방시스템 = '" + Num + "' And 공급설비종류 = '" + ce2 + "'");
            if (_TYPE == "Zone")
            {
                if (Value.Length > 0)
                {
                    if (Value.Length == 1)
                    {
                        ce2Zone_textBox.Text = Value[0][0];
                    }
                    else
                    {
                        ce2Zone_textBox.Text = Value[0][0] + "외 " + (Value.Length - 1) + "개 존";
                    }
                }
            }
            else if (_TYPE == "Ahu")
            {
                if (Value.Length > 0)
                {
                    if (Value.Length == 1)
                    {
                        ce2Ahu_textBox.Text = Value[0][0];
                    }
                    else
                    {
                        ce2Ahu_textBox.Text = Value[0][0] + "외 " + (Value.Length - 1) + "개 존";
                    }
                }
            }
            else return;
        }

        private void ce_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ce_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                ce_SelectRow = e.RowIndex;
            }

        }
        private void ce_Remove_button_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show(ce_dataGridView.Rows[ce_SelectRow].Cells[1].Value.ToString() + "을 삭제 하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                String substring = ce_dataGridView.Rows[ce_SelectRow].Cells[1].Value.ToString().Substring(ce_dataGridView.Rows[ce_SelectRow].Cells[1].Value.ToString().Length - 6, 6); //공급설비번호
                String substring2 = ce_dataGridView.Rows[ce_SelectRow].Cells[1].Value.ToString().Substring(0, 10); //존번호
                Program.DB.deleteValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호 ='" + substring2 + "' AND 공급설비 = '" + substring + "' AND 냉방시스템 = '" + Num + "'");
                ce_dataGridView.Rows.Remove(ce_dataGridView.Rows[ce_SelectRow]);
            }

        }

        private void Load_ce() //공급설비종류로 지정해야함
        {
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호,공급설비종류,공급설비,가동시간", "냉방시스템 = '" + Num + "'");//공조기번호,공조기공급설비종류,공급설비,가동시간
            if (Value.Length > 0)
            {
                int Sum = 1;
                ce_dataGridView.Rows.Clear();

                for (int i = 0; i < Value.Length; i++)
                {
                    if (Value[i][1] == "VAV유닛" || Value[i][1] == " CAV유닛" || Value[i][1] == "파워팬유닛")
                    {
                        int nRow = ce_dataGridView.Rows.Add();
                        ce_dataGridView.Rows[nRow].Cells[1].Value = Value[i][0] + "_" + Value[i][2]; //존번호 + 공급설비번호(개수포함)
                        ce_dataGridView.Rows[nRow].Cells[2].Value = Value[i][1];//공급설비종류 (CAV,VAV,FPU)
                        int index = Value[i][2].IndexOf("_");
                        string substring = Value[i][2].Substring(0, index);
                        string[][] 일람표정보 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "번호,명칭,용량_냉방,소비전력_냉방", "번호 = '" + substring + "'");
                        ce_dataGridView.Rows[nRow].Cells[3].Value = 일람표정보[0][1]; //일람표명칭

                        string[][] 최대부하 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Zone_HCneed_Result", "Q_max", "번호 = '" + Value[i][0] + "' And 난방_냉방 = '냉방' And 비이용일_이용일 = '이용일'");

                        double 용량 = Convert.ToDouble(최대부하[0][0]) / 1000;
                        ce_dataGridView.Rows[nRow].Cells[4].Value = string.Format("{0:F1}", 용량); //해당존의 최대부하값을 반영하기
                        ce_dataGridView.Rows[nRow].Cells[5].Value = 일람표정보[0][3];//소비전력

                        string[][] 존정보 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존이름,냉난방시간", "존번호 = '" + Value[i][0] + "'");
                        ce_dataGridView.Rows[nRow].Cells[6].Value = 존정보[0][0]; //존명칭
                        ce_dataGridView.Rows[nRow].Cells[7].Value = 존정보[0][1]; //최대냉난방가동시간
                        ce_dataGridView.Rows[nRow].Cells[8].Value = 존정보[0][1]; //실제가동시간
                    }



                    else
                    {
                        int nRow = ce_dataGridView.Rows.Add();
                        String[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "냉난방시간", "존번호='" + Value[i][0] + "'");
                        ce_dataGridView.Rows[nRow].Cells[7].Value = Value2[0][0];//최대냉난방가동시간
                        DataGridViewComboBoxCell 가동시간comboBox = new DataGridViewComboBoxCell();
                        if (Value2.Length > 0)
                        {
                            for (int h = 0; h < Convert.ToInt16(Value2[0][0]) + 1; h++)
                            {
                                가동시간comboBox.Items.Add(h.ToString());
                            }
                            ce_dataGridView.Rows[nRow].Cells[8] = 가동시간comboBox;
                            if (Value[i][3] == null || Value[i][3] == "")
                            { ce_dataGridView.Rows[nRow].Cells[8].Value = Value2[0][0]; }
                            else
                            {
                                ce_dataGridView.Rows[nRow].Cells[8].Value = Value[i][3];
                            }
                        }
                        ce_dataGridView.Rows[nRow].Cells[2].Value = Value[i][1];//종류
                        int index = Value[i][2].IndexOf("_");
                        String substring = Value[i][2].Substring(0, index);
                        string[][] 일람표정보 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "번호,명칭,용량_냉방,소비전력_냉방", "번호 = '" + substring + "'");
                        ce_dataGridView.Rows[nRow].Cells[3].Value = 일람표정보[0][1]; //일람표명칭
                        ce_dataGridView.Rows[nRow].Cells[4].Value = 일람표정보[0][2]; //용량
                        ce_dataGridView.Rows[nRow].Cells[5].Value = 일람표정보[0][3];//소비전력
                        string[][] 존정보 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,존이름", "존번호 = '" + Value[i][0] + "'");
                        ce_dataGridView.Rows[nRow].Cells[6].Value = 존정보[0][1];//존이름
                        ce_dataGridView.Rows[nRow].Cells[1].Value = 존정보[0][0] + "_" + Value[i][2];
                    }


                }
            }
        }

        private Boolean ce_datagridviewDesign(DataGridViewCell cell, int column, int row)
        {
            if (row % 2 == 1)
            {
                cell.Style.BackColor = SystemColors.InactiveBorder;
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = SystemColors.InactiveBorder;
                cell.Style.SelectionForeColor = Color.Black;

                return true;
            }
            else
            {
                cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                cell.Style.SelectionForeColor = Color.Black;

                return true;
            }
        }

        private void Save_ce()
        {
            Program.DB.deleteValue(DB.type.ProjDB, "Cooling_ce_Form", "냉방시스템 = '" + Num + "'");
            for (int n = 0; n < ce_dataGridView.Rows.Count; n++)
            {
                string 존번호, 공급설비;
                int index = ce_dataGridView.Rows[n].Cells[1].Value.ToString().IndexOf("CE");
                존번호 = ce_dataGridView.Rows[n].Cells[1].Value.ToString().Substring(0, index - 1);
                공급설비 = ce_dataGridView.Rows[n].Cells[1].Value.ToString().Substring(index, ce_dataGridView.Rows[n].Cells[1].Value.ToString().Length - index);
                Program.DB.setValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호,프로젝트유형,냉방시스템,공급설비종류,공급설비,용량,소비전력,가동시간",
                    "'" + 존번호 + "','" + 프로젝트유형 + "','" + Num + "','" + ce_dataGridView.Rows[n].Cells[2].Value + "','" + 공급설비 + "'," +
                    "'" + ce_dataGridView.Rows[n].Cells[4].Value + "','" + ce_dataGridView.Rows[n].Cells[5].Value + "','" + ce_dataGridView.Rows[n].Cells[8].Value + "'", "");
            }
        }

        #endregion
        private void Save_Image()
        {
            try
            {
                // 캡쳐할 영역의 위치와 크기 설정
                Rectangle captureRectangle = ImagePanel.RectangleToScreen(ImagePanel.ClientRectangle); //RectangleToScree는 화면 상 좌표를 읽음, ClientRectangle는 그림의 크기를 읽음 

                // 비트맵 생성
                Bitmap bmp = new Bitmap(captureRectangle.Width, captureRectangle.Height);

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    // 특정 영역을 캡쳐
                    g.CopyFromScreen(captureRectangle.Location, Point.Empty, captureRectangle.Size);
                }

                string pid = "0000-00-00";
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호");
                if (Value.Length > 0)
                {
                    pid = Value[0][0];
                }
                Directory.CreateDirectory(Program.gPath + "threejs\\public\\print\\img\\" + pid);
                // 저장할 파일 경로 설정
                string ImageName = "/print/img/" + pid + "/" + Num + ".png";
                string imagePath = Program.gPath + ImageName; // 최종 경로


                // 비트맵을 파일로 저장
                bmp.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);
            }
        }
        private void Save_button_Click(object sender, EventArgs e)
        {
            if (Name_f == null || Name_f.Length == 0)
            {
                MessageBox.Show("냉방시스템 명칭을 입력하세요");
            }
            else if (CSource == null || CSource.Length == 0) MessageBox.Show(" 냉방 열원을 선택해 주세요.");
            else
            {
                Save_Image();
                Save();
            }

        }
        private void Save()
        {
            Save_Pump();
            Save_ce();
            if (Save_CG() == true)
            {
                this.DialogResult = DialogResult.OK;
                this.Hide();
                Program.getMenuForm().DoLoadForm(38, OnLoadListProc);
            }
            else
            {
                return;
            }
        }

        public bool Save_CG()
        {
            //냉방설비, 저장설비
            switch (CG)
            {
                case "실외기12kW":
                    AirCon_Save();
                    break;
                case "공냉식냉동기":
                    AirCooler_Save();
                    Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Form", "번호,압축기", "'" + Num + "','" + SelectCGComp_nonsplit + "'", "번호");
                    break;
                case "수냉식냉동기":
                    WaterCooler_Save();
                    CoolingTop_Save();
                    Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Form", "번호, 압축기, 냉각탑, 냉각탑개수", "'" + Num + "','" + SelectCGComp_nonsplit + "','" + SelectCT_nonsplit + "','" + SelectCTN_nonsplit + "'", "번호");
                    break;
                case "지열히트펌프":
                    SoilCooler_Save();
                    break;
                case "지하수히트펌프":
                    SoilWaterCooler_Save();
                    break;

                case "흡수식냉동기":
                    AbsorbCooler_Save();
                    CoolingTop_Save();
                    Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Form", "번호, 냉각탑, 냉각탑개수", "'" + Num + "','" + SelectCT_nonsplit + "','" + SelectCTN_nonsplit + "'", "번호");
                    break;

                default:
                    break;
            }

            Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Form", "번호,프로젝트유형,명칭,공급존,공급AHU,냉방설비,열원설비,냉방유닛,제어유형,외기냉방시스템,설치대수,저장탱크,저장유형,냉방출력,냉방성능",
            "'" + Num + "','" + 프로젝트유형 + "','" + Name_f + "','" + SelectZone_nonsplit + "','" + SelectAHU_nonsplit + "', '" + CG + "','" + CSource +
             "', '" + SelectCG_nonsplit + "', '" + SelectCGC_nonsplit + "', '" + SelectCGE_nonsplit + "','" + SelectCGN_nonsplit + "','" + Stotype + "','" + StoSource + "','" + PowerTotal.ToString() + "','" + EERTotal.ToString() + "'", "번호");
            //분배설비
            Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Form", "번호,펌프유무,냉수펌프방식,냉수펌프1,냉수펌프2,냉각수펌프방식,냉각수펌프1,냉각수펌프2",
               "'" + Num + "','" + PumpUse + "','" + PumpMethod + "','" + Pump1_nonsplit + "','" + Pump2_nonsplit + "','" + CPumpMethod + "','" + CPump1_nonsplit + "','" + CPump2_nonsplit + "'", "번호");

            //공급설비
            Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Form", "번호,공급설비1종류,공급설비2종류,공급설비3종류,공급설비4종류", "'" + Num + "','" + ce1Type + "','" + ce2Type + "','" + ce1Ahu + "','" + ce2Ahu + "'", "번호");

            return true;

        }
        public static bool OnLoadListProc(Form form)
        {
            List_CoolingSystem f = (List_CoolingSystem)form;
            f.load_List();
            return true;
        }

        private void reset()
        {
            //저장항목
            SelectZone_nonsplit = null; SelectAHU_nonsplit = null; SelectCG_nonsplit = null; SelectCGC_nonsplit = null; SelectCGE_nonsplit = null; SelectCGN_nonsplit = null;
            SelectCT_nonsplit = null; SelectCTN_nonsplit = null;


            //만들어지느것
            ZoneNameList.Clear();
            AHUNameList.Clear();
            SelectCG_split.Clear();

            //생산설비
            CG = null; Num = null; Name_f = null;

            SelectCGC_split.Clear();
            SelectCGE_split.Clear();
            SelectCGN_split.Clear();
            SelectCT_split.Clear(); //? 확인
            SelectCTN_split.Clear();



            //이미지작성
            Install_f = null; Control_f = null; CSource = null; Fuel_f = null;
            Econo_f = null; Comp_f = null; Refri = null; EvaType = null;//부하측열원공급설비(Supp_f) 및 설치(기존,신규)가 추가됨
            CT_cwin.Visible = false;
            CT_cwout.Visible = false;
            Press_pictureBox.Visible = false;
            eva.Visible = false;
            ce1_pictureBox.Visible = false;
            ce2_pictureBox.Visible = false;
            ce3_pictureBox.Visible = false;

            //공급설비2 종류 및 공조기 공급부분 unviosible처리

            ce1AhuText.Visible = false;
            ce1Ahu_comboBox.Visible = false;
            ce1Ahu_comboBox.Text = null;
            ce1AhuText2.Visible = false;
            ce1Ahu_textBox.Visible = false;
            ce1Ahu_textBox.Text = null;
            ce1Ahu_button.Visible = false;

            ce2AhuText.Visible = false;
            ce2Ahu_comboBox.Visible = false;
            ce2Ahu_comboBox.Text = null;
            ce2AhuText2.Visible = false;
            ce2Ahu_textBox.Visible = false;
            ce2Ahu_textBox.Text = null;
            ce2Ahu_button.Visible = false;

            ce2ZoneText.Visible = false;
            ce2Zone_textBox.Visible = false;
            ce2Zone_textBox.Text = null;
            ce2Zone_button.Visible = false;

            ce1ZoneText.Text = null;
            ce2Type_comboBox.Text = null;
            ce1Type_comboBox.Text = null;

            //장비관련
            Power.Clear(); EER.Clear();

            //저장설비
            Stotype = null; StoSource = null;

            //쿨링탑관련
            CoolingTop_dataGridView.Visible = false;

            //펌프정의
            PumpUse = null; PumpMethod = null;
            Pump1_nonsplit = null; Pump2_nonsplit = null; CPump1_nonsplit = null; CPump2_nonsplit = null;

            //공급설비정의
            ce1Type = null; ce2Type = null; ce1Ahu = null; ce2Ahu = null;

            //텍스트 정의
            CZ_AnnualCoolingNeed_Textbox.Visible = false;
            CZ_AnnualCoolingNeed_Textbox.Text = null;

            CZ_MaxCoolingLoad_Textbox.Visible = false;
            CZ_MaxCoolingLoad_Textbox.Text = null;

            CZ_FloorArea_Textbox.Visible = false;
            CZ_FloorArea_Textbox.Text = null;

            CA_AnnualCoolingNeed_Textbox.Visible = false;
            CA_AnnualCoolingNeed_Textbox.Text = null;

            CA_MaxCoolingLoad_Textbox.Visible = false;
            CA_MaxCoolingLoad_Textbox.Text = null;

            CA_FloorArea_Textbox.Visible = false;
            CA_FloorArea_Textbox.Text = null;

            ZoneS_label.Visible = false;
            AhuS_label.Visible = false;
        }

        public void LoadData(String ID) // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();
            NumTextBox.Text = ID;
            Num = ID;

            string[][] DataValue = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "프로젝트유형,명칭,공급존,공급AHU,냉방설비,열원설비,냉방유닛, 제어유형,외기냉방시스템,설치대수,저장탱크,저장유형", "번호='" + Num + "'"); //냉방유닛추가됨

            if (DataValue.Length > 0)
            {
                프로젝트유형 = DataValue[0][0];
                Name_f = DataValue[0][1];
                CoolingSystemNameText.Text = Name_f;

                SelectZone_nonsplit = DataValue[0][2];
                Split(SelectZone_nonsplit, ZoneNameList);
                SelectedZoneText.Text = ZoneNameList[0].ToString() + " 외 " + (ZoneNameList.Count - 1).ToString() + "개";
                Zonemainwrite();

                //공조기 부분
                if (DataValue[0][3] != null && DataValue[0][3] != "")
                {
                    SelectAHU_nonsplit = DataValue[0][3];
                    Split(SelectAHU_nonsplit, AHUNameList);
                    SelectedAhuText.Visible = true;
                    SelectedAhuText.Text = AHUNameList[0].ToString() + " 외 " + (AHUNameList.Count - 1).ToString() + "개";
                    Ahumainwrite();
                }
                else
                {
                    SelectedAhuText.Visible = false;
                }


                CG = DataValue[0][4];
                CoolingGeneratorSelect_comboBox.Text = CG;

                CSource = DataValue[0][5];

                Program.UTIL.ffCode = true; //내용 추가
                Install_comboBox.Text = CSource;

                SelectCG_nonsplit = DataValue[0][6];
                SelectCGC_nonsplit = DataValue[0][7];
                SelectCGE_nonsplit = DataValue[0][8];
                SelectCGN_nonsplit = DataValue[0][9];
                Stotype = DataValue[0][10];
                StoSource = DataValue[0][11];

                switch (CG)
                {
                    case "실외기12kW":
                        AirCon_ReList();
                        break;
                    case "공냉식냉동기":
                        string[][] 공냉식 = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "압축기", "번호='" + Num + "'"); //냉방유닛추가됨
                        SelectCGComp_nonsplit = 공냉식[0][0];
                        AirCooler_ReList();
                        break;
                    case "수냉식냉동기":
                        string[][] 수냉식 = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "냉각탑,냉각탑개수", "번호='" + Num + "'"); //냉방유닛추가됨
                        SelectCT_nonsplit = 수냉식[0][0];
                        SelectCTN_nonsplit = 수냉식[0][1];
                        WaterCooler_ReList();
                        break;

                    case "흡수식냉동기":
                        string[][] 흡수식 = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "냉각탑,냉각탑개수", "번호='" + Num + "'"); //냉방유닛추가됨
                        SelectCT_nonsplit = 흡수식[0][0];
                        SelectCTN_nonsplit = 흡수식[0][1];
                        AbsorbCooler_ReList();
                        break;

                    case "지열히트펌프":
                        SoilCooler_ReList();
                        break;

                    case "지하수히트펌프":
                        SoilWaterCooler_ReList();
                        break;

                    default:
                        break;
                }
            }

            //펌프 로드함

            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "펌프유무,냉수펌프방식,냉수펌프1,냉수펌프2,냉각수펌프방식,냉각수펌프1,냉각수펌프2", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {
                PumpUse_comboBox.SelectedItem = Value[0][0];
                PumpMethod = Value[0][1];
                PumpUse = Value[0][0];

                if (PumpUse == "펌프 있음")
                {
                    List<string> pump = new List<string>();

                    PumpMethod_comboBox.SelectedItem = Value[0][1];
                    pump_image();
                    PumpMethod_label.Visible = true;
                    PumpMethod_comboBox.Visible = true;
                    Pump_dataGridView.Visible = true;
                    Create_Pump_Table();

                    if (Value[0][2] != null && Value[0][2] != "") //냉수펌프1
                    {
                        pump.Clear();
                        Pump1_textBox.Text = null;
                        Pump1_nonsplit = Value[0][2].ToString();
                        Split(Pump1_nonsplit, pump);
                        for (int i = 0; i < 5; i++)
                        {
                            Pump1[i] = pump[i];
                        }
                        Load_Pump_Table(Pump1[0], Pump1[1], Pump1[2]); //번호,타입,설치대수

                        for (int k = 0; k < Pump_dataGridView.Rows.Count; k++)
                        {
                            if (Pump_dataGridView.Rows[k].Cells[1].Value == Pump1[1])
                            {
                                Pump_dataGridView.Rows[k].Cells[10].Value = Pump1[3];
                                Pump_dataGridView.Rows[k].Cells[11].Value = Pump1[4];
                            }
                        }
                        Pump1_textBox.Text = Pump1[0];
                    }

                    if (Value[0][3] != null && Value[0][3] != "") //냉수펌프2
                    {
                        pump.Clear();
                        Pump2_textBox.Text = null;
                        Pump2_nonsplit = Value[0][3].ToString();
                        Split(Pump2_nonsplit, pump);
                        for (int i = 0; i < 5; i++)
                        {
                            Pump2[i] = pump[i];
                        }
                        Load_Pump_Table(Pump2[0], Pump2[1], Pump2[2]);

                        for (int k = 0; k < Pump_dataGridView.Rows.Count; k++)
                        {
                            if (Pump_dataGridView.Rows[k].Cells[1].Value == Pump2[1])
                            {
                                Pump_dataGridView.Rows[k].Cells[10].Value = Pump2[3];
                                Pump_dataGridView.Rows[k].Cells[11].Value = Pump2[4];
                            }
                        }
                        Pump2_textBox.Text = Pump2[0];
                    }

                    if (CG == "수냉식냉동기" || CG == "흡수식냉동기")
                    {
                        CPumpMethod_label.Visible = true;
                        CPumpMethod_label.Text = "냉각탑 순환펌프 방식";
                        CPumpMethod_comboBox.Visible = true;
                    }
                    else if (CG == "지열히트펌프")
                    {
                        CPumpMethod_label.Visible = true;
                        CPumpMethod_label.Text = "지열 순환펌프 방식";
                        CPumpMethod_comboBox.Visible = true;
                    }
                    else if (CG == "지하수히트펌프")
                    {
                        CPumpMethod_label.Visible = true;
                        CPumpMethod_label.Text = "지하수 순환펌프 방식";
                        CPumpMethod_comboBox.Visible = true;
                    }
                    else
                    {
                        CPumpMethod_label.Visible = false;
                        CPumpMethod_comboBox.Visible = false;
                    }

                    if (Value[0][4].Length > 0)
                    {
                        CPumpMethod_comboBox.SelectedItem = Value[0][4].ToString();
                    }


                    if (Value[0][5] != null && Value[0][5] != "") //냉각수펌프1
                    {
                        pump.Clear();
                        CPump1_textBox.Text = null;
                        CPump1_nonsplit = Value[0][5];
                        Split(CPump1_nonsplit, pump);
                        for (int i = 0; i < 5; i++)
                        {
                            CPump1[i] = pump[i];
                        }
                        Load_Pump_Table(CPump1[0], CPump1[1], CPump1[2]);
                        for (int k = 0; k < Pump_dataGridView.Rows.Count; k++)
                        {
                            if (Pump_dataGridView.Rows[k].Cells[1].Value == CPump1[1])
                            {
                                Pump_dataGridView.Rows[k].Cells[10].Value = CPump1[3];
                                Pump_dataGridView.Rows[k].Cells[11].Value = CPump1[4];
                            }
                        }
                        CPump1_textBox.Text = CPump1[0];
                    }

                    if (Value[0][6] != null && Value[0][6] != "") //냉각수펌프2
                    {
                        pump.Clear();
                        CPump2_textBox.Text = null;
                        CPump2_nonsplit = Value[0][6];
                        Split(CPump2_nonsplit, pump);
                        for (int i = 0; i < 5; i++)
                        {
                            CPump2[i] = pump[i];
                        }
                        Load_Pump_Table(CPump2[0], CPump2[1], CPump2[2]);
                        for (int k = 0; k < Pump_dataGridView.Rows.Count; k++)
                        {
                            if (Pump_dataGridView.Rows[k].Cells[1].Value == CPump2[1])
                            {
                                Pump_dataGridView.Rows[k].Cells[10].Value = CPump2[3];
                                Pump_dataGridView.Rows[k].Cells[11].Value = CPump2[4];
                            }
                        }
                        CPump2_textBox.Text = CPump2[0];
                    }
                }
                else
                {
                    Pump_pictureBox.Visible = false;
                    PumpMethod_label.Visible = false;
                    PumpMethod_comboBox.Visible = false;
                    PumpMethod_comboBox.SelectedItem = null;
                    CPumpMethod_label.Visible = false;
                    CPumpMethod_comboBox.Visible = false;
                    CPumpMethod_comboBox.SelectedItem = null;
                    Pump_dataGridView.Visible = false;
                }

            }

            //공급설비 첫번째 작성
            Value = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "공급설비1종류,공급설비2종류,공급설비3종류,공급설비4종류", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {
                Create_ce_Table();
                Load_ce();

                if (Value[0][0] != null && Value[0][0] != "")
                {
                    ce1Type = Value[0][0];
                    ce1Type_comboBox.SelectedItem = ce1Type;

                    if (ce1Type == "공조기")
                    {
                        ce1AhuText.Visible = true;
                        ce1Ahu_comboBox.Visible = true;
                        ce1AhuText2.Visible = true;
                        ce1Ahu_textBox.Visible = true;
                        ce1Ahu_button.Visible = true;

                        ce1Ahu = Value[0][2];
                        ce1Ahu_comboBox.SelectedItem = ce1Ahu;
                        List<string> 존 = new List<string>();
                        for (int i = 0; i < ce_dataGridView.Rows.Count; i++)
                        {
                            if (ce_dataGridView.Rows[i].Cells[2].Value.ToString() == ce1Ahu)
                            {
                                string data = ce_dataGridView.Rows[i].Cells[2].Value.ToString();
                                존.Add(data);
                            }
                        }
                        ce1Ahu_textBox.Text = 존[0] + " 외" + (존.Count - 1).ToString() + "개 존";

                        ce1ZoneText.Visible = false;
                        ce1Zone_textBox.Visible = false;
                        ce1Zone_button.Visible = false;
                    }
                    else
                    {
                        ce1AhuText.Visible = false;
                        ce1Ahu_comboBox.Visible = false;
                        ce1AhuText2.Visible = false;
                        ce1Ahu_textBox.Visible = false;
                        ce1Ahu_button.Visible = false;

                        ce1ZoneText.Visible = true;
                        ce1Zone_textBox.Visible = true;
                        ce1Zone_button.Visible = true;

                        List<string> 존 = new List<string>();
                        for (int i = 0; i < ce_dataGridView.Rows.Count; i++)
                        {
                            if (ce_dataGridView.Rows[i].Cells[2].Value.ToString() == ce1Type)
                            {
                                string data = ce_dataGridView.Rows[i].Cells[2].Value.ToString();
                                존.Add(data);
                            }
                        }
                        ce1Zone_textBox.Text = 존[0] + " 외" + (존.Count - 1).ToString() + "개 존";
                    }
                    imagemake(ce1Type, 1);
                }

                //공급설비 2번째 작성하기

                if (Value[0][1] != null && Value[0][1] != "")
                {
                    ce2Type = Value[0][1];
                    ce2Type_comboBox.SelectedItem = ce2Type;

                    if (ce2Type == "공조기")
                    {
                        ce2AhuText.Visible = true;
                        ce2Ahu_comboBox.Visible = true;
                        ce2AhuText2.Visible = true;
                        ce2Ahu_textBox.Visible = true;
                        ce2Ahu_button.Visible = true;

                        ce2Ahu = Value[0][3];
                        ce2Ahu_comboBox.SelectedItem = ce2Ahu;
                        List<string> 존 = new List<string>();
                        for (int i = 0; i < ce_dataGridView.Rows.Count; i++)
                        {
                            if (ce_dataGridView.Rows[i].Cells[2].Value.ToString() == ce2Ahu)
                            {
                                string data = ce_dataGridView.Rows[i].Cells[2].Value.ToString();
                                존.Add(data);
                            }
                        }
                        ce2Ahu_textBox.Text = 존[0] + " 외" + (존.Count - 1).ToString() + "개 존";

                        ce2ZoneText.Visible = false;
                        ce2Zone_textBox.Visible = false;
                        ce2Zone_button.Visible = false;
                    }
                    else
                    {
                        ce2AhuText.Visible = false;
                        ce2Ahu_comboBox.Visible = false;
                        ce2AhuText2.Visible = false;
                        ce2Ahu_textBox.Visible = false;
                        ce2Ahu_button.Visible = false;

                        ce2ZoneText.Visible = true;
                        ce2Zone_textBox.Visible = true;
                        ce2Zone_button.Visible = true;

                        List<string> 존 = new List<string>();
                        for (int i = 0; i < ce_dataGridView.Rows.Count; i++)
                        {
                            if (ce_dataGridView.Rows[i].Cells[2].Value.ToString() == ce2Type)
                            {
                                string data = ce_dataGridView.Rows[i].Cells[2].Value.ToString();
                                존.Add(data);
                            }
                        }
                        ce2Zone_textBox.Text = 존[0] + " 외" + (존.Count - 1).ToString() + "개 존";
                    }
                    imagemake(ce2Type, 2);
                }
            }
        }

        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            NumTextBox.Text = ID;
            Num = ID;
        }

        private void AirCon_Remove_button_Click(object sender, EventArgs e)
        {
            string sys;
            string[][] val = Program.DB.getValue(DB.type.ProjDB, "CoolingSystemList", "장비번호", "번호 ='" + Num + "'");
            if (val.Length > 1)
            {
                foreach (DataGridViewRow row in AirCon_dataGridView.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["check"].Value))
                    {
                        sys = row.Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "CoolingSystemList", "번호 ='" + Num + "' AND 장비번호 = '" + sys + "'");
                        //AirconList();
                    }

                }
            }
            else
            {
                MessageBox.Show("우선 저장버튼을 누른후 삭제하시기 바랍니다.");
            }
        }

        // ///////////////////////////////////////////////냉각탑////////////////////////////////////////////////////////
        #region
        private void CoolerTop_button_Click(object sender, EventArgs e)
        {
            //수냉식냉동기, 지열히트펌프, 지하수히트펌프, 흡수식냉동기
            if (Install_comboBox.Text == null)
            {
                MessageBox.Show("열원설비를 먼저 선택해 주세요.");
                return;
            }
            else
            {
                if (CG == "수냉식냉동기" || CG == "흡수식냉동기")
                {
                    Cooling_Top CoolingTop_Load = new Cooling_Top(Num, SelectCT_nonsplit, SelectCTN_nonsplit); //건식, 습식 등 종류 지정함
                    DialogResult result = CoolingTop_Load.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        if (CoolingTop_Load.SelectCT != null)
                        {
                            SelectCT_nonsplit = CoolingTop_Load.SelectCT;
                            SelectCTN_nonsplit = CoolingTop_Load.SelectCTN;

                            Split(CoolingTop_Load.SelectCT, SelectCT_split);
                            Split(CoolingTop_Load.SelectCTN, SelectCTN_split);

                            CoolingTop_Table();
                            CoolingTop_List();
                        }
                    }
                    else
                    {
                        MessageBox.Show("냉방설비가 선택되지않았습니다.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

        }
        private void CoolingTop_Table() //기본 테이블값 작성
        {
            CoolingTop_dataGridView.Visible = true;

            new StackedHeaderDecorator(CoolingTop_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

            CoolingTop_dataGridView.Rows.Clear();
            CoolingTop_dataGridView.Columns.Clear();

            CoolingTop_dataGridView.Columns.Add("A0", "번호");
            CoolingTop_dataGridView.Columns.Add("A1", "명칭");
            CoolingTop_dataGridView.Columns.Add("A2", "설치대수");
            CoolingTop_dataGridView.Columns.Add("A3", "냉각능력[kW]");
            CoolingTop_dataGridView.Columns.Add("A4", "냉방전력소비계수[kW/kW]");
            CoolingTop_dataGridView.Columns.Add("A5", "제어유형");
            CoolingTop_dataGridView.Columns.Add("A6", "팬유형");
            CoolingTop_dataGridView.Columns.Add("A7", "설치");

            CoolingTop_dataGridView.Columns[4].Width = 150;
        }
        private void CoolingTop_List() //리스트 업로드 작성함
        {
            List<string> check = new List<string>(), Type = new List<string>(); //설치, 형식   
            List<double> cwin = new List<double>(), cwout = new List<double>(), CTPower = new List<double>();

            int A = 0, B = 0, C = 0;
            double CTPower_f = 0, CTNumber_f = 0, CTCwin_f = 0, CTCwout_f = 0;

            check.Clear();
            Type.Clear();
            cwin.Clear();
            cwout.Clear();
            CTPower.Clear();

            for (int i = 0; i < SelectCT_split.Count; i++)
            {
                string[][] Val = Program.DB.getValue(DB.type.ProjDB, "User_CoolingTop", " 번호,명칭,냉각능력,냉방전력소비계수,제어유형,팬유형,설치,형식,입구온도,출구온도",
                                "번호='" + SelectCT_split[i] + "'");
                CoolingTop_dataGridView.Rows.Add();
                CoolingTop_dataGridView.Rows[i].Cells[0].Value = Val[0][0];//번호
                CoolingTop_dataGridView.Rows[i].Cells[1].Value = Val[0][1];//명칭
                CoolingTop_dataGridView.Rows[i].Cells[2].Value = SelectCTN_split[i];//설치대수
                CoolingTop_dataGridView.Rows[i].Cells[3].Value = Val[0][2];//냉각능력
                CoolingTop_dataGridView.Rows[i].Cells[4].Value = Val[0][3]; //냉방전력소비계수
                CoolingTop_dataGridView.Rows[i].Cells[5].Value = Val[0][4]; //제어유형
                CoolingTop_dataGridView.Rows[i].Cells[6].Value = Val[0][5]; //팬유형
                CoolingTop_dataGridView.Rows[i].Cells[7].Value = Val[0][6]; //설치
                Program.UTIL.dataGridView_doubleComa(CoolingTop_dataGridView, i, 2, 0);
                Program.UTIL.dataGridView_doubleComa(CoolingTop_dataGridView, i, 3, 1);
                Program.UTIL.dataGridView_doubleComa(CoolingTop_dataGridView, i, 4, 1);

                CTPower.Add(Convert.ToDouble(Val[0][2]));
                check.Add(Val[0][6]);
                Type.Add(Val[0][7]);

                cwin.Add(Convert.ToDouble(Val[0][8])); //수정
                cwout.Add(Convert.ToDouble(Val[0][9])); //수정

                for (int h = 0; h < check.Count; h++)
                {
                    if (check[h] == "기존")
                    {
                        A = 1 + A;
                    }
                    else if (check[h] == "신규")
                    {
                        B = 1 + B;
                    }
                    else C = 1 + C;
                }

                string Inst = installmake(A, B, C);

                for (int h = 0; h < CTPower.Count; h++)
                {
                    CTPower_f += CTPower[h] * Convert.ToInt32(SelectCTN_split[h]);
                    CTCwin_f += cwin[h] * Convert.ToInt32(SelectCTN_split[h]);
                    CTCwout_f += cwout[h] * Convert.ToInt32(SelectCTN_split[h]);
                    CTNumber_f += Convert.ToInt32(SelectCTN_split[h]);
                    if (CTPower.Max() == CTPower[h])
                    {
                        Install_comboBox.Text = Type[h];
                    }
                }

                CTCwin_f = CTCwin_f / CTNumber_f;
                CTCwout_f = CTCwout_f / CTNumber_f;

                //그림작성
                string contents = Install_comboBox.Text;
                Load_CSource(contents, Inst);
                CT_cwin.Visible = true;
                CT_cwin.Text = string.Format("{0}:{1:F1}", "입구[℃]", CTCwin_f);

                CT_cwout.Visible = true;
                CT_cwout.Text = string.Format("{0}:{1:F1}", "출구[℃]", CTCwout_f);
                CT_1.Visible = true;
                CT_2.Visible = true;
                CTPower_Text.Visible = true;
                CTPower_Text.Text = string.Format("{0:F1}", CTPower_f);

                //온도입력함

            }
        }
        private void CoolingTop_Save()
        {
            {
                SelectCT_nonsplit = null;
                SelectCTN_nonsplit = null;

                for (int k = 0; k < CoolingTop_dataGridView.Rows.Count; k++)
                {
                    if (k == CoolingTop_dataGridView.Rows.Count - 1)
                    {
                        SelectCT_nonsplit += CoolingTop_dataGridView.Rows[k].Cells[0].Value.ToString();
                        SelectCTN_nonsplit += (Program.UTIL.dataGridView_doubleComa(CoolingTop_dataGridView, k, 2, 0)).ToString();

                    }
                    else
                    {
                        SelectCT_nonsplit += CoolingTop_dataGridView.Rows[k].Cells[0].Value.ToString() + " + ";
                        SelectCTN_nonsplit += (Program.UTIL.dataGridView_doubleComa(CoolingTop_dataGridView, k, 2, 0)).ToString() + " + ";
                    }
                }
            }
        }
        #endregion
        // //////////////////////////////////////////////////공조존///////////////////////////////////////////////////////////////////////////
        #region 
        private void Ahu_button_Click(object sender, EventArgs e)
        {
            string select = "Ahu";
            if (CoolingSystemNameText.Text != null && CoolingSystemNameText.Text != "")
            {
                Name_f = CoolingSystemNameText.Text;

                Cooling_Zone Ahu = new Cooling_Zone(Num, SelectAHU_nonsplit, select);

                DialogResult result = Ahu.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (Ahu.SelectAhu != null)
                    {
                        SelectAHU_nonsplit = Ahu.SelectAhu;
                        Split(SelectAHU_nonsplit, AHUNameList);
                        SelectedAhuText.Visible = true;
                        SelectedAhuText.Text = AHUNameList[0].ToString() + " 외 " + (AHUNameList.Count - 1).ToString() + "개";
                        Ahumainwrite();
                    }
                    else
                    {
                        SelectAHU_nonsplit = null;
                        AHUNameList.Clear();
                        SelectedAhuText.Visible = true;
                        SelectedAhuText.Text = null;
                        Ahumainwrite();
                    }
                }
            }
            else
            {
                MessageBox.Show("먼저 명칭을 입력해 주세요!", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void ce1Ahu_button_Click(object sender, EventArgs e)
        {
            if (ce_dataGridView.Columns.Count == 0)
            {
                Create_ce_Table();
            }

            if (ce1Ahu_comboBox.Text == "" || ce1Ahu_comboBox.Text == null)
            {
                MessageBox.Show("공조기 공급설비를 먼저 선택해주세요.");
                return;
            }
            else
            {
                Cooling_ceZone ceAhu = new Cooling_ceZone(Num, SelectAHU_nonsplit, ce1Ahu_comboBox.Text); //공조기
                DialogResult result = ceAhu.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Load_ce();
                    Load_ce1Zone(ce1Ahu, "Ahu");
                }
            }
        }

        private void ce2Ahu_button_Click(object sender, EventArgs e)
        {
            if (ce_dataGridView.Columns.Count == 0)
            {
                Create_ce_Table();
            }
            if (ce2Ahu_comboBox.Text == "" || ce2Ahu_comboBox.Text == null)
            {
                MessageBox.Show("공조기 공급설비를 먼저 선택해주세요.");
                return;
            }
            else
            {
                ce2Ahu = ce2Ahu_comboBox.Text;
                Cooling_ceZone ceAhu = new Cooling_ceZone(Num, SelectAHU_nonsplit, ce2Ahu_comboBox.Text);
                DialogResult result = ceAhu.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Load_ce(); //공조기
                    Load_ce2Zone(ce2Ahu, "Ahu"); //공조기
                }
            }
        }
        #endregion
        // /////////////////////////////////////////////////////////////////펌프삭제 버튼 추가////////////////////////////////////////////////////////
        private void pump_removeBuutton_Click(object sender, EventArgs e)
        {
            int sum = 0;
            for (int i = 0; i < Pump_dataGridView.Rows.Count; i++)
            {
                if (Convert.ToBoolean(Pump_dataGridView.Rows[i].Cells[0].Value))
                {
                    sum++;
                }
            }
            if (sum > 0 && ((MessageBox.Show("선택한 펌프를 삭제 하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)))
            {
                for (int j = 0; j < Pump_dataGridView.Rows.Count; j++)
                {
                    if (Convert.ToBoolean(Pump_dataGridView.Rows[j].Cells[0].Value))
                    {
                        Pump_dataGridView.Rows.Remove(Pump_dataGridView.Rows[j]);
                        break;
                    }
                }
            }
        }

    }

}
