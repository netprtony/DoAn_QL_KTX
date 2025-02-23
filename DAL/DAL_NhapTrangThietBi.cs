using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_NhapTrangThietBi
    {
        DA_QL_KTXDataContext ktx = new DA_QL_KTXDataContext();
        // Phương thức thêm đơn nhập trang thiết bị
        public bool ThemDonNhapTTB(DonNhap_TTB donNhap, List<CT_DonNhap> chiTietDonNhap)
        {
            try
            {
                // Lấy mã đơn nhập lớn nhất từ cơ sở dữ liệu
                var maxMaDonNhap = ktx.DonNhap_TTBs
                    .Where(d => d.MaDonNhap.StartsWith("DN")) // Lọc theo các mã bắt đầu bằng "DN"
                    .OrderByDescending(d => d.MaDonNhap)
                    .FirstOrDefault();

                string newMaDonNhap = "DN001"; // Mặc định là DN001 nếu chưa có dữ liệu

                if (maxMaDonNhap != null)
                {
                    // Lấy phần số sau chữ "DN" và cộng 1
                    int maxNumber = int.Parse(maxMaDonNhap.MaDonNhap.Substring(2)); // Bỏ "DN" và lấy phần số
                    newMaDonNhap = "DN" + (maxNumber + 1).ToString("D3"); // Tạo mã mới với số tự động tăng
                }

                // Gán mã đơn nhập mới cho donNhap
                donNhap.MaDonNhap = newMaDonNhap;

                // Thêm đơn nhập vào bảng DonNhap_TTB
                ktx.DonNhap_TTBs.InsertOnSubmit(donNhap);

                // Thêm các chi tiết đơn nhập vào bảng CT_DonNhap
                foreach (var chiTiet in chiTietDonNhap)
                {
                    chiTiet.MaDonNhap = newMaDonNhap; // Gán mã đơn nhập cho chi tiết
                    ktx.CT_DonNhaps.InsertOnSubmit(chiTiet); // Thêm chi tiết vào bảng CT_DonNhap
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                ktx.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                // Nếu có lỗi, hiển thị thông báo lỗi và rollback
                Console.WriteLine("Lỗi khi thêm đơn nhập trang thiết bị và chi tiết: " + ex.Message);
                return false;
            }
        }

    }
}
