namespace main.subcontents
{
    partial class Window_FrameDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window_FrameDB));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            GeneralPanel = new Panel();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            Frame_dataGridView = new DataGridView();
            Save_button = new Button();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Frame_dataGridView).BeginInit();
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
            GeneralPanel.Size = new Size(977, 181);
            GeneralPanel.TabIndex = 18;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(644, 6);
            label2.Name = "label2";
            label2.Size = new Size(249, 165);
            label2.TabIndex = 2;
            label2.Text = "Uf,A: 길이 가중 ①②③ 프레임 평균 열관류율\r\n\r\nUf,B: 길이 가중 ④⑤⑥ 프레임 평균 열관류율\r\n\r\nUf,C: ⑦ 프레임 열관류율\r\n\r\ndA: ①②③ 길이 가중 프레임 평균 두께\r\n\r\ndB:  ④⑤⑥ 길이 가중 프레임 평균 두께\r\n\r\ndC: ⑦ 프레임 두께";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(388, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(201, 153);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(42, 66);
            label1.Name = "label1";
            label1.Size = new Size(291, 45);
            label1.TabIndex = 0;
            label1.Text = "- ISO 10077기준 시뮬레이션 계산값을 적용합니다.\r\n\r\n- 오른쪽 그림을 참조하여 해당하는 값을 입력하시오.";
            // 
            // Frame_dataGridView
            // 
            Frame_dataGridView.AllowUserToAddRows = false;
            Frame_dataGridView.AllowUserToDeleteRows = false;
            Frame_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Frame_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Frame_dataGridView.BackgroundColor = SystemColors.Control;
            Frame_dataGridView.BorderStyle = BorderStyle.None;
            Frame_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Frame_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Frame_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Frame_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Frame_dataGridView.Location = new Point(3, 172);
            Frame_dataGridView.Name = "Frame_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Frame_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Frame_dataGridView.RowHeadersVisible = false;
            Frame_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Frame_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Frame_dataGridView.RowTemplate.Height = 25;
            Frame_dataGridView.Size = new Size(971, 241);
            Frame_dataGridView.TabIndex = 19;
            Frame_dataGridView.CellContentClick += Frame_dataGridView_CellContentClick;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(830, 419);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 20;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // Window_FrameDB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(977, 459);
            Controls.Add(Save_button);
            Controls.Add(Frame_dataGridView);
            Controls.Add(GeneralPanel);
            Name = "Window_FrameDB";
            Text = "Window_FrameDB";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Frame_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private DataGridView Frame_dataGridView;
        private Button Save_button;
    }
}