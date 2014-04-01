using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEMIAnalyzer.EMI;
using System.IO;
using Utils;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;
using System.Globalization;
using System.Diagnostics;

namespace WatsEmiReportTool
{
    public partial class PairReportForm : Form
    {
        private IniFile mIniFile = new IniFile(".\\WatsEmiReportTool.ini");
        private EMIFileData mEmiA;
        private EMIFileData mEmiB;
        private double mAzimuthA;
        private double mAzimuthB;
        private List<ChannelSetting> mChannelSettings;
        private bool mIsManualConfig = false;
        private int mManualConfig;
        private List<LinkConfiguration> mLinkConfigurations;
        private Dictionary<string, EquipmentParameter> mEquipmentParameters 
            = new Dictionary<string,EquipmentParameter>();
        private LimitSetting mLimitSetting;

        private EmiAzimuthData mEmiAzimuthDataA;
        private EmiAzimuthData mEmiAzimuthDataB;

        private List<ChannelSetting> mAvailableChannels;
        private List<int> mFrequencyCounts;

        private WatsEmiDataManager mDataManagerA = new WatsEmiDataManager();
        private WatsEmiDataManager mDataManagerB = new WatsEmiDataManager();

        private List<LinkCombination> mLinkcombinations;

        List<BitMapInfo> mBmpInfosA, mBmpInfosB;

        List<List<string>> mAllCombinationDirectionMasks;

        private string mExportFileName;

        private Thread mExportThread;
        private bool mCancelExport = false;
        private ExportStatusForm mExportStatusForm;
        private delegate void UpdateStatusDelegate(string status);

        public LimitSetting LimitSetting
        {
            set { mLimitSetting = value; }
        }

        public bool AnalysisChannelCombination
        {
            get;
            set;
        }

        public EMIFileData EmiA
        {
            set { mEmiA = value; }
        }

        public EMIFileData EmiB
        {
            set { mEmiB = value; }
        }

        public double AzimuthA
        {
            set { mAzimuthA = value; }
        }

        public double AzimuthB
        {
            set { mAzimuthB = value; }
        }

        public List<ChannelSetting> ChannelSettings
        {
            set { mChannelSettings = value; }
        }

        public List<ChannelSetting> AvailableChannels
        {
            get { return mAvailableChannels; }
        }

        public bool IsManualConfig
        {
            set { mIsManualConfig = value; }
        }

        public int ManualConfig
        {
            set { mManualConfig = value; }
        }

        public List<LinkConfiguration> LinkConfigurations
        {
            set { mLinkConfigurations = value; }
        }

        public Dictionary<string, EquipmentParameter> EquipmentParameters
        {
            set { mEquipmentParameters = value; }
        }

        public PairReportForm()
        {
            InitializeComponent();
            GraphSpanEditor.Text = mIniFile.ReadString("General", "PairReportSpan", "500").Trim();
            if (!Regex.IsMatch(GraphSpanEditor.Text, @"^[1-9]\d*(\.\d+)?$"))
                GraphSpanEditor.Text = "500";

            mExportStatusForm = new ExportStatusForm(this);
        }

        public void Init()
        {
            mEmiAzimuthDataA = GetEmiAzimuthData(mEmiA, mAzimuthA);
            mEmiAzimuthDataB = GetEmiAzimuthData(mEmiB, mAzimuthB);
            mAvailableChannels = GetAvailableChannels();
        }

        public void UpdateStatus(string status)
        {
            this.BeginInvoke(new UpdateStatusDelegate(mExportStatusForm.UpdateStatus), status);
        }

        public void CancelExport()
        {
            try
            {
                mCancelExport = true;
                mExportThread.Join(10 * 1000);
                if (mExportThread.IsAlive)
                {
                    mExportThread.Abort();
                }
            }
            catch (System.Exception e)
            {

            }
        }

        private EmiAzimuthData GetEmiAzimuthData(EMIFileData emiData, double azimuth)
        {
            EmiAzimuthData emiAzimuthData = new EmiAzimuthData();

            emiAzimuthData.Azimuth = azimuth;
            emiAzimuthData.StartFreq = double.MaxValue;
            emiAzimuthData.EndFreq = double.MinValue;
            emiAzimuthData.HorizontalSamples = new List<WatsEmiSample>();
            emiAzimuthData.VerticalSamples = new List<WatsEmiSample>();
            emiAzimuthData.ChannelDatas = new Dictionary<ChannelSetting, WatsEmiData>();
            ChannelSetting curChannelSetting;
            WatsEmiData curData;
            foreach (DG_Type dataGroup in emiData.DataGroups)
            {
                if (dataGroup.DG_FB_Angle != azimuth)
                    continue;

                if (dataGroup.DG_FB_Start < emiAzimuthData.StartFreq)
                    emiAzimuthData.StartFreq = dataGroup.DG_FB_Start;
                if (dataGroup.DG_FB_End > emiAzimuthData.EndFreq)
                    emiAzimuthData.EndFreq = dataGroup.DG_FB_End;

                foreach (DG_Data_Type data in dataGroup.DGDatas)
                {
                    if (dataGroup.DB_FB_AntennaPolarization == 0)
                        emiAzimuthData.VerticalSamples.Add(new WatsEmiSample(data.DG_DI_Freq, data.DG_DI_RSSI));
                    else //if (dataGroup.DB_FB_AntennaPolarization == 1)
                        emiAzimuthData.HorizontalSamples.Add(new WatsEmiSample(data.DG_DI_Freq, data.DG_DI_RSSI));

                    curChannelSetting = null;
                    foreach (ChannelSetting channelSetting in mChannelSettings)
                    {
                        if (data.DG_DI_Freq >= channelSetting.StartFreq
                            && data.DG_DI_Freq <= channelSetting.EndFreq
                            || data.DG_DI_Freq >= channelSetting.Pair.StartFreq
                            && data.DG_DI_Freq <= channelSetting.Pair.EndFreq)
                        {
                            curChannelSetting = channelSetting;
                            break;
                        }
                    }
                    if (curChannelSetting == null)
                        continue;

                    if (!emiAzimuthData.ChannelDatas.TryGetValue(curChannelSetting, out curData))
                    {
                        curData = new WatsEmiData();
                        emiAzimuthData.ChannelDatas.Add(curChannelSetting, curData);
                    }
                    if (dataGroup.DB_FB_AntennaPolarization == 0)
                    {
                        if (data.DG_DI_Freq >= curChannelSetting.StartFreq
                            && data.DG_DI_Freq <= curChannelSetting.EndFreq)
                            curData.mVSamples.Add(new WatsEmiSample(data.DG_DI_Freq, data.DG_DI_RSSI));
                        else
                            curData.mVPairSamples.Add(new WatsEmiSample(data.DG_DI_Freq, data.DG_DI_RSSI));
                    }
                    else
                    {
                        if (data.DG_DI_Freq >= curChannelSetting.StartFreq
                            && data.DG_DI_Freq <= curChannelSetting.EndFreq)
                            curData.mHSamples.Add(new WatsEmiSample(data.DG_DI_Freq, data.DG_DI_RSSI));
                        else
                            curData.mHPairSamples.Add(new WatsEmiSample(data.DG_DI_Freq, data.DG_DI_RSSI));
                    }
                }
            }

            return emiAzimuthData;
        }

