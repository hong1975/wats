using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WatsEMIAnalyzer
{
    public partial class ConnectivityForm : Form
    {
        private bool mOKInvoked = false;

        public string ServerHost
        {
            get { return ServerHostEditor.Text; }
        }

        public bool UseProxy
        {
            get { return HttpProxyCheckBox.Checked; }
        }

        public string Proxy
        {
            get { return HttpProxyEditor.Text; }
        }


        public ConnectivityForm()
        {
            InitializeComponent();

            string serverHost = Program.Config.ReadString("Server", "ServerHost", "localhost:8080");
            ServerHostEditor.Text = serverHost.Trim();

            int useProxy = Program.Config.ReadInt("Server", "UseProxy", 0);
            HttpProxyCheckBox.Checked = (useProxy == 1);
            HttpProxyEditor.Enabled = (useProxy == 1);

            string proxy = Program.Config.ReadString("Server", "Proxy", "");
            HttpProxyEditor.Text = proxy.Trim();
        }

        private void HttpProxyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            HttpProxyEditor.Enabled = HttpProxyCheckBox.Checked;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            mOKInvoked = true;
        }

        private void ConnectivityForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mOKInvoked)
            {
                if (ServerHostEditor.Text.Length == 0)
                {
                    MessageBox.Show("Please input server host !");
                    ServerHostEditor.Focus();
                    e.Cancel = true;
                    return;
                }

                if (HttpProxyCheckBox.Checked)
                {
                    HttpProxyEditor.Text = HttpProxyEditor.Text.Trim();
                    if (HttpProxyEditor.Text.Length == 0)
                    {
                        MessageBox.Show("Please input http proxy !");
                        HttpProxyEditor.Focus();
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }
    }
}
