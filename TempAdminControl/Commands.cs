using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempAdminControl;

internal class Commands
{

    private static Dictionary<string, string> CommandList = new Dictionary<string, string>()
    {
        {"help"           ,"help                                                                - Get a list of all Commands" },
        {"h"              ,"h                                                                   - Get a list of all Commands" },
        {"log"            ,"log                                                                 - Get a list of changes to MaxTemp" },
        {"user"           ,"user {id}                                                           - Show a single user" },
        {"userlist"       ,"userlist                                                            - Show List of all Users" },
        {"sensor"         ,"sensor {id}                                                         - Show a single sensor" },
        {"sensorlist"     ,"sensorlist                                                          - Show List of all Sensors" },
        {"create user"    ,"create user {userName} {password} {name} {phoneNr} {isAdmin}        - Create new User" },
        {"create sensor"  ,"create sensor {serverSchrank} {adresse} {hersteller} {maxTemp}      - Create new User" },
        {"change"         ,"change [user|sensor] {ID}                                           - Start Modification Dialogue to change all attributes of user|sensor via ID" },
        {"modify"         ,"modify [user|sensor] {ID}                                           - Start Modification Dialogue to change all attributes of user|sensor via ID" },
        {"delete"         ,"delete [user|sensor|temp] {ID}                                      - Delete user|sensor|temp via ID" },
    };
    public static void ShowCommandList(string command = "")
    {
        if (String.IsNullOrWhiteSpace(command))
        {
            var table = new ConsoleTable("Command","Description");
            foreach (var cmd in CommandList)
            {
                table.AddRow(cmd.Key, cmd.Value);
            }
            table.Write();
            Console.WriteLine();
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
                throw new Exception("Nicht implementiert");
            case "temp":
                throw new Exception("Nicht implementiert");
            default:
                Console.WriteLine($"{deleteObject} ist kein Typ der gelöscht werden kann. Löschbare Typen sind sensor/temp/user");
                break;
        }
    }

    public static void ShowLog()
    {
        throw new Exception("Nicht implementiert");
    }

    public static async Task Modify(string modObject, string modID)
    {
        switch (modObject.ToLower())
        {
            case "sensor":
                await HTTPAdapter.ModifySensor(modID);
                break;
            case "user":
                await HTTPAdapter.ModifyUser(modID);
                break;
            default:
                Console.WriteLine($"{modObject} ist kein Typ der geändert werden kann. Änderbare Typen sind sensor|user");
                break;
        }
    }

    public static async Task ShowUser(string userID)
    {
                await HTTPAdapter.ShowUser(userID);
    }
    public static async Task ShowUserList()
    {
        await HTTPAdapter.ShowUserList();
    }

    public static async Task ShowSensor(string sensorID)
    {
        await HTTPAdapter.ShowSensor(sensorID);
    }
    public static async Task ShowSensorList()
    {
        await HTTPAdapter.ShowSensorList();
    }

    public static async Task CreateUser(string createUserName, string createPwd, string name, string createTelNr, string isAdmin)
    {
        await HTTPAdapter.RegisterUser(createUserName, createPwd, name, createTelNr, Boolean.Parse(isAdmin));
    }

    public static async Task CreateSensor(string serverSchrank, string adresse, string hersteller, string maxTemp)
    {
        await HTTPAdapter.RegisterSensor(serverSchrank, adresse, hersteller, maxTemp);
    }
}
