using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pabdproject
{
    public partial class Form1 : Form
    {
        private string userRole;

        // Modified constructor to accept user role as a parameter
        public Form1(string role)
        {
            InitializeComponent();
            userRole = role;  // Store the role for later use
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Use the role to enable/disable buttons or customize the form behavior here.
            if (userRole == "employee")
            {
                // Allow employees to select attendance status (don't disable radio buttons)
                radioButton1.Enabled = true;  // Enable "Hadir"
                radioButton2.Enabled = true;  // Enable "Tidak Hadir"

                // You can customize other controls if needed for employees
                // For example, if there's an admin-only feature, you can disable that
                // e.g., button3.Enabled = false; or hide certain controls
            }
            else if (userRole == "admin")
            {
                // Enable controls for admins (if needed, keep existing settings for admins)
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                // Further customization for admins...
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // Handle radio button changes if needed
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // Handle radio button changes if needed
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string status = "";
            // Check if any radio button is selected
            if (radioButton1.Checked)
            {
                status = "Hadir";
            }
            else if (radioButton2.Checked)
            {
                status = "Izin";
            }
            else
            {
                MessageBox.Show("Please select attendance status.");
                return;
            }

            // Ensure the date is selected
            DateTime selectedDate = dateTimePicker1.Value;

            // Use the static variable LoggedInUserID to get the logged-in user ID
            int userID = Form3.LoggedInUserID; // Access the logged-in user ID from Form3

            // Check if the user is logged in
            if (userID == -1)
            {
                MessageBox.Show("No user is logged in.");
                return;
            }

            // SQL connection string
            string connectionString = "Data Source=NITROSFAQIH\\SQLEXPRESS;Initial Catalog=MANDAK;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Check if the user exists in the Karyawan table
                    string checkUserQuery = "SELECT COUNT(*) FROM Karyawan WHERE ID_Karyawan = @userID";
                    SqlCommand checkCmd = new SqlCommand(checkUserQuery, conn);
                    checkCmd.Parameters.AddWithValue("@userID", userID);

                    int userExists = (int)checkCmd.ExecuteScalar();

                    // If the user doesn't exist, show an error message
                    if (userExists == 0)
                    {
                        MessageBox.Show("User not found. Please ensure the user exists in the Karyawan table.");
                        return;
                    }

                    // Insert attendance into the Kehadiran table with the date they took the attendance
                    string query = "INSERT INTO Kehadiran (ID_Karyawan, Tanggal, Status, Waktu_Masuk) VALUES (@userID, @selectedDate, @status, @WaktuMasuk)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Add parameters to avoid SQL injection
                    cmd.Parameters.AddWithValue("@userID", userID);
                    cmd.Parameters.AddWithValue("@selectedDate", selectedDate);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@WaktuMasuk", DateTime.Now); // The current date and time of attendance registration

                    // Execute the insert query
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Attendance recorded successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Pass the role when opening Form2
            Form2 form2 = new Form2(userRole);
            form2.Show();

            this.Hide(); // Hide Form1
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

    }
}
