using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_NhanVien
    {
        private DAL_NhanVien dalNhanVien = new DAL_NhanVien();

        public BLL_NhanVien()
        {
        }
        public List<NhanVien> GetAllMaNhanVien()
        {
            try
            {
                return dalNhanVien.GetAllMaNhanVien();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xử lý nghiệp vụ (Nhân viên): " + ex.Message);
                return new List<NhanVien>();
            }
        }

        // Phương thức lấy thông tin chi tiết nhân viên dựa trên MaTaiKhoan
        public NhanVien GetNhanVienDetailsByMaTaiKhoan(int maTaiKhoan)
        {
            return dalNhanVien.GetNhanVienDetailsByMaTaiKhoan(maTaiKhoan);
        }
        public NhanVien GetNhanVien_MaNV(string MaNV)
        {
            return dalNhanVien.GetNhanVien_MaNV(MaNV);
        }

        ////////////////////////////////
        ///

        public List<TaiKhoan> GetTaiKhoan()
        {
            return dalNhanVien.GetTaiKhoan();
        }

        public TaiKhoan GetTaiKhoanById(string maTaiKhoan)
        {
            return dalNhanVien.GetTaiKhoanById(maTaiKhoan);
        }

        public bool AddTaiKhoan(TaiKhoan taiKhoan)
        {

            return dalNhanVien.AddTaiKhoan(taiKhoan);
        }

        // Phương thức lấy toàn bộ danh sách nhân viên
        public List<NhanVien> GetNhanVien()
        {
            return dalNhanVien.GetNhanVien();
        }
        // Phương thức lấy thông tin chi tiết nhân viên dựa trên MaTaiKhoan

        public bool XoaTaiKhoan(int maTaiKhoan)
        {
            return dalNhanVien.XoaTaiKhoan(maTaiKhoan); // Gọi DAL để xóa tài khoản
        }


        public bool AddNhanVien(NhanVien newNhanVien)
        {
            // Kiểm tra dữ liệu trước khi thêm
            if (newNhanVien == null)
            {
                throw new ArgumentNullException("Nhân viên không hợp lệ.");
            }

            // Gọi phương thức từ DAL để thêm nhân viên
            bool result = dalNhanVien.AddNhanVien(newNhanVien);

            return result;
        }

        // Phương thức xóa nhân viên
        public bool XoaNhanVien(string maNV)
        {
            // Gọi phương thức từ DAL để xóa nhân viên
            bool result = dalNhanVien.XoaNhanVien(maNV);

            return result;
        }


        // Phương thức cập nhật thông tin nhân viên
        public bool UpdateNhanVien(NhanVien nhanVienDto)
        {
            return dalNhanVien.UpdateNhanVien(nhanVienDto);
        }

        // Phương thức gọi DAL để cập nhật tài khoản
        public bool UpdateTaiKhoan(TaiKhoan taiKhoan)
        {
            return dalNhanVien.UpdateTaiKhoan(taiKhoan); // Gọi DAL để cập nhật tài khoản
        }


    }
}
