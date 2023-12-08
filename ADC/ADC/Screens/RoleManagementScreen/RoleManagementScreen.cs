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

namespace ADC.Screens.RoleManagementScreen
{
    public partial class RoleManagementScreen : Form
    {
        private MainScreen parent;

        PropertyInfo[] properties = new RoleGrimoire().GetType().GetProperties();

        DataTable RoleList = new DataTable();

        public RoleManagementScreen(MainScreen parentScreen)
        {
            InitializeComponent();

            parent = parentScreen;

            this.FormClosing += new FormClosingEventHandler(OnFormClosing);

            GenerateTable();
        }

        public void OnFormClosing(object sender, FormClosingEventArgs args)
        {
            parent.OpenScreens.Remove(this);
        }

        public void GenerateTable()
        {


            foreach (PropertyInfo property in properties)
            {
                RoleList.Columns.Add(property.Name, property.PropertyType);
            }

            RefreshRoles();
        }

        public void RefreshRoles()
        {
            List<RoleGrimoire> rawList = Program.sqlMaster.List<RoleGrimoire>("Roles").ToList();


            RoleList = Program.dataConverter.ListToDataTable(rawList);

            RoleList.Columns["ID"].ReadOnly = true;
            RoleList.Columns["CreatedDate"].ReadOnly = true;
            RoleList.Columns["CreatedBy"].ReadOnly = true;


            dataRoles.DataSource = RoleList;
            dataRoles.Refresh();
        }
    }
}
