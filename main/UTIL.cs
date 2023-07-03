using main.contents;
using System;
using System.Data;
using System.Xml.Linq;
using System.Xml.Schema;

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
        public String SelectedItem_ByComboBox(ComboBox comboBox)
        {
            String Value = "";
            DataRowView? item = comboBox.SelectedItem as DataRowView;

            if (item != null && item.Row.ItemArray.Length >= 3)
            {
               
                Value = item.Row.ItemArray[0].ToString();
            }

            return Value;
        }
        public String GetValue_BySelectComboBox(ComboBox comboBox, String 테이블명, String 선택컬럼명, String 찾는컬럼명)
        {
            String Value = "";
            DataRowView? item = comboBox.SelectedItem as DataRowView;

            if (item != null && item.Row.ItemArray.Length >= 3)
            {
                string[][] res = Program.DB.getValue(DB.type.BaseDB_HCneed, 테이블명, 찾는컬럼명, 선택컬럼명 + " = '" + item.Row.ItemArray[0].ToString() + "' ");
                Value = res[0][0].ToString();
            }

            return Value;
        }

      
        public String GetValue2_BySelectComboBox(ComboBox comboBox, String 테이블명, String 선택컬럼명,String 다른조건문, String 찾는컬럼명)
        {

            String Value = "";
            DataRowView? item = comboBox.SelectedItem as DataRowView;

              string[][] res = Program.DB.getValue(DB.type.BaseDB_HCneed, 테이블명, 찾는컬럼명, 선택컬럼명 +"= '" + comboBox.SelectedItem.ToString() + "' AND "+다른조건문);
                Value = res[0][0].ToString();
         

            return Value;
        }

        public void FillComboBox(DB.type dbType, ComboBox comboBox, string cate, string subcate, string def_value = "")
        {
            List<String> List = new List<String>();

            string[][] res = Program.DB.querySQL(dbType, "SELECT a.이름, a.값, a.아이디 FROM 인덱스 AS a INNER JOIN 인덱스분류 AS b ON a.종류=b.아이디 WHERE b.종류='" + cate + "' AND b.이름='" + subcate + "'");

            int i = -1;
            while (++i < res.Length)
            {
                List.Add(res[i][0]);
            }
            string[] ComboArray = List.ToArray();
            comboBox.Items.Clear();
            comboBox.Items.AddRange(ComboArray);
            if (def_value != "")
            {
                for (i = 0; i < comboBox.Items.Count; i++)
                {
                    if (ComboArray.Length > 1 && i+1 == Convert.ToInt32(def_value))
                    {
                        comboBox.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
        
         public void FillComboBox_Parents(ComboBox comboBox, string cate, string subcate, string def_value = "")
        {
            string[][] res = Program.DB.querySQL(DB.type.BaseDB_HCneed, "SELECT a.이름, a.값, a.아이디 FROM 인덱스 AS a INNER JOIN 인덱스분류 AS b ON a.종류=b.아이디 WHERE b.종류='" + cate + "' AND b.이름='" + subcate + "'");

            FillComboBox_Category(comboBox, res, def_value);
        }
         
        public void FillComboBox_ByComboBox(ComboBox comboBox, ComboBox comboBox0, string def_value = "")
        {
            DataRowView? item = comboBox0.SelectedItem as DataRowView;

            if (item != null && item.Row.ItemArray.Length >= 3)
            {
                string id = item.Row.ItemArray[2].ToString();

                if (id != "")
                {
                    string[][] res = Program.DB.querySQL(DB.type.BaseDB_HCneed, "SELECT 이름, 값, 아이디 FROM 인덱스 WHERE 부모아이디=" + id);

                    FillComboBox_Category(comboBox, res, def_value);
                }

            }
        }
        public void FillComboBox_Category(ComboBox comboBox, string[][] data, string def_value = "")
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
        public void SelectComboBox(ComboBox comboBox, string text)
        {
            int i = -1;

            for (i = 0; i < comboBox.Items.Count; i++)
            {
                var arr = ((DataRowView)comboBox.Items[i]).Row.ItemArray;
                if (arr.Length > 1 && arr[0].ToString() == text)
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }
        }
        public void reloadWebCtrl()
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FormMain")
                {
                    MainContents f = (MainContents)(((FormMain)openForm).splitContainer1.Panel1.Controls[0]);

                    f.refreshWebCtrl();
                    return;
                }
            }
        }
        public void resetMainTree(int idx, int sub_idx, object[] obj, string select_id)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FormMain")
                {
                    MainContents f = (MainContents)(((FormMain)openForm).splitContainer1.Panel1.Controls[0]);
                    string s = System.Text.Json.JsonSerializer.Serialize(obj);

                    f.runScript("resetMainTree(" + idx + "," + sub_idx + ",'" + s + "','" + select_id + "')");
                    return;
                }
            }
        }
        public void setObjInfo(string data)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FormMain")
                {
                    MainContents f = (MainContents)(((FormMain)openForm).splitContainer1.Panel1.Controls[0]);

                    f.runScript("setObjInfo(" + data + ")");
                    return;
                }
            }
        }

        private String getRandomString()
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var Charsarr = new char[8];
            var random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = characters[random.Next(characters.Length)];
            }

            return new String(Charsarr);
        }

        public void load3DModel(string path)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "Model")
                {
                    Model f = (Model)openForm;
                    string p = Program.ProjName + Path.GetExtension(path);
                    string path2 = Program.gPath + "threejs\\public\\models";

                    DirectoryInfo di = new DirectoryInfo(path2);  //Create Directoryinfo value by sDirPath  

                    if (di.Exists == false)   //If New Folder not exits  
                    {
                        di.Create();             //create Folder  
                    }

                    File.Delete(path2 + "\\" + p);
                    File.Copy(path, path2 + "\\" + p);

                    if (File.Exists(path2 + "\\" + p))
                    {
                        f.runScript("open3DModel('/models/" + p + "')");
                    }
                    return;
                }
            }
        }
        public void sendMessage(string msg)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "Model")
                {
                    Model f = (Model)openForm;
                    f.runScript("sendMessage('" + msg + "')");
                    return;
                }
            }
        }
        public void write3DModel(string fname, string data)
        {
            string path2 = Program.gPath + "threejs\\public\\models";

            DirectoryInfo di = new DirectoryInfo(path2);  //Create Directoryinfo value by sDirPath  

            if (di.Exists == false)   //If New Folder not exits  
            {
                di.Create();             //create Folder  
            }

            File.Delete(path2 + "\\" + fname);
            File.WriteAllText(path2 + "\\" + fname, data);
        }

        public string read3DModel(string fname)
        {
            string path2 = Program.gPath + "threejs\\public\\models";

            DirectoryInfo di = new DirectoryInfo(path2);  //Create Directoryinfo value by sDirPath  

            if (di.Exists == true && File.Exists(path2 + "\\" + fname))   //If New Folder not exits  
            {
                return File.ReadAllText(path2 + "\\" + fname);
            }
            return "";
        }

        public String CreateNum(String 테이블명,String 컬럼명,String 기호)
        {
            String ItemNum;
            int Num;
            try
            {
                string[][] Check = Program.DB.getValue(DB.type.ProjDB, 테이블명, 컬럼명, "");
                String[] NumCheck = new string[Check.Length];
                int[] SpNum = new int[Check.Length];
                for (int n = 0; n < Check.Length; n++)
                {
                    NumCheck[n] = (Check[n][0]);
                    SpNum[n] = Convert.ToInt32(NumCheck[n].Substring(NumCheck[n].IndexOf(기호.Substring(기호.Length -1)) + 1));
                }
                Num = SpNum.Max() + 1;

                if (Num < 1)
                {
                    Num = 1;
                    ItemNum = 기호 + "01";

                }
                else if (Num < 10)
                {
                    ItemNum = 기호 + "0" + Num;
                }
                else
                {
                    ItemNum = 기호  + Num;
                }

            }
            catch { ItemNum = 기호 + "01" ; }

            return ItemNum;
        }
    }
}
