using GUI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using DTO;
using BLL;
using Control;
using OfficeOpenXml;
using System.Windows.Forms;
using System.Linq; // Đảm bảo có namespace này
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Office.Interop.Excel;



namespace GUI
{
    public partial class QuanLyDichVu : Form
    {
        // Tạo một instance của BLL_DichVu
        BLL_DichVu bllDichVu = new BLL_DichVu();
        BLL_NhapNguyenLieu bll_nnl = new BLL_NhapNguyenLieu();
        public QuanLyDichVu()
        {
            InitializeComponent();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            btn_LoadDSMonAn.Click += Btn_LoadDSMonAn_Click;
            //  btn_DoUuThicMonAn.Click += Btn_DoUuThicMonAn_Click;
            btn_XuatThucDon.Click += Btn_XuatThucDon_Click;
            this.Load += QL_DichVu_Load;
            txt_MSSV.KeyDown += Txt_MSSV_KeyDown;
            radio_GiacUi.CheckedChanged += Radio_GiacUi_CheckedChanged;
            radio_CanTin.CheckedChanged += Radio_CanTin_CheckedChanged;

            radio_Ca2.CheckedChanged += Radio_Ca2_CheckedChanged;
            Cbo_LoaiDK_CanTin.SelectedIndexChanged += Cbo_LoaiDK_CanTin_SelectedIndexChanged;
            Cbo_LoaiDK_GiacUi.SelectedIndexChanged += Cbo_LoaiDK_GiacUi_SelectedIndexChanged;
            txt_soluongKg_Giac.TextChanged += Txt_soluongKg_Giac_TextChanged;
            txt_thanhtien_cantin.TextChanged += Txt_thanhtien_cantin_TextChanged;
            txt_thanhTien_GiacUi.TextChanged += Txt_thanhTien_GiacUi_TextChanged;
            btn_LapDangKy_DV.Click += Btn_LapDangKy_DV_Click;
            data_GV_MuaNL.CellClick += Data_GV_MuaNL_CellClick;
            Data_GV_CT_Mua.CellClick += Data_GV_CT_Mua_CellClick;
            txt_Ma_NL.TextChanged += Txt_Ma_NL_TextChanged;

            btn_LoadDS_NhapNL.Click += Btn_LoadDS_NhapNL_Click;
            btn_Xuat_DS_ThucDon.Click += Btn_Xuat_DS_ThucDon_Click;
            DataGV_DanhSachMonAn.CellClick += DataGV_DanhSachMonAn_CellClick;
            cbo_MaDonMua.SelectedIndexChanged += Cbo_MaDonMua_SelectedIndexChanged;

            data_dsdangkydv.CellClick += data_dsdangkydv_CellClick;

            btn_LuuDonNhap.Click += Btn_LuuDonNhap_Click;
            // Khởi tạo Panel nếu chưa có
            if (panel2 == null)
            {
                panel2 = new Panel();
                panel2.Dock = DockStyle.Fill;
                panel2.AutoScroll = true;
                tabPage1.Controls.Add(panel2);
            }

            // Di chuyển các điều khiển vào Panel
            List<System.Windows.Forms.Control> controlsToMove = new List<System.Windows.Forms.Control>();
            foreach (System.Windows.Forms.Control control in tabPage1.Controls)
            {
                if (control != panel2)
                {
                    controlsToMove.Add(control);
                }
            }

            foreach (System.Windows.Forms.Control control in controlsToMove)
            {
                tabPage1.Controls.Remove(control);
                panel2.Controls.Add(control);
            }

            // Kiểm tra nếu panel3 chưa được tạo
            if (panel3 == null)
            {
                // Tạo panel3 mới
                panel3 = new Panel();
                panel3.Dock = DockStyle.Fill;
                panel3.AutoScroll = true;
                tabPage5.Controls.Add(panel3); // Thêm panel3 vào tabPage5
            }

            // Di chuyển các điều khiển vào panel3
            List<System.Windows.Forms.Control> controlsToMovePanel3 = new List<System.Windows.Forms.Control>();

            // Lọc các điều khiển trong tabPage5 và thêm vào danh sách các điều khiển cần di chuyển
            foreach (System.Windows.Forms.Control control in tabPage5.Controls)
            {
                if (control != panel3)  // Bỏ qua panel3 để không di chuyển nó
                {
                    controlsToMovePanel3.Add(control);
                }
            }

            // Thực hiện di chuyển các điều khiển từ tabPage5 vào panel3
            foreach (System.Windows.Forms.Control control in controlsToMovePanel3)
            {
                tabPage5.Controls.Remove(control);  // Loại bỏ khỏi tabPage5
                panel3.Controls.Add(control);       // Thêm vào panel3
            }

            // Khởi tạo Panel cho panel4 nếu chưa có
            if (panel4 == null)
            {
                panel4 = new Panel();
                panel4.Dock = DockStyle.Fill;
                panel4.AutoScroll = true;
                tabPage3.Controls.Add(panel4);
            }

            // Di chuyển các điều khiển vào Panel panel4
            List<System.Windows.Forms.Control> controlsToMoveToPanel4 = new List<System.Windows.Forms.Control>();
            foreach (System.Windows.Forms.Control control in tabPage3.Controls)
            {
                if (control != panel4)
                {
                    controlsToMoveToPanel4.Add(control);
                }
            }

            foreach (System.Windows.Forms.Control control in controlsToMoveToPanel4)
            {
                tabPage3.Controls.Remove(control);
                panel4.Controls.Add(control);
            }


            if (panel5 == null)
            {
                panel5 = new Panel();
                panel5.Dock = DockStyle.Fill;
                panel5.AutoScroll = true;
                tabPage4.Controls.Add(panel5);
            }

            // Di chuyển các điều khiển vào Panel
            List<System.Windows.Forms.Control> controlsToMoveTabPage5 = new List<System.Windows.Forms.Control>();
            foreach (System.Windows.Forms.Control control in tabPage4.Controls)
            {
                if (control != panel5)
                {
                    controlsToMoveTabPage5.Add(control);
                }
            }

            foreach (System.Windows.Forms.Control control in controlsToMoveTabPage5)
            {
                tabPage4.Controls.Remove(control);
                panel5.Controls.Add(control);
            }

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
            LoadLoaiDangKy();

            Txt_MaNhanVien.KeyDown += Txt_MaNhanVien_KeyDown;
            cbo_SapXep_GiaMua.SelectedIndexChanged += Cbo_SapXep_GiaMua_SelectedIndexChanged;
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;

        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = tabControl1.SelectedIndex;

            // Thực hiện các hành động tùy theo chỉ số tab
            if (index == 0)
            {
                // Code khi chuyển đến tab đầu tiên
            }
            else if (index == 3)
            {
                // Code khi chuyển đến tab thứ hai
            }
            else if (index == 4)
            {
                LoadMuaNguyenLieu();
                LoadmaMua();
            }
            else
            {
                // Code khi chuyển đến các tab khác
            }
        }