        private List<ChannelSetting> GetAvailableChannels()
        {
            ChannelPower powerA, powerB;
            List<ChannelSetting> availableChannels = new List<ChannelSetting>();
            foreach (ChannelSetting channelSetting in mChannelSettings)
            {
                if (!mEmiAzimuthDataA.ChannelDatas.ContainsKey(channelSetting)
                    || !mEmiAzimuthDataB.ChannelDatas.ContainsKey(channelSetting))
                    continue;

                powerA = new ChannelPower(mEmiA.SA_RBW, channelSetting, mLimitSetting,
                    mEmiAzimuthDataA.ChannelDatas[channelSetting]);
                powerB = new ChannelPower(mEmiB.SA_RBW, channelSetting, mLimitSetting,
                    mEmiAzimuthDataB.ChannelDatas[channelSetting]);

                if (powerA.IsValidHPower && powerA.IsValidVPower && powerA.IsValidVPairPower && powerA.IsValidHPairPower
                    && powerB.IsValidHPower && powerB.IsValidVPower && powerB.IsValidVPairPower && powerB.IsValidHPairPower)
                    availableChannels.Add(channelSetting);
            }

            return availableChannels;
        }

        private void AddManualConfiguration(int manualConfig)
        {
            LinkConfiguration linkConfiguration = new LinkConfiguration();
            linkConfiguration.RequiredConfiguration = ManualConfigConstants.GetRequiredConfiguration(manualConfig);
            mLinkConfigurations.Add(linkConfiguration);
        }

        private void PairReportForm_Load(object sender, EventArgs e)
        {
            SiteAIDLabel.Text = mEmiA.Site_ID;
            LongtitudeALabel.Text = Utility.ConvertLongtitude(mEmiA.Site_Longitude);
            LatitudeALabel.Text = Utility.ConvertLatitude(mEmiA.Site_Longitude);
            DateALabel.Text = mEmiA.PA_TestTime;
            EngineerALabel.Text = mEmiA.PA_UserName;

            SiteBIDLabel.Text = mEmiB.Site_ID;
            LongtitudeBLabel.Text = Utility.ConvertLongtitude(mEmiB.Site_Longitude);
            LatitudeBLabel.Text = Utility.ConvertLatitude(mEmiB.Site_Longitude);
            DateBLabel.Text = mEmiB.PA_TestTime;
            EngineerBLabel.Text = mEmiB.PA_UserName;

            ChannelMatcher.Instance.Channels = mAvailableChannels;
            ChannelMatcher.Instance.EquipmentParameters = mEquipmentParameters;
            mFrequencyCounts = new List<int>();
            if (mIsManualConfig)
            {
                mLinkConfigurations = new List<LinkConfiguration>();
                if ((mManualConfig & ManualConfigConstants.MC_1_PLUS_0) != 0)
                {
                    mFrequencyCounts.Add(LinkFrequencyCountMap.Instance.GetCount(ManualConfigConstants.MC_1_PLUS_0));
                    AddManualConfiguration(ManualConfigConstants.MC_1_PLUS_0);
                }
                if ((mManualConfig & ManualConfigConstants.MC_2_PLUS_0) != 0)
                {
                    mFrequencyCounts.Add(LinkFrequencyCountMap.Instance.GetCount(ManualConfigConstants.MC_2_PLUS_0));
                    AddManualConfiguration(ManualConfigConstants.MC_2_PLUS_0);
                }
                if ((mManualConfig & ManualConfigConstants.MC_3_PLUS_0) != 0)
                {
                    mFrequencyCounts.Add(LinkFrequencyCountMap.Instance.GetCount(ManualConfigConstants.MC_3_PLUS_0));
                    AddManualConfiguration(ManualConfigConstants.MC_3_PLUS_0);
                }
                if ((mManualConfig & ManualConfigConstants.MC_4_PLUS_0) != 0)
                {
                    mFrequencyCounts.Add(LinkFrequencyCountMap.Instance.GetCount(ManualConfigConstants.MC_4_PLUS_0));
                    AddManualConfiguration(ManualConfigConstants.MC_4_PLUS_0);
                }
                if ((mManualConfig & ManualConfigConstants.MC_1_PLUS_1FD) != 0)
                {
                    mFrequencyCounts.Add(LinkFrequencyCountMap.Instance.GetCount(ManualConfigConstants.MC_1_PLUS_1FD));
                    AddManualConfiguration(ManualConfigConstants.MC_1_PLUS_1FD);
                }
                if ((mManualConfig & ManualConfigConstants.MC_2_PLUS_1FD) != 0)
                {
                    mFrequencyCounts.Add(LinkFrequencyCountMap.Instance.GetCount(ManualConfigConstants.MC_2_PLUS_1FD));
                    AddManualConfiguration(ManualConfigConstants.MC_2_PLUS_1FD);
                }
                if ((mManualConfig & ManualConfigConstants.MC_3_PLUS_1FD) != 0)
                {
                    mFrequencyCounts.Add(LinkFrequencyCountMap.Instance.GetCount(ManualConfigConstants.MC_3_PLUS_1FD));
                    AddManualConfiguration(ManualConfigConstants.MC_3_PLUS_1FD);
                }
                if ((mManualConfig & ManualConfigConstants.MC_1_PLUS_1HSB) != 0)
                {
                    mFrequencyCounts.Add(LinkFrequencyCountMap.Instance.GetCount(ManualConfigConstants.MC_1_PLUS_1HSB));
                    AddManualConfiguration(ManualConfigConstants.MC_1_PLUS_1HSB);
                }
                if ((mManualConfig & ManualConfigConstants.MC_1_PLUS_1SD) != 0)
                {
                    mFrequencyCounts.Add(LinkFrequencyCountMap.Instance.GetCount(ManualConfigConstants.MC_1_PLUS_1SD));
                    AddManualConfiguration(ManualConfigConstants.MC_1_PLUS_1SD);
                }
                if ((mManualConfig & ManualConfigConstants.MC_DOUBLE_1_PLUS_1SD) != 0)
                {
                    mFrequencyCounts.Add(LinkFrequencyCountMap.Instance.GetCount(ManualConfigConstants.MC_DOUBLE_1_PLUS_1SD));
                    AddManualConfiguration(ManualConfigConstants.MC_DOUBLE_1_PLUS_1SD);
                }
                if ((mManualConfig & ManualConfigConstants.MC_TRIPLE_1_PLUS_1SD) != 0)
                {
                    mFrequencyCounts.Add(LinkFrequencyCountMap.Instance.GetCount(ManualConfigConstants.MC_TRIPLE_1_PLUS_1SD));
                    AddManualConfiguration(ManualConfigConstants.MC_TRIPLE_1_PLUS_1SD);
                }
                if ((mManualConfig & ManualConfigConstants.MC_FOURTIMES_1_PLUS_1SD) != 0)
                {
                    mFrequencyCounts.Add(LinkFrequencyCountMap.Instance.GetCount(ManualConfigConstants.MC_FOURTIMES_1_PLUS_1SD));
                    AddManualConfiguration(ManualConfigConstants.MC_FOURTIMES_1_PLUS_1SD);
                }
            }
            else
            {
                foreach (LinkConfiguration linkConfiguration in mLinkConfigurations)
                    mFrequencyCounts.Add(LinkFrequencyCountMap.Instance.GetCount(linkConfiguration.RequiredConfiguration));
            }

            Link parentLink = new Link();
            int[] availableChannelIndexs = new int[mAvailableChannels.Count];
            for (int i = 0; i < mAvailableChannels.Count; i++)
                availableChannelIndexs[i] = i;

            if (AnalysisChannelCombination)
                GetAllLinks(parentLink, availableChannelIndexs, 0, 0);

            mDataManagerA = Utility.GetEmiDataManager(mEmiA, mChannelSettings);
            mDataManagerB = Utility.GetEmiDataManager(mEmiB, mChannelSettings);

            GenerateGraphs();

            ShowGraph(true);
            ShowGraph(false);

            if (AnalysisChannelCombination)
            {
                mAllCombinationDirectionMasks = DirectionMasker.Instance().GetAllCombinateDirectionMasks(mFrequencyCounts);
                mLinkcombinations = GetAllLinkCombinations(parentLink);
                ShowLinkCombinations();
            }
        }

