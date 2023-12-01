using ADC.Archive;
using ADC.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.PeerToPeer.Collaboration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADC
{
    public partial class MainScreen : Form
    {
       
        public UserGrimoire LoggedInUser { get; set; }

        public Dictionary<string, string> RoleList { get; set; }
        public Dictionary<string, string> AccessList { get; set; }

        //A list of all menu objects, used for role-based activation/deactivation. Populated in PopulateMenus();
        public List<object> MenuOptions { get; set; }
        public List<object> OpenWindows { get; set; }

        UserManager userManager;
        UserManagerViewer userManagerViewer;

        public MainScreen()
        {
            InitializeComponent();

            this.Text = "ADC";
            OpenWindows = new List<object>();

            Login();
        }

        private void dashboardExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void dashboardManageUsers_Click(object sender, EventArgs e)
        {
            if (userManager != null)
            {
                userManager.Close();
            }

            if (userManagerViewer != null)
            {
                userManagerViewer.Close();
            }


            userManagerViewer = new UserManagerViewer();
            userManager = new UserManager(userManagerViewer);

            userManager.MdiParent = this;
            userManagerViewer.MdiParent = this;

            userManagerViewer.Show();
            OpenWindows.Add(userManager);

            userManagerViewer.UpdateTable();

            userManager.CloseUserManagementEditor += close_UserManagementViewer;
            userManagerViewer.CloseUserManagementViewer += close_UserManagementEditor;
        }


        private void close_UserManagementViewer(object sender, EventArgs e)
        {
            if (userManagerViewer.FormOpen == true)
            {
                OpenWindows.Remove(userManagerViewer);
                userManagerViewer.Close();
            }
        }

        private void close_UserManagementEditor(object sender, EventArgs e)
        {
            if (userManager.FormOpen == true)
            {
                OpenWindows.Remove(userManager);
                userManager.Close();
            }
        }

        private void PopulateMenus()
        {
            string[] userRoles = LoggedInUser.Roles.Split(',');

            SQLMaster sql = new SQLMaster();

            AccessList = sql.GetAccessList();

            //Add Menu Options to List
            MenuOptions = new List<object>();
            MenuOptions.Add(manageUsersToolStripMenuItem);
            MenuOptions.Add(debugScreenToolStripMenuItem);

            //Go through all menu options and activate/deactivate per user role.
            foreach (object obj in MenuOptions)
            {
                foreach (string role in userRoles)
                {
                    if (role == "Admin" || AccessList[obj.GetType().GetProperty("Text").GetValue(obj).ToString()].Contains(role))
                    {
                        obj.GetType().GetProperty("Enabled").SetValue(obj, true);
                        break;
                    }
                    else
                    {
                        obj.GetType().GetProperty("Enabled").SetValue(obj, false);
                    }
                }
            }
        }

        private void DepopulateMenus()
        {
            //Add Menu Options to List
            MenuOptions = new List<object>();
            MenuOptions.Add(manageUsersToolStripMenuItem);

            //Go through all menu options and activate/deactivate per user role.
            foreach (object obj in MenuOptions)
            {
                obj.GetType().GetProperty("Enabled").SetValue(obj, false);
            }
        }

        //Login Dialogue
        private void Login()
        {
            LoggedInUser = null;

            DepopulateMenus();

            Authentication auth = new Authentication();
            var authResult = auth.ShowDialog();

            //Check if login succeeded or if window was closed
            if (authResult == DialogResult.OK & auth.LoggedInUser != null)
            {
                LoggedInUser = auth.LoggedInUser;
            }
            else
            {
                Environment.Exit(0);
            }

            //Get the list of roles;
            SQLMaster sqldb = new SQLMaster();

            RoleList = sqldb.GetRolesList();

            //Populate menu options
            PopulateMenus();

            //Get the access list
            AccessList = new Dictionary<string, string>();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Close all windows, re-login.
            foreach (object obj in OpenWindows)
            {
                obj.GetType().GetMethod("Close").Invoke(obj, null);
            }
            OpenWindows.Clear();

            this.Hide();
            Login();
            this.Show();
        }

        private void debugScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public static void InitialUser()
        {
            AddUser initialUser = new AddUser(true);
            initialUser.Text = "Create Initial User";
            initialUser.ShowDialog();
        }
    }
    }
}
