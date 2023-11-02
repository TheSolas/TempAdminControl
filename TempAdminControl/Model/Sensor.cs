using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempAdminControl.Model;

public class Sensor
{
    public Sensor(int id)
    {
        ID = id;
        Serverschrank = null;
        Adresse = null;
        Hersteller = null;
        Max_Temperature = null;
    }
    public Sensor(int id,string serverschrank, string adresse, string hersteller, double max_Temperature)
    {
        ID = id;
        Serverschrank = serverschrank;
        Adresse = adresse;
        Hersteller = hersteller;
        Max_Temperature = max_Temperature;
    }
    public int ID { get; set; }
    public string? Serverschrank { get; set; }
    public string? Adresse { get; set; }
    public string? Hersteller { get; set; }
    public double? Max_Temperature { get; set; }
}
