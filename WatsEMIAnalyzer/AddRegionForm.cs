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
    public partial class AddRegionForm : Form
    {
        private string mName;
        private bool mOKInvoked = false;

        public string RegionName
        {
            get { return mName; }
        }

        public AddRegionForm()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            mOKInvoked = true;
        }

        private void AddRegionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mOKInvoked)
            {
                RegionNameEditor.Text = RegionNameEditor.Text.Trim();
                if (RegionNameEditor.Text.Length == 0)
                {
                    RegionNameEditor.Focus();
                    MessageBox.Show("Please input region name !");
                    e.Cancel = true;
                }
                else
                {
                    mName = RegionNameEditor.Text;
                }
            }

            mOKInvoked = false;
        }


    }
}
