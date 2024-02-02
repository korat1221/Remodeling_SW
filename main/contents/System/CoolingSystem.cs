using main.contentslist;
using main.subcontents;
using main.subcontents.CoolingSystem;
using main.subcontents.HeatingSystem;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static main.DB;
using static System.Net.Mime.MediaTypeNames;

namespace main.contents
{


    public partial class CoolingSystem : Form
    {
        //존및 공조기관련
        string SelectedZone, SelectedAHU;
        string MultiZone, MultiAhu; //단일존, 멀티존인지 판별하는것

        //생산설비
        string[][] 프로젝트유형;
        string CG, Num, Name_f;
        string Install_f, Control_f, CSource, Fuel_f, Econo_f, Supp_f, Comp_f, Refri, Eva_f; //부하측열원공급설비(Supp_f) 및 설치(기존,신규)가 추가됨
        double Power_f, EER_f, Pctrl_f, CWout_f, CWin_f, CSout_f, CSin_f, Number_f; //CWout_f 냉수출구온도  CSWout_f 냉각수출구온도

        //냉방존
        double ZoneNumber_f = 0, QC_a_z = 0, QC_max_z = 0, A_z = 0; //새로작성함

        //공조존
        double AhuNumber_f = 0, QC_a_Ahu = 0, QC_max_Ahu = 0, A_Ahu = 0; //새로작성함
        //냉방존 + 공조존 합계
        double SysNumber, QC_a, QC_max, A;

        //냉각탑
        string CTower_f, CTControl_f, CTPump, CTpumpValve, CTpumpControl;
        int CTNum_f, CTpumpNum;


        List<string> ZoneNameList = new List<string>();
        List<string> AHUNmaeList = new List<string>();

        //장비관련
        List<double> Power = new List<double>(), EER = new List<double>(), Pctrl = new List<double>(), CWout = new List<double>(), CWin = new List<double>();
        List<double> CSout = new List<double>(), CSin = new List<double>(); //지열냉각수
        List<double> Number = new List<double>(); //설치대수
        List<string> Comp = new List<string>(), Supp = new List<string>(), Eva = new List<string>(); //공냉식, 수냉식, 지열히트펌프 유형
        List<string> CGS = new List<string>(); //장비번호 즉 이름임 Name


        //저장설비
        string Stotype, StoSource;


        //펌프정의
        string PumpUse, PumpMethod, Pump1, Pump2, Pump1Valve, Pump2Valve, Pump1Control, Pump2Control;
        int Pump1Num, Pump2Num;

        //지열펌프
        string Soilpump, SoilpumpValve, SoilpumpControl;


        //공급설비정의
        string ce1Type, ce2Type, ce3Type;
        string[] ceType = { "공조기", "실내기", "팬코일유닛", "복사냉방(천장)", "복사냉방(벽)", "바닥매립형컨백터" };


        ArrayList SelectAirConditioning = new ArrayList(); ArrayList SelectPump = new ArrayList(); ArrayList Selectce1Zone = new ArrayList(); ArrayList Selectce2Zone = new ArrayList();
        int ce_SelectRow;




        public CoolingSystem()
        {
            InitializeComponent();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '냉방시스템'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");

            //시스템 콤보박스
            CoolingGeneratorSelect_comboBox.Items.Clear();
            CoolingGeneratorSelect_comboBox.Items.AddRange(Systemtype.ToArray());

            //저장 설비 콤보박스
            StorageList_comboBox.Items.AddRange(storagelist());

            //펌프 유무 콤보박스 
            PumpUse_comboBox.Items.Clear();
            PumpUse_comboBox.Items.Add("펌프 있음");
            PumpUse_comboBox.Items.Add("펌프 없음(설비 내장)");
            PumpUse_comboBox.SelectedIndex = 1;
            //펌프 방식 콤보박스
            PumpMethod_comboBox.Items.Clear();
            PumpMethod_comboBox.Items.Add("1차펌프");
            PumpMethod_comboBox.Items.Add("1차폐회로+2차펌프");

            //공급설비 콤보박스
            ce1Type_comboBox.Items.AddRange(ceType.ToArray());
            ce2Type_comboBox.Items.AddRange(ceType.ToArray());

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

            if (Program.UTIL.fromCode)
            {
                Program.UTIL.fromCode = false;
                return;
            }

            if (CoolingSystemNameText.Text != null && CoolingSystemNameText.Text != "")
            {
                string[] coolingzone_connect = new string[2];
                Name_f = CoolingSystemNameText.Text;

                coolingzone_connect[0] = Num; //번호
                coolingzone_connect[1] = Name_f; //명칭


                Cooling_Zone ZC = new Cooling_Zone(coolingzone_connect);

                DialogResult result = ZC.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Zonemainwrite(Num); 
                }
            }
            else
            {
                MessageBox.Show("먼저 명칭을 입력해 주세요!", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }
        //Zone 매인 윗화면 작성항목 만들기
        private void Zonemainwrite(string _num) //_num은 시스템 번호임
        {
            string[][] zonenames = Program.DB.getValue(DB.type.ProjDB, "CoolingZone", "존번호", "번호='" + _num + "'");
            
            A_z = 0;
            QC_a_z = 0;
            QC_max_z = 0;
            ZoneNumber_f = zonenames.Length;

            if (zonenames.Length > 0)
            {
                for (int i = 0; i < zonenames.Length; i++)
                {
                    string[][] ZoneGet0 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Zone_HCneed_Result",
                     "Qb_a,Q_max", "번호= '" + zonenames[i][0] + "' And 비이용일_이용일 = '이용일' And 난방_냉방 = '냉방'");
                    if (ZoneGet0.Length > 0)
                    {
                        QC_a_z += Convert.ToDouble(ZoneGet0[0][0]);
                        QC_max_z += Convert.ToDouble(ZoneGet0[0][1]) / 1000;
                    }
                    string[][] ZoneGet1 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form",
                    "순바닥면적", //값이있는 열
                    "존번호='" + zonenames[i][0] + //조건1
                    "'"); //마지막
                    if (ZoneGet1.Length > 0)
                    {
                        A_z += Convert.ToDouble(ZoneGet1[0][0]);
                        ZoneNameList.Add(zonenames[i][0].ToString());
                    }
                }
                int num = zonenames.Length - 1;
                SelectedZone = null;
                SelectedZone = zonenames[0][0].ToString() + " 외 " + num + "개";
                SelectedZoneText.Text = SelectedZone;
                if (zonenames.Length == 1) MultiZone = "존 공급방식:  단일존";
                else MultiZone = "존 공급방식:  멀티존";
                ZoneS_label.Text = MultiZone;

            }
            QC_a = QC_a_z + QC_a_Ahu;
            A = A_z + A_Ahu;
            QC_max = QC_max_z + QC_max_Ahu; 
                        
            CZ_AnnualCoolingNeed_Textbox.Text = QC_a.ToString("0");
            CZ_FloorArea_Textbox.Text = A.ToString("0.00");
            CZ_MaxCoolingLoad_Textbox.Text = QC_max.ToString("0.00");
        }
        private void Ahumainwrite(string _num)
        {
            A_Ahu = 0;
            QC_a_Ahu = 0;
            QC_max_Ahu = 0;

            QC_a = QC_a_z + QC_a_Ahu;
            A = A_z + A_Ahu;
            QC_max = QC_max_z + QC_max_Ahu;

            CZ_AnnualCoolingNeed_Textbox.Text = QC_a.ToString("0");
            CZ_FloorArea_Textbox.Text = A.ToString("0.00");
            CZ_MaxCoolingLoad_Textbox.Text = QC_max.ToString("0.00");

            //작성해야함
        }


        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {

            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
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
            LoadtabPage(CG);
            Distribute_Image();
        }

        private void LoadtabPage(string _CG) //탭활성화 및 열원설비 콤보박스
        {
            if (_CG == "실외기12kW")
            {
                Install_comboBox.Items.Clear();
                Install_comboBox.Items.AddRange(Installtype("열원설비").ToArray());
                tabControl2.SelectedTab = tabControl2.TabPages["AirCon_tabPage"];

            }
            else if (_CG == "공냉식냉동기")
            {
                Install_comboBox.Items.Clear();
                Install_comboBox.Items.AddRange(Installtype("열원설비").ToArray());
                tabControl2.SelectedTab = tabControl2.TabPages["AirCooler_tabPage"];
            }
            else if (_CG == "수냉식냉동기")
            {
                Install_comboBox.Items.Clear();
                Install_comboBox.Items.AddRange(Installtype("C열원설비").ToArray());
                tabControl2.SelectedTab = tabControl2.TabPages["WaterCooler_tabPage"];
            }
            else if (_CG == "흡수식냉동기")
            {
                Install_comboBox.Items.Clear();
                Install_comboBox.Items.AddRange(Installtype("C열원설비").ToArray());
                tabControl2.SelectedTab = tabControl2.TabPages["AbsorbCooler_tabPage"];
            }

            else if (_CG == "지열히트펌프")
            {
                Install_comboBox.Items.Clear();
                Install_comboBox.Items.AddRange(Installtype("S열원설비").ToArray());
                tabControl2.SelectedTab = tabControl2.TabPages["SoilCooler_tabPage"];
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
                LoadCSource(contents, Install_f);

            }

        }

