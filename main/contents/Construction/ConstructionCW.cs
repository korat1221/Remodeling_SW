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
using main.subcontents.ConstructionCW;

namespace main.contents
{
    public partial class ConstructionCW : Form
    {
        private String CWNum;
        String CWName, Type, OldCW, UcwMethod, DiIndi, FrameType, SingleDoubleType, FrameMaterial, FrameName, FixGlassName, OpenGlassName, PanelGlassName, SpacerName, InstallType, InstallName, LE_CL_V, PanelColor;
        String check_FrameType, check_LE_CL_V, check_InstallType;
        String[][] Size;
        double Ug_Fix, Ug_Open, Ug_panel, g, τD65_SNA, αp, Psi_g_fix, Psi_g_open, Ucw;
        List<double> Sub_Ucw = new List<double>(); List<double> Sub_Ucw_inst = new List<double>(); List<double> Sub_dUinst = new List<double>();// dUinst는 열교가산치, Uw_inst는 유효열관류율(창호열관류율+열교가산치)
        double Uf_mt, Uf_open, Psi_p, df_mt, df_open, df_btw;
        double Psi_InstallTop, Psi_InstallSide, Psi_InstallButtom;
        List<double> Sub_Area = new List<double>(); List<double> Sub_Width = new List<double>(); List<double> Sub_Height = new List<double>(); List<double> Sub_Ag_fix = new List<double>(); List<double> Sub_Ag_open = new List<double>(); List<double> Sub_Af_open = new List<double>(); List<double> Sub_Af_fix = new List<double>(); List<double> Sub_Af_btw = new List<double>(); List<double> Sub_Lg_fix = new List<double>(); List<double> Sub_Lg_open = new List<double>();
        String[][] Old; String[][] f_shgc; String[][] f_τ;

        public ConstructionCW()
        {
            InitializeComponent();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '커튼월창'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            //직접간접 콤보박스
            Program.UTIL.FillComboBox(DiIndi_comboBox, "커튼월", "실외조건", "1");
            //프레임종류 콤보박스
            Program.UTIL.FillComboBox(Frame_comboBox, "커튼월", "프레임재질", "1");
            //설치위치 콤보박스
            // Program.UTIL.FillComboBox(Install_comboBox, "커튼월", "설치위치", "1");

            Image = Program.DB.getValue(DB.type.BaseDB, "커튼월프레임이미지", "이미지", "유형 = '디포트'");
            CWFrame_pictureBox.Load(Program.gPath + Image[0][0]);
            CWFrame_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            Panel_checkBox.Checked = true;
            Panel_checkBox.Checked = false;

        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void Name_textBox_TextChanged(object sender, EventArgs e)
        {
            CWName = Name_textBox.Text;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Type = "기존 커튼월창";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_WindowType_image(Type);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Type = "신규 커튼월창";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_WindowType_image(Type);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Type = "철거 후 신규";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_WindowType_image(Type);
        }


        private void Changed_Type(String Type)
        {
            Ucw_comboBox.Items.Clear();

            switch (Type)
            {
                case "기존 커튼월창":
                    Ucw_comboBox.Items.Add("계산");
                    Ucw_comboBox.Items.Add("법규");
                    Ucw_comboBox.Items.Add("진단");
                    AdditionalCW_textBox.Visible = false;
                    OldCW_comboBox.Visible = false;
                    Load_AdditionalWindow(Type);
                    break;

                case "신규 커튼월창":
                    Ucw_comboBox.Items.Add("계산");
                    Ucw_comboBox.Items.Add("법규");
                    AdditionalCW_textBox.Visible = false;
                    OldCW_comboBox.Visible = false;
                    Load_AdditionalWindow(Type);
                    break;

                case "철거 후 신규":
                    Ucw_comboBox.Items.Add("계산");
                    Ucw_comboBox.Items.Add("법규");
                    AdditionalCW_textBox.Visible = true;
                    OldCW_comboBox.Visible = true;
                    Load_AdditionalWindow(Type);
                    break;
            }

            Ucw_comboBox.SelectedIndex = 0;
        }

        //기존 커튼월창 리스트 불러오기 
        private void Load_AdditionalWindow(String Type)
        {
            string def_value;
            String[][] Table;

            if (Type == "철거 후 신규")
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

            OldCW_comboBox.DataSource = sources.DefaultView;
            OldCW_comboBox.DisplayMember = "Text";
            for (i = 0; i < OldCW_comboBox.Items.Count; i++)
            {
                var arr = ((DataRowView)OldCW_comboBox.Items[i]).Row.ItemArray;
                if (arr.Length > 1 && arr[1].ToString() == def_value)
                {
                    OldCW_comboBox.SelectedIndex = i;
                    break;
                }
            }

        }

        private void OldWindow_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OldCW = OldCW_comboBox.SelectedItem.ToString();
        }



