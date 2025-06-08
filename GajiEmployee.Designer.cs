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
            this.dataGajiKaryawan.Location = new System.Drawing.Point(508, 50);
            this.dataGajiKaryawan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGajiKaryawan.Name = "dataGajiKaryawan";
            this.dataGajiKaryawan.RowHeadersWidth = 62;
            this.dataGajiKaryawan.RowTemplate.Height = 28;
            this.dataGajiKaryawan.Size = new System.Drawing.Size(664, 389);
            this.dataGajiKaryawan.TabIndex = 0;
            this.dataGajiKaryawan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGajiKaryawan_CellContentClick);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnUpdate.Location = new System.Drawing.Point(289, 291);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(138, 38);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "UPDATE";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnRefresh.Location = new System.Drawing.Point(47, 291);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(138, 38);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "REFRESH";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtNamaKaryawan
            // 
            this.txtNamaKaryawan.Location = new System.Drawing.Point(116, 50);
            this.txtNamaKaryawan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNamaKaryawan.Name = "txtNamaKaryawan";
            this.txtNamaKaryawan.Size = new System.Drawing.Size(333, 22);
            this.txtNamaKaryawan.TabIndex = 5;
            // 
            // txtGajiKaryawan
            // 
            this.txtGajiKaryawan.Location = new System.Drawing.Point(116, 96);
            this.txtGajiKaryawan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtGajiKaryawan.Name = "txtGajiKaryawan";
            this.txtGajiKaryawan.Size = new System.Drawing.Size(333, 22);
            this.txtGajiKaryawan.TabIndex = 6;
            // 
            // lblNamaKaryawan
            // 
            this.lblNamaKaryawan.AutoSize = true;
            this.lblNamaKaryawan.Location = new System.Drawing.Point(1, 54);
            this.lblNamaKaryawan.Name = "lblNamaKaryawan";
            this.lblNamaKaryawan.Size = new System.Drawing.Size(106, 16);
            this.lblNamaKaryawan.TabIndex = 8;
            this.lblNamaKaryawan.Text = "Nama Karyawan";
            // 
            // lblGaji
            // 
            this.lblGaji.AutoSize = true;
            this.lblGaji.Location = new System.Drawing.Point(1, 101);
            this.lblGaji.Name = "lblGaji";
            this.lblGaji.Size = new System.Drawing.Size(93, 16);
            this.lblGaji.TabIndex = 9;
            this.lblGaji.Text = "Gaji Karyawan";
            // 
            // txtJabatan
            // 
            this.txtJabatan.Location = new System.Drawing.Point(116, 141);
            this.txtJabatan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtJabatan.Name = "txtJabatan";
            this.txtJabatan.Size = new System.Drawing.Size(333, 22);
            this.txtJabatan.TabIndex = 10;
            // 
            // lblJabatan
            // 
            this.lblJabatan.AutoSize = true;
            this.lblJabatan.Location = new System.Drawing.Point(1, 146);
            this.lblJabatan.Name = "lblJabatan";
            this.lblJabatan.Size = new System.Drawing.Size(56, 16);
            this.lblJabatan.TabIndex = 11;
            this.lblJabatan.Text = "Jabatan";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(504, 22);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(64, 16);
            this.lblMessage.TabIndex = 12;
            this.lblMessage.Text = "Message";
            // 
            // txtDepartemen
            // 
            this.txtDepartemen.Location = new System.Drawing.Point(116, 188);
            this.txtDepartemen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDepartemen.Name = "txtDepartemen";
            this.txtDepartemen.Size = new System.Drawing.Size(333, 22);
            this.txtDepartemen.TabIndex = 13;
            // 
            // lblDepartemen
            // 
            this.lblDepartemen.AutoSize = true;
            this.lblDepartemen.Location = new System.Drawing.Point(1, 193);
            this.lblDepartemen.Name = "lblDepartemen";
            this.lblDepartemen.Size = new System.Drawing.Size(82, 16);
            this.lblDepartemen.TabIndex = 14;
            this.lblDepartemen.Text = "Departemen";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightCoral;
            this.button1.Location = new System.Drawing.Point(169, 364);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 38);
            this.button1.TabIndex = 15;
            this.button1.Text = "KEMBALI";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GajiEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(191)))), ((int)(((byte)(186)))));
            this.ClientSize = new System.Drawing.Size(1204, 512);
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
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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