using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI.Model;
using ThuVien;

namespace GUI
{
    public partial class FormNV_TuyenSinh : Form
    {
        private ThongTinLuuTru thongTinLuuTruForm;
        public FormNV_TuyenSinh()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            this.Load += FormNV_TuyenSinh_Load;
            btn_QL_LT_submenu.Click += Btn_QL_LT_submenu_Click;
            btn_DK_luutru.Click += Btn_DK_luutru_Click;
            btn_QL_SinhVien.Click += Btn_QL_SinhVien_Click;
            btn_PhanChiaPhong.Click += Btn_PhanChiaPhong_Click;
            btn_DangXuat.Click += Btn_DangXuat_Click;
            btn_QuanLyPhong.Click += Btn_QuanLyPhong_Click;
        }

        private void Btn_QuanLyPhong_Click(object sender, EventArgs e)
        {
            OpenChildForm(new QL_Phong(this));
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

        private void Btn_PhanChiaPhong_Click(object sender, EventArgs e)
        {
            OpenChildForm(new XepPhong());
        }

        private void Btn_QL_SinhVien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ThongTinSinhVien(this));
            hideSubmenu();
        }
        public void LoadThongTinLuuTruForm()
        {
            // Kiểm tra nếu form đã tồn tại, giải phóng nó trước khi tạo form mới
            if (thongTinLuuTruForm != null)
            {
                thongTinLuuTruForm.Close();
                thongTinLuuTruForm.Dispose();
            }

            // Tạo instance mới của ThongTinLuuTru
            thongTinLuuTruForm = new ThongTinLuuTru();
            thongTinLuuTruForm.TopLevel = false; // Đặt TopLevel = false để hiển thị bên trong panel
            thongTinLuuTruForm.FormBorderStyle = FormBorderStyle.None; // Loại bỏ viền
            thongTinLuuTruForm.Dock = DockStyle.Fill; // Điền đầy trong panel

            // Thêm form vào panelContainer và hiển thị
            panel_Main.Controls.Clear(); // Xóa control cũ nếu có
            panel_Main.Controls.Add(thongTinLuuTruForm);
            thongTinLuuTruForm.Show();
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
        private void Btn_DK_luutru_Click(object sender, EventArgs e)
        {
            OpenChildForm(new DangKyLuuTru(this)); // Truyền 'this' để có thể gọi phương thức OpenChildForm
            hideSubmenu();
        }

        private void Btn_QL_LT_submenu_Click(object sender, EventArgs e)
        {
            showsubmenu(panel_SinhVien_submenu);
        }

        private void FormNV_TuyenSinh_Load(object sender, EventArgs e)
        {
            txt_tendangnhap.Text = GUI.Model.DangNhap.TenDangNhap;
            submenu();
        }

        private void submenu()
        {
           
            panel_SinhVien_submenu.Visible = false;
           
        }
        private void hideSubmenu()
        {
           
            if (panel_SinhVien_submenu.Visible == true)
            {
                panel_SinhVien_submenu.Visible = false;
            }
         
        }

        private void showsubmenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hideSubmenu();
                submenu.Visible = true;
            }
            else
            {
                submenu.Visible = false;
            }

        }

      
    }
}
