using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SinhVienDTO
    {
        public string MSSV { get;set; }
        public string HoTen { get; set; }
        public bool TruongPhong { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string NoiSinh { get; set; }
        public string HoKhauThuongTru { get; set; }
        public int? nv1 { get; set; }

        public int? nv3 { get; set; }
        public int? nv2 { get; set; }
        public int? SoTang { get; set; }
        public Phong Phong { get; set; } // Thay đổi để chứa thông tin về phòng
        public string HinhNhanDien { get; set; }    

       
    }
}
