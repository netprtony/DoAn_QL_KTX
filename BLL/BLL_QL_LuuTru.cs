using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
namespace BLL
{
    public class BLL_QL_LuuTru
    {
        DAL_QL_LuuTru xuly =new DAL_QL_LuuTru();
        DAL_QL_Phong dal_Phong =new DAL_QL_Phong();
        public BLL_QL_LuuTru() { }

        public List<DangKyPhong> GetDangKyPhong()
        {
            return xuly.GetDKPhong();
         
        }
        public List<DangKyPhong> GetDangKyPhongTheoMaPhong(string maPhong)
        {
            // Lấy danh sách các bảng liên quan
            var danhSachDangKyPhong = xuly.GetAllDangKyPhong();
            var danhSachSinhVien = xuly.GetSinhVien();
            var danhSachPhong = dal_Phong.GetPhong();
            var danhSachLoaiPhong = xuly.GetLoaiPhongs();

            // Kết hợp thông tin với điều kiện lọc theo mã sinh viên
            var danhSachDangKyPhongTheoMaSV = from dk in danhSachDangKyPhong
                                              join sv in danhSachSinhVien on dk.MaSinhVien equals sv.MaSinhVien
                                              join p in danhSachPhong on dk.MaPhong equals p.MaPhong
                                              join lp in danhSachLoaiPhong on p.MaLoaiPhong equals lp.MaLoaiPhong
                                              where p.MaPhong == maPhong  // Điều kiện lọc theo mã sinh viên
                                              select new DangKyPhong
                                              {
                                                  MaDangKyPhong = dk.MaDangKyPhong,
                                                  MaPhong = dk.MaPhong,
                                                  MaSinhVien = dk.MaSinhVien,
                                                  NgayDK = dk.NgayDK,
                                                  NgayBD = dk.NgayBD,
                                                  NgayKT = dk.NgayKT,
                                               
                                                  //Tang = dk.Tang,
                                                  HoTen = sv.HoTen,
                                                  CCCD = sv.CCCD,
                                                  Email = sv.Email,
                                                  HinhThucThanhToan = dk.HinhThucThanhToan,
                                                  SDT = sv.SDT,
                                                  LoaiPhong_ = lp.TenLoaiPhong,
                                                  DonGiaPhong = lp.DonGiaPhong.GetValueOrDefault()
                                              };

            // Trả về danh sách kết quả
            return danhSachDangKyPhongTheoMaSV.ToList();
           // return xuly.GetDangKyPhongTheoMaPhong(maPhong);
        }
        // Phương thức lấy danh sách thông tin sinh viên theo mã
        public List<DangKyPhong> GetTheoMaSV(string maSinhVien)
        {
            // Lấy danh sách các bảng liên quan
            var danhSachDangKyPhong = xuly.GetAllDangKyPhong();
            var danhSachSinhVien = xuly.GetSinhVien();
            var danhSachPhong = dal_Phong.GetPhong();
            var danhSachLoaiPhong = xuly.GetLoaiPhongs();

            // Kết hợp thông tin với điều kiện lọc theo mã sinh viên
            var danhSachDangKyPhongTheoMaSV = from dk in danhSachDangKyPhong
                                              join sv in danhSachSinhVien on dk.MaSinhVien equals sv.MaSinhVien
                                              join p in danhSachPhong on dk.MaPhong equals p.MaPhong
                                              join lp in danhSachLoaiPhong on p.MaLoaiPhong equals lp.MaLoaiPhong
                                              where sv.MaSinhVien == maSinhVien  // Điều kiện lọc theo mã sinh viên
                                              select new DangKyPhong
                                              {
                                                  MaDangKyPhong = dk.MaDangKyPhong,
                                                  MaPhong = dk.MaPhong,
                                                  MaSinhVien = dk.MaSinhVien,
                                                  NgayDK = dk.NgayDK,
                                                  NgayBD = dk.NgayBD,
                                                  NgayKT = dk.NgayKT,
                                               
                                                 // Tang = dk.Tang,
                                                  HoTen = sv.HoTen,
                                                  CCCD = sv.CCCD,
                                                  Email = sv.Email,
                                                  HinhThucThanhToan = dk.HinhThucThanhToan,
                                                  SDT = sv.SDT,
                                                  LoaiPhong_ = lp.TenLoaiPhong,
                                                  DonGiaPhong = lp.DonGiaPhong.GetValueOrDefault()
                                              };

            // Trả về danh sách kết quả
            return danhSachDangKyPhongTheoMaSV.ToList();
        }

        public decimal LayDonGia(int MaLoaiPhong)
        {
            return xuly.LayDonGia(MaLoaiPhong);
        }

        public int DemSoSV(string maPhong)
        {
             return xuly.DemSoSV(maPhong);
           
        }

        //public List<int?> LayTatCaGiuongTrongPhong(string maPhong)
        //{
        //    return xuly.LayTatCaGiuongTrongPhong(maPhong);
        //}
        //public List<int?> LayGiuongDaDangKy(string maPhong)
        //{
        //    return xuly.LayGiuongDaDangKy(maPhong);
        //}

            public List<Phong> GetPhongTheoGioiTinh(string gioiTinh)
        {
            // Lấy danh sách phòng từ DAL
            List<Phong> phongList = dal_Phong.GetPhong();  // Giả sử xuly.GetPhong() lấy tất cả phòng từ DAL
            List<Phong> filteredPhongList = new List<Phong>();

            // Lọc phòng theo giới tính
            if (gioiTinh == "Nam")
            {
                // Lọc phòng cho Nam, chỉ lấy các phòng từ tầng 6 trở lên
                filteredPhongList = phongList.Where(p => GetTangFromMaPhong(p.MaPhong) >= 6).ToList();
            }
            else if (gioiTinh == "Nu")
            {
                // Lọc phòng cho Nữ, chỉ lấy các phòng từ tầng 1 đến 5
                filteredPhongList = phongList.Where(p => GetTangFromMaPhong(p.MaPhong) >= 1 && GetTangFromMaPhong(p.MaPhong) <= 5).ToList();
            }

            return filteredPhongList;
        }

