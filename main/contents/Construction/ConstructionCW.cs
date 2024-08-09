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
        String CWName, Type, OldCW, UcwMethod, DiIndi, FrameType, check_FrameType, FrameName, FixGlassName, OpenGlassName, SpacerName, InstallType, check_InstallType, InstallName, LE_CL_V, check_LE_CL_V, SizeName;
        String PanelName, PanelGlassName, LE_CL_V_Panel, PanelColor;
        String DoorFrame, check_DoorFrame, DoorGlassName, DoorSpacer, LE_CL_V_Door, check_LE_CL_V_Door;
        String[][] Size;
        double Ug_Fix, Ug_Open, g, τ, Psi_g_fix, Psi_g_open, Uf_mt, Uf_open, df_mt, df_open;
        double Up, Ug_panel, Conductivity_p, αp, Psi_p, dPanel;
        double Ug_Door, gd, τd, Psi_g_Door, df_door, Uf_door;
        double Psi_InstallTop, Psi_InstallSide, Psi_InstallButtom;
        double Ucw, Ucw_g, Ucw_p, Ucw_d;
        double Ucw_inst, dUinst;// dUinst는 열교가산치, Ucw_inst는 유효열관류율(커튼월창열관류율+열교가산치)
        double Ucw_g_inst, Ucw_p_inst, Ucw_d_inst;
        double Area, Width, Height, Ag_fix, Ag_open, Lg_fix, Lg_open, Ap, Lp, Af_mt, Af_open, Af_d, Ag_d, Lg_d;
        String[][] Old; String[][] f_shgc; String[][] f_τ;
        Boolean Panel_check, Door_check;
        double Ff_g, Ff_d;

        public ConstructionCW()
        {
            InitializeComponent();
            Program.DB.initTable(DB.type.CalcDB, "Import_CWSize"); //불러온 사이즈 정보 저장할 table 생성
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '커튼월창'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }


            //직접간접 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, DiIndi_comboBox, "커튼월", "실외조건", "1");
            //프레임종류 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, Frame_comboBox, "커튼월", "프레임재질", "1");
            //설치위치 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, Install_comboBox, "커튼월", "구조", "1");

            Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "커튼월프레임이미지", "이미지", "유형 = '디포트'");
            if (Image.Length > 0)
            {
                CWFrame_pictureBox.Load(Program.gPath + Image[0][0]);
                CWFrame_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "커튼월프레임이미지", "이미지", "유형 = '유리디포트'");
            if (Image.Length > 0)
            {
                CWGlass_pictureBox.Load(Program.gPath + Image[0][0]);
                CWGlass_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            string[][] value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호", "");
            if (value.Length > 0)
            {
                if (value[0][0] == "1")
                {
                    radioButton1.Checked = true;
                    radioButton2.Enabled = false;
                    radioButton3.Enabled = false;
                }
                else if (value[0][0] == "4")
                {
                    radioButton1.Enabled = false;
                    radioButton2.Checked = true;
                    radioButton3.Enabled = false;
                }
            }
            Panel_checkBox.Checked = true;
            Panel_checkBox.Checked = false;

            Door_checkBox.Checked = true;
            Door_checkBox.Checked = false;

        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }
        private void Name_textBox_TextChanged(object sender, EventArgs e)
        {
            if (Name_textBox.Text != null)
            {
                CWName = Name_textBox.Text.ToString();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Type = "기존 커튼월창";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_CWType_image(Type);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Type = "신규 커튼월창";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_CWType_image(Type);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Type = "철거 후 신규";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_CWType_image(Type);
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
                    Load_OldCW(Type);
                    break;

                case "신규 커튼월창":
                    Ucw_comboBox.Items.Add("계산");
                    Ucw_comboBox.Items.Add("법규");
                    AdditionalCW_textBox.Visible = false;
                    OldCW_comboBox.Visible = false;
                    Load_OldCW(Type);
                    break;

                case "철거 후 신규":
                    Ucw_comboBox.Items.Add("계산");
                    Ucw_comboBox.Items.Add("법규");
                    AdditionalCW_textBox.Visible = true;
                    OldCW_comboBox.Visible = true;
                    Load_OldCW(Type);
                    break;
            }

            Ucw_comboBox.SelectedIndex = 0;
        }

        //기존 커튼월창 리스트 불러오기 
        private void Load_OldCW(String Type)
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
            if (Table.Length > 0)
            {
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

        }

        private void OldCW_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView? item = OldCW_comboBox.SelectedItem as DataRowView;
            if (item != null)
            {
                OldCW = item.Row.ItemArray[0].ToString();
                Calc_Ucw();
                Calc_dUinst();
            }
        }

        private void Load_CWType_image(String Type)
        {

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "커튼월구조유형이미지", "이미지", "구조유형 = '" + Type + "'");
            if (Image.Length > 0)
            {
                CWType_pictureBox.Visible = true;
                CWType_pictureBox.Load(Program.gPath + Image[0][0]);
                CWType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void Ucw_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Ucw_comboBox.SelectedItem != null)
            {
                UcwMethod = Ucw_comboBox.SelectedItem.ToString();
                Act_UcWMethod();
                Rule_Ucw_g();
            }
        }

        private void Act_UcWMethod()
        {
            if (UcwMethod == "계산")
            {
                Calc_Ucw();
                Frame_label.Visible = true;
                Frame_comboBox.Visible = true;
                FrameName_textBox.Visible = true;
                FrameDB_button.Visible = true;

                Spacer_label.Visible = true;
                Spacer_button.Visible = true;
                SpacerName_textBox.Visible = true;

                PanelCheck();
                DoorCheck();

                UCW_g_textBox.Enabled = false;
                UCW_g_textBox.BorderStyle = BorderStyle.None;
                UCW_p_textBox.Enabled = false;
                UCW_p_textBox.BorderStyle = BorderStyle.None;
                UCW_d_textBox.Enabled = false;
                UCW_d_textBox.BorderStyle = BorderStyle.None;
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
                UCW_g_textBox.Enabled = false;
                UCW_g_textBox.BorderStyle = BorderStyle.None;

                if (Panel_checkBox.Checked)
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

                    UCW_p_textBox.Enabled = false;
                    UCW_p_textBox.BorderStyle = BorderStyle.None;
                    Rule_Ucw_p();
                }
                else
                {
                    PanelCheck();
                }

                if (Door_checkBox.Checked)
                {
                    DoorFrame_label.Visible = false;
                    DoorFrame_textBox.Visible = false;
                    DoorFrameDB_button.Visible = false;

                    DoorSpacer_label.Visible = false;
                    DoorSpacer_textBox.Visible = false;
                    DoorSpacerDB_button.Visible = false;

                    UCW_d_textBox.Enabled = false;
                    UCW_d_textBox.BorderStyle = BorderStyle.None;
                    Rule_Ucw_d();

                }
                else
                {
                    DoorCheck();
                }
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

                UCW_g_textBox.Enabled = true;
                UCW_g_textBox.BorderStyle = BorderStyle.FixedSingle;

                if (Panel_checkBox.Checked)
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

                    UCW_p_textBox.Enabled = true;
                    UCW_p_textBox.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    PanelCheck();
                }

                if (Door_checkBox.Checked)
                {
                    DoorFrame_label.Visible = false;
                    DoorFrame_textBox.Visible = false;
                    DoorFrameDB_button.Visible = false;

                    DoorSpacer_label.Visible = false;
                    DoorSpacer_textBox.Visible = false;
                    DoorSpacerDB_button.Visible = false;


                    UCW_d_textBox.Enabled = true;
                    UCW_d_textBox.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    DoorCheck();
                }

            }
            Calc_dUinst();
        }

        private void UCW_g_textBox_TextChanged(object sender, EventArgs e)
        {
            if (UcwMethod == "진단" && UCW_g_textBox.Text != string.Empty)
            {
                Ucw_g = Convert.ToDouble(UCW_g_textBox.Text);
            }
            Calc_Ucw();
            Calc_dUinst();
        }

        private void UCW_p_textBox_TextChanged(object sender, EventArgs e)
        {
            if (UcwMethod == "진단" && Panel_checkBox.Checked && UCW_p_textBox.Text != string.Empty)
            {
                Ucw_p = Convert.ToDouble(UCW_p_textBox.Text);
            }
            Calc_Ucw();
            Calc_dUinst();
        }

        private void UCW_d_textBox_TextChanged(object sender, EventArgs e)
        {
            if (UcwMethod == "진단" && Door_checkBox.Checked && UCW_d_textBox.Text != string.Empty)
            {
                Ucw_d = Convert.ToDouble(UCW_d_textBox.Text);
            }
            Calc_Ucw();
            Calc_dUinst();
        }

        private void DiIndi_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DiIndi_comboBox.SelectedItem != null)
            {
                DiIndi = DiIndi_comboBox.SelectedItem.ToString();
                Rule_Ucw_g();
                Calc_dUinst();
            }
        }


        private void Frame_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Frame_comboBox.SelectedItem != null)
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
                            FrameName_textBox.Text = "";
                            SpacerName = "";
                            SpacerName_textBox.Text = "";
                            InstallName = "";
                            Install_textBox.Text = "";
                            Uf_mt_textBox.Text = "";
                            Uf_open_textBox.Text = "";
                            df_mt_textBox.Text = "";
                            df_open_textBox.Text = "";
                        }
                    }
                }
                catch { }
                Calc_Ucw();
                Calc_dUinst();
            }
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
                    FrameName2_textBox.Text = FrameName;
                    tabControl1.SelectedTab = tabControl1.TabPages["Frame_tabPage"];
                    check_FrameType = cw_frameDB_form.Select_CWFrame[3];
                    Uf_mt = Convert.ToDouble(cw_frameDB_form.Select_CWFrame[5]);
                    Uf_open = Convert.ToDouble(cw_frameDB_form.Select_CWFrame[6]);
                    Psi_p = Convert.ToDouble(cw_frameDB_form.Select_CWFrame[7]);
                    df_mt = Convert.ToDouble(cw_frameDB_form.Select_CWFrame[8]);
                    df_open = Convert.ToDouble(cw_frameDB_form.Select_CWFrame[9]);
                    Uf_mt_textBox.Text = String.Format("{0:F2}", Uf_mt);
                    Uf_open_textBox.Text = String.Format("{0:F2}", Uf_open);
                    df_mt_textBox.Text = String.Format("{0:F2}", df_mt);
                    df_open_textBox.Text = String.Format("{0:F2}", df_open);

                }
            }
            Calc_Ucw();
            Calc_dUinst();

        }

        private void FixGlassDB_button_Click(object sender, EventArgs e)
        {
            GlassDB cw_glassDB_form = new GlassDB();
            DialogResult result = cw_glassDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                FixGlassName = cw_glassDB_form.Select_Glass[1];
                FixGlassName_textBox.Text = FixGlassName;
                FixGlassName2_textBox.Text = FixGlassName;
                tabControl1.SelectedTab = tabControl1.TabPages["Glass_tabPage"];
                LE_CL_V = cw_glassDB_form.Select_Glass[5];
                Ug_Fix = Convert.ToDouble(cw_glassDB_form.Select_Glass[6]);
                Ug_Fix_textBox.Text = String.Format("{0:F3}", Ug_Fix);
                g = Convert.ToDouble(cw_glassDB_form.Select_Glass[7]);
                g_textBox.Text = String.Format("{0:F3}", g);
                τ = Convert.ToDouble(cw_glassDB_form.Select_Glass[8]);
                τg_textBox.Text = String.Format("{0:F3}", τ);
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
            Calc_Ucw();
            Calc_dUinst();

        }

        private void OpenGlassDB_button_Click(object sender, EventArgs e)
        {
            GlassDB cw_glassDB_form = new GlassDB();
            DialogResult result = cw_glassDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                OpenGlassName = cw_glassDB_form.Select_Glass[1];
                OpenGlassName_textBox.Text = OpenGlassName;
                OpenGlassName2_textBox.Text = OpenGlassName;
                tabControl1.SelectedTab = tabControl1.TabPages["Glass_tabPage"];
                Ug_Open = Convert.ToDouble(cw_glassDB_form.Select_Glass[6]);
                Ug_Open_textBox.Text = String.Format("{0:F3}", Ug_Open);
            }
            Calc_Ucw();
            Calc_dUinst();

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
                    check_FrameType = cw_spacerDB_form.Select_Spacer[4];
                    check_LE_CL_V = cw_spacerDB_form.Select_Spacer[9];
                    SpacerName = cw_spacerDB_form.Select_Spacer[3];
                    SpacerName_textBox.Text = SpacerName;
                    if (LE_CL_V.Contains("LE"))
                    {
                        Psi_g_fix = Convert.ToDouble(cw_spacerDB_form.Select_Spacer[7]);
                        Psi_g_open = Convert.ToDouble(cw_spacerDB_form.Select_Spacer[8]);
                    }
                    else
                    {
                        Psi_g_fix = Convert.ToDouble(cw_spacerDB_form.Select_Spacer[5]);
                        Psi_g_open = Convert.ToDouble(cw_spacerDB_form.Select_Spacer[6]);
                    }
                    Psi_g_fix_textBox.Text = String.Format("{0:F3}", Psi_g_fix);
                    Psi_g_open_textBox.Text = String.Format("{0:F3}", Psi_g_open);
                    tabControl1.SelectedTab = tabControl1.TabPages["Glass_tabPage"];

                }
            }
            Calc_Ucw();
            Calc_dUinst();
        }

        private void Panel_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            Act_UcWMethod();
            PanelCheck();
            Calc_Ucw();
            Calc_dUinst();
            Panel_check = Panel_checkBox.Checked;
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
                Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, PanelColor_comboBox, "커튼월", "색깔", "1");

                UCW_p_label.Visible = true;
                UCW_p_textBox.Visible = true;
                UCW_p_label2.Visible = true;

                αp_label.Visible = true;
                αp_textBox.Visible = true;
                αp_label.Visible = true;
                αp_label2.Visible = true;


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
                Ucw_p = 0;

                αp_label.Visible = false;
                αp_textBox.Visible = false;
                αp_label.Visible = false;
                αp_label2.Visible = false;
                αp = 0;
            }
        }

        private void PanelDB_button_Click(object sender, EventArgs e)
        {
            CW_PanelDB cw_panelDB_form = new CW_PanelDB();
            DialogResult result = cw_panelDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                PanelName = cw_panelDB_form.Select_CWPanel[1];
                Conductivity_p = Convert.ToDouble(cw_panelDB_form.Select_CWPanel[4]);
                Panel_textBox.Text = PanelName;
            }
            Calc_Up();
            Calc_Ucw();
            Calc_dUinst();
        }

        private void dPanel_textBox_TextChanged(object sender, EventArgs e)
        {
            if (dPanel_textBox.Text != null)
            {
                dPanel = Convert.ToDouble(dPanel_textBox.Text);
                Calc_Up();
            }
        }
        private void PanelGlassDB_button_Click(object sender, EventArgs e)
        {
            GlassDB cw_glassDB_form = new GlassDB();
            DialogResult result = cw_glassDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                PanelGlassName = cw_glassDB_form.Select_Glass[1];
                PanelGlass_textBox.Text = PanelGlassName;
                Ug_panel = Convert.ToDouble(cw_glassDB_form.Select_Glass[6]);
                LE_CL_V_Panel = cw_glassDB_form.Select_Glass[5];
            }
            Calc_Up();
            Calc_Ucw();
            Calc_dUinst();
        }

        private void Calc_Up()
        {
            double R;

            if (LE_CL_V_Panel != "LE")
            {
                R = 0.197;
            }
            else
            {
                R = 0.364;
            }

            Up = 1 / ((dPanel / 1000) / Conductivity_p + (1 / Ug_panel) - 0.17 + R);
        }

        private void PanelColor_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PanelColor_comboBox.SelectedItem != null)
            {
                PanelColor = PanelColor_comboBox.SelectedItem.ToString();
                String[][] value = Program.DB.getValue(DB.type.BaseDB_HCneed, "흡수율", "흡수율", "외장재색 = '" + PanelColor + "'");
                if (value.Length > 0)
                {
                    αp = Convert.ToDouble(value[0][0]);
                    αp_textBox.Text = String.Format("{0:F1}", αp);
                }
            }
        }

        private void Door_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            Act_UcWMethod();
            DoorCheck();
            Calc_Ucw();
            Calc_dUinst();
            Door_check = Door_checkBox.Checked;
        }
        private void DoorCheck()
        {
            if (Door_checkBox.Checked)
            {
                DoorFrame_label.Visible = true;
                DoorFrame_textBox.Visible = true;
                DoorFrameDB_button.Visible = true;

                DoorGlass_label.Visible = true;
                DoorGlass_textBox.Visible = true;
                DoorGlass2_textBox.Visible = true;
                DoorGlassDB_button.Visible = true;

                DoorSpacer_label.Visible = true;
                DoorSpacer_textBox.Visible = true;
                DoorSpacerDB_button.Visible = true;

                UCW_d_label.Visible = true;
                UCW_d_textBox.Visible = true;
                UCW_d_label2.Visible = true;

                gd_label.Visible = true;
                gd_textBox.Visible = true;
                gd_label2.Visible = true;

                τd_label.Visible = true;
                τd_textBox.Visible = true;
                τd_label2.Visible = true;

                Ug_Door_textBox.Visible = true;
                Psi_g_Door_textBox.Visible = true;
            }
            else
            {
                DoorFrame_label.Visible = false;
                DoorFrame_textBox.Visible = false;
                DoorFrameDB_button.Visible = false;

                DoorGlass_label.Visible = false;
                DoorGlass_textBox.Visible = false;
                DoorGlass2_textBox.Visible = false;
                DoorGlassDB_button.Visible = false;

                DoorSpacer_label.Visible = false;
                DoorSpacer_textBox.Visible = false;
                DoorSpacerDB_button.Visible = false;

                UCW_d_label.Visible = false;
                UCW_d_textBox.Visible = false;
                UCW_d_label2.Visible = false;
                Ucw_d = 0;

                gd_label.Visible = false;
                gd_textBox.Visible = false;
                gd_label2.Visible = false;
                gd = 0;

                τd_label.Visible = false;
                τd_textBox.Visible = false;
                τd_label2.Visible = false;
                τd = 0;

                Ug_Door_textBox.Visible = false;
                Ug_Door = 0;
                Psi_g_Door_textBox.Visible = false;
                Psi_g_Door = 0;
            }
        }

        private void DoorFrameDB_button_Click(object sender, EventArgs e)
        {
            CW_DoorFrameDB cw_doorframeDB_form = new CW_DoorFrameDB();

            DialogResult result = cw_doorframeDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                DoorFrame = cw_doorframeDB_form.Select_DoorFrame[1];
                DoorFrame_textBox.Text = DoorFrame;
                tabControl1.SelectedTab = tabControl1.TabPages["Frame_tabPage"];
                check_DoorFrame = cw_doorframeDB_form.Select_DoorFrame[3];
                Uf_door = Convert.ToDouble(cw_doorframeDB_form.Select_DoorFrame[4]);
                df_door = Convert.ToDouble(cw_doorframeDB_form.Select_DoorFrame[5]);
                Uf_door_textBox.Text = String.Format("{0:F2}", Uf_door);
                df_door_textBox.Text = String.Format("{0:F2}", df_door);

            }
            Calc_Ucw();
            Calc_dUinst();
        }


        private void DoorGlassDB_button_Click(object sender, EventArgs e)
        {
            GlassDB cw_glassDB_form = new GlassDB();
            DialogResult result = cw_glassDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                DoorGlassName = cw_glassDB_form.Select_Glass[1];
                DoorGlass_textBox.Text = DoorGlassName;
                DoorGlass2_textBox.Text = DoorGlassName;
                LE_CL_V_Door = cw_glassDB_form.Select_Glass[5];
                Ug_Door = Convert.ToDouble(cw_glassDB_form.Select_Glass[6]);
                Ug_Door_textBox.Text = String.Format("{0:F3}", Ug_Door);
                gd = Convert.ToDouble(cw_glassDB_form.Select_Glass[7]);
                gd_textBox.Text = String.Format("{0:F3}", gd);
                τd = Convert.ToDouble(cw_glassDB_form.Select_Glass[8]);
                τd_textBox.Text = String.Format("{0:F3}", τd);
                tabControl1.SelectedTab = tabControl1.TabPages["Glass_tabPage"];
            }

            //유리를 다시 선택했을 경우 
            try
            {
                if (check_LE_CL_V_Door != null)
                {
                    if (LE_CL_V_Door != check_LE_CL_V_Door)
                    {
                        MessageBox.Show("간봉을 다시 선택하세요.");
                        DoorSpacer = "";
                        DoorSpacer_textBox.Text = "";
                    }
                }

            }
            catch { }
            Calc_Ucw();
            Calc_dUinst();

        }

        private void DoorSpacerDB_button_Click(object sender, EventArgs e)
        {
            if (DoorFrame == null)
            {
                MessageBox.Show("출입문 프레임부터 선택하세요.");
            }
            else if (LE_CL_V_Door == null)
            {
                MessageBox.Show("출입문 유리부터 선택하세요.");
            }
            else
            {
                CW_SpacerDB cw_spacerDB_form = new CW_SpacerDB(DoorFrame, LE_CL_V_Door);
                DialogResult result = cw_spacerDB_form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    check_DoorFrame = cw_spacerDB_form.Select_Spacer[4];
                    check_LE_CL_V_Door = cw_spacerDB_form.Select_Spacer[9];
                    DoorSpacer = cw_spacerDB_form.Select_Spacer[3];
                    DoorSpacer_textBox.Text = DoorSpacer;
                    if (LE_CL_V_Door.Contains("LE"))
                    {
                        Psi_g_Door = Convert.ToDouble(cw_spacerDB_form.Select_Spacer[7]);
                    }
                    else
                    {
                        Psi_g_Door = Convert.ToDouble(cw_spacerDB_form.Select_Spacer[5]);
                    }
                    Psi_g_Door_textBox.Text = String.Format("{0:F3}", Psi_g_Door);
                    tabControl1.SelectedTab = tabControl1.TabPages["Glass_tabPage"];
                }
            }
            Calc_Ucw();
            Calc_dUinst();

        }

        private void Install_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Install_comboBox.SelectedItem != null)
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
                Calc_dUinst();
            }
        }

        private void Install_button_Click(object sender, EventArgs e)
        {
            if (UcwMethod == "계산")
            {
                if (FrameType == null)
                {
                    MessageBox.Show("프레임부터 선택하세요.");
                }
                else if (InstallType == null)
                {
                    MessageBox.Show(" 설치구조유형부터 선택하세요.");
                }
                else
                {
                    CW_InstallDB cw_installDB_form = new CW_InstallDB(InstallType, FrameType);
                    DialogResult result = cw_installDB_form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        InstallName = cw_installDB_form.Select_CWInstall[4];
                        Install_textBox.Text = InstallName;
                        check_InstallType = cw_installDB_form.Select_CWInstall[2];
                        tabControl1.SelectedTab = tabControl1.TabPages["Install_tabPage"];
                        Psi_InstallTop = Convert.ToDouble(cw_installDB_form.Select_CWInstall[5]);
                        Psi_InstallSide = Convert.ToDouble(cw_installDB_form.Select_CWInstall[6]);
                        Psi_InstallButtom = Convert.ToDouble(cw_installDB_form.Select_CWInstall[7]);

                        Psi_InstallTop_textBox.Text = String.Format("{0:F3}", Psi_InstallTop);
                        Psi_InstallSide_textBox.Text = String.Format("{0:F3}", Psi_InstallSide);
                        Psi_InstallButtom_textBox.Text = String.Format("{0:F3}", Psi_InstallButtom);


                        string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "커튼월설치열교이미지", "이미지", "구분1 = '" + InstallType + "' AND 구분3 = '" + InstallName + "'");
                        if (Image.Length > 0)
                        {
                            CWnstall_pictureBox.Visible = true;
                            CWnstall_pictureBox.Load(Program.gPath + Image[0][0]);
                            CWnstall_pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
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
                    CW_InstallDB window_installDB_form = new CW_InstallDB(InstallType);
                    DialogResult result = window_installDB_form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        InstallName = window_installDB_form.Select_CWInstall[4];
                        Install_textBox.Text = InstallName;
                        check_InstallType = window_installDB_form.Select_CWInstall[2];
                        FrameType = window_installDB_form.Select_CWInstall[3];
                        tabControl1.SelectedTab = tabControl1.TabPages["Install_tabPage"];
                        Psi_InstallTop = Convert.ToDouble(window_installDB_form.Select_CWInstall[5]);
                        Psi_InstallSide = Convert.ToDouble(window_installDB_form.Select_CWInstall[6]);
                        Psi_InstallButtom = Convert.ToDouble(window_installDB_form.Select_CWInstall[7]);

                        Psi_InstallTop_textBox.Text = String.Format("{0:F3}", Psi_InstallTop);
                        Psi_InstallSide_textBox.Text = String.Format("{0:F3}", Psi_InstallSide);
                        Psi_InstallButtom_textBox.Text = String.Format("{0:F3}", Psi_InstallButtom);


                        string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "커튼월설치열교이미지", "이미지", "구분1 = '" + InstallType + "' AND 구분3 = '" + InstallName + "'");
                        if (Image.Length > 0)
                        {
                            CWnstall_pictureBox.Visible = true;
                            CWnstall_pictureBox.Load(Program.gPath + Image[0][0]);
                            CWnstall_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                    }
                }
            }
            Calc_dUinst();

        }

        private void ImportSize_button_Click_1(object sender, EventArgs e)
        {
            CW_ImportSize Importsize_form = new CW_ImportSize(CWNum, CWName);

            DialogResult result = Importsize_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                SizeName = Importsize_form.Select[1];
                Area = Convert.ToDouble(Importsize_form.Select[3]);
                Width = Convert.ToDouble(Importsize_form.Select[4]);
                Height = Convert.ToDouble(Importsize_form.Select[5]);
                Ag_fix = Convert.ToDouble(Importsize_form.Select[6]);
                Ag_open = Convert.ToDouble(Importsize_form.Select[7]);
                Lg_fix = Convert.ToDouble(Importsize_form.Select[8]);
                Lg_open = Convert.ToDouble(Importsize_form.Select[9]);
                Ap = Convert.ToDouble(Importsize_form.Select[10]);
                Lp = Convert.ToDouble(Importsize_form.Select[11]);
                Af_mt = Convert.ToDouble(Importsize_form.Select[12]);
                Af_open = Convert.ToDouble(Importsize_form.Select[13]);
                Af_d = Convert.ToDouble(Importsize_form.Select[14]);
                Ag_d = Convert.ToDouble(Importsize_form.Select[15]);
                Lg_d = Convert.ToDouble(Importsize_form.Select[16]);

                Size_textBox.Text = SizeName + " 치수 적용";
                Area_textBox.Text = String.Format("{0:F2}", Area);
                Width_textBox.Text = String.Format("{0:F2}", Width);
                Height_textBox.Text = String.Format("{0:F2}", Height);
                Ag_fix_textBox.Text = String.Format("{0:F2}", Ag_fix);
                Ag_open_textBox.Text = String.Format("{0:F2}", Ag_open);
                Lg_fix_textBox.Text = String.Format("{0:F2}", Lg_fix);
                Lg_open_textBox.Text = String.Format("{0:F2}", Lg_open);
                Ap_textBox.Text = String.Format("{0:F2}", Ap);
                Lp_textBox.Text = String.Format("{0:F2}", Lp);
                Af_mt_textBox.Text = String.Format("{0:F2}", Af_mt);
                Af_open_textBox.Text = String.Format("{0:F2}", Af_open);
                Af_d_textBox.Text = String.Format("{0:F2}", Af_d);
                Ag_d_textBox.Text = String.Format("{0:F2}", Ag_d);
                Lg_d_textBox.Text = String.Format("{0:F2}", Lg_d);
            }
            Calc_Ucw();
            Calc_dUinst();
            d_InstallTop_textBox.Text = String.Format("{0:F2}", Width);
            d_InstallButtom_textBox.Text = String.Format("{0:F2}", Width);
            d_InstallSide_textBox.Text = String.Format("{0:F2}", (Height * 2));
            tabControl1.SelectedTab = tabControl1.TabPages["Install_tabPage"];

        }


        public void Rule_Ucw_g()
        {
            if (UcwMethod == "법규")
            {
                String[][] Date = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "법규시기,지역구분", "");
                String[][] Uvalue;
                if (Type == "기존 커튼월창")
                {
                    Uvalue = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '창호' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND  직접간접 =  '" + DiIndi + "'");
                }
                else
                {
                    Uvalue = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '창호' And 시기 = '2018.09' AND  지역 ='" + Date[0][1] + "'  AND 직접간접 =  '" + DiIndi + "'");
                }
                if (Uvalue.Length > 0)
                {
                    Ucw_g = Convert.ToDouble(Uvalue[0][0]);
                    UCW_g_textBox.Text = String.Format("{0:F3}", Ucw_g);
                }

            }
        }

        public void Rule_Ucw_p()
        {
            if (UcwMethod == "법규")
            {
                String[][] Date = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "법규시기,지역구분", "");
                String[][] Uvalue;

                if (Type == "기존 커튼월창")
                {
                    Uvalue = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '외벽' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND 직접간접 =  '" + DiIndi + "'");
                }
                else
                {
                    Uvalue = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '외벽' And 시기 = '2018.09' AND  지역 ='" + Date[0][1] + "'  AND 직접간접 =  '" + DiIndi + "'");
                }

                if (Panel_checkBox.Checked)
                {
                    if (Uvalue.Length > 0)
                    {
                        Ucw_p = Convert.ToDouble(Uvalue[0][0]);
                        UCW_p_textBox.Text = String.Format("{0:F3}", Ucw_p);
                    }
                }
                else
                {
                    Ucw_p = 0;
                }
            }
        }

        public void Rule_Ucw_d()
        {
            if (UcwMethod == "법규")
            {
                String[][] Date = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "법규시기,지역구분", "");
                String[][] Uvalue;
                if (Type == "기존 커튼월창")
                {
                    Uvalue = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '창호' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND  직접간접 =  '" + DiIndi + "'");
                }
                else
                {
                    Uvalue = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '창호' And 시기 = '2018.09' AND  지역 ='" + Date[0][1] + "'  AND 직접간접 =  '" + DiIndi + "'");
                }

                if (Door_checkBox.Checked)
                {
                    if (Uvalue.Length > 0)
                    {
                        Ucw_d = Convert.ToDouble(Uvalue[0][0]);
                        UCW_d_textBox.Text = String.Format("{0:F3}", Ucw_d);
                        Ff_d = 0.7;
                    }
                }
                else
                {
                    Ucw_d = 0;
                }
                Ff_g = 0.7;
            }
        }

        public void Calc_Ucw()
        {
            double Af_mt_g = Af_mt * (Ag_fix + Ag_open + Af_open) / (Area - Af_mt);
            double Af_mt_p = Af_mt * Ap / (Area - Af_mt);
            double Af_mt_d = Af_mt * (Ag_d + Af_d) / (Area - Af_mt);

            if (UcwMethod == "계산")
            {
                if (Ug_Fix != 0 && Uf_open != 0 && Psi_g_fix != 0 && Area != 0)
                {
                    Ucw = ((Ug_Fix * Ag_fix) + (Ug_Open * Ag_open) + (Up * Ag_open) + (Ug_Door * Ag_d) + (Uf_mt * Af_mt) + (Uf_open * Af_open) + (Uf_door * Af_d) + (Psi_g_fix * Lg_fix) + (Psi_g_open * Lg_open) + (Psi_p * Lp) + (Psi_g_Door * Lg_d)) / Area;
                }

                if (Ug_Fix != 0 && Uf_open != 0 && Psi_g_fix != 0 && Area != 0)
                {
                    Ucw_g = ((Ug_Fix * Ag_fix) + (Ug_Open * Ag_open) + (Uf_mt * Af_mt_g) + (Uf_open * Af_open) + (Psi_g_fix * Lg_fix) + (Psi_g_open * Lg_open)) / (Ag_fix + Ag_open + Af_mt_g + Af_open);
                    UCW_g_textBox.Text = String.Format("{0:F3}", Ucw_g);
                    Ff_g = (Ag_fix + Ag_open) / (Ag_fix + Ag_open + Af_mt_g + Af_open);
                }

                if (Panel_checkBox.Checked)
                {
                    if (Up != 0 && Psi_p != 0 && Area != 0)
                    {
                        Ucw_p = ((Up * Ag_open) + (Uf_mt * Af_mt_p) + +(Psi_p * Lp)) / (Ap + Af_mt_p);
                        UCW_p_textBox.Text = String.Format("{0:F3}", Ucw_p);
                    }
                }
                else
                {
                    Ucw_p = 0;
                }

                if (Door_checkBox.Checked)
                {
                    if (Ug_Door != 0 && Uf_door != 0 && Psi_g_Door != 0 && Area != 0)
                    {
                        Ucw_d = ((Ug_Door * Ag_d) + (Uf_mt * Af_mt_d) + (Uf_door * Af_d) + (Psi_g_Door * Lg_d)) / (Ag_d + Af_mt_d + Af_d);
                        UCW_d_textBox.Text = String.Format("{0:F3}", Ucw_d);
                        Ff_d = Ag_d / (Ag_d + Af_mt_d + Af_d);
                    }
                }
                else
                {
                    Ucw_d = 0;
                }
            }
            else
            {
                Ucw = ((Ag_fix + Ag_open + Af_open + Af_mt_g) * Ucw_g + (Ap + Af_mt_p) * Ucw_p + (Af_d + Ag_d + Af_mt_d) * Ucw_d) / Area;
            }
        }

        public void Calc_dUinst()
        {
            if (Ucw != 0 && Area != 0)
            {
                dUinst = ((Psi_InstallTop * Width) + (Psi_InstallButtom * Width) + (Psi_InstallSide * Height * 2)) / Area;

                if (dUinst.Equals(double.NaN) == false)
                {
                    dUinst_textBox.Text = String.Format("{0:F3}", dUinst);
                }
            }

            Ucw_inst = Ucw + dUinst;
            Ucw_g_inst = Ucw_g + dUinst;

            if (Panel_checkBox.Checked)
            {
                if (Up != 0 && Area != 0)
                {
                    Ucw_p_inst = Ucw_p + dUinst;
                }
            }
            else
            {
                Ucw_p_inst = 0;
            }

            if (Door_checkBox.Checked)
            {
                if (Ug_Door != 0 && Area != 0)
                {
                    Ucw_d_inst = Ucw_d + dUinst;
                }
            }
            else
            {
                Ucw_d_inst = 0;
            }
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
            if (CWName == null)
            {
                MessageBox.Show("커튼월 명칭을 입력하세요.");
            }
            else if (Type == null)
            {
                MessageBox.Show("커튼월 리모델링 유형을 선택하세요.");
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
                else if (Panel_checkBox.Checked)
                {
                    if (PanelName == null)
                    {
                        MessageBox.Show("패널을 선택하세요.");
                    }
                    else if (PanelGlassName == null)
                    {
                        MessageBox.Show("패널 유리를 선택하세요.");
                    }
                    else if (dPanel == 0)
                    {
                        MessageBox.Show("패널 두께를 선택하세요.");
                    }
                    else if (PanelColor == null)
                    {
                        MessageBox.Show("패널 색를 선택하세요.");
                    }
                    else
                    {
                        Save();
                    }
                }
                else if (Door_checkBox.Checked)
                {
                    if (DoorFrame == null)
                    {
                        MessageBox.Show("출입문 프레임을 선택하세요.");
                    }
                    else if (DoorGlassName == null)
                    {
                        MessageBox.Show("출입문 유리를 선택하세요.");
                    }
                    else if (DoorSpacer == null)
                    {
                        MessageBox.Show("출입문 간봉을 선택하세요.");
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
            else
            {
                Save();
            }
        }
        public static bool OnLoadListProc(Form form)
        {
            List_ConstructionCW f = (List_ConstructionCW)form;

            f.load_List();

            return true;
        }

        private void Save()
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            #region 법규
            double 법규Ucw_g = 0, 법규Ucw_p = 0, 법규Ucw_d = 0;
            String[][] Date = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "법규시기,지역구분", "");
            String[][] Uvalue = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '창호' And 시기 = '2018.09' AND  지역 ='" + Date[0][1] + "'  AND 직접간접 =  '" + DiIndi + "'");
            if (Uvalue.Length > 0)
            {
                법규Ucw_g = Convert.ToDouble(Uvalue[0][0]);
                법규Ucw_d = Convert.ToDouble(Uvalue[0][0]);
            }
            Uvalue = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '외벽' And 시기 = '2018.09' AND  지역 ='" + Date[0][1] + "'  AND 직접간접 =  '" + DiIndi + "'");
            if (Uvalue.Length > 0)
            {
                법규Ucw_p = Convert.ToDouble(Uvalue[0][0]);
            }
            #endregion

            Program.DB.setValue(DB.type.ProjDB, "ConstructionCW", "번호,프로젝트유형,명칭,Type,기존커튼월,Ucw적용방법,직접간접,프레임유형,프레임종류,고정유리종류,개폐유리종류,간봉종류,설치유형,설치종류,LE_CL_V,패널적용유무,출입문적용유무," +
                      "고정유리열관류율,개폐유리열관류율,태양열취득률,빛투과율,고정유리선형열관류율,개폐유리선형열관류율,고정프레임열관류율,개폐프레임열관류율,고정프레임두께,개폐프레임두께," +
                      "상부설치열관류율,측면설치열관류율,하부설치열관류율," +
                      "사이즈명칭,커튼월면적,너비,높이,고정창유리면적,개폐창유리면적,고정창유리둘레길이,개폐창유리둘레길이,M_T프레임면적,개폐창프레임면적," +
                      "커튼월창열관류율,유리부분열관류율,설치열교가산치,커튼월창유효열관류율,유리부분유효열관류율,유리부분유리면적비," +
                      "법규유리부분열관류율",
                    "'" + CWNum_textBox.Text + "','" + 프로젝트유형[0][0] + "','" + CWName + "','" + Type + "','" + OldCW + "','" + UcwMethod + "','" + DiIndi + "','" + FrameType + "','" + FrameName + "','" + FixGlassName + "','" + OpenGlassName + "','" + SpacerName + "','" + InstallType + "','" + InstallName + "','" + LE_CL_V + "','" + Panel_check.ToString() + "','" + Door_check.ToString() + "','" +
                    Ug_Fix.ToString() + "','" + Ug_Open.ToString() + "','" + g.ToString() + "','" + τ.ToString() + "','" + Psi_g_fix.ToString() + "','" + Psi_g_open.ToString() + "','" + Uf_mt.ToString() + "','" + Uf_open.ToString() + "','" + df_mt.ToString() + "','" + df_open.ToString() + "','" +
                    Psi_InstallTop.ToString() + "','" + Psi_InstallSide.ToString() + "','" + Psi_InstallButtom.ToString() + "','" +
                    SizeName + "','" + Area.ToString() + "','" + Width.ToString() + "','" + Height.ToString() + "','" + Ag_fix.ToString() + "','" + Ag_open.ToString() + "','" + Lg_fix.ToString() + "','" + Lg_open.ToString() + "','" + Af_mt.ToString() + "','" + Af_open.ToString() + "','" +
                    Ucw.ToString() + "','" + Ucw_g.ToString() + "','" + dUinst.ToString() + "','" + Ucw_inst.ToString() + "','" + Ucw_g_inst.ToString() + "','" + Ff_g.ToString() + "','" +
                    법규Ucw_g.ToString()
                    + "'", "번호");

            if (Panel_checkBox.Checked)
            {
                Program.DB.setValue(DB.type.ProjDB, "ConstructionCW", "번호," +
                          "패널종류,패널유리종류,LE_CL_V_Panel," +
                          "패널열관류율,패널유리열관류율,패널열전도율,패널흡수율,패널선형열관류율,패널두께," +
                          "패널면적,패널둘레길이," +
                          "패널부분열관류율,패널부분유효열관류율," +
                          "법규패널부분열관류율",
                        "'" + CWNum_textBox.Text + "','" +
                        PanelName + "','" + PanelGlassName + "','" + LE_CL_V_Panel + "','" +
                        Up.ToString() + "','" + Ug_panel.ToString() + "','" + Conductivity_p.ToString() + "','" + αp.ToString() + "','" + Psi_p.ToString() + "','" + dPanel.ToString() + "','" +
                        Ap.ToString() + "','" + Lp.ToString() + "','" +
                        Ucw_p.ToString() + "','" + Ucw_p_inst.ToString() + "','" +
                        법규Ucw_p.ToString()
                        + "'", "번호");
            }
            if (Door_checkBox.Checked)
            {
                Program.DB.setValue(DB.type.ProjDB, "ConstructionCW", "번호," +
                       "출입문프레임유형,출입문프레임종류,출입문유리종류,출입문간봉종류,LE_CL_V_Door," +
                       "출입문유리열관류율,출입문태양열취득률,출입문빛투과율,출입문유리선형열관류율,출입문프레임두께,출입문프레임열관류율," +
                       "출입문프레임면적,출입문유리면적,출입문유리둘레길이," +
                       "출입문부분열관류율,출입문부분유효열관류율,출입문부분유리면적비," +
                       "법규출입문부분열관류율",
                        "'" + CWNum_textBox.Text + "','" +
                     DoorFrame + "','" + DoorFrame + "','" + DoorGlassName + "','" + DoorSpacer + "','" + LE_CL_V_Door + "','" +
                     Ug_Door.ToString() + "','" + gd.ToString() + "','" + τd.ToString() + "','" + Psi_g_Door.ToString() + "','" + df_door.ToString() + "','" + Uf_door.ToString() + "','" +
                     Af_d.ToString() + "','" + Ag_d.ToString() + "','" + Lg_d.ToString() + "','" +
                     Ucw_d.ToString() + "','" + Ucw_d_inst.ToString() + "','" + Ff_d.ToString() + "','" +
                     법규Ucw_d.ToString()
                     + "'", "번호");
            }
            this.DialogResult = DialogResult.OK;
            this.Hide();
            Program.getMenuForm().DoLoadForm(30, OnLoadListProc);
        }

        private void reset()
        {
            CWNum_textBox.Text = null;
            Name_textBox.Text = null;

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;

            OldCW_comboBox.SelectedItem = null;
            Ucw_comboBox.SelectedItem = null;
            DiIndi_comboBox.SelectedItem = null;
            Frame_comboBox.SelectedItem = null;

            FrameName_textBox.Text = null;
            FixGlassName_textBox.Text = null;
            FixGlassName2_textBox.Text = null;
            OpenGlassName_textBox.Text = null;
            OpenGlassName2_textBox.Text = null;
            SpacerName_textBox.Text = null;

            Install_comboBox.SelectedItem = null;
            Install_textBox.Text = null;

            Ug_Fix_textBox.Text = null;
            Ug_Open_textBox.Text = null;
            g_textBox.Text = null;
            τg_textBox.Text = null;
            Psi_g_fix_textBox.Text = null;
            Psi_g_open_textBox.Text = null;
            Uf_mt_textBox.Text = null;
            Uf_open_textBox.Text = null;
            df_mt_textBox.Text = null;
            df_open_textBox.Text = null;

            Psi_InstallTop_textBox.Text = null;
            Psi_InstallSide_textBox.Text = null;
            Psi_InstallButtom_textBox.Text = null;

            SizeName = null;
            Size_textBox.Text = null;
            Area_textBox.Text = null;
            Width_textBox.Text = null;
            Height_textBox.Text = null;
            Ag_fix_textBox.Text = null;
            Ag_open_textBox.Text = null;
            Lg_fix_textBox.Text = null;
            Lg_open_textBox.Text = null;
            Af_mt_textBox.Text = null;
            Af_open_textBox.Text = null;

            UCW_g_textBox.Text = null;
            dUinst_textBox.Text = null;
            Panel_checkBox.Checked = false;
            Door_checkBox.Checked = false;

            Panel_textBox.Text = null;
            PanelGlass_textBox.Text = null;

            αp_textBox.Text = null;

            dPanel_textBox.Text = null;
            Ap_textBox.Text = null;
            Lp_textBox.Text = null;

            UCW_p_textBox.Text = null;

            DoorFrame_textBox.Text = null;
            DoorGlass_textBox.Text = null;
            DoorGlass2_textBox.Text = null;
            Ug_Door_textBox.Text = null;
            gd_textBox.Text = null;
            Psi_g_Door_textBox.Text = null;
            df_door_textBox.Text = null;
            Uf_door_textBox.Text = null;
            Af_d_textBox.Text = null;
            Ag_d_textBox.Text = null;
            Lg_d_textBox.Text = null;
            UCW_d_textBox.Text = null;

            CWType_pictureBox.Visible = false;
            CWnstall_pictureBox.Visible = false;

            CWNum = null;
            CWName = null; Type = null; OldCW = null; UcwMethod = null; DiIndi = null; FrameType = null; check_FrameType = null; FrameName = null; FixGlassName = null; OpenGlassName = null; SpacerName = null; InstallType = null; check_InstallType = null; InstallName = null; LE_CL_V = null; check_LE_CL_V = null; SizeName = null;
            PanelName = null; PanelGlassName = null; LE_CL_V_Panel = null; PanelColor = null;
            DoorFrame = null; check_DoorFrame = null; DoorGlassName = null; DoorSpacer = null; LE_CL_V_Door = null; check_LE_CL_V_Door = null;
            Size = null;
            Ug_Fix = 0; Ug_Open = 0; g = 0; τ = 0; Psi_g_fix = 0; Psi_g_open = 0; Uf_mt = 0; Uf_open = 0; df_mt = 0; df_open = 0;
            Up = 0; Ug_panel = 0; Conductivity_p = 0; αp = 0; Psi_p = 0; dPanel = 0;
            Ug_Door = 0; gd = 0; τd = 0; Psi_g_Door = 0; df_door = 0; Uf_door = 0;
            Psi_InstallTop = 0; Psi_InstallSide = 0; Psi_InstallButtom = 0;
            Ucw = 0; Ucw_g = 0; Ucw_p = 0; Ucw_d = 0;
            Ucw_inst = 0; dUinst = 0;
            Ucw_g_inst = 0; Ucw_p_inst = 0; Ucw_d_inst = 0;
            Area = 0; Width = 0; Height = 0; Ag_fix = 0; Ag_open = 0; Lg_fix = 0; Lg_open = 0; Ap = 0; Lp = 0; Af_mt = 0; Af_open = 0; Af_d = 0; Ag_d = 0; Lg_d = 0;
            Old = null; f_shgc = null; f_τ = null;
            Panel_check = false; Door_check = false;
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();
            CWNum_textBox.Text = ID;
            CWNum = ID;

            String[][] Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "번호,명칭,Type,기존커튼월,Ucw적용방법,직접간접,프레임유형,프레임종류,고정유리종류,개폐유리종류,간봉종류,설치유형,설치종류,LE_CL_V," +
                  "고정유리열관류율,개폐유리열관류율,태양열취득률,빛투과율,고정유리선형열관류율,개폐유리선형열관류율,고정프레임열관류율,개폐프레임열관류율,고정프레임두께,개폐프레임두께," +
                  "상부설치열관류율,측면설치열관류율,하부설치열관류율," +
                  "사이즈명칭,커튼월면적,너비,높이,고정창유리면적,개폐창유리면적,고정창유리둘레길이,개폐창유리둘레길이,M_T프레임면적,개폐창프레임면적," +
                  "커튼월창열관류율,유리부분열관류율,설치열교가산치,커튼월창유효열관류율,유리부분유효열관류율," +
                  "패널적용유무,출입문적용유무"
                  , "번호 = '" + ID + "'");
            if (Load.Length > 0)
            {
                Name_textBox.Text = Load[0][1];
                Type = Load[0][2];
                switch (Type)
                {
                    case "기존 커튼월창":
                        radioButton1.Checked = true;
                        break;

                    case "신규 커튼월창":
                        radioButton2.Checked = true;
                        break;

                    case "철거 후 신규":
                        radioButton3.Checked = true;
                        break; ;
                }
                OldCW = Load[0][3];
                OldCW_comboBox.SelectedIndex = OldCW_comboBox.FindStringExact(OldCW);
                UcwMethod = Load[0][4];
                Ucw_comboBox.SelectedItem = UcwMethod;
                DiIndi = Load[0][5];
                DiIndi_comboBox.SelectedItem = DiIndi;

                FrameType = Load[0][6];
                Frame_comboBox.SelectedItem = FrameType;
                check_FrameType = FrameType;

                FrameName = Load[0][7];
                FrameName_textBox.Text = FrameName;
                FrameName2_textBox.Text = FrameName;

                FixGlassName = Load[0][8];
                FixGlassName_textBox.Text = FixGlassName;
                FixGlassName2_textBox.Text = FixGlassName;

                OpenGlassName = Load[0][9];
                OpenGlassName_textBox.Text = OpenGlassName;
                OpenGlassName2_textBox.Text = OpenGlassName;

                SpacerName = Load[0][10];
                SpacerName_textBox.Text = SpacerName;

                InstallType = Load[0][11];
                Install_comboBox.SelectedItem = InstallType;

                InstallName = Load[0][12];
                Install_textBox.Text = InstallName;

                LE_CL_V = Load[0][13];
                check_LE_CL_V = Load[0][13];

                Ug_Fix = Convert.ToDouble(Load[0][14]);
                Ug_Fix_textBox.Text = String.Format("{0:F3}", Ug_Fix);
                Ug_Open = Convert.ToDouble(Load[0][15]);
                Ug_Open_textBox.Text = String.Format("{0:F3}", Ug_Open);
                g = Convert.ToDouble(Load[0][16]);
                g_textBox.Text = String.Format("{0:F3}", g);
                τ = Convert.ToDouble(Load[0][17]);
                τg_textBox.Text = String.Format("{0:F3}", τ);
                Psi_g_fix = Convert.ToDouble(Load[0][18]);
                Psi_g_fix_textBox.Text = String.Format("{0:F3}", Psi_g_fix);
                Psi_g_open = Convert.ToDouble(Load[0][19]);
                Psi_g_open_textBox.Text = String.Format("{0:F3}", Psi_g_open);
                Uf_mt = Convert.ToDouble(Load[0][20]);
                Uf_mt_textBox.Text = String.Format("{0:F3}", Uf_mt);
                Uf_open = Convert.ToDouble(Load[0][21]);
                Uf_open_textBox.Text = String.Format("{0:F3}", Uf_open);
                df_mt = Convert.ToDouble(Load[0][22]);
                df_mt_textBox.Text = String.Format("{0:F3}", df_mt);
                df_open = Convert.ToDouble(Load[0][23]);
                df_open_textBox.Text = String.Format("{0:F3}", df_open);

                Psi_InstallTop = Convert.ToDouble(Load[0][24]);
                Psi_InstallTop_textBox.Text = String.Format("{0:F3}", Psi_InstallTop);
                Psi_InstallSide = Convert.ToDouble(Load[0][25]);
                Psi_InstallSide_textBox.Text = String.Format("{0:F3}", Psi_InstallSide);
                Psi_InstallButtom = Convert.ToDouble(Load[0][26]);
                Psi_InstallButtom_textBox.Text = String.Format("{0:F3}", Psi_InstallButtom);

                SizeName = Load[0][27];
                Area = Convert.ToDouble(Load[0][28]);
                Width = Convert.ToDouble(Load[0][29]);
                Height = Convert.ToDouble(Load[0][30]);
                Ag_fix = Convert.ToDouble(Load[0][31]);
                Ag_open = Convert.ToDouble(Load[0][32]);
                Lg_fix = Convert.ToDouble(Load[0][33]);
                Lg_open = Convert.ToDouble(Load[0][34]);
                Af_mt = Convert.ToDouble(Load[0][35]);
                Af_open = Convert.ToDouble(Load[0][36]);

                Size_textBox.Text = SizeName + " 치수 적용";
                Area_textBox.Text = String.Format("{0:F2}", Area);
                Width_textBox.Text = String.Format("{0:F2}", Width);
                Height_textBox.Text = String.Format("{0:F2}", Height);
                Ag_fix_textBox.Text = String.Format("{0:F2}", Ag_fix);
                Ag_open_textBox.Text = String.Format("{0:F2}", Ag_open);
                Lg_fix_textBox.Text = String.Format("{0:F2}", Lg_fix);
                Lg_open_textBox.Text = String.Format("{0:F2}", Lg_open);
                Af_mt_textBox.Text = String.Format("{0:F2}", Af_mt);
                Af_open_textBox.Text = String.Format("{0:F2}", Af_open);

                Ucw = Convert.ToDouble(Load[0][37]);
                Ucw_g = Convert.ToDouble(Load[0][38]);
                UCW_g_textBox.Text = String.Format("{0:F3}", Ucw_g);
                dUinst = Convert.ToDouble(Load[0][39]);
                dUinst_textBox.Text = String.Format("{0:F3}", dUinst);
                Ucw_inst = Convert.ToDouble(Load[0][40]);
                Ucw_g_inst = Convert.ToDouble(Load[0][41]);

                Panel_check = Convert.ToBoolean(Load[0][42]);
                Panel_checkBox.Checked = Convert.ToBoolean(Load[0][42]);
                Door_check = Convert.ToBoolean(Load[0][43]);
                Door_checkBox.Checked = Convert.ToBoolean(Load[0][43]);

            }

            if (Panel_check == true)
            {
                Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "패널종류,패널유리종류,LE_CL_V_Panel," +
                       "패널열관류율,패널유리열관류율,패널열전도율,패널흡수율,패널선형열관류율,패널두께," +
                       "패널면적,패널둘레길이," +
                       "패널부분열관류율,패널부분유효열관류율"
                       , "번호 = '" + ID + "'");
                if (Load.Length > 0)
                {
                    PanelName = Load[0][0];
                    Panel_textBox.Text = PanelName;

                    PanelGlassName = Load[0][1];
                    PanelGlass_textBox.Text = PanelGlassName;

                    LE_CL_V_Panel = Load[0][2];

                    Up = Convert.ToDouble(Load[0][3]);
                    Ug_panel = Convert.ToDouble(Load[0][4]);
                    Conductivity_p = Convert.ToDouble(Load[0][5]);

                    αp = Convert.ToDouble(Load[0][6]);
                    αp_textBox.Text = String.Format("{0:F1}", αp);

                    Psi_p = Convert.ToDouble(Load[0][7]);

                    dPanel = Convert.ToDouble(Load[0][8]);
                    dPanel_textBox.Text = String.Format("{0:F1}", dPanel_textBox.Text);

                    Ap = Convert.ToDouble(Load[0][9]);
                    Lp = Convert.ToDouble(Load[0][10]);
                    Ap_textBox.Text = String.Format("{0:F2}", Ap);
                    Lp_textBox.Text = String.Format("{0:F2}", Lp);


                    Ucw_p = Convert.ToDouble(Load[0][11]);
                    UCW_p_textBox.Text = String.Format("{0:F3}", Ucw_p);
                    Ucw_p_inst = Convert.ToDouble(Load[0][12]);
                }
            }
            else { }

            if (Door_check == true)
            {
                Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "출입문프레임유형,출입문프레임종류,출입문유리종류,출입문간봉종류,LE_CL_V_Door," +
                   "출입문유리열관류율,출입문태양열취득률,출입문빛투과율,출입문유리선형열관류율,출입문프레임두께,출입문프레임열관류율," +
                   "출입문프레임면적,출입문유리면적,출입문유리둘레길이," +
                   "출입문부분열관류율,출입문부분유효열관류율"
                      , "번호 = '" + ID + "'");
                if (Load.Length > 0)
                {
                    DoorFrame = Load[0][1];
                    DoorFrame_textBox.Text = DoorFrame;
                    check_DoorFrame = DoorFrame;

                    DoorGlassName = Load[0][2];
                    DoorGlass_textBox.Text = DoorGlassName;
                    DoorGlass2_textBox.Text = DoorGlassName;

                    DoorSpacer = Load[0][3];
                    LE_CL_V_Door = Load[0][4];
                    check_LE_CL_V_Door = LE_CL_V_Door;

                    Ug_Door = Convert.ToDouble(Load[0][5]);
                    Ug_Door_textBox.Text = String.Format("{0:F3}", Ug_Door);

                    gd = Convert.ToDouble(Load[0][6]);
                    gd_textBox.Text = String.Format("{0:F3}", gd);

                    τd = Convert.ToDouble(Load[0][7]);
                    τd_textBox.Text = String.Format("{0:F3}", τd);

                    Psi_g_Door = Convert.ToDouble(Load[0][8]);
                    Psi_g_Door_textBox.Text = String.Format("{0:F3}", Psi_g_Door);

                    df_door = Convert.ToDouble(Load[0][9]);
                    df_door_textBox.Text = String.Format("{0:F3}", df_door);

                    Uf_door = Convert.ToDouble(Load[0][10]);
                    Uf_door_textBox.Text = String.Format("{0:F3}", Uf_door);

                    Af_d = Convert.ToDouble(Load[0][11]);
                    Ag_d = Convert.ToDouble(Load[0][12]);
                    Lg_d = Convert.ToDouble(Load[0][13]);
                    Af_d_textBox.Text = String.Format("{0:F2}", Af_d);
                    Ag_d_textBox.Text = String.Format("{0:F2}", Ag_d);
                    Lg_d_textBox.Text = String.Format("{0:F2}", Lg_d);

                    Ucw_d = Convert.ToDouble(Load[0][14]);
                    UCW_d_textBox.Text = String.Format("{0:F3}", Ucw_d);
                    Ucw_d_inst = Convert.ToDouble(Load[0][15]);
                }
            }
            else { }


            string[][] Image1 = Program.DB.getValue(DB.type.BaseDB_HCneed, "커튼월구조유형이미지", "이미지", "구조유형 = '" + Type + "'");
            if (Image1.Length > 0)
            {
                CWType_pictureBox.Visible = true;
                CWType_pictureBox.Load(Program.gPath + Image1[0][0]);
                CWType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

            string[][] Image2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "커튼월설치열교이미지", "이미지", "구분1 = '" + InstallType + "' AND 구분3 = '" + InstallName + "'");
            if (Image2.Length > 0)
            {
                CWnstall_pictureBox.Visible = true;
                CWnstall_pictureBox.Load(Program.gPath + Image2[0][0]);
                CWnstall_pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }

        }

        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            CWNum_textBox.Text = ID;
            CWNum = ID;
        }

    }
}
