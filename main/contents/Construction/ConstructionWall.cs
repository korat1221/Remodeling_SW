using main.contentslist;
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
using main.subcontents;
using main.subcontents.ConstructionWall;
using main.subcontents.ConstructionCW;
using System.Net;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace main.contents
{
    public partial class ConstructionWall : Form
    {
        String WallNum, WallName, Type, check_Type, OldWall, CWName, UMethod, DiIndi, StructureType, check_StructureType, TBType, TBName, Color, Rule_RseRsi, LinearPoint;
        double α, Uvalue, dU, Ueff, PerArea, Rse, Rsi, dtot, Rtot, dins, check_dins;
        double A, B, C, PsiKai;
        String[][] Old;
        int SelectRow;
        List<double> Material_d = new List<double>(); List<double> Material_λ = new List<double>(); List<double> Material_R = new List<double>();

        public ConstructionWall()
        {
            InitializeComponent();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '외벽'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            //외장재색 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed,Color_comboBox, "외벽", "외장재색", "1");
            //직접간접 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, DiIndi_comboBox, "외벽", "직접/간접", "1");
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, DiIndi2_comboBox, "외벽", "직접/간접", "1");
            //표면열전달저항기준 콤보박스 
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, RseRsi_comboBox, "외벽", "실내외표면열전달저항", "1");
            //구조유형콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, StructureType_comboBox, "외벽", "구조유형", "3");
            Load_table();
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void Name_textBox_TextChanged(object sender, EventArgs e)
        {
            WallName = Name_textBox.Text;
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            Type = "기존외벽";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_WallType_image(Type);
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            Type = "신규";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_WallType_image(Type);
        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {
            Type = "철거 후 신규";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_WallType_image(Type);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Type = "외부덧댐";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_WallType_image(Type);
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            Type = "내부덧댐";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_WallType_image(Type);
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            Type = "커튼월덧댐";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_WallType_image(Type);
        }

        private void Changed_Type(String Type)
        {
            Uvalue_comboBox.Items.Clear();

            switch (Type)
            {
                case "기존외벽":
                    Uvalue_comboBox.Items.Add("계산");
                    Uvalue_comboBox.Items.Add("법규");
                    Uvalue_comboBox.Items.Add("진단");
                    OldWall_label.Visible = false;
                    OldWall_comboBox.Visible = false;
                    CW_label.Visible = false;
                    CW_comboBox.Visible = false;
                    break;

                case "신규":
                    Uvalue_comboBox.Items.Add("계산");
                    Uvalue_comboBox.Items.Add("법규");
                    OldWall_label.Visible = false;
                    OldWall_comboBox.Visible = false;
                    CW_label.Visible = false;
                    CW_comboBox.Visible = false;
                    break;

                case "철거 후 신규":
                    Uvalue_comboBox.Items.Add("계산");
                    Uvalue_comboBox.Items.Add("법규");
                    OldWall_label.Visible = true;
                    OldWall_comboBox.Visible = true;
                    Load_OldWall(Type);
                    CW_label.Visible = false;
                    CW_comboBox.Visible = false;
                    break;

                case "외부덧댐":
                    Uvalue_comboBox.Items.Add("계산");
                    OldWall_label.Visible = true;
                    OldWall_comboBox.Visible = true;
                    Load_OldWall(Type);
                    CW_label.Visible = false;
                    CW_comboBox.Visible = false;
                    break;

                case "내부덧댐":
                    Uvalue_comboBox.Items.Add("계산");
                    OldWall_label.Visible = true;
                    OldWall_comboBox.Visible = true;
                    Load_OldWall(Type);
                    CW_label.Visible = false;
                    CW_comboBox.Visible = false;
                    break;

                case "커튼월덧댐":
                    Uvalue_comboBox.Items.Add("계산");
                    OldWall_label.Visible = true;
                    OldWall_comboBox.Visible = true;
                    Load_OldWall(Type);
                    Load_OldCW(Type);
                    CW_label.Visible = true;
                    CW_comboBox.Visible = true;
                    break;
            }

            Uvalue_comboBox.SelectedIndex = 0;
        }

        //기존 외벽 리스트 불러오기 
        private void Load_OldWall(String Type)
        {
            string def_value;
            String[][] Table;

            if (Type == "철거 후 신규" || Type == "외부덧댐" || Type == "내부덧댐" || Type == "커튼월덧댐")
            {
                def_value = "Type = '기존외벽'";
                Table = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "명칭", def_value);
            }
            else
            {
                def_value = "Type = ''";
                Table = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "명칭", def_value);
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

            OldWall_comboBox.DataSource = sources.DefaultView;
            OldWall_comboBox.DisplayMember = "Text";
            for (i = 0; i < OldWall_comboBox.Items.Count; i++)
            {
                var arr = ((DataRowView)OldWall_comboBox.Items[i]).Row.ItemArray;
                if (arr.Length > 1 && arr[1].ToString() == def_value)
                {
                    OldWall_comboBox.SelectedIndex = i;
                    break;
                }
            }

        }
        private void OldWall_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OldWall = OldWall_comboBox.SelectedItem.ToString();
        }

        //기존 커튼월창 리스트 불러오기 
        private void Load_OldCW(String Type)
        {
            string def_value;
            String[][] Table;

            if (Type == "커튼월덧댐")
            {
                def_value = "Type = '기존 커튼월창'";
                Table = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "명칭", def_value);
            }
            else
            {
                def_value = "Type = ''";
                Table = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "명칭", def_value);
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

            OldWall_comboBox.DataSource = sources.DefaultView;
            OldWall_comboBox.DisplayMember = "Text";
            for (i = 0; i < OldWall_comboBox.Items.Count; i++)
            {
                var arr = ((DataRowView)OldWall_comboBox.Items[i]).Row.ItemArray;
                if (arr.Length > 1 && arr[1].ToString() == def_value)
                {
                    OldWall_comboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        private void CW_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CWName = CW_comboBox.SelectedItem.ToString();
        }

        private void Load_WallType_image(String Type)
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽유형이미지", "이미지", "외벽유형 = '" + Type + "'");
            WallType_pictureBox.Load(Program.gPath + Image[0][0]);
            WallType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

        }

        private void Color_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color = Color_comboBox.SelectedItem.ToString();
            String[][] value = Program.DB.getValue(DB.type.BaseDB_HCneed, "흡수율", "흡수율", "외장재색 = '" + Color + "'");
            α = Convert.ToDouble(value[0][0]);
            α_textBox.Text = String.Format("{0:F1}", α);
        }
        private void Uvalue_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UMethod = Uvalue_comboBox.SelectedItem.ToString();
            Act_UcWMethod();
        }

        private void Act_UcWMethod()
        {
            if (UMethod == "계산")
            {
                U_textBox.Enabled = false;
                U_textBox.BorderStyle = BorderStyle.None;
                DiIndi_comboBox.Visible = false;
                Ucalc_dataGridView.Show();
                Ucalc_tabPage.Enabled = true;
            }
            else if (UMethod == "법규")
            {
                Rule_U();
                U_textBox.Enabled = false;
                U_textBox.BorderStyle = BorderStyle.None;
                DiIndi_comboBox.Visible = true;
                Ucalc_dataGridView.Hide();
                Ucalc_tabPage.Enabled = false;
            }
            else if (UMethod == "진단")
            {
                U_textBox.Enabled = true;
                U_textBox.BorderStyle = BorderStyle.FixedSingle;
                DiIndi_comboBox.Visible = true;
                Ucalc_dataGridView.Hide();
                Ucalc_tabPage.Enabled = false;
            }
        }

        private void U_textBox_TextChanged(object sender, EventArgs e)
        {
            if (UMethod == "진단" && U_textBox.Text != string.Empty)
            {
                Uvalue = Convert.ToDouble(U_textBox.Text);
                dins = (1 / Uvalue) * 0.04 * 1000;
            }
            Calc_dU();
            Calc_Ueff();
        }


        private void DiIndi_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DiIndi = DiIndi_comboBox.SelectedItem.ToString();
            Rule_U();
            Calc_Ueff();
        }

        private void RseRsi_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Rule_RseRsi = RseRsi_comboBox.SelectedItem.ToString();
            Calc_RseRsi();
            Calc_Ueff();
        }

        private void DiIndi2_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DiIndi = DiIndi_comboBox.SelectedItem.ToString();
            Calc_RseRsi();
            Calc_Ueff();
        }
        private void StructureType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            StructureType = StructureType_comboBox.SelectedItem.ToString();

            //구조 유형 다시 선택했을 경우 
            try
            {
                if (check_StructureType != null)
                {
                    if (StructureType != check_StructureType)
                    {
                        MessageBox.Show("열교 종류를 다시 선택하세요.");
                        TBName = "";
                        TBName_textBox.Text = TBName;
                    }
                }
                else { }
            }
            catch { }
            Calc_dU();
            Calc_Ueff();
        }

        private void TB_button_Click(object sender, EventArgs e)
        {
            if (StructureType == null)
            {
                MessageBox.Show("구조유형부터 선택하세요.");
            }
            else if (Type == "커튼월덧댐")
            {
                MessageBox.Show("커튼월덧댐일 경우 열교 평가는 하지 않습니다.");
                TBName_textBox.Text = "열교없음";
            }
            else if (Type == "기존외벽" && StructureType != "경량철골조")
            {
                MessageBox.Show("기존 외벽일 경우 열교 평가는 하지 않습니다.");
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
                    Wall_TB TB_form = new Wall_TB(Type, StructureType, dins);
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
                        PerArea_textBox.Text = string.Format("{0:F3}", PerArea);
                        check_Type = TB_form.Select_TB[8];
                        check_dins = Convert.ToDouble(TB_form.Select_TB[9]);
                        dU = Convert.ToDouble(TB_form.Select_TB[10]);
                        dU_textBox.Text = string.Format("{0:F3}", dU);
                        dU2_textBox.Text = string.Format("{0:F3}", dU);
                        LinearPoint = TB_form.Select_TB[11];
                        PsiKai = Convert.ToDouble(TB_form.Select_TB[12]);
                        PsiKai_textBox.Text = string.Format("{0:F3}", PsiKai);

                        if (LinearPoint == "점형")
                        {
                            PerArea_label1.Text = "적용개수";
                            PerArea_label2.Text = "EA/m²";
                            PsiKai_label1.Text = "점형열교 열관류율";
                            PsiKai_label2.Text = "W/K";
                        }
                        else
                        {
                            PerArea_label1.Text = "적용길이";
                            PerArea_label2.Text = "m/m²";
                            PsiKai_label1.Text = "선형열교 열관류율";
                            PsiKai_label2.Text = "W/mK";
                        }

                        try
                        {
                            if (LinearPoint == "점형")
                            {
                                string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽점형열교이미지", "이미지_구조유형", "구조유형 = '" + StructureType + "'");
                                pictureBox1.Load(Program.gPath + Image[0][0]);
                                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                                Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽점형열교이미지", "이미지_고정유형", "구조유형 = '" + StructureType + "' And 열교유형 = '" + TBType + "'");
                                pictureBox2.Load(Program.gPath + Image[0][0]);
                                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                            }
                            else
                            {
                                string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽선형열교이미지", "이미지_구조유형", "구조유형 = '" + StructureType + "'");
                                pictureBox1.Load(Program.gPath + Image[0][0]);
                                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                                Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽선형열교이미지", "이미지_고정유형", "구조유형 = '" + StructureType + "' And 열교유형 = '" + TBType + "'");
                                pictureBox2.Load(Program.gPath + Image[0][0]);
                                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                            }

                           
                        }
                        catch { }
                    }
                }
            }
            Calc_Ueff();
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

        private void AddMaterial_button_Click(object sender, EventArgs e)
        {
            MaterialDB form = new MaterialDB();
            DialogResult result = form.ShowDialog();
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
            if (UMethod == "법규")
            {
                String[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율", "구조체 = '창호' And 시기 = '2018.09' AND  지역 = '중부1' AND 직접간접 =  '" + DiIndi + "'");
                Uvalue = Convert.ToDouble(Value[0][0]);
                U_textBox.Text = string.Format("{0:F3}", Uvalue);
                dins = (1 / Uvalue) * 0.04 * 1000;
                Calc_dU();
            }
        }
        private void Calc_RseRsi()
        {
            if (Rule_RseRsi != null && DiIndi != null)
            {
                String[][] RsiValue = Program.DB.getValue(DB.type.BaseDB_HCneed, "표면열전달저항", "저항값", "구조체 ='외벽' And 유형 = '실내' AND 기준 = '" + Rule_RseRsi + "'");
                Rsi = Convert.ToDouble(RsiValue[0][0]);
                String[][] RseValue = Program.DB.getValue(DB.type.BaseDB_HCneed, "표면열전달저항", "저항값", "구조체 ='외벽' And 유형 = '" + DiIndi + "' AND 기준 = '" + Rule_RseRsi + "'");
                Rse = Convert.ToDouble(RseValue[0][0]);

                Rsi_textBox.Text = string.Format("{0:F2}", Rsi);
                Rse_textBox.Text = string.Format("{0:F2}", Rse);
            }
        }

        private void Calc_U()
        {
            if (Ucalc_dataGridView.RowCount > 0)
            {
                Material_d.Clear();
                Material_λ.Clear();
                Material_R.Clear();
                dtot = 0;
                Rtot = 0;

                for (int k = 0; k < Ucalc_dataGridView.RowCount; k++)
                {
                    Material_d.Add(Convert.ToDouble(Ucalc_dataGridView.Rows[k].Cells[5].Value));
                    Material_λ.Add(Convert.ToDouble(Ucalc_dataGridView.Rows[k].Cells[4].Value));
                    Material_R.Add(Convert.ToDouble(Ucalc_dataGridView.Rows[k].Cells[6].Value));
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
                if (Ucalc_dataGridView.Rows[k].Cells[2].Value == "단열재")
                {
                    dins += Convert.ToDouble(Ucalc_dataGridView.Rows[k].Cells[5].Value);
                }
            }
            Calc_dU();
        }

        //단열두께 달라지면 열교가산치 재산정 
        private void Calc_dU()
        {
            PsiKai = (A * Math.Pow(dins, 2) + B * dins + C) / 1000;
            PsiKai_textBox.Text = string.Format("{0:F3}", PsiKai);
            dU = PsiKai * PerArea;
            dU_textBox.Text = string.Format("{0:F3}", dU);
            dU2_textBox.Text = string.Format("{0:F3}", dU);
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
                Program.getMenuForm().DoLoadForm(30, OnLoadListProc);
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (WallName == null)
            {
                MessageBox.Show("커튼월 명칭을 입력하세요.");
            }
            else if (Type == null)
            {
                MessageBox.Show("커튼월 리모델링 유형을 선택하세요.");
            }
            else if (TBName == null)
            {
                MessageBox.Show("설치열교를 선택하세요.");
            }
            else if (UMethod == "계산")
            {
            }
            else
            {
                Save();
            }
        }
        public static bool OnLoadListProc(Form form)
        {
            //   List_ConstructionWall f = (List_ConstructionWall)form;

            //f.load_List();

            return true;
        }

        private void Save()
        {
        }


        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {

        }

        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            CWNum_textBox.Text = ID;
            WallNum = ID;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void PerArea_textBox_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
