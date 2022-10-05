using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Reflection;

namespace FahrgemeinschaftsProjekt
{
    public class Carpool
    {
        public void CreateACarPool()
        {
            int UA4 = 0;
            ConsoleKeyInfo CarpoolCount;
            do
            {
                Console.Clear();
                Console.WriteLine("Sie befinden sich nun im Menü ein neuese Carpool zu erstellen");
                Console.WriteLine(string.Empty);
                Console.WriteLine("Wie viele Fahrgemeinschaften möchten Sie hinzufügen?");
                CarpoolCount = Console.ReadKey();
                if (char.IsDigit(CarpoolCount.KeyChar))
                {
                    UA4 = int.Parse(CarpoolCount.KeyChar.ToString());
                    break;
                }
                Console.WriteLine("Dies ist leider eine ungültige Eingabe versuchen sie es erneut");
            }while(true);
            var readText = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
            int Id = 0;
            if (readText != null && readText.Length > 0)
            {
                Id = Convert.ToInt32(readText.Last().Split(';').First()) + 1;
            }
            var baseId = Id;
            for (int i = Id; i < UA4 +baseId; i++)
            {
                Console.Clear();
                Console.WriteLine("Von wo möchten Sie abfahren?");
                string Start = Console.ReadLine();
                Console.WriteLine("Wo wollen sie hinfahren?");
                string Destination = Console.ReadLine();
                Console.WriteLine("Wann ist die geplante Abfahrt?");
                string Time = Console.ReadLine();
                Console.WriteLine("Wie viele Plätze hat Ihr Auto?");
                string SeatCount = Console.ReadLine();
                var Carpool = $"{Id};{Start};{Destination};{Time};{SeatCount}\n";
                File.AppendAllText($"C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv",Carpool);
                Console.WriteLine("Fahrgemeinschaft wurde erfolgreich hinzugefügt!");
                Thread.Sleep(1000);
                Id++;
            }
        }
        public void FindACarPool(string driverFile, string memberFile)
        {
            Console.Clear();
            int UA5 = 0;
            ConsoleKeyInfo UsersSearchWish;
            CheckUserInput(out UA5, out UsersSearchWish);
            var userQuestion = "";
            switch (UA5)
            {
                case 1:
                    userQuestion = "Wähle den Abfahrtsort aus.";
                    break;
                case 2:
                    userQuestion = "Wähle den Ankunftsort aus.";
                    break;
                case 3:
                    userQuestion = "Wähle die Abfahrtzeit aus.";
                    break;
                case 4:
                    SearchFallback(driverFile, memberFile);
                    return;
            }
            SearchFileByUserEntry(driverFile, memberFile, userQuestion);

        }

        private void SearchFallback(string driverFile, string memberFile)
        {
            Console.Clear();
            Console.WriteLine("Die folgende AufListung zeigt alle verfügbaren Fahrgemeinschaften.");
            Console.WriteLine(string.Empty);
            var CarPoolList = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);

            PrintOutCarPoolInfo(CarPoolList);
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(string.Empty);
            Console.WriteLine("Liegt eine dieser Fahgemeinschaften auf ihrem Weg (y/n)?");
            string PossibleMatch = Console.ReadLine();
            if (PossibleMatch == "y")
            {
                Console.WriteLine("Möchten sie sich eintragen, um der Fahrgemeinschaft beizutreten");

            }
            else if (PossibleMatch == "n")
            {
                Console.Clear();
                Console.WriteLine("Möchten Sie ihre eigene Fahrgemeinschaft erstellen(y/n)?");
                string UsersChoice = Console.ReadLine();
                if (UsersChoice == "y")
                {
                    CreateACarPool();
                    Console.Clear();
                    Console.WriteLine("Sie werden nun zum Dashboard geleitet");
                    Thread.Sleep(1000);
                    ReturnDashboardHandler(driverFile, memberFile);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Schade sie werden nun zum Dashboard geleitet");
                    Thread.Sleep(1000);
                    ReturnDashboardHandler(driverFile, memberFile);
                }

            }
        }

        private void SearchFileByUserEntry(string driverFile, string memberFile, string userQuestion)
        {
            Console.Clear();
            Console.WriteLine("hallo");
            var CarPoolList = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
            List<string> readList = CarPoolList.ToList();
            Console.WriteLine(userQuestion);
            string UserInput = Console.ReadLine();
            var filtered = readList.Where(x => x.Contains(UserInput)).ToArray();
            PrintOutCarPoolInfo(filtered);
            Thread.Sleep(1000);
            ReturnDashboardHandler(driverFile, memberFile);
        }

        private static void CheckUserInput(out int UA5, out ConsoleKeyInfo UsersSearchWish)
        {
            do
            {
                Console.WriteLine("[1] = Nach einer Fahrgemeinschaft im Bezug auf Abfahrtsort suchen\n" +
                    "------------------------------------------------------------------------------------\n"+
                    "[2] = Nach einer Fahrgemeinschaft im Bezug auf Ankunfstort suchen\n" +
                    "------------------------------------------------------------------------------------\n" +
                    "[3] = Nach einer Fahrgemeinschaft im Bezug auf Uhrzeit suchen\n" +
                    "------------------------------------------------------------------------------------\n" +
                    "[4] = Nach einer Fahrgemeinschaft manuell suchen");

                UsersSearchWish = Console.ReadKey();
                if (char.IsDigit(UsersSearchWish.KeyChar))
                {
                    UA5 = int.Parse(UsersSearchWish.KeyChar.ToString());
                    break;
                }
            } while (true);
        }

        private static void PrintOutCarPoolInfo(string[] CarPoolList)
        {
            foreach (var CarPool in CarPoolList)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
                var SplitCarPoolList = CarPool.Split(';');
                for (int i = 0; i < SplitCarPoolList.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            Console.WriteLine($"ID: {SplitCarPoolList[i]}");
                            break;
                        case 1:
                            Console.WriteLine($"Abfahrtsort: {SplitCarPoolList[i]}");
                            break;
                        case 2:
                            Console.WriteLine($"Ankunftsort: {SplitCarPoolList[i]}");
                            break;
                        case 3:
                            Console.WriteLine($"Abfahrtzeit: {SplitCarPoolList[i]}");
                            break;
                        case 4:
                            Console.WriteLine($"Sitzplätze: {SplitCarPoolList[i]}");
                            break;
                    }
                }
            }
        }

        public void ReturnDashboardHandler(string driverFile, string memberFile)
        {
            var ReturnLogIN = new LoginRegistrationHandler(driverFile, memberFile);
            ReturnLogIN.MenuePage();
        }
    }
}
