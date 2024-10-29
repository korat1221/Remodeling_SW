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
            InitializeComponent(); this.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular);
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '창호'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

            //직접간접 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, DiIndi_comboBox, "창호", "실외조건", "1");
            //프레임종류 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, Frame_comboBox, "창호", "프레임시스템", "1");
            //설치위치 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, Install_comboBox, "창호", "구조", "1");
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
                WindowName = Name_textBox.Text.ToString();
            }
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
        }

        private void AdditionalWindow_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView? item = AdditionalWindow_comboBox.SelectedItem as DataRowView;
            if (item != null)
            {
                OldWindow = AdditionalWindow_comboBox.SelectedItem.ToString(); ;
                Calc_AdditionalWindow();
            }
        }

        private void Calc_AdditionalWindow()
        {
            if (Type == "외부(커튼월)덧댐") //추후 커튼월 db로 고쳐야 함
            {
                if (OldWindow != null && GlassName != null)
                {
                    Old = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "번호,창호명칭,Type,LE_CL_V,유리열관류율,태양열취득률,빛투과율,창호열관류율", "창호명칭 = '" + OldWindow + "'");
                    if (Old.Length > 0)
                    {
                        String 조합구성 = LE_CL_V + "+" + Old[0][3];
                        f_shgc = Program.DB.getValue(DB.type.BaseDB_HCneed, "이중창보정계수", "계수", "조합구성 = '" + 조합구성 + "' AND 보정유형 = '태양열취득률'");
                        f_τ = Program.DB.getValue(DB.type.BaseDB_HCneed, "이중창보정계수", "계수", "조합구성 = '" + 조합구성 + "' AND 보정유형 = '빛투과율'");
                        if (f_shgc.Length > 0)
                        { g = Convert.ToDouble(f_shgc[0][0]) * Convert.ToDouble(Old[0][5]) * g; }
                        if (f_τ.Length > 0)
                        { τD65_SNA = Convert.ToDouble(f_τ[0][0]) * Convert.ToDouble(Old[0][6]) * τD65_SNA; }
                        Uw = 1 / (0.019 + 1 / Convert.ToDouble(Old[0][7]) + 1 / Uw);
                    }
                }
            }
            else if (Type == "내부덧댐")
            {
                if (OldWindow != null && GlassName != null)
                {
                    Old = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "번호,창호명칭,Type,LE_CL_V,유리열관류율,태양열취득률,빛투과율,창호열관류율", "창호명칭 = '" + OldWindow + "'");
                    if (Old.Length > 0)
                    {
                        String 조합구성 = LE_CL_V + "+" + Old[0][3];
                        f_shgc = Program.DB.getValue(DB.type.BaseDB_HCneed, "이중창보정계수", "계수", "조합구성 = '" + 조합구성 + "' AND 보정유형 = '태양열취득률'");
                        f_τ = Program.DB.getValue(DB.type.BaseDB_HCneed, "이중창보정계수", "계수", "조합구성 = '" + 조합구성 + "' AND 보정유형 = '빛투과율'");
                        if (f_shgc.Length > 0)
                        { g = Convert.ToDouble(f_shgc[0][0]) * Convert.ToDouble(Old[0][5]) * g; }
                        if (f_τ.Length > 0)
                        { τD65_SNA = Convert.ToDouble(f_τ[0][0]) * Convert.ToDouble(Old[0][6]) * τD65_SNA; }
                        Uw = 1 / (0.019 + 1 / Convert.ToDouble(Old[0][7]) + 1 / Uw);
                    }
                }
            }
            else
            {
                g = g;
                τD65_SNA = τD65_SNA;
                Uw = Uw;
            }

            g_textBox.Text = g.ToString();
            controls.ThousandsSeparator textbox2 = new controls.ThousandsSeparator(g_textBox, true, 3);
            g2_textBox.Text = g.ToString();
            controls.ThousandsSeparator textbox3 = new controls.ThousandsSeparator(g2_textBox, true, 3);
            g3_textBox.Text = g.ToString();
            controls.ThousandsSeparator textbox4 = new controls.ThousandsSeparator(g3_textBox, true, 3);
            τD65_SNA_textBox.Text = τD65_SNA.ToString();
            controls.ThousandsSeparator textbox5 = new controls.ThousandsSeparator(τD65_SNA_textBox, true, 3);
            τD65_SNA2_textBox.Text = τD65_SNA.ToString();
            controls.ThousandsSeparator textbox6 = new controls.ThousandsSeparator(τD65_SNA2_textBox, true, 3);
            Uw_textBox.Text = Uw.ToString();
            controls.ThousandsSeparator textbox7 = new controls.ThousandsSeparator(Uw_textBox, true, 3);
        }

        private void Load_WindowType_image(String Type)
        {

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "창호구조유형이미지", "이미지", "구조유형 = '" + Type + "'");
            if (Image.Length > 0)
            {
                WindowType_pictureBox.Visible = true;
                WindowType_pictureBox.Load(Program.gPath + Image[0][0]);
                WindowType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void UwMethod_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Uw_comboBox.SelectedItem != null)
            {
                UwMethod = Uw_comboBox.SelectedItem.ToString();

                if (UwMethod == "계산")
                {
                    Frame_label.Visible = true;
                    Frame_comboBox.Visible = true;
                    FrameName_textBox.Visible = true;

                    Spacer_label.Visible = true;
                    SpacerName_textBox.Visible = true;
                    SpacerName_textBox2.Visible = true;

                    Ug_label.Visible = true;
                    Ug_textBox.Visible = true;
                    Ug_unit_label.Visible = true;

                    Psi_fix_label.Visible = true;
                    Psi_open_label.Visible = true;
                    Psi_fix_unit_label.Visible = true;
                    Psi_open_unit_label.Visible = true;
                    Psi_g_fix_textBox.Visible = true;
                    Psi_g_open_textBox.Visible = true;
                }
                else if (UwMethod == "법규")
                {
                    Frame_label.Visible = false;
                    Frame_comboBox.Visible = false;
                    FrameName_textBox.Visible = false;

                    Spacer_label.Visible = false;
                    SpacerName_textBox.Visible = false;
                    SpacerName_textBox2.Visible = false;

                    Ug_label.Visible = false;
                    Ug_textBox.Visible = false;
                    Ug_unit_label.Visible = false;

                    Psi_fix_label.Visible = false;
                    Psi_open_label.Visible = false;
                    Psi_fix_unit_label.Visible = false;
                    Psi_open_unit_label.Visible = false;
                    Psi_g_fix_textBox.Visible = false;
                    Psi_g_open_textBox.Visible = false;
                }
                else if (UwMethod == "진단")
                {
                    Frame_label.Visible = false;
                    Frame_comboBox.Visible = false;
                    FrameName_textBox.Visible = false;

                    Spacer_label.Visible = false;
                    SpacerName_textBox.Visible = false;
                    SpacerName_textBox2.Visible = false;

                    Ug_label.Visible = false;
                    Ug_textBox.Visible = false;
                    Ug_unit_label.Visible = false;

                    Psi_fix_label.Visible = false;
                    Psi_open_label.Visible = false;
                    Psi_fix_unit_label.Visible = false;
                    Psi_open_unit_label.Visible = false;
                    Psi_g_fix_textBox.Visible = false;
                    Psi_g_open_textBox.Visible = false;
                }
                Calc_Uw();
                Rule_Uw();
                Calc_AdditionalWindow();
                Calc_dUinst();
            }
        }


        private void Uw2_textBox_TextChanged(object sender, EventArgs e)
        {
            if (UwMethod == "진단" && Uw_textBox.Text != string.Empty)
            {
                Uw = Convert.ToDouble(Uw_textBox.Text);
                Calc_dUinst();
            }
        }

        private void DiIndil_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DiIndi_comboBox.SelectedItem != null)
            {
                DiIndi = DiIndi_comboBox.SelectedItem.ToString();
                Rule_Uw();
                Calc_dUinst();
            }
        }

        private void Frame_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Frame_comboBox.SelectedItem != null)
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
                        GlassName_textBox2.Text = "";
                        SpacerName = "";
                        SpacerName_textBox.Text = "";
                        SpacerName_textBox2.Text = "";
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
        }


        private void Install_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Install_comboBox.SelectedItem != null)
            {
                InstallType = Install_comboBox.SelectedItem.ToString();


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
        }


        public void Calc_Uw()
        {
            if (UwMethod == "계산" && Ug != 0 && Uf_fix != 0 && Psi_g_fix != 0 && Area != 0)
            {
                Uw = (Ug * (Ag_fix + Ag_open) + (Uf_open * Af_open) + (Uf_fix * Af_fix) + (Uf_btw * Af_btw) + (Psi_g_fix * Lg_fix) + (Psi_g_open * Lg_open)) / Area;
                if (Uw.Equals(double.NaN) == false)
                {
                    Uw_textBox.Text = Uw.ToString();
                    controls.ThousandsSeparator textboxa = new controls.ThousandsSeparator(Uw_textBox, true, 3);
                }
            }
        }

        public void Rule_Uw()
        {
            if (UwMethod == "법규")
            {
                String[][] Uvalue = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율", "구조체 = '창호' And 시기 = '2018.09' AND  지역 = '중부1' AND 직접간접 =  '" + DiIndi + "'");
                if (Uvalue.Length > 0)
                {
                    Uw = Convert.ToDouble(Uvalue[0][0]);
                    Uw_textBox.Text = Uw.ToString();
                    controls.ThousandsSeparator textboxa = new controls.ThousandsSeparator(Uw_textBox, true, 3);
                }
            }
        }
        public void Calc_dUinst()
        {
            if (Uw != 0 && Area != 0)
            {
                dUinst = ((Psi_InstallTop * Width) + (Psi_InstallButtom * Width) + (Psi_InstallSide * Height * 2)) / Area;
                if (dUinst.Equals(double.NaN) == false)
                {
                    dUinst_textBox.Text = dUinst.ToString();
                    controls.ThousandsSeparator textboxa = new controls.ThousandsSeparator(dUinst_textBox, true, 3);
                }

                if (dUinst.Equals(double.NaN) == false && Uw.Equals(double.NaN) == false)
                {
                    Uw_inst = dUinst + Uw;
                    Uw_inst_textBox.Text = Uw_inst.ToString();
                    controls.ThousandsSeparator textboxb = new controls.ThousandsSeparator(Uw_inst_textBox, true, 3);
                    Uw_inst2_textBox.Text = Uw_inst.ToString();
                    controls.ThousandsSeparator textboxc = new controls.ThousandsSeparator(Uw_inst2_textBox, true, 3);
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

        private void reset()
        {
            WinNum_textBox.Text = "";
            Name_textBox.Text = "";

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;

            AdditionalWindow_comboBox.SelectedItem = null;
            Uw_comboBox.SelectedItem = null;
            DiIndi_comboBox.SelectedItem = null;
            Frame_comboBox.SelectedItem = null;

            FrameMaterial_textBox.Text = null;
            FrameName_textBox.Text = null;
            GlassName_textBox.Text = null;
            GlassName_textBox2.Text = null;
            SpacerName_textBox.Text = null;
            SpacerName_textBox2.Text = null;

            Install_comboBox.SelectedItem = null;
            Install_textBox.Text = null;

            Ug_textBox.Text = null;
            g_textBox.Text = null;
            g2_textBox.Text = null;
            g3_textBox.Text = null;
            τD65_SNA_textBox.Text = null;
            τD65_SNA2_textBox.Text = null;
            Psi_g_fix_textBox.Text = null;
            Psi_g_open_textBox.Text = null;
            Psi_InstallTop_textBox.Text = null;
            Psi_InstallSide_textBox.Text = null;
            Psi_InstallButtom_textBox.Text = null;
            Uw_textBox.Text = null;
            Uf_open_textBox.Text = null;
            Uf_fix_textBox.Text = null;
            Uf_btw_textBox.Text = null;
            df_open_textBox.Text = null;
            df_fix_textBox.Text = null;
            df_btw_textBox.Text = null;

            WindowType_pictureBox.Visible = false;
            WindowFrame_pictureBox.Visible = false;
            WindowInstall_pictureBox.Visible = false;

            WinNum = null;
            WindowName = null;
            Type = null;
            OldWindow = null;
            UwMethod = null;
            DiIndi = null;
            FrameType = null;
            SingleDoubleType = null;
            FrameMaterial = null;
            FrameName = null;
            GlassName = null;
            SpacerName = null;
            InstallType = null;
            InstallName = null;
            LE_CL_V = null;
            check_FrameType = null;
            check_SingleDoubleType = null;
            check_FrameMaterial = null;
            check_LE_CL_V = null;
            check_InstallType = null;

            Uw_textBox.Text = null;
            dUinst_textBox.Text = null;
            Uw_inst_textBox.Text = null;
            Uw_inst2_textBox.Text = null;
            Uf_open_textBox.Text = null;
            Uf_fix_textBox.Text = null;
            Uf_btw_textBox.Text = null;
            df_open_textBox.Text = null;
            df_fix_textBox.Text = null;
            df_btw_textBox.Text = null;

            Area_textBox.Text = null;
            Width_textBox.Text = null;
            Height_textBox.Text = null;
            Ag_fix_textBox.Text = null;
            Ag_open_textBox.Text = null;
            Af_open_textBox.Text = null;
            Af_fix_textBox.Text = null;
            Af_btw_textBox.Text = null;
            Lg_fix_textBox.Text = null;
            Lg_open_textBox.Text = null;

            d_InstallTop_textBox.Text = null;
            d_InstallButtom_textBox.Text = null;
            d_InstallSide_textBox.Text = null;

            Ug = 0;
            g = 0;
            τD65_SNA = 0;
            Psi_g_fix = 0;
            Psi_g_open = 0;
            Uw = 0;

            Uf_open = 0;
            Uf_fix = 0;
            Uf_btw = 0;
            df_open = 0;
            df_fix = 0;
            df_btw = 0;

            Psi_InstallTop = 0;
            Psi_InstallSide = 0;
            Psi_InstallButtom = 0;


            Old = null;
            f_shgc = null;
            f_τ = null;
            Size = null;
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();

            WinNum_textBox.Text = ID;
            WinNum = ID;

            String[][] SubLoad = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "번호,명칭,상위창호번호,창호면적,창호너비,창호높이,고정유리면적,개폐유리면적,개폐프레임면적,고정프레임면적,중간프레임면적,고정유리둘레길이,개폐유리둘레길이," +
                "창호열관류율,설치열교가산치,창호유효열관류율"
                , "번호 = '" + ID + "'");
            if (SubLoad.Length > 0)
            {
                String[][] MainLoad = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "번호,창호명칭,Type,기존창호,Uw적용방법,직접간접,프레임유형,이중단창,프레임재료,프레임종류,유리종류,간봉종류,설치유형,설치종류,LE_CL_V," +
              "유리열관류율,태양열취득률,빛투과율,고정유리선형열관류율,개폐유리선형열관류율," +
              "상부설치열관류율,측면설치열관류율,하부설치열관류율," +
              "창호열관류율," +
              "개폐부프레임열관류율,고정부프레임열관류율,중간바프레임열관류율,개폐부프레임두께,고정부프레임두께,중간바프레임두께"
                , "번호 = '" + SubLoad[0][2] + "'");
                if (SubLoad.Length > 0)
                {
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
                    GlassName_textBox2.Text = MainLoad[0][10];
                    SpacerName = MainLoad[0][11];
                    SpacerName_textBox.Text = MainLoad[0][11];
                    SpacerName_textBox2.Text = MainLoad[0][11];
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
                    τD65_SNA = Convert.ToDouble(MainLoad[0][17]);

                    g_textBox.Text = g.ToString();
                    controls.ThousandsSeparator textbox2 = new controls.ThousandsSeparator(g_textBox, true, 3);
                    g2_textBox.Text = g.ToString();
                    controls.ThousandsSeparator textbox3 = new controls.ThousandsSeparator(g2_textBox, true, 3);
                    g3_textBox.Text = g.ToString();
                    controls.ThousandsSeparator textbox4 = new controls.ThousandsSeparator(g3_textBox, true, 3);
                    τD65_SNA_textBox.Text = τD65_SNA.ToString();
                    controls.ThousandsSeparator textbox5 = new controls.ThousandsSeparator(τD65_SNA_textBox, true, 3);
                    τD65_SNA2_textBox.Text = τD65_SNA.ToString();
                    controls.ThousandsSeparator textbox6 = new controls.ThousandsSeparator(τD65_SNA2_textBox, true, 3);


                    Psi_g_fix = Convert.ToDouble(MainLoad[0][18]);
                    Psi_g_open = Convert.ToDouble(MainLoad[0][19]);

                    Psi_g_fix_textBox.Text = Psi_g_fix.ToString();
                    controls.ThousandsSeparator textbox21 = new controls.ThousandsSeparator(Psi_g_fix_textBox, true, 3);
                    Psi_g_open_textBox.Text = Psi_g_open.ToString();
                    controls.ThousandsSeparator textbox22 = new controls.ThousandsSeparator(Psi_g_open_textBox, true, 3);


                    Psi_InstallTop = Convert.ToDouble(MainLoad[0][20]);
                    Psi_InstallSide = Convert.ToDouble(MainLoad[0][21]);
                    Psi_InstallButtom = Convert.ToDouble(MainLoad[0][22]);

                    Psi_InstallTop_textBox.Text = Psi_InstallTop.ToString();
                    controls.ThousandsSeparator textbox23 = new controls.ThousandsSeparator(Psi_InstallTop_textBox, true, 3);
                    Psi_InstallSide_textBox.Text = Psi_InstallSide.ToString();
                    controls.ThousandsSeparator textbox24 = new controls.ThousandsSeparator(Psi_InstallSide_textBox, true, 3);
                    Psi_InstallButtom_textBox.Text = Psi_InstallButtom.ToString();
                    controls.ThousandsSeparator textbox25 = new controls.ThousandsSeparator(Psi_InstallButtom_textBox, true, 3);


                    Uw = Convert.ToDouble(SubLoad[0][13]);
                    if (UwMethod == "계산")
                    {
                        Uw_textBox.Text = Uw.ToString();
                        controls.ThousandsSeparator textbox = new controls.ThousandsSeparator(Uw_textBox, true, 3);
                    }
                    else
                    {
                        Uw_textBox.Text = Uw.ToString();
                        controls.ThousandsSeparator textbox = new controls.ThousandsSeparator(Uw_textBox, true, 3);
                    }
                    dUinst = Convert.ToDouble(SubLoad[0][14]);
                    dUinst_textBox.Text = dUinst.ToString();
                    controls.ThousandsSeparator textboxa = new controls.ThousandsSeparator(dUinst_textBox, true, 3);
                    Uw_inst = Convert.ToDouble(SubLoad[0][15]);
                    Uw_inst_textBox.Text = Uw_inst.ToString();
                    controls.ThousandsSeparator textboxb = new controls.ThousandsSeparator(Uw_inst_textBox, true, 3);
                    Uw_inst2_textBox.Text = Uw_inst.ToString();
                    controls.ThousandsSeparator textboxc = new controls.ThousandsSeparator(Uw_inst2_textBox, true, 3);

                    Uf_open = Convert.ToDouble(MainLoad[0][24]);
                    Uf_open_textBox.Text = Uf_open.ToString();
                    controls.ThousandsSeparator textbox11 = new controls.ThousandsSeparator(Uf_open_textBox, true, 2);

                    Uf_fix = Convert.ToDouble(MainLoad[0][25]);
                    Uf_fix_textBox.Text = Uf_fix.ToString();
                    controls.ThousandsSeparator textbox12 = new controls.ThousandsSeparator(Uf_fix_textBox, true, 2);

                    Uf_btw = Convert.ToDouble(MainLoad[0][26]);
                    Uf_btw_textBox.Text = Uf_btw.ToString();
                    controls.ThousandsSeparator textbox13 = new controls.ThousandsSeparator(Uf_btw_textBox, true, 2);

                    df_open = Convert.ToDouble(MainLoad[0][27]);
                    df_open_textBox.Text = df_open.ToString();
                    controls.ThousandsSeparator textbox14 = new controls.ThousandsSeparator(df_open_textBox, true, 2);

                    df_fix = Convert.ToDouble(MainLoad[0][28]);
                    df_fix_textBox.Text = df_fix.ToString();
                    controls.ThousandsSeparator textbox15 = new controls.ThousandsSeparator(df_fix_textBox, true, 2);

                    df_btw = Convert.ToDouble(MainLoad[0][29]);
                    df_btw_textBox.Text = df_btw.ToString();
                    controls.ThousandsSeparator textbox16 = new controls.ThousandsSeparator(df_btw_textBox, true, 2);

                    Area = Convert.ToDouble(SubLoad[0][3]);
                    Width = Convert.ToDouble(SubLoad[0][4]);
                    Height = Convert.ToDouble(SubLoad[0][5]);
                    Ag_fix = Convert.ToDouble(SubLoad[0][6]);
                    Ag_open = Convert.ToDouble(SubLoad[0][7]);
                    Af_open = Convert.ToDouble(SubLoad[0][8]);
                    Af_fix = Convert.ToDouble(SubLoad[0][9]);
                    Af_btw = Convert.ToDouble(SubLoad[0][10]);
                    Lg_fix = Convert.ToDouble(SubLoad[0][11]);
                    Lg_open = Convert.ToDouble(SubLoad[0][12]);

                    Area_textBox.Text = Area.ToString();
                    Width_textBox.Text = Width.ToString();
                    Height_textBox.Text = Height.ToString();
                    Ag_fix_textBox.Text = Ag_fix.ToString();
                    Ag_open_textBox.Text = Ag_open.ToString();
                    Af_open_textBox.Text = Af_open.ToString();
                    Af_fix_textBox.Text = Af_fix.ToString();
                    Af_btw_textBox.Text = Af_btw.ToString();
                    Lg_fix_textBox.Text = Lg_fix.ToString();
                    Lg_open_textBox.Text = Lg_open.ToString();

                    controls.ThousandsSeparator textbox111 = new controls.ThousandsSeparator(Area_textBox, true, 2);
                    controls.ThousandsSeparator textbox112 = new controls.ThousandsSeparator(Width_textBox, true, 2);
                    controls.ThousandsSeparator textbox113 = new controls.ThousandsSeparator(Height_textBox, true, 2);
                    controls.ThousandsSeparator textbox114 = new controls.ThousandsSeparator(Ag_fix_textBox, true, 2);
                    controls.ThousandsSeparator textbox115 = new controls.ThousandsSeparator(Ag_open_textBox, true, 2);
                    controls.ThousandsSeparator textbox116 = new controls.ThousandsSeparator(Af_open_textBox, true, 2);
                    controls.ThousandsSeparator textbox117 = new controls.ThousandsSeparator(Af_fix_textBox, true, 2);
                    controls.ThousandsSeparator textbox118 = new controls.ThousandsSeparator(Af_btw_textBox, true, 2);
                    controls.ThousandsSeparator textbox119 = new controls.ThousandsSeparator(Lg_fix_textBox, true, 2);
                    controls.ThousandsSeparator textbox120 = new controls.ThousandsSeparator(Lg_open_textBox, true, 2);

                    d_InstallTop_textBox.Text = Width.ToString();
                    controls.ThousandsSeparator textbox31 = new controls.ThousandsSeparator(d_InstallTop_textBox, true, 2);
                    d_InstallButtom_textBox.Text = Width.ToString();
                    controls.ThousandsSeparator textbox32 = new controls.ThousandsSeparator(d_InstallButtom_textBox, true, 2);
                    d_InstallSide_textBox.Text = (Height * 2).ToString();
                    controls.ThousandsSeparator textbox33 = new controls.ThousandsSeparator(d_InstallSide_textBox, true, 2);

                    string[][] Image1 = Program.DB.getValue(DB.type.BaseDB_HCneed, "창호구조유형이미지", "이미지", "구조유형 = '" + Type + "'");
                    if (Image1.Length > 0)
                    {
                        WindowType_pictureBox.Visible = true;
                        WindowType_pictureBox.Load(Program.gPath + Image1[0][0]);
                        WindowType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    string[][] Image2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "창호프레임이미지", "이미지", "유형1 = '" + FrameType + "' AND 유형2 = '기본형' AND 재료 = '" + FrameMaterial + "'");
                    if (Image2.Length > 0)
                    {
                        WindowFrame_pictureBox.Visible = true;
                        WindowFrame_pictureBox.Load(Program.gPath + Image2[0][0]);
                        WindowFrame_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    string[][] Image3 = Program.DB.getValue(DB.type.BaseDB_HCneed, "창호설치열교이미지", "이미지열교유형", "구분1 = '" + InstallType + "' AND 구분2 = '" + FrameMaterial + "' AND 구분3 = '" + SingleDoubleType + "' AND 구분4 = '" + InstallName + "'");
                    if (Image3.Length > 0)
                    {
                        WindowInstall_pictureBox.Visible = true;
                        WindowInstall_pictureBox.Load(Program.gPath + Image3[0][0]);
                        WindowInstall_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
            }
        }
        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            WinNum_textBox.Text = ID;
            WinNum = ID;
        }

    }
}
