using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace main.contents
{
    public partial class sub3dFRInfo : Form
    {
        String[] Material = new String[10];
        double[] Material_d = new double[10];
        double[] Material_λ = new double[10];
        double[] Material_R = new double[10];
        double OldFloor_R; string OldFloor;
        double Rsi, Rse, Rtot, Area, UW;

        string sid = "";

        public sub3dFRInfo()
        {
            InitializeComponent();
            new StackedHeaderDecorator(Ucalc_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, Ucalc_dataGridView_RowHandle);
        }
        private bool Ucalc_dataGridView_RowHandle(DataGridViewCell cell, int column, int row)
        {
            if (Ucalc_dataGridView.Rows[row].Cells[1].Value != null && Ucalc_dataGridView.Rows[row].Cells[1].Value.ToString() == "기존바닥")
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
            if (row == 0 || row == Ucalc_dataGridView.RowCount - 2 || row == Ucalc_dataGridView.RowCount - 1)
            {
                cell.Style.BackColor = SystemColors.Control;
                return true;
            }
            else return false;
        }
        private void Load_Material_Num()
        {
            for (int k = 0; k < Ucalc_dataGridView.RowCount; k++)
            {
                Ucalc_dataGridView.Rows[k].Cells[0].Value = (k + 1).ToString();
            }
        }

        private void onVisibleChanged(object sender, EventArgs e)
        {
            if (main.MainContents.selID != sid)
            {
                sid = main.MainContents.selID;

                String key = sid.IndexOf("F_Zone") > 0 ? "번호" : "아이디";
                String ID = main.MainContents.selID.Replace("board-", "");
                string[][] value1 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호,면적,번호", key + " = '" + ID + "'");

                if (value1.Length > 0)
                {
                    panel2.Visible = true;
                    //외피에 구조체 지정 안되어 있을 경우
                    Boolean check = (value1[0][0] != "");
                    Ucalc_dataGridView.Visible = check;
                    label7.Visible = check;
                    Base_textBox.Visible = check;
                    label5.Visible = check;
                    DI_textBox.Visible = check;
                    label11.Visible = check;
                    uw_textBox.Visible = check;
                    label1.Visible = check;
                    Name_textBox1.Visible = check;
                    Type_textBox.Visible = check;
                    pictureBox1.Visible = check;
                    pictureBox2.Visible = check;
                    TBType_textBox.Visible = check;


                    try
                    {
                        //데이터그리드뷰 생성
                        Ucalc_dataGridView.Columns.Clear();

                        Ucalc_dataGridView.Columns.Add("A1", "번호");
                        Ucalc_dataGridView.Columns.Add("A2", "구분");
                        Ucalc_dataGridView.Columns.Add("A3", "재료명         ");
                        Ucalc_dataGridView.Columns.Add("A4", "열전도율.[W/m·K]");
                        Ucalc_dataGridView.Columns.Add("A5", "두께.[mm]");
                        Ucalc_dataGridView.Columns.Add("A6", "열저항.[m²·K/W]");

                        Ucalc_dataGridView.Columns[0].Width = 20;
                        Ucalc_dataGridView.Columns[1].Width = 40;
                        Ucalc_dataGridView.Columns[2].Width = 100;
                        Ucalc_dataGridView.Columns[3].Width = 50;
                        Ucalc_dataGridView.Columns[4].Width = 50;
                        Ucalc_dataGridView.Columns[5].Width = 50;



                        String[][] Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionFloor", "번호,명칭,Type,기존바닥,기초설치,U적용방법,직접간접,구조유형,열교유형,열교종류,표면열전달저항기준,선형점형," +
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
                          "열관류율,열교가산치,유효열관류율"
                               , "번호 = '" + value1[0][0] + "'");

                        Ucalc_dataGridView.Rows.Clear();
                        for (int i = 0; i < 10; i++)
                        {
                            Material[i] = Load[0][(2 * i + 22)];
                            Material_d[i] = Convert.ToDouble(Load[0][(2 * i + 23)]);
                        }

                        //표면열전달저항 및 합계

                        Rsi = Convert.ToDouble(Load[0][18]);
                        //Rsi_textBox.Text = string.Format("{0:F2}", Rsi);
                        Rse = Convert.ToDouble(Load[0][17]);
                        //Rse_textBox.Text = string.Format("{0:F2}", Rse);
                        Rtot = Convert.ToDouble(Load[0][20]);
                        //Material_Rtot_textBox.Text = String.Format("{0:F2}", Rtot);

                        OldFloor = Load[0][3];

                        int nRow1 = Ucalc_dataGridView.Rows.Add();
                        //Ucalc_dataGridView.Rows[nRow1].Cells[1].Value = "실내";
                        Ucalc_dataGridView.Rows[nRow1].Cells[2].Value = "실내표면열전달저항";
                        Ucalc_dataGridView.Rows[nRow1].Cells[5].Value = string.Format("{0:F2}", Rsi);

                        for (int i = 0; i < 10; i++)
                        {
                            if (Material[i] != "")
                            {
                                string[][] Value;
                                string[][] OldFloor_U;
                                int nRow = Ucalc_dataGridView.Rows.Add();
                                Value = Program.DB.getValue(DB.type.ProjDB, "User_Material", "구분,열전도율", "재료명 = '" + Material[i] + "'");
                                if (Value.Length == 0)
                                {
                                    Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "열전도율", "구분,열전도율", "재료명 = '" + Material[i] + "'");
                                }
                                try
                                {
                                    Material_λ[i] = Convert.ToDouble(Value[0][1]);
                                    Material_R[i] = Convert.ToDouble(Load[0][(2 * i + 23)]) / 1000 / Material_λ[i];


                                    Ucalc_dataGridView.Rows[nRow].Cells[1].Value = Value[0][0];
                                    Ucalc_dataGridView.Rows[nRow].Cells[2].Value = Material[i];
                                    Ucalc_dataGridView.Rows[nRow].Cells[3].Value = Value[0][1];
                                    Ucalc_dataGridView.Rows[nRow].Cells[4].Value = Load[0][(2 * i + 23)];
                                    //    Ucalc_dataGridView.Rows[nRow].Cells[5].Style.BackColor = SystemColors.Info;
                                    Ucalc_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F2}", Material_R[i]);
                                }
                                catch { }

                                if (Value.Length == 0)
                                {
                                    OldFloor_U = Program.DB.getValue(DB.type.ProjDB, "ConstructionFloor", "열관류율", "명칭 = '" + Material[i] + "'");
                                    try
                                    {
                                        OldFloor_R = 1 / Convert.ToDouble(OldFloor_U[0][0]);
                                        Ucalc_dataGridView.Rows[nRow].Cells[1].Value = "기존바닥";
                                        Ucalc_dataGridView.Rows[nRow].Cells[2].Value = OldFloor;
                                        string[][] value = Program.DB.getValue(DB.type.ProjDB, "ConstructionFloor", "두께합계", "명칭 ='" + OldFloor + "'");
                                        if (value.Length > 0)
                                        {
                                            Ucalc_dataGridView.Rows[nRow].Cells[4].Value = Convert.ToDouble(value[0][0]).ToString("0");
                                        }
                                        Ucalc_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F2}", OldFloor_R);
                                    }
                                    catch { }
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
                        Ucalc_dataGridView.Rows[nRow3].Cells[4].Value = string.Format("{0:F0}", Convert.ToDouble(Load[0][20]));
                        Ucalc_dataGridView.Rows[nRow3].Cells[5].Value = string.Format("{0:F2}", Rtot);

                        Load_Material_Num();


                        //정보 불러오기

                        Name_textBox1.Text = Load[0][1];
                        DI_textBox.Text = Load[0][6];
                        UW = Convert.ToDouble(Load[0][44]);
                        uw_textBox.Text = String.Format("{0:F2}", UW);
                        if (Load[0][9] == null || Load[0][9] == "")
                        { Type_textBox.Text = ""; }
                        else { Type_textBox.Text = Load[0][7]; }
                        TBType_textBox.Text = Load[0][8];
                        TBType2_textBox.Text = Load[0][9];
                        Base_textBox.Text = Load[0][4];


                        //그림로드
                        string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "바닥유형이미지", "이미지", "바닥유형 = '" + Load[0][2] + "' AND 기초설치 = '" + Load[0][4] + "'");
                        //FloorType_pictureBox.Visible = true;
                        //FloorType_pictureBox.Load(Program.gPath + Image[0][0]);
                        //FloorType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;


                        string[][] Image2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "바닥선형열교이미지", "이미지_구조유형", "열교유형 = '" + Load[0][8] + "'");
                        pictureBox1.Load(Program.gPath + Image2[0][0]);
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                        string[][] Image3 = Program.DB.getValue(DB.type.BaseDB_HCneed, "바닥선형열교이미지", "이미지_고정유형", "제품명 = '" + Load[0][9] + "'  And 열교유형 = '" + Load[0][8] + "'");
                        pictureBox2.Load(Program.gPath + Image3[0][0]);
                        pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    catch { }

                    //정보 불러오기
                    Name_textBox.Text = value1[0][2];
                    Area = Convert.ToDouble(value1[0][1]);
                    Area_textBox.Text = String.Format("{0:F2}", Area);
                }
                else
                {
                    panel2.Visible = false;
                }
            }
        }
    }
}
