using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempAdminControl
{
    internal class Commands
    {

        private static Dictionary<string, string> CommandList = new Dictionary<string, string>()
        {
            {"help"  ,"h/help                                                       - Get a list of all Commands" },
            {"h"     ,"h/help                                                       - Get a list of all Commands" },
            {"user"  ,"user {ID}                                                    - Show user via ID.Alternatively, by not proving an ID, show List of all Users" },
            {"create","create [user] {userName} {password} {phoneNr} {isAdmin}      - Create new User" },
            {"change","change [user|sensor] {ID} {key} {value}                      - Change attribute 'key' of user|sensor via ID to 'value'" },
            {"modify","modify [user|sensor] {ID} {key} {value}                      - Change attribute 'key' of user|sensor via ID to 'value'" },
            {"delete","delete [user|sensor|temp] {ID}                               - Delete user|sensor|temp via ID" },
        };
        public static void ShowCommandList(string command = "")
        {
            if (String.IsNullOrWhiteSpace(command))
            {
                foreach (var cmd in CommandList)
                {
                    Console.WriteLine(cmd.Value);
                }
            }
            else
            {
                if (CommandList.ContainsKey(command))
                {
                    Console.WriteLine(CommandList[command]);
                }
                else
                {
                    Console.WriteLine($"Befehl {command} konnte nicht gefunden werden!");
                }

            }
        }

        public static void Delete(string deleteObject, string deleteID)
        {
            //Delete ID of Object from SQL DB
        }

        public static void ShowLog()
        {
            //Get Log from DB and show it
        }

        public static void Modify(string modObject, string modID, string modAttribute, string modValue)
        {
            //Ändert das gegebene modAttribute einer Tabelle modObject mit der ID modID auf den Wert modValue
        }

        public static void ShowUser(string showID = "")
        {
            if (String.IsNullOrWhiteSpace(showID))
            {
                //ShowList
            }
            else
            {
                //ShowUser via ID
            }
        }

        public static void CreateUser(string createUserName, string createPwd, string createTelNr, string isAdmin)
        {
            //CreateUser
            Console.WriteLine($"Der neue Nutzer {createUserName} wurde erfolgreich erstellt");
        }
    }
}
