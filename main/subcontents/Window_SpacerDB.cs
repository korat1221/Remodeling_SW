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
    public partial class Window_SpacerDB : Form
    {
        double Count_FrameDB;
        int SelectRow;
        String LE_CL_V;

        public Window_SpacerDB(String SingleDoubleType, String FrameMaterial,String LE_CL_V)
        {
            InitializeComponent();
            load_table_SpacerDB(SingleDoubleType, FrameMaterial, LE_CL_V);
        }
        void load_table_SpacerDB(String SingleDoubleType, String FrameMaterial,String LE_CL_V)
        {
            DataTable table_WindowSpacer = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Spacer_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Spacer_dataGridView.Columns.Add(checkBoxColumn);
            table_WindowSpacer.Columns.Add("번호", typeof(string));
            table_WindowSpacer.Columns.Add("제품명", typeof(string));
            table_WindowSpacer.Columns.Add("구분1", typeof(string));
            table_WindowSpacer.Columns.Add("구분2", typeof(string));
            table_WindowSpacer.Columns.Add("구분3", typeof(string));
            table_WindowSpacer.Columns.Add("고정유리(CL)\r\n선형열관류율" + Environment.NewLine + "Ψg,fix[W/m·K]", typeof(string));
            table_WindowSpacer.Columns.Add("개폐유리(CL)\r\n선형열관류율" + Environment.NewLine + "Ψg,t[W/m·K]", typeof(string));
            table_WindowSpacer.Columns.Add("고정유리(LE)\r\n선형열관류율" + Environment.NewLine + "Ψg,fix[W/m·K]", typeof(string));
            table_WindowSpacer.Columns.Add("개폐유리(LE)\r\n선형열관류율" + Environment.NewLine + "Ψg,t[W/m·K]", typeof(string));
            string[][] WinSpacer = Program.DB.getValue(DB.type.BaseDB, "창호간봉", "번호,DB유형,제품명,구분1,구분2,구분3,고정유리_CL_선형열관류율,개폐유리_CL_선형열관류율,고정유리_LE_선형열관류율,개폐유리_LE_선형열관류율", "구분2 = '" + SingleDoubleType + "'AND 구분3 ='" + FrameMaterial + "'");

            for (int n = 0; n < WinSpacer.Length; n++)
            {
                table_WindowSpacer.Rows.Add(WinSpacer[n][0], WinSpacer[n][2], WinSpacer[n][3], WinSpacer[n][4], WinSpacer[n][5], WinSpacer[n][6], WinSpacer[n][7], WinSpacer[n][8], WinSpacer[n][9]);
            }
            Spacer_dataGridView.DataSource = table_WindowSpacer;
            Count_FrameDB = WinSpacer.Length;
        }

        //데이터그리드뷰 체크박스 선택 시
        private void Spacer_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Spacer_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = Spacer_dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Count_FrameDB; k++)
                {
                    if (k != row.Index)
                    {
                        Spacer_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = Spacer_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = Spacer_dataGridView.Rows[e.RowIndex];
                    }
                }
                LE_CL_V = LE_CL_V;
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            Program.DB.deleteValue(DB.type.CalcDB, "Select_WindowSpacer");
            DataGridViewRow row = Spacer_dataGridView.Rows[SelectRow];
            Program.DB.setValue(DB.type.CalcDB, "Select_WindowSpacer", "번호,제품명,구분1,구분2,구분3,고정유리_CL_선형열관류율,개폐유리_CL_선형열관류율,고정유리_LE_선형열관류율,개폐유리_LE_선형열관류율,LE_CL_V",
            "'" + row.Cells[1].Value.ToString() + "','" + row.Cells[2].Value.ToString() + "','" + row.Cells[3].Value.ToString() + "','" + row.Cells[4].Value.ToString() + "','" + row.Cells[5].Value.ToString() + "','"
            + row.Cells[6].Value.ToString() + "','" + row.Cells[7].Value.ToString() + "','" + row.Cells[8].Value.ToString() + "','" + row.Cells[9].Value.ToString() + "','" + LE_CL_V + "'", "번호");

            this.DialogResult = DialogResult.OK;
            this.Close();

        }
    }
}
