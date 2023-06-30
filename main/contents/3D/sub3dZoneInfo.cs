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
    public partial class sub3dZoneInfo : Form
    {
        public sub3dZoneInfo()
        {
            InitializeComponent();

            dataGridView1.Columns[7].HeaderText = "면적" + Environment.NewLine + "[m²]";
            dataGridView1.Columns[8].HeaderText = "방위" + Environment.NewLine + " - ";
            dataGridView1.Columns[9].HeaderText = "기울기" + Environment.NewLine + "[°]";
        }

        private void onVisibleChanged(object sender, EventArgs e)
        {
        }
    }
}
