using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADC.Archive
{
    public class UserGrimoire
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string EmployeeID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
        public bool NewPasswordRequired { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime LastLogin { get; set; }
        public bool Deactivated { get; set; }

        public UserGrimoire()
        {
            ID = 0;
            Name = "Thane Doe";
            EmployeeID = "000000";
            Username = "Username";
            Password = "Password";
            Roles = "User";
            NewPasswordRequired = true;
            CreatedDate = DateTime.Now;
            CreatedBy = "None";
            ModifiedDate = DateTime.Now;
            ModifiedBy = "None";
            LastLogin = DateTime.Now;
            Deactivated = false;
        }

    }
}
