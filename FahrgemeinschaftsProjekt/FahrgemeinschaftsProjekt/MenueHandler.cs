using System;
using System.Threading;


namespace FahrgemeinschaftsProjekt
{
    public class MenueHandler 
    {
        //Klassenvariablen funktionieren ähnlich wie Properties
        private string _driverFile;
        private string _memberFile;
        //Konstruktor
        public MenueHandler(string driverFile, string memberFile)
        {
            _driverFile = driverFile;
            _memberFile = memberFile;
        }
        /// <summary>
        /// MenuePage Method / 5 different options to choose: Add Carpool/Find Carpool/ Manage your Carpools/ Seetings/ Exit
        /// Error Handling with do while loop
        /// </summary>
        public void MenuePage()
        {
            int UA3 = 0;
        Menue:
            ConsoleKeyInfo UsersAnswer;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("    ###       ###  ##########   ###         ########   ##########   ###     ###    ########## ");
                Thread.Sleep(20);
                Console.WriteLine("   ###       ###  ###          ###         ###        ###    ###   ####   #####   ###         ");
                Thread.Sleep(20);
                Console.WriteLine("  ##   ##   ##   ###          ###         ###        ###    ###   ### #  #  ###  ###          ");
                Thread.Sleep(20);
                Console.WriteLine(" ###  ###  ###  ########     ###         ###        ###    ###   ###  ###  ###  ########      ");
                Thread.Sleep(20);
                Console.WriteLine("###  #### ###  ###          ###         ###        ###    ###   ###       ###  ###            ");
                Thread.Sleep(20);
                Console.WriteLine("##### #####   ###          ###         ###        ###    ###   ###       ###  ###             ");
                Thread.Sleep(20);
                Console.WriteLine("###   ###    ##########   ##########  ########   ##########   ###       ###  ##########       ");
                Thread.Sleep(20);
                Console.WriteLine($"                             Willkommen zurück !");
                Thread.Sleep(20);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("════════════════════════════════════");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[1] = Add Carpool");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("════════════════════════════════════");
                Thread.Sleep(20);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[2] = Find a Carpool");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("════════════════════════════════════");
                Thread.Sleep(20);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[3] = Manage your Carpools ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("════════════════════════════════════");
                Thread.Sleep(20);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[4] = Settings");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("════════════════════════════════════");
                Thread.Sleep(20);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[5] = Exit");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("════════════════════════════════════");
                Console.ForegroundColor = ConsoleColor.White;
                UsersAnswer = Console.ReadKey();
                
                if (char.IsDigit(UsersAnswer.KeyChar))
                {
                    UA3 = int.Parse(UsersAnswer.KeyChar.ToString());
                    break;
                }

            } while (true);


            if (UA3 == 1)
            {
                var Carpool = new Carpool();
                Carpool.CreateACarPool();
                goto Menue;
            }
            else if (UA3 == 2)
            {
                var FindCarpool = new Carpool();
                FindCarpool.FindACarPool(_driverFile, _memberFile);
            }
            else if (UA3 == 3)
            {
                Console.Clear();
                var Carpool = new Carpool();
                Carpool.DisplayYourCarpools(_driverFile, _memberFile);
                goto Menue;
            }
            else if (UA3 == 4)
            {
                var Settings = new Settings();
                Settings.SettingsHandler(_driverFile, _memberFile);
            }
            else if (UA3 == 5)
            {
                //Warum kann ich hier auf die Methode zugreifen ohne eine Objekterstellung
                //Lösung, die Methode ist static
                LoginRegistrationHandler.Exit();
            }
        }
    }
}
