using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents.ConstructionCW
{
    public partial class CW_FrameDB : Form
    {
        String FrameType;
        double Count_FrameDB;
        int SelectRow;
        public string[] Select_CWFrame = new string[11];
        String UserNum, UserDBName, UserDB_Manufacture, UserDB_FrameShape, UserDBGlass, UserDBSpacer, UserDBSpacer_Type, UserDB_LE_CL_V, UserDB_Image;
        Double UserDB_Ucw, UserDB_FramedA, UserDB_FramedB, UserDB_FramedC, UserDB_Ug, UserDB_Psimt, UserDB_PsiOpen;
        Double UserDB_Ag, UserDB_Af, UserDB_Lopen, UserDB_Lfix, UserDB_Uf, UserDB_Psip;
        List<String> GlassList = new List<String>();
        List<String> SpacerList = new List<String>();

        public CW_FrameDB(String FrameType)
        {
            InitializeComponent();
            UserNum = Program.UTIL.CreateNum("User_CWFrame", "번호", "UCW_0");
            UserNum_textBox.Text = UserNum;
            this.FrameType = FrameType;
            load_table_FrameDB();

            //프레임 유형 콤보박스 
            UserDB_FrameType_comboBox.Text = this.FrameType;
            UserDB_FrameType_comboBox.Enabled = false;
            //프레임 형태 콤보박스 
            // Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, UserDB_FrameShape_comboBox, "커튼월", "형태", "1");
            UserDB_FrameShape_comboBox.Items.Add("기본형");
            UserDB_FrameShape_comboBox.Items.Add("1단형");
            UserDB_FrameShape_comboBox.Items.Add("3단형");
            UserDB_FrameShape_comboBox.Items.Add("4단형");
            //유리 콤보박스
            string[][] User_Glass = Program.DB.getValue(DB.type.ProjDB, "User_Glass", "번호,제품명", "");
            if (User_Glass.Length > 0)
            {
                for (int n = 0; n < User_Glass.Length; n++)
                { GlassList.Add(User_Glass[n][1]); }
            }
            string[][] Glass = Program.DB.getValue(DB.type.BaseDB_HCneed, "유리", "번호,제품명", "");
            if (Glass.Length > 0)
            {
                for (int n = 0; n < Glass.Length; n++)
                {
                    GlassList.Add(Glass[n][1]);
                }
            }           
            string[] GlassArray = GlassList.ToArray();
            UserDBGlass_comboBox.Items.Clear();
            UserDBGlass_comboBox.Items.AddRange(GlassArray);
            Load_SpacerList();


        }
        void load_table_FrameDB()
        {
            new StackedHeaderDecorator(Frame_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Frame_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Frame_dataGridView.Columns.Add(checkBoxColumn);

            Frame_dataGridView.Columns.Add("A1", "번호");
            Frame_dataGridView.Columns.Add("A2", "DB유형");
            Frame_dataGridView.Columns.Add("A3", "제품명");
            Frame_dataGridView.Columns.Add("A4", "제조사");
            Frame_dataGridView.Columns.Add("A5", "구분1");
            Frame_dataGridView.Columns.Add("A6", "구분2");
            Frame_dataGridView.Columns.Add("A7", "열관류율.고정부프레임.Umt[W/m2∙K]");
            Frame_dataGridView.Columns.Add("A8", "열관류율.개폐부프레임.Ufr[W/m2∙K]");
            Frame_dataGridView.Columns.Add("A9", "열관류율.패널엣지선형.Up,mt[W/m∙K]");
            Frame_dataGridView.Columns.Add("A10", "두께.M/T프레임.dA[m]");
            Frame_dataGridView.Columns.Add("A11", "두께.fr프레임.dB[m]");       
            
                string[][] User_CWFrame = Program.DB.getValue(DB.type.ProjDB, "User_CWFrame", "번호,DB유형,제품명,제조사,구분1,구분2,고정부프레임열관류율,개폐부프레임열관류율,패널엣지선형열관류율,M_T프레임두께,fr프레임두께", "구분1 ='" + FrameType + "'");
                if(User_CWFrame.Length > 0 )
                {
                    for (int n = 0; n < User_CWFrame.Length; n++)
                    {
                        Frame_dataGridView.Rows.Add();
                        int nRow = Frame_dataGridView.Rows.Count - 1;
                        for (int i = 0; i < 6; i++)
                        {
                            Frame_dataGridView.Rows[nRow].Cells[i + 1].Value = User_CWFrame[n][i];
                        }
                        for (int i = 6; i < 11; i++)
                        {
                            Frame_dataGridView.Rows[nRow].Cells[i + 1].Value = String.Format("{0:F2}", Convert.ToDouble(User_CWFrame[n][i]));
                        }                       
                    }
                }        
            string[][] CWFrame = Program.DB.getValue(DB.type.BaseDB_HCneed, "커튼월프레임", "번호,DB유형,제품명,제조사,구분1,구분2,고정부프레임열관류율,개폐부프레임열관류율,패널엣지선형열관류율,M_T프레임두께,fr프레임두께", "구분1 ='" + FrameType + "'");
            if(CWFrame.Length > 0 )
            {
                for (int n = 0; n < CWFrame.Length; n++)
                {
                    Frame_dataGridView.Rows.Add();
                    int nRow = Frame_dataGridView.Rows.Count - 1;
                    for (int i = 0; i < 11; i++)
                    {
                        Frame_dataGridView.Rows[nRow].Cells[i + 1].Value = CWFrame[n][i];
                    }                   
                }
            }          
            Count_FrameDB = CWFrame.Length;
        }


        private Boolean datagridviewDesign(DataGridViewCell cell, int column, int row)
        {
            if (row % 2 == 1)
            {
                cell.Style.BackColor = Color.FromArgb(251, 251, 251);
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = Color.FromArgb(251, 251, 251);
                cell.Style.SelectionForeColor = Color.Black;
                return true;
            }
            else return false;
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
            UserDB_Ucw = Convert.ToDouble(UserDBUw_textBox.Text);
            Calc_Uf();
        }
        private void UserDB_FrameShape_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDB_FrameShape = UserDB_FrameShape_comboBox.SelectedItem.ToString();
            UserDB_FrameShape_textBox.Text = UserDB_FrameShape;
            UserDB_FramedA = 0.06;
            UserDBFramedA_textBox.Text = (0.06).ToString();
            UserDB_FramedB = 0.08;
            UserDBFramedB_textBox.Text = (0.08).ToString();
            Calc_Uf();
            Load_FrameImage();
        }

        private void UserDBGlass_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDBGlass = UserDBGlass_comboBox.SelectedItem.ToString();
                string[][] User_WinGlass = Program.DB.getValue(DB.type.ProjDB, "User_Glass", "번호,제품명,LE_CL_V,열관류율", "제품명 ='" + UserDBGlass + "'");
                if(User_WinGlass.Length > 0)
                {
                    UserDB_LE_CL_V = User_WinGlass[0][2];
                    UserDB_Ug = Convert.ToDouble(User_WinGlass[0][3]);
                    UserDB_Ug_textBox.Text = String.Format("{0:F3}", UserDB_Ug);
                }
          
           string[][] WinGlass = Program.DB.getValue(DB.type.BaseDB_HCneed, "유리", "번호,제품명,LE_CL_V,열관류율", "제품명 ='" + UserDBGlass + "'");
            if(WinGlass.Length > 0)
            {
                UserDB_LE_CL_V = WinGlass[0][2];
                UserDB_Ug = Convert.ToDouble(WinGlass[0][3]);
                UserDB_Ug_textBox.Text = String.Format("{0:F3}", UserDB_Ug);
            }
            Calc_Uf();
        }

        private void UserDBSpacer_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDBSpacer = UserDBSpacer_comboBox.SelectedItem.ToString();
            
                string[][] User_CWSpacer = Program.DB.getValue(DB.type.ProjDB, "User_CWSpacer", "번호,제품명,고정유리_CL_선형열관류율,개폐유리_CL_선형열관류율,고정유리_LE_선형열관류율,개폐유리_LE_선형열관류율,구분1", "제품명 = '" + UserDBSpacer + "'AND 구분3 ='" + FrameType + "'");
                if(User_CWSpacer.Length > 0)
                {
                    if (UserDB_LE_CL_V.Contains("LE"))
                    {
                        UserDB_Psimt = Convert.ToDouble(User_CWSpacer[0][4]);
                        UserDB_PsiOpen = Convert.ToDouble(User_CWSpacer[0][5]);
                    }
                    else
                    {
                        UserDB_Psimt = Convert.ToDouble(User_CWSpacer[0][2]);
                        UserDB_PsiOpen = Convert.ToDouble(User_CWSpacer[0][3]);
                    }
                    UserDBSpacer_Type = User_CWSpacer[0][6];
                    UserDB_Psip = Convert.ToDouble(User_CWSpacer[0][2]);
                    UserDB_PsiOpen_textBox.Text = String.Format("{0:F3}", UserDB_PsiOpen);
                    UserDB_Psimt_textBox.Text = String.Format("{0:F3}", UserDB_Psimt);
                }
        
            string[][] CWSpacer = Program.DB.getValue(DB.type.BaseDB_HCneed, "커튼월간봉", "번호,제품명,고정유리_CL_선형열관류율,개폐유리_CL_선형열관류율,고정유리_LE_선형열관류율,개폐유리_LE_선형열관류율", "구분1 = '" + UserDBSpacer + "'AND 구분3 ='" + FrameType + "'");
            if(CWSpacer.Length > 0)
            {
                if (UserDB_LE_CL_V.Contains("LE"))
                {
                    UserDB_Psimt = Convert.ToDouble(CWSpacer[0][4]);
                    UserDB_PsiOpen = Convert.ToDouble(CWSpacer[0][5]);
                }
                else
                {
                    UserDB_Psimt = Convert.ToDouble(CWSpacer[0][2]);
                    UserDB_PsiOpen = Convert.ToDouble(CWSpacer[0][3]);
                }
                UserDBSpacer_Type = UserDBSpacer;
                UserDB_Psip = Convert.ToDouble(CWSpacer[0][2]);
                UserDB_PsiOpen_textBox.Text = String.Format("{0:F3}", UserDB_PsiOpen);
                UserDB_Psimt_textBox.Text = String.Format("{0:F3}", UserDB_Psimt);
            }
            Calc_Uf();
        }

        private void Load_SpacerList()
        { //간봉 콤보박스 

            string[][] UserCWSpacer = Program.DB.getValue(DB.type.ProjDB, "User_CWSpacer", "번호,제품명,구분1", "구분3 ='" + FrameType + "'");
            if (UserCWSpacer.Length > 0)
            {
                for (int n = 0; n < UserCWSpacer.Length; n++)
                { SpacerList.Add(UserCWSpacer[n][1]); }
            }
            string[][] CWSpacer = Program.DB.getValue(DB.type.BaseDB_HCneed, "커튼월간봉", "번호,제품명,구분1", "구분3 ='" + FrameType + "'");
            if(CWSpacer.Length > 0)
            {
                for (int n = 0; n < CWSpacer.Length; n++)
                {
                    SpacerList.Add(CWSpacer[n][2]);
                }
                string[] SpacerArray = SpacerList.ToArray();
                UserDBSpacer_comboBox.Items.Clear();
                UserDBSpacer_comboBox.Items.AddRange(SpacerArray);
            }            
        }
        private void Load_FrameImage()
        {
            if (FrameType != null && UserDB_FrameShape != null)
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "커튼월프레임이미지", "이미지", "유형 = '" + UserDB_FrameShape + "'");
                if(Image.Length > 0)
                {
                    UserDB_Frame_pictureBox.Load(Program.gPath + Image[0][0]);
                    UserDB_Frame_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
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
                    UserDB_Image = "images/CWframe/" + UserNum + ".jpg";
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
                        UserDB_Ag = (1 - (UserDB_FramedA + UserDB_FramedA * 0.5 + UserDB_FramedB * 2)) * (2 - 2 * (UserDB_FramedA + UserDB_FramedB)) + (1 - (UserDB_FramedA + UserDB_FramedA * 0.5)) * (2 - 2 * UserDB_FramedA);
                        UserDB_Af = 4 - UserDB_Ag;
                        UserDB_Lopen = (1 - (UserDB_FramedA + UserDB_FramedA * 0.5 + UserDB_FramedB * 2)) * 2 + (2 - 2 * (UserDB_FramedA + UserDB_FramedB)) * 2;
                        UserDB_Lfix = (1 - (UserDB_FramedA + UserDB_FramedA * 0.5)) * 2 + (2 - 2 * UserDB_FramedA) * 2;
                        break;
                    }
                case "1단형":
                    {
                        UserDB_Ag = (2 - UserDB_FramedA * 2) * (2 - UserDB_FramedA * 2);
                        UserDB_Af = 4 - UserDB_Ag;
                        UserDB_Lfix = (2 - UserDB_FramedA * 2) * 4;
                        break;
                    }

                case "3단형":
                    {

                        UserDB_Ag = (1 - (UserDB_FramedA + UserDB_FramedB)) * (0.5 - (UserDB_FramedA + UserDB_FramedB)) + (2 - 0.5 - 2 * UserDB_FramedA) * (1 - (UserDB_FramedA + UserDB_FramedA * 0.5)) + (1 - (UserDB_FramedA + UserDB_FramedA * 0.5)) * (2 - 2 * UserDB_FramedA);
                        UserDB_Af = 4 - UserDB_Ag;
                        UserDB_Lopen = 2 * (1 - (UserDB_FramedA + UserDB_FramedB)) + 2 * (0.5 - (UserDB_FramedA + UserDB_FramedB));
                        UserDB_Lfix = (1 - (UserDB_FramedA + UserDB_FramedA * 0.5)) * 2 + (2 - 2 * UserDB_FramedA) * 2 + (2 - 0.5 - 2 * UserDB_FramedA) * 2 + (1 - (UserDB_FramedA + UserDB_FramedA * 0.5)) * 2;
                        break;
                    }
                case "4단형":
                    {
                        UserDB_Ag = (1 - (UserDB_FramedA + UserDB_FramedB)) * (0.5 - (UserDB_FramedA + UserDB_FramedB)) + (2 - 0.5 - 2 * UserDB_FramedA) * (1 - (UserDB_FramedA + UserDB_FramedA * 0.5)) + (1 - (UserDB_FramedA + UserDB_FramedA * 0.5)) * (2 - 2 * UserDB_FramedA) - (2 - 2 * UserDB_FramedA) * UserDB_FramedA;
                        UserDB_Af = 4 - UserDB_Ag;
                        UserDB_Lopen = 2 * (1 - UserDB_FramedB) + 2 * (0.5 - UserDB_FramedB);
                        UserDB_Lfix = (1 - (UserDB_FramedA + UserDB_FramedA * 0.5)) * 2 + (2 - 3 * UserDB_FramedA) * 2 + (2 - 0.5 - 2 * UserDB_FramedA) * 2 + (1 - (UserDB_FramedA + UserDB_FramedA * 0.5)) * 2 + (2 - 2 * UserDB_FramedA) * 2;
                        break;
                    }
            }
            if (UserDB_Ucw > 0 && UserDB_Ug > 0 && UserDB_Ag > 0 && UserDB_Psimt > 0)
            {
                // UserDB_Uf = (UserDB_Ucw * 4 - UserDB_Ug * UserDB_Ag - UserDB_Psimt * UserDB_Lfix - UserDB_PsiOpen * UserDB_Lopen) / UserDB_Af;
                UserDB_Uf = (UserDB_Ucw * 4 - UserDB_Ug * UserDB_Ag) / UserDB_Af;
            }
            if (UserDB_Uf > 1.2)
            {
                UserDB_UfA_textBox.Text = String.Format("{0:F3}", UserDB_Uf);
                UserDB_UfB_textBox.Text = String.Format("{0:F3}", UserDB_Uf);
                UserDB_UfC_textBox.Text = String.Format("{0:F3}", UserDB_Psip);
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
            if (UserDB_Uf < 1.2)
            {
                MessageBox.Show("유리를 다시 선택해주세요.");
                UserDB_Uf = 0;
                UserDB_Ug = 0;
            }

            if (UserDB_Image == null)
            {
                MessageBox.Show("시험성적서 이미지를 저장하세요.");
            }
            else if (UserDBName != null && UserDB_FrameShape != null && UserDB_Uf > 0 && UserDB_Ucw > 0 && UserDB_Ug > 0 && UserDB_Psimt > 0)
            {
                string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
                Program.DB.setValue(DB.type.ProjDB, "User_CWFrame", "번호,프로젝트유형,DB유형,제품명,제조사,구분1,구분2,고정부프레임열관류율,개폐부프레임열관류율,패널엣지선형열관류율,M_T프레임두께,fr프레임두께,시험성적서이미지",
                    "'" + UserNum + "','" + 프로젝트유형[0][0] +"','" + "사용자" + "','" + UserDBName + "','" + UserDB_Manufacture + "','" + FrameType + "','" + UserDBSpacer_Type + "','" + UserDB_Uf.ToString() + "','" + UserDB_Uf.ToString() + "','" + UserDB_Psip.ToString() + "','" + UserDB_FramedA.ToString() + "','" + UserDB_FramedB.ToString() + "','" + UserDB_Image + "'", "번호");
                load_table_FrameDB();
                UserNum = Program.UTIL.CreateNum("User_CWFrame", "번호", "UCW_0");
                UserNum_textBox.Text = UserNum;
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
                        Program.DB.deleteValue(DB.type.ProjDB, "User_CWFrame", "번호 ='" + Delete_Num + "'");
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
                Select_CWFrame[i] = row.Cells[i + 2].Value.ToString();
            }
            Select_CWFrame[0] = row.Cells[1].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
