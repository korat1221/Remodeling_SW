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

namespace main.subcontents.ConstructionWall
{
    public partial class Wall_TB : Form
    {
        double Count_DB;
        double d_Ins;
        double Kai, Psi, PerArea, dx, dy, dU, A, B, C;
        int SelectRow;
        public String[] Select_TB = new String[13];
        int Num;
        String WallType, StructureType, TB_Type, LinearPoint;

        public Wall_TB(String WallType, String StructureType, double dins)
        {
            InitializeComponent();
            this.WallType = WallType;
            WallType_textBox.Text = WallType;
            this.StructureType = StructureType;
            this.d_Ins = dins;
            StructureType_textBox.Text = StructureType;
            TB_Type_comboBox.Items.Clear();
            //구분 콤보박스
            switch (StructureType)
            {
                case "경량철골조":
                    TB_Type_comboBox.Items.Add("금속스터드");
                    TB_Type_comboBox.Items.Add("단열패널");
                    break;
                case "목구조":
                    TB_Type_comboBox.Items.Add("목재스터드");
                    break;
                case "콘크리트조":
                    TB_Type_comboBox.Items.Add("직접고정");
                    TB_Type_comboBox.Items.Add("트러스(점형)");
                    TB_Type_comboBox.Items.Add("트러스(선형)");
                    TB_Type_comboBox.Items.Add("내단열");
                    break;
                case "기존외벽":
                    TB_Type_comboBox.Items.Add("직접고정");
                    TB_Type_comboBox.Items.Add("트러스(점형)");
                    TB_Type_comboBox.Items.Add("트러스(선형)");
                    TB_Type_comboBox.Items.Add("내단열");
                    break;                    
            }
            TB_Type_comboBox.SelectedIndex = 0;

        }

        private void TB_Type_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TB_Type = TB_Type_comboBox.SelectedItem.ToString();
            Check_LinearPoint(TB_Type);
            Load_Image2();
        }
        private void Check_LinearPoint(String TB_Type)
        {
            if (TB_Type == "직접고정" || TB_Type == "트러스(점형)")
            {
                LinearPoint = "점형";
            }
            else
            {
                LinearPoint = "선형";
            }
            load_table_DB();
            Load_Image1();
        }

