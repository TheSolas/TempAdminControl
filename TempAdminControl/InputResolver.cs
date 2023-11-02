using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TempAdminControl.Commands;

namespace TempAdminControl;

internal class InputResolver
{
    private const string USEHELP = "Use help or h to get a list of commands!";
    

    public static async Task<bool> ResolveUserInput()
    {
        try
        {

            bool loggedIn = true;
            string input = Console.ReadLine() ?? "";
            if (String.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(USEHELP);
                return true;
            }

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
                    await ResolveDelete(command);
                    break;
                case "change":
                    break;
                case "modify":
                    ResolveModify(command);
                    break;
                case "log":
                    Commands.ShowLog();
                    break;
                case "userlist":
                    Commands.ShowUser();
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

    private static async Task ResolveDelete(string[] command)
    {
        if (command.Length != 3) throw new Exception("Befehl entspricht nicht der Vorgabe! " + USEHELP);

        await Delete(command[1], command[2]);
    }

    //private static void ResolveShowUser(string[] command)
    //{
    //    if (command.Length == 1)
    //    {
    //        ShowUser();
    //    }
    //    else throw new Exception("Ungültige Anzahl an Parametern!");
    //}

    private static void ResolveModify(string[] command)
    {
        if (command.Length != 3) throw new Exception("Befehl entspricht nicht der Vorgabe! " + USEHELP);
        Modify(command[1], command[2]);
    }
}