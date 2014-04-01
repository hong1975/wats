using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEMIAnalyzer.EMI;
using WatsEMIAnalyzer.Bindings;
using WatsEMIAnalyzer.Model;
using WatsEmiReportTool;
using System.Text.RegularExpressions;
using System.Threading;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Globalization;
using System.Diagnostics;
using WatsEMIAnalyzer.HTTP;

namespace WatsEMIAnalyzer
{
    public partial class ReportViewForm : WeifenLuo.WinFormsUI.Docking.DockContent, IExport
    {
        private bool mIsViewReport;
        private Report mViewReport;

        private Report mReport;
        public Report Report
        {
            get { return mReport; } 
        }

        private TreeNode mAnalysisViewNode;
        private BindingTask mTask;
        private EMIFileData mEmiFileData;
        private List<ChannelSetting> mChannelSettings;

        private LimitSetting mLimitSetting = new LimitSetting();

        private double mDefaultStartFreq;
        private double mDefaultEndFreq;

        private WatsEmiDataManager mDataManager;
        private Dictionary<double, List<BitMapInfo>> mBitmaps = new Dictionary<double, List<BitMapInfo>>();

        private string mExportSpan;
        private string mStartFreq;
        private string mEndFreq;

        private Thread mExportThread;
        private bool mCancelExport = false;
        private string mExportFileName;
        private delegate void UpdateStatusDelegate(string status);

        private ExportStatusForm mExportStatusForm;

        public ReportViewForm(TreeNode analysisViewNode)
        {
            InitializeComponent();

            mAnalysisViewNode = analysisViewNode;
            SingleAnalysisTabPage.Text = "360\x00B0Analysis";

            mTask = ((TaskModel)analysisViewNode.Parent.Parent.Tag).mTask;
            mChannelSettings = DataCenter.Instance().ChannelSettings[mTask.ChannelSettingID];

            mIsViewReport = false;
        }

        public ReportViewForm(Report report)
        {
            InitializeComponent();
            SingleAnalysisTabPage.Text = "360\x00B0Analysis";
            mIsViewReport = true;
            mViewReport = report;

            mTask = DataCenter.Instance().Tasks[report.TaskID];
            mChannelSettings = DataCenter.Instance().ChannelSettings[report.ChannelSettingID];
            mEmiFileData = DataCenter.Instance().EMIs[mViewReport.EmiFileID];

            mLimitSetting = mViewReport.LimitSetting;
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

        private void ReportViewForm_Load(object sender, EventArgs e)
        {
            if (!mIsViewReport)
            {
                ChannelPowerCheckBox.Checked = Program.Config.ReadInt("General", "UseChannelPowerLimit", 0) == 1;
                ChannelPowerLimitEditor.Text = Program.Config.ReadString("General", "ChannelPowerLimit", "-85").Trim();
                DeltaPowerCheckBox.Checked = Program.Config.ReadInt("General", "UseDeltaPowerLimit", 0) == 1;
                DeltaPowerLimitEditor.Text = Program.Config.ReadString("General", "DeltaPowerLimit", "-3").Trim();

                List<EMIFileData> emiFileDatas = Utility.GetTaskAvailableEmiFiles(mTask);
                foreach (EMIFileData emiFileData in emiFileDatas)
                    EMIFileComboBox.Items.Add(emiFileData);

                ChannelSettingTextBox.Text = DataCenter.Instance().ChannelSettingDescriptions[mTask.ChannelSettingID].ToString();
            }
            else
            {
                ChannelPowerCheckBox.Checked = mViewReport.LimitSetting.UseChannelPowerLimit;
                ChannelPowerLimitEditor.Text = mViewReport.LimitSetting.ChannelPowerLimit.ToString();
                DeltaPowerCheckBox.Checked = mViewReport.LimitSetting.UseDeltaPowerLimit;
                DeltaPowerLimitEditor.Text = mViewReport.LimitSetting.DeltaPowerLimit.ToString();

                EMIFileComboBox.Items.Add(mEmiFileData);
                EMIFileComboBox.SelectedIndex = 0;
                EMIFileComboBox.Enabled = false;
                LoadButton.Visible = false;

                ChannelSettingTextBox.Text = DataCenter.Instance().ChannelSettingDescriptions[mViewReport.ChannelSettingID].ToString();
                mTask = DataCenter.Instance().Tasks[mViewReport.TaskID];

                AnalysisButton.Enabled = true;

                DisplayChannelCheckBox.Checked = mViewReport.IsDisplayChannel;
                StartFreqEditor.Text = WatsEmiReportTool.Utility.ConvertDoubleString(mViewReport.StartFreq);
                EndFreqEditor.Text = WatsEmiReportTool.Utility.ConvertDoubleString(mViewReport.EndFreq);
                SpanEditor.Text = mViewReport.Span.ToString();

                CommitButton.Visible = false;

                AnalysisButton_Click(null, null);
            }
            
        }

        private void EMIFileComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EMIFileDetailButton.Enabled = (EMIFileComboBox.SelectedIndex >= 0);
            if (EMIFileComboBox.SelectedIndex >= 0)
                mEmiFileData = (EMIFileData)EMIFileComboBox.SelectedItem;

            AnalysisButton.Enabled = (mEmiFileData != null && mChannelSettings != null);
        }

