using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
        String UserNum, UserDBName, UserDB_Manufacture, UserDB_SingleDoubleTriple="", UserDB_ArAir="", UserDB_LE_CL_V="";
        Double UserDB_Ug, UserDB_g, UserDB_Tao, UserDB_RExternal, UserDB_RInternal;
        public GlassDB()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            load_table_GlassDB();

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
            new StackedHeaderDecorator(Glass_dataGridView,DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Glass_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Glass_dataGridView.Columns.Add(checkBoxColumn);

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
                        Glass_dataGridView.Rows[nRow].Cells[k + 1].Value = User_WinGlass[n][k];
                    }
                }
            }

            string[][] WinGlass = Program.DB.getValue(DB.type.BaseDB_HCneed, "유리", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "");
            if(WinGlass.Length > 0)
            {
                for (int n = 0; n < WinGlass.Length; n++)
                {
                    Glass_dataGridView.Rows.Add();
                    int nRow = Glass_dataGridView.Rows.Count - 1;
                    for (int k = 0; k < 12; k++)
                    {
                        Glass_dataGridView.Rows[nRow].Cells[k + 1].Value = WinGlass[n][k];
                    }                   
                }
            }            
            Count_FrameDB = WinGlass.Length;
        }


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
            else return false;
        }

        private void Filter(string UserDB_SingleDoubleTriple, string UserDB_ArAir, string UserDB_LE_CL_V)
        {
            Glass_dataGridView.Rows.Clear();
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
                WinGlass = Program.DB.getValue(DB.type.BaseDB_HCneed, "유리", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "복층_삼중_단창='" + UserDB_SingleDoubleTriple  + "'");
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
                        Glass_dataGridView.Rows[nRow].Cells[k + 1].Value = User_WinGlass[n][k];
                    }
                }
            }
            if (WinGlass.Length >0)
            {
                for (int n = 0; n < WinGlass.Length; n++)
                {
                    Glass_dataGridView.Rows.Add();
                    int nRow = Glass_dataGridView.Rows.Count - 1;
                    for (int k = 0; k < 12; k++)
                    {
                        Glass_dataGridView.Rows[nRow].Cells[k + 1].Value = WinGlass[n][k];
                    }
                }
            }
        }

        private void SingleDoubleTriple_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDB_SingleDoubleTriple = SingleDoubleTriple_comboBox.SelectedItem.ToString();
            Filter(UserDB_SingleDoubleTriple, UserDB_ArAir, UserDB_LE_CL_V);
        }

        private void ArAir_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDB_ArAir = ArAir_comboBox.SelectedItem.ToString();
            Filter(UserDB_SingleDoubleTriple, UserDB_ArAir, UserDB_LE_CL_V);
        }

        private void LE_CL_V_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDB_LE_CL_V = LE_CL_V_comboBox.SelectedItem.ToString();
            Filter(UserDB_SingleDoubleTriple, UserDB_ArAir, UserDB_LE_CL_V);
        }

        private void AddUserDB_button_Click(object sender, EventArgs e)
        {
           int nRow = Glass_dataGridView.Rows.Add();

            UserNum = Program.UTIL.CreateNum("User_Glass", "번호", "UWG_0");
            Glass_dataGridView.Rows[nRow].Cells[1].Value = UserNum;
            Glass_dataGridView.Rows[nRow].Cells[2].Value = "사용자";

            DataGridViewComboBoxCell 복층_삼중_단창Combo = new DataGridViewComboBoxCell();
            복층_삼중_단창Combo.Items.Add("단창");
            복층_삼중_단창Combo.Items.Add("복층");
            복층_삼중_단창Combo.Items.Add("삼중");
            Glass_dataGridView.Rows[nRow].Cells[5] = 복층_삼중_단창Combo;

            DataGridViewComboBoxCell 아르곤_공기Combo = new DataGridViewComboBoxCell();
            아르곤_공기Combo.Items.Add("공기");
            아르곤_공기Combo.Items.Add("아르곤");
            Glass_dataGridView.Rows[nRow].Cells[6] = 아르곤_공기Combo;


            DataGridViewComboBoxCell LE_CL_VCombo = new DataGridViewComboBoxCell();
            LE_CL_VCombo.Items.Add("CL");
            LE_CL_VCombo.Items.Add("LE");
            LE_CL_VCombo.Items.Add("V");
            Glass_dataGridView.Rows[nRow].Cells[7] = LE_CL_VCombo;

            DataGridViewRow MoveRow = Glass_dataGridView.Rows[nRow];
            Glass_dataGridView.Rows.RemoveAt(nRow);
            Glass_dataGridView.Rows.Insert(0, MoveRow);
            Glass_dataGridView.CurrentCell = Glass_dataGridView[Glass_dataGridView.CurrentCell.ColumnIndex, 0];
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
            }
        }
        private void Save_button_Click(object sender, EventArgs e)
        {
            for(int a=0; a< Glass_dataGridView.Rows.Count; a++)
            {
                if (Glass_dataGridView.Rows[a].Cells[2].Value != null && Glass_dataGridView.Rows[a].Cells[2].Value.ToString() == "사용자")
                {
                    string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");

                    for (int aa = 1; aa < Glass_dataGridView.Columns.Count; aa++)
                    {
                        if (Glass_dataGridView.Rows[a].Cells[aa].Value == null || Glass_dataGridView.Rows[a].Cells[aa].Value.ToString() == "")
                        {
                            MessageBox.Show("사용자 입력값의 모든 값을 입력하세요.");
                            goto 그만;
                        }
                    }

                    for(int aa=8; aa< 13; aa++)
                    {
                        if (Glass_dataGridView.Rows[a].Cells[aa].Value != null && Glass_dataGridView.Rows[a].Cells[aa].Value.ToString() != "")
                        {
                            double parsedValue;
                            if (double.TryParse(Glass_dataGridView.Rows[a].Cells[aa].Value.ToString(), out parsedValue))
                            {
                            }
                            else
                            {
                                Glass_dataGridView.Rows[a].Cells[aa].Value = 0;
                                MessageBox.Show("숫자만 입력하세요.");
                                goto 그만;
                            }
                        }

                    }
                        Program.DB.setValue(DB.type.ProjDB, "User_Glass", "번호,프로젝트유형,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율",
                           "'" + Glass_dataGridView.Rows[a].Cells[1].Value.ToString() + "','" + 프로젝트유형[0][0] + "','" + "사용자" + "','" + Glass_dataGridView.Rows[a].Cells[3].Value.ToString() + "','" + Glass_dataGridView.Rows[a].Cells[4].Value.ToString() + "','" + Glass_dataGridView.Rows[a].Cells[5].Value.ToString() + "','" +
                           Glass_dataGridView.Rows[a].Cells[6].Value.ToString() + "','" + Glass_dataGridView.Rows[a].Cells[7].Value.ToString() + "','" +
                           Program.UTIL.dataGridView_doubleComa(Glass_dataGridView, a, 8, 3) + "','" + Program.UTIL.dataGridView_doubleComa(Glass_dataGridView, a, 9, 3) + "','" +
                           Program.UTIL.dataGridView_doubleComa(Glass_dataGridView, a, 10, 3) + "','" + Program.UTIL.dataGridView_doubleComa(Glass_dataGridView, a, 11, 3) + "','" +
                           Program.UTIL.dataGridView_doubleComa(Glass_dataGridView, a, 12, 3) + "'", "번호");
                }
            }
            DataGridViewRow row = Glass_dataGridView.Rows[SelectRow];

            for (int i = 1; i < row.Cells.Count - 2; i++)
            {
                Select_Glass[i] = row.Cells[i + 2].Value.ToString();
            }
            Select_Glass[0] = row.Cells[1].Value.ToString();

            this.DialogResult = DialogResult.OK;
            this.Close();
           그만: int x = 1; 

        }

    }
}
