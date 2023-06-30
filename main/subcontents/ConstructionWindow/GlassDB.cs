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
    public partial class GlassDB : Form
    {
        String FrameType;
        double Count_FrameDB;
        int SelectRow;
        public String[] Select_Glass = new string[11];
        String UserNum, UserDBName, UserDB_Manufacture, UserDB_SingleDoubleTriple, UserDB_ArAir, UserDB_LE_CL_V;
        Double UserDB_Ug, UserDB_g, UserDB_Tao, UserDB_RExternal, UserDB_RInternal;
        public GlassDB()
        {
            InitializeComponent();
            load_table_GlassDB();

            //복층/삼중/단창 콤보박스
            SingleDoubleTriple_comboBox.Items.Clear();
            SingleDoubleTriple_comboBox.Items.Add("단창");
            SingleDoubleTriple_comboBox.Items.Add("복층");
            SingleDoubleTriple_comboBox.Items.Add("유리");
            SingleDoubleTriple_comboBox.SelectedIndex = 0;
            //아르곤/공기 콤보박스
            ArAir_comboBox.Items.Clear();
            ArAir_comboBox.Items.Add("공기");
            ArAir_comboBox.Items.Add("아르곤");
            ArAir_comboBox.SelectedIndex = 0;
            //LE/CL/V 콤보박스
            LE_CL_V_comboBox.Items.Clear();
            LE_CL_V_comboBox.Items.Add("CL");
            LE_CL_V_comboBox.Items.Add("LE");
            LE_CL_V_comboBox.Items.Add("V");
            LE_CL_V_comboBox.SelectedIndex = 0;
            UserNum = Program.UTIL.CreateNum("User_Glass", "번호", "UWG_0");
            UserNum_textBox.Text = UserNum;
        }
        void load_table_GlassDB()
        {
            DataTable table_WindowGlass = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Glass_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Glass_dataGridView.Columns.Add(checkBoxColumn);
            table_WindowGlass.Columns.Add("번호", typeof(string));
            table_WindowGlass.Columns.Add("DB유형", typeof(string));
            table_WindowGlass.Columns.Add("제품명", typeof(string));
            table_WindowGlass.Columns.Add("제조사", typeof(string));
            table_WindowGlass.Columns.Add("복층/삼중/단창", typeof(string));
            table_WindowGlass.Columns.Add("아르곤/공기", typeof(string));
            table_WindowGlass.Columns.Add("LE/CL/V", typeof(string));
            table_WindowGlass.Columns.Add("열관류율" + Environment.NewLine + "Ug[W/m2∙K]", typeof(string));
            table_WindowGlass.Columns.Add("태양열\r\n취득율" + Environment.NewLine + "SHGC[-]", typeof(string));
            table_WindowGlass.Columns.Add("빛투과율" + Environment.NewLine + "τD65,SNA[-]", typeof(string));
            table_WindowGlass.Columns.Add("외부\r\n반사율" + Environment.NewLine + "ρv[-]", typeof(string));
            table_WindowGlass.Columns.Add("내부\r\n반사율" + Environment.NewLine + "ρ'v[-]", typeof(string));
            string[][] WinGlass = Program.DB.getValue(DB.type.BaseDB_HCneed, "유리", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "");


            try
            {
                string[][] User_WinGlass = Program.DB.getValue(DB.type.ProjDB, "User_Glass", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "");
                for (int n = 0; n < User_WinGlass.Length; n++)
                {
                    table_WindowGlass.Rows.Add(User_WinGlass[n][0], User_WinGlass[n][1], User_WinGlass[n][2], User_WinGlass[n][3], User_WinGlass[n][4], User_WinGlass[n][5], User_WinGlass[n][6], User_WinGlass[n][7], User_WinGlass[n][8], User_WinGlass[n][9], User_WinGlass[n][10], User_WinGlass[n][11]);
                }
            }
            catch { }

            for (int n = 0; n < WinGlass.Length; n++)
            {
                table_WindowGlass.Rows.Add(WinGlass[n][0], WinGlass[n][1], WinGlass[n][2], WinGlass[n][3], WinGlass[n][4], WinGlass[n][5], WinGlass[n][6], WinGlass[n][7], WinGlass[n][8], WinGlass[n][9], WinGlass[n][10], WinGlass[n][11]);
            }
            Glass_dataGridView.DataSource = table_WindowGlass;
            Count_FrameDB = WinGlass.Length;
        }
      


        private void UserDBName_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDBName = UserDBName_textBox.Text;
        }

        private void UserDB_Manufacture_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Manufacture = UserDB_Manufacture_textBox.Text;

        }

        private void SingleDoubleTriple_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDB_SingleDoubleTriple = SingleDoubleTriple_comboBox.SelectedItem.ToString();
        }

        private void ArAir_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDB_ArAir = ArAir_comboBox.SelectedItem.ToString();
        }

        private void LE_CL_V_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDB_LE_CL_V = LE_CL_V_comboBox.SelectedItem.ToString();
        }

        private void UserDB_Ug_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Ug = Convert.ToDouble(UserDB_Ug_textBox.Text);
        }

        private void UserDB_g_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_g = Convert.ToDouble(UserDB_g_textBox.Text);
        }

        private void UserDB_Tao_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Tao = Convert.ToDouble(UserDB_Tao_textBox.Text);
        }

        private void UserDB_RExternal_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_RExternal = Convert.ToDouble(UserDB_RExternal_textBox.Text);
        }

        private void UserDB_RInternal_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_RInternal = Convert.ToDouble(UserDB_RInternal_textBox.Text);
        }

        private void AddUserDB_button_Click(object sender, EventArgs e)
        {
            if (UserDBName != null && UserDB_SingleDoubleTriple != null && UserDB_ArAir != null && UserDB_LE_CL_V != null && UserDB_Ug != 0 && UserDB_g != 0 && UserDB_Tao != 0 && UserDB_RExternal != 0 && UserDB_RInternal != 0)
            {
                Program.DB.setValue(DB.type.ProjDB, "User_Glass", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율",
                    "'" + UserNum + "','" + "사용자" + "','" + UserDBName + "','" + UserDB_Manufacture + "','" + UserDB_SingleDoubleTriple + "','" + UserDB_ArAir + "','" + UserDB_LE_CL_V + "','" + UserDB_Ug.ToString() + "','" + UserDB_g.ToString() + "','" + UserDB_Tao.ToString() + "','" + UserDB_RExternal.ToString() + "','" + UserDB_RInternal.ToString() + "'", "번호");
                load_table_GlassDB();
            }
            else
            {
                MessageBox.Show("모든 값을 입력해주세요.");
            }

        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            int k = Glass_dataGridView.CurrentCell.RowIndex;
            if (k > -1)
            {
                if (Glass_dataGridView.Rows[k].Cells[2].Value.ToString() == "사용자")
                {
                    if ((MessageBox.Show(Glass_dataGridView.Rows[k].Cells[3].Value.ToString() + "을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        String Delete_Num = Glass_dataGridView.Rows[k].Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "User_Glass", "번호 ='" + Delete_Num + "'");
                        load_table_GlassDB();
                    }
                }
                else
                {
                    MessageBox.Show("기본 DB는 삭제할 수 없습니다.");
                }
            }

            
        }

        //데이터그리드뷰 체크박스 선택 시
        private void Glass_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Glass_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = Glass_dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Count_FrameDB; k++)
                {
                    if (k != row.Index)
                    {
                        Glass_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = Glass_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = Glass_dataGridView.Rows[e.RowIndex];
                    }
                }
            }
        }
        private void Save_button_Click(object sender, EventArgs e)
        {

            DataGridViewRow row = Glass_dataGridView.Rows[SelectRow];

            for (int i = 1; i < row.Cells.Count - 2; i++)
            {
                Select_Glass[i] = row.Cells[i + 2].Value.ToString();
            }
            Select_Glass[0] = row.Cells[1].Value.ToString();

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

    }
}
