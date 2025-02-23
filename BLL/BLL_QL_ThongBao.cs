using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BLL
{
    public class BLL_QL_ThongBao
    {
        DAL_QL_ThongBao dal = new DAL_QL_ThongBao();

        public BLL_QL_ThongBao()
        {

        }
        public bool ThemThongBaoVaDangThongBao(ThongBao thongBao, DangThongBao dangThongBao)
        {
            try
            {
                return dal.ThemThongBaoVaDangThongBao(thongBao, dangThongBao);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi trong BLL: " + ex.Message);
            }
        }
    }
}
