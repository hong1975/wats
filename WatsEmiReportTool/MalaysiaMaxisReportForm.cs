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
using Word = Microsoft.Office.Interop.Word;
using System.Globalization;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

namespace WatsEmiReportTool
{
    public partial class MalaysiaMaxisReportForm : Form
    {
        private static Regex channelRegex = new Regex(@"^(?<BandName>\d+[gG]).*$");

        private const float GRAPH_WIDTH = 220f;
        private const float GRAPH_HEIGHT = 230f;

        private LimitSetting mLimitSetting;
        private EMIFileData mEmi;
        private WatsEmiDataManager mEmiDataMgr;
        List<double> mAzimuths = new List<double>();

        private Dictionary<string, List<ChannelSetting>> mAllBandChannels 
            = new Dictionary<string,List<ChannelSetting>>();

        private string mExportFileName;
        private Thread mExportThread;
        private bool mCancelExport = false;
        private ExportStatusForm mExportStatusForm;
        private delegate void UpdateStatusDelegate(string status);

        private List<Dictionary<string, List<BitMapInfo>>> mBitmaps
            = new List<Dictionary<string, List<BitMapInfo>>>();

        private static string GetBandName(ChannelSetting channelSetting)
        {
            Match match;
            string bandName = "";
            match = channelRegex.Match(channelSetting.ChannelName);
            if (match.Success)
                bandName = match.Groups["BandName"].Value;

            return bandName;
        }

        public MalaysiaMaxisReportForm(LimitSetting limitSetting, 
            EMIFileData emiFileData, List<ChannelSetting> channelSettings)
        {
            mLimitSetting = limitSetting;
            mEmi = emiFileData;

            string bandName;
            foreach (ChannelSetting channelSetting in channelSettings)
            {
                bandName = GetBandName(channelSetting);
                if (!mAllBandChannels.ContainsKey(bandName))
                {
                    mAllBandChannels[bandName] = new List<ChannelSetting>();
                }
                mAllBandChannels[bandName].Add(channelSetting);
            }

            mEmiDataMgr = Utility.GetEmiDataManager(mEmi, channelSettings);
            mExportStatusForm = new ExportStatusForm(this);

            InitializeComponent();
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

            string str = doc.Range(ref objStart, ref objEnd).Text;

            object unit = (int)Word.WdUnits.wdCharacter;
            object count = 1;
            doc.Range(ref objStart, ref objEnd).Delete(ref unit, ref count);
        }

        private void MalaysiaMaxisReportForm_Load(object sender, EventArgs e)
        {
            EMIFileNameEditor.Text = mEmi.Site_ID + " (User: " + mEmi.PA_UserName + ", Time: " + mEmi.PA_TestTime + ")";
            
            AzimuthComboBox.Items.Add("0\x00B0");
            AzimuthComboBox.Items.Add("30\x00B0");
            AzimuthComboBox.Items.Add("-30\x00B0");
            AzimuthComboBox.Items.Add("60\x00B0");
            AzimuthComboBox.Items.Add("-60\x00B0");
            AzimuthComboBox.SelectedIndex = 0;
            foreach (KeyValuePair<string, List<ChannelSetting>> pair in mAllBandChannels)
            {
                BandComboBox.Items.Add(pair.Key + " Lo");
                BandComboBox.Items.Add(pair.Key + " Hi");
            }
            if (BandComboBox.Items.Count > 0)
                BandComboBox.SelectedIndex = 0;

            SiteIDLabel.Text = mEmi.Site_ID;
            LongtitudeLabel.Text = Utility.ConvertLongtitude(mEmi.Site_Longitude);
            LatitudeLabel.Text = Utility.ConvertLatitude(mEmi.Site_Longitude);
            DateLabel.Text = mEmi.PA_TestTime;
            EngineerLabel.Text = mEmi.PA_UserName;
            if (mLimitSetting.UseChannelPowerLimit)
                PChannelLimitLabel.Text = mLimitSetting.ChannelPowerLimit.ToString();
            else
                PChannelLimitLabel.Text = "";

            if (mLimitSetting.UseDeltaPowerLimit)
                PLevelLimitLabel.Text = mLimitSetting.DeltaPowerLimit.ToString();
            else
                PLevelLimitLabel.Text = "";

            GenerateGraphs();
            ShowGraph();

            if (mBitmaps.Count == 0)
            {
                ExportButton.Enabled = false;
                MessageBox.Show("EMI doesn't contain 5 azimuths data !");
                Close();
            }
        }

        private void MalaysiaMaxisReportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Utility.ClearTempFiles();
            MainForm.Instance.Show();
        }

