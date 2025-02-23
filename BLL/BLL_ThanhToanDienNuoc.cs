using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_ThanhToanDienNuoc
    {
        DAL_ThanhToan dal_thanhtoan;
        public BLL_ThanhToanDienNuoc()
        {
            dal_thanhtoan = new DAL_ThanhToan();
        }
        // Gọi các hàm từ DAL
        public List<ThanhToanDienNuoc> GetThanhToanDienNuocByThangVaNam(int thang, int nam)
        {
            return dal_thanhtoan.GetThanhToanDienNuocByThangVaNam(thang, nam);
        }

        public List<ThanhToanDienNuoc> GetThanhToanDienNuocByNam(int nam)
        {
            return dal_thanhtoan.GetThanhToanDienNuocByNam(nam);
        }

        public List<ThanhToanDienNuoc> GetThanhToanDienNuocByNgayThangNam(DateTime ngayThangNam)
        {
            return dal_thanhtoan.GetThanhToanDienNuocByNgayThangNam(ngayThangNam);
        }

        // Hàm gọi từ DAL để lấy danh sách thanh toán theo mã thanh toán
        public List<ThanhToanDienNuoc> LayDanhSachTheoMaThanhToan(string maThanhToan)
        {
            try
            {
                // Gọi hàm từ DAL và trả về kết quả
                return dal_thanhtoan.LayDanhSachTheoMaThanhToan(maThanhToan);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách thanh toán theo mã thanh toán: " + ex.Message);
                return new List<ThanhToanDienNuoc>();
            }
        }

        // Hàm gọi từ DAL để lấy danh sách thanh toán theo mã nhân viên
        public List<ThanhToanDienNuoc> LayDanhSachTheoMaNhanVien(string maNhanVien)
        {
            try
            {
                // Gọi hàm từ DAL và trả về kết quả
                return dal_thanhtoan.LayDanhSachTheoMaNhanVien(maNhanVien);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách thanh toán theo mã nhân viên: " + ex.Message);
                return new List<ThanhToanDienNuoc>();
            }
        }
        // Hàm gọi từ BLL để lấy danh sách thanh toán theo mã nhân viên
        public List<ThanhToanDienNuoc> LayDanhSachThanhToanTheoMaNhanVien(string maNhanVien)
        {
            // Gọi hàm từ DAL và trả về kết quả
            return dal_thanhtoan.LayDanhSachTheoMaNhanVien(maNhanVien);
        }
        public string LayTenNhanVien(string maNhanVien)
        {
            return dal_thanhtoan.LayTenNhanVienTuMa(maNhanVien); // Gọi phương thức DAL để lấy tên nhân viên
        }

        public string LayTenSinhVien(string maSinhVien)
        {
            return dal_thanhtoan.LayTenSinhVienTuMa(maSinhVien); // Gọi phương thức DAL để lấy tên sinh viên
        }
        // Hàm gọi từ BLL để lấy thông tin chi tiết phòng (chi tiết chỉ số điện, nước)
        public PhongChiTiet LayChiSoDienNuocTuMaPhong(string maPhong)
        {
            try
            {
                // Gọi hàm từ DAL để lấy thông tin chi tiết phòng
                return dal_thanhtoan.LayChiSoDienNuocTuMaPhong(maPhong);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy thông tin chi tiết phòng từ BLL: " + ex.Message);
                return null;
            }
        }
        // Hàm lấy chi tiết từ bảng CT_ThanhToanDienNuoc dựa trên MaThanhToanDN
        public List<CT_ThanhToanDienNuoc> LayChiTietCTThanhToanDienNuoc(string maThanhToanDN)
        {
            return dal_thanhtoan.LayChiTietCTThanhToanDienNuoc(maThanhToanDN); // Gọi hàm DAL
        }
        public ThanhToanDienNuoc LayChiTietThanhToanDienNuoc(string maThanhToanDN)
        {
            // Gọi hàm từ DAL để lấy chi tiết thanh toán
            return dal_thanhtoan.LayChiTietThanhToanDienNuoc(maThanhToanDN);
        }
        // Phương thức gọi DAL để lấy danh sách thanh toán điện nước
        public List<ThanhToanDienNuoc> LayDanhSachThanhToan()
        {
            try
            {
                // Gọi phương thức DAL
                return dal_thanhtoan.LayDanhSachThanhToanDienNuoc();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                Console.WriteLine("Lỗi khi lấy danh sách thanh toán: " + ex.Message);
                return new List<ThanhToanDienNuoc>();
            }
        }
        // Phương thức thống kê điện nước
        public List<ThongKeDienNuoc> ThongKeDienNuoc(int thang, int nam)
        {
            // Gọi phương thức từ DAL
            return dal_thanhtoan.ThongKeDienNuoc(thang, nam);
        }
        // Phương thức để lấy thông tin phòng
        public ThongTinDienNuoc LayThongTinPhong(string maPhong)
        {
            // Gọi phương thức từ DAL và trả về kết quả
            return dal_thanhtoan.LayThongTinPhong(maPhong);
        }


        // Phương thức để cập nhật chỉ số điện nước
        public bool CapNhatChiSoDienNuoc(string maPhong, int chiSoDien, int chiSoNuoc)
        {
            try
            {
                // Lấy thông tin phòng từ BLL (gọi DAL)
                ThongTinDienNuoc thongTin = dal_thanhtoan.LayThongTinPhong(maPhong);

                if (thongTin != null)
                {
                    // Lấy mã điện nước từ thông tin phòng
                    string maDienNuoc = thongTin.MaDienNuoc;

                    // Gọi phương thức cập nhật chỉ số điện nước từ DAL
                    bool ketQua = dal_thanhtoan.CapNhatChiSoDienNuoc(maDienNuoc, chiSoDien, chiSoNuoc);

                    return ketQua;
                }
                else
                {
                    Console.WriteLine("Không tìm thấy thông tin phòng cho mã phòng " + maPhong);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật chỉ số điện nước: " + ex.Message);
                return false;
            }
        }

        // Phương thức để thêm hóa đơn điện nước
        public bool ThemHoaDonDienNuoc(ThanhToanDienNuoc hoaDon, List<CT_ThanhToanDienNuoc> chiTietHoaDon)
        {
            // Gọi phương thức DAL_ThanhToan để thêm hóa đơn
            return dal_thanhtoan.ThemHoaDonDienNuoc(hoaDon, chiTietHoaDon);
        }
    }
}
