using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using DTO;
using System.Text.RegularExpressions;

namespace DAL
{
    public class DAL_QL_SinhVien
    {
        DA_QL_KTXDataContext ktx = new DA_QL_KTXDataContext();

        public DAL_QL_SinhVien() { }    

        public List<SinhVien> GetSinhViens()
        {
            return ktx.SinhViens.ToList();
        }

        public SinhVien GetByMa(string maSV)
        {
            return ktx.SinhViens.FirstOrDefault(sv => sv.MaSinhVien == maSV);
        }


        public bool ThemSinhVien(SinhVien sinhVien)
        {
            try
            {
                // Kiểm tra dữ liệu
                if (string.IsNullOrEmpty(sinhVien.MaSinhVien))
                {
                    throw new Exception("Mã sinh viên không được để trống.");
                }

                if (string.IsNullOrEmpty(sinhVien.HoTen))
                {
                    throw new Exception("Họ tên sinh viên không được để trống.");
                }

                if (sinhVien.NgaySinh == default(DateTime))
                {
                    throw new Exception("Ngày sinh không hợp lệ.");
                }

                // Loại bỏ ký tự đặc biệt trực tiếp trong CCCD
                // Loại bỏ ký tự đặc biệt trong CCCD
                // Thêm log kiểm tra CCCD
               


                // Kiểm tra tuổi
                DateTime today = DateTime.Today;
                int age = today.Year - sinhVien.NgaySinh.Year;
                if (sinhVien.NgaySinh.Date > today.AddYears(-age)) age--; // Điều chỉnh tuổi nếu sinh nhật chưa đến trong năm nay

                if (age < 18)
                {
                    throw new Exception("Sinh viên phải từ 18 tuổi trở lên.");
                }

                // In thông tin kiểm tra
            //    Console.WriteLine($"Mã: {sinhVien.MaSinhVien}, Tên: {sinhVien.HoTen}, Ngày sinh: {sinhVien.NgaySinh:dd/MM/yyyy}, Tuổi: {age}");

                var existingStudent = ktx.SinhViens.FirstOrDefault(s => s.MaSinhVien == sinhVien.MaSinhVien);
                if (existingStudent != null)
                {
                    throw new Exception($"Sinh viên với mã {sinhVien.MaSinhVien} đã tồn tại.");
                }

                // Thêm sinh viên
                ktx.SinhViens.InsertOnSubmit(sinhVien);
                ktx.SubmitChanges();
                Console.WriteLine($"Thêm thành công sinh viên: {sinhVien.MaSinhVien}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm sinh viên: " + ex.ToString());
                return false;
            }
        }



        public bool CapNhatSinhVien(SinhVien sinhVien)
        {
            try
            {
                // Tìm sinh viên trong cơ sở dữ liệu dựa vào mã sinh viên
                var sv = ktx.SinhViens.SingleOrDefault(sv1 => sv1.MaSinhVien == sinhVien.MaSinhVien);

                if (sv != null)
                {
                    // Cập nhật thông tin sinh viên
                    sv.HoTen = sinhVien.HoTen;
                    sv.CCCD = sinhVien.CCCD;
                    sv.Email = sinhVien.Email;
                    sv.SDT = sinhVien.SDT;
                    sv.NgaySinh = sinhVien.NgaySinh;
                    sv.GioiTinh = sinhVien.GioiTinh;
                    sv.HoKhauThuongTru = sinhVien.HoKhauThuongTru;
                    sv.NoiSinh = sinhVien.NoiSinh;
                    sv.GhiChu = sinhVien.GhiChu;
                    sv.TruongPhong = sinhVien.TruongPhong;
                    sv.HinhCCCDTruoc = sinhVien.HinhCCCDTruoc;
                    sv.HinhCCCDSau = sinhVien.HinhCCCDSau;
                    sv.HinhNhanDien = sinhVien.HinhNhanDien;
                    ktx.SubmitChanges();
                    return true; // Cập nhật thành công
                }
                return false; // Không tìm thấy sinh viên
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật sinh viên: " + ex.Message);
                return false; // Cập nhật thất bại
            }
        }

        public bool DeleteSinhVien(string maSoSinhVien)
        {
            try
            {
                // Tìm sinh viên cần xóa
                var sinhVien = ktx.SinhViens.SingleOrDefault(sv => sv.MaSinhVien == maSoSinhVien);

                if (sinhVien != null)
                {
                    ktx.SinhViens.DeleteOnSubmit(sinhVien);
                    ktx.SubmitChanges();
                    return true; // Xóa thành công
                }
                return false; // Không tìm thấy sinh viên
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa sinh viên: " + ex.Message);
                return false; // Xóa thất bại
            }
        }

        public List<SinhVien> GetSinhViensDaDangKyPhong()
        {
           
                // Truy vấn lấy sinh viên đã đăng ký phòng
                var sinhVienDaDangKy = (from sv in ktx.SinhViens
                                        join dk in ktx.DangKyPhongs on sv.MaSinhVien equals dk.MaSinhVien
                                        select sv).ToList();

                return sinhVienDaDangKy;
            
        }


        //Xếp phòng

        public List<SinhVien> GetSinhVienChuaDangKyPhong()
        {
            try
            {
                // Thực hiện truy vấn để lấy danh sách sinh viên chưa có phiếu đăng ký phòng
                var sinhViens = (from sv in ktx.SinhViens
                                 join pdk in ktx.DangKyPhongs
                                 on sv.MaSinhVien equals pdk.MaSinhVien into leftJoin
                                 from pdkLeft in leftJoin.DefaultIfEmpty()
                                 where pdkLeft == null // Kiểm tra sinh viên chưa có phiếu đăng ký
                                 select sv).ToList();

                return sinhViens;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách sinh viên chưa đăng ký phòng: " + ex.Message);
                return new List<SinhVien>();
            }
        }
       
     

        // Lấy nguyện vọng của sinh viên từ bảng SinhVien
        public List<SinhVien> GetNguyenVong()
        {
            return ktx.SinhViens.ToList();
        }

        // Thêm phiếu đăng ký phòng cho sinh viên
        public bool ThemDangKyPhong(string maSV, string maPhong)
        {
            var dangKyPhong = new DangKyPhong
            {
                MaSinhVien = maSV,
                MaPhong = maPhong
            };
            try
            {
                ktx.DangKyPhongs.InsertOnSubmit(dangKyPhong);
                ktx.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm phiếu đăng ký phòng: " + ex.Message);
                return false;
            }
        }
    

}
}
