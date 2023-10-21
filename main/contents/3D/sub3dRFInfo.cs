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
using static main.DB;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace main.contents
{
    public partial class sub3dRFInfo : Form
    {
        String[] Material = new String[10];
        double[] Material_d = new double[10];
        double[] Material_λ = new double[10];
        double[] Material_R = new double[10];
        double OldRoof_R, OldRoof;
        double Rsi, Rse, Rtot, Area, UW;
        string sid = "";

        public sub3dRFInfo()
        {
            InitializeComponent();
            new StackedHeaderDecorator(Ucalc_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, Ucalc_dataGridView_RowHandle);
        }
        private bool Ucalc_dataGridView_RowHandle(DataGridViewCell cell, int column, int row)
        {
            //if (column == 0 || column == 1 || column == 3 || column == 4)
            //{
            //    cell.Style.BackColor = Color.FromArgb(255, 255, 255);
            //    return true;
            //}
            //int kk = 1;
            //while (++kk < Ucalc_dataGridView.RowCount)
            //{
            //    if (row == kk || row == kk -3)
            //    {
            //        cell.Style.BackColor = Color.FromArgb(255, 255, 255);
            //        return true;
            //    }
            //}

            if (row == 0 || row == Ucalc_dataGridView.RowCount - 2 || row == Ucalc_dataGridView.RowCount - 1)
            {
                cell.Style.BackColor = SystemColors.Control;
                return true;
            }
            else return false;
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
            if (main.MainContents.selID != sid)
            {
                sid = main.MainContents.selID;

                String ID = main.MainContents.selID.Replace("board-", "");
                string[][] value1 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호,면적,번호", "아이디 = '" + ID + "'");

                if (value1.Length > 0)
                {
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
                    pictureBox1.Visible = check;
                    pictureBox2.Visible = check;
                    TBType_textBox.Visible = check;
                    TBType2_textBox.Visible = check;


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



                        String[][] Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "번호,명칭,Type,기존지붕,U적용방법,직접간접,구조유형,열교유형,열교종류,외장재색,표면열전달저항기준,선형점형," +
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

                        int nRow1 = Ucalc_dataGridView.Rows.Add();
                        //Ucalc_dataGridView.Rows[nRow1].Cells[1].Value = "실내";
                        Ucalc_dataGridView.Rows[nRow1].Cells[2].Value = "실내표면열전달저항";
                        Ucalc_dataGridView.Rows[nRow1].Cells[5].Value = string.Format("{0:F2}", Rsi);

                        for (int i = 0; i < 10; i++)
                        {
                            if (Material[i] != "")
                            {
                                string[][] Value;
                                string[][] OldRoof_U;
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
                                    //  Ucalc_dataGridView.Rows[nRow].Cells[5].Style.BackColor = SystemColors.Info;
                                    Ucalc_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F2}", Material_R[i]);
                                }
                                catch { }

                                if (Value.Length == 0)
                                {
                                    OldRoof_U = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "열관류율", "명칭 = '" + Material[i] + "'");
                                    try
                                    {
                                        OldRoof_R = 1 / Convert.ToDouble(OldRoof_U[0][0]);
                                        Ucalc_dataGridView.Rows[nRow].Cells[1].Value = "기존지붕";
                                        Ucalc_dataGridView.Rows[nRow].Cells[2].Value = OldRoof;
                                        //  Ucalc_dataGridView.Rows[nRow].Cells[5].Style.BackColor = SystemColors.Window;
                                        Ucalc_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F2}", OldRoof_R);
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
                        DI_textBox.Text = Load[0][5];
                        abs_textBox.Text = Load[0][42];
                        UW = Convert.ToDouble(Load[0][45]);
                        uw_textBox.Text = String.Format("{0:F2}", UW);
                        if (Load[0][8] == null || Load[0][8] == "")
                        { Type_textBox.Text = ""; }
                        else { Type_textBox.Text = Load[0][6]; }
                        TBType_textBox.Text = Load[0][7];
                        TBType2_textBox.Text = Load[0][8];


                        //그림로드
                        string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "지붕유형이미지", "이미지", "지붕유형 = '" + Load[0][2] + "'");
                        //RoofType_pictureBox.Visible = true;
                        //RoofType_pictureBox.Load(Program.gPath + Image[0][0]);
                        //RoofType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;


                        if (Load[0][12] == "점형")
                        {
                            string[][] Image2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "지붕점형열교이미지", "이미지열교구조유형", "열교유형 = '" + Load[0][7] + "'");
                            pictureBox1.Visible = true;
                            pictureBox1.Load(Program.gPath + Image2[0][0]);
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;


                            string[][] Image3 = Program.DB.getValue(DB.type.BaseDB_HCneed, "지붕점형열교이미지", "이미지고정유형", "제품명 = '" + Load[0][8] + "' And 열교유형 = '" + Load[0][7] + "'");
                            pictureBox2.Visible = true;
                            pictureBox2.Load(Program.gPath + Image3[0][0]);
                            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                        else
                        {
                            string[][] Image2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "지붕선형열교이미지", "이미지열교구조유형", "열교유형 = '" + Load[0][7] + "'");
                            pictureBox1.Visible = true;
                            pictureBox1.Load(Program.gPath + Image2[0][0]);
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                            string[][] Image3 = Program.DB.getValue(DB.type.BaseDB_HCneed, "지붕선형열교이미지", "이미지고정유형", "제품명 = '" + Load[0][8] + "' And 열교유형 = '" + Load[0][7] + "'");
                            pictureBox2.Visible = true;
                            pictureBox2.Load(Program.gPath + Image3[0][0]);
                            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                        }



                    }
                    catch { }

                    //정보 불러오기
                    Name_textBox.Text = value1[0][2];
                    Area = Convert.ToDouble(value1[0][1]);
                    Area_textBox.Text = String.Format("{0:F2}", Area);

                }
            }
        }
    }

}
