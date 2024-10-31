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
        string sid = "";
        public sub3dSLInfo()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
        }
        private void onVisibleChanged(object sender, EventArgs e)
        {
            if (main.MainContents.selID != sid)
            {
                sid = main.MainContents.selID;

                String key = sid.IndexOf("F_Zone") > 0 ? "번호" : "아이디";
                String ID = main.MainContents.selID.Replace("board-", "");
                string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "벽체길이,구조체,번호", key + " = '" + ID + "'");
            }
        }
    }
}
