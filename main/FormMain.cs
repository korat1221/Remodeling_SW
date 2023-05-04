using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            MainContents f1 = new MainContents();
            f1.TopLevel = false;
            splitContainer1.Panel1.Controls.Add(f1);
            f1.Show();

        }
    }
}
