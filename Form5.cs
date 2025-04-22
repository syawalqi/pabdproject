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
    public partial class Form5: Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            // SQL connection string
            string connectionString = "Data Source=LAPTOP-PFIH6R5H\\GALIHMAULANA;Initial Catalog=MANDAK;Integrated Security=True";

            // Query to join Kehadiran (attendance) and Karyawan (employee) tables
            string query = "SELECT h.ID_Kehadiran, h.ID_Karyawan, k.Nama, k.Jabatan, k.Departemen, h.Waktu_Masuk, h.Waktu_Keluar " +
                           "FROM Kehadiran h " +
                           "INNER JOIN Karyawan k ON h.ID_Karyawan = k.ID_Karyawan";

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
            Form2 form2 = new Form2("Admin"); // or pass "Employee" if needed
            form2.Show();
            this.Hide();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            // Check if a row is selected in the DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the ID_Kehadiran from the selected row (assuming it's in the first column)
                int idKehadiran = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_Kehadiran"].Value);

                // SQL connection string
                string connectionString = "Data Source=LAPTOP-PFIH6R5H\\GALIHMAULANA;Initial Catalog=MANDAK;Integrated Security=True";

                // SQL query to delete the attendance record based on ID_Kehadiran
                string query = "DELETE FROM Kehadiran WHERE ID_Kehadiran = @ID_Kehadiran";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        // Open connection
                        conn.Open();

                        // Create SqlCommand to execute the DELETE query
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ID_Kehadiran", idKehadiran);

                        // Execute the query to delete the record
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Attendance record deleted successfully.");

                            // Refresh the DataGridView after deletion
                            LoadAttendanceData();  // Assuming LoadAttendanceData() is the method used to load the data
                        }
                        else
                        {
                            MessageBox.Show("Error: Unable to delete the attendance record.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        // Optional: Refactor the code to load the data (as shown earlier)
        private void LoadAttendanceData()
        {
            // SQL connection string
            string connectionString = "Data Source=LAPTOP-PFIH6R5H\\GALIHMAULANA;Initial Catalog=MANDAK;Integrated Security=True";

            // Query to join Kehadiran (attendance) and Karyawan (employee) tables
            string query = "SELECT h.ID_Kehadiran, h.ID_Karyawan, k.Nama, k.Jabatan, k.Departemen, h.Waktu_Masuk, h.Waktu_Keluar " +
                           "FROM Kehadiran h " +
                           "INNER JOIN Karyawan k ON h.ID_Karyawan = k.ID_Karyawan";

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

    }
}
