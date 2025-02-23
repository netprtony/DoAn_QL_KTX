using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class From_TrangChu : Form
    {
        public From_TrangChu()
        {
            InitializeComponent();
            btn_DangNhap.Click += Btn_DangNhap_Click;
            // Đặt form ở chế độ toàn màn hình
            this.WindowState = FormWindowState.Maximized; // Mở rộng toàn màn hình
          //  this.FormBorderStyle = FormBorderStyle.None; // Loại bỏ viền và thanh tiêu đề

        }

        private void Btn_DangNhap_Click(object sender, EventArgs e)
        {
            Login f = new Login();
            this.Hide();
            f.ShowDialog();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
