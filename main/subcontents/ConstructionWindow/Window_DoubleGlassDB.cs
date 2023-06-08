using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents
{
    public partial class Window_DoubleGlassDB : Form
    {
        String FrameType;
        double Count_DB;
        int SelectRow;
        public String[] Select_WindowGlass = new string[11];
        String UserNum, UserDBName, UserDB_Manufacture, UserDB_SingleDoubleTriple, UserDB_ArAir, UserDB_LE_CL_V;
        Double UserDB_Ug, UserDB_g, UserDB_Tao, UserDB_RExternal, UserDB_RInternal;
        List<String> GlassList = new List<String>();
        String SelectGlass1, SelectGlass2;
        string[][] Select1_Glass;
        string[][] Select2_Glass;


        public Window_DoubleGlassDB()
        {
            InitializeComponent();
            load_table_GlassDB();
            load_Glass_comboBox();
            load_table_DoubleGlassDB();
            UserNum = Program.UTIL.CreateNum("User_DoubleGlass", "번호", "DWG_0");
            UserNum_textBox.Text = UserNum;
        }
        void load_table_GlassDB()
        {
            DataTable table_WindowGlass = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Glass_dataGridView.Columns.Clear();
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
            string[][] WinGlass = Program.DB.getValue(DB.type.BaseDB, "유리", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "");


            try
            {
                string[][] User_WinGlass = Program.DB.getValue(DB.type.ProjDB, "User_Glass", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "");
                for (int n = 0; n < User_WinGlass.Length; n++)
                {
                    table_WindowGlass.Rows.Add(User_WinGlass[n][0], User_WinGlass[n][1], User_WinGlass[n][2], User_WinGlass[n][3], User_WinGlass[n][4], User_WinGlass[n][5], User_WinGlass[n][6], User_WinGlass[n][7], User_WinGlass[n][8], User_WinGlass[n][9], User_WinGlass[n][10], User_WinGlass[n][11]);
                    GlassList.Add(User_WinGlass[n][2]);
                }
            }
            catch { }

            for (int n = 0; n < WinGlass.Length; n++)
            {
                table_WindowGlass.Rows.Add(WinGlass[n][0], WinGlass[n][1], WinGlass[n][2], WinGlass[n][3], WinGlass[n][4], WinGlass[n][5], WinGlass[n][6], WinGlass[n][7], WinGlass[n][8], WinGlass[n][9], WinGlass[n][10], WinGlass[n][11]);
                GlassList.Add(WinGlass[n][2]);
            }
            Glass_dataGridView.DataSource = table_WindowGlass;
            Count_DB = WinGlass.Length;

        }


        void load_Glass_comboBox()
        {
            string[] GlassArray = GlassList.ToArray();
            SelectGlass1_comboBox.Items.Clear();
            SelectGlass1_comboBox.Items.AddRange(GlassArray);
            SelectGlass2_comboBox.Items.Clear();
            SelectGlass2_comboBox.Items.AddRange(GlassArray);
        }

        private void UserDBName_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDBName = UserDBName_textBox.Text;

        }

        private void UserDB_Manufacture_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Manufacture = UserDB_Manufacture_textBox.Text;

        }

        private void SelectGlass1_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectGlass1 = SelectGlass1_comboBox.SelectedItem.ToString();

            try
            {

                Select1_Glass = Program.DB.getValue(DB.type.ProjDB, "User_Glass", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "제품명 = '" + SelectGlass1 + "'");
                SelectGlass1 = Select1_Glass[0][2];
            }
            catch
            {
                Select1_Glass = Program.DB.getValue(DB.type.BaseDB, "유리", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "제품명 = '" + SelectGlass1 + "'");
                SelectGlass1 = Select1_Glass[0][2];
            }
            Calc_DoubleGlass();
        }

        private void SelectGlass2_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectGlass2 = SelectGlass2_comboBox.SelectedItem.ToString();

            try
            {

                Select2_Glass = Program.DB.getValue(DB.type.ProjDB, "User_Glass", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "제품명 = '" + SelectGlass2 + "'");
                SelectGlass2 = Select2_Glass[0][2];

            }
            catch
            {
                Select2_Glass = Program.DB.getValue(DB.type.BaseDB, "유리", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "제품명 = '" + SelectGlass2 + "'");
                SelectGlass2 = Select2_Glass[0][2];
            }
            Calc_DoubleGlass();
        }

        private void Calc_DoubleGlass()
        {
            if (Select1_Glass != null && Select2_Glass != null)
            {
                UserDB_SingleDoubleTriple = Select1_Glass[0][4] + "+" + Select2_Glass[0][4];
                UserDB_ArAir = Select1_Glass[0][5] + "+" + Select2_Glass[0][5];
                UserDB_LE_CL_V = Select1_Glass[0][6] + "+" + Select2_Glass[0][6];
                UserDB_Ug = 1 / ((1 / Convert.ToDouble(Select1_Glass[0][7])) - 0.04 + 0.189 - 0.13 + (1 / Convert.ToDouble(Select2_Glass[0][7])));
                String[][] f_shgc = Program.DB.getValue(DB.type.BaseDB, "이중창보정계수", "계수", "조합구성 = '" + UserDB_LE_CL_V + "' AND 보정유형 = '태양열취득률'");
                String[][] f_τ = Program.DB.getValue(DB.type.BaseDB, "이중창보정계수", "계수", "조합구성 = '" + UserDB_LE_CL_V + "' AND 보정유형 = '빛투과율'");
                UserDB_g = Convert.ToDouble(f_shgc[0][0]) * Convert.ToDouble(Select1_Glass[0][8]) * Convert.ToDouble(Select2_Glass[0][8]);
                UserDB_Tao = Convert.ToDouble(f_τ[0][0]) * Convert.ToDouble(Select1_Glass[0][9]) * Convert.ToDouble(Select2_Glass[0][9]);
                UserDB_RExternal = Convert.ToDouble(Select1_Glass[0][10]);
                UserDB_RInternal = Convert.ToDouble(Select2_Glass[0][11]);
            }


        }



        private void AddUserDB_button_Click(object sender, EventArgs e)
        {
            if (UserDBName != null && UserDB_SingleDoubleTriple != null && UserDB_ArAir != null && UserDB_LE_CL_V != null && UserDB_Ug != 0 && UserDB_g != 0 && UserDB_Tao != 0 && UserDB_RExternal != 0 && UserDB_RInternal != 0)
            {
                Program.DB.setValue(DB.type.ProjDB, "User_DoubleGlass", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율",
                    "'" + UserNum + "','" + "사용자" + "','" + UserDBName + "','" + UserDB_Manufacture + "','" + UserDB_SingleDoubleTriple + "','" + UserDB_ArAir + "','" + UserDB_LE_CL_V + "','" + UserDB_Ug.ToString() + "','" + UserDB_g.ToString() + "','" + UserDB_Tao.ToString() + "','" + UserDB_RExternal.ToString() + "','" + UserDB_RInternal.ToString() + "'", "번호");
                load_table_DoubleGlassDB();
            }
            else
            {
                MessageBox.Show("모든 값을 입력해주세요.");
            }

        }
        void load_table_DoubleGlassDB()
        {
            DataTable table_DoubleGlass = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            DoubleGlass_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            DoubleGlass_dataGridView.Columns.Add(checkBoxColumn);
            table_DoubleGlass.Columns.Add("번호", typeof(string));
            table_DoubleGlass.Columns.Add("DB유형", typeof(string));
            table_DoubleGlass.Columns.Add("제품명", typeof(string));
            table_DoubleGlass.Columns.Add("제조사", typeof(string));
            table_DoubleGlass.Columns.Add("복층/삼중/단창", typeof(string));
            table_DoubleGlass.Columns.Add("아르곤/공기", typeof(string));
            table_DoubleGlass.Columns.Add("LE/CL/V", typeof(string));
            table_DoubleGlass.Columns.Add("열관류율" + Environment.NewLine + "Ug[W/m2∙K]", typeof(string));
            table_DoubleGlass.Columns.Add("태양열\r\n취득율" + Environment.NewLine + "SHGC[-]", typeof(string));
            table_DoubleGlass.Columns.Add("빛투과율" + Environment.NewLine + "τD65,SNA[-]", typeof(string));
            table_DoubleGlass.Columns.Add("외부\r\n반사율" + Environment.NewLine + "ρv[-]", typeof(string));
            table_DoubleGlass.Columns.Add("내부\r\n반사율" + Environment.NewLine + "ρ'v[-]", typeof(string));

            try
            {
                string[][] User_DGlass = Program.DB.getValue(DB.type.ProjDB, "User_DoubleGlass", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "");
                for (int n = 0; n < User_DGlass.Length; n++)
                {
                    table_DoubleGlass.Rows.Add(User_DGlass[n][0], User_DGlass[n][1], User_DGlass[n][2], User_DGlass[n][3], User_DGlass[n][4], User_DGlass[n][5], User_DGlass[n][6], String.Format("{0:F3}", Convert.ToDouble(User_DGlass[n][7])), String.Format("{0:F3}", Convert.ToDouble(User_DGlass[n][8])), String.Format("{0:F3}", Convert.ToDouble(User_DGlass[n][9])), User_DGlass[n][10], User_DGlass[n][11]);
                }
            }
            catch { }

            DoubleGlass_dataGridView.DataSource = table_DoubleGlass;
            Count_DB = table_DoubleGlass.Rows.Count;
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            int k = DoubleGlass_dataGridView.CurrentCell.RowIndex;
            if (k > -1)
            {
                if (DoubleGlass_dataGridView.Rows[k].Cells[2].Value.ToString() == "사용자")
                {
                    if ((MessageBox.Show(DoubleGlass_dataGridView.Rows[k].Cells[3].Value.ToString() + "을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        String Delete_Num = DoubleGlass_dataGridView.Rows[k].Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "User_DoubleGlass", "번호 ='" + Delete_Num + "'");
                        load_table_DoubleGlassDB();
                    }
                }
                else
                {
                    MessageBox.Show("기본 DB는 삭제할 수 없습니다.");
                }
            }


        }

        //데이터그리드뷰 체크박스 선택 시
        private void DoubleGlass_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DoubleGlass_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = DoubleGlass_dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Count_DB; k++)
                {
                    if (k != row.Index)
                    {
                        DoubleGlass_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = DoubleGlass_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = DoubleGlass_dataGridView.Rows[e.RowIndex];
                    }
                }
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {

            DataGridViewRow row = DoubleGlass_dataGridView.Rows[SelectRow];

            for (int i = 1; i < row.Cells.Count - 2; i++)
            {
                Select_WindowGlass[i] = row.Cells[i + 2].Value.ToString();
            }
            Select_WindowGlass[0] = row.Cells[1].Value.ToString();

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

    }
}
