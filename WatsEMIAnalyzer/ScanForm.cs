using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WatsEMIAnalyzer
{
    public partial class ScanForm : Form
    {
        private bool mOKInvoked = false;
        private bool mIsRecursive = false;
        private string mScanFolder = "";

        public string ScanFolder
        {
            get { return mScanFolder; }
        }

        public bool IsRecursiveScan
        {
            get { return mIsRecursive; }
        }

        public ScanForm()
        {
            InitializeComponent();
        }

        private void SelectFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "Select folder for scanning EMI file";
            dlg.ShowNewFolderButton = true;
            //dlg.RootFolder = Environment.SpecialFolder.Personal;
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            string scanFolder = dlg.SelectedPath;
            FolderPathEditor.Text = scanFolder;
        }

        private void ScanButton_Click(object sender, EventArgs e)
        {
            mOKInvoked = true;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            mOKInvoked = false;
        }

        private void ScanForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mOKInvoked)
            {
                mOKInvoked = false;
                mIsRecursive = RecursiveCheckBox.Checked;
                mScanFolder = FolderPathEditor.Text.Trim();
                if (mScanFolder.Length == 0)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }
    }
}