        private void Load_WindowType_image(String Type)
        {

            string[][] Image = Program.DB.getValue(DB.type.BaseDB, "커튼월구조유형이미지", "이미지", "구조유형 = '" + Type + "'");

            CWType_pictureBox.Load(Program.gPath + Image[0][0]);
            CWType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

        }

        private void Ucw_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UcwMethod = Ucw_comboBox.SelectedItem.ToString();

            if (UcwMethod == "계산")
            {
                Frame_label.Visible = true;
                Frame_comboBox.Visible = true;
                FrameName_textBox.Visible = true;
                FrameDB_button.Visible = true;

                Spacer_label.Visible = true;
                Spacer_button.Visible = true;
                SpacerName_textBox.Visible = true;

                UCW_g_label.Visible = true;
                Ucw_g_textBox.Visible = true;
                Ug_unit_label.Visible = true;

                Uw2_label.Visible = false;
                Uw2_unit_label.Visible = false;
                Ucw2_textBox.Visible = false;
                Ucw2_textBox.Enabled = false;
                Ucw2_textBox.BorderStyle = BorderStyle.None;
            }
            else if (UcwMethod == "법규")
            {
                Frame_label.Visible = false;
                Frame_comboBox.Visible = false;
                FrameName_textBox.Visible = false;
                FrameDB_button.Visible = false;

                Spacer_label.Visible = false;
                Spacer_button.Visible = false;
                SpacerName_textBox.Visible = false;

                UCW_g_label.Visible = false;
                Ucw_g_textBox.Visible = false;
                Ug_unit_label.Visible = false;

                Uw2_label.Visible = true;
                Uw2_unit_label.Visible = true;
                Ucw2_textBox.Visible = true;
                Ucw2_textBox.Enabled = false;
                Ucw2_textBox.BorderStyle = BorderStyle.None;
            }
            else if (UcwMethod == "진단")
            {
                Frame_label.Visible = false;
                Frame_comboBox.Visible = false;
                FrameName_textBox.Visible = false;
                FrameDB_button.Visible = false;

                Spacer_label.Visible = false;
                Spacer_button.Visible = false;
                SpacerName_textBox.Visible = false;

                UCW_g_label.Visible = false;
                Ucw_g_textBox.Visible = false;
                Ug_unit_label.Visible = false;

                Uw2_label.Visible = true;
                Uw2_unit_label.Visible = true;
                Ucw2_textBox.Visible = true;
                Ucw2_textBox.Text = string.Empty;
                Ucw2_textBox.Enabled = true;
                Ucw2_textBox.BorderStyle = BorderStyle.FixedSingle;
            }

            Rule_Uw();

        }

