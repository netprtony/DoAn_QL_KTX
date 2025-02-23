using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_YeuCauSuaChua
    {
        DA_QL_KTXDataContext ktx = new DA_QL_KTXDataContext();
        public DAL_YeuCauSuaChua()
        {

        }

        // Hàm lấy danh sách yêu cầu sửa chữa theo MaYCSuaChua
        public List<CT_YeuCauSuaChua> GetDanhSachYeuCauSuaChua(string maYCSuaChua)
        {
            try
            {
                // Truy vấn danh sách từ cơ sở dữ liệu
                var danhSach = ktx.CT_YeuCauSuaChuas
                    .Where(yc => yc.MaYCSuaChua == maYCSuaChua)
                    .Select(yc => new CT_YeuCauSuaChua
                    {
                        MaYCSuaChua = yc.MaYCSuaChua,
                        MaThietBi = yc.MaThietBi,
                        MaPhong = yc.MaPhong,
                        SoLuong = yc.SoLuong,
                        TinhTrang = yc.TinhTrang,
                        PhiSuaChua = yc.PhiSuaChua
                    })
                    .ToList();

                return danhSach;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ (ghi log nếu cần)
                Console.WriteLine("Lỗi khi lấy danh sách yêu cầu sửa chữa: " + ex.Message);
                return new List<CT_YeuCauSuaChua>(); // Trả về danh sách rỗng nếu có lỗi
            }
        }
        public bool CapNhatYeuCauSuaChua(string maYCSuaChua, string maThietBi, int soLuong, string tinhTrang, decimal phiSuaChua)
        {
            try
            {
                // Tìm bản ghi cần cập nhật dựa vào mã yêu cầu sửa chữa và mã thiết bị
                var yeuCau = ktx.CT_YeuCauSuaChuas.SingleOrDefault(yc => yc.MaYCSuaChua == maYCSuaChua
                                                                        && yc.MaThietBi == maThietBi);

                if (yeuCau == null)
                {
                    // Bản ghi không tồn tại
                    return false;
                }

                // Cập nhật thông tin
                yeuCau.SoLuong = soLuong;
                yeuCau.TinhTrang = tinhTrang;
                yeuCau.PhiSuaChua = phiSuaChua;

                // Lưu thay đổi vào cơ sở dữ liệu
                ktx.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ (ghi log nếu cần)
                Console.WriteLine("Lỗi khi cập nhật: " + ex.Message);
                return false;
            }
        }



        public int GetSoLuongTrangThietBi(string maPhong, string maThietBi)
        {
            try
            {
                // Truy vấn số lượng trang thiết bị theo mã phòng và mã thiết bị
                var soLuong = ktx.CT_TrangThietBis
                                  .Where(ct => ct.MaPhong == maPhong && ct.MaThietBi == maThietBi)
                                  .Select(ct => ct.SoLuong)
                                  .FirstOrDefault();

                // Đảm bảo trả về số lượng là kiểu int (nếu không tìm thấy trả về 0)
                return soLuong ?? 0;  // Sử dụng "??" để đảm bảo trả về int không nullable
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong DAL (GetSoLuongTrangThietBi): " + ex.Message);
                return 0; // Nếu có lỗi, trả về 0
            }
        }



        public bool CapNhatYeuCauSuaChua(YeuCauSuaChua ycSuaChua)
        {
            try
            {
                // Kiểm tra nếu đối tượng ycSuaChua là null
                if (ycSuaChua == null)
                {
                    return false; // Không có dữ liệu để cập nhật
                }

                // Tìm yêu cầu sửa chữa theo mã yêu cầu
                var yc = ktx.YeuCauSuaChuas.FirstOrDefault(y => y.MaYCSuaChua == ycSuaChua.MaYCSuaChua);
                if (yc == null)
                {
                    return false; // Không tìm thấy yêu cầu sửa chữa với mã này
                }

                // Cập nhật các trường thông tin từ đối tượng ycSuaChua
                yc.MaNhanVien = ycSuaChua.MaNhanVien;
                yc.NgayLap = ycSuaChua.NgayLap;
                yc.NgayHoanTat = ycSuaChua.NgayHoanTat;
                yc.TrangThai = ycSuaChua.TrangThai;
                yc.TongTien = ycSuaChua.TongTien;

                // Lưu lại thay đổi vào cơ sở dữ liệu
                ktx.SubmitChanges();

                return true; // Cập nhật thành công
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong DAL (Cập nhật): " + ex.Message);
                return false;
            }
        }

        // Phương thức gọi thủ tục và trả về kết quả thống kê
        public List<ThongKeSuaChua> GetThongKeSuaChua(int nam, int thang)
        {
            // Gọi thủ tục usp_ThongKeSuaChua qua LINQ to SQL
            var result = ktx.ExecuteQuery<ThongKeSuaChua>(
                "EXEC usp_ThongKeSuaChua @Nam = {0}, @Thang = {1}", nam, thang).ToList();

            return result;
        }

        public List<ThongKeSuaChuaTanSuat> GetTanSuatHongHoc(int? nam, int? thang)
        {
            var result = ktx.ExecuteQuery<ThongKeSuaChuaTanSuat>(
                "EXEC usp_ThongKeTanSuatHongHoc @Nam = {0}, @Thang = {1}", nam, thang).ToList();

            return result;
        }


        public List<YeuCauSuaChua> GetYeuCauSuaChuaByCostAsc()
        {
            try
            {
                var result = ktx.YeuCauSuaChuas
                                .OrderBy(yc => yc.TongTien)  // Sắp xếp theo chi phí sửa chữa tăng dần
                                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong DAL: " + ex.Message);
                return new List<YeuCauSuaChua>();
            }
        }

        public List<YeuCauSuaChua> GetYeuCauSuaChuaByCostDesc()
        {
            try
            {
                var result = ktx.YeuCauSuaChuas
                                .OrderByDescending(yc => yc.TongTien)  // Sắp xếp theo chi phí sửa chữa giảm dần
                                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong DAL: " + ex.Message);
                return new List<YeuCauSuaChua>();
            }
        }
        public bool CapNhatTrangThaiDaHuy(string maYCSuaChua)
        {
            try
            {
                // Tìm yêu cầu sửa chữa theo mã
                var yc = ktx.YeuCauSuaChuas.FirstOrDefault(y => y.MaYCSuaChua == maYCSuaChua);
                if (yc != null)
                {
                    // Cập nhật trạng thái thành "Đã Huỷ"
                    yc.TrangThai = "Đã Huỷ";
                    ktx.SubmitChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                    return true; // Thành công
                }
                return false; // Không tìm thấy yêu cầu
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật trạng thái: " + ex.Message);
                return false; // Lỗi
            }
        }

        public List<YeuCauSuaChua> GetYeuCauSuaChuaByMaNV(string maNhanVien)
        {
            try
            {
                var result = ktx.YeuCauSuaChuas
                .Where(yc => yc.MaNhanVien.Trim() == maNhanVien.Trim())
                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong DAL: " + ex.Message);
                return new List<YeuCauSuaChua>();
            }
        }
        public List<YeuCauSuaChua> GetYeuCauSuaChuaByTrangThai(string trangThai)
        {
            try
            {
                var result = ktx.YeuCauSuaChuas
                    .Where(yc => yc.TrangThai.Trim() == trangThai.Trim()) // Lọc theo trạng thái
                    .ToList();

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong DAL: " + ex.Message);
                return new List<YeuCauSuaChua>();
            }
        }

        public List<YeuCauSuaChua> GetYeuCauSuaChuaByThangVaNam(int thang, int nam)
        {
            try
            {
                return ktx.YeuCauSuaChuas
                    .Where(yc => yc.NgayLap.HasValue &&
                                 yc.NgayLap.Value.Month == thang &&
                                 yc.NgayLap.Value.Year == nam)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong DAL (Tháng và Năm): " + ex.Message);
                return new List<YeuCauSuaChua>();
            }
        }


        public List<YeuCauSuaChua> GetYeuCauSuaChuaByNam(int nam)
        {
            try
            {
                return ktx.YeuCauSuaChuas
                    .Where(yc => yc.NgayLap.HasValue && yc.NgayLap.Value.Year == nam)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong DAL (Năm): " + ex.Message);
                return new List<YeuCauSuaChua>();
            }
        }

        public List<YeuCauSuaChua> GetYeuCauSuaChuaByNgayThangNam(DateTime ngayThangNam)
        {
            try
            {
                return ktx.YeuCauSuaChuas
                    .Where(yc => yc.NgayLap.HasValue && yc.NgayLap.Value.Date == ngayThangNam.Date)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong DAL (Ngày): " + ex.Message);
                return new List<YeuCauSuaChua>();
            }
        }



        public List<YeuCauSuaChua> GetYeuCauSuaChuaByMa(string maYCSuaChua)
        {
            try
            {
                var result = ktx.YeuCauSuaChuas
                .Where(yc => yc.MaYCSuaChua.Trim() == maYCSuaChua.Trim())
                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong DAL: " + ex.Message);
                return new List<YeuCauSuaChua>();
            }
        }


        public List<YeuCauSuaChua> GetAllMaYeuCauSuaChua()
        {
            try
            {
                // Lấy danh sách thực thể từ ngữ cảnh dữ liệu
                var danhSachYCS = ktx.YeuCauSuaChuas.ToList();

                // Chuyển đổi danh sách thực thể sang danh sách DTO
                return danhSachYCS.Select(yc => new YeuCauSuaChua
                {
                    MaYCSuaChua = yc.MaYCSuaChua,
                    MaNhanVien = yc.MaNhanVien,
                    NgayLap = yc.NgayLap,
                    NgayHoanTat = yc.NgayHoanTat,
                    TrangThai = yc.TrangThai,
                    TongTien = yc.TongTien
                }).ToList();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("Lỗi khi truy vấn danh sách yêu cầu sửa chữa: " + ex.Message);
                return new List<YeuCauSuaChua>(); // Trả về danh sách rỗng nếu xảy ra lỗi
            }
        }

        public List<ThongTinYeuCauSuaChua> GetChiTietYCSC(string maYCSuaChua)
        {
            try
            {
                // Truy vấn các chi tiết yêu cầu sửa chữa từ cơ sở dữ liệu
                var query = from ctycs in ktx.CT_YeuCauSuaChuas
                            join tt in ktx.TrangThietBis on ctycs.MaThietBi equals tt.MaThietBi
                            join cttb in ktx.CT_TrangThietBis on new { ctycs.MaThietBi, ctycs.MaPhong } equals new { cttb.MaThietBi, cttb.MaPhong }
                            where ctycs.MaYCSuaChua == maYCSuaChua
                            select new ThongTinYeuCauSuaChua
                            {
                                MaPhong = ctycs.MaPhong,
                                MaThietBi = ctycs.MaThietBi,
                                TenThietBi = tt.TenThietBi,  // Lấy tên thiết bị từ bảng TrangThietBi
                                SoLuong = ctycs.SoLuong,
                                TinhTrang = ctycs.TinhTrang,
                                
                                PhiSuaChua = ctycs.PhiSuaChua
                            };

                // Trả về danh sách các chi tiết yêu cầu sửa chữa
                return query.ToList();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("Lỗi khi truy vấn dữ liệu: " + ex.Message);
                return null; // Trả về null nếu có lỗi
            }
        }


        public ThongTinYeuCauSuaChua GetYeuCauSuaChuaDetails(string maYCSuaChua)
        {
            try
            {
                // Truy vấn thông tin yêu cầu sửa chữa kết hợp với chi tiết yêu cầu sửa chữa
                var query = from yc in ktx.YeuCauSuaChuas
                            where yc.MaYCSuaChua == maYCSuaChua
                            select new ThongTinYeuCauSuaChua
                            {
                                MaYCSuaChua = yc.MaYCSuaChua,
                                MaNhanVien = yc.MaNhanVien,
                                NgayLap = yc.NgayLap,
                                NgayHoanTat = yc.NgayHoanTat,
                                TrangThai = yc.TrangThai,
                                TongTien = yc.TongTien,
                                
                            };

                // Chuyển đổi kết quả truy vấn thành danh sách
                return query.SingleOrDefault();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("Lỗi khi truy vấn: " + ex.Message);
                return null;
            }
        }
        // Hàm lấy danh sách yêu cầu sửa chữa
        public List<YeuCauSuaChua> LayDanhSachYeuCauSuaChua()
        {
            try
            {
                // Lấy danh sách thực thể từ ngữ cảnh dữ liệu, chỉ lấy các bản ghi có TrangThai = 0 (hoặc điều kiện tùy chỉnh)
                var danhSachYeuCau = ktx.YeuCauSuaChuas
                                       .Where(yc => yc.TrangThai != "Đã Huỷ") // Điều kiện lọc (nếu cần)
                                       .ToList();

                // Chuyển đổi sang danh sách DTO
                return danhSachYeuCau.Select(yc => new YeuCauSuaChua
                {
                    MaYCSuaChua = yc.MaYCSuaChua,
                    MaNhanVien = yc.MaNhanVien,
                    NgayLap = yc.NgayLap,
                    NgayHoanTat = yc.NgayHoanTat,
                    TrangThai = yc.TrangThai,
                    TongTien = yc.TongTien
                }).ToList();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("Lỗi khi lấy danh sách yêu cầu sửa chữa: " + ex.Message);
                return new List<YeuCauSuaChua>();
            }
        }
        //thông tin nhân viên từ mã nhân viên 
        public NhanVien GetThongTinNhanVien(string maNhanVien)
        {
            try
            {
                // Truy vấn thông tin nhân viên từ cơ sở dữ liệu
                var nhanVien = ktx.NhanViens // ktx là đối tượng DbContext
                                  .Where(nv => nv.MaNhanVien == maNhanVien)
                                  .FirstOrDefault(); // Lấy bản ghi đầu tiên hoặc null nếu không có

                return nhanVien; // Trả về đối tượng nhân viên
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("Lỗi khi lấy thông tin nhân viên: " + ex.Message);
                return null; // Trả về null trong trường hợp lỗi
            }
        }

        // Sử dụng DTO YeuCauSuaChua thay vì truyền tham số từng cái
        public bool AddYeuCauSuaChua(YeuCauSuaChua ycSuaChua)
        {
            try
            {
                // Gọi hàm GetMaYCSuaChua để lấy mã yêu cầu sửa chữa tự động
                ycSuaChua.MaYCSuaChua = GetMaYCSuaChua();

                // Thêm yêu cầu sửa chữa vào bảng YeuCauSuaChua
                ktx.YeuCauSuaChuas.InsertOnSubmit(ycSuaChua);
                ktx.SubmitChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm yêu cầu sửa chữa: " + ex.Message);
                return false;
            }
        }


        public string GetMaYCSuaChua()
        {
            try
            {
                // Lấy mã yêu cầu sửa chữa mới nhất từ cơ sở dữ liệu
                var maxMaYC = ktx.YeuCauSuaChuas
                    .Where(y => y.MaYCSuaChua.StartsWith("YC")) // Lọc theo các mã bắt đầu bằng "YC"
                    .OrderByDescending(y => y.MaYCSuaChua) // Sắp xếp theo mã giảm dần
                    .FirstOrDefault(); // Lấy mã yêu cầu sửa chữa mới nhất

                // Nếu không có dữ liệu nào thì trả về mã mặc định "YC001"
                if (maxMaYC == null)
                {
                    return "YC001";
                }

                // Lấy số sau chữ "YC" và cộng 1 để tạo mã mới
                int maxNumber = int.Parse(maxMaYC.MaYCSuaChua.Substring(2)); // Lấy phần số của mã
                string newMaYC = "YC" + (maxNumber + 1).ToString("D3"); // Tạo mã mới với số tự động tăng

                return newMaYC;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy mã yêu cầu sửa chữa: " + ex.Message);
                return string.Empty; // Trả về chuỗi rỗng nếu có lỗi
            }
        }

        // Sử dụng DTO CT_YeuCauSuaChua thay vì truyền tham số từng cái
        public bool AddCT_YeuCauSuaChua(CT_YeuCauSuaChua ctYeuCauSuaChua)
        {
            try
            {
                // Thêm chi tiết yêu cầu sửa chữa vào bảng CT_YeuCauSuaChua
                ktx.CT_YeuCauSuaChuas.InsertOnSubmit(ctYeuCauSuaChua);
                ktx.SubmitChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm chi tiết yêu cầu sửa chữa: " + ex.Message);
                return false;
            }
        }

        public bool AddYeuCauSuaChuaVaChiTiet(YeuCauSuaChua ycSuaChua, List<CT_YeuCauSuaChua> ctYeuCauSuaChuas)
        {
            try
            {
                // Lấy mã yêu cầu sửa chữa lớn nhất từ cơ sở dữ liệu
                var maxMaYC = ktx.YeuCauSuaChuas
                    .Where(y => y.MaYCSuaChua.StartsWith("YC")) // Lọc theo các mã bắt đầu bằng "YC"
                    .OrderByDescending(y => y.MaYCSuaChua)
                    .FirstOrDefault();

                string newMaYC = "YC001"; // Mặc định là YC001 nếu chưa có dữ liệu

                if (maxMaYC != null)
                {
                    // Lấy số sau chữ YC và cộng 1
                    int maxNumber = int.Parse(maxMaYC.MaYCSuaChua.Substring(2)); // Lấy phần số của mã
                    newMaYC = "YC" + (maxNumber + 1).ToString("D3"); // Tạo mã mới với số tự động tăng
                }

                // Gán mã yêu cầu sửa chữa mới cho ycSuaChua
                ycSuaChua.MaYCSuaChua = newMaYC;

                // Thêm yêu cầu sửa chữa vào bảng YeuCauSuaChua
                ktx.YeuCauSuaChuas.InsertOnSubmit(ycSuaChua);

                // Lặp qua danh sách chi tiết yêu cầu sửa chữa và thêm vào bảng CT_YeuCauSuaChua
                foreach (var ctYeuCauSuaChua in ctYeuCauSuaChuas)
                {
                    ctYeuCauSuaChua.MaYCSuaChua = newMaYC; // Gán mã yêu cầu sửa chữa cho chi tiết
                    ktx.CT_YeuCauSuaChuas.InsertOnSubmit(ctYeuCauSuaChua); // Thêm chi tiết vào bảng CT_YeuCauSuaChua
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                ktx.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm yêu cầu sửa chữa và chi tiết: " + ex.Message);
                return false;
            }
        }

    }
}
