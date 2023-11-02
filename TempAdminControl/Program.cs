using System.Net.Security;
using static TempAdminControl.InputResolver;

namespace TempAdminControl;
class Program
{
    private static bool loggedIn;
    static async Task Main(string[] args)
    {
        Console.WriteLine("Willkommen im AdminTool für die TempControlWebanwendung!");
        do
        {
            loggedIn = await Login();
        } while (!loggedIn);

        do
        {
            loggedIn = await ResolveUserInput();
        } while (loggedIn);

        Console.WriteLine("Hello, World!");
        Console.WriteLine("Hello, World!");
        Console.WriteLine("Hello, World!");
    }







    private static async Task<bool> Login()
    {
        Console.WriteLine("Bitte loggen sie sich ein:");
        Console.Write("Nutzername: ");
        var usrName = Console.ReadLine() ?? "";
        Console.Write("Passwort: ");
        var pwd = ReadPwd();
        Console.WriteLine();
        //Check Credentials
        var loginResult = await HTTPAdapter.ConfirmLogin(usrName, pwd);
        if (loginResult)
        {
            Console.WriteLine("Use help or h to get a list of commands!");
            return true;
        }
        else
        {
            return false;
        }
    }

    private static string ReadPwd()
    {
        var pass = string.Empty;
        ConsoleKey key;
        do
        {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && pass.Length > 0)
            {
                Console.Write("\b \b");
                pass = pass[0..^1];
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                Console.Write("*");
                pass += keyInfo.KeyChar;
            }
        } while (key != ConsoleKey.Enter);
        return pass;
    }
}
