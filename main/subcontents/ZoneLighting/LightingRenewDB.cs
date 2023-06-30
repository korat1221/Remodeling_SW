using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace main.subcontents.ZoneLighting
{
    public partial class RenewDB : Form
    {

        int SelectRow;
        public String[] Select_Renew = new string[8];
        double Count_RenewDB;
        //int SelectRow;

        public RenewDB()
        {
            InitializeComponent();
            load_table_RenewDB();

            //집광채광 종류 콤보박스
            RenewType_comboBox.Items.Clear();
            RenewType_comboBox.Items.Add("광덕트");
            RenewType_comboBox.Items.Add("프리즘");
            RenewType_comboBox.Items.Add("실내루버형");
            RenewType_comboBox.SelectedIndex = 0;

        }

        void load_table_RenewDB()
        {
            DataTable table_Renew = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Renew_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Renew_dataGridView.Columns.Add(checkBoxColumn);
            table_Renew.Columns.Add("번호", typeof(string));
            table_Renew.Columns.Add("집광채광명칭", typeof(string));
            table_Renew.Columns.Add("집광채광종류", typeof(string));
            table_Renew.Columns.Add("제조사", typeof(string));
            table_Renew.Columns.Add("집광채광 효율" + Environment.NewLine + "[-]", typeof(string));
            table_Renew.Columns.Add("산광부 가로 길이" + Environment.NewLine + "[m]", typeof(string));
            table_Renew.Columns.Add("산광부 세로 길이" + Environment.NewLine + "[m]", typeof(string));
            table_Renew.Columns.Add("산광부 면적" + Environment.NewLine + "[m2]", typeof(string));



            string[][] Renew = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_집광채광DB", "번호,집광채광명칭,집광채광종류,제조사,집광채광효율,산광부가로길이,산광부세로길이,산광부면적", "");

            for (int n = 0; n < Renew.Length; n++)
            {
                table_Renew.Rows.Add(Renew[n][0], Renew[n][1], Renew[n][2], Renew[n][3], Renew[n][4], Renew[n][5], Renew[n][6], Renew[n][7]);
            }
            Renew_dataGridView.DataSource = table_Renew;
            Count_RenewDB = Renew.Length;

        }

        //데이터그리드뷰 체크박스 선택 시
        private void Renew_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Renew_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = Renew_dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Count_RenewDB; k++)
                {
                    if (k != row.Index)
                    {
                        Renew_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = Renew_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = Renew_dataGridView.Rows[e.RowIndex];
                    }
                }
            }
        }

       
        private void Save_button_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = Renew_dataGridView.Rows[SelectRow];
            Select_Renew[0] = row.Cells[1].Value.ToString(); //번호
            Select_Renew[1] = row.Cells[2].Value.ToString(); //집광채광명칭
            Select_Renew[2] = row.Cells[3].Value.ToString(); //집광채광종류
            Select_Renew[3] = row.Cells[4].Value.ToString(); //제조사
            Select_Renew[4] = row.Cells[5].Value.ToString(); //집광채광효율
            Select_Renew[5] = row.Cells[6].Value.ToString(); //산광부가로길이
            Select_Renew[6] = row.Cells[7].Value.ToString(); //산광부세로길이
            Select_Renew[7] = row.Cells[8].Value.ToString(); //산광부면적


            this.DialogResult = DialogResult.OK;
            this.Close();

        }

      
    }
}
