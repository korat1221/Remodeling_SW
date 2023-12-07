using main.contentslist;
using main.subcontents;
using main.subcontents.ConstructionCW;
using main.subcontents.ConstructionDoor;
using main.subcontents.ConstructionWall;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace main.contents
{
    public partial class ConstructionDoor : Form
    {

        double DoorArea, DoorL, DoorH, GlassL, GlassH, GlassArea;//치수정보
        double DoorUD, DoorOver, DoorBottom, DoorUDGlass, DoorUDinsGlass;//문열관류율,문틀상하부열관류율,유리반영문열관류율,유효문열관류율
        String Material, FrameIn, DoorIn, DoorDB; //문짝문틀정보,제품명
        double OverL, UnderL;//상부하부길이
        String[] Select = new String[14];

        private String CWNum;
        String DoorName, Type, OldDoor, UDoorMethod, DiIndi, FrameType, check_FrameType, FrameName, FixGlassName, OpenGlassName, SpacerName, InstallType, installlocation, check_InstallType, Install2, Install1, LE_CL_V, check_LE_CL_V, SizeName, GlassName;
        String PanelName, PanelGlassName, LE_CL_V_Panel, DoorColor;
        double Ug_Fix, Ug_Open, Ug, g, τ, Psi_g_fix, Psi_g_open, Uf_mt, Uf_open, df_mt, df_open;
        double Up, Ug_panel, Conductivity_p, αd, Psi_p, dPanel;
        double Psi_InstallTop, Psi_InstallSide, Psi_InstallButtom;
        double Ucw, UD, Ucw_p, Ucw_d;
        double Ucw_inst, dUinst;// dUinst는 열교가산치, Ucw_inst는 유효열관류율(커튼월창열관류율+열교가산치)
        double Ucw_g_inst, Ucw_p_inst, Ucw_d_inst;
        double Area, Width, Height, Ag_fix, Ag_open, Lg_fix, Lg_open, Ap, Lp, Af_mt, Af_open;
        Boolean glass_check, Door_check;
        double Ff_g;



        public ConstructionDoor()
        {
            InitializeComponent();
            Program.DB.initTable(DB.type.CalcDB, "Import_CWSize"); //불러온 사이즈 정보 저장할 table 생성
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '외부출입문'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            //직접간접 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, DiIndi_comboBox, "출입문", "실외조건", "1");
            //설치위치 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, Install_comboBox, "출입문", "구조", "1");

            glass_checkBox.Checked = true;
            glass_checkBox.Checked = false;

            //치수정보 그림 시작하자마자 불러오기
            Load_DoorType_image2();



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
                DoorName = Name_textBox.Text.ToString();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Type = "기존 출입문";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_DoorType_image(Type);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Type = "신규 출입문";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_DoorType_image(Type);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Type = "철거 후 신규";
            Type_textBox.Text = Type;
            Changed_Type(Type);
            Load_DoorType_image(Type);
        }

        private void Changed_Type(String Type)
        {
            Udoor_comboBox.Items.Clear();

            switch (Type)
            {
                case "기존 출입문":
                    Udoor_comboBox.Items.Add("계산");
                    Udoor_comboBox.Items.Add("법규");
                    Udoor_comboBox.Items.Add("진단");
                    OldDoor_comboBox.Visible = false;
                    Load_OldDoor(Type);
                    break;

                case "신규 출입문":
                    Udoor_comboBox.Items.Add("계산");
                    Udoor_comboBox.Items.Add("법규");
                    OldDoor_comboBox.Visible = false;
                    Load_OldDoor(Type);
                    break;

                case "철거 후 신규":
                    Udoor_comboBox.Items.Add("계산");
                    Udoor_comboBox.Items.Add("법규");
                    OldDoor_comboBox.Visible = true;
                    Load_OldDoor(Type);
                    break;
            }

            //Udoor_comboBox.SelectedIndex = 0;
        }


        //기존 출입문 리스트 불러오기  ***************************** 나중에 테이블 생성하는거 보고
        private void Load_OldDoor(String Type)
        {
            string def_value;
            String[][] Table;

            if (Type == "철거 후 신규")
            {
                def_value = "Type = '기존 출입문'";
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

            OldDoor_comboBox.DataSource = sources.DefaultView;
            OldDoor_comboBox.DisplayMember = "Text";
            for (i = 0; i < OldDoor_comboBox.Items.Count; i++)
            {
                var arr = ((DataRowView)OldDoor_comboBox.Items[i]).Row.ItemArray;
                if (arr.Length > 1 && arr[1].ToString() == def_value)
                {
                    OldDoor_comboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        private void OldDoor_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView? item = OldDoor_comboBox.SelectedItem as DataRowView;
            if (item != null)
            {
                OldDoor = OldDoor_comboBox.SelectedItem.ToString();
                //Calc_Ucw();  // 여기 왜 들어가야하는지 나중에 봐보기 
                //Calc_dUinst(); // 여기 왜 들어가야하는지 나중에 봐보기 
            }
        }


        private void Udoor_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Udoor_comboBox.SelectedItem != null)
            {
                UDoorMethod = Udoor_comboBox.SelectedItem.ToString();
                Act_UDoorMethod();
                if (UDoorMethod == "법규")
                {
                    Rule_UD();
                }
            }

            g_textBox.Text = string.Empty;
        }

        private void Act_UDoorMethod()
        {
            if (UDoorMethod == "계산")
            {
                //Calc_Ucw();
                DoorlColor_label.Visible = true;
                DoorColor_comboBox.Visible = true;
                αp_label.Visible = true;
                abs_textBox.Visible = true;
                αp_label2.Visible = true;

                Door_label.Visible = true;
                label1.Visible = true;
                DoorDB_button.Visible = true;
                UCW_g_label.Visible = true;
                g_textBox.Visible = true;
                Ug_unit_label.Visible = true;
                label27.Visible = true;
                DoorL_textBox.Visible = true;
                label22.Visible = true;
                DoorH_textBox.Visible = true;
                label21.Visible = true;
                label30.Visible = true;
                DoorArea_textBox.Visible = true;
                label28.Visible = true;

                g_textBox.Enabled = false;
                g_textBox.BorderStyle = BorderStyle.None;

                label11.Visible = true;
                glass_checkBox.Visible = true;
            }
            else if (UDoorMethod == "법규")
            {
                //Calc_Ucw();
                DoorlColor_label.Visible = true;
                DoorColor_comboBox.Visible = true;
                αp_label.Visible = true;
                abs_textBox.Visible = true;
                αp_label2.Visible = true;
                Door_label.Visible = true;
                DoorL_textBox.Visible = true;
                label22.Visible = true;
                DoorH_textBox.Visible = true;
                label21.Visible = true;
                label30.Visible = true;
                DoorArea_textBox.Visible = true;
                label28.Visible = true;
                UCW_g_label.Visible = true;
                g_textBox.Visible = true;
                Ug_unit_label.Visible = true;
                label27.Visible = true;

                label1.Visible = false;
                DoorDB_textBox.Visible = false;
                label27.Visible = false;
                DoorL_textBox.Visible = false;
                label22.Visible = false;
                DoorH_textBox.Visible = false;
                label21.Visible = false;
                label30.Visible = false;
                DoorArea_textBox.Visible = false;
                label28.Visible = false;
                DoorDB_button.Visible = false;
                label11.Visible = false;
                glass_checkBox.Visible = false;
                g_textBox.Enabled = false;
                g_textBox.BorderStyle = BorderStyle.None;

                GlassArea_label.Visible = false;
                GlassArea_textBox.Visible = false;
                glassArea_label2.Visible = false;

            }
            else if (UDoorMethod == "진단")
            {

                // Calc_Ucw();
                DoorlColor_label.Visible = true;
                DoorColor_comboBox.Visible = true;
                αp_label.Visible = true;
                abs_textBox.Visible = true;
                αp_label2.Visible = true;
                Door_label.Visible = true;
                label1.Visible = true;
                DoorDB_button.Visible = true;
                UCW_g_label.Visible = true;
                g_textBox.Visible = true;
                Ug_unit_label.Visible = true;
                label27.Visible = true;
                DoorL_textBox.Visible = true;
                label22.Visible = true;
                DoorH_textBox.Visible = true;
                label21.Visible = true;
                label30.Visible = true;
                DoorArea_textBox.Visible = true;
                label28.Visible = true;

                label11.Visible = false;
                glass_checkBox.Visible = false;

                g_textBox.Enabled = true;
                g_textBox.BorderStyle = BorderStyle.FixedSingle;

                GlassArea_label.Visible = false;
                GlassArea_textBox.Visible = false;
                glassArea_label2.Visible = false;
            }
            //Calc_dUinst();
            glasscheck();
            Rule_UD();
        }


        private void DoorDB_button_Click(object sender, EventArgs e)
        {
            if (Select[0] == null)
            {
                DoorDB doordb = new DoorDB(null, null, null, null, null, null, null, null, null, null, null, null, null, null);
                DialogResult result = doordb.ShowDialog();

                if (result == DialogResult.OK)
                {
                    for (int i = 0; i < 14; i++)
                    {
                        Select[i] = doordb.Select_Door[i];
                    }

                    //출입문문가로
                    DoorL = Convert.ToDouble(doordb.Select_Door[11]);
                    DoorL_textBox.Text = DoorL.ToString();
                    DoorL2_textBox.Text = DoorL.ToString();
                    //출입문세로
                    DoorH = Convert.ToDouble(doordb.Select_Door[12]);
                    DoorH_textBox.Text = DoorH.ToString();
                    DoorH2_textBox.Text = DoorH.ToString();
                    //출입문열관류율
                    DoorUD = Convert.ToDouble(doordb.Select_Door[13]);
                    g_textBox.Text = DoorUD.ToString();
                    //문짝문틀정보
                    Material = doordb.Select_Door[7];
                    Material_textBox.Text = Material;
                    FrameIn = doordb.Select_Door[4];
                    FrameIn_textBox.Text = FrameIn;
                    DoorIn = doordb.Select_Door[8];
                    DoorIn_textBox.Text = DoorIn;
                    //문틀상하부
                    DoorOver = Convert.ToDouble(doordb.Select_Door[5]);
                    over_textBox.Text = DoorOver.ToString();
                    DoorBottom = Convert.ToDouble(doordb.Select_Door[6]);
                    bottom_textBox.Text = DoorBottom.ToString();
                    //제품명
                    DoorDB = doordb.Select_Door[2];
                    DoorDB_textBox.Text = DoorDB.ToString();

                }
            }
            else
            {
                DoorDB doordb = new DoorDB(Select[0], Select[1], Select[2], Select[3], Select[4], Select[5], Select[6], Select[7], Select[8], Select[9], Select[10], Select[11], Select[12], Select[13]);
                DialogResult result = doordb.ShowDialog();

                if (result == DialogResult.OK)
                {
                    for (int i = 0; i < 14; i++)
                    {
                        Select[i] = doordb.Select_Door[i];
                    }

                    //출입문문가로
                    DoorL = Convert.ToDouble(doordb.Select_Door[11]);
                    DoorL_textBox.Text = DoorL.ToString();
                    DoorL2_textBox.Text = DoorL.ToString();
                    //출입문세로
                    DoorH = Convert.ToDouble(doordb.Select_Door[12]);
                    DoorH_textBox.Text = DoorH.ToString();
                    DoorH2_textBox.Text = DoorH.ToString();
                    //출입문열관류율
                    DoorUD = Convert.ToDouble(doordb.Select_Door[13]);
                    g_textBox.Text = DoorUD.ToString();
                    //문짝문틀정보
                    Material = doordb.Select_Door[7];
                    Material_textBox.Text = Material;
                    FrameIn = doordb.Select_Door[4];
                    FrameIn_textBox.Text = FrameIn;
                    DoorIn = doordb.Select_Door[8];
                    DoorIn_textBox.Text = DoorIn;
                    //문틀상하부
                    DoorOver = Convert.ToDouble(doordb.Select_Door[5]);
                    over_textBox.Text = DoorOver.ToString();
                    DoorBottom = Convert.ToDouble(doordb.Select_Door[6]);
                    bottom_textBox.Text = DoorBottom.ToString();
                    //제품명
                    DoorDB = doordb.Select_Door[2];
                    DoorDB_textBox.Text = DoorDB.ToString();

                }
            }
            Load_DoorType_image3();
        }

        //출입문 치수 정보 
        private void DoorH_textBox_TextChanged(object sender, EventArgs e)
        {
            if (DoorH_textBox.Text != "")
            {
                DoorArea = DoorH * DoorL;
                DoorArea_textBox.Text = String.Format("{0:F1}", DoorArea / 1000000);
                DoorArea2_textBox.Text = String.Format("{0:F1}", DoorArea / 1000000);

                OverL = (2 * DoorH + DoorL) / 1000;
                OverL_textBox.Text = OverL.ToString();
                UnderL = DoorL / 1000;
                UnderL_textBox.Text = UnderL.ToString();
            }
        }

        private void DoorL_textBox_TextChanged(object sender, EventArgs e)
        {
            if (DoorL_textBox.Text != "")
            {
                DoorArea = DoorH * DoorL;
                DoorArea_textBox.Text = String.Format("{0:F1}", DoorArea / 1000000);
                DoorArea2_textBox.Text = String.Format("{0:F1}", DoorArea / 1000000);

                OverL = (2 * DoorH + DoorL) / 1000;
                OverL_textBox.Text = OverL.ToString();
                UnderL = DoorL / 1000;
                UnderL_textBox.Text = UnderL.ToString();
            }
        }

        //유리 치수 정보
        private void GlassH_textBox_TextChanged(object sender, EventArgs e)
        {
            if (glass_checkBox.Checked == true && GlassH_textBox.Text != "")
            {
                GlassH = double.Parse(GlassH_textBox.Text);
                GlassArea = GlassH * GlassL;
                GlassArea_textBox.Text = String.Format("{0:F3}", GlassArea / 1000000);
                GlassArea2_textBox.Text = String.Format("{0:F3}", GlassArea / 1000000);
            }
        }

        private void GlassL_textBox_TextChanged(object sender, EventArgs e)
        {
            if (glass_checkBox.Checked == true && GlassL_textBox.Text != "")
            {
                GlassL = double.Parse(GlassL_textBox.Text);
                GlassArea = GlassH * GlassL;
                GlassArea_textBox.Text = String.Format("{0:F3}", GlassArea / 1000000);
                GlassArea2_textBox.Text = String.Format("{0:F3}", GlassArea / 1000000);
            }
        }

        private void DoorColor_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DoorColor_comboBox.SelectedItem != null)
            {
                DoorColor = DoorColor_comboBox.SelectedItem.ToString();

                String[][] value = Program.DB.getValue(DB.type.BaseDB_HCneed, "흡수율", "흡수율", "외장재색 = '" + DoorColor + "'");
                αd = Convert.ToDouble(value[0][0]);
                abs_textBox.Text = String.Format("{0:F1}", αd);
            }
        }

        private void DiIndi_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DiIndi_comboBox.SelectedItem != null)
            {
                DiIndi = DiIndi_comboBox.SelectedItem.ToString();
                Rule_UD();
                //Calc_dUinst();
            }
            else { }
        }


        //유리 버튼 클릭 시
        private void GlassDB_button_Click(object sender, EventArgs e)
        {
            GlassDB door_glassDB_form = new GlassDB();
            DialogResult result = door_glassDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                GlassName = door_glassDB_form.Select_Glass[1];
                GlassName_textBox.Text = GlassName;
                Ug = Convert.ToDouble(door_glassDB_form.Select_Glass[6]);
                GlassU_textBox.Text = String.Format("{0:F3}", Ug);
            }

            UD_Glass();
        }


        //설치열교 버튼 클릭 시
        private void Install_button_Click_1(object sender, EventArgs e)
        {
            //DoorInstall doorinstall_form   = new DoorInstall(InstallType, installlocation);
            //DialogResult result = doorinstall_form.ShowDialog();

            if (UDoorMethod == "계산")
            {
                if (InstallType == null)
                {
                    MessageBox.Show(" 설치구조유형부터 선택하세요.");
                }
                else
                {
                    DoorInstall DoorInstall_form = new DoorInstall(InstallType, installlocation);
                    DialogResult result = DoorInstall_form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        Install1 = DoorInstall_form.Select_DoorInstall[1];
                        Install2 = DoorInstall_form.Select_DoorInstall[2];
                        Install_textBox.Text = Install2;
                        tabControl1.SelectedTab = tabControl1.TabPages["Install_tabPage"];
                        Psi_InstallTop = Convert.ToDouble(DoorInstall_form.Select_DoorInstall[3]);
                        Psi_InstallSide = Convert.ToDouble(DoorInstall_form.Select_DoorInstall[4]);
                        Psi_InstallButtom = Convert.ToDouble(DoorInstall_form.Select_DoorInstall[5]);

                        Psi_InstallTop_textBox.Text = String.Format("{0:F3}", Psi_InstallTop);
                        Psi_InstallSide_textBox.Text = String.Format("{0:F3}", Psi_InstallSide);
                        Psi_InstallButtom_textBox.Text = String.Format("{0:F3}", Psi_InstallButtom);

                        d_InstallTop_textBox.Text = Convert.ToString(DoorL / 1000);
                        d_InstallSide_textBox.Text = Convert.ToString(DoorH / 1000 * 2);
                        d_InstallButtom_textBox.Text = Convert.ToString(DoorL / 1000);

                        dUinst_textBox.Text = String.Format("{0:F3}", (Psi_InstallTop * (DoorL / 1000) + Psi_InstallSide * (DoorH / 1000 * 2) + Psi_InstallButtom * (DoorL / 1000)) / (DoorArea / 1000000));

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
                    DoorInstall DoorInstall_form = new DoorInstall(InstallType);
                    DialogResult result = DoorInstall_form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        Install1 = DoorInstall_form.Select_DoorInstall[1];
                        Install2 = DoorInstall_form.Select_DoorInstall[2];
                        Install_textBox.Text = Install2;
                        tabControl1.SelectedTab = tabControl1.TabPages["Install_tabPage"];
                        Psi_InstallTop = Convert.ToDouble(DoorInstall_form.Select_DoorInstall[3]);
                        Psi_InstallSide = Convert.ToDouble(DoorInstall_form.Select_DoorInstall[4]);
                        Psi_InstallButtom = Convert.ToDouble(DoorInstall_form.Select_DoorInstall[5]);

                        Psi_InstallTop_textBox.Text = String.Format("{0:F3}", Psi_InstallTop);
                        Psi_InstallSide_textBox.Text = String.Format("{0:F3}", Psi_InstallSide);
                        Psi_InstallButtom_textBox.Text = String.Format("{0:F3}", Psi_InstallButtom);

                        d_InstallTop_textBox.Text = Convert.ToString(DoorL / 1000);
                        d_InstallSide_textBox.Text = Convert.ToString(DoorH / 1000 * 2);
                        d_InstallButtom_textBox.Text = Convert.ToString(DoorL / 1000);

                        dUinst_textBox.Text = String.Format("{0:F3}", (Psi_InstallTop * (DoorL / 1000) + Psi_InstallSide * (DoorH / 1000 * 2) + Psi_InstallButtom * (DoorL / 1000)) / (DoorArea / 1000000));

                    }
                }
            }
            UDinstall_Glass();
            Load_DoorType_image4();
            Load_DoorType_image5();
        }


        //출입문type그림
        private void Load_DoorType_image(String Type)
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "출입문유형이미지", "이미지", "유형 = '" + Type + "'");
            DoorType_pictureBox.Visible = true;
            DoorType_pictureBox.Load(Program.gPath + Image[0][0]);
            DoorType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        //치수정보 그림 
        private void Load_DoorType_image2()
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "출입문유형이미지", "이미지", "유형 = '" + "치수" + "'");
            pictureBox1.Visible = true;
            pictureBox1.Load(Program.gPath + Image[0][0]);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        //문짝문틀정보 그림
        private void Load_DoorType_image3()
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "출입문이미지", "출입문유형", "문짝종류 = '" + Material + "' AND 문짝내부 = '" + DoorIn + "' AND 문틀내부 = '" + FrameIn + "'");
            pictureBox2.Visible = true;
            pictureBox2.Load(Program.gPath + Image[0][0]);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        //설치열교정보 그림
        //상부
        private void Load_DoorType_image4()
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "출입문설치열교이미지", "열교유형이미지", "구분1 = '" + Install1 + "' AND 구분2 = '" + Install2 + "'");
            pictureBox3.Visible = true;
            pictureBox3.Load(Program.gPath + Image[0][0]);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
        }

        //하부
        private void Load_DoorType_image5()
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "출입문설치열교이미지", "하부열교유형이미지", "구분1 = '" + Install1 + "' AND 구분2 = '" + Install2 + "'");
            pictureBox4.Visible = true;
            pictureBox4.Load(Program.gPath + Image[0][0]);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
        }


        //Calc
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ


        public void Rule_UD()
        {
            if (UDoorMethod == "법규")
            {
                String[][] Date = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "법규시기,지역구분", "");
                String[][] Uvalue;
                if (Type == "기존 출입문")
                {
                    Uvalue = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '문' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND  직접간접 =  '" + DiIndi + "'");
                }
                else
                {
                    Uvalue = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '문' And 시기 = '2018.09' AND  지역 ='" + Date[0][1] + "'  AND 직접간접 =  '" + DiIndi + "'");
                }
                UD = Convert.ToDouble(Uvalue[0][0]);
                g_textBox.Text = String.Format("{0:F3}", UD);

                // MessageBox.Show("[(" + Uvalue[0][2] + " 시행)" + Uvalue[0][1] + "] " + Uvalue[0][3] + " 열관류율 적용");
            }
        }


        //유리 포함 문 열관류율 
        public void UD_Glass()
        {
            if (g_textBox.Text != null && DoorArea_textBox.Text != null && GlassArea_textBox.Text != null && GlassU_textBox.Text != null)
            {
                DoorUDGlass = (DoorUD * (DoorArea / 1000000) - (GlassArea / 1000000) + Ug * (GlassArea / 1000000) + DoorOver * (2 * DoorH / 1000 + DoorL / 1000) + DoorBottom * DoorL / 1000) / (DoorArea / 1000000);
                DoorU_textBox.Text = String.Format("{0:F3}", DoorUDGlass);
            }
        }


        //문 유효 열관류율
        public void UDinstall_Glass()
        {
            if (dUinst_textBox.Text != null)
            {
                DoorUDinsGlass = DoorUDGlass + Convert.ToDouble(dUinst_textBox.Text);
                UD_textBox.Text = String.Format("{0:F3}", DoorUDinsGlass);
            }
        }



        private void glass_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            Act_UDoorMethod();
            glasscheck();
            glass_check = glass_checkBox.Checked;
        }

        private void glasscheck()
        {
            if (glass_checkBox.Checked)
            {
                glass1_label.Visible = true;
                GlassL_textBox.Visible = true;
                glass2_label.Visible = true;
                GlassH_textBox.Visible = true;
                label29.Visible = true;
                GlassArea_label.Visible = true;
                GlassArea_textBox.Visible = true;
                glassArea_label2.Visible = true;
                GlassName_textBox.Visible = true;
                GlassDB_button.Visible = true;
                label85.Visible = true;
                GlassU_textBox.Visible = true;
                label84.Visible = true;
                label87.Visible = true;
                DoorU_textBox.Visible = true;
                label86.Visible = true;
                GlassArea2_textBox.Visible = true;
                label35.Visible = true;
                label34.Visible = true;

                Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, DoorColor_comboBox, "출입문", "외장재색", "1");
            }
            else
            {
                glass1_label.Visible = false;
                GlassL_textBox.Visible = false;
                glass2_label.Visible = false;
                GlassH_textBox.Visible = false;
                label29.Visible = false;
                GlassArea_label.Visible = false;
                GlassArea_textBox.Visible = false;
                glassArea_label2.Visible = false;
                GlassName_textBox.Visible = false;
                GlassDB_button.Visible = false;
                label85.Visible = false;
                GlassU_textBox.Visible = false;
                label84.Visible = false;
                label87.Visible = false;
                DoorU_textBox.Visible = false;
                label86.Visible = false;
                GlassArea2_textBox.Visible = false;
                label35.Visible = false;
                label34.Visible = false;
            }
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
                            Install2 = "";
                            Install_textBox.Text = "";
                        }
                    }
                }
                catch { }
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
            if (DoorName == null)
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
            else if (Install2 == null)
            {
                MessageBox.Show("설치열교를 선택하세요.");
            }
            else if (UDoorMethod == "계산")
            {
                if (FrameName == null)
                {
                    MessageBox.Show("프레임을 선택하세요.");
                }
                else if (SpacerName == null)
                {
                    MessageBox.Show("간봉을 선택하세요.");
                }
                else if (glass_checkBox.Checked)
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
                    else if (DoorColor == null)
                    {
                        MessageBox.Show("패널 색를 선택하세요.");
                    }
                    else
                    {
                        Save();
                    }
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
            //치수 입력 안하면 저장 안되도록 메세지 박스 



            Program.DB.setValue(DB.type.ProjDB, "ConstructionCW", "번호,명칭,Type,기존커튼월,Ucw적용방법,직접간접,프레임유형,프레임종류,고정유리종류,개폐유리종류,간봉종류,설치유형,설치종류,LE_CL_V,패널적용유무,출입문적용유무," +
                      "고정유리열관류율,개폐유리열관류율,태양열취득률,빛투과율,고정유리선형열관류율,개폐유리선형열관류율,고정프레임열관류율,개폐프레임열관류율,고정프레임두께,개폐프레임두께," +
                      "상부설치열관류율,측면설치열관류율,하부설치열관류율," +
                      "사이즈명칭,커튼월면적,너비,높이,고정창유리면적,개폐창유리면적,고정창유리둘레길이,개폐창유리둘레길이,M_T프레임면적,개폐창프레임면적," +
                      "커튼월창열관류율,유리부분열관류율,설치열교가산치,커튼월창유효열관류율,유리부분유효열관류율,유리부분유리면적비",
                    "'" + DoorNum_textBox.Text + "','" + DoorName + "','" + Type + "','" + OldDoor + "','" + UDoorMethod + "','" + DiIndi + "','" + FrameType + "','" + FrameName + "','" + FixGlassName + "','" + OpenGlassName + "','" + SpacerName + "','" + InstallType + "','" + Install2 + "','" + LE_CL_V + "','" + glass_check.ToString() + "','" + Door_check.ToString() + "','" +
                    Ug_Fix.ToString() + "','" + Ug_Open.ToString() + "','" + g.ToString() + "','" + τ.ToString() + "','" + Psi_g_fix.ToString() + "','" + Psi_g_open.ToString() + "','" + Uf_mt.ToString() + "','" + Uf_open.ToString() + "','" + df_mt.ToString() + "','" + df_open.ToString() + "','" +
                    Psi_InstallTop.ToString() + "','" + Psi_InstallSide.ToString() + "','" + Psi_InstallButtom.ToString() + "','" +
                    SizeName + "','" + Area.ToString() + "','" + Width.ToString() + "','" + Height.ToString() + "','" + Ag_fix.ToString() + "','" + Ag_open.ToString() + "','" + Lg_fix.ToString() + "','" + Lg_open.ToString() + "','" + Af_mt.ToString() + "','" + Af_open.ToString() + "','" +
                    Ucw.ToString() + "','" + UD.ToString() + "','" + dUinst.ToString() + "','" + Ucw_inst.ToString() + "','" + Ucw_g_inst.ToString() + "','" + Ff_g.ToString()
                    + "'", "번호");

            if (glass_checkBox.Checked)
            {
                Program.DB.setValue(DB.type.ProjDB, "ConstructionCW", "번호," +
                          "패널종류,패널유리종류,LE_CL_V_Panel," +
                          "패널열관류율,패널유리열관류율,패널열전도율,패널흡수율,패널선형열관류율,패널두께," +
                          "패널면적,패널둘레길이," +
                          "패널부분열관류율,패널부분유효열관류율",
                        "'" + DoorNum_textBox.Text + "','" +
                        PanelName + "','" + PanelGlassName + "','" + LE_CL_V_Panel + "','" +
                        Up.ToString() + "','" + Ug_panel.ToString() + "','" + Conductivity_p.ToString() + "','" + αd.ToString() + "','" + Psi_p.ToString() + "','" + dPanel.ToString() + "','" +
                        Ap.ToString() + "','" + Lp.ToString() + "','" +
                        Ucw_p.ToString() + "','" + Ucw_p_inst.ToString()
                        + "'", "번호");
            }
            this.DialogResult = DialogResult.OK;
            this.Hide();
            Program.getMenuForm().DoLoadForm(30, OnLoadListProc);
        }

        private void reset()
        {
            DoorNum_textBox.Text = null;
            Name_textBox.Text = null;

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;

            OldDoor_comboBox.SelectedItem = null;
            Udoor_comboBox.SelectedItem = null;
            DiIndi_comboBox.SelectedItem = null;
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();
            try
            {
                DoorNum_textBox.Text = ID;
                CWNum = ID;

                String[][] Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "번호,명칭,Type,기존커튼월,Ucw적용방법,직접간접,프레임유형,프레임종류,고정유리종류,개폐유리종류,간봉종류,설치유형,설치종류,LE_CL_V," +
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
                    case "기존 출입문":
                        radioButton1.Checked = true;
                        break;

                    case "신규 출입문":
                        radioButton2.Checked = true;
                        break;

                    case "철거 후 신규":
                        radioButton3.Checked = true;
                        break; ;
                }
                OldDoor = Load[0][3];
                OldDoor_comboBox.SelectedItem = OldDoor;
                UDoorMethod = Load[0][4];
                Udoor_comboBox.SelectedItem = UDoorMethod;
                DiIndi = Load[0][5];
                DiIndi_comboBox.SelectedItem = DiIndi;

                FrameType = Load[0][6];
                check_FrameType = FrameType;


            }
            catch { }
        }

        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            DoorNum_textBox.Text = ID;
            CWNum = ID;
        }

    }
}
