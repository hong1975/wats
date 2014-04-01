using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using WatsEMIAnalyzer.HTTP;
using WatsEMIAnalyzer.EMI;
using WatsEMIAnalyzer.Bindings;
using System.Diagnostics;

namespace WatsEMIAnalyzer
{
    public partial class UploadEMIProgressForm : Form
    {
        private EMIFileUploadForm mUploadForm;
        private Dictionary<string, EMIFileData> mUploadEmiDatas;
        private Thread mUploadThread;
        private bool mIsCompleted = false;
        private bool mIsTerminated = false;
       
        private string mCurUploadFile;

        private int mEmiFileCount = 0;
        private int mUploadFailedCount = 0;
        private int mUploadSucceedCount = 0;

        private delegate void UpdateProgressInfoDelegate(int cur, string text);
        private delegate void UpdateEMICountInfoDelegate();
        private delegate void RemoveEMIFileDelegate(string emiFile);

        public delegate void EmiFileUploadedDelegate(EMIFileData emi);
        public event EmiFileUploadedDelegate OnEmiFileUploadHandler;

        public UploadEMIProgressForm(EMIFileUploadForm uploadForm, Dictionary<string, EMIFileData> datas)
        {
            mUploadForm = uploadForm;
            mUploadEmiDatas = datas;

            foreach (string fileName in datas.Keys)
            {
                Debug.WriteLine(fileName);
            }

            InitializeComponent();

            HTTPAgent.instance().onUploadFileFailed += new HTTPAgent.uploadFileFailed(UploadEMIProgressForm_onUploadFileFailed);
            HTTPAgent.instance().onUploadFileSuccessfully += new HTTPAgent.uploadFileSuccessfully(UploadEMIProgressForm_onUploadFileSuccessfully);

            mUploadThread = new Thread(delegate()
            {
                UploadEmiFile();
            });
        }

        private void UploadEMIProgressForm_Load(object sender, EventArgs e)
        {
            this.UploadProgressBar.Maximum = mUploadEmiDatas.Count;
            mUploadThread.Start();
        }

        private void UploadEMIProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mIsCompleted)
                return;

            if (DialogResult.No == MessageBox.Show("Upload is ongoing, stop it ?", "Warning", MessageBoxButtons.YesNo))
            {
                e.Cancel = true;
                return;
            }

            mIsTerminated = true;
            mUploadThread.Join(3000);
            mUploadThread = null;
        }

        void UploadEMIProgressForm_onUploadFileSuccessfully(HTTPAgent.FileType type,FileDescription description, byte[] parseData)
        {
            Debug.WriteLine("Upload emi file '" + mCurUploadFile + "' succeed !");
            DetailListBox.Items.Add(description.FileName);
            DetailListBox.Items.Add(" Succeed !");

            RemoveEmiFile(mCurUploadFile);

            mEmiFileCount++;
            mUploadSucceedCount++;
            UpdateStatusInfo();
            UpdateProgress(mEmiFileCount, mCurUploadFile);

            DataCenter.Instance().UploadFiles.Remove(mCurUploadFile);
            DataCenter.Instance().StoreData();

            if (OnEmiFileUploadHandler != null)
            {
                EMIFileData emi = Utility.Deserailize<EMIFileData>(parseData);
                OnEmiFileUploadHandler(emi);
            }

            if (mIsTerminated)
                return;

            UploadEmiFile();
        }

        void UploadEMIProgressForm_onUploadFileFailed(string fileName, System.Net.HttpStatusCode statusCode, ErrorMessage errMsg)
        {
            Debug.WriteLine("Upload emi file '" + fileName + "' failed, status = " + statusCode.ToString());
            DetailListBox.Items.Add(fileName);
            string reason = "Unknown reason";
            if (errMsg != null)
            {
                if ("com.mysql.jdbc.exceptions.MySQLIntegrityConstraintViolationException".Equals(errMsg.Name))
                    reason = "Duplicate file";
                else
                    reason = errMsg.Name;
            }
            DetailListBox.Items.Add("    Failed: " + reason + " !");

            RemoveEmiFile(mCurUploadFile);

            mEmiFileCount++;
            mUploadFailedCount++;
            UpdateStatusInfo();
            UpdateProgress(mEmiFileCount, mCurUploadFile);

            DataCenter.Instance().UploadFiles.Remove(mCurUploadFile);
            DataCenter.Instance().StoreData();

            if (mIsTerminated)
                return;

            UploadEmiFile();
        }

        private void UploadEmiFile()
        {
            if (mUploadEmiDatas.Count == 0)
            {
                CancelButton.Text = "Close";
                mIsCompleted = true;
                return;
            }

            if (mIsTerminated)
                return;

            mCurUploadFile = mUploadEmiDatas.Keys.First<string>();
            EMIFileData emiFileData = mUploadEmiDatas[mCurUploadFile];
            mUploadEmiDatas.Remove(mCurUploadFile);

            Debug.WriteLine("Upload emi file '" + mCurUploadFile + " ...");

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("SiteID", emiFileData.Site_ID);
            parameters.Add("Tester", emiFileData.PA_UserName);
            parameters.Add("TestTime", emiFileData.PA_TestTime);

            HTTPAgent.instance().uploadFile(this,
                HTTPAgent.FileType.emi,
                mCurUploadFile,
                Utility.GetShortFileName(mCurUploadFile),
                Utility.GetFileContent(mCurUploadFile),
                Utility.Serialize<EMIFileData>(emiFileData),
                parameters);
        }

        private void UpdateProgress(int cur, string text)
        {
            Invoke(new UpdateProgressInfoDelegate(OnUpdateProgressInfo),
                cur, text);
        }

        private void OnUpdateProgressInfo(int cur, string text)
        {
            this.UploadProgressBar.Value = cur;
            this.UploadProgressBar.Text = text;
        }

        private void UpdateStatusInfo()
        {
            Invoke(new UpdateEMICountInfoDelegate(OnUpdateStatusInfo));
        }

        private void OnUpdateStatusInfo()
        {
            this.StatusLabel.Text = "Succeed: "
                + mUploadSucceedCount
                + ", Failed: " + mUploadFailedCount;
        }

        private void RemoveEmiFile(string emiFile)
        {
            Invoke(new RemoveEMIFileDelegate(OnRemoveEmiFile), emiFile);
        }

        private void OnRemoveEmiFile(string emiFile)
        {
            mUploadForm.RemoveEmiFile(emiFile);
        }

        private void UploadEMIProgressForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            HTTPAgent.instance().onUploadFileFailed -= new HTTPAgent.uploadFileFailed(UploadEMIProgressForm_onUploadFileFailed);
            HTTPAgent.instance().onUploadFileSuccessfully -= new HTTPAgent.uploadFileSuccessfully(UploadEMIProgressForm_onUploadFileSuccessfully);
        }

        private void ShowDetailCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ShowDetail(ShowDetailCheckBox.Checked);
        }

        private void ShowDetail(bool show)
        {
            if (show)
            {
                this.Height = 400;
                CancelButton.Top = 336;
            }
            else
            {
                this.Height = 140;
                CancelButton.Top = 76;
            }
        }
    }
}
