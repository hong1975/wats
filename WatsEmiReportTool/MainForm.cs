using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEMIAnalyzer;
using WatsEMIAnalyzer.EMI;
using Utils;
using System.IO;

namespace WatsEmiReportTool
{
    public partial class MainForm : Form
    {
        private static MainForm mInstance;
        public static MainForm Instance
        {
            get { return mInstance; }
        }

        private IniFile mIniFile = new IniFile(".\\WatsEmiReportTool.ini");
        private EMIFileParser mParser = new EMIFileParser();
        private Dictionary<string, EMIFileData> mEmiFiles = new Dictionary<string, EMIFileData>();
        private Dictionary<string, List<ChannelSetting>> mChannelSettingFiles = new Dictionary<string, List<ChannelSetting>>();
        private Dictionary<string, List<LinkConfiguration>> mLinkConfigurationFiles = new Dictionary<string, List<LinkConfiguration>>();
        private Dictionary<string, Dictionary<string, EquipmentParameter>> mEquipmentParametersFiles = new Dictionary<string, Dictionary<string, EquipmentParameter>>();
        private int mManualConfig = 0;

        public MainForm()
        {
            InitializeComponent();
            mInstance = this;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string appPath = Utility.GetAppPath() + "\\Temp";
            if (!Directory.Exists(appPath))
                Directory.CreateDirectory(appPath);

            int emiFileCount = mIniFile.ReadInt("General", "EMIFileCount", 0);
            if (emiFileCount > 0)
            {
                string key, value;
                for (int i = 0; i < emiFileCount; i++)
                {
                    key = "EMIFile" + i;
                    value = mIniFile.ReadString("General", key, "").Trim();
                    if (File.Exists(value))
                    {
                        ParseEmiFile(value);
                    }
                }
            }

            int channelSettingFileCount = mIniFile.ReadInt("General", "ChannelSettingCount", 0);
            if (channelSettingFileCount > 0)
            {
                string key, value;
                int index;
                for (int i = 0; i < channelSettingFileCount; i++)
                {
                    key = "ChannelSetting" + i;
                    value = mIniFile.ReadString("General", key, "").Trim();
                    if (File.Exists(value))
                    {
                        index = ChannelSettingFileList.Items.Add(value);
                        ChannelSettingFileList.SelectedIndex = index;
                    }
                }
            }

            bool isGeneralReport = (mIniFile.ReadInt("General", "IsGeneralReport", 1) == 1);
            int useAllAnglesMode = mIniFile.ReadInt("General", "AllAnglesMode", 1);

            OnePairReportCheckBox.Checked = (useAllAnglesMode != 1);
            if (OnePairReportCheckBox.Checked)
                LoadOnePairConfiguration();

            int analysisChannelCombination = mIniFile.ReadInt("General", "AnalysisChannelCombination", 0);
            AnalysisChannelCombinationCheckBox.Checked = (analysisChannelCombination == 1);

            if (isGeneralReport) //General
            {
                GeneralRadioButton.Checked = true;
                OnePairReportCheckBox.Enabled = true;
                AnalysisChannelCombinationCheckBox.Enabled = OnePairReportCheckBox.Checked;
                OtherTypeComboBox.Enabled = false;
            }
            else //Other
            {
                OtherRadioButton.Checked = true;
                AnalysisChannelCombinationCheckBox.Enabled = false;
                OnePairReportCheckBox.Enabled = false;
                OtherTypeComboBox.Enabled = true;
            }

            int otherReportType = mIniFile.ReadInt("General", "OtherReportType", 0);
            if (otherReportType >= OtherTypeComboBox.Items.Count)
                otherReportType = 0;
            OtherTypeComboBox.SelectedIndex = otherReportType;

            UpdateForm(isGeneralReport && useAllAnglesMode != 1);
        }

        private bool IsAnalysisChannelCombination()
        {
            return (AnalysisChannelCombinationCheckBox.Enabled && AnalysisChannelCombinationCheckBox.Checked);
        }

        private void LoadOnePairConfiguration()
        {
            int linkConfigurationFileCount = mIniFile.ReadInt("General", "LinkConfigurationCount", 0);
            if (linkConfigurationFileCount > 0)
            {
                string key, value;
                int index;
                for (int i = 0; i < linkConfigurationFileCount; i++)
                {
                    key = "LinkConfiguration" + i;
                    value = mIniFile.ReadString("General", key, "").Trim();
                    if (File.Exists(value))
                    {
                        if (LinkConfigFileList.Items.Contains(value))
                            continue;

                        index = LinkConfigFileList.Items.Add(value);
                        LinkConfigFileList.SelectedIndex = index;
                    }
                }
            }

            int equipmentParameterFileCount = mIniFile.ReadInt("General", "EquipmentParameterCount", 0);
            if (equipmentParameterFileCount > 0)
            {
                string key, value;
                int index;
                for (int i = 0; i < equipmentParameterFileCount; i++)
                {
                    key = "EquipmentParameter" + i;
                    value = mIniFile.ReadString("General", key, "").Trim();
                    if (File.Exists(value))
                    {
                        if (EquipParameterFileList.Items.Contains(value))
                            continue;

                        index = EquipParameterFileList.Items.Add(value);
                        EquipParameterFileList.SelectedIndex = index;
                    }
                }
            }

            LoadManualConfig();
            bool useManualConfig = (mIniFile.ReadInt("General", "UseManualConfig", 0) == 1);
            if (useManualConfig)
                ConfigurationTabControl.SelectedIndex = 1;
            else
                ConfigurationTabControl.SelectedIndex = 0;
        }

