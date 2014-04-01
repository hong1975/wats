using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEmiReportTool
{
    public class WatsEmiDataManager
    {
        public Dictionary<double, Dictionary<ChannelSetting, WatsEmiData>> AllChannelSamples 
            = new Dictionary<double, Dictionary<ChannelSetting, WatsEmiData>>();

        public Dictionary<double, Dictionary<int, List<WatsEmiSample>>> AllSamples
            = new Dictionary<double,Dictionary<int,List<WatsEmiSample>>>();

    }
}
