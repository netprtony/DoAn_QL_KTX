using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_ViPham
    {
        DA_QL_KTXDataContext ktx = new DA_QL_KTXDataContext();

        // Hàm load tất cả danh sách loại nội quy
        public List<LoaiNoiQuy> GetAllLoaiNoiQuy()
        {
            try
            {
                // Truy vấn danh sách tất cả loại nội quy từ cơ sở dữ liệu, chỉ lấy những bản ghi có TrangThai = 0
                var danhSachLoaiNoiQuy = ktx.LoaiNoiQuys.Where(lnq => lnq.TrangThai == 0).ToList();
                return danhSachLoaiNoiQuy;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có và trả về danh sách rỗng
             //   Console.WriteLine("Lỗi khi lấy danh sách loại nội quy: " + ex.Message);
                return new List<LoaiNoiQuy>();
            }
        }

        // Hàm lọc danh sách vi phạm theo mã sinh viên
        public List<ViPham> GetViPhamByMaSinhVien(string maSinhVien)
        {
            try
            {
                var danhSachViPham = ktx.ViPhams
                    .Where(vp => vp.MaSinhVien == maSinhVien)
                    .ToList();
                return danhSachViPham;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có và trả về danh sách rỗng
                return new List<ViPham>();
            }
        }

        // Hàm lọc danh sách vi phạm theo tên loại nội quy
        public List<ViPham> GetViPhamByTenLoaiNoiQuy(string tenLoaiNoiQuy)
        {
            try
            {
                var danhSachViPham = (from vp in ktx.ViPhams
                                      join nq in ktx.NoiQuys on vp.MaNoiQuy equals nq.MaNoiQuy
                                      join lnq in ktx.LoaiNoiQuys on nq.MaLoaiNQ equals lnq.MaLoaiNQ
                                      where lnq.TenLoaiNQ == tenLoaiNoiQuy 
                                      select vp).ToList();
                return danhSachViPham;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có và trả về danh sách rỗng
                return new List<ViPham>();
            }
        }



        public ViPham XemChiTietViPham(string maViPham)
        {
            try
            {
                // Truy vấn thông tin phiếu vi phạm theo mã ViPham
                var viPham = ktx.ViPhams
                                .Where(vp => vp.MaViPham == maViPham)
                                .FirstOrDefault(); // Lấy bản ghi đầu tiên (hoặc null nếu không tìm thấy)

                // Kiểm tra nếu tìm thấy phiếu vi phạm
                if (viPham != null)
                {
                    return viPham; // Trả về chi tiết phiếu vi phạm
                }
                else
                {
                  Console.WriteLine("Không tìm thấy phiếu vi phạm với mã: " + maViPham);
                    return null; // Trả về null nếu không tìm thấy phiếu vi phạm
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                Console.WriteLine($"Đã xảy ra lỗi khi truy vấn chi tiết phiếu vi phạm: {ex.Message}");
                return null;
            }
        }

        public List<ViPham> LoadDanhSachViPham()
        {
            try
            {
                // Lấy danh sách thực thể từ ngữ cảnh dữ liệu
                var danhSachViPham = ktx.ViPhams
                                        .ToList();

                // Chuyển đổi sang danh sách DTO
                return danhSachViPham.Select(vp => new ViPham
                {
                    MaViPham = vp.MaViPham,
                    MaNoiQuy = vp.MaNoiQuy,
                    MaNhanVien = vp.MaNhanVien,
                    MaSinhVien = vp.MaSinhVien,
                    MoTa = vp.MoTa,
                    NgayViPham = vp.NgayViPham,
                    TrangThai = vp.TrangThai
                    
                }).ToList();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine($"Lỗi khi tải danh sách vi phạm: {ex.Message}");
                return new List<ViPham>(); // Trả về danh sách rỗng trong trường hợp lỗi
            }
        }

        public bool ThemViPham(ViPham viPham)
        {
            try
            {
                // Lấy mã ViPham hiện tại có giá trị lớn nhất
                var maxMaViPham = ktx.ViPhams
                    .Where(v => v.MaViPham.StartsWith("VP"))
                    .OrderByDescending(v => v.MaViPham)
                    .FirstOrDefault()?.MaViPham;

                Console.WriteLine($"MaxMaViPham hiện tại: {maxMaViPham}");

                // Tính toán mã ViPham mới
                int newId = 1;
                if (maxMaViPham != null)
                {
                    newId = int.Parse(maxMaViPham.Substring(2)) + 1;
                }
                string maViPhamMoi = $"VP{newId:D3}";

                Console.WriteLine($"Mã Vi Phạm mới: {maViPhamMoi}");

                // Tạo đối tượng ViPham từ DTO và thiết lập mã ViPham mới
                var viPhamEntity = new ViPham()
                {
                    MaViPham = maViPhamMoi,
                    MaNoiQuy = viPham.MaNoiQuy,
                    MaNhanVien = viPham.MaNhanVien,
                    MaSinhVien = viPham.MaSinhVien,
                    MoTa = viPham.MoTa,
                    NgayViPham = viPham.NgayViPham,
                    TrangThai = viPham.TrangThai // Thêm trường TrangThai vào
                };

                Console.WriteLine("Đối tượng ViPhamEntity được tạo thành công:");
                Console.WriteLine($"MaViPham: {viPhamEntity.MaViPham}");
                Console.WriteLine($"MaNoiQuy: {viPhamEntity.MaNoiQuy}");
                Console.WriteLine($"MaNhanVien: {viPhamEntity.MaNhanVien}");
                Console.WriteLine($"MaSinhVien: {viPhamEntity.MaSinhVien}");
                Console.WriteLine($"MoTa: {viPhamEntity.MoTa}");
                Console.WriteLine($"NgayViPham: {viPhamEntity.NgayViPham}");
                Console.WriteLine($"TrangThai: {viPhamEntity.TrangThai}");

                // Thêm phiếu vi phạm vào cơ sở dữ liệu
                ktx.ViPhams.InsertOnSubmit(viPhamEntity);
                ktx.SubmitChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                Console.WriteLine("Lưu phiếu vi phạm thành công.");
                return true; // Thành công
            }
            catch (FormatException fEx)
            {
                Console.WriteLine($"Lỗi định dạng dữ liệu: {fEx.Message}");
                return false;
            }
            catch (InvalidOperationException ioEx)
            {
                Console.WriteLine($"Lỗi thao tác không hợp lệ: {ioEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi chi tiết
                Console.WriteLine("Lỗi khi thêm phiếu vi phạm:");
                Console.WriteLine($"Loại lỗi: {ex.GetType().FullName}");
                Console.WriteLine($"Thông điệp lỗi: {ex.Message}");
                Console.WriteLine($"Chi tiết lỗi: {ex.StackTrace}");
                return false; // Thất bại
            }
        }


        public List<ThongKeLoaiViPham> ThongKeViPhamTheoThangNam(int thang, int nam)
        {
            try
            {
                var thongKe = ktx.ViPhams
                    .Where(vp => ((DateTime)vp.NgayViPham).Month == thang && ((DateTime)vp.NgayViPham).Year == nam)
                    .GroupBy(vp => new
                    {
                        TenLoaiNoiQuy = vp.NoiQuy.LoaiNoiQuy.TenLoaiNQ,
                        TenNoiQuy = vp.NoiQuy.TenNoiQuy
                    })
                    .Select(g => new ThongKeLoaiViPham
                    {
                        TenLoaiNoiQuy = g.Key.TenLoaiNoiQuy,
                        TenNoiQuy = g.Key.TenNoiQuy,
                        SoluongViPham = g.Count()
                    })
                    .OrderBy(x => x.TenLoaiNoiQuy)
                    .ThenBy(x => x.TenNoiQuy)
                    .ToList();

                return thongKe;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thống kê vi phạm: {ex.Message}");
                return null;
            }
        }

        // Thống kê số lượng vi phạm theo phòng trong tháng và năm
        public List<ThongKeViPhamTheoPhong> ThongKeViPhamTheoPhong(int thang, int nam)
        {
            try
            {
                // Truy vấn dữ liệu và thống kê
                var thongKe = (from dk in ktx.DangKyPhongs
                               join sv in ktx.SinhViens on dk.MaSinhVien equals sv.MaSinhVien
                               join p in ktx.Phongs on dk.MaPhong equals p.MaPhong
                               join vp in ktx.ViPhams on sv.MaSinhVien equals vp.MaSinhVien
                               where vp.NgayViPham.HasValue &&
                                     vp.NgayViPham.Value.Month == thang &&
                                     vp.NgayViPham.Value.Year == nam
                               group vp by new
                               {
                                   TenLoaiViPham = vp.NoiQuy.LoaiNoiQuy.TenLoaiNQ,  // Nhóm theo loại vi phạm
                                   TenViPham = vp.NoiQuy.TenNoiQuy,                  // Nhóm theo tên vi phạm
                                   MaPhong = p.MaPhong                               // Nhóm theo phòng
                               } into g
                               select new ThongKeViPhamTheoPhong
                               {
                                   TenLoaiViPham = g.Key.TenLoaiViPham,
                                   TenViPham = g.Key.TenViPham,
                                   SoPhong = g.Key.MaPhong,
                                   SoLuongViPham = g.Count() // Đếm số lượng vi phạm
                               })
                               .OrderBy(tk => tk.SoPhong)
                               .ThenBy(tk => tk.TenLoaiViPham)
                               .ThenBy(tk => tk.TenViPham)
                               .ToList();

                return thongKe;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thống kê vi phạm theo phòng: {ex.Message}");
                return null;
            }
        }
        // Hàm lấy tất cả mã phòng
        public List<Phong> GetAllMaPhong()
        {
            try
            {
                // Sử dụng LINQ to SQL để lấy danh sách mã phòng từ bảng 'Phong'
                var danhSachPhong = ktx.Phongs.Where(p => p.TrangThai == "Active").ToList(); // Lọc theo trạng thái hoặc điều kiện khác nếu cần

                // Chuyển đổi sang danh sách DTO
                return danhSachPhong.Select(p => new Phong
                {
                    MaPhong = p.MaPhong,
                    MaLoaiPhong = p.MaLoaiPhong,
                    SoLuongSinhVienToiDa = p.SoLuongSinhVienToiDa,
                    TrangThai = p.TrangThai,
                    MaDienNuoc = p.MaDienNuoc // Khóa ngoại
                }).ToList();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                Console.WriteLine($"Lỗi khi lấy dữ liệu mã phòng: {ex.Message}");
                return new List<Phong>();  // Trả về danh sách rỗng trong trường hợp có lỗi
            }
        }


    }
}
