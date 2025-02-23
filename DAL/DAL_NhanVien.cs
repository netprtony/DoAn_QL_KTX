using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_NhanVien
    {
        DA_QL_KTXDataContext ktx = new DA_QL_KTXDataContext();
        public List<NhanVien> GetAllMaNhanVien()
        {
            try
            {
                // Lấy danh sách thực thể từ ngữ cảnh dữ liệu
                var danhSachNhanVien = ktx.NhanViens.ToList();

                // Chuyển đổi danh sách thực thể sang danh sách DTO
                return danhSachNhanVien.Select(nv => new NhanVien
                {
                    MaNhanVien = nv.MaNhanVien,
                    HoTen = nv.HoTen,
                    ChucVu = nv.ChucVu
                }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi truy vấn danh sách nhân viên: " + ex.Message);
                return new List<NhanVien>();
            }
        }


        // Hàm lấy thông tin chi tiết nhân viên dựa trên MaTaiKhoan
        public NhanVien GetNhanVienDetailsByMaTaiKhoan(int maTaiKhoan)
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
            return null; // Trả về null nếu không tìm thấy nhân viên
        }
        public NhanVien GetNhanVien_MaNV(string MaNhanVien)
        {
            var nhanVien = ktx.NhanViens.FirstOrDefault(nv => nv.MaNhanVien == MaNhanVien);
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
            return null; // Trả về null nếu không tìm thấy nhân viên
        }


        ////////////////////////////////////////////////////////////


        // Hàm lấy toàn bộ danh sách nhân viên
        public List<NhanVien> GetNhanVien()
        {
            return ktx.NhanViens.ToList();
        }

        public List<TaiKhoan> GetTaiKhoan()
        {
            return ktx.TaiKhoans.ToList();
        }

        public bool AddTaiKhoan(TaiKhoan taiKhoan)
        {
            try
            {
                // Tìm giá trị lớn nhất của MaTaiKhoan hiện tại
                int maxMaTaiKhoan = ktx.TaiKhoans.Any()
                    ? ktx.TaiKhoans.Max(tk => tk.MaTaiKhoan)
                    : 0;

                // Gán MaTaiKhoan mới là giá trị lớn nhất + 1
                taiKhoan.MaTaiKhoan = maxMaTaiKhoan + 1;

                // Thêm tài khoản mới vào cơ sở dữ liệu
                ktx.TaiKhoans.InsertOnSubmit(taiKhoan);
                ktx.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message); // Log lỗi nếu có
                return false;
            }
        }

        public TaiKhoan GetTaiKhoanById(string maTaiKhoan)
        {
            return ktx.TaiKhoans.FirstOrDefault(tk => tk.MaTaiKhoan.ToString() == maTaiKhoan);
        }

        // Hàm lấy thông tin chi tiết nhân viên dựa trên MaTaiKhoan

        public bool AddNhanVien(NhanVien newNhanVien)
        {
            try
            {
                // Thêm nhân viên vào bảng NhanViens
                ktx.NhanViens.InsertOnSubmit(newNhanVien);

                // Lưu thay đổi vào cơ sở dữ liệu
                ktx.SubmitChanges();

                return true; // Thêm thành công
            }
            catch (Exception ex)
            {
                // Log lỗi chi tiết

                Console.WriteLine("Chi tiết lỗi: " + ex.StackTrace);
                return false;
            }
        }

        public bool XoaNhanVien(string maNV)
        {
            try
            {
                // Tìm nhân viên theo MaNV
                NhanVien nhanVien = ktx.NhanViens.SingleOrDefault(nv => nv.MaNhanVien == maNV);

                if (nhanVien != null)
                {
                    // Xóa nhân viên
                    ktx.NhanViens.DeleteOnSubmit(nhanVien);
                    ktx.SubmitChanges();
                    return true; // Thành công
                }
                else
                {
                    return false; // Không tìm thấy nhân viên
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine(ex.Message);
                return false;
            }
        }


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
            nhanVien.MaTaiKhoan = nhanVienDto.MaTaiKhoan;

            // Lưu thay đổi vào cơ sở dữ liệu
            ktx.SubmitChanges();
            return true;
        }

        // Phương thức xóa tài khoản từ cơ sở dữ liệu
        public bool XoaTaiKhoan(int maTaiKhoan)
        {
            try
            {
                // Tìm tài khoản trong cơ sở dữ liệu
                var taiKhoan = ktx.TaiKhoans.SingleOrDefault(tk => tk.MaTaiKhoan == maTaiKhoan);

                if (taiKhoan != null)
                {
                    // Xóa tài khoản
                    ktx.TaiKhoans.DeleteOnSubmit(taiKhoan);
                    ktx.SubmitChanges(); // Cập nhật cơ sở dữ liệu
                    return true;
                }
                else
                {
                    return false; // Không tìm thấy tài khoản
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa tài khoản: " + ex.Message);
                return false; // Xử lý lỗi
            }
        }

        // Phương thức cập nhật tài khoản
        public bool UpdateTaiKhoan(TaiKhoan taiKhoanDTO)
        {           
            var taikhoan = ktx.TaiKhoans.FirstOrDefault(nv => nv.MaTaiKhoan == taiKhoanDTO.MaTaiKhoan);
            if (taikhoan == null)
            {
                return false;
            }
       
            taikhoan.TenDangNhap = taiKhoanDTO.TenDangNhap;
            taikhoan.MatKhau = taiKhoanDTO.MatKhau;
            taikhoan.VaiTro = taiKhoanDTO.VaiTro;
            taikhoan.MaHoaDuLieu = taiKhoanDTO.MaHoaDuLieu;



            // Lưu thay đổi vào cơ sở dữ liệu
            ktx.SubmitChanges();
            return true;
        }
    }

}
