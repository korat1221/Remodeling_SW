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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace main.contents
{
    public partial class ConstructionDoor : Form
    {

        double DoorArea, DoorL, DoorH, GlassL, GlassH, GlassArea;//치수정보
        double DoorUD, DoorOver, DoorBottom, DoorUDGlass, DoorUDinsGlass, DoorThk, αd;//문열관류율,문틀상하부열관류율,유리반영문열관류율,유효문열관류율, 문짝두께, 문흡수율
        String Material, FrameIn, DoorIn, DoorDB, DoorInsul; //문짝문틀정보,제품명
        double OverL, UnderL;//상부하부길이
        String[] Select = new String[14];

        private String DoorNum;
        String DoorName, Type, OldDoor, DoorColor, UDoorMethod, DiIndi, InstallType, installlocation, check_InstallType, Install2, Install1, Install0, GlassName;
        double Psi_InstallTop, Psi_InstallSide, Psi_InstallBottom;
        double Ug, UD; //유리열관류율, 법규열관류율
        Boolean glass_check;
        String DBType, DBName, DBName2;   //Door유형, 제품명, 제조사, 문짝종류


        public ConstructionDoor()
        {
            InitializeComponent();
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
                Table = Program.DB.getValue(DB.type.ProjDB, "ConstructionDoor", "명칭", def_value);

            }
            else
            {
                def_value = "Type = ''";
                Table = Program.DB.getValue(DB.type.ProjDB, "ConstructionDoor", "명칭", def_value);
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
                //DoorDB doordb = new DoorDB(null, null, null, null, null, null, null, null, null, null, null, null, null, null);
                DoorDB doordb = new DoorDB(DoorNum, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
                DialogResult result = doordb.ShowDialog();

                if (result == DialogResult.OK)
                {
                    for (int i = 0; i < 14; i++)
                    {
                        Select[i] = doordb.Select_Door[i];
                    }

                    //
                    DBType = doordb.Select_Door[1];
                    DBName = doordb.Select_Door[2];
                    DBName2 = doordb.Select_Door[3];

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
                    DoorInsul = doordb.Select_Door[9]; // 단열재종류
                    DoorThk = Convert.ToDouble(doordb.Select_Door[10]); // 문짝두께
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
                DoorDB doordb = new DoorDB(DoorNum, Select[0], Select[1], Select[2], Select[3], Select[4], Select[5], Select[6], Select[7], Select[8], Select[9], Select[10], Select[11], Select[12], Select[13]);
                DialogResult result = doordb.ShowDialog();

                if (result == DialogResult.OK)
                {
                    for (int i = 0; i < 14; i++)
                    {
                        Select[i] = doordb.Select_Door[i];
                    }

                    //번호



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
                    DoorInsul = doordb.Select_Door[9]; // 단열재종류
                    DoorThk = Convert.ToDouble(doordb.Select_Door[10]); // 문짝두께
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
            UD_Glass();
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
                        Install0 = DoorInstall_form.Select_DoorInstall[0];//제품명
                        Install1 = DoorInstall_form.Select_DoorInstall[1];
                        Install2 = DoorInstall_form.Select_DoorInstall[2];
                        Install_textBox.Text = Install2;
                        tabControl1.SelectedTab = tabControl1.TabPages["Install_tabPage"];
                        Psi_InstallTop = Convert.ToDouble(DoorInstall_form.Select_DoorInstall[3]);
                        Psi_InstallSide = Convert.ToDouble(DoorInstall_form.Select_DoorInstall[4]);
                        Psi_InstallBottom = Convert.ToDouble(DoorInstall_form.Select_DoorInstall[5]);

                        Psi_InstallTop_textBox.Text = String.Format("{0:F3}", Psi_InstallTop);
                        Psi_InstallSide_textBox.Text = String.Format("{0:F3}", Psi_InstallSide);
                        Psi_InstallBottom_textBox.Text = String.Format("{0:F3}", Psi_InstallBottom);

                        d_InstallTop_textBox.Text = Convert.ToString(DoorL / 1000);
                        d_InstallSide_textBox.Text = Convert.ToString(DoorH / 1000 * 2);
                        d_InstallBottom_textBox.Text = Convert.ToString(DoorL / 1000);

                        dUinst_textBox.Text = String.Format("{0:F3}", (Psi_InstallTop * (DoorL / 1000) + Psi_InstallSide * (DoorH / 1000 * 2) + Psi_InstallBottom * (DoorL / 1000)) / (DoorArea / 1000000));

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
                        Install0 = DoorInstall_form.Select_DoorInstall[0];//제품명
                        Install1 = DoorInstall_form.Select_DoorInstall[1];
                        Install2 = DoorInstall_form.Select_DoorInstall[2];
                        Install_textBox.Text = Install2;
                        tabControl1.SelectedTab = tabControl1.TabPages["Install_tabPage"];
                        Psi_InstallTop = Convert.ToDouble(DoorInstall_form.Select_DoorInstall[3]);
                        Psi_InstallSide = Convert.ToDouble(DoorInstall_form.Select_DoorInstall[4]);
                        Psi_InstallBottom = Convert.ToDouble(DoorInstall_form.Select_DoorInstall[5]);

                        Psi_InstallTop_textBox.Text = String.Format("{0:F3}", Psi_InstallTop);
                        Psi_InstallSide_textBox.Text = String.Format("{0:F3}", Psi_InstallSide);
                        Psi_InstallBottom_textBox.Text = String.Format("{0:F3}", Psi_InstallBottom);

                        d_InstallTop_textBox.Text = Convert.ToString(DoorL / 1000);
                        d_InstallSide_textBox.Text = Convert.ToString(DoorH / 1000 * 2);
                        d_InstallBottom_textBox.Text = Convert.ToString(DoorL / 1000);

                        dUinst_textBox.Text = String.Format("{0:F3}", (Psi_InstallTop * (DoorL / 1000) + Psi_InstallSide * (DoorH / 1000 * 2) + Psi_InstallBottom * (DoorL / 1000)) / (DoorArea / 1000000));

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
                DoorUDGlass = (DoorUD * ((DoorArea / 1000000) - (GlassArea / 1000000)) + Ug * (GlassArea / 1000000) + DoorOver * (2 * DoorH / 1000 + DoorL / 1000) + DoorBottom * DoorL / 1000) / (DoorArea / 1000000);
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
                //label87.Visible = true;
                //DoorU_textBox.Visible = true;
                //label86.Visible = true;
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
                //label87.Visible = false;
                //DoorU_textBox.Visible = false;
                //label86.Visible = false;
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
                MessageBox.Show("외부출입문 명칭을 입력하세요.");
            }
            else if (Type == null)
            {
                MessageBox.Show("출입문 리모델링 유형을 선택하세요.");
            }
            else if (Install2 == null)
            {
                MessageBox.Show("설치열교를 선택하세요.");
            }
            else if (UDoorMethod == "계산")
            {
                if (glass_checkBox.Checked)
                {
                    if (GlassL == null || GlassH == null)
                    {
                        MessageBox.Show("유리 치수를 입력하세요.");
                    }
                    else if (GlassName == null)
                    {
                        MessageBox.Show("유리 종류를 선택하세요.");
                    }
                    else
                    {
                        Save();
                    }
                }
                else { Save(); };
            }
            else
            {
                Save();
            }
        }

        public static bool OnLoadListProc(Form form)
        {
            List_ConstructionDoor f = (List_ConstructionDoor)form;

            f.load_List();

            return true;
        }


        private void Save()
        {
            //치수 입력 안하면 저장 안되도록 메세지 박스 

            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            Program.DB.setValue(DB.type.ProjDB, "ConstructionDoor", "번호,프로젝트유형,명칭,Type,기존출입문,UD적용방법,직접간접,문짝제품,출입문재질,문틀내부,문짝내부유형,문짝색,흡수율,문짝단열재종류," +
                      "문짝두께,문짝열관류율,문틀상부측면열관류율,문틀하부열관류율,문면적,문높이,문길이,유리적용유무," +
                      "설치유형,설치유형2,상부측면설치길이,하부설치길이,상부선형열관류율,측면부선형열관류율,하부선형열관류율,상부설치길이,측면설치길이,하부설치길이2,열교가산치," +
                      "문유효열관류율,Door유형,제품명,제조사",
                    "'" + DoorNum_textBox.Text + "','" + 프로젝트유형[0][0] + "','" + DoorName + "','" + Type + "','" + OldDoor + "','" + UDoorMethod + "','" + DiIndi + "','" + DoorDB + "','" + Material + "','" + FrameIn + "','" + DoorIn + "','" + DoorColor + "','" + αd.ToString() + "','" + DoorInsul + "','" +
                    DoorThk.ToString() + "','" + DoorUD.ToString() + "','" + DoorOver.ToString() + "','" + DoorBottom.ToString() + "','" + DoorArea_textBox.Text + "','" + DoorH.ToString() + "','" + DoorL.ToString() + "','" + glass_check.ToString() + "','" +
                    InstallType + "','" + Install2 + "','" + OverL.ToString() + "','" + UnderL.ToString() + "','" + Psi_InstallTop.ToString() + "','" + Psi_InstallSide.ToString() + "','" + Psi_InstallBottom.ToString() + "','" + d_InstallTop_textBox.Text + "','" + d_InstallSide_textBox.Text + "','" + d_InstallBottom_textBox.Text + "','" + dUinst_textBox.Text + "','" +
                    DoorUDinsGlass.ToString() + "','" + DBType + "','" + DBName + "','" + DBName2
                    + "'", "번호");



            //유리 있을 경우 
            if (glass_checkBox.Checked)
            {
                Program.DB.setValue(DB.type.ProjDB, "ConstructionDoor", "번호," +
                          "유리가로,유리세로,유리종류,유리면적,유리열관류율,문열관류율",
                        "'" + DoorNum_textBox.Text + "','" +
                        GlassL.ToString() + "','" + GlassH.ToString() + "','" + GlassName + "','" +
                       GlassArea_textBox.Text + "','" + Ug.ToString() + "','" + DoorUDGlass.ToString()
                        + "'", "번호");
            }
            else { }
            this.DialogResult = DialogResult.OK;
            this.Hide();
            Program.getMenuForm().DoLoadForm(51, OnLoadListProc);
        }



        private void reset()
        {
            Name_textBox.Text = null;
            Type_textBox.Text = null;

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;

            OldDoor_comboBox.SelectedItem = null;

            Udoor_comboBox.SelectedItem = null;
            DiIndi_comboBox.SelectedItem = null;

            DoorColor_comboBox.SelectedItem = null;
            abs_textBox.Text = null;

            DoorDB_textBox.Text = null;
            g_textBox.Text = null;

            DoorL_textBox.Text = null;
            DoorH_textBox.Text = null;
            DoorArea_textBox.Text = null;

            glass_checkBox.Checked = false;
            GlassL_textBox.Text = null;
            GlassH_textBox.Text = null;
            GlassArea_textBox.Text = null;
            GlassName_textBox.Text = null;
            GlassU_textBox.Text = null;
            DoorU_textBox.Text = null;

            Install_comboBox.SelectedItem = null;
            Install_textBox.Text = null;
            UD_textBox.Text = null;

            Material_textBox.Text = null;
            FrameIn_textBox.Text = null;
            DoorIn_textBox.Text = null;
            over_textBox.Text = null;
            bottom_textBox.Text = null;
            OverL_textBox.Text = null;
            UnderL_textBox.Text = null;

            DoorL2_textBox.Text = null;
            DoorH2_textBox.Text = null;

            DoorArea2_textBox.Text = null;
            GlassArea2_textBox.Text = null;

            dUinst_textBox.Text = null;
            Psi_InstallTop_textBox.Text = null;
            Psi_InstallSide_textBox.Text = null;
            Psi_InstallBottom_textBox.Text = null;

            d_InstallTop_textBox.Text = null;
            d_InstallSide_textBox.Text = null;
            d_InstallBottom_textBox.Text = null;

            DoorType_pictureBox.Visible = false;
            pictureBox2.Visible = false;
            pictureBox1.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;


            DoorArea = 0; DoorL = 0; DoorH = 0; GlassL = 0; GlassH = 0; GlassArea = 0;
            DoorUD = 0; DoorOver = 0; DoorBottom = 0; DoorUDGlass = 0; DoorUDinsGlass = 0; DoorThk = 0; αd = 0;
            Material = null; FrameIn = null; DoorIn = null; DoorDB = null; DoorInsul = null;
            OverL = 0; UnderL = 0;
            Select = null;

            DoorName = null; Type = null; OldDoor = null; DoorColor = null; UDoorMethod = null; DiIndi = null; InstallType = null;
            installlocation = null; check_InstallType = null; Install2 = null; Install1 = null; InstallType = null; GlassName = null;
            Psi_InstallTop = 0; Psi_InstallSide = 0; Psi_InstallBottom = 0;
            Ug = 0; UD = 0;
            glass_check = false;
        }


        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();
            try
            {
                DoorNum_textBox.Text = ID;
                DoorNum = ID;

                String[][] Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionDoor", "번호,명칭,Type,기존출입문,UD적용방법,직접간접,문짝제품,출입문재질,문틀내부,문짝내부유형,문짝색,흡수율,문짝단열재종류," +
                      "문짝두께,문짝열관류율,문틀상부측면열관류율,문틀하부열관류율,문면적,문높이,문길이,유리적용유무," +
                      "설치유형,설치유형2,상부측면설치길이,하부설치길이,상부선형열관류율,측면부선형열관류율,하부선형열관류율,상부설치길이,측면설치길이,하부설치길이2,열교가산치," +
                      "문유효열관류율"
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
                DoorColor = Load[0][10];
                DoorColor_comboBox.SelectedItem = DoorColor;
                αd = Convert.ToDouble(Load[0][11]);
                abs_textBox.Text = αd.ToString();
                DoorDB = Load[0][6];
                DoorDB_textBox.Text = DoorDB;
                DoorUD = Convert.ToDouble(Load[0][14]);
                g_textBox.Text = DoorUD.ToString();
                DoorL = Convert.ToDouble(Load[0][19]);
                DoorL_textBox.Text = DoorL.ToString();
                DoorL2_textBox.Text = DoorL.ToString();
                DoorH = Convert.ToDouble(Load[0][18]);
                DoorH_textBox.Text = DoorH.ToString();
                DoorH2_textBox.Text = DoorH.ToString();
                DoorArea_textBox.Text = Load[0][17];
                DoorArea2_textBox.Text = Load[0][17];

                glass_check = Convert.ToBoolean(Load[0][20]);
                glass_checkBox.Checked = Convert.ToBoolean(Load[0][20]);

                InstallType = Load[0][21];
                Install_comboBox.SelectedItem = InstallType;
                Install2 = Load[0][22];
                Install_textBox.Text = Install2;

                DoorUDinsGlass = Convert.ToDouble(Load[0][32]);
                UD_textBox.Text = String.Format("{0:F3}", DoorUDinsGlass);

                //문짝문틀정보
                Material = Load[0][7];
                Material_textBox.Text = Material;
                FrameIn = Load[0][8];
                FrameIn_textBox.Text = FrameIn;
                DoorIn = Load[0][9];
                DoorIn_textBox.Text = DoorIn;

                DoorOver = Convert.ToDouble(Load[0][15]);
                over_textBox.Text = DoorOver.ToString();
                DoorBottom = Convert.ToDouble(Load[0][16]);
                bottom_textBox.Text = DoorBottom.ToString();
                OverL = Convert.ToDouble(Load[0][23]);
                OverL_textBox.Text = OverL.ToString();
                UnderL = Convert.ToDouble(Load[0][24]);
                UnderL_textBox.Text = Convert.ToString(UnderL.ToString());

                //설치열교정보
                Psi_InstallTop = Convert.ToDouble(Load[0][25]);
                Psi_InstallTop_textBox.Text = Psi_InstallTop.ToString();
                Psi_InstallSide = Convert.ToDouble(Load[0][26]);
                Psi_InstallSide_textBox.Text = Psi_InstallSide.ToString();
                Psi_InstallBottom = Convert.ToDouble(Load[0][27]);
                Psi_InstallBottom_textBox.Text = Psi_InstallBottom.ToString();

                d_InstallTop_textBox.Text = Load[0][28];
                d_InstallSide_textBox.Text = Load[0][29];
                d_InstallBottom_textBox.Text = Load[0][30];

                DoorUDinsGlass = Convert.ToDouble(Load[0][31]);
                dUinst_textBox.Text = String.Format("{0:F3}", DoorUDinsGlass);

                Load_DoorType_image2();
                Load_DoorType_image3();
                Load_DoorType_image4();
                Load_DoorType_image5();


            }
            catch { }


            if (glass_check == true)
            {
                try
                {
                    String[][] Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionDoor", "유리가로,유리세로,유리종류,유리면적,유리열관류율,문열관류율"
                  , "번호 = '" + ID + "'");

                    GlassL = Convert.ToDouble(Load[0][0]);
                    GlassL_textBox.Text = GlassL.ToString();
                    GlassH = Convert.ToDouble(Load[0][1]);
                    GlassH_textBox.Text = GlassH.ToString();

                    GlassName = Load[0][2];
                    GlassName_textBox.Text = GlassName;
                    GlassArea_textBox.Text = Load[0][3];
                    GlassArea2_textBox.Text = Load[0][3];

                    Ug = Convert.ToDouble(Load[0][4]);
                    GlassU_textBox.Text = Ug.ToString();
                    DoorUDGlass = Convert.ToDouble(Load[0][5]);
                    DoorU_textBox.Text = String.Format("{0:F3}", DoorUDGlass);
                }
                catch { }
            }


        }

        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            DoorNum_textBox.Text = ID;
            DoorNum = ID;
        }

    }
}
