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
    public partial class sub3dSLInfo : Form
    {
        public sub3dSLInfo()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
        }
        private void onVisibleChanged(object sender, EventArgs e)
        {
            string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "벽체길이,구조체,번호", "아이디 = '" + main.MainContents.selectInfo[2] + "'");
        }
    }
}