        private void ShowLinkCombinations()
        {
            int rowId;
            bool printLinkId;
            foreach (LinkCombination linkCombination in mLinkcombinations)
            {
                foreach (Link link in linkCombination.Links)
                {
                    printLinkId = true;
                    foreach (Link.Channel channel in link.Channels)
                    {
                        rowId = ChannelCombinationGrid.Rows.Add();
                        ChannelCombinationGrid.Rows[rowId].Cells["CombinationNoColumn"].Value = linkCombination.CombinationNo.ToString();

                        if (printLinkId)
                        {
                            ChannelCombinationGrid.Rows[rowId].Cells["LinkIDColumn"].Value = link.LinkID;
                            printLinkId = false;
                        }

                        ChannelCombinationGrid.Rows[rowId].Cells["ChannelIDColumn"].Value = channel.ChannelID;
                        ChannelCombinationGrid.Rows[rowId].Cells["TXColumn"].Value = channel.TX;
                        ChannelCombinationGrid.Rows[rowId].Cells["RXColumn"].Value = channel.RX;
                        ChannelCombinationGrid.Rows[rowId].Cells["ResultColumn"].Value = channel.Result;
                        ChannelCombinationGrid.Rows[rowId].Cells["SubBandColumn"].Value = channel.SubBand;
                    }
                }
            }
        }

        private bool IsLinkAllChannelsMatched(Link link)
        {
            if (link.Channels.Count == 1)
                return true;

            foreach (Link.Channel channel1 in link.Channels)
            {
                foreach (Link.Channel channel2 in link.Channels)
                {
                    if (channel1 == channel2)
                        continue;

                    if (!ChannelMatcher.Instance.IsMatch(channel1.ChannelID, channel2.ChannelID))
                        return false;
                }
            }
            return true;
        }

        private bool IsLinkAllDirectionMatched(Link link)
        {
            if (link.Channels.Count == 1)
                return true;

            for(int i = 0; i < link.Channels.Count - 1; i++)
            {
                if (Utility.IsChannelClosed(link.Channels[i].ChannelID, link.Channels[i + 1].ChannelID)
                    && link.Channels[i].Result.Equals(link.Channels[i + 1].Result))
                    return false;
            }

            return true;
        }

        private List<LinkCombination> GetResultCombinations(LinkCombination linkCombination)
        {
            List<LinkCombination> combinations = new List<LinkCombination>();

            List<Link.Channel> channels;
            string masks;
            bool isValidLinkCombination;
            LinkCombination newLinkCombination;
            foreach (List<string> directionMasks in mAllCombinationDirectionMasks)
            {
                isValidLinkCombination = true;
                for(int i = 0; i < linkCombination.Links.Count; i++)
                {
                    masks = directionMasks[i];
                    channels = linkCombination.Links[i].Channels;
                    for (int j = 0; j < masks.Length; j++)
                        channels[j].Result = (masks[j] == '1') ? "V":"H";

                    if (!IsLinkAllDirectionMatched(linkCombination.Links[i]))
                    {
                        isValidLinkCombination = false;
                        break;
                    }
                }

                if (isValidLinkCombination)
                {
                    newLinkCombination = LinkCombination.Clone(linkCombination);
                    combinations.Add(newLinkCombination);
                }
            }

            return combinations;
        }

        private List<LinkCombination> GetAllLinkCombinations(Link parentLink)
        {
            int combinationNo = 1;
            List<LinkCombination> linkCombinations = new List<LinkCombination>();
            List<Link> midLinks = new List<Link>();
            CombinateLinks(linkCombinations, midLinks, parentLink);

            List<LinkCombination> allLinkCombinations = new List<LinkCombination>();
            bool isValidCombination;
            foreach (LinkCombination combination in linkCombinations)
            {
                isValidCombination = true;
                foreach (Link link in combination.Links)
                {
                    if (!IsLinkAllChannelsMatched(link))
                    {
                        isValidCombination = false;
                        break;
                    }
                }

                if (!isValidCombination)
                    continue;

                List<LinkCombination> newLinkCombinations = GetResultCombinations(combination);
                foreach (LinkCombination newCombination in newLinkCombinations)
                {
                    newCombination.CombinationNo = combinationNo;
                    allLinkCombinations.Add(newCombination);
                    combinationNo++;
                }
            }

            return allLinkCombinations;
        }

        private void CombinateLinks(List<LinkCombination> combinations, List<Link> midLinks, Link curLink)
        {
            if (curLink.NextLinks == null || curLink.NextLinks.Count == 0)
            {
                LinkCombination combination = new LinkCombination();
                foreach (Link link in midLinks)
                    combination.Links.Add(link);
                combination.Links.Add(curLink);
                combinations.Add(combination);
            }
            else
            {
                foreach (Link link in curLink.NextLinks)
                {
                    if (link.NextLinks != null && link.NextLinks.Count > 0)
                        midLinks.Add(link);
                    CombinateLinks(combinations, midLinks, link);
                    if (link.NextLinks != null && link.NextLinks.Count > 0)
                        midLinks.Remove(link);
                }
            }
        }

