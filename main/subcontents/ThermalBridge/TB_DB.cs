using main.info;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace main.subcontents.ThermalBridge;

public partial class TB_DB : Form
{
    int SelectRow;
    String TBType;
    string StructureType1, StructureType2;
    public string TBNum;
    String UserTBNum;

    public TB_DB(String TBType)
    {
        InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
        Program.UTIL.IgnoreGridError(this);
        this.TBType = TBType;
        TBType_textBox.Text = TBType;
        load_table_DB();
        Load_Image1();
    }


    void load_table_DB()
    {
        string[][] value = Program.DB.getValue(DB.type.BaseDB_HCneed, "접합부열교", "구조체1, 구조체2", "열교유형='" + TBType + "'");
        if (value.Length > 0)
        {
            StructureType1 = value[0][0];
            StructureType2 = value[0][1];
        }

        new StackedHeaderDecorator(TB_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
        TB_dataGridView.Columns.Clear();
        checkBoxColumn.HeaderText = "선택";
        checkBoxColumn.Name = "check";
        TB_dataGridView.Columns.Add(checkBoxColumn);

        TB_dataGridView.Columns.Add("A1", "번호");
        TB_dataGridView.Columns.Add("A2", "DB타입");
        TB_dataGridView.Columns.Add("A3", "유형");
        TB_dataGridView.Columns.Add("A4", "명칭");
        TB_dataGridView.Columns.Add("A5", StructureType1);
        TB_dataGridView.Columns.Add("A6", StructureType2);
        TB_dataGridView.Columns.Add("A7", "선형 열관류율.[W/mK]");

        TB_dataGridView.Columns[0].Width = 30;
        TB_dataGridView.Columns[1].Width = 50;
        TB_dataGridView.Columns[2].Width = 50;

        value = Program.DB.getValue(DB.type.BaseDB_HCneed, "접합부열교", "번호,DB유형,열교유형,명칭,구조체1_단열유형,구조체2_단열유형,값", "열교유형='" + TBType + "'");
        if (value.Length > 0)
        {
            for (int i = 0; i < value.Length; i++)
            {
                int nRow = TB_dataGridView.Rows.Add();
                TB_dataGridView.Rows[nRow].Cells[1].Value = value[i][0];
                TB_dataGridView.Rows[nRow].Cells[2].Value = value[i][1];
                TB_dataGridView.Rows[nRow].Cells[3].Value = value[i][2];
                TB_dataGridView.Rows[nRow].Cells[4].Value = value[i][3];
                TB_dataGridView.Rows[nRow].Cells[5].Value = value[i][4];
                TB_dataGridView.Rows[nRow].Cells[6].Value = value[i][5];
                TB_dataGridView.Rows[nRow].Cells[7].Value = Program.UTIL.ToDoubleOrZero(value[i][6]).ToString("0.000");
            }
        }

        value = Program.DB.getValue(DB.type.ProjDB, "User_TB", "번호,DB타입,유형,명칭,구조체1_단열유형,구조체2_단열유형,값", "유형='" + TBType + "'");
        if (value.Length > 0)
        {
            for (int i = 0; i < value.Length; i++)
            {
                int nRow = TB_dataGridView.Rows.Add();
                TB_dataGridView.Rows[nRow].Cells[1].Value = value[i][0];
                TB_dataGridView.Rows[nRow].Cells[2].Value = value[i][1];
                TB_dataGridView.Rows[nRow].Cells[3].Value = value[i][2];
                TB_dataGridView.Rows[nRow].Cells[4].Value = value[i][3];
                TB_dataGridView.Rows[nRow].Cells[5].Value = value[i][4];
                TB_dataGridView.Rows[nRow].Cells[6].Value = value[i][5];
                TB_dataGridView.Rows[nRow].Cells[7].Value = Program.UTIL.ToDoubleOrZero(value[i][6]).ToString("0.000");
            }
        }
    }
    private Boolean datagridviewDesign(DataGridViewCell cell, int column, int row)
    {
        if (row % 2 == 1)
        {
            if (TB_dataGridView.Rows[row].Cells[column].Value != null)
            {
                cell.Style.BackColor = Color.FromArgb(251, 251, 251);
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = Color.FromArgb(251, 251, 251);
                cell.Style.SelectionForeColor = Color.Black;
                return true;
            }
            else return false;
        }
        else return false;
    }
    private void Load_Image1()
    {
        string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "접합부열교_이미지", "대분류_이미지", "대분류 = '" + TBType + "'");
        if (Image.Length > 0)
        {
            for (int i = 0; i < Image.Length; i++)
            {
                pictureBox1.Load(Program.gPath + Image[0][0]);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }

        }
    }



