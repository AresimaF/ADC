using ADC.Managers;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADC.Crafters
{
    internal class DatabaseCrafter
    {
        SQLMaster sql;
       public DatabaseCrafter(SQLMaster parent)
        {
            sql = parent;
        }
    }
}
