using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Threading;
using WatsEMIAnalyzer.Bindings;

namespace WatsEMIAnalyzer.HTTP
{
    class HTTPAgent
    {
        private static HTTPAgent mHTTPAgent;
        private static CookieContainer mCookieContainer = new CookieContainer();
        private static string mServerHost;
        private static string mUsername;
        private static string mPassword;
        private static string mProxy;
        private static bool mIsAdmin = false;

        /* login */
        public delegate void loginSuccessfully(User self);
        public event loginSuccessfully onLoginSuccessfully;
        public delegate void loginFailed(string error);
        public event loginFailed onLoginFailed;

        /* logout */
        public delegate void logoutSuccessfully();
        public event logoutSuccessfully onLogoutSuccessfully;
        public delegate void logoutFailed(HttpStatusCode statusCode);
        public event logoutFailed onLogoutFailed;

        /* get regions */
        public delegate void getGlobalRegionSuccessfully(GlobalRegion globalRegion);
        public event getGlobalRegionSuccessfully onGetGlobalRegionSuccessfully;
        public delegate void getGlobalRegionFailed(HttpStatusCode statusCode);
        public event getGlobalRegionFailed onGetGlobalRegionFailed;

        /* update region */
        public delegate void updateRegionSuccessfully(UpdateRegionResult result, object context);
        public event updateRegionSuccessfully onUpdateRegionSuccessfully;
        public delegate void updateRegionFailed(UpdateRegionRequest request, HttpStatusCode statusCode, string messageText);
        public event updateRegionFailed onUpdateRegionFailed;

        /* get sites */
        public delegate void getSitesSuccessfully(Sites sites);
        public event getSitesSuccessfully onGetSitesSuccessfully;
        public delegate void getSitesFailed(HttpStatusCode statusCode);
        public event getSitesFailed onGetSitesFailed;

        /* add sites */
        public delegate void addSitesSuccessfully(Sites addedSites);
        public event addSitesSuccessfully onAddSitesSuccessfully;
        public delegate void addSitesFailed(HttpStatusCode statusCode);
        public event addSitesFailed onAddSitesFailed;

        /* remove sites */
        public delegate void removeSitesSuccessfully(string siteId);
        public event removeSitesSuccessfully onRemoveSitesSuccessfully;
        public delegate void removeSitesFailed(HttpStatusCode statusCode, string siteId);
        public event removeSitesFailed onRemoveSitesFailed;

        /* get file list*/
        public delegate void getFileListSuccessfully(FileType type, FileList fileList);
        public event getFileListSuccessfully onGetFileListSuccessfully;
        public delegate void getFileListFailed(FileType type, HttpStatusCode statusCode);
        public event getFileListFailed onGetFileListFailed;

        /* get tasks */
        public delegate void getTasksSuccessfully(Tasks tasks);
        public event getTasksSuccessfully onGetTasksSuccessfully;
        public delegate void getTasksFailed(HttpStatusCode statusCode);
        public event getTasksFailed onGetTasksFailed;

        /* add tasks */
        public delegate void addTaskSuccessfully(int RegionVersion, BindingTask task);
        public event addTaskSuccessfully onAddTaskSuccessfully;
        public delegate void addTaskFailed(BindingTask task, HttpStatusCode statusCode);
        public event addTaskFailed onAddTaskFailed;

        /* update task */
        public delegate void updateTaskSuccessfully(long taskID, UpdateTaskRequest request, object context);
        public event updateTaskSuccessfully onUpdateTaskSuccessfully;
        public delegate void updateTaskFailed(long taskID, UpdateTaskRequest request, HttpStatusCode statusCode, string messageText);
        public event updateTaskFailed onUpdateTaskFailed;

        /* remove task */
        public delegate void removeTaskSuccessfully(long taskID, object context);
        public event removeTaskSuccessfully onRemoveTaskSuccessfully;
        public delegate void removeTaskFailed(long taskID, HttpStatusCode statusCode, string messageText, object context);
        public event removeTaskFailed onRemoveTaskFailed;

