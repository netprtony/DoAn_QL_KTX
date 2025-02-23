using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Model
{
    public class ThucDonHopLy
    {

        private List<MonAn> danhSachMonAn;
        private double nganSachToiDa;
        private Random random = new Random();

        public ThucDonHopLy(List<MonAn> danhSachMonAn, double nganSachToiDa)
        {
            this.danhSachMonAn = danhSachMonAn;
            this.nganSachToiDa = nganSachToiDa;
        }

        public List<List<MonAn>> SinhThucDonToiUu(int soTheHe, int kichThuocQuanThe)
        {
            // Khởi tạo quần thể ban đầu
            List<List<MonAn>> quanThe = KhoiTaoQuanThe(kichThuocQuanThe);

            for (int theHe = 0; theHe < soTheHe; theHe++)
            {
                // Tính fitness cho từng cá thể
                var fitnessValues = quanThe.Select(CalculateFitness).ToList();

                // In ra fitness của các cá thể trong thế hệ này
                Console.WriteLine($"Thế hệ {theHe + 1}:");
                for (int i = 0; i < fitnessValues.Count; i++)
                {
                    Console.WriteLine($"Fitness của thực đơn {i + 1}: {fitnessValues[i]}");
                }

                // Chọn lọc các cá thể tốt nhất
                quanThe = ChonLoc(quanThe, fitnessValues);

                // Thực hiện lai ghép và đột biến
                quanThe = LaiGhepVaDotBien(quanThe);
            }

            // Kiểm tra xem danh sách có phần tử nào không trước khi gọi First()
            var thucDonTuyChon = quanThe.OrderBy(thucDon => CalculateFitness(thucDon)).FirstOrDefault();

            // Nếu danh sách rỗng, trả về null hoặc danh sách mặc định
            if (thucDonTuyChon == null)
            {
                // In thông báo khi không có thực đơn tối ưu
                Console.WriteLine("Không thể tạo thực đơn tối ưu!");
                return new List<List<MonAn>>(); // Hoặc trả về danh sách mặc định khác
            }

            // Trả về thực đơn tốt nhất
            return new List<List<MonAn>> { thucDonTuyChon };
        }



        private List<List<MonAn>> KhoiTaoQuanThe(int kichThuocQuanThe)
        {
            List<List<MonAn>> quanThe = new List<List<MonAn>>();
            for (int i = 0; i < kichThuocQuanThe; i++)
            {
                quanThe.Add(TaoThucDonNgauNhien());
            }
            return quanThe;
        }



        private List<MonAn> TaoThucDonNgauNhien()
        {
            List<MonAn> thucDon = new List<MonAn>();

            for (int ngay = 0; ngay < 7; ngay++)
            {
                // Buổi sáng: Chỉ chọn 1 món sáng
                thucDon.Add(ChonNgauNhien("Sang"));

                // Buổi trưa: 3 món (Canh, Xào, Kho/Chiên)
                thucDon.Add(ChonNgauNhien("Canh"));
                thucDon.Add(ChonNgauNhien("Xao"));
                thucDon.Add(ChonNgauNhien(new[] { "Kho", "Chien" }));

                // Buổi chiều: 3 món (Canh, Xào, Kho/Chiên)
                thucDon.Add(ChonNgauNhien("Canh"));
                thucDon.Add(ChonNgauNhien("Xao"));
                thucDon.Add(ChonNgauNhien(new[] { "Kho", "Chien" }));
            }

            return thucDon;
        }


        private MonAn ChonNgauNhien(string loaiMon)
        {
            var danhSach = danhSachMonAn.Where(m => m.LoaiMon == loaiMon).ToList();
            return danhSach[random.Next(danhSach.Count)];
        }

        private MonAn ChonNgauNhien(string[] loaiMon)
        {
            var danhSach = danhSachMonAn.Where(m => loaiMon.Contains(m.LoaiMon)).ToList();
            return danhSach[random.Next(danhSach.Count)];
        }

        public double CalculateFitness(List<MonAn> thucDon)
        {
            double tongChiPhi = thucDon.Sum(m => m.ChiPhi);
            double tongCalo = thucDon.Sum(m => m.Calo);
            double tongProtein = thucDon.Sum(m => m.Protein);
            double tongCarb = thucDon.Sum(m => m.Carb);
            double tongFat = thucDon.Sum(m => m.Fat);

            double penalty = 0;
            if (tongChiPhi > nganSachToiDa) penalty += (tongChiPhi - nganSachToiDa) * 2;
            if (tongCalo < 2000) penalty += (2000 - tongCalo) * 0.5;
            if (tongProtein < 100) penalty += (100 - tongProtein) * 1; // Protein ít hơn yêu cầu
            if (tongCarb < 150) penalty += (150 - tongCarb) * 0.7; // Carb ít hơn yêu cầu
            if (tongFat < 50) penalty += (50 - tongFat) * 0.8; // Fat ít hơn yêu cầu

            return tongChiPhi + penalty;
        }


        private List<List<MonAn>> ChonLoc(List<List<MonAn>> quanThe, List<double> fitnessValues)
        {
            int soLuongChon = quanThe.Count / 2; // Chọn 50% cá thể tốt nhất
            return quanThe.Zip(fitnessValues, (thucDon, fitness) => new { thucDon, fitness })
                          .OrderBy(x => x.fitness)
                          .Take(soLuongChon)
                          .Select(x => x.thucDon)
                          .ToList();
        }

        private List<List<MonAn>> LaiGhepVaDotBien(List<List<MonAn>> quanThe)
        {
            List<List<MonAn>> quanTheMoi = new List<List<MonAn>>(quanThe);

            // Lai ghép
            for (int i = 0; i < quanThe.Count / 2; i++)
            {
                var parent1 = quanThe[random.Next(quanThe.Count)];
                var parent2 = quanThe[random.Next(quanThe.Count)];
                quanTheMoi.Add(LaiGhep(parent1, parent2));
            }

            // Đột biến
            for (int i = 0; i < quanTheMoi.Count; i++)
            {
                if (random.NextDouble() < 0.1) // 10% xác suất đột biến
                {
                    quanTheMoi[i] = DotBien(quanTheMoi[i]);
                }
            }

            return quanTheMoi;
        }

        private List<MonAn> LaiGhep(List<MonAn> parent1, List<MonAn> parent2)
        {
            int diemCat = random.Next(1, parent1.Count - 1);
            return parent1.Take(diemCat).Concat(parent2.Skip(diemCat)).ToList();
        }

        private List<MonAn> DotBien(List<MonAn> thucDon)
        {
            int viTri = random.Next(thucDon.Count);
            string loaiMon = thucDon[viTri].LoaiMon;
            thucDon[viTri] = ChonNgauNhien(loaiMon);
            return thucDon;
        }
    }
}
