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
    const string baseApiAdress = "http://192.168.155.232:5282/api";
    public static async Task<HttpResponseMessage> SendPostHTTPRequest<T>(T sendObject, string apiRoute)
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

    public static async Task<HttpResponseMessage> SendGetHTTPRequest(string apiRoute)
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
    private static async Task ShowResponseContent(HttpResponseMessage response)
    {
        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);
    }
    public static async Task RegisterUser(string userName, string password, string name, string phoneNr, bool isAdmin)
    {

        var user = new User(userName, password, name, phoneNr, isAdmin);
        var response = await SendPostHTTPRequest(user, "/User/register_user");
        await ShowResponseContent(response);
    }

    public static async Task RegisterSensor(string serverSchrank, string adresse, string hersteller, string maxTemp)
    {
        var sensor = new Sensor(null, serverSchrank, adresse, hersteller, Double.Parse(maxTemp));
        var response = await SendPostHTTPRequest(sensor, "/Sensor/create_sensor");
        await ShowResponseContent(response);
    }

    public static async Task<bool> ConfirmLogin(string userName, string password)
    {
        var credentials = new LoginCredentials(userName, password);
        var response = await SendPostHTTPRequest(credentials, "/User/login_user");
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
        var response = await SendPostHTTPRequest(sensor, $"/Sensor/{id}/delete_sensor");
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
        var response = await SendPostHTTPRequest(sensor, $"/Sensor/{id}/modify_sensor");
        await ShowResponseContent(response);
    }

    public static async Task ModifyUser(string id)
    {
        Console.Write("UserName: ");
        var userName = Console.ReadLine() ?? "";
        Console.Write("Passwort: ");
        var pwd = Console.ReadLine() ?? "";
        Console.Write("Name: ");
        var name = Console.ReadLine() ?? "";
        Console.Write("TelNr: ");
        var telNr = Console.ReadLine() ?? "";
        Console.Write("IsAdmin?: ");
        var isAdmin = Console.ReadLine() ?? "";

        var user = new User(int.Parse(id), userName, pwd, name, telNr, Boolean.Parse(isAdmin));
        var response = await SendPostHTTPRequest(user, $"/User/{id}/modify_user");
        await ShowResponseContent(response);
    }


    public static async Task ShowUserList()
    {
        var response = await SendGetHTTPRequest("/User/show_all_users");
        var responseBody = await response.Content.ReadAsStringAsync();
        if (responseBody.StartsWith("["))
        {

            List<User> userList = JsonConvert.DeserializeObject<List<User>>(responseBody) ?? new List<User>();
            var table = new ConsoleTable("UserID", "UserName", "Name", "TelefonNr", "IsAdmin", "LastLogin");
            foreach (User user in userList)
            {
                table.AddRow(user.UserID, user.UserName, user.Name, user.Phone, user.IsAdmin, user.LastLogIn);
            }
            table.Write();
            Console.WriteLine();
        }
        else await ShowResponseContent(response);
    }

    public static async Task ShowUser(string id)
    {
        var response = await SendGetHTTPRequest($"/User/{id}/show_user");
        var responseBody = await response.Content.ReadAsStringAsync();
        if (responseBody.StartsWith("{"))
        {
            var user = JsonConvert.DeserializeObject<User>(responseBody) ?? new User();
            var table = new ConsoleTable("UserID", "UserName", "Name", "TelefonNr", "IsAdmin", "LastLogin");
            table.AddRow(user.UserID, user.UserName, user.Name, user.Phone, user.IsAdmin, user.LastLogIn);
            table.Write();
            Console.WriteLine();
        }
        else await ShowResponseContent(response);

    }

    public static async Task ShowSensorList()
    {
        var response = await SendGetHTTPRequest("/Sensor/show_all_sensors");
        var responseBody = await response.Content.ReadAsStringAsync();
        if (responseBody.StartsWith("["))
        {

            List<Sensor> sensorList = JsonConvert.DeserializeObject<List<Sensor>>(responseBody) ?? new List<Sensor>();
            var table = new ConsoleTable("SensorID", "Serverschrank", "Adresse", "Hersteller", "MaxTemp");
            foreach (Sensor sensor in sensorList)
            {
                table.AddRow(sensor.SensorID, sensor.Serverschrank, sensor.Adresse, sensor.Hersteller, sensor.Max_Temperature);
            }
            table.Write();
            Console.WriteLine();
        }
        else await ShowResponseContent(response);
    }

    public static async Task ShowSensor(string id)
    {
        var response = await SendGetHTTPRequest($"/Sensor/{id}/show_sensor");
        var responseBody = await response.Content.ReadAsStringAsync();
        if (responseBody.StartsWith("{"))
        {
            var sensor = JsonConvert.DeserializeObject<Sensor>(responseBody) ?? new Sensor();
            var table = new ConsoleTable("SensorID", "Serverschrank", "Adresse", "Hersteller", "MaxTemp");
            table.AddRow(sensor.SensorID,sensor.Serverschrank,sensor.Adresse,sensor.Hersteller,sensor.Max_Temperature);
            table.Write();
            Console.WriteLine();
        }
        else await ShowResponseContent(response);

    }
}
