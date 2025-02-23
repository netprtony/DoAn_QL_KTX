using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_DangNhap
    {
        private DAL_DangNhap dalDangNhap = new DAL_DangNhap();
        public BLL_DangNhap()
        {
        }
        public string GetChucVuByMaTaiKhoan(int maTaiKhoan)
        {
            // Gọi hàm trong lớp DAL_DangNhap
            return dalDangNhap.GetChucVuByMaTaiKhoan(maTaiKhoan);
        }

        // Hàm gọi từ DAL để lấy vai trò theo mã tài khoản
        public string GetVaiTroByMaTaiKhoan(int maTaiKhoan)
        {
            // Gọi hàm trong lớp DAL_DangNhap
            return dalDangNhap.GetVaiTroByMaTaiKhoan(maTaiKhoan);
        }
        public TaiKhoan GetTaiKhoan_Login(string tenDangNhap, string matKhau)
        {
            // Gọi phương thức GetTaiKhoan_Login từ DAL và trả về kết quả
            return dalDangNhap.GetTaiKhoan_Login(tenDangNhap, matKhau);
        }
        // Thêm phương thức lấy thông tin nhân viên dựa trên MaTaiKhoan
        public NhanVien GetNhanVienByMaTaiKhoan(int maTaiKhoan)
        {
            // Gọi phương thức GetNhanVienByMaTaiKhoan từ DAL và trả về kết quả
            return dalDangNhap.GetNhanVienByMaTaiKhoan(maTaiKhoan);
        }
        public TaiKhoan GetTaiKhoanByMaTaiKhoan(int maTaiKhoan)
        {
            return dalDangNhap.GetTaiKhoanByMaTaiKhoan(maTaiKhoan);
        }

        public bool ChangePassword(int maTaiKhoan, string MHPassword, string newPassword)
        {
            // Gọi phương thức ChangePassword từ DAL và trả về kết quả
            return dalDangNhap.ChangePassword(maTaiKhoan, MHPassword, newPassword);
        }

        // Thêm phương thức lấy mật khẩu dựa trên MaTaiKhoan
        public string GetPasswordByMaTaiKhoan(int maTaiKhoan)
        {
            // Gọi phương thức GetPasswordByMaTaiKhoan từ DAL và trả về kết quả
            return dalDangNhap.GetPasswordByMaTaiKhoan(maTaiKhoan);
        }

        // Phương thức cập nhật thông tin nhân viên
        public bool UpdateNhanVien(NhanVien nhanVienDto)
        {
            return dalDangNhap.UpdateNhanVien(nhanVienDto);
        }
    }
}
