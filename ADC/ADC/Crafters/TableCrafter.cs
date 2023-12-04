using ADC.Archive;
using ADC.Managers;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADC.Crafters
{
    internal class TableCrafter
    {
        SQLMaster sql;
        Database db;
        Server server;

        Dictionary<string, bool> emptyNullables = new Dictionary<string, bool>();

        string connectionString = Program.iniFile.Read("ConnectionString");

        public TableCrafter()
        {


            connectionString = @connectionString.Replace("Initial Catalog=ADCDB;", "");

            try
            {
                server = new Server(new ServerConnection(new SqlConnection(connectionString)));
                db = server.Databases["ADCDB"];
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }


        }

        public void CraftTable(string Name, Dictionary<string, DataType> Columns)
        {

            Table tb = new Table(db, "adcdb" + Name);

            List<string> ColumnData = new List<string>();

            Column IDColumn = new Column(tb, "ID", DataType.Int);
            IDColumn.Identity = true;
            IDColumn.IdentitySeed = 1;
            IDColumn.IdentityIncrement = 1;
            tb.Columns.Add(IDColumn);

            foreach (KeyValuePair<string, DataType> Column in Columns)
            {
                Column col = new Column(tb, Column.Key, Column.Value);
                col.Nullable = false;

                tb.Columns.Add(col);

                ColumnData.Add(Column.Key + "(" + Column.Value.Name + ")");
            }

            BlueprintGrimoire blueprint = new BlueprintGrimoire() { TableName = Name, TableColumns = String.Join(";", ColumnData), RolesWithAccess = "Admin" };

            tb.Create();
            Program.sqlMaster.Create("Blueprint", blueprint);


        }

        public void CraftTable(string Name, Dictionary<string, DataType> Columns, Dictionary<string, bool> ColumnNullables)
        {
            Table tb = new Table(db, "adcdb" + Name);

            List<string> ColumnData = new List<string>();

            Column IDColumn = new Column(tb, "ID", DataType.Int);
            IDColumn.Identity = true;
            IDColumn.IdentitySeed = 1;
            IDColumn.IdentityIncrement = 1;
            tb.Columns.Add(IDColumn);

            foreach (KeyValuePair<string, DataType> Column in Columns)
            {
                Column col = new Column(tb, Column.Key, Column.Value);
                
                if (ColumnNullables.ContainsKey(Column.Key))
                {
                    col.Nullable = ColumnNullables[Column.Key];
                }
                else
                {
                    col.Nullable = false;
                }
                

                tb.Columns.Add(col);

                ColumnData.Add(Column.Key + "(" + Column.Value.Name + ")");
            }

            BlueprintGrimoire blueprint = new BlueprintGrimoire() { TableName = Name, TableColumns = String.Join(";", ColumnData), RolesWithAccess = "Admin" };

            tb.Create();
            Program.sqlMaster.Create("Blueprint", blueprint);
        }
    }
}
