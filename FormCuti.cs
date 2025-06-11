using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace pabdproject
{
    public partial class FormCutiPengajuan : Form
    {
        // Connection string should ideally be loaded from a configuration file (e.g., App.config)
        // for better security and maintainability, but for this example, we'll keep it here.
        private string connectionString = "Data Source=BILLAAA\\SA; Initial Catalog=MANDAK;Integrated Security=True";
        private int loggedInKaryawanID;
        private string userRole;

        // Declare UI Controls - these should match your form designer names
        private Label lblPilihCuti;
        private TextBox txtPilihCuti;
        private GroupBox groupBoxDetailCuti;

        public FormCutiPengajuan(string role)
        {
            InitializeComponent(); // This method is auto-generated and initializes components from the designer
            userRole = role;

            loggedInKaryawanID = Form3.LoggedInUserID; // Ensure Form3.LoggedInUserID is correctly populated

            // Initialize UI controls by finding them if InitializeComponent() doesn't directly connect them
            InitializeDynamicControls();

            // Apply styling
            ApplyCutiFormStyling();

            // Set up UI based on role immediately after initialization
            SetupUIForRole();

            // Load data when the form loads, not in the constructor
            this.Load += FormCutiPengajuan_Load;
        }

        /// <summary>
        /// Initializes controls by finding them in the form's control collection.
        /// This is useful if controls are not directly accessible as public members
        /// or if they are created dynamically.
        /// </summary>
        private void InitializeDynamicControls()
        {
            // Find all necessary controls once during initialization
            lblPilihCuti = Controls.Find("lblPilihCuti", true).Length > 0 ? (Label)Controls.Find("lblPilihCuti", true)[0] : null;
            txtPilihCuti = Controls.Find("txtPilihCuti", true).Length > 0 ? (TextBox)Controls.Find("txtPilihCuti", true)[0] : null;
            lblTanggalMulai = Controls.Find("lblTanggalMulai", true).Length > 0 ? (Label)Controls.Find("lblTanggalMulai", true)[0] : null;
            dtpTanggalMulai = Controls.Find("dtpTanggalMulai", true).Length > 0 ? (DateTimePicker)Controls.Find("dtpTanggalMulai", true)[0] : null;
            lblTanggalSelesai = Controls.Find("lblTanggalSelesai", true).Length > 0 ? (Label)Controls.Find("lblTanggalSelesai", true)[0] : null;
            dtpTanggalSelesai = Controls.Find("dtpTanggalSelesai", true).Length > 0 ? (DateTimePicker)Controls.Find("dtpTanggalSelesai", true)[0] : null;
            lblJenisCuti = Controls.Find("lblJenisCuti", true).Length > 0 ? (Label)Controls.Find("lblJenisCuti", true)[0] : null;
            cmbJenisCuti = Controls.Find("cmbJenisCuti", true).Length > 0 ? (ComboBox)Controls.Find("cmbJenisCuti", true)[0] : null;
            lblAlasanCuti = Controls.Find("lblAlasanCuti", true).Length > 0 ? (Label)Controls.Find("lblAlasanCuti", true)[0] : null;
            txtAlasanCuti = Controls.Find("txtAlasanCuti", true).Length > 0 ? (TextBox)Controls.Find("txtAlasanCuti", true)[0] : null;
            lblStatusApproval = Controls.Find("lblStatusApproval", true).Length > 0 ? (Label)Controls.Find("lblStatusApproval", true)[0] : null;
            cmbStatusApproval = Controls.Find("cmbStatusApproval", true).Length > 0 ? (ComboBox)Controls.Find("cmbStatusApproval", true)[0] : null;
            btnSubmitCuti = Controls.Find("btnSubmitCuti", true).Length > 0 ? (Button)Controls.Find("btnSubmitCuti", true)[0] : null;
            btnBatalCuti = Controls.Find("btnBatalCuti", true).Length > 0 ? (Button)Controls.Find("btnBatalCuti", true)[0] : null;
            btnKembali = Controls.Find("btnKembali", true).Length > 0 ? (Button)Controls.Find("btnKembali", true)[0] : null;
            btnUpdateStatusCuti = Controls.Find("btnUpdateStatusCuti", true).Length > 0 ? (Button)Controls.Find("btnUpdateStatusCuti", true)[0] : null;
            btnRefresh = Controls.Find("btnRefresh", true).Length > 0 ? (Button)Controls.Find("btnRefresh", true)[0] : null;
            dgvCuti = Controls.Find("dgvCuti", true).Length > 0 ? (DataGridView)Controls.Find("dgvCuti", true)[0] : null;
            groupBoxDetailCuti = Controls.Find("groupBoxDetailCuti", true).Length > 0 ? (GroupBox)Controls.Find("groupBoxDetailCuti", true)[0] : null;
            labelSelectedCutiID = Controls.Find("labelSelectedCutiID", true).Length > 0 ? (Label)Controls.Find("labelSelectedCutiID", true)[0] : null;
            txtPILIH = Controls.Find("txtIDPengajuanCuti", true).Length > 0 ? (TextBox)Controls.Find("txtIDPengajuanCuti", true)[0] : null;

            // Assign event handlers only if controls exist
            if (btnSubmitCuti != null) btnSubmitCuti.Click += btnSubmitCuti_Click;
            if (btnBatalCuti != null) btnBatalCuti.Click += btnBatalCuti_Click;
            if (btnKembali != null) btnKembali.Click += btnKembali_Click;
            if (btnRefresh != null) btnRefresh.Click += btnRefresh_Click;
            if (btnUpdateStatusCuti != null) btnUpdateStatusCuti.Click += btnUpdateStatusCuti_Click;
            if (dgvCuti != null) dgvCuti.CellClick += dgvCuti_CellClick;
        }

        private void btnUpdateStatusCuti_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void dgvCuti_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FormCutiPengajuan_Load(object sender, EventArgs e)
        {
            // Load initial data into the DataGridView
            LoadCutiData();
        }

        /// <summary>
        /// Configures the UI visibility and behavior based on the user's role.
        /// </summary>
        private void SetupUIForRole()
        {
            if (userRole == "employee")
            {
                // Hide HRD-only controls
                if (labelSelectedCutiID != null) labelSelectedCutiID.Visible = false;
                if (txtPILIH != null) txtPILIH.Visible = false;
                if (lblStatusApproval != null) lblStatusApproval.Visible = false;
                if (cmbStatusApproval != null) cmbStatusApproval.Visible = false;
                if (btnUpdateStatusCuti != null) btnUpdateStatusCuti.Visible = false;
                if (lblPilihCuti != null) lblPilihCuti.Visible = false;
                if (txtPilihCuti != null) txtPilihCuti.Visible = false;

                // Show employee controls
                if (lblTanggalMulai != null) lblTanggalMulai.Visible = true;
                if (dtpTanggalMulai != null) dtpTanggalMulai.Visible = true;
                if (lblTanggalSelesai != null) lblTanggalSelesai.Visible = true;
                if (dtpTanggalSelesai != null) dtpTanggalSelesai.Visible = true;
                if (lblJenisCuti != null) lblJenisCuti.Visible = true;
                if (cmbJenisCuti != null) cmbJenisCuti.Visible = true;
                if (lblAlasanCuti != null) lblAlasanCuti.Visible = true;
                if (txtAlasanCuti != null) txtAlasanCuti.Visible = true;
                if (btnSubmitCuti != null) btnSubmitCuti.Visible = true;
                if (btnBatalCuti != null) btnBatalCuti.Visible = true;
                if (btnKembali != null) btnKembali.Visible = true;
                if (btnRefresh != null) btnRefresh.Visible = true;

                // Setup employee-specific configurations
                SetupEmployeeControls();
            }
            else if (userRole == "admin" || userRole == "hrd")
            {
                // Hide employee-only controls
                if (lblTanggalMulai != null) lblTanggalMulai.Visible = false;
                if (dtpTanggalMulai != null) dtpTanggalMulai.Visible = false;
                if (lblTanggalSelesai != null) lblTanggalSelesai.Visible = false;
                if (dtpTanggalSelesai != null) dtpTanggalSelesai.Visible = false;
                if (lblJenisCuti != null) lblJenisCuti.Visible = false;
                if (cmbJenisCuti != null) cmbJenisCuti.Visible = false;
                if (lblAlasanCuti != null) lblAlasanCuti.Visible = false;
                if (txtAlasanCuti != null) txtAlasanCuti.Visible = false;
                if (btnSubmitCuti != null) btnSubmitCuti.Visible = false;
                if (btnBatalCuti != null) btnBatalCuti.Visible = false;

                // Show HRD/Admin controls
                if (labelSelectedCutiID != null) labelSelectedCutiID.Visible = true;
                if (txtPILIH != null) txtPILIH.Visible = true;
                if (lblStatusApproval != null) lblStatusApproval.Visible = true;
                if (cmbStatusApproval != null) cmbStatusApproval.Visible = true;
                if (btnUpdateStatusCuti != null) btnUpdateStatusCuti.Visible = true;
                if (lblPilihCuti != null) lblPilihCuti.Visible = true;
                if (txtPilihCuti != null) txtPilihCuti.Visible = true;
                if (btnKembali != null) btnKembali.Visible = true;
                if (btnRefresh != null) btnRefresh.Visible = true;

                // Setup HRD/Admin specific configurations
                SetupHRDControls();
            }
        }

        /// <summary>
        /// Configures controls specifically for employee users.
        /// </summary>
        private void SetupEmployeeControls()
        {
            // Setup date pickers for employees
            if (dtpTanggalMulai != null && dtpTanggalSelesai != null)
            {
                // Employee can only apply for leave 5 days from today
                DateTime minDate = DateTime.Today.AddDays(5);
                dtpTanggalMulai.MinDate = minDate;
                dtpTanggalSelesai.MinDate = minDate;
                dtpTanggalMulai.Value = minDate;
                dtpTanggalSelesai.Value = minDate;

                // Add event handler for start date change
                dtpTanggalMulai.ValueChanged += DtpTanggalMulai_ValueChanged;
            }

            // Setup Jenis Cuti ComboBox for employees
            if (cmbJenisCuti != null)
            {
                cmbJenisCuti.Items.Clear();
                cmbJenisCuti.Items.AddRange(new string[] { "Tahunan", "Melahirkan", "Sakit", "Lain-lain" });
                if (cmbJenisCuti.Items.Count > 0)
                {
                    cmbJenisCuti.SelectedIndex = 0;
                }
                cmbJenisCuti.DropDownStyle = ComboBoxStyle.DropDownList; // Prevent manual typing
            }
        }

        /// <summary>
        /// Configures controls specifically for HRD/Admin users.
        /// </summary>
        private void SetupHRDControls()
        {
            // Setup Status Persetujuan ComboBox for HRD
            if (cmbStatusApproval != null)
            {
                cmbStatusApproval.Items.Clear();
                cmbStatusApproval.Items.AddRange(new string[] { "Menunggu", "Disetujui", "Tidak Disetujui" });
                if (cmbStatusApproval.Items.Count > 0)
                {
                    cmbStatusApproval.SelectedIndex = 0; // Default to "Menunggu"
                }
                cmbStatusApproval.DropDownStyle = ComboBoxStyle.DropDownList; // Prevent manual typing
            }

            // Setup Pilih Cuti textbox as read-only (will be populated from DataGridView selection)
            if (txtPilihCuti != null)
            {
                txtPilihCuti.ReadOnly = true;
                txtPilihCuti.Text = "Pilih dari tabel di bawah";
            }

            if (txtPILIH != null)
            {
                txtPILIH.ReadOnly = true;
                txtPILIH.Text = "";
            }
        }

        /// <summary>
        /// Handles the ValueChanged event of dtpTanggalMulai to ensure dtpTanggalSelesai
        /// is not earlier than dtpTanggalMulai.
        /// </summary>
        private void DtpTanggalMulai_ValueChanged(object sender, EventArgs e)
        {
            // Update minimum date for end date when start date changes
            if (dtpTanggalMulai != null && dtpTanggalSelesai != null)
            {
                // Set minimum date for end date to be the same as start date
                dtpTanggalSelesai.MinDate = dtpTanggalMulai.Value.Date;

                // If current end date is before start date, update it
                if (dtpTanggalSelesai.Value.Date < dtpTanggalMulai.Value.Date)
                {
                    dtpTanggalSelesai.Value = dtpTanggalMulai.Value.Date;
                }
            }
        }

        /// <summary>
        /// Applies consistent styling to the form's controls.
        /// </summary>
        private void ApplyCutiFormStyling()
        {
            this.BackColor = Color.LightSteelBlue;

            // GroupBox Styling
            if (groupBoxDetailCuti != null)
            {
                groupBoxDetailCuti.ForeColor = Color.DarkBlue;
                groupBoxDetailCuti.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            }

            // Label Styling - Apply to all labels recursively
            ApplyLabelStyling(this);

            // Input Control Styling
            if (cmbJenisCuti != null) cmbJenisCuti.Font = new Font("Segoe UI", 10);
            if (dtpTanggalMulai != null) dtpTanggalMulai.Font = new Font("Segoe UI", 10);
            if (dtpTanggalSelesai != null) dtpTanggalSelesai.Font = new Font("Segoe UI", 10);
            if (txtAlasanCuti != null) txtAlasanCuti.Font = new Font("Segoe UI", 10);
            if (txtPilihCuti != null) txtPilihCuti.Font = new Font("Segoe UI", 10);
            if (cmbStatusApproval != null) cmbStatusApproval.Font = new Font("Segoe UI", 10);
            if (dgvCuti != null) dgvCuti.Font = new Font("Segoe UI", 9);

            // Button Styling
            SetButtonStyle(btnSubmitCuti, Color.ForestGreen);
            SetButtonStyle(btnBatalCuti, Color.IndianRed);
            SetButtonStyle(btnKembali, Color.Gray);
            SetButtonStyle(btnUpdateStatusCuti, Color.DarkBlue);
            SetButtonStyle(btnRefresh, Color.DarkOrange);
        }

        /// <summary>
        /// Recursively applies label styling to all labels in the form.
        /// </summary>
        private void ApplyLabelStyling(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Label lbl)
                {
                    lbl.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                    lbl.ForeColor = Color.Navy;
                }
                else if (control.HasChildren)
                {
                    ApplyLabelStyling(control); // Recursive call for containers like GroupBox
                }
            }
        }

        /// <summary>
        /// Helper method to apply consistent styling to buttons.
        /// </summary>
        private void SetButtonStyle(Button button, Color backColor)
        {
            if (button != null)
            {
                button.BackColor = backColor;
                button.ForeColor = Color.White;
                button.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
            }
        }

        /// <summary>
        /// Loads leave data into the DataGridView based on the user's role.
        /// </summary>
        private void LoadCutiData()
        {
            if (dgvCuti == null)
            {
                MessageBox.Show("DataGridView control not found. Cannot load data.", "UI Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dtCuti = new DataTable();
            string query;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (userRole == "employee")
                {
                    // Employee only sees their own leave requests
                    query = @"SELECT ID_Cuti, Jenis_Cuti, Tanggal_Mulai, Tanggal_Selesai, Alasan, Status_Persetujuan, Tanggal_Pengajuan, ID_Karyawan
                              FROM Cuti
                              WHERE ID_Karyawan = @IDKaryawan ORDER BY Tanggal_Pengajuan DESC";
                }
                else // admin or hrd
                {
                    // HRD/Admin sees all leave requests with employee names
                    query = @"SELECT c.ID_Cuti, k.Nama AS Nama_Karyawan, c.Jenis_Cuti, c.Tanggal_Mulai, c.Tanggal_Selesai, c.Alasan, c.Status_Persetujuan, c.Tanggal_Pengajuan, c.ID_Karyawan
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

                    // Configure DataGridView based on role
                    if (userRole == "admin" || userRole == "hrd")
                    {
                        dgvCuti.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgvCuti.MultiSelect = false;
                        dgvCuti.ReadOnly = true; // HRD cannot edit directly in grid

                        // Hide ID_Karyawan column for cleaner view
                        if (dgvCuti.Columns.Contains("ID_Karyawan"))
                            dgvCuti.Columns["ID_Karyawan"].Visible = false;
                    }
                    else if (userRole == "employee")
                    {
                        dgvCuti.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgvCuti.MultiSelect = false;
                        dgvCuti.ReadOnly = true; // Employee cannot edit directly in grid

                        // Hide ID_Karyawan column for employee
                        if (dgvCuti.Columns.Contains("ID_Karyawan"))
                            dgvCuti.Columns["ID_Karyawan"].Visible = false;
                    }

                    // Format columns if they exist
                    if (dgvCuti.Columns.Contains("Tanggal_Mulai"))
                        dgvCuti.Columns["Tanggal_Mulai"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    if (dgvCuti.Columns.Contains("Tanggal_Selesai"))
                        dgvCuti.Columns["Tanggal_Selesai"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    if (dgvCuti.Columns.Contains("Tanggal_Pengajuan"))
                        dgvCuti.Columns["Tanggal_Pengajuan"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading leave data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Handles the click event for the Submit Cuti (Submit Leave) button.
        /// </summary>
        private void btnSubmitCuti_Click(object sender, EventArgs e)
        {
            if (userRole != "employee")
            {
                MessageBox.Show("Hanya karyawan yang dapat mengajukan cuti.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (loggedInKaryawanID <= 0)
            {
                MessageBox.Show("ID Karyawan tidak valid. Silakan login kembali.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string reasonText = txtAlasanCuti?.Text.Trim() ?? string.Empty;

            if (cmbJenisCuti == null || cmbJenisCuti.SelectedItem == null || string.IsNullOrWhiteSpace(reasonText))
            {
                MessageBox.Show("Harap lengkapi Jenis Cuti dan Alasan Cuti.", "Input Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpTanggalMulai == null || dtpTanggalSelesai == null)
            {
                MessageBox.Show("Kontrol tanggal tidak ditemukan. Harap laporkan error ini.", "UI Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dtpTanggalMulai.Value.Date > dtpTanggalSelesai.Value.Date)
            {
                MessageBox.Show("Tanggal mulai cuti tidak boleh lebih dari tanggal selesai cuti.", "Date Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ensure leave application is at least 5 days from now
            if (dtpTanggalMulai.Value.Date < DateTime.Today.AddDays(5))
            {
                MessageBox.Show("Pengajuan cuti harus minimal 5 hari dari hari ini.", "Date Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string jenisCuti = cmbJenisCuti.SelectedItem.ToString();
            DateTime tanggalMulai = dtpTanggalMulai.Value.Date;
            DateTime tanggalSelesai = dtpTanggalSelesai.Value.Date;
            string alasanCuti = reasonText;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Cuti (ID_Karyawan, Jenis_Cuti, Tanggal_Mulai, Tanggal_Selesai, Alasan, Status_Persetujuan, Tanggal_Pengajuan) " +
                                   "VALUES (@IDKaryawan, @JenisCuti, @TglMulai, @TglSelesai, @Alasan, @Status, GETDATE())";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDKaryawan", loggedInKaryawanID);
                        cmd.Parameters.AddWithValue("@JenisCuti", jenisCuti);
                        cmd.Parameters.AddWithValue("@TglMulai", tanggalMulai);
                        cmd.Parameters.AddWithValue("@TglSelesai", tanggalSelesai);
                        cmd.Parameters.AddWithValue("@Alasan", alasanCuti);
                        cmd.Parameters.AddWithValue("@Status", "Menunggu");

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Pengajuan cuti berhasil diajukan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFormInput();
                        LoadCutiData(); // Refresh data after submission
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat mengajukan cuti: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFormInput()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles the click event for the Batal Cuti (Cancel Leave) button.
        /// </summary>
        private void btnBatalCuti_Click(object sender, EventArgs e)
        {
            if (userRole != "employee")
            {
                MessageBox.Show("Hanya karyawan yang dapat membatalkan pengajuan cuti.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvCuti == null)
            {
                MessageBox.Show("DataGridView control tidak ditemukan. Tidak dapat membatalkan cuti.", "UI Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // If no row selected, just clear the form inputs
            if (dgvCuti.SelectedRows.Count == 0)
            {
                ClearFormInput();
                MessageBox.Show("Input pengajuan cuti telah dibersihkan.", "Batal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // If a row is selected, try to cancel the leave request
            DataGridViewRow selectedRow = dgvCuti.SelectedRows[0];

            if (!dgvCuti.Columns.Contains("Status_Persetujuan") || selectedRow.Cells["Status_Persetujuan"].Value == null)
            {
                MessageBox.Show("Kolom Status Persetujuan tidak ditemukan atau kosong.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string status = selectedRow.Cells["Status_Persetujuan"].Value.ToString();

            if (!dgvCuti.Columns.Contains("ID_Cuti") || selectedRow.Cells["ID_Cuti"].Value == null || !int.TryParse(selectedRow.Cells["ID_Cuti"].Value.ToString(), out int idCuti))
            {
                MessageBox.Show("ID Cuti tidak ditemukan atau tidak valid.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate that the selected leave belongs to the logged-in employee
            if (!dgvCuti.Columns.Contains("ID_Karyawan") || selectedRow.Cells["ID_Karyawan"].Value == null || !int.TryParse(selectedRow.Cells["ID_Karyawan"].Value.ToString(), out int selectedKaryawanID))
            {
                MessageBox.Show("ID Karyawan tidak ditemukan atau tidak valid.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (selectedKaryawanID != loggedInKaryawanID)
            {
                MessageBox.Show("Anda tidak memiliki izin untuk membatalkan pengajuan cuti karyawan lain.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (status != "Menunggu")
            {
                MessageBox.Show("Hanya pengajuan cuti dengan status 'Menunggu' yang dapat dibatalkan.", "Cannot Cancel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin membatalkan pengajuan cuti ini?", "Konfirmasi Pembatalan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM Cuti WHERE ID_Cuti = @IDCuti AND ID_Karyawan = @IDKaryawan AND Status_Persetujuan = @Status";
                    }
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            string query = "DELETE FROM Cuti WHERE ID_Cuti = @IDCuti AND ID_Karyawan = @IDKaryawan AND Status_Persetujuan = @Status";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@IDCuti", idCuti);
                                cmd.Parameters.AddWithValue("@IDKaryawan", loggedInKaryawanID);
                                cmd.Parameters.AddWithValue("@Status", "Menunggu");
                                cmd.ExecuteNonQuery();
                            }
                        }
                        MessageBox.Show("Pengajuan cuti berhasil dibatalkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCutiData(); // Refresh data after cancellation
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan saat membatalkan pengajuan cuti: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat membatalkan pengajuan cuti: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                }
            }
        }
    }
}
