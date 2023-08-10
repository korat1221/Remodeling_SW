using main.contents;
using main.subcontents.EquipmentList;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace main.subcontents.HeatingSystem
{
    public partial class Heating_ceZone : Form
    {
        ArrayList SelectRow = new ArrayList();
        public string SelectBoiler;
        String SystemNum, ceType;
        public Heating_ceZone(String SystemNum, String CEType)
        {
            InitializeComponent();
            this.SystemNum = SystemNum;
            ceType = CEType;
            ceType_textBox.Text = ceType;
            load_table_DB();
           
            Icon_pictureBox.Load(Program.gPath + "images/1sticon/4.Zone_on3.png");
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

        }

        void load_table_DB()
        {
            DataTable ceZone_table = new DataTable();
            ceZone_dataGridView.Columns.Clear();
            ceZone_dataGridView.ColumnCount = 8;
            ceZone_dataGridView.Columns[0].HeaderText = "번호";
            ceZone_dataGridView.Columns[1].HeaderText = "존 번호";
            ceZone_dataGridView.Columns[2].HeaderText = "존 명칭";
            ceZone_dataGridView.Columns[3].HeaderText = "층";
            ceZone_dataGridView.Columns[4].HeaderText = "용도프로필"+ Environment.NewLine + "[EA]";
            ceZone_dataGridView.Columns[5].HeaderText = ceType + "대수" + Environment.NewLine + "[EA]";
            ceZone_dataGridView.Columns[6].HeaderText = "적용";
            ceZone_dataGridView.Columns[7].HeaderText = ceType + "종류";

            String[][] ZoneValve = Program.DB.querySQL(DB.type.ProjDB, "select b.존번호,b.존이름,용도프로필 FROM ZoneSystem_Form  AS a INNER JOIN  ZoneGeneral_Form AS b ON a.존번호 = b.존번호 where a.난방시스템 = '" + SystemNum + "'");
            for (int n = 0; n < ZoneValve.Length; n++)
            {
                ceZone_dataGridView.Rows.Add();
                String[][] 층 = Program.DB.getValue_dedupe(DB.type.ProjDB, "ZoneEnvelope_3D", "층", "존 = '" + ZoneValve[n][0] + "'");
                ceZone_dataGridView.Rows[n].Cells[0].Value = n + 1; //존번호
                ceZone_dataGridView.Rows[n].Cells[1].Value = ZoneValve[n][0]; //존번호
                ceZone_dataGridView.Rows[n].Cells[2].Value = ZoneValve[n][1]; //존이름
                ceZone_dataGridView.Rows[n].Cells[3].Value = 층[0][0]; //층
                ceZone_dataGridView.Rows[n].Cells[4].Value = ZoneValve[n][2]; //용도프로필
                ceZone_dataGridView.Rows[n].Cells[5].Style.BackColor = SystemColors.Info; //대수

                DataGridViewButtonCell ButtonCell = new DataGridViewButtonCell(); 
                ceZone_dataGridView.Rows[n].Cells[6] = ButtonCell;//적용 
                ButtonCell.Value = "+";
            }

        }
        private void ceZone_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 6)
                {
                    int sum = 0;
                    for(int i = 0; i< ceZone_dataGridView.Rows.Count; i++)
                    {
                        int index = ceZone_dataGridView.Rows[i].Cells[0].Value.ToString().IndexOf("_");
                        if(index >0)
                        {
                            String substring = ceZone_dataGridView.Rows[i].Cells[0].Value.ToString().Substring(0, index);

                            if (substring == ceZone_dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString())
                            {
                                sum += 1;
                            }
                        }
                    }
                    if(sum  > 0)
                    {
                        if ((MessageBox.Show("입력하신 " + ceZone_dataGridView.Rows[e.RowIndex].Cells[1].Value + "의 공급설비 정보를 리셋하시겠습니까?", ceZone_dataGridView.Rows[e.RowIndex].Cells[1].Value + " 공급설비 정보 리셋", MessageBoxButtons.YesNo) == DialogResult.Yes))
                        {
                            for (int i = 0; i < sum; i++)
                            {
                                ceZone_dataGridView.Rows.RemoveAt(e.RowIndex + 1);
                            }
                        }
                        else { return; }
                    }

                    int 대수 = Convert.ToInt16(ceZone_dataGridView.Rows[e.RowIndex].Cells[5].Value);
                    for(int k = (대수 -1); k > -1;  k--)
                    {
                        ceZone_dataGridView.Rows.Add();
                        int AddRowNum = ceZone_dataGridView.Rows.Count - 1;
                        ceZone_dataGridView.Rows[AddRowNum].Cells[0].Value = ceZone_dataGridView.Rows[e.RowIndex].Cells[0].Value + "_" + (k + 1).ToString();

                        DataGridViewComboBoxCell 일람표comboBox = new DataGridViewComboBoxCell();
                        try
                        {
                            String[][] 일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "명칭", "종류 = '" + ceType + "' AND 난방냉방 !='냉방'");
                            for (int j = 0; j < 일람표.Length; j++)
                            {
                                일람표comboBox.Items.Add(일람표[j][0]);
                            }
                        }
                        catch { }
                        ceZone_dataGridView.Rows[AddRowNum].Cells[7] = 일람표comboBox;


                        DataGridViewRow AddRow = ceZone_dataGridView.Rows[AddRowNum];
                        ceZone_dataGridView.Rows.RemoveAt(AddRowNum);
                        ceZone_dataGridView.Rows.Insert(e.RowIndex + 1, AddRow);

                       
                    }
                }
            }
        }

        private void ceZone_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                
            }
        }


        private void Save_button_Click(object sender, EventArgs e)
        {
            SelectRow.Clear();
            for (int k = 0; k < SelectRow.Count; k++)
            {
                if (k == SelectRow.Count - 1)
                {
                    SelectBoiler += ceZone_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString();
                }
                else
                {
                    SelectBoiler += ceZone_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString() + ",";
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
