using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADC.Archive
{
    internal class RoleGrimoire
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public RoleGrimoire()
        {
            ID = 0;
            Name = "";
            CreatedDate = DateTime.Now;
            CreatedBy = "";
        }
    }
}