        private void UpdateForm(bool reportAsOnePair)
        {
            ConfigurationTabControl.Visible = reportAsOnePair;
            EquipmentParameterGroup.Visible = reportAsOnePair;
            if (!reportAsOnePair)
            {
                this.Width = 430;
                ReportButton.Left = EMIGroup.Right - ReportButton.Width;
            }
            else
            {
                this.Width = 860;
                ReportButton.Left = ConfigurationTabControl.Right - ReportButton.Width;
            }
        }

        private void ParseEmiFile(string emiFile)
        {
            EMIFileParser parser = new EMIFileParser();
            parser.AttachedForm = this;
            parser.onParseSuccessfully += new EMIFileParser.parseSuccessfully(parser_onParseSuccessfully);
            parser.onParseFailed += new EMIFileParser.parseFailed(parser_onParseFailed);

            parser.Parse(emiFile, parser);
        }

        void parser_onParseFailed(string emiName, string errorMessage, object context)
        {
            EMIFileParser parser = (EMIFileParser)context;
            parser.onParseSuccessfully -= new EMIFileParser.parseSuccessfully(parser_onParseSuccessfully);
            parser.onParseFailed -= new EMIFileParser.parseFailed(parser_onParseFailed);

            if (EMIFilesList.Items.Contains(emiName))
            {
                EMIFilesList.Items.Remove(emiName);
            }

            if (mEmiFiles.ContainsKey(emiName))
                mEmiFiles.Remove(emiName);
        }

        void parser_onParseSuccessfully(string emiName, EMIFileData emiFileData, object context)
        {
            EMIFileParser parser = (EMIFileParser)context;
            parser.onParseSuccessfully -= new EMIFileParser.parseSuccessfully(parser_onParseSuccessfully);
            parser.onParseFailed -= new EMIFileParser.parseFailed(parser_onParseFailed);
            mEmiFiles[emiName] = emiFileData;

            if (!EMIFilesList.Items.Contains(emiName))
                EMIFilesList.Items.Add(emiName);

            if (EMIFilesList.Items.Count > 0)
                EMIFilesList.SelectedIndex = 0;

            StoreFileConfiguration();
        }

        private void AddEmiButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select EMI Files (*.emi)";
            dlg.Filter = "EMI Files(*.emi)|*.emi";
            dlg.Multiselect = true;
            if (dlg.ShowDialog() == DialogResult.Cancel)
                return;

            foreach (string file in dlg.FileNames)
            {
                ParseEmiFile(file);
            }
        }

        private void RemoveEmiButton_Click(object sender, EventArgs e)
        {
            if (EMIFilesList.SelectedIndex == -1)
                return;

            if (DialogResult.Cancel == MessageBox.Show("Are you sure remove EMI file \"" + EMIFilesList.SelectedItem.ToString() + " \" ?",
                "Warning", MessageBoxButtons.OKCancel))
                return;

            if (mEmiFiles.ContainsKey(EMIFilesList.SelectedItem.ToString()))
                mEmiFiles.Remove(EMIFilesList.SelectedItem.ToString());
            EMIFilesList.Items.RemoveAt(EMIFilesList.SelectedIndex);
            StoreFileConfiguration();

            RemoveEmiButton.Enabled = (EMIFilesList.SelectedIndex >= 0);
            ViewEmiDetailButton.Enabled = (EMIFilesList.SelectedIndex >= 0);
        }

        private void AddChannelSettingButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select Channel Setting Files (*.csv, *.xls, *.xlsx)";
            dlg.Filter = "Channel Setting  Files(*.csv, *.xls, *.xlsx)|*.csv;*.xls;*.xlsx";
            dlg.Multiselect = true;
            if (dlg.ShowDialog() == DialogResult.Cancel)
                return;

