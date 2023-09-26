using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya_Makrenko_PZ_20_3
{
    public class checkUser
    {
        public string Login { get; set; }
        public bool Isadmin { get; }

        public string Status => Isadmin ? "Admin" : "Director";

        public checkUser(string login, bool isadmin)
        {
            Login = login.Trim();
            Isadmin = isadmin;
        }
    }
}
