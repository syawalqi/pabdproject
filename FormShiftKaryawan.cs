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
using System.Runtime.Caching;

namespace pabdproject
{
    public partial class FormShiftKaryawan : Form
    {
        private readonly string connectionString = "Data Source=LAPTOP-PFIH6R5H\\GALIHMAULANA;Initial Catalog=MANDAK;Integrated Security=True";
        private int selectedShiftId = -1; // Default value, menandakan tidak ada baris yang dipilih
        private string userRole;
        private readonly MemoryCache cache = MemoryCache.Default;
        private readonly CacheItemPolicy policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) // Data akan kadaluarsa setelah 5 menit
        };
        private const string CacheKey = "ShiftData"; // Kunci unik untuk data shift di cache

        public FormShiftKaryawan(string userRole)
        {
            InitializeComponent();
            ApplyShiftFormStyling();
            this.userRole = userRole;
        }
        private void LoadShiftData()
        {
            DataTable dt;

            // Cek apakah data ada di cache
            if (cache.Contains(CacheKey)) // Menggunakan cache untuk menyimpan hasil query [cite: 542]
            {
                dt = cache.Get(CacheKey) as DataTable; // Mengambil data dari cache [cite: 543]
                                                       // MessageBox.Show("Data loaded from cache.", "Cache Hit", MessageBoxButtons.OK, MessageBoxIcon.Information); // Opsional
            }
            else
            {
                dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("GetAllShiftsWithKaryawan", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(dt); // Mengisi DataTable dengan data dari stored procedure

                            cache.Add(CacheKey, dt, policy); // Menyimpan data ke cache dengan kebijakan kadaluarsa 5 menit [cite: 554]
                                                             // MessageBox.Show("Data loaded from database and cached.", "Cache Miss", MessageBoxButtons.OK, MessageBoxIcon.Information); // Opsional
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading shift data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Penting: keluar jika ada error saat memuat data awal
                    }
                }
            }
            dgvShifts.DataSource = dt;
            dgvShifts.Columns["ID_Shift"].Visible = false;
            dgvShifts.Columns["ID_Karyawan"].Visible = false;
            dgvShifts.AutoGenerateColumns = true;
        }
        private void FormShiftKaryawan_Load(object sender, EventArgs e)
        {
            // Panggil metode untuk mengisi ComboBox Hari_Kerja
            PopulateHariKerjaComboBox();
            LoadShiftData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadShiftData(); // Muat ulang data saat tombol refresh diklik
            cache.Remove(CacheKey);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ClearForm()
        {
            txtIDKaryawan.Clear();
            cmbHariKerja.SelectedIndex = -1; // Menghapus pilihan di ComboBox
            dtpShiftMulai.Value = DateTime.Now.Date.AddHours(8); // Contoh: atur ke 08:00 hari ini
            dtpShiftSelesai.Value = DateTime.Now.Date.AddHours(17); // Contoh: atur ke 17:00 hari ini
                                                                    // Tambahkan pembersihan untuk elemen lain jika ada
        }
        private void PopulateHariKerjaComboBox()
        {
            cmbHariKerja.Items.Clear();
            cmbHariKerja.Items.Add("Senin");
            cmbHariKerja.Items.Add("Selasa");
            cmbHariKerja.Items.Add("Rabu");
            cmbHariKerja.Items.Add("Kamis");
            cmbHariKerja.Items.Add("Jumat");
            cmbHariKerja.Items.Add("Sabtu");
            cmbHariKerja.Items.Add("Minggu");
            cmbHariKerja.SelectedIndex = 0; // Pilih item pertama sebagai default
        }

        private void btnTambahShift_Click(object sender, EventArgs e)
        {
            // Validasi input sederhana di sisi aplikasi
            if (string.IsNullOrWhiteSpace(txtIDKaryawan.Text) || cmbHariKerja.SelectedItem == null)
            {
                MessageBox.Show("Harap isi ID Karyawan dan Hari Kerja.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtIDKaryawan.Text, out int idKaryawan))
            {
                MessageBox.Show("ID Karyawan harus berupa angka.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpShiftSelesai.Value <= dtpShiftMulai.Value)
            {
                MessageBox.Show("Waktu Shift Selesai harus setelah Waktu Shift Mulai.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hariKerja = cmbHariKerja.SelectedItem.ToString();
            TimeSpan shiftMulai = dtpShiftMulai.Value.TimeOfDay;
            TimeSpan shiftSelesai = dtpShiftSelesai.Value.TimeOfDay;

            if (shiftSelesai <= shiftMulai) // Gunakan TimeSpan yang sudah diambil
            {
                MessageBox.Show("Waktu Shift Selesai harus setelah Waktu Shift Mulai.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Hentikan proses jika validasi gagal
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("AddShift", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_Karyawan", idKaryawan);
                        cmd.Parameters.AddWithValue("@Hari_Kerja", hariKerja);
                        cmd.Parameters.AddWithValue("@Shift_Mulai", shiftMulai);
                        cmd.Parameters.AddWithValue("@Shift_Selesai", shiftSelesai);

                        cmd.ExecuteNonQuery(); // Menjalankan stored procedure

                        MessageBox.Show("Data shift berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadShiftData(); // Muat ulang data untuk menampilkan shift yang baru
                        ClearForm(); // Bersihkan form input

                        cache.Remove(CacheKey); // Invalidasi cache setelah penambahan data
                        LoadShiftData();
                        ClearForm();
                    }
                }
                catch (SqlException ex)
                {
                    // Menangani error dari stored procedure (misalnya dari RAISERROR)
                    MessageBox.Show("Error adding shift: " + ex.Message, "Kesalahan SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    // Menangani error umum
                    MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void dgvShifts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Pastikan klik terjadi pada baris data yang valid (bukan header atau baris kosong)
            if (e.RowIndex >= 0 && e.RowIndex < dgvShifts.Rows.Count)
            {
                try
                {
                    DataGridViewRow row = dgvShifts.Rows[e.RowIndex];

                    var idShiftCellValue = row.Cells["ID_Shift"].Value;
                    if (idShiftCellValue != null && idShiftCellValue != DBNull.Value)
                    {
                        selectedShiftId = Convert.ToInt32(idShiftCellValue);
                    }
                    else
                    {
                        selectedShiftId = -1; // Reset jika nilai tidak valid
                    }

                    var idKaryawanCellValue = row.Cells["ID_Karyawan"].Value;
                    txtIDKaryawan.Text = (idKaryawanCellValue != null && idKaryawanCellValue != DBNull.Value)
                                         ? idKaryawanCellValue.ToString()
                                         : string.Empty;


                    var hariKerjaCellValue = row.Cells["Hari_Kerja"].Value;
                    if (hariKerjaCellValue != null && hariKerjaCellValue != DBNull.Value)
                    {
                        cmbHariKerja.SelectedItem = hariKerjaCellValue.ToString();
                    }
                    else
                    {
                        cmbHariKerja.SelectedIndex = -1; // Reset pilihan jika kosong
                    }

                    var shiftMulaiCellValue = row.Cells["Shift_Mulai"].Value;
                    if (shiftMulaiCellValue != null && shiftMulaiCellValue != DBNull.Value)
                    {
                        // Convert dari TimeSpan (dari DB) ke DateTime (untuk DateTimePicker)
                        dtpShiftMulai.Value = DateTime.Today.Add((TimeSpan)shiftMulaiCellValue);
                    }
                    else
                    {
                        // Atur ke nilai default atau kosongkan jika memungkinkan
                        dtpShiftMulai.Value = DateTime.Today; // Default ke waktu saat ini jika null
                    }

                    var shiftSelesaiCellValue = row.Cells["Shift_Selesai"].Value;
                    if (shiftSelesaiCellValue != null && shiftSelesaiCellValue != DBNull.Value)
                    {
                        // Convert dari TimeSpan (dari DB) ke DateTime (untuk DateTimePicker)
                        dtpShiftSelesai.Value = DateTime.Today.Add((TimeSpan)shiftSelesaiCellValue);
                    }
                    else
                    {
                        // Atur ke nilai default atau kosongkan jika memungkinkan
                        dtpShiftSelesai.Value = DateTime.Today; // Default ke waktu saat ini jika null
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat mengambil data dari baris: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    selectedShiftId = -1; // Reset ID jika ada error
                    ClearForm(); // Bersihkan form jika ada error
                }
            }
            else
            {
                selectedShiftId = -1; // Reset jika tidak ada baris yang valid diklik
                ClearForm(); // Bersihkan form
            }
        }

        private void btnDeleteShift_Click(object sender, EventArgs e)
        {
            if (selectedShiftId == -1) // Periksa apakah ada shift yang dipilih
            {
                MessageBox.Show("Pilih shift yang ingin dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tambahkan konfirmasi sebelum menghapus
            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus shift ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("DeleteShift", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID_Shift", selectedShiftId);

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Data shift berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadShiftData(); // Muat ulang data untuk menampilkan perubahan
                            ClearForm(); // Bersihkan form input dan reset selected ID
                            selectedShiftId = -1; // Reset selected ID setelah penghapusan

                            cache.Remove(CacheKey); // Invalidasi cache setelah penghapusan data
                            LoadShiftData();
                            ClearForm();
                            selectedShiftId = -1;
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error deleting shift: " + ex.Message, "Kesalahan SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void ApplyShiftFormStyling()
        {

            foreach (Control control in this.Controls)
            {
                if (dgvShifts != null)
                {
                    dgvShifts.Font = new Font("Segoe UI", 9);
                    dgvShifts.BackgroundColor = Color.WhiteSmoke;
                    dgvShifts.DefaultCellStyle.BackColor = Color.AliceBlue;
                    dgvShifts.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan;
                    dgvShifts.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkCyan;
                    dgvShifts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dgvShifts.EnableHeadersVisualStyles = false;
                }
            }

        }
        private void btnKembali_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(userRole); // Membuat instance Form2 baru dengan role pengguna
            form2.Show(); // Menampilkan Form2
            this.Close(); // Menutup form saat ini
        }

        private void dtpShiftSelesai_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dgvShifts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvShifts.Rows.Count)
            {
                try
                {
                    DataGridViewRow row = dgvShifts.Rows[e.RowIndex];

                    var idShiftCellValue = row.Cells["ID_Shift"].Value;
                    if (idShiftCellValue != null && idShiftCellValue != DBNull.Value)
                    {
                        selectedShiftId = Convert.ToInt32(idShiftCellValue);
                    }
                    else
                    {
                        selectedShiftId = -1; // Reset jika nilai tidak valid
                    }

                    var idKaryawanCellValue = row.Cells["ID_Karyawan"].Value;
                    txtIDKaryawan.Text = (idKaryawanCellValue != null && idKaryawanCellValue != DBNull.Value)
                                         ? idKaryawanCellValue.ToString()
                                         : string.Empty;

                    // Hari Kerja (untuk ComboBox)
                    var hariKerjaCellValue = row.Cells["Hari_Kerja"].Value;
                    if (hariKerjaCellValue != null && hariKerjaCellValue != DBNull.Value)
                    {
                        cmbHariKerja.SelectedItem = hariKerjaCellValue.ToString();
                    }
                    else
                    {
                        cmbHariKerja.SelectedIndex = -1; // Reset pilihan jika kosong
                    }

                    // Shift Mulai (untuk DateTimePicker)
                    var shiftMulaiCellValue = row.Cells["Shift_Mulai"].Value;
                    if (shiftMulaiCellValue != null && shiftMulaiCellValue != DBNull.Value)
                    {
                        dtpShiftMulai.Value = DateTime.Today.Add((TimeSpan)shiftMulaiCellValue);
                    }
                    else
                    {
                        dtpShiftMulai.Value = DateTime.Today; // Default ke waktu saat ini jika null
                    }

                    // Shift Selesai (untuk DateTimePicker)
                    var shiftSelesaiCellValue = row.Cells["Shift_Selesai"].Value;
                    if (shiftSelesaiCellValue != null && shiftSelesaiCellValue != DBNull.Value)
                    {
                        dtpShiftSelesai.Value = DateTime.Today.Add((TimeSpan)shiftSelesaiCellValue);
                    }
                    else
                    {
                        dtpShiftSelesai.Value = DateTime.Today; // Default ke waktu saat ini jika null
                    }

                    dgvShifts.ClearSelection(); // Bersihkan seleksi sebelumnya
                    row.Selected = true;         // Seleksi baris yang diklik
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat mengambil data dari baris: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    selectedShiftId = -1; // Reset ID jika ada error
                    ClearForm(); // Bersihkan form jika ada error
                }
            }
            else
            {
                selectedShiftId = -1; // Reset jika tidak ada baris yang valid diklik
                ClearForm(); // Bersihkan form
            }
        }
    }
}


