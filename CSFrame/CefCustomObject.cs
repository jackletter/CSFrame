using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.Diagnostics;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using CSFrame.Ui;
using CSFrame.Browser;

namespace CSFrame
{
    public class CefCustomObject
    {
        public static string HtmlPathRoot { set; get; }
        // Declare a local instance of chromium and the main form in order to execute things from here in the main thread
        private ChromiumWebBrowser _instanceBrowser = null;
        // The form class needs to be changed according to yours
        private FormBase _instanceMainForm = null;
        private CrBrowser crBrowser = null;

        public static Login login = null;
        public static Form1 mainFrm = null;

        public CefCustomObject(CrBrowser crBrowser, FormBase mainForm)
        {
            this.crBrowser = crBrowser;
            _instanceBrowser = crBrowser.GetBrowser();
            _instanceMainForm = mainForm;
        }

        public void ShowDevTools()
        {
            _instanceMainForm.BeginInvoke(new Action(() =>
            {
                Form frm = new CSFrame.Ui.DevPwd(crBrowser);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }));
        }

        /// <summary>
        /// 退出程序：给js调用
        /// </summary>
        public void Close()
        {
            _instanceMainForm.BeginInvoke(new Action(() =>
            {
                //Application.Exit();
                _instanceMainForm.Close();
            }));
        }
        public void Exit()
        {
            _instanceMainForm.BeginInvoke(new Action(() =>
            {
                Application.Exit();
            }));
        }

        /// <summary>
        /// 最小化程序：给js调用
        /// </summary>
        public void Min()
        {
            _instanceMainForm.BeginInvoke(new Action(() =>
            {
                _instanceMainForm.WindowState = FormWindowState.Minimized;
            }));
        }

        /// <summary>
        /// 最大化程序：给js调用
        /// </summary>
        public void Max()
        {
            _instanceMainForm.BeginInvoke(new Action(() =>
            {
                _instanceMainForm.WindowState = FormWindowState.Maximized;
            }));
        }

        /// <summary>
        /// 正常窗口大小：给js调用
        /// </summary>
        public void Normal()
        {
            _instanceMainForm.BeginInvoke(new Action(() =>
            {
                _instanceMainForm.WindowState = FormWindowState.Normal;
                _instanceMainForm.Location = new System.Drawing.Point(200, 80);

            }));
        }

        /// <summary>
        /// 注册事件处理方法：给js调用
        /// </summary>
        /// <param name="type"></param>
        /// <param name="script"></param>
        public void RegistHandler(string type, string script)
        {

            if (_instanceMainForm.eventHandlers.ContainsKey(type))
            {
                _instanceMainForm.eventHandlers.Remove(type);
            }
            _instanceMainForm.eventHandlers.Add(type, script);
        }

        public void OpenMain(string url)
        {
            _instanceMainForm.BeginInvoke(new MethodInvoker(() =>
            {
                _instanceMainForm.Hide();
                mainFrm = new Form1();
                mainFrm.Show();
            }));
        }

        public void RememberPwd(string phone = "-123", string password = "-123", string islogin = "0")
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".tmp", "uio415145sd54.db");
            File.WriteAllLines(path, new string[] { phone, password, islogin });
        }
        public string GetDefaultUser()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".tmp");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, "uio415145sd54.db");
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            string lines = Newtonsoft.Json.JsonConvert.SerializeObject(File.ReadAllLines(path));
            return lines;
        }

        public void OpenQQ(string qq)
        {
            System.Diagnostics.Process.Start(string.Format("tencent://message/?uin={0}", qq));
        }

        public void LoginOut()
        {
            //处理自动登录文件
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".tmp", "uio415145sd54.db");
            if (File.Exists(path)) File.Delete(path);
            _instanceMainForm.BeginInvoke(new MethodInvoker(() =>
            {
                try
                {
                    _instanceMainForm.Close();
                    mainFrm.Close();
                    mainFrm.Dispose();
                }
                catch { }
                finally
                {
                    mainFrm = null;
                }
                login.Reload();
                login.Show();
            }));
        }

        public void RefreshPage()
        {
            IFrame frame = crBrowser.GetBrowser().GetMainFrame();
            frame.LoadUrl(frame.Url);
        }

        public void OpenCommonFrm(int width, int height, string url, string title)
        {
            //打开通用窗体
            _instanceMainForm.BeginInvoke(new MethodInvoker(() =>
            {
                new CSFrame.CommonFrm(width, height, url, title).ShowDialog();
            }));
        }

    }
}