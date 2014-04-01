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
    public partial class RenameForm : Form
    {
        private string mName;
        private bool mOKInvoked = false;
        private bool mUpdated = false;
        private CheckInputDelegate mCheckInputDelegate;

        public bool Updated
        {
            get { return mUpdated; }
        }

        public string NewName
        {
            get { return mName; }
        }

        public CheckInputDelegate CheckInputDelegate
        {
            set { mCheckInputDelegate = value; }
        }

        public RenameForm(string aTitle, string aOldName)
        {
            InitializeComponent();
            Text = aTitle;
            mName = aOldName;
            NameEditor.Text = aOldName;
            NameEditor.SelectAll();
            NameEditor.Focus();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            mOKInvoked = true;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            mOKInvoked = false;
        }

        private void RenameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mOKInvoked)
            {
                NameEditor.Text = NameEditor.Text.Trim();
                if (NameEditor.Text.Length == 0)
                {
                    MessageBox.Show("Name is empty !");
                    NameEditor.Focus();
                    e.Cancel = true;
                    return;
                } 
                else if (mName.Equals(NameEditor.Text, StringComparison.OrdinalIgnoreCase))
                {
                    mUpdated = false;
                }
                else if (mCheckInputDelegate != null && !mCheckInputDelegate.Invoke(NameEditor.Text))
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    mUpdated = true;
                }
                mName = NameEditor.Text;

                
            }
        }
    }
}
