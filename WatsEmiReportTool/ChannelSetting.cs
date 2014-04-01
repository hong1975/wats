using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEmiReportTool
{
    [Serializable]
    public class ChannelSetting
    {
        public string CellName;
        public ChannelSetting Pair;

        public string ChannelName;
        public double CenterFreq;
        public double BandWidth;
        public double StartFreq;
        public double EndFreq;
        public string ODUSubBand;

        public double VPower;
        public double HPower;
    }
}
