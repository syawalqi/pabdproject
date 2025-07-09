using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace pabdproject
{
    public partial class GrafikGaji : Form
    {
      
        string connect = ""; // Deklarasikan variabel untuk menyimpan string koneksi
        private string userRole;

        public GrafikGaji()
        {
            InitializeComponent();
            connect = Koneksi.GetConnectionString();
        }

        private void GrafikGaji_Load(object sender, EventArgs e)
        {
            cmbJenisAnalisis.Items.AddRange(new string[] {
                "Rata-rata Gaji per Departemen",
                "Rata-rata Gaji per Jabatan",
            });
            cmbJenisAnalisis.SelectedIndex = 0; // Pilih opsi pertama sebagai default

            // Panggil LoadGajiChartData dengan pilihan default
            LoadGajiChartData(cmbJenisAnalisis.SelectedItem.ToString());

            // Tambahkan event handler untuk perubahan pilihan ComboBox
            cmbJenisAnalisis.SelectedIndexChanged += CmbJenisAnalisis_SelectedIndexChanged;
        }

        // Event handler saat pilihan ComboBox berubah
        private void CmbJenisAnalisis_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedAnalisis = cmbJenisAnalisis.SelectedItem.ToString();
            LoadGajiChartData(selectedAnalisis); // Panggil ulang method dengan filter baru
        }

        private void LoadGajiChartData(string filterType) // Tambahkan parameter filterType
        {
            // 1. Bersihkan Chart dari data sebelumnya
            chartGaji.Series.Clear();
            chartGaji.Titles.Clear();
            chartGaji.ChartAreas.Clear();

            // 2. Tambahkan ChartArea
            ChartArea ca = new ChartArea("MainArea");
            ca.AxisX.LabelStyle.Angle = -45; // Memutar label agar tidak tumpang tindih
            ca.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            ca.AxisY.Title = "Rata-rata Gaji (Rp)";
            ca.AxisY.LabelStyle.Format = "N0";
            chartGaji.ChartAreas.Add(ca);

            string query = "";
            string chartTitle = "";
            string axisXTitle = "";
            string axisYTitle = "";
            string seriesName = "";
            SeriesChartType chartType = SeriesChartType.Column;
            System.Drawing.Color seriesColor = System.Drawing.Color.SteelBlue; // Warna default

            switch (filterType)
            {
                case "Rata-rata Gaji per Departemen":
                    query = @"
            SELECT
                k.Departemen AS Kategori,
                -- Bagi dengan 1.000.000.000.0 untuk mendapatkan nilai dalam Miliar Rupiah
                AVG(g.Gaji_Pokok + ISNULL(g.Tunjangan, 0) - ISNULL(g.Potongan, 0)) / 1000000000.0 AS Nilai
            FROM Karyawan k
            INNER JOIN Gaji g ON k.ID_Karyawan = g.ID_Karyawan
            GROUP BY k.Departemen
            ORDER BY Nilai DESC;";
                    chartTitle = "Grafik Rata-rata Gaji per Departemen";
                    axisXTitle = "Departemen";
                    axisYTitle = "Rata-rata Gaji (Miliar Rp)"; // Ubah judul sumbu Y untuk menunjukkan unit baru
                    seriesName = "Rata-rata Gaji";
                    seriesColor = System.Drawing.Color.SteelBlue;
                    chartType = SeriesChartType.Column;
                    break;

                case "Rata-rata Gaji per Jabatan":
                    query = @"
            SELECT
                k.Jabatan AS Kategori,
                -- Bagi dengan 1.000.000.000.0 untuk mendapatkan nilai dalam Miliar Rupiah
                AVG(g.Gaji_Pokok + ISNULL(g.Tunjangan, 0) - ISNULL(g.Potongan, 0)) / 1000000000.0 AS Nilai
            FROM Karyawan k
            INNER JOIN Gaji g ON k.ID_Karyawan = g.ID_Karyawan
            GROUP BY k.Jabatan
            ORDER BY Nilai DESC;";
                    chartTitle = "Grafik Rata-rata Gaji per Jabatan";
                    axisXTitle = "Jabatan";
                    axisYTitle = "Rata-rata Gaji (Miliar Rp)"; // Ubah judul sumbu Y
                    seriesName = "Rata-rata Gaji";
                    seriesColor = System.Drawing.Color.ForestGreen;
                    chartType = SeriesChartType.Column;
                    break;

                    // ... case "Total Karyawan per Departemen" dan default tetap sama ...
            }

            // Atur judul sumbu X dan Y berdasarkan tipe filter
            ca.AxisX.Title = axisXTitle;
            ca.AxisY.Title = axisYTitle;

            // 3. Tambahkan Title untuk Chart
            chartGaji.Titles.Add(chartTitle);

            // 4. Buat Series untuk data gaji
            Series sGaji = new Series(seriesName)
            {
                ChartType = chartType, // Tipe grafik berdasarkan pilihan
                Color = seriesColor,   // Warna berdasarkan pilihan
                IsValueShownAsLabel = true // Menampilkan nilai di atas/di chart
            };
            sGaji.ChartArea = "MainArea";

            DataTable dt = new DataTable();

            // 5. Ambil data dari database
            using (SqlConnection conn = new SqlConnection(connect))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading chart data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // 6. Masukkan data ke Series
            foreach (DataRow row in dt.Rows)
            {
                string kategori = row["Kategori"].ToString();
                decimal nilai = Convert.ToDecimal(row["Nilai"]);
                sGaji.Points.AddXY(kategori, nilai);
            }

            // 7. Tambahkan Series ke Chart
            chartGaji.Series.Add(sGaji);

            // 8. Tambahkan Legenda (opsional, tapi baik untuk kejelasan)
            // Khusus Pie Chart, legenda lebih penting
            if (chartType == SeriesChartType.Pie)
            {
                chartGaji.Legends.Add(new Legend("LegendaGaji"));
                // Mengatur format label untuk Pie Chart agar lebih informatif
                sGaji.Label = "#PERCENT{P1}"; // Menampilkan persentase dengan 1 desimal
                sGaji.LegendText = "#VALX (#VALY)"; // Menampilkan kategori dan nilai di legenda
            }
            else
            {
                // Untuk chart kolom, legenda mungkin tidak terlalu diperlukan jika hanya ada 1 series
                // Tetapi tetap bisa ditambahkan jika ingin menunjukkan nama Series
                if (!chartGaji.Legends.Any()) // Hanya tambahkan jika belum ada legenda
                {
                    chartGaji.Legends.Add(new Legend("LegendaGaji"));
                }
            }
        }

        private void cmbJenisAnalisis_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void bttnkembali_Click(object sender, EventArgs e)
        {
            // Pass the role when opening Form2
            Form2 form2 = new Form2(userRole);
            form2.Show();

            this.Hide(); // Hide Form1
        }
    }
}