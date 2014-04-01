using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEMIAnalyzer.Model;

namespace WatsEMIAnalyzer
{
    public class SiteNodeListSorter
    {
        class NameSorter: IComparer<TreeNode>
        {
            public int Compare(TreeNode x, TreeNode y)
            {
                SiteModel modelX = (SiteModel)x.Tag;
                SiteModel modelY = (SiteModel)x.Tag;

                int compare = modelX.Site.SiteName.CompareTo(modelY.Site.SiteName);
                if (compare == 0)
                    compare = modelX.Site.SiteID.CompareTo(modelY.Site.SiteID);

                return compare;
            }
            
        }

        public static void SortByName(TreeNode parentNode)
        {
            List<TreeNode> siteNodeList = new List<TreeNode>();
            foreach (TreeNode siteNode in parentNode.Nodes)
                siteNodeList.Add(siteNode);

            siteNodeList.Sort(new NameSorter());

            parentNode.Nodes.Clear();
            foreach (TreeNode siteNode in siteNodeList)
                parentNode.Nodes.Add(siteNode);

        }

        
    }
}
