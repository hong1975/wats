using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEMIAnalyzer.Model;
using WatsEMIAnalyzer.EMI;
using System.IO;
using System.Xml;
using WatsEMIAnalyzer.Bindings;
using WatsEMIAnalyzer.HTTP;
using System.Net;
using System.Runtime.InteropServices;

namespace WatsEMIAnalyzer
{
    public partial class WorkspaceForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private const int ICON_WORKSPACE = 22;
        private const int ICON_ADMIN = 0;
        private const int ICON_MANAGER = 10;
        private const int ICON_SETTINGS = 15;
        private const int ICON_SITES = 17;
        private const int ICON_SITE = 16;
        private const int ICON_USERS = 11;
        private const int ICON_REGION = 13;
        private const int ICON_TASK = 19;
        private const int ICON_LINKCONFIGURATION = 6;
        private const int ICON_CHANNELSETTING = 5;
        private const int ICON_EQUIPMENTPARAMETER = 8;
        private const int ICON_REGIONUNAUTHORIZED = 23;
        private const int ICON_UNASSIGNEDSITE = 20;
        private const int ICON_ANALYZER = 24;
        private const int ICON_ASSIGNEDTASK = 4;
        private const int ICON_MANAGEMENT = 9;
        private const int ICON_GLOBAL = 7;
        private const int ICON_ANALYSISES = 3;
        private const int ICON_ANALYSIS = 2;
        private const int ICON_TABLEVIEW = 18;
        private const int ICON_MAPVIEW = 12;
        private const int ICON_REPORTVIEW = 14;
        private const int ICON_ALLEMIFILE = 1;
        private const int ICON_EMI = 25;
        private const int ICON_UNASSIGNEDSITES = 21;
        private const int ICON_UNASSIGNEDEMI = 26;
        private const int ICON_CHANNELSETTINGS = 27;
        private const int ICON_LINKSETTINGS = 28;
        private const int ICON_EQUIPMENTSETTINGS = 29;

        

        public enum NodeType
        {
            Region,
            RegionManagers,
            RegionManager,
            RegionSites,
            RegionSite,
            RegionChannelSetting,
            RegionLinkConfiguration,
            RegionEquipmentParameter,

            Task,
            TaskSites,
            TaskSite,
            TaskAnalyzers,
            TaskAnalyzer,
        }

        private static WorkspaceForm instance;
        private TreeNode mWorkspaceNode;
        private TreeNode mAssignedTasksNode;
        private TreeNode mManagementNode;
        private TreeNode mGlobalNode;
        private bool mIsFirstLoad = true;

        public static WorkspaceForm Instance
        {
            get { return instance; }
        }

        public WorkspaceForm()
        {
            instance = this;
            InitializeComponent();
            HideOnClose = true;
        }

        public TreeNode WorkspaceNode
        {
            get { return mWorkspaceNode; }
        }

        public TreeNode AssignedTasksNode
        {
            get { return mAssignedTasksNode; }
        }

        public TreeNode ManagementNode
        {
            get { return mManagementNode; }
        }

        private void LoadRegion(TreeNode parentNode, SubRegion parentRegion)
        {
            TreeNode node;
            TreeNode adminNode;
            TreeNode managersNode;
            TreeNode managerNode;
            TreeNode sitesNode;
            TreeNode siteNode;
            TreeNode settingsNode;
            TreeNode channelSettingsNode;
            TreeNode linkSettingsNode;
            TreeNode equipmentSettingsNode;
            UsersModel usersModel;
            TreeNode channelSettingNode;
            TreeNode linkConfigurationNode;
            TreeNode equipmentParameterNode;
            if (parentRegion.Sub == null)
                return;
            foreach (SubRegion region in parentRegion.Sub)
            {
                node = parentNode.Nodes.Add(region.Name);
                node.Tag = region;
                node.ImageIndex = node.SelectedImageIndex = ICON_REGION;

                if (node.Parent != mGlobalNode)
                    adminNode = node.Nodes.Add("Administrator - " + region.Owner);
                else
                    adminNode = node.Nodes.Add("Administrator - [SYSTEM ADMIN]");
                adminNode.SelectedImageIndex = adminNode.ImageIndex = ICON_ADMIN;

                if (region.Manager.IndexOf(HTTPAgent.Username) >= 0
                    || region.Manager.Count == 0 && region.Owner != null && region.Owner.Equals(HTTPAgent.Username))
                {
                    managersNode = node.Nodes.Add("Managers");
                    usersModel = new UsersModel();
                    managersNode.Tag = usersModel;
                    managersNode.SelectedImageIndex = managersNode.ImageIndex = ICON_USERS;
                    if (region.Manager != null)
                    {
                        foreach (string manager in region.Manager)
                        {
                            managerNode = managersNode.Nodes.Add(manager);
                            managerNode.SelectedImageIndex = managerNode.ImageIndex = ICON_MANAGER;
                            usersModel.Add(manager);
                        }
                    }

                    sitesNode = node.Nodes.Add("Sites");
                    sitesNode.SelectedImageIndex = sitesNode.ImageIndex = ICON_SITES;
                    sitesNode.Tag = new SitesModel();
                    Site site;
                    if (region.Site != null)
                    {
                        foreach (string siteID in region.Site)
                        {
                            if (DataCenter.Instance().Sites.ContainsKey(siteID))
                                site = DataCenter.Instance().Sites[siteID];
                            else
                                site = new Site(siteID);
                            siteNode = sitesNode.Nodes.Add(site.ToString());
                            siteNode.SelectedImageIndex = siteNode.ImageIndex = ICON_SITE;
                            siteNode.Tag = new SiteModel(site);
                        }
                    }
                    SiteNodeListSorter.SortByName(sitesNode);
                    

                    settingsNode = node.Nodes.Add("Settings");
                    settingsNode.SelectedImageIndex = settingsNode.ImageIndex = ICON_SETTINGS;

                    channelSettingsNode = settingsNode.Nodes.Add("Channel Settings");
                    channelSettingsNode.SelectedImageIndex = channelSettingsNode.ImageIndex = ICON_CHANNELSETTINGS;

                    linkSettingsNode = settingsNode.Nodes.Add("Link Configuration Settings");
                    linkSettingsNode.SelectedImageIndex = linkSettingsNode.ImageIndex = ICON_LINKSETTINGS;

                    equipmentSettingsNode = settingsNode.Nodes.Add("Equipment Parameter Settings");
                    equipmentSettingsNode.SelectedImageIndex = equipmentSettingsNode.ImageIndex = ICON_EQUIPMENTSETTINGS;

                    /*
                    channelSettingNode = settingsNode.Nodes.Add("");
                    if (region.ChannelSettingID == -1)
                        channelSettingNode.Text = "Channel setting - <Not set>";
                    else
                        channelSettingNode.Text = "Channel setting - " + DataCenter.Instance().ChannelSettingDescriptions[region.ChannelSettingID].FileName;
                    channelSettingNode.SelectedImageIndex = channelSettingNode.ImageIndex = ICON_CHANNELSETTING;
                    channelSettingNode.Tag = new ChannelModel(region.ChannelSettingID);

                    linkConfigurationNode = settingsNode.Nodes.Add("");
                    if (region.LinkConfigurationID == -1)
                        linkConfigurationNode.Text = "Link configuration - <Not set>";
                    else
                        linkConfigurationNode.Text = DataCenter.Instance().LinkConfigurationDescriptions[region.LinkConfigurationID].FileName;
                    linkConfigurationNode.SelectedImageIndex = linkConfigurationNode.ImageIndex = ICON_LINKCONFIGURATION;
                    linkConfigurationNode.Tag = new LinkModel(region.LinkConfigurationID);

                    equipmentParameterNode = settingsNode.Nodes.Add("");
                    if (region.EquipmentParameterID == -1)
                        equipmentParameterNode.Text = "Equipment parameter - <Not set>";
                    else
                        equipmentParameterNode.Text = DataCenter.Instance().EquipmentParameterDescriptions[region.EquipmentParameterID].FileName;
                    equipmentParameterNode.SelectedImageIndex = equipmentParameterNode.ImageIndex = ICON_EQUIPMENTPARAMETER;
                    equipmentParameterNode.Tag = new EquipmentModel(region.EquipmentParameterID);
                    */ 

                    if (region.Task != null)
                    {
                        foreach (long taskID in region.Task)
                        {
                            LoadTask(node, taskID);
                        }
                    }
                    
                }
                else
                {
                    node.ImageIndex = node.SelectedImageIndex = ICON_REGIONUNAUTHORIZED;
                }

                LoadRegion(node, region);
            }

            
        }

