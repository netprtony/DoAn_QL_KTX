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

namespace GUI
{
    public partial class ThanhToanPhong : Form
    {

        private BLL_ThanhToanDienNuoc bll_tt_diennuoc;
        private BLL_NhanVien bll_nv;
        public ThanhToanPhong()
        {
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
