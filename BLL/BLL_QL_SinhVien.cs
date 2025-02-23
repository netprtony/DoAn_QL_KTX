using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
using Accord.MachineLearning;

//using Accord.Statistics.Kernels;


namespace BLL
{
    public class BLL_QL_SinhVien
    {
        DAL_QL_SinhVien dalSinhVien = new DAL_QL_SinhVien();
        public BLL_QL_SinhVien()
        {

        }

        public List<SinhVien> GetAllSinhViens()
        {
            return dalSinhVien.GetSinhViens();
        }

        public SinhVien GetSinhVienByMa(string maSinhVien)
        {
            return dalSinhVien.GetByMa(maSinhVien);
        }

        public bool AddSinhVien(SinhVien sinhVien)
        {
            return dalSinhVien.ThemSinhVien(sinhVien);
        }
        public bool ThemDanhSachSinhVien(List<SinhVien> sinhVienList)
        {
            foreach (var sinhVien in sinhVienList)
            {
               
                if (!dalSinhVien.ThemSinhVien(sinhVien)) // ThemSinhVien là phương thức thêm một sinh viên

                {
                    return false; // Nếu thêm một sinh viên thất bại, trả về false
                }
            }
            return true; // Nếu tất cả sinh viên đều được thêm thành công
        }

        private string RemoveSpecialChar(string input, char charToRemove)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Sử dụng Replace để loại bỏ ký tự đặc biệt
            return input.Replace(charToRemove.ToString(), string.Empty);
        }

        public bool UpdateSinhVien(SinhVien sinhVien)
        { 
            return dalSinhVien.CapNhatSinhVien(sinhVien);
        }
        public List<SinhVien> GetSinhVienChuaDangKyPhong()
        {
            return dalSinhVien.GetSinhVienChuaDangKyPhong();
        }

            public bool DeleteSinhVien(string maSoSinhVien)
        {
            return dalSinhVien.DeleteSinhVien(maSoSinhVien);
        }


        public void XepPhong()
        {
            //// Lấy danh sách sinh viên
            //List<SinhVien> sinhViens = dalSinhVien.GetSinhViens();

            //// Mảng chứa dữ liệu cần phân loại (Ví dụ: các nguyện vọng của sinh viên)
            //List<double[]> data = new List<double[]>();  // Sử dụng double[] thay vì int[]

            //// Chuyển nguyện vọng của sinh viên thành dữ liệu cho thuật toán K-means
            //foreach (var sv in sinhViens)
            //{
            //    // Chuyển các nguyện vọng từ chuỗi nvarchar thành các giá trị số và đảm bảo kiểu double[]
            //    double[] nguyenVongData = new double[]
            //    {
            //ConvertToDouble(sv.NV1), // Mã hóa nguyện vọng NV1 (ví dụ: "Yên tĩnh")
            //ConvertToDouble(sv.NV2), // Mã hóa nguyện vọng NV2 (ví dụ: "Dậy sớm")
            //ConvertToDouble(sv.NV3)  // Mã hóa nguyện vọng NV3 (ví dụ: "Gần bạn bè")
            //    };
            //    data.Add(nguyenVongData);
            //}

            //// Sử dụng thuật toán K-means để phân nhóm sinh viên
            //KMeans kmeans = new KMeans(k: 3); // Số nhóm phòng cần phân (3 phòng)
            //kmeans.Learn(data.ToArray());  // Cung cấp dữ liệu là mảng double[]

            //// Lấy chỉ số nhóm của từng sinh viên từ thuộc tính ClusterIndexes (hoặc ClusterAssignments)
            //int[] clusters = kmeans.Decide(data.ToArray()); // Trả về chỉ số nhóm cho mỗi sinh viên

            //// Gán phòng cho từng sinh viên
            //for (int i = 0; i < sinhViens.Count; i++)
            //{
            //    int cluster = clusters[i]; // Lấy nhóm của mỗi sinh viên từ mảng clusters
            //    string maPhong = "Phong" + (cluster + 1); // Gán phòng tương ứng

            //    // Thêm phiếu đăng ký phòng cho sinh viên
            //    dalSinhVien.ThemDangKyPhong(sinhViens[i].MaSinhVien, maPhong);
            //}

            //Console.WriteLine("Xếp phòng cho sinh viên thành công.");
        }

        // Hàm chuyển đổi nguyện vọng từ chuỗi nvarchar thành số kiểu double
        private double ConvertToDouble(string nguyenVong)
        {
            if (string.IsNullOrEmpty(nguyenVong))
            {
                return 0.0; // Nếu không có nguyện vọng, mặc định là 0.0
            }

            // Mã hóa các nguyện vọng thành số (dùng kiểu double để tương thích với KMeans)
            switch (nguyenVong.Trim().ToLower())
            {
                case "yên tĩnh":
                    return 0.0;
                case "dậy sớm":
                    return 1.0;
                case "gần bạn bè":
                    return 2.0;
                default:
                    return 0.0; // Nếu nguyện vọng không có trong danh sách, mặc định là 0.0
            }

            //public bool ThemDangKyPhong(string maSinhVien, string maPhong)
            //{
            //    return dalDangKyPhong.Insert(new DangKyPhong
            //    {
            //        MaSinhVien = maSinhVien,
            //        MaPhong = maPhong,
            //        NgayDangKy = DateTime.Now
            //    });
            //}
  
        
        }
        public List<SinhVien> GetSinhViensDaDangKyPhong()
        {
            // Lấy danh sách sinh viên đã đăng ký phòng
            return dalSinhVien.GetSinhViensDaDangKyPhong();
        }

        public List<SinhVien> GetSinhViensChuaDangKyPhong()
        {
            // Lấy danh sách sinh viên chưa đăng ký phòng
            return dalSinhVien.GetSinhVienChuaDangKyPhong();
        }

    }
}
