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
        string Name; double width; double height; double percent_open; double n_hori; double n_ver;
        ArrayList SelectRow = new ArrayList();
        String 상위창호기호, 상위창호명칭;
        double df_open; double df_fix; double df_btw;
        public Window_ImportSize(String WinNum, String Name, double df_open, double df_fix, double df_btw)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            this.상위창호기호 = WinNum;
            this.상위창호명칭 = Name;
            this.df_open = df_open;
            this.df_fix = df_fix;
            this.df_btw = df_btw;
            load_table_SizeInfo();
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
                Program.DB.deleteTable(DB.type.ProjDB, "Import_WindowSize");

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
                                Program.DB.setValue(DB.type.ProjDB, "Import_WindowSize", "창호명칭,창호면적,창호너비,창호높이,고정창유리면적,개폐창유리면적,개폐프레임면적,고정프레임면적,중간프레임면적,고정창유리둘레길이,개폐창유리둘레길이",
                                "'" + token[0] + "','" + token[1] + "','" + token[2] + "','" + token[3] + "','"
                                + token[4] + "','" + token[5] + "','" + token[6] + "','" + token[7] + "','" + token[8] + "','"
                                + token[9] + "','" + token[10] + "'", "창호명칭");
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
            new StackedHeaderDecorator(Size_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Size_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Size_dataGridView.Columns.Add(checkBoxColumn);

            Size_dataGridView.Columns.Add("A1", "창호명칭");
            Size_dataGridView.Columns.Add("A2", "창호전체.면적.[m²]");
            Size_dataGridView.Columns.Add("A3", "창호전체.너비.[m]");
            Size_dataGridView.Columns.Add("A4", "창호전체.높이.[m]");
            Size_dataGridView.Columns.Add("A5", "유리면적.고정창.[m²]");
            Size_dataGridView.Columns.Add("A6", "유리면적.개폐창.[m²]");
            Size_dataGridView.Columns.Add("A7", "프레임면적.개폐프레임.[m²]");
            Size_dataGridView.Columns.Add("A8", "프레임면적.고정프레임.[m²]");
            Size_dataGridView.Columns.Add("A9", "프레임면적.중간프레임.[m²]");
            Size_dataGridView.Columns.Add("A10", "유리 둘레길이.고정창.[m]");
            Size_dataGridView.Columns.Add("A11", "유리 둘레길이.개폐창.[m]");

            string[][] WinSize = Program.DB.getValue(DB.type.ProjDB, "Import_WindowSize", "창호명칭,창호면적,창호너비,창호높이,고정창유리면적,개폐창유리면적,개폐프레임면적,고정프레임면적,중간프레임면적,고정창유리둘레길이,개폐창유리둘레길이");
            if (WinSize.Length > 0)
            {
                for (int n = 0; n < WinSize.Length; n++)
                {
                    Size_dataGridView.Rows.Add();
                    int nRow = Size_dataGridView.Rows.Count - 1;
                    Size_dataGridView.Rows[nRow].Cells[1].Value = WinSize[n][0];
                    for (int k = 1; k < 11; k++)
                    {
                        if (WinSize[n][k] != "" && Convert.ToDouble(WinSize[n][k]) > 0)
                        { Size_dataGridView.Rows[nRow].Cells[k + 1].Value = Convert.ToDouble(WinSize[n][k]).ToString("0.00"); }
                        else
                        {
                            Size_dataGridView.Rows[nRow].Cells[k + 1].Value = 0;
                        }
                    }
                }
            }
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
            String 명칭;
            String 번호;

            String[][] Size = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "번호", "상위창호번호 = '" + 상위창호기호 + "'");
            if (Size.Length > 0)
            {
                Program.DB.deleteValue(DB.type.ProjDB, "SubWindow", "상위창호번호 = '" + 상위창호기호 + "'");
            }
            else
            {
            }

            for (int n = 0; n < SelectRow.Count; n++)
            {
                DataGridViewRow row = Size_dataGridView.Rows[Convert.ToInt32(SelectRow[n])];
                번호 = 상위창호기호.ToString() + "_" + (n + 1).ToString();
                명칭 = 상위창호명칭 + "_" + row.Cells[1].Value.ToString();

                double 창호면적 = Convert.ToDouble(row.Cells[2].Value);
                double 고정유리면적 = Convert.ToDouble(row.Cells[5].Value);
                double 개폐유리면적 = Convert.ToDouble(row.Cells[6].Value);
                double 유리면적비 = (고정유리면적 + 개폐유리면적) / 창호면적;
                string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
                Program.DB.setValue(DB.type.ProjDB, "SubWindow", "번호,프로젝트유형,명칭,상위창호번호,창호면적,창호너비,창호높이,고정유리면적,개폐유리면적,개폐프레임면적,고정프레임면적,중간프레임면적,고정유리둘레길이,개폐유리둘레길이,유리면적비",
                "'" + 번호 + "','" + 프로젝트유형[0][0] + "','" + 명칭 + "','" + 상위창호기호 + "','" + row.Cells[2].Value.ToString() + "','" + row.Cells[3].Value.ToString() + "','" + row.Cells[4].Value.ToString() + "','"
                + row.Cells[5].Value.ToString() + "','" + row.Cells[6].Value.ToString() + "','" + row.Cells[7].Value.ToString() + "','" + row.Cells[8].Value.ToString() + "','" + row.Cells[9].Value.ToString() + "','"
                + row.Cells[10].Value.ToString() + "','" + row.Cells[11].Value.ToString() + "','" + 유리면적비 + "'", "번호");

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void Calc_button_Click(object sender, EventArgs e)
        {
            Calc_Area();
            load_table_SizeInfo();
        }
        private void Calc_Area()
        {
            if (Name != null && Name != "" && width > 0 && height > 0 && n_hori > 0 && n_ver > 0)
            {
                double Af_open = Math.Max(((width - 2 * df_fix) * 2 + (height - 2 * df_fix) * 2) * percent_open / 100 * df_open, 0);
                double Af_fix = Math.Max(((width - 2 * df_fix) * 2 + (height - 2 * df_fix) * 2) * (1 - percent_open / 100) * df_fix, 0);
                double Af_btw = Math.Max(((n_hori - 1) * height + (n_ver - 1) * width) * df_btw, 0);
                double Area = width * height;
                double Ag_fix = Math.Max((Area - (Af_open + Af_fix + Af_btw)) * (1 - percent_open / 100), 0);
                double Ag_open = Math.Max(Area - (Af_open + Af_fix + Af_btw + Ag_fix), 0);
                double Lg_fix = Math.Max(((width - 2 * df_fix) * 2 + (height - 2 * df_fix) * 2 + ((n_hori - 1) * height + (n_ver - 1) * width)) * (1 - percent_open / 100), 0);
                double Lg_open = Math.Max(((width - 2 * df_fix) * 2 + (height - 2 * df_fix) * 2 + ((n_hori - 1) * height + (n_ver - 1) * width)) * (percent_open / 100), 0);

                if ((Af_open + Af_fix) > 0 && (Ag_fix + Ag_open) > 0)
                {
                    Program.DB.setValue(DB.type.ProjDB, "Import_WindowSize", "창호명칭,창호면적,창호너비,창호높이,고정창유리면적,개폐창유리면적,개폐프레임면적,고정프레임면적,중간프레임면적,고정창유리둘레길이,개폐창유리둘레길이",
                                    "'" + Name + "','" + Area + "','" + width + "','" + height + "','"
                                    + Ag_fix + "','" + Ag_open + "','" + Af_open + "','" + Af_fix + "','" + Af_btw + "','"
                                    + Lg_fix + "','" + Lg_open + "'", "창호명칭");
                }
            }
            else
            {
                MessageBox.Show("창호 명칭, 너비, 높이, 가로/세로 칸 수는 필수 입력입니다.");
            }

        }

        private void Name_textBox_TextChanged(object sender, EventArgs e)
        {
            if (Name_textBox.Text != null)
            {
                Name = Name_textBox.Text.ToString();
            }
        }

        private void width_textBox_TextChanged(object sender, EventArgs e)
        {
            width = Program.UTIL.textBox_doubleComa(width_textBox, false, 2);
        }

        private void height_textBox_TextChanged(object sender, EventArgs e)
        {
            height = Program.UTIL.textBox_doubleComa(height_textBox, false, 2);
        }

        private void percent_open_textBox_TextChanged(object sender, EventArgs e)
        {
            percent_open = Program.UTIL.textBox_doubleComa(percent_open_textBox, false, 1);
            if (percent_open < 1)
            {
                MessageBox.Show("퍼센트 단위로 입력하세요.(Ex : 90.1% ⇒ 90.1)");
            }
        }

        private void n_hori_textBox_TextChanged(object sender, EventArgs e)
        {
            n_hori = Program.UTIL.textBox_doubleComa(n_hori_textBox, false, 0);
        }

        private void n_ver_textBox_TextChanged(object sender, EventArgs e)
        {
            n_ver = Program.UTIL.textBox_doubleComa(n_ver_textBox, false, 0);
        }

    }
}
