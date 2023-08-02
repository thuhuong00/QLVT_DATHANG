using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLVT_DATHANG.NghiepVu
{
    public partial class frmKho : Form
    {
        String macn = "";
        int vitri = 0;
        String button = "";
        public frmKho()
        {
            InitializeComponent();
        }
        private void khoBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsKho.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }
        private void frmKho_Load(object sender, EventArgs e)
        {
            DS.EnforceConstraints = false;
            this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
            this.khoTableAdapter.Fill(this.DS.Kho);
            this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuNhapTableAdapter.Fill(this.DS.PhieuNhap);
            this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuXuatTableAdapter.Fill(this.DS.PhieuXuat);
            this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
            this.datHangTableAdapter.Fill(this.DS.DatHang);

            //CapNhat_MaKhoa();
            macn = Program.maphanmanh;
            cmbChiNhanh.DataSource = Program.bds_dspm;
            cmbChiNhanh.DisplayMember = "TENCN";
            cmbChiNhanh.ValueMember = "TENSERVER";
            cmbChiNhanh.SelectedIndex = Program.mphanmanh;

            if (Program.mgroup == "CONGTY")
            {
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnGhi.Enabled = btnPhucHoi.Enabled = btnChuyenChiNhanh.Enabled = false;
                cmbChiNhanh.Enabled = btnThoat.Enabled = true;
            }
            else
            {
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnGhi.Enabled = btnPhucHoi.Enabled = btnChuyenChiNhanh.Enabled = true;
                cmbChiNhanh.Enabled = btnThoat.Enabled = false;
            }

            gcKho.Enabled = true;
            pncKho.Enabled = false;

        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsKho.Position;
            pncKho.Enabled = true;
            bdsKho.AddNew();
            txtMaCN.Text = macn;
            txtMaKho.Enabled = true;

            btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnThoat.Enabled = btnReload.Enabled = btnChuyenChiNhanh.Enabled = false;
            btnPhucHoi.Enabled = btnGhi.Enabled = true;
            gcKho.Enabled = false;
            button = "Them";
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
                this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
                this.khoTableAdapter.Fill(this.DS.Kho);
                this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuNhapTableAdapter.Fill(this.DS.PhieuNhap);
                this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuXuatTableAdapter.Fill(this.DS.PhieuXuat);
                this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                this.datHangTableAdapter.Fill(this.DS.DatHang);
            }
        }

        private void btnHieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsKho.Position;
            pncKho.Enabled = true;
            txtMaCN.Text = macn;
            txtMaKho.Enabled = false;

            btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnThoat.Enabled = btnReload.Enabled = btnChuyenChiNhanh.Enabled = false;
            btnPhucHoi.Enabled = btnGhi.Enabled = true;
            gcKho.Enabled = false;
            button = "HieuChinh";
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtMaKho.Text.Trim() == "")
            {
                MessageBox.Show("Mã kho không được để trống", "", MessageBoxButtons.OK);
                txtMaKho.Focus();
                return;
            }
            if (txtTenKho.Text.Trim() == "")
            {
                MessageBox.Show("Tên kho không được để trống", "", MessageBoxButtons.OK);
                txtTenKho.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Địa chỉ không được để trống", "", MessageBoxButtons.OK);
                txtDiaChi.Focus();
                return;
            }
            if (txtMaCN.Text.Trim() == "")
            {
                MessageBox.Show("Mã chi nhánh không được để trống", "", MessageBoxButtons.OK);
                txtMaCN.Focus();
                return;
            }
            if (button == "Them")
            {
                String strLenh = "DECLARE @return_value int EXEC @return_value = [dbo].[SP_CHECKMA]  '" + txtMaKho.Text + "', N'MAKHO'" + " SELECT 'Return Value'  =  @return_value";
                SqlDataReader dataReader = Program.ExecSqlDataReader(strLenh);
                dataReader.Read();
                int result_value = (int)dataReader.GetValue(0);
                dataReader.Close();
                               
                if (result_value != 0)
                {
                    MessageBox.Show("Mã kho đã tồn tại, vui lòng nhập mã khác");
                    txtMaKho.Focus();
                    Program.conn.Close();
                    return;
                }
                               
                try
                {
                    bdsKho.EndEdit();
                    bdsKho.ResetCurrentItem();
                    this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.khoTableAdapter.Fill(this.DS.Kho);


                    MessageBox.Show("Thêm kho thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi ghi kho \n" + ex.Message, "", MessageBoxButtons.OK);
                    return;
                }

                btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnThoat.Enabled = btnReload.Enabled = btnChuyenChiNhanh.Enabled = btnThoat.Enabled = true;
                btnPhucHoi.Enabled = btnGhi.Enabled = false;
                gcKho.Enabled = true;
                pncKho.Enabled = false;


            }
            else
            {
                try
                {
                    bdsKho.EndEdit();
                    bdsKho.ResetCurrentItem();
                    this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.khoTableAdapter.Fill(this.DS.Kho);


                    MessageBox.Show("Hiệu chỉnh kho thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hiệu chỉnh nhân viên \n" + ex.Message, "", MessageBoxButtons.OK);
                    return;
                }
                btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnThoat.Enabled = btnReload.Enabled = btnChuyenChiNhanh.Enabled = true;
                btnPhucHoi.Enabled = btnGhi.Enabled = false;
                gcKho.Enabled = true;
                pncKho.Enabled = false;
                txtMaKho.Enabled = true;
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String maKho = "";
            if (bdsDatHang.Count > 0)
            {
                MessageBox.Show("Không thể xóa kho này vì đã có trong Đặt hàng!", "", MessageBoxButtons.OK);
                return;
            }
            if (bdsPhieuNhap.Count > 0)
            {
                MessageBox.Show("Không thể xóa kho này vì đã có trong Phiếu nhập!", "", MessageBoxButtons.OK);
                return;
            }
            if (bdsPhieuXuat.Count > 0)
            {
                MessageBox.Show("Không thể xóa kho này vì đã có trong Phiếu xuất!", "", MessageBoxButtons.OK);
                return;
            }
            if (MessageBox.Show("Bạn có thật sự muốn xóa kho này?", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    maKho = ((DataRowView)bdsKho[bdsKho.Position])["MAKHO"].ToString();
                    bdsKho.RemoveCurrent();
                    this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.khoTableAdapter.Update(this.DS.Kho);
                    MessageBox.Show("Xóa kho thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi Xóa kho \n" + ex.Message, "", MessageBoxButtons.OK);
                    this.khoTableAdapter.Fill(this.DS.Kho);
                    bdsKho.Position = bdsKho.Find("MAKHO", maKho);
                    return;
                }
            }
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = btnReload.Enabled = true;
            cmbChiNhanh.Enabled = btnGhi.Enabled = false;
            pncKho.Enabled = true;
            gcKho.Enabled = false;
            if (bdsKho.Count == 0) btnXoa.Enabled = false;
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.khoTableAdapter.Fill(this.DS.Kho);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Reload kho \n" + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tENKHOLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
