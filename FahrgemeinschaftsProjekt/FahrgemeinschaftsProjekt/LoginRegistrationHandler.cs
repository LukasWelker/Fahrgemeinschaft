using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace FahrgemeinschaftsProjekt
{
    public class LoginRegistrationHandler
    {
        //
        public readonly string driverFile;
        public readonly string memberFile;

        public LoginRegistrationHandler(string driverFile, string memberFile)
        {
            this.driverFile = driverFile;
            this.memberFile = memberFile;

        }

        public void Welcome()
        {
        Home:
            //Loginfenster & Registrierungsfenster           
            Console.WriteLine("Willkommen zu unserer Fahrgemeinschaftapp");
            Console.WriteLine("[1] = Login");
            Console.WriteLine("[2] = Registration");
            Console.WriteLine("[3] = Exit");
            int UsersAnswer = Convert.ToInt32(Console.ReadLine());
            if (UsersAnswer == 1)
            {
                LoginHandle();
            }
            else if (UsersAnswer == 2)
            {

                if (RegistrationHandle())
                {
                    Console.Clear();
                    goto Home;
                }
            }
            else if(UsersAnswer == 3)
            {
                Console.Clear();
                Console.WriteLine("Tschüss!");
                Thread.Sleep(900);
                Environment.Exit(1);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Ungültige Eingabe, bitte versuchen Sie es erneut.");
                Thread.Sleep(900);
                Console.Clear();
                goto Home;
            }

        }
        public string LoginHandle()
        {
            Console.Clear();
            Console.WriteLine("Sie befinden sich nun im Loginfenster!");
            Console.WriteLine(string.Empty);
            while (true)
            {
                Console.WriteLine("Geben Sie nun bitte Ihren Benutzernamen ein");
                string UsersName = Console.ReadLine();
                if (CheckIfUsersNameExistDM(UsersName, memberFile)
                    || CheckIfUsersNameExistDM(UsersName, driverFile))
                {
                    Console.Clear();
                    Console.WriteLine($"Hallo {UsersName}.");
                    Console.WriteLine("Geben Sie nun bitte Ihr Passwort ein!");
                    string UsersPassword = Console.ReadLine();
                    if (CheckifUserPasswordExistDM(UsersPassword, driverFile)
                        || CheckifUserPasswordExistDM(UsersPassword, memberFile))
                    {
                        Console.Clear();
                        Console.WriteLine("Login war erfolgreich!");
                        Thread.Sleep(300);
                        
                        return UsersName;

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Dies ist leider ein ungültiges Passwort/Benutzername!");
                    }

                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Dies ist leider ein ungültiges Passwort/Benutzername!");
                }
            }

        }

        public static bool CheckIfUsersNameExistDM(string UsersName, string path)
        {

            string[] readText = File.ReadAllLines(path, Encoding.UTF8);
            List<string> readList = readText.ToList();
            var filteredmeml = readText.FirstOrDefault(x => x.Split(';').First() == UsersName);
            if (filteredmeml != null)
                return true;
            return false;
        }
        private static bool CheckifUserPasswordExistDM(string UsersPassword, string path)
        {
            string[] readText3 = File.ReadAllLines(path, Encoding.UTF8);
            List<string> readList3 = readText3.ToList();

            var filteredPasswordD = readText3.FirstOrDefault(x => x.Split(';').Last() == UsersPassword);
            if (filteredPasswordD != null)
                return true;
            return false;
        }
        public static void ReadCsv(string path)
        {
            string[] readText = File.ReadAllLines(path, Encoding.UTF8).Skip(0).First().Split(';');
            List<string> readList = readText.ToList();
            foreach (string read in readList)
            {
                Console.WriteLine(read);
            }
        }

        public bool RegistrationHandle()
        {


            Console.Clear();
            Console.WriteLine("Sie befinden sich nun im Registrationsfenster!");
            Console.WriteLine(string.Empty);
            Console.WriteLine("Herzlich Willkommen, vielen Dank, dass Sie sich für uns entschieden haben.");
            Console.WriteLine("Um einen Account bei uns zu erstellen, müssen Sie zuerst angeben, ob Sie selbst Fahrer sind oder nur Mitfahrer!");
            string Usersconcern = Console.ReadLine();
            if (string.IsNullOrEmpty(Usersconcern))
            {
                Console.Clear();
                Console.WriteLine("Dies ist leider eine ungültige Eingabe bitte erneut versuchen!");
                Thread.Sleep(500);
                RegistrationHandle();
            }
            Console.Clear();
            if (Usersconcern == "Mitfahrer")
            {
                Mitfahrer:
                Console.WriteLine("Sie befinden sich im Registrierungsfenster als Mitfahrer!");
                Console.WriteLine(string.Empty);
                Console.WriteLine("Geben Sie nun bitte Ihren vollen Namen ein, der als Ihr Benutzername eingetragen wird.");
                string UsersRegistrationName = Console.ReadLine();
                if (string.IsNullOrEmpty(UsersRegistrationName))
                {
                    Console.Clear();
                    Console.WriteLine("Dies ist leider eine ungültige Eingabe, bitte erneut versuchen.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    goto Mitfahrer;
                }
                Console.Clear();
                Console.WriteLine($"Herzlich Willkommen {UsersRegistrationName}");
                Console.WriteLine("Jetzt fehlt nur noch Ihr privates Passwort. Denken Sie daran, nie Ihre Passwörter mit Dritten zu teilen!\nIhr Passwort muss mindestens 6 Zeichen lang sein!");

                while (true)
                {
                    string UsersRegistrationsPassword = Console.ReadLine();
                    if (UsersRegistrationsPassword.Length >= 5)
                    {
                        Member.Members(UsersRegistrationName, UsersRegistrationsPassword);
                        break;
                    }
                    Console.WriteLine("Ihr Passwort ist leider zu kurz. Geben Sie erneut ein gültiges Passwort ein!");

                }
                Console.Clear();
                Console.WriteLine($"Vielen Dank {UsersRegistrationName} Ihre Registrierung ist nun abgeschlossen!");
                Thread.Sleep(500);
                Console.WriteLine("Drücken sie nun Enter um zurück zu Startbildschirm zu kommen, um sich einzuloggen!");
                string ReturnHome = Console.ReadLine();
                if (ReturnHome == string.Empty)
                {
                    return true;
                }

            }
            else if (Usersconcern == "Fahrer")
            {
                Fahrer:
                Console.WriteLine("Sie befinden sich im Registrierungsfenster als Fahrer!");
                Console.WriteLine(string.Empty);
                Console.WriteLine("Vielen Dank, dass Sie sich für unser Anwendung entschieden haben, um Ihre Fahrten anzubieten.");
                Console.WriteLine("Geben Sie nun bitte Ihren vollständigen Namen an");
                string UsersRegistrationNameD = Console.ReadLine();
                if (string.IsNullOrEmpty(UsersRegistrationNameD))
                {
                    Console.Clear();
                    Console.WriteLine("Dies ist leider eine ungültige Eingabe, bitte erneut versuchen.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    goto Fahrer;
                }
              
                Console.WriteLine($"Herzlich Willkommen {UsersRegistrationNameD}");
                Console.WriteLine("Jetzt fehlt nur noch Ihr privates Passwort. Denken Sie daran, nie Ihre Passwörter mit Dritten zu teilen!\nIhr Passwort muss mindestens 6 Zeichen lang sein!");

                while (true)
                {
                    string UsersRegistrationsPasswordD = Console.ReadLine();
                    if (UsersRegistrationsPasswordD.Length >= 5)
                    {
                        Driver.Drivers(UsersRegistrationNameD, UsersRegistrationsPasswordD);
                        break;
                    }
                    Console.WriteLine("Ihr Passwort ist leider zu kurz! Geben Sie nun ein gültiges Passwort ein");
                }
                Console.Clear();
                Console.WriteLine($"Vielen Dank {UsersRegistrationNameD} Ihre Registrierung ist nun abgeschlossen!");
                Thread.Sleep(500);
                Console.WriteLine("Drücken sie nun Enter um zurück zu Startbildschirm zu kommen, um sich einzuloggen!");
                string ReturnHome = Console.ReadLine();
                if (ReturnHome == string.Empty)
                {
                    return true;
                }

            }
            Console.ReadLine();
            return false;
        }
        public void MenuePage()
        {
        Menue:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("    ###       ###  ##########   ###         ########   ##########   ###     ###    ########## ");
            Thread.Sleep(50);
            Console.WriteLine("   ###       ###  ###          ###         ###        ###    ###   ####   #####   ###         ");
            Thread.Sleep(50);
            Console.WriteLine("  ##   ##   ##   ###          ###         ###        ###    ###   ### #  #  ###  ###          ");
            Thread.Sleep(50);
            Console.WriteLine(" ###  ###  ###  ########     ###         ###        ###    ###   ###  ###  ###  ########      ");
            Thread.Sleep(50);
            Console.WriteLine("###  #### ###  ###          ###         ###        ###    ###   ###       ###  ###            ");
            Thread.Sleep(50);
            Console.WriteLine("##### #####   ###          ###         ###        ###    ###   ###       ###  ###             ");
            Thread.Sleep(50);
            Console.WriteLine("###   ###    ##########   ##########  ########   ##########   ###       ###  ##########       ");
            Thread.Sleep(50);
            Console.WriteLine($"                             Willkommen zurück !");
            Thread.Sleep(50);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[1] = Add Carpool");
            Thread.Sleep(50);
            Console.WriteLine("[2] = Find a Carpool");
            Thread.Sleep(50);
            Console.WriteLine("[3] = Manage your Carpools ");
            Thread.Sleep(50);
            Console.WriteLine("[4] = Settings");
            int UsersAnswer = Convert.ToInt32(Console.ReadLine());
            if (UsersAnswer == 1)
            {
                var Carpool = new Carpool();
                Carpool.CreateACarPool();
                goto Menue;
            }
            else if(UsersAnswer == 2)
            {
                var FindCarpool = new Carpool();
                FindCarpool.FindACarPool(driverFile, memberFile);
            }
            else if (UsersAnswer == 3)
            {
                Console.Clear();
                Console.WriteLine("Coming Soon!");
                Thread.Sleep(2000);
                goto Menue;
            }
            else if (UsersAnswer == 4)
            {
                var Settings = new Settings();
                Settings.SettingsHandler(driverFile,memberFile);
                
            }


        }
        
    }
}


