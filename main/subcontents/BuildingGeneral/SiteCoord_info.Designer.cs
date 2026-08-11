namespace main.subcontents.BuildingGeneral;

partial class SiteCoord_info
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
        label4 = new Label();
        Lat_label = new Label();
        Lat_textBox = new TextBox();
        Lat_unit_label = new Label();
        Lon_label = new Label();
        Lon_textBox = new TextBox();
        Lon_unit_label = new Label();
        AddressSearch_button = new Button();
        MapSelect_button = new Button();
        Save_button = new Button();
        Cancel_button = new Button();
        SuspendLayout();
        //
        // label4
        //
        label4.AutoSize = true;
        label4.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
        label4.Location = new Point(12, 10);
        label4.Name = "label4";
        label4.Size = new Size(63, 15);
        label4.TabIndex = 0;
        label4.Text = "대지 좌표";
        //
        // Lat_label
        //
        Lat_label.AutoSize = true;
        Lat_label.Font = new Font("나눔바른고딕", 9.75F);
        Lat_label.ForeColor = SystemColors.ControlText;
        Lat_label.Location = new Point(20, 50);
        Lat_label.Name = "Lat_label";
        Lat_label.Size = new Size(28, 15);
        Lat_label.TabIndex = 1;
        Lat_label.Text = "위도";
        //
        // Lat_textBox
        //
        Lat_textBox.BackColor = Color.White;
        Lat_textBox.BorderStyle = BorderStyle.FixedSingle;
        Lat_textBox.Font = new Font("나눔바른고딕", 9.75F);
        Lat_textBox.ForeColor = SystemColors.ControlText;
        Lat_textBox.Location = new Point(70, 47);
        Lat_textBox.Name = "Lat_textBox";
        Lat_textBox.Size = new Size(110, 22);
        Lat_textBox.TabIndex = 2;
        Lat_textBox.TextAlign = HorizontalAlignment.Center;
        //
        // Lat_unit_label
        //
        Lat_unit_label.AutoSize = true;
        Lat_unit_label.Font = new Font("나눔바른고딕", 9.75F);
        Lat_unit_label.ForeColor = SystemColors.ControlDark;
        Lat_unit_label.Location = new Point(185, 50);
        Lat_unit_label.Name = "Lat_unit_label";
        Lat_unit_label.Size = new Size(22, 15);
        Lat_unit_label.TabIndex = 3;
        Lat_unit_label.Text = "°N";
        //
        // Lon_label
        //
        Lon_label.AutoSize = true;
        Lon_label.Font = new Font("나눔바른고딕", 9.75F);
        Lon_label.ForeColor = SystemColors.ControlText;
        Lon_label.Location = new Point(20, 85);
        Lon_label.Name = "Lon_label";
        Lon_label.Size = new Size(28, 15);
        Lon_label.TabIndex = 4;
        Lon_label.Text = "경도";
        //
        // Lon_textBox
        //
        Lon_textBox.BackColor = Color.White;
        Lon_textBox.BorderStyle = BorderStyle.FixedSingle;
        Lon_textBox.Font = new Font("나눔바른고딕", 9.75F);
        Lon_textBox.ForeColor = SystemColors.ControlText;
        Lon_textBox.Location = new Point(70, 82);
        Lon_textBox.Name = "Lon_textBox";
        Lon_textBox.Size = new Size(110, 22);
        Lon_textBox.TabIndex = 5;
        Lon_textBox.TextAlign = HorizontalAlignment.Center;
        //
        // Lon_unit_label
        //
        Lon_unit_label.AutoSize = true;
        Lon_unit_label.Font = new Font("나눔바른고딕", 9.75F);
        Lon_unit_label.ForeColor = SystemColors.ControlDark;
        Lon_unit_label.Location = new Point(185, 85);
        Lon_unit_label.Name = "Lon_unit_label";
        Lon_unit_label.Size = new Size(22, 15);
        Lon_unit_label.TabIndex = 6;
        Lon_unit_label.Text = "°E";
        //
        // AddressSearch_button
        //
        AddressSearch_button.BackColor = SystemColors.ControlLight;
        AddressSearch_button.Enabled = false;
        AddressSearch_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
        AddressSearch_button.FlatStyle = FlatStyle.System;
        AddressSearch_button.Font = new Font("나눔바른고딕", 9.75F);
        AddressSearch_button.Location = new Point(20, 125);
        AddressSearch_button.Name = "AddressSearch_button";
        AddressSearch_button.Size = new Size(110, 25);
        AddressSearch_button.TabIndex = 7;
        AddressSearch_button.Text = "주소로 찾기";
        AddressSearch_button.UseVisualStyleBackColor = false;
        //
        // MapSelect_button
        //
        MapSelect_button.BackColor = SystemColors.ControlLight;
        MapSelect_button.Enabled = false;
        MapSelect_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
        MapSelect_button.FlatStyle = FlatStyle.System;
        MapSelect_button.Font = new Font("나눔바른고딕", 9.75F);
        MapSelect_button.Location = new Point(140, 125);
        MapSelect_button.Name = "MapSelect_button";
        MapSelect_button.Size = new Size(110, 25);
        MapSelect_button.TabIndex = 8;
        MapSelect_button.Text = "지도에서 선택";
        MapSelect_button.UseVisualStyleBackColor = false;
        //
        // Save_button
        //
        Save_button.BackColor = SystemColors.ButtonHighlight;
        Save_button.ForeColor = Color.Black;
        Save_button.Location = new Point(150, 175);
        Save_button.Name = "Save_button";
        Save_button.Size = new Size(90, 25);
        Save_button.TabIndex = 9;
        Save_button.Text = "확인";
        Save_button.UseVisualStyleBackColor = true;
        Save_button.Click += Save_button_Click;
        //
        // Cancel_button
        //
        Cancel_button.BackColor = SystemColors.Control;
        Cancel_button.ForeColor = Color.Black;
        Cancel_button.Location = new Point(250, 175);
        Cancel_button.Name = "Cancel_button";
        Cancel_button.Size = new Size(90, 25);
        Cancel_button.TabIndex = 10;
        Cancel_button.Text = "취소";
        Cancel_button.UseVisualStyleBackColor = true;
        Cancel_button.Click += Cancel_button_Click;
        //
        // SiteCoord_info
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(360, 220);
        Controls.Add(label4);
        Controls.Add(Lat_label);
        Controls.Add(Lat_textBox);
        Controls.Add(Lat_unit_label);
        Controls.Add(Lon_label);
        Controls.Add(Lon_textBox);
        Controls.Add(Lon_unit_label);
        Controls.Add(AddressSearch_button);
        Controls.Add(MapSelect_button);
        Controls.Add(Save_button);
        Controls.Add(Cancel_button);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "SiteCoord_info";
        StartPosition = FormStartPosition.CenterParent;
        Text = "대지 좌표";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private Label label4;
    private Label Lat_label;
    private TextBox Lat_textBox;
    private Label Lat_unit_label;
    private Label Lon_label;
    private TextBox Lon_textBox;
    private Label Lon_unit_label;
    private Button AddressSearch_button;
    private Button MapSelect_button;
    private Button Save_button;
    private Button Cancel_button;
}
