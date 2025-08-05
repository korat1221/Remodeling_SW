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

using System.Data.SQLite;
using main.info;

namespace main.contents
{
    public partial class Intro : Form
    {
        public Intro()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            Logo_pictureBox.Load(Program.gPath + "images/1sticon/0.Logo.png");
            Logo_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;


        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ProjectList.ProjectType = "1";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ProjectList.ProjectType = "2";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            ProjectList.ProjectType = "3";
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            ProjectList.ProjectType = "4";
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);

        }
        private void Save_button_Click(object sender, EventArgs e)
        {
            Program.getMenuForm().DoLoadForm(41, OnLoadProc2);
        }
        public static bool OnLoadProc2(Form form)
        {
            ProjectList f = (ProjectList)form;

            f.LoadData("");

            return true;
        }
        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            string[][] Value = Program.DB.querySQL(DB.type.ProjListDB, "Select type from projects where current = '1'");
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;

            if (Value.Length > 0)
            {
                switch (Value[0][0])
                {
                    case "1":
                        radioButton1.Checked = true;
                        break;
                    case "2":
                        radioButton2.Checked = true;
                        break;
                    case "3":
                        radioButton3.Checked = true;
                        break;
                    case "4":
                        radioButton4.Checked = true;
                        break;

                }
            }
        }

        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
        }

        private void info_Click(object sender, EventArgs e)
        {            
            string basePath = Program.gPath + "Manual\\1.contents\\0.main\\01.Intro";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }

    }
}
