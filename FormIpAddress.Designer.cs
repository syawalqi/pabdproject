namespace pabdproject
{
    partial class FormIpAddress
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
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.btnSaveIP = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGetLocalIP = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Location = new System.Drawing.Point(51, 86);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(142, 22);
            this.txtIpAddress.TabIndex = 0;
            // 
            // btnSaveIP
            // 
            this.btnSaveIP.Location = new System.Drawing.Point(36, 125);
            this.btnSaveIP.Name = "btnSaveIP";
            this.btnSaveIP.Size = new System.Drawing.Size(75, 23);
            this.btnSaveIP.TabIndex = 1;
            this.btnSaveIP.Text = "Save";
            this.btnSaveIP.UseVisualStyleBackColor = true;
            this.btnSaveIP.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(176)))), ((int)(((byte)(180)))));
            this.panel1.Controls.Add(this.btnGetLocalIP);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtIpAddress);
            this.panel1.Controls.Add(this.btnSaveIP);
            this.panel1.Location = new System.Drawing.Point(269, 121);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(239, 197);
            this.panel1.TabIndex = 2;
            // 
            // btnGetLocalIP
            // 
            this.btnGetLocalIP.Location = new System.Drawing.Point(135, 125);
            this.btnGetLocalIP.Name = "btnGetLocalIP";
            this.btnGetLocalIP.Size = new System.Drawing.Size(75, 23);
            this.btnGetLocalIP.TabIndex = 3;
            this.btnGetLocalIP.Text = "Get";
            this.btnGetLocalIP.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Masukkan Ip Adress";
            // 
            // FormIpAddress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(191)))), ((int)(((byte)(186)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "FormIpAddress";
            this.Text = "FormIpAddress";
            this.Load += new System.EventHandler(this.FormIpAddress_Load_1);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.Button btnSaveIP;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetLocalIP;
    }
}