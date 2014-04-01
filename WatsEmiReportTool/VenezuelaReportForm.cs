using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEMIAnalyzer.EMI;
using System.Threading;
using Utils;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Globalization;
using System.Diagnostics;

namespace WatsEmiReportTool
{
    public partial class VenezuelaReportForm : Form
    {
        private IniFile mIniFile = new IniFile(".\\WatsEmiReportTool.ini");
        private LimitSetting mLimitSetting;
        private EMIFileData mEmiA;
        private EMIFileData mEmiB;
        private double mAzimuthA;
        private double mAzimuthB;
        private List<ChannelSetting> mChannelSettings;

        private WatsEmiDataManager mEmiDataMgrA;
        private WatsEmiDataManager mEmiDataMgrB;

        List<BitMapInfo> mBmpInfosA, mBmpInfosB;
        
        private string mExportFileName;
        private Thread mExportThread;
        private bool mCancelExport = false;
        private ExportStatusForm mExportStatusForm;
        private delegate void UpdateStatusDelegate(string status);

        private string mVerticalCircleTitleA, mHorizontalCircleTitleA;
        private string mVerticalCircleBmpFileA, mHorizontalCircleBmpFileA;
        private string mVerticalCircleTitleB, mHorizontalCircleTitleB;
        private string mVerticalCircleBmpFileB, mHorizontalCircleBmpFileB;

        List<string> mValidChannelsInfo = new List<string>();

        public VenezuelaReportForm(LimitSetting limitSetting, double azimuthA, double azimuthB, EMIFileData emiA, EMIFileData emiB, List<ChannelSetting> channelSettings)
        {
            InitializeComponent();

            mLimitSetting = limitSetting;
            mAzimuthA = azimuthA;
            mAzimuthB = azimuthB;
            mEmiA = emiA;
            mEmiB = emiB;
            mChannelSettings = channelSettings;

            //int graphCount = mIniFile.ReadInt("General", "VenezuelaReportGraphCount", 1);
            mExportStatusForm = new ExportStatusForm(this);
        }

        private void VenezuelaReportForm_Load(object sender, EventArgs e)
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

            if (mLimitSetting.UseChannelPowerLimit)
                PChannelLimitALabel.Text = PChannelLimitBLabel.Text = mLimitSetting.ChannelPowerLimit.ToString();
            else
                PChannelLimitALabel.Text = PChannelLimitBLabel.Text = "";

            if (mLimitSetting.UseDeltaPowerLimit)
                PLevelLimitALabel.Text = PLevelLimitBLabel.Text = mLimitSetting.DeltaPowerLimit.ToString();
            else
                PLevelLimitALabel.Text = PLevelLimitBLabel.Text = "";

            mEmiDataMgrA = Utility.GetEmiDataManager(mEmiA, mChannelSettings);
            mEmiDataMgrB = Utility.GetEmiDataManager(mEmiB, mChannelSettings);

            int graphCount = mIniFile.ReadInt("General", "VenezuelaReportGraphCount", 1);
            GraphCountComboBox.SelectedIndex = graphCount - 1;

            GenerateGraphs();
            ShowGraph(true);
            ShowGraph(false);
        }

