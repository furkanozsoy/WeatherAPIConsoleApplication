using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI_InternshipProject;

namespace WeatherAPIConsole
{
    class WriteToExcel
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Maxtemp_c { get; set; }
        public string Mintemp_c { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }

        public string Date { get; set; }


        public static void WriteIt(FinalResults res, int j) //kullanılmıyor
        {
            ExcelPackage ExcelPkg2 = new ExcelPackage();
            ExcelWorksheet wsSheet1 = ExcelPkg2.Workbook.Worksheets.Add("Sheet1");
            using (ExcelRange Rng = wsSheet1.Cells[j+2, 1])
            {
                Rng.Value = res.Country;
            }
            ExcelPkg2.SaveAs(new FileInfo(@"results.xlsx"));


        }
        public static String Number2String(int number, bool isCaps)

        {

            Char c = (Char)((isCaps ? 65 : 97) + (number - 1));

            return c.ToString();

        }
    }
}
