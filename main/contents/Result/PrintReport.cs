using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.contents.Result
{
    public partial class PrintReport : Form
    {
        bool scriptable = false;
        public PrintReport()
        {
            InitializeComponent();

            InitializeAsync();
        }
        async void InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.WebMessageReceived += OnJSMessage;
            webView21.CoreWebView2.NavigationCompleted += OnNaviCompleted;
        }
        void OnJSMessage(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            try
            {
                String s = args.TryGetWebMessageAsString();
            }
            catch (Exception ex)
            {

            }
        }
        void OnNaviCompleted(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            scriptable = true;
        }
        public void runScript(string script)
        {
            if (scriptable)
            {
                webView21.CoreWebView2.ExecuteScriptAsync(script);
            }
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            string s, s2;
            string[][] ZoneG = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,존이름,실제어방식,냉난방유무,환기유무,환기방식,온도교환효율,전열교환효율,용도프로필,천장고,시작시간,종료시간,주이용일,재실자수,기기발열수준,일일급탕요구량,냉난방시간,사용시간,공조시간,연이용일수,재실밀도,재실수준,일일인체발열,면적당인체발열,일일기기발열,면적당기기발열,순체적,환기횟수,이용일환기량,비이용일환기량");
            string[][] value = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "번호,HT_tot,HT_Wall,HT_Roof,HT_Floor,HT_GWall,HT_Door,HT_Win,HT_CW,HT_Di_Wall,HT_Indi_Wall,HT_Di_Roof,HT_Indi_Roof,HT_Di_Win,HT_Indi_Win,HT_Di_Door,HT_Indi_Door,HT_TB_tot,HT_TB_Wall,HT_TB_Roof,HT_TB_Floor,HT_TB_Gwall,HT_TB_Win,HT_TB_Door,HT_TB_CW,HV_tot,HV_inf,HV_win,HV_z,HV_mech,H_tot,tao,dwe_mth,dwd_mth,theta_i,theta_e,QTsink_tot,QTsink_Wall,QTsink_Roof,QTsink_Floor,QTsink_GWall,QTsink_Door,QTsink_Win,QTsink_CW,QTsource_tot,QTsource_Wall,QTsource_Roof,QTsource_Floor,QTsource_GWall,QTsource_Door,QTsource_Win,QTsource_CW,QSopsink_tot,QSopsource_tot,QStr_tot,QSopsink_Wall,QSopsink_Roof,QSopsink_Door,QSopsink_CW_p,QSopsource_Wall,QSopsource_Roof,QSopsource_Door,QSopsource_CW_p,QStr_Win,QStr_CW,QVsink_tot,QV_inf_sink,QV_win_sink,QV_z_sink,QV_mech_sink,QVsource_tot,QV_inf_source,QV_win_source,QV_z_source,QV_mech_source,QI_tot,QI_L,QI_P,QI_fac,Qsink,Qsource,gamma,a,eta,dQc_b,dQc_sink,Qhb_we_day,Qhb_wd_day,Qcb_we_day,Qcb_wd_day,Qhb_mth,Qcb_mth,Qhb_we_mth,Qhb_wd_mth,Qcb_we_mth,Qcb_wd_mth,Qhb_a,Qcb_a,Qhb_we_a,Qhb_wd_a,Qcb_we_a,Qcb_wd_a");
            string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "Zone_LightResult", "번호,ITr,IRD,ISh_Ish,ISh_hA,Ish_vA,Ish_In_At,Wi,Ish_GDF,Ish,f_τeff_SNA,f_D,f_nearD,f_DCA,f_dclass,f_nearEm_SNA,f_fd_sna,f_fd_sa,f_nearEm_DC,f_fd_c,f_FDS,f_FD,as_bs,hs_bs,hg_hw,normal_ηR,saw_ηR,r_DSNA,r_DSA,r_dclass,r_nearEm_FDS,r_fd_sna,r_fd_sa,r_nearEm_DC,r_fd_c,r_FDS,r_FD,Sunlight_SCW,Sunlight_PjSC,Final_W");
            string[][] envelope = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,면적", "외피유형='" + "외벽" + "'");
            string[][] envelope2 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,면적", "외피유형='" + "커튼월창" + "'");
            string[][] envelope3 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,면적", "외피유형='" + "지붕" + "'");
            string[][] envelope4= Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,면적", "외피유형='" + "창호" + "'");
            string[][] envelope5 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,면적", "외피유형='" + "최하층바닥" + "'");


            //string[][] uenvelope = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope", "존,면적,Ueff", "외피유형='" + "외벽" + "'");
            //string[][] uenvelope2 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope", "존,면적,Ueff", "외피유형='" + "커튼월창" + "'");
            //string[][] uenvelope3= Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope", "존,면적,Ueff", "외피유형='" + "지붕" + "'");
            //string[][] uenvelope4 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope", "존,면적,Ueff", "외피유형='" + "창호" + "'");
            //string[][] uenvelope5 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope", "존,면적,Ueff", "외피유형='" + "최하층바닥" + "'");

            List<object> items = new List<object>();
            List<object> data = new List<object>();
            List<object> _data = new List<object>();
            List<object> _data2 = new List<object>();
            List<object> _data3 = new List<object>();
            List<object> _data4 = new List<object>();
            List<object> _data5 = new List<object>();
            List<object> _data6 = new List<object>();
            List<object> _data7 = new List<object>();
            List<object> _data8 = new List<object>();
            List<object> _data9 = new List<object>();
            List<object> _data10 = new List<object>();
            List<object> _data11 = new List<object>();
            List<object> _data12 = new List<object>();
            List<object> _data13 = new List<object>();
            List<object> _data14 = new List<object>();
            List<object> _data15 = new List<object>();
            List<object> _data16 = new List<object>();
            List<object> _data17 = new List<object>();
            List<object> _data18 = new List<object>();
            List<object> _data19 = new List<object>();
            List<object> _data20 = new List<object>();
            List<object> _data21 = new List<object>();
            List<object> _data22 = new List<object>();
            List<object> _data23 = new List<object>();
            List<object> _data24 = new List<object>();
            List<object> _data25 = new List<object>();
            List<object> _data26 = new List<object>();
            List<object> _data27 = new List<object>();
            List<object> _data28 = new List<object>();
            List<object> _data29 = new List<object>();
            List<object> _data30 = new List<object>();
            List<object> _data31 = new List<object>();
            List<object> _data32 = new List<object>();
            List<object> _data33 = new List<object>();
            List<object> _data34 = new List<object>();
            List<object> _data35 = new List<object>();
            List<object> _data36 = new List<object>();
            List<object> _data37 = new List<object>();
            List<object> _data38 = new List<object>();
            List<object> _data39 = new List<object>();
            List<object> _data40 = new List<object>();
            List<object> _data41 = new List<object>();
            List<object> _data42 = new List<object>();
            List<object> _data43 = new List<object>();
            List<object> _data44 = new List<object>();
            List<object> _data45 = new List<object>();
            List<object> _data46 = new List<object>();
            List<object> _data47 = new List<object>();
            List<object> _data48 = new List<object>();
            List<object> _data49 = new List<object>();
            List<object> _data50 = new List<object>();
            List<object> _data51 = new List<object>();
            List<object> _data52 = new List<object>();
            List<object> _data53 = new List<object>();
            List<object> _data54 = new List<object>();
            List<object> _data55 = new List<object>();
            List<object> _data56 = new List<object>();
            List<object> _data57 = new List<object>();
            List<object> _data58 = new List<object>();
            List<object> _data59 = new List<object>();
            List<object> _data60 = new List<object>();
            List<object> _data61 = new List<object>();
            List<object> _data62 = new List<object>();
            List<object> _data63 = new List<object>();
            List<object> _data64 = new List<object>();
            List<object> _data65 = new List<object>();
            List<object> _data66 = new List<object>();
            List<object> _data67 = new List<object>();
            List<object> _data68 = new List<object>();
            List<object> _data69 = new List<object>();
            List<object> _data70 = new List<object>();
            List<object> _data71 = new List<object>();
            List<object> _data72 = new List<object>();
            List<object> _data73 = new List<object>();
            List<object> _data74 = new List<object>();
            List<object> _data75 = new List<object>();
            List<object> _data76 = new List<object>();
            List<object> _data77 = new List<object>();
            List<object> _data78 = new List<object>();
            List<object> _data79 = new List<object>();
            List<object> _data80 = new List<object>(); 
            List<object> _data81 = new List<object>();
            List<object> _data82 = new List<object>();
            List<object> _data83 = new List<object>();
            List<object> _data84 = new List<object>();
            List<object> _data85 = new List<object>();
            List<object> _data86 = new List<object>();
            List<object> _data87 = new List<object>();
            List<object> _data88 = new List<object>();
            List<object> _data89 = new List<object>();
            List<object> _data90 = new List<object>();
            List<object> _data91 = new List<object>();
            List<object> _data92 = new List<object>();
            List<object> _data93 = new List<object>();
            List<object> _data94 = new List<object>();
            List<object> _data95 = new List<object>();
            List<object> _data96 = new List<object>();
            List<object> _data97 = new List<object>();
            List<object> _data98 = new List<object>();
            List<object> _data99 = new List<object>();
            List<object> _data100 = new List<object>();
            List<object> _data101 = new List<object>();
            List<object> _data102 = new List<object>();

            List<object> _data103 = new List<object>();
            List<object> _data104 = new List<object>();
            List<object> _data105 = new List<object>();
            List<object> _data106 = new List<object>();
            List<object> _data107 = new List<object>();
            List<object> _data108 = new List<object>();
            List<object> _data109 = new List<object>();
            List<object> _data110 = new List<object>();
            List<object> _data111 = new List<object>();
            List<object> _data112 = new List<object>();
            List<object> _data113 = new List<object>();
            List<object> _data114 = new List<object>();

            List<object> _data115 = new List<object>();
            List<object> _data116 = new List<object>();
            List<object> _data117 = new List<object>();
            List<object> _data118 = new List<object>();
            List<object> _data119 = new List<object>();
            List<object> _data120 = new List<object>();
            List<object> _data121 = new List<object>();
            List<object> _data122 = new List<object>();
            List<object> _data123 = new List<object>();
            List<object> _data124 = new List<object>();
            List<object> _data125 = new List<object>();
            List<object> _data126 = new List<object>();

            List<object> _data127 = new List<object>();
            List<object> _data128 = new List<object>();
            List<object> _data129 = new List<object>();
            List<object> _data130 = new List<object>();
            List<object> _data131 = new List<object>();
            List<object> _data132 = new List<object>();
            List<object> _data133 = new List<object>();
            List<object> _data134 = new List<object>();
            List<object> _data135 = new List<object>();
            List<object> _data136 = new List<object>();
            List<object> _data137 = new List<object>();
            List<object> _data138 = new List<object>();
            List<object> _data139 = new List<object>();
            List<object> _data140 = new List<object>();
            List<object> _data141 = new List<object>();
            List<object> _data142 = new List<object>();
            List<object> _data143 = new List<object>();
            List<object> _data144 = new List<object>();
            List<object> _data145 = new List<object>();
            List<object> _data146 = new List<object>();
            List<object> _data147 = new List<object>();
            List<object> _data148 = new List<object>();
            List<object> _data149 = new List<object>();
            List<object> _data150 = new List<object>();
            List<object> _data151 = new List<object>();
            List<object> _data152 = new List<object>();
            List<object> _data153 = new List<object>();
            List<object> _data154 = new List<object>();
            List<object> _data155 = new List<object>();
            List<object> _data156 = new List<object>();
            List<object> _data157 = new List<object>();
            List<object> _data158 = new List<object>();
            List<object> _data159 = new List<object>();
            List<object> _data160 = new List<object>();
            List<object> _data161 = new List<object>();
            List<object> _data162 = new List<object>();
            List<object> _data163 = new List<object>();
            List<object> _data164 = new List<object>();
            List<object> _data165 = new List<object>();
            List<object> _data166 = new List<object>();
            List<object> _data167 = new List<object>();
            List<object> _data168 = new List<object>();
            List<object> _data169 = new List<object>();
            List<object> _data170 = new List<object>();
            List<object> _data171 = new List<object>();
            List<object> _data172 = new List<object>();
            List<object> _data173 = new List<object>();
            List<object> _data174 = new List<object>();
            List<object> _data175 = new List<object>();
            List<object> _data176 = new List<object>();
            List<object> _data177 = new List<object>();
            List<object> _data178 = new List<object>();
            List<object> _data179 = new List<object>();
            List<object> _data180 = new List<object>();
            List<object> _data181 = new List<object>();
            List<object> _data182 = new List<object>();
            List<object> _data183 = new List<object>();
            List<object> _data184 = new List<object>();
            List<object> _data185 = new List<object>();
            List<object> _data186 = new List<object>();
            List<object> _data187 = new List<object>();
            List<object> _data188 = new List<object>();
            List<object> _data189 = new List<object>();
            List<object> _data190 = new List<object>();
            List<object> _data191 = new List<object>();
            List<object> _data192 = new List<object>();
            List<object> _data193 = new List<object>();
            List<object> _data194 = new List<object>();
            List<object> _data195 = new List<object>();
            List<object> _data196 = new List<object>();
            List<object> _data197 = new List<object>();
            List<object> _data198 = new List<object>();
            List<object> _data199 = new List<object>();
            List<object> _data200 = new List<object>();
            List<object> _data201 = new List<object>();
            List<object> _data202 = new List<object>();
            List<object> _data203 = new List<object>();
            List<object> _data204 = new List<object>();
            List<object> _data205 = new List<object>();
            List<object> _data206 = new List<object>();
            List<object> _data207 = new List<object>();
            List<object> _data208 = new List<object>();
            List<object> _data209 = new List<object>();
            List<object> _data210 = new List<object>();
            List<object> _data211 = new List<object>();
            List<object> _data212 = new List<object>();
            List<object> _data213 = new List<object>();
            List<object> _data214 = new List<object>();
            List<object> _data215 = new List<object>();
            List<object> _data216 = new List<object>();
            List<object> _data217 = new List<object>();
            List<object> _data218 = new List<object>();
            List<object> _data219 = new List<object>();
            List<object> _data220 = new List<object>();
            List<object> _data221 = new List<object>();
            List<object> _data222 = new List<object>();
            List<object> _data223 = new List<object>();
            List<object> _data224 = new List<object>();
            List<object> _data225 = new List<object>();
            List<object> _data226 = new List<object>();
            List<object> _data227 = new List<object>();
            List<object> _data228 = new List<object>();
            List<object> _data229 = new List<object>();
            List<object> _data230 = new List<object>();
            List<object> _data231 = new List<object>();
            List<object> _data232 = new List<object>();
            List<object> _data233 = new List<object>();
            List<object> _data234 = new List<object>();
            List<object> _data235 = new List<object>();
            List<object> _data236 = new List<object>();
            List<object> _data237 = new List<object>();
            List<object> _data238 = new List<object>();
            List<object> _data239 = new List<object>();
            List<object> _data240 = new List<object>();
            List<object> _data241 = new List<object>();
            List<object> _data242 = new List<object>();
            List<object> _data243 = new List<object>();
            List<object> _data244 = new List<object>();
            List<object> _data245 = new List<object>();
            List<object> _data246 = new List<object>();
            List<object> _data247 = new List<object>();
            List<object> _data248 = new List<object>();
            List<object> _data249 = new List<object>();
            List<object> _data250 = new List<object>();
            List<object> _data251 = new List<object>();
            List<object> _data252 = new List<object>();
            List<object> _data253 = new List<object>();
            List<object> _data254 = new List<object>();
            List<object> _data255 = new List<object>();
            List<object> _data256 = new List<object>();
            List<object> _data257 = new List<object>();
            List<object> _data258 = new List<object>();
            List<object> _data259 = new List<object>();
            List<object> _data260 = new List<object>();
            List<object> _data261 = new List<object>();
            List<object> _data262 = new List<object>();
            List<object> _data263 = new List<object>();
            List<object> _data264 = new List<object>();
            List<object> _data265 = new List<object>();
            List<object> _data266 = new List<object>();
            List<object> _data267 = new List<object>();
            List<object> _data268 = new List<object>();
            List<object> _data269 = new List<object>();
            List<object> _data270 = new List<object>();
            List<object> _data271 = new List<object>();
            List<object> _data272 = new List<object>();
            List<object> _data273 = new List<object>();
            List<object> _data274 = new List<object>();
            List<object> _data275 = new List<object>();
            List<object> _data276 = new List<object>();
            List<object> _data277 = new List<object>();
            List<object> _data278 = new List<object>();
            List<object> _data279 = new List<object>();
            List<object> _data280 = new List<object>();
            List<object> _data281 = new List<object>();
            List<object> _data282 = new List<object>();
            List<object> _data283 = new List<object>();
            List<object> _data284 = new List<object>();
            List<object> _data285 = new List<object>();
            List<object> _data286 = new List<object>();
            List<object> _data287 = new List<object>();
            List<object> _data288= new List<object>();
            List<object> _data289 = new List<object>();
            List<object> _data290 = new List<object>();
            List<object> _data291 = new List<object>();
            List<object> _data292 = new List<object>();
            List<object> _data293 = new List<object>();
            List<object> _data294 = new List<object>();
            List<object> _data295 = new List<object>();
            List<object> _data296 = new List<object>();
            List<object> _data297 = new List<object>();
            List<object> _data298 = new List<object>();
            List<object> _data299 = new List<object>();
            List<object> _data300 = new List<object>();
            List<object> _data301 = new List<object>();
            List<object> _data302 = new List<object>();
            List<object> _data303 = new List<object>();
            List<object> _data304 = new List<object>();
            List<object> _data305 = new List<object>();
            List<object> _data306 = new List<object>();
            List<object> _data307 = new List<object>();
            List<object> _data308 = new List<object>();
            List<object> _data309 = new List<object>();
            List<object> _data310 = new List<object>();
            List<object> _data311 = new List<object>();
            List<object> _data312 = new List<object>();
            List<object> _data313 = new List<object>();
            List<object> _data314 = new List<object>();
            List<object> _data315 = new List<object>();
            List<object> _data316 = new List<object>();
            List<object> _data317 = new List<object>();
            List<object> _data318 = new List<object>();
            List<object> _data319 = new List<object>();
            List<object> _data320 = new List<object>();
            List<object> _data321 = new List<object>();
            List<object> _data322 = new List<object>();
            List<object> _data323 = new List<object>();
            List<object> _data324 = new List<object>();
            List<object> _data325 = new List<object>();
            List<object> _data326 = new List<object>();
            List<object> _data327 = new List<object>();
            List<object> _data328 = new List<object>();
            List<object> _data329 = new List<object>();
            List<object> _data330 = new List<object>();
            List<object> _data331 = new List<object>();
            List<object> _data332 = new List<object>();
            List<object> _data333 = new List<object>();
            List<object> _data334 = new List<object>();
            List<object> _data335 = new List<object>();
            List<object> _data336 = new List<object>();
            List<object> _data337 = new List<object>();
            List<object> _data338 = new List<object>();
            List<object> _data339 = new List<object>();
            List<object> _data340 = new List<object>();
            List<object> _data341 = new List<object>();
            List<object> _data342 = new List<object>();
            List<object> _data343 = new List<object>();
            List<object> _data344 = new List<object>();
            List<object> _data345 = new List<object>();
            List<object> _data346 = new List<object>();
            List<object> _data347 = new List<object>();
            List<object> _data348 = new List<object>();
            List<object> _data349 = new List<object>();
            List<object> _data350 = new List<object>();
            List<object> _data351 = new List<object>();
            List<object> _data352 = new List<object>();
            List<object> _data353 = new List<object>();
            List<object> _data354 = new List<object>();
            List<object> _data355 = new List<object>();
            List<object> _data356 = new List<object>();
            List<object> _data357 = new List<object>();
            List<object> _data358 = new List<object>();
            List<object> _data359 = new List<object>();
            List<object> _data360 = new List<object>();
            List<object> _data361 = new List<object>();
            List<object> _data362 = new List<object>();
            List<object> _data363 = new List<object>();
            List<object> _data364 = new List<object>();
            List<object> _data365 = new List<object>();
            List<object> _data366 = new List<object>();
            List<object> _data367 = new List<object>();
            List<object> _data368 = new List<object>();
            List<object> _data369 = new List<object>();
            List<object> _data370 = new List<object>();
            List<object> _data371 = new List<object>();
            List<object> _data372 = new List<object>();
            List<object> _data373 = new List<object>();
            List<object> _data374 = new List<object>();
            List<object> _data375 = new List<object>();
            List<object> _data376 = new List<object>();
            List<object> _data377 = new List<object>();
            List<object> _data378 = new List<object>();
            List<object> _data379 = new List<object>();
            List<object> _data380 = new List<object>();
            List<object> _data381 = new List<object>();
            List<object> _data382 = new List<object>();
            List<object> _data383 = new List<object>();
            List<object> _data384 = new List<object>();
            List<object> _data385 = new List<object>();
            List<object> _data386 = new List<object>();
            List<object> _data387 = new List<object>();
            List<object> _data388 = new List<object>();
            List<object> _data389 = new List<object>();
            List<object> _data390 = new List<object>();
            List<object> _data391 = new List<object>();
            List<object> _data392 = new List<object>();
            List<object> _data393 = new List<object>();
            List<object> _data394 = new List<object>();
            List<object> _data395 = new List<object>();
            List<object> _data396 = new List<object>();
            List<object> _data397 = new List<object>();
            List<object> _data398 = new List<object>();
            List<object> _data399 = new List<object>();
            List<object> _data400 = new List<object>();
            List<object> _data401 = new List<object>();
            List<object> _data402 = new List<object>();
            List<object> _data403 = new List<object>();
            List<object> _data404 = new List<object>();
            List<object> _data405 = new List<object>();
            List<object> _data406 = new List<object>();
            List<object> _data407 = new List<object>();
            List<object> _data408 = new List<object>();
            List<object> _data409 = new List<object>();
            List<object> _data410 = new List<object>();
            List<object> _data411 = new List<object>();
            List<object> _data412 = new List<object>();
            List<object> _data413 = new List<object>();
            List<object> _data414 = new List<object>();
            List<object> _data415 = new List<object>();
            List<object> _data416 = new List<object>();
            List<object> _data417 = new List<object>();
            List<object> _data418 = new List<object>();
            List<object> _data419 = new List<object>();
            List<object> _data420 = new List<object>();
            List<object> _data421 = new List<object>();
            List<object> _data422 = new List<object>();
            List<object> _data423 = new List<object>();
            List<object> _data424 = new List<object>();
            List<object> _data425 = new List<object>();
            List<object> _data426 = new List<object>();
            List<object> _data427 = new List<object>();
            List<object> _data428 = new List<object>();
            List<object> _data429 = new List<object>();
            List<object> _data430 = new List<object>();
            List<object> _data431 = new List<object>();
            List<object> _data432 = new List<object>();
            List<object> _data433 = new List<object>();
            List<object> _data434 = new List<object>();
            List<object> _data435 = new List<object>();
            List<object> _data436 = new List<object>();
            List<object> _data437 = new List<object>();
            List<object> _data438 = new List<object>();
            List<object> _data439 = new List<object>();
            List<object> _data440 = new List<object>();
            List<object> _data441 = new List<object>();
            List<object> _data442 = new List<object>();
            List<object> _data443 = new List<object>();
            List<object> _data444 = new List<object>();
            List<object> _data445 = new List<object>();
            List<object> _data446 = new List<object>();
            List<object> _data447 = new List<object>();
            List<object> _data448 = new List<object>();
            List<object> _data449 = new List<object>();
            List<object> _data450 = new List<object>();
            List<object> _data451 = new List<object>();
            List<object> _data452 = new List<object>();
            List<object> _data453 = new List<object>();
            List<object> _data454 = new List<object>();
            List<object> _data455 = new List<object>();
            List<object> _data456 = new List<object>();
            List<object> _data457 = new List<object>();
            List<object> _data458 = new List<object>();
            List<object> _data459 = new List<object>();
            List<object> _data460 = new List<object>();
            List<object> _data461 = new List<object>();
            List<object> _data462 = new List<object>();
            List<object> _data463 = new List<object>();
            List<object> _data464 = new List<object>();
            List<object> _data465 = new List<object>();
            List<object> _data466 = new List<object>();
            List<object> _data467 = new List<object>();
            List<object> _data468 = new List<object>();
            List<object> _data469 = new List<object>();
            List<object> _data470 = new List<object>();
            List<object> _data471 = new List<object>();
            List<object> _data472 = new List<object>();
            List<object> _data473 = new List<object>();
            List<object> _data474 = new List<object>();
            List<object> _data475 = new List<object>();
            List<object> _data476 = new List<object>();
            List<object> _data477 = new List<object>();
            List<object> _data478 = new List<object>();
            List<object> _data479 = new List<object>();
            List<object> _data480 = new List<object>();
            List<object> _data481 = new List<object>();
            List<object> _data482 = new List<object>();
            List<object> _data483 = new List<object>();
            List<object> _data484 = new List<object>();
            List<object> _data485 = new List<object>();
            List<object> _data486 = new List<object>();
            List<object> _data487 = new List<object>();
            List<object> _data488 = new List<object>();
            List<object> _data489 = new List<object>();
            List<object> _data490 = new List<object>();
            List<object> _data491 = new List<object>();
            List<object> _data492 = new List<object>();
            List<object> _data493 = new List<object>();
            List<object> _data494 = new List<object>();
            List<object> _data495 = new List<object>();
            List<object> _data496 = new List<object>();
            List<object> _data497 = new List<object>();
            List<object> _data498 = new List<object>();
            List<object> _data499 = new List<object>();
            List<object> _data500 = new List<object>();
            List<object> _data501 = new List<object>();
            List<object> _data502 = new List<object>();
            List<object> _data503 = new List<object>();
            List<object> _data504 = new List<object>();
            List<object> _data505 = new List<object>();
            List<object> _data506 = new List<object>();
            List<object> _data507 = new List<object>();
            List<object> _data508 = new List<object>();
            List<object> _data509 = new List<object>();
            List<object> _data510 = new List<object>();
            List<object> _data511 = new List<object>();
            List<object> _data512 = new List<object>();
            List<object> _data513 = new List<object>();
            List<object> _data514 = new List<object>();
            List<object> _data515 = new List<object>();
            List<object> _data516 = new List<object>();
            List<object> _data517 = new List<object>();
            List<object> _data518 = new List<object>();
            List<object> _data519 = new List<object>();
            List<object> _data520 = new List<object>();
            List<object> _data521 = new List<object>();
            List<object> _data522 = new List<object>();
            List<object> _data523 = new List<object>();
            List<object> _data524 = new List<object>();
            List<object> _data525 = new List<object>();
            List<object> _data526 = new List<object>();
            List<object> _data527 = new List<object>();
            List<object> _data528 = new List<object>();
            List<object> _data529 = new List<object>();
            List<object> _data530 = new List<object>();
            List<object> _data531 = new List<object>();
            List<object> _data532 = new List<object>();
            List<object> _data533 = new List<object>();
            List<object> _data534 = new List<object>();
            List<object> _data535 = new List<object>();
            List<object> _data536 = new List<object>();
            List<object> _data537 = new List<object>();
            List<object> _data538 = new List<object>();
            List<object> _data539 = new List<object>();
            List<object> _data540 = new List<object>();
            List<object> _data541 = new List<object>();
            List<object> _data542 = new List<object>();
            List<object> _data543 = new List<object>();
            List<object> _data544 = new List<object>();
            List<object> _data545 = new List<object>();
            List<object> _data546 = new List<object>();
            List<object> _data547 = new List<object>();
            List<object> _data548 = new List<object>();
            List<object> _data549 = new List<object>();
            List<object> _data550 = new List<object>();
            List<object> _data551 = new List<object>();
            List<object> _data552 = new List<object>();
            List<object> _data553 = new List<object>();
            List<object> _data554 = new List<object>();
            List<object> _data555 = new List<object>();
            List<object> _data556 = new List<object>();
            List<object> _data557 = new List<object>();
            List<object> _data558 = new List<object>();
            List<object> _data559 = new List<object>();
            List<object> _data560 = new List<object>();

            List<object> _data561 = new List<object>();
            List<object> _data562 = new List<object>();
            List<object> _data563 = new List<object>();
            List<object> _data564 = new List<object>();
            List<object> _data565 = new List<object>();
            List<object> _data566 = new List<object>();
            List<object> _data567 = new List<object>();
            List<object> _data568 = new List<object>();
            List<object> _data569 = new List<object>();
            List<object> _data570 = new List<object>();
            List<object> _data571 = new List<object>();
            List<object> _data572 = new List<object>();
            List<object> _data573 = new List<object>();
            List<object> _data574 = new List<object>();
            List<object> _data575 = new List<object>();
            List<object> _data576 = new List<object>();
            List<object> _data577 = new List<object>();
            List<object> _data578 = new List<object>();
            List<object> _data579 = new List<object>();
            List<object> _data580 = new List<object>();

            List<object> _data581 = new List<object>();
            List<object> _data582 = new List<object>();
            List<object> _data583 = new List<object>();
            List<object> _data584 = new List<object>();
            List<object> _data585 = new List<object>();
            List<object> _data586 = new List<object>();
            List<object> _data587 = new List<object>();
            List<object> _data588 = new List<object>();
            List<object> _data589 = new List<object>();
            List<object> _data590 = new List<object>();
            List<object> _data591 = new List<object>();
            List<object> _data592 = new List<object>();
            List<object> _data593 = new List<object>();
            List<object> _data594 = new List<object>();
            List<object> _data595 = new List<object>();
            List<object> _data596 = new List<object>();
            List<object> _data597 = new List<object>();
            List<object> _data598 = new List<object>();
            List<object> _data599 = new List<object>();
            List<object> _data600 = new List<object>();
            List<object> _data601 = new List<object>();
            List<object> _data602 = new List<object>();
            List<object> _data603 = new List<object>();
            List<object> _data604 = new List<object>();
            List<object> _data605 = new List<object>();
            List<object> _data606 = new List<object>();
            List<object> _data607 = new List<object>();
            List<object> _data608 = new List<object>();
            List<object> _data609 = new List<object>();
            List<object> _data610 = new List<object>();
            List<object> _data611 = new List<object>();
            List<object> _data612 = new List<object>();
            List<object> _data613 = new List<object>();
            List<object> _data614 = new List<object>();
            List<object> _data615 = new List<object>();
            List<object> _data616 = new List<object>();
            List<object> _data617 = new List<object>();
            List<object> _data618 = new List<object>();
            List<object> _data619 = new List<object>();
            List<object> _data620 = new List<object>();
            List<object> _data621 = new List<object>();
            List<object> _data622 = new List<object>();
            List<object> _data623 = new List<object>();
            List<object> _data624 = new List<object>();
            List<object> _data625 = new List<object>();
            List<object> _data626 = new List<object>();
            List<object> _data627 = new List<object>();
            List<object> _data628 = new List<object>();
            List<object> _data629 = new List<object>();
            List<object> _data630 = new List<object>();
            List<object> _data631 = new List<object>();
            List<object> _data632 = new List<object>();
            List<object> _data633 = new List<object>();
            List<object> _data634 = new List<object>();
            List<object> _data635 = new List<object>();
            List<object> _data636 = new List<object>();
            List<object> _data637 = new List<object>();
            List<object> _data638 = new List<object>();
            List<object> _data639 = new List<object>();
            List<object> _data640 = new List<object>();




            int i = -1;
            
             while (++i < ZoneG.Length)
             {

                items.Add("zprint7.html"); // 예시 코드: 메인 메뉴 동적 할당

                _data.Add(new { idx = i, val = ZoneG[i][8] });
                _data2.Add(new { idx = i, val = Program.UTIL.asFixed(ZoneG[i][26]) });
                _data3.Add(new { idx = i, val = ZoneG[i][0] });
                _data4.Add(new { idx = i, val = ZoneG[i][1] });

                if (ZoneG[i][0] == value[i * 49][0])
                {
                    _data5.Add(new { idx = i, val = value[i * 49][1] });
                    _data6.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][2]) }); //HT_wall
                    _data7.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][3] )}); //HT_Roof
                    _data8.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][4] )}); //HT_Floor
                    _data9.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][5] )}); //HT_S
                    _data10.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][6] )});//HT_Door
                    _data11.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][7] )}); //HT_WIN
                    _data12.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][8] )}); //HT_CW
                    _data13.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][9]) }); //HT_Di_Wall
                    _data14.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][10]) }); //HT_Indi_wall
                    _data15.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][11] )}); //HT_Di_Roof
                    _data16.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][12] )});//HT_Indi_Roof
                    _data17.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][13] )});//HT_Di_Win
                    _data18.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][14] )});//HT_Indi_Win
                    _data19.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][15] )});//HT_Di_Door
                    _data20.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][16] )});//HT_Indi_Door
                    _data21.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][17] )});//HT_TB_tot
                    _data22.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][18] )});//HT_TB_Wall
                    _data23.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][19] )});//HT_TB_Roof
                    _data24.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][20] )});//HT_TB_Floor
                    _data25.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][21] )});//HT_TB_Gwall
                    _data26.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][22] )});//HT_TB_Win
                    _data27.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][23] )});//HT_TB_Door
                    _data28.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][24] )});//HT_TB_CW
                    _data29.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][25] )});//Hv_tot
                    _data30.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][26])});//Hv_inf (비이용일)
                    _data31.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][27]) });//Hv_win (비이용일)
                    _data32.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][28] )});//HV_Z (비이용일)
                    _data33.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][29] )});//Hv_mech (비이용일) 
                    _data34.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][30] )});//H_tot (비이용일)
                    _data35.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 49][31] )});//tao(비이용일)
                }

                else;

                if (ZoneG[i][0] == value[i * 37][0])
                {
                    _data36.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 37][26]) });//Hv_inf (이용일)
                    _data37.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 37][27]) });//Hv_win (이용일)
                    _data38.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 37][28]) });//HV_Z (이용일)
                    _data39.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 37][29]) });//Hv_mech (이용일) 
                    _data40.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 37][30]) });//H_tot (이용일)
                    _data41.Add(new { idx = i, val = Program.UTIL.asFixed(value[i * 37][31]) });//tao (이용일)
                }

                else;

                if (ZoneG[i][0] == value[48 * i + 12][0])
                {
                    _data42.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][33]) });//dwd_mth (이용일_난방1월)
                    _data43.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][33]) });//dwd_mth (이용일_난방2월)
                    _data44.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][33]) });//dwd_mth (이용일_난방3월)
                    _data45.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][33]) });//dwd_mth (이용일_난방4월)
                    _data46.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][33]) });//dwd_mth (이용일_난방5월)
                    _data47.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][33]) });//dwd_mth (이용일_난방6월)
                    _data48.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][33]) });//dwd_mth (이용일_난방7월)
                    _data49.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][33]) });//dwd_mth (이용일_난방8월)
                    _data50.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][33]) });//dwd_mth (이용일_난방9월)
                    _data51.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][33]) });//dwd_mth (이용일_난방10월)
                    _data52.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][33]) });//dwd_mth (이용일_난방11월)
                    _data53.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][33]) });//dwd_mth (이용일_난방12월)

                    _data54.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][34]) });//theta (이용일 실내온도_난방1월)
                    _data55.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][34]) });//theta (이용일 실내온도_난방2월)
                    _data56.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][34]) });//theta (이용일 실내온도_난방3월)
                    _data57.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][34]) });//theta (이용일 실내온도_난방4월)
                    _data58.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][34]) });//theta (이용일 실내온도_난방5월)
                    _data59.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][34]) });//theta (이용일 실내온도_난방6월)
                    _data60.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][34]) });//theta (이용일 실내온도_난방7월)
                    _data61.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][34]) });//theta (이용일 실내온도_난방8월)
                    _data62.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][34]) });//theta (이용일 실내온도_난방9월)
                    _data63.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][34]) });//theta (이용일 실내온도_난방10월)
                    _data64.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][34]) });//theta (이용일 실내온도_난방11월)
                    _data65.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][34]) });//theta (이용일 실내온도_난방12월)

                    _data66.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][93]) });//Qhw_dw (이용일 난방요구량_난방1월)
                    _data67.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][93]) });//Qhw_dw (이용일 난방요구량_난방2월)
                    _data68.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][93]) });//Qhw_dw (이용일 난방요구량_난방3월)
                    _data69.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][93]) });//Qhw_dw (이용일 난방요구량_난방4월)
                    _data70.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][93]) });//Qhw_dw (이용일 난방요구량_난방5월)
                    _data71.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][93]) });//Qhw_dw (이용일 난방요구량_난방6월)
                    _data72.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][93]) });//Qhw_dw (이용일 난방요구량_난방7월)
                    _data73.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][93]) });//Qhw_dw (이용일 난방요구량_난방8월)
                    _data74.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][93]) });//Qhw_dw (이용일 난방요구량_난방9월)
                    _data75.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][93]) });//Qhw_dw (이용일 난방요구량_난방10월)
                    _data76.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][93]) });//Qhw_dw (이용일 난방요구량_난방11월)
                    _data77.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][93]) });//Qhw_dw (이용일 난방요구량_난방12월)
                    _data78.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][93]) });//Qhw_dw (이용일 난방요구량_난방 연간 합산) 수정 필요

                    _data80.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][35]) });//theta e(외부온도_난방1월)
                    _data81.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][35]) });//theta e(외부온도_난방2월)
                    _data82.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][35]) });//theta e(외부온도_난방3월)
                    _data83.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][35]) });//theta e(외부온도_난방4월)
                    _data84.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][35]) });//theta e(외부온도_난방5월)
                    _data85.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][35]) });//theta e(외부온도_난방6월)
                    _data86.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][35]) });//theta e(외부온도_난방7월)
                    _data87.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][35]) });//theta e(외부온도_난방8월)
                    _data88.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][35]) });//theta e(외부온도_난방9월)
                    _data89.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][35]) });//theta e(외부온도_난방10월)
                    _data90.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][35]) });//theta e(외부온도_난방11월)
                    _data91.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][35]) });//theta e(외부온도_난방12월)

                    _data91.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][83]) });//eta(이용계수_난방1월)
                    _data92.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][83]) });//eta(이용계수_난방2월)
                    _data93.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][83]) });//eta(이용계수_난방3월)
                    _data94.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][83]) });//eta(이용계수_난방4월)
                    _data95.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][83]) });//eta(이용계수_난방5월)
                    _data96.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][83]) });//eta(이용계수_난방6월)
                    _data97.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][83]) });//eta(이용계수_난방7월)
                    _data98.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][83]) });//eta(이용계수_난방8월)
                    _data99.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][83]) });//eta(이용계수_난방9월)
                    _data100.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][83]) });//eta(이용계수_난방10월)
                    _data101.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][83]) });//eta(이용계수_난방11월)
                    _data102.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][83]) });//eta(이용계수_난방12월)

                    _data103.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][36]) });//QTsink(관류열손실_난방1월)
                    _data104.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][36]) });//QTsink(관류열손실_난방2월)
                    _data105.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][36]) });//QTsink(관류열손실_난방3월)
                    _data106.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][36]) });//QTsink(관류열손실_난방4월)
                    _data107.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][36]) });//QTsink(관류열손실_난방5월)
                    _data108.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][36]) });//QTsink(관류열손실_난방6월)
                    _data109.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][36]) });//QTsink(관류열손실_난방7월)
                    _data110.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][36]) });//QTsink(관류열손실_난방8월)
                    _data111.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][36]) });//QTsink(관류열손실_난방9월)
                    _data112.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][36]) });//QTsink(관류열손실_난방10월)
                    _data113.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][36]) });//QTsink(관류열손실_난방11월)
                    _data114.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][36]) });//QTsink(관류열손실_난방12월)

                    _data115.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][37]) });//QTsink wall(벽체열손실_난방1월)
                    _data116.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][37]) });//QTsink wall(벽체열손실_난방2월)
                    _data117.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][37]) });//QTsink wall(벽체열손실_난방3월)
                    _data118.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][37]) });//QTsink wall(벽체열손실_난방4월)
                    _data119.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][37]) });//QTsink wall(벽체열손실_난방5월)
                    _data120.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][37]) });//QTsink wall(벽체열손실_난방6월)
                    _data121.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][37]) });//QTsink wall(벽체열손실_난방7월)
                    _data122.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][37]) });//QTsink wall(벽체열손실_난방8월)
                    _data123.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][37]) });//QTsink wall(벽체열손실_난방9월)
                    _data124.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][37]) });//QTsink wall(벽체열손실_난방10월)
                    _data125.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][37]) });//QTsink wall(벽체열손실_난방11월)
                    _data126.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][37]) });//QTsink wall(벽체열손실_난방12월)

                    _data127.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][42]) });//QTsink window(창호열손실_난방1월)
                    _data128.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][42]) });//QTsink window(창호손실_난방2월)
                    _data129.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][42]) });//QTsink window(창호열손실_난방3월)
                    _data130.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][42]) });//QTsink window(창호열손실_난방4월)
                    _data131.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][42]) });//QTsink window(창호열손실_난방5월)
                    _data132.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][42]) });//QTsink window(창호열손실_난방6월)
                    _data133.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][42]) });//QTsink window(창호열손실_난방7월)
                    _data134.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][42]) });//QTsink window(창호열손실_난방8월)
                    _data135.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][42]) });//QTsink window(창호열손실_난방9월)
                    _data136.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][42]) });//QTsink window(창호열손실_난방10월)
                    _data137.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][42]) });//QTsink window(칭호열손실_난방11월)
                    _data138.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][42]) });//QTsink window(창호열손실_난방12월)

                    _data139.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][43]) });//QTsink cw(커튼월열손실_난방1월)
                    _data140.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][43]) });//QTsink cw(커튼월열손실_난방2월)
                    _data141.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][43]) });//QTsink cw(커튼월열손실_난방3월)
                    _data142.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][43]) });//QTsink cw(커튼월열손실_난방4월)
                    _data143.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][43]) });//QTsink cw(커튼월열손실_난방5월)
                    _data144.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][43]) });//QTsink cw(커튼월열손실_난방6월)
                    _data145.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][43]) });//QTsink cw(커튼월열손실_난방7월)
                    _data146.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][43]) });//QTsink cw(커튼월열손실_난방8월)
                    _data147.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][43]) });//QTsink cw(커튼월열손실_난방9월)
                    _data148.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][43]) });//QTsink cw(커튼월열손실_난방10월)
                    _data149.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][43]) });//QTsink cw(커튼월열손실_난방11월)
                    _data150.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][43]) });//QTsink cw(커튼월열손실_난방12월)

                    _data151.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][38]) });//QTsink roof(지붕열손실_난방1월)
                    _data152.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][38]) });//QTsink roof(지붕열손실_난방2월)
                    _data153.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][38]) });//QTsink roof(지붕열손실_난방3월)
                    _data154.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][38]) });//QTsink roof(지붕열손실_난방4월)
                    _data155.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][38]) });//QTsink roof(지붕열손실_난방5월)
                    _data156.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][38]) });//QTsink roof(지붕열손실_난방6월)
                    _data157.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][38]) });//QTsink roof(지붕열손실_난방7월)
                    _data158.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][38]) });//QTsink roof(지붕열손실_난방8월)
                    _data159.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][38]) });//QTsink roof(지붕열손실_난방9월)
                    _data160.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][38]) });//QTsink roof(지붕열손실_난방10월)
                    _data161.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][38]) });//QTsink roof(지붕열손실_난방11월)
                    _data162.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][38]) });//QTsink roof(지붕열손실_난방12월)

                    _data163.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][41]) });//QTsink door(출입문열손실_난방1월)
                    _data164.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][41]) });//QTsink door(출입문열손실_난방2월)
                    _data165.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][41]) });//QTsink door(출입문열손실_난방3월)
                    _data166.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][41]) });//QTsink door(출입문열손실_난방4월)
                    _data167.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][41]) });//QTsink door(출입문열손실_난방5월)
                    _data168.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][41]) });//QTsink door(출입문열손실_난방6월)
                    _data169.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][41]) });//QTsink door(출입문열손실_난방7월)
                    _data170.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][41]) });//QTsink door(출입문열손실_난방8월)
                    _data171.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][41]) });//QTsink door(출입문열손실_난방9월)
                    _data172.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][41]) });//QTsink door(출입문열손실_난방10월)
                    _data173.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][41]) });//QTsink door(출입문열손실_난방11월)
                    _data174.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][41]) });//QTsink door(출입문열손실_난방12월)

                    _data175.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][39]) });//QTsink floor(바닥열손실_난방1월)
                    _data176.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][39]) });//QTsink floor(바닥열손실_난방2월)
                    _data177.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][39]) });//QTsink floor(바닥열손실_난방3월)
                    _data178.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][39]) });//QTsink floor(바닥열손실_난방4월)
                    _data179.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][39]) });//QTsink floor(바닥열손실_난방5월)
                    _data180.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][39]) });//QTsink floor(바닥열손실_난방6월)
                    _data181.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][39]) });//QTsink floor(바닥열손실_난방7월)
                    _data182.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][39]) });//QTsink floor(바닥열손실_난방8월)
                    _data183.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][39]) });//QTsink floor(바닥열손실_난방9월)
                    _data184.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][39]) });//QTsink floor(바닥열손실_난방10월)
                    _data185.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][39]) });//QTsink floor(바닥열손실_난방11월)
                    _data186.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][39]) });//QTsink floor(바닥열손실_난방12월)

                    //_data187.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][39]) });//QTsink 2D(2D 접합부 열손실_난방1월)
                    //_data188.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][39]) });//QTsink 2D(2D 접합부 열손실_난방1월)
                    //_data189.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][39]) });//QTsink 2D(2D 접합부 열손실_난방1월)
                    //_data190.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][39]) });//QTsink 2D(2D 접합부 열손실_난방1월)
                    //_data191.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][39]) });//QTsink 2D(2D 접합부 열손실_난방1월)
                    //_data192.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][39]) });//QTsink 2D(2D 접합부 열손실_난방1월)
                    //_data193.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][39]) });//QTsink 2D(2D 접합부 열손실_난방1월)
                    //_data194.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][39]) });//QTsink 2D(2D 접합부 열손실_난방1월)
                    //_data195.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][39]) });//QTsink 2D(2D 접합부 열손실_난방1월)
                    //_data196.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][39]) });//QTsink 2D(2D 접합부 열손실_난방1월)
                    //_data197.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][39]) });//QTsink 2D(2D 접합부 열손실_난방1월)
                    //_data198.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][39]) });//QTsink 2D(2D 접합부 열손실_난방1월)

                    _data199.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][65]) });//QVsink_tot(환기열손실_난방1월)
                    _data200.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][65]) });//QVsink_tot(환기열손실_난방2월)
                    _data201.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][65]) });//QVsink_tot(환기열손실_난방3월)
                    _data202.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][65]) });//QVsink_tot(환기열손실_난방4월)
                    _data203.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][65]) });//QVsink_tot(환기열손실_난방5월)
                    _data204.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][65]) });//QVsink_tot(환기열손실_난방6월)
                    _data205.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][65]) });//QVsink_tot(환기열손실_난방7월)
                    _data206.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][65]) });//QVsink_tot(환기열손실_난방8월)
                    _data207.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][65]) });//QVsink_tot(환기열손실_난방9월)
                    _data208.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][65]) });//QVsink_tot(환기열손실_난방10월)
                    _data209.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][65]) });//QVsink_tot(환기열손실_난방11월)
                    _data210.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][65]) });//QVsink_tot(환기열손실_난방12월)

                    _data211.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][66]) });//QVsink_inf(침기열손실_난방1월)
                    _data212.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][66]) });//QVsink_inf(침기열손실_난방2월)
                    _data213.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][66]) });//QVsink_inf(침기열손실_난방3월)
                    _data214.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][66]) });//QVsink_inf(침기열손실_난방4월)
                    _data215.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][66]) });//QVsink_inf(침기열손실_난방5월)
                    _data216.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][66]) });//QVsink_inf(침기열손실_난방6월)
                    _data217.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][66]) });//QVsink_inf(침기열손실_난방7월)
                    _data218.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][66]) });//QVsink_inf(침기열손실_난방8월)
                    _data219.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][66]) });//QVsink_inf(침기열손실_난방9월)
                    _data220.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][66]) });//QVsink_inf(침기열손실_난방10월)
                    _data221.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][66]) });//QVsink_inf(침기열손실_난방11월)
                    _data222.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][66]) });//QVsink_inf(침기열손실_난방12월)

                    _data223.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][69]) });//QVsink_mech(기계환기열손실_난방1월)
                    _data224.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][69]) });//QVsink_mech(기계환기열손실_난방2월)
                    _data225.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][69]) });//QVsink_mech(기계환기열손실_난방3월)
                    _data226.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][69]) });//QVsink_mech(기계환기열손실_난방4월)
                    _data227.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][69]) });//QVsink_mech(기계환기열손실_난방5월)
                    _data228.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][69]) });//QVsink_mech(기계환기열손실_난방6월)
                    _data229.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][69]) });//QVsink_mech(기계환기열손실_난방7월)
                    _data230.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][69]) });//QVsink_mech(기계환기열손실_난방8월)
                    _data231.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][69]) });//QVsink_mech(기계환기열손실_난방9월)
                    _data232.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][69]) });//QVsink_mech(기계환기열손실_난방10월)
                    _data233.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][69]) });//QVsink_mech(기계환기열손실_난방11월)
                    _data234.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][69]) });//QVsink_mech(기계환기열손실_난방12월)

                    _data235.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][67]) });//QVsink_win(자연환기열손실_난방1월)
                    _data236.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][67]) });//QVsink_win(자연환기열손실_난방2월)
                    _data237.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][67]) });//QVsink_win(자연환기열손실_난방3월)
                    _data238.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][67]) });//QVsink_win(자연환기열손실_난방4월)
                    _data239.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][67]) });//QVsink_win(자연환기열손실_난방5월)
                    _data240.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][67]) });//QVsink_win(자연환기열손실_난방6월)
                    _data241.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][67]) });//QVsink_win(자연환기열손실_난방7월)
                    _data242.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][67]) });//QVsink_win(자연환기열손실_난방8월)
                    _data243.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][67]) });//QVsink_win(자연환기열손실_난방9월)
                    _data244.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][67]) });//QVsink_win(자연환기열손실_난방10월)
                    _data245.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][67]) });//QVsink_win(자연환기열손실_난방11월)
                    _data246.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][67]) });//QVsink_win(자연환기열손실_난방12월)

                    _data247.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][54]) });//Qstr_tot(총 일사열획득_난방1월) 외벽 지붕 출입문 포함안된값 
                    _data248.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][54]) });//Qstr_tot(총 일사열획득_난방2월)
                    _data249.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][54]) });//Qstr_tot(총 일사열획득_난방3월)
                    _data250.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][54]) });//Qstr_tot(총 일사열획득_난방4월)
                    _data251.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][54]) });//Qstr_tot(총 일사열획득_난방5월)
                    _data252.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][54]) });//Qstr_tot(총 일사열획득_난방6월)
                    _data253.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][54]) });//Qstr_tot(총 일사열획득_난방7월)
                    _data254.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][54]) });//Qstr_tot(총 일사열획득_난방8월)
                    _data255.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][54]) });//Qstr_tot(총 일사열획득_난방9월)
                    _data256.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][54]) });//Qstr_tot(총 일사열획득_난방10월)
                    _data257.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][54]) });//Qstr_tot(총 일사열획득_난방11월)
                    _data258.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][54]) });//Qstr_tot(총 일사열획득_난방12월)

                    _data259.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][63]) });//Qstr_win(창호 일사열획득_난방1월) 
                    _data260.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][63]) });//Qstr_win(창호 일사열획득_난방2월) 
                    _data261.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][63]) });//Qstr_win(창호 일사열획득_난방3월) 
                    _data262.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][63]) });//Qstr_win(창호 일사열획득_난방4월) 
                    _data263.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][63]) });//Qstr_win(창호 일사열획득_난방5월) 
                    _data264.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][63]) });//Qstr_win(창호 일사열획득_난방6월) 
                    _data265.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][63]) });//Qstr_win(창호 일사열획득_난방7월) 
                    _data266.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][63]) });//Qstr_win(창호 일사열획득_난방8월) 
                    _data267.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][63]) });//Qstr_win(창호 일사열획득_난방9월) 
                    _data268.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][63]) });//Qstr_win(창호 일사열획득_난방10월) 
                    _data269.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][63]) });//Qstr_win(창호 일사열획득_난방11월) 
                    _data270.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][63]) });//Qstr_win(창호 일사열획득_난방12월) 

                    _data271.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][64]) });//Qstr_cw(커튼월 일사열획득_난방1월) 
                    _data272.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][64]) });//Qstr_cw(커튼월 일사열획득_난방2월) 
                    _data273.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][64]) });//Qstr_cw(커튼월 일사열획득_난방3월) 
                    _data274.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][64]) });//Qstr_cw(커튼월 일사열획득_난방4월) 
                    _data275.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][64]) });//Qstr_cw(커튼월 일사열획득_난방5월) 
                    _data276.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][64]) });//Qstr_cw(커튼월 일사열획득_난방6월) 
                    _data277.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][64]) });//Qstr_cw(커튼월 일사열획득_난방7월) 
                    _data278.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][64]) });//Qstr_cw(커튼월 일사열획득_난방8월) 
                    _data279.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][64]) });//Qstr_cw(커튼월 일사열획득_난방9월)  
                    _data280.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][64]) });//Qstr_cw(커튼월 일사열획득_난방10월) 
                    _data281.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][64]) });//Qstr_cw(커튼월 일사열획득_난방11월) 
                    _data282.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][64]) });//Qstr_cw(커튼월 일사열획득_난방12월) 

                    _data283.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][59]) });//QSopsource_Wall(외벽 일사열획득_난방1월) 
                    _data284.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][59]) });//QSopsource_Wall(외벽 일사열획득_난방2월) 
                    _data285.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][59]) });//QSopsource_Wall(외벽 일사열획득_난방3월) 
                    _data286.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][59]) });//QSopsource_Wall(외벽 일사열획득_난방4월) 
                    _data287.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][59]) });//QSopsource_Wall(외벽 일사열획득_난방5월) 
                    _data288.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][59]) });//QSopsource_Wall(외벽 일사열획득_난방6월) 
                    _data289.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][59]) });//QSopsource_Wall(외벽 일사열획득_난방7월) 
                    _data290.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][59]) });//QSopsource_Wall(외벽 일사열획득_난방8월) 
                    _data291.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][59]) });//QSopsource_Wall(외벽 일사열획득_난방9월)  
                    _data292.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][59]) });//QSopsource_Wall(외벽 일사열획득_난방10월) 
                    _data293.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][59]) });//QSopsource_Wall(외벽 일사열획득_난방11월) 
                    _data294.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][59]) });//QSopsource_Wall(외벽 일사열획득_난방12월) 

                    _data295.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][60]) });//QSopsource_Roof(지붕 일사열획득_난방1월) 
                    _data296.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][60]) });//QSopsource_Roof(지붕 일사열획득_난방2월)  
                    _data297.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][60]) });//QSopsource_Roof(지붕 일사열획득_난방3월) 
                    _data298.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][60]) });//QSopsource_Roof(지붕 일사열획득_난방4월) 
                    _data299.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][60]) });//QSopsource_Roof(지붕 일사열획득_난방5월) 
                    _data300.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][60]) });//QSopsource_Roof(지붕 일사열획득_난방6월)  
                    _data301.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][60]) });//QSopsource_Roof(지붕 일사열획득_난방7월)  
                    _data302.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][60]) });//QSopsource_Roof(지붕 일사열획득_난방8월)  
                    _data303.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][60]) });//QSopsource_Roof(지붕 일사열획득_난방9월)   
                    _data304.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][60]) });//QSopsource_Roof(지붕 일사열획득_난방10월) 
                    _data305.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][60]) });//QSopsource_Roof(지붕 일사열획득_난방11월)  
                    _data306.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][60]) });//QSopsource_Roof(지붕 일사열획득_난방12월)  

                    _data307.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][61]) });//QSopsource_Door(출입문 일사열획득_난방1월) 
                    _data308.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][61]) });//QSopsource_Door(출입문 일사열획득_난방2월) 
                    _data309.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][61]) });//QSopsource_Door(출입문 일사열획득_난방3월) 
                    _data310.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][61]) });//QSopsource_Door(출입문 일사열획득_난방4월)  
                    _data311.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][61]) });//QSopsource_Door(출입문 일사열획득_난방5월) 
                    _data312.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][61]) });//QSopsource_Door(출입문 일사열획득_난방6월)  
                    _data313.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][61]) });//QSopsource_Door(출입문 일사열획득_난방7월)   
                    _data314.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][61]) });//QSopsource_Door(출입문 일사열획득_난방8월)   
                    _data315.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][61]) });//QSopsource_Door(출입문 일사열획득_난방9월)    
                    _data316.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][61]) });//QSopsource_Door(출입문 일사열획득_난방10월) 
                    _data317.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][61]) });//QSopsource_Door(출입문 일사열획득_난방11월) 
                    _data318.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][61]) });//QSopsource_Door(출입문 일사열획득_난방12월) 

                    _data319.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][75]) });//QI_tot(내부발열 1월) 
                    _data320.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][75]) });//QI_tot(내부발열 2월) 
                    _data321.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][75]) });//QI_tot(내부발열 3월)  
                    _data322.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][75]) });//QI_tot(내부발열 4월) 
                    _data323.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][75]) });//QI_tot(내부발열 5월) 
                    _data324.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][75]) });//QI_tot(내부발열 6월) 
                    _data325.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][75]) });//QI_tot(내부발열 7월) 
                    _data326.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][75]) });//QI_tot(내부발열 8월) 
                    _data327.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][75]) });//QI_tot(내부발열 9월) 
                    _data328.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][75]) });//QI_tot(내부발열 10월) 
                    _data329.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][75]) });//QI_tot(내부발열 11월) 
                    _data330.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][75]) });//QI_tot(내부발열 12월) 

                    _data331.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][76]) });//QI_l(내부발열조명 1월) 
                    _data332.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][76]) });//QI_l(내부발열조명 2월) 
                    _data333.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][76]) });//QI_l(내부발열조명 3월)  
                    _data334.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][76]) });//QI_l(내부발열조명 4월) 
                    _data335.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][76]) });//QI_l(내부발열조명 5월) 
                    _data336.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][76]) });//QI_l(내부발열조명 6월) 
                    _data337.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][76]) });//QI_l(내부발열조명 7월)  
                    _data338.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][76]) });//QI_l(내부발열조명 8월) 
                    _data339.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][76]) });//QI_l(내부발열조명 9월) 
                    _data340.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][76]) });//QI_l(내부발열조명 10월) 
                    _data341.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][76]) });//QI_l(내부발열조명 11월) 
                    _data342.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][76]) });//QI_l(내부발열조명 12월) 

                    _data343.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][77]) });//QI_p(내부발열인간 1월) 
                    _data344.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][77]) });//QI_p(내부발열인간 2월) 
                    _data345.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][77]) });//QI_p(내부발열인간 3월)  
                    _data346.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][77]) });//QI_p(내부발열인간 4월) 
                    _data347.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][77]) });//QI_p(내부발열인간 5월) 
                    _data348.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][77]) });//QI_p(내부발열인간 6월) 
                    _data349.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][77]) });//QI_p(내부발열인간 7월)  
                    _data350.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][77]) });//QI_p(내부발열인간 8월) 
                    _data351.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][77]) });//QI_p(내부발열인간 9월) 
                    _data352.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][77]) });//QI_p(내부발열인간 10월) 
                    _data353.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][77]) });//QI_p(내부발열인간 11월) 
                    _data354.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][77]) });//QI_p(내부발열인간 12월) 

                    _data355.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 12][78]) });//QI_e(내부발열기기 1월) 
                    _data356.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 13][78]) });//QI_e내부발열기기 2월) 
                    _data357.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 14][78]) });//QI_e(내부발열기기 3월)  
                    _data358.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 15][78]) });//QI_e(내부발열기기 4월) 
                    _data359.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 16][78]) });//QI_e(내부발열기기 5월) 
                    _data360.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 17][78]) });//QI_e(내부발열기기 6월) 
                    _data361.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 18][78]) });//QI_e(내부발열기기 7월)  
                    _data362.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 19][78]) });//QI_e(내부발열기기 8월) 
                    _data363.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 20][78]) });//QI_e(내부발열기기 9월) 
                    _data364.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 21][78]) });//QI_e(내부발열기기 10월) 
                    _data365.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 22][78]) });//QI_e(내부발열기기 11월) 
                    _data366.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 23][78]) });//QI_e(내부발열기기 12월) 

                    _data367.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 0][92]) });//Qhneed_we(난방요구량 비이용일 1월)
                    _data368.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 1][92]) });//Qhneed_we(난방요구량 비이용일 2월)
                    _data369.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 2][92]) });//Qhneed_we(난방요구량 비이용일 3월)
                    _data370.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 3][92]) });//Qhneed_we(난방요구량 비이용일 4월)
                    _data371.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 4][92]) });//Qhneed_we(난방요구량 비이용일 5월)
                    _data372.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 5][92]) });//Qhneed_we(난방요구량 비이용일 6월)
                    _data373.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 6][92]) });//Qhneed_we(난방요구량 비이용일 7월)
                    _data374.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 7][92]) });//Qhneed_we(난방요구량 비이용일 8월)
                    _data375.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 8][92]) });//Qhneed_we(난방요구량 비이용일 9월)
                    _data376.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 9][92]) });//Qhneed_we(난방요구량 비이용일 10월)
                    _data377.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 10][92]) });//Qhneed_we(난방요구량 비이용일 11월)
                    _data378.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 11][92]) });//Qhneed_we(난방요구량 비이용일 12월)
                    _data379.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 11][92]) });//Qhneed_we(난방요구량 비이용일 12월)

                    _data380.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 0][84]) });//deltaQc_b(대차축열량 1월)
                    _data381.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 1][84]) });//deltaQc_b(대차축열량 2월)
                    _data382.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 2][84]) });//deltaQc_b(대차축열량 3월)
                    _data383.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 3][84]) });//deltaQc_b(대차축열량 4월)
                    _data384.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 4][84]) });//deltaQc_b(대차축열량 5월)
                    _data385.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 5][84]) });//deltaQc_b(대차축열량 6월)
                    _data386.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 6][84]) });//deltaQc_b(대차축열량 7월)
                    _data387.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 7][84]) });//deltaQc_b(대차축열량 8월)
                    _data388.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 8][84]) });//deltaQc_b(대차축열량 9월)
                    _data389.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 9][84]) });//deltaQc_b(대차축열량 10월)
                    _data390.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 10][84]) });//deltaQc_b(대차축열량 11월)
                    _data391.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 11][84]) });//deltaQc_b(대차축열량 12월)

                    _data392.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 0][36]) });//QTsink_we(관류열손실량 비이용일 1월)
                    _data393.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 1][36]) });//QTsink_we(관류열손실량 비이용일 2월)
                    _data394.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 2][36]) });//QTsink_we(관류열손실량 비이용일 3월)
                    _data395.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 3][36]) });//QTsink_we(관류열손실량 비이용일 4월)
                    _data396.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 4][36]) });//QTsink_we(관류열손실량 비이용일 5월)
                    _data397.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 5][36]) });//QTsink_we(관류열손실량 비이용일 6월)
                    _data398.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 6][36]) });//QTsink_we(관류열손실량 비이용일 7월)
                    _data399.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 7][36]) });//QTsink_we(관류열손실량 비이용일 8월)
                    _data400.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 8][36]) });//QTsink_we(관류열손실량 비이용일 9월)
                    _data401.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 9][36]) });//QTsink_we(관류열손실량 비이용일 10월)
                    _data402.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 10][36]) });//QTsink_we(관류열손실량 비이용일 11월)
                    _data403.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 11][36]) });//QTsink_we(관류열손실량 비이용일 12월)

                    _data404.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 0][65]) });//QVsink_we(환기열손실량 비이용일 1월)
                    _data405.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 1][65]) });//QVsink_we(환기열손실량 비이용일 2월)
                    _data406.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 2][65]) });//QVsink_we(환기열손실량 비이용일 3월)
                    _data407.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 3][65]) });//QVsink_we(환기열손실량 비이용일 4월)
                    _data408.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 4][65]) });//QVsink_we(환기열손실량 비이용일 5월)
                    _data409.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 5][65]) });//QVsink_we(환기열손실량 비이용일 6월)
                    _data410.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 6][65]) });//QVsink_we(환기열손실량 비이용일 7월)
                    _data411.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 7][65]) });//QVsink_we(환기열손실량 비이용일 8월)
                    _data412.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 8][65]) });//QVsink_we(환기열손실량 비이용일 9월)
                    _data413.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 9][65]) });//QVsink_we(환기열손실량 비이용일 10월)
                    _data414.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 10][65]) });//QVsink_we(환기열손실량 비이용일 11월)
                    _data415.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 11][65]) });//QVsink_we(환기열손실량 비이용일 12월)

                    _data416.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 0][54]) });//Qstr_tot_we(일사열획득)
                    _data417.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 1][54]) });//Qstr_tot_we(일사열획득)
                    _data418.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 2][54]) });//Qstr_tot_we(일사열획득)
                    _data419.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 3][54]) });//Qstr_tot_we(일사열획득)
                    _data420.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 4][54]) });//Qstr_tot_we(일사열획득)
                    _data421.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 5][54]) });//Qstr_tot_we(일사열획득)
                    _data422.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 6][54]) });//Qstr_tot_we(일사열획득)
                    _data423.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 7][54]) });//Qstr_tot_we(일사열획득)
                    _data424.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 8][54]) });//Qstr_tot_we(일사열획득)
                    _data425.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 9][54]) });//Qstr_tot_we(일사열획득)
                    _data426.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 10][54]) });//Qstr_tot_we(일사열획득)
                    _data427.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 11][54]) });//Qstr_tot_we(일사열획득)

                    _data428.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 0][75]) });//QI_tot_we(내부발열)
                    _data429.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 1][75]) });//QI_tot_we(내부발열)
                    _data430.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 2][75]) });//QI_tot_we(내부발열)
                    _data431.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 3][75]) });//QI_tot_we(내부발열)
                    _data432.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 4][75]) });//QI_tot_we(내부발열)
                    _data433.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 5][75]) });//QI_tot_we(내부발열)
                    _data434.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 6][75]) });//QI_tot_we(내부발열)
                    _data435.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 7][75]) });//QI_tot_we(내부발열)
                    _data436.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 8][75]) });//QI_tot_we(내부발열)
                    _data437.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 9][75]) });//QI_tot_we(내부발열)
                    _data438.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 10][75]) });//QI_tot_we(내부발열)
                    _data439.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 11][75]) });//QI_tot_we(내부발열)

                    _data440.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 36][95]) });//Qc_need_wd(냉방요구량 이용일 1월)
                    _data441.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 37][95]) });//Qc_need_wd(냉방요구량 이용일 2월)
                    _data442.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 38][95]) });//Qc_need_wd(냉방요구량 이용일 3월)
                    _data443.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 39][95]) });//Qc_need_wd(냉방요구량 이용일 4월)
                    _data444.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 40][95]) });//Qc_need_wd(냉방요구량 이용일 5월)
                    _data445.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 41][95]) });//Qc_need_wd(냉방요구량 이용일 6월)
                    _data446.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 42][95]) });//Qc_need_wd(냉방요구량 이용일 7월)
                    _data447.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 43][95]) });//Qc_need_wd(냉방요구량 이용일 8월)
                    _data448.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 44][95]) });//Qc_need_wd(냉방요구량 이용일 9월)
                    _data449.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 45][95]) });//Qc_need_wd(냉방요구량 이용일 10월)
                    _data450.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 46][95]) });//Qc_need_wd(냉방요구량 이용일 11월)
                    _data451.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 47][95]) });//Qc_need_wd(냉방요구량 이용일 12월)
                    _data452.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 47][97]) });//Qc_need_wd(냉방요구량 이용일 12월) 총합

                    _data453.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 36][95]) });//Qc_need_wd(냉방요구량 이용일 1월)
                    _data454.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 37][95]) });//Qc_need_wd(냉방요구량 이용일 2월)
                    _data455.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 38][95]) });//Qc_need_wd(냉방요구량 이용일 3월)
                    _data456.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 39][95]) });//Qc_need_wd(냉방요구량 이용일 4월)
                    _data457.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 40][95]) });//Qc_need_wd(냉방요구량 이용일 5월)
                    _data458.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 41][95]) });//Qc_need_wd(냉방요구량 이용일 6월)
                    _data459.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 42][95]) });//Qc_need_wd(냉방요구량 이용일 7월)
                    _data460.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 43][95]) });//Qc_need_wd(냉방요구량 이용일 8월)
                    _data461.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 44][95]) });//Qc_need_wd(냉방요구량 이용일 9월)
                    _data462.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 45][95]) });//Qc_need_wd(냉방요구량 이용일 10월)
                    _data463.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 46][95]) });//Qc_need_wd(냉방요구량 이용일 11월)
                    _data464.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 47][95]) });//Qc_need_wd(냉방요구량 이용일 12월)
                    _data464.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 47][97]) });//Qc_need_wd(냉방요구량 이용일 12월)

                    //_data465.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 0][95]) });//Qc_need_wd(제습요구량 이용일 1월)
                    //_data466.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 1][95]) });//Qc_need_wd(제습요구량 이용일 2월)
                    //_data467.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 2][95]) });//Qc_need_wd(제습요구량 이용일 3월)
                    //_data468.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 3][95]) });//Qc_need_wd(제습요구량 이용일 4월)
                    //_data469.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 4][95]) });//Qc_need_wd(제습요구량 이용일 5월)
                    //_data470.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 5][95]) });//Qc_need_wd(제습요구량 이용일 6월)
                    //_data471.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 6][95]) });//Qc_need_wd(제습요구량 이용일 7월)
                    //_data472.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 7][95]) });//Qc_need_wd(제습요구량 이용일 8월)
                    //_data473.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 8][95]) });//Qc_need_wd(냉방요구량 이용일 9월)
                    //_data474.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 9][95]) });//Qc_need_wd(냉방요구량 이용일 10월)
                    //_data475.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 10][95]) });//Qc_need_wd(냉방요구량 이용일 11월)
                    //_data476.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 11][95]) });//Qc_need_wd(냉방요구량 이용일 12월)
                    //_data477.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 11][97]) });//Qc_need_wd(냉방요구량 이용일 12월)

                    _data478.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 36][34]) });//thetai (냉방온도 이용일 1월)
                    _data479.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 37][34]) });//thetai (냉방온도 이용일 2월)
                    _data480.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 38][34]) });//thetai (냉방온도 이용일 3월)
                    _data481.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 39][34]) });//thetai (냉방온도 이용일 4월)
                    _data482.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 40][34]) });//thetai (냉방온도 이용일 5월)
                    _data483.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 41][34]) });//thetai (냉방온도 이용일 6월)
                    _data484.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 42][34]) });//thetai (냉방온도 이용일 7월)
                    _data485.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 43][34]) });//thetai (냉방온도 이용일 8월)
                    _data486.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 44][34]) });//thetai (냉방온도 이용일 9월)
                    _data487.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 45][34]) });//thetai (냉방온도 이용일 10월)
                    _data488.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 46][34]) });//thetai (냉방온도 이용일 11월)
                    _data489.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 47][34]) });//thetai (냉방온도 이용일 12월)

                    _data490.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 36][44]) });//QTsource_total (관류열획득 냉방이용일 1월)
                    _data491.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 37][44]) });//QTsource_total (관류열획득 냉방이용일 2월)
                    _data492.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 38][44]) });//QTsource_total (관류열획득 냉방이용일 3월)
                    _data493.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 39][44]) });//QTsource_total (관류열획득 냉방이용일 4월)
                    _data494.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 40][44]) });//QTsource_total (관류열획득 냉방이용일 5월)
                    _data495.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 41][44]) });//QTsource_total (관류열획득 냉방이용일 6월)
                    _data496.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 42][44]) });//QTsource_total (관류열획득 냉방이용일 7월)
                    _data497.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 43][44]) });//QTsource_total (관류열획득 냉방이용일 8월)
                    _data498.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 44][44]) });//QTsource_total (관류열획득 냉방이용일 9월)
                    _data499.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 45][44]) });//QTsource_total (관류열획득 냉방이용일 10월)
                    _data500.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 46][44]) });//QTsource_total (관류열획득 냉방이용일 11월)
                    _data501.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 47][44]) });//QTsource_total (관류열획득 냉방이용일 12월)

                    _data502.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 36][71]) });//QVsource_total (환기열획득 냉방이용일 1월)
                    _data503.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 37][71]) });//QVsource_total (환기열획득 냉방이용일 2월)
                    _data504.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 38][71]) });//QVsource_total (환기열획득 냉방이용일 3월)
                    _data505.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 39][71]) });//QVsource_total (환기열획득 냉방이용일 4월)
                    _data506.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 40][71]) });//QVsource_total (환기열획득 냉방이용일 5월)
                    _data507.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 41][71]) });//QVsource_total (환기열획득 냉방이용일 6월)
                    _data508.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 42][71]) });//QVsource_total (환기열획득 냉방이용일 7월)
                    _data509.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 43][71]) });//QVsource_total (환기열획득 냉방이용일 8월)
                    _data510.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 44][71]) });//QVsource_total (환기열획득 냉방이용일 9월)
                    _data511.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 45][71]) });//QVsource_total (환기열획득 냉방이용일 10월)
                    _data512.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 46][71]) });//QVsource_total (환기열획득 냉방이용일 11월)
                    _data513.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 47][71]) });//QVsource_total (환기열획득 냉방이용일 12월)

                    _data514.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 36][54]) });//QSsource_total (일사열획득 냉방이용일 1월)
                    _data515.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 37][54]) });//QSsource_total (일사열획득 냉방이용일 2월)
                    _data516.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 38][54]) });//QSsource_total (일사열획득 냉방이용일 3월)
                    _data517.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 39][54]) });//QSsource_total (일사열획득 냉방이용일 4월)
                    _data518.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 40][54]) });//QSsource_total (일사열획득 냉방이용일 5월)
                    _data519.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 41][54]) });//QSsource_total (일사열획득 냉방이용일 6월)
                    _data520.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 42][54]) });//QSsource_total (일사열획득 냉방이용일 7월)
                    _data521.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 43][54]) });//QSsource_total (일사열획득 냉방이용일 8월)
                    _data522.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 44][54]) });//QSsource_total (일사열획득 냉방이용일 9월)
                    _data523.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 45][54]) });//QSsource_total (일사열획득 냉방이용일 10월)
                    _data524.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 46][54]) });//QSsource_total (일사열획득 냉방이용일 11월)
                    _data525.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 47][54]) });//QSsource_total (일사열획득 냉방이용일 12월)

                    _data526.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 24][94]) });//Qcneed_we (냉방요구량 1월)
                    _data527.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 25][94]) });//Qcneed_we (냉방요구량 2월)
                    _data528.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 26][94]) });//Qcneed_we (냉방요구량 3월)
                    _data529.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 27][94]) });//Qcneed_we (냉방요구량 4월)
                    _data530.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 28][94]) });//Qcneed_we (냉방요구량 5월)
                    _data531.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 29][94]) });//Qcneed_we (냉방요구량 6월)
                    _data532.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 30][94]) });//Qcneed_we (냉방요구량 7월)
                    _data533.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 31][94]) });//Qcneed_we (냉방요구량 8월)
                    _data534.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 32][94]) });//Qcneed_we (냉방요구량 9월)
                    _data535.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 33][94]) });//Qcneed_we (냉방요구량 10월)
                    _data536.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 34][94]) });//Qcneed_we (냉방요구량 11월)
                    _data537.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 35][94]) });//Qcneed_we (냉방요구량 12월)
                    _data538.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 35][100]) });//Qcneed_we (냉방요구량 12월)

                    _data539.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 24][34]) });//thetac_we (냉방기준온도 1월)
                    _data540.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 25][34]) });//thetac_we (냉방기준온도 2월)
                    _data541.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 26][34]) });//thetac_we (냉방기준온도 3월)
                    _data542.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 27][34]) });//thetac_we (냉방기준온도 4월)
                    _data543.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 28][34]) });//thetac_we (냉방기준온도 5월)
                    _data544.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 29][34]) });//thetac_we (냉방기준온도 6월)
                    _data545.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 30][34]) });//thetac_we (냉방기준온도 7월)
                    _data546.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 31][34]) });//thetac_we (냉방기준온도 8월)
                    _data547.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 32][34]) });//thetac_we (냉방기준온도 9월)
                    _data548.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 33][34]) });//thetac_we (냉방기준온도 10월)
                    _data549.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 34][34]) });//thetac_we (냉방기준온도 11월)
                    _data550.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 35][34]) });//thetac_we (냉방기준온도 12월)

                    //_data551.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 24][34]) });//thetac_we (최대난방부하 1월)

                    //_data552.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 24][34]) });//time_h (난방시간 1월)
                    //_data553.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 25][34]) });//time_h (난방시간 2월)
                    //_data554.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 26][34]) });//time_h (난방시간 3월)
                    //_data555.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 27][34]) });//time_h (난방시간 4월)
                    //_data556.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 28][34]) });//time_h (난방시간 5월)
                    //_data557.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 29][34]) });//time_h (난방시간 6월)
                    //_data558.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 30][34]) });//time_h (난방시간 7월)
                    //_data559.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 31][34]) });//time_h (난방시간 8월)
                    //_data560.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 32][34]) });//time_h (난방시간 9월)
                    //_data561.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 33][34]) });//time_h (난방시간 10월)
                    //_data562.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 34][34]) });//time_h (난방시간 11월)
                    //_data563.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 35][34]) });//time_h (난방시간 12월)

                    //_data564.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 24][34]) });//thetac_we (최대냉방부하 1월)

                    //_data565.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 24][34]) });//time_c (냉방시간 1월)
                    //_data566.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 25][34]) });//time_c (냉방시간 2월)
                    //_data567.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 26][34]) });//time_c (냉방시간 3월)
                    //_data568.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 27][34]) });//time_c (냉방시간 4월)
                    //_data569.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 28][34]) });//time_c (냉방시간 5월)
                    //_data570.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 29][34]) });//time_c (냉방시간 6월)
                    //_data571.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 30][34]) });//time_c (냉방시간 7월)
                    //_data572.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 31][34]) });//time_c (냉방시간 8월)
                    //_data573.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 32][34]) });//time_c (냉방시간 9월)
                    //_data574.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 33][34]) });//time_c (냉방시간 10월)
                    //_data575.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 34][34]) });//time_c (냉방시간 11월)
                    //_data576.Add(new { idx = i, val = Program.UTIL.asFixed(value[48 * i + 35][34]) });//time_c (냉방시간 12월)



                    double sum = 0;
                    double kkk;
                    for (int k = 0; k < 12; k++)
                    {
                        kkk = Convert.ToDouble(value[48 * i + 12 + k][92]);//theta (이용일 난방요구량_합산)
                        sum += kkk;//theta (이용일 난방요구량_합산)

                    }

                    _data78.Add(new { idx = i, val = sum });

                    //MessageBox.Show(sum.ToString());

                }
                else;

                //조명 요구량 
                if (ZoneG[i][0] == value2[12 * i][0])
                {
                    _data577.Add(new { idx = i, val = Program.UTIL.asFixed(value2[12 * i][39]) });//Finfal_W_1 (조명 요구량 1월)
                    _data578.Add(new { idx = i, val = Program.UTIL.asFixed(value2[12 * i + 1][39]) });//Finfal_W_2 (조명 요구량 2월)
                    _data579.Add(new { idx = i, val = Program.UTIL.asFixed(value2[12 * i + 2][39]) });//Finfal_W_3 (조명 요구량 3월)
                    _data580.Add(new { idx = i, val = Program.UTIL.asFixed(value2[12 * i + 3][39]) });//Finfal_W_4 (조명 요구량 4월)
                    _data581.Add(new { idx = i, val = Program.UTIL.asFixed(value2[12 * i + 4][39]) });//Finfal_W_5 (조명 요구량 5월)
                    _data582.Add(new { idx = i, val = Program.UTIL.asFixed(value2[12 * i + 5][39]) });//Finfal_W_6 (조명 요구량 6월)
                    _data583.Add(new { idx = i, val = Program.UTIL.asFixed(value2[12 * i + 6][39]) });//Finfal_W_7 (조명 요구량 7월)
                    _data584.Add(new { idx = i, val = Program.UTIL.asFixed(value2[12 * i + 7][39]) });//Finfal_W_8 (조명 요구량 8월)
                    _data585.Add(new { idx = i, val = Program.UTIL.asFixed(value2[12 * i + 8][39]) });//Finfal_W_9 (조명 요구량 9월)
                    _data586.Add(new { idx = i, val = Program.UTIL.asFixed(value2[12 * i + 9][39]) });//Finfal_W_10 (조명 요구량 10월)
                    _data587.Add(new { idx = i, val = Program.UTIL.asFixed(value2[12 * i + 10][39]) });//Finfal_W_11 (조명 요구량 11월)
                    _data588.Add(new { idx = i, val = Program.UTIL.asFixed(value2[12 * i + 11][39]) });//Finfal_W_12 (조명 요구량 12월)
                    //_data589.Add(new { idx = i, val = Program.UTIL.asFixed(value2[12 * i + 11][39]) });//Finfal_W_12 (조명 요구량 합산값 변경) 



                    double sum = 0;
                    double kkkk;
                    for (int k = 0; k < 12; k++)
                    {
                        kkkk = Convert.ToDouble(value2[12 * i + k][39]);//Finfal_W (조명에너지 요구량 합산)
                        sum += kkkk;// Finfal_W (조명에너지 요구량 합산)
                        //MessageBox.Show(sum.ToString());
                    }
                    //string result;
                    //result = string.Format("{0:0.#0}", sum);
                    //_data589.Add(new { idx = i, val = result });


                    _data589.Add(new { idx = i, val = Program.UTIL.asFixed(sum.ToString()) });

                    //MessageBox.Show(sum.ToString());

                }
                else;

                //int kk = -1;
                //while (++kk < envelope.Length)
                //{
                //    //외벽면적합 
                //    if (ZoneG[i][0] == envelope[kk][0])
                //    {
                //        double totalwall = 0;
                //        double wall;

                //        wall = Convert.ToDouble(envelope[kk][1]);
                //        totalwall += wall;

                //        MessageBox.Show(totalwall.ToString());
                //        //_data590.Add(new { idx = i, val = Program.UTIL.asFixed(total_wall.ToString()) });
                //    }
                //    else;

                int kk = -1;
                double totalwall = 0;
                double totalcw = 0;
                double totalroof = 0;
                double totalwin = 0;
                double totalfloor = 0;
                
                 while (++kk < envelope.Length)
                 {
                        if (ZoneG[i][0] == envelope[kk][0])
                        {
                            double wall = Convert.ToDouble(envelope[kk][1]);
                            totalwall += wall;
                        }
                        else
                        {
                        }
                 }


                kk = -1;
                while (++kk < envelope2.Length)
                {
                        if (ZoneG[i][0] == envelope2[kk][0])
                        {
                            double cw = Convert.ToDouble(envelope2[kk][1]);
                            totalcw += cw;
                        }
                        else
                        {
                        }
                }


                kk = -1;
                while (++kk < envelope3.Length)
                    {
                        if (ZoneG[i][0] == envelope3[kk][0])
                        {
                            double roof = Convert.ToDouble(envelope3[kk][1]);
                            totalroof += roof;
                        }
                        else
                        {
                        }
                }

                kk = -1;
                while (++kk < envelope4.Length)
                    {
                        if (ZoneG[i][0] == envelope4[kk][0])
                        {
                            double win = Convert.ToDouble(envelope4[kk][1]);
                            totalwin += win;
                        }
                        else
                        {
                        }
                }


                kk = -1;
                while (++kk < envelope5.Length)
                    {
                        if (ZoneG[i][0] == envelope5[kk][0])
                        {
                            double floor = Convert.ToDouble(envelope5[kk][1]);
                            totalfloor += floor;
                        }
                        else
                        {
                        }
                 
                }



                _data590.Add(new { idx = i, val = Program.UTIL.asFixed(totalwall.ToString()) });
                _data591.Add(new { idx = i, val = Program.UTIL.asFixed(totalcw.ToString()) });
                _data592.Add(new { idx = i, val = Program.UTIL.asFixed(totalroof.ToString()) });
                _data593.Add(new { idx = i, val = Program.UTIL.asFixed(totalwin.ToString()) });
                _data594.Add(new { idx = i, val = Program.UTIL.asFixed(totalfloor.ToString()) });

                //열관류율(벽체) 계산 파트
                kk = -1;
                double Utwall_sum = 0;
                double U_wall =0;
                //double Htwallarea = 0;


                String[][] uenvelope = Program.DB.querySQL(DB.type.ProjDB, "select a.존,a.면적,b.열관류율,b.유효열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + ZoneG[i][0] + "'");
                while (++kk < uenvelope.Length)
                {
                    if (ZoneG[i][0] == uenvelope[kk][0])
                    {
                        double areaueff = Convert.ToDouble(uenvelope[kk][1]) * Convert.ToDouble(uenvelope[kk][2]);
                        Utwall_sum += areaueff;
                        U_wall = Utwall_sum / totalwall;
                    }
                    else;
                }
                _data595.Add(new { idx = i, val = Program.UTIL.asFixed(U_wall.ToString()) });



                //열관류율(커튼월) 계산 파트
                kk = -1;
                double Utcwsum = 0;
                double U_cw = 0;
                //double Htwallarea = 0;
                String[][] uenvelope2 = Program.DB.querySQL(DB.type.ProjDB, "select a.존,a.면적,b.커튼월창열관류율,b.커튼월창유효열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionCW AS b ON a.구조체번호 = b.번호 where a.존 = '" + ZoneG[i][0] + "'");
                while (++kk < uenvelope2.Length)
                {
                    if (ZoneG[i][0] == uenvelope2[kk][0])
                    {
                        double areaueff = Convert.ToDouble(uenvelope2[kk][1]) * Convert.ToDouble(uenvelope2[kk][2]);
                        Utcwsum += areaueff;
                        U_cw = Utcwsum / totalcw;
                    }
                    else;
                }
                _data596.Add(new { idx = i, val = Program.UTIL.asFixed(U_cw.ToString()) });

                //열관류율(지붕) 계산 파트
                kk = -1;
                double Utroofsum = 0;
                double U_roof = 0;
                //double Htroofarea = 0;
                String[][] uenvelope3 = Program.DB.querySQL(DB.type.ProjDB, "select a.존,a.면적,b.열관류율,b.유효열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionRoof AS b ON a.구조체번호 = b.번호 where a.존 = '" + ZoneG[i][0] + "'");
                while (++kk < uenvelope3.Length)
                {
                    if (ZoneG[i][0] == uenvelope3[kk][0])
                    {
                        double areaueff = Convert.ToDouble(uenvelope3[kk][1]) * Convert.ToDouble(uenvelope3[kk][2]);
                        Utroofsum += areaueff;
                        U_roof = Utroofsum / totalroof;
                    }
                    else;
                }
                _data597.Add(new { idx = i, val = Program.UTIL.asFixed(U_roof.ToString()) });

                //열관류율(창호) 계산 파트
                kk = -1;
                double Utwinsum = 0;
                double U_win = 0;
                //double Htwinarea = 0;
                String[][] uenvelope4 = Program.DB.querySQL(DB.type.ProjDB, "select a.존,a.면적,b.창호열관류율,b.창호유효열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체번호 = b.번호 where a.존 = '" + ZoneG[i][0] + "'");
                while (++kk < uenvelope4.Length)
                {
                    if (ZoneG[i][0] == uenvelope4[kk][0])
                    {
                        double areaueff = Convert.ToDouble(uenvelope4[kk][1]) * Convert.ToDouble(uenvelope4[kk][2]);
                        Utwinsum += areaueff;
                        U_win = Utwinsum / totalwin;
                    }
                    else;
                }
                _data598.Add(new { idx = i, val = Program.UTIL.asFixed(U_win.ToString()) });

                //열관류율(바닥) 계산 파트
                kk = -1;
                double Utflsum = 0;
                double U_fl = 0;
                //double Htwinarea = 0;
                String[][] uenvelope5 = Program.DB.querySQL(DB.type.ProjDB, "select a.존,a.면적,b.열관류율,b.유효열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionFloor AS b ON a.구조체번호 = b.번호 where a.존 = '" + ZoneG[i][0] + "'");
                while (++kk < uenvelope5.Length)
                {
                    if (ZoneG[i][0] == uenvelope5[kk][0])
                    {
                        double areaueff = Convert.ToDouble(uenvelope5[kk][1]) * Convert.ToDouble(uenvelope5[kk][2]);
                        Utflsum += areaueff;
                        U_fl = Utflsum / totalfloor;
                        
                    }
                    else;
                }
                _data599.Add(new { idx = i, val = Program.UTIL.asFixed(U_fl.ToString()) });



















                //열전달계수(벽체) 계산 파트
                kk = -1;
                double Htwall_sum = 0;
                double h_wall = 0;
                //double Htwallarea = 0;


                String[][] henvelope = Program.DB.querySQL(DB.type.ProjDB, "select a.존,a.면적,b.열관류율,b.유효열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호 where a.존 = '" + ZoneG[i][0] + "'");
                while (++kk < henvelope.Length)
                {
                    if (ZoneG[i][0] == henvelope[kk][0])
                    {
                        double areaueff = Convert.ToDouble(henvelope[kk][3]) * Convert.ToDouble(henvelope[kk][2]);

                        Htwall_sum += areaueff;
                        h_wall = Htwall_sum;
                    }
                    else;
                }
                _data600.Add(new { idx = i, val = Program.UTIL.asFixed(h_wall.ToString()) });

                //열전달계수(커튼월) 계산 파트
                kk = -1;
                double Htcw = 0;

                while (++kk < uenvelope2.Length)
                {
                    if (ZoneG[i][0] == uenvelope2[kk][0])
                    {
                        double areaueff = Convert.ToDouble(uenvelope2[kk][1]) * Convert.ToDouble(uenvelope2[kk][2]);
                        Htcw += areaueff;
                    }
                    else;
                }
                _data601.Add(new { idx = i, val = Program.UTIL.asFixed(Htcw.ToString()) });

                //열전달계수(지붕) 계산 파트
                kk = -1;
                double Htroof = 0;

                while (++kk < uenvelope3.Length)
                {
                    if (ZoneG[i][0] == uenvelope3[kk][0])
                    {
                        double areaueff = Convert.ToDouble(uenvelope3[kk][1]) * Convert.ToDouble(uenvelope3[kk][2]);
                        Htroof += areaueff;
                    }
                    else;
                }
                _data602.Add(new { idx = i, val = Program.UTIL.asFixed(Htroof.ToString()) });

                //열전달계수(바닥) 계산 파트
                kk = -1;
                double Htfl = 0;

                while (++kk < uenvelope4.Length)
                {
                    if (ZoneG[i][0] == uenvelope4[kk][0])
                    {
                        double areaueff = Convert.ToDouble(uenvelope4[kk][1]) * Convert.ToDouble(uenvelope4[kk][2]);
                        Htfl += areaueff;
                    }
                    else;
                }
                _data603.Add(new { idx = i, val = Program.UTIL.asFixed(Htfl.ToString()) });

                //열전달계수(창호) 계산 파트
                kk = -1;
                double Htwin = 0;

                while (++kk < uenvelope5.Length)
                {
                    if (ZoneG[i][0] == uenvelope5[kk][0])
                    {
                        double areaueff = Convert.ToDouble(uenvelope5[kk][1]) * Convert.ToDouble(uenvelope5[kk][2]);
                        Htwin += areaueff;
                    }
                    else;
                }
                _data604.Add(new { idx = i, val = Program.UTIL.asFixed(Htwin.ToString()) });

             }



            ////////////////////////////////////////////////////////////////////
            data.Add(new { cname = "cls-profile-name", data = _data }); 
            data.Add(new { cname = "cls-volume", data = _data2 });
            data.Add(new { cname = "cls-sub-name", data = _data3 });
            data.Add(new { cname = "cls-zone-name", data = _data4 });
            data.Add(new { cname = "cls-zone-ht", data = _data5 });
            data.Add(new { cname = "cls-htd", data = _data13 });
            data.Add(new { cname = "cls-htu", data = _data14 });
            data.Add(new { cname = "cls-hts", data = _data9 });
            data.Add(new { cname = "cls-htb", data= _data21 });
            data.Add(new { cname = "cls-hinf", data = _data31});
            //이용일 
            data.Add(new { cname = "cls-day-1wd", data = _data42 });
            data.Add(new { cname = "cls-day-2wd", data = _data43 });
            data.Add(new { cname = "cls-day-3wd", data = _data44 });
            data.Add(new { cname = "cls-day-4wd", data = _data45 });
            data.Add(new { cname = "cls-day-5wd", data = _data46 });
            data.Add(new { cname = "cls-day-6wd", data = _data47 });
            data.Add(new { cname = "cls-day-7wd", data = _data48 });
            data.Add(new { cname = "cls-day-8wd", data = _data49 });
            data.Add(new { cname = "cls-day-9wd", data = _data50 });
            data.Add(new { cname = "cls-day-10wd", data = _data51 });
            data.Add(new { cname = "cls-day-11wd", data = _data52 });
            data.Add(new { cname = "cls-day-12wd", data = _data53 });
            //난방기준온도
            data.Add(new { cname = "cls-htemp-1wd", data = _data54 });
            data.Add(new { cname = "cls-htemp-2wd", data = _data55 });
            data.Add(new { cname = "cls-htemp-3wd", data = _data56 });
            data.Add(new { cname = "cls-htemp-4wd", data = _data57 });
            data.Add(new { cname = "cls-htemp-5wd", data = _data58 });
            data.Add(new { cname = "cls-htemp-6wd", data = _data59 });
            data.Add(new { cname = "cls-htemp-7wd", data = _data60 });
            data.Add(new { cname = "cls-htemp-8wd", data = _data61 });
            data.Add(new { cname = "cls-htemp-9wd", data = _data62 });
            data.Add(new { cname = "cls-htemp-10wd", data = _data63 });
            data.Add(new { cname = "cls-htemp-11wd", data = _data64 });
            data.Add(new { cname = "cls-htemp-12wd", data = _data65 });
            //난방요구량
            data.Add(new { cname = "cls-hneed-1wd", data = _data66 });
            data.Add(new { cname = "cls-hneed-2wd", data = _data67 });
            data.Add(new { cname = "cls-hneed-3wd", data = _data68 });
            data.Add(new { cname = "cls-hneed-4wd", data = _data69 });
            data.Add(new { cname = "cls-hneed-5wd", data = _data70 });
            data.Add(new { cname = "cls-hneed-6wd", data = _data71 });
            data.Add(new { cname = "cls-hneed-7wd", data = _data72 });
            data.Add(new { cname = "cls-hneed-8wd", data = _data73 });
            data.Add(new { cname = "cls-hneed-9wd", data = _data74 });
            data.Add(new { cname = "cls-hneed-10wd", data = _data75 });
            data.Add(new { cname = "cls-hneed-11wd", data = _data76 });
            data.Add(new { cname = "cls-hneed-12wd", data = _data77 });
            data.Add(new { cname = "cls-hneed-sumwd", data = _data78 });
            //외부온도
            data.Add(new { cname = "cls-temp-1wd", data = _data79 });
            data.Add(new { cname = "cls-temp-2wd", data = _data80 });
            data.Add(new { cname = "cls-temp-3wd", data = _data81 });
            data.Add(new { cname = "cls-temp-4wd", data = _data82 });
            data.Add(new { cname = "cls-temp-5wd", data = _data83 });
            data.Add(new { cname = "cls-temp-6wd", data = _data84 });
            data.Add(new { cname = "cls-temp-7wd", data = _data85 });
            data.Add(new { cname = "cls-temp-8wd", data = _data86 });
            data.Add(new { cname = "cls-temp-9wd", data = _data87 });
            data.Add(new { cname = "cls-temp-10wd", data = _data88 });
            data.Add(new { cname = "cls-temp-11wd", data = _data89 });
            data.Add(new { cname = "cls-temp-12wd", data = _data90 });
            //이용계수
            data.Add(new { cname = "cls-eta-1wd", data =_data91 });
            data.Add(new { cname = "cls-eta-2wd", data = _data92 });
            data.Add(new { cname = "cls-eta-3wd", data = _data93 });
            data.Add(new { cname = "cls-eta-4wd", data = _data94 });
            data.Add(new { cname = "cls-eta-5wd", data = _data95 });
            data.Add(new { cname = "cls-eta-6wd", data = _data96 });
            data.Add(new { cname = "cls-eta-7wd", data = _data97 });
            data.Add(new { cname = "cls-eta-8wd", data = _data98 });
            data.Add(new { cname = "cls-eta-9wd", data = _data99 });
            data.Add(new { cname = "cls-eta-10wd", data = _data100 });
            data.Add(new { cname = "cls-eta-11wd", data = _data101 });
            data.Add(new { cname = "cls-eta-12wd", data = _data102 });
            //관류열손실량
            data.Add(new { cname = "cls-lossqt-1wd", data = _data103 });
            data.Add(new { cname = "cls-lossqt-2wd", data = _data104 });
            data.Add(new { cname = "cls-lossqt-3wd", data = _data105 });
            data.Add(new { cname = "cls-lossqt-4wd", data = _data106 });
            data.Add(new { cname = "cls-lossqt-5wd", data = _data107 });
            data.Add(new { cname = "cls-lossqt-6wd", data = _data108 });
            data.Add(new { cname = "cls-lossqt-7wd", data = _data109 });
            data.Add(new { cname = "cls-lossqt-8wd", data = _data110 });
            data.Add(new { cname = "cls-lossqt-9wd", data = _data111 });
            data.Add(new { cname = "cls-lossqt-10wd", data = _data112 });
            data.Add(new { cname = "cls-lossqt-11wd", data = _data113 });
            data.Add(new { cname = "cls-lossqt-12wd", data = _data114 });
            //벽체열손실량
            data.Add(new { cname = "cls-losswall-1wd", data = _data115 });
            data.Add(new { cname = "cls-losswall-2wd", data = _data116 });
            data.Add(new { cname = "cls-losswall-3wd", data = _data117 });
            data.Add(new { cname = "cls-losswall-4wd", data = _data118 });
            data.Add(new { cname = "cls-losswall-5wd", data = _data119 });
            data.Add(new { cname = "cls-losswall-6wd", data = _data120 });
            data.Add(new { cname = "cls-losswall-7wd", data = _data121 });
            data.Add(new { cname = "cls-losswall-8wd", data = _data122 });
            data.Add(new { cname = "cls-losswall-9wd", data = _data123 });
            data.Add(new { cname = "cls-losswall-10wd", data = _data124 });
            data.Add(new { cname = "cls-losswall-11wd", data = _data125 });
            data.Add(new { cname = "cls-losswall-12wd", data = _data126 });
            //창호열손실량
            data.Add(new { cname = "cls-losswin-1wd", data = _data127 });
            data.Add(new { cname = "cls-losswin-2wd", data = _data128 });
            data.Add(new { cname = "cls-losswin-3wd", data = _data129 });
            data.Add(new { cname = "cls-losswin-4wd", data = _data130 });
            data.Add(new { cname = "cls-losswin-5wd", data = _data131 });
            data.Add(new { cname = "cls-losswin-6wd", data = _data132 });
            data.Add(new { cname = "cls-losswin-7wd", data = _data133 });
            data.Add(new { cname = "cls-losswin-8wd", data = _data134 });
            data.Add(new { cname = "cls-losswin-9wd", data = _data135 });
            data.Add(new { cname = "cls-losswin-10wd", data = _data136 });
            data.Add(new { cname = "cls-losswin-11wd", data = _data137 });
            data.Add(new { cname = "cls-losswin-12wd", data = _data138 });
            //커튼월열손실량
            data.Add(new { cname = "cls-losscw-1wd", data = _data139 });
            data.Add(new { cname = "cls-losscw-2wd", data = _data140 });
            data.Add(new { cname = "cls-losscw-3wd", data = _data141 });
            data.Add(new { cname = "cls-losscw-4wd", data = _data142 });
            data.Add(new { cname = "cls-losscw-5wd", data = _data143 });
            data.Add(new { cname = "cls-losscw-6wd", data = _data144 });
            data.Add(new { cname = "cls-losscw-7wd", data = _data145 });
            data.Add(new { cname = "cls-losscw-8wd", data = _data146 });
            data.Add(new { cname = "cls-losscw-9wd", data = _data147 });
            data.Add(new { cname = "cls-losscw-10wd", data = _data148 });
            data.Add(new { cname = "cls-losscw-11wd", data = _data149 });
            data.Add(new { cname = "cls-losscw-12wd", data = _data150 });
            //지붕열손실량
            data.Add(new { cname = "cls-lossroof-1wd", data = _data151 });
            data.Add(new { cname = "cls-lossroof-2wd", data = _data152 });
            data.Add(new { cname = "cls-lossroof-3wd", data = _data153 });
            data.Add(new { cname = "cls-lossroof-4wd", data = _data154 });
            data.Add(new { cname = "cls-lossroof-5wd", data = _data155 });
            data.Add(new { cname = "cls-lossroof-6wd", data = _data156 });
            data.Add(new { cname = "cls-lossroof-7wd", data = _data157 });
            data.Add(new { cname = "cls-lossroof-8wd", data = _data158 });
            data.Add(new { cname = "cls-lossroof-9wd", data = _data159 });
            data.Add(new { cname = "cls-lossroof-10wd", data = _data160 });
            data.Add(new { cname = "cls-lossroof-11wd", data = _data161 });
            data.Add(new { cname = "cls-lossroof-12wd", data = _data162 });
            //출입문열손실량
            data.Add(new { cname = "cls-lossdoor-1wd", data = _data163 });
            data.Add(new { cname = "cls-lossdoor-2wd", data = _data164 });
            data.Add(new { cname = "cls-lossdoor-3wd", data = _data165 });
            data.Add(new { cname = "cls-lossdoor-4wd", data = _data166 });
            data.Add(new { cname = "cls-lossdoor-5wd", data = _data167 });
            data.Add(new { cname = "cls-lossdoor-6wd", data = _data168 });
            data.Add(new { cname = "cls-lossdoor-7wd", data = _data169 });
            data.Add(new { cname = "cls-lossdoor-8wd", data = _data170 });
            data.Add(new { cname = "cls-lossdoor-9wd", data = _data171 });
            data.Add(new { cname = "cls-lossdoor-10wd", data = _data172 });
            data.Add(new { cname = "cls-lossdoor-11wd", data = _data173 });
            data.Add(new { cname = "cls-lossdoor-12wd", data = _data174 });
            //바닥열손실량
            data.Add(new { cname = "cls-lossfloor-1wd", data = _data175 });
            data.Add(new { cname = "cls-lossfloor-2wd", data = _data176 });
            data.Add(new { cname = "cls-lossfloor-3wd", data = _data177 });
            data.Add(new { cname = "cls-lossfloor-4wd", data = _data178 });
            data.Add(new { cname = "cls-lossfloor-5wd", data = _data179 });
            data.Add(new { cname = "cls-lossfloor-6wd", data = _data180 });
            data.Add(new { cname = "cls-lossfloor-7wd", data = _data181 });
            data.Add(new { cname = "cls-lossfloor-8wd", data = _data182 });
            data.Add(new { cname = "cls-lossfloor-9wd", data = _data183 });
            data.Add(new { cname = "cls-lossfloor-10wd", data = _data184 });
            data.Add(new { cname = "cls-lossfloor-11wd", data = _data185 });
            data.Add(new { cname = "cls-lossfloor-12wd", data = _data186 });
            //2D 열교 열손실량
            data.Add(new { cname = "cls-loss2d-1wd", data = _data187 });
            data.Add(new { cname = "cls-loss2d-2wd", data = _data188 });
            data.Add(new { cname = "cls-loss2d-3wd", data = _data189 });
            data.Add(new { cname = "cls-loss2d-4wd", data = _data190 });
            data.Add(new { cname = "cls-loss2d-5wd", data = _data191 });
            data.Add(new { cname = "cls-loss2d-6wd", data = _data192 });
            data.Add(new { cname = "cls-loss2d-7wd", data = _data193 });
            data.Add(new { cname = "cls-loss2d-8wd", data = _data194 });
            data.Add(new { cname = "cls-loss2d-9wd", data = _data195 });
            data.Add(new { cname = "cls-loss2d-10wd", data = _data196 });
            data.Add(new { cname = "cls-loss2d-11wd", data = _data197 });
            data.Add(new { cname = "cls-loss2d-12wd", data = _data198 });
            //환기 열손실량
            data.Add(new { cname = "cls-lossvent-1wd", data = _data199 });
            data.Add(new { cname = "cls-lossvent-2wd", data = _data200 });
            data.Add(new { cname = "cls-lossvent-3wd", data = _data201 });
            data.Add(new { cname = "cls-lossvent-4wd", data = _data202 });
            data.Add(new { cname = "cls-lossvent-5wd", data = _data203 });
            data.Add(new { cname = "cls-lossvent-6wd", data = _data204 });
            data.Add(new { cname = "cls-lossvent-7wd", data = _data205 });
            data.Add(new { cname = "cls-lossvent-8wd", data = _data206 });
            data.Add(new { cname = "cls-lossvent-9wd", data = _data207 });
            data.Add(new { cname = "cls-lossvent-10wd", data = _data208 });
            data.Add(new { cname = "cls-lossvent-11wd", data = _data209 });
            data.Add(new { cname = "cls-lossvent-12wd", data = _data210 });
            //침기 열손실량
            data.Add(new { cname = "cls-lossinf-1wd", data = _data211 });
            data.Add(new { cname = "cls-lossinf-2wd", data = _data212 });
            data.Add(new { cname = "cls-lossinf-3wd", data = _data213 });
            data.Add(new { cname = "cls-lossinf-4wd", data = _data214 });
            data.Add(new { cname = "cls-lossinf-5wd", data = _data215 });
            data.Add(new { cname = "cls-lossinf-6wd", data = _data216 });
            data.Add(new { cname = "cls-lossinf-7wd", data = _data217 });
            data.Add(new { cname = "cls-lossinf-8wd", data = _data218 });
            data.Add(new { cname = "cls-lossinf-9wd", data = _data219 });
            data.Add(new { cname = "cls-lossinf-10wd", data = _data220 });
            data.Add(new { cname = "cls-lossinf-11wd", data = _data221 });
            data.Add(new { cname = "cls-lossinf-12wd", data = _data222 });
            //기계환기 열손실량
            data.Add(new { cname = "cls-lossmech-1wd", data = _data223 });
            data.Add(new { cname = "cls-lossmech-2wd", data = _data224 });
            data.Add(new { cname = "cls-lossmech-3wd", data = _data225 });
            data.Add(new { cname = "cls-lossmech-4wd", data = _data226 });
            data.Add(new { cname = "cls-lossmech-5wd", data = _data227 });
            data.Add(new { cname = "cls-lossmech-6wd", data = _data228 });
            data.Add(new { cname = "cls-lossmech-7wd", data = _data229 });
            data.Add(new { cname = "cls-lossmech-8wd", data = _data230 });
            data.Add(new { cname = "cls-lossmech-9wd", data = _data231 });
            data.Add(new { cname = "cls-lossmech-10wd", data = _data232 });
            data.Add(new { cname = "cls-lossmech-11wd", data = _data233 });
            data.Add(new { cname = "cls-lossmech-12wd", data = _data234 });
            //자연환기 열손실량
            data.Add(new { cname = "cls-losswin-1wd", data = _data235 });
            data.Add(new { cname = "cls-losswin-2wd", data = _data236 });
            data.Add(new { cname = "cls-losswin-3wd", data = _data237 });
            data.Add(new { cname = "cls-losswin-4wd", data = _data238 });
            data.Add(new { cname = "cls-losswin-5wd", data = _data239 });
            data.Add(new { cname = "cls-losswin-6wd", data = _data240 });
            data.Add(new { cname = "cls-losswin-7wd", data = _data241 });
            data.Add(new { cname = "cls-losswin-8wd", data = _data242 });
            data.Add(new { cname = "cls-losswin-9wd", data = _data243 });
            data.Add(new { cname = "cls-losswin-10wd", data = _data244 });
            data.Add(new { cname = "cls-losswin-11wd", data = _data245 });
            data.Add(new { cname = "cls-losswin-12wd", data = _data246 });

            //일사열획득량
            data.Add(new { cname = "cls-qstr-tot-1wd", data = _data247 });
            data.Add(new { cname = "cls-qstr-tot-2wd", data = _data248 });
            data.Add(new { cname = "cls-qstr-tot-3wd", data = _data249 });
            data.Add(new { cname = "cls-qstr-tot-4wd", data = _data250 });
            data.Add(new { cname = "cls-qstr-tot-5wd", data = _data251 });
            data.Add(new { cname = "cls-qstr-tot-6wd", data = _data252 });
            data.Add(new { cname = "cls-qstr-tot-7wd", data = _data253 });
            data.Add(new { cname = "cls-qstr-tot-8wd", data = _data254 });
            data.Add(new { cname = "cls-qstr-tot-9wd", data = _data255 });
            data.Add(new { cname = "cls-qstr-tot-10wd", data = _data256 });
            data.Add(new { cname = "cls-qstr-tot-11wd", data = _data257 });
            data.Add(new { cname = "cls-qstr-tot-12wd", data = _data258 });
            //창호 일사열획득량
            data.Add(new { cname = "cls-qstrwin-1wd", data = _data259 });
            data.Add(new { cname = "cls-qstrwin-2wd", data = _data260 });
            data.Add(new { cname = "cls-qstrwin-3wd", data = _data261 });
            data.Add(new { cname = "cls-qstrwin-4wd", data = _data262 });
            data.Add(new { cname = "cls-qstrwin-5wd", data = _data263 });
            data.Add(new { cname = "cls-qstrwin-6wd", data = _data264 });
            data.Add(new { cname = "cls-qstrwin-7wd", data = _data265 });
            data.Add(new { cname = "cls-qstrwin-8wd", data = _data266 });
            data.Add(new { cname = "cls-qstrwin-9wd", data = _data267 });
            data.Add(new { cname = "cls-qstrwin-10wd", data = _data268 });
            data.Add(new { cname = "cls-qstrwin-11wd", data = _data269 });
            data.Add(new { cname = "cls-qstrwin-12wd", data = _data270 });
            //커튼월 일사열획득량
            data.Add(new { cname = "cls-qstrcw-1wd", data = _data271 });
            data.Add(new { cname = "cls-qstrcw-2wd", data = _data272 });
            data.Add(new { cname = "cls-qstrcw-3wd", data = _data273 });
            data.Add(new { cname = "cls-qstrcw-4wd", data = _data274 });
            data.Add(new { cname = "cls-qstrcw-5wd", data = _data275 });
            data.Add(new { cname = "cls-qstrcw-6wd", data = _data276 });
            data.Add(new { cname = "cls-qstrcw-7wd", data = _data277 });
            data.Add(new { cname = "cls-qstrcw-8wd", data = _data278 });
            data.Add(new { cname = "cls-qstrcw-9wd", data = _data279 });
            data.Add(new { cname = "cls-qstrcw-10wd", data = _data280 });
            data.Add(new { cname = "cls-qstrcw-11wd", data = _data281 });
            data.Add(new { cname = "cls-qstrcw-12wd", data = _data282 });
            //외벽 일사열획득량
            data.Add(new { cname = "cls-qswall-1wd", data = _data283 });
            data.Add(new { cname = "cls-qswall-2wd", data = _data284 });
            data.Add(new { cname = "cls-qswall-3wd", data = _data285 });
            data.Add(new { cname = "cls-qswall-4wd", data = _data286 });
            data.Add(new { cname = "cls-qswall-5wd", data = _data287 });
            data.Add(new { cname = "cls-qswall-6wd", data = _data288 });
            data.Add(new { cname = "cls-qswall-7wd", data = _data289 });
            data.Add(new { cname = "cls-qswall-8wd", data = _data290 });
            data.Add(new { cname = "cls-qswall-9wd", data = _data291 });
            data.Add(new { cname = "cls-qswall-10wd", data = _data292 });
            data.Add(new { cname = "cls-qswall-11wd", data = _data293 });
            data.Add(new { cname = "cls-qswall-12wd", data = _data294 });
            //지붕 일사열획득량
            data.Add(new { cname = "cls-qsroof-1wd", data = _data295 });
            data.Add(new { cname = "cls-qsroof-2wd", data = _data296 });
            data.Add(new { cname = "cls-qsroof-3wd", data = _data297 });
            data.Add(new { cname = "cls-qsroof-4wd", data = _data298 });
            data.Add(new { cname = "cls-qsroof-5wd", data = _data299 });
            data.Add(new { cname = "cls-qsroof-6wd", data = _data300 });
            data.Add(new { cname = "cls-qsroof-7wd", data = _data301 });
            data.Add(new { cname = "cls-qsroof-8wd", data = _data302 });
            data.Add(new { cname = "cls-qsroof-9wd", data = _data303 });
            data.Add(new { cname = "cls-qsroof-10wd", data = _data304 });
            data.Add(new { cname = "cls-qsroof-11wd", data = _data305 });
            data.Add(new { cname = "cls-qsroof-12wd", data = _data306 });
            //출입문 일사열획득량
            data.Add(new { cname = "cls-qsdoor-1wd", data = _data307 });
            data.Add(new { cname = "cls-qsdoor-2wd", data = _data308 });
            data.Add(new { cname = "cls-qsdoor-3wd", data = _data309 });
            data.Add(new { cname = "cls-qsdoor-4wd", data = _data310 });
            data.Add(new { cname = "cls-qsdoor-5wd", data = _data311 });
            data.Add(new { cname = "cls-qsdoor-6wd", data = _data312 });
            data.Add(new { cname = "cls-qsdoor-7wd", data = _data313 });
            data.Add(new { cname = "cls-qsdoor-8wd", data = _data314 });
            data.Add(new { cname = "cls-qsdoor-9wd", data = _data315 });
            data.Add(new { cname = "cls-qsdoor-10wd", data = _data316 });
            data.Add(new { cname = "cls-qsdoor-11wd", data = _data317 });
            data.Add(new { cname = "cls-qsdoor-12wd", data = _data318 });
            //내부발열
            data.Add(new { cname = "cls-indoor-1wd", data = _data319 });
            data.Add(new { cname = "cls-indoor-2wd", data = _data320 });
            data.Add(new { cname = "cls-indoor-3wd", data = _data321 });
            data.Add(new { cname = "cls-indoor-4wd", data = _data322 });
            data.Add(new { cname = "cls-indoor-5wd", data = _data323 });
            data.Add(new { cname = "cls-indoor-6wd", data = _data324 });
            data.Add(new { cname = "cls-indoor-7wd", data = _data325 });
            data.Add(new { cname = "cls-indoor-8wd", data = _data326 });
            data.Add(new { cname = "cls-indoor-9wd", data = _data327 });
            data.Add(new { cname = "cls-indoor-10wd", data = _data328 });
            data.Add(new { cname = "cls-indoor-11wd", data = _data329 });
            data.Add(new { cname = "cls-indoor-12wd", data = _data330 });
            //내부조명
            data.Add(new { cname = "cls-inlight-1wd", data = _data331 });
            data.Add(new { cname = "cls-inlight-2wd", data = _data332 });
            data.Add(new { cname = "cls-inlight-3wd", data = _data333 });
            data.Add(new { cname = "cls-inlight-4wd", data = _data334 });
            data.Add(new { cname = "cls-inlight-5wd", data = _data335 });
            data.Add(new { cname = "cls-inlight-6wd", data = _data336 });
            data.Add(new { cname = "cls-inlight-7wd", data = _data337 });
            data.Add(new { cname = "cls-inlight-8wd", data = _data338 });
            data.Add(new { cname = "cls-inlight-9wd", data = _data339 });
            data.Add(new { cname = "cls-inlight-10wd", data = _data340 });
            data.Add(new { cname = "cls-inlight-11wd", data = _data341 });
            data.Add(new { cname = "cls-inlight-12wd", data = _data342 });
            //내부인간
            data.Add(new { cname = "cls-inp-1wd", data = _data343 });
            data.Add(new { cname = "cls-inp-2wd", data = _data344 });
            data.Add(new { cname = "cls-inp-3wd", data = _data345 });
            data.Add(new { cname = "cls-inp-4wd", data = _data346 });
            data.Add(new { cname = "cls-inp-5wd", data = _data347 });
            data.Add(new { cname = "cls-inp-6wd", data = _data348 });
            data.Add(new { cname = "cls-inp-7wd", data = _data349 });
            data.Add(new { cname = "cls-inp-8wd", data = _data350 });
            data.Add(new { cname = "cls-inp-9wd", data = _data351 });
            data.Add(new { cname = "cls-inp-10wd", data = _data352 });
            data.Add(new { cname = "cls-inp-11wd", data = _data353 });
            data.Add(new { cname = "cls-inp-12wd", data = _data354 });
            //내부기기 
            data.Add(new { cname = "cls-ine-1wd", data = _data355 });
            data.Add(new { cname = "cls-ine-2wd", data = _data356 });
            data.Add(new { cname = "cls-ine-3wd", data = _data357 });
            data.Add(new { cname = "cls-ine-4wd", data = _data358 });
            data.Add(new { cname = "cls-ine-5wd", data = _data359 });
            data.Add(new { cname = "cls-ine-6wd", data = _data360 });
            data.Add(new { cname = "cls-ine-7wd", data = _data361 });
            data.Add(new { cname = "cls-ine-8wd", data = _data362 });
            data.Add(new { cname = "cls-ine-9wd", data = _data363 });
            data.Add(new { cname = "cls-ine-10wd", data = _data364 });
            data.Add(new { cname = "cls-ine-11wd", data = _data365 });
            data.Add(new { cname = "cls-ine-12wd", data = _data366 });
            //비이용일 난방요구량
            data.Add(new { cname = "cls-hneed-1we", data = _data367 });
            data.Add(new { cname = "cls-hneed-2we", data = _data368 });
            data.Add(new { cname = "cls-hneed-3we", data = _data369 });
            data.Add(new { cname = "cls-hneed-4we", data = _data370 });
            data.Add(new { cname = "cls-hneed-5we", data = _data371 });
            data.Add(new { cname = "cls-hneed-6we", data = _data372 });
            data.Add(new { cname = "cls-hneed-7we", data = _data373 });
            data.Add(new { cname = "cls-hneed-8we", data = _data374 });
            data.Add(new { cname = "cls-hneed-9we", data = _data375 });
            data.Add(new { cname = "cls-hneed-10we", data = _data376 });
            data.Add(new { cname = "cls-hneed-11we", data = _data377 });
            data.Add(new { cname = "cls-hneed-12we", data = _data378 });
            data.Add(new { cname = "cls-hneed-12we", data = _data379 }); //합산값 
            //대차축열량
            data.Add(new { cname = "cls-dqcb-1we", data = _data380 });
            data.Add(new { cname = "cls-dqcb-2we", data = _data381 });
            data.Add(new { cname = "cls-dqcb-3we", data = _data382 });
            data.Add(new { cname = "cls-dqcb-4we", data = _data383 });
            data.Add(new { cname = "cls-dqcb-5we", data = _data384 });
            data.Add(new { cname = "cls-dqcb-6we", data = _data385 });
            data.Add(new { cname = "cls-dqcb-7we", data = _data386 });
            data.Add(new { cname = "cls-dqcb-8we", data = _data387 });
            data.Add(new { cname = "cls-dqcb-9we", data = _data388 });
            data.Add(new { cname = "cls-dqcb-10we", data = _data389 });
            data.Add(new { cname = "cls-dqcb-11we", data = _data390 });
            data.Add(new { cname = "cls-dqcb-12we", data = _data391 });
            //비이용일 관류열손실
            data.Add(new { cname = "cls-lossqt-1we", data = _data392 });
            data.Add(new { cname = "cls-lossqt-2we", data = _data393 });
            data.Add(new { cname = "cls-lossqt-3we", data = _data394 });
            data.Add(new { cname = "cls-lossqt-4we", data = _data395 });
            data.Add(new { cname = "cls-lossqt-5we", data = _data396 });
            data.Add(new { cname = "cls-lossqt-6we", data = _data397 });
            data.Add(new { cname = "cls-lossqt-7we", data = _data398 });
            data.Add(new { cname = "cls-lossqt-8we", data = _data399 });
            data.Add(new { cname = "cls-lossqt-9we", data = _data400 });
            data.Add(new { cname = "cls-lossqt-10we", data = _data401 });
            data.Add(new { cname = "cls-lossqt-11we", data = _data402 });
            data.Add(new { cname = "cls-lossqt-12we", data = _data403 });
            //비이용일 환기열손실
            data.Add(new { cname = "cls-lossven-1we", data = _data404 });
            data.Add(new { cname = "cls-lossven-2we", data = _data405 });
            data.Add(new { cname = "cls-lossven-3we", data = _data406 });
            data.Add(new { cname = "cls-lossven-4we", data = _data407 });
            data.Add(new { cname = "cls-lossven-5we", data = _data408 });
            data.Add(new { cname = "cls-lossven-6we", data = _data409 });
            data.Add(new { cname = "cls-lossven-7we", data = _data410 });
            data.Add(new { cname = "cls-lossven-8we", data = _data411 });
            data.Add(new { cname = "cls-lossven-9we", data = _data412 });
            data.Add(new { cname = "cls-lossven-10we", data = _data413 });
            data.Add(new { cname = "cls-lossven-11we", data = _data414 });
            data.Add(new { cname = "cls-lossven-12we", data = _data415 });
            //비이용일 일사열획득
            data.Add(new { cname = "cls-qstr-1we", data = _data416 });
            data.Add(new { cname = "cls-qstr-2we", data = _data417 });
            data.Add(new { cname = "cls-qstr-3we", data = _data418 });
            data.Add(new { cname = "cls-qstr-4we", data = _data419 });
            data.Add(new { cname = "cls-qstr-5we", data = _data420 });
            data.Add(new { cname = "cls-qstr-6we", data = _data421 });
            data.Add(new { cname = "cls-qstr-7we", data = _data422 });
            data.Add(new { cname = "cls-qstr-8we", data = _data423 });
            data.Add(new { cname = "cls-qstr-9we", data = _data424 });
            data.Add(new { cname = "cls-qstr-10we", data = _data425 });
            data.Add(new { cname = "cls-qstr-11we", data = _data426 });
            data.Add(new { cname = "cls-qstr-12we", data = _data427 });
            //비용일 내부발열
            data.Add(new { cname = "cls-indoor-1we", data = _data428 });
            data.Add(new { cname = "cls-indoor-2we", data = _data429 });
            data.Add(new { cname = "cls-indoor-3we", data = _data430 });
            data.Add(new { cname = "cls-indoor-4we", data = _data431 });
            data.Add(new { cname = "cls-indoor-5we", data = _data432 });
            data.Add(new { cname = "cls-indoor-6we", data = _data433 });
            data.Add(new { cname = "cls-indoor-7we", data = _data434 });
            data.Add(new { cname = "cls-indoor-8we", data = _data435 });
            data.Add(new { cname = "cls-indoor-9we", data = _data436 });
            data.Add(new { cname = "cls-indoor-10we", data = _data437 });
            data.Add(new { cname = "cls-indoor-11we", data = _data438 });
            data.Add(new { cname = "cls-indoor-12we", data = _data439 });
            //냉방 요구량 
            data.Add(new { cname = "cls-cneed-1wd", data = _data440 });
            data.Add(new { cname = "cls-cneed-2wd", data = _data441 });
            data.Add(new { cname = "cls-cnned-3wd", data = _data442 });
            data.Add(new { cname = "cls-cnned-4wd", data = _data443 });
            data.Add(new { cname = "cls-cnned-5wd", data = _data444 });
            data.Add(new { cname = "cls-cnned-6wd", data = _data445 });
            data.Add(new { cname = "cls-cnned-7wd", data = _data446 });
            data.Add(new { cname = "cls-cnned-8wd", data = _data447 });
            data.Add(new { cname = "cls-cnned-9wd", data = _data448 });
            data.Add(new { cname = "cls-cnned-10wd", data = _data449 });
            data.Add(new { cname = "cls-cnned-11wd", data = _data450 });
            data.Add(new { cname = "cls-cnned-12wd", data = _data451 });
            data.Add(new { cname = "cls-cnned-13wd", data = _data452 });
            //냉방 요구량 제습포함
            data.Add(new { cname = "cls-cneed2-1wd", data = _data453 });
            data.Add(new { cname = "cls-cneed2-2wd", data = _data454 });
            data.Add(new { cname = "cls-cnned2-3wd", data = _data455});
            data.Add(new { cname = "cls-cnned2-4wd", data = _data456 });
            data.Add(new { cname = "cls-cnned2-5wd", data = _data457 });
            data.Add(new { cname = "cls-cnned2-6wd", data = _data458 });
            data.Add(new { cname = "cls-cnned2-7wd", data = _data459 });
            data.Add(new { cname = "cls-cnned2-8wd", data = _data460 });
            data.Add(new { cname = "cls-cnned2-9wd", data = _data461 });
            data.Add(new { cname = "cls-cnned2-10wd", data = _data462 });
            data.Add(new { cname = "cls-cnned2-11wd", data = _data463 });
            data.Add(new { cname = "cls-cnned2-12wd", data = _data463 });
            data.Add(new { cname = "cls-cnned2-13wd", data = _data464 });
            //제습 요구량 
            data.Add(new { cname = "cls-cneed2-1wd", data = _data465 });
            data.Add(new { cname = "cls-cneed2-2wd", data = _data466 });
            data.Add(new { cname = "cls-cnned2-3wd", data = _data467 });
            data.Add(new { cname = "cls-cnned2-4wd", data = _data468 });
            data.Add(new { cname = "cls-cnned2-5wd", data = _data469 });
            data.Add(new { cname = "cls-cnned2-6wd", data = _data470 });
            data.Add(new { cname = "cls-cnned2-7wd", data = _data471 });
            data.Add(new { cname = "cls-cnned2-8wd", data = _data472 });
            data.Add(new { cname = "cls-cnned2-9wd", data = _data473 });
            data.Add(new { cname = "cls-cnned2-10wd", data = _data474 });
            data.Add(new { cname = "cls-cnned2-11wd", data = _data475 });
            data.Add(new { cname = "cls-cnned2-12wd", data = _data476 });
            data.Add(new { cname = "cls-cnned2-13wd", data = _data477 });
            //제습 요구량 
            data.Add(new { cname = "cls-cneed2-1wd", data = _data465 });
            data.Add(new { cname = "cls-cneed2-2wd", data = _data466 });
            data.Add(new { cname = "cls-cnned2-3wd", data = _data467 });
            data.Add(new { cname = "cls-cnned2-4wd", data = _data468 });
            data.Add(new { cname = "cls-cnned2-5wd", data = _data469 });
            data.Add(new { cname = "cls-cnned2-6wd", data = _data470 });
            data.Add(new { cname = "cls-cnned2-7wd", data = _data471 });
            data.Add(new { cname = "cls-cnned2-8wd", data = _data472 });
            data.Add(new { cname = "cls-cnned2-9wd", data = _data473 });
            data.Add(new { cname = "cls-cnned2-10wd", data = _data474 });
            data.Add(new { cname = "cls-cnned2-11wd", data = _data475 });
            data.Add(new { cname = "cls-cnned2-12wd", data = _data476 });
            data.Add(new { cname = "cls-cnned2-13wd", data = _data477 });
            //제습 요구량 
            data.Add(new { cname = "cls-cneed2-1wd", data = _data478 });
            data.Add(new { cname = "cls-cneed2-2wd", data = _data466 });
            data.Add(new { cname = "cls-cnned2-3wd", data = _data467 });
            data.Add(new { cname = "cls-cnned2-4wd", data = _data468 });
            data.Add(new { cname = "cls-cnned2-5wd", data = _data469 });
            data.Add(new { cname = "cls-cnned2-6wd", data = _data470 });
            data.Add(new { cname = "cls-cnned2-7wd", data = _data471 });
            data.Add(new { cname = "cls-cnned2-8wd", data = _data472 });
            data.Add(new { cname = "cls-cnned2-9wd", data = _data473 });
            data.Add(new { cname = "cls-cnned2-10wd", data = _data474 });
            data.Add(new { cname = "cls-cnned2-11wd", data = _data475 });
            data.Add(new { cname = "cls-cnned2-12wd", data = _data476 });
            data.Add(new { cname = "cls-cnned2-13wd", data = _data477 });
            //냉방기준온도
            data.Add(new { cname = "cls-ctemp-1wd", data = _data478 });
            data.Add(new { cname = "cls-ctemp-2wd", data = _data479 });
            data.Add(new { cname = "cls-ctemp-3wd", data = _data480 });
            data.Add(new { cname = "cls-ctemp-4wd", data = _data481 });
            data.Add(new { cname = "cls-ctemp-5wd", data = _data482 });
            data.Add(new { cname = "cls-ctemp-6wd", data = _data483 });
            data.Add(new { cname = "cls-ctemp-7wd", data = _data484 });
            data.Add(new { cname = "cls-ctemp-8wd", data = _data485 });
            data.Add(new { cname = "cls-ctemp-9wd", data = _data486 });
            data.Add(new { cname = "cls-cnned2-10wd", data = _data487 });
            data.Add(new { cname = "cls-ctemp-11wd", data = _data488 });
            data.Add(new { cname = "cls-ctemp-12wd", data = _data489 });
            //관류열획득량 
            data.Add(new { cname = "cls-qtsource-1wd", data = _data490 });
            data.Add(new { cname = "cls-qtsource-2wd", data = _data491 });
            data.Add(new { cname = "cls-qtsource-3wd", data = _data492 });
            data.Add(new { cname = "cls-qtsource-4wd", data = _data493 });
            data.Add(new { cname = "cls-qtsource-5wd", data = _data494 });
            data.Add(new { cname = "cls-qtsource-6wd", data = _data495 });
            data.Add(new { cname = "cls-qtsource-7wd", data = _data496 });
            data.Add(new { cname = "cls-qtsource-8wd", data = _data497 });
            data.Add(new { cname = "cls-qtsource-9wd", data = _data498 });
            data.Add(new { cname = "cls-qtsource-10wd", data = _data499 });
            data.Add(new { cname = "cls-qtsource-11wd", data = _data500 });
            data.Add(new { cname = "cls-qtsource-12wd", data = _data501 });
            //환기열획득량 
            data.Add(new { cname = "cls-qvsource-1wd", data = _data502 });
            data.Add(new { cname = "cls-qvsource-2wd", data = _data503 });
            data.Add(new { cname = "cls-qvsource-3wd", data = _data504 });
            data.Add(new { cname = "cls-qvsource-4wd", data = _data505 });
            data.Add(new { cname = "cls-qvsource-5wd", data = _data506 });
            data.Add(new { cname = "cls-qvsource-6wd", data = _data507 });
            data.Add(new { cname = "cls-qvsource-7wd", data = _data508 });
            data.Add(new { cname = "cls-qvsource-8wd", data = _data509 });
            data.Add(new { cname = "cls-qvsource-9wd", data = _data510 });
            data.Add(new { cname = "cls-qvsource-10wd", data = _data511 });
            data.Add(new { cname = "cls-qvsource-11wd", data = _data512 });
            data.Add(new { cname = "cls-qvsource-12wd", data = _data513 });
            //일사열획득량 
            data.Add(new { cname = "cls-qssource-1wd", data = _data514 });
            data.Add(new { cname = "cls-qssource-2wd", data = _data515 });
            data.Add(new { cname = "cls-qssource-3wd", data = _data516 });
            data.Add(new { cname = "cls-qssource-4wd", data = _data517 });
            data.Add(new { cname = "cls-qssource-5wd", data = _data518 });
            data.Add(new { cname = "cls-qssource-6wd", data = _data519 });
            data.Add(new { cname = "cls-qssource-7wd", data = _data520 });
            data.Add(new { cname = "cls-qssource-8wd", data = _data521 });
            data.Add(new { cname = "cls-qssource-9wd", data = _data522 });
            data.Add(new { cname = "cls-qssource-10wd", data = _data523 });
            data.Add(new { cname = "cls-qssource-11wd", data = _data524 });
            data.Add(new { cname = "cls-qssource-12wd", data = _data525 });
            //비이용일 냉방 요구량
            data.Add(new { cname = "cls-cneed-1we", data = _data526 });
            data.Add(new { cname = "cls-cneed-2we", data = _data527 });
            data.Add(new { cname = "cls-cneed-3we", data = _data528 });
            data.Add(new { cname = "cls-cneed-4we", data = _data529 });
            data.Add(new { cname = "cls-cneed-5we", data = _data530 });
            data.Add(new { cname = "cls-cneed-6we", data = _data531 });
            data.Add(new { cname = "cls-cneed-7we", data = _data532 });
            data.Add(new { cname = "cls-cneed-8we", data = _data533 });
            data.Add(new { cname = "cls-cneed-9we", data = _data534 });
            data.Add(new { cname = "cls-cneed-10we", data = _data535 });
            data.Add(new { cname = "cls-cneed-11we", data = _data536 });
            data.Add(new { cname = "cls-cneed-12we", data = _data537 });
            data.Add(new { cname = "cls-cneed-13we", data = _data538 });
            //비이용일 냉방 기준온도
            data.Add(new { cname = "cls-ctemp-1we", data = _data539 });
            data.Add(new { cname = "cls-ctemp-2we", data = _data540 });
            data.Add(new { cname = "cls-ctemp-3we", data = _data541 });
            data.Add(new { cname = "cls-ctemp-4we", data = _data542 });
            data.Add(new { cname = "cls-ctemp-5we", data = _data543 });
            data.Add(new { cname = "cls-ctemp-6we", data = _data544 });
            data.Add(new { cname = "cls-ctemp-7we", data = _data545 });
            data.Add(new { cname = "cls-ctemp-8we", data = _data546 });
            data.Add(new { cname = "cls-ctemp-9we", data = _data547 });
            data.Add(new { cname = "cls-ctemp-10we", data = _data548 });
            data.Add(new { cname = "cls-ctemp-11we", data = _data549 });
            data.Add(new { cname = "cls-ctemp-12we", data = _data550 });
            //최대난방부하
            data.Add(new { cname = "cls-hload-max", data = _data551 });
            //난방시간
            data.Add(new { cname = "cls-htime-1", data = _data552 });
            data.Add(new { cname = "cls-htime-2", data = _data553 });
            data.Add(new { cname = "cls-htime-3", data = _data554 });
            data.Add(new { cname = "cls-htime-4", data = _data555 });
            data.Add(new { cname = "cls-htime-5", data = _data556 });
            data.Add(new { cname = "cls-htime-6", data = _data557 });
            data.Add(new { cname = "cls-htime-7", data = _data558 });
            data.Add(new { cname = "cls-htime-8", data = _data559 });
            data.Add(new { cname = "cls-htime-9", data = _data560 });
            data.Add(new { cname = "cls-htime-10", data = _data561 });
            data.Add(new { cname = "cls-htime-11", data = _data562 });
            data.Add(new { cname = "cls-htime-12", data = _data563 });
            //최대냉방부하
            data.Add(new { cname = "cls-cload-max", data = _data564 });
            //냉방시간
            data.Add(new { cname = "cls-ctime-1", data = _data565 });
            data.Add(new { cname = "cls-ctime-2", data = _data566 });
            data.Add(new { cname = "cls-ctime-3", data = _data567 });
            data.Add(new { cname = "cls-ctime-4", data = _data568 });
            data.Add(new { cname = "cls-ctime-5", data = _data569 });
            data.Add(new { cname = "cls-ctime-6", data = _data570 });
            data.Add(new { cname = "cls-ctime-7", data = _data571 });
            data.Add(new { cname = "cls-ctime-8", data = _data572 });
            data.Add(new { cname = "cls-ctime-9", data = _data573 });
            data.Add(new { cname = "cls-ctime-10", data = _data574 });
            data.Add(new { cname = "cls-ctime-11", data = _data575 });
            data.Add(new { cname = "cls-ctime-12", data = _data576 });
            //조명요구량
            data.Add(new { cname = "cls-lneed-1", data = _data577 });
            data.Add(new { cname = "cls-lneed-2", data = _data578 });
            data.Add(new { cname = "cls-lneed-3", data = _data579 });
            data.Add(new { cname = "cls-lneed-4", data = _data580 });
            data.Add(new { cname = "cls-lneed-5", data = _data581 });
            data.Add(new { cname = "cls-lneed-6", data = _data582 });
            data.Add(new { cname = "cls-lneed-7", data = _data583 });
            data.Add(new { cname = "cls-lneed-8", data = _data584 });
            data.Add(new { cname = "cls-lneed-9", data = _data585 });
            data.Add(new { cname = "cls-lneed-10", data = _data586 });
            data.Add(new { cname = "cls-lneed-11", data = _data587 });
            data.Add(new { cname = "cls-lneed-12", data = _data588 });
            data.Add(new { cname = "cls-lneed-13", data = _data589 });
            //중간부분
            data.Add(new { cname = "cls-wall-area", data = _data590 });
            data.Add(new { cname = "cls-cwall-area", data = _data591 });
            data.Add(new { cname = "cls-roof-area", data = _data592 });
            data.Add(new { cname = "cls-window-area", data = _data593 });
            data.Add(new { cname = "cls-floor-area", data = _data594 });
            data.Add(new { cname = "cls-wall-u", data = _data595 });
            data.Add(new { cname = "cls-cwall-u", data = _data596 });
            data.Add(new { cname = "cls-roof-u", data = _data597 });
            data.Add(new { cname = "cls-win-u", data = _data598 });
            data.Add(new { cname = "cls-floor-u", data = _data599 });
            //현재 열관류율 시트가 따로없음
            data.Add(new { cname = "cls-wall-ueff", data = _data595 });
            data.Add(new { cname = "cls-cwall-ueff", data = _data596 });
            data.Add(new { cname = "cls-roof-ueff", data = _data597 });
            data.Add(new { cname = "cls-window-ueff", data = _data598 });
            data.Add(new { cname = "cls-floor-ueff", data = _data599 });

            data.Add(new { cname = "cls-wall-h", data = _data600 });
            data.Add(new { cname = "cls-cwall-h", data = _data601});
            data.Add(new { cname = "cls-roof-h", data = _data602 });
            data.Add(new { cname = "cls-window-h", data = _data603 });
            data.Add(new { cname = "cls-floor-h", data = _data604 });




            s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
            s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());

            runScript("init(" + s + "," + s2 + ")");

  
        }




        private void button1_Click(object sender, EventArgs e)
        {
            webView21.CoreWebView2.ShowPrintUI();
        }
    }
}
