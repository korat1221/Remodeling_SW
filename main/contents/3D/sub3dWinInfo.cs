using main.subcontents.ConstructionRoof;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.contents
{
    public partial class sub3dWINInfo : Form
    {
        public sub3dWINInfo()
        {
            InitializeComponent();
        }


        private void onVisibleChanged(object sender, EventArgs e)
        {
            double R1, R2,L1,L2,S1,S2,T1,T2;


            string[][] ValueA = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");   //지역, 프로젝트 조건?

            String ID = main.MainContents.selID.Replace("board-", "");

            ID = ID.Replace("_win1", "");
            ID = ID.Replace("_win2", "");
            ID = ID.Replace("_win3", "");
            ID = ID.Replace("_win4", "");
            ID = ID.Replace("_win5", "");

            string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,우측면돌출각도,좌측면돌출각도,상부돌출각도,주변요소음영각도,우측면돌출길이,좌측면돌출길이,상부돌출길이,주변요소음영길이,번호,방위,기울기,구조체번호", "아이디 = '" + ID + "'");

            //if (rec.Length > 0)
            //{
            //    string[][] rec2 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_3D", "주광너비,주광깊이,상인방높이", "존번호 = '" + rec[0][0] + "'");

            //    textBox23.Text = (rec[0][1] == "0" ? "0" : Double.Parse(rec[0][1]).ToString("#.##"));
            //    textBox2.Text = (rec[0][2] == "0" ? "0" : Double.Parse(rec[0][2]).ToString("#.##"));
            //    textBox1.Text = (rec[0][3] == "0" ? "0" : Double.Parse(rec[0][3]).ToString("#.##"));
            //    textBox3.Text = (rec[0][4] == "0" ? "0" : Double.Parse(rec[0][4]).ToString("#.##"));
            //    textBox9.Text = (rec[0][5] == "0" ? "0" : Double.Parse(rec[0][5]).ToString("#.##"));
            //    textBox7.Text = (rec[0][6] == "0" ? "0" : Double.Parse(rec[0][6]).ToString("#.##"));
            //    textBox8.Text = (rec[0][7] == "0" ? "0" : Double.Parse(rec[0][7]).ToString("#.##"));
            //    textBox6.Text = (rec[0][8] == "0" ? "0" : Double.Parse(rec[0][8]).ToString("#.##"));
            //    textBox10.Text = (rec2[0][0] == "0" ? "0" : Double.Parse(rec2[0][0]).ToString("#.##"));
            //    textBox5.Text = (rec2[0][1] == "0" ? "0" : Double.Parse(rec2[0][1]).ToString("#.##"));
            //    textBox11.Text = (rec2[0][2] == "0" ? "0" : Double.Parse(rec2[0][2]).ToString("#.##"));
            //    textBox4.Text = (rec[0][9]);
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

            //창호정보 불러오기 
            String[][] SubLoad = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "번호,명칭,상위창호번호,창호면적,창호너비,창호높이,창호유효열관류율"
                   , "번호 = '" + rec[0][12] + "'");

            String[][] MainLoad = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "번호,창호명칭,Type,기존창호,Uw적용방법,직접간접,프레임유형,이중단창,프레임재료,프레임종류,유리종류,간봉종류,설치유형,설치종류,LE_CL_V," +
                  "유리열관류율,태양열취득률,빛투과율,고정유리선형열관류율,개폐유리선형열관류율," +
                  "상부설치열관류율,측면설치열관류율,하부설치열관류율," +
                  "창호열관류율," +
                  "개폐부프레임열관류율,고정부프레임열관류율,중간바프레임열관류율,개폐부프레임두께,고정부프레임두께,중간바프레임두께"
                    , "번호 = '" + SubLoad[0][2] + "'");




        }

    }
}
