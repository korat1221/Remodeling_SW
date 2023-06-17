using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents
{
    public partial class CW_ImportSize : Form
    {
        public double Count_SizeInfo;
        int SelectRow ;
        String CWNum, CWName, SizeNum;
        public String[] Select = new string[18];

        public CW_ImportSize(String CWNum, String Name)
        {
            InitializeComponent();
            this.CWNum = CWNum;
            this.CWName = Name;
        }

        private void CSVImport_button_Click(object sender, EventArgs e)
        {
            Import_SizeInfo();
        }

        void Import_SizeInfo()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = ".csv files (*.csv)|*.csv";
            openFileDialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Program.DB.deleteTable(DB.type.CalcDB, "Import_CWSize");

                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                    {
                        int n = 0;
                        while (!sr.EndOfStream)
                        {
                            string[] token = sr.ReadLine().Split(',');
                            if (n == 0)
                            {
                            }
                            else
                            {
                                Program.DB.setValue(DB.type.CalcDB, "Import_CWSize", "명칭,커튼월면적,너비,높이,고정창유리면적,개폐창유리면적,고정창유리둘레길이,개폐창유리둘레길이,패널면적,패널둘레길이,M_T프레임면적,개폐창프레임면적,출입문프레임면적,출입문유리면적,출입문유리둘레길이",
                                "'" + token[0] + "','" + token[1] + "','" + token[2] + "','" + token[3] + "','"
                                + token[4] + "','" + token[5] + "','" + token[6] + "','" + token[7] + "','" + token[8] + "','"
                                + token[9] + "','" + token[10] + "','" + token[11] + "','" + token[12] + "','" + token[13] + "','"
                                + token[14] + "'", "명칭");
                            }
                            n++;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("파일의 형식이 올바르지않습니다. 데이터를 확인해주세요.");
                }
                load_table_SizeInfo();
            }
        }

        void load_table_SizeInfo()
        {

            DataTable table_CWSize = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Size_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Size_dataGridView.Columns.Add(checkBoxColumn);
            table_CWSize.Columns.Add("명칭", typeof(string));
            table_CWSize.Columns.Add("면적" + Environment.NewLine + "[m²]", typeof(string));
            table_CWSize.Columns.Add("너비" + Environment.NewLine + "[m]", typeof(string));
            table_CWSize.Columns.Add("높이" + Environment.NewLine + "[m]", typeof(string));
            table_CWSize.Columns.Add("고정창\r\n유리면적", typeof(string));
            table_CWSize.Columns.Add("개폐창\r\n유리면적" + Environment.NewLine + "[m²]", typeof(string));
            table_CWSize.Columns.Add("고정창유리\r\n둘레길이" + Environment.NewLine + "[m]", typeof(string));
            table_CWSize.Columns.Add("개폐창유리\r\n둘레길이" + Environment.NewLine + "[m]", typeof(string));
            table_CWSize.Columns.Add("패널면적" + Environment.NewLine + "[m²]", typeof(string));
            table_CWSize.Columns.Add("패널\r\n둘레길이" + Environment.NewLine + "[m]", typeof(string));
            table_CWSize.Columns.Add("M/T\r\n프레임면적" + Environment.NewLine + "[m²]", typeof(string));
            table_CWSize.Columns.Add("개폐\r\n프레임면적" + Environment.NewLine + "[m²]", typeof(string));
            table_CWSize.Columns.Add("출입문\r\n프레임면적" + Environment.NewLine + "[m²]", typeof(string));
            table_CWSize.Columns.Add("출입문\r\n유리면적" + Environment.NewLine + "[m²]", typeof(string));
            table_CWSize.Columns.Add("출입문유리\r\n둘레길이" + Environment.NewLine + "[m]", typeof(string));
            string[][] CWSize = Program.DB.getValue(DB.type.CalcDB, "Import_CWSize", "명칭,커튼월면적,너비,높이,고정창유리면적,개폐창유리면적,고정창유리둘레길이,개폐창유리둘레길이,패널면적,패널둘레길이,M_T프레임면적,개폐창프레임면적,출입문프레임면적,출입문유리면적,출입문유리둘레길이");

            for (int n = 0; n < CWSize.Length; n++)
            {
                table_CWSize.Rows.Add(CWSize[n][0], CWSize[n][1], CWSize[n][2], CWSize[n][3], CWSize[n][4], CWSize[n][5], CWSize[n][6], CWSize[n][7], CWSize[n][8], CWSize[n][9], CWSize[n][10], CWSize[n][11], CWSize[n][12], CWSize[n][13], CWSize[n][14]);
            }
            Size_dataGridView.DataSource = table_CWSize;
            Count_SizeInfo = CWSize.Length;
        }

        //데이터그리드뷰 체크박스 선택 시
        private void Size_dataGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Size_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = Size_dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Count_SizeInfo; k++)
                {
                    if (k != row.Index)
                    {
                        Size_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = Size_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = Size_dataGridView.Rows[e.RowIndex];
                    }
                }
            }

        }
       
        private void Save_button_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = Size_dataGridView.Rows[SelectRow];
            SizeNum = "Size_" + CWNum.ToString(); 

            for (int i = 3; i < 17; i++)
            {
                Select[i] = row.Cells[i - 1].Value.ToString();
            }
            Select[0] = SizeNum;
            Select[1] = row.Cells[1].Value.ToString();
            Select[2] = CWNum;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
