using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEMIAnalyzer.Model
{
    [Serializable]
    public class SiteRegionModel:Model
    {
        private string mName;
        private List<SiteRegionModel> mSubRegions = new List<SiteRegionModel>();
        private List<SiteModel> mSites = new List<SiteModel>();

        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public List<SiteRegionModel> SubRegions
        {
            get { return mSubRegions; }
        }

        public List<SiteModel> Sites
        {
            get { return mSites; }
        }

        public SiteRegionModel(string name)
        {
            mName = name;
        }

        public SiteRegionModel AddSiteRegion(SiteRegionModel siteRegion)
        {
            mSubRegions.Add(siteRegion);
            return siteRegion;
        }

        public SiteRegionModel RemoveSiteRegion(SiteRegionModel siteRegion)
        {
            mSubRegions.Remove(siteRegion);
            return siteRegion;
        }

        public SiteModel AddSite(SiteModel site)
        {
            mSites.Add(site);
            return site;
        }

        public SiteModel RemoveSite(SiteModel site)
        {
            mSites.Remove(site);
            return site;
        }
    }
}
