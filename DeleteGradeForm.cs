using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace individualProject440
{
    public partial class DeleteGradeForm : Form
    {
        public DeleteGradeForm()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            
            bool validInput = false;
            int studentID = 0;
            int courseID = 0;

            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                errorForm invaCred = new errorForm("Please fill out all required information");
                invaCred.Show();
                validInput = false;
            }
            else
            {
                //method to check if the integer values are integers and positive
                if (checkIfInt())
                {
                    studentID = Int32.Parse(textBox1.Text);
                    courseID = Int32.Parse(textBox2.Text);

                    validInput = true;
                }

            }


            int registrationID = 0;
            //check if grade exists

            if (validInput == true)
            {
                string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";

                MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

                try
                {
                    conn.Open();

                    string sql = "SELECT * FROM kim440registration WHERE studentID = @studentID AND courseCRN = @courseCRN";
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@studentID", studentID);
                    cmd.Parameters.AddWithValue("@courseCRN", courseID);

                    MySqlDataReader myReader = cmd.ExecuteReader();

                    while (myReader.Read())
                    {
                          registrationID = int.Parse(myReader["registrationID"].ToString());

                    }

                    myReader.Close();

                }
                catch (Exception e1)
                {
                    Console.WriteLine(e1.ToString());
                }
                conn.Close();

                if (registrationID == 0)
                {
                    errorForm invaCred = new errorForm("Record Does Not Exist");
                    invaCred.Show();
                }
                else
                {
                    try
                    {
                        conn.Open();

                        string sql = "DELETE FROM kim440registration WHERE studentID = @studentID AND courseCRN = @courseCRN";
                        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                        cmd.Parameters.AddWithValue("@studentID", studentID);
                        cmd.Parameters.AddWithValue("@courseCRN", courseID);

                        MySqlDataReader myReader = cmd.ExecuteReader();

                        myReader.Close();

                    }
                    catch (Exception e1)
                    {
                        Console.WriteLine(e1.ToString());
                    }
                    conn.Close();

                    errorForm success = new errorForm("Record Deleted");
                    success.Show();

                }
            }

            textBox1.Text = "";
            textBox2.Text = "";
            

        }
        //input validation
        public bool checkIfInt()
        {
            int parsedValue;
            if (!int.TryParse(textBox1.Text, out parsedValue)) //checks if ID is an integer
            {
                errorForm error = new errorForm("Invalid Student ID");
                error.Show();
                return false;

            }
            else if (Convert.ToInt32(textBox1.Text) < 1) //checks if ID is a positive integer
            {
                errorForm error = new errorForm("Invalid Student ID");
                error.Show();
                return false;
            }

            if (!int.TryParse(textBox2.Text, out parsedValue)) //checks if CRN is an integer
            {
                errorForm error = new errorForm("Invalid CRN");
                error.Show();
                return false;
            }
            else if (Convert.ToInt32(textBox2.Text) < 1) //checks if CRN is a positive integer
            {
                errorForm error = new errorForm("Invalid CRN");
                error.Show();
                return false;
            }

            return true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
