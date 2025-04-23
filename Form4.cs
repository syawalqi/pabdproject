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
    public partial class Form4 : Form
    {
        private string userRole;

        // Modified constructor to accept user role as a parameter
        public Form4(string role)
        {
            InitializeComponent();
            userRole = role; // Store the role for later use
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // Use the role to enable/disable buttons or customize the form behavior here
            if (userRole == "employee")
            {
                // Example: Disable buttons/features for employees
                button2.Enabled = false; // Disable delete button for employees
            }

            // Load Karyawan data after customizing based on role
            LoadKaryawanData();
        }

        // Load data from the Karyawan table
        private void LoadKaryawanData()
        {
            string connectionString = "Data Source=LAPTOP-PFIH6R5H\\GALIHMAULANA;Initial Catalog=MANDAK;Integrated Security=True";
            string query = "SELECT ID_Karyawan, Nama, Jabatan, Departemen, Tanggal_Masuk FROM Karyawan";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // Delete selected row from the Karyawan table
        private void button2_Click(object sender, EventArgs e)
        {
            if (userRole == "employee")
            {
                MessageBox.Show("You do not have permission to delete records.");
                return;
            }

            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int idKaryawan = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_Karyawan"].Value);

                    string connectionString = "Data Source=LAPTOP-PFIH6R5H\\GALIHMAULANA;Initial Catalog=MANDAK;Integrated Security=True";
                    string query = "DELETE FROM Karyawan WHERE ID_Karyawan = @ID_Karyawan";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID_Karyawan", idKaryawan);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Refresh the DataGridView after deletion
                    LoadKaryawanData();
                    MessageBox.Show("Record deleted successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a record to delete.");
            }
        }

        // Button to go back to Form2
        private void button1_Click(object sender, EventArgs e)
        {
            // Pass the role when opening Form2
            Form2 form2 = new Form2(userRole); // Pass the role here
            form2.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form popup = new Form()
            {
                Width = 400,
                Height = 400,
                Text = "Add New Karyawan"
            };

            Label lblNama = new Label() { Text = "Nama", Left = 10, Top = 20 };
            TextBox txtNama = new TextBox() { Left = 150, Top = 20, Width = 200 };

            Label lblJabatan = new Label() { Text = "Jabatan", Left = 10, Top = 60 };
            TextBox txtJabatan = new TextBox() { Left = 150, Top = 60, Width = 200 };

            Label lblDepartemen = new Label() { Text = "Departemen", Left = 10, Top = 100 };
            TextBox txtDepartemen = new TextBox() { Left = 150, Top = 100, Width = 200 };

            Label lblTanggal = new Label() { Text = "Tanggal Masuk", Left = 10, Top = 140 };
            DateTimePicker dtTanggal = new DateTimePicker() { Left = 150, Top = 140, Width = 200, Format = DateTimePickerFormat.Short };

            Label lblPassword = new Label() { Text = "Password", Left = 10, Top = 180 };
            TextBox txtPassword = new TextBox() { Left = 150, Top = 180, Width = 200 };

            Label lblRole = new Label() { Text = "Role", Left = 10, Top = 220 };
            ComboBox cmbRole = new ComboBox() { Left = 150, Top = 220, Width = 200 };
            cmbRole.Items.AddRange(new string[] { "employee", "admin" });
            cmbRole.SelectedIndex = 0;

            Button btnSave = new Button() { Text = "Save", Left = 150, Top = 270, Width = 100 };
            btnSave.Click += (s, ev) =>
            {
                string nama = txtNama.Text.Trim();
                string jabatan = txtJabatan.Text.Trim();
                string departemen = txtDepartemen.Text.Trim();
                DateTime tanggalMasuk = dtTanggal.Value;
                string passwd = txtPassword.Text.Trim();
                string role = cmbRole.SelectedItem.ToString();

                if (string.IsNullOrEmpty(nama) || string.IsNullOrEmpty(jabatan) || string.IsNullOrEmpty(departemen) || string.IsNullOrEmpty(passwd))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                string connectionString = "Data Source=LAPTOP-PFIH6R5H\\GALIHMAULANA;Initial Catalog=MANDAK;Integrated Security=True";
                string query = "INSERT INTO Karyawan (Nama, Jabatan, Departemen, Tanggal_Masuk, Passwd, Role) " +
                               "VALUES (@Nama, @Jabatan, @Departemen, @Tanggal_Masuk, @Passwd, @Role)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Nama", nama);
                        cmd.Parameters.AddWithValue("@Jabatan", jabatan);
                        cmd.Parameters.AddWithValue("@Departemen", departemen);
                        cmd.Parameters.AddWithValue("@Tanggal_Masuk", tanggalMasuk);
                        cmd.Parameters.AddWithValue("@Passwd", passwd);
                        cmd.Parameters.AddWithValue("@Role", role);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Karyawan added successfully.");
                        popup.Close();

                        LoadKaryawanData(); // Refresh the DataGridView
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error inserting data: " + ex.Message);
                    }
                }
            };

            popup.Controls.Add(lblNama);
            popup.Controls.Add(txtNama);
            popup.Controls.Add(lblJabatan);
            popup.Controls.Add(txtJabatan);
            popup.Controls.Add(lblDepartemen);
            popup.Controls.Add(txtDepartemen);
            popup.Controls.Add(lblTanggal);
            popup.Controls.Add(dtTanggal);
            popup.Controls.Add(lblPassword);
            popup.Controls.Add(txtPassword);
            popup.Controls.Add(lblRole);
            popup.Controls.Add(cmbRole);
            popup.Controls.Add(btnSave);

            popup.ShowDialog();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record to update.");
                return;
            }

            // Get selected row data
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            int idKaryawan = Convert.ToInt32(selectedRow.Cells["ID_Karyawan"].Value);
            string currentNama = selectedRow.Cells["Nama"].Value.ToString();
            string currentJabatan = selectedRow.Cells["Jabatan"].Value.ToString();
            string currentDepartemen = selectedRow.Cells["Departemen"].Value.ToString();
            DateTime currentTanggalMasuk = Convert.ToDateTime(selectedRow.Cells["Tanggal_Masuk"].Value);

            // Create popup form
            Form popup = new Form()
            {
                Width = 400,
                Height = 350,
                Text = "Update Karyawan"
            };

            Label lblNama = new Label() { Text = "Nama", Left = 10, Top = 20 };
            TextBox txtNama = new TextBox() { Left = 150, Top = 20, Width = 200, Text = currentNama };

            Label lblJabatan = new Label() { Text = "Jabatan", Left = 10, Top = 60 };
            TextBox txtJabatan = new TextBox() { Left = 150, Top = 60, Width = 200, Text = currentJabatan };

            Label lblDepartemen = new Label() { Text = "Departemen", Left = 10, Top = 100 };
            TextBox txtDepartemen = new TextBox() { Left = 150, Top = 100, Width = 200, Text = currentDepartemen };

            Label lblTanggal = new Label() { Text = "Tanggal Masuk", Left = 10, Top = 140 };
            DateTimePicker dtTanggal = new DateTimePicker() { Left = 150, Top = 140, Width = 200, Format = DateTimePickerFormat.Short, Value = currentTanggalMasuk };

            Button btnSave = new Button() { Text = "Update", Left = 150, Top = 200, Width = 100 };
            btnSave.Click += (s, ev) =>
            {
                string nama = txtNama.Text.Trim();
                string jabatan = txtJabatan.Text.Trim();
                string departemen = txtDepartemen.Text.Trim();
                DateTime tanggalMasuk = dtTanggal.Value;

                if (string.IsNullOrEmpty(nama) || string.IsNullOrEmpty(jabatan) || string.IsNullOrEmpty(departemen))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                string connectionString = "Data Source=LAPTOP-PFIH6R5H\\GALIHMAULANA;Initial Catalog=MANDAK;Integrated Security=True";
                string query = "UPDATE Karyawan SET Nama = @Nama, Jabatan = @Jabatan, Departemen = @Departemen, Tanggal_Masuk = @Tanggal_Masuk WHERE ID_Karyawan = @ID_Karyawan";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Nama", nama);
                        cmd.Parameters.AddWithValue("@Jabatan", jabatan);
                        cmd.Parameters.AddWithValue("@Departemen", departemen);
                        cmd.Parameters.AddWithValue("@Tanggal_Masuk", tanggalMasuk);
                        cmd.Parameters.AddWithValue("@ID_Karyawan", idKaryawan);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Karyawan updated successfully.");
                        popup.Close();
                        LoadKaryawanData(); // Refresh the table
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating data: " + ex.Message);
                    }
                }
            };

            popup.Controls.Add(lblNama);
            popup.Controls.Add(txtNama);
            popup.Controls.Add(lblJabatan);
            popup.Controls.Add(txtJabatan);
            popup.Controls.Add(lblDepartemen);
            popup.Controls.Add(txtDepartemen);
            popup.Controls.Add(lblTanggal);
            popup.Controls.Add(dtTanggal);
            popup.Controls.Add(btnSave);

            popup.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }


}

