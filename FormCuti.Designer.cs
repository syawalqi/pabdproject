using System;

namespace pabdproject
{
    partial class FormCutiPengajuan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSubmitCuti = new System.Windows.Forms.Button();
            this.txtAlasanCuti = new System.Windows.Forms.TextBox();
            this.lblAlasanCuti = new System.Windows.Forms.Label();
            this.cmbJenisCuti = new System.Windows.Forms.ComboBox();
            this.lblJenisCuti = new System.Windows.Forms.Label();
            this.dtpTanggalSelesai = new System.Windows.Forms.DateTimePicker();
            this.dtpTanggalMulai = new System.Windows.Forms.DateTimePicker();
            this.lblTanggalSelesai = new System.Windows.Forms.Label();
            this.lblTanggalMulai = new System.Windows.Forms.Label();
            this.btnUpdateStatusCuti = new System.Windows.Forms.Button();
            this.cmbStatusApproval = new System.Windows.Forms.ComboBox();
            this.lblStatusApproval = new System.Windows.Forms.Label();
            this.txtPILIH = new System.Windows.Forms.TextBox();
            this.labelSelectedCutiID = new System.Windows.Forms.Label();
            this.dgvCuti = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnBatalCuti = new System.Windows.Forms.Button();
            this.btnKembali = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuti)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSubmitCuti
            // 
            this.btnSubmitCuti.Location = new System.Drawing.Point(20, 20);
            this.btnSubmitCuti.Name = "btnSubmitCuti";
            this.btnSubmitCuti.Size = new System.Drawing.Size(150, 30);
            this.btnSubmitCuti.TabIndex = 6;
            this.btnSubmitCuti.Text = "Ajukan Cuti Baru";
            this.btnSubmitCuti.UseVisualStyleBackColor = true;
            this.btnSubmitCuti.Click += new System.EventHandler(this.btnSubmitCuti_Click);
            // 
            // txtAlasanCuti
            // 
            this.txtAlasanCuti.Location = new System.Drawing.Point(220, 220);
            this.txtAlasanCuti.Multiline = true;
            this.txtAlasanCuti.Name = "txtAlasanCuti";
            this.txtAlasanCuti.Size = new System.Drawing.Size(300, 80);
            this.txtAlasanCuti.TabIndex = 7;
            this.txtAlasanCuti.TextChanged += new System.EventHandler(this.txtAlasanCuti_TextChanged);
            // 
            // lblAlasanCuti
            // 
            this.lblAlasanCuti.AutoSize = true;
            this.lblAlasanCuti.Location = new System.Drawing.Point(217, 201);
            this.lblAlasanCuti.Name = "lblAlasanCuti";
            this.lblAlasanCuti.Size = new System.Drawing.Size(101, 16);
            this.lblAlasanCuti.TabIndex = 6;
            this.lblAlasanCuti.Text = "Keterangan Cuti";
            // 
            // cmbJenisCuti
            // 
            this.cmbJenisCuti.FormattingEnabled = true;
            this.cmbJenisCuti.Location = new System.Drawing.Point(220, 160);
            this.cmbJenisCuti.Name = "cmbJenisCuti";
            this.cmbJenisCuti.Size = new System.Drawing.Size(180, 24);
            this.cmbJenisCuti.TabIndex = 5;
            // 
            // lblJenisCuti
            // 
            this.lblJenisCuti.AutoSize = true;
            this.lblJenisCuti.Location = new System.Drawing.Point(217, 141);
            this.lblJenisCuti.Name = "lblJenisCuti";
            this.lblJenisCuti.Size = new System.Drawing.Size(64, 16);
            this.lblJenisCuti.TabIndex = 4;
            this.lblJenisCuti.Text = "Jenis Cuti";
            // 
            // dtpTanggalSelesai
            // 
            this.dtpTanggalSelesai.Location = new System.Drawing.Point(220, 100);
            this.dtpTanggalSelesai.Name = "dtpTanggalSelesai";
            this.dtpTanggalSelesai.Size = new System.Drawing.Size(180, 22);
            this.dtpTanggalSelesai.TabIndex = 3;
            // 
            // dtpTanggalMulai
            // 
            this.dtpTanggalMulai.Location = new System.Drawing.Point(220, 40);
            this.dtpTanggalMulai.Name = "dtpTanggalMulai";
            this.dtpTanggalMulai.Size = new System.Drawing.Size(180, 22);
            this.dtpTanggalMulai.TabIndex = 2;
            // 
            // lblTanggalSelesai
            // 
            this.lblTanggalSelesai.AutoSize = true;
            this.lblTanggalSelesai.Location = new System.Drawing.Point(217, 81);
            this.lblTanggalSelesai.Name = "lblTanggalSelesai";
            this.lblTanggalSelesai.Size = new System.Drawing.Size(132, 16);
            this.lblTanggalSelesai.TabIndex = 1;
            this.lblTanggalSelesai.Text = "Tanggal Selesai Cuti";
            // 
            // lblTanggalMulai
            // 
            this.lblTanggalMulai.AutoSize = true;
            this.lblTanggalMulai.Location = new System.Drawing.Point(217, 21);
            this.lblTanggalMulai.Name = "lblTanggalMulai";
            this.lblTanggalMulai.Size = new System.Drawing.Size(118, 16);
            this.lblTanggalMulai.TabIndex = 0;
            this.lblTanggalMulai.Text = "Tanggal Mulai Cuti";
            // 
            // btnUpdateStatusCuti
            // 
            this.btnUpdateStatusCuti.Location = new System.Drawing.Point(20, 100);
            this.btnUpdateStatusCuti.Name = "btnUpdateStatusCuti";
            this.btnUpdateStatusCuti.Size = new System.Drawing.Size(150, 30);
            this.btnUpdateStatusCuti.TabIndex = 10;
            this.btnUpdateStatusCuti.Text = "Update Status Cuti";
            this.btnUpdateStatusCuti.UseVisualStyleBackColor = true;
            this.btnUpdateStatusCuti.Click += new System.EventHandler(this.btnUpdateStatusCuti_Click);
            // 
            // cmbStatusApproval
            // 
            this.cmbStatusApproval.FormattingEnabled = true;
            this.cmbStatusApproval.Location = new System.Drawing.Point(550, 160);
            this.cmbStatusApproval.Name = "cmbStatusApproval";
            this.cmbStatusApproval.Size = new System.Drawing.Size(180, 24);
            this.cmbStatusApproval.TabIndex = 8;
            // 
            // lblStatusApproval
            // 
            this.lblStatusApproval.AutoSize = true;
            this.lblStatusApproval.Location = new System.Drawing.Point(547, 141);
            this.lblStatusApproval.Name = "lblStatusApproval";
            this.lblStatusApproval.Size = new System.Drawing.Size(118, 16);
            this.lblStatusApproval.TabIndex = 9;
            this.lblStatusApproval.Text = "Status Persetujuan";
            // 
            // txtPILIH
            // 
            this.txtPILIH.Location = new System.Drawing.Point(550, 40);
            this.txtPILIH.Name = "txtPILIH";
            this.txtPILIH.ReadOnly = true;
            this.txtPILIH.Size = new System.Drawing.Size(180, 22);
            this.txtPILIH.TabIndex = 8;
            // 
            // labelSelectedCutiID
            // 
            this.labelSelectedCutiID.AutoSize = true;
            this.labelSelectedCutiID.Location = new System.Drawing.Point(547, 21);
            this.labelSelectedCutiID.Name = "labelSelectedCutiID";
            this.labelSelectedCutiID.Size = new System.Drawing.Size(85, 16);
            this.labelSelectedCutiID.TabIndex = 8;
            this.labelSelectedCutiID.Text = "ID Cuti Dipilih";
            // 
            // dgvCuti
            // 
            this.dgvCuti.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCuti.Location = new System.Drawing.Point(204, 320);
            this.dgvCuti.Name = "dgvCuti";
            this.dgvCuti.RowHeadersWidth = 51;
            this.dgvCuti.RowTemplate.Height = 24;
            this.dgvCuti.Size = new System.Drawing.Size(680, 170);
            this.dgvCuti.TabIndex = 2;
            this.dgvCuti.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCuti_CellClick);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(20, 180);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(150, 30);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh Data";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnBatalCuti
            // 
            this.btnBatalCuti.Location = new System.Drawing.Point(20, 60);
            this.btnBatalCuti.Name = "btnBatalCuti";
            this.btnBatalCuti.Size = new System.Drawing.Size(150, 30);
            this.btnBatalCuti.TabIndex = 4;
            this.btnBatalCuti.Text = "Batalkan Cuti";
            this.btnBatalCuti.UseVisualStyleBackColor = true;
            // 
            // btnKembali
            // 
            this.btnKembali.Location = new System.Drawing.Point(20, 220);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(150, 30);
            this.btnKembali.TabIndex = 5;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.btnSubmitCuti);
            this.panel1.Controls.Add(this.btnBatalCuti);
            this.panel1.Controls.Add(this.btnUpdateStatusCuti);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnKembali);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(190, 508);
            this.panel1.TabIndex = 6;
            // 
            // FormCutiPengajuan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 508);
            this.Controls.Add(this.labelSelectedCutiID);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtPILIH);
            this.Controls.Add(this.lblTanggalMulai);
            this.Controls.Add(this.lblStatusApproval);
            this.Controls.Add(this.dtpTanggalMulai);
            this.Controls.Add(this.cmbStatusApproval);
            this.Controls.Add(this.dtpTanggalSelesai);
            this.Controls.Add(this.lblTanggalSelesai);
            this.Controls.Add(this.lblJenisCuti);
            this.Controls.Add(this.cmbJenisCuti);
            this.Controls.Add(this.lblAlasanCuti);
            this.Controls.Add(this.txtAlasanCuti);
            this.Controls.Add(this.dgvCuti);
            this.Name = "FormCutiPengajuan";
            this.Text = "Formulir Pengajuan dan Persetujuan Cuti";
            this.Load += new System.EventHandler(this.FormCutiPengajuan_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuti)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void FormCutiPengajuan_Load_1(object sender, EventArgs e)
        {
            // This method is called when the FormCutiPengajuan form is loaded.
            // It's crucial for initializing the form's state and loading necessary data.

            // 1. Load the data into the DataGridView.
            //    This method fetches leave records from the database and populates dgvCuti.
            LoadCutiData();

            // 2. Set up the user interface based on the logged-in user's role (employee, admin, or HRD).
            //    This method dynamically shows/hides or enables/disables controls.
            SetupUIForRole();

            // 3. Clear the input fields to ensure a clean state for new entries or selections.
            ClearFormInput();
        }






        // Ensure the method `txtAlasanCuti_TextChanged` exists in the code-behind file (FormCutiPengajuan.cs).  
        // If it doesn't exist, add the following method to handle the event:

        private void txtAlasanCuti_TextChanged(object sender, EventArgs e)
        {
            // Add logic to handle the TextChanged event for txtAlasanCuti here.
        }

        #endregion
        private System.Windows.Forms.Label lblTanggalSelesai;
        private System.Windows.Forms.Label lblTanggalMulai;
        private System.Windows.Forms.Label lblJenisCuti;
        private System.Windows.Forms.DateTimePicker dtpTanggalSelesai;
        private System.Windows.Forms.DateTimePicker dtpTanggalMulai;
        private System.Windows.Forms.TextBox txtAlasanCuti;
        private System.Windows.Forms.Label lblAlasanCuti;
        private System.Windows.Forms.ComboBox cmbJenisCuti;
        private System.Windows.Forms.Label labelSelectedCutiID;
        private System.Windows.Forms.Button btnUpdateStatusCuti;
        private System.Windows.Forms.ComboBox cmbStatusApproval;
        private System.Windows.Forms.Label lblStatusApproval;
        private System.Windows.Forms.TextBox txtPILIH; // Consider renaming this to txtCutiIDSelected in the designer
        private System.Windows.Forms.DataGridView dgvCuti;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnBatalCuti;
        private System.Windows.Forms.Button btnKembali;
        private System.Windows.Forms.Button btnSubmitCuti;
        private System.Windows.Forms.Panel panel1;
    }
}