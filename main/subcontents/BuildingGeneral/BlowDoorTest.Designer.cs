namespace main.subcontents.BuildingGeneral;

partial class BlowDoorTest
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
        Save_button = new Button();
        Blow_dataGridView = new DataGridView();
        Add_button = new Button();
        Delete_button = new Button();
        label4 = new Label();
        n50_textBox = new TextBox();
        n50_label1 = new Label();
        n50_label2 = new Label();
        ((System.ComponentModel.ISupportInitialize)Blow_dataGridView).BeginInit();
        SuspendLayout();
        // 
        // Save_button
        // 
        Save_button.BackColor = SystemColors.ButtonHighlight;
        Save_button.ForeColor = Color.Black;
        Save_button.Location = new Point(232, 207);
        Save_button.Name = "Save_button";
        Save_button.Size = new Size(135, 25);
        Save_button.TabIndex = 20;
        Save_button.Text = "SAVE";
        Save_button.UseVisualStyleBackColor = true;
        Save_button.Click += Save_button_Click;
        // 
        // Blow_dataGridView
        // 
        Blow_dataGridView.AllowUserToAddRows = false;
        Blow_dataGridView.AllowUserToDeleteRows = false;
        Blow_dataGridView.AllowUserToResizeColumns = false;
        Blow_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        Blow_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        Blow_dataGridView.BackgroundColor = Color.White;
        Blow_dataGridView.BorderStyle = BorderStyle.None;
        Blow_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        Blow_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
        dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
        dataGridViewCellStyle1.SelectionForeColor = Color.Black;
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        Blow_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        Blow_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        Blow_dataGridView.Location = new Point(0, 32);
        Blow_dataGridView.Name = "Blow_dataGridView";
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle2.BackColor = SystemColors.Control;
        dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
        dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
        dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
        Blow_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
        Blow_dataGridView.RowHeadersVisible = false;
        Blow_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
        dataGridViewCellStyle3.ForeColor = Color.Black;
        dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
        dataGridViewCellStyle3.SelectionForeColor = Color.Black;
        Blow_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
        Blow_dataGridView.RowTemplate.Height = 25;
        Blow_dataGridView.Size = new Size(391, 141);
        Blow_dataGridView.TabIndex = 19;
        Blow_dataGridView.CellContentClick += Blow_dataGridView_CellContentClick;
        Blow_dataGridView.CellValueChanged += Blow_dataGridView_CellValueChanged;
        // 
        // Add_button
        // 
        Add_button.BackColor = SystemColors.ControlLight;
        Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
        Add_button.FlatStyle = FlatStyle.System;
        Add_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
        Add_button.Location = new Point(327, 6);
        Add_button.Margin = new Padding(0);
        Add_button.Name = "Add_button";
        Add_button.Size = new Size(23, 23);
        Add_button.TabIndex = 172;
        Add_button.Text = "+";
        Add_button.UseVisualStyleBackColor = false;
        Add_button.Click += Add_button_Click;
        // 
        // Delete_button
        // 
        Delete_button.BackColor = SystemColors.ControlLight;
        Delete_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
        Delete_button.FlatStyle = FlatStyle.System;
        Delete_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
        Delete_button.Location = new Point(354, 6);
        Delete_button.Margin = new Padding(0);
        Delete_button.Name = "Delete_button";
        Delete_button.Size = new Size(23, 23);
        Delete_button.TabIndex = 173;
        Delete_button.Text = "-";
        Delete_button.UseVisualStyleBackColor = false;
        Delete_button.Click += Delete_button_Click;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
        label4.Location = new Point(12, 10);
        label4.Name = "label4";
        label4.Size = new Size(99, 15);
        label4.TabIndex = 174;
        label4.Text = "기밀 테스트 결과";
        // 
        // n50_textBox
        // 
        n50_textBox.BackColor = Color.White;
        n50_textBox.BorderStyle = BorderStyle.None;
        n50_textBox.Font = new System.Drawing.Font("맑은 고딕", 9F,FontStyle.Regular, GraphicsUnit.Point);
        n50_textBox.ForeColor = SystemColors.ControlText;
        n50_textBox.Location = new Point(232, 186);
        n50_textBox.Name = "n50_textBox";
        n50_textBox.Size = new Size(120, 15);
        n50_textBox.TabIndex = 176;
        n50_textBox.TextAlign = HorizontalAlignment.Center;
        // 
        // n50_label1
        // 
        n50_label1.AutoSize = true;
        n50_label1.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
        n50_label1.ForeColor = SystemColors.ControlText;
        n50_label1.Location = new Point(175, 187);
        n50_label1.Name = "n50_label1";
        n50_label1.Size = new Size(56, 15);
        n50_label1.TabIndex = 175;
        n50_label1.Text = "평균 n50";
        // 
        // n50_label2
        // 
        n50_label2.AutoSize = true;
        n50_label2.Font = new System.Drawing.Font("맑은 고딕", 9F,FontStyle.Regular, GraphicsUnit.Point);
        n50_label2.ForeColor = SystemColors.ControlText;
        n50_label2.Location = new Point(352, 185);
        n50_label2.Name = "n50_label2";
        n50_label2.Size = new Size(25, 16);
        n50_label2.TabIndex = 177;
        n50_label2.Text = "1/h";
        // 
        // BlowDoorTest
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(393, 244);
        Controls.Add(n50_label2);
        Controls.Add(n50_textBox);
        Controls.Add(n50_label1);
        Controls.Add(label4);
        Controls.Add(Delete_button);
        Controls.Add(Add_button);
        Controls.Add(Blow_dataGridView);
        Controls.Add(Save_button);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "BlowDoorTest";
        Text = "BlowDoor Test";
        ((System.ComponentModel.ISupportInitialize)Blow_dataGridView).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private Button Save_button;
    private DataGridView Blow_dataGridView;
    private TextBox textBox2;
    private TextBox d_ins_textBox;
    private Button Add_button;
    private Button Delete_button;
    private Label label4;
    private TextBox n50_textBox;
    private Label n50_label1;
    private Label n50_label2;
}