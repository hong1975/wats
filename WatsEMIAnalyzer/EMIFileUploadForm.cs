using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEMIAnalyzer.EMI;
using WatsEMIAnalyzer.Bindings;
using WatsEMIAnalyzer.HTTP;

namespace WatsEMIAnalyzer
{
    public partial class EMIFileUploadForm : Form
    {
        private EMIFileParser mParser = new EMIFileParser();
        private HashSet<string> mScanSites = new HashSet<string>();
        private Dictionary<string, EMIFileData> mEMIDatas = new Dictionary<string, EMIFileData>();
        private BindingTask mTask;
        private List<EMIFileData> mUploadedEmis = new List<EMIFileData>();

        public void AddSite(string site)
        {
            mScanSites.Add(site);
        }

        public EMIFileUploadForm(BindingTask task)
        {
            InitializeComponent();
            DataCenter.Instance().LoadData();
            mTask = task;

            HTTPAgent.instance().onUpdateTaskSuccessfully += new HTTPAgent.updateTaskSuccessfully(EMIFileUploadForm_onUpdateTaskSuccessfully);
            HTTPAgent.instance().onUpdateTaskFailed += new HTTPAgent.updateTaskFailed(EMIFileUploadForm_onUpdateTaskFailed);
        }

        private void EMIFileUploadForm_Load(object sender, EventArgs e)
        {
            EMIFileData emiData;
            foreach (string emiFile in DataCenter.Instance().UploadFiles)
            {
                if (EMIFileCheckedListBox.Items.IndexOf(emiFile) >= 0)
                    return;

                try
                {
                    emiData = mParser.ParseSync(emiFile);
                }
                catch (System.Exception ex)
                {
                    emiData = null;
                }
                
                if (emiData == null)
                    continue;

                EMIFileCheckedListBox.Items.Add(emiFile);
                mEMIDatas[emiFile] = emiData;
                
                EMIFileCheckedListBox.SetItemChecked(EMIFileCheckedListBox.Items.Count - 1, true);
            }
        }

        private void ScanFolderButton_Click(object sender, EventArgs e)
        {
            ScanForm scanForm = new ScanForm();
            if (DialogResult.Cancel == scanForm.ShowDialog())
                return;

            ScanProgressForm scanProgressForm
                = new ScanProgressForm(this, mScanSites, scanForm.ScanFolder, scanForm.IsRecursiveScan);
            scanProgressForm.ShowDialog();
        }

        public void AddEmiFile(string emiFile, EMIFileData emiData)
        {
            if (EMIFileCheckedListBox.Items.IndexOf(emiFile) >= 0)
                return;

            mEMIDatas.Add(emiFile, emiData);

            EMIFileCheckedListBox.Items.Add(emiFile);
            EMIFileCheckedListBox.SetItemChecked(EMIFileCheckedListBox.Items.Count - 1, true);

            DataCenter.Instance().UploadFiles.Add(emiFile);
            DataCenter.Instance().StoreData();
        }

        public void RemoveEmiFile(string emiFile)
        {
            EMIFileCheckedListBox.Items.Remove(emiFile);
            mEMIDatas.Remove(emiFile);
        }

