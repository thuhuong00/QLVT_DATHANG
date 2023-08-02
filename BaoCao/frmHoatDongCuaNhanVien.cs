using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLVT_DATHANG.BaoCao
{
    public partial class frmHoatDongCuaNhanVien : Form
    {
        public frmHoatDongCuaNhanVien()
        {
            InitializeComponent();
        }

        private void hOTENNVBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
        }

        private void frmHoatDongCuaNhanVien_Load(object sender, EventArgs e)
        {
            DS.EnforceConstraints = false;
            this.hOTENNVTableAdapter.Connection.ConnectionString = Program.connstr;
            this.hOTENNVTableAdapter.Fill(this.DS.HOTENNV);
            this.hOTENNVTableAdapter.Fill(this.DS.HOTENNV);
            txtMaNV.Text = cmbNhanVien.SelectedValue.ToString();
            txtMaNV.Enabled = false;
            cmbNhanVien.SelectedIndex = 0;

        }

        private void hOTENNVBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.hOTENNVBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                txtMaNV.Text = cmbNhanVien.SelectedValue.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dtNgayBD.Text == "")
            {
                MessageBox.Show("Ngày bắt đầu không được để trống", "", MessageBoxButtons.OK);
                dtNgayBD.Focus();
                return;
            }
            if (dtNgayKT.Text == "")
            {
                MessageBox.Show("Ngày kết thúc không được để trống", "", MessageBoxButtons.OK);
                dtNgayKT.Focus();
                return;
            }
            Xprt_HoatDongCuaNhanVien1 report = new Xprt_HoatDongCuaNhanVien1(int.Parse(txtMaNV.Text), dtNgayBD.DateTime, dtNgayKT.DateTime); ;
            report.lbTenNV.Text = Program.frmChinh.tssl_HoTen.Text;
            report.lbNgayBD.Text = dtNgayBD.Text;
            report.lbNgayKT.Text = dtNgayKT.Text;
            ReportPrintTool print = new ReportPrintTool(report);
            print.ShowPreviewDialog();
        }
    }
}
