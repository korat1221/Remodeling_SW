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
            if (Program.DB.openPListDB())
            {
                string[][] res = Program.DB.querySQL(DB.type.ProjListDB, "SELECT pnum FROM projects WHERE current= 1");

                if (res.Length > 0)
                {
                    ProjectList.CurProjID = res[0][0];
                }
            }

            Program.DB.openDB("projects\\" + ProjectList.CurProjID + ".sqlite");
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
            Program.DB.closePListDB();
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


        public static bool OnLoadProc1(Form form)
        {
            Intro f = (Intro)form;

            f.LoadData("");

            return true;
        }
        public static bool OnLoadProc2(Form form)
        {
            OpenProject f = (OpenProject)form;

            f.LoadData("");

            return true;
        }
       

        private void EnergyNeed_Sim_Click(object sender, EventArgs e)
        {
            CALC.run(new string[] { "존계산" });
            MessageBox.Show("계산되었습니다.");
        }

        private void FinalEnergy_Sim_Click(object sender, EventArgs e)
        {
            Program.DB.UseCaches(true);
            CALC.run(new string[]{ "모두계산" });
            Program.DB.UseCaches(false);
            MessageBox.Show("계산되었습니다.");
        }

        private void ProjectOpen_Click(object sender, EventArgs e)
        {
            Program.getMenuForm().DoLoadForm(42, OnLoadProc2);
        }
    }
}
