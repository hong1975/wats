using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEMIAnalyzer.Bindings;

namespace WatsEMIAnalyzer
{
    public partial class RemoveSiteSelectForm : Form
    {
        private bool mOKInvokded = false;

        public List<Site> Sites
        {
            get;
            set;
        }

        public RemoveSiteSelectForm()
        {
            InitializeComponent();
        }

        private void RemoveSiteSelectForm_Load(object sender, EventArgs e)
        {
            foreach (Site site in Sites)
            {
                SiteList.Items.Add(site);
            }

        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            mOKInvokded = true;
        }

        private void RemoveSiteSelectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mOKInvokded)
            {
                Sites.Clear();
                foreach (object siteObj in SiteList.CheckedItems)
                    Sites.Add((Site)siteObj);
            }
        }

        private void SiteList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            OKButton.Enabled = (SiteList.CheckedItems.Count > 0);
        }
    }
}
