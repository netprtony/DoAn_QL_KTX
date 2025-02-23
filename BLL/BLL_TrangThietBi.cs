using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_TrangThietBi
    {
        private DAL_TrangThietBi dal_ttb;
        public BLL_TrangThietBi()
        {
            dal_ttb = new DAL_TrangThietBi();
        }
        // Gọi hàm lấy tất cả trang thiết bị
        public List<TrangThietBi> GetAllTrangThietBi()
        {
            return dal_ttb.GetAllTrangThietBi();
        }
        // lây tất cả chi tiết trang thiết bị ở từng phòng
        public List<CT_TrangThietBi> GetAllCT_TrangThietBi()
        {
            return dal_ttb.GetAllCT_TrangThietBi();
        }
        // Phương thức gọi DAL để lấy thông tin chi tiết trang thiết bị theo MaThietBi và MaPhong
        // Gọi phương thức GetCT_TrangThietBiByMaThietBiAndMaPhong từ DAL
        public PhieuKTTrangThietBi GetCT_TrangThietBiByMaThietBiAndMaPhong(string maThietBi, string maPhong)
        {
            try
            {
                // Gọi DAL và trả kết quả
                return dal_ttb.GetCT_TrangThietBiByMaThietBiAndMaPhong(maThietBi, maPhong);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("Lỗi khi lấy thông tin từ BLL: " + ex.Message);
                return null;
            }
        }

        public List<Phong> GetAllMaPhong()
        {
            return dal_ttb.GetAllMaPhong();
        }
        // Gọi hàm lấy thông tin trang thiết bị theo mã
        public TrangThietBi GetTrangThietBiById(string maThietBi)
        {
            return dal_ttb.GetTrangThietBiById(maThietBi);
        }

        // Gọi hàm thêm trang thiết bị
        public bool AddTrangThietBi(TrangThietBi thietBi)
        {
            return dal_ttb.AddTrangThietBi(thietBi);
        }

        // Gọi hàm cập nhật trang thiết bị
        public bool UpdateTrangThietBi(TrangThietBi thietBi)
        {
            return dal_ttb.UpdateTrangThietBi(thietBi);
        }

        // Gọi hàm xóa trang thiết bị theo mã
        public bool DeleteTrangThietBi(string maThietBi)
        {
            return dal_ttb.DeleteTrangThietBi(maThietBi);
     
        }
        // Phương thức gọi DAL để lấy tên loại phòng
        public string GetTenLoaiPhong(string maPhong)
        {
            return dal_ttb.GetTenLoaiPhongByMaPhong(maPhong);
        }

        // Gọi hàm từ DAL để lấy danh sách trang thiết bị theo mã phòng
        public List<string> GetTrangThietBiByMaPhong(string maPhong)
        {
            return dal_ttb.GetTrangThietBiByMaPhong(maPhong); // Gọi trực tiếp hàm DAL
        }
        public string GetMaThietBiByTen(string tenThietBi)
        {
            // Gọi phương thức DAL để lấy mã thiết bị từ tên trang thiết bị
            return dal_ttb.GetMaThietBiByTen(tenThietBi);
        }

        // Phương thức gọi hàm GetThongTinTrangThietBi từ DAL
        public List<PhieuKTTrangThietBi> GetThongTinTrangThietBi()
        {
            return dal_ttb.GetThongTinTrangThietBi();
        }

        public bool CapNhatTrangThaiChiTietTTB(string maThietBi, string maPhong, string trangThai)
        {
            try
            {
                // Gọi phương thức DAL để cập nhật trạng thái
                return dal_ttb.CapNhatTrangThaiChiTietTTB(maThietBi, maPhong, trangThai);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật trạng thái chi tiết thiết bị: {ex.Message}");
                return false;
            }
        }
        public string GenerateNextMaThietBi()
        {
            return dal_ttb.GetNextMaThietBi();
        }

        public List<CTTTB_DTO> GetChiTietTrangThietBiByMaThietBi(string maThietBi)
        {
            return dal_ttb.GetChiTietTrangThietBiByMaThietBi(maThietBi);
        }

        public List<CTTTB_DTO> GetAllChiTietTrangThietBiWithTen()
        {
            return dal_ttb.GetAllChiTietTrangThietBiWithTen();
        }
        public bool XoaHoacCapNhatTrangThai(string maThietBi)
        {
            try
            {
                // Kiểm tra thiết bị có chi tiết liên quan không
                bool hasDetails = dal_ttb.HasChiTietThietBi(maThietBi);

                if (hasDetails)
                {
                    // Nếu có chi tiết liên quan, cập nhật trạng thái thành "Ngưng Hoạt Động"
                    return dal_ttb.UpdateTrangThaiTrangThietBi(maThietBi, "Ngưng Hoạt Động");
                }
                else
                {
                    // Nếu không có chi tiết liên quan, thực hiện xóa thiết bị
                    return dal_ttb.DeleteTrangThietBi(maThietBi);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xử lý xóa hoặc cập nhật trạng thái: {ex.Message}");
                return false;
            }
        }
        public bool UpdateTrangThaiChiTietByMaThietBi(string maThietBi, string trangThai)
        {
            return dal_ttb.UpdateTrangThaiChiTietByMaThietBi(maThietBi, trangThai);
        }
        public bool AddChiTietTrangThietBi(CT_TrangThietBi chiTiet)
        {
            return dal_ttb.AddChiTietTrangThietBi(chiTiet);
        }
        public bool UpdateTongSoLuong(string maThietBi)
        {
            try
            {
                // Tính tổng số lượng chi tiết thiết bị không ở trạng thái "Ngừng Hoạt Động"
                int? tongSoLuong = dal_ttb.GetTongSoLuongHoatDong(maThietBi);

                // Cập nhật tổng số lượng vào bảng TrangThietBi
                return dal_ttb.UpdateTongSoLuongTrangThietBi(maThietBi, tongSoLuong);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật tổng số lượng: {ex.Message}");
                return false;
            }
        }
        public bool UpdateChiTietTrangThietBi(CT_TrangThietBi chiTiet)
        {
            return dal_ttb.UpdateChiTietTrangThietBi(chiTiet);
        }
        //QUAN_Ly_Nhap
        public List<DonNhapDTO> GetAllDonNhapWithDetails()
        {
            return dal_ttb.GetAllDonNhapWithDetails();
        }
        public List<ChiTietDonNhap_DTO> GetChiTietDonNhapByMaDonNhap(string maDonNhap)
        {
            return dal_ttb.GetChiTietDonNhapByMaDonNhap(maDonNhap);
        }
        public List<DonNhapDTO> GetAllThongTinDonNhap()
        {
            try
            {
                // Lấy dữ liệu từ DAL, bao gồm bảng chính và các bảng liên quan
                return dal_ttb.GetAllThongTinDonNhap();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách Đơn Nhập: {ex.Message}");
            }
        }
        //Thong ke
        public List<ThongKeDonNhap> GetThongKeTheoQuy(int nam, int quy)
        {
            return dal_ttb.GetThongKeTheoQuy(nam, quy);
        }
        public List<ThongKeDonNhap> GetThongKeTongTienTheoThang(int nam)
        {
            return dal_ttb.GetThongKeTongTienTheoThang(nam);
        }
    }
}
