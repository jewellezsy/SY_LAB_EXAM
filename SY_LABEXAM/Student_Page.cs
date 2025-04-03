using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // Use MySQL Client
using SY_LABEXAM; // Namespace reference for StudentPage_Individual

namespace SY_LABEXAM
{
    public partial class Student_Page : Form
    {
        // Corrected connection string for MySQL
        private string connectionString = "Server=localhost;Database=studentinfodb;User Id=Jiwel;Password=JiwelSy00!";

        public Student_Page()
        {
            InitializeComponent();
            LoadStudentRecords();
        }

        private void LoadStudentRecords()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT studentId, CONCAT(firstName, ' ', middleName, ' ', lastName) AS FullName FROM StudentRecordTB";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Add VIEW Button column if not exists
                if (!dataGridViewStudents.Columns.Contains("ViewButton"))
                {
                    DataGridViewButtonColumn viewButton = new DataGridViewButtonColumn
                    {
                        Name = "ViewButton",
                        HeaderText = "Action",
                        Text = "VIEW",
                        UseColumnTextForButtonValue = true
                    };
                    dataGridViewStudents.Columns.Add(viewButton);
                }

                dataGridViewStudents.DataSource = dt;
            }
        }


        private void dataGridViewStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewStudents.Columns["ViewButton"].Index && e.RowIndex >= 0)
            {
                int studentId = Convert.ToInt32(dataGridViewStudents.Rows[e.RowIndex].Cells["studentId"].Value);
                StudentPage_Individual studentPage = new StudentPage_Individual(studentId);
                studentPage.Show();
            }
        }

        private void Student_Page_Load(object sender, EventArgs e)
        {
            // You can load any other data when the form is loaded, if needed
        }
    }
}
