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

namespace ADC.Screens.NewPasswordScreen
{
    public partial class NewPasswordScreen : Form
    {
        public NewPasswordScreen(bool isRequired = false)
        {
            InitializeComponent();

            if (isRequired)
            {
                ControlBox = false;
            }
            else
            {
                ControlBox = true;
            }
        }

        private void buttonShowHideOldPassword_Click(object sender, EventArgs e)
        {
            textOldPassword.UseSystemPasswordChar = !textOldPassword.UseSystemPasswordChar;
        }

        private void buttonShowHideNewPassword_Click(object sender, EventArgs e)
        {
            textNewPassword.UseSystemPasswordChar = !textNewPassword.UseSystemPasswordChar;
            textRepeatNewPassword.UseSystemPasswordChar = !textRepeatNewPassword.UseSystemPasswordChar;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textNewPassword.Text != textRepeatNewPassword.Text)
            {
                MessageBox.Show("New passwords do not match.", "", MessageBoxButtons.OK);
            }
            else if (textNewPassword.Text.Length < 8)
            {
                MessageBox.Show("New passwordsmust be at least 8 characters long.", "", MessageBoxButtons.OK);
            }
            else if (Program.cryptMaster.verifyPassword(textOldPassword.Text, Program.LoggedInUser.Password) == false)
            {
                MessageBox.Show("Old password incorrect.", "", MessageBoxButtons.OK);
            }
            else
            {
                UserGrimoire updatedUser = Program.LoggedInUser;

                updatedUser.Password = Program.cryptMaster.hashPassword(textNewPassword.Text);
                updatedUser.NewPasswordRequired = false;

                int result = Program.sqlMaster.Update("Users", updatedUser);

                if (result == 1)
                {
                    MessageBox.Show("Password changed successfully!", "", MessageBoxButtons.OK);
                    Program.LoggedInUser = updatedUser;
                    Close();
                }
                else
                {
                    MessageBox.Show("Error updating user password. Rows affected: " + result.ToString(), "Error", MessageBoxButtons.OK);
                    Close();
                }
            }
        }
    }
}
