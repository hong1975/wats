using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WatsEMIAnalyzer.Bindings
{
    [DataContract]
    public class Site
    {
        [DataMember]
        public string SamID;

        [DataMember]
        public string SiteID;

        [DataMember]
        public string SiteName;

        [DataMember]
        public string SiteType;

        [DataMember]
        public double Longitude;

        [DataMember]
        public double Latitude;

        [DataMember]
        public string BSC;

        [DataMember]
        public string RNC;

        public override string ToString()
        {
            return "(" + SiteID + ") - " + SiteName;
        }

        public Site() {}

        public Site(string siteID)
        {
            SiteID = siteID;
        }
    }
}
