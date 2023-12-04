using ADC.Archive;
using ADC.Managers;
using ADC.Screens.NewUserScreen;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        Server server;

        string connectionString = Program.iniFile.Read("ConnectionString");

        object[] initialRoles;

        List<string> UsersBlueprint = new List<string>();
        List<string> RolesBlueprint = new List<string>();

        public DatabaseCrafter(SQLMaster parent)
        {
            sql = parent;
            connectionString = @connectionString.Replace("Initial Catalog=ADCDB;", "");
            //connectionString = @"Data Source=.\SQLExpress;User ID=sa;Password=password;";
            try
            {
                server = new Server(new ServerConnection(new SqlConnection(connectionString)));
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public void CheckDatabase()
        {
            

            if (server.Databases["ADCDB"] is null)
            {
                var createDatabaseBox = MessageBox.Show("Server does not have a database for this program. Generate?", "Warning", MessageBoxButtons.YesNo);

                if (createDatabaseBox == DialogResult.Yes)
                {
                    GenerateDatabase();
                }
            }

        }

        public void GenerateDatabase()
        {
            Program.loadingScreen.SetProgress(0);
            Program.loadingScreen.Show();

            Database db = new Database(server, "ADCDB");
            db.Create();

            Program.loadingScreen.SetProgress(5);

            System.Threading.Thread.Sleep(500);

            sql.ConnectToDatabase();

            Program.loadingScreen.SetProgress(10);

            GenerateBlueprintTable();

            Program.loadingScreen.SetProgress(25);

            GenerateRolesTable();

            Program.loadingScreen.SetProgress(60);

            GenerateUsersTable();

            Program.loadingScreen.SetProgress(90);

            CraftBlueprints();

            Program.loadingScreen.SetProgress(100);
            Program.loadingScreen.Close();
        }

        public void GenerateRolesTable()
        {
            Database db = server.Databases["ADCDB"];

            Table tb = new Table(db, "adcdbRoles");

            Column col = new Column(tb, "ID", DataType.Int);
            col.Identity = true;
            col.IdentitySeed = 1;
            col.IdentityIncrement = 1;
            tb.Columns.Add(col);

            col = new Column(tb, "Name", DataType.NVarCharMax);
            col.Nullable = false;
            tb.Columns.Add(col);

            col = new Column(tb, "CreatedBy", DataType.NVarCharMax);
            col.Nullable = false;
            tb.Columns.Add(col);

            col = new Column(tb, "CreatedDate", DataType.DateTime);
            col.Nullable = false;
            tb.Columns.Add(col);

            tb.Create();


            RolesBlueprint.Add("Name(" + DataType.NVarCharMax.Name + ")");
            RolesBlueprint.Add("CreatedBy(" + DataType.NVarCharMax.Name + ")");
            RolesBlueprint.Add("CreatedDate(" + DataType.DateTime.Name + ")");


            RoleGrimoire toAdd = new RoleGrimoire();
            
            toAdd.Name = "Admin";
            toAdd.CreatedBy = "Default";

            sql.Create("Roles", toAdd);

            toAdd.Name = "User";
            toAdd.CreatedBy = "Default";

            sql.Create("Roles", toAdd);

        }

        public void GenerateUsersTable()
        {
            Database db = server.Databases["ADCDB"];

            Table tb = new Table(db, "adcdbUsers");

            Column col = new Column(tb, "ID", DataType.Int);
            col.Identity = true;
            col.IdentitySeed = 1;
            col.IdentityIncrement = 1;
            tb.Columns.Add(col);

            col = new Column(tb, "Name", DataType.NVarCharMax);
            col.Nullable = false;
            tb.Columns.Add(col);

            col = new Column(tb, "EmployeeID", DataType.NVarCharMax);
            col.Nullable = false;
            tb.Columns.Add(col);

            col = new Column(tb, "Username", DataType.NVarCharMax);
            col.Nullable = false;
            tb.Columns.Add(col);

            col = new Column(tb, "Password", DataType.NVarCharMax);
            col.Nullable = false;
            tb.Columns.Add(col);

            col = new Column(tb, "Roles", DataType.NVarCharMax);
            col.Nullable = false;
            tb.Columns.Add(col);

            col = new Column(tb, "NewPasswordRequired", DataType.Bit);
            col.Nullable = false;
            tb.Columns.Add(col);

            col = new Column(tb, "CreatedDate", DataType.DateTime);
            col.Nullable = false;
            tb.Columns.Add(col);

            col = new Column(tb, "CreatedBy", DataType.NVarCharMax);
            col.Nullable = false;
            tb.Columns.Add(col);

            col = new Column(tb, "ModifiedDate", DataType.DateTime);
            col.Nullable = false;
            tb.Columns.Add(col);

            col = new Column(tb, "ModifiedBy", DataType.NVarCharMax);
            col.Nullable = false;
            tb.Columns.Add(col);

            col = new Column(tb, "LastLogin", DataType.DateTime);
            col.Nullable = false;
            tb.Columns.Add(col);

            col = new Column(tb, "Deactivated", DataType.Bit);
            col.Nullable = false;
            tb.Columns.Add(col);

            tb.Create();

            UsersBlueprint.Add("Name(" + DataType.NVarCharMax.Name + ")");
            UsersBlueprint.Add("EmployeeID(" + DataType.NVarCharMax.Name + ")");
            UsersBlueprint.Add("Username(" + DataType.NVarCharMax.Name + ")");
            UsersBlueprint.Add("Password(" + DataType.NVarCharMax.Name + ")");
            UsersBlueprint.Add("Roles(" + DataType.NVarCharMax.Name + ")");
            UsersBlueprint.Add("NewPasswordRequired(" + DataType.Bit.Name + ")");
            UsersBlueprint.Add("CreatedDate(" + DataType.DateTime.Name + ")");
            UsersBlueprint.Add("CreatedBy(" + DataType.NVarCharMax.Name + ")");
            UsersBlueprint.Add("ModifiedDate(" + DataType.DateTime.Name + ")");
            UsersBlueprint.Add("ModifiedBy(" + DataType.NVarCharMax.Name + ")");
            UsersBlueprint.Add("LastLogin(" + DataType.DateTime.Name + ")");
            UsersBlueprint.Add("Deactivated(" + DataType.Bit.Name + ")");

            SeedUser();
        }

        public void GenerateBlueprintTable()
        {
            Database db = server.Databases["ADCDB"];

            Table tb = new Table(db, "adcdbBlueprint");

            Column col = new Column(tb, "ID", DataType.Int);
            col.Identity = true;
            col.IdentitySeed = 1;
            col.IdentityIncrement = 1;
            tb.Columns.Add(col);

            col = new Column(tb, "TableName", DataType.NVarCharMax);
            col.Nullable = false;
            tb.Columns.Add(col);

            col = new Column(tb, "TableColumns", DataType.NVarCharMax);
            col.Nullable = false;
            tb.Columns.Add(col);

            col = new Column(tb, "RolesWithAccess", DataType.NVarCharMax);
            col.Nullable = false;
            tb.Columns.Add(col);

            tb.Create();
        }

        public void SeedUser()
        {
            NewUserScreen seedScreen = new NewUserScreen(true, sql);
            seedScreen.ShowDialog();
        }

        public void CraftBlueprints()
        {

            BlueprintGrimoire Users = new BlueprintGrimoire() { TableName = "Users", TableColumns = String.Join(";", UsersBlueprint), RolesWithAccess = "Admin" };
            BlueprintGrimoire Roles = new BlueprintGrimoire() { TableName = "Roles", TableColumns = String.Join(";", RolesBlueprint), RolesWithAccess = "Admin" };

            sql.Create("Blueprint", Users);
            sql.Create("Blueprint", Roles);

        }
    }
}
