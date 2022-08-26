using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
namespace WeatherAPI_InternshipProject
{
    class ApiHelper
    {
        public static RestSharp.RestResponse ApiConnection(int j, string i)
        {
            try
            {
                DateTime dateAndTime = DateTime.Now.AddDays(-j);
                String Today = dateAndTime.ToString("yyyy-MM-dd");
                String Api = ("309513621ff6401090493155222408");
                String url = "http://api.weatherapi.com/v1/history.json?key=" + Api + "&q=" + i + "&dt=" + Today;
                var client = new RestClient(url);
                var request = new RestRequest();
                var response = client.Get(request);
                return response;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Oooops Error API Connection", ex);
            }
        }
    }
}
