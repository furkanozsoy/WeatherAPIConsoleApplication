using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WeatherAPIConsole
{
    class ExcelCreate
    {
        public static void CreateExcelFile()  // kullanılmıyor işlemler program.cs de yapıldı
        {   
            
            ExcelPackage ExcelPkg = new ExcelPackage();
            ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");
            wsSheet1.Column(1).Width = 25;
            wsSheet1.Column(2).Width = 25;
            wsSheet1.Column(3).Width = 16;
            wsSheet1.Column(4).Width = 16;
            wsSheet1.Column(5).Width = 16;
            wsSheet1.Column(6).Width = 16;
            wsSheet1.Cells["A0:F0"].Style.Border.Top.Style = ExcelBorderStyle.Thick;
            wsSheet1.Cells["A0:F0"].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
            wsSheet1.Cells["A0:F0"].Style.Border.Left.Style = ExcelBorderStyle.Thick;
            wsSheet1.Cells["A0:F0"].Style.Border.Right.Style = ExcelBorderStyle.Thick;


            using (ExcelRange Rng = wsSheet1.Cells[1, 1])
            {
                Rng.Value = "Country";
                Rng.Style.Font.Bold = true;
                Rng.Style.Font.Italic = true;
            }
            using (ExcelRange Rng = wsSheet1.Cells[1, 2])
            {
                Rng.Value = "Name";
                Rng.Style.Font.Bold = true;
                Rng.Style.Font.Italic = true;
            }
            using (ExcelRange Rng = wsSheet1.Cells[1, 3])
            {
                Rng.Value = "Max Temp Celcius";
                Rng.Style.Font.Bold = true;
                Rng.Style.Font.Italic = true;
            }
            using (ExcelRange Rng = wsSheet1.Cells[1, 4])
            {
                Rng.Value = "Min Temp Celcius";
                Rng.Style.Font.Bold = true;
                Rng.Style.Font.Italic = true;
            }
            using (ExcelRange Rng = wsSheet1.Cells[1, 5])
            {
                Rng.Value = "Sunrise Time";
                Rng.Style.Font.Bold = true;
                Rng.Style.Font.Italic = true;
            }
            using (ExcelRange Rng = wsSheet1.Cells[1, 6])
            {
                Rng.Value = "Sunset Time";
                Rng.Style.Font.Bold = true;
                Rng.Style.Font.Italic = true;
            }

            //Öyle bir For olsun ki Excel ile tek sefer uğraşalım ve kaydetmiş olalım 


            wsSheet1.Protection.IsProtected = false;
            wsSheet1.Protection.AllowSelectLockedCells = false;
            ExcelPkg.SaveAs(new FileInfo(@"results.xlsx"));

        }
           // Yazılacak metod ve parametreler önemlidirler.
        }
}
