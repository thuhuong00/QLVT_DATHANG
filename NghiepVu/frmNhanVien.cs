using QLVT_DATHANG.DanhMuc;
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
   
    public partial class frmNhanVien : Form
    {
        String macn = "";
        int vitri = 0;
        String button = "";
        public static string maCNChuyen = "";
        
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void nhanVienBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsNhanVien.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            DS.EnforceConstraints = false;

            this.NhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
            this.NhanVienTableAdapter.Fill(this.DS.NhanVien);
            
            this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
            this.datHangTableAdapter.Fill(this.DS.DatHang);
            
            this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuNhapTableAdapter.Fill(this.DS.PhieuNhap);
            
            this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuXuatTableAdapter.Fill(this.DS.PhieuXuat);
            macn = Program.maphanmanh;
            //CapNhat_MaKhoa();
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

            gcNV.Enabled = true;
            pncNV.Enabled = false;


        }

        private void trangThaiXoaLabel_Click(object sender, EventArgs e)
        {

        }

        private void trangThaiXoaCheckEdit_CheckedChanged(object sender, EventArgs e)
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
                MessageBox.Show("Lỗi kết nối về khoa mới !", "", MessageBoxButtons.OK);
            }
            else
            {
                this.NhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
                this.NhanVienTableAdapter.Fill(this.DS.NhanVien);

                this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                this.datHangTableAdapter.Fill(this.DS.DatHang);

                this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuNhapTableAdapter.Fill(this.DS.PhieuNhap);

                this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuXuatTableAdapter.Fill(this.DS.PhieuXuat);
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsNhanVien.Position;
            pncNV.Enabled = true;
            bdsNhanVien.AddNew();
            txtMaCN.Text = macn;
            txtNgaySinh.EditValue = "";
            ckTrangThaiXoa.Text = "0";
            txtMaNV.Enabled = true;



            btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnThoat.Enabled = btnReload.Enabled = btnChuyenChiNhanh.Enabled = false;
            btnPhucHoi.Enabled = btnGhi.Enabled = true;
            gcNV.Enabled = false;
            button = "Them";
        }

        private void btnHieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsNhanVien.Position;
            pncNV.Enabled = true;
            txtMaNV.Enabled = false;

            btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnThoat.Enabled = btnReload.Enabled = btnChuyenChiNhanh.Enabled = false;
            btnPhucHoi.Enabled = btnGhi.Enabled = true;
            gcNV.Enabled = false;
            button = "HieuChinh";
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtMaNV.Text.Trim() == "")
            {
                MessageBox.Show("Mã nhân viên không được để trống", "", MessageBoxButtons.OK);
                txtMaNV.Focus();
                return;
            }
            if (txtHo.Text.Trim() == "")
            {
                MessageBox.Show("Họ nhân viên không được để trống", "", MessageBoxButtons.OK);
                txtHo.Focus();
                return;
            }
            if (txtTen.Text.Trim() == "")
            {
                MessageBox.Show("Tên nhân viên không được để trống", "", MessageBoxButtons.OK);
                txtTen.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Địa chỉ nhân viên không được để trống", "", MessageBoxButtons.OK);
                txtDiaChi.Focus();
                return;
            }
            if (txtNgaySinh.Text.Trim() == "")
            {
                MessageBox.Show("Ngày sinh nhân viên không được để trống", "", MessageBoxButtons.OK);
                txtNgaySinh.Focus();
                return;
            }
            if (txtLuong.Text.Trim() == "")
            {
                MessageBox.Show("Lương nhân viên không được để trống", "", MessageBoxButtons.OK);
                txtLuong.Focus();
                return;
            }

            if (button == "Them")
            {
                
                String strLenh = "DECLARE @return_value int EXEC @return_value = [dbo].[SP_CHECKMA]  '" + txtMaNV.Text + "', N'MANV'" + " SELECT 'Return Value'  =  @return_value";
                SqlDataReader dataReader = Program.ExecSqlDataReader(strLenh);
                // Đọc và lấy result
                dataReader.Read();
                int result_value = (int)dataReader.GetValue(0);
                dataReader.Close();


                if (result_value == 0)
                {
                    try
                    {
                        MessageBox.Show(txtMaNV.Text + txtMaCN.Text + ckTrangThaiXoa.Text  , "", MessageBoxButtons.OK);
                        bdsNhanVien.EndEdit();
                        bdsNhanVien.ResetCurrentItem();
                        
                        this.NhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.NhanVienTableAdapter.Fill(this.DS.NhanVien);

                        MessageBox.Show("Thêm nhân viên thành công");




                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi ghi nhân viên \n" + ex.Message, "", MessageBoxButtons.OK);
                        return;
                    }

                    btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnThoat.Enabled = btnReload.Enabled = btnChuyenChiNhanh.Enabled = btnThoat.Enabled = true;
                    btnPhucHoi.Enabled = btnGhi.Enabled = false;
                    gcNV.Enabled = true;
                    pncNV.Enabled = false;

                   
                }
                else
                {
                    MessageBox.Show("Mã nhân viên đã tồn tại, vui lòng nhập mã khác");
                    txtMaNV.Focus();
                    return;
                }
            }
            else
            {
                try
                {
                    bdsNhanVien.EndEdit();
                    bdsNhanVien.ResetCurrentItem();
                    this.NhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.NhanVienTableAdapter.Fill(this.DS.NhanVien);

                    MessageBox.Show("Hiệu chỉnh nhân viên thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hiệu chỉnh nhân viên \n" + ex.Message, "", MessageBoxButtons.OK);
                    return;
                }
                btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnThoat.Enabled = btnReload.Enabled = btnChuyenChiNhanh.Enabled = true;
                btnPhucHoi.Enabled = btnGhi.Enabled = false;
                gcNV.Enabled = true;
                pncNV.Enabled = false;
                txtMaNV.Enabled = true;
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            int manv = int.Parse(((DataRowView)bdsNhanVien[bdsNhanVien.Position])["MANV"].ToString());
            if (MessageBox.Show("Bạn có thật sự muốn xóa nhân viên này", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    bdsNhanVien.RemoveCurrent();
                    this.NhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.NhanVienTableAdapter.Update(this.DS.NhanVien);

                    String strLenh = "EXECUTE dbo.SP_XOANV " + manv;
                    Program.Execute(strLenh);
                    if (Program.kt == 0)
                    {
                        MessageBox.Show("Xóa nhân viên thành công");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi Xóa nhân viên \n" + ex.Message, "", MessageBoxButtons.OK);
                    this.NhanVienTableAdapter.Fill(this.DS.NhanVien);
                    bdsNhanVien.Position = bdsNhanVien.Find("MANV", manv);
                    return;
                }
            }
            bdsNhanVien.Position = bdsNhanVien.Position - 1;
            if (bdsNhanVien.Position < 0)
                btnXoa.Enabled = btnHieuChinh.Enabled = false;
            else
                btnXoa.Enabled = btnHieuChinh.Enabled = true;
        }

        private void btnChuyenChiNhanh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int manv = int.Parse(((DataRowView)bdsNhanVien[bdsNhanVien.Position])["MANV"].ToString());
            string trangThaiXoa = ((DataRowView)bdsNhanVien[bdsNhanVien.Position])["TrangThaiXoa"].ToString();
            if (trangThaiXoa == "False") 
            {
                Program.formChuyenChiNhanh = new frmChuyenChiNhanh(manv);
                Program.formChuyenChiNhanh.Show();
                Program.frmChinh.Enabled = false;
                this.NhanVienTableAdapter.Fill(this.DS.NhanVien);
            } else
            {
                MessageBox.Show(" Nhân viên đã được xóa không chuyển được.\n", "", MessageBoxButtons.OK);
            }

        }

        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.NhanVienTableAdapter.Fill(this.DS.NhanVien);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Reload nhân viên \n" + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void nGAYSINHDateEdit_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
