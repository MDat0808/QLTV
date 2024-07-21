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
    public partial class FormThanToanHD : Form
    {
        private Sach db = new Sach();
        private int money = 0;
        private int userId ;
        private int maHD;
        public FormThanToanHD(int userId ,int money, int maHD)
        {
            InitializeComponent();
            this.money = money;
            this.userId = userId;
            this.maHD = maHD;
        }

        private void FormHoaDon_Load(object sender, EventArgs e)
        {
            this.lb_TongTien.Text = this.money.ToString();
        }

      

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btn_ThanhToan_Click(object sender, EventArgs e)
        {
            int check = db.thanhToan(userId, maHD, money);
            if (check >= 0) {
                MessageBox.Show("Thanh toán thành công.");
                this.Close();
            } else
            {
                MessageBox.Show("Thanh toán thất bại");
            }
        }
    }
}
