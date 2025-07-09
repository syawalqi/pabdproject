using Microsoft.Reporting.WinForms;
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
    public partial class exportreport: Form
    {
        private string strKonek;
        string connect = ""; // Deklarasikan variabel untuk menyimpan string koneksi
        private readonly string userRole;
        public exportreport()
        {
            InitializeComponent();
            strKonek = Koneksi.GetConnectionString();
        }

        private void exportreport_Load(object sender, EventArgs e)
        {
            // Setup ReportViewer data
            SetupReportViewer();
            // Refresh report to display data
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        private void SetupReportViewer()
        {
           

            // SQL query to retrieve the required data from the database
            string query = @"
               SELECT h.ID_Kehadiran, h.ID_Karyawan, k.Nama, k.Jabatan, k.Departemen, h.Waktu_Masuk, h.Waktu_Keluar, h.Status
                    FROM Kehadiran h
                    INNER JOIN Karyawan k ON h.ID_Karyawan = k.ID_Karyawan";

            // Create a DataTable to store the data
            DataTable dt = new DataTable();

            // Use SqlDataAdapter to fill the DataTable with data from the database
            using (SqlConnection conn = new SqlConnection(connect))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            // Create a ReportDataSource
            ReportDataSource rds = new ReportDataSource("DataSet1", dt); // Make sure "DataSet1" matches your RDLC dataset name

            // Clear any existing data sources and add the new data source
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            // Set the path to the report (.rdlc file)
            // Change this to the actual path of your RDLC file
            reportViewer1.LocalReport.ReportPath = @"D:\TugasKampus\SEM4\PABD\pabdproject\laporanattendance.rdlc";


            // Refresh the ReportViewer to show the updated report
            reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5(userRole);
            form5.Show();
            this.Hide();
        }
    }
}
