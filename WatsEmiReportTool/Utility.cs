using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;
using WatsEMIAnalyzer.EMI;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;

namespace WatsEmiReportTool
{
    public class Utility
    {
        private const double DOUBLE_DELTA = 1E-05;
        private const double FLOAT_DELTA = 1E-03;
        
        private static double mExcelVersion = -1.0f;
        private static bool mIsFirstExcelVersionParsed = true;
        private static double mWordVersion = -1.0f;
        private static bool mIsFirstWordVersionParsed = true;

        private const string CHANNEL_NAME_PATTERN = @"(?<Name>(.*[^\d])?)(?<ID>\d+)";

        public static bool IsChannelClosed(string channel1Name, string channel2Name)
        {
            Regex regex = new Regex(CHANNEL_NAME_PATTERN);

            Match match1 = regex.Match(channel1Name);
            if (!match1.Success)
                return false;
            string name1 = match1.Groups["Name"].ToString();
            int id1 = Int32.Parse(match1.Groups["ID"].ToString());

            Match match2 = regex.Match(channel2Name);
            if (!match2.Success)
                return false;
            string name2 = match2.Groups["Name"].ToString();
            int id2 = Int32.Parse(match2.Groups["ID"].ToString());

            if (!name1.Equals(name2, StringComparison.OrdinalIgnoreCase))
                return false;

            return (Math.Abs(id1 - id2) == 1);
        }

        private static int SortWatsEmiSampleByFreq(WatsEmiSample sample1, WatsEmiSample sample2)
        {
            return sample1.mFreq.CompareTo(sample2.mFreq);
        }
        /*
        public Dictionary<double, Dictionary<ChannelSetting, WatsEmiData>> AllChannelSamples
            = new Dictionary<double, Dictionary<ChannelSetting, WatsEmiData>>();

        public Dictionary<double, Dictionary<int, List<WatsEmiSample>>> AllSamples
            = new Dictionary<double, Dictionary<int, List<WatsEmiSample>>>();
        */
        private static void SortEmiData(WatsEmiDataManager watsEmiDataMgr)
        {
            Dictionary<ChannelSetting, WatsEmiData> channelSamples;
            foreach (KeyValuePair<double, Dictionary<ChannelSetting, WatsEmiData>> pair in watsEmiDataMgr.AllChannelSamples)
            {
                channelSamples = pair.Value;
                foreach (KeyValuePair<ChannelSetting, WatsEmiData> pair2 in pair.Value)
                {
                    pair2.Value.mHPairSamples.Sort(SortWatsEmiSampleByFreq);
                    pair2.Value.mHSamples.Sort(SortWatsEmiSampleByFreq);
                    pair2.Value.mVSamples.Sort(SortWatsEmiSampleByFreq);
                    pair2.Value.mVPairSamples.Sort(SortWatsEmiSampleByFreq);
                }
            }

            Dictionary<int, List<WatsEmiSample>> samples;
            foreach (KeyValuePair<double, Dictionary<int, List<WatsEmiSample>>> pair in watsEmiDataMgr.AllSamples)
            {
                samples = pair.Value;
                foreach (KeyValuePair<int, List<WatsEmiSample>> pair2 in pair.Value)
                {
                    pair2.Value.Sort(SortWatsEmiSampleByFreq);
                }
            }
        }

