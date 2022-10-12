using System;

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
            var menueHandler = new MenueHandler("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv",
                "C:\\Projects001\\FahrgemeinschaftProject\\Members.csv");
            menueHandler.MenuePage();            
            Console.ReadLine();
        }
    }
}
