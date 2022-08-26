using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using OfficeOpenXml;
using WeatherAPIConsole;
using OfficeOpenXml.Style;


// Ömer Furkan Özsoy Weather API Console Project

namespace WeatherAPI_InternshipProject
{
    class Program
    {
        static public void Main(String[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;  // EEPlus için lisans işlemi

            ExcelPackage ExcelPkg = new ExcelPackage();                                 // Excel Dosya işlemi
            ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");       // Excel Doyası içerisinde Workbook/Worksheet işlemi
            wsSheet1.Column(1).Width = 25;
            wsSheet1.Column(2).Width = 25;
            wsSheet1.Column(3).Width = 16;
            wsSheet1.Column(4).Width = 16;
            wsSheet1.Column(5).Width = 16;
            wsSheet1.Column(6).Width = 16;
            wsSheet1.Column(7).Width = 16;
            wsSheet1.Cells["A0:G0"].Style.Border.Top.Style = ExcelBorderStyle.Thick;
            wsSheet1.Cells["A0:G0"].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
            wsSheet1.Cells["A0:G0"].Style.Border.Left.Style = ExcelBorderStyle.Thick;
            wsSheet1.Cells["A0:G0"].Style.Border.Right.Style = ExcelBorderStyle.Thick;      // Excel Dosyası görüntü özellikleri


            using (ExcelRange Rng = wsSheet1.Cells[1, 1])                                   // Excel Tablo Headerları
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
            using (ExcelRange Rng = wsSheet1.Cells[1, 7])
            {
                Rng.Value = "Date";
                Rng.Style.Font.Bold = true;
                Rng.Style.Font.Italic = true;
            }

            // Excel dosyası oluşturuldu.  Ayrı bir class'da olmama sebebi classda dosyayı kaydedip daha sonra yeni veri yazmaya çalıştığımda eski veriler kayboluyor.



            //TextFileCheckAndCreate.CheckAndCreate(); //Yeni  text dosyası oluşturuldu ve istenen format verildi. !!! Kullanılmıyor
            int counter = 2;                            // Counter döngü içerisinde Exceldeki gerekli hücreye ulaşmak için kullanılıyor
            if (args.Length != 0 && args != null)  //Command Line'da çalıştırılan uygulamaya parametre girildi mi kontrolü.
            {
                foreach (string i in args) // Foreach kullanılarak kullanıcının istediği tüm şehirler için olmak üzere bir döngü oluşturuldu.                                    
                {
                    for (int j = 0; j < 3; j++)  // 3 günlük hava durumu için döngü her bir gün için olmak üzere 3 defa dönüyor.
                    {
                        var ApiResponse = ApiHelper.ApiConnection(j, i); // API Bağlantısı sağlandı ve şehir bilgileri fonskiyonun içine yollandı.
                                                                         // Şehirler için gerekli tarihlerdeki forecast bilgileri çekildi.
                        String apiresult = ApiResponse.Content.ToString(); // API'dan alınan bilgiler sonraki aşamada daha rahat kullanılmak üzere
                                                                           // String formatına döndürüldü.
                        FinalResults NewResult = FinalResults.PrepareResults(apiresult, j); // String formatındaki bilgiler FinalResult Objesindeki gerekli 
                                                                                            // Bilgilere yazıldı
                        //AddRecord.AddRecordText(data);  //Dosya yazımına hazır hale gelen bilgi txt dosyasına yazıldı.
                        using (ExcelRange Rng = wsSheet1.Cells[counter, 1])
                        {
                            Rng.Value = NewResult.Country;
                        }
                        using (ExcelRange Rng = wsSheet1.Cells[counter, 2])
                        {
                            Rng.Value = NewResult.Name;
                        }
                        using (ExcelRange Rng = wsSheet1.Cells[counter, 3])
                        {
                            Rng.Value = NewResult.Maxtemp_c + "C°";
                        }
                        using (ExcelRange Rng = wsSheet1.Cells[counter, 4])
                        {
                            Rng.Value = NewResult.Mintemp_c + "C°";
                        }
                        using (ExcelRange Rng = wsSheet1.Cells[counter, 5])
                        {
                            Rng.Value = NewResult.Sunrise;
                        }
                        using (ExcelRange Rng = wsSheet1.Cells[counter, 6])
                        {
                            Rng.Value = NewResult.Sunset;
                        }
                        using (ExcelRange Rng = wsSheet1.Cells[counter, 7])
                        {
                            Rng.Value = NewResult.Date;
                        }
                        counter++;  // Sayaç Arttırıldı.

                    }
                }

            }
            else
            {
                Console.Write("Command Line'da Şehir girmediğiniz, İstediğiniz Sehirleri Giriniz: ");
                // Eğer kullanıcı istenildiği üzere cmd üzerinden şehirler girmezse input olarak soruluyor.
                // else yapısında geri kalan işlemler input üzerinden olmak üzere if tarafı ile aynı.
                string sehir = Console.ReadLine();
                Array array1 = InputParcala.InputParcalama(sehir); // İnputu kelime kelime parçalayıp array haline getiren metod. 

                
                foreach (string i in array1)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var ApiResponse = ApiHelper.ApiConnection(j, i);
                        
                        String apiresult = ApiResponse.Content.ToString();
                        FinalResults NewResult = FinalResults.PrepareResults(apiresult, j);
                        //AddRecord.AddRecordText(data);
                        //WriteToExcel.WriteIt(NewResult, j);
                        using (ExcelRange Rng = wsSheet1.Cells[counter, 1])
                        {
                            Rng.Value = NewResult.Country;
                        }
                        using (ExcelRange Rng = wsSheet1.Cells[counter, 2])
                        {
                            Rng.Value = NewResult.Name;
                        }
                        using (ExcelRange Rng = wsSheet1.Cells[counter, 3])
                        {
                            Rng.Value = NewResult.Maxtemp_c + "C°";
                        }
                        using (ExcelRange Rng = wsSheet1.Cells[counter, 4])
                        {
                            Rng.Value = NewResult.Mintemp_c + "C°";
                        }
                        using (ExcelRange Rng = wsSheet1.Cells[counter, 5])
                        {
                            Rng.Value = NewResult.Sunrise;
                        }
                        using (ExcelRange Rng = wsSheet1.Cells[counter, 6])
                        {
                            Rng.Value = NewResult.Sunset;
                        }
                        using (ExcelRange Rng = wsSheet1.Cells[counter, 7])
                        {
                            Rng.Value = NewResult.Date;
                        }
                        counter++;  // Sayaç Arttırıldı.
                    }

                }

                  //Command Line komutu çalıştırıldıktan ve program gerekli kodları
                                                                               ////Çalıştırıp sonlanırken kullanıcı için txt dosyasını açan kısım.
            }
            wsSheet1.Protection.IsProtected = false;
            wsSheet1.Protection.AllowSelectLockedCells = false;
            ExcelPkg.SaveAs(new FileInfo(@"results.xlsx"));
            _ = System.Diagnostics.Process.Start(@"results.xlsx");
        }
    }
}
