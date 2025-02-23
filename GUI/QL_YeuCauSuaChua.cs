using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Excel= Microsoft.Office.Interop.Excel;
using BLL;
using DTO;
using OfficeOpenXml;
using System.IO;
using GUI.Model;
using Control;


namespace GUI
{
    public partial class QL_YeuCauSuaChua : Form
    {
        private BLL_TrangThietBi bll_ttb;
        private BLL_YeuCauSuaChua bll_ycsc;
        private BLL_NhanVien bll_nv;
        private BLL_ThanhToanDienNuoc bll_tt_diennuoc;
        public QL_YeuCauSuaChua()
        {
            InitializeComponent();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            bll_ttb = new BLL_TrangThietBi();
            bll_ycsc = new BLL_YeuCauSuaChua();
            bll_nv = new BLL_NhanVien();
            bll_tt_diennuoc = new BLL_ThanhToanDienNuoc();
            this.Load += YeuCauSuaChua_Load;
            btn_XuatPhieuKT_TTB.Click += Btn_XuatPhieuKT_TTB_Click;
            btn_themdexuat.Click += Btn_themdexuat_Click;
            btn_ThemYCSC.Click += Btn_ThemYCSC_Click;
            btn_Xuat_YCSC.Click += Btn_Xuat_YCSC_Click;
            Data_GV_DeXuatSuaChua.CellClick += Data_GV_DeXuatSuaChua_CellClick;

          
            txt_MaSinhVien.TextChanged += Txt_MaSinhVien_TextChanged;

            text_maphong.KeyDown += Text_maphong_KeyDown;
            cbo_Ten_TTB.SelectedIndexChanged += Cbo_Ten_TTB_SelectedIndexChanged;
            btn_LapPhieuYCSC.Click += Btn_LapPhieuYCSC_Click;
            btn_TTB_YCSC.Click += Btn_TTB_YCSC_Click;
            btn_Xoa_TTB.Click += Btn_Xoa_TTB_Click;
            btn_Sua_TTB.Click += Btn_Sua_TTB_Click;
            dataGV_DS_YCSC.CellClick += DataGV_DS_YCSC_CellClick;
            btn_Xuat_PYCSC_SV.Click += Btn_Xuat_PYCSC_SV_Click;
            txtQL_MaNhanVien.TextChanged += TxtQL_MaNhanVien_TextChanged;
            dataGVQL_DS_YCSC.CellClick += DataGVQL_DS_YCSC_CellClick;
            cbo_QL_MaYCSC.SelectedIndexChanged += Cbo_QL_MaYCSC_SelectedIndexChanged;
            cbo_QL_MaNV.SelectedIndexChanged += Cbo_QL_MaNV_SelectedIndexChanged;
            cbo_SapXep.SelectedIndexChanged += Cbo_SapXep_SelectedIndexChanged;
            cbo_QL_TrangThai_YCSC.SelectedIndexChanged += Cbo_QL_TrangThai_YCSC_SelectedIndexChanged;
            btn_timKiem_ngay_thang_nam.Click += Btn_timKiem_ngay_thang_nam_Click;
            btn_xoa_YCCS.Click += Btn_xoa_YCCS_Click;
            btn_Sua_YCSC.Click += Btn_Sua_YCSC_Click;
            btn_XuatTK_TTB.Click += Btn_XuatTK_TTB_Click;

            dataGV_QL_TTB.CellClick += DataGV_QL_TTB_CellClick;

            //thống kê
            btn_TKSL_TTB.Click += Btn_TKSL_TTB_Click;
            btn_TK_TSSC.Click += Btn_TK_TSSC_Click;

            // Khởi tạo Panel cho panel2 nếu chưa có
            if (panel2 == null)
            {
                panel2 = new Panel();
                panel2.Dock = DockStyle.Fill;
                panel2.AutoScroll = true;
                tabPage1.Controls.Add(panel2);
            }

            // Di chuyển các điều khiển vào Panel panel2
            List<System.Windows.Forms.Control> controlsToMoveToPanel2 = new List<System.Windows.Forms.Control>();
            foreach (System.Windows.Forms.Control control in tabPage1.Controls)
            {
                if (control != panel2)
                {
                    controlsToMoveToPanel2.Add(control);
                }
            }

            foreach (System.Windows.Forms.Control control in controlsToMoveToPanel2)
            {
                tabPage1.Controls.Remove(control);
                panel2.Controls.Add(control);
            }

            /////////////////
            // Khởi tạo Panel cho panel3 nếu chưa có
            if (panel3 == null)
            {
                panel3 = new Panel();
                panel3.Dock = DockStyle.Fill;
                panel3.AutoScroll = true;
                tabPage2.Controls.Add(panel3);
            }

            // Di chuyển các điều khiển vào Panel panel3
            List<System.Windows.Forms.Control> controlsToMoveToPanel3 = new List<System.Windows.Forms.Control>();
            foreach (System.Windows.Forms.Control control in tabPage2.Controls)
            {
                if (control != panel3)
                {
                    controlsToMoveToPanel3.Add(control);
                }
            }

            foreach (System.Windows.Forms.Control control in controlsToMoveToPanel3)
            {
                tabPage2.Controls.Remove(control);
                panel3.Controls.Add(control);
            }

           
            ////////////////////////////////////////////////

            // Khởi tạo Panel cho panel4 nếu chưa có
            if (panel4 == null)
            {
                panel4 = new Panel();
                panel4.Dock = DockStyle.Fill;
                panel4.AutoScroll = true;
                tabPage3.Controls.Add(panel4);
            }

            // Di chuyển các điều khiển vào Panel panel4
            List<System.Windows.Forms.Control> controlsToMoveToPanel5 = new List<System.Windows.Forms.Control>();
            foreach (System.Windows.Forms.Control control in tabPage3.Controls)
            {
                if (control != panel4)
                {
                    controlsToMoveToPanel5.Add(control);
                }
            }

            foreach (System.Windows.Forms.Control control in controlsToMoveToPanel5)
            {
                tabPage3.Controls.Remove(control);
                panel4.Controls.Add(control);
            }




            // Khởi tạo Panel cho panel5 nếu chưa có
            if (panel5 == null)
            {
                panel5 = new Panel();
                panel5.Dock = DockStyle.Fill;
                panel5.AutoScroll = true;
                tabPage4.Controls.Add(panel5);
            }

            // Di chuyển các điều khiển vào Panel panel5
            List<System.Windows.Forms.Control> controlsToMoveToPanel6 = new List<System.Windows.Forms.Control>();
            foreach (System.Windows.Forms.Control control in tabPage4.Controls)
            {
                if (control != panel5)
                {
                    controlsToMoveToPanel6.Add(control);
                }
            }

            foreach (System.Windows.Forms.Control control in controlsToMoveToPanel6)
            {
                tabPage4.Controls.Remove(control);
                panel5.Controls.Add(control);
            }
            btn_CapNhatCT_DN.Click += Btn_CapNhatCT_DN_Click;

        }

        private void Btn_XuatTK_TTB_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ DataGridView
            List<ThongKeSuaChua> thongKeData = (List<ThongKeSuaChua>)dataGV_TKSL_TTB.DataSource;

            if (thongKeData == null || thongKeData.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo một ứng dụng Excel mới
            Excel.Application excelApp = new Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Excel không được cài đặt trên máy tính của bạn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo một workbook mới và thêm một worksheet
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];

            // Thiết lập tiêu đề cột
            worksheet.Cells[1, 1] = "STT";
            worksheet.Cells[1, 2] = "Mã Thiết Bị";
            worksheet.Cells[1, 3] = "Tên Thiết Bị";
            worksheet.Cells[1, 4] = "Số Lượng Sửa Chữa";
            worksheet.Cells[1, 5] = "Tổng Chi Phí Sửa Chữa";

            // Ghi dữ liệu vào worksheet
            int rowIndex = 2;  // Bắt đầu ghi từ hàng thứ 2
            int stt = 1;  // Số thứ tự bắt đầu từ 1
            foreach (var item in thongKeData)
            {
                worksheet.Cells[rowIndex, 1] = stt;
                worksheet.Cells[rowIndex, 2] = item.MaThietBi;
                worksheet.Cells[rowIndex, 3] = item.TenThietBi;
                worksheet.Cells[rowIndex, 4] = item.SoLuongSuaChua;
                worksheet.Cells[rowIndex, 5] = item.TongChiPhiSuaChua;
                rowIndex++;
                stt++;
            }

            // Tùy chỉnh định dạng
            worksheet.Columns.AutoFit();  // Tự động điều chỉnh chiều rộng cột

            // Hiển thị Excel
            excelApp.Visible = true;

            // Giải phóng tài nguyên
            Marshal.ReleaseComObject(worksheet);
            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(excelApp);
        }

        private void Btn_CapNhatCT_DN_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ các textbox và kiểm tra tính hợp lệ
                string maYCSuaChua = textQL_MaYCSC.Text.Trim(); // Giả sử bạn có textbox chứa MaYCSuaChua
                string maThietBi = txt_ql_matb.Text.Trim();
                string maPhong = txt_ql_maphong.Text.Trim();

