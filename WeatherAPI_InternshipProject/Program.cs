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
        static void Main(string[] args)
        {
            TextFileCheckAndCreate.CheckAndCreate();
            //Check And Create Classından Fonksiyon çağırılarak TXT varlığı kontrol edildi yoksa oluşturuldu.
            Console.Write("Sehiri Giriniz: ");
            string sehir = Console.ReadLine();
            // Kullanıcıdan Gerekli Şehir inputları alındı
            Array array1 = InputParcala.inputParcalama(sehir);
            // Kullanıcı birden fazla şehir girebilir, bunun için gelen her bir şehri farklı şekilde değerlendirmek üzere input parçalandı

            foreach (string i in array1)
            {
                for (int j = 0; j < 3; j++)
                {
                    //DateTime dateAndTime = DateTime.Now.AddDays(-j);
                    //String Today = dateAndTime.ToString("yyyy-MM-dd");
                    //String Api = ("8f09b934186e4321afc112128220908");
                    //String url = "http://api.weatherapi.com/v1/history.json?key=" + Api + "&q=" + i + "&dt=" + Today;
                    //var client = new RestClient(url);
                    //var request = new RestRequest();
                    //var response = client.Get(request);
                    var ApiResponse = ApiHelper.ApiConnection(j, i);
                    String apiresult = ApiResponse.Content.ToString();

                    FinalResults temp = new FinalResults();
                    JObject json = JObject.Parse(apiresult);
                    temp.Country = json["location"]["country"].ToString();
                    temp.Name = json["location"]["name"].ToString();
                    temp.Maxtemp_c = json["forecast"]["forecastday"][0]["day"]["maxtemp_c"].ToString();
                    temp.Mintemp_c = json["forecast"]["forecastday"][0]["day"]["mintemp_c"].ToString();
                    temp.Sunrise = json["forecast"]["forecastday"][0]["astro"]["sunrise"].ToString();
                    temp.Sunset = json["forecast"]["forecastday"][0]["astro"]["sunset"].ToString();
                    DateTime day = DateTime.Now.AddDays(-j);
                    temp.Date = day.ToString("yyyy-MM-dd");
                    //string outputLine = temp.Country + temp.Name + temp.Maxtemp_c + temp.Mintemp_c + temp.Sunrise + temp.Sunset;
                    String data = String.Format("{0,-25} {1,-20} {2,-12} {3, -12} {4, -12} {5, -12} {6, -12} \n",
                                                 temp.Country, temp.Name, temp.Maxtemp_c, temp.Mintemp_c, temp.Sunrise,temp.Sunset, temp.Date );

                    AddRecord.AddRecordText(data);
                }
            }
            _ = System.Diagnostics.Process.Start(@"ForecastResults.txt");
        }
    }
}
