using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public partial class CTTTB_DTO
    {
        //string tenThietBi;

        //public string TenThietBi { get => tenThietBi; set => tenThietBi = value; }
        public string MaThietBi { get; set; }  // Thuộc tính từ bảng CT_TrangThietBis
        public string MaPhong { get; set; }   // Thuộc tính từ bảng CT_TrangThietBis
        public int? SoLuong { get; set; }      // Thuộc tính từ bảng CT_TrangThietBis
        public string TrangThai { get; set; } // Thuộc tính từ bảng CT_TrangThietBis
        public string TenThietBi { get; set; } // Thuộc tính bổ sung từ bảng TrangThietBis
    }
}
