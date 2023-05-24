using main.contents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace main.subcontents
{
    public partial class Window_FrameDB : Form
    {
        String FrameType;
        public double Count_FrameDB;
        int SelectRow;

        public Window_FrameDB(String FrameType)
        {
            InitializeComponent();
            FrameType = FrameType;
            load_table_FrameDB(FrameType);
        }
        void load_table_FrameDB(String FrameType)
        {
            DataTable table_WindowFrame = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Frame_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Frame_dataGridView.Columns.Add(checkBoxColumn);
            table_WindowFrame.Columns.Add("번호", typeof(string));
            table_WindowFrame.Columns.Add("제품명", typeof(string));
            table_WindowFrame.Columns.Add("제조사", typeof(string));
            table_WindowFrame.Columns.Add("프레임종류", typeof(string));
            table_WindowFrame.Columns.Add("프레임재료", typeof(string));
            table_WindowFrame.Columns.Add("개폐부프레임\r\n열관류율" + Environment.NewLine + "Uf,A[W/m2∙K]", typeof(string));
            table_WindowFrame.Columns.Add("고정부프레임\r\n열관류율" + Environment.NewLine + "Uf,B[W/m2∙K]", typeof(string));
            table_WindowFrame.Columns.Add("중간바프레임\r\n열관류율" + Environment.NewLine + "Uf,C[W/m2∙K]", typeof(string));
            table_WindowFrame.Columns.Add("개폐부\r\n프레임두께" + Environment.NewLine + "dA[m]", typeof(string));
            table_WindowFrame.Columns.Add("고정부\r\n프레임두께" + Environment.NewLine + "dB[m]", typeof(string));
            table_WindowFrame.Columns.Add("중간바\r\n프레임두께" + Environment.NewLine + "dC[m]", typeof(string));
            string[][] WinFrame = Program.DB.getValue(DB.type.BaseDB, "창호프레임", "번호,제품명,제조사,프레임종류,프레임재료,개폐부프레임열관류율,고정부프레임열관류율,중간바프레임열관류율,개폐부프레임두께,고정부프레임두께,중간바프레임두께", "프레임종류 ='" + FrameType + "'");

            for (int n = 0; n < WinFrame.Length; n++)
            {
                table_WindowFrame.Rows.Add(WinFrame[n][0], WinFrame[n][1], WinFrame[n][2], WinFrame[n][3], WinFrame[n][4], WinFrame[n][5], WinFrame[n][6], WinFrame[n][7], WinFrame[n][8], WinFrame[n][9], WinFrame[n][10]);
            }
            Frame_dataGridView.DataSource = table_WindowFrame;
            Count_FrameDB = WinFrame.Length;
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
            Program.DB.deleteValue(DB.type.CalcDB, "Select_WindowFrame");
            DataGridViewRow row = Frame_dataGridView.Rows[SelectRow];
            Program.DB.setValue(DB.type.CalcDB, "Select_WindowFrame", "번호,제품명,제조사,프레임종류,프레임재료,개폐부프레임열관류율,고정부프레임열관류율,중간바프레임열관류율,개폐부프레임두께,고정부프레임두께,중간바프레임두께",
            "'" + row.Cells[1].Value.ToString() + "','" + row.Cells[2].Value.ToString() + "','" + row.Cells[3].Value.ToString() + "','" + row.Cells[4].Value.ToString() + "','"
            + row.Cells[5].Value.ToString() + "','" + row.Cells[6].Value.ToString() + "','" + row.Cells[7].Value.ToString() + "','" + row.Cells[8].Value.ToString() + "','" + row.Cells[9].Value.ToString() + "','"
            + row.Cells[10].Value.ToString() + "','" + row.Cells[11].Value.ToString() + "'", "번호");

            this.DialogResult = DialogResult.OK;
            this.Close();

        }
    }
}
