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
            Console.Clear();
            Console.WriteLine("Sie befinden sich nun im Menü ein neuese Carpool zu erstellen");
            Console.WriteLine(string.Empty);
            Console.WriteLine("Wie viele Fahrgemeinschaften möchten Sie hinzufügen?");
            int CarpoolCount = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < CarpoolCount; i++)
            {
                int Id = i;
                Console.Clear();
                Console.WriteLine("Von wo möchten Sie abfahren?");
                string Start = Console.ReadLine();
                Console.WriteLine("Wo wollen sie hinfahren?");
                string Destination = Console.ReadLine();
                Console.WriteLine("Wann ist die geplante Abfahrt?");
                string Time = Console.ReadLine();
                Console.WriteLine("Wie viele Plätze hat Ihr Auto?");
                string SeatCount = Console.ReadLine();
                Console.WriteLine("Zum Schluss müssen sie nun noch ihre persönliche ID hinzufügen, sodass sie der Fahrgemeinscgaft zugeordnet werden können");
                string PersonalId = Console.ReadLine();
                var Carpool = $"{Id};{Start};{Destination};{Time};{SeatCount};{PersonalId}\n";
                File.AppendAllText($"C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv",Carpool);
                Console.WriteLine("Fahrgemeinschaft wurde erfolgreich hinzugefügt!");
                Thread.Sleep(1000);
            }

        }
        public void FindACarPool(string driverFile, string memberFile)
        {
            Console.Clear();
            Console.WriteLine("Die folgende AufListung ist gegliedert in ID (der Fahrgemeinschaft), Abfahrtsort, Ankunftsort, Abfahrtzeit, freie Sitzplätze ");
            Console.WriteLine(string.Empty);
            string[] readText = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
            List<string> readList = readText.ToList();
            foreach (string read in readList)
            {
                Console.WriteLine(read);
            }
            Console.WriteLine("Liegt eine dieser Fahgemeinschaften auf ihrem Weg (y/n)?");
            string PossibleMatch = Console.ReadLine();
            if (PossibleMatch == "y")
            {

            }
            else if(PossibleMatch == "n")
            {
                Console.Clear();
                Console.WriteLine("Möchten Sie ihre eigene Fahrgemeinschaft erstellen(y/n)?");
                string UsersChoice = Console.ReadLine();
                if(UsersChoice == "y")
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
        public void ReturnDashboardHandler(string driverFile, string memberFile)
        {
            var ReturnLogIN = new LoginRegistrationHandler(driverFile, memberFile);
            ReturnLogIN.MenuePage();
        }
    }
}
