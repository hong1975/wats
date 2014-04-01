using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEMIAnalyzer.Model;
using WatsEMIAnalyzer.Bindings;
using WatsEMIAnalyzer.HTTP;
using System.Diagnostics;
using System.Net;

namespace WatsEMIAnalyzer
{
    public partial class AdminForm : Form
    {
        private LogInForm mLogInForm;
        private TreeNode mGlobalNode;

        private const int PIC_GLOBAL   = 0;
        private const int PIC_MANAGER  = 1;
        private const int PIC_REGION   = 2;
        private const int PIC_MANAGERS = 3;

        public AdminForm(LogInForm logInForm)
        {
            mLogInForm = logInForm;
            InitializeComponent();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            HTTPAgent.instance().onUpdateRegionSuccessfully += new HTTPAgent.updateRegionSuccessfully(AdminForm_onUpdateRegionSuccessfully);
            HTTPAgent.instance().onUpdateRegionFailed += new HTTPAgent.updateRegionFailed(AdminForm_onUpdateRegionFailed);

            HTTPAgent.instance().onAddUserSuccessfully +=new HTTPAgent.addUserSuccessfully(AdminForm_onAddUserSuccessfully);
            HTTPAgent.instance().onAddUserFailed+=new HTTPAgent.addUserFailed(AdminForm_onAddUserFailed);

            HTTPAgent.instance().onRemoveUserSuccessfully+=new HTTPAgent.removeUserSuccessfully(AdminForm_onRemoveUserSuccessfully);
            HTTPAgent.instance().onRemoveUserFailed += new HTTPAgent.removeUserFailed(AdminForm_onRemoveUserFailed);

            HTTPAgent.instance().onUpdateUserSuccessfully += new HTTPAgent.updateUserSuccessfully(AdminForm_onUpdateUserSuccessfully);
            HTTPAgent.instance().onUpdateUserFailed += new HTTPAgent.updateUserFailed(AdminForm_onUpdateUserFailed);

            /************************************************************************/
            /* Load Region                                                          */
            /************************************************************************/
            RegionTreeView.ImageList = ImageList;
            mGlobalNode = RegionTreeView.Nodes.Add("Global");
            mGlobalNode.SelectedImageIndex = mGlobalNode.ImageIndex = PIC_GLOBAL;
            mGlobalNode.Tag = DataCenter.Instance().GlobalRegion.Root;

            if (DataCenter.Instance().GlobalRegion.Root.Sub != null)
            {
                TreeNode regionNode;
                TreeNode managersNode;
                TreeNode managerNode;
                foreach (SubRegion region in DataCenter.Instance().GlobalRegion.Root.Sub)
                {
                    regionNode = mGlobalNode.Nodes.Add(region.Name);
                    regionNode.SelectedImageIndex = regionNode.ImageIndex = PIC_REGION;
                    regionNode.Tag = region;

                    managersNode = regionNode.Nodes.Add("Managers");
                    UsersModel usersModel = new UsersModel();
                    managersNode.Tag = usersModel;
                    managersNode.SelectedImageIndex = managersNode.ImageIndex = PIC_MANAGERS;

                    if (region.Manager == null)
                        continue;

                    Debug.WriteLine("Region=" + region.Name);
                    foreach (string manager in region.Manager)
                    {
                        Debug.WriteLine("Manager=" + manager);
                        managerNode = managersNode.Nodes.Add(manager);
                        managerNode.SelectedImageIndex = managerNode.ImageIndex = PIC_MANAGER;
                        usersModel.Add(manager);
                    }
                }
            }
            
            /************************************************************************/
            /* Load Users                                                           */
            /************************************************************************/
            foreach (KeyValuePair<string, User> pair in DataCenter.Instance().Users)
            {
                if (!pair.Value.locked)
                {
                    ActiveUsersListBox.Items.Add(pair.Key);
                }
                else
                {
                    LockedUsersListBox.Items.Add(pair.Key);
                }
            }
        }

        private void AdminForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            HTTPAgent.instance().onUpdateRegionSuccessfully -= new HTTPAgent.updateRegionSuccessfully(AdminForm_onUpdateRegionSuccessfully);
            HTTPAgent.instance().onUpdateRegionFailed -= new HTTPAgent.updateRegionFailed(AdminForm_onUpdateRegionFailed);

            HTTPAgent.instance().onAddUserSuccessfully -= new HTTPAgent.addUserSuccessfully(AdminForm_onAddUserSuccessfully);
            HTTPAgent.instance().onAddUserFailed -= new HTTPAgent.addUserFailed(AdminForm_onAddUserFailed);

            HTTPAgent.instance().onRemoveUserSuccessfully -= new HTTPAgent.removeUserSuccessfully(AdminForm_onRemoveUserSuccessfully);
            HTTPAgent.instance().onRemoveUserFailed -= new HTTPAgent.removeUserFailed(AdminForm_onRemoveUserFailed);

