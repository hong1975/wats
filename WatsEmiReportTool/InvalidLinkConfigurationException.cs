using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEmiReportTool
{
    public class InvalidLinkConfigurationException : Exception
    {
        public InvalidLinkConfigurationException(string message)
            : base(message)
        {
        }
    }
}