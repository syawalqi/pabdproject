// File: FormCutiPengajuan.cs
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace pabdproject
{
    public partial class FormCutiPengajuan : Form
    {
        private string connectionString = "Data Source=BILLAAA\\SA; Initial Catalog=MANDAK;Integrated Security=True";
        private int loggedInKaryawanID;
        private string loggedInKaryawanName;
        private string userRole;
        private int selectedCutiIdForAdminUpdate = -1; // To store the ID_Cuti when admin clicks a row
       

        // Constructor that accepts user role
        public FormCutiPengajuan(string role)
        {
            InitializeComponent();
            userRole = role;

            loggedInKaryawanID = Form3.LoggedInUserID;
            loggedInKaryawanName = Form3.LoggedInUserName;

            // Apply styling
            ApplyCutiFormStyling();

            // Set up UI based on role immediately after initialization
            SetupUIForRole();
        }

        private void FormCutiPengajuan_Load(object sender, EventArgs e)
        {
            LoadCutiData();
        }

        private void SetupUIForRole()
        {
            // Common settings for date pickers (initial state)
            dtpTanggalMulai.MinDate = DateTime.Today;
            dtpTanggalSelesai.MinDate = DateTime.Today;
            dtpTanggalMulai.Value = DateTime.Today;
            dtpTanggalSelesai.Value = DateTime.Today;

            // Populate ComboBox for Jenis Cuti
            cmbJenisCuti.Items.Clear();
            cmbJenisCuti.Items.AddRange(new string[] { "Tahunan", "Sakit", "Pernikahan", "Duka Cita", "Melahirkan", "Hamil", "Acara Keluarga", "Lain-lain" });
            if (cmbJenisCuti.Items.Count > 0)
            {
                cmbJenisCuti.SelectedIndex = 0;
            }

            // Populate ComboBox for Status Approval
            cmbStatus_Cuti.Items.Clear();
            cmbStatus_Cuti.Items.AddRange(new string[] { "Menunggu", "Disetujui", "Ditolak" });
            if (cmbStatus_Cuti.Items.Count > 0)
            {
                cmbStatus_Cuti.SelectedIndex = 0;
            }

            if (userRole == "employee")
            {

                lblJenisCuti.Visible = true;
                cmbJenisCuti.Visible = true;
                lblTanggalMulai.Visible = true;
                dtpTanggalMulai.Visible = true;
                lblTanggalSelesai.Visible = true;
                dtpTanggalSelesai.Visible = true;
                lblKeterangan_Cuti.Visible = true;
                txtKeterangan_Cuti.Visible = true;
                btnSubmitCuti.Visible = true;

                lblStatus_Cuti.Visible = false;
                cmbStatus_Cuti.Visible = false;
                btnUpdateStatusCuti.Visible = false;
                labelSelectedCutiID.Visible = false;
                txtPILIH.Visible = false;

                dtpTanggalMulai.MinDate = DateTime.Today.AddDays(5);
                dtpTanggalSelesai.MinDate = DateTime.Today.AddDays(5);
                dtpTanggalMulai.Value = DateTime.Today.AddDays(5);
                dtpTanggalSelesai.Value = DateTime.Today.AddDays(5);
            }
            else if (userRole == "admin")
            {

                lblJenisCuti.Visible = false;
                cmbJenisCuti.Visible = false;
                lblTanggalMulai.Visible = false;
                dtpTanggalMulai.Visible = false;
                lblTanggalSelesai.Visible = false;
                dtpTanggalSelesai.Visible = false;
                lblKeterangan_Cuti.Visible = false;
                txtKeterangan_Cuti.Visible = false;
                btnSubmitCuti.Visible = false;

                lblStatus_Cuti.Visible = true;
                cmbStatus_Cuti.Visible = true;
                btnUpdateStatusCuti.Visible = true;
                labelSelectedCutiID.Visible = true;
                txtPILIH.Visible = true;
            }

            txtPILIH.Text = string.Empty;
        }

        private void ApplyCutiFormStyling()
        {
 
            foreach (Control control in this.Controls)
            {
                if (dgvCuti != null)
                {
                    dgvCuti.Font = new Font("Segoe UI", 9);
                    dgvCuti.BackgroundColor = Color.WhiteSmoke;
                    dgvCuti.DefaultCellStyle.BackColor = Color.AliceBlue;
                    dgvCuti.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan;
                    dgvCuti.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkCyan;
                    dgvCuti.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dgvCuti.EnableHeadersVisualStyles = false;
                }
            }
            
        }

        private void LoadCutiData()
        {
            DataTable dtCuti = new DataTable();
            string query;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (userRole == "employee")
                {
                    // Karyawan hanya melihat pengajuan cuti mereka sendiri
                    // dengan status 'Menunggu', 'Disetujui', 'Ditolak'
                    // DAN Tanggal_Selesai cuti belum lewat dari hari ini
                    query = @"SELECT ID_Cuti, Jenis_Cuti, Tanggal_Mulai, Tanggal_Selesai, Keterangan_Cuti, Status_Persetujuan, Tanggal_Pengajuan
                              FROM Cuti
                              WHERE ID_Karyawan = @IDKaryawan
                                AND Status_Persetujuan IN ('Menunggu', 'Disetujui', 'Ditolak')
                                AND Tanggal_Selesai >= GETDATE()
                              ORDER BY Tanggal_Pengajuan DESC";
                }
                else // admin
                {
                    // Admin melihat semua pengajuan cuti dengan nama karyawan
                    query = @"SELECT c.ID_Cuti, k.Nama AS Nama_Karyawan, c.Jenis_Cuti, c.Tanggal_Mulai, c.Tanggal_Selesai, c.Keterangan_Cuti, c.Status_Persetujuan, c.Tanggal_Pengajuan
                              FROM Cuti c
                              INNER JOIN Karyawan k ON c.ID_Karyawan = k.ID_Karyawan ORDER BY c.Tanggal_Pengajuan DESC";
                }

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                if (userRole == "employee")
                {
                    da.SelectCommand.Parameters.AddWithValue("@IDKaryawan", loggedInKaryawanID);
                }

                try
                {
                    conn.Open();
                    da.Fill(dtCuti);
                    dgvCuti.DataSource = dtCuti;

                    // Auto-resize columns for better viewing
                    dgvCuti.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                    // If admin, make ID_Cuti and Nama_Karyawan columns read-only for selection
                    if (userRole == "admin")
                    {
                        if (dgvCuti.Columns.Contains("ID_Cuti"))
                            dgvCuti.Columns["ID_Cuti"].ReadOnly = true;
                        if (dgvCuti.Columns.Contains("Nama_Karyawan"))
                            dgvCuti.Columns["Nama_Karyawan"].ReadOnly = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading leave data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSubmitCuti_Click(object sender, EventArgs e)
        {
            if (loggedInKaryawanID == -1)
            {
                MessageBox.Show("ID Karyawan tidak valid. Silakan login kembali.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbJenisCuti.SelectedItem == null || string.IsNullOrWhiteSpace(txtKeterangan_Cuti.Text))
            {
                MessageBox.Show("Mohon lengkapi Jenis Cuti dan Keterangan Cuti.", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime tanggalMulai = dtpTanggalMulai.Value.Date;
            DateTime tanggalSelesai = dtpTanggalSelesai.Value.Date;
            string jenisCuti = cmbJenisCuti.SelectedItem.ToString();
            string keteranganCuti = txtKeterangan_Cuti.Text.Trim();

            // Validasi tanggal (sudah ada di SP, tapi baik juga divalidasi di UI)
            if (tanggalMulai > tanggalSelesai)
            {
                MessageBox.Show("Tanggal mulai cuti tidak boleh lebih dari tanggal selesai cuti.", "Validasi Tanggal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tanggalMulai < DateTime.Today.AddDays(5))
            {
                MessageBox.Show("Pengajuan cuti harus setidaknya 5 hari dari hari ini.", "Validasi Tanggal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("AjukanCuti", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Karyawan", loggedInKaryawanID);
                        cmd.Parameters.AddWithValue("@Jenis_Cuti", jenisCuti);
                        cmd.Parameters.AddWithValue("@Tanggal_Mulai", tanggalMulai);
                        cmd.Parameters.AddWithValue("@Tanggal_Selesai", tanggalSelesai);
                        cmd.Parameters.AddWithValue("@Keterangan_Cuti", keteranganCuti);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Pengajuan cuti berhasil diajukan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFormInput();
                        LoadCutiData();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Terjadi kesalahan saat mengajukan cuti: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan tak terduga: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbStatus_Cuti_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tidak perlu ada logika di sini saat ini, karena perubahan status dilakukan via tombol "Update Status Cuti"
        }

        // Metode btnBatalCuti_Click sudah dihapus, karena tombolnya juga dihapus.

        private void ClearFormInput()
        {
            if (cmbJenisCuti.Items.Count > 0)
            {
                cmbJenisCuti.SelectedIndex = 0;
            }
            // Reset dates based on role
            if (userRole == "employee")
            {
                dtpTanggalMulai.Value = DateTime.Today.AddDays(5);
                dtpTanggalSelesai.Value = DateTime.Today.AddDays(5);
            }
            else // Admin doesn't interact with these directly, but good to reset
            {
                dtpTanggalMulai.Value = DateTime.Today;
                dtpTanggalSelesai.Value = DateTime.Today;
            }

            txtKeterangan_Cuti.Text = string.Empty;
            if (cmbStatus_Cuti.Items.Count > 0)
            {
                cmbStatus_Cuti.SelectedIndex = 0;
            }
            selectedCutiIdForAdminUpdate = -1;
            txtPILIH.Text = string.Empty;
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(userRole); // Membuat instance Form2 baru dengan role pengguna
            form2.Show(); // Menampilkan Form2
            this.Close(); // Menutup form saat ini
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCutiData(); // Memuat ulang data cuti dari database
            ClearFormInput(); // Mengosongkan input form setelah refresh
        }

        private void dgvCuti_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCuti.Rows[e.RowIndex];

                // Always try to get and display ID_Cuti in txtPILIH if column exists
                if (dgvCuti.Columns.Contains("ID_Cuti") && row.Cells["ID_Cuti"].Value != null)
                {
                    txtPILIH.Text = row.Cells["ID_Cuti"].Value.ToString();
                    int.TryParse(row.Cells["ID_Cuti"].Value.ToString(), out selectedCutiIdForAdminUpdate);
                }
                else
                {
                    txtPILIH.Text = string.Empty; // Clear if ID_Cuti column not found or value is null
                    selectedCutiIdForAdminUpdate = -1;
                }


                if (userRole == "admin")
                {
                    // Admin melihat detail cuti yang dipilih untuk di-approve/reject
                    if (dgvCuti.Columns.Contains("Status_Persetujuan") && row.Cells["Status_Persetujuan"].Value != null)
                    {
                        string currentStatus = row.Cells["Status_Persetujuan"].Value.ToString();
                        int index = cmbStatus_Cuti.FindStringExact(currentStatus);
                        if (index != -1)
                        {
                            cmbStatus_Cuti.SelectedIndex = index;
                        }
                    }
                    // Admin juga bisa melihat keterangan cuti dari karyawan
                    if (dgvCuti.Columns.Contains("Keterangan_Cuti") && row.Cells["Keterangan_Cuti"].Value != null)
                    {
                        // Anda bisa menampilkan ini di MessageBox atau di kontrol lain jika perlu
                        // Misalnya: MessageBox.Show("Keterangan Cuti: " + row.Cells["Keterangan_Cuti"].Value.ToString(), "Detail Cuti");
                        // Atau jika Anda punya textbox tersembunyi untuk admin: txtAdminKeterangan.Text = row.Cells["Keterangan_Cuti"].Value.ToString();
                    }
                }
                else if (userRole == "employee")
                {
                    // Employee hanya melihat cuti mereka, dan mungkin mengisi ulang form
                    // untuk kemudahan melihat detail, bukan untuk mengedit/membatalkan (karena fungsi batal dihapus)
                    if (dgvCuti.Columns.Contains("Jenis_Cuti") && row.Cells["Jenis_Cuti"].Value != null)
                        cmbJenisCuti.SelectedItem = row.Cells["Jenis_Cuti"].Value.ToString();
                    if (dgvCuti.Columns.Contains("Tanggal_Mulai") && row.Cells["Tanggal_Mulai"].Value != null)
                        dtpTanggalMulai.Value = Convert.ToDateTime(row.Cells["Tanggal_Mulai"].Value);
                    if (dgvCuti.Columns.Contains("Tanggal_Selesai") && row.Cells["Tanggal_Selesai"].Value != null)
                        dtpTanggalSelesai.Value = Convert.ToDateTime(row.Cells["Tanggal_Selesai"].Value);
                    if (dgvCuti.Columns.Contains("Keterangan_Cuti") && row.Cells["Keterangan_Cuti"].Value != null)
                        txtKeterangan_Cuti.Text = row.Cells["Keterangan_Cuti"].Value.ToString();
                }
            }
            else
            {
                // Jika tidak ada baris yang valid diklik (misalnya header)
                ClearFormInput(); // Membersihkan input form
            }
        }

        private void btnUpdateStatusCuti_Click(object sender, EventArgs e)
        {
            if (userRole != "admin")
            {
                MessageBox.Show("Anda tidak memiliki izin untuk melakukan tindakan ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedCutiIdForAdminUpdate == -1)
            {
                MessageBox.Show("Mohon pilih pengajuan cuti yang akan diperbarui statusnya di tabel.", "Tidak Ada Pilihan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbStatus_Cuti.SelectedItem == null)
            {
                MessageBox.Show("Mohon pilih status persetujuan baru.", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newStatus = cmbStatus_Cuti.SelectedItem.ToString();

            // Opsional: Tambahkan input untuk Keterangan_Approval (misalnya, jika status ditolak)
            // Untuk saat ini, kita akan menggunakan string kosong atau null jika tidak ada input khusus.
            string keteranganApproval = string.Empty;
            if (newStatus == "Ditolak")
            {
                // Anda bisa menambahkan prompt input di sini, contoh:
                // InputBox dialog = new InputBox("Alasan Penolakan:", "Masukkan alasan penolakan:");
                // if (dialog.ShowDialog() == DialogResult.OK)
                // {
                //     keteranganApproval = dialog.InputText;
                // }
                // else
                // {
                //     return; // Jika admin membatalkan input alasan, batalkan juga update
                // }
                // Untuk sekarang, kita bisa biarkan kosong atau berikan default
                keteranganApproval = "Tidak ada alasan spesifik."; // Atau biarkan kosong
            }


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateStatusCuti", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Cuti", selectedCutiIdForAdminUpdate);
                        cmd.Parameters.AddWithValue("@NewStatus", newStatus);
                        cmd.Parameters.AddWithValue("@Keterangan_Approval", (object)keteranganApproval ?? DBNull.Value); // Bisa NULL
                        cmd.Parameters.AddWithValue("@ID_Approver", loggedInKaryawanID); // Admin yang sedang login

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Status cuti berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFormInput();
                        LoadCutiData(); // Refresh DataGridView
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Terjadi kesalahan saat memperbarui status cuti: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan tak terduga: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPILIH_TextChanged(object sender, EventArgs e)
        {

        }
    }
}