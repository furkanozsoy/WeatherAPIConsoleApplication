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
using System.Drawing;
using OfficeOpenXml.Drawing;


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
            wsSheet1.Column(8).Width = 16;
            wsSheet1.Column(9).Width = 5.75;
            wsSheet1.Rows[0,999999].Height = 30;

            wsSheet1.Cells["A0:I0"].Style.Border.Top.Style = ExcelBorderStyle.Thick;
            wsSheet1.Cells["A0:I0"].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
            wsSheet1.Cells["A0:I0"].Style.Border.Left.Style = ExcelBorderStyle.Thick;
            wsSheet1.Cells["A0:I0"].Style.Border.Right.Style = ExcelBorderStyle.Thick;      // Excel Dosyası görüntü özellikleri




            //System.IO.FileInfo image = new System.IO.FileInfo("FullMoon.jpg");
            //ExcelPicture excelImage = null;
            //if (image != null)
            //{

            //    //note, image name must be unique if you are using multiple images in same excel
            //    excelImage = wsSheet1.Drawings.AddPicture("image", image);

            //    // In .SetPosition, we are using 8th Column and 8th Row, with 0 Offset 
            //    excelImage.SetPosition(9, 0, 8, 0);


            //    //set size of image, 100= width, 100= height
            //    excelImage.SetSize(40, 40);

            //}





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
            using (ExcelRange Rng = wsSheet1.Cells[1, 8])
            {
                Rng.Value = "3 Day Avg. Temp";
                Rng.Style.Font.Bold = true;
                Rng.Style.Font.Italic = true;
            }
            using (ExcelRange Rng = wsSheet1.Cells[1, 9])
            {
                Rng.Value = "Moon";
                Rng.Style.Font.Bold = true;
                Rng.Style.Font.Italic = true;
            }

            // Ay evreleri isimlendirildi ve değşikenlere koyuldu

            System.IO.FileInfo image_fullmoon = new System.IO.FileInfo("../../pics/FullMoon.jpg");
            System.IO.FileInfo image_waxgib = new System.IO.FileInfo("../../pics/WaxingGibbous.jpg");
            System.IO.FileInfo image_firstq = new System.IO.FileInfo("../../pics/FirstQuarter.jpg");
            System.IO.FileInfo image_waxcres = new System.IO.FileInfo("../../pics/WaxingCrescent.jpg");
            System.IO.FileInfo image_newmoon = new System.IO.FileInfo("../../pics/NewMoon.jpg");
            System.IO.FileInfo image_wancres = new System.IO.FileInfo("../../pics/WaningCrescent.jpg");
            System.IO.FileInfo image_lastq = new System.IO.FileInfo("../../pics/LastQuarter.jpg");
            System.IO.FileInfo image_wangib = new System.IO.FileInfo("../../pics/WaningGibbous.jpg");

            // Excel dosyası oluşturuldu.  Ayrı bir class'da olmama sebebi classda dosyayı kaydedip daha sonra yeni veri yazmaya çalıştığımda eski veriler kayboluyor.



            //TextFileCheckAndCreate.CheckAndCreate(); //Yeni  text dosyası oluşturuldu ve istenen format verildi. !!! Kullanılmıyor
            int counter = 2;                           // Counter döngü içerisinde Exceldeki gerekli hücreye ulaşmak için kullanılıyor
            int ortalamaC = 0;
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
                        ortalamaC = ortalamaC + 1; // ortalama sıcaklık
                        if (ortalamaC == 3)
                        {
                            using (ExcelRange Rng = wsSheet1.Cells[counter - 2, 8, counter, 8])
                            {
                                Rng.Value = NewResult.Avgtemp + "C°";
                            }
                            ortalamaC = 0;
                        }

                        // Ay evresi kısmı şimdilik uzun bir if else yapısı ile kuruldu
                        ExcelPicture excelImage = null;
                        System.IO.FileInfo image = null;

                        if (NewResult.MoonPhase == "First Quarter")
                        {
                            image = image_firstq;
                            excelImage = wsSheet1.Drawings.AddPicture("image" + counter, image);
                            excelImage.SetPosition(counter - 1, 0, 8, 0);
                            excelImage.SetSize(40, 40);
                        }
                        else if (NewResult.MoonPhase == "Full Moon")
                        {
                            image = image_fullmoon;
                            excelImage = wsSheet1.Drawings.AddPicture("image" + counter, image);
                            excelImage.SetPosition(counter - 1, 0, 8, 0);
                            excelImage.SetSize(40, 40);
                        }
                        else if (NewResult.MoonPhase == "Waxing Gibbous")
                        {
                            image = image_waxgib;
                            excelImage = wsSheet1.Drawings.AddPicture("image" + counter, image);
                            excelImage.SetPosition(counter - 1, 0, 8, 0);
                            excelImage.SetSize(40, 40);
                        }
                        else if (NewResult.MoonPhase == "Waxing Crescent")
                        {
                            image = image_waxcres;
                            excelImage = wsSheet1.Drawings.AddPicture("image" + counter, image);
                            excelImage.SetPosition(counter - 1, 0, 8, 0);
                            excelImage.SetSize(40, 40);
                        }
                        else if (NewResult.MoonPhase == "New Moon")
                        {
                            image = image_newmoon;
                            excelImage = wsSheet1.Drawings.AddPicture("image" + counter, image);
                            excelImage.SetPosition(counter - 1, 0, 8, 0);
                            excelImage.SetSize(40, 40);
                        }
                        else if (NewResult.MoonPhase == "Waning Crescent")
                        {
                            image = image_wancres;
                            excelImage = wsSheet1.Drawings.AddPicture("image" + counter, image);
                            excelImage.SetPosition(counter - 1, 0, 8, 0);
                            excelImage.SetSize(40, 40);
                        }
                        else if (NewResult.MoonPhase == "Third Quarter")
                        {
                            image = image_lastq;
                            excelImage = wsSheet1.Drawings.AddPicture("image" + counter, image);
                            excelImage.SetPosition(counter - 1, 0, 8, 0);
                            excelImage.SetSize(40, 40);
                        }
                        else if (NewResult.MoonPhase == "Waning Gibbous")
                        {
                            image = image_wangib;
                            excelImage = wsSheet1.Drawings.AddPicture("image" + counter, image);
                            excelImage.SetPosition(counter - 1, 0, 8, 0);
                            excelImage.SetSize(40, 40);
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
                        ortalamaC = ortalamaC + 1; // ortalama sıcaklık
                        if (ortalamaC == 3)
                        {
                            using (ExcelRange Rng = wsSheet1.Cells[counter - 2, 8, counter, 8])
                            {
                                Rng.Value = NewResult.Avgtemp + "C°";
                            }
                            ortalamaC = 0;
                        }
                        ExcelPicture excelImage = null;
                        System.IO.FileInfo image = null;

                        if (NewResult.MoonPhase == "First Quarter")
                        {
                            image = image_firstq;
                                excelImage = wsSheet1.Drawings.AddPicture("image"+counter, image);
                                excelImage.SetPosition(counter-1, 0, 8, 0);
                                excelImage.SetSize(40, 40);  
                        }
                        else if (NewResult.MoonPhase == "Full Moon")
                        {
                            image = image_fullmoon;
                            excelImage = wsSheet1.Drawings.AddPicture("image" + counter, image);
                            excelImage.SetPosition(counter - 1, 0, 8, 0);
                            excelImage.SetSize(40, 40);
                        }
                        else if (NewResult.MoonPhase == "Waxing Gibbous")
                        {
                            image = image_waxgib;
                            excelImage = wsSheet1.Drawings.AddPicture("image" + counter, image);
                            excelImage.SetPosition(counter - 1, 0, 8, 0);
                            excelImage.SetSize(40, 40);
                        }
                        else if (NewResult.MoonPhase == "Waxing Crescent")
                        {
                            image = image_waxcres;
                            excelImage = wsSheet1.Drawings.AddPicture("image" + counter, image);
                            excelImage.SetPosition(counter - 1, 0, 8, 0);
                            excelImage.SetSize(40, 40);
                        }
                        else if (NewResult.MoonPhase == "New Moon")
                        {
                            image = image_newmoon;
                            excelImage = wsSheet1.Drawings.AddPicture("image" + counter, image);
                            excelImage.SetPosition(counter - 1, 0, 8, 0);
                            excelImage.SetSize(40, 40);
                        }
                        else if (NewResult.MoonPhase == "Waning Crescent")
                        {
                            image = image_wancres;
                            excelImage = wsSheet1.Drawings.AddPicture("image" + counter, image);
                            excelImage.SetPosition(counter - 1, 0, 8, 0);
                            excelImage.SetSize(40, 40);
                        }
                        else if (NewResult.MoonPhase == "Third Quarter")
                        {
                            image = image_lastq;
                            excelImage = wsSheet1.Drawings.AddPicture("image" + counter, image);
                            excelImage.SetPosition(counter - 1, 0, 8, 0);
                            excelImage.SetSize(40, 40);
                        }
                        else if (NewResult.MoonPhase == "Waning Gibbous")
                        {
                            image = image_wangib;
                            excelImage = wsSheet1.Drawings.AddPicture("image" + counter, image);
                            excelImage.SetPosition(counter - 1, 0, 8, 0);
                            excelImage.SetSize(40, 40);
                        }

                        counter++;  // Sayaç Arttırıldı.
                    }
                       
                  }

                }

                  //Command Line komutu çalıştırıldıktan ve program gerekli kodları
                                                                               ////Çalıştırıp sonlanırken kullanıcı için txt dosyasını açan kısım.
            
            wsSheet1.Protection.IsProtected = false;
            wsSheet1.Protection.AllowSelectLockedCells = false;
            ExcelPkg.SaveAs(new FileInfo(@"results.xlsx"));
            _ = System.Diagnostics.Process.Start(@"results.xlsx"); 
        }
    }
}