        private void EMIFileDetailButton_Click(object sender, EventArgs e)
        {
            EMIDetailForm detailForm = new EMIDetailForm(mEmiFileData);
            detailForm.ShowDialog();
        }

        private void ChannelSettingDetailButton_Click(object sender, EventArgs e)
        {
            ChannelSettingDetailForm channelSettingDetailForm = new ChannelSettingDetailForm(mChannelSettings);
            channelSettingDetailForm.ShowDialog();
        }

        private void AnalysisButton_Click(object sender, EventArgs e)
        {
            ChannelPowerLimitEditor.Text = ChannelPowerLimitEditor.Text.Trim();
            if (ChannelPowerLimitEditor.Text.Length == 0)
            {
                MessageBox.Show("Please input channel power limit !", "Warning");
                ChannelPowerLimitEditor.Focus();
                return;
            }
            if (!Regex.IsMatch(ChannelPowerLimitEditor.Text, @"^-?[1-9]\d*$"))
            {
                MessageBox.Show(ChannelPowerLimitEditor.Text + " is not a valid power limit !", "Warning");
                ChannelPowerLimitEditor.SelectAll();
                ChannelPowerLimitEditor.Focus();
                return;
            }

            DeltaPowerLimitEditor.Text = DeltaPowerLimitEditor.Text.Trim();
            if (DeltaPowerLimitEditor.Text.Length == 0)
            {
                MessageBox.Show("Please input delta power limit !", "Warning");
                DeltaPowerLimitEditor.Focus();
                return;
            }
            if (!Regex.IsMatch(DeltaPowerLimitEditor.Text, @"^-?[1-9]\d*$"))
            {
                MessageBox.Show(DeltaPowerLimitEditor.Text + " is not a valid delta limit !", "Warning");
                DeltaPowerLimitEditor.SelectAll();
                DeltaPowerLimitEditor.Focus();
                return;
            }

            if (!mIsViewReport)
            {
                Program.Config.WriteInt("General", "UseChannelPowerLimit", ChannelPowerCheckBox.Checked ? 1 : 0);
                Program.Config.WriteString("General", "ChannelPowerLimit", ChannelPowerLimitEditor.Text.Trim());
                Program.Config.WriteInt("General", "UseDeltaPowerLimit", DeltaPowerCheckBox.Checked ? 1 : 0);
                Program.Config.WriteString("General", "DeltaPowerLimit", DeltaPowerLimitEditor.Text.Trim());
            }
            
            mLimitSetting.UseChannelPowerLimit = ChannelPowerCheckBox.Checked;
            mLimitSetting.ChannelPowerLimit = Int32.Parse(ChannelPowerLimitEditor.Text);
            mLimitSetting.UseDeltaPowerLimit = DeltaPowerCheckBox.Checked;
            mLimitSetting.DeltaPowerLimit = Int32.Parse(DeltaPowerLimitEditor.Text);

            SiteIDLabel.Text = mEmiFileData.Site_ID;
            SiteNameLabel.Text = mEmiFileData.Site_ID;
            AddressLabel.Text = mEmiFileData.Site_Address;
            DateLabel.Text = mEmiFileData.PA_TestTime;
            EngineerLabel.Text = mEmiFileData.PA_UserName;
            LongtitudeLabel.Text = WatsEmiReportTool.Utility.ConvertLongtitude(mEmiFileData.Site_Longitude);
            LatitudeLabel.Text = WatsEmiReportTool.Utility.ConvertLatitude(mEmiFileData.Site_Longitude); ;

            if (mLimitSetting.UseChannelPowerLimit)
                PChannelLimitLabel.Text = mLimitSetting.ChannelPowerLimit.ToString();
            else
                PChannelLimitLabel.Text = "";

            if (mLimitSetting.UseDeltaPowerLimit)
                LevelLimitLabel.Text = mLimitSetting.DeltaPowerLimit.ToString();
            else
                LevelLimitLabel.Text = "";

            if (Program.Config.ReadInt("General", "ChannelPreferred", 1) == 1)
                ChannelPreferredRadioButton.Checked = true;
            else
                FrequencePreferredRadioButton.Checked = true;

            DisplayChannelCheckBox.Enabled = (!ChannelPreferredRadioButton.Checked);

            if (!mIsViewReport)
            {
                DisplayChannelCheckBox.Checked = (Program.Config.ReadInt("General", "DisplayChannel", 1) == 1);
                SpanEditor.Text = Program.Config.ReadString("General", "Span", "300").Trim();
                if (!Regex.IsMatch(SpanEditor.Text, @"^[1-9]\d*(\.\d+)?$"))
                    SpanEditor.Text = "300";                
            }

            mDefaultStartFreq = mChannelSettings[0].StartFreq;
            mDefaultEndFreq = mChannelSettings[mChannelSettings.Count - 1].Pair.EndFreq;
            SetDefaultFreq();

            mExportStatusForm = new ExportStatusForm(this);

            mDataManager = WatsEmiReportTool.Utility.GetEmiDataManager(mEmiFileData, mChannelSettings);
            GenerateGraphs();
        }

