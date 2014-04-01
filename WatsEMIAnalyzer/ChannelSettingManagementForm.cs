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
    public partial class ChannelSettingManagementForm : Form
    {
        private bool mUseForSelect = false;
        private bool mOKInvoked = false;
        private long mSelectedChannel = -1;

        public bool UseForSelect
        {
            set { mUseForSelect = value; }
        }

        public long SelectedChannel
        {
            get { return mSelectedChannel; }
        }

        public ChannelSettingManagementForm()
        {
            InitializeComponent();
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select Channel Setting Files (*.csv, *.xls, *.xlsx)";
            dlg.Filter = "Channel Setting  Files(*.csv, *.xls, *.xlsx)|*.csv;*.xls;*.xlsx";
            //dlg.Multiselect = true;
            if (dlg.ShowDialog() == DialogResult.Cancel)
                return;

            foreach (string channelSettingFile in dlg.FileNames)
            {
                byte[] fileContent = Utility.GetFileContent(channelSettingFile);
                List<ChannelSetting> channelSettings = ChannelSettingReader.Read(channelSettingFile);
                if (channelSettings.Count == 0)
                    continue;

                byte[] parseData = Utility.Serialize<List<ChannelSetting>>(channelSettings);
                string shortName = Utility.GetShortFileName(channelSettingFile);
                string fid = shortName + " (" + HTTPAgent.Username + ", " + Utility.GetDateTime() + ")";
                HTTPAgent.instance().uploadFile(this, HTTPAgent.FileType.channelSetting, channelSettingFile, shortName,
                    fileContent, parseData, null);
            }
        }

        private void ChannelSettingManagementForm_Load(object sender, EventArgs e)
        {
            HTTPAgent.instance().onUploadFileSuccessfully += new HTTPAgent.uploadFileSuccessfully(ChannelSettingManagementForm_onUploadFileSuccessfully);
            HTTPAgent.instance().onUploadFileFailed += new HTTPAgent.uploadFileFailed(ChannelSettingManagementForm_onUploadFileFailed);
            HTTPAgent.instance().onRemoveFileSuccessfully += new HTTPAgent.removeFileSuccessfully(ChannelSettingManagementForm_onRemoveFileSuccessfully);
            HTTPAgent.instance().onRemoveFileFailed += new HTTPAgent.removeFileFileFailed(ChannelSettingManagementForm_onRemoveFileFailed);

            if (mUseForSelect)
            {
                this.Text = "Select channel setting";
                CancelButton.Visible = OKButton.Visible = true;
            }

            foreach (KeyValuePair<long, FileDescription> pair in DataCenter.Instance().ChannelSettingDescriptions)
                ChannelSettingList.Items.Add(pair.Value);
        }

        void ChannelSettingManagementForm_onRemoveFileFailed(HTTPAgent.FileType type, long fid, string title, System.Net.HttpStatusCode statusCode)
        {
            MessageBox.Show("Remove " + type.ToString() + " file '" + title + "' failed, status code = " + statusCode.ToString());
        }

        void ChannelSettingManagementForm_onRemoveFileSuccessfully(HTTPAgent.FileType type, long fid, string title)
        {
            if (DataCenter.Instance().ChannelSettings.ContainsKey(fid))
            {
                ChannelSettingList.Items.Remove(DataCenter.Instance().ChannelSettingDescriptions[fid]);
                DataCenter.Instance().ChannelSettings.Remove(fid);
                DataCenter.Instance().ChannelSettingDescriptions.Remove(fid);
            }

            ViewButton.Enabled = RemoveButton.Enabled = (ChannelSettingList.SelectedItems.Count > 0);
        }

        void ChannelSettingManagementForm_onUploadFileFailed(string fileName, System.Net.HttpStatusCode statusCode, ErrorMessage errMessage)
        {
            MessageBox.Show("Upload file" + fileName + " failed, status code = " + statusCode.ToString());
        }

        void ChannelSettingManagementForm_onUploadFileSuccessfully(HTTPAgent.FileType type, WatsEMIAnalyzer.Bindings.FileDescription description, byte[] parseData)
        {
            DataCenter.Instance().ChannelSettings[description.ID] = Utility.Deserailize<List<ChannelSetting>>(parseData);
            DataCenter.Instance().ChannelSettingDescriptions[description.ID] = description;
            ChannelSettingList.Items.Add(description);
            ViewButton.Enabled = RemoveButton.Enabled = (ChannelSettingList.SelectedItems.Count > 0);

            ChannelSettingDetailForm detailForm = new ChannelSettingDetailForm(DataCenter.Instance().ChannelSettings[description.ID]);
            detailForm.ShowDialog();
        }

        private void ChannelSettingManagementForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            HTTPAgent.instance().onUploadFileSuccessfully -= new HTTPAgent.uploadFileSuccessfully(ChannelSettingManagementForm_onUploadFileSuccessfully);
            HTTPAgent.instance().onUploadFileFailed -= new HTTPAgent.uploadFileFailed(ChannelSettingManagementForm_onUploadFileFailed);
            HTTPAgent.instance().onRemoveFileSuccessfully -= new HTTPAgent.removeFileSuccessfully(ChannelSettingManagementForm_onRemoveFileSuccessfully);
            HTTPAgent.instance().onRemoveFileFailed -= new HTTPAgent.removeFileFileFailed(ChannelSettingManagementForm_onRemoveFileFailed);
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not yet implemented !");
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            FileDescription description = (FileDescription)ChannelSettingList.SelectedItem;
            List<ChannelSetting> channelSettings = DataCenter.Instance().ChannelSettings[description.ID];

            ChannelSettingDetailForm channelSettingForm = new ChannelSettingDetailForm(channelSettings);
            channelSettingForm.Text = "Channel Setting - " + description.Title;
            channelSettingForm.ShowDialog();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (ChannelSettingList.SelectedItems.Count == 0)
                return;

            FileDescription description = (FileDescription)ChannelSettingList.SelectedItem;
            if (DialogResult.No == MessageBox.Show("You will remove channel setting file '" + description.Title + "', are you sure ?",
                "Warning", MessageBoxButtons.YesNo))
                return;

            HTTPAgent.instance().reomveFile(this, HTTPAgent.FileType.channelSetting, description.ID, description.Title);
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            mOKInvoked = true;
        }

        private void ChannelSettingManagementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!mUseForSelect)
                return;

            if (mOKInvoked)
            {
                mOKInvoked = false;
                if (ChannelSettingList.SelectedIndex == -1)
                {
                    e.Cancel = true;
                    MessageBox.Show("Please select channel setting !");
                    return;
                }
                mSelectedChannel = ((FileDescription)ChannelSettingList.SelectedItem).ID;
            }
        }

        private void ChannelSettingList_SelectedIndexChanged(object sender, EventArgs e)
        {
            OKButton.Enabled = ViewButton.Enabled = RemoveButton.Enabled = (ChannelSettingList.SelectedItems.Count > 0);
        }
    }
}