        private void ShowGraph(bool graphA)
        {
            if (graphA)
            {
                if (FrequencyBandComboxA.SelectedIndex == -1)
                {
                    VerticalCircleALabel.Text = "";
                    VerticalCircleAPictureBox.Image = (System.Drawing.Image)Properties.Resources.blank;

                    HorizontalCircleALabel.Text = "";
                    HorizontalCircleAPictureBox.Image = (System.Drawing.Image)Properties.Resources.blank;

                    VerticalALabel.Text = "";
                    VerticalAPictureBox.Image = (System.Drawing.Image)Properties.Resources.blank;

                    HorizontalALabel.Text = "";
                    HorizontalAPictureBox.Image = (System.Drawing.Image)Properties.Resources.blank;
                }
                else
                {
                    VerticalCircleALabel.Text = mVerticalCircleTitleA;
                    VerticalCircleAPictureBox.ImageLocation = mVerticalCircleBmpFileA;

                    HorizontalCircleALabel.Text = mHorizontalCircleTitleA;
                    HorizontalCircleAPictureBox.ImageLocation = mHorizontalCircleBmpFileA;

                    VerticalALabel.Text = mBmpInfosA[FrequencyBandComboxA.SelectedIndex].Title1;
                    VerticalAPictureBox.ImageLocation = mBmpInfosA[FrequencyBandComboxA.SelectedIndex].BmpFile1;

                    HorizontalALabel.Text = mBmpInfosA[FrequencyBandComboxA.SelectedIndex].Title2;
                    HorizontalAPictureBox.ImageLocation = mBmpInfosA[FrequencyBandComboxA.SelectedIndex].BmpFile2;
                }
            }
            else
            {
                if (FrequencyBandComboxB.SelectedIndex == -1)
                {
                    VerticalCircleBLabel.Text = "";
                    VerticalCircleBPictureBox.Image = (System.Drawing.Image)Properties.Resources.blank;

                    HorizontalCircleBLabel.Text = "";
                    HorizontalCircleBPictureBox.Image = (System.Drawing.Image)Properties.Resources.blank;

                    VerticalBLabel.Text = "";
                    VerticalBPictureBox.Image = (System.Drawing.Image)Properties.Resources.blank;

                    HorizontalBLabel.Text = "";
                    HorizontalBPictureBox.Image = (System.Drawing.Image)Properties.Resources.blank;
                }
                else
                {
                    VerticalCircleBLabel.Text = mVerticalCircleTitleB;
                    VerticalCircleBPictureBox.ImageLocation = mVerticalCircleBmpFileB;

                    HorizontalCircleBLabel.Text = mHorizontalCircleTitleB;
                    HorizontalCircleBPictureBox.ImageLocation = mHorizontalCircleBmpFileB;

                    VerticalBLabel.Text = mBmpInfosB[FrequencyBandComboxB.SelectedIndex].Title1;
                    VerticalBPictureBox.ImageLocation = mBmpInfosB[FrequencyBandComboxB.SelectedIndex].BmpFile1;

                    HorizontalBLabel.Text = mBmpInfosB[FrequencyBandComboxB.SelectedIndex].Title2;
                    HorizontalBPictureBox.ImageLocation = mBmpInfosB[FrequencyBandComboxB.SelectedIndex].BmpFile2;
                }
            }
        }

        private void GenerateGraphs()
        {
            int minAbsRssi = Int32.MaxValue, maxAbsRssi = Int32.MinValue;
            Dictionary<int, List<WatsEmiSample>> samples;
            foreach (KeyValuePair<double, Dictionary<int, List<WatsEmiSample>>> pair in mEmiDataMgrA.AllSamples)
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

            foreach (KeyValuePair<double, Dictionary<int, List<WatsEmiSample>>> pair in mEmiDataMgrB.AllSamples)
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

            int graphCount = GraphCountComboBox.SelectedIndex + 1;
            double span = (mChannelSettings[mChannelSettings.Count - 1].Pair.EndFreq - mChannelSettings[0].StartFreq) / graphCount * 1.0f;
            double channelStartFreq = mChannelSettings[0].StartFreq;
            double channelEndFreq = mChannelSettings[mChannelSettings.Count - 1].Pair.EndFreq;
            List<FrequencyRange> ranges = new List<FrequencyRange>();
            FrequencyRange range;
            double startFreq = channelStartFreq;
            do
            {
                range = new FrequencyRange();
                range.FromFreq = startFreq;
                range.EndFreq = startFreq + span;
                if (Math.Ceiling(range.EndFreq) >= channelEndFreq)
                {
                    range.EndFreq = channelEndFreq;
                    ranges.Add(range);
                    break;
                }
                ranges.Add(range);
                startFreq = startFreq + span;

            } while (true);

            mBmpInfosA = VenezuelaReportPictureCreator.create(mEmiA, mAzimuthA,
                mEmiA.Site_ID + " to " + mEmiB.Site_ID + " - " + mAzimuthA + "\x00B0",
                mEmiDataMgrA, mChannelSettings, mLimitSetting, minAbsRssi, maxAbsRssi, span, ranges,
                ref mVerticalCircleTitleA, ref mHorizontalCircleTitleA,
                ref mVerticalCircleBmpFileA, ref mHorizontalCircleBmpFileA);
            mBmpInfosB = VenezuelaReportPictureCreator.create(mEmiB, mAzimuthB,
                mEmiB.Site_ID + " to " + mEmiA.Site_ID + " - " + mAzimuthB + "\x00B0",
                mEmiDataMgrB, mChannelSettings, mLimitSetting, minAbsRssi, maxAbsRssi, span, ranges,
                ref mVerticalCircleTitleB, ref mHorizontalCircleTitleB,
                ref mVerticalCircleBmpFileB, ref mHorizontalCircleBmpFileB);

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

            mValidChannelsInfo.Clear();

            string reportTemplateFile;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Select report file name";

            Excel.XlFileFormat excelFormat;
            if (exportSettingForm.ExportOffice2003)
            {
                reportTemplateFile = System.AppDomain.CurrentDomain.BaseDirectory
                 + "VenezuelaTemplate.xls";
                //excelFormat = Excel.XlFileFormat.xlExcel8;
                //excelFormat = Excel.XlFileFormat.xlWorkbookNormal;
                saveFileDialog.Filter = "report file(*.xls)|*.xls";
            }
            else
            {
                reportTemplateFile = System.AppDomain.CurrentDomain.BaseDirectory
                 + "VenezuelaTemplate.xlsx";
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

            Hide();
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

                    summarySheet = (Excel.Worksheet)sheets["Resumen"];
                    summarySheet.Cells[1, 4] = mEmiA.Site_ID;
                    summarySheet.Cells[1, 12] = mEmiB.Site_ID;

                    UpdateStatus("Export Cover sheet ...");
                    /* Cover Sheet */
                    sheet = (Excel.Worksheet)sheets["Cubrir"];
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
                    sheet = (Excel.Worksheet)sheets["Información del dispositivo"];
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
                        sheet = (Excel.Worksheet)sheets["EMI" + (i + 1).ToString()];
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
                                ((Excel.Range)sheet.Rows[13 + channelIndex, objOpt]).Copy(objOpt);
                                ((Excel.Range)sheet.Cells[14 + channelIndex + j, 1]).EntireRow.Insert(objOpt, objOpt);
                            }
                        }

