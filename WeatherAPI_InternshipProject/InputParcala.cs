using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI_InternshipProject
{
    class InputParcala
    {
        public Array Words { get; set; }
        public static Array InputParcalama(string input)
        {
            try
            {
                string phrase = input;
                string[] Words = phrase.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                return Words;   //Tek bir string değişkeni olarak alınan input kelimeler haline getirilip bir Array'e atandılar.
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Oooops Error inputParcalama", ex);
            }
        }
    }
}