        public static WatsEmiDataManager GetEmiDataManager(EMIFileData emiFileData, List<ChannelSetting> channelSettings)
        {
            WatsEmiDataManager watsEmiDataManager = new WatsEmiDataManager();

            Dictionary<double, double> startFrequencys = new Dictionary<double, double>();
            Dictionary<double, double> endFrequencys = new Dictionary<double, double>();
            Dictionary<ChannelSetting, WatsEmiData> datas;
            Dictionary<int, List<WatsEmiSample>> samples = null;
            ChannelSetting curChannelSetting;
            WatsEmiData curData;
            foreach (DG_Type dataGroup in emiFileData.DataGroups)
            {
                if (!startFrequencys.ContainsKey(dataGroup.DG_FB_Angle))
                    startFrequencys[dataGroup.DG_FB_Angle] = dataGroup.DG_FB_Start;
                else if (dataGroup.DG_FB_Start < startFrequencys[dataGroup.DG_FB_Angle])
                    startFrequencys[dataGroup.DG_FB_Angle] = dataGroup.DG_FB_Start;

                if (!endFrequencys.ContainsKey(dataGroup.DG_FB_Angle))
                    endFrequencys[dataGroup.DG_FB_Angle] = dataGroup.DG_FB_End;
                else if (dataGroup.DG_FB_End > endFrequencys[dataGroup.DG_FB_Angle])
                    endFrequencys[dataGroup.DG_FB_Angle] = dataGroup.DG_FB_End;

                if (!watsEmiDataManager.AllChannelSamples.TryGetValue(dataGroup.DG_FB_Angle, out datas))
                {
                    datas = new Dictionary<ChannelSetting, WatsEmiData>();
                    watsEmiDataManager.AllChannelSamples.Add(dataGroup.DG_FB_Angle, datas);
                }

                if (!watsEmiDataManager.AllSamples.TryGetValue(dataGroup.DG_FB_Angle, out samples))
                {
                    samples = new Dictionary<int, List<WatsEmiSample>>();
                    samples[0] = new List<WatsEmiSample>();
                    samples[1] = new List<WatsEmiSample>();
                    watsEmiDataManager.AllSamples.Add(dataGroup.DG_FB_Angle, samples);
                }

                foreach (DG_Data_Type data in dataGroup.DGDatas)
                {
                    if (dataGroup.DB_FB_AntennaPolarization == 0)
                        samples[0].Add(new WatsEmiSample(data.DG_DI_Freq, data.DG_DI_RSSI));
                    else //if (dataGroup.DB_FB_AntennaPolarization == 1)
                        samples[1].Add(new WatsEmiSample(data.DG_DI_Freq, data.DG_DI_RSSI));

                    curChannelSetting = null;
                    foreach (ChannelSetting channelSetting in channelSettings)
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

            SortEmiData(watsEmiDataManager);
            return watsEmiDataManager;
        }

        public static void ClearTempFiles()
        {
            int count = 0;
            while (true)
            {
                try
                {
                    DirectoryInfo tempFolder = new DirectoryInfo(Utility.GetAppPath() + "\\Temp");
                    foreach (FileInfo fileInfo in tempFolder.GetFiles())
                    {
                        try
                        {
                            Debug.Write("Remove File \"" + fileInfo.FullName + "\", ");
                            File.Delete(fileInfo.FullName);
                            Debug.WriteLine("Successfully !");
                        }
                        catch (System.Exception ex)
                        {
                            Debug.WriteLine("Failed, " + ex.Message + " !");
                        }
                    }
                    break;
                }
                catch (System.Exception e)
                {
                    if (++count > 10)
                    {
                        Thread.Sleep(10);
                        break;
                    }
                }
            }
        }

        public static double GetExcelVersion()
        {
            if (mIsFirstExcelVersionParsed)
            {
                System.Globalization.CultureInfo Oldci = null;
                mIsFirstExcelVersionParsed = false;
                Excel.Application app = null;
                try
                {
                    Oldci = System.Threading.Thread.CurrentThread.CurrentCulture;
                    System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");
                    app = new Excel.Application();
                    double.TryParse(app.Version, out mExcelVersion);
                }
                catch (Exception e)
                {
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

                    ReleaseCom(app);

                    GC.Collect(System.GC.GetGeneration(app));
                    GC.Collect(); 
                }
            }
            return mExcelVersion;
        }

        public static bool Excel2003Installed()
        {
            RegistryKey rk = Registry.LocalMachine;
            RegistryKey key = rk.OpenSubKey(@"SOFTWARE\Microsoft\Office\11.0\Excel\InstallRoot\");

            return key != null;
        }

        public static bool Excel2007Installed()
        {
            RegistryKey rk = Registry.LocalMachine;
            RegistryKey key = rk.OpenSubKey(@"SOFTWARE\Microsoft\Office\12.0\Excel\InstallRoot\");

            return key != null;
        }

        public static double GetWordVersion()
        {
            if (mIsFirstExcelVersionParsed)
            {
                System.Globalization.CultureInfo Oldci = null;
                mIsFirstWordVersionParsed = false;
                Excel.Application app = null;
                try
                {
                    Oldci = System.Threading.Thread.CurrentThread.CurrentCulture;
                    System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");
                    app = new Excel.Application();
                    double.TryParse(app.Version, out mWordVersion);
                }
                catch (Exception e)
                {
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

                    ReleaseCom(app);

                    GC.Collect(System.GC.GetGeneration(app));
                    GC.Collect();
                }
            }
            return mWordVersion;
        }

        public static bool Word2003Installed()
        {
            RegistryKey rk = Registry.LocalMachine;
            RegistryKey key = rk.OpenSubKey(@"SOFTWARE\Microsoft\Office\11.0\Word\InstallRoot\");

            return key != null;
        }

        public static bool Word2007Installed()
        {
            RegistryKey rk = Registry.LocalMachine;
            RegistryKey key = rk.OpenSubKey(@"SOFTWARE\Microsoft\Office\12.0\Word\InstallRoot\");

            return key != null;
        }

        public static bool DoubleEquals(double value1, double value2)
        {
            return (value1 == value2)
                || Math.Abs(value1 - value2) < DOUBLE_DELTA;
        }

        public static bool DoubleLessOrEqual(double value1, double value2)
        {
            return (value1 < value2)
                || DoubleEquals(value1, value2);
        }

        public static bool DoubleBiggerOrEqual(double value1, double value2)
        {
            return (value1 > value2)
                || DoubleEquals(value1, value2);
        }

        public static bool FloatEquals(float value1, float value2)
        {
            return (value1 == value2)
                || Math.Abs(value1 - value2) < FLOAT_DELTA;
        }

        public static string ConvertDoubleString(double value)
        {
            string str = value.ToString("f2");
            if (str.EndsWith(".00"))
                str = str.Substring(0, str.Length - 3);

            return str;
        }

        public static string GetAppPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public static string GetDateTimeAsFileName()
        {
            DateTime now = DateTime.Now;
            return now.Year.ToString() + now.Month.ToString() + now.Day.ToString()
                + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString();
        }

        public static string ConvertLongtitude(double longtitudeValue)
        {
            long totalSeconds = (long)Math.Abs(longtitudeValue * 3600);
            int degree = (int)(totalSeconds/3600);
            int cent = (int)((totalSeconds - degree*3600)/60);
            float seconds = (float)(Math.Abs(longtitudeValue*3600) - degree * 3600 - cent*60);

            return degree.ToString() + "\x00B0" 
                + cent.ToString() + "'" 
                + seconds.ToString() + "\""
                + " "
                + (longtitudeValue >= 0 ? "E" : "W");

        }

        public static string ConvertLatitude(double latitudeValue)
        {
            long totalSeconds = (long)Math.Abs(latitudeValue * 3600);
            int degree = (int)(totalSeconds / 3600);
            int cent = (int)((totalSeconds - degree * 3600) / 60);
            float seconds = (float)(Math.Abs(latitudeValue * 3600) - degree * 3600 - cent * 60);

            return degree.ToString() + "\x00B0"
                + cent.ToString() + "'"
                + seconds.ToString() + "\""
                + " "
                + (latitudeValue >= 0 ? "N" : "S");
        }

        public static string ConvertToDate(string dateTime)
        {
            string year = dateTime.Substring(0, 4);
            string month = dateTime.Substring(4, 2);
            string day = dateTime.Substring(6, 2);

            return year + "-" + month + "-" + day;
        }

        public static void ReleaseCom(object pObj)
        {
            if (pObj == null)
                return;


            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pObj);
            }
            catch
            {
                //...
            }
            finally
            {
                pObj = null;
            }
        }

        public static double CalculateDeltaPower(List<WatsEmiSample> samples)
        {
            if (samples.Count == 0)
                return 0;

            List<WatsEmiSample> sortedSamples = new List<WatsEmiSample>();
            foreach (WatsEmiSample sample in samples)
                sortedSamples.Add(new WatsEmiSample(sample.mFreq, sample.mRssi));

            sortedSamples.Sort(SortSampleByRssi);

            int minCount = (int)Math.Round(samples.Count * 0.05);
            if (minCount <= 0)
                minCount = 1;

            double averageMin = 0, max = sortedSamples[samples.Count - 1].mRssi;
            for (int i = 0; i < minCount; i++)
                averageMin += sortedSamples[i].mRssi;

            averageMin /= minCount;

            return Math.Abs(max - averageMin);
        }

        private static int SortSampleByRssi(WatsEmiSample sample1, WatsEmiSample sample2)
        {
            return sample1.mRssi.CompareTo(sample2.mRssi);
        }

        public static Marker CreateMarker(string channelName, List<WatsEmiSample> channelSamples)
        {
            Marker marker = new Marker();
            marker.channelName = channelName;
            double minmumAbsRssi = double.MaxValue;
            foreach (WatsEmiSample sample in channelSamples)
            {
                if (Math.Abs(sample.mRssi) < minmumAbsRssi)
                {
                    minmumAbsRssi = Math.Abs(sample.mRssi);
                    marker.frequency = sample.mFreq;
                    marker.rssi = sample.mRssi;
                }
            }

            return marker;
        }

        public static int SortMarkerByFrequency(Marker marker1, Marker marker2)
        {
            return marker1.frequency.CompareTo(marker2.frequency);
        }


    }
}
