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
            // Determine attendance status from radio buttons
            string status = "";
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

            // Get the selected date
            DateTime selectedDate = dateTimePicker1.Value;

            // Get logged-in user ID from Form3
            int userID = Form3.LoggedInUserID;
            if (userID == -1)
            {
                MessageBox.Show("No user is logged in.");
                return;
            }

            // Connection string for SQL Server
            string connectionString = "Data Source=BILLAAA\\SA; Initial Catalog=MANDAK;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("TambahAttendance", conn))
                    {
                        // Specify that this is a stored procedure call
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters safely to avoid SQL injection
                        cmd.Parameters.AddWithValue("@ID_Karyawan", userID);
                        cmd.Parameters.AddWithValue("@Tanggal", selectedDate);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@Waktu_Masuk", DateTime.Now);

                        // Execute the stored procedure
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Attendance recorded successfully!");
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL errors such as user not found
                MessageBox.Show("Database error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                // Handle other possible errors
                MessageBox.Show("Error: " + ex.Message);
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
