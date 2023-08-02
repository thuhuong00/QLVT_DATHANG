using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace QLVT_DATHANG.BaoCao
{
    public partial class Xprt_HoatDongCuaNhanVien1 : DevExpress.XtraReports.UI.XtraReport
    {
        public Xprt_HoatDongCuaNhanVien1()
        {
            InitializeComponent();
        }
        public Xprt_HoatDongCuaNhanVien1(int Manv, DateTime NgayBD, DateTime NgayKT)
        {
            InitializeComponent();
            this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
            this.sqlDataSource1.Queries[0].Parameters[0].Value = Manv;
            this.sqlDataSource1.Queries[0].Parameters[1].Value = NgayBD;
            this.sqlDataSource1.Queries[0].Parameters[2].Value = NgayKT;
            this.sqlDataSource1.Fill();
        }

    }
}
