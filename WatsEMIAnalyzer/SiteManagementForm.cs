using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEMIAnalyzer.HTTP;
using WatsEMIAnalyzer.Bindings;
using System.Text.RegularExpressions;

namespace WatsEMIAnalyzer
{
    public partial class SiteManagementForm : Form
    {
        private bool mUseForSelect = false;
        private bool mOKInvoked = false;
        private bool mIsUpdated = false;
        private List<Site> mSelectedSites = new List<Site>();
        private List<Site> mAvailableSites = null;

        public bool UseForSelect
        {
            set { mUseForSelect = value; }
        }

        public List<Site> AvailableSites
        {
            set { mAvailableSites = value; }
        }

        public List<Site> SelectedSites
        {
            get { return mSelectedSites; }
        }

        public bool IsUpdated
        {
            get { return mIsUpdated; }
        }

        public SiteManagementForm()
        {
            InitializeComponent();
        }

        private void SiteManagementForm_Load(object sender, EventArgs e)
        {
            HTTPAgent.instance().onAddSitesFailed += new HTTPAgent.addSitesFailed(SiteManagementForm_onAddSitesFailed);
            HTTPAgent.instance().onAddSitesSuccessfully += new HTTPAgent.addSitesSuccessfully(SiteManagementForm_onAddSitesSuccessfully);
            HTTPAgent.instance().onRemoveSitesSuccessfully += new HTTPAgent.removeSitesSuccessfully(SiteManagementForm_onRemoveSitesSuccessfully);
            HTTPAgent.instance().onRemoveSitesFailed += new HTTPAgent.removeSitesFailed(SiteManagementForm_onRemoveSitesFailed);

            if (mUseForSelect)
            {
                this.Text = "Select sites";
                
                if (mAvailableSites != null)
                {
                    foreach (Site site in mAvailableSites)
                        SiteList.Items.Add(site);
                }

                OKButton.Visible = true;
                CancelButton.Visible = true;
                SiteList.SelectionMode = SelectionMode.MultiExtended;
            }

            if (mAvailableSites != null)
            {
                foreach (Site site in mAvailableSites)
                    SiteList.Items.Add(site);

                UploadButton.Enabled 
                    = CreateButton.Enabled 
                    = RemoveButton.Enabled = false;
            }
            else
            {
                foreach (KeyValuePair<string, Site> pair in DataCenter.Instance().Sites)
                    SiteList.Items.Add(pair.Value);
                //SiteList.Items.Add("(" + pair.Key + ") - " + pair.Value.SiteName);
            }
        }

        void SiteManagementForm_onRemoveSitesFailed(System.Net.HttpStatusCode statusCode, string siteId)
        {
            MessageBox.Show("Remove site " + siteId + " failed, status code = " + statusCode.ToString());
        }

        void SiteManagementForm_onRemoveSitesSuccessfully(string siteId)
        {
            mIsUpdated = true;
            if (DataCenter.Instance().Sites.ContainsKey(siteId))
                DataCenter.Instance().Sites.Remove(siteId);

            SiteList.Items.Clear();
            foreach (KeyValuePair<string, Site> pair in DataCenter.Instance().Sites)
            {
                SiteList.Items.Add(pair.Value);
                //SiteList.Items.Add("(" + pair.Key + ") - " + pair.Value.SiteName);
            }

            ViewButton.Enabled = RemoveButton.Enabled = (SiteList.SelectedItems.Count > 0);
        }

        void SiteManagementForm_onAddSitesSuccessfully(WatsEMIAnalyzer.Bindings.Sites addedSites)
        {
            mIsUpdated = true;
            if (addedSites == null || addedSites.Site == null)
                return;

            foreach (Site site in addedSites.Site)
                DataCenter.Instance().Sites[site.SiteID] = site;

            SiteList.Items.Clear();
            foreach (KeyValuePair<string, Site> pair in DataCenter.Instance().Sites)
            {
                SiteList.Items.Add(pair.Value);
                //SiteList.Items.Add("(" + pair.Key + ") - " + pair.Value.SiteName);
            }

            ViewButton.Enabled = RemoveButton.Enabled = (SiteList.SelectedItems.Count > 0);
        }

