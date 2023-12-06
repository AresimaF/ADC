using ADC.Archive;
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

            dataUsers.DataSource = UserList;
            dataUsers.Refresh();
        }

        public void SaveChanges()
        {
            // TODO:
            // -Add a conversion to the converterMaster for object > row and vice versa
            // -Run a check between current database user list and edited list, update as changes found
            // -Refresh datagrid

        }

        

        public void OnFormClosing(object sender, FormClosingEventArgs args)
        {
            parent.OpenScreens.Remove(this);
        }


        
    }
}
