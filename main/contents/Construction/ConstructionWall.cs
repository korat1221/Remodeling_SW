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

namespace main.contents
{
    public partial class ConstructionWall : Form
    {
        private String WallNum;
        String WallName, Type, OldWall, CWName, UMethod, DiIndi, FrameType, check_FrameType, FrameName, FixGlassName, OpenGlassName, SpacerName, StructureType, check_StructureType, TBName, LE_CL_V, SizeName;
        String PanelName, PanelGlassName, LE_CL_V_Panel, Color;
        String DoorFrame, check_DoorFrame, DoorGlassName, DoorSpacer, LE_CL_V_Door, check_LE_CL_V_Door;
        String[][] Size;
        double Ug_Fix, Ug_Open, g, τ, Psi_g_fix, Psi_g_open, Uf_mt, Uf_open, df_mt, df_open;
        double Up, Ug_panel, Conductivity_p, α, Psi_p, dPanel;
        double Ug_Door, gd, τd, Psi_g_Door, df_door, Uf_door;
        double Psi_InstallTop, Psi_InstallSide, Psi_InstallButtom;
        double Ucw, Uvalue, Ucw_p, Ucw_d;
        double Ucw_inst, dU;// dUinst는 열교가산치, Ucw_inst는 유효열관류율(커튼월창열관류율+열교가산치)
        double Ucw_g_inst, Ucw_p_inst, Ucw_d_inst;
        double Area, Width, Height, Ag_fix, Ag_open, Lg_fix, Lg_open, Ap, Lp, Af_mt, Af_open, Af_d, Ag_d, Lg_d;
        String[][] Old; String[][] f_shgc; String[][] f_τ;
        Boolean Panel_check, Door_check;
        public ConstructionWall()
        {
            InitializeComponent();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '외벽'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            //외장재색 콤보박스
            Program.UTIL.FillComboBox(Color_comboBox, "외벽", "외장재색", "1");
            //직접간접 콤보박스
            Program.UTIL.FillComboBox(DiIndi_comboBox, "외벽", "직접/간접", "1");
            //구조유형콤보박스
            Program.UTIL.FillComboBox(StructureType_comboBox, "외벽", "구조유형", "1");

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

            string[][] Image = Program.DB.getValue(DB.type.BaseDB, "외벽유형이미지", "이미지", "외벽유형 = '" + Type + "'");

            WallType_pictureBox.Load(Program.gPath + Image[0][0]);
            WallType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

        }

        private void Color_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color = Color_comboBox.SelectedItem.ToString();
            String[][] value = Program.DB.getValue(DB.type.BaseDB, "흡수율", "흡수율", "외장재색 = '" + Color + "'");
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
                Calc_Ucw();
                Frame_label.Visible = true;

                TB_button.Visible = true;

