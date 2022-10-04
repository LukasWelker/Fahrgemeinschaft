using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FahrgemeinschaftsProjekt
{
    public class Settings 
    {
       
        public void SettingsHandler(string driverFile, string memberFile)
        {
            Console.Clear();
            Console.WriteLine("[1] = Logout");
            Console.WriteLine("[2] = Change Username");
            Console.WriteLine("[3] = Change Password");
            int UsersChoice = Convert.ToInt32(Console.ReadLine());
            if(UsersChoice == 1)
            {
                Console.Clear();
                Console.WriteLine("Wollen Sie sich wirklich ausloggen wenn ja, drücken sie nun ENTER!");
                    string OutLogCheck = Console.ReadLine();
                
                if(OutLogCheck == string.Empty)
                {
                    Console.Clear();
                    var ReturnLogIN = new LoginRegistrationHandler(driverFile,memberFile);
                    ReturnLogIN.Welcome();
                    ReturnLogIN.MenuePage();
                }
            }
            else if (UsersChoice == 2) 
            { 

            }
        }
    }
}
