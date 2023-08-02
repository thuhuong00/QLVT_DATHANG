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
    public partial class frmPhieuNhap : Form
    {
        String macn = "";
        int vitri = 0;
        String temp = "";
        String button = "";
        public frmPhieuNhap()
        {
            InitializeComponent();
        }

        private void datHangBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsDatHang.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        private void frmPhieuNhap_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DS.CTDDH' table. You can move, or remove it, as needed.
            
            DS.EnforceConstraints = false;
            this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
            this.datHangTableAdapter.Fill(this.DS.DatHang);
            this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuNhapTableAdapter.Fill(this.DS.PhieuNhap);
            this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTPNTableAdapter.Fill(this.DS.CTPN);
            this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTDDHTableAdapter.Fill(this.DS.CTDDH);
            cmbChiNhanh.DataSource = Program.bds_dspm;
            cmbChiNhanh.DisplayMember = "TENCN";
            cmbChiNhanh.ValueMember = "TENSERVER";
            cmbChiNhanh.SelectedIndex = Program.mphanmanh;

            if (Program.mgroup == "CONGTY")
            {
                txtMaNV.Text = Program.username;
                txtMaNV.Enabled = true;
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnGhi.Enabled = btnPhucHoi.Enabled = btnChuyenChiNhanh.Enabled = false;
                cmbChiNhanh.Enabled = btnThoat.Enabled = true;
                gcDatHang.Enabled = gcPhieuNhap.Enabled = gcCTPN.Enabled = true;
            }
            else
            {
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnGhi.Enabled = btnPhucHoi.Enabled = btnChuyenChiNhanh.Enabled = true;
                cmbChiNhanh.Enabled = btnThoat.Enabled = false;
                gcDatHang.Enabled = gcPhieuNhap.Enabled = gcCTPN.Enabled = true;
            }


        }

        private void btnDSKho_Click(object sender, EventArgs e)
        {
            Program.formDSKho = new frmDSKho("PhieuNhap");
            Program.formDSKho.Show();
            Program.frmChinh.Enabled = false;
        }

        private void btnDSVatTu_Click(object sender, EventArgs e)
        {
            Program.formDSVatTu = new frmDSVatTu("CTPN");
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
                this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTPNTableAdapter.Fill(this.DS.CTPN);
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
                this.cTPNTableAdapter.Fill(this.DS.CTPN);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Reload nhân viên \n" + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String t = "";
            String strLenh = "DECLARE @return_value int EXEC @return_value = [dbo].[SP_CHECKMA]  " + ((DataRowView)bdsDatHang[bdsDatHang.Position])["MASODDH"].ToString() + ",  PN_DDH " + " SELECT 'Return Value'  =  @return_value";
            SqlDataReader dataReader = Program.ExecSqlDataReader(strLenh);
            // Đọc và lấy result
            dataReader.Read();
            int result_value = (int)dataReader.GetValue(0);
            Console.WriteLine(dataReader);
            dataReader.Close();
            if (result_value !=0)
            {
                if (result_value == 1)
                {
                    MessageBox.Show("Mã đơn đặt hàng đã được lập phiếu nhập ở chi nhánh hiện tại, vui lòng nhập mã khác");
                    txtMaSoDDH.Focus();
                    return;
                }
                else if (result_value == 2)
                {
                    MessageBox.Show("Mã đơn đặt hàng đã được lập phiếu nhập ở chi nhánh khác, vui lòng nhập mã khác");
                    return;
                }
            }
            else
            {

                bdsPhieuNhap.AddNew();
                //Nhập mã phiếu nhập
                frmNhapMaPN m = new frmNhapMaPN();
                m.ShowDialog();
/*                strLenh = "DECLARE @return_value int EXEC @return_value = [dbo].[SP_CHECKMA]  " + txtMaPN.Text + ",  MAPN " + " SELECT 'Return Value'  =  @return_value";
                dataReader = Program.ExecSqlDataReader(strLenh);
                // Đọc và lấy result
                dataReader.Read();
                result_value = (int)dataReader.GetValue(0);
                dataReader.Close();
                if (result_value != 0)
                {
                    if (result_value == 1)
                    {
                        MessageBox.Show("Không thể thêm vì mã phiếu nhập này đã tồn tại ở chi nhánh hiện tại, vui lòng nhập mã khác");
                    }
                    else if (result_value == 2)
                    {
                        MessageBox.Show("Không thể thêm vì mã phiếu nhập này đã tồn tại ở chi nhánh khác, vui lòng nhập mã khác");
                    }

                }
                else
                {*/
                    t = txtMaPN.Text;
                    txtNgay.Text = ((DataRowView)bdsDatHang[bdsDatHang.Position])["NGAY"].ToString();
                    txtMaSoDDH.Text = ((DataRowView)bdsDatHang[bdsDatHang.Position])["MasoDDH"].ToString();
                    txtMaNV.Text = Program.username;
                    txtMaKho.Text = ((DataRowView)bdsDatHang[bdsDatHang.Position])["MAKHO"].ToString();

                    try
                    {
                        MessageBox.Show(groupControl1.ToString());
                        bdsPhieuNhap.EndEdit();
                        bdsPhieuNhap.ResetCurrentItem();
                        this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.phieuNhapTableAdapter.Update(this.DS.PhieuNhap);
                        MessageBox.Show("Thêm phiếu nhập thành công!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi ghi phiếu nhập \n" + ex.Message, "", MessageBoxButtons.OK);
                        //btnReload_ItemClick(sender, e);
                        return;
                    }


                    //Thêm chi tiết phiếu nhập
                    for (int i = 0; i < (bdsCTDDH.Count); i++)
                    {
                        bdsCTPN.AddNew();
                        txtMaPN_CTPN.Text = t;
                        txtMaVT.Text = ((DataRowView)bdsCTDDH[i])["MAVT"].ToString();
                        txtSoLuong.Text = ((DataRowView)bdsCTDDH[i])["SOLUONG"].ToString();
                        txtDonGia.Text = ((DataRowView)bdsCTDDH[i])["DONGIA"].ToString();
                        try
                        {
                            strLenh = "DECLARE @return_value int EXEC @return_value = [dbo].[SP_CapNhatSoLuongVatTu] @CHEDO = N'N', @MAVT = N'" + txtMaVT.Text + "', @SOLUONG = " +txtSoLuong.Text + " SELECT 'Return Value' = @return_value";
                            dataReader = Program.ExecSqlDataReader(strLenh);
                            // Đọc và lấy result
                            dataReader.Read();
                            result_value = (int)dataReader.GetValue(0);
                            dataReader.Close();
                            bdsCTPN.EndEdit();
                            bdsCTPN.ResetCurrentItem();
                            this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
                            this.cTPNTableAdapter.Update(this.DS.CTPN);
                            MessageBox.Show("Thêm chi tiết phiếu nhập thành công!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi ghi chi tiết phiếu nhập \n" + ex.Message, "", MessageBoxButtons.OK);
                            return;
                        }

                   // }
                }
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = btnPhucHoi.Enabled = btnReload.Enabled = true;
                btnGhi.Enabled = false;
            }
        }

        private void btnHieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            button = "HieuChinh";
            if (MessageBox.Show("Chọn Yes nếu muốn hiệu chỉnh phiếu nhập \nChọn No nếu muốn hiệu chỉnh Chi tiết phiếu nhập ", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                temp = "PhieuNhap";
                txtMaPN.Enabled = false;
                vitri = bdsPhieuNhap.Position;
                groupControl1.Enabled = true;

                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = false;
                btnGhi.Enabled = btnPhucHoi.Enabled = true;
                gcPhieuNhap.Enabled = gcCTPN.Enabled = groupControl2.Enabled = false;
                txtMaKho.Enabled = false;
                txtMaNV.Enabled = false;
                txtMaSoDDH.Enabled = false;
            }
            else
            {
                temp = "CTPN";
                txtMaPN_CTPN.Enabled = false;
                txtMaVT.Enabled = false;
                txtMaPN_CTPN.Text = ((DataRowView)bdsCTPN[bdsCTPN.Position])["MAPN"].ToString();
                vitri = bdsCTPN.Position;
                groupControl2.Enabled = true;

                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = btnReload.Enabled = false;
                btnGhi.Enabled = btnPhucHoi.Enabled = true;
                gcPhieuNhap.Enabled = gcCTPN.Enabled = groupControl1.Enabled = false;
            }
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (temp == "PhieuNhap")
            {
                if (txtNgay.Text.Trim() == "")
                {
                    MessageBox.Show("Ngày không được để trống", "", MessageBoxButtons.OK);
                    txtNgay.Focus();
                    return;
                }
                if (txtMaSoDDH.Text.Trim() == "")
                {
                    MessageBox.Show("Mã số đơn đăt hàng không được để trống", "", MessageBoxButtons.OK);
                    txtMaSoDDH.Focus();
                    return;
                }
                try
                {
                    bdsPhieuNhap.EndEdit();
                    bdsPhieuNhap.ResetCurrentItem();
                    this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.phieuNhapTableAdapter.Update(this.DS.PhieuNhap);

                    MessageBox.Show("Hiệu chỉnh phiếu nhập thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hiệu chỉnh phiếu nhập \n" + ex.Message, "", MessageBoxButtons.OK);
                    return;
                }
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = btnReload.Enabled = true;
                btnGhi.Enabled = cmbChiNhanh.Enabled = false;
                gcPhieuNhap.Enabled = gcCTPN.Enabled = true;
                groupControl1.Enabled = false;
            }
            else if (temp == "CTPN")
            {
                int sLBD = int.Parse(((DataRowView)bdsCTPN[bdsCTPN.Position])["SOLUONG"].ToString());
                if (txtSoLuong.Text.Trim() == "")
                {
                    MessageBox.Show("Số lượng không được để trống!", "", MessageBoxButtons.OK);
                    txtSoLuong.Focus();
                    return;
                }
                if (int.Parse(txtSoLuong.Text.Trim()) > sLBD)
                {
                    MessageBox.Show("Số lượng thay đổi không được lớn hơn số lượng ban đầu!", "", MessageBoxButtons.OK);
                    txtSoLuong.Focus();
                    return;
                }
                if (txtDonGia.Text.Trim() == "")
                {
                    MessageBox.Show("Đơn giá không được để trống!", "", MessageBoxButtons.OK);
                    txtDonGia.Focus();
                    return;
                }
                try
                {
                    // cập nhật lại số lượng tồn trước khi thêm CTPX
                    string strLenh = "DECLARE @return_value int EXEC @return_value = [dbo].[SP_CapNhatSoLuongVatTu] @CHEDO = N'N', @MAVT = N'" + txtMaVT.Text + "', @SOLUONG = " + txtSoLuong.Text + " SELECT 'Return Value' = @return_value";
                    SqlDataReader dataReader = Program.ExecSqlDataReader(strLenh);
                    // Đọc và lấy result
                    dataReader.Read();
                    int result_value = (int)dataReader.GetValue(0);
                    dataReader.Close();
                    bdsCTPN.EndEdit();
                    bdsCTPN.ResetCurrentItem();
                    this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.cTPNTableAdapter.Update(this.DS.CTPN);

                    MessageBox.Show("Hiệu chỉnh Chi tiết phiếu nhập thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hiệu chỉnh Chi tiết phiếu nhập \n" + ex.Message, "", MessageBoxButtons.OK);
                    return;
                }
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = btnReload.Enabled = true;
                btnGhi.Enabled = cmbChiNhanh.Enabled = false;
                gcCTPN.Enabled = gcPhieuNhap.Enabled = true;
                groupControl2.Enabled = false;
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String tam = "";
            if (MessageBox.Show("Chọn Yes nếu muốn xóa Phiếu nhập \nChọn No nếu muốn xóa Chi tiết phiếu nhập ", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (bdsCTPN.Count > 0)
                {
                    MessageBox.Show("Không thể xóa phiếu nhập này!", "", MessageBoxButtons.OK);
                    return;
                }
                if (MessageBox.Show("Bạn có thật sự muốn xóa phiếu nhập này?", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        tam = ((DataRowView)bdsPhieuNhap[bdsPhieuNhap.Position])["MAPN"].ToString();
                        bdsPhieuNhap.RemoveCurrent();
                        this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.phieuNhapTableAdapter.Update(this.DS.PhieuNhap);
                        MessageBox.Show("Xóa phiếu nhập thành công!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi Xóa phiếu nhập! \n" + ex.Message, "", MessageBoxButtons.OK);
                        this.phieuNhapTableAdapter.Fill(this.DS.PhieuNhap);
                        bdsPhieuNhap.Position = bdsPhieuNhap.Find("MAPN", tam);
                        return;
                    }
                }
            }
            else
            {
                if (MessageBox.Show("Bạn có thật sự muốn xóa chi tiết phiếu nhập này?", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        tam = ((DataRowView)bdsCTPN[bdsCTPN.Position])["MAPN"].ToString();
                        bdsCTPN.RemoveCurrent();
                        this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.cTPNTableAdapter.Update(this.DS.CTPN);
                        MessageBox.Show("Xóa CTPN thành công!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi Xóa Chi tiết phiếu nhập! \n" + ex.Message, "", MessageBoxButtons.OK);
                        this.cTPNTableAdapter.Fill(this.DS.CTPN);
                        bdsCTPN.Position = bdsCTPN.Find("MAPN", tam);
                        return;
                    }
                }
            }
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = btnReload.Enabled = true;
            btnGhi.Enabled = false;
            groupControl1.Enabled = groupControl2.Enabled = false;
            gcPhieuNhap.Enabled = gcCTPN.Enabled = true;
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}
