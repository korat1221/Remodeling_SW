using main.contentslist;
using main.subcontents.ConstructionCW;
using main.subcontents.HeatingSystem;
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
using static System.Net.Mime.MediaTypeNames;

namespace main.contents
{
    public partial class HeatingSystem : Form
    {
        String Num, Name; String SelectZone_nonsplit;
        String SystemLoacation, SLRL, Complex, MainSystem, Sub1System, Sub2System;
        String SelectBoiler_nonsplit, BoilerNum_nonsplit;
        String PumpUse, PumpMethod, Pump1, Pump2, Pump1Valve, Pump2Valve, Pump1Control, Pump2Control; int Pump1Num, Pump2Num;
        String ce1Type, ce2Type; int ce_SelectRow;
        String StorageUse, StoragePumpUse, StoragePump; double Vs;
        String[] SystemType = { "보일러", "히트펌프", "흡수식온수기", "지역난방", "태양열시스템" };
        String[] ceType = { "실내기", "방열기", "팬코일유닛", "파워팬유닛", "복사난방" };
        double PipeD, PipeInsD, PipeIns_Ramda;
        String PipeIns;
        double ZoneArea;
        ArrayList SelectZone_split = new ArrayList(); ArrayList SelectBoiler_split = new ArrayList();

        public HeatingSystem()
        {
            InitializeComponent();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '난방시스템'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Load(Program.gPath + "images/HeatingSystem/BoilerSystem.png");
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;


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

            //공급설비 콤보박스
            ce1Type_comboBox.Items.Clear();
            ce2Type_comboBox.Items.Clear();
            for (int i = 0; i < ceType.Length; i++)
            {
                ce1Type_comboBox.Items.Add(ceType[i]);
                ce2Type_comboBox.Items.Add(ceType[i]);
            }
        }
        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }
        private void Name_textBox_TextChanged(object sender, EventArgs e)
        {
            if (Name_textBox.Text != null)
            {
                Name = Name_textBox.Text.ToString();
            }
        }

