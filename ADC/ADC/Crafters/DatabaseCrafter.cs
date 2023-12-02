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
        SqlConnection db;
        string connectionString = Program.iniFile.Read("ConnectionString");

        public DatabaseCrafter(SQLMaster parent)
        {
            sql = parent;
            connectionString = connectionString.Replace("Initial Catalog=ADCDB;", "");
            db = new SqlConnection(connectionString);
        }

        public void CheckDatabase()
        {
            try
            {
                db.Open();
                db.Close();
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }


        }
    }
}
