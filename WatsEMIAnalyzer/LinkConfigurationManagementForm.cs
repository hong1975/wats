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
using WatsEmiReportTool;

namespace WatsEMIAnalyzer
{
    public partial class LinkConfigurationManagementForm : Form
    {
        private bool mUseForSelect = false;
        private bool mOKInvoked = false;
        private long mSelectedLink = -1;

        public bool UseForSelect
        {
            set { mUseForSelect = value; }
        }

        public long SelectedLink
        {
            get { return mSelectedLink; }
        }

        public LinkConfigurationManagementForm()
        {
            InitializeComponent();
        }

        private void LinkConfigurationManagementForm_Load(object sender, EventArgs e)
        {
            HTTPAgent.instance().onUploadFileSuccessfully += new HTTPAgent.uploadFileSuccessfully(LinkConfigurationManagementForm_onUploadFileSuccessfully);
            HTTPAgent.instance().onUploadFileFailed += new HTTPAgent.uploadFileFailed(LinkConfigurationManagementForm_onUploadFileFailed);
            HTTPAgent.instance().onRemoveFileSuccessfully += new HTTPAgent.removeFileSuccessfully(LinkConfigurationManagementForm_onRemoveFileSuccessfully);
            HTTPAgent.instance().onRemoveFileFailed += new HTTPAgent.removeFileFileFailed(LinkConfigurationManagementForm_onRemoveFileFailed);

            if (mUseForSelect)
            {
                this.Text = "Select link configuration";
                OKButton.Visible = true;
                CancelButton.Visible = true;
            }

            foreach (KeyValuePair<long, FileDescription> pair in DataCenter.Instance().LinkConfigurationDescriptions)
                LinkConfigurationList.Items.Add(pair.Value);
        }

        void LinkConfigurationManagementForm_onRemoveFileFailed(HTTPAgent.FileType type, long fid, string title, System.Net.HttpStatusCode statusCode)
        {
            MessageBox.Show("Remove " + type.ToString() + " file '" + title + "' failed, status code = " + statusCode.ToString());
        }

        void LinkConfigurationManagementForm_onRemoveFileSuccessfully(HTTPAgent.FileType type, long fid, string title)
        {
            if (DataCenter.Instance().LinkConfigurations.ContainsKey(fid))
            {
                LinkConfigurationList.Items.Remove(DataCenter.Instance().LinkConfigurationDescriptions[fid]);
                DataCenter.Instance().LinkConfigurations.Remove(fid);
                DataCenter.Instance().LinkConfigurationDescriptions.Remove(fid);
            }

            OKButton.Enabled = ViewButton.Enabled = RemoveButton.Enabled = (LinkConfigurationList.SelectedItems.Count > 0);
        }

        void LinkConfigurationManagementForm_onUploadFileFailed(string fileName, System.Net.HttpStatusCode statusCode, ErrorMessage errMessage)
        {
            MessageBox.Show("Upload file" + fileName + " failed, status code = " + statusCode.ToString());
        }

        void LinkConfigurationManagementForm_onUploadFileSuccessfully(HTTPAgent.FileType type, WatsEMIAnalyzer.Bindings.FileDescription description, byte[] parseData)
        {
            DataCenter.Instance().LinkConfigurations[description.ID] = Utility.Deserailize<List<LinkConfiguration>>(parseData);
            DataCenter.Instance().LinkConfigurationDescriptions[description.ID] = description;
            LinkConfigurationList.Items.Add(description);
            ViewButton.Enabled = RemoveButton.Enabled = (LinkConfigurationList.SelectedItems.Count > 0);

            LinkConfigurationDetailForm detailForm = new LinkConfigurationDetailForm(DataCenter.Instance().LinkConfigurations[description.ID]);
            detailForm.ShowDialog();
        }

        private void LinkConfigurationManagementForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            HTTPAgent.instance().onUploadFileSuccessfully -= new HTTPAgent.uploadFileSuccessfully(LinkConfigurationManagementForm_onUploadFileSuccessfully);
            HTTPAgent.instance().onUploadFileFailed -= new HTTPAgent.uploadFileFailed(LinkConfigurationManagementForm_onUploadFileFailed);
            HTTPAgent.instance().onRemoveFileSuccessfully -= new HTTPAgent.removeFileSuccessfully(LinkConfigurationManagementForm_onRemoveFileSuccessfully);
            HTTPAgent.instance().onRemoveFileFailed -= new HTTPAgent.removeFileFileFailed(LinkConfigurationManagementForm_onRemoveFileFailed);
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select Link Configuration Files (*.csv, *.xls, *.xlsx)";
            dlg.Filter = "Link Configuration Files(*.csv, *.xls, *.xlsx)|*.csv;*.xls;*.xlsx";
            //dlg.Multiselect = true;
            if (dlg.ShowDialog() == DialogResult.Cancel)
                return;

            foreach (string linkConfigurationFile in dlg.FileNames)
            {
                byte[] fileContent = Utility.GetFileContent(linkConfigurationFile);
                List<LinkConfiguration> configurations = LinkConfigurationReader.Read(linkConfigurationFile);
                if (configurations.Count == 0)
                    continue;

                byte[] parseData = Utility.Serialize<List<LinkConfiguration>>(configurations);
                string shortName = Utility.GetShortFileName(linkConfigurationFile);
                string fid = shortName + " (" + HTTPAgent.Username + ", " + Utility.GetDateTime() + ")";
                HTTPAgent.instance().uploadFile(this, HTTPAgent.FileType.linkconfiguration, linkConfigurationFile, shortName,
                    fileContent, parseData, null);
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not yet implemented !");
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            FileDescription description = (FileDescription)LinkConfigurationList.SelectedItem;
            List<LinkConfiguration> linkConfigurations = DataCenter.Instance().LinkConfigurations[description.ID];

            LinkConfigurationDetailForm configurationform = new LinkConfigurationDetailForm(linkConfigurations);
            configurationform.Text = "Link Configuration - " + description.Title;
            configurationform.ShowDialog();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (LinkConfigurationList.SelectedItems.Count == 0)
                return;

            FileDescription description = (FileDescription)LinkConfigurationList.SelectedItem;
            if (DialogResult.No == MessageBox.Show("You will remove link configuration file '" + description.Title + "', are you sure ?",
                "Warning", MessageBoxButtons.YesNo))
                return;

            HTTPAgent.instance().reomveFile(this, HTTPAgent.FileType.linkconfiguration, description.ID, description.Title);
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            mOKInvoked = true;
        }

        private void LinkConfigurationManagementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!mUseForSelect)
                return;

            if (mOKInvoked)
            {
                mOKInvoked = false;
                if (LinkConfigurationList.SelectedIndex == -1)
                {
                    e.Cancel = true;
                    MessageBox.Show("Please select link configuration !");
                    return;
                }

                mSelectedLink = ((FileDescription)LinkConfigurationList.SelectedItem).ID;
            }
        }

        private void LinkConfigurationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            OKButton.Enabled = ViewButton.Enabled = RemoveButton.Enabled = (LinkConfigurationList.SelectedItems.Count > 0);
        }
    }
}
