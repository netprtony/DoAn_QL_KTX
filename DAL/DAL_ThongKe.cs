using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_ThongKe
    {
        DA_QL_KTXDataContext ktx = new DA_QL_KTXDataContext();
        // Gọi thủ tục usp_ThongKeChiPhi và trả về danh sách ThongKeChiPhi
        // Gọi thủ tục ThongKeChiPhi và trả về danh sách ThongKeChiPhi cho toàn bộ năm
        public List<ThongKeChiPhi> GetThongKeChiPhi(int nam)
        {
            // Thực thi thủ tục qua LINQ to SQL và trả về danh sách kết quả cho tất cả các tháng trong năm
            var result = ktx.ExecuteQuery<ThongKeChiPhi>(
                "EXEC ThongKeChiPhi @Nam = {0}", nam).ToList();

            return result;
        }
        // Gọi thủ tục ThongKeChiPhiTheoNam và trả về danh sách ThongKeChiPhi
        public List<ThongKeChiPhiThu> GetThongKeChiPhiThu(int nam)
        {
            // Thực thi thủ tục qua ExecuteQuery và trả về danh sách kết quả cho tất cả các tháng trong năm
            var result = ktx.ExecuteQuery<ThongKeChiPhiThu>(
                "EXEC ThongKeChiPhiTheoNam @Nam = {0}", nam).ToList();

            return result;
        }
    }
}
