using DevExpress.XtraReports.UI;
using QLVT_DATHANG.BaoCao;
using QLVT_DATHANG.DanhMuc;
using QLVT_DATHANG.NghiepVu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static DevExpress.Utils.HashCodeHelper;

namespace QLVT_DATHANG
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmMain()
        {
            InitializeComponent();
        }
        
        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }

        private void btnDangNhap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmDangNhap));
            if (frm != null)
            {
                frm.Activate();
            }
            else
            {
                frmDangNhap f = new frmDangNhap();
                f.MdiParent = this;
                f.Show();
            }
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            // nhóm Tài khoản
            rpNghiepVu.Visible = true;
            rpDanhMuc.Visible = true;
            rpBaoCao.Visible = true;
            Program.frmChinh.btnDangNhap.Enabled = true;
            Program.frmChinh.btnDangXuat.Enabled = false;
            Program.frmChinh.btnTaoTaiKhoan.Enabled = false;
        }

        private void btnKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmKho));
            if (frm != null) frm.Activate();
            else
            {
                Program.formKho = new frmKho();
                Program.formKho.MdiParent = this;
                Program.formKho.Show();
            }
        }

        private void btnNhanVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmNhanVien));
            if (frm != null) frm.Activate();
            else
            {
                Program.formNhanVien = new frmNhanVien();
                Program.formNhanVien.MdiParent = this;
                Program.formNhanVien.Show();
            }
        }

        private void btnDatHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmDatHang));
            if (frm != null) frm.Activate();
            else
            {
                Program.formDatHang = new frmDatHang();
                Program.formDatHang.MdiParent = this;
                Program.formDatHang.Show();
            }
        }

        private void btnPhieuNhap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmPhieuNhap));
            if (frm != null) frm.Activate();
            else
            {
                Program.formPhieuNhap = new frmPhieuNhap();
                Program.formPhieuNhap.MdiParent = this;
                Program.formPhieuNhap.Show();
            }

        }

        private void btnPhieuXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmPhieuXuat));
            if (frm != null) frm.Activate();
            else
            {
                Program.formPhieuXuat = new frmPhieuXuat();
                Program.formPhieuXuat.MdiParent = this;
                Program.formPhieuXuat.Show();
            }
        }

        private void btnInNhanVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmChonChiNhanh));
            if (frm != null) frm.Activate();
            else
            {
                Program.formChonChiNhanh = new frmChonChiNhanh();
                Program.formChonChiNhanh.MdiParent = this;
                Program.formChonChiNhanh.Show();
            }
        }

        private void btnInVatTu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Xprt_DSVatTu report = new Xprt_DSVatTu();
            ReportPrintTool print = new ReportPrintTool(report);
            print.ShowPreviewDialog();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmChiTietPhieu));
            if (frm != null) frm.Activate();
            else
            {
                Program.formChiTietPhieu = new frmChiTietPhieu();
                Program.formChiTietPhieu.MdiParent = this;
                Program.formChiTietPhieu.Show();
            }
        }

        private void btnDonKhongPhieuNhap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Xprt_DSDDHKhongPhieuNhap report = new Xprt_DSDDHKhongPhieuNhap();
            ReportPrintTool print = new ReportPrintTool(report);
            print.ShowPreviewDialog();
        }

        private void btnBaoCaoNhanVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmHoatDongCuaNhanVien));
            if (frm != null) frm.Activate();
            else
            {
                Program.formHoatDongCuaNhanVien = new frmHoatDongCuaNhanVien();
                Program.formHoatDongCuaNhanVien.MdiParent = this;
                Program.formHoatDongCuaNhanVien.Show();
            }
        }

        private void btnTongHopXuatNhap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmTongHopXuatNhap));
            if (frm != null) frm.Activate();
            else
            {
                Program.formTongHopXuatNhap = new frmTongHopXuatNhap();
                Program.formTongHopXuatNhap.MdiParent = this;
                Program.formTongHopXuatNhap.Show();
            }
        }

        private void btnVatTu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmVatTu));
            if (frm != null) frm.Activate();
            else
            {
                Program.formVatTu = new frmVatTu();
                Program.formVatTu.MdiParent = this;
                Program.formVatTu.Show();
            }
        }
    }
}