        // Hàm trợ giúp để lấy tầng từ mã phòng
        private int GetTangFromMaPhong(string maPhong)
        {
            // Giả sử mã phòng có định dạng "KTX_XX_YY" và "XX" là tầng
            var parts = maPhong.Split('-');  // Tách mã phòng theo dấu gạch dưới
            if (parts.Length >= 2)
            {
                // Chuyển phần "XX" (tầng) thành số nguyên
                if (int.TryParse(parts[1], out int tang))
                {
                    return tang;
                }
            }
            return -1;  // Trả về -1 nếu không tìm thấy tầng hợp lệ
        }

        public string GetMaxMaPhong()
        {
            try
            {
                // Lấy tất cả phiếu đăng ký phòng từ DAL
                var listDangKyPhong = xuly.GetAllDangKyPhong();

                // Kiểm tra nếu danh sách rỗng
                if (listDangKyPhong == null || !listDangKyPhong.Any())
                {
                    return "DKP000"; // Trả về mã phòng đầu tiên nếu không có phiếu đăng ký
                }

                // Lấy giá trị số lớn nhất từ danh sách
                var maxMaPhong = listDangKyPhong
                                     .Select(dk =>
                                     {
                                         // Trích xuất phần số từ MaDangKyPhong
                                         string numberPart = dk.MaDangKyPhong.Substring(3); // Bỏ "DKP"

                                         // Thử chuyển đổi phần số thành int
                                         if (int.TryParse(numberPart, out int parsedValue))
                                         {
                                             return parsedValue;
                                         }
                                         return -1; // Trả về -1 nếu không hợp lệ
                                     })
                                     .Where(value => value != -1) // Loại bỏ các giá trị không hợp lệ
                                     .DefaultIfEmpty(0) // Nếu danh sách rỗng, mặc định trả về 0
                                     .Max(); // Lấy giá trị lớn nhất

                // Chuyển giá trị maxMaPhong thành định dạng chuỗi với tiền tố "DKP" và phần số có 3 chữ số
                return "DKP" + (maxMaPhong + 1).ToString("D3"); // Trả về mã phòng tiếp theo
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy mã phòng lớn nhất: " + ex.Message);
                return "DKP001"; // Trả về mã phòng mặc định nếu có lỗi
            }
        }



        //  Phương thức lấy danh sách đăng ký phòng kèm theo thông tin sinh viên
        public List<DangKyPhong> GetDangKyPhong_()
        {
            // Gọi phương thức từ DAL để lấy dữ liệu từ bảng DangKyPhong và SinhVien
            var danhSachDangKyPhong = xuly.GetAllDangKyPhong();
            var danhSachSinhVien = xuly.GetSinhVien();
            var danhSachPhong = dal_Phong.GetPhong();
            var danhSachLoaiPhong = xuly.GetLoaiPhongs();  // Lấy dữ liệu từ bảng LoaiPhong
            // Kết hợp thông tin từ hai danh sách
            var danhSachDangKyPhongSinhVien = from dk in danhSachDangKyPhong
                                              join sv in danhSachSinhVien on dk.MaSinhVien equals sv.MaSinhVien
                                              join p in danhSachPhong on dk.MaPhong equals p.MaPhong  // Thêm join với bảng Phong
                                              join lp in danhSachLoaiPhong on p.MaLoaiPhong equals lp.MaLoaiPhong  // Thêm join với bảng LoaiPhong
                                              select new DangKyPhong
                                              {
                                                  MaDangKyPhong = dk.MaDangKyPhong,
                                                  MaPhong = dk.MaPhong,
                                                  MaSinhVien = dk.MaSinhVien,
                                                  NgayDK = dk.NgayDK,
                                                  NgayBD = dk.NgayBD,
                                                  NgayKT = dk.NgayKT,
                                                 
                                                //  Tang = dk.Tang,
                                                  HoTen = sv.HoTen,
                                                  CCCD = sv.CCCD,
                                                  Email = sv.Email,
                                                  HinhThucThanhToan=dk.HinhThucThanhToan,
                                                  SDT = sv.SDT,
                                                  LoaiPhong_ = lp.TenLoaiPhong,  
                                                  DonGiaPhong =lp.DonGiaPhong.GetValueOrDefault()
                                              };

            return danhSachDangKyPhongSinhVien.ToList();
        }

        public List<DangKyPhong> LayThongTinSinhVien(string maPhong)
        {
            return xuly.LayThongTinSinhVien(maPhong);
        }

        public bool ThemDangKyPhong(DangKyPhong dkphong,SinhVien sv)
        {
            return xuly.ThemPhieuDangKy(dkphong,sv);
        }

        
        public bool SuaPhieuDangKyPhong(string maDangKyPhong, string columnIndex, string duLieuMoi)
        {
            //return xuly.SuaPhieuDangKy(dkphong) ;
            return xuly.SuaPhieuDangKyPhong(maDangKyPhong, columnIndex, duLieuMoi);
        }
         
        public bool XoaPhieuDangKyPhong(string dkphong)
        {
            return xuly.XoaPhieuDangKyPhong(dkphong);
         
        }

        public List<Phong> GetPhong()
        {
            return xuly.GetPhongs();
        }

        public List<LoaiPhong> GetLoais()
        {
            return xuly.GetLoaiPhongs();
        }
    }
}
