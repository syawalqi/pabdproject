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
            // Tampilkan nama user yang login
            label4.Text = "Selamat Datang, " + Form3.LoggedInUserName;

            // Atur tampilan berdasarkan role
            if (userRole == "employee")
            {
                button2.Visible = false; // Presensi check - hilang
                button3.Visible = false; // Karyawan list - hilang
                button4.Visible = false; // Fitur admin - hilang
            }
            else if (userRole == "admin")
            {
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not yet available.");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Navigate to Form1 (Presensi input)
            Form1 form1 = new Form1(userRole);  // 👈 Pass role here
            form1.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (userRole == "employee")
            {
                MessageBox.Show("This feature is not available for employees.");
            }
            else
            {
                Form5 form5 = new Form5(userRole);  // 👈 Pass role here
                form5.Show();
                this.Hide();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (userRole == "employee")
            {
                MessageBox.Show("This feature is not available for employees.");
            }
            else
            {
                // Pass the user role correctly to Form4
                Form4 form4 = new Form4(userRole);  // 👈 Pass role here
                form4.Show();
                this.Hide();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            // Check if the logged-in user name is available
            if (!string.IsNullOrEmpty(Form3.LoggedInUserName))
            {
                // Display the logged-in user's name in label3
                label4.Text = "" + Form3.LoggedInUserName;
            }
            else
            {
                // Handle case where no user is logged in (optional)
                label4.Text = "No user logged in";
            }
        }


    }
}
