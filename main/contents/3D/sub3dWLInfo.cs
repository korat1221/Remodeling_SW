using main.info;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static main.DB;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace main.contents
{
    public partial class sub3dWLInfo : Form
    {
        String[] Material = new String[10];
        double[] Material_d = new double[10];
        double[] Material_λ = new double[10];
        double[] Material_R = new double[10];
        double OldWall_R, CW_R;
        string OldWall, CWName;
        double Rsi, Rse, Rtot, Area, UW;

        public sub3dWLInfo()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            new StackedHeaderDecorator(Ucalc_dataGridView, DataGridViewAutoSizeColumnsMode.None, Ucalc_dataGridView_RowHandle);
        }


        private void Load_Material_Num()
        {
            for (int k = 1; k < Ucalc_dataGridView.RowCount - 2; k++)
            {
                Ucalc_dataGridView.Rows[k].Cells[0].Value = k.ToString();
            }
        }

        private void onVisibleChanged(object sender, EventArgs e)
        {
            string[][] value1 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호,면적,번호", "아이디 = '" + main.MainContents.selectInfo[2] + "'");

            if (value1.Length > 0)
            {
                panel2.Visible = true;
                //외피에 구조체 지정 안되어 있을 경우
                Boolean check = (value1[0][0] != "");
                Ucalc_dataGridView.Visible = check;
                label7.Visible = check;
                abs_textBox.Visible = check;
                label5.Visible = check;
                DI_textBox.Visible = check;
                label11.Visible = check;
                uw_textBox.Visible = check;
                label1.Visible = check;
                Name_textBox1.Visible = check;
                Type_textBox.Visible = check;
                Wallinst_pictureBox.Visible = check;
                pictureBox2.Visible = check;
                TBType_textBox.Visible = check;
                TBType2_textBox.Visible = check;

                try
                {  //데이터그리드뷰 생성
                    Ucalc_dataGridView.Columns.Clear();

                    Ucalc_dataGridView.Columns.Add("A0", "번호");
                    Ucalc_dataGridView.Columns.Add("A1", "구분");
                    Ucalc_dataGridView.Columns.Add("A2", "재료명");
                    Ucalc_dataGridView.Columns.Add("A3", "열전도율.[W/m·K]");
                    Ucalc_dataGridView.Columns.Add("A4", "두께.[mm]");
                    string Uni = "[m" + Program.UTIL.Subscript(2, true) + "·K / W]";
                    Ucalc_dataGridView.Columns.Add("A5", "열저항." + Uni);
                    Ucalc_dataGridView.Columns[0].Width = 50;
                    Ucalc_dataGridView.Columns[1].Width = 60;
                    Ucalc_dataGridView.Columns[2].Width = 150;
                    Ucalc_dataGridView.Columns[3].Width = 80;
                    Ucalc_dataGridView.Columns[4].Width = 80;
                    Ucalc_dataGridView.Columns[5].Width = 80;



                    String[][] Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "번호,명칭,Type,기존외벽,덧댐커튼월,U적용방법,직접간접,구조유형,열교유형,열교종류,외장재색,표면열전달저항기준,선형점형," +
                            "A,B,C,PsiKai,단위면적당적용," +
                            "Rse,Rsi,두께합계,열저항합계,단열재두께," +
                            "재료1종류,재료1두께," +
                            "재료2종류,재료2두께," +
                            "재료3종류,재료3두께," +
                            "재료4종류,재료4두께," +
                            "재료5종류,재료5두께," +
                            "재료6종류,재료6두께," +
                            "재료7종류,재료7두께," +
                            "재료8종류,재료8두께," +
                            "재료9종류,재료9두께," +
                            "재료10종류,재료10두께," +
                            "흡수율,열관류율,열교가산치,유효열관류율"
                                    , "번호 = '" + value1[0][0] + "'");


                    Ucalc_dataGridView.Rows.Clear();

                    for (int i = 0; i < 10; i++)
                    {
                        Material[i] = Load[0][(2 * i + 23)];
                        Material_d[i] = Program.UTIL.ToDoubleOrZero(Load[0][(2 * i + 24)]);
                    }

                    //표면열전달저항 및 합계


                    Rsi = Program.UTIL.ToDoubleOrZero(Load[0][19]);
                    //Rsi_textBox.Text = string.Format("{0:F2}", Rsi);
                    Rse = Program.UTIL.ToDoubleOrZero(Load[0][18]);
                    //Rse_textBox.Text = string.Format("{0:F2}", Rse);
                    Rtot = Program.UTIL.ToDoubleOrZero(Load[0][21]);

                    int nRow1 = Ucalc_dataGridView.Rows.Add();
                    //Ucalc_dataGridView.Rows[nRow1].Cells[1].Value = "실내";
                    Ucalc_dataGridView.Rows[nRow1].Cells[2].Value = "실내표면열전달저항";
                    Ucalc_dataGridView.Rows[nRow1].Cells[5].Value = string.Format("{0:F2}", Rsi);


                    OldWall = Load[0][3];
                    CWName = Load[0][4];

                    for (int i = 0; i < 10; i++)
                    {
                        if (Material[i] != "")
                        {
                            string[][] Value;
                            string[][] OldWall_U;
                            int nRow = Ucalc_dataGridView.Rows.Add();
                            Value = Program.DB.getValue(DB.type.ProjDB, "User_Material", "구분,열전도율", "재료명 = '" + Material[i] + "'");
                            if (Value.Length == 0)
                            {
                                Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "열전도율", "구분,열전도율", "재료명 = '" + Material[i] + "'");
                            }
                            try
                            {
                                Material_λ[i] = Program.UTIL.ToDoubleOrZero(Value[0][1]);
                                Material_R[i] = Program.UTIL.ToDoubleOrZero(Load[0][(2 * i + 24)]) / 1000 / Material_λ[i];


                                Ucalc_dataGridView.Rows[nRow].Cells[1].Value = Value[0][0];
                                Ucalc_dataGridView.Rows[nRow].Cells[2].Value = Material[i];
                                Ucalc_dataGridView.Rows[nRow].Cells[3].Value = Value[0][1];
                                Ucalc_dataGridView.Rows[nRow].Cells[4].Value = Load[0][(2 * i + 24)];
                                //  Ucalc_dataGridView.Rows[nRow].Cells[5].Style.BackColor = SystemColors.Info;
                                Ucalc_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F2}", Material_R[i]);
                            }
                            catch { }

                            if (Value.Length == 0)
                            {
                                OldWall_U = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "열관류율", "명칭 = '" + Material[i] + "'");
                                try
                                {
                                    OldWall_R = 1 / Program.UTIL.ToDoubleOrZero(OldWall_U[0][0]);
                                    Ucalc_dataGridView.Rows[nRow].Cells[1].Value = "기존외벽";
                                    Ucalc_dataGridView.Rows[nRow].Cells[2].Value = OldWall;
                                    string[][] value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "두께합계", "명칭 ='" + OldWall + "'");
                                    if (value.Length > 0)
                                    {
                                        Ucalc_dataGridView.Rows[nRow].Cells[4].Value = Program.UTIL.ToDoubleOrZero(value[0][0]).ToString("0");
                                    }
                                    Ucalc_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F2}", OldWall_R);
                                }
                                catch { }
                                if (OldWall_U.Length == 0)
                                {
                                    string[][] CW_U = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "유리부분열관류율", "명칭 = '" + Material[i] + "'");
                                    try
                                    {
                                        CW_R = 1 / Program.UTIL.ToDoubleOrZero(CW_U[0][0]);
                                        Ucalc_dataGridView.Rows[nRow].Cells[1].Value = "덧댐커튼월";
                                        Ucalc_dataGridView.Rows[nRow].Cells[2].Value = CWName;
                                        Ucalc_dataGridView.Rows[nRow].Cells[4].Value = 150;
                                        Ucalc_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F2}", CW_R);
                                    }
                                    catch { }
                                }
                                else { }
                            }
                            else { }
                        }
                        else { }
                    }

                    int nRow2 = Ucalc_dataGridView.Rows.Add();
                    //Ucalc_dataGridView.Rows[nRow2].Cells[1].Value = "실외";
                    Ucalc_dataGridView.Rows[nRow2].Cells[2].Value = "실외표면열전달저항";
                    Ucalc_dataGridView.Rows[nRow2].Cells[5].Value = string.Format("{0:F2}", Rse);

                    int nRow3 = Ucalc_dataGridView.Rows.Add();
                    Ucalc_dataGridView.Rows[nRow3].Cells[2].Value = "합계";
                    Ucalc_dataGridView.Rows[nRow3].Cells[4].Value = string.Format("{0:F0}", Program.UTIL.ToDoubleOrZero(Load[0][20]));
                    Ucalc_dataGridView.Rows[nRow3].Cells[5].Value = string.Format("{0:F2}", Rtot);

                    Load_Material_Num();


                    //정보 불러오기
                    Name_textBox1.Text = Load[0][1];
                    DI_textBox.Text = Load[0][6];
                    abs_textBox.Text = Load[0][43];
                    UW = Program.UTIL.ToDoubleOrZero(Load[0][46]);
                    uw_textBox.Text = String.Format("{0:F2}", UW) + " W/m" + Program.UTIL.Subscript(2, true) + "·K";
                    if (Load[0][9] == null || Load[0][9] == "")
                    { Type_textBox.Text = ""; }
                    else { Type_textBox.Text = Load[0][7]; }
                    TBType_textBox.Text = Load[0][8];
                    TBType2_textBox.Text = Load[0][9];

                    ////그림 불러오기 
                    //string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽유형이미지", "이미지", "외벽유형 = '" + Load[0][2] + "'");
                    //WallType_pictureBox.Visible = true;
                    //WallType_pictureBox.Load(Program.gPath + Image[0][0]);
                    //WallType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;


                    //Material_Rtot_textBox.Text = String.Format("{0:F2}", Rtot);

                    if (Load[0][12] == "점형")
                    {
                        string[][] Image3 = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽점형열교이미지", "이미지_구조유형", "열교유형 = '" + Load[0][8] + "'");
                        Wallinst_pictureBox.Load(Program.gPath + Image3[0][0]);
                        Wallinst_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                        string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽점형열교이미지", "이미지_고정유형", "제품명 = '" + Load[0][9] + "' And 열교유형 = '" + Load[0][8] + "'");
                        pictureBox2.Visible = true;
                        pictureBox2.Load(Program.gPath + Image[0][0]);
                        pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    else
                    {
                        string[][] Image3 = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽선형열교이미지", "이미지_구조유형", "열교유형 = '" + Load[0][8] + "'");
                        Wallinst_pictureBox.Load(Program.gPath + Image3[0][0]);
                        Wallinst_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                        string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽선형열교이미지", "이미지_고정유형", "제품명 = '" + Load[0][9] + "' And 열교유형 = '" + Load[0][8] + "'");
                        pictureBox2.Visible = true;
                        pictureBox2.Load(Program.gPath + Image[0][0]);
                        pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    }

                    abs_textBox.ForeColor = Color.Black;
                    TBType_textBox.ForeColor = Color.Black;
                    Type_textBox.ForeColor = Color.Black;
                    TBType2_textBox.ForeColor = Color.Black;

                }
                catch { }

                //정보 불러오기
                Name_textBox.Text = value1[0][2];
                Area = Program.UTIL.ToDoubleOrZero(value1[0][1]);
                Area_textBox.Text = String.Format("{0:F2}", Area) + " m" + Program.UTIL.Subscript(2, true);
            }
            else
            {
                panel2.Visible = false;
            }
        }
        private bool Ucalc_dataGridView_RowHandle(DataGridViewCell cell, int column, int row)
        {
            if (Ucalc_dataGridView.Rows[row].Cells[1].Value != null && Ucalc_dataGridView.Rows[row].Cells[1].Value.ToString() == "기존외벽")
            {
                if (column == 3)
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.SelectionForeColor = Color.Black;
                    return true;
                }
                else { return false; }
            }
            if (Ucalc_dataGridView.Rows[row].Cells[1].Value != null && Ucalc_dataGridView.Rows[row].Cells[1].Value.ToString() == "덧댐커튼월")
            {
                if (column == 3)
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.SelectionForeColor = Color.Black;
                    return true;
                }
                else { return false; }
            }
            if (column == 5)
            {
                cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                return true;
            }
            if (row == 0 || row == Ucalc_dataGridView.RowCount - 2 || row == Ucalc_dataGridView.RowCount - 1)
            {
                cell.Style.BackColor = SystemColors.Control;
                return true;
            }
            else return false;
        }

        private void info_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\11.3D_Construction\\1.Wall";

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
