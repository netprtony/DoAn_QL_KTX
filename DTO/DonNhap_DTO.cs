using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DonNhapDTO
    {
       
        // Tên nhà cung cấp
        public string MaDonNhap { get; set; }
        public DateTime NgayNhap { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; }

        // Thông tin Nhà cung cấp
        public string MaNCC { get; set; }
        public string TenNCC { get; set; }
        public string DiaChi { get; set; }

        // Thông tin Nhân viên
        public string MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public string ChucVu { get; set; }


    }

}
