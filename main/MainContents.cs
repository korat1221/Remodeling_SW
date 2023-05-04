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
    public partial class MainContents : Form
    {
        ConstructionWall f2 = new ConstructionWall();
        ConstructionRoof f3 = new ConstructionRoof();
        Form1 f4 = new Form1();
        public MainContents()
        {
            InitializeComponent();

            foreach (FormMain openForm in Application.OpenForms)
            {
                if (openForm.Name == "FormMain")
                {
                    f2.TopLevel = false;
                    openForm.splitContainer2.Panel1.Controls.Add(f2);
                    f3.TopLevel = false;
                    openForm.splitContainer2.Panel1.Controls.Add(f3);
                    f4.TopLevel = false;
                    openForm.splitContainer2.Panel2.Controls.Add(f4);
                    return;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (FormMain openForm in Application.OpenForms)
            {
                if (openForm.Name == "FormMain")
                {
                    f3.Hide();
                    f2.Show();
                    return;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (FormMain openForm in Application.OpenForms)
            {
                if (openForm.Name == "FormMain")
                {
                    f2.Hide();
                    f3.Show();
                    return;
                }
            }
        }

        private void OnMenuLoad(object sender, EventArgs e)
        {
            f2.Show();
            f4.Show();
        }
    }
}
