using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using DTO;

namespace DAL
{
    public class DAL_NoiQuy
    {
        DA_QL_KTXDataContext ktx = new DA_QL_KTXDataContext();

        public DAL_NoiQuy()
        {
        }
        public string GetTenLoaiNoiQuy(int maLoaiNQ)
        {
            try
            {
                // Tìm loại nội quy theo mã loại nội quy
                var loaiNoiQuy = ktx.LoaiNoiQuys.FirstOrDefault(lnq => lnq.MaLoaiNQ == maLoaiNQ);
                return loaiNoiQuy?.TenLoaiNQ; // Trả về tên loại nội quy (hoặc null nếu không tìm thấy)
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy tên loại nội quy");
            }
        }

        // Lấy MaLoaiNQ dựa trên MaNoiQuy
        public int? GetMaLoaiNQByMaNoiQuy(int maNoiQuy)
        {
            try
            {
                // Tìm bản ghi trong bảng NoiQuy có MaNoiQuy khớp
                var noiQuy = ktx.NoiQuys.FirstOrDefault(nq => nq.MaNoiQuy == maNoiQuy);

                // Nếu tìm thấy, trả về MaLoaiNQ, nếu không trả về null
                return noiQuy?.MaLoaiNQ;
            }
            catch (Exception)
            {
                // Xử lý ngoại lệ (nếu có) và trả về null
                return null;
            }
        }

        // Lấy MaNoiQuy dựa trên TenNoiQuy
        public int? GetMaNoiQuy(string tenNoiQuy)
        {
            try
            {
                // Tìm bản ghi trong bảng NoiQuy có TenNoiQuy khớp
                var noiQuy = ktx.NoiQuys.FirstOrDefault(nq => nq.TenNoiQuy == tenNoiQuy);

                // Nếu tìm thấy, trả về MaNoiQuy, nếu không trả về null
                return noiQuy?.MaNoiQuy;
            }
            catch (Exception)
            {
                // Xử lý ngoại lệ (nếu có) và trả về null
                return null;
            }
        }

        // Lấy MaLoaiNQ dựa trên TenLoaiNQ
        public int? GetMaLoaiNQ(string tenLoaiNQ)
        {
            try
            {
                // Tìm bản ghi có TenLoaiNQ khớp
                var loaiNoiQuy = ktx.LoaiNoiQuys.FirstOrDefault(l => l.TenLoaiNQ == tenLoaiNQ);

                // Nếu tìm thấy, trả về MaLoaiNQ, nếu không trả về null
                return loaiNoiQuy?.MaLoaiNQ;
            }
            catch (Exception)
            {
                // Xử lý ngoại lệ (nếu có) và trả về null
                return null;
            }
        }

        // Lấy danh sách nội quy
        public List<NoiQuy> GetAllNoiQuy()
        {
            // Lấy danh sách thực thể từ ngữ cảnh dữ liệu, chỉ lấy các bản ghi có TrangThai = 0
            var danhSachNoiQuy = ktx.NoiQuys.Where(nq => nq.TrangThai == 0).ToList();

            // Chuyển đổi sang danh sách DTO
            return danhSachNoiQuy.Select(nq => new NoiQuy
            {
                MaNoiQuy = nq.MaNoiQuy,
                TenNoiQuy = nq.TenNoiQuy,
                MucPhatTien = nq.MucPhatTien,
                HinhThucXL = nq.HinhThucXL,
                MaLoaiNQ = nq.MaLoaiNQ // Khóa ngoại
            }).ToList();
        }


        // Xem chi tiết nội quy
        public NoiQuy GetNoiQuyById(int maNoiQuy)
        {
            var noiQuy = ktx.NoiQuys.FirstOrDefault(nq => nq.MaNoiQuy == maNoiQuy);
            if (noiQuy != null)
            {
                return new NoiQuy
                {
                    MaNoiQuy = noiQuy.MaNoiQuy,
                    TenNoiQuy = noiQuy.TenNoiQuy,
                    MucPhatTien = noiQuy.MucPhatTien,
                    HinhThucXL = noiQuy.HinhThucXL,
                    MaLoaiNQ = noiQuy.MaLoaiNQ // Khóa ngoại
                };
            }
            return null;
        }
        public int GetNextMaNoiQuy()
        {
            try
            {
                // Lấy giá trị MaNoiQuy lớn nhất trong bảng NoiQuy
                var maxMaNoiQuy = ktx.NoiQuys.Max(nq => (int?)nq.MaNoiQuy) ?? 0;
                // Trả về MaNoiQuy kế tiếp
                return maxMaNoiQuy + 1;
            }
            catch (Exception)
            {
                return 1; // Nếu có lỗi (ví dụ bảng trống), trả về 1 là mã nội quy đầu tiên
            }
        }


