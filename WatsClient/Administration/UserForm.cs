using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WatsClient.Administration
{
    public partial class UserForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private static UserForm mUserForm;
        private TreeNode mLicenseRootNode;

        public static UserForm Instance
        {
            get 
            {
                if (mUserForm == null)
                    mUserForm = new UserForm();

                return mUserForm;
            }
        
        }

        private UserForm()
        {
            InitializeComponent();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            mLicenseRootNode = LicenseTreeView.Nodes.Add("License Assignment");
        }

        private void AddUserButton_Click(object sender, EventArgs e)
        {
            UserDetailForm userDetailForm = new UserDetailForm();
            userDetailForm.ShowDialog();
        }
    }
}
