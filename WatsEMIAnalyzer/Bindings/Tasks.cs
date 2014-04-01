using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WatsEMIAnalyzer.Bindings
{
    [DataContract]
    public class Tasks
    {
        [DataMember]
        public List<BindingTask> Task = new List<BindingTask>();
    }
}
