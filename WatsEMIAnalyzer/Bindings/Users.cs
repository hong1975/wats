using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WatsEMIAnalyzer.Bindings
{
    [DataContract]
    public class Users
    {
        [DataMember]
        public List<User> User = new List<User>();
    }
}
