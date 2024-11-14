using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static main.DB;
using System.Xml.Linq;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace main.contents.Building
{
    public partial class EnergyUse : Form
    {
        DataGridViewCheckBoxColumn Gas_m3_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn Gas_kWh_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn Elec_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn DH_Mcal_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn DH_kWh_checkBoxColumn = new DataGridViewCheckBoxColumn();
        int Elec_SelectRow; int Gas_SelectRow, Gas_SelectColumn; int DH_SelectRow, DH_SelectColumn;
        public EnergyUse()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            InitializeAsync();

            Elec_StartDay_comboBox.Items.Clear();
            Elec_EndDay_comboBox.Items.Clear();
            Gas_StartDay_comboBox.Items.Clear();
            Gas_EndDay_comboBox.Items.Clear();
            DH_StartDay_comboBox.Items.Clear();
            DH_EndDay_comboBox.Items.Clear();
            for (int i = 1; i < 32; i++)
            {
                Elec_StartDay_comboBox.Items.Add((i).ToString());
                Elec_EndDay_comboBox.Items.Add((i).ToString());
                Gas_StartDay_comboBox.Items.Add((i).ToString());
                Gas_EndDay_comboBox.Items.Add((i).ToString());
                DH_StartDay_comboBox.Items.Add((i).ToString());
                DH_EndDay_comboBox.Items.Add((i).ToString());
            }

            Create_ElecUse_Table();
            Create_GasUse_Table();
            Create_DHUse_Table();
            webView21.Source = new Uri(Program.gPath + "threejs\\public\\chart_ctrl2.html", true);
            webView22.Source = new Uri(Program.gPath + "threejs\\public\\chart_ctrl2.html", true);
            webView23.Source = new Uri(Program.gPath + "threejs\\public\\chart_ctrl2.html", true);
        }
        async void InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);
            await webView22.EnsureCoreWebView2Async(null);
            await webView23.EnsureCoreWebView2Async(null);
        }


        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }
       
        
        

        #region 전기
        private void Create_ElecUse_Table()
        {
            new StackedHeaderDecorator(Elec_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            Elec_dataGridView.Columns.Clear();
            Elec_checkBoxColumn.HeaderText = "선택";
            Elec_checkBoxColumn.Name = "check";
            Elec_dataGridView.Columns.Add(Elec_checkBoxColumn);

            DataGridViewComboBoxColumn 연도Combo = new DataGridViewComboBoxColumn();
            연도Combo.HeaderText = "연도";
            연도Combo.Items.Clear();
            for (int i = 0; i < 100; i++)
            {
                연도Combo.Items.Add((2040 - i).ToString());
            }
            Elec_dataGridView.Columns.Add(연도Combo);
            for (int mth = 1; mth < 13; mth++)
            {
                Elec_dataGridView.Columns.Add("A" + mth.ToString(), "전기사용량." + mth + "월.[kWh]");
            }
        }
        private void Elec_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = Elec_dataGridView.Rows.Add();
        }

        private void Elec_Remove_button_Click(object sender, EventArgs e)
        {
            Elec_dataGridView.Rows.Remove(Elec_dataGridView.Rows[Elec_SelectRow]);
        }
        private void Elec_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Elec_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                Elec_SelectRow = e.RowIndex;
            }
        }
        private void Elec_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Elec_updateGraph();
            }
        }

        private Random random = new Random();
        public string GetRandomGrayColor()
        {
            int grayValue = random.Next(0, 256); // 0에서 255 사이의 랜덤 값
            return $"rgba({grayValue}, {grayValue}, {grayValue}, 1)"; // 회색 색상 생성
        }
        private void Elec_updateGraph()
        {
            string s = "";

            if (Elec_dataGridView.Rows.Count > 0)
            {
                string label = "0000 년";
                for (int a = 0; a < Elec_dataGridView.Rows.Count; a++)
                {
                    string randomGrayColor = GetRandomGrayColor();
                    if (Elec_dataGridView.Rows[a].Cells[1].Value != null)
                    { label = Elec_dataGridView.Rows[a].Cells[1].Value.ToString() + "년"; }
                    string s3 = null;
                    for (int mth = 0; mth < 12; mth++)
                    {
                        s3 += Elec_dataGridView.Rows[a].Cells[mth + 2].Value + ",";
                    }
                    string s2 = "[" + s3 + "]";
                    s += "{label:\"" + label + "\",type:\"line\",data:" + s2 + ",yAxisTitle:\"전기 에너지사용량[kWh]\",pointStyle:\"circle\",pointRadius:\"2.5\",borderWidth:\"0.5\",borderColor:\"" + randomGrayColor + "\",backgroundColor:\"" + randomGrayColor + "\",dash:true,tension: 0.4},";
                }
            }
            if (Elec_dataGridView.Rows.Count > 0)
            {
                string label = "평균";
                double[] average = new double[12];
                for (int mth = 0; mth < 12; mth++)
                {
                    for (int row = 0; row < Elec_dataGridView.Rows.Count; row++)
                    {
                        if (Elec_dataGridView.Rows[row].Cells[mth + 2].Value != null)
                        { average[mth] += Program.UTIL.dataGridView_doubleComa(Elec_dataGridView, row, mth + 2, true, 0); }
                    }
                    average[mth] = average[mth] / Elec_dataGridView.Rows.Count;
                }
                string s3 = null;
                for (int mth = 0; mth < 12; mth++)
                {
                    s3 += average[mth].ToString() + ",";
                }
                string s2 = "[" + s3 + "]";
               s += "{label:\"" + label + "\",type:\"line\",data:" + s2 + ",yAxisTitle:\"전기 에너지사용량[kWh]\",pointStyle:\"rect\",pointRadius:\"3.5\",borderWidth:\"2\",borderColor:\"#ED7D31\",backgroundColor:\"#ED7D31\",dash:false,tension: 0.4},";
            }

            double Elec_max = 0;
            for (int row = 0; row < Elec_dataGridView.Rows.Count; row++)
            {
                for (int column = 2; column < Elec_dataGridView.Columns.Count; column++)
                {
                    if (Elec_dataGridView.Rows[row].Cells[column].Value != null)
                    {
                        if (Elec_max < Program.UTIL.dataGridView_doubleComa(Elec_dataGridView, row, column, true, 0))
                        {
                            Elec_max = Program.UTIL.dataGridView_doubleComa(Elec_dataGridView, row, column, true, 0);
                        }
                        else
                        {
                            Elec_max = Elec_max;
                        }
                    }

                }
            }
            int n = ((int)Elec_max).ToString().Length;
            Elec_max = Convert.ToDouble(String.Format("{0:F0}", Elec_max / Math.Pow(10, n - 1))) * Math.Pow(10, n - 1) + Math.Pow(10, n - 1);
            webView22.CoreWebView2.ExecuteScriptAsync("drawChart_energyuse([" + s + "]," + Elec_max.ToString() + ")");
        }       
        #endregion
        #region 가스
        private void Create_GasUse_Table()
        {
            new StackedHeaderDecorator(Gas_m3_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            Gas_m3_dataGridView.Columns.Clear();
            Gas_m3_checkBoxColumn.HeaderText = "선택";
            Gas_m3_checkBoxColumn.Name = "check";
            Gas_m3_dataGridView.Columns.Add(Gas_m3_checkBoxColumn);

            DataGridViewComboBoxColumn 연도Combo = new DataGridViewComboBoxColumn();
            연도Combo.HeaderText = "연도";
            연도Combo.Items.Clear();
            for (int i = 0; i < 100; i++)
            {
                연도Combo.Items.Add((2040 - i).ToString());
            }
            Gas_m3_dataGridView.Columns.Add(연도Combo);
            for (int mth = 1; mth < 13; mth++)
            {
                Gas_m3_dataGridView.Columns.Add("B" + mth.ToString(), "가스사용량." + mth + "월.[m3]");
            }

            new StackedHeaderDecorator(Gas_kWh_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            Gas_kWh_dataGridView.Columns.Clear();
            Gas_kWh_checkBoxColumn.HeaderText = "선택";
            Gas_kWh_checkBoxColumn.Name = "check";
            Gas_kWh_dataGridView.Columns.Add(Gas_kWh_checkBoxColumn);

            Gas_kWh_dataGridView.Columns.Add("C0", "연도");

            for (int mth = 1; mth < 13; mth++)
            {
                Gas_kWh_dataGridView.Columns.Add("C" + mth.ToString(), mth + "월.[kWh]");
            }
        }
        private void Gas_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = Gas_m3_dataGridView.Rows.Add();
            Gas_kWh_dataGridView.Rows.Add();
        }

        private void Gas_Remove_button_Click(object sender, EventArgs e)
        {
            Gas_m3_dataGridView.Rows.Remove(Gas_m3_dataGridView.Rows[Gas_SelectRow]);
            Gas_kWh_dataGridView.Rows.Remove(Gas_kWh_dataGridView.Rows[Gas_SelectRow]);
        }

        private void Gas_m3_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Gas_m3_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                Gas_SelectRow = e.RowIndex;
            }
        }

        private void Gas_m3_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Gas_SelectColumn = e.ColumnIndex;
                Gas_SelectRow = e.RowIndex;
                Load_Gas_kWh_Value();
                Gas_updateGraph();
            }
        }
        private void Load_Gas_kWh_Value()
        {
            if (Gas_SelectColumn > 1)
            { Gas_kWh_dataGridView.Rows[Gas_SelectRow].Cells[Gas_SelectColumn].Value = string.Format("{0:F0}", Convert.ToDouble(Gas_m3_dataGridView.Rows[Gas_SelectRow].Cells[Gas_SelectColumn].Value.ToString()) * 43.1 * 0.277778); }
            else
            {
                Gas_kWh_dataGridView.Rows[Gas_SelectRow].Cells[Gas_SelectColumn].Value = string.Format("{0:F0}", Convert.ToDouble(Gas_m3_dataGridView.Rows[Gas_SelectRow].Cells[Gas_SelectColumn].Value.ToString()));
            }
        }
        private void Gas_updateGraph()
        {
            string s = "";

            if (Gas_kWh_dataGridView.Rows.Count > 0)
            {
                string label = "0000 년";
                for (int a = 0; a < Gas_kWh_dataGridView.Rows.Count; a++)
                {
                    string randomGrayColor = GetRandomGrayColor();
                    if (Gas_kWh_dataGridView.Rows[a].Cells[1].Value != null)
                    { label = Gas_kWh_dataGridView.Rows[a].Cells[1].Value.ToString() + "년"; }
                    string s3 = null;
                    for (int mth = 0; mth < 12; mth++)
                    {
                        s3 += Gas_kWh_dataGridView.Rows[a].Cells[mth + 2].Value + ",";
                    }
                    string s2 = "[" + s3 + "]";
                    s += "{label:\"" + label + "\",type:\"line\",data:" + s2 + ",yAxisTitle:\"가스 에너지사용량[kWh]\",pointStyle:\"circle\",pointRadius:\"2.5\",borderWidth:\"0.5\",borderColor:\"" + randomGrayColor + "\",backgroundColor:\"" + randomGrayColor + "\",dash:true,tension: 0.4},";
                }
            }
            if (Gas_kWh_dataGridView.Rows.Count > 0)
            {
                string label = "평균";
                double[] average = new double[12];
                for (int mth = 0; mth < 12; mth++)
                {
                    for (int row = 0; row < Gas_kWh_dataGridView.Rows.Count; row++)
                    {
                        if (Gas_kWh_dataGridView.Rows[row].Cells[mth + 2].Value != null)
                        { average[mth] += Program.UTIL.dataGridView_doubleComa(Gas_kWh_dataGridView, row, mth + 2, true, 0); }
                    }
                    average[mth] = average[mth] / Gas_kWh_dataGridView.Rows.Count;
                }
                string s3 = null;
                for (int mth = 0; mth < 12; mth++)
                {
                    s3 += average[mth].ToString() + ",";
                }
                string s2 = "[" + s3 + "]";
                s += "{label:\"" + label + "\",type:\"line\",data:" + s2 + ",yAxisTitle:\"가스 에너지사용량[kWh]\",pointStyle:\"rect\",pointRadius:\"3.5\",borderWidth:\"2\",borderColor:\"#ED7D31\",backgroundColor:\"#ED7D31\",dash:false,tension: 0.4},";
            }

            double Gas_max = 0;
            for (int row = 0; row < Gas_kWh_dataGridView.Rows.Count; row++)
            {
                for (int column = 2; column < Gas_kWh_dataGridView.Columns.Count; column++)
                {
                    if (Gas_kWh_dataGridView.Rows[row].Cells[column].Value != null)
                    {
                        if (Gas_max < Program.UTIL.dataGridView_doubleComa(Gas_kWh_dataGridView, row, column, true, 0))
                        {
                            Gas_max = Program.UTIL.dataGridView_doubleComa(Gas_kWh_dataGridView, row, column, true, 0);
                        }
                        else
                        {
                            Gas_max = Gas_max;
                        }
                    }

                }
            }
            int n = ((int)Gas_max).ToString().Length;
            Gas_max = Convert.ToDouble(String.Format("{0:F0}", Gas_max / Math.Pow(10, n - 1))) * Math.Pow(10, n - 1) + Math.Pow(10, n - 1);
            webView21.CoreWebView2.ExecuteScriptAsync("drawChart_energyuse([" + s + "]," + Gas_max.ToString() + ")");
        }

        #endregion
        #region 지역난방
        private void Create_DHUse_Table()
        {
            new StackedHeaderDecorator(DH_Mcal_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            DH_Mcal_dataGridView.Columns.Clear();
            DH_Mcal_checkBoxColumn.HeaderText = "선택";
            DH_Mcal_checkBoxColumn.Name = "check";
            DH_Mcal_dataGridView.Columns.Add(DH_Mcal_checkBoxColumn);

            DataGridViewComboBoxColumn 연도Combo = new DataGridViewComboBoxColumn();
            연도Combo.HeaderText = "연도";
            연도Combo.Items.Clear();
            for (int i = 0; i < 100; i++)
            {
                연도Combo.Items.Add((2040 - i).ToString());
            }
            DH_Mcal_dataGridView.Columns.Add(연도Combo);
            for (int mth = 1; mth < 13; mth++)
            {
                DH_Mcal_dataGridView.Columns.Add("B" + mth.ToString(), "지역난방사용량." + mth + "월.[Mcal]");
            }

            new StackedHeaderDecorator(DH_kWh_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            DH_kWh_dataGridView.Columns.Clear();
            DH_kWh_checkBoxColumn.HeaderText = "선택";
            DH_kWh_checkBoxColumn.Name = "check";
            DH_kWh_dataGridView.Columns.Add(DH_kWh_checkBoxColumn);

            DH_kWh_dataGridView.Columns.Add("C0", "연도");

            for (int mth = 1; mth < 13; mth++)
            {
                DH_kWh_dataGridView.Columns.Add("C" + mth.ToString(), mth + "월.[kWh]");
            }
        }
        private void DH_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = DH_Mcal_dataGridView.Rows.Add();
            DH_kWh_dataGridView.Rows.Add();
        }

        private void DH_Remove_button_Click(object sender, EventArgs e)
        {
            DH_Mcal_dataGridView.Rows.Remove(DH_Mcal_dataGridView.Rows[DH_SelectRow]);
            DH_kWh_dataGridView.Rows.Remove(DH_kWh_dataGridView.Rows[DH_SelectRow]);
        }

        private void DH_Mcal_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DH_Mcal_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                DH_SelectRow = e.RowIndex;
            }
        }

        private void DH_Mcal_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DH_SelectColumn = e.ColumnIndex;
                DH_SelectRow = e.RowIndex;
                Load_DH_kWh_Value();
                DH_updateGraph();
            }
        }
        private void Load_DH_kWh_Value()
        {
            if (DH_SelectColumn > 1)
            { DH_kWh_dataGridView.Rows[DH_SelectRow].Cells[DH_SelectColumn].Value = string.Format("{0:F0}", Convert.ToDouble(DH_Mcal_dataGridView.Rows[DH_SelectRow].Cells[DH_SelectColumn].Value.ToString()) * 1.1622); }
            else
            {
                DH_kWh_dataGridView.Rows[DH_SelectRow].Cells[DH_SelectColumn].Value = string.Format("{0:F0}", Convert.ToDouble(DH_Mcal_dataGridView.Rows[DH_SelectRow].Cells[DH_SelectColumn].Value.ToString()));
            }
        }

        private void DH_updateGraph()
        {
            string s = "";

            if (DH_kWh_dataGridView.Rows.Count > 0)
            {
                string label = "0000 년";
                for (int a = 0; a < DH_kWh_dataGridView.Rows.Count; a++)
                {
                    string randomGrayColor = GetRandomGrayColor();
                    if (DH_kWh_dataGridView.Rows[a].Cells[1].Value != null)
                    { label = DH_kWh_dataGridView.Rows[a].Cells[1].Value.ToString() + "년"; }
                    string s3 = null;
                    for (int mth = 0; mth < 12; mth++)
                    {
                        s3 += DH_kWh_dataGridView.Rows[a].Cells[mth + 2].Value + ",";
                    }
                    string s2 = "[" + s3 + "]";
                    s += "{label:\"" + label + "\",type:\"line\",data:" + s2 + ",yAxisTitle:\"지역난방 에너지사용량[kWh]\",pointStyle:\"circle\",pointRadius:\"2.5\",borderWidth:\"0.5\",borderColor:\"" + randomGrayColor + "\",backgroundColor:\"" + randomGrayColor + "\",dash:true,tension: 0.4},";
                }
            }
            if (DH_kWh_dataGridView.Rows.Count > 0)
            {
                string label = "평균";
                double[] average = new double[12];
                for (int mth = 0; mth < 12; mth++)
                {
                    for (int row = 0; row < DH_kWh_dataGridView.Rows.Count; row++)
                    {
                        if (DH_kWh_dataGridView.Rows[row].Cells[mth + 2].Value != null)
                        { average[mth] += Program.UTIL.dataGridView_doubleComa(DH_kWh_dataGridView, row, mth + 2, true, 0); }
                    }
                    average[mth] = average[mth] / DH_kWh_dataGridView.Rows.Count;
                }
                string s3 = null;
                for (int mth = 0; mth < 12; mth++)
                {
                    s3 += average[mth].ToString() + ",";
                }
                string s2 = "[" + s3 + "]";
                s += "{label:\"" + label + "\",type:\"line\",data:" + s2 + ",yAxisTitle:\"지역난방 에너지사용량[kWh]\",pointStyle:\"rect\",pointRadius:\"3.5\",borderWidth:\"2\",borderColor:\"#ED7D31\",backgroundColor:\"#ED7D31\",dash:false,tension: 0.4},";
            }

            double DH_max = 0;
            for (int row = 0; row < DH_kWh_dataGridView.Rows.Count; row++)
            {
                for (int column = 2; column < DH_kWh_dataGridView.Columns.Count; column++)
                {
                    if (DH_kWh_dataGridView.Rows[row].Cells[column].Value != null)
                    {
                        if (DH_max < Program.UTIL.dataGridView_doubleComa(DH_kWh_dataGridView, row, column, true, 0))
                        {
                            DH_max = Program.UTIL.dataGridView_doubleComa(DH_kWh_dataGridView, row, column, true, 0);
                        }
                        else
                        {
                            DH_max = DH_max;
                        }
                    }

                }
            }
            int n = ((int)DH_max).ToString().Length;
            DH_max = Convert.ToDouble(String.Format("{0:F0}", DH_max / Math.Pow(10, n - 1))) * Math.Pow(10, n - 1) + Math.Pow(10, n - 1);
            webView23.CoreWebView2.ExecuteScriptAsync("drawChart_energyuse([" + s + "]," + DH_max.ToString() + ")");
        }

        #endregion



        private void Save_button_Click(object sender, EventArgs e)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            for (int n = 0; n < Elec_dataGridView.Rows.Count; n++)
            {
                for (int mth = 1; mth < 13; mth++)
                {
                    double energy = Program.UTIL.dataGridView_doubleComa(Elec_dataGridView, n, mth + 1, true, 0);
                    Program.DB.setValue(DB.type.ProjDB, "BuildingEnergyUse", "프로젝트유형,연료,연도,월,단위,에너지사용량,사용시작일,사용종료일",
                          "'" + 프로젝트유형[0][0] + "','전기','" + Elec_dataGridView.Rows[n].Cells[1].Value + "','" + mth + "월','kWh','" + energy + "','" +
                          Elec_StartDay_comboBox.Text + "','" + Elec_EndDay_comboBox.Text
                          + "'", "연료,연도,월");
                }
            }
            for (int n = 0; n < Gas_m3_dataGridView.Rows.Count; n++)
            {
                for (int mth = 1; mth < 13; mth++)
                {
                    double energy = Program.UTIL.dataGridView_doubleComa(Gas_m3_dataGridView, n, mth + 1, true, 0);
                    Program.DB.setValue(DB.type.ProjDB, "BuildingEnergyUse", "프로젝트유형,연료,연도,월,단위,에너지사용량,사용시작일,사용종료일",
                          "'" + 프로젝트유형[0][0] + "','가스','" + Gas_m3_dataGridView.Rows[n].Cells[1].Value + "','" + mth + "월','m3','" + energy+ "','" +
                          Gas_StartDay_comboBox.Text + "','" + Gas_EndDay_comboBox.Text
                          + "'", "연료,연도,월,단위");
                }
            }
            for (int n = 0; n < Gas_kWh_dataGridView.Rows.Count; n++)
            {
                for (int mth = 1; mth < 13; mth++)
                {
                    double energy = Program.UTIL.dataGridView_doubleComa(Gas_kWh_dataGridView, n, mth + 1, true, 0);
                    Program.DB.setValue(DB.type.ProjDB, "BuildingEnergyUse", "프로젝트유형,연료,연도,월,단위,에너지사용량,사용시작일,사용종료일",
                          "'" + 프로젝트유형[0][0] + "','가스','" + Gas_kWh_dataGridView.Rows[n].Cells[1].Value + "','" + mth + "월','kWh','" + energy + "','" +
                          Gas_StartDay_comboBox.Text + "','" + Gas_EndDay_comboBox.Text
                          + "'", "연료,연도,월,단위");
                }
            }
            for (int n = 0; n < DH_Mcal_dataGridView.Rows.Count; n++)
            {
                for (int mth = 1; mth < 13; mth++)
                {
                    double energy = Program.UTIL.dataGridView_doubleComa(DH_Mcal_dataGridView, n, mth + 1, true, 0);
                    Program.DB.setValue(DB.type.ProjDB, "BuildingEnergyUse", "프로젝트유형,연료,연도,월,단위,에너지사용량,사용시작일,사용종료일",
                          "'" + 프로젝트유형[0][0] + "','지역난방','" + DH_Mcal_dataGridView.Rows[n].Cells[1].Value + "','" + mth + "월','Mcal','" + energy + "','" +
                          DH_StartDay_comboBox.Text + "','" + DH_EndDay_comboBox.Text
                          + "'", "연료,연도,월,단위");
                }
            }
            for (int n = 0; n < DH_kWh_dataGridView.Rows.Count; n++)
            {
                for (int mth = 1; mth < 13; mth++)
                {
                    double energy = Program.UTIL.dataGridView_doubleComa(DH_kWh_dataGridView, n, mth + 1, true, 0);
                    Program.DB.setValue(DB.type.ProjDB, "BuildingEnergyUse", "프로젝트유형,연료,연도,월,단위,에너지사용량,사용시작일,사용종료일",
                          "'" + 프로젝트유형[0][0] + "','지역난방','" + DH_kWh_dataGridView.Rows[n].Cells[1].Value + "','" + mth + "월','kWh','" + energy + "','" +
                          DH_StartDay_comboBox.Text + "','" + DH_EndDay_comboBox.Text
                          + "'", "연료,연도,월,단위");
                }
            }
            MessageBox.Show("저장 되었습니다.");
        }


        private void reset()
        {
            Elec_StartDay_comboBox.SelectedItem = null;
            Gas_StartDay_comboBox.SelectedItem = null;
            DH_StartDay_comboBox.SelectedItem = null;
            Elec_EndDay_comboBox.SelectedItem = null;
            Gas_EndDay_comboBox.SelectedItem = null;
            DH_EndDay_comboBox.SelectedItem = null;
            Elec_dataGridView.Rows.Clear();
            Gas_m3_dataGridView.Rows.Clear();
            Gas_kWh_dataGridView.Rows.Clear();
            DH_Mcal_dataGridView.Rows.Clear();
            DH_kWh_dataGridView.Rows.Clear();
        }

        public void LoadData(String ID)             // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();

            String[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "사용시작일,사용종료일", "연료 = '전기'");
            if (Value.Length > 0)
            {
                Elec_StartDay_comboBox.SelectedItem = Value[0][0];
                Elec_EndDay_comboBox.SelectedItem = Value[0][1];
            }


            Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "BuildingEnergyUse", "연도", "연료 = '전기'");
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    int Row = Elec_dataGridView.Rows.Add();
                    Elec_dataGridView.Rows[Row].Cells[1].Value = Value[n][0];
                    for (int mth = 1; mth < 13; mth++)
                    {
                        string[][] EnergyValue = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "연료 = '전기' And 연도 ='" + Value[n][0] + "' And 월 ='" + mth + "월'");
                        Elec_dataGridView.Rows[Row].Cells[1 + mth].Value = EnergyValue[0][0];
                    }

                }
            }


            Value = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "사용시작일,사용종료일", "연료 = '가스'");
            if (Value.Length > 0)
            {
                Gas_StartDay_comboBox.SelectedItem = Value[0][0];
                Gas_EndDay_comboBox.SelectedItem = Value[0][1];
            }

            Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "BuildingEnergyUse", "연도", "연료 = '가스' and 단위 ='m3'");
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    int Row = Gas_m3_dataGridView.Rows.Add();
                    Gas_kWh_dataGridView.Rows.Add();
                    Gas_m3_dataGridView.Rows[Row].Cells[1].Value = Value[n][0];
                    Gas_kWh_dataGridView.Rows[Row].Cells[1].Value = Value[n][0];
                    for (int mth = 1; mth < 13; mth++)
                    {
                        string[][] EnergyValue = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "연료 = '가스' and 단위 ='m3' And 연도 ='" + Value[n][0] + "' And 월 ='" + mth + "월'");
                        Gas_m3_dataGridView.Rows[Row].Cells[1 + mth].Value = EnergyValue[0][0];
                    }

                }
            }


            Value = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "사용시작일,사용종료일", "연료 = '지역난방'");
            if (Value.Length > 0)
            {
                DH_StartDay_comboBox.SelectedItem = Value[0][0];
                DH_EndDay_comboBox.SelectedItem = Value[0][1];
            }

            Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "BuildingEnergyUse", "연도", "연료 = '지역난방' and 단위 ='Mcal'");
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    int Row = DH_Mcal_dataGridView.Rows.Add();
                    DH_kWh_dataGridView.Rows.Add();
                    DH_Mcal_dataGridView.Rows[Row].Cells[1].Value = Value[n][0];
                    DH_kWh_dataGridView.Rows[Row].Cells[1].Value = Value[n][0];
                    for (int mth = 1; mth < 13; mth++)
                    {
                        string[][] EnergyValue = Program.DB.getValue(DB.type.ProjDB, "BuildingEnergyUse", "에너지사용량", "연료 = '지역난방' and 단위 ='Mcal' And 연도 ='" + Value[n][0] + "' And 월 ='" + mth + "월'");
                        DH_Mcal_dataGridView.Rows[Row].Cells[1 + mth].Value = EnergyValue[0][0];
                    }

                }
            }

        }
    }
}
