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
        String ProjectType;
        public ProjectList()
        {
            InitializeComponent();
            string[][] Value = Program.DB.getValue(type.ProjDB, "BuildingGeneral", "프로젝트유형", "");
            ProjectType = Value[0][0];
            ProjectType_textBox.Text = ProjectType;
            if (ProjectType == null) { }
            else if (ProjectType == "기존")
            {
                Icon_pictureBox.Load(Program.gPath + "images/1sticon/0.Intro1.png");
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else if (ProjectType == "리모델링")
            {
                Icon_pictureBox.Load(Program.gPath + "images/1sticon/0.Intro2.png");
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else if (ProjectType == "리트로핏")
            {
                Icon_pictureBox.Load(Program.gPath + "images/1sticon/0.Intro3.png");
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                Icon_pictureBox.Load(Program.gPath + "images/1sticon/0.Intro2.png");
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

        }


        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);

        }


        private void reset()
        {
            ProjectType = null;
        }
        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();
            try
            {
                String[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형", "");

                ProjectType = Value[0][1];


            }
            catch { }

        }

        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
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
