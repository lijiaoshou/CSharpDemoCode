using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SonWinChangeFaWin
{
    public partial class SonWin : Form
    {
        public FaterWin.CallObject co;

        public SonWin()
        {
            InitializeComponent();
        }

        public SonWin(FaterWin.CallObject cov) : this()
        {
            this.co = cov;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //改变父窗体中的值
            co.ChangeSelText("子窗体给的值");
            this.Close();
            this.Dispose();
        }
    }
}
