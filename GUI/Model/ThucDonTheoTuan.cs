using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Model
{
    public class ThucDonTheoTuan
    {
        public string NgayThu { get; set; } // Thứ trong tuần (ví dụ: Thứ Hai, Thứ Ba...)
        public string Sang { get; set; } // Món ăn cho bữa sáng
        public string Trua { get; set; } // Món ăn cho bữa trưa
        public string Chieu { get; set; } // Món ăn cho bữa chiều
    }

}
