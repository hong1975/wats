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
using WatsEmiReportTool;
using WatsEMIAnalyzer.EMI;
using System.Diagnostics;

namespace WatsEMIAnalyzer
{
    public partial class LoadDataForm : Form
    {
        private LogInForm mLogInForm;
        private Stack<Task> mTaskStack = new Stack<Task>();

        public LoadDataForm(LogInForm logInForm)
        {
            InitializeComponent();
            mLogInForm = logInForm;
            DataCenter.Instance().Reset();
        }

        private void LoadDataForm_Load(object sender, EventArgs e)
        {
            HTTPAgent.instance().onGetGlobalRegionSuccessfully += new HTTPAgent.getGlobalRegionSuccessfully(LoadDataForm_onGetGlobalRegionSuccessfully);
            HTTPAgent.instance().onGetGlobalRegionFailed += new HTTPAgent.getGlobalRegionFailed(LoadDataForm_onGetGlobalRegionFailed);

            HTTPAgent.instance().onGetTasksSuccessfully += new HTTPAgent.getTasksSuccessfully(LoadDataForm_onGetTasksSuccessfully);
            HTTPAgent.instance().onGetTasksFailed += new HTTPAgent.getTasksFailed(LoadDataForm_onGetTasksFailed);

            HTTPAgent.instance().onGetUsersFailed += new HTTPAgent.getUsersFailed(LoadDataForm_onGetUsersFailed);
            HTTPAgent.instance().onGetUsersSuccessfully += new HTTPAgent.getUsersSuccessfully(LoadDataForm_onGetUsersSuccessfully);

            HTTPAgent.instance().onGetSitesSuccessfully += new HTTPAgent.getSitesSuccessfully(LoadDataForm_onGetSitesSuccessfully);
            HTTPAgent.instance().onGetSitesFailed += new HTTPAgent.getSitesFailed(LoadDataForm_onGetSitesFailed);

            HTTPAgent.instance().onGetFileListSuccessfully += new HTTPAgent.getFileListSuccessfully(LoadDataForm_onGetFileListSuccessfully);
            HTTPAgent.instance().onGetFileListFailed += new HTTPAgent.getFileListFailed(LoadDataForm_onGetFileListFailed);

            HTTPAgent.instance().onDownloadFileSuccessfully += new HTTPAgent.downloadFileSuccessfully(LoadDataForm_onDownloadFileSuccessfully);
            HTTPAgent.instance().onDownloadFileFailed += new HTTPAgent.downloadFileFailed(LoadDataForm_onDownloadFileFailed);

            if (HTTPAgent.IsAdmin)
            {
                mTaskStack.Push(new Task(TaskType.getUsers, HTTPAgent.FileType.unknown, -1, ""));
                mTaskStack.Push(new Task(TaskType.getRegion, HTTPAgent.FileType.unknown, -1, ""));
            }
            else
            {
                mTaskStack.Push(new Task(TaskType.getTasks, HTTPAgent.FileType.unknown, -1, ""));
                mTaskStack.Push(new Task(TaskType.getFileList, HTTPAgent.FileType.emi, -1, ""));
                mTaskStack.Push(new Task(TaskType.getFileList, HTTPAgent.FileType.linkconfiguration, -1, ""));
                mTaskStack.Push(new Task(TaskType.getFileList, HTTPAgent.FileType.equipmentParameter, -1, ""));
                mTaskStack.Push(new Task(TaskType.getFileList, HTTPAgent.FileType.channelSetting, -1, ""));
                mTaskStack.Push(new Task(TaskType.getSites, HTTPAgent.FileType.unknown, -1, ""));
                mTaskStack.Push(new Task(TaskType.getUsers, HTTPAgent.FileType.unknown, -1, ""));
                mTaskStack.Push(new Task(TaskType.getRegion, HTTPAgent.FileType.unknown, -1, ""));
            }
        }

        private void LoadDataForm_Shown(object sender, EventArgs e)
        {
            ExecuteTask();
        }

        void LoadDataForm_onGetGlobalRegionFailed(System.Net.HttpStatusCode statusCode)
        {
            MessageBox.Show("Get global region failed, status code = " + statusCode.ToString() + " !");
            ExecuteTask();
        }

        void LoadDataForm_onGetGlobalRegionSuccessfully(GlobalRegion globalRegion)
        {
            
            DataCenter.Instance().GlobalRegion = globalRegion;
            ExecuteTask();
        }

        void LoadDataForm_onGetTasksFailed(System.Net.HttpStatusCode statusCode)
        {
            MessageBox.Show("Get tasks failed, status code = " + statusCode.ToString() + " !");
            ExecuteTask();
        }