            int id;
            foreach (string channelSettingFile in dlg.FileNames)
            {
                if (!ChannelSettingFileList.Items.Contains(channelSettingFile))
                {
                    id = ChannelSettingFileList.Items.Add(channelSettingFile);
                    ChannelSettingFileList.SelectedIndex = id;
                }
            }
            StoreFileConfiguration();
        }

        private void RemoveChannelSettingButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.Cancel == MessageBox.Show("Are you sure remove Channel Setting file \"" + ChannelSettingFileList.SelectedItem.ToString() + " \" ?",
                "Warning", MessageBoxButtons.OKCancel))
                return;
            
            ChannelSettingFileList.Items.RemoveAt(ChannelSettingFileList.SelectedIndex);
            StoreFileConfiguration();

            RemoveChannelSettingButton.Enabled = (ChannelSettingFileList.SelectedIndex >= 0);
            ViewChannelDetailButton.Enabled = (ChannelSettingFileList.SelectedIndex >= 0);
        }

        private bool ValidateReport()
        {
            if (EMIFilesList.Items.Count == 0)
            {
                MessageBox.Show("Please add EMI files !");
                return false;
            }

            if (ChannelSettingFileList.SelectedIndex == -1)
            {
                MessageBox.Show("No channel setting file was selected !");
                ChannelSettingFileList.Focus();
                return false;
            }

            if (OtherRadioButton.Checked)
            {
                if (OtherTypeComboBox.Items.Count < 2)
                {
                    MessageBox.Show("Please add at least 2 EMI files !");
                    return false;
                }
            }
            else if (GeneralRadioButton.Checked && OnePairReportCheckBox.Checked)
            {
                if (EMIFilesList.Items.Count < 2)
                {
                    MessageBox.Show("Please add at least 2 EMI files !");
                    return false;
                }
                
                if (EquipParameterFileList.SelectedIndex == -1 && AnalysisChannelCombinationCheckBox.Checked)
                {
                    MessageBox.Show("Not Equipment Parameter was selected !");
                    return false;
                }

                if (ConfigurationTabControl.SelectedIndex == 1)
                {
                    if (mManualConfig == 0 && AnalysisChannelCombinationCheckBox.Checked)
                    {
                        MessageBox.Show("Manual Config not set !");
                        return false;
                    }
                }
                else
                {
                    if (LinkConfigFileList.SelectedIndex == -1 && AnalysisChannelCombinationCheckBox.Checked)
                    {
                        MessageBox.Show("No Link Configuration was selected !");
                        return false;
                    }
                }
            }
            else
            {
                if (EMIFilesList.SelectedIndex == -1)
                {
                    MessageBox.Show("No EMI file was selected !");
                    EMIFilesList.Focus();
                    return false;
                }
            }
            return true;
        }

        private void ReportButton_Click(object sender, EventArgs e)
        {
            if (!ValidateReport())
                return;

            StoreFileConfiguration();
            mIniFile.WriteInt("General", "AllAnglesMode", (OnePairReportCheckBox.Checked ? 0:1));

            List<ChannelSetting> channelSettings;
            string channelSttingFile = ChannelSettingFileList.SelectedItem.ToString();
            if (mChannelSettingFiles.ContainsKey(channelSttingFile))
            {
                channelSettings = mChannelSettingFiles[channelSttingFile];
            }
            else
            {
                channelSettings = ChannelSettingReader.Read(channelSttingFile);
                if (channelSettings.Count == 0)
                {
                    MessageBox.Show("Invalid channel setting !");
                    return;
                }
                mChannelSettingFiles[channelSttingFile] = channelSettings;
            }

            if (GeneralRadioButton.Checked && OnePairReportCheckBox.Checked)
            {
                if (ConfigurationTabControl.SelectedIndex == 0)
                {
                    if (LinkConfigFileList.SelectedIndex >= 0)
                    {
                        List<LinkConfiguration> linkConfigurations;
                        string linkConfigurationFile = LinkConfigFileList.SelectedItem.ToString();
                        if (mLinkConfigurationFiles.ContainsKey(linkConfigurationFile))
                        {
                            linkConfigurations = mLinkConfigurationFiles[linkConfigurationFile];
                        }
                        else
                        {
                            linkConfigurations = LinkConfigurationReader.Read(linkConfigurationFile);
                            if (linkConfigurations.Count == 0)
                            {
                                MessageBox.Show("Invalid link configuration !");
                                return;
                            }
                            mLinkConfigurationFiles[linkConfigurationFile] = linkConfigurations;
                        }
                    }
                }
                
                if (EquipParameterFileList.SelectedIndex >= 0)
                {
                    Dictionary<string, EquipmentParameter> equipmentParameters;
                    string equipmentParameterFile = EquipParameterFileList.SelectedItem.ToString();
                    if (mEquipmentParametersFiles.ContainsKey(equipmentParameterFile))
                    {
                        equipmentParameters = mEquipmentParametersFiles[equipmentParameterFile];
                    }
                    else
                    {
                        equipmentParameters = EquipmentParameterReader.Read(equipmentParameterFile);
                        if (equipmentParameters.Count == 0)
                        {
                            MessageBox.Show("Invalid equipment parameter !");
                            return;
                        }
                        mEquipmentParametersFiles[equipmentParameterFile] = equipmentParameters;
                    }
                }
            }

            LimitSettingForm limitSettingForm = new LimitSettingForm();
            if (DialogResult.Cancel == limitSettingForm.ShowDialog())
                return;

            LimitSetting limitSetting = new LimitSetting();
            limitSetting.UseChannelPowerLimit = limitSettingForm.UseChannelPowerLimit;
            limitSetting.UseDeltaPowerLimit = limitSettingForm.UseDeltaPowerLimit;
            limitSetting.ChannelPowerLimit = limitSettingForm.ChannelPowerLimit;
            limitSetting.DeltaPowerLimit = limitSettingForm.DeltaPowerLimit;

            if (OtherRadioButton.Checked) //Malaysia or Venezuela
            {
                List<string> emiFiles = new List<string>();
                List<EMIFileData> emiFileDatas = new List<EMIFileData>();
                foreach (KeyValuePair<string, EMIFileData> pair in mEmiFiles)
                {
                    emiFiles.Add(pair.Key);
                    emiFileDatas.Add(pair.Value);
                }

                if (OtherTypeComboBox.SelectedIndex != 2)
                {
                    EMIPairSelectForm emiPairSelectForm = new EMIPairSelectForm(emiFiles, emiFileDatas);
                    emiPairSelectForm.IsOnlySitesSelectable = (OtherTypeComboBox.SelectedIndex == 1);
                    if (emiPairSelectForm.ShowDialog() == DialogResult.Cancel)
                        return;

                    EMIFileData dataA = emiPairSelectForm.EmiA;
                    EMIFileData dataB = emiPairSelectForm.EmiB;

                    if (OtherTypeComboBox.SelectedIndex == 3) //Venezuela Report
                    {
                        VenezuelaReportForm venezuelaReportForm = new VenezuelaReportForm(limitSetting,
                        emiPairSelectForm.AzimuthA, emiPairSelectForm.AzimuthB,
                        dataA, dataB, channelSettings);
                        Hide();
                        venezuelaReportForm.Show();
                    }
                    else
                    {
                        MalaysiaReportForm malaysiaReportForm 
                            = new MalaysiaReportForm(OtherTypeComboBox.SelectedIndex == 0,
                                limitSetting, dataA, dataB, channelSettings);
                        if (OtherTypeComboBox.SelectedIndex == 0)
                        {
                            malaysiaReportForm.OppositeAzimuthA = emiPairSelectForm.AzimuthA;
                            malaysiaReportForm.OppositeAzimuthB = emiPairSelectForm.AzimuthB;
                        }

                        Hide();
                        malaysiaReportForm.Show();
                    }
                    
                }
                else if (OtherTypeComboBox.SelectedIndex == 2) //Malaysia Maxis Report
                {
                    EMISingleSelectForm emiSingleSelectForm = new EMISingleSelectForm(emiFiles, emiFileDatas);
                    if (emiSingleSelectForm.ShowDialog() == DialogResult.Cancel)
                        return;

                    EMIFileData emiFileData = emiSingleSelectForm.Emi;
                    if (emiFileData == null)
                        return;

                    MalaysiaMaxisReportForm malaysiaMaxisReportForm 
                        = new MalaysiaMaxisReportForm(limitSetting, emiFileData, channelSettings);

                    Hide();
                    malaysiaMaxisReportForm.Show();
                }
                
                return;
            }
            else if (!OnePairReportCheckBox.Checked)    //General 360
            {
                EMIFileData emiFileData;
                if (!mEmiFiles.ContainsKey(EMIFilesList.SelectedItem.ToString()))
                {
                    MessageBox.Show(EMIFilesList.SelectedItem.ToString() + " is an invalid EMI file, \r\nplease choose another file !");
                    return;
                }
                emiFileData = mEmiFiles[EMIFilesList.SelectedItem.ToString()];

                bool isFiveAzimuthReport = (OtherTypeComboBox.SelectedIndex == 0);
                ReportForm reportForm = new ReportForm(isFiveAzimuthReport, limitSetting, emiFileData, channelSettings);
                Hide();
                reportForm.Show();
                return;
            }
            else    //General one pair
            {
                List<string> emiFiles = new List<string>();
                List<EMIFileData> emiFileDatas = new List<EMIFileData>();
                foreach (KeyValuePair<string, EMIFileData> pair in mEmiFiles)
                {
                    emiFiles.Add(pair.Key);
                    emiFileDatas.Add(pair.Value);
                }

                EMIPairSelectForm emiPairSelectForm = new EMIPairSelectForm(emiFiles, emiFileDatas);
                if (emiPairSelectForm.ShowDialog() == DialogResult.Cancel)
                    return;

                PairReportForm pairReportForm = new PairReportForm();
                pairReportForm.LimitSetting = limitSetting;
                pairReportForm.EmiA = emiPairSelectForm.EmiA;
                pairReportForm.EmiB = emiPairSelectForm.EmiB;
                pairReportForm.AzimuthA = emiPairSelectForm.AzimuthA;
                pairReportForm.AzimuthB = emiPairSelectForm.AzimuthB;
                pairReportForm.ChannelSettings = mChannelSettingFiles[ChannelSettingFileList.SelectedItem.ToString()];
                pairReportForm.Init();

                pairReportForm.IsManualConfig = (ConfigurationTabControl.SelectedIndex == 1);
                if (ConfigurationTabControl.SelectedIndex == 1)
                {
                    if (mManualConfig > 0)
                    {
                        pairReportForm.ManualConfig = mManualConfig;
                        int frequencyCount = LinkFrequencyCountMap.Instance.GetCount(mManualConfig);
                        if (pairReportForm.AvailableChannels.Count < frequencyCount)
                        {
                            MessageBox.Show("Available channels count is " + pairReportForm.AvailableChannels.Count
                                + ",\r\nwhile config needs " + frequencyCount + " channels"
                                + ",\r\n please change config !");

                            return;
                        }
                    }
                }
                else
                {
                    string linkName1 = emiPairSelectForm.EmiA.Site_ID + "_" + emiPairSelectForm.EmiB.Site_ID;
                    string linkName2 = emiPairSelectForm.EmiB.Site_ID + "_" + emiPairSelectForm.EmiA.Site_ID;
                    List<LinkConfiguration> linkConfigurations = new List<LinkConfiguration>();
                    if (LinkConfigFileList.SelectedIndex != -1)
                    {
                        foreach (LinkConfiguration linkConfiguration in mLinkConfigurationFiles[LinkConfigFileList.SelectedItem.ToString()])
                        {
                            if (linkConfiguration.LinkName.Equals(linkName1)
                                || linkConfiguration.LinkName.Equals(linkName2))
                            {
                                linkConfigurations.Add(linkConfiguration);
                            }
                        }
                    }

                    if (linkConfigurations.Count == 0 && AnalysisChannelCombinationCheckBox.Checked)
                    {
                        MessageBox.Show("Not find link configuration \"" + linkName1 + "\" or \"" + linkName2 + "\" !");
                        return;
                    }
                    pairReportForm.LinkConfigurations = linkConfigurations;

                    int frequencyCount = 0;
                    foreach (LinkConfiguration linkConfiguration in linkConfigurations)
                        frequencyCount += LinkFrequencyCountMap.Instance.GetCount(linkConfiguration.RequiredConfiguration);
                    if (pairReportForm.AvailableChannels.Count < frequencyCount && AnalysisChannelCombinationCheckBox.Checked)
                    {
                        MessageBox.Show("Available channels count is " + pairReportForm.AvailableChannels.Count
                            + ",\r\nwhile config needs " + frequencyCount + " channels"
                            + ",\r\n please change config !");

                        return;
                    }
                }

                if (EquipParameterFileList.SelectedIndex >= 0)
                    pairReportForm.EquipmentParameters = mEquipmentParametersFiles[EquipParameterFileList.SelectedItem.ToString()];
                Hide();
                pairReportForm.Show();
            }
        }

        private void EMIFilesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemoveEmiButton.Enabled = (EMIFilesList.SelectedIndex >= 0);
            ViewEmiDetailButton.Enabled = (EMIFilesList.SelectedIndex >= 0);
        }

        private void ChannelSettingFileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemoveChannelSettingButton.Enabled = (ChannelSettingFileList.SelectedIndex >= 0);
            ViewChannelDetailButton.Enabled = (ChannelSettingFileList.SelectedIndex >= 0);
        }

        private void ViewEmiDetailButton_Click(object sender, EventArgs e)
        {
            if (EMIFilesList.SelectedIndex == -1)
                return;

            if (mEmiFiles.ContainsKey(EMIFilesList.SelectedItem.ToString()))
            {
                EMIDataForm emiDataForm = new EMIDataForm(mEmiFiles[EMIFilesList.SelectedItem.ToString()]);
                emiDataForm.ShowDialog();
            }
        }

        private void ViewChannelDetailButton_Click(object sender, EventArgs e)
        {
            if (ChannelSettingFileList.SelectedIndex == -1)
                return;

            List<ChannelSetting> channelSettings;
            string channelSettingFile = ChannelSettingFileList.SelectedItem.ToString();
            if (mChannelSettingFiles.ContainsKey(channelSettingFile))
            {
                channelSettings = mChannelSettingFiles[channelSettingFile];
            }
            else
            {
                try
                {
                    channelSettings = ChannelSettingReader.Read(channelSettingFile);
                    mChannelSettingFiles[channelSettingFile] = channelSettings;
                }
                catch (System.Exception ex)
                {
                	MessageBox.Show("Channel Setting is invalid !\r\n" + ex.Message);
                    return;
                }
            }

            ChannelSettingForm channelSettingForm = new ChannelSettingForm(channelSettings);
            channelSettingForm.ShowDialog();
        }

        private void RemoveAllEmiButton_Click(object sender, EventArgs e)
        {
            if (EMIFilesList.Items.Count == 0)
                return;

            if (MessageBox.Show("Are you sure remove all EMI files ?", "Warning", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;

            EMIFilesList.Items.Clear();
            mEmiFiles.Clear();
            StoreFileConfiguration();

            RemoveEmiButton.Enabled = (EMIFilesList.SelectedIndex >= 0);
            ViewEmiDetailButton.Enabled = (EMIFilesList.SelectedIndex >= 0);
        }

        private void RemoveAllChannelsButton_Click(object sender, EventArgs e)
        {
            if (ChannelSettingFileList.Items.Count == 0)
                return;

            if (MessageBox.Show("Are you sure remove all channel setting files ?", "Warning", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;

            ChannelSettingFileList.Items.Clear();
            StoreFileConfiguration();

            RemoveChannelSettingButton.Enabled = false;
            ViewChannelDetailButton.Enabled = false;
        }

        private void StoreFileConfiguration()
        {
            mIniFile.WriteInt("General", "EMIFileCount", EMIFilesList.Items.Count);
            string key;
            for (int i = 0; i < EMIFilesList.Items.Count; i++)
            {
                key = "EMIFile" + i;
                mIniFile.WriteString("General", key, EMIFilesList.Items[i].ToString());
            }

            mIniFile.WriteInt("General", "ChannelSettingCount", ChannelSettingFileList.Items.Count);
            for (int i = 0; i < ChannelSettingFileList.Items.Count; i++)
            {
                key = "ChannelSetting" + i;
                mIniFile.WriteString("General", key, ChannelSettingFileList.Items[i].ToString());
            }

            if (OnePairReportCheckBox.Checked)
            {
                mIniFile.WriteInt("General", "LinkConfigurationCount", LinkConfigFileList.Items.Count);
                for (int i = 0; i < LinkConfigFileList.Items.Count; i++)
                {
                    key = "LinkConfiguration" + i;
                    mIniFile.WriteString("General", key, LinkConfigFileList.Items[i].ToString());
                }

                mIniFile.WriteInt("General", "EquipmentParameterCount", EquipParameterFileList.Items.Count);
                for (int i = 0; i < EquipParameterFileList.Items.Count; i++)
                {
                    key = "EquipmentParameter" + i;
                    mIniFile.WriteString("General", key, EquipParameterFileList.Items[i].ToString());
                }
            }
        }

        private void OnePairReportCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            mIniFile.WriteInt("General", "AllAnglesMode", (OnePairReportCheckBox.Checked ? 0 : 1));
            if (OnePairReportCheckBox.Checked)
                LoadOnePairConfiguration();

            AnalysisChannelCombinationCheckBox.Enabled = OnePairReportCheckBox.Checked;

            UpdateForm(OnePairReportCheckBox.Checked);
        }

        private void AddLinkButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select Link Configuration Files (*.csv, *.xls, *.xlsx)";
            dlg.Filter = "Link Configuration Files(*.csv, *.xls, *.xlsx)|*.csv;*.xls;*.xlsx";
            dlg.Multiselect = true;
            if (dlg.ShowDialog() == DialogResult.Cancel)
                return;

            int id;
            foreach (string linkConfigurationFile in dlg.FileNames)
            {
                if (!LinkConfigFileList.Items.Contains(linkConfigurationFile))
                {
                    id = LinkConfigFileList.Items.Add(linkConfigurationFile);
                    LinkConfigFileList.SelectedIndex = id;
                }
            }

            StoreFileConfiguration();
        }

        private void LinkConfigFileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemoveLinkButton.Enabled = (LinkConfigFileList.SelectedIndex >= 0);
            ViewLinkButton.Enabled = (LinkConfigFileList.SelectedIndex >= 0);
        }

        private void RemoveLinkButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.Cancel == MessageBox.Show("Are you sure remove Link Configuration file \"" + LinkConfigFileList.SelectedItem.ToString() + " \" ?",
                "Warning", MessageBoxButtons.OKCancel))
                return;

            LinkConfigFileList.Items.RemoveAt(LinkConfigFileList.SelectedIndex);
            StoreFileConfiguration();

            RemoveLinkButton.Enabled = (LinkConfigFileList.SelectedIndex >= 0);
            ViewLinkButton.Enabled = (LinkConfigFileList.SelectedIndex >= 0);
        }

        private void RemoveAllLinkButton_Click(object sender, EventArgs e)
        {
            if (LinkConfigFileList.Items.Count == 0)
                return;

            if (MessageBox.Show("Are you sure remove all Link Configuration files ?", "Warning", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;

            LinkConfigFileList.Items.Clear();
            StoreFileConfiguration();

            RemoveLinkButton.Enabled = false;
            ViewLinkButton.Enabled = false;
        }

        private void ViewLinkButton_Click(object sender, EventArgs e)
        {
            if (LinkConfigFileList.SelectedIndex == -1)
                return;

            List<LinkConfiguration> linkConfigurations;
            string linkConfigurationFile = LinkConfigFileList.SelectedItem.ToString();
            if (mLinkConfigurationFiles.ContainsKey(linkConfigurationFile))
            {
                linkConfigurations = mLinkConfigurationFiles[linkConfigurationFile];
            }
            else
            {
                try
                {
                    linkConfigurations = LinkConfigurationReader.Read(linkConfigurationFile);
                    mLinkConfigurationFiles[linkConfigurationFile] = linkConfigurations;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Link Configuration is invalid !\r\n" + ex.Message);
                    return;
                }
            }

            LinkConfigurationForm linkConfigurationForm = new LinkConfigurationForm(linkConfigurations);
            linkConfigurationForm.ShowDialog();
        }

        private void AddEquipParameterButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select Equipment Parameter Files (*.csv, *.xls, *.xlsx)";
            dlg.Filter = "Equipment Parameter Files(*.csv, *.xls, *.xlsx)|*.csv;*.xls;*.xlsx";
            dlg.Multiselect = true;
            if (dlg.ShowDialog() == DialogResult.Cancel)
                return;

            int id;
            foreach (string equipmentParameterFile in dlg.FileNames)
            {
                if (!EquipParameterFileList.Items.Contains(equipmentParameterFile))
                {
                    id = EquipParameterFileList.Items.Add(equipmentParameterFile);
                    EquipParameterFileList.SelectedIndex = id;
                }
            }

            StoreFileConfiguration();
        }

        private void RemoveEquipParameterButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.Cancel == MessageBox.Show("Are you sure remove Equipment Parameter file \"" + EquipParameterFileList.SelectedItem.ToString() + " \" ?",
                "Warning", MessageBoxButtons.OKCancel))
                return;

            EquipParameterFileList.Items.RemoveAt(EquipParameterFileList.SelectedIndex);
            StoreFileConfiguration();

            RemoveEquipParameterButton.Enabled = (EquipParameterFileList.SelectedIndex >= 0);
            ViewEquipParameterButton.Enabled = (EquipParameterFileList.SelectedIndex >= 0);
        }

        private void RemoveAllEquipParameterButton_Click(object sender, EventArgs e)
        {
            if (EquipParameterFileList.Items.Count == 0)
                return;

            if (MessageBox.Show("Are you sure remove all Equipment Parameter files ?", "Warning", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;

            EquipParameterFileList.Items.Clear();
            StoreFileConfiguration();

            RemoveEquipParameterButton.Enabled = false;
            ViewEquipParameterButton.Enabled = false;
        }

        private void ViewEquipParameterButton_Click(object sender, EventArgs e)
        {
            if (EquipParameterFileList.SelectedIndex == -1)
                return;

            Dictionary<string, EquipmentParameter> equipmentParameters;
            string equipmentParameterFile = EquipParameterFileList.SelectedItem.ToString();
            if (mEquipmentParametersFiles.ContainsKey(equipmentParameterFile))
            {
                equipmentParameters = mEquipmentParametersFiles[equipmentParameterFile];
            }
            else
            {
                try
                {
                    equipmentParameters = EquipmentParameterReader.Read(equipmentParameterFile);
                    mEquipmentParametersFiles[equipmentParameterFile] = equipmentParameters;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Equipment Parameter is invalid !\r\n" + ex.Message);
                    return;
                }
            }

            EquipmentParameterForm equipmentParameterForm = new EquipmentParameterForm(equipmentParameters);
            equipmentParameterForm.ShowDialog();
        }

        private void EquipParameterFileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemoveEquipParameterButton.Enabled = (EquipParameterFileList.SelectedIndex >= 0);
            ViewEquipParameterButton.Enabled = (EquipParameterFileList.SelectedIndex >= 0);
        }

        private void StoreManualConfig()
        {
            mManualConfig = 0;
            if (MC_1_PLUS_0_Checkbox.Checked) mManualConfig = mManualConfig | ManualConfigConstants.MC_1_PLUS_0;
            if (MC_2_PLUS_0_Checkbox.Checked) mManualConfig = mManualConfig | ManualConfigConstants.MC_2_PLUS_0;
            if (MC_3_PLUS_0_Checkbox.Checked) mManualConfig = mManualConfig | ManualConfigConstants.MC_3_PLUS_0;
            if (MC_4_PLUS_0_Checkbox.Checked) mManualConfig = mManualConfig | ManualConfigConstants.MC_4_PLUS_0;
            if (MC_1_PLUS_1FD_Checkbox.Checked) mManualConfig = mManualConfig | ManualConfigConstants.MC_1_PLUS_1FD;
            if (MC_2_PLUS_1FD_Checkbox.Checked) mManualConfig = mManualConfig | ManualConfigConstants.MC_2_PLUS_1FD;
            if (MC_3_PLUS_1FD_Checkbox.Checked) mManualConfig = mManualConfig | ManualConfigConstants.MC_3_PLUS_1FD;
            if (MC_1_PLUS_1HSB_Checkbox.Checked) mManualConfig = mManualConfig | ManualConfigConstants.MC_1_PLUS_1HSB;
            if (MC_1_PLUS_1SD_Checkbox.Checked) mManualConfig = mManualConfig | ManualConfigConstants.MC_1_PLUS_1SD;
            if (MC_DOUBLE_1_PLUS_1SD_Checkbox.Checked) mManualConfig = mManualConfig | ManualConfigConstants.MC_DOUBLE_1_PLUS_1SD;
            if (MC_TRIPLE_1_PLUS_1SD_Checkbox.Checked) mManualConfig = mManualConfig | ManualConfigConstants.MC_TRIPLE_1_PLUS_1SD;
            if (MC_FOURTIMES_1_PLUS_1SD_Checkbox.Checked) mManualConfig = mManualConfig | ManualConfigConstants.MC_FOURTIMES_1_PLUS_1SD;

            mIniFile.WriteInt("General", "ManualConfig", mManualConfig);
        }

        private void LoadManualConfig()
        {
            int manualConfig = mIniFile.ReadInt("General", "ManualConfig", 0);
            if (manualConfig == 0)
                return;

            MC_1_PLUS_0_Checkbox.Checked = (manualConfig & ManualConfigConstants.MC_1_PLUS_0) != 0;
            MC_2_PLUS_0_Checkbox.Checked = (manualConfig & ManualConfigConstants.MC_2_PLUS_0) != 0;
            MC_3_PLUS_0_Checkbox.Checked = (manualConfig & ManualConfigConstants.MC_3_PLUS_0) != 0;
            MC_4_PLUS_0_Checkbox.Checked = (manualConfig & ManualConfigConstants.MC_4_PLUS_0) != 0;
            MC_1_PLUS_1FD_Checkbox.Checked = (manualConfig & ManualConfigConstants.MC_1_PLUS_1FD) != 0;
            MC_2_PLUS_1FD_Checkbox.Checked = (manualConfig & ManualConfigConstants.MC_2_PLUS_1FD) != 0;
            MC_3_PLUS_1FD_Checkbox.Checked = (manualConfig & ManualConfigConstants.MC_3_PLUS_1FD) != 0;
            MC_1_PLUS_1HSB_Checkbox.Checked = (manualConfig & ManualConfigConstants.MC_1_PLUS_1HSB) != 0;
            MC_1_PLUS_1SD_Checkbox.Checked = (manualConfig & ManualConfigConstants.MC_1_PLUS_1SD) != 0;
            MC_DOUBLE_1_PLUS_1SD_Checkbox.Checked = (manualConfig & ManualConfigConstants.MC_DOUBLE_1_PLUS_1SD) != 0;
            MC_TRIPLE_1_PLUS_1SD_Checkbox.Checked = (manualConfig & ManualConfigConstants.MC_TRIPLE_1_PLUS_1SD) != 0;
            MC_FOURTIMES_1_PLUS_1SD_Checkbox.Checked = (manualConfig & ManualConfigConstants.MC_FOURTIMES_1_PLUS_1SD) != 0;
        }

        private void MC_1_PLUS_0_Checkbox_CheckedChanged(object sender, EventArgs e) { StoreManualConfig(); }
        private void MC_2_PLUS_0_Checkbox_CheckedChanged(object sender, EventArgs e) { StoreManualConfig(); }
        private void MC_3_PLUS_0_Checkbox_CheckedChanged(object sender, EventArgs e) { StoreManualConfig(); }
        private void MC_4_PLUS_0_Checkbox_CheckedChanged(object sender, EventArgs e) { StoreManualConfig(); }
        private void MC_1_PLUS_1FD_Checkbox_CheckedChanged(object sender, EventArgs e) { StoreManualConfig(); }
        private void MC_2_PLUS_1FD_Checkbox_CheckedChanged(object sender, EventArgs e) { StoreManualConfig(); }
        private void MC_3_PLUS_1FD_Checkbox_CheckedChanged(object sender, EventArgs e) { StoreManualConfig(); }
        private void MC_1_PLUS_1HSB_Checkbox_CheckedChanged(object sender, EventArgs e) { StoreManualConfig(); }
        private void MC_1_PLUS_1SD_Checkbox_CheckedChanged(object sender, EventArgs e) { StoreManualConfig(); }
        private void MC_DOUBLE_1_PLUS_1SD_Checkbox_CheckedChanged(object sender, EventArgs e) { StoreManualConfig(); }
        private void MC_TRIPLE_1_PLUS_1SD_Checkbox_CheckedChanged(object sender, EventArgs e) { StoreManualConfig(); }
        private void MC_FOURTIMES_1_PLUS_1SD_Checkbox_CheckedChanged(object sender, EventArgs e) { StoreManualConfig(); }

        private void EMIFilesList_MouseMove(object sender, MouseEventArgs e) {ShowListBoxItemTip(sender, e);}
        private void ChannelSettingFileList_MouseMove(object sender, MouseEventArgs e) { ShowListBoxItemTip(sender, e); }
        private void LinkConfigFileList_MouseMove(object sender, MouseEventArgs e) { ShowListBoxItemTip(sender, e); }
        private void EquipParameterFileList_MouseMove(object sender, MouseEventArgs e) { ShowListBoxItemTip(sender, e); }

        private void ShowListBoxItemTip(object sender, MouseEventArgs e)
        {
            /*
            try
            {
                ListBox objListBox = (ListBox)sender;
                int itemIndex = -1;
                if (objListBox.ItemHeight != 0)
                {
                    itemIndex = e.Y / objListBox.ItemHeight;
                    itemIndex += objListBox.TopIndex;
                }
                if (itemIndex >= 0)
                {
                    ListBoxToolTip.SetToolTip(objListBox, objListBox.Items[itemIndex].ToString());
                }
                else
                {
                    ListBoxToolTip.Hide(objListBox);
                }
            }
            catch (Exception ex)
            {
            }
            */
        }

        private void ConfigurationTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ConfigurationTabControl.SelectedIndex == 0)
                mIniFile.WriteInt("General", "UseManualConfig", 0);
            else
                mIniFile.WriteInt("General", "UseManualConfig", 1);
        }

        private void AnalysisChannelCombinationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            mIniFile.WriteInt("General", "AnalysisChannelCombination", AnalysisChannelCombinationCheckBox.Checked ? 1 : 0);
        }

        private void GeneralRadioButton_Click(object sender, EventArgs e)
        {
            mIniFile.WriteInt("General", "IsGeneralReport", 1);

            OnePairReportCheckBox.Enabled = true;
            AnalysisChannelCombinationCheckBox.Enabled = true;
            OtherTypeComboBox.Enabled = false;

            OnePairReportCheckBox_CheckedChanged(null, null);
        }

        private void OtherRadioButton_Click(object sender, EventArgs e)
        {
            mIniFile.WriteInt("General", "IsGeneralReport", 0);
            OnePairReportCheckBox.Enabled = false;
            AnalysisChannelCombinationCheckBox.Enabled = false;
            OtherTypeComboBox.Enabled = true;
            UpdateForm(false);
        }

        private void OtherTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mIniFile.WriteInt("General", "OtherReportType", OtherTypeComboBox.SelectedIndex);
        }
    }
}
