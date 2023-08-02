namespace QLVT_DATHANG.NghiepVu
{
    partial class frmChuyenChiNhanh
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
            this.cmbChiNhanhChuyen = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbChiNhanhChuyen
            // 
            this.cmbChiNhanhChuyen.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChiNhanhChuyen.FormattingEnabled = true;
            this.cmbChiNhanhChuyen.Location = new System.Drawing.Point(270, 139);
            this.cmbChiNhanhChuyen.Name = "cmbChiNhanhChuyen";
            this.cmbChiNhanhChuyen.Size = new System.Drawing.Size(405, 30);
            this.cmbChiNhanhChuyen.TabIndex = 0;
            this.cmbChiNhanhChuyen.SelectedIndexChanged += new System.EventHandler(this.cmbChiNhanhChuyen_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(95, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Chọn chi nhánh:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(243, 232);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(239, 38);
            this.button1.TabIndex = 2;
            this.button1.Text = "Chuyển chi nhánh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmChuyenChiNhanh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 358);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbChiNhanhChuyen);
            this.Name = "frmChuyenChiNhanh";
            this.Text = "frmChuyenChiNhanh";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmChuyenChiNhanh_FormClosing);
            this.Load += new System.EventHandler(this.frmChuyenChiNhanh_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbChiNhanhChuyen;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button button1;
    }
}