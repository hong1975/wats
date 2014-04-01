using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatsEMIAnalyzer.EMI;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace WatsEmiReportTool
{
    public class VenezuelaReportPictureCreator
    {
        public static List<BitMapInfo> create(EMIFileData emi, double azimuth,
            string title, WatsEmiDataManager dataMgr,
            List<ChannelSetting> channelSettings, LimitSetting limitSetting,
            int minAbsRssi, int maxAbsRssi, double span, List<FrequencyRange> ranges,
            ref string verticalCircleTitle, ref string horizontalCircleTitle,
            ref string verticalCircleBmpFile, ref string horizontalCircleBmpFile)
        {
            List<BitMapInfo> bitMapInfos = new List<BitMapInfo>();
            List<WatsEmiSample> verticalSamples;
            List<WatsEmiSample> horizontalSamples;

            verticalCircleBmpFile = Utility.GetAppPath() + "\\Temp\\" + azimuth.ToString() + "-circle-vertical.emf";
            verticalCircleTitle = "V-" + azimuth.ToString() + "\x00B0["
                + ranges[0].FromFreq + "~" + ranges[ranges.Count - 1].EndFreq + " MHz]";
            horizontalCircleBmpFile = Utility.GetAppPath() + "\\Temp\\" + azimuth.ToString() + "circle-horizontal.emf";
            horizontalCircleTitle = "H-" + azimuth.ToString() + "\x00B0["
                + ranges[0].FromFreq + "~" + ranges[ranges.Count - 1].EndFreq + " MHz]";

            //draw circle picture
            drawCirclePicture(true, verticalCircleBmpFile, azimuth, 30, emi.SA_RBW, dataMgr.AllChannelSamples[azimuth], limitSetting);
            drawCirclePicture(false, horizontalCircleBmpFile, azimuth, 30, emi.SA_RBW, dataMgr.AllChannelSamples[azimuth], limitSetting);

            //calculate all Markers
            List<Marker> allVerticalMarkers = new List<Marker>();
            List<Marker> allHoizontalMarkers = new List<Marker>();
            Marker marker;
            ChannelPower channelPower;
            foreach (ChannelSetting channelSetting in channelSettings)
            {
                channelPower = new ChannelPower(emi.SA_RBW, channelSetting, limitSetting, dataMgr.AllChannelSamples[azimuth][channelSetting]);

                if (!channelPower.IsValidVPower)
                {
                    marker = Utility.CreateMarker(channelSetting.ChannelName, dataMgr.AllChannelSamples[azimuth][channelSetting].mVSamples);
                    allVerticalMarkers.Add(marker);
                }

                if (!channelPower.IsValidVPairPower)
                {
                    marker = Utility.CreateMarker(channelSetting.Pair.ChannelName, dataMgr.AllChannelSamples[azimuth][channelSetting].mVPairSamples);
                    allVerticalMarkers.Add(marker);
                }

                if (!channelPower.IsValidHPower)
                {
                    marker = Utility.CreateMarker(channelSetting.ChannelName, dataMgr.AllChannelSamples[azimuth][channelSetting].mHSamples);
                    allHoizontalMarkers.Add(marker);
                }

                if (!channelPower.IsValidHPairPower)
                {
                    marker = Utility.CreateMarker(channelSetting.Pair.ChannelName, dataMgr.AllChannelSamples[azimuth][channelSetting].mHPairSamples);
                    allHoizontalMarkers.Add(marker);
                }
            }

            allVerticalMarkers.Sort(Utility.SortMarkerByFrequency);
            allHoizontalMarkers.Sort(Utility.SortMarkerByFrequency);

            //draw picture per range
            BitMapInfo bitMapInfo;
            List<Marker> verticalMarkers;
            List<Marker> horizontalMarkers;
            foreach (FrequencyRange range in ranges)
            {
                bitMapInfo = new BitMapInfo();

                verticalSamples = new List<WatsEmiSample>();
                horizontalSamples = new List<WatsEmiSample>();

                verticalMarkers = new List<Marker>();
                horizontalMarkers = new List<Marker>();

                foreach (Marker rangeMarker in allVerticalMarkers)
                {
                    if (rangeMarker.frequency >= range.FromFreq && rangeMarker.frequency <= range.EndFreq)
                        verticalMarkers.Add(rangeMarker);
                }

                foreach (Marker rangeMarker in allHoizontalMarkers)
                {
                    if (rangeMarker.frequency >= range.FromFreq && rangeMarker.frequency <= range.EndFreq)
                        horizontalMarkers.Add(rangeMarker);
                }

                int markerRows = Math.Max(verticalMarkers.Count, horizontalMarkers.Count);

                bitMapInfo.Band = Utility.ConvertDoubleString(range.FromFreq) + "-"
                    + Utility.ConvertDoubleString(range.EndFreq);
                bitMapInfo.Title1 = "V-" + Utility.ConvertDoubleString(azimuth) + "\x00B0"
                    + "[" + Utility.ConvertDoubleString(range.FromFreq) + "~"
                    + range.EndFreq
                    + " MHz]";
                bitMapInfo.Title2 = "H-" + Utility.ConvertDoubleString(azimuth) + "\x00B0"
                    + "[" + Utility.ConvertDoubleString(range.FromFreq) + "~"
                    + range.EndFreq
                    + " MHz]";
                bitMapInfo.BmpFile1 = Utility.GetAppPath() + "\\Temp\\Angle"
                    + azimuth.ToString() + "-vertical-"
                    + ((int)range.FromFreq).ToString() + ".emf";
                bitMapInfo.BmpFile2 = Utility.GetAppPath() + "\\Temp\\Angle"
                    + azimuth.ToString() + "-horizontal-"
                    + ((int)range.FromFreq).ToString() + ".emf";

                for (int i = 0; i < dataMgr.AllSamples[azimuth][0].Count; i++)
                {
                    if (dataMgr.AllSamples[azimuth][0][i].mFreq >= range.FromFreq
                        && dataMgr.AllSamples[azimuth][0][i].mFreq <= range.EndFreq)
                        verticalSamples.Add(dataMgr.AllSamples[azimuth][0][i]);
                }

                for (int i = 0; i < dataMgr.AllSamples[azimuth][1].Count; i++)
                {
                    if (dataMgr.AllSamples[azimuth][1][i].mFreq >= range.FromFreq
                        && dataMgr.AllSamples[azimuth][1][i].mFreq <= range.EndFreq)
                        horizontalSamples.Add(dataMgr.AllSamples[azimuth][1][i]);
                }

                drawPicture(emi, verticalSamples, span, range, limitSetting, bitMapInfo.BmpFile1, title + " V", minAbsRssi, maxAbsRssi, verticalMarkers, markerRows);
                drawPicture(emi, horizontalSamples, span, range, limitSetting, bitMapInfo.BmpFile2, title + " H", minAbsRssi, maxAbsRssi, horizontalMarkers, markerRows);

                bitMapInfos.Add(bitMapInfo);
            }

            return bitMapInfos;
        }

        private static int SortSpectrumByStartFreq(ChannelPowerSpectrum spectrum1, ChannelPowerSpectrum spectrum2)
        {
            return spectrum2.startFreq.CompareTo(spectrum1.startFreq);
        }

        private static bool drawCirclePicture(bool isVertical, string picturePath, 
            double azimuth, double azimuth_step, double sa_rbw,
            Dictionary<ChannelSetting, WatsEmiData> channelSamples,
            LimitSetting limitSetting)
        {
            try
            {
                Bitmap bmp = new Bitmap(300, 260);
                Graphics gs = Graphics.FromImage(bmp);
                Metafile mf = new Metafile(picturePath, gs.GetHdc());
                Graphics g = Graphics.FromImage(mf);
                using (Brush backgroundBrush = new SolidBrush(Color.White))
                {
                    g.FillRectangle(backgroundBrush, 0, 0, 300, 260);
                }

                using (Pen circlePen = new Pen(Color.Blue, 3.0f))
                {
                    g.DrawEllipse(circlePen, 45, 25, 210, 210);
                }

                ChannelPower power;
                ChannelPowerSpectrum spectrum;
                List<ChannelPowerSpectrum> spectrums = new List<ChannelPowerSpectrum>();
                foreach (KeyValuePair<ChannelSetting, WatsEmiData> pair in channelSamples)
                {
                    power = new ChannelPower(sa_rbw, pair.Key, limitSetting, pair.Value);
                    spectrum = new ChannelPowerSpectrum();
                    spectrum.startFreq = pair.Key.StartFreq;
                    spectrum.endFreq = pair.Key.EndFreq;
                    spectrum.isValidPower = (isVertical ? power.IsValidVPower : power.IsValidHPower);
                    spectrums.Add(spectrum);

                    power = new ChannelPower(sa_rbw, pair.Key.Pair, limitSetting, pair.Value);
                    spectrum = new ChannelPowerSpectrum();
                    spectrum.startFreq = pair.Key.Pair.StartFreq;
                    spectrum.endFreq = pair.Key.Pair.EndFreq;
                    spectrum.isValidPower = (isVertical ? power.IsValidVPairPower : power.IsValidHPairPower);
                    spectrums.Add(spectrum);
                }

                spectrums.Sort(SortSpectrumByStartFreq);
                float freqSpan = (float)(spectrums[0].endFreq - spectrums[spectrums.Count - 1].startFreq);
                int length;
                Rectangle rect;
                Color color;
                float drawAzimuth;
                for (int i = 0; i < spectrums.Count; i++)
                {
                    if (azimuth >= 0 && azimuth <= 90)
                        drawAzimuth = (float)azimuth - 90;
                    else if (azimuth >= 90 && azimuth <= 180)
                        drawAzimuth = (float)azimuth - 90;
                    else if (azimuth >= 180 && azimuth <= 270)
                        drawAzimuth =  (float)azimuth - 90;
                    else
                        drawAzimuth = (float)azimuth - 450;
                    Debug.WriteLine(spectrums[i].startFreq + " - " + spectrums[i].endFreq);
                    color = spectrums[i].isValidPower ? Color.Green : Color.Red;
                    using (Brush brush = new SolidBrush(color))
                    {
                        length = 2 * (int)(100 * (spectrums[i].endFreq - spectrums[spectrums.Count - 1].startFreq) / freqSpan);
                        rect = new Rectangle((300 - length) / 2, (260 - length) /2, length, length);
                        g.FillPie(brush, rect, (float)(drawAzimuth - azimuth_step / 2), (float)azimuth_step);
                    }

                    if (i != spectrums.Count - 1)
                    {
                        using (Brush whiteBrush = new SolidBrush(Color.White))
                        {
                            length = 2 * (int)(100 * (spectrums[i].startFreq - spectrums[spectrums.Count - 1].startFreq) / freqSpan);
                            rect = new Rectangle((300 - length) / 2, (260 - length) / 2, length, length);
                            g.FillPie(whiteBrush, rect, (float)(drawAzimuth - azimuth_step / 2), (float)azimuth_step);
                        }

                        using (Pen seperateCirclePen = new Pen(Color.White, 1.0f))
                        {
                            length = 2 * (int)(100 * (spectrums[i].endFreq - spectrums[spectrums.Count - 1].startFreq) / freqSpan);
                            rect = new Rectangle((300 - length) / 2, (260 - length) / 2, length, length);
                            g.DrawArc(seperateCirclePen, rect, (float)(drawAzimuth - azimuth_step / 2), (float)azimuth_step);
                        }
                    }
                }

                using (Pen directionPen = new Pen(Color.Blue, 2.0f))
                {
                    Point[] pt = new Point[]{new Point(50, 50), new Point(50, -50)};
                    GraphicsPath strokePath = new GraphicsPath();
                    strokePath.AddLine(new Point(0, 0), new Point(0, 5));
                    strokePath.AddLine(new Point(0, 5), new Point(3, 0));
                    strokePath.AddLine(new Point(0, 5), new Point(-3, 0));
                    CustomLineCap customLineCap = new CustomLineCap(null, strokePath);
                    customLineCap.SetStrokeCaps(LineCap.Round, LineCap.Round);
                    directionPen.CustomEndCap = customLineCap;

                    Point center = new Point(150, 130);
                    int endX, endY;
                    int deltX = (int)(Math.Abs(Math.Sin(azimuth * Math.PI / 180)) * 120);
                    int deltY = (int)(Math.Abs(Math.Cos(azimuth * Math.PI / 180)) * 120);
                    if (azimuth >= 0 && azimuth <= 90)
                    {
                        endX = 150 + deltX;
                        endY = 130 - deltY;
                    }
                    else if (azimuth >= 90 && azimuth <= 180)
                    {
                        endX = 150 + deltX;
                        endY = 130 + deltY;
                    }
                    else if (azimuth >= 180 && azimuth <= 270)
                    {
                        endX = 150 - deltX;
                        endY = 130 + deltY;
                    }
                    else
                    {
                        endX = 150 - deltX;
                        endY = 130 - deltY;
                    }
                    Point end = new Point(endX, endY);
                    g.DrawLine(directionPen, center, end);

                    using (Font font = new Font("Times New Roman", 10.0f))
                    {
                        string text = azimuth.ToString() + "\x00B0";
                        SizeF sizef = g.MeasureString(text, font, Int32.MaxValue);
                        float textX, textY;
                        if (azimuth >= 0 && azimuth <= 90)
                        {
                            textX = end.X - 1;
                            textY = end.Y - sizef.Height - 10;
                        }
                        else if (azimuth >= 90 && azimuth <= 180)
                        {
                            textX = end.X - 1;
                            textY = end.Y + sizef.Height;
                        }
                        else if (azimuth >= 180 && azimuth <= 270)
                        {
                            textX = end.X - sizef.Width / 2;
                            textY = end.Y + sizef.Height;
                        }
                        else
                        {
                            textX = end.X - sizef.Width / 2;
                            textY = end.Y - sizef.Height - 10;
                        }

                        RectangleF rf = new RectangleF(textX, textY, sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Blue, rf);
                    }
                }
                
                g.Save();
                g.Dispose();
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

        private static bool drawPicture(EMIFileData emi, List<WatsEmiSample> samples,
            double span, FrequencyRange range, LimitSetting limitSetting,
            string picturePath, string title, int minAbsRssi, int maxAbsRssi,
            List<Marker> markers, int markerRows)
        {
            try
            {
                int height = 180 + (markerRows + 1) * 15 + 4 * 15;
                Bitmap bmp = new Bitmap(425, height);
                Graphics gs = Graphics.FromImage(bmp);
                Metafile mf = new Metafile(picturePath, gs.GetHdc());
                Graphics g = Graphics.FromImage(mf);

                double minX = range.FromFreq;
                using (Brush bgBrush = new SolidBrush(Color.FromArgb(255, 255, 128)))
                {
                    g.FillRectangle(bgBrush, 0, 0, 425, height);
                }

                using (Pen boldRectPen = new Pen(Color.Black, 2.0f))
                {
                    g.DrawRectangle(boldRectPen, 0, 0, 425, height);
                }

                using (Pen boldRectPen = new Pen(Color.Black, 1.0f))
                {
                    g.DrawRectangle(boldRectPen, 52, 15, 350, 130);
                }

                using (Pen gridPen = new Pen(Color.Gray, 0.5f))
                {
                    gridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    for (int j = 0; j < 9; j++)
                        g.DrawLine(gridPen, 52, 15 + (j + 1) * 13, 402, 15 + (j + 1) * 13);

                    for (int j = 0; j < 9; j++)
                        g.DrawLine(gridPen, 52 + (j + 1) * 35, 15, 52 + (j + 1) * 35, 145);
                }

                string text;
                SizeF sizef;
                RectangleF rf;

                //drawing title
                using (Font font = new Font("Times New Roman", 8.0f))
                {
                    text = title;
                    sizef = g.MeasureString(text, font, Int32.MaxValue);
                    rf = new RectangleF(227 - sizef.Width / 2, 10 - sizef.Height / 2, sizef.Width, sizef.Height);

                    g.DrawString(text, font, Brushes.Black, rf);
                }

                //drawing side text
                using (Font font = new Font("Times New Roman", 8.0f))
                {
                    text = "Power (dBm)";
                    StringFormat sf = new StringFormat(StringFormatFlags.DirectionVertical);
                    sizef = g.MeasureString(text, font, Int32.MaxValue, sf);
                    rf = new RectangleF(5, 75 - sizef.Height / 2, sizef.Width, sizef.Height);

                    g.TranslateTransform((rf.Left + rf.Right) / 2, (rf.Top + rf.Bottom) / 2);
                    g.RotateTransform(180);

                    RectangleF newRf = new RectangleF(-sizef.Width / 2, -sizef.Height / 2, sizef.Width, sizef.Height);
                    g.DrawString(text, font, Brushes.Black, newRf, sf);
                    g.ResetTransform();
                }

                //drawing bottom
                using (Font font = new Font("Times New Roman", 8.0f))
                {
                    text = "Frequency (MHz)";
                    sizef = g.MeasureString(text, font, Int32.MaxValue);
                    rf = new RectangleF(227 - sizef.Width / 2, 160, sizef.Width, sizef.Height);

                    g.DrawString(text, font, Brushes.Black, rf);
                }

                //drawing x
                using (Font font = new Font("Times New Roman", 5.0f))
                {
                    List<string> xTexts = new List<string>();
                    int j;
                    for (j = 0; j < 11; j++)
                        xTexts.Add(Utility.ConvertDoubleString(minX + span * j / 10));

                    j = 0;
                    foreach (string xText in xTexts)
                    {
                        sizef = g.MeasureString(xText, font, Int32.MaxValue);
                        rf = new RectangleF(52 + j * 35 - sizef.Width / 2, 147,
                            sizef.Width, sizef.Height);
                        g.DrawString(xText, font, Brushes.Black, rf);
                        j++;
                    }
                }

                //drawing y
                using (Font font = new Font("Times New Roman", 5.0f))
                {
                    List<string> yTexts = new List<string>();
                    int j;
                    for (j = 0; j < 11; j++)
                        yTexts.Add("-" + (minAbsRssi + (maxAbsRssi - minAbsRssi) / 10 * j).ToString());

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

                //drawing curve
                using (Pen dataPen = new Pen(Color.Blue, 1.0f))
                {
                    float x1, x2, y1, y2;
                    for (int j = 0; j < samples.Count - 1; j++)
                    {
                        x1 = (float)((samples[j].mFreq - minX) * 350 / span + 52);
                        y1 = (float)((Math.Abs(samples[j].mRssi) - minAbsRssi) * 130 / (maxAbsRssi - minAbsRssi) + 15);

                        x2 = (float)((samples[j + 1].mFreq - minX) * 350 / span + 52);
                        y2 = (float)((Math.Abs(samples[j + 1].mRssi) - minAbsRssi) * 130 / (maxAbsRssi - minAbsRssi) + 15);

                        g.DrawLine(dataPen, x1, y1, x2, y2);
                    }
                }

                //drawing bold separate line
                using (Pen boldSeparateLinePen = new Pen(Color.Black, 2.0f))
                {
                    g.DrawLine(boldSeparateLinePen, 0, 180, 425, 180);
                    g.DrawLine(boldSeparateLinePen, 0, 180 + (markerRows + 1) * 15, 425, 180 + (markerRows + 1) * 15);
                }

                using (Pen thinPen = new Pen(Color.Black, 1.0f))
                {
                    using (Font font = new Font("Times New Roman", 7.0f))
                    {
                        //thin horizontal marking separate line
                        for (int j = 0; j < markerRows; j++)
                            g.DrawLine(thinPen, 0, 180 + (j + 1) * 15, 425, 180 + (j + 1) * 15);

                        //thin vertical marking separate line
                        g.DrawLine(thinPen, 65, 180, 65, 180 + (markerRows + 1) * 15);
                        g.DrawLine(thinPen, 65 + 120, 180, 65 + 120, 180 + (markerRows + 1) * 15);
                        g.DrawLine(thinPen, 65 + 240, 180, 65 + 240, 180 + (markerRows + 1) * 15);

                        //marker titles
                        text = "Marker No.";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(5, 180 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        text = "Frequency (MHz)";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(65 + 5, 180 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        text = "RSSI (dBm)";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(185 + 5, 180 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        text = "Channel";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(305 + 5, 180 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        //marker no.
                        for (int j = 0; j < markers.Count; j++)
                        {
                            text = (j + 1).ToString();
                            sizef = g.MeasureString(text, font, Int32.MaxValue);
                            rf = new RectangleF(5, 180 + (j + 1) * 15 + 3.5f,
                                sizef.Width, sizef.Height);
                            g.DrawString(text, font, Brushes.Black, rf);
                        }

                        //drawing mark
                        int i = 0;
                        foreach (Marker marker in markers)
                        {
                            text = Utility.ConvertDoubleString(marker.frequency);
                            sizef = g.MeasureString(text, font, Int32.MaxValue);
                            rf = new RectangleF(65 + 5, 180 + (i + 1) * 15 + 3.5f,
                                sizef.Width, sizef.Height);
                            g.DrawString(text, font, Brushes.Black, rf);

                            text = Utility.ConvertDoubleString(marker.rssi);
                            sizef = g.MeasureString(text, font, Int32.MaxValue);
                            rf = new RectangleF(185 + 5, 180 + (i + 1) * 15 + 3.5f,
                                sizef.Width, sizef.Height);
                            g.DrawString(text, font, Brushes.Black, rf);

                            text = marker.channelName;
                            sizef = g.MeasureString(text, font, Int32.MaxValue);
                            rf = new RectangleF(305 + 5, 180 + (i + 1) * 15 + 3.5f,
                                sizef.Width, sizef.Height);
                            g.DrawString(text, font, Brushes.Black, rf);

                            drawMarker(g, minX, span, minAbsRssi, maxAbsRssi, marker);

                            i++;
                        }

                        //thin horizontal measurement separate line
                        for (int j = 0; j < 3; j++)
                            g.DrawLine(thinPen, 0, 180 + (markerRows + 1) * 15 + (j + 1) * 15, 425, 180 + (markerRows + 1) * 15 + (j + 1) * 15);

                        //thin vertical measurement separate line
                        g.DrawLine(thinPen, 90, 180 + (markerRows + 1) * 15 + 15,
                            90, 180 + (markerRows + 1) * 15 + 60);
                        g.DrawLine(thinPen, 90 + 112, 180 + (markerRows + 1) * 15 + 15,
                            90 + 112, 180 + (markerRows + 1) * 15 + 60);
                        g.DrawLine(thinPen, 90 + 112 + 90, 180 + (markerRows + 1) * 15 + 15,
                            90 + 112 + 90, 180 + (markerRows + 1) * 15 + 60);

                        //drawing measurement
                        text = "Measurement Parameter";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(209 - sizef.Width / 2, 180 + (markerRows + 1) * 15.0f + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        /************************************************************************/
                        /* Reference Level                                                      */
                        /************************************************************************/
                        text = "Reference Level";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(5, 180 + (markerRows + 1) * 15 + 15 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        text = Utility.ConvertDoubleString(emi.SA_REF_LEVEL) + " dBm";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(90 + 5, 180 + (markerRows + 1) * 15 + 15 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        /************************************************************************/
                        /* Attenuation                                                          */
                        /************************************************************************/
                        text = "Attenuation";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(202 + 5, 180 + (markerRows + 1) * 15 + 15 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        text = emi.SA_Attenuation_Value.ToString() + " dB";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(292 + 5, 180 + (markerRows + 1) * 15 + 15 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        /************************************************************************/
                        /* RBW                                                                  */
                        /************************************************************************/
                        text = "RBW";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(5, 180 + (markerRows + 1) * 15 + 30 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        text = Utility.ConvertDoubleString(emi.SA_RBW) + " Hz";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(90 + 5, 180 + (markerRows + 1) * 15 + 30 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        /************************************************************************/
                        /* VBW                                                          */
                        /************************************************************************/
                        text = "VBW";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(202 + 5, 180 + (markerRows + 1) * 15 + 30 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        text = Utility.ConvertDoubleString(emi.SA_VBW) + " Hz";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(292 + 5, 180 + (markerRows + 1) * 15 + 30 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        /************************************************************************/
                        /* Detection                                                            */
                        /************************************************************************/
                        text = "Detection";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(5, 180 + (markers.Count + 1) * 15 + 43 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        text = emi.SA_Detector;
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(90 + 5, 180 + (markers.Count + 1) * 15 + 43 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        /************************************************************************/
                        /* Mode                                                                 */
                        /************************************************************************/
                        text = "Mode";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(202 + 5, 180 + (markers.Count + 1) * 15 + 43 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        text = emi.SA_Sweep_Mode;
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(292 + 5, 180 + (markers.Count + 1) * 15 + 43 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);
                    }
                }

                g.Save();
                g.Dispose();
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

        private static void drawMarker(Graphics g, double minX, double span, int minAbsRssi, int maxAbsRssi, Marker marker)
        {
            //drawing curve
            using (Pen markerPen = new Pen(Color.Red, 1.0f))
            {
                float xa, xb, xc, ya, yb, yc;
                xa = (float)((marker.frequency - minX) * 350 / span + 52);
                ya = (float)((Math.Abs(marker.rssi) - minAbsRssi) * 130 / (maxAbsRssi - minAbsRssi) + 15);

                xb = xa - 1.5f; yb = ya - 4.0f;
                xc = xa + 1.5f; yc = ya - 4.0f;

                g.DrawLine(markerPen, xa, ya, xb, yb);
                g.DrawLine(markerPen, xa, ya, xc, yc);
                g.DrawLine(markerPen, xb, yb, xc, yc);
            }
        }
    }
}
