using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using WatsEMIAnalyzer.EMI;

namespace WatsEMIAnalyzer
{
    public partial class ScanProgressForm : Form
    {
        private string mScanRootFolder;
        private bool mIsRecursive;
        private Thread mScanThread;
        private bool mIsTerminated = false;
        private int mEmiFileCount = 0;
        private bool mIsCompleted = false;
        private HashSet<string> mScanSites;
        private EMIFileUploadForm mUploadForm;
        
        private delegate void UpdateProgressInfoDelegate(int cur, int total, string text);
        private delegate void UpdateEMICountInfoDelegate();
        private delegate void AddEMIFileDelegate(string emiFile, EMIFileData emiData);
        private delegate void FinishDelegate();

        private EMIFileParser mParser = new EMIFileParser();

        public ScanProgressForm(EMIFileUploadForm uploadForm, HashSet<string> scanSites, string scanRootFolder, bool isRecursive)
        {
            mUploadForm = uploadForm;
            mScanSites = scanSites;
            mScanRootFolder = scanRootFolder;
            mIsRecursive = isRecursive;

            InitializeComponent();
        }

        private void Finish()
        {
            Close();
        }

        private void ScanProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mIsCompleted)
                return;

            if (DialogResult.No == MessageBox.Show("Scan is ongoing, stop it ?", "Warning", MessageBoxButtons.YesNo))
            {
                e.Cancel = true;
                return;
            }

            mIsTerminated = true;
            mScanThread.Join(3000);
            mScanThread = null;
        }

        private bool ScanFolder(string folderName)
        {
            try
            {
                DirectoryInfo folder = new DirectoryInfo(folderName);
                int total = folder.GetFiles().Length;
                int cur = 1;
                EMIFileData emiData;
                foreach (FileInfo fileInfo in folder.GetFiles())
                {
                    if (mIsTerminated)
                        return false;

                    Thread.Sleep(1);
                    UpdateProgress(cur, total, fileInfo.FullName);
                    cur++;

                    if (fileInfo.FullName.ToLower().EndsWith(".emi"))
                    {
                        try
                        {
                            emiData = mParser.ParseSync(fileInfo.FullName);
                        }
                        catch (System.Exception ex)
                        {
                            emiData = null;
                        }
                        
                        if (emiData == null)
                            continue;

                        if (mScanSites.Count > 0 && mScanSites.Contains(emiData.Site_ID))
                            continue;

                        mEmiFileCount++;
                        UpdateEMICountInfo();
                        AddEmiFile(fileInfo.FullName, emiData);
                    }
                    
                }

                if (!mIsRecursive)
                    return true;

                foreach (DirectoryInfo dirInfo in folder.GetDirectories())
                {
                    if (mIsTerminated)
                        return false;

                    if (!ScanFolder(dirInfo.FullName))
                        return false;
                }

                return true;
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
                return true;
            }
        }

        private void UpdateProgress(int cur, int total, string text)
        {
            BeginInvoke(new UpdateProgressInfoDelegate(OnUpdateProgressInfo),
                cur, total, text);
        }

        private void OnUpdateProgressInfo(int cur, int total, string text)
        {
            this.ScanProgressBar.Value = cur;
            this.ScanProgressBar.Maximum = total;
            this.ScanInfoLabel.Text = text;
        }

        private void UpdateEMICountInfo()
        {
            BeginInvoke(new UpdateEMICountInfoDelegate(OnUpdateEMICountInfo));
        }

        private void OnUpdateEMICountInfo()
        {
            this.EMICountLabel.Text = mEmiFileCount + " EMI files was found.";
        }

        private void AddEmiFile(string emiFile, EMIFileData emiData)
        {
            BeginInvoke(new AddEMIFileDelegate(OnAddEmiFile), emiFile, emiData);
        }

        private void OnAddEmiFile(string emiFile, EMIFileData emiData)
        {
            mUploadForm.AddEmiFile(emiFile, emiData);
        }

        private void ScanProgressForm_Load(object sender, EventArgs e)
        {
            mScanThread = new Thread(delegate()
            {
                if (!ScanFolder(mScanRootFolder))
                {
                    return;
                }

                mIsCompleted = true;
                BeginInvoke(new FinishDelegate(Finish));
            });

            mScanThread.Start();
        }

        private void ScanProgressForm_Shown(object sender, EventArgs e)
        {
            //mScanThread.Start();
        }
    }
}
