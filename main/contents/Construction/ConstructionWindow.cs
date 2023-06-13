using main.contentslist;
using main.Properties;
using main.subcontents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace main.contents
{
    public partial class ConstructionWindow : Form
    {
        private String WinNum;
        String WindowName, Type, OldWindow, UwMethod, DiIndi, FrameType, SingleDoubleType, FrameMaterial, FrameName, GlassName, SpacerName, InstallType, InstallName, LE_CL_V;
        String check_FrameType, check_SingleDoubleType, check_FrameMaterial, check_LE_CL_V, check_InstallType;
        String[][] Size;
        double Ug, g, τD65_SNA, Psi_g_fix, Psi_g_open, Uw;
        List<double> Sub_Uw = new List<double>(); List<double> Sub_Uw_inst = new List<double>(); List<double> Sub_dUinst = new List<double>();// dUinst는 열교가산치, Uw_inst는 유효열관류율(창호열관류율+열교가산치)
        double Uf_open, Uf_fix, Uf_btw, df_open, df_fix, df_btw;
        double Psi_InstallTop, Psi_InstallSide, Psi_InstallButtom;
        List<double> Sub_Area = new List<double>(); List<double> Sub_Width = new List<double>(); List<double> Sub_Height = new List<double>(); List<double> Sub_Ag_fix = new List<double>(); List<double> Sub_Ag_open = new List<double>(); List<double> Sub_Af_open = new List<double>(); List<double> Sub_Af_fix = new List<double>(); List<double> Sub_Af_btw = new List<double>(); List<double> Sub_Lg_fix = new List<double>(); List<double> Sub_Lg_open = new List<double>();
        String[][] Old; String[][] f_shgc; String[][] f_τ;

        public ConstructionWindow()
        {
            InitializeComponent();
            Program.DB.initTable(DB.type.CalcDB, "Import_WindowSize"); //불러온 사이즈 정보 저장할 table 생성
            Program.DB.initTable(DB.type.CalcDB, "Select_WindowSize"); //선택한 사이즈 정보 저장할 table 생성
            Program.DB.initTable(DB.type.ProjDB, "SubWindow");
            string[][] Image = Program.DB.getValue(DB.type.BaseDB, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '창호'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;


            //직접간접 콤보박스
            Program.UTIL.FillComboBox(DiIndi_comboBox, "창호", "실외조건", "1");
            //프레임종류 콤보박스
            Program.UTIL.FillComboBox(Frame_comboBox, "창호", "프레임시스템", "1");
            //설치위치 콤보박스
            Program.UTIL.FillComboBox(Install_comboBox, "창호", "구조", "1");


        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void Name_textBox_TextChanged(object sender, EventArgs e)
        {
            WindowName = Name_textBox.Text;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Type = "기존창호";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_WindowType_image(Type);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Type = "신규";
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

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Type = "외부(커튼월)덧댐";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_WindowType_image(Type);
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            Type = "내부덧댐";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_WindowType_image(Type);
        }

        private void Changed_Type(String Type)
        {
            Uw_comboBox.Items.Clear();

            switch (Type)
            {
                case "기존창호":
                    Uw_comboBox.Items.Add("계산");
                    Uw_comboBox.Items.Add("법규");
                    Uw_comboBox.Items.Add("진단");
                    AdditionalWindow_textBox.Visible = false;
                    AdditionalWindow_comboBox.Visible = false;
                    break;

                case "신규":
                    Uw_comboBox.Items.Add("계산");
                    Uw_comboBox.Items.Add("법규");
                    AdditionalWindow_textBox.Visible = false;
                    AdditionalWindow_comboBox.Visible = false;
                    break;

                case "철거 후 신규":
                    Uw_comboBox.Items.Add("계산");
                    Uw_comboBox.Items.Add("법규");
                    AdditionalWindow_textBox.Visible = true;
                    AdditionalWindow_comboBox.Visible = true;
                    Load_AdditionalWindow(Type);
                    break; ;

                case "외부(커튼월)덧댐":
                    Uw_comboBox.Items.Add("계산");
                    AdditionalWindow_textBox.Text = "커튼월창 :";
                    AdditionalWindow_textBox.Visible = true;
                    AdditionalWindow_comboBox.Visible = true;
                    Load_AdditionalWindow(Type);
                    break;

                case "내부덧댐":
                    Uw_comboBox.Items.Add("계산");
                    AdditionalWindow_textBox.Text = "기존 창호 :";
                    AdditionalWindow_textBox.Visible = true;
                    AdditionalWindow_comboBox.Visible = true;
                    Load_AdditionalWindow(Type);
                    break;
            }

            Uw_comboBox.SelectedIndex = 0;
        }

        //덧댐 창호 리스트 불러오기 
        private void Load_AdditionalWindow(String Type)
        {
            string def_value;
            String[][] Table;

            if (Type == "외부(커튼월)덧댐")
            {
                def_value = "Type = '기존창호'";
                Table = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "창호명칭", def_value);

            }
            else if (Type == "내부덧댐")
            {
                def_value = "Type = '기존창호'";
                Table = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "창호명칭", def_value);

            }
            else if (Type == "철거 후 신규")
            {
                def_value = "Type = '기존창호'";
                Table = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "창호명칭", def_value);

            }
            else
            {
                def_value = "Type = ''";
                Table = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "창호명칭", def_value);
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

            AdditionalWindow_comboBox.DataSource = sources.DefaultView;
            AdditionalWindow_comboBox.DisplayMember = "Text";
            for (i = 0; i < AdditionalWindow_comboBox.Items.Count; i++)
            {
                var arr = ((DataRowView)AdditionalWindow_comboBox.Items[i]).Row.ItemArray;
                if (arr.Length > 1 && arr[1].ToString() == def_value)
                {
                    AdditionalWindow_comboBox.SelectedIndex = i;
                    break;
                }
            }

        }

        private void AdditionalWindow_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OldWindow = AdditionalWindow_comboBox.SelectedItem.ToString();
            Calc_g_AdditionalWindow();
        }

        private double Calc_Uw_AdditionalWindow(double NewUw)
        {
            double Uw;
            if (OldWindow != null)
            {
                if (Type == "외부(커튼월)덧댐") //추후 커튼월 db로 고쳐야 함
                {

                    Old = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "번호,창호명칭,Type,LE_CL_V,유리열관류율,태양열취득률,빛투과율,창호열관류율", "창호명칭 = '" + OldWindow + "'");
                    Uw = 1 / (0.019 + 1 / Convert.ToDouble(Old[0][7]) + 1 / NewUw);
                }
                else if (Type == "내부덧댐")
                {
                    Old = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "번호,창호명칭,Type,LE_CL_V,유리열관류율,태양열취득률,빛투과율,창호열관류율", "창호명칭 = '" + OldWindow + "'");
                    Uw = 1 / (0.019 + 1 / Convert.ToDouble(Old[0][7]) + 1 / NewUw);
                }
                else
                {
                    Uw = NewUw;
                }
            }
            else
            {
                Uw = NewUw;
            }
            return Uw;
        }

        private void Calc_g_AdditionalWindow()
        {
            if (Type == "외부(커튼월)덧댐") //추후 커튼월 db로 고쳐야 함
            {
                if (OldWindow != null && GlassName != null)
                {
                    Old = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "번호,창호명칭,Type,LE_CL_V,유리열관류율,태양열취득률,빛투과율,창호열관류율", "창호명칭 = '" + OldWindow + "'");
                    String 조합구성 = LE_CL_V + "+" + Old[0][3];
                    f_shgc = Program.DB.getValue(DB.type.BaseDB, "이중창보정계수", "계수", "조합구성 = '" + 조합구성 + "' AND 보정유형 = '태양열취득률'");
                    f_τ = Program.DB.getValue(DB.type.BaseDB, "이중창보정계수", "계수", "조합구성 = '" + 조합구성 + "' AND 보정유형 = '빛투과율'");

                    g = Convert.ToDouble(f_shgc[0][0]) * Convert.ToDouble(Old[0][5]) * g;
                    τD65_SNA = Convert.ToDouble(f_τ[0][0]) * Convert.ToDouble(Old[0][6]) * τD65_SNA;

                }
            }
            else if (Type == "내부덧댐")
            {
                if (OldWindow != null && GlassName != null)
                {
                    Old = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "번호,창호명칭,Type,LE_CL_V,유리열관류율,태양열취득률,빛투과율,창호열관류율", "창호명칭 = '" + OldWindow + "'");
                    String 조합구성 = LE_CL_V + "+" + Old[0][3];
                    f_shgc = Program.DB.getValue(DB.type.BaseDB, "이중창보정계수", "계수", "조합구성 = '" + 조합구성 + "' AND 보정유형 = '태양열취득률'");
                    f_τ = Program.DB.getValue(DB.type.BaseDB, "이중창보정계수", "계수", "조합구성 = '" + 조합구성 + "' AND 보정유형 = '빛투과율'");
                    g = Convert.ToDouble(f_shgc[0][0]) * Convert.ToDouble(Old[0][5]) * g;
                    τD65_SNA = Convert.ToDouble(f_τ[0][0]) * Convert.ToDouble(Old[0][6]) * τD65_SNA;
                }
            }
            else
            {
                g = g;
                τD65_SNA = τD65_SNA;
            }

            g_textBox.Text = String.Format("{0:F3}", g);
            τD65_SNA_textBox.Text = String.Format("{0:F3}", τD65_SNA);

        }

        private void Load_WindowType_image(String Type)
        {

            string[][] Image = Program.DB.getValue(DB.type.BaseDB, "창호구조유형이미지", "이미지", "구조유형 = '" + Type + "'");

            WindowType_pictureBox.Load(Program.gPath + Image[0][0]);
            WindowType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

        }

        private void UwMethod_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UwMethod = Uw_comboBox.SelectedItem.ToString();

            if (UwMethod == "계산")
            {
                Frame_label.Visible = true;
                Frame_comboBox.Visible = true;
                FrameName_textBox.Visible = true;
                FrameDB_button.Visible = true;

                Spacer_label.Visible = true;
                Spacer_button.Visible = true;
                SpacerName_textBox.Visible = true;

                Ug_label.Visible = true;
                Ug_textBox.Visible = true;
                Ug_unit_label.Visible = true;

                Psi_fix_label.Visible = true;
                Psi_open_label.Visible = true;
                Psi_fix_unit_label.Visible = true;
                Psi_open_unit_label.Visible = true;
                Psi_g_fix_textBox.Visible = true;
                Psi_g_open_textBox.Visible = true;


                Uw2_label.Visible = false;
                Uw2_unit_label.Visible = false;
                Uw2_textBox.Visible = false;
                Uw2_textBox.Enabled = false;
                Uw2_textBox.BorderStyle = BorderStyle.None;
            }
            else if (UwMethod == "법규")
            {
                Frame_label.Visible = false;
                Frame_comboBox.Visible = false;
                FrameName_textBox.Visible = false;
                FrameDB_button.Visible = false;

                Spacer_label.Visible = false;
                Spacer_button.Visible = false;
                SpacerName_textBox.Visible = false;

                Ug_label.Visible = false;
                Ug_textBox.Visible = false;
                Ug_unit_label.Visible = false;

                Psi_fix_label.Visible = false;
                Psi_open_label.Visible = false;
                Psi_fix_unit_label.Visible = false;
                Psi_open_unit_label.Visible = false;
                Psi_g_fix_textBox.Visible = false;
                Psi_g_open_textBox.Visible = false;

                Uw2_label.Visible = true;
                Uw2_unit_label.Visible = true;
                Uw2_textBox.Visible = true;
                Uw2_textBox.Enabled = false;
                Uw2_textBox.BorderStyle = BorderStyle.None;
            }
            else if (UwMethod == "진단")
            {
                Frame_label.Visible = false;
                Frame_comboBox.Visible = false;
                FrameName_textBox.Visible = false;
                FrameDB_button.Visible = false;

                Spacer_label.Visible = false;
                Spacer_button.Visible = false;
                SpacerName_textBox.Visible = false;

                Ug_label.Visible = false;
                Ug_textBox.Visible = false;
                Ug_unit_label.Visible = false;

                Psi_fix_label.Visible = false;
                Psi_open_label.Visible = false;
                Psi_fix_unit_label.Visible = false;
                Psi_open_unit_label.Visible = false;
                Psi_g_fix_textBox.Visible = false;
                Psi_g_open_textBox.Visible = false;

                Uw2_label.Visible = true;
                Uw2_unit_label.Visible = true;
                Uw2_textBox.Visible = true;
                Uw2_textBox.Text = string.Empty;
                Uw2_textBox.Enabled = true;
                Uw2_textBox.BorderStyle = BorderStyle.FixedSingle;
            }

            Rule_Uw();

        }


        private void Uw2_textBox_TextChanged(object sender, EventArgs e)
        {
            if (UwMethod == "진단" && Uw2_textBox.Text != string.Empty)
            {
                Uw = Convert.ToDouble(Uw2_textBox.Text);
            }
        }

        private void DiIndil_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DiIndi = DiIndi_comboBox.SelectedItem.ToString();
            Rule_Uw();
        }

        private void ImportSize_button_Click(object sender, EventArgs e)
        {
            Window_ImportSize window_importsize_form = new Window_ImportSize(WinNum, WindowName);

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
            Sub_Uw.Clear();
            Sub_dUinst.Clear();
            Sub_Uw_inst.Clear();

            // Program.DB.deleteValue(DB.type.ProjDB, "SubWindow", "상위창호번호 = '" + WinNum + "'");
            Size = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "번호,명칭,상위창호번호,창호면적,창호너비,창호높이,고정유리면적,개폐유리면적,개폐프레임면적,고정프레임면적,중간프레임면적,고정유리둘레길이,개폐유리둘레길이", "상위창호번호 = '" + WinNum + "'");
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

                    Sub_Uw.Add(Calc_Uw(Convert.ToDouble(Sub_Area[i]), Convert.ToDouble(Sub_Width[i]), Convert.ToDouble(Sub_Height[i]), Convert.ToDouble(Sub_Ag_fix[i]), Convert.ToDouble(Sub_Ag_open[i]), Convert.ToDouble(Sub_Af_open[i]), Convert.ToDouble(Sub_Af_fix[i]), Convert.ToDouble(Sub_Af_btw[i]), Convert.ToDouble(Sub_Lg_fix[i]), Convert.ToDouble(Sub_Lg_open[i])));
                    if (UwMethod == "계산")
                    {
                        Sub_dUinst.Add(Calc_dUinst(Convert.ToDouble(Sub_Uw[i]), Convert.ToDouble(Sub_Area[i]), Convert.ToDouble(Sub_Width[i]), Convert.ToDouble(Sub_Height[i])));
                        Sub_Uw_inst.Add(Convert.ToDouble(Sub_Uw[i]) + Convert.ToDouble(Sub_dUinst[i]));
                    }
                    else
                    {
                        Sub_dUinst.Add(Calc_dUinst(Uw, Convert.ToDouble(Sub_Area[i]), Convert.ToDouble(Sub_Width[i]), Convert.ToDouble(Sub_Height[i])));
                        Sub_Uw_inst.Add(Uw + Convert.ToDouble(Sub_dUinst[i]));
                    }
                    Sub_Uw[i] = Calc_Uw_AdditionalWindow(Sub_Uw[i]);
                    Program.DB.executeSQL(DB.type.ProjDB, "UPDATE SubWindow SET 창호열관류율  ='" + Sub_Uw[i] + "', 설치열교가산치 = '" + Sub_dUinst[i] + "', 창호유효열관류율 = '" + Sub_Uw_inst[i] + "' WHERE  번호 = '" + Size[i][0] + "'");
                }
                Size_textBox.Text = Size.Length.ToString() + "개 치수 적용";
            }


        }

        private void Frame_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FrameType = Frame_comboBox.SelectedItem.ToString();
            switch (FrameType)
            {
                case "단창_SL":
                    SingleDoubleType = "단창";
                    break;

                case "단창_T/T":
                    SingleDoubleType = "단창";
                    break;

                case "이중창_SL":
                    SingleDoubleType = "이중창";
                    break;
            }
            //창호 유형 다시 선택했을 경우 
            try
            {
                if (check_FrameType != null)
                {
                    if (FrameType != check_FrameType)
                    {
                        MessageBox.Show("프레임, 유리, 간봉, 설치열교를 다시 선택하세요.");
                        FrameName = "";
                        FrameMaterial = "";
                        FrameName_textBox.Text = "";
                        GlassName = "";
                        GlassName_textBox.Text = "";
                        SpacerName = "";
                        SpacerName_textBox.Text = "";
                        InstallName = "";
                        Install_textBox.Text = "";
                        Uf_open_textBox.Text = "";
                        Uf_fix_textBox.Text = "";
                        Uf_btw_textBox.Text = "";
                        df_open_textBox.Text = "";
                        df_fix_textBox.Text = "";
                        df_btw_textBox.Text = "";
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
                Window_FrameDB window_frameDB_form = new Window_FrameDB(FrameType, SingleDoubleType);

                DialogResult result = window_frameDB_form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    FrameName = window_frameDB_form.Select_WindowFrame[1];
                    FrameMaterial = window_frameDB_form.Select_WindowFrame[4];
                    switch (FrameMaterial)
                    {
                        case "플라스틱":
                            FrameMaterial = FrameMaterial;
                            break;

                        case "금속":
                            FrameMaterial = FrameMaterial;
                            break;

                        case "금속_단열바":
                            FrameMaterial = "금속";
                            break;
                    }
                    FrameName_textBox.Text = FrameName;
                    FrameMaterial_textBox.Text = FrameMaterial;
                    tabControl1.SelectedTab = tabControl1.TabPages["Frame_tabPage"];
                    check_FrameType = window_frameDB_form.Select_WindowFrame[3];
                    Uf_open = Convert.ToDouble(window_frameDB_form.Select_WindowFrame[5]);
                    Uf_fix = Convert.ToDouble(window_frameDB_form.Select_WindowFrame[6]);
                    Uf_btw = Convert.ToDouble(window_frameDB_form.Select_WindowFrame[7]);
                    df_open = Convert.ToDouble(window_frameDB_form.Select_WindowFrame[8]);
                    df_fix = Convert.ToDouble(window_frameDB_form.Select_WindowFrame[9]);
                    df_btw = Convert.ToDouble(window_frameDB_form.Select_WindowFrame[10]);
                    Uf_open_textBox.Text = String.Format("{0:F2}", Uf_open);
                    Uf_fix_textBox.Text = String.Format("{0:F2}", Uf_fix);
                    Uf_btw_textBox.Text = String.Format("{0:F2}", Uf_btw);
                    df_open_textBox.Text = String.Format("{0:F2}", df_open);
                    df_fix_textBox.Text = String.Format("{0:F2}", df_fix);
                    df_btw_textBox.Text = String.Format("{0:F2}", df_btw);


                    string[][] Image = Program.DB.getValue(DB.type.BaseDB, "창호프레임이미지", "이미지", "유형1 = '" + FrameType + "' AND 유형2 = '기본형' AND 재료 = '" + FrameMaterial + "'");

                    WindowFrame_pictureBox.Load(Program.gPath + Image[0][0]);
                    WindowFrame_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;


                }
            }

            //프레임종류 다시 선택했을 경우 
            try
            {
                if (check_SingleDoubleType != null && check_FrameMaterial != null)
                {
                    if (SingleDoubleType != check_SingleDoubleType || FrameMaterial != check_FrameMaterial)
                    {
                        MessageBox.Show("간봉과 설치열교을 다시 선택하세요.");
                        SpacerName = "";
                        SpacerName_textBox.Text = "";
                        Psi_g_fix_textBox.Text = "";
                        Psi_g_open_textBox.Text = "";
                        InstallName = "";
                        Install_textBox.Text = "";
                    }
                }
            }
            catch { }

        }

        private void Glass_button_Click(object sender, EventArgs e)
        {
            if (SingleDoubleType == "이중창")
            {

                Window_DoubleGlassDB window_doubleglassDB_form = new Window_DoubleGlassDB();
                DialogResult result = window_doubleglassDB_form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    GlassName = window_doubleglassDB_form.Select_WindowGlass[1];
                    GlassName_textBox.Text = GlassName;
                    LE_CL_V = window_doubleglassDB_form.Select_WindowGlass[5];
                    Ug = Convert.ToDouble(window_doubleglassDB_form.Select_WindowGlass[6]);
                    Ug_textBox.Text = String.Format("{0:F3}", Ug);
                    g = Convert.ToDouble(window_doubleglassDB_form.Select_WindowGlass[7]);
                    g_textBox.Text = String.Format("{0:F3}", g);
                    τD65_SNA = Convert.ToDouble(window_doubleglassDB_form.Select_WindowGlass[8]);
                    τD65_SNA_textBox.Text = String.Format("{0:F3}", τD65_SNA);
                    Calc_g_AdditionalWindow(); //덧댐일 경우 
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
                            Psi_g_fix_textBox.Text = "";
                            Psi_g_open_textBox.Text = "";
                        }

                    }

                }
                catch { }

            }
            else
            {
                Window_GlassDB window_glassDB_form = new Window_GlassDB();
                DialogResult result = window_glassDB_form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    GlassName = window_glassDB_form.Select_WindowGlass[1];
                    GlassName_textBox.Text = GlassName;
                    LE_CL_V = window_glassDB_form.Select_WindowGlass[5];
                    Ug = Convert.ToDouble(window_glassDB_form.Select_WindowGlass[6]);
                    Ug_textBox.Text = String.Format("{0:F3}", Ug);
                    g = Convert.ToDouble(window_glassDB_form.Select_WindowGlass[7]);
                    g_textBox.Text = String.Format("{0:F3}", g);
                    τD65_SNA = Convert.ToDouble(window_glassDB_form.Select_WindowGlass[8]);
                    τD65_SNA_textBox.Text = String.Format("{0:F3}", τD65_SNA);
                    Calc_g_AdditionalWindow();
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
                            Psi_g_fix_textBox.Text = "";
                            Psi_g_open_textBox.Text = "";
                        }

                    }

                }
                catch { }
            }

        }

        private void Spacer_button_Click(object sender, EventArgs e)
        {
            if (SingleDoubleType == null || FrameMaterial == null)
            {
                MessageBox.Show("프레임부터 선택하세요.");
            }
            else if (LE_CL_V == null)
            {
                MessageBox.Show("유리부터 선택하세요.");
            }
            else
            {
                Window_SpacerDB window_spacerDB_form = new Window_SpacerDB(SingleDoubleType, FrameMaterial, LE_CL_V);
                DialogResult result = window_spacerDB_form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    check_FrameMaterial = window_spacerDB_form.Select_WindowSpacer[3];
                    check_SingleDoubleType = window_spacerDB_form.Select_WindowSpacer[4];
                    check_LE_CL_V = window_spacerDB_form.Select_WindowSpacer[9];
                    SpacerName = window_spacerDB_form.Select_WindowSpacer[2];
                    SpacerName_textBox.Text = SpacerName;
                    if (LE_CL_V.Contains("LE"))
                    {
                        Psi_g_fix = Convert.ToDouble(window_spacerDB_form.Select_WindowSpacer[7]);
                        Psi_g_open = Convert.ToDouble(window_spacerDB_form.Select_WindowSpacer[8]);
                    }
                    else
                    {
                        Psi_g_fix = Convert.ToDouble(window_spacerDB_form.Select_WindowSpacer[5]);
                        Psi_g_open = Convert.ToDouble(window_spacerDB_form.Select_WindowSpacer[6]);
                    }
                    Psi_g_fix_textBox.Text = String.Format("{0:F3}", Psi_g_fix);
                    Psi_g_open_textBox.Text = String.Format("{0:F3}", Psi_g_open);
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
            if (UwMethod == "계산")
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
            if (UwMethod == "법규")
            {
                String[][] Uvalue = Program.DB.getValue(DB.type.BaseDB, "법규열관류율", "열관류율", "구조체 = '창호' And 시기 = '2018.09' AND  지역 = '중부1' AND 직접간접 =  '" + DiIndi + "'");
                Uw = Convert.ToDouble(Uvalue[0][0]);
                Uw2_textBox.Text = String.Format("{0:F3}", Uw);
            }
        }

        public double Calc_Uw(double Area, double Width, double Height, double Ag_fix, double Ag_open, double Af_open, double Af_fix, double Af_btw, double Lg_fix, double Lg_open)
        {
            double Uwcalc;
            if (UwMethod == "계산" && Ug != 0 && Uf_fix != 0 && Psi_g_fix != 0 && Area != 0)
            {
                Uwcalc = (Ug * (Ag_fix + Ag_open) + (Uf_open * Af_open) + (Uf_fix * Af_fix) + (Uf_btw * Af_btw) + (Psi_g_fix * Lg_fix) + (Psi_g_open * Lg_open)) / Area;
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
            if (WindowName == null)
            {
                MessageBox.Show("창호 명칭을 입력하세요.");
            }
            else if (Type == null)
            {
                MessageBox.Show("창호 리모델링 유형을 선택하세요.");
            }
            else if (GlassName == null)
            {
                MessageBox.Show("유리를 선택하세요.");
            }
            else if (InstallName == null)
            {
                MessageBox.Show("설치열교를 선택하세요.");
            }
            else if (UwMethod == "계산")
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
            Program.DB.setValue(DB.type.ProjDB, "ConstructionWindow", "번호,창호명칭,Type,기존창호,Uw적용방법,직접간접,프레임유형,이중단창,프레임재료,프레임종류,유리종류,간봉종류,설치유형,설치종류,LE_CL_V," +
                  "유리열관류율,태양열취득률,빛투과율,고정유리선형열관류율,개폐유리선형열관류율," +
                  "개폐부프레임열관류율,고정부프레임열관류율,중간바프레임열관류율,개폐부프레임두께,고정부프레임두께,중간바프레임두께," +
                  "상부설치열관류율,측면설치열관류율,하부설치열관류율," +
                  "창호열관류율",
                "'" + WinNum_textBox.Text + "','" + WindowName + "','" + Type + "','" + OldWindow + "','" + UwMethod + "','" + DiIndi + "','" + FrameType + "','" + SingleDoubleType + "','" + FrameMaterial + "','" + FrameName + "','" + GlassName + "','" + SpacerName + "','" + InstallType + "','" + InstallName + "','" + LE_CL_V + "','" +
                Ug.ToString() + "','" + g.ToString() + "','" + τD65_SNA.ToString() + "','" + Psi_g_fix.ToString() + "','" + Psi_g_open.ToString() + "','" +
                Uf_open.ToString() + "','" + Uf_fix.ToString() + "','" + Uf_btw.ToString() + "','" + df_open.ToString() + "','" + df_fix.ToString() + "','" + df_btw.ToString() + "','" +
                Psi_InstallTop.ToString() + "','" + Psi_InstallSide.ToString() + "','" + Psi_InstallButtom.ToString() + "','" +
                Uw.ToString()
                + "'", "번호");


            Size = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "번호", "상위창호번호 = '" + WinNum + "'");


            ImportSize();
            this.DialogResult = DialogResult.OK;
            this.Hide();
            Program.getMenuForm().DoLoadForm(29, OnLoadListProc);
        }


        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            try
            {
                WinNum_textBox.Text = ID;
                WinNum = ID;
                String[][] Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "번호,창호명칭,Type,기존창호,Uw적용방법,직접간접,프레임유형,이중단창,프레임재료,프레임종류,유리종류,간봉종류,설치유형,설치종류,LE_CL_V," +
                  "유리열관류율,태양열취득률,빛투과율,고정유리선형열관류율,개폐유리선형열관류율," +
                  "상부설치열관류율,측면설치열관류율,하부설치열관류율," +
                  "창호열관류율," +
                  "개폐부프레임열관류율,고정부프레임열관류율,중간바프레임열관류율,개폐부프레임두께,고정부프레임두께,중간바프레임두께"
                    , "번호 = '" + ID + "'");
                Name_textBox.Text = Load[0][1];
                Type = Load[0][2];
                switch (Type)
                {
                    case "기존창호":
                        radioButton1.Checked = true;
                        break;

                    case "신규":
                        radioButton2.Checked = true;
                        break;

                    case "철거 후 신규":
                        radioButton3.Checked = true;
                        break; ;

                    case "외부(커튼월)덧댐":
                        radioButton4.Checked = true;
                        break;

                    case "내부덧댐":
                        radioButton5.Checked = true;
                        break;
                }
                AdditionalWindow_comboBox.SelectedItem = Load[0][3];
                Uw_comboBox.SelectedItem = Load[0][4];
                DiIndi_comboBox.SelectedItem = Load[0][5];
                Frame_comboBox.SelectedItem = Load[0][6];
                FrameType = Load[0][6];
                check_FrameType = Load[0][6];
                SingleDoubleType = Load[0][7];
                check_SingleDoubleType = Load[0][7];
                FrameMaterial = Load[0][8];
                check_FrameMaterial = Load[0][8];
                FrameMaterial_textBox.Text = Load[0][8];
                FrameName = Load[0][9];
                FrameName_textBox.Text = Load[0][9];
                GlassName = Load[0][10];
                GlassName_textBox.Text = Load[0][10];
                SpacerName = Load[0][11];
                SpacerName_textBox.Text = Load[0][11];
                InstallType = Load[0][12];
                check_InstallType = Load[0][12];
                Install_comboBox.SelectedItem = Load[0][12];
                InstallName = Load[0][13];
                Install_textBox.Text = Load[0][13];
                LE_CL_V = Load[0][14];
                check_LE_CL_V = Load[0][14];

                Ug = Convert.ToDouble(Load[0][15]);
                Ug_textBox.Text = Load[0][15];

                g = Convert.ToDouble(Load[0][16]);
                g_textBox.Text = String.Format("{0:F3}", g);

                τD65_SNA = Convert.ToDouble(Load[0][17]);
                τD65_SNA_textBox.Text = String.Format("{0:F3}", τD65_SNA);

                Psi_g_fix = Convert.ToDouble(Load[0][18]);
                Psi_g_fix_textBox.Text = string.Format("{0:F3}", Psi_g_fix);

                Psi_g_open = Convert.ToDouble(Load[0][19]);
                Psi_g_open_textBox.Text = String.Format("{0:F3}", Psi_g_open);

                Psi_InstallTop = Convert.ToDouble(Load[0][20]);
                Psi_InstallTop_textBox.Text = String.Format("{0:F3}", Psi_InstallTop);

                Psi_InstallSide = Convert.ToDouble(Load[0][21]);
                Psi_InstallSide_textBox.Text = String.Format("{0:F3}", Psi_InstallSide);


                Psi_InstallButtom = Convert.ToDouble(Load[0][22]);
                Psi_InstallButtom_textBox.Text = String.Format("{0:F3}", Psi_InstallButtom);

                Uw = Convert.ToDouble(Load[0][23]);
                if (UwMethod == "계산")
                {

                }
                else
                {
                    Uw2_textBox.Text = String.Format("{0:F3}", Uw);
                }
                Uf_open = Convert.ToDouble(Load[0][24]);
                Uf_open_textBox.Text = String.Format("{0:F2}", Uf_open);
                Uf_fix = Convert.ToDouble(Load[0][25]);
                Uf_fix_textBox.Text = String.Format("{0:F2}", Uf_fix);
                Uf_btw = Convert.ToDouble(Load[0][26]);
                Uf_btw_textBox.Text = String.Format("{0:F2}", Uf_btw);
                df_open = Convert.ToDouble(Load[0][27]);
                df_open_textBox.Text = String.Format("{0:F2}", df_open);
                df_fix = Convert.ToDouble(Load[0][28]);
                df_fix_textBox.Text = String.Format("{0:F2}", df_fix);
                df_btw = Convert.ToDouble(Load[0][29]);
                df_btw_textBox.Text = String.Format("{0:F2}", df_btw);

                ImportSize();

                string[][] Image1 = Program.DB.getValue(DB.type.BaseDB, "창호구조유형이미지", "이미지", "구조유형 = '" + Type + "'");
                WindowType_pictureBox.Load(Program.gPath + Image1[0][0]);
                WindowType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                string[][] Image2 = Program.DB.getValue(DB.type.BaseDB, "창호프레임이미지", "이미지", "유형1 = '" + FrameType + "' AND 유형2 = '기본형' AND 재료 = '" + FrameMaterial + "'");
                WindowFrame_pictureBox.Load(Program.gPath + Image2[0][0]);
                WindowFrame_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                string[][] Image3 = Program.DB.getValue(DB.type.BaseDB, "창호설치열교이미지", "이미지열교유형", "구분1 = '" + InstallType + "' AND 구분2 = '" + FrameMaterial + "' AND 구분3 = '" + SingleDoubleType + "' AND 구분4 = '" + InstallName + "'");
                WindowInstall_pictureBox.Load(Program.gPath + Image3[0][0]);
                WindowInstall_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;


            }
            catch { }
        }
        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            WinNum_textBox.Text = ID;
            WinNum = ID;
        }

        public void CopyForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            WinNum_textBox.Text = ID;
            WinNum = ID;

            if (Name_textBox.Text != "")
            {
                WindowName = Name_textBox.Text + "_복사";
                Name_textBox.Text = WindowName;
                Save();
            }

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void Frame_label_Click(object sender, EventArgs e)
        {
        }

        private void label11_Click(object sender, EventArgs e)
        {
        }

        private void GlassName_textBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void Spacer_label_Click(object sender, EventArgs e)
        {
        }

        private void SpacerName_textBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void label16_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void Size_textBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }
    }
}
