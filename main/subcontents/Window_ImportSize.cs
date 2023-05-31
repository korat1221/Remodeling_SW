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
    public partial class Window_ImportSize : Form
    {
        public double Count_SizeInfo;
        ArrayList SelectRow = new ArrayList();

        public Window_ImportSize()
        {
            InitializeComponent();
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
                Program.DB.deleteTable(DB.type.CalcDB, "Import_WindowSize");

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
                                Program.DB.setValue(DB.type.CalcDB, "Import_WindowSize", "창호명칭,창호면적,창호너비,창호높이,고정창유리면적,개폐창유리면적,개폐프레임면적,고정프레임면적,중간프레임면적,고정창유리둘레길이,개폐창유리둘레길이",
                                "'" + token[0] + "','" + token[1] + "','" + token[2] + "','" + token[3] + "','"
                                + token[4] + "','" + token[5] + "','" + token[6] + "','" + token[7] + "','" + token[8] + "','"
                                + token[9] + "','" + token[10] + "'", "창호 명칭");
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

            DataTable table_WindowSize = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Size_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Size_dataGridView.Columns.Add(checkBoxColumn);
            table_WindowSize.Columns.Add("창호명칭", typeof(string));
            table_WindowSize.Columns.Add("창호면적" + Environment.NewLine + "[m²]", typeof(string));
            table_WindowSize.Columns.Add("창호너비" + Environment.NewLine + "[m]", typeof(string));
            table_WindowSize.Columns.Add("창호높이" + Environment.NewLine + "[m]", typeof(string));
            table_WindowSize.Columns.Add("고정창\r\n유리면적", typeof(string));
            table_WindowSize.Columns.Add("개폐창\r\n유리면적" + Environment.NewLine + "[m²]", typeof(string));
            table_WindowSize.Columns.Add("개폐\r\n프레임면적" + Environment.NewLine + "[m²]", typeof(string));
            table_WindowSize.Columns.Add("고정\r\n프레임면적" + Environment.NewLine + "[m²]", typeof(string));
            table_WindowSize.Columns.Add("중간\r\n프레임면적" + Environment.NewLine + "[m²]", typeof(string));
            table_WindowSize.Columns.Add("고정창유리\r\n둘레길이" + Environment.NewLine + "[m]", typeof(string));
            table_WindowSize.Columns.Add("개폐창유리\r\n둘레길이" + Environment.NewLine + "[m]", typeof(string));
            string[][] WinSize = Program.DB.getValue(DB.type.CalcDB, "Import_WindowSize", "창호명칭,창호면적,창호너비,창호높이,고정창유리면적,개폐창유리면적,개폐프레임면적,고정프레임면적,중간프레임면적,고정창유리둘레길이,개폐창유리둘레길이");

            for (int n = 0; n < WinSize.Length; n++)
            {
                table_WindowSize.Rows.Add(WinSize[n][0], WinSize[n][1], WinSize[n][2], WinSize[n][3], WinSize[n][4], WinSize[n][5], WinSize[n][6], WinSize[n][7], WinSize[n][8], WinSize[n][9], WinSize[n][10]);
            }
            Size_dataGridView.DataSource = table_WindowSize;
            Count_SizeInfo = WinSize.Length;
        }

        private void SelectCheckBox()
        {
            foreach (DataGridViewRow row in Size_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
                    SelectRow.Add(row.Index);
                }
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            SelectRow.Clear();
            SelectCheckBox();

            Program.DB.deleteTable(DB.type.CalcDB, "Select_WindowSize");
            for (int n = 0; n < SelectRow.Count; n++)
            {
                DataGridViewRow row = Size_dataGridView.Rows[Convert.ToInt32(SelectRow[n])];
                Program.DB.setValue(DB.type.CalcDB, "Select_WindowSize", "창호명칭,창호면적,창호너비,창호높이,고정창유리면적,개폐창유리면적,개폐프레임면적,고정프레임면적,중간프레임면적,고정창유리둘레길이,개폐창유리둘레길이",
                "'" + row.Cells[1].Value.ToString() + "','" + row.Cells[2].Value.ToString() + "','" + row.Cells[3].Value.ToString() + "','" + row.Cells[4].Value.ToString() + "','"
                + row.Cells[5].Value.ToString() + "','" + row.Cells[6].Value.ToString() + "','" + row.Cells[7].Value.ToString() + "','" + row.Cells[8].Value.ToString() + "','" + row.Cells[9].Value.ToString() + "','"
                + row.Cells[10].Value.ToString() + "','" + row.Cells[11].Value.ToString() + "'", "창호명칭");

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

    }
}
