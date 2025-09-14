namespace BaiTapThongTinSoXo
{
    partial class frmSoXo
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
            this.btnTim = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbTinh = new System.Windows.Forms.ComboBox();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpNgay = new System.Windows.Forms.DateTimePicker();
            this.txtSoCanDo = new System.Windows.Forms.TextBox();
            this.rdMienNam = new System.Windows.Forms.RadioButton();
            this.rdMienTrung = new System.Windows.Forms.RadioButton();
            this.rdMienBac = new System.Windows.Forms.RadioButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTim
            // 
            this.btnTim.Location = new System.Drawing.Point(19, 300);
            this.btnTim.Margin = new System.Windows.Forms.Padding(4);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(123, 28);
            this.btnTim.TabIndex = 5;
            this.btnTim.Text = "Tìm";
            this.btnTim.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbTinh);
            this.groupBox1.Controls.Add(this.btnLamMoi);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnTim);
            this.groupBox1.Controls.Add(this.dtpNgay);
            this.groupBox1.Controls.Add(this.txtSoCanDo);
            this.groupBox1.Controls.Add(this.rdMienNam);
            this.groupBox1.Controls.Add(this.rdMienTrung);
            this.groupBox1.Controls.Add(this.rdMienBac);
            this.groupBox1.Location = new System.Drawing.Point(17, 12);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(177, 415);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sổ xố 3 miền";
            // 
            // cmbTinh
            // 
            this.cmbTinh.FormattingEnabled = true;
            this.cmbTinh.Location = new System.Drawing.Point(19, 162);
            this.cmbTinh.Name = "cmbTinh";
            this.cmbTinh.Size = new System.Drawing.Size(123, 24);
            this.cmbTinh.TabIndex = 17;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(19, 379);
            this.btnLamMoi.Margin = new System.Windows.Forms.Padding(4);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(123, 28);
            this.btnLamMoi.TabIndex = 9;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 241);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Dò số";
            // 
            // dtpNgay
            // 
            this.dtpNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgay.Location = new System.Drawing.Point(19, 23);
            this.dtpNgay.Margin = new System.Windows.Forms.Padding(4);
            this.dtpNgay.Name = "dtpNgay";
            this.dtpNgay.Size = new System.Drawing.Size(123, 22);
            this.dtpNgay.TabIndex = 4;
            // 
            // txtSoCanDo
            // 
            this.txtSoCanDo.Location = new System.Drawing.Point(19, 270);
            this.txtSoCanDo.Margin = new System.Windows.Forms.Padding(4);
            this.txtSoCanDo.Name = "txtSoCanDo";
            this.txtSoCanDo.Size = new System.Drawing.Size(123, 22);
            this.txtSoCanDo.TabIndex = 7;
            // 
            // rdMienNam
            // 
            this.rdMienNam.AutoSize = true;
            this.rdMienNam.Location = new System.Drawing.Point(19, 135);
            this.rdMienNam.Margin = new System.Windows.Forms.Padding(4);
            this.rdMienNam.Name = "rdMienNam";
            this.rdMienNam.Size = new System.Drawing.Size(89, 20);
            this.rdMienNam.TabIndex = 2;
            this.rdMienNam.TabStop = true;
            this.rdMienNam.Text = "Miền Nam";
            this.rdMienNam.UseVisualStyleBackColor = true;
            // 
            // rdMienTrung
            // 
            this.rdMienTrung.AutoSize = true;
            this.rdMienTrung.Location = new System.Drawing.Point(19, 107);
            this.rdMienTrung.Margin = new System.Windows.Forms.Padding(4);
            this.rdMienTrung.Name = "rdMienTrung";
            this.rdMienTrung.Size = new System.Drawing.Size(95, 20);
            this.rdMienTrung.TabIndex = 1;
            this.rdMienTrung.TabStop = true;
            this.rdMienTrung.Text = "Miền Trung";
            this.rdMienTrung.UseVisualStyleBackColor = true;
            // 
            // rdMienBac
            // 
            this.rdMienBac.AutoSize = true;
            this.rdMienBac.Location = new System.Drawing.Point(19, 79);
            this.rdMienBac.Margin = new System.Windows.Forms.Padding(4);
            this.rdMienBac.Name = "rdMienBac";
            this.rdMienBac.Size = new System.Drawing.Size(84, 20);
            this.rdMienBac.TabIndex = 0;
            this.rdMienBac.TabStop = true;
            this.rdMienBac.Text = "Miền Bắc";
            this.rdMienBac.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(201, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(955, 415);
            this.dataGridView1.TabIndex = 16;
            // 
            // frmSoXo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1168, 439);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmSoXo";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpNgay;
        private System.Windows.Forms.TextBox txtSoCanDo;
        private System.Windows.Forms.RadioButton rdMienNam;
        private System.Windows.Forms.RadioButton rdMienTrung;
        private System.Windows.Forms.RadioButton rdMienBac;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmbTinh;
    }
}

