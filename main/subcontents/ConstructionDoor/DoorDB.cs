using main.info;
using main.subcontents.ConstructionCW;
using main.subcontents.HeatingSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents
{
    public partial class DoorDB : Form
    {
        DataGridViewCheckBoxColumn Door_checkBoxColumn = new DataGridViewCheckBoxColumn();
        double 열전도율, R, d, U, Width, Height, 열저항, d2; //d2 = 열저항 계산시 두께 
        double yi;
        int nRow;
        public String[] Select_Door = new String[14];
        double[] x = { 0, 5, 7, 10, 15, 25, 50, 100, 300 }; //두께
        double[] y = { 0, 0.11, 0.13, 0.15, 0.17, 0.18, 0.18, 0.18, 0.18 }; //벽체 열저항


        public DoorDB(String DoorNum, String Select0, String Select1, String Select2, String Select3, String Select4, String Select5, String Select6, String Select7, String Select8, String Select9, String Select10, String Select11, String Select12, String Select13)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            Create_Table();
            if (Select0 != null)
            {
                Door_dataGridView.Rows[nRow].Cells[1].Value = Select0;
                Door_dataGridView.Rows[nRow].Cells[2].Value = Select1;
                Door_dataGridView.Rows[nRow].Cells[4].Value = Select2;
                Door_dataGridView.Rows[nRow].Cells[5].Value = Select3;
                Door_dataGridView.Rows[nRow].Cells[6].Value = Select4;
                Door_dataGridView.Rows[nRow].Cells[7].Value = Select5;
                Door_dataGridView.Rows[nRow].Cells[8].Value = Select6;
                Door_dataGridView.Rows[nRow].Cells[9].Value = Select7;
                Door_dataGridView.Rows[nRow].Cells[10].Value = Select8;
                Door_dataGridView.Rows[nRow].Cells[11].Value = Select9;
                Door_dataGridView.Rows[nRow].Cells[13].Value = Select10;
                Door_dataGridView.Rows[nRow].Cells[14].Value = Select11;
                Door_dataGridView.Rows[nRow].Cells[15].Value = Select12;
                Door_dataGridView.Rows[nRow].Cells[16].Value = Select13;

            }

            // LoadData(DoorNum);

        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }
        private bool Design_dataGridView(DataGridViewCell cell, int column, int row)
        {
            if (column == 3 || column == 7 || column == 8 || column == 12)
            {
                cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                return true;
            }

            else return false;
        }
        public void Create_Table()
        {
            new StackedHeaderDecorator(Door_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, Design_dataGridView);
            Door_dataGridView.Columns.Clear();
            Door_dataGridView.Rows.Clear();
            Door_checkBoxColumn.HeaderText = "선택";
            Door_checkBoxColumn.Name = "check";
            Door_dataGridView.Columns.Add(Door_checkBoxColumn);

            Door_dataGridView.Columns.Add("A1", "번호");
            Door_dataGridView.Columns.Add("A2", "DB.유형");
            Door_dataGridView.Columns.Add("A3", "DB.+");
            Door_dataGridView.Columns.Add("A4", "제품명");
            Door_dataGridView.Columns.Add("A5", "제조사");
            Door_dataGridView.Columns.Add("A6", "문틀.내부"); // 콤보박스
            Door_dataGridView.Columns.Add("A7", "문틀.상부열관류율.[W/m∙K]");
            Door_dataGridView.Columns.Add("A8", "문틀.하부열관류율.[W/m∙K]");
            Door_dataGridView.Columns.Add("A9", "문짝.종류"); //콤보박스
            Door_dataGridView.Columns.Add("A10", "문짝.내부.유형"); //콤보박스
            Door_dataGridView.Columns.Add("A11", "문짝.내부.종류");// 단열재 결정값 
            Door_dataGridView.Columns.Add("A12", "문짝.내부.+"); //버튼
            Door_dataGridView.Columns.Add("A13", "문짝.두께.[mm]");
            Door_dataGridView.Columns.Add("A14", "문짝치수.가로.[mm]");
            Door_dataGridView.Columns.Add("A15", "문짝치수.세로.[mm]");
            Door_dataGridView.Columns.Add("A16", "문열관류율.[W/m" + Program.UTIL.Subscript(2, true) + "∙K]");
            Door_dataGridView.Columns[0].Width = 40;
            Door_dataGridView.Columns[1].Width = 50;
            Door_dataGridView.Columns[2].Width = 60;
            Door_dataGridView.Columns[3].Width = 25;
            Door_dataGridView.Columns[4].Width = 80;
            Door_dataGridView.Columns[5].Width = 80;
            Door_dataGridView.Columns[6].Width = 60;
            Door_dataGridView.Columns[7].Width = 80;
            Door_dataGridView.Columns[8].Width = 80;
            Door_dataGridView.Columns[9].Width = 60;
            Door_dataGridView.Columns[10].Width = 60;
            Door_dataGridView.Columns[11].Width = 100;
            Door_dataGridView.Columns[12].Width = 25;
            Door_dataGridView.Columns[13].Width = 60;
            Door_dataGridView.Columns[14].Width = 60;
            Door_dataGridView.Columns[15].Width = 60;
            Door_dataGridView.Columns[16].Width = 80;

            DataGridViewComboBoxCell 유형Combo = new DataGridViewComboBoxCell();
            유형Combo.Items.Add("도면");
            유형Combo.Items.Add("기본");

            nRow = Door_dataGridView.Rows.Add();
            Load_Num();
            Door_dataGridView.Rows[nRow].Cells[2] = 유형Combo;

        }
        private void User()
        {
            DataGridViewComboBoxCell 문틀Combo = new DataGridViewComboBoxCell();
            문틀Combo.Items.Add("목재");
            문틀Combo.Items.Add("공기");
            문틀Combo.Items.Add("시멘트몰탈");
            문틀Combo.Items.Add("단열재");
            Door_dataGridView.Rows[nRow].Cells[6] = 문틀Combo;


            DataGridViewComboBoxCell 문짝Combo = new DataGridViewComboBoxCell();
            문짝Combo.Items.Add("목재문");
            문짝Combo.Items.Add("철문");
            문짝Combo.Items.Add("단열문");
            Door_dataGridView.Rows[nRow].Cells[9] = 문짝Combo;


            DataGridViewComboBoxCell 문짝내부Combo = new DataGridViewComboBoxCell();
            문짝내부Combo.Items.Add("공기");
            문짝내부Combo.Items.Add("단열재");
            Door_dataGridView.Rows[nRow].Cells[10] = 문짝내부Combo;
        }


        private void Load_Num()
        {
            for (int k = 0; k < Door_dataGridView.RowCount; k++)
            {
                if (k + 1 < 10)
                { Door_dataGridView.Rows[k].Cells[1].Value = "UD0" + (k + 1).ToString(); }
                else { Door_dataGridView.Rows[k].Cells[1].Value = "UD" + (k + 1).ToString(); }
            }
        }

        private void Door_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //DB유형>기본>플러스버튼시>DoorDefault뜨도록
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 3)
                {
                    if (Door_dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString() == "기본")
                    {
                        DoorDefault doordefault = new DoorDefault();
                        DialogResult result = doordefault.ShowDialog();
                        Door_dataGridView.Rows[nRow].Cells[6].Value = doordefault.문틀내부;
                        Door_dataGridView.Rows[nRow].Cells[7].Value = doordefault.문틀상부;
                        Door_dataGridView.Rows[nRow].Cells[8].Value = doordefault.문틀하부;
                        Door_dataGridView.Rows[nRow].Cells[9].Value = doordefault.문짝종류;
                        Door_dataGridView.Rows[nRow].Cells[10].Value = doordefault.문짝내부;

                        if (doordefault.문짝내부 == "단열재")
                        {
                            DataGridViewButtonCell Insul_ButtonCell = new DataGridViewButtonCell();
                            Door_dataGridView.Rows[e.RowIndex].Cells[11].Value = null;
                            Door_dataGridView.Rows[nRow].Cells[12] = Insul_ButtonCell;
                            Insul_ButtonCell.Value = "+";
                        }
                        else if (doordefault.문짝내부 == "공기")
                        {
                            Door_dataGridView.Rows[e.RowIndex].Cells[11].Value = "-";
                            DataGridViewTextBoxCell Insul_TextCell = new DataGridViewTextBoxCell();
                            Door_dataGridView.Rows[nRow].Cells[12] = Insul_TextCell;
                            Insul_TextCell.Value = null;
                        }
                        else
                        {
                            Door_dataGridView.Rows[e.RowIndex].Cells[11].Value = null;
                            DataGridViewTextBoxCell Insul_TextCell = new DataGridViewTextBoxCell();
                            Door_dataGridView.Rows[nRow].Cells[12] = Insul_TextCell;
                            Insul_TextCell.Value = null;
                        }
                    }
                    else { }
                }
            }

            //플러스 버튼을 선택하면 DB 창이 뜨도록 해야함
            //단열재만 있는 DB는 없어서 따로 만들어야하고 임시로 MaterialDB 만들어야함
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 12)
                {
                    if (Door_dataGridView.Rows[nRow].Cells[10].Value.ToString() == "단열재")
                    {
                        CW_PanelDB material = new CW_PanelDB();
                        DialogResult result = material.ShowDialog();
                        Door_dataGridView.Rows[e.RowIndex].Cells[11].Value = material.Select_CWPanel[1];
                    }
                    else { }

                }
            }
        }
        private void Door_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 2)
                {
                    if (Door_dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString() == "도면")
                    {
                        Door_dataGridView.Rows[e.RowIndex].Cells[11].Value = "-";
                        Door_dataGridView.Rows[e.RowIndex].Cells[13].Value = "-";
                        DataGridViewTextBoxCell Insul_TextCell = new DataGridViewTextBoxCell();
                        Door_dataGridView.Rows[nRow].Cells[12] = Insul_TextCell;
                        Insul_TextCell.Value = null;
                        DataGridViewTextBoxCell ss_TextCell = new DataGridViewTextBoxCell();
                        Door_dataGridView.Rows[nRow].Cells[3] = ss_TextCell;
                        ss_TextCell.Value = null;
                        User();
                        Door_dataGridView.Rows[e.RowIndex].Cells[7].Value = null;
                        Door_dataGridView.Rows[e.RowIndex].Cells[8].Value = null;

                    }
                    else if (Door_dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString() == "기본")
                    {
                        DataGridViewButtonCell default_ButtonCell = new DataGridViewButtonCell();
                        Door_dataGridView.Rows[nRow].Cells[3] = default_ButtonCell;
                        default_ButtonCell.Value = "+";
                        Door_dataGridView.Rows[e.RowIndex].Cells[13].Value = null;
                        Door_dataGridView.Rows[e.RowIndex].Cells[16].Value = null;
                    }
                }
                if (Door_dataGridView.Rows[e.RowIndex].Cells[2].Value != null && Door_dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString() == "기본")
                {
                    if (Door_dataGridView.Rows[e.RowIndex].Cells[13].Value != null && Door_dataGridView.Rows[e.RowIndex].Cells[13].Value.ToString() != "-")
                    {
                        d = Convert.ToDouble(Door_dataGridView.Rows[e.RowIndex].Cells[13].Value);
                        if (열전도율 > 0 && d > 0)
                        {
                            R = d / 1000 / 열전도율;
                        }
                    }
                    if (Door_dataGridView.Rows[e.RowIndex].Cells[14].Value != null && Door_dataGridView.Rows[e.RowIndex].Cells[14].Value.ToString() != "-")
                    {
                        Width = Convert.ToDouble(Door_dataGridView.Rows[e.RowIndex].Cells[14].Value) / 1000;
                    }
                    if (Door_dataGridView.Rows[e.RowIndex].Cells[15].Value != null && Door_dataGridView.Rows[e.RowIndex].Cells[15].Value.ToString() != "-")
                    {
                        Height = Convert.ToDouble(Door_dataGridView.Rows[e.RowIndex].Cells[15].Value) / 1000;
                    }
                    if (Door_dataGridView.Rows[e.RowIndex].Cells[11].Value != null && Door_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString() != "-")
                    {
                        if (Door_dataGridView.Rows[e.RowIndex].Cells[10].Value != null && Door_dataGridView.Rows[e.RowIndex].Cells[10].Value.ToString() == "단열재")
                        {
                            string[][] Value;

                            Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "열전도율", "열전도율", "재료명 ='" + Door_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString() + "'");
                            if (Value.Length > 0)
                            {
                                열전도율 = Convert.ToDouble(Value[0][0]);
                                if (열전도율 > 0 && d > 0)
                                {
                                    R = d / 1000 / 열전도율;
                                }
                            }
                        }
                        else { }
                    }
                    if (Door_dataGridView.Rows[e.RowIndex].Cells[10].Value != null && Door_dataGridView.Rows[e.RowIndex].Cells[10].Value.ToString() == "공기")
                    {
                        if (d > 0)
                        {
                            R = Calc_Air_Layer(d);
                        }
                    }
                    //if (R > 0 && Height > 0 && Width > 0)
                    if (Height > 0 && Width > 0)
                    {
                        Calc_U(e.RowIndex);
                    }
                    else { }
                }
            }
        }

        private double Calc_Air_Layer(double d)
        {
            double R_up = 0, R_down = 0, d_up = 0, d_down = 0;
            double ha, hr, Ramda_air = 0;
            string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "공기층열저항", "두께,대류열저항", "구조체 = '외벽'");
            if (Value.Length > 0)
            {
                double[,] arr_Value = new double[Value.Length, 2];
                for (int k = 0; k < Value.Length; k++)
                {
                    arr_Value[k, 0] = Convert.ToDouble(Value[k][0]);
                    arr_Value[k, 1] = Convert.ToDouble(Value[k][1]);
                }
                for (int k = 0; k < Value.Length; k++)
                {
                    if (arr_Value[k, 0] > d)
                    {
                        d_down = arr_Value[k - 1, 0];
                        R_down = arr_Value[k - 1, 1];
                        d_up = arr_Value[k, 0];
                        R_up = arr_Value[k, 1];
                        break;
                    }
                }
                if (d > arr_Value[Value.Length - 1, 0])
                {
                    d_down = arr_Value[Value.Length - 1, 0];
                    R_down = arr_Value[Value.Length - 1, 1];
                    d_up = arr_Value[Value.Length - 1, 0];
                    R_up = arr_Value[Value.Length - 1, 1];
                }
                if (d_up == d_down)
                { ha = 1 / R_up; }
                else { ha = 1 / ((R_up - R_down) / (d_up - d_down) * (d - d_up) + R_up); }

                hr = 5.1 / (1 / 0.9 + 1 / 0.9 - 1);

                Ramda_air = d / 1000 * (hr + ha);
            }
            return Ramda_air;
        }


        //두께에 대한 벽체 열저항 선형보간
        private double Interpolate(double[] x, double[] y, double xi)
        {
            int i = 0;
            while (i < x.Length && xi > x[i])
            {
                i++;
            }

            if (i == 0 || i == x.Length)
            {
                throw new Exception("xi is out of range");
            }

            double x0 = x[i - 1];
            double x1 = x[i];
            double y0 = y[i - 1];
            double y1 = y[i];

            yi = y0 + ((xi - x0) / (x1 - x0)) * (y1 - y0);

            return yi;

        }


        private void Calc_U(int nRow)
        {
            if (Door_dataGridView.Rows[nRow].Cells[11].Value != null && Door_dataGridView.Rows[nRow].Cells[11].Value.ToString() != "")
            {
                //문짝내부가 단열재인 경우
                if (Door_dataGridView.Rows[nRow].Cells[11].Value.ToString() != "-")
                {
                    U = (1 / (R + 0.17));
                }
                else
                {
                    if (Door_dataGridView.Rows[nRow].Cells[9].Value.ToString() == "목재문")
                    {
                        //목재문일 경우 두께 -10 
                        d2 = d - 10;

                        //두께에 대한 벽체 열저항 선형보간

                        Interpolate(x, y, d2);
                        U = (1 / (0.17 + yi));
                    }
                    else
                    {
                        Interpolate(x, y, d);
                        U = (1 / (0.17 + yi));
                    }
                }
                Door_dataGridView.Rows[nRow].Cells[16].Value = string.Format("{0:F3}", U);
            }


        }



        private void Save_button_Click(object sender, EventArgs e)
        {
            Select_Door[0] = Door_dataGridView.Rows[nRow].Cells[1].Value.ToString();//번호
            Select_Door[1] = Door_dataGridView.Rows[nRow].Cells[2].Value.ToString();//DB유형
            for (int i = 2; i < 12; i++)
            {
                if (Door_dataGridView.Rows[nRow].Cells[i + 2].Value != null)
                { Select_Door[i] = Door_dataGridView.Rows[nRow].Cells[i + 2].Value.ToString(); }
            }
            for (int i = 10; i < 14; i++)
            {
                if (Door_dataGridView.Rows[nRow].Cells[i + 3].Value != null)
                { Select_Door[i] = Door_dataGridView.Rows[nRow].Cells[i + 3].Value.ToString(); }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void info_Click(object sender, EventArgs e)
        {

            string basePath = Program.gPath + "Manual\\2.subcontents\\6.Door\\1.DoorDB";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }
    }
}
