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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddGradeForm addGradeForm = new AddGradeForm();
            addGradeForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditGradeForm editGradeForm = new EditGradeForm();
            editGradeForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteGradeForm deleteGradeForm = new DeleteGradeForm();
            deleteGradeForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            ImportForm importForm = new ImportForm();
            importForm.Show();
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            PrintForm printForm = new PrintForm();
            printForm.Show();
        }
    }
}
