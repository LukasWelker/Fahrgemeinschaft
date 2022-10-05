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
            int UA2 = 0;
            ConsoleKeyInfo UsersChoice;
            do
            {
                Console.Clear();
                Console.WriteLine("[1] = Logout");
                Console.WriteLine("[2] = Change Username");
                Console.WriteLine("[3] = Change Password");
                UsersChoice = Console.ReadKey();
                if (char.IsDigit(UsersChoice.KeyChar))
                {
                    UA2 = int.Parse(UsersChoice.KeyChar.ToString());
                    break;
                }
            }while (true);
           
            if(UA2 == 1)
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
            else if (UA2 == 2) 
            { 

            }
            else if (UA2 == 3)
            {

            }
        }
    }
}
