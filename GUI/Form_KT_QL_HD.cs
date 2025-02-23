using BLL;
using DTO;
using GUI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Control;

namespace GUI
{
    public partial class Form_KT_QL_HD : Form
    {
        private BLL_ThanhToanDienNuoc bll_tt_diennuoc;
        private BLL_NhanVien bll_nv;
        // Tạo một instance của BLL_DichVu
        BLL_DichVu bllDichVu = new BLL_DichVu();
        public Form_KT_QL_HD()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            bll_tt_diennuoc = new BLL_ThanhToanDienNuoc();
            bll_nv = new BLL_NhanVien();
            txt_MaPhong.KeyDown += Txt_MaPhong_KeyDown;
            txt_ChiSoDienMoi.KeyDown += Txt_ChiSoDienMoi_KeyDown;



            txt_ChiSoDienMoi.Leave += Txt_ChiSoDienMoi_Leave;
            txt_DNuocMoi.KeyDown += Txt_DNuocMoi_KeyDown;
            btn_ThanhToanDienNuoc.Click += Btn_ThanhToanDienNuoc_Click;
            btn_TK_TTDN_TheoThang.Click += Btn_TK_TTDN_TheoThang_Click;
            datagv_TK_TTDN_TheoThang.Click += Datagv_TK_TTDN_TheoThang_Click;
            btn_Xuat_TKDN_theoThang.Click += Btn_Xuat_TKDN_theoThang_Click;
            dataGV_DS_TT_DN.CellClick += DataGV_DS_TT_DN_CellClick;
            this.Load += ThanhToanPhong_Load;
            txt_MaTT_DN.TextChanged += Txt_MaTT_DN_TextChanged;
            txt_maphongtt.TextChanged += Txt_maphongtt_TextChanged;
            txt_MaNV_TT.TextChanged += Txt_MaNV_TT_TextChanged;
            txt_MSSV_TT.TextChanged += Txt_MSSV_TT_TextChanged;
            cbo_Loc_NhanVien.SelectedIndexChanged += Cbo_Loc_NhanVien_SelectedIndexChanged;
            cbo_Loc_MaThanhToan.SelectedIndexChanged += Cbo_Loc_MaThanhToan_SelectedIndexChanged;
            btn_timkiem_TT_ngaythangnam.Click += Btn_timkiem_TT_ngaythangnam_Click;

            btn_LapDangKy_DV.Click += Btn_LapDangKy_DV_Click;
            txt_MSSV.KeyDown += Txt_MSSV_KeyDown;
           
           
            radio_GiacUi.CheckedChanged += Radio_GiacUi_CheckedChanged;
            radio_CanTin.CheckedChanged += Radio_CanTin_CheckedChanged;

            radio_Ca2.CheckedChanged += Radio_Ca2_CheckedChanged;
            Cbo_LoaiDK_CanTin.SelectedIndexChanged += Cbo_LoaiDK_CanTin_SelectedIndexChanged;
            Cbo_LoaiDK_GiacUi.SelectedIndexChanged += Cbo_LoaiDK_GiacUi_SelectedIndexChanged;
            txt_soluongKg_Giac.TextChanged += Txt_soluongKg_Giac_TextChanged;
            txt_thanhtien_cantin.TextChanged += Txt_thanhtien_cantin_TextChanged;
            txt_thanhTien_GiacUi.TextChanged += Txt_thanhTien_GiacUi_TextChanged;



            LoadDataDangKyDV();
            data_dsdangkydv.Columns["SinhVien"].Visible = false;
            data_dsdangkydv.Columns["NhanVien"].Visible = false;
            cbm_madangky.Items.Clear();
            cbm_madangky.SelectedIndex = -1;
            cbm_madangky.Click += cbm_madangky_Click;
            cbm_madangky.SelectedIndexChanged += cbm_madangky_SelectedIndexChanged;
            cbm_manhanvien.DataSource = null;
            cbm_masinhvien.DataSource = null;
            cbm_trangthai.Items.Add("Hoàn Tất");
            cbm_trangthai.Items.Add("Chưa Hoàn Tất");
            cbm_sapxep.Items.Add("Tăng dần");
            cbm_sapxep.Items.Add("Giảm dần");
            dataGridView_ctdangkydv.AutoGenerateColumns = true;

            cbm_sapxep.SelectedIndexChanged += Cbm_sapxep_SelectedIndexChanged;
            cbm_trangthai.SelectedIndexChanged += Cbm_trangthai_SelectedIndexChanged;
            cbm_masinhvien.SelectedIndexChanged += Cbm_masinhvien_SelectedIndexChanged;

            cbm_manhanvien.SelectedIndexChanged += Cbm_manhanvien_SelectedIndexChanged;
            cbm_masinhvien.Click += Cbm_masinhvien_Click;

            data_dsdangkydv.CellClick += Data_dsdangkydv_CellClick;
            btn_sua.Click += Btn_sua_Click;
           // LoadLoaiDangKy();
        }

        private void Cbm_masinhvien_Click(object sender, EventArgs e)
        {
            // Lấy danh sách nhân viên từ BLL
            List<SinhVien> danhSachSinhVien = bllDichVu.GetDanhSachSinhVien();

            // Lấy danh sách Mã Nhân Viên từ danh sách nhân viên
            var danhSachMaSinhVien = danhSachSinhVien.Select(nv => nv.MaSinhVien).ToList();

            // Gán vào ComboBox
            cbm_masinhvien.DataSource = danhSachMaSinhVien;
        }