                        //drawing circle
                        UpdateStatus("Export sheet " + azimuths[i].ToString() + "\x00B0 vertical circle picture ...");
                        string verticalTitle = (i == 0 ? mVerticalCircleTitleA : mVerticalCircleTitleB);
                        string horizontalTitle = (i == 0 ? mHorizontalCircleTitleA : mHorizontalCircleTitleB);
                        string verticalFile = (i == 0 ? mVerticalCircleBmpFileA : mVerticalCircleBmpFileB);
                        string horizontalFile = (i == 0 ? mHorizontalCircleBmpFileA : mHorizontalCircleBmpFileB);

                        sheet.Cells[12 + channelIndex, 1] = verticalTitle;
                        sheet.Cells[12 + channelIndex, 9] = horizontalTitle;

                        range = (Excel.Range)sheet.Cells[12 + channelIndex, 1];                            
                        float left = Convert.ToSingle(range.Left) + 110;
                        float top = Convert.ToSingle(range.Top) + 13;
                        float width = 170;
                        float height = 150;
                        sheet.Shapes.AddPicture(verticalFile, Microsoft.Office.Core.MsoTriState.msoFalse,
                            Microsoft.Office.Core.MsoTriState.msoTrue, left, top, width, height);

                        range = (Excel.Range)sheet.Cells[12 + channelIndex, 9];
                        left = Convert.ToSingle(range.Left) + 110;
                        top = Convert.ToSingle(range.Top) + 13;
                        sheet.Shapes.AddPicture(horizontalFile, Microsoft.Office.Core.MsoTriState.msoFalse,
                            Microsoft.Office.Core.MsoTriState.msoTrue, left, top, width, height);
                        
