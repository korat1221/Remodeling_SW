using main.contents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace main.subcontents
{
    public partial class Heating_BoilerDB : Form
    {
        double Count_FrameDB;
        int SelectRow;
        public string[] Select_WindowFrame = new string[11];
        String UserNum, UserDBName, UserDB_Manufacture, UserDB_FrameShape, UserDB_FrameMaterial_orgin, UserDB_FrameMaterial, UserDBGlass, UserDBSpacer, SingleDoubleType, UserDB_LE_CL_V, UserDB_Image;
        Double UserDB_Uw, UserDB_FramedA, UserDB_FramedB, UserDB_FramedC, UserDB_Ug, UserDB_PsiOpen, UserDB_PsiFix;
        Double UserDB_Ag, UserDB_Af, UserDB_Lopen, UserDB_Lfix, UserDB_Uf;
        List<String> GlassList = new List<String>();
        List<String> SpacerList = new List<String>();

        public Heating_BoilerDB()
        {
            InitializeComponent();
            UserNum = Program.UTIL.CreateNum("User_Boiler", "번호", "UBS_0");
            UserNum_textBox.Text = UserNum;
            load_table();

            //프레임 형태 콤보박스 
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, UserDB_FrameShape_comboBox, "창호", "형태", "1");
            //프레임 재질 콤보박스 
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, UserDB_FrameMaterial_comboBox, "창호", "프레임재질", "1");
            //유리 콤보박스
            try
            {
                string[][] User_WinGlass = Program.DB.getValue(DB.type.ProjDB, "User_Glass", "번호,제품명", "");
                for (int n = 0; n < User_WinGlass.Length; n++)
                { GlassList.Add(User_WinGlass[n][1]); }
            }
            catch { }
            string[][] WinGlass = Program.DB.getValue(DB.type.BaseDB_HCneed, "유리", "번호,제품명", "");
            for (int n = 0; n < WinGlass.Length; n++)
            {
                GlassList.Add(WinGlass[n][1]);
            }
            string[] GlassArray = GlassList.ToArray();
            UserDBGlass_comboBox.Items.Clear();
            UserDBGlass_comboBox.Items.AddRange(GlassArray);


        }
        void load_table()
        {
            DataTable table_Boiler = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Boiler_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Boiler_dataGridView.Columns.Add(checkBoxColumn);
            table_Boiler.Columns.Add("번호", typeof(string));
            table_Boiler.Columns.Add("종류", typeof(string));
            table_Boiler.Columns.Add("연료", typeof(string));
            table_Boiler.Columns.Add("제품명", typeof(string));
            table_Boiler.Columns.Add("제조사", typeof(string));
            table_Boiler.Columns.Add("열효율", typeof(string));
            table_Boiler.Columns.Add("전부하\r\n효율", typeof(string));
            table_Boiler.Columns.Add("부분부하\r\n효율", typeof(string));
            table_Boiler.Columns.Add("대기전력", typeof(string));
            table_Boiler.Columns.Add("소비전력", typeof(string));
            //try
            //{
            //    string[][] User_WinFrame = Program.DB.getValue(DB.type.ProjDB, "User_WindowFrame", "번호,DB유형,제품명,제조사,프레임종류,프레임재료,개폐부프레임열관류율,고정부프레임열관류율,중간바프레임열관류율,개폐부프레임두께,고정부프레임두께,중간바프레임두께", "프레임종류 ='" + FrameType + "'");
            //    for (int n = 0; n < User_WinFrame.Length; n++)
            //    {
            //        table_Boiler.Rows.Add(User_WinFrame[n][0], User_WinFrame[n][1], User_WinFrame[n][2], User_WinFrame[n][3], User_WinFrame[n][4], User_WinFrame[n][5], String.Format("{0:F2}", Convert.ToDouble(User_WinFrame[n][6])), String.Format("{0:F2}", Convert.ToDouble(User_WinFrame[n][7])), String.Format("{0:F2}", Convert.ToDouble(User_WinFrame[n][8])), String.Format("{0:F2}", Convert.ToDouble(User_WinFrame[n][9])), String.Format("{0:F2}", Convert.ToDouble(User_WinFrame[n][10])), String.Format("{0:F2}", Convert.ToDouble(User_WinFrame[n][11])));
            //    }
            //}
            //catch { }

            string[][] DBValue = Program.DB.getValue(DB.type.BaseDB_HCneed, "창호프레임", "번호,DB유형,제품명,제조사,프레임종류,프레임재료,개폐부프레임열관류율,고정부프레임열관류율,중간바프레임열관류율,개폐부프레임두께,고정부프레임두께,중간바프레임두께", "");

            for (int n = 0; n < DBValue.Length; n++)
            {
                table_Boiler.Rows.Add(DBValue[n][0], DBValue[n][1], DBValue[n][2], DBValue[n][3], DBValue[n][4], DBValue[n][5], DBValue[n][6], DBValue[n][7], DBValue[n][8], DBValue[n][9], DBValue[n][10], DBValue[n][11]);
            }
            Boiler_dataGridView.DataSource = table_Boiler;
            Count_FrameDB = DBValue.Length;
        }

        private void UserDBName_textBox_TextChanged(object sender, EventArgs e)
        {

            UserDBName = UserDBName_textBox.Text;
        }

        private void UserDB_Manufacture_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Manufacture = UserDB_Manufacture_textBox.Text;
        }
        private void UserDBUw_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Uw = Convert.ToDouble(UserDBUw_textBox.Text);
        }
        private void UserDB_FrameShape_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDB_FrameShape = UserDB_FrameShape_comboBox.SelectedItem.ToString();
            UserDB_FrameShape_textBox.Text = UserDB_FrameShape;
            Load_FrameImage();
        }

        private void UserDB_FrameMaterial_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDB_FrameMaterial_orgin = UserDB_FrameMaterial_comboBox.SelectedItem.ToString();
            UserDB_FrameMaterial = UserDB_FrameMaterial_comboBox.SelectedItem.ToString();
            switch (UserDB_FrameMaterial)
            {
                case "플라스틱":
                    UserDB_FrameMaterial = UserDB_FrameMaterial;
                    break;

                case "금속":
                    UserDB_FrameMaterial = UserDB_FrameMaterial;
                    break;

                case "금속_단열바":
                    UserDB_FrameMaterial = "금속";
                    break;
            }

            //프레임두께
            string[][] WinFrame = Program.DB.getValue(DB.type.BaseDB_HCneed, "창호프레임", "개폐부프레임두께,고정부프레임두께,중간바프레임두께", "프레임재료 ='" + UserDB_FrameMaterial_orgin + "'");
            UserDB_FramedA = Convert.ToDouble(WinFrame[0][0]);
            UserDBFramedA_textBox.Text = String.Format("{0:F2}", UserDB_FramedA);
            UserDB_FramedB = Convert.ToDouble(WinFrame[0][1]);
            UserDBFramedB_textBox.Text = String.Format("{0:F2}", UserDB_FramedB);
            UserDB_FramedC = Convert.ToDouble(WinFrame[0][2]);
            UserDBFramedC_textBox.Text = String.Format("{0:F2}", UserDB_FramedC);

            //간봉리스트
            Load_SpacerList(SingleDoubleType, UserDB_FrameMaterial);
            Load_FrameImage();
        }
        private void UserDBGlass_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDBGlass = UserDBGlass_comboBox.SelectedItem.ToString();
            try
            {
                string[][] User_WinGlass = Program.DB.getValue(DB.type.ProjDB, "User_Glass", "번호,제품명,LE_CL_V,열관류율", "제품명 ='" + UserDBGlass + "'");
                UserDB_LE_CL_V = User_WinGlass[0][2];
                UserDB_Ug = Convert.ToDouble(User_WinGlass[0][3]);
                UserDB_Ug_textBox.Text = String.Format("{0:F3}", UserDB_Ug);
            }
            catch
            {
                string[][] WinGlass = Program.DB.getValue(DB.type.BaseDB_HCneed, "유리", "번호,제품명,LE_CL_V,열관류율", "제품명 ='" + UserDBGlass + "'");
                UserDB_LE_CL_V = WinGlass[0][2];
                UserDB_Ug = Convert.ToDouble(WinGlass[0][3]);
                UserDB_Ug_textBox.Text = String.Format("{0:F3}", UserDB_Ug);
            }

        }

        private void UserDBSpacer_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDBSpacer = UserDBSpacer_comboBox.SelectedItem.ToString();
            try
            {
                string[][] User_WinSpacer = Program.DB.getValue(DB.type.ProjDB, "User_WindowSpacer", "번호,제품명,고정유리_CL_선형열관류율,개폐유리_CL_선형열관류율,고정유리_LE_선형열관류율,개폐유리_LE_선형열관류율", "제품명 = '" + UserDBSpacer + "'AND 구분2 = '" + SingleDoubleType + "'AND 구분3 ='" + UserDB_FrameMaterial + "'");
                if (UserDB_LE_CL_V.Contains("LE"))
                {
                    UserDB_PsiFix = Convert.ToDouble(User_WinSpacer[0][4]);
                    UserDB_PsiOpen = Convert.ToDouble(User_WinSpacer[0][5]);
                }
                else
                {
                    UserDB_PsiFix = Convert.ToDouble(User_WinSpacer[0][2]);
                    UserDB_PsiOpen = Convert.ToDouble(User_WinSpacer[0][3]);
                }
                UserDB_PsiFix_textBox.Text = String.Format("{0:F3}", UserDB_PsiFix);
                UserDB_PsiOpen_textBox.Text = String.Format("{0:F3}", UserDB_PsiOpen);
            }
            catch
            {
                string[][] WinSpacer = Program.DB.getValue(DB.type.BaseDB_HCneed, "창호간봉", "번호,제품명,고정유리_CL_선형열관류율,개폐유리_CL_선형열관류율,고정유리_LE_선형열관류율,개폐유리_LE_선형열관류율", "구분1 = '" + UserDBSpacer + "'AND 구분2 = '" + SingleDoubleType + "'AND 구분3 ='" + UserDB_FrameMaterial + "'");
                if (UserDB_LE_CL_V.Contains("LE"))
                {
                    UserDB_PsiFix = Convert.ToDouble(WinSpacer[0][4]);
                    UserDB_PsiOpen = Convert.ToDouble(WinSpacer[0][5]);
                }
                else
                {
                    UserDB_PsiFix = Convert.ToDouble(WinSpacer[0][2]);
                    UserDB_PsiOpen = Convert.ToDouble(WinSpacer[0][3]);
                }
                UserDB_PsiFix_textBox.Text = String.Format("{0:F3}", UserDB_PsiFix);
                UserDB_PsiOpen_textBox.Text = String.Format("{0:F3}", UserDB_PsiOpen);
            }
        }

        private void Load_SpacerList(String SingleDoubleType, String UserDB_FrameMaterial)
        { //간봉 콤보박스 
            if (SingleDoubleType != null && UserDB_FrameMaterial != null)
            {
                try
                {
                    string[][] User_WinSpacer = Program.DB.getValue(DB.type.ProjDB, "User_WindowSpacer", "번호,제품명,구분1", "구분2 = '" + SingleDoubleType + "'AND 구분3 ='" + UserDB_FrameMaterial + "'");
                    for (int n = 0; n < User_WinSpacer.Length; n++)
                    { SpacerList.Add(User_WinSpacer[n][1]); }
                }
                catch { }
                string[][] WinSpacer = Program.DB.getValue(DB.type.BaseDB_HCneed, "창호간봉", "번호,제품명,구분1", "구분2 = '" + SingleDoubleType + "'AND 구분3 ='" + UserDB_FrameMaterial + "'");
                for (int n = 0; n < WinSpacer.Length; n++)
                {
                    SpacerList.Add(WinSpacer[n][2]);
                }
                string[] SpacerArray = SpacerList.ToArray();
                UserDBSpacer_comboBox.Items.Clear();
                UserDBSpacer_comboBox.Items.AddRange(SpacerArray);
            }
        }
        private void Load_FrameImage()
        {
            if (UserDB_FrameShape != null && UserDB_FrameMaterial != null)
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "창호프레임이미지", "이미지", " 유형2 = '" + UserDB_FrameShape + "' And 재료 = '" + UserDB_FrameMaterial + "'");

                UserDB_Frame_pictureBox.Load(Program.gPath + Image[0][0]);
                UserDB_Frame_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void Import_button_Click(object sender, EventArgs e)
        {
            if (UserNum != null)
            {
                OpenFileDialog f = new OpenFileDialog();
                f.Filter = "( *.bmp; *.jpg; *.png; *.jpeg) | *.BMP; *.JPG; *.PNG; *.JPEG";
                if (f.ShowDialog() == DialogResult.OK)
                {
                    UserDBCertification_pictureBox.Image = Image.FromFile(f.FileName);
                    UserDBCertification_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    UserDBCertification_pictureBox.BackColor = Color.White;
                    UserDB_Image = "images/windowframe/" + UserNum + ".jpg";
                    UserDBCertification_pictureBox.Image.Save(Program.gPath + UserDB_Image, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
            else
            {
                MessageBox.Show("제품명부터 입력하세요.");
            }

        }


        private void AddUserDB_button_Click(object sender, EventArgs e)
        {
            if (UserDB_Uf < 0.5)
            {
                MessageBox.Show("유리를 다시 선택해주세요.");
                UserDB_Uf = 0;
                UserDB_Ug = 0;
            }

            if (UserDB_Image == null)
            {
                MessageBox.Show("시험성적서 이미지를 저장하세요.");
            }
            else if (UserDBName != null && UserDB_FrameShape != null && UserDB_Uf > 0 && UserDB_Uw > 0 && UserDB_Ug > 0 && UserDB_PsiOpen > 0)
            {
                Program.DB.setValue(DB.type.ProjDB, "User_WindowFrame", "번호,DB유형,제품명,제조사,프레임종류,프레임재료,개폐부프레임열관류율,고정부프레임열관류율,중간바프레임열관류율,개폐부프레임두께,고정부프레임두께,중간바프레임두께,시험성적서이미지",
                    "'" + UserNum + "','" + "사용자" + "','" + UserDBName + "','" + UserDB_Manufacture + "','" + UserDB_FrameMaterial + "','" + UserDB_Uf.ToString() + "','" + UserDB_Uf.ToString() + "','" + UserDB_Uf.ToString() + "','" + UserDB_FramedA.ToString() + "','" + UserDB_FramedB.ToString() + "','" + UserDB_FramedC.ToString() + "','" + UserDB_Image + "'", "번호");
                load_table();
            }
            else
            {
                MessageBox.Show("모든 값을 입력해주세요.");
            }
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {

            int k = Boiler_dataGridView.CurrentCell.RowIndex;
            if (k > -1)
            {
                if (Boiler_dataGridView.Rows[k].Cells[2].Value.ToString() == "사용자")
                {
                    if ((MessageBox.Show(Boiler_dataGridView.Rows[k].Cells[3].Value.ToString() + "을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        String Delete_Num = Boiler_dataGridView.Rows[k].Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "User_WindowFrame", "번호 ='" + Delete_Num + "'");
                        load_table();
                    }
                }
                else
                {
                    MessageBox.Show("기본 DB는 삭제할 수 없습니다.");
                }
            }

        }

        //데이터그리드뷰 체크박스 선택 시
        private void Frame_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Boiler_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = Boiler_dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Count_FrameDB; k++)
                {
                    if (k != row.Index)
                    {
                        Boiler_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = Boiler_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = Boiler_dataGridView.Rows[e.RowIndex];
                    }
                }
            }
        }
        private void Save_button_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = Boiler_dataGridView.Rows[SelectRow];
            for (int i = 1; i < row.Cells.Count - 2; i++)
            {
                Select_WindowFrame[i] = row.Cells[i + 2].Value.ToString();
            }
            Select_WindowFrame[0] = row.Cells[1].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
