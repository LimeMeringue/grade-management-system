using ExcelDataReader;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace individualProject440
{
    public partial class ImportForm : Form
    {
        public ImportForm()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            folderLabel.Text = "None";
            filesLabel.Text = "";
            textBox2.Text = "";
            allowImportLabel.Text = "false";

            using (var folder = new FolderBrowserDialog())
            {
                DialogResult result = folder.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folder.SelectedPath))
                {
                    folderLabel.Text = folder.SelectedPath;
                    files = Directory.GetFiles(folder.SelectedPath);

                    if (files.Length == 0)
                    {
                        errorForm noFiles = new errorForm("No Files Found");
                        noFiles.Show();
                    }
                    else
                    {
                        for (int i = 0; i < files.Length; i++)
                        {
                            if (i == 0)
                            {
                                filesLabel.Text = files[i];
                                textBox2.Text = files[i];
                            }
                            else
                            {
                                filesLabel.Text = filesLabel.Text + Environment.NewLine + files[i];
                                textBox2.Text = textBox2.Text + Environment.NewLine + files[i];
                            }


                            Console.WriteLine($"File : {files[i]}, number {i}");
                        }
                        allowImportLabel.Text = "true";
                    }


                }
            }
        }

        //importButton
        private void button1_Click(object sender, EventArgs e)
        {
            if(allowImportLabel.Text == "true")
            {
                for (int i = 0; i < files.Length; i++)
                {

                    string crnString = getCourseCrn(files[i]);  
                   

                    if (Convert.ToInt32(crnString) < 1) //if crnString == 0 no course found
                    {
                        errorForm noClass = new errorForm("Invalid file: " + files[i]);
                        noClass.Show();

                    }
                    else if (i == files.Length - 1)
                    {
                        importGradeFile(files[i], crnString);
                        errorForm error = new errorForm("Files Imported");
                        error.Show();
                    }
                    else //only display confirmation on last message
                    {
                        importGradeFile(files[i], crnString);

                    }
                }

            }
            else
            {
                errorForm error = new errorForm("No Folder Selected");
                error.Show();
            }

            folderLabel.Text = "None";
            filesLabel.Text = "";
            textBox2.Text = "";
            allowImportLabel.Text = "false";
        }

        public void importGradeFile(string fileName, string crnString)
        {
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {

                    //importing our excel sheet as a dataSet
                    var data = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });

                    //just want the table from the excel sheet
                    DataTable students = data.Tables[0];

                    foreach (DataColumn column in students.Columns)
                    {
                        Console.WriteLine($"Column Name: {column.ColumnName}");
                    }


                    foreach (DataRow row in students.Rows)
                    {
 
                        string name = (string)row["Name"];
                        string studentID = row["ID"].ToString();
                        string regGrade = (string)row["Grade"];
                        int crn = Convert.ToInt32(crnString);

                        Console.WriteLine($"ID: {studentID}, gradeLetter: {regGrade}, crn: {crn}");

                        addStudentInfo(Convert.ToInt32(studentID), crn, regGrade, fileName);

                    }
                }

            }
        }

        public void addStudentInfo(int studentID, int courseID, string grade, string fileName)
        {
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            int registrationID = 0;
            //adds garde record
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

            //checks if the record was added 
            try
            {
                conn.Open();

                string sql = "SELECT * FROM kim440registration WHERE studentID = @studentID AND courseCRN = @courseCRN";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@studentID", studentID);
                cmd.Parameters.AddWithValue("@courseCRN", courseID);
                cmd.Parameters.AddWithValue("@registrationGrade", grade);

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
                
                errorForm invaCred = new errorForm("Invalid Data: Could not import file "+fileName);
                invaCred.Show();
            }
        }

        string[] files = null;
        public string getCourseCrn(string fileN)
        {
            //getting indexes to isolate file name
            int startIndex = fileN.LastIndexOf("\\");
            int endIndex = fileN.LastIndexOf(".");
            int length = endIndex - startIndex - 1;

            fileN = fileN.Substring(startIndex + 1, length);
            //fileN is now in the format of: coursePrefix courseNumber year semester
            //for example: CSC 190 2021 Sprng

            //separating the individual components
            string semester = fileN.Substring(fileN.LastIndexOf(" ") + 1);
            fileN = fileN.Substring(0, fileN.Length - (semester.Length + 1));

            string year = fileN.Substring(fileN.LastIndexOf(" "));
            fileN = fileN.Substring(0, fileN.Length - year.Length);

            string courseN = fileN.Substring(fileN.LastIndexOf(" "));
            fileN = fileN.Substring(0, fileN.Length - courseN.Length);

            string prefix = fileN;

            //input validation
            int parsedValue;
            if (!int.TryParse(courseN, out parsedValue)) //checks if ID is an integer
            {
                errorForm error = new errorForm("Invalid File Name");
                error.Show();
            }
            else if (!int.TryParse(year, out parsedValue))
            {
                errorForm error = new errorForm("Invalid File Name");
                error.Show();
            }
            else
            {
                int courseNint = Convert.ToInt32(courseN);
                int yearInt = Convert.ToInt32(year);

                string result = getCourseCRNHelper(prefix, courseNint, yearInt, semester);
                Console.WriteLine($"RESULT CRN: {result}");

                return result;
            }
            return "0";

        }

        //gets course number from info in the file name
        public string getCourseCRNHelper(string coursePrefix, int courseNumber, int courseYear, string courseSemester)
        {
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";
            string courseCRN = "0";
            int courseInt = 0;
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {
                conn.Open();

                string sql = "SELECT * FROM kim440course WHERE coursePrefix = @coursePrefix AND courseNumber = @courseNumber AND courseYear = @courseYear AND courseSemester = @courseSemester";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@coursePrefix", coursePrefix);
                cmd.Parameters.AddWithValue("@courseNumber", courseNumber);
                cmd.Parameters.AddWithValue("@courseYear", courseYear);
                cmd.Parameters.AddWithValue("@courseSemester", courseSemester);

                MySqlDataReader myReader = cmd.ExecuteReader();
                if (!(myReader.HasRows))
                {
                    Console.WriteLine("No course found");
                    myReader.Close();
                    conn.Close();
                    return courseCRN;
                }

                while (myReader.Read())
                {
                    courseInt = int.Parse(myReader["courseCRN"].ToString());

                }

                myReader.Close();

                courseCRN = courseInt.ToString();
                Console.WriteLine($"CRN from query: {courseCRN}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            conn.Close();
            return courseCRN;
        }

    }
}