        private void LoadTask(TreeNode regionNode, long taskID)
        {
            if (!DataCenter.Instance().Tasks.ContainsKey(taskID))
            {
                MessageBox.Show("Task " + taskID + " does not exist !");
                return;
            }

            BindingTask task = DataCenter.Instance().Tasks[taskID];
            TreeNode taskNode = regionNode.Nodes.Add(task.Name);
            taskNode.Tag = new TaskModel(task);
            taskNode.SelectedImageIndex = taskNode.ImageIndex = ICON_TASK;

            TreeNode sitesNode = taskNode.Nodes.Add("Sites");
            sitesNode.SelectedImageIndex = sitesNode.ImageIndex = ICON_SITES;
            sitesNode.Tag = new SitesModel();

            Site site;
            TreeNode siteNode, analyzersNode, analyzerNode;            
            foreach (string siteID in task.Site)
            {
                if (DataCenter.Instance().Sites.ContainsKey(siteID))
                    site = DataCenter.Instance().Sites[siteID];
                else
                    site = new Site(siteID);
                siteNode = sitesNode.Nodes.Add(site.ToString());
                siteNode.SelectedImageIndex = siteNode.ImageIndex = ICON_SITE;
                siteNode.Tag = new SiteModel(site);
            }

            foreach (string siteID in task.UnassignedSite)
            {
                if (DataCenter.Instance().Sites.ContainsKey(siteID))
                    site = DataCenter.Instance().Sites[siteID];
                else
                    site = new Site(siteID);
                siteNode = sitesNode.Nodes.Add(site.ToString());
                siteNode.SelectedImageIndex = siteNode.ImageIndex = ICON_UNASSIGNEDSITE;
                siteNode.Tag = new SiteModel(site);
            }

            analyzersNode = taskNode.Nodes.Add("Analyzers");
            UsersModel usersModel = new UsersModel();
            analyzersNode.Tag = usersModel;
            analyzersNode.SelectedImageIndex = analyzersNode.ImageIndex = ICON_USERS;
            if (task.Analyzer != null)
            {
                foreach (string analyzer in task.Analyzer)
                {
                    analyzerNode = analyzersNode.Nodes.Add(analyzer);
                    analyzerNode.SelectedImageIndex = analyzerNode.ImageIndex = ICON_ANALYZER;
                    usersModel.Add(analyzer);
                }
            }
        }

        private void WorkspaceForm_Load(object sender, EventArgs e)
        {
            HTTPAgent.instance().onUpdateRegionSuccessfully += new HTTPAgent.updateRegionSuccessfully(WorkspaceForm_onUpdateRegionSuccessfully);
            HTTPAgent.instance().onUpdateRegionFailed += new HTTPAgent.updateRegionFailed(WorkspaceForm_onUpdateRegionFailed);

            HTTPAgent.instance().onAddUserSuccessfully += new HTTPAgent.addUserSuccessfully(WorkspaceForm_onAddUserSuccessfully);
            HTTPAgent.instance().onAddUserFailed += new HTTPAgent.addUserFailed(WorkspaceForm_onAddUserFailed);

            HTTPAgent.instance().onRemoveUserSuccessfully += new HTTPAgent.removeUserSuccessfully(WorkspaceForm_onRemoveUserSuccessfully);
            HTTPAgent.instance().onRemoveUserFailed += new HTTPAgent.removeUserFailed(WorkspaceForm_onRemoveUserFailed);

            HTTPAgent.instance().onUpdateUserSuccessfully += new HTTPAgent.updateUserSuccessfully(WorkspaceForm_onUpdateUserSuccessfully);
            HTTPAgent.instance().onUpdateUserFailed += new HTTPAgent.updateUserFailed(WorkspaceForm_onUpdateUserFailed);

            HTTPAgent.instance().onUpdateTaskSuccessfully += new HTTPAgent.updateTaskSuccessfully(WorkspaceForm_onUpdateTaskSuccessfully);
            HTTPAgent.instance().onUpdateTaskFailed += new HTTPAgent.updateTaskFailed(WorkspaceForm_onUpdateTaskFailed);

            HTTPAgent.instance().onRemoveTaskSuccessfully += new HTTPAgent.removeTaskSuccessfully(WorkspaceForm_onRemoveTaskSuccessfully);
            HTTPAgent.instance().onRemoveTaskFailed += new HTTPAgent.removeTaskFailed(WorkspaceForm_onRemoveTaskFailed);

            HTTPAgent.instance().onGetReportsSuccessfully += new HTTPAgent.getReportsSuccessfully(WorkspaceForm_onGetReportsSuccessfully);
            HTTPAgent.instance().onGetReportsFailed += new HTTPAgent.getReportsFailed(WorkspaceForm_onGetReportsFailed);

            WorkspaceTreeView.ImageList = WorkspaceImageList;

            mWorkspaceNode = WorkspaceTreeView.Nodes.Add("Workspace");
            mWorkspaceNode.SelectedImageIndex = mWorkspaceNode.ImageIndex = ICON_WORKSPACE;

            mAssignedTasksNode = mWorkspaceNode.Nodes.Add("Assigned Tasks");
            mAssignedTasksNode.SelectedImageIndex = mAssignedTasksNode.ImageIndex = ICON_ASSIGNEDTASK;

            mManagementNode = mWorkspaceNode.Nodes.Add("Management");
            mManagementNode.SelectedImageIndex = mManagementNode.ImageIndex = ICON_MANAGEMENT;
            mGlobalNode = mManagementNode.Nodes.Add("Global");
            mGlobalNode.SelectedImageIndex = mGlobalNode.ImageIndex = ICON_GLOBAL;
            mGlobalNode.Tag = DataCenter.Instance().GlobalRegion.Root;

            ReLoad();
        }

        public void Detach()
        {
            HTTPAgent.instance().onUpdateRegionSuccessfully -= new HTTPAgent.updateRegionSuccessfully(WorkspaceForm_onUpdateRegionSuccessfully);
            HTTPAgent.instance().onUpdateRegionFailed -= new HTTPAgent.updateRegionFailed(WorkspaceForm_onUpdateRegionFailed);

            HTTPAgent.instance().onAddUserSuccessfully -= new HTTPAgent.addUserSuccessfully(WorkspaceForm_onAddUserSuccessfully);
            HTTPAgent.instance().onAddUserFailed -= new HTTPAgent.addUserFailed(WorkspaceForm_onAddUserFailed);

            HTTPAgent.instance().onRemoveUserSuccessfully -= new HTTPAgent.removeUserSuccessfully(WorkspaceForm_onRemoveUserSuccessfully);
            HTTPAgent.instance().onRemoveUserFailed -= new HTTPAgent.removeUserFailed(WorkspaceForm_onRemoveUserFailed);

            HTTPAgent.instance().onUpdateUserSuccessfully -= new HTTPAgent.updateUserSuccessfully(WorkspaceForm_onUpdateUserSuccessfully);
            HTTPAgent.instance().onUpdateUserFailed -= new HTTPAgent.updateUserFailed(WorkspaceForm_onUpdateUserFailed);

            HTTPAgent.instance().onUpdateTaskSuccessfully -= new HTTPAgent.updateTaskSuccessfully(WorkspaceForm_onUpdateTaskSuccessfully);
            HTTPAgent.instance().onUpdateTaskFailed -= new HTTPAgent.updateTaskFailed(WorkspaceForm_onUpdateTaskFailed);

            HTTPAgent.instance().onRemoveTaskSuccessfully -= new HTTPAgent.removeTaskSuccessfully(WorkspaceForm_onRemoveTaskSuccessfully);
            HTTPAgent.instance().onRemoveTaskFailed -= new HTTPAgent.removeTaskFailed(WorkspaceForm_onRemoveTaskFailed);

            HTTPAgent.instance().onGetReportsSuccessfully -= new HTTPAgent.getReportsSuccessfully(WorkspaceForm_onGetReportsSuccessfully);
            HTTPAgent.instance().onGetReportsFailed -= new HTTPAgent.getReportsFailed(WorkspaceForm_onGetReportsFailed);

        }

        public void ReLoad()
        {
            if (!mIsFirstLoad)
            {
                SyncForm syncForm = new SyncForm();
                syncForm.ShowDialog();
            }
            
            mAssignedTasksNode.Nodes.Clear();
            mGlobalNode.Nodes.Clear();

            if (DataCenter.Instance().GlobalRegion.Root.Sub == null)
                return;

            LoadRegion(mGlobalNode, DataCenter.Instance().GlobalRegion.Root);
            mGlobalNode.Expand();

            foreach (BindingTask task in DataCenter.Instance().Tasks.Values)
            {
                if (task.Analyzer.Contains(HTTPAgent.Username))
                {
                    AddAssignedTaskNode(task);
                }
            }
            mWorkspaceNode.Expand();
            mAssignedTasksNode.Expand();
            mManagementNode.Expand();

            if (mIsFirstLoad)
                mIsFirstLoad = false;
        }

        void WorkspaceForm_onUpdateTaskFailed(long taskID, UpdateTaskRequest request, HttpStatusCode statusCode, string messageText)
        {
            MessageBox.Show(messageText + " failed, status=" + statusCode.ToString());
            if (UpdateTaskType.AddAnalyzer.ToString().Equals(request.Type))
            {
                ReLoad();
                FocusNode(NodeType.TaskAnalyzers, (object)taskID);
            }
            else if (UpdateTaskType.RemoveAnalyzer.ToString().Equals(request.Type))
            {
                ReLoad();
                FocusNode(NodeType.TaskAnalyzers, (object)taskID);

            }
            else if (UpdateTaskType.AddSite.ToString().Equals(request.Type))
            {
                ReLoad();
                FocusNode(NodeType.TaskSites, (object)taskID);
            }
            else if (UpdateTaskType.RemoveSite.ToString().Equals(request.Type))
            {
                ReLoad();
                FocusNode(NodeType.TaskSites, (object)taskID);
            }
        }

