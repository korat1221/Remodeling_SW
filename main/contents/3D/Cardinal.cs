using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace main.contents._3D
{
    public partial class Cardinal : Form
    {
        public int rotation = 0;
        public Cardinal()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

            string cardi = "";
            int i = -1, n = 0, max = -1;
            string[][] res = Program.DB.querySQL(DB.type.ProjDB, "SELECT 방위, COUNT(*) FROM ZoneEnvelope_3D WHERE 방위 <> '수평' GROUP BY 방위");

            while (++i < res.Length)
            {
                n = int.Parse(res[i][1]);

                if (n > max)
                {
                    max = n;
                    cardi = res[i][0];
                }
            }

            comboBox1.SelectedItem = cardi;
            comboBox2.SelectedItem = cardi;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rotation = asDirection(comboBox2.SelectedItem.ToString()) - asDirection(comboBox1.SelectedItem.ToString());

            int i = -1;
            string[][] res = Program.DB.querySQL(DB.type.ProjDB, "SELECT ID, 방위 FROM ZoneEnvelope_3D WHERE 방위 <> '수평'");

            while(++i < res.Length)
            {
                Program.DB.executeSQL(DB.type.ProjDB, "UPDATE ZoneEnvelope_3D SET 방위='" + asCardinal(asDirection(res[i][1]) + rotation) + "' WHERE ID=" + res[i][0]);
            }

            MessageBox.Show("주 방위 변경에 따라 모두 일괄 적용하였습니다.");

            DialogResult = DialogResult.OK;
        }

        private int asDirection(string cardi)
        { 
            switch(cardi)
            {
                case "북동":
                    return 45;
                case "북":
                    return 90;
                case "북서":
                    return 135;
                case "서":
                    return 180;
                case "남서":
                    return 225;
                case "남":
                    return 270;
                case "남동":
                    return 315;
                default:
                    return 0;
            }
        }

        private string asCardinal(int direction)
        {
            direction += 360;
            direction %= 360;

            if (direction <= 68 && direction > 23)
            {
                return "북동";
            }
            else if (direction <= 113 && direction > 68)
            {
                return "북";
            }
            else if (direction <= 158 && direction > 113)
            {
                return "북서";
            }
            else if (direction <= 203 && direction > 158)
            {
                return "서";
            }
            else if (direction <= 248 && direction > 203)
            {
                return "남서";
            }
            else if (direction <= 293 && direction > 248)
            {
                return "남";
            }
            else if (direction <= 338 && direction > 293)
            {
                return "남동";
            }
            else
            {
                return "동";
            }
        }
    }
}
