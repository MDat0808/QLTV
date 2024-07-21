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
    public partial class FormQuanLyNguoiDung : Form
    {
        private NguoiDung db = new NguoiDung();

        public FormQuanLyNguoiDung()
        {
            InitializeComponent();
            LoadNguoiDung();
            btnLuu.Enabled = false;
        }


        private void DisplayInfor(int r)

        {
            try
            {

                txtHoTen.Text = dgvNguoidung.Rows[r].Cells["HoTen"].Value.ToString();
                txtDiaChi.Text = dgvNguoidung.Rows[r].Cells["DiaChi"].Value.ToString();
                txtEmail.Text = dgvNguoidung.Rows[r].Cells["email"].Value.ToString();
                dtNgaySinh.Value = DateTime.Parse(dgvNguoidung.Rows[r].Cells["NgaySinh"].Value.ToString());
                txtMk.Text = dgvNguoidung.Rows[r].Cells["MatKhau"].Value.ToString();
                txtQuyen.Text = dgvNguoidung.Rows[r].Cells["Quyen"].Value.ToString();

            }
            catch (Exception) { MessageBox.Show("Sách này hiện tại chưa có."); }

        }

        private void ResetVariable()
        {

            txtHoTen.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
            dtNgaySinh.Value = DateTime.Now;
            txtMk.Text = "";
            txtQuyen.Text = "";
            btnLuu.Enabled = false;
        }

        private void LoadNguoiDung()
        {
            dgvNguoidung.DataSource = db.getUsers();
        }

        private void dgvNguoidung_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int r = dgvNguoidung.CurrentCell.RowIndex;
            if (dgvNguoidung.SelectedRows.Count < 1)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần sửa.");
            }
            else
            {
                MessageBox.Show("Lưu ý không thể chỉnh sửa tài khoản.");
                btnThem.Enabled = false;
                btnLuu.Enabled = true;
                DisplayInfor(r);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int r = dgvNguoidung.CurrentCell.RowIndex;
            btnThem.Enabled = true;
            string id = dgvNguoidung.Rows[r].Cells["id"].Value.ToString();
            string sql = $"update nguoidung set HoTen ='{txtHoTen.Text}', DiaChi = '{txtDiaChi.Text}', email = '{txtEmail.Text}', Quyen = '{txtQuyen.Text}', MatKhau = '{txtMk.Text}', NgaySinh = '{dtNgaySinh.Value.ToString("yyyy-MM-dd")}' where id = '{id}' ";
            int check = db.Sql(sql);
            if (check >= 0)
            {
                MessageBox.Show("Cập nhật dữ liệu thành công.");
                LoadNguoiDung();
            }
            else
            {
                MessageBox.Show("Cập nhật dữ liệu thất bại.");
            }
            ResetVariable();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {

                string sql = $"insert into nguoidung (HoTen, DiaChi, email, Quyen, TaiKhoan, MatKhau ,NgaySinh, NgayTao) values ('{txtHoTen.Text}', '{txtDiaChi.Text}', '{txtEmail.Text}', '{txtQuyen.Text}', '{txtTk.Text}', '{txtMk.Text}' , '{dtNgaySinh.Value.ToString("yyyy-MM-dd")}' ,'{DateTime.Now.ToString("yyyy-MM-dd")}') ";
                int check = db.Sql(sql);
                if (check >= 0)
                {
                    MessageBox.Show("Tạo mới người dùng thành công.");
                    ResetVariable();
                    LoadNguoiDung();
                }
                else
                {
                    MessageBox.Show("Tạo mới người dùng thất bại.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaTk.Text))
            {
                MessageBox.Show("Vui lòng nhập mã người dùng");
            }
            else
            {
                DataTable data = db.getNguoidungTheoid(int.Parse(txtMaTk.Text));
                if (data.Rows.Count <= 0)
                {
                    MessageBox.Show("Mã người dùng không hợp lệ.");
                }
                else
                {
                    dgvNguoidung.DataSource = data;
                }
                txtMaTk.Text = "";
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int r = this.dgvNguoidung.CurrentCell.RowIndex;
            string id = this.dgvNguoidung.Rows[r].Cells[0].Value.ToString();
            DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa người dùng này không?", "Xóa", MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string sql = $"Delete from nguoidung where id = '{id}'";
                int check = db.Sql(sql);
                if (check >= 0)
                {
                    MessageBox.Show("Xóa thành công.");
                    LoadNguoiDung();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại thử lại sau.");
                }
            }
        }
    }
}
