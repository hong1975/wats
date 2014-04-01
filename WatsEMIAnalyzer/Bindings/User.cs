using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WatsEMIAnalyzer.Bindings
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string userId;
        [DataMember]
        public string ha1;
        [DataMember]
        public string role;
        [DataMember]
        public bool locked;

        public override string ToString()
        {
            return userId;
        }
    }
}
