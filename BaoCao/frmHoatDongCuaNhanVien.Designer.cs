namespace QLVT_DATHANG.BaoCao
{
    partial class frmHoatDongCuaNhanVien
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label mANVLabel;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            this.label1 = new System.Windows.Forms.Label();
            this.DS = new QLVT_DATHANG.DS();
            this.cmbNhanVien = new System.Windows.Forms.ComboBox();
            this.hOTENNVBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.hOTENNVTableAdapter = new QLVT_DATHANG.DSTableAdapters.HOTENNVTableAdapter();
            this.tableAdapterManager = new QLVT_DATHANG.DSTableAdapters.TableAdapterManager();
            this.dtNgayBD = new DevExpress.XtraEditors.DateEdit();
            this.dtNgayKT = new DevExpress.XtraEditors.DateEdit();
            this.button1 = new System.Windows.Forms.Button();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            mANVLabel = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hOTENNVBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayBD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayBD.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayKT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayKT.Properties.CalendarTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // mANVLabel
            // 
            mANVLabel.AutoSize = true;
            mANVLabel.Location = new System.Drawing.Point(394, 137);
            mANVLabel.Name = "mANVLabel";
            mANVLabel.Size = new System.Drawing.Size(49, 16);
            mANVLabel.TabIndex = 3;
            mANVLabel.Text = "MANV:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(76, 137);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(70, 16);
            label2.TabIndex = 5;
            label2.Text = "Nhân viên:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(55, 210);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(91, 16);
            label3.TabIndex = 9;
            label3.Text = "Ngày bắt đầu:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(394, 206);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(91, 16);
            label4.TabIndex = 10;
            label4.Text = "Ngày kết thúc:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(200, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(327, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "HOẠT ĐỘNG CỦA NHÂN VIÊN";
            // 
            // DS
            // 
            this.DS.DataSetName = "DS";
            this.DS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cmbNhanVien
            // 
            this.cmbNhanVien.DataSource = this.hOTENNVBindingSource;
            this.cmbNhanVien.DisplayMember = "HOTEN";
            this.cmbNhanVien.FormattingEnabled = true;
            this.cmbNhanVien.Location = new System.Drawing.Point(166, 133);
            this.cmbNhanVien.Name = "cmbNhanVien";
            this.cmbNhanVien.Size = new System.Drawing.Size(196, 24);
            this.cmbNhanVien.TabIndex = 1;
            this.cmbNhanVien.ValueMember = "MANV";
            this.cmbNhanVien.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // hOTENNVBindingSource
            // 
            this.hOTENNVBindingSource.DataMember = "HOTENNV";
            this.hOTENNVBindingSource.DataSource = this.DS;
            // 
            // hOTENNVTableAdapter
            // 
            this.hOTENNVTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.ChiNhanhTableAdapter = null;
            this.tableAdapterManager.CTDDHTableAdapter = null;
            this.tableAdapterManager.CTPNTableAdapter = null;
            this.tableAdapterManager.CTPXTableAdapter = null;
            this.tableAdapterManager.DatHangTableAdapter = null;
            this.tableAdapterManager.HOTENNVTableAdapter = this.hOTENNVTableAdapter;
            this.tableAdapterManager.KhoTableAdapter = null;
            this.tableAdapterManager.NhanVienTableAdapter = null;
            this.tableAdapterManager.PhieuNhapTableAdapter = null;
            this.tableAdapterManager.PhieuXuatTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = QLVT_DATHANG.DSTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.VattuTableAdapter = null;
            // 
            // dtNgayBD
            // 
            this.dtNgayBD.EditValue = null;
            this.dtNgayBD.Location = new System.Drawing.Point(166, 207);
            this.dtNgayBD.Name = "dtNgayBD";
            this.dtNgayBD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtNgayBD.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtNgayBD.Size = new System.Drawing.Size(196, 22);
            this.dtNgayBD.TabIndex = 6;
            // 
            // dtNgayKT
            // 
            this.dtNgayKT.EditValue = null;
            this.dtNgayKT.Location = new System.Drawing.Point(490, 203);
            this.dtNgayKT.Name = "dtNgayKT";
            this.dtNgayKT.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtNgayKT.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtNgayKT.Size = new System.Drawing.Size(180, 22);
            this.dtNgayKT.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(327, 266);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 29);
            this.button1.TabIndex = 11;
            this.button1.Text = "Chọn";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtMaNV
            // 
            this.txtMaNV.Location = new System.Drawing.Point(490, 134);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Size = new System.Drawing.Size(100, 22);
            this.txtMaNV.TabIndex = 13;
            // 
            // frmHoatDongCuaNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 323);
            this.Controls.Add(this.txtMaNV);
            this.Controls.Add(this.button1);
            this.Controls.Add(label4);
            this.Controls.Add(label3);
            this.Controls.Add(this.dtNgayKT);
            this.Controls.Add(this.dtNgayBD);
            this.Controls.Add(label2);
            this.Controls.Add(mANVLabel);
            this.Controls.Add(this.cmbNhanVien);
            this.Controls.Add(this.label1);
            this.Name = "frmHoatDongCuaNhanVien";
            this.Text = "frmHoatDongCuaNhanVien";
            this.Load += new System.EventHandler(this.frmHoatDongCuaNhanVien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hOTENNVBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayBD.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayBD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayKT.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayKT.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DS DS;
        private System.Windows.Forms.ComboBox cmbNhanVien;
        private System.Windows.Forms.BindingSource hOTENNVBindingSource;
        private DSTableAdapters.HOTENNVTableAdapter hOTENNVTableAdapter;
        private DSTableAdapters.TableAdapterManager tableAdapterManager;
        private DevExpress.XtraEditors.DateEdit dtNgayBD;
        private DevExpress.XtraEditors.DateEdit dtNgayKT;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtMaNV;
    }
}