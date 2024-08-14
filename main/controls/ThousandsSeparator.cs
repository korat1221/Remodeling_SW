using Eagle._Components.Public;
using main;
using System;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;

namespace main.controls
{
    public class ThousandsSeparator
    {
        int NumberDecimal = 0;
        private readonly TextBox textBox;
        public double text = 0;
        public ThousandsSeparator(TextBox textBox, bool LoadOrNot, int NumberDecimal)
        {
            this.textBox = textBox;
            this.textBox.Font = new Font("Arial", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            this.NumberDecimal = NumberDecimal;

            //Load일 경우 true,아니고 입력일 경우 false
            if (LoadOrNot)
            {
                double value;
                if (double.TryParse(textBox.Text, out value))
                {
                    string code_N = NumberDecimalPlaces(NumberDecimal, value);
                    textBox.Text = value.ToString(code_N);
                }
                else
                    textBox.Text = String.Empty;
            }
            else
            {
                this.textBox.Leave += textBox_Leave;
                double result;
                if (textBox.Text != null && textBox.Text.ToString() != "")
                {
                    if (double.TryParse(textBox.Text, out result) == true)
                    {
                        this.text = Convert.ToDouble(textBox.Text.ToString());
                    }
                    else
                    {
                        MessageBox.Show("숫자를 입력하세요.");
                    }
                }
            }                 
        }
        private string NumberDecimalPlaces(int a, double Value)
        {
            string code = "";
            if(Value <1)
            {
                if (a == 0)
                {
                    code = "0";
                }
                else if (a == 1)
                {
                    code = "0.0";
                }
                else if (a == 2)
                {
                    code = "0.00";
                }
                else
                {
                    code = "0.000";
                }
            }
            else
            {
                if (a == 0)
                {
                    code = "#,##0";
                }
                else if (a == 1)
                {
                    code = "#,#.#";
                }
                else if (a == 2)
                {
                    code = "#,#.##";
                }
                else
                {
                    code = "#,#.###";
                }

            }

            return code;
        }
        private void textBox_Leave(object sender, EventArgs e)
        {
            double value;
            if (double.TryParse(textBox.Text, out value))
            {
                string code_N = NumberDecimalPlaces(NumberDecimal, value);
                textBox.Text = value.ToString(code_N);
            }
            else
                textBox.Text = String.Empty;
        }

    }
}
