using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEMIAnalyzer.HTTP;
using WatsEMIAnalyzer.Bindings;

namespace WatsEMIAnalyzer
{
    public partial class NewTaskForm : Form
    {
        private SubRegion mRegion;
        private List<Site> mAvailableSites = null;
        private long mChannelSettingID = -1;
        private long mLinkConfigurationID = -1;
        private long mEquipmentParameterID = -1;
        private BindingTask mTask = null;
        
        
        public BindingTask NewTask
        {
            get { return mTask; }
        }

        public NewTaskForm(SubRegion region)
        {
            InitializeComponent();
            mRegion = region;
        }

        private void NewTaskForm_Load(object sender, EventArgs e)
        {
            HTTPAgent.instance().onAddTaskSuccessfully += new HTTPAgent.addTaskSuccessfully(NewTaskForm_onAddTaskSuccessfully);
            HTTPAgent.instance().onAddTaskFailed += new HTTPAgent.addTaskFailed(NewTaskForm_onAddTaskFailed);

            TaskNameEditor.Text = "Task (Created by " + HTTPAgent.Username + ", " + Utility.GetDateTime() + ")";
            mAvailableSites = Utility.GetAvailableSites(mRegion);
            foreach (Site site in mAvailableSites)
                TaskSiteList.Items.Add(site);
            mChannelSettingID = Utility.GetChannelSettingID(mRegion);
            mLinkConfigurationID = Utility.GetLinkConfigurationID(mRegion);
            mEquipmentParameterID = Utility.GetEquipmentParameterID(mRegion);

            if (mChannelSettingID != -1)
                ChannelSettingEditor.Text = DataCenter.Instance().ChannelSettingDescriptions[mChannelSettingID].Title;
            else
                ChannelSettingEditor.Text = "<Not set>";

            if (mLinkConfigurationID != -1)
                LinkConfigurationEditor.Text = DataCenter.Instance().LinkConfigurationDescriptions[mLinkConfigurationID].Title;
            else
                LinkConfigurationEditor.Text = "<Not set>";

            if (mEquipmentParameterID != -1)
                EquipmentParameterEditor.Text = DataCenter.Instance().EquipmentParameterDescriptions[mEquipmentParameterID].Title;
            else
                EquipmentParameterEditor.Text = "<Not set>";
        }

        void NewTaskForm_onAddTaskFailed(BindingTask task, System.Net.HttpStatusCode statusCode)
        {
            MessageBox.Show("Create task failed, status=" + statusCode.ToString() + " !");
            mTask = null;
            Close();
        }

        void NewTaskForm_onAddTaskSuccessfully(int RegionVersion, BindingTask task)
        {
            mTask = task;
            Close();
        }

        private void TaskDescriptionEditor_Leave(object sender, EventArgs e)
        {
            this.AcceptButton = OKButton;
        }

        private void TaskDescriptionEditor_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = null;
        }

        private void AddSiteButton_Click(object sender, EventArgs e)
        {
            SiteManagementForm siteManagementForm = new SiteManagementForm();
            siteManagementForm.UseForSelect = true;
            siteManagementForm.AvailableSites = mAvailableSites;
            if (siteManagementForm.ShowDialog() == DialogResult.Cancel)
                return;

            List<Site> selectedSites = siteManagementForm.SelectedSites;
            if (selectedSites.Count == 0)
                return;

            bool existed = false;
            foreach (Site selectedSite in selectedSites)
            {
                existed = false;
                foreach (Site taskSite in TaskSiteList.Items)
                {
                    if (taskSite.SiteID.Equals(selectedSite.SiteID))
                    {
                        existed = true;
                        break;
                    }
                }
                
                if (!existed)
                {
                    TaskSiteList.Items.Add(selectedSite);
                }
            }
        }

        private void RemoveSiteButton_Click(object sender, EventArgs e)
        {
            int count = TaskSiteList.SelectedItems.Count;
            if (DialogResult.No == MessageBox.Show("Remove " + count + " sites, are you sure ?", "Warning", MessageBoxButtons.YesNo))
                return;

            List<Site> removeSites = new List<Site>();
            foreach (Site removedSite in TaskSiteList.SelectedItems)
                removeSites.Add(removedSite);
            foreach (Site removedSite in removeSites)
            {
                TaskSiteList.Items.Remove(removedSite);
            }
        }

        private void ViewSiteButton_Click(object sender, EventArgs e)
        {
            Site site = (Site)TaskSiteList.SelectedItem;

            SiteDetailForm siteForm = new SiteDetailForm(false);
            siteForm.Site = site;
            siteForm.AllowUpdate = false;
            siteForm.ShowDialog();
        }

