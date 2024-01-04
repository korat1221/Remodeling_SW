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

        //생산설비
        string[][] 프로젝트유형;

        string Num, Name_f, SelectedZone, SelectedAHU, Install_f, Control_f, Fuel_f, Econo_f, CG, CSource, Comp_f, Refri, CG_Max;
        double Power_f, EER_f, Pctrl_f, CWout_f, CWin_f, PLVav, CSWout_f, CSWin_f;
        int Number_f;
        string MultiZone;

        List<string> ZoneNameList = new List<string>();
        List<string> AHUNmaeList = new List<string>();
        List<double> Power = new List<double>(), EER = new List<double>(), Pctrl = new List<double>();
        List<int> Number = new List<int>(); //설치대수
        List<string> Comp = new List<string>(); //공냉식, 수냉식, 지열히트펌프 유형
        List<string> CGS = new List<string>();

        //저장설비
        string Stotype, StoSource;


        //펌프정의
        string SLRL, Complex, MainSystem, Sub1System, Sub2System, PumpUse, PumpMethod, Pump1, Pump2, Pump1Valve, Pump2Valve, Pump1Control, Pump2Control, ce1Type, ce2Type, ce3Type;
        int Pump1Num, Pump2Num;


        //공급설비정의
        string[] ceType = { "공조기", "실내기", "팬코일유닛", "복사냉방(천장)", "복사냉방(벽)", "바닥매립형컨백터" };


        ArrayList SelectAirConditioning = new ArrayList(); ArrayList SelectPump = new ArrayList(); ArrayList Selectce1Zone = new ArrayList(); ArrayList Selectce2Zone = new ArrayList();
        int ce_SelectRow;




        public CoolingSystem()
        {
            InitializeComponent();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '냉방시스템'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
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
            for (int i = 0; i < DefaultDB_Value.Length; i++)
            {
                list[i] = (DefaultDB_Value[i][0]);
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

                coolingzone_connect[0] = Num;
                coolingzone_connect[1] = Name_f;


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
        private void Zonemainwrite(string _num)
        {
            string[][] zonenames = Program.DB.getValue(DB.type.ProjDB, "CoolingZone", "존번호", "번호='" + _num + "'");
            double area = 0;
            double annualenergyneed = 0;
            double maxload = 0;
            //
            for (int i = 0; i < zonenames.Length; i++)
            {
                string[][] ZoneGet0 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Zone_HCneed_Result",
                 "Qb_a,Q_max", "번호= '" + zonenames[i][0] + "' And 비이용일_이용일 = '이용일' And 난방_냉방 = '냉방'");
                annualenergyneed += Convert.ToDouble(ZoneGet0[0][0]);
                maxload += Convert.ToDouble(ZoneGet0[0][1]) / 1000;

                string[][] ZoneGet1 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form",
                "순바닥면적", //값이있는 열
                "존번호='" + zonenames[i][0] + //조건1
                "'"); //마지막
                area += Convert.ToDouble(ZoneGet1[0][0]);
                ZoneNameList.Add(zonenames[i][0].ToString());
            }
            CZ_AnnualCoolingNeed_Textbox.Text = annualenergyneed.ToString("0");
            CZ_FloorArea_Textbox.Text = area.ToString("0.00");
            CZ_MaxCoolingLoad_Textbox.Text = maxload.ToString("0.00");

            if (zonenames.Length > 0)
            {
                int num = zonenames.Length - 1;
                SelectedZone = null;
                SelectedZone = zonenames[0][0].ToString() + " 외 " + num + "개";
                SelectedZoneText.Text = SelectedZone;
                if (zonenames.Length == 1) MultiZone = "존 공급방식:  단일존";
                else MultiZone = "존 공급방식:  멀티존";
                ZoneS_label.Text = MultiZone;
            }
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
                for (int i = 0; i < System.Length; i++)
                {
                    _Systemtype.Add(System[i][0]);
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
            for (int i = 0; i < Intall.Length; i++)
            {
                _Installtype.Add(Intall[i][0]);
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
            DistpictureBox.Size = new System.Drawing.Size(610, 254);
            DistpictureBox.Location = new Point(0, 25);
            DistpictureBox.Load(Program.gPath + Image[0][0]);
            DistpictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }
        private void CoolingGeneratorImageSelect(string type, string install)//2.냉방설비 그림
        {
            if (Install_f == "" || Install_f == null)
            {
                MessageBox.Show("먼저 냉방설비를 먼저 선택해 주세요.", "주의", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            Press_pictureBox.Visible = true;
            Press_pictureBox.Size = new System.Drawing.Size(50, 40);
            Press_pictureBox.Location = new Point(10, 115);
            Press_pictureBox.Load(Program.gPath + ImageP[0][0]);
            Press_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Press_pictureBox.BackColor = Color.Transparent;
            Press_pictureBox.Parent = SyspictureBox;
            ;
        }

        private void Install_comboBox_SelectedIndexChanged(object sender, EventArgs e)  // 열원 설비 그림
        {
            if (Install_f == "" || Install_f == null)
            {
                MessageBox.Show("냉방설비를 먼저 선택해 주세요.", "주의", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string contents = Install_comboBox.Text;
                string[][] image = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉방설비이미지", "이미지", "설비유형='" + contents + "' And 설치유형='" + Install_f + "'");
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
                StopictureBox.Visible = true;
                StopictureBox.Size = new System.Drawing.Size(135, 135);
                StopictureBox.Location = new Point(12, 98);
                StopictureBox.Load(Program.gPath + stoimage[0][0]);
                StopictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                StopictureBox.BackColor = Color.Transparent;
                StopictureBox.Parent = DistpictureBox;
            }

        }

        private void pump_image()
        {
            string[][] pumpimage = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉방설비이미지", "이미지", "항목유형='분배설비' And 설비유형 = '펌프'");
            Pump_pictureBox.Visible = true;
            Pump_pictureBox.Size = new System.Drawing.Size(40, 50);
            Pump_pictureBox.Location = new Point(40, 186);
            Pump_pictureBox.Load(Program.gPath + pumpimage[0][0]);
            Pump_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Pump_pictureBox.BackColor = Color.Transparent;
            Pump_pictureBox.Parent = DistpictureBox;
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
                StoType_pictureBox.Visible = true;
                StoType_pictureBox.Size = new System.Drawing.Size(50, 50);
                StoType_pictureBox.Location = new Point(60, 70);
                StoType_pictureBox.Load(Program.gPath + stoTimage1[0][0]);
                StoType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                StoType_pictureBox.BackColor = Color.Transparent;
                StoType_pictureBox.Parent = StopictureBox;
            }
        }

        private void imagemake(string _type) //공급설비 그림 넣기
        {
            string[][] image = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉방설비이미지", "이미지", "항목유형= '공급설비' And 설비유형='" + _type + "'");
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


        private string installmake(int a, int b, int c)
        {
            Program.UTIL.fromCode = true;


            string check = null;
            if (a > 0)
            {
                if (b == 0)
                {
                    if (c == 0)
                    {
                        radioButton1.Checked = true;
                        check = "기존";
                    }
                    else if (c > 0)
                    {
                        radioButton3.Checked = true;
                        check = "신규";
                    }
                }
                else if (b > 0)
                {
                    if (c == 0)
                    {
                        radioButton3.Checked = true;
                        check = "신규";
                    }
                    else if (c > 0)
                    {
                        radioButton3.Checked = true;
                        check = "신규";
                    }
                }
            }

            else if (a == 0)
            {
                if (b == 0)
                {
                    if (c > 1)
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
        } //신규,기존 선택하는 창
        #region A. AirCon 작성
        private void Load_AirCon()
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
                    Power_f = 0;
                    EER_f = 0;
                    Number_f = 0;
                    Power.Clear();
                    EER.Clear();

                    AirCon_dataGridView.Rows.Clear();

                    foreach (string SAC in AirCon_Load.SelectAirCon)
                    {
                        string[][] DefaultDB_Value = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", " 번호,명칭,DB유형,냉방정격용량,냉방정격소비전력,냉방정격COP,대기전력,연료,설치",
                               "번호='" + SAC + "'");
                        for (int i = 0; i < DefaultDB_Value.Length; i++)
                        {
                            check.Add(DefaultDB_Value[i][8].ToString());
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

                            Power.Add(Convert.ToDouble(DefaultDB_Value[i][3]));
                            EER.Add(Convert.ToDouble(DefaultDB_Value[i][5]));
                            Number_f = 1 + Number_f;
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
            }

        }

        private void AirconList() //다시불러올때
        {
            AirConLoad_table();

            List<string> check = new List<string>();
            int A = 0, B = 0, C = 0;
            Power_f = 0;
            EER_f = 0;
            Number_f = 0;
            Power.Clear();
            EER.Clear();


            AirCon_dataGridView.Rows.Clear();
            string[][] System = Program.DB.getValue(DB.type.ProjDB, "User_CoolingSystem", " 장비번호,제어유형,외기냉방유무,설치대수", "번호='" + Num + "'");

            for (int g = 0; g < System.Length; g++)
            {
                string[][] DefaultDB_Value = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", " 번호,명칭,DB유형,냉방정격용량,냉방정격소비전력,냉방정격COP,대기전력,연료,설치",
                       "번호='" + System[g][0] + "'");

                for (int i = 0; i < DefaultDB_Value.Length; i++)
                {
                    check.Add(DefaultDB_Value[i][8].ToString());
                    AirCon_dataGridView.Rows.Add();
                    int nRow = AirCon_dataGridView.Rows.Count - 1;

                    AirCon_dataGridView.Rows[nRow].Cells[1].Value = DefaultDB_Value[i][0];
                    AirCon_dataGridView.Rows[nRow].Cells[2].Value = DefaultDB_Value[i][1];
                    AirCon_dataGridView.Rows[nRow].Cells[3].Value = DefaultDB_Value[i][2];
                    AirCon_dataGridView.Rows[nRow].Cells[4].Value = System[g][1];
                    AirCon_dataGridView.Rows[nRow].Cells[5].Value = System[g][2];
                    AirCon_dataGridView.Rows[nRow].Cells[6].Value = System[g][3];
                    AirCon_dataGridView.Rows[nRow].Cells[7].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][3])); // 냉방출력
                    AirCon_dataGridView.Rows[nRow].Cells[8].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][4])); //소비전력
                    AirCon_dataGridView.Rows[nRow].Cells[9].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][5])); //COP
                    AirCon_dataGridView.Rows[nRow].Cells[10].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][6])); //대기전력
                    AirCon_dataGridView.Rows[nRow].Cells[11].Value = DefaultDB_Value[i][7]; //연료

                    Power.Add(Convert.ToDouble(DefaultDB_Value[i][3]) * Convert.ToDouble(System[g][3]));
                    EER.Add(Convert.ToDouble(DefaultDB_Value[i][5]));
                    Number_f = Convert.ToInt32(System[g][3]) + Number_f;
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

            EER_f = EER_f / Power_f;

            PowerTotal_textBox.Text = string.Format("{0:0.0}", Power_f);
            EERTotal_textBox.Text = string.Format("{0:0.0}", EER_f);
            InstallTotal_textBox.Text = string.Format("{0:0.0}", Number_f);
            G_label.Visible = true;
            G_label.Text = string.Format("설치대수: {0}", Number_f);
            ZoneS_label.Visible = true;
            Install_f = installmake(A, B, C);

            CoolingGeneratorImageSelect(CG, Install_f);
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
            for (int i = 0; i < items.Length; i++)
            {
                ControlcomboBox.Items.Add(items[i][0]);
            }
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
                for (int i = 1; i < 12; i++)
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
                Number.Add(Convert.ToInt32(Value[5]));
                Pctrl.Add(Convert.ToDouble(Value[9]));
                CGS.Add(Value[0]);


                Program.DB.setValue(DB.type.ProjDB, "User_CoolingSystem", "번호,명칭,장비번호,장비명칭,DB유형,제어유형,외기냉방유무,설치대수, 냉방출력, 연료",
                 "'" + Num + "','" + Name_f + "','" + Value[0] + "','" + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[10] + "'", "번호, 장비번호");
            }
            int a = Power.IndexOf(Power.Max());

            string[][] Max_info = Program.DB.getValue(DB.type.ProjDB, "User_CoolingSystem", "장비번호,제어유형,외기냉방유무,연료", "번호 = '" + Num + "' And 장비번호 = '" + CGS[a] + "'");


            CG_Max = Convert.ToString(Max_info[0][0]);
            Control_f = Convert.ToString(Max_info[0][1]);
            Econo_f = Convert.ToString(Max_info[0][2]);
            Fuel_f = Convert.ToString(Max_info[0][3]);


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

                if (AirCooler_Load.SelectAirCooler != null)
                {
                    List<string> check = new List<string>();
                    int A = 0, B = 0, C = 0;
                    Power_f = 0;
                    EER_f = 0;
                    Number_f = 0;
                    Power.Clear();
                    EER.Clear();
                    Comp.Clear();

                    AirCooler_dataGridView.Rows.Clear();

                    foreach (string SAC in AirCooler_Load.SelectAirCooler)
                    {
                        string[][] DefaultDB_Value = Program.DB.getValue(DB.type.ProjDB, "User_AirCooler", "번호,명칭,DB유형,냉방출력,냉방소비전력,EER,압축기,연료,대기전력,설치,부하측공급형식,증발기,냉수입구온도,냉수출구온도",
                              "번호='" + SAC + "'");
                        for (int i = 0; i < DefaultDB_Value.Length; i++)
                        {
                            check.Add(DefaultDB_Value[i][9]);
                            AirCooler_dataGridView.Rows.Add();
                            int nRow = AirCooler_dataGridView.Rows.Count - 1;
                            AirCooler_dataGridView.Rows[nRow].Cells[1].Value = DefaultDB_Value[i][0];
                            AirCooler_dataGridView.Rows[nRow].Cells[2].Value = DefaultDB_Value[i][1];
                            AirCooler_dataGridView.Rows[nRow].Cells[3].Value = DefaultDB_Value[i][2];
                            AirCooler_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][3]));
                            AirCooler_dataGridView.Rows[nRow].Cells[7].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][4]));
                            AirCooler_dataGridView.Rows[nRow].Cells[8].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][5]));
                            AirCooler_dataGridView.Rows[nRow].Cells[10].Value = DefaultDB_Value[i][6];
                            AirCooler_dataGridView.Rows[nRow].Cells[11].Value = DefaultDB_Value[i][7];

                            Power.Add(Convert.ToDouble(DefaultDB_Value[i][3]));
                            EER.Add(Convert.ToDouble(DefaultDB_Value[i][5]));
                            Comp.Add(DefaultDB_Value[i][6]);
                            Number_f = 1 + Number_f;
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
        private void AirCoolerLoad_table()
        {


            string[][] items = Program.DB.getValue_SameCheck(DB.type.BaseDB_Cooling, "CoolSystem", " 제어유형,압축기유형",
                               "냉동기유형='공냉식냉동기'");

            new StackedHeaderDecorator(AirCooler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

            AirCooler_dataGridView.Columns.Clear();

            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            AirCooler_dataGridView.Columns.Add(checkBoxColumn);

            AirCooler_dataGridView.Columns.Add("A1", "번호");
            AirCooler_dataGridView.Columns.Add("A2", "명칭");
            AirCooler_dataGridView.Columns.Add("A3", "DB유형");

            DataGridViewComboBoxColumn ControlcomboBox = new DataGridViewComboBoxColumn();
            ControlcomboBox.Items.Clear();
            ControlcomboBox.HeaderText = "제어유형";
            ControlcomboBox.Name = "control";
            for (int i = 0; i < items.Length; i++)
            {
                ControlcomboBox.Items.Add(items[i][0]);
            }
            AirCooler_dataGridView.Columns.Add(ControlcomboBox);

            DataGridViewComboBoxColumn EconomcomboBox = new DataGridViewComboBoxColumn();
            EconomcomboBox.Items.Clear();
            EconomcomboBox.HeaderText = "외기냉방유무";
            EconomcomboBox.Name = "Economizer";
            EconomcomboBox.Items.AddRange(new string[] { "있음", "없음" });
            AirCooler_dataGridView.Columns.Add(EconomcomboBox);

            AirCooler_dataGridView.Columns.Add("A6", "설치대수");
            AirCooler_dataGridView.Columns.Add("A7", "냉방출력[kW]");
            AirCooler_dataGridView.Columns.Add("A8", "소비전력[kW]");
            AirCooler_dataGridView.Columns.Add("A9", "냉방성능[EER]");
            AirCooler_dataGridView.Columns.Add("A10", "압축기");
            AirCooler_dataGridView.Columns.Add("A11", "연료");

            AirCooler_dataGridView.Columns[0].Width = 40;
            AirCooler_dataGridView.Columns[1].Width = 50;
        }

        private void GAirCooler_Save_button_Click(object sender, EventArgs e)
        {
            for (int k = 0; k < AirCon_dataGridView.Rows.Count; k++)
            {
                List<string> Value = new List<string>();
                for (int i = 0; i < 6; i++)
                {
                    if (AirCon_dataGridView.Rows[k].Cells[i + 1].Value != null)
                    {
                        Value.Add(AirCon_dataGridView.Rows[k].Cells[i + 1].Value.ToString());
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
            for (int i = 0; i < items.Length; i++)
            {
                ControlcomboBox.Items.Add(items[i][0]);
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
            for (int i = 0; i < items.Length; i++)
            {
                ControlcomboBox.Items.Add(items[i][0]);
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
            for (int i = 0; i < items.Length; i++)
            {
                ControlcomboBox.Items.Add(items[i][0]);
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
            for (int i = 0; i < items.Length; i++)
            {
                ControlcomboBox.Items.Add(items[i][0]);
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
                try
                {
                    if (heating_pump.SelectPump != null)
                    {
                        Pump1 = heating_pump.SelectPump;
                        string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "명칭", "번호 = '" + Pump1.ToString() + "'");
                        Pump1_textBox.Text = Value[0][0];
                        if (Pump_dataGridView.Rows.Count == 1)
                        { Load_Pump_Table(0, Pump1); }

                    }
                }
                catch { }
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
                try
                {
                    if (heating_pump.SelectPump != null)
                    {
                        Pump2 = heating_pump.SelectPump;
                        string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "명칭", "번호 = '" + Pump2.ToString() + "'");
                        Pump2_textBox.Text = Value[0][0];
                        if (Pump_dataGridView.Rows.Count == 2)
                        { Load_Pump_Table(1, Pump2); }
                    }
                }
                catch { }
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

            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "번호,명칭,종류,A효율,B효율,유량,동력,양정", "번호 = '" + PumpNum.ToString() + "'");
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
            catch { }
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
            DataGridViewCheckBoxColumn ce_checkBoxColumn = new DataGridViewCheckBoxColumn();
            new StackedHeaderDecorator(ce_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, ce_datagridviewDesign);
            ce_checkBoxColumn.HeaderText = "선택";
            ce_checkBoxColumn.Name = "check";
            ce_dataGridView.Columns.Add(ce_checkBoxColumn);
            ce_dataGridView.Columns.Add("A1", "번호");
            ce_dataGridView.Columns.Add("A2", "종류");

            ce_dataGridView.Columns.Add("A3", "일람표 명칭");
            ce_dataGridView.Columns.Add("A4", "용량.[kW]");
            ce_dataGridView.Columns.Add("A5", "소비전력.[kW]");

            ce_dataGridView.Columns.Add("A6", "적용 존명칭");

            ce_dataGridView.Columns[0].Width = 30;
            ce_dataGridView.Columns[1].Width = 150;
            ce_dataGridView.Columns[2].Width = 120;
            ce_dataGridView.Columns[3].Width = 130;
            ce_dataGridView.Columns[4].Width = 70;
            ce_dataGridView.Columns[5].Width = 70;

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
            try
            {
                String[][] Value = Program.DB.getValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호,공급설비종류,공급설비", "냉방시스템 = '" + Num + "' And 공급설비종류 = '" + CE + "'");

                int Sum = 1;
                for (int n = 0; n < Value.Length; n++)
                {
                    int nRow = ce_dataGridView.Rows.Add();

                    ce_dataGridView.Rows[nRow].Cells[2].Value = Value[n][1];//종류
                    int index = Value[n][2].IndexOf("_");
                    String substring = Value[n][2].Substring(0, index);
                    string[][] 일람표정보 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "번호,명칭,용량,소비전력", "번호 = '" + substring + "'");
                    ce_dataGridView.Rows[nRow].Cells[3].Value = 일람표정보[0][1]; //일람표명칭
                    ce_dataGridView.Rows[nRow].Cells[4].Value = 일람표정보[0][2]; //용량
                    ce_dataGridView.Rows[nRow].Cells[5].Value = 일람표정보[0][3];//소비전력
                    string[][] 존정보 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,존이름", "존번호 = '" + Value[n][0] + "'");
                    ce_dataGridView.Rows[nRow].Cells[6].Value = 존정보[0][1];//존이름
                    ce_dataGridView.Rows[nRow].Cells[1].Value = 존정보[0][0] + "_" + Value[n][2];
                }
            }
            catch { }
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

            Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Form", "번호,프로젝트유형,펌프유무,펌프방식,펌프1종류,펌프2종류,펌프1밸브,펌프2밸브,펌프1제어,펌프2제어,펌프1대수,펌프2대수", "'" + Num + "','" + 프로젝트유형[0][0] + "','" + PumpUse + "','" + PumpMethod + "','" + Pump1 + "','" + Pump2 + "','" + Pump1Valve + "','" + Pump2Valve + "','" + Pump1Control + "','" + Pump2Control + "','" + Pump1Num.ToString() + "','" + Pump2Num.ToString() + "'", "번호");
            Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Form", "번호,프로젝트유형,명칭,설치,공급존,공급AHU,냉방설비,열원설비,냉방출력,냉방성능,제어유형,연료,외기냉방시스템,대기전력,설치대수,저장탱크,저장유형",
                "'" + Num + "','" + 프로젝트유형[0][0] + "','" + Name_f + "','" + Install_f + "','" + SelectedZone + "','" + SelectedAHU + "', '" + CG + "','" + CSource +
                "', '" + Power_f + "', '" + EER_f + "', '" + Control_f + "','" + Fuel_f + "','" + Econo_f + "','" + Pctrl_f + "','" + Number_f + "','" + Stotype + "','" + StoSource + "'", "번호");

            Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Form", "번호,프로젝트유형,공급설비1종류,공급설비2종류", "'" + Num + "','" + 프로젝트유형[0][0] + "','" + ce1Type + "','" + ce2Type + "'", "번호");


            this.DialogResult = DialogResult.OK;
            this.Hide();
            Program.getMenuForm().DoLoadForm(38, OnLoadListProc);
        }

        public void Save_CG()
        {
            if (CG_Max == null || CG_Max == "")
            {
                MessageBox.Show("생산설비의 SAVE 버튼을 클릭하세요.");
            }
            else
            {
                switch (CG)
                {
                    case "실내기12kW":
                        Program.DB.setValue(DB.type.ProjDB, "CoolingSystem_Form", "번호,프로젝트유형,명칭,설치,공급존,공급AHU,냉방설비,열원설비,냉방출력,냉방성능,제어유형,연료,외기냉방시스템,대기전력,설치대수,저장탱크,저장유형",
                               "'" + Num + "','" + 프로젝트유형[0][0] + "','" + Name_f + "','" + Install_f + "','" + SelectedZone + "','" + SelectedAHU + "', '" + CG + "','" + CSource +
                               "', '" + Power_f + "', '" + EER_f + "', '" + Control_f + "','" + Fuel_f + "','" + Econo_f + "','" + Pctrl_f + "','" + Number_f + "','" + Stotype + "','" + StoSource + "'", "번호");
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
            Num = null; Name_f = null; SelectedZone = null; SelectedAHU = null; Install_f = null;
            CG = null; CSource = null; Comp_f = null;
            Power_f = 0; EER_f = 0;
            Number_f = 0;
            MultiZone = null;

            Stotype = null; StoSource = null;

            PumpUse_comboBox.SelectedItem = null;
            PumpMethod_comboBox.SelectedItem = null;
            Pump1_textBox.Text = null;
            Pump2_textBox.Text = null;
            Pump_dataGridView.Columns.Clear();
            Pump_dataGridView.Rows.Clear();

            ce1Type_comboBox.SelectedItem = null;
            ce2Type_comboBox.SelectedItem = null;
            ce1Zone_textBox.Text = null;
            ce2Zone_textBox.Text = null;
            ce_dataGridView.Columns.Clear();
            ce_dataGridView.Rows.Clear();

        }

        public void LoadData(String ID) // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();
            NumTextBox.Text = ID; ;
            Num = ID;

            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "명칭,설치,공급존,공급AHU,냉방설비,열원설비,냉방출력,냉방성능,설치대수,저장탱크,저장유형", "번호='" + ID + "'");
                CoolingSystemNameText.Text = Value[0][0];
                Name_f = Value[0][0];
                Install_f = Value[0][1];

                Zonemainwrite(ID);
                CoolingGeneratorSelect_comboBox.Text = Value[0][4];
                CG = CoolingGeneratorSelect_comboBox.Text;
                Install_comboBox.Text = Value[0][5];
                Distribute_Image();
                StorageList_comboBox.Text = Value[0][9];
                StorageType_comboBox.Text = Value[0][10];




                switch (CG)
                {
                    case "실외기12kW":
                        AirconList();
                        break;
                    case "공냉식냉동기":
                        Load_AirCooler(); //작업해야함
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
            catch { }


            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "펌프유무,펌프방식,펌프1종류,펌프2종류,펌프1밸브,펌프2밸브,펌프1제어,펌프2제어,펌프1대수,펌프2대수", "번호 = '" + ID + "'");

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
            catch { }



            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "공급설비1종류,공급설비2종류", "번호 = '" + ID + "'");
                ce1Type = Value[0][0];
                ce1Type_comboBox.SelectedItem = ce1Type;

                ce2Type = Value[0][1];
                ce2Type_comboBox.SelectedItem = ce2Type;

                if (ce1Type != null && ce1Type != "")
                {
                    try
                    {
                        Create_ce_Table();
                        Load_ce(ce1Type);
                        Load_ce1Zone(ce1Type);

                        for (int n = 0; n < ce_dataGridView.Rows.Count; n++)
                        {
                            String 존번호, 공급설비;
                            int index = ce_dataGridView.Rows[n].Cells[1].Value.ToString().IndexOf("CE");
                            존번호 = ce_dataGridView.Rows[n].Cells[1].Value.ToString().Substring(0, index - 1);
                            공급설비 = ce_dataGridView.Rows[n].Cells[1].Value.ToString().Substring(index, ce_dataGridView.Rows[n].Cells[1].Value.ToString().Length - index);

                            string[][] CE_Value = Program.DB.getValue(DB.type.ProjDB, "Cooling_ce_Form", "설치위치", "냉방시스템 = '" + ID + "' And 존번호 = '" + 존번호 + "' And 공급설비 = '" + 공급설비 + "'");
                            ce_dataGridView.Rows[n].Cells[7].Value = CE_Value[0][0].ToString();
                        }
                    }
                    catch { }
                }

                if (ce2Type != null && ce2Type != "")
                {
                    try
                    {

                        Load_ce(ce2Type);
                        Load_ce2Zone(ce2Type);
                        for (int n = 0; n < ce_dataGridView.Rows.Count; n++)
                        {
                            String 존번호, 공급설비;
                            int index = ce_dataGridView.Rows[n].Cells[1].Value.ToString().IndexOf("CE");
                            존번호 = ce_dataGridView.Rows[n].Cells[1].Value.ToString().Substring(0, index - 1);
                            공급설비 = ce_dataGridView.Rows[n].Cells[1].Value.ToString().Substring(index, ce_dataGridView.Rows[n].Cells[1].Value.ToString().Length - index);

                            string[][] CE_Value = Program.DB.getValue(DB.type.ProjDB, "Cooling_ce_Form", "설치위치", "냉방시스템 = '" + ID + "' And 존번호 = '" + 존번호 + "' And 공급설비 = '" + 공급설비 + "'");
                            ce_dataGridView.Rows[n].Cells[7].Value = CE_Value[0][0].ToString();
                        }
                    }
                    catch { }
                }
            }
            catch { }

        }

        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            NumTextBox.Text = ID;
            Num = ID;
        }

        private void AirCon_Remove_button_Click(object sender, EventArgs e)
        {
            AirCon_dataGridView.SelectedRows.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cal_Cooling cc1 = new Cal_Cooling(Num);
        }
    }

}
