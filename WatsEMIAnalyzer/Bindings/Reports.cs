using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WatsEMIAnalyzer.Bindings
{
    [DataContract]
    public class Reports
    {
        [DataMember]
        public List<Report> Report = new List<Report>();
    }
}
