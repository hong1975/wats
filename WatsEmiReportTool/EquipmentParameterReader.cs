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
    public class EquipmentParameterReader
    {
        private static Dictionary<string, EquipmentParameter> ReadCsvFile(string equipParameterFile)
        {
            Regex regex = new Regex(@"(?<TRSpacing>\d+),"
                + @"(?<SubBand>[^,]+),"
                + @"((?<LoStart>[1-9]\d*(\.\d*)?)" + "|\"(?<LoStart>[\\d,\\.]+)\"),"
                + @"((?<LoStop>[1-9]\d*(\.\d*)?)" + "|\"(?<LoStop>[\\d,\\.]+)\"),"
                + @"((?<HiStart>[1-9]\d*(\.\d*)?)" + "|\"(?<HiStart>[\\d,\\.]+)\"),"
                + @"((?<HiStop>[1-9]\d*(\.\d*)?)" + "|\"(?<HiStop>[\\d,\\.]+)\"),"
                + @"((?<LeftLowBand>[1-9]\d*(\.\d*)?)" + "|\"(?<LeftLowBand>[\\d,\\.]+)\"),"
                + @"((?<RightLowBand>[1-9]\d*(\.\d*)?)" + "|\"(?<RightLowBand>[\\d,\\.]+)\"),"
                + @"((?<LeftHighBand>[1-9]\d*(\.\d*)?)" + "|\"(?<LeftHighBand>[\\d,\\.]+)\"),"
                + @"((?<RightHighBand>[1-9]\d*(\.\d*)?)" + "|\"(?<RightHighBand>[\\d,\\.]+)\")");

            Match match;
            EquipmentParameter equipmentParameter;
            Dictionary<string, EquipmentParameter> parameters = new Dictionary<string, EquipmentParameter>();
            if (!File.Exists(equipParameterFile))
                return parameters;

            try
            {
                using (FileStream fs = new FileStream(equipParameterFile, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(equipParameterFile, Encoding.Default))
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

                            equipmentParameter = new EquipmentParameter();
                            equipmentParameter.TRSpacing = Int32.Parse(match.Groups["TRSpacing"].Value);
                            equipmentParameter.SubBand = match.Groups["SubBand"].Value;
                            equipmentParameter.LoStart = double.Parse(match.Groups["LoStart"].Value);
                            equipmentParameter.LoStop = double.Parse(match.Groups["LoStop"].Value);
                            equipmentParameter.HiStart = double.Parse(match.Groups["HiStart"].Value);
                            equipmentParameter.HiStop = double.Parse(match.Groups["HiStop"].Value);
                            equipmentParameter.LeftLowBand = double.Parse(match.Groups["LeftLowBand"].Value);
                            equipmentParameter.RightLowBand = double.Parse(match.Groups["RightLowBand"].Value);
                            equipmentParameter.LeftHighBand = double.Parse(match.Groups["LeftHighBand"].Value);
                            equipmentParameter.RightHighBand = double.Parse(match.Groups["RightHighBand"].Value);

                            parameters[equipmentParameter.SubBand] = equipmentParameter;
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

            return parameters;
        }

        private static Dictionary<string, EquipmentParameter> ReadXmlFile(string equipParameterFile)
        {
            Dictionary<string, EquipmentParameter> parameters = new Dictionary<string, EquipmentParameter>();

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
                    equipParameterFile, objOpt, false, objOpt, objOpt, objOpt, true,
                    objOpt, objOpt, true, objOpt, objOpt, objOpt, objOpt, objOpt);

                sheets = workBook.Worksheets;
                sheet = (Excel.Worksheet)sheets[1];

                EquipmentParameter equipmentParameter;
                for (int row = 3; ; row++)
                {
                    equipmentParameter = new EquipmentParameter();
                    if (((Range)sheet.Cells[row, 1]).Text == null || ((Range)sheet.Cells[row, 1]).Text.ToString().Length == 0)
                        return parameters;
                    equipmentParameter.TRSpacing = Int32.Parse(((Range)sheet.Cells[row, 1]).Text.ToString().Trim());

                    equipmentParameter.SubBand = ((Range)sheet.Cells[row, 2]).Text.ToString().Trim();
                    equipmentParameter.LoStart = double.Parse(((Range)sheet.Cells[row, 3]).Text.ToString().Trim());
                    equipmentParameter.LoStop = double.Parse(((Range)sheet.Cells[row, 4]).Text.ToString().Trim());
                    equipmentParameter.HiStart = double.Parse(((Range)sheet.Cells[row, 5]).Text.ToString().Trim());
                    equipmentParameter.HiStop = double.Parse(((Range)sheet.Cells[row, 6]).Text.ToString().Trim());
                    equipmentParameter.LeftLowBand = double.Parse(((Range)sheet.Cells[row, 7]).Text.ToString().Trim());
                    equipmentParameter.RightLowBand = double.Parse(((Range)sheet.Cells[row, 8]).Text.ToString().Trim());
                    equipmentParameter.LeftHighBand = double.Parse(((Range)sheet.Cells[row, 9]).Text.ToString().Trim());
                    equipmentParameter.RightHighBand = double.Parse(((Range)sheet.Cells[row, 10]).Text.ToString().Trim());

                    parameters[equipmentParameter.SubBand] = equipmentParameter;
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

        public static Dictionary<string, EquipmentParameter> Read(string equipParameterFile)
        {
            if (equipParameterFile.ToLower().EndsWith(".xls")
                || equipParameterFile.ToLower().EndsWith(".xlsx"))
            {
                return ReadXmlFile(equipParameterFile);
            }
            else if (equipParameterFile.ToLower().EndsWith(".csv"))
            {
                return ReadCsvFile(equipParameterFile);
            }
            else
            {
                return new Dictionary<string, EquipmentParameter>();
            }
        }
    }
}
