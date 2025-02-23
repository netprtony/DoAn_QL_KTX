using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BLL
{
    public class BLL_ViPham
    {
        DAL_ViPham dal_vipham =new DAL_ViPham();


        // Phương thức gọi GetViPhamByMaSinhVien từ DAL
        public List<ViPham> GetViPhamByMaSinhVien(string maSinhVien)
        {
            try
            {
                // Gọi phương thức GetViPhamByMaSinhVien từ DAL và trả về kết quả
                return dal_vipham.GetViPhamByMaSinhVien(maSinhVien);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                return new List<ViPham>(); // Trả về danh sách rỗng trong trường hợp lỗi
            }
        }

        // Phương thức gọi GetViPhamByTenLoaiNoiQuy từ DAL
        public List<ViPham> GetViPhamByTenLoaiNoiQuy(string tenLoaiNoiQuy)
        {
            try
            {
                // Gọi phương thức GetViPhamByTenLoaiNoiQuy từ DAL và trả về kết quả
                return dal_vipham.GetViPhamByTenLoaiNoiQuy(tenLoaiNoiQuy);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                return new List<ViPham>(); // Trả về danh sách rỗng trong trường hợp lỗi
            }
        }

        // Phương thức gọi GetAllLoaiNoiQuy từ DAL
        public List<LoaiNoiQuy> GetAllLoaiNoiQuy()
        {
            try
            {
                // Gọi phương thức GetAllLoaiNoiQuy từ DAL và trả về kết quả
                return dal_vipham.GetAllLoaiNoiQuy();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
               // Console.WriteLine($"Đã xảy ra lỗi khi lấy danh sách loại nội quy: {ex.Message}");
                return new List<LoaiNoiQuy>(); // Trả về danh sách rỗng trong trường hợp lỗi
            }
        }
      
        public ViPham XemChiTietViPham(string maViPham)
        {
            try
            {
                // Gọi phương thức XemChiTietViPham từ DAL
                ViPham viPhamChiTiet = dal_vipham.XemChiTietViPham(maViPham);

                if (viPhamChiTiet != null)
                {
                    // Xử lý logic thêm nếu cần
                    return viPhamChiTiet;
                }
                else
                {
                    Console.WriteLine("Không tìm thấy phiếu vi phạm.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                Console.WriteLine($"Đã xảy ra lỗi khi truy vấn chi tiết phiếu vi phạm: {ex.Message}");
                return null;
            }
        }

        // Phương thức gọi hàm LoadDanhSachViPham trong DAL_ViPham
        public List<ViPham> LayDanhSachViPham()
        {
            // Gọi hàm LoadDanhSachViPham từ DAL_ViPham và trả về danh sách
            return dal_vipham.LoadDanhSachViPham();
        }
        // Phương thức gọi hàm ThemViPham trong DAL_ViPham
        public bool LapPhieuViPham(ViPham viPham)
        {
            // Gọi hàm ThemViPham từ DAL_ViPham và trả về kết quả
            return dal_vipham.ThemViPham(viPham);
        }
        // Phương thức gọi hàm ThongKeViPhamTheoThangNam trong DAL_ViPham
        public List<ThongKeLoaiViPham> ThongKeViPhamTheoThangNam(int thang, int nam)
        {
            // Gọi hàm ThongKeViPhamTheoThangNam từ DAL_ViPham và trả về kết quả
            return dal_vipham.ThongKeViPhamTheoThangNam(thang, nam);
        }
        // Phương thức gọi hàm ThongKeViPhamTheoPhong trong DAL_ViPham
        public List<ThongKeViPhamTheoPhong> ThongKeViPhamTheoPhong(int thang, int nam)
        {
            // Gọi hàm ThongKeViPhamTheoPhong từ DAL_ViPham và trả về kết quả
            return dal_vipham.ThongKeViPhamTheoPhong(thang, nam);
        }
        // Phương thức gọi hàm GetAllMaPhong từ DAL_ViPham
        public List<Phong> GetAllMaPhong()
        {
            return dal_vipham.GetAllMaPhong();  // Gọi hàm DAL để lấy danh sách mã phòng
        }
    }
}
