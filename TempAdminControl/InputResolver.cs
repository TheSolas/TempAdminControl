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
        public static async Task<bool> ResolveUserInput()
        {
            try
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
                    case "log":
                        Commands.ShowLog();
                        break;
                    case "user":
                        ResolveShowUser(command);
                        break;
                    case "create":
                        await ResolveCreateUser(command);
                        break;
                    default:
                        Console.WriteLine(USEHELP);
                        break;
                }

                return loggedIn;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return true;
            }
        }

        private static async Task ResolveCreateUser(string[] command)
        {
            if (command.Length != 7) throw new Exception("Ungültige Anzahl an Parametern! Nutze 'help create' für den korrekten Aufbau des Befehls");
            await CreateUser(command[2], command[3], command[4], command[5], command[6]);
        }

        private static void ResolveShowUser(string[] command)
        {
            switch (command.Length)
            {
                case 1:
                    ShowUser();
                    break;
                case 2:
                    ShowUser(command[1]);
                    break;
                default:
                    throw new Exception("Ungültige Anzahl an Parametern!");
            }
        }

        private static void ResolveModify(string[] command)
        {
            if (command.Length != 5) throw new Exception("Befehl entspricht nicht der Vorgabe! " + USEHELP);
            switch (command[1])
            {
                case "sensor":
                case "user":
                    Modify(command[1], command[2], command[3], command[4]);
                    break;
                default:
                    Console.WriteLine($"{command[1]} ist kein Typ der geändert werden kann. Änderbare Typen sind sensor|user");
                    break;
            }
        }
    }
}
