using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace FahrgemeinschaftsProjekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Fahrgemeinschaftsprojekt";
            var loginHandler = new LoginRegistrationHandler(
                "C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv",
                "C:\\Projects001\\FahrgemeinschaftProject\\Members.csv");
            loginHandler.Welcome();
            loginHandler.MenuePage();
            
            Console.ReadLine();
        }
    }
}
