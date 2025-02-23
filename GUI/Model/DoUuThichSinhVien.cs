using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Model
{
    public class DoUuThichSinhVien
    {
        public string MaSinhVien { get; set; }  // Mã sinh viên
        public bool AnChay { get; set; }         // Tình trạng ăn chay của sinh viên
        public List<string> MonAnUaThich { get; set; }  // Danh sách món ăn ưa thích của sinh viên

        // Constructor để khởi tạo đối tượng với các tham số
        public DoUuThichSinhVien(string maSinhVien, bool anChay, List<string> monAnUaThich)
        {
            MaSinhVien = maSinhVien;
            AnChay = anChay;
            MonAnUaThich = monAnUaThich;
        }
    }
}
