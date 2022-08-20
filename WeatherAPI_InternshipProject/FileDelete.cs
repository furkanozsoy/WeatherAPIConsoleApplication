using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI_InternshipProject
{
    class FileDelete
    {


        public static void FileDeleter()
        {
            try
            {
                string filename = @"ForecastResults.txt";
                if (File.Exists(filename))
                {
                    System.IO.File.Delete(filename);
                }
                else
                {
                    Debug.WriteLine("File does not exist.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
