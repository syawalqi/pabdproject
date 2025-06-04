using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using System.Windows.Forms;


namespace pabdproject
{
    public partial class Form4 : Form
    {
        private readonly string userRole;
        private readonly string connectionString = "Data Source=LAPTOP-PFIH6R5H\\GALIHMAULANA;Initial Catalog=MANDAK;Integrated Security=True";

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
                button2.Enabled = false; // Disable delete button

            LoadKaryawanData();
        }

        private void LoadKaryawanData()
        {
            // Try to get data from cache first
            DataTable dt = _cache.Get(CacheKey) as DataTable;

            if (dt == null)
            {
                // Cache miss: Load from database
                string query = "SELECT ID_Karyawan, Nama, Jabatan, Departemen, Tanggal_Masuk, Role FROM Karyawan";

                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        dt = new DataTable();
                        da.Fill(dt);

                        // Store in cache for 5 minutes
                        var policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) };
                        _cache.Set(CacheKey, dt, policy);
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


        private void button2_Click(object sender, EventArgs e)
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
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data karyawan berhasil dihapus.");
                            _cache.Remove(CacheKey);  // Clear cache
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

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(userRole);
            form2.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowAddKaryawanPopup();
        }

        private void ShowAddKaryawanPopup()
        {
            Form popup = new Form()
            {
                Width = 400,
                Height = 400,
                Text = "Add New Karyawan"
            };

            // Controls
            TextBox txtNama = new TextBox(), txtJabatan = new TextBox(), txtDepartemen = new TextBox(), txtPassword = new TextBox();
            DateTimePicker dtTanggal = new DateTimePicker() { Format = DateTimePickerFormat.Short };
            dtTanggal.MinDate = DateTime.Today.AddYears(-1);
            dtTanggal.MaxDate = DateTime.Today.AddYears(1);
            ComboBox cmbRole = new ComboBox();
            cmbRole.Items.AddRange(new string[] { "employee", "admin" });
            cmbRole.SelectedIndex = 0;

            Button btnSave = new Button() { Text = "Save" };
            btnSave.Click += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(txtNama.Text) ||
                    string.IsNullOrWhiteSpace(txtJabatan.Text) ||
                    string.IsNullOrWhiteSpace(txtDepartemen.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                // Password length check: minimum 8 characters
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
                        _cache.Remove(CacheKey);  // Clear cache
                        LoadKaryawanData();
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting data: " + ex.Message);
                }
            };

            // Layout
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data karyawan yang akan diupdate.");
                return;
            }

            DataGridViewRow row = dataGridView1.SelectedRows[0];
            int idKaryawan = Convert.ToInt32(row.Cells["ID_Karyawan"].Value);

            Form popup = new Form()
            {
                Width = 450,
                Height = 350,
                Text = "Update Data Karyawan",
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            TextBox txtNama = new TextBox() { Text = row.Cells["Nama"].Value.ToString() };
            TextBox txtJabatan = new TextBox() { Text = row.Cells["Jabatan"].Value.ToString() };
            TextBox txtDepartemen = new TextBox() { Text = row.Cells["Departemen"].Value.ToString() };
            DateTimePicker dtTanggal = new DateTimePicker() { Format = DateTimePickerFormat.Short, Value = Convert.ToDateTime(row.Cells["Tanggal_Masuk"].Value) };

            Button btnUpdate = new Button() { Text = "Update" };
            btnUpdate.Click += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(txtNama.Text) || string.IsNullOrWhiteSpace(txtJabatan.Text) || string.IsNullOrWhiteSpace(txtDepartemen.Text))
                {
                    MessageBox.Show("Semua field harus diisi!");
                    return;
                }

                DialogResult confirm = MessageBox.Show("Apakah Anda yakin ingin mengupdate data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
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

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil diupdate.");
                        popup.Close();
                        _cache.Remove(CacheKey);  // Clear cache
                        LoadKaryawanData();
                    }
                }
            };

            Button btnCancel = new Button() { Text = "Batal" };
            btnCancel.Click += (s, ev) => popup.Close();

            AddFormRow(popup, "Nama", txtNama, 20);
            AddFormRow(popup, "Jabatan", txtJabatan, 60);
            AddFormRow(popup, "Departemen", txtDepartemen, 100);
            AddFormRow(popup, "Tanggal Masuk", dtTanggal, 140);
            popup.Controls.Add(btnUpdate);
            popup.Controls.Add(btnCancel);
            btnUpdate.SetBounds(150, 200, 100, 30);
            btnCancel.SetBounds(260, 200, 100, 30);

            popup.ShowDialog();
        }

        private void AddFormRow(Form form, string labelText, Control inputControl, int top)
        {
            Label label = new Label() { Text = labelText, Left = 10, Top = top, Width = 120 };
            inputControl.Left = 150;
            inputControl.Top = top;
            inputControl.Width = 200;
            form.Controls.Add(label);
            form.Controls.Add(inputControl);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // You can implement row-click behavior here if needed
        }
    }
}

