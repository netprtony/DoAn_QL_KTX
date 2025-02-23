using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ThongKeSuaChua
    {
        public string MaThietBi { get; set; }  // Mã thiết bị
        public string TenThietBi { get; set; } // Tên thiết bị
        public int SoLuongSuaChua { get; set; } // Tổng số lượng sửa chữa
        public decimal TongChiPhiSuaChua { get; set; } // Tổng chi phí sửa chữa
      
    }
}