            HTTPAgent.instance().onUpdateUserSuccessfully -= new HTTPAgent.updateUserSuccessfully(AdminForm_onUpdateUserSuccessfully);
            HTTPAgent.instance().onUpdateUserFailed -= new HTTPAgent.updateUserFailed(AdminForm_onUpdateUserFailed);

            mLogInForm.Show();
        }

        private void LockedUsersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemoveLockedUserButton.Enabled
                = UnLockButton.Enabled
                = (LockedUsersListBox.SelectedItems.Count > 0);

        }

        private void ActiveUsersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemoveActiveUserButton.Enabled
                = LockButton.Enabled
                = (ActiveUsersListBox.SelectedItems.Count > 0);
        }

        void AdminForm_onUpdateRegionFailed(UpdateRegionRequest request, HttpStatusCode statusCode, string messageText)
        {
            MessageBox.Show(messageText + " failed, status=" + statusCode.ToString());
        }

        void AdminForm_onUpdateRegionSuccessfully(UpdateRegionResult result, object context)
        {
            DataCenter.Instance().GlobalRegion.Version = result.NewVer;
            if (UpdateRegionType.AddRegion.ToString().Equals(result.Type))
            {
                if (DataCenter.Instance().GlobalRegion.Root.Sub == null)
                    DataCenter.Instance().GlobalRegion.Root.Sub = new List<SubRegion>();

                DataCenter.Instance().GlobalRegion.Root.Sub.Add(result.Region);
                TreeNode regionNode = mGlobalNode.Nodes.Add(result.Region.Name);
                regionNode.SelectedImageIndex = regionNode.ImageIndex = PIC_REGION;
                regionNode.Tag = result.Region;

                TreeNode managersNode = regionNode.Nodes.Add("Managers");
                managersNode.Tag = new UsersModel();
                managersNode.SelectedImageIndex = managersNode.ImageIndex = PIC_MANAGERS;

                RegionTreeView.SelectedNode = regionNode;
                regionNode.ExpandAll();
            }
            else if (UpdateRegionType.RenameRegion.ToString().Equals(result.Type))
            {
                TreeNode regionNode = (TreeNode)context;
                regionNode.Text = result.Region.Name;
                ((SubRegion)regionNode.Tag).Name = result.Region.Name;
            }
            else if (UpdateRegionType.RemoveRegion.ToString().Equals(result.Type))
            {
                TreeNode regionNode = (TreeNode)context;
                SubRegion removedRegion = (SubRegion)regionNode.Tag;
                ((SubRegion)regionNode.Parent.Tag).Sub.Remove(removedRegion);
                regionNode.Remove();
            }
            else if (UpdateRegionType.AddManager.ToString().Equals(result.Type))
            {
                TreeNode managersNode = (TreeNode)context;
                SubRegion region = (SubRegion)managersNode.Parent.Tag;
                TreeNode managerNode;
                foreach (string manager in result.Region.Manager)
                {
                    managerNode = managersNode.Nodes.Add(manager);
                    managerNode.SelectedImageIndex = managerNode.ImageIndex = PIC_MANAGER;

                    ((UsersModel)managersNode.Tag).Add(manager);
                    region.Manager.Add(manager);
                }
            }
            else if (UpdateRegionType.RemoveManager.ToString().Equals(result.Type))
            {
                TreeNode managerNode = (TreeNode)context;
                TreeNode managersNode = managerNode.Parent;
                SubRegion region = (SubRegion)managersNode.Parent.Tag;

                string removedManager = result.Region.Manager[0];
                ((UsersModel)managersNode.Tag).Remove(removedManager);
                region.Manager.Remove(removedManager);
                managerNode.Remove();
            }
        }

        void AdminForm_onUpdateUserFailed(string user, UpdateUserType type, System.Net.HttpStatusCode statusCode)
        {
            MessageBox.Show("Can't " + type.ToString() + " for user '" + user + "', status=" + statusCode + " !");
        }

        void AdminForm_onUpdateUserSuccessfully(string user, UpdateUserType type, string newValue)
        {
            switch(type)
            {
                case UpdateUserType.ChangePassword:
                    {

                    }
                    break;

                case UpdateUserType.ChangeRole:
                    {

                    }
                    break;

                case UpdateUserType.LockUser:
                    {
                        if ("yes".Equals(newValue, StringComparison.OrdinalIgnoreCase) 
                            || "1".Equals(newValue, StringComparison.OrdinalIgnoreCase)
                            || "true".Equals(newValue,StringComparison.OrdinalIgnoreCase))
                        {
                            ActiveUsersListBox.Items.Remove(user);
                            LockedUsersListBox.Items.Add(user);
                            DataCenter.Instance().Users[user].locked = true;
                        }
                        else
                        {
                            LockedUsersListBox.Items.Remove(user);
                            ActiveUsersListBox.Items.Add(user);
                            DataCenter.Instance().Users[user].locked = false;
                        }
                    }
                    break;
            }
        }

        void AdminForm_onRemoveUserFailed(string user, System.Net.HttpStatusCode statusCode)
        {
            MessageBox.Show("Remove user '" + user + "' failed, status code = " + statusCode.ToString());
        }

        void AdminForm_onRemoveUserSuccessfully(string user)
        {
            DataCenter.Instance().Users.Remove(user);
            if (ActiveUsersListBox.Items.IndexOf(user) >= 0)
                ActiveUsersListBox.Items.Remove(user);
            if (LockedUsersListBox.Items.IndexOf(user) >= 0)
                LockedUsersListBox.Items.Remove(user);
        }

        void AdminForm_onAddUserFailed(string user, System.Net.HttpStatusCode statusCode)
        {
            MessageBox.Show("Add user '" + user + "' failed, status=" + statusCode.ToString());
        }

        void AdminForm_onAddUserSuccessfully(User user)
        {
            DataCenter.Instance().Users.Add(user.userId, user);
            ActiveUsersListBox.Items.Add(user.userId);
        }

        private void AddReagionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regionName = "";
            if (DialogResult.Cancel == InputDialog.Show("Add region", 
                "Input region name", 
                ref regionName,
                new CheckInputDelegate(CheckRegionName)))
                return;

            UpdateRegionRequest request = new UpdateRegionRequest();
            request.Type = UpdateRegionType.AddRegion.ToString();
            request.Region = new SubRegion();
            request.Region.ParentID = ((SubRegion)RegionTreeView.SelectedNode.Tag).ID;
            request.Region.Name = regionName;

            HTTPAgent.instance().updateRegion(this, DataCenter.Instance().GlobalRegion.Version, request, "Add region '" + regionName + "'", null);
        }

        private bool CheckRegionName(string regionName)
        {
            foreach (TreeNode regionNode in mGlobalNode.Nodes)
            {
                if (regionName.Equals(regionNode.Text, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Region " + regionName + " already exists !");
                    return false;
                }
            }

            return true;
        }

        private void RegionTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            RegionTreeView.SelectedNode = RegionTreeView.GetNodeAt(e.X, e.Y);
            if (RegionTreeView.GetNodeAt(e.X, e.Y) == null)
            {
                return;
            }

            if (RegionTreeView.SelectedNode.Level == 0) //global
            {
                GlobalContextMenu.Show(Cursor.Position);
            }
            else if (RegionTreeView.SelectedNode.Level == 1) //region
            {
                RegionContextMenu.Show(Cursor.Position);
            }
            else if (RegionTreeView.SelectedNode.Level == 2) 
            {
                if (RegionTreeView.SelectedNode.Index == 0) //managers
                    ManagersContextMenu.Show(Cursor.Position);
            }
            else if (RegionTreeView.SelectedNode.Level == 3)
            {
                if (RegionTreeView.SelectedNode.Parent.Index == 0) //manager
                    ManagerContextMenu.Show(Cursor.Position);
            }
        }

        private void RemoveRegionToolMenuItem_Click(object sender, EventArgs e)
        {
            string prompt = "Remove region '" + RegionTreeView.SelectedNode.Text + "', are you sure ?";
            if (DialogResult.No == MessageBox.Show(prompt, "Warning", MessageBoxButtons.YesNo))
                return;

            UpdateRegionRequest request = new UpdateRegionRequest();
            request.Type = UpdateRegionType.RemoveRegion.ToString();
            request.Region = new SubRegion();
            request.Region.ParentID = ((SubRegion)RegionTreeView.SelectedNode.Parent.Tag).ID;
            request.Region.ID = ((SubRegion)RegionTreeView.SelectedNode.Tag).ID;

            HTTPAgent.instance().updateRegion(this, DataCenter.Instance().GlobalRegion.Version, request,
                "Remove region '" + RegionTreeView.SelectedNode.Text + "'", RegionTreeView.SelectedNode);
        }

        private void RenameRegionToolMenuItem_Click(object sender, EventArgs e)
        {
            string oldRegionName = RegionTreeView.SelectedNode.Text;
            RenameForm renameForm = new RenameForm("Rename region", oldRegionName);
            renameForm.CheckInputDelegate = new CheckInputDelegate(CheckRegionName);
            if (DialogResult.OK == renameForm.ShowDialog() && renameForm.Updated)
            {
                UpdateRegionRequest request = new UpdateRegionRequest();
                request.Type = UpdateRegionType.RenameRegion.ToString();
                request.Region = new SubRegion();
                request.Region.Name = renameForm.NewName;
                request.Region.ID = ((SubRegion)RegionTreeView.SelectedNode.Tag).ID;

                HTTPAgent.instance().updateRegion(this, DataCenter.Instance().GlobalRegion.Version, request,
                    "Rename region '" + oldRegionName + "'", RegionTreeView.SelectedNode);
            }
        }

        private void AssignManagersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AssignUserForm assignUserForm = new AssignUserForm();
            assignUserForm.Prompt = "Add task manager";
            TreeNode managersNode = null;
            if (DialogResult.OK == assignUserForm.ShowDialog())
            {
                managersNode = RegionTreeView.SelectedNode;
                UpdateRegionRequest request = new UpdateRegionRequest();
                request.Type = UpdateRegionType.AddManager.ToString();
                request.Region = new SubRegion();
                request.Region.ID = ((SubRegion)managersNode.Parent.Tag).ID;

                foreach (string user in assignUserForm.Users)
                {
                    if (((UsersModel)managersNode.Tag).Find(user))
                        continue;

                    request.Region.Manager.Add(user);
                }

                if (request.Region.Manager.Count == 0)
                    return;

                HTTPAgent.instance().updateRegion(this, DataCenter.Instance().GlobalRegion.Version, request,
                    "Add managers", managersNode);
            }
        }

        private void RemoveManagerToolMenuItem_Click(object sender, EventArgs e)
        {
            string prompt = "Remove manager '" + RegionTreeView.SelectedNode.Text + "', are you sure ?";
            if (DialogResult.No == MessageBox.Show(prompt, "Warning", MessageBoxButtons.YesNo))
                return;

            UpdateRegionRequest request = new UpdateRegionRequest();
            request.Type = UpdateRegionType.RemoveManager.ToString();
            request.Region = new SubRegion();
            request.Region.ID = ((SubRegion)RegionTreeView.SelectedNode.Parent.Parent.Tag).ID;

            TreeNode managerNode = RegionTreeView.SelectedNode;
            TreeNode managersNode = managerNode.Parent;
            string manager = managerNode.Text;
            if (!((UsersModel)managersNode.Tag).Find(manager))
                return;

            request.Region.Manager.Add(manager);
            HTTPAgent.instance().updateRegion(this, DataCenter.Instance().GlobalRegion.Version, request,
                "Remove manager '" + manager + "'", managerNode);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            UserDetailForm userDetailForm = new UserDetailForm(null);
            if (DialogResult.OK != userDetailForm.ShowDialog())
                return;

            HTTPAgent.instance().addUser(this, userDetailForm.User.userId, userDetailForm.User.role);
        }

        private void RemoveActiveUserButton_Click(object sender, EventArgs e)
        {
            string userId = ActiveUsersListBox.SelectedItem.ToString();
            RemoveUser(userId);            
        }

        private void RemoveLockedUserButton_Click(object sender, EventArgs e)
        {
            string userId = LockedUsersListBox.SelectedItem.ToString();
            RemoveUser(userId);
        }

        private void RemoveUser(string userId)
        {
            if (DataCenter.Instance().Users.ContainsKey(userId))
            {
                User user = DataCenter.Instance().Users[userId];
                if ("admin".Equals(user.role))
                {
                    MessageBox.Show("Admin user can't be removed !");
                    return;
                }
            }
            else
            {
                MessageBox.Show("User does not exist !");
                return;
            }

            if (DialogResult.Yes == MessageBox.Show("Remove user '" + userId + "', are you sure ?", "Warning", MessageBoxButtons.YesNo))
            {
                HTTPAgent.instance().removeUser(this, userId);
            }
        }

        private void LockButton_Click(object sender, EventArgs e)
        {
            string userId = ActiveUsersListBox.SelectedItem.ToString();
            if (DataCenter.Instance().Users.ContainsKey(userId))
            {
                User user = DataCenter.Instance().Users[userId];
                if ("admin".Equals(user.role))
                {
                    MessageBox.Show("Admin user can't be locked !");
                    return;
                }
            }
            else
            {
                MessageBox.Show("User does not exist !");
                return;
            }

            if (DialogResult.Yes == MessageBox.Show("Lock user '" + userId + "', are you sure ?", "Warning", MessageBoxButtons.YesNo))
            {
                HTTPAgent.instance().updateUser(this, userId, UpdateUserType.LockUser, "yes");
            }
        }

        private void UnLockButton_Click(object sender, EventArgs e)
        {
            string userId = LockedUsersListBox.SelectedItem.ToString();
            if (DialogResult.Yes == MessageBox.Show("UnLock user '" + userId + "', are you sure ?", "Warning", MessageBoxButtons.YesNo))
            {
                HTTPAgent.instance().updateUser(this, userId, UpdateUserType.LockUser, "no");
            }
        }
    }
}
