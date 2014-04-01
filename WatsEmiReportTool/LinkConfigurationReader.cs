using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Globalization;
using Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using System.IO;

namespace WatsEmiReportTool
{
    public class LinkConfigurationReader
    {
        private static List<LinkConfiguration> ReadCsvFile(string linkConfigurationFile)
        {
            Regex regex = new Regex(@"(?<LinkName>[^,]+),"
                + @"(?<IsParallel>yes|no),"
                + @"(?<RequiredConfiguration>[^,]+)", RegexOptions.IgnoreCase);

            Match match;
            LinkConfiguration linkConfiguration;
            List<LinkConfiguration> configurations = new List<LinkConfiguration>();
            if (!File.Exists(linkConfigurationFile))
                return configurations;

            try
            {
                string isParallel;
                using (FileStream fs = new FileStream(linkConfigurationFile, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(linkConfigurationFile, Encoding.Default))
                    {
                        fs.Seek(0, SeekOrigin.Begin);
                        string line = sr.ReadLine();
                        while (line != null)
                        {
                            match = regex.Match(line);
                            if (!match.Success)
                            {
                                line = sr.ReadLine();
                                continue;
                            }

                            linkConfiguration = new LinkConfiguration();
                            linkConfiguration.LinkName = match.Groups["LinkName"].Value;
                            isParallel = match.Groups["IsParallel"].Value;
                            if (isParallel.ToLower().Equals("yes"))
                                linkConfiguration.IsParallel = true;
                            else if (isParallel.ToLower().Equals("no"))
                                linkConfiguration.IsParallel = false;
                            else
                            {
                                throw new InvalidLinkConfigurationException(
                                    "Invalid Link Configuration '" + linkConfiguration.LinkName + "'");
                            }
                            linkConfiguration.RequiredConfiguration = match.Groups["RequiredConfiguration"].Value;
                            configurations.Add(linkConfiguration);
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

            return configurations;
        }

        private static List<LinkConfiguration> ReadXmlFile(string linkConfigurationFile)
        {
            List<LinkConfiguration> configurations = new List<LinkConfiguration>();

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
                    linkConfigurationFile, objOpt, false, objOpt, objOpt, objOpt, true,
                    objOpt, objOpt, true, objOpt, objOpt, objOpt, objOpt, objOpt);

                sheets = workBook.Worksheets;
                sheet = (Excel.Worksheet)sheets[1];

                LinkConfiguration linkConfiguration;
                string isParallel;

                for (int row = 2; ; row++)
                {
                    /************************************************************************/
                    /* Primary Channel                                                      */
                    /************************************************************************/
                    linkConfiguration = new LinkConfiguration();

                    //Link Name
                    if (((Range)sheet.Cells[row, 1]).Text == null 
                        || ((Range)sheet.Cells[row, 1]).Text.ToString().Length == 0)
                        return configurations;
                    linkConfiguration.LinkName = ((Range)sheet.Cells[row, 1]).Text.ToString().Trim();

                    //IsParallel
                    isParallel = ((Range)sheet.Cells[row, 2]).Text.ToString().Trim();
                    if (isParallel.ToLower().Equals("yes"))
                        linkConfiguration.IsParallel = true;
                    else if (isParallel.ToLower().Equals("no"))
                        linkConfiguration.IsParallel = false;
                    else
                    {
                        throw new InvalidLinkConfigurationException(
                            "Invalid Link Configuration '" + linkConfiguration.LinkName +"'");
                    }

                    if (((Range)sheet.Cells[row, 3]).Text == null 
                        || ((Range)sheet.Cells[row, 3]).Text.ToString().Length == 0)
                        return configurations;
                    linkConfiguration.RequiredConfiguration = ((Range)sheet.Cells[row, 3]).Text.ToString().Trim();

                    configurations.Add(linkConfiguration);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return configurations;
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

                Utility.ReleaseCom(sheet);
                Utility.ReleaseCom(sheets);
                Utility.ReleaseCom(workBook);
                Utility.ReleaseCom(app);

                GC.Collect(System.GC.GetGeneration(sheet));
                GC.Collect(System.GC.GetGeneration(sheets));
                GC.Collect(System.GC.GetGeneration(workBook));
                GC.Collect(System.GC.GetGeneration(app));
                GC.Collect();
            }
        }

        public static List<LinkConfiguration> Read(string linkConfigurationFile)
        {
            if (linkConfigurationFile.ToLower().EndsWith(".xls")
                || linkConfigurationFile.ToLower().EndsWith(".xlsx"))
            {
                return ReadXmlFile(linkConfigurationFile);
            }
            else if (linkConfigurationFile.ToLower().EndsWith(".csv"))
            {
                return ReadCsvFile(linkConfigurationFile);
            }
            else
            {
                return new List<LinkConfiguration>();
            }
        }
    }
}
