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
    public partial class frmVatTu : Form
    {
        String macn = "";
        int vitri = 0;
        String button = "";
        public frmVatTu()
        {
            InitializeComponent();
        }

        private void vattuBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsVatTu.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        private void frmVatTu_Load(object sender, EventArgs e)
        {
            DS.EnforceConstraints = false;

            this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
            this.vattuTableAdapter.Fill(this.DS.Vattu);
            this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTDDHTableAdapter.Fill(this.DS.CTDDH);
            this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTPNTableAdapter.Fill(this.DS.CTPN);
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
            }
            else
            {
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnGhi.Enabled = btnPhucHoi.Enabled = btnChuyenChiNhanh.Enabled = true;
                cmbChiNhanh.Enabled = btnThoat.Enabled = false;
            }

            gcVT.Enabled = true;
            pncVT.Enabled = false;
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsVatTu.Position;
            bdsVatTu.AddNew();

            btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnThoat.Enabled = btnReload.Enabled = btnChuyenChiNhanh.Enabled = false;
            btnPhucHoi.Enabled = btnGhi.Enabled = true;
            gcVT.Enabled = false;
            pncVT.Enabled = true;
            button = "Them";
        }

        private void btnHieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsVatTu.Position;
            pncVT.Enabled = true;
            txtMaVT.Enabled = false;

            btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnThoat.Enabled = btnReload.Enabled = btnChuyenChiNhanh.Enabled = false;
            btnPhucHoi.Enabled = btnGhi.Enabled = true;
            gcVT.Enabled = false;
            button = "HieuChinh";
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtMaVT.Text.Trim() == "" || txtMaVT.Text.Trim().Length > 4)
            {
                MessageBox.Show("Mã vật tư không được để trống và không quá 4 ký tự!", "", MessageBoxButtons.OK);
                txtMaVT.Focus();
                return;
            }
            if (txtTenVT.Text.Trim() == "")
            {
                MessageBox.Show("Tên vật tư không được để trống", "", MessageBoxButtons.OK);
                txtTenVT.Focus();
                return;
            }
            if (txtDVT.Text.Trim() == "")
            {
                MessageBox.Show("Đơn vị tính không được để trống", "", MessageBoxButtons.OK);
                txtDVT.Focus();
                return;
            }
            if (txtSoLuongTon.Text.Trim() == "")
            {
                MessageBox.Show("Số lượng tồn không được để trống", "", MessageBoxButtons.OK);
                txtSoLuongTon.Focus();
                return;
            }
            if (button == "Them")
            {

                String strLenh = "DECLARE @return_value int EXEC @return_value = [dbo].[SP_CHECKMA]  '" + txtMaVT.Text + "', N'MAVT'" + " SELECT 'Return Value'  =  @return_value";
                SqlDataReader dataReader = Program.ExecSqlDataReader(strLenh);
                dataReader.Read();
                int result_value = (int)dataReader.GetValue(0);
                dataReader.Close();

                if (result_value != 0)
                {
                    MessageBox.Show("Mã kho đã tồn tại, vui lòng nhập mã khác");
                    txtMaVT.Focus();
                    Program.conn.Close();
                    return;
                }
                else if (Program.kt == 0)
                {
                    try
                    {
                        bdsVatTu.EndEdit();
                        bdsVatTu.ResetCurrentItem();
                        this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.vattuTableAdapter.Fill(this.DS.Vattu);


                        MessageBox.Show("Thêm vật tư thành công");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi ghi kho \n" + ex.Message, "", MessageBoxButtons.OK);
                        return;
                    }

                    btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnThoat.Enabled = btnReload.Enabled = btnChuyenChiNhanh.Enabled = btnThoat.Enabled = true;
                    btnPhucHoi.Enabled = btnGhi.Enabled = false;
                    gcVT.Enabled = true;
                    pncVT.Enabled = false;
                }
                
            }
            else
            {
                try
                {
                    bdsVatTu.EndEdit();
                    bdsVatTu.ResetCurrentItem();
                    this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.vattuTableAdapter.Fill(this.DS.Vattu);


                    MessageBox.Show("Hiệu chỉnh kho thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hiệu chỉnh nhân viên \n" + ex.Message, "", MessageBoxButtons.OK);
                    return;
                }
                btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnThoat.Enabled = btnReload.Enabled = btnChuyenChiNhanh.Enabled = true;
                btnPhucHoi.Enabled = btnGhi.Enabled = false;
                gcVT.Enabled = true;
                pncVT.Enabled = false;
                txtMaVT.Enabled = true;
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String maVT = "";
            if (bdsVatTu.Count == 0)
            {
                return;
            }
            else
            {
                if (bdsCTDDH.Count > 0)
                {
                    MessageBox.Show("Không thể xóa vì vật tư này đã nhập hàng!", "", MessageBoxButtons.OK);
                    return;
                }
                if (bdsCTPN.Count > 0)
                {
                    MessageBox.Show("Không thể xóa vì vật tư này đã nằm trong Chi tiết phiếu nhập!", "", MessageBoxButtons.OK);
                    return;
                }
                if (bdsCTPX.Count > 0)
                {
                    MessageBox.Show("Không thể xóa vì vật tư này đã nằm trong Chi tiết phiếu xuất!", "", MessageBoxButtons.OK);
                    return;
                }
                if (MessageBox.Show("Bạn có thật sự muốn xóa vật tư này", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        maVT = ((DataRowView)bdsVatTu[bdsVatTu.Position])["MAVT"].ToString();
                        bdsVatTu.RemoveCurrent();
                        this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.vattuTableAdapter.Update(this.DS.Vattu);
                        MessageBox.Show("Xóa vật tư thành công");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi Xóa vật tư \n" + ex.Message, "", MessageBoxButtons.OK);
                        this.vattuTableAdapter.Fill(this.DS.Vattu);
                        bdsVatTu.Position = bdsVatTu.Find("MAVT", maVT);
                        return;
                    }
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
                this.vattuTableAdapter.Fill(this.DS.Vattu);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Reload vật tư \n" + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DS.EnforceConstraints = false;

            this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
            this.vattuTableAdapter.Fill(this.DS.Vattu);
            this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTDDHTableAdapter.Fill(this.DS.CTDDH);
            this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTPNTableAdapter.Fill(this.DS.CTPN);
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
            }
            else
            {
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnGhi.Enabled = btnPhucHoi.Enabled = btnChuyenChiNhanh.Enabled = true;
                cmbChiNhanh.Enabled = btnThoat.Enabled = false;
            }

            gcVT.Enabled = true;
            pncVT.Enabled = false;
        }
    }
}
