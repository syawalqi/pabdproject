﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pabdproject
{
    public partial class Form1 : Form
    {
        string connect = ""; // Deklarasikan variabel untuk menyimpan string koneksi
        private string userRole;

        // Modified constructor to accept user role as a parameter
        public Form1(string role)
        {
            InitializeComponent();
            userRole = role;  // Store the role for later use
            connect = Koneksi.GetConnectionString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Batasi tanggal maksimal dan minimal
            dateTimePicker1.MinDate = DateTime.Today.AddYears(-5);
            dateTimePicker1.MaxDate = DateTime.Today.AddYears(1);
            dateTimePicker1.Format = DateTimePickerFormat.Short;

            if (userRole == "employee")
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;

                // Batasi karyawan hanya bisa mengisi presensi hari ini
                dateTimePicker1.Value = DateTime.Today;
                dateTimePicker1.Enabled = false;
            }
            else if (userRole == "admin")
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;

                dateTimePicker1.Value = DateTime.Today;
                dateTimePicker1.Enabled = false;
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

            

            try
            {
                using (SqlConnection conn = new SqlConnection(connect))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("TambahAttendance", conn))
                    {
                        // Specify that this is a stored procedure call
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Inside try
                        selectedDate = selectedDate.Date;
                        DateTime waktuNow = DateTime.Now;
                        DateTime waktuMasuk = new DateTime(
                            selectedDate.Year,
                            selectedDate.Month,
                            selectedDate.Day,
                            waktuNow.Hour,
                            waktuNow.Minute,
                            waktuNow.Second
                        );

                        // Add parameters safely to avoid SQL injection
                        cmd.Parameters.AddWithValue("@ID_Karyawan", userID);
                        cmd.Parameters.AddWithValue("@Tanggal", selectedDate);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@Waktu_Masuk", waktuMasuk);

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
