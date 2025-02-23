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
    public partial class From_NV_KTcs : Form
    {
        public From_NV_KTcs()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            btn_DangXuat.Click += Btn_DangXuat_Click;
            btn_QuanLyHoaDon.Click += Btn_QuanLyHoaDon_Click;
            btn_ql_thu.Click += Btn_ql_thu_Click;
            btn_ThongKeChi.Click += Btn_ThongKeChi_Click;
            btn_baocao.Click += Btn_baocao_Click;
        }

        private void Btn_baocao_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BaoCao());
        }

        private void Btn_ThongKeChi_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ThongKeDoanhChi());
        }

        private void Btn_ql_thu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ThongKeThu());
        }

        private void Btn_QuanLyHoaDon_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form_KT_QL_HD());
        }

        private void Btn_DangXuat_Click(object sender, EventArgs e)
        {
            // Hiển thị thông báo xác nhận đăng xuất
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Mở form Login
                Login loginForm = new Login(); // Tạo đối tượng form Login
                loginForm.Show();

                // Đóng form hiện tại
                this.Close();
            }
        }

        private Form currentFormChild;

        public void OpenChildForm(Form child)
        {
            if (child == null)
            {
                MessageBox.Show("Form con không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Đóng form con hiện tại nếu có
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }

            // Lưu tham chiếu form con
            currentFormChild = child;
            child.TopLevel = false;
            child.Dock = DockStyle.Fill;
            child.FormBorderStyle = FormBorderStyle.None;
            //     child.AutoScroll = true;
            panel_Main.Controls.Add(child);  // Thêm form vào panel chứa
            panel_Main.Tag = child;
            child.BringToFront();
            child.Show();  // Hiển thị form con
        }


    }
}
