using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WatsEMIAnalyzer.Bindings
{
    [DataContract]
    public class UpdateTaskRequest
    {
        [DataMember]
        public string Type;
        [DataMember]
        public string NewName;
        [DataMember]
        public List<string> Site = new List<string>();
        [DataMember]
        public List<string> Analyzer = new List<string>();
    }
}
