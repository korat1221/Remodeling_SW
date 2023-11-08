using main.contentslist;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static main.DB;
using System.Data.Entity.Core.Metadata.Edm;
using System.Security.Cryptography;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Data.SQLite;
using main.subcontents.HeatingSystem;

namespace main.contents
{
    public partial class ProjectList : Form
    {
        public static String ProjectType = "1";

        Dictionary<string, string> types = new Dictionary<string, string>();

        public ProjectList()
        {
            InitializeComponent();

            types.Add("1", "기존건물");
            types.Add("2", "리트로핏");
            types.Add("3", "리모델링");
            types.Add("4", "신규건물");
        }


        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);

        }


        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            try
            {
                ProjectType_textBox.Text = types[ProjectType];
                if (ProjectType == null) { }
                else if (ProjectType == "1")
                {
                    Icon_pictureBox.Load(Program.gPath + "images/1sticon/0.Intro1.png");
                    Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else if (ProjectType == "2")
                {
                    Icon_pictureBox.Load(Program.gPath + "images/1sticon/0.Intro2.png");
                    Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else if (ProjectType == "3")
                {
                    Icon_pictureBox.Load(Program.gPath + "images/1sticon/0.Intro3.png");
                    Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    Icon_pictureBox.Load(Program.gPath + "images/1sticon/0.Intro2.png");
                    Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }

                dataGridView1.Rows.Clear();

                string[][] res = Program.DB.querySQL(DB.type.ProjListDB, "SELECT ID, pnum, title, type FROM projects");
                for (int n = 0; n < res.Length; n++)
                {
                    dataGridView1.Rows.Add();
                    int nRow = dataGridView1.Rows.Count - 1;

                    for (int k = 0; k < 4; k++)
                    {
                        dataGridView1.Rows[nRow].Cells[k + 1].Value = (k == 3) ? types[res[n][k]] : res[n][k];
                    }
                }

            }
            catch { }

        }

        private void Copy_button_Click(object sender, EventArgs e)
        {
            ProjectCopy projectcopy = new ProjectCopy();
            DialogResult result = projectcopy.ShowDialog();
            if (result == DialogResult.OK)
            {
            }
        }
    }
}
