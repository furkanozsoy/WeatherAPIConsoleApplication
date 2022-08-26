using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI_InternshipProject
{
    class AddRecord
    {
        public static void AddRecordText(string data)  // Trxt yazımında kullanılmış olan class (şuan kullanılmıyor)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"ForecastResults.txt", true))
                {
                    file.WriteLine(data);
                }
                // Metin Dosyasına yazma işlemi.
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Oooops File Write Error", ex);
            }
            // Hata kontrolü için Try Catch kullanıldı.
        }
    }
}
