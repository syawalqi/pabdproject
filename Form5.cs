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

        private void Form4_Load(object sender, EventArgs e)
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

        private void Form5_Load(object sender, EventArgs e)
        {

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
    }
}