        private void Cbm_manhanvien_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy giá trị Mã Nhân Viên đã chọn từ ComboBox
            var maNhanVien = cbm_manhanvien.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(maNhanVien))
            {
                // Lấy danh sách từ BLL
                List<DangKyDichVu> danhSach = bllDichVu.GetDanhSachDangKyDichVu();

                // Lọc danh sách DangKyDichVu theo MaNhanVien
                var filteredList = danhSach.Where(dk => dk.MaNhanVien == maNhanVien).ToList();

                // Gán danh sách đã lọc vào DataGridView
                data_dsdangkydv.DataSource = filteredList;

                // Tự động điều chỉnh kích thước các cột
                data_dsdangkydv.AutoResizeColumns();
            }
            else
            {
                // Nếu không có Mã Nhân Viên nào được chọn, hiển thị lại tất cả dữ liệu
                LoadDataDangKyDV();
            }
        }

        private void Data_dsdangkydv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Kiểm tra xem dòng được chọn có hợp lệ không
                if (e.RowIndex >= 0)
                {
                    // Lấy dòng được chọn
                    DataGridViewRow row = data_dsdangkydv.Rows[e.RowIndex];

                    // Gán giá trị từ DataGridView vào các TextBox và ComboBox
                    txt_madv.Text = row.Cells["MaDangKy"].Value?.ToString() ?? string.Empty;
                    txt_masv.Text = row.Cells["MaSinhVien"].Value?.ToString() ?? string.Empty;
                    txt_manv.Text = row.Cells["MaNhanVien"].Value?.ToString() ?? string.Empty;
                    txt_tongtien.Text = row.Cells["TongTien"].Value?.ToString() ?? string.Empty;
                    txt_trangthai.Text = row.Cells["TrangThai"].Value?.ToString() ?? string.Empty;
                    // Gán giá trị cho ComboBox nếu tồn tại trong danh sách
                    string loaiDangKy = row.Cells["LoaiDangKy"].Value?.ToString();
                    if (!string.IsNullOrEmpty(loaiDangKy) && cbm_loaidangky.Items.Contains(loaiDangKy))
                    {
                        cbm_loaidangky.SelectedItem = loaiDangKy;
                    }
                    else
                    {
                        cbm_loaidangky.Text = loaiDangKy ?? string.Empty;
                    }

                    // Gán giá trị ngày tháng vào DateTimePicker (giả sử cột là "NgayDangKy")
                    if (row.Cells["NgayDangKy"].Value != null)
                    {
                        if (DateTime.TryParse(row.Cells["NgayDangKy"].Value.ToString(), out DateTime ngayDangKy))
                        {
                            dateTimePicker1.Value = ngayDangKy;
                        }
                        else
                        {
                            dateTimePicker1.Value = DateTime.Now; // Giá trị mặc định nếu không hợp lệ
                        }
                    }

                    // Lấy thông tin sinh viên từ BLL và hiển thị
                    var sinhVien = bllDichVu.GetThongTinSinhVien(txt_masv.Text);
                    if (sinhVien != null)
                    {
                        txt_hotensv.Text = sinhVien.HoTen ?? string.Empty;
                    }
                    else
                    {
                        txt_hotensv.Text = string.Empty;
                    }

                    // Lấy thông tin mã phòng từ bảng Đăng Ký Phòng
                    var dangKyPhong = bllDichVu.GetThongTinPhongTheoSinhVien(txt_masv.Text);
                    if (dangKyPhong != null)
                    {
                        txt_maphongsv.Text = dangKyPhong.MaPhong ?? string.Empty;

                        // Lấy thông tin phòng dựa trên mã phòng
                        var phong = bllDichVu.GetThongTinPhong(dangKyPhong.MaPhong);
                        if (phong != null)
                        {
                            txt_loaiphong.Text = phong.MaLoaiPhong.ToString();
                        }
                        else
                        {
                            txt_loaiphong.Text = string.Empty;
                        }
                    }
                    else
                    {
                        txt_maphongsv.Text = string.Empty;
                        txt_loaiphong.Text = string.Empty;
                    }

                    // Lấy thông tin nhân viên từ BLL và hiển thị
                    var nhanVien = bllDichVu.GetThongTinNhanVien(txt_manv.Text);
                    if (nhanVien != null)
                    {
                        txt_hotennv.Text = nhanVien.HoTen ?? string.Empty;
                        txt_chucvu.Text = nhanVien.ChucVu ?? string.Empty;
                    }
                    else
                    {
                        txt_hotennv.Text = string.Empty;
                        txt_chucvu.Text = string.Empty;
                    }

                    // Lấy mã đăng ký từ dòng được chọn
                    string maDangKy = txt_madv.Text;

                    // Lấy danh sách chi tiết đăng ký dịch vụ từ BLL
                    var ctDangKyDichVuList = bllDichVu.GetThongTinCTDangKyDichVuTheoMaDangKy(maDangKy);
                    if (ctDangKyDichVuList != null && ctDangKyDichVuList.Any())
                    {
                        // Gán danh sách vào DataGridView
                        dataGridView_ctdangkydv.DataSource = ctDangKyDichVuList;

                        // Tự động điều chỉnh kích thước các cột
                        dataGridView_ctdangkydv.AutoResizeColumns();
                        dataGridView_ctdangkydv.Refresh();
                    }
                    else
                    {
                        // Nếu không có chi tiết đăng ký, hiển thị thông báo hoặc xóa dữ liệu cũ
                        dataGridView_ctdangkydv.DataSource = null;
                        MessageBox.Show("Không có chi tiết đăng ký dịch vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi chung
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_sua_Click(object sender, EventArgs e)
        {
            try
            {
                string maDangKy = txt_madv.Text;
                string maSinhVien = txt_masv.Text;
                string maNhanVien = txt_manv.Text;
                decimal tongTien = decimal.Parse(txt_tongtien.Text);
                string loaiDangKy = cbm_loaidangky.SelectedItem?.ToString();
                DateTime ngayDangKy = dateTimePicker1.Value;
                string trangThai = txt_trangthai.Text;


                // Tạo đối tượng DangKyDichVu
                DangKyDichVu dangKyDichVu = new DangKyDichVu
                {
                    MaDangKy = maDangKy,
                    MaSinhVien = maSinhVien,
                    MaNhanVien = maNhanVien,
                    TongTien = tongTien,
                    LoaiDangKy = loaiDangKy,
                    NgayDangKy = ngayDangKy,
                    TrangThai = trangThai
                };

                // Gọi BLL để cập nhật
                bool isUpdated = bllDichVu.SuaDangKyDichVu(dangKyDichVu);

                if (isUpdated)
                {
                    MessageBox.Show("Sửa đăng ký dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataDangKyDV(); // Cập nhật lại danh sách
                }
                else
                {
                    MessageBox.Show("Sửa đăng ký dịch vụ thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cbm_masinhvien_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy giá trị Mã Nhân Viên đã chọn từ ComboBox
            var maSinhVien = cbm_masinhvien.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(maSinhVien))
            {
                // Lấy danh sách từ BLL
                List<DangKyDichVu> danhSach = bllDichVu.GetDanhSachDangKyDichVu();

                // Lọc danh sách DangKyDichVu theo MaNhanVien
                var filteredList = danhSach.Where(dk => dk.MaSinhVien == maSinhVien).ToList();

                // Gán danh sách đã lọc vào DataGridView
                data_dsdangkydv.DataSource = filteredList;

                // Tự động điều chỉnh kích thước các cột
                data_dsdangkydv.AutoResizeColumns();
            }
            else
            {
                // Nếu không có Mã Nhân Viên nào được chọn, hiển thị lại tất cả dữ liệu
                LoadDataDangKyDV();
            }
        }

        private void Cbm_trangthai_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy giá trị đã chọn từ ComboBox
            var trangThai = cbm_trangthai.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(trangThai))
            {
                // Lấy danh sách từ BLL
                List<DangKyDichVu> danhSach = bllDichVu.GetDanhSachDangKyDichVu();

                // Lọc danh sách theo trạng thái "Hoàn Tất" hoặc "Chưa Hoàn Tất"
                var filteredList = danhSach.Where(dk => dk.TrangThai == trangThai).ToList();

                // Gán danh sách đã lọc vào DataGridView
                data_dsdangkydv.DataSource = filteredList;

                // Tự động điều chỉnh kích thước các cột
                data_dsdangkydv.AutoResizeColumns();
            }
            else
            {
                // Nếu không có trạng thái nào được chọn, hiển thị lại tất cả dữ liệu
                LoadDataDangKyDV();
            }
        }

        private void Cbm_sapxep_SelectedIndexChanged(object sender, EventArgs e)
        {// Lấy giá trị đã chọn từ ComboBox
            var sapXep = cbm_sapxep.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(sapXep))
            {
                // Lấy danh sách từ BLL
                List<DangKyDichVu> danhSach = bllDichVu.GetDanhSachDangKyDichVu();

                // Kiểm tra lựa chọn và thực hiện sắp xếp
                if (sapXep == "Tăng dần")
                {
                    // Sắp xếp theo Mã Đăng Ký tăng dần (hoặc trường khác nếu cần)
                    danhSach = danhSach.OrderBy(dk => dk.MaDangKy).ToList();
                }
                else if (sapXep == "Giảm dần")
                {
                    // Sắp xếp theo Mã Đăng Ký giảm dần (hoặc trường khác nếu cần)
                    danhSach = danhSach.OrderByDescending(dk => dk.MaDangKy).ToList();
                }

                // Gán danh sách đã sắp xếp vào DataGridView
                data_dsdangkydv.DataSource = danhSach;

                // Tự động điều chỉnh kích thước các cột
                data_dsdangkydv.AutoResizeColumns();
            }
            else
            {
                // Nếu không có lựa chọn nào được chọn, hiển thị lại dữ liệu ban đầu
                LoadDataDangKyDV();
            }

        }

        private void cbm_madangky_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy giá trị Mã Đăng Ký từ ComboBox
            var maDangKy = cbm_madangky.SelectedItem?.ToString();

            // Kiểm tra nếu giá trị Mã Đăng Ký hợp lệ (không phải null hoặc rỗng)
            if (!string.IsNullOrEmpty(maDangKy))
            {
                // Lấy danh sách từ BLL
                List<DangKyDichVu> danhSach = bllDichVu.GetDanhSachDangKyDichVu();

                // Lọc danh sách theo Mã Đăng Ký
                var filteredList = danhSach.Where(dk => dk.MaDangKy == maDangKy).ToList();

                // Gán lại danh sách đã lọc vào DataGridView
                data_dsdangkydv.DataSource = filteredList;

                // Tự động điều chỉnh kích thước các cột
                data_dsdangkydv.AutoResizeColumns();
            }
            else
            {
                // Nếu không có Mã Đăng Ký nào được chọn, hiển thị tất cả dữ liệu
                LoadDataDangKyDV();
            }
        }
        private void cbm_madangky_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu ComboBox đã có dữ liệu
            if (cbm_madangky.Items.Count == 0)
            {
                // Lấy danh sách từ BLL
                List<DangKyDichVu> danhSach = bllDichVu.GetDanhSachDangKyDichVu();

                // Lấy danh sách Mã Đăng Ký và nạp vào ComboBox
                var danhSachMaDangKy = danhSach.Select(dk => dk.MaDangKy).ToList();
                cbm_madangky.DataSource = danhSachMaDangKy;
            }
        }

        private void LoadDataDangKyDV()
        {
            // Lấy danh sách từ BLL
            List<DangKyDichVu> danhSach = bllDichVu.GetDanhSachDangKyDichVu();
            // Lấy danh sách Mã Đăng Ký


            // Gán danh sách vào DataGridView
            data_dsdangkydv.DataSource = danhSach;

            // Tự động điều chỉnh kích thước các cột
            data_dsdangkydv.AutoResizeColumns();
        }

        private void LoadDichVu()
        {
            try
            {
                // Kiểm tra nếu radio_GiacUi được chọn
                if (radio_GiacUi.Checked)
                {


                    // Lấy thông tin dịch vụ "Giặt Ủi"
                    var dichVu = bllDichVu.GetDichVuGiacUi();

                    // Kiểm tra nếu dữ liệu không null
                    if (dichVu != null)
                    {
                        // Hiển thị thông tin lên các TextBox tương ứng
                        txt_MaGiacUi.Text = dichVu.MaDichVu.ToString();
                        txt_TenGiacUi.Text = dichVu.TenDichVu;
                        txt_DonGiaGiacUi.Text = dichVu.GiaDichVu.ToString("N2"); // Định dạng số thập phân
                        txt_DonViTinh_GiacUi.Text = dichVu.DonVi; // Đơn vị tính
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin dịch vụ 'Giặt Ủi'.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadCanTin()
        {
            try
            {
                // Kiểm tra nếu radio_GiacUi được chọn
                if (radio_CanTin.Checked)
                {

                    // Gọi phương thức LayDichVuCanTin từ lớp BLL_DichVu để lấy thông tin dịch vụ Căn Tin
                    var dichVuCanTin = bllDichVu.LayDichVuCanTin();

                    // Kiểm tra nếu tìm thấy dịch vụ
                    if (dichVuCanTin != null)
                    {
                        // Hiển thị thông tin lên các textbox tương ứng
                        txt_MaDV_CanTin.Text = dichVuCanTin.MaDichVu.ToString();
                        txt_TenDV_CanTin.Text = dichVuCanTin.TenDichVu;
                        txt_DonGia_CănTin.Text = dichVuCanTin.GiaDichVu.ToString("N2"); // Hiển thị với 2 chữ số sau dấu phẩy
                        txt_DVT_CanTin.Text = dichVuCanTin.DonVi;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin dịch vụ 'Giặt Ủi'.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Radio_Ca2_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_Ca2.Checked)
            {
                // Tải dữ liệu cho tab Giặt Ủi
                tabControl2.SelectedTab = tab_GiacUi;
                LoadDichVu(); // Gọi hàm tải dữ liệu Giặt Ủi

                // Tải dữ liệu cho tab Căn Tin
                tabControl2.SelectedTab = tab_CanTin;
                LoadCanTin(); // Gọi hàm tải dữ liệu Căn Tin

                // Trở về tab đầu tiên hoặc giữ tab được chọn theo nhu cầu
                tabControl2.SelectedTab = tab_GiacUi; // Nếu muốn mặc định quay về tab Giặt Ủi
            }
        }
        private void Radio_GiacUi_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_GiacUi.Checked)
            {
                //tabControl2.SelectedTab = tab_GiacUi;
                LoadDichVu(); // Tải dữ liệu Giặt Ủi
            }
            else
            {
                txt_thanhtien_cantin.Text = "";
            }
        }
        private void Radio_CanTin_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_CanTin.Checked)
            {
               // tabControl2.SelectedTab = tab_CanTin;
                LoadCanTin(); // Tải dữ liệu Căn Tin
            }
            else
            {
                txt_thanhTien_GiacUi.Text = "";
            }
        }

        private void Txt_thanhTien_GiacUi_TextChanged(object sender, EventArgs e)
        {
            UpdateTongTienDV();
        }

        private void Txt_thanhtien_cantin_TextChanged(object sender, EventArgs e)
        {
            UpdateTongTienDV();
        }


        private void UpdateTongTienDV()
        {
            string TongThanhTien;
            // Lấy giá trị thành tiền của Giặt ủi
            decimal thanhTienGiacUi = 0;
            decimal thanhTienCanTin = 0;
            if (radio_GiacUi.Checked)
            {

                if (!string.IsNullOrWhiteSpace(txt_thanhTien_GiacUi.Text) && decimal.TryParse(txt_thanhTien_GiacUi.Text, out decimal giacUi))
                {
                    thanhTienGiacUi = giacUi;
                    txt_TongTienDV.Text = giacUi.ToString("N2");


                }
            }
            // Lấy giá trị thành tiền của Căn tin

            else if (radio_CanTin.Checked)
            {
                if (!string.IsNullOrWhiteSpace(txt_thanhtien_cantin.Text) && decimal.TryParse(txt_thanhtien_cantin.Text, out decimal canTin))
                {
                    thanhTienCanTin = canTin;
                    txt_TongTienDV.Text = canTin.ToString("N2");
                }
            }
            else if (radio_Ca2.Checked)
            {
                if (!string.IsNullOrWhiteSpace(txt_thanhTien_GiacUi.Text) && decimal.TryParse(txt_thanhTien_GiacUi.Text, out decimal giacUi))
                {
                    thanhTienGiacUi = giacUi;



                }
                if (!string.IsNullOrWhiteSpace(txt_thanhtien_cantin.Text) && decimal.TryParse(txt_thanhtien_cantin.Text, out decimal cantin))
                {
                    thanhTienCanTin = cantin;

                }

                // Cộng tổng hai giá trị
                txt_TongTienDV.Text = (thanhTienGiacUi + thanhTienCanTin).ToString("N2");
            }
            else
            {
                MessageBox.Show("Chưa lựa chọn");
            }
        }


        private void Txt_soluongKg_Giac_TextChanged(object sender, EventArgs e)
        {
            // Đảm bảo các giá trị đầu vào hợp lệ
            if (string.IsNullOrWhiteSpace(txt_DonGiaGiacUi.Text) || !decimal.TryParse(txt_DonGiaGiacUi.Text, out decimal donGia))
            {
                txt_thanhTien_GiacUi.Text = "0.00";
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_soluongKg_Giac.Text) || !decimal.TryParse(txt_soluongKg_Giac.Text, out decimal soLuongKg))
            {
                txt_thanhTien_GiacUi.Text = "0.00";
                return;
            }

            // Tính thành tiền
            decimal thanhTien = donGia * soLuongKg;

            // Gán giá trị cho txt_thanhTien_GiacUi
            txt_thanhTien_GiacUi.Text = thanhTien.ToString("N2"); // Hiển thị với định dạng thập phân
            UpdateTongTienDV();
        }

        private void Cbo_LoaiDK_GiacUi_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem giá trị đã chọn có phải là "Ngày" không
            if (Cbo_LoaiDK_GiacUi.SelectedItem.ToString() == "Ngày")
            {
                // Ngày bắt đầu và kết thúc giống nhau
                dateTime_BD_GiacUi.Value = DateTime.Now; // Ngày hiện tại
                dateTime_KT_GiacUi.Value = DateTime.Now; // Ngày hiện tại
                dateTime_KT_GiacUi.Enabled = false; // Khóa ngày kết thúc
                dateTime_BD_GiacUi.Enabled = false; // Khóa ngày bắt đầu
            }
            UpdateTongTienDV();
        }

        private void Cbo_LoaiDK_CanTin_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Đảm bảo các dữ liệu cần thiết có giá trị trước khi tính toán
            if (string.IsNullOrWhiteSpace(txt_DonGia_CănTin.Text) || !decimal.TryParse(txt_DonGia_CănTin.Text, out decimal donGia))
            {
                MessageBox.Show("Vui lòng nhập giá trị hợp lệ cho đơn giá!");
                return;
            }

            // Lấy giá trị được chọn trong ComboBox
            string selectedValue = Cbo_LoaiDK_CanTin.SelectedItem.ToString();

            // Lấy giá trị ngày bắt đầu từ dateTimeBD_CanTin
            DateTime startDate = dateTimeBD_CanTin.Value;

            int soSuatAn = 0;

            // Xử lý ràng buộc ngày và tính số suất ăn
            if (selectedValue == "Ngày")
            {
                // Ngày bắt đầu và ngày kết thúc giống nhau
                dateTimeKT_CanTin.Value = startDate;
                dateTimeKT_CanTin.Enabled = false; // Khóa ngày kết thúc
                soSuatAn = 3; // Mỗi ngày có 3 suất cơm
            }
            else if (selectedValue == "Tuần")
            {
                // Ngày kết thúc là 7 ngày sau ngày bắt đầu
                dateTimeKT_CanTin.Value = startDate.AddDays(7);
                dateTimeKT_CanTin.Enabled = false; // Khóa ngày kết thúc
                soSuatAn = 21; // Mỗi tuần có 21 suất cơm (7 ngày x 3 suất mỗi ngày)
            }
            else if (selectedValue == "Tháng")
            {
                // Ngày kết thúc là ngày cuối cùng của tháng hiện tại
                int daysInMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
                dateTimeKT_CanTin.Value = new DateTime(startDate.Year, startDate.Month, daysInMonth);
                dateTimeKT_CanTin.Enabled = false; // Khóa ngày kết thúc
                soSuatAn = daysInMonth * 3; // Mỗi ngày có 3 suất cơm
            }
            else
            {
                // Mở khóa ngày kết thúc nếu chọn giá trị khác
                dateTimeKT_CanTin.Enabled = true;
                MessageBox.Show("Loại đăng ký không hợp lệ!");
                return;
            }

            // Gán giá trị cho txt_SL_suatan
            txt_SL_suatan.Text = soSuatAn.ToString();

            // Tính thành tiền
            decimal thanhTien = donGia * soSuatAn;

            // Gán giá trị cho txt_thanhtien_căntin
            txt_thanhtien_cantin.Text = thanhTien.ToString("N2"); // Hiển thị với định dạng thập phân

            UpdateTongTienDV();
        }
        private void Txt_MSSV_KeyDown(object sender, KeyEventArgs e)
        {
            // Kiểm tra nếu người dùng nhấn Enter
            if (e.KeyCode == Keys.Enter)
            {
                // Lấy mã sinh viên từ ô nhập liệu
                string maSinhVien = txt_MSSV.Text.Trim();

                // Kiểm tra nếu mã sinh viên không rỗng
                if (!string.IsNullOrEmpty(maSinhVien))
                {
                    // Khởi tạo đối tượng BLL_SinhVien
                    BLL_SinhVien bll_sinhvien = new BLL_SinhVien();

                    // Gọi hàm GetSinhVienPhong để lấy thông tin sinh viên
                    SinhVienDTO sinhVienDTO = bll_sinhvien.GetSinhVienPhong(maSinhVien);

                    // Kiểm tra nếu sinh viên tồn tại
                    if (sinhVienDTO != null)
                    {
                        // Hiển thị thông tin sinh viên lên các ô tương ứng
                        txt_HoVaTen.Text = sinhVienDTO.HoTen;
                        // Điền mã phòng (nếu có)
                        txt_MaPhong.Text = sinhVienDTO.Phong?.MaPhong;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                // Đặt KeyCode của Enter thành None để tránh âm thanh beep mặc định
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void Btn_LapDangKy_DV_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ các textbox và control
                string mssv = txt_MSSV.Text;
                DateTime ngayDK = dateTime_NgayDK.Value;
                decimal tongTien = decimal.Parse(txt_TongTienDV.Text);
                string maNV = "NV001";
                string trangThai = "Hoàn Tất"; // Trang thái mặc định là "Hoàn Tất"

                // Xác định giá trị của LoaiDangKy dựa trên radio button đã chọn
                string loaiDangKy = "";

                if (radio_CanTin.Checked)
                {
                    loaiDangKy = "Căn Tin";
                }
                else if (radio_GiacUi.Checked)
                {
                    loaiDangKy = "Giặt Ủi";
                }
                else if (radio_Ca2.Checked)
                {
                    loaiDangKy = "Căn Tin và Giặt Ủi";
                }
                // Tạo đối tượng DangKyDichVu
                DangKyDichVu dangKyDV = new DangKyDichVu
                {

                    MaNhanVien = maNV,
                    MaSinhVien = mssv,
                    LoaiDangKy = loaiDangKy,  // Mặc định cho cả hai dịch vụ (Có thể điều chỉnh)
                    NgayDangKy = ngayDK,
                    TongTien = tongTien,
                    TrangThai = trangThai
                };

                // Danh sách chi tiết đăng ký dịch vụ
                List<CT_DangKyDV> chiTietDichVu = new List<CT_DangKyDV>();

                // Kiểm tra xem radio nào được chọn và lấy thông tin chi tiết dịch vụ
                if (radio_CanTin.Checked)
                {
                    // Lấy thông tin cho dịch vụ Căn Tin
                    CT_DangKyDV ctCanTin = new CT_DangKyDV
                    {
                        MaDichVu = txt_MaDV_CanTin.Text,

                        LoaiDangKy = Cbo_LoaiDK_CanTin.SelectedItem.ToString(),
                        SoLuong = int.Parse(txt_SL_suatan.Text),
                        DonGia = decimal.Parse(txt_thanhtien_cantin.Text),
                        DonViTinh = txt_DVT_CanTin.Text,
                        NgayBD = dateTimeBD_CanTin.Value,
                        NgayKT = dateTimeKT_CanTin.Value
                    };
                    chiTietDichVu.Add(ctCanTin);  // Thêm chi tiết dịch vụ vào danh sách
                }
                else if (radio_GiacUi.Checked)
                {
                    // Lấy thông tin cho dịch vụ Giặt Ủi
                    CT_DangKyDV ctGiacUi = new CT_DangKyDV
                    {
                        MaDichVu = txt_MaGiacUi.Text,

                        LoaiDangKy = Cbo_LoaiDK_GiacUi.SelectedItem.ToString(),
                        SoLuong = int.Parse(txt_soluongKg_Giac.Text),
                        DonGia = decimal.Parse(txt_thanhTien_GiacUi.Text),
                        DonViTinh = txt_DonViTinh_GiacUi.Text,
                        NgayBD = dateTime_BD_GiacUi.Value,
                        NgayKT = dateTime_KT_GiacUi.Value
                    };
                    chiTietDichVu.Add(ctGiacUi);  // Thêm chi tiết dịch vụ vào danh sách
                }
                else if (radio_Ca2.Checked)
                {
                    // Lấy thông tin cho cả 2 dịch vụ Căn Tin và Giặt Ủi
                    CT_DangKyDV ctCanTin = new CT_DangKyDV
                    {
                        MaDichVu = txt_MaDV_CanTin.Text,

                        LoaiDangKy = Cbo_LoaiDK_CanTin.SelectedItem.ToString(),
                        SoLuong = int.Parse(txt_SL_suatan.Text),
                        DonGia = decimal.Parse(txt_thanhtien_cantin.Text),
                        DonViTinh = txt_DVT_CanTin.Text,
                        NgayBD = dateTimeBD_CanTin.Value,
                        NgayKT = dateTimeKT_CanTin.Value
                    };
                    chiTietDichVu.Add(ctCanTin);  // Thêm chi tiết dịch vụ Căn Tin vào danh sách

                    CT_DangKyDV ctGiacUi = new CT_DangKyDV
                    {
                        MaDichVu = txt_MaGiacUi.Text,

                        LoaiDangKy = Cbo_LoaiDK_GiacUi.SelectedItem.ToString(),
                        SoLuong = int.Parse(txt_soluongKg_Giac.Text),
                        DonGia = decimal.Parse(txt_thanhTien_GiacUi.Text),
                        DonViTinh = txt_DonViTinh_GiacUi.Text,
                        NgayBD = dateTime_BD_GiacUi.Value,
                        NgayKT = dateTime_KT_GiacUi.Value
                    };
                    chiTietDichVu.Add(ctGiacUi);  // Thêm chi tiết dịch vụ Giặt Ủi vào danh sách
                }

                // Gọi phương thức LapPhieuDangKy từ BLL_DichVu để lưu phiếu đăng ký và chi tiết
                BLL_DichVu bllDichVu = new BLL_DichVu();
                bool isSuccess = bllDichVu.LapPhieuDangKy(dangKyDV, chiTietDichVu);

                // Thông báo kết quả
                if (isSuccess)
                {
                    MessageBox.Show("Lập phiếu đăng ký dịch vụ thành công!");
                }
                else
                {
                    MessageBox.Show("Lập phiếu đăng ký dịch vụ thất bại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }

        private void Btn_timkiem_TT_ngaythangnam_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị từ các trường nhập
                string ngayNhap = txt_Ngay_TT.Text.Trim();
                string thangNhap = cbo_Thang_TT.SelectedItem?.ToString();
                string namNhap = txt_namTT.Text.Trim();

                // Kiểm tra các trường hợp nhập thông tin
                if (!string.IsNullOrEmpty(ngayNhap) && !string.IsNullOrEmpty(thangNhap) && !string.IsNullOrEmpty(namNhap))
                {
                    // Tìm kiếm theo ngày, tháng và năm
                    DateTime ngayThangNam;
                    if (DateTime.TryParse($"{namNhap}-{thangNhap}-{ngayNhap}", out ngayThangNam))
                    {
                        var danhSach = bll_tt_diennuoc.GetThanhToanDienNuocByNgayThangNam(ngayThangNam);

                        // Cập nhật lại DataSource
                        dataGV_DS_TT_DN.DataSource = null;  // Xóa dữ liệu cũ
                        dataGV_DS_TT_DN.DataSource = danhSach;  // Gán lại dữ liệu mới

                        // Gọi lại hàm thiết lập lại giao diện nếu cần
                        // Ví dụ, cập nhật lại thứ tự các cột hoặc xử lý format dữ liệu
                        Console.WriteLine($"Đã thêm {danhSach.Count} dòng");  // Debug
                    }
                    else
                    {
                        MessageBox.Show("Định dạng ngày không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (!string.IsNullOrEmpty(thangNhap) && !string.IsNullOrEmpty(namNhap))
                {
                    // Tìm kiếm theo tháng và năm
                    int thang, nam;
                    if (int.TryParse(thangNhap, out thang) && int.TryParse(namNhap, out nam))
                    {
                        var danhSach = bll_tt_diennuoc.GetThanhToanDienNuocByThangVaNam(thang, nam);

                        // Cập nhật lại DataSource
                        dataGV_DS_TT_DN.DataSource = null;  // Xóa dữ liệu cũ
                        dataGV_DS_TT_DN.DataSource = danhSach;  // Gán lại dữ liệu mới

                        Console.WriteLine($"Đã thêm {danhSach.Count} dòng");  // Debug
                    }
                    else
                    {
                        MessageBox.Show("Tháng hoặc năm không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (!string.IsNullOrEmpty(namNhap))
                {
                    // Tìm kiếm theo năm
                    int nam;
                    if (int.TryParse(namNhap, out nam))
                    {
                        var danhSach = bll_tt_diennuoc.GetThanhToanDienNuocByNam(nam);

                        // Cập nhật lại DataSource
                        dataGV_DS_TT_DN.DataSource = null;  // Xóa dữ liệu cũ
                        dataGV_DS_TT_DN.DataSource = danhSach;  // Gán lại dữ liệu mới

                        Console.WriteLine($"Đã thêm {danhSach.Count} dòng");  // Debug
                    }
                    else
                    {
                        MessageBox.Show("Năm không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (!string.IsNullOrEmpty(ngayNhap))
                {
                    // Chỉ nhập ngày thì báo lỗi
                    MessageBox.Show("Không đủ dữ kiện để tìm kiếm! Vui lòng nhập thêm tháng và năm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Không nhập thông tin nào
                    MessageBox.Show("Vui lòng nhập thông tin để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void LoadMaThanhToanToComboBox()
        {
            try
            {
                // Lấy danh sách mã thanh toán từ BLL (hoặc DAL)
                List<ThanhToanDienNuoc> danhSachThanhToan = bll_tt_diennuoc.LayDanhSachThanhToan(); // Hàm này trả về tất cả các thanh toán

                cbo_Loc_MaThanhToan.Items.Clear(); // Xóa tất cả các mục trong ComboBox
                cbo_Loc_MaThanhToan.Items.Add(""); // Thêm mục trống vào đầu ComboBox

                // Thêm tất cả mã thanh toán vào ComboBox
                foreach (var thanhToan in danhSachThanhToan)
                {
                    cbo_Loc_MaThanhToan.Items.Add(thanhToan.MaThanhToanDN);
                }

                // Đặt lựa chọn mặc định (nếu cần)
                if (cbo_Loc_MaThanhToan.Items.Count > 0)
                {
                    cbo_Loc_MaThanhToan.SelectedIndex = 0; // Đặt mục mặc định là mục đầu tiên
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách mã thanh toán: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cbo_Loc_MaThanhToan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu giá trị ComboBox không rỗng
                string maThanhToan = cbo_Loc_MaThanhToan.SelectedItem.ToString();

                if (string.IsNullOrEmpty(maThanhToan))
                {
                    // Nếu mã thanh toán trống, gọi lại hàm LoadDanhSachThanhToan để tải tất cả danh sách
                    LoadDanhSachThanhToan();
                }
                else
                {
                    // Nếu có mã thanh toán, lọc danh sách theo mã thanh toán
                    List<ThanhToanDienNuoc> danhSachThanhToan = bll_tt_diennuoc.LayDanhSachTheoMaThanhToan(maThanhToan);

                    // Đổ danh sách đã lọc vào DataGridView hoặc xử lý theo yêu cầu
                    dataGV_DS_TT_DN.DataSource = danhSachThanhToan;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc danh sách thanh toán: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Cbo_Loc_NhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị được chọn trong ComboBox
                string selectedValue = cbo_Loc_NhanVien.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(selectedValue))
                {
                    // Nếu giá trị rỗng, load lại toàn bộ danh sách thanh toán
                    LoadDanhSachThanhToan();
                }
                else
                {
                    // Nếu có giá trị, gọi hàm lấy danh sách thanh toán theo mã nhân viên
                    List<ThanhToanDienNuoc> danhSach = bll_tt_diennuoc.LayDanhSachThanhToanTheoMaNhanVien(selectedValue);

                    // Đổ dữ liệu vào DataGridView
                    dataGV_DS_TT_DN.DataSource = danhSach;

                    // Đặt tên tiêu đề cột
                    dataGV_DS_TT_DN.Columns["MaThanhToanDN"].HeaderText = "Mã Thanh Toán";
                    dataGV_DS_TT_DN.Columns["MaNhanVien"].HeaderText = "Mã Nhân Viên";
                    dataGV_DS_TT_DN.Columns["MaSinhVien"].HeaderText = "Mã Sinh Viên";
                    dataGV_DS_TT_DN.Columns["NgayLap"].HeaderText = "Ngày Lập";
                    dataGV_DS_TT_DN.Columns["TongTien"].HeaderText = "Tổng Tiền";
                    dataGV_DS_TT_DN.Columns["TrangThai"].HeaderText = "Trạng Thái";

                    // Tùy chỉnh độ rộng cột
                    dataGV_DS_TT_DN.Columns["MaThanhToanDN"].Width = 120;
                    dataGV_DS_TT_DN.Columns["NgayLap"].Width = 100;
                    dataGV_DS_TT_DN.Columns["TongTien"].Width = 100;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xử lý thay đổi lựa chọn: " + ex.Message, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadMaNhanVienToComboBox()
        {
            try
            {
                var danhSachNV = bll_nv.GetAllMaNhanVien();

                cbo_Loc_NhanVien.Items.Clear();
                cbo_Loc_NhanVien.Items.Add("");

                foreach (var nv in danhSachNV)
                {
                    cbo_Loc_NhanVien.Items.Add(nv.MaNhanVien);
                }

                if (cbo_Loc_NhanVien.Items.Count > 0)
                {
                    cbo_Loc_NhanVien.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách mã nhân viên: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Txt_MSSV_TT_TextChanged(object sender, EventArgs e)
        {
            string maSinhVien = txt_MSSV_TT.Text; // Lấy mã sinh viên từ TextBox

            if (!string.IsNullOrEmpty(maSinhVien)) // Kiểm tra nếu mã sinh viên không trống
            {
                // Gọi hàm BLL để lấy tên sinh viên từ mã sinh viên
                BLL_ThanhToanDienNuoc bllThanhToan = new BLL_ThanhToanDienNuoc();
                string tenSinhVien = bllThanhToan.LayTenSinhVien(maSinhVien);

                if (tenSinhVien != null)
                {
                    txt_TenSinhVien.Text = tenSinhVien; // Hiển thị tên sinh viên vào TextBox
                }
                else
                {
                    txt_TenSinhVien.Text = "Không tìm thấy sinh viên"; // Thông báo nếu không tìm thấy sinh viên
                }
            }
            else
            {
                txt_TenSinhVien.Text = ""; // Nếu mã sinh viên trống, xóa tên sinh viên
            }
        }


        private void Txt_MaNV_TT_TextChanged(object sender, EventArgs e)
        {
            string maNhanVien = txt_MaNV_TT.Text; // Lấy mã nhân viên từ TextBox

            if (!string.IsNullOrEmpty(maNhanVien)) // Kiểm tra nếu mã nhân viên không trống
            {
                // Gọi hàm BLL để lấy tên nhân viên từ mã nhân viên
                BLL_ThanhToanDienNuoc bllThanhToan = new BLL_ThanhToanDienNuoc();
                string tenNhanVien = bllThanhToan.LayTenNhanVien(maNhanVien);

                if (tenNhanVien != null)
                {
                    txt_tenNhanVien.Text = tenNhanVien; // Hiển thị tên nhân viên vào TextBox
                }
                else
                {
                    txt_tenNhanVien.Text = "Không tìm thấy nhân viên"; // Thông báo nếu không tìm thấy nhân viên
                }
            }
            else
            {
                txt_tenNhanVien.Text = ""; // Nếu mã nhân viên trống, xóa tên nhân viên
            }
        }


        private void Txt_maphongtt_TextChanged(object sender, EventArgs e)
        {
            // Lấy mã phòng từ TextBox
            string maPhong = txt_maphongtt.Text.Trim();

            // Kiểm tra xem mã phòng có hợp lệ hay không (nếu rỗng hoặc null thì không thực hiện)
            if (string.IsNullOrEmpty(maPhong))
            {
                return; // Nếu mã phòng rỗng, không làm gì
            }

            // Gọi phương thức BLL để lấy thông tin chi tiết phòng (chỉ số điện và nước)
            PhongChiTiet phongChiTiet = bll_tt_diennuoc.LayChiSoDienNuocTuMaPhong(maPhong);

            if (phongChiTiet != null)
            {
                // Hiển thị thông tin chỉ số điện và nước lên các TextBox
                txt_chiso_Dien_hientai.Text = phongChiTiet.ChiSoDien.ToString();
                txt_chiso_Nuoc_hientai.Text = phongChiTiet.ChiSoNuoc.ToString();
            }
            else
            {
                // Nếu không tìm thấy thông tin, có thể hiển thị thông báo hoặc để trống
                txt_chiso_Dien_hientai.Clear();
                txt_chiso_Nuoc_hientai.Clear();
                MessageBox.Show("Không tìm thấy thông tin phòng.");
            }
        }


        private void Txt_MaTT_DN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Lấy mã thanh toán từ TextBox txt_MaTT_DN
                string maThanhToanDN = txt_MaTT_DN.Text.Trim();

                // Kiểm tra nếu mã thanh toán không rỗng
                if (!string.IsNullOrEmpty(maThanhToanDN))
                {
                    // Gọi hàm từ BLL để lấy chi tiết thanh toán điện nước
                    BLL_ThanhToanDienNuoc bll = new BLL_ThanhToanDienNuoc();
                    List<CT_ThanhToanDienNuoc> chiTietList = bll.LayChiTietCTThanhToanDienNuoc(maThanhToanDN);

                    if (chiTietList != null && chiTietList.Count > 0)
                    {
                        // Lấy chi tiết của bản ghi đầu tiên (vì mỗi mã thanh toán chỉ có một chi tiết tương ứng)
                        var chiTiet = chiTietList[0];

                        // Hiển thị thông tin vào các TextBox và DateTimePicker
                        txt_chisoDienCu.Text = chiTiet.ChiSoDienCu.ToString();
                        txt_chisoDien_Moi.Text = chiTiet.ChiSoDienMoi.ToString();
                        txt_sodien_tieuthu.Text = chiTiet.SoDienTieuThu.ToString();
                        txt_sonuoc_cu.Text = chiTiet.ChiSoNuocCu.ToString();
                        txt_sonuoc_moi.Text = chiTiet.ChiSoNuocMoi.ToString();
                        txt_sonuoc_tieuthu.Text = chiTiet.SoNuocTieuThu.ToString();
                        txt_dongia_dien.Text = chiTiet.DonGiaDien.ToString();
                        txt_dongia_nuoc.Text = chiTiet.DonGiaNuoc.ToString();
                        // Kiểm tra và gán giá trị cho DateTimePicker nếu có giá trị hợp lệ
                        if (chiTiet.NgayBatDau != null)
                            dateTime_NgayBD.Value = chiTiet.NgayBatDau.Value;
                        else
                            dateTime_NgayBD.Value = DateTime.Now; // Hoặc một giá trị mặc định

                        if (chiTiet.NgayKetThuc != null)
                            dateTime_NgayKT.Value = chiTiet.NgayKetThuc.Value;
                        else
                            dateTime_NgayKT.Value = DateTime.Now; // Hoặc một giá trị mặc định
                        txt_maphongtt.Text = chiTiet.MaPhong;
                    }
                    else
                    {
                        // Nếu không có dữ liệu, thông báo cho người dùng
                        MessageBox.Show("Không tìm thấy chi tiết thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // Nếu mã thanh toán rỗng, xóa hết thông tin trong các TextBox và DateTimePicker
                    txt_chisoDienCu.Clear();
                    txt_chisoDien_Moi.Clear();
                    txt_sodien_tieuthu.Clear();
                    txt_sonuoc_cu.Clear();
                    txt_sonuoc_moi.Clear();
                    txt_sonuoc_tieuthu.Clear();
                    txt_dongia_dien.Clear();
                    txt_dongia_nuoc.Clear();
                    dateTime_NgayBD.Value = DateTime.Now;
                    dateTime_NgayKT.Value = DateTime.Now;
                    txt_maphongtt.Clear();
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DataGV_DS_TT_DN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Kiểm tra chỉ số dòng hợp lệ
                if (e.RowIndex >= 0)
                {
                    // Lấy mã thanh toán từ cột đầu tiên của dòng được chọn
                    // Lấy mã thanh toán từ cột đầu tiên của dòng được chọn (chỉ số cột là 0)
                    string maThanhToanDN = dataGV_DS_TT_DN.Rows[e.RowIndex].Cells[0].Value.ToString();

                    //  MessageBox.Show("Không tìm thấy thông tin chi tiết!", "Thông báo" + maThanhToanDN);
                    // Gọi BLL để lấy thông tin chi tiết

                    ThanhToanDienNuoc chiTiet = bll_tt_diennuoc.LayChiTietThanhToanDienNuoc(maThanhToanDN);

                    if (chiTiet != null)
                    {
                        // Hiển thị thông tin lên các TextBox và DateTimePicker
                        txt_MaTT_DN.Text = maThanhToanDN;
                        txt_MaNV_TT.Text = chiTiet.MaNhanVien;
                        txt_MSSV_TT.Text = chiTiet.MaSinhVien;
                        txt_TongTien_TT.Text = chiTiet.TongTien.ToString(); // Hiển thị tiền theo định dạng tiền tệ
                        txt_TrangThaiTT.Text = chiTiet.TrangThai;
                        dateTime_NgayTT.Value = chiTiet.NgayLap ?? DateTime.Now; // Nếu Ngày Lập null thì gán ngày hiện tại
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ThanhToanPhong_Load(object sender, EventArgs e)
        {
            LoadDanhSachThanhToan();
            LoadMaNhanVienToComboBox();
            LoadMaThanhToanToComboBox();
            LoadDataDangKyDV();
        }

        private void LoadDanhSachThanhToan()
        {
            try
            {
                // Lấy danh sách thanh toán
                List<ThanhToanDienNuoc> danhSach = bll_tt_diennuoc.LayDanhSachThanhToan();

                // Đổ dữ liệu vào DataGridView
                dataGV_DS_TT_DN.DataSource = danhSach;

                // Đặt tên tiêu đề cột
                dataGV_DS_TT_DN.Columns["MaThanhToanDN"].HeaderText = "Mã Thanh Toán";
                dataGV_DS_TT_DN.Columns["MaNhanVien"].HeaderText = "Mã Nhân Viên";
                dataGV_DS_TT_DN.Columns["MaSinhVien"].HeaderText = "Mã Sinh Viên";
                dataGV_DS_TT_DN.Columns["NgayLap"].HeaderText = "Ngày Lập";
                dataGV_DS_TT_DN.Columns["TongTien"].HeaderText = "Tổng Tiền";
                dataGV_DS_TT_DN.Columns["TrangThai"].HeaderText = "Trạng Thái";

                // Tùy chỉnh độ rộng cột
                dataGV_DS_TT_DN.Columns["MaThanhToanDN"].Width = 120;
                dataGV_DS_TT_DN.Columns["NgayLap"].Width = 100;
                dataGV_DS_TT_DN.Columns["TongTien"].Width = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Xuat_TKDN_theoThang_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ các control trên giao diện
                int thang = int.Parse(cbo_Thang_TK_DN.SelectedItem.ToString());
                int nam = int.Parse(txt_Nam_TK_DN.Text);
                decimal tongTien = decimal.Parse(text_TongTien_TheoThang.Text);

                // Tạo danh sách thông tin từ DataGridView
                List<ThongKeDienNuoc> danhSachThongKe = new List<ThongKeDienNuoc>();
                foreach (DataGridViewRow row in datagv_TK_TTDN_TheoThang.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        // Đảm bảo lấy dữ liệu từ đúng cột trong DataGridView
                        ThongKeDienNuoc thongKe = new ThongKeDienNuoc
                        {
                            MaPhong = row.Cells["MaPhong"].Value?.ToString() ?? "",
                            ChiSoDienCu = row.Cells["ChiSoDienCu"].Value != null ? Convert.ToInt32(row.Cells["ChiSoDienCu"].Value) : 0,
                            ChiSoDienMoi = row.Cells["ChiSoDienMoi"].Value != null ? Convert.ToInt32(row.Cells["ChiSoDienMoi"].Value) : 0,
                            SoDienTieuThu = row.Cells["SoDienTieuThu"].Value != null ? Convert.ToInt32(row.Cells["SoDienTieuThu"].Value) : 0,
                            DonGiaDien = row.Cells["DonGiaDien"].Value != null ? Convert.ToDecimal(row.Cells["DonGiaDien"].Value) : 0,
                            ThanhTienDien = row.Cells["ThanhTienDien"].Value != null ? Convert.ToDecimal(row.Cells["ThanhTienDien"].Value) : 0,
                            ChiSoNuocCu = row.Cells["ChiSoNuocCu"].Value != null ? Convert.ToInt32(row.Cells["ChiSoNuocCu"].Value) : 0,
                            ChiSoNuocMoi = row.Cells["ChiSoNuocMoi"].Value != null ? Convert.ToInt32(row.Cells["ChiSoNuocMoi"].Value) : 0,
                            SoNuocTieuThu = row.Cells["SoNuocTieuThu"].Value != null ? Convert.ToInt32(row.Cells["SoNuocTieuThu"].Value) : 0,
                            DonGiaNuoc = row.Cells["DonGiaNuoc"].Value != null ? Convert.ToDecimal(row.Cells["DonGiaNuoc"].Value) : 0,
                            ThanhTienNuoc = row.Cells["ThanhTienNuoc"].Value != null ? Convert.ToDecimal(row.Cells["ThanhTienNuoc"].Value) : 0,
                            TongTien = row.Cells["TongTien"].Value != null ? Convert.ToDecimal(row.Cells["TongTien"].Value) : 0
                        };
                        danhSachThongKe.Add(thongKe);
                    }
                }

                // Lấy thông tin nhân viên (nếu cần)
                int MaTK = DangNhap.MaTK;
                BLL_NhanVien nv = new BLL_NhanVien();
                NhanVien nhanVien = nv.GetNhanVienDetailsByMaTaiKhoan(MaTK);

                // Lấy tên nhân viên
                string nguoiLapPhieu = "Lê Nhật Quyên";

                // Tạo đường dẫn lưu file PDF
                string filePath = @"D:\DACN\HoaDonDienNuoc\ThongKeDienNuoc" + thang + "_" + nam + ".pdf";
                PdfReport pdfReport = new PdfReport(); // Tạo đối tượng PdfReport
                // Gọi phương thức tạo báo cáo PDF
                pdfReport.CreateSummaryReport(filePath, thang, nam, nguoiLapPhieu, danhSachThongKe, tongTien);

                // Hiển thị thông báo khi báo cáo được tạo thành công
                MessageBox.Show("Báo cáo đã được xuất thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Datagv_TK_TTDN_TheoThang_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu có hàng nào được chọn
            if (datagv_TK_TTDN_TheoThang.CurrentRow != null)
            {
                // Lấy hàng hiện tại
                DataGridViewRow row = datagv_TK_TTDN_TheoThang.CurrentRow;

                // Gán giá trị từ các cột của hàng vào TextBox
                text_MaPhongTK.Text = row.Cells["MaPhong"].Value?.ToString() ?? "";
                text_SoDienCu.Text = row.Cells["ChiSoDienCu"].Value?.ToString() ?? "";
                text_SoDien_Moi.Text = row.Cells["ChiSoDienMoi"].Value?.ToString() ?? "";
                text_SoDienTieuThu.Text = row.Cells["SoDienTieuThu"].Value?.ToString() ?? "";
                text_DonGiaDien.Text = row.Cells["DonGiaDien"].Value?.ToString() ?? "";
                text_ThanhTienDien.Text = row.Cells["ThanhTienDien"].Value?.ToString() ?? "";
                text_ChiSoNuocCu.Text = row.Cells["ChiSoNuocCu"].Value?.ToString() ?? "";
                text_ChiSoNuocMoi.Text = row.Cells["ChiSoNuocMoi"].Value?.ToString() ?? "";
                text_SoNuocTieuThu.Text = row.Cells["SoNuocTieuThu"].Value?.ToString() ?? "";
                text_DonGiaNuoc.Text = row.Cells["DonGiaNuoc"].Value?.ToString() ?? "";
                text_ThanhTienNuoc.Text = row.Cells["ThanhTienNuoc"].Value?.ToString() ?? "";
                txt_TongTien_TK.Text = row.Cells["TongTien"].Value?.ToString() ?? "";
            }
        }

        private void Btn_TK_TTDN_TheoThang_Click(object sender, EventArgs e)
        {
            // Lấy giá trị tháng và năm từ các control
            int thang = int.Parse(cbo_Thang_TK_DN.SelectedItem.ToString());
            int nam = int.Parse(txt_Nam_TK_DN.Text);

            // Gọi BLL để lấy dữ liệu

            List<ThongKeDienNuoc> danhSachThongKe = bll_tt_diennuoc.ThongKeDienNuoc(thang, nam);

            // Gán dữ liệu vào DataGridView
            datagv_TK_TTDN_TheoThang.DataSource = danhSachThongKe;

            // Đặt tên lại các cột
            datagv_TK_TTDN_TheoThang.Columns["MaPhong"].HeaderText = "Mã Phòng";
            datagv_TK_TTDN_TheoThang.Columns["ChiSoDienCu"].HeaderText = "Chỉ Số Điện Cũ";
            datagv_TK_TTDN_TheoThang.Columns["ChiSoDienMoi"].HeaderText = "Chỉ Số Điện Mới";
            datagv_TK_TTDN_TheoThang.Columns["SoDienTieuThu"].HeaderText = "Số Điện Tiêu Thụ";
            datagv_TK_TTDN_TheoThang.Columns["DonGiaDien"].HeaderText = "Đơn Giá Điện";
            datagv_TK_TTDN_TheoThang.Columns["ThanhTienDien"].HeaderText = "Thành Tiền Điện";
            datagv_TK_TTDN_TheoThang.Columns["ChiSoNuocCu"].HeaderText = "Chỉ Số Nước Cũ";
            datagv_TK_TTDN_TheoThang.Columns["ChiSoNuocMoi"].HeaderText = "Chỉ Số Nước Mới";
            datagv_TK_TTDN_TheoThang.Columns["SoNuocTieuThu"].HeaderText = "Số Nước Tiêu Thụ";
            datagv_TK_TTDN_TheoThang.Columns["DonGiaNuoc"].HeaderText = "Đơn Giá Nước";
            datagv_TK_TTDN_TheoThang.Columns["ThanhTienNuoc"].HeaderText = "Thành Tiền Nước";
            datagv_TK_TTDN_TheoThang.Columns["TongTien"].HeaderText = "Tổng Tiền";

            // Tính tổng giá trị cột "TongTien"
            decimal tongTien = 0;

            foreach (DataGridViewRow row in datagv_TK_TTDN_TheoThang.Rows)
            {
                // Kiểm tra giá trị null hoặc lỗi chuyển đổi
                if (row.Cells["TongTien"].Value != null &&
                    decimal.TryParse(row.Cells["TongTien"].Value.ToString(), out decimal giaTri))
                {
                    tongTien += giaTri;
                }
            }

            // Hiển thị tổng tiền lên TextBox
            text_TongTien_TheoThang.Text = tongTien.ToString("N0"); // Định dạng số nguyên có dấu phân cách
        }

        private void Btn_ThanhToanDienNuoc_Click(object sender, EventArgs e)
        {

            int MaTK = DangNhap.MaTK;
            BLL_NhanVien nv = new BLL_NhanVien();
            // Lấy thông tin nhân viên bằng mã tài khoản
            NhanVien nhanVien = nv.GetNhanVienDetailsByMaTaiKhoan(MaTK);
            try
            {
                // Lấy thông tin từ các điều khiển giao diện
                string maPhong = txt_MaPhong.Text;
                string Manv = "NV002";
                string maSV_TP = txt_MaSV_TP.Text;
                DateTime ngayBatDau = date_NgayBatDau.Value;
                DateTime ngayKetThuc = date_NgayKetThuc.Value;
                string maDienNuoc = txt_MaDienNuoc.Text;
                decimal donGiaDien = decimal.Parse(txt_DonGiaDien.Text);
                decimal donGiaNuoc = decimal.Parse(txt_DonGiaNuoc.Text);
                int soDienCu = int.Parse(txt_SoDienCu.Text);
                int soNuocCu = int.Parse(txt_SoNuocCu.Text);
                int chiSoDienMoi = int.Parse(txt_ChiSoDienMoi.Text);
                int chiSoNuocMoi = int.Parse(txt_DNuocMoi.Text);
                string emailTP = txt_Email_TP.Text; // Địa chỉ email của người nhận


                // Tính tiêu thụ điện và nước
                int soDienTieuThu = chiSoDienMoi - soDienCu;
                int soNuocTieuThu = chiSoNuocMoi - soNuocCu;


                // Ép kiểu từ int sang decimal trước khi tính tổng tiền
                decimal TongTien = (soDienTieuThu * donGiaDien) + (soNuocTieuThu * donGiaNuoc);

                // Cập nhật chỉ số điện nước
                bool capNhatChiSo = bll_tt_diennuoc.CapNhatChiSoDienNuoc(maPhong, chiSoDienMoi, chiSoNuocMoi);

                if (capNhatChiSo)
                {
                    // Tạo hóa đơn điện nước
                    ThanhToanDienNuoc hoaDon = new ThanhToanDienNuoc
                    {
                        MaSinhVien = maSV_TP,
                        MaNhanVien = Manv,
                        NgayLap = ngayBatDau,
                        TongTien = (soDienTieuThu * donGiaDien) + (soNuocTieuThu * donGiaNuoc), // Tổng tiền thanh toán
                        TrangThai = "Hoàn Tất"
                    };

                    // Tạo chi tiết hóa đơn điện nước
                    List<CT_ThanhToanDienNuoc> chiTietHoaDon = new List<CT_ThanhToanDienNuoc>
            {
                new CT_ThanhToanDienNuoc
                {
                    MaPhong = maPhong,
                    MaThanhToanDN = hoaDon.MaThanhToanDN,
                    ChiSoDienCu = soDienCu,
                    ChiSoDienMoi = chiSoDienMoi,
                    ChiSoNuocCu = soNuocCu,
                    ChiSoNuocMoi = chiSoNuocMoi,
                    SoDienTieuThu = soDienTieuThu,
                    SoNuocTieuThu = soNuocTieuThu,
                    DonGiaDien = donGiaDien,
                    DonGiaNuoc = donGiaNuoc,
                    NgayBatDau = ngayBatDau,
                    NgayKetThuc = ngayKetThuc,
                }
            };

                    // Thêm hóa đơn điện nước vào cơ sở dữ liệu
                    bool thanhToanThanhCong = bll_tt_diennuoc.ThemHoaDonDienNuoc(hoaDon, chiTietHoaDon);

                    if (thanhToanThanhCong)
                    {
                        // Tạo file PDF hóa đơn
                        string monthYear = DateTime.Now.ToString("MM-yyyy"); // Lấy tháng và năm hiện tại
                        string filePath = Path.Combine(@"D:\DACN\HoaDonDienNuoc", $"HoaDon_{monthYear}_{maPhong}.pdf");

                        PdfReport pdfReport = new PdfReport();
                        pdfReport.CreateInvoicePdf(filePath, maPhong, TongTien, soDienCu, soNuocCu, chiSoDienMoi, chiSoNuocMoi, soDienTieuThu, soNuocTieuThu);

                        // Gửi email với hóa đơn đính kèm
                        EmailService emailService = new EmailService();
                        string subject = "Hóa Đơn Thanh Toán Điện Nước";
                        string body = $"Xin chào, \n\nĐây là hóa đơn thanh toán điện nước của bạn từ {ngayBatDau.ToString("dd/MM/yyyy")} đến {ngayKetThuc.ToString("dd/MM/yyyy")}. Vui lòng kiểm tra file đính kèm.";

                        emailService.SendInvoiceEmail(emailTP, subject, body, filePath);

                        MessageBox.Show("Thanh toán điện nước thành công và hóa đơn đã được gửi qua email!");
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi khi thêm hóa đơn thanh toán!");
                    }
                }
                else
                {
                    MessageBox.Show("Cập nhật chỉ số điện nước thất bại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thanh toán điện nước: " + ex.Message);
            }
        }



        private void Txt_DNuocMoi_KeyDown(object sender, KeyEventArgs e)
        {
            // Kiểm tra xem các ô txt_DNuocMoi và txt_SoNuocCu có giá trị hợp lệ không
            if (int.TryParse(txt_DNuocMoi.Text, out int chiSoNuocMoi) &&
                int.TryParse(txt_SoNuocCu.Text, out int chiSoNuocCu))
            {
                // Tính toán số nước tiêu thụ
                int soNuocTieuThu = chiSoNuocMoi - chiSoNuocCu;

                // Hiển thị kết quả vào txt_SNuocTieuThu
                txt_SNuocTieuThu.Text = soNuocTieuThu.ToString();
            }
            else
            {
                // Xóa giá trị trong txt_SNuocTieuThu nếu không hợp lệ
                txt_SNuocTieuThu.Text = string.Empty;
            }
        }

        private void Txt_ChiSoDienMoi_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                if (int.TryParse(txt_ChiSoDienMoi.Text, out int chiSoDienMoi) &&
                    int.TryParse(txt_SoDienCu.Text, out int chiSoDienCu))
                {
                    int soDienTieuThu = chiSoDienMoi - chiSoDienCu;
                    txt_SDien_TieuThu.Text = soDienTieuThu.ToString();
                }
                else
                {
                    txt_SDien_TieuThu.Text = string.Empty;
                }
            }
        }



        private void Txt_ChiSoDienMoi_Leave(object sender, EventArgs e)
        {
            int chiSoDienMoi;
            int chiSoDienCu;

            // Kiểm tra nếu giá trị của txt_ChiSoDienMoi và txt_SoDienCu là hợp lệ
            if (int.TryParse(txt_ChiSoDienMoi.Text, out chiSoDienMoi) &&
                int.TryParse(txt_SoDienCu.Text, out chiSoDienCu))
            {
                // Kiểm tra nếu chỉ số điện mới lớn hơn chỉ số cũ
                if (chiSoDienMoi > chiSoDienCu)
                {
                    // Tính số điện tiêu thụ và hiển thị lên txt_SDien_TieuThu
                    txt_SDien_TieuThu.Text = (chiSoDienMoi - chiSoDienCu).ToString();
                }
                else
                {
                    // Nếu chỉ số mới nhỏ hơn hoặc bằng chỉ số cũ, báo lỗi và xóa giá trị đã nhập
                    MessageBox.Show("Chỉ số điện mới phải lớn hơn chỉ số điện cũ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_ChiSoDienMoi.Clear();
                    txt_SDien_TieuThu.Clear();
                }
            }
            else
            {
                // Nếu giá trị nhập vào không hợp lệ, xóa giá trị ở txt_SDien_TieuThu
                txt_SDien_TieuThu.Clear();
            }

        }






        private void Txt_MaPhong_KeyDown(object sender, KeyEventArgs e)
        {
            // Kiểm tra nếu phím Enter được nhấn
            if (e.KeyCode == Keys.Enter)
            {
                // Gọi hàm lấy thông tin phòng từ BLL
                string maPhong = txt_MaPhong.Text;
                var thongTinPhong = bll_tt_diennuoc.LayThongTinPhong(maPhong);

                if (thongTinPhong != null)
                {
                    // Giả sử thongTinPhong là một đối tượng chứa các thông tin cần thiết, bạn có thể xử lý như sau:
                    txt_soluongSV.Text = thongTinPhong.SoLuongSV.ToString();
                    txt_MaSV_TP.Text = thongTinPhong.MaSV_TP;
                    txt_Hoten_TP.Text = thongTinPhong.HoTen_TP;
                    txt_Email_TP.Text = thongTinPhong.Email_TP;
                    txt_DonGiaDien.Text = thongTinPhong.DonGiaDien.ToString();
                    txt_DonGiaNuoc.Text = thongTinPhong.DonGiaNuoc.ToString();
                    txt_SoDienCu.Text = thongTinPhong.ChiSoDienCu.ToString();
                    txt_SoNuocCu.Text = thongTinPhong.ChiSoNuocCu.ToString();
                    txt_tenPhong.Text = thongTinPhong.TenLoaiPhong.ToString();
                    txt_MaDienNuoc.Text = thongTinPhong.MaDienNuoc;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin phòng với mã phòng này.");
                }
            }
        }
    }
}
