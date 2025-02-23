using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_TrangThietBi
    {
        DA_QL_KTXDataContext ktx = new DA_QL_KTXDataContext();
        public DAL_TrangThietBi() { }
        // Lấy danh sách tất cả trang thiết bị
        public List<TrangThietBi> GetAllTrangThietBi()
        {
            return ktx.TrangThietBis.ToList();
        }
        //lấy danh sách tất cả chi trang thiết bị ở từng phòng ạ
        public List<CT_TrangThietBi> GetAllCT_TrangThietBi()
        {
            return ktx.CT_TrangThietBis.ToList();
        }
        // Hàm lấy thông tin chi tiết trang thiết bị theo MaThietBi và MaPhong
        public PhieuKTTrangThietBi GetCT_TrangThietBiByMaThietBiAndMaPhong(string maThietBi, string maPhong)
        {
            try
            {
                // Truy vấn kết hợp giữa bảng CT_TrangThietBi và TrangThietBi
                var result = (from ct in ktx.CT_TrangThietBis
                              join ttb in ktx.TrangThietBis on ct.MaThietBi equals ttb.MaThietBi
                              where ct.MaThietBi == maThietBi && ct.MaPhong == maPhong
                              select new
                              {
                                  ct.MaThietBi,
                                  ct.MaPhong,
                                  ct.SoLuong,
                                  TenThietBi = ttb.TenThietBi // Lấy tên thiết bị từ bảng TrangThietBi
                              }).FirstOrDefault(); // Lấy kết quả đầu tiên hoặc null nếu không tìm thấy

                // Nếu kết quả có, chuyển đổi dữ liệu về kiểu CT_TrangThietBi
                if (result != null)
                {
                    return new PhieuKTTrangThietBi
                    {
                        MaThietBi = result.MaThietBi,
                        MaPhong = result.MaPhong,
                        SoLuong = result.SoLuong,
                        TenThietBi = result.TenThietBi // Trả về tên thiết bị
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("Lỗi khi truy vấn dữ liệu: " + ex.Message);
                return null;
            }
        }

        // Xem chi tiết trang thiết bị theo mã
        public TrangThietBi GetTrangThietBiById(string maThietBi)
        {
            var thietBi = ktx.TrangThietBis.FirstOrDefault(tb => tb.MaThietBi == maThietBi);
            if (thietBi != null)
            {
                return new TrangThietBi
                {
                    MaThietBi = thietBi.MaThietBi,
                    TenThietBi = thietBi.TenThietBi,
                    TrangThai = thietBi.TrangThai
                };
            }
            return null;
        }

        // Thêm trang thiết bị mới
        public bool AddTrangThietBi(TrangThietBi thietBi)
        {
            try
            {
                var newThietBi = new TrangThietBi
                {
                    MaThietBi = thietBi.MaThietBi,
                    TenThietBi = thietBi.TenThietBi,
                    TrangThai = thietBi.TrangThai
                };

                ktx.TrangThietBis.InsertOnSubmit(newThietBi);
                ktx.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Sửa thông tin trang thiết bị
        public bool UpdateTrangThietBi(TrangThietBi thietBi)
        {
            try
            {
                var existingThietBi = ktx.TrangThietBis.FirstOrDefault(tb => tb.MaThietBi == thietBi.MaThietBi);
                if (existingThietBi != null)
                {
                    existingThietBi.TenThietBi = thietBi.TenThietBi;
                    existingThietBi.TrangThai = thietBi.TrangThai;

                    ktx.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Xóa trang thiết bị theo mã
        public bool DeleteTrangThietBi(string maThietBi)
        {
            try
            {
                var thietBi = ktx.TrangThietBis.FirstOrDefault(tb => tb.MaThietBi == maThietBi);
                if (thietBi != null)
                {
                    ktx.TrangThietBis.DeleteOnSubmit(thietBi);
                    ktx.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
       
        // Lấy danh sách tất cả các phòng
        public List<Phong> GetAllMaPhong()
        {
            // Lấy danh sách tất cả các phòng từ bảng Phong
            var danhSachPhong = ktx.Phongs.ToList();
            return danhSachPhong;
        }
        // Lấy tên loại phòng theo mã phòng
        public string GetTenLoaiPhongByMaPhong(string maPhong)
        {
            // Lấy MaLoaiPhong từ bảng Phong theo MaPhong
            var maLoaiPhong = (from p in ktx.Phongs
                               where p.MaPhong == maPhong
                               select p.MaLoaiPhong).FirstOrDefault();

            // Truy vấn LINQ để lấy tên loại phòng từ bảng LoaiPhong qua MaLoaiPhong
            var tenLoaiPhong = (from lp in ktx.LoaiPhongs
                                where lp.MaLoaiPhong == maLoaiPhong
                                select lp.TenLoaiPhong).FirstOrDefault();

            return tenLoaiPhong; // Trả về tên loại phòng, nếu không tìm thấy sẽ trả về null
        }
        public List<string> GetTrangThietBiByMaPhong(string maPhong)
        {
            // Lấy MaLoaiPhong từ bảng Phong theo MaPhong
            var maLoaiPhong = (from p in ktx.Phongs
                               where p.MaPhong == maPhong
                               select p.MaLoaiPhong).FirstOrDefault();

            List<string> trangThietBiList = new List<string>();

            if (maLoaiPhong == 1) // Nếu là phòng đơn (MaLoaiPhong = 1)
            {
                // Lấy tất cả trang thiết bị, ngoại trừ Tủ Lạnh và TiVi
                trangThietBiList = (from tb in ktx.TrangThietBis
                                    where tb.TenThietBi != "Tủ Lạnh" && tb.TenThietBi != "Ti Vi"
                                    select tb.TenThietBi).ToList();
            }
            else if (maLoaiPhong == 2) // Nếu là phòng đôi (MaLoaiPhong = 2)
            {
                // Lấy tất cả trang thiết bị
                trangThietBiList = (from tb in ktx.TrangThietBis
                                    select tb.TenThietBi).ToList();
            }

            return trangThietBiList;
        }
        public string GetMaThietBiByTen(string tenThietBi)
        {
            // Truy vấn để lấy mã thiết bị từ tên trang thiết bị
            var maThietBi = (from tb in ktx.TrangThietBis
                             where tb.TenThietBi == tenThietBi
                             select tb.MaThietBi).FirstOrDefault();

            return maThietBi; // Trả về mã thiết bị, nếu không tìm thấy sẽ trả về null
        }

        // Hàm lấy danh sách thông tin mã phòng, mã thiết bị, tên thiết bị và số lượng
        public List<PhieuKTTrangThietBi> GetThongTinTrangThietBi()
        {
            // Thực hiện truy vấn LINQ để lấy dữ liệu từ các bảng kết hợp
            var query = from ct in ktx.CT_TrangThietBis
                        join tb in ktx.TrangThietBis on ct.MaThietBi equals tb.MaThietBi
                        join p in ktx.Phongs on ct.MaPhong equals p.MaPhong
                        select new PhieuKTTrangThietBi
                        {
                            MaPhong = ct.MaPhong,
                            MaThietBi = ct.MaThietBi,
                            TenThietBi = tb.TenThietBi,
                            SoLuong = ct.SoLuong
                        };

            // Trả về danh sách kết quả
            return query.ToList();
        }
        // Quan ly nha
        public List<DonNhapDTO> GetAllDonNhapWithDetails()
        {
            var query = from dn in ktx.DonNhap_TTBs
                        join nv in ktx.NhanViens on dn.MaNhanVien equals nv.MaNhanVien
                        join ncc in ktx.NhaCungCaps on dn.MaNCC equals ncc.MaNCC
                        select new DonNhapDTO
                        {
                            MaDonNhap = dn.MaDonNhap,
                            NgayNhap = dn.NgayNhap,
                            TongTien = dn.TongTien,
                            TrangThai = dn.TrangThai,
                            MaNhanVien = dn.MaNhanVien,
                            TenNhanVien = nv.HoTen, // Lấy tên nhân viên từ bảng Nhân Viên
                            MaNCC = dn.MaNCC,
                            TenNCC = ncc.TenNCC     // Lấy tên nhà cung cấp từ bảng Nhà Cung Cấp
                        };

            return query.ToList();
        }
        public bool CapNhatTrangThaiChiTietTTB(string maThietBi, string maPhong, string trangThai)
        {
            try
            {
                // Tìm chi tiết thiết bị trong cơ sở dữ liệu
                CT_TrangThietBi chiTiet = ktx.CT_TrangThietBis.FirstOrDefault(ct => ct.MaThietBi == maThietBi && ct.MaPhong == maPhong);

                if (chiTiet != null)
                {
                    // Cập nhật trạng thái
                    chiTiet.TrangThai = trangThai;
                    ktx.SubmitChanges();
                    return true;
                }
                return false; // Không tìm thấy chi tiết
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật trạng thái chi tiết thiết bị: {ex.Message}");
                return false;
            }
        }
        public List<CTTTB_DTO> GetAllChiTietTrangThietBiWithTen()
        {
            try
            {
                var query = from ct in ktx.CT_TrangThietBis
                            join tb in ktx.TrangThietBis on ct.MaThietBi equals tb.MaThietBi
                            select new CTTTB_DTO
                            {
                                MaThietBi = ct.MaThietBi,
                                MaPhong = ct.MaPhong,
                                SoLuong = ct.SoLuong,
                                TrangThai = ct.TrangThai,
                                TenThietBi = tb.TenThietBi // Thuộc tính bổ sung
                            };

                return query.ToList(); // Trả về danh sách
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy chi tiết thiết bị: {ex.Message}");
                return new List<CTTTB_DTO>();
            }
        }

        public List<CTTTB_DTO> GetChiTietTrangThietBiByMaThietBi(string maThietBi)
        {
            try
            {
                // Truy vấn chi tiết thiết bị với điều kiện mã thiết bị
                var query = from ct in ktx.CT_TrangThietBis
                            join tb in ktx.TrangThietBis on ct.MaThietBi equals tb.MaThietBi
                            where ct.MaThietBi == maThietBi
                            select new CTTTB_DTO
                            {
                                MaThietBi = ct.MaThietBi,
                                MaPhong = ct.MaPhong,
                                SoLuong = ct.SoLuong,
                                TrangThai = ct.TrangThai,
                                TenThietBi = tb.TenThietBi
                            };

                return query.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy chi tiết thiết bị: {ex.Message}");
                return new List<CTTTB_DTO>();
            }
        }


        public string GetNextMaThietBi()
        {
            // Lấy mã thiết bị lớn nhất hiện tại từ cơ sở dữ liệu
            var maxMaThietBi = ktx.TrangThietBis
                                 .OrderByDescending(tb => tb.MaThietBi)
                                 .Select(tb => tb.MaThietBi)
                                 .FirstOrDefault();

            if (maxMaThietBi != null)
            {
                // Tách phần số từ mã hiện tại
                //Dùng TakeWhile(char.IsLetter) để lấy tất cả các ký tự chữ cái liên tiếp từ đầu.
                //Dùng SkipWhile(char.IsLetter) để lấy phần số (phần còn lại).
                string prefix = new string(maxMaThietBi.TakeWhile(char.IsLetter).ToArray()); // Lấy phần chữ cái
                string numberPart = new string(maxMaThietBi.SkipWhile(char.IsLetter).ToArray()); // Lấy phần số

                // Chuyển đổi phần số và tăng giá trị
                int number = int.Parse(numberPart);
                number++; // Tăng giá trị lên 1

                // Trả về mã mới với phần số định dạng 3 chữ số
                return $"{prefix}{number:D3}";
            }
            else
            {
                // Nếu chưa có mã nào trong cơ sở dữ liệu, bắt đầu từ mã đầu tiên
                return "TTB001";
            }
        }
        public bool UpdateTrangThaiTrangThietBi(string maThietBi, string trangThai)
        {
            try
            {
                var thietBi = ktx.TrangThietBis.FirstOrDefault(tb => tb.MaThietBi == maThietBi);
                if (thietBi != null)
                {
                    thietBi.TrangThai = trangThai; // Cập nhật trạng thái thành "Ngưng Hoạt Động"
                    ktx.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi khi xoa: {ex.Message}");
                return false;
            }
        }

        public bool HasChiTietThietBi(string maThietBi)
        {
            try
            {
                // Kiểm tra xem có chi tiết thiết bị nào liên quan đến mã thiết bị
                return ktx.CT_TrangThietBis.Any(ct => ct.MaThietBi == maThietBi);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi kiểm tra chi tiết thiết bị: {ex.Message}");
                return false;
            }
        }
        public int? GetTongSoLuongHoatDong(string maThietBi)
        {
            try
            {
                // Lấy tổng số lượng chi tiết không ở trạng thái "Ngừng Hoạt Động"
                return ktx.CT_TrangThietBis
                          .Where(ct => ct.MaThietBi == maThietBi && ct.TrangThai != "Ngừng Hoạt Động")
                          .Sum(ct => ct.SoLuong);
            }
            catch (Exception)
            {
                return 0; // Nếu có lỗi hoặc không có chi tiết nào, trả về 0
            }
        }
        public bool UpdateChiTietTrangThietBi(CT_TrangThietBi chiTiet)
        {
            try
            {
                // Tìm chi tiết thiết bị dựa vào MaThietBi và MaPhong
                var existingChiTiet = ktx.CT_TrangThietBis
                                         .FirstOrDefault(ct => ct.MaThietBi == chiTiet.MaThietBi && ct.MaPhong == chiTiet.MaPhong);

                if (existingChiTiet != null)
                {
                    // Cập nhật thông tin chi tiết thiết bị
                    existingChiTiet.SoLuong = chiTiet.SoLuong;
               //     existingChiTiet.TrangThai = chiTiet.TrangThai;

                    // Lưu thay đổi
                    ktx.SubmitChanges();
                    return true;
                }

                return false; // Không tìm thấy chi tiết thiết bị
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật chi tiết TTB: {ex.Message}");
                return false;
            }
        }
        public bool AddChiTietTrangThietBi(CT_TrangThietBi chiTiet)
        {
            try
            {
                ktx.CT_TrangThietBis.InsertOnSubmit(chiTiet); // Thêm vào cơ sở dữ liệu
                ktx.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm chi tiết TTB: {ex.Message}");
                return false;
            }
        }
        public bool UpdateTrangThaiChiTietByMaThietBi(string maThietBi, string trangThai)
        {
            try
            {
                // Lấy tất cả các chi tiết liên quan đến mã thiết bị
                var chiTietList = ktx.CT_TrangThietBis.Where(ct => ct.MaThietBi == maThietBi).ToList();

                if (chiTietList.Any())
                {
                    // Cập nhật trạng thái cho từng chi tiết
                    foreach (var chiTiet in chiTietList)
                    {
                        chiTiet.TrangThai = trangThai;
                    }

                    // Lưu thay đổi
                    ktx.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật trạng thái chi tiết TTB: {ex.Message}");
                return false;
            }
        }
        public bool UpdateTongSoLuongTrangThietBi(string maThietBi, int? tongSoLuong)
        {
            try
            {
                var thietBi = ktx.TrangThietBis.FirstOrDefault(tb => tb.MaThietBi == maThietBi);
                if (thietBi != null)
                {
                    thietBi.SoLuong = tongSoLuong;
                    ktx.SubmitChanges();
                    return true;
                }
                return false; // Không tìm thấy thiết bị
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật tổng số lượng thiết bị: {ex.Message}");
                return false;
            }
        }
        public List<ChiTietDonNhap_DTO> GetChiTietDonNhapByMaDonNhap(string maDonNhap)
        {
            var query = from ctdn in ktx.CT_DonNhaps
                        join ttb in ktx.TrangThietBis on ctdn.MaThietBi equals ttb.MaThietBi
                        where ctdn.MaDonNhap == maDonNhap
                        select new ChiTietDonNhap_DTO
                        {
                            MaDonNhap = ctdn.MaDonNhap,
                            MaThietBi = ctdn.MaThietBi,
                            TenThietBi = ttb.TenThietBi,
                            SoLuong = ctdn.SoLuong,
                            DonGia = ctdn.DonGia,
                            ThanhTien = ctdn.SoLuong * ctdn.DonGia
                        };

            return query.ToList();
        }
        public List<DonNhapDTO> GetAllThongTinDonNhap()
        {
            try
            {

                // Truy vấn JOIN giữa các bảng
                var query = from dn in ktx.DonNhap_TTBs
                            join ncc in ktx.NhaCungCaps on dn.MaNCC equals ncc.MaNCC into nccGroup
                            from ncc in nccGroup.DefaultIfEmpty()
                            join nv in ktx.NhanViens on dn.MaNhanVien equals nv.MaNhanVien into nvGroup
                            from nv in nvGroup.DefaultIfEmpty()
                            select new DonNhapDTO
                            {
                                MaDonNhap = dn.MaDonNhap,
                                NgayNhap = dn.NgayNhap,
                                TongTien = dn.TongTien,
                                TrangThai = dn.TrangThai,
                                MaNCC = ncc.MaNCC,
                                TenNCC = ncc.TenNCC,
                                DiaChi = ncc.DiaChi,
                                MaNhanVien = nv.MaNhanVien,
                                TenNhanVien = nv.HoTen,
                                ChucVu = nv.ChucVu
                            };

                return query.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy dữ liệu: {ex.Message}");
            }
        }
        // Thống kê
        public List<ThongKeDonNhap> GetThongKeTheoQuy(int nam, int quy)
        {
            // Gọi thủ tục usp_ThongKeTheoQuy qua LINQ to SQL
            var result = ktx.ExecuteQuery<ThongKeDonNhap>(
                "EXEC usp_ThongKeSoLuongNhapTBTheoQuy @Nam = {0}, @Quy = {1}", nam, quy).ToList();

            return result;
        }

        public List<ThongKeDonNhap> GetThongKeTongTienTheoThang(int nam)
        {
            var result = ktx.ExecuteQuery<ThongKeDonNhap>(
                "EXEC usp_ThongKeTongTienTheoThang @Nam = {0}", nam).ToList();

            return result;
        }

    }
}
