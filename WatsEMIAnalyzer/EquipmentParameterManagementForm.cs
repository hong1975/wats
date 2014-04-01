using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEMIAnalyzer.HTTP;
using WatsEmiReportTool;
using WatsEMIAnalyzer.Bindings;

namespace WatsEMIAnalyzer
{
    public partial class EquipmentParameterManagementForm : Form
    {
        private bool mUseForSelect = false;
        private bool mOKInvoked = false;
        private long mSelectedEquipment = -1;

        public bool UseForSelect
        {
            set { mUseForSelect = value; }
        }

        public long SelectedEquipment
        {
            get { return mSelectedEquipment; }
        }

        public EquipmentParameterManagementForm()
        {
            InitializeComponent();
        }

        private void EquipmentParameterManagementForm_Load(object sender, EventArgs e)
        {
            HTTPAgent.instance().onUploadFileSuccessfully += new HTTPAgent.uploadFileSuccessfully(EquipmentParameterManagementForm_onUploadFileSuccessfully);
            HTTPAgent.instance().onUploadFileFailed += new HTTPAgent.uploadFileFailed(EquipmentParameterManagementForm_onUploadFileFailed);
            HTTPAgent.instance().onRemoveFileSuccessfully += new HTTPAgent.removeFileSuccessfully(EquipmentParameterManagementForm_onRemoveFileSuccessfully);
            HTTPAgent.instance().onRemoveFileFailed += new HTTPAgent.removeFileFileFailed(EquipmentParameterManagementForm_onRemoveFileFailed);

            if (mUseForSelect)
            {
                this.Text = "Select equipment parameter";
                OKButton.Visible = true;
            }

            foreach (KeyValuePair<long, FileDescription> pair in DataCenter.Instance().EquipmentParameterDescriptions)
                EquipmentParameterList.Items.Add(pair.Value);
        }

        void EquipmentParameterManagementForm_onRemoveFileFailed(HTTPAgent.FileType type, long fid, string title, System.Net.HttpStatusCode statusCode)
        {
            MessageBox.Show("Remove " + type.ToString() + " file '" + title + "' failed, status code = " + statusCode.ToString());
        }

        void EquipmentParameterManagementForm_onRemoveFileSuccessfully(HTTPAgent.FileType type, long fid, string title)
        {
            if (DataCenter.Instance().EquipmentParameters.ContainsKey(fid))
            {
                EquipmentParameterList.Items.Remove(DataCenter.Instance().EquipmentParameterDescriptions[fid]);
                DataCenter.Instance().EquipmentParameters.Remove(fid);
                DataCenter.Instance().EquipmentParameterDescriptions.Remove(fid);
            }
            ViewButton.Enabled = RemoveButton.Enabled = (EquipmentParameterList.SelectedItems.Count > 0);
        }

        void EquipmentParameterManagementForm_onUploadFileFailed(string fileName, System.Net.HttpStatusCode statusCode, ErrorMessage errMsg)
        {
            MessageBox.Show("Upload file" + fileName + " failed, status code = " + statusCode.ToString());
        }

        void EquipmentParameterManagementForm_onUploadFileSuccessfully(HTTPAgent.FileType type, WatsEMIAnalyzer.Bindings.FileDescription description, byte[] parseData)
        {
            DataCenter.Instance().EquipmentParameters[description.ID] = Utility.Deserailize<Dictionary<string, EquipmentParameter>>(parseData);
            DataCenter.Instance().EquipmentParameterDescriptions[description.ID] = description;
            EquipmentParameterList.Items.Add(description);
            ViewButton.Enabled = RemoveButton.Enabled = (EquipmentParameterList.SelectedItems.Count > 0);

            EquipmentParameterDetailForm detailForm = new EquipmentParameterDetailForm(DataCenter.Instance().EquipmentParameters[description.ID]);
            detailForm.ShowDialog();
        }

        private void EquipmentParameterManagementForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            HTTPAgent.instance().onUploadFileSuccessfully -= new HTTPAgent.uploadFileSuccessfully(EquipmentParameterManagementForm_onUploadFileSuccessfully);
            HTTPAgent.instance().onUploadFileFailed -= new HTTPAgent.uploadFileFailed(EquipmentParameterManagementForm_onUploadFileFailed);
            HTTPAgent.instance().onRemoveFileSuccessfully -= new HTTPAgent.removeFileSuccessfully(EquipmentParameterManagementForm_onRemoveFileSuccessfully);
            HTTPAgent.instance().onRemoveFileFailed -= new HTTPAgent.removeFileFileFailed(EquipmentParameterManagementForm_onRemoveFileFailed);
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select Equipment Parameter Files (*.csv, *.xls, *.xlsx)";
            dlg.Filter = "Equipment Parameter Files(*.csv, *.xls, *.xlsx)|*.csv;*.xls;*.xlsx";
            //dlg.Multiselect = true;
            if (dlg.ShowDialog() == DialogResult.Cancel)
                return;

            foreach (string equipmentParameterFile in dlg.FileNames)
            {
                byte[] fileContent = Utility.GetFileContent(equipmentParameterFile);
                Dictionary<string, EquipmentParameter> equipmentParameters = EquipmentParameterReader.Read(equipmentParameterFile);
                if (equipmentParameters.Count == 0)
                    continue;

                byte[] parseData = Utility.Serialize<Dictionary<string, EquipmentParameter>>(equipmentParameters);
                string shortName = Utility.GetShortFileName(equipmentParameterFile);
                string fid = shortName + " (" + HTTPAgent.Username + ", " + Utility.GetDateTime() + ")";
                HTTPAgent.instance().uploadFile(this, HTTPAgent.FileType.equipmentParameter, equipmentParameterFile, shortName,
                    fileContent, parseData, null);
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            mOKInvoked = true;
        }

        private void EquipmentParameterManagementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!mUseForSelect)
                return;

            if (mOKInvoked)
            {
                mOKInvoked = false;
                if (EquipmentParameterList.SelectedIndex == -1)
                {
                    e.Cancel = true;
                    MessageBox.Show("Please select equipment parameter !");
                    return;
                }
                mSelectedEquipment = ((FileDescription)EquipmentParameterList.SelectedItem).ID;
            }
        }

        private void EquipmentParameterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            OKButton.Enabled = ViewButton.Enabled = RemoveButton.Enabled = (EquipmentParameterList.SelectedItems.Count > 0);
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            FileDescription description = (FileDescription)EquipmentParameterList.SelectedItem;
            Dictionary<string, EquipmentParameter> equipmentParameters = DataCenter.Instance().EquipmentParameters[description.ID];

            EquipmentParameterDetailForm equipmentParameterDetailForm = new EquipmentParameterDetailForm(equipmentParameters);
            equipmentParameterDetailForm.Text = "Equipment Parameter - " + description.Title;
            equipmentParameterDetailForm.ShowDialog();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (EquipmentParameterList.SelectedItems.Count == 0)
                return;

            FileDescription description = (FileDescription)EquipmentParameterList.SelectedItem;
            if (DialogResult.No == MessageBox.Show("You will remove equipment parameter file '" + description.Title + "', are you sure ?",
                "Warning", MessageBoxButtons.YesNo))
                return;

            HTTPAgent.instance().reomveFile(this, HTTPAgent.FileType.equipmentParameter, description.ID, description.Title);
        }
    }
}
