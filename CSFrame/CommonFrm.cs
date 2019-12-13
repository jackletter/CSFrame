using CefSharp;
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

namespace CSFrame
{
    public partial class CommonFrm : FormBase
    {
        public static FormBase _mainForm;
        public static CrBrowser crBrowser;

        private readonly string _url = "";

        public CommonFrm(int width, int height, string url, string title)
        {
            _url = url;
            _mainForm = this;
            this.Text = title;
            InitializeComponent();
            this.Width = width;
            this.Height = height;
            this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            InitBrowser(); // 初始化浏览器     
            crBrowser.FrmModel = FormModel.FixedDialog;
        }
        public void InitBrowser()
        {
            String htmlPath = CefCustomObject.HtmlPathRoot + _url;
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
