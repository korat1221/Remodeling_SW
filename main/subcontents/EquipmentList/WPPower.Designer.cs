namespace main.subcontents.EquipmentList;

partial class WPPower
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
        WPPower_dataGridView = new DataGridView();
        ((System.ComponentModel.ISupportInitialize)WPPower_dataGridView).BeginInit();
        SuspendLayout();
        // 
        // Save_button
        // 
        Save_button.BackColor = SystemColors.ButtonHighlight;
        Save_button.ForeColor = Color.Black;
        Save_button.Location = new Point(93, 374);
        Save_button.Name = "Save_button";
        Save_button.Size = new Size(135, 25);
        Save_button.TabIndex = 20;
        Save_button.Text = "SAVE";
        Save_button.UseVisualStyleBackColor = true;
        Save_button.Click += Save_button_Click;
        // 
        // WPPower_dataGridView
        // 
        WPPower_dataGridView.AllowUserToAddRows = false;
        WPPower_dataGridView.AllowUserToDeleteRows = false;
        WPPower_dataGridView.AllowUserToResizeColumns = false;
        WPPower_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        WPPower_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        WPPower_dataGridView.BackgroundColor = Color.White;
        WPPower_dataGridView.BorderStyle = BorderStyle.None;
        WPPower_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        WPPower_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
        dataGridViewCellStyle1.Font = new Font("나눔바른고딕", 9.75F);
        dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
        dataGridViewCellStyle1.SelectionForeColor = Color.Black;
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        WPPower_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        WPPower_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        WPPower_dataGridView.Location = new Point(0, 32);
        WPPower_dataGridView.Name = "WPPower_dataGridView";
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle2.BackColor = SystemColors.Control;
        dataGridViewCellStyle2.Font = new Font("나눔바른고딕", 9.75F);
        dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
        dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
        WPPower_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
        WPPower_dataGridView.RowHeadersVisible = false;
        WPPower_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle3.Font = new Font("나눔바른고딕", 9.75F);
        dataGridViewCellStyle3.ForeColor = Color.Black;
        dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
        dataGridViewCellStyle3.SelectionForeColor = Color.Black;
        WPPower_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
        WPPower_dataGridView.Size = new Size(240, 322);
        WPPower_dataGridView.TabIndex = 19;
        WPPower_dataGridView.CellContentClick += WPPower_dataGridView_CellContentClick;
        // 
        // WPPower
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(243, 411);
        Controls.Add(WPPower_dataGridView);
        Controls.Add(Save_button);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "WPPower";
        Text = "WPPower";
        ((System.ComponentModel.ISupportInitialize)WPPower_dataGridView).EndInit();
        ResumeLayout(false);
    }

    #endregion
    private Button Save_button;
    private DataGridView WPPower_dataGridView;
    private TextBox textBox2;
    private TextBox d_ins_textBox;
}