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

namespace main.subcontents.HeatingSystem
{
    public partial class Heating_Pump: Form
    {
        
        public string SelectPump;

        public Heating_Pump()
        {
            InitializeComponent();
            //heatingSystem = system;
            load_table_DB();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

        }

        void load_table_DB()
        {
            DataTable Pump_table = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Pump_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Pump_dataGridView.Columns.Add(checkBoxColumn);
            Pump_table.Columns.Add("번호", typeof(string));
            Pump_table.Columns.Add("명칭", typeof(string));
            Pump_table.Columns.Add("종류", typeof(string));           
            Pump_table.Columns.Add("A효율" + Environment.NewLine + "[%]", typeof(string));
            Pump_table.Columns.Add("B효율" + Environment.NewLine + "[%]", typeof(string));
            Pump_table.Columns.Add("유량" + Environment.NewLine + "[CMH]", typeof(string));
            Pump_table.Columns.Add("동력" + Environment.NewLine + "[kW]", typeof(string));
            Pump_table.Columns.Add("양정" + Environment.NewLine + "[m]", typeof(string));

         
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "번호,명칭,종류,A효율,B효율,유량,동력,양정", "종류 ='온수순환펌프' OR 종류 = '냉온수순환펌프'");
                for (int n = 0; n < User_Value.Length; n++)
                {
                    string A효율 = "", B효율 = "", 유량 = "", 동력 = "", 양정 = "";             
                    if (User_Value[n][3] != null && User_Value[n][3] != "")
                    {
                        A효율 = string.Format("{0:F1}", Convert.ToDouble(User_Value[n][3]));
                    }
                    if (User_Value[n][4] != null && User_Value[n][4] != "")
                    {
                        B효율 = string.Format("{0:F1}", Convert.ToDouble(User_Value[n][4]));
                    }
                    if (User_Value[n][5] != null && User_Value[n][5] != "")
                    {
                       유량 = string.Format("{0:F0}", Convert.ToDouble(User_Value[n][5]));
                    }
                    if (User_Value[n][6] != null && User_Value[n][6] != "")
                    {
                        동력 = string.Format("{0:F0}", Convert.ToDouble(User_Value[n][6]));
                    }
                    if (User_Value[n][7] != null && User_Value[n][7] != "")
                    {
                        양정 = string.Format("{0:F0}", Convert.ToDouble(User_Value[n][7]));
                    }
                Pump_table.Rows.Add(User_Value[n][0], User_Value[n][1], User_Value[n][2], A효율, B효율, 유량, 동력, 양정);
            }
            
            Pump_dataGridView.DataSource = Pump_table;
        }



        private void Save_button_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in Pump_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;

                    SelectPump = Pump_dataGridView.Rows[Convert.ToInt16(row.Index)].Cells[1].Value.ToString();
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
