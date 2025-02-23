using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_NhaCungCap
    {
        DAL_NhaCungCap dal_ncc;
        public BLL_NhaCungCap()
        {
            dal_ncc = new DAL_NhaCungCap();
        }
        // Phương thức gọi từ DAL để lấy thông tin nhà cung cấp
        // Gọi hàm lấy thông tin nhà cung cấp theo tên từ DAL
        public NhaCungCap GetNhaCungCapByName(string tenNCC)
        {
            return dal_ncc.GetNhaCungCapByName(tenNCC);
        }
        public NhaCungCap GetNhaCungCapByMa(string maNCC)
        {
            return dal_ncc.GetNhaCungCapByMa(maNCC);
        }

        // Phương thức lấy tất cả tên nhà cung cấp
        public List<NhaCungCap> GetAllNhaCungCapNames()
        {
            return dal_ncc.GetAllNhaCungCapNames();  // Gọi hàm từ DAL
        }

    }
}
