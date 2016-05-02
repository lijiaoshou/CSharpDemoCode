using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeedDevelpTools
{
    public partial class UserControlTest : UserControl
    {
        public UserControlTest()
        {
            InitializeComponent();
        }

        private void UserControlTest_Load(object sender, EventArgs e)
        {

            Form1 form = new Form1();
            form.TopLevel = false;
            this.Controls.Add(form);
            form.Show();
        }
    }
}
