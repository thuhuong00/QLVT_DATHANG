using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace QLVT_DATHANG.BaoCao
{
    public partial class Xprt_TongHopXuatNhap : DevExpress.XtraReports.UI.XtraReport
    {
        public Xprt_TongHopXuatNhap()
        {
            InitializeComponent();
        }
        public Xprt_TongHopXuatNhap(DateTime NgayBD, DateTime NgayKT)
        {
            InitializeComponent();
            this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
            this.sqlDataSource1.Queries[0].Parameters[0].Value = NgayBD;
            this.sqlDataSource1.Queries[0].Parameters[1].Value = NgayKT;
            this.sqlDataSource1.Fill();
        }

    }
}
