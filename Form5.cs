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
using System.Runtime.Caching;  // <-- Add this for MemoryCache

namespace pabdproject
{
    public partial class Form5 : Form
    {
        private readonly string userRole;
        private readonly string connectionString = "Data Source=BILLAAA\\SA; Initial Catalog=MANDAK;Integrated Security=True";

        // Cache instance and key
        private readonly MemoryCache _cache = MemoryCache.Default;
        private const string CacheKey = "AttendanceData";

        public Form5(string role)
        {
            InitializeComponent();
            userRole = role;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            _cache.Remove(CacheKey);
            LoadAttendanceData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form2 = new Form2(userRole);
            form2.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to delete.");
                return;
            }

            int idKehadiran = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_Kehadiran"].Value);

            DeleteAttendanceRecord(idKehadiran);
        }

        private void LoadAttendanceData()
        {
            // Try get from cache first
            var dt = _cache.Get(CacheKey) as DataTable;

            if (dt == null)
            {
                // Cache miss - load from DB
                const string query = @"
                    SELECT h.ID_Kehadiran, h.ID_Karyawan, k.Nama, k.Jabatan, k.Departemen, h.Waktu_Masuk, h.Waktu_Keluar, h.Status
                    FROM Kehadiran h
                    INNER JOIN Karyawan k ON h.ID_Karyawan = k.ID_Karyawan";
                try
                {
                    using (var conn = new SqlConnection(connectionString))
                    using (var da = new SqlDataAdapter(query, conn))
                    {
                        dt = new DataTable();
                        da.Fill(dt);

                        // Cache for 5 minutes from now
                        var policy = new CacheItemPolicy
                        {
                            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
                        };
                        _cache.Set(CacheKey, dt, policy);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading attendance data: " + ex.Message);
                    return;
                }
            }

            dataGridView1.DataSource = dt;

            if (dataGridView1.Columns.Contains("Waktu_Masuk"))
                dataGridView1.Columns["Waktu_Masuk"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";

            if (dataGridView1.Columns.Contains("Waktu_Keluar"))
                dataGridView1.Columns["Waktu_Keluar"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
        }

        private void DeleteAttendanceRecord(int idKehadiran)
        {
            const string query = "DELETE FROM Kehadiran WHERE ID_Kehadiran = @ID_Kehadiran";

            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_Kehadiran", idKehadiran);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Attendance record deleted successfully.");

                        // Clear cache after delete
                        _cache.Remove(CacheKey);

                        LoadAttendanceData();
                    }
                    else
                    {
                        MessageBox.Show("Unable to delete the attendance record.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting attendance record: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: handle clicks if needed
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _cache.Remove(CacheKey);
            LoadAttendanceData();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            exportreport exportreport = new exportreport();
            exportreport.Show();
            this.Hide();
        }
    }
}
