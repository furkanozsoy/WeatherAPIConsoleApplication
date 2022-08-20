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
        public static Array InputParcalama(string input)
        {
            try
            {
                string phrase = input;
                string[] Words = phrase.Split(' ');
                return Words;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Oooops Error inputParcalama", ex);
            }
        }
    }
}
