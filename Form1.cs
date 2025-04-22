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
            // You can use the role to enable/disable buttons or customize the form behavior here.
            if (userRole == "employee")
            {
                // Example: Disable certain controls for employees
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                // Further customization based on role...
            }
            else if (userRole == "admin")
            {
                // Example: Enable controls for admins
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                // Further customization based on role...
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
                status = "Tidak Hadir";
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
            string connectionString = "Data Source=LAPTOP-PFIH6R5H\\GALIHMAULANA;Initial Catalog=MANDAK;Integrated Security=True";

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

                    // Insert attendance into the Kehadiran table
                    string query = "INSERT INTO Kehadiran (ID_Karyawan, Tanggal, Status) VALUES (@userID, @selectedDate, @status)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Add parameters to avoid SQL injection
                    cmd.Parameters.AddWithValue("@userID", userID);
                    cmd.Parameters.AddWithValue("@selectedDate", selectedDate);
                    cmd.Parameters.AddWithValue("@status", status);

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

            this.Hide();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Handle date picker changes if needed
        }
    }
}
