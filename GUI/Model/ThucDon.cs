using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.Statistics.Filters;
using System.Data;
using Accord.Math;
using Accord.Statistics.Models.Regression;  // Giả sử bạn đang sử dụng thư viện Accord.NET
using System.Collections.Generic;
using Accord.Statistics.Filters;  // Chứa Codification



namespace GUI.Model
{
    public class ThucDon
    {
        private List<MonAn> danhSachMonAn;
        private double nganSachToiDa;
        private Codification codebook;
        private DecisionTree tree;

        public ThucDon(List<MonAn> monAnList, double nganSach)
        {
            danhSachMonAn = monAnList;
            nganSachToiDa = nganSach;
        }

        // Hàm xây dựng cây quyết định
        public void XayDungCayQuyetDinh()
        {
            // Chuyển đổi danh sách món ăn thành DataTable
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("LoaiMon", typeof(string));
            dataTable.Columns.Add("Calo", typeof(double));
            dataTable.Columns.Add("Protein", typeof(double));
            dataTable.Columns.Add("Carb", typeof(double));
            dataTable.Columns.Add("Fat", typeof(double));
            dataTable.Columns.Add("ChiPhi", typeof(double));
            dataTable.Columns.Add("TenMon", typeof(string));

            foreach (var monAn in danhSachMonAn)
            {
                dataTable.Rows.Add(monAn.LoaiMon, monAn.Calo, monAn.Protein, monAn.Carb, monAn.Fat, monAn.ChiPhi, monAn.Ten);
            }

            // Mã hóa dữ liệu
            codebook = new Codification(dataTable);
            DataTable symbols = codebook.Apply(dataTable);

            // Lấy các giá trị từ DataTable vào các danh sách
    var caloValues = symbols.AsEnumerable().Select(row => row.Field<double>("Calo")).ToList();
    var proteinValues = symbols.AsEnumerable().Select(row => row.Field<double>("Protein")).ToList();
    var carbValues = symbols.AsEnumerable().Select(row => row.Field<double>("Carb")).ToList();
    var fatValues = symbols.AsEnumerable().Select(row => row.Field<double>("Fat")).ToList();
    var chiPhiValues = symbols.AsEnumerable().Select(row => row.Field<double>("ChiPhi")).ToList();

    // Chuyển các biến liên tục thành rời rạc (Binning)
    var binnedCalo = Binning(caloValues, 3);
    var binnedProtein = Binning(proteinValues, 3);
    var binnedCarb = Binning(carbValues, 3);
    var binnedFat = Binning(fatValues, 3);
    var binnedChiPhi = Binning(chiPhiValues, 3);

    // Cập nhật lại các cột trong DataTable với giá trị đã binning
    for (int i = 0; i < symbols.Rows.Count; i++)
    {
        symbols.Rows[i]["Calo"] = binnedCalo[i];
        symbols.Rows[i]["Protein"] = binnedProtein[i];
        symbols.Rows[i]["Carb"] = binnedCarb[i];
        symbols.Rows[i]["Fat"] = binnedFat[i];
        symbols.Rows[i]["ChiPhi"] = binnedChiPhi[i];
    }

            // Chuẩn bị dữ liệu cho ID3
            int[][] inputs = symbols.ToArray<int>("LoaiMon", "Calo", "Protein", "Carb", "Fat", "ChiPhi");
            int[] outputs = symbols.ToArray<int>("TenMon");

            // Khởi tạo cây quyết định
            var decisionVariables = new DecisionVariable[] {
        new DecisionVariable("LoaiMon", codebook["LoaiMon"].Symbols),
        new DecisionVariable("Calo", codebook["Calo"].Symbols), // Bây giờ "Calo" là giá trị rời rạc
        new DecisionVariable("Protein", codebook["Protein"].Symbols),
        new DecisionVariable("Carb", codebook["Carb"].Symbols),
        new DecisionVariable("Fat", codebook["Fat"].Symbols),
        new DecisionVariable("ChiPhi", codebook["ChiPhi"].Symbols)
    };

            tree = new DecisionTree(decisionVariables, codebook["TenMon"].Symbols);

            // Huấn luyện cây quyết định
            var id3Learning = new ID3Learning(tree);
            id3Learning.Learn(inputs, outputs);
        }

        // Phương thức binning để chuyển các giá trị liên tục thành các nhóm
        private List<int> Binning(List<double> values, int numBins)
        {
            List<int> binnedValues = new List<int>();
            double min = values.Min();
            double max = values.Max();
            double binSize = (max - min) / numBins;

            foreach (var value in values)
            {
                int bin = (int)((value - min) / binSize); // Chuyển giá trị vào nhóm
                bin = Math.Min(bin, numBins - 1); // Đảm bảo bin không vượt quá số nhóm
                binnedValues.Add(bin);
            }

            return binnedValues;
        }




