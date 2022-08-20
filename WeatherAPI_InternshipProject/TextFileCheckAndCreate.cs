using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI_InternshipProject
{
    class TextFileCheckAndCreate
    {
        public static void CheckAndCreate()
        {
                using (StreamWriter sw = File.CreateText(@"ForecastResults.txt")); // Text Dosyası Oluşturuldu
                String headers = String.Format("{0,-25} {1,-20} {2,-12} {3, -12} {4, -12} {5, -12} {6, -12} \n",
                                               "Country",
                                               "Name",
                                               "Max_Temp_C",
                                               "Min_Temp_C",
                                               "Sunrise",
                                               "Sunset",
                                               "Date");                            // Dosyanın içinde olacak Headerler Formatlandı

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"ForecastResults.txt", true))
                {
                    file.WriteLine(headers);            // Headerler text dosyasına istenen formatta yazıldı.
                }
            
        }
    }
}
