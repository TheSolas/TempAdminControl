using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TempAdminControl.Commands;

namespace TempAdminControl
{
    internal class InputResolver
    {
        private const string USEHELP = "Use help or h to get a list of commands!";
        private static void ResolveDelete(string[] command)
        {
            if (command.Length != 3) throw new Exception("Befehl entspricht nicht der Vorgabe! " + USEHELP);
            switch (command[1])
            {
                case "sensor":
                case "temp":
                case "user":
                    Delete(command[1], command[2]);
                    break;
                default:
                    Console.WriteLine($"{command[1]} ist kein Typ der gelöscht werden kann. Löschbare Typen sind sensor/temp/user");
                    break;
            }
        }
        public static bool ResolveUserInput()
        {
            bool loggedIn = true;
            var nullableInput = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(nullableInput))
            {
                Console.WriteLine(USEHELP);
                return true;
            }
            string input = (string)nullableInput;
            var command = input.Split(" ");
            switch (command[0])
            {
                case "help":
                case "h":
                    ShowCommandList();
                    break;
                case "logout":
                    loggedIn = false;
                    break;
                case "delete":
                    ResolveDelete(command);
                    break;
                case "change":
                case "modify":
                    ResolveModify(command);
                    break;
                default:
                    Console.WriteLine(USEHELP);
                    break;
            }
            return loggedIn;
        }

        private static void ResolveModify(string[] command)
        {
            throw new NotImplementedException();
        }
    }
}
