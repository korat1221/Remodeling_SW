using main.contents;
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
            Program.DB.openDB(Program.ProjName + ".sqlite");
            MainContents f1 = new MainContents();

            f1.Location.Offset(0, 0);
            f1.Size = new Size(splitContainer1.Panel1.Width, splitContainer1.Panel1.Height);
            f1.TopLevel = false;
            splitContainer1.Panel1.Controls.Add(f1);

            f1.DoResizeMain(new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height));

            f1.Show();

        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            Program.DB.closeDB();
            main.Program.killServer();
        }

        private void OnResize(object sender, EventArgs e)
        {
            MainContents f1 = (MainContents)splitContainer1.Panel1.Controls[0];

            f1.Location.Offset(0, 0);
            f1.Size = new Size(splitContainer1.Panel1.Width, splitContainer1.Panel1.Height);

            f1.DoResizeMain(new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height));
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Program.getMenuForm().DoLoadForm(40, OnLoadProc1);
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Program.getMenuForm().DoLoadForm(41, OnLoadProc2);
        }
        public static bool OnLoadProc1(Form form)
        {
            Intro f = (Intro)form;

            f.LoadData("");

            return true;
        }
        public static bool OnLoadProc2(Form form)
        {
            ProjectList f = (ProjectList)form;

            f.LoadData("");

            return true;
        }

        
    }
}
