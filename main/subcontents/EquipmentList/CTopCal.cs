using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;


namespace main.subcontents.EquipmentList
{
    public partial class CTopCal : Form
    {
        public double CTPower, CTFluid, InTemp, OutTemp, FanPower, FanConsum;
        public string Fan;
        string TYP;
        public CTopCal(string Typ)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            TYP = Typ; //개방형건식,개방형습식,밀폐형건식,밀폐형습식
            //건식은 0.045kW/kW
            //개방형습식 0.033kW/kW

            CG_comboBox.Items.Clear();
            CG_comboBox.Items.AddRange(new string[] { "1중효용흡수식냉동기", "2중효용흡수식냉동기", "압축식냉동기" });
            Fan_comboBox.Items.Clear();
            Fan_comboBox.Items.AddRange(new string[] { "축류형", "원심형"});
            outtemp.Visible = false;
            intemp.Visible = false;
            ctpower.Visible = false;
            fluid.Visible = false;
        }

        private void CG_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Type = (string)CG_comboBox.SelectedItem;
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_Cooling, "CoolTop", "주소", "이름 = '" + Type + "'");
            if (Image.Length > 0)
            {
                pictureBox2.Size = new System.Drawing.Size(390, 250);
                pictureBox2.Location = new Point(50, 80);
                pictureBox2.Load(Program.gPath + Image[0][0]);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void calButton_Click(object sender, EventArgs e)
        {
            CTPower = 0;
            CTFluid = 0;
            InTemp = 0;
            OutTemp = 0;
            if (Information.IsNumeric(Convert.ToDouble(Power_textBox.Text)) != true)
            {
                MessageBox.Show("냉방출력을 입력해 주세요.");
            }
            else if(CG_comboBox.SelectedIndex == null|| CG_comboBox.Text == "")
            {
                MessageBox.Show("냉동기 종류를 선택해 주세요.");                
            }
            else if(Fan_comboBox.SelectedIndex == null || Fan_comboBox.Text == "")
            {
                MessageBox.Show("팬 종류를 선택해 주세요.");
            }
            else
            {
                Fan = Fan_comboBox.Text;
                string[][] val = Program.DB.getValue(DB.type.BaseDB_Cooling,"CoolTop_Power","전기소비계수", "냉각탑유형 = '" + TYP + "' and 팬유형 = '" + Fan + "'");
                FanConsum = Convert.ToDouble(val[0][0]);
                string Type = (string)CG_comboBox.SelectedItem;
                double Value = Convert.ToDouble(Power_textBox.Text);
                switch (Type)
                {
                    case "1중효용흡수식냉동기":
                        outtemp.Visible = true;
                        intemp.Visible = true;
                        ctpower.Visible = true;
                        fluid.Visible = true;

                        OutTemp = 32;
                        InTemp = 37.5;
                        outtemp.Text = OutTemp.ToString("F1") + " ℃";
                        intemp.Text = InTemp.ToString("F1") + " ℃";
                        CTPower = Value * 2.2;//kW임
                        CTFluid = CTPower * 860 / 5500; //CMH
                        FanPower = CTPower * FanConsum;

                        ctpower.Text = CTPower.ToString("F1") + " kW";
                        fluid.Text = CTFluid.ToString("F1") + " CMH";
                        break;
                    case "2중효용흡수식냉동기":
                        outtemp.Visible = true;
                        intemp.Visible = true;
                        ctpower.Visible = true;
                        fluid.Visible = true;

                        OutTemp = 32;
                        InTemp = 37.5;
                        outtemp.Text = OutTemp.ToString("F1") + " ℃";
                        intemp.Text = InTemp.ToString("F1") + " ℃";
                        CTPower = Value * 1.5;//kW임
                        CTFluid = CTPower * 860 / 5500; //CMH
                        FanPower = CTPower * FanConsum;

                        ctpower.Text = CTPower.ToString("F1") + " kW";
                        fluid.Text = CTFluid.ToString("F1") + " CMH";
                        break;
                    case "압축식냉동기":
                        outtemp.Visible = true;
                        intemp.Visible = true;
                        ctpower.Visible = true;
                        fluid.Visible = true;

                        OutTemp = 32;
                        InTemp = 37;
                        outtemp.Text = OutTemp.ToString("F1") + " ℃";
                        intemp.Text = InTemp.ToString("F1") + " ℃";
                        CTPower = Value * 1.2;//kW임 20%여유율 적용함
                        CTFluid = CTPower * 860 / 5000; //CMH
                        FanPower = CTPower * FanConsum;

                        ctpower.Text = CTPower.ToString("F1") + " kW";
                        fluid.Text = CTFluid.ToString("F1") + " CMH";
                        break;
                    default:
                        break;
                }
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
