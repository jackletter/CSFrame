using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.WinForms;
using System.Windows.Forms;

namespace CSFrame.Ui
{
    public partial class DevPwd : Form
    {
        private CSFrame.Browser.CrBrowser crBrower = null;
        public DevPwd(CSFrame.Browser.CrBrowser crBrower)
        {
            this.crBrower = crBrower;
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            showDev();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                showDev();
            }
        }
        private void showDev()
        {
            if (textBox1.Text == "789456123")
            {
                crBrower.GetBrowser().ShowDevTools();
                this.Close();
                this.Dispose();
            }
        }
    }
}
