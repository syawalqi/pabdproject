namespace pabdproject
{
    partial class GrafikGaji
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartGaji = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cmbJenisAnalisis = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bttnkembali = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartGaji)).BeginInit();
            this.SuspendLayout();
            // 
            // chartGaji
            // 
            this.chartGaji.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(176)))), ((int)(((byte)(180)))));
            chartArea1.BackColor = System.Drawing.Color.Gainsboro;
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.LeftRight;
            chartArea1.BackSecondaryColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            this.chartGaji.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartGaji.Legends.Add(legend1);
            this.chartGaji.Location = new System.Drawing.Point(50, 109);
            this.chartGaji.Name = "chartGaji";
            series1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            series1.ChartArea = "ChartArea1";
            series1.Color = System.Drawing.Color.Transparent;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.BrightPastel;
            series1.ShadowColor = System.Drawing.Color.Transparent;
            this.chartGaji.Series.Add(series1);
            this.chartGaji.Size = new System.Drawing.Size(642, 293);
            this.chartGaji.TabIndex = 0;
            this.chartGaji.Text = "chart1";
            // 
            // cmbJenisAnalisis
            // 
            this.cmbJenisAnalisis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJenisAnalisis.FormattingEnabled = true;
            this.cmbJenisAnalisis.Location = new System.Drawing.Point(338, 59);
            this.cmbJenisAnalisis.Name = "cmbJenisAnalisis";
            this.cmbJenisAnalisis.Size = new System.Drawing.Size(217, 24);
            this.cmbJenisAnalisis.TabIndex = 1;
            this.cmbJenisAnalisis.SelectedIndexChanged += new System.EventHandler(this.cmbJenisAnalisis_SelectedIndexChanged_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(212, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(347, 38);
            this.label1.TabIndex = 2;
            this.label1.Text = "Grafik Gaji Karyawan";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(217, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Cari Berdasarkan:";
            // 
            // bttnkembali
            // 
            this.bttnkembali.Location = new System.Drawing.Point(12, 415);
            this.bttnkembali.Name = "bttnkembali";
            this.bttnkembali.Size = new System.Drawing.Size(75, 23);
            this.bttnkembali.TabIndex = 4;
            this.bttnkembali.Text = "Kembali";
            this.bttnkembali.UseVisualStyleBackColor = true;
            this.bttnkembali.Click += new System.EventHandler(this.bttnkembali_Click);
            // 
            // GrafikGaji
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(191)))), ((int)(((byte)(186)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.bttnkembali);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbJenisAnalisis);
            this.Controls.Add(this.chartGaji);
            this.Name = "GrafikGaji";
            this.Text = "GrafikGaji";
            this.Load += new System.EventHandler(this.GrafikGaji_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartGaji)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartGaji;
        private System.Windows.Forms.ComboBox cmbJenisAnalisis;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bttnkembali;
    }
}