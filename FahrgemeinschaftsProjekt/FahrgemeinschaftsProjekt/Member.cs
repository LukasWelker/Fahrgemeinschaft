using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace FahrgemeinschaftsProjekt
{
    public class Member
    {
        /// <summary>
        /// Method to create the CSV-File for users who registrate as a member
        /// Also creates the file if it does not exist  --> creates the file on connection with the first user who signs up as a member
        /// </summary>
        /// <param name="usersRegistrationName"></param>
        /// <param name="usersRegistrationsPassword"></param>
        /// <param name="usersregistrationAName"></param>
        public static void Members(string usersRegistrationName, string usersRegistrationsPassword, string usersregistrationAName)
        {
            var memberscsv = $"{usersRegistrationName};{usersregistrationAName};{usersRegistrationsPassword}\n";
            File.AppendAllText("C:\\Projects001\\FahrgemeinschaftProject\\Members.csv", memberscsv, Encoding.UTF8);
        }
    }
}
