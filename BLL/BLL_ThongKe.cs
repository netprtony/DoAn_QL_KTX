using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class BLL_ThongKe
    {
        DAL_ThongKe dal_tk = new DAL_ThongKe();
        DAL_BaoCao dal_bc = new DAL_BaoCao();

        // Phương thức lấy tỷ lệ sử dụng nước theo năm
        public List<TyLeSuDungNuocTheoTang> GetThongKeTiLeSuDungNuoc(int nam)
        {
            // Gọi phương thức từ DAL_BaoCao để lấy tỷ lệ sử dụng nước
            return dal_bc.GetThongKeTiLeSuDungNuocTheoNam(nam);
        }

        // Phương thức lấy tỷ lệ sử dụng điện theo năm
        public List<TyLeSuDungDienTheoTang> GetThongKeTiLeSuDungDien(int nam)
        {
            // Gọi phương thức từ DAL_BaoCao
            return dal_bc.GetThongKeTiLeSuDungDien(nam);
        }
        // Hàm gọi từ UI để lấy danh sách sinh viên xét lưu trú
        public List<SinhVienXetLuuTru> GetDanhSachSinhVienXetLuuTru(int namHoc)
        {
            // Có thể thực hiện thêm xử lý trước khi gọi DAL
            return dal_bc.GetDanhSachSinhVienXetLuuTru(namHoc);
        }

        // Phương thức để lấy thống kê tỷ lệ sinh viên theo năm
        public List<TyLeSinhVienOTang> GetThongKeTyLeSinhVienTheoNam(int nam)
        {
            // Gọi phương thức từ DAL_BaoCao để lấy dữ liệu
            return dal_bc.GetThongKeTyLeSinhVienTheoNam(nam);
          
        }
        // Phương thức để gọi thủ tục và xử lý kết quả
        // Phương thức để lấy thống kê chi phí cho toàn bộ năm
        public List<ThongKeChiPhi> GetThongKeChiPhi(int nam)
        {
            // Gọi DAL để lấy thống kê chi phí
            return dal_tk.GetThongKeChiPhi(nam);
        }
        // Phương thức để gọi thủ tục và xử lý kết quả
        public List<ThongKeChiPhiThu> GetThongKeChiPhiThu(int nam)
        {
            // Gọi DAL để lấy thống kê chi phí
            return dal_tk.GetThongKeChiPhiThu(nam);  // Gọi phương thức GetThongKeChiPhiThu từ DAL
        }
        ////////////////////////////////////////////////////////
      
    }
}