        // Thêm nội quy
        public bool AddNoiQuy(NoiQuy noiQuy)
        {
            try
            {
                // Tạo MaNoiQuy mới bằng cách lấy giá trị lớn nhất và cộng 1
                noiQuy.MaNoiQuy = GetNextMaNoiQuy();

                // Tạo đối tượng NoiQuy mới
                var newNoiQuy = new NoiQuy
                {
                    MaNoiQuy = noiQuy.MaNoiQuy,
                    TenNoiQuy = noiQuy.TenNoiQuy,
                    MucPhatTien = noiQuy.MucPhatTien,
                    HinhThucXL = noiQuy.HinhThucXL,
                    MaLoaiNQ = noiQuy.MaLoaiNQ, // Khóa ngoại
                    TrangThai = noiQuy.TrangThai
                };

                // Thêm mới vào cơ sở dữ liệu
                ktx.NoiQuys.InsertOnSubmit(newNoiQuy);
                ktx.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                // In ra thông báo lỗi nếu có
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        // Sửa nội quy
        public bool UpdateNoiQuy(NoiQuy noiQuy)
        {
            try
            {
                var existingNoiQuy = ktx.NoiQuys.FirstOrDefault(nq => nq.MaNoiQuy == noiQuy.MaNoiQuy);
                if (existingNoiQuy != null)
                {
                    existingNoiQuy.TenNoiQuy = noiQuy.TenNoiQuy;
                    existingNoiQuy.MucPhatTien = noiQuy.MucPhatTien;
                    existingNoiQuy.HinhThucXL = noiQuy.HinhThucXL;
                   // existingNoiQuy.MaLoaiNQ = noiQuy.MaLoaiNQ; // Cập nhật khóa ngoại

                    ktx.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Xóa nội quy
        public bool DeleteNoiQuy(int maNoiQuy)
        {
            try
            {
                // Tìm nội quy có mã nội quy tương ứng
                var noiQuy = ktx.NoiQuys.FirstOrDefault(nq => nq.MaNoiQuy == maNoiQuy);
                if (noiQuy != null)
                {
                    // Cập nhật trạng thái của nội quy thành 1
                    noiQuy.TrangThai = 1;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    ktx.SubmitChanges();
                    return true;
                }
                return false; // Không tìm thấy nội quy với mã đã cho
            }
            catch (Exception)
            {
                return false; // Bắt lỗi nếu có vấn đề xảy ra
            }
        }


        // Lấy danh sách nội quy theo mã loại nội quy
        public List<NoiQuy> GetNoiQuyByMaLoaiNQ(int maLoaiNQ)
        {
            // Lấy danh sách thực thể từ ngữ cảnh dữ liệu
            var danhSachNoiQuyEntities = ktx.NoiQuys
                                             .Where(nq => nq.MaLoaiNQ == maLoaiNQ && nq.TrangThai == 0) // Thêm điều kiện TrangThai = 0
                                             .ToList();

            // Chuyển đổi danh sách thực thể thành danh sách DTO.NoiQuy
            var danhSachNoiQuy = danhSachNoiQuyEntities.Select(nq => new NoiQuy
            {
                MaNoiQuy = nq.MaNoiQuy,
                TenNoiQuy = nq.TenNoiQuy,
                MucPhatTien = nq.MucPhatTien,
                HinhThucXL = nq.HinhThucXL,
                MaLoaiNQ = nq.MaLoaiNQ
            }).ToList();

            return danhSachNoiQuy;
        }


        // Hàm lấy các nội dung còn lại từ TenNoiQuy
        public NoiQuy GetNoiQuyByTenNoiQuy(string tenNoiQuy)
        {
            try
            {
                // Tìm kiếm nội quy theo tên nội quy trong bảng NoiQuy
                var noiQuy = ktx.NoiQuys
                                .FirstOrDefault(nq => nq.TenNoiQuy == tenNoiQuy);

                if (noiQuy != null)
                {
                    // Trả về đối tượng NoiQuy nếu tìm thấy
                    return noiQuy;
                }
                else
                {
                    // Trường hợp không tìm thấy nội quy, trả về null
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("Lỗi: " + ex.Message);
                return null;
            }
        }


    }
}
