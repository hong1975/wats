using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WatsClient.Utility
{
    public class SystemHelper
    {
        public static string GetUserHomeDir()
        {
            string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
            if (Environment.OSVersion.Version.Major >= 6)
            {
                path = Directory.GetParent(path).FullName;
            }

            return path;
        }
    }
        
}