                if (!int.TryParse(txt_ql_soluong.Text.Trim(), out int soLuong))
                {
                    MessageBox.Show("Số lượng không hợp lệ. Vui lòng nhập lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string tinhTrang = txt_ql_tinhtrang.Text.Trim();

                if (!decimal.TryParse(txt_ql_chiphi.Text.Trim(), out decimal phiSuaChua))
                {
                    MessageBox.Show("Chi phí sửa chữa không hợp lệ. Vui lòng nhập lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tạo một instance của BLL_YeuCauSuaChua
                BLL_YeuCauSuaChua bllYeuCauSuaChua = new BLL_YeuCauSuaChua();

                // Gọi hàm cập nhật yêu cầu sửa chữa từ BLL
                bool isUpdated = bllYeuCauSuaChua.CapNhatYeuCauSuaChua(maYCSuaChua, maThietBi, soLuong, tinhTrang, phiSuaChua);

                // Kiểm tra kết quả và hiển thị thông báo
                if (isUpdated)
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Gọi hàm load lại DataGridView
                    SetupDataGV_QL_CT_YCSC(maYCSuaChua);
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại. Vui lòng kiểm tra lại thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi tổng quát và hiển thị thông báo chi tiết
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void DataGV_QL_TTB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng có click vào hàng hợp lệ không (không phải tiêu đề cột hoặc hàng trống)
            if (e.RowIndex >= 0 && e.RowIndex < dataGV_QL_TTB.Rows.Count)
            {
                // Lấy hàng được chọn
                DataGridViewRow selectedRow = dataGV_QL_TTB.Rows[e.RowIndex];

                // Gán giá trị từ các cột của hàng được chọn vào các TextBox tương ứng
                txt_ql_maphong.Text = selectedRow.Cells["MaPhong"].Value?.ToString() ?? string.Empty;
                txt_ql_matb.Text = selectedRow.Cells["MaThietBi"].Value?.ToString() ?? string.Empty;
                txt_ql_tinhtrang.Text = selectedRow.Cells["TinhTrang"].Value?.ToString() ?? string.Empty;
                txt_ql_soluong.Text = selectedRow.Cells["SoLuong"].Value?.ToString() ?? string.Empty;
                txt_ql_chiphi.Text = selectedRow.Cells["ChiPhiSuaChua"].Value?.ToString() ?? string.Empty;
            }
            else
            {
                // Nếu người dùng click vào vị trí không hợp lệ, xóa sạch các TextBox
                txt_ql_maphong.Clear();
                txt_ql_matb.Clear();
                txt_ql_tinhtrang.Clear();
                txt_ql_soluong.Clear();
                txt_ql_chiphi.Clear();
            }
        }

        private void Txt_MaSinhVien_TextChanged(object sender, EventArgs e)
        {
            // Lấy mã sinh viên từ ô nhập liệu
            string maSinhVien = txt_MaSinhVien.Text.Trim();

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
                    txt_HoVaTen_SV.Text = sinhVienDTO.HoTen;


                    // Điền mã phòng (nếu có)
                    text_maphong.Text = sinhVienDTO.Phong?.MaPhong;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Text_maphong_KeyDown(object sender, KeyEventArgs e)
        {
            // Kiểm tra nếu phím Enter được nhấn
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(text_maphong.Text)) // Kiểm tra nếu mã phòng không trống
                {
                    string maPhong = text_maphong.Text; // Lấy mã phòng từ TextBox

                    var thongTinPhong = bll_tt_diennuoc.LayThongTinPhong(maPhong);
                    if (thongTinPhong != null)
                    {
                        // Giả sử thongTinPhong là một đối tượng chứa các thông tin cần thiết, bạn có thể xử lý như sau:
                        txt_MaSinhVien.Text = thongTinPhong.MaSV_TP;
                        // txt_Hoten_TP.Text = thongTinPhong.HoTen_TP;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin phòng với mã phòng này.");
                    }

                    string tenLoaiPhong = bll_ttb.GetTenLoaiPhong(maPhong); // Gọi BLL để lấy tên loại phòng

                    if (!string.IsNullOrEmpty(tenLoaiPhong))
                    {
                        text_LoaiPhong.Text = tenLoaiPhong; // Hiển thị tên loại phòng

                        // Lấy danh sách trang thiết bị theo mã phòng (hoặc loại phòng)
                        var trangThietBiList = bll_ttb.GetTrangThietBiByMaPhong(maPhong); // Gọi BLL để lấy danh sách trang thiết bị

                        // Cập nhật ComboBox với danh sách trang thiết bị
                        cbo_Ten_TTB.Items.Clear(); // Xóa tất cả các item cũ trong cbo_Ten_TTB

                        if (trangThietBiList.Any()) // Kiểm tra nếu có trang thiết bị
                        {
                            foreach (var thietBi in trangThietBiList)
                            {
                                cbo_Ten_TTB.Items.Add(thietBi); // Thêm từng trang thiết bị vào cbo_Ten_TTB
                            }
                        }
                        else
                        {
                            cbo_Ten_TTB.Items.Add("Không có trang thiết bị cho loại phòng này"); // Nếu không có trang thiết bị
                        }
                    }
                    else
                    {
                        text_LoaiPhong.Text = "Không có thông tin loại phòng!"; // Nếu không tìm thấy tên loại phòng
                        cbo_Ten_TTB.Items.Clear(); // Xóa tất cả các item cũ trong cbo_Ten_TTB
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập mã phòng!"); // Thông báo khi mã phòng trống
                }
            }
        }


        private void Btn_Sua_YCSC_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem người dùng đã chọn một hàng trong DataGridView chưa
                if (dataGVQL_DS_YCSC.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn một yêu cầu sửa chữa để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy mã yêu cầu sửa chữa từ cột thứ 2 (index = 1)
                string maYCSuaChua = dataGVQL_DS_YCSC.CurrentRow.Cells[1].Value?.ToString();

                if (string.IsNullOrEmpty(maYCSuaChua))
                {
                    MessageBox.Show("Không thể lấy mã yêu cầu sửa chữa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string maNhanVien = txtQL_MaNhanVien.Text;
                DateTime? ngayLap = dateTime_QL_NgayLap.Value;
                DateTime? ngayHoanTat = dateTime_QL_NgayHT.Value;
                // Lấy giá trị trạng thái từ ComboBox
                string trangThai = cbo_QL_TrangThai.SelectedItem?.ToString() ?? string.Empty;
                decimal? tongTien = string.IsNullOrEmpty(text_QL_TongTien.Text) ? (decimal?)null : decimal.Parse(text_QL_TongTien.Text); // Tổng tiền (nếu có)

                // Kiểm tra điều kiện bắt buộc (ví dụ: mã yêu cầu sửa chữa)
                if (string.IsNullOrEmpty(maYCSuaChua))
                {
                    MessageBox.Show("Mã yêu cầu sửa chữa không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tạo đối tượng YeuCauSuaChua và điền dữ liệu
                YeuCauSuaChua ycSuaChua = new YeuCauSuaChua
                {
                    MaYCSuaChua = maYCSuaChua,
                    MaNhanVien = maNhanVien,
                    NgayLap = ngayLap,
                    NgayHoanTat = ngayHoanTat,
                    TrangThai = trangThai,
                    TongTien = tongTien
                };

               

                // Gọi hàm cập nhật từ BLL
                bool ketQua = bll_ycsc.CapNhatYeuCauSuaChua(ycSuaChua);

                // Xử lý kết quả
                if (ketQua)
                {
                    loadDS_YCSD();
                    MessageBox.Show("Cập nhật yêu cầu sửa chữa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Cập nhật lại danh sách sau khi xóa
                   
                }
                else
                {
                    MessageBox.Show("Cập nhật yêu cầu sửa chữa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Btn_xoa_YCCS_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem người dùng đã chọn một hàng trong DataGridView chưa
                if (dataGVQL_DS_YCSC.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn một yêu cầu sửa chữa để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy mã yêu cầu sửa chữa từ cột thứ 2 (index = 1)
                string maYCSuaChua = dataGVQL_DS_YCSC.CurrentRow.Cells[1].Value?.ToString();

                if (string.IsNullOrEmpty(maYCSuaChua))
                {
                    MessageBox.Show("Không thể lấy mã yêu cầu sửa chữa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Hiển thị hộp thoại xác nhận
                DialogResult xacNhan = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa yêu cầu sửa chữa với mã '{maYCSuaChua}' không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                // Nếu người dùng chọn "Yes"
                if (xacNhan == DialogResult.Yes)
                {
                    // Gọi hàm BLL để xóa yêu cầu sửa chữa
                    bool ketQua = bll_ycsc.XoaYeuCauSuaChua(maYCSuaChua);

                    if (ketQua)
                    {
                        MessageBox.Show("Xóa yêu cầu sửa chữa thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Cập nhật lại danh sách sau khi xóa
                        loadDS_YCSD();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa yêu cầu sửa chữa. Vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa yêu cầu sửa chữa: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Btn_timKiem_ngay_thang_nam_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị từ các trường nhập
                string ngayNhap = txt_Ngay.Text.Trim();
                string thangNhap = cbo_LocThang.SelectedItem?.ToString();
                string namNhap = txt_LocNam.Text.Trim();

                // Kiểm tra các trường hợp nhập thông tin
                if (!string.IsNullOrEmpty(ngayNhap) && !string.IsNullOrEmpty(thangNhap) && !string.IsNullOrEmpty(namNhap))
                {
                    // Tìm kiếm theo ngày, tháng và năm
                    DateTime ngayThangNam;
                    if (DateTime.TryParse($"{namNhap}-{thangNhap}-{ngayNhap}", out ngayThangNam))
                    {
                        var danhSach = bll_ycsc.GetYeuCauSuaChuaByNgayThangNam(ngayThangNam);
                        // Xóa dữ liệu cũ
                        dataGVQL_DS_YCSC.Rows.Clear();

                        // Thêm dữ liệu mới vào DataGridView
                        int stt = 1;
                        foreach (var yc in danhSach)
                        {
                            dataGVQL_DS_YCSC.Rows.Add(stt++,
                                yc.MaYCSuaChua,
                                yc.MaNhanVien,
                                yc.NgayLap?.ToString("dd/MM/yyyy"),
                                yc.NgayHoanTat?.ToString("dd/MM/yyyy"),
                                yc.TrangThai,
                                yc.TongTien);
                            Console.WriteLine($"Đã thêm dòng: {yc.MaYCSuaChua}");  // Debug
                        }
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
                        var danhSach = bll_ycsc.GetYeuCauSuaChuaByThangVaNam(thang, nam);
                        // Xóa dữ liệu cũ
                        dataGVQL_DS_YCSC.Rows.Clear();

                        // Thêm dữ liệu mới vào DataGridView
                        int stt = 1;
                        foreach (var yc in danhSach)
                        {
                            dataGVQL_DS_YCSC.Rows.Add(stt++,
                                yc.MaYCSuaChua,
                                yc.MaNhanVien,
                                yc.NgayLap?.ToString("dd/MM/yyyy"),
                                yc.NgayHoanTat?.ToString("dd/MM/yyyy"),
                                yc.TrangThai,
                                yc.TongTien);
                            Console.WriteLine($"Đã thêm dòng: {yc.MaYCSuaChua}");  // Debug
                        }
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
                        var danhSach = bll_ycsc.GetYeuCauSuaChuaByNam(nam);
                        // Xóa dữ liệu cũ
                        dataGVQL_DS_YCSC.Rows.Clear();

                        // Thêm dữ liệu mới vào DataGridView
                        int stt = 1;
                        foreach (var yc in danhSach)
                        {
                            dataGVQL_DS_YCSC.Rows.Add(stt++,
                                yc.MaYCSuaChua,
                                yc.MaNhanVien,
                                yc.NgayLap?.ToString("dd/MM/yyyy"),
                                yc.NgayHoanTat?.ToString("dd/MM/yyyy"),
                                yc.TrangThai,
                                yc.TongTien);
                            Console.WriteLine($"Đã thêm dòng: {yc.MaYCSuaChua}");  // Debug
                        }
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



        private void Cbo_QL_TrangThai_YCSC_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            try
            {
                // Lấy giá trị mã nhân viên từ ComboBox
                string maTrangThai = cbo_QL_TrangThai_YCSC.SelectedItem?.ToString();
                // MessageBox.Show("" + maYCSuaChua);

                if (string.IsNullOrEmpty(maTrangThai))
                {
                    // Nếu chọn mục trống, tải lại danh sách tất cả yêu cầu sửa chữa
                    loadDS_YCSD();
                }
                else
                {
                    var danhSachYCSC = bll_ycsc.GetYeuCauSuaChuaByTrangThai(maTrangThai);

                    // Xóa dữ liệu cũ
                    dataGVQL_DS_YCSC.Rows.Clear();

                    // Thêm dữ liệu mới vào DataGridView
                    int stt = 1;
                    foreach (var yc in danhSachYCSC)
                    {
                        dataGVQL_DS_YCSC.Rows.Add(stt++,
                            yc.MaYCSuaChua,
                            yc.MaNhanVien,
                            yc.NgayLap?.ToString("dd/MM/yyyy"),
                            yc.NgayHoanTat?.ToString("dd/MM/yyyy"),
                            yc.TrangThai,
                            yc.TongTien);
                        Console.WriteLine($"Đã thêm dòng: {yc.MaYCSuaChua}");  // Debug
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc danh sách: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_TK_TSSC_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ các điều khiển trên form
            int nam = int.Parse(txt_Nam_TK_TSSC.Text);  // Năm từ ComboBox
            int thang = int.Parse(cbo_Thang_TK_TSSC.SelectedItem.ToString());  // Tháng từ ComboBox

            // Tạo đối tượng BLL_YeuCauSuaChua
            BLL_YeuCauSuaChua bll = new BLL_YeuCauSuaChua();

            // Lấy dữ liệu từ BLL
            List<ThongKeSuaChuaTanSuat> thongKeData = bll.GetTanSuatHongHoc(nam, thang);

            // Đưa dữ liệu vào DataGridView
            dataGV_TK_TSSC.DataSource = thongKeData;

            // Vẽ biểu đồ
            DrawPieChart(thongKeData);
        }
        private void DrawPieChart(List<ThongKeSuaChuaTanSuat> thongKeData)
        {
            // Xóa dữ liệu cũ trong Chart
            chart_TK_TSSC.Series.Clear();

            // Tạo Series mới
            var series = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Tần Suất Hỏng Hóc",
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie, // Biểu đồ hình tròn
                XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String,
                YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32
            };

            chart_TK_TSSC.Series.Add(series);

            // Thêm dữ liệu vào Series
            foreach (var item in thongKeData)
            {
                series.Points.AddXY(item.TenThietBi, item.SoLanHongHoc);
            }

            // Thiết lập hiển thị biểu đồ
            chart_TK_TSSC.Legends.Clear(); // Xóa legends cũ nếu có
            var legend = new System.Windows.Forms.DataVisualization.Charting.Legend
            {
                Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Right,
                Alignment = StringAlignment.Center
            };
            chart_TK_TSSC.Legends.Add(legend);
        }


        private void Btn_TKSL_TTB_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ các điều khiển trên form
            int nam = int.Parse(txt_nam_TKSL_TTB.Text);  // Năm từ TextBox
            int thang = int.Parse(cbo_thang_TKSL_TTB.SelectedItem.ToString());  // Tháng từ ComboBox

            // Tạo đối tượng BLL_YeuCauSuaChua
            BLL_YeuCauSuaChua bll = new BLL_YeuCauSuaChua();

            // Lấy dữ liệu từ BLL
            List<ThongKeSuaChua> thongKeData = bll.GetThongKeSuaChua(nam, thang);

            // Đưa dữ liệu vào DataGridView
            dataGV_TKSL_TTB.DataSource = thongKeData;

            // Thêm cột STT vào DataGridView (nếu cần)
            int stt = 1;
            foreach (DataGridViewRow row in dataGV_TKSL_TTB.Rows)
            {
                row.Cells["STT"].Value = stt;
                stt++;
            }
            // Vẽ biểu đồ
            DrawChart(thongKeData);
        }
        private void DrawChart(List<ThongKeSuaChua> thongKeData)
        {
            // Xóa dữ liệu cũ trong Chart
            chart_TKSL_TTB.Series.Clear();

            // Tạo Series mới
            var series = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Số Lượng",
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column, // Biểu đồ cột
                XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String,
                YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32
            };

            chart_TKSL_TTB.Series.Add(series);

            // Thêm dữ liệu vào Series
            foreach (var item in thongKeData)
            {
                series.Points.AddXY(item.TenThietBi, item.SoLuongSuaChua);
            }

            // Thiết lập trục và hiển thị biểu đồ
            chart_TKSL_TTB.ChartAreas[0].AxisX.Title = "Trang Thiết Bị";
            chart_TKSL_TTB.ChartAreas[0].AxisY.Title = "Số Lượng ";
            chart_TKSL_TTB.ChartAreas[0].AxisX.Interval = 1; // Hiển thị từng tên trên trục hoành
        }


        private void Cbo_SapXep_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra giá trị được chọn trong cbo_SapXep
                string selectedValue = cbo_SapXep.SelectedItem.ToString();

                List<YeuCauSuaChua> danhSachYeuCau;

                // Kiểm tra tùy chọn và gọi các hàm BLL tương ứng
                if (selectedValue == "Tăng")
                {
                    // Lấy danh sách yêu cầu sửa chữa theo chi phí tăng dần
                    danhSachYeuCau = bll_ycsc.GetYeuCauSuaChuaByCostAsc();
                    UpdateDataGridView(danhSachYeuCau);
                }
                else if (selectedValue == "Giảm")
                {
                    // Lấy danh sách yêu cầu sửa chữa theo chi phí giảm dần
                    danhSachYeuCau = bll_ycsc.GetYeuCauSuaChuaByCostDesc();
                    UpdateDataGridView(danhSachYeuCau);
                }
                else
                {
                     loadDS_YCSD();
                }

                // Cập nhật DataGridView với danh sách đã lọc
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc danh sách yêu cầu sửa chữa: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateDataGridView(List<YeuCauSuaChua> danhSachYeuCau)
        {
            // Xóa dữ liệu cũ trong DataGridView
            dataGVQL_DS_YCSC.Rows.Clear();

            // Thêm dữ liệu vào DataGridView
            int stt = 1; // Biến đếm số thứ tự
            foreach (var yc in danhSachYeuCau)
            {
                dataGVQL_DS_YCSC.Rows.Add(
                    stt++, // Số thứ tự
                    yc.MaYCSuaChua,
                    yc.MaNhanVien,
                    yc.NgayLap?.ToString("dd/MM/yyyy"),  // Định dạng ngày
                    yc.NgayHoanTat?.ToString("dd/MM/yyyy"),  // Định dạng ngày
                    yc.TrangThai,
                    yc.TongTien?.ToString("C") // Định dạng tiền tệ
                );
            }
        }

        private void Cbo_QL_MaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị mã nhân viên từ ComboBox
                string maNhanVien = cbo_QL_MaNV.SelectedItem?.ToString();
                // MessageBox.Show("" + maYCSuaChua);

                if (string.IsNullOrEmpty(maNhanVien))
                {
                    // Nếu chọn mục trống, tải lại danh sách tất cả yêu cầu sửa chữa
                    loadDS_YCSD();
                }
                else
                {
                    // Nếu có mã yêu cầu sửa chữa, lọc theo mã
                    LoadDanhSachYCSCTheoMaNV(maNhanVien);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc danh sách: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cbo_QL_MaYCSC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị mã yêu cầu sửa chữa từ ComboBox
                string maYCSuaChua = cbo_QL_MaYCSC.SelectedItem?.ToString();
                // MessageBox.Show("" + maYCSuaChua);

                if (string.IsNullOrEmpty(maYCSuaChua))
                {
                    // Nếu chọn mục trống, tải lại danh sách tất cả yêu cầu sửa chữa
                    loadDS_YCSD();
                }
                else
                {
                    // Nếu có mã yêu cầu sửa chữa, lọc theo mã
                    LoadDanhSachYeuCauSuaChua(maYCSuaChua);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc danh sách: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadDanhSachYCSCTheoMaNV(string maNhanVien)
        {
            try
            {
                // Gọi BLL để lấy danh sách lọc
                var danhSach = bll_ycsc.GetYeuCauSuaChuaByMaNhanVien(maNhanVien);

                // Xóa dữ liệu cũ
                dataGVQL_DS_YCSC.Rows.Clear();

                // Thêm dữ liệu mới vào DataGridView
                int stt = 1;
                foreach (var yc in danhSach)
                {
                    dataGVQL_DS_YCSC.Rows.Add(stt++,
                        yc.MaYCSuaChua,
                        yc.MaNhanVien,
                        yc.NgayLap?.ToString("dd/MM/yyyy"),
                        yc.NgayHoanTat?.ToString("dd/MM/yyyy"),
                        yc.TrangThai,
                        yc.TongTien);
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadDanhSachYeuCauSuaChua(string maYCSuaChua)
        {
            try
            {
                // Gọi BLL để lấy danh sách lọc
                var danhSach = bll_ycsc.GetYeuCauSuaChuaByMa(maYCSuaChua);

                // Xóa dữ liệu cũ
                dataGVQL_DS_YCSC.Rows.Clear();

                // Thêm dữ liệu mới vào DataGridView
                int stt = 1;
                foreach (var yc in danhSach)
                {
                    dataGVQL_DS_YCSC.Rows.Add(stt++,
                        yc.MaYCSuaChua,
                        yc.MaNhanVien,
                        yc.NgayLap?.ToString("dd/MM/yyyy"),
                        yc.NgayHoanTat?.ToString("dd/MM/yyyy"),
                        yc.TrangThai,
                        yc.TongTien);
                    Console.WriteLine($"Đã thêm dòng: {yc.MaYCSuaChua}");  // Debug
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private void LoadTrangThaiYCSCToComboBox()
        {
            try
            {
                var danhSachYCS = bll_ycsc.GetAllMaYeuCauSuaChua();

                cbo_QL_TrangThai_YCSC.Items.Clear();


                // Thêm mục trống vào ComboBox
                cbo_QL_TrangThai_YCSC.Items.Add("");  // Mục trống để lọc lại tất cả dữ liệu


                // Lọc các trạng thái duy nhất
                var uniqueTrangThai = danhSachYCS
                    .Select(tt => tt.TrangThai) // Chọn cột TrangThai
                    .Distinct() // Lọc các giá trị duy nhất
                    .ToList();

                // Thêm các trạng thái duy nhất vào ComboBox
                foreach (var tt in uniqueTrangThai)
                {
                    cbo_QL_TrangThai_YCSC.Items.Add(tt);
                }

                if (cbo_QL_TrangThai_YCSC.Items.Count > 0)
                {
                    cbo_QL_TrangThai_YCSC.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách trạng thái: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadMaNhanVienToComboBox()
        {
            try
            {
                var danhSachNV = bll_nv.GetAllMaNhanVien();

                cbo_QL_MaNV.Items.Clear();
                cbo_QL_MaNV.Items.Add("");

                foreach (var nv in danhSachNV)
                {
                    cbo_QL_MaNV.Items.Add(nv.MaNhanVien);
                }

                if (cbo_QL_MaNV.Items.Count > 0)
                {
                    cbo_QL_MaNV.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách mã nhân viên: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMaYeuCauSuaChuaToComboBox()
        {
            try
            {
                var danhSachYCS = bll_ycsc.GetAllMaYeuCauSuaChua();

                cbo_QL_MaYCSC.Items.Clear();

                // Thêm mục trống vào ComboBox
                cbo_QL_MaYCSC.Items.Add("");  // Mục trống để lọc lại tất cả dữ liệu

                // Thêm các mã yêu cầu sửa chữa vào ComboBox
                foreach (var yc in danhSachYCS)
                {
                    cbo_QL_MaYCSC.Items.Add(yc.MaYCSuaChua);
                }

                // Nếu có ít nhất một item, chọn item đầu tiên
                if (cbo_QL_MaYCSC.Items.Count > 0)
                {
                    cbo_QL_MaYCSC.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách mã yêu cầu sửa chữa: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DataGVQL_DS_YCSC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy mã yêu cầu sửa chữa từ cột đầu tiên
                string maYCSuaChua = dataGVQL_DS_YCSC.Rows[e.RowIndex].Cells[1].Value.ToString();

                // Gọi hàm để hiển thị chi tiết yêu cầu sửa chữa vào DataGridView
                SetupDataGV_QL_CT_YCSC(maYCSuaChua);
                ThongTinYeuCauSuaChua ycscDetails = bll_ycsc.GetYeuCauSuaChuaDetails(maYCSuaChua);

                if (ycscDetails != null)
                {
                    // Hiển thị thông tin lên giao diện
                    textQL_MaYCSC.Text = ycscDetails.MaYCSuaChua;
                    txtQL_MaNhanVien.Text = ycscDetails.MaNhanVien;
                    dateTime_QL_NgayLap.Value = ycscDetails.NgayLap ?? DateTime.Now; // Kiểm tra null
                    dateTime_QL_NgayHT.Value = ycscDetails.NgayHoanTat ?? DateTime.Now; // Kiểm tra null

                    // Kiểm tra nếu giá trị TrangThai tồn tại trong cbo_QL_TrangThai
                    if (cbo_QL_TrangThai.Items.Contains(ycscDetails.TrangThai))
                    {
                        cbo_QL_TrangThai.SelectedItem = ycscDetails.TrangThai;
                    }
                    else
                    {
                        MessageBox.Show("Trạng thái không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    cbo_QL_TrangThai.SelectedItem = ycscDetails.TrangThai;
                    text_QL_TongTien.Text = ycscDetails.TongTien.HasValue ? ycscDetails.TongTien.Value.ToString("N0") : "0"; // Hiển thị tiền
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin chi tiết yêu cầu sửa chữa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void TxtQL_MaNhanVien_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtQL_MaNhanVien.Text)) // Kiểm tra nếu mã nhân viên không trống
            {
                try
                {
                    string maNhanVien = txtQL_MaNhanVien.Text.Trim(); // Lấy mã nhân viên từ TextBox và loại bỏ khoảng trắng thừa

                    // Tạo một instance của BLL_NhanVien
                    BLL_NhanVien bllNhanVien = new BLL_NhanVien();

                    // Lấy thông tin chi tiết nhân viên từ mã nhân viên
                    var nhanVien = bllNhanVien.GetNhanVien_MaNV(maNhanVien);

                    if (nhanVien != null)
                    {
                        // Hiển thị thông tin nhân viên trên các TextBox
                        txt_HoTenNV.Text = nhanVien.HoTen;
                        txt_ChucVuNhanVien.Text = nhanVien.ChucVu;

                        // Nếu cần thêm thông tin, hiển thị tại đây
                        // txt_DienThoai.Text = nhanVien.DienThoai;
                        // txt_Email.Text = nhanVien.Email;
                    }
                    else
                    {
                        // Xóa thông tin cũ nếu không tìm thấy nhân viên
                        txt_HoTenNV.Clear();
                        txt_ChucVuNhanVien.Clear();

                        MessageBox.Show("Không tìm thấy thông tin nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy thông tin nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Xóa thông tin trên các TextBox nếu mã nhân viên trống
                txt_HoTenNV.Clear();
                txt_ChucVuNhanVien.Clear();

                MessageBox.Show("Vui lòng nhập mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        // Hàm load dữ liệu vào DataGridView
        private void loadDS_YCSD()
        {
            try
            {
                // Lấy danh sách yêu cầu sửa chữa từ BLL
                var danhSachYeuCau = bll_ycsc.LayDanhSachYeuCauSuaChua()
                                             .Where(yc => yc.TrangThai != "Đã Huỷ") // Lọc yêu cầu không phải trạng thái "Đã Huỷ"
                                             .ToList();

                // Xóa dữ liệu cũ trong DataGridView
                dataGVQL_DS_YCSC.Rows.Clear();

                // Thêm dữ liệu vào DataGridView
                int stt = 1; // Biến đếm số thứ tự
                foreach (var yc in danhSachYeuCau)
                {
                    dataGVQL_DS_YCSC.Rows.Add(
                        stt++, // Số thứ tự
                        yc.MaYCSuaChua,
                        yc.MaNhanVien,
                        yc.NgayLap?.ToString("dd/MM/yyyy"), // Định dạng ngày
                        yc.NgayHoanTat?.ToString("dd/MM/yyyy"), // Định dạng ngày
                        yc.TrangThai,
                        yc.TongTien?.ToString("C") // Định dạng tiền tệ
                    );
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có
                MessageBox.Show("Lỗi khi tải danh sách yêu cầu sửa chữa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Xuat_PYCSC_SV_Click(object sender, EventArgs e)
        {
            // Đường dẫn tới mẫu Word
            string templatePath = @"D:\DACN\PhieuYeuCauSuaChua\PhieuYCSC_SV.docx";
            string outputPath = @"D:\DACN\PhieuYeuCauSuaChua\PhieuYCSC_SV_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".docx";

            // Kiểm tra xem file mẫu có tồn tại không
            if (!File.Exists(templatePath))
            {
                MessageBox.Show("Không tìm thấy file mẫu tại: " + templatePath, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo ứng dụng Word và mở file mẫu
            Microsoft.Office.Interop.Word._Application wordApp = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(templatePath);

            try
            {
                // Lấy giá trị từ giao diện người dùng
                string maSinhVien = txt_MaSinhVien.Text;
                string hoVaTenSinhVien = txt_HoVaTen_SV.Text;
                string hoVaTenNhanVien = "Trần Bình";
                string chucVu = "Nhân Viên Quản Lý";
                string soQuyetDinh = "1110";
                string ngayLap = dateTime_NgayLap.Value.ToString("dd");
                string thangLap = dateTime_NgayLap.Value.ToString("MM");
                string namLap = dateTime_NgayLap.Value.ToString("yyyy");

                // Thay thế các placeholder trong tài liệu
                ReplacePlaceholder(doc, "«SoQuyetDinh»", soQuyetDinh);
                ReplacePlaceholder(doc, "«Ngay»", ngayLap);
                ReplacePlaceholder(doc, "«Thang»", thangLap);
                ReplacePlaceholder(doc, "«Nam»", namLap);
                ReplacePlaceholder(doc, "«MaSoSinhVien»", maSinhVien);
                ReplacePlaceholder(doc, "«HoTenSinhVien»  ", hoVaTenSinhVien);
                ReplacePlaceholder(doc, "«HoVaTenNhanVien»", hoVaTenNhanVien);
                ReplacePlaceholder(doc, "«ChucVu»", chucVu);

                // Tìm bảng trong Word
                Microsoft.Office.Interop.Word.Table targetTable = null;
                foreach (Microsoft.Office.Interop.Word.Table tbl in doc.Tables)
                {
                    if (tbl.Rows.Count > 0 && tbl.Cell(1, 1).Range.Text.Contains("STT"))
                    {
                        targetTable = tbl;
                        break;
                    }
                }

                if (targetTable == null)
                {
                    MessageBox.Show("Không tìm thấy bảng phù hợp trong tài liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Xóa các hàng cũ trong bảng (trừ hàng tiêu đề)
                while (targetTable.Rows.Count > 1)
                {
                    targetTable.Rows[2].Delete();
                }

                // Lặp qua các hàng trong DataGridView và thêm vào bảng Word
                for (int i = 0; i < dataGV_DS_YCSC.Rows.Count; i++)
                {
                    string stt = (i + 1).ToString();
                    string maPhong = dataGV_DS_YCSC.Rows[i].Cells["MaPhong"].Value?.ToString() ?? "";
                    string maThietBi = dataGV_DS_YCSC.Rows[i].Cells["MaThietBi"].Value?.ToString() ?? "";
                    string tenThietBi = dataGV_DS_YCSC.Rows[i].Cells["TenThietBi"].Value?.ToString() ?? "";
                    string soLuong = dataGV_DS_YCSC.Rows[i].Cells["SoLuong"].Value?.ToString() ?? "";
                    string trangThai = dataGV_DS_YCSC.Rows[i].Cells["TinhTrang"].Value?.ToString() ?? "";

                    // Thêm một dòng mới vào bảng
                    Microsoft.Office.Interop.Word.Row newRow = targetTable.Rows.Add();

                    // Gán giá trị vào các ô của dòng mới
                    newRow.Cells[1].Range.Text = stt;
                    newRow.Cells[2].Range.Text = maPhong;
                    newRow.Cells[3].Range.Text = maThietBi;
                    newRow.Cells[4].Range.Text = tenThietBi;
                    newRow.Cells[5].Range.Text = soLuong;
                    newRow.Cells[6].Range.Text = trangThai;
                }

                // Lưu tài liệu với tên file mới
                doc.SaveAs2(outputPath);
                MessageBox.Show("Phiếu yêu cầu sửa chữa đã được lưu tại: " + outputPath, "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tạo phiếu yêu cầu sửa chữa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Đóng tài liệu và ứng dụng Word
                doc.Close();
                wordApp.Quit();

                // Giải phóng tài nguyên COM
                System.Runtime.InteropServices.Marshal.ReleaseComObject(doc);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
            }
        }


        // danh sách yêu cầu sửa chữa từ sinh viên
        private void DataGV_DS_YCSC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Kiểm tra xem có dòng nào được chọn không
                if (e.RowIndex >= 0 && e.RowIndex < dataGV_DS_YCSC.Rows.Count)
                {
                    DataGridViewRow selectedRow = dataGV_DS_YCSC.Rows[e.RowIndex];

                    // Lấy dữ liệu từ dòng được chọn và hiển thị lên các TextBox
                    txt_maphong.Text = selectedRow.Cells["MaPhong"].Value?.ToString();
                    txt_MaTTB.Text = selectedRow.Cells["MaThietBi"].Value?.ToString();
                    cbo_Ten_TTB.SelectedItem = selectedRow.Cells["TenThietBi"].Value?.ToString();
                    txt_soluong_sc.Text = selectedRow.Cells["SoLuong"].Value?.ToString();
                    txt_tinhtrang_sc.Text = selectedRow.Cells["TinhTrang"].Value?.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //sửa
        private void Btn_Sua_TTB_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có dòng nào được chọn không
                if (dataGV_DS_YCSC.SelectedRows.Count > 0)
                {
                    // Lấy dòng đang chọn
                    int selectedRowIndex = dataGV_DS_YCSC.SelectedRows[0].Index;
                    DataGridViewRow selectedRow = dataGV_DS_YCSC.Rows[selectedRowIndex];

                    // Cập nhật thông tin dòng từ các TextBox
                    selectedRow.Cells["MaPhong"].Value = text_maphong.Text;
                    selectedRow.Cells["MaThietBi"].Value = txt_MaTTB.Text;
                    selectedRow.Cells["TenThietBi"].Value = cbo_Ten_TTB.SelectedItem != null ? cbo_Ten_TTB.SelectedItem.ToString() : string.Empty;
                    selectedRow.Cells["SoLuong"].Value = int.Parse(txt_soluong_sc.Text);
                    selectedRow.Cells["TinhTrang"].Value = txt_tinhtrang_sc.Text;
                    selectedRow.Cells["ChiPhiSuaChua"].Value = 0; // Chi phí sửa chữa giữ nguyên

                    MessageBox.Show("Đã cập nhật thông tin thiết bị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn dòng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //xoá
        private void Btn_Xoa_TTB_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có dòng nào được chọn không
                if (dataGV_DS_YCSC.SelectedRows.Count > 0)
                {
                    // Xóa dòng đang chọn
                    int selectedRowIndex = dataGV_DS_YCSC.SelectedRows[0].Index;
                    dataGV_DS_YCSC.Rows.RemoveAt(selectedRowIndex);

                    MessageBox.Show("Đã xóa thiết bị khỏi danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //thêm 
        private void Btn_TTB_YCSC_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ các TextBox, ComboBox
                string maPhong = text_maphong.Text;
                string maTTB = txt_MaTTB.Text;
                string tenTTB = cbo_Ten_TTB.SelectedItem != null ? cbo_Ten_TTB.SelectedItem.ToString() : string.Empty;
                int soLuong = int.Parse(txt_soluong_sc.Text);
                string tinhTrang = txt_tinhtrang_sc.Text;
                decimal chiPhiSuaChua = 0; // Chi phí sửa chữa mặc định là 0

                // Kiểm tra dữ liệu nhập vào có hợp lệ không
                if (string.IsNullOrEmpty(maPhong) || string.IsNullOrEmpty(maTTB) || string.IsNullOrEmpty(tenTTB) || soLuong <= 0)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin và đảm bảo số lượng lớn hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra số lượng trang thiết bị có vượt quá số lượng trong cơ sở dữ liệu không
                BLL_YeuCauSuaChua bll = new BLL_YeuCauSuaChua();
                int soLuongHienCo = bll.GetSoLuongTrangThietBi(maPhong, maTTB);

                if (soLuong > soLuongHienCo)
                {
                    MessageBox.Show($"Số lượng trang thiết bị nhập vào ({soLuong}) vượt quá số lượng hiện có trong phòng ({soLuongHienCo}).",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Thêm dữ liệu vào DataGridView
                int stt = dataGV_DS_YCSC.Rows.Count + 1; // Tính số thứ tự (STT)
                dataGV_DS_YCSC.Rows.Add(stt, maPhong, maTTB, tenTTB, soLuong, tinhTrang, chiPhiSuaChua);

                // Thông báo thành công
                MessageBox.Show("Đã thêm thiết bị vào danh sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Xóa dữ liệu cũ trên giao diện sau khi thêm
                txt_MaTTB.Clear();
                txt_soluong_sc.Clear();
                txt_tinhtrang_sc.Clear();
                cbo_Ten_TTB.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Btn_LapPhieuYCSC_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ giao diện
               // string maNhanVien = "NV001"; // Có thể lấy từ giao diện nếu cần
                int MaTK = DangNhap.MaTK;
                BLL_NhanVien nv = new BLL_NhanVien();
                // Lấy thông tin nhân viên bằng mã tài khoản
                NhanVien nhanVien = nv.GetNhanVienDetailsByMaTaiKhoan(MaTK);
                // Tạo đối tượng yêu cầu sửa chữa và gán các giá trị

                DateTime ngayLap = data_time_NgayLap.Value;
                DateTime ngayHoanThanh = data_time_NgayHT.Value;

                // Tạo đối tượng YeuCauSuaChua
                YeuCauSuaChua ycSuaChua = new YeuCauSuaChua
                {
                    MaNhanVien = nhanVien.MaNhanVien,
                    TrangThai = "Chuẩn Bị Thực Hiện", // Trạng thái mặc định
                    TongTien = 0, // Tổng tiền mặc định là 0
                    NgayLap = ngayLap,
                    NgayHoanTat = ngayHoanThanh
                };

                // Lấy danh sách từ DataGridView
                List<CT_YeuCauSuaChua> ctYeuCauSuaChuas = new List<CT_YeuCauSuaChua>();
                foreach (DataGridViewRow row in dataGV_DS_YCSC.Rows)
                {
                    if (row.IsNewRow) continue; // Bỏ qua dòng trống cuối cùng trong DataGridView

                    // Tạo đối tượng CT_YeuCauSuaChua từ từng dòng
                    CT_YeuCauSuaChua ctYeuCauSuaChua = new CT_YeuCauSuaChua
                    {
                        MaPhong = row.Cells["MaPhong"].Value?.ToString(),
                        MaThietBi = row.Cells["MaThietBi"].Value?.ToString(),
                        SoLuong = int.TryParse(row.Cells["SoLuong"].Value?.ToString(), out int soLuong) ? soLuong : 0,
                        TinhTrang = row.Cells["TinhTrang"].Value?.ToString(),
                        PhiSuaChua = 0 // Phí sửa chữa mặc định là 0
                    };

                    // Thêm vào danh sách
                    ctYeuCauSuaChuas.Add(ctYeuCauSuaChua);
                }

                // Kiểm tra nếu không có dữ liệu
                if (ctYeuCauSuaChuas.Count == 0)
                {
                    MessageBox.Show("Danh sách thiết bị sửa chữa trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Gọi BLL để thêm dữ liệu
                BLL_YeuCauSuaChua bll_ycsc = new BLL_YeuCauSuaChua();
                bool isAdded = bll_ycsc.AddYeuCauSuaChuaAndDetails(ycSuaChua, ctYeuCauSuaChuas);

                // Kiểm tra kết quả
                if (isAdded)
                {
                    MessageBox.Show("Lập phiếu yêu cầu sửa chữa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGV_DS_YCSC.Rows.Clear(); // Xóa dữ liệu sau khi lưu thành công
                }
                else
                {
                    MessageBox.Show("Lập phiếu yêu cầu sửa chữa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cbo_Ten_TTB_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu có mục nào được chọn
            if (cbo_Ten_TTB.SelectedIndex != -1)
            {
                // Lấy tên trang thiết bị từ ComboBox
                string tenThietBi = cbo_Ten_TTB.SelectedItem.ToString();

                // Gọi phương thức BLL để lấy mã thiết bị
                string maThietBi = bll_ttb.GetMaThietBiByTen(tenThietBi);

                // Hiển thị mã thiết bị trong TextBox hoặc xử lý khác nếu cần
                if (!string.IsNullOrEmpty(maThietBi))
                {
                    txt_MaTTB.Text = maThietBi; // Hiển thị mã thiết bị trong TextBox
                }
                else
                {
                    txt_MaTTB.Text = "Không có mã thiết bị!"; // Nếu không tìm thấy
                }
            }
        }

    

       

        //danh sách đề xuất sửa chữa theo định kỳ
        private void Data_GV_DeXuatSuaChua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Kiểm tra xem người dùng có chọn một hàng hợp lệ hay không
                if (e.RowIndex >= 0)
                {
                    // Lấy giá trị của các cột từ dòng được chọn
                    DataGridViewRow row = Data_GV_DeXuatSuaChua.Rows[e.RowIndex];

                    // Hiển thị các giá trị tương ứng lên các TextBox
                    txt_MaTB.Text = row.Cells["MaThietBi"].Value.ToString();
                    txt_TenThietBi.Text = row.Cells["TenThietBi"].Value.ToString();
                    txt_maphong.Text = row.Cells["MaPhong"].Value.ToString();
                    txt_soluong.Text = row.Cells["SoLuong"].Value.ToString();
                    txt_trangthai.Text = row.Cells["TinhTrang"].Value.ToString();
                    txt_chiphisua.Text = row.Cells["ChiPhiSuaChua"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin: " + ex.Message);
            }
        }

        // xuất yêu cầu sửa chữa đã hoàn thành xong theo định kỳ
        private void Btn_Xuat_YCSC_Click(object sender, EventArgs e)
        {
            // Đường dẫn tới mẫu Word
            string templatePath = @"D:\DACN\PhieuYeuCauSuaChua\PhieuYeuCauSuaChua.docx";
            string outputPath = @"D:\DACN\PhieuYeuCauSuaChua\PhieuYeuCauSuaChua_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".docx";

            // Kiểm tra xem file mẫu có tồn tại không
            if (!File.Exists(templatePath))
            {
                MessageBox.Show("Không tìm thấy file mẫu tại: " + templatePath, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo ứng dụng Word và mở file mẫu
            Microsoft.Office.Interop.Word._Application wordApp = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(templatePath);

            int MaTK = DangNhap.MaTK;
            BLL_NhanVien nv = new BLL_NhanVien();
            // Lấy thông tin nhân viên bằng mã tài khoản
            NhanVien nhanVien = nv.GetNhanVienDetailsByMaTaiKhoan(MaTK);
            try
            {
                // Lấy giá trị từ giao diện người dùng
                string HoVaTenNhanVien = nhanVien.HoTen;
                string ChucVu = nhanVien.ChucVu;
                string soQuyetDinh = "1110";
                string ngayLap = dateTime_NgayLap.Value.ToString("dd");
                string thangLap = dateTime_NgayLap.Value.ToString("MM");
                string namLap = dateTime_NgayLap.Value.ToString("yyyy");

                // Tính tổng chi phí sửa chữa
               // CalculateTotalCost();

                // Thay thế các placeholder trong tài liệu
                ReplacePlaceholder(doc, "«SoQuyetDinh»", soQuyetDinh);
                ReplacePlaceholder(doc, "«Ngay»", ngayLap);
                ReplacePlaceholder(doc, "«Thang»", thangLap);
                ReplacePlaceholder(doc, "«Nam»", namLap);
                ReplacePlaceholder(doc, "«HoVaTenNhanVien»", HoVaTenNhanVien);
                ReplacePlaceholder(doc, "«ChucVu»", ChucVu);
                ReplacePlaceholder(doc, "«TongTien»", txt_TongChiPhiSuaChua.Text); // Thay thế tổng tiền

                // Tìm bảng trong Word và thay thế dữ liệu
                Microsoft.Office.Interop.Word.Range searchRange = doc.Content;
                searchRange.Find.ClearFormatting();
                // Thay đổi nội dung tìm kiếm để sử dụng giá trị Chức Vụ
                searchRange.Find.Text = "Bộ Phận : " + ChucVu;

                if (searchRange.Find.Execute())
                {
                    // Di chuyển vị trí đến sau văn bản tìm được
                    Microsoft.Office.Interop.Word.Range tableRange = doc.Range(searchRange.End, doc.Content.End);
                    Microsoft.Office.Interop.Word.Table targetTable = null;

                    // Xác định bảng đầu tiên sau vị trí đã tìm
                    foreach (Microsoft.Office.Interop.Word.Table tbl in doc.Tables)
                    {
                        if (tbl.Range.Start >= tableRange.Start)
                        {
                            targetTable = tbl;
                            break;
                        }
                    }

                    if (targetTable == null)
                    {
                        MessageBox.Show("Không tìm thấy bảng phù hợp trong tài liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Xóa hết các hàng trong bảng (trừ hàng tiêu đề nếu có)
                    while (targetTable.Rows.Count > 1)
                    {
                        targetTable.Rows[2].Delete();  // Xóa các hàng sau tiêu đề
                    }

                    // Lặp qua các hàng trong DataGridView và thêm vào bảng Word
                    for (int i = 0; i < Data_GV_DeXuatSuaChua.Rows.Count; i++)
                    {
                        string stt = (i + 1).ToString();
                        string maPhong = Data_GV_DeXuatSuaChua.Rows[i].Cells["MaPhong"].Value?.ToString() ?? "";
                        string maThietBi = Data_GV_DeXuatSuaChua.Rows[i].Cells["MaThietBi"].Value?.ToString() ?? "";
                        string tenThietBi = Data_GV_DeXuatSuaChua.Rows[i].Cells["TenThietBi"].Value?.ToString() ?? "";
                        string soLuong = Data_GV_DeXuatSuaChua.Rows[i].Cells["SoLuong"].Value?.ToString() ?? "";
                        string trangThai = Data_GV_DeXuatSuaChua.Rows[i].Cells["TinhTrang"].Value?.ToString() ?? "";
                        string chiPhi = Data_GV_DeXuatSuaChua.Rows[i].Cells["ChiPhiSuaChua"].Value?.ToString() ?? "";

                        // Thêm một dòng mới vào bảng
                        Microsoft.Office.Interop.Word.Row newRow = targetTable.Rows.Add();

                        // Gán trực tiếp giá trị vào các ô của dòng mới
                        newRow.Cells[1].Range.Text = stt;
                        newRow.Cells[2].Range.Text = maPhong;
                        newRow.Cells[3].Range.Text = maThietBi;
                        newRow.Cells[4].Range.Text = tenThietBi;
                        newRow.Cells[5].Range.Text = soLuong;
                        newRow.Cells[6].Range.Text = trangThai;
                        newRow.Cells[7].Range.Text = chiPhi;
                    }

                    // Lưu tài liệu với tên file mới
                    doc.SaveAs2(outputPath);
                    MessageBox.Show("Phiếu yêu cầu sửa chữa đã được lưu tại: " + outputPath, "Thông báo");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy văn bản 'Bộ Phận : : Nhân Viên Quản Lý' trong tài liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tạo phiếu yêu cầu sửa chữa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Đóng tài liệu và ứng dụng Word
                doc.Close();
                wordApp.Quit();

                // Giải phóng tài nguyên COM
                System.Runtime.InteropServices.Marshal.ReleaseComObject(doc);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
            }
        }





        // Hàm ReplacePlaceholder để thay thế các trường trong tài liệu Word
        private void ReplacePlaceholder(Microsoft.Office.Interop.Word.Document doc, string placeholder, string value)
        {
            Microsoft.Office.Interop.Word.Find findObject = doc.Content.Find;
            findObject.ClearFormatting();
            findObject.Text = placeholder;
            findObject.Replacement.ClearFormatting();
            findObject.Replacement.Text = value;
            findObject.Execute(Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
        }

        // Hàm thay thế placeholder trong từng ô của một dòng trong bảng
        private void ReplacePlaceholderInRow(Microsoft.Office.Interop.Word.Row row, string placeholder, string value)
        {
            foreach (Microsoft.Office.Interop.Word.Cell cell in row.Cells)
            {
                if (cell.Range.Text.Contains(placeholder))
                {
                    cell.Range.Text = cell.Range.Text.Replace(placeholder, value);
                }
            }
        }


        private void Btn_ThemYCSC_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem người dùng có nhập tổng chi phí sửa chữa hay không
                if (string.IsNullOrWhiteSpace(txt_TongChiPhiSuaChua.Text))
                {
                    MessageBox.Show("Vui lòng nhập tổng chi phí sửa chữa.");
                    return;
                }

                // Kiểm tra xem tổng chi phí sửa chữa có phải là một số hợp lệ hay không
                int tongTien;
                if (!int.TryParse(txt_TongChiPhiSuaChua.Text, out tongTien))
                {
                    MessageBox.Show("Tổng chi phí sửa chữa phải là một số hợp lệ.");
                    return;
                }

                int MaTK = DangNhap.MaTK;
                BLL_NhanVien nv = new BLL_NhanVien();
                // Lấy thông tin nhân viên bằng mã tài khoản
                NhanVien nhanVien = nv.GetNhanVienDetailsByMaTaiKhoan(MaTK);
                // Tạo đối tượng yêu cầu sửa chữa và gán các giá trị
                YeuCauSuaChua ycSuaChua = new YeuCauSuaChua
                {
                    NgayLap = dateTime_NgayLap.Value,
                    NgayHoanTat = dateTime_NgayDK_HoanTat.Value,
                    TrangThai = "Hoàn Tất", // Trạng thái yêu cầu sửa chữa
                    MaNhanVien = nhanVien.MaNhanVien,  // Giả sử mã nhân viên là NV002
                    TongTien = tongTien   // Gán tổng chi phí sửa chữa vào yêu cầu
                };

                // Danh sách chi tiết yêu cầu sửa chữa
                List<CT_YeuCauSuaChua> ctYeuCauSuaChuas = new List<CT_YeuCauSuaChua>();

                // Lặp qua tất cả các dòng trong DataGridView để lấy dữ liệu chi tiết yêu cầu sửa chữa
                foreach (DataGridViewRow row in Data_GV_DeXuatSuaChua.Rows)
                {
                    // Kiểm tra nếu cột "YeuCauSuaChua" có giá trị là "2"
                    if (row.Cells["YeuCauSuaChua"].Value != null && row.Cells["YeuCauSuaChua"].Value.ToString() == "2")
                    {
                        // Kiểm tra và lấy các giá trị cho mỗi chi tiết yêu cầu sửa chữa
                        int soLuong, phiSuaChua;

                        // Kiểm tra số lượng
                        if (!int.TryParse(row.Cells["SoLuong"].Value.ToString(), out soLuong))
                        {
                            MessageBox.Show($"Số lượng không hợp lệ ở dòng {row.Index + 1}.");
                            return;
                        }

                        // Kiểm tra chi phí sửa chữa
                        if (!int.TryParse(row.Cells["ChiPhiSuaChua"].Value.ToString(), out phiSuaChua))
                        {
                            MessageBox.Show($"Chi phí sửa chữa không hợp lệ ở dòng {row.Index + 1}.");
                            return;
                        }

                        // Tạo chi tiết yêu cầu sửa chữa và thêm vào danh sách
                        CT_YeuCauSuaChua ctYeuCauSuaChua = new CT_YeuCauSuaChua
                        {
                            MaYCSuaChua = ycSuaChua.MaYCSuaChua,
                            MaPhong = row.Cells["MaPhong"].Value.ToString(),
                            MaThietBi = row.Cells["MaThietBi"].Value.ToString(),
                            SoLuong = soLuong,
                            TinhTrang = row.Cells["TinhTrang"].Value.ToString(),
                            PhiSuaChua = phiSuaChua
                        };

                        ctYeuCauSuaChuas.Add(ctYeuCauSuaChua);  // Thêm vào danh sách chi tiết
                    }
                }

                // Kiểm tra nếu không có chi tiết yêu cầu sửa chữa nào được thêm
                if (ctYeuCauSuaChuas.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một yêu cầu sửa chữa.");
                    return;
                }

                // Gọi phương thức để thêm yêu cầu sửa chữa và chi tiết yêu cầu sửa chữa vào cơ sở dữ liệu
                bool isSuccess = bll_ycsc.AddYeuCauSuaChuaAndDetails(ycSuaChua, ctYeuCauSuaChuas);
                if (!isSuccess)
                {
                    MessageBox.Show("Lỗi khi thêm yêu cầu sửa chữa.");
                    return;
                }

                // Thông báo thành công
                MessageBox.Show("Đã thêm yêu cầu sửa chữa thành công.");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và hiển thị thông báo chi tiết
                MessageBox.Show($"Lỗi: {ex.Message}\n{ex.StackTrace}");
            }
        }





        public static void ExportDanhSachSuaChua(List<PhieuKTTrangThietBi> danhSachSuaChua)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Phiếu Kiểm Tra");

                // Tiêu đề các cột
                worksheet.Cells[1, 1].Value = "STT";
                worksheet.Cells[1, 2].Value = "Mã Phòng";
                worksheet.Cells[1, 3].Value = "Mã Thiết Bị";
                worksheet.Cells[1, 4].Value = "Tên Thiết Bị";
                worksheet.Cells[1, 5].Value = "Số Lượng";
                worksheet.Cells[1, 6].Value = "Trạng Thái";
                worksheet.Cells[1, 7].Value = "Yêu Cầu Sửa Chữa";

                int row = 2;
                int stt = 1;

                foreach (var item in danhSachSuaChua)
                {
                    worksheet.Cells[row, 1].Value = stt++;
                    worksheet.Cells[row, 2].Value = item.MaPhong;
                    worksheet.Cells[row, 3].Value = item.MaThietBi;
                    worksheet.Cells[row, 4].Value = item.TenThietBi;
                    worksheet.Cells[row, 5].Value = item.SoLuong;
                    worksheet.Cells[row, 6].Value = item.TrangThai;
                    worksheet.Cells[row, 7].Value = item.YeuCauSuaChua;

                    row++;
                }

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    FileName = "PhieuKiemTraTrangThietBi.xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var filePath = saveFileDialog.FileName;
                    var fileInfo = new FileInfo(filePath);
                    package.SaveAs(fileInfo);
                    MessageBox.Show("Đã xuất Excel thành công!");
                }
            }
        }

        private void Btn_themdexuat_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Chọn file Excel để nhập"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                    using (var package = new ExcelPackage(fileInfo))
                    {
                        // Lấy worksheet đầu tiên trong file
                        var worksheet = package.Workbook.Worksheets[0];

                        // Kiểm tra số lượng cột trong worksheet và DataGridView
                        var rowCount = worksheet.Dimension.Rows;
                        var colCount = worksheet.Dimension.Columns;

                        // Kiểm tra xem số cột có hợp lệ không
                        if (colCount != Data_GV_DeXuatSuaChua.Columns.Count)
                        {
                            MessageBox.Show("Số lượng cột trong file Excel không khớp với số cột trong DataGridView.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Xóa hết dữ liệu cũ trong DataGridView
                        Data_GV_DeXuatSuaChua.Rows.Clear();

                        // Biến để tính tổng chi phí sửa chữa
                        double totalCost = 0;

                        // Đọc dữ liệu từ worksheet và thêm vào DataGridView
                        for (int row = 13; row <= rowCount; row++) // Bắt đầu từ dòng 13 như bạn yêu cầu
                        {
                            // Lấy giá trị của cột "Yêu Cầu Sửa Chữa" (giả sử cột này là cột 7)
                            string yeuCauSuaChuaValue = worksheet.Cells[row, 7].Text.Trim();

                            // Chỉ thêm hàng nếu giá trị của "Yêu Cầu Sửa Chữa" là "2"
                            if (yeuCauSuaChuaValue == "2")
                            {
                                // Tạo dòng mới cho DataGridView
                                var dgvRow = new DataGridViewRow();

                                // Lặp qua tất cả các cột và thêm giá trị vào mỗi cell
                                for (int col = 1; col <= colCount; col++)
                                {
                                    string cellValue = worksheet.Cells[row, col].Text.Trim(); // Lấy dữ liệu của ô, bỏ khoảng trắng
                                    dgvRow.Cells.Add(new DataGridViewTextBoxCell { Value = cellValue });
                                }

                                // Thêm dòng vào DataGridView
                                Data_GV_DeXuatSuaChua.Rows.Add(dgvRow);

                                // Lấy giá trị chi phí sửa chữa từ cột "Chi phí sửa chữa" (giả sử cột này là cột 6)
                                if (double.TryParse(worksheet.Cells[row, 8].Text.Trim(), out double cost))
                                {
                                    totalCost += cost; // Cộng dồn vào tổng chi phí
                                }
                            }
                        }

                        // Cập nhật tổng chi phí sửa chữa vào TextBox
                        txt_TongChiPhiSuaChua.Text = totalCost.ToString();

                    }

                    MessageBox.Show("Dữ liệu đã được nhập thành công từ file Excel!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi nhập dữ liệu từ Excel: " + ex.Message);
                }
            }
        }

        //danh sách yêu cầu sửa thiết bị từ sinh viên
        private void SetupDataGridView_SinhVien()
        {
            // Đảm bảo cột trong DataGridView đúng với dữ liệu
            dataGV_DS_YCSC.Columns.Clear();  // Xóa các cột cũ nếu có

            // Thêm cột vào DataGridView
            dataGV_DS_YCSC.Columns.Add("STT", "STT");
            dataGV_DS_YCSC.Columns.Add("MaPhong", "Mã Phòng");
            dataGV_DS_YCSC.Columns.Add("MaThietBi", "Mã Thiết Bị");
            dataGV_DS_YCSC.Columns.Add("TenThietBi", "Tên Thiết Bị");
            dataGV_DS_YCSC.Columns.Add("SoLuong", "Số Lượng");
            dataGV_DS_YCSC.Columns.Add("TinhTrang", "Trạng Thái");
            dataGV_DS_YCSC.Columns.Add("ChiPhiSuaChua", "Chi Phi Sửa Chữa");

        }


        private void SetupDataGridView()
        {
            // Đảm bảo cột trong DataGridView đúng với dữ liệu
            Data_GV_DeXuatSuaChua.Columns.Clear();  // Xóa các cột cũ nếu có

            // Thêm cột vào DataGridView
            Data_GV_DeXuatSuaChua.Columns.Add("STT", "STT");
            Data_GV_DeXuatSuaChua.Columns.Add("MaPhong", "Mã Phòng");
            Data_GV_DeXuatSuaChua.Columns.Add("MaThietBi", "Mã Thiết Bị");
            Data_GV_DeXuatSuaChua.Columns.Add("TenThietBi", "Tên Thiết Bị");
            Data_GV_DeXuatSuaChua.Columns.Add("SoLuong", "Số Lượng");
            Data_GV_DeXuatSuaChua.Columns.Add("TinhTrang", "Trạng Thái");
            Data_GV_DeXuatSuaChua.Columns.Add("YeuCauSuaChua", "Yêu Cầu Sửa Chữa");
            Data_GV_DeXuatSuaChua.Columns.Add("ChiPhiSuaChua", "Chi Phi Sửa Chữa");
        }

        // quản lý danh sách yêu cầu sữa chữa
        // Hàm cấu hình DataGridView
        private void SetupDataGridView_QLDS_YCSC()
        {
            // Đảm bảo cột trong DataGridView đúng với dữ liệu
            dataGVQL_DS_YCSC.Columns.Clear();  // Xóa các cột cũ nếu có

            // Thêm cột vào DataGridView
            dataGVQL_DS_YCSC.Columns.Add("STT", "STT");
            dataGVQL_DS_YCSC.Columns.Add("MaYCSC", "Mã Yêu Cầu Sửa Chữa");
            dataGVQL_DS_YCSC.Columns.Add("MaNhanVien", "Mã Nhân Viên");
            dataGVQL_DS_YCSC.Columns.Add("NgayLap", "Ngày Lập");
            dataGVQL_DS_YCSC.Columns.Add("NgayHoanTat", "Ngày Hoàn Tất");
            dataGVQL_DS_YCSC.Columns.Add("TinhTrang", "Trạng Thái");
            dataGVQL_DS_YCSC.Columns.Add("TongTien", "Tổng Tiền");

            // Đặt các thuộc tính hiển thị tùy chỉnh
            dataGVQL_DS_YCSC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGVQL_DS_YCSC.AllowUserToAddRows = false;
            dataGVQL_DS_YCSC.ReadOnly = true;
        }

        private void SetupDataGV_QL_CT_YCSC(string maYCSuaChua)
        {
            // Xóa các cột cũ nếu có
            dataGV_QL_TTB.Columns.Clear();

            // Thêm cột vào DataGridView
            dataGV_QL_TTB.Columns.Add("STT", "STT");
            dataGV_QL_TTB.Columns.Add("MaPhong", "Mã Phòng");
            dataGV_QL_TTB.Columns.Add("MaThietBi", "Mã Thiết Bị");
            dataGV_QL_TTB.Columns.Add("TenThietBi", "Tên Thiết Bị");
            dataGV_QL_TTB.Columns.Add("SoLuong", "Số Lượng");
            dataGV_QL_TTB.Columns.Add("TinhTrang", "Trạng Thái");
            dataGV_QL_TTB.Columns.Add("ChiPhiSuaChua", "Chi Phí Sửa Chữa");

            int stt = 0; // Biến đếm số thứ tự
            decimal? tongTien = 0; // Biến lưu tổng chi phí sửa chữa

            // Lấy chi tiết yêu cầu sửa chữa từ DAL hoặc BLL
            var chiTietYCSC = bll_ycsc.GetChiTietYCSC(maYCSuaChua);

            if (chiTietYCSC != null)
            {
                // Điền dữ liệu vào DataGridView
                foreach (var item in chiTietYCSC)
                {
                    dataGV_QL_TTB.Rows.Add(
                        stt++,
                        item.MaPhong,
                        item.MaThietBi,
                        item.TenThietBi,
                        item.SoLuong,
                        item.TinhTrang,
                        item.PhiSuaChua
                    );

                    // Cộng dồn tổng chi phí sửa chữa
                    tongTien += item.PhiSuaChua;
                }

                // Cập nhật tổng chi phí sửa chữa vào text_QL_TongTien
                text_QL_TongTien.Text = tongTien.ToString(); // Hiển thị tổng với định dạng số
            }
            else
            {
                MessageBox.Show("Không có chi tiết yêu cầu sửa chữa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SetupDataGridView_TKSL_TTB()
        {
            dataGV_TKSL_TTB.Columns.Clear();  // Xóa các cột cũ nếu có

            // Thêm cột STT (cột này sẽ được tính toán sau khi tải dữ liệu)
            dataGV_TKSL_TTB.Columns.Add("STT", "STT");
            dataGV_TKSL_TTB.Columns["STT"].Width = 50;

           
        }


        //xuất phiêu sửa chữa định kỳ
        private void Btn_XuatPhieuKT_TTB_Click(object sender, EventArgs e)
        {
            try
            {
                // Khởi tạo ứng dụng Excel
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = false;

                // Tạo Workbook và Worksheet mới
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                worksheet.Name = "PhieuKiemTra";

                // Thiết lập tiêu đề
                worksheet.Cells[1, 4] = "BỘ CÔNG THƯƠNG";
                worksheet.Cells[2, 3] = "TRƯỜNG ĐẠI HỌC CÔNG THƯƠNG THÀNH PHỐ HỒ CHÍ MINH";
                worksheet.Cells[4, 4] = "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM";
                worksheet.Cells[5, 4] = "Độc lập - Tự do - Hạnh phúc";
                worksheet.Cells[7, 1] = $"Ngày {DateTime.Now.ToString("dd/MM/yyyy")}";
                worksheet.Cells[8, 1] = "Số: 111";

                // Căn giữa cho từng dòng
                worksheet.Cells[1, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[2, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[4, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[5, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[7, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[8, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;



                // Tiêu đề phiếu
                worksheet.Cells[10, 3] = "Phiếu Kiểm Tra Trang Thiết Bị";
                worksheet.Cells[10, 3].Font.Size = 14;  // Thay đổi kích thước chữ
                worksheet.Cells[10, 3].Font.Bold = true;  // Định dạng chữ in đậm
                worksheet.Cells[10, 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  // Căn giữa

                // Thiết lập tiêu đề các cột
                worksheet.Cells[12, 1] = "STT";
                worksheet.Cells[12, 2] = "Mã Phòng";
                worksheet.Cells[12, 3] = "Mã Thiết Bị";
                worksheet.Cells[12, 4] = "Tên Thiết Bị";
                worksheet.Cells[12, 5] = "Số Lượng";
                worksheet.Cells[12, 6] = "Trạng Thái";
                worksheet.Cells[12, 7] = "Yêu Cầu Sửa Chữa";
                worksheet.Cells[12, 8] = "Chi Phí Sửa Chữa";

                // Định dạng tiêu đề các cột
                for (int col = 1; col <= 7; col++)
                {
                    worksheet.Cells[12, col].Font.Bold = true;  // Định dạng chữ in đậm
                    worksheet.Cells[12, col].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  // Căn giữa
                    worksheet.Cells[12, col].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;  // Căn dọc giữa
                    worksheet.Cells[12, col].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray); // Màu nền cột
                }

                // Định dạng chiều rộng cột
                worksheet.Columns[1].ColumnWidth = 10;  // Cột STT
                worksheet.Columns[2].ColumnWidth = 15;  // Cột Mã Phòng
                worksheet.Columns[3].ColumnWidth = 15;  // Cột Mã Thiết Bị
                worksheet.Columns[4].ColumnWidth = 25;  // Cột Tên Thiết Bị
                worksheet.Columns[5].ColumnWidth = 15;  // Cột Số Lượng
                worksheet.Columns[6].ColumnWidth = 15;  // Cột Trạng Thái
                worksheet.Columns[7].ColumnWidth = 25;  // Cột Yêu Cầu Sửa Chữa
                worksheet.Columns[8].ColumnWidth = 25;  // Cột Chi Phí Sửa Chữa

                // Lấy danh sách thông tin trang thiết bị
                List<PhieuKTTrangThietBi> danhSach = bll_ttb.GetThongTinTrangThietBi();

                int row = 13; // Dòng bắt đầu để ghi dữ liệu
                int stt = 1;

                foreach (var item in danhSach)
                {
                    worksheet.Cells[row, 1] = stt++;
                    worksheet.Cells[row, 2] = item.MaPhong;
                    worksheet.Cells[row, 3] = item.MaThietBi;
                    worksheet.Cells[row, 4] = item.TenThietBi;
                    worksheet.Cells[row, 5] = item.SoLuong;
                    worksheet.Cells[row, 6] = "";

                    // Tạo một ComboBox cho cột "Yêu Cầu Sửa Chữa"
                    Excel.Range checkboxCell = worksheet.Cells[row, 7];
                    checkboxCell.Value = ""; // Đặt giá trị mặc định là trống

                    // Thêm ComboBox vào cột "Yêu Cầu Sửa Chữa"
                    Excel.Shape comboBox = worksheet.Shapes.AddFormControl(
                        Excel.XlFormControl.xlDropDown,  // 8 là giá trị nguyên tương đương với msoControlComboBox
                        (int)checkboxCell.Left,  // Thay đổi từ float sang double
                        (int)checkboxCell.Top,   // Thay đổi từ float sang double
                        (int)checkboxCell.Width, // Thay đổi từ float sang double
                        (int)checkboxCell.Height // Thay đổi từ float sang double
                    );


                    // Lấy đối tượng ComboBox
                    Excel.DropDown comboBoxControl = (Excel.DropDown)comboBox.OLEFormat.Object;
                    comboBoxControl.AddItem("Chưa yêu cầu sửa chữa");  // Mục 1
                    comboBoxControl.AddItem("Cần sửa chữa");  // Mục 2
                    comboBoxControl.AddItem("Đã sửa chữa");  // Mục 3

                    // Bạn cũng có thể đặt các thuộc tính khác cho ComboBox nếu cần
                    comboBoxControl.ListFillRange = "";  // Nếu muốn liên kết ComboBox với một phạm vi dữ liệu trong sheet
                    comboBoxControl.LinkedCell = worksheet.Cells[row, 7].Address;  // Liên kết ComboBox với ô để lưu giá trị lựa chọn
                    worksheet.Cells[row, 8] = "";

                    row++;
                }

                // Lưu file Excel
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Lưu phiếu kiểm tra trang thiết bị";
                saveFileDialog.FileName = "PhieuKiemTraTrangThietBi.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Xuất phiếu kiểm tra thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Đóng ứng dụng Excel
                workbook.Close(false);
                excelApp.Quit();
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(worksheet);
                Marshal.ReleaseComObject(excelApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi xuất phiếu kiểm tra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void YeuCauSuaChua_Load(object sender, EventArgs e)
        {
            // Thiết lập LicenseContext cho EPPlus
           
            SetupDataGridView();
            SetupDataGridView_SinhVien();
            SetupDataGridView_QLDS_YCSC();
            //SetupDataGV_QL_CT_YCSC();
            loadDS_YCSD();
            LoadMaYeuCauSuaChuaToComboBox();
            LoadTrangThaiYCSCToComboBox();
            LoadMaNhanVienToComboBox();
            SetupDataGridView_TKSL_TTB();


        }
    }
}
