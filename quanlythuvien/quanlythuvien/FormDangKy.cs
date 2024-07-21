using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlythuvien
{
    public partial class FormDangKy : Form
 
   {
        Model.NguoiDung model = new Model.NguoiDung();
        public FormDangKy()
        {
            InitializeComponent();
        }

        private void FormDangKy_Load(object sender, EventArgs e)
        {

        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            FormDangNhap formDangNhap = new FormDangNhap();
            formDangNhap.Show();
            this.Close();
        }

        private void btnDangky_Click(object sender, EventArgs e)
        {
            string account = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();
            string password2 = txtnhaplaimk.Text.Trim();
            string email = txtEmail.Text.Trim();
            string name = txtName.Text;
            string diaChi = "null";
            DateTime date = DateTime.Now;
            string ngaySinh = date.ToString("yyyy-MM-dd");

            if (string.IsNullOrEmpty(account) ||
              string.IsNullOrEmpty(password) ||
              string.IsNullOrEmpty(password2) ||
              string.IsNullOrEmpty(email) ||
              string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
            }
            else if (password != password2)
            {
                MessageBox.Show("Mật khẩu nhập lại không chính xác!");
            }
                else
            {
                string sql = $"INSERT INTO nguoidung (HoTen, TaiKhoan, MatKhau, email, NgaySinh, DiaChi) VALUES ('{name}', '{account}', '{password}', '{email}', '{ngaySinh}', '{diaChi}')";
                int check = model.Sql(sql);
                if (check >= 0) {
                    MessageBox.Show("Bạn đã đăng ký thành công.");
                } else
                {
                    MessageBox.Show("Bạn đã đăng ký thất bại");

                }
            }
        }
    }
}
