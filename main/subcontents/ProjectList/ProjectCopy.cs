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
    public partial class ProjectCopy : Form
    {
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

    }

}