                U_textBox.Enabled = false;
                U_textBox.BorderStyle = BorderStyle.None;
                Ueff_textBox.Enabled = false;
                Ueff_textBox.BorderStyle = BorderStyle.None;
            }
            else if (UMethod == "법규")
            {
                Rule_U();
                Frame_label.Visible = false;
                TB_button.Visible = false;
                U_textBox.Enabled = false;
                U_textBox.BorderStyle = BorderStyle.None;
            }
            else if (UMethod == "진단")
            {
                Frame_label.Visible = false;


                U_textBox.Enabled = true;
                U_textBox.BorderStyle = BorderStyle.FixedSingle;
            }
            Calc_dUinst();
        }

        private void U_textBox_TextChanged(object sender, EventArgs e)
        {
            if (UMethod == "진단" && U_textBox.Text != string.Empty)
            {
                Uvalue = Convert.ToDouble(U_textBox.Text);
            }
            Calc_Ucw();
            Calc_dUinst();
        }


        private void DiIndi_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DiIndi = DiIndi_comboBox.SelectedItem.ToString();
            Rule_U();
            Calc_dUinst();
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
                    }
                }
            }
            catch { }
            Calc_dUinst();
        }

        private void TB_button_Click(object sender, EventArgs e)
        {
            if (StructureType == null)
            {
                MessageBox.Show("설치 유형부터 선택하세요.");
            }
            else
            {
                Wall_TB TB_form = new Wall_TB(Type, StructureType);
                DialogResult result = TB_form.ShowDialog();
                if (result == DialogResult.OK)
                {                   
                    check_StructureType = TB_form.Select_TB[2];
                    TBName = TB_form.Select_TB[3];
                    TBName_textBox.Text = TBName;
                    check_FrameType = TB_form.Select_TB[9];
                    dU = Convert.ToDouble(TB_form.Select_TB[10]);
                    dU_textBox.Text = string.Format("{0:F3}", dU);
                    //SpacerName = cw_spacerDB_form.Select_Spacer[3];
                    //SpacerName_textBox.Text = SpacerName;
                    //if (LE_CL_V.Contains("LE"))
                    //{
                    //    Psi_g_fix = Convert.ToDouble(cw_spacerDB_form.Select_Spacer[7]);
                    //    Psi_g_open = Convert.ToDouble(cw_spacerDB_form.Select_Spacer[8]);
                    //}
                    //else
                    //{
                    //    Psi_g_fix = Convert.ToDouble(cw_spacerDB_form.Select_Spacer[5]);
                    //    Psi_g_open = Convert.ToDouble(cw_spacerDB_form.Select_Spacer[6]);
                    //}
                    //Psi_g_fix_textBox.Text = String.Format("{0:F3}", Psi_g_fix);
                    //Psi_g_open_textBox.Text = String.Format("{0:F3}", Psi_g_open);
                    //tabControl1.SelectedTab = tabControl1.TabPages["Glass_tabPage"];

                }
            }
            //Calc_Ucw();
            //Calc_dUinst();
        }


        public void Rule_U()
        {
            if (UMethod == "법규")
            {
                String[][] Uvalue = Program.DB.getValue(DB.type.BaseDB, "법규열관류율", "열관류율", "구조체 = '창호' And 시기 = '2018.09' AND  지역 = '중부1' AND 직접간접 =  '" + DiIndi + "'");
                this.Uvalue = Convert.ToDouble(Uvalue[0][0]);
                U_textBox.Text = string.Format("{0:F3}", this.Uvalue);

            }
        }

        public void Calc_Ucw()
        {
            double Af_mt_g = Af_mt * (Ag_fix + Ag_open + Af_open) / (Area - Af_mt);
            double Af_mt_p = Af_mt * Ap / (Area - Af_mt);
            double Af_mt_d = Af_mt * (Ag_d + Af_d) / (Area - Af_mt);

            if (UMethod == "계산")
            {
                if (Ug_Fix != 0 && Uf_open != 0 && Psi_g_fix != 0 && Area != 0)
                {
                    Ucw = ((Ug_Fix * Ag_fix) + (Ug_Open * Ag_open) + (Up * Ag_open) + (Ug_Door * Ag_d) + (Uf_mt * Af_mt) + (Uf_open * Af_open) + (Uf_door * Af_d) + (Psi_g_fix * Lg_fix) + (Psi_g_open * Lg_open) + (Psi_p * Lp) + (Psi_g_Door * Lg_d)) / Area;
                }

                if (Ug_Fix != 0 && Uf_open != 0 && Psi_g_fix != 0 && Area != 0)
                {
                    Uvalue = ((Ug_Fix * Ag_fix) + (Ug_Open * Ag_open) + (Uf_mt * Af_mt_g) + (Uf_open * Af_open) + (Psi_g_fix * Lg_fix) + (Psi_g_open * Lg_open)) / (Ag_fix + Ag_open + Af_mt_g + Af_open);
                    U_textBox.Text = String.Format("{0:F3}", Uvalue);
                }

            }
            else
            {
                Ucw = ((Ag_fix + Ag_open + Af_open + Af_mt_g) * Uvalue + (Ap + Af_mt_p) * Ucw_p + (Af_d + Ag_d + Af_mt_d) * Ucw_d) / Area;
            }
        }

        public void Calc_dUinst()
        {
            if (Ucw != 0 && Area != 0)
            {
                dU = ((Psi_InstallTop * Width) + (Psi_InstallButtom * Width) + (Psi_InstallSide * Height * 2)) / Area;

                if (dU.Equals(double.NaN) == false)
                {
                }
            }

            Ucw_inst = Ucw + dU;
            Ucw_g_inst = Uvalue + dU;

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
            else if (FixGlassName == null)
            {
                MessageBox.Show("유리를 선택하세요.");
            }
            else if (TBName == null)
            {
                MessageBox.Show("설치열교를 선택하세요.");
            }
            else if (UMethod == "계산")
            {
                if (FrameName == null)
                {
                    MessageBox.Show("프레임을 선택하세요.");
                }
                else if (SpacerName == null)
                {
                    MessageBox.Show("간봉을 선택하세요.");
                }
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
            Program.DB.setValue(DB.type.ProjDB, "ConstructionWall", "번호,명칭,Type,기존커튼월,Ucw적용방법,직접간접,프레임유형,프레임종류,고정유리종류,개폐유리종류,간봉종류,설치유형,설치종류,LE_CL_V,패널적용유무,출입문적용유무," +
                      "고정유리열관류율,개폐유리열관류율,태양열취득률,빛투과율,고정유리선형열관류율,개폐유리선형열관류율,고정프레임열관류율,개폐프레임열관류율,고정프레임두께,개폐프레임두께," +
                      "상부설치열관류율,측면설치열관류율,하부설치열관류율," +
                      "사이즈명칭,커튼월면적,너비,높이,고정창유리면적,개폐창유리면적,고정창유리둘레길이,개폐창유리둘레길이,M_T프레임면적,개폐창프레임면적," +
                      "커튼월창열관류율,유리부분열관류율,설치열교가산치,커튼월창유효열관류율,유리부분유효열관류율",
                    "'" + CWNum_textBox.Text + "','" + WallName + "','" + Type + "','" + OldWall + "','" + UMethod + "','" + DiIndi + "','" + FrameType + "','" + FrameName + "','" + FixGlassName + "','" + OpenGlassName + "','" + SpacerName + "','" + StructureType + "','" + TBName + "','" + LE_CL_V + "','" + Panel_check.ToString() + "','" + Door_check.ToString() + "','" +
                    Ug_Fix.ToString() + "','" + Ug_Open.ToString() + "','" + g.ToString() + "','" + τ.ToString() + "','" + Psi_g_fix.ToString() + "','" + Psi_g_open.ToString() + "','" + Uf_mt.ToString() + "','" + Uf_open.ToString() + "','" + df_mt.ToString() + "','" + df_open.ToString() + "','" +
                    Psi_InstallTop.ToString() + "','" + Psi_InstallSide.ToString() + "','" + Psi_InstallButtom.ToString() + "','" +
                    SizeName + "','" + Area.ToString() + "','" + Width.ToString() + "','" + Height.ToString() + "','" + Ag_fix.ToString() + "','" + Ag_open.ToString() + "','" + Lg_fix.ToString() + "','" + Lg_open.ToString() + "','" + Af_mt.ToString() + "','" + Af_open.ToString() + "','" +
                    Ucw.ToString() + "','" + Uvalue.ToString() + "','" + dU.ToString() + "','" + Ucw_inst.ToString() + "','" + Ucw_g_inst.ToString()
                    + "'", "번호");

            this.DialogResult = DialogResult.OK;
            this.Hide();
            Program.getMenuForm().DoLoadForm(30, OnLoadListProc);
        }


        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            try
            {
                CWNum_textBox.Text = ID;
                WallNum = ID;

                String[][] Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "번호,명칭,Type,기존커튼월,Ucw적용방법,직접간접,프레임유형,프레임종류,고정유리종류,개폐유리종류,간봉종류,설치유형,설치종류,LE_CL_V," +
                      "고정유리열관류율,개폐유리열관류율,태양열취득률,빛투과율,고정유리선형열관류율,개폐유리선형열관류율,고정프레임열관류율,개폐프레임열관류율,고정프레임두께,개폐프레임두께," +
                      "상부설치열관류율,측면설치열관류율,하부설치열관류율," +
                      "사이즈명칭,커튼월면적,너비,높이,고정창유리면적,개폐창유리면적,고정창유리둘레길이,개폐창유리둘레길이,M_T프레임면적,개폐창프레임면적," +
                      "커튼월창열관류율,유리부분열관류율,설치열교가산치,커튼월창유효열관류율,유리부분유효열관류율," +
                      "패널적용유무,출입문적용유무"
                      , "번호 = '" + ID + "'");
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
                OldWall = Load[0][3];
                UMethod = Load[0][4];
                Uvalue_comboBox.SelectedItem = UMethod;
                DiIndi = Load[0][5];
                DiIndi_comboBox.SelectedItem = DiIndi;

                FrameType = Load[0][6];
                check_FrameType = FrameType;

                FrameName = Load[0][7];
                FrameName2_textBox.Text = FrameName;

                FixGlassName = Load[0][8];
                TBName_textBox.Text = FixGlassName;

                OpenGlassName = Load[0][9];

                SpacerName = Load[0][10];

                StructureType = Load[0][11];
                StructureType_comboBox.SelectedItem = StructureType;

                TBName = Load[0][12];

                LE_CL_V = Load[0][13];
                check_StructureType = Load[0][13];

                Ug_Fix = Convert.ToDouble(Load[0][14]);
                Ug_Open = Convert.ToDouble(Load[0][15]);
                g = Convert.ToDouble(Load[0][16]);
                α_textBox.Text = String.Format("{0:F3}", g);
                τ = Convert.ToDouble(Load[0][17]);
                dU_textBox.Text = String.Format("{0:F3}", τ);
                Psi_g_fix = Convert.ToDouble(Load[0][18]);
                Psi_g_open = Convert.ToDouble(Load[0][19]);
                Uf_mt = Convert.ToDouble(Load[0][20]);
                Uf_open = Convert.ToDouble(Load[0][21]);
                df_mt = Convert.ToDouble(Load[0][22]);
                df_open = Convert.ToDouble(Load[0][23]);

                Psi_InstallTop = Convert.ToDouble(Load[0][24]);
                Psi_InstallSide = Convert.ToDouble(Load[0][25]);
                Psi_InstallButtom = Convert.ToDouble(Load[0][26]);

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

                Ucw = Convert.ToDouble(Load[0][37]);
                Uvalue = Convert.ToDouble(Load[0][38]);
                U_textBox.Text = String.Format("{0:F3}", Uvalue);
                dU = Convert.ToDouble(Load[0][39]);
                Ucw_inst = Convert.ToDouble(Load[0][40]);
                Ucw_g_inst = Convert.ToDouble(Load[0][41]);

                Panel_check = Convert.ToBoolean(Load[0][42]);
                Door_check = Convert.ToBoolean(Load[0][43]);
            }
            catch { }

            if (Panel_check == true)
            {
                try
                {
                    String[][] Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "패널종류,패널유리종류,LE_CL_V_Panel," +
                          "패널열관류율,패널유리열관류율,패널열전도율,패널흡수율,패널선형열관류율,패널두께," +
                          "패널면적,패널둘레길이," +
                          "패널부분열관류율,패널부분유효열관류율"
                          , "번호 = '" + ID + "'");

                    PanelName = Load[0][0];

                    PanelGlassName = Load[0][1];

                    LE_CL_V_Panel = Load[0][2];

                    Up = Convert.ToDouble(Load[0][3]);
                    Ug_panel = Convert.ToDouble(Load[0][4]);
                    Conductivity_p = Convert.ToDouble(Load[0][5]);

                    α = Convert.ToDouble(Load[0][6]);
                    α_textBox.Text = String.Format("{0:F1}", α);

                    Psi_p = Convert.ToDouble(Load[0][7]);

                    dPanel = Convert.ToDouble(Load[0][8]);
                    Ap = Convert.ToDouble(Load[0][9]);
                    Lp = Convert.ToDouble(Load[0][10]);


                    Ucw_p = Convert.ToDouble(Load[0][11]);
                    Ueff_textBox.Text = String.Format("{0:F3}", Ucw_p);
                    Ucw_p_inst = Convert.ToDouble(Load[0][12]);
                }
                catch { }
            }
            else { }

            if (Door_check == true)
            {
                try
                {
                    String[][] Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "출입문프레임유형,출입문프레임종류,출입문유리종류,출입문간봉종류,LE_CL_V_Door," +
                       "출입문유리열관류율,출입문태양열취득률,출입문빛투과율,출입문유리선형열관류율,출입문프레임두께,출입문프레임열관류율," +
                       "출입문프레임면적,출입문유리면적,출입문유리둘레길이," +
                       "출입문부분열관류율,출입문부분유효열관류율"
                          , "번호 = '" + ID + "'");

                    DoorFrame = Load[0][1];
                    check_DoorFrame = DoorFrame;

                    DoorGlassName = Load[0][2];

                    LE_CL_V_Door = Load[0][4];
                    check_LE_CL_V_Door = LE_CL_V_Door;

                    Ug_Door = Convert.ToDouble(Load[0][5]);

                    gd = Convert.ToDouble(Load[0][6]);

                    τd = Convert.ToDouble(Load[0][7]);

                    Psi_g_Door = Convert.ToDouble(Load[0][8]);

                    df_door = Convert.ToDouble(Load[0][9]);

                    Uf_door = Convert.ToDouble(Load[0][10]);

                    Af_d = Convert.ToDouble(Load[0][11]);
                    Ag_d = Convert.ToDouble(Load[0][12]);
                    Lg_d = Convert.ToDouble(Load[0][13]);

                    Ucw_d = Convert.ToDouble(Load[0][14]);
                    Ucw_d_inst = Convert.ToDouble(Load[0][15]);

                }
                catch { }
            }
            else { }

            string[][] Image1 = Program.DB.getValue(DB.type.BaseDB, "커튼월구조유형이미지", "이미지", "구조유형 = '" + Type + "'");
            WallType_pictureBox.Load(Program.gPath + Image1[0][0]);
            WallType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            string[][] Image2 = Program.DB.getValue(DB.type.BaseDB, "커튼월설치열교이미지", "이미지", "구분1 = '" + StructureType + "' AND 구분3 = '" + TBName + "'");
        }

        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            CWNum_textBox.Text = ID;
            WallNum = ID;
        }

        public void CopyForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            CWNum_textBox.Text = ID;
            WallNum = ID;

            if (Name_textBox.Text != "")
            {
                WallName = Name_textBox.Text + "_복사";
                Name_textBox.Text = WallName;
                Save();
            }

        }

    }
}
