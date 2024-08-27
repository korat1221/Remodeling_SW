using main.contents;
using main.contents.Building;
using main.contents.Alt;
using main.contents.Construction;
using main.contents.Result;
using main.contentslist;
using main.subcontents.ConstructionWindow;
using Microsoft.Web.WebView2.Core;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static main.contents.Model;
using static main.MainContents;
using main.contents.Result.Building_Report;

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
            Element_Report,
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
            List_ConstructionDoor,
            PrintReport_Final,
            List_PV,
            List_FuelCell,
            List_WindPower,
            PrintReport_Main,
            List_AHUSystem,
            List_Alt,
            AltMain,
            None

        }
        Form[] forms = new Form[] { new General(), new EnergyUse(),
            new ConstructionCW(), new ConstructionWall(), new ConstructionRoof(), new ConstructionFloor(), new ConstructionWindow(), new ConstructionDoor(),
            new Model(), new Shade(), new ConstructionBlind(), new ThermalBridge(),
            new ZoneGeneral(), new ZoneEnvelope(), new ZoneLighting(), new ZoneSystem(),
            new EquipmentList(),new AHUSystem(), new DHWSystem(), new HeatingSystem(), new CoolingSystem(),
            new PV(), new FuelCell(), new WindPower(), new SupplyRatio(), new EIndependenceRate(),
            new Element_Report_Main(), new Algorithm_Report(),
            new FormDebug(),
            new List_ConstructionWindow(),new List_ConstructionCW(),new SubWindow(),
            new List_Floor(), new List_Zone(),
            new List_ConstructionWall(), new List_ConstructionRoof(), new List_ConstructionFloor(), new PrintReport_HCneed(),new List_CoolingSystem(), new List_HeatingSystem(),
            new Intro(), new ProjectList(), new OpenProject(), new List_RESystem(),
            new PrintReport_Lighting(),new PrintReport_Heating(),new PrintReport_Cooling(),new PrintReport_DHWSystem(),new PrintReport_AHUSystem(),
            new List_DHWSystem(),new List_ConstructionBlind(),new List_ConstructionDoor(),
            new PrintReport_Final(), 
            new List_PV(),new List_FuelCell(),new List_WindPower(),
            new Building_Report(),
            new List_AHUSystem(),
            new List_Alt(), new AltMain()
        }; 
        bool scriptable = false;
        public class FormParam
        {
            public int formID { get; set; }
            public string? ID { get; set; }
        }
        static FormParam? formParam;
        static public String? selID;
        static public FormID currentForm = FormID.General;
        static public string selID_old = "";
        int tick_old = 0;
        bool ticked = false;

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
            else if (formParam.formID == 7)
            {
                ConstructionDoor f = (ConstructionDoor)form;

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
            else if (formParam.formID == 17)
            {
                AHUSystem f = (AHUSystem)form;

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
            else if (formParam.formID == 21)
            {
                PV f = (PV)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 22)
            {
                FuelCell f = (FuelCell)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 23)
            {
                WindPower f = (WindPower)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 26)
            {
                Element_Report_Main f = (Element_Report_Main)form;

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
            else if (formParam.formID == 51)
            {
                List_ConstructionDoor f = (List_ConstructionDoor)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 52)
            {
                PrintReport_Final f = (PrintReport_Final)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 53)
            {
                List_PV f = (List_PV)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 54)
            {
                List_FuelCell f = (List_FuelCell)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 55)
            {
                List_WindPower f = (List_WindPower)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 56)
            {
                Building_Report f = (Building_Report)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 57)
            {
                List_AHUSystem f = (List_AHUSystem)form;

                f.LoadData(formParam.ID);
            }           
            else if (formParam.formID == 58)
            {
                List_Alt f = (List_Alt)form;

                f.LoadData(formParam.ID);
            }
            else if (formParam.formID == 59)
            {
                AltMain f = (AltMain)form;

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
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (!ticked)
            {
                ticked = true;

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
        }

        void OnJSMessage(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            int tick = Environment.TickCount;

            selID = args.TryGetWebMessageAsString();

            if (selID != selID_old)
            {
                if (Deserialize(selID))
                {
                    if (formParam.formID == 99999991)
                    {
                        DoLoadForm(8, OnLoadProc);

                        ticked = false;
                        timer1.Interval = 200;
                        timer1.Tick += new EventHandler(timer1_Tick);
                        timer1.Enabled = true;
                    }
                    else if (formParam.formID >= 0 && formParam.formID < 100)
                    {
                        DoLoadForm(formParam.formID, OnLoadProc);
                    }

                    if (selID == "{\"formID\":37,\"ID\":\"Result_0\"}")
                    {
                        selID = "37";
                    }
                }
                else
                {
                    String json = "{\"formID\":" + selID + ",\"ID\":\"0\"}";

                    if (Deserialize(json))
                    {
                        if (formParam.formID == 8)
                        {
                            Program.UTIL.setObjInfo(ProjectList.CurProjID);

                            DoLoadForm(8, OnLoadProc);
                        }
                        else if (formParam.formID >= 0 && formParam.formID < 100)
                        {
                            DoLoadForm(formParam.formID, OnLoadProc);
                        }
                    }
                    else
                    {
                        int n = selID.IndexOf("$$$");

                        Program.UTIL.sendMessage(n > 0 ? selID.Substring(0, n) : selID);

                        if (selID.IndexOf("_win2") >= 0 || selID.IndexOf("_win3") >= 0 || selID.IndexOf("_win4") >= 0 || selID.IndexOf("_CW_") >= 0)
                        {
                            formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>("{\"formID\":8,\"ID\":\"3\"}");
                        }
                        else if (selID.IndexOf("_WALL_") >= 0 || selID.IndexOf("_WL_") >= 0)
                        {
                            formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>("{\"formID\":8,\"ID\":\"4\"}");
                        }
                        else if (selID.IndexOf("_ROOF_") >= 0 || selID.IndexOf("_RF_") >= 0)
                        {
                            formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>("{\"formID\":8,\"ID\":\"5\"}");
                        }
                        else if (selID.IndexOf("_FLOOR_") >= 0 || selID.IndexOf("_FR_") >= 0)
                        {
                            formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>("{\"formID\":8,\"ID\":\"6\"}");
                        }
                        else if (selID.IndexOf("_win1") >= 0 || selID.IndexOf("_WIN_") >= 0)
                        {
                            formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>("{\"formID\":8,\"ID\":\"7\"}");
                        }
                        else if (selID.IndexOf("_win5") >= 0 || selID.IndexOf("_DR_") >= 0)   
                        {
                            formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>("{\"formID\":8,\"ID\":\"8\"}");
                        }
                        else if (selID.IndexOf("_INWALL_") >= 0 || selID.IndexOf("_IW_") >= 0)
                        {
                            formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>("{\"formID\":8,\"ID\":\"9\"}");
                        }
                        else if (selID.IndexOf("_INFLOOR_") >= 0 || selID.IndexOf("_SL_") >= 0)
                        {
                            formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>("{\"formID\":8,\"ID\":\"10\"}");
                        }
                        else if (selID.IndexOf("space-") >= 0)
                        {
                            formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>("{\"formID\":8,\"ID\":\"2\"}");
                        }
                        else if (selID.IndexOf("bridge-") >= 0)
                        {
                            formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>("{\"formID\":8,\"ID\":\"11\"}");
                        }                        
                        else if (selID.IndexOf("RTB") >= 0 || selID.IndexOf("WTB") >= 0)
                        {
                            formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>("{\"formID\":8,\"ID\":\"12\"}");
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
                else if (i == 51)
                {
                    List_ConstructionDoor f = (List_ConstructionDoor)forms[i];

                    f.LoadData("");
                }
                else if (i == 53)
                {
                    List_PV f = (List_PV)forms[i];

                    f.LoadData("");
                }
                else if (i == 54)
                {
                    List_FuelCell f = (List_FuelCell)forms[i];

                    f.LoadData("");
                }
                else if (i == 55)
                {
                    List_WindPower f = (List_WindPower)forms[i];

                    f.LoadData("");
                }
                else if (i == 57)
                {
                    List_AHUSystem f = (List_AHUSystem)forms[i];

                    f.LoadData("");
                }
            }
        }
        public void DoLoadFormDirect(int idx)
        {
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
            try
            {
                webView21.Location.Offset(0, 0);
                webView21.Size = this.ClientSize;
                webView21.Height = webView21.Size.Height - 4;
            }
            catch (Exception ex)
            {

            }
            
        }

        public void runScript(string script)
        {
            if (scriptable)
            {
                webView21.CoreWebView2.ExecuteScriptAsync(script);
            }
        }
        public void resetAll()
        {
            foreach (FormMain openForm in Application.OpenForms)
            {
                if (openForm.Name == "FormMain")
                {
                    int i = -1;
                    while (++i < forms.Length)
                    {
                        if (i != 41 && i != 8)
                        {
                            openForm.splitContainer1.Panel2.Controls.Remove(forms[i]);
                        }
                    }

                    forms[0] = new General();
                    forms[1] = new EnergyUse();
                    forms[2] = new ConstructionCW();
                    forms[3] = new ConstructionWall();
                    forms[4] = new ConstructionRoof();
                    forms[5] = new ConstructionFloor();
                    forms[6] = new ConstructionWindow();
                    forms[7] = new ConstructionDoor();
//                    forms[8] = new Model();
                    forms[9] = new Shade();
                    forms[10] = new ConstructionBlind();
                    forms[11] = new ThermalBridge();
                    forms[12] = new ZoneGeneral();
                    forms[13] = new ZoneEnvelope();
                    forms[14] = new ZoneLighting();
                    forms[15] = new ZoneSystem();
                    forms[16] = new EquipmentList();
                    forms[17] = new AHUSystem();
                    forms[18] = new DHWSystem();
                    forms[19] = new HeatingSystem();
                    forms[20] = new CoolingSystem();
                    forms[21] = new PV();
                    forms[22] = new FuelCell();
                    forms[23] = new WindPower();
                    forms[24] = new SupplyRatio();
                    forms[25] = new EIndependenceRate();
                    forms[26] = new Element_Report_Main();
                    forms[27] = new Algorithm_Report();
                    forms[28] = new FormDebug();
                    forms[29] = new List_ConstructionWindow();
                    forms[30] = new List_ConstructionCW();
                    forms[31] = new SubWindow();
                    forms[32] = new List_Floor();
                    forms[33] = new List_Zone();
                    forms[34] = new List_ConstructionWall();
                    forms[35] = new List_ConstructionRoof();
                    forms[36] = new List_ConstructionFloor();
                    forms[37] = new PrintReport_HCneed();
                    forms[38] = new List_CoolingSystem();
                    forms[39] = new List_HeatingSystem();
                    forms[40] = new Intro();
                    //forms[41] = new ProjectList();
                    forms[42] = new OpenProject();
                    forms[43] = new List_RESystem();
                    forms[44] = new PrintReport_Lighting();
                    forms[45] = new PrintReport_Heating();
                    forms[46] = new PrintReport_Cooling();
                    forms[47] = new PrintReport_DHWSystem();
                    forms[48] = new PrintReport_AHUSystem();
                    forms[49] = new List_DHWSystem();
                    forms[50] = new List_ConstructionBlind();
                    forms[51] = new List_ConstructionDoor();
                    forms[52] = new PrintReport_Final();
                    forms[53] = new List_PV();
                    forms[54] = new List_FuelCell();
                    forms[55] = new List_WindPower();
                    forms[56] = new Building_Report();
                    forms[57] = new List_AHUSystem();
                    forms[58] = new List_Alt();
                    forms[59] = new AltMain();

                    i = -1;
                    while (++i < forms.Length)
                    {
                        if (i != 41 && i != 8)
                        {
                            forms[i].TopLevel = false;
                            openForm.splitContainer1.Panel2.Controls.Add(forms[i]);
                        }
                    }

                    webView21.Reload();

                    return;
                }
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
            else if (idx == 7)
            {
                foreach (FormMain openForm in Application.OpenForms)
                {
                    if (openForm.Name == "FormMain")
                    {
                        openForm.splitContainer1.Panel2.Controls.Remove(forms[idx]);
                        forms[idx] = new ConstructionDoor();
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
            else if (idx == 17)
            {
                foreach (FormMain openForm in Application.OpenForms)
                {
                    if (openForm.Name == "FormMain")
                    {
                        openForm.splitContainer1.Panel2.Controls.Remove(forms[idx]);
                        forms[idx] = new AHUSystem();
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
            else if (idx == 21)
            {
                foreach (FormMain openForm in Application.OpenForms)
                {
                    if (openForm.Name == "FormMain")
                    {
                        openForm.splitContainer1.Panel2.Controls.Remove(forms[idx]);
                        forms[idx] = new PV();
                        forms[idx].TopLevel = false;
                        openForm.splitContainer1.Panel2.Controls.Add(forms[idx]);
                        return;
                    }
                }
            }
            else if (idx == 22)
            {
                foreach (FormMain openForm in Application.OpenForms)
                {
                    if (openForm.Name == "FormMain")
                    {
                        openForm.splitContainer1.Panel2.Controls.Remove(forms[idx]);
                        forms[idx] = new FuelCell();
                        forms[idx].TopLevel = false;
                        openForm.splitContainer1.Panel2.Controls.Add(forms[idx]);
                        return;
                    }
                }
            }
            else if (idx == 23)
            {
                foreach (FormMain openForm in Application.OpenForms)
                {
                    if (openForm.Name == "FormMain")
                    {
                        openForm.splitContainer1.Panel2.Controls.Remove(forms[idx]);
                        forms[idx] = new WindPower();
                        forms[idx].TopLevel = false;
                        openForm.splitContainer1.Panel2.Controls.Add(forms[idx]);
                        return;
                    }
                }
            }
            else if (idx == 59)
            {
                foreach (FormMain openForm in Application.OpenForms)
                {
                    if (openForm.Name == "FormMain")
                    {
                        openForm.splitContainer1.Panel2.Controls.Remove(forms[idx]);
                        forms[idx] = new AltMain();
                        forms[idx].TopLevel = false;
                        openForm.splitContainer1.Panel2.Controls.Add(forms[idx]);
                        return;
                    }
                }
            }
        }
    }
}
