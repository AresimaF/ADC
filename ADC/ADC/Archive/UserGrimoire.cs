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
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
        public string Name { get; set; }
        public string EmployeeID { get; set; }
        public bool NewPasswordRequired { get; set; }
        public string LastLogin { get; set; }
        public string LastModification { get; set; }
        public string DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public string DateDeactivated { get; set; }
        public string DeactivatedBy { get; set; }
        public bool Deactivated { get; set; }

        public UserGrimoire()
        {
            ID = 0;
            UserName = "";
            Password = "";
            Name = "";
            EmployeeID = "";
            Roles = "";
            NewPasswordRequired = false;
            LastLogin = "";
            DateCreated = "";
            CreatedBy = "";
            DateDeactivated = "";
            DeactivatedBy = "";
            Deactivated = false;
        }
    }
}