        /* get users */
        public delegate void getUsersSuccessfully(Users users);
        public event getUsersSuccessfully onGetUsersSuccessfully;
        public delegate void getUsersFailed(HttpStatusCode statusCode);
        public event getUsersFailed onGetUsersFailed;

        /* add user */
        public delegate void addUserSuccessfully(User user);
        public event addUserSuccessfully onAddUserSuccessfully;
        public delegate void addUserFailed(string user, HttpStatusCode statusCode);
        public event addUserFailed onAddUserFailed;

        /* update user */
        public delegate void updateUserSuccessfully(string user, UpdateUserType type, string newValue);
        public event updateUserSuccessfully onUpdateUserSuccessfully;
        public delegate void updateUserFailed(string user, UpdateUserType type, HttpStatusCode statusCode);
        public event updateUserFailed onUpdateUserFailed;

        /* remove user */
        public delegate void removeUserSuccessfully(string user);
        public event removeUserSuccessfully onRemoveUserSuccessfully;
        public delegate void removeUserFailed(string user, HttpStatusCode statusCode);
        public event removeUserFailed onRemoveUserFailed;

        /* upload file */
        public delegate void uploadFileSuccessfully(FileType type, FileDescription description, byte[] parseData);
        public event uploadFileSuccessfully onUploadFileSuccessfully;
        public delegate void uploadFileFailed(string fileName, HttpStatusCode statusCode, ErrorMessage errorMessage);
        public event uploadFileFailed onUploadFileFailed;

        /* download file */
        public delegate void downloadFileSuccessfully(FileType type, long fid, string title, DownloadType downloadType, byte[] content);
        public event downloadFileSuccessfully onDownloadFileSuccessfully;
        public delegate void downloadFileFailed(FileType type, long fid, string title, DownloadType downloadType, HttpStatusCode statusCode);
        public event downloadFileFailed onDownloadFileFailed;

        /* remove file */
        public delegate void removeFileSuccessfully(FileType type, long fid, string title);
        public event removeFileSuccessfully onRemoveFileSuccessfully;
        public delegate void removeFileFileFailed(FileType type, long fid, string title, HttpStatusCode statusCode);
        public event removeFileFileFailed onRemoveFileFailed;

        /* get reports */
        public delegate void getReportsSuccessfully(long taskID, Reports reports, object context);
        public event getReportsSuccessfully onGetReportsSuccessfully;
        public delegate void getReportsFailed(long taskID, HttpStatusCode statusCode);
        public event getReportsFailed onGetReportsFailed;

        /* upload report */
        public delegate void uploadReportSuccessfully(long taskID, Report report, object context);
        public event uploadReportSuccessfully onUploadReportSuccessfully;
        public delegate void uploadReportFailed(long taskID, HttpStatusCode statusCode);
        public event uploadReportFailed onUploadReportFailed;

        public enum DownloadType
        {
            obj,
            content
        }

        enum Action
        {
            login,
            logout,

            getglobalregion,
            updateregion,

            adduser,
            getusers,
            updateuser,
            removeuser,

            getfilelist,
            uploadfile,
            downloadfile,
            removefile,

            addsites,
            removesites,
            getsites,
            
            gettasks,
            createtask,
            removetask,
            renametask,
            updatetask,

            uploadreport,
            getreports
        }

        public enum FileType
        {
            unknown,
            emi,
            site,
            colorSetting,
            channelSetting,
            equipmentParameter,
            linkconfiguration
        }

        public static string UserAgent
        {
            get { return Constants.UserAgent; }
        }

        public static string ServerHost
        {
            set { mServerHost = value; }
        }
        
        public static string Username
        {
            set { mUsername = value; }
            get { return mUsername; }
        }

        public static bool IsAdmin
        {
            get { return mIsAdmin; }
        }

        public static string Password
        {
            set { mPassword = value; }
            get { return mPassword; }
        }

        public static string Proxy
        {
            set { mProxy = value; }
            get { return mProxy; }
        }

        public static CookieContainer CookieContainer
        {
            get { return mCookieContainer; }
        }

