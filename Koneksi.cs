using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms; // Penting untuk MessageBox

namespace pabdproject
{
    internal class Koneksi
    {
        // Variabel statis untuk menyimpan string koneksi dan IP yang terakhir berhasil dibaca.
        // Ini akan di-cache di seluruh aplikasi.
        private static string _cachedConnectionString = null;
        private static string _cachedServerIP = null;
        // Variabel ini akan melacak apakah sudah ada upaya pemuatan koneksi,
        // agar MessageBox error tidak muncul berulang kali jika ada banyak form yang mencoba memuatnya.
        private static bool _hasAttemptedLoad = false;

        // Konstruktor kosong atau untuk inisialisasi lain jika diperlukan oleh instansi kelas Koneksi
        public Koneksi()
        {
            // Tidak ada logika koneksi di sini, karena sudah ditangani oleh metode statis.
        }

        /// <summary>
        /// Mengembalikan string koneksi database yang sudah di-cache.
        /// Jika belum ada di cache, akan mencoba memuatnya dari file konfigurasi.
        /// </summary>
        /// <returns>String koneksi yang valid, atau string kosong jika gagal.</returns>
        public static string GetConnectionString()
        {
            if (_cachedConnectionString != null && _cachedConnectionString != string.Empty)
            {
                return _cachedConnectionString;
            }

            _hasAttemptedLoad = true;
            return LoadAndBuildConnectionString();
        }

        /// <summary>
        /// Memaksa sistem untuk memuat ulang string koneksi dari file konfigurasi.
        /// Ini harus dipanggil setelah file server_config.txt diubah.
        /// </summary>
        /// <returns>String koneksi yang baru dimuat, atau string kosong jika gagal.</returns>
        public static string RefreshConnectionString()
        {
            _cachedConnectionString = null;
            _cachedServerIP = null;
            _hasAttemptedLoad = false;
            return LoadAndBuildConnectionString();
        }

        /// <summary>
        /// Metode internal untuk memuat IP dari file konfigurasi, membangun string koneksi, dan meng-cache-nya.
        /// </summary>
        /// <returns>String koneksi yang sudah dibangun, atau string kosong jika gagal.</returns>
        private static string LoadAndBuildConnectionString()
        {
            string connectStr = "";
            try
            {
                string serverIP = GetServerIPFromConfig();

                if (!serverIP.Contains(":"))
                {
                    serverIP += ",1433";
                }

                connectStr = $"Server={serverIP};Initial Catalog=MANDAK;" +
                             $"User ID=sa;Password=admin123; TrustServerCertificate=true;";

                _cachedConnectionString = connectStr;
                _cachedServerIP = serverIP;

                return connectStr;
            }
            catch (FileNotFoundException ex)
            {
                if (!_hasAttemptedLoad)
                {
                    string expectedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MANDAK", "server_config.txt");
                    MessageBox.Show("File konfigurasi koneksi database tidak ditemukan!\n" +
                                    "Mohon buat file 'server_config.txt' di:\n" + expectedPath +
                                    "\ndan isi dengan IP Address server SQL Anda (misal: 192.168.1.100).",
                                    "Kesalahan Konfigurasi Aplikasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Console.WriteLine("Error: " + ex.ToString());
                _cachedConnectionString = string.Empty;
                return string.Empty;
            }
            catch (Exception ex)
            {
                if (!_hasAttemptedLoad)
                {
                    MessageBox.Show("Gagal membaca atau memvalidasi IP server dari file konfigurasi:\n" + ex.Message +
                                    "\nMohon periksa isi file 'server_config.txt' dan pastikan format IP Address sudah benar.",
                                    "Kesalahan Konfigurasi Aplikasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Console.WriteLine("Error: " + ex.ToString());
                _cachedConnectionString = string.Empty;
                return string.Empty;
            }
        }

        /// <summary>
        /// Mengambil IP Address server dari file konfigurasi. Jika file tidak ada, akan membuatnya dengan IP default.
        /// </summary>
        /// <returns>IP Address server yang dibaca atau default.</returns>
        /// <exception cref="Exception">Dilemparkan jika ada masalah saat membaca atau membuat file, atau format IP tidak valid.</exception>
        private static string GetServerIPFromConfig()
        {
            string configFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MANDAK");
            string configPath = Path.Combine(configFolder, "server_config.txt");

            // Pastikan folder konfigurasi ada
            if (!Directory.Exists(configFolder))
            {
                Directory.CreateDirectory(configFolder);
            }

            // Jika file konfigurasi tidak ada, buat dengan IP default
            if (!File.Exists(configPath))
            {
                string defaultIp = "192.168.100.32";
                try
                {
                    File.WriteAllText(configPath, defaultIp);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Gagal membuat file konfigurasi default: {ex.Message}");
                }
            }

            string ip = File.ReadAllText(configPath).Trim();

            if (string.IsNullOrEmpty(ip))
            {
                throw new Exception("Isi file konfigurasi 'server_config.txt' kosong.");
            }

            
            if (!IsValidIPv4(ip))
            {
                throw new Exception("Format IP tidak valid. Harap masukkan IP Address dengan format IPv4 (contoh: 192.168.100.32).");
            }
            // ------------------------------------------------------------------------------------

            return ip;
        }

        /// <summary>
        /// Memvalidasi apakah sebuah string adalah format IP Address IPv4 yang valid.
        /// </summary>
        /// <param name="ipString">String IP Address untuk divalidasi.</param>
        /// <returns>True jika valid IPv4, False jika tidak.</returns>
        public static bool IsValidIPv4(string ipString) // <--- INI PERBAIKANNYA: UBAH DARI 'private' MENJADI 'public'
        {
            string ipPart = ipString.Split(':')[0].Split(',')[0].Trim();

            if (IPAddress.TryParse(ipPart, out IPAddress ip))
            {
                return ip.AddressFamily == AddressFamily.InterNetwork;
            }
            return false;
        }

        /// <summary>
        /// Mengambil IP Address lokal IPv4 dari PC yang menjalankan aplikasi.
        /// </summary>
        /// <returns>IP Address lokal IPv4.</returns>
        /// <exception cref="Exception">Dilemparkan jika tidak ada IP Address lokal IPv4 yang valid ditemukan.</exception>
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Tidak dapat menemukan IP Address lokal yang valid.");
        }
    }
}