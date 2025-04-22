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
    public partial class Form4: Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // SQL connection string
            string connectionString = "Data Source=LAPTOP-PFIH6R5H\\GALIHMAULANA;Initial Catalog=MANDAK;Integrated Security=True";

            string query = "SELECT ID_Karyawan, Nama, Jabatan, Departemen, Tanggal_masuk" +
                          " FROM Karyawan"; // space before 'FROM'


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    // Open connection
                    conn.Open();

                    // Create SqlDataAdapter to execute the query and fill the DataGridView
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();

                    // Fill the DataTable with the query results
                    da.Fill(dt);

                    // Bind the DataTable to the DataGridView
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create a new instance of Form2
            Form2 form2 = new Form2();

            // Show Form2
            form2.Show();

            // Optionally, you can hide Form1 if you don't want it visible after opening Form2
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Check if a row is selected in the DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    // Get the ID of the selected row (assuming ID_Karyawan is in the first column)
                    int idKaryawan = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_Karyawan"].Value);

                    // SQL connection string
                    string connectionString = "Data Source=LAPTOP-PFIH6R5H\\GALIHMAULANA;Initial Catalog=MANDAK;Integrated Security=True";

                    // SQL DELETE query to remove the selected record
                    string query = "DELETE FROM Karyawan WHERE ID_Karyawan = @ID_Karyawan";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        // Create SqlCommand object to execute the query
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Add the ID parameter to the query
                            cmd.Parameters.AddWithValue("@ID_Karyawan", idKaryawan);

                            // Execute the DELETE query
                            cmd.ExecuteNonQuery();

                            // Refresh the DataGridView after deletion
                            LoadData(); // Call a method to reload the data
                        }
                    }

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

        private void LoadData()
        {
            // SQL connection string
            string connectionString = "Data Source=LAPTOP-PFIH6R5H\\GALIHMAULANA;Initial Catalog=MANDAK;Integrated Security=True";

            // SQL query to fetch data
            string query = "SELECT ID_Karyawan, Nama, Jabatan, Departemen, Tanggal_masuk FROM Karyawan";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Create SqlDataAdapter to execute the query and fill the DataGridView
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();

                    // Fill the DataTable with the query results
                    da.Fill(dt);

                    // Bind the DataTable to the DataGridView
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

    }
}
