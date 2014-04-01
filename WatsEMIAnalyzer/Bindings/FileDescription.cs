using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WatsEMIAnalyzer.Bindings
{
    [DataContract]
    public class FileDescription
    {
        [DataMember]
        public long ID;
        [DataMember]
        public string Title;
        [DataMember]
        public string FileName;
        [DataMember]
        public string Uploader;
        [DataMember]
        public string CreateTime;
        [DataMember]
        public string SiteID;
        [DataMember]
        public string Tester;
        [DataMember]
        public string TestTime;

        public override string ToString()
        {
            return Title;
        }
    }
}
