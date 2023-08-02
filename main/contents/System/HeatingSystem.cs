using main.contentslist;
using main.subcontents.ConstructionWall;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.contents
{
    public partial class HeatingSystem : Form
    {
        String Num, Name;
        String HeatingDHW, Combi, SystemLoacation, SLRL, MainSystem, SubSystem1, SubSystem2, InPump;
        String[] SystemType = { "보일러", "히트펌프", "흡수식온수기", "지역난방", "태양열시스템" };
        public HeatingSystem()
        {
            InitializeComponent();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '난방시스템'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            //난방+급탕 콤보박스
            HeatingDHW_comboBox.Items.Clear();
            HeatingDHW_comboBox.Items.Add("난방");
            HeatingDHW_comboBox.Items.Add("난방+급탕");
            HeatingDHW_comboBox.SelectedIndex = 0;

            //콤비설비 콤보박스 추후 반영 
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

            //생산설비내 펌프 유무
            InPump_comboBox.Items.Clear();
            InPump_comboBox.Items.Add("내부 펌프 없음");
            InPump_comboBox.Items.Add("내부 펌프 있음+외기온에 따른 제어");
            InPump_comboBox.Items.Add("내부 펌프 있음+실내온도에 따른 제어");
            InPump_comboBox.SelectedIndex = 2;

            MainSystem_comboBox.Items.Clear();
            SubSystem1_comboBox.Items.Clear();
            SubSystem2_comboBox.Items.Clear();
            for (int i = 0; i < SystemType.Length; i++)
            {
                MainSystem_comboBox.Items.Add(SystemType[i]);
                SubSystem1_comboBox.Items.Add(SystemType[i]);
                SubSystem2_comboBox.Items.Add(SystemType[i]);
            }
            MainSystem_comboBox.SelectedIndex = 0;

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

        private void HeatingDHW_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HeatingDHW_comboBox.SelectedItem != null)
            {
                HeatingDHW = HeatingDHW_comboBox.SelectedItem.ToString();
            }
        }

        private void Combi_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Combi_comboBox.SelectedItem != null)
            {
                Combi = Combi_comboBox.SelectedItem.ToString();
            }
        }

        private void SystemLoacation_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SystemLoacation_comboBox.SelectedItem != null)
            {
                SystemLoacation = SystemLoacation_comboBox.SelectedItem.ToString();
            }
        }

        private void SLRL_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SLRL_comboBox.SelectedItem != null)
            {
                SLRL = SLRL_comboBox.SelectedItem.ToString();
            }
        }

        private void MainSystem_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MainSystem_comboBox.SelectedItem != null)
            {
                MainSystem = MainSystem_comboBox.SelectedItem.ToString();
                LoadtabPage(MainSystem);
            }
        }

        private void SubSystem1_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SubSystem1_comboBox.SelectedItem != null)
            {
                SubSystem1 = SubSystem1_comboBox.SelectedItem.ToString();
                LoadtabPage(SubSystem1);
            }
        }

        private void SubSystem2_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SubSystem2_comboBox.SelectedItem != null)
            {
                SubSystem2 = SubSystem2_comboBox.SelectedItem.ToString();
                LoadtabPage(SubSystem2);
            }

        }
        private void LoadtabPage(String System)
        {
            if (System == "히트펌프")
            {
                tabControl2.SelectedTab = tabControl2.TabPages["HP_tabPage"];
            }
            else if (System == "보일러")
            {
                tabControl2.SelectedTab = tabControl2.TabPages["Boiler_tabPage"];
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

        private void InPump_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InPump_comboBox.SelectedItem != null)
            {
                InPump = InPump_comboBox.SelectedItem.ToString();
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
