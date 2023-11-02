using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempAdminControl;

public class User
{
    public User(string userName, string password, string name, string phone, bool isAdmin)
    {
        UserName = userName;
        Password = password;
        Name = name;
        Phone = phone;
        IsAdmin = isAdmin;
    }

    public string UserName { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public bool IsAdmin { get; set; }
}
