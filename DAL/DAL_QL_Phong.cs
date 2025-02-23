using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Accord.MachineLearning;
using Accord.Math.Distances;

namespace DAL
{
    public class DAL_QL_Phong
    {
        DA_QL_KTXDataContext ktx= new DA_QL_KTXDataContext();

        public DAL_QL_Phong() { }

        public bool KiemTraTrangThaiPhong(string maPhong)
        {
            // Tìm phòng theo mã phòng
            Phong phong = ktx.Phongs.FirstOrDefault(p => p.MaPhong == maPhong);

            // Kiểm tra nếu phòng tồn tại và trạng thái là "Hoạt động"
            if (phong != null)
            {
                return phong.TrangThai == "Đang hoạt động"; // So sánh với giá trị "Hoạt động" trong CSDL
            }

            // Trả về false nếu không tìm thấy phòng
            return false;
        }
        // Hàm thêm sinh viên vào bảng SinhVien và đăng ký phòng vào bảng DangKyPhong
        public string ThemSinhVienVaDangKyPhong(List<SinhVien> sinhVienList, List<DangKyPhong> dangKyPhongList)
        {
            try
            {
                foreach (var sinhVienDTO in sinhVienList)
                {
                    // Kiểm tra nếu mã sinh viên đã tồn tại
                    var existingSinhVien = ktx.SinhViens.FirstOrDefault(sv => sv.MaSinhVien == sinhVienDTO.MaSinhVien);
                    if (existingSinhVien != null)
                    {
                        return "Mã sinh viên đã tồn tại!";
                    }

                    // Thêm sinh viên vào bảng SinhVien
                    SinhVien newSinhVien = new SinhVien
                    {
                        MaSinhVien = sinhVienDTO.MaSinhVien,
                        HoTen = sinhVienDTO.HoTen,
                        TruongPhong = sinhVienDTO.TruongPhong,
                        NgaySinh = sinhVienDTO.NgaySinh,
                        GioiTinh = sinhVienDTO.GioiTinh,
                        NoiSinh = sinhVienDTO.NoiSinh,
                        HoKhauThuongTru = sinhVienDTO.HoKhauThuongTru,
                        Email = sinhVienDTO.Email,
                        CCCD = sinhVienDTO.CCCD,
                        GhiChu = sinhVienDTO.GhiChu,
                        HinhNhanDien = sinhVienDTO.HinhNhanDien,
                        HinhCCCDTruoc = sinhVienDTO.HinhCCCDTruoc,
                        HinhCCCDSau = sinhVienDTO.HinhCCCDSau
                    };

                    ktx.SinhViens.InsertOnSubmit(newSinhVien);
                    ktx.SubmitChanges(); // Lưu vào database sau khi thêm sinh viên

                    // Sau khi thêm sinh viên thành công, lấy mã sinh viên vừa tạo
                    string maSinhVien = newSinhVien.MaSinhVien;

                    foreach (var dangKyPhongDTO in dangKyPhongList)
                    {
                        // Kiểm tra xem mã sinh viên có trùng khớp với mã đăng ký phòng không
                        if (dangKyPhongDTO.MaSinhVien == maSinhVien)
                        {
                            // Lấy mã đăng ký phòng lớn nhất trong cơ sở dữ liệu
                            var lastDangKyPhong = ktx.DangKyPhongs.OrderByDescending(dkp => dkp.MaDangKyPhong)
                                                                    .FirstOrDefault();

                            string newMaDangKyPhong;

                            if (lastDangKyPhong != null)
                            {
                                // Lấy số từ mã đăng ký phòng cuối cùng
                                string lastCode = lastDangKyPhong.MaDangKyPhong;
                                string numberPart = lastCode.Substring(3); // Lấy phần số sau "DKP"
                                int lastNumber = int.Parse(numberPart);

                                // Cộng thêm 1
                                int newNumber = lastNumber + 1;

                                // Tạo mã mới với cấu trúc DKPxxx (ví dụ: DKP002, DKP003, ...)
                                newMaDangKyPhong = "DKP" + newNumber.ToString("D3");
                            }
                            else
                            {
                                // Nếu chưa có mã nào, tạo mã mới là "DKP001"
                                newMaDangKyPhong = "DKP001";
                            }

                            // Thêm đăng ký phòng vào bảng DangKyPhong
                            DangKyPhong newDangKyPhong = new DangKyPhong
                            {
                                MaDangKyPhong = newMaDangKyPhong, // Sử dụng mã đăng ký phòng mới
                                MaPhong = dangKyPhongDTO.MaPhong,
                                MaSinhVien = maSinhVien, // Sử dụng mã sinh viên đã tạo
                                NgayDK = dangKyPhongDTO.NgayDK,
                                NgayBD = dangKyPhongDTO.NgayBD,
                                NgayKT = dangKyPhongDTO.NgayKT,
                                NV1 = dangKyPhongDTO.NV1,
                                NV2 = dangKyPhongDTO.NV2,
                                NV3 = dangKyPhongDTO.NV3,
                                TrangThai = dangKyPhongDTO.TrangThai
                            };

                            ktx.DangKyPhongs.InsertOnSubmit(newDangKyPhong);
                            ktx.SubmitChanges(); // Lưu đăng ký phòng vào database
                        }
                    }
                }

                return "Thêm sinh viên và đăng ký phòng thành công!";
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("Lỗi khi thêm sinh viên và đăng ký phòng: " + ex.Message);
                return "Lỗi khi thêm sinh viên và đăng ký phòng!";
            }
        }

