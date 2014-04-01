using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEmiReportTool
{
    [Serializable]
    public class LinkConfiguration
    {
        public string LinkName;
        public bool IsParallel;
        public string RequiredConfiguration;
    }
}
