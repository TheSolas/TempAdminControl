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
                    Commands.ShowCommandList();
                    break;
                case "logout":
                    loggedIn = false;
                    break;
                case "delete":
                    await ResolveDelete(command);
                    break;
                case "change":
                case "modify":
                    await ResolveModify(command);
                    break;
                case "log":
                    Commands.ShowLog();
                    break;
                case "userlist":
                    await Commands.ShowUserList();
                    break;
                case "user":
                    await Commands.ShowUser(command);
                    break;
                case "create":
                    await ResolveCreate(command);
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

    private static async Task ResolveCreate(string[] command)
    {
        switch (command[1])
        {
            case "user":
                if (command.Length != 7) throw new Exception("Ungültige Anzahl an Parametern! Nutze 'help create' für den korrekten Aufbau des Befehls");
                await CreateUser(command[2], command[3], command[4], command[5], command[6]);
                break;
            case "sensor":
                if (command.Length != 6) throw new Exception("Ungültige Anzahl an Parametern! Nutze 'help create' für den korrekten Aufbau des Befehls");
                await CreateSensor(command[2], command[3], command[4], command[5]);
                break;
            default:
                throw new Exception("Ungültiger Create Befehl! Nutze 'help create' für den korrekten Aufbau des Befehls");
        }
    }

    private static async Task ResolveDelete(string[] command)
    {
        if (command.Length != 3) throw new Exception("Befehl entspricht nicht der Vorgabe! " + USEHELP);

        await Delete(command[1], command[2]);
    }

    private static async Task ResolveModify(string[] command)
    {
        if (command.Length != 3) throw new Exception("Befehl entspricht nicht der Vorgabe! " + USEHELP);
        await Modify(command[1], command[2]);
    }
}