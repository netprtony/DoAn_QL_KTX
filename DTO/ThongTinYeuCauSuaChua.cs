using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ThongTinYeuCauSuaChua
    {
        public string MaYCSuaChua { get; set; }
        public string MaNhanVien { get; set; }
        public DateTime? NgayLap { get; set; }
        public DateTime? NgayHoanTat { get; set; }
        public string TrangThai { get; set; }
        public decimal? TongTien { get; set; }
        public string MaThietBi { get; set; }
        public string TenThietBi { get; set; }
        public string MaPhong { get; set; }
        public int? SoLuong { get; set; }
        public string TinhTrang { get; set; }
        public decimal? PhiSuaChua { get; set; }
    }
}
