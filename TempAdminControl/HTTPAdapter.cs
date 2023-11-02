using ConsoleTables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempAdminControl.Model;

namespace TempAdminControl;

public class HTTPAdapter
{
    const string baseApiAdress = "http://192.168.173.232:5282/api";
    public static async Task<HttpResponseMessage> SendHttpRequest<T>(T sendObject, string apiRoute)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Serialisieren das Datenmodell in JSON
                string jsonContent = System.Text.Json.JsonSerializer.Serialize(sendObject);

                // Erstellen Sie den HTTP-Content mit JSON-Daten
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Senden Sie die POST-Anfrage an den API-Endpunkt
                var apiUrl = baseApiAdress + apiRoute;
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                return response;
                // Überprüfen Sie die Antwort und geben Sie sie aus
                //string responseContent = await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ein Fehler ist aufgetreten: " + ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }
    }

    public static async Task<HttpResponseMessage> SendHttpRequestGet(string apiRoute)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Senden Sie die Get-Anfrage an den API-Endpunkt
                var apiUrl = baseApiAdress + apiRoute;
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                return response;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ein Fehler ist aufgetreten: " + ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
    public static async Task RegisterUser(string userName, string password, string name, string phoneNr, bool isAdmin)
    {

        var user = new User(userName, password, name, phoneNr, isAdmin);
        var response = await SendHttpRequest(user, "/User/register_user");
        await ShowResponseContent(response);
    }

    public static async Task<bool> ConfirmLogin(string userName, string password)
    {
        var credentials = new LoginCredentials(userName, password);
        var response = await SendHttpRequest(credentials, "/User/login_user");
        await ShowResponseContent(response);
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static async Task DeleteSensor(string id)
    {
        var sensor = new Sensor(int.Parse(id));
        var response = await SendHttpRequest(sensor, "/Sensor/delete_sensor");
        await ShowResponseContent(response);
    }


    public static async Task ModifySensor(string id)
    {
        Console.Write("Serverschrank: ");
        var serverschrank = Console.ReadLine() ?? "";
        Console.Write("Adresse: ");
        var adresse = Console.ReadLine() ?? "";
        Console.Write("Hersteller: ");
        var hersteller = Console.ReadLine() ?? "";
        Console.Write("Maximale Temperatur: ");
        var maxTemp = Console.ReadLine() ?? "";

        var sensor = new Sensor(int.Parse(id), serverschrank, adresse, hersteller, Double.Parse(maxTemp));
        var response = await SendHttpRequest(sensor, "/Sensor/modify_sensor");
        await ShowResponseContent(response);
    }

    public static async Task ShowUserList()
    {
        var response = await SendHttpRequestGet("/User/show_users");
        var responseBody = await response.Content.ReadAsStringAsync();
        List<User> userList = JsonConvert.DeserializeObject<List<User>>(responseBody) ?? new List<User>();
        var table = new ConsoleTable("UserName", "Name", "TelefonNr", "IsAdmin");
        foreach (User user in userList)
        {
            table.AddRow(user.UserName, user.Name, user.Phone, user.IsAdmin);
        }
        table.Write();
        Console.WriteLine();
    }
    private static async Task ShowResponseContent(HttpResponseMessage response)
    {
        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);
    }
}
