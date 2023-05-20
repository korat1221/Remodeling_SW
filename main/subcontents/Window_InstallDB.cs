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
    public partial class Window_InstallDB : Form
    {
        String FrameType;
        double Count_InstallDB;
        int SelectRow;
        public Window_InstallDB(String Install, String SingleDoubleType, String FrameMaterial)
        {
            InitializeComponent();
            load_table_InstallDB(Install, SingleDoubleType, FrameMaterial);
        }
        void load_table_InstallDB(String Install, String SingleDoubleType, String FrameMaterial)
        {
            DataTable table_WindowInstall = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Frame_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Frame_dataGridView.Columns.Add(checkBoxColumn);
            table_WindowInstall.Columns.Add("번호", typeof(string));
            table_WindowInstall.Columns.Add("제품명", typeof(string));
            table_WindowInstall.Columns.Add("구분1", typeof(string));
            table_WindowInstall.Columns.Add("구분2", typeof(string));
            table_WindowInstall.Columns.Add("구분3", typeof(string));
            table_WindowInstall.Columns.Add("구분4", typeof(string));
            table_WindowInstall.Columns.Add("상부설치\r\n선형열관류율" + Environment.NewLine + "Ψg,fix[W/m·K]", typeof(string));
            table_WindowInstall.Columns.Add("측면설치\r\n선형열관류율" + Environment.NewLine + "Ψg,t[W/m·K]", typeof(string));
            table_WindowInstall.Columns.Add("하부설치\r\n선형열관류율" + Environment.NewLine + "Ψg,t[W/m·K]", typeof(string));
            string[][] WinInstall = Program.DB.getValue(DB.type.BaseDB, "창호설치열교", "번호,DB유형,제품명,구분1,구분2,구분3,구분4,상부설치선형열관류율,측면설치선형열관류율,하부설치선형열관류율", "구분1 = '" + Install + "'AND 구분2 = '" + FrameMaterial + "'AND 구분3 ='" + SingleDoubleType + "'");

            for (int n = 0; n < WinInstall.Length; n++)
            {
                table_WindowInstall.Rows.Add(WinInstall[n][0], WinInstall[n][2], WinInstall[n][3], WinInstall[n][4], WinInstall[n][5], WinInstall[n][6], WinInstall[n][7], WinInstall[n][8], WinInstall[n][9]);
            }
            Frame_dataGridView.DataSource = table_WindowInstall;
            Count_InstallDB = WinInstall.Length;
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
                for (int k = 0; k < Count_InstallDB; k++)
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

            DataGridViewRow row = Frame_dataGridView.Rows[SelectRow];
            Program.DB.setValue(DB.type.CalcDB, "Select_WindowInstall", "번호,제품명,구분1,구분2,구분3,구분4,상부설치선형열관류율,측면설치선형열관류율,하부설치선형열관류율",
            "'" + row.Cells[0].Value.ToString() + "','" + row.Cells[1].Value.ToString() + "','" + row.Cells[2].Value.ToString() + "','" + row.Cells[3].Value.ToString() + "','" + row.Cells[4].Value.ToString() + "','"
            + row.Cells[5].Value.ToString() + "','" + row.Cells[6].Value.ToString() + "','" + row.Cells[7].Value.ToString() + "','" + row.Cells[8].Value.ToString() + "'", "번호");

            this.DialogResult = DialogResult.OK;
            this.Close();

        }
    }
}
