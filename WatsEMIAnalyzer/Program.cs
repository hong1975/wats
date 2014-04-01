using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Utils;

namespace WatsEMIAnalyzer
{
    static class Program
    {
        static IniFile mIniFile;

        public static IniFile Config
        {
            get { return mIniFile; }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            mIniFile = new IniFile(Constants.INI_FILE);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LogInForm());
        }
    }
}
