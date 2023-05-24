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
    public partial class Window_GlassDB : Form
    {
        String FrameType;
        double Count_FrameDB;
        int SelectRow;
        public Window_GlassDB()
        {
            InitializeComponent();
            load_table_GlassDB();
        }
        void load_table_GlassDB()
        {
            DataTable table_WindowGlass = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Frame_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Frame_dataGridView.Columns.Add(checkBoxColumn);
            table_WindowGlass.Columns.Add("번호", typeof(string));
            table_WindowGlass.Columns.Add("제품명", typeof(string));
            table_WindowGlass.Columns.Add("제조사", typeof(string));
            table_WindowGlass.Columns.Add("복층/삼중/단창", typeof(string));
            table_WindowGlass.Columns.Add("아르곤/공기", typeof(string));
            table_WindowGlass.Columns.Add("LE/CL/V", typeof(string));
            table_WindowGlass.Columns.Add("열관류율" + Environment.NewLine + "Ug[W/m2∙K]", typeof(string));
            table_WindowGlass.Columns.Add("태양열취득율" + Environment.NewLine + "SHGC[-]", typeof(string));
            table_WindowGlass.Columns.Add("빛투과율" + Environment.NewLine + "τD65,SNA[-]", typeof(string));
            table_WindowGlass.Columns.Add("외부반사율" + Environment.NewLine + "ρv[-]", typeof(string));
            table_WindowGlass.Columns.Add("내부반사율" + Environment.NewLine + "ρ'v[-]", typeof(string));
            string[][] WinGlass = Program.DB.getValue(DB.type.BaseDB, "유리", "번호,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "");

            for (int n = 0; n < WinGlass.Length; n++)
            {
                table_WindowGlass.Rows.Add(WinGlass[n][0], WinGlass[n][1], WinGlass[n][2], WinGlass[n][3], WinGlass[n][4], WinGlass[n][5], WinGlass[n][6], WinGlass[n][7], WinGlass[n][8], WinGlass[n][9], WinGlass[n][10]);
            }
            Frame_dataGridView.DataSource = table_WindowGlass;
            Count_FrameDB = WinGlass.Length;
        }
        //데이터그리드뷰 체크박스 선택 시
        private void Frame_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Frame_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = Frame_dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Count_FrameDB; k++)
                {
                    if (k != row.Index)
                    {
                        Frame_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = Frame_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = Frame_dataGridView.Rows[e.RowIndex];
                    }
                }
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            Program.DB.deleteValue(DB.type.CalcDB, "Select_WindowGlass");
            DataGridViewRow row = Frame_dataGridView.Rows[SelectRow];
            Program.DB.setValue(DB.type.CalcDB, "Select_WindowGlass", "번호,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율",
            "'" + row.Cells[1].Value.ToString() + "','" + row.Cells[2].Value.ToString() + "','" + row.Cells[3].Value.ToString() + "','" + row.Cells[4].Value.ToString() + "','" + row.Cells[5].Value.ToString() + "','"
            + row.Cells[6].Value.ToString() + "','" + row.Cells[7].Value.ToString() + "','" + row.Cells[8].Value.ToString() + "','" + row.Cells[9].Value.ToString() + "','" + row.Cells[10].Value.ToString() + "','"
            + row.Cells[11].Value.ToString() + "'", "번호");

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

    }
}
