using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsClient.REST;
using System.Net;

namespace WatsClient
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (UsernameEditor.Text.Trim().Length == 0)
            {
                MessageBox.Show("User name is empty !");
                UsernameEditor.Focus();
                return;
            }
            
            if (PasswordEditor.Text.Length == 0)
            {
                MessageBox.Show("Password is empty !");
                PasswordEditor.Focus();
                return;
            }

            Client.AuthUser = UsernameEditor.Text.Trim();
            Client.AuthPass = PasswordEditor.Text;
            Client.BaseUrl = "http://localhost:8080/emi";

            Client.OnLoginResult += new Client.LoginResult(Client_OnLoginResult);
            Client.LogIn();
            EnableLogin(false);
        }

        void Client_OnLoginResult(System.Net.HttpStatusCode statusCode, WatsClient.Bindings.User self)
        {
            Client.OnLoginResult -= new Client.LoginResult(Client_OnLoginResult);
            this.Invoke((MethodInvoker)delegate()
            {
                EnableLogin(true);
                if (statusCode == HttpStatusCode.OK)
                {
                    if (self.Locked)
                    {
                        MessageBox.Show("User '" + self + "' was locked, please contact administrator !");
                    }
                    else
                    {
                        this.Hide();
                        DataCenter.MySelf = self;
                        MainForm mainForm = new MainForm();
                        mainForm.Show();
                    }
                }
                else
                {
                    if (statusCode == HttpStatusCode.Unauthorized)
                    {
                        MessageBox.Show("Incorrect user or password !");
                    }
                    else
                    {
                        MessageBox.Show("Login failed, status = " + statusCode.ToString());
                    }
                }
                
            });
        }

        private void EnableLogin(bool enabled)
        {
            UsernameEditor.Enabled = enabled;
            PasswordEditor.Enabled = enabled;
            LoginButton.Enabled = enabled;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        
    }
}
