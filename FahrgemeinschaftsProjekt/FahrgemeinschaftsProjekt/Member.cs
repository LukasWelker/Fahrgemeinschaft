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
        public static void Members(string usersRegistrationName, string usersRegistrationsPassword, string usersregistrationAName)
        {
            var memberscsv = $"{usersRegistrationName};{usersregistrationAName};{usersRegistrationsPassword}\n";
            File.AppendAllText("C:\\Projects001\\FahrgemeinschaftProject\\Members.csv", memberscsv, Encoding.UTF8);
        }
    }
}
