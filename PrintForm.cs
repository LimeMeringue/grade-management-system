using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Drawing;
using System.Data;
using ExcelDataReader;
using System.IO;

namespace individualProject440
{
    public partial class PrintForm : Form
    {
        public PrintForm()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            
          
             //input validation for studentID
            int parsedValue;
            if (!int.TryParse(textBox1.Text, out parsedValue)) //checks if ID is an integer
            {
                errorForm error = new errorForm("Invalid Student ID");
                error.Show();
                textBox1.Text = "";
            }
            else if (Convert.ToInt32(textBox1.Text) < 1) //checks if ID is a positive integer
            {
                errorForm error = new errorForm("Invalid Student ID");
                error.Show();
                textBox1.Text = "";
            }
            else
            {
                int studentID = Int32.Parse(textBox1.Text);
                string stuGPA = "";
                string name = "";
                bool exists = false;
                    //get student info 

                    string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";
                    DataTable myTable = new DataTable();

                    MySqlConnection conn = new MySqlConnection(connStr);

                    try
                    {
                        conn.Open();

                        string sql = "SELECT * FROM kim440student WHERE studentID = @studentID";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@studentID", studentID);

                        MySqlDataReader myReader = cmd.ExecuteReader();
                        //checks if student exists
                        if (myReader.HasRows)
                        {
                        exists = true;
                            while (myReader.Read())
                            {

                                stuGPA = (myReader["studentGPA"].ToString());
                                name = (myReader["studentName"].ToString());

                            }

                        }
                        else
                        {

                            conn.Close();
                            Console.WriteLine("Invalid Student");
                            errorForm error = new errorForm("Invalid Student ID");
                            error.Show();
                            textBox1.Text = "";
                        }

                        

                        myReader.Close();
 

                        Console.WriteLine($"Name: {name}, GPA: {stuGPA}");


                    }
                    catch (Exception e1)
                    {
                        Console.WriteLine(e1.ToString());
                    }
                    conn.Close();

                
                if(exists == true)
                {


                    //prints transcript based on student info + records
                    DataTable dt = studentRegistrationRecords(studentID);
                    createTranscript(name, studentID.ToString(), stuGPA, dt);

                    errorForm success = new errorForm("Transcript Printed");
                    success.Show();
                    textBox1.Text = "";
                }

                


            }
        }
        public DataTable studentRegistrationRecords(int studentID)
        {
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";
            DataTable myTable = new DataTable();

            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                //SELECT kim440registration.courseCRN, kim440course.coursePrefix, kim440course.courseNumber, kim440course.courseHour, kim440course.courseYear, kim440course.courseSemester, kim440registration.registrationGrade FROM kim440registration LEFT JOIN kim440course ON kim440registration.courseCRN=kim440course.courseCRN WHERE studentID = 1;
                string sql = "SELECT kim440registration.courseCRN, kim440course.coursePrefix, kim440course.courseNumber, kim440course.courseHour, kim440course.courseYear, kim440course.courseSemester, kim440registration.registrationGrade FROM kim440registration LEFT JOIN kim440course ON kim440registration.courseCRN=kim440course.courseCRN WHERE studentID = @studentID";
                //string sql = "SELECT * FROM kim440registration WHERE studentID = @studentID";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@studentID", studentID);

                MySqlDataAdapter myAdapter = new MySqlDataAdapter(cmd);

                myAdapter.Fill(myTable);
                Console.WriteLine("Table is ready.");

                foreach (DataColumn column in myTable.Columns)
                {
                    Console.Write($"{column.ColumnName}\t");
                }
                Console.WriteLine();

                // Print data
                foreach (DataRow row in myTable.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        Console.Write($"{item}\t");
                    }
                    Console.WriteLine();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            conn.Close();
            return myTable;
        }
        public void createTranscript(string name, string id, string gpa, DataTable dt)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header()
                        .Text(name + ", ID: " + id + ", GPA: " + gpa)
                        .Bold().FontSize(20);

                    page.Content()
                        .PaddingVertical(4, Unit.Centimetre)
                        //gives us: courseCRN,coursePrefix,courseNumber,courseHour,courseYear,courseSemester,registrationGrade
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });
                            table.Header(header =>
                            {
                                header.Cell().AlignLeft().Text("Course CRN");
                                header.Cell().AlignLeft().Text("Course Prefix");
                                header.Cell().AlignLeft().Text("Course Number");
                                header.Cell().AlignLeft().Text("Hours");
                                header.Cell().AlignLeft().Text("Year");
                                header.Cell().AlignLeft().Text("Semester");
                                header.Cell().AlignLeft().Text("Grade Earned");


                            });

                            foreach (DataRow row in dt.Rows)
                            {

                                table.Cell().Text(row[0].ToString());
                                table.Cell().Text(row[1].ToString());
                                table.Cell().Text(row[2].ToString());
                                table.Cell().Text(row[3].ToString());
                                table.Cell().Text(row[4].ToString());
                                table.Cell().Text(row[5].ToString());
                                table.Cell().Text(row[6].ToString());
                            }

                        });


                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });

                });

            });
            //the pdf pops up in the default web browser
            document.GeneratePdfAndShow();

        }
        public bool checkStudentExistance(int studentID)
        {
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";
            DataTable myTable = new DataTable();

            MySqlConnection conn = new MySqlConnection(connStr);

            bool exists = false;
            try
            {
                conn.Open();

                string sql = "SELECT * FROM kim440student WHERE studentID = @studentID";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@studentID", studentID);

                MySqlDataReader myReader = cmd.ExecuteReader();
                if (!(myReader.HasRows))
                {
                    conn.Close();
                    Console.WriteLine("Invalid Student");
                    
                }
                else
                {
                    exists = true;
                }


                myReader.Close();
                

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            conn.Close();

            return exists;
        }
    }
}