        void LoadDataForm_onGetTasksSuccessfully(Tasks tasks)
        {
            try
            {
                HashSet<string> sites = new HashSet<string>();

                foreach (BindingTask task in tasks.Task)
                {
                    Debug.WriteLine("Task ID:" + task.ID + ", name=" + task.Name);
                    DataCenter.Instance().Tasks.Add(task.ID, task);

                    foreach (string site in task.UnassignedSite)
                    {
                        sites.Add(site);
                    }

                    foreach (string site in task.Site)
                    {
                        sites.Add(site);
                    }
                }

                foreach (KeyValuePair<long, FileDescription> pair in DataCenter.Instance().EMIDescriptions)
                {
                    if (sites.Contains(pair.Value.SiteID)
                        && !DataCenter.Instance().EMIs.ContainsKey(pair.Key) //如果EMI文件不存在
                        )
                    {
                        mTaskStack.Push(new Task(TaskType.getFile, HTTPAgent.FileType.emi,
                            pair.Key, pair.Value.Title));
                    }
                }
            }
            catch (System.Exception e)
            {
            	
            }
            
            ExecuteTask();
        }

        void LoadDataForm_onGetUsersSuccessfully(Users users)
        {
            foreach (User user in users.User)
            {
                DataCenter.Instance().Users[user.userId] = user;
            }
            ExecuteTask();

        }

        void LoadDataForm_onGetUsersFailed(System.Net.HttpStatusCode statusCode)
        {
            ExecuteTask();
        }

        private void UpdateProgress(string text)
        {
            ProgressLabel.Text = text;
            ProgressLabel.Invalidate();
        }

        private void ExecuteTask()
        {
            if (mTaskStack.Count == 0)
            {
                if (HTTPAgent.IsAdmin)
                {
                    AdminForm adminForm = new AdminForm(mLogInForm);
                    adminForm.Show();
                }
                else
                {
                    MDIForm mdiForm = new MDIForm(mLogInForm);
                    mdiForm.Show();
                }
                
                Close();
            }
            else
            {
                Task curTask = mTaskStack.Pop();
                switch (curTask.mType)
                {
                    case TaskType.getRegion:
                        UpdateProgress("Get Region ...");
                        HTTPAgent.instance().getGlobalRegion(this);
                        break;

                    case TaskType.getTasks:
                        UpdateProgress("Get Tasks ...");
                        HTTPAgent.instance().getTasks(this);
                        break;

                    case TaskType.getUsers:
                        UpdateProgress("Get Users ...");
                        HTTPAgent.instance().getUsers(this);
                        break;

                    case TaskType.getSites:
                        UpdateProgress("Get Site ...");
                        HTTPAgent.instance().getSites(this);
                        break;

                    case TaskType.getFileList:
                        switch(curTask.mFileType)
                        {
                            case HTTPAgent.FileType.channelSetting:
                                UpdateProgress("Get Channel Setting file list ...");
                                break;

                            case HTTPAgent.FileType.colorSetting:
                                UpdateProgress("Get Color Setting file list ...");
                                break;

                            case HTTPAgent.FileType.equipmentParameter:
                                UpdateProgress("Get Equipment Parameter file list ...");
                                break;

                            case HTTPAgent.FileType.linkconfiguration:
                                UpdateProgress("Get Link Configuration file list ...");
                                break;

                            case HTTPAgent.FileType.emi:
                                UpdateProgress("Get EMI file list ...");
                                break;
                        }
                        HTTPAgent.instance().getFileList(this, curTask.mFileType);
                        break;
                        

                    case TaskType.getFile:
                        {
                            UpdateProgress("Get file '" + curTask.mFileTitle + "' ...");
                            HTTPAgent.instance().downloadFile(this, curTask.mFileType, curTask.mFileId, curTask.mFileTitle, HTTPAgent.DownloadType.obj, null);
                        }
                        break;
                }
            }
        }

        void LoadDataForm_onDownloadFileSuccessfully(HTTPAgent.FileType type, long fid, string title, HTTPAgent.DownloadType downloadType, byte[] content)
        {
            switch(type)
            {
                case HTTPAgent.FileType.channelSetting:
                    DataCenter.Instance().ChannelSettings[fid] = Utility.Deserailize <List<ChannelSetting>>(content);
                    break;

                case HTTPAgent.FileType.colorSetting:
                    break;

                case HTTPAgent.FileType.equipmentParameter:
                    DataCenter.Instance().EquipmentParameters[fid] = Utility.Deserailize<Dictionary<string, EquipmentParameter>>(content);
                    break;

                case HTTPAgent.FileType.linkconfiguration:
                    DataCenter.Instance().LinkConfigurations[fid] = Utility.Deserailize<List<LinkConfiguration>>(content);
                    break;

                case HTTPAgent.FileType.emi:
                    DataCenter.Instance().EMIs[fid] = Utility.Deserailize<EMIFileData>(content);
                    break;
            }

            ExecuteTask();
        }

