using main.contents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main
{
    public partial class MainContents : Form
    {
        public enum FormID
        {
            ConstructionWall = 0,
            ConstructionRoof,
            ZoneUsage,
            FormDebug
        }

        Form[] forms = new Form[] { new ConstructionWall(), new ConstructionRoof(), new ZoneUsage(), new FormDebug() };

        public MainContents()
        {
            InitializeComponent();

            foreach (FormMain openForm in Application.OpenForms)
            {
                if (openForm.Name == "FormMain")
                {
                    int i = -1;
                    while(++i < forms.Length)
                    {
                        forms[i].TopLevel = false;
                        openForm.splitContainer2.Panel1.Controls.Add(forms[i]);
                    }
                    
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
                    int i = -1;
                    while (++i < forms.Length)
                    {
                        forms[i].Hide();
                    }
                    forms[(int)FormID.ConstructionWall].Show();
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
                    int i = -1;
                    while (++i < forms.Length)
                    {
                        forms[i].Hide();
                    }
                    forms[(int)FormID.ConstructionRoof].Show();
                    return;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (FormMain openForm in Application.OpenForms)
            {
                if (openForm.Name == "FormMain")
                {
                    int i = -1;
                    while (++i < forms.Length)
                    {
                        forms[i].Hide();
                    }
                    forms[(int)FormID.ZoneUsage].Show();
                    return;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            foreach (FormMain openForm in Application.OpenForms)
            {
                if (openForm.Name == "FormMain")
                {
                    int i = -1;
                    while (++i < forms.Length)
                    {
                        forms[i].Hide();
                    }
                    forms[(int)FormID.FormDebug].Show();
                    return;
                }
            }
        }
    }
}
