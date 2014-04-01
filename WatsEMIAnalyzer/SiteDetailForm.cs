using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEMIAnalyzer.Bindings;
using System.Text.RegularExpressions;

namespace WatsEMIAnalyzer
{
    public partial class SiteDetailForm : Form
    {
        private bool mOKInvoked = false;
        private bool mIsNew = false;
        private Site mSite;
        private bool mAllowUpdate = true;
        
        public Site Site
        {
            get { return mSite; }
            set { mSite = value; }
        }

        public bool AllowUpdate
        {
            get { return mAllowUpdate; }
            set { mAllowUpdate = value; }
        }

        public SiteDetailForm(bool isNew)
        {
            mIsNew = isNew;
            InitializeComponent();
        }

        private void SiteForm_Load(object sender, EventArgs e)
        {
            if (mIsNew)
            {
                mIsNew = true;
                ConfirmButton.Text = "Create";
            }
            else
            {
                if (mAllowUpdate)
                    ConfirmButton.Text = "Update";
                else
                {
                    CancelButton.Visible = false;
                    ConfirmButton.Text = "OK";
                }

                SiteIdEditor.ReadOnly = true;
                if (mSite != null)
                {
                    SiteIdEditor.Text = mSite.SiteID;
                    SamIdEditor.Text = mSite.SamID;
                    SiteNameEditor.Text = mSite.SiteName;
                    SiteTypeEditor.Text = mSite.SiteType;
                    LongitudeEditor.Text = mSite.Longitude.ToString();
                    LatitudeEditor.Text = mSite.Longitude.ToString();
                    BSCEditor.Text = mSite.BSC;
                    RNCEditor.Text = mSite.RNC;
                }

                SiteIdEditor.ReadOnly = SamIdEditor.ReadOnly = SiteNameEditor.ReadOnly = SiteTypeEditor.ReadOnly
                    = LongitudeEditor.ReadOnly = LatitudeEditor.ReadOnly = BSCEditor.ReadOnly = RNCEditor.ReadOnly
                    = !mAllowUpdate;
            }

            if (mSite != null)
                this.Text = "Site: (" + mSite.SiteID + ") - " + mSite.SiteName;
            else
                this.Text = "Create Site";
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            mOKInvoked = false;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            mOKInvoked = true;
        }

        private void SiteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!mIsNew && !mAllowUpdate)
                return;

            if (mOKInvoked)
            {
                mOKInvoked = false;

                if (SiteIdEditor.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Please input site Id !");
                    SiteIdEditor.Focus();
                    e.Cancel = true;
                    return;
                }

                if (mSite == null)
                    mSite = new Site();

                mSite.SiteID = SiteIdEditor.Text;

                LongitudeEditor.Text = LongitudeEditor.Text.Trim();
                if (LongitudeEditor.Text.Length == 0)
                    LongitudeEditor.Text = "0.0";
                if (!Regex.IsMatch(LongitudeEditor.Text, @"^[+-]?\d+(\.\d*)?$"))
                {
                    MessageBox.Show("Longitude is invalid !");
                    LongitudeEditor.Focus();
                    e.Cancel = true;
                    return;
                }
                mSite.Longitude = double.Parse(LongitudeEditor.Text);

                LatitudeEditor.Text = LatitudeEditor.Text.Trim();
                if (LatitudeEditor.Text.Length == 0)
                    LatitudeEditor.Text = "0.0";
                if (!Regex.IsMatch(LatitudeEditor.Text, @"^[+-]?\d+(\.\d*)?$"))
                {
                    MessageBox.Show("LatitudeEditor is invalid !");
                    LatitudeEditor.Focus();
                    e.Cancel = true;
                    return;
                }
                mSite.Latitude = double.Parse(LatitudeEditor.Text);

                mSite.SiteName = SiteNameEditor.Text;
                mSite.SamID = SamIdEditor.Text;
                mSite.SiteType = SiteTypeEditor.Text;
                mSite.RNC = RNCEditor.Text;
                mSite.BSC = BSCEditor.Text;
            }
        }
    }
}
