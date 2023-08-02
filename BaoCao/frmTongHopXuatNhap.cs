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
    public partial class frmTongHopXuatNhap : Form
    {
        public frmTongHopXuatNhap()
        {
            InitializeComponent();
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
            Xprt_TongHopXuatNhap report = new Xprt_TongHopXuatNhap(dtNgayBD.DateTime, dtNgayKT.DateTime); ;
            report.lbNgay.Text = dtNgayBD.Text + " đến ngày "+ dtNgayKT.Text;
            ReportPrintTool print = new ReportPrintTool(report);
            print.ShowPreviewDialog();
        }
    }
}
