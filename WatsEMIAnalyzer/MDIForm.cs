using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEMIAnalyzer.Model;
using System.Xml;
using WatsEMIAnalyzer.HTTP;
using System.Diagnostics;

namespace WatsEMIAnalyzer
{
    public partial class MDIForm : Form
    {
        const string SiteXml =
            "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
            "<Sites>" +
            /*
            "<China sid='10000'>" +
            "<Shanghai sid='10001'>" +
            "<徐汇 sid='10003'>" +
            "</徐汇>" +
            "</Shanghai>" +
            "</China>" +
            "<Japan sid='20000'>" +
            "</Japan>" +
            */
            "</Sites>";

        
        private static MDIForm mSelf = null;
        private LogInForm mLoginForm;
        private WorkspaceForm mWorkspaceForm;
        private Timer mTimer = new Timer();

        public static MDIForm Instance
        {
            get { return mSelf; }
        }

        public MDIForm(LogInForm loginForm)
        {
            mSelf = this;

            mLoginForm = loginForm;
            InitializeComponent();

            this.Text += " - " + HTTPAgent.Username;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mWorkspaceForm.Detach();
            mLoginForm.Show();
        }

        private void MDIForm_Load(object sender, EventArgs e)
        {
            mWorkspaceForm = new WorkspaceForm();
            mWorkspaceForm.Show(DockPanel, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);

            mTimer.Interval = 1000;
            mTimer.Tick += new EventHandler(mTimer_Tick);
            mTimer.Start();
        }

        void mTimer_Tick(object sender, EventArgs e)
        {
            //Debug.WriteLine(Form.ActiveForm != null && Form.ActiveForm.Equals(this) ? "Focused" : "Lost focus");
        }

        public WeifenLuo.WinFormsUI.Docking.DockPanel getDockPanel()
        {
            return DockPanel;
        }

        private void FileToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
        }

        private void TaskManagerViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void WorkspaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mWorkspaceForm.IsHidden = !mWorkspaceForm.IsHidden;
            WorkspaceToolStripMenuItem.Checked = !WorkspaceToolStripMenuItem.Checked;
        }
        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainStatusStrip.Visible = !MainStatusStrip.Visible;
        }

        private void changePasswToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasswordForm passwordForm = new PasswordForm();
            DialogResult result = passwordForm.ShowDialog();
        }

        private void colorSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorSettingForm colorSettingForm = new ColorSettingForm();
            colorSettingForm.MdiParent = this;
            colorSettingForm.Show(MDIForm.Instance.getDockPanel(), WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }

        private void userManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserManagementForm userManagmentForm = new UserManagementForm();
            userManagmentForm.ShowDialog();
        }

        private void UploadEMIFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            EMIFileUploadForm uploadForm = new EMIFileUploadForm(null);
            uploadForm.ShowDialog();
            */
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Are you sure exit ?", "Warning", MessageBoxButtons.YesNo))
                return;

            Application.Exit();
        }
    }
}
