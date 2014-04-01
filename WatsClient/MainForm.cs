using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsClient.Administration;
using WatsClient.Settings;
using WatsClient.Bindings;

namespace WatsClient
{
    public partial class MainForm : Form
    {
        private static MainForm singltone;

        private User mSelf;

        public WorkspaceForm mWorkspaceForm;
        public UserForm mUserForm;
        public ColorSettingForm mColorSettingForm;
        public ChannelSettingForm mChannelSettingForm;
        public LinkConfigurationForm mLinkConfigurationForm;
        public EquipmentParameterForm mEquipmentParameterForm;

        public static MainForm Instance
        {
            get { return singltone; }
        }

        public MainForm(User self)
        {
            mSelf = self;
            singltone = this;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            mWorkspaceForm = new WorkspaceForm();
            mUserForm = new UserForm();
            mColorSettingForm = new ColorSettingForm();
            mChannelSettingForm = new ChannelSettingForm();
            mLinkConfigurationForm = new LinkConfigurationForm();
            mEquipmentParameterForm = new EquipmentParameterForm();

            mWorkspaceForm.Show(MainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);
            mUserForm.Show(MainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            mColorSettingForm.Show(MainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            mChannelSettingForm.Show(MainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            mLinkConfigurationForm.Show(MainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            mEquipmentParameterForm.Show(MainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }
    }
}
