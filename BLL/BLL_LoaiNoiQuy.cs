using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class BLL_LoaiNoiQuy
    {
        private DAL_LoaiNoiQuy dalLoaiNoiQuy;

        public BLL_LoaiNoiQuy()
        {
            dalLoaiNoiQuy = new DAL_LoaiNoiQuy();
        }

        // Phương thức gọi DAL để lấy mã loại nội quy theo tên loại nội quy
        public int? GetMaLoaiNoiQuyByTenLoai(string tenLoaiNoiQuy)
        {
            return dalLoaiNoiQuy.GetMaLoaiNoiQuyByTenLoai(tenLoaiNoiQuy);
        }

        // Lấy danh sách loại nội quy
        public List<LoaiNoiQuy> GetAllLoaiNoiQuy()
        {
            return dalLoaiNoiQuy.GetAllLoaiNoiQuy();
        }

        // Xem chi tiết loại nội quy
        public LoaiNoiQuy GetLoaiNoiQuyById(int maLoaiNQ)
        {
            return dalLoaiNoiQuy.GetLoaiNoiQuyById(maLoaiNQ);
        }

        // Thêm loại nội quy
        public bool AddLoaiNoiQuy(LoaiNoiQuy loaiNoiQuy)
        {
            return dalLoaiNoiQuy.AddLoaiNoiQuy(loaiNoiQuy);
        }
        // Phương thức gọi DAL để xóa loại nội quy
        public bool XoaLoaiNoiQuy(int maLoaiNoiQuy)
        {
            // Gọi hàm XoaLoaiNoiQuy từ DAL
            return dalLoaiNoiQuy.XoaLoaiNoiQuy(maLoaiNoiQuy);
        }

        // Sửa loại nội quy
        public bool UpdateLoaiNoiQuy(LoaiNoiQuy loaiNoiQuy)
        {
            return dalLoaiNoiQuy.UpdateLoaiNoiQuy(loaiNoiQuy);
        }
        public List<NoiQuy> GetNoiQuyByLoaiNoiQuy(string tenLoaiNoiQuy)
        {
            return dalLoaiNoiQuy.GetNoiQuyByTenLoaiNQ(tenLoaiNoiQuy);
        }
        // Hàm gọi đến DAL để lấy danh sách tên nội quy theo tên loại nội quy
        public List<string> GetNoiQuyByTenLoaiNoiQuy(string tenLoaiNoiQuy)
        {
            return dalLoaiNoiQuy.GetNoiQuyByTenLoaiNoiQuy(tenLoaiNoiQuy);
        }

        // Phương thức gọi hàm GetLoaiNoiQuyByName từ DAL
        public LoaiNoiQuy GetLoaiNoiQuyByNameBLL(string tenLoaiNoiQuy)
        {
            return dalLoaiNoiQuy.GetLoaiNoiQuyByName(tenLoaiNoiQuy);
        }

    }
}
