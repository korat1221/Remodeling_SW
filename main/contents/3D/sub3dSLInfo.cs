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
    public partial class sub3dSLInfo : Form
    {
        public sub3dSLInfo()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
        }
        private void onVisibleChanged(object sender, EventArgs e)
        {
            string[][] value1 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적,번호,인접존", "아이디 = '" + main.MainContents.selectInfo[2] + "'");
            if (value1.Length > 0)
            {
                Name_textBox.Text = value1[0][1];
                double Area = Program.UTIL.ToDoubleOrZero(value1[0][0]);
                Area_textBox.Text = Area.ToString();
                Program.UTIL.textBox_doubleComa(Area_textBox, true, 2);
                Area_textBox.Text = Area_textBox.Text.ToString() + " m" + Program.UTIL.Subscript(2, true);
                NearZone_textBox.Text = value1[0][2];
            }
        }
    }
}
