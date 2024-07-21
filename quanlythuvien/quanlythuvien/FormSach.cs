using MySql.Data.MySqlClient;
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
 
    public partial class FormSach : Form
    {
        private int userId ;
       private Sach db = new Sach();


        public FormSach(int userID)
        {
            InitializeComponent();
            this.userId = userID;
        }

        private void FormSach_Load(object sender, EventArgs e)
        {
           this.dgvBangSach.DataSource = db.getBooks();
        }

        private void LoadForm()
        {
            this.dgvBangSach.DataSource = db.getBooks();
            this.txtTenSach.Text = String.Empty;
            this.txtTenTacGia.Text = String.Empty;

        }

        private void ResetVariable()
        {
           txtTenSach.Text = String.Empty;
            txtTenTacGia.Text= String.Empty;
           
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadForm();
        }

     

        private void btnSachMoi_Click(object sender, EventArgs e)
        {
            dgvBangSach.DataSource = db.getTop10Books();
        }

   

        private void btnChiTietSach_Click(object sender, EventArgs e)
        {
            int r = this.dgvBangSach.CurrentCell.RowIndex;
            string masach = this.dgvBangSach.Rows[r].Cells[0].Value.ToString();
            FormChiTietSach form = new FormChiTietSach(userId,int.Parse (masach));
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtTenSach.Text) && String.IsNullOrEmpty(txtTenTacGia.Text)) 
            {
                MessageBox.Show("Vui lòng nhập thông tin tìm kiếm.");
                return;
            }
            this.dgvBangSach.DataSource = db.findBooks(txtTenSach.Text, txtTenTacGia.Text);
            ResetVariable();
        }

     

       
    }
}