        static HTTPAgent()
        {
            ServicePointManager.DefaultConnectionLimit = 500;
        }

        public static HTTPAgent instance()
        {
            if (mHTTPAgent == null)
            {
                mHTTPAgent = new HTTPAgent();
            }
            return mHTTPAgent;
        }

        private HTTPAgent()
        {            
        }

        public void logIn(Form attachedForm)
        {
            new Thread(delegate()
            {
                string json = "";
                byte[] respData;
                WebHeaderCollection headers;
                HttpStatusCode statusCode = HTTPRequest.MakeRequest(getUrl(Action.login), "POST",
                    Constants.JSON_MIME, null,
                    null, out respData, out headers);
                if (statusCode == HttpStatusCode.OK)
                {
                    if (onLoginSuccessfully != null)
                    {
                        User self = null;
                        if (respData != null)
                        {
                            json = Encoding.UTF8.GetString(respData);
                            self = Utility.JsonDeserialize<User>(json);
                            attachedForm.BeginInvoke(onLoginSuccessfully, self);
                            mIsAdmin = ("admin".Equals(self.role, StringComparison.OrdinalIgnoreCase));
                        }
                        else
                        {
                            string reason = "Can't get self info !";
                            attachedForm.BeginInvoke(onLoginFailed, reason);
                        }
                        
                    }
                }
                else
                {
                    string reason = "status code = " + statusCode.ToString();
                    if (statusCode == HttpStatusCode.Unauthorized)
                        reason = "incorrect user or password";
                    
                    if (onLoginFailed != null)
                        attachedForm.BeginInvoke(onLoginFailed, reason);
                }

            }).Start();
        }

        public void logOut(Form attachedForm)
        {
            new Thread(delegate()
            {
                byte[] respData;
                WebHeaderCollection headers;
                HttpStatusCode statusCode = HTTPRequest.MakeRequest(getUrl(Action.login), "DELETE",
                    null, null,
                    null, out respData, out headers);
                if (statusCode == HttpStatusCode.NoContent)
                {
                    if (onLogoutSuccessfully != null)
                        attachedForm.BeginInvoke(onLogoutSuccessfully);
                }
                else
                {
                    if (onLoginFailed != null)
                        attachedForm.BeginInvoke(onLogoutFailed, statusCode);
                }

            }).Start();
        }

        public void getGlobalRegion(Form attachedForm)
        {
            new Thread(delegate()
            {
                byte[] respData;
                string json = "";
                WebHeaderCollection headers;
                string url = getUrl(Action.getglobalregion);
                HttpStatusCode statusCode = HTTPRequest.MakeRequest(url, "GET",
                    Constants.JSON_MIME, null,
                    null, out respData, out headers);
                if (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.NoContent)
                {
                    if (onGetGlobalRegionSuccessfully != null)
                    {
                        GlobalRegion globalRegion = null;// = new GlobalRegion();
                        if (respData != null)
                        {
                            json = Encoding.UTF8.GetString(respData);
                            globalRegion = Utility.JsonDeserialize<GlobalRegion>(json);
                            Utility.FillGlobalRegion(globalRegion);
                        }
                        attachedForm.BeginInvoke(onGetGlobalRegionSuccessfully, globalRegion);
                    }
                }
                else
                {
                    if (onGetGlobalRegionFailed != null)
                        attachedForm.BeginInvoke(onGetGlobalRegionFailed, statusCode);
                }

            }).Start();
        }

