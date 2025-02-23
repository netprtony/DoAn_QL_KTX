using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using DTO;
namespace DAL
{
    public class DAL_QL_LuuTru
    {
       DA_QL_KTXDataContext ktx = new DA_QL_KTXDataContext();

        public DAL_QL_LuuTru() { }

        public List<SinhVien> GetSinhVien()
        {
            return ktx.SinhViens.ToList();
        }
        public List<DangKyPhong> GetTheoMaSV(string maSV)
        {
            return ktx.DangKyPhongs.Where(sv => sv.MaSinhVien == maSV).ToList();
        }

        public int DemSoSV(string maPhong)
        {
            try
            {
                int count = ktx.DangKyPhongs.Count(dkp => dkp.MaPhong == maPhong);
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi đếm số lượng sinh viên" + ex.Message);
                return -1;
            }
        }

        public List<DangKyPhong> GetAllDangKyPhong()
        {
            try
            {
                // Lấy tất cả phiếu đăng ký phòng từ cơ sở dữ liệu
                return ktx.DangKyPhongs.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy tất cả phiếu đăng ký: " + ex.Message);
                return new List<DangKyPhong>(); // Trả về danh sách rỗng nếu có lỗi
            }
        }

        public List<DangKyPhong> LayThongTinSinhVien(string maPhong)
        {
            // Lấy danh sách đăng ký phòng theo mã phòng
            var danhSachDangKyPhong = ktx.DangKyPhongs
                .Where(dk => dk.MaPhong == maPhong)
                .ToList();

            // Thêm thông tin HinhNhanDien từ bảng SinhVien
            foreach (var dk in danhSachDangKyPhong)
            {
                var sinhVien = ktx.SinhViens
                    .FirstOrDefault(sv => sv.MaSinhVien == dk.MaSinhVien);

                if (sinhVien != null)
                {
                    dk.HinhNhanDien = sinhVien.HinhNhanDien;
                }
            }

            return danhSachDangKyPhong;
        }

