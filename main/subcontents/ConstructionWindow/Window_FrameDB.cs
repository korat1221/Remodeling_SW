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
    public partial class Window_FrameDB : Form
    {
        String FrameType;
        double Count_FrameDB;
        int SelectRow;
        public string[] Select_WindowFrame = new string[11];
        String UserNum, UserDBName, UserDB_Manufacture, UserDB_FrameShape, UserDB_FrameMaterial_orgin, UserDB_FrameMaterial, UserDBGlass, UserDBSpacer, SingleDoubleType, UserDB_LE_CL_V, UserDB_Image;
        Double UserDB_Uw, UserDB_FramedA, UserDB_FramedB, UserDB_FramedC, UserDB_Ug, UserDB_PsiOpen, UserDB_PsiFix;
        Double UserDB_Ag, UserDB_Af, UserDB_Lopen, UserDB_Lfix, UserDB_Uf;
        List<String> GlassList = new List<String>();
        List<String> SpacerList = new List<String>();

        public Window_FrameDB(String FrameType, String SingleDoubleType)
        {
            InitializeComponent();
            UserNum = Program.UTIL.CreateNum("User_WindowFrame", "번호", "UWF_0");
            UserNum_textBox.Text = UserNum;
            this.FrameType = FrameType;
            this.SingleDoubleType = SingleDoubleType;
            load_table_FrameDB();

            //프레임 유형 콤보박스 
            UserDB_FrameType_comboBox.Text = this.FrameType;
            UserDB_FrameType_comboBox.Enabled = false;
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
        void load_table_FrameDB()
        {
            new StackedHeaderDecorator(Frame_dataGridView);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Frame_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Frame_dataGridView.Columns.Add(checkBoxColumn);

            Frame_dataGridView.Columns.Add("A1", "번호");
            Frame_dataGridView.Columns.Add("A2", "DB유형");
            Frame_dataGridView.Columns.Add("A3", "제품명");
            Frame_dataGridView.Columns.Add("A4", "제조사");
            Frame_dataGridView.Columns.Add("A5", "종류");
            Frame_dataGridView.Columns.Add("A6", "재료");
            Frame_dataGridView.Columns.Add("A7", "프레임열관류율.개폐부.Uf,A.[W/m2∙K]");
            Frame_dataGridView.Columns.Add("A8", "프레임열관류율.고정부.Uf,B.[W/m2∙K]");
            Frame_dataGridView.Columns.Add("A9", "프레임열관류율.중간바.Uf,C.[W/m2∙K]");
            Frame_dataGridView.Columns.Add("A10", "프레임두께.개폐부.dA.[m]");
            Frame_dataGridView.Columns.Add("A11", "프레임두께.고정부.dB.[m]"); ;
            Frame_dataGridView.Columns.Add("A12", "프레임두께.중간바.dC.[m]");

            //table_WindowFrame.Columns.Add("번호", typeof(string));
            //table_WindowFrame.Columns.Add("DB유형", typeof(string));
            //table_WindowFrame.Columns.Add("제품명", typeof(string));
            //table_WindowFrame.Columns.Add("제조사", typeof(string));
            //table_WindowFrame.Columns.Add("프레임\r\n종류", typeof(string));
            //table_WindowFrame.Columns.Add("프레임\r\n재료", typeof(string));
            //table_WindowFrame.Columns.Add("개폐부프레임\r\n열관류율" + Environment.NewLine + "Uf,A[W/m2∙K]", typeof(string));
            //table_WindowFrame.Columns.Add("고정부프레임\r\n열관류율" + Environment.NewLine + "Uf,B[W/m2∙K]", typeof(string));
            //table_WindowFrame.Columns.Add("중간바프레임\r\n열관류율" + Environment.NewLine + "Uf,C[W/m2∙K]", typeof(string));
            //table_WindowFrame.Columns.Add("개폐부\r\n프레임두께" + Environment.NewLine + "dA[m]", typeof(string));
            //table_WindowFrame.Columns.Add("고정부\r\n프레임두께" + Environment.NewLine + "dB[m]", typeof(string));
            //table_WindowFrame.Columns.Add("중간바\r\n프레임두께" + Environment.NewLine + "dC[m]", typeof(string));
            try
            {
                string[][] User_WinFrame = Program.DB.getValue(DB.type.ProjDB, "User_WindowFrame", "번호,DB유형,제품명,제조사,프레임종류,프레임재료,개폐부프레임열관류율,고정부프레임열관류율,중간바프레임열관류율,개폐부프레임두께,고정부프레임두께,중간바프레임두께", "프레임종류 ='" + FrameType + "'");
                for (int n = 0; n < User_WinFrame.Length; n++)
                {
                    Frame_dataGridView.Rows.Add();
                    int nRow = Frame_dataGridView.Rows.Count - 1;
                    for (int k = 0; k < 12; k++)
                    {
                        Frame_dataGridView.Rows[nRow].Cells[k + 1].Value = User_WinFrame[n][k];
                    }
                   // table_WindowFrame.Rows.Add(User_WinFrame[n][0], User_WinFrame[n][1], User_WinFrame[n][2], User_WinFrame[n][3], User_WinFrame[n][4], User_WinFrame[n][5], String.Format("{0:F2}", Convert.ToDouble(User_WinFrame[n][6])), String.Format("{0:F2}", Convert.ToDouble(User_WinFrame[n][7])), String.Format("{0:F2}", Convert.ToDouble(User_WinFrame[n][8])), String.Format("{0:F2}", Convert.ToDouble(User_WinFrame[n][9])), String.Format("{0:F2}", Convert.ToDouble(User_WinFrame[n][10])), String.Format("{0:F2}", Convert.ToDouble(User_WinFrame[n][11])));
                }
            }
            catch { }

            string[][] WinFrame = Program.DB.getValue(DB.type.BaseDB_HCneed, "창호프레임", "번호,DB유형,제품명,제조사,프레임종류,프레임재료,개폐부프레임열관류율,고정부프레임열관류율,중간바프레임열관류율,개폐부프레임두께,고정부프레임두께,중간바프레임두께", "프레임종류 ='" + FrameType + "'");

            for (int n = 0; n < WinFrame.Length; n++)
            {
                Frame_dataGridView.Rows.Add();
                int nRow = Frame_dataGridView.Rows.Count - 1;
                for (int k = 0; k < 12; k++)
                {
                    Frame_dataGridView.Rows[nRow].Cells[k + 1].Value = WinFrame[n][k];
                }
               // table_WindowFrame.Rows.Add(WinFrame[n][0], WinFrame[n][1], WinFrame[n][2], WinFrame[n][3], WinFrame[n][4], WinFrame[n][5], WinFrame[n][6], WinFrame[n][7], WinFrame[n][8], WinFrame[n][9], WinFrame[n][10], WinFrame[n][11]);
            }
            //Frame_dataGridView.DataSource = table_WindowFrame;
            Count_FrameDB = WinFrame.Length;
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
            Calc_Uf();
        }
        private void UserDB_FrameShape_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDB_FrameShape = UserDB_FrameShape_comboBox.SelectedItem.ToString();
            UserDB_FrameShape_textBox.Text = UserDB_FrameShape;
            Calc_Uf();
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
            string[][] WinFrame = Program.DB.getValue(DB.type.BaseDB_HCneed, "창호프레임", "개폐부프레임두께,고정부프레임두께,중간바프레임두께", "프레임종류 ='" + FrameType + "'AND 프레임재료 ='" + UserDB_FrameMaterial_orgin + "'");
            UserDB_FramedA = Convert.ToDouble(WinFrame[0][0]);
            UserDBFramedA_textBox.Text = String.Format("{0:F2}", UserDB_FramedA);
            UserDB_FramedB = Convert.ToDouble(WinFrame[0][1]);
            UserDBFramedB_textBox.Text = String.Format("{0:F2}", UserDB_FramedB);
            UserDB_FramedC = Convert.ToDouble(WinFrame[0][2]);
            UserDBFramedC_textBox.Text = String.Format("{0:F2}", UserDB_FramedC);

            //간봉리스트
            Load_SpacerList(SingleDoubleType, UserDB_FrameMaterial);
            Calc_Uf();
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
            Calc_Uf();

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
            Calc_Uf();
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
            if (FrameType != null && UserDB_FrameShape != null && UserDB_FrameMaterial != null)
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "창호프레임이미지", "이미지", "유형1 = '" + FrameType + "' And 유형2 = '" + UserDB_FrameShape + "' And 재료 = '" + UserDB_FrameMaterial + "'");

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

        private void Calc_Uf()
        {
            switch (UserDB_FrameShape)
            {
                case "기본형":
                    {
                        UserDB_Ag = (2 - (UserDB_FramedA + UserDB_FramedB + UserDB_FramedC)) * (2 - 2 * (0.5 * UserDB_FramedA + 0.5 * UserDB_FramedB));
                        UserDB_Af = 4 - UserDB_Ag;
                        UserDB_Lopen = 2 * (2 - 2 * UserDB_FramedA) + 2 * (2 * 0.5 - (UserDB_FramedA + UserDB_FramedC * 0.5));
                        UserDB_Lfix = 2 * (2 - 2 * UserDB_FramedB) + 2 * (2 * 0.5 - (UserDB_FramedB + UserDB_FramedC * 0.5));
                        break;
                    }

                case "3단형":
                    {
                        if (FrameType == "이중창_SL")
                        {
                            UserDB_Ag = (2 - (UserDB_FramedB + (UserDB_FramedA * 0.5 + UserDB_FramedB * 1.5) / 2 + (UserDB_FramedC * 0.5 + UserDB_FramedB * 1.5) / 2)) * (2 - (UserDB_FramedB + 0.5 * UserDB_FramedC + (UserDB_FramedA * 1 + UserDB_FramedB * 1) / 2)) - ((0.5 - UserDB_FramedA) * UserDB_FramedB);
                            UserDB_Lopen = 2 * ((1 - UserDB_FramedA) - UserDB_FramedB) + 4 * (0.5 - UserDB_FramedA);
                        }
                        else
                        {
                            UserDB_Ag = (2 - (UserDB_FramedB + (UserDB_FramedA * 0.5 + UserDB_FramedB * 1.5) / 2 + (UserDB_FramedC * 0.5 + UserDB_FramedB * 1.5) / 2)) * (2 - (UserDB_FramedB + 0.5 * UserDB_FramedC + (UserDB_FramedA * 1 + UserDB_FramedB * 1) / 2));
                            UserDB_Lopen = 2 * (1 - UserDB_FramedA) + 2 * (0.5 - UserDB_FramedA);
                        }
                        UserDB_Af = 4 - UserDB_Ag;
                        UserDB_Lfix = 2 * (2 - 2 * UserDB_FramedB) + 2 * (2 - (0.5 + UserDB_FramedC + UserDB_FramedB)) + 2 * (2 - 3 * UserDB_FramedB);
                        break;
                    }
                case "4단형":
                    {
                        UserDB_Ag = (2 - (UserDB_FramedB + (UserDB_FramedA * 0.5 + UserDB_FramedB * 1.5) / 2 + (UserDB_FramedC * 0.5 + UserDB_FramedB * 1.5) / 2)) * (2 - (UserDB_FramedB + (UserDB_FramedC * 1 + UserDB_FramedB * 1) / 2 + (UserDB_FramedA * 1 + UserDB_FramedB * 1) / 2));
                        UserDB_Af = 4 - UserDB_Ag;
                        UserDB_Lopen = 2 * (1 - UserDB_FramedA) + 2 * (0.5 - UserDB_FramedA);
                        UserDB_Lfix = 4 * (2 - (0.5 + UserDB_FramedC + UserDB_FramedB)) + 2 * (0.5 - UserDB_FramedB) + 4 * (2 - (1 + UserDB_FramedC + UserDB_FramedB)) + 2 * (1 - UserDB_FramedB);
                        break;
                    }
            }
            if (UserDB_Uw > 0 && UserDB_Ug > 0 && UserDB_Ag > 0 && UserDB_PsiOpen > 0)
            {
                UserDB_Uf = (UserDB_Uw * 4 - UserDB_Ug * UserDB_Ag - UserDB_PsiOpen * UserDB_Lopen - UserDB_PsiFix * UserDB_Lfix) / UserDB_Af;      
            }

            if (UserDB_Uf > 0.5)
            {
                UserDB_UfA_textBox.Text = String.Format("{0:F3}", UserDB_Uf);
                UserDB_UfB_textBox.Text = String.Format("{0:F3}", UserDB_Uf);
                UserDB_UfC_textBox.Text = String.Format("{0:F3}", UserDB_Uf);
            }
            else
            {
                UserDB_UfA_textBox.Text = "유리 Check";
                UserDB_UfB_textBox.Text = "유리 Check";
                UserDB_UfC_textBox.Text = "유리 Check";
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
                    "'" + UserNum + "','" + "사용자" + "','" + UserDBName + "','" + UserDB_Manufacture + "','" + FrameType + "','" + UserDB_FrameMaterial + "','" + UserDB_Uf.ToString() + "','" + UserDB_Uf.ToString() + "','" + UserDB_Uf.ToString() + "','" + UserDB_FramedA.ToString() + "','" + UserDB_FramedB.ToString() + "','" + UserDB_FramedC.ToString() + "','" + UserDB_Image + "'", "번호");
                load_table_FrameDB();
            }
            else
            {
                MessageBox.Show("모든 값을 입력해주세요.");
            }
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {

            int k = Frame_dataGridView.CurrentCell.RowIndex;
            if (k > -1)
            {
                if (Frame_dataGridView.Rows[k].Cells[2].Value.ToString() == "사용자")
                {
                    if ((MessageBox.Show(Frame_dataGridView.Rows[k].Cells[3].Value.ToString() + "을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        String Delete_Num = Frame_dataGridView.Rows[k].Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "User_WindowFrame", "번호 ='" + Delete_Num + "'");
                        load_table_FrameDB();
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
                Frame_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;               
            }
        }
        private void Save_button_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = Frame_dataGridView.Rows[SelectRow];
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
