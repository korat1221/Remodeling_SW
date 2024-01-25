using main.contents;
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
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents.HeatingSystem
{
    public partial class DoorDefault : Form
    {

        public string 문짝종류,문짝내부,문틀내부,문틀상부,문틀하부;
        ArrayList SelectPump_split = new ArrayList();
        public DoorDefault()
        {
            InitializeComponent();
            //heatingSystem = system;
            load_table_DB();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '외부출입문'");
            if(Image.Length > 0 )
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        void load_table_DB()
        {
            new StackedHeaderDecorator(Door_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            // DataTable Pump_table = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Door_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Door_dataGridView.Columns.Add(checkBoxColumn);
            Door_dataGridView.Columns.Add("A1", "번호");
            Door_dataGridView.Columns.Add("A2", "제품명");
            Door_dataGridView.Columns.Add("A3", "제조사");
            Door_dataGridView.Columns.Add("A4", "문짝.종류"); //콤보박스
            Door_dataGridView.Columns.Add("A5", "문짝.내부"); // 콤보박스
            Door_dataGridView.Columns.Add("A6", "문틀.내부"); //콤보박스
            Door_dataGridView.Columns.Add("A7", "문틀.상부열관류율.[W/m∙K]");
            Door_dataGridView.Columns.Add("A8", "문틀.하부열관류율.[W/m∙K]");
            Door_dataGridView.Columns[0].Width = 50;

            string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "외부출입문", "번호,제품명,제조사,문짝종류,문짝내부,문틀내부,문틀상부열관류율,문틀하부열관류율", "");
            if(Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {

                    int nRow = Door_dataGridView.Rows.Add();
                    Door_dataGridView.Rows[nRow].Cells[1].Value = Value[n][0];
                    Door_dataGridView.Rows[nRow].Cells[2].Value = Value[n][1];
                    Door_dataGridView.Rows[nRow].Cells[3].Value = Value[n][2];
                    Door_dataGridView.Rows[nRow].Cells[4].Value = Value[n][3];
                    Door_dataGridView.Rows[nRow].Cells[5].Value = Value[n][4];
                    Door_dataGridView.Rows[nRow].Cells[6].Value = Value[n][5];
                    Door_dataGridView.Rows[nRow].Cells[7].Value = Value[n][6];
                    Door_dataGridView.Rows[nRow].Cells[8].Value = Value[n][7];
                }
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in Door_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {

                    문짝종류 = Door_dataGridView.Rows[Convert.ToInt16(row.Index)].Cells[4].Value.ToString();
                    문짝내부 = Door_dataGridView.Rows[Convert.ToInt16(row.Index)].Cells[5].Value.ToString();
                    문틀내부 = Door_dataGridView.Rows[Convert.ToInt16(row.Index)].Cells[6].Value.ToString();
                    문틀상부 = Door_dataGridView.Rows[Convert.ToInt16(row.Index)].Cells[7].Value.ToString();
                    문틀하부 = Door_dataGridView.Rows[Convert.ToInt16(row.Index)].Cells[8].Value.ToString();
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }       
    }
}
