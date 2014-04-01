using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WatsEMIAnalyzer.Bindings
{
    [DataContract]
    public class FileList
    {
        [DataMember]
        public List<FileDescription> Description = new List<FileDescription>();
    }
}
