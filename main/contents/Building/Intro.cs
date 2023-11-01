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
        String ProjectName, ProjectType;
        public Intro()
        {
            InitializeComponent();
            Logo_pictureBox.Load(Program.gPath + "images/1sticon/0.Logo.png");
            Logo_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            //프로젝트명
            if (ProjectName != null) { ProjectName_textBox.Text = ProjectName.ToString(); }
            else { }

            //프로젝트유형
            ProjectType_textBox.Visible = false;
            ProjectType_pictureBox.Visible = false;
        }

        private void ProjectName_textBox_TextChanged(object sender, EventArgs e)
        {
            if(ProjectName_textBox.Text != null)
            {
                ProjectName = ProjectName_textBox.Text.ToString();
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ProjectType = "기존";
            ProjectType_textBox.Visible = true;
            ProjectType_textBox.Text = ProjectType.ToString();
            ProjectType_pictureBox.Visible = true;
            ProjectType_pictureBox.Load(Program.gPath + "images/1sticon/0.Intro1.png");
            ProjectType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ProjectType = "리트로핏";
            ProjectType_textBox.Visible = true;
            ProjectType_textBox.Text = ProjectType.ToString();
            ProjectType_pictureBox.Visible = true;
            ProjectType_pictureBox.Load(Program.gPath + "images/1sticon/0.Intro2.png");
            ProjectType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;


        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            ProjectType = "리모델링";
            ProjectType_textBox.Visible = true;
            ProjectType_textBox.Text = ProjectType.ToString();
            ProjectType_pictureBox.Visible = true;
            ProjectType_pictureBox.Load(Program.gPath + "images/1sticon/0.Intro3.png");
            ProjectType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
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

        private void Import_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = ".sqlite files (*.sqlite)|*.sqlite";
            openFileDialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //try
                //{
                //   string[][] FileValue = getValue(Path.GetFileNameWithoutExtension(openFileDialog.FileName),"BuildingGeneral", "주소", "프로젝트유형='기존'");
                //    MessageBox.Show(Path.GetFileNameWithoutExtension(openFileDialog.FileName));
                //}
                //catch
                //{
                //    MessageBox.Show("파일의 형식이 올바르지않습니다. 데이터를 확인해주세요.");
                //}
            }
        }
        //public string[][] getValue(string filename, string table, string columns, string conditions = "")
        //{
        //    SQLiteCommand cmd = new SQLiteCommand();
        //    List<string[]> objects = new List<string[]>();             
        //    cmd.Connection = new SQLiteConnection(filename);
        //    if (conditions != "")
        //    {
        //        cmd.CommandText = "SELECT " + columns + " FROM " + table + " WHERE " + conditions;
        //    }
        //    else
        //    {
        //        cmd.CommandText = "SELECT " + columns + " FROM " + table;
        //    }

        //    using (SQLiteDataReader reader = cmd.ExecuteReader())
        //    {
        //        string json = string.Empty;

        //        while (reader.Read())
        //        {
        //            string[] rec = new string[reader.FieldCount];

        //            for (int i = 0; i < reader.FieldCount; i++)
        //            {
        //                rec[i] = reader[i].ToString();
        //            }
        //            objects.Add(rec);
        //        }
        //    }

        //    return objects.ToArray();
        //}
        private void Save_button_Click(object sender, EventArgs e)
        {
            if (ProjectName == null)
            {
                MessageBox.Show("프로젝트 명칭을 입력하세요.");
            }
            else if (ProjectType == null)
            {
                MessageBox.Show("프로젝트 타입을 선택하세요.");
            }
            else { Save(); }
        }

        private void Save()
        {
            Program.DB.setValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트명,프로젝트유형",
            "'" + ProjectName + "','" + ProjectType + "'", "프로젝트명");

            MessageBox.Show("저장되었습니다.");
        }

        private void reset()
        {
            ProjectName = null;
            ProjectName_textBox.Text = null;

            ProjectType = null;
            ProjectType_textBox.Text = null;


        }
        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();
            try
            {
                String[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트명,프로젝트유형", "");

                ProjectName = Value[0][0];
                ProjectName_textBox.Text = ProjectName.ToString();

                ProjectType = Value[0][1];
                ProjectType_textBox.Text = ProjectType.ToString();


            }
            catch { }

        }

        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
        }

    }
}
