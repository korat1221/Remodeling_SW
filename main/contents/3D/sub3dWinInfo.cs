using main.subcontents.ConstructionRoof;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.contents
{
    public partial class sub3dWINInfo : Form
    {
        string sid = "";
        public sub3dWINInfo()
        {
            InitializeComponent();
        }


        private void onVisibleChanged(object sender, EventArgs e)
        {
            if (main.MainContents.selID != sid)
            {
                sid = main.MainContents.selID;

                double R1, R2, L1, L2, S1, S2, T1, T2, uw, install;
                string Type, InstallType, FrameMaterial, SingleDoubleType, InstallName;
                double Area;

                string[][] ValueA = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");   //지역, 프로젝트 조건?

                String ID = main.MainContents.selID.Replace("board-", "");

                ID = ID.Replace("_win1", "");
                ID = ID.Replace("_win2", "");
                ID = ID.Replace("_win3", "");
                ID = ID.Replace("_win4", "");
                ID = ID.Replace("_win5", "");

                string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,우측면돌출각도,좌측면돌출각도,상부돌출각도,주변요소음영각도,우측면돌출길이,좌측면돌출길이,상부돌출길이,주변요소음영길이,번호,방위,기울기,구조체번호,면적,창호너비,창호높이", "아이디 = '" + ID + "'");

                if (rec.Length > 0)
                {


                    //음영정보 이미지로드
                    string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "음영이미지", "이미지", "분류 = '이미지1'");
                    pictureBox1.Load(Program.gPath + Image[0][0]);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                    string[][] Image1 = Program.DB.getValue(DB.type.BaseDB_HCneed, "음영이미지", "이미지", "분류 = '이미지2'");
                    pictureBox2.Load(Program.gPath + Image1[0][0]);
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

                    //음영정보
                    R1 = Convert.ToDouble(rec[0][5]);
                    R1_textBox.Text = R1.ToString("0.00") + "m";
                    R2 = Convert.ToDouble(rec[0][1]);
                    R2_textBox.Text = R2.ToString("0.00") + "°";
                    L1 = Convert.ToDouble(rec[0][6]);
                    L1_textBox.Text = L1.ToString("0.00") + "m";
                    L2 = Convert.ToDouble(rec[0][2]);
                    L2_textBox.Text = L2.ToString("0.00") + "°";
                    S1 = Convert.ToDouble(rec[0][8]);
                    S1_textBox.Text = S1.ToString("0.00") + "m";
                    S2 = Convert.ToDouble(rec[0][4]);
                    S2_textBox.Text = S2.ToString("0.00") + "°";
                    T1 = Convert.ToDouble(rec[0][7]);
                    T1_textBox.Text = T1.ToString("0.00") + "m";
                    T2 = Convert.ToDouble(rec[0][3]);
                    T2_textBox.Text = T2.ToString("0.00") + "°";


                    //save한 음영계수값 불러오기 (최종만)
                    string[][] value = Program.DB.getValue(DB.type.ProjDB, "Shade_3D", "음영계수", "번호 = '" + ID + "'");

                    //창호정보 불러오기 // *************************창호 너비 높이 면적은 존 인벨롭에서 들어오는 값으로 해야함 (임시방편)
                    String[][] SubLoad = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "번호,명칭,상위창호번호,창호면적,창호너비,창호높이,창호유효열관류율,설치열교가산치", "번호 = '" + rec[0][12] + "'");

                    //구조체 지정 전까지 면적,너비,높이 빼고 전부 다 안보이게
                    if (rec[0][12] == "")
                    {
                        label4.Visible = false;
                        install_textBox.Visible = false;
                        label2.Visible = false;
                        glass_textBox.Visible = false;
                        label3.Visible = false;
                        frame_textBox.Visible = false;
                        label6.Visible = false;
                        Spacer_textBox.Visible = false;
                        label7.Visible = false;
                        shgc_textBox.Visible = false;
                        label9.Visible = false;
                        light_textBox.Visible = false;
                        label11.Visible = false;
                        uw_textBox.Visible = false;
                        label13.Visible = false;
                        inst_textBox.Visible = false;
                        WindowType_pictureBox.Visible = false;
                        WindowInstall_pictureBox.Visible = false;
                        label10.Visible = false;
                        label12.Visible = false;
                    }
                    else
                    {
                        label8.Visible = true;
                        height_textBox.Visible = true;
                        label16.Visible = true;
                        label4.Visible = true;
                        install_textBox.Visible = true;
                        label2.Visible = true;
                        glass_textBox.Visible = true;
                        frame_textBox.Visible = true;
                        Spacer_textBox.Visible = true;
                        label7.Visible = true;
                        shgc_textBox.Visible = true;
                        label9.Visible = true;
                        light_textBox.Visible = true;
                        label11.Visible = true;
                        uw_textBox.Visible = true;
                        label13.Visible = true;
                        inst_textBox.Visible = true;
                        WindowType_pictureBox.Visible = true;
                        WindowInstall_pictureBox.Visible = true;
                    }

                    //정보
                    Name_textBox.Text = rec[0][9];
                    Area = Convert.ToDouble(rec[0][13]);
                    Area_textBox.Text = String.Format("{0:F2}", Area);
                    Width_textBox.Text = String.Format("{0:F2}", Convert.ToDouble(rec[0][14]));
                    height_textBox.Text = String.Format("{0:F2}", Convert.ToDouble(rec[0][15]));

                    if (SubLoad.Length > 0)
                    {
                        String[][] MainLoad = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "번호,창호명칭,프레임재료,프레임종류,유리종류,간봉종류,설치유형,설치종류,태양열취득률,빛투과율,Type,설치유형,프레임재료,이중단창,설치종류", "번호 = '" + SubLoad[0][2] + "'");

                        // 텍스트정보 
                        Name_textBox1.Text = SubLoad[0][1];
                        if (MainLoad[0][6] == "내단열" || MainLoad[0][6] == "외단열")
                        { install_textBox.Text = "콘크리트조 " + MainLoad[0][6]; }
                        else { install_textBox.Text = MainLoad[0][6]; }
                        glass_textBox.Text = MainLoad[0][4];
                        frame_textBox.Text = MainLoad[0][2];
                        Spacer_textBox.Text = MainLoad[0][5];
                        shgc_textBox.Text = MainLoad[0][8];
                        light_textBox.Text = MainLoad[0][9];
                        uw = Convert.ToDouble(SubLoad[0][6]);
                        uw_textBox.Text = uw.ToString("0.000");
                        install = Convert.ToDouble(SubLoad[0][7]);
                        inst_textBox.Text = install.ToString("0.000");
                        Type = MainLoad[0][10];
                        InstallType = MainLoad[0][11];
                        FrameMaterial = MainLoad[0][12];
                        SingleDoubleType = MainLoad[0][13];
                        InstallName = MainLoad[0][14];

                        //그림로드
                        string[][] Image2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "창호구조유형이미지", "이미지", "구조유형 = '" + Type + "'");
                        WindowType_pictureBox.Visible = true;
                        WindowType_pictureBox.Load(Program.gPath + Image2[0][0]);
                        WindowType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                        string[][] Image3 = Program.DB.getValue(DB.type.BaseDB_HCneed, "창호설치열교이미지", "이미지열교유형", "구분1 = '" + InstallType + "' AND 구분2 = '" + FrameMaterial + "' AND 구분3 = '" + SingleDoubleType + "' AND 구분4 = '" + InstallName + "'");
                        WindowInstall_pictureBox.Visible = true;
                        WindowInstall_pictureBox.Load(Program.gPath + Image3[0][0]);
                        WindowInstall_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                    }
                }
            }
        }

    }
}
