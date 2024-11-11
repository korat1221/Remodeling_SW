using main.contentslist;
using main.subcontents.DHWSystem;
using main.subcontents.ConstructionCW;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static main.DB;
using static System.ComponentModel.Design.ObjectSelectorEditor;


using main.subcontents.HeatingSystem;

namespace main.contents
{
    public partial class DHWSystem : Form
    {
        String Num, Name; String SelectZone_nonsplit;
        String SystemLoacation, SLRL, Complex, MainSystem, Sub1System, Sub2System;
        String SelectBoiler_nonsplit, BoilerNum_nonsplit;
        String SelectSolar_nonsplit, SolarNum_nonsplit, SolarDirection_nonsplit, SolarDegree_nonsplit;
        String SelectHP_nonsplit, HPNum_nonsplit, HPControl_nonsplit; //외기/지열/지하수 순 
        String SelectDH_nonsplit;
        String PumpUse, PumpMethod, Pump1, Pump2, Pump1Valve, Pump2Valve, Pump1Control, Pump2Control; int Pump1Num, Pump2Num;
        String StorageUse, StoragePumpUse, StoragePump, StorageType; double Vs;
        String[] SystemType = { "보일러", "지역난방", "태양열시스템", "외기 히트펌프" };
        double PipeD, PipeInsD, PipeIns_Ramda;
        String PipeIns;
        double ZoneArea;
        ArrayList SelectZone_split = new ArrayList(); ArrayList SelectBoiler_split = new ArrayList(); ArrayList SelectHP_split = new ArrayList(); ArrayList SelectGroundHP_split = new ArrayList(); ArrayList SelectGWHP_split = new ArrayList(); ArrayList SelectSolar_split = new ArrayList(); ArrayList SelectAS_split = new ArrayList(); ArrayList SelectDH_split = new ArrayList();
        string[][] 프로젝트유형;
        public DHWSystem()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '급탕시스템'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            if (프로젝트유형.Length > 0)
            {
                if (프로젝트유형[0][0] == "1")
                {
                    radioButton1.Checked = true;
                    radioButton2.Enabled = false;
                    radioButton3.Enabled = false;
                    radioButton4.Enabled = false;
                }
                else if (프로젝트유형[0][0] == "4")
                {
                    radioButton1.Enabled = false;
                    radioButton2.Checked = false;
                    radioButton3.Enabled = true;
                    radioButton4.Enabled = false;
                }
            }
            //복합설비 콤보박스  
            Complex_comboBox.Items.Clear();
            Complex_comboBox.Items.Add("단일설비가동");
            Complex_comboBox.Items.Add("복합설비가동");
            Complex_comboBox.SelectedIndex = 0;

            //설치위치 콤보박스
            SystemLoacation_comboBox.Items.Clear();
            SystemLoacation_comboBox.Items.Add("단열외피 내부");
            SystemLoacation_comboBox.Items.Add("단열외피 외부");
            SystemLoacation_comboBox.Items.Add("외기");
            SystemLoacation_comboBox.SelectedIndex = 1;

            //공급온도/환수온도 콤보박스
            SLRL_comboBox.Items.Clear();
            SLRL_comboBox.Items.Add("고온수(70/55)");
            SLRL_comboBox.Items.Add("중온수(55/45)");
            SLRL_comboBox.Items.Add("저온수(35/28)");
            SLRL_comboBox.SelectedIndex = 1;

            //설비유형 콤보박스
            MainSystem_comboBox.Items.Clear();
            Sub1System_comboBox.Items.Clear();
            Sub2System_comboBox.Items.Clear();
            for (int i = 0; i < SystemType.Length; i++)
            {
                MainSystem_comboBox.Items.Add(SystemType[i]);
                Sub1System_comboBox.Items.Add(SystemType[i]);
                Sub2System_comboBox.Items.Add(SystemType[i]);
            }
            MainSystem_comboBox.SelectedIndex = 0;
            //축열 유무 콤보박스
            StorageUse_comboBox.Items.Clear();
            StorageUse_comboBox.Items.Add("축열탱크 없음");
            StorageUse_comboBox.Items.Add("축열탱크 있음");
            StorageUse_comboBox.SelectedIndex = 0;
            //축열펌프 유무 콤보박스
            StoragePump_comboBox.Items.Clear();
            StoragePump_comboBox.Items.Add("축열펌프 없음");
            StoragePump_comboBox.Items.Add("축열펌프 있음");
            StoragePump_comboBox.SelectedIndex = 0;
            //펌프 유무 콤보박스 
            PumpUse_comboBox.Items.Clear();
            PumpUse_comboBox.Items.Add("펌프 있음");
            PumpUse_comboBox.Items.Add("펌프 없음(설비 내장)");
            PumpUse_comboBox.SelectedIndex = 1;
            //펌프 방식 콤보박스
            PumpMethod_comboBox.Items.Clear();
            PumpMethod_comboBox.Items.Add("1차펌프");
            PumpMethod_comboBox.Items.Add("1차폐회로+2차펌프");

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
        private void Name_textBox_TextChanged(object sender, EventArgs e)
        {
            if (Name_textBox.Text != null)
            {
                Name = Name_textBox.Text.ToString();
            }
        }
        #region 존
        private void Zone_button_Click(object sender, EventArgs e)
        {
            DHW_Zone heatingzone = new DHW_Zone(Num, SelectZone_nonsplit);
            DialogResult result = heatingzone.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (heatingzone.SelectZone != null)
                {
                    SelectZone_nonsplit = heatingzone.SelectZone;
                    Split_Zone(heatingzone.SelectZone);
                    Calc_Pipe();
                    ce_Pic();
                }
            }
        }
        private void ce_Pic()
        {
            if (radioButton1.Checked)
            {
                ce1_pictureBox.Visible = true;
                ce1_pictureBox.Size = new System.Drawing.Size(360, 90); //260,60
                ce1_pictureBox.Location = new Point(180, 8); //250,10
                ce1_pictureBox.Load(Program.gPath + "images/HeatingSystem/Old/Wsup.png");
                ce1_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                ce1_pictureBox.BackColor = Color.Transparent;
                ce1_pictureBox.Parent = DistpictureBox;
            }
            else
            {
                ce1_pictureBox.Visible = true;
                ce1_pictureBox.Size = new System.Drawing.Size(360, 90);
                ce1_pictureBox.Location = new Point(180, 8);
                ce1_pictureBox.Load(Program.gPath + "images/HeatingSystem/New/Wsup.png");
                ce1_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                ce1_pictureBox.BackColor = Color.Transparent;
                ce1_pictureBox.Parent = DistpictureBox;
            }
        }
        private void Split_Zone(String nonSplit)
        {
            String 내용;
            if (nonSplit != null)
            {
                if (nonSplit.Contains("+"))
                {
                    string[] token = nonSplit.Split('+');
                    SelectZone_split.Clear();
                    foreach (var item in token)
                    {
                        SelectZone_split.Add(item.ToString());
                    }
                    내용 = SelectZone_split[0].ToString() + " 외 " + (SelectZone_split.Count - 1).ToString() + "개";
                }
                else
                {
                    SelectZone_split.Clear();
                    SelectZone_split.Add(nonSplit);
                    내용 = SelectZone_split[0].ToString();
                }
                Zone_textBox.Text = 내용;
            }
            else { 내용 = ""; }

        }
        #endregion

        #region 생산
        /////////////////////////////////////////////////////생산////////////////////////////////////////////////////////////////////

