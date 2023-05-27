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
        public String[] Select_WindowInstall = new string[9];
        String InstallType, SingleDoubleType, FrameMaterial;
        string[][] WinInstall;

        public Window_InstallDB(String InstallType, String SingleDoubleType, String FrameMaterial)
        {
            InitializeComponent();
            this.InstallType = InstallType;
            this.SingleDoubleType = SingleDoubleType;
            this.FrameMaterial = FrameMaterial;
            load_table_InstallDB();
        }
        public Window_InstallDB(String InstallType)
        {
            InitializeComponent();
            this.InstallType = InstallType;
            load_table_InstallDB();
        }

        void load_table_InstallDB()
        {
            DataTable table_WindowInstall = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Install_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Install_dataGridView.Columns.Add(checkBoxColumn);
            table_WindowInstall.Columns.Add("번호", typeof(string));
            table_WindowInstall.Columns.Add("제품명", typeof(string));
            table_WindowInstall.Columns.Add("구분1", typeof(string));
            table_WindowInstall.Columns.Add("구분2", typeof(string));
            table_WindowInstall.Columns.Add("구분3", typeof(string));
            table_WindowInstall.Columns.Add("구분4", typeof(string));
            table_WindowInstall.Columns.Add("상부설치\r\n선형열관류율" + Environment.NewLine + "Ψg,fix\r\n[W/m·K]", typeof(string));
            table_WindowInstall.Columns.Add("측면설치\r\n선형열관류율" + Environment.NewLine + "Ψg,t\r\n[W/m·K]", typeof(string));
            table_WindowInstall.Columns.Add("하부설치\r\n선형열관류율" + Environment.NewLine + "Ψg,t\r\n[W/m·K]", typeof(string));

            if (InstallType != null && SingleDoubleType != null && FrameMaterial != null)
            {
                WinInstall = Program.DB.getValue(DB.type.BaseDB, "창호설치열교", "번호,DB유형,제품명,구분1,구분2,구분3,구분4,상부설치선형열관류율,측면설치선형열관류율,하부설치선형열관류율", "구분1 = '" + InstallType + "'AND 구분2 = '" + FrameMaterial + "'AND 구분3 ='" + SingleDoubleType + "'");
            }
            else
            {
                WinInstall = Program.DB.getValue(DB.type.BaseDB, "창호설치열교", "번호,DB유형,제품명,구분1,구분2,구분3,구분4,상부설치선형열관류율,측면설치선형열관류율,하부설치선형열관류율", "구분1 = '" + InstallType  + "'");
            }

            for (int n = 0; n < WinInstall.Length; n++)
            {
                table_WindowInstall.Rows.Add(WinInstall[n][0], WinInstall[n][2], WinInstall[n][3], WinInstall[n][4], WinInstall[n][5], WinInstall[n][6], WinInstall[n][7], WinInstall[n][8], WinInstall[n][9]);
            }
            Install_dataGridView.DataSource = table_WindowInstall;
            Count_InstallDB = WinInstall.Length;
        }

        //데이터그리드뷰 체크박스 선택 시
        private void Frame_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Install_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = Install_dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Count_InstallDB; k++)
                {
                    if (k != row.Index)
                    {
                        Install_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = Install_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = Install_dataGridView.Rows[e.RowIndex];
                    }
                }
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = Install_dataGridView.Rows[SelectRow];
            for (int i = 1; i < row.Cells.Count; i++)
            {
                Select_WindowInstall[i - 1] = row.Cells[i].Value.ToString();
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
