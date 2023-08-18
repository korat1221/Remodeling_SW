using main.contentslist;
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
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Net.Mime.MediaTypeNames;

namespace main.contents
{
    public partial class HeatingSystem : Form
    {
        String Num, Name;
        String SystemLoacation, SLRL, Complex, MainSystem, Sub1System, Sub2System, PumpUse, PumpMethod, Pump1, Pump2, Pump1Valve, Pump2Valve, Pump1Control, Pump2Control, ce1Type, ce2Type;
        int Pump1Num, Pump2Num;
        String[] SystemType = { "보일러", "히트펌프", "흡수식온수기", "지역난방", "태양열시스템" };
        String[] ceType = { "실내기", "방열기", "팬코일유닛", "파워팬유닛", "복사난방" };
        ArrayList SelectBoiler = new ArrayList(); ArrayList SelectPump = new ArrayList(); ArrayList Selectce1Zone = new ArrayList(); ArrayList Selectce2Zone = new ArrayList();
        int ce_SelectRow;

        public HeatingSystem()
        {
            InitializeComponent();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '난방시스템'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

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

        private void Name_textBox_TextChanged(object sender, EventArgs e)
        {
            if (Name_textBox.Text != null)
            {
                Name = Name_textBox.Text.ToString();
            }
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
            Heating_Boiler heating_Boiler = new Heating_Boiler("장비일람표 적용");
            DialogResult result = heating_Boiler.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    if (heating_Boiler.SelectBoiler != null)
                    {

                        string[] token = heating_Boiler.SelectBoiler.Split(',');
                        SelectBoiler.Clear();
                        foreach (var item in token)
                        {
                            SelectBoiler.Add(item.ToString());
                        }
                        string[][] BoilerName;
                        String 내용;
                        if (SelectBoiler.Count > 1)
                        {
                            BoilerName = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "명칭", "번호 = '" + SelectBoiler[0].ToString() + "'");
                            내용 = BoilerName[0][0] + " 외 " + (SelectBoiler.Count - 1).ToString() + "개";
                        }
                        else if (SelectBoiler.Count == 1)
                        {
                            BoilerName = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "명칭", "번호 = '" + SelectBoiler[0].ToString() + "'");
                            내용 = BoilerName[0][0];
                        }
                        else { 내용 = ""; }

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
                }
                catch { }
            }
        }

        private void Load_Boiler_Table()
        {
            try
            {
                new StackedHeaderDecorator(Boiler_dataGridView);
                Boiler_dataGridView.Columns.Clear();
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

                for (int n = 0; n < SelectBoiler.Count; n++)
                {
                    string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "번호,명칭,연료,Type,용량,전부하효율,부분부하효율,소비전력,대기전력", "번호 = '" + SelectBoiler[n].ToString() + "'");
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
                    for (int k = 0; k < 4; k++)
                    {
                        Boiler_dataGridView.Rows[nRow].Cells[k].Value = User_Value[0][0];
                    }
                    Boiler_dataGridView.Rows[nRow].Cells[4].Value = 용량;
                    Boiler_dataGridView.Rows[nRow].Cells[5].Value = 전부하효율;
                    Boiler_dataGridView.Rows[nRow].Cells[6].Value = 부분부하효율;
                    Boiler_dataGridView.Rows[nRow].Cells[7].Value = 소비전력;
                    Boiler_dataGridView.Rows[nRow].Cells[8].Value = 대기전력;
                }


            }
            catch { }
        }


        /////////////////////////////////////////////////////저장////////////////////////////////////////////////////////////////////
        private void Storage_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /////////////////////////////////////////////////////분배////////////////////////////////////////////////////////////////////
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
            Heating_Pump heating_pump = new Heating_Pump();
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
            Heating_Pump heating_pump = new Heating_Pump();
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
            new StackedHeaderDecorator(Pump_dataGridView);
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
            new StackedHeaderDecorator(ce_dataGridView);
            ce_dataGridView.Columns.Clear();
            ce_checkBoxColumn.HeaderText = "선택";
            ce_checkBoxColumn.Name = "check";
            ce_dataGridView.Columns.Add(ce_checkBoxColumn);
            ce_dataGridView.Columns.Add("A1", "번호                 ");
            ce_dataGridView.Columns.Add("A2", "종류      ");
            // ce_dataGridView.Columns.Add("A3", "일람표 번호");
            ce_dataGridView.Columns.Add("A3", "일람표 명칭          ");
            ce_dataGridView.Columns.Add("A4", "용량   .[kW]");
            ce_dataGridView.Columns.Add("A5", "소비전력.[kW]");
            // ce_dataGridView.Columns.Add("A7", "적용 존.존번호");
            ce_dataGridView.Columns.Add("A6", "적용 존.존명칭         ");
            ce_dataGridView.Columns.Add("A7", "적용 존.설치위치       ");
        }
        private void ce1Zone_button_Click(object sender, EventArgs e)
        {
            if (ce_dataGridView.Columns.Count == 0)
            {
                Create_ce_Table();
            }
            Heating_ceZone ceZone = new Heating_ceZone(Num, ce1Type);
            DialogResult result = ceZone.ShowDialog();
            if (result == DialogResult.OK)
            {
                Load_ce(ce1Type);
                String[][] Value = Program.DB.getValue_dedupe(DB.type.ProjDB, "ZoneHeatingSystem_Form", "존번호", "난방시스템 = '" + Num + "' And 공급설비종류 = '" + ce1Type + "'");
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
        }

        private void ce2Zone_button_Click(object sender, EventArgs e)
        {
            Heating_ceZone ceZone = new Heating_ceZone(Num, ce2Type);
            DialogResult result = ceZone.ShowDialog();
            if (result == DialogResult.OK)
            {
                Load_ce(ce2Type);
                String[][] Value = Program.DB.getValue_dedupe(DB.type.ProjDB, "ZoneHeatingSystem_Form", "존번호", "난방시스템 = '" + Num + "' And 공급설비종류 = '" + ce2Type + "'");
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
                Program.DB.deleteValue(DB.type.ProjDB, "ZoneHeatingSystem_Form", "존번호 ='" + substring2 + "' AND 공급설비 = '" + substring + "' AND 난방시스템 = '" + Num + "'");
                ce_dataGridView.Rows.Remove(ce_dataGridView.Rows[ce_SelectRow]);
            }
        }
        private void Load_ce(string CE)
        {
            try
            {
                String[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneHeatingSystem_Form", "존번호,공급설비종류,공급설비", "난방시스템 = '" + Num + "' And 공급설비종류 = '" + CE + "'");

                int Sum = 1;
                for (int n = 0; n < Value.Length; n++)
                {
                    int nRow = ce_dataGridView.Rows.Add();
                    DataGridViewComboBoxCell 설치위치comboBox = new DataGridViewComboBoxCell();
                    설치위치comboBox.Items.Add("내벽 설치");
                    설치위치comboBox.Items.Add("외벽 설치");
                    설치위치comboBox.Items.Add("창호측 설치");
                    설치위치comboBox.Items.Add("창호측 설치");
                    ce_dataGridView.Rows[nRow].Cells[7] = 설치위치comboBox;

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
























        private void Save()
        {
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
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();

            try
            {
            }
            catch { }
        }

        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            Num_textBox.Text = ID;
            Num = ID;
        }

        private void Zone_button_Click(object sender, EventArgs e)
        {
            Heating_Zone SelectZone = new Heating_Zone(Num);
            DialogResult result = SelectZone.ShowDialog();
            if (result == DialogResult.OK)
            {
            }
        }

    }
}