        void WorkspaceForm_onUpdateTaskSuccessfully(long taskID, UpdateTaskRequest request, object context)
        {
            if (UpdateTaskType.AddAnalyzer.ToString().Equals(request.Type))
            {
                string lastAnalyzer = null;
                foreach (string analyzer in request.Analyzer)
                {
                    if (!DataCenter.Instance().Tasks[taskID].Analyzer.Contains(analyzer))
                    {
                        DataCenter.Instance().Tasks[taskID].Analyzer.Add(analyzer);
                        lastAnalyzer = analyzer;
                    }
                }

                if (lastAnalyzer != null)
                {
                    ReLoad();
                    FocusNode(NodeType.TaskAnalyzer, (object)taskID, lastAnalyzer);
                }
            }
            else if (UpdateTaskType.RemoveAnalyzer.ToString().Equals(request.Type))
            {
                ReLoad();
                FocusNode(NodeType.TaskAnalyzers, (object)taskID);

            }
            else if (UpdateTaskType.AddSite.ToString().Equals(request.Type))
            {
                string lastSite = null;
                foreach (string site in request.Site)
                {
                    if (!DataCenter.Instance().Tasks[taskID].Site.Contains(site))
                    {
                        DataCenter.Instance().Tasks[taskID].Site.Add(site);
                        lastSite = site;
                    }
                }

                if (lastSite != null)
                {
                    ReLoad();
                    FocusNode(NodeType.TaskSite, (object)taskID, lastSite);
                }
            }
            else if (UpdateTaskType.RemoveSite.ToString().Equals(request.Type))
            {
                ReLoad();
                FocusNode(NodeType.TaskSites, (object)taskID);
            }
            else if (UpdateTaskType.RenameTask.ToString().Equals(request.Type))
            {
                ReLoad();
                FocusNode(NodeType.Task, (object)taskID);
            }
            else 
            {
                return;
            }
        }

        void WorkspaceForm_onRemoveTaskSuccessfully(long taskID, object context)
        {
            BindingTask task = null;
            if (DataCenter.Instance().Tasks.ContainsKey(taskID))
                task = DataCenter.Instance().Tasks[taskID];

            ReLoad();
            if (task != null)
            {
                FocusNode(NodeType.Region, task.RegionID);
            }
        }

        void WorkspaceForm_onRemoveTaskFailed(long taskID, HttpStatusCode statusCode, string messageText, object context)
        {
            MessageBox.Show(messageText + " failed, status=" + statusCode.ToString());
            BindingTask task = null;
            if (DataCenter.Instance().Tasks.ContainsKey(taskID))
                task = DataCenter.Instance().Tasks[taskID];

            ReLoad();
            if (task != null)
            {
                FocusNode(NodeType.Region, task.RegionID);
            }
        }

        void WorkspaceForm_onUpdateUserFailed(string user, WatsEMIAnalyzer.Bindings.UpdateUserType type, System.Net.HttpStatusCode statusCode)
        {
        }

        void WorkspaceForm_onUpdateUserSuccessfully(string user, WatsEMIAnalyzer.Bindings.UpdateUserType type, string newValue)
        {
        }

        void WorkspaceForm_onRemoveUserFailed(string user, System.Net.HttpStatusCode statusCode)
        {
        }

        void WorkspaceForm_onRemoveUserSuccessfully(string user)
        {
        }

        void WorkspaceForm_onAddUserFailed(string user, System.Net.HttpStatusCode statusCode)
        {
        }

        void WorkspaceForm_onAddUserSuccessfully(WatsEMIAnalyzer.Bindings.User user)
        {
        }

        void WorkspaceForm_onUpdateRegionFailed(UpdateRegionRequest request, System.Net.HttpStatusCode statusCode, string messageText)
        {
            if (request.Type.Equals(UpdateRegionType.MoveTasksToNewRegion.ToString()))
                return;

            MessageBox.Show(messageText + " failed, status=" + statusCode.ToString());

            if (UpdateRegionType.AddRegion.ToString().Equals(request.Type))
            {
                ReLoad();
                FocusNode(NodeType.Region, request.Region.ParentID);
            }
            else if (UpdateRegionType.RenameRegion.ToString().Equals(request.Type))
            {
                ReLoad();
                FocusNode(NodeType.Region, request.Region.ID);
            }
            else if (UpdateRegionType.RemoveRegion.ToString().Equals(request.Type))
            {
                ReLoad();
                FocusNode(NodeType.Region, request.Region.ID);
            }
            else if (UpdateRegionType.AddManager.ToString().Equals(request.Type))
            {
                ReLoad();
                FocusNode(NodeType.RegionManagers, request.Region.ID);
            }
            else if (UpdateRegionType.RemoveManager.ToString().Equals(request.Type))
            {
                ReLoad();
                FocusNode(NodeType.RegionManagers, request.Region.ID);
            }
            else if (UpdateRegionType.AddSite.ToString().Equals(request.Type))
            {
                ReLoad();
                FocusNode(NodeType.RegionSites, request.Region.ID);
            }
            else if (UpdateRegionType.RemoveSite.ToString().Equals(request.Type))
            {
                ReLoad();
                FocusNode(NodeType.RegionSites, request.Region.ID);
            }
            else if (UpdateRegionType.UpdateChanSettingID.ToString().Equals(request.Type))
            {
                ReLoad();
                FocusNode(NodeType.RegionChannelSetting, request.Region.ID);
            }
            else if (UpdateRegionType.UpdateLinkConfigID.ToString().Equals(request.Type))
            {
                ReLoad();
                FocusNode(NodeType.RegionLinkConfiguration, request.Region.ID);
            }
            else if (UpdateRegionType.UpdateEquipParamID.ToString().Equals(request.Type))
            {
                ReLoad();
                FocusNode(NodeType.RegionEquipmentParameter, request.Region.ID);
            }
        }

        void WorkspaceForm_onUpdateRegionSuccessfully(WatsEMIAnalyzer.Bindings.UpdateRegionResult result, object context)
        {
            //DataCenter.Instance().GlobalRegion.Version = result.NewVer;
            if (UpdateRegionType.AddRegion.ToString().Equals(result.Type))
            {
                ReLoad();
                FocusNode(NodeType.Region, result.Region.ID);
            }
            else if (UpdateRegionType.RenameRegion.ToString().Equals(result.Type))
            {
                ReLoad();
                FocusNode(NodeType.Region, result.Region.ID);
            }
            else if (UpdateRegionType.RemoveRegion.ToString().Equals(result.Type))
            {
                TreeNode regionNode = (TreeNode)context;
                ReLoad();
                FocusNode(NodeType.Region, ((SubRegion)regionNode.Parent.Tag).ID);
            }
            else if (UpdateRegionType.AddManager.ToString().Equals(result.Type))
            {
                string lastManager = null;
                foreach (string manager in result.Region.Manager)
                {
                    lastManager = manager;
                }

                ReLoad();
                if (lastManager != null)
                    FocusNode(NodeType.RegionManager, result.Region.ID, lastManager);
            }
            else if (UpdateRegionType.RemoveManager.ToString().Equals(result.Type))
            {
                ReLoad();
                FocusNode(NodeType.RegionManagers, result.Region.ID);
            }
            else if (UpdateRegionType.AddSite.ToString().Equals(result.Type))
            {
                string lastSiteID = null;
                foreach (string siteID in result.Region.Site)
                {
                    lastSiteID = siteID;
                }
                ReLoad();
                if (lastSiteID != null)
                    FocusNode(NodeType.RegionSite, result.Region.ID, lastSiteID);
            }
            else if (UpdateRegionType.RemoveSite.ToString().Equals(result.Type))
            {
                ReLoad();
                FocusNode(NodeType.RegionSites, result.Region.ID);
            }
            else if (UpdateRegionType.UpdateChanSettingID.ToString().Equals(result.Type))
            {
                ReLoad();
                FocusNode(NodeType.RegionChannelSetting, result.Region.ID);
            }
            else if (UpdateRegionType.UpdateLinkConfigID.ToString().Equals(result.Type))
            {
                ReLoad();
                FocusNode(NodeType.RegionLinkConfiguration, result.Region.ID);
            }
            else if (UpdateRegionType.UpdateEquipParamID.ToString().Equals(result.Type))
            {
                ReLoad();
                FocusNode(NodeType.RegionEquipmentParameter, result.Region.ID);
            }
        }

        private string GetNodeParentPath(XmlNode node)
        {
            StringBuilder parentPath = new StringBuilder();
            XmlNode parent = node.ParentNode;
            while (parent != null && !"Sites".Equals(parent.Name, StringComparison.OrdinalIgnoreCase))
            {
                parentPath.Insert(0, parent.Name + ".");
                parent = parent.ParentNode;
            }

            return parentPath.ToString();
        }

        private void UpdateAssignTaskNodeText()
        {
            mAssignedTasksNode.Text = "Assigned Tasks '" 
                + DataCenter.Instance().Tasks.Count +" tasks'";
        }

