using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.PeerToPeer.Collaboration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ADC;
using ADC.Crafters;
using ADC.Archive;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Diagnostics;

namespace ADC.Managers
{
    internal class SQLMaster
    {
        DatabaseCrafter builder;

        SqlConnection sqldb;

        string databaseName = "ADCDB";
        string databasePrefix { get { return databaseName.ToLower(); } }
        string[] dbRootTableNames = { "Users", "Roles", "Blueprint" };

        string connectionString = Program.iniFile.Read("ConnectionString");

        public SQLMaster()
        {
            builder = new DatabaseCrafter(this);

            try
            {
                sqldb = new SqlConnection(connectionString);

                sqldb.Open();
                sqldb.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Could not connect to the database. Please ensure connection information is correct.");
            }
        }

        //Modular Create New Entry
        public bool Create<T>(string Table, T Entry)
        {
            return true;
        }

        //Modular Update Entry
        public bool Update<T>(string Table, T Entry)
        {
            return true;
        }

        //Modular Read Entry with EQUALS
        public object Read<T> (string Table, string key, T value)
        {
            return null;
        }

        //Modular Delete Entry with EQUALS
        public bool Delete<T>(string Table, string key, T value)
        {
            return true;
        }

    }
}
