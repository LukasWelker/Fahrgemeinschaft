using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
                Console.WriteLine("[2] = Change Password");
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
                Console.WriteLine("Wollen Sie sich wirklich ausloggen wenn ja, drücken Sie nun ENTER!");
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
                
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Um ihr Passwort zurückzusetzen, geben Sie bitte ihren aktuellen Benutzernamen ein");
                    string userInput = Console.ReadLine();
                    userInput.Trim(' ');
                    //
                    if (LoginRegistrationHandler.CheckIfUsersNameExistDM(userInput, memberFile)
                                || LoginRegistrationHandler.CheckIfUsersNameExistDM(userInput, driverFile))
                    {
                        Console.WriteLine("Alles klar geben Sie nun bitte Ihr Passwort ein");
                        string userPassword = Console.ReadLine();
                        userPassword.Trim(' ');
                        //
                        if (LoginRegistrationHandler.CheckifUserPasswordExistDM(userPassword, driverFile)
                            || LoginRegistrationHandler.CheckifUserPasswordExistDM(userPassword, memberFile))
                        {
                            Console.Clear();
                            Console.WriteLine("Vielen Dank, geben Sie nun Ihr neues Passwort ein");
                            string userNewPassword = Console.ReadLine();
                            userNewPassword.Trim(' ');
                            string path = "C:\\Projects001\\FahrgemeinschaftProject\\Members.csv";
                            string path2 = "C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv";
                            PreConditionForPasswordChange(userInput, userPassword, userNewPassword, path);
                            PreConditionForPasswordChange(userInput, userPassword, userNewPassword, path2);
                            Console.Clear();
                            Console.WriteLine("Ihr Passwort wurde nun geändert.");
                            var returnDashobard = new Carpool();
                            returnDashobard.ReturnDashboardHandler(driverFile, memberFile);

                        }
                        else
                        {
                            Console.WriteLine("Dies ist leider ein ungültiges Passwort.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Dies ist ein ungültiger Benutzername");
                    }
                }
               
            }
        }

        private static void PreConditionForPasswordChange(string userInput, string userPassword, string userNewPassword, string path)
        {
            string[] carPoolArray = File.ReadAllLines(path, Encoding.UTF8);
            List<string> carPoolList = carPoolArray.ToList();
            //Filtern der CSV-Datei nach der gesuchten Spalte
            var matchingLine = carPoolList.FirstOrDefault(x => x.Split(';')[2] == userPassword && x.Contains(userInput));
            //Filtert die übrigen Zeilen der CSV-Datei
            var remainingLines = carPoolList.Where(x => x.Split(';')[2] != userPassword && x.Split(';')[0] != userInput).ToList();
            if(matchingLine != null)
            {
                PasswordChange(userPassword, userNewPassword, path, matchingLine, remainingLines);
            }
        }

        private static void PasswordChange(string userPassword, string userNewPassword, string path, string matchingLine, List<string> remainingLines)
        {
            var newMatchingLine = matchingLine.Replace(userPassword, userNewPassword);
            remainingLines.Add(newMatchingLine);
            File.WriteAllLines(path, remainingLines);
        }
    }
}
