using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    internal class UTIL
    {
        public void trim(string[] arr)
        {
            int i = -1;

            while(++i < arr.Length)
            {
                arr[i] = arr[i].Trim();
            }
        }


       public String GetValue_BySelectComboBox(ComboBox comboBox, String 테이블명, String 선택컬럼명, String 찾는컬럼명)
        {
            String Value = "";
            DataRowView? item = comboBox.SelectedItem as DataRowView;

            if (item != null && item.Row.ItemArray.Length >= 3)
            {
                string[][] res = Program.DB.getValue(DB.type.BaseDB, 테이블명, 찾는컬럼명, 선택컬럼명 + " = '" + item.Row.ItemArray[0].ToString() + "' ");
                Value = res[0][0].ToString();
            }

            return Value;
        }
      
        public String GetValue2_BySelectComboBox(ComboBox comboBox, String 테이블명, String 선택컬럼명,String 다른조건문, String 찾는컬럼명)
        {

            String Value = "";
            DataRowView? item = comboBox.SelectedItem as DataRowView;

               if (item != null && item.Row.ItemArray.Length >= 3)
             {
              string[][] res = Program.DB.getValue(DB.type.BaseDB, 테이블명, 찾는컬럼명, 선택컬럼명 +"= '" + item.Row.ItemArray[0].ToString() + "' AND "+다른조건문);
                Value = res[0][0].ToString();
            }

            return Value;
        }

        public void FillComboBox_ByCategory(ComboBox comboBox, string cate, string subcate, string def_value = "")
        {
            string[][] res = Program.DB.querySQL(DB.type.BaseDB, "SELECT a.이름, a.값, a.아이디 FROM 인덱스 AS a INNER JOIN 인덱스분류 AS b ON a.종류=b.아이디 WHERE b.종류='" + cate + "' AND b.이름='" + subcate + "'");

            FillComboBox(comboBox, res, def_value);
        }

        public void FillComboBox_ByComboBox(ComboBox comboBox, ComboBox comboBox0, string def_value = "")
        {
            DataRowView? item = comboBox0.SelectedItem as DataRowView;

            if (item != null && item.Row.ItemArray.Length >= 3)
            {
                string id = item.Row.ItemArray[2].ToString();

                if (id != "")
                {
                    string[][] res = Program.DB.querySQL(DB.type.BaseDB, "SELECT 이름, 값, 아이디 FROM 인덱스 WHERE 부모아이디=" + id);

                    FillComboBox(comboBox, res, def_value);
                }

            }
        }

        public void FillComboBox(ComboBox comboBox, string[][] data, string def_value = "")
        {
            int i = -1;
            DataTable sources = new DataTable();

            sources.Columns.Add("Text");
            sources.Columns.Add("Value");
            sources.Columns.Add("ID");

            while (++i < data.Length)
            {
                DataRow dr = sources.NewRow();
                dr["Text"] = data[i][0];
                dr["Value"] = data[i][1];
                dr["ID"] = data[i][2];
                sources.Rows.Add(dr);
            }

            comboBox.DataSource = sources.DefaultView;

            comboBox.DisplayMember = "Text";
            comboBox.ValueMember = "Value";

            if (def_value != "")
            {
                for (i = 0; i < comboBox.Items.Count; i++)
                {
                    var arr = ((DataRowView)comboBox.Items[i]).Row.ItemArray;
                    if (arr.Length > 1 && arr[1].ToString() == def_value)
                    {
                        comboBox.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
    }
}
