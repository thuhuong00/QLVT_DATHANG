using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QLVT_DATHANG.BaoCao
{
    public partial class Xprt_ChiTietPhieu : DevExpress.XtraReports.UI.XtraReport
    {
        public Xprt_ChiTietPhieu()
        {
            InitializeComponent();
        }
        public Xprt_ChiTietPhieu(string role, string loai, DateTime NgayBD, DateTime NgayKT)
        {
            InitializeComponent();
            this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
            this.sqlDataSource1.Queries[0].Parameters[0].Value = role;
            this.sqlDataSource1.Queries[0].Parameters[1].Value = loai;
            this.sqlDataSource1.Queries[0].Parameters[2].Value = NgayBD;
            this.sqlDataSource1.Queries[0].Parameters[3].Value = NgayKT;
            this.sqlDataSource1.Fill();
        }

    }
}
