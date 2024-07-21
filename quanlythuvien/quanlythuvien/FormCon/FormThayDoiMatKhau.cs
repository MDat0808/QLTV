using quanlythuvien.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlythuvien.FormCon
{
    public partial class FormThayDoiMatKhau : Form
    {
        private int userId;
        private NguoiDung model = new NguoiDung();
        public FormThayDoiMatKhau(int id)
        {
            this.userId = id;
            InitializeComponent();
        }

        private void FormDangKy_Load(object sender, EventArgs e)
        {

        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
           
            this.Close();
        }

        private void btnDangky_Click(object sender, EventArgs e)
        {
            string mkcu = txtMKOld.Text.Trim();
            string password = txtPass.Text.Trim();
            string password2 = txtnhaplaimk.Text.Trim();
            
       

            if (string.IsNullOrEmpty(mkcu) ||
              string.IsNullOrEmpty(password) ||
              string.IsNullOrEmpty(password2))
   
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
            }
            else if (password != password2)
            {
                MessageBox.Show("Mật khẩu nhập lại không chính xác!");
            }
            else
            {
                string sql = $"Select MatKhau from nguoidung where id = '{userId}'";
                string check = model.getMatKhauById(sql);
                if (check == mkcu)
                {
                    string update = $"update nguoidung set MatKhau = '{password}' where id = '{userId}'";
                    int isCheck = model.Sql(update);
                    if (isCheck >= 0)
                    {
                        MessageBox.Show("Cập nhật mật khẩu thành công.");
                    } else
                    {
                        MessageBox.Show("Cập nhật mật khẩu thất bại, vui lòng thử lại sau.");
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu hiện tại không chính xác.");

                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtMKOld_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
