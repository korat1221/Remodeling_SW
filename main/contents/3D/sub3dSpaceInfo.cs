namespace main.contents
{
    public partial class sub3dSpaceInfo : Form
    {
        string sid = "";
        public sub3dSpaceInfo()
        {
            InitializeComponent();
        }
        private void onVisibleChanged(object sender, EventArgs e)
        {
            if (main.MainContents.selID != sid)
            {
                sid = main.MainContents.selID;

                int n = Int32.Parse(main.MainContents.selID.Replace("space-", ""));
                String ID = n.ToString().PadLeft(3, '0');
                string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,존이름,용도프로필,순바닥면적,천장고", "존번호 LIKE '%Zone" + ID + "'");

                if (rec.Length > 0)
                {
                    Name_textBox.Text = rec[0][0];
                    Name_textBox1.Text = rec[0][1];
                    textBox2.Text = rec[0][2];
                    Area_textBox.Text = rec[0][3];
                    textBox1.Text = rec[0][4];
                }
                else
                {
                    Name_textBox.Visible = false;
                    Name_textBox1.Visible = false;
                    textBox2.Visible = false;
                    Area_textBox.Visible = false;
                    textBox1.Visible = false;

                    label6.Visible = false;
                    label14.Visible = false;
                    label2.Visible = false;
                    label1.Visible = false;
                    label3.Visible = false;

                }
            }
        }
    }
}
