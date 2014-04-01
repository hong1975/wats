using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WatsEMIAnalyzer.Bindings
{
    [DataContract]
    public class GlobalRegion
    {
        [DataMember]
        public int Version;
        [DataMember]
        public SubRegion Root;
    }
}