        private void Zone_button_Click(object sender, EventArgs e)
        {
            Heating_Zone heatingzone = new Heating_Zone(Num, SelectZone_nonsplit);
            DialogResult result = heatingzone.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    if (heatingzone.SelectZone != null)
                    {
                        SelectZone_nonsplit = heatingzone.SelectZone;
                        Split_Zone(heatingzone.SelectZone);
                        Calc_Pipe();
                    }
                }
                catch { }
            }
        }
        private void Split_Zone(String nonSplit)
        {
            String 내용;
            if (nonSplit != null)
            {
                if (nonSplit.Contains(","))
                {
                    string[] token = nonSplit.Split(',');
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
                    SelectZone_split.Add(SelectZone_split);
                    내용 = SelectZone_split[0].ToString();
                }
                Zone_textBox.Text = 내용;
            }
            else { 내용 = ""; }

        }
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
                LoadtabPage(MainSystem);
                if (MainSystem == Sub1System)
                {
                    MessageBox.Show("이미 Sub1설비로 선택되어 있습니다. 다른 설비를 선택하세요.");
                }
                else if (MainSystem == Sub2System)
                {
                    MessageBox.Show("이미 Sub2설비로 선택되어 있습니다. 다른 설비를 선택하세요.");
                }
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
        }

        private void Sub1UserList_button_Click(object sender, EventArgs e)
        {
            if (Sub1System == "보일러")
            {
                Load_BoilerForm();
            }
        }

        private void Sub2UserList_button_Click(object sender, EventArgs e)
        {
            if (Sub2System == "보일러")
            {
                Load_BoilerForm();
            }
        }

        private void LoadtabPage(String System)
        {
            if (System == "보일러")
            {
                tabControl2.SelectedTab = tabControl2.TabPages["Boiler_tabPage"];
            }
            else if (System == "히트펌프")
            {
                tabControl2.SelectedTab = tabControl2.TabPages["HP_tabPage"];
            }
            else if (System == "흡수식온수기")
            {
                tabControl2.SelectedTab = tabControl2.TabPages["AS_tabPage"];
            }
            else if (System == "지역난방")
            {
                tabControl2.SelectedTab = tabControl2.TabPages["DH_tabPage"];
            }
            else if (System == "태양열시스템")
            {
                tabControl2.SelectedTab = tabControl2.TabPages["Solar_tabPage"];
            }
        }


        /////////////////////////////////////////////////////보일러////////////////////////////////////////////////////////////////////
        private void Load_BoilerForm()
        {
            // Heating_Boiler heating_Boiler = new Heating_Boiler(this);
            Heating_Boiler heating_Boiler = new Heating_Boiler("장비일람표 적용", SelectBoiler_nonsplit);
            DialogResult result = heating_Boiler.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    if (heating_Boiler.SelectBoiler != null)
                    {
                        SelectBoiler_nonsplit = heating_Boiler.SelectBoiler;
                        Split_Boiler(heating_Boiler.SelectBoiler);
                    }
                }
                catch { }
            }
        }

        private void Split_Boiler(String nonSplit)
        {
            String 내용;
            if (nonSplit != null)
            {
                if (nonSplit.Contains(','))
                {
                    string[] token = nonSplit.Split(',');
                    SelectBoiler_split.Clear();
                    foreach (var item in token)
                    {
                        SelectBoiler_split.Add(item.ToString());
                    }

                    string[][] BoilerName = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "명칭", "번호 = '" + SelectBoiler_split[0].ToString() + "'");
                    내용 = BoilerName[0][0] + " 외 " + (SelectBoiler_split.Count - 1).ToString() + "개";
                }
                else
                {
                    SelectBoiler_split.Clear();
                    SelectBoiler_split.Add(nonSplit);
                    string[][] BoilerName = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "명칭", "번호 = '" + SelectBoiler_split[0].ToString() + "'");
                    내용 = BoilerName[0][0];
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
            try
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
            catch { }
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
                        BoilerNum_nonsplit += Boiler_dataGridView.Rows[k].Cells[10].Value.ToString() + ",";
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
                if (nonSplit.Contains(','))
                {
                    ArrayList BoilerNum_split = new ArrayList();

                    string[] token = nonSplit.Split(',');
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
                    Boiler_dataGridView.Rows[0].Cells[10].Value = nonSplit;
                }
            }
            else { return; }

        }
        /////////////////////////////////////////////////////저장////////////////////////////////////////////////////////////////////
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
            }
            else
            {
                Vs = 0;
            }
        }

        private void Vs_textBox_TextChanged(object sender, EventArgs e)
        {
            if (Vs_textBox.Text != null && Vs_textBox.Text != "")
            { Vs = Convert.ToDouble(Vs_textBox.Text.ToString()); }
            else { Vs = 0; }
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
            Heating_Pump heating_pump = new Heating_Pump(StoragePump);
            DialogResult result = heating_pump.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
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
                catch { }
            }
        }
        private void Load_StoragePump(String StoragePump)
        {
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "번호,명칭,종류,A효율,B효율,유량,동력,양정", "번호 = '" + StoragePump.ToString() + "'");
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

        /////////////////////////////////////////////////////분배////////////////////////////////////////////////////////////////////
        ///
        private void PipeD_textBox_TextChanged(object sender, EventArgs e)
        {
            if (PipeD_textBox.Text != null)
            {
                PipeD = Convert.ToDouble(PipeD_textBox.Text);
            }
        }
        private void PipeInsD_textBox_TextChanged(object sender, EventArgs e)
        {
            if (PipeInsD_textBox.Text != null)
            {
                PipeInsD = Convert.ToDouble(PipeInsD_textBox.Text);
            }
        }
        private void Calc_Pipe()
        {
            if (SelectZone_split.Count > 0)
            {
                ZoneArea = 0;
                for (int n = 0; n < SelectZone_split.Count; n++)
                {
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "존번호 = '" + SelectZone_split[n].ToString() + "'");
                    ZoneArea += Convert.ToDouble(Value[0][0]);
                }
                string[][] Value_PipeIns = Program.DB.getValue(DB.type.BaseDB_Heating, "배관단열", "순바닥면적,배관관경,단열두께,열전도율", "");

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
                PipeInsD_textBox.Text = PipeInsD.ToString();
                PipeIns_Ramda = 0.035;
                PipeIns_Ramda_textBox.Text = PipeIns_Ramda.ToString();
                PipeIns_textBox.Text = "일반 보온재";
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
            Pump_dataGridView.Columns.Add("A7", "동력.[kW]");
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
        /////////////////////////////////////////////////////공급////////////////////////////////////////////////////////////////////
        private void ce1Type_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ce1Type_comboBox.SelectedItem != null)
            {
                ce1Type = ce1Type_comboBox.SelectedItem.ToString();
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
            // ce_dataGridView.Columns.Add("A3", "일람표 번호");
            ce_dataGridView.Columns.Add("A3", "일람표 명칭");
            ce_dataGridView.Columns.Add("A4", "용량.[kW]");
            ce_dataGridView.Columns.Add("A5", "소비전력.[kW]");
            // ce_dataGridView.Columns.Add("A7", "적용 존.존번호");
            ce_dataGridView.Columns.Add("A6", "적용 존.존명칭");
            ce_dataGridView.Columns.Add("A7", "적용 존.설치위치");
            ce_dataGridView.Columns[0].Width = 30;
            ce_dataGridView.Columns[1].Width = 150;
            ce_dataGridView.Columns[2].Width = 120;
            ce_dataGridView.Columns[3].Width = 130;
            ce_dataGridView.Columns[4].Width = 70;
            ce_dataGridView.Columns[5].Width = 70;
            ce_dataGridView.Columns[6].Width = 130;

        }
        private void ce1Zone_button_Click(object sender, EventArgs e)
        {
            if (ce_dataGridView.Columns.Count == 0)
            {
                Create_ce_Table();
            }
            Heating_ceZone ceZone = new Heating_ceZone(Num, SelectZone_nonsplit, ce1Type);
            DialogResult result = ceZone.ShowDialog();
            if (result == DialogResult.OK)
            {
                Load_ce(ce1Type);
                Load_ce1Zone(ce1Type);
            }
        }
        private void Load_ce1Zone(String ce1Type)
        {
            String[][] Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Heating_ce_Form", "존번호", "난방시스템 = '" + Num + "' And 공급설비종류 = '" + ce1Type + "'");
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
        private void ce2Zone_button_Click(object sender, EventArgs e)
        {
            Heating_ceZone ceZone = new Heating_ceZone(Num, SelectZone_nonsplit, ce2Type);
            DialogResult result = ceZone.ShowDialog();
            if (result == DialogResult.OK)
            {
                Load_ce(ce2Type);
                Load_ce2Zone(ce2Type);
            }
        }
        private void Load_ce2Zone(String ce2Type)
        {
            String[][] Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Heating_ce_Form", "존번호", "난방시스템 = '" + Num + "' And 공급설비종류 = '" + ce2Type + "'");
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
                Program.DB.deleteValue(DB.type.ProjDB, "Heating_ce_Form", "존번호 ='" + substring2 + "' AND 공급설비 = '" + substring + "' AND 난방시스템 = '" + Num + "'");
                ce_dataGridView.Rows.Remove(ce_dataGridView.Rows[ce_SelectRow]);
            }
        }
        private void Load_ce(string CE)
        {
            try
            {
                String[][] Value = Program.DB.getValue(DB.type.ProjDB, "Heating_ce_Form", "존번호,공급설비종류,공급설비", "난방시스템 = '" + Num + "' And 공급설비종류 = '" + CE + "'");

                int Sum = 1;
                for (int n = 0; n < Value.Length; n++)
                {
                    int nRow = ce_dataGridView.Rows.Add();
                    if (CE != "복사난방")
                    {
                        DataGridViewComboBoxCell 설치위치comboBox = new DataGridViewComboBoxCell();
                        설치위치comboBox.Items.Add("내벽 설치");
                        설치위치comboBox.Items.Add("외벽 설치");
                        설치위치comboBox.Items.Add("창호측 설치");
                        설치위치comboBox.Items.Add("창호측 설치");
                        ce_dataGridView.Rows[nRow].Cells[7] = 설치위치comboBox;
                    }
                    else
                    {
                        DataGridViewComboBoxCell 설치위치comboBox = new DataGridViewComboBoxCell();
                        설치위치comboBox.Items.Add("습식바닥");
                        설치위치comboBox.Items.Add("건식바닥");
                        설치위치comboBox.Items.Add("벽체");
                        설치위치comboBox.Items.Add("천장");
                        ce_dataGridView.Rows[nRow].Cells[7] = 설치위치comboBox;
                    }


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
            Program.DB.deleteValue(DB.type.ProjDB, "Heating_ce_Form", "난방시스템 = '" + Num + "'");

            for (int n = 0; n < ce_dataGridView.Rows.Count; n++)
            {
                String 존번호, 공급설비;
                int index = ce_dataGridView.Rows[n].Cells[1].Value.ToString().IndexOf("CE");
                존번호 = ce_dataGridView.Rows[n].Cells[1].Value.ToString().Substring(0, index - 1);
                공급설비 = ce_dataGridView.Rows[n].Cells[1].Value.ToString().Substring(index, ce_dataGridView.Rows[n].Cells[1].Value.ToString().Length - index);
                Program.DB.setValue(DB.type.ProjDB, "Heating_ce_Form", "존번호,난방시스템,공급설비종류,공급설비,설치위치"
                , "'" + 존번호 + "','" + Num + "','" + ce_dataGridView.Rows[n].Cells[2].Value + "','" +
                    공급설비 + "','" + ce_dataGridView.Rows[n].Cells[7].Value + "'", "");
            }
        }




















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
            NonSplit_BoilerNum();
            Save_Pump();
            Save_ce();
            Program.DB.setValue(DB.type.ProjDB, "HeatingSystem_Form", "번호,명칭,존," +
                "설치위치,공급환수온도,복합설비유무,주요설비,보조설비1,보조설비2," +
                "보일러종류,보일러대수," +
                "펌프유무,펌프방식,펌프1종류,펌프2종류,펌프1밸브,펌프2밸브,펌프1제어,펌프2제어,펌프1대수,펌프2대수," +
                "공급설비1종류,공급설비2종류," +
                "축열유무,축열펌프유무,축열펌프,축열용량"
            , "'" + Num_textBox.Text + "','" + Name + "','" + SelectZone_nonsplit + "','" +
                SystemLoacation + "','" + SLRL + "','" + Complex + "','" + MainSystem + "','" + Sub1System + "','" + Sub2System + "','" +
                SelectBoiler_nonsplit + "','" + BoilerNum_nonsplit + "','" +
                PumpUse + "','" + PumpMethod + "','" + Pump1 + "','" + Pump2 + "','" + Pump1Valve + "','" + Pump2Valve + "','" + Pump1Control + "','" + Pump2Control + "','" + Pump1Num.ToString() + "','" + Pump2Num.ToString() + "','" +
                ce1Type + "','" + ce2Type + "','" +
                StorageUse + "','" + StoragePumpUse + "','" + StoragePump + "','" + Vs.ToString() + "'", "번호");

            Program.DB.setValue(DB.type.ProjDB, "HeatingSystem_Form", "번호,배관관경,배관보온두께,보온열전도율,배관보온재"
            , "'" + Num_textBox.Text + "','" + PipeD.ToString() + "','" + PipeInsD.ToString() + "','" + PipeIns_Ramda.ToString() + PipeIns + "'", "번호");

            this.DialogResult = DialogResult.OK;
            this.Hide();
            Program.getMenuForm().DoLoadForm(39, OnLoadListProc);
        }

        public static bool OnLoadListProc(Form form)
        {
            List_HeatingSystem f = (List_HeatingSystem)form;
            f.load_List();
            return true;
        }

        private void reset()
        {
            Num = null; Name = null; SelectZone_nonsplit = null;
            SystemLoacation = null; SLRL = null; Complex = null; MainSystem = null; Sub1System = null; Sub2System = null;
            SelectBoiler_nonsplit = null; BoilerNum_nonsplit = null;
            PumpUse = null; PumpMethod = null; Pump1 = null; Pump2 = null; Pump1Valve = null; Pump2Valve = null; Pump1Control = null; Pump2Control = null;
            Pump1Num = 0; Pump2Num = 0;
            ce1Type = null; ce2Type = null; ce_SelectRow = 0;
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

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();

            Num_textBox.Text = ID;
            Num = ID;

            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "명칭,존", "번호 = '" + ID + "'");

                Name_textBox.Text = Value[0][0];
                Name = Value[0][0];

                SelectZone_nonsplit = Value[0][1];
                Split_Zone(SelectZone_nonsplit);
            }
            catch { }
            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "설치위치,공급환수온도,복합설비유무,주요설비,보조설비1,보조설비2", "번호 = '" + ID + "'");

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
            catch { }

            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "보일러종류,보일러대수", "번호 = '" + ID + "'");
                SelectBoiler_nonsplit = Value[0][0];
                Split_Boiler(SelectBoiler_nonsplit);

                BoilerNum_nonsplit = Value[0][1];
                Split_BoilerNum(BoilerNum_nonsplit);
            }
            catch { }

            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "펌프유무,펌프방식,펌프1종류,펌프2종류,펌프1밸브,펌프2밸브,펌프1제어,펌프2제어,펌프1대수,펌프2대수", "번호 = '" + ID + "'");

                PumpUse_comboBox.SelectedItem = Value[0][0];
                PumpUse = Value[0][0];

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
            catch { }


            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "공급설비1종류,공급설비2종류", "번호 = '" + ID + "'");
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

                            string[][] CE_Value = Program.DB.getValue(DB.type.ProjDB, "Heating_ce_Form", "설치위치", "난방시스템 = '" + ID + "' And 존번호 = '" + 존번호 + "' And 공급설비 = '" + 공급설비 + "'");
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

                            string[][] CE_Value = Program.DB.getValue(DB.type.ProjDB, "Heating_ce_Form", "설치위치", "난방시스템 = '" + ID + "' And 존번호 = '" + 존번호 + "' And 공급설비 = '" + 공급설비 + "'");
                            ce_dataGridView.Rows[n].Cells[7].Value = CE_Value[0][0].ToString();
                        }
                    }
                    catch { }
                }
            }
            catch { }

            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "축열유무,축열펌프유무,축열펌프,축열용량", "번호 = '" + ID + "'");
                StorageUse = Value[0][0];
                StorageUse_comboBox.SelectedItem = StorageUse;

                StoragePumpUse = Value[0][1];
                StoragePump_comboBox.SelectedItem = StoragePumpUse;

                StoragePump = Value[0][2];

                if (StoragePump_dataGridView.Rows.Count == 0)
                {
                    StoragePump_dataGridView.Rows.Add();
                }
                Load_StoragePump(StoragePump);

                if (Value[0][3] != null && Value[0][3] != "")
                {
                    Vs = Convert.ToDouble(Value[0][3]);
                    Vs_textBox.Text = Vs.ToString();
                }

            }
            catch { }

            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "배관관경,배관보온두께,보온열전도율,배관보온재", "번호 = '" + ID + "'");
                PipeD = Convert.ToDouble(Value[0][0]);
                PipeD_textBox.Text = PipeD.ToString();

                PipeInsD = Convert.ToDouble(Value[0][1]);
                PipeInsD_textBox.Text = PipeInsD.ToString();

                PipeInsD = Convert.ToDouble(Value[0][2]);
                PipeIns_Ramda_textBox.Text = PipeInsD.ToString();

                PipeIns = Value[0][3];
                PipeIns_textBox.Text = PipeIns;
            }
            catch { }


        }

        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            Num_textBox.Text = ID;
            Num = ID;
        }

    }
}
