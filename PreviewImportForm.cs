using NPOI.POIFS.Crypt.Dsig;
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
    public partial class PreviewImportForm : Form
    {
        private readonly string userRole;
        private string connectionString = "Data Source=BILLAAA\\SA; Initial Catalog=MANDAK;Integrated Security=True";

        public PreviewImportForm(DataTable data)
        {
            InitializeComponent();
            dataGridView1.DataSource = data;
        }

        private void PreviewImportForm_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoResizeColumns();
        }

        private bool ValidateRow(DataRow row, out DateTime tanggalMasuk)
        {
            tanggalMasuk = DateTime.MinValue;

            string nama = row["Nama"]?.ToString().Trim();
            string jabatan = row["Jabatan"]?.ToString().Trim();
            string departemen = row["Departemen"]?.ToString().Trim();
            string tanggalMasukStr = row["Tanggal_Masuk"]?.ToString().Trim();
            string role = row.Table.Columns.Contains("Role") ? row["Role"]?.ToString().Trim().ToLower() : "employee";

            if (string.IsNullOrWhiteSpace(nama) || string.IsNullOrWhiteSpace(jabatan) || string.IsNullOrWhiteSpace(departemen))
            {
                MessageBox.Show("Kolom Nama, Jabatan, dan Departemen wajib diisi.", "Kesalahan Validasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!DateTime.TryParse(tanggalMasukStr, out tanggalMasuk))
            {
                MessageBox.Show("Tanggal Masuk tidak valid.", "Kesalahan Validasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (role != "admin" && role != "employee")
            {
                MessageBox.Show("Role harus bernilai 'admin' atau 'employee'.", "Kesalahan Validasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void ImportDataToDatabase()
        {
            try
            {
                DataTable dt = (DataTable)dataGridView1.DataSource;

                foreach (DataRow row in dt.Rows)
                {
                    if (!ValidateRow(row, out DateTime tanggalMasuk))
                        continue;

                    string nama = row["Nama"]?.ToString().Trim();
                    string jabatan = row["Jabatan"]?.ToString().Trim();
                    string departemen = row["Departemen"]?.ToString().Trim();
                    string passwd = row.Table.Columns.Contains("Passwd") ? row["Passwd"]?.ToString().Trim() : "defaultPass";
                    string role = row.Table.Columns.Contains("Role") ? row["Role"]?.ToString().Trim().ToLower() : "employee";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("TambahKaryawan", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Nama", nama);
                            cmd.Parameters.AddWithValue("@Jabatan", jabatan);
                            cmd.Parameters.AddWithValue("@Departemen", departemen);
                            cmd.Parameters.AddWithValue("@Tanggal_Masuk", tanggalMasuk);
                            cmd.Parameters.AddWithValue("@Passwd", passwd);
                            cmd.Parameters.AddWithValue("@Role", role);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Data berhasil diimpor ke database melalui stored procedure.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Tutup form setelah import
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat mengimpor data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah Anda ingin mengimpor data ini ke database?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ImportDataToDatabase();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(userRole);
            form4.Show();
            this.Close();
        }
    }
}
