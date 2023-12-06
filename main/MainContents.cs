using main.contents;
using main.contents.Building;
using main.contents.Construction;
using main.contents.Result;
using main.contentslist;
using main.subcontents.ConstructionWindow;
using Microsoft.Web.WebView2.Core;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static main.contents.Model;
using static main.MainContents;

namespace main
{
    public delegate bool OnOpenProc(Form form);

    public partial class MainContents : Form
    {
        public enum FormID
        {
            General =0,
            EnergyUse,
            ConstructionCW,
            ConstructionWall,
            ConstructionRoof,
            ConstructionFloor,
            ConstructionWindow,
            ConstructionDoor,
            Model,
            Shade,
            ConstructionBlind,
            ThermalBridge,
            ZoneGeneral,
            ZoneEnvelope,
            ZoneLighting,
            ZoneSystem,
            EquipmentList,
            AHUSystem,
            DHWSystem,
            HeatingSystem,
            CoolingSystem,
            PV,
            FuelCell,
            WindPower,
            SupplyRate,
            EIndependenceRatio,
            ReportExisting,
            ReportRemodeling,
            FormDebug,
            List_ConstructionWindow,
            List_ConstructionCW,
            SubWindow,
            List_Floor,
            List_Zone,
            List_ConstructionWall,
            List_ConstructionRoof,
            List_ConstructionFloor,
            PrintReport_HCneed,
            List_CoolingSystem,
            List_HeatingSystem,
            Intro,
            ProjectList,
            OpenProject,
            List_RESystem,
            PrintReport_Lighting,
            PrintReport_Heating,
            PrintReport_Cooling,
            PrintReport_DHSystem,
            PrintReport_AHUSystem,
            List_DHWSystem,
            List_ConstructionBlind,
            None

        }
        Form[] forms = new Form[] { new General(), new EnergyUse(),
            new ConstructionCW(), new ConstructionWall(), new ConstructionRoof(), new ConstructionFloor(), new ConstructionWindow(), new ConstructionDoor(),
            new Model(), new Shade(), new ConstructionBlind(), new ThermalBridge(),
            new ZoneGeneral(), new ZoneEnvelope(), new ZoneLighting(), new ZoneSystem(),
            new EquipmentList(),new AHUSystem(), new DHWSystem(), new HeatingSystem(), new CoolingSystem(),
            new PV(), new FuelCell(), new WindPower(), new SupplyRatio(), new EIndependenceRate(),
            new ReportExisting(), new ReportRemodeling(),
            new FormDebug(),
            new List_ConstructionWindow(),new List_ConstructionCW(),new SubWindow(),
            new List_Floor(), new List_Zone(),
            new List_ConstructionWall(), new List_ConstructionRoof(), new List_ConstructionFloor(), new PrintReport_HCneed(),new List_CoolingSystem(), new List_HeatingSystem(),
            new Intro(), new ProjectList(), new OpenProject(), new List_RESystem(),
            new PrintReport_Lighting(),new PrintReport_Heating(),new PrintReport_Cooling(),new PrintReport_DHWSystem(),new PrintReport_AHUSystem(),
            new List_DHWSystem(),new List_ConstructionBlind()}; //나중에 PV를 냉방리스트로 바꿔야함 
        bool scriptable = false;
        public class FormParam
        {
            public int formID { get; set; }
            public string? ID { get; set; }
        }
        static FormParam? formParam;
        static public String? selID;
        static public FormID currentForm = FormID.General;
        string selID_old = "";
        int tick_old = 0;

        public MainContents()
        {
            InitializeComponent();

            webView21.Source = new Uri(Program.gPath + "menu.html");
            webView21.Location.Offset(0, 0);
            webView21.Size = this.ClientSize;

            InitializeAsync();

            Program.DB.initTables(DB.type.ProjDB);

            foreach (FormMain openForm in Application.OpenForms)
            {
                if (openForm.Name == "FormMain")
                {
                    int i = -1;
                    while (++i < forms.Length)
                    {
                        forms[i].TopLevel = false;
                        openForm.splitContainer1.Panel2.Controls.Add(forms[i]);
                    }

                    return;
                }
            }
        }

