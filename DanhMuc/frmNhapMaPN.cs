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
    public partial class frmNhapMaPN : Form
    {
        public frmNhapMaPN()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNhapMaPN.Text != "")
            {
                string strLenh = "DECLARE @return_value int EXEC @return_value = [dbo].[SP_CHECKMA]  " + txtNhapMaPN.Text + ",  MAPN " + " SELECT 'Return Value'  =  @return_value";
                SqlDataReader dataReader = Program.ExecSqlDataReader(strLenh);
                // Đọc và lấy result
                dataReader.Read();
                int result_value = (int)dataReader.GetValue(0);
                dataReader.Close();
                if (result_value != 0)
                {
                    if (result_value == 1)
                    {
                        txtNhapMaPN.Focus();
                        MessageBox.Show("Không thể thêm vì mã phiếu nhập này đã tồn tại ở chi nhánh hiện tại, vui lòng nhập mã khác");
                    }
                    else if (result_value == 2)
                    {
                        txtNhapMaPN.Focus();
                        MessageBox.Show("Không thể thêm vì mã phiếu nhập này đã tồn tại ở chi nhánh khác, vui lòng nhập mã khác");
                    }
                }
                else
                {

                    Program.formPhieuNhap.txtMaPN.Text = txtNhapMaPN.Text;
                    Program.formPhieuNhap.txtMaPN_CTPN.Text = txtNhapMaPN.Text;
                    this.Close();
                }


            }
        }

        private void frmNhapMaPN_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.frmChinh.Enabled = true;
        }
    }
}