        void load_table_DB()
        {
            DataTable table_TB = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            TB_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            TB_dataGridView.Columns.Add(checkBoxColumn);
            table_TB.Columns.Add("번호", typeof(string));
            table_TB.Columns.Add("DB유형", typeof(string));
            table_TB.Columns.Add("제품명", typeof(string));
            table_TB.Columns.Add("제조사", typeof(string));
            table_TB.Columns.Add("구조유형", typeof(string));
            table_TB.Columns.Add("열교유형", typeof(string));
            table_TB.Columns.Add("수직간격" + Environment.NewLine + "[mm]", typeof(string));
            table_TB.Columns.Add("수평간격" + Environment.NewLine + "[mm]", typeof(string));
            table_TB.Columns.Add("A", typeof(string));
            table_TB.Columns.Add("B", typeof(string));
            table_TB.Columns.Add("C", typeof(string));

            if (LinearPoint == "점형")
            {
                table_TB.Columns.Add("점형\r\n열관류율" + Environment.NewLine + "d =" + string.Format("{0:F0}", d_Ins) + "mm", typeof(string));
                string[][] TB = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽점형열교", "번호,DB유형,제품명,제조사,구조유형,열교유형,수직간격,수평간격,A,B,C", "구조유형 ='" + StructureType + "' And 열교유형 = '" + TB_Type + "'");
                for (int n = 0; n < TB.Length; n++)
                {
                    A = Convert.ToDouble(TB[n][8]);
                    B = Convert.ToDouble(TB[n][9]);
                    C = Convert.ToDouble(TB[n][10]);
                    Kai = (A * Math.Pow(d_Ins, 2) + B * d_Ins + C) / 1000;
                    table_TB.Rows.Add(TB[n][0], TB[n][1], TB[n][2], TB[n][3], TB[n][4], TB[n][5], TB[n][6], TB[n][7], TB[n][8], TB[n][9], TB[n][10], string.Format("{0:F3}", Kai));
                    Count_DB = TB.Length;
                }
            }
            else
            {
                table_TB.Columns.Add("선형\r\n열관류율" + Environment.NewLine + "d =" + string.Format("{0:F0}", d_Ins) + "mm", typeof(string));
                string[][] TB = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽선형열교", "번호,DB유형,제품명,제조사,구조유형,열교유형,수직간격,수평간격,A,B,C", "구조유형 ='" + StructureType + "' And 열교유형 = '" + TB_Type + "'");
                for (int n = 0; n < TB.Length; n++)
                {
                    A = Convert.ToDouble(TB[n][8]);
                    B = Convert.ToDouble(TB[n][9]);
                    C = Convert.ToDouble(TB[n][10]);
                    Psi = (A * Math.Pow(d_Ins, 2) + B * d_Ins + C) / 1000;
                    table_TB.Rows.Add(TB[n][0], TB[n][1], TB[n][2], TB[n][3], TB[n][4], TB[n][5], TB[n][6], TB[n][7], TB[n][8], TB[n][9], TB[n][10], string.Format("{0:F3}", Psi));
                    Count_DB = TB.Length;
                }
            }

            TB_dataGridView.DataSource = table_TB;
        }
        private void Load_Image1()
        {
            if (LinearPoint == "점형")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽점형열교이미지", "이미지_구조유형", "구조유형 = '" + StructureType + "'");
                pictureBox1.Load(Program.gPath + Image[0][0]);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽선형열교이미지", "이미지_구조유형", "구조유형 = '" + StructureType + "'");
                pictureBox1.Load(Program.gPath + Image[0][0]);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            }
        }



        //데이터그리드뷰 체크박스 선택 시
        private void Spacer_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                TB_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = TB_dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Count_DB; k++)
                {
                    if (k != row.Index)
                    {
                        TB_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = TB_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = TB_dataGridView.Rows[e.RowIndex];
                    }
                }
                TBName_textBox.Text = row.Cells[3].Value.ToString(); //제품명
            }
            Calc_dU();

        }
        private void Load_Image2()
        {
            try
            {
                if (LinearPoint == "점형")
                {
                    string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽점형열교이미지", "이미지_고정유형", "구조유형 = '" + StructureType + "' And 열교유형 = '" + TB_Type + "'");
                    pictureBox2.Load(Program.gPath + Image[0][0]);
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽선형열교이미지", "이미지_고정유형", "구조유형 = '" + StructureType + "' And 열교유형 = '" + TB_Type + "'");
                    pictureBox2.Load(Program.gPath + Image[0][0]);
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

                }
            }
            catch { }
        }

        private void dx_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back) || e.KeyChar == '-' || e.KeyChar == '.'))
            {
                e.Handled = true;
                dx = Convert.ToDouble(dx_textBox.Text);
                Calc_PerArea();
            }
        }

        private void dy_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back) || e.KeyChar == '-' || e.KeyChar == '.'))
            {
                e.Handled = true;
                dy = Convert.ToDouble(dy_textBox.Text);
                Calc_PerArea();
            }

        }
        private void Calc_PerArea()
        {
            dx_textBox.Text = string.Format("{0:F1}", dx);
            dy_textBox.Text = string.Format("{0:F1}", dy);
            if (LinearPoint == "점형")
            {
                if (dx != 0 && dy != 0)
                {
                    if (TB_Type == "직접고정")
                    {
                        PerArea = 2 * dx * dy;
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
                PerArea = 1 / (dx + dy);
                PerArea_label1.Text = "적용길이";
                PerArea_label2.Text = "m/m²";
            }
            PerArea_textBox.Text = string.Format("{0:F3}", PerArea);
        }
        private void Calc_dU()
        {
            DataGridViewRow row = TB_dataGridView.Rows[SelectRow];
            dx = Convert.ToDouble(row.Cells[7].Value) / 1000;
            dy = Convert.ToDouble(row.Cells[8].Value) / 1000;
            Calc_PerArea();

            if (LinearPoint == "점형")
            {
                Kai = Convert.ToDouble(row.Cells[12].Value);
                dU = Kai * PerArea;
            }
            else
            {
                Psi = Convert.ToDouble(row.Cells[12].Value);
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
            Select_TB[8] = WallType; //리모델링유형 check용
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

    }
}