        public void updateRegion(Form attachedForm, int curVersion, UpdateRegionRequest request, string messageText, object context)
        {
            new Thread(delegate()
            {
                byte[] respData;
                string json = "";
                WebHeaderCollection headers;
                string url = getUrl(Action.updateregion) + "/" + curVersion;
                HttpStatusCode statusCode = HTTPRequest.MakeRequest(url, "PUT",
                    Constants.JSON_MIME, Constants.JSON_MIME,
                    Encoding.UTF8.GetBytes(Utility.JsonSerialize<UpdateRegionRequest>(request)), out respData, out headers);
                if (statusCode == HttpStatusCode.OK)
                {
                    if (onUpdateRegionSuccessfully != null)
                    {
                        UpdateRegionResult result;
                        if (respData != null)
                        {
                            json = Encoding.UTF8.GetString(respData);

                            result = Utility.JsonDeserialize<UpdateRegionResult>(json);
                            Utility.FillUpdateRegionResult(result);
                            attachedForm.BeginInvoke(onUpdateRegionSuccessfully, result, context);
                        }
                    }
                }
                else
                {
                    if (onUpdateRegionFailed != null)
                    {
                        attachedForm.BeginInvoke(onUpdateRegionFailed, request, statusCode, messageText);
                    }
                }

            }).Start();
        }

        public void getUsers(Form attachedForm)
        {
            new Thread(delegate()
            {
                byte[] respData;
                string json = "";
                WebHeaderCollection headers;
                string url = getUrl(Action.getusers);
                HttpStatusCode statusCode = HTTPRequest.MakeRequest(url, "GET",
                    Constants.JSON_MIME, null,
                    null, out respData, out headers);
                if (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.NoContent)
                {
                    if (onGetUsersSuccessfully != null)
                    {
                        Users users = new Users();
                        if (respData != null)
                        {
                            json = Encoding.UTF8.GetString(respData);
                            users = Utility.JsonDeserialize<Users>(json);
                            Utility.FillUsers(users);
                        }
                        attachedForm.BeginInvoke(onGetUsersSuccessfully, users);
                    }
                }
                else
                {
                    if (onGetUsersSuccessfully != null)
                        attachedForm.BeginInvoke(onGetUsersSuccessfully, statusCode);
                }

            }).Start();
        }

        public void addUser(Form attachedForm, string userId, string role)
        {
            new Thread(delegate()
            {
                User user = new User();
                user.userId = userId;
                user.locked = false;
                user.role = role;
                
                byte[] respData;
                WebHeaderCollection headers;
                HttpStatusCode statusCode = HTTPRequest.MakeRequest(getUrl(Action.adduser), "POST",
                    null, Constants.JSON_MIME,
                    Encoding.UTF8.GetBytes(Utility.JsonSerialize<User>(user)), out respData, out headers);
                if (statusCode == HttpStatusCode.Accepted)
                {
                    if (onAddUserSuccessfully != null)
                    {
                        attachedForm.BeginInvoke(onAddUserSuccessfully, user);
                    }
                }
                else
                {
                    if (onAddUserFailed != null)
                        attachedForm.BeginInvoke(onAddUserFailed, userId, statusCode);
                }
            }).Start();
        }

        public void updateUser(Form attachedForm, string userId, UpdateUserType type, string newValue)
        {
            new Thread(delegate()
            {
                byte[] respData;
                WebHeaderCollection headers;
                string url = getUrl(Action.updateuser) + "/" + userId + "/" + type.ToString() + "/" + newValue;
                HttpStatusCode statusCode = HTTPRequest.MakeRequest(url, "PUT",
                    null, null,
                    null, out respData, out headers);
                if (statusCode == HttpStatusCode.Accepted)
                {
                    if (onUpdateUserSuccessfully != null)
                        attachedForm.BeginInvoke(onUpdateUserSuccessfully, userId, type, newValue);
                }
                else
                {
                    if (onUpdateUserFailed != null)
                        attachedForm.BeginInvoke(onUpdateUserFailed, userId, type, statusCode);
                }

            }).Start();
        }

        public void removeUser(Form attachedForm, string userId)
        {
            new Thread(delegate()
            {
                byte[] respData;
                WebHeaderCollection headers;
                string url = getUrl(Action.removeuser) + "/" + userId;
                HttpStatusCode statusCode = HTTPRequest.MakeRequest(url, "DELETE",
                    null, null,
                    null, out respData, out headers);
                if (statusCode == HttpStatusCode.NoContent)
                {
                    if (onRemoveUserSuccessfully != null)
                        attachedForm.BeginInvoke(onRemoveUserSuccessfully, userId);
                }
                else
                {
                    if (onRemoveUserFailed != null)
                        attachedForm.BeginInvoke(onRemoveUserFailed, userId, statusCode);
                }

            }).Start();
        }

