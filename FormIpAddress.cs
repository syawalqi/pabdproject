using System;
using System.IO;
using System.Windows.Forms;
using System.Net; // Ditambahkan untuk IPAddress.TryParse

namespace pabdproject
{
    public partial class FormIpAddress : Form
    {
        // Properti untuk jalur file konfigurasi, bisa diakses dari metode mana pun
        private readonly string configFolder;
        private readonly string configPath;

        public FormIpAddress()
        {
            InitializeComponent();

            // Inisialisasi jalur file konfigurasi
            configFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MANDAK");
            configPath = Path.Combine(configFolder, "server_config.txt");

            // Mengikat event handler secara programatik (alternatif jika tidak terhubung via desainer)
            this.Load += new EventHandler(FormIpAddress_Load);

            // Pastikan event Click tombol "Save" terhubung ke btnSave_Click
            // Pastikan event Click tombol "Get" terhubung ke btnGetLocalIP_Click
            // Jika Anda sudah menghubungkannya di desainer, baris ini tidak perlu ditulis manual
            // Misalnya: this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
        }

        private void FormIpAddress_Load(object sender, EventArgs e)
        {
            LoadCurrentIP();
        }

        private void LoadCurrentIP()
        {
            try
            {
                // Pastikan folder ada sebelum mencoba membaca
                if (!Directory.Exists(configFolder))
                {
                    Directory.CreateDirectory(configFolder);
                }

                // Jika file ada, baca isinya
                if (File.Exists(configPath))
                {
                    txtIpAddress.Text = File.ReadAllText(configPath).Trim(); // Pastikan TextBox bernama txtIpAddress
                }
                else
                {
                    // Jika file tidak ada, tampilkan IP default dan berikan informasi
                    string defaultIp = "192.168.100.32"; // IP default dari Koneksi.cs
                    txtIpAddress.Text = defaultIp;
                    MessageBox.Show("File konfigurasi IP tidak ditemukan. Menggunakan IP default: " + defaultIp + ". Silakan simpan untuk konfirmasi.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat IP saat ini: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtIpAddress.Text = "Error Memuat IP"; // Menampilkan pesan error di TextBox
            }
        }

        // Event handler untuk tombol "Save" (Pastikan nama metode sesuai dengan yang terhubung di desainer)
        private void btnSave_Click(object sender, EventArgs e) // Saya menggunakan nama btnSave sesuai screenshot
        {
            string newIp = txtIpAddress.Text.Trim();

            if (string.IsNullOrEmpty(newIp))
            {
                MessageBox.Show("IP Address tidak boleh kosong.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Memanggil metode statis IsValidIPv4 dari kelas Koneksi untuk validasi
            if (!Koneksi.IsValidIPv4(newIp))
            {
                MessageBox.Show("Format IP tidak valid. Harap masukkan IP Address dengan format IPv4 (contoh: 192.168.1.100).", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Pastikan folder ada sebelum menulis
                if (!Directory.Exists(configFolder))
                {
                    Directory.CreateDirectory(configFolder);
                }

                File.WriteAllText(configPath, newIp);
                MessageBox.Show("IP Address berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // --- PENTING: Memaksa Koneksi.cs untuk memuat ulang connection string ---
                Koneksi.RefreshConnectionString(); // Memanggil metode statis untuk refresh cache
                // ----------------------------------------------------------------------

                // Tutup form ini setelah IP disimpan
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan IP Address: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler untuk tombol "Get" (Pastikan nama metode sesuai dengan yang terhubung di desainer)
        private void btnGetLocalIP_Click(object sender, EventArgs e) // Saya menggunakan nama btnGetLocalIP
        {
            try
            {
                string localIp = Koneksi.GetLocalIPAddress(); // Memanggil metode statis dari Koneksi
                txtIpAddress.Text = localIp;
                MessageBox.Show("IP Lokal Anda: " + localIp, "Info IP Lokal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mendapatkan IP Lokal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler kosong untuk TextBox (jika ada, agar tidak error jika di-refer di desainer)
        private void txtIpAddress_TextChanged(object sender, EventArgs e)
        {
            // Tidak perlu logika di sini jika hanya untuk menampilkan dan menyimpan.
        }

        private void btnSaveIP_Click(object sender, EventArgs e)
        {

        }

        private void FormIpAddress_Load_1(object sender, EventArgs e)
        {

        }
    }
}