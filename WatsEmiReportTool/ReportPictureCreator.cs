using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.IO;

namespace WatsEmiReportTool
{
    public class ReportPictureCreator
    {
        public static List<BitMapInfo> create(double angle, 
            Dictionary<int, List<WatsEmiSample>> angleSamples, 
            List<ChannelSetting> channelSettings, bool displayChannel,
            int minAbsRssi, int maxAbsRssi, List<FrequencyRange> ranges, double maxShowFreq)
        {
            List<BitMapInfo> bitMapInfos = new List<BitMapInfo>();
            List<WatsEmiSample> verticalSamples; //= angleSamples[0];
            List<WatsEmiSample> horizontalSamples;//= angleSamples[1];

            BitMapInfo bitMapInfo;
            foreach (FrequencyRange range in ranges)
            {
                verticalSamples = new List<WatsEmiSample>();
                horizontalSamples = new List<WatsEmiSample>();

                bitMapInfo = new BitMapInfo();
                bitMapInfo.Band = Utility.ConvertDoubleString(range.FromFreq) + "-"
                    + Utility.ConvertDoubleString(range.EndFreq);
                bitMapInfo.Title1 = "V-" + angle.ToString() + "\x00B0"
                    + "[" + range.FromFreq + "~"
                    + range.EndFreq
                    + " MHz]";
                bitMapInfo.Title2 = "H-" + angle.ToString() + "\x00B0"
                    + "[" + range.FromFreq + "~"
                    + range.EndFreq
                    + " MHz]";
                bitMapInfo.BmpFile1 = Utility.GetAppPath() + "\\Temp\\Angle"
                    + ((int)angle).ToString() + "-vertical-" 
                    + ((int)range.FromFreq).ToString() + ".emf";
                bitMapInfo.BmpFile2 = Utility.GetAppPath() + "\\Temp\\Angle"
                    + ((int)angle).ToString() + "-horizontal-"
                    + ((int)range.FromFreq).ToString() + ".emf";

                foreach (WatsEmiSample sample in angleSamples[0])
                {
                    if (sample.mFreq < range.FromFreq)
                        continue;
                    else if (sample.mFreq > range.EndFreq)
                        break;
                    verticalSamples.Add(sample);
                }
                drawPicture(range, maxShowFreq, channelSettings, displayChannel, verticalSamples,
                    minAbsRssi, maxAbsRssi, bitMapInfo.BmpFile1, bitMapInfo.Title1);

                foreach (WatsEmiSample sample in angleSamples[1])
                {
                    if (sample.mFreq < range.FromFreq)
                        continue;
                    else if (sample.mFreq > range.EndFreq)
                        break;
                    horizontalSamples.Add(sample);
                }
                drawPicture(range, maxShowFreq, channelSettings, displayChannel, horizontalSamples,
                    minAbsRssi, maxAbsRssi, bitMapInfo.BmpFile2, bitMapInfo.Title2);

                bitMapInfos.Add(bitMapInfo);
            }
            return bitMapInfos;
        }

