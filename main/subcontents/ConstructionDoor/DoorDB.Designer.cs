namespace main.subcontents
{
    partial class DoorDB
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
            label2 = new Label();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            Save_button = new Button();
            Door_dataGridView = new DataGridView();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Door_dataGridView).BeginInit();
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
            GeneralPanel.Size = new Size(1104, 98);
            GeneralPanel.TabIndex = 18;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(690, 11);
            label2.Name = "label2";
            label2.Size = new Size(219, 75);
            label2.TabIndex = 2;
            label2.Text = "λ2 : 적용 열전도율 (W/mK)\r\nλ1 : 초기열전도율 (W/mK)\r\nFT: 온도 조건 변동 성능저하 보정계수\r\nFm: 습기 조건 변동 성능 저하 보정계수\r\nFa:  시간 경과 성능 저하 보정계수\r\n";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(470, 19);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(201, 59);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 26);
            label1.Name = "label1";
            label1.Size = new Size(419, 45);
            label1.TabIndex = 0;
            label1.Text = "- DB를 추가하고자 하는 경우 입력 방식에 따라 + 버튼을 누른후 입력하세요.\r\n\r\n- 재료명, 종류1, 열전도율은 필수 입력값 입니다.";
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(958, 256);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 20;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // Door_dataGridView
            // 
            Door_dataGridView.AllowUserToAddRows = false;
            Door_dataGridView.AllowUserToDeleteRows = false;
            Door_dataGridView.AllowUserToResizeColumns = false;
            Door_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Door_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Door_dataGridView.BackgroundColor = SystemColors.Window;
            Door_dataGridView.BorderStyle = BorderStyle.None;
            Door_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Door_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Door_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Door_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Door_dataGridView.Location = new Point(12, 144);
            Door_dataGridView.Name = "Door_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font =  new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Door_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Door_dataGridView.RowHeadersVisible = false;
            Door_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font =  new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Door_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Door_dataGridView.RowTemplate.Height = 25;
            Door_dataGridView.Size = new Size(1081, 106);
            Door_dataGridView.TabIndex = 112;
            Door_dataGridView.CellContentClick += Door_dataGridView_CellContentClick;
            Door_dataGridView.CellValueChanged += Door_dataGridView_CellValueChanged;
            // 
            // DoorDB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1105, 291);
            Controls.Add(Door_dataGridView);
            Controls.Add(Save_button);
            Controls.Add(GeneralPanel);
            Name = "DoorDB";
            Text = "MaterialDB_new";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Door_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private Button Save_button;
        private Label label7;
        private DataGridView Door_dataGridView;
    }
}