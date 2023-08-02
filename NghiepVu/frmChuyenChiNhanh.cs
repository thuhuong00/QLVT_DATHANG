using DevExpress.CodeParser;
using DevExpress.DataProcessing.InMemoryDataProcessor;
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
    public partial class frmChuyenChiNhanh : Form
    {
        string macn = "";
        int maNV;
        public frmChuyenChiNhanh(int manv)
        {
            InitializeComponent();
            maNV = manv;

        }

        private void frmChuyenChiNhanh_Load(object sender, EventArgs e)
        {

            cmbChiNhanhChuyen.Items.Add("CN1");
            cmbChiNhanhChuyen.Items.Add("CN2");
            cmbChiNhanhChuyen.SelectedItem = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (cmbChiNhanhChuyen.SelectedIndex == 0)
            {
                macn = "CN1";
            }else
            {
                macn = "CN2";
            }

            if (cmbChiNhanhChuyen.SelectedIndex == Program.mphanmanh) 
            {
                MessageBox.Show("Chi nhánh được chuyển cần là một chi nhánh khác chi nhánh hiện tại ", "", MessageBoxButtons.OK);
            } 
            else
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn chuyển nhân viên này đi ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.OK)
                {
                    try
                    {
                        String strLenh = "DECLARE @return_value int EXEC  @return_value = [dbo].[sp_ChuyenChiNhanh] @MANV = " + maNV +", @MACN = N'" + macn + "' SELECT  'Return Value' = @return_value";
                        SqlDataReader dataReader = Program.ExecSqlDataReader(strLenh);
                        // Đọc và lấy result
                        dataReader.Read();
                        int result_value = (int)dataReader.GetValue(0);
                        dataReader.Close();

                        if (result_value == 0)
                        {
                            MessageBox.Show("Chuyển chi nhánh nhân viên thành công");
                            Program.frmChinh.Enabled = true;
                            Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi \n" + ex.Message, "", MessageBoxButtons.OK);
                        return;
                    }
                
                }
                else
                {
                    return;
                }
            }    
           
            
            
        }

        private void frmChuyenChiNhanh_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.frmChinh.Enabled = true;
        }

        private void cmbChiNhanhChuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}
