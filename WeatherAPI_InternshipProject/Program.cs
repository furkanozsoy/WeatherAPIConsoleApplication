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

namespace WeatherAPI_InternshipProject
{
    class Program
    {
        static public void Main(String[] args)
        {
            
            TextFileCheckAndCreate.CheckAndCreate(); //Yeni  text dosyası oluşturuldu ve istenen format verildi

            if (args.Length != 0 && args != null)  
            {
                foreach (string i in args) // Foreach kullanılarak kullanıcının istediği tüm şehirler için olmak üzere bir döngü oluşturuldu                                    
                {
                    for (int j = 0; j < 3; j++)  // 3 günlük hava durumu için döngü her bir gün için olmak üzere 3 defa dönüyor
                    {
                        var ApiResponse = ApiHelper.ApiConnection(j, i); // API Bağlantısı sağlandı ve şehir bilgileri fonskiyonun içine yollandı
                                                                         // Şehirler için gerekli tarihlerdeki forecast bilgileri çekildi
                        String apiresult = ApiResponse.Content.ToString(); // API'dan alınan bilgiler sonraki aşamada daha rahat kullanılmak üzere
                                                                           // String formatına döndürüldü
                        String data = FinalResults.PrepareResults(apiresult, j); // String formatındaki bilgiler uygun String formatı düzenine getirildi
                        AddRecord.AddRecordText(data);  //Dosya yazımına hazır hale gelen bilgi txt dosyasına yazıldı.
                    }
                }
                _ = System.Diagnostics.Process.Start(@"ForecastResults.txt");  //Command Line komutu çalıştırıldıktan ve program gerekli kodları 
                                                                               //Çalıştırıp sonlanırken kullanıcı için txt dosyasını açan kısım
            }
            else
            {
                Console.Write("Command Line'da Şehir girmediğiniz, İstediğiniz Sehirleri Giriniz: ");
                // Eğer kullanıcı istenildiği üzere cmd üzerinden şehirler girmezse input olarak soruluyor.
                // else yapısında geri kalan işlemler input üzerinden olmak üzere if tarafı ile aynı.
                string sehir = Console.ReadLine();
                Array array1 = InputParcala.InputParcalama(sehir);
                foreach (string i in array1)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var ApiResponse = ApiHelper.ApiConnection(j, i);

                        String apiresult = ApiResponse.Content.ToString();

                        String data = FinalResults.PrepareResults(apiresult, j);
                        AddRecord.AddRecordText(data);
                    }
                }
                _ = System.Diagnostics.Process.Start(@"ForecastResults.txt");  //Command Line komutu çalıştırıldıktan ve program gerekli kodları 
            }
        }
    }
}
