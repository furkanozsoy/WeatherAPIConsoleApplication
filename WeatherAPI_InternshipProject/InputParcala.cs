using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI_InternshipProject
{
    class InputParcala
    {
        public Array Words {get; set;}
        public static Array inputParcalama(string input)
        {
            string phrase = input;
            string[] Words = phrase.Split(' ');
            return Words;
        }
    }
}
