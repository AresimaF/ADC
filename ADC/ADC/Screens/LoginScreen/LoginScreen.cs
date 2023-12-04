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

namespace ADC.Screens.LoginScreen
{
    public partial class LoginScreen : Form
    {
        public LoginScreen()
        {
            InitializeComponent();

            textPassword.Text = "";
            textUsername.Text = "";

            Program.LoggedInUser = null;
        }

        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            AttemptLogin();
        }

        private void textPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                AttemptLogin();
            }
        }

        private void textUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                AttemptLogin();
            }
        }

        public void AttemptLogin()
        {

         
            List<UserGrimoire> userList = Program.sqlMaster.Read<UserGrimoire>("Users", "Username", textUsername.Text);
            UserGrimoire checkAgainst;

            if (userList.Count == 0)
            {
                LoginError();
                return;
            }
            else
            {
                checkAgainst = userList[0];
            }

            if (Program.cryptMaster.verifyPassword(textPassword.Text, checkAgainst.Password) == true)
            {
                Program.LoggedInUser = checkAgainst;
                Program.LoggedInUser.LastLogin = DateTime.Now;

                Program.sqlMaster.Update("Users", Program.LoggedInUser);

                if (Program.LoggedInUser.NewPasswordRequired)
                {
                    var newPassword = new NewPasswordScreen.NewPasswordScreen(true);
                    newPassword.ShowDialog();
                }

                try
                {
                    if (Program.mainScreen is null)
                    {

                    }
                    else
                    {
                        Program.mainScreen.Show();
                    }
                    
                }
                catch (Exception e)
                {

                }
                

                textUsername.Text = "";
                textPassword.Text = "";

                this.Hide();
            }
            else
            {
                LoginError();
            }

        }

        public void LoginError()
        {
            MessageBox.Show("Username or Password is incorrect.", "", MessageBoxButtons.OK);
            textPassword.Text = "";
        }

        private void LoginScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
