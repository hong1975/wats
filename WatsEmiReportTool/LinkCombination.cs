using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEmiReportTool
{
    public class LinkCombination
    {
        public static LinkCombination Clone(LinkCombination linkCombination)
        {
            LinkCombination newLinkCombination = new LinkCombination();
            newLinkCombination.CombinationNo = linkCombination.CombinationNo;
            Link newLink;
            foreach (Link link in linkCombination.Links)
            {
                newLink = Link.Clone(link);
                newLinkCombination.Links.Add(newLink);
            }

            return newLinkCombination;
        }

        public int CombinationNo;
        public List<Link> Links = new List<Link>();
    }
}