        public void getSites(Form attachedForm)
        {
            new Thread(delegate()
            {
                byte[] respData;
                string json = "";
                WebHeaderCollection headers;
                string url = getUrl(Action.getsites);
                HttpStatusCode statusCode = HTTPRequest.MakeRequest(url, "GET",
                    Constants.JSON_MIME, null,
                    null, out respData, out headers);
                if (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.NoContent)
                {
                    if (onGetSitesSuccessfully != null)
                    {
                        Sites sites = new Sites();
                        if (respData != null)
                        {
                            json = Encoding.UTF8.GetString(respData);
                            sites = Utility.JsonDeserialize<Sites>(json);
                            Utility.FillSites(sites);
                        }
                        attachedForm.BeginInvoke(onGetSitesSuccessfully, sites);
                    }
                }
                else
                {
                    if (onGetSitesFailed != null)
                        attachedForm.BeginInvoke(onGetSitesFailed, statusCode);
                }

            }).Start();
        }

        public void addSites(Form attachedForm, Sites sites)
        {
            new Thread(delegate()
            {
                byte[] respData;
                WebHeaderCollection headers;
                string url = getUrl(Action.getsites);
                string str = Utility.JsonSerialize<Sites>(sites);
                HttpStatusCode statusCode = HTTPRequest.MakeRequest(url, "POST",
                    null, Constants.JSON_MIME,
                    Encoding.UTF8.GetBytes(Utility.JsonSerialize<Sites>(sites)), out respData, out headers);
                if (statusCode == HttpStatusCode.OK)
                {
                    Sites addedSites = Utility.JsonDeserialize<Sites>(Encoding.UTF8.GetString(respData));
                    Utility.FillSites(addedSites);
                    if (onAddSitesSuccessfully != null)
                        attachedForm.BeginInvoke(onAddSitesSuccessfully, addedSites);
                }
                else
                {
                    if (onAddSitesFailed != null)
                        attachedForm.BeginInvoke(onAddSitesFailed, statusCode);
                }

            }).Start();
        }

        public void removeSites(Form attachedForm, string siteId)
        {
            new Thread(delegate()
            {
                byte[] respData;
                WebHeaderCollection headers;
                string url = getUrl(Action.removesites) + "/" + siteId;
                HttpStatusCode statusCode = HTTPRequest.MakeRequest(url, "DELETE",
                    null, null,
                    null, out respData, out headers);
                if (statusCode == HttpStatusCode.NoContent)
                {
                    if (onRemoveSitesSuccessfully != null)
                        attachedForm.BeginInvoke(onRemoveSitesSuccessfully, siteId);
                }
                else
                {
                    if (onRemoveSitesFailed != null)
                        attachedForm.BeginInvoke(onRemoveSitesFailed, statusCode, siteId);
                }

            }).Start();
        }

        public void getFileList(Form attachedForm, FileType type)
        {
            new Thread(delegate()
            {
                byte[] respData;
                string json = "";
                WebHeaderCollection headers;
                string url = getUrl(Action.getfilelist) + "/" + type.ToString().ToLower();
                HttpStatusCode statusCode = HTTPRequest.MakeRequest(url, "GET",
                    Constants.JSON_MIME, null,
                    null, out respData, out headers);
                if (statusCode == HttpStatusCode.OK)
                {
                    if (onGetFileListSuccessfully != null)
                    {
                        FileList fileList = new FileList();
                        if (respData != null)
                        {
                            json = Encoding.UTF8.GetString(respData);
                            fileList = Utility.JsonDeserialize<FileList>(json);
                            Utility.FillFileList(fileList);
                        }
                        attachedForm.BeginInvoke(onGetFileListSuccessfully, type, fileList);
                    }
                }
                else
                {
                    if (onGetFileListFailed != null)
                        attachedForm.BeginInvoke(onGetFileListFailed, type, statusCode);
                }

            }).Start();
        }

