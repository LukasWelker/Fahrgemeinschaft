using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Net.NetworkInformation;

namespace FahrgemeinschaftsProjekt
{
    public class Carpool
    {
        //Benötige ich Variablen in verschiedenen Methoden derselben Klasse, definiere ich diese global
        private int Id;
        private bool noSpaceInCarPool = false;
        private string IdOfCarPool;
        //Konstante, die nicht variabel difiniert werden darf
        private LoginRegistrationHandler lrHandler = new LoginRegistrationHandler(
              "C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv",
              "C:\\Projects001\\FahrgemeinschaftProject\\Members.csv");
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
            if (File.Exists("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv"))
            {
                var readText = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
                if (readText != null && readText.Length > 0)
                {
                    Id = Convert.ToInt32(readText.Last().Split(';').First()) + 1;
                }
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
                var SplitCarPoolList = CarPool.Split(';');
                if (string.IsNullOrEmpty(CarPool))
                    return;
                if (!string.IsNullOrEmpty(CarPool))
                {
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
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
            IdOfCarPool = Console.ReadLine();
            CheckIfCarPoolIsFull(IdOfCarPool, driverFile, memberFile);
            if (!noSpaceInCarPool)
            {
                Console.WriteLine("Alles klar, nun brauchen wir noch Ihren Namen, um Sie der Fahrgemeinschaft hinzufügen.");
                string UserWhoEnters = Console.ReadLine();
                string[] CarPoolList = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
                List<string> readList = CarPoolList.ToList();
                var MatchingCarPool = readList.FirstOrDefault(x => x.Split(';')[Id] == IdOfCarPool) + "," + UserWhoEnters;
                var CarPool = readList.Where(x => x.Split(';')[Id] != IdOfCarPool).ToList();
                CarPool.Add(MatchingCarPool);
                var orderdCarpool = CarPool.OrderBy(x => x.Split(';')[0]);
                File.Delete("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv");
                File.AppendAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", orderdCarpool);
                Console.WriteLine(string.Empty);
                Console.WriteLine("Alles klar sie wurden nun der Fahrgemeinschaft hinzugefügt");
                ReturnDashboardHandler(driverFile, memberFile);
            }
           
        }

        public void DisplayYourCarpools(string driverFile, string memberFile)
        {
            int UA7 = 0;
            ConsoleKeyInfo usersWish;
            Console.Clear();
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
                Console.Clear();
                ReturnDashbaordWithEnter(driverFile, memberFile);
            }
            else if (UA7 == 2)
            {
                Console.Clear();
                ShowingYourCarPools(driverFile, memberFile);
                LeaveCarPool(driverFile, memberFile);
                ReturnDashboardHandler(driverFile, memberFile);
            }
        }

        private void ReturnDashbaordWithEnter(string driverFile, string memberFile)
        {
            Console.WriteLine("Drücken sie nun Enter um zurück zum Dashboard zu gelangen!");
            string goBackToDash = Console.ReadLine();
            if (string.IsNullOrEmpty(goBackToDash))
            {
                ReturnDashboardHandler(driverFile, memberFile);
            }
        }

        private void ShowingYourCarPools(string driverFile, string memberFile)

