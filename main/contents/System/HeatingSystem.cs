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
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace main.contents
{
    public partial class HeatingSystem : Form
    {
        String Num, Name;
        String SystemLoacation, SLRL, Complex, MainSystem, Sub1System, Sub2System, PumpUse, PumpMethod, Pump1, Pump2, Pump1Valve, Pump2Valve, Pump1Control, Pump2Control;
        int Pump1Num, Pump2Num;
        String[] SystemType = { "보일러", "히트펌프", "흡수식온수기", "지역난방", "태양열시스템" };
        ArrayList SelectBoiler = new ArrayList(); ArrayList SelectPump = new ArrayList();


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
            PumpUse_comboBox.SelectedIndex = 0;

            //펌프 방식 콤보박스
            PumpMethod_comboBox.Items.Clear();
            PumpMethod_comboBox.Items.Add("1차펌프");
            PumpMethod_comboBox.Items.Add("1차폐회로+2차펌프");
            PumpMethod_comboBox.SelectedIndex = 0;

            //펌프 정유량 밸브 콤보박스
            Pump1Valve_comboBox.Items.Clear();
            Pump1Valve_comboBox.Items.Add("있음");
            Pump1Valve_comboBox.Items.Add("없음");
            Pump1Valve_comboBox.SelectedIndex = 0;
            Pump2Valve_comboBox.Items.Clear();
            Pump2Valve_comboBox.Items.Add("있음");
            Pump2Valve_comboBox.Items.Add("없음");
            Pump2Valve_comboBox.SelectedIndex = 0;

            //펌프 제어 콤보박스
            Pump1Control_comboBox.Items.Clear();
            Pump1Control_comboBox.Items.Add("대수제어");
            Pump1Control_comboBox.Items.Add("인버터제어");
            Pump1Control_comboBox.Items.Add("제어없음");
            Pump1Control_comboBox.SelectedIndex = 1;
            Pump2Control_comboBox.Items.Clear();
            Pump2Control_comboBox.Items.Add("대수제어");
            Pump2Control_comboBox.Items.Add("인버터제어");
            Pump2Control_comboBox.Items.Add("제어없음");
            Pump2Control_comboBox.SelectedIndex = 1;

            Pump1Num = 1;
            Pump1Num_textBox.Text = Pump1Num.ToString();

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
                DataTable Boiler_table = new DataTable();
                Boiler_dataGridView.Columns.Clear();
                Boiler_table.Columns.Add("번호", typeof(string));
                Boiler_table.Columns.Add("명칭", typeof(string));
                Boiler_table.Columns.Add("연료", typeof(string));
                Boiler_table.Columns.Add("Type", typeof(string));
                Boiler_table.Columns.Add("용량" + Environment.NewLine + "[kW]", typeof(string));
                Boiler_table.Columns.Add("전부하효율" + Environment.NewLine + "[%]", typeof(string));
                Boiler_table.Columns.Add("부분부하효율" + Environment.NewLine + "[%]", typeof(string));
                Boiler_table.Columns.Add("소비전력" + Environment.NewLine + "[W]", typeof(string));
                Boiler_table.Columns.Add("대기전력" + Environment.NewLine + "[W]", typeof(string));
                Boiler_table.Columns.Add("대수" + Environment.NewLine + "[EA]", typeof(string));



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
                    Boiler_table.Rows.Add(User_Value[0][0], User_Value[0][1], User_Value[0][2], User_Value[0][3], 용량, 전부하효율, 부분부하효율, 소비전력, 대기전력);

                    Boiler_dataGridView.DataSource = Boiler_table;
                    Boiler_dataGridView.Rows[n].Cells[9].Style.BackColor = SystemColors.Info;
                }


            }
            catch { }
        }

        /////////////////////////////////////////////////////분배////////////////////////////////////////////////////////////////////
        private void PumpUse_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PumpUse_comboBox.SelectedItem != null)
            {
                PumpUse = PumpUse_comboBox.SelectedItem.ToString();
                Boolean Check = (PumpUse == "펌프 있음");

                PumpMethod_label.Visible = Check;
                PumpMethod_comboBox.Visible = Check;

                if (Check == false)
                {
                    PumpMethod_comboBox.SelectedItem = null;
                }

                ChangeVisble_Pump1(PumpUse);
                ChangeVisble_Pump2(PumpMethod); ;
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
                ChangeVisble_Pump2(PumpMethod);
            }
            else
            {
                PumpMethod = null;
            }
        }

        private void Pump1Valve_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Pump1Valve_comboBox.Visible == true && Pump1Valve_comboBox.SelectedItem != null)
            {
                Pump1Valve = Pump1Valve_comboBox.SelectedItem.ToString();
            }
            else
            {
                Pump1Valve = null;
            }
        }

        private void Pump1Control_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Pump1Control_comboBox.Visible == true && Pump1Control_comboBox.SelectedItem != null)
            {
                Pump1Control = Pump1Control_comboBox.SelectedItem.ToString();
            }
            else { Pump1Control = null; }
        }

        private void Pump1Num_textBox_TextChanged(object sender, EventArgs e)
        {
            if (Pump1Num_textBox.Visible == true && Pump1Num_textBox.Text != null)
            {
                Pump1Num = Convert.ToInt32(Pump1Num_textBox.Text.ToString());
            }
            else
            {
                Pump1Num = 0;
            }
        }

        private void Pump2Valve_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Pump2Valve_comboBox.Visible == true && Pump2Valve_comboBox.SelectedItem != null)
            {
                Pump2Valve = Pump2Valve_comboBox.SelectedItem.ToString();
            }
            else
            {
                Pump2Valve = null;
            }
        }

        private void Pump2Control_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Pump2Control_comboBox.Visible == true && Pump2Control_comboBox.SelectedItem != null)
            {
                Pump2Control = Pump2Control_comboBox.SelectedItem.ToString();
            }
            else
            {
                Pump2Control = null;
            }
        }

        private void Pump2Num_textBox_TextChanged(object sender, EventArgs e)
        {
            if (Pump2Num_textBox.Visible == true && Pump2Num_textBox.Text != null)
            {
                Pump2Num = Convert.ToInt32(Pump2Num_textBox.Text.ToString());
            }
            else
            {
                Pump2Num = 0;
            }

        }

        private void ChangeVisble_Pump1(String PumpUse)
        {
            Boolean Check = (PumpUse == "펌프 있음");
            Pump1_label.Visible = Check;
            Pump1_textBox.Visible = Check;
            Pump1_button.Visible = Check;
            Pump1Valve_label.Visible = Check;
            Pump1Valve_comboBox.Visible = Check;
            Pump1Control_label.Visible = Check;
            Pump1Control_comboBox.Visible = Check;
            Pump1Num_label1.Visible = Check;
            Pump1Num_textBox.Visible = Check;
            Pump1Num_label2.Visible = Check;

            if (Check == false)
            {
                Pump1 = null;
                Pump1Valve_comboBox.SelectedItem = null;
                Pump1Control_comboBox.SelectedItem = null;
                Pump1Num = 0;
                Pump1Num_textBox.Text = null;
            }
        }

        private void ChangeVisble_Pump2(String PumpMethod)
        {
            Boolean Check = (PumpMethod == "1차폐회로+2차펌프");
            Pump2_label.Visible = Check;
            Pump2_textBox.Visible = Check;
            Pump2_button.Visible = Check;
            Pump2Valve_label.Visible = Check;
            Pump2Valve_comboBox.Visible = Check;
            Pump2Control_label.Visible = Check;
            Pump2Control_comboBox.Visible = Check;
            Pump2Num_label1.Visible = Check;
            Pump2Num_textBox.Visible = Check;
            Pump2Num_label2.Visible = Check;

            if (Check == false)
            {
                Pump2 = null;
                Pump2Valve_comboBox.SelectedItem = null;
                Pump2Control_comboBox.SelectedItem = null;
                Pump2Num = 0;
                Pump2Num_textBox.Text = null;
            }
        }

        private void Pump1_button_Click(object sender, EventArgs e)
        {
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
                    }
                }
                catch { }
            }
        }

        private void Pump2_button_Click(object sender, EventArgs e)
        {
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
                    }
                }
                catch { }
            }
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
