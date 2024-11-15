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

namespace main.contents
{
    public partial class PreProjectCopy : Form
    {
        public string pid0;
        public List<string> tables = new List<string>();
        public bool model_copy = false;

        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        public PreProjectCopy()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            Building_pictureBox.Load(Program.gPath + "images/1sticon/1.Building_on.png");
            Building_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Construction_pictureBox.Load(Program.gPath + "images/1sticon/2.Construction_on.png");
            Construction_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Model_pictureBox.Load(Program.gPath + "images/1sticon/3.3D_on.png");
            Model_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Zone_pictureBox.Load(Program.gPath + "images/1sticon/4.Zone_on.png");
            Zone_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            System_pictureBox.Load(Program.gPath + "images/1sticon/5.System_on.png");
            System_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            drawList();
            Construction_checkBox.Enabled = Building_checkBox.Checked;
        }
        private void drawList()
        {
            dataGridView1.Rows.Clear();
            string[][] res = Program.DB.querySQL(DB.type.ProjListDB, "SELECT ID, pnum, title, type FROM projects WHERE type='1'");
            if (res.Length > 0)
            {
                for (int n = 0; n < res.Length; n++)
                {
                    int nRow = dataGridView1.Rows.Add();
                    for (int k = 0; k < 4; k++)
                    {
                        if (k == 3) { dataGridView1.Rows[nRow].Cells[k + 1].Value = "기존건물"; }
                        else { dataGridView1.Rows[nRow].Cells[k + 1].Value =res[n][k]; }
                    }

                    DataGridViewCheckBoxCell cell = dataGridView1.Rows[nRow].Cells[0] as DataGridViewCheckBoxCell;

                }
            }

        }
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            int k = GetSelectedIndex();
            if(k < 0)
            {
                MessageBox.Show("먼저 복사할 프로젝트부터 선택하세요.");
            }
            else
            {
                Construction_checkBox.Enabled = Building_checkBox.Checked;
                if (!Building_checkBox.Checked)
                {
                    Construction_checkBox.Checked = false;
                }

                Model_checkBox.Enabled = Construction_checkBox.Checked;
                if (!Construction_checkBox.Checked)
                {
                    Model_checkBox.Checked = false;
                }

                Zone_checkBox.Enabled = Model_checkBox.Checked;
                if (!Model_checkBox.Checked)
                {
                    Zone_checkBox.Checked = false;
                }

                System_checkBox.Enabled = Zone_checkBox.Checked;
                if (!Zone_checkBox.Checked)
                {
                    System_checkBox.Checked = false;
                }
            }            
        }
        private int GetSelectedIndex()
        {
            for (int k = 0; k < dataGridView1.Rows.Count; k++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[k].Cells[0].Value) == true)
                {
                    return k;
                }
            }
            return -1;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);

                for (int k = 0; k < dataGridView1.Rows.Count; k++)
                {
                    if (k != dataGridView1.CurrentCell.RowIndex)
                    {
                        dataGridView1.Rows[k].Cells[0].Value = false;
                    }
                    else
                    {
                        dataGridView1.Rows[k].Cells[0].Value = true;
                    }

                }
            }

        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            int k = GetSelectedIndex();
            if (k < 0)
            {
                MessageBox.Show("먼저 복사할 프로젝트부터 선택하세요.");
            }
            else
            {
                pid0 = dataGridView1.Rows[k].Cells[2].Value.ToString();
                if (Building_checkBox.Checked)
                {
                    tables.Add("BuildingGeneral");
                    tables.Add("BuildingEnergyUse");
                    tables.Add("BlowDoorTest");
                    tables.Add("User_PV");
                    tables.Add("User_PVInverter");
                    tables.Add("User_PVBattery");
                    tables.Add("User_FC");
                    tables.Add("User_WP");
                    tables.Add("User_Boiler");
                    tables.Add("User_AirHP");
                    tables.Add("User_GroundHP");
                    tables.Add("User_GroundWHP");
                    tables.Add("User_Pump");
                    tables.Add("User_ce");
                    tables.Add("User_Solar");
                    tables.Add("User_ABS");
                    tables.Add("User_DH");
                    tables.Add("User_AHU");
                    tables.Add("User_HRV");
                    tables.Add("User_DHWHP");
                    tables.Add("User_AirCooler");
                    tables.Add("User_WaterCooler");
                    tables.Add("User_CoolerTop");
                }
                if (Construction_checkBox.Checked)
                {
                    tables.Add("ConstructionCW");
                    tables.Add("ConstructionWall");
                    tables.Add("ConstructionRoof");
                    tables.Add("ConstructionFloor");
                    tables.Add("ConstructionWindow");
                    tables.Add("ConstructionDoor");
                    tables.Add("ConstructionBlind");
                    tables.Add("SubWindow");
                    tables.Add("Import_WindowSize");
                    tables.Add("Import_CWSize");
                    tables.Add("User_WindowFrame");
                    tables.Add("User_CWFrame");
                    tables.Add("User_Glass");
                    tables.Add("User_DoubleGlass");
                    tables.Add("User_WindowSpacer");
                    tables.Add("User_CWSpacer");
                    tables.Add("User_CWDoorFrame");
                    tables.Add("User_WindowInstall");
                    tables.Add("User_CWInstall");
                    tables.Add("User_Material");
                    tables.Add("User_Blind");
                    tables.Add("User_DoorInstall");
                }
                if (Model_checkBox.Checked)
                {
                    model_copy = true;
                    tables.Add("Blind_3D");
                    tables.Add("Shade_3D");
                    tables.Add("ZoneGeneral_3D");
                    tables.Add("ZoneEnvelope_3D");
                    tables.Add("ThermalBridge_3D");
                    tables.Add("User_TB");
                }
                if (Zone_checkBox.Checked)
                {
                    tables.Add("User_Lighting");
                    tables.Add("User_Renew");
                    tables.Add("ZoneGeneral_Form");
                    tables.Add("ZoneLighting_form");
                }
                if (System_checkBox.Checked)
                {
                    tables.Add("HeatingSystem_Form");
                    tables.Add("Heating_ce_Form");
                    tables.Add("CoolingSystem_Form");
                    tables.Add("Cooling_ce_Form");
                    tables.Add("DHWSystem_Form");
                    tables.Add("AHUSystem_Form");
                    tables.Add("PV_Form");
                    tables.Add("PV_Result");
                }

                this.DialogResult = DialogResult.OK;

                this.Close();
            }
            
        }

    }

}

