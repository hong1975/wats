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
    public partial class AddSiteForm : Form
    {
        private string mName;
        private bool mOKInvoked = false;

        public string SiteName
        {
            get { return mName; }
        }

        public AddSiteForm()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            mOKInvoked = true;
        }

        private void AddSiteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mOKInvoked)
            {
                SiteNameEditor.Text = SiteNameEditor.Text.Trim();
                if (SiteNameEditor.Text.Length == 0)
                {
                    SiteNameEditor.Focus();
                    MessageBox.Show("Please input site name !");
                    e.Cancel = true;
                }
                else
                {
                    mName = SiteNameEditor.Text;
                }
            }

            mOKInvoked = false;
        }
    }
}
