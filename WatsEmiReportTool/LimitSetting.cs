using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WatsEmiReportTool
{
    [DataContract]
    public class LimitSetting
    {
        [DataMember]
        public bool UseChannelPowerLimit;
        [DataMember]
        public bool UseDeltaPowerLimit;
        [DataMember]
        public int ChannelPowerLimit;
        [DataMember]
        public int DeltaPowerLimit;
    }
}
