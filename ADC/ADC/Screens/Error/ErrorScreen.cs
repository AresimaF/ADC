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
        public ErrorScreen(Exception e)
        {
            InitializeComponent();

            textErrorMessage.Text = e.Message;
        }
    }
}
