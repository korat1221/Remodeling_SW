namespace main.subcontents.ThermalBridge;


partial class TB_DB
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
        TBType_textBox = new TextBox();
        label1 = new Label();
        pictureBox1 = new PictureBox();
        Save_button = new Button();
        panel1 = new Panel();
        pictureBox4 = new PictureBox();
        result_textBox2 = new TextBox();
        TBName_textBox = new TextBox();
        result_textBox = new TextBox();
        pictureBox3 = new PictureBox();
        label2 = new Label();
        pictureBox2 = new PictureBox();
        TB_dataGridView = new DataGridView();
        AddUserDB_button = new Button();
        Deletebutton = new Button();
        label36 = new Label();
        GeneralPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
        ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
        ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
        ((System.ComponentModel.ISupportInitialize)TB_dataGridView).BeginInit();
        SuspendLayout();
        // 
        // GeneralPanel
        // 
        GeneralPanel.BackColor = Color.AliceBlue;
        GeneralPanel.Controls.Add(TBType_textBox);
        GeneralPanel.Controls.Add(label1);
        GeneralPanel.Controls.Add(pictureBox1);
        GeneralPanel.Location = new Point(0, -2);
        GeneralPanel.Name = "GeneralPanel";
        GeneralPanel.Size = new Size(293, 297);
        GeneralPanel.TabIndex = 18;
        // 
        // TBType_textBox
        // 
        TBType_textBox.BackColor = Color.AliceBlue;
        TBType_textBox.BorderStyle = BorderStyle.None;
        TBType_textBox.Enabled = false;
        TBType_textBox.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold);
        TBType_textBox.ForeColor = SystemColors.ControlDark;
        TBType_textBox.Location = new Point(124, 27);
        TBType_textBox.Name = "TBType_textBox";
        TBType_textBox.Size = new Size(120, 16);
        TBType_textBox.TabIndex = 98;
        TBType_textBox.TextAlign = HorizontalAlignment.Center;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold);
        label1.ForeColor = SystemColors.ControlText;
        label1.Location = new Point(24, 27);
        label1.Name = "label1";
        label1.Size = new Size(31, 15);
        label1.TabIndex = 97;
        label1.Text = "유형";
        // 
        // pictureBox1
        // 
        pictureBox1.Location = new Point(11, 59);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(269, 215);
        pictureBox1.TabIndex = 0;
        pictureBox1.TabStop = false;
        // 
        // Save_button
        // 
        Save_button.BackColor = SystemColors.ButtonHighlight;
        Save_button.ForeColor = Color.Black;
        Save_button.Location = new Point(632, 541);
        Save_button.Name = "Save_button";
        Save_button.Size = new Size(135, 25);
        Save_button.TabIndex = 20;
        Save_button.Text = "SAVE";
        Save_button.UseVisualStyleBackColor = true;
        Save_button.Click += Save_button_Click;
        // 
        // panel1
        // 
        panel1.BackColor = SystemColors.GradientInactiveCaption;
        panel1.Controls.Add(result_textBox2);
        panel1.Controls.Add(TBName_textBox);
        panel1.Controls.Add(result_textBox);
        panel1.Controls.Add(pictureBox3);
        panel1.Controls.Add(label2);
        panel1.Controls.Add(pictureBox2);
        panel1.Controls.Add(pictureBox4);
        panel1.Location = new Point(292, -2);
        panel1.Name = "panel1";
        panel1.Size = new Size(508, 297);
        panel1.TabIndex = 27;
        // 
        // pictureBox4
        // 
        pictureBox4.Location = new Point(130, 59);
        pictureBox4.Name = "pictureBox4";
        pictureBox4.Size = new Size(245, 215);
        pictureBox4.TabIndex = 137;
        pictureBox4.TabStop = false;
        pictureBox4.Visible = false;
        // 
        // result_textBox2
        // 
        result_textBox2.BackColor = SystemColors.GradientInactiveCaption;
        result_textBox2.BorderStyle = BorderStyle.None;
        result_textBox2.Enabled = false;
        result_textBox2.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold);
        result_textBox2.ForeColor = SystemColors.ControlDark;
        result_textBox2.Location = new Point(323, 27);
        result_textBox2.Name = "result_textBox2";
        result_textBox2.Size = new Size(75, 16);
        result_textBox2.TabIndex = 136;
        // 
        // TBName_textBox
        // 
        TBName_textBox.BackColor = SystemColors.GradientInactiveCaption;
        TBName_textBox.BorderStyle = BorderStyle.None;
        TBName_textBox.Enabled = false;
        TBName_textBox.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold);
        TBName_textBox.ForeColor = SystemColors.WindowText;
        TBName_textBox.Location = new Point(116, 27);
        TBName_textBox.Name = "TBName_textBox";
        TBName_textBox.ShortcutsEnabled = false;
        TBName_textBox.Size = new Size(120, 16);
        TBName_textBox.TabIndex = 135;
        TBName_textBox.TextAlign = HorizontalAlignment.Right;
        // 
        // result_textBox
        // 
        result_textBox.BackColor = SystemColors.GradientInactiveCaption;
        result_textBox.BorderStyle = BorderStyle.None;
        result_textBox.Enabled = false;
        result_textBox.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold);
        result_textBox.ForeColor = SystemColors.ControlDark;
        result_textBox.Location = new Point(242, 27);
        result_textBox.Name = "result_textBox";
        result_textBox.Size = new Size(75, 16);
        result_textBox.TabIndex = 134;
        result_textBox.TextAlign = HorizontalAlignment.Center;
        // 
        // pictureBox3
        // 
        pictureBox3.Location = new Point(254, 58);
        pictureBox3.Name = "pictureBox3";
        pictureBox3.Size = new Size(245, 215);
        pictureBox3.TabIndex = 134;
        pictureBox3.TabStop = false;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold);
        label2.ForeColor = SystemColors.ControlText;
        label2.Location = new Point(193, 8);
        label2.Name = "label2";
        label2.Size = new Size(0, 15);
        label2.TabIndex = 100;
        // 
        // pictureBox2
        // 
        pictureBox2.Location = new Point(7, 58);
        pictureBox2.Name = "pictureBox2";
        pictureBox2.Size = new Size(245, 215);
        pictureBox2.TabIndex = 1;
        pictureBox2.TabStop = false;
        // 
        // TB_dataGridView
        // 
        TB_dataGridView.AllowUserToAddRows = false;
        TB_dataGridView.AllowUserToDeleteRows = false;
        TB_dataGridView.AllowUserToResizeColumns = false;
        TB_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        TB_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        TB_dataGridView.BackgroundColor = Color.White;
        TB_dataGridView.BorderStyle = BorderStyle.None;
        TB_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        TB_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
        dataGridViewCellStyle1.Font =  new Font(UTIL.Families[0], 9.75F);
        dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
        dataGridViewCellStyle1.SelectionForeColor = Color.Black;
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        TB_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        TB_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        TB_dataGridView.Location = new Point(0, 327);
        TB_dataGridView.Name = "TB_dataGridView";
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle2.BackColor = SystemColors.Control;
        dataGridViewCellStyle2.Font =  new Font(UTIL.Families[0], 9.75F);
        dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
        dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
        TB_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
        TB_dataGridView.RowHeadersVisible = false;
        TB_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle3.Font =  new Font(UTIL.Families[0], 9.75F);
        dataGridViewCellStyle3.ForeColor = Color.Black;
        dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
        dataGridViewCellStyle3.SelectionForeColor = Color.Black;
        TB_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
        TB_dataGridView.Size = new Size(800, 208);
        TB_dataGridView.TabIndex = 19;
        TB_dataGridView.CellContentClick += TB_dataGridView_CellContentClick;
        // 
        // AddUserDB_button
        // 
        AddUserDB_button.BackColor = SystemColors.ControlLight;
        AddUserDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
        AddUserDB_button.FlatStyle = FlatStyle.System;
        AddUserDB_button.Font = new Font(UTIL.Families[0], 9.75F);
        AddUserDB_button.Location = new Point(732, 301);
        AddUserDB_button.Margin = new Padding(0);
        AddUserDB_button.Name = "AddUserDB_button";
        AddUserDB_button.Size = new Size(23, 23);
        AddUserDB_button.TabIndex = 172;
        AddUserDB_button.Text = "+";
        AddUserDB_button.UseVisualStyleBackColor = false;
        AddUserDB_button.Click += AddUserDB_button_Click;
        // 
        // Deletebutton
        // 
        Deletebutton.BackColor = SystemColors.ControlLight;
        Deletebutton.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
        Deletebutton.FlatStyle = FlatStyle.System;
        Deletebutton.Font = new Font(UTIL.Families[0], 9.75F);
        Deletebutton.Location = new Point(759, 301);
        Deletebutton.Margin = new Padding(0);
        Deletebutton.Name = "Deletebutton";
        Deletebutton.Size = new Size(23, 23);
        Deletebutton.TabIndex = 173;
        Deletebutton.Text = "-";
        Deletebutton.UseVisualStyleBackColor = false;
        Deletebutton.Click += Deletebutton_Click;
        // 
        // label36
        // 
        label36.AutoSize = true;
        label36.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold);
        label36.Location = new Point(632, 306);
        label36.Name = "label36";
        label36.Size = new Size(60, 15);
        label36.TabIndex = 174;
        label36.Text = "사용자DB";
        // 
        // TB_DB
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(797, 578);
        Controls.Add(label36);
        Controls.Add(Deletebutton);
        Controls.Add(AddUserDB_button);
        Controls.Add(TB_dataGridView);
        Controls.Add(Save_button);
        Controls.Add(GeneralPanel);
        Controls.Add(panel1);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "TB_DB";
        Text = "TB";
        GeneralPanel.ResumeLayout(false);
        GeneralPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
        ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
        ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
        ((System.ComponentModel.ISupportInitialize)TB_dataGridView).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Panel GeneralPanel;
    private Button Save_button;
    private Panel panel1;
    private DataGridView TB_dataGridView;
    private Label label1;
    private TextBox TBType_textBox;
    private Label label2;
    private TextBox result_textBox;
    private TextBox textBox2;
    private TextBox d_ins_textBox;
    private TextBox TBName_textBox;
    private PictureBox pictureBox1;
    private PictureBox pictureBox2;
    private Button AddUserDB_button;
    private Button Deletebutton;
    private Label label36;
    private PictureBox pictureBox3;
    private TextBox result_textBox2;
    private PictureBox pictureBox4;
}