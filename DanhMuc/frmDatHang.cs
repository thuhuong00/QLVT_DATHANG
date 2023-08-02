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
    public partial class frmDatHang : Form
    {
        String macn = "";
        int vitri = 0;
        String temp = "";
        String button = "";
        public frmDatHang()
        {
            InitializeComponent();
        }

        private void datHangBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsDatHang.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        private void frmDatHang_Load(object sender, EventArgs e)
        {
            DS.EnforceConstraints = false;
            this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
            this.datHangTableAdapter.Fill(this.DS.DatHang);
            this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuNhapTableAdapter.Fill(this.DS.PhieuNhap);
            this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTDDHTableAdapter.Fill(this.DS.CTDDH);
            cmbChiNhanh.DataSource = Program.bds_dspm;
            cmbChiNhanh.DisplayMember = "TENCN";
            cmbChiNhanh.ValueMember = "TENSERVER";
            cmbChiNhanh.SelectedIndex = Program.mphanmanh;

            if (Program.mgroup == "CONGTY")
            {
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnGhi.Enabled = btnPhucHoi.Enabled = btnChuyenChiNhanh.Enabled = false;
                cmbChiNhanh.Enabled = btnThoat.Enabled = true;
                gcDatHang.Enabled = gcCTDDH.Enabled = true;
            }
            else
            {
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnGhi.Enabled = btnPhucHoi.Enabled = btnChuyenChiNhanh.Enabled = true;
                cmbChiNhanh.Enabled = btnThoat.Enabled = false;
                gcDatHang.Enabled = gcCTDDH.Enabled = true;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.formDSKho = new frmDSKho("DatHang");
            Program.formDSKho.Show();
            Program.frmChinh.Enabled = false;
        }

        private void btnDSMaVT_Click(object sender, EventArgs e)
        {
            Program.formDSVatTu = new frmDSVatTu("DatHang");
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
                this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                this.datHangTableAdapter.Fill(this.DS.DatHang);
                this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuNhapTableAdapter.Fill(this.DS.PhieuNhap);
                this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTDDHTableAdapter.Fill(this.DS.CTDDH);
            }
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.datHangTableAdapter.Fill(this.DS.DatHang);
                this.phieuNhapTableAdapter.Fill(this.DS.PhieuNhap);
                this.cTDDHTableAdapter.Fill(this.DS.CTDDH);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Reload đặt hàng và CT đặt hàng \n" + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            button = "Them";
            if (MessageBox.Show("Chọn Yes nếu muốn thêm Đơn đặt hàng \nChọn No nếu muốn thêm Chi tiết Đơn đặt hàng ", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                temp = "DatHang";
                vitri = bdsDatHang.Position;
                groupControl1.Enabled = true;
                bdsDatHang.AddNew();
                txtMaNV.Text = Program.username;
                txtNgay.EditValue = "";

                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = false;
                btnGhi.Enabled = btnPhucHoi.Enabled = true;
                gcDatHang.Enabled = gcCTDDH.Enabled = groupControl2.Enabled = false;
            }
            else
            {
                temp = "CTDDH";
                vitri = bdsCTDDH.Position;
                groupControl2.Enabled = true;
                bdsCTDDH.AddNew();
                txtMaSoDDH_CTDDH.Text = ((DataRowView)bdsDatHang[bdsDatHang.Position])["MASODDH"].ToString();
                txtMaSoDDH_CTDDH.Enabled = false;

                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = false;
                btnGhi.Enabled = btnPhucHoi.Enabled = true;
                gcDatHang.Enabled = gcCTDDH.Enabled = groupControl1.Enabled = false;
            }
        }

        private void btnHieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            button = "HieuChinh";
            if (MessageBox.Show("Chọn Yes nếu muốn hiệu chỉnh Đơn đặt hàng \nChọn No nếu muốn hiệu chỉnh Chi tiết Đơn đặt hàng ", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                temp = "DatHang";
                txtMaSoDDH.Enabled = false;
                vitri = bdsDatHang.Position;
                groupControl1.Enabled = true;

                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = false;
                btnGhi.Enabled = btnPhucHoi.Enabled = true;
                gcDatHang.Enabled = gcCTDDH.Enabled = groupControl2.Enabled = false;
            }
            else
            {
                temp = "CTDDH";
                txtMaSoDDH_CTDDH.Enabled = false;
                txtMaSoDDH_CTDDH.Text = ((DataRowView)bdsDatHang[bdsDatHang.Position])["MASODDH"].ToString();
                //txtMaVT.Enabled = false;
                vitri = bdsCTDDH.Position;
                groupControl2.Enabled = true;

                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = false;
                btnGhi.Enabled = btnPhucHoi.Enabled = true;
                gcDatHang.Enabled = gcCTDDH.Enabled = groupControl1.Enabled = false;
            }
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (temp == "DatHang")
            {
                if (txtMaSoDDH.Text.Trim() == "")
                {
                    MessageBox.Show("Mã số đơn đặt hàng không được để trống", "", MessageBoxButtons.OK);
                    txtMaSoDDH.Focus();
                    return;
                }
                if (txtNgay.Text.Trim() == "")
                {
                    MessageBox.Show("Ngày không được để trống", "", MessageBoxButtons.OK);
                    txtNgay.Focus();
                    return;
                }
                if (txtNhaCC.Text.Trim() == "")
                {
                    MessageBox.Show("Nhà cung cấp không được để trống", "", MessageBoxButtons.OK);
                    txtNhaCC.Focus();
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
                    String strLenh = "DECLARE @return_value int EXEC @return_value = [dbo].[SP_CHECKMA]  " + txtMaSoDDH.Text + ",  MasoDDH "+" SELECT 'Return Value'  =  @return_value";
                    SqlDataReader dataReader = Program.ExecSqlDataReader(strLenh);
                    // Đọc và lấy result
                    dataReader.Read();
                    int result_value = (int)dataReader.GetValue(0);
                    dataReader.Close();
                    if (result_value == 0)
                    {
                        try
                        {
                            bdsDatHang.EndEdit();
                            bdsDatHang.ResetCurrentItem();
                            this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                            this.datHangTableAdapter.Update(this.DS.DatHang);

                            MessageBox.Show("Thêm đơn đặt hàng thành công");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi ghi đơn đặt hàng \n" + ex.Message, "", MessageBoxButtons.OK);
                            return;
                        }
                        btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
                        cmbChiNhanh.Enabled = btnGhi.Enabled = false;
                        gcCTDDH.Enabled = gcDatHang.Enabled = true;
                        groupControl1.Enabled = false;
                    }
                    else 
                    {
                        
                        if (result_value == 1) 
                        {
                            MessageBox.Show("Mã đơn đặt hàng đã tồn tại ở chi nhánh hiện tại, vui lòng nhập mã khác");
                            txtMaSoDDH.Focus();
                            return;
                        }
                        else if (result_value == 2)
                        {
                            MessageBox.Show("Mã đơn đặt hàng đã tồn tại ở chi nhánh khác, vui lòng nhập mã khác");
                            txtMaSoDDH.Focus();
                            return;
                        }
                        
                    }
                    
                }
                else
                {
                    try
                    {
                        bdsDatHang.EndEdit();
                        bdsDatHang.ResetCurrentItem();
                        this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.datHangTableAdapter.Update(this.DS.DatHang);

                        MessageBox.Show("Hiệu chỉnh đơn đặt hàng thành công");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi hiệu chỉnh Đơn đặt hàng \n" + ex.Message, "", MessageBoxButtons.OK);
                        return;
                    }
                    btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
                    cmbChiNhanh.Enabled = btnGhi.Enabled = false;
                    gcCTDDH.Enabled = gcDatHang.Enabled = true;
                    groupControl1.Enabled = false;
                    txtMaSoDDH.Enabled = true;
                }
            }
            else if (temp == "CTDDH")
            {
                if (txtMaSoDDH_CTDDH.Text.Trim() == "")
                {
                    MessageBox.Show("Mã số đơn đặt hàng không được để trống", "", MessageBoxButtons.OK);
                    txtMaSoDDH_CTDDH.Focus();
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
                        bdsCTDDH.EndEdit();
                        bdsCTDDH.ResetCurrentItem();
                        this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.cTDDHTableAdapter.Update(this.DS.CTDDH);

                        MessageBox.Show("Thêm Chi tiết đơn đặt hàng thành công");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi ghi chi tiết đơn đặt hàng \n" + ex.Message, "", MessageBoxButtons.OK);
                        return;
                    }
                    btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
                    cmbChiNhanh.Enabled = btnGhi.Enabled = false;
                    gcCTDDH.Enabled = gcDatHang.Enabled = true;
                    groupControl2.Enabled = false;
                    txtMaSoDDH_CTDDH.Enabled = true;
                    txtMaVT.Enabled = true;
                }
                else
                {
                    try
                    {
                        bdsCTDDH.EndEdit();
                        bdsCTDDH.ResetCurrentItem();
                        this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.cTDDHTableAdapter.Update(this.DS.CTDDH);

                        MessageBox.Show("Hiệu chỉnh Chi tiết đơn đặt hàng thành công");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi hiệu chỉnh Chi tiết đơn đặt hàng \n" + ex.Message, "", MessageBoxButtons.OK);
                        return;
                    }
                    btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
                    cmbChiNhanh.Enabled = btnGhi.Enabled = false;
                    gcCTDDH.Enabled = gcDatHang.Enabled = true;
                    groupControl2.Enabled = false;
                    txtMaSoDDH_CTDDH.Enabled = true;
                    txtMaVT.Enabled = true;
                }
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String tam = "";
            if (MessageBox.Show("Chọn Yes nếu muốn xóa Đơn đặt hàng \nChọn No nếu muốn xóa Chi tiết Đơn đặt hàng ", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (bdsPhieuNhap.Count + bdsCTDDH.Count > 0)
                {
                    MessageBox.Show("Không thể xóa đơn đặt hàng này", "", MessageBoxButtons.OK);
                    return;
                }
                if (MessageBox.Show("Bạn có thật sự muốn xóa đơn đặt hàng này", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        tam = ((DataRowView)bdsDatHang[bdsDatHang.Position])["MASODDH"].ToString();
                        bdsDatHang.RemoveCurrent();
                        this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.datHangTableAdapter.Update(this.DS.DatHang);
                        MessageBox.Show("Xóa Đơn đặt hàng thành công");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi Xóa Đơn đặt hàng \n" + ex.Message, "", MessageBoxButtons.OK);
                        this.datHangTableAdapter.Fill(this.DS.DatHang);
                        bdsDatHang.Position = bdsDatHang.Find("MASODDH", tam);
                        return;
                    }
                }
            }
            else
            {
                if (bdsPhieuNhap.Count > 0)
                {
                    MessageBox.Show("Không thể xóa chi tiết đơn đặt hàng này vì đã lập phiếu nhập", "", MessageBoxButtons.OK);
                    return;
                }
                if (MessageBox.Show("Bạn có thật sự muốn xóa chi tiết đơn đặt hàng này", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        tam = ((DataRowView)bdsCTDDH[bdsCTDDH.Position])["MASODDH"].ToString();
                        bdsCTDDH.RemoveCurrent();
                        this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.cTDDHTableAdapter.Update(this.DS.CTDDH);
                        MessageBox.Show("Xóa CTDDH thành công");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi Xóa CTDDH \n" + ex.Message, "", MessageBoxButtons.OK);
                        this.cTDDHTableAdapter.Fill(this.DS.CTDDH);
                        bdsCTDDH.Position = bdsCTDDH.Find("MASODDH", tam);
                        return;
                    }
                }
            }

            groupControl2.Enabled = groupControl1.Enabled = false;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = btnPhucHoi.Enabled = true;
            btnGhi.Enabled = false;
            gcDatHang.Enabled = gcCTDDH.Enabled = true;
            if (bdsDatHang.Count == 0) btnXoa.Enabled = false;
        }
    }
}
