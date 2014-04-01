using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEmiReportTool
{
    public class ParserState
    {
        public List<ChannelSetting> channelSettings;
        public bool useChannelPowerLimit;
        public int channelPowerLimit;
        public bool useDeltaPowerLimit;
        public int deltaPowerLimit;
        public int span;
    }
}