        private void SelectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < EMIFileCheckedListBox.Items.Count; i++)
                EMIFileCheckedListBox.SetItemChecked(i, SelectAllCheckBox.Checked);
        }

        private void AddFilesButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select EMI Files (*.emi)";
            dlg.Filter = "EMI Files(*.emi)|*.emi";
            dlg.Multiselect = true;
            if (dlg.ShowDialog() == DialogResult.Cancel)
                return;

            EMIFileData data;
            foreach (string file in dlg.FileNames)
            {
                try
                {
                    data = mParser.ParseSync(file);
                }
                catch (System.Exception ex)
                {
                    data = null;
                }
                
                if (data == null)
                    continue;

                AddEmiFile(file, data);
            }
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            if (EMIFileCheckedListBox.CheckedItems.Count == 0)
            {
                MessageBox.Show("No file was selected to upload !");
                return;
            }

            int count = 0;
            HashSet<string> md5List = new HashSet<string>();
            string fileName;
            string md5;
            List<string> uploadFileNames = new List<string>();
            List<string> identialFiles = new List<string>();
            foreach (object item in EMIFileCheckedListBox.CheckedItems)
            {
                fileName = item.ToString();

                md5 = Utility.MD5File(fileName);
                if (md5List.Contains(md5))
                {
                    identialFiles.Add(fileName);
                    continue;
                }
                md5List.Add(md5);

                uploadFileNames.Add(fileName);
                count++;
            }

            if (identialFiles.Count > 0)
            {
                string msg = "Following file:\r\n";
                foreach (string identialFile in identialFiles)
                {
                    msg += identialFile + "\r\n";
                }
                msg += "are identical to other files !";

                MessageBox.Show(msg);
            }

            Dictionary<string, EMIFileData> uploadEmis = new Dictionary<string, EMIFileData>();
            foreach (string emiFileName in uploadFileNames)
                uploadEmis[emiFileName] = mEMIDatas[emiFileName];

            UploadEMIProgressForm progressForm = new UploadEMIProgressForm(this, uploadEmis);
            progressForm.OnEmiFileUploadHandler += new UploadEMIProgressForm.EmiFileUploadedDelegate(progressForm_OnEmiFileUploadHandler);
            progressForm.ShowDialog();
            progressForm.OnEmiFileUploadHandler -= new UploadEMIProgressForm.EmiFileUploadedDelegate(progressForm_OnEmiFileUploadHandler);

            if (mUploadedEmis.Count > 0)
            {
                HashSet<string> unAssignedSites = new HashSet<string>();
                foreach (EMIFileData emi in mUploadedEmis)
                {
                    if (!mTask.Site.Contains(emi.Site_ID) && !mTask.UnassignedSite.Contains(emi.Site_ID))
                    {
                        unAssignedSites.Add(emi.Site_ID);
                    }
                }

                if (unAssignedSites.Count > 0)
                {
                    UpdateTaskRequest updateTaskRequest = new UpdateTaskRequest();
                    updateTaskRequest.Type = UpdateTaskType.AddUnassignedSite.ToString();
                    foreach (string site in unAssignedSites)
                    {
                        updateTaskRequest.Site.Add(site);
                    }
                    HTTPAgent.instance().updateTask(this, mTask.ID, updateTaskRequest, "Add unassigned sites for task " + mTask.ID, null);
                }
                
            }
        }

        void EMIFileUploadForm_onUpdateTaskFailed(long taskID, UpdateTaskRequest request, System.Net.HttpStatusCode statusCode, string messageText)
        {
            MessageBox.Show(messageText + " failed, status code = " + statusCode.ToString());
        }

        void EMIFileUploadForm_onUpdateTaskSuccessfully(long taskID, UpdateTaskRequest request, object context)
        {
            
        }


        void progressForm_OnEmiFileUploadHandler(EMIFileData emi)
        {
            mUploadedEmis.Add(emi);
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (EMIFileCheckedListBox.CheckedItems.Count == 0)
                return;

            DialogResult result = MessageBox.Show("Remove " + EMIFileCheckedListBox.CheckedItems.Count + " files ?", 
                "Confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                bool removed = false;
                while (EMIFileCheckedListBox.CheckedItems.Count > 0)
                {
                    mEMIDatas.Remove(EMIFileCheckedListBox.CheckedItems[0].ToString());
                    DataCenter.Instance().UploadFiles.Remove(EMIFileCheckedListBox.CheckedItems[0].ToString());
                    EMIFileCheckedListBox.Items.Remove(EMIFileCheckedListBox.CheckedItems[0]);
                    removed = true;
                }
                if (removed)
                    DataCenter.Instance().StoreData();
            }
        }

        private void EMIFileUploadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            HTTPAgent.instance().onUpdateTaskSuccessfully -= new HTTPAgent.updateTaskSuccessfully(EMIFileUploadForm_onUpdateTaskSuccessfully);
            HTTPAgent.instance().onUpdateTaskFailed -= new HTTPAgent.updateTaskFailed(EMIFileUploadForm_onUpdateTaskFailed);
        }
    }
}
