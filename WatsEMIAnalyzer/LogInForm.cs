using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using WatsEMIAnalyzer.HTTP;
using WatsEMIAnalyzer.Model;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using WatsEMIAnalyzer.Bindings;

namespace WatsEMIAnalyzer
{
    public partial class LogInForm : Form
    {
        private string mServerHost;
        public LogInForm()
        {
            InitializeComponent();

            mServerHost = Program.Config.ReadString("Server", "ServerHost", "localhost:8080");
            string userName = Program.Config.ReadString("Client", "UserName", "");
            
            UserNameEditor.Text = userName;
        }

        private void ConnectivityButton_Click(object sender, EventArgs e)
        {
            ConnectivityForm connectivityForm = new ConnectivityForm();
            DialogResult result = connectivityForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                mServerHost = connectivityForm.ServerHost;
                Program.Config.WriteString("Server", "ServerHost", connectivityForm.ServerHost);
                Program.Config.WriteString("Server", "Proxy", connectivityForm.Proxy);
                Program.Config.WriteInt("Server", "UseProxy", connectivityForm.UseProxy ? 1 : 0);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            UserNameEditor.Text = UserNameEditor.Text.Trim();
            if (UserNameEditor.Text.Length == 0)
            {
                MessageBox.Show("Please input user name !");
                UserNameEditor.Focus();
                return;
            }

            PasswordEditor.Text = PasswordEditor.Text.Trim();
            if (PasswordEditor.Text.Length == 0)
            {
                MessageBox.Show("Please input password !");
                PasswordEditor.Focus();
                return;
            }

            Program.Config.WriteString("Client", "UserName", UserNameEditor.Text);

            string ha1 = UserNameEditor.Text + ":" + Constants.REALM + ":" + PasswordEditor.Text;
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] s = md5.ComputeHash(UnicodeEncoding.UTF8.GetBytes(ha1)); 
            ha1 = BitConverter.ToString(s);
            ha1 = ha1.Replace("-", string.Empty).ToLower();
            Program.Config.WriteString("Client", "HA1", ha1);

            HTTPAgent.ServerHost = mServerHost;
            HTTPAgent.Username = UserNameEditor.Text;
            HTTPAgent.Password = PasswordEditor.Text;
            if (Program.Config.ReadInt("Server", "UseProxy", 0) == 1)
            {
                HTTPAgent.Proxy = Program.Config.ReadString("Server", "Proxy", "");
            }
            else
            {
                HTTPAgent.Proxy = null;
            }

            LogInButton.Enabled = false;

            HTTPAgent.instance().onLoginSuccessfully += new HTTPAgent.loginSuccessfully(LogInForm_onLoginSuccessfully);
            HTTPAgent.instance().onLoginFailed += new HTTPAgent.loginFailed(LogInForm_onLoginFailed);

            HTTPAgent.instance().logIn(this);
        }

        private void LogInForm_onLoginSuccessfully(User self)
        {
            HTTPAgent.instance().onLoginSuccessfully -= new HTTPAgent.loginSuccessfully(LogInForm_onLoginSuccessfully);
            HTTPAgent.instance().onLoginFailed -= new HTTPAgent.loginFailed(LogInForm_onLoginFailed);

            if (self.locked)
            {
                MessageBox.Show("User '" + self + "' was locked, please contact administrator !");
                LogInButton.Enabled = true;
                return;
            }

            LogInButton.Enabled = true;
            Hide();
            LoadDataForm loadDataForm = new LoadDataForm(this);
            loadDataForm.Show();
        }

        private void LogInForm_onLoginFailed(string reason)
        {
            HTTPAgent.instance().onLoginSuccessfully -= new HTTPAgent.loginSuccessfully(LogInForm_onLoginSuccessfully);
            HTTPAgent.instance().onLoginFailed -= new HTTPAgent.loginFailed(LogInForm_onLoginFailed);
            MessageBox.Show("Login Failed, " + reason);
            LogInButton.Enabled = true;
        }

        private void LogInForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //HTTPAgent.instance().onLoginSuccessfully -= new HTTPAgent.loginSuccessfully(LogInForm_onLoginSuccessfully);
            //HTTPAgent.instance().onLoginFailed -= new HTTPAgent.loginFailed(LogInForm_onLoginFailed);
        }

        private void LogInForm_VisibleChanged(object sender, EventArgs e)
        {
            
        }

        private void LogInForm_Load(object sender, EventArgs e)
        {
            string appPath = WatsEmiReportTool.Utility.GetAppPath() + "\\Temp";
            if (!Directory.Exists(appPath))
                Directory.CreateDirectory(appPath);
        }
    }
}
