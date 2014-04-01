using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatsEMIAnalyzer.Bindings;

namespace WatsEMIAnalyzer.Model
{
    [Serializable]
    public class SiteModel: Model
    {
        private Site mSite;


        public Site Site
        {
            get { return mSite; }
            set { mSite = value; }
        }

        public SiteModel(Site site)
        {
            mSite = site;
        }
    }
}
