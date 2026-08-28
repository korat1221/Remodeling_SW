namespace main.contents
{
    public partial class TB_property : Form
    {
        string sid = ""; string IDNum;
        string SelectTBType, checkTBType;
        string TBType;
        string SelectTBNum, SelectTBName;
        string SelectTBIns1, SelectTBIns2; 


        public TB_property()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '열교정보'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

        }
        private void onVisibleChanged(object sender, EventArgs e)
        {
            if (main.MainContents.selID != sid)
            {
                this.panel1.Show();

                // selID 형식: "selectedg::RTB1::01" → ThermalBridge_3D.번호 는 "RTB1_01"
                string[] parts = main.MainContents.selID.Split("::");
                if (parts.Length >= 3)
                {
                    IDNum = parts[1] + "_" + parts[2];
                }
                else
                {
                    IDNum = main.MainContents.selID;
                }
                label4.Text = IDNum;

                string[][] TB_Type = Program.DB.getValue(DB.type.ProjDB, "ThermalBridge_3D", "번호, 열교항목, 열교길이, 선택열교", "번호 = '" + IDNum + "'");
                if (TB_Type.Length > 0)
                {
                    TBType = TB_Type[0][1];
                    Type_Textbox.Text = TB_Type[0][1];
                    Length_Textbox.Text = Program.UTIL.ToDoubleOrZero(TB_Type[0][2]).ToString("0.0") + " m";
                    SelectTBNum = TB_Type[0][3];
                    string[][] tb2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "접합부열교", "번호,명칭,값,구조체1_단열유형, 구조체2_단열유형", "번호 ='" + SelectTBNum + "'");

                    if (tb2.Length == 0) { tb2 = Program.DB.getValue(DB.type.ProjDB, "User_TB", "번호,명칭,값,구조체1_단열유형, 구조체2_단열유형", "번호 ='" + SelectTBNum + "'"); }
                    if (tb2.Length > 0)
                    {
                        SelectTBName = tb2[0][1];
                        TB_Textbox.Text = SelectTBNum + "." + " " + tb2[0][1];
                        Psi_textBox.Text = Program.UTIL.ToDoubleOrZero(tb2[0][2]).ToString("0.000");
                        SelectTBIns1 = tb2[0][3];
                        SelectTBIns2 = tb2[0][4];
                        Load_Image2();
                    }
                    else
                    {
                        TB_Textbox.Text = "열교를 선택해주세요.";
                    }
                }

            }
        }

        private void Load_Image2()
        {
            string[][] tb2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "접합부열교", "번호", "번호 ='" + SelectTBNum + "'");

            if (tb2.Length > 0)
            {
                pictureBox3.Visible = false;
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "접합부열교_이미지", "소분류_이미지1", "대분류 ='" + TBType + "' and 소분류 = '" + SelectTBName + "'");
                if (Image.Length > 0)
                {
                    pictureBox1.Visible = true;
                    pictureBox1.Load(Program.gPath + Image[0][0]);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "접합부열교_이미지", "소분류_이미지2", "대분류 ='" + TBType + "' and 소분류 = '" + SelectTBName + "'");
                if (Image.Length > 0)
                {
                    pictureBox2.Visible = true;
                    pictureBox2.Load(Program.gPath + Image[0][0]);
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                }

            }
            else
            {
                pictureBox2.Visible = false;
                //pictureBox3.Visible = true;
                //pictureBox3.Load(Program.gPath + "images/TB/User/" + SelectTBNum + ".jpg");
                //pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;

                string[][] value = Program.DB.getValue(DB.type.BaseDB_HCneed, "접합부열교", "명칭", "열교유형 ='" + TBType + "' and 구조체1_단열유형 = '" + SelectTBIns1 +  "' and 구조체2_단열유형 = '" + SelectTBIns2 + "'");
                if (value.Length > 0)
                {
                    string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "접합부열교_이미지", "소분류_이미지1", "대분류 ='" + TBType + "' and 소분류 = '" + value[0][0] + "'");
                    if (Image.Length > 0)
                    {
                        pictureBox1.Visible = true;
                        pictureBox1.Load(Program.gPath + Image[0][0]);
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }

            }

        }




        private void Checked_Value()
        {
            //for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //{
            //    if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) == true)
            //    {
            //        if (checkTBType == null)
            //        {
            //            checkTBType = dataGridView1.Rows[i].Cells[2].Value.ToString();
            //            checkSame = true;
            //        }
            //        else if (dataGridView1.Rows[i].Cells[2].Value.ToString() == checkTBType)
            //        {
            //            checkSame = true;
            //        }
            //        else
            //        {
            //            MessageBox.Show("같은 유형만 선택하세요.");
            //            checkSame = false;
            //        }
            //    }
            //}

        }
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == 0)
            {
                int cellX = dataGridView1.Location.X + e.CellBounds.X;
                int cellY = dataGridView1.Location.Y + e.CellBounds.Y;

                if (e.ColumnIndex == 0)
                {
                    if (!Check_checkBox.Visible)
                    {
                        Check_checkBox.Location = new Point(cellX + 10, cellY + 5);
                        Check_checkBox.Size = new Size(e.CellBounds.Width, e.CellBounds.Height);
                        Check_checkBox.Show();
                    }
                }
                if (e.ColumnIndex == 2)
                {
                    if (!TB_comboBox.Visible)
                    {
                        TB_comboBox.Location = new Point(cellX, cellY);
                        TB_comboBox.Size = new Size(e.CellBounds.Width, e.CellBounds.Height);
                        TB_comboBox.Show();
                    }
                }
                else if (e.ColumnIndex == 3)
                {
                    if (!TB_button.Visible)
                    {
                        TB_button.Location = new Point(cellX, cellY - 1);
                        TB_button.Size = new Size(e.CellBounds.Width, e.CellBounds.Height);
                        TB_button.Show();
                    }
                }
            }
        }

        private void fillFilterCombos()
        {
            int i = -1;
            string[][] rec = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ThermalBridge_3D", "열교항목");

            TB_comboBox.Items.Clear();

            TB_comboBox.Items.Add("ALL");
            while (++i < rec.Length)
            {
                TB_comboBox.Items.Add(rec[i][0]);
            }
        }
        private bool dataGridView1_RowHandle(DataGridViewCell cell, int column, int row)
        {
            if (row % 2 == 1)
            {
                if (column == 1 || column == 2 || column == 3 || column == 4 || column == 5 || column == 6)
                {
                    cell.Style.BackColor = SystemColors.InactiveBorder;
                    return true;
                }
                else return false;
            }
            else
            {
                if (column == 1 || column == 2 || column == 3 || column == 4 || column == 5 || column == 6)
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                    return true;
                }
                else return false;
            }
        }


    }
}
