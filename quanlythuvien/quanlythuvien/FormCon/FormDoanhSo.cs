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
    public partial class FormDoanhSo : Form
    {
        private int gia;

            public FormDoanhSo(int gia)
            {
                InitializeComponent();
                   this.gia = gia;
            lb_Tien.Text = gia.ToString() + "VND";
            }

            private void btn_QuayLai_Click(object sender, EventArgs e)
            {
                this.Close();
            }

           
        }
}
