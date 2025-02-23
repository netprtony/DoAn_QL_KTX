using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_YeuCauSuaChua
    {
        private DAL_YeuCauSuaChua dal_ycsc;
        public BLL_YeuCauSuaChua()
        {
            dal_ycsc = new DAL_YeuCauSuaChua(); 
        }

        public List<CT_YeuCauSuaChua> HienThiDanhSachYeuCau(string maYCSuaChua)
        {
            try
            {
                return dal_ycsc.GetDanhSachYeuCauSuaChua(maYCSuaChua);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong BLL (HienThiDanhSachYeuCau): " + ex.Message);
                return new List<CT_YeuCauSuaChua>();
            }
        }


        // Hàm gọi DAL để cập nhật yêu cầu sửa chữa
        public bool CapNhatYeuCauSuaChua(string maYCSuaChua, string maThietBi, int soLuong, string tinhTrang, decimal phiSuaChua)
        {
            try
            {
                // Gọi hàm CapNhatYeuCauSuaChua từ DAL với các tham số riêng lẻ
                return dal_ycsc.CapNhatYeuCauSuaChua(maYCSuaChua, maThietBi, soLuong, tinhTrang, phiSuaChua);
            }
            catch (Exception ex)
            {
                // Ghi log hoặc xử lý lỗi theo cách bạn muốn
                Console.WriteLine("Lỗi trong BLL (CapNhatYeuCauSuaChua): " + ex.Message);
                return false; // Trả về false nếu có lỗi
            }
        }

        // Hàm gọi DAL để lấy số lượng trang thiết bị
        public int GetSoLuongTrangThietBi(string maPhong, string maThietBi)
        {
            try
            {
                // Gọi hàm GetSoLuongTrangThietBi từ DAL
                return dal_ycsc.GetSoLuongTrangThietBi(maPhong, maThietBi);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong BLL (GetSoLuongTrangThietBi): " + ex.Message);
                return 0; // Trả về 0 nếu có lỗi
            }
        }
        // Hàm gọi DAL để cập nhật yêu cầu sửa chữa
        public bool CapNhatYeuCauSuaChua(YeuCauSuaChua ycSuaChua)
        {
            try
            {
                // Gọi hàm từ DAL để thực hiện cập nhật
                return dal_ycsc.CapNhatYeuCauSuaChua(ycSuaChua);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong BLL: " + ex.Message);
                return false; // Trả về false nếu có lỗi
            }
        }
        public bool XoaYeuCauSuaChua(string maYCSuaChua)
        {
            try
            {
                // Gọi hàm từ DAL để cập nhật trạng thái
                return dal_ycsc.CapNhatTrangThaiDaHuy(maYCSuaChua);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong BLL: " + ex.Message);
                return false;
            }
        }

        public List<YeuCauSuaChua> GetYeuCauSuaChuaByThangVaNam(int thang, int nam)
        {
            try
            {
                return dal_ycsc.GetYeuCauSuaChuaByThangVaNam(thang, nam);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong BLL (Tháng và Năm): " + ex.Message);
                return new List<YeuCauSuaChua>();
            }
        }


        public List<YeuCauSuaChua> GetYeuCauSuaChuaByNam(int nam)
        {
            try
            {
                return dal_ycsc.GetYeuCauSuaChuaByNam(nam);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong BLL (Năm): " + ex.Message);
                return new List<YeuCauSuaChua>();
            }
        }

        public List<YeuCauSuaChua> GetYeuCauSuaChuaByNgayThangNam(DateTime ngayThangNam)
        {
            try
            {
                return dal_ycsc.GetYeuCauSuaChuaByNgayThangNam(ngayThangNam);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong BLL (Ngày): " + ex.Message);
                return new List<YeuCauSuaChua>();
            }
        }

        // Phương thức để lấy thống kê sửa chữa (gọi hàm từ DAL)
        public List<ThongKeSuaChua> GetThongKeSuaChua(int nam, int thang)
        {
            // Gọi phương thức GetThongKeSuaChua từ DAL và trả kết quả cho bên ngoài
            return dal_ycsc.GetThongKeSuaChua(nam, thang);
        }
        // Phương thức lấy dữ liệu tần suất hỏng hóc thiết bị
        public List<ThongKeSuaChuaTanSuat> GetTanSuatHongHoc(int? nam, int? thang)
        {
            return dal_ycsc.GetTanSuatHongHoc(nam, thang);
        }
        // Gọi DAL để lấy danh sách yêu cầu sửa chữa theo chi phí tăng dần
        public List<YeuCauSuaChua> GetYeuCauSuaChuaByCostAsc()
        {
            try
            {
                return dal_ycsc.GetYeuCauSuaChuaByCostAsc();  // Gọi hàm DAL
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong BLL: " + ex.Message);
                return new List<YeuCauSuaChua>();
            }
        }

        // Gọi DAL để lấy danh sách yêu cầu sửa chữa theo chi phí giảm dần
        public List<YeuCauSuaChua> GetYeuCauSuaChuaByCostDesc()
        {
            try
            {
                return dal_ycsc.GetYeuCauSuaChuaByCostDesc();  // Gọi hàm DAL
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong BLL: " + ex.Message);
                return new List<YeuCauSuaChua>();
            }
        }
        public List<YeuCauSuaChua> GetYeuCauSuaChuaByMaNhanVien(string maNhanVien)
        {
            try
            {
                return dal_ycsc.GetYeuCauSuaChuaByMaNV(maNhanVien);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi xử lý nghiệp vụ: " + ex.Message);
                return new List<YeuCauSuaChua>();
            }
        }
        public List<YeuCauSuaChua> GetYeuCauSuaChuaByMa(string maYCSuaChua)
        {
            try
            {
                return dal_ycsc.GetYeuCauSuaChuaByMa(maYCSuaChua);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi xử lý nghiệp vụ: " + ex.Message);
                return new List<YeuCauSuaChua>();
            }
        }

        public List<YeuCauSuaChua> GetAllMaYeuCauSuaChua()
        {
            try
            {
                return dal_ycsc.GetAllMaYeuCauSuaChua();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xử lý nghiệp vụ: " + ex.Message);
                return new List<YeuCauSuaChua>();
            }
        }
        public List<YeuCauSuaChua> GetYeuCauSuaChuaByTrangThai(string trangThai)
        {
            try
            {
                return dal_ycsc.GetYeuCauSuaChuaByTrangThai(trangThai);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong BLL: " + ex.Message);
                return new List<YeuCauSuaChua>();
            }
        }


        // Hàm gọi phương thức GetChiTietYCSC từ DAL.
        public List<ThongTinYeuCauSuaChua> GetChiTietYCSC(string maYCSuaChua)
        {
            return dal_ycsc.GetChiTietYCSC(maYCSuaChua);
        }
        public ThongTinYeuCauSuaChua GetYeuCauSuaChuaDetails(string maYCSuaChua)
        {
            try
            {
                // Gọi phương thức từ DAL để lấy thông tin yêu cầu sửa chữa
                return dal_ycsc.GetYeuCauSuaChuaDetails(maYCSuaChua);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xử lý nghiệp vụ: " + ex.Message);
                return null;
            }
        }
        public bool AddYeuCauSuaChuaAndDetails(YeuCauSuaChua ycSuaChua, List<CT_YeuCauSuaChua> ctYeuCauSuaChuas)
        {
            try
            {
                // Gọi phương thức trong DAL để thêm yêu cầu sửa chữa và các chi tiết của nó
                return dal_ycsc.AddYeuCauSuaChuaVaChiTiet(ycSuaChua, ctYeuCauSuaChuas);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm yêu cầu sửa chữa và chi tiết trong BLL: " + ex.Message);
                return false;
            }
        }

        // Phương thức lấy danh sách yêu cầu sửa chữa từ DAL
        public List<YeuCauSuaChua> LayDanhSachYeuCauSuaChua()
        {
            try
            {
                return dal_ycsc.LayDanhSachYeuCauSuaChua();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("Lỗi khi gọi BLL_YeuCauSuaChua: " + ex.Message);
                return new List<YeuCauSuaChua>();
            }
        }
        // Hàm thêm cả yêu cầu sửa chữa và chi tiết yêu cầu sửa chữa
        //public bool AddYeuCauSuaChuaAndDetails(YeuCauSuaChua ycSuaChua, List<CT_YeuCauSuaChua> ctYeuCauSuaChuas)
        //{
        //    try
        //    {
        //        // Gọi phương thức thêm yêu cầu sửa chữa
        //        bool isYeuCauSuccess = dal_ycsc.AddYeuCauSuaChua(ycSuaChua);
        //        if (!isYeuCauSuccess)
        //        {
        //            return false; // Nếu thêm yêu cầu sửa chữa không thành công thì dừng
        //        }

        //        // Lấy MaYCSuaChua (Mã yêu cầu sửa chữa) vừa được thêm vào từ cơ sở dữ liệu
        //        ycSuaChua.MaYCSuaChua = dal_ycsc.GetMaYCSuaChua(); // Hàm này cần được cài đặt trong DAL để lấy MaYCSuaChua

        //        // Thêm các chi tiết yêu cầu sửa chữa 
        //        foreach (var ct in ctYeuCauSuaChuas)
        //        {
        //            ct.MaYCSuaChua = ycSuaChua.MaYCSuaChua; // Gán MaYCSuaChua cho chi tiết yêu cầu sửa chữa

        //            bool isCTSuccess = dal_ycsc.AddCT_YeuCauSuaChua(ct);
        //            if (!isCTSuccess)
        //            {
        //                return false; // Nếu có lỗi khi thêm chi tiết thì dừng
        //            }
        //        }

        //        return true; // Thêm cả yêu cầu sửa chữa và chi tiết thành công
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Lỗi khi thêm yêu cầu sửa chữa và chi tiết: " + ex.Message);
        //        return false;
        //    }
        //}

        // Phương thức để thêm yêu cầu sửa chữa
        public bool AddYeuCauSuaChua(YeuCauSuaChua ycSuaChua)
        {
            try
            {
                return dal_ycsc.AddYeuCauSuaChua(ycSuaChua);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm yêu cầu sửa chữa: " + ex.Message);
                return false;
            }
        }

        // Phương thức để thêm chi tiết yêu cầu sửa chữa
        public bool AddCT_YeuCauSuaChua(CT_YeuCauSuaChua ctYeuCauSuaChua)
        {
            try
            {
                return dal_ycsc.AddCT_YeuCauSuaChua(ctYeuCauSuaChua);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm chi tiết yêu cầu sửa chữa: " + ex.Message);
                return false;
            }
        }

        // Phương thức kết hợp thêm cả yêu cầu sửa chữa và chi tiết yêu cầu sửa chữa
        public bool CreateYeuCauSuaChua(YeuCauSuaChua ycSuaChua, CT_YeuCauSuaChua ctYeuCauSuaChua)
        {
            // Thêm yêu cầu sửa chữa
            bool resultYCSuaChua = AddYeuCauSuaChua(ycSuaChua);
            if (resultYCSuaChua)
            {
                // Thêm chi tiết yêu cầu sửa chữa
                return AddCT_YeuCauSuaChua(ctYeuCauSuaChua);
            }

            return false;
        }
    }
}
