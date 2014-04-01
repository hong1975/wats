using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using WatsEmiReportTool;

namespace WatsEMIAnalyzer.Bindings
{
    [DataContract]
    public class Report
    {
        public override string ToString()
        {
            return "Report-" 
                + "Task<" + DataCenter.Instance().Tasks[TaskID].Name + ">, "
                + Analyzer + " reported at " + ReportTime;
        }

        [DataMember]
        public long ID;
        [DataMember]
        public string ReportTime;
        [DataMember]
        public string Analyzer;
        [DataMember]
        public bool IsPairReport;
        [DataMember]
        public long TaskID;
        [DataMember]
        public long EmiFileID;
        [DataMember]
        public long ChannelSettingID;
        [DataMember]
        public LimitSetting LimitSetting;
        [DataMember]
        public double Span;
        [DataMember]
        public double StartFreq;
        [DataMember]
        public double EndFreq;
        [DataMember]
        public bool IsChannelPreferred;
        [DataMember]
        public bool IsDisplayChannel;
    }
}
