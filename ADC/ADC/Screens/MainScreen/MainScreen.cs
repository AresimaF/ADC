using ADC.Archive;
using ADC.Managers;
using ADC.Screens.LoginScreen;
using ADC.Screens.NewPasswordScreen;
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
        public MainScreen()
        {
            InitializeComponent();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewPasswordScreen newPassword = new NewPasswordScreen();
            newPassword.ShowDialog();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.LoggedInUser = null;
            Program.loginScreen.Show();
            Hide();
        }
    }
}
