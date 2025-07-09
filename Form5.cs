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
        
        string connect = ""; // Deklarasikan variabel untuk menyimpan string koneksi

        // Cache instance and key
        private readonly MemoryCache _cache = MemoryCache.Default;
        private const string CacheKey = "AttendanceData";

        public Form5(string role)
        {
            InitializeComponent();
            userRole = role;
            connect = Koneksi.GetConnectionString();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            EnsureAttendanceIndexes();
            LoadStatusOptions();
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
                SELECT 
                    h.ID_Kehadiran, 
                    h.ID_Karyawan, 
                    k.Nama, 
                    k.Jabatan, 
                    k.Departemen, 
                    h.Tanggal,     
                    h.Waktu_Masuk, 
                    h.Waktu_Keluar, 
                    h.Status
                FROM Kehadiran h
                INNER JOIN Karyawan k ON h.ID_Karyawan = k.ID_Karyawan";
                try
                {
                    using (var conn = new SqlConnection(connect))
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
                using (var conn = new SqlConnection(connect))
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


        private void EnsureAttendanceIndexes()
        {
            using (var conn = new SqlConnection(connect))
            {
                try
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        // Index on (ID_Karyawan, Tanggal)
                        cmd.CommandText = @"
                            IF NOT EXISTS (
                                SELECT 1 FROM sys.indexes 
                                WHERE name = 'idx_Kehadiran_Karyawan_Tanggal' AND object_id = OBJECT_ID('Kehadiran')
                            )
                            BEGIN
                                CREATE NONCLUSTERED INDEX idx_Kehadiran_Karyawan_Tanggal
                                ON Kehadiran(ID_Karyawan, Tanggal);
                            END";
                        cmd.ExecuteNonQuery();

                        // Optional: Index on Tanggal
                        cmd.CommandText = @"
                            IF NOT EXISTS (
                                SELECT 1 FROM sys.indexes 
                                WHERE name = 'idx_Kehadiran_Tanggal' AND object_id = OBJECT_ID('Kehadiran')
                            )
                            BEGIN
                                CREATE NONCLUSTERED INDEX idx_Kehadiran_Tanggal
                                ON Kehadiran(Tanggal);
                            END";
                        cmd.ExecuteNonQuery();

                        // Optional: Index on Status
                        cmd.CommandText = @"
                            IF NOT EXISTS (
                                SELECT 1 FROM sys.indexes 
                                WHERE name = 'idx_Kehadiran_Status' AND object_id = OBJECT_ID('Kehadiran')
                            )
                            BEGIN
                                CREATE NONCLUSTERED INDEX idx_Kehadiran_Status
                                ON Kehadiran(Status);
                            END";
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to create indexes: " + ex.Message);
                }
            }
        }

        private void SearchAttendanceData()
        {
            StringBuilder query = new StringBuilder(@"
                SELECT h.ID_Kehadiran, h.ID_Karyawan, k.Nama, k.Jabatan, k.Departemen, 
                       h.Waktu_Masuk, h.Waktu_Keluar, h.Status
                FROM Kehadiran h
                INNER JOIN Karyawan k ON h.ID_Karyawan = k.ID_Karyawan
                WHERE 1=1");

            var parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(txtSearchNama.Text))
            {
                query.Append(" AND k.Nama LIKE @Nama");
                parameters.Add(new SqlParameter("@Nama", "%" + txtSearchNama.Text + "%"));
            }

            if (cmbStatus.SelectedItem != null && cmbStatus.SelectedItem.ToString() != "")
            {
                query.Append(" AND h.Status = @Status");
                parameters.Add(new SqlParameter("@Status", cmbStatus.SelectedItem.ToString()));
            }

            if (dateSearch.Checked)
            {
                query.Append(" AND h.Tanggal = @Tanggal");
                parameters.Add(new SqlParameter("@Tanggal", dateSearch.Value.Date));
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connect))
                using (SqlCommand cmd = new SqlCommand(query.ToString(), conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    dataGridView1.Columns["Waktu_Masuk"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                    dataGridView1.Columns["Waktu_Keluar"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search error: " + ex.Message);
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            _cache.Remove(CacheKey); // Optional: force fresh result
            SearchAttendanceData();
        }


        private void LoadStatusOptions()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add(""); // Add empty option for "no filter"

            try
            {
                using (SqlConnection conn = new SqlConnection(connect))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT Status FROM Kehadiran ORDER BY Status", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbStatus.Items.Add(reader["Status"].ToString());
                        }
                    }
                }

                cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load status list: " + ex.Message);
            }
        }

        private void txtSearchNama_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
