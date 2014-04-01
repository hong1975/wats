using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEmiReportTool
{
    public class InvalidChannelSettingException : Exception
    {
        public InvalidChannelSettingException(string message) 
            : base(message)
        {
        }
    }
}
