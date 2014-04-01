using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Globalization;
using Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using System.IO;

namespace WatsEmiReportTool
{
    public class ChannelSettingReader
    {
        private static List<ChannelSetting> ReadCsvFile(string channelSettingFile)
        {
            Regex regex = new Regex(@"(?<No>[1-9]\d*),"
                + @"(?<ChName1>[^,]+),"
                + @"((?<CentFreq1>[1-9]\d*(\.\d*)?)" + "|\"(?<CentFreq1>[\\d,\\.]+)\"),"
                + @"((?<BandWidth1>[1-9]\d*(\.\d*)?)" + "|\"(?<BandWidth1>[\\d,\\.]+)\"),"
                + @"((?<StartFreq1>[1-9]\d*(\.\d*)?)" + "|\"(?<StartFreq1>[\\d,\\.]+)\"),"
                + @"((?<EndFreq1>[1-9]\d*(\.\d*)?)" + "|\"(?<EndFreq1>[\\d,\\.]+)\"),"
                + @"(?<ODUSubBand1>[^,]+),"
                + @"(?<SP>[^,]*),"
                + @"(?<ChName2>[^,]+),"
                + @"((?<CentFreq2>[1-9]\d*(\.\d*)?)" + "|\"(?<CentFreq2>[\\d,\\.]+)\"),"
                + @"((?<BandWidth2>[1-9]\d*(\.\d*)?)" + "|\"(?<BandWidth2>[\\d,\\.]+)\"),"
                + @"((?<StartFreq2>[1-9]\d*(\.\d*)?)" + "|\"(?<StartFreq2>[\\d,\\.]+)\"),"
                + @"((?<EndFreq2>[1-9]\d*(\.\d*)?)" + "|\"(?<EndFreq2>[\\d,\\.]+)\"),"
                + @"(?<ODUSubBand2>[^,]+)");

            Match match;
            ChannelSetting channelSetting;
            List<ChannelSetting> settings = new List<ChannelSetting>();
            if (!File.Exists(channelSettingFile))
                return settings;

            try
            {
                using (FileStream fs = new FileStream(channelSettingFile, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(channelSettingFile, Encoding.Default))
                    {
                        fs.Seek(0, SeekOrigin.Begin);
                        string line = sr.ReadLine();
                        while (line != null)
                        {
                            match = regex.Match(line);
                            if (!match.Success)
                            {
                                line = sr.ReadLine();
                                continue;
                            }

                            channelSetting = new ChannelSetting();
                            channelSetting.ChannelName = match.Groups["ChName1"].Value;
                            channelSetting.CenterFreq = double.Parse(match.Groups["CentFreq1"].Value);
                            channelSetting.BandWidth = double.Parse(match.Groups["BandWidth1"].Value);
                            channelSetting.StartFreq = double.Parse(match.Groups["StartFreq1"].Value);
                            channelSetting.EndFreq = double.Parse(match.Groups["EndFreq1"].Value);
                            channelSetting.ODUSubBand = match.Groups["ODUSubBand1"].Value;

                            //Check Frequency Setting
                            if (!Utility.DoubleEquals(channelSetting.StartFreq, channelSetting.CenterFreq - channelSetting.BandWidth / 2)
                                || !Utility.DoubleEquals(channelSetting.EndFreq, channelSetting.CenterFreq + channelSetting.BandWidth / 2))
                            {
                                throw new InvalidChannelSettingException(
                                    "Invalid Frequency Setting of channel '" + channelSetting.ChannelName);
                            }

                            channelSetting.Pair = new ChannelSetting();
                            channelSetting.Pair.ChannelName = match.Groups["ChName2"].Value;
                            channelSetting.Pair.CenterFreq = double.Parse(match.Groups["CentFreq2"].Value);
                            channelSetting.Pair.BandWidth = double.Parse(match.Groups["BandWidth2"].Value);
                            channelSetting.Pair.StartFreq = double.Parse(match.Groups["StartFreq2"].Value);
                            channelSetting.Pair.EndFreq = double.Parse(match.Groups["EndFreq2"].Value);
                            channelSetting.Pair.ODUSubBand = match.Groups["ODUSubBand2"].Value;

                            //Check Frequency Setting
                            if (!Utility.DoubleEquals(channelSetting.Pair.StartFreq, channelSetting.Pair.CenterFreq - channelSetting.Pair.BandWidth / 2)
                                || !Utility.DoubleEquals(channelSetting.Pair.EndFreq, channelSetting.Pair.CenterFreq + channelSetting.Pair.BandWidth / 2))
                            {
                                throw new InvalidChannelSettingException(
                                    "Invalid Frequency Setting of channel '" + channelSetting.Pair.ChannelName);
                            }

                            if (channelSetting.StartFreq <= channelSetting.Pair.StartFreq)
                                settings.Add(channelSetting);
                            else
                                settings.Add(channelSetting.Pair);

                            line = sr.ReadLine();
                        } 
                    }
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {

            }

            return settings;
        }

        private static List<ChannelSetting> ReadXmlFile(string channelSettingFile)
        {
            List<ChannelSetting> settings = new List<ChannelSetting>();

            System.Globalization.CultureInfo Oldci = null;
            Excel._Application app = null;
            Excel.WorkbookClass workBook = null;
            Excel.Sheets sheets = null;
            Excel.Worksheet sheet = null;
            try
            {
                Oldci = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");

                app = new Excel.Application();
                object objOpt = System.Reflection.Missing.Value;

                workBook = (Excel.WorkbookClass)app.Workbooks.Open(
                    channelSettingFile, objOpt, false, objOpt, objOpt, objOpt, true,
                    objOpt, objOpt, true, objOpt, objOpt, objOpt, objOpt, objOpt);

                sheets = workBook.Worksheets;
                sheet = (Excel.Worksheet)sheets[1];

                ChannelSetting channelSetting1;
                ChannelSetting channelSetting2;
                string str;

                for (int row = 2; ; row++)
                {
                    /************************************************************************/
                    /* Primary Channel                                                      */
                    /************************************************************************/
                    channelSetting1 = new ChannelSetting();

                    //Channel Name
                    if (((Range)sheet.Cells[row, 2]).Text == null || ((Range)sheet.Cells[row, 2]).Text.ToString().Length == 0)
                    {
                        return settings;
                    }

                    channelSetting1.ChannelName = ((Range)sheet.Cells[row, 2]).Text.ToString().Trim();

                    //Center Freq
                    str = ((Range)sheet.Cells[row, 3]).Text.ToString().Trim();
                    if (!Regex.IsMatch(str, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        throw new InvalidChannelSettingException(
                            "Invalid Center Freq of channel '" + channelSetting1.ChannelName + "': row=" + row + ", col=" + 2);
                    }
                    channelSetting1.CenterFreq = double.Parse(str);

                    //Band Width
                    str = ((Range)sheet.Cells[row, 4]).Text.ToString().Trim();
                    if (!Regex.IsMatch(str, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        throw new InvalidChannelSettingException(
                            "Invalid Band Width of channel '" + channelSetting1.ChannelName + "': row=" + row + ", col=" + 2);
                    }
                    channelSetting1.BandWidth = double.Parse(str);

                    //Start Freq
                    str = ((Range)sheet.Cells[row, 5]).Text.ToString().Trim();
                    if (!Regex.IsMatch(str, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        throw new InvalidChannelSettingException(
                            "Invalid Start Freq of channel '" + channelSetting1.ChannelName + "': row=" + row + ", col=" + 2);
                    }
                    channelSetting1.StartFreq = double.Parse(str);

                    //End Freq
                    str = ((Range)sheet.Cells[row, 6]).Text.ToString().Trim();
                    if (!Regex.IsMatch(str, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        throw new InvalidChannelSettingException(
                            "Invalid End Freq of channel '" + channelSetting1.ChannelName + "': row=" + row + ", col=" + 2);
                    }
                    channelSetting1.EndFreq = double.Parse(str);

                    //Check Frequency Setting
                    if (!Utility.DoubleEquals(channelSetting1.StartFreq, channelSetting1.CenterFreq - channelSetting1.BandWidth / 2)
                        || !Utility.DoubleEquals(channelSetting1.EndFreq, channelSetting1.CenterFreq + channelSetting1.BandWidth / 2))
                    {
                        throw new InvalidChannelSettingException(
                            "Invalid Frequency Setting of channel '" + channelSetting1.ChannelName + "': row=" + row);
                    }

                    //ODU Sub band
                    if (((Range)sheet.Cells[row, 7]).Text == null)
                    {
                        throw new InvalidChannelSettingException(
                            "ODU Sub band of channel '" + channelSetting1.ChannelName + "' was not set: row=" + row + ", col=" + 2);
                    }
                    else if (((Range)sheet.Cells[row, 7]).Text.ToString().Length == 0)
                    {
                        throw new InvalidChannelSettingException(
                            "ODU Sub band of channel '" + channelSetting1.ChannelName + "' is empty: row=" + row + ", col=" + 2);
                    }
                    channelSetting1.ODUSubBand = ((Range)sheet.Cells[row, 7]).Text.ToString().Trim();

                    /************************************************************************/
                    /* Pair Channel                                                         */
                    /************************************************************************/
                    channelSetting2 = new ChannelSetting();

                    //Channel Name
                    if (((Range)sheet.Cells[row, 9]).Text == null)
                    {
                        throw new InvalidChannelSettingException(
                            "Channel Name was not set: row=" + row + ", col=" + 2);
                    }
                    else if (((Range)sheet.Cells[row, 9]).Text.ToString().Length == 0)
                    {
                        throw new InvalidChannelSettingException(
                            "Channel Name is empty: row=" + row + ", col=" + 2);
                    }
                    channelSetting2.ChannelName = ((Range)sheet.Cells[row, 9]).Text.ToString().Trim();

                    //Center Freq
                    str = ((Range)sheet.Cells[row, 10]).Text.ToString().Trim();
                    if (!Regex.IsMatch(str, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        throw new InvalidChannelSettingException(
                            "Invalid Center Freq of channel '" + channelSetting2.ChannelName + "': row=" + row + ", col=" + 2);
                    }
                    channelSetting2.CenterFreq = double.Parse(str);

                    //Band Width
                    str = ((Range)sheet.Cells[row, 11]).Text.ToString().Trim();
                    if (!Regex.IsMatch(str, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        throw new InvalidChannelSettingException(
                            "Invalid Band Width of channel '" + channelSetting2.ChannelName + "': row=" + row + ", col=" + 2);
                    }
                    channelSetting2.BandWidth = double.Parse(str);

                    //Start Freq
                    str = ((Range)sheet.Cells[row, 12]).Text.ToString().Trim();
                    if (!Regex.IsMatch(str, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        throw new InvalidChannelSettingException(
                            "Invalid Start Freq of channel '" + channelSetting2.ChannelName + "': row=" + row + ", col=" + 2);
                    }
                    channelSetting2.StartFreq = double.Parse(str);

                    //End Freq
                    str = ((Range)sheet.Cells[row, 13]).Text.ToString().Trim();
                    if (!Regex.IsMatch(str, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        throw new InvalidChannelSettingException(
                            "Invalid End Freq of channel '" + channelSetting2.ChannelName + "': row=" + row + ", col=" + 2);
                    }
                    channelSetting2.EndFreq = double.Parse(str);

                    //Check Frequency Setting
                    if (!Utility.DoubleEquals(channelSetting2.StartFreq, channelSetting2.CenterFreq - channelSetting2.BandWidth / 2)
                        || !Utility.DoubleEquals(channelSetting2.EndFreq, channelSetting2.CenterFreq + channelSetting2.BandWidth / 2))
                    {
                        throw new InvalidChannelSettingException(
                            "Invalid Frequency Setting of channel '" + channelSetting2.ChannelName + "': row=" + row);
                    }

                    //ODU Sub band
                    if (((Range)sheet.Cells[row, 14]).Text == null)
                    {
                        throw new InvalidChannelSettingException(
                            "ODU Sub band of channel '" + channelSetting2.ChannelName + "' was not set: row=" + row + ", col=" + 2);
                    }
                    else if (((Range)sheet.Cells[row, 14]).Text.ToString().Length == 0)
                    {
                        throw new InvalidChannelSettingException(
                            "ODU Sub band of channel '" + channelSetting2.ChannelName + "' is empty: row=" + row + ", col=" + 2);
                    }
                    channelSetting2.ODUSubBand = ((Range)sheet.Cells[row, 14]).Text.ToString().Trim();

                    channelSetting1.Pair = channelSetting2;
                    channelSetting2.Pair = channelSetting1;
                    if (channelSetting1.StartFreq <= channelSetting2.StartFreq)
                        settings.Add(channelSetting1);
                    else
                        settings.Add(channelSetting2);
                }
            }
            catch (Exception e)
            {
                throw e;
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
            }
        }

        public static List<ChannelSetting> Read(string channelSettingFile)
        {
            if (channelSettingFile.ToLower().EndsWith(".xls")
                || channelSettingFile.ToLower().EndsWith(".xlsx"))
            {
                return ReadXmlFile(channelSettingFile);
            }
            else if (channelSettingFile.ToLower().EndsWith(".csv"))
            {
                return ReadCsvFile(channelSettingFile);
            }
            else
            {
                return new List<ChannelSetting>();
            }
        }
    }
}
