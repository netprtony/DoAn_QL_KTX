using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_NhaCungCap
    {
        DA_QL_KTXDataContext ktx = new DA_QL_KTXDataContext();
        public DAL_NhaCungCap()
        {

        }
        // Hàm lấy thông tin nhà cung cấp theo tên
        public NhaCungCap GetNhaCungCapByName(string tenNCC)
        {
            try
            {
                // Tìm nhà cung cấp với tên chính xác từ cơ sở dữ liệu
                var nhaCungCap = ktx.NhaCungCaps
                                    .FirstOrDefault(ncc => ncc.TenNCC== tenNCC);

                if (nhaCungCap != null)
                {
                    // Trả về đối tượng NhaCungCap nếu tìm thấy
                    return nhaCungCap;


                }
                else
                {
                    // Trường hợp không tìm thấy nhà cung cấp, trả về null
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("Lỗi khi lấy thông tin nhà cung cấp: " + ex.Message);
                return null; // Trả về null nếu có lỗi
            }
        }


        public NhaCungCap GetNhaCungCapByMa(string maNCC)
        {
            try
            {
                // Tìm nhà cung cấp với tên chính xác từ cơ sở dữ liệu
                var nhaCungCap = ktx.NhaCungCaps
                                    .FirstOrDefault(ncc => ncc.MaNCC == maNCC);

                if (nhaCungCap != null)
                {
                    // Trả về đối tượng NhaCungCap nếu tìm thấy
                    return nhaCungCap;


                }
                else
                {
                    // Trường hợp không tìm thấy nhà cung cấp, trả về null
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("Lỗi khi lấy thông tin nhà cung cấp: " + ex.Message);
                return null; // Trả về null nếu có lỗi
            }
        }
        // Hàm load tất cả danh sách tên nhà cung cấp
        public List<NhaCungCap> GetAllNhaCungCapNames()
        {
            try
            {
                // Truy vấn danh sách tất cả nhà cung cấp từ cơ sở dữ liệu
                var danhSachNhaCungCap = ktx.NhaCungCaps.ToList();
                return danhSachNhaCungCap;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có và trả về danh sách rỗng
                Console.WriteLine("Lỗi khi lấy danh sách nhà cung cấp: " + ex.Message);
                return new List<NhaCungCap>();
            }
        }

    }
}
