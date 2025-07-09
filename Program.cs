using System;
using System.Windows.Forms;

namespace pabdproject
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Buat instance FormIpAddress terlebih dahulu
            FormIpAddress ipForm = new FormIpAddress();
            // Tampilkan sebagai dialog modal, sehingga pengguna harus menutupnya
            // sebelum form lain bisa ditampilkan.
            ipForm.ShowDialog();

            // Setelah FormIpAddress ditutup, baru jalankan aplikasi dengan Form3 (Login)
            Application.Run(new Form3());
        }
    }
}