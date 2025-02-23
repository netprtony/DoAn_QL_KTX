using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_BaoCao
    {
        DA_QL_KTXDataContext ktx = new DA_QL_KTXDataContext();
        public DAL_BaoCao()
        {

        }
        // Phương thức lấy tỷ lệ sử dụng nước theo tầng
        public List<TyLeSuDungNuocTheoTang> GetThongKeTiLeSuDungNuocTheoNam(int nam)
        {
            // Gọi thủ tục để lấy dữ liệu thống kê tỷ lệ sử dụng nước theo tầng
            var result = ktx.ExecuteQuery<TyLeSuDungNuocTheoTang>(
                "EXEC ThongKeTiLeSuDungNuoc @Nam = {0}", nam).ToList();

            return result;
        }
        // Phương thức lấy tỷ lệ sinh viên theo năm
        public List<TyLeSinhVienOTang> GetThongKeTyLeSinhVienTheoNam(int nam)
        {
            // Gọi thủ tục để lấy dữ liệu thống kê tỷ lệ sinh viên theo năm
            var result = ktx.ExecuteQuery<TyLeSinhVienOTang>(
                "EXEC ThongKeTyLeSinhVienTheoNam @Nam = {0}", nam).ToList();

            return result;
        }

        // Phương thức lấy tỷ lệ sử dụng điện theo năm
        public List<TyLeSuDungDienTheoTang> GetThongKeTiLeSuDungDien(int nam)
        {
            // Gọi thủ tục để lấy dữ liệu thống kê tỷ lệ sử dụng điện theo tầng và năm
            var result = ktx.ExecuteQuery<TyLeSuDungDienTheoTang>(
                "EXEC ThongKeTiLeSuDungDien @Nam = {0}", nam).ToList();

            return result;
        }

        // Hàm lấy danh sách sinh viên xét lưu trú Ký túc xá năm 2024 - 2025
        public List<SinhVienXetLuuTru> GetDanhSachSinhVienXetLuuTru(int namHoc)
        {
            // Tính toán năm học bắt đầu và kết thúc
            DateTime ngayBatDauNamHoc = new DateTime(namHoc, 9, 1); // Giả sử năm học bắt đầu vào tháng 9
            DateTime ngayKetThucNamHoc = new DateTime(namHoc + 1, 8, 31); // Và kết thúc vào tháng 8 năm sau

            // Truy vấn danh sách sinh viên đã đăng ký phòng trong năm học 2024-2025
            var result = from dangKy in ktx.DangKyPhongs
                         join phong in ktx.Phongs on dangKy.MaPhong equals phong.MaPhong
                         join sinhVien in ktx.SinhViens on dangKy.MaSinhVien equals sinhVien.MaSinhVien
                         where dangKy.NgayDK >= ngayBatDauNamHoc && dangKy.NgayDK <= ngayKetThucNamHoc
                         && dangKy.TrangThai == "Đang Ở" // Trạng thái là đã duyệt
                         select new SinhVienXetLuuTru
                         {
                            // MaDangKyPhong = dangKy.MaDangKyPhong,
                             MaSinhVien = sinhVien.MaSinhVien,
                             TenSinhVien = sinhVien.HoTen,
                             NgaySinh = sinhVien.NgaySinh,
                             NgayDK = dangKy.NgayDK,
                             MaPhong = phong.MaPhong,
                             Tang = phong.Tang,
                            
                         };

            return result.ToList();
        }

    }
}
