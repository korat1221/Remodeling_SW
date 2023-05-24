namespace main.subcontents
{
    partial class Window_InstallDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window_InstallDB));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            GeneralPanel = new Panel();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            Install_dataGridView = new DataGridView();
            Save_button = new Button();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Install_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.AliceBlue;
            GeneralPanel.Controls.Add(label2);
            GeneralPanel.Controls.Add(pictureBox1);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Location = new Point(0, -2);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(682, 181);
            GeneralPanel.TabIndex = 18;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(484, 43);
            label2.Name = "label2";
            label2.Size = new Size(171, 90);
            label2.TabIndex = 2;
            label2.Text = "표준값 적용시\r\n왼쪽그림처럼 콘크리트에 의해\r\n단열이 끊어지는 경우 구분1에\r\n따라 다음 항목으로 적용한다.\r\n1. 내단열 --> 외부측 설치\r\n2. 외단열--> 내부측 설치\r\n";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(398, 33);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(80, 117);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 43);
            label1.Name = "label1";
            label1.Size = new Size(390, 90);
            label1.TabIndex = 0;
            label1.Text = "- ISO 10211 기준, KIAEBS S-8:2017 기준 시뮬레이션 값을 적용합니다.\r\n\r\n- 오른쪽 그림을 참조하여 해당하는 값을 입력하시오.\r\n\r\n- 설치 부위는 상부, 측면, 하부로 구분되며 차양장치에의한 추가 열교는\r\n   외피 정보에서 추가로 반영됩니다.";
            // 
            // Frame_dataGridView
            // 
            Install_dataGridView.AllowUserToAddRows = false;
            Install_dataGridView.AllowUserToDeleteRows = false;
            Install_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Install_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Install_dataGridView.BackgroundColor = SystemColors.Control;
            Install_dataGridView.BorderStyle = BorderStyle.None;
            Install_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Install_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Install_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Install_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Install_dataGridView.Location = new Point(3, 172);
            Install_dataGridView.Name = "Frame_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Install_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Install_dataGridView.RowHeadersVisible = false;
            Install_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Install_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Install_dataGridView.RowTemplate.Height = 25;
            Install_dataGridView.Size = new Size(679, 120);
            Install_dataGridView.TabIndex = 19;
            Install_dataGridView.CellContentClick += Frame_dataGridView_CellContentClick;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(538, 298);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 20;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // Window_InstallDB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(685, 331);
            Controls.Add(Save_button);
            Controls.Add(Install_dataGridView);
            Controls.Add(GeneralPanel);
            Name = "Window_InstallDB";
            Text = "Window_InstallDB";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Install_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private DataGridView Install_dataGridView;
        private Button Save_button;
    }
}