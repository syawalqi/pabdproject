namespace pabdproject
{
    partial class GajiEmployee
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
            this.dataGajiKaryawan = new System.Windows.Forms.DataGridView();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtNamaKaryawan = new System.Windows.Forms.TextBox();
            this.txtGajiKaryawan = new System.Windows.Forms.TextBox();
            this.lblNamaKaryawan = new System.Windows.Forms.Label();
            this.lblGaji = new System.Windows.Forms.Label();
            this.txtJabatan = new System.Windows.Forms.TextBox();
            this.lblJabatan = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtDepartemen = new System.Windows.Forms.TextBox();
            this.lblDepartemen = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGajiKaryawan)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGajiKaryawan
            // 
            this.dataGajiKaryawan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGajiKaryawan.Location = new System.Drawing.Point(571, 62);
            this.dataGajiKaryawan.Name = "dataGajiKaryawan";
            this.dataGajiKaryawan.RowHeadersWidth = 62;
            this.dataGajiKaryawan.RowTemplate.Height = 28;
            this.dataGajiKaryawan.Size = new System.Drawing.Size(747, 486);
            this.dataGajiKaryawan.TabIndex = 0;
            this.dataGajiKaryawan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGajiKaryawan_CellContentClick);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnUpdate.Location = new System.Drawing.Point(325, 364);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(155, 48);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "UPDATE";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(53, 364);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(155, 48);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "REFRESH";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtNamaKaryawan
            // 
            this.txtNamaKaryawan.Location = new System.Drawing.Point(131, 62);
            this.txtNamaKaryawan.Name = "txtNamaKaryawan";
            this.txtNamaKaryawan.Size = new System.Drawing.Size(374, 26);
            this.txtNamaKaryawan.TabIndex = 5;
            // 
            // txtGajiKaryawan
            // 
            this.txtGajiKaryawan.Location = new System.Drawing.Point(131, 120);
            this.txtGajiKaryawan.Name = "txtGajiKaryawan";
            this.txtGajiKaryawan.Size = new System.Drawing.Size(374, 26);
            this.txtGajiKaryawan.TabIndex = 6;
            // 
            // lblNamaKaryawan
            // 
            this.lblNamaKaryawan.AutoSize = true;
            this.lblNamaKaryawan.Location = new System.Drawing.Point(1, 68);
            this.lblNamaKaryawan.Name = "lblNamaKaryawan";
            this.lblNamaKaryawan.Size = new System.Drawing.Size(124, 20);
            this.lblNamaKaryawan.TabIndex = 8;
            this.lblNamaKaryawan.Text = "Nama Karyawan";
            // 
            // lblGaji
            // 
            this.lblGaji.AutoSize = true;
            this.lblGaji.Location = new System.Drawing.Point(1, 126);
            this.lblGaji.Name = "lblGaji";
            this.lblGaji.Size = new System.Drawing.Size(110, 20);
            this.lblGaji.TabIndex = 9;
            this.lblGaji.Text = "Gaji Karyawan";
            // 
            // txtJabatan
            // 
            this.txtJabatan.Location = new System.Drawing.Point(131, 176);
            this.txtJabatan.Name = "txtJabatan";
            this.txtJabatan.Size = new System.Drawing.Size(374, 26);
            this.txtJabatan.TabIndex = 10;
            // 
            // lblJabatan
            // 
            this.lblJabatan.AutoSize = true;
            this.lblJabatan.Location = new System.Drawing.Point(1, 182);
            this.lblJabatan.Name = "lblJabatan";
            this.lblJabatan.Size = new System.Drawing.Size(67, 20);
            this.lblJabatan.TabIndex = 11;
            this.lblJabatan.Text = "Jabatan";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(567, 27);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(74, 20);
            this.lblMessage.TabIndex = 12;
            this.lblMessage.Text = "Message";
            // 
            // txtDepartemen
            // 
            this.txtDepartemen.Location = new System.Drawing.Point(131, 235);
            this.txtDepartemen.Name = "txtDepartemen";
            this.txtDepartemen.Size = new System.Drawing.Size(374, 26);
            this.txtDepartemen.TabIndex = 13;
            // 
            // lblDepartemen
            // 
            this.lblDepartemen.AutoSize = true;
            this.lblDepartemen.Location = new System.Drawing.Point(1, 241);
            this.lblDepartemen.Name = "lblDepartemen";
            this.lblDepartemen.Size = new System.Drawing.Size(98, 20);
            this.lblDepartemen.TabIndex = 14;
            this.lblDepartemen.Text = "Departemen";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SpringGreen;
            this.button1.Location = new System.Drawing.Point(190, 455);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 48);
            this.button1.TabIndex = 15;
            this.button1.Text = "KEMBALI";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GajiEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1355, 640);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblDepartemen);
            this.Controls.Add(this.txtDepartemen);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblJabatan);
            this.Controls.Add(this.txtJabatan);
            this.Controls.Add(this.lblGaji);
            this.Controls.Add(this.lblNamaKaryawan);
            this.Controls.Add(this.txtGajiKaryawan);
            this.Controls.Add(this.txtNamaKaryawan);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.dataGajiKaryawan);
            this.Name = "GajiEmployee";
            this.Text = "GajiEmployee";
            this.Load += new System.EventHandler(this.GajiEmployee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGajiKaryawan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGajiKaryawan;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtNamaKaryawan;
        private System.Windows.Forms.TextBox txtGajiKaryawan;
        private System.Windows.Forms.Label lblNamaKaryawan;
        private System.Windows.Forms.Label lblGaji;
        private System.Windows.Forms.TextBox txtJabatan;
        private System.Windows.Forms.Label lblJabatan;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtDepartemen;
        private System.Windows.Forms.Label lblDepartemen;
        private System.Windows.Forms.Button button1;
    }
}