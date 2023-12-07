using ADC.Archive;
using KellermanSoftware.CompareNetObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADC.Screens.UserManagementScreen
{
    public partial class UserManagementScreen : Form
    {
        DataTable UserList = new DataTable();

        MainScreen parent;

        PropertyInfo[] properties = new UserGrimoire().GetType().GetProperties();

        public UserManagementScreen(MainScreen parentScreen)
        {
            InitializeComponent();

            parent = parentScreen;

            this.FormClosing += new FormClosingEventHandler(OnFormClosing);

            GenerateTable();
        }

        public void GenerateTable()
        {
            

            foreach (PropertyInfo property in properties)
            {
                UserList.Columns.Add(property.Name, property.PropertyType);
            }

            RefreshUsers();
        }

        public void RefreshUsers()
        {
            List<UserGrimoire> rawList = Program.sqlMaster.List<UserGrimoire>("Users").ToList();

            
            UserList = Program.dataConverter.ListToDataTable(rawList);

            UserList.Columns["ID"].ReadOnly = true;

            dataUsers.DataSource = UserList;
            dataUsers.Refresh();
        }

        

        

        public void OnFormClosing(object sender, FormClosingEventArgs args)
        {
            parent.OpenScreens.Remove(this);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            
            
            List<UserGrimoire> tableList = Program.dataConverter.DataTableToList<UserGrimoire>(UserList);

            List<UserGrimoire> databaseList = Program.sqlMaster.List<UserGrimoire>("Users");

            int changeCount = 0;

            CompareLogic compare = new CompareLogic()
            {
                Config = new ComparisonConfig()
                {
                    TypesToIgnore = new List<Type>()
                    {
                        typeof(DateTime)
                    },
                    MembersToIgnore = new List<string>
                    {
                        "ID"
                    }
                    //add other configurations
                }
            }; 

            foreach (UserGrimoire user in tableList)
            {
                UserGrimoire checkAgainst = databaseList.Where(x => x.Username == user.Username).FirstOrDefault();

                ComparisonResult result = compare.Compare(user, checkAgainst);

                user.ID = checkAgainst.ID;

                if (!result.AreEqual)
                {
                    user.ModifiedDate = DateTime.Now;
                    user.ModifiedBy = Program.LoggedInUser.Username;
                    int x = Program.sqlMaster.Update("Users", user);
                    changeCount++;
                }
            }
            
            UserList.AcceptChanges();
            RefreshUsers();

            MessageBox.Show("Changes saved! Total users changed: " + changeCount.ToString(), "", MessageBoxButtons.OK);
        }

        private void buttonUndo_Click(object sender, EventArgs e)
        {
            UserList.RejectChanges();

            RefreshUsers();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            NewUserScreen.NewUserScreen newUser = new NewUserScreen.NewUserScreen(false);
            newUser.Show();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshUsers();
        }
    }
}
