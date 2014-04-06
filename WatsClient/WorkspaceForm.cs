using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsClient.Administration;
using WatsClient.Settings;
using WeifenLuo.WinFormsUI.Docking;

namespace WatsClient
{
    public partial class WorkspaceForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private DockPanel mDockPanel;

        private TreeNode mWorkspaceRootNode;

        //Administration
        private TreeNode mAdministrationNode;
        private TreeNode mUsersAndLicensingNode;

        //Setting Files
        private TreeNode mSettingFilesNode;
        private TreeNode mColorSettingNode;
        private TreeNode mSitesAndEmiNode;
        private TreeNode mChannelSettingFilesNode;
        private TreeNode mLinkconfigurationFilesNode;
        private TreeNode mEquipmentParameterFilesNode;

        //Region
        private TreeNode mRegionsAndAssignmentsNode;

        //Task
        private TreeNode mTasksNode;

        public WorkspaceForm(DockPanel dockPanel)
        {
            mDockPanel = dockPanel;
            InitializeComponent();
        }

        private void WorkspaceForm_Load(object sender, EventArgs e)
        {
            mWorkspaceRootNode = WorkspaceTreeView.Nodes.Add("Workspace");

            if (Constants.ROLE_ADMIN.Equals(DataCenter.MySelf.Role))
            {
                mAdministrationNode = mWorkspaceRootNode.Nodes.Add("Administration");
                mUsersAndLicensingNode = mAdministrationNode.Nodes.Add("Users & Licensing");
            }

            mSettingFilesNode = mWorkspaceRootNode.Nodes.Add("Settings File");
            mColorSettingNode = mSettingFilesNode.Nodes.Add("Color");
            mSitesAndEmiNode = mSettingFilesNode.Nodes.Add("Site & EMI");
            //mEmiFilesNode = mSettingFilesNode.Nodes.Add("EMI");
            mChannelSettingFilesNode = mSettingFilesNode.Nodes.Add("Channel");
            mLinkconfigurationFilesNode = mSettingFilesNode.Nodes.Add("Link");
            mEquipmentParameterFilesNode = mSettingFilesNode.Nodes.Add("Equipment");

            mRegionsAndAssignmentsNode = mWorkspaceRootNode.Nodes.Add("Regions & Assignments");

            mTasksNode = mWorkspaceRootNode.Nodes.Add("Tasks");

            mWorkspaceRootNode.ExpandAll();
        }

        private void WorkspaceTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            WorkspaceTreeView.SelectedNode = WorkspaceTreeView.GetNodeAt(e.X, e.Y);
            TreeNode curNode = WorkspaceTreeView.GetNodeAt(e.X, e.Y);
            if (curNode == null)
            {
                return;
            }
            else if (curNode == mUsersAndLicensingNode)
            {
                UserAndLicensingContextMenuStrip.Show(Cursor.Position);
            }
            else if (curNode == mColorSettingNode)
            {
                
            }


            /*
            if (WorkspaceTreeView.SelectedNode.Tag is TaskModel)
            {
                TaskContextMenuStrip.Show(Cursor.Position);
            }
            */ 
        }

        private void WorkspaceTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            WorkspaceTreeView.SelectedNode = WorkspaceTreeView.GetNodeAt(e.X, e.Y);
            TreeNode curNode = WorkspaceTreeView.GetNodeAt(e.X, e.Y);

            if (e.Button == MouseButtons.Left)
            {
                if (curNode == mUsersAndLicensingNode)
                {
                    if (!UserForm.Instance.Visible)
                        UserForm.Instance.Show(mDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                    else
                        UserForm.Instance.Focus();
                }
                else if (curNode == mColorSettingNode)
                {
                    if (!ColorSettingForm.Instance.Visible)
                        ColorSettingForm.Instance.Show(mDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                    else
                        ColorSettingForm.Instance.Focus();
                }

                /*
                else if (curNode == mChannelSettingFilesNode)
                {
                    if (!MainForm.Instance.mChannelSettingForm.Visible)
                        MainForm.Instance.mChannelSettingForm.Show();
                    else
                        MainForm.Instance.mChannelSettingForm.Focus();
                }
                else if (curNode == mLinkconfigurationFilesNode)
                {
                    if (!MainForm.Instance.mLinkConfigurationForm.Visible)
                        MainForm.Instance.mLinkConfigurationForm.Show();
                    else
                        MainForm.Instance.mLinkConfigurationForm.Focus();
                }
                else if (curNode == mEquipmentParameterFilesNode)
                {
                    if (!MainForm.Instance.mEquipmentParameterForm.Visible)
                        MainForm.Instance.mEquipmentParameterForm.Show();
                    else
                        MainForm.Instance.mEquipmentParameterForm.Focus();
                }
                */


            }

        }

        private void AddUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserDetailForm userDetailForm = new UserDetailForm();
            userDetailForm.ShowDialog();
        }
    }
}
