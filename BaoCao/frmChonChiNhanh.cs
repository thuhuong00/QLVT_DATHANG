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
    public partial class frmChonChiNhanh : Form
    {
        string macn = "";
        public frmChonChiNhanh()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string CHINHANH = "";
           if (cmbChiNhanh.SelectedIndex == 0)
            {
                macn = "CN1";
                CHINHANH = "CHI NHÁNH 1";
            } else 
            {
                macn = "CN2";
                CHINHANH = "CHI NHÁNH 2";
            }
            MessageBox.Show(macn, "", MessageBoxButtons.OK);


            xrpt_DSNhanVien report = new xrpt_DSNhanVien(macn);
            report.lbTuaDe.Text = "DANH SÁCH NHÂN VIÊN " + CHINHANH;

            
            ReportPrintTool print = new ReportPrintTool(report);
            print.ShowPreviewDialog();

        }

        private void frmChonChiNhanh_Load(object sender, EventArgs e)
        {
            DS.EnforceConstraints = false;
            this.nhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
            this.nhanVienTableAdapter.Fill(this.DS.NhanVien);
            cmbChiNhanh.DataSource = Program.bds_dspm;
            cmbChiNhanh.DisplayMember = "TENCN";
            cmbChiNhanh.ValueMember = "TENSERVER";
            cmbChiNhanh.SelectedIndex = Program.mphanmanh;
            if (Program.mgroup == "CONGTY")
            {
                cmbChiNhanh.Enabled = true;
            }
            else
            {
               cmbChiNhanh.Enabled = false;
            }

        }

        private void frmChonChiNhanh_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.frmChinh.Enabled = true;
        }

        private void nhanVienBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsNhanVien.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        private void nhanVienBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChiNhanh.SelectedValue.ToString() == "System.Data.DataRowView")
            {
                return;
            }
            Program.servername = cmbChiNhanh.SelectedValue.ToString();

            if (cmbChiNhanh.SelectedIndex != Program.mphanmanh)
            {
                Program.mlogin = Program.remotelogin;
                Program.password = Program.remotepass;
            }
            else
            {
                Program.mlogin = Program.mloginDN;
                Program.password = Program.passDN;
            }
            if (Program.KetNoi() == 0)
            {
                MessageBox.Show("Lỗi kết nối về chi nhánh mới !", "", MessageBoxButtons.OK);
            }
            else
            {
                this.nhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
                this.nhanVienTableAdapter.Fill(this.DS.NhanVien);
            }
        }
    }
}
