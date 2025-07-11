﻿using System;
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
using System.Runtime.Caching;

namespace pabdproject
{
    public partial class GajiEmployee : Form
    {
        private readonly string userRole;
        
        string connect = ""; // Deklarasikan variabel untuk menyimpan string koneksi
        private int selectedID_Karyawan = -1;
        private readonly MemoryCache _cache = MemoryCache.Default;
        private const string CacheKey = "GajiData";

        public GajiEmployee(string role)
        {
            InitializeComponent();
            userRole = role;
            // Mengganti event handler ke CellClick untuk pemilihan baris dengan satu klik
            this.dataGajiKaryawan.CellContentClick -= new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGajiKaryawan_CellContentClick);
            this.dataGajiKaryawan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGajiKaryawan_CellClick);
            connect = Koneksi.GetConnectionString();
        }

        private void LoadJoinedData()
        {
            if (_cache.Contains(CacheKey))
            {
                dataGajiKaryawan.DataSource = _cache.Get(CacheKey) as DataTable;
                return;
            }

            using (SqlConnection conn = new SqlConnection(connect))
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

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                try
                {
                    adapter.Fill(dt);
                    _cache.Set(CacheKey, dt, new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) });
                    dataGajiKaryawan.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }

            if (dataGajiKaryawan.Columns["ID_Karyawan"] != null)
                dataGajiKaryawan.Columns["ID_Karyawan"].Visible = false;
        }

        private void GajiEmployee_Load(object sender, EventArgs e)
        {
            LoadJoinedData();
        }

        // Event handler yang digunakan adalah CellClick untuk merespons satu kali klik
        private void dataGajiKaryawan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Memastikan event berasal dari klik pada baris yang valid (bukan header)
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dataGajiKaryawan.Rows[e.RowIndex];
                    var ID_KaryawanCellValue = row.Cells["ID_Karyawan"].Value;

                    // Memastikan sel yang diklik bukan bagian dari baris baru yang kosong
                    if (ID_KaryawanCellValue != null && ID_KaryawanCellValue != DBNull.Value)
                    {
                        selectedID_Karyawan = Convert.ToInt32(ID_KaryawanCellValue);

                        txtNamaKaryawan.Text = row.Cells["Nama"].Value?.ToString() ?? "";
                        txtGajiKaryawan.Text = row.Cells["Gaji_Pokok"].Value?.ToString() ?? "";
                        txtJabatan.Text = row.Cells["Jabatan"].Value?.ToString() ?? "";
                        txtDepartemen.Text = row.Cells["Departemen"].Value?.ToString() ?? "";

                        lblMessage.Text = $"Data karyawan ID {selectedID_Karyawan} siap diubah.";
                    }
                    else
                    {
                        // Membersihkan textbox jika baris yang diklik adalah baris kosong di akhir
                        selectedID_Karyawan = -1;
                        txtNamaKaryawan.Clear();
                        txtGajiKaryawan.Clear();
                        txtJabatan.Clear();
                        txtDepartemen.Clear();
                        lblMessage.Text = "Pilih baris data yang valid.";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat memilih data: " + ex.Message);
                }
            }
        }

        // Ini adalah handler lama, bisa dihapus atau dibiarkan kosong karena sudah tidak terpakai
        private void dataGajiKaryawan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Logika sudah dipindahkan ke dataGajiKaryawan_CellClick
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

            if (string.IsNullOrEmpty(namaKaryawan) || string.IsNullOrEmpty(jabatan) ||
                string.IsNullOrEmpty(departemen) || !decimal.TryParse(gajiStr, out decimal gajiPokok))
            {
                lblMessage.Text = "Pastikan semua kolom diisi dengan benar.";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connect))
            {
                SqlTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    // Update Karyawan using stored procedure
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateKaryawan", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Karyawan", selectedID_Karyawan);
                        cmd.Parameters.AddWithValue("@Nama", namaKaryawan);
                        cmd.Parameters.AddWithValue("@Jabatan", jabatan);
                        cmd.Parameters.AddWithValue("@Departemen", departemen);
                        cmd.ExecuteNonQuery();
                    }


                    // Upsert Gaji using stored procedure
                    using (SqlCommand cmd = new SqlCommand("sp_UpsertGaji", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Karyawan", selectedID_Karyawan);
                        cmd.Parameters.AddWithValue("@Gaji_Pokok", gajiPokok);
                        cmd.ExecuteNonQuery();
                    }

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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _cache.Remove(CacheKey);
            LoadJoinedData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(userRole);
            form2.Show();
            this.Hide();
        }

        // Tombol analisis yang menampilkan statistik performa SQL
        private void bttnAnalisisGaji_Click(object sender, EventArgs e)
        {
            var statsBuilder = new StringBuilder();
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.InfoMessage += (obj, args) => {
                    statsBuilder.AppendLine(args.Message);
                };

                string query = @"
                SELECT k.ID_Karyawan, k.Nama, k.Jabatan, k.Departemen, g.Gaji_Pokok
                FROM Karyawan k LEFT JOIN Gaji g ON k.ID_Karyawan = g.ID_Karyawan";

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
    }
}