using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BLL
{
    public class BLL_BaoCao
    {
      private  DAL_BaoCao dal_bc =new DAL_BaoCao();
       public BLL_BaoCao() { }

        // Phương thức gọi hàm từ DAL để lấy thống kê tỷ lệ sinh viên theo năm
        public List<TyLeSinhVienOTang> GetThongKeTyLeSinhVienTheoNam(int nam)
        {
            // Gọi hàm từ DAL_BaoCao để lấy dữ liệu
            return dal_bc.GetThongKeTyLeSinhVienTheoNam(nam);
        }
    }
}
