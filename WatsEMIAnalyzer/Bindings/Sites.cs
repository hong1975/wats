using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WatsEMIAnalyzer.Bindings
{
    [DataContract]
    public class Sites
    {
        [DataMember]
        public List<Site> Site;

        public Sites()
        {
            Site = new List<Site>();
        }

        public void Add(Site site)
        {
            Site.Add(site);
        }
    }
}
