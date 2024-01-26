using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace individualProject440
{
    public partial class errorForm : Form
    {
        public errorForm(string error)
        {
            InitializeComponent();
            errorLabel.Text = error;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
