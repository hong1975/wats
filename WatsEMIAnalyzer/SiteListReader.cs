using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using WatsEmiReportTool;
using System.Globalization;
using WatsEMIAnalyzer.Bindings;

namespace WatsEMIAnalyzer
{
    public class SiteListReader
    {
        private static Sites ReadCsvFile(string siteListFile)
        {
            Regex regex = new Regex(@"(?<SamID>\S+),"
                + @"(?<SiteID>\S+),"
                + @"(?<SiteName>[^,]+),"
                + @"(?<Type>[^,]+),"
                + @"(?<Longitude>[+-]?\d+(\.\d*)?),"
                + @"(?<Latitude>[+-]?\d+(\.\d*)?),"
                + @"(?<BSC>\S+),"
                + @"(?<RNC>\S+)");

            Match match;
            Site site;
            Sites sites = new Sites();
            if (!File.Exists(siteListFile))
                return sites;

            try
            {
                using (FileStream fs = new FileStream(siteListFile, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(siteListFile, Encoding.Default))
                    {
                        fs.Seek(0, SeekOrigin.Begin);
                        sr.ReadLine();  //skip head line
                        string line = sr.ReadLine();
                        while (line != null)
                        {
                            match = regex.Match(line);
                            if (!match.Success)
                                break;

                            site = new Site();
                            site.SamID = match.Groups["SamID"].Value;
                            site.SiteID = match.Groups["SiteID"].Value;
                            site.SiteName = match.Groups["SiteName"].Value;
                            site.SiteType = match.Groups["Type"].Value;
                            site.Longitude = double.Parse(match.Groups["Longitude"].Value);
                            site.Latitude = double.Parse(match.Groups["Latitude"].Value);
                            site.BSC = match.Groups["BSC"].Value;
                            site.RNC = match.Groups["RNC"].Value;
                            sites.Add(site);

                            line = sr.ReadLine();
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {

            }

            return sites;
        }

        private static Sites ReadXmlFile(string siteListFile)
        {
            Site site;
            Sites sites = new Sites();

            System.Globalization.CultureInfo Oldci = null;
            Excel._Application app = null;
            Excel.WorkbookClass workBook = null;
            Excel.Sheets sheets = null;
            Excel.Worksheet sheet = null;
            try
            {
                Oldci = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");

                app = new Excel.Application();
                object objOpt = System.Reflection.Missing.Value;

                workBook = (Excel.WorkbookClass)app.Workbooks.Open(
                    siteListFile, objOpt, false, objOpt, objOpt, objOpt, true,
                    objOpt, objOpt, true, objOpt, objOpt, objOpt, objOpt, objOpt);

                sheets = workBook.Worksheets;
                sheet = (Excel.Worksheet)sheets[1];

                if (!"Sam ID".Equals(((Range)sheet.Cells[1, 1]).Text.ToString(), StringComparison.OrdinalIgnoreCase)
                    || !"Site ID".Equals(((Range)sheet.Cells[1, 2]).Text.ToString(), StringComparison.OrdinalIgnoreCase)
                    || !"Site Name".Equals(((Range)sheet.Cells[1, 3]).Text.ToString(), StringComparison.OrdinalIgnoreCase)
                    || !"MEGA SITE TYPE".Equals(((Range)sheet.Cells[1, 4]).Text.ToString(), StringComparison.OrdinalIgnoreCase)
                    || !"Longitude".Equals(((Range)sheet.Cells[1, 5]).Text.ToString(), StringComparison.OrdinalIgnoreCase)
                    || !"Latitude".Equals(((Range)sheet.Cells[1, 6]).Text.ToString(), StringComparison.OrdinalIgnoreCase)
                    || !"BSC".Equals(((Range)sheet.Cells[1, 7]).Text.ToString(), StringComparison.OrdinalIgnoreCase)
                    || !"RNC".Equals(((Range)sheet.Cells[1, 8]).Text.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return sites;
                }

                string[] strs = new string[8];
                for (int row = 2; ; row++)
                {
                    for (int i = 0; i < 8; i++)
                        strs[i] = ((Range)sheet.Cells[row, i + 1]).Text.ToString();
                    if (strs[0].Trim().Length == 0 || strs[1].Trim().Length == 0
                        || strs[2].Trim().Length == 0 || strs[3].Trim().Length == 0
                        || strs[6].Trim().Length == 0 || strs[7].Trim().Length == 0
                        || !Regex.IsMatch(strs[4], @"[+-]?[\d]+(\.\d*)?")
                        || !Regex.IsMatch(strs[5], @"[+-]?[\d]+(\.\d*)?"))
                    {
                        return sites;
                    }
                    
                    site = new Site();
                    site.SamID = strs[0];
                    site.SiteID = strs[1];
                    site.SiteName = strs[2];
                    site.SiteType = strs[3];
                    site.Longitude = double.Parse(strs[4]);
                    site.Latitude = double.Parse(strs[5]);
                    site.BSC = strs[6];
                    site.RNC = strs[7];
                    sites.Add(site);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (Oldci != null)
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = Oldci;
                }

                if (app != null)
                    app.Quit();
                ExcelAppKiller.Kill(app);

                WatsEmiReportTool.Utility.ReleaseCom(sheet);
                WatsEmiReportTool.Utility.ReleaseCom(sheets);
                WatsEmiReportTool.Utility.ReleaseCom(workBook);
                WatsEmiReportTool.Utility.ReleaseCom(app);

                GC.Collect(System.GC.GetGeneration(sheet));
                GC.Collect(System.GC.GetGeneration(sheets));
                GC.Collect(System.GC.GetGeneration(workBook));
                GC.Collect(System.GC.GetGeneration(app));

                GC.Collect();
            }
        }

        public static Sites Read(string siteListFile)
        {
            if (siteListFile.ToLower().EndsWith(".xls")
                || siteListFile.ToLower().EndsWith(".xlsx"))
            {
                return ReadXmlFile(siteListFile);
            }
            else if (siteListFile.ToLower().EndsWith(".csv"))
            {
                return ReadCsvFile(siteListFile);
            }
            else
            {
                return new Sites();
            }
        }
    }
}
