namespace main.contents
{
    public partial class sub3dSpaceInfo : Form
    {
        public sub3dSpaceInfo()
        {
            InitializeComponent();
        }
        private void onVisibleChanged(object sender, EventArgs e)
        {
            int n = Int32.Parse(main.MainContents.selID.Replace("space-", "")) + 1;
            String ID = n.ToString().PadLeft(3, '0');
            string[][] rec = Program.DB.getValue(DB.type.ProjDB, "Zonegeneral_3D", "주광너비,주광깊이,상인방높이,바닥면적,존번호", "존번호 LIKE '%Zone" + ID + "'");

            if (rec.Length > 0)
            {
                textBox1.Text = rec[0][4];
                textBox4.Text = rec[0][3] == "0" ? rec[0][3] : Double.Parse(rec[0][3]).ToString("#.##");
            }
        }
    }
}
