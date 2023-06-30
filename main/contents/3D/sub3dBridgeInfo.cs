using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static main.MainContents;

namespace main.contents
{
    public partial class sub3dBridgeInfo : Form
    {
        public sub3dBridgeInfo()
        {
            InitializeComponent();
        }
        private void onVisibleChanged(object sender, EventArgs e)
        {
            if (main.MainContents.selID.IndexOf("bridge-") >= 0)
            {
                this.panel1.Hide();
                this.panel2.Show();
            }
            else
            {
                this.panel1.Show();
                this.panel2.Hide();
            }
        }
    }
}
