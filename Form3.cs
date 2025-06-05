using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace pabdproject
{
    public partial class Form3: Form
    {
        public Form3()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
        }

        public static string LoggedInUserName { get; set; }  // Static property to store the username

        private void Username_Click(object sender, EventArgs e)
        {
            ToolTip tip = new ToolTip();
            tip.SetToolTip(txtusername, "Enter your name here");
        }


        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        public static int LoggedInUserID = -1;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtusername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            // 🔐 Check if password is at least 8 characters
            if (password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.");
                return;
            }

            string connectionString = "Data Source=NITROSFAQIH\\SQLEXPRESS;Initial Catalog=MANDAK;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // SQL query to get the ID_Karyawan and Role for the logged-in user
                    string query = "SELECT ID_Karyawan, Role FROM Karyawan WHERE Nama = @username AND Passwd = @password";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    // Execute the query and get the user ID and role
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        LoggedInUserID = (int)reader["ID_Karyawan"]; // Store the logged-in user's ID
                        string userRole = reader["Role"].ToString(); // Get the user's role from the database
                        LoggedInUserName = username;  // Set the logged-in user's name

                        MessageBox.Show("Login successful!");

                        // Open Form2 and pass the user's role to it
                        Form2 form2 = new Form2(userRole); // Pass the role here
                        form2.Show();
                        this.Hide(); // or this.Close(); if you want to fully exit Form3
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


    }
}
