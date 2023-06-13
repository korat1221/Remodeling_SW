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

namespace main.subcontents.ConstructionWindow
{
    public partial class SubWindow : Form
    {
        private String WinNum;
        String WindowName, Type, OldWindow, UwMethod, DiIndi, FrameType, SingleDoubleType, FrameMaterial, FrameName, GlassName, SpacerName, InstallType, InstallName, LE_CL_V;
        String check_FrameType, check_SingleDoubleType, check_FrameMaterial, check_LE_CL_V, check_InstallType;
        String[][] Size;
        double Ug, g, τD65_SNA, Psi_g_fix, Psi_g_open, Uw, Uw_inst, dUinst;// dUinst는 열교가산치, Uw_inst는 유효열관류율(창호열관류율+열교가산치)
        double Uf_open, Uf_fix, Uf_btw, df_open, df_fix, df_btw;
        double Psi_InstallTop, Psi_InstallSide, Psi_InstallButtom;
        double Area, Width, Height, Ag_fix, Ag_open, Af_open, Af_fix, Af_btw, Lg_fix, Lg_open;
        String[][] Old; String[][] f_shgc; String[][] f_τ;

        public SubWindow()
        {
            InitializeComponent();
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
                    Load_AdditionalWindow(Type);
                    break;

                case "신규":
                    Uw_comboBox.Items.Add("계산");
                    Uw_comboBox.Items.Add("법규");
                    AdditionalWindow_textBox.Visible = false;
                    AdditionalWindow_comboBox.Visible = false;
                    Load_AdditionalWindow(Type);
                    break;

                case "철거 후 신규":
                    Uw_comboBox.Items.Add("계산");
                    Uw_comboBox.Items.Add("법규");
                    AdditionalWindow_textBox.Visible = false;
                    AdditionalWindow_comboBox.Visible = false;
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
            OldWindow = AdditionalWindow_comboBox.SelectedItem.ToString(); ;
            Calc_AdditionalWindow();
        }

