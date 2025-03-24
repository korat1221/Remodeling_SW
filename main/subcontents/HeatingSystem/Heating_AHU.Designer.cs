
namespace main.subcontents.HeatingSystem
{
    partial class Heating_AHU
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            GeneralPanel = new Panel();
            Save_button = new Button();
            AHU_dataGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)AHU_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.AliceBlue;
            GeneralPanel.Location = new Point(0, -2);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(800, 47);
            GeneralPanel.TabIndex = 18;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(627, 442);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 20;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // Zone_dataGridView
            // 
            AHU_dataGridView.AllowUserToAddRows = false;
            AHU_dataGridView.AllowUserToDeleteRows = false;
            AHU_dataGridView.AllowUserToResizeColumns = false;
            AHU_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            AHU_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            AHU_dataGridView.BackgroundColor = SystemColors.Control;
            AHU_dataGridView.BorderStyle = BorderStyle.None;
            AHU_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            AHU_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            AHU_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            AHU_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            AHU_dataGridView.Location = new Point(0, 39);
            AHU_dataGridView.Name = "Zone_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font =  new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            AHU_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            AHU_dataGridView.RowHeadersVisible = false;
            AHU_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font =  new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            AHU_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            AHU_dataGridView.RowTemplate.Height = 25;
            AHU_dataGridView.Size = new Size(800, 397);
            AHU_dataGridView.TabIndex = 19;
            // 
            // Heating_AHU
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(797, 479);
            Controls.Add(AHU_dataGridView);
            Controls.Add(Save_button);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Heating_AHU";
            Text = "HeatingAHU";
            ((System.ComponentModel.ISupportInitialize)AHU_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Button Save_button;
        private DataGridView AHU_dataGridView;
        private TextBox textBox2;
        private TextBox d_ins_textBox;
    }
}