using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using WatsEMIAnalyzer.Bindings;

namespace WatsEMIAnalyzer
{
    public partial class AssignUserForm : Form
    {
        private string mPrompt;
        private bool mOKInvoked = false;
        private List<string> mUsers = new List<string>();

        public AssignUserForm()
        {
            InitializeComponent();
        }

        public string Prompt
        {
            set { mPrompt = value; }
        }

        public List<string> Users
        {
            get { return mUsers; }
        }

        private void AssignUserForm_Load(object sender, EventArgs e)
        {
            PromptLabel.Text = mPrompt;
            User user;
            foreach (string userId in DataCenter.Instance().Users.Keys)
            {
                user = DataCenter.Instance().Users[userId];
                if (user.role == "normal" && !user.locked)
                    UsersList.Items.Add(userId);
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            mOKInvoked = true;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            mOKInvoked = false;
        }

        private void AssignUserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mOKInvoked)
            {
                if (UsersList.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Not select any user !");
                    UsersList.Focus();
                    e.Cancel = true;
                    return;
                }

                foreach (object o in UsersList.SelectedItems)
                {
                    mUsers.Add(o.ToString());
                }
            }
        }

        private void UsersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            OKButton.Enabled = (UsersList.SelectedItems.Count > 0);
        }

        
    }
}
