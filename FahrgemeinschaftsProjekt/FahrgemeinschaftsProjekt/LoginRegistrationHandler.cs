using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FahrgemeinschaftsProjekt
{
    public class LoginRegistrationHandler
    {
        public static void Welcome()
        {
            //Loginfenster & Registrierungsfenster           
            Console.WriteLine("Willkommen zu unserer Fahrgemeinschaftapp");
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
            //string UserName = "Lukas";
            //string UserPassword = "root";
            while (true)
            {
                Console.WriteLine("Geben Sie nun bitte Ihren Benutzernamen ein");
                string UsersName = Console.ReadLine();
                if (CheckIfUserExistM(UsersName)|| CheckIfUserExistD(UsersName))
                {
                    Console.WriteLine("Hallo Lukas, vielen Dank!");
                    Console.WriteLine("Geben Sie nun bitte Ihr Passwort ein!");
                    string UsersPassword = Console.ReadLine();
                    if (CheckifUserPasswordExistD(UsersPassword) || CheckifUserPasswordExistM(UsersPassword))
                    {
                        Console.Clear();
                        Console.WriteLine("Willkommen zurück!");
                        break;
                    }
                        Console.WriteLine("Dies ist leider ein ungültiges Passwort!");
                }
                else
                {
                    Console.WriteLine("Dies ist leider ein ungültiger Benutzername!");

                } 
            }

        }

        private static bool CheckIfUserExistM(string UsersName)
        {
            string[] readText = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Members.csv", Encoding.UTF8);
            List<string> readList = readText.ToList();
            var filteredmeml = readText.FirstOrDefault(x => x.Split(';').First() == UsersName);
            if (filteredmeml != null)
                return true;
            return false;
        }
       private static bool CheckIfUserExistD(string UsersName)
        {
            string[] readText2 = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv", Encoding.UTF8);
            List<string> readList2 = readText2.ToList();
            var filteredDl = readText2.FirstOrDefault(x => x.Split(';').First() == UsersName);
            if (filteredDl != null)
                return true;
            return false;
        }
        private static bool CheckifUserPasswordExistD(string UsersPassword)
        {
            string[] readText3 = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv", Encoding.UTF8);
            List<string> readList3 = readText3.ToList();
            var filteredPasswordD = readText3.First(x => x.Split(';').Last() == UsersPassword);
            if (filteredPasswordD != null)
                return true;
            return false;
        }
        private static bool CheckifUserPasswordExistM(string UsersPassword)
        {
            string[] readText4 = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Members.csv", Encoding.UTF8);
            List<string> readList4 = readText4.ToList();
            var filteredPasswordD2 = readText4.First(x => x.Split(';').Last() == UsersPassword);
            if (filteredPasswordD2 != null)
                return true;
            return false;
        }

        public  static void RegistrationHandle()
        {
                 
             Console.WriteLine("Sie befinden sich nun im Registrationsfenster!");
            Console.WriteLine(string.Empty);
                Console.WriteLine("Herzlich Willkommen, vielen Dank, dass Sie sich für uns entschieden haben.");
                Console.WriteLine("Um einen Account bei uns zu erstellen, müssen Sie zuerst angeben, ob Sie selbst Fahrer sind oder nur Mitfahrer!");
                string Usersconcern = Console.ReadLine();
                Console.Clear();
            if (Usersconcern == "Mitfahrer")
            {
                Console.WriteLine("Alles klar, das macht die Accounterstellung ganz einfach.");
                Console.WriteLine("Geben Sie nun bitte Ihren vollen Namen ein, der als Ihr Benutzername eingetragen wird.");
                string UsersRegistrationName = Console.ReadLine();
                Console.Clear();
                Console.WriteLine($"Herzlich Willkommen {UsersRegistrationName}");
                Console.WriteLine("Jetzt fehlt nur noch Ihr privates Passwort. Denken Sie daran, nie Ihre Passwörter mit Dritten zu teilen!\nIhr Passwort muss mindestens 6 Zeichen lang sein!");

                while (true)
                {
                    string UsersRegistrationsPassword = Console.ReadLine();
                    if (UsersRegistrationsPassword.Length >= 6)
                    {
                        Member.Members(UsersRegistrationName, UsersRegistrationsPassword);
                        break;
                    }
                    Console.WriteLine("Ihr Passwort ist leider zu kurz. Geben Sie erneut ein gültiges Passwort ein!");

                }
                Console.WriteLine("Vielen Dank, Ihre Registrierung ist nun abgeschlossen.");
            }
            else if (Usersconcern == "Fahrer")
            {
                Console.WriteLine("Vielen Dank, dass Sie sich für unser Anwendung entschieden haben, um Ihre Fahrten anzubieten.");
                Console.WriteLine("Geben Sie nun bitte Ihren vollständigen Namen an");
                string UsersRegistrationNameD = Console.ReadLine();
                Console.Clear();
                Console.WriteLine($"Herzlich Willkommen {UsersRegistrationNameD}");
                Console.WriteLine("Jetzt fehlt nur noch Ihr privates Passwort. Denken Sie daran, nie Ihre Passwörter mit Dritten zu teilen!\nIhr Passwort muss mindestens 6 Zeichen lang sein!");

                while (true)
                {
                    string UsersRegistrationsPasswordD = Console.ReadLine();
                    if (UsersRegistrationsPasswordD.Length >= 6)
                    {
                        Driver.Drivers(UsersRegistrationNameD, UsersRegistrationsPasswordD);
                        break;
                    }
                    Console.WriteLine("Ihr Passwort ist leider zu kurz! Geben Sie nun ein gültiges Passwort ein");
                }
               
                Console.WriteLine("Vielen Dank, Ihre Registrierung ist nun abgeschlossen.");
               
            }
            Console.ReadLine();
        }
    }
}
    

