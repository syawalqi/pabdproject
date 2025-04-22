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
    }


}

