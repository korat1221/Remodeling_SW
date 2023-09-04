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
    public partial class sub3dWLInfo : Form
    {
        String[] Material = new String[10];
        double[] Material_d = new double[10];
        double[] Material_λ = new double[10];
        double[] Material_R = new double[10];
        double OldWall_R, CW_R;
        string OldWall, CWName;

        public sub3dWLInfo()
        {
            InitializeComponent();
            new StackedHeaderDecorator(Ucalc_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
        }

        private void Load_Material_Num()
        {
            for (int k = 0; k < Ucalc_dataGridView.RowCount; k++)
            {
                Ucalc_dataGridView.Rows[k].Cells[1].Value = (k + 1).ToString();
            }
        }

        private void onVisibleChanged(object sender, EventArgs e)
        {
            String ID = main.MainContents.selID.Replace("board-", "");


            //데이터그리드뷰 생성
            Ucalc_dataGridView.Columns.Clear();

            Ucalc_dataGridView.Columns.Add("A1", "번호");
            Ucalc_dataGridView.Columns.Add("A2", "구분");
            Ucalc_dataGridView.Columns.Add("A3", "재료명         ");
            Ucalc_dataGridView.Columns.Add("A4", "열전도율.[W/m·K]");
            Ucalc_dataGridView.Columns.Add("A5", "두께.[mm]");
            Ucalc_dataGridView.Columns.Add("A6", "열저항.[m²·K/W]");

            Ucalc_dataGridView.Columns[0].Width = 40;
            Ucalc_dataGridView.Columns[1].Width = 70;
            Ucalc_dataGridView.Columns[2].Width = 130;
            Ucalc_dataGridView.Columns[3].Width = 70;
            Ucalc_dataGridView.Columns[4].Width = 70;
            Ucalc_dataGridView.Columns[5].Width = 70;

            //데이터그리드뷰에 행 추가해서 data 불러오기 , 구조체 번호 일치하는거
            //int nRow = Ucalc_dataGridView.Rows.Add(5);

           string[][] value1 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호", "아이디 = '" + ID + "'");

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
                Material_d[i] = Convert.ToDouble(Load[0][(2 * i + 24)]);
            }

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
                        Material_λ[i] = Convert.ToDouble(Value[0][1]);
                        Material_R[i] = Convert.ToDouble(Load[0][(2 * i + 24)]) / 1000 / Material_λ[i];


                        Ucalc_dataGridView.Rows[nRow].Cells[2].Value = Value[0][0];
                        Ucalc_dataGridView.Rows[nRow].Cells[3].Value = Material[i];
                        Ucalc_dataGridView.Rows[nRow].Cells[4].Value = Value[0][1];
                        Ucalc_dataGridView.Rows[nRow].Cells[5].Value = Load[0][(2 * i + 24)];
                        //  Ucalc_dataGridView.Rows[nRow].Cells[5].Style.BackColor = SystemColors.Info;
                        Ucalc_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F2}", Material_R[i]);
                    }
                    catch { }

                    if (Value.Length == 0)
                    {
                        OldWall_U = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "열관류율", "명칭 = '" + Material[i] + "'");
                        try
                        {
                            OldWall_R = 1 / Convert.ToDouble(OldWall_U[0][0]);
                            Ucalc_dataGridView.Rows[nRow].Cells[2].Value = "기존외벽";
                            Ucalc_dataGridView.Rows[nRow].Cells[3].Value = OldWall;
                            // Ucalc_dataGridView.Rows[nRow].Cells[5].Style.BackColor = SystemColors.Window;
                            Ucalc_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F2}", OldWall_R);
                        }
                        catch { }
                        if (OldWall_U.Length == 0)
                        {
                            string[][] CW_U = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "유리부분열관류율", "명칭 = '" + Material[i] + "'");
                            try
                            {
                                CW_R = 1 / Convert.ToDouble(CW_U[0][0]);
                                Ucalc_dataGridView.Rows[nRow].Cells[2].Value = "덧댐커튼월";
                                Ucalc_dataGridView.Rows[nRow].Cells[3].Value = CWName;
                                //     Ucalc_dataGridView.Rows[nRow].Cells[5].Style.BackColor = SystemColors.Window;
                                Ucalc_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F2}", CW_R);
                            }
                            catch { }
                        }
                        else { }
                    }
                    else { }
                }
                else { }
            }

            Load_Material_Num();
        }





        }
    }