        // Hàm dự đoán thực đơn hợp lý
        public Dictionary<string, Dictionary<string, List<string>>> DuDoanThucDonHopLy(double caloSang, double caloTrua, double caloChieu)
        {

            XayDungCayQuyetDinh();
            // Kết quả: Thực đơn cho từng ngày trong tuần
            Dictionary<string, Dictionary<string, List<string>>> thucDonTuan = new Dictionary<string, Dictionary<string, List<string>>>();

            // Từ điển ánh xạ Loại món ăn -> số nguyên (mã hóa)
            Dictionary<string, int> loaiMonMapping = new Dictionary<string, int>
    {
        { "Sang", 1 }, { "Trua", 2 }, { "Chieu", 3 },
        { "Canh", 4 }, { "Xao", 5 }, { "Chien", 6 }, { "Kho", 7 }
    };

            // Khởi tạo ngân sách tối đa
            double nganSachConLai = nganSachToiDa;

            // Các ngày trong tuần
            string[] ngayTrongTuan = { "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7", "Chủ nhật" };

            foreach (var ngay in ngayTrongTuan)
            {
                // Khởi tạo thực đơn cho ngày hiện tại
                Dictionary<string, List<string>> thucDonNgay = new Dictionary<string, List<string>>
        {
            { "BuoiSang", new List<string>() },
            { "BuoiTrua", new List<string>() },
            { "BuoiChieu", new List<string>() }
        };

                // Lặp qua danh sách món ăn để xây dựng thực đơn
                foreach (var monAn in danhSachMonAn)
                {
                    // Bỏ qua nếu không đủ ngân sách
                    if (monAn.ChiPhi > nganSachConLai)
                        continue;

                    // Mã hóa thuộc tính LoaiMon
                    if (!loaiMonMapping.TryGetValue(monAn.LoaiMon, out int encodedLoaiMon))
                    {
                        throw new Exception($"Loại món ăn không hợp lệ: {monAn.LoaiMon}");
                    }

                    // Chuẩn bị đầu vào cho mô hình
                    int[] encodedInput = new int[]
                    {
                encodedLoaiMon,           // Loại món ăn (mã hóa)
                (int)monAn.Calo,          // Lượng calo
                (int)monAn.Protein,       // Lượng protein
                (int)monAn.Carb,          // Lượng carb
                (int)monAn.Fat,           // Lượng fat
                (int)monAn.ChiPhi         // Chi phí
                    };

                    // Dự đoán tên món ăn
                    int predicted = tree.Decide(encodedInput);

                    // Giải mã tên món ăn
                    string tenMon = codebook.Revert("TenMon", predicted);

                    // Logic thêm món vào thực đơn cho từng ngày
                    if (monAn.LoaiMon == "Sang" && thucDonNgay["BuoiSang"].Count == 0 && monAn.Calo >= caloSang)
                    {
                        thucDonNgay["BuoiSang"].Add(tenMon);
                        nganSachConLai -= monAn.ChiPhi;
                        continue;
                    }

                    if (monAn.LoaiMon == "Trua" && thucDonNgay["BuoiTrua"].Count < 3 && monAn.Calo >= caloTrua / 3)
                    {
                        // Đảm bảo có đủ món Canh, Xào, Chiên/Kho
                        if (!thucDonNgay["BuoiTrua"].Any(m => m.Contains("Canh")) && monAn.LoaiMon == "Canh")
                        {
                            thucDonNgay["BuoiTrua"].Add(tenMon);
                        }
                        else if (!thucDonNgay["BuoiTrua"].Any(m => m.Contains("Xao")) && monAn.LoaiMon == "Xao")
                        {
                            thucDonNgay["BuoiTrua"].Add(tenMon);
                        }
                        else if (!thucDonNgay["BuoiTrua"].Any(m => m.Contains("Chien") || m.Contains("Kho")) &&
                                 (monAn.LoaiMon == "Chien" || monAn.LoaiMon == "Kho"))
                        {
                            thucDonNgay["BuoiTrua"].Add(tenMon);
                        }

                        nganSachConLai -= monAn.ChiPhi;
                        continue;
                    }

                    if (monAn.LoaiMon == "Chieu" && thucDonNgay["BuoiChieu"].Count < 3 && monAn.Calo >= caloChieu / 3)
                    {
                        // Đảm bảo có đủ món Canh, Xào, Chiên/Kho
                        if (!thucDonNgay["BuoiChieu"].Any(m => m.Contains("Canh")) && monAn.LoaiMon == "Canh")
                        {
                            thucDonNgay["BuoiChieu"].Add(tenMon);
                        }
                        else if (!thucDonNgay["BuoiChieu"].Any(m => m.Contains("Xao")) && monAn.LoaiMon == "Xao")
                        {
                            thucDonNgay["BuoiChieu"].Add(tenMon);
                        }
                        else if (!thucDonNgay["BuoiChieu"].Any(m => m.Contains("Chien") || m.Contains("Kho")) &&
                                 (monAn.LoaiMon == "Chien" || monAn.LoaiMon == "Kho"))
                        {
                            thucDonNgay["BuoiChieu"].Add(tenMon);
                        }

                        nganSachConLai -= monAn.ChiPhi;
                    }
                }

                // Lưu thực đơn cho ngày vào từ điển của tuần
                thucDonTuan[ngay] = thucDonNgay;
            }

            return thucDonTuan;
        }



    }

    // Lớp MonAn và các logic khác tương tự như trước

}
