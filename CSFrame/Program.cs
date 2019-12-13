using CefSharp;
using CefSharp.WinForms;
using CSFrame.Browser.Handler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSFrame
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region 必须以管理员身份启动
            //获得当前登录的Windows用户标示 
            if (!new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent()).IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                //创建启动对象 
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                //设置运行文件 
                startInfo.FileName = System.Windows.Forms.Application.ExecutablePath;
                //设置启动动作,确保以管理员身份运行 
                startInfo.Verb = "runas";
                try
                {
                    //如果不是管理员，则启动UAC 
                    System.Diagnostics.Process.Start(startInfo);
                }
                catch { }
                //退出 
                System.Windows.Forms.Application.Exit();
                return;
            }
            #endregion
            #region 程序只能运行一次  
            int processCount = 0;
            Process[] pa = Process.GetProcesses();//获取当前进程数组。
            foreach (Process PTest in pa)
            {
                if (PTest.ProcessName == Process.GetCurrentProcess().ProcessName)
                {
                    processCount += 1;
                }
            }
            if (processCount > 1)
            {
                //如果程序已经运行，则给出提示。并退出本进程。
                DialogResult dr;
                dr = MessageBox.Show(Process.GetCurrentProcess().ProcessName + "程序已经在运行！", "退出程序", MessageBoxButtons.OK, MessageBoxIcon.Error);//可能你不需要弹出窗口，在这里可以屏蔽掉
                return; //Exit;
            }
            #endregion
            InitCef();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }

        /// <summary>
        /// 初始化 CEF
        /// 忽略https证书的问题,跨域问题
        /// </summary>
        private static void InitCef()
        {
            var settings = new CefSettings();
            settings.CefCommandLineArgs.Add("--ignore-urlfetcher-cert-requests", "1");
            settings.CefCommandLineArgs.Add("--ignore-certificate-errors", "1");
            settings.CefCommandLineArgs.Add("--disable-web-security", "1");
            settings.RegisterScheme(new CefCustomScheme()
            {
                SchemeName = ResourceSchemeHandlerFactory.SchemeName,
                SchemeHandlerFactory = new ResourceSchemeHandlerFactory()
            });
            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);
        }
    }
}
