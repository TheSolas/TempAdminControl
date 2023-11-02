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
            {"help"    ,"help                                                                - Get a list of all Commands" },
            {"h"       ,"h                                                                   - Get a list of all Commands" },
            {"log"     ,"log                                                                 - Get a list of changes to MaxTemp" },
            {"userlist","userlist                                                            - Show List of all Users" },
            {"create"  ,"create [user] {userName} {password} {name} {phoneNr} {isAdmin}      - Create new User" },
            {"change"  ,"change [user|sensor] {ID}                                           - Start Modification Dialogue to change all attributes of user|sensor via ID" },
            {"modify"  ,"modify [user|sensor] {ID}                                           - Start Modification Dialogue to change all attributes of user|sensor via ID" },
            {"delete"  ,"delete [user|sensor|temp] {ID}                                      - Delete user|sensor|temp via ID" },
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

        public static async Task Delete(string deleteObject, string deleteID)
        {
            switch (deleteObject.ToLower())
            {
                case "sensor":
                    await HTTPAdapter.DeleteSensor(deleteID);
                    break;
                case "user":
                    break;
                case "temp":
                    break;
                default:
                    Console.WriteLine($"{deleteObject} ist kein Typ der gelöscht werden kann. Löschbare Typen sind sensor/temp/user");
                    break;
            }
        }

        public static void ShowLog()
        {
            //Get Log from DB and show it
        }

        public static async void Modify(string modObject, string modID)
        {
            switch (modObject.ToLower())
            {
                case "sensor":
                    break;
                case "user":
                    break;
                default:
                    Console.WriteLine($"{modObject} ist kein Typ der geändert werden kann. Änderbare Typen sind sensor|user");
                    break;
            }
        }

        public static async void ShowUser()
        {
            await HTTPAdapter.ShowUserList();
        }

        public static async Task CreateUser(string createUserName, string createPwd,string name, string createTelNr, string isAdmin)
        {
            await HTTPAdapter.RegisterUser(createUserName,createPwd,name,createTelNr,Boolean.Parse(isAdmin));
            //Console.WriteLine($"Der neue Nutzer {createUserName} wurde erfolgreich erstellt");
        }
    }
}
