using main.contentslist;
using main.subcontents.ConstructionFloor;
using main.subcontents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace main.contents.Construction
{
    public partial class ConstructionFloor : Form
    {
        String FloorNum, FloorName, Type, check_Type, OldFloor, UMethod, Base, DiIndi, StructureType, check_StructureType, TBType, TBName, ISO_KS, LinearPoint;
        double A, B, C, PsiKai, PerArea;
        double Rse, Rsi, dtot, Rtot, dins, check_dins;
        double OldFloor_R;
        double Uvalue, dU, Ueff;
        String[][] Old;
        int SelectRow;
        String[] Material = new String[10];
        double[] Material_d = new double[10];
        double[] Material_λ = new double[10];
        double[] Material_R = new double[10];

        public ConstructionFloor()
        {
            InitializeComponent();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '최하층바닥'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            //표면열전달저항기준 콤보박스 
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, ISO_KS_comboBox, "바닥", "실내외표면열전달저항", "1");
            //단열수준콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, Uvalue_comboBox, "바닥", "단열수준", "3");
            Load_table();
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);

        }
        private void Name_textBox_TextChanged(object sender, EventArgs e)
        {
            if (Name_textBox.Text != null)
            {
                FloorName = Name_textBox.Text.ToString();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Type = "기존바닥";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            //Load_FloorType_image(Type,Base);
            Dilndi();
            BaseType();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Type = "신규";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            //Load_FloorType_image(Type, Base);
            Dilndi();
            BaseType();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Type = "철거 후 신규";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            //Load_FloorType_image(Type, Base);
            Dilndi();
            BaseType();
        }


        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Type = "외부덧댐";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            //Load_FloorType_image(Type, Base);
            Dilndi();
            BaseType();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            Type = "내부덧댐";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            //Load_FloorType_image(Type,Base);
            Dilndi();
            BaseType();
        }

        private void BaseType()
        {
            //지면위 및 단열지하실은 외부덧댐 없음(엑셀상이)
            if (Type == "외부덧댐")
            {
                Base_comboBox.Items.Clear();
                Base_comboBox.Items.Add("비단열지하실");
                Base_comboBox.Items.Add("바닥(외기)");
            }
            else
            {
                //기초설치콤보박스
                Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, Base_comboBox, "바닥", "기초설치", "1");
            }
        }


        private void Changed_Type(String Type)
        {
            Uvalue_comboBox.Items.Clear();

            switch (Type)
            {
                case "기존바닥":
                    Uvalue_comboBox.Items.Add("계산");
                    Uvalue_comboBox.Items.Add("법규");
                    Uvalue_comboBox.Items.Add("진단");
                    OldFloor_label.Visible = false;
                    OldFloor_comboBox.Visible = false;
                    StructureType_textBox.Text = "기존바닥";
                    break;

                case "신규":
                    Uvalue_comboBox.Items.Add("계산");
                    Uvalue_comboBox.Items.Add("법규");
                    OldFloor_label.Visible = false;
                    OldFloor_comboBox.Visible = false;
                    StructureType_textBox.Text = "콘크리트조";
                    break;

                case "철거 후 신규":
                    Uvalue_comboBox.Items.Add("계산");
                    Uvalue_comboBox.Items.Add("법규");
                    OldFloor_label.Visible = true;
                    OldFloor_comboBox.Visible = true;
                    Load_OldFloor(Type);
                    StructureType_textBox.Text = "콘크리트조";
                    break;

                case "외부덧댐":
                    Uvalue_comboBox.Items.Add("계산");
                    OldFloor_label.Visible = true;
                    OldFloor_comboBox.Visible = true;
                    Load_OldFloor(Type);
                    StructureType_textBox.Text = "기존바닥";
                    break;

                case "내부덧댐":
                    Uvalue_comboBox.Items.Add("계산");
                    OldFloor_label.Visible = true;
                    OldFloor_comboBox.Visible = true;
                    Load_OldFloor(Type);
                    StructureType_textBox.Text = "기존바닥";
                    break;

            }

            Uvalue_comboBox.SelectedIndex = 0;
        }

        //기존 바닥 리스트 불러오기 
        private void Load_OldFloor(String Type)
        {
            string def_value;
            String[][] Table;

            if (Type == "철거 후 신규" || Type == "외부덧댐" || Type == "내부덧댐")
            {
                def_value = "Type = '기존바닥'";
                Table = Program.DB.getValue(DB.type.ProjDB, "ConstructionFloor", "명칭", def_value);
            }
            else
            {
                def_value = "Type = ''";
                Table = Program.DB.getValue(DB.type.ProjDB, "ConstructionFloor", "명칭", def_value);
            }

            int i = -1;
            DataTable sources = new DataTable();
            sources.Columns.Add("Text");
            sources.Columns.Add("Value");
            sources.Columns.Add("ID");

            while (++i < Table.Length)
            {
                DataRow dr = sources.NewRow();
                dr["Text"] = Table[i][0];
                sources.Rows.Add(dr);
            }

            OldFloor_comboBox.DataSource = sources.DefaultView;
            OldFloor_comboBox.DisplayMember = "Text";
            for (i = 0; i < OldFloor_comboBox.Items.Count; i++)
            {
                var arr = ((DataRowView)OldFloor_comboBox.Items[i]).Row.ItemArray;
                if (arr.Length > 1 && arr[1].ToString() == def_value)
                {
                    OldFloor_comboBox.SelectedIndex = i;
                    break;
                }
            }

        }

        private void OldFloor_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView? item = OldFloor_comboBox.SelectedItem as DataRowView;
            if (item != null)
            {
                OldFloor = item.Row.ItemArray[0].ToString();
                string[][] OldWall_U = Program.DB.getValue(DB.type.ProjDB, "ConstructionFloor", "열관류율", "명칭 = '" + OldFloor + "'");
                try
                {
                    OldFloor_R = 1 / Convert.ToDouble(OldWall_U[0][0]);
                }
                catch { }
                Add_OldFloor();
            }
            else { }
        }


        private void Act_UcWMethod()
        {
            if (UMethod == "계산")
            {
                tabControl1.SelectedTab = tabControl1.TabPages["Ucalc_tabPage"];
                U_textBox.Enabled = false;
                U_textBox.BorderStyle = BorderStyle.None;
                Ucalc_dataGridView.Show();
                Ucalc_tabPage.Enabled = true;
                pictureBox3.Visible = true;
            }
            else if (UMethod == "법규")
            {
                Rule_U();
                U_textBox.Enabled = false;
                U_textBox.BorderStyle = BorderStyle.None;
                Ucalc_dataGridView.Hide();
                Ucalc_tabPage.Enabled = false;
                pictureBox3.Visible = false;
            }
            else if (UMethod == "진단")
            {
                U_textBox.Enabled = true;
                U_textBox.BorderStyle = BorderStyle.FixedSingle;
                Ucalc_dataGridView.Hide();
                Ucalc_tabPage.Enabled = false;
                pictureBox3.Visible = false;
            }
        }


        private void U_textBox_TextChanged(object sender, EventArgs e)
        {
            if (UMethod == "진단" && U_textBox.Text != string.Empty)
            {
                Uvalue = Convert.ToDouble(U_textBox.Text);
                dins = (1 / Uvalue) * 0.04 * 1000;
                Calc_dU();
                Calc_Ueff();
            }

        }

        private void Base_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Base_comboBox.SelectedItem != null)
            {
                Base = Base_comboBox.SelectedItem.ToString();

                Load_FloorType_image(Type, Base);

                Uvalue_comboBox.SelectedIndex = 0;
            }
        }


        private void DiIndi_textBox_TextChanged(object sender, EventArgs e)
        {
            if (DiIndi_textBox.Text != null)
            {
                Dilndi();
            }
        }
        private void DiIndi2_textBox_TextChanged(object sender, EventArgs e)
        {

            if (DiIndi2_textBox.Text != null)
            {
                Dilndi();
                Calc_RseRsi();
                Calc_Ueff();
            }
        }

        // 지면위 -> 지면 / 단열지하실 -> 간접외기 / 비단열지하실 -> 간접외기 / 바닥(외기) -> 직접외기 
        public void Dilndi()
        {
            if (UMethod == "법규" || UMethod == "진단")
            {
                if (Base == "지면위" || Base == "단열지하실")
                {
                    DiIndi_textBox.Text = "지면";
                    DiIndi2_textBox.Text = "";
                }
                else if (Base == "바닥(외기)")
                {
                    DiIndi_textBox.Text = "직접외기";
                    DiIndi2_textBox.Text = "";
                }
                else
                {
                    DiIndi_textBox.Text = "간접외기";
                    DiIndi2_textBox.Text = "";
                }
                DiIndi = DiIndi_textBox.Text;
            }
            else if (UMethod == "계산")
            {
                if (Base == "지면위" || Base == "단열지하실")
                {
                    DiIndi_textBox.Text = "";
                    DiIndi2_textBox.Text = "지면";
                }
                else if (Base == "바닥(외기)")
                {
                    DiIndi_textBox.Text = "";
                    DiIndi2_textBox.Text = "직접외기";
                }
                else
                {
                    DiIndi_textBox.Text = "";
                    DiIndi2_textBox.Text = "간접외기";
                }
                DiIndi = DiIndi2_textBox.Text;
            }
            else
            {
                DiIndi_textBox.Text = "";
                DiIndi2_textBox.Text = "";
            }
        }


        private void Load_FloorType_image(String Type, String Base)
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "바닥유형이미지", "이미지", "바닥유형 = '" + Type + "' AND 기초설치 = '" + Base + "'");
            FloorType_pictureBox.Visible = true;
            FloorType_pictureBox.Load(Program.gPath + Image[0][0]);
            FloorType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

        }



        private void Uvalue_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Uvalue_comboBox.SelectedItem != null)
            {
                UMethod = Uvalue_comboBox.SelectedItem.ToString();
                Act_UcWMethod();
                Dilndi();
                Calc_Ueff();
            }
        }


        private void ISO_KS_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ISO_KS_comboBox.SelectedItem != null)
            {
                ISO_KS = ISO_KS_comboBox.SelectedItem.ToString();
                Calc_RseRsi();
                Calc_Ueff();
            }
        }

        private void TBName_textBox_TextChanged(object sender, EventArgs e)
        {
            if (TBName_textBox != null)
            {
                Calc_dU();
                Calc_Ueff();
            }
        }

        private void StructureType_textBox_TextChanged(object sender, EventArgs e)
        {
            if (StructureType_textBox != null)
            {
                StructureType = StructureType_textBox.Text;
            }
        }

        private void TB_button_Click(object sender, EventArgs e)
        {
            //지면접합, 단열지하의 경우 1D 열교를 미반영하고 지면접합 검토에서 반영하는 것으로 함( 그런데, 지면접합 유형을 단순방식으로 간단히 검토하기로 해서 추후 변경 필요)
            if (Base == "지면위" )
            {
                MessageBox.Show("지면위는 열교 평가는 하지 않습니다.");
                TBName_textBox.Text = "열교없음";
            }
            else if (Base == "단열지하실")
            {
                MessageBox.Show("단열지하실은 열교 평가는 하지 않습니다.");
                TBName_textBox.Text = "열교없음";
            }
            else if (Type == "내부덧댐")
            {
                MessageBox.Show("내부 덧댐일 경우 열교 평가는 하지 않습니다.");
                TBName_textBox.Text = "열교없음";
            }
            if (Type == "내부덧댐")
            {
                MessageBox.Show("내부 덧댐일 경우 열교 평가는 하지 않습니다.");
                TBName_textBox.Text = "열교없음";
            }
            else if (Type == "기존바닥")
            {
                MessageBox.Show("기존 바닥일 경우 열교 평가는 하지 않습니다.");
                TBName_textBox.Text = "열교없음";
            }
            else
            {
                if (UMethod == "계산" && Rtot == 0)
                {
                    MessageBox.Show("열관류율부터 계산하세요.");
                }
                else if (UMethod == "계산" && dins == 0)
                {
                    MessageBox.Show("단열재가 없으므로 열교 평가는 하지 않습니다.");
                    TBName_textBox.Text = "열교없음";
                }
                else
                {
                    Floor_TB TB_form = new Floor_TB(Type, StructureType, dins);
                    DialogResult result = TB_form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        TBName = TB_form.Select_TB[1];
                        TBName_textBox.Text = TBName;
                        check_StructureType = TB_form.Select_TB[2];
                        TBType = TB_form.Select_TB[3];
                        A = Convert.ToDouble(TB_form.Select_TB[4]);
                        B = Convert.ToDouble(TB_form.Select_TB[5]);
                        C = Convert.ToDouble(TB_form.Select_TB[6]);
                        PerArea = Convert.ToDouble(TB_form.Select_TB[7]);
                        if (PerArea != 0)
                        { PerArea_textBox.Text = string.Format("{0:F3}", PerArea); }

                        check_Type = TB_form.Select_TB[8];
                        check_dins = Convert.ToDouble(TB_form.Select_TB[9]);
                        dU = Convert.ToDouble(TB_form.Select_TB[10]);
                        dU_textBox.Text = string.Format("{0:F3}", dU);

                        if (PerArea_textBox.Text != "" && PerArea != 0)
                        { dU2_textBox.Text = string.Format("{0:F3}", dU); }

                        LinearPoint = TB_form.Select_TB[11];
                        PsiKai = Convert.ToDouble(TB_form.Select_TB[12]);

                        if (PerArea_textBox.Text != "" && PerArea != 0)
                        { PsiKai_textBox.Text = string.Format("{0:F3}", PsiKai); }

                        tabControl1.SelectedTab = tabControl1.TabPages["dU_tabPage"];
                        if (LinearPoint == "점형")
                        {
                            PerArea_label1.Text = "적용개수";
                            PerArea_label2.Text = "EA/m²";
                            PsiKai_label1.Text = "점형열교 열관류율";
                            PsiKai_label2.Text = "W/K";
                            dU_label3.Text = "1D 열교가산치";
                            dU_label4.Text = "W/m²·K";


                        }
                        else
                        {
                            PerArea_label1.Text = "적용길이";
                            PerArea_label2.Text = "m/m²";
                            PsiKai_label1.Text = "선형열교 열관류율";
                            PsiKai_label2.Text = "W/mK";
                            dU_label3.Text = "1D 열교가산치";
                            dU_label4.Text = "W/m²·K";
                        }

                        Load_TB_Image();
                    }
                }
            }
            Calc_Ueff();
        }

        private void Load_TB_Image()
        {
            try
            {
                if (LinearPoint == "선형")
                {
                    if (TBType != "")
                    {
                        string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "바닥선형열교이미지", "이미지_구조유형", "열교유형 = '" + TBType + "'");
                        pictureBox1.Visible = true;
                        pictureBox1.Load(Program.gPath + Image[0][0]);
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    if (TBType != "" && TBName != "")
                    {
                        string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "바닥선형열교이미지", "이미지_고정유형", "제품명 = '" + TBName + "' And 열교유형 = '" + TBType + "'");
                        pictureBox2.Visible = true;
                        pictureBox2.Load(Program.gPath + Image[0][0]);
                        pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
                else { }
            }
            catch { }
        }

        private void Load_table()
        {

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Ucalc_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Ucalc_dataGridView.Columns.Add(checkBoxColumn);

            Ucalc_dataGridView.ColumnCount = 7;
            Ucalc_dataGridView.Columns[1].HeaderText = "번호";
            Ucalc_dataGridView.Columns[2].HeaderText = "구분";
            Ucalc_dataGridView.Columns[3].HeaderText = "재료명";
            Ucalc_dataGridView.Columns[4].HeaderText = "열전도율" + Environment.NewLine + "[W/m·K]";
            Ucalc_dataGridView.Columns[5].HeaderText = "두께" + Environment.NewLine + "[mm]";
            Ucalc_dataGridView.Columns[6].HeaderText = "열저항" + Environment.NewLine + "[m²·K/W]";

        }
        private void Add_OldFloor()
        {
            for (int k = 0; k < Ucalc_dataGridView.RowCount; k++)
            {
                if (Ucalc_dataGridView.Rows[k].Cells[2].Value == "기존바닥")
                {
                    Ucalc_dataGridView.Rows.Remove(Ucalc_dataGridView.Rows[k]);
                }
                else { }
            }

            int nRow = Ucalc_dataGridView.Rows.Add();
            Ucalc_dataGridView.Rows[nRow].Cells[2].Value = "기존바닥";
            Ucalc_dataGridView.Rows[nRow].Cells[3].Value = OldFloor;
            Ucalc_dataGridView.Rows[nRow].Cells[5].Style.BackColor = SystemColors.Window;
            Ucalc_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F2}", OldFloor_R);
            Load_Material_Num();

        }


        private void AddMaterial_button_Click(object sender, EventArgs e)
        {
            MaterialDB form = new MaterialDB();
            DialogResult result = form.ShowDialog();
            if (Ucalc_dataGridView.Rows.Count < 10)
            {
                if (result == DialogResult.OK)
                {
                    int nRow = Ucalc_dataGridView.Rows.Add();
                    Ucalc_dataGridView.Rows[nRow].Cells[2].Value = form.Select[10];
                    Ucalc_dataGridView.Rows[nRow].Cells[3].Value = form.Select[1];
                    Ucalc_dataGridView.Rows[nRow].Cells[4].Value = form.Select[4];
                    Ucalc_dataGridView.Rows[nRow].Cells[5].Style.BackColor = SystemColors.Info;
                }
                Load_Material_Num();
                Calc_dins();
            }
            else
            {
                MessageBox.Show("열개까지만 생성 가능합니다.");
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Ucalc_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = Ucalc_dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Ucalc_dataGridView.RowCount; k++)
                {
                    if (k != row.Index)
                    {
                        Ucalc_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = Ucalc_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = SystemColors.Window;
                        row2.DefaultCellStyle.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = SystemColors.WindowText;
                        row = Ucalc_dataGridView.Rows[e.RowIndex];
                    }
                }
            }
        }

        private void DeleteMaterial_button_Click(object sender, EventArgs e)
        {
            Ucalc_dataGridView.Rows.Remove(Ucalc_dataGridView.Rows[SelectRow]);
            Load_Material_Num();
            Calc_dins();
        }

        private void MaterialUP_button_Click(object sender, EventArgs e)
        {
            if (Ucalc_dataGridView.CurrentRow.Index <= 0) return;
            int iCurrentRow = Ucalc_dataGridView.CurrentRow.Index;
            DataGridViewRow MoveRow = Ucalc_dataGridView.Rows[iCurrentRow];
            Ucalc_dataGridView.Rows.RemoveAt(iCurrentRow);
            Ucalc_dataGridView.Rows.Insert(iCurrentRow - 1, MoveRow);
            Ucalc_dataGridView.Rows[iCurrentRow - 1].Selected = true;
            Ucalc_dataGridView.CurrentCell = Ucalc_dataGridView[Ucalc_dataGridView.CurrentCell.ColumnIndex, iCurrentRow - 1];
            Load_Material_Num();
        }
        private void MaterialDown_button_Click(object sender, EventArgs e)
        {
            if (Ucalc_dataGridView.CurrentRow.Index < 0 || Ucalc_dataGridView.CurrentRow.Index + 1 == Ucalc_dataGridView.Rows.Count) return;
            int iCurrentRow = Ucalc_dataGridView.CurrentRow.Index;
            DataGridViewRow _dgvRow = Ucalc_dataGridView.Rows[iCurrentRow];
            Ucalc_dataGridView.Rows.RemoveAt(iCurrentRow);
            Ucalc_dataGridView.Rows.Insert(iCurrentRow + 1, _dgvRow);
            Ucalc_dataGridView.Rows[iCurrentRow + 1].Selected = true;
            Ucalc_dataGridView.CurrentCell = Ucalc_dataGridView[Ucalc_dataGridView.CurrentCell.ColumnIndex, iCurrentRow + 1];
            Load_Material_Num();
        }
        private void Load_Material_Num()
        {
            for (int k = 0; k < Ucalc_dataGridView.RowCount; k++)
            {
                Ucalc_dataGridView.Rows[k].Cells[1].Value = (k + 1).ToString();
            }
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 5)
                {
                    double d = Convert.ToDouble(Ucalc_dataGridView.Rows[e.RowIndex].Cells[5].Value);
                    double λ = Convert.ToDouble(Ucalc_dataGridView.Rows[e.RowIndex].Cells[4].Value);
                    double R = d / 1000 / λ;
                    Ucalc_dataGridView.Rows[e.RowIndex].Cells[6].Value = String.Format("{0:F2}", R);
                }
            }
            Calc_U();
            Calc_dins();
            Calc_Ueff();
        }

        private void Rule_U()
        {
            Dilndi();

            if (UMethod == "법규")
            {
                String[][] Date = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "법규시기,지역구분", "");
                String[][] Value;
                String DiIndi_;

                if (DiIndi == "직접외기" || DiIndi == "간접외기")
                {
                    DiIndi_ = DiIndi;
                }
                else
                {
                    DiIndi_ = "간접외기";
                }


                if (Type == "기존바닥")
                {
                    Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '바닥' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND 직접간접 =  '" + DiIndi_ + "'");

                }
                else
                {
                    Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '바닥' And 시기 = '2018.09' AND  지역 ='" + Date[0][1] + "'  AND 직접간접 =  '" + DiIndi_ + "'");
                }
                if (Value.Length > 0)
                {
                    Uvalue = Convert.ToDouble(Value[0][0]);
                    U_textBox.Text = string.Format("{0:F3}", Uvalue);
                    dins = (1 / Uvalue) * 0.04 * 1000;
                    Calc_dU();
                    // MessageBox.Show("[(" + Value[0][2] + " 시행)" + Value[0][1] + "] " + Value[0][3] + " 열관류율 적용");
                }
                else { }
            }
        }
        private void Calc_RseRsi()
        {
            if (ISO_KS != null && DiIndi != null)
            {
                String[][] RsiValue = Program.DB.getValue(DB.type.BaseDB_HCneed, "표면열전달저항", "저항값", "구조체 ='바닥' And 유형 = '실내' AND 기준 = '" + ISO_KS + "'");
                Rsi = Convert.ToDouble(RsiValue[0][0]);
                String[][] RseValue = Program.DB.getValue(DB.type.BaseDB_HCneed, "표면열전달저항", "저항값", "구조체 ='바닥' And 유형 = '" + DiIndi + "' AND 기준 = '" + ISO_KS + "'");
                Rse = Convert.ToDouble(RseValue[0][0]);

                Rsi_textBox.Text = string.Format("{0:F2}", Rsi);
                Rse_textBox.Text = string.Format("{0:F2}", Rse);
            }

        }

        private void Calc_U()
        {
            if (Ucalc_dataGridView.RowCount > 0)
            {
                dtot = 0;
                Rtot = 0;

                for (int k = 0; k < Ucalc_dataGridView.RowCount; k++)
                {
                    if (Ucalc_dataGridView.Rows[k].Cells[3].Value != null)
                    {
                        Material[k] = Ucalc_dataGridView.Rows[k].Cells[3].Value.ToString();
                    }
                    else { }
                    Material_d[k] = Convert.ToDouble(Ucalc_dataGridView.Rows[k].Cells[5].Value);
                    Material_λ[k] = Convert.ToDouble(Ucalc_dataGridView.Rows[k].Cells[4].Value);
                    Material_R[k] = Convert.ToDouble(Ucalc_dataGridView.Rows[k].Cells[6].Value);
                    dtot += Material_d[k];
                    Rtot += Material_R[k];
                }
                Rtot = Rsi + Rse + Rtot;

                Material_dtot_textBox.Text = String.Format("{0:F0}", dtot);
                Material_Rtot_textBox.Text = String.Format("{0:F2}", Rtot);
                Uvalue = 1 / Rtot;
                U_textBox.Text = string.Format("{0:F3}", Uvalue);
            }
            else { return; }
        }

        private void Calc_dins()
        {
            dins = 0;
            for (int k = 0; k < Ucalc_dataGridView.RowCount; k++)
            {
                if (Ucalc_dataGridView.Rows[k].Cells[2].Value != null)
                {
                    if (Ucalc_dataGridView.Rows[k].Cells[2].Value.ToString() == "단열재")
                    {
                        dins += Convert.ToDouble(Ucalc_dataGridView.Rows[k].Cells[5].Value);
                    }

                }
            }
            Calc_dU();

        }

        //단열두께 달라지면 열교가산치 재산정 
        private void Calc_dU()
        {
            if (PerArea_textBox.Text != "" && PerArea != 0)
            {
                PsiKai = (A * Math.Pow(dins, 2) + B * dins + C) / 1000;
                PsiKai_textBox.Text = string.Format("{0:F3}", PsiKai);
                dU = PsiKai * PerArea;
                dU_textBox.Text = string.Format("{0:F3}", dU);
                dU2_textBox.Text = string.Format("{0:F3}", dU);
            }

        }

        private void Calc_Ueff()
        {
            Ueff = dU + Uvalue;
            Ueff_textBox.Text = string.Format("{0:F3}", Ueff);
        }

        private void Previous_button_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("이전 화면으로 이동하시겠습니까?", "이전 화면 이동", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                this.DialogResult = DialogResult.OK;
                this.Hide();
                Program.getMenuForm().DoLoadForm(36, OnLoadListProc);
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (FloorName == null)
            {
                MessageBox.Show("바닥 명칭을 입력하세요.");
            }
            else if (Type == null)
            {
                MessageBox.Show("바닥 리모델링 유형을 선택하세요.");
            }
            else if (Type != "기존바닥")
            {
                if (TBName == null)
                {
                    MessageBox.Show("열교를 입력하세요.");
                }
                else
                {
                    Save();
                }
            }
            else
            {
                Save();
            }
        }
        public static bool OnLoadListProc(Form form)
        {
            List_ConstructionFloor f = (List_ConstructionFloor)form;
            f.load_List();
            return true;
        }

        private void Save()
        {
            Program.DB.setValue(DB.type.ProjDB, "ConstructionFloor", "번호,명칭,Type,기존바닥,기초설치,U적용방법,직접간접,구조유형,열교유형,열교종류,표면열전달저항기준,선형점형," +
                "A,B,C,PsiKai,단위면적당적용," +
                "Rse,Rsi,두께합계,열저항합계,단열재두께," +
                "재료1종류,재료1두께," +
                "재료2종류,재료2두께," +
                "재료3종류,재료3두께," +
                "재료4종류,재료4두께," +
                "재료5종류,재료5두께," +
                "재료6종류,재료6두께," +
                "재료7종류,재료7두께," +
                "재료8종류,재료8두께," +
                "재료9종류,재료9두께," +
                "재료10종류,재료10두께," +
                "열관류율,열교가산치,유효열관류율",
                "'" + FloorNum_textBox.Text + "','" + FloorName + "','" + Type + "','" + OldFloor + "','" + Base + "','" + UMethod + "','" + DiIndi + "','" + StructureType + "','" + TBType + "','" + TBName + "','" + ISO_KS + "','" + LinearPoint + "','" +
                A.ToString() + "','" + B.ToString() + "','" + C.ToString() + "','" + PsiKai.ToString() + "','" + PerArea.ToString() + "','" +
                Rse.ToString() + "','" + Rsi.ToString() + "','" + dtot.ToString() + "','" + Rtot.ToString() + "','" + dins.ToString() + "','" +
                Material[0] + "','" + Material_d[0].ToString() + "','" +
                Material[1] + "','" + Material_d[1].ToString() + "','" +
                Material[2] + "','" + Material_d[2].ToString() + "','" +
                Material[3] + "','" + Material_d[3].ToString() + "','" +
                Material[4] + "','" + Material_d[4].ToString() + "','" +
                Material[5] + "','" + Material_d[5].ToString() + "','" +
                Material[6] + "','" + Material_d[6].ToString() + "','" +
                Material[7] + "','" + Material_d[7].ToString() + "','" +
                Material[8] + "','" + Material_d[8].ToString() + "','" +
                Material[9] + "','" + Material_d[9].ToString() + "','" +
                Uvalue.ToString() + "','" + dU.ToString() + "','" + Ueff.ToString()
                 + "'", "번호");
            this.DialogResult = DialogResult.OK;
            this.Hide();
            Program.getMenuForm().DoLoadForm(36, OnLoadListProc);
        }


        //흡수율,색깔,커튼월 지우기
        //기초설치 넣기
        private void reset()
        {
            FloorNum_textBox.Text = "";
            Name_textBox.Text = "";

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;

            OldFloor_comboBox.SelectedItem = null;
            Uvalue_comboBox.SelectedItem = null;
            Base_comboBox.SelectedItem = null;
            DiIndi_textBox.Text = null;
            DiIndi2_textBox.Text = null;

            StructureType_textBox.Text = null;

            TBType_textBox.Text = null;
            TBName_textBox.Text = null;
            TBName2_textBox.Text = null;

            ISO_KS_comboBox.SelectedItem = null;

            PsiKai_textBox.Text = null;
            PerArea_textBox.Text = null;

            PerArea_label1.Text = null;
            PerArea_label2.Text = null;
            PsiKai_label1.Text = null;
            PsiKai_label2.Text = null;
            dU_label3.Text = null;
            dU_label4.Text = null;

            Rse_textBox.Text = null;
            Rsi_textBox.Text = null;

            Material_dtot_textBox.Text = null;
            Material_Rtot_textBox.Text = null;

            U_textBox.Text = null;

            dU_textBox.Text = null;
            dU2_textBox.Text = "";

            Ueff_textBox.Text = null;

            Ucalc_dataGridView.Rows.Clear();

            FloorType_pictureBox.Visible = false;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            A = 0;
            B = 0;
            C = 0;
            PsiKai = 0;
            PerArea = 0;
            Rse = 0;
            Rsi = 0;
            for (int i = 0; i < 10; i++)
            {
                Material_d[i] = 0;
                Material_λ[i] = 0;
                Material_R[i] = 0;
            }
            dtot = 0;
            Rtot = 0;
            dins = 0;
            check_dins = 0;
            OldFloor_R = 0;
            Uvalue = 0;
            dU = 0;
            Ueff = 0;
            SelectRow = 0;

            Old = null;
            FloorNum = null;
            FloorName = null;
            Type = null;
            check_Type = null;
            OldFloor = null;
            UMethod = null;
            DiIndi = null;
            StructureType = null;
            check_StructureType = null;
            TBType = null;
            TBName = null;
            ISO_KS = null;
            LinearPoint = null;
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();

            try
            {
                FloorNum_textBox.Text = ID;
                FloorNum = ID;



                String[][] Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionFloor", "번호,명칭,Type,기존바닥,기초설치,U적용방법,직접간접,구조유형,열교유형,열교종류,표면열전달저항기준,선형점형," +
                  "A,B,C,PsiKai,단위면적당적용," +
                  "Rse,Rsi,두께합계,열저항합계,단열재두께," +
                  "재료1종류,재료1두께," +
                  "재료2종류,재료2두께," +
                  "재료3종류,재료3두께," +
                  "재료4종류,재료4두께," +
                  "재료5종류,재료5두께," +
                  "재료6종류,재료6두께," +
                  "재료7종류,재료7두께," +
                  "재료8종류,재료8두께," +
                  "재료9종류,재료9두께," +
                  "재료10종류,재료10두께," +
                  "열관류율,열교가산치,유효열관류율"
                       , "번호 = '" + ID + "'");
                Name_textBox.Text = Load[0][1];
                Type = Load[0][2];
                switch (Type)
                {
                    case "기존바닥":
                        radioButton1.Checked = true;
                        break;
                    case "신규":
                        radioButton2.Checked = true;
                        break;
                    case "철거 후 신규":
                        radioButton3.Checked = true;
                        break; ;
                    case "외부덧댐":
                        radioButton4.Checked = true;
                        break; ;
                    case "내부덧댐":
                        radioButton5.Checked = true;
                        break; ;
                }
                OldFloor = Load[0][3];
                DataRowView? item = OldFloor_comboBox.SelectedItem as DataRowView;
                OldFloor_comboBox.SelectedItem = Load[0][3];
                if (item != null)
                {
                    OldFloor = item.Row.ItemArray[0].ToString();
                }

                Base = Load[0][4];
                Base_comboBox.SelectedItem = base.ToString();

                UMethod = Load[0][5];
                Uvalue_comboBox.SelectedItem = UMethod;

                DiIndi = Load[0][6];
                DiIndi_textBox.Text = DiIndi;
                DiIndi2_textBox.Text = DiIndi;


                StructureType = Load[0][7];
                check_StructureType = Load[0][7];
                StructureType_textBox.Text = StructureType;

                TBType = Load[0][8];
                TBType_textBox.Text = TBType;

                TBName = Load[0][9];
                TBName_textBox.Text = TBName;
                TBName2_textBox.Text = TBName;


                ISO_KS = Load[0][10];
                ISO_KS_comboBox.SelectedItem = ISO_KS;

                LinearPoint = Load[0][11];

                if (LinearPoint == "점형")
                {
                    PerArea_label1.Text = "적용개수";
                    PerArea_label2.Text = "EA/m²";
                    PsiKai_label1.Text = "점형열교 열관류율";
                    PsiKai_label2.Text = "W/K";
                    dU_label3.Text = "1D 열교가산치";
                    dU_label4.Text = "W/m²·K";
                }
                else if (LinearPoint == "선형")
                {
                    PerArea_label1.Text = "적용길이";
                    PerArea_label2.Text = "m/m²";
                    PsiKai_label1.Text = "선형열교 열관류율";
                    PsiKai_label2.Text = "W/mK";
                    dU_label3.Text = "1D 열교가산치";
                    dU_label4.Text = "W/m²·K";
                }
                else { }

                A = Convert.ToDouble(Load[0][12]);
                B = Convert.ToDouble(Load[0][13]);
                C = Convert.ToDouble(Load[0][14]);

                PsiKai = Convert.ToDouble(Load[0][15]);


                PerArea = Convert.ToDouble(Load[0][16]);

                Rse = Convert.ToDouble(Load[0][17]);
                Rse_textBox.Text = string.Format("{0:F2}", Rse);
                Rsi = Convert.ToDouble(Load[0][18]);
                Rsi_textBox.Text = string.Format("{0:F2}", Rsi);

                dtot = Convert.ToDouble(Load[0][19]);
                Rtot = Convert.ToDouble(Load[0][20]);
                Material_dtot_textBox.Text = String.Format("{0:F0}", dtot);
                Material_Rtot_textBox.Text = String.Format("{0:F2}", Rtot);

                dins = Convert.ToDouble(Load[0][21]);
                check_dins = Convert.ToDouble(Load[0][21]);

                Uvalue = Convert.ToDouble(Load[0][42]);
                U_textBox.Text = string.Format("{0:F3}", Uvalue);

                dU = Convert.ToDouble(Load[0][43]);
                dU_textBox.Text = string.Format("{0:F3}", dU);

                if (PerArea != 0)
                {
                    PerArea_textBox.Text = string.Format("{0:F3}", Convert.ToDouble(Load[0][16]));
                }
                if (PerArea_textBox.Text != "" && PerArea != 0)
                {
                    PsiKai_textBox.Text = string.Format("{0:F3}", Convert.ToDouble(Load[0][15]));
                    dU2_textBox.Text = string.Format("{0:F3}", Convert.ToDouble(Load[0][43]));
                }

                Ueff = Convert.ToDouble(Load[0][44]);
                Ueff_textBox.Text = string.Format("{0:F3}", Ueff);

                Ucalc_dataGridView.Rows.Clear();
                for (int i = 0; i < 10; i++)
                {
                    Material[i] = Load[0][(2 * i + 22)];
                    Material_d[i] = Convert.ToDouble(Load[0][(2 * i + 23)]);
                }


                for (int i = 0; i < 10; i++)
                {
                    if (Material[i] != "")
                    {
                        string[][] Value;
                        string[][] OldFloor_U;
                        int nRow = Ucalc_dataGridView.Rows.Add();
                        Value = Program.DB.getValue(DB.type.ProjDB, "User_Material", "구분,열전도율", "재료명 = '" + Material[i] + "'");
                        if (Value.Length == 0)
                        {
                            Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "열전도율", "구분,열전도율", "재료명 = '" + Material[i] + "'");
                        }
                        try
                        {
                            Material_λ[i] = Convert.ToDouble(Value[0][1]);
                            Material_R[i] = Convert.ToDouble(Load[0][(2 * i + 23)]) / 1000 / Material_λ[i];


                            Ucalc_dataGridView.Rows[nRow].Cells[2].Value = Value[0][0];
                            Ucalc_dataGridView.Rows[nRow].Cells[3].Value = Material[i];
                            Ucalc_dataGridView.Rows[nRow].Cells[4].Value = Value[0][1];
                            Ucalc_dataGridView.Rows[nRow].Cells[5].Value = Load[0][(2 * i + 23)];
                            Ucalc_dataGridView.Rows[nRow].Cells[5].Style.BackColor = SystemColors.Info;
                            Ucalc_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F2}", Material_R[i]);
                        }
                        catch { }

                        if (Value.Length == 0)
                        {
                            OldFloor_U = Program.DB.getValue(DB.type.ProjDB, "ConstructionFloor", "열관류율", "명칭 = '" + Material[i] + "'");
                            try
                            {
                                OldFloor_R = 1 / Convert.ToDouble(OldFloor_U[0][0]);
                                Ucalc_dataGridView.Rows[nRow].Cells[2].Value = "기존바닥";
                                Ucalc_dataGridView.Rows[nRow].Cells[3].Value = OldFloor;
                                Ucalc_dataGridView.Rows[nRow].Cells[5].Style.BackColor = SystemColors.Window;
                                Ucalc_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F2}", OldFloor_R);
                            }
                            catch { }

                        }
                        else { }
                    }
                    else { }
                }

                Load_Material_Num();

                string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '최하층바닥'");
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                Load_FloorType_image(Type, Base);
                Load_TB_Image();
            }
            catch { }

        }

        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            FloorNum_textBox.Text = ID;
            FloorNum = ID;
        }

    }
}

