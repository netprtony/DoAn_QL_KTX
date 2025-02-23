using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_NhapNguyenLieu
    {
        DA_QL_KTXDataContext ktx = new DA_QL_KTXDataContext();

        // Lấy danh sách mã mua
        public List<string> LayDanhSachMaMua()
        {
            // Truy vấn lấy danh sách các mã mua duy nhất
            var danhSachMaMua = ktx.MuaNguyenLieus
                                    .Select(m => m.MaMua)
                                    .Distinct()
                                    .ToList();

            return danhSachMaMua;
        }

        // Lọc theo mã mua
        public List<MuaNguyenLieu> LocTheoMaMua(string maMua)
        {
            var danhSach = ktx.MuaNguyenLieus.Where(m => m.MaMua == maMua).ToList();
            return danhSach.Select(m => new MuaNguyenLieu
            {
                MaMua = m.MaMua,
                MaNhanVien = m.MaNhanVien,
               // SoLuong = m.SoLuong,
                GiaMua = m.GiaMua,
                NgayMua = m.NgayMua
            }).ToList();
        }

        // Lọc theo mã nhân viên
        public List<MuaNguyenLieu> LocTheoMaNhanVien(string maNhanVien)
        {
            var danhSach = ktx.MuaNguyenLieus.Where(m => m.MaNhanVien == maNhanVien).ToList();
            return danhSach.Select(m => new MuaNguyenLieu
            {
                MaMua = m.MaMua,
                MaNhanVien = m.MaNhanVien,
               // SoLuong = m.SoLuong,
                GiaMua = m.GiaMua,
                NgayMua = m.NgayMua
            }).ToList();
        }
        // Lọc theo thứ tự giảm dần theo tổng tiền (GiaMua * SoLuong)
        public List<MuaNguyenLieu> LocGiamTheoTongTien()
        {
            var danhSach = ktx.MuaNguyenLieus
                               .OrderByDescending(m => m.GiaMua)
                               .ToList();
            return danhSach.Select(m => new MuaNguyenLieu
            {
                MaMua = m.MaMua,
                MaNhanVien = m.MaNhanVien,
              //  SoLuong = m.SoLuong,
                GiaMua = m.GiaMua,
                NgayMua = m.NgayMua
            }).ToList();
        }
        public List<MuaNguyenLieu> LocTangTheoTongTien()
        {
            var danhSach = ktx.MuaNguyenLieus
                                .OrderBy(m => m.GiaMua)  // Tăng dần theo tổng tiền
                                .ToList();

            return danhSach.Select(m => new MuaNguyenLieu
            {
                MaMua = m.MaMua,
                MaNhanVien = m.MaNhanVien,
              //  SoLuong = m.SoLuong,
                GiaMua = m.GiaMua,
                NgayMua = m.NgayMua
            }).ToList();
        }


        // Lọc theo tháng và năm
        public List<MuaNguyenLieu> LocTheoThangNam(int thang, int nam)
        {
            var danhSach = ktx.MuaNguyenLieus
                               .Where(m => m.NgayMua.Month == thang && m.NgayMua.Year == nam)
                               .ToList();
            return danhSach.Select(m => new MuaNguyenLieu
            {
                MaMua = m.MaMua,
                MaNhanVien = m.MaNhanVien,
              //  SoLuong = m.SoLuong,
                GiaMua = m.GiaMua,
                NgayMua = m.NgayMua
            }).ToList();
        }

        // Lọc theo ngày, tháng, năm
        public List<MuaNguyenLieu> LocTheoNgayThangNam(DateTime ngay)
        {
            var danhSach = ktx.MuaNguyenLieus
                               .Where(m => m.NgayMua == ngay)
                               .ToList();
            return danhSach.Select(m => new MuaNguyenLieu
            {
                MaMua = m.MaMua,
                MaNhanVien = m.MaNhanVien,
              //  SoLuong = m.SoLuong,
                GiaMua = m.GiaMua,
                NgayMua = m.NgayMua
            }).ToList();
        }
    

    public List<MuaNguyenLieu> HienThiMuaNguyenLieu()
        {
            // Lấy tất cả dữ liệu MuaNguyenLieu từ cơ sở dữ liệu
            var danhSach = ktx.MuaNguyenLieus.ToList();

            // Lọc lại chỉ những trường cần thiết nếu cần, hoặc giữ nguyên đối tượng
            return danhSach.Select(m => new MuaNguyenLieu
            {
                MaMua = m.MaMua,
                MaNhanVien = m.MaNhanVien,
                SoLuong = m.SoLuong,
                GiaMua = m.GiaMua,
                NgayMua = m.NgayMua
            }).ToList();
        }


        //thông tin đơn mua theo mã mua
        public List<object> HienThiThongTinDonMua(string maMua)
        {
            var chiTietMua = (from ct in ktx.ChiTietMuas
                              join nl in ktx.NguyenLieus on ct.MaNguyenLieu equals nl.MaNguyenLieu
                              where ct.MaMua == maMua
                              select new
                              {
                                  MaMua = ct.MaMua,
                                  MaNguyenLieu = ct.MaNguyenLieu,
                                  TenNguyenLieu = nl.TenNguyenLieu,
                                  SoLuong = ct.SoLuong,
                                  GiaThanh = ct.GiaThanh
                              }).ToList();

            return chiTietMua.Cast<object>().ToList();
        }

        // chi tiết đơn nhập nguyên liệu theo mã
        public List<object> HienThiChiTietMua(string maMua)
        {
            var chiTietMua = (from ct in ktx.ChiTietMuas
                              join nl in ktx.NguyenLieus on ct.MaNguyenLieu equals nl.MaNguyenLieu
                              where ct.MaMua == maMua
                              select new
                              {
                                  MaMua = ct.MaMua,
                                  MaNguyenLieu = ct.MaNguyenLieu,
                                  TenNguyenLieu = nl.TenNguyenLieu,
                                  SoLuong = ct.SoLuong,
                                  GiaThanh = ct.GiaThanh
                              }).ToList();

            return chiTietMua.Cast<object>().ToList();
        }

        // Hàm lấy tên nguyên liệu từ mã nguyên liệu
        public string LayTenNguyenLieu(string maNguyenLieu)
        {
            var nguyenLieu = ktx.NguyenLieus
                               .Where(nl => nl.MaNguyenLieu == maNguyenLieu)
                               .FirstOrDefault();
            return nguyenLieu?.TenNguyenLieu ?? string.Empty; // Trả về tên nguyên liệu nếu tìm thấy, nếu không trả về chuỗi rỗng
        }
        public ChiTietMua HienThiChiTietDonMua(string maMua)
        {
            try
            {
                // Truy vấn chi tiết đơn mua từ cơ sở dữ liệu
                var chiTietMua = (from ct in ktx.ChiTietMuas // Lấy dữ liệu từ bảng ChiTietMua
                                  join nl in ktx.NguyenLieus on ct.MaNguyenLieu equals nl.MaNguyenLieu // Kết nối với bảng NguyenLieu
                                  where ct.MaMua == maMua // Điều kiện lọc theo MaMua
                                  select new ChiTietMua
                                  {
                                      MaMua = ct.MaMua,
                                      MaNguyenLieu = ct.MaNguyenLieu,
                                      SoLuong = ct.SoLuong,
                                      GiaThanh = ct.GiaThanh,
                                    
                                  }).FirstOrDefault(); // Lấy bản ghi đầu tiên (hoặc null nếu không có)

                return chiTietMua; // Trả về chi tiết đơn mua
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("Lỗi khi lấy thông tin chi tiết đơn mua: " + ex.Message);
                return null; // Trả về null trong trường hợp có lỗi
            }
        }

        public MuaNguyenLieu HienThiThongTinMuaNguyenLieu(string maMua)
        {
            try
            {
                // Truy vấn thông tin mua nguyên liệu từ cơ sở dữ liệu
                var muaNguyenLieu = ktx.MuaNguyenLieus // ktx là đối tượng DbContext
                                       .Where(mn => mn.MaMua == maMua)
                                       .FirstOrDefault(); // Lấy bản ghi đầu tiên hoặc null nếu không có

                return muaNguyenLieu; // Trả về đối tượng mua nguyên liệu
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("Lỗi khi lấy thông tin mua nguyên liệu: " + ex.Message);
                return null; // Trả về null trong trường hợp lỗi
            }
        }
        // Phương thức lấy mã đơn mua mới, tự động tăng
        private string GetNewMaMua()
        {
            // Lấy mã đơn mua lớn nhất hiện tại
            var lastMaMua = ktx.MuaNguyenLieus
                               .OrderByDescending(m => m.MaMua)
                               .FirstOrDefault();

            if (lastMaMua != null)
            {
                // Tách số cuối cùng từ mã đơn mua hiện tại và tăng thêm 1
                int lastNumber = int.Parse(lastMaMua.MaMua.Substring(1)); // Lấy số sau chữ "M"
                int newNumber = lastNumber + 1;
                return "M" + newNumber.ToString("D3"); // Định dạng "M001", "M002", ...
            }

            return "M001";  // Nếu không có đơn nhập nào, bắt đầu từ M001
        }

        // Phương thức thêm đơn nhập nguyên liệu
        public bool ThemDonNhapNguyenLieu(MuaNguyenLieu muanguyenlieu, List<ChiTietMua> CT_Mua)
        {
            try
            {
                // Lấy mã đơn nhập lớn nhất từ cơ sở dữ liệu
                var maxMaMua = ktx.MuaNguyenLieus
                    .Where(m => m.MaMua.StartsWith("M")) // Lọc theo các mã bắt đầu bằng "M"
                    .OrderByDescending(m => m.MaMua)
                    .FirstOrDefault();

                string newMaMua = "M001"; // Mặc định là M001 nếu chưa có dữ liệu

                if (maxMaMua != null)
                {
                    // Lấy phần số sau chữ "M" và cộng 1
                    int maxNumber = int.Parse(maxMaMua.MaMua.Substring(1)); // Lấy phần số của mã
                    newMaMua = "M" + (maxNumber + 1).ToString("D3"); // Tạo mã mới với số tự động tăng
                }

                // Gán mã đơn nhập mới cho muanguyenlieu
                muanguyenlieu.MaMua = newMaMua;

                // Thêm đơn nhập vào bảng MuaNguyenLieu
                ktx.MuaNguyenLieus.InsertOnSubmit(muanguyenlieu);

                // Thêm các chi tiết mua vào bảng ChiTietMua
                foreach (var chiTiet in CT_Mua)
                {
                    chiTiet.MaMua = newMaMua; // Gán mã đơn nhập cho chi tiết
                    ktx.ChiTietMuas.InsertOnSubmit(chiTiet); // Thêm chi tiết vào bảng ChiTietMua
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                ktx.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                // Nếu có lỗi, hiển thị thông báo lỗi và rollback
                Console.WriteLine("Lỗi khi thêm đơn nhập nguyên liệu và chi tiết: " + ex.Message);
                return false;
            }
        }



    }
}
