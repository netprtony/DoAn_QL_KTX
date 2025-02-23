using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_ThanhToan
    {
        DA_QL_KTXDataContext ktx = new DA_QL_KTXDataContext();

        public List<ThanhToanDienNuoc> GetThanhToanDienNuocByThangVaNam(int thang, int nam)
        {
            try
            {
                return ktx.ThanhToanDienNuocs
                    .Where(tt => tt.NgayLap.HasValue &&
                                 tt.NgayLap.Value.Month == thang &&
                                 tt.NgayLap.Value.Year == nam)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong DAL (Tháng và Năm): " + ex.Message);
                return new List<ThanhToanDienNuoc>();
            }
        }

        public List<ThanhToanDienNuoc> GetThanhToanDienNuocByNam(int nam)
        {
            try
            {
                return ktx.ThanhToanDienNuocs
                    .Where(tt => tt.NgayLap.HasValue && tt.NgayLap.Value.Year == nam)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong DAL (Năm): " + ex.Message);
                return new List<ThanhToanDienNuoc>();
            }
        }

        public List<ThanhToanDienNuoc> GetThanhToanDienNuocByNgayThangNam(DateTime ngayThangNam)
        {
            try
            {
                return ktx.ThanhToanDienNuocs
                    .Where(tt => tt.NgayLap.HasValue && tt.NgayLap.Value.Date == ngayThangNam.Date)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong DAL (Ngày): " + ex.Message);
                return new List<ThanhToanDienNuoc>();
            }
        }


        public List<string> LayDanhSachMaThanhToan()
        {
            try
            {
                // Lấy danh sách mã thanh toán từ bảng ThanhToanDienNuoc
                var danhSachMaThanhToan = ktx.ThanhToanDienNuocs
                                             .Select(tt => tt.MaThanhToanDN)
                                             .ToList();

                return danhSachMaThanhToan;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách mã thanh toán: " + ex.Message);
                return new List<string>(); // Trả về danh sách rỗng nếu có lỗi
            }
        }


        public List<ThanhToanDienNuoc> LayDanhSachTheoMaThanhToan(string maThanhToan)
        {
            try
            {
                // Truy vấn danh sách thanh toán theo mã thanh toán
                var danhSachThanhToan = ktx.ThanhToanDienNuocs
                                           .Where(tt => tt.MaThanhToanDN == maThanhToan)
                                           .ToList();

                return danhSachThanhToan;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lọc danh sách thanh toán: " + ex.Message);
                return new List<ThanhToanDienNuoc>(); // Trả về danh sách rỗng nếu có lỗi
            }
        }

        // Hàm lọc danh sách ThanhToanDienNuoc theo mã nhân viên
        public List<ThanhToanDienNuoc> LayDanhSachTheoMaNhanVien(string maNhanVien)
        {
            try
            {
                // Truy vấn bảng ThanhToanDienNuoc lọc theo MaNhanVien
                var danhSachThanhToan = ktx.ThanhToanDienNuocs
                                           .Where(tt => tt.MaNhanVien == maNhanVien)
                                           .ToList(); // Chuyển kết quả thành danh sách

                return danhSachThanhToan; // Trả về danh sách các bản ghi
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách thanh toán: " + ex.Message);
                return new List<ThanhToanDienNuoc>(); // Trả về danh sách rỗng nếu có lỗi
            }
        }


        public string LayTenNhanVienTuMa(string maNhanVien)
        {
            try
            {
                // Truy vấn thông tin nhân viên từ bảng NhanVien theo mã nhân viên
                var nhanVien = ktx.NhanViens
                    .Where(nv => nv.MaNhanVien == maNhanVien)
                    .FirstOrDefault(); // Tìm nhân viên theo mã nhân viên

                if (nhanVien == null)
                {
                    return null; // Nếu không tìm thấy nhân viên, trả về null
                }

                return nhanVien.HoTen; // Trả về tên nhân viên
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy tên nhân viên: " + ex.Message);
                return null; // Trả về null nếu có lỗi
            }
        }

        public string LayTenSinhVienTuMa(string maSinhVien)
        {
            try
            {
                // Truy vấn thông tin sinh viên từ bảng SinhVien theo mã sinh viên
                var sinhVien = ktx.SinhViens
                    .Where(sv => sv.MaSinhVien == maSinhVien)
                    .FirstOrDefault(); // Tìm sinh viên theo mã sinh viên

                if (sinhVien == null)
                {
                    return null; // Nếu không tìm thấy sinh viên, trả về null
                }

                return sinhVien.HoTen; // Trả về tên sinh viên
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy tên sinh viên: " + ex.Message);
                return null; // Trả về null nếu có lỗi
            }
        }


        public PhongChiTiet LayChiSoDienNuocTuMaPhong(string maPhong)
        {
            try
            {
                // Truy vấn thông tin phòng từ bảng Phong
                var phong = ktx.Phongs
                    .Where(p => p.MaPhong == maPhong)
                    .FirstOrDefault(); // Tìm phòng theo mã phòng

                if (phong == null)
                {
                    return null; // Nếu không tìm thấy phòng, trả về null
                }

                // Truy vấn thông tin chỉ số điện nước từ bảng ChiSoDienNuoc
                var chiSoDienNuoc = ktx.ChiSoDienNuocs
                    .Where(cs => cs.MaDienNuoc == phong.MaDienNuoc)
                    .FirstOrDefault(); // Tìm thông tin chỉ số điện nước theo MaDienNuoc

                if (chiSoDienNuoc == null)
                {
                    return null; // Nếu không tìm thấy chỉ số điện nước, trả về null
                }

                // Tạo đối tượng PhongChiTiet để trả về thông tin
                PhongChiTiet phongChiTiet = new PhongChiTiet
                {

                    ChiSoDien = chiSoDienNuoc.ChiSoDien,
                    ChiSoNuoc = chiSoDienNuoc.ChiSoNuoc,

                };

                return phongChiTiet; // Trả về đối tượng PhongChiTiet
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy thông tin chỉ số điện nước từ mã phòng: " + ex.Message);
                return null; // Trả về null nếu có lỗi
            }
        }

        // Hàm lấy chi tiết từ bảng CT_ThanhToanDienNuoc dựa trên MaThanhToanDN
        public List<CT_ThanhToanDienNuoc> LayChiTietCTThanhToanDienNuoc(string maThanhToanDN)
        {
            try
            {
                // Lấy danh sách chi tiết thanh toán điện nước từ bảng CT_ThanhToanDienNuoc theo MaThanhToanDN
                var chiTietList = ktx.CT_ThanhToanDienNuocs
                    .Where(ct => ct.MaThanhToanDN == maThanhToanDN)
                    .ToList(); // Lấy toàn bộ danh sách thực thể

                // Chuyển đổi danh sách thực thể sang danh sách DTO
                return chiTietList.Select(ct => new CT_ThanhToanDienNuoc
                {
                    MaPhong = ct.MaPhong,
                    MaThanhToanDN = ct.MaThanhToanDN,
                    ChiSoDienCu = ct.ChiSoDienCu,
                    ChiSoNuocCu = ct.ChiSoNuocCu,
                    ChiSoDienMoi = ct.ChiSoDienMoi,
                    ChiSoNuocMoi = ct.ChiSoNuocMoi,
                    SoDienTieuThu = ct.SoDienTieuThu,
                    SoNuocTieuThu = ct.SoNuocTieuThu,
                    DonGiaDien = ct.DonGiaDien,
                    DonGiaNuoc = ct.DonGiaNuoc,
                    NgayBatDau = ct.NgayBatDau,
                    NgayKetThuc = ct.NgayKetThuc
                }).ToList(); // Trả về danh sách DTO
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("Lỗi khi lấy chi tiết thanh toán điện nước: " + ex.Message);
                return new List<CT_ThanhToanDienNuoc>(); // Trả về danh sách rỗng nếu có lỗi
            }
        }

        public List<ThanhToanDienNuoc> LayDanhSachThanhToanDienNuoc()
        {
            try
            {
                // Lấy danh sách thực thể từ ngữ cảnh dữ liệu
                var danhSachTTDN = ktx.ThanhToanDienNuocs.ToList();

                // Chuyển đổi danh sách thực thể sang danh sách DTO
                return danhSachTTDN.Select(tt => new ThanhToanDienNuoc
                {
                    MaThanhToanDN = tt.MaThanhToanDN,
                    MaNhanVien = tt.MaNhanVien,
                    MaSinhVien = tt.MaSinhVien,
                    NgayLap = tt.NgayLap,
                    TongTien = tt.TongTien,
                    TrangThai = tt.TrangThai
                }).ToList();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("Lỗi khi truy vấn danh sách thanh toán điện nước: " + ex.Message);
                return new List<ThanhToanDienNuoc>(); // Trả về danh sách rỗng nếu xảy ra lỗi
            }
        }

        public ThanhToanDienNuoc LayChiTietThanhToanDienNuoc(string maThanhToanDN)
        {
            try
            {
                // Truy vấn dữ liệu từ cơ sở dữ liệu
                var thanhToan = ktx.ThanhToanDienNuocs
                                    .Where(tt => tt.MaThanhToanDN == maThanhToanDN)
                                    .FirstOrDefault();

                if (thanhToan != null)
                {
                    // Khởi tạo và ánh xạ dữ liệu vào DTO
                    return new ThanhToanDienNuoc
                    {
                        MaThanhToanDN = thanhToan.MaThanhToanDN,
                        MaNhanVien = thanhToan.MaNhanVien,
                        MaSinhVien = thanhToan.MaSinhVien,
                        NgayLap = thanhToan.NgayLap,
                        TongTien = thanhToan.TongTien,
                        TrangThai = thanhToan.TrangThai
                    };
                }

                // Trả về null nếu không tìm thấy thanh toán
                return null;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                Console.WriteLine("Lỗi khi lấy chi tiết thanh toán điện nước: " + ex.Message);
                return null; // Trả về null nếu có lỗi
            }
        }



        public ThongTinDienNuoc LayThongTinPhong(string maPhong)
        {
            var thongTin = (from dk in ktx.DangKyPhongs
                            join sv in ktx.SinhViens on dk.MaSinhVien equals sv.MaSinhVien
                            join p in ktx.Phongs on dk.MaPhong equals p.MaPhong
                            join cs in ktx.ChiSoDienNuocs on p.MaDienNuoc equals cs.MaDienNuoc
                            join lp in ktx.LoaiPhongs on p.MaLoaiPhong equals lp.MaLoaiPhong
                            where dk.MaPhong == maPhong && sv.TruongPhong == true
                            select new
                            {
                                SoLuongSV = p.SoLuongSinhVienToiDa,
                                MaSV_TP = sv.MaSinhVien,
                                HoTen_TP = sv.HoTen,
                                Email_TP = sv.Email,
                                ChiSoDienCu = cs.ChiSoDien,
                                ChiSoNuocCu = cs.ChiSoNuoc,
                                DonGiaDien = cs.DonGiaDien,
                                DonGiaNuoc = cs.DonGiaNuoc,
                                MaDienNuoc = cs.MaDienNuoc,
                                TenLoaiPhong = lp.TenLoaiPhong
                            }).FirstOrDefault();

            if (thongTin != null)
            {
                return new ThongTinDienNuoc
                {
                    SoLuongSV = thongTin.SoLuongSV,
                    MaSV_TP = thongTin.MaSV_TP,
                    HoTen_TP = thongTin.HoTen_TP,
                    Email_TP = thongTin.Email_TP,
                    ChiSoDienCu = thongTin.ChiSoDienCu,
                    ChiSoNuocCu = thongTin.ChiSoNuocCu,
                    DonGiaDien = thongTin.DonGiaDien,
                    DonGiaNuoc = thongTin.DonGiaNuoc,
                    MaDienNuoc = thongTin.MaDienNuoc,
                    TenLoaiPhong = thongTin.TenLoaiPhong
                };
            }
            else
            {
                return null;
            }
        }

        // Hàm tạo mã thanh toán mới tự động với định dạng DNxxxx
        private string TaoMaThanhToanMoi()
        {
            // Lấy mã thanh toán lớn nhất hiện tại từ bảng ThanhToanDienNuoc có dạng DNxxxx
            var maxMaThanhToan = ktx.ThanhToanDienNuocs
                .Where(t => t.MaThanhToanDN.StartsWith("DN"))
                .OrderByDescending(t => t.MaThanhToanDN)
                .Select(t => t.MaThanhToanDN)
                .FirstOrDefault();

            // Kiểm tra nếu chưa có mã thanh toán nào (maxMaThanhToan là null)
            if (maxMaThanhToan == null)
            {
                // Nếu chưa có mã thanh toán, trả về DN0001
                return "DN0001";
            }
            else
            {
                // Nếu đã có mã thanh toán, chuyển phần số của mã thành số nguyên và tăng thêm 1
                int newNumber = int.Parse(maxMaThanhToan.Substring(2)) + 1;

                // Định dạng mã mới với "DN" + 4 chữ số (vd: "DN0001")
                return "DN" + newNumber.ToString("D4");
            }
        }

        // Hàm thêm hóa đơn điện nước
        public bool ThemHoaDonDienNuoc(ThanhToanDienNuoc hoaDon, List<CT_ThanhToanDienNuoc> chiTietHoaDon)
        {
            try
            {
                // Gọi hàm tạo mã thanh toán mới và gán vào hóa đơn
                hoaDon.MaThanhToanDN = TaoMaThanhToanMoi();

                // Thêm thông tin hóa đơn vào bảng ThanhToanDienNuoc
                ktx.ThanhToanDienNuocs.InsertOnSubmit(hoaDon);
                ktx.SubmitChanges(); // Lưu thay đổi để lấy ID của hóa đơn mới thêm

                // Duyệt qua từng chi tiết hóa đơn và thêm vào bảng CT_ThanhToanDienNuoc
                foreach (var ct in chiTietHoaDon)
                {
                    // Gán mã hóa đơn cho mỗi chi tiết
                    ct.MaThanhToanDN = hoaDon.MaThanhToanDN; // Sử dụng ID từ hoaDon vừa được thêm

                    // Thêm chi tiết hóa đơn vào bảng CT_ThanhToanDienNuoc
                    ktx.CT_ThanhToanDienNuocs.InsertOnSubmit(ct);
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                ktx.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm hóa đơn điện nước: " + ex.Message);
                return false;
            }
        }

        public string LayMaDienNuocTheoPhong(string maPhong)
        {
            try
            {
                // Truy vấn bảng Phong để lấy mã điện nước liên kết với mã phòng
                var phong = ktx.Phongs.FirstOrDefault(p => p.MaPhong == maPhong);

                if (phong != null)
                {
                    // Trả về mã điện nước của phòng
                    return phong.MaDienNuoc;
                }
                else
                {
                    Console.WriteLine("Không tìm thấy phòng với mã " + maPhong);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy mã điện nước: " + ex.Message);
                return null;
            }
        }

        public bool CapNhatChiSoDienNuoc(string maDienNuoc, int chiSoDien, int chiSoNuoc)
        {
            try
            {
                // Truy vấn bảng ChiSoDienNuoc để tìm mã điện nước cần cập nhật
                var chiSo = ktx.ChiSoDienNuocs.FirstOrDefault(c => c.MaDienNuoc == maDienNuoc);

                if (chiSo != null)
                {
                    // Cập nhật giá trị mới của chỉ số điện và chỉ số nước
                    chiSo.ChiSoDien = chiSoDien;
                    chiSo.ChiSoNuoc = chiSoNuoc;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    ktx.SubmitChanges();
                    Console.WriteLine("Cập nhật chỉ số điện nước thành công.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Không tìm thấy mã điện nước " + maDienNuoc);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật chỉ số điện nước: " + ex.Message);
                return false;
            }
        }
        public List<ThongKeDienNuoc> ThongKeDienNuoc(int thang, int nam)
        {
            List<ThongKeDienNuoc> result = new List<ThongKeDienNuoc>();

            try
            {
                // Gọi thủ tục và log thông tin
                Console.WriteLine("Gọi thủ tục usp_ThongKeDienNuoc với Tháng = " + thang + ", Năm = " + nam);

                var query = ktx.ExecuteQuery<ThongKeDienNuoc>(
                    "EXEC usp_ThongKeDienNuoc @Thang = {0}, @Nam = {1}",
                    thang, nam).ToList();

                // Log số lượng bản ghi trả về
                Console.WriteLine("Số bản ghi trả về: " + query.Count);

                result.AddRange(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi gọi thủ tục: " + ex.Message);
            }

            return result;
        }





    }
}
