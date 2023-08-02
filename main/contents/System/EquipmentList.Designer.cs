using System.Windows.Forms;

namespace main.contents
{
    partial class EquipmentList
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
            label4 = new Label();
            Icon_pictureBox = new PictureBox();
            tabControl2 = new TabControl();
            Boiler_tabPage = new TabPage();
            Boiler_Save_button = new Button();
            Boiler_dataGridView = new DataGridView();
            Copy_button = new Button();
            Remove_button = new Button();
            Add_button = new Button();
            HP_tabPage = new TabPage();
            AS_tabPage = new TabPage();
            DH_tabPage = new TabPage();
            Solar_tabPage = new TabPage();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            tabControl2.SuspendLayout();
            Boiler_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Boiler_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(label4);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 57);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(70, 22);
            label4.Name = "label4";
            label4.Size = new Size(67, 15);
            label4.TabIndex = 101;
            label4.Text = "장비일람표";
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(14, 4);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 97;
            Icon_pictureBox.TabStop = false;
            // 
            // tabControl2
            // 
            tabControl2.Controls.Add(Boiler_tabPage);
            tabControl2.Controls.Add(HP_tabPage);
            tabControl2.Controls.Add(AS_tabPage);
            tabControl2.Controls.Add(DH_tabPage);
            tabControl2.Controls.Add(Solar_tabPage);
            tabControl2.Location = new Point(12, 75);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new Size(977, 643);
            tabControl2.TabIndex = 145;
            // 
            // Boiler_tabPage
            // 
            Boiler_tabPage.Controls.Add(Boiler_Save_button);
            Boiler_tabPage.Controls.Add(Boiler_dataGridView);
            Boiler_tabPage.Controls.Add(Copy_button);
            Boiler_tabPage.Controls.Add(Remove_button);
            Boiler_tabPage.Controls.Add(Add_button);
            Boiler_tabPage.Location = new Point(4, 24);
            Boiler_tabPage.Name = "Boiler_tabPage";
            Boiler_tabPage.Padding = new Padding(3);
            Boiler_tabPage.Size = new Size(969, 615);
            Boiler_tabPage.TabIndex = 6;
            Boiler_tabPage.Text = "보일러";
            Boiler_tabPage.UseVisualStyleBackColor = true;
            // 
            // Boiler_Save_button
            // 
            Boiler_Save_button.BackColor = SystemColors.ButtonHighlight;
            Boiler_Save_button.ForeColor = Color.Black;
            Boiler_Save_button.Location = new Point(863, 569);
            Boiler_Save_button.Name = "Boiler_Save_button";
            Boiler_Save_button.Size = new Size(88, 25);
            Boiler_Save_button.TabIndex = 102;
            Boiler_Save_button.Text = "SAVE";
            Boiler_Save_button.UseVisualStyleBackColor = true;
            Boiler_Save_button.Click += Boiler_Save_button_Click;
            // 
            // Boiler_dataGridView
            // 
            Boiler_dataGridView.AllowUserToAddRows = false;
            Boiler_dataGridView.AllowUserToDeleteRows = false;
            Boiler_dataGridView.AllowUserToResizeColumns = false;
            Boiler_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Boiler_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Boiler_dataGridView.BackgroundColor = SystemColors.Window;
            Boiler_dataGridView.BorderStyle = BorderStyle.None;
            Boiler_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Boiler_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Boiler_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Boiler_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Boiler_dataGridView.Location = new Point(19, 65);
            Boiler_dataGridView.Name = "Boiler_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Boiler_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Boiler_dataGridView.RowHeadersVisible = false;
            Boiler_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Boiler_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Boiler_dataGridView.RowTemplate.Height = 25;
            Boiler_dataGridView.Size = new Size(932, 467);
            Boiler_dataGridView.TabIndex = 101;
            Boiler_dataGridView.CellContentClick += Boiler_dataGridView_CellContentClick;
            Boiler_dataGridView.CellValueChanged += Boiler_dataGridView_CellValueChanged;
            // 
            // Copy_button
            // 
            Copy_button.BackColor = SystemColors.ControlLight;
            Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Copy_button.FlatStyle = FlatStyle.System;
            Copy_button.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            Copy_button.Location = new Point(904, 21);
            Copy_button.Margin = new Padding(0);
            Copy_button.Name = "Copy_button";
            Copy_button.Size = new Size(47, 23);
            Copy_button.TabIndex = 100;
            Copy_button.Text = "Copy";
            Copy_button.UseVisualStyleBackColor = false;
            // 
            // Remove_button
            // 
            Remove_button.BackColor = SystemColors.ControlLight;
            Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Remove_button.FlatStyle = FlatStyle.System;
            Remove_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Remove_button.Location = new Point(871, 21);
            Remove_button.Margin = new Padding(0);
            Remove_button.Name = "Remove_button";
            Remove_button.Size = new Size(23, 23);
            Remove_button.TabIndex = 99;
            Remove_button.Text = "-";
            Remove_button.UseVisualStyleBackColor = false;
            Remove_button.Click += Boiler_Remove_button_Click;
            // 
            // Add_button
            // 
            Add_button.BackColor = SystemColors.ControlLight;
            Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Add_button.FlatStyle = FlatStyle.System;
            Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Add_button.Location = new Point(838, 21);
            Add_button.Margin = new Padding(0);
            Add_button.Name = "Add_button";
            Add_button.Size = new Size(23, 23);
            Add_button.TabIndex = 98;
            Add_button.Text = "+";
            Add_button.UseVisualStyleBackColor = false;
            Add_button.Click += Boiler_Add_button_Click;
            // 
            // HP_tabPage
            // 
            HP_tabPage.BackColor = Color.White;
            HP_tabPage.Location = new Point(4, 24);
            HP_tabPage.Name = "HP_tabPage";
            HP_tabPage.Padding = new Padding(3);
            HP_tabPage.Size = new Size(969, 615);
            HP_tabPage.TabIndex = 2;
            HP_tabPage.Text = "히트펌프";
            // 
            // AS_tabPage
            // 
            AS_tabPage.Location = new Point(4, 24);
            AS_tabPage.Name = "AS_tabPage";
            AS_tabPage.Padding = new Padding(3);
            AS_tabPage.Size = new Size(969, 615);
            AS_tabPage.TabIndex = 3;
            AS_tabPage.Text = "흡수식온수기";
            AS_tabPage.UseVisualStyleBackColor = true;
            // 
            // DH_tabPage
            // 
            DH_tabPage.Location = new Point(4, 24);
            DH_tabPage.Name = "DH_tabPage";
            DH_tabPage.Padding = new Padding(3);
            DH_tabPage.Size = new Size(969, 615);
            DH_tabPage.TabIndex = 4;
            DH_tabPage.Text = "지역난방";
            DH_tabPage.UseVisualStyleBackColor = true;
            // 
            // Solar_tabPage
            // 
            Solar_tabPage.Location = new Point(4, 24);
            Solar_tabPage.Name = "Solar_tabPage";
            Solar_tabPage.Padding = new Padding(3);
            Solar_tabPage.Size = new Size(969, 615);
            Solar_tabPage.TabIndex = 5;
            Solar_tabPage.Text = "태양열시스템";
            Solar_tabPage.UseVisualStyleBackColor = true;
            // 
            // EquipmentList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(tabControl2);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "EquipmentList";
            Text = "Form3";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            tabControl2.ResumeLayout(false);
            Boiler_tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Boiler_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private TabControl tabControl2;
        private TabPage Boiler_tabPage;
        private TabPage HP_tabPage;
        private TabPage AS_tabPage;
        private TabPage DH_tabPage;
        private TabPage Solar_tabPage;
        private Label label4;
        private PictureBox Icon_pictureBox;
        private Button Copy_button;
        private Button Remove_button;
        private Button Add_button;
        private DataGridView Boiler_dataGridView;
        private Button Boiler_Save_button;
    }
}