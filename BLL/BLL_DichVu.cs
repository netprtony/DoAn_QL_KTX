using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_DichVu
    {
        DAL_DichVu dal_dv;
        public BLL_DichVu()
        {
            dal_dv = new DAL_DichVu();
        }
        // Hàm gọi GetChiTietDangKyDichVu từ DAL_DichVu
        public DangKyDichVu GetChiTietDangKyDichVu(string maDangKy)
        {
            return dal_dv.GetChiTietDangKyDichVu(maDangKy);
        }
        // Hàm gọi GetDichVuGiacUi từ DAL_DichVu
        public DichVu GetDichVuGiacUi()
        {
            return dal_dv.GetDichVuGiacUi();
        }
        public DichVu LayDichVuCanTin()
        {
            return dal_dv.GetDichVuCanTin();
        }

        // Hàm gọi LapPhieuDangKy từ DAL_DichVu
        public bool LapPhieuDangKy(DangKyDichVu DangKyDV, List<CT_DangKyDV> chiTietDichVu)
        {
            return dal_dv.LapPhieuDangKy(DangKyDV, chiTietDichVu);
        }

        //////PHAN TAO LAM///////////
        public List<DangKyDichVu> GetDanhSachDangKyDichVu()
        {
            return dal_dv.GetDanhSachDangKyDichVu();
        }


        public bool XoaDichVu(string maDichVu)
        {
            try
            {
                return dal_dv.XoaDichVu(maDichVu);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi trong BLL khi xóa đăng ký dịch vụ: {ex.Message}");
            }
        }

        public bool SuaDangKyDichVu(DangKyDichVu dangKyDichVu)
        {
            return dal_dv.CapNhatDangKyDichVu(dangKyDichVu);
        }


        public List<NhanVien> GetDanhSachNhanVien()
        {
            return dal_dv.GetDanhSachNhanVien();
        }

        public List<SinhVien> GetDanhSachSinhVien()
        {
            return dal_dv.GetDanhSachSinhVien();
        }

        public DichVu GetDichVuById(string maDichVu)
        {
            return dal_dv.GetDichVuById(maDichVu);
        }

        public SinhVien GetThongTinSinhVien(string maSinhVien)
        {
            return dal_dv.GetThongTinSinhVien(maSinhVien);
        }

        public DangKyPhong GetThongTinPhongTheoSinhVien(string maSinhVien)
        {
            return dal_dv.GetThongTinPhongTheoSinhVien(maSinhVien);
        }

        public Phong GetThongTinPhong(string maPhong)
        {
            return dal_dv.GetThongTinPhong(maPhong);
        }

        public NhanVien GetThongTinNhanVien(string maNhanVien)
        {
            return dal_dv.GetThongTinNhanVien(maNhanVien);
        }

        public List<CT_DangKyDV> GetThongTinCTDangKyDichVuTheoMaDangKy(string maDangKy)
        {
            // Có thể thêm các xử lý logic nghiệp vụ nếu cần
            return dal_dv.GetThongTinCTDangKyDichVuTheoMaDangKy(maDangKy);
        }

    }
}