        private void Ucw2_textBox_TextChanged(object sender, EventArgs e)
        {
            if (UcwMethod == "진단" && Ucw2_textBox.Text != string.Empty)
            {
                Ucw = Convert.ToDouble(Ucw2_textBox.Text);
            }
        }
        private void DiIndi_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DiIndi = DiIndi_comboBox.SelectedItem.ToString();
            Rule_Uw();
        }

        private void ImportSize_button_Click(object sender, EventArgs e)
        {
            Window_ImportSize window_importsize_form = new Window_ImportSize(CWNum, CWName);

            DialogResult result = window_importsize_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                ImportSize();
            }
        }
        private void ImportSize()
        {
            Sub_Area.Clear();
            Sub_Width.Clear();
            Sub_Height.Clear();
            Sub_Ag_fix.Clear();
            Sub_Ag_open.Clear();
            Sub_Af_open.Clear();
            Sub_Af_fix.Clear();
            Sub_Af_btw.Clear();
            Sub_Lg_fix.Clear();
            Sub_Lg_open.Clear();
            Sub_Ucw.Clear();
            Sub_dUinst.Clear();
            Sub_Ucw_inst.Clear();

            // Program.DB.deleteValue(DB.type.ProjDB, "SubWindow", "상위창호번호 = '" + WinNum + "'");
            Size = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "번호,명칭,상위창호번호,창호면적,창호너비,창호높이,고정유리면적,개폐유리면적,개폐프레임면적,고정프레임면적,중간프레임면적,고정유리둘레길이,개폐유리둘레길이", "상위창호번호 = '" + CWNum + "'");
            if (Size.Length > -1)
            {
                for (int i = 0; i < Size.Length; i++)
                {

                    Sub_Area.Add(Convert.ToDouble(Size[i][3])); //나중에 row가 여러개로 되도록 고쳐야함 
                    Sub_Width.Add(Convert.ToDouble(Size[i][4]));
                    Sub_Height.Add(Convert.ToDouble(Size[i][5]));
                    Sub_Ag_fix.Add(Convert.ToDouble(Size[i][6]));
                    Sub_Ag_open.Add(Convert.ToDouble(Size[i][7]));
                    Sub_Af_open.Add(Convert.ToDouble(Size[i][8]));
                    Sub_Af_fix.Add(Convert.ToDouble(Size[i][9]));
                    Sub_Af_btw.Add(Convert.ToDouble(Size[i][10]));
                    Sub_Lg_fix.Add(Convert.ToDouble(Size[i][11]));
                    Sub_Lg_open.Add(Convert.ToDouble(Size[i][12]));

                    Sub_Ucw.Add(Calc_Uw(Convert.ToDouble(Sub_Area[i]), Convert.ToDouble(Sub_Width[i]), Convert.ToDouble(Sub_Height[i]), Convert.ToDouble(Sub_Ag_fix[i]), Convert.ToDouble(Sub_Ag_open[i]), Convert.ToDouble(Sub_Af_open[i]), Convert.ToDouble(Sub_Af_fix[i]), Convert.ToDouble(Sub_Af_btw[i]), Convert.ToDouble(Sub_Lg_fix[i]), Convert.ToDouble(Sub_Lg_open[i])));
                    if (UcwMethod == "계산")
                    {
                        Sub_dUinst.Add(Calc_dUinst(Convert.ToDouble(Sub_Ucw[i]), Convert.ToDouble(Sub_Area[i]), Convert.ToDouble(Sub_Width[i]), Convert.ToDouble(Sub_Height[i])));
                        Sub_Ucw_inst.Add(Convert.ToDouble(Sub_Ucw[i]) + Convert.ToDouble(Sub_dUinst[i]));
                    }
                    else
                    {
                        Sub_dUinst.Add(Calc_dUinst(Ucw, Convert.ToDouble(Sub_Area[i]), Convert.ToDouble(Sub_Width[i]), Convert.ToDouble(Sub_Height[i])));
                        Sub_Ucw_inst.Add(Ucw + Convert.ToDouble(Sub_dUinst[i]));
                    }
                    Program.DB.executeSQL(DB.type.ProjDB, "UPDATE SubWindow SET 창호열관류율  ='" + Sub_Ucw[i] + "', 설치열교가산치 = '" + Sub_dUinst[i] + "', 창호유효열관류율 = '" + Sub_Ucw_inst[i] + "' WHERE  번호 = '" + Size[i][0] + "'");
                }
                Size_textBox.Text = Size.Length.ToString() + "개 치수 적용";
            }


        }

        private void Frame_comboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            FrameType = Frame_comboBox.SelectedItem.ToString();

            //프레임 유형 다시 선택했을 경우 
            try
            {
                if (check_FrameType != null)
                {
                    if (FrameType != check_FrameType)
                    {
                        MessageBox.Show("프레임, 간봉, 설치열교를 다시 선택하세요.");
                        FrameName = "";
                        FrameMaterial = "";
                        FrameName_textBox.Text = "";
                        SpacerName = "";
                        SpacerName_textBox.Text = "";
                        InstallName = "";
                        Install_textBox.Text = "";
                        Uf_mt_textBox.Text = "";
                        Uf_open_textBox.Text = "";
                        Psi_p_textBox.Text = "";
                        df_mt_textBox.Text = "";
                        df_open_textBox.Text = "";
                    }
                }

            }
            catch { }

        }

        private void FrameDB_button_Click(object sender, EventArgs e)
        {
            if (FrameType == null)
            {
                MessageBox.Show("프레임 유형부터 선택하세요.");
            }
            else
            {
                CW_FrameDB cw_frameDB_form = new CW_FrameDB(FrameType);

                DialogResult result = cw_frameDB_form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    FrameName = cw_frameDB_form.Select_CWFrame[1];
                    FrameName_textBox.Text = FrameName;
                    tabControl1.SelectedTab = tabControl1.TabPages["Frame_tabPage"];
                    check_FrameType = cw_frameDB_form.Select_CWFrame[3];
                    Uf_mt = Convert.ToDouble(cw_frameDB_form.Select_CWFrame[5]);
                    Uf_open = Convert.ToDouble(cw_frameDB_form.Select_CWFrame[6]);
                    Psi_p = Convert.ToDouble(cw_frameDB_form.Select_CWFrame[7]);
                    df_mt = Convert.ToDouble(cw_frameDB_form.Select_CWFrame[8]);
                    df_open = Convert.ToDouble(cw_frameDB_form.Select_CWFrame[9]);
                    Uf_mt_textBox.Text = String.Format("{0:F2}", Uf_mt);
                    Uf_open_textBox.Text = String.Format("{0:F2}", Uf_open);
                    Psi_p_textBox.Text = String.Format("{0:F2}", Psi_p);
                    df_mt_textBox.Text = String.Format("{0:F2}", df_mt);
                    df_open_textBox.Text = String.Format("{0:F2}", df_open);

                }
            }

        }

        private void FixGlassDB_button_Click(object sender, EventArgs e)
        {
            GlassDB cw_glassDB_form = new GlassDB();
            DialogResult result = cw_glassDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                FixGlassName = cw_glassDB_form.Select_WindowGlass[1];
                FixGlassName_textBox.Text = FixGlassName;
                LE_CL_V = cw_glassDB_form.Select_WindowGlass[5];
                Ug_Fix = Convert.ToDouble(cw_glassDB_form.Select_WindowGlass[6]);
                g = Convert.ToDouble(cw_glassDB_form.Select_WindowGlass[7]);
                g_textBox.Text = String.Format("{0:F3}", g);
                τD65_SNA = Convert.ToDouble(cw_glassDB_form.Select_WindowGlass[8]);
                τD65_SNA_textBox.Text = String.Format("{0:F3}", τD65_SNA);
            }

            //유리를 다시 선택했을 경우 
            try
            {
                if (check_LE_CL_V != null)
                {
                    if (LE_CL_V != check_LE_CL_V)
                    {
                        MessageBox.Show("간봉을 다시 선택하세요.");
                        SpacerName = "";
                        SpacerName_textBox.Text = "";
                    }
                }

            }
            catch { }

        }

        private void OpenGlassDB_button_Click(object sender, EventArgs e)
        {
            GlassDB cw_glassDB_form = new GlassDB();
            DialogResult result = cw_glassDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                OpenGlassName = cw_glassDB_form.Select_WindowGlass[1];
                OpenGlassName_textBox.Text = OpenGlassName;
                Ug_Open = Convert.ToDouble(cw_glassDB_form.Select_WindowGlass[6]);
            }

        }

        private void Panel_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            PanelCheck();
        }
        private void PanelCheck()
        {
            if (Panel_checkBox.Checked)
            {
                Panel_label.Visible = true;
                Panel_textBox.Visible = true;
                PanelDB_button.Visible = true;

                dPanel_label.Visible = true;
                dPanel_textBox.Visible = true;
                dPanel_label2.Visible = true;

                PanelGlass_label.Visible = true;
                PanelGlass_textBox.Visible = true;
                PanelGlassDB_button.Visible = true;

                PanelColor_label.Visible = true;
                PanelColor_comboBox.Visible = true;
                Program.UTIL.FillComboBox(PanelColor_comboBox, "커튼월", "색깔", "1");

                UCW_p_label.Visible = true;
                UCW_p_textBox.Visible = true;
                UCW_p_label2.Visible = true;

                αp_label.Visible = true;
                αp_textBox.Visible = true;
                αp_label.Visible = true;


            }
            else
            {
                Panel_label.Visible = false;
                Panel_textBox.Visible = false;
                PanelDB_button.Visible = false;

                dPanel_label.Visible = false;
                dPanel_textBox.Visible = false;
                dPanel_label2.Visible = false;

                PanelGlass_label.Visible = false;
                PanelGlass_textBox.Visible = false;
                PanelGlassDB_button.Visible = false;

                PanelColor_label.Visible = false;
                PanelColor_comboBox.Visible = false;

                UCW_p_label.Visible = false;
                UCW_p_textBox.Visible = false;
                UCW_p_label2.Visible = false;

                αp_label.Visible = false;
                αp_textBox.Visible = false;
                αp_label.Visible = false;

            }
        }

        private void PanelGlassDB_button_Click(object sender, EventArgs e)
        {
            GlassDB cw_glassDB_form = new GlassDB();
            DialogResult result = cw_glassDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                PanelGlassName = cw_glassDB_form.Select_WindowGlass[1];
                PanelGlass_textBox.Text = PanelGlassName;
                Ug_panel = Convert.ToDouble(cw_glassDB_form.Select_WindowGlass[6]);
            }

        }

        private void PanelColor_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PanelColor = PanelColor_comboBox.SelectedItem.ToString();
            String[][] value = Program.DB.getValue(DB.type.BaseDB, "흡수율", "흡수율", "외장재색 = '" + PanelColor + "'");
            αp = Convert.ToDouble(value[0][0]);
            αp_textBox.Text = String.Format("{0:F1}", αp);
        }
        private void Spacer_button_Click(object sender, EventArgs e)
        {
            if (FrameType == null)
            {
                MessageBox.Show("프레임부터 선택하세요.");
            }
            else if (LE_CL_V == null)
            {
                MessageBox.Show("유리부터 선택하세요.");
            }
            else
            {
                CW_SpacerDB cw_spacerDB_form = new CW_SpacerDB(FrameType, LE_CL_V);
                DialogResult result = cw_spacerDB_form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    check_FrameType = cw_spacerDB_form.Select_WindowSpacer[4];
                    check_LE_CL_V = cw_spacerDB_form.Select_WindowSpacer[9];
                    SpacerName = cw_spacerDB_form.Select_WindowSpacer[3];
                    SpacerName_textBox.Text = SpacerName;
                    if (LE_CL_V.Contains("LE"))
                    {
                        Psi_g_fix = Convert.ToDouble(cw_spacerDB_form.Select_WindowSpacer[7]);
                        Psi_g_open = Convert.ToDouble(cw_spacerDB_form.Select_WindowSpacer[8]);
                    }
                    else
                    {
                        Psi_g_fix = Convert.ToDouble(cw_spacerDB_form.Select_WindowSpacer[5]);
                        Psi_g_open = Convert.ToDouble(cw_spacerDB_form.Select_WindowSpacer[6]);
                    }
                }
            }
        }

        private void Install_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            InstallType = Install_comboBox.SelectedItem.ToString();

            //설치 유형 다시 선택했을 경우 
            try
            {
                if (check_InstallType != null)
                {
                    if (InstallType != check_InstallType)
                    {
                        MessageBox.Show("설치 위치를 다시 선택하세요.");
                        InstallName = "";
                        Install_textBox.Text = "";
                    }
                }
            }
            catch { }

        }

        private void Install_button_Click(object sender, EventArgs e)
        {
            if (UcwMethod == "계산")
            {
                if (SingleDoubleType == null || FrameMaterial == null)
                {
                    MessageBox.Show("프레임종류부터 선택하세요.");
                }
                else if (InstallType == null)
                {
                    MessageBox.Show(" 설치구조유형부터 선택하세요.");
                }
                else
                {
                    Window_InstallDB window_installDB_form = new Window_InstallDB(InstallType, SingleDoubleType, FrameMaterial);
                    DialogResult result = window_installDB_form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        InstallName = window_installDB_form.Select_WindowInstall[5];
                        Install_textBox.Text = InstallName;
                        check_InstallType = window_installDB_form.Select_WindowInstall[2];
                        tabControl1.SelectedTab = tabControl1.TabPages["Install_tabPage"];
                        Psi_InstallTop = Convert.ToDouble(window_installDB_form.Select_WindowInstall[6]);
                        Psi_InstallSide = Convert.ToDouble(window_installDB_form.Select_WindowInstall[7]);
                        Psi_InstallButtom = Convert.ToDouble(window_installDB_form.Select_WindowInstall[8]);

                        Psi_InstallTop_textBox.Text = String.Format("{0:F3}", Psi_InstallTop);
                        Psi_InstallSide_textBox.Text = String.Format("{0:F3}", Psi_InstallSide);
                        Psi_InstallButtom_textBox.Text = String.Format("{0:F3}", Psi_InstallButtom);


                        string[][] Image = Program.DB.getValue(DB.type.BaseDB, "창호설치열교이미지", "이미지열교유형", "구분1 = '" + InstallType + "' AND 구분2 = '" + FrameMaterial + "' AND 구분3 = '" + SingleDoubleType + "' AND 구분4 = '" + InstallName + "'");

                        WindowInstall_pictureBox.Load(Program.gPath + Image[0][0]);
                        WindowInstall_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
            }
            else //법규이거나 진단일 경우 
            {
                if (InstallType == null)
                {
                    MessageBox.Show(" 설치구조유형부터 선택하세요.");
                }
                else
                {
                    Window_InstallDB window_installDB_form = new Window_InstallDB(InstallType);
                    DialogResult result = window_installDB_form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        InstallName = window_installDB_form.Select_WindowInstall[5];
                        Install_textBox.Text = InstallName;
                        check_InstallType = window_installDB_form.Select_WindowInstall[2];
                        FrameMaterial = window_installDB_form.Select_WindowInstall[3];
                        SingleDoubleType = window_installDB_form.Select_WindowInstall[4];
                        tabControl1.SelectedTab = tabControl1.TabPages["Install_tabPage"];
                        Psi_InstallTop = Convert.ToDouble(window_installDB_form.Select_WindowInstall[6]);
                        Psi_InstallSide = Convert.ToDouble(window_installDB_form.Select_WindowInstall[7]);
                        Psi_InstallButtom = Convert.ToDouble(window_installDB_form.Select_WindowInstall[8]);

                        Psi_InstallTop_textBox.Text = String.Format("{0:F3}", Psi_InstallTop);
                        Psi_InstallSide_textBox.Text = String.Format("{0:F3}", Psi_InstallSide);
                        Psi_InstallButtom_textBox.Text = String.Format("{0:F3}", Psi_InstallButtom);


                        string[][] Image = Program.DB.getValue(DB.type.BaseDB, "창호설치열교이미지", "이미지열교유형", "구분1 = '" + InstallType + "' AND 구분2 = '" + FrameMaterial + "' AND 구분3 = '" + SingleDoubleType + "' AND 구분4 = '" + InstallName + "'");

                        WindowInstall_pictureBox.Load(Program.gPath + Image[0][0]);
                        WindowInstall_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }

            }

        }
        public void Rule_Uw()
        {
            if (UcwMethod == "법규")
            {
                String[][] Uvalue = Program.DB.getValue(DB.type.BaseDB, "법규열관류율", "열관류율", "구조체 = '창호' And 시기 = '2018.09' AND  지역 = '중부1' AND 직접간접 =  '" + DiIndi + "'");
                Ucw = Convert.ToDouble(Uvalue[0][0]);
                Ucw2_textBox.Text = String.Format("{0:F3}", Ucw);
            }
        }

        public double Calc_Uw(double Area, double Width, double Height, double Ag_fix, double Ag_open, double Af_open, double Af_fix, double Af_btw, double Lg_fix, double Lg_open)
        {
            double Uwcalc;
            if (UcwMethod == "계산" && Ug_Fix != 0 && Uf_open != 0 && Psi_g_fix != 0 && Area != 0)
            {
                Uwcalc = (Ug_Fix * (Ag_fix + Ag_open) + (Uf_mt * Af_open) + (Uf_open * Af_fix) + (Psi_p * Af_btw) + (Psi_g_fix * Lg_fix) + (Psi_g_open * Lg_open)) / Area;
            }
            else { Uwcalc = 0; }
            return Uwcalc;
        }

        public double Calc_dUinst(double Uw, double Area, double Width, double Height)
        {
            double dUinstcalc;
            if (Uw != 0 && Area != 0)
            {
                dUinstcalc = ((Psi_InstallTop * Width) + (Psi_InstallButtom * Width) + (Psi_InstallSide * Height * 2)) / Area;
            }
            else { dUinstcalc = 0; }
            return dUinstcalc;
        }

        private void Previous_button_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("이전 화면으로 이동하시겠습니까?", "이전 화면 이동", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                this.DialogResult = DialogResult.OK;
                this.Hide();
                Program.getMenuForm().DoLoadForm(29, OnLoadListProc);
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (CWName == null)
            {
                MessageBox.Show("창호 명칭을 입력하세요.");
            }
            else if (Type == null)
            {
                MessageBox.Show("창호 리모델링 유형을 선택하세요.");
            }
            else if (FixGlassName == null)
            {
                MessageBox.Show("유리를 선택하세요.");
            }
            else if (InstallName == null)
            {
                MessageBox.Show("설치열교를 선택하세요.");
            }
            else if (UcwMethod == "계산")
            {
                if (FrameName == null)
                {
                    MessageBox.Show("프레임을 선택하세요.");
                }
                else if (SpacerName == null)
                {
                    MessageBox.Show("간봉을 선택하세요.");
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
            List_ConstructionWindow f = (List_ConstructionWindow)form;

            f.load_List();

            return true;
        }

        private void Save()
        {
            this.DialogResult = DialogResult.OK;
            this.Hide();
            Program.getMenuForm().DoLoadForm(30, OnLoadListProc);
        }


        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
        }
        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            CWNum_textBox.Text = ID;
            CWNum = ID;
        }

        public void CopyForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            CWNum_textBox.Text = ID;
            CWNum = ID;

            if (Name_textBox.Text != "")
            {
                CWName = Name_textBox.Text + "_복사";
                Name_textBox.Text = CWName;
                Save();
            }

        }

    }
}
