using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents.ConstructionFloor
{
    public partial class Floor_TB : Form
    {
        double Count_DB;
        double d_Ins;
        double Kai, Psi, PerArea, dx, dy, dU, A, B, C;
        int SelectRow;
        public String[] Select_TB = new String[13];
        int Num;
        String FloorType, StructureType, TB_Type, LinearPoint, TBName;


        public Floor_TB(String FloorType, String StructureType, double dins)
        {
            InitializeComponent();
            this.FloorType = FloorType;
            WallType_textBox.Text = FloorType;
            this.StructureType = StructureType;
            if (StructureType == "기존바닥")
            {
                this.StructureType = "콘크리트조";
            }
            this.d_Ins = dins;
            StructureType_textBox.Text = StructureType;
            TB_Type_comboBox.Items.Clear();


            //구분 콤보박스
            if (FloorType == "신규" || FloorType == "철거 후 신규" || FloorType == "기존바닥")
            {
                TB_Type_comboBox.Items.Clear();
                TB_Type_comboBox.Items.Add("외단열");
                TB_Type_comboBox.Items.Add("내단열");
            }
            else if (FloorType == "외부덧댐")
            {
                TB_Type_comboBox.Items.Clear();
                TB_Type_comboBox.Items.Add("외부덧댐형");
            }

            TB_Type_comboBox.SelectedIndex = 0;

        }

        private void TB_Type_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TB_Type = TB_Type_comboBox.SelectedItem.ToString();
            Check_LinearPoint(TB_Type);
        }
        private void Check_LinearPoint(String TB_Type)
        {
            LinearPoint = "선형";
            load_table_DB();
            Load_Image1();
        }

        void load_table_DB()
        {
            new StackedHeaderDecorator(TB_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            TB_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            TB_dataGridView.Columns.Add(checkBoxColumn);

            TB_dataGridView.Columns.Add("A1", "번호");
            TB_dataGridView.Columns.Add("A2", "DB유형");
            TB_dataGridView.Columns.Add("A3", "제품명");
            TB_dataGridView.Columns.Add("A4", "제조사");
            TB_dataGridView.Columns.Add("A5", "구조유형");
            TB_dataGridView.Columns.Add("A6", "열교유형");
            TB_dataGridView.Columns.Add("A7", "간격.수직.[mm]");
            TB_dataGridView.Columns.Add("A8", "간격.수평.[mm]");
            TB_dataGridView.Columns.Add("A9", "선형열관류율.(단열재 두께 =" + string.Format("{0:F0}", d_Ins) + "mm).[W/mK]");
            TB_dataGridView.Columns[3].Width = 130;
            TB_dataGridView.Columns[9].Width = 130;

            string[][] TB = Program.DB.getValue(DB.type.BaseDB_HCneed, "바닥선형열교", "번호,DB유형,제품명,제조사,구조유형,열교유형,수직간격,수평간격,A,B,C", "구조유형 ='" + StructureType + "' And 열교유형 = '" + TB_Type + "'");
            if (TB.Length > 0)
            {
                for (int n = 0; n < TB.Length; n++)
                {
                    double row_A = Convert.ToDouble(TB[n][8]);
                    double row_B = Convert.ToDouble(TB[n][9]);
                    double row_C = Convert.ToDouble(TB[n][10]);
                    double row_Psi = (row_A * Math.Pow(d_Ins, 2) + row_B * d_Ins + row_C) / 1000;

                    TB_dataGridView.Rows.Add();
                    int nRow = TB_dataGridView.Rows.Count - 1;
                    for (int k = 0; k < 8; k++)
                    {
                        TB_dataGridView.Rows[nRow].Cells[k + 1].Value = TB[n][k];
                    }
                    TB_dataGridView.Rows[nRow].Cells[9].Value = string.Format("{0:F3}", row_Psi);

                    Count_DB = TB.Length;
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

        private void Load_Image1()
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "바닥선형열교이미지", "이미지_구조유형", "열교유형 = '" + TB_Type + "'");
            if (Image.Length > 0)
            {
                pictureBox1.Load(Program.gPath + Image[0][0]);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }



        //데이터그리드뷰 체크박스 선택 시
        private void TB_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                for (int i = 0; i < TB_dataGridView.Rows.Count; i++)
                {
                    if (i != e.RowIndex)
                    {
                        TB_dataGridView.Rows[i].Cells[0].Value = false;
                    }
                }
                TB_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = TB_dataGridView.Rows[SelectRow];
                TBName = row.Cells[3].Value.ToString(); //제품명
                TBName_textBox.Text = row.Cells[3].Value.ToString(); //제품명
                Load_Image2();


                string[][] TB = Program.DB.getValue(DB.type.BaseDB_HCneed, "바닥선형열교", "번호, A, B, C", "번호 = '" + row.Cells[1].Value.ToString() + "'");
                if (TB.Length > 0)
                {
                    for (int n = 0; n < TB.Length; n++)
                    {
                        A = Convert.ToDouble(TB[n][1]);
                        B = Convert.ToDouble(TB[n][2]);
                        C = Convert.ToDouble(TB[n][3]);
                        Psi = (A * Math.Pow(d_Ins, 2) + B * d_Ins + C) / 1000;
                        Count_DB = TB.Length;
                    }
                }
                dx = Convert.ToDouble(row.Cells[7].Value) / 1000;
                dy = Convert.ToDouble(row.Cells[8].Value) / 1000;
                dx_textBox.Text = string.Format("{0:F1}", dx);
                dy_textBox.Text = string.Format("{0:F1}", dy);
                Calc_PerArea();
            }

        }
        private void Load_Image2()
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "바닥선형열교이미지", "이미지_고정유형", "제품명 = '" + TBName + "'  And 열교유형 = '" + TB_Type + "'");
            if (Image.Length > 0)
            {
                pictureBox2.Load(Program.gPath + Image[0][0]);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
        private void Calc_PerArea()
        {
            if (LinearPoint == "점형")
            {
                if (dx != 0 || dy != 0)
                {
                    if (TB_Type == "직접고정")
                    {
                        PerArea = 2 / dx / dy;
                    }
                    else
                    {
                        PerArea = 1 / dx / dy;
                    }
                }
                else
                {
                    PerArea = 0;
                }
                PerArea_label1.Text = "적용개수";
                PerArea_label2.Text = "EA/m²";
            }
            else
            {
                if (dx != 0 || dy != 0)
                {
                    PerArea = 1 / Math.Max(dx, dy);
                    PerArea_label1.Text = "적용길이";
                    PerArea_label2.Text = "m/m²";
                }
            }
            PerArea_textBox.Text = string.Format("{0:F3}", PerArea);
            Calc_dU();
        }
        private void Calc_dU()
        {
            if (LinearPoint == "점형")
            {
                dU = Kai * PerArea;
            }
            else
            {
                dU = Psi * PerArea;
            }
            dU_textBox.Text = string.Format("{0:F3}", dU);
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = TB_dataGridView.Rows[SelectRow];

            Select_TB[0] = row.Cells[1].Value.ToString(); //번호
            Select_TB[1] = row.Cells[3].Value.ToString(); //제품명
            Select_TB[2] = row.Cells[5].Value.ToString(); //구조유형
            Select_TB[3] = row.Cells[6].Value.ToString(); //열교유형
            Select_TB[4] = A.ToString(); //계수A
            Select_TB[5] = B.ToString(); //계수B
            Select_TB[6] = C.ToString(); //계수C
            Select_TB[7] = PerArea.ToString(); //단위면적당길이 or 개수
            Select_TB[8] = FloorType; //리모델링유형 check용
            Select_TB[9] = d_Ins.ToString(); //단열재두께 check용
            Select_TB[10] = dU.ToString(); //1D열교가산치
            Select_TB[11] = LinearPoint; //선형인지 점형인지
            if (LinearPoint == "점형")
            {
                Select_TB[12] = Kai.ToString(); //Kai
            }
            else
            {
                Select_TB[12] = Psi.ToString(); //Psi
            }


            this.DialogResult = DialogResult.OK;
            this.Close();

        }
        private void dx_textBox_TextChanged(object sender, EventArgs e)
        {
            controls.ThousandsSeparator textbox = new controls.ThousandsSeparator(dx_textBox, false, 1);
            dx = textbox.text;
            Calc_PerArea();
        }

        private void dy_textBox_TextChanged(object sender, EventArgs e)
        {
            controls.ThousandsSeparator textbox = new controls.ThousandsSeparator(dy_textBox, false, 1);
            dy = textbox.text;
            Calc_PerArea();
        }
    }
}