        {
            while (true)
            {
                if (File.Exists("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv"))
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
                        Console.WriteLine("Dies sind alle Ihre Fahrgemeinschaften." +
                            "Merken Sie sich bitte die individuelle ID.");
                        Console.WriteLine(string.Empty);
                        Console.WriteLine("Drücken sie nun Enter um sie weiterzuleiten!");
                        string goBackToDash = Console.ReadLine();
                        if (string.IsNullOrEmpty(goBackToDash))
                        {
                            Console.Clear();
                            break;
                        }

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
                            ReturnDashboardHandler(driverFile, memberFile);
                        }
                    }
                }
                //verhindert Fehler, dass wenn noch keine CSV Datei exiistiert, kein Fegler entsteht + User wird daraufhingewiesen was er tun muss
                else
                {
                    Console.WriteLine("Sie müssen zuerst in einer Fahrgemeinschaft eingrtagen sein.");
                    Console.WriteLine("Sie werden nun zum Dashboard weitergelietet.");
                    ReturnDashboardHandler(driverFile, memberFile);
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

        public void LeaveCarPool(string driverFile, string memberFile)
        {
            int UA8 = 0;
            ConsoleKeyInfo usersWish;
            do
            {
                Console.Clear();
                Console.WriteLine("[1] = Möchten Sie eine dieser Fahrgemeinschaften verlassen?");
                Console.WriteLine("[2] = Möchten Sie zurück zum Dashboard");
                usersWish = Console.ReadKey();
                if (char.IsDigit(usersWish.KeyChar))
                {
                    UA8 = int.Parse(usersWish.KeyChar.ToString());
                    break;
                }
            } while (true);
            if (UA8 == 1)
            {
                Console.Clear();
                Console.WriteLine("Bitte geben Sie die Id der Fahrgemeinschaft an, welche sie verlassen möchten.");
                var IdOfCarPool = Console.ReadLine();
                Console.WriteLine("Alles klar, nun brauchen wir noch Ihren Namen, um Sie aus der Fahrgemeinschaft zu entfernen.");
                    string userWhoLeaves = Console.ReadLine();
                if (LoginRegistrationHandler.CheckIfUsersNameExistDM(userWhoLeaves, memberFile)
                         || LoginRegistrationHandler.CheckIfUsersNameExistDM(userWhoLeaves, driverFile))
                {
                    List<string> readList = ReadCarPoolCsv();
                    //So kann man was entfernen und hinzufügen in einer CSV Datei
                    //Man sucht in der Csv Datei nach der Zeile mit Id und Name
                    var MatchingCarPool = readList
                        .FirstOrDefault(x => x
                            .Split(';')[Id] == IdOfCarPool && x
                            .Contains(userWhoLeaves));
                    //Sucht/Liest alle anderen Zeilen
                    var CarPoolOriginal = readList
                        .Where(x => x
                            .Split(';')[Id] != IdOfCarPool)
                        .ToList();
                    //Splitet das Array in strings
                    var SplitedMatchingCarPool = MatchingCarPool.Split(';');
                    //Splitted die gewünschte Zeile intern nach ',' um einen einzelnen Eintrag zu removen
                    var SplitSearchedLine = SplitedMatchingCarPool[7].Split(',').ToList();
                    //Suche alle Einträge raus, die nicht dem User Input entsprechen
                    var DifferntiateListInput = SplitSearchedLine.Where(x => !x.Equals(userWhoLeaves));
                    //Wandelt es in einen String um
                    var RecreateLine = string.Join(",", DifferntiateListInput);
                    //Schreibt die Zeile, wie man sie braucht
                    var WishResultSplitedMatchingCarPool = $"{SplitedMatchingCarPool[0]};{SplitedMatchingCarPool[1]};{SplitedMatchingCarPool[2]};{SplitedMatchingCarPool[3]};{SplitedMatchingCarPool[4]};{SplitedMatchingCarPool[5]};" +
                        $"{SplitedMatchingCarPool[6]};{RecreateLine}";
                    //Fügt alle Zeilen, die man aus der Liste nicht braucht mit der einen veränderten zusammen in eine Liste
                    CarPoolOriginal.Add(WishResultSplitedMatchingCarPool);
                    //Löscht die ganze Liste um in Zeile 395 die Liste wie in Zeile 392 zusammengefügt in eine Csv Datei zu schreiben
                    File.Delete("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv");
                    File.AppendAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", CarPoolOriginal);
                    Console.WriteLine("Vielen Dank, Sie wurden nun aus der Fahrgemeinschaft entfernt.");
                    InstantDeletionOfCarPoolIfEmpty(IdOfCarPool);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Entweder Sie haben Ihren Namen falsch geschrieben oder Sie haben sich vertippt." +
                        " Sie werden nun zurückgeleitet, zur erneuten Eingabe.");
                    Thread.Sleep(2000);
                    LeaveCarPool(driverFile, memberFile);
                }
            }
            else if (UA8 == 2)
            {
                Console.WriteLine("Drücken Sie nun Enter um zurück zum Dashboard zu gelangen!");
                string goBackToDash = Console.ReadLine();
                if (string.IsNullOrEmpty(goBackToDash))
                {
                    ReturnDashboardHandler(driverFile, memberFile);
                }
            }
        }

        private static List<string> ReadCarPoolCsv()
        {
            string[] CarPoolList = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
            List<string> readList = CarPoolList.ToList();
            return readList;
        }

        public void InstantDeletionOfCarPoolIfEmpty(string IdOfCarPool)
        {
            string[] CarPoolList = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
            var id = Convert.ToInt32(IdOfCarPool);
            //Uwandlung des einzelnen Strings in Array 
            //string[] singleCarPool = CarPoolList[id].Split(';');
            string[] singleCarPool;
            for (int i = 0; i < CarPoolList.Count(); i++)
            {
                singleCarPool = CarPoolList[i].Split(';');
                if (singleCarPool.Length <= 8 && singleCarPool[7] == string.Empty)
                {
                    //Ersetzt den String mit einem leeren String wenn das Array kleiner gleich 8 ist
                    CarPoolList[id] = string.Empty;
                    CarPoolList[id].ToList();
                    List<string> readList = ReadCarPoolCsv();
                    var CarPoolOriginal = readList
                       .Where(x => x
                           .Split(';')[Id] != IdOfCarPool)
                       .ToList();
                    CarPoolOriginal.Add(CarPoolList[id]);
                    File.Delete("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv");
                    File.AppendAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", CarPoolOriginal);

                }
            }
        }

        public void CheckIfCarPoolIsFull(string IdofCarPool, string driverFile, string memberFile)
        {
            string[] CarPoolList = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
            var id = Convert.ToInt32(IdofCarPool);
            string[] singleCarPool = CarPoolList[id].Split(';');
            string[] carPoolNames = singleCarPool[7].Split(',');
            if(carPoolNames.Length == 5)
            {
                Console.WriteLine("Es ist leider kein Platz mehr in dieser Fahrgemeinschaft.\n" +
                    "Sie werdem nun zum Dashboard weitergeleitet.");
                Thread.Sleep(1000);
                ReturnDashboardHandler(driverFile, memberFile);
                noSpaceInCarPool = true;
            }
            
        }
    }
}
