using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ThongKeDonNhap
    {
        public string MaThietBi { get; set; }
        public string TenThietBi { get; set; }
        public int TongSoLuongNhap { get; set; }

        public int Thang { get; set; }        
        public decimal TongTien { get; set; } // Tổng tiền nhập trong tháng đó
    }
}
