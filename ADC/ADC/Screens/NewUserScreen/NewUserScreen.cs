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
        }
    }
}
