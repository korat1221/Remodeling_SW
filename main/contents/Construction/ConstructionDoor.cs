using main.contentslist;
using main.info;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace main.contents
{
    public partial class ConstructionDoor : Form, IConfirmable
    {

        double DoorArea, DoorL, DoorH, GlassL, GlassH, GlassArea;//치수정보
        double DoorUD, DoorOver, DoorBottom, DoorUDGlass, DoorUDinsGlass, DoorThk, αd;//문열관류율,문틀상하부열관류율,유리반영문열관류율,유효문열관류율, 문짝두께, 문흡수율
        String Material, FrameIn, DoorIn, DoorDB, DoorInsul; //문짝문틀정보,제품명
        String[] Select = new String[14];

        private String DoorNum;
        String DoorName, Type, OldDoor, DoorColor, UDoorMethod, DiIndi, InstallType, installlocation, check_InstallType, Install2, Install1, Install0, GlassName;
        double Psi_InstallTop, Psi_InstallSide, Psi_InstallBottom;
        double Ug, UD; //유리열관류율, 법규열관류율
        Boolean glass_check;
        String DBType, DBName, DBName2;   //Door유형, 제품명, 제조사, 문짝종류


        public ConstructionDoor()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '외부출입문'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

            //직접간접 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, DiIndi_comboBox, "출입문", "실외조건", "1");
            //설치위치 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, Install_comboBox, "출입문", "구조", "1");

            glass_checkBox.Checked = true;
            glass_checkBox.Checked = false;

            //치수정보 그림 시작하자마자 불러오기
            Load_DoorType_image2();
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
        }

        private void OldDoor_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView? item = OldDoor_comboBox.SelectedItem as DataRowView;
            if (item != null)
            {
                OldDoor = item.Row.ItemArray[0].ToString();
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
                    UD_Glass();
                }
            }

            //g_textBox.Text = string.Empty;
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

                Door_label.Visible = true;
                label1.Visible = true;
                DoorDB_button.Visible = true;
                UCW_g_label.Visible = true;
                UD1_textBox.Visible = true;
                label27.Visible = true;
                DoorL_textBox.Visible = true;
                label22.Visible = true;
                DoorH_textBox.Visible = true;
                label30.Visible = true;
                DoorArea_textBox.Visible = true;

                UD1_textBox.Enabled = false;
                UD1_textBox.BorderStyle = BorderStyle.None;

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
                Door_label.Visible = true;
                DoorL_textBox.Visible = true;
                label22.Visible = true;
                DoorH_textBox.Visible = true;
                label30.Visible = true;
                DoorArea_textBox.Visible = true;
                UCW_g_label.Visible = true;
                UD1_textBox.Visible = true;
                label27.Visible = true;

                label1.Visible = false;
                DoorDB_textBox.Visible = false;
                label27.Visible = false;
                DoorL_textBox.Visible = false;
                label22.Visible = false;
                DoorH_textBox.Visible = false;
                label30.Visible = false;
                DoorArea_textBox.Visible = false;
                DoorDB_button.Visible = false;
                label11.Visible = false;
                glass_checkBox.Visible = false;
                UD1_textBox.Enabled = false;
                UD1_textBox.BorderStyle = BorderStyle.None;

                GlassArea_label.Visible = false;
                GlassArea_textBox.Visible = false;

            }
            else if (UDoorMethod == "진단")
            {

                // Calc_Ucw();
                DoorlColor_label.Visible = true;
                DoorColor_comboBox.Visible = true;
                αp_label.Visible = true;
                abs_textBox.Visible = true;
                Door_label.Visible = true;
                label1.Visible = true;
                DoorDB_button.Visible = true;
                UCW_g_label.Visible = true;
                UD1_textBox.Visible = true;
                label27.Visible = true;
                DoorL_textBox.Visible = true;
                label22.Visible = true;
                DoorH_textBox.Visible = true;
                label30.Visible = true;
                DoorArea_textBox.Visible = true;

                label11.Visible = false;
                glass_checkBox.Visible = false;

                UD1_textBox.Enabled = true;
                UD1_textBox.BorderStyle = BorderStyle.FixedSingle;

                GlassArea_label.Visible = false;
                GlassArea_textBox.Visible = false;
            }
            //Calc_dUinst();
            glasscheck();
            Rule_UD();
        }

        private void Doorsize()
        {
            if (UDoorMethod == "법규")
            {
                //출입문문가로
                DoorL = 900;
                DoorL_textBox.Text = DoorL.ToString() + " mm";
                DoorL2_textBox.Text = DoorL.ToString() + " mm";
                //출입문세로
                DoorH = 2100;
                DoorH_textBox.Text = DoorH.ToString() + " mm";
                DoorH2_textBox.Text = DoorH.ToString() + " mm";
            }
            else;
        }

        private void DoorDB_button_Click(object sender, EventArgs e)
        {
            if (Select[1] == null)
            {
                //DoorDB doordb = new DoorDB(null, null, null, null, null, null, null, null, null, null, null, null, null, null);
                string[] select = new string[14];
                DoorDB doordb = new DoorDB(DoorNum, select, DiIndi);
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

                    Doorsize();
                    //출입문열관류율
                    DoorUD = Program.UTIL.ToDoubleOrZero(doordb.Select_Door[13]);
                    UD1_textBox.Text = DoorUD.ToString() + " W/m" + Program.UTIL.Subscript(2, true) + "·K";

                    //문짝문틀정보
                    Material = doordb.Select_Door[7];
                    Material_textBox.Text = Material;
                    FrameIn = doordb.Select_Door[4];
                    FrameIn_textBox.Text = FrameIn;
                    DoorIn = doordb.Select_Door[8];
                    DoorIn_textBox.Text = DoorIn;
                    DoorInsul = doordb.Select_Door[9]; // 단열재종류
                    if (DBType == "기본")
                    {
                        DoorThk = Program.UTIL.ToDoubleOrZero(doordb.Select_Door[10]); // 문짝두께
                    }
                    else
                    {
                        DoorThk = 0;
                    }
                    //문틀상하부
                    DoorOver = doordb.Select_Door[5]=="" ?  0: Program.UTIL.ToDoubleOrZero(doordb.Select_Door[5]);
                    over_textBox.Text = DoorOver.ToString() + " W/m" + Program.UTIL.Subscript(2, true) + "·K";
                    DoorBottom = doordb.Select_Door[6] == "" ? 0 : Program.UTIL.ToDoubleOrZero(doordb.Select_Door[6]);
                    bottom_textBox.Text =  DoorBottom.ToString() + " W/m" + Program.UTIL.Subscript(2, true) + "·K";
                    //제품명
                    DoorDB = doordb.Select_Door[2];
                    DoorDB_textBox.Text = DoorDB.ToString();


                    if (DoorDB_textBox.Text != null)
                    {
                        //출입문문가로
                        DoorL = Program.UTIL.ToDoubleOrZero(doordb.Select_Door[11]);
                        DoorL_textBox.Text = DoorL.ToString() + " mm";
                        DoorL2_textBox.Text = DoorL.ToString() + " mm";
                        //출입문세로
                        DoorH = Program.UTIL.ToDoubleOrZero(doordb.Select_Door[12]);
                        DoorH_textBox.Text = DoorH.ToString() + " mm";
                        DoorH2_textBox.Text = DoorH.ToString() + " mm";
                    }
                    else;
                }
            }
            else
            {
                DoorDB doordb = new DoorDB(DoorNum, Select, DiIndi);
                DialogResult result = doordb.ShowDialog();

                if (result == DialogResult.OK)
                {
                    for (int i = 0; i < 14; i++)
                    {
                        Select[i] = doordb.Select_Door[i];
                    }

                    Doorsize();


                    //출입문열관류율
                    DoorUD = Program.UTIL.ToDoubleOrZero(doordb.Select_Door[13]);
                    UD1_textBox.Text = DoorUD.ToString() + " W/m" + Program.UTIL.Subscript(2, true) + "·K";
                    //문짝문틀정보
                    Material = doordb.Select_Door[7];
                    Material_textBox.Text = Material;
                    FrameIn = doordb.Select_Door[4];
                    FrameIn_textBox.Text = FrameIn;
                    DoorIn = doordb.Select_Door[8];
                    DoorIn_textBox.Text = DoorIn;
                    DoorInsul = doordb.Select_Door[9]; // 단열재종류
                    if (DBType == "기본")
                    {
                        DoorThk = Program.UTIL.ToDoubleOrZero(doordb.Select_Door[10]); // 문짝두께
                    }
                    else
                    {
                        DoorThk = 0;
                    }
                    //문틀상하부
                    DoorOver = doordb.Select_Door[5]=="" ?  0 : Program.UTIL.ToDoubleOrZero(doordb.Select_Door[5]);
                    over_textBox.Text = DoorOver.ToString() + " W/m" + Program.UTIL.Subscript(2, true) + "·K";
                    DoorBottom = doordb.Select_Door[6]=="" ? 0 :  Program.UTIL.ToDoubleOrZero(doordb.Select_Door[6]);
                    bottom_textBox.Text = DoorBottom.ToString() + " W/m" + Program.UTIL.Subscript(2, true) + "·K";
                    //제품명
                    DoorDB = doordb.Select_Door[2];
                    DoorDB_textBox.Text = DoorDB.ToString();

                    DBType = doordb.Select_Door[1];
                    DBName = doordb.Select_Door[2];
                    DBName2 = doordb.Select_Door[3];

                    if (DoorDB_textBox.Text != null)
                    {
                        //출입문문가로
                        DoorL = Program.UTIL.ToDoubleOrZero(doordb.Select_Door[11]);
                        DoorL_textBox.Text = DoorL.ToString() + " mm";
                        DoorL2_textBox.Text = DoorL.ToString() + " mm";
                        //출입문세로
                        DoorH = Program.UTIL.ToDoubleOrZero(doordb.Select_Door[12]);
                        DoorH_textBox.Text = DoorH.ToString() + " mm";
                        DoorH2_textBox.Text = DoorH.ToString() + " mm";
                    }
                    else;

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
                DoorArea_textBox.Text = String.Format("{0:F1}", DoorArea / 1000000) + " m" + Program.UTIL.Subscript(2, true);
                DoorArea2_textBox.Text = String.Format("{0:F1}", DoorArea / 1000000) + " m" + Program.UTIL.Subscript(2, true);

                OverL_textBox.Text = ((DoorL + DoorH * 2) / 1000).ToString() + " m";
                UnderL_textBox.Text = (DoorL / 1000).ToString() + " m";
            }
        }

        private void DoorL_textBox_TextChanged(object sender, EventArgs e)
        {
            if (DoorL_textBox.Text != "")
            {
                DoorArea = DoorH * DoorL;
                DoorArea_textBox.Text = String.Format("{0:F1}", DoorArea / 1000000) + " m" + Program.UTIL.Subscript(2, true);
                DoorArea2_textBox.Text = String.Format("{0:F1}", DoorArea / 1000000) + " m" + Program.UTIL.Subscript(2, true);

                OverL_textBox.Text = ((DoorL + DoorH * 2 / 1000)).ToString() + " m";
                UnderL_textBox.Text = (DoorL / 1000).ToString() + " m";
            }
        }

        //유리 치수 정보
        private void GlassH_textBox_TextChanged(object sender, EventArgs e)
        {
            if (glass_checkBox.Checked == true && GlassH_textBox.Text != "")
            {
                GlassH = double.Parse(GlassH_textBox.Text);
                GlassArea = GlassH * GlassL;
                GlassArea_textBox.Text = String.Format("{0:F3}", GlassArea / 1000000) + " m" + Program.UTIL.Subscript(2, true);
                GlassArea2_textBox.Text = String.Format("{0:F3}", GlassArea / 1000000) + " m" + Program.UTIL.Subscript(2, true);
            }
        }

        private void GlassL_textBox_TextChanged(object sender, EventArgs e)
        {
            if (glass_checkBox.Checked == true && GlassL_textBox.Text != "")
            {
                GlassL = double.Parse(GlassL_textBox.Text);
                GlassArea = GlassH * GlassL;
                GlassArea_textBox.Text = String.Format("{0:F3}", GlassArea / 1000000) + " m" + Program.UTIL.Subscript(2, true);
                GlassArea2_textBox.Text = String.Format("{0:F3}", GlassArea / 1000000) + " m" + Program.UTIL.Subscript(2, true);
            }
        }

        private void DoorColor_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DoorColor_comboBox.SelectedItem != null)
            {
                DoorColor = DoorColor_comboBox.SelectedItem.ToString();

                String[][] value = Program.DB.getValue(DB.type.BaseDB_HCneed, "흡수율", "흡수율", "외장재색 = '" + DoorColor + "'");
                if (value.Length > 0)
                {
                    αd = Program.UTIL.ToDoubleOrZero(value[0][0]);
                    abs_textBox.Text = String.Format("{0:F1}", αd);
                }
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
                Ug = Program.UTIL.ToDoubleOrZero(door_glassDB_form.Select_Glass[6]);
                GlassU_textBox.Text = String.Format("{0:F3}", Ug) + " W/m" + Program.UTIL.Subscript(2, true) + "·K";
            }

            UD_Glass();
        }


        //설치열교 버튼 클릭 시
        private void Install_button_Click_1(object sender, EventArgs e)
        {
            //DoorInstall doorinstall_form   = new DoorInstall(InstallType, installlocation);
            //DialogResult result = doorinstall_form.ShowDialog();
            Doorsize(); ;
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
                        Install1 = DoorInstall_form.Select_DoorInstall[1];//구분1
                        Install2 = DoorInstall_form.Select_DoorInstall[2];//구분2
                        Install_textBox.Text = Install2;
                        tabControl1.SelectedTab = tabControl1.TabPages["Install_tabPage"];
                        Psi_InstallTop = Program.UTIL.ToDoubleOrZero(DoorInstall_form.Select_DoorInstall[3]);
                        Psi_InstallSide = Program.UTIL.ToDoubleOrZero(DoorInstall_form.Select_DoorInstall[4]);
                        Psi_InstallBottom = Program.UTIL.ToDoubleOrZero(DoorInstall_form.Select_DoorInstall[5]);

                        Psi_InstallTop_textBox.Text = String.Format("{0:F3}", Psi_InstallTop) + " W/m·K";
                        Psi_InstallSide_textBox.Text = String.Format("{0:F3}", Psi_InstallSide) + " W/m·K";
                        Psi_InstallBottom_textBox.Text = String.Format("{0:F3}", Psi_InstallBottom) + " W/m·K";

                        d_InstallTop_textBox.Text = Convert.ToString(DoorL / 1000) + " m";
                        d_InstallSide_textBox.Text = Convert.ToString(DoorH / 1000 * 2) + " m";
                        d_InstallBottom_textBox.Text = Convert.ToString(DoorL / 1000) + " m";

                        dUinst_textBox.Text = String.Format("{0:F3}", (Psi_InstallTop * (DoorL / 1000) + Psi_InstallSide * (DoorH / 1000 * 2) + Psi_InstallBottom * (DoorL / 1000)) / (DoorArea / 1000000)) + " W/m" + Program.UTIL.Subscript(2, true) + "·K";

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
                        Psi_InstallTop = Program.UTIL.ToDoubleOrZero(DoorInstall_form.Select_DoorInstall[3]);
                        Psi_InstallSide = Program.UTIL.ToDoubleOrZero(DoorInstall_form.Select_DoorInstall[4]);
                        Psi_InstallBottom = Program.UTIL.ToDoubleOrZero(DoorInstall_form.Select_DoorInstall[5]);

                        Psi_InstallTop_textBox.Text = String.Format("{0:F3}", Psi_InstallTop) + " W/m·K";
                        Psi_InstallSide_textBox.Text = String.Format("{0:F3}", Psi_InstallSide) + " W/m·K";
                        Psi_InstallBottom_textBox.Text = String.Format("{0:F3}", Psi_InstallBottom) + " W/m·K";

                        d_InstallTop_textBox.Text = Convert.ToString(DoorL / 1000) + " m";
                        d_InstallSide_textBox.Text = Convert.ToString(DoorH / 1000 * 2) + " m";
                        d_InstallBottom_textBox.Text = Convert.ToString(DoorL / 1000) + " m";

                        dUinst_textBox.Text = String.Format("{0:F3}", (Psi_InstallTop * (DoorL / 1000) + Psi_InstallSide * (DoorH / 1000 * 2) + Psi_InstallBottom * (DoorL / 1000)) / (DoorArea / 1000000)) + " W/m" + Program.UTIL.Subscript(2, true) + "·K";

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
            if (Image.Length > 0)
            {
                DoorType_pictureBox.Visible = true;
                DoorType_pictureBox.Load(Program.gPath + Image[0][0]);
                DoorType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
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
            if (Image.Length > 0)
            {
                pictureBox2.Visible = true;
                pictureBox2.Load(Program.gPath + Image[0][0]);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        //설치열교정보 그림
        //상부
        private void Load_DoorType_image4()
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "출입문설치열교이미지", "열교유형이미지", "구분1 = '" + Install1 + "' AND 구분2 = '" + Install2 + "'");
            if (Image.Length > 0)
            {
                pictureBox3.Visible = true;
                pictureBox3.Load(Program.gPath + Image[0][0]);
                pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        //하부
        private void Load_DoorType_image5()
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "출입문설치열교이미지", "하부열교유형이미지", "구분1 = '" + Install1 + "' AND 구분2 = '" + Install2 + "'");
            if (Image.Length > 0)
            {
                pictureBox4.Visible = true;
                pictureBox4.Load(Program.gPath + Image[0][0]);
                pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }


        //Calc
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ


        public void Rule_UD()
        {
            if (UDoorMethod == "법규")
            {
                String[][] Date = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "법규시기,지역구분", "");
                if (Date.Length > 0)
                {
                    String[][] Uvalue;
                    if (Type == "기존 출입문")
                    {
                        Uvalue = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '문' And 시기 = '" + Date[0][0] + "' AND  지역 ='" + Date[0][1] + "'  AND  직접간접 =  '" + DiIndi + "'");
                    }
                    else
                    {
                        Uvalue = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '문' And 시기 = '2018.09' AND  지역 ='" + Date[0][1] + "'  AND 직접간접 =  '" + DiIndi + "'");
                    }
                    if (Uvalue.Length > 0)
                    {
                        DoorUD = Program.UTIL.ToDoubleOrZero(Uvalue[0][0]);
                        UD1_textBox.Text = String.Format("{0:F3}", DoorUD) + " W/m" + Program.UTIL.Subscript(2, true) + "·K";
                    }
                }
            }
        }


        //유리 포함 문 열관류율 
        public void UD_Glass()
        {
            if (UDoorMethod == "법규")
            {
                if (UD1_textBox.Text != null && UD1_textBox.Text.ToString() != "")
                {
                    DoorUDGlass = Program.UTIL.ToDoubleOrZero(UD1_textBox.Text);
                    UD2_textBox.Text = String.Format("{0:F3}", DoorUDGlass) + " W/m" + Program.UTIL.Subscript(2, true) + "·K";
                }
            }
            else
            {
                if (UD1_textBox.Text != null && DoorArea_textBox.Text != null && GlassArea_textBox.Text.ToString() != "" && GlassU_textBox.Text.ToString() != "")
                {
                    //유리포함 문 열관류율
                    //(유리면적x유리열관류율 + 문면적x문열관율) / 문면적
                    double doorarea=0, glassarea=0, totalarea=0, dooru=0, glassu=0;
                    totalarea = DoorArea / 1000000;
                    glassarea = GlassArea / 1000000;
                    doorarea = totalarea - glassarea;
                    dooru = DoorUD;
                    glassu = Ug;

                    DoorUDGlass = (glassarea * glassu + doorarea * dooru) / totalarea;
                    UD2_textBox.Text = String.Format("{0:F3}", DoorUDGlass) + " W/m" + Program.UTIL.Subscript(2, true) + "·K";

                }
                else;
            }

        }


        //문 유효 열관류율
        public void UDinstall_Glass()
        {
            if (dUinst_textBox.Text != null)
            {
                if (GlassArea_textBox.Text.ToString() == "")
                { DoorUDinsGlass = DoorUD + Program.UTIL.ToDoubleOrZero(dUinst_textBox.Text); }
                else
                {
                    DoorUDinsGlass = DoorUDGlass + Program.UTIL.ToDoubleOrZero(dUinst_textBox.Text);
                }

                UD_textBox.Text = String.Format("{0:F3}", DoorUDinsGlass) + " W/m" + Program.UTIL.Subscript(2, true) + "·K";
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
                GlassName_textBox.Visible = true;
                GlassDB_button.Visible = true;
                label85.Visible = true;
                GlassU_textBox.Visible = true;
                label87.Visible = true;
                UD2_textBox.Visible = true;
                GlassArea2_textBox.Visible = true;
                label35.Visible = true;

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
                GlassName_textBox.Visible = false;
                GlassDB_button.Visible = false;
                label85.Visible = false;
                GlassU_textBox.Visible = false;
                label87.Visible = false;
                UD2_textBox.Visible = false;
                GlassArea2_textBox.Visible = false;
                label35.Visible = false;
            }
        }


        private void Install_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Install_comboBox.SelectedItem != null)
            {
                InstallType = Install_comboBox.SelectedItem.ToString();

                //설치 유형 다시 선택했을 경우 

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
        }


        public bool ValidateAndSave(bool isManualSave = false)
        {
            try
            {
                // 명칭 없으면 아직 만들다 만 구조체 → 저장할 것 없음. 화면 전환은 막지 않는다.
                if (Name_textBox.Text == "")
                {
                    if (isManualSave)
                    {
                        MessageBox.Show("명칭을 입력하세요.");
                    }
                    return true;
                }

                // 빠진 항목을 모은다.
                List<string> missing = new List<string>();
                if (DoorUD <= 0)
                {
                    missing.Add("문제품정보");
                }
                if (Install2 == null)
                {
                    missing.Add("설치열교");
                }
                if (UDoorMethod == "계산" && glass_checkBox.Checked && GlassName == null)
                {
                    missing.Add("유리종류");
                }

                // 안내(막지 않음) + 미입력 목록을 '+'로 이어 DB에 저장한다.
                if (missing.Count > 0)
                {
                    MessageBox.Show(string.Join(", ", missing) + " 항목이 비어 있습니다.");
                }
                Save(string.Join("+", missing), isManualSave);
                return true;
            }
            catch (Exception ex)
            {
                // 디버깅 중단점 방지를 위해 예외를 무시하거나 로그만 남김
                System.Diagnostics.Debug.WriteLine($"ValidateAndSave 오류: {ex.Message}");
                return true;
            }
        }

       
        private void Save(string missingItems, bool isManualSave = false)
        {
            #region 법규
            String[][] Date = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "법규시기,지역구분", "");
            double 법규U = 0;
            if (Date.Length > 0)
            {
                String[][] Uvalue = Program.DB.getValue(DB.type.BaseDB_HCneed, "법규열관류율", "열관류율,기준,시기,지역", "구조체 = '문' And 시기 = '2018.09' AND  지역 ='" + Date[0][1] + "'  AND 직접간접 =  '" + DiIndi + "'");

                if (Uvalue.Length > 0)
                {
                    법규U = Program.UTIL.ToDoubleOrZero(Uvalue[0][0]);
                }
            }
            #endregion

          
                string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
                Program.DB.setValue(DB.type.ProjDB, "ConstructionDoor", "번호,프로젝트유형,명칭,Type,기존출입문,UD적용방법,직접간접,문짝제품,출입문재질,문틀내부,문짝내부유형,문짝색,흡수율,문짝단열재종류," +
                          "문짝두께,문열관류율,문틀상부측면열관류율,문틀하부열관류율,문면적,문높이,문길이,유리적용유무," +
                          "설치유형,설치유형2,상부선형열관류율,측면부선형열관류율,하부선형열관류율,상부설치길이,측면설치길이,하부설치길이,열교가산치," +
                          "문유효열관류율,Door유형,제품명,제조사," +
                          "법규열관류율,미입력항목",
                        "'" + DoorNum_textBox.Text + "','" + 프로젝트유형[0][0] + "','" + DoorName + "','" + Type + "','" + OldDoor + "','" + UDoorMethod + "','" + DiIndi + "','" + DoorDB + "','" + Material + "','" + FrameIn + "','" + DoorIn + "','" + DoorColor + "','" + αd.ToString() + "','" + DoorInsul + "','" +
                        DoorThk.ToString() + "','" + DoorUD.ToString() + "','" + DoorOver.ToString() + "','" + DoorBottom.ToString() + "','" + DoorArea_textBox.Text.Split(' ')[0] + "','" + DoorH.ToString() + "','" + DoorL.ToString() + "','" + glass_check.ToString() + "','" +
                        InstallType + "','" + Install2 + "','" + Psi_InstallTop.ToString() + "','" + Psi_InstallSide.ToString() + "','" + Psi_InstallBottom.ToString() + "','" + d_InstallTop_textBox.Text.Split(' ')[0] + "','" + d_InstallSide_textBox.Text.Split(' ')[0] + "','" + d_InstallBottom_textBox.Text.Split(' ')[0] + "','" + dUinst_textBox.Text.Split(' ')[0] + "','" +
                        DoorUDinsGlass.ToString() + "','" + DBType + "','" + DBName + "','" + DBName2 + "','" +
                        법규U.ToString()
                        + "','" + missingItems + "'", "번호");

                //유리 있을 경우 
                if (glass_checkBox.Checked)
                {
                    Program.DB.setValue(DB.type.ProjDB, "ConstructionDoor", "번호," +
                              "유리가로,유리세로,유리종류,유리면적,유리열관류율,유리반영문열관류율",
                            "'" + DoorNum_textBox.Text + "','" +
                            GlassL.ToString() + "','" + GlassH.ToString() + "','" + GlassName + "','" +
                           GlassArea_textBox.Text.Split(' ')[0] + "','" + Ug.ToString() + "','" + DoorUDGlass.ToString()
                            + "'", "번호");
                }
                else { }
                
            
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
            UD1_textBox.Text = null;

            DoorL_textBox.Text = null;
            DoorH_textBox.Text = null;
            DoorArea_textBox.Text = null;

            glass_checkBox.Checked = false;
            GlassL_textBox.Text = null;
            GlassH_textBox.Text = null;
            GlassArea_textBox.Text = null;
            GlassName_textBox.Text = null;
            GlassU_textBox.Text = null;
            UD2_textBox.Text = null;

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
            for (int i = 0; i < 14; i++)
            {
                Select[i] = null;
            }


            DoorName = null; Type = null; OldDoor = null; DoorColor = null; UDoorMethod = null; DiIndi = null; InstallType = null;
            installlocation = null; check_InstallType = null; Install2 = null; Install1 = null; InstallType = null; GlassName = null;
            Psi_InstallTop = 0; Psi_InstallSide = 0; Psi_InstallBottom = 0;
            Ug = 0;
            glass_check = false;
        }


        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();

            DoorNum_textBox.Text = ID;
            DoorNum = ID;

            String[][] Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionDoor", "번호,명칭,Type,기존출입문,UD적용방법,직접간접,문짝제품,출입문재질,문틀내부,문짝내부유형,문짝색,흡수율,문짝단열재종류," +
                  "문짝두께,문열관류율,문틀상부측면열관류율,문틀하부열관류율,문면적,문높이,문길이,유리적용유무," +
                  "설치유형,설치유형2,상부선형열관류율,측면부선형열관류율,하부선형열관류율,상부설치길이,측면설치길이,하부설치길이,열교가산치," +
                  "문유효열관류율"
                  , "번호 = '" + ID + "'");
            if (Load.Length > 0)
            {
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
                OldDoor_comboBox.SelectedIndex = OldDoor_comboBox.FindStringExact(OldDoor);
                UDoorMethod = Load[0][4];
                Udoor_comboBox.SelectedItem = UDoorMethod;
                DiIndi = Load[0][5];
                DiIndi_comboBox.SelectedItem = DiIndi;
                DoorColor = Load[0][10];
                DoorColor_comboBox.SelectedItem = DoorColor;
                αd = Program.UTIL.ToDoubleOrZero(Load[0][11]);
                abs_textBox.Text = αd.ToString();
                DoorDB = Load[0][6];
                DoorDB_textBox.Text = DoorDB;
                DoorUD = Program.UTIL.ToDoubleOrZero(Load[0][14]);
                UD1_textBox.Text = DoorUD.ToString() + " W/m" + Program.UTIL.Subscript(2, true) + "·K";
                DoorL = Program.UTIL.ToDoubleOrZero(Load[0][19]);
                DoorL_textBox.Text = DoorL.ToString() + " mm";
                DoorL2_textBox.Text = DoorL.ToString() + " mm";
                DoorH = Program.UTIL.ToDoubleOrZero(Load[0][18]);
                DoorH_textBox.Text = DoorH.ToString() + " mm";
                DoorH2_textBox.Text = DoorH.ToString() + " mm";
                DoorArea_textBox.Text = Load[0][17] == "" ? "" : Load[0][17] + " m" + Program.UTIL.Subscript(2, true);
                DoorArea2_textBox.Text = Load[0][17] == "" ? "" : Load[0][17] + " m" + Program.UTIL.Subscript(2, true);

                glass_check = Convert.ToBoolean(Load[0][20]);
                glass_checkBox.Checked = Convert.ToBoolean(Load[0][20]);

                InstallType = Load[0][21];
                Install1 = Load[0][21];
                Install_comboBox.SelectedItem = InstallType;
                Install2 = Load[0][22];
                Install_textBox.Text = Install2;

                DoorUDinsGlass = Program.UTIL.ToDoubleOrZero(Load[0][30]);
                UD_textBox.Text = String.Format("{0:F3}", DoorUDinsGlass) + " W/m" + Program.UTIL.Subscript(2, true) + "·K";

                //문짝문틀정보
                Material = Load[0][7];
                Material_textBox.Text = Material;
                FrameIn = Load[0][8];
                FrameIn_textBox.Text = FrameIn;
                DoorIn = Load[0][9];
                DoorIn_textBox.Text = DoorIn;

                DoorOver = Program.UTIL.ToDoubleOrZero(Load[0][15]);
                over_textBox.Text = DoorOver.ToString() + " W/m" + Program.UTIL.Subscript(2, true) + "·K";
                DoorBottom = Program.UTIL.ToDoubleOrZero(Load[0][16]);
                bottom_textBox.Text = DoorBottom.ToString() + " W/m" + Program.UTIL.Subscript(2, true) + "·K";

                OverL_textBox.Text = ((DoorL + DoorH * 2) / 1000).ToString() + " m";
                UnderL_textBox.Text = (DoorL / 1000).ToString() + " m";

                //설치열교정보
                Psi_InstallTop = Program.UTIL.ToDoubleOrZero(Load[0][23]);
                Psi_InstallTop_textBox.Text = Psi_InstallTop.ToString() + " W/m·K";
                Psi_InstallSide = Program.UTIL.ToDoubleOrZero(Load[0][24]);
                Psi_InstallSide_textBox.Text = Psi_InstallSide.ToString() + " W/m·K";
                Psi_InstallBottom = Program.UTIL.ToDoubleOrZero(Load[0][25]);
                Psi_InstallBottom_textBox.Text = Psi_InstallBottom.ToString() + " W/m·K";

                d_InstallTop_textBox.Text = Load[0][26] == "" ? "" : Load[0][26] + " m";
                d_InstallSide_textBox.Text = Load[0][27] == "" ? "" : Load[0][27] + " m";
                d_InstallBottom_textBox.Text = Load[0][28] == "" ? "" : Load[0][28] + " m";

                DoorUDinsGlass = Program.UTIL.ToDoubleOrZero(Load[0][29]);
                dUinst_textBox.Text = String.Format("{0:F3}", DoorUDinsGlass) + " W/m" + Program.UTIL.Subscript(2, true) + "·K";
                Load_DoorType_image2();
                Load_DoorType_image3();
                Load_DoorType_image4();
                Load_DoorType_image5();

                Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionDoor", "Door유형,제품명,제조사,문틀내부,문틀상부측면열관류율,문틀하부열관류율,출입문재질,문짝내부유형,문짝단열재종류,문짝두께,문길이,문높이,문열관류율", "번호 = '" + ID + "'");
                if (Load.Length > 0)
                {
                    Select[0] = ID;
                    Select[1] = Load[0][0];
                    Select[2] = Load[0][1];
                    Select[3] = Load[0][2];
                    Select[4] = Load[0][3];
                    Select[5] = Load[0][4];
                    Select[6] = Load[0][5];
                    Select[7] = Load[0][6];
                    Select[8] = Load[0][7];
                    Select[9] = Load[0][8];
                    Select[10] = Load[0][9];
                    Select[11] = Load[0][10];
                    Select[12] = Load[0][11];
                    Select[13] = Load[0][12];
                }


            }
            if (glass_check == true)
            {
                Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionDoor", "유리가로,유리세로,유리종류,유리면적,유리열관류율,유리반영문열관류율"
              , "번호 = '" + ID + "'");
                if (Load.Length > 0)
                {
                    GlassL = Program.UTIL.ToDoubleOrZero(Load[0][0]);
                    GlassL_textBox.Text = GlassL.ToString();
                    GlassH = Program.UTIL.ToDoubleOrZero(Load[0][1]);
                    GlassH_textBox.Text = GlassH.ToString();

                    GlassName = Load[0][2];
                    GlassName_textBox.Text = GlassName;
                    GlassArea_textBox.Text = Load[0][3] == "" ? "" : Load[0][3] + " m" + Program.UTIL.Subscript(2, true).ToString();
                    GlassArea2_textBox.Text = Load[0][3] == "" ? "" : Load[0][3] + " m" + Program.UTIL.Subscript(2, true).ToString();

                    Ug = Program.UTIL.ToDoubleOrZero(Load[0][4]);
                    GlassU_textBox.Text = Ug.ToString() + " W/m" + Program.UTIL.Subscript(2, true) + "·K";
                    DoorUDGlass = Program.UTIL.ToDoubleOrZero(Load[0][5]);
                    UD2_textBox.Text = String.Format("{0:F3}", DoorUDGlass) + " W/m" + Program.UTIL.Subscript(2, true) + "·K";
                }
            }
        }

        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            DoorNum_textBox.Text = ID;
            DoorNum = ID;
        }

        private void info_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\7.Door";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }
    }
}
