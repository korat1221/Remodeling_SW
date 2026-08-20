namespace main.subcontents.CoolingSystem
{
    partial class Pipe_Diameter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            Save_button = new Button();
            label3 = new Label();
            label4 = new Label();
            ceNumber_textBox = new TextBox();
            tempDiffer_textBox = new TextBox();
            panel1 = new Panel();
            panel2 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(34, 38);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 0;
            label1.Text = "냉수 온도차";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(34, 65);
            label2.Name = "label2";
            label2.Size = new Size(83, 15);
            label2.TabIndex = 1;
            label2.Text = "공급설비 개수";
            // 
            // Save_button
            // 
            Save_button.Location = new Point(130, 4);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(87, 23);
            Save_button.TabIndex = 4;
            Save_button.Text = "Save";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(240, 38);
            label3.Name = "label3";
            label3.Size = new Size(14, 15);
            label3.TabIndex = 5;
            label3.Text = "K";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(240, 67);
            label4.Name = "label4";
            label4.Size = new Size(19, 15);
            label4.TabIndex = 6;
            label4.Text = "개";
            // 
            // ceNumber_textBox
            // 
            ceNumber_textBox.Location = new Point(118, 62);
            ceNumber_textBox.Name = "ceNumber_textBox";
            ceNumber_textBox.Size = new Size(121, 23);
            ceNumber_textBox.TabIndex = 7;
            ceNumber_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // tempDiffer_textBox
            // 
            tempDiffer_textBox.Location = new Point(118, 34);
            tempDiffer_textBox.Name = "tempDiffer_textBox";
            tempDiffer_textBox.Size = new Size(121, 23);
            tempDiffer_textBox.TabIndex = 8;
            tempDiffer_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(Save_button);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 138);
            panel1.Name = "panel1";
            panel1.Size = new Size(281, 31);
            panel1.TabIndex = 9;
            // 
            // panel2
            // 
            panel2.BackColor = Color.AliceBlue;
            panel2.Controls.Add(tempDiffer_textBox);
            panel2.Controls.Add(ceNumber_textBox);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(281, 169);
            panel2.TabIndex = 10;
            // 
            // Pipe_Diameter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(281, 169);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Name = "Pipe_Diameter";
            Text = "Pipe_Diameter";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button Save_button;
        private Label label3;
        private Label label4;
        private TextBox ceNumber_textBox;
        private TextBox tempDiffer_textBox;
        private Panel panel1;
        private Panel panel2;
    }
}