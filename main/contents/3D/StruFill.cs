using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CustomComboBox;

namespace main.contents._3D
{
    public partial class StruFill : Form
    {
        sub3dZoneInfo pform;
        public StruFill(sub3dZoneInfo parent)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

            pform = parent;

            if (parent.comboBox3.IsChecked("커튼월창"))
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "번호,명칭", "");

                if (Value != null)
                {
                    for (int k = 0; k < Value.Length; k++)
                    {
                        comboBox1.Items.Add(Value[k][1]);
                    }
                }
            }

            if (comboBox1.Items.Count <= 0)
            {
                comboBox1.Enabled = false;
            }

            if (parent.comboBox3.IsChecked("외벽"))
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "번호,명칭", "");

                if (Value != null)
                {
                    for (int k = 0; k < Value.Length; k++)
                    {
                        comboBox2.Items.Add(Value[k][1]);
                    }
                }
            }
            if (comboBox2.Items.Count <= 0)
            {
                comboBox2.Enabled = false;
            }
            if (parent.comboBox3.IsChecked("지붕"))
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "번호,명칭", "");

                if (Value != null)
                {
                    for (int k = 0; k < Value.Length; k++)
                    {
                        comboBox3.Items.Add(Value[k][1]);
                    }
                }
            }
            if (comboBox3.Items.Count <= 0)
            {
                comboBox3.Enabled = false;
            }
            if (parent.comboBox3.IsChecked("최하층바닥"))
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionFloor", "번호,명칭", "");

                if (Value != null)
                {
                    for (int k = 0; k < Value.Length; k++)
                    {
                        comboBox4.Items.Add(Value[k][1]);
                    }
                }
            }
            if (comboBox4.Items.Count <= 0)
            {
                comboBox4.Enabled = false;
            }
            if (parent.comboBox3.IsChecked("창호"))
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "번호,명칭", "");

                if (Value != null)
                {
                    for (int k = 0; k < Value.Length; k++)
                    {
                        comboBox5.Items.Add(Value[k][1]);
                    }
                }
            }
            if (comboBox5.Items.Count <= 0)
            {
                comboBox5.Enabled = false;
            }
            if (parent.comboBox3.IsChecked("외부출입문"))
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionDoor", "번호,명칭", ""); //출입문으로 나중에 바꿔야함 

                if (Value != null)
                {
                    for (int k = 0; k < Value.Length; k++)
                    {
                        comboBox6.Items.Add(Value[k][1]);
                    }
                }
            }
            if (comboBox6.Items.Count <= 0)
            {
                comboBox6.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = -1;
            string key, val;
            Dictionary<string,string> dict = new Dictionary<string,string>();

            dict.Add("커튼월창", comboBox1.Text);
            dict.Add("외벽", comboBox2.Text);
            dict.Add("지붕", comboBox3.Text);
            dict.Add("최하층바닥", comboBox4.Text);
            dict.Add("창호", comboBox5.Text);
            dict.Add("외부출입문", comboBox6.Text);
               
            while (++i < pform.dataGridView1.RowCount)
            {
                key = pform.dataGridView1.Rows[i].Cells[4].Value.ToString();
                val = dict.ContainsKey(key) ? dict[key] : "";
                if (val != "")
                {
                    ((DataGridViewComboBoxCell)pform.dataGridView1.Rows[i].Cells[10]).Value = val;
                }
            }

            MessageBox.Show("선택하신 항목들을 모두 일괄 적용하였습니다.");

            DialogResult = DialogResult.OK;
        }
    }
}