        void LoadDataForm_onDownloadFileFailed(HTTPAgent.FileType type, long fid, string title, HTTPAgent.DownloadType downloadType, System.Net.HttpStatusCode statusCode)
        {
            switch (type)
            {
                case HTTPAgent.FileType.channelSetting:
                    if (DataCenter.Instance().ChannelSettingDescriptions.ContainsKey(fid))
                        DataCenter.Instance().ChannelSettingDescriptions.Remove(fid);
                    break;

                case HTTPAgent.FileType.colorSetting:
                    break;

                case HTTPAgent.FileType.equipmentParameter:
                    if (DataCenter.Instance().EquipmentParameterDescriptions.ContainsKey(fid))
                        DataCenter.Instance().EquipmentParameterDescriptions.Remove(fid);
                    break;

                case HTTPAgent.FileType.linkconfiguration:
                    if (DataCenter.Instance().LinkConfigurationDescriptions.ContainsKey(fid))
                        DataCenter.Instance().LinkConfigurationDescriptions.Remove(fid);
                    break;
            }

            MessageBox.Show("Can't download " + type.ToString() + " file '" + title + "', status code = " + statusCode.ToString());
            ExecuteTask();
        }

        void LoadDataForm_onGetFileListFailed(HTTPAgent.FileType type, System.Net.HttpStatusCode statusCode)
        {
            MessageBox.Show("Can't download " + type.ToString() + " file list !");
            ExecuteTask();
        }

        void LoadDataForm_onGetFileListSuccessfully(HTTPAgent.FileType type, FileList fileList)
        {
            if (fileList != null && fileList.Description != null)
            {
                for (int i = fileList.Description.Count - 1; i >= 0; i--)
                {
                    switch (type)
                    {
                        case HTTPAgent.FileType.channelSetting:
                            DataCenter.Instance().ChannelSettingDescriptions[fileList.Description[i].ID] = fileList.Description[i];
                            break;

                        case HTTPAgent.FileType.equipmentParameter:
                            DataCenter.Instance().EquipmentParameterDescriptions[fileList.Description[i].ID] = fileList.Description[i];
                            break;

                        case HTTPAgent.FileType.linkconfiguration:
                            DataCenter.Instance().LinkConfigurationDescriptions[fileList.Description[i].ID] = fileList.Description[i];
                            break;

                        case HTTPAgent.FileType.emi:
                            DataCenter.Instance().EMIDescriptions[fileList.Description[i].ID] = fileList.Description[i];
                            break;
                    }

                    if (type != HTTPAgent.FileType.emi)
                    {
                        mTaskStack.Push(new Task(TaskType.getFile, type, fileList.Description[i].ID, fileList.Description[i].Title));
                    }
                }
            }

            ExecuteTask();                
        }

        void LoadDataForm_onGetSitesFailed(System.Net.HttpStatusCode statusCode)
        {
            mTaskStack.Clear();
            MessageBox.Show("Can't get site information, error = " + statusCode.ToString());
            mLogInForm.Show();
            Close();
        }

        void LoadDataForm_onGetSitesSuccessfully(WatsEMIAnalyzer.Bindings.Sites sites)
        {
            if (sites != null)
            {
                foreach (Site site in sites.Site)
                    DataCenter.Instance().Sites[site.SiteID] = site;
            }

            ExecuteTask();
        }

        private void LoadDataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            HTTPAgent.instance().onGetTasksSuccessfully -= new HTTPAgent.getTasksSuccessfully(LoadDataForm_onGetTasksSuccessfully);
            HTTPAgent.instance().onGetTasksFailed -= new HTTPAgent.getTasksFailed(LoadDataForm_onGetTasksFailed);

            HTTPAgent.instance().onDownloadFileSuccessfully -= new HTTPAgent.downloadFileSuccessfully(LoadDataForm_onDownloadFileSuccessfully);
            HTTPAgent.instance().onDownloadFileFailed -= new HTTPAgent.downloadFileFailed(LoadDataForm_onDownloadFileFailed);

            HTTPAgent.instance().onGetUsersFailed -= new HTTPAgent.getUsersFailed(LoadDataForm_onGetUsersFailed);
            HTTPAgent.instance().onGetUsersSuccessfully -= new HTTPAgent.getUsersSuccessfully(LoadDataForm_onGetUsersSuccessfully);

            HTTPAgent.instance().onGetSitesSuccessfully -= new HTTPAgent.getSitesSuccessfully(LoadDataForm_onGetSitesSuccessfully);
            HTTPAgent.instance().onGetSitesFailed -= new HTTPAgent.getSitesFailed(LoadDataForm_onGetSitesFailed);

            HTTPAgent.instance().onGetFileListSuccessfully -= new HTTPAgent.getFileListSuccessfully(LoadDataForm_onGetFileListSuccessfully);
            HTTPAgent.instance().onGetFileListFailed -= new HTTPAgent.getFileListFailed(LoadDataForm_onGetFileListFailed);

            HTTPAgent.instance().onGetGlobalRegionSuccessfully -= new HTTPAgent.getGlobalRegionSuccessfully(LoadDataForm_onGetGlobalRegionSuccessfully);
            HTTPAgent.instance().onGetGlobalRegionFailed -= new HTTPAgent.getGlobalRegionFailed(LoadDataForm_onGetGlobalRegionFailed);
        }
    }
}
