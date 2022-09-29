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
        public static void Drivers(string usersRegistrationNameD, string usersRegistrationsPasswordD) 
        {
            // FileStream fs = new FileStream("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv", FileMode.Create);
            var foo = $"{usersRegistrationNameD};{usersRegistrationsPasswordD}\n";
            File.AppendAllText("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv", foo, Encoding.UTF8);
           
        }
        
    }
}
