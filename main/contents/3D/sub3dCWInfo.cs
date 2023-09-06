using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace main.contents
{
    public partial class sub3dCWInfo : Form
    {
        public sub3dCWInfo()
        {
            InitializeComponent();
        }
        private void onVisibleChanged(object sender, EventArgs e)
        {
            double R1, R2, L1, L2, S1, S2, T1, T2, uw, install;
            string Type, InstallType, FrameMaterial, SingleDoubleType, InstallName, UCWtype;

            String ID = main.MainContents.selID.Replace("board-", "").Replace("_win2", "");
            //string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "벽체길이,구조체,번호", "아이디 = '" + ID + "'");
            string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,우측면돌출각도,좌측면돌출각도,상부돌출각도,주변요소음영각도,우측면돌출길이,좌측면돌출길이,상부돌출길이,주변요소음영길이,번호,방위,기울기,구조체번호", "아이디 = '" + ID + "'");

            //if (rec.Length > 0)
            //{
            //    textBox3.Text = rec[0][2];
            //    //textBox1.Text = Double.Parse(rec[0][0]).ToString("#.##");
            //    //textBox2.Text = rec[0][1];
            //}

            //if (ID.IndexOf("_INWALL_") > 0)
            //{
            //    label3.Show();
            //    label1.Hide();
            //}
            //else
            //{
            //    label1.Show();
            //    label3.Hide();
            //}

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
            string[][] value = Program.DB.getValue(DB.type.ProjDB, "Shade", "음영계수", "번호 = '" + ID + "'");

            //커튼월정보 불러오기  // *************************창호 너비 높이 면적은 존 인벨롭에서 들어오는 값으로 해야함 (임시방편)
            String[][] CWLoad = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "번호,명칭,커튼월면적,너비,높이,프레임종류,고정유리종류,개폐유리종류,간봉종류,설치유형,태양열취득률,빛투과율,설치열교가산치,커튼월창유효열관류율,Type,설치종류,Ucw적용방법", "번호 = '" + rec[0][12] + "'");

            //구조체 지정 전까지 면적,너비,높이 빼고 전부 다 안보이게
            if (rec[0][12] == "")
            {
                label4.Visible= false;
                install_textBox.Visible= false;
                label2.Visible= false;
                glass_textBox.Visible= false;
                label10.Visible= false;
                glass2_textBox.Visible= false;
                label3.Visible= false;
                frame_textBox.Visible= false;
                label6.Visible= false;
                Spacer_textBox.Visible= false;
                label7.Visible= false;
                shgc_textBox.Visible= false;
                label9.Visible= false;
                light_textBox.Visible= false;
                label11.Visible= false;
                uw_textBox.Visible= false;
                label13.Visible= false;
                inst_textBox.Visible= false;
                CWType_pictureBox.Visible= false;
                CWInstall_pictureBox.Visible= false;
            }
            else
            {  
                label4.Visible = true;
                install_textBox.Visible = true;
                label2.Visible = true;
                glass_textBox.Visible = true;
                label10.Visible = true;
                glass2_textBox.Visible = true;
                label3.Visible = true;
                frame_textBox.Visible = true;
                label6.Visible = true;
                Spacer_textBox.Visible = true;
                label7.Visible = true;
                shgc_textBox.Visible = true;
                label9.Visible = true;
                light_textBox.Visible = true;
                label11.Visible = true;
                uw_textBox.Visible = true;
                label13.Visible = true;
                inst_textBox.Visible = true; 
                CWType_pictureBox.Visible = true;
                CWInstall_pictureBox.Visible = true;
            }



            // 텍스트정보 
            Name_textBox.Text = rec[0][9];
            Name_textBox1.Text = CWLoad[0][1];
            Area_textBox.Text = CWLoad[0][2];
            Width_textBox.Text = CWLoad[0][3];
            height_textBox.Text = CWLoad[0][4];
            install_textBox.Text = CWLoad[0][9];
            glass_textBox.Text = CWLoad[0][6];
            glass2_textBox.Text = CWLoad[0][7];
            frame_textBox.Text = CWLoad[0][5];
            Spacer_textBox.Text = CWLoad[0][8];
            shgc_textBox.Text = CWLoad[0][10];
            light_textBox.Text = CWLoad[0][11];
            uw = Convert.ToDouble(CWLoad[0][13]);
            uw_textBox.Text = uw.ToString("0.000");
            install = Convert.ToDouble(CWLoad[0][12]);
            inst_textBox.Text = install.ToString("0.000");

            Type = CWLoad[0][14];
            InstallType = CWLoad[0][9];
            InstallName = CWLoad[0][15];

            UCWtype= CWLoad[0][16];

            //UCWtype에 따른 비활성화/활성화
            if (UCWtype == "계산")
            {
                label3.Visible = true;
                label6.Visible= true;
                frame_textBox.Visible= true;
                Spacer_textBox.Visible= true;
            }
            else
            {
                label3.Visible = false;
                label6.Visible = false;
                frame_textBox.Visible = false;
                Spacer_textBox.Visible = false;
            }


            //그림로드
            string[][] Image2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "커튼월구조유형이미지", "이미지", "구조유형 = '" + Type + "'");
            CWType_pictureBox.Visible = true;
            CWType_pictureBox.Load(Program.gPath + Image2[0][0]);
            CWType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            string[][] Image3 = Program.DB.getValue(DB.type.BaseDB_HCneed, "커튼월설치열교이미지", "이미지", "구분1 = '" + InstallType + "' AND 구분3 = '" + InstallName + "'");
            CWInstall_pictureBox.Visible = true;
            CWInstall_pictureBox.Load(Program.gPath + Image3[0][0]);
            CWInstall_pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

        }

    }
}
