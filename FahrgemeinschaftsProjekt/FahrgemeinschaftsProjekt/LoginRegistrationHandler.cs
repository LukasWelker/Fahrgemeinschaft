using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace FahrgemeinschaftsProjekt
{
    public class LoginRegistrationHandler
    {
        public static void Welcome()
        {
            //Loginfenster & Registrierungsfenster           
            Console.WriteLine("Willkommen zu unsere Fahrgemeinschaftapp");
            Console.WriteLine("[1] = Login");
            Console.WriteLine("[2] = Registration");
            int UsersAnswer = Convert.ToInt32(Console.ReadLine());
            if (UsersAnswer == 1)
            {
                LoginRegistrationHandler.LoginHandle();
            }
            else if (UsersAnswer == 2)
            {
                LoginRegistrationHandler.RegistrationHandle();
            }
        }
        //Console.Clear();
        public static void LoginHandle()
        {
            Console.Clear();
            Console.WriteLine("Sie befinden sich nun im Loginfenster!");
            Console.WriteLine(string.Empty);
            string UserName = "Lukas";
            //string UserPassword = "root";
                while (true)
                {
                    Console.WriteLine("Geben sie nun bitte ihren Benutzernamen ein");
                    string UsersName = Console.ReadLine();
                    if (UsersName == UserName)
                    {
                        Console.WriteLine("Hallo Lukas, vielen Dank!");
                        Console.WriteLine("Geben Sie nun bitte ihr Passwort ein!");
                        string UsersPassword = Console.ReadLine();
                        if (UsersPassword == "root")
                        {
                        Console.Clear();
                        Console.WriteLine("Willkommen zurück!");
                        //int passwort = (Convert.ToInt32(Console.ReadLine()));
                        break;
                        }
                    
                        else 
                        {
                            Console.WriteLine("Dies ist leider ein ungültiges Passwort!");

                        }
                    }
                    else
                    {
                        Console.WriteLine("Dies ist leider ein ungültiger Benutzername!");
                        
                    }
                }
            
        }
        public  static void RegistrationHandle()
        {
                 
             Console.WriteLine("Sie befinden sich nun im Registrationsfenster!");
            Console.WriteLine(string.Empty);
                Console.WriteLine("Herzlich Willkommen, Vielen Dank, dass Sie sich für uns entschieden haben.");
                Console.WriteLine("Um ein Account bei uns zu erstellen müssen sie zuerst angeben, ob sie selbst Fahrer sind oder nur Mitfaher!");
                string Usersconcern = Console.ReadLine();
                Console.Clear();
            if (Usersconcern == "Mitfahrer")
            {
                Console.WriteLine("Alles klar, das macht die Accounterstellung ganz einfach.");
                Console.WriteLine("Geben sie nun bitte ihren vollen Namen ein, der als ihr Benutzername eingetragen wird.");
                string UsersRegistrationName = Console.ReadLine();
                Console.Clear();
                Console.WriteLine($"Herzlich Willkommen {UsersRegistrationName}");
                Console.WriteLine("Jetzt fehlt nur noch ihr privates Passwort. Denken sie daran nie ihre Passwörter mit dritten zu teilen!\nIhr Passwort muss mindestens 6 Zeichen lang sein!");

                while (true)
                {
                    string UsersRegistrationsPassword = Console.ReadLine();
                    if (UsersRegistrationsPassword.Length >= 6)
                    {
                        Member.Members(UsersRegistrationName, UsersRegistrationsPassword);
                        break;
                    }
                    Console.WriteLine("Ihr Passwort ist leider zu kurz. Geben sie erneut ein gültiges Passwort ein!");

                }
                Console.WriteLine("Vielen Dank ihre Registrierung ist nun abgeschlossen.");
            }
            else if (Usersconcern == "Fahrer")
            {
                Console.WriteLine("Vielen Dank, das sie sich für unser Anwendung entschieden haben, um ihre Fahrten anzubieten.");
                Console.WriteLine("Geben sie nun bitte ihren vollständigen Namen an");
                string UsersRegistrationNameD = Console.ReadLine();
                Console.Clear();
                Console.WriteLine($"Herzlich Willkommen {UsersRegistrationNameD}");
                Console.WriteLine("Jetzt fehlt nur noch ihr privates Passwort. Denken sie daran nie ihre Passwörter mit dritten zu teilen!\nIhr Passwort muss mindestens 6 Zeichen lang sein!");

                while (true)
                {
                    string UsersRegistrationsPasswordD = Console.ReadLine();
                    if (UsersRegistrationsPasswordD.Length >= 6)
                    {
                        Driver.Drivers(UsersRegistrationNameD, UsersRegistrationsPasswordD);
                        break;
                    }
                    Console.WriteLine("Ihr Passwort ist leider zu kurz! Geben sie nun ein gültiges Passwort ein");
                }
               
                Console.WriteLine("Vielen Dank ihre Registrierung ist nun abgeschlossen.");
               
            }
            
            Console.ReadLine();
        }
    }
}
    

