using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WatsEMIAnalyzer.Bindings
{
    [DataContract]
    public class BindingTask
    {
        [DataMember]
        public long ID;

        [DataMember]
        public string Name;

        [DataMember]
        public string Description;

        [DataMember]
        public string RegionID;

        [DataMember]
        public string Creator;

        [DataMember]
        public string CreateTime;

        [DataMember]
        public List<string> Site = new List<string>();

        [DataMember]
        public List<string> UnassignedSite = new List<string>();

        [DataMember]
        public long ChannelSettingID;

        [DataMember]
        public long LinkConfigurationID = -1;

        [DataMember]
        public long EquipmentParameterID = -1;

        [DataMember]
        public List<string> Analyzer = new List<string>();
    }
}
