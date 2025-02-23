using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using DTO;

namespace DAL
{
    public class DAL_LoaiNoiQuy
    {
        DA_QL_KTXDataContext ktx = new DA_QL_KTXDataContext();

        public DAL_LoaiNoiQuy()
        {
        }
        // Hàm lấy mã loại nội quy dựa vào tên loại nội quy
        public int? GetMaLoaiNoiQuyByTenLoai(string tenLoaiNoiQuy)
        {
            // Tìm loại nội quy dựa vào tên loại nội quy (so sánh không phân biệt chữ hoa chữ thường)
            var loaiNoiQuy = ktx.LoaiNoiQuys
                                .FirstOrDefault(lnq => string.Equals(lnq.TenLoaiNQ, tenLoaiNoiQuy));

            // Kiểm tra nếu tìm thấy, trả về mã loại nội quy
            if (loaiNoiQuy != null)
            {
                return loaiNoiQuy.MaLoaiNQ;
            }

            // Nếu không tìm thấy, trả về null
            return null;
        }
        // Lấy danh sách loại nội quy
        // Hàm load tất cả danh sách loại nội quy
        public List<LoaiNoiQuy> GetAllLoaiNoiQuy()
        {
            try
            {
                // Truy vấn danh sách tất cả loại nội quy từ cơ sở dữ liệu, chỉ lấy những bản ghi có TrangThai = 0
                var danhSachLoaiNoiQuy = ktx.LoaiNoiQuys.Where(lnq => lnq.TrangThai == 0).ToList();
                return danhSachLoaiNoiQuy;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có và trả về danh sách rỗng
                Console.WriteLine("Lỗi khi lấy danh sách loại nội quy: " + ex.Message);
                return new List<LoaiNoiQuy>();
            }
        }




        // Xem chi tiết loại nội quy
        public LoaiNoiQuy GetLoaiNoiQuyById(int maLoaiNQ)
        {
            var loaiNoiQuy = ktx.LoaiNoiQuys.FirstOrDefault(lnq => lnq.MaLoaiNQ == maLoaiNQ);
            if (loaiNoiQuy != null)
            {
                return new LoaiNoiQuy
                {
                    MaLoaiNQ = loaiNoiQuy.MaLoaiNQ,
                    TenLoaiNQ = loaiNoiQuy.TenLoaiNQ,
                    MoTa = loaiNoiQuy.MoTa,
                };
            }
            return null;
        }

        // Thêm loại nội quy
        public bool AddLoaiNoiQuy(LoaiNoiQuy loaiNoiQuy)
        {
            try
            {
                // Lấy mã loại nội quy cao nhất từ cơ sở dữ liệu
                var maxMaLoaiNQ = ktx.LoaiNoiQuys.Max(lnq => (int?)lnq.MaLoaiNQ) ?? 0;

                // Tạo mã loại nội quy mới là mã cao nhất cộng thêm 1
                int newMaLoaiNQ = maxMaLoaiNQ + 1;

                // Tạo đối tượng LoaiNoiQuy mới với mã loại nội quy mới và các thông tin khác
                var newLoaiNoiQuy = new DTO.LoaiNoiQuy
                {
                    MaLoaiNQ = newMaLoaiNQ, // Gán mã loại nội quy mới
                    TenLoaiNQ = loaiNoiQuy.TenLoaiNQ,
                    MoTa = loaiNoiQuy.MoTa,
                    TrangThai = 0 // Mặc định trạng thái là 0 khi thêm mới
                };

                // Thêm đối tượng vào danh sách và lưu thay đổi vào cơ sở dữ liệu
                ktx.LoaiNoiQuys.InsertOnSubmit(newLoaiNoiQuy);
                ktx.SubmitChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                return true; // Thêm thành công
            }
            catch (Exception ex)
            {
                // Log exception nếu cần
                return false; // Thêm thất bại
            }
        }
        //xoá loại nội quy
        // Hàm cập nhật trạng thái loại nội quy thành 1 (xóa)
        public bool XoaLoaiNoiQuy(int maLoaiNoiQuy)
        {
            try
            {
                // Tìm loại nội quy theo mã
                var loaiNoiQuy = ktx.LoaiNoiQuys.FirstOrDefault(l => l.MaLoaiNQ == maLoaiNoiQuy);

                if (loaiNoiQuy != null)
                {
                    // Cập nhật trạng thái thành 1 (đã xóa)
                    loaiNoiQuy.TrangThai = 1;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    ktx.SubmitChanges();
                    return true;
                }
                else
                {
                    return false; // Không tìm thấy loại nội quy
                }
            }
            catch (Exception)
            {
                return false; // Xử lý lỗi
            }
        }

