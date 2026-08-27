using main.contents.Alt;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace main.subcontents.Alt
{
    public partial class AltWin : Form
    {
        string WinRemodelingType;
        int SelectRow;
        public string SelectName;

        bool scriptable = false;
        public AltWin(String SelectValue)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            WinRemodelingType_comboBox.Items.Clear();
            WinRemodelingType_comboBox.Items.Add("내부덧댐");
            WinRemodelingType_comboBox.Items.Add("철거 후 신규");
            create_table_DB();
            WinRemodelingType_comboBox.SelectedIndex = 0;
            if (SelectValue == null || SelectValue == "")
            {
                if ((MessageBox.Show("창호 리모델링안을 검토합니다", "창호 리모델링안 검토", MessageBoxButtons.YesNo) == DialogResult.Yes))
                {
                    Cal_Optimal cal = new Cal_Optimal();
                    cal.Calc_Optimal_Win();
                    MessageBox.Show("리모델링안 검토가 완료되었습니다.");
                    Save_WinOptimal();
                    load_table_DB(WinRemodelingType);
                }
            }
            else
            {
                string[][] Value2 = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select 리모델링유형 From 투명최적안 where 구조체='창호' and 최적안='" + SelectValue + "'");
                if (Value2.Length > 0)
                {
                    WinRemodelingType_comboBox.SelectedItem = Value2[0][0];
                    load_table_DB(WinRemodelingType);
                    for (int i = 0; i < Alt_dataGridView.Rows.Count; i++)
                    {
                        Alt_dataGridView.Rows[i].Cells[0].Value = false;
                    }
                    if (Alt_dataGridView.Rows.Count > 0)
                    {
                        for (int i = 0; i < Alt_dataGridView.Rows.Count; i++)
                        {
                            Alt_dataGridView.Rows[i].Cells[0].Value = false;
                        }
                        for (int i = 0; i < Alt_dataGridView.Rows.Count; i++)
                        {
                            if (Alt_dataGridView.Rows[i].Cells[1].Value.ToString() == SelectValue)
                            {
                                Alt_dataGridView.Rows[i].Cells[0].Value = true;
                                Alt_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                                SelectName = SelectValue;
                                if (SelectName != null && SelectName != "")
                                {
                                    Load_Select_Remodling(SelectName);
                                }
                                else { GeneralPanel.Visible = false; }
                            }
                        }
                    }
                }
            }

        }

        #region 최적안 자재 리스트

        private void WinRemodelingType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (WinRemodelingType_comboBox.SelectedItem != null)
            {
                WinRemodelingType = WinRemodelingType_comboBox.SelectedItem.ToString();
                if (WinRemodelingType != null && WinRemodelingType != "")
                {
                    load_table_DB(WinRemodelingType);
                }
            }
        }
        private void create_table_DB()
        {
            new StackedHeaderDecorator(Alt_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Alt_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Alt_dataGridView.Columns.Add(checkBoxColumn);
            Alt_dataGridView.Columns.Add("A1", "리모델링안");
            Alt_dataGridView.Columns.Add("A2", "유효열관류율.[W/m" + Program.UTIL.Subscript(2, true) + "·K]");
            Alt_dataGridView.Columns.Add("A3", "종합 점수");
            Alt_dataGridView.Columns.Add("A4", "점수.에너지절감");
            Alt_dataGridView.Columns.Add("A5", "점수.쾌적성");
            Alt_dataGridView.Columns.Add("A6", "점수.적법성");
            Alt_dataGridView.Columns.Add("A7", "점수.경제성");
            Alt_dataGridView.Columns.Add("A8", "에너지절감률.[%]");
            Alt_dataGridView.Columns.Add("A9", "예상 순공사비.[원]");
            Alt_dataGridView.Columns[0].Width = 40;
            Alt_dataGridView.Columns[2].Width = 50;
            Alt_dataGridView.Columns[3].Width = 60;
            Alt_dataGridView.Columns[4].Width = 50;
            Alt_dataGridView.Columns[5].Width = 50;
            Alt_dataGridView.Columns[6].Width = 50;
            Alt_dataGridView.Columns[7].Width = 50;
            Alt_dataGridView.Columns[8].Width = 70;
            Alt_dataGridView.Columns[9].Width = 110;
        }
        void load_table_DB(string WinRemodelingType)
        {
            Alt_dataGridView.Rows.Clear();
            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select 리모델링안,리모델링값,순공사비,재료비,노무비,경비,에너지절감량,에너지절감률,에너지점수,쾌적성점수,적법성점수,경제성점수,종합점수 From Optimal_PreResult Where 검토유형='창호' ORDER BY 종합점수 DESC");
            if (Value.Length > 0)
            {
                for (int a = 0; a < Value.Length; a++)
                {
                    string[][] Value2 = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select 리모델링유형 From 투명최적안  where 구조체='창호' and 최적안='" + Value[a][0] + "'");
                    if (Value2.Length > 0 && WinRemodelingType == Value2[0][0])
                    {
                        int nRow = Alt_dataGridView.Rows.Add();
                        Alt_dataGridView.Rows[nRow].Cells[1].Value = Value[a][0];
                        Alt_dataGridView.Rows[nRow].Cells[2].Value = Program.UTIL.ToDoubleOrZero(Value[a][1]).ToString("0.00");
                        Alt_dataGridView.Rows[nRow].Cells[3].Value = Program.UTIL.ToDoubleOrZero(Value[a][12]).ToString("0.0") + " 점";
                        Alt_dataGridView.Rows[nRow].Cells[4].Value = Program.UTIL.ToDoubleOrZero(Value[a][8]).ToString("0.0") + " 점";
                        Alt_dataGridView.Rows[nRow].Cells[5].Value = Program.UTIL.ToDoubleOrZero(Value[a][9]).ToString("0.0") + " 점";
                        Alt_dataGridView.Rows[nRow].Cells[6].Value = Program.UTIL.ToDoubleOrZero(Value[a][10]).ToString("0.0") + " 점";
                        Alt_dataGridView.Rows[nRow].Cells[7].Value = Program.UTIL.ToDoubleOrZero(Value[a][11]).ToString("0.0") + " 점";
                        Alt_dataGridView.Rows[nRow].Cells[8].Value = Program.UTIL.ToDoubleOrZero(Value[a][7]).ToString("0.0") + " %";
                        Alt_dataGridView.Rows[nRow].Cells[9].Value = Program.UTIL.ToDoubleOrZero(Value[a][2]).ToString("#,##0"); //직접공사비
                    }
                }
            }
            if (Alt_dataGridView.Rows.Count > 0)
            {
                for (int i = 0; i < Alt_dataGridView.Rows.Count; i++)
                {
                    Alt_dataGridView.Rows[i].Cells[0].Value = false;
                }
                if (Alt_dataGridView.Columns.Count > 1 && Alt_dataGridView.Rows[0].Cells[1].Value != null)
                {
                    Alt_dataGridView.Rows[0].Cells[0].Value = true;
                    Alt_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    SelectRow = 0;
                    SelectName = Alt_dataGridView.Rows[0].Cells[1].Value.ToString();
                    if (SelectName != null && SelectName != "")
                    {
                        Load_Select_Remodling(SelectName);
                    }
                    else { GeneralPanel.Visible = false; }
                }
            }
        }

        private Boolean datagridviewDesign(DataGridViewCell cell, int column, int row)
        {
            if (row % 2 == 1)
            {
                cell.Style.BackColor = SystemColors.InactiveBorder;
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = SystemColors.InactiveBorder;
                cell.Style.SelectionForeColor = Color.Black;
                return true;
            }
            else
            {
                cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                cell.Style.SelectionForeColor = Color.Black;
                return true;
            }
        }
        #endregion

        #region 특정자재 선택
        private void Alt_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Alt_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                for (int i = 0; i < Alt_dataGridView.Rows.Count; i++)
                {
                    if (i != e.RowIndex) { Alt_dataGridView.Rows[i].Cells[0].Value = false; }
                    else { Alt_dataGridView.Rows[i].Cells[0].Value = true; }
                }
                int row = GetSelectedIndex();
                if (row > -1)
                {
                    SelectRow = row;
                    SelectName = Alt_dataGridView.Rows[row].Cells[1].Value.ToString();
                    if (SelectName != null && SelectName != "")
                    {
                        Load_Select_Remodling(SelectName);
                    }
                    else { GeneralPanel.Visible = false; }
                }
            }
        }

        private int GetSelectedIndex()
        {
            for (int k = 0; k < Alt_dataGridView.Rows.Count; k++)
            {
                if (Convert.ToBoolean(Alt_dataGridView.Rows[k].Cells[0].Value) == true)
                {
                    return k;
                }
            }
            return -1;
        }
        private void Load_Select_Remodling(string 리모델링안)
        {
            GeneralPanel.Visible = true;
            string[][] Value = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select 리모델링유형,최적안구분, 프레임 From 투명최적안  where 구조체='창호' and 최적안='" + 리모델링안 + "'");
            if (Value.Length > 0)
            {
                Load_WindowImage(Value[0][0], Value[0][1], Value[0][2]);
                double[] result = Cal_Ueff(리모델링안, Value[0][0]);////유효열관류율, 태양열취득률, 빛투과율, 설치열교가산치
                double Ueff = result[0], g = result[1], tao = result[2], dU = result[3];
                Ueff_textBox.Text = Ueff.ToString();
                Program.UTIL.textBox_doubleComa(Ueff_textBox, true, 3);
                g_textBox.Text = g.ToString();
                Program.UTIL.textBox_doubleComa(g_textBox, true, 3);
                tao_textBox.Text = tao.ToString();
                Program.UTIL.textBox_doubleComa(tao_textBox, true, 3);
                dU_textBox.Text = dU.ToString();
                Program.UTIL.textBox_doubleComa(dU_textBox, true, 3);

                double[] WinValue = LoadData_Win(리모델링안);
                double ug = WinValue[0];
                double Uf_open = WinValue[3], Uf_fix = WinValue[4], Uf_btw = WinValue[5];
                double Psi_g_fix = WinValue[6], Psi_g_open = WinValue[7];
                double Psi_InstallTop = WinValue[8], Psi_InstallSide = WinValue[9], Psi_InstallButtom = WinValue[10];
                string 유리 = "";
                string[][] value = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select 최적안구분,프레임,유리 From 투명최적안 where 최적안='" + 리모델링안 + "' and 구조체='창호'");
                if (value.Length > 0)
                {
                    유리 = value[0][2];

                }
                frame_textBox.Text = 리모델링안;
                glass_textBox.Text = 유리;
                Spacer_textBox.Text = "단열간봉";

            }
        }
        private void Load_WindowImage(string WindowType, string FrameType, string FrameMaterial)
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "창호구조유형이미지", "이미지", "구조유형 = '" + WindowType + "'");
            if (Image.Length > 0)
            {
                WindowType_pictureBox.Visible = true;
                WindowType_pictureBox.Load(Program.gPath + Image[0][0]);
                WindowType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            if (FrameMaterial == "금속_단열바") { FrameMaterial = "금속"; }
            Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "창호프레임이미지", "이미지", "유형1 = '" + FrameType + "' AND 유형2 = '기본형' AND 재료 = '" + FrameMaterial + "'");
            if (Image.Length > 0)
            {
                WindowFrame_pictureBox.Visible = true;
                WindowFrame_pictureBox.Load(Program.gPath + Image[0][0]);
                WindowFrame_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        #endregion

        #region 비용 및 절감량 계산

        private void Save_WinOptimal()
        {

            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            string[][] Value = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select 최적안,리모델링유형 From 투명최적안  where 구조체='창호'");
            if (Value.Length > 0)
            {
                for (int a = 0; a < Value.Length; a++)
                {
                    //ID, 프로젝트번호, 프로젝트유형, 검토유형, 리모델링안, 유효열관류율,순공사비,에너지절감량, 에너지절감률 
                    string 리모델링안 = Value[a][0];
                    double[] result = Cal_Ueff(리모델링안, Value[a][1]);
                    double ueff = result[0];
                    double g = result[1];
                    double tao = result[2];
                    double dU = result[3];
                    double[] cost = Cal_Cost(리모델링안);//직접공사비, 재료비, 노무비, 경비 순 
                    double Saving = Cal_Saving(리모델링안);
                    double SavingPercent = Cal_SavingPercent(리모델링안);

                    double[] point = new double[5];
                    point[0] = Cal_SavingPoint(Saving); //에너지
                    point[1] = Cal_ComfortPoint(리모델링안); //쾌적성
                    point[2] = Cal_RulePoint(ueff);//적법성
                    point[3] = Cal_CostPoint(cost[0]); //경제성
                    point[4] = (point[0] + point[1] + point[2] + point[3]) / 4; //종합

                    Program.DB.setValue(DB.type.ProjDB, "Optimal_PreResult", "프로젝트번호,프로젝트유형,검토유형,리모델링안," +
                   "리모델링값유형,리모델링값,순공사비,재료비,노무비,경비,에너지절감량,에너지절감률," +
                   "에너지점수,쾌적성점수,적법성점수,경제성점수,종합점수",
                   "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','창호','" + 리모델링안 + "','유효열관류율','" + ueff.ToString() + "','" +
                   cost[0].ToString() + "','" + cost[1].ToString() + "','" + cost[2].ToString() + "','" + cost[3].ToString() + "','" +
                   Saving.ToString() + "','" + SavingPercent.ToString() + "','" +
                   point[0].ToString() + "','" + point[1].ToString() + "','" + point[2].ToString() + "','" + point[3].ToString() + "','" + point[4].ToString()
                   + "'", "검토유형,리모델링안");
                }

                Program.DB.deleteValue(DB.type.ProjDB, "Optimal_PreResult", "순공사비='0'");
                string[][] Value2 = Program.DB.querySQL(DB.type.ProjDB, "Select 리모델링안,에너지점수,쾌적성점수,적법성점수,경제성점수 From Optimal_PreResult  where 검토유형='창호'");
                if (Value2.Length > 0)
                {
                    double[] Point1 = new double[Value2.Length]; //에너지
                    double[] Point2 = new double[Value2.Length]; //쾌적성
                    double[] Point3 = new double[Value2.Length]; //적법성
                    double[] Point4 = new double[Value2.Length]; //경제성
                    double[] Point5 = new double[Value2.Length]; //종합
                    for (int a = 0; a < Value2.Length; a++)
                    {
                        Point1[a] = Program.UTIL.ToDoubleOrZero(Value2[a][1]);
                        Point2[a] = Program.UTIL.ToDoubleOrZero(Value2[a][2]);
                        Point3[a] = Program.UTIL.ToDoubleOrZero(Value2[a][3]);
                        Point4[a] = Program.UTIL.ToDoubleOrZero(Value2[a][4]);
                    }
                    double Avg1, Avg2, Avg3, Avg4;
                    Avg1 = Point1.ToArray().Average();
                    Avg2 = Point2.ToArray().Average();
                    Avg3 = Point3.ToArray().Average();
                    Avg4 = Point4.ToArray().Average();

                    for (int a = 0; a < Value2.Length; a++)
                    {
                        Point1[a] = Program.UTIL.ToDoubleOrZero(Value2[a][1]) / Avg1 * 100;
                        Point2[a] = Program.UTIL.ToDoubleOrZero(Value2[a][2]) / Avg2 * 100;
                        Point3[a] = Program.UTIL.ToDoubleOrZero(Value2[a][3]) / Avg3 * 100;
                        Point4[a] = Program.UTIL.ToDoubleOrZero(Value2[a][4]) / Avg4 * 100;
                        Point5[a] = (Point1[a] + Point2[a] + Point3[a] + Point4[a]) / 4; //종합
                        Program.DB.setValue(DB.type.ProjDB, "Optimal_PreResult", "프로젝트번호,프로젝트유형,검토유형,리모델링안," +
                        "에너지점수,쾌적성점수,적법성점수,경제성점수,종합점수",
                        "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','창호','" + Value2[a][0] + "','" +
                         Point1[a].ToString() + "','" + Point2[a].ToString() + "','" + Point3[a].ToString() + "','" + Point4[a].ToString() + "','" + Point5[a].ToString()
                         + "'", "검토유형,리모델링안");
                    }
                }

                
            }
        }
        private double[] Cal_Ueff(string 리모델링안, string WinRemodelingType)
        {
            double[] result = new double[4]; //유효열관류율, 태양열취득률, 빛투과율, 설치열교가산치
            double[] WinValue = LoadData_Win(리모델링안);
            double ug = WinValue[0], g = WinValue[1], tao = WinValue[2];
            double Uf_open = WinValue[3], Uf_fix = WinValue[4], Uf_btw = WinValue[5];
            double Psi_g_fix = WinValue[6], Psi_g_open = WinValue[7];
            double Psi_InstallTop = WinValue[8], Psi_InstallSide = WinValue[9], Psi_InstallButtom = WinValue[10];
            double Area_sum = 0;
            String[][] ZoneWin = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.창호열관류율,b.설치열교가산치,b.창호유효열관류율,b.유리면적비,b.상위창호번호,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체번호 = b.번호");
            if (ZoneWin.Length > 0)
            {
                int i = -1;
                while (++i < ZoneWin.Length)
                {
                    double Uw = 0, dU = 0, Ueff = 0;
                    String[][] ZoneWin_P = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "직접간접,태양열취득률,빛투과율", "번호='" + ZoneWin[i][7] + "'");
                    if (ZoneWin_P.Length > 0)
                    {
                        string[][] size = Program.DB.querySQL(DB.type.ProjDB, "Select  a.창호면적,a.창호너비,a.창호높이,a.고정유리면적,a.개폐유리면적,a.개폐프레임면적,a.고정프레임면적,a.중간프레임면적,a.고정유리둘레길이,a.개폐유리둘레길이,a.유리면적비 FROM SubWindow AS a INNER JOIN ZoneEnvelope_3D AS b ON b.구조체번호 = a.번호 where b.번호 = '" + ZoneWin[i][0] + "'");
                        if (size.Length > 0)
                        {
                            double NewUw = Calc_Uw(size, ug, Uf_open, Uf_fix, Uf_btw, Psi_g_fix, Psi_g_open);
                            double Newg = WinValue[1];
                            double Newtao = WinValue[2];
                            if (WinRemodelingType == "내부덧댐")
                            {
                                double[] v = Calc_AdditionalWindow(NewUw, Program.UTIL.ToDoubleOrZero(ZoneWin[0][3]), Newg, Program.UTIL.ToDoubleOrZero(ZoneWin_P[0][1]), Newtao, Program.UTIL.ToDoubleOrZero(ZoneWin_P[0][2])); //double NewUw, double OldUw, double Newg, double Oldg, double Newtao, double Oldtao
                                Uw = v[0]; g = v[1]; tao = v[2];
                            }
                            else
                            {
                                Uw = NewUw; g = Newg; tao = Newtao;
                            }
                            dU = Calc_dUinst(size, Psi_InstallTop, Psi_InstallButtom, Psi_InstallSide);
                            Ueff = Uw + dU;
                            Area_sum += Program.UTIL.ToDoubleOrZero(ZoneWin[i][1]);
                            result[0] += Ueff * Program.UTIL.ToDoubleOrZero(ZoneWin[i][1]);
                            result[3] += Uw * Program.UTIL.ToDoubleOrZero(ZoneWin[i][1]);
                        }
                    }
                }
            }
            result[0] = result[0] / Area_sum;
            result[1] = g;
            result[2] = tao;
            result[3] = Math.Max(0, result[0] - result[3] / Area_sum);
            return result;
        }
        private double[] LoadData_Win(string 리모델링안)
        {
            double[] WinValue = new double[11];
            double ug = 0, g = 0, tao = 0;
            double Uf_open = 0, Uf_fix = 0, Uf_btw = 0;
            double Psi_g_fix = 0, Psi_g_open = 0;
            double Psi_InstallTop = 0, Psi_InstallSide = 0, Psi_InstallButtom = 0;
            string[][] value = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select 최적안구분,프레임,유리 From 투명최적안 where 최적안='" + 리모델링안 + "' and 구조체='창호'");
            if (value.Length > 0)
            {
                string 프레임재료 = value[0][1]; string 단창이중창 = "단창"; string 유리 = value[0][2];
                if (value[0][0] == "이중창_SL") { 단창이중창 = "이중창"; 유리 = "LE/12R/CL"; }
                if (value[0][1] == "금속_단열바") { 프레임재료 = "금속"; }
                string[][] frameValue = Program.DB.querySQL(DB.type.BaseDB_HCneed, "Select 개폐부프레임열관류율,고정부프레임열관류율,중간바프레임열관류율  From 창호프레임  where 프레임종류='" + value[0][0] + "' and 프레임재료='" + value[0][1] + "' and DB유형='표준'");
                if (frameValue.Length > 0)
                {
                    Uf_open = Program.UTIL.ToDoubleOrZero(frameValue[0][0]); Uf_fix = Program.UTIL.ToDoubleOrZero(frameValue[0][1]); Uf_btw = Program.UTIL.ToDoubleOrZero(frameValue[0][2]);
                }
                string[][] glassValue = Program.DB.querySQL(DB.type.BaseDB_HCneed, "Select 번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율  From 유리  where 제품명='" + 유리 + "'and DB유형='표준'");
                if (glassValue.Length > 0)
                {
                    ug = Program.UTIL.ToDoubleOrZero(glassValue[0][7]); g = Program.UTIL.ToDoubleOrZero(glassValue[0][8]); tao = Program.UTIL.ToDoubleOrZero(glassValue[0][9]);
                }
                if (value[0][0] == "이중창_SL")
                {
                    double[] v = Calc_DoubleGlass(glassValue); ug = v[0]; g = v[1]; tao = v[2];
                }
                string[][] TBValue = Program.DB.querySQL(DB.type.BaseDB_HCneed, "Select 상부설치선형열관류율,측면설치선형열관류율,하부설치선형열관류율  From 창호설치열교  where 구분1='외단열'and 구분2='" + 프레임재료 + "'and 구분3='" + 단창이중창 + "'and 구분4='외부측'");
                if (TBValue.Length > 0)
                {
                    Psi_InstallTop = Program.UTIL.ToDoubleOrZero(TBValue[0][0]); Psi_InstallSide = Program.UTIL.ToDoubleOrZero(TBValue[0][1]); Psi_InstallButtom = Program.UTIL.ToDoubleOrZero(TBValue[0][2]);
                }
                string[][] Spacer = Program.DB.querySQL(DB.type.BaseDB_HCneed, "Select 고정유리_LE_선형열관류율,개폐유리_LE_선형열관류율  From 창호간봉  where 구분1='단열간봉'and 구분2='" + 단창이중창 + "'and 구분3='" + 프레임재료 + "'");
                if (Spacer.Length > 0) { Psi_g_fix = Program.UTIL.ToDoubleOrZero(Spacer[0][0]); Psi_g_open = Program.UTIL.ToDoubleOrZero(Spacer[0][1]); }

                WinValue[0] = ug; WinValue[1] = g; WinValue[2] = tao;
                WinValue[3] = Uf_open; WinValue[4] = Uf_fix; WinValue[5] = Uf_btw;
                WinValue[6] = Psi_g_fix; WinValue[7] = Psi_g_open;
                WinValue[8] = Psi_InstallTop; WinValue[9] = Psi_InstallSide; WinValue[10] = Psi_InstallButtom;
            }
            return WinValue;
        }
        private double Calc_Uw(string[][] Size, double Ug, double Uf_open, double Uf_fix, double Uf_btw, double Psi_g_fix, double Psi_g_open)
        {
            double Area = Program.UTIL.ToDoubleOrZero(Size[0][0]), Width = Program.UTIL.ToDoubleOrZero(Size[0][1]), Height = Program.UTIL.ToDoubleOrZero(Size[0][2]), Ag_fix = Program.UTIL.ToDoubleOrZero(Size[0][3]), Ag_open = Program.UTIL.ToDoubleOrZero(Size[0][4]), Af_open = Program.UTIL.ToDoubleOrZero(Size[0][5]), Af_fix = Program.UTIL.ToDoubleOrZero(Size[0][6]), Af_btw = Program.UTIL.ToDoubleOrZero(Size[0][7]), Lg_fix = Program.UTIL.ToDoubleOrZero(Size[0][8]), Lg_open = Program.UTIL.ToDoubleOrZero(Size[0][0]);
            double Uw = (Ug * (Ag_fix + Ag_open) + (Uf_open * Af_open) + (Uf_fix * Af_fix) + (Uf_btw * Af_btw) + (Psi_g_fix * Lg_fix) + (Psi_g_open * Lg_open)) / Area;
            return Uw;
        }
        private double[] Calc_AdditionalWindow(double NewUw, double OldUw, double Newg, double Oldg, double Newtao, double Oldtao)
        {
            double[] value = new double[3];
            double Uw = 1 / (0.019 + 1 / OldUw + 1 / NewUw); double g = 0, tao = 0;
            String 조합구성 = "LE+LE";
            string[][] f_shgc = Program.DB.getValue(DB.type.BaseDB_HCneed, "이중창보정계수", "계수", "조합구성 = '" + 조합구성 + "' AND 보정유형 = '태양열취득률'");
            string[][] f_τ = Program.DB.getValue(DB.type.BaseDB_HCneed, "이중창보정계수", "계수", "조합구성 = '" + 조합구성 + "' AND 보정유형 = '빛투과율'");
            if (f_shgc.Length > 0)
            { g = Program.UTIL.ToDoubleOrZero(f_shgc[0][0]) * Oldg * Newg; }
            if (f_τ.Length > 0)
            { tao = Program.UTIL.ToDoubleOrZero(f_τ[0][0]) * Oldtao * Newtao; }
            value[0] = Uw; value[1] = g; value[2] = tao;
            return value;
        }
        public double Calc_dUinst(string[][] Size, double Psi_InstallTop, double Psi_InstallButtom, double Psi_InstallSide)
        {
            double Area = Program.UTIL.ToDoubleOrZero(Size[0][0]), Width = Program.UTIL.ToDoubleOrZero(Size[0][1]), Height = Program.UTIL.ToDoubleOrZero(Size[0][2]), Ag_fix = Program.UTIL.ToDoubleOrZero(Size[0][3]), Ag_open = Program.UTIL.ToDoubleOrZero(Size[0][4]), Af_open = Program.UTIL.ToDoubleOrZero(Size[0][5]), Af_fix = Program.UTIL.ToDoubleOrZero(Size[0][6]), Af_btw = Program.UTIL.ToDoubleOrZero(Size[0][7]), Lg_fix = Program.UTIL.ToDoubleOrZero(Size[0][8]), Lg_open = Program.UTIL.ToDoubleOrZero(Size[0][0]);
            double dUinst = ((Psi_InstallTop * Width) + (Psi_InstallButtom * Width) + (Psi_InstallSide * Height * 2)) / Area;
            return dUinst;
        }
        private double[] Calc_DoubleGlass(string[][] GlassValue)
        {
            String LE_CL_V = GlassValue[0][6] + "+" + GlassValue[0][6];
            double[] value = new double[3];// Ug, g, Tao;
            value[0] = 1 / ((1 / Program.UTIL.ToDoubleOrZero(GlassValue[0][7])) - 0.04 + 0.189 - 0.13 + (1 / Program.UTIL.ToDoubleOrZero(GlassValue[0][7])));
            String[][] f_shgc = Program.DB.getValue(DB.type.BaseDB_HCneed, "이중창보정계수", "계수", "조합구성 = '" + LE_CL_V + "' AND 보정유형 = '태양열취득률'");
            String[][] f_τ = Program.DB.getValue(DB.type.BaseDB_HCneed, "이중창보정계수", "계수", "조합구성 = '" + LE_CL_V + "' AND 보정유형 = '빛투과율'");
            if (f_shgc.Length > 0)
            {
                value[1] = Program.UTIL.ToDoubleOrZero(f_shgc[0][0]) * Program.UTIL.ToDoubleOrZero(GlassValue[0][8]) * Program.UTIL.ToDoubleOrZero(GlassValue[0][8]);
            }
            if (f_τ.Length > 0)
            { value[2] = Program.UTIL.ToDoubleOrZero(f_τ[0][0]) * Program.UTIL.ToDoubleOrZero(GlassValue[0][9]) * Program.UTIL.ToDoubleOrZero(GlassValue[0][9]); }
            return value;
        }
        private double[] Cal_Cost(string 리모델링안)
        {
            double[] cost = new double[4];//직접공사비, 재료비, 노무비, 경비 순 
            double Area = 0;
            string[][] ar = Program.DB.querySQL(DB.type.ProjDB, "Select Sum(면적) From ZoneEnvelope_3D  where 외피유형='창호'");
            if (ar.Length > 0 && ar[0][0] != "")
            {
                Area = Program.UTIL.ToDoubleOrZero(ar[0][0]);
            }
            string[][] Value = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select 철거유형,직접공사비,재료비,노무비,경비 from 투명최적안 Where 최적안='" + 리모델링안 + "'");
            if (Value.Length > 0)
            {
                cost[0] = Program.UTIL.ToDoubleOrZero(Value[0][1]) * Area;
                cost[1] = Program.UTIL.ToDoubleOrZero(Value[0][2]) * Area;
                cost[2] = Program.UTIL.ToDoubleOrZero(Value[0][3]) * Area;
                cost[3] = Program.UTIL.ToDoubleOrZero(Value[0][4]) * Area;
            }
            return cost;
        }
        private double Cal_SavingPercent(string 리모델링안)
        {
            double SavingPercent = 0;
            string[][] PreValue = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량,기저에너지 from FinalEnergy_Result Where 연료='전체'and 월='연간'");
            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 from FinalEnergy_Result_Optimal Where 리모델링안='" + 리모델링안 + "' and 검토유형='창호' and 연료='전체'");
            if (Value.Length > 0 && PreValue.Length > 0)
            {
                double pre = Program.UTIL.ToDoubleOrZero(PreValue[0][0]) - Program.UTIL.ToDoubleOrZero(PreValue[0][1]);
                SavingPercent = Math.Max((Program.UTIL.ToDoubleOrZero(PreValue[0][0]) - Program.UTIL.ToDoubleOrZero(Value[0][0])) / pre * 100, 0);
            }
            return SavingPercent;
        }
        private double Cal_Saving(string 리모델링안)
        {
            double Saving = 0;
            string[][] PreValue = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량,기저에너지 from FinalEnergy_Result Where 연료='전체'and 월='연간'");
            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 from FinalEnergy_Result_Optimal Where 리모델링안='" + 리모델링안 + "' and 검토유형='창호' and 연료='전체'");
            if (Value.Length > 0 && PreValue.Length > 0)
            {
                Saving = Math.Max((Program.UTIL.ToDoubleOrZero(PreValue[0][0]) - Program.UTIL.ToDoubleOrZero(Value[0][0])), 0);
            }
            return Saving;
        }
        #endregion

        #region 점수계산


        private double Cal_RulePoint(double Ueff)
        {
            double point = 0;
            double Total_Area = 0, RuleValue = 0;
            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체번호 = b.번호 INNER JOIN ConstructionWindow AS c ON b.상위창호번호 = c.번호");
            if (Value.Length > 0)
            {
                for (int k = 0; k < Value.Length; k++)
                {
                    Total_Area += Program.UTIL.ToDoubleOrZero(Value[k][0]);
                    RuleValue += Program.UTIL.ToDoubleOrZero(Value[k][0]) * Program.UTIL.ToDoubleOrZero(Value[k][1]);
                }
                RuleValue = RuleValue / Total_Area;
                point = (RuleValue / Ueff * 100);
            }
            return point;
        }
        private double Cal_SavingPoint(double Saving_Optimal)
        {
            double point = 0; double RuleSaving = 0;
            string[][] PreValue = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량,기저에너지 from FinalEnergy_Result Where 연료='전체'and 월='연간'");
            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 from FinalEnergy_Result_Rule Where 검토유형='창호' and 연료='전체'");
            if (Value.Length > 0 && PreValue.Length > 0)
            {
                RuleSaving = Math.Max((Program.UTIL.ToDoubleOrZero(PreValue[0][0]) - Program.UTIL.ToDoubleOrZero(Value[0][0])), 0);
                point = (Saving_Optimal / RuleSaving * 100);
            }
            return point;
        }
        private double Cal_ComfortPoint(string 리모델링안)
        {
            double point = 0;
            string[][] Alt = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select 최적안구분 From 투명최적안 Where 최적안='" + 리모델링안 + "'");
            if (Alt.Length > 0)
            {
                if (Alt[0][0] == "단창_T/T") { point = 100; }
                else { point = 70; }
            }
            return point;

        }
        private double Cal_CostPoint(double Cost_Optimal)
        {
            double point = 0;
            double CostAVG = 0;

            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select 순공사비 From Optimal_PreResult Where 검토유형='창호'");
            if (Value.Length > 0)
            {
                for (int a = 0; a < Value.Length; a++)
                {
                    CostAVG += Program.UTIL.ToDoubleOrZero(Value[a][0]);
                }
                CostAVG = CostAVG / Value.Length;
            }
            point = (CostAVG / Cost_Optimal * 100);
            return point;
        }

        #endregion

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (SelectRow > -1)
            {
                SelectName = Alt_dataGridView.Rows[SelectRow].Cells[1].Value.ToString();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("창호 리모델링안을 선택해주세요.");
            }
        }

    }
}