        async void InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.WebMessageReceived += OnJSMessage;
            webView21.CoreWebView2.NavigationCompleted += OnNaviCompleted;
        }
        public void refreshWebCtrl()
        {
            webView21.Source = new Uri(Program.gPath + "menu.html?n=2",true);

            Model f = (Model)forms[8];

            f.DoLoadForm(0);
        }
        public static bool OnLoadProc(Form form)
        {
            if (formParam.formID == 0)
            {
                General f = (General)form;

                f.LoadData(formParam.ID);
            }
            if (formParam.formID == 1)
            {
                EnergyUse f = (EnergyUse)form;

                f.LoadData(formParam.ID);
            }
            if (formParam.formID == 2)
            {
                ConstructionCW f = (ConstructionCW)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 3)
            {
                ConstructionWall f = (ConstructionWall)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 4)
            {
                ConstructionRoof f = (ConstructionRoof)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 5)
            {
                ConstructionFloor f = (ConstructionFloor)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 6)
            {
                ConstructionWindow f = (ConstructionWindow)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 8)
            {
                Model f = (Model)form;

                f.DoLoadForm(Int32.Parse(formParam.ID));
            }
            else if (formParam.formID == 10)
            {
               ConstructionBlind f = (ConstructionBlind)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 18)
            {
                DHWSystem f = (DHWSystem)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 19)
            {
                HeatingSystem f = (HeatingSystem)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 20)
            {
                CoolingSystem f = (CoolingSystem)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 29)
            {
                List_ConstructionWindow f = (List_ConstructionWindow)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 30)
            {
                List_ConstructionCW f = (List_ConstructionCW)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 31)
            {
                SubWindow f = (SubWindow)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 32)
            {
                List_Floor f = (List_Floor)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 33)
            {
                List_Zone f = (List_Zone)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 34)
            {
                List_ConstructionWall f = (List_ConstructionWall)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 35)
            {
                List_ConstructionRoof f = (List_ConstructionRoof)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 36)
            {
                List_ConstructionFloor f = (List_ConstructionFloor)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 37)
            {
                PrintReport_HCneed f = (PrintReport_HCneed)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 38)
            {
                List_CoolingSystem f = (List_CoolingSystem)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 39)
            {
                List_HeatingSystem f = (List_HeatingSystem)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 40)
            {
               Intro f = (Intro)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 41)
            {
                ProjectList f = (ProjectList)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 42)
            {
                OpenProject f = (OpenProject)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 43)
            {
                List_RESystem f = (List_RESystem)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 44)
            {
                PrintReport_Lighting f = (PrintReport_Lighting)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 45)
            {
                PrintReport_Heating f = (PrintReport_Heating)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 46)
            {
                PrintReport_Cooling f = (PrintReport_Cooling)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 47)
            {
                PrintReport_DHWSystem f = (PrintReport_DHWSystem)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 48)
            {
                PrintReport_AHUSystem f = (PrintReport_AHUSystem)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 49)
            {
                List_DHWSystem f = (List_DHWSystem)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 50)
            {
                List_ConstructionBlind f = (List_ConstructionBlind)form;

                f.LoadData(formParam.ID);
            }
            return true;
        }

