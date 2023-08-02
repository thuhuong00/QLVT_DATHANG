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

namespace QLVT_DATHANG
{
    public partial class frmDangNhap : Form
    {


        private SqlConnection conn_publisher = new SqlConnection();
        private void lay_dspm(String cmd)
        {
            DataTable dt = new DataTable();
            if (conn_publisher.State == ConnectionState.Closed) conn_publisher.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn_publisher);
            da.Fill(dt);
            conn_publisher.Close();
            Program.bds_dspm.DataSource = dt;
            cmbChiNhanh.DataSource = Program.bds_dspm;
            cmbChiNhanh.DisplayMember = "TENCN";
            cmbChiNhanh.ValueMember = "TENSERVER";
        }
        private int KetNoi_CSDLGOC()
        {
            if (conn_publisher != null && conn_publisher.State == ConnectionState.Open)
                conn_publisher.Close();
            try
            {
                conn_publisher.ConnectionString = Program.connstr_publisher;
                conn_publisher.Open();
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi kế nối về cơ sở dữ liệu gốc.\n Bạn xem lại Tên Server của Publisher, và tên CSDL", "Lỗi đăng nhập", MessageBoxButtons.OK);
                return 0;
            }
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            if (KetNoi_CSDLGOC() == 0)
                return;
            lay_dspm("SELECT * FROM V_DS_PHANMANH");
            cmbChiNhanh.SelectedIndex = 1;
            cmbChiNhanh.SelectedIndex = 0;
        }



        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text.Trim() == "" || txtMatKhau.Text.Trim() == "")
            {
                MessageBox.Show("Login name và mật khẩu không được trống ", "", MessageBoxButtons.OK);
                return;
            }
            string strLenh = "";
            Program.mlogin = txtTaiKhoan.Text;
            Program.password = txtMatKhau.Text;
            if (Program.KetNoi() == 0)
            {
                return;

            }
            Program.mphanmanh = cmbChiNhanh.SelectedIndex;
            Program.mloginDN = Program.mlogin;
            Program.passDN = Program.password;
            strLenh = "EXEC SP_DANGNHAP '" + Program.mlogin + "'";

            Program.myReader = Program.ExecSqlDataReader(strLenh);
            if (Program.myReader == null) return;
            Program.myReader.Read();

            Program.username = Program.myReader.GetString(0);
            if (Convert.IsDBNull(Program.username))
            {
                MessageBox.Show("Login bạn không có quyền truy cập dữ liệu,\n bạn xem lại username và password", "", MessageBoxButtons.OK);
                return;
            }
            Program.mhoten = Program.myReader.GetString(1);
            Program.mgroup = Program.myReader.GetString(2);
            Program.myReader.Close();
            Program.conn.Close();

            if (Program.mphanmanh == 0) Program.maphanmanh = "CN1";
            else Program.maphanmanh = "CN2";

            Program.frmChinh.tssl_Ma.Text = Program.username;
            Program.frmChinh.tssl_HoTen.Text = Program.mhoten;
            Program.frmChinh.tssl_Nhom.Text = Program.mgroup;
            if (Program.maphanmanh != "USER")
            {
                Program.frmChinh.btnTaoTaiKhoan.Enabled = true;
            }

            MessageBox.Show("Đăng nhập thành công vào " + Program.maphanmanh, "", MessageBoxButtons.OK);
            this.Close();
        }

        private void cmbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Program.servername = cmbChiNhanh.SelectedValue.ToString();

            }
            catch (Exception) { }
        }
    }
}
