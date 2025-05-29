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
        private readonly string userRole;

        public Form2(string role)
        {
            InitializeComponent();
            userRole = role;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Display logged-in username
            label4.Text = $"Selamat Datang, {Form3.LoggedInUserName}";

            // Configure UI based on user role
            if (userRole == "employee")
            {
                button2.Visible = false; // Hide "Presensi Check"
                button3.Visible = false; // Hide "Karyawan List"
                button4.Visible = false; // Hide admin feature button
            }
            else if (userRole == "admin")
            {
                // Show admin-specific features
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Navigate to Form1 (Presensi input)
            var presensiForm = new Form1(userRole);
            presensiForm.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (userRole == "employee")
            {
                MessageBox.Show("This feature is not available for employees.");
                return;
            }

            // Navigate to Form5 (Attendance list)
            var attendanceForm = new Form5(userRole);
            attendanceForm.Show();
            this.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (userRole == "employee")
            {
                MessageBox.Show("This feature is not available for employees.");
                return;
            }

            // Navigate to Form4 (Karyawan management)
            var karyawanForm = new Form4(userRole);
            karyawanForm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Placeholder for future feature
            MessageBox.Show("This feature is not yet available.");
        }

        private void label4_Click(object sender, EventArgs e)
        {
            // Refresh display of logged-in username when label clicked
            if (!string.IsNullOrEmpty(Form3.LoggedInUserName))
            {
                label4.Text = Form3.LoggedInUserName;
            }
            else
            {
                label4.Text = "No user logged in";
            }
        }
    }
}
