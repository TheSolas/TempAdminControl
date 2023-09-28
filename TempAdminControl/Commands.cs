using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempAdminControl
{
    internal class Commands
    {
        public static void ShowCommandList()
        {
            Console.WriteLine("h/help                           - Get a list of all Commands");
            Console.WriteLine("delete {user|sensor|temp} {ID}   - Delete user|sensor|temp via ID");
        }

        public static void Delete(string deleteObject, string deleteID)
        {
            //Delete ID of Object from SQL DB
        }
    }
}
