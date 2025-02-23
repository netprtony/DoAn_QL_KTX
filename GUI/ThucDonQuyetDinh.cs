using GUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
   
    public class ThucDonQuyetDinh
    {
        // Hàm phân loại loại món ăn (Canh, Xào, Kho/Chiên) dựa trên tên món
       private string PhanLoaiLoaiMon(string tenMon)
{
    if (string.IsNullOrEmpty(tenMon)) return "Khác"; // Kiểm tra tên món trống

    if (tenMon.IndexOf("Canh", StringComparison.OrdinalIgnoreCase) >= 0)
        return "Canh";
    if (tenMon.IndexOf("Xào", StringComparison.OrdinalIgnoreCase) >= 0)
        return "Xào";
    if (tenMon.IndexOf("Kho", StringComparison.OrdinalIgnoreCase) >= 0 || tenMon.IndexOf("Chiên", StringComparison.OrdinalIgnoreCase) >= 0)
        return "Kho/Chiên";

    return "Khác";  // Nếu không khớp với bất kỳ loại món nào, trả về "Khác"
}



        // Hàm kiểm tra món ăn có đáp ứng ngưỡng dinh dưỡng không
        private bool KiemTraDinhDuong(MonAn mon, double caloMin, double proteinMin, double carbMin, double fatMin)
        {
            return mon.Calo >= caloMin &&
                   mon.Protein >= proteinMin &&
                   mon.Carb >= carbMin &&
                   mon.Fat >= fatMin;
        }

        // Hàm lọc danh sách món ăn theo loại món và nguyện vọng dinh dưỡng
        public List<MonAn> LocDanhSachMonAn(
            List<MonAn> danhSachMonAn,
            string loaiThucDon,
            double caloMin,
            double proteinMin,
            double carbMin,
            double fatMin)
        {
            return danhSachMonAn
                .Where(mon =>
                    mon.LoaiMon.Equals(loaiThucDon, StringComparison.OrdinalIgnoreCase) &&
                    KiemTraDinhDuong(mon, caloMin, proteinMin, carbMin, fatMin))
                .ToList();
        }

        // Hàm chọn thực đơn hợp lý (Canh, Xào, Kho/Chiên) cho bữa trưa/tối
        public List<MonAn> TimThucDonDayDu(List<MonAn> danhSachLoc)
        {
            var thucDon = new List<MonAn>();

            var canh = danhSachLoc.FirstOrDefault(mon => PhanLoaiLoaiMon(mon.Ten) == "Canh");
            var xao = danhSachLoc.FirstOrDefault(mon => PhanLoaiLoaiMon(mon.Ten) == "Xào");
            var khoChien = danhSachLoc.FirstOrDefault(mon => PhanLoaiLoaiMon(mon.Ten) == "Kho/Chiên");

            if (canh != null) thucDon.Add(canh);
            if (xao != null) thucDon.Add(xao);
            if (khoChien != null) thucDon.Add(khoChien);

            // Đảm bảo đủ 3 loại món
            return thucDon.Count == 3 ? thucDon : new List<MonAn>();
        }

        // Hàm chọn thực đơn đơn giản (chỉ cần 1 món) cho bữa sáng
        public List<MonAn> TimThucDonDonGian(List<MonAn> danhSachLoc)
        {
            return danhSachLoc.OrderBy(mon => mon.ChiPhi).Take(1).ToList(); // Lấy món rẻ nhất
        }

        // Hàm chọn thực đơn tùy theo bữa ăn (sáng, trưa/tối)
        public List<MonAn> TimThucDonTheoBuoi(
            List<MonAn> danhSachMonAn,
            string loaiThucDon,
            string buoiAn,
            double caloMin,
            double proteinMin,
            double carbMin,
            double fatMin)
        {
            var danhSachLoc = LocDanhSachMonAn(danhSachMonAn, loaiThucDon, caloMin, proteinMin, carbMin, fatMin);

            return buoiAn.Equals("Sáng", StringComparison.OrdinalIgnoreCase)
                ? TimThucDonDonGian(danhSachLoc)
                : TimThucDonDayDu(danhSachLoc);
        }

    }
}
