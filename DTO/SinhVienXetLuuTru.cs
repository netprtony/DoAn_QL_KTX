using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SinhVienXetLuuTru
    {
        public int STT;
       // public string MaDangKyPhong { get; set; }  // Mã đăng ký phòng
        public string MaSinhVien { get; set; }     // Mã sinh viên
        public string TenSinhVien { get; set; }    // Tên sinh viên
        public DateTime NgaySinh { get; set; }     // Ngày sinh viên
        public DateTime? NgayDK { get; set; }      // Ngày đăng ký
        public string MaPhong { get; set; }        // Mã phòng
        public int Tang { get; set; }              // Tầng của phòng
       // public int SoLuongSinhVienToiDa { get; set; } // Số lượng sinh viên tối đa
       // public DateTime? NgayBD { get; set; }      // Ngày bắt đầu ở
       // public DateTime? NgayKT { get; set; }      // Ngày kết thúc ở
       // public string HinhThucThanhToan { get; set; } // Hình thức thanh toán
    }
}