        // Sửa loại nội quy
        public bool UpdateLoaiNoiQuy(LoaiNoiQuy loaiNoiQuy)
        {
            try
            {
                var existingLoaiNoiQuy = ktx.LoaiNoiQuys.FirstOrDefault(lnq => lnq.MaLoaiNQ == loaiNoiQuy.MaLoaiNQ);
                if (existingLoaiNoiQuy != null)
                {
                    existingLoaiNoiQuy.TenLoaiNQ = loaiNoiQuy.TenLoaiNQ;
                    existingLoaiNoiQuy.MoTa=loaiNoiQuy.MoTa;
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

        public List<NoiQuy> GetNoiQuyByTenLoaiNQ(string tenLoaiNoiQuy)
        {
            // Tìm loại nội quy dựa trên tên loại nội quy
            var loaiNoiQuy = ktx.LoaiNoiQuys
                                .FirstOrDefault(lnq => lnq.TenLoaiNQ == tenLoaiNoiQuy);

            // Nếu tìm thấy loại nội quy, lọc danh sách nội quy theo MaLoaiNQ
            if (loaiNoiQuy != null)
            {
                var danhSachNoiQuyEntities = ktx.NoiQuys
                    .Where(nq => nq.MaLoaiNQ == loaiNoiQuy.MaLoaiNQ)
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

            // Trả về danh sách rỗng nếu không tìm thấy loại nội quy
            return new List<NoiQuy>();
        }
        // Lấy danh sách tên nội quy theo tên loại nội quy
        public List<string> GetNoiQuyByTenLoaiNoiQuy(string tenLoaiNoiQuy)
        {
            // Lấy mã loại nội quy dựa vào tên loại nội quy
            var loaiNoiQuy = ktx.LoaiNoiQuys.FirstOrDefault(lnq => lnq.TenLoaiNQ == tenLoaiNoiQuy);

            if (loaiNoiQuy == null)
            {
                return new List<string>(); // Trả về danh sách rỗng nếu không tìm thấy
            }

            int maLoaiNQ = loaiNoiQuy.MaLoaiNQ;

            // Lấy danh sách tên nội quy theo mã loại nội quy
            var danhSachNoiQuy = ktx.NoiQuys
                                    .Where(nq => nq.MaLoaiNQ == maLoaiNQ)
                                    .Select(nq => nq.TenNoiQuy)
                                    .ToList();

            return danhSachNoiQuy;
        }
        //lấy các trường còn lại khi truyền vào tên loại nội quy ạ 
        public LoaiNoiQuy GetLoaiNoiQuyByName(string tenLoaiNoiQuy)
        {
            try
            {
                // Truy vấn dữ liệu từ bảng LoaiNoiQuy để tìm loại nội quy theo tên
                var loaiNoiQuy = ktx.LoaiNoiQuys.SingleOrDefault(l => l.TenLoaiNQ == tenLoaiNoiQuy);

                if (loaiNoiQuy != null)
                {
                    // Nếu tìm thấy, trả về loại nội quy
                    return loaiNoiQuy;
                }
                else
                {
                    // Nếu không tìm thấy, trả về null
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có lỗi xảy ra
               // MessageBox.Show("Lỗi khi truy vấn dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


    }
}