        private void LoadmaMua()
        {
            try
            {
                // Get list of purchase order IDs from BLL
                List<string> maMuaList = bll_nnl.GetDanhSachMaMua();

                // Clear any existing items in the ComboBox
                cbo_MaDonMua.Items.Clear();

                // Add an empty item to the ComboBox
                cbo_MaDonMua.Items.Add(string.Empty);

                // Add each purchase order ID to the ComboBox
                foreach (string maMua in maMuaList)
                {
                    cbo_MaDonMua.Items.Add(maMua);
                }

                // (Optional) Select the first item by default
                if (cbo_MaDonMua.Items.Count > 0)
                {
                    cbo_MaDonMua.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., database connection error)
                MessageBox.Show("Error loading purchase order IDs: " + ex.Message);
            }
        }

        private void Cbo_SapXep_GiaMua_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem người dùng chọn tăng hay giảm
                string selectedOption = cbo_SapXep_GiaMua.SelectedItem.ToString();

                List<MuaNguyenLieu> danhSachMuaNguyenLieu;

                if (selectedOption == "Tăng")
                {
                    // Lọc theo tổng tiền tăng dần
                    danhSachMuaNguyenLieu = bll_nnl.GetMuaNguyenLieuByTongTienAsc();
                }
                else if (selectedOption == "Giảm")
                {
                    // Lọc theo tổng tiền giảm dần
                    danhSachMuaNguyenLieu = bll_nnl.GetMuaNguyenLieuByTongTienDesc();
                }
                else
                {
                    // Nếu không chọn gì, lấy tất cả dữ liệu
                    danhSachMuaNguyenLieu = bll_nnl.GetMuaNguyenLieu();
                }

                // Đổ dữ liệu lên DataGridView
                data_GV_MuaNL.DataSource = danhSachMuaNguyenLieu;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc dữ liệu: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Txt_MaNhanVien_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // Kiểm tra nếu người dùng nhấn phím Enter hoặc phím khác để tìm kiếm
                if (e.KeyCode == Keys.Enter)
                {
                    string maNhanVien = Txt_MaNhanVien.Text;

                    // Lọc theo mã nhân viên
                    List<MuaNguyenLieu> danhSachMuaNguyenLieu = bll_nnl.GetMuaNguyenLieuByMaNhanVien(maNhanVien);

                    // Đổ dữ liệu lên DataGridView
                    data_GV_MuaNL.DataSource = danhSachMuaNguyenLieu;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc dữ liệu: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Cbo_MaDonMua_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị mã đơn mua từ ComboBox
                string maMua = cbo_MaDonMua.SelectedItem.ToString();

                // Lọc theo mã đơn mua
                List<MuaNguyenLieu> danhSachMuaNguyenLieu = bll_nnl.GetMuaNguyenLieuMaMua(maMua);

                // Đổ dữ liệu lên DataGridView
                data_GV_MuaNL.DataSource = danhSachMuaNguyenLieu;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc dữ liệu: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DataGV_DanhSachMonAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu người dùng click vào một ô hợp lệ (không phải tiêu đề cột)
            if (e.RowIndex >= 0)
            {
                // Lấy thông tin dòng được chọn
                DataGridViewRow row = DataGV_DanhSachMonAn.Rows[e.RowIndex];

                // Lấy các giá trị từ các cột trong dòng đã chọn
                string tenMonAn = row.Cells["TenMonAn"].Value.ToString();
                string chiphi = row.Cells["Chiphi"].Value.ToString();
                string calo = row.Cells["Calo"].Value.ToString();
                string protein = row.Cells["Protein"].Value.ToString();
                string carb = row.Cells["Carb"].Value.ToString();
                string fat = row.Cells["Fat"].Value.ToString();

                // Gán các giá trị này vào các TextBox
                txt_TenMonAn.Text = tenMonAn;
                txt_Calo.Text = calo;
                txt_Protein.Text = protein;
                txt_Carb.Text = carb;
                txt_Fat.Text = fat;
            }
        }


        private void Btn_Xuat_DS_ThucDon_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy ngày bắt đầu và kết thúc
                DateTime ngayBD = dateTimeNgayBD.Value.Date;
                DateTime ngayKT = dateTimeNgayKT.Value.Date;

                if (ngayBD > ngayKT)
                {
                    MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Chuẩn bị dữ liệu từ DataGridView
                List<MonAn> danhSachMonAn = new List<MonAn>();
                foreach (DataGridViewRow row in DataGV_ThucDon.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        MonAn monAn = new MonAn
                        {
                            Ten = row.Cells["NgayThu"].Value?.ToString() ?? "",
                            Sang = row.Cells["Sang"].Value?.ToString() ?? "",
                            Trua = row.Cells["Trua"].Value?.ToString() ?? "",
                            Chieu = row.Cells["Chieu"].Value?.ToString() ?? ""
                        };
                        danhSachMonAn.Add(monAn);
                    }
                }

