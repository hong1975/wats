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

namespace WatsEMIAnalyzer
{
    public partial class MoveTaskForm : Form
    {
        private TreeNode mRegionNode;
        private bool mSuccess = false;
        private bool mUserCancel = false;

        public bool Success
        {
            get { return mSuccess; }
        }

        public bool UserCancel
        {
            get { return mUserCancel; }
        }

        public MoveTaskForm(TreeNode regionNode, int taskNum)
        {
            InitializeComponent();
            mRegionNode = regionNode;
            AlertInfoLabel.Text = taskNum + " tasks already exist in region, must be removed to";
        }

        private void MoveTaskAlertForm_Load(object sender, EventArgs e)
        {
            HTTPAgent.instance().onUpdateRegionSuccessfully+=new HTTPAgent.updateRegionSuccessfully(MoveTaskAlertForm_onUpdateRegionSuccessfully);
            HTTPAgent.instance().onUpdateRegionFailed += new HTTPAgent.updateRegionFailed(MoveTaskAlertForm_onUpdateRegionFailed);
        }

        private void MoveTaskAlertForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mUserCancel)
            {
                return;
            }
        }

        private void MoveTaskAlertForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            HTTPAgent.instance().onUpdateRegionSuccessfully -= new HTTPAgent.updateRegionSuccessfully(MoveTaskAlertForm_onUpdateRegionSuccessfully);
            HTTPAgent.instance().onUpdateRegionFailed -= new HTTPAgent.updateRegionFailed(MoveTaskAlertForm_onUpdateRegionFailed);
        }

        void MoveTaskAlertForm_onUpdateRegionSuccessfully(UpdateRegionResult result, object context)
        {
            if (!UpdateRegionType.MoveTasksToNewRegion.ToString().Equals(result.Type))
                return;

            mSuccess = true;
            Close();
        }

        void MoveTaskAlertForm_onUpdateRegionFailed(UpdateRegionRequest request, System.Net.HttpStatusCode statusCode, string messageText)
        {
            if (!request.Type.Equals(UpdateRegionType.MoveTasksToNewRegion.ToString()))
                return;

            OKButton.Enabled = true;
            MessageBox.Show(messageText + "failed, status=" + statusCode.ToString());
            Close();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            RegionNameEditor.Text = RegionNameEditor.Text.Trim();
            if (RegionNameEditor.Text.Length == 0)
            {
                MessageBox.Show("Region name is empty !");
                RegionNameEditor.Focus();
                return;
            }

            if (Utility.FindRegionByName(DataCenter.Instance().GlobalRegion.Root, RegionNameEditor.Text) != null)
            {
                MessageBox.Show("Region \"" + RegionNameEditor.Text + "\" already exists !");
                return;
            }

            UpdateRegionRequest request = new UpdateRegionRequest();
            request.Type = UpdateRegionType.MoveTasksToNewRegion.ToString();
            request.Region = (SubRegion)mRegionNode.Tag;
            request.NewRegionName = RegionNameEditor.Text;

            HTTPAgent.instance().updateRegion(this, DataCenter.Instance().GlobalRegion.Version,
                request, "Move tasks", mRegionNode);

            OKButton.Enabled = false;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            mUserCancel = true;
        }
    }
}
