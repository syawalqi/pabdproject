namespace pabdproject
{
    partial class FormCutiPengajuan
    { 
        private System.ComponentModel.IContainer components = null;
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnKembali = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnUpdateStatusCuti = new System.Windows.Forms.Button();
            this.btnSubmitCuti = new System.Windows.Forms.Button();
            this.lblTanggalMulai = new System.Windows.Forms.Label();
            this.lblTanggalSelesai = new System.Windows.Forms.Label();
            this.lblJenisCuti = new System.Windows.Forms.Label();
            this.lblKeterangan_Cuti = new System.Windows.Forms.Label();
            this.labelSelectedCutiID = new System.Windows.Forms.Label();
            this.lblStatus_Cuti = new System.Windows.Forms.Label();
            this.dtpTanggalMulai = new System.Windows.Forms.DateTimePicker();
            this.dtpTanggalSelesai = new System.Windows.Forms.DateTimePicker();
            this.cmbJenisCuti = new System.Windows.Forms.ComboBox();
            this.cmbStatus_Cuti = new System.Windows.Forms.ComboBox();
            this.txtKeterangan_Cuti = new System.Windows.Forms.TextBox();
            this.txtPILIH = new System.Windows.Forms.TextBox();
            this.dgvCuti = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuti)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.btnKembali);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnUpdateStatusCuti);
            this.panel1.Controls.Add(this.btnSubmitCuti);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(188, 450);
            this.panel1.TabIndex = 0;
            // 
            // btnKembali
            // 
            this.btnKembali.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(176)))), ((int)(((byte)(180)))));
            this.btnKembali.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnKembali.Location = new System.Drawing.Point(0, 380);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(188, 35);
            this.btnKembali.TabIndex = 3;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = false;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRefresh.Location = new System.Drawing.Point(0, 415);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(188, 35);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh Data";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnUpdateStatusCuti
            // 
            this.btnUpdateStatusCuti.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnUpdateStatusCuti.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUpdateStatusCuti.Location = new System.Drawing.Point(0, 35);
            this.btnUpdateStatusCuti.Name = "btnUpdateStatusCuti";
            this.btnUpdateStatusCuti.Size = new System.Drawing.Size(188, 35);
            this.btnUpdateStatusCuti.TabIndex = 1;
            this.btnUpdateStatusCuti.Text = "Update Status Cuti";
            this.btnUpdateStatusCuti.UseVisualStyleBackColor = false;
            this.btnUpdateStatusCuti.Click += new System.EventHandler(this.btnUpdateStatusCuti_Click);
            // 
            // btnSubmitCuti
            // 
            this.btnSubmitCuti.AutoSize = true;
            this.btnSubmitCuti.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnSubmitCuti.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSubmitCuti.Location = new System.Drawing.Point(0, 0);
            this.btnSubmitCuti.Name = "btnSubmitCuti";
            this.btnSubmitCuti.Size = new System.Drawing.Size(188, 35);
            this.btnSubmitCuti.TabIndex = 0;
            this.btnSubmitCuti.Text = "Ajukan Cuti Baru";
            this.btnSubmitCuti.UseVisualStyleBackColor = false;
            this.btnSubmitCuti.Click += new System.EventHandler(this.btnSubmitCuti_Click);
            // 
            // lblTanggalMulai
            // 
            this.lblTanggalMulai.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTanggalMulai.AutoSize = true;
            this.lblTanggalMulai.Location = new System.Drawing.Point(260, 32);
            this.lblTanggalMulai.Name = "lblTanggalMulai";
            this.lblTanggalMulai.Size = new System.Drawing.Size(118, 16);
            this.lblTanggalMulai.TabIndex = 1;
            this.lblTanggalMulai.Text = "Tanggal Mulai Cuti";
            // 
            // lblTanggalSelesai
            // 
            this.lblTanggalSelesai.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTanggalSelesai.AutoSize = true;
            this.lblTanggalSelesai.Location = new System.Drawing.Point(260, 102);
            this.lblTanggalSelesai.Name = "lblTanggalSelesai";
            this.lblTanggalSelesai.Size = new System.Drawing.Size(132, 16);
            this.lblTanggalSelesai.TabIndex = 2;
            this.lblTanggalSelesai.Text = "Tanggal Selesai Cuti";
            // 
            // lblJenisCuti
            // 
            this.lblJenisCuti.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblJenisCuti.AutoSize = true;
            this.lblJenisCuti.Location = new System.Drawing.Point(260, 170);
            this.lblJenisCuti.Name = "lblJenisCuti";
            this.lblJenisCuti.Size = new System.Drawing.Size(64, 16);
            this.lblJenisCuti.TabIndex = 3;
            this.lblJenisCuti.Text = "Jenis Cuti";
            // 
            // lblKeterangan_Cuti
            // 
            this.lblKeterangan_Cuti.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblKeterangan_Cuti.AutoSize = true;
            this.lblKeterangan_Cuti.Location = new System.Drawing.Point(260, 236);
            this.lblKeterangan_Cuti.Name = "lblKeterangan_Cuti";
            this.lblKeterangan_Cuti.Size = new System.Drawing.Size(101, 16);
            this.lblKeterangan_Cuti.TabIndex = 4;
            this.lblKeterangan_Cuti.Text = "Keterangan Cuti";
            // 
            // labelSelectedCutiID
            // 
            this.labelSelectedCutiID.AutoSize = true;
            this.labelSelectedCutiID.Location = new System.Drawing.Point(436, 78);
            this.labelSelectedCutiID.Name = "labelSelectedCutiID";
            this.labelSelectedCutiID.Size = new System.Drawing.Size(48, 16);
            this.labelSelectedCutiID.TabIndex = 5;
            this.labelSelectedCutiID.Text = "ID Cuti ";
            // 
            // lblStatus_Cuti
            // 
            this.lblStatus_Cuti.AutoSize = true;
            this.lblStatus_Cuti.Location = new System.Drawing.Point(402, 141);
            this.lblStatus_Cuti.Name = "lblStatus_Cuti";
            this.lblStatus_Cuti.Size = new System.Drawing.Size(118, 16);
            this.lblStatus_Cuti.TabIndex = 6;
            this.lblStatus_Cuti.Text = "Status Persetujuan";
            // 
            // dtpTanggalMulai
            // 
            this.dtpTanggalMulai.Location = new System.Drawing.Point(263, 56);
            this.dtpTanggalMulai.Name = "dtpTanggalMulai";
            this.dtpTanggalMulai.Size = new System.Drawing.Size(176, 22);
            this.dtpTanggalMulai.TabIndex = 7;
            // 
            // dtpTanggalSelesai
            // 
            this.dtpTanggalSelesai.Location = new System.Drawing.Point(263, 125);
            this.dtpTanggalSelesai.Name = "dtpTanggalSelesai";
            this.dtpTanggalSelesai.Size = new System.Drawing.Size(176, 22);
            this.dtpTanggalSelesai.TabIndex = 8;
            // 
            // cmbJenisCuti
            // 
            this.cmbJenisCuti.FormattingEnabled = true;
            this.cmbJenisCuti.Location = new System.Drawing.Point(263, 193);
            this.cmbJenisCuti.Name = "cmbJenisCuti";
            this.cmbJenisCuti.Size = new System.Drawing.Size(176, 24);
            this.cmbJenisCuti.TabIndex = 9;
            // 
            // cmbStatus_Cuti
            // 
            this.cmbStatus_Cuti.FormattingEnabled = true;
            this.cmbStatus_Cuti.Location = new System.Drawing.Point(396, 165);
            this.cmbStatus_Cuti.Name = "cmbStatus_Cuti";
            this.cmbStatus_Cuti.Size = new System.Drawing.Size(132, 24);
            this.cmbStatus_Cuti.TabIndex = 10;
            // 
            // txtKeterangan_Cuti
            // 
            this.txtKeterangan_Cuti.Location = new System.Drawing.Point(263, 258);
            this.txtKeterangan_Cuti.Multiline = true;
            this.txtKeterangan_Cuti.Name = "txtKeterangan_Cuti";
            this.txtKeterangan_Cuti.Size = new System.Drawing.Size(176, 65);
            this.txtKeterangan_Cuti.TabIndex = 11;
            // 
            // txtPILIH
            // 
            this.txtPILIH.Location = new System.Drawing.Point(396, 97);
            this.txtPILIH.Name = "txtPILIH";
            this.txtPILIH.Size = new System.Drawing.Size(132, 22);
            this.txtPILIH.TabIndex = 12;
            this.txtPILIH.TextChanged += new System.EventHandler(this.txtPILIH_TextChanged);
            // 
            // dgvCuti
            // 
            this.dgvCuti.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCuti.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dgvCuti.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCuti.GridColor = System.Drawing.SystemColors.ButtonShadow;
            this.dgvCuti.Location = new System.Drawing.Point(188, 329);
            this.dgvCuti.Name = "dgvCuti";
            this.dgvCuti.RowHeadersWidth = 51;
            this.dgvCuti.RowTemplate.Height = 24;
            this.dgvCuti.Size = new System.Drawing.Size(616, 121);
            this.dgvCuti.TabIndex = 13;
            this.dgvCuti.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCuti_CellClick);
            this.dgvCuti.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCuti_CellContentClick);
            // 
            // FormCutiPengajuan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(191)))), ((int)(((byte)(186)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvCuti);
            this.Controls.Add(this.txtPILIH);
            this.Controls.Add(this.txtKeterangan_Cuti);
            this.Controls.Add(this.cmbStatus_Cuti);
            this.Controls.Add(this.cmbJenisCuti);
            this.Controls.Add(this.dtpTanggalSelesai);
            this.Controls.Add(this.dtpTanggalMulai);
            this.Controls.Add(this.lblStatus_Cuti);
            this.Controls.Add(this.labelSelectedCutiID);
            this.Controls.Add(this.lblKeterangan_Cuti);
            this.Controls.Add(this.lblJenisCuti);
            this.Controls.Add(this.lblTanggalSelesai);
            this.Controls.Add(this.lblTanggalMulai);
            this.Controls.Add(this.panel1);
            this.Name = "FormCutiPengajuan";
            this.Text = "Cuti";
            this.Load += new System.EventHandler(this.FormCutiPengajuan_Load_1);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuti)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnKembali;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnUpdateStatusCuti;
        private System.Windows.Forms.Button btnSubmitCuti;
        private System.Windows.Forms.Label lblTanggalMulai;
        private System.Windows.Forms.Label lblTanggalSelesai;
        private System.Windows.Forms.Label lblJenisCuti;
        private System.Windows.Forms.Label lblKeterangan_Cuti;
        private System.Windows.Forms.Label labelSelectedCutiID;
        private System.Windows.Forms.Label lblStatus_Cuti;
        private System.Windows.Forms.DateTimePicker dtpTanggalMulai;
        private System.Windows.Forms.DateTimePicker dtpTanggalSelesai;
        private System.Windows.Forms.ComboBox cmbJenisCuti;
        private System.Windows.Forms.ComboBox cmbStatus_Cuti;
        private System.Windows.Forms.TextBox txtKeterangan_Cuti;
        private System.Windows.Forms.TextBox txtPILIH;
        private System.Windows.Forms.DataGridView dgvCuti;
    }
}