        private void SystemLoacation_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SystemLoacation_comboBox.SelectedItem != null)
            {
                SystemLoacation = SystemLoacation_comboBox.SelectedItem.ToString();
            }
            else
            {
                SystemLoacation = null;
            }
        }

        private void SLRL_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SLRL_comboBox.SelectedItem != null)
            {
                SLRL = SLRL_comboBox.SelectedItem.ToString();
            }
            else
            {
                SLRL = null;
            }
        }

        private void Complex_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Complex_comboBox.SelectedItem != null)
            {
                Complex = Complex_comboBox.SelectedItem.ToString();
                ChangeVisble_Complex(Complex);
            }
            else
            {
                Complex = null;
            }
        }
        private void ChangeVisble_Complex(String Complex)
        {
            if (Complex != "복합설비가동")
            {
                Sub1System_label.Visible = false;
                Sub2System_label.Visible = false;
                Sub1System_comboBox.Visible = false;
                Sub2System_comboBox.Visible = false;
                Sub1UserList_Label.Visible = false;
                Sub2UserList_Label.Visible = false;
                Sub1UserList_textBox.Visible = false;
                Sub2UserList_textBox.Visible = false;
                Sub1UserList_button.Visible = false;
                Sub2UserList_button.Visible = false;
            }
            else
            {
                Sub1System_label.Visible = true;
                Sub2System_label.Visible = true;
                Sub1System_comboBox.Visible = true;
                Sub2System_comboBox.Visible = true;
                Sub1UserList_Label.Visible = true;
                Sub2UserList_Label.Visible = true;
                Sub1UserList_textBox.Visible = true;
                Sub2UserList_textBox.Visible = true;
                Sub1UserList_button.Visible = true;
                Sub2UserList_button.Visible = true;
            }
        }
        private void MainSystem_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MainSystem_comboBox.SelectedItem != null)
            {
                MainSystem = MainSystem_comboBox.SelectedItem.ToString();
                LoadImage();
                MainSystemImage(MainSystem, "신규");
                LoadtabPage(MainSystem);
                if (MainSystem == Sub1System)
                {
                    MessageBox.Show("이미 Sub1설비로 선택되어 있습니다. 다른 설비를 선택하세요.");
                }
                else if (MainSystem == Sub2System)
                {
                    MessageBox.Show("이미 Sub2설비로 선택되어 있습니다. 다른 설비를 선택하세요.");
                }

                if (MainSystem == "지열 히트펌프" || MainSystem == "지하수 히트펌프")
                { HeatSourceImage("지열", "신규"); }
                else if (MainSystem == "지역난방")
                { HeatSourceImage("지역난방", "신규"); }
                else
                { HeatSourceImage(null, "신규"); }
            }
            else
            {
                MainSystem = null;
            }
        }

        private void SubSystem1_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Sub1System_comboBox.SelectedItem != null)
            {
                Sub1System = Sub1System_comboBox.SelectedItem.ToString();
                LoadtabPage(Sub1System);
                if (Sub1System == MainSystem)
                {
                    MessageBox.Show("이미 Main설비로 선택되어 있습니다. 다른 설비를 선택하세요.");
                }
                else if (Sub1System == Sub2System)
                {
                    MessageBox.Show("이미 Sub2설비로 선택되어 있습니다. 다른 설비를 선택하세요.");
                }
                if (Sub1System == "지열 히트펌프" || Sub1System == "지하수 히트펌프")
                { HeatSourceImage("지열", "신규"); }
                else if (Sub1System == "지역난방")
                { HeatSourceImage("지역난방", "신규"); }
                else
                { HeatSourceImage(null, "신규"); }
            }
            else
            {
                Sub1System = null;
            }
        }

        private void Sub2System_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Sub2System_comboBox.SelectedItem != null)
            {
                Sub2System = Sub2System_comboBox.SelectedItem.ToString();
                LoadtabPage(Sub2System);
                if (Sub2System == MainSystem)
                {
                    MessageBox.Show("이미 Main설비로 선택되어 있습니다. 다른 설비를 선택하세요.");
                }
                else if (Sub2System == Sub1System)
                {
                    MessageBox.Show("이미 Sub1설비로 선택되어 있습니다. 다른 설비를 선택하세요.");
                }
                if (Sub2System == "지열 히트펌프" || Sub2System == "지하수 히트펌프")
                { HeatSourceImage("지열", "신규"); }
                else if (Sub2System == "지역난방")
                { HeatSourceImage("지역난방", "신규"); }
                else
                { HeatSourceImage(null, "신규"); }
            }
            else
            {
                Sub2System = null;
            }
        }

        private void MainUserList_button_Click(object sender, EventArgs e)
        {
            if (MainSystem == "보일러")
            {
                Load_BoilerForm();
            }
            else if (MainSystem == "외기 히트펌프")
            {
                Load_HPForm();
            }
            else if (MainSystem == "태양열시스템")
            {
                Load_SolarForm();
            }
            else if (MainSystem == "지역난방")
            {
                Load_DHForm();
            }

        }

        private void Sub1UserList_button_Click(object sender, EventArgs e)
        {
            if (Sub1System == "보일러")
            {
                Load_BoilerForm();
            }
            else if (Sub1System == "태양열시스템")
            {
                Load_SolarForm();
            }
            else if (Sub1System == "지역난방")
            {
                Load_DHForm();
            }
        }

        private void Sub2UserList_button_Click(object sender, EventArgs e)
        {
            if (Sub2System == "보일러")
            {
                Load_BoilerForm();
            }
            else if (Sub2System == "태양열시스템")
            {
                Load_SolarForm();
            }
            else if (Sub2System == "지역난방")
            {
                Load_DHForm();
            }
        }

        private void LoadtabPage(String System)
        {
            if (System == "보일러")
            {
                tabControl2.SelectedTab = tabControl2.TabPages["Boiler_tabPage"];
            }
            else if (System == "지역난방")
            {
                tabControl2.SelectedTab = tabControl2.TabPages["DH_tabPage"];
            }
            else if (System == "태양열시스템")
            {
                tabControl2.SelectedTab = tabControl2.TabPages["Solar_tabPage"];
            }
            else if (System == "외기 히트펌프")
            {
                tabControl2.SelectedTab = tabControl2.TabPages["HP_tabPage"];
            }
            ChangeIndex_StorageType_comboBox();
        }

        #endregion

        #region 보일러
        /////////////////////////////////////////////////////보일러////////////////////////////////////////////////////////////////////
        private void Load_BoilerForm()
        {
            // Heating_Boiler heating_Boiler = new Heating_Boiler(this);
            DHW_BoilerDB heating_Boiler = new DHW_BoilerDB("장비일람표 적용", SelectBoiler_nonsplit);
            DialogResult result = heating_Boiler.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (heating_Boiler.SelectBoiler != null)
                {
                    SelectBoiler_nonsplit = heating_Boiler.SelectBoiler;
                    Split_Boiler(heating_Boiler.SelectBoiler);
                }
            }
        }

        private void Split_Boiler(String nonSplit)
        {
            String 내용 = null;
            if (nonSplit != null)
            {
                if (nonSplit.Contains('+'))
                {
                    string[] token = nonSplit.Split('+');
                    SelectBoiler_split.Clear();
                    foreach (var item in token)
                    {
                        SelectBoiler_split.Add(item.ToString());
                    }

                    string[][] BoilerName = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "명칭", "번호 = '" + SelectBoiler_split[0].ToString() + "'");
                    if (BoilerName.Length > 0)
                    { 내용 = BoilerName[0][0] + " 외 " + (SelectBoiler_split.Count - 1).ToString() + "개"; }
                }
                else
                {
                    SelectBoiler_split.Clear();
                    SelectBoiler_split.Add(nonSplit);
                    string[][] BoilerName = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "명칭", "번호 = '" + SelectBoiler_split[0].ToString() + "'");
                    if (BoilerName.Length > 0)
                    { 내용 = BoilerName[0][0]; }
                }

                if (MainSystem == "보일러")
                {
                    MainUserList_textBox.Text = 내용;
                }
                else if (Sub1System == "보일러")
                {
                    Sub1UserList_textBox.Text = 내용;
                }
                else if (Sub2System == "보일러")
                {
                    Sub2UserList_textBox.Text = 내용;
                }
                Load_Boiler_Table();
            }
            else
            {
                내용 = "";
            }
        }

        private void Load_Boiler_Table()
        {
            DataGridViewCheckBoxColumn Boiler_checkBoxColumn = new DataGridViewCheckBoxColumn();
            new StackedHeaderDecorator(Boiler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            Boiler_dataGridView.Columns.Clear();
            Boiler_checkBoxColumn.HeaderText = "선택";
            Boiler_checkBoxColumn.Name = "check";
            Boiler_dataGridView.Columns.Add(Boiler_checkBoxColumn);
            Boiler_dataGridView.Columns.Add("A1", "번호");
            Boiler_dataGridView.Columns.Add("A2", "명칭");
            Boiler_dataGridView.Columns.Add("A3", "연료");
            Boiler_dataGridView.Columns.Add("A4", "Type");
            Boiler_dataGridView.Columns.Add("A5", "용량.[kW]");
            Boiler_dataGridView.Columns.Add("A6", "효율.전부하효율.[%]");
            Boiler_dataGridView.Columns.Add("A7", "효율.부분부하효율.[%]");
            Boiler_dataGridView.Columns.Add("A8", "소비전력.[W]");
            Boiler_dataGridView.Columns.Add("A9", "대기전력.[W]");
            Boiler_dataGridView.Columns.Add("A10", "대수.[EA]");
            Boiler_dataGridView.Columns[0].Width = 30;

            for (int n = 0; n < SelectBoiler_split.Count; n++)
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "번호,명칭,연료,Type,용량,전부하효율,부분부하효율,소비전력,대기전력", "번호 = '" + SelectBoiler_split[n].ToString() + "'");
                if (User_Value.Length > 0)
                {
                    string 용량 = "", 전부하효율 = "", 부분부하효율 = "", 소비전력 = "", 대기전력 = "";
                    if (User_Value[0][4] != null && User_Value[0][4] != "")
                    {
                        double a = Convert.ToDouble(User_Value[0][4]);
                        용량 = string.Format("{0:F1}", Convert.ToDouble(User_Value[0][4]));
                    }
                    if (User_Value[0][5] != null && User_Value[0][5] != "")
                    {
                        전부하효율 = string.Format("{0:F1}", Convert.ToDouble(User_Value[0][5]));
                    }
                    if (User_Value[0][6] != null && User_Value[0][6] != "")
                    {
                        부분부하효율 = string.Format("{0:F1}", Convert.ToDouble(User_Value[0][6]));
                    }
                    if (User_Value[0][7] != null && User_Value[0][7] != "")
                    {
                        소비전력 = string.Format("{0:F0}", Convert.ToDouble(User_Value[0][7]));
                    }
                    if (User_Value[0][8] != null && User_Value[0][8] != "")
                    {
                        대기전력 = string.Format("{0:F0}", Convert.ToDouble(User_Value[0][8]));
                    }
                    Boiler_dataGridView.Rows.Add();
                    int nRow = Boiler_dataGridView.Rows.Count - 1;
                    for (int k = 1; k < 5; k++)
                    {
                        Boiler_dataGridView.Rows[nRow].Cells[k].Value = User_Value[0][k];
                    }
                    Boiler_dataGridView.Rows[nRow].Cells[5].Value = 용량;
                    Boiler_dataGridView.Rows[nRow].Cells[6].Value = 전부하효율;
                    Boiler_dataGridView.Rows[nRow].Cells[7].Value = 부분부하효율;
                    Boiler_dataGridView.Rows[nRow].Cells[8].Value = 소비전력;
                    Boiler_dataGridView.Rows[nRow].Cells[9].Value = 대기전력;
                }
            }
        }

        private void NonSplit_BoilerNum()
        {
            if (Boiler_dataGridView.Rows.Count == 0)
            { BoilerNum_nonsplit = null; }
            else if (Boiler_dataGridView.Rows.Count == 1 && Boiler_dataGridView.Rows[0].Cells[10] != null)
            { BoilerNum_nonsplit += Boiler_dataGridView.Rows[0].Cells[10].Value.ToString(); }
            else
            {
                int CheckNull = 0;
                for (int k = 0; k < Boiler_dataGridView.RowCount; k++)
                {
                    if (Boiler_dataGridView.Rows[k].Cells[10].Value == null)
                    {
                        CheckNull = CheckNull + 1;
                    }
                }
                if (CheckNull == 0)
                {
                    for (int k = 0; k < Boiler_dataGridView.RowCount; k++)
                    {
                        BoilerNum_nonsplit += Boiler_dataGridView.Rows[k].Cells[10].Value.ToString() + "+";
                    }
                }
                else
                {
                    MessageBox.Show("보일러 대수를 모두 입력하세요.");
                }
            }
        }
        private void Split_BoilerNum(String nonSplit)
        {
            if (nonSplit != null)
            {
                if (nonSplit.Contains('+'))
                {
                    ArrayList BoilerNum_split = new ArrayList();

                    string[] token = nonSplit.Split('+');
                    BoilerNum_split.Clear();
                    foreach (var item in token)
                    {
                        BoilerNum_split.Add(item.ToString());
                    }
                    for (int k = 0; k < Boiler_dataGridView.Rows.Count; k++)
                    {
                        Boiler_dataGridView.Rows[k].Cells[10].Value = BoilerNum_split[k];
                    }
                }
                else
                {
                }
            }
            else { return; }

        }
        #endregion

        #region 태양열
        /////////////////////////////////////////////////////태양열////////////////////////////////////////////////////////////////////
        private void Load_SolarForm()
        {
            DHW_SolarDB heating_Solar = new DHW_SolarDB("장비일람표 적용", SelectSolar_nonsplit);
            DialogResult result = heating_Solar.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (heating_Solar.SelectSolar != null)
                {
                    SelectSolar_nonsplit = heating_Solar.SelectSolar;
                    Split_Solar(heating_Solar.SelectSolar);
                }
            }
        }

        private void Split_Solar(String nonSplit)
        {
            String 내용 = null;
            if (nonSplit != null)
            {
                if (nonSplit.Contains('+'))
                {
                    string[] token = nonSplit.Split('+');
                    SelectSolar_split.Clear();
                    foreach (var item in token)
                    {
                        SelectSolar_split.Add(item.ToString());
                    }

                    string[][] SolarName = Program.DB.getValue(DB.type.ProjDB, "User_Solar", "명칭", "번호 = '" + SelectSolar_split[0].ToString() + "'");
                    if (SolarName.Length > 0)
                    { 내용 = SolarName[0][0] + " 외 " + (SelectSolar_split.Count - 1).ToString() + "개"; }

                }
                else
                {
                    SelectSolar_split.Clear();
                    SelectSolar_split.Add(nonSplit);
                    string[][] SolarName = Program.DB.getValue(DB.type.ProjDB, "User_Solar", "명칭", "번호 = '" + SelectSolar_split[0].ToString() + "'");
                    if (SolarName.Length > 0)
                    { 내용 = SolarName[0][0]; }
                }

                if (MainSystem == "태양열시스템")
                {
                    MainUserList_textBox.Text = 내용;
                }
                else if (Sub1System == "태양열시스템")
                {
                    Sub1UserList_textBox.Text = 내용;
                }
                else if (Sub2System == "태양열시스템")
                {
                    Sub2UserList_textBox.Text = 내용;
                }
                Load_Solar_Table();
            }
            else
            {
                내용 = "";
            }
        }

        private void Load_Solar_Table()
        {
            DataGridViewCheckBoxColumn Solar_checkBoxColumn = new DataGridViewCheckBoxColumn();
            new StackedHeaderDecorator(Solar_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            Solar_dataGridView.Columns.Clear();
            Solar_checkBoxColumn.HeaderText = "선택";
            Solar_checkBoxColumn.Name = "check";
            Solar_dataGridView.Columns.Add(Solar_checkBoxColumn);
            Solar_dataGridView.Columns.Add("A1", "번호");
            Solar_dataGridView.Columns.Add("A2", "명칭");
            Solar_dataGridView.Columns.Add("A3", "모듈면적.A[m2]");
            Solar_dataGridView.Columns.Add("A4", "효율.ηo");
            Solar_dataGridView.Columns.Add("A5", "손실계수.1차.k1");
            Solar_dataGridView.Columns.Add("A6", "손실계수.2차.k2");
            Solar_dataGridView.Columns.Add("A7", "50°의 입사각.Khem(50֠)");
            Solar_dataGridView.Columns.Add("A8", "유효 열용량.C");
            Solar_dataGridView.Columns.Add("A9", "설치.모듈개수");
            Solar_dataGridView.Columns.Add("A10", "설치.방위");
            Solar_dataGridView.Columns.Add("A11", "설치.기울기");
            Solar_dataGridView.Columns[0].Width = 30;

            for (int n = 0; n < SelectSolar_split.Count; n++)
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_Solar", "번호,명칭,모듈면적,효율,열손실계수1차,열손실계수2차,입사각50도,유효열용량", "번호 = '" + SelectSolar_split[n].ToString() + "'");
                if (User_Value.Length > 0)
                {
                    Solar_dataGridView.Rows.Add();
                    int nRow = Solar_dataGridView.Rows.Count - 1;
                    for (int k = 1; k < 9; k++)
                    {
                        Solar_dataGridView.Rows[nRow].Cells[k].Value = User_Value[0][k - 1];
                    }
                    DataGridViewComboBoxCell 방위comboBox = new DataGridViewComboBoxCell();
                    방위comboBox.Items.Add("수평");
                    방위comboBox.Items.Add("동");
                    방위comboBox.Items.Add("서");
                    방위comboBox.Items.Add("남");
                    방위comboBox.Items.Add("북");
                    방위comboBox.Items.Add("남동");
                    방위comboBox.Items.Add("남서");
                    방위comboBox.Items.Add("북동");
                    방위comboBox.Items.Add("북서");
                    Solar_dataGridView.Rows[nRow].Cells[10] = 방위comboBox;
                    DataGridViewComboBoxCell 기울기comboBox = new DataGridViewComboBoxCell();
                    기울기comboBox.Items.Add("0˚");
                    기울기comboBox.Items.Add("30˚");
                    기울기comboBox.Items.Add("45˚");
                    기울기comboBox.Items.Add("60˚");
                    기울기comboBox.Items.Add("90˚");
                    Solar_dataGridView.Rows[nRow].Cells[11] = 기울기comboBox;
                }
            }
        }

        private void NonSplit_Solar()
        {

            for (int k = 0; k < Solar_dataGridView.Rows.Count; k++)
            {

                if (Solar_dataGridView.Rows[k].Cells[9].Value == null || Solar_dataGridView.Rows[k].Cells[10].Value == null || Solar_dataGridView.Rows[k].Cells[11].Value == null)
                {
                    MessageBox.Show("태양열시스템의 모든 정보를 입력하세요.");
                    break;
                }
            }


            for (int k = 0; k < Solar_dataGridView.Rows.Count; k++)
            {
                if (k == Solar_dataGridView.Rows.Count - 1 && Solar_dataGridView.Rows[k].Cells[9].Value != null && Solar_dataGridView.Rows[k].Cells[10].Value != null && Solar_dataGridView.Rows[k].Cells[11].Value != null)
                {
                    SolarNum_nonsplit += Solar_dataGridView.Rows[k].Cells[9].Value.ToString();
                    SolarDirection_nonsplit += Solar_dataGridView.Rows[k].Cells[10].Value.ToString();
                    SolarDegree_nonsplit += Solar_dataGridView.Rows[k].Cells[11].Value.ToString();
                }
                else if (Solar_dataGridView.Rows[k].Cells[9].Value != null && Solar_dataGridView.Rows[k].Cells[10].Value != null && Solar_dataGridView.Rows[k].Cells[11].Value != null)
                {
                    SolarNum_nonsplit += Solar_dataGridView.Rows[k].Cells[9].Value.ToString() + "+";
                    SolarDirection_nonsplit += Solar_dataGridView.Rows[k].Cells[10].Value.ToString() + "+";
                    SolarDegree_nonsplit += Solar_dataGridView.Rows[k].Cells[11].Value.ToString() + "+";
                }
            }
        }
        private void Split_SolarNum(String nonSplit)
        {
            if (nonSplit != null)
            {
                if (nonSplit.Contains('+'))
                {
                    ArrayList Solar_split = new ArrayList();

                    string[] token = nonSplit.Split('+');
                    Solar_split.Clear();
                    foreach (var item in token)
                    {
                        Solar_split.Add(item.ToString());
                    }
                    for (int k = 0; k < Solar_dataGridView.Rows.Count; k++)
                    {
                        Solar_dataGridView.Rows[k].Cells[9].Value = Solar_split[k];
                    }
                }
                else
                {
                    if (Solar_dataGridView.Rows.Count > 0)
                    { Solar_dataGridView.Rows[0].Cells[9].Value = nonSplit; }
                }
            }
            else { return; }

        }
        private void Split_SolarDirection(String nonSplit)
        {
            if (nonSplit != null)
            {
                if (nonSplit.Contains('+'))
                {
                    ArrayList Solar_split = new ArrayList();

                    string[] token = nonSplit.Split('+');
                    Solar_split.Clear();
                    foreach (var item in token)
                    {
                        Solar_split.Add(item.ToString());
                    }
                    for (int k = 0; k < Solar_dataGridView.Rows.Count; k++)
                    {
                        Solar_dataGridView.Rows[k].Cells[10].Value = Solar_split[k];
                    }
                }
                else
                {
                    if (Solar_dataGridView.Rows.Count > 0)
                    {
                        Solar_dataGridView.Rows[0].Cells[10].Value = nonSplit;
                    }
                }
            }
            else { return; }

        }
        private void Split_SolarDegree(String nonSplit)
        {
            if (nonSplit != null)
            {
                if (nonSplit.Contains('+'))
                {
                    ArrayList Solar_split = new ArrayList();

                    string[] token = nonSplit.Split('+');
                    Solar_split.Clear();
                    foreach (var item in token)
                    {
                        Solar_split.Add(item.ToString());
                    }
                    for (int k = 0; k < Solar_dataGridView.Rows.Count; k++)
                    {
                        Solar_dataGridView.Rows[k].Cells[11].Value = Solar_split[k];
                    }
                }
                else
                {
                    if (Solar_dataGridView.Rows.Count > 0)
                    {
                        Solar_dataGridView.Rows[0].Cells[11].Value = nonSplit;
                    }
                }
            }
            else { return; }

        }

        #endregion

        #region 지역난방
        /////////////////////////////////////////////////////지역난방///////////////////////////////////////////////////////////////////
        private void Load_DHForm()
        {
            DHWDH_DB heating_DH = new DHWDH_DB("장비일람표 적용", SelectDH_nonsplit);
            DialogResult result = heating_DH.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (heating_DH.SelectDH != null)
                {
                    SelectDH_nonsplit = heating_DH.SelectDH;
                    Split_DH(heating_DH.SelectDH);
                }
            }
        }

        private void Split_DH(String nonSplit)
        {
            String 내용 = null;
            if (nonSplit != null)
            {
                if (nonSplit.Contains('+'))
                {
                    string[] token = nonSplit.Split('+');
                    SelectDH_split.Clear();
                    foreach (var item in token)
                    {
                        SelectDH_split.Add(item.ToString());
                    }

                    string[][] DHName = Program.DB.getValue(DB.type.ProjDB, "User_DH", "명칭", "번호 = '" + SelectDH_split[0].ToString() + "'");
                    if (DHName.Length > 0)
                    {
                        내용 = DHName[0][0] + " 외 " + (SelectDH_split.Count - 1).ToString() + "개";
                    }
                }
                else
                {
                    SelectDH_split.Clear();
                    SelectDH_split.Add(nonSplit);
                    string[][] DHName = Program.DB.getValue(DB.type.ProjDB, "User_DH", "명칭", "번호 = '" + SelectDH_split[0].ToString() + "'");
                    if (DHName.Length > 0)
                    { 내용 = DHName[0][0]; }
                }

                if (MainSystem == "지역난방")
                {
                    MainUserList_textBox.Text = 내용;
                }
                else if (Sub1System == "지역난방")
                {
                    Sub1UserList_textBox.Text = 내용;
                }
                else if (Sub2System == "지역난방")
                {
                    Sub2UserList_textBox.Text = 내용;
                }
                Load_DH_Table();
            }
            else
            {
                내용 = "";
            }
        }

        private void Load_DH_Table()
        {
            DataGridViewCheckBoxColumn DH_checkBoxColumn = new DataGridViewCheckBoxColumn();
            new StackedHeaderDecorator(DH_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            DH_dataGridView.Columns.Clear();
            DH_checkBoxColumn.HeaderText = "선택";
            DH_checkBoxColumn.Name = "check";
            DH_dataGridView.Columns.Add(DH_checkBoxColumn);
            DH_dataGridView.Columns.Add("A1", "번호");
            DH_dataGridView.Columns.Add("A2", "명칭");
            DH_dataGridView.Columns.Add("A3", "용도");
            DH_dataGridView.Columns.Add("A4", "용량.[kW]");
            DH_dataGridView.Columns.Add("A5", "1차온도.공급온도.[℃]");
            DH_dataGridView.Columns.Add("A6", "1차온도.환수온도.[℃]");
            DH_dataGridView.Columns.Add("A7", "2차온도.공급온도.[℃]");
            DH_dataGridView.Columns.Add("A8", "2차온도.환수온도.[℃]");
            DH_dataGridView.Columns[0].Width = 30;

            for (int n = 0; n < SelectDH_split.Count; n++)
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_DH", "번호,명칭,용도,용량,공급온도1차,환수온도1차,공급온도2차,환수온도2차", "번호 = '" + SelectDH_split[n].ToString() + "'");
                if (User_Value.Length > 0)
                {
                    DH_dataGridView.Rows.Add();
                    int nRow = DH_dataGridView.Rows.Count - 1;
                    for (int k = 1; k < 9; k++)
                    {
                        DH_dataGridView.Rows[nRow].Cells[k].Value = User_Value[0][k - 1];
                    }
                }
            }
        }
        #endregion


        #region 히트펌프
        /////////////////////////////////////////////////////히트펌프///////////////////////////////////////////////////////////////////
        private void Load_HPForm()
        {
            String nonsplit;
            nonsplit = SelectHP_nonsplit;
            DHWHP_DB heating_HP = new DHWHP_DB(nonsplit);
            DialogResult result = heating_HP.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (heating_HP.SelectHP != null)
                {
                    SelectHP_nonsplit = heating_HP.SelectHP;

                    Split_HP(heating_HP.SelectHP, "외기 히트펌프");
                }
            }
        }
        private void Split_HP(String nonSplit, String HeatSource)
        {
            String 내용 = "";
            if (nonSplit != null)
            {
                if (nonSplit.Contains('+'))
                {
                    string[] token = nonSplit.Split('+');

                    SelectHP_split.Clear();

                    foreach (var item in token)
                    {
                        SelectHP_split.Add(item.ToString());
                    }
                    string[][] HPName = Program.DB.getValue(DB.type.ProjDB, "User_DHWHP", "명칭", "번호 = '" + SelectHP_split[0].ToString() + "'");
                    if (HPName.Length > 0)
                    {
                        내용 = HPName[0][0] + " 외 " + (SelectHP_split.Count - 1).ToString() + "개";
                    }
                }
                else
                {
                    SelectHP_split.Clear();

                    SelectHP_split.Add(nonSplit);

                    string[][] HPName = Program.DB.getValue(DB.type.ProjDB, "User_DHWHP", "명칭", "번호 = '" + SelectHP_split[0].ToString() + "'");
                    if (HPName.Length > 0)
                    { 내용 = HPName[0][0]; }
                    else { 내용 = ""; }

                }

                if (MainSystem == HeatSource)
                {
                    MainUserList_textBox.Text = 내용;
                }
                else if (Sub1System == HeatSource)
                {
                    Sub1UserList_textBox.Text = 내용;
                }
                else if (Sub2System == HeatSource)
                {
                    Sub2UserList_textBox.Text = 내용;
                }
                if (HP_dataGridView.Columns.Count == 0)
                {
                    create_HP_Table();
                }
                Load_HP_Table();

            }
            else
            {
                내용 = "";
            }
        }
        private void create_HP_Table()
        {
            DataGridViewCheckBoxColumn HP_checkBoxColumn = new DataGridViewCheckBoxColumn();
            new StackedHeaderDecorator(HP_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            HP_dataGridView.Columns.Clear();
            HP_checkBoxColumn.HeaderText = "선택";
            HP_checkBoxColumn.Name = "check";
            HP_dataGridView.Columns.Add(HP_checkBoxColumn);
            HP_dataGridView.Columns.Add("A1", "번호");
            HP_dataGridView.Columns.Add("A2", "명칭");
            HP_dataGridView.Columns.Add("A3", "정격.용량.[kW]");
            HP_dataGridView.Columns.Add("A4", "정격.COP.[kW]");
            HP_dataGridView.Columns.Add("A5", "정격.소비전력.[kW]");
            HP_dataGridView.Columns.Add("A6", "제어방식");
            HP_dataGridView.Columns.Add("A7", "대수.[EA]");
            HP_dataGridView.Columns[0].Width = 30;
            HP_dataGridView.Columns[0].Width = 60;

        }
        private void Load_HP_Table()
        {
            String source;
            source = "외기";
            for (int k = 0; k < SelectHP_split.Count; k++)
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_DHWHP", "번호,명칭,급탕정격용량,급탕정격COP,급탕정격소비전력", "번호='" + SelectHP_split[k] + "'");
                if (User_Value.Length > 0)
                {
                    for (int n = 0; n < User_Value.Length; n++)
                    {
                        HP_dataGridView.Rows.Add();
                        int nRow = HP_dataGridView.Rows.Count - 1;
                        HP_dataGridView.Rows[nRow].Cells[1].Value = User_Value[n][0];
                        HP_dataGridView.Rows[nRow].Cells[2].Value = User_Value[n][1];
                        HP_dataGridView.Rows[nRow].Cells[3].Value = string.Format("{0:F1}", Convert.ToDouble(User_Value[n][2]));
                        HP_dataGridView.Rows[nRow].Cells[4].Value = string.Format("{0:F1}", Convert.ToDouble(User_Value[n][3]));
                        HP_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F1}", Convert.ToDouble(User_Value[n][4]));

                        DataGridViewComboBoxCell 제어방식comboBox = new DataGridViewComboBoxCell();
                        제어방식comboBox.Items.Add("ON/OFF제어");
                        제어방식comboBox.Items.Add("인버터제어");
                        HP_dataGridView.Rows[nRow].Cells[6] = 제어방식comboBox;
                    }
                }
            }
        }
        private void NonSplit_HP()
        {

            for (int k = 0; k < HP_dataGridView.Rows.Count; k++)
            {
                if (k == HP_dataGridView.Rows.Count - 1 && HP_dataGridView.Rows[k].Cells[6].Value != null && HP_dataGridView.Rows[k].Cells[7].Value != null)
                {
                    HPControl_nonsplit += HP_dataGridView.Rows[k].Cells[6].Value.ToString();
                    HPNum_nonsplit += HP_dataGridView.Rows[k].Cells[7].Value.ToString();
                }
                else if (HP_dataGridView.Rows[k].Cells[6].Value != null && HP_dataGridView.Rows[k].Cells[7].Value != null)
                {
                    HPControl_nonsplit += HP_dataGridView.Rows[k].Cells[6].Value.ToString() + "+";
                    HPNum_nonsplit += HP_dataGridView.Rows[k].Cells[7].Value.ToString() + "+";
                }
            }
        }
        private void Split_HPControl(String nonSplit)
        {
            if (nonSplit != null)
            {
                if (nonSplit.Contains('+'))
                {
                    ArrayList HP_split = new ArrayList();

                    string[] token = nonSplit.Split('+');
                    HP_split.Clear();
                    foreach (var item in token)
                    {
                        HP_split.Add(item.ToString());
                    }
                    for (int k = 0; k < HP_dataGridView.Rows.Count; k++)
                    {
                        HP_dataGridView.Rows[k].Cells[6].Value = HP_split[k];
                    }
                }
                else
                {
                    if (HP_dataGridView.Rows.Count > 0)
                    { HP_dataGridView.Rows[0].Cells[6].Value = nonSplit; }
                }
            }
            else { return; }

        }
        private void Split_HPNum(String nonSplit)
        {
            if (nonSplit != null)
            {
                if (nonSplit.Contains('+'))
                {
                    ArrayList HP_split = new ArrayList();

                    string[] token = nonSplit.Split('+');
                    HP_split.Clear();
                    foreach (var item in token)
                    {
                        HP_split.Add(item.ToString());
                    }
                    for (int k = 0; k < HP_dataGridView.Rows.Count; k++)
                    {
                        HP_dataGridView.Rows[k].Cells[7].Value = HP_split[k];
                    }
                }
                else
                {
                    if (HP_dataGridView.Rows.Count > 0)
                    { HP_dataGridView.Rows[0].Cells[7].Value = nonSplit; }
                }
            }
            else { return; }
        }

        #endregion

        #region 저장
        /////////////////////////////////////////////////////저장////////////////////////////////////////////////////////////////////


        private void ChangeIndex_StorageType_comboBox()
        {
            if (MainSystem_comboBox.SelectedItem != null && MainSystem_comboBox.SelectedItem.ToString() == "태양열시스템")
            {
                StorageType_comboBox.Items.Clear();
                StorageType_comboBox.Items.Add("2단 구분 축열탱크");
            }
            else if (Sub1System_comboBox.SelectedItem != null && Sub1System_comboBox.SelectedItem.ToString() == "태양열시스템")
            {
                StorageType_comboBox.Items.Clear();
                StorageType_comboBox.Items.Add("2단 구분 축열탱크");
            }
            else if (Sub2System_comboBox.SelectedItem != null && Sub2System_comboBox.SelectedItem.ToString() == "태양열시스템")
            {
                StorageType_comboBox.Items.Clear();
                StorageType_comboBox.Items.Add("2단 구분 축열탱크");
            }
            else
            {
                StorageType_comboBox.Items.Clear();
                StorageType_comboBox.Items.Add("간접식");
                StorageType_comboBox.Items.Add("전기 직접식");
                StorageType_comboBox.Items.Add("가스 직접식");
            }
        }

        private void StorageUse_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StorageUse_comboBox.SelectedItem != null)
            {
                StorageUse = StorageUse_comboBox.SelectedItem.ToString();
                if (StorageUse == "축열탱크 있음")
                {
                    Vs_label1.Visible = true;
                    Vs_textBox.Visible = true;
                    Vs_label2.Visible = true;
                }
                else
                {
                    Vs_label1.Visible = false;
                    Vs_textBox.Visible = false;
                    Vs_label2.Visible = false;
                }
                StorageImage("신규");
            }
            else
            {
                Vs = 0;
            }
        }

        private void Vs_textBox_TextChanged(object sender, EventArgs e)
        {
            controls.ThousandsSeparator textbox = new controls.ThousandsSeparator(Vs_textBox, false, 3);
            Vs = textbox.text;
        }
        private void StoragePump_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StoragePump_comboBox.SelectedItem != null)
            {
                StoragePumpUse = StoragePump_comboBox.SelectedItem.ToString();
                if (StoragePumpUse == "축열펌프 있음")
                {
                    StoragePump_label.Visible = true;
                    StoragePump_textBox.Visible = true;
                    StoragePump_button.Visible = true;
                    Create_StoragePump_Table();
                }
                else
                {
                    StoragePump_label.Visible = false;
                    StoragePump_textBox.Visible = false;
                    StoragePump_button.Visible = false;
                    StoragePump_dataGridView.Columns.Clear();
                }
            }
            else
            {
                StoragePumpUse = null;
            }
        }

        private void StorageType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StorageType_comboBox.SelectedItem != null)
            {
                StorageType = StorageType_comboBox.SelectedItem.ToString();
            }
            else
            {
                StorageType = null;
            }
        }
        private void Create_StoragePump_Table()
        {
            new StackedHeaderDecorator(StoragePump_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            StoragePump_dataGridView.Columns.Clear();
            StoragePump_dataGridView.Columns.Add("A0", "구분");
            StoragePump_dataGridView.Columns.Add("A1", "펌프번호");
            StoragePump_dataGridView.Columns.Add("A2", "명칭");
            StoragePump_dataGridView.Columns.Add("A3", "종류");
            StoragePump_dataGridView.Columns.Add("A4", "A효율.[%]");
            StoragePump_dataGridView.Columns.Add("A5", "B효율.[%]");
            StoragePump_dataGridView.Columns.Add("A6", "유량.[CMH]");
            StoragePump_dataGridView.Columns.Add("A7", "동력.[kW]");
            StoragePump_dataGridView.Columns.Add("A8", "양정.[m]");
            StoragePump_dataGridView.Columns[0].Width = 70;
            StoragePump_dataGridView.Columns[1].Width = 50;

        }
        private void StoragePump_button_Click(object sender, EventArgs e)
        {
            DHW_PumpDB heating_pump = new DHW_PumpDB(StoragePump);
            DialogResult result = heating_pump.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (heating_pump.SelectPump != null)
                {
                    if (StoragePump_dataGridView.Rows.Count == 0)
                    {
                        StoragePump_dataGridView.Rows.Add();
                    }

                    StoragePump = heating_pump.SelectPump;
                    Load_StoragePump(StoragePump);
                }
            }
        }
        private void Load_StoragePump(String StoragePump)
        {
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "번호,명칭,종류,A효율,B효율,유량,동력,양정", "번호 = '" + StoragePump.ToString() + "'");
            if (Value.Length > 0)
            {
                StoragePump_textBox.Text = Value[0][0];
                string A효율 = "", B효율 = "", 유량 = "", 동력 = "", 양정 = "";
                if (Value[0][3] != null && Value[0][3] != "")
                {
                    A효율 = string.Format("{0:F1}", Convert.ToDouble(Value[0][3]));
                }
                if (Value[0][4] != null && Value[0][4] != "")
                {
                    B효율 = string.Format("{0:F1}", Convert.ToDouble(Value[0][4]));
                }
                if (Value[0][5] != null && Value[0][5] != "")
                {
                    유량 = string.Format("{0:F0}", Convert.ToDouble(Value[0][5]));
                }
                if (Value[0][6] != null && Value[0][6] != "")
                {
                    동력 = string.Format("{0:F0}", Convert.ToDouble(Value[0][6]));
                }
                if (Value[0][7] != null && Value[0][7] != "")
                {
                    양정 = string.Format("{0:F0}", Convert.ToDouble(Value[0][7]));
                }
                StoragePump_dataGridView.Rows[0].Cells[0].Value = "축열펌프";
                StoragePump_dataGridView.Rows[0].Cells[1].Value = Value[0][0];
                StoragePump_dataGridView.Rows[0].Cells[2].Value = Value[0][1];
                StoragePump_dataGridView.Rows[0].Cells[3].Value = Value[0][2];
                StoragePump_dataGridView.Rows[0].Cells[4].Value = A효율;
                StoragePump_dataGridView.Rows[0].Cells[5].Value = B효율;
                StoragePump_dataGridView.Rows[0].Cells[6].Value = 유량;
                StoragePump_dataGridView.Rows[0].Cells[7].Value = 동력;
                StoragePump_dataGridView.Rows[0].Cells[8].Value = 양정;
            }
        }

        #endregion

        #region 분배
        /////////////////////////////////////////////////////분배////////////////////////////////////////////////////////////////////
        ///
        private void PipeD_textBox_TextChanged(object sender, EventArgs e)
        {
            controls.ThousandsSeparator textbox = new controls.ThousandsSeparator(PipeD_textBox, false, 1);
            PipeD = textbox.text;
        }
        private void PipeInsD_textBox_TextChanged(object sender, EventArgs e)
        {
            controls.ThousandsSeparator textbox = new controls.ThousandsSeparator(PipeInsD_textBox, false, 1);
            PipeInsD = textbox.text;
        }
        private void Calc_Pipe()
        {
            if (SelectZone_split.Count > 0)
            {
                ZoneArea = 0;
                for (int n = 0; n < SelectZone_split.Count; n++)
                {
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "존번호 = '" + SelectZone_split[n].ToString() + "'");
                    if (Value.Length > 0)
                    {
                        ZoneArea += Convert.ToDouble(Value[0][0]);
                    }
                }
                string[][] Value_PipeIns = Program.DB.getValue(DB.type.BaseDB_Heating, "배관단열", "순바닥면적,배관관경,단열두께,열전도율", "");
                if (Value_PipeIns.Length > 0)
                {
                    if (ZoneArea < Convert.ToDouble(Value_PipeIns[0][0]))
                    {
                        PipeD = Convert.ToDouble(Value_PipeIns[0][1]);
                        PipeInsD = Convert.ToDouble(Value_PipeIns[0][2]);
                    }
                    else if (ZoneArea < Convert.ToDouble(Value_PipeIns[1][0]))
                    {
                        PipeD = Convert.ToDouble(Value_PipeIns[1][1]);
                        PipeInsD = Convert.ToDouble(Value_PipeIns[1][2]);
                    }
                    else if (ZoneArea < Convert.ToDouble(Value_PipeIns[2][0]))
                    {
                        PipeD = Convert.ToDouble(Value_PipeIns[2][1]);
                        PipeInsD = Convert.ToDouble(Value_PipeIns[2][2]);
                    }
                    else if (ZoneArea < Convert.ToDouble(Value_PipeIns[3][0]))
                    {
                        PipeD = Convert.ToDouble(Value_PipeIns[3][1]);
                        PipeInsD = Convert.ToDouble(Value_PipeIns[3][2]);
                    }
                    else
                    {
                        PipeD = Convert.ToDouble(Value_PipeIns[4][1]);
                        PipeInsD = Convert.ToDouble(Value_PipeIns[4][2]);
                    }
                    PipeD_textBox.Text = PipeD.ToString();
                    controls.ThousandsSeparator textbox = new controls.ThousandsSeparator(PipeD_textBox, true, 1);

                    PipeInsD_textBox.Text = PipeInsD.ToString();
                    controls.ThousandsSeparator textbox1 = new controls.ThousandsSeparator(PipeInsD_textBox, true, 1);

                    PipeIns_Ramda = 0.035;
                    PipeIns_Ramda_textBox.Text = PipeIns_Ramda.ToString();
                    controls.ThousandsSeparator textbox2 = new controls.ThousandsSeparator(PipeIns_Ramda_textBox, true, 3);

                    PipeIns_textBox.Text = "일반 보온재";
                }
            }

        }

        private void PipeIns_button_Click(object sender, EventArgs e)
        {
            CW_PanelDB InsDB_form = new CW_PanelDB();
            DialogResult result = InsDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                PipeIns = InsDB_form.Select_CWPanel[1];
                PipeIns_textBox.Text = PipeIns;

                PipeIns_Ramda = Convert.ToDouble(InsDB_form.Select_CWPanel[4]);
                PipeIns_Ramda_textBox.Text = PipeIns_Ramda.ToString();
                controls.ThousandsSeparator textbox1 = new controls.ThousandsSeparator(PipeIns_Ramda_textBox, true, 3);
            }
        }
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
            if (Pump_dataGridView.Rows.Count == 0)
            {
                Pump_dataGridView.Rows.Add();
            }
            DHW_PumpDB heating_pump = new DHW_PumpDB(Pump1);
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
            DHW_PumpDB heating_pump = new DHW_PumpDB(Pump2);
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

        #region 이미지
        ///////////////////////////////////////그림 넣기///////////////////////////////////////////////////////////



        private void LoadImage() // 1.분배설비 그림넣기
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_Heating, "난방설비이미지", "이미지", "항목유형 = '분배설비'");
            if (Image.Length > 0)
            {
                DistpictureBox.Size = new System.Drawing.Size(610, 254);
                DistpictureBox.Location = new Point(0, 25);
                DistpictureBox.Load(Program.gPath + Image[0][0]);
                DistpictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }


        private void MainSystemImage(string type, string install)//2.냉방설비 그림
        {

            string[][] image1 = Program.DB.getValue(DB.type.BaseDB_Heating, "난방설비이미지", "이미지",
                "설비유형='" + type + "' And 설치유형='" + install + "'");
            if (image1.Length > 0)
            {
                SyspictureBox.Size = new System.Drawing.Size(110, 170);
                SyspictureBox.Location = new Point(0, 90);
                SyspictureBox.Load(Program.gPath + image1[0][0]);
                SyspictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void StorageImage(string install)//2.난방설비 그림
        {
            if (StorageUse == "축열탱크 없음")
            {
                StopictureBox.Visible = false;
            }
            else
            {
                string[][] stoimage = Program.DB.getValue(DB.type.BaseDB_Heating, "난방설비이미지", "이미지", "항목유형='저장설비' And 설치유형='" + install + "'");
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
        private void HeatSourceImage(string HeatSource, string Install_f)  // 지열 그림
        {
            if (HeatSource == "지열" || HeatSource == "지역난방")
            {
                SourcepictureBox.Visible = true;
            }
            else
            {
                SourcepictureBox.Visible = false;
            }
            string[][] image = Program.DB.getValue(DB.type.BaseDB_Heating, "난방설비이미지", "이미지", "항목유형 = '열원' AND 설비유형='" + HeatSource + "' And 설치유형='" + Install_f + "'");
            if (image.Length > 0)
            {
                SourcepictureBox.Size = new System.Drawing.Size(250, 200);
                SourcepictureBox.Location = new Point(0, 60);
                SourcepictureBox.Load(Program.gPath + image[0][0]);
                SourcepictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

        }

        #endregion

        #region 세이브
        private void Save_button_Click(object sender, EventArgs e)
        {
            if (Name == null)
            {
                MessageBox.Show("난방시스템 명칭을 입력하세요.");
            }
            else
            {
                Save();
            }

        }
        private void Save()
        {
            BoilerNum_nonsplit = "";
            SolarNum_nonsplit = ""; SolarDirection_nonsplit = ""; SolarDegree_nonsplit = "";
            SelectDH_nonsplit = "";
            HPNum_nonsplit = ""; HPControl_nonsplit = "";
            NonSplit_BoilerNum();
            NonSplit_Solar();
            NonSplit_HP();
            Save_Pump();

            Program.DB.setValue(DB.type.ProjDB, "DHWSystem_Form", "번호,프로젝트유형,명칭,존", "'" + Num_textBox.Text + "','" + 프로젝트유형[0][0] + "','" + Name + "','" + SelectZone_nonsplit + "'", "번호");
            Program.DB.setValue(DB.type.ProjDB, "DHWSystem_Form", "번호,프로젝트유형,설치위치,공급환수온도,복합설비유무,주요설비,보조설비1,보조설비2", "'" + Num_textBox.Text + "','" + 프로젝트유형[0][0] + "','" + SystemLoacation + "','" + SLRL + "','" + Complex + "','" + MainSystem + "','" + Sub1System + "','" + Sub2System + "'", "번호");
            Program.DB.setValue(DB.type.ProjDB, "DHWSystem_Form", "번호,프로젝트유형,보일러종류,보일러대수", "'" + Num_textBox.Text + "','" + 프로젝트유형[0][0] + "','" + SelectBoiler_nonsplit + "','" + BoilerNum_nonsplit + "'", "번호");
            Program.DB.setValue(DB.type.ProjDB, "DHWSystem_Form", "번호,프로젝트유형,태양열번호,모듈개수,모듈방위,모듈기울기", "'" + Num_textBox.Text + "','" + 프로젝트유형[0][0] + "','" + SelectSolar_nonsplit + "','" + SolarNum_nonsplit + "','" + SolarDirection_nonsplit + "','" + SolarDegree_nonsplit + "'", "번호");
            Program.DB.setValue(DB.type.ProjDB, "DHWSystem_Form", "번호,프로젝트유형,지역난방번호", "'" + Num_textBox.Text + "','" + 프로젝트유형[0][0] + "','" + SelectDH_nonsplit + "'", "번호");
            Program.DB.setValue(DB.type.ProjDB, "DHWSystem_Form", "번호,프로젝트유형,펌프유무,펌프방식,펌프1종류,펌프2종류,펌프1밸브,펌프2밸브,펌프1제어,펌프2제어,펌프1대수,펌프2대수", "'" + Num_textBox.Text + "','" + 프로젝트유형[0][0] + "','" + PumpUse + "','" + PumpMethod + "','" + Pump1 + "','" + Pump2 + "','" + Pump1Valve + "','" + Pump2Valve + "','" + Pump1Control + "','" + Pump2Control + "','" + Pump1Num.ToString() + "','" + Pump2Num.ToString() + "'", "번호");
            Program.DB.setValue(DB.type.ProjDB, "DHWSystem_Form", "번호,프로젝트유형,축열유무,축열펌프유무,축열펌프,축열용량,축열유형", "'" + Num_textBox.Text + "','" + 프로젝트유형[0][0] + "','" + StorageUse + "','" + StoragePumpUse + "','" + StoragePump + "','" + Vs.ToString() + "','" + StorageType + "'", "번호");
            Program.DB.setValue(DB.type.ProjDB, "DHWSystem_Form", "번호,프로젝트유형,배관관경,배관보온두께,보온열전도율,배관보온재", "'" + Num_textBox.Text + "','" + 프로젝트유형[0][0] + "','" + PipeD.ToString() + "','" + PipeInsD.ToString() + "','" + PipeIns_Ramda.ToString() + "','" + PipeIns + "'", "번호");
            Program.DB.setValue(DB.type.ProjDB, "DHWSystem_Form", "번호,프로젝트유형,히트펌프번호,히트펌프제어방식,히트펌프대수", "'" + Num_textBox.Text + "','" + 프로젝트유형[0][0] + "','" + SelectHP_nonsplit + "','" + HPControl_nonsplit + "','" + HPNum_nonsplit + "'", "번호");


            this.DialogResult = DialogResult.OK;
            this.Hide();
            Program.getMenuForm().DoLoadForm(49, OnLoadListProc);
        }

        public static bool OnLoadListProc(Form form)
        {
            List_DHWSystem f = (List_DHWSystem)form;
            f.load_List();
            return true;
        }

        private void reset()
        {
            Num = null; Name = null; SelectZone_nonsplit = null;
            SystemLoacation = null; SLRL = null; Complex = null; MainSystem = null; Sub1System = null; Sub2System = null;
            SelectBoiler_nonsplit = null; BoilerNum_nonsplit = null;
            SelectSolar_nonsplit = null; SolarNum_nonsplit = null; SolarDirection_nonsplit = null; SolarDegree_nonsplit = null;
            PumpUse = null; PumpMethod = null; Pump1 = null; Pump2 = null; Pump1Valve = null; Pump2Valve = null; Pump1Control = null; Pump2Control = null;
            Pump1Num = 0; Pump2Num = 0;
            StorageUse = null; StoragePumpUse = null; StoragePump = null; Vs = 0;
            SelectZone_split.Clear(); SelectBoiler_split.Clear();
            PipeD = 0; PipeInsD = 0; PipeIns_Ramda = 0; PipeIns = null;
            ZoneArea = 0;

            Num_textBox.Text = null;
            Name_textBox.Text = null;
            Zone_textBox.Text = null;

            SystemLoacation_comboBox.SelectedItem = null;
            SLRL_comboBox.SelectedItem = null;
            Complex_comboBox.SelectedItem = null;
            MainSystem_comboBox.SelectedItem = null;
            MainUserList_textBox.Text = null;
            Sub1System_comboBox.SelectedItem = null;
            Sub1UserList_textBox.Text = null;
            Sub2System_comboBox.SelectedItem = null;
            Sub2UserList_textBox.Text = null;

            Boiler_dataGridView.Columns.Clear();
            Boiler_dataGridView.Rows.Clear();

            Solar_dataGridView.Columns.Clear();
            Solar_dataGridView.Rows.Clear();

            DH_dataGridView.Columns.Clear();
            DH_dataGridView.Rows.Clear();

            HP_dataGridView.Columns.Clear();
            HP_dataGridView.Rows.Clear();

            PumpUse_comboBox.SelectedItem = null;
            PumpMethod_comboBox.SelectedItem = null;
            Pump1_textBox.Text = null;
            Pump2_textBox.Text = null;
            Pump_dataGridView.Columns.Clear();
            Pump_dataGridView.Rows.Clear();


            StorageUse_comboBox.SelectedItem = null;
            Vs_textBox.Text = null;
            StoragePump_comboBox.SelectedItem = null;
            StoragePump_textBox.Text = null;
            StoragePump_dataGridView.Columns.Clear();
            StoragePump_dataGridView.Rows.Clear();

            PipeD_textBox.Text = null;
            PipeInsD_textBox.Text = null;
            PipeIns_Ramda_textBox.Text = null;
            PipeIns_textBox.Text = null;
        }

        #endregion

        #region 로드
        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();

            Num_textBox.Text = ID;
            Num = ID;


            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "명칭,존", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {
                Name_textBox.Text = Value[0][0];
                Name = Value[0][0];

                SelectZone_nonsplit = Value[0][1];
                Split_Zone(SelectZone_nonsplit);
                ce_Pic();
            }
            Value = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "설치위치,공급환수온도,복합설비유무,주요설비,보조설비1,보조설비2", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {
                SystemLoacation_comboBox.SelectedItem = Value[0][0];
                SystemLoacation = Value[0][0];

                SLRL_comboBox.SelectedItem = Value[0][1];
                SLRL = Value[0][1];

                Complex_comboBox.SelectedItem = Value[0][2];
                Complex = Value[0][2];

                MainSystem_comboBox.SelectedItem = Value[0][3];
                MainSystem = Value[0][3];
                Sub1System_comboBox.SelectedItem = Value[0][4];
                Sub1System = Value[0][4];
                Sub2System_comboBox.SelectedItem = Value[0][5];
                Sub2System = Value[0][5];
            }
            Value = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "보일러종류,보일러대수", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {
                SelectBoiler_nonsplit = Value[0][0];
                Split_Boiler(SelectBoiler_nonsplit);

                BoilerNum_nonsplit = Value[0][1];
                Split_BoilerNum(BoilerNum_nonsplit);
            }
            Value = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "태양열번호,모듈개수,모듈방위,모듈기울기", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {
                SelectSolar_nonsplit = Value[0][0];
                Split_Solar(SelectSolar_nonsplit);

                SolarNum_nonsplit = Value[0][1];
                Split_SolarNum(SolarNum_nonsplit);

                SolarDirection_nonsplit = Value[0][2];
                Split_SolarDirection(SolarDirection_nonsplit);

                SolarDegree_nonsplit = Value[0][3];
                Split_SolarDegree(SolarDegree_nonsplit);
            }
            Value = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "지역난방번호", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {
                SelectDH_nonsplit = Value[0][0];
                Split_DH(SelectDH_nonsplit);
            }

            Value = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "히트펌프번호,히트펌프제어방식,히트펌프대수", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {
                SelectHP_nonsplit = Value[0][0];
                Split_HP(SelectHP_nonsplit, "외기 히트펌프");

                HPControl_nonsplit = Value[0][1];
                Split_HPControl(HPControl_nonsplit);

                HPNum_nonsplit = Value[0][2];
                Split_HPNum(HPNum_nonsplit);
            }

            Value = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "펌프유무,펌프방식,펌프1종류,펌프2종류,펌프1밸브,펌프2밸브,펌프1제어,펌프2제어,펌프1대수,펌프2대수", "번호 = '" + ID + "'");
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
                        if (Pump_Value.Length > 0)
                        {
                            Pump1_textBox.Text = Pump_Value[0][0];
                            if (Pump_dataGridView.Rows.Count == 0)
                            {
                                Pump_dataGridView.Rows.Add();
                            }
                            Load_Pump_Table(0, Pump1);
                        }
                    }

                    Pump2 = Value[0][3];
                    if (Pump2 != null && Pump2 != "")
                    {
                        string[][] Pump_Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "명칭", "번호 = '" + Pump2.ToString() + "'");
                        if (Pump_Value.Length > 0)
                        {
                            Pump2_textBox.Text = Pump_Value[0][0];
                            if (Pump_dataGridView.Rows.Count == 1)
                            {
                                Pump_dataGridView.Rows.Add();
                            }
                            Load_Pump_Table(1, Pump2);
                        }
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

            Value = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "축열유무,축열펌프유무,축열펌프,축열용량", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {
                StorageUse = Value[0][0];
                StorageUse_comboBox.SelectedItem = StorageUse;

                StoragePumpUse = Value[0][1];
                StoragePump_comboBox.SelectedItem = StoragePumpUse;
                if (StoragePumpUse == "축열펌프 없음")
                {
                    StoragePump_dataGridView.Visible = false;
                }
                else
                {
                    StoragePump_dataGridView.Visible = true;
                    StoragePump = Value[0][2];

                    if (StoragePump_dataGridView.Rows.Count == 0)
                    {
                        StoragePump_dataGridView.Rows.Add();
                    }
                    Load_StoragePump(StoragePump);
                }


                if (Value[0][3] != null && Value[0][3] != "")
                {
                    Vs = Convert.ToDouble(Value[0][3]);
                    Vs_textBox.Text = Vs.ToString();
                    controls.ThousandsSeparator textbox1 = new controls.ThousandsSeparator(Vs_textBox, true, 3);
                }
            }
            Value = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "배관관경,배관보온두께,보온열전도율,배관보온재", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {
                PipeD = Convert.ToDouble(Value[0][0]);
                PipeD_textBox.Text = PipeD.ToString();
                controls.ThousandsSeparator textbox1 = new controls.ThousandsSeparator(PipeD_textBox, true, 1);

                PipeInsD = Convert.ToDouble(Value[0][1]);
                PipeInsD_textBox.Text = PipeInsD.ToString();
                controls.ThousandsSeparator textbox2 = new controls.ThousandsSeparator(PipeInsD_textBox, true, 1);

                PipeInsD = Convert.ToDouble(Value[0][2]);
                PipeIns_Ramda_textBox.Text = PipeInsD.ToString();
                controls.ThousandsSeparator textbox3 = new controls.ThousandsSeparator(PipeIns_Ramda_textBox, true, 3);

                PipeIns = Value[0][3];
                PipeIns_textBox.Text = PipeIns;
            }
        }

        #endregion

        #region 리셋 
        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            Num_textBox.Text = ID;
            Num = ID;
        }

        #endregion

    }
}
