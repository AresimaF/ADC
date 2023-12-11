using ADC.Archive;
using ADC.Managers;
using ADC.Screens.LoginScreen;
using ADC.Screens.NewPasswordScreen;
using ADC.Screens.RoleManagementScreen;
using ADC.Screens.UserManagementScreen;
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

        public List<object> OpenScreens { get; set; } = new List<object>();

        public MainScreen()
        {
            InitializeComponent();

            RefreshPermissions += new EventHandler(RemoteRefresh);

            //RefreshMenu();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewPasswordScreen newPassword = new NewPasswordScreen();
            newPassword.ShowDialog();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int loops = OpenScreens.Count;


            for (int i = 0; i < loops; i++)
            {
                Form screen = (Form)OpenScreens[0];
                
                screen.Close();
            }

            Program.LoggedInUser = null;
            Program.loginScreen.Show();
            Hide();
        }

        public void RefreshMenu()
        {
            List<BlueprintGrimoire> blueprintsRaw = Program.sqlMaster.List<BlueprintGrimoire>("Blueprint");
            Dictionary<string, BlueprintGrimoire> blueprintsIndexed = new Dictionary<string, BlueprintGrimoire>();

            foreach(BlueprintGrimoire blueprint in blueprintsRaw)
            {
                blueprintsIndexed.Add(blueprint.TableName, blueprint);
            }

            menuStripManagement.Enabled = false;

            if (LockCheck(blueprintsIndexed["Users"]))
            {
                menuStripManagementUsers.Visible = true;
                menuStripManagementUsers.Enabled = true;
                menuStripManagement.Enabled = true;
            }
            else
            {
                menuStripManagementUsers.Visible = false;
                menuStripManagementUsers.Enabled = false;
            }

            if (LockCheck(blueprintsIndexed["Roles"]))
            {
                menuStripManagementRoles.Visible = true;
                menuStripManagementRoles.Enabled = true;
                menuStripManagement.Enabled = true;
            }
            else
            {
                menuStripManagementRoles.Visible = false;
                menuStripManagementRoles.Enabled = false;
            }

            if (LockCheck(blueprintsIndexed["Modules"]))
            {
                menuStripManagementModules.Visible = true;
                menuStripManagementModules.Enabled = true;
                menuStripManagement.Enabled = true;
            }
            else
            {
                menuStripManagementModules.Visible = false;
                menuStripManagementModules.Enabled = false;
            }
            
            blueprintsIndexed.Remove("Users");
            blueprintsIndexed.Remove("Roles");
            blueprintsIndexed.Remove("Modules");


            foreach(KeyValuePair<string, BlueprintGrimoire> bp in blueprintsIndexed)
            {
                if (LockCheck(bp.Value))
                {
                    ToolStripMenuItem newItem = new ToolStripMenuItem();

                    newItem.Text = bp.Key;
                    newItem.Click += new EventHandler(OpenModule);

                    menuStripModules.DropDownItems.Add(newItem);
                }
            }

        }

        public void RemoteRefresh(object sender, EventArgs args)
        {
            RefreshMenu();
        }

        private bool LockCheck(BlueprintGrimoire blueprint)
        {
            string[] userRoles = Program.LoggedInUser.Roles.Split(',');

            foreach(string role in userRoles)
            {
                if (blueprint.RolesWithAccess.Contains(role) && role.Length > 1)
                {
                    return true;
                }
            }


            return false;
        }

        private void OpenModule(Object eventObject, EventArgs myEventArgs)
        {   
            ToolStripMenuItem sender = (ToolStripMenuItem)eventObject;

            //Open dynamic window for the particular table 
        }

        private void menuStripManagementUsers_Click(object sender, EventArgs e)
        {
            UserManagementScreen newScreen = new UserManagementScreen(this);

            OpenScreens.Add(newScreen);

            newScreen.Show();
        }

        private void menuStripManagementRoles_Click(object sender, EventArgs e)
        {
            RoleManagementScreen newScreen = new RoleManagementScreen(this);

            OpenScreens.Add(newScreen);

            newScreen.Show();
        }

        public event EventHandler RefreshPermissions;
    }
}
