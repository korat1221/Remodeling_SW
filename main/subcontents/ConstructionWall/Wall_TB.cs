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
using main.info;

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
        String WallType, StructureType, TB_Type, LinearPoint, TBName;

        public Wall_TB(String WallType, String StructureType, double dins)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            this.WallType = WallType;
            WallType_textBox.Text = WallType;
            this.StructureType = StructureType;
            this.d_Ins = dins;
            StructureType_textBox.Text = StructureType;
            if (StructureType == "기존외벽") //덧댐인 경우에는 새로 시공되는 덧댐 부위를 초점으로 검토해야 하므로, 기존외벽이 다른 유형이더라도 콘크리트조로 가정하고, 덧댐 시공된 것으로 검토 
            {
                this.StructureType = "콘크리트조";
            }

            TB_Type_comboBox.Items.Clear();
            //구분 콤보박스
            switch (this.StructureType)
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
            }
            if (WallType == "내부덧댐")
            {
                TB_Type_comboBox.Items.Clear();
                TB_Type_comboBox.Items.Add("내단열");
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
            TB_dataGridView.Columns.Add("A7", "수직간격.[mm]");
            TB_dataGridView.Columns.Add("A8", "수평간격.[mm]");
            if (LinearPoint == "점형")
            {
                TB_dataGridView.Columns.Add("A9", "점형열관류율.(단열재 두께 =" + string.Format("{0:F0}", d_Ins) + "mm).[W/K]");
            }
            else
            {
                TB_dataGridView.Columns.Add("A9", "선형열관류율.(단열재 두께 =" + string.Format("{0:F0}", d_Ins) + "mm).[W/mK]");
            }
            TB_dataGridView.Columns[3].Width = 100;
            TB_dataGridView.Columns[6].Width = 100;
            TB_dataGridView.Columns[9].Width = 130;


            string[][] User_TB = Program.DB.getValue(DB.type.ProjDB, "User_1DTB", "번호,DB유형,제품명,제조사,구조유형,열교유형,수직간격,수평간격,열교열관류율", "구조체='외벽' and  구조유형 = '" + StructureType + "' And 열교유형 = '" + TB_Type + "'");
            if (User_TB.Length > 0)
            {
                for (int n = 0; n < User_TB.Length; n++)
                {
                    TB_dataGridView.Rows.Add();
                    int nRow = TB_dataGridView.Rows.Count - 1;
                    for (int k = 0; k < 9; k++)
                    {
                        TB_dataGridView.Rows[nRow].Cells[k + 1].Value = User_TB[n][k];
                    }
                }
            }

            if (LinearPoint == "점형")
            {

                string[][] TB = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽점형열교", "번호,DB유형,제품명,제조사,구조유형,열교유형,수직간격,수평간격,A,B,C", "구조유형 ='" + StructureType + "' And 열교유형 = '" + TB_Type + "'");
                if (TB.Length > 0)
                {
                    for (int n = 0; n < TB.Length; n++)
                    {
                        double row_A = Program.UTIL.ToDoubleOrZero(TB[n][8]);
                        double row_B = Program.UTIL.ToDoubleOrZero(TB[n][9]);
                        double row_C = Program.UTIL.ToDoubleOrZero(TB[n][10]);
                        double row_Kai = (row_A * Math.Pow(d_Ins, 2) + row_B * d_Ins + row_C) / 1000;

                        TB_dataGridView.Rows.Add();
                        int nRow = TB_dataGridView.Rows.Count - 1;
                        for (int k = 0; k < 8; k++)
                        {
                            TB_dataGridView.Rows[nRow].Cells[k + 1].Value = TB[n][k];
                        }
                        TB_dataGridView.Rows[nRow].Cells[9].Value = string.Format("{0:F3}", row_Kai);
                        Count_DB = TB.Length;
                    }
                }
            }
            else
            {
                string[][] TB = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽선형열교", "번호,DB유형,제품명,제조사,구조유형,열교유형,수직간격,수평간격,A,B,C", "구조유형 ='" + StructureType + "' And 열교유형 = '" + TB_Type + "'");
                if (TB.Length > 0)
                {
                    for (int n = 0; n < TB.Length; n++)
                    {
                        double row_A = Program.UTIL.ToDoubleOrZero(TB[n][8]);
                        double row_B = Program.UTIL.ToDoubleOrZero(TB[n][9]);
                        double row_C = Program.UTIL.ToDoubleOrZero(TB[n][10]);
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
            if (LinearPoint == "점형")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽점형열교이미지", "이미지_구조유형", "열교유형 = '" + TB_Type + "'");
                if (Image.Length > 0)
                {
                    pictureBox1.Load(Program.gPath + Image[0][0]);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            else
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽선형열교이미지", "이미지_구조유형", "열교유형 = '" + TB_Type + "'");
                if (Image.Length > 0)
                {
                    pictureBox1.Load(Program.gPath + Image[0][0]);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
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

                if (TB_dataGridView.Rows[SelectRow].Cells[2].Value.ToString()=="도면")
                {
                    if (LinearPoint == "점형")
                    {
                        Kai = Program.UTIL.dataGridView_doubleComa(TB_dataGridView, SelectRow, 9, 3);
                    }
                    else
                    {
                        Psi = Program.UTIL.dataGridView_doubleComa(TB_dataGridView, SelectRow, 9, 3);
                    }
                }
                else
                {
                    if (LinearPoint == "점형")
                    {
                        string[][] TB = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽점형열교", "번호,A,B,C", "번호 ='" + row.Cells[1].Value.ToString() + "'");
                        if (TB.Length > 0)
                        {
                            for (int n = 0; n < TB.Length; n++)
                            {
                                A = Program.UTIL.ToDoubleOrZero(TB[n][1]);
                                B = Program.UTIL.ToDoubleOrZero(TB[n][2]);
                                C = Program.UTIL.ToDoubleOrZero(TB[n][3]);
                                Kai = (A * Math.Pow(d_Ins, 2) + B * d_Ins + C) / 1000;
                                Count_DB = TB.Length;
                            }
                        }
                    }
                    else
                    {
                        string[][] TB = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽선형열교", "번호, A, B, C", "번호 = '" + row.Cells[1].Value.ToString() + "'");
                        if (TB.Length > 0)
                        {
                            for (int n = 0; n < TB.Length; n++)
                            {
                                A = Program.UTIL.ToDoubleOrZero(TB[n][1]);
                                B = Program.UTIL.ToDoubleOrZero(TB[n][2]);
                                C = Program.UTIL.ToDoubleOrZero(TB[n][3]);
                                Psi = (A * Math.Pow(d_Ins, 2) + B * d_Ins + C) / 1000;
                                //Psi = 0.05805; //홍은동 티푸스 열교 가산치 계산 임의로 넣어놓음
                                Count_DB = TB.Length;
                            }
                        }
                    }
                }
               
                dx = Program.UTIL.ToDoubleOrZero(row.Cells[7].Value) / 1000;
                dy = Program.UTIL.ToDoubleOrZero(row.Cells[8].Value) / 1000;
                dx_textBox.Text = string.Format("{0:F1}", dx);
                dy_textBox.Text = string.Format("{0:F1}", dy);
                Calc_PerArea();
            }
        }
        private void Load_Image2()
        {
            if (LinearPoint == "점형")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽점형열교이미지", "이미지_고정유형", "제품명 = '" + TBName + "' And 열교유형 = '" + TB_Type + "'");
                if (Image.Length > 0)
                {
                    pictureBox2.Load(Program.gPath + Image[0][0]);
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            else
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽선형열교이미지", "이미지_고정유형", "제품명 = '" + TBName + "'  And 열교유형 = '" + TB_Type + "'");
                if (Image.Length > 0)
                {
                    pictureBox2.Load(Program.gPath + Image[0][0]);
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                }
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
            }
            else
            {
                if (dx != 0 || dy != 0)
                {

                    PerArea = 1 / Math.Max(dx, dy);
                    PerArea_label1.Text = "적용길이";
                }
            }
            PerArea_textBox.Text = string.Format("{0:F3}", PerArea) + (LinearPoint == "점형" ? " EA/m" : " m/m") + Program.UTIL.Subscript(2, true);
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
            dU_textBox.Text = string.Format("{0:F3}", dU) + " W/m" + Program.UTIL.Subscript(2, true) + "·K";
        }
        private void Save_button_Click(object sender, EventArgs e)
        {
            for (int a = 0; a < TB_dataGridView.Rows.Count; a++)
            {
                if (TB_dataGridView.Rows[a].Cells[2].Value != null && TB_dataGridView.Rows[a].Cells[2].Value.ToString() == "도면")
                {
                    for (int aa = 7; aa < 10; aa++)
                    {
                        if (TB_dataGridView.Rows[a].Cells[aa].Value != null && TB_dataGridView.Rows[a].Cells[aa].Value.ToString() != "")
                        {
                            double parsedValue;
                            if (double.TryParse(TB_dataGridView.Rows[a].Cells[aa].Value.ToString(), out parsedValue))
                            {
                            }
                            else
                            {
                                TB_dataGridView.Rows[a].Cells[aa].Value = 0;
                                MessageBox.Show("숫자만 입력하세요.");
                                goto 그만;
                            }
                        }

                    }
                    Program.DB.setValue(DB.type.ProjDB, "User_1DTB", "번호,DB유형,제품명,제조사,구조체,구조유형,열교유형,수직간격,수평간격,열교열관류율",
                        "'" + TB_dataGridView.Rows[a].Cells[1].Value.ToString() + "','" + "도면" + "','" + TB_dataGridView.Rows[a].Cells[3].Value.ToString() + "','" + TB_dataGridView.Rows[a].Cells[4].Value.ToString() + "','외벽','" +
                        TB_dataGridView.Rows[a].Cells[5].Value.ToString() + "','" + TB_dataGridView.Rows[a].Cells[6].Value.ToString() + "','" +
                        Program.UTIL.dataGridView_doubleComa(TB_dataGridView, a, 7, 0) + "','" + Program.UTIL.dataGridView_doubleComa(TB_dataGridView, a, 8, 0) + "','" +
                        Program.UTIL.dataGridView_doubleComa(TB_dataGridView, a, 9, 3) + "'", "번호");
                }
            }
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
        그만: int x = 1;

        }

        private void dx_textBox_TextChanged(object sender, EventArgs e)
        {
            dx = Program.UTIL.textBox_doubleComa(dx_textBox, false, 1);
            Calc_PerArea();
        }

        private void dy_textBox_TextChanged(object sender, EventArgs e)
        {
            dy = Program.UTIL.textBox_doubleComa(dy_textBox, false, 1);
            Calc_PerArea();
        }

        private void info_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\2.subcontents\\2.Wall";

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

        private void Add_button_Click(object sender, EventArgs e)
        {
            int nRow = TB_dataGridView.Rows.Add();
            string UserNum = Program.UTIL.CreateNum("User_1DTB", "번호", "UTB_0");
            TB_dataGridView.Rows[nRow].Cells[1].Value = UserNum;
            TB_dataGridView.Rows[nRow].Cells[2].Value = "도면";
            TB_dataGridView.Rows[nRow].Cells[5].Value = this.StructureType;
            TB_dataGridView.Rows[nRow].Cells[6].Value = this.TB_Type;

            DataGridViewRow MoveRow = TB_dataGridView.Rows[nRow];
            TB_dataGridView.Rows.RemoveAt(nRow);
            TB_dataGridView.Rows.Insert(0, MoveRow);
            TB_dataGridView.CurrentCell =TB_dataGridView[TB_dataGridView.CurrentCell.ColumnIndex, 0];
        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            int k = TB_dataGridView.CurrentCell.RowIndex;
            if (k > -1)
            {
                if (TB_dataGridView.Rows[k].Cells[2].Value.ToString() == "도면")
                {
                    if ((MessageBox.Show(TB_dataGridView.Rows[k].Cells[3].Value.ToString() + "을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        String Delete_Num = TB_dataGridView.Rows[k].Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "User_1DTB", "번호 ='" + Delete_Num + "'");
                        load_table_DB();
                    }
                }
                else
                {
                    MessageBox.Show("기본 DB는 삭제할 수 없습니다.");
                }
            }

        }

    }
}
