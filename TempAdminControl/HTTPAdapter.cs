using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempAdminControl
{
    public class HTTPAdapter
    {
        const string baseApiAdress = "http://192.168.173.232:5282/api";

        public static async Task RegisterUser(string userName, string password, string name, string phoneNr, bool isAdmin)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var user = new User(userName, password, name, phoneNr, isAdmin);

                    // Serialisieren das Datenmodell in JSON
                    string jsonContent = System.Text.Json.JsonSerializer.Serialize(user);

                    // Erstellen Sie den HTTP-Content mit JSON-Daten
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    // Senden Sie die POST-Anfrage an den API-Endpunkt
                    var apiUrl = baseApiAdress + "/User/user_registration";
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    // Überprüfen Sie die Antwort und geben Sie sie aus
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Erfolgreiche Registrierung: " + responseContent);
                    }
                    else
                    {
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Fehler bei der Registrierung: " + errorMessage);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ein Fehler ist aufgetreten: " + ex.Message);
                }
            }
        }

        public static async Task<bool> ConfirmLogin(string userName, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var credentials = new LoginCredentials(userName, password);

                    // Serialisieren das Datenmodell in JSON
                    string jsonContent = System.Text.Json.JsonSerializer.Serialize(credentials);

                    // Erstellen Sie den HTTP-Content mit JSON-Daten
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    // Senden Sie die POST-Anfrage an den API-Endpunkt
                    var apiUrl = baseApiAdress + "/User/user_login";
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    // Überprüfen Sie die Antwort und geben Sie sie aus
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Login Erfolgreich: " + responseContent);
                        return true;
                    }
                    else
                    {
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Fehler beim Loginvorgang: " + errorMessage);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ein Fehler ist aufgetreten: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