        public decimal LayDonGia(int MaLoaiPhong)
        {
            try
            {
                // Tìm phòng theo mã
                var Lphong = ktx.LoaiPhongs.FirstOrDefault(p => p.MaLoaiPhong == MaLoaiPhong);

                // Nếu tìm thấy phòng, trả về đơn giá
                if (Lphong != null)
                {
                    return Lphong.DonGiaPhong?? 0;
                }
                else
                {
                    throw new ArgumentException("Phòng không tồn tại.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy đơn giá: " + ex.Message);
                return -1; // Trả về -1 để biểu thị lỗi
            }
        }
        public bool ThemSinhVien(SinhVien sv)
        {
            try
            {
               ktx.SinhViens.InsertOnSubmit(sv);
                ktx.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CapNhatSinhVien(SinhVien sv)
        {
            try
            {
                var sinhvien = ktx.SinhViens.FirstOrDefault(a => a.MaSinhVien == sv.MaSinhVien);
                if (sinhvien != null)
                {
                    sinhvien.HoTen=sv.HoTen;
                    sinhvien.TruongPhong=sv.TruongPhong;
                    sinhvien.NgaySinh=sv.NgaySinh;
                    sinhvien.GioiTinh=sv.GioiTinh;
                    sinhvien.NoiSinh=sv.NoiSinh;
                    sinhvien.HoKhauThuongTru=sv.HoKhauThuongTru;
                    sinhvien.Email=sv.Email;
                    sinhvien.CCCD=sv.CCCD;
                    sinhvien.GhiChu=sv.GhiChu;
                    sinhvien.HinhCCCDTruoc=sv.HinhCCCDTruoc;
                    sinhvien.HinhCCCDSau=sv.HinhCCCDSau;
                    sinhvien.HinhNhanDien = sinhvien.HinhNhanDien;
                    ktx.SubmitChanges();
                    return true;
                }
            }
            catch
            {

                return false;
            }
            return false;
        }

        public bool XoaSinhVien(SinhVien sv)
        {
            try
            {
                var sinhvien = ktx.SinhViens.FirstOrDefault(a => a.MaSinhVien == sv.MaSinhVien); 
                if (sinhvien != null)
                {
                    ktx.SinhViens.DeleteOnSubmit(sinhvien);
                    ktx.SubmitChanges();
                    return true;
                }    
            }
            catch { return false; }
            return false;
        }

        public List<DangKyPhong> GetDKPhong()
        {
            return ktx.DangKyPhongs.ToList();
        }

        public bool ThemPhieuDangKy(DangKyPhong dkphong, SinhVien sv)
        {
            try
            {
                // Kiểm tra xem sinh viên có tồn tại trong hệ thống chưa
                var sinhvienTonTai = ktx.SinhViens.FirstOrDefault(s => s.MaSinhVien == sv.MaSinhVien);

                if (sinhvienTonTai == null)
                {
                    // Nếu sinh viên chưa tồn tại, thêm sinh viên mới
                    ktx.SinhViens.InsertOnSubmit(sv);
                    ktx.SubmitChanges();
                }
                else
                {
                    // Nếu sinh viên đã tồn tại, không cần thêm lại sinh viên
                    sv = sinhvienTonTai;
                }

                // Thêm phiếu đăng ký phòng
                dkphong.MaSinhVien = sv.MaSinhVien; // Gán mã sinh viên đã có
                ktx.DangKyPhongs.InsertOnSubmit(dkphong);
                ktx.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm phiếu đăng ký: " + ex.Message);
                return false;
            }
        }


        //public bool SuaPhieuDangKy(DangKyPhong dkphong)
        //{
        //    try
        //    {
        //        var dk = ktx.DangKyPhongs.FirstOrDefault(p => p.MaDangKyPhong == dkphong.MaDangKyPhong);
        //        if (dk != null)
        //        {
        //            dk.MaPhong = dkphong.MaPhong;
        //            dk.MaSinhVien = dkphong.MaSinhVien;
        //            dk.NgayDK = dkphong.NgayDK;
        //            dk.NgayBD = dkphong.NgayBD;
        //            dk.NgayKT = dkphong.NgayKT; // Kiểm tra xem thuộc tính này có trong bảng hay không
        //            dk.Giuong = dkphong.Giuong;
        //            dk.Tang = dkphong.Tang;
        //        }    
        //    }catch
        //    {
        //        Console.WriteLine("Có lỗi");
        //        return false;

        //    }
        //    return false;
        //}

        public bool ValidateDangKyPhong(int columnName, string duLieuMoi, DateTime? ngayDK, DateTime? ngayBD, DateTime? ngayKT)
        {
            switch (columnName)
            {
                case 4: // Kiểm tra Ngày DK
                    if (DateTime.TryParse(duLieuMoi, out DateTime ngayDangKy))
                    {
                        if (ngayBD.HasValue && ngayDangKy >= ngayBD.Value)
                            throw new ArgumentException("Ngày đăng ký phải trước ngày bắt đầu ở.");
                        return true;
                    }
                    else
                        throw new ArgumentException("Ngày đăng ký không hợp lệ.");

                case 5: // Kiểm tra Ngày BD
                    if (DateTime.TryParse(duLieuMoi, out DateTime ngayBatDau))
                    {
                        if (ngayDK.HasValue && ngayBatDau <= ngayDK.Value)
                            throw new ArgumentException("Ngày bắt đầu ở phải sau ngày đăng ký.");
                        if (ngayKT.HasValue && ngayBatDau >= ngayKT.Value)
                            throw new ArgumentException("Ngày bắt đầu ở phải trước ngày kết thúc ở.");
                        return true;
                    }
                    else
                        throw new ArgumentException("Ngày bắt đầu ở không hợp lệ.");

                case 6: // Kiểm tra Ngày KT
                    if (DateTime.TryParse(duLieuMoi, out DateTime ngayKetThuc))
                    {
                        if (ngayBD.HasValue && ngayKetThuc <= ngayBD.Value)
                            throw new ArgumentException("Ngày kết thúc ở phải sau ngày bắt đầu ở.");
                        return true;
                    }
                    else
                        throw new ArgumentException("Ngày kết thúc ở không hợp lệ.");

                case 7: // Kiểm tra Giường
                    if (int.TryParse(duLieuMoi, out int giuong))
                    {
                        if (giuong < 0)
                            throw new ArgumentException("Giường không thể có giá trị âm.");
                        return true;
                    }
                    else
                        throw new ArgumentException("Giường không hợp lệ.");

                case 8: // Kiểm tra Tầng
                    if (int.TryParse(duLieuMoi, out int tang))
                    {
                        if (tang < 0)
                            throw new ArgumentException("Tầng không thể có giá trị âm.");
                        return true;
                    }
                    else
                        throw new ArgumentException("Tầng không hợp lệ.");

                default:
                    return false;
            }
        }

        public bool SuaPhieuDangKyPhong(string maDangKyPhong, string columnName, string duLieuMoi)
        {
            try
            {
                // Tìm đối tượng phiếu đăng ký phòng theo mã đăng ký phòng
                var dkPhong = ktx.DangKyPhongs.FirstOrDefault(p => p.MaDangKyPhong == maDangKyPhong);

                // Nếu không tìm thấy bản ghi, trả về false
                if (dkPhong == null)
                {
                    return false;
                }

                // Xác định cột cần cập nhật dựa trên tên cột và gán giá trị mới
                switch (columnName)
                {
                    case "NgayDK": // Ngày đăng ký
                        if (DateTime.TryParse(duLieuMoi, out DateTime ngayDangKy))
                        {
                            dkPhong.NgayDK = ngayDangKy;
                        }
                        else
                        {
                            throw new ArgumentException("Dữ liệu ngày đăng ký không hợp lệ.");
                        }
                        break;

                    case "NgayBD": // Ngày bắt đầu
                        if (DateTime.TryParse(duLieuMoi, out DateTime ngayBatDau))
                        {
                            dkPhong.NgayBD = ngayBatDau;
                        }
                        else
                        {
                            throw new ArgumentException("Dữ liệu ngày bắt đầu không hợp lệ.");
                        }
                        break;

                    case "NgayKT": // Ngày kết thúc
                        if (DateTime.TryParse(duLieuMoi, out DateTime ngayKetThuc))
                        {
                            dkPhong.NgayKT = ngayKetThuc;
                        }
                        else
                        {
                            throw new ArgumentException("Dữ liệu ngày kết thúc không hợp lệ.");
                        }
                        break;

                

                    case "Tang": // Tầng
                        if (int.TryParse(duLieuMoi, out int tang))
                        {
                            if (tang >= 0)
                                dkPhong.Tang = tang;
                            else
                                throw new ArgumentException("Tầng không thể là số âm.");
                        }
                        else
                        {
                            throw new ArgumentException("Dữ liệu tầng không hợp lệ.");
                        }
                        break;

                    case "HinhThucThanhToan": // Hình thức thanh toán
                        if (!string.IsNullOrEmpty(duLieuMoi))
                        {
                            dkPhong.HinhThucThanhToan = duLieuMoi;
                        }
                        else
                        {
                            throw new ArgumentException("Dữ liệu hình thức thanh toán không hợp lệ.");
                        }
                        break;

               

                    case "HoTen": // Họ tên sinh viên
                        if (!string.IsNullOrEmpty(duLieuMoi))
                        {
                            dkPhong.HoTen = duLieuMoi;
                        }
                        else
                        {
                            throw new ArgumentException("Dữ liệu họ tên không hợp lệ.");
                        }
                        break;

                    case "CCCD": // CCCD sinh viên
                        if (!string.IsNullOrEmpty(duLieuMoi))
                        {
                            dkPhong.CCCD = duLieuMoi;
                        }
                        else
                        {
                            throw new ArgumentException("Dữ liệu CCCD không hợp lệ.");
                        }
                        break;

                    case "Email": // Email sinh viên
                        if (!string.IsNullOrEmpty(duLieuMoi))
                        {
                            dkPhong.Email = duLieuMoi;
                        }
                        else
                        {
                            throw new ArgumentException("Dữ liệu email không hợp lệ.");
                        }
                        break;

                    case "SDT": // Số điện thoại sinh viên
                        if (!string.IsNullOrEmpty(duLieuMoi))
                        {
                            dkPhong.SDT = duLieuMoi;
                        }
                        else
                        {
                            throw new ArgumentException("Dữ liệu số điện thoại không hợp lệ.");
                        }
                        break;

                    default:
                        throw new ArgumentException("Cột không hợp lệ.");
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                ktx.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật phiếu đăng ký: " + ex.Message);
                return false;
            }
        }




        public bool XoaPhieuDangKyPhong(string maDangKyPhong)
        {
            try
            {
                var dkPhong =ktx.DangKyPhongs.FirstOrDefault(p => p.MaDangKyPhong == maDangKyPhong);
                if (dkPhong != null)
                {
                    ktx.DangKyPhongs.DeleteOnSubmit(dkPhong);
                    ktx.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa phiếu đăng ký: " + ex.Message);
            }
            return false;
        }


        //phong
        public List<Phong> GetPhongs()
        {
            return ktx.Phongs.ToList();
        }

        public List<LoaiPhong> GetLoaiPhongs()
        {
            return ktx.LoaiPhongs.ToList();
        }

        public List<DangKyPhong> GetDangKyPhongTheoMaPhong(string maPhong)
        {
           
             // Lấy danh sách đăng ký phòng theo mã phòng
                return ktx.DangKyPhongs.Where(dk => dk.MaPhong == maPhong).ToList();
            
        }

        // Lấy các giường đã được đăng ký trong bảng DangKyPhong
        // int ? đảm bảo rằng tất cả giá trị không phải là null.
        //public List<int?> LayGiuongDaDangKy(string maPhong)
        //{
        //    List<int?> giuongDaDangKy = new List<int?>();

          
        //        var giuongDaDangKyQuery = from dkp in ktx.DangKyPhongs
        //                                  where dkp.MaPhong == maPhong
        //                                  select dkp.Giuong;

        //        giuongDaDangKy = giuongDaDangKyQuery.ToList();
        

        //    return giuongDaDangKy;
        //}
    }
}