        public void uploadFile(Form attachedForm, FileType type, string fullFileName, string fileName, 
            byte[] fileContent, byte[] parseData, Dictionary<string,string> paramKeyValues)
        {
            new Thread(delegate()
            {
                byte[] respData;
                string json;
                WebHeaderCollection headers;
                string url = getUrl(Action.uploadfile);
                url += "/" + type.ToString();
                if (paramKeyValues != null && paramKeyValues.Count > 0)
                {
                    int i = 0;
                    foreach (KeyValuePair<string,string> pair in paramKeyValues)
                    {
                        if (i == 0)
                            url += "?";
                        else
                            url += "&";
                        url += pair.Key + "=" + pair.Value;
                        i++;
                    }
                }

                UploadFileData uploadFileData = new UploadFileData();
                uploadFileData.ShortName = fileName;
                uploadFileData.Uploader = HTTPAgent.Username;
                uploadFileData.Md5 = Utility.MD5File(fullFileName);
                uploadFileData.FileContent = fileContent;
                uploadFileData.ParseData = parseData;
                byte[] sendBytes = uploadFileData.ToBytes();

                HttpStatusCode statusCode = HTTPRequest.MakeRequest(url, "POST",
                    Constants.JSON_MIME, Constants.RAW_MIME,
                    sendBytes, out respData, out headers);
                if (statusCode == HttpStatusCode.Accepted)
                {
                    FileDescription description;
                    if (respData != null)
                    {
                        json = Encoding.UTF8.GetString(respData);
                        description = Utility.JsonDeserialize<FileDescription>(json);
                        if (onUploadFileSuccessfully != null)
                        {
                            attachedForm.BeginInvoke(onUploadFileSuccessfully, type, description, parseData);
                        }
                    }
                }
                else
                {
                    ErrorMessage errMsg = null;
                    if (respData != null)
                    {
                        json = Encoding.UTF8.GetString(respData);
                        errMsg = Utility.JsonDeserialize<ErrorMessage>(json);
                    }

                    if (onUploadFileFailed != null)
                        attachedForm.BeginInvoke(onUploadFileFailed, fullFileName, statusCode, errMsg);
                }

            }).Start();
        }

        public void downloadFile(Form attachedForm, FileType type, long fid, string title, DownloadType downloadType,
            Dictionary<string, string> paramKeyValues)
        {
            new Thread(delegate()
            {
                byte[] data;
                WebHeaderCollection headers;
                string url = getUrl(Action.downloadfile);
                url += "/" + type.ToString().ToLower() + "/" + downloadType.ToString();
                url += "?fid=" + fid;
                if (paramKeyValues != null && paramKeyValues.Count > 0)
                {
                   foreach (KeyValuePair<string, string> pair in paramKeyValues)
                    {
                        url += "&" + pair.Key + "=" + pair.Value;
                    }
                }

                HttpStatusCode statusCode = HTTPRequest.MakeRequest(url, "GET",
                    null, Constants.RAW_MIME,
                    null, out data, out headers);
                if (statusCode == HttpStatusCode.OK)
                {
                    if (onDownloadFileSuccessfully != null)
                        attachedForm.BeginInvoke(onDownloadFileSuccessfully, type, fid, title, downloadType, data);
                }
                else
                {
                    if (onDownloadFileFailed != null)
                        attachedForm.BeginInvoke(onDownloadFileFailed, type, fid, title, downloadType, statusCode);
                }

            }).Start();
        }

        public void reomveFile(Form attachedForm, FileType type, long fid, string title)
        {
            new Thread(delegate()
            {
                byte[] respData;
                WebHeaderCollection headers;
                string url = getUrl(Action.removefile);
                url += "/" + type.ToString().ToLower();
                url += "?fid=" + fid;
                HttpStatusCode statusCode = HTTPRequest.MakeRequest(url, "DELETE",
                    null, null,
                    null, out respData, out headers);
                if (statusCode == HttpStatusCode.NoContent)
                {
                    if (onRemoveFileSuccessfully != null)
                        attachedForm.BeginInvoke(onRemoveFileSuccessfully, type, fid, title);
                }
                else
                {
                    if (onRemoveFileFailed != null)
                        attachedForm.BeginInvoke(onRemoveFileFailed, type, fid, title, statusCode);
                }

            }).Start();
        }

