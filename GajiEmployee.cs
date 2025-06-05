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
using static NPOI.HSSF.Util.HSSFColor;

namespace pabdproject
{
    public partial class GajiEmployee : Form
    {
        private readonly string userRole;
        private string connectionString = "Data Source=NITROSFAQIH\\SQLEXPRESS;Initial Catalog=MANDAK;Integrated Security=True";
        private int selectedID_Karyawan = -1;

        public GajiEmployee(string role)
        {
            InitializeComponent();
            userRole = role;
        }

        private void LoadJoinedData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT
                    k.ID_Karyawan,
                    k.Nama,
                    k.Jabatan,
                    k.Departemen,
                    g.Gaji_Pokok
                FROM
                    Karyawan k
                LEFT JOIN
                    Gaji g ON k.ID_Karyawan = g.ID_Karyawan";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();

                try
                {
                    dataAdapter.Fill(dataTable);
                    dataGajiKaryawan.DataSource = dataTable;

                    // Optional: Hide the ID_Karyawan column
                    dataGajiKaryawan.Columns["ID_Karyawan"].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }


        private void GajiEmployee_Load(object sender, EventArgs e)
        {

        }

        private void dataGajiKaryawan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Pastikan indeks baris valid
            if (e.RowIndex >= 0 && e.RowIndex < dataGajiKaryawan.Rows.Count - 1)
            {
                try
                {
                    // Ambil ID_Organisasi dari baris yang dipilih
                    var ID_KaryawanCellValue = dataGajiKaryawan.Rows[e.RowIndex].Cells["ID_Karyawan"].Value;
                    if (ID_KaryawanCellValue != null && ID_KaryawanCellValue != DBNull.Value)
                    {
                        // Simpan ID yang dipilih untuk digunakan nanti (pada operasi: update/delete)
                        selectedID_Karyawan = Convert.ToInt32(ID_KaryawanCellValue);

                        // Isi TextBox dengan data dari baris yang dipilih
                        txtNamaKaryawan.Text = dataGajiKaryawan.Rows[e.RowIndex].Cells["Nama"].Value?.ToString() ?? "";
                        txtGajiKaryawan.Text = dataGajiKaryawan.Rows[e.RowIndex].Cells["Gaji_pokok"].Value?.ToString() ?? "";
                        txtJabatan.Text = dataGajiKaryawan.Rows[e.RowIndex].Cells["Departemen"].Value?.ToString() ?? "";
                        txtDepartemen.Text = dataGajiKaryawan.Rows[e.RowIndex].Cells["Jabatan"].Value?.ToString() ?? "";
                    }
                    else
                    {
                        lblMessage.Text = "ID organisasi tidak valid.";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat mengambil data: " + ex.Message);
                }
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string namaKaryawan = txtNamaKaryawan.Text;
            string gajiPokokStr = txtGajiKaryawan.Text;
            string jabatan = txtJabatan.Text;
            string departemen = txtDepartemen.Text;

            // Validasi input
            if (string.IsNullOrEmpty(namaKaryawan) || string.IsNullOrEmpty(gajiPokokStr) ||
                string.IsNullOrEmpty(jabatan) || string.IsNullOrEmpty(departemen))
            {
                lblMessage.Text = "Isi semua kolom terlebih dahulu.";
                return;
            }

            if (!decimal.TryParse(gajiPokokStr, out decimal gajiPokok))
            {
                lblMessage.Text = "Gaji harus berupa angka.";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.Transaction = transaction;

                    // Insert ke tabel Karyawan
                    cmd.CommandText = @"
                        INSERT INTO Karyawan (Nama, Jabatan, Departemen)
                        VALUES (@Nama, @Jabatan, @Departemen);
                        SELECT SCOPE_IDENTITY();";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Nama", namaKaryawan);
                    cmd.Parameters.AddWithValue("@Jabatan", jabatan);
                    cmd.Parameters.AddWithValue("@Departemen", departemen);

                    int idKaryawan = Convert.ToInt32(cmd.ExecuteScalar());

                    // Insert ke tabel Gaji
                    cmd.CommandText = @"
                        INSERT INTO Gaji (ID_Karyawan, Gaji_Pokok)
                        VALUES (@ID_Karyawan, @Gaji_Pokok)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID_Karyawan", idKaryawan);
                    cmd.Parameters.AddWithValue("@Gaji_Pokok", gajiPokok);

                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    lblMessage.Text = "Data karyawan dan gaji berhasil disimpan.";
                    LoadJoinedData();
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    lblMessage.Text = "Terjadi kesalahan: " + ex.Message;
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedID_Karyawan == -1)
            {
                lblMessage.Text = "Pilih karyawan yang ingin diubah.";
                return;
            }

            string namaKaryawan = txtNamaKaryawan.Text;
            string jabatan = txtJabatan.Text;
            string departemen = txtDepartemen.Text;
            string gajiStr = txtGajiKaryawan.Text;

            // Validasi input
            if (string.IsNullOrEmpty(namaKaryawan) || string.IsNullOrEmpty(jabatan) ||
                string.IsNullOrEmpty(departemen) || !decimal.TryParse(gajiStr, out decimal gajiPokok))
            {
                lblMessage.Text = "Pastikan semua kolom diisi dengan benar.";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = conn,
                        Transaction = transaction
                    };

                    // Update data Karyawan
                    cmd.CommandText = @"
                UPDATE Karyawan
                SET Nama = @Nama, Jabatan = @Jabatan, Departemen = @Departemen
                WHERE ID_Karyawan = @ID_Karyawan";
                    cmd.Parameters.AddWithValue("@Nama", namaKaryawan);
                    cmd.Parameters.AddWithValue("@Jabatan", jabatan);
                    cmd.Parameters.AddWithValue("@Departemen", departemen);
                    cmd.Parameters.AddWithValue("@ID_Karyawan", selectedID_Karyawan);
                    cmd.ExecuteNonQuery();

                    // Update data Gaji (opsional: hanya jika ada 1 entri gaji atau update entri terakhir)
                    // Check if Gaji exists
                    // Check if Gaji exists
                    cmd.CommandText = "SELECT COUNT(*) FROM Gaji WHERE ID_Karyawan = @ID_Karyawan";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID_Karyawan", selectedID_Karyawan);

                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        // Update existing Gaji
                    cmd.CommandText = @"
                    UPDATE Gaji
                    SET Gaji_Pokok = @Gaji_Pokok
                    WHERE ID_Karyawan = @ID_Karyawan";
                    }
                    else
                    {
                        // Insert new Gaji
                    cmd.CommandText = @"
                    INSERT INTO Gaji (ID_Karyawan, Gaji_Pokok)
                    VALUES (@ID_Karyawan, @Gaji_Pokok)";
                    }

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID_Karyawan", selectedID_Karyawan);
                    cmd.Parameters.AddWithValue("@Gaji_Pokok", gajiPokok);
                    cmd.ExecuteNonQuery();



                    transaction.Commit();
                    lblMessage.Text = "Data berhasil diubah.";
                    LoadJoinedData();
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    lblMessage.Text = "Error: " + ex.Message;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedID_Karyawan == -1)
            {
                lblMessage.Text = "Pilih karyawan yang ingin dihapus";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = conn,
                        Transaction = transaction
                    };

                    // Delete from Gaji table (foreign key dependency)
                    cmd.CommandText = "DELETE FROM Gaji WHERE ID_Karyawan = @ID_Karyawan";
                    cmd.Parameters.AddWithValue("@ID_Karyawan", selectedID_Karyawan);
                    cmd.ExecuteNonQuery();

                    // Delete from Karyawan table
                    cmd.CommandText = "DELETE FROM Karyawan WHERE ID_Karyawan = @ID_Karyawan";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID_Karyawan", selectedID_Karyawan);
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    lblMessage.Text = "Data berhasil dihapus.";
                    LoadJoinedData(); // Refresh after deletion
                    selectedID_Karyawan = -1;
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    lblMessage.Text = "Error: " + ex.Message;
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadJoinedData(); // Refresh after deletion
        }
    }
}
