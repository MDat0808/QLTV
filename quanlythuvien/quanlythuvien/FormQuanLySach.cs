using quanlythuvien.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlythuvien
{
    public partial class FormQuanLySach : Form
    {
        private Sach db = new Sach();
        public FormQuanLySach()
        {
            InitializeComponent();
        }

        private void FormQuanLySach_Load(object sender, EventArgs e)
        {
            LoadSach();
            btnLuu.Enabled = false;
        }

        private void LoadSach()
        {
            dgvSach.DataSource = db.getBooks();

        }

        private void ResetVariable()
        {

            txtTenTG.Text = "";
            txtTensach.Text = "";
            txtTenNXB.Text = "";
            dtNgayphathanh.Value = DateTime.Now;
            txtGia.Text = "";
            txtSoLuong.Text = "";
        }
   

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {

                string sql = $"insert into sach (TenSach, TacGia, SoLuong, NhaXuatBan, Gia, NamXuatBan ,NgayNhap) values ('{txtTensach.Text}', '{txtTenTG.Text}', '{txtSoLuong.Text}', '{txtTenNXB.Text}', '{txtGia.Text}', '{dtNgayphathanh.Value.ToString("yyyy-MM-dd")}' ,'{DateTime.Now.ToString("yyyy-MM-dd")}') ";
                int check = db.Sql(sql);
                if (check >= 0)
                {
                    MessageBox.Show("Tạo mới sách thành công.");
                ResetVariable();
                LoadSach();
                }
                else
                {
                    MessageBox.Show("Tạo mới sách  thất bại.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int r = this.dgvSach.CurrentCell.RowIndex;
            string maSach = this.dgvSach.Rows[r].Cells[0].Value.ToString();
            DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa sách này không?", "Xóa", MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string sql = $"Delete from sach where MaSach = '{maSach}'";
                int check = db.Sql(sql);
                if (check >= 0)
                {
                    MessageBox.Show("Xóa thành công.");
                    LoadSach();

                }
                else
                {
                    MessageBox.Show("Xóa thất bại thử lại sau.");
                }
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int r = dgvSach.CurrentCell.RowIndex;
            if (dgvSach.SelectedRows.Count < 1)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần sửa.");
            } else
            {
            btnThem.Enabled = false;
                btnLuu.Enabled = true;
             DisplayInfor(r);
            } 
        }
        private void DisplayInfor(int r)

        {
                try { 
            
            txtTenTG.Text = dgvSach.Rows[r].Cells["TacGia"].Value.ToString();
            txtTensach.Text = dgvSach.Rows[r].Cells["TenSach"].Value.ToString();
             txtTenNXB.Text = dgvSach.Rows[r].Cells["NhaXuatBan"].Value.ToString();
           dtNgayphathanh.Value = DateTime.Parse(dgvSach.Rows[r].Cells["NgayNhap"].Value.ToString());
            txtGia.Text = dgvSach.Rows[r].Cells["Gia"].Value.ToString();
            txtSoLuong.Text = dgvSach.Rows[r].Cells["SoLuong"].Value.ToString();
            
            }
            catch (Exception ) { MessageBox.Show("Sách này hiện tại chưa có."); }

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int r = dgvSach.CurrentCell.RowIndex;
            btnThem.Enabled = true;
            string maSach = dgvSach.Rows[r].Cells["MaSach"].Value.ToString();
            string sql = $"update sach set TenSach ='{txtTensach.Text}', TacGia = '{txtTenTG.Text}', SoLuong = '{txtSoLuong.Text}', NhaXuatBan = '{txtTenNXB.Text}', Gia = '{txtGia.Text}', NgayNhap = '{dtNgayphathanh.Value.ToString("yyyy-MM-dd")}' where MaSach = '{maSach}' ";
            int check = db.Sql(sql);
            if (check >=0)
            {
                MessageBox.Show("Cập nhật dữ liệu thành công.");
            } else
            {
                MessageBox.Show("Cập nhật dữ liệu thất bại.");
            }
            ResetVariable();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaSach.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sách.");
            }
            else
            {
                DataTable data = db.getSachTheoMaSach(int.Parse(txtMaSach.Text));
                if (data.Rows.Count <= 0)
                {
                    MessageBox.Show("Mã sách không hợp lệ.");
                }
                else
                {
                    dgvSach.DataSource = data;
                }
                txtMaSach.Text = "";
            }
        }

        private void dgvSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
