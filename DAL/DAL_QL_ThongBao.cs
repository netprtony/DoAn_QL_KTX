using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_QL_ThongBao
    {
        DA_QL_KTXDataContext ktx = new DA_QL_KTXDataContext();

        public bool ThemThongBaoVaDangThongBao(ThongBao thongBao, DangThongBao dangThongBao)
        {
            try
            {
                // Bước 1: Thêm vào bảng ThongBao
                ThongBao tb = new ThongBao
                {
                    TieuDe = thongBao.TieuDe,
                    NoiDung = thongBao.NoiDung,
                    NgayTao = thongBao.NgayTao,
                    NgayHetHan = thongBao.NgayHetHan
                };

                ktx.ThongBaos.InsertOnSubmit(tb);
                ktx.SubmitChanges(); // Lưu thay đổi để lấy mã thông báo được tạo

                // Lấy mã thông báo vừa được thêm (ID tự tăng)
                int maThongBao = tb.MaThongBao;

                // Bước 2: Thêm vào bảng DangThongBao
                DangThongBao dtb = new DangThongBao
                {
                    MaThongBao = maThongBao,
                    MaNhanVien = dangThongBao.MaNhanVien,
                    TieuDe = dangThongBao.TieuDe,
                    FileTB = dangThongBao.FileTB,
                    NgayDang = dangThongBao.NgayDang
                };

                ktx.DangThongBaos.InsertOnSubmit(dtb);
                ktx.SubmitChanges(); // Lưu thay đổi để hoàn tất

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm thông báo và đăng thông báo: " + ex.Message);
            }
        }

    }
}