        public void getTasks(Form attachedForm)
        {
            new Thread(delegate()
            {
                byte[] respData;
                string json = "";
                WebHeaderCollection headers;
                string url = getUrl(Action.gettasks);
                HttpStatusCode statusCode = HTTPRequest.MakeRequest(url, "GET",
                    Constants.JSON_MIME, null,
                    null, out respData, out headers);
                if (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.NoContent)
                {
                    if (onGetTasksSuccessfully != null)
                    {
                        Tasks tasks = new Tasks();
                        if (respData != null)
                        {
                            json = Encoding.UTF8.GetString(respData);
                            tasks = Utility.JsonDeserialize<Tasks>(json);
                            Utility.FillTasks(tasks);
                        }
                        attachedForm.Invoke(onGetTasksSuccessfully, tasks);
                    }
                }
                else
                {
                    if (onGetTasksFailed != null)
                        attachedForm.BeginInvoke(onGetTasksFailed, statusCode);
                }

            }).Start();
        }

        public void addTask(Form attachedForm, int curVersion, BindingTask task)
        {
            new Thread(delegate()
            {
                byte[] respData;
                string json = "";
                WebHeaderCollection headers;
                string url = getUrl(Action.createtask) + "/" + curVersion;
                HttpStatusCode statusCode = HTTPRequest.MakeRequest(url, "POST",
                    Constants.JSON_MIME, Constants.JSON_MIME,
                    Encoding.UTF8.GetBytes(Utility.JsonSerialize<BindingTask>(task)), out respData, out headers);
                if (statusCode == HttpStatusCode.Accepted)
                {
                    if (onAddTaskSuccessfully != null)
                    {
                        json = Encoding.UTF8.GetString(respData);
                        AddTaskResult result = Utility.JsonDeserialize<AddTaskResult>(json);
                        task.ID = result.TaskID;
                        attachedForm.BeginInvoke(onAddTaskSuccessfully, result.RegionVersion, task);
                    }
                }
                else
                {
                    if (onAddTaskFailed != null)
                        attachedForm.BeginInvoke(onAddTaskFailed, task, statusCode);
                }

            }).Start();
        }

        public void removeTask(Form attachedForm, long taskID, string messageText, object context)
        {
            new Thread(delegate()
            {
                byte[] respData;
                WebHeaderCollection headers;
                string url = getUrl(Action.removetask, taskID);
                HttpStatusCode statusCode = HTTPRequest.MakeRequest(url, "DELETE",
                    null, null,
                    null, out respData, out headers);
                if (statusCode == HttpStatusCode.Accepted)
                {
                    if (onRemoveTaskSuccessfully != null)
                        attachedForm.BeginInvoke(onRemoveTaskSuccessfully, taskID, context);
                }
                else
                {
                    if (onRemoveTaskFailed != null)
                        attachedForm.BeginInvoke(onRemoveTaskFailed, taskID, statusCode, messageText, context);
                }

            }).Start();
        }

        public void updateTask(Form attachedForm, long taskID, UpdateTaskRequest request, string messageText, object context)
        {
            new Thread(delegate()
            {
                byte[] respData;
                WebHeaderCollection headers;
                string url = getUrl(Action.updatetask, taskID);
                HttpStatusCode statusCode = HTTPRequest.MakeRequest(url, "PUT",
                    null, Constants.JSON_MIME,
                    Encoding.UTF8.GetBytes(Utility.JsonSerialize<UpdateTaskRequest>(request)), out respData, out headers);
                if (statusCode == HttpStatusCode.Accepted)
                {
                    if (onUpdateTaskSuccessfully != null)
                    {
                        attachedForm.BeginInvoke(onUpdateTaskSuccessfully, taskID, request, context);
                    }
                }
                else
                {
                    if (onUpdateTaskFailed != null)
                    {
                        attachedForm.BeginInvoke(onUpdateTaskFailed, taskID, request, statusCode, messageText);
                    }
                }

            }).Start();
        }

