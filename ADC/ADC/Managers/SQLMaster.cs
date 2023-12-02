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

namespace ADC.Managers
{
    internal class SQLMaster
    {
        DatabaseCrafter builder;

        string databaseName = "ADCDB";
        string databasePrefix { get { return databaseName.ToLower(); } }
        string[] dbRootTableNames = { "Users", "Roles", "Blueprint" };

        string connectionString = Program.iniFile.Read("ConnectionString");

        public SQLMaster()
        {
            builder = new DatabaseCrafter(this);
        }

        //Modular Create New Entry
        public bool Create(string Table, object Entry)
        {
            return true;
        }

        //Modular Update Entry
        public bool Update(string Table, object Entry)
        {
            return true;
        }

        //Modular Read Entry with EQUALS
        public object ReadEqual<T> (string Table, string key, T value)
        {
            return null;
        }

        //Modular Delete Entry with EQUALS
        public bool DeleteEqual<T>(string Table, string key, T value)
        {
            return true;
        }

    }
}