                        for (int j = 0; j < pictureRows; j++)
                        {
                            UpdateStatus("Export sheet " + azimuths[i].ToString() + "\x00B0 vertical picture "
                                + (j + 1).ToString() + " ...");
                            if (mCancelExport)
                                return;

                            sheet.Cells[13 + channelIndex + j, 1] = bitmapInfos[i][j].Title1;
                            range = (Excel.Range)sheet.Cells[13 + channelIndex + j, 1];
                            //range.Select();

                            left = Convert.ToSingle(range.Left) + 15;
                            top = Convert.ToSingle(range.Top) + 15;
                            width = 370;
                            height = 180;

                            sheet.Shapes.AddPicture(bitmapInfos[i][j].BmpFile1, Microsoft.Office.Core.MsoTriState.msoFalse,
                                Microsoft.Office.Core.MsoTriState.msoTrue, left, top, width, height);


                            UpdateStatus("Export sheet " + azimuths[i] + "\x00B0 horizontal picture "
                                + (j + 1).ToString() + " ...");

                            if (mCancelExport)
                                return;

                            sheet.Cells[13 + channelIndex + j, 9] = bitmapInfos[i][j].Title2;
                            range = (Excel.Range)sheet.Cells[13 + channelIndex + j, 9];
                            //range.Select();

                            left = Convert.ToSingle(range.Left) + 15;
                            top = Convert.ToSingle(range.Top) + 15;

                            sheet.Shapes.AddPicture(bitmapInfos[i][j].BmpFile2, Microsoft.Office.Core.MsoTriState.msoFalse,
                                Microsoft.Office.Core.MsoTriState.msoTrue, left, top, width, height);
                        }
                    }

                    string validChannelsInfoText = "La base en el resultado anterior, se recomienda para:\r\n\r\n";
                    foreach (string vaildChannelInfo in mValidChannelsInfo)
                        validChannelsInfoText += vaildChannelInfo + ", ";
                    if (mValidChannelsInfo.Count > 0)
                        validChannelsInfoText += "\r\n\r\n";
                    validChannelsInfoText += "para ser utilizado para link ";
                    validChannelsInfoText += mEmiA.Site_ID + "-" + mEmiB.Site_ID;
                    summarySheet.Cells[4 + mChannelSettings.Count, 1] = validChannelsInfoText;

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

        private void UpdateSummarySheet(Excel.Worksheet summarySheet, ChannelSetting curChannel, bool isFirstEmi,
            bool isValidVPower, bool isValidHPower, bool isValidVPairPower, bool isValidHPairPower)
        {
            int row = -1;
            for (int i = 0; i < mChannelSettings.Count; i++)
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

            bool validPolV = false;
            bool validPolH = false;
            if (((Excel.Range)summarySheet.Cells[3 + row, 5]).Text.ToString().Length == 0
                && ((Excel.Range)summarySheet.Cells[3 + row, 9]).Text.ToString().Length == 0
                && ((Excel.Range)summarySheet.Cells[3 + row, 13]).Text.ToString().Length == 0
                && ((Excel.Range)summarySheet.Cells[3 + row, 17]).Text.ToString().Length == 0)
            {
                summarySheet.Cells[3 + row, 19] = "";
                validPolV = true;
            }

            if (((Excel.Range)summarySheet.Cells[3 + row, 6]).Text.ToString().Length == 0
                && ((Excel.Range)summarySheet.Cells[3 + row, 10]).Text.ToString().Length == 0
                && ((Excel.Range)summarySheet.Cells[3 + row, 14]).Text.ToString().Length == 0
                && ((Excel.Range)summarySheet.Cells[3 + row, 18]).Text.ToString().Length == 0)
            {
                summarySheet.Cells[3 + row, 20] = "";
                validPolH = true;
            }

            if (validPolV && validPolH)
                mValidChannelsInfo.Add(((Excel.Range)summarySheet.Cells[3 + row, 3]).Text.ToString() + " V & H");
            else if (validPolV)
                mValidChannelsInfo.Add(((Excel.Range)summarySheet.Cells[3 + row, 3]).Text.ToString() + " V");
            else if (validPolH)
                mValidChannelsInfo.Add(((Excel.Range)summarySheet.Cells[3 + row, 3]).Text.ToString() + " H");
        }

        private void VenezuelaReportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Utility.ClearTempFiles();
            MainForm.Instance.Show();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            Utility.ClearTempFiles();

            int graphCount = GraphCountComboBox.SelectedIndex + 1;
            mIniFile.WriteInt("General", "VenezuelaReportGraphCount", GraphCountComboBox.SelectedIndex + 1);

            GenerateGraphs();
            ShowGraph(true);
            ShowGraph(false);
        }

        private void VerticalCircleAPictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || VerticalCircleAPictureBox.ImageLocation == null)
                return;

            PictureForm form = new PictureForm();
            form.ImageLocation = VerticalCircleAPictureBox.ImageLocation;
            form.Title = VerticalCircleALabel.Text;
            form.Width = form.Height = 600;
            form.ShowDialog();
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

        private void HorizontalCircleAPictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || HorizontalCircleAPictureBox.ImageLocation == null)
                return;

            PictureForm form = new PictureForm();
            form.ImageLocation = HorizontalCircleAPictureBox.ImageLocation;
            form.Title = HorizontalCircleALabel.Text;
            form.Width = form.Height = 600;
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

        private void VerticalCircleBPictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || VerticalCircleBPictureBox.ImageLocation == null)
                return;

            PictureForm form = new PictureForm();
            form.ImageLocation = VerticalCircleBPictureBox.ImageLocation;
            form.Title = VerticalCircleBLabel.Text;
            form.Width = form.Height = 600;
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

        private void HorizontalCircleBPictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || HorizontalCircleBPictureBox.ImageLocation == null)
                return;

            PictureForm form = new PictureForm();
            form.ImageLocation = HorizontalCircleBPictureBox.ImageLocation;
            form.Title = HorizontalCircleBLabel.Text;
            form.Width = form.Height = 600;
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

        private void FrequencyBandComboxA_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowGraph(true);
        }

        private void FrequencyBandComboxB_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowGraph(false);
        }
    }
}