        void SiteManagementForm_onAddSitesFailed(System.Net.HttpStatusCode statusCode)
        {
            MessageBox.Show("Add site failed, status code = " + statusCode.ToString());
        }

        private void SiteManagementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!mUseForSelect)
                return;

            if (mOKInvoked)
            {
                mOKInvoked = false;
                if (SiteList.SelectedItems.Count == 0)
                {
                    e.Cancel = true;
                    MessageBox.Show("Please select site !");
                    return;
                }

                foreach (Site site in SiteList.SelectedItems)
                    mSelectedSites.Add(site);
            }
        }

        private void SiteManagementForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            HTTPAgent.instance().onAddSitesFailed -= new HTTPAgent.addSitesFailed(SiteManagementForm_onAddSitesFailed);
            HTTPAgent.instance().onAddSitesSuccessfully -= new HTTPAgent.addSitesSuccessfully(SiteManagementForm_onAddSitesSuccessfully);
            HTTPAgent.instance().onRemoveSitesSuccessfully -= new HTTPAgent.removeSitesSuccessfully(SiteManagementForm_onRemoveSitesSuccessfully);
            HTTPAgent.instance().onRemoveSitesFailed -= new HTTPAgent.removeSitesFailed(SiteManagementForm_onRemoveSitesFailed);
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select Site List Files (*.csv, *.xls, *.xlsx)";
            dlg.Filter = "Site List Files(*.csv, *.xls, *.xlsx)|*.csv;*.xls;*.xlsx";
            //dlg.Multiselect = true;
            if (dlg.ShowDialog() == DialogResult.Cancel)
                return;

            Sites sites = new Sites();
            Sites newSites;
            foreach (string siteListFile in dlg.FileNames)
            {
                newSites = SiteListReader.Read(siteListFile);
                foreach (Site site in newSites.Site)
                    sites.Add(site);
            }
            HTTPAgent.instance().addSites(this, sites);
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            SiteDetailForm newSiteForm = new SiteDetailForm(true);
            if (DialogResult.Cancel == newSiteForm.ShowDialog())
                return;

            Site site = newSiteForm.Site;
            Sites sites = new Sites();
            sites.Add(site);

            HTTPAgent.instance().addSites(this, sites);
        }

        private static string GetSiteID(string siteInfoText)
        {
            string siteId = "";
            Regex regex = new Regex(@"^\((?<SiteID>\S+)\) - (?<SiteName>.*)$");
            Match match = regex.Match(siteInfoText);
            siteId = match.Groups["SiteID"].Value.ToString();

            return siteId;
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            Site site = (Site)SiteList.SelectedItem;

            SiteDetailForm siteForm = new SiteDetailForm(false);
            siteForm.Site = site;
            if (mAvailableSites != null)
                siteForm.AllowUpdate = false;
            siteForm.ShowDialog();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (SiteList.SelectedItems.Count == 0)
                return;

            if (DialogResult.No == MessageBox.Show("You will remove " + SiteList.SelectedItems.Count + " sites, are you sure ?",
                "Warning", MessageBoxButtons.YesNo))
                return;

            foreach (Site site in SiteList.SelectedItems)
            {
                HTTPAgent.instance().removeSites(this, site.SiteID);
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            mOKInvoked = true;
        }

        private void SiteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            OKButton.Enabled = ViewButton.Enabled = RemoveButton.Enabled = (SiteList.SelectedItems.Count > 0);

            if (mAvailableSites != null)
            {
                UploadButton.Enabled
                    = CreateButton.Enabled
                    = RemoveButton.Enabled = false;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            mOKInvoked = false;
        }
    }
}
