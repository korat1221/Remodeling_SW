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
    public partial class ProjectCopy : Form
    {
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

        public ProjectCopy()
        {
            InitializeComponent();
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
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (Building_checkBox.Checked)
            {
                tables.Add("BuildingGeneral");
            }
            if (Construction_checkBox.Checked)
            {
                tables.Add("ConstructionWall");
                tables.Add("ConstructionCW");
                tables.Add("ConstructionWindow");
                tables.Add("ConstructionFloor");
                tables.Add("ConstructionRoof");
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
                tables.Add("ConstructionBlind");
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
                tables.Add("User_PVModule");
                tables.Add("User_PVInverter");
                tables.Add("User_PVBattery");
                tables.Add("User_FC");
                tables.Add("User_WP");
                tables.Add("HeatingSystem_Form");
                tables.Add("Heating_ce_Form");
                tables.Add("User_Boiler");
                tables.Add("User_AirHP");
                tables.Add("User_GroundHP");
                tables.Add("User_GroundWHP");
                tables.Add("User_Pump");
                tables.Add("User_ce");
                tables.Add("User_Solar");
                tables.Add("User_ABS");
                tables.Add("User_DH");
                tables.Add("Cooling_ce_Form");
                tables.Add("CoolingZone");
                tables.Add("User_CoolingSystem");
                tables.Add("User_AirCon");
                tables.Add("User_AirCooler");
                tables.Add("User_WaterCooler");
                tables.Add("User_AbsorbCooler");
                tables.Add("User_SoilCooler");
                tables.Add("User_CoolerTop");
                tables.Add("DHWSystem_Form");
            }

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
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

}

