using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pabdproject
{
    public partial class Form2 : Form
    {
        private string userRole;

        public Form2(string role)
        {
            InitializeComponent();
            userRole = role;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Disable or hide buttons based on the role
            if (userRole == "employee")
            {
                button2.Enabled = false; // Disables button2 (Check Presensi)
                button3.Enabled = false; // Disables button3 (Daftar Karyawan)
                button4.Enabled = false; // Optionally, disable button4 if it's not needed
            }
            else if (userRole == "admin")
            {
                // Enable buttons for admin
                button2.Enabled = true;  // Check Presensi
                button3.Enabled = true;  // Daftar Karyawan
                button4.Enabled = true;  // Optionally, enable button4 for admin
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Your logic for button4
            // You can either leave this empty or add the desired functionality
            MessageBox.Show("This feature is not yet available.");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Define the role (for example, based on user data or fixed value)
            string role = "Admin";  // or you can use dynamic role based on your logic

            // Create a new instance of Form1 and pass the role
            Form1 form1 = new Form1(role);

            // Show Form1
            form1.Show();

            // Hide Form2
            this.Hide();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Check if the user is an admin, else show a message
            if (userRole == "employee")
            {
                MessageBox.Show("This feature is not available for employees.");
            }
            else
            {
                // Create a new instance of Form5 (Check Presensi)
                Form5 form5 = new Form5();

                // Show Form5
                form5.Show();

                // Optionally, you can hide Form2 if you don't want it visible after opening Form5
                this.Hide();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            // Check if the user is an admin, else show a message
            if (userRole == "employee")
            {
                MessageBox.Show("This feature is not available for employees.");
            }
            else
            {
                // Create a new instance of Form4 (Daftar Karyawan)
                Form4 form4 = new Form4();

                // Show Form4
                form4.Show();

                // Optionally, you can hide Form2 if you don't want it visible after opening Form4
                this.Hide();
            }
        }
    }
}
