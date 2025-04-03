using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SY_LABEXAM
{
    public partial class StudentPage_Individual : Form
    {
        private int studentId;

        // Constructor accepting studentId
        public StudentPage_Individual(int studentId)
        {
            InitializeComponent();
            this.studentId = studentId;  // Store the studentId passed from the previous form
        }

        // Load event for the form
        private void StudentPage_Individual_Load(object sender, EventArgs e)
        {
            // Now use the studentId to fetch student details
            LoadStudentDetails();
        }

        // Method to load student details from the database
        private void LoadStudentDetails()
        {
            string connectionString = "Server=localhost;Database=studentinfodb;User Id=Jiwel;Password=JiwelSy00!";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT studentId, firstName, middleName, lastName, houseNo, brgyName, municipality, province, region, country, birthdate, age, studContactNo, emailAddress, guardianFirstName, guardianLastName, hobbies, nickname, courseId, yearId " +
                                   "FROM StudentRecordTB WHERE studentId = @studentId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@studentId", studentId);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Populate labels with the fetched data in the correct order
                        labelStudentId.Text = reader["studentId"].ToString();
                        labelFirstName.Text = reader["firstName"].ToString();
                        labelMiddleName.Text = reader["middleName"].ToString();
                        labelLastName.Text = reader["lastName"].ToString();
                        labelHouseNo.Text = reader["houseNo"].ToString();
                        labelBrgyName.Text = reader["brgyName"].ToString();
                        labelMunicipality.Text = reader["municipality"].ToString();
                        labelProvince.Text = reader["province"].ToString();
                        labelRegion.Text = reader["region"].ToString();
                        labelCountry.Text = reader["country"].ToString();
                        labelBirthdate.Text = reader["birthdate"].ToString();
                        labelAge.Text = reader["age"].ToString();
                        labelContact.Text = reader["studContactNo"].ToString();
                        labelEmail.Text = reader["emailAddress"].ToString();
                        labelGuardian.Text = $"{reader["guardianFirstName"]} {reader["guardianLastName"]}";
                        labelHobbies.Text = reader["hobbies"].ToString();
                        labelNickname.Text = reader["nickname"].ToString();
                        labelCourse.Text = reader["courseId"].ToString(); // Assuming courseId is displayed as it is (can map to a name if needed)
                        labelYear.Text = reader["yearId"].ToString(); // Assuming yearId is displayed as it is (can map to a name if needed)
                    }
                    else
                    {
                        MessageBox.Show("Student not found!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        // OK button event
        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();  // Close the form when the OK button is clicked
        }
    }
}
