using quanlythuvien.FormCon;
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

namespace quanlythuvien
{
    public partial class FormTaiKhoan : Form
    {
        private NguoiDung db = new NguoiDung();
        private Sach sach = new Sach();
        private int userId;

        public FormTaiKhoan(int userID)
        {
            InitializeComponent();
            this.userId = userID;
      
        }

        private void FormTaiKhoan_Load(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView1.DataSource = sach.dsSachDaMuon(userId);
                DataTable information = db.getUserById(userId) ;
                DisplayInfo(information);
                btnThanhToan.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Bạn chưa có thông tin tài khoản");
            }
        }

        private void DisplayInfo(DataTable source)
        {
            this.txtTenDangNhap.Text = source.Rows[0]["TaiKhoan"].ToString();
            this.txtMatKhau.Text = source.Rows[0]["MatKhau"].ToString();
            this.txtHoVaTen.Text = source.Rows[0]["HoTen"].ToString();
            this.txtDiaChi.Text = source.Rows[0]["DiaChi"].ToString();
            this.txtEmail.Text = source.Rows[0]["Email"].ToString();
            this.txtNgaySinh.Text = source.Rows[0]["NgaySinh"].ToString();
        }

        private void btnSachCuaToi_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = sach.dsSachDaMuon(userId);
            btnThanhToan.Enabled = false;

        }

        private void btnSachDaLuu_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = sach.dsSachChuaThanhToan(userId);
            btnThanhToan.Enabled = true;
        }

        private void btnThayDoi_Click(object sender, EventArgs e)
        {
            string ten = this.txtHoVaTen.Text;
            string ngaysinh = this.txtNgaySinh.Text;
            DateTime date = DateTime.ParseExact(ngaysinh, "M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string formattedDate = date.ToString("yyyy-MM-dd");
            string diachi = this.txtDiaChi.Text;
            string email = this.txtEmail.Text;

            DialogResult dr = MessageBox.Show("Bạn có chắc muốn cập nhật thông tin cá nhân không?", "Lưu", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string sql = $"Update nguoidung set HoTen = '{ten}', NgaySinh = '{formattedDate}', DiaChi = '{diachi}', email = '{email}' where id = '{userId}'";
                int check = db.Sql(sql);
                if (check >= 0)
                {
                MessageBox.Show("Lưu thành công thành công.");

                } else
                {
                    MessageBox.Show("Lưu thất bại.");
                }
            }
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            FormThayDoiMatKhau form = new FormThayDoiMatKhau(userId);
            this.Hide();
            form.ShowDialog();
        }

        private void btn_DanhGia_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
            int r = this.dataGridView1.CurrentCell.RowIndex;
            string maHD = this.dataGridView1.Rows[r].Cells[0].Value.ToString();
            int gia = int.Parse(this.dataGridView1.Rows[r].Cells["Gia"].Value.ToString());
            FormThanToanHD form = new FormThanToanHD(userId, gia,int.Parse(maHD));
            form.ShowDialog();

            } catch {
                MessageBox.Show("Hiện tại đã thanh toán tất cả sách thuê.");
            }
        }
    }
}
