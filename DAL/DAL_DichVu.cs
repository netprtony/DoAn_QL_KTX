using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_DichVu
    {
        DA_QL_KTXDataContext ktx = new DA_QL_KTXDataContext();

        // Hàm lấy thông tin chi tiết đăng ký dịch vụ theo mã đăng ký
        public DangKyDichVu GetChiTietDangKyDichVu(string maDangKy)
        {
            try
            {
                // Lấy thông tin từ cơ sở dữ liệu dựa trên mã đăng ký
                var result = ktx.DangKyDichVus.FirstOrDefault(dk => dk.MaDangKy == maDangKy);

                if (result != null)
                {
                    // Truyền dữ liệu từ result vào đối tượng DangKyDichVu
                    return new DangKyDichVu
                    {
                        MaDangKy = result.MaDangKy,
                        MaNhanVien = result.MaNhanVien,
                        MaSinhVien = result.MaSinhVien,
                        LoaiDangKy = result.LoaiDangKy,
                        NgayDangKy = result.NgayDangKy,
                        TongTien = result.TongTien,
                        TrangThai = result.TrangThai
                    };
                }
                return null; // Không tìm thấy thông tin đăng ký
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin chi tiết đăng ký dịch vụ: " + ex.Message);
            }
        }
        // Hàm lấy thông tin dịch vụ "Giặt Ủi"
        public DichVu GetDichVuGiacUi()
        {
            var result = ktx.DichVus.FirstOrDefault(dv => dv.TenDichVu == "Giặt Ủi");
            if (result != null)
            {
                // Truyền dữ liệu từ result vào đối tượng DichVu
                return new DichVu
                {
                    MaDichVu = result.MaDichVu,
                    TenDichVu = result.TenDichVu,
                    GiaDichVu = result.GiaDichVu,
                    DonVi = result.DonVi

                };
            }
            return null; // Không tìm thấy dịch vụ
        }

        public DichVu GetDichVuCanTin()
        {
            var result = ktx.DichVus.FirstOrDefault(dv => dv.TenDichVu == "Căn Tin");
            if (result != null)
            {
                // Truyền dữ liệu từ result vào đối tượng DichVu
                return new DichVu
                {
                    MaDichVu = result.MaDichVu,
                    TenDichVu = result.TenDichVu,
                    GiaDichVu = result.GiaDichVu,
                    DonVi = result.DonVi
                };
            }
            return null; // Không tìm thấy dịch vụ
        }

        private string GenerateMaDangKy()
        {
            string prefix = "DKDV";

            // Lấy mã đăng ký cuối cùng có dạng DKDVXXXXXX
            var lastMaDangKy = ktx.DangKyDichVus
                                  .Where(dk => dk.MaDangKy.StartsWith(prefix))
                                  .OrderByDescending(dk => dk.MaDangKy)
                                  .Select(dk => dk.MaDangKy)
                                  .FirstOrDefault();

            if (lastMaDangKy != null)
            {
                // Lấy số sau DKDV và tăng lên 1
                int number = int.Parse(lastMaDangKy.Substring(4)) + 1;
                return $"{prefix}{number:D6}";  // Đảm bảo rằng số có 6 chữ số
            }
            else
            {
                return $"{prefix}000001";  // Nếu không có mã nào, bắt đầu từ DKDV000001
            }
        }

        // Hàm lập phiếu đăng ký dịch vụ
        // Hàm lập phiếu đăng ký dịch vụ
        public bool LapPhieuDangKy(DangKyDichVu DangKyDV, List<CT_DangKyDV> chiTietDichVu)
        {
            try
            {
                // 1. Sinh mã đăng ký tự động
                string maDangKy = GenerateMaDangKy();

                // 2. Thêm thông tin vào bảng DangKyDichVu
                DangKyDV.MaDangKy = maDangKy;  // Gán mã đăng ký mới
                ktx.DangKyDichVus.InsertOnSubmit(DangKyDV);
                ktx.SubmitChanges();

                // 3. Thêm chi tiết dịch vụ vào bảng CT_DangKyDV
                foreach (var chiTiet in chiTietDichVu)
                {
                    CT_DangKyDV newChiTiet = new CT_DangKyDV
                    {
                        MaDangKy = maDangKy,  // Gán mã đăng ký đã tạo
                        MaDichVu = chiTiet.MaDichVu,
                        LoaiDangKy = chiTiet.LoaiDangKy,
                        DonGia = chiTiet.DonGia,
                        SoLuong = chiTiet.SoLuong,
                        DonViTinh = chiTiet.DonViTinh,
                        NgayBD = chiTiet.NgayBD,
                        NgayKT = chiTiet.NgayKT
                    };
                    ktx.CT_DangKyDVs.InsertOnSubmit(newChiTiet);
                }

                // 4. Lưu tất cả các thay đổi
                ktx.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        //PHAN TAO LAM///

        public List<DangKyDichVu> GetDanhSachDangKyDichVu()
        {
            return ktx.DangKyDichVus.ToList();
        }

        public List<NhanVien> GetDanhSachNhanVien()
        {
            return ktx.NhanViens.ToList();
        }

        public List<SinhVien> GetDanhSachSinhVien()
        {
            return ktx.SinhViens.ToList();
        }

        public DichVu GetDichVuById(string maDichVu)
        {
            return ktx.DichVus.FirstOrDefault(dv => dv.MaDichVu == maDichVu);
        }

        public SinhVien GetThongTinSinhVien(string maSinhVien)
        {
            return ktx.SinhViens.FirstOrDefault(sv => sv.MaSinhVien == maSinhVien);
        }

        public DangKyPhong GetThongTinPhongTheoSinhVien(string maSinhVien)
        {
            return ktx.DangKyPhongs.FirstOrDefault(dk => dk.MaSinhVien == maSinhVien);
        }

        public Phong GetThongTinPhong(string maPhong)
        {
            return ktx.Phongs.FirstOrDefault(p => p.MaPhong == maPhong);
        }

        public NhanVien GetThongTinNhanVien(string maNhanVien)
        {
            return ktx.NhanViens.FirstOrDefault(p => p.MaNhanVien == maNhanVien);
        }

        public List<CT_DangKyDV> GetThongTinCTDangKyDichVuTheoMaDangKy(string maDangKy)
        {
            return ktx.CT_DangKyDVs.Where(ct => ct.MaDangKy == maDangKy).ToList();
        }

        public bool XoaDichVu(string maDangKy)
        {
            try
            {
                var dangKyDichVu = ktx.DangKyDichVus.FirstOrDefault(d => d.MaDangKy == maDangKy);

                if (dangKyDichVu != null)
                {

                    ktx.DangKyDichVus.DeleteOnSubmit(dangKyDichVu);
                    ktx.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Có lỗi khi xóa đăng ký dịch vụ với mã {maDangKy}: {ex.Message}");
            }
        }

        public bool CapNhatDangKyDichVu(DangKyDichVu dangKyDichVu)
        {
            try
            {
                var entity = ktx.DangKyDichVus.FirstOrDefault(d => d.MaDangKy == dangKyDichVu.MaDangKy);
                if (entity != null)
                {
                    entity.MaSinhVien = dangKyDichVu.MaSinhVien;
                    entity.MaNhanVien = dangKyDichVu.MaNhanVien;
                    entity.TongTien = dangKyDichVu.TongTien;
                    entity.LoaiDangKy = dangKyDichVu.LoaiDangKy;
                    entity.NgayDangKy = dangKyDichVu.NgayDangKy;
                    entity.TrangThai = dangKyDichVu.TrangThai;

                    ktx.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi khi cập nhật đăng ký dịch vụ: " + ex.Message);
            }
        }

    }
}
