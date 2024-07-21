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
    public partial class FormQuanLyHoaDon : Form
    {
        private Model.HoaDon db = new Model.HoaDon();
        private string mahoadon = "%";
        private string mataikhoan = "%";
        private int year = 0;
        private int month = 1;
        private int day = 0;

        public FormQuanLyHoaDon()
        {
            InitializeComponent();
        }

        private void FormQuanLyHoaDon_Load(object sender, EventArgs e)
        {
            LoadHoaDon();
            LoadNgayThangNam();
            btnLuu.Enabled = false;
        }


        private void LoadNgayThangNam()
        {
            this.cbbYear.Items.Add("");
            this.cbbMonth.Items.Add("");
            this.cbbDay.Items.Add("");
            for (int i = 2024; i <= 2025; i++)
            {
                this.cbbYear.Items.Add(i);
            }

            for (int i = 1; i <= 12; i++)
            {
                this.cbbMonth.Items.Add(i);
            }

            for (int i = 1; i <= 31; i++)
            {
                this.cbbDay.Items.Add(i);
            }
        }

        private void LoadHoaDon() {
            dgvHoaDon.DataSource = db.getHoaDon();

        }

        private void resetHoaDon()
        {
            txtGia.Text = "";
            txtTrangThai.Text = "";
        }

       

        private void btnSach_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void btnTacgia_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void btnGiongdoc_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void btnKhachhang_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void btnDanhgia_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtMaHoaDon.Text))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn.");
            } else
            {
                DataTable data = db.getHoaDonTheoMaHD(int.Parse(txtMaHoaDon.Text));
                if (data.Rows.Count <= 0)
                {
                    MessageBox.Show("Mã hóa đơn không hợp lệ.");
                } else
                {
                    dgvHoaDon.DataSource = data;
                }
                txtMaHoaDon.Text = "";
            } 
        }

      


        private void btnThongKe_Click(object sender, EventArgs e)
        {
            this.year = (this.cbbYear.Text == "") ? 0 : int.Parse(this.cbbYear.Text);
            this.month = (this.cbbMonth.Text == "") ? 0 : int.Parse(this.cbbMonth.Text);
            this.day = (this.cbbDay.Text == "") ? 0 : int.Parse(this.cbbDay.Text);
            int tongTien = db.ThongKeHoaDon(year, month, day);
            FormDoanhSo form = new FormDoanhSo(tongTien);
           form.ShowDialog();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dgvHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count < 1)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần sửa.");
            }
            else {
                btnLuu.Enabled = true;
                int r = this.dgvHoaDon.CurrentCell.RowIndex;
                string maHD = this.dgvHoaDon.Rows[r].Cells[0].Value.ToString();
                int gia = int.Parse(this.dgvHoaDon.Rows[r].Cells["TongTien"].Value.ToString());
                string trangThai = (this.dgvHoaDon.Rows[r].Cells["TrangThai"].Value.ToString());
                txtGia.Text = gia.ToString();
               txtTrangThai.Text = trangThai;

            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int r = this.dgvHoaDon.CurrentCell.RowIndex;
            string maHD = this.dgvHoaDon.Rows[r].Cells[0].Value.ToString();
            string sql = $"Update hoadon set TrangThai = '{txtTrangThai.Text}', TongTien = '{txtGia.Text}' where MaHoaDon = '{maHD}' ";
            int check = db.Sql(sql);
            if (check >= 0) {
                MessageBox.Show("Cập nhật hóa đơn thành công.");
                LoadHoaDon();
            } else
            {
                MessageBox.Show("Cập nhật hóa đơn thất bại.");

            }
            resetHoaDon();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count < 1)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa.");
            }
            else
            {
                int r = this.dgvHoaDon.CurrentCell.RowIndex;
                string maHD = this.dgvHoaDon.Rows[r].Cells[0].Value.ToString();
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa hóa đơn này không?", "Xóa", MessageBoxButtons.YesNo,
              MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    string sql = $"Delete from hoadon where MaHoaDon = '{maHD}'";
                    int check = db.Sql(sql);
                    if (check >= 0)
                    {
                        MessageBox.Show("Xóa thành công.");
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại thử lại sau.");
                    }
                }

            }
        }

        private void txtMaHoaDon_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
