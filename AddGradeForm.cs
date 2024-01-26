﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace individualProject440
{
    public partial class AddGradeForm : Form
    {
        public AddGradeForm()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            bool validInput = false;
            string grade = "L";
            int studentID = 0;
            int courseID = 0;

            //check if text boxes are empty
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text))
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
                    grade = textBox3.Text.ToString();

                    validInput = true;
                }
                
            }
            
            //for checking if record exists
            int registrationID = 0;


            if (grade.Equals("A") || grade.Equals("B") || grade.Equals("C") || grade.Equals("D") || grade.Equals("F") && validInput == true)
            {
                validInput = true;
            }
            else
            {
                Console.WriteLine("ERROR: Invalid input");
                validInput = false;
                //would create an invalid form
                errorForm invaCred = new errorForm("Cannot Add Record. Grade Input Invalid.");
                invaCred.Show();
            }


            if (validInput == true)
            {
                //now we add the grade
                
                string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";

                MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

                try
                {
                    conn.Open();

                    string sql = "INSERT INTO kim440registration (studentID, courseCRN, registrationGrade) VALUES (@studentID, @courseCRN, @registrationGrade)";
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                    Console.WriteLine(studentID);
                    Console.WriteLine(courseID);
                    Console.WriteLine(grade);

                    cmd.Parameters.AddWithValue("@studentID", studentID);
                    cmd.Parameters.AddWithValue("@courseCRN", courseID);
                    cmd.Parameters.AddWithValue("@registrationGrade", grade);

                    MySqlDataReader myReader = cmd.ExecuteReader();

                    myReader.Close();

                }
                catch (Exception e1)
                {
                    Console.WriteLine(e1.ToString());
                }
                conn.Close();

                
                
                //we check if the record added or not
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
                    errorForm invaCred = new errorForm("Invalid Credential: Cannot Add Data. Please Check Course ID and Student ID");
                    invaCred.Show();
                }
                else
                {
                    //inform user of success
                    errorForm success = new errorForm("Record Added");
                    success.Show();
                }
                
                
                
                //set textboxes blank
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }
        }
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
    }  
}
