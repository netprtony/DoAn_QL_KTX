using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Control;
using GUI.Model;

namespace GUI
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            this.Load += FormMain_Load;
         //   btn_ql_thu.Click += Btn_ql_thu_Click;
            btn_ql_phichi.Click += Btn_ql_phichi_Click;
            btn_ql_nhanvien.Click += Btn_ql_nhanvien_Click;
            btn_QL_ThongBao.Click += Btn_QL_ThongBao_Click;
            btn_DangXuat.Click += Btn_DangXuat_Click;
            btn_QuanLySinhVien.Click += Btn_QuanLySinhVien_Click;
            btn_BaoCao.Click += Btn_BaoCao_Click;
        }

        private void Btn_BaoCao_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BaoCao());
        }

        private void Btn_QuanLySinhVien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ThongTinSinhVien());
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

        private void Btn_QL_ThongBao_Click(object sender, EventArgs e)
        {
            OpenChildForm(new QuanLyThongBao());
        }

        private void Btn_ql_nhanvien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new QuanLyNhanVien());
        }

        private void Btn_ql_phichi_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ThongKeDoanhChi());
        }

        //private void Btn_ql_thu_Click(object sender, EventArgs e)
        //{
        //    OpenChildForm(new ThongKeDoanhChi());
        //}

        private void FormMain_Load(object sender, EventArgs e)
        {
            txt_tendangnhap.Text = DangNhap.TenDangNhap;
            submenu();

        }

        private void submenu()
        {
            panel_Phong_SubMenu.Visible = false;
            panel_SinhVien_submenu.Visible = false;
            panel_DichVu_submenu.Visible = false;
            panel_NoiQuy_SubMenu.Visible = false;
            panel_ql_TaiChinh.Visible = false;
        }
        private void hideSubmenu()
        {
            if (panel_Phong_SubMenu.Visible == true)
            {
                panel_Phong_SubMenu.Visible = false;
            }
            if (panel_SinhVien_submenu.Visible == true)
            {
                panel_SinhVien_submenu.Visible = false;
            }
            if(panel_DichVu_submenu.Visible==true)
            {
                panel_DichVu_submenu.Visible=false;
            }
            if (panel_NoiQuy_SubMenu.Visible == true)
            {
                panel_NoiQuy_SubMenu.Visible=false;
            }  
            if(panel_ql_TaiChinh.Visible == true)
            {
                panel_ql_TaiChinh.Visible =false;
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
        //phong
        private void button1_Click(object sender, EventArgs e)
        {
            showsubmenu(panel_Phong_SubMenu);
        }

        private void btn_quanlyphong_Click(object sender, EventArgs e)
        {
           // quanLyPhong1.Visible = true;
           // yeuCauSuaChua1.Visible = false;
          //  dangKyLuuTru1.Visible = false;
            //hideSubmenu();
            //OpenChildForm(new QL_Phong(this));
        }

        private void btn_SinhVien_submenu_Click(object sender, EventArgs e)
        {
            showsubmenu(panel_SinhVien_submenu);
        }

        private void btn_yeucausuachua_Click(object sender, EventArgs e)
        {
            //  quanLyPhong1.Visible = false;
            //  yeuCauSuaChua1.Visible = true;
            // dangKyLuuTru1.Visible = false;
            // hideSubmenu();
            OpenChildForm(new QL_YeuCauSuaChua());
        }
        
        private void btn_DK_luutru_Click(object sender, EventArgs e)
        {
           // dangKyLuuTru1.Visible = true;
           // quanLyPhong1.Visible = false;
           // yeuCauSuaChua1.Visible = false;
            hideSubmenu();
        }

        private void btn_DichVu_Click(object sender, EventArgs e)
        {

            showsubmenu(panel_DichVu_submenu);
        }

        private void btn_NoiQuy_submenu_Click(object sender, EventArgs e)
        {
            showsubmenu(panel_NoiQuy_SubMenu);
        }

        private void btn_NoiQuy_Click(object sender, EventArgs e)
        {
            OpenChildForm(new QuanLyNoiQuy()); 
           // quanLyPhong1.Visible = false;
            //yeuCauSuaChua1.Visible = false;
           // dangKyLuuTru1.Visible = false;


        }
        private Form currentFormChild;
        public void OpenChildForm(Form child)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();

            }
            currentFormChild = child;
            child.TopLevel = false;
            child.Dock = DockStyle.Fill;
            child.FormBorderStyle = FormBorderStyle.None;
            panel_Main.Controls.Add(child);
            panel_Main.Tag = child;
            child.BringToFront();
            child.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new QuanLyDichVu());
        }

        private void giớiThiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ThongTinCaNhan());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            showsubmenu(panel_ql_TaiChinh);
        }

        private void btn_ql_thu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ThanhToanPhong());
        }

        private void btn_QuanLyTTB_Click(object sender, EventArgs e)
        {
            OpenChildForm(new QuanLyTrangThietBi());
           
        }

        private void btn_QuanLyThe_Click(object sender, EventArgs e)
        {
            OpenChildForm(new LapThe());
        }

        private void txt_tendangnhap_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
