namespace pabdproject
{
    partial class FormShiftKaryawan
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
            this.dgvShifts = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtIDKaryawan = new System.Windows.Forms.TextBox();
            this.lblIDKaryawan = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbHariKerja = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpShiftMulai = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.btnTambahShift = new System.Windows.Forms.Button();
            this.btnDeleteShift = new System.Windows.Forms.Button();
            this.btnKembali = new System.Windows.Forms.Button();
            this.dtpShiftSelesai = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShifts)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvShifts
            // 
            this.dgvShifts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShifts.Location = new System.Drawing.Point(-1, 249);
            this.dgvShifts.Name = "dgvShifts";
            this.dgvShifts.RowHeadersWidth = 51;
            this.dgvShifts.RowTemplate.Height = 24;
            this.dgvShifts.Size = new System.Drawing.Size(803, 169);
            this.dgvShifts.TabIndex = 0;
            this.dgvShifts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShifts_CellClick);
            this.dgvShifts.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvShifts_RowHeaderMouseClick);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(13, 148);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(102, 26);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh Data";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtIDKaryawan
            // 
            this.txtIDKaryawan.Location = new System.Drawing.Point(337, 56);
            this.txtIDKaryawan.Name = "txtIDKaryawan";
            this.txtIDKaryawan.Size = new System.Drawing.Size(121, 22);
            this.txtIDKaryawan.TabIndex = 2;
            // 
            // lblIDKaryawan
            // 
            this.lblIDKaryawan.AutoSize = true;
            this.lblIDKaryawan.Location = new System.Drawing.Point(365, 37);
            this.lblIDKaryawan.Name = "lblIDKaryawan";
            this.lblIDKaryawan.Size = new System.Drawing.Size(66, 16);
            this.lblIDKaryawan.TabIndex = 3;
            this.lblIDKaryawan.Text = "Karyawan";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(365, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Hari Kerja";
            // 
            // cmbHariKerja
            // 
            this.cmbHariKerja.FormattingEnabled = true;
            this.cmbHariKerja.Location = new System.Drawing.Point(337, 100);
            this.cmbHariKerja.Name = "cmbHariKerja";
            this.cmbHariKerja.Size = new System.Drawing.Size(121, 24);
            this.cmbHariKerja.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(209, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Shift Mulai";
            // 
            // dtpShiftMulai
            // 
            this.dtpShiftMulai.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpShiftMulai.Location = new System.Drawing.Point(201, 167);
            this.dtpShiftMulai.Name = "dtpShiftMulai";
            this.dtpShiftMulai.ShowUpDown = true;
            this.dtpShiftMulai.Size = new System.Drawing.Size(93, 22);
            this.dtpShiftMulai.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(533, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Shift Selesai";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // btnTambahShift
            // 
            this.btnTambahShift.BackColor = System.Drawing.Color.MediumAquamarine;
            this.btnTambahShift.Location = new System.Drawing.Point(13, 24);
            this.btnTambahShift.Name = "btnTambahShift";
            this.btnTambahShift.Size = new System.Drawing.Size(102, 29);
            this.btnTambahShift.TabIndex = 10;
            this.btnTambahShift.Text = "Tambah Data";
            this.btnTambahShift.UseVisualStyleBackColor = false;
            this.btnTambahShift.Click += new System.EventHandler(this.btnTambahShift_Click);
            // 
            // btnDeleteShift
            // 
            this.btnDeleteShift.BackColor = System.Drawing.Color.LightCoral;
            this.btnDeleteShift.Location = new System.Drawing.Point(13, 81);
            this.btnDeleteShift.Name = "btnDeleteShift";
            this.btnDeleteShift.Size = new System.Drawing.Size(103, 29);
            this.btnDeleteShift.TabIndex = 11;
            this.btnDeleteShift.Text = "Hapus Data";
            this.btnDeleteShift.UseVisualStyleBackColor = false;
            this.btnDeleteShift.Click += new System.EventHandler(this.btnDeleteShift_Click);
            // 
            // btnKembali
            // 
            this.btnKembali.BackColor = System.Drawing.Color.LightCoral;
            this.btnKembali.Location = new System.Drawing.Point(13, 214);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(103, 29);
            this.btnKembali.TabIndex = 12;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = false;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // dtpShiftSelesai
            // 
            this.dtpShiftSelesai.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpShiftSelesai.Location = new System.Drawing.Point(521, 167);
            this.dtpShiftSelesai.Name = "dtpShiftSelesai";
            this.dtpShiftSelesai.ShowUpDown = true;
            this.dtpShiftSelesai.Size = new System.Drawing.Size(93, 22);
            this.dtpShiftSelesai.TabIndex = 13;
            // 
            // FormShiftKaryawan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(191)))), ((int)(((byte)(186)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dtpShiftSelesai);
            this.Controls.Add(this.btnKembali);
            this.Controls.Add(this.btnDeleteShift);
            this.Controls.Add(this.btnTambahShift);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpShiftMulai);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbHariKerja);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblIDKaryawan);
            this.Controls.Add(this.txtIDKaryawan);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvShifts);
            this.Name = "FormShiftKaryawan";
            this.Text = "FormShiftKaryawan";
            this.Load += new System.EventHandler(this.FormShiftKaryawan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShifts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvShifts;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtIDKaryawan;
        private System.Windows.Forms.Label lblIDKaryawan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbHariKerja;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpShiftMulai;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTambahShift;
        private System.Windows.Forms.Button btnDeleteShift;
        private System.Windows.Forms.Button btnKembali;
        private System.Windows.Forms.DateTimePicker dtpShiftSelesai;
    }
}