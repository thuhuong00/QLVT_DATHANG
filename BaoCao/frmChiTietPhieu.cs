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
    public partial class frmChiTietPhieu : Form
    {
        public frmChiTietPhieu()
        {
            InitializeComponent();
            cmbPhieu.SelectedIndex = 0;
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
            
            String temp = "";
            string loai = "";
            if (cmbPhieu.SelectedIndex == 0)
            {
                temp = "NHẬP";
                loai = "N";
            }
            else 
            {
                temp = "XUẤT";
                loai = "X";
            } 
            Xprt_ChiTietPhieu report = new Xprt_ChiTietPhieu(Program.mgroup, loai, dtNgayBD.DateTime, dtNgayKT.DateTime); ;
            report.lbTuaDe.Text = "CHI TIẾT SỐ LƯỢNG - TRỊ GIÁ HÀNG " + temp;
            report.lbTenNV.Text = Program.frmChinh.tssl_HoTen.Text;
            report.lbNgay.Text = "Thời gian: " + dtNgayBD.Text + " - " + dtNgayKT.Text;
            ReportPrintTool print = new ReportPrintTool(report);
            print.ShowPreviewDialog();
        }
    }
}
