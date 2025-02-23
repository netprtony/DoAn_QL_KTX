using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public partial class DangKyPhong
    {
        string hinhNhanDien;

        public string HinhNhanDien { get => hinhNhanDien; set => hinhNhanDien = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public string SDT { get => sDT; set => sDT = value; }
        public string Email { get => email; set => email = value; }
        public string CCCD { get => cCCD; set => cCCD = value; }
        public string LoaiPhong_ { get => loaiPhong; set => loaiPhong = value; }
        public decimal DonGiaPhong { get => donGiaPhong; set => donGiaPhong = value; }

        string hoTen;
        string sDT;
        string email;
        string cCCD;
        string loaiPhong;
        decimal donGiaPhong;
    }
}
