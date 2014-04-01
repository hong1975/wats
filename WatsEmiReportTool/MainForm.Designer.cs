namespace WatsEmiReportTool
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.EMIGroup = new System.Windows.Forms.GroupBox();
            this.RemoveAllEmiButton = new System.Windows.Forms.Button();
            this.ViewEmiDetailButton = new System.Windows.Forms.Button();
            this.RemoveEmiButton = new System.Windows.Forms.Button();
            this.AddEmiButton = new System.Windows.Forms.Button();
            this.EMIFilesList = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RemoveAllChannelsButton = new System.Windows.Forms.Button();
            this.ViewChannelDetailButton = new System.Windows.Forms.Button();
            this.RemoveChannelSettingButton = new System.Windows.Forms.Button();
            this.AddChannelSettingButton = new System.Windows.Forms.Button();
            this.ChannelSettingFileList = new System.Windows.Forms.ListBox();
            this.ReportButton = new System.Windows.Forms.Button();
            this.OnePairReportCheckBox = new System.Windows.Forms.CheckBox();
            this.EquipmentParameterGroup = new System.Windows.Forms.GroupBox();
            this.RemoveAllEquipParameterButton = new System.Windows.Forms.Button();
            this.ViewEquipParameterButton = new System.Windows.Forms.Button();
            this.RemoveEquipParameterButton = new System.Windows.Forms.Button();
            this.AddEquipParameterButton = new System.Windows.Forms.Button();
            this.EquipParameterFileList = new System.Windows.Forms.ListBox();
            this.ConfigurationTabControl = new System.Windows.Forms.TabControl();
            this.LinkConfigPage = new System.Windows.Forms.TabPage();
            this.RemoveAllLinkButton = new System.Windows.Forms.Button();
            this.LinkConfigFileList = new System.Windows.Forms.ListBox();
            this.ViewLinkButton = new System.Windows.Forms.Button();
            this.AddLinkButton = new System.Windows.Forms.Button();
            this.RemoveLinkButton = new System.Windows.Forms.Button();
            this.ManuallyConfigPage = new System.Windows.Forms.TabPage();
            this.MC_FOURTIMES_1_PLUS_1SD_Checkbox = new System.Windows.Forms.CheckBox();
            this.MC_TRIPLE_1_PLUS_1SD_Checkbox = new System.Windows.Forms.CheckBox();
            this.MC_DOUBLE_1_PLUS_1SD_Checkbox = new System.Windows.Forms.CheckBox();
            this.MC_1_PLUS_1SD_Checkbox = new System.Windows.Forms.CheckBox();
            this.MC_1_PLUS_1HSB_Checkbox = new System.Windows.Forms.CheckBox();
            this.MC_3_PLUS_1FD_Checkbox = new System.Windows.Forms.CheckBox();
            this.MC_2_PLUS_1FD_Checkbox = new System.Windows.Forms.CheckBox();
            this.MC_1_PLUS_1FD_Checkbox = new System.Windows.Forms.CheckBox();
            this.MC_4_PLUS_0_Checkbox = new System.Windows.Forms.CheckBox();
            this.MC_3_PLUS_0_Checkbox = new System.Windows.Forms.CheckBox();
            this.MC_2_PLUS_0_Checkbox = new System.Windows.Forms.CheckBox();
            this.MC_1_PLUS_0_Checkbox = new System.Windows.Forms.CheckBox();
            this.ListBoxToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.AnalysisChannelCombinationCheckBox = new System.Windows.Forms.CheckBox();
            this.GeneralRadioButton = new System.Windows.Forms.RadioButton();
            this.OtherRadioButton = new System.Windows.Forms.RadioButton();
            this.OtherTypeComboBox = new System.Windows.Forms.ComboBox();
            this.EMIGroup.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.EquipmentParameterGroup.SuspendLayout();
            this.ConfigurationTabControl.SuspendLayout();
            this.LinkConfigPage.SuspendLayout();
            this.ManuallyConfigPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // EMIGroup
            // 
            this.EMIGroup.Controls.Add(this.RemoveAllEmiButton);
            this.EMIGroup.Controls.Add(this.ViewEmiDetailButton);
            this.EMIGroup.Controls.Add(this.RemoveEmiButton);
            this.EMIGroup.Controls.Add(this.AddEmiButton);
            this.EMIGroup.Controls.Add(this.EMIFilesList);
            this.EMIGroup.Location = new System.Drawing.Point(7, 4);
            this.EMIGroup.Margin = new System.Windows.Forms.Padding(2);
            this.EMIGroup.Name = "EMIGroup";
            this.EMIGroup.Padding = new System.Windows.Forms.Padding(2);
            this.EMIGroup.Size = new System.Drawing.Size(411, 166);
            this.EMIGroup.TabIndex = 0;
            this.EMIGroup.TabStop = false;
            this.EMIGroup.Text = "EMI Files";
            // 
            // RemoveAllEmiButton
            // 
            this.RemoveAllEmiButton.Location = new System.Drawing.Point(301, 95);
            this.RemoveAllEmiButton.Margin = new System.Windows.Forms.Padding(2);
            this.RemoveAllEmiButton.Name = "RemoveAllEmiButton";
            this.RemoveAllEmiButton.Size = new System.Drawing.Size(96, 26);
            this.RemoveAllEmiButton.TabIndex = 3;
            this.RemoveAllEmiButton.Text = "Remove All";
            this.RemoveAllEmiButton.UseVisualStyleBackColor = true;
            this.RemoveAllEmiButton.Click += new System.EventHandler(this.RemoveAllEmiButton_Click);
            // 
            // ViewEmiDetailButton
            // 
            this.ViewEmiDetailButton.Enabled = false;
            this.ViewEmiDetailButton.Location = new System.Drawing.Point(301, 129);
            this.ViewEmiDetailButton.Margin = new System.Windows.Forms.Padding(2);
            this.ViewEmiDetailButton.Name = "ViewEmiDetailButton";
            this.ViewEmiDetailButton.Size = new System.Drawing.Size(96, 26);
            this.ViewEmiDetailButton.TabIndex = 0;
            this.ViewEmiDetailButton.Text = "View Detail";
            this.ViewEmiDetailButton.UseVisualStyleBackColor = true;
            this.ViewEmiDetailButton.Click += new System.EventHandler(this.ViewEmiDetailButton_Click);
            // 
            // RemoveEmiButton
            // 
            this.RemoveEmiButton.Enabled = false;
            this.RemoveEmiButton.Location = new System.Drawing.Point(301, 61);
            this.RemoveEmiButton.Margin = new System.Windows.Forms.Padding(2);
            this.RemoveEmiButton.Name = "RemoveEmiButton";
            this.RemoveEmiButton.Size = new System.Drawing.Size(96, 26);
            this.RemoveEmiButton.TabIndex = 2;
            this.RemoveEmiButton.Text = "Remove";
            this.RemoveEmiButton.UseVisualStyleBackColor = true;
            this.RemoveEmiButton.Click += new System.EventHandler(this.RemoveEmiButton_Click);
            // 
            // AddEmiButton
            // 
            this.AddEmiButton.Location = new System.Drawing.Point(301, 27);
            this.AddEmiButton.Margin = new System.Windows.Forms.Padding(2);
            this.AddEmiButton.Name = "AddEmiButton";
            this.AddEmiButton.Size = new System.Drawing.Size(96, 26);
            this.AddEmiButton.TabIndex = 1;
            this.AddEmiButton.Text = "Add";
            this.AddEmiButton.UseVisualStyleBackColor = true;
            this.AddEmiButton.Click += new System.EventHandler(this.AddEmiButton_Click);
            // 
            // EMIFilesList
            // 
            this.EMIFilesList.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EMIFilesList.FormattingEnabled = true;
            this.EMIFilesList.HorizontalScrollbar = true;
            this.EMIFilesList.ItemHeight = 14;
            this.EMIFilesList.Location = new System.Drawing.Point(13, 26);
            this.EMIFilesList.Margin = new System.Windows.Forms.Padding(2);
            this.EMIFilesList.Name = "EMIFilesList";
            this.EMIFilesList.Size = new System.Drawing.Size(271, 130);
            this.EMIFilesList.Sorted = true;
            this.EMIFilesList.TabIndex = 0;
            this.EMIFilesList.SelectedIndexChanged += new System.EventHandler(this.EMIFilesList_SelectedIndexChanged);
            this.EMIFilesList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.EMIFilesList_MouseMove);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RemoveAllChannelsButton);
            this.groupBox2.Controls.Add(this.ViewChannelDetailButton);
            this.groupBox2.Controls.Add(this.RemoveChannelSettingButton);
            this.groupBox2.Controls.Add(this.AddChannelSettingButton);
            this.groupBox2.Controls.Add(this.ChannelSettingFileList);
            this.groupBox2.Location = new System.Drawing.Point(7, 175);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(411, 166);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Channel Setting Files";
            // 
            // RemoveAllChannelsButton
            // 
            this.RemoveAllChannelsButton.Location = new System.Drawing.Point(301, 93);
            this.RemoveAllChannelsButton.Margin = new System.Windows.Forms.Padding(2);
            this.RemoveAllChannelsButton.Name = "RemoveAllChannelsButton";
            this.RemoveAllChannelsButton.Size = new System.Drawing.Size(96, 26);
            this.RemoveAllChannelsButton.TabIndex = 4;
            this.RemoveAllChannelsButton.Text = "Remove All";
            this.RemoveAllChannelsButton.UseVisualStyleBackColor = true;
            this.RemoveAllChannelsButton.Click += new System.EventHandler(this.RemoveAllChannelsButton_Click);
            // 
            // ViewChannelDetailButton
            // 
            this.ViewChannelDetailButton.Enabled = false;
            this.ViewChannelDetailButton.Location = new System.Drawing.Point(301, 127);
            this.ViewChannelDetailButton.Margin = new System.Windows.Forms.Padding(2);
            this.ViewChannelDetailButton.Name = "ViewChannelDetailButton";
            this.ViewChannelDetailButton.Size = new System.Drawing.Size(96, 26);
            this.ViewChannelDetailButton.TabIndex = 3;
            this.ViewChannelDetailButton.Text = "View Detail";
            this.ViewChannelDetailButton.UseVisualStyleBackColor = true;
            this.ViewChannelDetailButton.Click += new System.EventHandler(this.ViewChannelDetailButton_Click);
            // 
            // RemoveChannelSettingButton
            // 
            this.RemoveChannelSettingButton.Enabled = false;
            this.RemoveChannelSettingButton.Location = new System.Drawing.Point(301, 59);
            this.RemoveChannelSettingButton.Margin = new System.Windows.Forms.Padding(2);
            this.RemoveChannelSettingButton.Name = "RemoveChannelSettingButton";
            this.RemoveChannelSettingButton.Size = new System.Drawing.Size(96, 26);
            this.RemoveChannelSettingButton.TabIndex = 2;
            this.RemoveChannelSettingButton.Text = "Remove";
            this.RemoveChannelSettingButton.UseVisualStyleBackColor = true;
            this.RemoveChannelSettingButton.Click += new System.EventHandler(this.RemoveChannelSettingButton_Click);
            // 
            // AddChannelSettingButton
            // 
            this.AddChannelSettingButton.Location = new System.Drawing.Point(301, 25);
            this.AddChannelSettingButton.Margin = new System.Windows.Forms.Padding(2);
            this.AddChannelSettingButton.Name = "AddChannelSettingButton";
            this.AddChannelSettingButton.Size = new System.Drawing.Size(96, 26);
            this.AddChannelSettingButton.TabIndex = 1;
            this.AddChannelSettingButton.Text = "Add";
            this.AddChannelSettingButton.UseVisualStyleBackColor = true;
            this.AddChannelSettingButton.Click += new System.EventHandler(this.AddChannelSettingButton_Click);
            // 
            // ChannelSettingFileList
            // 
            this.ChannelSettingFileList.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChannelSettingFileList.FormattingEnabled = true;
            this.ChannelSettingFileList.HorizontalScrollbar = true;
            this.ChannelSettingFileList.ItemHeight = 14;
            this.ChannelSettingFileList.Location = new System.Drawing.Point(13, 25);
            this.ChannelSettingFileList.Margin = new System.Windows.Forms.Padding(2);
            this.ChannelSettingFileList.Name = "ChannelSettingFileList";
            this.ChannelSettingFileList.Size = new System.Drawing.Size(271, 130);
            this.ChannelSettingFileList.TabIndex = 0;
            this.ChannelSettingFileList.SelectedIndexChanged += new System.EventHandler(this.ChannelSettingFileList_SelectedIndexChanged);
            this.ChannelSettingFileList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChannelSettingFileList_MouseMove);
            // 
            // ReportButton
            // 
            this.ReportButton.Location = new System.Drawing.Point(322, 356);
            this.ReportButton.Margin = new System.Windows.Forms.Padding(2);
            this.ReportButton.Name = "ReportButton";
            this.ReportButton.Size = new System.Drawing.Size(96, 33);
            this.ReportButton.TabIndex = 2;
            this.ReportButton.Text = "Report";
            this.ReportButton.UseVisualStyleBackColor = true;
            this.ReportButton.Click += new System.EventHandler(this.ReportButton_Click);
            // 
            // OnePairReportCheckBox
            // 
            this.OnePairReportCheckBox.AutoSize = true;
            this.OnePairReportCheckBox.Location = new System.Drawing.Point(88, 346);
            this.OnePairReportCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.OnePairReportCheckBox.Name = "OnePairReportCheckBox";
            this.OnePairReportCheckBox.Size = new System.Drawing.Size(60, 18);
            this.OnePairReportCheckBox.TabIndex = 3;
            this.OnePairReportCheckBox.Text = "1 Hop ";
            this.OnePairReportCheckBox.UseVisualStyleBackColor = true;
            this.OnePairReportCheckBox.CheckedChanged += new System.EventHandler(this.OnePairReportCheckBox_CheckedChanged);
            // 
            // EquipmentParameterGroup
            // 
            this.EquipmentParameterGroup.Controls.Add(this.RemoveAllEquipParameterButton);
            this.EquipmentParameterGroup.Controls.Add(this.ViewEquipParameterButton);
            this.EquipmentParameterGroup.Controls.Add(this.RemoveEquipParameterButton);
            this.EquipmentParameterGroup.Controls.Add(this.AddEquipParameterButton);
            this.EquipmentParameterGroup.Controls.Add(this.EquipParameterFileList);
            this.EquipmentParameterGroup.Location = new System.Drawing.Point(433, 175);
            this.EquipmentParameterGroup.Margin = new System.Windows.Forms.Padding(2);
            this.EquipmentParameterGroup.Name = "EquipmentParameterGroup";
            this.EquipmentParameterGroup.Padding = new System.Windows.Forms.Padding(2);
            this.EquipmentParameterGroup.Size = new System.Drawing.Size(411, 166);
            this.EquipmentParameterGroup.TabIndex = 5;
            this.EquipmentParameterGroup.TabStop = false;
            this.EquipmentParameterGroup.Text = "Equipment Parameter Files";
            // 
            // RemoveAllEquipParameterButton
            // 
            this.RemoveAllEquipParameterButton.Location = new System.Drawing.Point(294, 91);
            this.RemoveAllEquipParameterButton.Margin = new System.Windows.Forms.Padding(2);
            this.RemoveAllEquipParameterButton.Name = "RemoveAllEquipParameterButton";
            this.RemoveAllEquipParameterButton.Size = new System.Drawing.Size(96, 26);
            this.RemoveAllEquipParameterButton.TabIndex = 4;
            this.RemoveAllEquipParameterButton.Text = "Remove All";
            this.RemoveAllEquipParameterButton.UseVisualStyleBackColor = true;
            this.RemoveAllEquipParameterButton.Click += new System.EventHandler(this.RemoveAllEquipParameterButton_Click);
            // 
            // ViewEquipParameterButton
            // 
            this.ViewEquipParameterButton.Enabled = false;
            this.ViewEquipParameterButton.Location = new System.Drawing.Point(294, 125);
            this.ViewEquipParameterButton.Margin = new System.Windows.Forms.Padding(2);
            this.ViewEquipParameterButton.Name = "ViewEquipParameterButton";
            this.ViewEquipParameterButton.Size = new System.Drawing.Size(96, 26);
            this.ViewEquipParameterButton.TabIndex = 3;
            this.ViewEquipParameterButton.Text = "View Detail";
            this.ViewEquipParameterButton.UseVisualStyleBackColor = true;
            this.ViewEquipParameterButton.Click += new System.EventHandler(this.ViewEquipParameterButton_Click);
            // 
            // RemoveEquipParameterButton
            // 
            this.RemoveEquipParameterButton.Enabled = false;
            this.RemoveEquipParameterButton.Location = new System.Drawing.Point(294, 57);
            this.RemoveEquipParameterButton.Margin = new System.Windows.Forms.Padding(2);
            this.RemoveEquipParameterButton.Name = "RemoveEquipParameterButton";
            this.RemoveEquipParameterButton.Size = new System.Drawing.Size(96, 26);
            this.RemoveEquipParameterButton.TabIndex = 2;
            this.RemoveEquipParameterButton.Text = "Remove";
            this.RemoveEquipParameterButton.UseVisualStyleBackColor = true;
            this.RemoveEquipParameterButton.Click += new System.EventHandler(this.RemoveEquipParameterButton_Click);
            // 
            // AddEquipParameterButton
            // 
            this.AddEquipParameterButton.Location = new System.Drawing.Point(294, 23);
            this.AddEquipParameterButton.Margin = new System.Windows.Forms.Padding(2);
            this.AddEquipParameterButton.Name = "AddEquipParameterButton";
            this.AddEquipParameterButton.Size = new System.Drawing.Size(96, 26);
            this.AddEquipParameterButton.TabIndex = 1;
            this.AddEquipParameterButton.Text = "Add";
            this.AddEquipParameterButton.UseVisualStyleBackColor = true;
            this.AddEquipParameterButton.Click += new System.EventHandler(this.AddEquipParameterButton_Click);
            // 
            // EquipParameterFileList
            // 
            this.EquipParameterFileList.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EquipParameterFileList.FormattingEnabled = true;
            this.EquipParameterFileList.HorizontalScrollbar = true;
            this.EquipParameterFileList.ItemHeight = 14;
            this.EquipParameterFileList.Location = new System.Drawing.Point(13, 23);
            this.EquipParameterFileList.Margin = new System.Windows.Forms.Padding(2);
            this.EquipParameterFileList.Name = "EquipParameterFileList";
            this.EquipParameterFileList.Size = new System.Drawing.Size(272, 130);
            this.EquipParameterFileList.TabIndex = 0;
            this.EquipParameterFileList.SelectedIndexChanged += new System.EventHandler(this.EquipParameterFileList_SelectedIndexChanged);
            this.EquipParameterFileList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.EquipParameterFileList_MouseMove);
            // 
            // ConfigurationTabControl
            // 
            this.ConfigurationTabControl.Controls.Add(this.LinkConfigPage);
            this.ConfigurationTabControl.Controls.Add(this.ManuallyConfigPage);
            this.ConfigurationTabControl.Location = new System.Drawing.Point(433, 4);
            this.ConfigurationTabControl.Margin = new System.Windows.Forms.Padding(2);
            this.ConfigurationTabControl.Name = "ConfigurationTabControl";
            this.ConfigurationTabControl.SelectedIndex = 0;
            this.ConfigurationTabControl.Size = new System.Drawing.Size(411, 166);
            this.ConfigurationTabControl.TabIndex = 6;
            this.ConfigurationTabControl.SelectedIndexChanged += new System.EventHandler(this.ConfigurationTabControl_SelectedIndexChanged);
            // 
            // LinkConfigPage
            // 
            this.LinkConfigPage.BackColor = System.Drawing.SystemColors.Control;
            this.LinkConfigPage.Controls.Add(this.RemoveAllLinkButton);
            this.LinkConfigPage.Controls.Add(this.LinkConfigFileList);
            this.LinkConfigPage.Controls.Add(this.ViewLinkButton);
            this.LinkConfigPage.Controls.Add(this.AddLinkButton);
            this.LinkConfigPage.Controls.Add(this.RemoveLinkButton);
            this.LinkConfigPage.Location = new System.Drawing.Point(4, 23);
            this.LinkConfigPage.Margin = new System.Windows.Forms.Padding(2);
            this.LinkConfigPage.Name = "LinkConfigPage";
            this.LinkConfigPage.Padding = new System.Windows.Forms.Padding(2);
            this.LinkConfigPage.Size = new System.Drawing.Size(403, 139);
            this.LinkConfigPage.TabIndex = 0;
            this.LinkConfigPage.Text = "Link Configuration File";
            // 
            // RemoveAllLinkButton
            // 
            this.RemoveAllLinkButton.Location = new System.Drawing.Point(294, 67);
            this.RemoveAllLinkButton.Margin = new System.Windows.Forms.Padding(2);
            this.RemoveAllLinkButton.Name = "RemoveAllLinkButton";
            this.RemoveAllLinkButton.Size = new System.Drawing.Size(96, 26);
            this.RemoveAllLinkButton.TabIndex = 8;
            this.RemoveAllLinkButton.Text = "Remove All";
            this.RemoveAllLinkButton.UseVisualStyleBackColor = true;
            this.RemoveAllLinkButton.Click += new System.EventHandler(this.RemoveAllLinkButton_Click);
            // 
            // LinkConfigFileList
            // 
            this.LinkConfigFileList.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LinkConfigFileList.FormattingEnabled = true;
            this.LinkConfigFileList.HorizontalScrollbar = true;
            this.LinkConfigFileList.ItemHeight = 14;
            this.LinkConfigFileList.Location = new System.Drawing.Point(10, 11);
            this.LinkConfigFileList.Margin = new System.Windows.Forms.Padding(2);
            this.LinkConfigFileList.Name = "LinkConfigFileList";
            this.LinkConfigFileList.Size = new System.Drawing.Size(271, 102);
            this.LinkConfigFileList.TabIndex = 1;
            this.LinkConfigFileList.SelectedIndexChanged += new System.EventHandler(this.LinkConfigFileList_SelectedIndexChanged);
            this.LinkConfigFileList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LinkConfigFileList_MouseMove);
            // 
            // ViewLinkButton
            // 
            this.ViewLinkButton.Enabled = false;
            this.ViewLinkButton.Location = new System.Drawing.Point(294, 95);
            this.ViewLinkButton.Margin = new System.Windows.Forms.Padding(2);
            this.ViewLinkButton.Name = "ViewLinkButton";
            this.ViewLinkButton.Size = new System.Drawing.Size(96, 26);
            this.ViewLinkButton.TabIndex = 7;
            this.ViewLinkButton.Text = "View Detail";
            this.ViewLinkButton.UseVisualStyleBackColor = true;
            this.ViewLinkButton.Click += new System.EventHandler(this.ViewLinkButton_Click);
            // 
            // AddLinkButton
            // 
            this.AddLinkButton.Location = new System.Drawing.Point(294, 11);
            this.AddLinkButton.Margin = new System.Windows.Forms.Padding(2);
            this.AddLinkButton.Name = "AddLinkButton";
            this.AddLinkButton.Size = new System.Drawing.Size(96, 26);
            this.AddLinkButton.TabIndex = 5;
            this.AddLinkButton.Text = "Add";
            this.AddLinkButton.UseVisualStyleBackColor = true;
            this.AddLinkButton.Click += new System.EventHandler(this.AddLinkButton_Click);
            // 
            // RemoveLinkButton
            // 
            this.RemoveLinkButton.Enabled = false;
            this.RemoveLinkButton.Location = new System.Drawing.Point(294, 39);
            this.RemoveLinkButton.Margin = new System.Windows.Forms.Padding(2);
            this.RemoveLinkButton.Name = "RemoveLinkButton";
            this.RemoveLinkButton.Size = new System.Drawing.Size(96, 26);
            this.RemoveLinkButton.TabIndex = 6;
            this.RemoveLinkButton.Text = "Remove";
            this.RemoveLinkButton.UseVisualStyleBackColor = true;
            this.RemoveLinkButton.Click += new System.EventHandler(this.RemoveLinkButton_Click);
            // 
            // ManuallyConfigPage
            // 
            this.ManuallyConfigPage.BackColor = System.Drawing.Color.Transparent;
            this.ManuallyConfigPage.Controls.Add(this.MC_FOURTIMES_1_PLUS_1SD_Checkbox);
            this.ManuallyConfigPage.Controls.Add(this.MC_TRIPLE_1_PLUS_1SD_Checkbox);
            this.ManuallyConfigPage.Controls.Add(this.MC_DOUBLE_1_PLUS_1SD_Checkbox);
            this.ManuallyConfigPage.Controls.Add(this.MC_1_PLUS_1SD_Checkbox);
            this.ManuallyConfigPage.Controls.Add(this.MC_1_PLUS_1HSB_Checkbox);
            this.ManuallyConfigPage.Controls.Add(this.MC_3_PLUS_1FD_Checkbox);
            this.ManuallyConfigPage.Controls.Add(this.MC_2_PLUS_1FD_Checkbox);
            this.ManuallyConfigPage.Controls.Add(this.MC_1_PLUS_1FD_Checkbox);
            this.ManuallyConfigPage.Controls.Add(this.MC_4_PLUS_0_Checkbox);
            this.ManuallyConfigPage.Controls.Add(this.MC_3_PLUS_0_Checkbox);
            this.ManuallyConfigPage.Controls.Add(this.MC_2_PLUS_0_Checkbox);
            this.ManuallyConfigPage.Controls.Add(this.MC_1_PLUS_0_Checkbox);
            this.ManuallyConfigPage.Location = new System.Drawing.Point(4, 23);
            this.ManuallyConfigPage.Margin = new System.Windows.Forms.Padding(2);
            this.ManuallyConfigPage.Name = "ManuallyConfigPage";
            this.ManuallyConfigPage.Padding = new System.Windows.Forms.Padding(2);
            this.ManuallyConfigPage.Size = new System.Drawing.Size(403, 139);
            this.ManuallyConfigPage.TabIndex = 1;
            this.ManuallyConfigPage.Text = "Manually Config";
            // 
            // MC_FOURTIMES_1_PLUS_1SD_Checkbox
            // 
            this.MC_FOURTIMES_1_PLUS_1SD_Checkbox.AutoSize = true;
            this.MC_FOURTIMES_1_PLUS_1SD_Checkbox.Location = new System.Drawing.Point(199, 98);
            this.MC_FOURTIMES_1_PLUS_1SD_Checkbox.Margin = new System.Windows.Forms.Padding(2);
            this.MC_FOURTIMES_1_PLUS_1SD_Checkbox.Name = "MC_FOURTIMES_1_PLUS_1SD_Checkbox";
            this.MC_FOURTIMES_1_PLUS_1SD_Checkbox.Size = new System.Drawing.Size(78, 18);
            this.MC_FOURTIMES_1_PLUS_1SD_Checkbox.TabIndex = 11;
            this.MC_FOURTIMES_1_PLUS_1SD_Checkbox.Text = "4*(1+1SD)";
            this.MC_FOURTIMES_1_PLUS_1SD_Checkbox.UseVisualStyleBackColor = true;
            this.MC_FOURTIMES_1_PLUS_1SD_Checkbox.CheckedChanged += new System.EventHandler(this.MC_FOURTIMES_1_PLUS_1SD_Checkbox_CheckedChanged);
            // 
            // MC_TRIPLE_1_PLUS_1SD_Checkbox
            // 
            this.MC_TRIPLE_1_PLUS_1SD_Checkbox.AutoSize = true;
            this.MC_TRIPLE_1_PLUS_1SD_Checkbox.Location = new System.Drawing.Point(199, 71);
            this.MC_TRIPLE_1_PLUS_1SD_Checkbox.Margin = new System.Windows.Forms.Padding(2);
            this.MC_TRIPLE_1_PLUS_1SD_Checkbox.Name = "MC_TRIPLE_1_PLUS_1SD_Checkbox";
            this.MC_TRIPLE_1_PLUS_1SD_Checkbox.Size = new System.Drawing.Size(78, 18);
            this.MC_TRIPLE_1_PLUS_1SD_Checkbox.TabIndex = 10;
            this.MC_TRIPLE_1_PLUS_1SD_Checkbox.Text = "3*(1+1SD)";
            this.MC_TRIPLE_1_PLUS_1SD_Checkbox.UseVisualStyleBackColor = true;
            this.MC_TRIPLE_1_PLUS_1SD_Checkbox.CheckedChanged += new System.EventHandler(this.MC_TRIPLE_1_PLUS_1SD_Checkbox_CheckedChanged);
            // 
            // MC_DOUBLE_1_PLUS_1SD_Checkbox
            // 
            this.MC_DOUBLE_1_PLUS_1SD_Checkbox.AutoSize = true;
            this.MC_DOUBLE_1_PLUS_1SD_Checkbox.Location = new System.Drawing.Point(199, 43);
            this.MC_DOUBLE_1_PLUS_1SD_Checkbox.Margin = new System.Windows.Forms.Padding(2);
            this.MC_DOUBLE_1_PLUS_1SD_Checkbox.Name = "MC_DOUBLE_1_PLUS_1SD_Checkbox";
            this.MC_DOUBLE_1_PLUS_1SD_Checkbox.Size = new System.Drawing.Size(78, 18);
            this.MC_DOUBLE_1_PLUS_1SD_Checkbox.TabIndex = 9;
            this.MC_DOUBLE_1_PLUS_1SD_Checkbox.Text = "2*(1+1SD)";
            this.MC_DOUBLE_1_PLUS_1SD_Checkbox.UseVisualStyleBackColor = true;
            this.MC_DOUBLE_1_PLUS_1SD_Checkbox.CheckedChanged += new System.EventHandler(this.MC_DOUBLE_1_PLUS_1SD_Checkbox_CheckedChanged);
            // 
            // MC_1_PLUS_1SD_Checkbox
            // 
            this.MC_1_PLUS_1SD_Checkbox.AutoSize = true;
            this.MC_1_PLUS_1SD_Checkbox.Location = new System.Drawing.Point(199, 15);
            this.MC_1_PLUS_1SD_Checkbox.Margin = new System.Windows.Forms.Padding(2);
            this.MC_1_PLUS_1SD_Checkbox.Name = "MC_1_PLUS_1SD_Checkbox";
            this.MC_1_PLUS_1SD_Checkbox.Size = new System.Drawing.Size(58, 18);
            this.MC_1_PLUS_1SD_Checkbox.TabIndex = 8;
            this.MC_1_PLUS_1SD_Checkbox.Text = "1+1SD";
            this.MC_1_PLUS_1SD_Checkbox.UseVisualStyleBackColor = true;
            this.MC_1_PLUS_1SD_Checkbox.CheckedChanged += new System.EventHandler(this.MC_1_PLUS_1SD_Checkbox_CheckedChanged);
            // 
            // MC_1_PLUS_1HSB_Checkbox
            // 
            this.MC_1_PLUS_1HSB_Checkbox.AutoSize = true;
            this.MC_1_PLUS_1HSB_Checkbox.Location = new System.Drawing.Point(106, 98);
            this.MC_1_PLUS_1HSB_Checkbox.Margin = new System.Windows.Forms.Padding(2);
            this.MC_1_PLUS_1HSB_Checkbox.Name = "MC_1_PLUS_1HSB_Checkbox";
            this.MC_1_PLUS_1HSB_Checkbox.Size = new System.Drawing.Size(65, 18);
            this.MC_1_PLUS_1HSB_Checkbox.TabIndex = 7;
            this.MC_1_PLUS_1HSB_Checkbox.Text = "1+1HSB";
            this.MC_1_PLUS_1HSB_Checkbox.UseVisualStyleBackColor = true;
            this.MC_1_PLUS_1HSB_Checkbox.CheckedChanged += new System.EventHandler(this.MC_1_PLUS_1HSB_Checkbox_CheckedChanged);
            // 
            // MC_3_PLUS_1FD_Checkbox
            // 
            this.MC_3_PLUS_1FD_Checkbox.AutoSize = true;
            this.MC_3_PLUS_1FD_Checkbox.Location = new System.Drawing.Point(106, 71);
            this.MC_3_PLUS_1FD_Checkbox.Margin = new System.Windows.Forms.Padding(2);
            this.MC_3_PLUS_1FD_Checkbox.Name = "MC_3_PLUS_1FD_Checkbox";
            this.MC_3_PLUS_1FD_Checkbox.Size = new System.Drawing.Size(58, 18);
            this.MC_3_PLUS_1FD_Checkbox.TabIndex = 6;
            this.MC_3_PLUS_1FD_Checkbox.Text = "3+1FD";
            this.MC_3_PLUS_1FD_Checkbox.UseVisualStyleBackColor = true;
            this.MC_3_PLUS_1FD_Checkbox.CheckedChanged += new System.EventHandler(this.MC_3_PLUS_1FD_Checkbox_CheckedChanged);
            // 
            // MC_2_PLUS_1FD_Checkbox
            // 
            this.MC_2_PLUS_1FD_Checkbox.AutoSize = true;
            this.MC_2_PLUS_1FD_Checkbox.Location = new System.Drawing.Point(106, 43);
            this.MC_2_PLUS_1FD_Checkbox.Margin = new System.Windows.Forms.Padding(2);
            this.MC_2_PLUS_1FD_Checkbox.Name = "MC_2_PLUS_1FD_Checkbox";
            this.MC_2_PLUS_1FD_Checkbox.Size = new System.Drawing.Size(58, 18);
            this.MC_2_PLUS_1FD_Checkbox.TabIndex = 5;
            this.MC_2_PLUS_1FD_Checkbox.Text = "2+1FD";
            this.MC_2_PLUS_1FD_Checkbox.UseVisualStyleBackColor = true;
            this.MC_2_PLUS_1FD_Checkbox.CheckedChanged += new System.EventHandler(this.MC_2_PLUS_1FD_Checkbox_CheckedChanged);
            // 
            // MC_1_PLUS_1FD_Checkbox
            // 
            this.MC_1_PLUS_1FD_Checkbox.AutoSize = true;
            this.MC_1_PLUS_1FD_Checkbox.Location = new System.Drawing.Point(106, 15);
            this.MC_1_PLUS_1FD_Checkbox.Margin = new System.Windows.Forms.Padding(2);
            this.MC_1_PLUS_1FD_Checkbox.Name = "MC_1_PLUS_1FD_Checkbox";
            this.MC_1_PLUS_1FD_Checkbox.Size = new System.Drawing.Size(58, 18);
            this.MC_1_PLUS_1FD_Checkbox.TabIndex = 4;
            this.MC_1_PLUS_1FD_Checkbox.Text = "1+1FD";
            this.MC_1_PLUS_1FD_Checkbox.UseVisualStyleBackColor = true;
            this.MC_1_PLUS_1FD_Checkbox.CheckedChanged += new System.EventHandler(this.MC_1_PLUS_1FD_Checkbox_CheckedChanged);
            // 
            // MC_4_PLUS_0_Checkbox
            // 
            this.MC_4_PLUS_0_Checkbox.AutoSize = true;
            this.MC_4_PLUS_0_Checkbox.Location = new System.Drawing.Point(27, 98);
            this.MC_4_PLUS_0_Checkbox.Margin = new System.Windows.Forms.Padding(2);
            this.MC_4_PLUS_0_Checkbox.Name = "MC_4_PLUS_0_Checkbox";
            this.MC_4_PLUS_0_Checkbox.Size = new System.Drawing.Size(44, 18);
            this.MC_4_PLUS_0_Checkbox.TabIndex = 3;
            this.MC_4_PLUS_0_Checkbox.Text = "4+0";
            this.MC_4_PLUS_0_Checkbox.UseVisualStyleBackColor = true;
            this.MC_4_PLUS_0_Checkbox.CheckedChanged += new System.EventHandler(this.MC_4_PLUS_0_Checkbox_CheckedChanged);
            // 
            // MC_3_PLUS_0_Checkbox
            // 
            this.MC_3_PLUS_0_Checkbox.AutoSize = true;
            this.MC_3_PLUS_0_Checkbox.Location = new System.Drawing.Point(27, 71);
            this.MC_3_PLUS_0_Checkbox.Margin = new System.Windows.Forms.Padding(2);
            this.MC_3_PLUS_0_Checkbox.Name = "MC_3_PLUS_0_Checkbox";
            this.MC_3_PLUS_0_Checkbox.Size = new System.Drawing.Size(44, 18);
            this.MC_3_PLUS_0_Checkbox.TabIndex = 2;
            this.MC_3_PLUS_0_Checkbox.Text = "3+0";
            this.MC_3_PLUS_0_Checkbox.UseVisualStyleBackColor = true;
            this.MC_3_PLUS_0_Checkbox.CheckedChanged += new System.EventHandler(this.MC_3_PLUS_0_Checkbox_CheckedChanged);
            // 
            // MC_2_PLUS_0_Checkbox
            // 
            this.MC_2_PLUS_0_Checkbox.AutoSize = true;
            this.MC_2_PLUS_0_Checkbox.Location = new System.Drawing.Point(27, 43);
            this.MC_2_PLUS_0_Checkbox.Margin = new System.Windows.Forms.Padding(2);
            this.MC_2_PLUS_0_Checkbox.Name = "MC_2_PLUS_0_Checkbox";
            this.MC_2_PLUS_0_Checkbox.Size = new System.Drawing.Size(44, 18);
            this.MC_2_PLUS_0_Checkbox.TabIndex = 1;
            this.MC_2_PLUS_0_Checkbox.Text = "2+0";
            this.MC_2_PLUS_0_Checkbox.UseVisualStyleBackColor = true;
            this.MC_2_PLUS_0_Checkbox.CheckedChanged += new System.EventHandler(this.MC_2_PLUS_0_Checkbox_CheckedChanged);
            // 
            // MC_1_PLUS_0_Checkbox
            // 
            this.MC_1_PLUS_0_Checkbox.AutoSize = true;
            this.MC_1_PLUS_0_Checkbox.Location = new System.Drawing.Point(27, 15);
            this.MC_1_PLUS_0_Checkbox.Margin = new System.Windows.Forms.Padding(2);
            this.MC_1_PLUS_0_Checkbox.Name = "MC_1_PLUS_0_Checkbox";
            this.MC_1_PLUS_0_Checkbox.Size = new System.Drawing.Size(44, 18);
            this.MC_1_PLUS_0_Checkbox.TabIndex = 0;
            this.MC_1_PLUS_0_Checkbox.Text = "1+0";
            this.MC_1_PLUS_0_Checkbox.UseVisualStyleBackColor = true;
            this.MC_1_PLUS_0_Checkbox.CheckedChanged += new System.EventHandler(this.MC_1_PLUS_0_Checkbox_CheckedChanged);
            // 
            // ListBoxToolTip
            // 
            this.ListBoxToolTip.AutoPopDelay = 5000;
            this.ListBoxToolTip.InitialDelay = 2000;
            this.ListBoxToolTip.IsBalloon = true;
            this.ListBoxToolTip.ReshowDelay = 100;
            // 
            // AnalysisChannelCombinationCheckBox
            // 
            this.AnalysisChannelCombinationCheckBox.AutoSize = true;
            this.AnalysisChannelCombinationCheckBox.Enabled = false;
            this.AnalysisChannelCombinationCheckBox.Location = new System.Drawing.Point(157, 346);
            this.AnalysisChannelCombinationCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.AnalysisChannelCombinationCheckBox.Name = "AnalysisChannelCombinationCheckBox";
            this.AnalysisChannelCombinationCheckBox.Size = new System.Drawing.Size(144, 18);
            this.AnalysisChannelCombinationCheckBox.TabIndex = 7;
            this.AnalysisChannelCombinationCheckBox.Text = "Analysis Combination";
            this.AnalysisChannelCombinationCheckBox.UseVisualStyleBackColor = true;
            this.AnalysisChannelCombinationCheckBox.CheckedChanged += new System.EventHandler(this.AnalysisChannelCombinationCheckBox_CheckedChanged);
            // 
            // GeneralRadioButton
            // 
            this.GeneralRadioButton.AutoSize = true;
            this.GeneralRadioButton.Checked = true;
            this.GeneralRadioButton.Location = new System.Drawing.Point(7, 347);
            this.GeneralRadioButton.Name = "GeneralRadioButton";
            this.GeneralRadioButton.Size = new System.Drawing.Size(69, 18);
            this.GeneralRadioButton.TabIndex = 8;
            this.GeneralRadioButton.TabStop = true;
            this.GeneralRadioButton.Text = "General";
            this.GeneralRadioButton.UseVisualStyleBackColor = true;
            this.GeneralRadioButton.Click += new System.EventHandler(this.GeneralRadioButton_Click);
            // 
            // OtherRadioButton
            // 
            this.OtherRadioButton.AutoSize = true;
            this.OtherRadioButton.Location = new System.Drawing.Point(7, 376);
            this.OtherRadioButton.Name = "OtherRadioButton";
            this.OtherRadioButton.Size = new System.Drawing.Size(55, 18);
            this.OtherRadioButton.TabIndex = 9;
            this.OtherRadioButton.Text = "Other";
            this.OtherRadioButton.UseVisualStyleBackColor = true;
            this.OtherRadioButton.Click += new System.EventHandler(this.OtherRadioButton_Click);
            // 
            // OtherTypeComboBox
            // 
            this.OtherTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OtherTypeComboBox.Font = new System.Drawing.Font("Calibri", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OtherTypeComboBox.FormattingEnabled = true;
            this.OtherTypeComboBox.Items.AddRange(new object[] {
            "Malaysia 5-Azimuth",
            "Malaysia All Azimuth",
            "Malaysia MAXIS",
            "Venezuela 1 Hop"});
            this.OtherTypeComboBox.Location = new System.Drawing.Point(88, 374);
            this.OtherTypeComboBox.Name = "OtherTypeComboBox";
            this.OtherTypeComboBox.Size = new System.Drawing.Size(203, 21);
            this.OtherTypeComboBox.TabIndex = 10;
            this.OtherTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.OtherTypeComboBox_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 404);
            this.Controls.Add(this.OtherTypeComboBox);
            this.Controls.Add(this.OtherRadioButton);
            this.Controls.Add(this.GeneralRadioButton);
            this.Controls.Add(this.AnalysisChannelCombinationCheckBox);
            this.Controls.Add(this.ConfigurationTabControl);
            this.Controls.Add(this.EquipmentParameterGroup);
            this.Controls.Add(this.OnePairReportCheckBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ReportButton);
            this.Controls.Add(this.EMIGroup);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WATS EMI Report Tool";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.EMIGroup.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.EquipmentParameterGroup.ResumeLayout(false);
            this.ConfigurationTabControl.ResumeLayout(false);
            this.LinkConfigPage.ResumeLayout(false);
            this.ManuallyConfigPage.ResumeLayout(false);
            this.ManuallyConfigPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox EMIGroup;
        private System.Windows.Forms.ListBox EMIFilesList;
        private System.Windows.Forms.Button AddEmiButton;
        private System.Windows.Forms.Button RemoveEmiButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button RemoveChannelSettingButton;
        private System.Windows.Forms.Button AddChannelSettingButton;
        private System.Windows.Forms.ListBox ChannelSettingFileList;
        private System.Windows.Forms.Button ReportButton;
        private System.Windows.Forms.Button ViewEmiDetailButton;
        private System.Windows.Forms.Button ViewChannelDetailButton;
        private System.Windows.Forms.Button RemoveAllEmiButton;
        private System.Windows.Forms.Button RemoveAllChannelsButton;
        private System.Windows.Forms.CheckBox OnePairReportCheckBox;
        private System.Windows.Forms.GroupBox EquipmentParameterGroup;
        private System.Windows.Forms.Button RemoveAllEquipParameterButton;
        private System.Windows.Forms.Button ViewEquipParameterButton;
        private System.Windows.Forms.Button RemoveEquipParameterButton;
        private System.Windows.Forms.Button AddEquipParameterButton;
        private System.Windows.Forms.ListBox EquipParameterFileList;
        private System.Windows.Forms.TabControl ConfigurationTabControl;
        private System.Windows.Forms.TabPage LinkConfigPage;
        private System.Windows.Forms.TabPage ManuallyConfigPage;
        private System.Windows.Forms.ListBox LinkConfigFileList;
        private System.Windows.Forms.Button RemoveAllLinkButton;
        private System.Windows.Forms.Button ViewLinkButton;
        private System.Windows.Forms.Button AddLinkButton;
        private System.Windows.Forms.Button RemoveLinkButton;
        private System.Windows.Forms.CheckBox MC_FOURTIMES_1_PLUS_1SD_Checkbox;
        private System.Windows.Forms.CheckBox MC_TRIPLE_1_PLUS_1SD_Checkbox;
        private System.Windows.Forms.CheckBox MC_DOUBLE_1_PLUS_1SD_Checkbox;
        private System.Windows.Forms.CheckBox MC_1_PLUS_1SD_Checkbox;
        private System.Windows.Forms.CheckBox MC_1_PLUS_1HSB_Checkbox;
        private System.Windows.Forms.CheckBox MC_3_PLUS_1FD_Checkbox;
        private System.Windows.Forms.CheckBox MC_2_PLUS_1FD_Checkbox;
        private System.Windows.Forms.CheckBox MC_1_PLUS_1FD_Checkbox;
        private System.Windows.Forms.CheckBox MC_4_PLUS_0_Checkbox;
        private System.Windows.Forms.CheckBox MC_3_PLUS_0_Checkbox;
        private System.Windows.Forms.CheckBox MC_2_PLUS_0_Checkbox;
        private System.Windows.Forms.CheckBox MC_1_PLUS_0_Checkbox;
        private System.Windows.Forms.ToolTip ListBoxToolTip;
        private System.Windows.Forms.CheckBox AnalysisChannelCombinationCheckBox;
        private System.Windows.Forms.RadioButton GeneralRadioButton;
        private System.Windows.Forms.RadioButton OtherRadioButton;
        private System.Windows.Forms.ComboBox OtherTypeComboBox;





    }
}