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

namespace QLVT_DATHANG.DanhMuc
{
    public partial class frmPhieuXuat : Form
    {
        String macn = "";
        int vitri = 0;
        String temp = "";
        String button = "";
        public frmPhieuXuat()
        {
            InitializeComponent();
        }

        private void frmPhieuXuat_Load(object sender, EventArgs e)
        {
            DS.EnforceConstraints = false;
            this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuXuatTableAdapter.Fill(this.DS.PhieuXuat);
            this.cTPXTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTPXTableAdapter.Fill(this.DS.CTPX);
            cmbChiNhanh.DataSource = Program.bds_dspm;
            cmbChiNhanh.DisplayMember = "TENCN";
            cmbChiNhanh.ValueMember = "TENSERVER";
            cmbChiNhanh.SelectedIndex = Program.mphanmanh;

            if (Program.mgroup == "CONGTY")
            {
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnGhi.Enabled = btnPhucHoi.Enabled = btnChuyenChiNhanh.Enabled = false;
                cmbChiNhanh.Enabled = btnThoat.Enabled = true;
                gcPhieuXuat.Enabled = gcCTPX.Enabled = true;
            }
            else
            {
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnGhi.Enabled = btnPhucHoi.Enabled = btnChuyenChiNhanh.Enabled = true;
                cmbChiNhanh.Enabled = btnThoat.Enabled = false;
                gcPhieuXuat.Enabled = gcCTPX.Enabled = true;
            }

        }

        private void phieuXuatBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsPhieuXuat.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        private void nGAYLabel_Click(object sender, EventArgs e)
        {

        }

        private void btnDSKho_Click(object sender, EventArgs e)
        {
            Program.formDSKho = new frmDSKho("PhieuXuat");
            Program.formDSKho.Show();
            Program.frmChinh.Enabled = false;
        }

