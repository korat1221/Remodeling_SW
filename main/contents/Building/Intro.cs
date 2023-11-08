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

namespace main.contents
{
    public partial class Intro : Form
    {
        public Intro()
        {
            InitializeComponent();
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
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);

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
        }

        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
        }


    }
}
