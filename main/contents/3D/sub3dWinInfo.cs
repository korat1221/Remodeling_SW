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
            String ID = main.MainContents.selID.Replace("board-", "");

            ID = ID.Replace("_win1", "");
            ID = ID.Replace("_win2", "");
            ID = ID.Replace("_win3", "");
            ID = ID.Replace("_win4", "");
            ID = ID.Replace("_win5", "");

            string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,우측면돌출각도,좌측면돌출각도,상부돌출각도,주변요소음영각도,우측면돌출길이,좌측면돌출길이,상부돌출길이,주변요소음영길이,번호", "아이디 = '" + ID + "'");

            if (rec.Length > 0)
            {
                string[][] rec2 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_3D", "주광너비,주광깊이,상인방높이", "존번호 = '" + rec[0][0] + "'");

                textBox23.Text = (rec[0][1] == "0" ? "0" : Double.Parse(rec[0][1]).ToString("#.##"));
                textBox2.Text = (rec[0][2] == "0" ? "0" : Double.Parse(rec[0][2]).ToString("#.##"));
                textBox1.Text = (rec[0][3] == "0" ? "0" : Double.Parse(rec[0][3]).ToString("#.##"));
                textBox3.Text = (rec[0][4] == "0" ? "0" : Double.Parse(rec[0][4]).ToString("#.##"));
                textBox9.Text = (rec[0][5] == "0" ? "0" : Double.Parse(rec[0][5]).ToString("#.##"));
                textBox7.Text = (rec[0][6] == "0" ? "0" : Double.Parse(rec[0][6]).ToString("#.##"));
                textBox8.Text = (rec[0][7] == "0" ? "0" : Double.Parse(rec[0][7]).ToString("#.##"));
                textBox6.Text = (rec[0][8] == "0" ? "0" : Double.Parse(rec[0][8]).ToString("#.##"));
                textBox10.Text = (rec2[0][0] == "0" ? "0" : Double.Parse(rec2[0][0]).ToString("#.##"));
                textBox5.Text = (rec2[0][1] == "0" ? "0" : Double.Parse(rec2[0][1]).ToString("#.##"));
                textBox11.Text = (rec2[0][2] == "0" ? "0" : Double.Parse(rec2[0][2]).ToString("#.##"));
                textBox4.Text = (rec[0][9]);
            }

        }
    }
}
