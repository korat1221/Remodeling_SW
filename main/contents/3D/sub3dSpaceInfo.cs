namespace main.contents
{
    public partial class sub3dSpaceInfo : Form
    {
        string sid = "";
        public sub3dSpaceInfo()
        {
            InitializeComponent(); this.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular);
        }
        private void onVisibleChanged(object sender, EventArgs e)
        {
            if (main.MainContents.selID != sid)
            {
                sid = main.MainContents.selID;

                int n = main.MainContents.selID.IndexOf("_Zone");

                if (n > -1)
                {
                    string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,존이름,용도프로필,순바닥면적,천장고", "존번호 LIKE '%Zone" + main.MainContents.selID.Substring(n + 5) + "'");

                    if (rec.Length > 0)
                    {
                        Name_textBox.Text = rec[0][0];
                        Name_textBox1.Text = rec[0][1];
                        textBox2.Text = rec[0][2];
                        Area_textBox.Text = rec[0][3];
                        textBox1.Text = rec[0][4];
                        return;
                    }
                }

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
