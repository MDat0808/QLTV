using quanlythuvien.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace quanlythuvien
{
    public partial class FormDangNhap : Form
    {
        Model.NguoiDung model = new Model.NguoiDung();

        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangky_Click(object sender, EventArgs e)
        {
            FormDangKy form = new FormDangKy();
            this.Hide();
            form.ShowDialog();
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            string account = txtUser.Text.Trim();
            string mk = txtPass.Text.Trim();

            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(mk))
            {
                MessageBox.Show("Vui lòng nhập thông tin.");
                return;
            }
            // Kiểm tra tài khoản
            string sql = $"SELECT quyen FROM nguoidung WHERE TaiKhoan = '{account}' and MatKhau = '{mk}'";
            string getId = $"SELECT id FROM nguoidung WHERE TaiKhoan = '{account}' and MatKhau = '{mk}'";
            int check = model.Login(sql);
            int id = model.getId(getId);
            if (check == 1)
            {
                this.Hide();
                FormMain form = new FormMain(id);
                form.ShowDialog();
                
            } else if (check == 0)
            {
                this.Hide();
                FormNguoiDung form = new FormNguoiDung(id);
                form.ShowDialog();
            } else
            {
                MessageBox.Show("Đăng nhập thất bại.");
            }

        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
