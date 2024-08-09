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
        private readonly TextBox textBox;
        public double text = 0;
        public ThousandsSeparator(TextBox textBox, bool LoadOrNot)
        {
            this.textBox = textBox;
            this.textBox.Font = new Font("Arial", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            
            //Load일 경우 true,아니고 입력일 경우 false
            if(LoadOrNot)
            {
                Double value;
                if (Double.TryParse(textBox.Text, out value))
                    textBox.Text = string.Format("{0:#,#.##}", value);
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
        private void textBox_Leave(object sender, EventArgs e)
        {
            Double value;
            if (Double.TryParse(textBox.Text, out value))
                textBox.Text = string.Format("{0:#,#.##}", value);
            else
                textBox.Text = String.Empty;
        }

    }
}
