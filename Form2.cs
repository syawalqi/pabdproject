﻿using System;
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
        string connect = ""; // Deklarasikan variabel untuk menyimpan string koneksi
        public Form2(string role)
        {
            InitializeComponent();
            userRole = role;
            connect = Koneksi.GetConnectionString();
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
                button4.Visible = false; // Hide admin feature button (Gaji Employee)

                bttnCuti.Visible = true;
                bttnCuti.Text = "Ajukan/Lihat Cuti";
                bttnShiftKaryawan.Visible = false;
                btnGrafikGaji.Visible = false;
            }
            else if (userRole == "admin" || userRole == "hrd") // Pastikan 'hrd' ada di sini
            {
                // Show admin-specific features
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;

                // Tampilkan tombol Cuti untuk admin/hrd
                bttnCuti.Visible = true;
                bttnCuti.Text = "Kelola Cuti";
                bttnShiftKaryawan.Visible = true;
                btnGrafikGaji.Visible = true;
            }
            else
            {
                // Jika ada peran lain yang tidak terdefinisi
                bttnCuti.Visible = false; // Sembunyikan tombol cuti
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

        private void Logout_Click(object sender, EventArgs e)
        {
            // Clear session info
            Form3.LoggedInUserID = -1;
            Form3.LoggedInUserName = string.Empty;

            // Open the login form BEFORE closing current forms
            Form3 loginForm = new Form3();
            loginForm.Show();

            // Close all other forms including current one except the login form
            foreach (Form openForm in Application.OpenForms.Cast<Form>().ToList())
            {
                if (!(openForm is Form3))
                {
                    openForm.Close();
                }
            }

            // Optionally, close current form if it's not login
            if (!(this is Form3))
            {
                this.Close();
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (userRole == "employee")
            {
                MessageBox.Show("This feature is not available for employees.");
                return;
            }

            // Navigate to Form5 (Attendance list)
            var attendanceForm = new GajiEmployee(userRole);
            attendanceForm.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bttnCuti_Click(object sender, EventArgs e)
        {
            // Pastikan FormCutiPengajuan menerima role
            FormCutiPengajuan formCuti = new FormCutiPengajuan(userRole);
            formCuti.Show();
            this.Hide();
        }

        private void bttnShiftKaryawan_Click(object sender, EventArgs e)
        {
            if (userRole == "employee")
            {
                MessageBox.Show("Fitur ini tidak tersedia untuk karyawan.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Perbaikan: Teruskan userRole ke konstruktor FormShiftKaryawan
            var shiftForm = new FormShiftKaryawan(userRole); // <--- Pastikan baris ini sudah benar
            shiftForm.Show(); // Tampilkan FormShiftKaryawan
            this.Hide(); // Sembunyikan Form2 saat FormShiftKaryawan terbuka
        }

        private void btnGrafikGaji_Click(object sender, EventArgs e)
        {
            GrafikGaji formGrafikGaji = new GrafikGaji(); // Sesuaikan nama class jika berbeda
            formGrafikGaji.Show(); // Tampilkan form grafik
            this.Hide(); // Sembunyikan form dashboard
        }
    }
}

