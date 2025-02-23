using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_SinhVien
    {

        DAL_SinhVien dal_sinhvien = new DAL_SinhVien();
        public BLL_SinhVien()
        {

        }
        // Xem chi tiết loại nội quy
        public SinhVien GetLoaiNoiQuyById(string MaSinhVien)
        {
            return dal_sinhvien.GetinhVienByID(MaSinhVien);
        }

        public SinhVienDTO GetSinhVienPhong(string MaSinhVien)
        {
            return dal_sinhvien.GetSinhVienPhong(MaSinhVien);
        }
    }
}
