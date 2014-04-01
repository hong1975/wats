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
using System.Threading;
using Word = Microsoft.Office.Interop.Word;
using System.Globalization;
using System.Diagnostics;

namespace WatsEmiReportTool
{
    public partial class MalaysiaReportForm : Form
    {
        private const float GRAPH_WIDTH = 240f;
        private const float GRAPH_HEIGHT = 260f;

        private double mOppositeAzimuthA;
        private double mOppositeAzimuthB;

        private bool mIsFiveAzimuthReport;
        private LimitSetting mLimitSetting;
        private EMIFileData mEmiA;
        private EMIFileData mEmiB;
        private EMIFileData mCurEmi;
        private List<ChannelSetting> mChannelSettings;
        private WatsEmiDataManager mEmiDataMgrA;
        private WatsEmiDataManager mEmiDataMgrB;
        private Dictionary<double, BitMapInfo> mBitmapsA = new Dictionary<double, BitMapInfo>();
        private Dictionary<double, BitMapInfo> mBitmapsB = new Dictionary<double, BitMapInfo>();

        private List<double> mAzimuthsA = new List<double>();
        private List<double> mAzimuthsB = new List<double>();

        private Dictionary<double, double> mActualAzimuthMapA = new Dictionary<double, double>();
        private Dictionary<double, double> mActualAzimuthMapB = new Dictionary<double, double>();

        private string mExportFileName;
        private Thread mExportThread;
        private bool mCancelExport = false;
        private ExportStatusForm mExportStatusForm;
        private delegate void UpdateStatusDelegate(string status);

        public double OppositeAzimuthA
        {
            set { mOppositeAzimuthA = value; }
        }

        public double OppositeAzimuthB
        {
            set { mOppositeAzimuthB = value; }
        }

