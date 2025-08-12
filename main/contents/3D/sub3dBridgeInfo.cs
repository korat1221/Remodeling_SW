using main.subcontents.HeatingSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CustomComboBox;
using static main.MainContents;
using main.subcontents.ThermalBridge;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using main.info;

namespace main.contents
{
    public partial class sub3dBridgeInfo : Form
    {
        string sid = "";
        string SelectTBType, checkTBType;
        Boolean checkSame = true;
        string TBNum;
        public sub3dBridgeInfo()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '열교정보'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            create_datagridview1();
        }
        private void onVisibleChanged(object sender, EventArgs e)
        {
            if (main.MainContents.selID != sid)
            {
                this.panel1.Show();
                Load_TBDB();
            }
        }
        private void create_datagridview1()
        {
            new StackedHeaderDecorator(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill, dataGridView1_RowHandle, true);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            dataGridView1.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            dataGridView1.Columns.Add(checkBoxColumn);

            dataGridView1.Columns.Add("A1", "번호");
            dataGridView1.Columns.Add("A2", "유형");
            dataGridView1.Columns.Add("A3", "적용 열교");
            dataGridView1.Columns.Add("A4", "열교 명칭");
            dataGridView1.Columns.Add("A5", "선형 열관류율[W/mK]");
            dataGridView1.Columns.Add("A6", "길이[m]");
            dataGridView1.Columns[0].Width = 30;
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[6].Width = 80;
            fillFilterCombos();
        }
        private void Load_TBDB()
        {
            fillFilterCombos();
            dataGridView1.Rows.Clear();

            string[][] Value;
            if (SelectTBType == null || SelectTBType == "" || SelectTBType == "ALL")
            {
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct 번호,열교항목,열교길이,선택열교 from ThermalBridge_3D Order by 번호");

            }
            else
            {
                Value = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct 번호,열교항목,열교길이,선택열교 from ThermalBridge_3D Where 열교항목 ='" + SelectTBType + "'");
            }

            if (Value.Length > 0)
            {
                for (int i = 0; i < Value.Length; i++)
                {
                    int nRow = dataGridView1.Rows.Add();
                    if (Check_checkBox.Checked == true)
                    { dataGridView1.Rows[nRow].Cells[0].Value = true; }
                    else { dataGridView1.Rows[nRow].Cells[0].Value = false; }
                    dataGridView1.Rows[nRow].Cells[1].Value = Value[i][0]; ;
                    dataGridView1.Rows[nRow].Cells[2].Value = Value[i][1]; ;
                    dataGridView1.Rows[nRow].Cells[6].Value = Convert.ToDouble(Value[i][2]).ToString("0.0");
                    dataGridView1.Rows[nRow].Cells[3].Value = Value[i][3]; ;
                    if (Value[i][3] != null && Value[i][3] != "")
                    {
                        string[][] tb2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "접합부열교", "번호,명칭,값", "번호 ='" + Value[i][3] + "'");
                        if (tb2.Length > 0) { }
                        else
                        {
                            tb2 = Program.DB.getValue(DB.type.ProjDB, "User_TB", "번호,명칭,값", "번호 ='" + Value[i][3] + "'");
                        }

                        if (tb2.Length > 0)
                        {
                            dataGridView1.Rows[i].Cells[3].Value = tb2[0][0]; ;
                            dataGridView1.Rows[i].Cells[4].Value = tb2[0][1]; ;
                            dataGridView1.Rows[i].Cells[5].Value = Convert.ToDouble(tb2[0][2]).ToString("0.000");
                        }
                    }
                }
            }
            string[][] dU = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "외벽dUtb, 지붕dUtb, 바닥dUtb");
            if (dU.Length > 0 && dU[0][0] != "")
            {
                dUtb_label.Visible = true;
                dUtbWall_label.Visible = true;
                dUtbRoof_label.Visible = true;
                dUtbFloor_label.Visible = true;

                string script = Program.UTIL.Subscript(2, true);
                dUtbWall_label.Text = "외벽 : " + Convert.ToDouble(dU[0][0]).ToString("0.00") + " W/m" + script + "·K";
                dUtbRoof_label.Text = "지붕 : " + Convert.ToDouble(dU[0][1]).ToString("0.00") + " W/m" + script + "·K";
                dUtbFloor_label.Text = "바닥 : " + Convert.ToDouble(dU[0][2]).ToString("0.00") + " W/m" + script + "·K";
            }
            else
            {
                dUtb_label.Visible = false;
                dUtbWall_label.Visible = false;
                dUtbRoof_label.Visible = false;
                dUtbFloor_label.Visible = false;
            }
        }

        private void Check_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = Check_checkBox.Checked;
            }
        }
        private void TB_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (TB_comboBox.SelectedItem != null)
            //{
            //    SelectTBType = TB_comboBox.SelectedItem.ToString();
            //    if (SelectTBType != null && SelectTBType != "")
            //    {

            //        for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //        {
            //            Program.DB.setValue(DB.type.ProjDB, "ThermalBridge_3D", "번호,선택열교",
            //             "'" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[3].Value + "'",
            //             "번호");
            //        }
            //        Program.DB.saveProject();
            //        Load_TBDB();
            //        Check_checkBox.Checked = true;
            //    }
            //}
        }
        private void Checked_Value()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) == true)
                {
                    if (checkTBType == null)
                    {
                        checkTBType = dataGridView1.Rows[i].Cells[2].Value.ToString();
                        checkSame = true;
                    }
                    else if (dataGridView1.Rows[i].Cells[2].Value.ToString() == checkTBType)
                    {
                        checkSame = true;
                    }
                    else
                    {
                        MessageBox.Show("같은 유형만 선택하세요.");
                        checkSame = false;
                        break;
                    }
                }
            }

        }
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == 0)
            {
                int cellX = dataGridView1.Location.X + e.CellBounds.X;
                int cellY = dataGridView1.Location.Y + e.CellBounds.Y;

                if (e.ColumnIndex == 0)
                {
                    if (!Check_checkBox.Visible)
                    {
                        Check_checkBox.Location = new Point(cellX + 10, cellY + 5);
                        Check_checkBox.Size = new Size(e.CellBounds.Width, e.CellBounds.Height);
                        Check_checkBox.Show();
                    }
                }
                if (e.ColumnIndex == 2)
                {
                    if (!TB_comboBox.Visible)
                    {
                        //  TB_comboBox.Parent = dataGridView1;           // 또는 dataGridView1
                        TB_comboBox.Location = new Point(cellX, cellY);
                        TB_comboBox.Size = new Size(e.CellBounds.Width, e.CellBounds.Height);
                        //TB_comboBox.BringToFront();
                        //TB_comboBox.Visible = true;
                        TB_comboBox.Show();
                    }
                }
                else if (e.ColumnIndex == 3)
                {
                    if (!TB_button.Visible)
                    {
                        TB_button.Location = new Point(cellX, cellY - 1);
                        TB_button.Size = new Size(e.CellBounds.Width, e.CellBounds.Height);
                        TB_button.Show();
                    }
                }
            }
        }




        private void fillFilterCombos()
        {
            int i = -1;
            string[][] rec = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct 열교항목 From ThermalBridge_3D Order by 번호");

            TB_comboBox.Items.Clear();

            TB_comboBox.Items.Add("ALL");
            while (++i < rec.Length)
            {
                TB_comboBox.Items.Add(rec[i][0]);
            }
        }
        private bool dataGridView1_RowHandle(DataGridViewCell cell, int column, int row)
        {
            if (row % 2 == 1)
            {
                if (column == 1 || column == 2 || column == 3 || column == 4 || column == 5 || column == 6)
                {
                    cell.Style.BackColor = SystemColors.InactiveBorder;
                    return true;
                }
                else return false;
            }
            else
            {
                if (column == 1 || column == 2 || column == 3 || column == 4 || column == 5 || column == 6)
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                    return true;
                }
                else return false;
            }
        }

        private void TB_button_Click(object sender, EventArgs e)
        {
            Checked_Value();
            if (checkSame)
            {
                if (checkTBType != null)
                {
                    subcontents.ThermalBridge.TB_DB tb = new subcontents.ThermalBridge.TB_DB(checkTBType);
                    DialogResult result = tb.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        TBNum = tb.TBNum;

                        string[][] tb2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "접합부열교", "번호,명칭,값", "번호 ='" + TBNum + "'");
                        if (tb2.Length == 0) { tb2 = Program.DB.getValue(DB.type.ProjDB, "User_TB", "번호,명칭,값", "번호 ='" + TBNum + "'"); }


                        if (tb2.Length > 0)
                        {
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) == true)
                                {
                                    dataGridView1.Rows[i].Cells[3].Value = tb2[0][0]; ;
                                    dataGridView1.Rows[i].Cells[4].Value = tb2[0][1]; ;
                                    dataGridView1.Rows[i].Cells[5].Value = Convert.ToDouble(tb2[0][2]).ToString("0.000");
                                }
                            }
                        }
                    }

                    checkTBType = null;
                }
                else
                {
                    MessageBox.Show("열교 값을 적용할 부위를 선택해주세요.");
                }
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                Program.DB.setValue(DB.type.ProjDB, "ThermalBridge_3D", "번호,선택열교",
                 "'" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[3].Value + "'",
                 "번호");
            }
            Program.DB.saveProject();
            MessageBox.Show("저장되었습니다.");

        }

        private void info_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\12.3D_TB";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }

        private void TB_comboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (TB_comboBox.SelectedItem != null)
            {
                SelectTBType = TB_comboBox.SelectedItem.ToString();
                if (SelectTBType != null && SelectTBType != "")
                {

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        Program.DB.setValue(DB.type.ProjDB, "ThermalBridge_3D", "번호,선택열교",
                         "'" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[3].Value + "'",
                         "번호");
                    }
                    Program.DB.saveProject();
                    Load_TBDB();
                    Check_checkBox.Checked = true;
                }
                TB_comboBox.Text = SelectTBType;
            }
        }
    }
}