        private void btnDSVatTu_Click(object sender, EventArgs e)
        {
            Program.formDSVatTu = new frmDSVatTu("PhieuXuat");
            Program.formDSVatTu.Show();
            Program.frmChinh.Enabled = false;
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
                this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuXuatTableAdapter.Fill(this.DS.PhieuXuat);
                this.cTPXTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTPXTableAdapter.Fill(this.DS.CTPX);
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            button = "them";
            if (MessageBox.Show("Chọn Yes nếu muốn thêm Phiếu xuât \nChọn No nếu muốn thêm Chi tiết Phiếu Xuất ", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                temp = "PhieuXuat";
                groupControl1.Enabled = true;
                bdsPhieuXuat.AddNew();
                txtMaNV.Text = Program.username;
                txtNgay.EditValue = "";

                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = false;
                btnGhi.Enabled = btnPhucHoi.Enabled = true;
                gcPhieuXuat.Enabled = gcCTPX.Enabled = groupControl2.Enabled = false;
            }
            else
            {
                temp = "CTPX";
                groupControl2.Enabled = true;
                bdsCTPX.AddNew();
                txtMaPX_CTPX.Text = ((DataRowView)bdsPhieuXuat[bdsPhieuXuat.Position])["MAPX"].ToString();
                txtMaPX_CTPX.Enabled = false;
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = false;
                btnGhi.Enabled = btnPhucHoi.Enabled = true;
                gcPhieuXuat.Enabled = gcCTPX.Enabled = groupControl1.Enabled = false;

            }
        }

        private void btnHieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            button = "HieuChinh";
            if (MessageBox.Show("Chọn Yes nếu muốn hiệu chỉnh Phiếu xuất \nChọn No nếu muốn hiệu chỉnh Chi tiết Phiếu xuất ", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                temp = "PhieuXuat";
                vitri = bdsPhieuXuat.Position;
                groupControl1.Enabled = true;

                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = false;
                btnGhi.Enabled = btnPhucHoi.Enabled = true;
                gcPhieuXuat.Enabled = gcCTPX.Enabled = groupControl2.Enabled = false;
            }
            else
            {
                temp = "CTPX";
                groupControl2.Enabled = true;
                txtMaPX_CTPX.Text = ((DataRowView)bdsPhieuXuat[bdsPhieuXuat.Position])["MAPX"].ToString();
                txtMaPX_CTPX.Enabled = false;
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = false;
                btnGhi.Enabled = btnPhucHoi.Enabled = true;
                gcPhieuXuat.Enabled = gcCTPX.Enabled = groupControl1.Enabled = false;
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String tam = "";
            if (MessageBox.Show("Chọn Yes nếu muốn xóa Phiếu xuất \nChọn No nếu muốn xóa Chi tiết Phiếu xuất ", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (bdsCTPX.Count > 0)
                {
                    MessageBox.Show("Không thể xóa phiếu xuất này", "", MessageBoxButtons.OK);
                    return;
                }
                if (MessageBox.Show("Bạn có thật sự muốn xóa phiếu xuất này", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        tam = ((DataRowView)bdsPhieuXuat[bdsPhieuXuat.Position])["MAPX"].ToString();
                        bdsPhieuXuat.RemoveCurrent();
                        this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.phieuXuatTableAdapter.Update(this.DS.PhieuXuat);
                        MessageBox.Show("Xóa Phiếu xuất thành công");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi Xóa Phiếu xuất \n" + ex.Message, "", MessageBoxButtons.OK);
                        this.phieuXuatTableAdapter.Fill(this.DS.PhieuXuat);
                        bdsPhieuXuat.Position = bdsPhieuXuat.Find("MAPX", tam);
                        return;
                    }
                }
            }
            else
            {
                if (MessageBox.Show("Bạn có thật sự muốn xóa chi tiết phiếu xuất này", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        tam = ((DataRowView)bdsCTPX[bdsCTPX.Position])["MAPX"].ToString();
                        bdsCTPX.RemoveCurrent();
                        this.cTPXTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.cTPXTableAdapter.Update(this.DS.CTPX);
                        MessageBox.Show("Xóa CTPX thành công");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi Xóa CTPX \n" + ex.Message, "", MessageBoxButtons.OK);
                        this.cTPXTableAdapter.Fill(this.DS.CTPX);
                        bdsCTPX.Position = bdsCTPX.Find("MAPX", tam);
                        return;
                    }
                }
            }
            groupControl1.Enabled = groupControl2.Enabled = false;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = true;
            btnGhi.Enabled =  false;
            gcPhieuXuat.Enabled = gcCTPX.Enabled = true;
            if (bdsPhieuXuat.Count == 0) btnXoa.Enabled = false;

        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (temp == "PhieuXuat")
            {
                if (txtMaPX.Text.Trim() == "")
                {
                    MessageBox.Show("Mã phiếu xuất không được để trống", "", MessageBoxButtons.OK);
                    txtMaPX.Focus();
                    return;
                }
                if (txtNgay.Text.Trim() == "")
                {
                    MessageBox.Show("Ngày không được để trống", "", MessageBoxButtons.OK);
                    txtNgay.Focus();
                    return;
                }
                if (txtHoTenKH.Text.Trim() == "")
                {
                    MessageBox.Show("Họ tên khách hàng không được để trống", "", MessageBoxButtons.OK);
                    txtHoTenKH.Focus();
                    return;
                }
                if (txtMaKho.Text.Trim() == "")
                {
                    MessageBox.Show("Mã kho không được để trống", "", MessageBoxButtons.OK);
                    txtMaKho.Focus();
                    return;
                }
                if (button == "Them")
                {

                    String strLenh = "DECLARE @return_value int EXEC @return_value = [dbo].[SP_CHECKMA]  '" + txtMaPX.Text + "', N'MAPX'" + " SELECT 'Return Value'  =  @return_value";
                    SqlDataReader dataReader = Program.ExecSqlDataReader(strLenh);
                    // Đọc và lấy result
                    dataReader.Read();
                    int result_value = (int)dataReader.GetValue(0);
                    dataReader.Close();
                    if (result_value == 0)
                    {
                        try
                        {
                            bdsPhieuXuat.EndEdit();
                            bdsPhieuXuat.ResetCurrentItem();
                            this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                            this.phieuXuatTableAdapter.Update(this.DS.PhieuXuat);
                            MessageBox.Show("Thêm phiếu xuất thành công");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi ghi phiếu xuất \n" + ex.Message, "", MessageBoxButtons.OK);
                            return;
                        }
                        btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = true;
                        btnGhi.Enabled = cmbChiNhanh.Enabled = false;
                        gcPhieuXuat.Enabled = gcCTPX.Enabled = true;
                        groupControl1.Enabled = false;

                    }
                    else
                    {
                        if (result_value == 1)
                        {
                            MessageBox.Show("Mã phiếu xuất đã được lập ở chi nhánh hiện tại, vui lòng nhập mã khác");
                            txtMaPX.Focus();
                            return;
                        }
                        else if (result_value == 2)
                        {
                            MessageBox.Show("Mã phiếu xuất đã được lập ở chi nhánh khác, vui lòng nhập mã khác");
                            return;
                        }
                    }
                }
                else
                {
                    try
                    {
                        bdsPhieuXuat.EndEdit();
                        bdsPhieuXuat.ResetCurrentItem();
                        this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.phieuXuatTableAdapter.Update(this.DS.PhieuXuat);

                        MessageBox.Show("Hiệu chỉnh phiếu xuất thành công");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi hiệu chỉnh phiếu xuất \n" + ex.Message, "", MessageBoxButtons.OK);
                        return;
                    }

                    btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = true;
                    btnGhi.Enabled = cmbChiNhanh.Enabled = false;
                    gcPhieuXuat.Enabled = gcCTPX.Enabled = true;
                    groupControl1.Enabled = false;
                    txtMaPX.Enabled = true;
                    txtMaKho.Enabled = true;
                }
            }
            else if (temp == "CTPX")
            {
                if (txtMaPX_CTPX.Text.Trim() == "")
                {
                    MessageBox.Show("Mã phiếu xuất không được để trống", "", MessageBoxButtons.OK);
                    txtMaPX_CTPX.Focus();
                    return;
                }
                if (txtMaVT.Text.Trim() == "")
                {
                    MessageBox.Show("Mã vật tư không được để trống", "", MessageBoxButtons.OK);
                    txtMaVT.Focus();
                    return;
                }
                if (txtSoLuong.Text.Trim() == "")
                {
                    MessageBox.Show("Số lượng không được để trống", "", MessageBoxButtons.OK);
                    txtSoLuong.Focus();
                    return;
                }
                if (txtDonGia.Text.Trim() == "")
                {
                    MessageBox.Show("Đơn giá không được để trống", "", MessageBoxButtons.OK);
                    txtDonGia.Focus();
                    return;
                }
                if (button == "Them")
                {
                    try
                    {
                        // cập nhật lại số lượng tồn trước khi thêm CTPX

                        string strLenh = "DECLARE @return_value int EXEC @return_value = [dbo].[SP_CapNhatSoLuongVatTu] @CHEDO = N'X', @MAVT = N'" + txtMaVT.Text + "', @SOLUONG = " + txtSoLuong.Text + " SELECT 'Return Value' = @return_value";
                        SqlDataReader dataReader = Program.ExecSqlDataReader(strLenh);
                        // Đọc và lấy result
                        dataReader.Read();
                        int result_value = (int)dataReader.GetValue(0);
                        dataReader.Close();
                        bdsCTPX.EndEdit();
                        bdsCTPX.ResetCurrentItem();
                        this.cTPXTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.cTPXTableAdapter.Update(this.DS.CTPX);

                        MessageBox.Show("Thêm Chi tiết phiếu xuất thành công");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi ghi chi tiết phiếu xuất \n" + ex.Message, "", MessageBoxButtons.OK);
                        return;
                    }
                    btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = true;
                    btnGhi.Enabled = cmbChiNhanh.Enabled = false;
                    gcPhieuXuat.Enabled = gcCTPX.Enabled = true;
                    groupControl2.Enabled = false;
                    txtMaPX_CTPX.Enabled = true;
                    txtMaVT.Enabled = true;


                }
                else
                {
                    try
                    {
                        string strLenh = "DECLARE @return_value int EXEC @return_value = [dbo].[SP_CapNhatSoLuongVatTu] @CHEDO = N'X', @MAVT = N'" + txtMaVT.Text + "', @SOLUONG = " + txtSoLuong.Text + " SELECT 'Return Value' = @return_value";
                        SqlDataReader dataReader = Program.ExecSqlDataReader(strLenh);
                        // Đọc và lấy result
                        dataReader.Read();
                        int result_value = (int)dataReader.GetValue(0);
                        dataReader.Close();
                        bdsCTPX.EndEdit();
                        bdsCTPX.ResetCurrentItem();
                        this.cTPXTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.cTPXTableAdapter.Update(this.DS.CTPX);

                        MessageBox.Show("Hiệu chỉnh Chi tiết phiếu xuất thành công");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi hiệu chỉnh Chi tiết phiếu xuất \n" + ex.Message, "", MessageBoxButtons.OK);
                        return;
                    }
                    btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = true;
                    btnGhi.Enabled = cmbChiNhanh.Enabled = false;
                    gcPhieuXuat.Enabled = gcCTPX.Enabled = true;
                    groupControl2.Enabled = false;
                    txtMaPX_CTPX.Enabled = true;
                    txtMaVT.Enabled = true;

                }
            }
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.phieuXuatTableAdapter.Fill(this.DS.PhieuXuat);
                this.cTPXTableAdapter.Fill(this.DS.CTPX);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Reload phiếu xuất và CT phiếu xuất \n" + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}
