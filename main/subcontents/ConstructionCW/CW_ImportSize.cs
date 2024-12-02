using Eagle._Constants;
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
        double width; double height; double percent_open; double n_hori; double n_ver; double percent_panel; double door_width; double door_height;
        double df_mt, df_open, df_door;
        public double Count_SizeInfo;
        int SelectRow;
        String CWNum, CWName, SizeNum;
        public String[] Select = new string[18];

        public CW_ImportSize(String CWNum, String Name, double df_mt, double df_open, double df_door)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            this.CWNum = CWNum;
            this.CWName = Name;
            Name_textBox.Text = CWName;
            this.df_mt = df_mt;
            this.df_open = df_open;
            this.df_door = df_door;
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
                Program.DB.deleteTable(DB.type.ProjDB, "Import_CWSize");

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
                                Program.DB.setValue(DB.type.ProjDB, "Import_CWSize", "명칭,커튼월면적,너비,높이,고정창유리면적,개폐창유리면적,고정창유리둘레길이,개폐창유리둘레길이,패널면적,패널둘레길이,M_T프레임면적,개폐창프레임면적,출입문프레임면적,출입문유리면적,출입문유리둘레길이",
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
            new StackedHeaderDecorator(Size_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Size_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Size_dataGridView.Columns.Add(checkBoxColumn);

            Size_dataGridView.Columns.Add("A1", "명칭");
            Size_dataGridView.Columns.Add("A2", "커튼월전체.면적.[m"+Program.UTIL.Subscript(2, true)+"]");
            Size_dataGridView.Columns.Add("A3", "커튼월전체.너비.[m]");
            Size_dataGridView.Columns.Add("A4", "커튼월전체.높이.[m]");
            Size_dataGridView.Columns.Add("A5", "유리.고정창 면적.[m"+Program.UTIL.Subscript(2, true)+"]");
            Size_dataGridView.Columns.Add("A6", "유리.개폐창 면적.[m"+Program.UTIL.Subscript(2, true)+"]");
            Size_dataGridView.Columns.Add("A5", "유리.고정창 둘레길이.[m]");
            Size_dataGridView.Columns.Add("A6", "유리.개폐창 둘레길이.[m]");
            Size_dataGridView.Columns.Add("A7", "패널.면적.[m"+Program.UTIL.Subscript(2, true)+"]");
            Size_dataGridView.Columns.Add("A8", "패널.둘레길이.[m]");
            Size_dataGridView.Columns.Add("A9", "프레임.M/T면적.[m"+Program.UTIL.Subscript(2, true)+"]");
            Size_dataGridView.Columns.Add("A10", "프레임.개폐면적.[m"+Program.UTIL.Subscript(2, true)+"]");
            Size_dataGridView.Columns.Add("A11", "출입문.프레임면적.[m"+Program.UTIL.Subscript(2, true)+"]");
            Size_dataGridView.Columns.Add("A12", "출입문.유리면적.[m"+Program.UTIL.Subscript(2, true)+"]");
            Size_dataGridView.Columns.Add("A13", "출입문.둘레길이.[m]");

            string[][] CWSize = Program.DB.getValue(DB.type.ProjDB, "Import_CWSize", "명칭,커튼월면적,너비,높이,고정창유리면적,개폐창유리면적,고정창유리둘레길이,개폐창유리둘레길이,패널면적,패널둘레길이,M_T프레임면적,개폐창프레임면적,출입문프레임면적,출입문유리면적,출입문유리둘레길이");
            if (CWSize.Length > 0)
            {
                for (int n = 0; n < CWSize.Length; n++)
                {
                    Size_dataGridView.Rows.Add();
                    int nRow = Size_dataGridView.Rows.Count - 1;
                    Size_dataGridView.Rows[nRow].Cells[1].Value = CWSize[n][0];
                    for (int k = 1; k < 15; k++)
                    {
                        if (CWSize[n][k] != "" && Convert.ToDouble(CWSize[n][k]) > 0)
                        { Size_dataGridView.Rows[nRow].Cells[k + 1].Value = Convert.ToDouble(CWSize[n][k]).ToString("0.00"); }
                        else
                        {
                            Size_dataGridView.Rows[nRow].Cells[k + 1].Value = 0;
                        }
                    }
                }
            }
            Count_SizeInfo = CWSize.Length;
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

        //데이터그리드뷰 체크박스 선택 시
        private void Size_dataGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Size_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if(Size_dataGridView.Rows.Count >0)
            {
                DataGridViewRow row = Size_dataGridView.Rows[SelectRow];
                SizeNum = "Size_" + CWNum.ToString();

                for (int i = 3; i < 17; i++)
                {
                    if (row.Cells[i - 1].Value !=null)
                    {
                        Select[i] = row.Cells[i - 1].Value.ToString();
                    }                    
                }
                Select[0] = SizeNum;
                if(row.Cells[1].Value!= null)
                {
                    Select[1] = row.Cells[1].Value.ToString();
                }                
                Select[2] = CWNum;

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
            if (Width > 0 && height > 0 && n_hori > 0 && n_ver > 0)
            {
                double Area = width * height;  //커튼월 면적 
                if (percent_open + percent_panel > 100)
                {
                    MessageBox.Show("개폐창 또는 패널 비율을 확인하세요.");
                }
                else if (Area*(1-percent_open/100-percent_panel/100)  < door_width * door_height)
                {
                    MessageBox.Show("출입문 사이즈 정보를 확인하세요.");
                }
                else
                {
                    double Af_mt = Math.Max(height * df_mt + n_hori * (height - df_open) * df_mt + (width - 2 * df_mt) * df_open + n_ver * (width - df_mt) * df_open - n_hori * n_ver * df_mt * df_open, 0); // m/t 프레임(A)면적

                    double Af_open = Math.Max(((((width - df_mt * (n_hori + 1)) / n_hori + 2 * df_open) * ((height - df_mt * (n_ver + 1)) / n_ver + 2 * df_open) - (((width - df_mt * (n_hori + 1)) / n_hori) * (height - df_mt * (n_ver + 1))) / n_ver) * n_hori * n_ver * percent_open) / 100, 0); // 개폐창프레임(B)면적

                    double Ag_open = Math.Max((Area * (percent_open / 100)) - Af_open, 0);//개폐창 면적 

                    double Lg_fix = Math.Max(((width - 2 * df_open) * 2 + (height - 2 * df_open) * 2 + ((n_hori - 1) * height + (n_ver - 1) * width)) * (1 - percent_open / 100 - percent_panel / 100), 0);//고정창 길이 

                    double Lg_open = Math.Max((((width - 2 * df_open) * 2 + (height - 2 * df_open) * 2 + ((n_hori - 1) * height + (n_ver - 1) * width)) * percent_open) / 100, 0); // 개폐부 길이

                    double Ag_panel = Math.Max((Area * (percent_panel / 100)), 0);//패널면적 

                    double Lg_panel = Math.Max(((width - 2 * df_open) * 2 + (height - 2 * df_open) * 2 + ((n_hori - 1) * height + (n_ver - 1) * width)) * (percent_panel / 100), 0);//패널부 길이 

                    double Af_door = Math.Max((door_width * door_height) - (door_width - 2 * df_door) * (door_height - 2 * df_door), 0);

                    double Ag_door = Math.Max((door_width * door_height) - Af_door, 0);

                    double Lg_door = Math.Max((door_width + door_height - 4 * df_door) * 2, 0);

                    double Ag_fix = Math.Max(Area - (Af_mt + Af_open + Ag_open + Ag_panel + Af_door + Ag_door), 0); // 고정창 면적

                    if ((Af_open + Af_mt) > 0 && (Ag_fix + Ag_open) > 0)
                    {
                        Program.DB.setValue(DB.type.ProjDB, "Import_CWSize", "명칭,커튼월면적,너비,높이," +
                            "고정창유리면적,개폐창유리면적,고정창유리둘레길이,개폐창유리둘레길이," +
                            "패널면적,패널둘레길이," +
                            "M_T프레임면적,개폐창프레임면적," +
                            "출입문프레임면적,출입문유리면적,출입문유리둘레길이",
                                     "'" + CWName + "','" + Area + "','" + width + "','" + height + "','"
                                     + Ag_fix + "','" + Ag_open + "','" + Lg_fix + "','" + Lg_open + "','"
                                     + Ag_panel + "','" + Lg_panel + "','"
                                     + Af_mt + "','" + Af_open + "','"
                                     + Af_door + "','" + Ag_door + "','" + Lg_door + "'", "명칭");
                    }
                }
            }
            else
            {
                MessageBox.Show("커튼월창 너비, 높이, 가로/세로 칸 수는 필수 입력입니다.");
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
            if (percent_open!= 0 && percent_open < 1)
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

        private void percent_panel_textBox_TextChanged(object sender, EventArgs e)
        {
            percent_panel = Program.UTIL.textBox_doubleComa(percent_panel_textBox, false, 1);
            if (percent_panel != 0 && percent_panel < 1)
            {
                MessageBox.Show("퍼센트 단위로 입력하세요.(Ex : 90.1% ⇒ 90.1)");
            }
        }

        private void door_width_textBox_TextChanged(object sender, EventArgs e)
        {
            door_width = Program.UTIL.textBox_doubleComa(door_width_textBox, false, 2);
        }

        private void door_height_textBox_TextChanged(object sender, EventArgs e)
        {
            door_height = Program.UTIL.textBox_doubleComa(door_height_textBox, false, 2);
        }

    }
}
