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

namespace main.subcontents.HeatingSystem
{
    public partial class Heating_Zone : Form
    {
        double Count_DB;
        ArrayList SelectRow = new ArrayList();
        String SystemNum;

        public Heating_Zone(String Num)
        {
            InitializeComponent();
            load_table_DB();
            SystemNum = Num;
        }

        void load_table_DB()
        {
            DataTable table_Zone = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Zone_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Zone_dataGridView.Columns.Add(checkBoxColumn);
            table_Zone.Columns.Add("번호", typeof(string));
            table_Zone.Columns.Add("층", typeof(string));
            table_Zone.Columns.Add("존 명칭", typeof(string));
            table_Zone.Columns.Add("용도프로필", typeof(string));
            table_Zone.Columns.Add("연간 난방요구량", typeof(string));
            table_Zone.Columns.Add("면적", typeof(string));
            
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,존이름,용도프로필,순바닥면적", "냉난방유무 ='냉난방' OR 냉난방유무 = '난방'");
            
            for (int n = 0; n < Value.Length; n++)
                {
                string[][] 층 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "층", "존 ='" + Value[n][0] + "'");
                string[][] 요구량 = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed", "Qhb_a", "번호 ='"+Value[n][0]+"'");
                table_Zone.Rows.Add(Value[n][0], 층[0][0], Value[n][1], Value[n][2], string.Format("{0:F2}", Convert.ToDouble(요구량[0][0])), string.Format("{0:F1}", Convert.ToDouble(Value[n][3])));
                Count_DB = Value.Length;
                }

            Zone_dataGridView.DataSource = table_Zone;
        }


        private void SelectCheckBox()
        {
            foreach (DataGridViewRow row in Zone_dataGridView.Rows)
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

            String[][] Size = Program.DB.getValue(DB.type.ProjDB, "ZoneHeatingSystem_Form", "존번호, 난방시스템", "난방시스템 = '" + SystemNum + "'");
            if (Size.Length > 0)
            {
                Program.DB.deleteValue(DB.type.ProjDB, "ZoneHeatingSystem_Form", "난방시스템 = '" + SystemNum + "'");
            }
            for (int n = 0; n < SelectRow.Count; n++)
            {
                DataGridViewRow row = Zone_dataGridView.Rows[Convert.ToInt32(SelectRow[n])];               

                Program.DB.setValue(DB.type.ProjDB, "ZoneHeatingSystem_Form", "존번호,난방시스템",
                "'" + row.Cells[1].Value.ToString() + "','" + SystemNum + "'","");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

    }
}
