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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents.ConstructionWall
{
    public partial class Heating_Boiler : Form
    {
        ArrayList SelectRow = new ArrayList();
        String DefaultUse;
        public string SelectBoiler;
        //HeatingSystem heatingSystem;

      // public Heating_Boiler(HeatingSystem system)
        public Heating_Boiler(String DefaultUse)
        {
            InitializeComponent();
            //heatingSystem = system;
            this.DefaultUse =DefaultUse;
            load_table_DB();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

        }

        void load_table_DB()
        {
            DataTable Boiler_table = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Boiler_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Boiler_dataGridView.Columns.Add(checkBoxColumn);
            Boiler_table.Columns.Add("번호", typeof(string));
            Boiler_table.Columns.Add("명칭", typeof(string));
            Boiler_table.Columns.Add("연료", typeof(string));
            if(DefaultUse != "기본DB 적용")
            {
                Boiler_table.Columns.Add("Type", typeof(string));
                Boiler_table.Columns.Add("용량" + Environment.NewLine + "[kW]", typeof(string));
            }           
            Boiler_table.Columns.Add("전부하효율" + Environment.NewLine + "[%]", typeof(string));
            Boiler_table.Columns.Add("부분부하효율" + Environment.NewLine + "[%]", typeof(string));
            Boiler_table.Columns.Add("소비전력" + Environment.NewLine + "[W]", typeof(string));
            Boiler_table.Columns.Add("대기전력" + Environment.NewLine + "[W]", typeof(string));

            if (DefaultUse == "기본DB 적용")
            {
                string[][] DefaultDB_Value = Program.DB.getValue(DB.type.BaseDB_Heating, "보일러", "번호,제품명,연료,전부하효율,부분부하효율,소비전력,대기전력", "");
                for (int n = 0; n < DefaultDB_Value.Length; n++)
                {
                    Boiler_table.Rows.Add(DefaultDB_Value[n][0], DefaultDB_Value[n][1], DefaultDB_Value[n][2], (Convert.ToDouble(DefaultDB_Value[n][3]) * 100).ToString(), (Convert.ToDouble(DefaultDB_Value[n][4]) * 100).ToString(), string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[n][5])), string.Format("{0:F0}", Convert.ToDouble(DefaultDB_Value[n][6])));
                }
            }
            else
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "번호,명칭,연료,Type,용량,전부하효율,부분부하효율,소비전력,대기전력", "난방급탕 ='난방' OR 난방급탕 = '난방+급탕'");
                for (int n = 0; n < User_Value.Length; n++)
                {
                    string 용량 = "", 전부하효율 = "", 부분부하효율 = "", 소비전력 = "", 대기전력 = "";
                    if (User_Value[n][4] != null && User_Value[n][4] != "")
                    {
                        용량 = string.Format("{0:F1}", Convert.ToDouble(User_Value[n][4]));
                    }
                    if (User_Value[n][5] != null && User_Value[n][5] != "")
                    {
                        전부하효율 = string.Format("{0:F1}", Convert.ToDouble(User_Value[n][5]));
                    }
                    if (User_Value[n][6] != null && User_Value[n][6] != "")
                    {
                        부분부하효율 = string.Format("{0:F1}", Convert.ToDouble(User_Value[n][6]));
                    }
                    if (User_Value[n][7] != null && User_Value[n][7] != "")
                    {
                        소비전력 = string.Format("{0:F0}", Convert.ToDouble(User_Value[n][7]));
                    }
                    if (User_Value[n][8] != null && User_Value[n][8] != "")
                    {
                        대기전력 = string.Format("{0:F0}", Convert.ToDouble(User_Value[n][8]));
                    }
                    Boiler_table.Rows.Add(User_Value[n][0], User_Value[n][1], User_Value[n][2], User_Value[n][3], 용량, 전부하효율, 부분부하효율, 소비전력, 대기전력);
                }
            }
            Boiler_dataGridView.DataSource = Boiler_table;
        }


        private void SelectCheckBox()
        {
            foreach (DataGridViewRow row in Boiler_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
                    SelectRow.Add(row.Index);
                }
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            SelectRow.Clear();
            SelectCheckBox();
            for (int k = 0; k < SelectRow.Count; k++)
            {
                if(k == SelectRow.Count-1)
                {
                    SelectBoiler += Boiler_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString();
                }
                else
                {
                    SelectBoiler += Boiler_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString() + ",";                   
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