        private void Calc_AdditionalWindow()
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
                    Uw = 1 / (0.019 + 1 / Convert.ToDouble(Old[0][7]) + 1 / Uw);

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
                    Uw = 1 / (0.019 + 1 / Convert.ToDouble(Old[0][7]) + 1 / Uw);
                }
            }
            else
            {
                g = g;
                τD65_SNA = τD65_SNA;
                Uw = Uw;
            }

            g_textBox.Text = String.Format("{0:F3}", g);
            τD65_SNA_textBox.Text = String.Format("{0:F3}", τD65_SNA);
            Uw_textBox.Text = String.Format("{0:F3}", Uw);

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

                Spacer_label.Visible = true;
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

                Uw_textBox.Visible = true;
                Uw_label.Visible = true;
                Uw_unit_label.Visible = true;

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

                Spacer_label.Visible = false;
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

                Uw_textBox.Visible = false;
                Uw_label.Visible = false;
                Uw_unit_label.Visible = false;

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

                Spacer_label.Visible = false;
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

                Uw_textBox.Visible = false;
                Uw_label.Visible = false;
                Uw_unit_label.Visible = false;

                Uw2_label.Visible = true;
                Uw2_unit_label.Visible = true;
                Uw2_textBox.Visible = true;
                Uw2_textBox.Text = string.Empty;
                Uw2_textBox.Enabled = true;
                Uw2_textBox.BorderStyle = BorderStyle.FixedSingle;
            }

            Calc_Uw();
            Rule_Uw();
            Calc_AdditionalWindow();
            Calc_dUinst();

        }


        private void Uw2_textBox_TextChanged(object sender, EventArgs e)
        {
            if (UwMethod == "진단" && Uw2_textBox.Text != string.Empty)
            {
                Uw = Convert.ToDouble(Uw2_textBox.Text);
                Calc_dUinst();
            }
        }

        private void DiIndil_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DiIndi = DiIndi_comboBox.SelectedItem.ToString();
            Rule_Uw();
            Calc_dUinst();
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


        public void Calc_Uw()
        {
            if (UwMethod == "계산" && Ug != 0 && Uf_fix != 0 && Psi_g_fix != 0 && Area != 0)
            {
                Uw = (Ug * (Ag_fix + Ag_open) + (Uf_open * Af_open) + (Uf_fix * Af_fix) + (Uf_btw * Af_btw) + (Psi_g_fix * Lg_fix) + (Psi_g_open * Lg_open)) / Area;
                if (Uw.Equals(double.NaN) == false)
                {
                    Uw_textBox.Text = String.Format("{0:F3}", Uw);
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
        public void Calc_dUinst()
        {
            if (Uw != 0 && Area != 0)
            {
                dUinst = ((Psi_InstallTop * Width) + (Psi_InstallButtom * Width) + (Psi_InstallSide * Height * 2)) / Area;
                if (dUinst.Equals(double.NaN) == false)
                {
                    dUinst_textBox.Text = String.Format("{0:F3}", dUinst);
                }

                if (dUinst.Equals(double.NaN) == false && Uw.Equals(double.NaN) == false)
                {
                    Uw_inst = dUinst + Uw;
                    Uw_inst_textBox.Text = String.Format("{0:F3}", Uw_inst);
                }
            }

        }

        private void Previous_button_Click(object sender, EventArgs e)
        {
                this.DialogResult = DialogResult.OK;
                this.Hide();
                Program.getMenuForm().DoLoadForm(29, OnLoadListProc);
            
        }

        public static bool OnLoadListProc(Form form)
        {
            List_ConstructionWindow f = (List_ConstructionWindow)form;

            f.load_List();

            return true;
        }


        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            try
            {
                WinNum_textBox.Text = ID;
                WinNum = ID;

                String[][] SubLoad = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "번호,명칭,상위창호번호,창호면적,창호너비,창호높이,고정유리면적,개폐유리면적,개폐프레임면적,고정프레임면적,중간프레임면적,고정유리둘레길이,개폐유리둘레길이," +
                    "창호열관류율,설치열교가산치,창호유효열관류율"
                    , "번호 = '" + ID + "'");

                String[][] MainLoad = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "번호,창호명칭,Type,기존창호,Uw적용방법,직접간접,프레임유형,이중단창,프레임재료,프레임종류,유리종류,간봉종류,설치유형,설치종류,LE_CL_V," +
                  "유리열관류율,태양열취득률,빛투과율,고정유리선형열관류율,개폐유리선형열관류율," +
                  "상부설치열관류율,측면설치열관류율,하부설치열관류율," +
                  "창호열관류율," +
                  "개폐부프레임열관류율,고정부프레임열관류율,중간바프레임열관류율,개폐부프레임두께,고정부프레임두께,중간바프레임두께"
                    , "번호 = '" + SubLoad[0][2] + "'");
                Name_textBox.Text = SubLoad[0][1];
                Type = MainLoad[0][2];
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
                AdditionalWindow_comboBox.SelectedItem = MainLoad[0][3];
                Uw_comboBox.SelectedItem = MainLoad[0][4];
                DiIndi_comboBox.SelectedItem = MainLoad[0][5];
                Frame_comboBox.SelectedItem = MainLoad[0][6];
                FrameType = MainLoad[0][6];
                check_FrameType = MainLoad[0][6];
                SingleDoubleType = MainLoad[0][7];
                check_SingleDoubleType = MainLoad[0][7];
                FrameMaterial = MainLoad[0][8];
                check_FrameMaterial = MainLoad[0][8];
                FrameMaterial_textBox.Text = MainLoad[0][8];
                FrameName = MainLoad[0][9];
                FrameName_textBox.Text = MainLoad[0][9];
                GlassName = MainLoad[0][10];
                GlassName_textBox.Text = MainLoad[0][10];
                SpacerName = MainLoad[0][11];
                SpacerName_textBox.Text = MainLoad[0][11];
                InstallType = MainLoad[0][12];
                check_InstallType = MainLoad[0][12];
                Install_comboBox.SelectedItem = MainLoad[0][12];
                InstallName = MainLoad[0][13];
                Install_textBox.Text = MainLoad[0][13];
                LE_CL_V = MainLoad[0][14];
                check_LE_CL_V = MainLoad[0][14];

                Ug = Convert.ToDouble(MainLoad[0][15]);
                Ug_textBox.Text = MainLoad[0][15];

                g = Convert.ToDouble(MainLoad[0][16]);
                g_textBox.Text = String.Format("{0:F3}", g);

                τD65_SNA = Convert.ToDouble(MainLoad[0][17]);
                τD65_SNA_textBox.Text = String.Format("{0:F3}", τD65_SNA);

                Psi_g_fix = Convert.ToDouble(MainLoad[0][18]);
                Psi_g_fix_textBox.Text = string.Format("{0:F3}", Psi_g_fix);

                Psi_g_open = Convert.ToDouble(MainLoad[0][19]);
                Psi_g_open_textBox.Text = String.Format("{0:F3}", Psi_g_open);

                Psi_InstallTop = Convert.ToDouble(MainLoad[0][20]);
                Psi_InstallTop_textBox.Text = String.Format("{0:F3}", Psi_InstallTop);

                Psi_InstallSide = Convert.ToDouble(MainLoad[0][21]);
                Psi_InstallSide_textBox.Text = String.Format("{0:F3}", Psi_InstallSide);


                Psi_InstallButtom = Convert.ToDouble(MainLoad[0][22]);
                Psi_InstallButtom_textBox.Text = String.Format("{0:F3}", Psi_InstallButtom);

                Uw = Convert.ToDouble(SubLoad[0][13]);
                if (UwMethod == "계산")
                {
                    Uw_textBox.Text = String.Format("{0:F3}", Uw);
                }
                else
                {
                    Uw2_textBox.Text = String.Format("{0:F3}", Uw);
                }
                dUinst = Convert.ToDouble(SubLoad[0][14]);
                dUinst_textBox.Text = String.Format("{0:F3}", dUinst);

                Uw_inst = Convert.ToDouble(SubLoad[0][15]);
                Uw_inst_textBox.Text = String.Format("{0:F3}", Uw_inst);

                Uf_open = Convert.ToDouble(MainLoad[0][24]);
                Uf_open_textBox.Text = String.Format("{0:F2}", Uf_open);
                Uf_fix = Convert.ToDouble(MainLoad[0][25]);
                Uf_fix_textBox.Text = String.Format("{0:F2}", Uf_fix);
                Uf_btw = Convert.ToDouble(MainLoad[0][26]);
                Uf_btw_textBox.Text = String.Format("{0:F2}", Uf_btw);
                df_open = Convert.ToDouble(MainLoad[0][27]);
                df_open_textBox.Text = String.Format("{0:F2}", df_open);
                df_fix = Convert.ToDouble(MainLoad[0][28]);
                df_fix_textBox.Text = String.Format("{0:F2}", df_fix);
                df_btw = Convert.ToDouble(MainLoad[0][29]);
                df_btw_textBox.Text = String.Format("{0:F2}", df_btw);

                Area = Convert.ToDouble(SubLoad[0][3]);
                Area_textBox.Text = String.Format("{0:F2}", Area);
                Width = Convert.ToDouble(SubLoad[0][4]);
                Width_textBox.Text = String.Format("{0:F2}", Width);
                Height = Convert.ToDouble(SubLoad[0][5]);
                Height_textBox.Text = String.Format("{0:F2}", Height);
                Ag_fix = Convert.ToDouble(SubLoad[0][6]);
                Ag_fix_textBox.Text = String.Format("{0:F2}", Ag_fix);
                Ag_open = Convert.ToDouble(SubLoad[0][7]);
                Ag_open_textBox.Text = String.Format("{0:F2}", Ag_open);
                Af_open = Convert.ToDouble(SubLoad[0][8]);
                Af_open_textBox.Text = String.Format("{0:F2}", Af_open);
                Af_fix = Convert.ToDouble(SubLoad[0][9]);
                Af_fix_textBox.Text = String.Format("{0:F2}", Af_fix);
                Af_btw = Convert.ToDouble(SubLoad[0][10]);
                Af_btw_textBox.Text = String.Format("{0:F2}", Af_btw);
                Lg_fix = Convert.ToDouble(SubLoad[0][11]);
                Lg_fix_textBox.Text = String.Format("{0:F2}", Lg_fix);
                Lg_open = Convert.ToDouble(SubLoad[0][12]);
                Lg_open_textBox.Text = String.Format("{0:F2}", Lg_open);

                d_InstallTop_textBox.Text = String.Format("{0:F2}", Width);
                d_InstallButtom_textBox.Text = String.Format("{0:F2}", Width);
                d_InstallSide_textBox.Text = String.Format("{0:F2}", (Height * 2));

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


    }
}
