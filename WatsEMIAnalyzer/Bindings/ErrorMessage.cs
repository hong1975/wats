using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WatsEMIAnalyzer.Bindings
{
    [DataContract]
    public class ErrorMessage
    {
        [DataMember]
        public string Name;
        [DataMember]
        public string Description;
    }
}
