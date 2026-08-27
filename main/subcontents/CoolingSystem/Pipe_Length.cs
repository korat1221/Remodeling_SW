using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace main.subcontents.CoolingSystem
{
    public partial class Pipe_Length : Form
    {
        double Lv, Ls, La; //주배관, 수직배관, 분기관 길이
        double Lv_pipe, Ls_pipe, La_pipe; //외경
        double Lv_insul, Ls_insul, La_insul; //단열두께
        String PipeIns = "보온재"; double PipeIns_Ramda = 0.035;
        string Num, System;
        double QC_max;


        public Pipe_Length(string num, string system)
        {
            InitializeComponent();
            this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

            string[][] IconImage = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉방설비이미지", "이미지", "항목유형 = '분배설비' And 설비유형 = '배관아이콘'");
            if (IconImage.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + IconImage[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_Cooling, "냉방설비이미지", "이미지", "항목유형 = '분배설비' And 설비유형 = '배관길이'");
            if (Image.Length > 0)
            {
                Pipe_Length_pictureBox.Load(Program.gPath + Image[0][0]);
                Pipe_Length_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

            Reset();

            Num = num; //냉방설비번호
            System = system; //설비유형(냉방,난방,급탕)
           
           
            //글자보여주기
            ps_ComboBox.Visible = false;
            LL_textBox.Visible = false;
            LW_textBox.Visible = false;
            Hf_textBox.Visible = false;
            nf_textBox.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            Cal_button.Visible = false;

            Pipe_Length_ComboBox.Items.AddRange(new object[] { "직접입력", "표준값" });
            ps_ComboBox.Items.AddRange(new object[] { "외벽포함", "건물내부" });

            Load_SaveValue();

        }

        private void Pipe_Length_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Pipe_Length_ComboBox.SelectedItem?.ToString() == "직접입력")
            {
                ps_ComboBox.Visible = false;
                LL_textBox.Visible = false;
                LW_textBox.Visible = false;
                Hf_textBox.Visible = false;
                nf_textBox.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                Cal_button.Visible = false;
                MessageBox.Show("배관 관련 아래 표에 직접 입력하세요");
                Load_Pipe_Table();
            }
            else if (Pipe_Length_ComboBox.SelectedItem?.ToString() == "표준값")
            {
                ps_ComboBox.Visible = true;
                LL_textBox.Visible = true;
                LW_textBox.Visible = true;
                Hf_textBox.Visible = true;
                nf_textBox.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                Cal_button.Visible = true;
            }
        }

        private void Create_Pipe_Table()
        {
            Pipe_dataGridView.Columns.Clear();
            new StackedHeaderDecorator(Pipe_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            Pipe_dataGridView.Columns.Add("A0", "구분"); //주배관, 수직배관, 분기관
            Pipe_dataGridView.Columns.Add("A1", "배관길이.[m]"); //위에서 직접입력 또는 표준값 적용하면 작성되도록 이벤트 발생
        }

        private void Load_Pipe_Table()
        {
            Create_Pipe_Table();

            for (int i = 0; i < 3; i++)
            {
                Pipe_dataGridView.Rows.Add();
                if (i == 0)
                {
                    Pipe_dataGridView.Rows[i].Cells[0].Value = "주배관";

                    if (Pipe_Length_ComboBox.Text == "표준값")
                    {
                        Pipe_dataGridView.Rows[i].Cells[1].Value = string.Format("{0:F2}", Lv);
                    }

                }
                else if (i == 1)
                {
                    Pipe_dataGridView.Rows[i].Cells[0].Value = "수직배관";
                    if (Pipe_Length_ComboBox.Text == "표준값")
                    {
                        Pipe_dataGridView.Rows[i].Cells[1].Value = string.Format("{0:F2}", Ls);
                    }
                }

                else
                {
                    Pipe_dataGridView.Rows[i].Cells[0].Value = "분기관";
                    if (Pipe_Length_ComboBox.Text == "표준값")
                    {
                        Pipe_dataGridView.Rows[i].Cells[1].Value = string.Format("{0:F2}", La);
                    }

                }

                    //Pipe_dataGridView.Rows[i].Cells[3].Value = PipeIns;
                    //Pipe_dataGridView.Rows[i].Cells[4].Value = string.Format("{0:F3}", PipeIns_Ramda);

                    ////2,4,5
                    //if (!double.TryParse(Pipe_dataGridView.Rows[i].Cells[2].Value?.ToString(), out double di)) continue;
                    //if (!double.TryParse(Pipe_dataGridView.Rows[i].Cells[4].Value?.ToString(), out double lamda)) continue;
                    //if (!double.TryParse(Pipe_dataGridView.Rows[i].Cells[5].Value?.ToString(), out double t)) continue;

                    //// mm → m 변환
                    //double d_i = di / 10000;
                    //double d_a = (t * 2 + di) / 1000.0;

                    //// 선형열관류율 계산
                    //double psi = Math.PI /
                    //     (1 / (2 * lamda) * Math.Log(d_a / d_i)
                    //      + 1 / (8 * d_a));

                    //// 결과 저장 (Cells[6]에 출력)
                    //Pipe_dataGridView.Rows[i].Cells[6].Value = psi.ToString("F3");
                
            }
        }

        private void Cal_button_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(LL_textBox.Text, out double ll))
            {
                MessageBox.Show("LL 값에 올바른 숫자를 입력하세요.");
                LL_textBox.Focus();
                return;
            }

            if (!double.TryParse(LW_textBox.Text, out double lw))
            {
                MessageBox.Show("LW 값에 올바른 숫자를 입력하세요.");
                LW_textBox.Focus();
                return;
            }

            if (!double.TryParse(Hf_textBox.Text, out double hf))
            {
                MessageBox.Show("Hf 값에 올바른 숫자를 입력하세요.");
                Hf_textBox.Focus();
                return;
            }
            if (!double.TryParse(nf_textBox.Text, out double nf))
            {
                MessageBox.Show("nf 값에 올바른 숫자를 입력하세요.");
                nf_textBox.Focus();
                return;
            }

            if (ps_ComboBox.Text == "외벽포함")
            {
                Lv = 2 * ll + 0.016525 * ll * lw * lw;
            }
            else if (ps_ComboBox.Text == "건물내부")
            {
                Lv = 2 * ll + 0.0325 * ll * lw + 6;
            }
            else
            {
                MessageBox.Show("PS 설치유형을 선택해 주세요.");
                return;
            }

            Ls = 0.025 * ll * lw * hf * nf;
            La = 0.55 * ll * lw * nf;

            Load_Pipe_Table();
        }
        
        //private void Pipe_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0)
        //    {
        //        try
        //        {
        //            if (e.ColumnIndex == 5)
        //            {
        //                if (double.TryParse(Pipe_dataGridView.Rows[e.RowIndex].Cells[2].Value?.ToString(), out double di) &&
        //                    double.TryParse(Pipe_dataGridView.Rows[e.RowIndex].Cells[4].Value?.ToString(), out double lamda) &&
        //                    double.TryParse(Pipe_dataGridView.Rows[e.RowIndex].Cells[5].Value?.ToString(), out double da))
        //                {
        //                    double _di = di / 1000;
        //                    double _da = 2 * (da / 1000) + _di;
        //                    double term1 = (1.0 / (2.0 * lamda)) * Math.Log(_da / _di);
        //                    double term2 = 1.0 / (8 * _da);
        //                    Pipe_dataGridView.Rows[e.RowIndex].Cells[6].Value = (Math.PI / (term1 + term2)).ToString("0.000");
        //                }
        //                else Pipe_dataGridView.Rows[e.RowIndex].Cells[6].Value = "";
        //            }
        //        }
        //        catch { }

        //    }
        //}
        private bool dataGridView_Check()
        {
            foreach (DataGridViewRow selectrow in Pipe_dataGridView.Rows)
            {
                if (!double.TryParse(selectrow.Cells[1].Value?.ToString(), out double di))
                {
                    MessageBox.Show("배관 길이를 입력해주세요.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            return true;
        }
        private void Save()
        {
            string 배관유형, 배관길이;
            foreach (DataGridViewRow selectrow in Pipe_dataGridView.Rows)
            {
                배관유형 = selectrow.Cells[0].Value.ToString();
                배관길이 = selectrow.Cells[1].Value.ToString();
                              
                Program.DB.setValue(DB.type.ProjDB, "Distribution_Form", "번호,설비유형,배관유형,배관길이",
          "'" + Num + "','" + System + "','" + 배관유형 + "', '" + 배관길이 + "'", "번호,배관유형");
            }
            
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (dataGridView_Check() == false)
            {
                return;
            }
            Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Reset()
        {
            double Lv = 0, Ls= 0, La= 0;
            string Num = null, System=null;
            Pipe_dataGridView.Rows.Clear();
            Pipe_dataGridView.Columns.Clear();
            Pipe_Length_ComboBox.Text = null;
            ps_ComboBox.Text = null;
            LL_textBox.Text = null;
            LW_textBox.Text = null;
            Hf_textBox.Text = null;
            nf_textBox.Text = null;
        }

        private void Load_SaveValue()
        {
            string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "Distribution_Form", "번호,설비유형", "번호 ='"+Num+"'");
            if (User_Value.Length > 0)
            {
                Create_Pipe_Table();
                string 배관유형;
                for(int k = 0; k<3; k++)
                {
                    if (k == 0) 배관유형 = "주배관";
                    else if (k == 1) 배관유형 = "수직배관";
                    else 배관유형 = "분기관";                 
                    Pipe_dataGridView.Rows.Add();
                    string[][] 배관 = Program.DB.getValue(DB.type.ProjDB, "Distribution_Form", "배관유형,배관길이", "번호 ='" + Num + "' And 배관유형='"+배관유형+"'");
                    if(배관.Length > 0)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            Pipe_dataGridView.Rows[k].Cells[i].Value = 배관[0][i].ToString();
                        }
                    }
                }
            }
        }
    }
}
