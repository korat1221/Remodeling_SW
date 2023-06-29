using main.contents;
using main.contents.Building;
using main.contentslist;
using main.subcontents.ConstructionWindow;
using Microsoft.Web.WebView2.Core;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
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
            Blind,
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
            SubWindow,
            List_Floor,
            List_Zone,
            List_ConstructionWall

        }
          Form[] forms = new Form[] { new General(), new EnergyUse(), 
            new ConstructionCW(), new ConstructionWall(), new ConstructionRoof(), new ConstructionFloor(), new ConstructionWindow(), new ConstructionDoor(), 
            new Model(), new Shade(), new Blind(), new ThermalBridge(), 
            new ZoneGeneral(), new ZoneEnvelope(), new ZoneLighting(), new ZoneSystem(),
            new EquipmentList(),new AHUSystem(), new DHWSystem(), new HeatingSystem(), new CoolingSystem(),
            new PV(), new FuelCell(), new WindPower(), new SupplyRatio(), new EIndependenceRate(),
            new ReportExisting(), new ReportRemodeling(),
            new FormDebug(),
            new List_ConstructionWindow(),new List_ConstructionCW(),new SubWindow(),
            new List_Floor(), new List_Zone(),
            new List_ConstructionWall()};
        bool scriptable = false;
        public class FormParam
        {
            public int formID { get; set; }
            public string? ID { get; set; }
        }
        static FormParam? formParam;

        public MainContents()
        {
            InitializeComponent();

            webView21.Source = new Uri(Program.gPath + "menu.html");
            webView21.Location.Offset(0, 0);
            webView21.Size = this.ClientSize;

            InitializeAsync();

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
        public static bool OnLoadProc(Form form)
        {
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
            else if (formParam.formID == 6)
            {
                ConstructionWindow f = (ConstructionWindow)form;

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

            return true;
        }

        private bool Deserializable(String data)
        {
            try
            {
                var _a = System.Text.Json.JsonSerializer.Deserialize<FormParam>(data);

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

            void OnJSMessage(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            if (Deserializable(args.TryGetWebMessageAsString()))
            {
                formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>(args.TryGetWebMessageAsString());

                if (formParam.formID == 99999991)
                {
                    DoLoadForm(8, OnLoadProc);

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
                else if (formParam.formID >= 0 && formParam.formID < 100)
                {
                    DoLoadForm(formParam.formID, OnLoadProc);
                }
            }
            else
            {
                String ID = args.TryGetWebMessageAsString();
                String json = "{\"formID\":" + ID + ",\"ID\":\"\"}";

                if (Deserializable(json))
                {
                    formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>(json);
                    if (formParam.formID == 8)
                    {
                        Program.UTIL.setObjInfo(Program.UTIL.read3DModel(Program.ProjName + ".json"));

                        DoLoadForm(8, OnLoadProc);
                    }
                    else if (Deserializable(json))
                    {
                        if (formParam.formID >= 0 && formParam.formID < 100)
                        {
                            DoLoadForm(formParam.formID, OnLoadProc);
                        }
                    }
                }
                else
                {
                    Program.UTIL.sendMessage(ID);
                }
            }
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
            }
        }
        public void DoLoadForm(int idx, OnOpenProc proc = null)
        {
            int i = -1;
            while (++i < forms.Length)
            {
                forms[i].Hide();
            }

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
        }
    }
}
