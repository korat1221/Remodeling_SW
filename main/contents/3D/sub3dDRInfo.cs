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
    public partial class sub3dDRInfo : Form
    {
        public sub3dDRInfo()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
        }
        private void onVisibleChanged(object sender, EventArgs e)
        {
            String[][] RES = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호", "아이디 = '" + main.MainContents.selectInfo[2] + "'");
            string 번호 = null;
            if (RES.Length > 0)
            {
                번호 = RES[0][0];
            }
            string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,면적,창호너비,창호높이,구조체번호", "번호 = '" + 번호 + "'");

            if (rec.Length > 0)
            {
                Name_textBox.Text = rec[0][0];
                Area_textBox.Text = String.Format("{0:F2}", Program.UTIL.ToDoubleOrZero(rec[0][1])) + " m" + Program.UTIL.Subscript(2, true);
                Width_textBox.Text = (rec[0][2] == "" ? "0" : String.Format("{0:F2}", Program.UTIL.ToDoubleOrZero(rec[0][2]))) + " m";
                height_textBox.Text = (rec[0][3] == "" ? "0" : String.Format("{0:F2}", Program.UTIL.ToDoubleOrZero(rec[0][3]))) + " m";


                //문짝제품, 문틀내부, 열교가산치, 문유효열관류율
                string[][] DRLoad = Program.DB.getValue(DB.type.ProjDB, "ConstructionDoor", "문짝제품, 문틀내부, 열교가산치, 문유효열관류율", "번호 = '" + rec[0][4] + "'");
                if( DRLoad.Length > 0 )
                {
                    Panel1.Visible = true;
                    Door_textBox.Text = DRLoad[0][0];
                    DRFrame_textBox.Text = DRLoad[0][1];
                    dUinst_textBox.Text = String.Format("{0:F3}", Program.UTIL.ToDoubleOrZero(DRLoad[0][2])) + " W/m" + Program.UTIL.Subscript(2, true) + "·K";
                    UD_textBox.Text = String.Format("{0:F3}", Program.UTIL.ToDoubleOrZero(DRLoad[0][3])) + " W/m" + Program.UTIL.Subscript(2, true) + "·K";
                    string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "출입문유형이미지", "이미지", "유형 = '" + "치수" + "'");
                    pictureBox1.Visible = true;
                    pictureBox1.Load(Program.gPath + Image[0][0]);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    Panel1.Visible= false;
                }
            }
        }
    }
}
