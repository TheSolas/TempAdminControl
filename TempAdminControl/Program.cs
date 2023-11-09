using System.Diagnostics;
using System.Net.Security;
using static TempAdminControl.InputResolver;

namespace TempAdminControl;
class Program
{
    private static bool loggedIn;
    static async Task Main(string[] args)
    {
        Console.WriteLine("Willkommen im AdminTool für die TempControlWebanwendung!");
        await SuccessfulLogin();

        do
        {
            try
            {
                loggedIn = await ResolveUserInput();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } while (loggedIn);

    }

    private static async Task SuccessfulLogin()
    {
        int loginTries = 0;
        if (Debugger.IsAttached)
        {
            loggedIn = true;
        }
        else
        {
            do
            {
                loggedIn = await Login();
                if (!loggedIn) loginTries++;
                if (loginTries > 2)
                {
                    Console.WriteLine("Zu viele Loginversuche! Das Programm wird sich nun schließen!");
                    await Task.Delay(1500);
                    Environment.Exit(0);
                }
            } while (!loggedIn);
        }
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