        private void GenerateGraphs()
        {
            int minAbsRssi = Int32.MaxValue, maxAbsRssi = Int32.MinValue;
            Dictionary<int, List<WatsEmiSample>> samples;
            foreach (KeyValuePair<double, Dictionary<int, List<WatsEmiSample>>> pair in mDataManagerA.AllSamples)
            {
                samples = pair.Value;
                foreach (WatsEmiSample sample in samples[0])
                {
                    if (Math.Abs(sample.mRssi) < minAbsRssi)
                        minAbsRssi = (int)Math.Abs(sample.mRssi);
                    if (Math.Abs(sample.mRssi) > maxAbsRssi)
                        maxAbsRssi = (int)Math.Abs(sample.mRssi);
                }

                foreach (WatsEmiSample sample in samples[1])
                {
                    if (Math.Abs(sample.mRssi) < minAbsRssi)
                        minAbsRssi = (int)Math.Abs(sample.mRssi);
                    if (Math.Abs(sample.mRssi) > maxAbsRssi)
                        maxAbsRssi = (int)Math.Abs(sample.mRssi);
                }
            }

            foreach (KeyValuePair<double, Dictionary<int, List<WatsEmiSample>>> pair in mDataManagerB.AllSamples)
            {
                samples = pair.Value;
                foreach (WatsEmiSample sample in samples[0])
                {
                    if (Math.Abs(sample.mRssi) < minAbsRssi)
                        minAbsRssi = (int)Math.Abs(sample.mRssi);
                    if (Math.Abs(sample.mRssi) > maxAbsRssi)
                        maxAbsRssi = (int)Math.Abs(sample.mRssi);
                }

                foreach (WatsEmiSample sample in samples[1])
                {
                    if (Math.Abs(sample.mRssi) < minAbsRssi)
                        minAbsRssi = (int)Math.Abs(sample.mRssi);
                    if (Math.Abs(sample.mRssi) > maxAbsRssi)
                        maxAbsRssi = (int)Math.Abs(sample.mRssi);
                }
            }

            minAbsRssi -= 10;
            minAbsRssi = minAbsRssi - minAbsRssi % 10;
            maxAbsRssi += 10;
            maxAbsRssi = maxAbsRssi - maxAbsRssi % 10;

            double span = double.Parse(GraphSpanEditor.Text.Trim());
            //Dictionary<ChannelSetting, WatsEmiData> channelDatasA = mDataManagerA.AllChannelSamples[mAzimuthA];
            //Dictionary<ChannelSetting, WatsEmiData> channelDatasB = mDataManagerB.AllChannelSamples[mAzimuthB];

            double channelStartFreq = mChannelSettings[0].StartFreq;
            double channelEndFreq = mChannelSettings[mChannelSettings.Count - 1].EndFreq;
            if (span > channelEndFreq - channelStartFreq)
                span = channelEndFreq - channelStartFreq;
            List<FrequencyRange> ranges = new List<FrequencyRange>();
            FrequencyRange range;
            double startFreq = channelStartFreq;
            do
            {
                range = new FrequencyRange();
                range.FromFreq = startFreq;
                range.EndFreq = startFreq + span;
//                 if (range.EndFreq >= channelEndFreq)
//                     range.EndFreq = channelEndFreq;
                
                ranges.Add(range);
                if (range.EndFreq >= channelEndFreq)
                    break;
                startFreq = startFreq + span;

            } while (true);

            mBmpInfosA = ReportPictureCreator.create(mAzimuthA, mDataManagerA.AllSamples[mAzimuthA], 
                mChannelSettings, false, minAbsRssi, maxAbsRssi, ranges, channelEndFreq);
            mBmpInfosB = ReportPictureCreator.create(mAzimuthB, mDataManagerB.AllSamples[mAzimuthB],
                mChannelSettings, false, minAbsRssi, maxAbsRssi, ranges, channelEndFreq);
            
            FrequencyBandComboxA.Items.Clear();
            FrequencyBandComboxB.Items.Clear();
            foreach (BitMapInfo bitMapInfo in mBmpInfosA)
                FrequencyBandComboxA.Items.Add(bitMapInfo.Band);
            foreach (BitMapInfo bitMapInfo in mBmpInfosB)
                FrequencyBandComboxB.Items.Add(bitMapInfo.Band);
            if (FrequencyBandComboxA.Items.Count > 0)
                FrequencyBandComboxA.SelectedIndex = 0;
            if (FrequencyBandComboxB.Items.Count > 0)
                FrequencyBandComboxB.SelectedIndex = 0;
        }

        private void ShowGraph(bool graphA)
        {
            if (graphA)
            {
                if (FrequencyBandComboxA.SelectedIndex == -1)
                {
                    VerticalAPictureBox.Image = (System.Drawing.Image)Properties.Resources.blank;
                    HorizontalAPictureBox.Image = (System.Drawing.Image)Properties.Resources.blank;
                    VerticalALabel.Text = "";
                    HorizontalALabel.Text = "";
                    return;
                }

                VerticalALabel.Text = mBmpInfosA[FrequencyBandComboxA.SelectedIndex].Title1;
                VerticalAPictureBox.ImageLocation = mBmpInfosA[FrequencyBandComboxA.SelectedIndex].BmpFile1;
                VerticalALabel.Left = VerticalAPictureBox.Left + (VerticalAPictureBox.Width - VerticalALabel.Width) / 2;

                HorizontalALabel.Text = mBmpInfosA[FrequencyBandComboxA.SelectedIndex].Title2;
                HorizontalAPictureBox.ImageLocation = mBmpInfosA[FrequencyBandComboxA.SelectedIndex].BmpFile2;
                HorizontalALabel.Left = HorizontalAPictureBox.Left + (HorizontalAPictureBox.Width - HorizontalALabel.Width) / 2;
            }
            else
            {
                if (FrequencyBandComboxB.SelectedIndex == -1)
                {
                    VerticalBPictureBox.Image = (System.Drawing.Image)Properties.Resources.blank;
                    HorizontalBPictureBox.Image = (System.Drawing.Image)Properties.Resources.blank;
                    VerticalBLabel.Text = "";
                    HorizontalBLabel.Text = "";
                    return;
                }

                VerticalBLabel.Text = mBmpInfosB[FrequencyBandComboxB.SelectedIndex].Title1;
                VerticalBPictureBox.ImageLocation = mBmpInfosB[FrequencyBandComboxB.SelectedIndex].BmpFile1;
                VerticalBLabel.Left = VerticalBPictureBox.Left + (VerticalBPictureBox.Width - VerticalBLabel.Width) / 2;

                HorizontalBLabel.Text = mBmpInfosB[FrequencyBandComboxB.SelectedIndex].Title2;
                HorizontalBPictureBox.ImageLocation = mBmpInfosB[FrequencyBandComboxB.SelectedIndex].BmpFile2;
                HorizontalBLabel.Left = HorizontalBPictureBox.Left + (HorizontalBPictureBox.Width - HorizontalBLabel.Width) / 2;
            }
        }

        private void FrequencyBandComboxA_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowGraph(true);
        }