        private Dictionary<string, int> GetSoLuongSinhVienTrongPhong()
        {
            Dictionary<string, int> soLuongHienTai = new Dictionary<string, int>();

            try
            {
                // Sử dụng DataContext để truy vấn cơ sở dữ liệu
                var soLuongTheoPhong = ktx.DangKyPhongs
                    .GroupBy(dk => dk.MaPhong)
                    .Select(group => new
                    {
                        MaPhong = group.Key,
                        SoLuong = group.Count()
                    }).ToList();

                // Đưa dữ liệu từ kết quả truy vấn vào Dictionary
                foreach (var item in soLuongTheoPhong)
                {
                    soLuongHienTai[item.MaPhong] = item.SoLuong;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy số lượng sinh viên trong phòng: {ex.Message}");
                Console.WriteLine($"Chi tiết lỗi: {ex.StackTrace}");
            }

            return soLuongHienTai;
        }



        public string GetTangForPhong(string maPhong)
        {
            try
            {
                // Lấy phòng từ cơ sở dữ liệu theo mã phòng
                var phong = ktx.Phongs.FirstOrDefault(p => p.MaPhong == maPhong);
                if (phong != null)
                {
                    return phong.Tang.ToString(); // Trả về tầng của phòng
                }
                else
                {
                    return "Không có thông tin tầng"; // Nếu không tìm thấy phòng
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("Lỗi khi lấy tầng phòng: " + ex.Message);
                return "Lỗi";
            }
        }

        public string GetTangForPhongq(string maPhong)
        {
            try
            {
                // Lấy phòng từ cơ sở dữ liệu theo mã phòng
                var phong = ktx.Phongs.FirstOrDefault(p => p.MaPhong == maPhong);
                if (phong != null)
                {
                    return phong.Tang.ToString(); // Trả về tầng của phòng
                }
                else
                {
                    return "Không có thông tin tầng"; // Nếu không tìm thấy phòng
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("Lỗi khi lấy tầng phòng: " + ex.Message);
                return "Lỗi";
            }
        }
        public List<Phong> LayDSPhongThuong()
        {
            var danhSachPhong = ktx.Phongs
                             .Where(p => p.TrangThai == "Đang hoạt động" && p.MaLoaiPhong == 1)
                             .ToList();

            // Chuyển đổi các bản ghi từ bảng Phong sang danh sách DTO
            return danhSachPhong.Select(p => new Phong
            {
                MaPhong = p.MaPhong,
                MaLoaiPhong = p.MaLoaiPhong,
                SoLuongSinhVienToiDa = p.SoLuongSinhVienToiDa,
                TrangThai = p.TrangThai,
                MaDienNuoc = p.MaDienNuoc, // Khóa ngoại
                Tang = p.Tang // Nếu có trường tầng
            }).ToList();
        }
        public Dictionary<string, List<SinhVienDTO>> PhanPhongBangKMeans(List<SinhVienDTO> sinhVienList, List<Phong> danhSachPhong)
        {
            try
            {
                if (sinhVienList == null || !sinhVienList.Any())
                {
                    Console.WriteLine("Danh sách sinh viên không hợp lệ hoặc rỗng.");
                    return new Dictionary<string, List<SinhVienDTO>>();
                }

                if (danhSachPhong == null || !danhSachPhong.Any())
                {
                    Console.WriteLine("Danh sách phòng không hợp lệ hoặc rỗng.");
                    return new Dictionary<string, List<SinhVienDTO>>();
                }

                // Phân loại sinh viên theo giới tính
                Console.WriteLine("Bắt đầu phân loại sinh viên...");
                var sinhVienNam = sinhVienList.Where(sv => sv.GioiTinh == "Nam").ToList();
                var sinhVienNu = sinhVienList.Where(sv => sv.GioiTinh == "Nữ").ToList();

                Console.WriteLine($"Số lượng sinh viên Nam: {sinhVienNam.Count}, Nữ: {sinhVienNu.Count}");

                // Phân loại phòng theo tầng
                var phongNam = danhSachPhong.Where(p => p.Tang >= 6).ToList();
                var phongNu = danhSachPhong.Where(p => p.Tang <= 5).ToList();

                Console.WriteLine($"Số lượng phòng Nam: {phongNam.Count}, Nữ: {phongNu.Count}");

                // Phân phòng cho từng nhóm
                Console.WriteLine("Bắt đầu phân nhóm K-Means cho sinh viên Nam...");
                var ketQuaNam = PhanNhomKMeans(sinhVienNam, phongNam);

                Console.WriteLine("Bắt đầu phân nhóm K-Means cho sinh viên Nữ...");
                var ketQuaNu = PhanNhomKMeans(sinhVienNu, phongNu);

                // Kết hợp kết quả từ hai nhóm
                var ketQuaPhanPhong = new Dictionary<string, List<SinhVienDTO>>();
                foreach (var kvp in ketQuaNam.Concat(ketQuaNu))
                {
                    if (!ketQuaPhanPhong.ContainsKey(kvp.Key))
                    {
                        ketQuaPhanPhong[kvp.Key] = new List<SinhVienDTO>();
                    }
                    ketQuaPhanPhong[kvp.Key].AddRange(kvp.Value);
                }

                Console.WriteLine("Phân phòng hoàn tất!");
                return ketQuaPhanPhong;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong quá trình phân phòng: {ex.Message}");
                Console.WriteLine($"Chi tiết lỗi: {ex.StackTrace}");
                return new Dictionary<string, List<SinhVienDTO>>();
            }
        }

        private Dictionary<string, List<SinhVienDTO>> PhanNhomKMeans(List<SinhVienDTO> sinhVienList, List<Phong> danhSachPhong)
        {
            try
            {
                if (sinhVienList == null || !sinhVienList.Any())
                {
                    Console.WriteLine("Danh sách sinh viên không hợp lệ hoặc rỗng trong K-Means.");
                    return new Dictionary<string, List<SinhVienDTO>>();
                }

                if (danhSachPhong == null || !danhSachPhong.Any())
                {
                    Console.WriteLine("Danh sách phòng không hợp lệ hoặc rỗng trong K-Means.");
                    return new Dictionary<string, List<SinhVienDTO>>();
                }

                Console.WriteLine("Bắt đầu chuyển dữ liệu nguyện vọng thành mảng 2D...");
                double[][] data = sinhVienList.Select(sv => new double[]
                {
            sv.nv1 ?? 0,
            sv.nv2 ?? 0,
            sv.nv3 ?? 0
                }).ToArray();

                int soPhong = danhSachPhong.Count;
                Console.WriteLine($"Số phòng cần phân chia: {soPhong}");

                // Lấy số lượng sinh viên hiện tại trong mỗi phòng
                Dictionary<string, int> soLuongHienTai = GetSoLuongSinhVienTrongPhong();

                // Tạo thuật toán K-Means
                KMeans kmeans = new KMeans(k: soPhong);
                Console.WriteLine("Thuật toán K-Means được khởi tạo.");

                // Áp dụng thuật toán
                var clusters = kmeans.Learn(data);
                int[] labels = clusters.Decide(data);

                Console.WriteLine("Phân nhóm K-Means thành công!");

                // Tạo từ điển lưu kết quả
                Dictionary<string, List<SinhVienDTO>> ketQua = new Dictionary<string, List<SinhVienDTO>>();
                foreach (var phong in danhSachPhong)
                {
                    ketQua[phong.MaPhong] = new List<SinhVienDTO>();
                }

                // Gán sinh viên vào phòng
                for (int i = 0; i < labels.Length; i++)
                {
                    int group = labels[i];
                    string maphong = danhSachPhong[group].MaPhong;

                    // Kiểm tra số lượng sinh viên trong phòng
                    int soLuongHienCo = soLuongHienTai.ContainsKey(maphong) ? soLuongHienTai[maphong] : 0;

                    // Chỉ thêm sinh viên nếu phòng còn chỗ
                    if (soLuongHienCo + ketQua[maphong].Count < 10) // Giới hạn tối đa 10 sinh viên/phòng
                    {
                        ketQua[maphong].Add(sinhVienList[i]);
                    }
                    else
                    {
                        Console.WriteLine($"Phòng {maphong} đã đầy, không thể thêm sinh viên {sinhVienList[i].MSSV}.");
                    }
                }

                return ketQua;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong K-Means: {ex.Message}");
                Console.WriteLine($"Chi tiết lỗi: {ex.StackTrace}");
                return new Dictionary<string, List<SinhVienDTO>>();
            }
        }





        // Phương thức lấy danh sách phòng
        public List<string> LayDanhSachPhong()
        {
            try
            {
                return (from phong in ktx.Phongs
                        select phong.MaPhong).ToList();
            }
            catch (Exception ex)
            {
                // Ghi log thông tin lỗi
                Console.WriteLine("Lỗi khi lấy danh sách phòng: " + ex.Message);

                // Trả về một danh sách rỗng nếu có lỗi
                return new List<string>();
            }
        }


        public int LaySoSV(string maPhong)
        {
            return ktx.DangKyPhongs.Count(dkp=>dkp.MaPhong==maPhong);
        }

        public int LaySoSVToiDa(string maPhong)
        {
            var phong = ktx.Phongs.FirstOrDefault(p => p.MaPhong == maPhong);
            return phong != null ? phong.SoLuongSinhVienToiDa : 0; // Trả về 0 nếu phòng không tồn tại
        }

        public List<Phong> GetPhong()
        {
            return ktx.Phongs.ToList();

        }

        public List<LoaiPhong> GetLoaiPhong()
        {
            return ktx.LoaiPhongs.ToList();
        }
        public List<Phong> GetPhongByLoaiPhong(int maLoaiPhong)
        {
            try
            {
                // Query to filter rooms by room type ID
                return ktx.Phongs.Where(p => p.MaLoaiPhong == maLoaiPhong).ToList();
            }
            catch (Exception ex)
            {
                // Log any error that occurs
                Console.WriteLine("Lỗi khi lấy danh sách phòng theo loại " + ex.Message);

                // Return an empty list if there’s an error
                return new List<Phong>();
            }
        }

        public bool CapNhatPhong(string maPhong, int maLoaiPhong, bool trangThai, int soLuongSVToiDa)
        {
            try
            {
                // Tìm phòng cần cập nhật
                var phong = ktx.Phongs.FirstOrDefault(p => p.MaPhong == maPhong);

                if (phong == null)
                {
                    Console.WriteLine("Phòng không tồn tại với mã: " + maPhong);
                    return false; 
                }

                // Cập nhật thông tin phòng
                phong.MaLoaiPhong = maLoaiPhong;
                if(trangThai)
                {
                    phong.TrangThai = "Đang hoạt động";
                }
                else { phong.TrangThai = "Ngưng hoạt động"; } 
                    
                
                phong.SoLuongSinhVienToiDa = soLuongSVToiDa;

                // Lưu thay đổi vào cơ sở dữ liệu
                ktx.SubmitChanges();

                return true; // Trả về true nếu cập nhật thành công
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu có
                Console.WriteLine("Lỗi khi cập nhật thông tin phòng: " + ex.Message);
                return false; // Trả về false nếu có lỗi
            }
        }


        public List<LoaiPhong> LayLoaiPhong()
        {
            return ktx.LoaiPhongs.ToList();
        }

        public Phong GetPhongTheoMaPhong(string maphong)
        {
           
              return  ktx.Phongs.FirstOrDefault(p => p.MaPhong == maphong);
           
           
        }
        //public List<string> LayDanhSachMaPhong()
        //{
        //     Lấy danh sách mã phòng từ bảng DangKyPhong
        //    return ktx.DangKyPhongs.Select(dkp => dkp.MaPhong).Distinct().ToList();
        //}
    }
}
