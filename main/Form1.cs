
using System;
using System.IO;
using System.Security.Policy;

namespace main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            //            main.Program.killServer();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var table = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);

            StreamReader sr = new StreamReader(openFileDialog1.FileName);

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] data = line.Split(',');

                // 결과를 출력해본다.

                //                Console.WriteLine("{0}, {1}, {2}, ... ", data[0], data[1], data[2], ... );

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.DB.setValue(DB.type.ProjDB, "연습테이블3", "연습필드5,연습필드6", "'4','3333'", "연습필드5");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[][] res = Program.DB.getValue(DB.type.ProjDB, "연습테이블3", "연습필드6", "연습필드5 = '4'");

            if (res.Count() > 0 && res[0].Count() > 0)
            {
                MessageBox.Show(res[0][0]);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Program.CALC.run(new string[] { "셈플: CSV 를 메모리DB에 로딩..." });
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Program.CALC.run(new string[] {
                "존 HT",
                "존 HV",
                "존 tao",
                "존 thetai",
                "존 QT",
                "존 QV",
                "존 QSop",
                "존 QStr",
                "존 QI",
                "존 eta",
                "존 Qb"
            });
        }
    }
}