using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WatsEMIAnalyzer.Bindings
{
    [DataContract]
    public class UpdateRegionRequest
    {
        [DataMember]
        public int NewVer;
        [DataMember]
        public string Type;
        [DataMember]
        public SubRegion Region;
        [DataMember]
        public List<string> Site = new List<string>();
        [DataMember]
        public long LinkConfigurationID = -1;
        [DataMember]
        public long EquipmentParameterID = -1;
        [DataMember]
        public long ChannelSettingID = -1;
        [DataMember]
        public string NewRegionName;

        public void addSite(string site)
        {
            Site.Add(site);
        }
    }
}
