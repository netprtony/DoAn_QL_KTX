using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public  class ThongTinDienNuoc
    {
        public int SoLuongSV { get; set; }
        public string MaSV_TP { get; set; }
        public string HoTen_TP { get; set; }
        public string Email_TP { get; set; }
        public decimal? DonGiaDien { get; set; }
        public decimal? DonGiaNuoc { get; set; }
        public int? ChiSoDienCu { get; set; }
        public int? ChiSoNuocCu { get; set; }
        public string MaDienNuoc { get; set; } 
        public string TenLoaiPhong { get; set; } 
    }
}