        /************************************************************************/
        /* WorkspaceTreeView mouse event                                        */
        /************************************************************************/
        private void WorkspaceTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null && e.Node.Tag is AnalysisModel)
            {
                //ReportViewForm analysisViewForm = ((AnalysisModel)e.Node.Tag).AnalysisViewForm;
                //analysisViewForm.Show(MDIForm.Instance.getDockPanel(), WeifenLuo.WinFormsUI.Docking.DockState.Document);
            }
            else if (e.Node.Tag != null && e.Node.Tag is AnalysisReportViewModel)
            {
                ReportViewForm reportViewForm = ((AnalysisReportViewModel)e.Node.Tag).ReportForm;
                reportViewForm.Show(MDIForm.Instance.getDockPanel(), WeifenLuo.WinFormsUI.Docking.DockState.Document);
                reportViewForm.Text = "Report View - " + e.Node.Parent.Text;
            }
            else if (e.Node.Tag != null && e.Node.Tag is AnalysisTableViewModel)
            {
                TableViewForm tableViewForm = ((AnalysisTableViewModel)e.Node.Tag).TableForm;
                tableViewForm.Show(MDIForm.Instance.getDockPanel(), WeifenLuo.WinFormsUI.Docking.DockState.Document);
                tableViewForm.Text = "Table View - " + e.Node.Parent.Text;
            }
        }

        private void WorkspaceTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            WorkspaceTreeView.SelectedNode = WorkspaceTreeView.GetNodeAt(e.X, e.Y);
            if (WorkspaceTreeView.GetNodeAt(e.X, e.Y) == null)
            {
                return;
            }

            if (WorkspaceTreeView.SelectedNode.Tag is TaskModel)
            {
                TaskContextMenuStrip.Show(Cursor.Position);
            }
            else if (WorkspaceTreeView.SelectedNode.Tag is List<AnalysisModel>)
            {
                TaskAllAnalysisContextMenuStrip.Show(Cursor.Position);
            }
            else if (WorkspaceTreeView.SelectedNode.Tag is AnalysisModel)
            {
                TaskAnalysisContextMenuStrip.Show(Cursor.Position);
            }
            else if (WorkspaceTreeView.SelectedNode.Tag is AnalysisTableViewModel)
            {

            }
            else if (WorkspaceTreeView.SelectedNode.Tag is AnalysisMapViewModel)
            {
                //MapViewContextMenuStrip.Show(Cursor.Position);
            }
            else if (WorkspaceTreeView.SelectedNode.Tag is AnalysisReportViewModel)
            {

            }
            else if (e.Node.Tag != null && e.Node.Tag is EMIFileModel)
            {
                EmiContextMenuStrip.Show(Cursor.Position);
            }
            
            else if (e.Node.Tag != null && e.Node.Tag is MapModel)
            {
                MapContextMenuStrip.Show(Cursor.Position);
            }
            else if (WorkspaceTreeView.SelectedNode.Tag is SubRegion)
            {
                RegionContextMenuStrip.Show(Cursor.Position);
            }
            else if ("Management".Equals(e.Node.Text))
            {
                AdministrationContextMenuStrip.Show(Cursor.Position);
            }
            else if ("Managers".Equals(e.Node.Text))
            {
                ManagersContextMenuStrip.Show(Cursor.Position);
            }
            else if (e.Node.Parent != null && "Managers".Equals(e.Node.Parent.Text))
            {
                ManagerContextMenuStrip.Show(Cursor.Position);
            }
            else if ("Analyzers".Equals(e.Node.Text))
            {
                AnalyzersContextMenuStrip.Show(Cursor.Position);
            }
            else if (e.Node.Parent != null && "Analyzers".Equals(e.Node.Parent.Text))
            {
                AnalyzerContextMenuStrip.Show(Cursor.Position);
            }
            else if (e.Node.Tag is SitesModel)
            {
                SitesContextMenuStrip.Show(Cursor.Position);
            }
            else if (WorkspaceTreeView.SelectedNode.Tag is SiteModel)
            {
                SiteContextMenuStrip.Show(Cursor.Position);
            }
            else if (WorkspaceTreeView.SelectedNode.Tag is ChannelModel)
            {
                ChannelContextMenuStrip.Show(Cursor.Position);
            }
            else if (WorkspaceTreeView.SelectedNode.Tag is LinkModel)
            {
                LinkContextMenuStrip.Show(Cursor.Position);
            }
            else if (WorkspaceTreeView.SelectedNode.Tag is EquipmentModel)
            {
                EquipmentContextMenuStrip.Show(Cursor.Position);
            }
        }

        #region Task Menu 
        
        private void RenameTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldTaskName = WorkspaceTreeView.SelectedNode.Text;
            RenameForm renameForm = new RenameForm("Rename task", oldTaskName);
            renameForm.CheckInputDelegate = new CheckInputDelegate(CheckTaskName);
            if (DialogResult.OK == renameForm.ShowDialog() && renameForm.Updated)
            {
                UpdateTaskRequest request = new UpdateTaskRequest();
                request.Type = UpdateTaskType.RenameTask.ToString();
                request.NewName = renameForm.NewName;

                long taskID = ((TaskModel)WorkspaceTreeView.SelectedNode.Tag).mTask.ID;

                HTTPAgent.instance().updateTask(this, taskID, request,
                    "Rename task '" + oldTaskName + "'", WorkspaceTreeView.SelectedNode);
            }
        }

        private void RemoveTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string prompt = "Remove task '" + WorkspaceTreeView.SelectedNode.Text + "', are you sure ?";
            if (DialogResult.No == MessageBox.Show(prompt, "Warning", MessageBoxButtons.YesNo))
                return;

            BindingTask task = ((TaskModel)WorkspaceTreeView.SelectedNode.Tag).mTask;
            HTTPAgent.instance().removeTask(this, task.ID, "Remove task '" + task.Name + "'", null);
        }

        private void UploadEMIFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BindingTask task = ((TaskModel)WorkspaceTreeView.SelectedNode.Tag).mTask;
            EMIFileUploadForm uploadForm = new EMIFileUploadForm(((TaskModel)WorkspaceTreeView.SelectedNode.Tag).mTask);
            uploadForm.ShowDialog();

            ReLoad();
            FocusNode(NodeType.Task, task.ID);
        }

        private void GetTaskReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BindingTask task = ((TaskModel)WorkspaceTreeView.SelectedNode.Tag).mTask;
            HTTPAgent.instance().getTaskReports(this, task.ID, null);
        }
        #endregion

        #region Task All Analysis Menu
        private void NewAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TaskModel taskModel = (TaskModel)WorkspaceTreeView.SelectedNode.Parent.Tag;
            string taskName = taskModel.mTask.Name;

            //Analysis Node
            AnalysisModel analysisModel = new AnalysisModel();
            analysisModel.Name = "Analysis-" + Utility.GetTimeStr();
            taskModel.AnalysisModelList.Add(analysisModel);
            TreeNode analysisNode = WorkspaceTreeView.SelectedNode.Nodes.Add(analysisModel.Name);
            analysisNode.Tag = analysisModel;
            analysisNode.SelectedImageIndex = analysisNode.ImageIndex = ICON_ANALYSIS;
            analysisNode.Parent.Expand();
            WorkspaceTreeView.SelectedNode = analysisNode;

            //Table View Node
            TreeNode tableViewNode = analysisNode.Nodes.Add("Table View");
            tableViewNode.ImageIndex = tableViewNode.SelectedImageIndex = ICON_TABLEVIEW;
            AnalysisTableViewModel tableViewModel = new AnalysisTableViewModel(analysisNode);
            tableViewNode.Tag = tableViewModel;
            analysisModel.TableViewModel = tableViewModel;

            //Map View Node
            TreeNode mapViewNode = analysisNode.Nodes.Add("Map View");
            mapViewNode.ImageIndex = mapViewNode.SelectedImageIndex = ICON_MAPVIEW;
            AnalysisMapViewModel mapViewModel = new AnalysisMapViewModel();
            mapViewNode.Tag = mapViewModel;
            analysisModel.MapViewModel = mapViewModel;

            //Report View Node
            TreeNode reportViewNode = analysisNode.Nodes.Add("Report View");
            reportViewNode.ImageIndex = reportViewNode.SelectedImageIndex = ICON_REPORTVIEW;
            AnalysisReportViewModel reportViewModel = new AnalysisReportViewModel(analysisNode);
            reportViewNode.Tag = reportViewModel;
            analysisModel.ReportViewModel = reportViewModel;

            analysisNode.ExpandAll();

            RenameForm renameForm = new RenameForm("Rename analysis name", analysisModel.Name);
            if (DialogResult.OK == renameForm.ShowDialog() && renameForm.Updated)
            {
                analysisNode.Text = renameForm.NewName;
                analysisModel.Name = renameForm.NewName;
            }
        }
        #endregion

        #region Analysis View Menu
        private void RenameAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalysisModel analysisModel = (AnalysisModel)WorkspaceTreeView.SelectedNode.Tag;
            RenameForm renameForm = new RenameForm("Rename analysis", analysisModel.Name);
            if (DialogResult.OK == renameForm.ShowDialog() && renameForm.Updated)
            {
                analysisModel.Name = renameForm.NewName;
                WorkspaceTreeView.SelectedNode.Text = renameForm.NewName;

                //analysisModel.TableViewModel.
                //analysisModel.MapViewModel.
                analysisModel.ReportViewModel.ReportForm.Text = "Report View - " + renameForm.NewName;
            }
        }

        private void RemoveAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalysisModel analysisModel = (AnalysisModel)WorkspaceTreeView.SelectedNode.Tag;
            if (DialogResult.Yes == MessageBox.Show("Are you sure remove the analysis view:\r\n'" + WorkspaceTreeView.SelectedNode.Text + "' ?",
                "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                WorkspaceTreeView.SelectedNode.Remove();
            }
        }
        #endregion

        private void NewMapViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapModel mapModel = new MapModel();
            mapModel.Name = "MapView-" + Utility.GetTimeStr();
            TreeNode mapNode = WorkspaceTreeView.SelectedNode.Nodes.Add(mapModel.Name);
            mapNode.Tag = mapModel;
            mapNode.SelectedImageIndex = mapNode.ImageIndex = ICON_SITES;
            mapNode.Parent.Expand();
            WorkspaceTreeView.SelectedNode = mapNode;

            RenameForm renameForm = new RenameForm("Rename map view name", mapModel.Name);
            if (DialogResult.OK == renameForm.ShowDialog() && renameForm.Updated)
            {
                mapNode.Text = renameForm.NewName;
                mapModel.Name = renameForm.NewName;
            }
        }

        private void RemoveMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapModel mapModel = (MapModel)WorkspaceTreeView.SelectedNode.Tag;
            if (DialogResult.No == MessageBox.Show("Are you sure remove the map view:\r\n'" + WorkspaceTreeView.SelectedNode.Text + "' ?",
                "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                return;
            }
            WorkspaceTreeView.SelectedNode.Remove();
        }

        private void RemoveEMIFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EMIFileModel emiFileModel = (EMIFileModel)WorkspaceTreeView.SelectedNode.Tag;
            if (DialogResult.No == MessageBox.Show("Are you sure remove the EMI file:\r\n'" + WorkspaceTreeView.SelectedNode.Text + "' ?",
                "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                return;
            }

            TreeNode parentNode = WorkspaceTreeView.SelectedNode.Parent;
            bool isLastNode = (parentNode.Nodes.Count == 1);
            WorkspaceTreeView.SelectedNode.Remove();
            if (isLastNode)
            {
                parentNode.Remove();
            }
        }

        private void RenameMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapModel mapModel = (MapModel)WorkspaceTreeView.SelectedNode.Tag;
            RenameForm renameForm = new RenameForm("Rename map view", mapModel.Name);
            if (DialogResult.OK == renameForm.ShowDialog() && renameForm.Updated)
            {
                mapModel.Name = renameForm.NewName;
                WorkspaceTreeView.SelectedNode.Text = renameForm.NewName;
            }
        }

        public void AddAssignedTaskNode(BindingTask task)
        {
            //Task Node
            TreeNode taskNode = mAssignedTasksNode.Nodes.Add(task.Name);
            taskNode.SelectedImageIndex = taskNode.ImageIndex = ICON_TASK;
            TaskModel taskModel = new TaskModel(task);
            taskNode.Tag = taskModel;

            //All EMI Files Node
            TreeNode emiFilesNode = taskNode.Nodes.Add("All EMI Files");
            emiFilesNode.SelectedImageIndex = emiFilesNode.ImageIndex = ICON_ALLEMIFILE;

            //Assigned Sites Node
            TreeNode assignedSiteNode = emiFilesNode.Nodes.Add("Assigned Sites");
            assignedSiteNode.SelectedImageIndex = assignedSiteNode.ImageIndex = ICON_SITES;

            //Assigned Site Node
            TreeNode siteNode;
            Site site;
            EMIFileData emiFileData;
            if (task.Site != null)
            {
                foreach (string siteID in task.Site)
                {
                    if (DataCenter.Instance().Sites.ContainsKey(siteID))
                        site = DataCenter.Instance().Sites[siteID];
                    else
                        site = new Site(siteID);
                    siteNode = assignedSiteNode.Nodes.Add(site.ToString());
                    siteNode.SelectedImageIndex = siteNode.ImageIndex = ICON_SITE;
                    siteNode.Tag = new SiteModel(site);

                    foreach (KeyValuePair<long, FileDescription> pair in DataCenter.Instance().EMIDescriptions)
                    {
                        //EMI Node
                        if (siteID.Equals(pair.Value.SiteID))
                        {
                            TreeNode emiNode = siteNode.Nodes.Add(pair.Value.Title);
                            emiNode.SelectedImageIndex = emiNode.ImageIndex = ICON_EMI;
                            emiFileData = DataCenter.Instance().EMIs[pair.Value.ID];
                            emiNode.Tag = new EMIFileModel(emiFileData);
                        }
                    }
                }
            }
            
            //Unassigned Sites Node
            TreeNode unAssignedSiteNode = emiFilesNode.Nodes.Add("Unassigned Sites");
            unAssignedSiteNode.SelectedImageIndex = unAssignedSiteNode.ImageIndex = ICON_UNASSIGNEDSITES;

            //UnAssigned Site Node
            if (task.UnassignedSite != null) 
            {
                foreach (string siteID in task.UnassignedSite)
                {
                    if (DataCenter.Instance().Sites.ContainsKey(siteID))
                        site = DataCenter.Instance().Sites[siteID];
                    else
                        site = new Site(siteID);
                    siteNode = unAssignedSiteNode.Nodes.Add(site.ToString());
                    siteNode.SelectedImageIndex = siteNode.ImageIndex = ICON_UNASSIGNEDSITE;
                    siteNode.Tag = new SiteModel(site);

                    foreach (KeyValuePair<long, FileDescription> pair in DataCenter.Instance().EMIDescriptions)
                    {
                        //EMI Node
                        if (siteID.Equals(pair.Value.SiteID))
                        {
                            TreeNode emiNode = siteNode.Nodes.Add(pair.Value.Title);
                            emiNode.SelectedImageIndex = emiNode.ImageIndex = ICON_UNASSIGNEDEMI;
                            emiFileData = DataCenter.Instance().EMIs[pair.Value.ID];
                            emiNode.Tag = new EMIFileModel(emiFileData);
                        }
                    }
                }
            }
            
            //All Task Analysis Node
            List<AnalysisModel> analysisModelList = new List<AnalysisModel>();
            taskModel.AnalysisModelList = analysisModelList;
            TreeNode allTaskAnalysisNode = taskNode.Nodes.Add("Task Analysis");
            allTaskAnalysisNode.ImageIndex = allTaskAnalysisNode.SelectedImageIndex = ICON_ANALYSISES;
            allTaskAnalysisNode.Tag = analysisModelList;
        }

        private void RegionAndTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void SiteManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SiteManagementForm siteManagementForm = new SiteManagementForm();
            siteManagementForm.ShowDialog();
        }

        private void ChannelSettingManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChannelSettingManagementForm channelManagementForm = new ChannelSettingManagementForm();
            channelManagementForm.ShowDialog();
        }

        private void LinkConfigurationManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkConfigurationManagementForm linkConfigurationMangementForm = new LinkConfigurationManagementForm();
            linkConfigurationMangementForm.ShowDialog();
        }

        private void EquipmentParameterManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EquipmentParameterManagementForm equipmentParameterManagementForm = new EquipmentParameterManagementForm();
            equipmentParameterManagementForm.ShowDialog();
        }

        private void TaskContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            TaskModel taskModel = (TaskModel)WorkspaceTreeView.SelectedNode.Tag;
            SubRegion subRegion = Utility.FindRegionByID(DataCenter.Instance().GlobalRegion.Root, taskModel.mTask.RegionID);
            if (subRegion != null && subRegion.Manager.IndexOf(HTTPAgent.Username) >= 0)
                RemoveTaskToolStripMenuItem.Enabled = true;
            else
                RemoveTaskToolStripMenuItem.Enabled = false;
        }

        private void RegionContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            SubRegion region = (SubRegion)WorkspaceTreeView.SelectedNode.Tag;

            AddTaskToolStripMenuItem.Enabled
                = region.Manager !=null && region.Manager.IndexOf(HTTPAgent.Username) >= 0 
                && (region.Sub == null || region.Sub.Count == 0);

            AddSubRegionToolStripMenuItem.Enabled
                = region.Manager != null && region.Manager.IndexOf(HTTPAgent.Username) >= 0;

            RenameRegionToolStripMenuItem.Enabled
                = RemoveRegionToolStripMenuItem.Enabled
                = HTTPAgent.Username.Equals(region.Owner);
        }

        private void ChannelContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            ChannelModel channelModel = (ChannelModel)WorkspaceTreeView.SelectedNode.Tag;
            if (channelModel.ID == -1)
            {
                SetChannelToolStripMenuItem.Text = "Set";
                ChannelDetailToolStripMenuItem.Visible = false;
                UnSetChannelToolStripMenuItem.Visible = false;
            }
            else
            {
                SetChannelToolStripMenuItem.Text = "Change";
                ChannelDetailToolStripMenuItem.Visible = true;
                UnSetChannelToolStripMenuItem.Visible = true;
            }
        }

        private void LinkContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            LinkModel linkModel = (LinkModel)WorkspaceTreeView.SelectedNode.Tag;
            if (linkModel.ID == -1)
            {
                SetLinkToolStripMenuItem.Text = "Set";
                LinkDetailToolStripMenuItem.Visible = false;
                UnSetLinkToolStripMenuItem.Visible = false;
            }
            else
            {
                SetLinkToolStripMenuItem.Text = "Change";
                LinkDetailToolStripMenuItem.Visible = true;
                UnSetLinkToolStripMenuItem.Visible = true;
            }
        }

        private void EquipmentContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            EquipmentModel equipmentModel = (EquipmentModel)WorkspaceTreeView.SelectedNode.Tag;
            if (equipmentModel.ID == -1)
            {
                SetEquipmentToolStripMenuItem.Text = "Set";
                EquipmentDetailToolStripMenuItem.Visible = false;
                UnSetEquipmentToolStripMenuItem.Visible = false;
            }
            else
            {
                SetEquipmentToolStripMenuItem.Text = "Change";
                EquipmentDetailToolStripMenuItem.Visible = true;
                UnSetEquipmentToolStripMenuItem.Visible = true;
            }
        }

        private void AddManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AssignUserForm assignUserForm = new AssignUserForm();
            assignUserForm.Prompt = "Add task manager";
            TreeNode managersNode = null;
            SubRegion origionRegion;
            if (DialogResult.OK == assignUserForm.ShowDialog())
            {
                managersNode = WorkspaceTreeView.SelectedNode;
                origionRegion = (SubRegion)managersNode.Parent.Tag;
                UpdateRegionRequest request = new UpdateRegionRequest();
                request.Type = UpdateRegionType.AddManager.ToString();
                request.Region = new SubRegion();
                request.Region.ID = origionRegion.ID;

                foreach (string user in assignUserForm.Users)
                {
                    if (origionRegion.Manager.Contains(user))
                        continue;

                    request.Region.Manager.Add(user);
                }

                if (request.Region.Manager.Count == 0)
                    return;

                HTTPAgent.instance().updateRegion(this, DataCenter.Instance().GlobalRegion.Version, request,
                    "Add managers", managersNode);
            }
        }

        private void RemoveManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string prompt = "Remove manager '" + WorkspaceTreeView.SelectedNode.Text + "', are you sure ?";
            if (DialogResult.No == MessageBox.Show(prompt, "Warning", MessageBoxButtons.YesNo))
                return;

            SubRegion origionRegion = (SubRegion)WorkspaceTreeView.SelectedNode.Parent.Parent.Tag;
            UpdateRegionRequest request = new UpdateRegionRequest();
            request.Type = UpdateRegionType.RemoveManager.ToString();
            request.Region = new SubRegion();
            request.Region.ID = origionRegion.ID;

            TreeNode managerNode = WorkspaceTreeView.SelectedNode;
            TreeNode managersNode = managerNode.Parent;
            string manager = managerNode.Text;
            if (!origionRegion.Manager.Contains(manager))
                return;

            request.Region.Manager.Add(manager);
            HTTPAgent.instance().updateRegion(this, DataCenter.Instance().GlobalRegion.Version, request,
                "Remove manager '" + manager + "'", managerNode);
        }

        private void ManagerDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode managerNode = WorkspaceTreeView.SelectedNode;
            User manager = DataCenter.Instance().Users[managerNode.Text];
            UserDetailForm userDetailForm = new UserDetailForm(manager);
            userDetailForm.AllowUpdate = false;
            userDetailForm.ShowDialog();
        }

        private void AddSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SiteManagementForm siteManagementForm = new SiteManagementForm();
            siteManagementForm.UseForSelect = true;

            TreeNode sitesNode = WorkspaceTreeView.SelectedNode;
            if (sitesNode.Parent.Tag is SubRegion)
            {
                if (siteManagementForm.ShowDialog() == DialogResult.Cancel)
                    return;

                List<Site> selectedSites = siteManagementForm.SelectedSites;
                if (selectedSites.Count == 0)
                    return;

                SubRegion originRegion = (SubRegion)sitesNode.Parent.Tag;
                UpdateRegionRequest request = new UpdateRegionRequest();
                request.Type = UpdateRegionType.AddSite.ToString();
                request.Region = new SubRegion();
                request.Region.ID = originRegion.ID;
                if (originRegion.Site == null)
                    originRegion.Site = new List<string>();
                foreach (Site selectedSite in selectedSites)
                {
                    if (originRegion.Site.Contains(selectedSite.SiteID))
                        continue;

                    request.Region.Site.Add(selectedSite.SiteID);
                }

                if (request.Region.Site.Count == 0)
                    return;

                HTTPAgent.instance().updateRegion(this, DataCenter.Instance().GlobalRegion.Version, request,
                    "Add region sites", sitesNode);
            }
            else if (sitesNode.Parent.Tag is TaskModel)
            {
                siteManagementForm.AvailableSites = Utility.GetAvailableSites((SubRegion)sitesNode.Parent.Parent.Tag);
                if (siteManagementForm.ShowDialog() == DialogResult.Cancel)
                    return;

                List<Site> selectedSites = siteManagementForm.SelectedSites;
                if (selectedSites.Count == 0)
                    return;

                BindingTask task = ((TaskModel)sitesNode.Parent.Tag).mTask;
                UpdateTaskRequest request = new UpdateTaskRequest();
                request.Type = UpdateTaskType.AddSite.ToString();
                foreach(Site selectedSite in selectedSites)
                {
                    if (task.Site.Contains(selectedSite.SiteID))
                        continue;

                    request.Site.Add(selectedSite.SiteID);
                }

                if (request.Site.Count == 0)
                    return;

                HTTPAgent.instance().updateTask(this, task.ID, request, "Add task sites", sitesNode);
            }
            
        }

        private void RemoveSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string prompt = "Remove site '" + WorkspaceTreeView.SelectedNode.Text + "', are you sure ?";
            if (DialogResult.No == MessageBox.Show(prompt, "Warning", MessageBoxButtons.YesNo))
                return;

            TreeNode siteNode = WorkspaceTreeView.SelectedNode;
            if (siteNode.Parent.Parent.Tag is SubRegion)
            {
                SubRegion origionRegion = (SubRegion)siteNode.Parent.Parent.Tag;
                UpdateRegionRequest request = new UpdateRegionRequest();
                request.Type = UpdateRegionType.RemoveSite.ToString();
                request.Region = new SubRegion();
                request.Region.ID = origionRegion.ID;

                string siteID = ((SiteModel)siteNode.Tag).Site.SiteID;
                if (!origionRegion.Site.Contains(siteID))
                    return;

                request.Region.Site.Add(siteID);
                HTTPAgent.instance().updateRegion(this, DataCenter.Instance().GlobalRegion.Version, request,
                    "Remove site '" + siteID + "'", siteNode);

            }
            else if (siteNode.Parent.Parent.Tag is TaskModel)
            {
                BindingTask task = ((TaskModel)siteNode.Parent.Parent.Tag).mTask;
                UpdateTaskRequest request = new UpdateTaskRequest();
                request.Type = UpdateTaskType.RemoveSite.ToString();

                string siteID = ((SiteModel)siteNode.Tag).Site.SiteID;
                if (!task.Site.Contains(siteID))
                    return;

                request.Site.Add(siteID);
                HTTPAgent.instance().updateTask(this, task.ID, request, "Remove site '" + siteID + "'", siteNode);
            }
        }

        private void SiteDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode siteNode = WorkspaceTreeView.SelectedNode;
            Site site = ((SiteModel)siteNode.Tag).Site;
            SiteDetailForm siteDetailForm = new SiteDetailForm(false);
            siteDetailForm.Site = site;
            siteDetailForm.AllowUpdate = false;
            siteDetailForm.ShowDialog();
        }

        private void SetChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChannelSettingManagementForm channelSettingManagmentForm = new ChannelSettingManagementForm();
            channelSettingManagmentForm.UseForSelect = true;
            if (channelSettingManagmentForm.ShowDialog() == DialogResult.Cancel)
                return;

            long selectedChannel = channelSettingManagmentForm.SelectedChannel;
            if (selectedChannel == -1)
                return;

            TreeNode channelNode = WorkspaceTreeView.SelectedNode;
            SubRegion originRegion = (SubRegion)channelNode.Parent.Parent.Tag;

            UpdateRegionRequest request = new UpdateRegionRequest();
            request.Type = UpdateRegionType.UpdateChanSettingID.ToString();
            request.Region = new SubRegion();
            request.Region.ID = originRegion.ID;
            request.Region.ChannelSettingID = selectedChannel;

            HTTPAgent.instance().updateRegion(this, DataCenter.Instance().GlobalRegion.Version, request,
                "Change channel id to '" + selectedChannel + "'", channelNode);
        }

        private void UnSetChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            long channelID = ((ChannelModel)WorkspaceTreeView.SelectedNode.Tag).ID;
            if (MessageBox.Show("Unset channel setting '" + DataCenter.Instance().ChannelSettingDescriptions[channelID].Title,
                "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            TreeNode channelNode = WorkspaceTreeView.SelectedNode;
            SubRegion originRegion = (SubRegion)channelNode.Parent.Parent.Tag;
            UpdateRegionRequest request = new UpdateRegionRequest();
            request.Type = UpdateRegionType.UpdateChanSettingID.ToString();
            request.Region = new SubRegion();
            request.Region.ID = originRegion.ID;
            request.Region.ChannelSettingID = -1;

            HTTPAgent.instance().updateRegion(this, DataCenter.Instance().GlobalRegion.Version, request,
                "Change channel id to '-1'", channelNode);
        }

        private void ChannelDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            long channelID = ((ChannelModel)WorkspaceTreeView.SelectedNode.Tag).ID;
            ChannelSettingDetailForm channelSettingForm = new ChannelSettingDetailForm(DataCenter.Instance().ChannelSettings[channelID]);
            channelSettingForm.ShowDialog();
        }

        private void SetLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkConfigurationManagementForm linkConfigurationManagmentForm = new LinkConfigurationManagementForm();
            linkConfigurationManagmentForm.UseForSelect = true;
            if (linkConfigurationManagmentForm.ShowDialog() == DialogResult.Cancel)
                return;

            long selectedLink = linkConfigurationManagmentForm.SelectedLink;
            if (selectedLink == -1)
                return;

            TreeNode linkNode = WorkspaceTreeView.SelectedNode;
            SubRegion originRegion = (SubRegion)linkNode.Parent.Parent.Tag;

            UpdateRegionRequest request = new UpdateRegionRequest();
            request.Type = UpdateRegionType.UpdateLinkConfigID.ToString();
            request.Region = new SubRegion();
            request.Region.ID = originRegion.ID;
            request.Region.LinkConfigurationID = selectedLink;

            HTTPAgent.instance().updateRegion(this, DataCenter.Instance().GlobalRegion.Version, request,
                "Change link id to '" + selectedLink + "'", linkNode);
        }

        private void UnSetLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            long linkID = ((LinkModel)WorkspaceTreeView.SelectedNode.Tag).ID;
            if (MessageBox.Show("Unset link setting '" + DataCenter.Instance().LinkConfigurationDescriptions[linkID].Title,
                "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            TreeNode linkNode = WorkspaceTreeView.SelectedNode;
            SubRegion originRegion = (SubRegion)linkNode.Parent.Parent.Tag;
            UpdateRegionRequest request = new UpdateRegionRequest();
            request.Type = UpdateRegionType.UpdateLinkConfigID.ToString();
            request.Region = new SubRegion();
            request.Region.ID = originRegion.ID;
            request.Region.LinkConfigurationID = -1;

            HTTPAgent.instance().updateRegion(this, DataCenter.Instance().GlobalRegion.Version, request,
                "Change link id to '-1'", linkNode);
        }

        private void LinkDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            long linkID = ((LinkModel)WorkspaceTreeView.SelectedNode.Tag).ID;
            LinkConfigurationDetailForm linkConfigurationForm = new LinkConfigurationDetailForm(DataCenter.Instance().LinkConfigurations[linkID]);
            linkConfigurationForm.ShowDialog();
        }

        private void SetEquipmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EquipmentParameterManagementForm equipmentManagmentForm = new EquipmentParameterManagementForm();
            equipmentManagmentForm.UseForSelect = true;
            if (equipmentManagmentForm.ShowDialog() == DialogResult.Cancel)
                return;

            long selectedEquipment = equipmentManagmentForm.SelectedEquipment;
            if (selectedEquipment == -1)
                return;

            TreeNode equipmentNode = WorkspaceTreeView.SelectedNode;
            SubRegion originRegion = (SubRegion)equipmentNode.Parent.Parent.Tag;

            UpdateRegionRequest request = new UpdateRegionRequest();
            request.Type = UpdateRegionType.UpdateEquipParamID.ToString();
            request.Region = new SubRegion();
            request.Region.ID = originRegion.ID;
            request.Region.EquipmentParameterID = selectedEquipment;

            HTTPAgent.instance().updateRegion(this, DataCenter.Instance().GlobalRegion.Version, request,
                "Change equipment id to '" + selectedEquipment + "'", equipmentNode);
        }

        private void UnSetEquipmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            long equipmentID = ((EquipmentModel)WorkspaceTreeView.SelectedNode.Tag).ID;
            if (MessageBox.Show("Unset equipment setting '" + DataCenter.Instance().EquipmentParameterDescriptions[equipmentID].Title,
                "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            TreeNode equipmentNode = WorkspaceTreeView.SelectedNode;
            SubRegion originRegion = (SubRegion)equipmentNode.Parent.Parent.Tag;
            UpdateRegionRequest request = new UpdateRegionRequest();
            request.Type = UpdateRegionType.UpdateEquipParamID.ToString();
            request.Region = new SubRegion();
            request.Region.ID = originRegion.ID;
            request.Region.EquipmentParameterID = -1;

            HTTPAgent.instance().updateRegion(this, DataCenter.Instance().GlobalRegion.Version, request,
                "Change equipment id to '-1'", equipmentNode);
        }

        private void EquipmentDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            long equipmentID = ((EquipmentModel)WorkspaceTreeView.SelectedNode.Tag).ID;
            EquipmentParameterDetailForm equipmentParameterForm
                = new EquipmentParameterDetailForm(DataCenter.Instance().EquipmentParameters[equipmentID]);
            equipmentParameterForm.ShowDialog();
        }

        private void AddTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SubRegion region = (SubRegion)WorkspaceTreeView.SelectedNode.Tag;
            if (Utility.GetAvailableSites(region).Count == 0)
            {
                MessageBox.Show("No available sites for this region !");
                return;
            }

            if (Utility.GetChannelSettingID(region) == -1)
            {
                MessageBox.Show("Channel Setting was not set !");
                return;
            }

            NewTaskForm taskForm = new NewTaskForm(region);
            taskForm.ShowDialog();

            ReLoad();
            if (taskForm.NewTask == null)
            {
                FocusNode(NodeType.Region, region.ID);
            }
            else
            {
                FocusNode(NodeType.Task, (object)taskForm.NewTask.ID);
            }
        }

        private void RenameRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldRegionName = WorkspaceTreeView.SelectedNode.Text;
            RenameForm renameForm = new RenameForm("Rename region", oldRegionName);
            renameForm.CheckInputDelegate = new CheckInputDelegate(CheckRegionName);
            if (DialogResult.OK == renameForm.ShowDialog() && renameForm.Updated)
            {
                UpdateRegionRequest request = new UpdateRegionRequest();
                request.Type = UpdateRegionType.RenameRegion.ToString();
                request.Region = new SubRegion();
                request.Region.Name = renameForm.NewName;
                request.Region.ID = ((SubRegion)WorkspaceTreeView.SelectedNode.Tag).ID;

                HTTPAgent.instance().updateRegion(this, DataCenter.Instance().GlobalRegion.Version, request,
                    "Rename region '" + oldRegionName + "'", WorkspaceTreeView.SelectedNode);
            }
        }

        private bool CheckRegionName(string regionName)
        {
            if (Utility.FindRegionByName(DataCenter.Instance().GlobalRegion.Root, regionName) != null)
            {
                MessageBox.Show("Region " + regionName + " already exists !");
                return false;
            }

            return true;
        }

        private bool CheckTaskName(string taskName)
        {
            if (Utility.FindTaskByName(DataCenter.Instance().GlobalRegion.Root, taskName) != null)
            {
                MessageBox.Show("Region '" + taskName + "' already exists !");
                return false;
            }

            return true;
        }

        private void AddSubRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SubRegion region = (SubRegion)WorkspaceTreeView.SelectedNode.Tag;
            if (region.Task.Count > 0)
            {
                MoveTaskForm moveTaskForm = new MoveTaskForm(WorkspaceTreeView.SelectedNode, region.Task.Count);
                moveTaskForm.ShowDialog();
                if (moveTaskForm.UserCancel)
                    return;

                if (!moveTaskForm.Success)
                {
                    ReLoad();
                    FocusNode(NodeType.Region, region.ID);
                    return;
                }
            }

            string regionName = "";
            if (DialogResult.Cancel == InputDialog.Show("Add region",
                "Input region name",
                ref regionName,
                new CheckInputDelegate(CheckRegionName)))
                return;

            UpdateRegionRequest request = new UpdateRegionRequest();
            request.Type = UpdateRegionType.AddRegion.ToString();
            request.Region = new SubRegion();
            request.Region.ParentID = ((SubRegion)WorkspaceTreeView.SelectedNode.Tag).ID;
            request.Region.Name = regionName;

            HTTPAgent.instance().updateRegion(this, DataCenter.Instance().GlobalRegion.Version, request, "Add region '" + regionName + "'", (SubRegion)WorkspaceTreeView.SelectedNode.Tag);
        }

        private void SiteContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            RemoveSiteToolStripMenuItem.Enabled = "Sites".Equals(WorkspaceTreeView.SelectedNode.Parent.Text);
        }

        private void AddAnalyzerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AssignUserForm assignUserForm = new AssignUserForm();
            assignUserForm.Prompt = "Add task analyzer";
            TreeNode analyzersNode = null;
            BindingTask task;
            if (DialogResult.OK == assignUserForm.ShowDialog())
            {
                analyzersNode = WorkspaceTreeView.SelectedNode;
                task = ((TaskModel)analyzersNode.Parent.Tag).mTask;
                UpdateTaskRequest request = new UpdateTaskRequest();
                request.Type = UpdateTaskType.AddAnalyzer.ToString();
                foreach (string user in assignUserForm.Users)
                {
                    if (request.Analyzer.Contains(user))
                        continue;

                    request.Analyzer.Add(user);
                }

                if (request.Analyzer.Count == 0)
                    return;

                HTTPAgent.instance().updateTask(this, task.ID, request, "Add analyzers", analyzersNode);
            }
        }

        private void RemoveAnalyzerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string prompt = "Remove analyzer '" + WorkspaceTreeView.SelectedNode.Text + "', are you sure ?";
            if (DialogResult.No == MessageBox.Show(prompt, "Warning", MessageBoxButtons.YesNo))
                return;

            BindingTask task = ((TaskModel)WorkspaceTreeView.SelectedNode.Parent.Parent.Tag).mTask;
            UpdateTaskRequest request = new UpdateTaskRequest();
            request.Type = UpdateTaskType.RemoveAnalyzer.ToString();

            TreeNode analyzerNode = WorkspaceTreeView.SelectedNode;
            string analyzer = analyzerNode.Text;
            if (!task.Analyzer.Contains(analyzer))
                return;

            request.Analyzer.Add(analyzer);
            HTTPAgent.instance().updateTask(this, task.ID, request, "Remove analyzer '" + analyzer + "'", analyzerNode);
        }

        private void AnalyzerDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode analyzerNode = WorkspaceTreeView.SelectedNode;
            User analyzer = DataCenter.Instance().Users[analyzerNode.Text];
            UserDetailForm userDetailForm = new UserDetailForm(analyzer);
            userDetailForm.AllowUpdate = false;
            userDetailForm.ShowDialog();
        }

        private void WorkspaceForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                //MessageBox.Show("Hide");
            }
        }

        private TreeNode GetRegionNode(TreeNode startNode, string regionID)
        {
            if (startNode.Tag != null && startNode.Tag is SubRegion)
            {
                SubRegion region = (SubRegion)startNode.Tag;
                if (regionID.Equals(region.ID))
                    return startNode;
            }

            TreeNode matchedNode;
            foreach (TreeNode node in startNode.Nodes)
            {
                matchedNode = GetRegionNode(node, regionID);
                if (matchedNode != null)
                    return matchedNode;
            }

            return null;
        }

        private TreeNode GetTaskNode(TreeNode startNode, long taskID)
        {
            if (startNode.Tag != null && startNode.Tag is TaskModel)
            {
                BindingTask task = ((TaskModel)startNode.Tag).mTask;
                if (task.ID == taskID)
                    return startNode;
            }

            TreeNode matchedNode;
            foreach (TreeNode node in startNode.Nodes)
            {
                matchedNode = GetTaskNode(node, taskID);
                if (matchedNode != null)
                    return matchedNode;
            }

            return null;
        }

        public void FocusNode(NodeType type, params object[] args)
        {
            SubRegion region;
            BindingTask task;
            TreeNode regionNode;
            TreeNode taskNode;
            TreeNode navigateNode;
            switch (type)
            {
                case NodeType.Region:
                case NodeType.RegionManagers:
                case NodeType.RegionManager:
                case NodeType.RegionSites: 
                case NodeType.RegionSite:
                case NodeType.RegionChannelSetting:
                case NodeType.RegionLinkConfiguration:
                case NodeType.RegionEquipmentParameter:
                {
                    region = Utility.FindRegionByID(DataCenter.Instance().GlobalRegion.Root, args[0].ToString());
                    if (region == null)
                        return;

                    regionNode = GetRegionNode(mGlobalNode, region.ID);
                    if (regionNode == null)
                        return;

                    navigateNode = regionNode;
                    if (type == NodeType.RegionManagers)
                        navigateNode = regionNode.Nodes[1];
                    else if (type == NodeType.RegionManager)
                    {
                        foreach (TreeNode managerNode in regionNode.Nodes[1].Nodes)
                        {
                            if (args[1].ToString().Equals(managerNode.Text))
                            {
                                navigateNode = managerNode;
                                break;
                            }
                        }
                    }
                    else if (type == NodeType.RegionSites)
                        navigateNode = regionNode.Nodes[2];
                    else if (type == NodeType.RegionSite)
                    {
                        foreach (TreeNode siteNode in regionNode.Nodes[2].Nodes)
                        {
                            if (args[1].ToString().Equals(((SiteModel)siteNode.Tag).Site.SiteID))
                            {
                                navigateNode = siteNode;
                                break;
                            }
                        }
                    }
                    else if (type == NodeType.RegionChannelSetting)
                        navigateNode = regionNode.Nodes[3].Nodes[0];
                    else if (type == NodeType.RegionLinkConfiguration)
                        navigateNode = regionNode.Nodes[3].Nodes[1];
                    else if (type == NodeType.RegionEquipmentParameter)
                        navigateNode = regionNode.Nodes[3].Nodes[2];

                    TreeNode focusNode = navigateNode;
                    navigateNode.Expand();
                    while (navigateNode.Parent != null)
                    {
                        navigateNode.Parent.Expand();
                        navigateNode = navigateNode.Parent;
                    }
                    WorkspaceTreeView.SelectedNode = focusNode;
                }

                break;

                case NodeType.Task:
                case NodeType.TaskSites:
                case NodeType.TaskSite:
                case NodeType.TaskAnalyzers:
                case NodeType.TaskAnalyzer:
                    {
                        long taskID = (long)args[0];
                        if (!DataCenter.Instance().Tasks.ContainsKey(taskID))
                            return;

                        task = DataCenter.Instance().Tasks[taskID];
                        taskNode = GetTaskNode(mGlobalNode, taskID);
                        if (taskNode == null)
                            return;

                        navigateNode = taskNode;
                        if (type == NodeType.TaskSites)
                            navigateNode = taskNode.Nodes[0];
                        else if (type == NodeType.TaskSite)
                        {
                            foreach (TreeNode siteNode in taskNode.Nodes[0].Nodes)
                            {
                                if (args[1].ToString().Equals(((SiteModel)siteNode.Tag).Site.SiteID))
                                {
                                    navigateNode = siteNode;
                                    break;
                                }
                            }
                        }
                        else if (type == NodeType.TaskAnalyzers)
                            navigateNode = taskNode.Nodes[1];
                        else if (type == NodeType.TaskAnalyzer)
                        {
                            foreach (TreeNode analyzerNode in taskNode.Nodes[1].Nodes)
                            {
                                if (args[1].ToString().Equals(analyzerNode.Text))
                                {
                                    navigateNode = analyzerNode;
                                    break;
                                }
                            }
                        }

                        TreeNode focusNode = navigateNode;
                        navigateNode.Expand();
                        while (navigateNode.Parent != null)
                        {
                            navigateNode.Parent.Expand();
                            navigateNode = navigateNode.Parent;
                        }

                        WorkspaceTreeView.SelectedNode = focusNode;
                    }
                break;
            }

        }

        private void RemoveRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string prompt = "Remove region '" + WorkspaceTreeView.SelectedNode.Text + "', are you sure ?";
            if (DialogResult.No == MessageBox.Show(prompt, "Warning", MessageBoxButtons.YesNo))
                return;

            UpdateRegionRequest request = new UpdateRegionRequest();
            request.Type = UpdateRegionType.RemoveRegion.ToString();
            request.Region = new SubRegion();
            request.Region.ParentID = ((SubRegion)WorkspaceTreeView.SelectedNode.Parent.Tag).ID;
            request.Region.ID = ((SubRegion)WorkspaceTreeView.SelectedNode.Tag).ID;

            HTTPAgent.instance().updateRegion(this, DataCenter.Instance().GlobalRegion.Version, request,
                "Remove region '" + WorkspaceTreeView.SelectedNode.Text + "'", WorkspaceTreeView.SelectedNode);
        }

        private void EmiDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EMIFileData data = ((EMIFileModel)WorkspaceTreeView.SelectedNode.Tag).mEmiData;
            EMIDetailForm emiDetailForm = new EMIDetailForm(data);
            emiDetailForm.ShowDialog();
        }



        void WorkspaceForm_onGetReportsFailed(long taskID, HttpStatusCode statusCode)
        {
            string taskName = DataCenter.Instance().Tasks[taskID].Name;
            MessageBox.Show("Get reports of task '" + taskName + "' failed, status = " + statusCode.ToString() + " !");
        }

        void WorkspaceForm_onGetReportsSuccessfully(long taskID, Reports reports, object context)
        {
            string taskName = DataCenter.Instance().Tasks[taskID].Name;
            if (reports == null || reports.Report == null || reports.Report.Count == 0)
            {
                MessageBox.Show("No report for task '" + taskName + "' !");
                return;
            }

            TaskReportListForm taskReportListForm = new TaskReportListForm(reports);
            taskReportListForm.ShowDialog();
        }

        private void RenameTableViewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void RemoveTableViewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void RemoveAnalysisToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void SortSiteByNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SiteNodeListSorter.SortByName(WorkspaceTreeView.SelectedNode);
        }

        private void SortSiteByTestTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void RemoveAllSitesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("You will remove all sites, are you sure ?", "Warning", MessageBoxButtons.YesNo);
            if (DialogResult.No == result)
                return;

            SiteModel siteModel;

            SubRegion origionRegion = (SubRegion)WorkspaceTreeView.SelectedNode.Parent.Tag;
            UpdateRegionRequest request = new UpdateRegionRequest();
            request.Type = UpdateRegionType.RemoveSite.ToString();
            request.Region = new SubRegion();
            request.Region.ID = origionRegion.ID;
            string siteID;
            foreach (TreeNode node in WorkspaceTreeView.SelectedNode.Nodes)
            {
                siteModel = (SiteModel)node.Tag;
                siteID = siteModel.Site.SiteID;
                
                if (!origionRegion.Site.Contains(siteID))
                    continue;

                request.Region.Site.Add(siteID);
            }

            HTTPAgent.instance().updateRegion(this, DataCenter.Instance().GlobalRegion.Version, request,
                "Remove all sites of region '" + origionRegion.Name + "'", null);
        }

        private void BatchedRemoveSitesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Site site;
            List<Site> sites = new List<Site>();
            foreach (TreeNode node in WorkspaceTreeView.SelectedNode.Nodes)
            {
                site = ((SiteModel)node.Tag).Site;
                sites.Add(site);
            }

            RemoveSiteSelectForm selectSiteForm = new RemoveSiteSelectForm();
            selectSiteForm.Sites = sites;
            DialogResult result = selectSiteForm.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            if (selectSiteForm.Sites.Count == 0)
                return;

            SubRegion origionRegion = (SubRegion)WorkspaceTreeView.SelectedNode.Parent.Tag;
            UpdateRegionRequest request = new UpdateRegionRequest();
            request.Type = UpdateRegionType.RemoveSite.ToString();
            request.Region = new SubRegion();
            request.Region.ID = origionRegion.ID;
            string siteID;

            foreach (Site removeSite in selectSiteForm.Sites)
            {
                siteID = removeSite.SiteID;

                if (!origionRegion.Site.Contains(siteID))
                    continue;

                request.Region.Site.Add(siteID);
            }

            result = MessageBox.Show("You will remove " + selectSiteForm.Sites.Count + " sites, are you sure ?", "Warning", MessageBoxButtons.YesNo);
            if (DialogResult.No == result)
                return;

            HTTPAgent.instance().updateRegion(this, DataCenter.Instance().GlobalRegion.Version, request,
                "Remove " + selectSiteForm.Sites.Count + " sites of region '" + origionRegion.Name + "'", null);
        }

        private void SitesContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            RemoveAllSitesToolStripMenuItem.Enabled
                = BatchedRemoveSitesToolStripMenuItem.Enabled
                = SortSiteByNameToolStripMenuItem.Enabled
                = (WorkspaceTreeView.SelectedNode.Nodes.Count > 0);
        }

        private void AdvancedAnalyzerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
