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
        public string connectionString = @"Data Source=.\SQLExpress;User ID=sa;Password=password";

        Server server;

        SQLMaster dapper;

        object seedRoleAdmin;
        object seedRoleUser;

        public DatabaseCrafter(SQLMaster sqlInput)
        {

            dapper = sqlInput;

            try
            {
                server = new Server(new ServerConnection(new SqlConnection(connectionString)));
            }
            catch (Exception e)
            {

                MessageBox.Show("Failed to connect to database!" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


            seedRoleAdmin = new
            {
                ID = 0,
                Title = "Admin",
                AccessCodes = "",
                CreatedBy = "Default",
                DateCreated = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss K")
            };

            seedRoleUser = new
            {
                ID = 1,
                Title = "User",
                AccessCodes = "",
                CreatedBy = "Default",
                DateCreated = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss K")
            };

        }

        //Check if appropriate database exists.
        public bool CheckDatabase()
        {
            if (server.Databases["ADCDB"] is null)
            {
                var buildDatabase = MessageBox.Show("Database 'adcdb' does not exist." + "\n" + "Would you like to craft a database?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None);

                if (buildDatabase == DialogResult.Yes)
                {
                    CreateDatabase();
                    CheckTables();
                }

                return false;
            }
            else
            {

                CheckTables();
                return true;
            }
        }

        public bool CheckTables()
        {
            if (!server.Databases["ADCDB"].Tables.Contains("adcbdUsers") || !server.Databases["ADCDB"].Tables.Contains("adcdbRoles") || !server.Databases["ADCDB"].Tables.Contains("adcdbAccess") || !server.Databases["ADCDB"].Tables.Contains("adcdbBlueprints"))
            {
                var buildTables = MessageBox.Show("Minimum necessary tables do not exist." + "\n" + "Would you like to craft necessary tables?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None);

                if (buildTables == DialogResult.Yes)
                {
                    CreateTables();

                }
                return false;
            }
            else
            {
                return true;
            }
        }

        public void CreateDatabase()
        {
            Database db = new Database(server, "ADCDB");
            db.Create();
            MessageBox.Show("Database 'ADCDB' created.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void CreateTables()
        {

            Database db = server.Databases["ADCDB"];

            List<string> tablesAdded = new List<string>();

            #region User Table

            if (!server.Databases["ADCDB"].Tables.Contains("adcdbUsers"))
            {

                Table tb = new Table(db, "adcdbUsers");

                Column col1;
                col1 = new Column(tb, "ID", DataType.Int);
                col1.Identity = true;
                col1.IdentitySeed = 1;
                col1.IdentityIncrement = 1;
                tb.Columns.Add(col1);

                Column col2;
                col2 = new Column(tb, "UserName", DataType.NVarCharMax);
                col2.Collation = "Latin1_General_CI_AS";
                col2.Nullable = false;
                tb.Columns.Add(col2);

                Column col3;
                col3 = new Column(tb, "Password", DataType.NVarCharMax);
                col3.Nullable = false;
                tb.Columns.Add(col3);

                Column col4;
                col4 = new Column(tb, "Name", DataType.NVarCharMax);
                col4.Nullable = false;
                tb.Columns.Add(col4);

                Column col5;
                col5 = new Column(tb, "EmployeeID", DataType.NVarChar(50));
                col5.Nullable = true;
                tb.Columns.Add(col5);

                Column col6;
                col6 = new Column(tb, "Roles", DataType.NVarCharMax);
                col6.Nullable = false;
                tb.Columns.Add(col6);

                Column col7;
                col7 = new Column(tb, "NewPasswordRequired", DataType.Bit);
                col7.Nullable = false;
                tb.Columns.Add(col7);

                Column col8;
                col8 = new Column(tb, "LastLogin", DataType.NVarCharMax);
                col8.Nullable = true;
                tb.Columns.Add(col8);

                Column col9;
                col9 = new Column(tb, "DateCreated", DataType.NVarCharMax);
                col9.Nullable = false;
                tb.Columns.Add(col9);

                Column col10;
                col10 = new Column(tb, "CreatedBy", DataType.NVarCharMax);
                col10.Nullable = false;
                tb.Columns.Add(col10);

                Column col11;
                col11 = new Column(tb, "DateDeactivated", DataType.NVarCharMax);
                col11.Nullable = true;
                tb.Columns.Add(col11);

                Column col12;
                col12 = new Column(tb, "DeactivatedBy", DataType.NVarCharMax);
                col12.Nullable = true;
                tb.Columns.Add(col12);

                Column col13;
                col13 = new Column(tb, "Deactivated", DataType.Bit);
                col13.Nullable = true;
                tb.Columns.Add(col13);

                tb.Create();


                MainScreen.InitialUser();

                tablesAdded.Add("Users");

            }

            #endregion

            #region Roles Table

            if (!server.Databases["ADCDB"].Tables.Contains("adcdbRoles"))
            {

                Table tb2 = new Table(db, "adcdbRoles");

                Column col21;
                col21 = new Column(tb2, "ID", DataType.Int);
                col21.Identity = true;
                col21.IdentitySeed = 1;
                col21.IdentityIncrement = 1;
                tb2.Columns.Add(col21);

                Column col22;
                col22 = new Column(tb2, "Title", DataType.NVarCharMax);
                col22.Collation = "Latin1_General_CI_AS";
                col22.Nullable = false;
                tb2.Columns.Add(col22);

                Column col23;
                col23 = new Column(tb2, "AccessCodes", DataType.NVarCharMax);
                col23.Nullable = true;
                tb2.Columns.Add(col23);

                Column col24;
                col24 = new Column(tb2, "CreatedBy", DataType.NVarCharMax);
                col24.Nullable = false;
                tb2.Columns.Add(col24);

                Column col25;
                col25 = new Column(tb2, "DateCreated", DataType.NVarCharMax);
                col25.Nullable = false;
                tb2.Columns.Add(col25);

                tb2.Create();

                dapper.AddRole(seedRoleAdmin);
                dapper.AddRole(seedRoleUser);

                tablesAdded.Add("Roles");

            }

            #endregion

            #region Access Table

            if (!server.Databases["ADCDB"].Tables.Contains("adcdbAccess"))
            {

                Table tb3 = new Table(db, "adcdbAccess");

                Column col31;
                col31 = new Column(tb3, "ID", DataType.Int);
                col31.Identity = true;
                col31.IdentitySeed = 1;
                col31.IdentityIncrement = 1;
                tb3.Columns.Add(col31);

                Column col32;
                col32 = new Column(tb3, "Feature", DataType.NVarCharMax);
                col32.Collation = "Latin1_General_CI_AS";
                col32.Nullable = false;
                tb3.Columns.Add(col32);

                Column col33;
                col33 = new Column(tb3, "RolesWithAccess", DataType.NVarCharMax);
                col33.Nullable = true;
                tb3.Columns.Add(col33);

                tb3.Create();

                tablesAdded.Add("Access");


                dapper.AddAccess("Manage Users", "Admin");

                dapper.AddAccess("Debug Screen", "Admin");

            }

            #endregion Blueprint Table

            #region 

            if (!server.Databases["ADCDB"].Tables.Contains("adcdbBlueprints"))
            {

                Table tb4 = new Table(db, "adcdbBlueprints");

                Column col41;
                col41 = new Column(tb4, "ID", DataType.Int);
                col41.Identity = true;
                col41.IdentitySeed = 1;
                col41.IdentityIncrement = 1;
                tb4.Columns.Add(col41);

                Column col42;
                col42 = new Column(tb4, "Tables", DataType.NVarCharMax);
                col42.Nullable = false;
                tb4.Columns.Add(col42);

                Column col43;
                col43 = new Column(tb4, "TablesColumns", DataType.NVarCharMax);
                col43.Nullable = true;
                tb4.Columns.Add(col43);

                tb4.Create();

                tablesAdded.Add("adcdbBlueprints");
            }

                #endregion

                MessageBox.Show("Initial tables [" + String.Join(", ", tablesAdded.ToArray()) + "] created.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
