using main.contents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
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
            if (Program.DB.openPListDB(Program.gPath))
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
            string[][] value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호,프로젝트유형,프로젝트명");
            if (value.Length > 0)
            {
                this.Text = "ZEROFIX        " + value[0][0] + "_" + value[0][2] + "_" + value[0][1];
            }
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            Program.DB.closeDB();
            Program.DB.closePListDB();
            main.Program.killServer();
        }

        private void OnResize(object sender, EventArgs e)
        {
            try
            {
                MainContents f1 = (MainContents)splitContainer1.Panel1.Controls[0];

                f1.Location.Offset(0, 0);
                f1.Size = new Size(splitContainer1.Panel1.Width, splitContainer1.Panel1.Height);

                f1.DoResizeMain(new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height));
            }
            catch (Exception ex)
            {
                //                MessageBox.Show(ex.ToString());
            }
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
            CALC.run(new string[] { "모두계산" });
            Program.DB.UseCaches(false);
            MessageBox.Show("계산되었습니다.");
        }

        private void ProjectOpen_Click(object sender, EventArgs e)
        {
            Program.getMenuForm().DoLoadForm(42, OnLoadProc2);
        }

        private void Element_Sim_Click(object sender, EventArgs e)
        {
            Program.DB.UseCaches(true);
            CALC.run(new string[] { "요소기술계산" });
            Program.DB.UseCaches(false);
            MessageBox.Show("요소기술별 계산이 완료되었습니다.");
        }
    }
}
