using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEmiReportTool
{
    public class ReportPictureInfo
    {
        public ReportPictureInfo(string fileName, string title)
        {
            FileName = fileName;
            Title = title;
        }

        public string FileName;
        public string Title;
    }
}
