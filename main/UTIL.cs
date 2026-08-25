using main.contents;
using System.Data;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace main
{
    internal class UTIL
    {
        int NumberDecimal = 0;
        private TextBox textBox;
        private DataGridView dataGridView;
        public double textdouble = 0;
        public bool fromCode = false;
        public bool ffCode = false;
        private static UTIL inst = new UTIL();
        public PrivateFontCollection privateFont = new PrivateFontCollection();
        public UTIL()
        {
            AddFontFromMemory();
        }
        // DB.getValue() 등이 빈 문자열/null을 돌려줄 수 있는데 Convert.ToDouble("")은 예외를 던지므로,
        // 파싱 실패 시 예외 대신 0을 반환하는 안전한 버전. 프로젝트 전체에서 Convert.ToDouble 대신 이걸 쓴다.
        public double ToDoubleOrZero(object value)
        {
            return double.TryParse(value?.ToString(), out double result) ? result : 0;
        }
        // 옛날 프로젝트 데이터에 저장된 값이 지금의 콤보박스 목록에 없으면(DB 값 이름 변경, 컬럼 나중 추가 등)
        // DataGridViewComboBoxCell이 ArgumentException을 던져서 기본 오류 대화상자가 뜬다.
        // root 안의 모든 DataGridView에 대해 그 예외를 무시하고 넘어가도록 DataError를 걸어준다.
        public void IgnoreGridError(Control root)
        {
            if (root is DataGridView grid)
            {
                grid.DataError += (s, e) => { e.ThrowException = false; };
            }
            for (int i = 0; i < root.Controls.Count; i++)
            {
                IgnoreGridError(root.Controls[i]);
            }
        }
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
        public String Subscript(int num, bool SuperSub)
        {
            //SuperSub 위첨자 : true, 아래첨자 : false
            String[][] Uni;
            string superscript = "";
            if (SuperSub)
            {
                Uni = Program.DB.getValue(DB.type.BaseDB_HCneed, "첨자유니코드", "숫자_위첨자", "숫자 = '" + num.ToString() + "'");
                if (Uni.Length > 0)
                {
                    superscript = Regex.Unescape(Uni[0][0]);
                }
            }
            else
            {
                Uni = Program.DB.getValue(DB.type.BaseDB_HCneed, "첨자유니코드", "숫자_아래첨자", "숫자 = '" + num.ToString() + "'");
                if (Uni.Length > 0)
                {
                    superscript = Regex.Unescape(Uni[0][0]);
                }
            }

            return superscript;           
        }
        public String Subscript(string alphabet, bool SuperSub)
        {
            //SuperSub 위첨자 : true, 아래첨자 : false
            String[][] Uni;
            string superscript = "";
            if (SuperSub)
            {
                Uni = Program.DB.getValue(DB.type.BaseDB_HCneed, "첨자유니코드", "소문자_위첨자", "소문자 = '" + alphabet.ToString() + "'");
                if (Uni.Length > 0)
                {
                    superscript = Regex.Unescape(Uni[0][0]);
                }
                else
                {
                    Uni = Program.DB.getValue(DB.type.BaseDB_HCneed, "첨자유니코드", "대문자_위첨자", "대문자 = '" + alphabet.ToString() + "'");
                    if (Uni.Length > 0)
                    {
                        superscript = Regex.Unescape(Uni[0][0]);
                    }
                }
            }
            else
            {
                Uni = Program.DB.getValue(DB.type.BaseDB_HCneed, "첨자유니코드", "소문자_아래첨자", "소문자 = '" + alphabet.ToString() + "'");
                if (Uni.Length > 0)
                {
                    superscript = Regex.Unescape(Uni[0][0]);
                }
                else
                {
                    Uni = Program.DB.getValue(DB.type.BaseDB_HCneed, "첨자유니코드", "대문자_아래첨자", "대문자 = '" + alphabet.ToString() + "'");
                    if (Uni.Length > 0)
                    {
                        superscript = Regex.Unescape(Uni[0][0]);
                    }
                }
            }

            return superscript;
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
        public void selectWall(string sid)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FormMain")
                {
                    MainContents f = (MainContents)(((FormMain)openForm).splitContainer1.Panel1.Controls[0]);

                    f.runScript("selectWall('" + sid + "')");
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
        public void unselectAll()
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FormMain")
                {
                    MainContents f = (MainContents)(((FormMain)openForm).splitContainer1.Panel1.Controls[0]);

                    f.runScript("unselectAll()");
                    return;
                }
            }
        }
        public void setObjInfo(string pid)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FormMain")
                {
                    MainContents f = (MainContents)(((FormMain)openForm).splitContainer1.Panel1.Controls[0]);

                    f.runScript("setObjInfo('" + pid + "')");
                    return;
                }
            }
        }
        public void loadMainMenu(int idx)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FormMain")
                {
                    MainContents f = (MainContents)(((FormMain)openForm).splitContainer1.Panel1.Controls[0]);

                    f.runScript("load(" + idx + ")");
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
                    string p = ProjectList.CurProjID + Path.GetExtension(path);

                    File.Delete(Program.gPath + "projects\\" + p);
                    File.Copy(path, Program.gPath + "projects\\" + p);

                    if (File.Exists(Program.gPath + "projects\\" + p))
                    {
                        Program.DB.deleteValue(DB.type.ProjDB, "Blind_3D", "");
                        Program.DB.deleteValue(DB.type.ProjDB, "Shade_3D", "");
                        Program.DB.deleteValue(DB.type.ProjDB, "ThermalBridge_3D", "");
                        Program.DB.deleteValue(DB.type.ProjDB, "ZoneGeneral_Form", "");
                        Program.DB.deleteValue(DB.type.ProjDB, "ZoneLighting_form", "");
                        f.CalculateModel(true);
                    }
                    return;
                }
            }
        }
        public void ReloadModel()
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "Model")
                {
                    Model f = (Model)openForm;
                    f.Reload();
                    return;
                }
            }
        }

        public void modelScript(string scr)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "Model")
                {
                    Model f = (Model)openForm;
                    f.runScript(scr);
                    return;
                }
            }
        }
        public void select3DObject(string msg)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "Model")
                {
                    Model f = (Model)openForm;
                    f.runScript("selectObject('" + msg + "')");
                    return;
                }
            }
        }
        public void write3DModel(string data)
        {
            string fname = "model.json";
            string path2 = Program.gPath + "threejs\\public\\models\\" + ProjectList.CurProjID;

            DirectoryInfo di = new DirectoryInfo(path2);  //Create Directoryinfo value by sDirPath  

            if (di.Exists == false)   //If New Folder not exits  
            {
                di.Create();             //create Folder  
            }

            File.Delete(path2 + "\\" + fname);
            File.WriteAllText(path2 + "\\" + fname, data);
        }
        public string doubleComa(string s, int NumberDecimal)
        {
            s = s.Trim();
            return s != "" ? Program.UTIL.ToDoubleOrZero(s).ToString(NumberDecimalPlaces(NumberDecimal, Program.UTIL.ToDoubleOrZero(s))) : "0";
        }
        private string NumberDecimalPlaces(int a, double Value)
        {
            string code = "";
            if (Value < 1)
            {
                if (a == 0)
                {
                    code = "0";
                }
                else if (a == 1)
                {
                    code = "0.0";
                }
                else if (a == 2)
                {
                    code = "0.00";
                }
                else
                {
                    code = "0.000";
                }
            }
            else
            {
                if (a == 0)
                {
                    code = "#,##0";
                }
                else if (a == 1)
                {
                    code = "#,0.#";
                }
                else if (a == 2)
                {
                    code = "#,0.##";
                }
                else
                {
                    code = "#,0.###";
                }

            }

            return code;
        }

        #region textBox 숫자 입력 오류 
        private void textBox_Leave(object sender, EventArgs e)
        {
            double value;
            if (double.TryParse(textBox.Text, out value))
            {
                string code_N = NumberDecimalPlaces(NumberDecimal, value);
                try
                {
                    textBox.Text = value.ToString(code_N);
                }
                catch
                {
                    textBox.Text = value.ToString();
                }
            }
            else
                textBox.Text = String.Empty;
        }
        public double textBox_doubleComa(TextBox textBox, bool LoadOrNot, int NumberDecimal)
        {
            this.textBox = textBox;
            this.textBox.Font = new System.Drawing.Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            this.textBox.TextAlign = HorizontalAlignment.Center;
            this.NumberDecimal = NumberDecimal;
            this.textdouble = 0;

            //Load일 경우 true,아니고 입력일 경우 false
            if (LoadOrNot)
            {
                double value;
                if (textBox.Text != null && textBox.Text.ToString() != "")
                {
                    if (double.TryParse(textBox.Text, out value) == true)
                    {
                        string code_N = NumberDecimalPlaces(NumberDecimal, value);
                        textBox.Text = value.ToString(code_N);
                        this.textdouble = Program.UTIL.ToDoubleOrZero(textBox.Text.ToString());
                    }
                    else
                    {
                        textBox.Text = String.Empty;
                    }
                }
            }
            else
            {
               textBox.Leave += textBox_Leave;
                double value;
                if (textBox.Text != null && textBox.Text.ToString() != "")
                {
                    if (double.TryParse(textBox.Text, out value) == true)
                    {
                        this.textdouble = Program.UTIL.ToDoubleOrZero(textBox.Text.ToString());
                    }
                    else
                    {
                        MessageBox.Show("숫자를 입력하세요.");
                        textBox.Text = String.Empty;
                    }
                }
            }
            return this.textdouble;
        }
        #endregion
        #region textBox 숫자 입력 오류 
        public double dataGridView_doubleComa(DataGridView dataGridView, int row, int column, int NumberDecimal)
        {
            var cellValue = dataGridView.Rows[row].Cells[column].Value;
            this.dataGridView = dataGridView;
            this.NumberDecimal = NumberDecimal;
            this.textdouble = 0;

            //Load일 경우 true,아니고 입력일 경우 false
            if (cellValue != null && cellValue.ToString() != "" && cellValue.ToString() != "-")
            {
                double parsedValue;
                if (double.TryParse(cellValue.ToString(), out parsedValue))
                {
                    string code_N = NumberDecimalPlaces(NumberDecimal, parsedValue);
                    dataGridView.Rows[row].Cells[column].Value = parsedValue.ToString(code_N);
                    this.textdouble = Program.UTIL.ToDoubleOrZero(cellValue.ToString());
                }
                else
                {
                    dataGridView.Rows[row].Cells[column].Value = String.Empty;
                }
            }
            return this.textdouble;
        }
        #endregion 
        public string asFixed(string s)
        {
            s = s.Trim();
            return s != "" ? Program.UTIL.ToDoubleOrZero(s).ToString("0.##") : "0.00";
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
        public static FontFamily[] Families
        {
            get
            {
                return inst.privateFont.Families;
            }
        }

        private void AddFontFromMemory()
        {
            List<byte[]> fonts = new List<byte[]>();
            fonts.Add(Properties.Resources.NanumBarunGothic);

            foreach (byte[] font in fonts)
            {
                IntPtr fontBuffer = Marshal.AllocCoTaskMem(font.Length);
                Marshal.Copy(font, 0, fontBuffer, font.Length);
                privateFont.AddMemoryFont(fontBuffer, font.Length);
            }
        }

        public bool data_inputcheck(DataGridView db, int row, int column, int title) //datagridview, 행번호, 열번호, 항목번호
        {
            double a;
            if (db.Rows[row].Cells[column].Value != "" && db.Rows[row].Cells[column].Value != null)
            {
                if (double.TryParse(db.Rows[row].Cells[column].Value.ToString(), out a))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show(string.Format("{0}에서 {1}항목에 숫자를 입력해 주세요", db.Rows[row].Cells[title].Value.ToString(), db.Columns[column].HeaderText), "주의", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else return false;
        }
    }
}