        private void AzimuthComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowGraph();
        }

        private void BandComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowGraph();
        }

        private void GenerateGraphs()
        {
            int minAbsRssi = Int32.MaxValue, maxAbsRssi = Int32.MinValue;
            Dictionary<int, List<WatsEmiSample>> samples;
            foreach (KeyValuePair<double, Dictionary<int, List<WatsEmiSample>>> pair in mEmiDataMgr.AllSamples)
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

            int i;
            Dictionary<string, List<BitMapInfo>> bmpInfos;
            if (mEmiDataMgr.AllChannelSamples.Count >= 5)
            {
                List<double> tempAzimuths = new List<double>();
                foreach (KeyValuePair<double, Dictionary<ChannelSetting, WatsEmiData>> pair in mEmiDataMgr.AllChannelSamples)
                {
                    tempAzimuths.Add(pair.Key);
                }
                tempAzimuths.Sort();
                mAzimuths.Add(tempAzimuths[0]);
                for(i = 1; i < 5; i++)
                {
                    for (int j = 1; j < tempAzimuths.Count; j++) 
                    {
                        if (tempAzimuths[j] == tempAzimuths[0] + 30 * i)
                            mAzimuths.Add(tempAzimuths[j]);
                    }
                }

                if (mAzimuths.Count != 5)
                    return;

                tempAzimuths.Clear();
                tempAzimuths.Add(mAzimuths[2]); //0
                tempAzimuths.Add(mAzimuths[3]); //30
                tempAzimuths.Add(mAzimuths[1]); //-30
                tempAzimuths.Add(mAzimuths[4]); //60
                tempAzimuths.Add(mAzimuths[0]); //-60

                mAzimuths = tempAzimuths;
                List<string> relativeAzimuths = new List<string>();
                relativeAzimuths.Add("0");
                relativeAzimuths.Add("30");
                relativeAzimuths.Add("-30");
                relativeAzimuths.Add("60");
                relativeAzimuths.Add("-60");
                i = 0;
                foreach (double azimuth in mAzimuths)
                {
                    bmpInfos = MalaysiaMaxisReportPictureCreator.create(mEmi, azimuth, relativeAzimuths[i],
                        mEmiDataMgr, mAllBandChannels, mLimitSetting, minAbsRssi, maxAbsRssi);
                    mBitmaps.Add(bmpInfos);
                    i++;
                }
            }
        }

        private void ShowGraph()
        {
            if (mBitmaps.Count == 0 || AzimuthComboBox.SelectedIndex == -1 || BandComboBox.SelectedIndex == -1)
            {
                HorizontalPictureBox.Image = (System.Drawing.Image)Properties.Resources.blank;
                VerticalPictureBox.Image = (System.Drawing.Image)Properties.Resources.blank;
                return;
            }

            int azimuthIndex = AzimuthComboBox.SelectedIndex;
            string bandName = BandComboBox.SelectedItem.ToString().Substring(0, 
                BandComboBox.SelectedItem.ToString().Length - 3);
            int lowOrHighBand = BandComboBox.SelectedIndex % 2; //0: vertical, 1: horizontal
            VerticalPictureBox.ImageLocation = mBitmaps[azimuthIndex][bandName][lowOrHighBand].BmpFile1;
            HorizontalPictureBox.ImageLocation = mBitmaps[azimuthIndex][bandName][lowOrHighBand].BmpFile2;

            VerticalPictureBox.Tag = HorizontalPictureBox.Tag = mBitmaps[azimuthIndex][bandName][lowOrHighBand];
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
                 + "MalaysiaMaxisTemplate.doc";
                saveFileDialog.Filter = "report file(*.doc)|*.doc";
            }
            else
            {
                reportTemplateFile = System.AppDomain.CurrentDomain.BaseDirectory
                 + "MalaysiaMaxisTemplate.docx";
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

                    //Remove picture page
                    int removePageCount = (8 - mAllBandChannels.Count) * 5;
                    for (int i = 0; i < removePageCount; i++)
                        RemovePage(doc, 32);

                    //Remove table page
                    removePageCount = 10 - mAllBandChannels.Count;
                    for (int i = 0; i < removePageCount; i++)
                        RemovePage(doc, 18);

                    int j = 0;
                    List<ChannelSetting> channelSettings;
                    ChannelPower channelPower;
                    foreach (KeyValuePair<string, List<ChannelSetting>> pair in mAllBandChannels)
                    {
                        channelSettings = pair.Value;

                        //Band Name
                        doc.Tables[19 + j * 2].Cell(1, 1).Range.Text = pair.Key;

                        //Frequency data table
                        for (int i = 0; i < channelSettings.Count; i++)
                        {
                            //low band
                            doc.Tables[20 + j * 2].Cell(3 + i, 1).Range.Text = (i + 1).ToString();
                            doc.Tables[20 + j * 2].Cell(3 + i, 2).Range.Text = channelSettings[i].CenterFreq.ToString();

                            //high band
                            doc.Tables[20 + j * 2].Cell(20 + i, 1).Range.Text = (i + 1).ToString();
                            doc.Tables[20 + j * 2].Cell(20 + i, 2).Range.Text = channelSettings[i].Pair.CenterFreq.ToString();

                            for (int k = 0; k < mAzimuths.Count; k++)
                            {
                                channelPower = new ChannelPower(mEmi.SA_RBW, channelSettings[i], mLimitSetting,
                                    mEmiDataMgr.AllChannelSamples[mAzimuths[k]][channelSettings[i]]);

                                //low band
                                doc.Tables[20 + j * 2].Cell(3 + i, 2 + k * 2 + 1).Range.Text 
                                    = channelPower.IsValidVPower ? "" : "X";
                                doc.Tables[20 + j * 2].Cell(3 + i, 2 + k * 2 + 2).Range.Text
                                    = channelPower.IsValidHPower ? "" : "X";

                                //high band
                                doc.Tables[20 + j * 2].Cell(20 + i, 2 + k * 2 + 1).Range.Text
                                    = channelPower.IsValidVPairPower ? "" : "X";
                                doc.Tables[20 + j * 2].Cell(20 + i, 2 + k * 2 + 2).Range.Text
                                    = channelPower.IsValidHPairPower ? "" : "X";
                            }
                        }

                        j++;
                    }

                    //Graph table
                    object linkToFile = false;
                    object saveWithDocument = true;
                    object anchor; ;
                    Word.InlineShape shape;
                    int graphStartTableIndex = 18 + mAllBandChannels.Count * 2 + 2;
                    j = 0;
                    foreach (KeyValuePair<string, List<ChannelSetting>> pair in mAllBandChannels)
                    {
                        //band title
                        doc.Tables[graphStartTableIndex + j * 11].Cell(1, 1).Range.Text
                            = "FREQUENCY SCANNING SCREEN SHOT FOR " + pair.Key + " MDEF - LAXI";

                        for (int i = 0; i < 5; i++)
                        {
                            //vertical ...
                            //low band
                            doc.Tables[graphStartTableIndex + j * 11 + i * 2 + 1].Cell(1, 1).Select();
                            anchor = doc.Application.Selection.Range;
                            shape = doc.Application.ActiveDocument.InlineShapes.AddPicture(mBitmaps[i][pair.Key][0].BmpFile1,
                                ref linkToFile, ref saveWithDocument, ref anchor);
                            shape.Width = GRAPH_WIDTH;
                            shape.Height = GRAPH_HEIGHT;

                            //high band
                            doc.Tables[graphStartTableIndex + j * 11 + i * 2 + 1].Cell(1, 3).Select();
                            anchor = doc.Application.Selection.Range;
                            shape = doc.Application.ActiveDocument.InlineShapes.AddPicture(mBitmaps[i][pair.Key][1].BmpFile1,
                                ref linkToFile, ref saveWithDocument, ref anchor);
                            shape.Width = GRAPH_WIDTH;
                            shape.Height = GRAPH_HEIGHT;

                            //horizontal ...
                            //low band
                            doc.Tables[graphStartTableIndex + j * 11 + i * 2 + 2].Cell(1, 1).Select();
                            anchor = doc.Application.Selection.Range;
                            shape = doc.Application.ActiveDocument.InlineShapes.AddPicture(mBitmaps[i][pair.Key][0].BmpFile2,
                                ref linkToFile, ref saveWithDocument, ref anchor);
                            shape.Width = GRAPH_WIDTH;
                            shape.Height = GRAPH_HEIGHT;

                            //high band
                            doc.Tables[graphStartTableIndex + j * 11 + i * 2 + 2].Cell(1, 3).Select();
                            anchor = doc.Application.Selection.Range;
                            shape = doc.Application.ActiveDocument.InlineShapes.AddPicture(mBitmaps[i][pair.Key][1].BmpFile2,
                                ref linkToFile, ref saveWithDocument, ref anchor);
                            shape.Width = GRAPH_WIDTH;
                            shape.Height = GRAPH_HEIGHT;
                        }

                        j++;
                    }

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
