using main.info;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
        string UserDB_SingleDoubleTriple2 = "", UserDB_ArAir2 = "", UserDB_LE_CL_V2 = "";


        public Window_DoubleGlassDB()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            load_table_GlassDB();
            load_Glass_comboBox();
            load_table_DoubleGlassDB();
            UserNum = Program.UTIL.CreateNum("User_DoubleGlass", "번호", "DWG_0");
            UserNum_textBox.Text = UserNum;
            //복층/삼중/단창 콤보박스
            SingleDoubleTriple_comboBox.Items.Clear();
            SingleDoubleTriple_comboBox.Items.Add("단창");
            SingleDoubleTriple_comboBox.Items.Add("복층");
            SingleDoubleTriple_comboBox.Items.Add("삼중");
            //아르곤/공기 콤보박스
            ArAir_comboBox.Items.Clear();
            ArAir_comboBox.Items.Add("공기");
            ArAir_comboBox.Items.Add("아르곤");
            //LE/CL/V 콤보박스
            LE_CL_V_comboBox.Items.Clear();
            LE_CL_V_comboBox.Items.Add("CL");
            LE_CL_V_comboBox.Items.Add("LE");
            LE_CL_V_comboBox.Items.Add("V");
        }
        void load_table_GlassDB()
        {
            new StackedHeaderDecorator(Glass_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Glass_dataGridView.Columns.Clear();

            Glass_dataGridView.Columns.Add("A1", "번호");
            Glass_dataGridView.Columns.Add("A2", "DB유형");
            Glass_dataGridView.Columns.Add("A3", "제품명");
            Glass_dataGridView.Columns.Add("A4", "제조사");
            Glass_dataGridView.Columns.Add("A5", "복층/삼중/단창");
            Glass_dataGridView.Columns.Add("A6", "아르곤/공기");
            Glass_dataGridView.Columns.Add("A7", "LE/CL/V");
            Glass_dataGridView.Columns.Add("A8", "유리성능.열관류율.Ug.[W/m2∙K]");
            Glass_dataGridView.Columns.Add("A9", "유리성능.태양열취득률.SHGC.[-]");
            Glass_dataGridView.Columns.Add("A10", "유리성능.빛투과율.τD65,SNA.[-]");
            Glass_dataGridView.Columns.Add("A11", "반사율.외부.ρv.[-]");
            Glass_dataGridView.Columns.Add("A12", "반사율.내부.ρ'v.[-]");

            string[][] User_WinGlass = Program.DB.getValue(DB.type.ProjDB, "User_Glass", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "");
            if (User_WinGlass.Length > 0)
            {
                for (int n = 0; n < User_WinGlass.Length; n++)
                {
                    Glass_dataGridView.Rows.Add();
                    int nRow = Glass_dataGridView.Rows.Count - 1;
                    for (int k = 0; k < 12; k++)
                    {
                        Glass_dataGridView.Rows[nRow].Cells[k].Value = User_WinGlass[n][k];
                    }

                    GlassList.Add(User_WinGlass[n][2]);
                }
            }


            string[][] WinGlass = Program.DB.getValue(DB.type.BaseDB_HCneed, "유리", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "");
            if (WinGlass.Length > 0)
            {
                for (int n = 0; n < WinGlass.Length; n++)
                {
                    Glass_dataGridView.Rows.Add();
                    int nRow = Glass_dataGridView.Rows.Count - 1;
                    for (int k = 0; k < 12; k++)
                    {
                        Glass_dataGridView.Rows[nRow].Cells[k].Value = WinGlass[n][k];
                    }
                    GlassList.Add(WinGlass[n][2]);
                }
            }
            Count_DB = WinGlass.Length;
        }
        private Boolean datagridviewDesign(DataGridViewCell cell, int column, int row)
        {
            if (row % 2 == 1)
            {
                cell.Style.BackColor = Color.FromArgb(251, 251, 251);
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = Color.FromArgb(251, 251, 251);
                cell.Style.SelectionForeColor = Color.Black;
                return true;
            }
            else return false;
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
            Select1_Glass = Program.DB.getValue(DB.type.ProjDB, "User_Glass", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "제품명 = '" + SelectGlass1 + "'");
            if (Select1_Glass.Length > 0)
            { SelectGlass1 = Select1_Glass[0][2]; }
            Select1_Glass = Program.DB.getValue(DB.type.BaseDB_HCneed, "유리", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "제품명 = '" + SelectGlass1 + "'");
            if (Select1_Glass.Length > 0)
            { SelectGlass1 = Select1_Glass[0][2]; }
            Calc_DoubleGlass();
        }

        private void SelectGlass2_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectGlass2 = SelectGlass2_comboBox.SelectedItem.ToString();
            Select2_Glass = Program.DB.getValue(DB.type.ProjDB, "User_Glass", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "제품명 = '" + SelectGlass2 + "'");
            if (Select2_Glass.Length > 0)
            { SelectGlass2 = Select2_Glass[0][2]; }

            Select2_Glass = Program.DB.getValue(DB.type.BaseDB_HCneed, "유리", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "제품명 = '" + SelectGlass2 + "'");
            if (Select2_Glass.Length > 0)
            { SelectGlass2 = Select2_Glass[0][2]; }

            Calc_DoubleGlass();
        }

        private void Calc_DoubleGlass()
        {
            try
            {
                if (Select1_Glass != null && Select1_Glass.Length > 0)
                {
                    if (Select2_Glass != null && Select2_Glass.Length > 0)
                    {
                        UserDB_SingleDoubleTriple = Select1_Glass[0][4] + "+" + Select2_Glass[0][4];
                        UserDB_ArAir = Select1_Glass[0][5] + "+" + Select2_Glass[0][5];
                        UserDB_LE_CL_V = Select1_Glass[0][6] + "+" + Select2_Glass[0][6];
                        UserDB_Ug = 1 / ((1 / Convert.ToDouble(Select1_Glass[0][7])) - 0.04 + 0.189 - 0.13 + (1 / Convert.ToDouble(Select2_Glass[0][7])));
                        String[][] f_shgc = Program.DB.getValue(DB.type.BaseDB_HCneed, "이중창보정계수", "계수", "조합구성 = '" + UserDB_LE_CL_V + "' AND 보정유형 = '태양열취득률'");
                        String[][] f_τ = Program.DB.getValue(DB.type.BaseDB_HCneed, "이중창보정계수", "계수", "조합구성 = '" + UserDB_LE_CL_V + "' AND 보정유형 = '빛투과율'");
                        if (f_shgc.Length > 0)
                        {
                            UserDB_g = Convert.ToDouble(f_shgc[0][0]) * Convert.ToDouble(Select1_Glass[0][8]) * Convert.ToDouble(Select2_Glass[0][8]);
                        }
                        if (f_τ.Length > 0)
                        { UserDB_Tao = Convert.ToDouble(f_τ[0][0]) * Convert.ToDouble(Select1_Glass[0][9]) * Convert.ToDouble(Select2_Glass[0][9]); }
                        UserDB_RExternal = Convert.ToDouble(Select1_Glass[0][10]);
                        UserDB_RInternal = Convert.ToDouble(Select2_Glass[0][11]);
                    }
                }
            }
            catch { }
        }

        private void AddUserDB_button_Click(object sender, EventArgs e)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            if (UserDBName != null && UserDB_SingleDoubleTriple != null && UserDB_ArAir != null && UserDB_LE_CL_V != null && UserDB_Ug != 0 && UserDB_g != 0 && UserDB_Tao != 0 && UserDB_RExternal != 0 && UserDB_RInternal != 0)
            {
                Program.DB.setValue(DB.type.ProjDB, "User_DoubleGlass", "번호,프로젝트유형,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율",
                    "'" + UserNum + "','" + 프로젝트유형[0][0] + "','" + "사용자" + "','" + UserDBName + "','" + UserDB_Manufacture + "','" + UserDB_SingleDoubleTriple + "','" + UserDB_ArAir + "','" + UserDB_LE_CL_V + "','" + UserDB_Ug.ToString() + "','" + UserDB_g.ToString() + "','" + UserDB_Tao.ToString() + "','" + UserDB_RExternal.ToString() + "','" + UserDB_RInternal.ToString() + "'", "번호");
                load_table_DoubleGlassDB();
                UserNum = Program.UTIL.CreateNum("User_DoubleGlass", "번호", "DWG_0");
                UserNum_textBox.Text = UserNum;
                Program.DB.saveProject();
            }
            else
            {
                MessageBox.Show("모든 값을 입력해주세요.");
            }

        }
        void load_table_DoubleGlassDB()
        {
            new StackedHeaderDecorator(DoubleGlass_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            DoubleGlass_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            DoubleGlass_dataGridView.Columns.Add(checkBoxColumn);

            DoubleGlass_dataGridView.Columns.Add("B1", "번호");
            DoubleGlass_dataGridView.Columns.Add("B2", "DB유형");
            DoubleGlass_dataGridView.Columns.Add("B3", "제품명");
            DoubleGlass_dataGridView.Columns.Add("B4", "제조사");
            DoubleGlass_dataGridView.Columns.Add("B5", "복층/삼중/단창");
            DoubleGlass_dataGridView.Columns.Add("B6", "아르곤/공기");
            DoubleGlass_dataGridView.Columns.Add("B7", "LE/CL/V");
            DoubleGlass_dataGridView.Columns.Add("B8", "열관류율.Ug[W/m2∙K]");
            DoubleGlass_dataGridView.Columns.Add("B9", "태양열취득율.SHGC[-]");
            DoubleGlass_dataGridView.Columns.Add("B10", "빛투과율.τD65,SNA[-]");
            DoubleGlass_dataGridView.Columns.Add("B11", "외부반사율.ρv[-]");
            DoubleGlass_dataGridView.Columns.Add("B12", "내부반사율.ρ'v[-]");

            string[][] User_DGlass = Program.DB.getValue(DB.type.ProjDB, "User_DoubleGlass", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "");
            if (User_DGlass.Length > 0)
            {
                for (int n = 0; n < User_DGlass.Length; n++)
                {
                    DoubleGlass_dataGridView.Rows.Add();
                    int nRow = DoubleGlass_dataGridView.Rows.Count - 1;
                    for (int a = 0; a < 7; a++)
                    {
                        DoubleGlass_dataGridView.Rows[nRow].Cells[a + 1].Value = User_DGlass[n][a];
                    }
                    for (int a = 7; a < 10; a++)
                    {
                        DoubleGlass_dataGridView.Rows[nRow].Cells[a + 1].Value = String.Format("{0:F3}", Convert.ToDouble(User_DGlass[n][a]));
                    }
                    DoubleGlass_dataGridView.Rows[nRow].Cells[11].Value = User_DGlass[n][10];
                    DoubleGlass_dataGridView.Rows[nRow].Cells[12].Value = User_DGlass[n][11];

                }
            }
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

        private void SingleDoubleTriple_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDB_SingleDoubleTriple2 = SingleDoubleTriple_comboBox.SelectedItem.ToString();
            Filter(UserDB_SingleDoubleTriple2, UserDB_ArAir2, UserDB_LE_CL_V2);
        }

        private void ArAir_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDB_ArAir2 = ArAir_comboBox.SelectedItem.ToString();
            Filter(UserDB_SingleDoubleTriple2, UserDB_ArAir2, UserDB_LE_CL_V2);
        }

        private void LE_CL_V_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDB_LE_CL_V2 = LE_CL_V_comboBox.SelectedItem.ToString();
            Filter(UserDB_SingleDoubleTriple2, UserDB_ArAir2, UserDB_LE_CL_V2);
        }
        private void Filter(string UserDB_SingleDoubleTriple, string UserDB_ArAir, string UserDB_LE_CL_V)
        {
            Glass_dataGridView.Rows.Clear();
            GlassList.Clear();
            string[][] User_WinGlass = null;
            string[][] WinGlass = null;

            if (UserDB_SingleDoubleTriple != "" && UserDB_ArAir != "" && UserDB_LE_CL_V != "")
            {
                User_WinGlass = Program.DB.getValue(DB.type.ProjDB, "User_Glass", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "복층_삼중_단창='" + UserDB_SingleDoubleTriple + "' and 아르곤_공기='" + UserDB_ArAir + "'and LE_CL_V='" + UserDB_LE_CL_V + "'");
                WinGlass = Program.DB.getValue(DB.type.BaseDB_HCneed, "유리", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "복층_삼중_단창='" + UserDB_SingleDoubleTriple + "' and 아르곤_공기='" + UserDB_ArAir + "'and LE_CL_V='" + UserDB_LE_CL_V + "'");
            }
            else if (UserDB_SingleDoubleTriple != "" && UserDB_ArAir != "" && UserDB_LE_CL_V == "")
            {
                User_WinGlass = Program.DB.getValue(DB.type.ProjDB, "User_Glass", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "복층_삼중_단창='" + UserDB_SingleDoubleTriple + "' and 아르곤_공기='" + UserDB_ArAir + "'");
                WinGlass = Program.DB.getValue(DB.type.BaseDB_HCneed, "유리", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "복층_삼중_단창='" + UserDB_SingleDoubleTriple + "' and 아르곤_공기='" + UserDB_ArAir + "'");
            }
            else if (UserDB_SingleDoubleTriple != "" && UserDB_ArAir == "" && UserDB_LE_CL_V != "")
            {
                User_WinGlass = Program.DB.getValue(DB.type.ProjDB, "User_Glass", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "복층_삼중_단창='" + UserDB_SingleDoubleTriple + "' and LE_CL_V='" + UserDB_LE_CL_V + "'");
                WinGlass = Program.DB.getValue(DB.type.BaseDB_HCneed, "유리", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "복층_삼중_단창='" + UserDB_SingleDoubleTriple + "' and LE_CL_V='" + UserDB_LE_CL_V + "'");
            }
            else if (UserDB_SingleDoubleTriple == "" && UserDB_ArAir != "" && UserDB_LE_CL_V != "")
            {
                User_WinGlass = Program.DB.getValue(DB.type.ProjDB, "User_Glass", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "아르곤_공기='" + UserDB_ArAir + "' and LE_CL_V='" + UserDB_LE_CL_V + "'");
                WinGlass = Program.DB.getValue(DB.type.BaseDB_HCneed, "유리", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "아르곤_공기='" + UserDB_ArAir + "' and LE_CL_V='" + UserDB_LE_CL_V + "'");
            }
            else if (UserDB_SingleDoubleTriple != "" && UserDB_ArAir == "" && UserDB_LE_CL_V == "")
            {
                User_WinGlass = Program.DB.getValue(DB.type.ProjDB, "User_Glass", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "복층_삼중_단창='" + UserDB_SingleDoubleTriple + "'");
                WinGlass = Program.DB.getValue(DB.type.BaseDB_HCneed, "유리", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "복층_삼중_단창='" + UserDB_SingleDoubleTriple + "'");
            }
            else if (UserDB_SingleDoubleTriple == "" && UserDB_ArAir != "" && UserDB_LE_CL_V == "")
            {
                User_WinGlass = Program.DB.getValue(DB.type.ProjDB, "User_Glass", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "아르곤_공기='" + UserDB_ArAir + "'");
                WinGlass = Program.DB.getValue(DB.type.BaseDB_HCneed, "유리", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "아르곤_공기='" + UserDB_ArAir + "'");
            }
            else if (UserDB_SingleDoubleTriple == "" && UserDB_ArAir == "" && UserDB_LE_CL_V != "")
            {
                User_WinGlass = Program.DB.getValue(DB.type.ProjDB, "User_Glass", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "LE_CL_V='" + UserDB_LE_CL_V + "'");
                WinGlass = Program.DB.getValue(DB.type.BaseDB_HCneed, "유리", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "LE_CL_V='" + UserDB_LE_CL_V + "'");
            }
            else
            {

            }

            if (User_WinGlass.Length > 0)
            {
                for (int n = 0; n < User_WinGlass.Length; n++)
                {
                    Glass_dataGridView.Rows.Add();
                    int nRow = Glass_dataGridView.Rows.Count - 1;
                    for (int k = 0; k < 12; k++)
                    {
                        Glass_dataGridView.Rows[nRow].Cells[k].Value = User_WinGlass[n][k];
                    }
                    GlassList.Add(User_WinGlass[n][2]);
                }
            }
            if (WinGlass.Length > 0)
            {
                for (int n = 0; n < WinGlass.Length; n++)
                {
                    Glass_dataGridView.Rows.Add();
                    int nRow = Glass_dataGridView.Rows.Count - 1;
                    for (int k = 0; k < 12; k++)
                    {
                        Glass_dataGridView.Rows[nRow].Cells[k].Value = WinGlass[n][k];
                    }
                    GlassList.Add(WinGlass[n][2]);
                    load_Glass_comboBox();
                }
            }
        }

        private void info_Click(object sender, EventArgs e)
        {

            string basePath = Program.gPath + "Manual\\2.subcontents\\5.Window\\2.DoubleGlassDB";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }
    }
}