                // Kiểm tra nếu danh sách món ăn rỗng
                if (danhSachMonAn.Count == 0)
                {
                    MessageBox.Show("Danh sách thực đơn rỗng. Vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo đường dẫn lưu file PDF
                string directoryPath = @"D:\DACN\ThucDonTheoTuan";
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string filePath = Path.Combine(directoryPath, $"ThucDon_{ngayBD:yyyyMMdd}_{ngayKT:yyyyMMdd}.pdf");

                // Lấy thông tin người lập phiếu
                string nguoiLapPhieu = "Lê Nhật Quyên";

                // Gọi phương thức tạo báo cáo PDF
                PdfReport report = new PdfReport();
                report.ThuDonTheoTuan(filePath, nguoiLapPhieu, danhSachMonAn, ngayBD, ngayKT);

                // Hiển thị thông báo khi báo cáo được tạo thành công
                MessageBox.Show("Xuất file PDF thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Mở file sau khi xuất
                if (File.Exists(filePath))
                {
                    System.Diagnostics.Process.Start(filePath);
                }
                else
                {
                    MessageBox.Show($"Không thể tìm thấy file PDF tại: {filePath}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void Btn_LuuDonNhap_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra và lấy dữ liệu hợp lệ
                int tongSoLuong;
                decimal tongTienNhap;

                if (!int.TryParse(txt_SoLuong.Text.Trim(), out tongSoLuong))
                {
                    MessageBox.Show("Vui lòng nhập số lượng hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txt_TongTienNhap.Text.Trim(), out tongTienNhap))
                {
                    MessageBox.Show("Vui lòng nhập tổng tiền nhập hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int MaTK = DangNhap.MaTK;
                BLL_NhanVien nv = new BLL_NhanVien();
                // Lấy thông tin nhân viên bằng mã tài khoản
                NhanVien nhanVien = nv.GetNhanVienDetailsByMaTaiKhoan(MaTK);
                // Tạo đối tượng yêu cầu sửa chữa và gán các giá trị
                var ngayLap = datatime_NgayLap.Value;

                // Tạo đối tượng MuaNguyenLieu
                var muaNguyenLieu = new MuaNguyenLieu
                {
                    MaNhanVien = nhanVien.MaNhanVien,
                    NgayMua = ngayLap,
                    SoLuong = tongSoLuong,
                    GiaMua = tongTienNhap
                };

                // Tạo danh sách ChiTietMua
                var chiTietMuaList = new List<ChiTietMua>();
                foreach (DataGridViewRow row in Data_GV_CT_Mua.Rows)
                {
                    if (row.Cells["MaNguyenLieu"].Value != null && row.Cells["SoLuong"].Value != null && row.Cells["GiaThanh"].Value != null)
                    {
                        int soLuong;
                        decimal giaThanh;

                        if (!int.TryParse(row.Cells["SoLuong"].Value.ToString(), out soLuong))
                        {
                            MessageBox.Show($"Dòng {row.Index + 1}: Số lượng không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (!decimal.TryParse(row.Cells["GiaThanh"].Value.ToString(), out giaThanh))
                        {
                            MessageBox.Show($"Dòng {row.Index + 1}: Giá thành không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        chiTietMuaList.Add(new ChiTietMua
                        {
                            MaNguyenLieu = row.Cells["MaNguyenLieu"].Value.ToString(),
                            SoLuong = soLuong,
                            GiaThanh = giaThanh
                        });
                    }
                }

                // Gọi BLL
                var bll = new BLL_NhapNguyenLieu();
                bool isSuccess = bll.ThemDonNhapNguyenLieu(muaNguyenLieu, chiTietMuaList);

                if (isSuccess)
                {
                    MessageBox.Show("Lưu đơn nhập nguyên liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Lưu đơn nhập nguyên liệu thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void Btn_LoadDS_NhapNL_Click(object sender, EventArgs e)
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

                        // Xác định số lượng hàng và cột
                        var rowCount = worksheet.Dimension.End.Row; // Dòng cuối cùng có dữ liệu
                        var colCount = worksheet.Dimension.End.Column; // Cột cuối cùng có dữ liệu

                        // Hiển thị số lượng cột trong MessageBox
                        MessageBox.Show("Số lượng cột: " + colCount.ToString());
                        MessageBox.Show("Số lượng hàng: " + rowCount.ToString());

                        // Kiểm tra số lượng cột có hợp lệ với DataGridView
                        if (colCount != 4) // File Excel cần có 4 cột (MaNguyenLieu, TenNguyenLieu, SoLuong, GiaThanh)
                        {
                            MessageBox.Show("Số lượng cột trong file Excel không khớp với số cột trong DataGridView.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Đảm bảo DataGridView có số cột phù hợp trước khi thêm dữ liệu
                        Data_GV_CT_Mua.Columns.Clear(); // Xóa các cột hiện tại
                        Data_GV_CT_Mua.Columns.Add("MaNguyenLieu", "Mã Nguyên Liệu");
                        Data_GV_CT_Mua.Columns.Add("TenNguyenLieu", "Tên Nguyên Liệu");
                        Data_GV_CT_Mua.Columns.Add("SoLuong", "Số Lượng");
                        Data_GV_CT_Mua.Columns.Add("GiaThanh", "Giá Thành");

                        // Xóa dữ liệu cũ trong DataGridView
                        Data_GV_CT_Mua.Rows.Clear();
                        // Biến lưu trữ tổng tiền và tổng số lượng
                        decimal tongTien = 0;
                        int tongSoLuong = 0;

                        // Đọc dữ liệu từ worksheet và thêm vào DataGridView
                        for (int row = 5; row <= rowCount; row++) // Giả sử dữ liệu bắt đầu từ dòng 2 (dòng 1 là tiêu đề)
                        {
                            var dgvRow = new DataGridViewRow();
                            bool isRowEmpty = true; // Kiểm tra nếu dòng này rỗng

                            decimal giaThanh = 0;
                            int soLuong = 0;

                            for (int col = 1; col <= 4; col++) // Duyệt các cột (4 cột)
                            {
                                var cellValue = worksheet.Cells[row, col].Value?.ToString().Trim(); // Lấy giá trị ô, tránh null
                                if (!string.IsNullOrEmpty(cellValue))
                                {
                                    isRowEmpty = false;
                                }

                                // Lấy dữ liệu từ từng ô và thêm vào dòng của DataGridView
                                dgvRow.Cells.Add(new DataGridViewTextBoxCell { Value = cellValue });

                                // Nếu là cột "Số Lượng" hoặc "Giá Thành", chuyển đổi và cộng dồn
                                if (col == 4 && decimal.TryParse(cellValue, out giaThanh)) // Cột "GiaThanh"
                                {
                                    tongTien += giaThanh;
                                }
                                if (col == 3 && int.TryParse(cellValue, out soLuong)) // Cột "SoLuong"
                                {
                                    tongSoLuong += soLuong;
                                }
                            }

                            // Nếu dòng không rỗng, thêm vào DataGridView
                            if (!isRowEmpty)
                            {
                                Data_GV_CT_Mua.Rows.Add(dgvRow);
                            }
                        }
                        // Cập nhật các ô txt_TongTienNhap và txt_SoLuong
                        txt_TongTienNhap.Text = tongTien.ToString("C"); // Hiển thị tổng tiền dưới dạng tiền tệ
                        txt_SoLuong.Text = tongSoLuong.ToString();

                        MessageBox.Show("Dữ liệu đã được nhập thành công từ file Excel!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi nhập dữ liệu từ Excel: " + ex.Message);
                }
            }
        }

        private void Txt_Ma_NL_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu mã nguyên liệu không rỗng
                if (!string.IsNullOrWhiteSpace(txt_Ma_NL.Text))
                {
                    // Lấy mã nguyên liệu từ TextBox
                    string maNguyenLieu = txt_Ma_NL.Text.Trim();

                    // Gọi hàm BLL để lấy tên nguyên liệu
                    string tenNguyenLieu = bll_nnl.LayTenNguyenLieu(maNguyenLieu);

                    // Cập nhật giá trị tên nguyên liệu vào TextBox
                    txt_TenNguyenLieu.Text = tenNguyenLieu;
                }
                else
                {
                    // Nếu mã nguyên liệu trống, xóa tên nguyên liệu trong TextBox
                    txt_TenNguyenLieu.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy tên nguyên liệu: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Data_GV_CT_Mua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ////try
            //{
            //    // Kiểm tra nếu có dòng được chọn
            //    if (e.RowIndex >= 0)
            //    {
            //        // Lấy mã mua từ cột đầu tiên của dòng được chọn
            //        string maMua = Data_GV_CT_Mua.Rows[e.RowIndex].Cells[0].Value.ToString();

            //        // Gọi hàm BLL để lấy chi tiết đơn mua
            //        ChiTietMua  chiTietDonMua = bll_nnl.HienThiChiTietDonMua(maMua); // Giả sử BLL_NNL là lớp xử lý logic

            //        // Hiển thị thông tin lên các TextBox
            //        // Nếu có dữ liệu trả về
            //        if (chiTietDonMua != null)
            //        {


            //            // Cập nhật các TextBox với thông tin chi tiết đơn mua
            //            txt_maMua.Text = chiTietDonMua.MaMua.ToString();
            //            txt_Ma_NL.Text = chiTietDonMua.MaNguyenLieu.ToString();
            //            txt_sl_ct_dm.Text = chiTietDonMua.SoLuong.ToString();
            //            txt_dg_ct_dm.Text = chiTietDonMua.GiaThanh.ToString();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi khi hiển thị chi tiết đơn mua: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void Data_GV_MuaNL_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Kiểm tra nếu có dòng được chọn
                if (e.RowIndex >= 0)
                {
                    // Lấy mã mua từ cột đầu tiên
                    string maMua = data_GV_MuaNL.Rows[e.RowIndex].Cells[0].Value.ToString();
                    // Gọi hàm từ BLL để lấy thông tin chi tiết mua
                    List<object> chiTietMua = bll_nnl.HienThiThongTinDonMua(maMua);
                    Data_GV_CT_Mua.Columns.Clear();
                    // Đổ dữ liệu lên DataGridView chi tiết mua
                    Data_GV_CT_Mua.DataSource = chiTietMua;

                    // Gọi BLL để lấy thông tin đơn mua qua DTO

                    MuaNguyenLieu thongTinMua = bll_nnl.GetMuaNguyenLieuByMaMua(maMua);

                    // Hiển thị thông tin lên các TextBox và DateTimePicker
                    if (thongTinMua != null)
                    {
                        txt_MaNL.Text = thongTinMua.MaMua;
                        txt_SoLuong.Text = thongTinMua.SoLuong.ToString();
                        datatime_NgayLap.Value = thongTinMua.NgayMua; // Đổi ngày lập cho DateTimePicker
                        txt_TongTienNhap.Text = thongTinMua.GiaMua.ToString("N2");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin cho mã mua này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xử lý sự kiện: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadChiTietMua(string maMua)
        {
            try
            {
                // Lấy danh sách chi tiết mua từ BLL
                var danhSachChiTiet = bll_nnl.HienThiChiTietMua(maMua);

                // Đổ dữ liệu lên DataGridView
                Data_GV_CT_Mua.DataSource = danhSachChiTiet;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách chi tiết mua: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadMuaNguyenLieu()
        {
            try
            {
                // Gọi hàm từ BLL để lấy danh sách MuaNguyenLieu
                List<MuaNguyenLieu> danhSachMuaNguyenLieu = bll_nnl.GetMuaNguyenLieu();

                // Đổ dữ liệu lên DataGridView
                data_GV_MuaNL.DataSource = danhSachMuaNguyenLieu;

                // Ẩn tất cả các cột trước
                foreach (DataGridViewColumn column in data_GV_MuaNL.Columns)
                {
                    column.Visible = false;
                }

                // Hiển thị các cột cần thiết
                data_GV_MuaNL.Columns["MaMua"].Visible = true;
                data_GV_MuaNL.Columns["MaNhanVien"].Visible = true;
                data_GV_MuaNL.Columns["GiaMua"].Visible = true;
                data_GV_MuaNL.Columns["NgayMua"].Visible = true;

                // (Tùy chọn) Đặt tên header cho các cột nếu cần
                data_GV_MuaNL.Columns["MaMua"].HeaderText = "Mã Mua";
                data_GV_MuaNL.Columns["MaNhanVien"].HeaderText = "Mã Nhân Viên";
                data_GV_MuaNL.Columns["GiaMua"].HeaderText = "Giá Mua";
                data_GV_MuaNL.Columns["NgayMua"].HeaderText = "Ngày Mua";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                tabControl2.SelectedTab = tab_GiacUi;
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
                tabControl2.SelectedTab = tab_CanTin;
                LoadCanTin(); // Tải dữ liệu Căn Tin
            }
            else
            {
                txt_thanhTien_GiacUi.Text = "";
            }
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



        private void Btn_XuatThucDon_Click(object sender, EventArgs e)
        {
            // 1. Lấy danh sách món ăn từ DataGridView
            List<MonAn> danhSachMonAn = new List<MonAn>();
            foreach (DataGridViewRow row in DataGV_DanhSachMonAn.Rows)
            {
                if (row.Cells["TenMonAn"].Value != null && !string.IsNullOrEmpty(row.Cells["TenMonAn"].Value.ToString()))
                {
                    string tenMonAn = row.Cells["TenMonAn"].Value?.ToString() ?? "Chưa đặt tên";
                    double chiPhi = Convert.ToDouble(row.Cells["Chiphi"].Value);
                    double calo = Convert.ToDouble(row.Cells["Calo"].Value);
                    double protein = Convert.ToDouble(row.Cells["Protein"].Value);
                    double carb = Convert.ToDouble(row.Cells["Carb"].Value);
                    double fat = Convert.ToDouble(row.Cells["Fat"].Value);
                    string loaiMon = row.Cells["Loaimon"].Value?.ToString() ?? "Không xác định";

                    MonAn monAn = new MonAn(tenMonAn, chiPhi, calo, protein, carb, fat, loaiMon);
                    danhSachMonAn.Add(monAn);
                }
            }

            // 2. Sinh thực đơn tối ưu
            double nganSachToiDa = 1000000000; // Ví dụ: ngân sách tối đa 500,000 VND
            ThucDonHopLy thucDonHopLy = new ThucDonHopLy(danhSachMonAn, nganSachToiDa);

            // Sinh thực đơn tối ưu - Lấy danh sách thực đơn tối ưu từ phương thức SinhThucDonToiUu
            List<List<MonAn>> thucDonHopLyList = thucDonHopLy.SinhThucDonToiUu(soTheHe: 7, kichThuocQuanThe: 49);

            // Lấy thực đơn tốt nhất từ danh sách trả về
            List<MonAn> thucDonToiUu = null;
            if (thucDonHopLyList != null && thucDonHopLyList.Count > 0)
            {
                // Lựa chọn thực đơn có fitness tốt nhất (fitness thấp nhất)
                thucDonToiUu = thucDonHopLyList.OrderBy(thucDon => thucDonHopLy.CalculateFitness(thucDon)).FirstOrDefault();
            }

            if (thucDonToiUu != null && thucDonToiUu.Count > 0)
            {
                // 3. Hiển thị thực đơn lên DataGridView
                HienThiThucDon(thucDonToiUu);
            }
            else
            {
                MessageBox.Show("Không thể tạo thực đơn tối ưu!");
            }
        }


        private void HienThiThucDon(List<MonAn> thucDonToiUu)
        {
            // 3.1. Cấu hình lại DataGridView nếu cần
            SetupDataGridViewDS_ThucDon();

            // 3.2. Tạo danh sách theo ngày (7 ngày, mỗi ngày gồm các món ăn sáng, trưa, chiều)
            int soNgay = 7;
            for (int ngay = 0; ngay < soNgay; ngay++)
            {
                // Món sáng (1 món đầu tiên của ngày)
                string monSang = thucDonToiUu[ngay * 7].Ten;

                // Món trưa (3 món: Canh, Xào, Kho/Chiên)
                string monTrua = $"{thucDonToiUu[ngay * 7 + 1].Ten}, {thucDonToiUu[ngay * 7 + 2].Ten}, {thucDonToiUu[ngay * 7 + 3].Ten}";

                // Món chiều (3 món: Canh, Xào, Kho/Chiên)
                string monChieu = $"{thucDonToiUu[ngay * 7 + 4].Ten}, {thucDonToiUu[ngay * 7 + 5].Ten}, {thucDonToiUu[ngay * 7 + 6].Ten}";

                // Thêm dòng vào DataGridView
                DataGV_ThucDon.Rows.Add(
                    $"Thứ {ngay + 2}", // Hiển thị Thứ 2 đến Chủ Nhật
                    monSang,  // Món sáng
                    monTrua, // Món trưa
                    monChieu // Món chiều
                );
            }
        }




        private bool isLoaded = false;
        private void QL_DichVu_Load(object sender, EventArgs e)
        {
            if (!isLoaded)
            {
                SetupDataGridViewDS_MonAn();
                //    SetupDataGridViewDS_DoUuThichMonAn();
                SetupDataGridViewDS_ThucDon();
                LoadMuaNguyenLieu();
                LoadmaMua();
                isLoaded = true;
            }

        }

        private void Btn_LoadDSMonAn_Click(object sender, EventArgs e)
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

                        // Xác định số lượng hàng và cột
                        var rowCount = worksheet.Dimension.End.Row; // Dòng cuối cùng có dữ liệu
                        var colCount = worksheet.Dimension.End.Column; // Cột cuối cùng có dữ liệu

                        // Hiển thị số lượng cột trong MessageBox
                        MessageBox.Show("Số lượng cột: " + colCount.ToString());
                        MessageBox.Show("Số lượng hàng: " + rowCount.ToString());

                        // Kiểm tra xem số cột có hợp lệ không
                        if (colCount != DataGV_DanhSachMonAn.Columns.Count)
                        {
                            MessageBox.Show("Số lượng cột trong file Excel không khớp với số cột trong DataGridView.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Xóa dữ liệu cũ trong DataGridView
                        DataGV_DanhSachMonAn.Rows.Clear();

                        // Đọc dữ liệu từ worksheet và thêm vào DataGridView
                        for (int row = 4; row <= rowCount; row++) // Bắt đầu từ dòng thứ 8 (dòng tiêu đề có thể nằm ở trên)
                        {
                            var dgvRow = new DataGridViewRow();
                            for (int col = 1; col <= colCount; col++) // Duyệt các cột
                            {
                                // Lấy dữ liệu từ từng ô và thêm vào dòng của DataGridView
                                string cellValue = worksheet.Cells[row, col].Text.Trim();
                                dgvRow.Cells.Add(new DataGridViewTextBoxCell { Value = cellValue });
                            }

                            // Thêm dòng vào DataGridView
                            DataGV_DanhSachMonAn.Rows.Add(dgvRow);
                        }

                        MessageBox.Show("Dữ liệu đã được nhập thành công từ file Excel!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi nhập dữ liệu từ Excel: " + ex.Message);
                }
            }


        }

        private void SetupDataGridViewDS_MonAn()
        {
            // Đảm bảo xóa các cột cũ trong DataGridView nếu có
            DataGV_DanhSachMonAn.Columns.Clear();

            // Thêm cột vào DataGridView


            DataGV_DanhSachMonAn.Columns.Add("TenMonAn", "Tên Món Ăn");
            DataGV_DanhSachMonAn.Columns.Add("Chiphi", "Chi Phí");
            DataGV_DanhSachMonAn.Columns.Add("Calo", "Calo");
            DataGV_DanhSachMonAn.Columns.Add("Protein", "Protein");
            DataGV_DanhSachMonAn.Columns.Add("Carb", "Carb");
            DataGV_DanhSachMonAn.Columns.Add("Fat", "Fat");
            DataGV_DanhSachMonAn.Columns.Add("Loaimon", "Loại Món");
        }



        private void SetupDataGridViewDS_ThucDon()
        {
            // Đảm bảo xóa các cột cũ trong DataGridView nếu có
            DataGV_ThucDon.Columns.Clear();

            // Thêm cột vào DataGridView

            DataGV_ThucDon.Columns.Add("NgayThu", "Thứ");
            DataGV_ThucDon.Columns.Add("Sang", "Sáng");
            DataGV_ThucDon.Columns.Add("Trua", "Trưa");
            DataGV_ThucDon.Columns.Add("Chieu", "Chiều");

        }

        private void txt_maSinhVien_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void DataGV_ThucDon_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn một ô hợp lệ trong bảng (không phải tiêu đề cột)
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Lấy dòng dữ liệu đã chọn
                DataGridViewRow selectedRow = DataGV_ThucDon.Rows[e.RowIndex];
                // Debug: In ra các thông tin dòng đã chọn
                Console.WriteLine($"Selected Row: {e.RowIndex}");
                Console.WriteLine($"NgayThu: {selectedRow.Cells["NgayThu"].Value?.ToString()}");
                Console.WriteLine($"Sang: {selectedRow.Cells["Sang"].Value?.ToString()}");
                Console.WriteLine($"Trua: {selectedRow.Cells["Trua"].Value?.ToString()}");
                Console.WriteLine($"Chieu: {selectedRow.Cells["Chieu"].Value?.ToString()}");

                // Cập nhật các TextBox theo thông tin từ DataGridView
                // Thứ (Ngày)
                txt_thucdon_thu.Text = selectedRow.Cells["NgayThu"].Value?.ToString() ?? "Không xác định";

                // Món sáng
                txt_thucdon_sang.Text = selectedRow.Cells["Sang"].Value?.ToString() ?? "Không có món sáng";

                // Món trưa (Canh, Xào, Kho/Chiên)
                var trua = selectedRow.Cells["Trua"].Value?.ToString() ?? "Không có món trưa";
                string[] truaMon = trua.Split(','); // Giả sử món trưa là một chuỗi phân cách bằng dấu phẩy
                if (truaMon.Length == 3)
                {
                    txt_thucdon_trua_canh.Text = truaMon[0].Trim();
                    txt_thucdon_trua_xao.Text = truaMon[1].Trim();
                    txt_thucdon_trua_chien_kho.Text = truaMon[2].Trim();
                }
                else
                {
                    txt_thucdon_trua_canh.Text = "Không có món canh";
                    txt_thucdon_trua_xao.Text = "Không có món xào";
                    txt_thucdon_trua_chien_kho.Text = "Không có món kho/chiên";
                }

                // Món chiều (Canh, Xào, Kho/Chiên)
                var chieu = selectedRow.Cells["Chieu"].Value?.ToString() ?? "Không có món chiều";
                string[] chieuMon = chieu.Split(','); // Giả sử món chiều là một chuỗi phân cách bằng dấu phẩy
                if (chieuMon.Length == 3)
                {
                    txt_thucdon_chieu_canh.Text = chieuMon[0].Trim();
                    txt_thucdon_chieu_xao.Text = chieuMon[1].Trim();
                    txt_thucdon_chieu_chien_kho.Text = chieuMon[2].Trim();
                }
                else
                {
                    txt_thucdon_chieu_canh.Text = "Không có món canh";
                    txt_thucdon_chieu_xao.Text = "Không có món xào";
                    txt_thucdon_chieu_chien_kho.Text = "Không có món kho/chiên";
                }
            }
            else
            {
                MessageBox.Show("Selected invalid cell (header or out of bounds).");
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }


        //PHAN TAO LAM//////














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

        private void LoadLoaiDangKy()
        {
            try
            {
                // Giả sử bạn muốn các giá trị như "Căn Tin", "Giặt Ủi", "Căn Tin và Giặt Ủi"
                var danhSachLoaiDangKy = new List<string>
                {
                    "Căn Tin",
                    "Giặt Ủi",
                    "Cả 2"
                };

                // Gán dữ liệu vào ComboBox
                cbm_loaidangky.DataSource = danhSachLoaiDangKy;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi khi tải dữ liệu cho ComboBox Loại Đăng Ký: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btn_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem người dùng đã chọn dòng nào trong DataGridView chưa
                if (data_dsdangkydv.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một đăng ký dịch vụ để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy mã đăng ký từ dòng được chọn
                DataGridViewRow selectedRow = data_dsdangkydv.SelectedRows[0]; // Lấy dòng đầu tiên được chọn
                string maDangKy = selectedRow.Cells["MaDangKy"].Value?.ToString(); // Thay "MaDangKy" bằng tên cột chính xác

                if (string.IsNullOrEmpty(maDangKy))
                {
                    MessageBox.Show("Không thể xác định mã đăng ký dịch vụ để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Xác nhận từ người dùng
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa đăng ký dịch vụ với mã {maDangKy}?",
                                                      "Xác nhận xóa",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Thực hiện xóa qua BLL
                    bool isDeleted = bllDichVu.XoaDichVu(maDangKy);

                    if (isDeleted)
                    {
                        MessageBox.Show("Xóa đăng ký dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataDangKyDV(); // Làm mới danh sách sau khi xóa
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy đăng ký dịch vụ cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
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

        private void btn_lammoi_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDataDangKyDV();
                cbm_madangky.SelectedIndex = -1;
                cbm_manhanvien.SelectedIndex = -1;
                cbm_masinhvien.SelectedIndex = -1;
                cbm_trangthai.SelectedIndex = -1;


                txt_madv.Text = string.Empty;
                txt_masv.Text = string.Empty;
                txt_manv.Text = string.Empty;
                txt_tongtien.Text = string.Empty;
                txt_hotensv.Text = string.Empty;
                txt_maphongsv.Text = string.Empty;
                txt_loaiphong.Text = string.Empty;
                txt_hotennv.Text = string.Empty;
                txt_chucvu.Text = string.Empty;

                MessageBox.Show("Làm mới dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi làm mới: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbm_manhanvien_Click(object sender, EventArgs e)
        {
            List<NhanVien> danhSachNhanVien = bllDichVu.GetDanhSachNhanVien();
            var danhSachMaNhanVien = danhSachNhanVien.Select(nv => nv.MaNhanVien).ToList();
            cbm_manhanvien.DataSource = danhSachMaNhanVien;
        }

        private void cbm_manhanvien_SelectedIndexChanged(object sender, EventArgs e)
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

        private void cbm_masinhvien_Click(object sender, EventArgs e)
        {
            // Lấy danh sách nhân viên từ BLL
            List<SinhVien> danhSachSinhVien = bllDichVu.GetDanhSachSinhVien();

            // Lấy danh sách Mã Nhân Viên từ danh sách nhân viên
            var danhSachMaSinhVien = danhSachSinhVien.Select(nv => nv.MaSinhVien).ToList();

            // Gán vào ComboBox
            cbm_masinhvien.DataSource = danhSachMaSinhVien;
        }

        private void cbm_masinhvien_SelectedIndexChanged(object sender, EventArgs e)
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

        private void cbm_trangthai_SelectedIndexChanged(object sender, EventArgs e)
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

        private void cbm_sapxep_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy giá trị đã chọn từ ComboBox
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

        private void dataGridView_ctdangkydv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có chọn một dòng hợp lệ không
            if (e.RowIndex >= 0)
            {
                // Lấy dòng được chọn
                DataGridViewRow row = dataGridView_ctdangkydv.Rows[e.RowIndex];

                // Lấy mã dịch vụ từ dòng được chọn
                string maDichVu = row.Cells["MaDichVu"].Value?.ToString() ?? "";

                // Lấy thông tin dịch vụ từ bảng Dịch Vụ
                var dichVu = bllDichVu.GetDichVuById(maDichVu);

                if (dichVu != null)
                {
                    // Gán tên dịch vụ vào TextBox txt_tendichvu
                    txt_tendichvu.Text = dichVu.TenDichVu ?? ""; // Đảm bảo rằng TenDichVu không null
                }

                // Gán các giá trị còn lại vào các điều khiển
                txt_madichvu.Text = maDichVu; // Mã dịch vụ

                string loaiDangKy = row.Cells["LoaiDangKy"].Value?.ToString();

                if (cbm_loaidk.Items.Contains(loaiDangKy))
                {
                    cbm_loaidk.SelectedItem = loaiDangKy;
                }
                else
                {
                    cbm_loaidk.Text = loaiDangKy ?? ""; // Hiển thị giá trị ngay cả khi không có trong danh sách
                }
                dongia.Text = row.Cells["DonGia"].Value?.ToString() ?? ""; // Đơn giá
                txt_soluongg.Text = row.Cells["SoLuong"].Value?.ToString() ?? ""; // Số lượng
                txt_donvitinh.Text = row.Cells["DonViTinh"].Value?.ToString() ?? ""; // Đơn vị tính

                // Kiểm tra ngày bắt đầu và ngày kết thúc có giá trị không
                DateTime? ngayBD = row.Cells["NgayBD"].Value as DateTime?;
                DateTime? ngayKT = row.Cells["NgayKT"].Value as DateTime?;

                // Cập nhật giá trị vào DateTimePicker
                if (ngayBD.HasValue)
                {
                    dateTimePicker_ngaybd.Value = ngayBD.Value;
                }
                else
                {
                    dateTimePicker_ngaybd.Value = DateTime.Now; // Hoặc có thể gán một giá trị mặc định nếu cần
                }

                if (ngayKT.HasValue)
                {
                    dateTimePicker_ngaykt.Value = ngayKT.Value;
                }
                else
                {
                    dateTimePicker_ngaykt.Value = DateTime.Now; // Hoặc có thể gán một giá trị mặc định nếu cần
                }
            }
        }

        private void data_dsdangkydv_CellClick(object sender, DataGridViewCellEventArgs e)
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

                  //  MessageBox.Show("" + txt_madv.Text);
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



        private void button1_Click_1(object sender, EventArgs e)
        {
            HienThiThongKeDichVu();
            HienThiDoanhThuTheoLoaiDichVu();
            HienThiSoLuongTheoTrangThai();
            HienThiDangKyTheoNgay();
        }




        //THONG KE//
        private void HienThiThongKeDichVu()
        {
            try
            {
                // Lấy danh sách đăng ký dịch vụ từ BLL
                List<DangKyDichVu> danhSachDangKy = bllDichVu.GetDanhSachDangKyDichVu();

                // Thống kê số lượng từng loại dịch vụ
                var thongKeDichVu = danhSachDangKy
                    .GroupBy(dv => dv.LoaiDangKy)
                    .Select(group => new
                    {
                        LoaiDangKy = group.Key,
                        SoLuong = group.Count()
                    })
                    .ToList();

                // Cấu hình Chart
                chartThongKe.Series.Clear();
                chartThongKe.Titles.Clear();

                chartThongKe.Titles.Add("Thống kê dịch vụ");
                chartThongKe.ChartAreas[0].AxisX.Title = "Loại Dịch Vụ";
                chartThongKe.ChartAreas[0].AxisY.Title = "Số Lượng";

                // Thêm Series vào Chart
                System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series("Số lượng")
                {
                    ChartType = SeriesChartType.Column // Có thể đổi sang Pie, Line, Bar, v.v.
                };

                // Thêm dữ liệu vào Series
                foreach (var item in thongKeDichVu)
                {
                    series.Points.AddXY(item.LoaiDangKy, item.SoLuong);
                }

                chartThongKe.Series.Add(series);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi khi hiển thị thống kê: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void HienThiDoanhThuTheoLoaiDichVu()
        {
            try
            {
                // Lấy danh sách đăng ký dịch vụ từ BLL
                List<DangKyDichVu> danhSachDangKy = bllDichVu.GetDanhSachDangKyDichVu();

                // Thống kê tổng doanh thu từng loại dịch vụ
                var thongKeDoanhThu = danhSachDangKy
                    .GroupBy(dv => dv.LoaiDangKy)
                    .Select(group => new
                    {
                        LoaiDangKy = group.Key,
                        TongDoanhThu = group.Sum(dv => dv.TongTien)
                    })
                    .ToList();

                // Cấu hình Chart
                chart1.Series.Clear();
                chartThongKe.Titles.Clear();

                chart1.Titles.Add("Thống kê doanh thu theo loại dịch vụ");
                chart1.ChartAreas[0].AxisX.Title = "Loại Dịch Vụ";
                chart1.ChartAreas[0].AxisY.Title = "Doanh Thu (VND)";

                // Thêm Series vào Chart
                System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series("Doanh Thu")
                {
                    ChartType = SeriesChartType.Bar // Có thể đổi sang Pie, Line, etc.
                };

                // Thêm dữ liệu vào Series
                foreach (var item in thongKeDoanhThu)
                {
                    series.Points.AddXY(item.LoaiDangKy, item.TongDoanhThu);
                }

                chart1.Series.Add(series);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi khi hiển thị thống kê: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HienThiSoLuongTheoTrangThai()
        {
            try
            {
                // Lấy danh sách đăng ký dịch vụ từ BLL
                List<DangKyDichVu> danhSachDangKy = bllDichVu.GetDanhSachDangKyDichVu();

                // Thống kê số lượng theo trạng thái
                var thongKeTrangThai = danhSachDangKy
                    .GroupBy(dv => dv.TrangThai)
                    .Select(group => new
                    {
                        TrangThai = group.Key,
                        SoLuong = group.Count()
                    })
                    .ToList();

                // Cấu hình Chart
                chart2.Series.Clear();
                chart2.Titles.Clear();

                chart2.Titles.Add("Thống kê số lượng dịch vụ theo trạng thái");
                chart2.ChartAreas[0].AxisX.Title = "Trạng Thái";
                chart2.ChartAreas[0].AxisY.Title = "Số Lượng";

                // Thêm Series vào Chart
                System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series("Số lượng")
                {
                    ChartType = SeriesChartType.Pie // Hiển thị dạng Pie Chart
                };

                // Thêm dữ liệu vào Series
                foreach (var item in thongKeTrangThai)
                {
                    series.Points.AddXY(item.TrangThai, item.SoLuong);
                }

                chart2.Series.Add(series);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi khi hiển thị thống kê: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HienThiDangKyTheoNgay()
        {
            try
            {
                // Lấy danh sách đăng ký dịch vụ từ BLL
                List<DangKyDichVu> danhSachDangKy = bllDichVu.GetDanhSachDangKyDichVu();

                // Thống kê số lượng đăng ký theo ngày
                var thongKeTheoNgay = danhSachDangKy
                    .GroupBy(dv => dv.NgayDangKy.Date)
                    .Select(group => new
                    {
                        NgayDangKy = group.Key,
                        SoLuong = group.Count()
                    })
                    .OrderBy(item => item.NgayDangKy)
                    .ToList();

                // Cấu hình Chart
                chart3.Series.Clear();
                chart3.Titles.Clear();

                chart3.Titles.Add("Thống kê số lượng đăng ký theo ngày");
                chart3.ChartAreas[0].AxisX.Title = "Ngày Đăng Ký";
                chart3.ChartAreas[0].AxisY.Title = "Số Lượng";

                // Thêm Series vào Chart
                System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series("Số lượng")
                {
                    ChartType = SeriesChartType.Line // Hiển thị dạng Line Chart
                };

                // Thêm dữ liệu vào Series
                foreach (var item in thongKeTheoNgay)
                {
                    series.Points.AddXY(item.NgayDangKy.ToShortDateString(), item.SoLuong);
                }

                chart3.Series.Add(series);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi khi hiển thị thống kê: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Xuat_DS_ThucDon_Click_1(object sender, EventArgs e)
        {

        }

        private void btn_XuatThucDon_Click_1(object sender, EventArgs e)
        {

        }
    }
}
