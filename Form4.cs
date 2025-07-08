using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pabdproject
{
    public partial class Form4 : Form
    {
        private readonly string userRole;
        private readonly string connectionString = "Data Source=LAPTOP-PFIH6R5H\\GALIHMAULANA; Initial Catalog=MANDAK;Integrated Security=True";

        private readonly MemoryCache _cache = MemoryCache.Default;
        private const string CacheKey = "KaryawanData";

        public Form4(string role)
        {
            InitializeComponent();
            userRole = role;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            if (userRole == "employee")
                // Tombol Hapus dinonaktifkan untuk role employee
                bttnHapus.Enabled = false;

            LoadKaryawanData();
        }

        private void LoadKaryawanData()
        {
            DataTable dt = _cache.Get(CacheKey) as DataTable;

            if (dt == null)
            {
                string query = "SELECT ID_Karyawan, Nama, Jabatan, Departemen, Tanggal_Masuk, Role FROM Karyawan";

                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        dt = new DataTable();
                        da.Fill(dt);
                        _cache.Set(CacheKey, dt, new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return;
                }
            }

            dataGridView1.DataSource = dt;
        }

        // Tombol Kembali
        private void button1_Click(object sender, EventArgs e)
        {
            new Form2(userRole).Show();
            this.Hide();
        }

        // Tombol Hapus
        private void bttnHapus_Click(object sender, EventArgs e)
        {
            if (userRole == "employee")
            {
                MessageBox.Show("Anda tidak memiliki izin untuk menghapus data.");
                return;
            }

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data karyawan yang akan dihapus.");
                return;
            }

            DialogResult confirm = MessageBox.Show("Yakin ingin menghapus data karyawan ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                int idKaryawan = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_Karyawan"].Value);

                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    using (SqlCommand cmd = new SqlCommand("HapusKaryawan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Karyawan", idKaryawan);
                        conn.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Data karyawan berhasil dihapus.");
                            _cache.Remove(CacheKey);
                            LoadKaryawanData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // Tombol Tambah
        private void bttnTambah_Click(object sender, EventArgs e)
        {
            ShowAddKaryawanPopup();
        }

        // Tombol Impor
        private void button5_Click(object sender, EventArgs e)
        {
            using (var openFile = new OpenFileDialog { Filter = "Excel Files|*.xlsx;*.xlsm" })
            {
                if (openFile.ShowDialog() == DialogResult.OK)
                    PreviewData(openFile.FileName);
            }
        }

        // --- PERUBAHAN DI SINI ---
        // Tombol Refresh, sekarang juga menampilkan analisis data
        private void button6_Click(object sender, EventArgs e)
        {
            // Mulai stopwatch
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            // 1. Hapus cache & muat ulang data dari database
            _cache.Remove(CacheKey);
            LoadKaryawanData();

            // 2. Analisis data
            if (dataGridView1.DataSource is DataTable dt)
            {
                int totalRows = dt.Rows.Count;
                int adminCount = dt.AsEnumerable().Count(row => row.Field<string>("Role") == "admin");
                int employeeCount = dt.AsEnumerable().Count(row => row.Field<string>("Role") == "employee");

                // 3. Stop timing
                stopwatch.Stop();
                long elapsed = stopwatch.ElapsedMilliseconds;

                MessageBox.Show(
                    $"Total Data: {totalRows}\nAdmin: {adminCount}\nEmployee: {employeeCount}\n\nWaktu load: {elapsed} ms",
                    "Analisis Data Karyawan + Timing",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                MessageBox.Show("Data belum dimuat atau tidak valid.");
            }
        }


        // Tombol Analisis SQL (Fungsi tidak berubah, sudah benar)
        private void btnAnalisis_Click(object sender, EventArgs e)
        {
            var statsBuilder = new StringBuilder();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.InfoMessage += (obj, args) => {
                    statsBuilder.AppendLine(args.Message);
                };

                string query = "SELECT ID_Karyawan, Nama, Jabatan, Departemen, Tanggal_Masuk, Role FROM Karyawan";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();

                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SET STATISTICS IO ON; SET STATISTICS TIME ON;", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    dataAdapter.Fill(dataTable);

                    using (SqlCommand cmd = new SqlCommand("SET STATISTICS IO OFF; SET STATISTICS TIME OFF;", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show(
                        statsBuilder.ToString(),
                        "STATISTICS INFO",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat analisis: " + ex.Message);
                }
            }
        }

        // Fix for CS0136: Renaming the local function parameter 'e' to avoid conflict with the enclosing scope's 'e'.
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silakan pilih baris data karyawan terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dataGridView1.SelectedRows[0];
            int idKaryawan = Convert.ToInt32(selectedRow.Cells["ID_Karyawan"].Value);
            string nama = selectedRow.Cells["Nama"].Value.ToString();
            string jabatan = selectedRow.Cells["Jabatan"].Value.ToString();
            string departemen = selectedRow.Cells["Departemen"].Value.ToString();
            DateTime tanggalMasuk = Convert.ToDateTime(selectedRow.Cells["Tanggal_Masuk"].Value);

            Form popup = new Form { Width = 400, Height = 300, Text = "Update Karyawan" };

            
            void HanyaHuruf(object senderKeyPress, KeyPressEventArgs keyPressEventArgs)
            {
                if (!char.IsControl(keyPressEventArgs.KeyChar) && !char.IsLetter(keyPressEventArgs.KeyChar) && !char.IsWhiteSpace(keyPressEventArgs.KeyChar))
                {
                    keyPressEventArgs.Handled = true;
                }
            }

            TextBox txtNama = new TextBox { Text = nama };
            txtNama.KeyPress += HanyaHuruf;

            TextBox txtJabatan = new TextBox { Text = jabatan };
            txtJabatan.KeyPress += HanyaHuruf;

            TextBox txtDepartemen = new TextBox { Text = departemen };
            txtDepartemen.KeyPress += HanyaHuruf;
            DateTimePicker dtTanggal = new DateTimePicker { Value = tanggalMasuk, Format = DateTimePickerFormat.Short, MinDate = DateTime.Today.AddYears(-5), MaxDate = DateTime.Today.AddYears(1) };

            Button btnSimpan = new Button { Text = "Simpan" };
            btnSimpan.Click += (s, ev) =>
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("UpdateKaryawan", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_Karyawan", idKaryawan);
                    cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                    cmd.Parameters.AddWithValue("@Jabatan", txtJabatan.Text.Trim());
                    cmd.Parameters.AddWithValue("@Departemen", txtDepartemen.Text.Trim());
                    cmd.Parameters.AddWithValue("@Tanggal_Masuk", dtTanggal.Value);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil diupdate.");
                        popup.Close();
                        _cache.Remove(CacheKey);
                        LoadKaryawanData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal mengupdate data: " + ex.Message);
                    }
                }
            };

            AddFormRow(popup, "Nama", txtNama, 20);
            AddFormRow(popup, "Jabatan", txtJabatan, 60);
            AddFormRow(popup, "Departemen", txtDepartemen, 100);
            AddFormRow(popup, "Tanggal Masuk", dtTanggal, 140);
            popup.Controls.Add(btnSimpan);
            btnSimpan.SetBounds(150, 200, 100, 30);

            popup.ShowDialog();
        }

        #region Helper Methods
        private void ShowAddKaryawanPopup()
        {
            Form popup = new Form { Width = 400, Height = 400, Text = "Add New Karyawan" };

            TextBox txtNama = new TextBox(), txtJabatan = new TextBox(), txtDepartemen = new TextBox(), txtPassword = new TextBox();
            txtNama.KeyPress += OnlyLetters_KeyPress;
            txtJabatan.KeyPress += OnlyLetters_KeyPress;
            txtDepartemen.KeyPress += OnlyLetters_KeyPress;

            DateTimePicker dtTanggal = new DateTimePicker { Format = DateTimePickerFormat.Short, MinDate = DateTime.Today.AddYears(-5), MaxDate = DateTime.Today.AddYears(1) };
            ComboBox cmbRole = new ComboBox();
            cmbRole.Items.AddRange(new[] { "employee", "admin" });
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.SelectedIndex = 0;

            Button btnSave = new Button { Text = "Save" };
            btnSave.Click += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(txtNama.Text) || string.IsNullOrWhiteSpace(txtJabatan.Text) || string.IsNullOrWhiteSpace(txtDepartemen.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }
                if (txtPassword.Text.Trim().Length < 8)
                {
                    MessageBox.Show("Password must be at least 8 characters long.");
                    return;
                }

                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    using (SqlCommand cmd = new SqlCommand("TambahKaryawan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@Jabatan", txtJabatan.Text.Trim());
                        cmd.Parameters.AddWithValue("@Departemen", txtDepartemen.Text.Trim());
                        cmd.Parameters.AddWithValue("@Tanggal_Masuk", dtTanggal.Value);
                        cmd.Parameters.AddWithValue("@Passwd", txtPassword.Text.Trim());
                        cmd.Parameters.AddWithValue("@Role", cmbRole.SelectedItem.ToString());

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Karyawan added successfully.");
                        popup.Close();
                        _cache.Remove(CacheKey);
                        LoadKaryawanData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting data: " + ex.Message);
                }
            };

            AddFormRow(popup, "Nama", txtNama, 20);
            AddFormRow(popup, "Jabatan", txtJabatan, 60);
            AddFormRow(popup, "Departemen", txtDepartemen, 100);
            AddFormRow(popup, "Tanggal Masuk", dtTanggal, 140);
            AddFormRow(popup, "Password", txtPassword, 180);
            AddFormRow(popup, "Role", cmbRole, 220);
            popup.Controls.Add(btnSave);
            btnSave.SetBounds(150, 270, 100, 30);

            popup.ShowDialog();
        }

        private void PreviewData(string filePath)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook = new XSSFWorkbook(fs);
                    ISheet sheet = workbook.GetSheetAt(0);
                    DataTable dt = new DataTable();

                    IRow headerRow = sheet.GetRow(0);
                    foreach (var cell in headerRow.Cells)
                        dt.Columns.Add(cell?.ToString() ?? $"Column{cell.ColumnIndex}");

                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;

                        DataRow newRow = dt.NewRow();
                        for (int col = 0; col < dt.Columns.Count; col++)
                            newRow[col] = row.GetCell(col)?.ToString() ?? string.Empty;
                        dt.Rows.Add(newRow);
                    }

                    new PreviewImportForm(dt).ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading Excel file: " + ex.Message);
            }
        }

        private void OnlyLetters_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void AddFormRow(Form form, string labelText, Control inputControl, int top)
        {
            Label label = new Label { Text = labelText, Left = 10, Top = top, Width = 120 };
            inputControl.Left = 150;
            inputControl.Top = top;
            inputControl.Width = 200;
            form.Controls.Add(label);
            form.Controls.Add(inputControl);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: Can be used for other interactions if needed
        }

        // This button event handler is empty and can be removed from the designer if not used.
        private void button3_Click(object sender, EventArgs e)
        {

        }


        private void SearchKaryawan(string keyword)
        {
            string trimmed = keyword.Trim();
            if (string.IsNullOrWhiteSpace(trimmed))
            {
                LoadKaryawanData();
                return;
            }

            string query = @"
                SELECT ID_Karyawan, Nama, Jabatan, Departemen, Tanggal_Masuk, Role
                FROM Karyawan
                WHERE Nama LIKE @kw
                   OR Jabatan LIKE @kw
                   OR Departemen LIKE @kw
                   OR Role LIKE @kw;
                ";

            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@kw", trimmed + "%");

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during search: " + ex.Message);
            }
        }


        private void btnCari_Click_1(object sender, EventArgs e)
        {
            SearchKaryawan(txtSearch.Text);
        }

        #endregion
    }
}