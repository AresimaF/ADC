using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADC.Screens.Error
{
    public partial class ErrorScreen : Form
    {
        string[] windowTitles = { "Error", "Fatal Error" };

        public ErrorScreen(Exception e, bool isFatal = false)
        {
            InitializeComponent();

            Text = (isFatal ? windowTitles[1] : windowTitles[0]);
            textErrorMessage.Text = e.Message;

            
        }

        private void buttonContinue_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }
    }
}
