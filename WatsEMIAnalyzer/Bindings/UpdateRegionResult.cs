using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WatsEMIAnalyzer.Bindings
{
    [DataContract]
    public class UpdateRegionResult
    {
        [DataMember]
        public string Type;
        [DataMember]
        public int NewVer;
        [DataMember]
        public SubRegion Region;
    }
}
