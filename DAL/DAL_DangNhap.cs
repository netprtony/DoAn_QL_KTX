using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_DangNhap
    {
        DA_QL_KTXDataContext ktx = new DA_QL_KTXDataContext();


        public string GetChucVuByMaTaiKhoan(int maTaiKhoan)
        {
            // Tìm nhân viên dựa trên mã tài khoản
            var nhanVien = ktx.NhanViens.FirstOrDefault(nv => nv.MaTaiKhoan == maTaiKhoan);

            // Nếu tìm thấy, trả về ChucVu
            if (nhanVien != null)
            {
                return nhanVien.ChucVu;
            }

            // Nếu không tìm thấy, trả về null hoặc thông báo lỗi
            return null; // Hoặc "Chức vụ không tồn tại"
        }


        public string GetVaiTroByMaTaiKhoan(int maTaiKhoan)
        {
            // Tìm tài khoản dựa trên mã tài khoản
            var taiKhoan = ktx.TaiKhoans.FirstOrDefault(tk => tk.MaTaiKhoan == maTaiKhoan);

            // Nếu tìm thấy, trả về VaiTro
            if (taiKhoan != null)
            {
                return taiKhoan.VaiTro;
            }

            // Nếu không tìm thấy, trả về null hoặc thông báo lỗi
            return null; // Hoặc "Vai trò không tồn tại"
        }

        // Hàm lấy thông tin tài khoản dựa trên TenDangNhap và MatKhau
        public TaiKhoan GetTaiKhoan_Login(string tenDangNhap, string matKhau)
        {
            // Tìm tài khoản dựa trên tên đăng nhập và mật khẩu
            var taiKhoan = ktx.TaiKhoans.FirstOrDefault(tk => tk.TenDangNhap == tenDangNhap && tk.MaHoaDuLieu == matKhau);

            // Nếu tìm thấy tài khoản, trả về đối tượng TaiKhoan
            if (taiKhoan != null)
            {
                return new TaiKhoan
                {
                    MaTaiKhoan = taiKhoan.MaTaiKhoan,
                    TenDangNhap = taiKhoan.TenDangNhap,
                    MatKhau = taiKhoan.MatKhau,
                    VaiTro = taiKhoan.VaiTro,
                    MaHoaDuLieu = taiKhoan.MaHoaDuLieu
                };
            }

            // Nếu không tìm thấy, trả về null
            return null;
        }
        // Hàm lấy thông tin nhân viên dựa trên MaTaiKhoan
        public NhanVien GetNhanVienByMaTaiKhoan(int maTaiKhoan)
        {
            var nhanVien = ktx.NhanViens.FirstOrDefault(nv => nv.MaTaiKhoan == maTaiKhoan);
            if (nhanVien != null)
            {
                return new NhanVien
                {
                    MaNhanVien = nhanVien.MaNhanVien,
                    HoTen = nhanVien.HoTen,
                    ChucVu = nhanVien.ChucVu,
                    DiaChiLienLac = nhanVien.DiaChiLienLac,
                    DienThoai = nhanVien.DienThoai,
                    Email = nhanVien.Email,
                    NgaySinh = nhanVien.NgaySinh,
                    GioiTinh = nhanVien.GioiTinh,
                    Hinhanh = nhanVien.Hinhanh,
                    MaTaiKhoan = nhanVien.MaTaiKhoan
                };
            }
            return null;
        }
        // Hàm lấy thông tin tài khoản dựa trên MaTaiKhoan
        public TaiKhoan GetTaiKhoanByMaTaiKhoan(int maTaiKhoan)
        {
            // Tìm tài khoản dựa trên MaTaiKhoan
            var taiKhoan = ktx.TaiKhoans.FirstOrDefault(tk => tk.MaTaiKhoan == maTaiKhoan);

            // Nếu tìm thấy tài khoản, trả về đối tượng TaiKhoan
            if (taiKhoan != null)
            {
                return new TaiKhoan
                {
                    MaTaiKhoan = taiKhoan.MaTaiKhoan,
                    TenDangNhap = taiKhoan.TenDangNhap,
                    MatKhau = taiKhoan.MatKhau,
                    VaiTro = taiKhoan.VaiTro,
                    MaHoaDuLieu = taiKhoan.MaHoaDuLieu
                };
            }

            // Nếu không tìm thấy, trả về null
            return null;
        }
        // Hàm lấy mật khẩu dựa trên mã tài khoản
        public string GetPasswordByMaTaiKhoan(int maTaiKhoan)
        {
            // Tìm tài khoản dựa trên MaTaiKhoan
            var taiKhoan = ktx.TaiKhoans.FirstOrDefault(tk => tk.MaTaiKhoan == maTaiKhoan);

            // Nếu tìm thấy tài khoản, trả về mật khẩu
            if (taiKhoan != null)
            {
                return taiKhoan.MatKhau; // Trả về mật khẩu
            }

            // Nếu không tìm thấy, trả về null
            return null;
        }

        //đổi mật khẩu
        public bool ChangePassword(int maTaiKhoan,string MHPassword, string newPassword)
        {
            // Tìm tài khoản dựa trên MaTaiKhoan
            var taiKhoan = ktx.TaiKhoans.FirstOrDefault(tk => tk.MaTaiKhoan == maTaiKhoan);

            // Kiểm tra mật khẩu hiện tại
            if (taiKhoan != null  )
            {
                taiKhoan.MaHoaDuLieu = MHPassword; // Cập nhật mật khẩu mới (cần mã hóa trước khi lưu)
                taiKhoan.MatKhau = newPassword;
                // Cập nhật dữ liệu vào cơ sở dữ liệu
                ktx.SubmitChanges();
                return true; // Đổi mật khẩu thành công
            }

            return false; // Không thành công
        }
        // Hàm cập nhật thông tin nhân viên dựa vào đối tượng NhanVien
        public bool UpdateNhanVien(NhanVien nhanVienDto)
        {
            // Tìm nhân viên theo mã nhân viên
            var nhanVien = ktx.NhanViens.FirstOrDefault(nv => nv.MaNhanVien == nhanVienDto.MaNhanVien);

            // Kiểm tra nếu nhân viên không tồn tại, trả về false
            if (nhanVien == null)
            {
                return false;
            }

            // Cập nhật thông tin nhân viên dựa trên các thuộc tính của đối tượng NhanVien
            nhanVien.HoTen = nhanVienDto.HoTen;
            nhanVien.ChucVu = nhanVienDto.ChucVu;
            nhanVien.Hinhanh = nhanVienDto.Hinhanh;
            nhanVien.NgaySinh = nhanVienDto.NgaySinh;
            nhanVien.DiaChiLienLac = nhanVienDto.DiaChiLienLac;
            nhanVien.GioiTinh = nhanVienDto.GioiTinh;
            nhanVien.DienThoai = nhanVienDto.DienThoai;
            nhanVien.Email = nhanVienDto.Email;

            // Lưu thay đổi vào cơ sở dữ liệu
            ktx.SubmitChanges();
            return true;
        }


    }
}
