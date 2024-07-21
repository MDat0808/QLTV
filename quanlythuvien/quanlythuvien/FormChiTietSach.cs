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
    public partial class FormChiTietSach : Form
    {
        private Sach db = new Sach();
        private int userId;
        private int r;
        private int flag;
        private int MaSach;
           

        public FormChiTietSach(int id, int maSach)
        {
            InitializeComponent();
            MaSach = maSach;
            this.userId = id;
        }

        private void FormChiTietSach_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
           DataTable dt = db.getBookById( MaSach);
           lb_MaSach.Text = MaSach.ToString();
            lb_TenSach.Text = dt.Rows[0]["TenSach"].ToString();
            lb_TenTacGia.Text = dt.Rows[0]["TacGia"].ToString();
            lb_NamXuatBan.Text = dt.Rows[0]["NamXuatBan"].ToString();
            lb_NXB.Text = dt.Rows[0]["NhaXuatBan"].ToString();
            lb_NgayNhap.Text = dt.Rows[0]["NgayNhap"].ToString();
            lb_Gia.Text = dt.Rows[0]["Gia"].ToString();
        }

      
     

     

        private void btn_QuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btn_Thue_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc muốn thuê sách này không?", "Thuê", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                int check = db.muonSach(userId, MaSach, int.Parse(lb_Gia.Text));
                if (check >= 0) {
                    MessageBox.Show("Thuê thành công.");
                } else
                {
                    MessageBox.Show("Thuê thất bại thử lại sau.");
                }
            }
        }
    }
}