        private void LoadCSource(string csource, string install)
        {
            string[][] image = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉방설비이미지", "이미지", "설비유형='" + csource + "' And 설치유형='" + install + "'");
            if (image.Length > 0)
            {
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
                string[][] stoimage = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉방설비이미지", "이미지", "항목유형='저장설비' And 설비유형 = '" + Stotype + "' And 설치유형='" + Install_f + "'");
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
                string[][] stoTimage1 = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉방설비이미지", "이미지", "항목유형= '저장설비' And  설비유형= '" + StoSource + "'");
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
        }

        private void imagemake(string _type) //공급설비 그림 넣기
        {
            string[][] image = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉방설비이미지", "이미지", "항목유형= '공급설비' And 설비유형='" + _type + "'");
            if (image.Length > 0)
            {
                if (_type == ce1Type)
                {
                    ce1_pictureBox.Visible = true;
                    ce1_pictureBox.Size = new System.Drawing.Size(260, 60);
                    ce1_pictureBox.Location = new Point(250, 10);
                    ce1_pictureBox.Load(Program.gPath + image[0][0]);
                    ce1_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    ce1_pictureBox.BackColor = Color.Transparent;
                    ce1_pictureBox.Parent = DistpictureBox;
                }
                else if (_type == ce2Type)
                {
                    ce2_pictureBox.Visible = true;
                    ce2_pictureBox.Size = new System.Drawing.Size(260, 60);
                    ce2_pictureBox.Location = new Point(250, 80);
                    ce2_pictureBox.Load(Program.gPath + image[0][0]);
                    ce2_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    ce2_pictureBox.BackColor = Color.Transparent;
                    ce2_pictureBox.Parent = DistpictureBox;
                }
                else if (_type == ce3Type)
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
                        Load_AirCon();
                        break;
                    case "공냉식냉동기":
                        Load_AirCooler();
                        break;
                    case "수냉식냉동기":
                        Load_WaterCooler();
                        break;
                    case "흡수식냉동기":
                        Load_AbsorbCooler();
                        break;
                    case "지열히트펌프":
                        Load_SoilCooler();
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

        #region A. AirCon 작성
        private void Load_AirCon() //설비버튼 클릭시 작동
        {
            Cooling_AirCon AirCon_Load = new Cooling_AirCon();
            DialogResult result = AirCon_Load.ShowDialog();

            if (result == DialogResult.OK)
            {
                AirConLoad_table();//

                if (AirCon_Load.SelectAirCon != null)
                {
                    List<string> check = new List<string>();
                    int A = 0, B = 0, C = 0;

                    AirCon_dataGridView.Rows.Clear();

                    foreach (string SAC in AirCon_Load.SelectAirCon)
                    {

                        string[][] DefaultDB_Value = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", " 번호,명칭,DB유형,냉방정격용량,냉방정격소비전력,냉방정격COP,대기전력,연료,설치",
                               "번호='" + SAC + "'");
                        if (DefaultDB_Value.Length > 0)
                        {
                            for (int i = 0; i < DefaultDB_Value.Length; i++)
                            {

                                AirCon_dataGridView.Rows.Add();
                                int nRow = AirCon_dataGridView.Rows.Count - 1;

                                AirCon_dataGridView.Rows[nRow].Cells[1].Value = DefaultDB_Value[i][0];
                                AirCon_dataGridView.Rows[nRow].Cells[2].Value = DefaultDB_Value[i][1];
                                AirCon_dataGridView.Rows[nRow].Cells[3].Value = DefaultDB_Value[i][2];
                                AirCon_dataGridView.Rows[nRow].Cells[7].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][3])); // 냉방출력
                                AirCon_dataGridView.Rows[nRow].Cells[8].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][4])); //소비전력
                                AirCon_dataGridView.Rows[nRow].Cells[9].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][5])); //COP
                                AirCon_dataGridView.Rows[nRow].Cells[10].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][6])); //대기전력
                                AirCon_dataGridView.Rows[nRow].Cells[11].Value = DefaultDB_Value[i][7]; //연료
                                AirCon_dataGridView.Rows[nRow].Cells[12].Value = DefaultDB_Value[i][8]; //설치
                                check.Add(DefaultDB_Value[i][8].ToString());
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

                }

                CoolingGeneratorImageSelect(CG, Install_f);
            }

        }

        private void AirconList() //다시불러올때
        {

            Program.UTIL.ffCode = true;
            string[][] DataValue = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "프로젝트유형,명칭,냉방설비,열원설비,냉방출력,냉방성능,설치대수,제어유형,연료,외기냉방시스템,대기전력,저장탱크,저장유형", "번호='" + Num + "'");
            string[] Value = new string[13];
            if (DataValue.Length > 0)
            {
                for (int i = 0; i < 13; i++)
                {
                    Value[i] = DataValue[0][i];
                }
            }

            CoolingSystemNameText.Text = Convert.ToString(Value[1]);
            Name_f = CoolingSystemNameText.Text;

            CoolingGeneratorSelect_comboBox.Text = Value[2];
            CG = CoolingGeneratorSelect_comboBox.Text;
            CSource = Value[3];
            Install_comboBox.Text = CSource;
            Distribute_Image();

            Power_f = Convert.ToDouble(Value[4]);
            EER_f = Convert.ToDouble(Value[5]);
            Number_f = Convert.ToDouble(Value[6]);

            PowerTotal_textBox.Text = string.Format("{0:0.0}", Power_f);
            EERTotal_textBox.Text = string.Format("{0:0.0}", EER_f);
            InstallTotal_textBox.Text = string.Format("{0:0.0}", Number_f);
            G_label.Visible = true;
            G_label.Text = string.Format("설치대수: {0}", Number_f);
            ZoneS_label.Visible = true;

            StorageList_comboBox.Text = Value[11];
            StorageType_comboBox.Text = Value[12];

            AirConLoad_table();
            List<string> check = new List<string>();
            int A = 0, B = 0, C = 0;
            Power_f = 0;
            EER_f = 0;
            Number_f = 0;
            Power.Clear();
            EER.Clear();

            AirCon_dataGridView.Rows.Clear();
            string[][] System = Program.DB.getValue(DB.type.ProjDB, "CoolingSystemList", " 장비번호,장비명칭,DB유형,제어유형,외기냉방유무,설치대수,냉방출력,냉방전력,EER,대기전력,연료,설치", "번호='" + Num + "'");
            if (System.Length > 0)
            {
                for (int g = 0; g < System.Length; g++)
                {

                    AirCon_dataGridView.Rows.Add();
                    int nRow = AirCon_dataGridView.Rows.Count - 1;

                    AirCon_dataGridView.Rows[nRow].Cells[1].Value = System[g][0];//장비번호
                    AirCon_dataGridView.Rows[nRow].Cells[2].Value = System[g][1];//장비명칭
                    AirCon_dataGridView.Rows[nRow].Cells[3].Value = System[g][2];//DB유형
                    AirCon_dataGridView.Rows[nRow].Cells[4].Value = System[g][3];//제어유형
                    AirCon_dataGridView.Rows[nRow].Cells[5].Value = System[g][4]; //외기냉방유무
                    AirCon_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F1}", Convert.ToDouble(System[g][5])); //설치대수
                    AirCon_dataGridView.Rows[nRow].Cells[7].Value = string.Format("{0:F1}", Convert.ToDouble(System[g][6])); // 냉방출력
                    AirCon_dataGridView.Rows[nRow].Cells[8].Value = string.Format("{0:F1}", Convert.ToDouble(System[g][7])); //소비전력
                    AirCon_dataGridView.Rows[nRow].Cells[9].Value = string.Format("{0:F1}", Convert.ToDouble(System[g][8])); //COP,EER
                    AirCon_dataGridView.Rows[nRow].Cells[10].Value = string.Format("{0:F1}", Convert.ToDouble(System[g][9])); //대기전력
                    AirCon_dataGridView.Rows[nRow].Cells[11].Value = System[g][10]; //연료
                    AirCon_dataGridView.Rows[nRow].Cells[12].Value = System[g][11]; //설치

                    check.Add(System[g][11].ToString());

                    Power.Add(Convert.ToDouble(System[g][6]) * Convert.ToDouble(System[g][5]));
                    EER.Add(Convert.ToDouble(System[g][8]));
                    Number_f = Convert.ToDouble(System[g][5]) + Number_f;

                }
            }

            for (int h = 0; h < Power.Count; h++)
            {
                EER_f += Power[h] * EER[h]; //파워가중 평균값을 적용해야함
                Power_f += Power[h];

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

            CoolingGeneratorImageSelect(CG, Install_f);
            LoadCSource(CSource, Install_f);
        }

        private void AirConLoad_table()
        {

            string[][] items = Program.DB.getValue_SameCheck(DB.type.BaseDB_Cooling, "CoolSystem", " 제어유형,압축기유형,멀티공급유형",
                               "냉동기유형='실외기12kW'"); //각 유형별 항목 만들기
            new StackedHeaderDecorator(AirCon_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

            AirCon_dataGridView.Columns.Clear();

            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            AirCon_dataGridView.Columns.Add(checkBoxColumn);

            AirCon_dataGridView.Columns.Add("A1", "번호");
            AirCon_dataGridView.Columns.Add("A2", "명칭");
            AirCon_dataGridView.Columns.Add("A3", "DB유형");

            DataGridViewComboBoxColumn ControlcomboBox = new DataGridViewComboBoxColumn();
            ControlcomboBox.HeaderText = "제어유형";
            ControlcomboBox.Name = "control";
            ControlcomboBox.Items.AddRange(new string[] { "on/off제어", "인버터제어" });
            AirCon_dataGridView.Columns.Add(ControlcomboBox);


            DataGridViewComboBoxColumn EconomcomboBox = new DataGridViewComboBoxColumn();
            EconomcomboBox.HeaderText = "외기냉방유무";
            EconomcomboBox.Name = "Economizer";
            EconomcomboBox.Items.AddRange(new string[] { "있음", "없음" });
            AirCon_dataGridView.Columns.Add(EconomcomboBox);

            AirCon_dataGridView.Columns.Add("A6", "설치대수");
            AirCon_dataGridView.Columns.Add("A7", "냉방출력[kW]");
            AirCon_dataGridView.Columns.Add("A8", "소비전력[kW]");
            AirCon_dataGridView.Columns.Add("A9", "냉방성능[EER]");
            AirCon_dataGridView.Columns.Add("A10", "대기전력");
            AirCon_dataGridView.Columns.Add("A11", "연료");
            AirCon_dataGridView.Columns.Add("A12", "설치");
            AirCon_dataGridView.Columns[0].Width = 40;
            AirCon_dataGridView.Columns[1].Width = 50;
        }

        private void GAirCon_Save_button_Click(object sender, EventArgs e) //나중에 다시 그대로 가져옴
        {

            Power_f = 0;
            EER_f = 0;
            Number_f = 0;
            Pctrl_f = 0;
            Power.Clear();
            EER.Clear();
            Number.Clear();

            for (int k = 0; k < AirCon_dataGridView.Rows.Count; k++)
            {
                List<string> Value = new List<string>();
                for (int i = 1; i < 13; i++)
                {
                    if (AirCon_dataGridView.Rows[k].Cells[i].Value != null)
                    {
                        Value.Add(AirCon_dataGridView.Rows[k].Cells[i].Value.ToString());
                    }
                    else
                    {
                        MessageBox.Show("선택항목을 완료해주세요.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }


                Power.Add(Convert.ToDouble(Value[6]));
                EER.Add(Convert.ToDouble(Value[8]));
                Number.Add(Convert.ToDouble(Value[5]));
                Pctrl.Add(Convert.ToDouble(Value[9]));
                CGS.Add(Value[0]);

                Program.DB.setValue(DB.type.ProjDB, "CoolingSystemList", "번호,명칭,장비번호,장비명칭,DB유형,제어유형,외기냉방유무,설치대수, 냉방출력, 냉방전력, EER, 대기전력, 연료, 설치",
                 "'" + Num + "','" + Name_f + "','" + Value[0] + "','" + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "'," +
                 "'" + Value[7] + "', '" + Value[8] + "', '" + Value[9] + "','" + Value[10] + "', '" + Value[11] + "'", "번호, 장비번호");
            }

            int a = Power.IndexOf(Power.Max());

            string[][] Max_info = Program.DB.getValue(DB.type.ProjDB, "CoolingSystemList", "제어유형,외기냉방유무,연료", "번호 = '" + Num + "' And 장비번호 = '" + CGS[a] + "'");
            if (Max_info.Length > 0)
            {
                Control_f = Convert.ToString(Max_info[0][0]);
                Econo_f = Convert.ToString(Max_info[0][1]);
                Fuel_f = Convert.ToString(Max_info[0][2]);
            }
            for (int h = 0; h < Power.Count; h++)
            {
                EER_f += Power[h] * EER[h] * Number[h]; //파워가중 평균값을 적용해야함
                Power_f += Power[h] * Number[h];
                Pctrl_f += Pctrl[h] * Number[h];
                Number_f += Number[h];
            }
            EER_f = EER_f / Power_f;
            Supp_f = "직팽식";

            PowerTotal_textBox.Text = string.Format("{0:0.0}", Power_f);
            EERTotal_textBox.Text = string.Format("{0:0.0}", EER_f);
            InstallTotal_textBox.Text = string.Format("{0:0.0}", Number_f);
            
            MessageBox.Show("저장되었습니다.");

        }

        #endregion
        #region B. AirCooler 작성

        private void Load_AirCooler()
        {
            Cooling_AirCooler AirCooler_Load = new Cooling_AirCooler();
            DialogResult result = AirCooler_Load.ShowDialog();

            if (result == DialogResult.OK)
            {
                AirCoolerLoad_table();

                if (AirCooler_Load.SelectItem != null)
                {
                    List<string> check = new List<string>();
                    int A = 0, B = 0, C = 0;

                    Power_f = 0;
                    Power.Clear();
                    Comp.Clear();
                    Supp.Clear();
                    Eva.Clear();

                    AirCooler_dataGridView.Rows.Clear();

                    for (int j = 0; j < AirCooler_Load.SelectItem.Count; j++)
                    {
                        string SAC = AirCooler_Load.SelectItem[j].SelectAirCooler; // 설비번호
                        string PPE = AirCooler_Load.SelectItem[j].SelectPressor; //압축기 이름

                        string[][] CtrlType = Program.DB.getValue_SameCheck(DB.type.BaseDB_Cooling, "CoolSystem", "제어유형", "냉동기유형 = '공냉식냉동기' And 압축기유형 = '" + PPE + "'");

                        string[][] DefaultDB_Value = Program.DB.getValue(DB.type.ProjDB, "User_AirCooler", "번호,명칭,DB유형,냉방출력,냉방소비전력,EER,대기전력,연료,압축기,설치,부하측공급형식,증발기,냉수입구온도,냉수출구온도",
                              "번호='" + SAC + "'");

                        if (DefaultDB_Value.Length > 0)
                        {

                            AirCooler_dataGridView.Rows.Add();
                            int nRow = AirCooler_dataGridView.Rows.Count - 1;
                            AirCooler_dataGridView.Rows[nRow].Cells[1].Value = DefaultDB_Value[0][0];
                            AirCooler_dataGridView.Rows[nRow].Cells[2].Value = DefaultDB_Value[0][1];
                            AirCooler_dataGridView.Rows[nRow].Cells[3].Value = DefaultDB_Value[0][2];

                            DataGridViewComboBoxCell CtrlCombo = new DataGridViewComboBoxCell();
                            if (CtrlType.Length > 0)
                            {
                                for (int l = 0; l < CtrlType.Length; l++)
                                {
                                    CtrlCombo.Items.Add(CtrlType[l][0]);
                                }
                            }
                            AirCooler_dataGridView.Rows[nRow].Cells[4] = CtrlCombo; //제어유형
                            AirCooler_dataGridView.Rows[nRow].Cells[7].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[0][3])); //냉방출력
                            AirCooler_dataGridView.Rows[nRow].Cells[8].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[0][4])); //냉방소비전력
                            AirCooler_dataGridView.Rows[nRow].Cells[9].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[0][5])); //EER
                            AirCooler_dataGridView.Rows[nRow].Cells[10].Value = DefaultDB_Value[0][6]; //대기전력
                            AirCooler_dataGridView.Rows[nRow].Cells[11].Value = DefaultDB_Value[0][7]; //연료
                            AirCooler_dataGridView.Rows[nRow].Cells[12].Value = DefaultDB_Value[0][8]; //압축기
                            check.Add(DefaultDB_Value[0][9]);
                            AirCooler_dataGridView.Rows[nRow].Cells[13].Value = DefaultDB_Value[0][10]; //부하측공급형식
                            AirCooler_dataGridView.Rows[nRow].Cells[14].Value = DefaultDB_Value[0][11]; //증발기
                            AirCooler_dataGridView.Rows[nRow].Cells[15].Value = DefaultDB_Value[0][12]; //냉수입구온도
                            AirCooler_dataGridView.Rows[nRow].Cells[16].Value = DefaultDB_Value[0][13]; //냉수출구온도
                            AirCooler_dataGridView.Rows[nRow].Cells[17].Value = DefaultDB_Value[0][9]; //설치
                            Power.Add(Convert.ToDouble(DefaultDB_Value[0][3]));

                            //EER.Add(Convert.ToDouble(DefaultDB_Value[i][5]));
                            Comp.Add(DefaultDB_Value[0][8]);
                            //추가사항
                            Supp.Add(DefaultDB_Value[0][10]);
                            Eva.Add(DefaultDB_Value[0][11]);
                        }
                        else
                        {
                            string[][] DB_Value = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", "번호,명칭,DB유형,냉방정격용량,냉방정격소비전력,냉방정격COP,대기전력,연료,설치",
                             "번호='" + SAC + "'");
                            if (DB_Value.Length > 0)
                            {
                                AirCooler_dataGridView.Rows.Add();
                                int nRow = AirCooler_dataGridView.Rows.Count - 1;
                                AirCooler_dataGridView.Rows[nRow].Cells[1].Value = DB_Value[0][0];
                                AirCooler_dataGridView.Rows[nRow].Cells[2].Value = DB_Value[0][1];
                                AirCooler_dataGridView.Rows[nRow].Cells[3].Value = DB_Value[0][2];

                                DataGridViewComboBoxCell CtrlCombo = new DataGridViewComboBoxCell();

                                for (int l = 0; l < CtrlType.Length; l++)
                                {
                                    CtrlCombo.Items.Add(CtrlType[l][0]);
                                }
                                AirCooler_dataGridView.Rows[nRow].Cells[4] = CtrlCombo; //제어유형
                                AirCooler_dataGridView.Rows[nRow].Cells[7].Value = string.Format("{0:F1}", Convert.ToDouble(DB_Value[0][3])); //냉방출력
                                AirCooler_dataGridView.Rows[nRow].Cells[8].Value = string.Format("{0:F1}", Convert.ToDouble(DB_Value[0][4])); //냉방소비전력
                                AirCooler_dataGridView.Rows[nRow].Cells[9].Value = string.Format("{0:F1}", Convert.ToDouble(DB_Value[0][5])); //EER
                                AirCooler_dataGridView.Rows[nRow].Cells[10].Value = DB_Value[0][6]; //대기전력
                                AirCooler_dataGridView.Rows[nRow].Cells[11].Value = DB_Value[0][7]; //연료
                                AirCooler_dataGridView.Rows[nRow].Cells[12].Value = PPE; //압축기
                                check.Add(DB_Value[0][8]);
                                AirCooler_dataGridView.Rows[nRow].Cells[13].Value = "직팽식"; //부하측공급형식
                                AirCooler_dataGridView.Rows[nRow].Cells[14].Value = null; //증발기
                                AirCooler_dataGridView.Rows[nRow].Cells[15].Value = null; //냉수입구온도
                                AirCooler_dataGridView.Rows[nRow].Cells[16].Value = null; //냉수출구온도
                                AirCooler_dataGridView.Rows[nRow].Cells[17].Value = null; //설치

                                Power.Add(Convert.ToDouble(DB_Value[0][3]));

                                //EER.Add(Convert.ToDouble(DefaultDB_Value[i][5]));
                                Comp.Add(PPE);
                                Supp.Add("직팽식");

                            }
                        }

                    }

                    for (int h = 0; h < Comp.Count; h++)
                    {
                        if (Power.Max() == Power[h])
                        {
                            Comp_f = Comp[h];
                            Supp_f = Supp[h];
                            if (Eva.Count() > 0)
                            {
                                Eva_f = Eva[h];
                            }
                            else Eva_f = null;
                        }


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
                }

                CoolingGeneratorImageSelect(CG, Install_f);
                LoadPressImage(Comp_f, Install_f);

                if (Eva_f != null)
                {
                    LoadEvaImage(Eva_f, Install_f);
                }
            }
        }

        private void AirCoolerList() //다시불러올때
        {
            Program.UTIL.ffCode = true;
            string[][] DataValue = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "프로젝트유형,명칭,냉방설비,열원설비,냉방출력,냉방성능,설치대수,제어유형,연료,외기냉방시스템,대기전력,부하측열원공급설비,냉수출구온도,냉수입구온도,저장탱크,저장유형,압축기종류,증발기", "번호='" + Num + "'");
            string[] Value = new string[18];
            if (DataValue.Length > 0)
            {
                for (int i = 0; i < 18; i++)
                {
                    if (DataValue[0][i] == null)
                    {
                        Value[i] = null;
                    }
                    else if (DataValue[0][i] != null)
                    {
                        Value[i] = DataValue[0][i];
                    }
                }
            }
            CoolingSystemNameText.Text = Convert.ToString(Value[1]);
            Name_f = CoolingSystemNameText.Text;

            CoolingGeneratorSelect_comboBox.Text = Value[2];
            CG = CoolingGeneratorSelect_comboBox.Text;
            CSource = Value[3];
            Install_comboBox.Text = CSource;
            Distribute_Image();

            Power_f = Convert.ToDouble(Value[4]);
            EER_f = Convert.ToDouble(Value[5]);
            Number_f = Convert.ToDouble(Value[6]);
            Supp_f = Value[11];
            Comp_f = Value[16];

            PowerTotal_textBox.Text = string.Format("{0:0.0}", Power_f);
            EERTotal_textBox.Text = string.Format("{0:0.0}", EER_f);
            InstallTotal_textBox.Text = string.Format("{0:0.0}", Number_f);
            G_label.Visible = true;
            G_label.Text = string.Format("설치대수: {0}", Number_f);
            ZoneS_label.Visible = true;

            AirCoolerLoad_table();

            List<string> check = new List<string>();
            int A = 0, B = 0, C = 0;

            AirCooler_dataGridView.Rows.Clear();

            string[][] System = Program.DB.getValue(DB.type.ProjDB, "CoolingSystemList", " 장비번호, 장비명칭, DB유형, 제어유형, 외기냉방유무, 설치대수, " +
                "냉방출력, 냉방전력, EER, 대기전력, 연료, 압축기, 부하측형식, 증발기, 냉수입구온도, 냉수출구온도, 설치", "번호='" + Num + "'");
            if (System.Length > 0)
            {
                for (int g = 0; g < System.Length; g++)
                {
                    AirCooler_dataGridView.Rows.Add();
                    int nRow = AirCooler_dataGridView.Rows.Count - 1;

                    AirCooler_dataGridView.Rows[nRow].Cells[1].Value = System[g][0]; //장비번호
                    AirCooler_dataGridView.Rows[nRow].Cells[2].Value = System[g][1]; //장비명칭
                    AirCooler_dataGridView.Rows[nRow].Cells[3].Value = System[g][2]; //DB유형

                    AirCooler_dataGridView.Rows[nRow].Cells[4].Value = System[g][3]; //제어유형
                    AirCooler_dataGridView.Rows[nRow].Cells[5].Value = System[g][4]; //외기냉방유무

                    AirCooler_dataGridView.Rows[nRow].Cells[6].Value = System[g][5]; //설치대수
                    AirCooler_dataGridView.Rows[nRow].Cells[7].Value = string.Format("{0:F1}", Convert.ToDouble(System[g][6])); // 냉방출력
                    Power.Add(Convert.ToDouble(System[g][6]));


                    AirCooler_dataGridView.Rows[nRow].Cells[8].Value = string.Format("{0:F1}", Convert.ToDouble(System[g][7])); //소비전력
                    AirCooler_dataGridView.Rows[nRow].Cells[9].Value = string.Format("{0:F1}", Convert.ToDouble(System[g][8])); //COP
                    EER.Add(Convert.ToDouble(System[g][8]));

                    AirCooler_dataGridView.Rows[nRow].Cells[10].Value = string.Format("{0:F1}", Convert.ToDouble(System[g][9])); //대기전력
                    AirCooler_dataGridView.Rows[nRow].Cells[11].Value = System[g][10]; //연료
                    AirCooler_dataGridView.Rows[nRow].Cells[12].Value = System[g][11]; //압축기
                    AirCooler_dataGridView.Rows[nRow].Cells[13].Value = System[g][12]; //부하측형식
                    AirCooler_dataGridView.Rows[nRow].Cells[14].Value = System[g][13]; //증발기
                    AirCooler_dataGridView.Rows[nRow].Cells[15].Value = System[g][14]; //냉수입구온도
                    if (System[g][14].Length > 0)
                    {
                        CWin.Add(Convert.ToDouble(System[g][14]));
                    }
                    AirCooler_dataGridView.Rows[nRow].Cells[16].Value = System[g][15]; //냉수출구온도
                    if (System[g][15].Length > 0)
                    {
                        CWout.Add(Convert.ToDouble(System[g][15]));
                    }

                    AirCooler_dataGridView.Rows[nRow].Cells[17].Value = System[g][16]; //설치
                    check.Add(System[g][16].ToString());
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

            Eva_f = Value[17];
            StorageList_comboBox.Text = Value[14];
            StorageType_comboBox.Text = Value[15];

            CoolingGeneratorImageSelect(CG, Install_f);
            LoadPressImage(Comp_f, Install_f);

            //부하측 형식이 결정되어야함
            if (Supp_f == "수방식")
            {
                LoadEvaImage(Eva_f, Install_f);
            }
            LoadCSource(CSource, Install_f);
        }



        private void AirCoolerLoad_table() //테이블 불러오기
        {
            new StackedHeaderDecorator(AirCooler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

            AirCooler_dataGridView.Columns.Clear();

            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            AirCooler_dataGridView.Columns.Add(checkBoxColumn);

            AirCooler_dataGridView.Columns.Add("A1", "번호");
            AirCooler_dataGridView.Columns.Add("A2", "명칭");
            AirCooler_dataGridView.Columns.Add("A3", "DB유형");
            AirCooler_dataGridView.Columns.Add("A4", "제어유형");

            DataGridViewComboBoxColumn EconomcomboBox = new DataGridViewComboBoxColumn();
            EconomcomboBox.Items.Clear();
            EconomcomboBox.HeaderText = "외기냉방유무";
            EconomcomboBox.Name = "Economizer";
            EconomcomboBox.Items.AddRange(new string[] { "있음", "없음" });
            AirCooler_dataGridView.Columns.Add(EconomcomboBox);

            AirCooler_dataGridView.Columns.Add("A6", "설치대수");
            AirCooler_dataGridView.Columns.Add("A7", "냉방.출력[kW]");
            AirCooler_dataGridView.Columns.Add("A8", "냉방.전력[kW]");
            AirCooler_dataGridView.Columns.Add("A9", "냉방.EER[W/W]");

            AirCooler_dataGridView.Columns.Add("A10", "대기전력");
            AirCooler_dataGridView.Columns.Add("A11", "연료");
            //추가항목
            AirCooler_dataGridView.Columns.Add("A12", "압축기");
            AirCooler_dataGridView.Columns.Add("A13", "부하측.형식");
            AirCooler_dataGridView.Columns.Add("A14", "부하측.증발기");
            AirCooler_dataGridView.Columns.Add("A15", "냉수온도.입구");
            AirCooler_dataGridView.Columns.Add("A16", "냉수온도.출구");
            AirCooler_dataGridView.Columns.Add("A17", "설치");


            AirCooler_dataGridView.Columns[0].Width = 40;
            AirCooler_dataGridView.Columns[1].Width = 50;
        }

        private void GAirCooler_Save_button_Click(object sender, EventArgs e)
        {
            //로드에서 결정사항
            //파워 power.add
            //압축기종류 comp.add Comp_f
            //부하공급방식 supp.add Supp_f
            //증발기항목 Eva.add Eva_f
            //총설비개수... Number_f
            //설치 ---Install_f

            Power_f = 0;
            EER_f = 0;
            Pctrl_f = 0;
            Number_f = 0;
            Number.Clear();
            Power.Clear();
            EER.Clear();
            //추가사항

            CWin.Clear();
            CWout.Clear();


            for (int k = 0; k < AirCooler_dataGridView.Rows.Count; k++)
            {
                List<string> Value = new List<string>();

                for (int i = 0; i < 6; i++) //설치대수까지임
                {
                    if (AirCooler_dataGridView.Rows[k].Cells[i + 1].Value != null)
                    {
                        Value.Add(Convert.ToString(AirCooler_dataGridView.Rows[k].Cells[i + 1].Value));
                    }
                    else
                    {
                        MessageBox.Show("선택항목을 완료해주세요.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                for (int i = 6; i < 17; i++)
                {
                    if (AirCooler_dataGridView.Rows[k].Cells[i + 1].Value != null)
                    {
                        Value.Add(Convert.ToString(AirCooler_dataGridView.Rows[k].Cells[i + 1].Value));
                    }
                    else { Value.Add(null); }
                }

                Number.Add(Convert.ToDouble(Value[5]));
                Power.Add(Convert.ToDouble(Value[6]));
                EER.Add(Convert.ToDouble(Value[8]));
                Pctrl.Add(Convert.ToDouble(Value[9]));
                CGS.Add(Value[0]);
                if (Value[14] == "" || Value[14] == null)
                {

                }
                else
                {
                    CWin.Add(Convert.ToDouble(Value[14]));
                    CWout.Add(Convert.ToDouble(Value[15]));
                }

                //8개 항목
                Program.DB.setValue(DB.type.ProjDB, "CoolingSystemList", "번호,명칭,장비번호,장비명칭,DB유형,제어유형,외기냉방유무,설치대수,냉방출력,냉방전력,EER,대기전력,연료,압축기,부하측형식,증발기,냉수입구온도,냉수출구온도,설치",
                 "'" + Num + "','" + Name_f + "','" + Value[0] + "','" + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "'," +
                 "'" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','" + Value[10] + "','" + Value[11] + "','" + Value[12] + "'," +
                 "'" + Value[13] + "', '" + Value[14] + "','" + Value[15] + "', '" + Value[16] + "'", "번호, 장비번호");
            }

            int a = Power.IndexOf(Power.Max());
            string[][] Max_info = Program.DB.getValue(DB.type.ProjDB, "CoolingSystemList", "제어유형,외기냉방유무,연료,냉수입구온도,냉수출구온도,부하측형식", "번호 = '" + Num + "' And 장비번호 = '" + CGS[a] + "'");

            if (Max_info.Length > 0)
            {
                Control_f = Convert.ToString(Max_info[0][0]);
                Econo_f = Convert.ToString(Max_info[0][1]);
                Fuel_f = Convert.ToString(Max_info[0][2]);
                if (Convert.ToString(Max_info[0][5]) == "수방식")
                {
                    CWin_f = Convert.ToDouble(Max_info[0][3]);
                    CWout_f = Convert.ToDouble(Max_info[0][4]);
                }
            }

          
            for (int h = 0; h < Power.Count; h++)
            {
                EER_f += Power[h] * EER[h] * Number[h]; //파워가중 평균값을 적용해야함
                Power_f += Power[h] * Number[h];
                Pctrl_f += Pctrl[h] * Number[h];
                Number_f += Number[h];
            }
            EER_f = EER_f / Power_f;

            PowerTotal_textBox.Text = string.Format("{0:0.0}", Power_f);
            EERTotal_textBox.Text = string.Format("{0:0.0}", EER_f);
            InstallTotal_textBox.Text = string.Format("{0:0.0}", Number_f);
            G_label.Visible = true;
            G_label.Text = string.Format("설치대수: {0}", Number_f);
            ZoneS_label.Visible = true;

            MessageBox.Show("저장되었습니다.");
        }

        #endregion
        #region C. WaterCooler 작성
        private void Load_WaterCooler()
        {
            Cooling_WaterCooler WaterCooler_Load = new Cooling_WaterCooler();
            DialogResult result = WaterCooler_Load.ShowDialog();

            if (result == DialogResult.OK)
            {
                WaterCoolerLoad_table();

                if (WaterCooler_Load.SelectWaterCooler != null)
                {
                    List<string> check = new List<string>();
                    int A = 0, B = 0, C = 0;
                    Power_f = 0;
                    EER_f = 0;
                    Number_f = 0;
                    Power.Clear();
                    EER.Clear();
                    Comp.Clear();

                    WaterCooler_dataGridView.Rows.Clear();

                    foreach (string SAC in WaterCooler_Load.SelectWaterCooler)
                    {
                        string[][] DefaultDB_Value = Program.DB.getValue(DB.type.ProjDB, "User_WaterCooler", "번호,명칭,DB유형,냉방출력,냉방소비전력,EER,압축기,연료,대기전력,설치,증발기,냉수입구온도,냉수출구온도",
                              "번호='" + SAC + "'");
                        if (DefaultDB_Value.Length > 0)
                        {
                            for (int i = 0; i < DefaultDB_Value.Length; i++)
                            {
                                check.Add(DefaultDB_Value[i][9]);
                                WaterCooler_dataGridView.Rows.Add();
                                int nRow = WaterCooler_dataGridView.Rows.Count - 1;

                                WaterCooler_dataGridView.Rows[nRow].Cells[1].Value = DefaultDB_Value[i][0];
                                WaterCooler_dataGridView.Rows[nRow].Cells[2].Value = DefaultDB_Value[i][1];
                                WaterCooler_dataGridView.Rows[nRow].Cells[3].Value = DefaultDB_Value[i][2];
                                WaterCooler_dataGridView.Rows[nRow].Cells[7].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][3]));
                                WaterCooler_dataGridView.Rows[nRow].Cells[8].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][4]));
                                WaterCooler_dataGridView.Rows[nRow].Cells[9].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][5]));
                                WaterCooler_dataGridView.Rows[nRow].Cells[10].Value = DefaultDB_Value[i][6];
                                WaterCooler_dataGridView.Rows[nRow].Cells[11].Value = DefaultDB_Value[i][7];

                                Power.Add(Convert.ToDouble(DefaultDB_Value[i][3]));
                                EER.Add(Convert.ToDouble(DefaultDB_Value[i][5]));
                                Comp.Add(DefaultDB_Value[i][6]);
                                Number_f = 1 + Number_f;
                            }
                        }
                    }
                    for (int h = 0; h < Number_f; h++)
                    {
                        if (Power.Max() == Power[h])
                        {
                            Comp_f = Comp[h];
                        }

                        EER_f += Power[h] * EER[h];
                        Power_f += Power[h];
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
                    EER_f = EER_f / Power_f;

                    PowerTotal_textBox.Text = string.Format("{0:0.0}", Power_f);
                    EERTotal_textBox.Text = string.Format("{0:0.0}", EER_f);
                    InstallTotal_textBox.Text = string.Format("{0:0.0}", Number_f);
                    G_label.Visible = true;
                    G_label.Text = string.Format("설치대수: {0}", Number_f);
                    ZoneS_label.Visible = true;
                    Install_f = installmake(A, B, C);
                }
                CoolingGeneratorImageSelect(CG, Install_f);
                LoadPressImage(Comp_f, Install_f);
            }
        }

        private void WaterCoolerLoad_table()
        {
            string[][] items = Program.DB.getValue_SameCheck(DB.type.BaseDB_Cooling, "CoolSystem", " 제어유형,압축기유형",
                               "냉동기유형='수냉식냉동기'");
            new StackedHeaderDecorator(WaterCooler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

            WaterCooler_dataGridView.Columns.Clear();

            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            WaterCooler_dataGridView.Columns.Add(checkBoxColumn);

            WaterCooler_dataGridView.Columns.Add("A1", "번호");
            WaterCooler_dataGridView.Columns.Add("A2", "명칭");
            WaterCooler_dataGridView.Columns.Add("A3", "DB유형");

            DataGridViewComboBoxColumn ControlcomboBox = new DataGridViewComboBoxColumn();
            ControlcomboBox.Items.Clear();
            ControlcomboBox.HeaderText = "제어유형";
            ControlcomboBox.Name = "control";
            if (items.Length > 0)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    ControlcomboBox.Items.Add(items[i][0]);
                }
            }
            WaterCooler_dataGridView.Columns.Add(ControlcomboBox);

            DataGridViewComboBoxColumn EconomcomboBox = new DataGridViewComboBoxColumn();
            EconomcomboBox.Items.Clear();
            EconomcomboBox.HeaderText = "외기냉방유무";
            EconomcomboBox.Name = "Economizer";
            EconomcomboBox.Items.AddRange(new string[] { "있음", "없음" });
            WaterCooler_dataGridView.Columns.Add(EconomcomboBox);

            WaterCooler_dataGridView.Columns.Add("A6", "설치대수");
            WaterCooler_dataGridView.Columns.Add("A7", "냉방출력[kW]");
            WaterCooler_dataGridView.Columns.Add("A8", "소비전력[kW]");
            WaterCooler_dataGridView.Columns.Add("A9", "냉방성능[EER]");
            WaterCooler_dataGridView.Columns.Add("A10", "대기전력");
            WaterCooler_dataGridView.Columns.Add("A11", "연료");

            WaterCooler_dataGridView.Columns[0].Width = 40;
            WaterCooler_dataGridView.Columns[1].Width = 50;

        }
        private void GWater_Save_button_Click(object sender, EventArgs e)
        {
            for (int k = 0; k < WaterCooler_dataGridView.Rows.Count; k++)
            {
                List<string> Value = new List<string>();
                for (int i = 0; i < 6; i++)
                {
                    if (WaterCooler_dataGridView.Rows[k].Cells[i + 1].Value != null)
                    {
                        Value.Add(WaterCooler_dataGridView.Rows[k].Cells[i + 1].Value.ToString());
                    }
                    else { MessageBox.Show("선택항목을 완료해주세요.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                //8개 항목
                Program.DB.setValue(DB.type.ProjDB, "User_CoolingSystem", "번호,명칭,장비번호,장비명칭,DB유형,제어유형,외기냉방유무,설치대수",
                 "'" + Num + "','" + Name_f + "','" + Value[0] + "','" + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "'", "번호, 장비번호");
            }
            MessageBox.Show("저장되었습니다.");
        }


        #endregion
        #region D. AbsorbCooler 작성
        private void Load_AbsorbCooler()
        {
            Cooling_AirCon AirCon_Load = new Cooling_AirCon();
            DialogResult result = AirCon_Load.ShowDialog();
            AbsorbCoolerLoad_table("실외기12kW");

            if (result == DialogResult.OK)
            {
                if (AirCon_Load.SelectAirCon != null)
                {
                    foreach (string SAC in AirCon_Load.SelectAirCon)
                    {
                        string[][] DefaultDB_Value = Program.DB.getValue(DB.type.ProjDB, "User_AirCon", " 번호,명칭,냉방출력,냉방소비전력,EER,대기전력,연료",
                               "번호='" + SAC + "'");
                        if(DefaultDB_Value.Length > 0) 
                        {
                            for (int i = 0; i < DefaultDB_Value.Length; i++)
                            {
                                AirCon_dataGridView.Rows.Add();
                                int nRow = AirCon_dataGridView.Rows.Count - 1;

                                AirCon_dataGridView.Rows[nRow].Cells[1].Value = DefaultDB_Value[i][0];
                                AirCon_dataGridView.Rows[nRow].Cells[2].Value = DefaultDB_Value[i][1];


                                AirCon_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][2]));
                                AirCon_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][3]));
                                AirCon_dataGridView.Rows[nRow].Cells[7].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][4]));
                                AirCon_dataGridView.Rows[nRow].Cells[8].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][5]));
                                AirCon_dataGridView.Rows[nRow].Cells[9].Value = DefaultDB_Value[i][6];
                            }
                        }
                    }
                }
            }
        }

        private void AbsorbCoolerLoad_table(string CoolingSystem)
        {
            string[][] items = Program.DB.getValue_SameCheck(DB.type.BaseDB_Cooling, "CoolSystem", " 제어유형,압축기유형,멀티공급유형",
                               "냉동기유형='" + CoolingSystem + "'");


            new StackedHeaderDecorator(AirCon_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            AirCon_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            AirCon_dataGridView.Columns.Add(checkBoxColumn);

            AirCon_dataGridView.Columns.Add("A1", "번호");
            AirCon_dataGridView.Columns.Add("A2", "명칭");

            DataGridViewComboBoxColumn ControlcomboBox = new DataGridViewComboBoxColumn();
            ControlcomboBox.HeaderText = "제어유형";
            ControlcomboBox.Name = "control";
            if (items.Length > 0)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    ControlcomboBox.Items.Add(items[i][0]);
                }
            }
            AirCon_dataGridView.Columns.Add(ControlcomboBox);

            DataGridViewComboBoxColumn EconomcomboBox = new DataGridViewComboBoxColumn();
            EconomcomboBox.HeaderText = "외기냉방유무";
            EconomcomboBox.Name = "Economizer";
            EconomcomboBox.Items.AddRange(new string[] { "있음", "없음" });
            AirCon_dataGridView.Columns.Add(EconomcomboBox);


            AirCon_dataGridView.Columns.Add("A5", "냉방출력[kW]");
            AirCon_dataGridView.Columns.Add("A6", "소비전력[kW]");
            AirCon_dataGridView.Columns.Add("A7", "냉방성능[EER]");
            AirCon_dataGridView.Columns.Add("A8", "대기전력");
            AirCon_dataGridView.Columns.Add("A9", "연료");
        }


        #endregion
        #region F. SoilCooler 작성
        private void Load_SoilCooler()
        {
            Cooling_AirCon AirCon_Load = new Cooling_AirCon();
            DialogResult result = AirCon_Load.ShowDialog();
            SoilCoolerLoad_table("실외기12kW");

            if (result == DialogResult.OK)
            {
                if (AirCon_Load.SelectAirCon != null)
                {
                    foreach (string SAC in AirCon_Load.SelectAirCon)
                    {
                        string[][] DefaultDB_Value = Program.DB.getValue(DB.type.ProjDB, "User_AirCon", " 번호,명칭,냉방출력,냉방소비전력,EER,대기전력,연료",
                               "번호='" + SAC + "'");
                        if (DefaultDB_Value.Length > 0)
                        {
                            for (int i = 0; i < DefaultDB_Value.Length; i++)
                            {
                                AirCon_dataGridView.Rows.Add();
                                int nRow = AirCon_dataGridView.Rows.Count - 1;

                                AirCon_dataGridView.Rows[nRow].Cells[1].Value = DefaultDB_Value[i][0];
                                AirCon_dataGridView.Rows[nRow].Cells[2].Value = DefaultDB_Value[i][1];


                                AirCon_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][2]));
                                AirCon_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][3]));
                                AirCon_dataGridView.Rows[nRow].Cells[7].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][4]));
                                AirCon_dataGridView.Rows[nRow].Cells[8].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][5]));
                                AirCon_dataGridView.Rows[nRow].Cells[9].Value = DefaultDB_Value[i][6];

                            }
                        }
                    }
                }
            }
        }

        private void SoilCoolerLoad_table(string CoolingSystem)
        {
            string[][] items = Program.DB.getValue_SameCheck(DB.type.BaseDB_Cooling, "CoolSystem", " 제어유형,압축기유형,멀티공급유형",
                               "냉동기유형='" + CoolingSystem + "'");


            new StackedHeaderDecorator(AirCon_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            AirCon_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            AirCon_dataGridView.Columns.Add(checkBoxColumn);

            AirCon_dataGridView.Columns.Add("A1", "번호");
            AirCon_dataGridView.Columns.Add("A2", "명칭");

            DataGridViewComboBoxColumn ControlcomboBox = new DataGridViewComboBoxColumn();
            ControlcomboBox.HeaderText = "제어유형";
            ControlcomboBox.Name = "control";
            if (items.Length > 0)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    ControlcomboBox.Items.Add(items[i][0]);
                }
            }
            AirCon_dataGridView.Columns.Add(ControlcomboBox);

            DataGridViewComboBoxColumn EconomcomboBox = new DataGridViewComboBoxColumn();
            EconomcomboBox.HeaderText = "외기냉방유무";
            EconomcomboBox.Name = "Economizer";
            EconomcomboBox.Items.AddRange(new string[] { "있음", "없음" });
            AirCon_dataGridView.Columns.Add(EconomcomboBox);


            AirCon_dataGridView.Columns.Add("A5", "냉방출력[kW]");
            AirCon_dataGridView.Columns.Add("A6", "소비전력[kW]");
            AirCon_dataGridView.Columns.Add("A7", "냉방성능[EER]");
            AirCon_dataGridView.Columns.Add("A8", "대기전력");
            AirCon_dataGridView.Columns.Add("A9", "연료");
        }
        #endregion
        #region G. CoolerTop 작성
        private void Load_CoolerTop()
        {
            Cooling_AirCon AirCon_Load = new Cooling_AirCon();
            DialogResult result = AirCon_Load.ShowDialog();
            CoolerTopLoad_table("실외기12kW");

            if (result == DialogResult.OK)
            {
                if (AirCon_Load.SelectAirCon != null)
                {
                    foreach (string SAC in AirCon_Load.SelectAirCon)
                    {
                        string[][] DefaultDB_Value = Program.DB.getValue(DB.type.ProjDB, "User_AirCon", " 번호,명칭,냉방출력,냉방소비전력,EER,대기전력,연료",
                               "번호='" + SAC + "'");
                        if(DefaultDB_Value.Length > 0)
                        {
                            for (int i = 0; i < DefaultDB_Value.Length; i++)
                            {
                                AirCon_dataGridView.Rows.Add();
                                int nRow = AirCon_dataGridView.Rows.Count - 1;

                                AirCon_dataGridView.Rows[nRow].Cells[1].Value = DefaultDB_Value[i][0];
                                AirCon_dataGridView.Rows[nRow].Cells[2].Value = DefaultDB_Value[i][1];


                                AirCon_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][2]));
                                AirCon_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][3]));
                                AirCon_dataGridView.Rows[nRow].Cells[7].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][4]));
                                AirCon_dataGridView.Rows[nRow].Cells[8].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][5]));
                                AirCon_dataGridView.Rows[nRow].Cells[9].Value = DefaultDB_Value[i][6];
                            }
                        }
                    }
                }
            }
        }

        private void CoolerTopLoad_table(string CoolingSystem)
        {
            string[][] items = Program.DB.getValue_SameCheck(DB.type.BaseDB_Cooling, "CoolSystem", " 제어유형,압축기유형,멀티공급유형",
                               "냉동기유형='" + CoolingSystem + "'");


            new StackedHeaderDecorator(AirCon_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            AirCon_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            AirCon_dataGridView.Columns.Add(checkBoxColumn);

            AirCon_dataGridView.Columns.Add("A1", "번호");
            AirCon_dataGridView.Columns.Add("A2", "명칭");

            DataGridViewComboBoxColumn ControlcomboBox = new DataGridViewComboBoxColumn();
            ControlcomboBox.HeaderText = "제어유형";
            ControlcomboBox.Name = "control";
            if (items.Length > 0)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    ControlcomboBox.Items.Add(items[i][0]);
                }
            }
            AirCon_dataGridView.Columns.Add(ControlcomboBox);

            DataGridViewComboBoxColumn EconomcomboBox = new DataGridViewComboBoxColumn();
            EconomcomboBox.HeaderText = "외기냉방유무";
            EconomcomboBox.Name = "Economizer";
            EconomcomboBox.Items.AddRange(new string[] { "있음", "없음" });
            AirCon_dataGridView.Columns.Add(EconomcomboBox);


            AirCon_dataGridView.Columns.Add("A5", "냉방출력[kW]");
            AirCon_dataGridView.Columns.Add("A6", "소비전력[kW]");
            AirCon_dataGridView.Columns.Add("A7", "냉방성능[EER]");
            AirCon_dataGridView.Columns.Add("A8", "대기전력");
            AirCon_dataGridView.Columns.Add("A9", "연료");
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
        #region H.펌프
        private void PumpUse_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PumpUse_comboBox.SelectedItem != null)
            {
                PumpUse = PumpUse_comboBox.SelectedItem.ToString();

                if (PumpUse == "펌프 있음")
                {
                    pump_image();
                    PumpMethod_label.Visible = true;
                    PumpMethod_comboBox.Visible = true;
                    Create_Pump_Table();
                }
                else
                {
                    Pump_pictureBox.Visible = false;
                    PumpMethod_label.Visible = false;
                    PumpMethod_comboBox.Visible = false;
                    PumpMethod_comboBox.SelectedItem = null;
                    Pump_dataGridView.Columns.Clear();
                    ChangeVisble_Pump(null);
                }
            }
            else
            {
                PumpUse = null;
            }

        }
        private void PumpMethod_comboBox_SelectedIndexChanged(object sender, EventArgs e)
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
        private void ChangeVisble_Pump(String PumpMethod)
        {
            if (PumpMethod == "1차펌프")
            {
                Pump2 = null;
                Pump1_label.Visible = true;
                Pump1_textBox.Visible = true;
                Pump1_button.Visible = true;
                Pump2_label.Visible = false;
                Pump2_textBox.Visible = false;
                Pump2_button.Visible = false;
                Pump2 = null;
                Pump2_textBox.Text = null;
                if (Pump1 != null)
                {
                    if (Pump_dataGridView.Rows.Count == 0)
                    {
                        Pump_dataGridView.Rows.Add();
                        Load_Pump_Table(0, Pump1);
                    }
                    if (Pump_dataGridView.Rows.Count == 1)
                    { Load_Pump_Table(0, Pump1); }
                }
                if (Pump_dataGridView.Rows.Count == 2)
                { Pump_dataGridView.Rows.Remove(Pump_dataGridView.Rows[1]); }
            }
            else if (PumpMethod == "1차폐회로+2차펌프")
            {
                Pump1_label.Visible = true;
                Pump1_textBox.Visible = true;
                Pump1_button.Visible = true;
                Pump2_label.Visible = true;
                Pump2_textBox.Visible = true;
                Pump2_button.Visible = true;
                if (Pump1 != null && Pump2 != null)
                {
                    if (Pump_dataGridView.Rows.Count == 0)
                    {
                        Pump_dataGridView.Rows.Add();
                        Pump_dataGridView.Rows.Add();
                        Load_Pump_Table(0, Pump1);
                        Load_Pump_Table(1, Pump2);
                    }
                    else if (Pump_dataGridView.Rows.Count == 1)
                    {
                        Pump_dataGridView.Rows.Add();
                        Load_Pump_Table(0, Pump1);
                        Load_Pump_Table(1, Pump2);
                    }
                    else if (Pump_dataGridView.Rows.Count == 2)
                    {
                        Load_Pump_Table(0, Pump1);
                        Load_Pump_Table(1, Pump2);
                    }
                }
            }
            else
            {
                Pump1 = null;
                Pump2 = null;
                Pump1_label.Visible = false;
                Pump1_textBox.Visible = false;
                Pump1_button.Visible = false;
                Pump2_label.Visible = false;
                Pump2_textBox.Visible = false;
                Pump2_button.Visible = false;
                Pump2 = null;
                Pump1 = null;
                Pump2_textBox.Text = null;
                Pump1_textBox.Text = null;
                Pump_dataGridView.Rows.Clear();
            }
        }
        private void Pump1_button_Click(object sender, EventArgs e)
        {
            pump_image();
            if (Pump_dataGridView.Rows.Count == 0)
            {
                Pump_dataGridView.Rows.Add();
            }
            Heating_Pump heating_pump = new Heating_Pump(Pump1);
            DialogResult result = heating_pump.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (heating_pump.SelectPump != null)
                {
                    Pump1 = heating_pump.SelectPump;
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "명칭", "번호 = '" + Pump1.ToString() + "'");
                    if (Value.Length > 0)
                    {
                        Pump1_textBox.Text = Value[0][0];
                        if (Pump_dataGridView.Rows.Count == 1)
                        { Load_Pump_Table(0, Pump1); }
                    }
                }
            }
        }

        private void Pump2_button_Click(object sender, EventArgs e)
        {
            if (Pump_dataGridView.Rows.Count == 1)
            {
                Pump_dataGridView.Rows.Add();
            }
            Heating_Pump heating_pump = new Heating_Pump(Pump2);
            DialogResult result = heating_pump.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (heating_pump.SelectPump != null)
                {
                    Pump2 = heating_pump.SelectPump;
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "명칭", "번호 = '" + Pump2.ToString() + "'");
                    if (Value.Length > 0)
                    {
                        Pump2_textBox.Text = Value[0][0];
                        if (Pump_dataGridView.Rows.Count == 2)
                        { Load_Pump_Table(1, Pump2); }
                    }
                }
            }

        }

        private void Create_Pump_Table()
        {
            Pump_dataGridView.Columns.Clear();
            new StackedHeaderDecorator(Pump_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            Pump_dataGridView.Columns.Add("A0", "구분");
            Pump_dataGridView.Columns.Add("A1", "펌프번호");
            Pump_dataGridView.Columns.Add("A2", "명칭");
            Pump_dataGridView.Columns.Add("A3", "종류");
            Pump_dataGridView.Columns.Add("A4", "A효율.[%]");
            Pump_dataGridView.Columns.Add("A5", "B효율.[%]");
            Pump_dataGridView.Columns.Add("A6", "유량.[CMH]");
            Pump_dataGridView.Columns.Add("A7", "동력.[W]");
            Pump_dataGridView.Columns.Add("A8", "양정.[m]");
            Pump_dataGridView.Columns.Add("A9", "정유량 밸브");
            Pump_dataGridView.Columns.Add("A10", "펌프 제어");
            Pump_dataGridView.Columns.Add("A11", "대수.[EA]");
            Pump_dataGridView.Columns[0].Width = 50;
            Pump_dataGridView.Columns[1].Width = 50;

        }
        private void Load_Pump_Table(int nRow, String PumpNum)
        {
            DataGridViewComboBoxCell 정유량밸브comboBox = new DataGridViewComboBoxCell();
            정유량밸브comboBox.Items.Add("있음");
            정유량밸브comboBox.Items.Add("없음");
            Pump_dataGridView.Rows[nRow].Cells[9] = 정유량밸브comboBox;

            DataGridViewComboBoxCell 제어comboBox = new DataGridViewComboBoxCell();
            제어comboBox.Items.Add("대수제어");
            제어comboBox.Items.Add("인버터제어");
            제어comboBox.Items.Add("제어없음");
            Pump_dataGridView.Rows[nRow].Cells[10] = 제어comboBox;

            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "번호,명칭,종류,A효율,B효율,유량,동력,양정", "번호 = '" + PumpNum.ToString() + "'");
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    string A효율 = "", B효율 = "", 유량 = "", 동력 = "", 양정 = "";
                    if (Value[n][3] != null && Value[n][3] != "")
                    {
                        A효율 = string.Format("{0:F1}", Convert.ToDouble(Value[n][3]));
                    }
                    if (Value[n][4] != null && Value[n][4] != "")
                    {
                        B효율 = string.Format("{0:F1}", Convert.ToDouble(Value[n][4]));
                    }
                    if (Value[n][5] != null && Value[n][5] != "")
                    {
                        유량 = string.Format("{0:F0}", Convert.ToDouble(Value[n][5]));
                    }
                    if (Value[n][6] != null && Value[n][6] != "")
                    {
                        동력 = string.Format("{0:F0}", Convert.ToDouble(Value[n][6]));
                    }
                    if (Value[n][7] != null && Value[n][7] != "")
                    {
                        양정 = string.Format("{0:F0}", Convert.ToDouble(Value[n][7]));
                    }

                    if (nRow == 1)
                    {
                        Pump_dataGridView.Rows[nRow].Cells[0].Value = "2차펌프";
                    }
                    else { Pump_dataGridView.Rows[nRow].Cells[0].Value = "1차펌프"; }
                    Pump_dataGridView.Rows[nRow].Cells[1].Value = Value[0][0];
                    Pump_dataGridView.Rows[nRow].Cells[2].Value = Value[0][1];
                    Pump_dataGridView.Rows[nRow].Cells[3].Value = Value[0][2];
                    Pump_dataGridView.Rows[nRow].Cells[4].Value = A효율;
                    Pump_dataGridView.Rows[nRow].Cells[5].Value = B효율;
                    Pump_dataGridView.Rows[nRow].Cells[6].Value = 유량;
                    Pump_dataGridView.Rows[nRow].Cells[7].Value = 동력;
                    Pump_dataGridView.Rows[nRow].Cells[8].Value = 양정;
                }
            }
        }

        private void Save_Pump()
        {
            if (Pump_dataGridView.Rows.Count == 0) { return; }
            for (int k = 0; k < Pump_dataGridView.Rows.Count; k++)
            {
                if (k == 0)
                {
                    if (Pump_dataGridView.Rows[0].Cells[9].Value != null)
                    { Pump1Valve = Pump_dataGridView.Rows[0].Cells[9].Value.ToString(); }
                    else { MessageBox.Show("펌프 밸브를 선택하세요."); }
                    if (Pump_dataGridView.Rows[0].Cells[10].Value != null)
                    { Pump1Control = Pump_dataGridView.Rows[0].Cells[10].Value.ToString(); }
                    else { MessageBox.Show("펌프 제어를 선택하세요."); }
                    if (Pump_dataGridView.Rows[0].Cells[11].Value != null)
                    { Pump1Num = Convert.ToInt16(Pump_dataGridView.Rows[0].Cells[11].Value); }
                    else { MessageBox.Show("펌프 제어를 선택하세요."); }
                }
                else if (k == 1)
                {
                    if (Pump_dataGridView.Rows[1].Cells[9].Value != null)
                    { Pump2Valve = Pump_dataGridView.Rows[1].Cells[9].Value.ToString(); }
                    else { MessageBox.Show("펌프 밸브를 선택하세요."); }
                    if (Pump_dataGridView.Rows[1].Cells[10].Value != null)
                    { Pump2Control = Pump_dataGridView.Rows[1].Cells[10].Value.ToString(); }
                    else { MessageBox.Show("펌프 제어를 선택하세요."); }
                    if (Pump_dataGridView.Rows[1].Cells[11].Value != null)
                    { Pump2Num = Convert.ToInt16(Pump_dataGridView.Rows[1].Cells[11].Value); }
                    else { MessageBox.Show("펌프 제어를 선택하세요."); }
                }
            }
        }


        #endregion

        #region I.공급설비

        private void ce1Type_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ce1Type_comboBox.SelectedItem != null)
            {
                ce1Type = ce1Type_comboBox.SelectedItem.ToString();
                imagemake(ce1Type);

            }
            else
            {
                ce1Type = null;
            }
        }



        private void ce2Type_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ce2Type_comboBox.SelectedItem != null)
            {
                ce2Type = ce2Type_comboBox.SelectedItem.ToString();
                imagemake(ce2Type);

                if (ce1Type == ce2Type)
                {
                    MessageBox.Show("공급설비1과 다른 종류의 공급설비를 선택하세요.");
                    ce2Type_comboBox.SelectedItem = null;
                }
            }
            else
            {
                ce2Type = null;
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

            ce_dataGridView.Columns.Add("A6", "존명칭");


            ce_dataGridView.Columns[0].Width = 30;
            ce_dataGridView.Columns[1].Width = 150;
            ce_dataGridView.Columns[2].Width = 120;
            ce_dataGridView.Columns[3].Width = 130;

        }
        private void ce1Zone_button_Click(object sender, EventArgs e)
        {
            if (ce_dataGridView.Columns.Count == 0)
            {
                Create_ce_Table();
            }
            Cooling_ceZone ceZone = new Cooling_ceZone(Num, ZoneNameList, ce1Type);
            DialogResult result = ceZone.ShowDialog();
            if (result == DialogResult.OK)
            {
                Load_ce(ce1Type);
                Load_ce1Zone(ce1Type);
            }

        }

        private void ce2Zone_button_Click(object sender, EventArgs e)
        {
            Cooling_ceZone ceZone = new Cooling_ceZone(Num, ZoneNameList, ce2Type);
            DialogResult result = ceZone.ShowDialog();
            if (result == DialogResult.OK)
            {
                Load_ce(ce2Type);
                Load_ce2Zone(ce2Type);
            }
        }

        private void Load_ce1Zone(String ce1Type)
        {
            String[][] Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Cooling_ce_Form", "존번호", "냉방시스템 = '" + Num + "' And 공급설비종류 = '" + ce1Type + "'");
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

        private void Load_ce2Zone(String ce2Type)
        {
            String[][] Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Heating_ce_Form", "존번호", "냉방시스템 = '" + Num + "' And 공급설비종류 = '" + ce2Type + "'");
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

        private void Load_ce(string CE)
        {
            String[][] Value = Program.DB.getValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호,공급설비종류,공급설비", "냉방시스템 = '" + Num + "' And 공급설비종류 = '" + CE + "'");
            if (Value.Length > 0)
            {
                int Sum = 1;
                for (int n = 0; n < Value.Length; n++)
                {
                    int nRow = ce_dataGridView.Rows.Add();

                    ce_dataGridView.Rows[nRow].Cells[2].Value = Value[n][1];//종류
                    int index = Value[n][2].IndexOf("_");
                    String substring = Value[n][2].Substring(0, index);
                    string[][] 일람표정보 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "번호,명칭,용량_냉방,소비전력_냉방", "번호 = '" + substring + "'");
                    ce_dataGridView.Rows[nRow].Cells[3].Value = 일람표정보[0][1]; //일람표명칭
                    ce_dataGridView.Rows[nRow].Cells[4].Value = 일람표정보[0][2]; //용량
                    ce_dataGridView.Rows[nRow].Cells[5].Value = 일람표정보[0][3];//소비전력
                    string[][] 존정보 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,존이름", "존번호 = '" + Value[n][0] + "'");
                    ce_dataGridView.Rows[nRow].Cells[6].Value = 존정보[0][1];//존이름
                    ce_dataGridView.Rows[nRow].Cells[1].Value = 존정보[0][0] + "_" + Value[n][2];
                }
            }
        }

        private Boolean ce_datagridviewDesign(DataGridViewCell cell, int column, int row)
        {
            if (ce_dataGridView.Rows[row].Cells[2].Value != null && ce_dataGridView.Rows[row].Cells[2].Value.ToString() == "복사난방")
            {
                if (column == 4 || column == 5)
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.SelectionForeColor = Color.Black;
                    return true;
                }
                else { return false; }
            }
            else return false;
        }


        private void Save_ce()
        {
            Program.DB.deleteValue(DB.type.ProjDB, "Cooling_ce_Form", "냉방시스템 = '" + Num + "'");

            for (int n = 0; n < ce_dataGridView.Rows.Count; n++)
            {
                String 존번호, 공급설비;
                int index = ce_dataGridView.Rows[n].Cells[1].Value.ToString().IndexOf("CE");
                존번호 = ce_dataGridView.Rows[n].Cells[1].Value.ToString().Substring(0, index - 1);
                공급설비 = ce_dataGridView.Rows[n].Cells[1].Value.ToString().Substring(index, ce_dataGridView.Rows[n].Cells[1].Value.ToString().Length - index);
                Program.DB.setValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호,냉방시스템,공급설비종류,공급설비", "'" + 존번호 + "','" + Num + "','" + ce_dataGridView.Rows[n].Cells[2].Value + "','" + 공급설비 + "'", "");
            }
        }

        #endregion 

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CoolerTop_button_Click(object sender, EventArgs e)
        {

        }

        private void ce1_pictureBox_Click(object sender, EventArgs e)
        {

        }

        private void ce2_pictureBox_Click(object sender, EventArgs e)
        {

        }

        private void ce3_pictureBox_Click(object sender, EventArgs e)
        {

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
                Save();
            }

        }
        private void Save()
        {
            //공조기 저장 -- AHU에서 해결함
            //냉방존리스트 저장 -- coolingzone에서 해결함, 연간에너지요구량,최대출력 
            //실외기리스트 저장 -- UserCooling 에서 해결함
            //저장설비 저장
            //분배설비 저장
            //공급설비 저장

            Save_Pump();
            Save_ce();
            Save_CG();

            this.DialogResult = DialogResult.OK;
            this.Hide();
            Program.getMenuForm().DoLoadForm(38, OnLoadListProc);
        }

        public void Save_CG()
        {

            if (Math.Round(Convert.ToDouble(PowerTotal_textBox.Text)) != Math.Round(Power_f))
            {
                MessageBox.Show("생산설비의 SAVE 버튼을 클릭하세요.");
                return;
            }else if(Control_f == null||Control_f=="")
            {
                MessageBox.Show("생산설비의 SAVE 버튼을 클릭하세요.");
                return;
            }
            else
            {
                switch (CG)
                {
                    case "실외기12kW":
                        //냉방설비, 저장설비
                        Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Form", "번호,프로젝트유형,명칭,설치,공급존,공급AHU,냉방설비,열원설비,냉방출력,냉방성능,제어유형,연료,외기냉방시스템,대기전력,설치대수,저장탱크,저장유형,부하측열원공급설비",
                            "'" + Num + "','" + 프로젝트유형[0][0] + "','" + Name_f + "','" + Install_f + "','" + SelectedZone + "','" + SelectedAHU + "', '" + CG + "','" + CSource +
                            "', '" + Power_f + "', '" + EER_f + "', '" + Control_f + "','" + Fuel_f + "','" + Econo_f + "','" + Pctrl_f + "','" + Number_f + "','" + Stotype + "','" + StoSource + "','"+ Supp_f +"'", "번호");

                        //분배설비
                        Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Form", "번호,펌프유무,펌프방식,펌프1종류,펌프2종류,펌프1밸브,펌프2밸브,펌프1제어,펌프2제어,펌프1대수,펌프2대수",
                            "'" + Num + "','" + PumpUse + "','" + PumpMethod + "','" + Pump1 + "','" + Pump2 + "','" + Pump1Valve + "','" + Pump2Valve + "','" + Pump1Control + "','" + Pump2Control + "','" + Pump1Num.ToString() + "','" + Pump2Num.ToString() + "'", "번호");
                        //공급설비
                        Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Form", "번호,공급설비1종류,공급설비2종류", "'" + Num + "','" + ce1Type + "','" + ce2Type + "'", "번호");
                        

                        //냉방존,공조기존
                        Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Form", "번호,ZoneNumber_f,QC_a_z,QC_max_z,A_z,AhuNumber_f,QC_a_Ahu,QC_max_ahu,A_ahu",
                            "'" + Num + "','" + ZoneNumber_f + "','" + QC_a_z + "','" + QC_max_z + "','" + A_z + "','" + AhuNumber_f + "','" + QC_a_Ahu + "','" + QC_max_Ahu + "','" + A_Ahu + "'", "번호");
                        break;

                    case "공냉식냉동기":
                        //냉방설비, 저장설비
                        Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Form", "번호,프로젝트유형,명칭,설치,공급존,공급AHU,냉방설비,열원설비,냉방출력,냉방성능,제어유형,연료,외기냉방시스템,대기전력,설치대수,저장탱크,저장유형,증발기",
                            "'" + Num + "','" + 프로젝트유형[0][0] + "','" + Name_f + "','" + Install_f + "','" + SelectedZone + "','" + SelectedAHU + "', '" + CG + "','" + CSource +
                            "', '" + Power_f + "', '" + EER_f + "', '" + Control_f + "','" + Fuel_f + "','" + Econo_f + "','" + Pctrl_f + "','" + Number_f + "','" + Stotype + "','" + StoSource + "', '" + Eva_f + "'", "번호");

                        //압축기종류,냉수출구온도,냉수입구온도,부하측열원공급설비
                        Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Form", "번호,부하측열원공급설비,냉수출구온도,냉수입구온도,압축기종류",
                           "'" + Num + "','" + Supp_f + "','" + CWout_f + "','" + CWin_f + "','" + Comp_f + "'", "번호");

                        //분배설비
                        Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Form", "번호,펌프유무,펌프방식,펌프1종류,펌프2종류,펌프1밸브,펌프2밸브,펌프1제어,펌프2제어,펌프1대수,펌프2대수",
                             "'" + Num + "','" + PumpUse + "','" + PumpMethod + "','" + Pump1 + "','" + Pump2 + "','" + Pump1Valve + "','" + Pump2Valve + "','" + Pump1Control + "','" + Pump2Control + "','" + Pump1Num.ToString() + "','" + Pump2Num.ToString() + "'", "번호");
                        //공급설비
                        Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Form", "번호,공급설비1종류,공급설비2종류", "'" + Num + "','" + ce1Type + "','" + ce2Type + "'", "번호");
                        
                        //냉방존,공조기존
                        Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Form", "번호,ZoneNumber_f,QC_a_z,QC_max_z,A_z,AhuNumber_f,QC_a_Ahu,QC_max_ahu,A_ahu",
                            "'" + Num + "','" + ZoneNumber_f + "','" + QC_a_z + "','" + QC_max_z + "','" + A_z + "','" + AhuNumber_f + "','" + QC_a_Ahu + "','" + QC_max_Ahu + "','" + A_Ahu + "'", "번호");
                        break;

                    default:
                        break;
                }

            }
        }
        public static bool OnLoadListProc(Form form)
        {
            List_CoolingSystem f = (List_CoolingSystem)form;
            f.load_List();
            return true;
        }

        private void reset()
        {
            //존및 공조기관련
            SelectedZone = null; SelectedAHU = null; MultiZone = null; MultiAhu = null; //단일존, 멀티존인지 판별하는것
            CG = null; Num = null; Name_f = null;
            //프로젝트 유형은 못함

            Install_f = null; Control_f = null; CSource = null; Fuel_f = null; Econo_f = null; Supp_f = null; Comp_f = null; Refri = null; Eva_f = null; //증발기 및 설치(기존,신규)가 추가됨
            Power_f = 0; EER_f = 0; Pctrl_f = 0; CWout_f = 0; CWin_f = 0; CSout_f = 0; CSin_f = 0;  //CWout_f 냉수출구온도  CSWout_f 냉각수출구온도
            Number_f = 0;

            //냉각탑
            CTower_f = null; CTControl_f = null; CTPump = null; CTpumpValve = null; CTpumpControl = null;
            CTNum_f = 0; CTpumpNum = 0;


            ZoneNameList.Clear();
            AHUNmaeList.Clear();

            //장비관련
            Power.Clear(); EER.Clear(); Pctrl.Clear(); CWout.Clear(); CWin.Clear();
            CSout.Clear(); CSin.Clear(); //지열냉각수
            Number.Clear(); //설치대수
            Comp.Clear(); Supp.Clear(); Eva.Clear(); //공냉식, 수냉식, 지열히트펌프 유형
            CGS.Clear(); //장비번호 즉 이름임 Name


            //저장설비
            Stotype = null; StoSource = null;


            //펌프정의
            PumpUse = null; PumpMethod = null; Pump1 = null; Pump2 = null; Pump1Valve = null; Pump2Valve = null; Pump1Control = null; Pump2Control = null;
            Pump1Num = 0; Pump2Num = 0;

            //지열펌프
            Soilpump = null; SoilpumpValve = null; SoilpumpControl = null;


            //공급설비정의
            ce1Type = null; ce2Type = null; ce3Type = null;
            string[] ceType = { "공조기", "실내기", "팬코일유닛", "복사냉방(천장)", "복사냉방(벽)", "바닥매립형컨백터" };

        }

        public void LoadData(String ID) // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();
            NumTextBox.Text = ID;
            Num = ID;
            Zonemainwrite(Num);
            string[][] DataValue = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "냉방설비", "번호='" + Num + "'");
            if (DataValue.Length > 0)
            {
                CG = Convert.ToString(DataValue[0][0]);

                switch (CG)
                {
                    case "실외기12kW":
                        AirconList();
                        break;
                    case "공냉식냉동기":
                        AirCoolerList();
                        break;
                    case "수냉식냉동기":
                        Load_WaterCooler(); //작업해야함
                        break;
                    case "흡수식냉동기":
                        Load_AbsorbCooler();
                        break;
                    case "지열히트펌프":
                        Load_SoilCooler();
                        break;
                    default:
                        break;
                }
            }
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "펌프유무,펌프방식,펌프1종류,펌프2종류,펌프1밸브,펌프2밸브,펌프1제어,펌프2제어,펌프1대수,펌프2대수", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {
                PumpUse_comboBox.SelectedItem = Value[0][0];
                PumpUse = Value[0][0];
                if (PumpUse == "펌프 있음")
                {
                    Pump_dataGridView.Visible = true;
                    PumpMethod_comboBox.SelectedItem = Value[0][1];
                    PumpMethod = Value[0][1];

                    Pump1 = Value[0][2];
                    if (Pump1 != null && Pump1 != "")
                    {
                        string[][] Pump_Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "명칭", "번호 = '" + Pump1.ToString() + "'");
                        Pump1_textBox.Text = Pump_Value[0][0];
                        if (Pump_dataGridView.Rows.Count == 0)
                        {
                            Pump_dataGridView.Rows.Add();
                        }
                        Load_Pump_Table(0, Pump1);
                    }

                    Pump2 = Value[0][3];
                    if (Pump2 != null && Pump2 != "")
                    {
                        string[][] Pump_Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "명칭", "번호 = '" + Pump2.ToString() + "'");
                        Pump2_textBox.Text = Pump_Value[0][0];
                        if (Pump_dataGridView.Rows.Count == 1)
                        {
                            Pump_dataGridView.Rows.Add();
                        }
                        Load_Pump_Table(1, Pump2);
                    }

                    Pump1Valve = Value[0][4];
                    Pump2Valve = Value[0][5];
                    Pump1Control = Value[0][6];
                    Pump2Control = Value[0][7];
                    Pump1Num = Convert.ToInt16(Value[0][8]);
                    Pump2Num = Convert.ToInt16(Value[0][9]);

                    if (Pump_dataGridView.Rows.Count > 0)
                    {
                        Pump_dataGridView.Rows[0].Cells[9].Value = Pump1Valve;
                        Pump_dataGridView.Rows[0].Cells[10].Value = Pump1Control;
                        Pump_dataGridView.Rows[0].Cells[11].Value = Pump1Num;
                    } 
                    else if (Pump_dataGridView.Rows.Count > 1)
                    {
                        Pump_dataGridView.Rows[0].Cells[9].Value = Pump1Valve;
                        Pump_dataGridView.Rows[0].Cells[10].Value = Pump1Control;
                        Pump_dataGridView.Rows[0].Cells[11].Value = Pump1Num;
                        Pump_dataGridView.Rows[1].Cells[9].Value = Pump2Valve;
                        Pump_dataGridView.Rows[1].Cells[10].Value = Pump2Control;
                        Pump_dataGridView.Rows[1].Cells[11].Value = Pump2Num;
                    }
                }
                else { Pump_dataGridView.Visible = false; }
            }



            Value = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "공급설비1종류,공급설비2종류", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {
                ce1Type = Value[0][0];
                ce1Type_comboBox.SelectedItem = ce1Type;

                ce2Type = Value[0][1];
                ce2Type_comboBox.SelectedItem = ce2Type;

                if (ce1Type != null && ce1Type != "")
                {
                    Create_ce_Table();
                    Load_ce(ce1Type);
                    Load_ce1Zone(ce1Type);
                }

                if (ce2Type != null && ce2Type != "")
                {
                    Load_ce(ce2Type);
                    Load_ce2Zone(ce2Type);
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
                        AirconList();
                    }

                }
            }
            else
            {
                MessageBox.Show("우선 저장버튼을 누른후 삭제하시기 바랍니다.");
            }
        }


    }

}