        private void FrequencyBandComboxB_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowGraph(false);
        }

        private void ClearTempFiles()
        {
            try
            {
                DirectoryInfo tempFolder = new DirectoryInfo(Utility.GetAppPath() + "\\Temp");
                foreach (FileInfo fileInfo in tempFolder.GetFiles())
                {
                    try
                    {
                        File.Delete(fileInfo.FullName);
                    }
                    catch (System.Exception ex)
                    {

                    }
                }
            }
            catch (System.Exception e)
            {

            }
        }

        private void PairReportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearTempFiles();
            MainForm.Instance.Show();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            Utility.ClearTempFiles();

            VerticalAPictureBox.ImageLocation = null;
            HorizontalAPictureBox.ImageLocation = null;
            VerticalBPictureBox.ImageLocation = null;
            HorizontalBPictureBox.ImageLocation = null;
            GraphSpanEditor.Text = GraphSpanEditor.Text.Trim();
            if (GraphSpanEditor.Text.Length == 0)
            {
                MessageBox.Show("Please input span value !", "Warning");
                GraphSpanEditor.Focus();
                return;
            }
            if (!Regex.IsMatch(GraphSpanEditor.Text, @"^[1-9]\d*(\.\d+)?$"))
            {
                MessageBox.Show(GraphSpanEditor.Text + " is not a valid span value !", "Warning");
                GraphSpanEditor.SelectAll();
                GraphSpanEditor.Focus();
                return;
            }

            mIniFile.WriteString("General", "PairReportSpan", GraphSpanEditor.Text.Trim());

            GenerateGraphs();
        }

        private void GetAllLinks(Link parentLink, int[] availableChannelIndexs, int linkID, int frequencyCountIndex)
        {
            List<int[]> remainIndexsList;
            List<Link> linkList = GetLinks(linkID, availableChannelIndexs, mFrequencyCounts[frequencyCountIndex], out remainIndexsList);
            parentLink.NextLinks = linkList;
            
            if (remainIndexsList.Count == 0)
                return;

            linkID++;
            frequencyCountIndex++;
            if (frequencyCountIndex == mFrequencyCounts.Count)
                return;

            Link link;
            for (int i = 0; i < linkList.Count; i++)
            {
                link = linkList[i];
                GetAllLinks(link, remainIndexsList[i], linkID, frequencyCountIndex);
            }
        }

        private List<Link> GetLinks(int linkID, int[] channelIndexs, int num, out List<int[]> remainIndexsList)
        {
            remainIndexsList = new List<int[]>();
            int[] remainIndexs;

            List<Link> links = new List<Link>();

            List<int[]> combinations = Algorithms.PermutationAndCombination<int>.GetCombination(channelIndexs, num);
            Link link;
            Link.Channel channel;
            ChannelSetting channelSetting;
            
            int remainIndexsCount;
            foreach (int[] combination in combinations)
            {
                link = new Link();
                link.LinkID = "Link " + linkID;
                
                foreach (int index in combination)
                {
                    channelSetting = mAvailableChannels[index];

                    channel = new Link.Channel();
                    channel.ChannelID = channelSetting.ChannelName;
                    channel.SubBand = channelSetting.ODUSubBand;
                    channel.TX = channelSetting.CenterFreq;
                    channel.RX = channelSetting.Pair.CenterFreq;

                    link.Channels.Add(channel);
                }
                link.Sort();
                links.Add(link);

                remainIndexsCount = channelIndexs.Length - combination.Length;
                if (remainIndexsCount == 0)
                    continue;
                remainIndexs = new int[remainIndexsCount];
                int i = 0;
                foreach (int index in channelIndexs)
                {
                    if (!FindInArray(combination, index))
                        remainIndexs[i++] = index;
                }
                remainIndexsList.Add(remainIndexs);
            }

            return links;
        }

        private static bool FindInArray(int[] array, int value)
        {
            bool find = false;
            foreach (int el in array)
            {
                if (value == el)
                {
                    find = true;
                    break;
                }
            }
            return find;
        }

        private void VerticalAPictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || VerticalAPictureBox.ImageLocation == null)
                return;

            PictureForm form = new PictureForm();
            form.ImageLocation = VerticalAPictureBox.ImageLocation;
            form.Title = VerticalALabel.Text;
            form.ShowDialog();
        }

        private void HorizontalAPictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || HorizontalAPictureBox.ImageLocation == null)
                return;

            PictureForm form = new PictureForm();
            form.ImageLocation = HorizontalAPictureBox.ImageLocation;
            form.Title = HorizontalALabel.Text;
            form.ShowDialog();
        }

        private void VerticalBPictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || VerticalBPictureBox.ImageLocation == null)
                return;

            PictureForm form = new PictureForm();
            form.ImageLocation = VerticalBPictureBox.ImageLocation;
            form.Title = VerticalBLabel.Text;
            form.ShowDialog();
        }

        private void HorizontalBPictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || HorizontalBPictureBox.ImageLocation == null)
                return;

            PictureForm form = new PictureForm();
            form.ImageLocation = HorizontalBPictureBox.ImageLocation;
            form.Title = HorizontalBLabel.Text;
            form.ShowDialog();
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            if (Utility.GetExcelVersion() < 0)
            {
                MessageBox.Show("Excel was not installed !");
                return;
            }

            ExcelExportSettingForm exportSettingForm = new ExcelExportSettingForm();
            if (exportSettingForm.ShowDialog() == DialogResult.Cancel)
                return;

            string reportTemplateFile;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Select report file name";

            if (exportSettingForm.ExportOffice2003)
            {
                reportTemplateFile = System.AppDomain.CurrentDomain.BaseDirectory
                 + "1HopReportTemplate.xls";
                saveFileDialog.Filter = "report file(*.xls)|*.xls";
            }
            else
            {
                reportTemplateFile = System.AppDomain.CurrentDomain.BaseDirectory
                 + "1HopReportTemplate.xlsx";
                saveFileDialog.Filter = "report file(*.xlsx)|*.xlsx";
            }

            do
            {
                if (DialogResult.Cancel == saveFileDialog.ShowDialog())
                    return;

                if (saveFileDialog.FileName.Equals(reportTemplateFile,
                    StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Can't select report template file !");
                    continue;
                }

                try
                {
                    File.Copy(reportTemplateFile, saveFileDialog.FileName, true);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Can't save file " + saveFileDialog.FileName
                        + "!\r\n" + "Select another file name for report.");
                    continue;
                }

                mExportFileName = saveFileDialog.FileName;
                break;

            } while (true);

            Hide();
            mExportStatusForm.Show();
            mCancelExport = false;

            mExportThread = new Thread(delegate()
            {
                bool isReportSucceed = false;
                string[] subBands = new string[2];

                System.Globalization.CultureInfo Oldci = null;
                Excel._Application app = null;
                Excel.WorkbookClass workBook = null;
                Excel.Sheets sheets = null;
                Excel.Worksheet sheet = null;
                Excel.Worksheet summarySheet;
                Excel.Worksheet combinationSheet;
                try
                {
                    Oldci = System.Threading.Thread.CurrentThread.CurrentCulture;
                    System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");

                    app = new Excel.Application();
                    app.DisplayAlerts = false;
                    object objOpt = System.Reflection.Missing.Value;

                    UpdateStatus("Initialize ...");
                    workBook = (Excel.WorkbookClass)app.Workbooks.Open(
                        mExportFileName, objOpt, false, objOpt, objOpt, objOpt, true,
                        objOpt, objOpt, true, objOpt, objOpt, objOpt, objOpt, objOpt);

                    sheets = workBook.Worksheets;

                    summarySheet = (Excel.Worksheet)sheets["Summary"];
                    summarySheet.Cells[1, 4] = mEmiA.Site_ID;
                    summarySheet.Cells[1, 12] = mEmiB.Site_ID;

                    combinationSheet = (Excel.Worksheet)sheets["available channels combination"];
                    if (AnalysisChannelCombination)
                        UpdateCombinationSheet(combinationSheet);
                    else
                        combinationSheet.Delete();

                    UpdateStatus("Export Cover sheet ...");
                    /* Cover Sheet */
                    sheet = (Excel.Worksheet)sheets["Cover"];
                    if (!string.IsNullOrEmpty(mEmiA.PI_ID))
                        sheet.Cells[15, 5] = mEmiA.PI_ID;
                    if (!string.IsNullOrEmpty(mEmiA.PA_UserName))
                        sheet.Cells[17, 5] = mEmiA.PA_UserName;

                    double startFrequencyA = double.MaxValue;
                    double startFrequencyB = double.MaxValue;
                    double endFrequencyA = double.MinValue;
                    double endFrequencyB = double.MinValue;
                    WatsEmiDataManager dataManagerA, dataManagerB;
                    dataManagerA = Utility.GetEmiDataManager(mEmiA, mChannelSettings);
                    dataManagerB = Utility.GetEmiDataManager(mEmiB, mChannelSettings);
                    foreach (DG_Type dataGroup in mEmiA.DataGroups)
                    {
                        if (dataGroup.DG_FB_Angle != mAzimuthA)
                            continue;
                        if (dataGroup.DG_FB_Start < startFrequencyA)
                            startFrequencyA = dataGroup.DG_FB_Start;
                        if (dataGroup.DG_FB_End > endFrequencyA)
                            endFrequencyA = dataGroup.DG_FB_End;
                    }

                    foreach (DG_Type dataGroup in mEmiB.DataGroups)
                    {
                        if (dataGroup.DG_FB_Angle != mAzimuthB)
                            continue;
                        if (dataGroup.DG_FB_Start < startFrequencyB)
                            startFrequencyB = dataGroup.DG_FB_Start;
                        if (dataGroup.DG_FB_End > endFrequencyB)
                            endFrequencyB = dataGroup.DG_FB_End;
                    }

                    for (int i = 1; i < mChannelSettings.Count; i++)
                    {
                        if (mCancelExport)
                            return;

                        ((Excel.Range)summarySheet.Rows[3, objOpt]).Copy(objOpt);
                        ((Excel.Range)summarySheet.Cells[3 + i, 1]).EntireRow.Insert(objOpt, objOpt);
                    }

                    for (int i = 0; i < mChannelSettings.Count; i++)
                    {
                        subBands[0] = "";
                        subBands[1] = "";
                        if (mChannelSettings[i].ODUSubBand.Length == 1)
                            subBands[0] = mChannelSettings[i].ODUSubBand;
                        else if (mChannelSettings[i].ODUSubBand.Length == 3)
                        {
                            subBands[0] = mChannelSettings[i].ODUSubBand.Substring(0, 1);
                            subBands[1] = mChannelSettings[i].ODUSubBand.Substring(2);
                        }
                        summarySheet.Cells[3 + i, 1] = subBands[0];
                        summarySheet.Cells[3 + i, 2] = subBands[1];
                        
                        summarySheet.Cells[3 + i, 3] = mChannelSettings[i].ChannelName;
                        summarySheet.Cells[3 + i, 4] = mChannelSettings[i].CenterFreq.ToString();
                        summarySheet.Cells[3 + i, 7] = mChannelSettings[i].Pair.ChannelName;
                        summarySheet.Cells[3 + i, 8] = mChannelSettings[i].Pair.CenterFreq.ToString();
                        summarySheet.Cells[3 + i, 11] = mChannelSettings[i].ChannelName;
                        summarySheet.Cells[3 + i, 12] = mChannelSettings[i].CenterFreq.ToString();
                        summarySheet.Cells[3 + i, 15] = mChannelSettings[i].Pair.ChannelName;
                        summarySheet.Cells[3 + i, 16] = mChannelSettings[i].Pair.CenterFreq.ToString();

                        summarySheet.Cells[3 + i, 5] = summarySheet.Cells[3 + i, 6] 
                            = summarySheet.Cells[3 + i, 9] = summarySheet.Cells[3 + i, 10]
                            = summarySheet.Cells[3 + i, 13] = summarySheet.Cells[3 + i, 14]
                            = summarySheet.Cells[3 + i, 17] = summarySheet.Cells[3 + i, 18]
                            = summarySheet.Cells[3 + i, 19] = summarySheet.Cells[3 + i, 20]
                            = "X";
                    }

                    UpdateStatus("Export Device Info sheet ...");
                    /* Device Info Sheet */
                    sheet = (Excel.Worksheet)sheets["Device info"];
                    sheet.Cells[18, 12] = mEmiA.SA_RBW + "kHz";
                    sheet.Cells[19, 12] = mEmiA.SA_VBW + "kHz";
                    sheet.Cells[20, 12] = mEmiA.SA_Detector;
                    sheet.Cells[21, 12] = mEmiA.SA_Trace;
                    sheet.Cells[22, 12] = mEmiA.SA_Attenuation_Value + "dB";
                    sheet.Cells[23, 12] = mEmiA.SA_REF_LEVEL + "dBm";

                    List<EMIFileData> emis = new List<EMIFileData>();
                    emis.Add(mEmiA);
                    emis.Add(mEmiB);
                    List<double> startFrequencies = new List<double>();
                    startFrequencies.Add(startFrequencyA);
                    startFrequencies.Add(startFrequencyB);
                    List<double> endFrequencies = new List<double>();
                    endFrequencies.Add(endFrequencyA);
                    endFrequencies.Add(endFrequencyB);
                    List<double> azimuths = new List<double>();
                    azimuths.Add(mAzimuthA);
                    azimuths.Add(mAzimuthB);
                    List<List<BitMapInfo>> bitmapInfos = new List<List<BitMapInfo>>();
                    bitmapInfos.Add(mBmpInfosA);
                    bitmapInfos.Add(mBmpInfosB);
                    List<WatsEmiDataManager> dataManagers = new List<WatsEmiDataManager>();
                    dataManagers.Add(dataManagerA);
                    dataManagers.Add(dataManagerB);

                    for (int i = 0; i < 2; i++)
                    {
                        UpdateStatus("Export Information of EMI file '" + emis[i].Site_ID + "'");
                        sheet = (Excel.Worksheet)sheets["EMI" + (i+1).ToString()];
                        sheet.Name = emis[i].Site_ID;
                        sheet.Cells[2, 3] = emis[i].Site_ID;
                        sheet.Cells[2, 11] = emis[i].Site_ID;
                        sheet.Cells[3, 3] = emis[i].Site_Address;
                        sheet.Cells[4, 3] = Utility.ConvertLatitude(emis[i].Site_Latitude);
                        sheet.Cells[4, 11] = Utility.ConvertLongtitude(emis[i].Site_Longitude);
                        if (mLimitSetting.UseChannelPowerLimit)
                            sheet.Cells[5, 11] = mLimitSetting.ChannelPowerLimit.ToString();
                        else
                            sheet.Cells[5, 11] = "";

                        if (mLimitSetting.UseDeltaPowerLimit)
                            sheet.Cells[5, 15] = mLimitSetting.DeltaPowerLimit.ToString();
                        else
                            sheet.Cells[5, 15] = "";
                        sheet.Cells[6, 3] = Utility.ConvertToDate(emis[i].PA_TestTime);
                        sheet.Cells[6, 11] = emis[i].PA_UserName;

                        int channelIndex = 0;
                        Excel.Range range;
                        Dictionary<ChannelSetting, WatsEmiData> channelSamples = dataManagers[i].AllChannelSamples[azimuths[i]];
                        foreach (KeyValuePair<ChannelSetting, WatsEmiData> channelSamplePair in channelSamples)
                        {
                            if (mCancelExport)
                                return;

                            if (channelIndex > 0)
                            {
                                ((Excel.Range)sheet.Rows[11, objOpt]).Copy(objOpt);
                                ((Excel.Range)sheet.Cells[11 + channelIndex, 1]).EntireRow.Insert(objOpt, objOpt);
                            }
                            sheet.Cells[11 + channelIndex, 3] = channelSamplePair.Key.ChannelName;
                            sheet.Cells[11 + channelIndex, 4] = channelSamplePair.Key.CenterFreq;
                            sheet.Cells[11 + channelIndex, 5] = channelSamplePair.Key.BandWidth;

                            //channel pair
                            sheet.Cells[11 + channelIndex, 10] = channelSamplePair.Key.Pair.ChannelName;
                            sheet.Cells[11 + channelIndex, 11] = channelSamplePair.Key.Pair.CenterFreq;
                            sheet.Cells[11 + channelIndex, 12] = channelSamplePair.Key.Pair.BandWidth;

                            ChannelPower channelPower = new ChannelPower(emis[i].SA_RBW, channelSamplePair.Key, mLimitSetting, channelSamplePair.Value);
                            sheet.Cells[11 + channelIndex, 8] = channelPower.HPower;
                            sheet.Cells[11 + channelIndex, 15] = channelPower.HPairPower;
                            sheet.Cells[11 + channelIndex, 6] = channelPower.VPower;
                            sheet.Cells[11 + channelIndex, 13] = channelPower.VPairPower;

                            if (!channelPower.IsValidVPower)
                            {
                                sheet.Cells[11 + channelIndex, 7] = "X";
                            }
                            else
                            {
                                sheet.Cells[11 + channelIndex, 7] = "";
                            }

                            if (!channelPower.IsValidHPower)
                            {
                                sheet.Cells[11 + channelIndex, 9] = "X";
                            }
                            else
                            {
                                sheet.Cells[11 + channelIndex, 9] = "";
                            }

                            if (!channelPower.IsValidVPairPower)
                            {
                                sheet.Cells[11 + channelIndex, 14] = "X";
                            }
                            else
                            {
                                sheet.Cells[11 + channelIndex, 14] = "";
                            }

                            if (!channelPower.IsValidHPairPower)
                            {
                                sheet.Cells[11 + channelIndex, 16] = "X";
                            }
                            else
                            {
                                sheet.Cells[11 + channelIndex, 16] = "";
                            }

                            UpdateSummarySheet(summarySheet, channelSamplePair.Key, i == 0,
                                channelPower.IsValidVPower, channelPower.IsValidHPower,
                                channelPower.IsValidVPairPower, channelPower.IsValidHPairPower);

                            channelIndex++;
                        }

                        if (channelIndex > 1)
                        {
                            range = sheet.get_Range(sheet.Cells[11, 1], sheet.Cells[11 + channelIndex - 1, 1]);
                            range.ClearContents();
                            range.Merge(objOpt);

                            range = sheet.get_Range(sheet.Cells[11, 2], sheet.Cells[11 + channelIndex - 1, 2]);
                            range.ClearContents();
                            range.Merge(objOpt);
                        }

                        sheet.Cells[11, 1] = startFrequencies[i].ToString() + "-" + endFrequencies[i].ToString();
                        sheet.Cells[11, 2] = azimuths[i].ToString() + "\x00B0";

                        int pictureRows = bitmapInfos[i].Count;
                        if (pictureRows > 1)
                        {
                            for (int j = 0; j < pictureRows - 1; j++)
                            {
                                ((Excel.Range)sheet.Rows[12 + channelIndex, objOpt]).Copy(objOpt);
                                ((Excel.Range)sheet.Cells[13 + channelIndex + j, 1]).EntireRow.Insert(objOpt, objOpt);
                            }
                        }

                        for (int j = 0; j < pictureRows; j++)
                        {
                            UpdateStatus("Export sheet " + azimuths[i].ToString() + "\x00B0 vertical picture "
                                + (j + 1).ToString() + " ...");
                            if (mCancelExport)
                                return;

                            sheet.Cells[12 + channelIndex + j, 1] = bitmapInfos[i][j].Title1;
                            range = (Excel.Range)sheet.Cells[12 + channelIndex + j, 1];
                            //range.Select();

                            float left = Convert.ToSingle(range.Left) + 15;
                            float top = Convert.ToSingle(range.Top) + 15;
                            float width = 310;
                            float height = 150;

                            sheet.Shapes.AddPicture(bitmapInfos[i][j].BmpFile1, Microsoft.Office.Core.MsoTriState.msoFalse,
                                Microsoft.Office.Core.MsoTriState.msoTrue, left, top, width, height);


                            UpdateStatus("Export sheet " + azimuths[i] + "\x00B0 horizontal picture "
                                + (j + 1).ToString() + " ...");

                            if (mCancelExport)
                                return;

                            sheet.Cells[12 + channelIndex + j, 9] = bitmapInfos[i][j].Title2;
                            range = (Excel.Range)sheet.Cells[12 + channelIndex + j, 9];
                            //range.Select();

                            left = Convert.ToSingle(range.Left) + 15;
                            top = Convert.ToSingle(range.Top) + 15;
                            width = 310;
                            height = 150;

                            sheet.Shapes.AddPicture(bitmapInfos[i][j].BmpFile2, Microsoft.Office.Core.MsoTriState.msoFalse,
                                Microsoft.Office.Core.MsoTriState.msoTrue, left, top, width, height);
                        }
                    }
                    

                    isReportSucceed = true;
                    UpdateStatus("Save export file, please wait ...");
                    workBook.Save();

                    UpdateStatus("Export succeed !");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Create report failed !\r\nException: " + ex.Message);
                }
                finally
                {
                    if (Oldci != null)
                    {
                        System.Threading.Thread.CurrentThread.CurrentCulture = Oldci;
                    }

                    if (app != null)
                        app.Quit();
                    ExcelAppKiller.Kill(app);

                    Utility.ReleaseCom(sheet);
                    Utility.ReleaseCom(sheets);
                    Utility.ReleaseCom(workBook);
                    Utility.ReleaseCom(app);

                    GC.Collect(System.GC.GetGeneration(sheet));
                    GC.Collect(System.GC.GetGeneration(sheets));
                    GC.Collect(System.GC.GetGeneration(workBook));
                    GC.Collect(System.GC.GetGeneration(app));

                    GC.Collect();

                    /*
                    ReleaseCom(sheet);
                    ReleaseCom(sheets);
                    ReleaseCom(workBook);
                    if (app != null)
                        app.Quit();
                    ExcelAppKiller.Kill(app);
                    ReleaseCom(app);

                    GC.Collect(); 
                    */
                }

                if (isReportSucceed)
                {
                    try
                    {
                        UpdateStatus("Open excel ...");

                        Process process = Process.Start("excel", "\"" + mExportFileName + "\"");
                        process.Close();
                    }
                    catch (System.Exception ex)
                    {

                    }
                }

                UpdateStatus("Finished");
            }
            );

            mExportThread.Start();
        }

        private void ChannelCombinationGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 对第1列相同单元格进行合并
            if (e.ColumnIndex == 0 && e.RowIndex != -1 && !ChannelCombinationGrid.Rows[e.RowIndex].IsNewRow)
            {
                /*
                if (e.RowIndex < ChannelCombinationGrid.Rows.Count - 1 
                    && ChannelCombinationGrid.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value == null)
                    return;
                */

                try
                {
                    using (Brush gridBrush = new SolidBrush(this.ChannelCombinationGrid.GridColor),
                    backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                    {
                        using (Pen gridLinePen = new Pen(gridBrush))
                        {
                            // 清除单元格
                            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

                            // 画 Grid 边线（仅画单元格的底边线和右边线）
                            //   如果下一行和当前行的数据不同，则在当前的单元格画一条底边线
                            if (e.RowIndex < ChannelCombinationGrid.Rows.Count - 1 &&
                                ChannelCombinationGrid.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value.ToString() != e.Value.ToString())
                                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                                e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                                e.CellBounds.Bottom - 1);
                            // 画右边线
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                                e.CellBounds.Top, e.CellBounds.Right - 1,
                                e.CellBounds.Bottom);

                            // 画（填写）单元格内容，相同的内容的单元格只填写第一个
                            if (e.Value != null)
                            {
                                if (e.RowIndex > 0 && ChannelCombinationGrid.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value.ToString() == e.Value.ToString())
                                { }
                                else
                                {
                                    e.Graphics.DrawString((String)e.Value, e.CellStyle.Font,
                                        Brushes.Black, e.CellBounds.X + 2,
                                        e.CellBounds.Y + 5, StringFormat.GenericDefault);
                                }
                            }
                            e.Handled = true;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                	
                }

                
            }
        }

        private void UpdateSummarySheet(Excel.Worksheet summarySheet, ChannelSetting curChannel, bool isFirstEmi,
            bool isValidVPower, bool isValidHPower, bool isValidVPairPower, bool isValidHPairPower)
        {
            int row = -1;
            for(int i = 0; i < mChannelSettings.Count; i++)
            {
                if (((Excel.Range)summarySheet.Cells[3 + i, 3]).Text.ToString().Equals(curChannel.ChannelName, StringComparison.OrdinalIgnoreCase))
                {
                    row = i;
                    break;
                }
            }
            
            if (row == -1)
                return;

            if (isFirstEmi)
            {
                summarySheet.Cells[3 + row, 5] = (isValidVPower ? "" : "X");
                summarySheet.Cells[3 + row, 6] = (isValidHPower ? "" : "X");
                summarySheet.Cells[3 + row, 9] = (isValidVPairPower ? "" : "X");
                summarySheet.Cells[3 + row, 10] = (isValidHPairPower ? "" : "X");
            }
            else
            {
                summarySheet.Cells[3 + row, 13] = (isValidVPower ? "" : "X");
                summarySheet.Cells[3 + row, 14] = (isValidHPower ? "" : "X");
                summarySheet.Cells[3 + row, 17] = (isValidVPairPower ? "" : "X");
                summarySheet.Cells[3 + row, 18] = (isValidHPairPower ? "" : "X");
            }

            if (((Excel.Range)summarySheet.Cells[3 + row, 5]).Text.ToString().Length == 0
                && ((Excel.Range)summarySheet.Cells[3 + row, 9]).Text.ToString().Length == 0
                && ((Excel.Range)summarySheet.Cells[3 + row, 13]).Text.ToString().Length == 0
                && ((Excel.Range)summarySheet.Cells[3 + row, 17]).Text.ToString().Length == 0)
            {
                summarySheet.Cells[3 + row, 19] = "";
            }

            if (((Excel.Range)summarySheet.Cells[3 + row, 6]).Text.ToString().Length == 0
                && ((Excel.Range)summarySheet.Cells[3 + row, 10]).Text.ToString().Length == 0
                && ((Excel.Range)summarySheet.Cells[3 + row, 14]).Text.ToString().Length == 0
                && ((Excel.Range)summarySheet.Cells[3 + row, 18]).Text.ToString().Length == 0)
            {
                summarySheet.Cells[3 + row, 20] = "";
            }
        }

        private void UpdateCombinationSheet(Excel.Worksheet combinationSheet)
        {
            List<Excel.Range> boldBorderRanges = new List<Excel.Range>();

            object objOpt = System.Reflection.Missing.Value;

            if (mLinkConfigurations.Count == 0)
                return;
            bool isParallelLink = (mLinkConfigurations.Count >= 2);

            combinationSheet.Cells[1, 4] = isParallelLink ? "Yes" : "No";
            for (int i = 1; i < mLinkConfigurations.Count; i++)
            {
                ((Excel.Range)combinationSheet.Rows[2, objOpt]).Copy(objOpt);
                ((Excel.Range)combinationSheet.Cells[2 + i, 1]).EntireRow.Insert(objOpt, objOpt);
            }

            for(int i = 0; i < mLinkConfigurations.Count; i++)
            {
                combinationSheet.Cells[2 + i, 1] 
                    = "Link " + (i+1) + " additional required configuration";
                combinationSheet.Cells[2 + i, 6] = mLinkConfigurations[i].RequiredConfiguration;
            }

            int totalRequiredChannelQty = 0;
            foreach (LinkConfiguration linkConfiguration in mLinkConfigurations)
                totalRequiredChannelQty += LinkFrequencyCountMap.Instance.GetCount(linkConfiguration.RequiredConfiguration);
            combinationSheet.Cells[2 + mLinkConfigurations.Count, 6] = totalRequiredChannelQty.ToString();

            int rowCount = mLinkcombinations.Count * totalRequiredChannelQty;
            for (int i = 1; i < rowCount; i++)
            {
                ((Excel.Range)combinationSheet.Rows[6 + mLinkConfigurations.Count, objOpt]).Copy(objOpt);
                ((Excel.Range)combinationSheet.Cells[6 + mLinkConfigurations.Count + i, 1]).EntireRow.Insert(objOpt, objOpt);
            }

            boldBorderRanges.Add(combinationSheet.get_Range(combinationSheet.Cells[5 + mLinkConfigurations.Count, 1],
                combinationSheet.Cells[5 + mLinkConfigurations.Count, 7]));

            Excel.Range range;
            int curRowIndex = 6 + mLinkConfigurations.Count;
            foreach (LinkCombination combination in mLinkcombinations)
            {
                combinationSheet.Cells[curRowIndex, 1] = combination.CombinationNo.ToString();
                foreach (Link link in combination.Links)
                {
                    combinationSheet.Cells[curRowIndex, 2] = link.LinkID;
                    foreach (Link.Channel channel in link.Channels)
                    {
                        combinationSheet.Cells[curRowIndex, 3] = channel.ChannelID;
                        combinationSheet.Cells[curRowIndex, 4] = channel.TX.ToString();
                        combinationSheet.Cells[curRowIndex, 5] = channel.RX.ToString();
                        combinationSheet.Cells[curRowIndex, 6] = channel.Result;
                        combinationSheet.Cells[curRowIndex, 7] = channel.SubBand;
                        curRowIndex++;
                    }

                    if (link.Channels.Count >= 2)
                    {
                        range = combinationSheet.get_Range(
                            combinationSheet.Cells[curRowIndex - link.Channels.Count, 2],
                            combinationSheet.Cells[curRowIndex - 1, 2]);
                        range.Merge(objOpt);
                    }
                }

                if (totalRequiredChannelQty > 1)
                {
                    range = combinationSheet.get_Range(
                        combinationSheet.Cells[curRowIndex - totalRequiredChannelQty, 1],
                        combinationSheet.Cells[curRowIndex - 1, 1]);
                    range.Merge(objOpt);
                }
                
                boldBorderRanges.Add(combinationSheet.get_Range(
                    combinationSheet.Cells[curRowIndex - totalRequiredChannelQty, 1],
                    combinationSheet.Cells[curRowIndex - 1, 7]));
            }

            foreach (Excel.Range boldBolderRange in boldBorderRanges)
            {
                boldBolderRange.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).Weight = Excel.XlBorderWeight.xlMedium;
                boldBolderRange.Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).Weight = Excel.XlBorderWeight.xlMedium;
                boldBolderRange.Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft).Weight = Excel.XlBorderWeight.xlMedium;
                boldBolderRange.Borders.get_Item(Excel.XlBordersIndex.xlEdgeRight).Weight = Excel.XlBorderWeight.xlMedium;
            }
        }
    }
}
