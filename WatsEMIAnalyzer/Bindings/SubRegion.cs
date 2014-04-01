using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WatsEMIAnalyzer.Bindings
{
    [DataContract]
    public class SubRegion
    {
        public SubRegion Parent;
        [DataMember]
        public int Version;
        [DataMember]
        public string Owner = "";
        [DataMember]
        public string ParentID ="";
        [DataMember]
        public string ID = "";
        [DataMember] 
        public string Name ="";
        [DataMember]
        public long ChannelSettingID = -1;
        [DataMember]
        public long LinkConfigurationID = -1;
        [DataMember]
        public long EquipmentParameterID = -1;
        [DataMember]
        public List<string> Site = new List<string>();
        [DataMember]
        public List<string> Manager = new List<string>();
        [DataMember]
        public List<SubRegion> Sub = new List<SubRegion>();
        [DataMember]
        public List<long> Task = new List<long>();
        [DataMember]
        public bool ValidChannelSetting;
        [DataMember]
        public bool ValidLinkConfiguration;
        [DataMember]
        public bool ValidEquipmentParameter;
    }
}