        private static bool drawPicture(FrequencyRange range, double maxShowFreq,
            List<ChannelSetting> channels, bool displayChannel,
            List<WatsEmiSample> samples, int minRssi, int maxRssi, 
            string picturePath, string title)
        {
            try
            {
                double span = range.EndFreq - range.FromFreq;
                double minX = range.FromFreq;

                Bitmap bmp = new Bitmap(418, 180);
                Graphics gs = Graphics.FromImage(bmp);
                /*
                Rectangle rect = new Rectangle(0, 0, 418, 180);
                Metafile mf = new Metafile(channelPictureFile, gs.GetHdc(), rect, MetafileFrameUnit.Pixel);
                */
                Metafile mf = new Metafile(picturePath, gs.GetHdc());

                Graphics g = Graphics.FromImage(mf);
                //Graphics g = Graphics.FromImage(bmp);

                using (Brush bgBrush = new SolidBrush(Color.FromArgb(255, 255, 128)))
                    g.FillRectangle(bgBrush, 0, 0, 418, 180);

                using (Pen outerRectPen = new Pen(Color.Black, 1.0f))
                    g.DrawRectangle(outerRectPen, 52, 15, 350, 130);

                using (Pen gridPen = new Pen(Color.Gray, 0.5f))
                {
                    gridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    for (int j = 0; j < 9; j++)
                        g.DrawLine(gridPen, 52, 15 + (j + 1) * 13, 402, 15 + (j + 1) * 13);

                    for (int j = 0; j < 9; j++)
                        g.DrawLine(gridPen, 52 + (j + 1) * 35, 15, 52 + (j + 1) * 35, 145);
                }

                string text;
                using (Font font = new Font("Times New Roman", 8.0f))
                {
                    text = "Power (dBm)";
                    StringFormat sf = new StringFormat(StringFormatFlags.DirectionVertical);
                    SizeF sizef = g.MeasureString(text, font, Int32.MaxValue, sf);

                    RectangleF rf = new RectangleF(15, 75 - sizef.Height / 2, sizef.Width, sizef.Height);

                    g.TranslateTransform((rf.Left + rf.Right) / 2, (rf.Top + rf.Bottom) / 2);
                    g.RotateTransform(180);

                    RectangleF newRf = new RectangleF(-sizef.Width / 2, -sizef.Height / 2, sizef.Width, sizef.Height);
                    g.DrawString(text, font, Brushes.Black, newRf, sf);
                    g.ResetTransform();
                }

                using (Font font = new Font("Times New Roman", 8.0f))
                {
                    text = "Frequency (MHz)";
                    SizeF sizef = g.MeasureString(text, font, Int32.MaxValue);
                    RectangleF rf = new RectangleF(227 - sizef.Width / 2, 160, sizef.Width, sizef.Height);

                    //g.DrawRectangle(Pens.Black, newRf.Left, newRf.Top, newRf.Width, newRf.Height);
                    g.DrawString(text, font, Brushes.Black, rf);
                }

                using (Font font = new Font("Times New Roman", 5.0f))
                {
                    List<string> xTexts = new List<string>();
                    int j = 0;
                    for (j = 0; j < 10; j++)
                        xTexts.Add(Utility.ConvertDoubleString(minX + span * j / 10));
                    xTexts.Add(Utility.ConvertDoubleString(minX + span));

                    j = 0;
                    SizeF sizef;
                    RectangleF rf;
                    foreach (string xText in xTexts)
                    {
                        sizef = g.MeasureString(xText, font, Int32.MaxValue);
                        rf = new RectangleF(52 + j * 35 - sizef.Width / 2, 147,
                            sizef.Width, sizef.Height);
                        g.DrawString(xText, font, Brushes.Black, rf);
                        j++;
                    }
                }

                using (Font font = new Font("Times New Roman", 5.0f))
                {
                    List<string> yTexts = new List<string>();
                    int j;
                    for (j = 0; j < 11; j++)
                        yTexts.Add("-" + (minRssi + (maxRssi - minRssi) / 10 * j).ToString());

                    SizeF sizef;
                    RectangleF rf;
                    j = 0;
                    foreach (string yText in yTexts)
                    {
                        sizef = g.MeasureString(yText, font, Int32.MaxValue);
                        rf = new RectangleF(50 - sizef.Width, 15 + j * 13 - sizef.Height / 2,
                            sizef.Width, sizef.Height);
                        g.DrawString(yText, font, Brushes.Black, rf);
                        j++;
                    }
                }

                if (displayChannel)
                {
                    float x1, x2;
                    float lastDrawX = (float)minX;
                    foreach (ChannelSetting channelSetting in channels)
                    {
                        if ((double)channelSetting.EndFreq < minX)
                            continue;

                        else if ((double)channelSetting.StartFreq < minX
                            && (double)channelSetting.EndFreq > minX)
                        {
                            using (Pen channelPen = new Pen(Color.Red, 1.0f))
                            {
                                x1 = 52;
                                x2 = (float)(((double)channelSetting.EndFreq - minX) * 350 / span + 52);
                                if (x2 <= 402)
                                {
                                    g.DrawLine(channelPen, x2, 15, x2, 145);
                                    lastDrawX = x2;
                                }
                            }

                            using (Font font = new Font("Times New Roman", 6.0f))
                            {
                                SizeF sizef;
                                RectangleF rf;
                                sizef = g.MeasureString(channelSetting.ChannelName, font, Int32.MaxValue);
                                rf = new RectangleF((x1 + x2 - sizef.Width) / 2, 10 - sizef.Height / 2,
                                    sizef.Width, sizef.Height);
                                g.DrawString(channelSetting.ChannelName, font, Brushes.Red, rf);
                            }
                        }
                        else if ((double)channelSetting.StartFreq >= minX && (double)channelSetting.EndFreq <= minX + span)
                        {
                            using (Pen channelPen = new Pen(Color.Red, 1.0f))
                            {
                                x1 = (float)(((double)channelSetting.StartFreq - minX) * 350 / span + 52);
                                if (!Utility.FloatEquals(x1, lastDrawX))
                                {
                                    g.DrawLine(channelPen, x1, 15, x1, 145);
                                    lastDrawX = x1;
                                }

                                x2 = (float)(((double)channelSetting.EndFreq - minX) * 350 / span + 52);
                                if (x2 <= 402)
                                {
                                    if (!Utility.FloatEquals(x2, lastDrawX))
                                    {
                                        g.DrawLine(channelPen, x2, 15, x2, 145);
                                        lastDrawX = x2;
                                    }
                                }
                            }

                            using (Font font = new Font("Times New Roman", 6.0f))
                            {
                                SizeF sizef;
                                RectangleF rf;
                                sizef = g.MeasureString(channelSetting.ChannelName, font, Int32.MaxValue);
                                rf = new RectangleF((x1 + x2 - sizef.Width) / 2, 10 - sizef.Height / 2,
                                    sizef.Width, sizef.Height);
                                g.DrawString(channelSetting.ChannelName, font, Brushes.Red, rf);
                            }
                        }
                        else if ((double)channelSetting.StartFreq <= minX + span
                            && (double)channelSetting.EndFreq > minX + span)
                        {
                            using (Pen channelPen = new Pen(Color.Red, 1.0f))
                            {
                                x1 = (float)(((double)channelSetting.StartFreq - minX) * 350 / span + 52);
                                if (!Utility.FloatEquals(x1, lastDrawX))
                                {
                                    g.DrawLine(channelPen, x1, 15, x1, 145);
                                    lastDrawX = x1;
                                }

                                x2 = 402;
                            }

                            using (Font font = new Font("Times New Roman", 6.0f))
                            {
                                SizeF sizef;
                                RectangleF rf;
                                sizef = g.MeasureString(channelSetting.ChannelName, font, Int32.MaxValue);
                                rf = new RectangleF((x1 + x2 - sizef.Width) / 2, 10 - sizef.Height / 2,
                                    sizef.Width, sizef.Height);
                                g.DrawString(channelSetting.ChannelName, font, Brushes.Red, rf);
                            }
                        }
                    }

                    foreach (ChannelSetting channelSetting in channels)
                    {
                        if ((double)channelSetting.Pair.EndFreq < minX)
                            continue;
                        else if ((double)channelSetting.Pair.StartFreq < minX
                            && (double)channelSetting.Pair.EndFreq > minX)
                        {
                            using (Pen channelPen = new Pen(Color.Purple, 1.0f))
                            {
                                x1 = 52;
                                x2 = (float)(((double)channelSetting.Pair.EndFreq - minX) * 350 / span + 52);
                                if (x2 <= 402)
                                {
                                    g.DrawLine(channelPen, x2, 15, x2, 145);
                                    lastDrawX = x2;
                                }
                            }

                            using (Font font = new Font("Times New Roman", 6.0f))
                            {
                                SizeF sizef;
                                RectangleF rf;
                                sizef = g.MeasureString(channelSetting.Pair.ChannelName, font, Int32.MaxValue);
                                rf = new RectangleF((x1 + x2 - sizef.Width) / 2, 10 - sizef.Height / 2,
                                    sizef.Width, sizef.Height);
                                g.DrawString(channelSetting.Pair.ChannelName, font, Brushes.Purple, rf);
                            }
                        }
                        else if ((double)channelSetting.Pair.StartFreq >= minX && (double)channelSetting.Pair.EndFreq <= minX + span)
                        {
                            using (Pen channelPen = new Pen(Color.Purple, 1.0f))
                            {
                                x1 = (float)(((double)channelSetting.Pair.StartFreq - minX) * 350 / span + 52);
                                if (!Utility.FloatEquals(x1, lastDrawX))
                                {
                                    g.DrawLine(channelPen, x1, 15, x1, 145);
                                    lastDrawX = x1;
                                }

                                x2 = (float)(((double)channelSetting.Pair.EndFreq - minX) * 350 / span + 52);
                                if (x2 <= 402)
                                {
                                    if (!Utility.FloatEquals(x2, lastDrawX))
                                    {
                                        g.DrawLine(channelPen, x2, 15, x2, 145);
                                        lastDrawX = x2;
                                    }
                                }
                            }

                            using (Font font = new Font("Times New Roman", 6.0f))
                            {
                                SizeF sizef;
                                RectangleF rf;
                                sizef = g.MeasureString(channelSetting.Pair.ChannelName, font, Int32.MaxValue);
                                rf = new RectangleF((x1 + x2 - sizef.Width) / 2, 10 - sizef.Height / 2,
                                    sizef.Width, sizef.Height);
                                g.DrawString(channelSetting.Pair.ChannelName, font, Brushes.Purple, rf);
                            }
                        }
                        else if ((double)channelSetting.Pair.StartFreq <= minX + span && (double)channelSetting.Pair.EndFreq > minX + span)
                        {
                            using (Pen channelPen = new Pen(Color.Purple, 1.0f))
                            {
                                x1 = (float)(((double)channelSetting.Pair.StartFreq - minX) * 350 / span + 52);
                                if (!Utility.FloatEquals(x1, lastDrawX))
                                {
                                    g.DrawLine(channelPen, x1, 15, x1, 145);
                                    lastDrawX = x1;
                                }

                                x2 = 402;
                            }

                            using (Font font = new Font("Times New Roman", 6.0f))
                            {
                                SizeF sizef;
                                RectangleF rf;
                                sizef = g.MeasureString(channelSetting.Pair.ChannelName, font, Int32.MaxValue);
                                rf = new RectangleF((x1 + x2 - sizef.Width) / 2, 10 - sizef.Height / 2,
                                    sizef.Width, sizef.Height);
                                g.DrawString(channelSetting.Pair.ChannelName, font, Brushes.Purple, rf);
                            }
                        }
                    }
                }

                using (Pen dataPen = new Pen(Color.Blue, 1.0f))
                {
                    float x1, x2, y1, y2;
                    for (int j = 0; j < samples.Count - 1; j++)
                    {
                        if (samples[j].mFreq > maxShowFreq)
                            break;

                        x1 = (float)((samples[j].mFreq - minX) * 350 / span + 52);
                        y1 = (float)((Math.Abs(samples[j].mRssi) - minRssi) * 130 / (maxRssi - minRssi) + 15);

                        x2 = (float)((samples[j + 1].mFreq - minX) * 350 / span + 52);
                        y2 = (float)((Math.Abs(samples[j + 1].mRssi) - minRssi) * 130 / (maxRssi - minRssi) + 15);

                        g.DrawLine(dataPen, x1, y1, x2, y2);
                    }
                }

                //bmp.Save(channelPictureFile, ImageFormat.Png);
                g.Save();
                g.Dispose();
                //mf.Dispose();
            }
            catch (System.Exception e)
            {
                File.Delete("c:\\watsLog.txt");
                FileStream fs = new FileStream("c:\\watsLog.txt", FileMode.CreateNew);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(e.Message);
                sw.WriteLine(e.StackTrace);
                sw.Flush();
                sw.Close();
                fs.Close();

                return false;
            }

            return true;
        }

        
    }
}
