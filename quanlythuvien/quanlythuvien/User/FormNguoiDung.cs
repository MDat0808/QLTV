using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlythuvien.User
{
    public partial class FormNguoiDung : Form
    {
        private int userId;
        public FormNguoiDung(int id)
        {
            InitializeComponent();
            this.userId = id;
        }

        void MoFormCon(Form f)
        {
            foreach (Form child in this.MdiChildren)
            {
                if (child.Name == f.Name)
                {
                    child.Activate();
                    return;
                }
            }
            f.MdiParent = this;
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MoFormCon(new FormSach(userId));

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // MoFormCon(new ChuShop.frmDanhMucMatHang());
        }

        private void frmChuShop_Load(object sender, EventArgs e)
        {
        }

        private void frmChuShop_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }



        private void button4_Click(object sender, EventArgs e)
        {
            MoFormCon(new FormTaiKhoan(userId));

        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormDangNhap formDangNhap = new FormDangNhap();
            formDangNhap.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            MoFormCon(new FormQuanLyHoaDon());

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
