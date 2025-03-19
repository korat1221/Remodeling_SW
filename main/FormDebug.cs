
using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Security.Policy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace main
{
    public partial class FormDebug : Form
    {
        public FormDebug()
        {
            InitializeComponent();

            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, comboBox2, "커튼월", "프레임도어", "3");
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, comboBox1, "건물", "건물용도", "1");

            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;

            Program.UTIL.FillComboBox_ByComboBox(comboBox3, comboBox1, "3");
        }

        private void ComboBox1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Program.UTIL.FillComboBox_ByComboBox(comboBox3, comboBox1);
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
            if(TableName_textBox.Text!= null && ColumnName_textBox.Text!= null)
            {
                string directoryPath = Program.gPath + "projects"; // 대상 폴더
                string[] sqliteFiles = Directory.GetFiles(directoryPath, "*.sqlite"); // 모든 .sqlite 파일 검색

                foreach (string dbPath in sqliteFiles)
                {
                    Program.DB.UpdateDatabase(dbPath, TableName_textBox.Text.ToString(), ColumnName_textBox.Text.ToString());
                }

                MessageBox.Show("모든 SQLite 데이터베이스 업데이트 완료!", "업데이트 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("컬럼을 추가할 테이블 명칭과 컬럼명칭을 작성하세요.");
            }
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void OnGormShown(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
         
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Program.DB.UseCaches(true);
            CALC.run(new string[] { "요소기술계산" });
            Program.DB.UseCaches(false);
            MessageBox.Show("요소기술별 계산이 완료되었습니다.");
        }
    }
}