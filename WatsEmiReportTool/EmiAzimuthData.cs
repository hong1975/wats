using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEmiReportTool
{
    public class EmiAzimuthData
    {
        public double Azimuth;
        public double StartFreq;
        public double EndFreq;
        public List<WatsEmiSample> HorizontalSamples;
        public List<WatsEmiSample> VerticalSamples;
        public Dictionary<ChannelSetting, WatsEmiData> ChannelDatas;
    }
}
