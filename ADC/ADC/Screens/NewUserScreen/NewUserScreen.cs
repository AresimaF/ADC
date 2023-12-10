using ADC.Archive;
using ADC.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ADC.Screens.NewUserScreen
{
    public partial class NewUserScreen : Form
    {
        UserGrimoire currentUser;
        List<RoleGrimoire> roles;
        SQLMaster sql;
        MainScreen parent;

        public NewUserScreen(bool isSeedUser, MainScreen parentScreen)
        {
            InitializeComponent();

            sql = Program.sqlMaster;

            parent = parentScreen;

            PopulateRoleList();

            if (isSeedUser)
            {
                SeedUser();
            }
            else
            {
                NormalUser();
            }

            this.parent = parent;
        }

        public NewUserScreen(bool isSeedUser, SQLMaster inputSQL)
        {
            InitializeComponent();

            sql = inputSQL;

            PopulateRoleList();

            if (isSeedUser)
            {
                SeedUser();
            }
            else
            {
                NormalUser();
            }
        }

        public void PopulateRoleList()
        {
            roles = sql.List<RoleGrimoire>("Roles");
            listRoles.Items.Clear();

            foreach (RoleGrimoire role in roles)
            {
                listRoles.Items.Add(role.Name);
            }

            listRoles.Refresh();
        }

        private void SeedUser()
        {
            ControlBox = false;
            
            currentUser = new UserGrimoire();

            currentUser.ID = 0;
            currentUser.Username = "admin";
            currentUser.Password = "password";
            currentUser.Roles = "Admin, User";
            currentUser.Name = "Admin";
            currentUser.CreatedBy = "Default";
            currentUser.ModifiedBy = "Default";
            currentUser.NewPasswordRequired = false;

            buttonCancel.Enabled = false;
            textEmployeeID.Enabled = false;
            textName.Enabled = false;
            listRoles.Enabled = false;

            LoadValues();           

        }

        private void NormalUser()
        {
            ControlBox = true;
            
            currentUser = new UserGrimoire();

            currentUser.CreatedBy = Program.LoggedInUser.Username;
            currentUser.ModifiedBy = Program.LoggedInUser.Username;

            buttonCancel.Enabled = true;
            textEmployeeID.Enabled = true;
            textName.Enabled = true;
            listRoles.Enabled = true;

            LoadValues();

            this.FormClosing += new FormClosingEventHandler(OnFormClosing);

        }

        private void LoadValues()
        {
            textUsername.Text = currentUser.Username;
            textPassword.Text = currentUser.Password;
            textName.Text = currentUser.Name;
            textEmployeeID.Text = currentUser.EmployeeID;

            //Load roles here
            for (int i = 0; i <= (listRoles.Items.Count - 1); i++)
            {
                if (currentUser.Roles.Contains(listRoles.Items[i].ToString()))
                {
                    listRoles.SetItemChecked(i, true);
                }
                else
                {
                    listRoles.SetItemChecked(i, false);
                }
            }
        }

        private void HashPassword()
        {
            currentUser.Password = Program.cryptMaster.hashPassword(currentUser.Password); 
        }

        private void buttonCreateUser_Click(object sender, EventArgs e)
        {
            HashPassword();

            List<string> userRoles = new List<string>();

            listRoles.Refresh();

            foreach (object item in listRoles.CheckedItems)
            {
                userRoles.Add(item.ToString());
            }

            int rowsAffected = sql.Create<UserGrimoire>("Users", currentUser);

            if (rowsAffected != 1)
            {
                Program.ErrorHandler(new Exception("Error creating user. Rows Affected: " + rowsAffected.ToString()));
            }
            else
            {
                ClearAndClose();
            }
            
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            ClearAndClose();
        }

        private void ClearAndClose()
        {
            currentUser = new UserGrimoire();

            listRoles.Items.Clear();

            this.Close();
        }

        public void OnFormClosing(object sender, FormClosingEventArgs args)
        {
            parent.OpenScreens.Remove(this);
        }
    }
}
