using Newtonsoft.Json;
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

    }
}
