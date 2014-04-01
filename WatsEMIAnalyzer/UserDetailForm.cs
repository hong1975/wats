using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEMIAnalyzer.Bindings;
using System.Text.RegularExpressions;
using WatsEMIAnalyzer.HTTP;

namespace WatsEMIAnalyzer
{
    public partial class UserDetailForm : Form
    {
        private bool mOKInvoked;
        private User mUser;
        private bool mIsNew;
        private bool mAllowUpdate = true;

        public User User
        {
            get { return mUser; }
        }

        public bool IsNew
        {
            get { return mIsNew; }
        }

        public bool AllowUpdate
        {
            get { return mAllowUpdate;}
            set { mAllowUpdate = value;}
        }
        
        public UserDetailForm(User user)
        {
            InitializeComponent();
            mUser = user;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            mOKInvoked = false;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            mOKInvoked = true;
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            if (mUser == null)
            {
                mIsNew = true;
                mUser = new User();
                this.Text = "Create new user";
                OKButton.Text = "Create";
                RoleCombox.SelectedIndex = 0;
                LockCheckBox.Enabled = false;
            }
            else
            {
                mIsNew = false;
                this.Text = "User - " + mUser.userId;
                OKButton.Text = "Update";
                if ("admin".Equals(mUser.role))
                    RoleCombox.SelectedIndex = 1;
                else
                    RoleCombox.SelectedIndex = 0;
                
                UserIdEditor.Text = mUser.userId;
                UserIdEditor.ReadOnly = true;

                LockCheckBox.Checked = mUser.locked;

                CloseButton.Visible = RoleCombox.Enabled = LockCheckBox.Enabled = mAllowUpdate;
                if (!mAllowUpdate)
                    OKButton.Text = "OK";
            }
        }

        private void UserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!mAllowUpdate)
                return;

            if (!mOKInvoked)
                return;

            mOKInvoked = false;

            UserIdEditor.Text = UserIdEditor.Text.Trim();
            if (!Regex.IsMatch(UserIdEditor.Text, @"[a-zA-Z0-9\._\-\s]+"))
            {
                MessageBox.Show("User id is invalid !");
                UserIdEditor.Focus();
                e.Cancel = true;
                return;
            }

            mUser.userId = UserIdEditor.Text;
            mUser.role = RoleCombox.SelectedItem.ToString();
            mUser.locked = LockCheckBox.Checked;
        }

        private void UserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
