using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ThongKeDienNuoc
    {
        public string MaPhong { get; set; }
        public int ChiSoDienCu { get; set; }
        public int ChiSoDienMoi { get; set; }
        public int SoDienTieuThu { get; set; }
        public decimal DonGiaDien { get; set; }
        public decimal ThanhTienDien { get; set; }
        public int ChiSoNuocCu { get; set; }
        public int ChiSoNuocMoi { get; set; }
        public int SoNuocTieuThu { get; set; }
        public decimal DonGiaNuoc { get; set; }
        public decimal ThanhTienNuoc { get; set; }
        public decimal TongTien { get; set; }
    }
}
