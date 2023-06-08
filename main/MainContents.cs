using main.contents;
using main.contents.Building;
using main.contentslist;
using Microsoft.Web.WebView2.Core;
using System.Windows.Forms;

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

        }
          Form[] forms = new Form[] { new General(), new EnergyUse(), 
            new ConstructionCW(), new ConstructionWall(), new ConstructionRoof(), new ConstructionFloor(), new ConstructionWindow(), new ConstructionDoor(), 
            new Model(), new Shade(), new Blind(), new ThermalBridge(), 
            new ZoneGeneral(), new ZoneEnvelope(), new ZoneLighting(), new ZoneSystem(),
            new EquipmentList(),new AHUSystem(), new DHWSystem(), new HeatingSystem(), new CoolingSystem(),
            new PV(), new FuelCell(), new WindPower(), new SupplyRatio(), new EIndependenceRate(),
            new ReportExisting(), new ReportRemodeling(),
            new FormDebug(),
            new List_ConstructionWindow(),new List_ConstructionCW()};
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
            return true;
        }

        void OnJSMessage(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            try
            {
                formParam = System.Text.Json.JsonSerializer.Deserialize<FormParam>(args.TryGetWebMessageAsString());

                if (formParam.formID >= 0 && formParam.formID < 100)
                {
                    DoLoadForm(formParam.formID, OnLoadProc);
                }
            }
            catch (Exception ex)
            {

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
        }
    }
}
