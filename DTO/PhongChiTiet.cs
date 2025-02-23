using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PhongChiTiet
    {
        public string TenPhong { get; set; }
        public int? ChiSoDien { get; set; }
        public int? ChiSoNuoc { get; set; }
        public decimal DonGiaDien { get; set; }
        public decimal DonGiaNuoc { get; set; }
    }
}
