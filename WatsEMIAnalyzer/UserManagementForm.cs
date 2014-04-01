using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEMIAnalyzer.Bindings;
using WatsEMIAnalyzer.HTTP;

namespace WatsEMIAnalyzer
{
    public partial class UserManagementForm : Form
    {
        public UserManagementForm()
        {
            InitializeComponent();
        }

        private void UserManagementForm_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, User> pair in DataCenter.Instance().Users)
            {
                UserList.Items.Add(pair.Key);
            }

            HTTPAgent.instance().onAddUserSuccessfully += new HTTPAgent.addUserSuccessfully(UserManagementForm_onAddUserSuccessfully);
            HTTPAgent.instance().onAddUserFailed += new HTTPAgent.addUserFailed(UserManagementForm_onAddUserFailed);
            HTTPAgent.instance().onRemoveUserSuccessfully += new HTTPAgent.removeUserSuccessfully(UserManagementForm_onRemoveUserSuccessfully);
            HTTPAgent.instance().onRemoveUserFailed += new HTTPAgent.removeUserFailed(UserManagementForm_onRemoveUserFailed);
        }

        void UserManagementForm_onAddUserSuccessfully(User user)
        {
            MessageBox.Show("User '" + user.userId + "' <role: " + user.role + "> was created !");
            DataCenter.Instance().Users[user.userId] = user;
            UserList.Items.Add(user.userId);
        }

        void UserManagementForm_onAddUserFailed(string user, System.Net.HttpStatusCode statusCode)
        {
            MessageBox.Show("Add user '" + user + "' failed, status code = " + statusCode.ToString());
        }

        void UserManagementForm_onRemoveUserFailed(string user, System.Net.HttpStatusCode statusCode)
        {
            MessageBox.Show("Remove user '" + user + "' failed, status code = " + statusCode.ToString());
        }

        void UserManagementForm_onRemoveUserSuccessfully(string user)
        {
            DataCenter.Instance().Users.Remove(user);
            UserList.Items.Remove(user);
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            UserDetailForm userDetailForm = new UserDetailForm(null);
            if (DialogResult.OK != userDetailForm.ShowDialog())
                return;

            HTTPAgent.instance().addUser(this, userDetailForm.User.userId, userDetailForm.User.role);
        }

        private void UserManagementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            HTTPAgent.instance().onAddUserSuccessfully -= new HTTPAgent.addUserSuccessfully(UserManagementForm_onAddUserSuccessfully);
            HTTPAgent.instance().onAddUserFailed += new HTTPAgent.addUserFailed(UserManagementForm_onAddUserFailed);
            HTTPAgent.instance().onRemoveUserSuccessfully -= new HTTPAgent.removeUserSuccessfully(UserManagementForm_onRemoveUserSuccessfully);
            HTTPAgent.instance().onRemoveUserFailed -= new HTTPAgent.removeUserFailed(UserManagementForm_onRemoveUserFailed);
        }

        private void UserList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DetailButton.Enabled = RemoveButton.Enabled = (UserList.SelectedIndex >= 0);
        }

        private void DetailButton_Click(object sender, EventArgs e)
        {
            User user;
            if (DataCenter.Instance().Users.ContainsKey(UserList.SelectedItem.ToString()))
                user = DataCenter.Instance().Users[UserList.SelectedItem.ToString()];
            else
                return;

            UserDetailForm userDetailForm = new UserDetailForm(user);
            if (DialogResult.OK != userDetailForm.ShowDialog())
                return;

            
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            string userId = UserList.SelectedItem.ToString();
            if (DialogResult.Yes == MessageBox.Show("Remove user '" + userId + "', are you sure ?", "Warning", MessageBoxButtons.YesNo))
            {
                HTTPAgent.instance().removeUser(this, userId);
            }
        }
    }
}
