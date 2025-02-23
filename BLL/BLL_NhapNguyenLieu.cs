using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLL_NhapNguyenLieu
    {
        DAL_NhapNguyenLieu DAL_NNL = new DAL_NhapNguyenLieu();
        public BLL_NhapNguyenLieu() { }
        // Hàm lấy danh sách mã mua từ DAL
        public List<string> GetDanhSachMaMua()
        {
            return DAL_NNL.LayDanhSachMaMua();
        }

        // Hàm gọi đến LocTheoMaMua từ DAL
        public List<MuaNguyenLieu> GetMuaNguyenLieuMaMua(string maMua)
        {
            return DAL_NNL.LocTheoMaMua(maMua); // Gọi hàm lọc theo mã mua từ DAL
        }

        // Hàm gọi đến LocTheoMaNhanVien từ DAL
        public List<MuaNguyenLieu> GetMuaNguyenLieuByMaNhanVien(string maNhanVien)
        {
            return DAL_NNL.LocTheoMaNhanVien(maNhanVien); // Gọi hàm lọc theo mã nhân viên từ DAL
        }

        // Hàm gọi đến LocGiamTheoTongTien từ DAL
        public List<MuaNguyenLieu> GetMuaNguyenLieuByTongTienDesc()
        {
            return DAL_NNL.LocGiamTheoTongTien(); // Gọi hàm lọc theo giảm dần tổng tiền từ DAL
        }

        // Hàm gọi đến LocTangTheoTongTien từ DAL
        public List<MuaNguyenLieu> GetMuaNguyenLieuByTongTienAsc()
        {
            return DAL_NNL.LocTangTheoTongTien(); // Gọi hàm lọc theo tăng dần tổng tiền từ DAL
        }

        // Hàm gọi đến LocTheoThangNam từ DAL
        public List<MuaNguyenLieu> GetMuaNguyenLieuByThangNam(int thang, int nam)
        {
            return DAL_NNL.LocTheoThangNam(thang, nam); // Gọi hàm lọc theo tháng và năm từ DAL
        }


        // Hàm gọi đến HienThiMuaNguyenLieu từ DAL
        public List<MuaNguyenLieu> GetMuaNguyenLieu()
        {
            return DAL_NNL.HienThiMuaNguyenLieu(); // Gọi hàm từ DAL
        }
        public List<object> HienThiChiTietMua(string maMua)
        {
            return DAL_NNL.HienThiChiTietMua(maMua);
        }
        // Hàm gọi từ DAL để lấy chi tiết mua theo mã mua
        public List<object> HienThiThongTinDonMua(string maMua)
        {
            // Gọi phương thức trong DAL để lấy thông tin chi tiết mua
            return DAL_NNL.HienThiThongTinDonMua(maMua);
        }
        // Hàm gọi DAL để lấy tên nguyên liệu từ mã nguyên liệu
        public string LayTenNguyenLieu(string maNguyenLieu)
        {
            return DAL_NNL.LayTenNguyenLieu(maNguyenLieu);
        }
        // Hàm gọi DAL để lấy thông tin chi tiết đơn mua
        public ChiTietMua HienThiChiTietDonMua(string maMua)
        {
            return DAL_NNL.HienThiChiTietDonMua(maMua);
        }

        // Gọi phương thức DAL để lấy thông tin mua nguyên liệu
        public MuaNguyenLieu GetMuaNguyenLieuByMaMua(string maMua)
        {
            return DAL_NNL.HienThiThongTinMuaNguyenLieu(maMua);
        }

        // Phương thức gọi hàm ThemDonNhapNguyenLieu từ DAL
        public bool ThemDonNhapNguyenLieu(MuaNguyenLieu muanguyenlieu, List<ChiTietMua> CT_Mua)
        {
            try
            {
                // Gọi hàm ThemDonNhapNguyenLieu từ DAL_NhapNguyenLieu
                return DAL_NNL.ThemDonNhapNguyenLieu(muanguyenlieu, CT_Mua);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi trong lớp BLL nếu cần
                Console.WriteLine("Lỗi trong BLL khi gọi hàm ThemDonNhapNguyenLieu: " + ex.Message);
                return false;
            }
        }
    }
}