        private void SetDefaultFreq()
        {
            StartFreqEditor.Text = WatsEmiReportTool.Utility.ConvertDoubleString(mDefaultStartFreq);
            EndFreqEditor.Text = WatsEmiReportTool.Utility.ConvertDoubleString(mDefaultEndFreq);
        }

        private void DefaultButton_Click(object sender, EventArgs e)
        {
            SetDefaultFreq();
        }

        private void GenerateGraphs()
        {
            int minAbsRssi = Int32.MaxValue, maxAbsRssi = Int32.MinValue;
            Dictionary<int, List<WatsEmiSample>> samples;
            foreach (KeyValuePair<double, Dictionary<int, List<WatsEmiSample>>> pair in mDataManager.AllSamples)
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

            double span = double.Parse(SpanEditor.Text.Trim());
            List<BitMapInfo> bmpInfos;
            double angle;
            List<WatsEmiReportTool.FrequencyRange> ranges;
            WatsEmiReportTool.FrequencyRange range;
            if (ChannelPreferredRadioButton.Checked)
            {
                mBitmaps.Clear();
                foreach (KeyValuePair<double, Dictionary<ChannelSetting, WatsEmiData>> pair in mDataManager.AllChannelSamples)
                {
                    angle = pair.Key;
                    double channelStartFreq = pair.Value.Keys.First<ChannelSetting>().StartFreq;
                    double channelEndFreq = pair.Value.Keys.Last<ChannelSetting>().Pair.EndFreq;
                    if (span > channelEndFreq - channelStartFreq)
                        span = channelEndFreq - channelStartFreq;

                    ranges = new List<WatsEmiReportTool.FrequencyRange>();
                    double startFreq = channelStartFreq;
                    do
                    {
                        range = new WatsEmiReportTool.FrequencyRange();
                        range.FromFreq = startFreq;
                        range.EndFreq = startFreq + span;

                        //if (range.EndFreq >= channelEndFreq)
                        //  range.EndFreq = channelEndFreq;

                        ranges.Add(range);

                        if (range.EndFreq >= channelEndFreq)
                            break;
                        startFreq = startFreq + span;
                    } while (true);

                    bmpInfos = WatsEmiReportTool.ReportPictureCreator.create(angle, mDataManager.AllSamples[angle],
                        mChannelSettings, true, minAbsRssi, maxAbsRssi, ranges, channelEndFreq);
                    mBitmaps[angle] = bmpInfos;
                }
            }
            else
            {
                mBitmaps.Clear();
                double startFreq = double.Parse(StartFreqEditor.Text.Trim());
                double endFreq = double.Parse(EndFreqEditor.Text.Trim());
                if (span > endFreq - startFreq)
                    span = endFreq - startFreq;
                bool displayChannel = DisplayChannelCheckBox.Checked;

                foreach (KeyValuePair<double, Dictionary<int, List<WatsEmiSample>>> pair in mDataManager.AllSamples)
                {
                    angle = pair.Key;
                    ranges = new List<FrequencyRange>();
                    do
                    {
                        range = new FrequencyRange();
                        range.FromFreq = startFreq;
                        range.EndFreq = startFreq + span;
                        //                         if (range.EndFreq >= endFreq)
                        //                             range.EndFreq = endFreq;

                        ranges.Add(range);

                        if (range.EndFreq >= endFreq)
                            break;
                        startFreq = startFreq + span;
                    } while (true);

                    bmpInfos = ReportPictureCreator.create(angle, mDataManager.AllSamples[angle],
                            mChannelSettings, displayChannel, minAbsRssi, maxAbsRssi, ranges, endFreq);
                    mBitmaps[angle] = bmpInfos;
                }
            }

            AngleCombox.Items.Clear();
            BandList.Items.Clear();
            foreach (KeyValuePair<double, List<BitMapInfo>> pair in mBitmaps)
            {
                AngleCombox.Items.Add(pair.Key.ToString());
                foreach (BitMapInfo info in pair.Value)
                {
                    BandList.Items.Add(info.Band);
                }
            }

            if (AngleCombox.Items.Count > 0)
                AngleCombox.SelectedIndex = 0;

            if (BandList.Items.Count > 0)
                BandList.SelectedIndex = 0;
        }

        private void ChannelPreferredRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            DisplayChannelCheckBox.Enabled
                = StartFreqEditor.Enabled
                = EndFreqEditor.Enabled
                = DefaultButton.Enabled
                = FrequencePreferredRadioButton.Checked;
        }

        private void AngleCombox_SelectedIndexChanged(object sender, EventArgs e)
        {
            BandList.Items.Clear();
            if (AngleCombox.SelectedIndex != -1)
            {
                double angle = double.Parse(AngleCombox.SelectedItem.ToString());
                foreach (BitMapInfo info in mBitmaps[angle])
                {
                    BandList.Items.Add(info.Band);
                }

                if (BandList.Items.Count > 0)
                    BandList.SelectedIndex = 0;
            }

            ShowGraph();
        }

