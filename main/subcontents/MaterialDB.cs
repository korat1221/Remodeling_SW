using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents
{
    public partial class MaterialDB : Form
    {
        double Count_FrameDB;
        int SelectRow;
        String LE_CL_V;
        public String[] Select = new String[11];
        String MaterialType, UserNum, UserDBName, UserDBType1, UserDBType2, UserDB_Note;
        Double UserDB_Conductivity, UserDB_Density, UserDB_dry, UserDB_wet, UserDB_c;
        int Num;
        List<String> List = new List<String>();


        public MaterialDB()
        {
            InitializeComponent();
            UserNum = Program.UTIL.CreateNum("User_Material", "번호", "UM_0");
            UserNum_textBox.Text = UserNum;

            //재료유형 리스트 생성 
            MaterialType_comboBox.Items.Add("단열재");
            MaterialType_comboBox.Items.Add("콘크리트");
            MaterialType_comboBox.Items.Add("조적");
            MaterialType_comboBox.Items.Add("패널");
            MaterialType_comboBox.Items.Add("미장");
            MaterialType_comboBox.Items.Add("목재");
            MaterialType_comboBox.Items.Add("금속재");
            MaterialType_comboBox.Items.Add("타일");
            MaterialType_comboBox.Items.Add("지중");
            MaterialType_comboBox.Items.Add("공기층");
            MaterialType_comboBox.SelectedIndex = 0;
        }
        private void MaterialType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MaterialType = MaterialType_comboBox.SelectedItem.ToString();
            //재료하위 유형 리스트 불러오기
            Load_UserDB_Type1();
            //테이블 불러오기 
            load_tableDB();
        }

        private void Load_UserDB_Type1()
        {
            string[][] Type2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "열전도율", "종류2", "구분 = '" + MaterialType + "'");
            List.Clear();
            if(Type2.Length >0 )
            {
                List.Add(Type2[0][0]);
                for (int n = 1; n < Type2.Length; n++)
                {
                    if (Type2[n - 1][0] != Type2[n][0])
                    {
                        List.Add(Type2[n][0]);
                    }
                }
                string[] Array = List.ToArray();
                UserDB_Type1_comboBox.Items.Clear();
                UserDB_Type1_comboBox.Items.AddRange(Array);
                UserDB_Type1_comboBox.SelectedIndex = 0;
            }
          
        }

        void load_tableDB()
        {
            DataTable table = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            dataGridView.Columns.Add(checkBoxColumn);
            table.Columns.Add("번호", typeof(string));
            table.Columns.Add("DB유형", typeof(string));
            table.Columns.Add("재료명", typeof(string));
            table.Columns.Add("종류1", typeof(string));
            table.Columns.Add("종류2", typeof(string));
            table.Columns.Add("열전도율" + Environment.NewLine + "λ\r\n[W/m·K]", typeof(string));
            table.Columns.Add("밀도" + Environment.NewLine + "ρ\r\n[kg/m³]", typeof(string));
            table.Columns.Add("비열" + Environment.NewLine + "с\r\n[kJ/kg·K]", typeof(string));
            table.Columns.Add("투습저항계수" + Environment.NewLine + "dry", typeof(string));
            table.Columns.Add("투습저항계수" + Environment.NewLine + "wet", typeof(string));
            table.Columns.Add("비고", typeof(string));


            if(MaterialType=="공기층")
            {
                table.Rows.Add("M_000", "표준", "공기층",null,null,"0");
            }
            else { table.Rows.Clear(); }
            string[][] User_DB = Program.DB.getValue(DB.type.ProjDB, "User_Material", "번호,DB유형,재료명,종류2,종류1,열전도율,밀도,비열,투습저항계수dry,투습저항계수wet,비고", "구분 = '" + MaterialType + "'");
            if (User_DB.Length > 0)
            {
                for (int n = 0; n < User_DB.Length; n++)
                {
                    table.Rows.Add(User_DB[n][0], User_DB[n][1], User_DB[n][2], User_DB[n][3], User_DB[n][4], User_DB[n][5], User_DB[n][6], User_DB[n][7], User_DB[n][8], User_DB[n][9], User_DB[n][10]);
                }
            }

            string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "열전도율", "재료명,종류2,종류1,열전도율,밀도,비열,투습저항계수dry,투습저항계수wet,비고", "구분 = '" + MaterialType + "'");
            String dbnum;
            if(Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    if (n + 1 < 10)
                    {
                        dbnum = "M_00" + (n + 1).ToString();
                    }
                    else
                    {
                        dbnum = "M_0" + (n + 1).ToString();
                    }
                    table.Rows.Add(dbnum, "표준", Value[n][0], Value[n][1], Value[n][2], Value[n][3], Value[n][4], Value[n][5], Value[n][6], Value[n][7], Value[n][8]);
                }
            }
            dataGridView.DataSource = table;
            dataGridView.Columns[0].Width = 40;
            dataGridView.Columns[1].Width = 60;
            dataGridView.Columns[2].Width = 70;
            dataGridView.Columns[3].Width = 150;
            Count_FrameDB = Value.Length;
        }

        private void UserDB_Type1_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDBType1 = UserDB_Type1_comboBox.SelectedItem.ToString();

            UserDBName = UserDBType1 + UserDBType2;
            UserDBName_textBox.Text = UserDBName;
        }

        private void UserDB_Type2_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDBType2 = UserDB_Type2_textBox.Text;

            UserDBName = UserDBType1 + UserDBType2;
            UserDBName_textBox.Text = UserDBName;
        }
        private void UserDB_Conductivity_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Conductivity = Convert.ToDouble(UserDB_Conductivity_textBox.Text);
        }

        private void UserDB_Density_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Density = Convert.ToDouble(UserDB_Density_textBox.Text);
        }

        private void UserDB_c_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_c = Convert.ToDouble(UserDB_c_textBox.Text);
        }

        private void UserDB_dry_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_dry = Convert.ToDouble(UserDB_dry_textBox.Text);
        }

        private void UserDB_wet_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_wet = Convert.ToDouble(UserDB_wet_textBox.Text);
        }


        private void UserDB_Note_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Note = UserDB_Note_textBox.Text;
        }
        private void AddUserDB_button_Click(object sender, EventArgs e)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            if (UserDBName != null && UserDBType1 != null && UserDB_Conductivity != 0)
            {
                Program.DB.setValue(DB.type.ProjDB, "User_Material", "번호,프로젝트유형,DB유형,구분,재료명,종류2,종류1,열전도율,밀도,투습저항계수dry,투습저항계수wet,비열,비고",
                    "'" + UserNum + "','" + 프로젝트유형[0][0] +"','" + "사용자" + "','" + "단열재" + "','" + UserDBName + "','" + UserDBType1 + "','" + UserDBType2 + "','" + UserDB_Conductivity.ToString() + "','" + UserDB_Density.ToString() + "','" + UserDB_dry.ToString() + "','" + UserDB_wet.ToString() + "','" + UserDB_c.ToString() + "','" + UserDB_Note + "'", "번호");
                load_tableDB();
            }
            else
            {
                MessageBox.Show("재료명, 종류1, 열전도율은 필수 입력 항목 입니다.");
            }
        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            int k = dataGridView.CurrentCell.RowIndex;
            if (k > -1)
            {
                if (dataGridView.Rows[k].Cells[2].Value.ToString() == "사용자")
                {
                    if ((MessageBox.Show(dataGridView.Rows[k].Cells[3].Value.ToString() + "을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        String Delete_Num = dataGridView.Rows[k].Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "User_Material", "번호 ='" + Delete_Num + "'");
                        load_tableDB();
                    }
                }
                else
                {
                    MessageBox.Show("기본 DB는 삭제할 수 없습니다.");
                }
            }
        }

        //데이터그리드뷰 체크박스 선택 시
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Count_FrameDB; k++)
                {
                    if (k != row.Index)
                    {
                        dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = dataGridView.Rows[e.RowIndex];
                    }
                }
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView.Rows[SelectRow];

            for (int i = 1; i < (row.Cells.Count - 2); i++)
            {
                Select[i] = row.Cells[i + 2].Value.ToString();
            }

            Select[0] = row.Cells[1].Value.ToString();
            Select[10] = MaterialType;

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

    }
}