        public MalaysiaReportForm(bool isFiveAzimuthReport, LimitSetting limitSetting, EMIFileData emiA, EMIFileData emiB, List<ChannelSetting> channelSettings)
        {
            InitializeComponent();

            mIsFiveAzimuthReport = isFiveAzimuthReport;

            mLimitSetting = limitSetting;
            mEmiA = emiA;
            mEmiB = emiB;
            mChannelSettings = channelSettings;

            EmiComboBox.Items.Add(mEmiA.Site_ID + " (User: " + mEmiA.PA_UserName + ", Time: " + mEmiA.PA_TestTime + ")");
            EmiComboBox.Items.Add(mEmiB.Site_ID + " (User: " + mEmiB.PA_UserName + ", Time: " + mEmiB.PA_TestTime + ")");

            mExportStatusForm = new ExportStatusForm(this);
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

        private void ShowSiteInformation()
        {
            SiteNameLabel.Text = mCurEmi.Site_ID;
            SiteIDLabel.Text = mCurEmi.Site_ID;
            AddressLabel.Text = mCurEmi.Site_Address;

            DateLabel.Text = mCurEmi.PA_TestTime;
            EngineerLabel.Text = mCurEmi.PA_UserName;
            LongtitudeLabel.Text = Utility.ConvertLongtitude(mCurEmi.Site_Longitude);
            LatitudeLabel.Text = Utility.ConvertLatitude(mCurEmi.Site_Longitude); ;

            if (mLimitSetting.UseChannelPowerLimit)
                PChannelLimitLabel.Text = mLimitSetting.ChannelPowerLimit.ToString();
            else
                PChannelLimitLabel.Text = "";

            if (mLimitSetting.UseDeltaPowerLimit)
                LevelLimitLabel.Text = mLimitSetting.DeltaPowerLimit.ToString();
            else
                LevelLimitLabel.Text = "";
        }

        private void RemovePage(Word._Document doc, int pageNo)
        {
            object oMissing = System.Reflection.Missing.Value; 
            int pages = doc.ComputeStatistics(Word.WdStatistic.wdStatisticPages, ref oMissing);
            object objWhat = Word.WdGoToItem.wdGoToPage;
            object objWhich = Word.WdGoToDirection.wdGoToAbsolute;
            object objPage = pageNo;//指定页  
            Word.Range range1 = doc.GoTo(ref objWhat, ref objWhich, ref objPage, ref oMissing);  
            Word.Range range2 = range1.GoToNext(Word.WdGoToItem.wdGoToPage);  
            object objStart = range1.Start;  
            object objEnd = range2.Start;  
            if (range1.Start == range2.Start)  
                objEnd = doc.Characters.Count;//最后一页  
            
            string str= doc.Range(ref objStart, ref objEnd).Text;  

            object unit = (int)Word.WdUnits.wdCharacter;  
            object count = 1;
            doc.Range(ref objStart, ref objEnd).Delete(ref unit, ref count);  
        }

        private void ExportSiteReport(int offset, Word._Document doc, bool isSiteA)
        {
            object oMissing = System.Reflection.Missing.Value;

            Dictionary<double, BitMapInfo> bitmaps = (isSiteA ? mBitmapsA : mBitmapsB);
            List<double> azimuths = (isSiteA ? mAzimuthsA : mAzimuthsB);
            int graphTableCount = (int)Math.Ceiling(azimuths.Count / 2.0);
            WatsEmiDataManager emiDataMgr = (isSiteA ? mEmiDataMgrA : mEmiDataMgrB);
            EMIFileData emi = (isSiteA ? mEmiA : mEmiB);
            Dictionary<double, double> actualAzimuthMap = (isSiteA ? mActualAzimuthMapA : mActualAzimuthMapB);

            object linkToFile = false;
            object saveWithDocument = true;
            object anchor; ;
            Word.InlineShape shape;

            for (int i = 0; i < graphTableCount; i++)
            {
                UpdateStatus("Export Site " + (isSiteA ? "A":"B") + " graph table " + (i + 1).ToString());
                doc.Tables[offset + 4 + i].Cell(1, 1).Select();
                anchor = doc.Application.Selection.Range;
                shape = doc.Application.ActiveDocument.InlineShapes.AddPicture(bitmaps[azimuths[2 * i]].BmpFile1,
                    ref linkToFile, ref saveWithDocument, ref anchor);
                shape.Width = GRAPH_WIDTH;
                shape.Height = GRAPH_HEIGHT;
                doc.Tables[offset + 4 + i].Cell(2, 1).Range.Text = azimuths[2 * i].ToString() + "\x00B0" + " V";

                doc.Tables[offset + 4 + i].Cell(1, 3).Select();
                anchor = doc.Application.Selection.Range;
                shape = doc.Application.ActiveDocument.InlineShapes.AddPicture(bitmaps[azimuths[2 * i]].BmpFile2,
                    ref linkToFile, ref saveWithDocument, ref anchor);
                shape.Width = GRAPH_WIDTH;
                shape.Height = GRAPH_HEIGHT;
                doc.Tables[offset + 4 + i].Cell(2, 3).Range.Text = azimuths[2 * i].ToString() + "\x00B0" + " H";


                if (2 * i + 1 == azimuths.Count)
                    break;

                doc.Tables[offset + 4 + i].Cell(4, 1).Select();
                anchor = doc.Application.Selection.Range;
                shape = doc.Application.ActiveDocument.InlineShapes.AddPicture(bitmaps[azimuths[2 * i + 1]].BmpFile1,
                    ref linkToFile, ref saveWithDocument, ref anchor);
                shape.Width = GRAPH_WIDTH;
                shape.Height = GRAPH_HEIGHT;
                doc.Tables[offset + 4 + i].Cell(5, 1).Range.Text = azimuths[2 * i + 1].ToString() + "\x00B0" + " V";

                doc.Tables[offset + 4 + i].Cell(4, 3).Select();
                anchor = doc.Application.Selection.Range;
                shape = doc.Application.ActiveDocument.InlineShapes.AddPicture(bitmaps[azimuths[2 * i + 1]].BmpFile2,
                    ref linkToFile, ref saveWithDocument, ref anchor);
                shape.Width = GRAPH_WIDTH;
                shape.Height = GRAPH_HEIGHT;
                doc.Tables[offset + 4 + i].Cell(5, 3).Range.Text = azimuths[2 * i + 1].ToString() + "\x00B0" + " H";
            }

            if (mIsFiveAzimuthReport)
            {
                UpdateStatus("Export Site " + (isSiteA ? "A" : "B") + " 5-azimuth detail");
                for (int i = 1; i <= 5; i++)
                    doc.Tables[offset + 3 + graphTableCount + 3].Delete();

                doc.Tables[offset + 3 + graphTableCount + 2].Cell(1, 1).Merge(doc.Tables[offset + 3 + graphTableCount + 2].Cell(2, 1));
                doc.Tables[offset + 3 + graphTableCount + 2].Cell(1, 2).Merge(doc.Tables[offset + 3 + graphTableCount + 2].Cell(2, 2));

                for (int i = 0; i < mChannelSettings.Count * 2 - 1; i++)
                    doc.Tables[offset + 3 + graphTableCount + 2].Rows.Add(ref oMissing);

                ChannelSetting channelSetting;
                ChannelPower channelPower;
                for (int i = 0; i < mChannelSettings.Count; i++)
                {
                    channelSetting = mChannelSettings[i];

                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i, 1).Range.Text = channelSetting.ChannelName;
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i, 2).Range.Text = ((float)(((float)channelSetting.CenterFreq) / 1000)).ToString();
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i + mChannelSettings.Count, 1).Range.Text = channelSetting.Pair.ChannelName;
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i + mChannelSettings.Count, 2).Range.Text = ((float)(((float)channelSetting.Pair.CenterFreq) / 1000)).ToString();

