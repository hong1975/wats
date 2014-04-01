using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatsEMIAnalyzer.EMI;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace WatsEmiReportTool
{
   
    public class MalaysiaMaxisReportPictureCreator
    {
        public static Dictionary<string, List<BitMapInfo>> create(EMIFileData emi, 
            double azimuth, string relativeAzimuth, WatsEmiDataManager dataMgr,
            Dictionary<string, List<ChannelSetting>> allBandchannels, LimitSetting limitSetting,
            int minAbsRssi, int maxAbsRssi)
        {
            Dictionary<string, List<BitMapInfo>> allChannelBitmapInfos = new Dictionary<string,List<BitMapInfo>>();
            List<BitMapInfo> channelBitmapInfos;
            BitMapInfo loBitMapInfo, hiBitMapInfo;
            List<WatsEmiSample> loVerticalSamples, hiVerticalSamples;
            List<WatsEmiSample> loHorizontalSamples, hiHorizontalSamples;

            string bandName;
            double curFreq;
            int counter = 0;
            List<BitMapInfo> channelBitmaps;
            List<Marker> loVerticalMarkers, loHoizontalMarkers;
            List<Marker> hiVerticalMarkers, hiHoizontalMarkers;
            List<ChannelSetting> channelSettings;
            List<ChannelSetting> pairChannelSettings;
            Marker marker;
            ChannelPower channelPower;
            foreach (KeyValuePair<string, List<ChannelSetting>> pair in allBandchannels)
            {
                bandName = pair.Key;
                channelBitmapInfos = new List<BitMapInfo>();
                channelSettings = pair.Value;
                pairChannelSettings = new List<ChannelSetting>();
                foreach (ChannelSetting channelSetting in channelSettings)
                    pairChannelSettings.Add(channelSetting.Pair);
                
                loVerticalSamples = new List<WatsEmiSample>();
                for (int i = 0; i < dataMgr.AllSamples[azimuth][0].Count; i++)
                {
                    curFreq = dataMgr.AllSamples[azimuth][0][i].mFreq;
                    if (curFreq >= channelSettings[0].StartFreq
                        && curFreq <= channelSettings[channelSettings.Count - 1].EndFreq)
                    {
                        loVerticalSamples.Add(dataMgr.AllSamples[azimuth][0][i]);
                    }
                }

                loHorizontalSamples = new List<WatsEmiSample>();
                for (int i = 0; i < dataMgr.AllSamples[azimuth][1].Count; i++)
                {
                    curFreq = dataMgr.AllSamples[azimuth][1][i].mFreq;
                    if (curFreq >= channelSettings[0].StartFreq
                        && curFreq <= channelSettings[channelSettings.Count - 1].EndFreq)
                    {
                        loHorizontalSamples.Add(dataMgr.AllSamples[azimuth][1][i]);
                    }
                }

                hiVerticalSamples = new List<WatsEmiSample>();
                for (int i = 0; i < dataMgr.AllSamples[azimuth][0].Count; i++)
                {
                    curFreq = dataMgr.AllSamples[azimuth][0][i].mFreq;
                    if (curFreq >= pairChannelSettings[0].StartFreq
                        && curFreq <= pairChannelSettings[pairChannelSettings.Count - 1].EndFreq)
                    {
                        hiVerticalSamples.Add(dataMgr.AllSamples[azimuth][0][i]);
                    }
                }

                hiHorizontalSamples = new List<WatsEmiSample>();
                for (int i = 0; i < dataMgr.AllSamples[azimuth][1].Count; i++)
                {
                    curFreq = dataMgr.AllSamples[azimuth][1][i].mFreq;
                    if (curFreq >= pairChannelSettings[0].StartFreq
                        && curFreq <= pairChannelSettings[pairChannelSettings.Count - 1].EndFreq)
                    {
                        hiHorizontalSamples.Add(dataMgr.AllSamples[azimuth][1][i]);
                    }
                }

                loBitMapInfo = new BitMapInfo();
                loBitMapInfo.Title1 = "Spectrum Analyzer Data MDEF-LAXI_" + bandName + "_" + relativeAzimuth + "V (LO)";
                loBitMapInfo.Title2 = "Spectrum Analyzer Data MDEF-LAXI_" + bandName + "_" + relativeAzimuth + "H (LO)";
                loBitMapInfo.BmpFile1 = Utility.GetAppPath() + "\\Temp\\Site_" + emi.Site_ID + "_Azimuth_"
                    + relativeAzimuth + "_" + bandName + "_LowBand_Vertical_" + (++counter).ToString() + ".emf";
                loBitMapInfo.BmpFile2 = Utility.GetAppPath() + "\\Temp\\Site_" + emi.Site_ID + "_Azimuth_"
                    + relativeAzimuth + "_" + bandName + "_LowBand_Horizontal_" + (counter).ToString() + ".emf";

                hiBitMapInfo = new BitMapInfo();
                hiBitMapInfo.Title1 = "Spectrum Analyzer DataMDEF-LAXI_" + bandName + "_" + relativeAzimuth + "V (HI)";
                hiBitMapInfo.Title2 = "Spectrum Analyzer DataMDEF-LAXI_" + bandName + "_" + relativeAzimuth + "H (HI)";
                hiBitMapInfo.BmpFile1 = Utility.GetAppPath() + "\\Temp\\Site_" + emi.Site_ID + "_Azimuth_"
                    + relativeAzimuth + "_" + bandName + "_HighBand_Vertical_" + (++counter).ToString() + ".emf";
                hiBitMapInfo.BmpFile2 = Utility.GetAppPath() + "\\Temp\\Site_" + emi.Site_ID + "_Azimuth_"
                    + relativeAzimuth + "_" + bandName + "_HighBand_Horizontal_" + (counter).ToString() + ".emf";

                loVerticalMarkers = new List<Marker>();
                loHoizontalMarkers = new List<Marker>();
                hiVerticalMarkers = new List<Marker>();
                hiHoizontalMarkers = new List<Marker>();
                foreach (ChannelSetting channelSetting in channelSettings)
                {
                    channelPower = new ChannelPower(emi.SA_RBW, channelSetting, limitSetting, dataMgr.AllChannelSamples[azimuth][channelSetting]);
                    if (!channelPower.IsValidVPower)
                    {
                        marker = Utility.CreateMarker(channelSetting.ChannelName, dataMgr.AllChannelSamples[azimuth][channelSetting].mVSamples);
                        loVerticalMarkers.Add(marker);
                    }

                    if (!channelPower.IsValidVPairPower)
                    {
                        marker = Utility.CreateMarker(channelSetting.Pair.ChannelName, dataMgr.AllChannelSamples[azimuth][channelSetting].mVPairSamples);
                        hiVerticalMarkers.Add(marker);
                    }

                    if (!channelPower.IsValidHPower)
                    {
                        marker = Utility.CreateMarker(channelSetting.ChannelName, dataMgr.AllChannelSamples[azimuth][channelSetting].mHSamples);
                        loHoizontalMarkers.Add(marker);
                    }

                    if (!channelPower.IsValidHPairPower)
                    {
                        marker = Utility.CreateMarker(channelSetting.Pair.ChannelName, dataMgr.AllChannelSamples[azimuth][channelSetting].mHPairSamples);
                        hiHoizontalMarkers.Add(marker);
                    }
                }

                drawPicture(emi, loVerticalSamples, channelSettings, limitSetting, loBitMapInfo.BmpFile1, loBitMapInfo.Title1, minAbsRssi, maxAbsRssi, loVerticalMarkers);
                drawPicture(emi, loHorizontalSamples, channelSettings, limitSetting, loBitMapInfo.BmpFile2, loBitMapInfo.Title2, minAbsRssi, maxAbsRssi, loHoizontalMarkers);

                drawPicture(emi, hiVerticalSamples, pairChannelSettings, limitSetting, hiBitMapInfo.BmpFile1, hiBitMapInfo.Title1, minAbsRssi, maxAbsRssi, hiVerticalMarkers);
                drawPicture(emi, hiHorizontalSamples, pairChannelSettings, limitSetting, hiBitMapInfo.BmpFile2, hiBitMapInfo.Title2, minAbsRssi, maxAbsRssi, hiHoizontalMarkers);

                channelBitmaps = new List<BitMapInfo>();
                channelBitmaps.Add(loBitMapInfo);
                channelBitmaps.Add(hiBitMapInfo);

                allChannelBitmapInfos[bandName] = channelBitmaps;
            }

            return allChannelBitmapInfos;
        }

        public static bool drawPicture(EMIFileData emi, List<WatsEmiSample> samples,
            List<ChannelSetting> channels, LimitSetting limitSetting,
            string picturePath, string title, int minAbsRssi, int maxAbsRssi, List<Marker> markers)
        {
            try
            {
                int height = 180 + (markers.Count + 1) * 15 + 6 * 15;
                Bitmap bmp = new Bitmap(425, height);
                Graphics gs = Graphics.FromImage(bmp);
                Metafile mf = new Metafile(picturePath, gs.GetHdc());
                Graphics g = Graphics.FromImage(mf);

                double minX = channels[0].StartFreq;
                double span = channels[channels.Count - 1].EndFreq - channels[0].StartFreq;

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
                    g.DrawLine(boldSeparateLinePen, 0, 180 + (markers.Count + 1) * 15, 425, 180 + (markers.Count + 1) * 15);
                }

                using (Pen thinPen = new Pen(Color.Black, 1.0f))
                {
                    using (Font font = new Font("Times New Roman", 7.0f))
                    {
                        //thin horizontal marking separate line
                        for (int j = 0; j < markers.Count; j++)
                            g.DrawLine(thinPen, 0, 180 + (j + 1) * 15, 425, 180 + (j + 1) * 15);

                        //thin vertical marking separate line
                        g.DrawLine(thinPen, 65, 180, 65, 180 + (markers.Count + 1) * 15);
                        g.DrawLine(thinPen, 65 + 120, 180, 65 + 120, 180 + (markers.Count + 1) * 15);
                        g.DrawLine(thinPen, 65 + 240, 180, 65 + 240, 180 + (markers.Count + 1) * 15);

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

                            text = marker.rssi.ToString();
                            sizef = g.MeasureString(text, font, Int32.MaxValue);
                            rf = new RectangleF(185 + 5, 180 + (i + 1) * 15 + 3.5f,
                                sizef.Width, sizef.Height);
                            g.DrawString(text, font, Brushes.Black, rf);

                            text = marker.channelName;
                            sizef = g.MeasureString(text, font, Int32.MaxValue);
                            rf = new RectangleF(305 + 5, 180 + (i + 1) * 15 + 3.5f,
                                sizef.Width, sizef.Height);
                            g.DrawString(text, font, Brushes.Black, rf);

                            MalaysiaReportPictureCreator.drawMarker(g, minX, span, minAbsRssi, maxAbsRssi, marker);

                            i++;
                        }

                        //thin horizontal measurement separate line
                        for (int j = 0; j < 5; j++)
                            g.DrawLine(thinPen, 0, 180 + (markers.Count + 1) * 15 + (j + 1) * 15, 425, 180 + (markers.Count + 1) * 15 + (j + 1) * 15);

                        //thin vertical measurement separate line
                        g.DrawLine(thinPen, 90, 180 + (markers.Count + 1) * 15 + 15,
                            90, 180 + (markers.Count + 1) * 15 + 90);
                        g.DrawLine(thinPen, 90 + 112, 180 + (markers.Count + 1) * 15 + 15,
                            90 + 112, 180 + (markers.Count + 1) * 15 + 90);
                        g.DrawLine(thinPen, 90 + 112 + 90, 180 + (markers.Count + 1) * 15 + 15,
                            90 + 112 + 90, 180 + (markers.Count + 1) * 15 + 90);

                        //drawing measurement
                        text = "Measurement Parameter";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(209 - sizef.Width / 2, 180 + (markers.Count + 1) * 15.0f + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        /************************************************************************/
                        /* Center Frequency                                                     */
                        /************************************************************************/
                        text = "Center Frequency";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(5, 180 + (markers.Count + 1) * 15 + 15 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        text = Utility.ConvertDoubleString((channels[0].StartFreq + channels[channels.Count - 1].EndFreq) / 2000) + " GHz";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(90 + 5, 180 + (markers.Count + 1) * 15 + 15 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        /************************************************************************/
                        /* Span                                                                 */
                        /************************************************************************/
                        text = "Span";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(202 + 5, 180 + (markers.Count + 1) * 15 + 15 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        text = Utility.ConvertDoubleString(channels[channels.Count - 1].EndFreq - channels[0].StartFreq) + " MHz";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(292 + 5, 180 + (markers.Count + 1) * 15 + 15 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        /************************************************************************/
                        /* Start Frequency                                                     */
                        /************************************************************************/
                        text = "Start Frequency";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(5, 180 + (markers.Count + 1) * 15 + 30 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        text = Utility.ConvertDoubleString(channels[0].StartFreq / 1000) + " GHz";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(90 + 5, 180 + (markers.Count + 1) * 15 + 30 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        /************************************************************************/
                        /* Stop Frequency                                                                 */
                        /************************************************************************/
                        text = "Stop Frequency";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(202 + 5, 180 + (markers.Count + 1) * 15 + 30 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        text = Utility.ConvertDoubleString(channels[channels.Count - 1].EndFreq / 1000) + " GHz";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(292 + 5, 180 + (markers.Count + 1) * 15 + 30 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        /************************************************************************/
                        /* Reference Level                                                      */
                        /************************************************************************/
                        text = "Reference Level";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(5, 180 + (markers.Count + 1) * 15 + 45 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        text = Utility.ConvertDoubleString(emi.SA_REF_LEVEL) + " dBm";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(90 + 5, 180 + (markers.Count + 1) * 15 + 45 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        /************************************************************************/
                        /* Attenuation                                                          */
                        /************************************************************************/
                        text = "Attenuation";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(202 + 5, 180 + (markers.Count + 1) * 15 + 45 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        text = emi.SA_Attenuation_Value.ToString() + " dB";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(292 + 5, 180 + (markers.Count + 1) * 15 + 45 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        /************************************************************************/
                        /* RBW                                                                  */
                        /************************************************************************/
                        text = "RBW";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(5, 180 + (markers.Count + 1) * 15 + 60 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        text = Utility.ConvertDoubleString(emi.SA_RBW) + " Hz";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(90 + 5, 180 + (markers.Count + 1) * 15 + 60 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        /************************************************************************/
                        /* VBW                                                          */
                        /************************************************************************/
                        text = "VBW";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(202 + 5, 180 + (markers.Count + 1) * 15 + 60 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        text = Utility.ConvertDoubleString(emi.SA_VBW) + " Hz";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(292 + 5, 180 + (markers.Count + 1) * 15 + 60 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        /************************************************************************/
                        /* Detection                                                            */
                        /************************************************************************/
                        text = "Detection";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(5, 180 + (markers.Count + 1) * 15 + 73 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        text = emi.SA_Detector;
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(90 + 5, 180 + (markers.Count + 1) * 15 + 73 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        /************************************************************************/
                        /* Mode                                                                 */
                        /************************************************************************/
                        text = "Mode";
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(202 + 5, 180 + (markers.Count + 1) * 15 + 73 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);

                        text = emi.SA_Sweep_Mode;
                        sizef = g.MeasureString(text, font, Int32.MaxValue);
                        rf = new RectangleF(292 + 5, 180 + (markers.Count + 1) * 15 + 73 + 3.5f,
                            sizef.Width, sizef.Height);
                        g.DrawString(text, font, Brushes.Black, rf);
                    }
                }

                g.Save();
                gs.Dispose();
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
    }
}