    //데이터그리드뷰 체크박스 선택 시
    private void TB_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            TB_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            SelectRow = e.RowIndex;
            for (int i = 0; i < TB_dataGridView.Rows.Count; i++)
            {
                if (i != SelectRow) { TB_dataGridView.Rows[i].Cells[0].Value = false; }
            }

            if (TB_dataGridView.Rows[SelectRow].Cells[4].Value != null)
            { TBName_textBox.Text = TB_dataGridView.Rows[SelectRow].Cells[4].Value.ToString(); }//명칭
            if (TB_dataGridView.Rows[SelectRow].Cells[7].Value != null)
            { result_textBox.Text = TB_dataGridView.Rows[SelectRow].Cells[7].Value.ToString(); }//선형열관류율
            result_textBox2.Text = "W/m·K";
            Load_Image2();
        }
    }

    private void Load_Image2()
    {
        if (TB_dataGridView.Rows[SelectRow].Cells[2].Value != null && TB_dataGridView.Rows[SelectRow].Cells[2].Value.ToString() != "사용자DB")
        {

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "접합부열교_이미지", "소분류_이미지1", "대분류 ='" + TBType + "' and 소분류 = '" + TB_dataGridView.Rows[SelectRow].Cells[4].Value.ToString() + "'");
            if (Image.Length > 0)
            {
                pictureBox2.Visible = true;
                pictureBox2.Load(Program.gPath + Image[0][0]);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
            Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "접합부열교_이미지", "소분류_이미지2", "대분류 ='" + TBType + "' and 소분류 = '" + TB_dataGridView.Rows[SelectRow].Cells[4].Value.ToString() + "'");
            if (Image.Length > 0)
            {
                pictureBox3.Visible = true;
                pictureBox3.Load(Program.gPath + Image[0][0]);
                pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
        else
        {
            if (TB_dataGridView.Rows[SelectRow].Cells[5].Value != null && TB_dataGridView.Rows[SelectRow].Cells[6].Value != null)
            {
                string[][] value = Program.DB.getValue(DB.type.BaseDB_HCneed, "접합부열교", "명칭", "열교유형 ='" + TBType + "' and 구조체1_단열유형 = '" + TB_dataGridView.Rows[SelectRow].Cells[5].Value.ToString() + "' and 구조체2_단열유형 = '" + TB_dataGridView.Rows[SelectRow].Cells[6].Value.ToString() + "'");
                if (value.Length > 0)
                {
                    string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "접합부열교_이미지", "소분류_이미지1", "대분류 ='" + TBType + "' and 소분류 = '" + value[0][0] + "'");
                    if (Image.Length > 0)
                    {
                        pictureBox2.Visible = true;
                        pictureBox2.Load(Program.gPath + Image[0][0]);
                        pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
            }
            else
            {
                MessageBox.Show("사용자 입력값을 전부 입력한 후 선택주세요.");
            }
            pictureBox3.Visible = false;
        }

    }


    private void Import_Image(int nRow)
    {
        string UserDB_Image;
        MessageBox.Show("시뮬레이션 결과 이미지를 업로드하세요.");
        OpenFileDialog f = new OpenFileDialog();
        f.Filter = "( *.bmp; *.jpg; *.png; *.jpeg) | *.BMP; *.JPG; *.PNG; *.JPEG";
        if (f.ShowDialog() == DialogResult.OK)
        {
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = true;
            pictureBox4.Image = Image.FromFile(f.FileName);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            UserDB_Image = "images/TB/User/" + TB_dataGridView.Rows[nRow].Cells[1].Value + ".jpg";
            pictureBox4.Image.Save(Program.gPath + UserDB_Image, System.Drawing.Imaging.ImageFormat.Png);
        }
    }

    private void AddUserDB_button_Click(object sender, EventArgs e)
    {
        int nRow = TB_dataGridView.Rows.Add();
        Create_UserNum(nRow);
        //Import_Image(nRow);
        TB_dataGridView.Rows[nRow].Cells[2].Value = "사용자DB";
        TB_dataGridView.Rows[nRow].Cells[3].Value = TBType;

        string[][] value = Program.DB.getValue_SameCheck(DB.type.BaseDB_HCneed, "접합부열교", "구조체1_단열유형", "열교유형 ='" + TBType + "'");
        if (value.Length > 0)
        {
            DataGridViewComboBoxCell 구조체1Combo = new DataGridViewComboBoxCell();
            for (int i = 0; i < value.Length; i++)
            {
                구조체1Combo.Items.Add(value[i][0]);
            }
            TB_dataGridView.Rows[nRow].Cells[5] = 구조체1Combo;
        }
        value = Program.DB.getValue_SameCheck(DB.type.BaseDB_HCneed, "접합부열교", "구조체2_단열유형", "열교유형 ='" + TBType + "'");
        if (value.Length > 0)
        {
            DataGridViewComboBoxCell 구조체2Combo = new DataGridViewComboBoxCell();
            for (int i = 0; i < value.Length; i++)
            {
                구조체2Combo.Items.Add(value[i][0]);
            }
            TB_dataGridView.Rows[nRow].Cells[6] = 구조체2Combo;
        }
    }
    private void Create_UserNum(int nRow)
    {
        UserTBNum = Program.UTIL.CreateNum("User_TB", "번호", "UTB");
        double splitnum = Program.UTIL.ToDoubleOrZero(UserTBNum.Substring(3, 2));
        double Count_UserDB = 0;
        for (int i = 0; i < TB_dataGridView.Rows.Count; i++)
        {
            if (TB_dataGridView.Rows[i].Cells[2].Value != null && TB_dataGridView.Rows[i].Cells[2].Value.ToString() == "사용자DB")
            {
                Count_UserDB = Count_UserDB + 1;
            }
        }
        double num = splitnum + Count_UserDB;

        if (num < 10)
        { TB_dataGridView.Rows[nRow].Cells[1].Value = "UTB0" + num.ToString(); }
        else { TB_dataGridView.Rows[nRow].Cells[1].Value = "UTB" + num.ToString(); }
    }
    private void Deletebutton_Click(object sender, EventArgs e)
    {
        if (TB_dataGridView.Rows[SelectRow].Cells[2].Value.ToString() == "사용자DB")
        {
            pictureBox4.Load(Program.gPath + "images/TB/User/.png");
            // System.IO.File.Delete(Program.gPath + "images/TB/User/" + TB_dataGridView.Rows[SelectRow].Cells[1].Value.ToString() + ".jpg");
            Program.DB.deleteValue(DB.type.ProjDB, "User_TB", "번호 ='" + TB_dataGridView.Rows[SelectRow].Cells[1].Value.ToString() + "'");
            TB_dataGridView.Rows.Remove(TB_dataGridView.Rows[SelectRow]);
        }
    }

    private void Save_button_Click(object sender, EventArgs e)
    {
        #region 사용자DB 저장
        string[][] value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
        int check = 0;
        for (int i = 0; i < TB_dataGridView.Rows.Count; i++)
        {
            // 4 5 6 7 칼럼에 아무것도 없으면은 경고문자 뜨는걸로
            if (TB_dataGridView.Rows[i].Cells[4].Value == null || TB_dataGridView.Rows[i].Cells[5].Value == null || TB_dataGridView.Rows[i].Cells[6].Value == null || TB_dataGridView.Rows[i].Cells[7].Value == null)
            {
                MessageBox.Show("모든 값을 쓰세요.");
                check = 1;
                break;
            }
        }
        if (check > 0)
        {
        }
        else
        {
            for (int i = 0; i < TB_dataGridView.Rows.Count; i++)
            {

                if (TB_dataGridView.Rows[i].Cells[2].Value != null && TB_dataGridView.Rows[i].Cells[2].Value.ToString() == "사용자DB")
                {
                    Program.DB.setValue(DB.type.ProjDB, "User_TB", "번호,프로젝트유형,DB타입,유형,명칭,구조체1,구조체1_단열유형,구조체2,구조체2_단열유형,값",

                    "'" + TB_dataGridView.Rows[i].Cells[1].Value + "','"
                    + value[0][0] + "','"
                    + TB_dataGridView.Rows[i].Cells[2].Value + "','"
                    + TB_dataGridView.Rows[i].Cells[3].Value + "','"
                    + TB_dataGridView.Rows[i].Cells[4].Value + "','"
                    + StructureType1 + "','"
                    + TB_dataGridView.Rows[i].Cells[5].Value + "','"
                    + StructureType2 + "','"
                    + TB_dataGridView.Rows[i].Cells[6].Value + "','"
                    + TB_dataGridView.Rows[i].Cells[7].Value + "'",
                    "번호");
                    Program.DB.saveProject();
                    MessageBox.Show("저장되었습니다.");
                }

            }
        }
        #endregion

        TBNum = TB_dataGridView.Rows[SelectRow].Cells[1].Value.ToString();

        string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ThermalBridge_3D", "번호", "열교항목 ='" + TBType + "'");
        for (int i = 0; i < Value.Length; i++)
        {
            Program.DB.setValue(DB.type.ProjDB, "ThermalBridge_3D", "번호,선택열교",
             "'" + Value[i][0] + "','" + TBNum + "'",
             "번호");
        }
        Program.DB.saveProject();
        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private void info_Click(object sender, EventArgs e)
    {
        string basePath = Program.gPath + "Manual\\2.subcontents\\9.3D_TB\\2.TB_DB";

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
