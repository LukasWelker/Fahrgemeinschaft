using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FahrgemeinschaftsProjekt
{
    public class Driver 
    {
        public static void Drivers(string usersRegistrationNameD, string usersRegistrationsPasswordD, string usersRegistrationANameD) 
        {
            // FileStream fs = new FileStream("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv", FileMode.Create);
            var driverscsv = $"{usersRegistrationNameD};{usersRegistrationANameD};{usersRegistrationsPasswordD}\n";
            File.AppendAllText("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv", driverscsv, Encoding.UTF8);
           
        }
        
    }
}
