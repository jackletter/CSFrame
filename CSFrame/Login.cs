using CefSharp;
using CSFrame;
using CSFrame.Browser;
using CSFrame.Ui;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CSFrame
{
    public partial class Login : FormBase
    {

        public static Login _mainForm;
        public static CrBrowser crBrowser;
        public Login()
        {
            _mainForm = this;
            InitializeComponent();
            this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            InitBrowser(); // 初始化浏览器     
            crBrowser.FrmModel = FormModel.FixedDialog;
        }

        public void InitBrowser()
        {
            //String htmlPath = string.Format(@"{0}\web\login.html", Application.StartupPath);
            string htmlPath = System.Configuration.ConfigurationManager.AppSettings["htmlPath"];
            htmlPath = new DESEncrypt().Decrypt(htmlPath, "dfgtre42");
            if (!htmlPath.StartsWith("http") && !htmlPath.StartsWith("nacollector"))
            {
                htmlPath = Path.GetFullPath(htmlPath);
            }
            CefCustomObject.HtmlPathRoot = htmlPath;
            htmlPath = htmlPath + "login.html";
            crBrowser = new CrBrowser(this, htmlPath);
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            chromeBrowser = crBrowser.GetBrowser();
            cef = new CefCustomObject(crBrowser, this);
            crBrowser.GetBrowser().RegisterJsObject("cef", cef, new BindingOptions()
            {
                CamelCaseJavascriptNames = false
            });
            _mainForm.Controls.Add(crBrowser.GetBrowser());
        }
    }
}
