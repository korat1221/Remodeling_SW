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
using main.subcontents.ConstructionRoof;
using System.Net;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using Microsoft.Web.WebView2.Core;

namespace main.contents
{
    public partial class ConstructionRoof : Form
    {
        String RoofNum, RoofName, Type, check_Type, OldRoof, UMethod, DiIndi, StructureType, check_StructureType, TBType, TBName, Color_Envelope, ISO_KS, LinearPoint;
        double A, B, C, PsiKai, PerArea;
        double Rse, Rsi, dtot, Rtot, dins, check_dins;
        double OldRoof_R;
        double α, Uvalue, dU, Ueff;
        int SelectRow;
        String[] Material = new String[10];
        double[] Material_d = new double[10];//두께
        double[] Material_λ = new double[10];
        double[] Material_R = new double[10];
        double[] Material_T = new double[12]; //온도
        bool scriptable = false;

        public ConstructionRoof()
        {
            InitializeComponent(); this.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular);
            new StackedHeaderDecorator(Ucalc_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, Ucalc_dataGridView_RowHandle);

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '지붕'");
            if(Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            string[][] value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호", "");
            if (value.Length > 0)
            {
                if (value[0][0] == "1")
                {
                    radioButton1.Checked = true;
                    radioButton2.Enabled = false;
                    radioButton3.Enabled = false;
                    radioButton4.Enabled = false;
                    radioButton5.Enabled = false;
                }
                else if (value[0][0] == "4")
                {
                    radioButton1.Enabled = false;
                    radioButton2.Checked = true;
                    radioButton3.Enabled = false;
                    radioButton4.Enabled = false;
                    radioButton5.Enabled = false;
                }
            }

            //외장재색 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, Color_comboBox, "지붕", "외장재색", "1");
            //직접간접 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, DiIndi_comboBox, "지붕", "실외조건", "1");
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, DiIndi2_comboBox, "지붕", "실외조건", "1");
            //표면열전달저항기준 콤보박스 
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, ISO_KS_comboBox, "지붕", "실내외표면열전달저항", "1");
            //구조유형콤보박스
            //Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, StructureType_comboBox, "지붕", "구조유형", "3");
            Load_table();
            InitializeAsync();
        }
        async void InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.NavigationCompleted += OnNaviCompleted;
        }

        void OnNaviCompleted(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            scriptable = true;
        }
        public void runScript(string script)
        {
            if (scriptable)
            {
                webView21.CoreWebView2.ExecuteScriptAsync(script);
            }
        }
        private bool Ucalc_dataGridView_RowHandle(DataGridViewCell cell, int column, int row)
        {
            if (Ucalc_dataGridView.Rows[row].Cells[2].Value != null && Ucalc_dataGridView.Rows[row].Cells[2].Value.ToString() == "기존지붕")
            {
                if (column == 4)
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.SelectionForeColor = Color.Black;
                    return true;
                }
                else { return false; }
            }
            if (column == 6)
            {
                cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                return true;
            }
            else return false;
        }
        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);

        }

        private void Name_textBox_TextChanged(object sender, EventArgs e)
        {
            if (Name_textBox.Text != null)
            {
                RoofName = Name_textBox.Text.ToString();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Type = "기존지붕";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_RoofType_image(Type);
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Type = "신규";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_RoofType_image(Type);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Type = "철거 후 신규";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_RoofType_image(Type);
        }

        private void radioButton4_CheckedChanged_1(object sender, EventArgs e)
        {
            Type = "외부덧댐";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_RoofType_image(Type);
        }

        private void radioButton5_CheckedChanged_1(object sender, EventArgs e)
        {
            Type = "내부덧댐";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_RoofType_image(Type);
        }

        private void Changed_Type(String Type)
        {
            Uvalue_comboBox.Items.Clear();
            StructureType_comboBox.Items.Clear();

            switch (Type)
            {
                case "기존지붕":
                    Uvalue_comboBox.Items.Add("계산");
                    Uvalue_comboBox.Items.Add("법규");
                    Uvalue_comboBox.Items.Add("진단");
                    StructureType_comboBox.Items.Add("경량철골조");
                    StructureType_comboBox.Items.Add("목구조");
                    StructureType_comboBox.Items.Add("콘크리트조");
                    OldRoof_label.Visible = false;
                    OldRoof_comboBox.Visible = false;
                    break;

                case "신규":
                    Uvalue_comboBox.Items.Add("계산");
                    Uvalue_comboBox.Items.Add("법규");
                    StructureType_comboBox.Items.Add("경량철골조");
                    StructureType_comboBox.Items.Add("목구조");
                    StructureType_comboBox.Items.Add("콘크리트조");
                    OldRoof_label.Visible = false;
                    OldRoof_comboBox.Visible = false;
                    break;

                case "철거 후 신규":
                    Uvalue_comboBox.Items.Add("계산");
                    Uvalue_comboBox.Items.Add("법규");
                    StructureType_comboBox.Items.Add("경량철골조");
                    StructureType_comboBox.Items.Add("목구조");
                    StructureType_comboBox.Items.Add("콘크리트조");
                    OldRoof_label.Visible = true;
                    OldRoof_comboBox.Visible = true;
                    Load_OldRoof(Type);
                    break;

                case "외부덧댐":
                    Uvalue_comboBox.Items.Add("계산");
                    StructureType_comboBox.Items.Add("기존지붕");
                    OldRoof_label.Visible = true;
                    OldRoof_comboBox.Visible = true;
                    Load_OldRoof(Type);
                    break;

                case "내부덧댐":
                    Uvalue_comboBox.Items.Add("계산");
                    StructureType_comboBox.Items.Add("기존지붕");
                    OldRoof_label.Visible = true;
                    OldRoof_comboBox.Visible = true;
                    Load_OldRoof(Type);
                    break;

            }

            Uvalue_comboBox.SelectedIndex = 0;
        }

        //기존 지붕 리스트 불러오기 
        private void Load_OldRoof(String Type)
        {
            string def_value;
            String[][] Table;

            if (Type == "철거 후 신규" || Type == "외부덧댐" || Type == "내부덧댐")
            {
                def_value = "Type = '기존지붕'";
                Table = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "명칭", def_value);
            }
            else
            {
                def_value = "Type = ''";
                Table = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "명칭", def_value);
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

            OldRoof_comboBox.DataSource = sources.DefaultView;
            OldRoof_comboBox.DisplayMember = "Text";
            for (i = 0; i < OldRoof_comboBox.Items.Count; i++)
            {
                var arr = ((DataRowView)OldRoof_comboBox.Items[i]).Row.ItemArray;
                if (arr.Length > 1 && arr[1].ToString() == def_value)
                {
                    OldRoof_comboBox.SelectedIndex = i;
                    break;
                }
            }

        }

        private void OldRoof_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView? item = OldRoof_comboBox.SelectedItem as DataRowView;
            if (item != null)
            {
                OldRoof = item.Row.ItemArray[0].ToString();
                string[][] OldRoof_U = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "열관류율", "명칭 = '" + OldRoof + "'");
                if(OldRoof_U.Length > 0)
                {
                    OldRoof_R = 1 / Convert.ToDouble(OldRoof_U[0][0]);
                    Add_OldRoof();
                }               
            }
            else { }

        }


        private void Load_RoofType_image(String Type)
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "지붕유형이미지", "이미지", "지붕유형 = '" + Type + "'");
            if(Image.Length > 0)
            {
                RoofType_pictureBox.Visible = true;
                RoofType_pictureBox.Load(Program.gPath + Image[0][0]);
                RoofType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void Color_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Color_comboBox.SelectedItem != null)
            {
                Color_Envelope = Color_comboBox.SelectedItem.ToString();
                String[][] value = Program.DB.getValue(DB.type.BaseDB_HCneed, "흡수율", "흡수율", "외장재색 = '" + Color_Envelope + "'");
                if(value.Length > 0) 
                {
                    α = Convert.ToDouble(value[0][0]);
                    α_textBox.Text = String.Format("{0:F1}", α);
                    α2_textBox.Text = String.Format("{0:F1}", α);
                }
            }


        }

        private void Uvalue_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Uvalue_comboBox.SelectedItem != null)
            {
                UMethod = Uvalue_comboBox.SelectedItem.ToString();
                Act_UcWMethod();
                Calc_Ueff();
            }
        }

        private void Act_UcWMethod()
        {
            if (UMethod == "계산")
            {
                tabControl1.SelectedTab = tabControl1.TabPages["Ucalc_tabPage"];
                U_textBox.Enabled = false;
                U_textBox.BorderStyle = BorderStyle.None;
                DiIndi_comboBox.Visible = false;
                Ucalc_dataGridView.Show();
                Ucalc_tabPage.Enabled = true;
                webView21.Visible = true;
            }
            else if (UMethod == "법규")
            {
                Rule_U();  //*오류 
                U_textBox.Enabled = false;
                U_textBox.BorderStyle = BorderStyle.None;
                DiIndi_comboBox.Visible = true;
                Ucalc_dataGridView.Hide();
                Ucalc_tabPage.Enabled = false;
                webView21.Visible = false;
            }
            else if (UMethod == "진단")
            {
                U_textBox.Enabled = true;
                U_textBox.BorderStyle = BorderStyle.FixedSingle;
                DiIndi_comboBox.Visible = true;
                Ucalc_dataGridView.Hide();
                Ucalc_tabPage.Enabled = false;
                webView21.Visible = false;
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


        private void DiIndi_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DiIndi_comboBox.SelectedItem != null)
            {
                DiIndi = DiIndi_comboBox.SelectedItem.ToString();
                Rule_U();
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

        private void DiIndi2_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DiIndi2_comboBox.SelectedItem != null)
            {
                DiIndi = DiIndi_comboBox.SelectedItem.ToString();
                Calc_RseRsi();
                Calc_Ueff();
            }
        }


        private void StructureType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StructureType_comboBox.SelectedItem != null)
            {
                StructureType = StructureType_comboBox.SelectedItem.ToString();

                //타입이 기존지붕이면서 구조 유형이 콘크리트일때만 열교 평가 문구 
                if (Type == "기존지붕")
                {
                    if (StructureType == "콘크리트조")
                    {
                        //  MessageBox.Show("내단열일 경우 열교를 입력하세요");
                        TBName_textBox.Text = "";
                    }
                    else { }
                }
                else
                {
                    //MessageBox.Show("열교를 입력하세요");
                }


                    if (check_StructureType != null)
                    {
                        if (StructureType != check_StructureType)
                        {
                            MessageBox.Show("열교 종류를 다시 선택하세요.");
                            TBName = "";
                            TBName_textBox.Text = TBName;
                            A = 0; B = 0; C = 0; PsiKai = 0;
                            Calc_dU();
                        }
                    }
                    else { }
               
                Calc_dU();
                Calc_Ueff();
            }
        }

        private void TB_button_Click(object sender, EventArgs e)
        {
            if (StructureType == null)
            {
                MessageBox.Show("구조유형부터 선택하세요.");
            }

            else if (Type == "기존지붕" && StructureType != "콘크리트조")
            {
                MessageBox.Show("기존 지붕일 경우 열교 평가는 하지 않습니다.");
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
                    //MessageBox.Show(dins.ToString());
                    MessageBox.Show("단열재가 없으므로 열교 평가는 하지 않습니다.");
                    TBName_textBox.Text = "열교없음";
                }
                else
                {
                    Roof_TB TB_form = new Roof_TB(Type, StructureType, dins);
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
          
                if (LinearPoint == "점형")
                {
                    if (TBType != "")
                    {
                        string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "지붕점형열교이미지", "이미지열교구조유형", "열교유형 = '" + TBType + "'");
                        if(Image.Length > 0)
                        {
                            pictureBox1.Visible = true;
                            pictureBox1.Load(Program.gPath + Image[0][0]);
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                    }

                    if (TBType != "" && TBName != "")
                    {
                        string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "지붕점형열교이미지", "이미지고정유형", "제품명 = '" + TBName + "' And 열교유형 = '" + TBType + "'");
                        if(Image.Length > 0)
                        {
                            pictureBox2.Visible = true;
                            pictureBox2.Load(Program.gPath + Image[0][0]);
                            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                    }

                }
                else if (LinearPoint == "선형")
                {
                    if (TBType != "")
                    {
                        string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "지붕선형열교이미지", "이미지열교구조유형", "열교유형 = '" + TBType + "'");
                        if( Image.Length > 0)
                        {
                            pictureBox1.Visible = true;
                            pictureBox1.Load(Program.gPath + Image[0][0]);
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        }                       
                    }
                    if (TBType != "" && TBName != "")
                    {
                        string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "지붕선형열교이미지", "이미지고정유형", "제품명 = '" + TBName + "' And 열교유형 = '" + TBType + "'");
                        if( Image.Length > 0)
                        {
                            pictureBox2.Visible = true;
                            pictureBox2.Load(Program.gPath + Image[0][0]);
                            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                        }                       
                    }
                }
                else { }
          
        }


        private void Load_table()
        {

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Ucalc_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Ucalc_dataGridView.Columns.Add(checkBoxColumn);


            Ucalc_dataGridView.Columns.Add("A1", "번호");
            Ucalc_dataGridView.Columns.Add("A2", "구분");
            Ucalc_dataGridView.Columns.Add("A3", "재료명");
            Ucalc_dataGridView.Columns.Add("A4", "열전도율.[W/m·K]");
            Ucalc_dataGridView.Columns.Add("A5", "두께.[mm]");
            Ucalc_dataGridView.Columns.Add("A6", "열저항.[m²·K/W]");
            Ucalc_dataGridView.Columns.Add("A7", "Color");
            Ucalc_dataGridView.Columns[0].Width = 40;
            Ucalc_dataGridView.Columns[1].Width = 40;
            Ucalc_dataGridView.Columns[2].Width = 70;
            Ucalc_dataGridView.Columns[3].Width = 130;
            Ucalc_dataGridView.Columns[4].Width = 70;
            Ucalc_dataGridView.Columns[6].Width = 70;
            Ucalc_dataGridView.Columns[7].Visible = false;

            //Ucalc_dataGridView.ColumnCount = 7;
            //Ucalc_dataGridView.Columns[1].HeaderText = "번호";
            //Ucalc_dataGridView.Columns[2].HeaderText = "구분";
            //Ucalc_dataGridView.Columns[3].HeaderText = "재료명";
            //Ucalc_dataGridView.Columns[4].HeaderText = "열전도율" + Environment.NewLine + "[W/m·K]";
            //Ucalc_dataGridView.Columns[5].HeaderText = "두께" + Environment.NewLine + "[mm]";
            //Ucalc_dataGridView.Columns[6].HeaderText = "열저항" + Environment.NewLine + "[m²·K/W]";

        }
        private void Add_OldRoof()
        {
            for (int k = 0; k < Ucalc_dataGridView.RowCount; k++)
            {
                if (Ucalc_dataGridView.Rows[k].Cells[2].Value == "기존지붕")
                {
                    Ucalc_dataGridView.Rows.Remove(Ucalc_dataGridView.Rows[k]);
                }
                else { }
            }

            int nRow = Ucalc_dataGridView.Rows.Add();
            Ucalc_dataGridView.Rows[nRow].Cells[2].Value = "기존지붕";
            Ucalc_dataGridView.Rows[nRow].Cells[3].Value = OldRoof;
            string[][] value = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "두께합계", "명칭 ='" + OldRoof + "'");
            if (value.Length > 0)
            {
                Ucalc_dataGridView.Rows[nRow].Cells[5].Value = Convert.ToDouble(value[0][0]).ToString("0");
            }
            Ucalc_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F2}", OldRoof_R);
            Load_Material_Num();
            Array.Clear(Material, 0, 10);
            Array.Clear(Material_d, 0, 10);
            Array.Clear(Material_λ, 0, 10);
            Array.Clear(Material_R, 0, 10);
            Array.Clear(Material_T, 0, 12);
            Calc_U();

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
                   // Ucalc_dataGridView.Rows[nRow].Cells[5].Style.BackColor = SystemColors.Info;

                    string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "열전도율", "색상", "재료명 = '" + form.Select[1] + "'");

                    if (Value.Length > 0)
                    {
                        Ucalc_dataGridView.Rows[nRow].Cells[7].Value = Value[0][0];
                    }
                    else
                    {
                        if (Ucalc_dataGridView.Rows[nRow].Cells[2].Value != null)
                        {
                            switch (Ucalc_dataGridView.Rows[nRow].Cells[2].Value.ToString())
                            {
                                case "기존외벽":
                                    Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "6e6e6e";
                                    break;
                                case "기존지붕":
                                    Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "6e6e6e";
                                    break;
                                case "기존바닥":
                                    Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "6e6e6e";
                                    break; ;
                                case "덧댐커튼월":
                                    Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "99CCFF";
                                    break; ;
                                case "공기층":
                                    Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "FFFFFF";
                                    break; ;
                                case "단열재":
                                    Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "FAEB78";
                                    break; ;
                                case "콘크리트":
                                    Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "b4b4b4";
                                    break; ;
                                case "미장":
                                    Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "828282";
                                    break; ;
                                case "조적":
                                    Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "D2691E";
                                    break; ;
                                case "패널":
                                    Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "497649";
                                    break; ;
                                case "목재":
                                    Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "A06641";
                                    break; ;
                                case "금속재":
                                    Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "C8D7FF";
                                    break; ;
                                case "타일":
                                    Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "FDF5DC";
                                    break; ;
                                case "지중":
                                    Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "A98B59";
                                    break; ;
                            }
                        }
                    }
                }
                Load_Material_Num();
                Calc_dins();
                Calc_U();
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
            }
        }

        private void DeleteMaterial_button_Click(object sender, EventArgs e)
        {
            if (Ucalc_dataGridView.Rows.Count > 0)
            {
                Ucalc_dataGridView.Rows.Remove(Ucalc_dataGridView.Rows[SelectRow]);
                Load_Material_Num();
                Calc_dins();
                Array.Clear(Material, 0, 10);
                Array.Clear(Material_d, 0, 10);
                Array.Clear(Material_λ, 0, 10);
                Array.Clear(Material_R, 0, 10);
                Array.Clear(Material_T, 0, 12);
                Calc_U();
            }
            else
            {
                MessageBox.Show("우선 재료를 입력해 주세요.");
            }
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
                    if (Ucalc_dataGridView.Rows[e.RowIndex].Cells[5].Value != null)
                    {
                        double d = Convert.ToDouble(Ucalc_dataGridView.Rows[e.RowIndex].Cells[5].Value);
                        if (Ucalc_dataGridView.Rows[e.RowIndex].Cells[2].Value != null && Ucalc_dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString() == "공기층")
                        {
                            Calc_Air_Layer(e.RowIndex, d);
                        }
                        double λ = Convert.ToDouble(Ucalc_dataGridView.Rows[e.RowIndex].Cells[4].Value);
                        double R = d / 1000 / λ;
                        Ucalc_dataGridView.Rows[e.RowIndex].Cells[6].Value = String.Format("{0:F2}", R);
                    }
                }
                else if (e.ColumnIndex == 2)
                {
                    if (Ucalc_dataGridView.Rows[e.RowIndex].Cells[2].Value != null && Ucalc_dataGridView.Rows[e.RowIndex].Cells[5].Value != null)
                    {
                        if (Ucalc_dataGridView.Rows[e.RowIndex].Cells[2].Value != null && Ucalc_dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString() == "공기층")
                        {
                            double d = Convert.ToDouble(Ucalc_dataGridView.Rows[e.RowIndex].Cells[5].Value);
                            Calc_Air_Layer(e.RowIndex, d);
                            double λ = Convert.ToDouble(Ucalc_dataGridView.Rows[e.RowIndex].Cells[4].Value);
                            double R = d / 1000 / λ;
                            Ucalc_dataGridView.Rows[e.RowIndex].Cells[6].Value = String.Format("{0:F2}", R);
                        }
                    }
                }
                Calc_U();
                Calc_dins();
                Calc_Ueff();
            }
        }


        private void Rule_U()
        {
            if (UMethod == "법규")
            {
                String[][] Date = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "법규시기,지역구분", "");
                String[][] Value;
                if(Date.Length > 0)
                {
                    if (Type == "기존지붕")
                    {
                        Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '지붕' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND 직접간접 =  '" + DiIndi + "'");
                    }
                    else
                    {
                        Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '지붕' And 시기 = '2018.09' AND  지역 ='" + Date[0][1] + "'  AND 직접간접 =  '" + DiIndi + "'");
                    }

                    if (Value.Length > 0)
                    {
                        Uvalue = Convert.ToDouble(Value[0][0]);
                        U_textBox.Text = string.Format("{0:F3}", Uvalue);
                        dins = (1 / Uvalue) * 0.04 * 1000;
                        Calc_dU();
                    }
                    else { }
                }               
            }
        }
        private void Calc_RseRsi()
        {
            if (ISO_KS != null && DiIndi != null)
            {
                String[][] RsiValue = Program.DB.getValue(DB.type.BaseDB_HCneed, "표면열전달저항", "저항값", "구조체 ='지붕' And 유형 = '실내' AND 기준 = '" + ISO_KS + "'");
                if(RsiValue.Length > 0)
                {
                    Rsi = Convert.ToDouble(RsiValue[0][0]);
                    Rsi_textBox.Text = string.Format("{0:F3}", Rsi);
                }

                String[][] RseValue = Program.DB.getValue(DB.type.BaseDB_HCneed, "표면열전달저항", "저항값", "구조체 ='지붕' And 유형 = '" + DiIndi + "' AND 기준 = '" + ISO_KS + "'");
                if (RseValue.Length > 0)
                {
                    Rse = Convert.ToDouble(RseValue[0][0]);
                    Rse_textBox.Text = string.Format("{0:F3}", Rse);
                }
            }
        }

        private void Calc_Air_Layer(int nRow, double d)
        {
            string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "공기층열저항", "두께,대류열저항", "구조체 = '외벽'");
            double[,] arr_Value = new double[Value.Length, 2];
            double R_up = 0, R_down = 0, d_up = 0, d_down = 0;
            double ha, hr, Ramda_air;
            if(Value.Length > 0)
            {
                for (int k = 0; k < Value.Length; k++)
                {
                    arr_Value[k, 0] = Convert.ToDouble(Value[k][0]);
                    arr_Value[k, 1] = Convert.ToDouble(Value[k][1]);
                }
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

            // Ramda_air = d / 1000 * (hr + ha);
            Ramda_air = d / 1000 * ha;

            Ucalc_dataGridView.Rows[nRow].Cells[4].Value = String.Format("{0:F2}", Ramda_air);

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
                double Q = (20 - (-5)) / Rtot;

                //Material_T[0] = (20 - Q * Rsi)
                Material_T[0] = 20;
                for (int k = 1; k < Ucalc_dataGridView.RowCount + 1; k++)
                {
                    Material_T[k] = (Material_T[k - 1] - Q * Material_R[k - 1]);
                }
                //Material_T[Ucalc_dataGridView.RowCount + 1] = Material_T[Ucalc_dataGridView.RowCount ] - Q * Rse
                Material_T[Ucalc_dataGridView.RowCount] = -5;
                Material_T[Ucalc_dataGridView.RowCount + 1] = -5;



                Material_dtot_textBox.Text = String.Format("{0:F0}", dtot);
                Material_Rtot_textBox.Text = String.Format("{0:F2}", Rtot);
                Uvalue = 1 / Rtot;
                U_textBox.Text = string.Format("{0:F3}", Uvalue);

                int i = 0;
                int count = Ucalc_dataGridView.RowCount + 1;
                string s = "{\"cate\":\"---\",\"bgcolor\":\"FFFFFF\",\"width\": 50,\"temper\":  " + Material_T[0] + "},";

                while (++i < count)
                {
                    var cate = Ucalc_dataGridView.Rows[i - 1].Cells[2].Value != null ? Ucalc_dataGridView.Rows[i - 1].Cells[2].Value.ToString() : "---";
                    var color = Ucalc_dataGridView.Rows[i - 1].Cells[7].Value != null ? Ucalc_dataGridView.Rows[i - 1].Cells[7].Value.ToString() : "6e6e6e";
                    s += "{\"cate\":\"" + cate + "\",\"bgcolor\":\"" + color + "\",\"width\": " + Material_d[i - 1] + ",\"temper\":  " + Material_T[i] + "},";
                }

                s += "{\"cate\":\"---\",\"bgcolor\":\"FFFFFF\",\"width\": 50,\"temper\":  " + Material_T[i] + "},";

                runScript("drawWall([" + s + "])");

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
                if (TBName_textBox.Text == "열교없음")
                {
                    dU = 0;
                }
                else
                {
                    PsiKai = (A * Math.Pow(dins, 2) + B * dins + C) / 1000;
                    PsiKai_textBox.Text = string.Format("{0:F3}", PsiKai);
                    dU = PsiKai * PerArea;
                    dU_textBox.Text = string.Format("{0:F3}", dU);
                    dU2_textBox.Text = string.Format("{0:F3}", dU);
                }

            }

        }

        private void Calc_Ueff()
        {
            Ueff = dU + Uvalue;
            Ueff_textBox.Text = string.Format("{0:F3}", Ueff);
            Ueff2_textBox.Text = string.Format("{0:F3}", Ueff);
        }

        private void Previous_button_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("이전 화면으로 이동하시겠습니까?", "이전 화면 이동", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                this.DialogResult = DialogResult.OK;
                this.Hide();
                Program.getMenuForm().DoLoadForm(35, OnLoadListProc);
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (RoofName == null)
            {
                MessageBox.Show("지붕 명칭을 입력하세요.");
            }
            else if (Type == null)
            {
                MessageBox.Show("지붕 리모델링 유형을 선택하세요.");
            }
            else if (Type != "기존지붕")
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
            List_ConstructionRoof f = (List_ConstructionRoof)form;
            f.load_List();
            return true;
        }

        private void Save()
        {
            if (TBName_textBox.Text == "열교없음")
            {
                TBType = null; TBName = null; LinearPoint = null;
                A = 0; B = 0; C = 0; PsiKai = 0; PerArea = 0;
                Calc_dU();
                Calc_Ueff();
            }
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            #region 법규
            String DiIndi_;
            double 법규U = 0;
            String[][] Date = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "법규시기,지역구분", "");           
            if (Date.Length > 0)
            {
                String[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '지붕' And 시기 = '2018.09' AND  지역 ='" + Date[0][1] + "'  AND 직접간접 =  '" + DiIndi + "'");
                if (Value.Length > 0)
                {
                    법규U = Convert.ToDouble(Value[0][0]);
                }
            }
            #endregion
            Program.DB.setValue(DB.type.ProjDB, "ConstructionRoof", "번호,프로젝트유형,명칭,Type,기존지붕,U적용방법,직접간접,구조유형,열교유형,열교종류,외장재색,표면열전달저항기준,선형점형," +
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
                "흡수율,열관류율,열교가산치,유효열관류율," +
                "법규열관류율",
                "'" + RoofNum_textBox.Text + "','" + 프로젝트유형[0][0] + "','" + RoofName + "','" + Type + "','" + OldRoof + "','" + UMethod + "','" + DiIndi + "','" + StructureType + "','" + TBType + "','" + TBName + "','" + Color_Envelope + "','" + ISO_KS + "','" + LinearPoint + "','" +
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
                α.ToString() + "','" + Uvalue.ToString() + "','" + dU.ToString() + "','" + Ueff.ToString() + "','" +
                법규U.ToString()
                 + "'", "번호");
            this.DialogResult = DialogResult.OK;
            this.Hide();
            Program.getMenuForm().DoLoadForm(35, OnLoadListProc);
        }

        private void reset()
        {
            RoofNum_textBox.Text = "";
            Name_textBox.Text = "";

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;

            OldRoof_comboBox.SelectedItem = null;
            Uvalue_comboBox.SelectedItem = null;
            DiIndi_comboBox.SelectedItem = null;

            StructureType_comboBox.SelectedItem = null;

            TBType_textBox.Text = null;
            TBName_textBox.Text = null;
            TBName2_textBox.Text = null;

            Color_comboBox.SelectedItem = null;

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

            α_textBox.Text = null;
            α2_textBox.Text = null;
            U_textBox.Text = null;

            dU_textBox.Text = null;
            dU2_textBox.Text = "";

            Ueff_textBox.Text = null;
            Ueff2_textBox.Text = null;

            Ucalc_dataGridView.Rows.Clear();

            RoofType_pictureBox.Visible = false;
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
            OldRoof_R = 0;
            α = 0;
            Uvalue = 0;
            dU = 0;
            Ueff = 0;
            SelectRow = 0;

            RoofNum = null;
            RoofName = null;
            Type = null;
            check_Type = null;
            OldRoof = null;
            UMethod = null;
            DiIndi = null;
            StructureType = null;
            check_StructureType = null;
            TBType = null;
            TBName = null;
            Color_Envelope = null;
            ISO_KS = null;
            LinearPoint = null;
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();

                RoofNum_textBox.Text = ID;
                RoofNum = ID;
                String[][] Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "번호,명칭,Type,기존지붕,U적용방법,직접간접,구조유형,열교유형,열교종류,외장재색,표면열전달저항기준,선형점형," +
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
                 "흡수율,열관류율,열교가산치,유효열관류율"
                      , "번호 = '" + ID + "'");
                if(Load.Length >0)
                {
                    Name_textBox.Text = Load[0][1];
                    Type = Load[0][2];
                    switch (Type)
                    {
                        case "기존지붕":
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
                    OldRoof = Load[0][3];
                    OldRoof_comboBox.SelectedIndex = OldRoof_comboBox.FindStringExact(OldRoof);

                UMethod = Load[0][4];
                    Uvalue_comboBox.SelectedItem = UMethod;

                    DiIndi = Load[0][5];
                    DiIndi_comboBox.SelectedItem = DiIndi;
                    DiIndi2_comboBox.SelectedItem = DiIndi;

                StructureType = Load[0][6];
                    check_StructureType = Load[0][6];
                    StructureType_comboBox.SelectedItem = StructureType;

                    TBType = Load[0][7];
                    TBType_textBox.Text = TBType;

                    TBName = Load[0][8]; 
                if (TBName == "")
                {
                    TBName_textBox.Text = "열교없음";
                }
                else
                {
                    TBName_textBox.Text = TBName;
                    TBName2_textBox.Text = TBName;
                }

                Color_Envelope = Load[0][9];
                    Color_comboBox.SelectedItem = Color_Envelope;

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
                    Rse_textBox.Text = string.Format("{0:F3}", Rse);
                    Rsi = Convert.ToDouble(Load[0][18]);
                    Rsi_textBox.Text = string.Format("{0:F3}", Rsi);

                    dtot = Convert.ToDouble(Load[0][19]);
                    Rtot = Convert.ToDouble(Load[0][20]);
                    Material_dtot_textBox.Text = String.Format("{0:F0}", dtot);
                    Material_Rtot_textBox.Text = String.Format("{0:F2}", Rtot);

                    dins = Convert.ToDouble(Load[0][21]);
                    check_dins = Convert.ToDouble(Load[0][21]);


                    α = Convert.ToDouble(Load[0][42]);
                    α_textBox.Text = String.Format("{0:F1}", α);
                    α2_textBox.Text = String.Format("{0:F1}", α);

                    Uvalue = Convert.ToDouble(Load[0][43]);
                    U_textBox.Text = string.Format("{0:F3}", Uvalue);

                    Ucalc_dataGridView.Rows.Clear();
                    for (int i = 0; i < 10; i++)
                    {
                        Material[i] = Load[0][(2 * i + 22)];
                        Material_d[i] = Convert.ToDouble(Load[0][(2 * i + 23)]);
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        if (Material[i] == "공기층")
                        {
                            int nRow = Ucalc_dataGridView.Rows.Add();
                            Ucalc_dataGridView.Rows[i].Cells[5].Value = Material_d[i];
                            Ucalc_dataGridView.Rows[i].Cells[2].Value = Material[i];
                            Ucalc_dataGridView.Rows[i].Cells[3].Value = Material[i];
                            Calc_Air_Layer(i, Material_d[i]);
                        }
                        else if (Material[i] != "")
                        {
                            string[][] Value;
                            string[][] OldRoof_U;
                            int nRow = Ucalc_dataGridView.Rows.Add();
                            Value = Program.DB.getValue(DB.type.ProjDB, "User_Material", "구분,열전도율", "재료명 = '" + Material[i] + "'");
                            if (Value.Length == 0)
                            {
                                Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "열전도율", "구분,열전도율,색상", "재료명 = '" + Material[i] + "'");
                            }                         
                            if (Value.Length == 0)
                            {
                                OldRoof_U = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "열관류율", "명칭 = '" + Material[i] + "'");
                                if(OldRoof_U.Length >0)
                                {
                                    OldRoof_R = 1 / Convert.ToDouble(OldRoof_U[0][0]);
                                    Ucalc_dataGridView.Rows[nRow].Cells[2].Value = "기존지붕";
                                    Ucalc_dataGridView.Rows[nRow].Cells[3].Value = OldRoof;
                                string[][] value = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "두께합계", "명칭 ='" + OldRoof + "'");
                                if (value.Length > 0)
                                {
                                    Ucalc_dataGridView.Rows[nRow].Cells[5].Value = Convert.ToDouble(value[0][0]).ToString("0");
                                }
                                Ucalc_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F2}", OldRoof_R);
                                    Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "6e6e6e";
                                }
                            }
                        else
                        {
                            Material_λ[i] = Convert.ToDouble(Value[0][1]);
                            Material_R[i] = Convert.ToDouble(Load[0][(2 * i + 23)]) / 1000 / Material_λ[i];
                            Ucalc_dataGridView.Rows[nRow].Cells[2].Value = Value[0][0];
                            Ucalc_dataGridView.Rows[nRow].Cells[3].Value = Material[i];
                            Ucalc_dataGridView.Rows[nRow].Cells[4].Value = Value[0][1];
                            Ucalc_dataGridView.Rows[nRow].Cells[5].Value = Load[0][(2 * i + 23)];
                            Ucalc_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F2}", Material_R[i]);
                            try
                            { Ucalc_dataGridView.Rows[nRow].Cells[7].Value = Value[0][2]; }
                            catch { }
                        }

                        if (Ucalc_dataGridView.Rows[nRow].Cells[2].Value != null)
                        {
                            if (Ucalc_dataGridView.Rows[nRow].Cells[7].Value == null)
                            {
                                switch (Ucalc_dataGridView.Rows[nRow].Cells[2].Value.ToString())
                                {
                                    case "기존외벽":
                                        Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "6e6e6e";
                                        break;
                                    case "기존지붕":
                                        Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "6e6e6e";
                                        break;
                                    case "기존바닥":
                                        Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "6e6e6e";
                                        break; ;
                                    case "덧댐커튼월":
                                        Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "99CCFF";
                                        break; ;
                                    case "공기층":
                                        Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "FFFFFF";
                                        break; ;
                                    case "단열재":
                                        Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "FAEB78";
                                        break; ;
                                    case "콘크리트":
                                        Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "b4b4b4";
                                        break; ;
                                    case "미장":
                                        Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "828282";
                                        break; ;
                                    case "조적":
                                        Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "D2691E";
                                        break; ;
                                    case "패널":
                                        Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "497649";
                                        break; ;
                                    case "목재":
                                        Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "A06641";
                                        break; ;
                                    case "금속재":
                                        Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "C8D7FF";
                                        break; ;
                                    case "타일":
                                        Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "FDF5DC";
                                        break; ;
                                    case "지중":
                                        Ucalc_dataGridView.Rows[nRow].Cells[7].Value = "A98B59";
                                        break; ;
                                }
                            }
                        }
                    }
                        else { }
                    }

                    Load_Material_Num();

                dU = Convert.ToDouble(Load[0][44]);
                dU_textBox.Text = string.Format("{0:F3}", dU);

                if (PerArea != 0)
                {
                    PerArea_textBox.Text = string.Format("{0:F3}", Convert.ToDouble(Load[0][16]));
                }
                if (PerArea_textBox.Text != "" && PerArea != 0)
                {
                    PsiKai_textBox.Text = string.Format("{0:F3}", Convert.ToDouble(Load[0][15]));
                    dU2_textBox.Text = string.Format("{0:F3}", Convert.ToDouble(Load[0][44]));
                }

                Ueff = Convert.ToDouble(Load[0][45]);
                Ueff_textBox.Text = string.Format("{0:F3}", Ueff);
                Ueff2_textBox.Text = string.Format("{0:F3}", Ueff);

                string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '지붕'");
                    if(Image.Length > 0)
                    {
                        Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                        Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                    }
                    Load_RoofType_image(Type);
                    Load_TB_Image();

                }
               
           
        }

        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            RoofNum_textBox.Text = ID;
            RoofNum = ID;
        }


    }
}
