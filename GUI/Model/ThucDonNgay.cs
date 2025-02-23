using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Model
{
    public class ThucDonNgay
    {
        public List<MonAn> Sang { get; set; }    // Các món ăn bữa sáng
        public List<MonAn> Trua { get; set; }    // Các món ăn bữa trưa
        public List<MonAn> Chieu { get; set; }   // Các món ăn bữa chiều
    }

}
