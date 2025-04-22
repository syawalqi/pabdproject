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
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
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
            // Provide the required argument for the role (e.g., "Admin" or "Employee")
            string role = "Admin"; // or "Employee" based on your logic

            // Now pass the role when creating Form2
            Form2 form2 = new Form2(role);
            form2.Show();
            this.Hide();
        }

        // You can add a method to delete from Admin table as well, similar to the above logic
        private void DeleteAdminRecord(int adminID)
        {
            string connectionString = "Data Source=LAPTOP-PFIH6R5H\\GALIHMAULANA;Initial Catalog=MANDAK;Integrated Security=True";
            string query = "DELETE FROM Admin WHERE ID_Admin = @ID_Admin";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_Admin", adminID);
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
