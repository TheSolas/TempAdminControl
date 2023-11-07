using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempAdminControl;

public class User
{
    public User()
    {
        UserName = "";
        Password = "";
        Name = "";
        Phone = "";
        IsAdmin = false;
        LastLogIn = null;
    }
    public User(int? id,string userName, string password, string name, string phone, bool isAdmin, DateTime? lastLogin = null)
    {
        UserID = id;
        UserName = userName;
        Password = password;
        Name = name;
        Phone = phone;
        IsAdmin = isAdmin;
        LastLogIn = lastLogin;
    }

    public int? UserID { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime? LastLogIn { get; set; }
}
