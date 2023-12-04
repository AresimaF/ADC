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
    public class SQLMaster
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
                ConnectToDatabase();

                sqldb.Open();
                sqldb.Close();
            }
            catch (SqlException e)
            {
                builder.CheckDatabase();
                
                //throw new Exception("Could not connect to the database. Please ensure connection information is correct.");
            }
        }

        public async void ConnectToDatabase()
        {
            sqldb = new SqlConnection(connectionString);

        }

        //Modular Create New Entry
        public int Create<T>(string Table, T Entry)
        {

            List<string> columns = new List<string>();
            List<string> properties = new List<string>();

            List<PropertyInfo> propertyList = Entry.GetType().GetProperties().ToList();

            foreach (PropertyInfo property in propertyList)
            {
                if (property.Name == "ID")
                {
                    continue;
                }
                columns.Add(property.Name);
                properties.Add("@" + property.Name);
            }

            string columnString = "(" + String.Join(", ", columns) + ")";
            string propertiesString = "(" + String.Join(", ", properties) + ")";

            var insertString = "INSERT INTO adcdb" + Table + " " + columnString + " VALUES " + propertiesString;

            sqldb.Open();
            int rowsAffected = sqldb.Execute(insertString, Entry);
            sqldb.Close();

            return rowsAffected;
        }

        //Modular Update Entry
        public int Update<T>(string Table, T Entry)
        {
            List<string> properties = new List<string>();

            List<PropertyInfo> propertyList = Entry.GetType().GetProperties().ToList();

            foreach (PropertyInfo property in propertyList)
            {
                if (property.Name == "ID")
                {
                    continue;
                }
                
                properties.Add(property.Name + " = @" + property.Name);
            }


            string propertiesString = "(" + String.Join(", ", properties) + ")";

            var insertString = "UPDATE adcdb" + Table + " SET " + propertiesString + " WHERE ID = @ID";

            sqldb.Open();
            int rowsAffected = sqldb.Execute(insertString, Entry);
            sqldb.Close();

            return rowsAffected;
        }

        //Modular Read Entry with EQUALS
        public List<T> Read<T> (string Table, string key, object value)
        {
            var parameters = new { Key = key, Value = value, Table = Table };

            string queryString = @"SELECT * FROM adcdb" + Table + " WHERE " + key + " = @Value";

            var toReturn = sqldb.Query<T>(queryString, parameters).ToList();

            return toReturn;
        }

        //Modular Delete Entry with EQUALS
        public bool Delete<T>(string Table, string key, object value)
        {
            return true;
        }

        //Return Full List
        public List<T> List<T>(string Table, int resultCount = 1000)
        {
            
            sqldb.Open();
            var toReturn = sqldb.Query<T>("SELECT * FROM " + Table).ToList();
            sqldb.Close();
            
            return toReturn;
        }

    }
}
