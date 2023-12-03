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
using Microsoft.SqlServer.Management.Smo;
using System.Reflection;

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
            catch (SqlException e)
            {
                builder.CheckDatabase();
                //throw new Exception("Could not connect to the database. Please ensure connection information is correct.");
            }
        }

        //Modular Create New Entry
        public int Create<T>(string Table, T Entry)
        {

            List<string> columns = new List<string>();
            List<string> properties = new List<string>();

            List<PropertyInfo> propertyList = Entry.GetType().GetProperties().ToList();

            foreach (PropertyInfo property in propertyList)
            {
                columns.Add(property.Name);
                properties.Add("@" + property.Name);
            }

            string columnString = "(" + String.Join(", ", columns) + ")";
            string propertiesString = "(" + String.Join(", ", properties) + ")";

            var insertString = "INSERT INTO adcdb" + Table + " " + columnString + " VALUES " + propertiesString;

            int rowsAffected = sqldb.Execute(insertString, Entry);

            return rowsAffected;
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

        //Return Full List
        public List<T> List<T>(string Table, int resultCount = 1000)
        {
            var parameters = new { queryTable = Table , resultCount = resultCount};
            
            sqldb.Open();
            var toReturn = sqldb.Query("SELECT @resultCount FROM @queryTable", parameters).ToList();
            sqldb.Close();
            
            return toReturn.Cast<T>().ToList();
        }

    }
}
