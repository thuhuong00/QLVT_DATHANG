using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QLVT_DATHANG.BaoCao
{
    public partial class xrpt_DSNhanVien : DevExpress.XtraReports.UI.XtraReport
    {
        public xrpt_DSNhanVien()
        {
            InitializeComponent();
        }
        public xrpt_DSNhanVien(String macn)
        {
            InitializeComponent();
            this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
            this.sqlDataSource1.Queries[0].Parameters[0].Value = macn;
            this.sqlDataSource1.Fill();
        }

    }
}
