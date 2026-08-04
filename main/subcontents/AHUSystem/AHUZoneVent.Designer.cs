namespace main.subcontents.AHUSystem
{
    partial class AHUZoneVent
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            GeneralPanel = new Panel();
            Title_label = new Label();
            AddZone_label = new Label();
            AddZone_comboBox = new CustomComboBox();
            AddZone_button = new Button();
            RemoveZone_label = new Label();
            RemoveZone_comboBox = new CustomComboBox();
            RemoveZone_button = new Button();
            Save_button = new Button();
            Content_panel = new Panel();
            DiagramScroll_panel = new Panel();
            Diagram_panel = new HRVDiagramPanel();
            tabControl1 = new CustomTabControl();
            ZoneList_tabPage = new TabPage();
            Zone_dataGridView = new DataGridView();
            AdjacentZone_tabPage = new TabPage();
            AdjacentZone_dataGridView = new DataGridView();
            GeneralPanel.SuspendLayout();
            Content_panel.SuspendLayout();
            tabControl1.SuspendLayout();
            ZoneList_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Zone_dataGridView).BeginInit();
            AdjacentZone_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AdjacentZone_dataGridView).BeginInit();
            SuspendLayout();
            //
            // GeneralPanel
            //
            GeneralPanel.BackColor = Color.AliceBlue;
            GeneralPanel.Controls.Add(Title_label);
            GeneralPanel.Controls.Add(AddZone_label);
            GeneralPanel.Controls.Add(AddZone_comboBox);
            GeneralPanel.Controls.Add(AddZone_button);
            GeneralPanel.Controls.Add(RemoveZone_label);
            GeneralPanel.Controls.Add(RemoveZone_comboBox);
            GeneralPanel.Controls.Add(RemoveZone_button);
            GeneralPanel.Location = new Point(0, -2);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(1000, 47);
            GeneralPanel.TabIndex = 0;
            //
            // Title_label
            //
            Title_label.AutoSize = true;
            Title_label.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            Title_label.Location = new Point(20, 13);
            Title_label.Name = "Title_label";
            Title_label.Size = new Size(120, 16);
            Title_label.TabIndex = 0;
            Title_label.Text = "존별 급배기량 입력";
            //
            // AddZone_label
            //
            AddZone_label.AutoSize = true;
            AddZone_label.Location = new Point(380, 15);
            AddZone_label.Name = "AddZone_label";
            AddZone_label.Size = new Size(50, 16);
            AddZone_label.TabIndex = 2;
            AddZone_label.Text = "존 추가";
            //
            // AddZone_comboBox
            //
            // 미연결 존을 헤더로 추가하는 콤보박스 — 하우스 스타일(CustomComboBox) 사용.
            AddZone_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            AddZone_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            AddZone_comboBox.Font = new Font("나눔바른고딕", 9.75F);
            AddZone_comboBox.FormattingEnabled = true;
            AddZone_comboBox.Location = new Point(436, 12);
            AddZone_comboBox.Name = "AddZone_comboBox";
            AddZone_comboBox.Size = new Size(160, 23);
            AddZone_comboBox.TabIndex = 3;
            //
            // AddZone_button
            //
            // AHUoptions_button(다른 subcontents 팝업을 여는 "+" 버튼)과 동일 스타일
            AddZone_button.BackColor = SystemColors.ControlLight;
            AddZone_button.FlatStyle = FlatStyle.System;
            AddZone_button.Font = new Font("나눔바른고딕", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            AddZone_button.Location = new Point(602, 11);
            AddZone_button.Name = "AddZone_button";
            AddZone_button.Size = new Size(23, 23);
            AddZone_button.TabIndex = 4;
            AddZone_button.Text = "+";
            AddZone_button.UseVisualStyleBackColor = false;
            AddZone_button.Click += AddZone_button_Click;
            //
            // RemoveZone_label
            //
            RemoveZone_label.AutoSize = true;
            RemoveZone_label.Location = new Point(645, 15);
            RemoveZone_label.Name = "RemoveZone_label";
            RemoveZone_label.Size = new Size(50, 16);
            RemoveZone_label.TabIndex = 5;
            RemoveZone_label.Text = "존 제거";
            //
            // RemoveZone_comboBox
            //
            // "+"로 추가했거나(또는 저장 후 재로드로 복원된) 설비 미연결 존만 골라 보여줌 — 설비에 실제로
            // 연결된 존(ZoneGeneral_Form 기준)은 이 화면에서 임의로 뗄 수 있는 대상이 아니므로 제외
            RemoveZone_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            RemoveZone_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            RemoveZone_comboBox.Font = new Font("나눔바른고딕", 9.75F);
            RemoveZone_comboBox.FormattingEnabled = true;
            RemoveZone_comboBox.Location = new Point(701, 12);
            RemoveZone_comboBox.Name = "RemoveZone_comboBox";
            RemoveZone_comboBox.Size = new Size(160, 23);
            RemoveZone_comboBox.TabIndex = 6;
            //
            // RemoveZone_button
            //
            RemoveZone_button.BackColor = SystemColors.ControlLight;
            RemoveZone_button.FlatStyle = FlatStyle.System;
            RemoveZone_button.Font = new Font("나눔바른고딕", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            RemoveZone_button.Location = new Point(867, 11);
            RemoveZone_button.Name = "RemoveZone_button";
            RemoveZone_button.Size = new Size(23, 23);
            RemoveZone_button.TabIndex = 7;
            RemoveZone_button.Text = "-";
            RemoveZone_button.UseVisualStyleBackColor = false;
            RemoveZone_button.Click += RemoveZone_button_Click;
            //
            // Save_button
            //
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(845, 665);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 2;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            //
            // Content_panel
            //
            Content_panel.Controls.Add(DiagramScroll_panel);
            Content_panel.Controls.Add(tabControl1);
            Content_panel.Location = new Point(0, 45);
            Content_panel.Name = "Content_panel";
            Content_panel.Size = new Size(1000, 610);
            Content_panel.TabIndex = 1;
            //
            // DiagramScroll_panel
            //
            // 존 개수가 많아 계통도(Diagram_panel)가 뷰포트보다 커지면 세로 스크롤바가 생김(폼 크기는 고정 유지)
            DiagramScroll_panel.AutoScroll = true;
            DiagramScroll_panel.BorderStyle = BorderStyle.FixedSingle;
            DiagramScroll_panel.Controls.Add(Diagram_panel);
            DiagramScroll_panel.Dock = DockStyle.Fill;
            DiagramScroll_panel.Location = new Point(0, 320);
            DiagramScroll_panel.Name = "DiagramScroll_panel";
            DiagramScroll_panel.Size = new Size(1000, 290);
            DiagramScroll_panel.TabIndex = 2;
            //
            // Diagram_panel
            //
            // Height는 존 개수에 맞춰 UpdateDiagramHeight()에서 런타임에 계산해 다시 설정함
            Diagram_panel.BorderStyle = BorderStyle.None;
            Diagram_panel.Dock = DockStyle.Top;
            Diagram_panel.Location = new Point(0, 0);
            Diagram_panel.Name = "Diagram_panel";
            Diagram_panel.Size = new Size(1000, 290);
            Diagram_panel.TabIndex = 1;
            //
            // tabControl1
            //
            tabControl1.Controls.Add(ZoneList_tabPage);
            tabControl1.Controls.Add(AdjacentZone_tabPage);
            tabControl1.DisplayStyleProvider.BorderColor = SystemColors.Control;
            tabControl1.DisplayStyleProvider.BorderColorHot = SystemColors.Control;
            tabControl1.DisplayStyleProvider.CloserColor = Color.Empty;
            tabControl1.DisplayStyleProvider.FocusTrack = true;
            tabControl1.DisplayStyleProvider.HotTrack = true;
            tabControl1.DisplayStyleProvider.ImageAlign = ContentAlignment.MiddleLeft;
            tabControl1.DisplayStyleProvider.Opacity = 1F;
            tabControl1.DisplayStyleProvider.Overlap = 0;
            tabControl1.DisplayStyleProvider.Padding = new Point(6, 3);
            tabControl1.DisplayStyleProvider.ShowTabCloser = false;
            tabControl1.DisplayStyleProvider.TextColor = SystemColors.ControlText;
            tabControl1.DisplayStyleProvider.TextColorDisabled = SystemColors.ControlDark;
            tabControl1.DisplayStyleProvider.TextColorSelected = SystemColors.ControlText;
            tabControl1.Dock = DockStyle.Top;
            tabControl1.HotTrack = true;
            tabControl1.ItemSize = new Size(128, 20);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1000, 320);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabIndex = 0;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            //
            // ZoneList_tabPage
            //
            ZoneList_tabPage.BackColor = Color.White;
            ZoneList_tabPage.Controls.Add(Zone_dataGridView);
            ZoneList_tabPage.Location = new Point(4, 25);
            ZoneList_tabPage.Name = "ZoneList_tabPage";
            ZoneList_tabPage.Padding = new Padding(3);
            ZoneList_tabPage.Size = new Size(992, 291);
            ZoneList_tabPage.TabIndex = 0;
            ZoneList_tabPage.Text = "존 별 급·배기량";
            //
            // Zone_dataGridView
            //
            Zone_dataGridView.AllowUserToAddRows = false;
            Zone_dataGridView.AllowUserToDeleteRows = false;
            Zone_dataGridView.AllowUserToResizeColumns = false;
            Zone_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Zone_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Zone_dataGridView.BackgroundColor = SystemColors.Control;
            Zone_dataGridView.BorderStyle = BorderStyle.None;
            Zone_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Zone_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Zone_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Zone_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Zone_dataGridView.Dock = DockStyle.Fill;
            Zone_dataGridView.Location = new Point(3, 3);
            Zone_dataGridView.Name = "Zone_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Zone_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Zone_dataGridView.RowHeadersVisible = false;
            Zone_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Zone_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Zone_dataGridView.RowTemplate.Height = 25;
            Zone_dataGridView.Size = new Size(986, 285);
            Zone_dataGridView.TabIndex = 0;
            Zone_dataGridView.CellEndEdit += Zone_dataGridView_CellEndEdit;
            //
            // AdjacentZone_tabPage
            //
            AdjacentZone_tabPage.BackColor = Color.White;
            AdjacentZone_tabPage.Controls.Add(AdjacentZone_dataGridView);
            AdjacentZone_tabPage.Location = new Point(4, 25);
            AdjacentZone_tabPage.Name = "AdjacentZone_tabPage";
            AdjacentZone_tabPage.Padding = new Padding(3);
            AdjacentZone_tabPage.Size = new Size(992, 291);
            AdjacentZone_tabPage.TabIndex = 1;
            AdjacentZone_tabPage.Text = "인접존 환기량";
            //
            // AdjacentZone_dataGridView
            //
            AdjacentZone_dataGridView.AllowUserToAddRows = false;
            AdjacentZone_dataGridView.AllowUserToDeleteRows = false;
            AdjacentZone_dataGridView.AllowUserToResizeColumns = false;
            AdjacentZone_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            AdjacentZone_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            AdjacentZone_dataGridView.BackgroundColor = SystemColors.Control;
            AdjacentZone_dataGridView.BorderStyle = BorderStyle.None;
            AdjacentZone_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            AdjacentZone_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            AdjacentZone_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            AdjacentZone_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            AdjacentZone_dataGridView.Dock = DockStyle.Fill;
            AdjacentZone_dataGridView.Location = new Point(3, 3);
            AdjacentZone_dataGridView.Name = "AdjacentZone_dataGridView";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            AdjacentZone_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            AdjacentZone_dataGridView.RowHeadersVisible = false;
            AdjacentZone_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            AdjacentZone_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
            AdjacentZone_dataGridView.RowTemplate.Height = 25;
            AdjacentZone_dataGridView.Size = new Size(986, 285);
            AdjacentZone_dataGridView.TabIndex = 0;
            AdjacentZone_dataGridView.CellEndEdit += AdjacentZone_dataGridView_CellEndEdit;
            //
            // AHUZoneVent
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1000, 700);
            Controls.Add(Content_panel);
            Controls.Add(Save_button);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "AHUZoneVent";
            Text = "존별 급배기량 입력";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            Content_panel.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            ZoneList_tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Zone_dataGridView).EndInit();
            AdjacentZone_tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)AdjacentZone_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Label Title_label;
        private Label AddZone_label;
        private CustomComboBox AddZone_comboBox;
        private Button AddZone_button;
        private Label RemoveZone_label;
        private CustomComboBox RemoveZone_comboBox;
        private Button RemoveZone_button;
        private Button Save_button;
        private Panel Content_panel;
        private CustomTabControl tabControl1;
        private TabPage ZoneList_tabPage;
        private DataGridView Zone_dataGridView;
        private TabPage AdjacentZone_tabPage;
        private DataGridView AdjacentZone_dataGridView;
        private Panel DiagramScroll_panel;
        private HRVDiagramPanel Diagram_panel;
    }
}