        private bool Deserialize(String data)
        {
            try
            {
                formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>(data);

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

            void OnJSMessage(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            int tick = Environment.TickCount;

            selID = args.TryGetWebMessageAsString();

            if (tick - tick_old > 1000 || selID != selID_old)
            {
                if (Deserialize(selID))
                {
                    if (formParam.formID == 99999991)
                    {
                        DoLoadForm(8, OnLoadProc);

                        if (MessageBox.Show("모델을 다시 로드 할 경우, 입력 정보 전체 삭제됩니다. 계속하시겠습니까 ?", "YesOrNo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            using (OpenFileDialog openFileDialog = new OpenFileDialog())
                            {
                                openFileDialog.Filter = "obj 파일 (*.obj)|*.obj";
                                openFileDialog.FilterIndex = 2;
                                openFileDialog.RestoreDirectory = true;

                                if (openFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    //Get the path of specified file
                                    Program.UTIL.load3DModel(openFileDialog.FileName);
                                }
                            }
                        }
                    }
                    else if (formParam.formID >= 0 && formParam.formID < 100)
                    {
                        DoLoadForm(formParam.formID, OnLoadProc);
                    }
                }
                else
                {
                    String json = "{\"formID\":" + selID + ",\"ID\":\"0\"}";

                    if (Deserialize(json))
                    {
                        if (formParam.formID == 8)
                        {
                            Program.UTIL.setObjInfo(Program.UTIL.read3DModel());

                            DoLoadForm(8, OnLoadProc);
                        }
                        else if (formParam.formID >= 0 && formParam.formID < 100)
                        {
                            DoLoadForm(formParam.formID, OnLoadProc);
                        }
                    }
                    else
                    {
                        Program.UTIL.sendMessage(selID);

                        if (selID.IndexOf("_win2") >= 0 || selID.IndexOf("_win3") >= 0 || selID.IndexOf("_win4") >= 0)
                        {
                            formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>("{\"formID\":8,\"ID\":\"3\"}");
                        }
                        else if (selID.IndexOf("_WALL_") >= 0)
                        {
                            formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>("{\"formID\":8,\"ID\":\"4\"}");
                        }
                        else if (selID.IndexOf("_ROOF_") >= 0)
                        {
                            formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>("{\"formID\":8,\"ID\":\"5\"}");
                        }
                        else if (selID.IndexOf("_FLOOR_") >= 0)
                        {
                            formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>("{\"formID\":8,\"ID\":\"6\"}");
                        }
                        else if (selID.IndexOf("_win1") >= 0)
                        {
                            formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>("{\"formID\":8,\"ID\":\"7\"}");
                        }
                        else if (selID.IndexOf("_win5") >= 0)
                        {
                            formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>("{\"formID\":8,\"ID\":\"8\"}");
                        }
                        else if (selID.IndexOf("_INWALL_") >= 0)
                        {
                            formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>("{\"formID\":8,\"ID\":\"9\"}");
                        }
                        else if (selID.IndexOf("_INFLOOR_") >= 0)
                        {
                            formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>("{\"formID\":8,\"ID\":\"10\"}");
                        }
                        else if (selID.IndexOf("space-") >= 0)
                        {
                            formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>("{\"formID\":8,\"ID\":\"2\"}");
                        }
                        else if (selID.IndexOf("bridge-") >= 0)
                        {
                            formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>("{\"formID\":8,\"ID\":\"1\"}");
                        }
                        else
                        {
                            return;
                        }

                        DoLoadForm(formParam.formID, OnLoadProc);
                    }
                }
            }
            tick_old = tick;
            selID_old = selID;
        }
        void OnNaviCompleted(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            scriptable = true;
            int i = -1;
            while (++i < forms.Length)
            {
                if (i == 29)
                {
                    List_ConstructionWindow f = (List_ConstructionWindow)forms[i];

                    f.LoadData("");
                }
                else if (i == 30)
                {
                    List_ConstructionCW f = (List_ConstructionCW)forms[i];

                    f.LoadData("");
                }
                else if (i == 32)
                {
                    List_Floor f = (List_Floor)forms[i];

                    f.LoadData("");
                }
                else if (i == 34)
                {
                    List_ConstructionWall f = (List_ConstructionWall)forms[i];

                    f.LoadData("");
                }
                else if (i == 35)
                {
                    List_ConstructionRoof f = (List_ConstructionRoof)forms[i];

                    f.LoadData("");
                }
                else if (i == 36)
                {
                    List_ConstructionFloor f = (List_ConstructionFloor)forms[i];

                    f.LoadData("");
                }
                else if (i == 37)
                {
                    PrintReport_HCneed f = (PrintReport_HCneed)forms[i];

                    f.LoadData("");
                }
                else if (i == 38)
                {
                    List_CoolingSystem f = (List_CoolingSystem)forms[i];

                    f.LoadData("");
                }
                else if (i == 39)
                {
                    List_HeatingSystem f = (List_HeatingSystem)forms[i];

                    f.LoadData("");
                }
                else if (i == 43)
                {
                    List_RESystem f = (List_RESystem)forms[i];

                    f.LoadData("");
                }
                else if (i == 49)
                {
                    List_DHWSystem f = (List_DHWSystem)forms[i];

                    f.LoadData("");
                }
                else if (i == 50)
                {
                    List_ConstructionBlind f = (List_ConstructionBlind)forms[i];

                    f.LoadData("");
                }
            }
        }
        public void DoLoadFormDirect(int idx)
        {
            //        Program.UTIL.unselectAll();
            formParam.formID = idx;
            DoLoadForm(idx, OnLoadProc);
        }

        public void DoLoadForm(int idx, OnOpenProc proc = null)
        {
            currentForm = FormID.None;
            int i = -1;
            while (++i < forms.Length)
            {
                forms[i].Hide();
            }

            currentForm = (FormID)idx;

            if (proc != null) proc(forms[idx]);

            forms[idx].Show();
        }

        public void DoResizeMain(Size sz)
        {
            int i = -1;
            while (++i < forms.Length)
            {
                forms[i].Size = sz;
            }
        }

        private void OnResize(object sender, EventArgs e)
        {
            webView21.Location.Offset(0, 0);
            webView21.Size = this.ClientSize;
            webView21.Height = webView21.Size.Height - 4;
        }

        public void runScript(string script)
        {
            if (scriptable)
            {
                webView21.CoreWebView2.ExecuteScriptAsync(script);
            }
        }
        public void ResetForm(int idx)
        {
            if (idx == 6)
            {
                foreach (FormMain openForm in Application.OpenForms)
                {
                    if (openForm.Name == "FormMain")
                    {
                        openForm.splitContainer1.Panel2.Controls.Remove(forms[idx]);
                        forms[idx] = new ConstructionWindow();
                        forms[idx].TopLevel = false;
                        openForm.splitContainer1.Panel2.Controls.Add(forms[idx]);
                        return;
                    }
                }
            }
            else if (idx == 2)
            {
                foreach (FormMain openForm in Application.OpenForms)
                {
                    if (openForm.Name == "FormMain")
                    {
                        openForm.splitContainer1.Panel2.Controls.Remove(forms[idx]);
                        forms[idx] = new ConstructionCW();
                        forms[idx].TopLevel = false;
                        openForm.splitContainer1.Panel2.Controls.Add(forms[idx]);
                        return;
                    }
                }
            }
            else if (idx == 3)
            {
                foreach (FormMain openForm in Application.OpenForms)
                {
                    if (openForm.Name == "FormMain")
                    {
                        openForm.splitContainer1.Panel2.Controls.Remove(forms[idx]);
                        forms[idx] = new ConstructionWall();
                        forms[idx].TopLevel = false;
                        openForm.splitContainer1.Panel2.Controls.Add(forms[idx]);
                        return;
                    }
                }
            }
            else if (idx == 4)
            {
                foreach (FormMain openForm in Application.OpenForms)
                {
                    if (openForm.Name == "FormMain")
                    {
                        openForm.splitContainer1.Panel2.Controls.Remove(forms[idx]);
                        forms[idx] = new ConstructionRoof();
                        forms[idx].TopLevel = false;
                        openForm.splitContainer1.Panel2.Controls.Add(forms[idx]);
                        return;
                    }
                }
            }
            else if (idx == 5)
            {
                foreach (FormMain openForm in Application.OpenForms)
                {
                    if (openForm.Name == "FormMain")
                    {
                        openForm.splitContainer1.Panel2.Controls.Remove(forms[idx]);
                        forms[idx] = new ConstructionFloor();
                        forms[idx].TopLevel = false;
                        openForm.splitContainer1.Panel2.Controls.Add(forms[idx]);
                        return;
                    }
                }
            }
            else if (idx == 8)
            {
                foreach (FormMain openForm in Application.OpenForms)
                {
                    if (openForm.Name == "FormMain")
                    {
                        openForm.splitContainer1.Panel2.Controls.Remove(forms[idx]);
                        forms[idx] = new Model();
                        forms[idx].TopLevel = false;
                        openForm.splitContainer1.Panel2.Controls.Add(forms[idx]);
                        return;
                    }
                }
            }
            else if (idx == 10)
            {
                foreach (FormMain openForm in Application.OpenForms)
                {
                    if (openForm.Name == "FormMain")
                    {
                        openForm.splitContainer1.Panel2.Controls.Remove(forms[idx]);
                        forms[idx] = new ConstructionBlind();
                        forms[idx].TopLevel = false;
                        openForm.splitContainer1.Panel2.Controls.Add(forms[idx]);
                        return;
                    }
                }
            }
            else if (idx == 18)
            {
                foreach (FormMain openForm in Application.OpenForms)
                {
                    if (openForm.Name == "FormMain")
                    {
                        openForm.splitContainer1.Panel2.Controls.Remove(forms[idx]);
                        forms[idx] = new DHWSystem();
                        forms[idx].TopLevel = false;
                        openForm.splitContainer1.Panel2.Controls.Add(forms[idx]);
                        return;
                    }
                }
            }
            else if (idx == 19)
            {
                foreach (FormMain openForm in Application.OpenForms)
                {
                    if (openForm.Name == "FormMain")
                    {
                        openForm.splitContainer1.Panel2.Controls.Remove(forms[idx]);
                        forms[idx] = new HeatingSystem();
                        forms[idx].TopLevel = false;
                        openForm.splitContainer1.Panel2.Controls.Add(forms[idx]);
                        return;
                    }
                }
            }
            else if (idx == 20)
            {
                foreach (FormMain openForm in Application.OpenForms)
                {
                    if (openForm.Name == "FormMain")
                    {
                        openForm.splitContainer1.Panel2.Controls.Remove(forms[idx]);
                        forms[idx] = new CoolingSystem();
                        forms[idx].TopLevel = false;
                        openForm.splitContainer1.Panel2.Controls.Add(forms[idx]);
                        return;
                    }
                }
            }
        }
    }
}
