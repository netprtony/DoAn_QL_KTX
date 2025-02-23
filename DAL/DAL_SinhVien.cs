using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_SinhVien
    {
        DA_QL_KTXDataContext ktx = new DA_QL_KTXDataContext();
        public DAL_SinhVien()
        {

        }

        public SinhVien GetinhVienByID(string maSinhVien)
        {
            var sinhVien = ktx.SinhViens.FirstOrDefault(sv => sv.MaSinhVien == maSinhVien);
            if (sinhVien != null)
            {
                // Trả về đối tượng SinhVien chứa thông tin
                return new SinhVien
                {
                    MaSinhVien = sinhVien.MaSinhVien,
                    HoTen = sinhVien.HoTen,
                    TruongPhong = sinhVien.TruongPhong,
                    NgaySinh = sinhVien.NgaySinh,
                    GioiTinh = sinhVien.GioiTinh,
                    NoiSinh = sinhVien.NoiSinh,
                    HoKhauThuongTru = sinhVien.HoKhauThuongTru,
                    Email = sinhVien.Email,
                    CCCD = sinhVien.CCCD,
                    GhiChu = sinhVien.GhiChu,
                    HinhNhanDien = sinhVien.HinhNhanDien,
                    HinhCCCDTruoc = sinhVien.HinhCCCDTruoc,
                    HinhCCCDSau = sinhVien.HinhCCCDSau
                };
            }
            return null;
        }
        public SinhVienDTO GetSinhVienPhong(string maSinhVien)
        {
            // Lấy thông tin sinh viên dựa trên mã sinh viên
            var sinhVien = ktx.SinhViens
                .Where(sv => sv.MaSinhVien == maSinhVien)
                .Select(sv => new
                {
                    sv.HoTen,
                    sv.TruongPhong,
                    sv.NgaySinh,
                    sv.GioiTinh,
                    sv.NoiSinh,
                    sv.HoKhauThuongTru,
                    sv.HinhNhanDien,
                    Phong = sv.DangKyPhongs.Select(dk => new
                    {
                        dk.MaPhong,
                        //   TenLoaiPhong = dk.Phong.MaLoaiPhong != null ? ktx.LoaiPhongs.FirstOrDefault(lp => lp.MaLoaiPhong == dk.Phong.MaLoaiPhong)?.TenLoaiPhong : null
                    }).FirstOrDefault() // Chỉ lấy thông tin phòng đầu tiên
                })
                .FirstOrDefault();

            if (sinhVien == null)
            {
                return null; // Không tìm thấy sinh viên
            }


            // Tạo một đối tượng DTO để trả về
            var sinhVienDTO = new SinhVienDTO
            {
                HoTen = sinhVien.HoTen,
                //  TruongPhong = sinhVien.TruongPhong, // Giá trị sẽ được ánh xạ từ bit sang bool
                NgaySinh = sinhVien.NgaySinh,
                GioiTinh = sinhVien.GioiTinh,
                NoiSinh = sinhVien.NoiSinh,
                HoKhauThuongTru = sinhVien.HoKhauThuongTru,
                HinhNhanDien = sinhVien.HinhNhanDien,
                Phong = new Phong
                {
                    MaPhong = sinhVien.Phong?.MaPhong,
                    // TenLoaiPhong = sinhVien.Phong != null ? sinhVien.Phong.TenLoaiPhong : null
                }
            };

            return sinhVienDTO;
        }
    }
}