        public void getTaskReports(Form attachedForm, long taskID, object context)
        {
            new Thread(delegate()
            {
                string json = "";
                byte[] respData;
                WebHeaderCollection headers;
                string url = getUrl(Action.getreports, taskID);
                HttpStatusCode statusCode = HTTPRequest.MakeRequest(url, "GET",
                    Constants.JSON_MIME, null,
                    null, out respData, out headers);
                if (statusCode == HttpStatusCode.OK)
                {
                    if (onGetReportsSuccessfully != null)
                    {
                        Reports reports = new Reports();
                        if (respData != null)
                        {
                            json = Encoding.UTF8.GetString(respData);
                            reports = Utility.JsonDeserialize<Reports>(json);
                            Utility.FillReports(reports);
                        }
                        attachedForm.BeginInvoke(onGetReportsSuccessfully, taskID, reports, context);
                    }
                }
                else
                {
                    if (onGetReportsFailed != null)
                    {
                        attachedForm.BeginInvoke(onGetReportsFailed, taskID, statusCode);
                    }
                }

            }).Start();
        }

        public void uploadTaskReport(Form attachedForm, long taskID, Report report, object context)
        {
            new Thread(delegate()
            {
                string json = "";
                byte[] respData;
                WebHeaderCollection headers;
                string url = getUrl(Action.getreports, taskID);
                HttpStatusCode statusCode = HTTPRequest.MakeRequest(url, "POST",
                    Constants.JSON_MIME, Constants.JSON_MIME,
                    Encoding.UTF8.GetBytes(Utility.JsonSerialize<Report>(report)), out respData, out headers);
                if (statusCode == HttpStatusCode.OK)
                {
                    if (onUploadReportSuccessfully != null)
                    {
                        report = new Report();
                        if (respData != null)
                        {
                            json = Encoding.UTF8.GetString(respData);
                            report = Utility.JsonDeserialize<Report>(json);
                        }
                        attachedForm.BeginInvoke(onUploadReportSuccessfully, taskID, report, context);
                    }
                }
                else
                {
                    if (onUploadReportFailed != null)
                    {
                        attachedForm.BeginInvoke(onUploadReportFailed, taskID, statusCode);
                    }
                }

            }).Start();
        }

        private string getUrl(Action action, params object[] args)
        {
            string url = null;
            switch(action)
            {
                case Action.login:
                case Action.logout:
                    url = "http://" + mServerHost + "/emi/" + "registration"; 
                    break;

                case Action.getglobalregion:
                case Action.updateregion:
                    url = "http://" + mServerHost + "/emi/" + "region"; 
                    break;

                case Action.adduser:
                case Action.getusers:
                case Action.updateuser:
                case Action.removeuser:
                    url = "http://" + mServerHost + "/emi/" + "users";
                    break;

                case Action.getfilelist:
                case Action.uploadfile:
                case Action.downloadfile:
                case Action.removefile:
                    url = "http://" + mServerHost + "/emi/" + "files";
                    break;

                case Action.addsites:
                case Action.removesites:
                case Action.getsites:
                    url = "http://" + mServerHost + "/emi/" + "sites";
                    break;

                case Action.gettasks:
                case Action.createtask:
                    url = "http://" + mServerHost + "/emi/" + "tasks";
                    break;

                case Action.updatetask:
                case Action.removetask:
                    url = "http://" + mServerHost + "/emi/" + "tasks/" + args[0].ToString();
                    //url = "http://" + mServerHost + "/emi/region/" + args[0].ToString() + "/tasks/" + args[1].ToString();
                    break;

                case Action.uploadreport:
                    url = "http://" + mServerHost + "/emi/" + "tasks/" + args[0].ToString() + "/reports";
                    break;

                case Action.getreports:
                    url = "http://" + mServerHost + "/emi/" + "tasks/" + args[0].ToString() + "/reports";
                    break;
            }

            return url;
        }

    }
}