                    //1st Center
                    channelPower = new ChannelPower(emi.SA_RBW, channelSetting, mLimitSetting,
                        emiDataMgr.AllChannelSamples[actualAzimuthMap[azimuths[2]]][channelSetting]);
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(1, 3).Range.Text = "Center (0\x00B0)";
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i, 3).Range.Text = channelPower.IsValidVPower ? "" : "X";
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i, 4).Range.Text = channelPower.IsValidHPower ? "" : "X";
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i + mChannelSettings.Count, 3).Range.Text = channelPower.IsValidVPairPower ? "" : "X";
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i + mChannelSettings.Count, 4).Range.Text = channelPower.IsValidHPairPower ? "" : "X";

                    //2nd 
                    channelPower = new ChannelPower(emi.SA_RBW, channelSetting, mLimitSetting,
                        emiDataMgr.AllChannelSamples[actualAzimuthMap[azimuths[3]]][channelSetting]);
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(1, 4).Range.Text = azimuths[3] + "\x00B0";
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i, 5).Range.Text = channelPower.IsValidVPower ? "" : "X";
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i, 6).Range.Text = channelPower.IsValidHPower ? "" : "X";
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i + mChannelSettings.Count, 5).Range.Text = channelPower.IsValidVPairPower ? "" : "X";
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i + mChannelSettings.Count, 6).Range.Text = channelPower.IsValidHPairPower ? "" : "X";

                    //3rd 
                    channelPower = new ChannelPower(emi.SA_RBW, channelSetting, mLimitSetting,
                        emiDataMgr.AllChannelSamples[actualAzimuthMap[azimuths[1]]][channelSetting]);
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(1, 5).Range.Text = azimuths[1] + "\x00B0";
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i, 7).Range.Text = channelPower.IsValidVPower ? "" : "X";
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i, 8).Range.Text = channelPower.IsValidHPower ? "" : "X";
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i + mChannelSettings.Count, 7).Range.Text = channelPower.IsValidVPairPower ? "" : "X";
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i + mChannelSettings.Count, 8).Range.Text = channelPower.IsValidHPairPower ? "" : "X";

                    //4th 
                    channelPower = new ChannelPower(emi.SA_RBW, channelSetting, mLimitSetting,
                        emiDataMgr.AllChannelSamples[actualAzimuthMap[azimuths[4]]][channelSetting]);
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(1, 6).Range.Text = azimuths[4] + "\x00B0";
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i, 9).Range.Text = channelPower.IsValidVPower ? "" : "X";
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i, 10).Range.Text = channelPower.IsValidHPower ? "" : "X";
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i + mChannelSettings.Count, 9).Range.Text = channelPower.IsValidVPairPower ? "" : "X";
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i + mChannelSettings.Count, 10).Range.Text = channelPower.IsValidHPairPower ? "" : "X";

                    //5th 
                    channelPower = new ChannelPower(emi.SA_RBW, channelSetting, mLimitSetting,
                        emiDataMgr.AllChannelSamples[actualAzimuthMap[azimuths[0]]][channelSetting]);
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(1, 7).Range.Text = azimuths[0] + "\x00B0";
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i, 11).Range.Text = channelPower.IsValidVPower ? "" : "X";
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i, 12).Range.Text = channelPower.IsValidHPower ? "" : "X";
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i + mChannelSettings.Count, 11).Range.Text = channelPower.IsValidVPairPower ? "" : "X";
                    doc.Tables[offset + 3 + graphTableCount + 2].Cell(3 + i + mChannelSettings.Count, 12).Range.Text = channelPower.IsValidHPairPower ? "" : "X";
                }
            }
            else
            {
                UpdateStatus("Export Site " + (isSiteA ? "A" : "B") + " all-azimuth detail");
                doc.Tables[offset + 3 + graphTableCount + 2].Delete();
                int detailTableCount = (int)Math.Ceiling(azimuths.Count / 6.0);
                int removeDetailTableCount = 5 - detailTableCount;
                for (int i = 0; i < removeDetailTableCount; i++)
                    doc.Tables[offset + 3 + graphTableCount + 3].Delete();

                ChannelSetting channelSetting;
                ChannelPower channelPower;

                for (int i = 0; i < detailTableCount; i++)
                {
                    doc.Tables[offset + 3 + graphTableCount + 2 + i].Cell(1, 1).Merge(doc.Tables[offset + 3 + graphTableCount + 2 + i].Cell(2, 1));
                    doc.Tables[offset + 3 + graphTableCount + 2 + i].Cell(1, 2).Merge(doc.Tables[offset + 3 + graphTableCount + 2 + i].Cell(2, 2));
                    for (int k = 0; k < mChannelSettings.Count * 2 - 1; k++)
                        doc.Tables[offset + 3 + graphTableCount + 2 + i].Rows.Add(ref oMissing);

                    for (int j = 0; j < 6; j++)
                    {
                        if (i * 6 + j < azimuths.Count)
                        {
                            doc.Tables[offset + 3 + graphTableCount + 2 + i].Cell(1, 3 + j).Range.Text = azimuths[i * 6 + j].ToString() + "\x00B0";
                            for (int k = 0; k < mChannelSettings.Count; k++)
                            {
                                channelSetting = mChannelSettings[k];

                                channelPower = new ChannelPower(emi.SA_RBW, channelSetting, mLimitSetting,
                                    emiDataMgr.AllChannelSamples[azimuths[i * 6 + j]][channelSetting]);

                                doc.Tables[offset + 3 + graphTableCount + 2 + i].Cell(3 + k, 1).Range.Text = channelSetting.ChannelName;
                                doc.Tables[offset + 3 + graphTableCount + 2 + i].Cell(3 + k, 2).Range.Text = ((float)(((float)channelSetting.CenterFreq) / 1000)).ToString();
                                doc.Tables[offset + 3 + graphTableCount + 2 + i].Cell(3 + k, 3 + 2 * j).Range.Text = channelPower.IsValidVPower ? "" : "X";
                                doc.Tables[offset + 3 + graphTableCount + 2 + i].Cell(3 + k, 3 + 2 * j + 1).Range.Text = channelPower.IsValidHPower ? "" : "X";

                                doc.Tables[offset + 3 + graphTableCount + 2 + i].Cell(3 + k + mChannelSettings.Count, 1).Range.Text = channelSetting.Pair.ChannelName;
                                doc.Tables[offset + 3 + graphTableCount + 2 + i].Cell(3 + k + mChannelSettings.Count, 2).Range.Text = ((float)(((float)channelSetting.Pair.CenterFreq) / 1000)).ToString();
                                doc.Tables[offset + 3 + graphTableCount + 2 + i].Cell(3 + k + mChannelSettings.Count, 3 + 2 * j).Range.Text = channelPower.IsValidVPairPower ? "" : "X";
                                doc.Tables[offset + 3 + graphTableCount + 2 + i].Cell(3 + k + mChannelSettings.Count, 3 + 2 * j + 1).Range.Text = channelPower.IsValidHPairPower ? "" : "X";
                            }
                        }
                        else
                        {
                            doc.Tables[offset + 3 + graphTableCount + 2 + i].Cell(1, 3 + j).Range.Text = "";
                            doc.Tables[offset + 3 + graphTableCount + 2 + i].Cell(2, 3 + 2 * j).Range.Text = "";
                            doc.Tables[offset + 3 + graphTableCount + 2 + i].Cell(2, 3 + 2 * j + 1).Range.Text = "";
                        }
                    }
                }
            }

        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            if (Utility.GetWordVersion() < 0)
            {
                MessageBox.Show("Word was not installed !");
                return;
            }

            WordExportSettingForm exportSettingForm = new WordExportSettingForm();
            if (exportSettingForm.ShowDialog() == DialogResult.Cancel)
                return;

            string reportTemplateFile;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Select report file name";

            if (exportSettingForm.ExportOffice2003)
            {
                reportTemplateFile = System.AppDomain.CurrentDomain.BaseDirectory
                 + "MalaysiaTemplate.doc";
                saveFileDialog.Filter = "report file(*.doc)|*.doc";
            }
            else
            {
                reportTemplateFile = System.AppDomain.CurrentDomain.BaseDirectory
                 + "MalaysiaTemplate.docx";
                saveFileDialog.Filter = "report file(*.docx)|*.docx";
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

                System.Globalization.CultureInfo Oldci = null;
                object oMissing = System.Reflection.Missing.Value;
                Word._Application app = null;
                Word._Document doc;
                try
                {
                    Oldci = System.Threading.Thread.CurrentThread.CurrentCulture;
                    System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");

                    app = new Word.Application();
                    app.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;

                    UpdateStatus("Open word document ...");
                    object objFileName = mExportFileName;
                    doc = app.Documents.Open(ref objFileName,
                        ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                        ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                        ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                    UpdateStatus("Calculate graph table count ...");

                    int graphTableCountA = (int)Math.Ceiling(mAzimuthsA.Count / 2.0);
                    int graphTableCountB = (int)Math.Ceiling(mAzimuthsB.Count / 2.0);
                    int removePageCount = 10 - graphTableCountB;
                    for (int i = 0; i < removePageCount; i++)
                        RemovePage(doc, 15);
                    removePageCount = 10 - graphTableCountA;
                    for (int i = 0; i < removePageCount; i++)
                        RemovePage(doc, 4);

                    UpdateStatus("Export Site A ...");
                    ExportSiteReport(0, doc, true);

                    int detailTableCountA = (int)Math.Ceiling(mAzimuthsA.Count / 6.0);
                    int offset = graphTableCountA + 1 + detailTableCountA;

                    UpdateStatus("Export Site B ...");
                    ExportSiteReport(offset, doc, false);

                    isReportSucceed = true;
                    UpdateStatus("Save export file, please wait ...");
                    doc.Save();

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
                        app.Quit(ref oMissing, ref oMissing, ref oMissing);
                }

                if (isReportSucceed)
                {
                    try
                    {
                        UpdateStatus("Open word ...");

                        Process process = Process.Start("winword", "\"" + mExportFileName + "\"");
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

        private void EmiComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EmiComboBox.SelectedIndex == 0)
                mCurEmi = mEmiA;
            else
                mCurEmi = mEmiB;

            ShowSiteInformation();
            AzimuthComboBox.Items.Clear();

            List<double> azimuths;
            if (EmiComboBox.SelectedIndex == 0)
                azimuths = mAzimuthsA;
            else
                azimuths = mAzimuthsB;
            foreach (double azimuth in azimuths)
            {
                AzimuthComboBox.Items.Add(azimuth);
                if (AzimuthComboBox.Items.Count > 0)
                {
                    AzimuthComboBox.SelectedIndex = 0;
                }
            }

            ShowGraph();
        }

        private void MalaysiaReportForm_Load(object sender, EventArgs e)
        {
            mEmiDataMgrA = Utility.GetEmiDataManager(mEmiA, mChannelSettings);
            mEmiDataMgrB = Utility.GetEmiDataManager(mEmiB, mChannelSettings);

            GenerateGraphs();
            EmiComboBox.SelectedIndex = 0;

            ShowGraph();
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

            BitMapInfo bmpInfo;
            int i = 0;
            int index = 0;
            List<double> azimuths = new List<double>();
            if (mIsFiveAzimuthReport)
            {
                double actualAzimuth;
                if (mEmiDataMgrA.AllChannelSamples.Count >= 5)
                {
                    double azimuth;
                    foreach (KeyValuePair<double, Dictionary<ChannelSetting, WatsEmiData>> pair in mEmiDataMgrA.AllChannelSamples)
                    {
                        azimuths.Add(pair.Key);
                        if (mOppositeAzimuthA == pair.Key)
                            index = i;
                        i++;
                    }

                    actualAzimuth = azimuths[(index - 2 + azimuths.Count) % azimuths.Count];
                    azimuth = (actualAzimuth < mOppositeAzimuthA ? actualAzimuth - mOppositeAzimuthA : actualAzimuth - mOppositeAzimuthA - 360);
                    mAzimuthsA.Add(azimuth);
                    mActualAzimuthMapA[azimuth] = actualAzimuth;

                    actualAzimuth = azimuths[(index - 1 + azimuths.Count) % azimuths.Count];
                    azimuth = (actualAzimuth < mOppositeAzimuthA ? actualAzimuth - mOppositeAzimuthA : actualAzimuth - mOppositeAzimuthA - 360);
                    mAzimuthsA.Add(azimuth);
                    mActualAzimuthMapA[azimuth] = actualAzimuth;

                    mAzimuthsA.Add(0.0);
                    mActualAzimuthMapA[0.0] = mOppositeAzimuthA;

                    actualAzimuth = azimuths[(index + 1 + azimuths.Count) % azimuths.Count];
                    azimuth = (actualAzimuth < mOppositeAzimuthA ? actualAzimuth + 360 - mOppositeAzimuthA : actualAzimuth - mOppositeAzimuthA);
                    mAzimuthsA.Add(azimuth);
                    mActualAzimuthMapA[azimuth] = actualAzimuth;

                    actualAzimuth = azimuths[(index + 2 + azimuths.Count) % azimuths.Count];
                    azimuth = (actualAzimuth < mOppositeAzimuthA ? actualAzimuth + 360 - mOppositeAzimuthA : actualAzimuth - mOppositeAzimuthA);
                    mAzimuthsA.Add(azimuth);
                    mActualAzimuthMapA[azimuth] = actualAzimuth;
                }

                if (mEmiDataMgrB.AllChannelSamples.Count >= 5)
                {
                    double azimuth;
                    i = 0;
                    azimuths.Clear();
                    foreach (KeyValuePair<double, Dictionary<ChannelSetting, WatsEmiData>> pair in mEmiDataMgrB.AllChannelSamples)
                    {
                        azimuths.Add(pair.Key);
                        if (mOppositeAzimuthB == pair.Key)
                            index = i;
                        i++;
                    }

                    actualAzimuth = azimuths[(index - 2 + azimuths.Count) % azimuths.Count];
                    azimuth = (actualAzimuth < mOppositeAzimuthB ? actualAzimuth - mOppositeAzimuthB : actualAzimuth - mOppositeAzimuthB - 360);
                    mAzimuthsB.Add(azimuth);
                    mActualAzimuthMapB[azimuth] = actualAzimuth;

                    actualAzimuth = azimuths[(index - 1 + azimuths.Count) % azimuths.Count];
                    azimuth = (actualAzimuth < mOppositeAzimuthB ? actualAzimuth - mOppositeAzimuthB : actualAzimuth - mOppositeAzimuthB - 360);
                    mAzimuthsB.Add(azimuth);
                    mActualAzimuthMapB[azimuth] = actualAzimuth;

                    mAzimuthsB.Add(0.0);
                    mActualAzimuthMapB[0.0] = mOppositeAzimuthB;

                    actualAzimuth = azimuths[(index + 1 + azimuths.Count) % azimuths.Count];
                    azimuth = (actualAzimuth < mOppositeAzimuthB ? actualAzimuth + 360 - mOppositeAzimuthB : actualAzimuth - mOppositeAzimuthB);
                    mAzimuthsB.Add(azimuth);
                    mActualAzimuthMapB[azimuth] = actualAzimuth;

                    actualAzimuth = azimuths[(index + 2 + azimuths.Count) % azimuths.Count];
                    azimuth = (actualAzimuth < mOppositeAzimuthB ? actualAzimuth + 360 - mOppositeAzimuthB : actualAzimuth - mOppositeAzimuthB);
                    mAzimuthsB.Add(azimuth);
                    mActualAzimuthMapB[azimuth] = actualAzimuth;
                }

                mBitmapsA.Clear();
                foreach (double azimuth in mAzimuthsA)
                {
                    bmpInfo = MalaysiaReportPictureCreator.create(mEmiA, azimuth, mActualAzimuthMapA[azimuth], "SiteA: " + mEmiA.Site_ID + " - " + "SiteB: " + mEmiB.Site_ID, mEmiDataMgrA, mChannelSettings, mLimitSetting, minAbsRssi, maxAbsRssi);
                    mBitmapsA[azimuth] = bmpInfo;
                }

                mBitmapsB.Clear();
                foreach (double azimuth in mAzimuthsB)
                {
                    bmpInfo = MalaysiaReportPictureCreator.create(mEmiB, azimuth, mActualAzimuthMapB[azimuth], "SiteB: " + mEmiB.Site_ID + " - " + "SiteA: " + mEmiA.Site_ID, mEmiDataMgrB, mChannelSettings, mLimitSetting, minAbsRssi, maxAbsRssi);
                    mBitmapsB[azimuth] = bmpInfo;
                }
            }
            else
            {
                mBitmapsA.Clear();
                foreach (KeyValuePair<double, Dictionary<ChannelSetting, WatsEmiData>> pair in mEmiDataMgrA.AllChannelSamples)
                {
                    mAzimuthsA.Add(pair.Key);
                    bmpInfo = MalaysiaReportPictureCreator.create(mEmiA, pair.Key, pair.Key, "SiteA: " + mEmiA.Site_ID, mEmiDataMgrA, mChannelSettings, mLimitSetting, minAbsRssi, maxAbsRssi);
                    mBitmapsA[pair.Key] = bmpInfo;

                    mActualAzimuthMapA[pair.Key] = pair.Key;
                }

                mBitmapsB.Clear();
                foreach (KeyValuePair<double, Dictionary<ChannelSetting, WatsEmiData>> pair in mEmiDataMgrB.AllChannelSamples)
                {
                    mAzimuthsB.Add(pair.Key);
                    bmpInfo = MalaysiaReportPictureCreator.create(mEmiB, pair.Key, pair.Key, "SiteB: " + mEmiB.Site_ID, mEmiDataMgrB, mChannelSettings, mLimitSetting, minAbsRssi, maxAbsRssi);
                    mBitmapsB[pair.Key] = bmpInfo;

                    mActualAzimuthMapB[pair.Key] = pair.Key;
                }
            }
        }

        private void MalaysiaReportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Utility.ClearTempFiles();
            MainForm.Instance.Show();
        }

        private void ShowGraph()
        {
            if (EmiComboBox.SelectedIndex == -1 || AzimuthComboBox.SelectedIndex == -1)
                return;

            VerticalPictureBox.ImageLocation = null;
            HorizontalPictureBox.ImageLocation = null;

            if (EmiComboBox.SelectedIndex == 0)
            {
                VerticalPictureBox.ImageLocation = mBitmapsA[(double)AzimuthComboBox.SelectedItem].BmpFile1;
                HorizontalPictureBox.ImageLocation = mBitmapsA[(double)AzimuthComboBox.SelectedItem].BmpFile2;

                VerticalPictureBox.Tag = HorizontalPictureBox.Tag = mBitmapsA[(double)AzimuthComboBox.SelectedItem];
            }
            else if (EmiComboBox.SelectedIndex == 1)
            {
                VerticalPictureBox.ImageLocation = mBitmapsB[(double)AzimuthComboBox.SelectedItem].BmpFile1;
                HorizontalPictureBox.ImageLocation = mBitmapsB[(double)AzimuthComboBox.SelectedItem].BmpFile2;

                VerticalPictureBox.Tag = HorizontalPictureBox.Tag = mBitmapsB[(double)AzimuthComboBox.SelectedItem];
            }
        }

        private void AzimuthComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowGraph();
        }

        private void VerticalPictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || VerticalPictureBox.ImageLocation == null)
                return;

            PictureForm form = new PictureForm();
            form.Title = ((BitMapInfo)VerticalPictureBox.Tag).Title1;
            form.ImageLocation = VerticalPictureBox.ImageLocation;
            form.Width = 700;
            form.Height = 780;
            form.ShowDialog();
        }

        private void HorizontalPictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || HorizontalPictureBox.ImageLocation == null)
                return;

            PictureForm form = new PictureForm();
            form.Title = ((BitMapInfo)HorizontalPictureBox.Tag).Title2;
            form.ImageLocation = HorizontalPictureBox.ImageLocation;
            form.Width = 700;
            form.Height = 780;
            form.ShowDialog();
        }
    }
}
