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
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(47, 34);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 0;
            label1.Text = "냉수 온도차";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(47, 61);
            label2.Name = "label2";
            label2.Size = new Size(83, 15);
            label2.TabIndex = 1;
            label2.Text = "공급설비 개수";
            // 
            // Save_button
            // 
            Save_button.Location = new Point(130, 3);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(121, 23);
            Save_button.TabIndex = 4;
            Save_button.Text = "Save";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(253, 34);
            label3.Name = "label3";
            label3.Size = new Size(14, 15);
            label3.TabIndex = 5;
            label3.Text = "K";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(253, 63);
            label4.Name = "label4";
            label4.Size = new Size(19, 15);
            label4.TabIndex = 6;
            label4.Text = "개";
            // 
            // ceNumber_textBox
            // 
            ceNumber_textBox.Location = new Point(131, 58);
            ceNumber_textBox.Name = "ceNumber_textBox";
            ceNumber_textBox.Size = new Size(121, 23);
            ceNumber_textBox.TabIndex = 7;
            ceNumber_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // tempDiffer_textBox
            // 
            tempDiffer_textBox.Location = new Point(131, 30);
            tempDiffer_textBox.Name = "tempDiffer_textBox";
            tempDiffer_textBox.Size = new Size(121, 23);
            tempDiffer_textBox.TabIndex = 8;
            tempDiffer_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // panel1
            // 
            panel1.BackColor = Color.AliceBlue;
            panel1.Controls.Add(Save_button);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 138);
            panel1.Name = "panel1";
            panel1.Size = new Size(281, 31);
            panel1.TabIndex = 9;
            // 
            // Pipe_Diameter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(281, 169);
            Controls.Add(panel1);
            Controls.Add(tempDiffer_textBox);
            Controls.Add(ceNumber_textBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Pipe_Diameter";
            Text = "Pipe_Diameter";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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
    }
}