using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLVT_DATHANG.DanhMuc
{
    public partial class frmDSVatTu : Form
    {
        String vitri = "";
        public frmDSVatTu(String vitri)
        {
            InitializeComponent();
            this.vitri = vitri;
        }

        private void vattuBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsVatTu.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        private void frmDSVatTu_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dS.Vattu' table. You can move, or remove it, as needed.
            DS.EnforceConstraints = false;
            this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
            this.vattuTableAdapter.Fill(this.DS.Vattu);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String maVT = ((DataRowView)bdsVatTu.Current)["MAVT"].ToString();
            if (vitri == "DatHang") Program.formDatHang.txtMaVT.Text = maVT;
            else if (vitri == "CTPN") Program.formPhieuNhap.txtMaVT.Text = maVT;
            else if (vitri == "PhieuXuat") Program.formPhieuXuat.txtMaVT.Text = maVT;

            this.Close();
        }

        private void frmDSVatTu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.frmChinh.Enabled = true;
        }
    }
}
