using main.contentslist;
using main.subcontents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace main.contents
{
    public partial class CoolingSystem : Form
    {
        List<string> CoolingGeneratorTypeSum = new List<string>();
        List<CoolingGenerator> CoolingGenerators = new List<CoolingGenerator>();
        String Num;
        string[] ZoneNameList;

        public CoolingSystem()
        {
            InitializeComponent();
            CoolingGeneratorTypes();
            ZoneListName.Clear();
            Program.DB.initTable(DB.type.ProjDB, "CoolingGeneratorType");
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void ZoneSelection_Click(object sender, EventArgs e) //존선택 창으로 이동함
        {
            if (String.IsNullOrEmpty(CoolingSystemTypeSelectCombobox.Text))
            {
                MessageBox.Show("먼저 냉방설비유형을 선택해주세요!");
            }
            else
            {
               // ZoneCooling ZC = new ZoneCooling(this);
             //   ZC.Show();
            }
        }

        private void CoolingGeneratorTypes() //냉방설비유형 콤보박스에 추가
        {
            try
            {
                string[][] _CoolingGeneratorTypes = Program.DB.getValue(DB.type.ProjDB, "CoolingGeneratorType", "냉방설비유형");//[행][열], "연습필드4", "연습필드3 = '4'");
                for (int i = 0; i < _CoolingGeneratorTypes.Length; i++)
                {
                    string _CoolingGeneratorTypeName = _CoolingGeneratorTypes[i][0];
                    CoolingGeneratorTypeSum.Add(_CoolingGeneratorTypeName);
                }

                foreach (var insert in CoolingGeneratorTypeSum)
                {
                    CoolingSystemTypeSelectCombobox.Items.Add(insert);
                }
            }
            catch { }

            
        }

        private void CoolingGeneratorSelectCombobox_SelectedIndexChanged(object sender, EventArgs e) //냉방설비유형 이미지 변경
        {
            string type = CoolingSystemTypeSelectCombobox.Text;
            CoolingGeneratordb(type);
            try
            {
                string[][] image = Program.DB.getValue(DB.type.ProjDB, "CoolingGeneratorType", "이미지", "냉방설비유형='" + type + "'");
                CoolingGeneratorImage.Load(Program.gPath + image[0][0]);
                CoolingGeneratorImage.SizeMode = PictureBoxSizeMode.Zoom;


                DataTable table = new DataTable();
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();
                DataGridViewComboBoxColumn comboBoxColumn_1 = new DataGridViewComboBoxColumn();
                DataGridViewComboBoxColumn comboBoxColumn_2 = new DataGridViewComboBoxColumn();
                CoolingGeneratorList.Columns.Clear();

                checkBoxColumn.HeaderText = "선택";
                checkBoxColumn.Name = "check";

                comboBoxColumn.HeaderText = "제어유형";
                comboBoxColumn.Name = "installlocaton";
                comboBoxColumn.Items.AddRange(new object[] { "인버터제어", "on/off제어" });

                comboBoxColumn_1.HeaderText = "설치위치";
                comboBoxColumn_1.Name = "installlocaton";
                comboBoxColumn_1.Items.AddRange(new object[] { "일사노출설치", "건물내부설치", "음영가리개 설치", "그늘설치" });

                comboBoxColumn_2.HeaderText = "외기냉방";
                comboBoxColumn_2.Name = "installlocaton";
                comboBoxColumn_2.Items.AddRange(new object[] { "있음", "없음" });

                CoolingGeneratorList.Columns.Add(checkBoxColumn);
                CoolingGeneratorList.Columns.Add(comboBoxColumn);
                CoolingGeneratorList.Columns.Add(comboBoxColumn_1);
                CoolingGeneratorList.Columns.Add(comboBoxColumn_2);
                table.Columns.Add("기호", typeof(string));
                table.Columns.Add("개수", typeof(string));
                table.Columns.Add("냉방출력" + Environment.NewLine + "[kW]", typeof(string));
                table.Columns.Add("소비전력" + Environment.NewLine + "[kW]", typeof(string));
                table.Columns.Add("냉방성능" + Environment.NewLine + "[EER]", typeof(string));
                table.Columns.Add("열원", typeof(string));


                foreach (CoolingGenerator kkk in CoolingGenerators)
                {
                    table.Rows.Add(kkk.unit, null, kkk.CoolingPower,
                        kkk.CoolingPowerConsumtion, kkk.EER, kkk.EnergyMedium);
                }

                CoolingGeneratorList.DataSource = table;


            }
            catch { }

        }


        public void LoadData(String ID)  // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
        }
        public static bool OnLoadListProc(Form form)
        {
            List_CoolingSystem f = (List_CoolingSystem)form;
            f.load_List();
            return true;
        }

        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {

            Num = ID;
        }

        public void CoolingZoneMaker()
        {

            ZoneNameList = ZoneListName.Text.Split(',');
            for (int i = 0; i < ZoneNameList.Length; i++)
            {
                MessageBox.Show(ZoneNameList[i]);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CoolingZoneMaker();
        }

        private void ZoneListName_TextChanged(object sender, EventArgs e) //존별 에너지 계산임
        {

        }



        public void CoolingGeneratordb(string type) // 냉방설비항목 작성
        {

            string[][] test = Program.DB.getValue(DB.type.ProjDB, "CGList", "기호,정격냉방,냉방소비전력,냉방성능표준,열원", "냉방설비유형='" + type + "'");//+ CoolingSystemTypeSelectCombobox.Text +
            for (int i = 0; i < test.Length; i++)
            {
                CoolingGenerator CG = new CoolingGenerator();
                CG.unit = test[i][0];
                CG.CoolingPower = Convert.ToDouble(test[i][1]);
                CG.CoolingPowerConsumtion = Convert.ToDouble(test[i][2]);
                CG.EER = Convert.ToDouble(test[i][3]);
                CG.EnergyMedium = test[i][4];
                CoolingGenerators.Add(CG);
            }

        }

        private void CoolingGeneratorList_CellContentClick(object sender, DataGridViewCellEventArgs e)//삭제해도됨
        {
        }

        private void CGdesideBtu_Click(object sender, EventArgs e)
        {
            double cp = 0;
            for (int j = 0; j < CoolingGeneratorList.Rows.Count; j++)
            {
                if (Convert.ToBoolean(CoolingGeneratorList.Rows[j].Cells[0].Value))

                {
                    string a = Convert.ToString(CoolingGeneratorList.Rows[j].Cells[1].Value);
                    string b = Convert.ToString(CoolingGeneratorList.Rows[j].Cells[2].Value);
                    string c = Convert.ToString(CoolingGeneratorList.Rows[j].Cells[3].Value);
                    if (a != "" && b != "" && c != "")
                    {
                        double p = Convert.ToDouble(CoolingGeneratorList.Rows[j].Cells[6].Value);
                        cp += p;

                    }
                    else
                    {
                        MessageBox.Show("선택항목을 완료해 주세요.");
                    }

                }
            }
            label15.Text = Convert.ToString(cp);

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
