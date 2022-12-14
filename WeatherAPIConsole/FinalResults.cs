using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI_InternshipProject
{
    class FinalResults
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Maxtemp_c { get; set; }
        public string Mintemp_c { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }

        public string Date { get; set; }
        public string Avgtemp { get; set; }
        public string MoonPhase { get; set; }
        public string Icon { get; set; }


        public static FinalResults PrepareResults(string apiresult, int j)
        {
            try
            {
                FinalResults temp = new FinalResults();
                JObject json = JObject.Parse(apiresult);
                temp.Country = json["location"]["country"].ToString();
                temp.Name = json["location"]["name"].ToString();
                temp.Maxtemp_c = json["forecast"]["forecastday"][0]["day"]["maxtemp_c"].ToString();
                temp.Mintemp_c = json["forecast"]["forecastday"][0]["day"]["mintemp_c"].ToString();
                temp.Sunrise = json["forecast"]["forecastday"][0]["astro"]["sunrise"].ToString();
                temp.Sunset = json["forecast"]["forecastday"][0]["astro"]["sunset"].ToString();
                temp.Avgtemp = json["forecast"]["forecastday"][0]["day"]["avgtemp_c"].ToString();
                temp.MoonPhase = json["forecast"]["forecastday"][0]["astro"]["moon_phase"].ToString();
                temp.Icon = json["forecast"]["forecastday"][0]["day"]["condition"]["icon"].ToString();
                DateTime day = DateTime.Now.AddDays(-j);
                temp.Date = day.ToString("yyyy-MM-dd");
                String data = String.Format("{0,-25} {1,-20} {2,-12} {3, -12} {4, -12} {5, -12} {6, -12} \n",
                                             temp.Country, temp.Name, temp.Maxtemp_c, temp.Mintemp_c, temp.Sunrise, temp.Sunset, temp.Date);
                String returnValue = temp.Country +" "+ temp.Name + " " + temp.Maxtemp_c + " " + temp.Mintemp_c + " " + temp.Sunrise + " " + temp.Sunset + " " + temp.Date;
                //return data;
                return temp;
                

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Oooops Error Result Formatting", ex);
            }

        }
    }
}
