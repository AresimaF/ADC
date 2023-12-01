using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.PeerToPeer.Collaboration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ADC.Crafters;
using ADC.Archive;
using Microsoft.Data.SqlClient;
using Dapper;

namespace ADC.Managers
{
    internal class SQLMaster
    {
        DatabaseCrafter builder;

        string connectionString = @"Data Source=.\SQLExpress;Initial Catalog=ADCDB;User ID=sa;Password=password";



        //Test the connection, show error if fail.
        public bool ConnectionTest()
        {
            SqlConnection sqldb = new SqlConnection(connectionString);

            builder = new DatabaseCrafter(this);

            try
            {
                sqldb.Open();
                sqldb.Close();
            }
            catch (SqlException e)
            {

                builder.CheckDatabase();
                return false;

            }


            return true;
        }

        //Pulls from user table by username, then checks password against entry.
        public UserGrimoire AttemptLogin(string username, string password)
        {
            SqlConnection sqldb = new SqlConnection(connectionString);
            CryptMaster crypto = new CryptMaster();
            UserGrimoire user;
            var parameters = new { QueryUsername = username };

            sqldb.Open();
            var query = sqldb.Query<UserGrimoire>("SELECT * FROM adcdbUsers WHERE UserName=@QueryUsername AND Deactivated = 0;", parameters).ToList();
            sqldb.Close();

            if (query.Count < 1)
            {
                MessageBox.Show("Username or password incorrect, login failed.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            else
            {
                user = query[0];
            }

            bool passCheck = crypto.verifyPassword(password, user.Password);
            //string passCheck = password;

            if (user.UserName.Length > 0 & passCheck)
            {
                return user;
            }
            else
            {
                MessageBox.Show("Username or password incorrect, login failed.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }


        }

        //Apply password hashing and attempt to add the new user information.
        public bool AddUser(UserGrimoire user)
        {
            var insertString = "INSERT INTO adcdbUsers (UserName, Password, Name, EmployeeID, Roles, NewPasswordRequired, LastLogin, DateCreated, CreatedBy, DateDeactivated, " +
                "DeactivatedBy, Deactivated)" +
                " VALUES (@UserName, @Password, @Name, @EmployeeID, @Roles, @NewPasswordRequired, @LastLogin, @DateCreated, @CreatedBy, @DateDeactivated, @DeactivatedBy, @Deactivated)";

            UserGrimoire toAdd = user;
            SqlConnection sqldb = new SqlConnection(connectionString);
            CryptMaster crypto = new CryptMaster();

            string hashedPassword = crypto.standardSaltHash(user.Password);
            toAdd.Password = hashedPassword;            

            sqldb.Open();
            int rowsAffected = sqldb.Execute(insertString, toAdd);
            sqldb.Close();

            if (rowsAffected == 1)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Data entry error. Number of rows affected: " + rowsAffected, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //Check if a username appears in the user data table. Return true if yes.
        public bool UsernameInUse(string username)
        {

            var parameters = new { QueryUsername = username };

            SqlConnection sqldb = new SqlConnection(connectionString);

            sqldb.Open();
            var query = sqldb.Query<UserGrimoire>("SELECT * FROM adcdbUsers WHERE UserName=@QueryUsername AND Deactivated = 0;", parameters).ToList();
            sqldb.Close();

            if (query.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Update a user entry to given user.
        public bool UpdateUser(UserGrimoire user)
        {
            var updateString = @"UPDATE adcdbUsers SET UserName = @UserName, Password = @Password, Name = @Name, EmployeeID = @EmployeeID, Roles = @Roles, " +
                "NewPasswordRequired = @NewPasswordRequired, LastLogin = @LastLogin, DateCreated = @DateCreated, CreatedBy = @CreatedBy, DateDeactivated = @DateDeactivated," +
                "DeactivatedBy = @DeactivatedBy, Deactivated = @Deactivated WHERE ID = @ID";

            UserGrimoire toUpdate = user;
            SqlConnection sqldb = new SqlConnection(connectionString);          

            sqldb.Open();
            int rowsAffected = sqldb.Execute(updateString, toUpdate);
            sqldb.Close();

            if (rowsAffected == 1)
            {
                return true;
            }
            else
            {
                MessageBox.Show("User Update Error. Number of rows affected: " + rowsAffected, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //Query for single user directly. Property should never be passed as a user-editable string.
        public UserGrimoire QueryUser(string property, object value)
        {
            var parameters = new Dictionary<string, object>() { ["QueryValue"] = value, ["QueryProperty"] = property };

            SqlConnection sqldb = new SqlConnection(connectionString);
            UserGrimoire user;

            sqldb.Open();
            var query = sqldb.Query<UserGrimoire>("SELECT * FROM adcdbUsers WHERE @QueryProperty=@QueryValue AND Deactivated = 0;", parameters).ToList();
            sqldb.Close();


            user = query[0];
            return user;

        }


        //Return a list of users
        public DataSet GetUserData()
        {

            string query = "SELECT * FROM adcdbUsers WHERE Deactivated = 0";

            SqlConnection sqldb = new SqlConnection(connectionString);

            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, sqldb);

            DataSet toReturn = new DataSet();

            sqldb.Open();
            dataAdapter.Fill(toReturn, "Users_table");
            sqldb.Close();

            return toReturn;
        }

        //Overload for a filtered list of users
        public DataSet GetUserData(string filter, string filterby)
        {

            string query = "SELECT * FROM adcdbUsers WHERE Deactivated = 0 AND @QueryFilterBy LIKE '%' + @QueryFilter + '%';";

            SqlConnection sqldb = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, sqldb);

            command.Parameters.AddWithValue("@QueryFilterBy", filterby);
            command.Parameters.AddWithValue("@QueryFilter", filter);

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

            DataSet toReturn = new DataSet();

            sqldb.Open();
            dataAdapter.Fill(toReturn, "Users_table");
            sqldb.Close();

            return toReturn;
        }

        public Dictionary<string, string> GetRolesList()
        {

            string query = "SELECT * FROM adcdbRoles;";

            SqlConnection sqldb = new SqlConnection(connectionString);

            sqldb.Open();
            var dict = sqldb.Query(query).ToDictionary(
                row => (string)row.Title,
                row => (string)row.AccessCodes
                );
            return dict;
        }

        //Apply password hashing and attempt to add the new user information.
        public bool AddRole(object obj)
        {

            var insertString = "INSERT INTO adcdbRoles (Title, AccessCodes, CreatedBy, DateCreated) VALUES (@Title, @AccessCodes, @CreatedBy, @DateCreated)";
            object toAdd = obj;

            SqlConnection sqldb = new SqlConnection(connectionString);

            sqldb.Open();
            int rowsAffected = sqldb.Execute(insertString, toAdd);
            sqldb.Close();

            if (rowsAffected == 0)
            {
                return false;
            }
            else if (rowsAffected == 1)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Data entry error. Number of rows affected: " + rowsAffected, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        public Dictionary<string, string> GetAccessList()
        {
            SqlConnection sqldb = new SqlConnection(connectionString);

            Dictionary<string, string> toReturn = new Dictionary<string, string>();

            sqldb.Open();
            var query = sqldb.Query<EntryGrimoire>("SELECT * FROM adcdbAccess").ToList();
            sqldb.Close();

            foreach (EntryGrimoire obj in query)
            {
                toReturn[obj.Feature] = obj.RolesWithAccess;
            }

            return toReturn;
        }

        public bool AddAccess(string feature, string rolesWithAccess)
        {
            Dictionary<string, string> alreadyExists = GetAccessList();

            if (alreadyExists.ContainsKey(feature))
            {
                MessageBox.Show("Tried to add access entry that already exists.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            EntryGrimoire toAdd = new EntryGrimoire();

            toAdd.Feature = feature;
            toAdd.RolesWithAccess = rolesWithAccess;

            SqlConnection sqldb = new SqlConnection(connectionString);

            var insertString = "INSERT INTO adcdbAccess (Feature, RolesWithAccess) VALUES (@Feature, @RolesWithAccess)";

            sqldb.Open();
            int rowsAffected = sqldb.Execute(insertString, toAdd);
            sqldb.Close();

            if (rowsAffected == 0)
            {
                return false;
            }
            else if (rowsAffected == 1)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Data entry error. Number of rows affected: " + rowsAffected, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //make sure to add in seeded access for user management and debug screen
        }
    }
}