        private void AddAnalyzerButton_Click(object sender, EventArgs e)
        {
            AssignUserForm assignUserForm = new AssignUserForm();
            assignUserForm.Prompt = "Add task analyzer";
            if (DialogResult.OK == assignUserForm.ShowDialog())
            {
                foreach (string user in assignUserForm.Users)
                {
                    if (TaskAnalyzerList.Items.Contains(user))
                        continue;

                    TaskAnalyzerList.Items.Add(user);
                }
            }
        }

        private void RemoveAnalyzerButton_Click(object sender, EventArgs e)
        {
            int count = TaskAnalyzerList.SelectedItems.Count;
            if (DialogResult.No == MessageBox.Show("Remove " + count + " users, are you sure ?", "Warning", MessageBoxButtons.YesNo))
                return;

            List<string> removeUsers = new List<string>();
            foreach (string removedUser in TaskAnalyzerList.SelectedItems)
                removeUsers.Add(removedUser);
            foreach (string removeUser in removeUsers)
            {
                TaskAnalyzerList.Items.Remove(removeUser);
            }
        }

        private void ViewAnalyzerButton_Click(object sender, EventArgs e)
        {
            User user = DataCenter.Instance().Users[TaskAnalyzerList.SelectedItem.ToString()];
            UserDetailForm userDetailForm = new UserDetailForm(user);
            userDetailForm.AllowUpdate = false;
            userDetailForm.ShowDialog();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (!ValidateTask())
                return;

            BindingTask task = new BindingTask();
            task.Name = TaskNameEditor.Text;
            task.Description = TaskDescriptionEditor.Text;
            task.RegionID = mRegion.ID;
            task.Creator = HTTPAgent.Username;
            task.CreateTime = Utility.GetTimeStr();
            
            foreach (string userId in TaskAnalyzerList.Items)
                task.Analyzer.Add(userId);
            foreach (Site site in TaskSiteList.Items)
                task.Site.Add(site.SiteID);
            task.ChannelSettingID = mChannelSettingID;
            task.LinkConfigurationID = mLinkConfigurationID;
            task.EquipmentParameterID = mEquipmentParameterID;

            HTTPAgent.instance().addTask(this, DataCenter.Instance().GlobalRegion.Version, task);
        }

        private void TaskSiteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewSiteButton.Enabled = RemoveSiteButton.Enabled = (TaskSiteList.SelectedItems.Count > 0);
        }

        private void AnalyzerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewAnalyzerButton.Enabled = RemoveAnalyzerButton.Enabled = (TaskAnalyzerList.SelectedItems.Count > 0);
        }

        private void NewTaskForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void NewTaskForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            HTTPAgent.instance().onAddTaskSuccessfully -= new HTTPAgent.addTaskSuccessfully(NewTaskForm_onAddTaskSuccessfully);
            HTTPAgent.instance().onAddTaskFailed -= new HTTPAgent.addTaskFailed(NewTaskForm_onAddTaskFailed);
        }

        private bool ValidateTask()
        {
            TaskNameEditor.Text = TaskNameEditor.Text.Trim();
            if (TaskNameEditor.Text.Length == 0)
            {
                MessageBox.Show("Task name is empty !");
                TaskNameEditor.Focus();
                return false;
            }

            if (TaskAnalyzerList.Items.Count == 0)
            {
                MessageBox.Show("No analyzer available !");
                return false;
            }

            if (TaskSiteList.Items.Count == 0)
            {
                MessageBox.Show("No site available !");
                return false;
            }

            if (Utility.FindTaskByName(DataCenter.Instance().GlobalRegion.Root, TaskNameEditor.Text) != null)
            {
                MessageBox.Show("Task '" + TaskNameEditor.Text + "' already exists !");
                TaskNameEditor.SelectAll();
                TaskNameEditor.Focus();
                return false;
            }

            return true;
        }

        private void ChannelSettingDetailButton_Click(object sender, EventArgs e)
        {
            ChannelSettingDetailForm channelSettingForm = new ChannelSettingDetailForm(DataCenter.Instance().ChannelSettings[mChannelSettingID]);
            channelSettingForm.ShowDialog();
        }

        private void LinkConfigrationDetailButton_Click(object sender, EventArgs e)
        {
            LinkConfigurationDetailForm linkConfigurationForm = new LinkConfigurationDetailForm(DataCenter.Instance().LinkConfigurations[mLinkConfigurationID]);
            linkConfigurationForm.ShowDialog();
        }

        private void EquipmentParameterDetailButton_Click(object sender, EventArgs e)
        {
            EquipmentParameterDetailForm equipmentParameterForm
                = new EquipmentParameterDetailForm(DataCenter.Instance().EquipmentParameters[mEquipmentParameterID]);
            equipmentParameterForm.ShowDialog();
        }
    }
}
