using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class BLL_QL_Phong
    {

        DAL_QL_Phong dal_phong = new DAL_QL_Phong();

        public BLL_QL_Phong()
        {
        }
        public bool KiemTraTrangThaiPhong(string maPhong)
        {
            // Gọi hàm kiểm tra trạng thái phòng từ DAL
            return dal_phong.KiemTraTrangThaiPhong(maPhong);
        }

        // Hàm gọi ThemSinhVienVaDangKyPhong từ DAL với danh sách sinh viên và đăng ký phòng
        public string ThemSinhVienVaDangKyPhong(List<SinhVien> sinhVienList, List<DangKyPhong> dangKyPhongList)
        {
            return dal_phong.ThemSinhVienVaDangKyPhong(sinhVienList, dangKyPhongList);
        }
        public string GetTangForPhong(string maPhong)
        {
            return dal_phong.GetTangForPhong(maPhong);
        }

        // Hàm gọi PhanPhongBangKMeans từ DAL và trả về kết quả
        public Dictionary<string, List<SinhVienDTO>> PhanPhongBangKMeans(List<SinhVienDTO> sinhVienList, List<Phong> danhSachPhong)
        {
            return dal_phong.PhanPhongBangKMeans(sinhVienList, danhSachPhong);
        }
        // Hàm gọi LayDSPhongThuong từ DAL và trả về kết quả
        public List<Phong> LayDSPhongThuong()
        {
            // Gọi hàm từ DAL để lấy danh sách phòng
            return dal_phong.LayDSPhongThuong();
        }
        public List<string> DS_Phong()
        {
            return dal_phong.LayDanhSachPhong();
        }
        public int LaySoSinhVien(string maPhong)
        {
            return dal_phong.LaySoSV(maPhong);
        }
        public int LaySoSVToiDa(string maPhong)
        {
            return dal_phong.LaySoSVToiDa(maPhong);
        }

        public List<Phong> GetPhongTheoLoai(int maLoaiPhong)
        {
            // Ensure you have a method in the DAL layer that retrieves rooms based on room type
            return dal_phong.GetPhongByLoaiPhong(maLoaiPhong);
        }
        public List<Phong> GetPhong()
        {
            return dal_phong.GetPhong();
        }

        public List<LoaiPhong> LayLoaiPhong()
        {
            return dal_phong.LayLoaiPhong();
        }

        public bool CapNhatPhong(string maPhong, int maLoaiPhong, bool trangThai, int soLuongSVToiDa)
        {
            return dal_phong.CapNhatPhong(maPhong, maLoaiPhong, trangThai, soLuongSVToiDa);
        }

        public Phong GetPhongTheoMaPhong(string maphong)
        {
            return dal_phong.GetPhongTheoMaPhong(maphong);
        }
    }
}
