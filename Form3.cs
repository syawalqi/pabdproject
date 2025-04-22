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
        }

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

            string connectionString = "Data Source=LAPTOP-PFIH6R5H\\GALIHMAULANA;Initial Catalog=MANDAK;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // SQL query to get the ID_Karyawan for the logged-in user
                    string query = "SELECT ID_Karyawan FROM Karyawan WHERE Nama = @username AND Passwd = @password";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    // Execute the query and get the user ID
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        LoggedInUserID = (int)result; // Store the logged-in user's ID

                        MessageBox.Show("Login successful!");

                        // Open Form2 and hide the login form (Form3)
                        Form2 form2 = new Form2();
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