        private void ShowGraph()
        {
            if (AngleCombox.SelectedIndex == -1 || BandList.SelectedIndex == -1)
            {
                HorizontalPicture.Image = (System.Drawing.Image)Properties.Resources.blank;
                VerticalPicture.Image = (System.Drawing.Image)Properties.Resources.blank;
                HorizontalPictureTitleLabel.Text = "";
                VerticalPictureTitleLabel.Text = "";
                return;
            }

            double angle = double.Parse(AngleCombox.SelectedItem.ToString());

            VerticalPictureTitleLabel.Text = mBitmaps[angle][BandList.SelectedIndex].Title1;
            //VerticalPicture.Image = Image.FromFile(mBitmaps[angle][BandList.SelectedIndex].BmpFile1);
            VerticalPicture.ImageLocation = mBitmaps[angle][BandList.SelectedIndex].BmpFile1;
            VerticalPictureTitleLabel.Left = VerticalPicture.Left + (VerticalPicture.Width - VerticalPictureTitleLabel.Width) / 2;

            HorizontalPictureTitleLabel.Text = mBitmaps[angle][BandList.SelectedIndex].Title2;
            //HorizontalPicture.Image = Image.FromFile(mBitmaps[angle][BandList.SelectedIndex].BmpFile2);
            HorizontalPicture.ImageLocation = mBitmaps[angle][BandList.SelectedIndex].BmpFile2;
            HorizontalPictureTitleLabel.Left = HorizontalPicture.Left + (HorizontalPicture.Width - HorizontalPictureTitleLabel.Width) / 2;
        }

