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
        int Elec_SelectRow; int Gas_SelectRow, Gas_SelectColumn;
        public EnergyUse()
        {
            InitializeComponent();
            InitializeAsync();

            Elec_StartDay_comboBox.Items.Clear();
            Elec_EndDay_comboBox.Items.Clear();
            Gas_StartDay_comboBox.Items.Clear();
            Gas_EndDay_comboBox.Items.Clear();
            for (int i = 1; i < 32; i++)
            {
                Elec_StartDay_comboBox.Items.Add((i).ToString());
                Elec_EndDay_comboBox.Items.Add((i).ToString());
                Gas_StartDay_comboBox.Items.Add((i).ToString());
                Gas_EndDay_comboBox.Items.Add((i).ToString());
            }

            Create_ElecUse_Table();
            Create_GasUse_Table();
            webView21.Source = new Uri(Program.gPath + "threejs\\public\\chart_ctrl2.html", true);
            webView22.Source = new Uri(Program.gPath + "threejs\\public\\chart_ctrl2.html", true);
        }
        async void InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);
            await webView22.EnsureCoreWebView2Async(null);
        }


        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }
        public void Create_ElecUse_Table()
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
        public void Create_GasUse_Table()
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
        private void Elec_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Elec_updateGraph();
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

            if (checkBox1.Checked && Gas_kWh_dataGridView.Rows.Count > 0)
            {
                if (Gas_kWh_dataGridView.Rows[0].Cells[1].Value != null)
                { textBox1.Text = Gas_kWh_dataGridView.Rows[0].Cells[1].Value.ToString() + "년"; }
                string s3 = null;
                for (int mth = 1; mth < 12; mth++)
                {
                    s3 += Gas_kWh_dataGridView.Rows[0].Cells[mth + 1].Value + ",";
                }
                s3 += Gas_kWh_dataGridView.Rows[0].Cells[13].Value;
                string s2 = "[" + s3 + "]";
                s += "{type:\"line\",data:" + s2 + ",borderColor:\"#5B9BD5\",backgroundColor:\"#5B9BD5\",dash:true,tension: 0.4},";

            }

            if (checkBox2.Checked && Gas_kWh_dataGridView.Rows.Count > 1)
            {
                if (Gas_kWh_dataGridView.Rows[1].Cells[1].Value != null)
                { textBox2.Text = Gas_kWh_dataGridView.Rows[1].Cells[1].Value.ToString() + "년"; }
                string s3 = null;
                for (int mth = 1; mth < 12; mth++)
                {
                    s3 += Gas_kWh_dataGridView.Rows[1].Cells[mth + 1].Value + ",";
                }
                s3 += Gas_kWh_dataGridView.Rows[1].Cells[13].Value;
                string s2 = "[" + s3 + "]";
                s += "{type:\"line\",data:" + s2 + ",borderColor:\"#70AD47\",backgroundColor:\"#70AD47\",dash:true,tension: 0.4},";

            }

            if (checkBox3.Checked && Gas_kWh_dataGridView.Rows.Count > 2)
            {
                if (Gas_kWh_dataGridView.Rows[2].Cells[1].Value != null)
                { textBox3.Text = Gas_kWh_dataGridView.Rows[2].Cells[1].Value.ToString() + "년"; }

                string s3 = null;
                for (int mth = 1; mth < 12; mth++)
                {
                    s3 += Gas_kWh_dataGridView.Rows[2].Cells[mth + 1].Value + ",";
                }
                s3 += Gas_kWh_dataGridView.Rows[2].Cells[13].Value;
                string s2 = "[" + s3 + "]";
                s += "{type:\"line\",data:" + s2 + ",borderColor:\"#4472C4\",backgroundColor:\"#4472C4\",dash:true,tension: 0.4},";

            }

            if (checkBox4.Checked && Gas_kWh_dataGridView.Rows.Count > 0)
            {

                { textBox4.Text = "평균"; }
                double[] average = new double[12];
                for (int mth = 1; mth < 13; mth++)
                {
                    for (int row = 0; row < Gas_kWh_dataGridView.Rows.Count; row++)
                    {
                        if (Gas_kWh_dataGridView.Rows[row].Cells[mth + 1].Value != null)
                        { average[mth - 1] += Convert.ToDouble(Gas_kWh_dataGridView.Rows[row].Cells[mth + 1].Value); }
                    }
                    average[mth - 1] = average[mth - 1] / Gas_kWh_dataGridView.Rows.Count;
                }


                string s3 = null;
                for (int mth = 1; mth < 12; mth++)
                {
                    s3 += average[mth - 1].ToString() + ",";
                }
                s3 += average[11].ToString();
                string s2 = "[" + s3 + "]";
                s += "{type:\"line\",data:" + s2 + ",borderColor:\"#ED7D31\",backgroundColor:\"#ED7D31\",dash:false,tension: 0.4},";

            }

            double Gas_max = 0;
            for (int row = 0; row < Gas_kWh_dataGridView.Rows.Count; row++)
            {
                for (int column = 2; column < Gas_kWh_dataGridView.Columns.Count; column++)
                {
                    if (Gas_kWh_dataGridView.Rows[row].Cells[column].Value != null)
                    {
                        if (Gas_max < Convert.ToDouble(Gas_kWh_dataGridView.Rows[row].Cells[column].Value))
                        {
                            Gas_max = Convert.ToDouble(Gas_kWh_dataGridView.Rows[row].Cells[column].Value);
                        }
                        else
                        {
                            Gas_max = Gas_max;
                        }
                    }

                }
            }

            Gas_max = Convert.ToDouble(String.Format("{0:F0}", Gas_max / 1000)) * 1000 + 1000;
            webView21.CoreWebView2.ExecuteScriptAsync("drawChart2([" + s + "]," + Gas_max.ToString() + ")");

        }

        private void Elec_updateGraph()
        {
            string s = "";

            if (checkBox5.Checked && Elec_dataGridView.Rows.Count > 0)
            {
                if (Elec_dataGridView.Rows[0].Cells[1].Value != null)
                { textBox5.Text = Elec_dataGridView.Rows[0].Cells[1].Value.ToString() + "년"; }

                string s3 = null;
                for (int mth = 1; mth < 12; mth++)
                {
                    s3 += Elec_dataGridView.Rows[0].Cells[mth + 1].Value + ",";
                }
                s3 += Elec_dataGridView.Rows[0].Cells[13].Value;
                string s2 = "[" + s3 + "]";
                s += "{type:\"line\",data:" + s2 + ",borderColor:\"#5B9BD5\",backgroundColor:\"#5B9BD5\",dash:true,tension: 0.4},";

            }

            if (checkBox6.Checked && Elec_dataGridView.Rows.Count > 1)
            {
                if (Elec_dataGridView.Rows[1].Cells[1].Value != null)
                { textBox6.Text = Elec_dataGridView.Rows[1].Cells[1].Value.ToString() + "년"; }
                string s3 = null;
                for (int mth = 1; mth < 12; mth++)
                {
                    s3 += Elec_dataGridView.Rows[1].Cells[mth + 1].Value + ",";
                }
                s3 += Elec_dataGridView.Rows[1].Cells[13].Value;
                string s2 = "[" + s3 + "]";
                s += "{type:\"line\",data:" + s2 + ",borderColor:\"#70AD47\",backgroundColor:\"#70AD47\",dash:true,tension: 0.4},";

            }

            if (checkBox7.Checked && Elec_dataGridView.Rows.Count > 2)
            {
                if (Elec_dataGridView.Rows[2].Cells[1].Value != null)
                { textBox7.Text = Elec_dataGridView.Rows[2].Cells[1].Value.ToString() + "년"; }
                string s3 = null;
                for (int mth = 1; mth < 12; mth++)
                {
                    s3 += Elec_dataGridView.Rows[2].Cells[mth + 1].Value + ",";
                }
                s3 += Elec_dataGridView.Rows[2].Cells[13].Value;
                string s2 = "[" + s3 + "]";
                s += "{type:\"line\",data:" + s2 + ",borderColor:\"#4472C4\",backgroundColor:\"#4472C4\",dash:true,tension: 0.4},";

            }

            if (checkBox8.Checked && Elec_dataGridView.Rows.Count > 0)
            {
                { textBox8.Text = "평균"; }
                double[] average = new double[12];
                for (int mth = 1; mth < 13; mth++)
                {
                    for (int row = 0; row < Elec_dataGridView.Rows.Count; row++)
                    {
                        if (Elec_dataGridView.Rows[row].Cells[mth + 1].Value != null)
                        { average[mth - 1] += Convert.ToDouble(Elec_dataGridView.Rows[row].Cells[mth + 1].Value); }
                    }
                    average[mth - 1] = average[mth - 1] / Elec_dataGridView.Rows.Count;
                }


                string s3 = null;
                for (int mth = 1; mth < 12; mth++)
                {
                    s3 += average[mth - 1].ToString() + ",";
                }
                s3 += average[11].ToString();
                string s2 = "[" + s3 + "]";
                s += "{type:\"line\",data:" + s2 + ",borderColor:\"#ED7D31\",backgroundColor:\"#ED7D31\",dash:false,tension: 0.4},";

            }

            double Elec_max = 0;
            for (int row = 0; row < Elec_dataGridView.Rows.Count; row++)
            {
                for (int column = 2; column < Elec_dataGridView.Columns.Count; column++)
                {
                    if (Elec_dataGridView.Rows[row].Cells[column].Value != null)
                    {
                        if (Elec_max < Convert.ToDouble(Elec_dataGridView.Rows[row].Cells[column].Value))
                        {
                            Elec_max = Convert.ToDouble(Elec_dataGridView.Rows[row].Cells[column].Value);
                        }
                        else
                        {
                            Elec_max = Elec_max;
                        }
                    }

                }
            }

            Elec_max = Convert.ToDouble(String.Format("{0:F0}", Elec_max / 1000)) * 1000 + 1000;
            webView22.CoreWebView2.ExecuteScriptAsync("drawChart2([" + s + "]," + Elec_max.ToString() + ")");

        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Gas_updateGraph();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            Gas_updateGraph();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Gas_updateGraph();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Gas_updateGraph();
        }
        private void checkBox5_CheckedChanged_1(object sender, EventArgs e)
        {
            Elec_updateGraph();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            Elec_updateGraph();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            Elec_updateGraph();
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            Elec_updateGraph();
        }
        private void Save_button_Click(object sender, EventArgs e)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            for (int n = 0; n < Elec_dataGridView.Rows.Count; n++)
            {
                for (int mth = 1; mth < 13; mth++)
                {
                    Program.DB.setValue(DB.type.ProjDB, "BuildingEnergyUse", "프로젝트유형,연료,연도,월,단위,에너지사용량,사용시작일,사용종료일",
                          "'" + 프로젝트유형[0][0] + "','전기','" + Elec_dataGridView.Rows[n].Cells[1].Value + "','" + mth + "월','kWh','" + Elec_dataGridView.Rows[n].Cells[mth + 1].Value + "','" +
                          Elec_StartDay_comboBox.Text + "','" + Elec_EndDay_comboBox.Text
                          + "'", "연료,연도,월");
                }
            }
            for (int n = 0; n < Gas_m3_dataGridView.Rows.Count; n++)
            {
                for (int mth = 1; mth < 13; mth++)
                {
                    Program.DB.setValue(DB.type.ProjDB, "BuildingEnergyUse", "프로젝트유형,연료,연도,월,단위,에너지사용량,사용시작일,사용종료일",
                          "'" + 프로젝트유형[0][0] + "','가스','" + Gas_m3_dataGridView.Rows[n].Cells[1].Value + "','" + mth + "월','m3','" + Gas_m3_dataGridView.Rows[n].Cells[mth + 1].Value + "','" +
                          Gas_StartDay_comboBox.Text + "','" + Gas_EndDay_comboBox.Text
                          + "'", "연료,연도,월,단위");
                }
            }
            for (int n = 0; n < Gas_kWh_dataGridView.Rows.Count; n++)
            {
                for (int mth = 1; mth < 13; mth++)
                {
                    Program.DB.setValue(DB.type.ProjDB, "BuildingEnergyUse", "프로젝트유형,연료,연도,월,단위,에너지사용량,사용시작일,사용종료일",
                          "'" + 프로젝트유형[0][0] + "','가스','" + Gas_kWh_dataGridView.Rows[n].Cells[1].Value + "','" + mth + "월','kWh','" + Gas_kWh_dataGridView.Rows[n].Cells[mth + 1].Value + "','" +
                          Gas_StartDay_comboBox.Text + "','" + Gas_EndDay_comboBox.Text
                          + "'", "연료,연도,월,단위");
                }
            }
            MessageBox.Show("저장 되었습니다.");
        }


        private void reset()
        {
            Elec_StartDay_comboBox.SelectedItem = null;
            Gas_StartDay_comboBox.SelectedItem = null;
            Elec_EndDay_comboBox.SelectedItem = null;
            Gas_EndDay_comboBox.SelectedItem = null;
            Elec_dataGridView.Rows.Clear();
            Gas_m3_dataGridView.Rows.Clear();
            Gas_kWh_dataGridView.Rows.Clear();
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



        }


    }
}
