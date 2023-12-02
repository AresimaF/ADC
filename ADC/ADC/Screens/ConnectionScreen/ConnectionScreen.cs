using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADC.Screens.ConnectionScreen
{
    public partial class ConnectionScreen : Form
    {
        public ConnectionScreen()
        {
            InitializeComponent();
            textPassword.UseSystemPasswordChar = true;
        }

        private void buttonShowHidePassword_Click(object sender, EventArgs e)
        {
            textPassword.UseSystemPasswordChar = !textPassword.UseSystemPasswordChar;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //Program.iniFile.Write("ConnectionString", @"Data Source=.\SQLExpress;Initial Catalog=ADCDB;User ID=sa;Password=password");
            Program.iniFile.Write("ConnectionString", @"Data Source=" + textDataSource.Text + ";Initial Catalog=ADCDB;User ID=" + textUserID.Text + ";Password=" + textPassword.Text);
            Close();
        }
    }
}