        private void VerticalPicture_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || VerticalPicture.ImageLocation == null)
                return;

            PictureForm form = new PictureForm();
            form.ImageLocation = VerticalPicture.ImageLocation;
            form.Title = VerticalPictureTitleLabel.Text;
            form.ShowDialog();
        }

        private void HorizontalPicture_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || HorizontalPicture.ImageLocation == null)
                return;

            PictureForm form = new PictureForm();
            form.ImageLocation = HorizontalPicture.ImageLocation;
            form.Title = HorizontalPictureTitleLabel.Text;
            form.ShowDialog();
        }

        private void BandList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowGraph();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            WatsEmiReportTool.Utility.ClearTempFiles();

            VerticalPicture.ImageLocation = null;
            HorizontalPicture.ImageLocation = null;
            SpanEditor.Text = SpanEditor.Text.Trim();
            if (SpanEditor.Text.Length == 0)
            {
                MessageBox.Show("Please input span value !", "Warning");
                SpanEditor.Focus();
                return;
            }
            if (!Regex.IsMatch(SpanEditor.Text, @"^[1-9]\d*(\.\d+)?$"))
            {
                MessageBox.Show(SpanEditor.Text + " is not a valid span value !", "Warning");
                SpanEditor.SelectAll();
                SpanEditor.Focus();
                return;
            }

            if (FrequencePreferredRadioButton.Checked)
            {
                if (!Regex.IsMatch(StartFreqEditor.Text, @"^[1-9]\d*(\.\d+)?$"))
                {
                    MessageBox.Show(StartFreqEditor.Text + " is not a valid start frequency value !", "Warning");
                    StartFreqEditor.SelectAll();
                    StartFreqEditor.Focus();
                    return;
                }

                if (!Regex.IsMatch(EndFreqEditor.Text, @"^[1-9]\d*(\.\d+)?$"))
                {
                    MessageBox.Show(EndFreqEditor.Text + " is not a valid start frequency value !", "Warning");
                    EndFreqEditor.SelectAll();
                    EndFreqEditor.Focus();
                    return;
                }

                if (double.Parse(EndFreqEditor.Text.Trim()) <= double.Parse(StartFreqEditor.Text.Trim()))
                {
                    MessageBox.Show("EndFrequency should bigger than StartFrequency !");
                    StartFreqEditor.SelectAll();
                    StartFreqEditor.Focus();
                    return;
                }
            }

            Program.Config.WriteInt("General", "ChannelPreferred", ChannelPreferredRadioButton.Checked ? 1 : 0);
            Program.Config.WriteInt("General", "DisplayChannel", DisplayChannelCheckBox.Checked ? 1 : 0);
            Program.Config.WriteString("General", "Span", SpanEditor.Text.Trim());

            mExportSpan = SpanEditor.Text;
            mStartFreq = StartFreqEditor.Text;
            mEndFreq = EndFreqEditor.Text;

            GenerateGraphs();

            mReport = new Report();
            mReport.Analyzer = HTTPAgent.Username;
            mReport.IsPairReport = false;
            mReport.TaskID = mTask.ID;
            mReport.ChannelSettingID = mTask.ChannelSettingID;
            mReport.LimitSetting = mLimitSetting;
            mReport.Span = double.Parse(mExportSpan);
            mReport.StartFreq = double.Parse(StartFreqEditor.Text);
            mReport.EndFreq = double.Parse(EndFreqEditor.Text);
            mReport.IsChannelPreferred = ChannelPreferredRadioButton.Checked;
            mReport.IsDisplayChannel = DisplayChannelCheckBox.Checked;
            mReport.EmiFileID = mEmiFileData.UID;
        }

        private void ViewExcelButton_Click(object sender, EventArgs e)
        {
            if (WatsEmiReportTool.Utility.GetExcelVersion() < 0)
            {
                MessageBox.Show("Excel was not installed !");
                return;
            }

            ExportSettingForm exportSettingForm = new ExportSettingForm();
            if (exportSettingForm.ShowDialog() == DialogResult.Cancel)
                return;

            string reportTemplateFile;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Select report file name";

            Excel.XlFileFormat excelFormat;
            if (exportSettingForm.ExportOffice2003)
            {
                reportTemplateFile = System.AppDomain.CurrentDomain.BaseDirectory
                 + "ReportTemplate.xls";
                //excelFormat = Excel.XlFileFormat.xlExcel8;
                //excelFormat = Excel.XlFileFormat.xlWorkbookNormal;
                saveFileDialog.Filter = "report file(*.xls)|*.xls";
            }
            else
            {
                reportTemplateFile = System.AppDomain.CurrentDomain.BaseDirectory
                 + "ReportTemplate.xlsx";
                //excelFormat = Excel.XlFileFormat.xlExcel12;
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

            //Hide();
            mExportStatusForm.Show();
            mCancelExport = false;
            //ExportButton.Enabled = false;

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
                    summarySheet.Cells[1, 6] = mEmiFileData.Site_ID;

                    UpdateStatus("Export Cover sheet ...");
                    /* Cover Sheet */
                    sheet = (Excel.Worksheet)sheets["Cover"];
                    if (!string.IsNullOrEmpty(mEmiFileData.PI_ID))
                        sheet.Cells[15, 5] = mEmiFileData.PI_ID;
                    if (!string.IsNullOrEmpty(mEmiFileData.PA_UserName))
                        sheet.Cells[17, 5] = mEmiFileData.PA_UserName;

                    Dictionary<double, double> startFrequencys = new Dictionary<double, double>();
                    Dictionary<double, double> endFrequencys = new Dictionary<double, double>();
                    WatsEmiDataManager dataManager = new WatsEmiDataManager();
                    Dictionary<ChannelSetting, WatsEmiData> datas;
                    Dictionary<int, List<WatsEmiSample>> samples = null;
                    ChannelSetting curChannelSetting;
                    WatsEmiData curData;
                    foreach (DG_Type dataGroup in mEmiFileData.DataGroups)
                    {
                        if (mCancelExport)
                            return;

                        if (!startFrequencys.ContainsKey(dataGroup.DG_FB_Angle))
                            startFrequencys[dataGroup.DG_FB_Angle] = dataGroup.DG_FB_Start;
                        else if (dataGroup.DG_FB_Start < startFrequencys[dataGroup.DG_FB_Angle])
                            startFrequencys[dataGroup.DG_FB_Angle] = dataGroup.DG_FB_Start;

                        if (!endFrequencys.ContainsKey(dataGroup.DG_FB_Angle))
                            endFrequencys[dataGroup.DG_FB_Angle] = dataGroup.DG_FB_End;
                        else if (dataGroup.DG_FB_End > endFrequencys[dataGroup.DG_FB_Angle])
                            endFrequencys[dataGroup.DG_FB_Angle] = dataGroup.DG_FB_End;

                        if (!dataManager.AllChannelSamples.TryGetValue(dataGroup.DG_FB_Angle, out datas))
                        {
                            datas = new Dictionary<ChannelSetting, WatsEmiData>();
                            dataManager.AllChannelSamples.Add(dataGroup.DG_FB_Angle, datas);
                        }

                        if (!dataManager.AllSamples.TryGetValue(dataGroup.DG_FB_Angle, out samples))
                        {
                            samples = new Dictionary<int, List<WatsEmiSample>>();
                            samples[0] = new List<WatsEmiSample>();
                            samples[1] = new List<WatsEmiSample>();
                            dataManager.AllSamples.Add(dataGroup.DG_FB_Angle, samples);
                        }

                        foreach (DG_Data_Type data in dataGroup.DGDatas)
                        {
                            if (mCancelExport)
                                return;

                            if (dataGroup.DB_FB_AntennaPolarization == 0)
                                samples[0].Add(new WatsEmiSample(data.DG_DI_Freq, data.DG_DI_RSSI));
                            else //if (dataGroup.DB_FB_AntennaPolarization == 1)
                                samples[1].Add(new WatsEmiSample(data.DG_DI_Freq, data.DG_DI_RSSI));

                            curChannelSetting = null;
                            foreach (ChannelSetting channelSetting in mChannelSettings)
                            {
                                if (mCancelExport)
                                    return;

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

                            if (!datas.TryGetValue(curChannelSetting, out curData))
                            {
                                curData = new WatsEmiData();
                                datas.Add(curChannelSetting, curData);
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

                    for (int i = 1; i < dataManager.AllChannelSamples.Count; i++)
                    {
                        if (mCancelExport)
                            return;

                        ((Excel.Range)summarySheet.Rows[3, objOpt]).Copy(objOpt);
                        ((Excel.Range)summarySheet.Cells[3 + i, 1]).EntireRow.Insert(objOpt, objOpt);
                    }

                    UpdateStatus("Export Device Info sheet ...");

                    /* Device Info Sheet */
                    sheet = (Excel.Worksheet)sheets["Device info"];
                    sheet.Cells[18, 12] = mEmiFileData.SA_RBW + "kHz";
                    sheet.Cells[19, 12] = mEmiFileData.SA_VBW + "kHz";
                    sheet.Cells[20, 12] = mEmiFileData.SA_Detector;
                    sheet.Cells[21, 12] = mEmiFileData.SA_Trace;
                    sheet.Cells[22, 12] = mEmiFileData.SA_Attenuation_Value + "dB";
                    sheet.Cells[23, 12] = mEmiFileData.SA_REF_LEVEL + "dBm";

                    int channelIndex;
                    Excel.Range range;
                    int summaryAngleStartRowIndex = 3;

                    List<Excel.Range> boldBorderRanges = new List<Excel.Range>();
                    boldBorderRanges.Add(summarySheet.get_Range(summarySheet.Cells[1, 1],
                        summarySheet.Cells[2, 14]));
                    foreach (KeyValuePair<double, Dictionary<ChannelSetting, WatsEmiData>> pair in dataManager.AllChannelSamples)
                    {
                        UpdateStatus("Export sheet " + pair.Key.ToString() + "\x00B0" + " ...");

                        if (mCancelExport)
                            return;

                        ((Excel.Worksheet)sheets["template"]).Copy(objOpt, workBook.ActiveSheet);
                        sheet = (Excel.Worksheet)workBook.ActiveSheet;
                        sheet.Name = pair.Key.ToString() + "\x00B0";
                        sheet.Cells[2, 3] = mEmiFileData.Site_ID;
                        sheet.Cells[2, 11] = mEmiFileData.Site_ID;
                        sheet.Cells[3, 3] = mEmiFileData.Site_Address;
                        sheet.Cells[4, 3] = WatsEmiReportTool.Utility.ConvertLatitude(mEmiFileData.Site_Latitude);
                        sheet.Cells[4, 11] = WatsEmiReportTool.Utility.ConvertLongtitude(mEmiFileData.Site_Longitude);
                        if (mLimitSetting.UseChannelPowerLimit)
                            sheet.Cells[5, 11] = mLimitSetting.ChannelPowerLimit.ToString();
                        else
                            sheet.Cells[5, 11] = "";

                        if (mLimitSetting.UseDeltaPowerLimit)
                            sheet.Cells[5, 15] = mLimitSetting.DeltaPowerLimit.ToString();
                        else
                            sheet.Cells[5, 15] = "";

                        sheet.Cells[6, 3] = WatsEmiReportTool.Utility.ConvertToDate(mEmiFileData.PA_TestTime);
                        sheet.Cells[6, 11] = mEmiFileData.PA_UserName;

                        summarySheet.Cells[summaryAngleStartRowIndex, 1] = pair.Key.ToString() + "\x00B0";

                        channelIndex = 0;
                        foreach (KeyValuePair<ChannelSetting, WatsEmiData> dataPair in pair.Value)
                        {
                            if (mCancelExport)
                                return;

                            if (channelIndex > 0)
                            {
                                ((Excel.Range)sheet.Rows[11, objOpt]).Copy(objOpt);
                                ((Excel.Range)sheet.Cells[11 + channelIndex, 1]).EntireRow.Insert(objOpt, objOpt);

                                ((Excel.Range)summarySheet.Rows[summaryAngleStartRowIndex, objOpt]).Copy(objOpt);
                                ((Excel.Range)summarySheet.Cells[summaryAngleStartRowIndex + channelIndex, 1]).EntireRow.Insert(objOpt, objOpt);
                            }

                            sheet.Cells[11 + channelIndex, 3] = dataPair.Key.ChannelName;
                            sheet.Cells[11 + channelIndex, 4] = dataPair.Key.CenterFreq;
                            sheet.Cells[11 + channelIndex, 5] = dataPair.Key.BandWidth;

                            //channel pair
                            sheet.Cells[11 + channelIndex, 10] = dataPair.Key.Pair.ChannelName;
                            sheet.Cells[11 + channelIndex, 11] = dataPair.Key.Pair.CenterFreq;
                            sheet.Cells[11 + channelIndex, 12] = dataPair.Key.Pair.BandWidth;

                            summarySheet.Cells[summaryAngleStartRowIndex + channelIndex, 5] = dataPair.Key.ChannelName;
                            summarySheet.Cells[summaryAngleStartRowIndex + channelIndex, 6] = dataPair.Key.CenterFreq;
                            summarySheet.Cells[summaryAngleStartRowIndex + channelIndex, 9] = dataPair.Key.Pair.ChannelName;
                            summarySheet.Cells[summaryAngleStartRowIndex + channelIndex, 10] = dataPair.Key.Pair.CenterFreq;

                            WatsEmiReportTool.ChannelPower channelPower = new WatsEmiReportTool.ChannelPower(mEmiFileData.SA_RBW, dataPair.Key, mLimitSetting, dataPair.Value);
                            sheet.Cells[11 + channelIndex, 8] = channelPower.HPower;
                            sheet.Cells[11 + channelIndex, 15] = channelPower.HPairPower;
                            sheet.Cells[11 + channelIndex, 6] = channelPower.VPower;
                            sheet.Cells[11 + channelIndex, 13] = channelPower.VPairPower;

                            subBands[0] = "";
                            subBands[1] = "";
                            if (dataPair.Key.ODUSubBand.Length == 1)
                                subBands[0] = dataPair.Key.ODUSubBand;
                            else if (dataPair.Key.ODUSubBand.Length == 3)
                            {
                                subBands[0] = dataPair.Key.ODUSubBand.Substring(0, 1);
                                subBands[1] = dataPair.Key.ODUSubBand.Substring(2);
                            }
                            summarySheet.Cells[summaryAngleStartRowIndex + channelIndex, 3] = subBands[0];
                            summarySheet.Cells[summaryAngleStartRowIndex + channelIndex, 4] = subBands[1];

                            if (!channelPower.IsValidVPower || !channelPower.IsValidVPairPower)
                                summarySheet.Cells[summaryAngleStartRowIndex + channelIndex, 13] = "X";
                            else
                                summarySheet.Cells[summaryAngleStartRowIndex + channelIndex, 13] = "";

                            if (!channelPower.IsValidHPower || !channelPower.IsValidHPairPower)
                                summarySheet.Cells[summaryAngleStartRowIndex + channelIndex, 14] = "X";
                            else
                                summarySheet.Cells[summaryAngleStartRowIndex + channelIndex, 14] = "";


                            if (!channelPower.IsValidVPower)
                            {
                                summarySheet.Cells[summaryAngleStartRowIndex + channelIndex, 7] = "X";
                                sheet.Cells[11 + channelIndex, 7] = "X";
                            }
                            else
                            {
                                summarySheet.Cells[summaryAngleStartRowIndex + channelIndex, 7] = "";
                                sheet.Cells[11 + channelIndex, 7] = "";
                            }

                            if (!channelPower.IsValidHPower)
                            {
                                summarySheet.Cells[summaryAngleStartRowIndex + channelIndex, 8] = "X";
                                sheet.Cells[11 + channelIndex, 9] = "X";
                            }
                            else
                            {
                                summarySheet.Cells[summaryAngleStartRowIndex + channelIndex, 8] = "";
                                sheet.Cells[11 + channelIndex, 9] = "";
                            }

                            if (!channelPower.IsValidVPairPower)
                            {
                                summarySheet.Cells[summaryAngleStartRowIndex + channelIndex, 11] = "X";
                                sheet.Cells[11 + channelIndex, 14] = "X";
                            }
                            else
                            {
                                summarySheet.Cells[summaryAngleStartRowIndex + channelIndex, 11] = "";
                                sheet.Cells[11 + channelIndex, 14] = "";
                            }

                            if (!channelPower.IsValidHPairPower)
                            {
                                summarySheet.Cells[summaryAngleStartRowIndex + channelIndex, 12] = "X";
                                sheet.Cells[11 + channelIndex, 16] = "X";
                            }
                            else
                            {
                                summarySheet.Cells[summaryAngleStartRowIndex + channelIndex, 12] = "";
                                sheet.Cells[11 + channelIndex, 16] = "";
                            }

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

                            range = summarySheet.get_Range(summarySheet.Cells[summaryAngleStartRowIndex, 1],
                                summarySheet.Cells[summaryAngleStartRowIndex + channelIndex - 1, 1]);
                            range.Merge(objOpt);
                        }

                        boldBorderRanges.Add(summarySheet.get_Range(summarySheet.Cells[summaryAngleStartRowIndex, 1],
                            summarySheet.Cells[summaryAngleStartRowIndex + channelIndex - 1, 14]));

                        summaryAngleStartRowIndex += channelIndex;

                        sheet.Cells[11, 1] = startFrequencys[pair.Key].ToString()
                            + "-" + endFrequencys[pair.Key].ToString();
                        sheet.Cells[11, 2] = pair.Key.ToString() + "\x00B0";

                        List<BitMapInfo> bitmapInfos = mBitmaps[pair.Key];
                        int pictureRows = bitmapInfos.Count;
                        if (pictureRows > 1)
                        {
                            for (int i = 0; i < pictureRows - 1; i++)
                            {
                                ((Excel.Range)sheet.Rows[12 + channelIndex, objOpt]).Copy(objOpt);
                                ((Excel.Range)sheet.Cells[13 + channelIndex + i, 1]).EntireRow.Insert(objOpt, objOpt);
                            }
                        }

                        for (int i = 0; i < pictureRows; i++)
                        {
                            UpdateStatus("Export sheet " + pair.Key.ToString() + "\x00B0 vertical picture "
                                + (i + 1).ToString() + " ...");
                            if (mCancelExport)
                                return;

                            sheet.Cells[12 + channelIndex + i, 1] = bitmapInfos[i].Title1;
                            range = (Excel.Range)sheet.Cells[12 + channelIndex + i, 1];

                            range.Select();

                            float left = Convert.ToSingle(range.Left) + 15;
                            float top = Convert.ToSingle(range.Top) + 15;
                            float width = 310;
                            float height = 150;

                            sheet.Shapes.AddPicture(bitmapInfos[i].BmpFile1, Microsoft.Office.Core.MsoTriState.msoFalse,
                                Microsoft.Office.Core.MsoTriState.msoTrue, left, top, width, height);


                            UpdateStatus("Export sheet " + pair.Key.ToString() + "\x00B0 horizontal picture "
                                + (i + 1).ToString() + " ...");

                            if (mCancelExport)
                                return;

                            sheet.Cells[12 + channelIndex + i, 9] = bitmapInfos[i].Title2;
                            range = (Excel.Range)sheet.Cells[12 + channelIndex + i, 9];

                            range.Select();

                            left = Convert.ToSingle(range.Left) + 15;
                            top = Convert.ToSingle(range.Top) + 15;
                            width = 310;
                            height = 150;

                            sheet.Shapes.AddPicture(bitmapInfos[i].BmpFile2, Microsoft.Office.Core.MsoTriState.msoFalse,
                                Microsoft.Office.Core.MsoTriState.msoTrue, left, top, width, height);
                        }
                    }

                    foreach (Excel.Range boldBorderRange in boldBorderRanges)
                    {
                        boldBorderRange.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).Weight = Excel.XlBorderWeight.xlMedium;
                        boldBorderRange.Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).Weight = Excel.XlBorderWeight.xlMedium;
                        boldBorderRange.Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft).Weight = Excel.XlBorderWeight.xlMedium;
                        boldBorderRange.Borders.get_Item(Excel.XlBordersIndex.xlEdgeRight).Weight = Excel.XlBorderWeight.xlMedium;
                    }

                    UpdateStatus("Delete template sheet ...");
                    ((Excel.Worksheet)sheets["template"]).Delete();

                    isReportSucceed = true;
                    UpdateStatus("Save export file, please wait ...");
                    workBook.Save();

                    /*
                    workBook.SaveAs(mExportFileName, Excel.XlFileFormat.xlWorkbookNormal, 
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, 
                        Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, 
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    */

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

                    WatsEmiReportTool.Utility.ReleaseCom(sheet);
                    WatsEmiReportTool.Utility.ReleaseCom(sheets);
                    WatsEmiReportTool.Utility.ReleaseCom(workBook);
                    WatsEmiReportTool.Utility.ReleaseCom(app);

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

        private void CommitButton_Click(object sender, EventArgs e)
        {
            if (mReport == null)
            {
                MessageBox.Show("Not ready for commit, report is not generated !");
                return;
            }

            HTTPAgent.instance().onUploadReportSuccessfully += new HTTPAgent.uploadReportSuccessfully(AnalysisViewForm_onUploadReportSuccessfully);
            HTTPAgent.instance().onUploadReportFailed += new HTTPAgent.uploadReportFailed(AnalysisViewForm_onUploadReportFailed);

            HTTPAgent.instance().uploadTaskReport(this, mTask.ID, mReport, null);
        }

        void AnalysisViewForm_onUploadReportFailed(long taskID, System.Net.HttpStatusCode statusCode)
        {
            HTTPAgent.instance().onUploadReportSuccessfully -= new HTTPAgent.uploadReportSuccessfully(AnalysisViewForm_onUploadReportSuccessfully);
            HTTPAgent.instance().onUploadReportFailed -= new HTTPAgent.uploadReportFailed(AnalysisViewForm_onUploadReportFailed);

            MessageBox.Show("Upload report for task '" + mTask.Name + "' failed, status = " + statusCode.ToString() + " !");
        }

        void AnalysisViewForm_onUploadReportSuccessfully(long taskID, Report report, object context)
        {
            HTTPAgent.instance().onUploadReportSuccessfully -= new HTTPAgent.uploadReportSuccessfully(AnalysisViewForm_onUploadReportSuccessfully);
            HTTPAgent.instance().onUploadReportFailed -= new HTTPAgent.uploadReportFailed(AnalysisViewForm_onUploadReportFailed);

            MessageBox.Show("Upload report for task '" + mTask.Name + "' succeed !");
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            EMIFileComboBox.Items.Clear();
            List<EMIFileData> emiFileDatas = Utility.GetTaskAvailableEmiFiles(mTask);
            foreach (EMIFileData emiFileData in emiFileDatas)
                EMIFileComboBox.Items.Add(emiFileData);
        }
    }
}
