using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class BLL_NhapTrangThietBi
    {
        DAL_NhapTrangThietBi DAL_NTTB = new DAL_NhapTrangThietBi();
        public BLL_NhapTrangThietBi() { }

        // Phương thức gọi hàm ThemDonNhapTTB
        public bool ThemDonNhapTTB(DonNhap_TTB donNhap, List<CT_DonNhap> chiTietDonNhap)
        {
            try
            {
                // Gọi hàm từ DAL
                return DAL_NTTB.ThemDonNhapTTB(donNhap, chiTietDonNhap);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và hiển thị thông báo
                Console.WriteLine("Lỗi khi gọi hàm ThemDonNhapTTB từ DAL: " + ex.Message);
                return false;
            }
        }
    }
}
