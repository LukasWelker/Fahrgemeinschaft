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
        /// <summary>
        ///  Method to create the CSV-File for users who registrate as a driver
        /// Also creates the file if it does not exist --> creates the file on connection with the first user who signs up as a driver
        /// </summary>
        /// <param name="usersRegistrationNameD"></param>
        /// <param name="usersRegistrationsPasswordD"></param>
        /// <param name="usersRegistrationANameD"></param>
        public static void Drivers(string usersRegistrationNameD, string usersRegistrationsPasswordD, string usersRegistrationANameD) 
        {
            // FileStream fs = new FileStream("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv", FileMode.Create);
            //ist dasselbe, wie in Zeile 18
            var driverscsv = $"{usersRegistrationNameD};{usersRegistrationANameD};{usersRegistrationsPasswordD}\n";
            File.AppendAllText("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv", driverscsv, Encoding.UTF8);
        }
        
    }
}
