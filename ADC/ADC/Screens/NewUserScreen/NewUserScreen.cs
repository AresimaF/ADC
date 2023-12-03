using ADC.Archive;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADC.Screens.NewUserScreen
{
    public partial class NewUserScreen : Form
    {
        UserGrimoire currentUser;
        List<RoleGrimoire> roles;

        public NewUserScreen(bool isSeedUser)
        {
            InitializeComponent();

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
            roles = Program.sqlMaster.List<RoleGrimoire>("adcdbRoles");
            listRoles.Items.Clear();

            foreach (RoleGrimoire role in roles)
            {
                listRoles.Items.Add(role.Name);
            }

        }

        private void SeedUser()
        {
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
            currentUser = new UserGrimoire();

            buttonCancel.Enabled = false;
            textEmployeeID.Enabled = false;
            textName.Enabled = false;
            listRoles.Enabled = false;

            LoadValues();

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

        private void buttonCreateUser_Click(object sender, EventArgs e)
        {
            currentUser.Roles = listRoles.CheckedItems.ToString();

            int x = 0;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            currentUser = new UserGrimoire();

            listRoles.Items.Clear();

            this.Close();
        }
    }
}
