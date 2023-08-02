using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static main.DB;
using System.Xml.Linq;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace main.contents
{
    public partial class EquipmentList : Form
    {
        DataGridViewCheckBoxColumn Boiler_checkBoxColumn = new DataGridViewCheckBoxColumn();
        int Boiler_SelectRow;


        public EquipmentList()
        {
            InitializeComponent();
            Program.DB.initTable(DB.type.ProjDB, "User_Boiler");

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Create_Boiler_Table();
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        public void Create_Boiler_Table()
        {
            Boiler_dataGridView.Columns.Clear();
            Boiler_checkBoxColumn.HeaderText = "선택";
            Boiler_checkBoxColumn.Name = "check";
            Boiler_dataGridView.Columns.Add(Boiler_checkBoxColumn);

            Boiler_dataGridView.ColumnCount = 11;
            Boiler_dataGridView.Columns[1].HeaderText = "번호";
            Boiler_dataGridView.Columns[2].HeaderText = "명칭";
            Boiler_dataGridView.Columns[3].HeaderText = "난방/급탕";
            Boiler_dataGridView.Columns[4].HeaderText = "연료";
            Boiler_dataGridView.Columns[5].HeaderText = "Type";
            Boiler_dataGridView.Columns[6].HeaderText = "용량" + Environment.NewLine + "[kW]";
            Boiler_dataGridView.Columns[7].HeaderText = "전부하효율" + Environment.NewLine + "[%]";
            Boiler_dataGridView.Columns[8].HeaderText = "부분부하효율" + Environment.NewLine + "[%]";
            Boiler_dataGridView.Columns[9].HeaderText = "소비전력" + Environment.NewLine + "[W]";
            Boiler_dataGridView.Columns[10].HeaderText = "대기전력" + Environment.NewLine + "[W]";

        }

        private void Boiler_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = Boiler_dataGridView.Rows.Add();
            Load_Material_Num();

            DataGridViewComboBoxCell 난방급탕Combo = new DataGridViewComboBoxCell();
            난방급탕Combo.Items.Add("난방");
            난방급탕Combo.Items.Add("급탕");
            난방급탕Combo.Items.Add("난방+급탕");
            Boiler_dataGridView.Rows[nRow].Cells[3] = 난방급탕Combo;

            DataGridViewComboBoxCell 연료Combo = new DataGridViewComboBoxCell();
            연료Combo.Items.Add("LNG");
            연료Combo.Items.Add("LPG");
            연료Combo.Items.Add("기름");
            연료Combo.Items.Add("펠릿");
            연료Combo.Items.Add("전기");
            Boiler_dataGridView.Rows[nRow].Cells[4] = 연료Combo;

            Boiler_dataGridView.Rows[nRow].Cells[2].Style.BackColor = SystemColors.Info;
            Boiler_dataGridView.Rows[nRow].Cells[6].Style.BackColor = SystemColors.Info;
            Boiler_dataGridView.Rows[nRow].Cells[7].Style.BackColor = SystemColors.Info;
            Boiler_dataGridView.Rows[nRow].Cells[8].Style.BackColor = SystemColors.Info;
            Boiler_dataGridView.Rows[nRow].Cells[9].Style.BackColor = SystemColors.Info;
            Boiler_dataGridView.Rows[nRow].Cells[10].Style.BackColor = SystemColors.Info;
        }

        private void Boiler_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 4)
                {
                    Load_Boiler_Type(e.RowIndex);
                }
                else if (e.ColumnIndex == 7 || e.ColumnIndex == 8)
                {
                    if (Boiler_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    {
                        if (Convert.ToDouble(Boiler_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) < 1)
                        {
                            MessageBox.Show("퍼센트 단위로 입력하세요.(Ex : 90.1% ⇒ 90.1");
                            Boiler_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                        }
                    }
                }
                if(Boiler_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    Boiler_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                }
            }
        }

        private void Load_Boiler_Type(int nRow)
        {
            DataGridViewComboBoxCell TypeCombo = new DataGridViewComboBoxCell();

            switch (Boiler_dataGridView.Rows[nRow].Cells[4].Value)
            {
                case "LPG":
                    TypeCombo.Items.Clear();
                    TypeCombo.Items.Add("콘덴싱가스보일러");
                    TypeCombo.Items.Add("일반가스보일러");
                    break;
                case "LNG":
                    TypeCombo.Items.Clear();
                    TypeCombo.Items.Add("콘덴싱가스보일러");
                    TypeCombo.Items.Add("일반가스보일러");
                    break;
                case "기름":
                    TypeCombo.Items.Clear();
                    TypeCombo.Items.Add("콘덴싱기름보일러");
                    TypeCombo.Items.Add("일반기름보일러");
                    break;
                case "펠릿":
                    TypeCombo.Items.Clear();
                    TypeCombo.Items.Add("펠릿콘덴싱보일러");
                    TypeCombo.Items.Add("펠릿노통형보일러");
                    break;
                case "전기":
                    TypeCombo.Items.Clear();
                    TypeCombo.Items.Add("전기보일러");
                    break;
            }
            Boiler_dataGridView.Rows[nRow].Cells[5] = TypeCombo;
        }

        private void Boiler_Remove_button_Click(object sender, EventArgs e)
        {
            Boiler_dataGridView.Rows.Remove(Boiler_dataGridView.Rows[Boiler_SelectRow]);
            Load_Material_Num();
        }

        private void Boiler_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Boiler_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                Boiler_SelectRow = e.RowIndex;
                DataGridViewRow row = Boiler_dataGridView.Rows[Boiler_SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Boiler_dataGridView.RowCount; k++)
                {
                    if (k != row.Index)
                    {
                        Boiler_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = Boiler_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = SystemColors.Window;
                        row2.DefaultCellStyle.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = SystemColors.WindowText;
                        row = Boiler_dataGridView.Rows[e.RowIndex];
                    }
                }
            }
        }

        private void Load_Material_Num()
        {
            for (int k = 0; k < Boiler_dataGridView.RowCount; k++)
            {
                Boiler_dataGridView.Rows[k].Cells[1].Value = "UBS"+(k + 1).ToString();
            }
        }

        private void Boiler_Save_button_Click(object sender, EventArgs e)
        {
            for(int k = 0; k < Boiler_dataGridView.RowCount; k++)
            {
                Program.DB.deleteValue(DB.type.ProjDB, "User_Boiler", "번호 = '" + Boiler_dataGridView.Rows[k].Cells[1].Value.ToString()+"'");
                String[] Value = new String[10];
                for(int i = 0; i<10 ; i++)
                {
                    if (Boiler_dataGridView.Rows[k].Cells[i + 1].Value != null)
                    { Value[i] = Boiler_dataGridView.Rows[k].Cells[i + 1].Value.ToString(); }
                    else { Value[i] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_Boiler", "번호,명칭,난방급탕,연료,Type,용량,전부하효율,부분부하효율,소비전력,대기전력",
                "'" + Value[0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','"
                 + Value[9] 
                 + "'", "번호");
                
            }
            MessageBox.Show("저장되었습니다.");
        }
    }
}
