using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsClient.REST;
using System.Net;
using WatsClient.Bindings;

namespace WatsClient
{
    public partial class SyncForm : Form
    {
        public class SyncTask
        {
            public int type;
            public List<int> resourceIds = new List<int>();
 
            public SyncTask(int type)
            {
                this.type = type;
            }

            public void AddResource(int id)
            {
                resourceIds.Add(id);
            }

        }

        private ResourceList mResourceList;
        private Queue<SyncTask> mSyncTaskQueue = new Queue<SyncTask>();
        

        public SyncForm()
        {
            InitializeComponent();
        }

        private void SyncForm_Load(object sender, EventArgs e)
        {
            mSyncTaskQueue.Enqueue(new SyncTask(SyncType.USER));
            Start();
        }

        private void Start()
        {
            SyncLabel.Text = "Get All Users ...";
            Client.OnGetAllUsersResult += new Client.GetAllUsersResult(Client_OnGetAllUsersResult);
            Client.GetAllUsers();
        }

        private void Sync()
        {
            if (mSyncTaskQueue.Count == 0)
            {
                Close();
                return;
            }

            SyncTask task = mSyncTaskQueue.Dequeue();
            switch (task.type)
            {
            }
        }

        void SetSyncLabelInformation(string info)
        {
            this.Invoke((MethodInvoker)delegate()
            {
                SyncLabel.Text = info;
            });
        }

        void Client_OnGetAllUsersResult(HttpStatusCode statusCode, WatsClient.Bindings.Users users)
        {
            Client.OnGetAllUsersResult -= new Client.GetAllUsersResult(Client_OnGetAllUsersResult);
            if (statusCode == HttpStatusCode.OK)
                DataCenter.AllUsers = users;

            SetSyncLabelInformation("Get Resource List ...");
            Client.OnGetResourceListResult += new Client.GetResourceListResult(Client_OnGetResourceListResult);
            Client.GetResourceList();
        }

        void Client_OnGetResourceListResult(HttpStatusCode statusCode, ResourceList resourceList)
        {
            Client.OnGetResourceListResult -= new Client.GetResourceListResult(Client_OnGetResourceListResult);
            if (statusCode == HttpStatusCode.OK)
            {
                mResourceList = resourceList;
                if (!string.IsNullOrEmpty(mResourceList.ColorMd5))
                {

                }
            }

            Sync();
        }
    }
}
