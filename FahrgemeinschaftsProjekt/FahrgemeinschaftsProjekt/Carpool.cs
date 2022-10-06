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
        private int Id;
        public Carpool()
        {
            Id = 0;
        }
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
            } while (true);
            var readText = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
            if (readText != null && readText.Length > 0)
            {
                Id = Convert.ToInt32(readText.Last().Split(';').First()) + 1;
            }
            var baseId = Id;
            for (int i = Id; i < UA4 + baseId; i++)
            {
                Console.Clear();
                Console.WriteLine("Geben sie ihrer Fahrgemeinschaft einen Namen!");
                string CarPoolName = Console.ReadLine();
                Console.WriteLine("Von wo möchten Sie abfahren?");
                string Start = Console.ReadLine();
                Console.WriteLine("Wo wollen sie hinfahren?");
                string Destination = Console.ReadLine();
                Console.WriteLine("Wann ist die geplante Abfahrt?");
                string Time = Console.ReadLine();
                Console.WriteLine("Wie viele Plätze hat Ihr Auto noch frei?");
                string SeatCount = Console.ReadLine();
                Console.WriteLine("Sind sie Fahrer?");
                string Driver = Console.ReadLine();
                Console.WriteLine("Zuletzt benötigen wir noch Ihren Namen, um sie der Gemeinschaft hinzuzufügen.");
                string UsersName = Console.ReadLine();
                var Carpool = $"{Id};{CarPoolName};{Start};{Destination};{Time};{SeatCount};{Driver};{UsersName}\n";
                File.AppendAllText($"C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Carpool);
                Console.Clear();
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
            var location = 1;
            switch (UA5)
            {
                case 1:
                    userQuestion = "Wähle den Abfahrtsort aus.";
                    location = 2;
                    break;
                case 2:
                    userQuestion = "Wähle den Ankunftsort aus.";
                    location = 3;
                    break;
                case 3:
                    userQuestion = "Wähle die Abfahrtzeit aus.";
                    location = 4;
                    break;
                case 4:
                    SearchFallback(driverFile, memberFile);
                    return;
            }
            SearchFileByUserEntry(driverFile, memberFile, userQuestion, location);

        }

        private void SearchFallback(string driverFile, string memberFile)
        {
            Console.Clear();
            Console.WriteLine("Die folgende AufListung zeigt alle verfügbaren Fahrgemeinschaften.");
            Console.WriteLine(string.Empty);
            var CarPoolList = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);

            PrintOutCarPoolInfo(CarPoolList);
            AskUserForMatchingCarPool(driverFile, memberFile);
        }

        private void AskUserForMatchingCarPool(string driverFile, string memberFile)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(string.Empty);
            Console.WriteLine("Liegt eine dieser Fahgemeinschaften auf ihrem Weg (y/n)?");
            string PossibleMatch = Console.ReadLine();
            if (PossibleMatch == "y")
            {
                Console.Clear();
                EnterCarPool(driverFile, memberFile);

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

        private void SearchFileByUserEntry(string driverFile, string memberFile, string userQuestion, int location)
        {
            Console.Clear();
            var CarPoolList = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
            List<string> readList = CarPoolList.ToList();
            Console.WriteLine(userQuestion);
            string UserInput = Console.ReadLine();
            string[] filtered = FilterBasesOnUserInput(readList, UserInput, location);
            PrintOutCarPoolInfo(filtered);
            AskUserForMatchingCarPool(driverFile, memberFile);

        }

        private static string[] FilterBasesOnUserInput(List<string> readList, string UserInput, int location)
        {
            return readList.Where(x => x.Split(';')[location] == UserInput).ToArray();
        }

        private static void CheckUserInput(out int UA5, out ConsoleKeyInfo UsersSearchWish)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("[1] = Nach einer Fahrgemeinschaft im Bezug auf Abfahrtsort suchen\n" +
                    "------------------------------------------------------------------------------------\n" +
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
                            Console.WriteLine($"Name: {SplitCarPoolList[i]}");
                            break;
                        case 2:
                            Console.WriteLine($"Abfahrtsort: {SplitCarPoolList[i]}");
                            break;
                        case 3:
                            Console.WriteLine($"Ankunftsort: {SplitCarPoolList[i]}");
                            break;
                        case 4:
                            Console.WriteLine($"Abfahrtzeit: {SplitCarPoolList[i]}");
                            break;
                        case 5:
                            Console.WriteLine($"Sitzplätze: {SplitCarPoolList[i]}");
                            break;
                        case 6:
                            Console.WriteLine($"Fahrer bereits vorhanden: {SplitCarPoolList[i]}");
                            break;
                        case 7:
                            Console.WriteLine($"Mitglieder: {SplitCarPoolList[i]}");
                            break;
                    }
                }
            }
        }

        public void ReturnDashboardHandler(string driverFile, string memberFile)
        {
            Thread.Sleep(1000);
            var ReturnLogIN = new LoginRegistrationHandler(driverFile, memberFile);
            ReturnLogIN.MenuePage();
        }

        public void EnterCarPool(string driverFile, string memberFile)
        {
            Console.WriteLine("Sie haben eine passende Fahrgemeinschaft gefunden? Welche ID hat diese Fahrgemeinschaft?");
            var IdOfCarPool = Console.ReadLine();
            Console.WriteLine("Alles klar, nun brauchen wir noch ihren Namen, um sie der Fahrgemeinschaft hinzufügen.");
            string UserWhoEnters = Console.ReadLine();
            string[] CarPoolList = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
            List<string> readList = CarPoolList.ToList();
            var MatchingCarPool = readList.FirstOrDefault(x => x.Split(';')[Id] == IdOfCarPool) + " " + UserWhoEnters;
            var CarPool = readList.Where(x => x.Split(';')[Id] != IdOfCarPool).ToList();
            CarPool.Add(MatchingCarPool);
            var orderdCarpool = CarPool.OrderBy(x => x.Split(';')[0]);
            File.Delete("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv");
            File.AppendAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", orderdCarpool);
            Console.WriteLine(string.Empty);
            Console.WriteLine("Alles klar sie wurden nun der Fahrgemeinschaft hinzugefügt");
            ReturnDashboardHandler(driverFile, memberFile);
        }

        public void DisplayYourCarpools(string driverFile, string memberFile)
        {
            int UA7 = 0;
            ConsoleKeyInfo usersWish;
            do
            {
                Console.Clear();
                Console.WriteLine("[1] = Ausgeben aller Fahrgemeinschaften, in welchen Sie sich befinden");
                Console.WriteLine("[2] = Sie aus einer Fahrgemeinschaft entfernen");
                usersWish = Console.ReadKey();
                if (char.IsDigit(usersWish.KeyChar))
                {
                    UA7 = int.Parse(usersWish.KeyChar.ToString());
                    break;
                }
            } while (true);
            if (UA7 == 1)
            {
                Console.Clear();
                ShowingYourCarPools(driverFile, memberFile);
                Console.WriteLine("Drücken sie nun Enter um zurück zum Dashboard zu gelangen!");
                string goBackToDash = Console.ReadLine();
                if (string.IsNullOrEmpty(goBackToDash))
                {
                    ReturnDashboardHandler(driverFile, memberFile);
                }
                
            }
            else if (UA7 == 2)
            {
                //An diese Stelle muss ShowingYourCarPools(string driverFile, string memberFile)
                //An diese Stelle muss LeaveCarPool(string driveFile, string memberFile)
            }




        }

        private void ShowingYourCarPools(string driverFile, string memberFile)
        {
            while (true)
            {
                List<string> readList = ReadCarPoolList();
                Console.WriteLine("Geben sie bitte ihren Benutzernamen ein, um zu sehen in welcher Fahrgemeinschaft Sie eingetragen sind.");
                string userInput = Console.ReadLine();

                if (LoginRegistrationHandler.CheckIfUsersNameExistDM(userInput, memberFile)
                        || LoginRegistrationHandler.CheckIfUsersNameExistDM(userInput, driverFile))
                {
                    Console.Clear();
                    var filteredUserCarPools = readList.Where(x => x.Contains(userInput)).ToArray();
                    PrintOutCarPoolInfo(filteredUserCarPools);
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("Dies sind alle Ihre Fahrgemeinschaften.");
                    Console.WriteLine(string.Empty);
                   break;

                }
                else
                {
                    int UA6 = 0;
                    ConsoleKeyInfo usersChoice;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Entweder sind Sie in keiner Fahrgemeinschaft oder dies ist ein falscher Benutzername!");
                        Console.WriteLine("Möchten Sie es nochmal versuchen[1] oder zum Dashboard zurückkehren[2] ?");
                        usersChoice = Console.ReadKey();
                        if (char.IsDigit(usersChoice.KeyChar))
                        {
                            UA6 = int.Parse(usersChoice.KeyChar.ToString());
                            break;
                        }
                    } while (true);
                    if (UA6 == 1)
                    {
                        Console.Clear();
                    }
                    else if (UA6 == 2)
                    {
                        var ReturnLogIN = new LoginRegistrationHandler(driverFile, memberFile);
                        ReturnLogIN.MenuePage();
                    }
                }
            }
        }

        private static List<string> ReadCarPoolList()
        {
            var CarPoolList = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
            List<string> readList = CarPoolList.ToList();
            return readList;
        }

        public void DeleteCarPool(string driveFile, string memberFile)
        {
            List<string> readList = ReadCarPoolList();

        }

        public void LeaveCarPool(string driveFile, string memberFile)
        {
            int UA8 = 0;
            ConsoleKeyInfo usersWish;
            do
            {
                Console.WriteLine("[1] = Möchten Sie eine dieser Fahrgemeinschaften verlassen?");
                Console.WriteLine("[2] = Möchten sie zurück zum Dashboard");
                usersWish = Console.ReadKey();
                if (char.IsDigit(usersWish.KeyChar))
                {
                    UA8 = int.Parse(usersWish.KeyChar.ToString());
                    break;
                }
            } while (true);
            if (UA8 == 1)
            {

            }
            else if (UA8 == 2)
            {
                Console.WriteLine("Drücken sie nun Enter um zurück zum Dashboard zu gelangen!");
                string goBackToDash = Console.ReadLine();
                if (string.IsNullOrEmpty(goBackToDash))
                {
                    ReturnDashboardHandler(driveFile, memberFile);
                }
            }
        }
    }
}
