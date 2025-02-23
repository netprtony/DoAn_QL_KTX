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


namespace GUI
{
    public partial class QuanLyTrangThietBi : Form
    {
        private BLL_TrangThietBi bll_ttb;
        public BLL_NhaCungCap bll_ncc;
        public BLL_NhanVien bll_nv;
        public QuanLyTrangThietBi()
        {
            InitializeComponent();
            UpdateFontForAllTextBoxes(this);
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; 
            this.Load += QuanLyTrangThietBi_Load;
            bll_ttb = new BLL_TrangThietBi();
            bll_ncc = new BLL_NhaCungCap();
            bll_nv= new BLL_NhanVien();
            dataGV_TrangThietBi.CellClick += DataGV_TrangThietBi_CellClick;
           panel2.AutoScroll = true;
            panel_FillQLNhap.AutoScroll = true;
           panel_FillThongKe.AutoScroll = true;

            cbo_MaPhong.SelectedIndexChanged += Cbo_MaPhong_SelectedIndexChanged;
            cbo_Ten_TTB.SelectedIndexChanged += Cbo_Ten_TTB_SelectedIndexChanged;
            dataGV_CT_TrangThietBi.CellClick += DataGV_CT_TrangThietBi_CellClick;
            comboBoxTinhThanh.SelectedIndexChanged += ComboBoxTinhThanh_SelectedIndexChanged;
            comboBoxHuyen.SelectedIndexChanged += ComboBoxHuyen_SelectedIndexChanged;
            cbo_TenNhaCungCap.SelectedIndexChanged += Cbo_TenNhaCungCap_SelectedIndexChanged;
            cbo_QLN_maNCC.SelectedIndexChanged += Cbo_QLN_maNCC_SelectedIndexChanged;
            ////

            btn_Them_DSNhap.Click += Btn_Them_DSNhap_Click;
            btn_Add_TTB.Click += Btn_Add_TTB_Click;
            btn_SuaTTB.Click += Btn_SuaTTB_Click;
            btn_XoaTTB.Click += Btn_XoaTTB_Click;
            btn_RefreshTTB.Click += Btn_RefreshTTB_Click;

            btn_themCTTTB.Click += Btn_themCTTTB_Click;
            btn_CapNhaCTTTB.Click += Btn_CapNhaCTTTB_Click;
            btn_LamMoiCTTTB.Click += Btn_LamMoiCTTTB_Click;
            btn_XoaCTTTB.Click += Btn_XoaCTTTB_Click;
            txt_MaTTB.Enabled = false;

            //Quan ly nhap
            ///
            dataGV_QLN_DSDonNhap.CellClick += DataGV_QLN_DSDonNhap_CellClick;
            dataGV_QLN_CTDN.CellClick += DataGV_QLN_CTDN_CellClick;
            cbo_Loc_QLN_maDonNhap.SelectedValueChanged += Cbo_Loc_QLN_maDonNhap_SelectedValueChanged;
            cbo_Loc_QLN_NCC.SelectedValueChanged += Cbo_Loc_QLN_NCC_SelectedValueChanged;
            cbo_Loc_QLN_MaNV.SelectedValueChanged += Cbo_Loc_QLN_MaNV_SelectedValueChanged;
            cbo_sapXep_QLN.SelectedIndexChanged += Cbo_sapXep_QLN_SelectedIndexChanged;


            //Thống kê
            btn_xuatThongKe_TongTien.Click += Btn_xuatThongKe_TongTien_Click;
            btn_TKTongTien_Thang.Click += Btn_TKTongTien_Thang_Click;
            btn_TK_SoLuongNhap.Click += Btn_TK_SoLuongNhap_Click;
            btn_XuatTK_SoLuongNhap.Click += Btn_XuatTK_SoLuongNhap_Click;
            LoadCboQuy();

            ////
            btn_Them_DSNhap.Click += Btn_Them_DSNhap_Click;

            btn_LuuPhieuNhap.Click += Btn_LuuPhieuNhap_Click;

            btn_HoaDonNhap.Click += Btn_HoaDonNhap_Click;
        }

        private void Btn_HoaDonNhap_Click(object sender, EventArgs e)
        {
            // Đường dẫn tới mẫu Word
            string templatePath = @"D:\DACN\PhieuNhapThietBiHoaChat\PhieuXuatDonNhapTrangThietBi\HoaDonNhapTrangThietBi.docx";
            string outputPath = @"D:\DACN\PhieuNhapThietBiHoaChat\PhieuXuatDonNhapTrangThietBi\HoaDonNhapThietBi_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".docx";

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
                // Lấy thông tin từ giao diện
                string tenNhaCungCap = cbo_TenNhaCungCap.Text;
                string emailNCC = txt_Email_NhaCungCap.Text;
                string soDienThoaiNCC = txt_SoDienThoai_NCC.Text;
                string diaChiNCC = $"{txt_Duong_Thon.Text}, {comboBoxXa.Text}, {comboBoxHuyen.Text}, {comboBoxTinhThanh.Text}";
                string hoVaTenNhanVien = "Lê Nhật Quyên"; // Có thể thay bằng thông tin thực tế từ tài khoản đăng nhập
                string chucVu = "Nhân Viên Quản Lý";
                string soQuyetDinh = "12345";
                string ngayLap = DateTime.Now.ToString("dd");
                string thangLap = DateTime.Now.ToString("MM");
                string namLap = DateTime.Now.ToString("yyyy");
                string tongTien = txt_tongTienNhap.Text;

                // Thay thế các placeholder trong tài liệu
                ReplacePlaceholder(doc, "«SoQuyetDinh»", soQuyetDinh);
                ReplacePlaceholder(doc, "«Ngay»", ngayLap);
                ReplacePlaceholder(doc, "«Thang»", thangLap);
                ReplacePlaceholder(doc, "«Nam»", namLap);
                ReplacePlaceholder(doc, "«TenNhaCungCap»", tenNhaCungCap);
                ReplacePlaceholder(doc, "«Email»", emailNCC);
                ReplacePlaceholder(doc, "«SoDienThoai»", soDienThoaiNCC);
                ReplacePlaceholder(doc, "«DiaChi»", diaChiNCC);
                ReplacePlaceholder(doc, "«HoVaTenNhanVien»", hoVaTenNhanVien);
                ReplacePlaceholder(doc, "«ChucVu»", chucVu);
                ReplacePlaceholder(doc, "«TongTien»", tongTien);

                // Tìm bảng trong Word và thay thế dữ liệu
                Microsoft.Office.Interop.Word.Range searchRange = doc.Content;
                searchRange.Find.ClearFormatting();
                // Thay đổi nội dung tìm kiếm để sử dụng giá trị Chức Vụ
                searchRange.Find.Text = "Bộ Phận : " + "Nhân Viên Quản Lý";

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


                    // Xóa các dòng cũ trong bảng (trừ dòng tiêu đề nếu có)
                    while (targetTable.Rows.Count > 1)
                    {
                        targetTable.Rows[2].Delete();
                    }

                    // Thêm dữ liệu từ DataGridView vào bảng
                    for (int i = 0; i < dataGV_DS_Nhap_TTB.Rows.Count; i++)
                    {
                        DataGridViewRow row = dataGV_DS_Nhap_TTB.Rows[i];
                        if (row.IsNewRow) continue;

                        string stt = (i + 1).ToString();
                        string maThietBi = row.Cells["MaThietBi"].Value?.ToString() ?? "";
                        string tenThietBi = row.Cells["TenThietBi"].Value?.ToString() ?? "";
                        string soLuong = row.Cells["SoLuong"].Value?.ToString() ?? "";
                        string donGia = row.Cells["DonGia"].Value?.ToString() ?? "";

                        // Thêm dòng mới vào bảng Word
                        Microsoft.Office.Interop.Word.Row newRow = targetTable.Rows.Add();
                        newRow.Cells[1].Range.Text = stt;
                        newRow.Cells[2].Range.Text = maThietBi;
                        newRow.Cells[3].Range.Text = tenThietBi;
                        newRow.Cells[4].Range.Text = soLuong;
                        newRow.Cells[5].Range.Text = donGia;
                    }

                    // Lưu tài liệu
                    doc.SaveAs2(outputPath);
                    MessageBox.Show("Hóa đơn nhập thiết bị đã được lưu tại: " + outputPath, "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tạo hóa đơn nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Đóng tài liệu và ứng dụng Word
                doc.Close(false);
                wordApp.Quit();

                // Giải phóng tài nguyên COM
                System.Runtime.InteropServices.Marshal.ReleaseComObject(doc);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
            }
        }

        private void ReplacePlaceholder(Microsoft.Office.Interop.Word.Document doc, string placeholder, string value)
        {
            Microsoft.Office.Interop.Word.Find findObject = doc.Content.Find;
            findObject.ClearFormatting();
            findObject.Text = placeholder;
            findObject.Replacement.ClearFormatting();
            findObject.Replacement.Text = value;
            findObject.Execute(Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
        }

        private void Btn_LuuPhieuNhap_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo đối tượng BLL
                BLL_NhapTrangThietBi bllNTTB = new BLL_NhapTrangThietBi();

                // Lấy thông tin từ các ô nhập liệu
                string maNCC = txt_MaNhaCungCap.Text.Trim();
                DateTime ngayNhap =date_NgayNhap.Value;
                decimal tongTienNhap = decimal.Parse(txt_tongTienNhap.Text.Trim());
                string trangThai = "Hoạt Động";
                string maNhanVien = "NV001";

                // Kiểm tra danh sách nhập từ DataGridView
                if (dataGV_DS_Nhap_TTB.Rows.Count == 0)
                {
                    MessageBox.Show("Danh sách nhập trang thiết bị không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo đối tượng DonNhap_TTB
                DonNhap_TTB donNhap = new DonNhap_TTB
                {
                    NgayNhap = ngayNhap,
                    TongTien = tongTienNhap,
                    TrangThai = trangThai,
                    MaNhanVien = maNhanVien,
                    MaNCC = maNCC
                };

                // Lấy danh sách chi tiết đơn nhập từ DataGridView
                List<CT_DonNhap> chiTietDonNhap = new List<CT_DonNhap>();

                foreach (DataGridViewRow row in dataGV_DS_Nhap_TTB.Rows)
                {
                    if (row.Cells["MaThietBi"].Value != null && row.Cells["SoLuong"].Value != null && row.Cells["DonGia"].Value != null)
                    {
                        CT_DonNhap chiTiet = new CT_DonNhap
                        {
                            MaThietBi = row.Cells["MaThietBi"].Value.ToString(),
                            SoLuong = int.Parse(row.Cells["SoLuong"].Value.ToString()),
                            DonGia = decimal.Parse(row.Cells["DonGia"].Value.ToString()),
                            TrangThai = "Hoạt Động"
                        };
                        chiTietDonNhap.Add(chiTiet);
                    }
                }

                // Gọi phương thức thêm đơn nhập từ BLL
                bool result = bllNTTB.ThemDonNhapTTB(donNhap, chiTietDonNhap);

                if (result)
                {
                    MessageBox.Show("Lưu phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Reset hoặc cập nhật giao diện sau khi lưu thành công
                    txt_MaNhaCungCap.Clear();
                    txt_tongTienNhap.Clear();
                    dataGV_DS_Nhap_TTB.Rows.Clear();
                }
                else
                {
                    MessageBox.Show("Lưu phiếu nhập thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_XuatTK_SoLuongNhap_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ DataGridView
                var thongKeData = new List<ThongKeDonNhap>();

                foreach (DataGridViewRow row in dataGV_TK_Quy_TTB.Rows)
                {
                    if (row.Cells["MaThietBi"].Value != null && row.Cells["TongSoLuongNhap"].Value != null)
                    {
                        var maThietBi = row.Cells["MaThietBi"].Value.ToString();
                        var tenThietBi = row.Cells["TenThietBi"].Value.ToString();
                        var tongSoLuongNhap = int.Parse(row.Cells["TongSoLuongNhap"].Value.ToString());

                        thongKeData.Add(new ThongKeDonNhap
                        {
                            MaThietBi = maThietBi,
                            TenThietBi = tenThietBi,
                            TongSoLuongNhap = tongSoLuongNhap
                        });
                    }
                }

                // Kiểm tra dữ liệu
                if (thongKeData.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Gọi hàm xuất Excel
                XuatThongKeRaExcel_SoLuongNhap(thongKeData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xuất dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void XuatThongKeRaExcel_SoLuongNhap(List<ThongKeDonNhap> thongKeData)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Cấu hình hộp thoại lưu file
                saveFileDialog.Filter = "Excel Files|*.xlsx";  // Chỉ cho phép lưu file Excel
                saveFileDialog.Title = "Lưu file Thống Kê";  // Tiêu đề hộp thoại
                saveFileDialog.FileName = "ThongKe_SoLuongNhap.xlsx";  // Tên file mặc định

                // Hiển thị hộp thoại và kiểm tra xem người dùng có chọn file không
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Lấy đường dẫn đầy đủ (bao gồm tên file) mà người dùng đã chọn
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        using (ExcelPackage excel = new ExcelPackage())
                        {
                            // Tạo sheet mới
                            var workSheet = excel.Workbook.Worksheets.Add("Thống Kê");

                            // Thêm thông tin vào các ô trước khi thêm dữ liệu thống kê
                            workSheet.Cells[1, 1].Value = "BỘ CÔNG THƯƠNG";
                            workSheet.Cells[2, 1].Value = "TRƯỜNG ĐẠI HỌC CÔNG THƯƠNG THÀNH PHỐ HỒ CHÍ MINH";
                            workSheet.Cells[4, 1].Value = "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM";
                            workSheet.Cells[5, 1].Value = "Độc lập - Tự do - Hạnh phúc";
                            workSheet.Cells[7, 1].Value = "Ngày " + DateTime.Now.ToString("dd/MM/yyyy");

                            int soThuTu = 1; // Ví dụ: số thứ tự, có thể tăng lên mỗi lần xuất
                            workSheet.Cells[8, 1].Value = "Số: " + soThuTu.ToString("D3"); // D3 sẽ làm số có 3 chữ số, ví dụ "001", "002"

                            // Tiêu đề cột
                            workSheet.Cells[10, 1].Value = "Mã Thiết Bị";
                            workSheet.Cells[10, 2].Value = "Tên Thiết Bị";
                            workSheet.Cells[10, 3].Value = "Tổng Số Lượng Nhập";

                            // Dữ liệu
                            for (int i = 0; i < thongKeData.Count; i++)
                            {
                                workSheet.Cells[i + 11, 1].Value = thongKeData[i].MaThietBi;
                                workSheet.Cells[i + 11, 2].Value = thongKeData[i].TenThietBi;
                                workSheet.Cells[i + 11, 3].Value = thongKeData[i].TongSoLuongNhap;
                            }

                            // Lưu file Excel vào đường dẫn người dùng đã chọn
                            FileInfo excelFile = new FileInfo(filePath);
                            excel.SaveAs(excelFile);

                            // Thông báo người dùng
                            MessageBox.Show("Xuất dữ liệu thành công: " + filePath, "Thông Báo");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi nếu có
                        MessageBox.Show("Đã xảy ra lỗi khi lưu file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void Cbo_QLN_maNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu ComboBox không rỗng và có item được chọn
            if (cbo_QLN_maNCC.SelectedItem != null)
            {
                // Lấy tên nhà cung cấp từ ComboBox
                string maNCC = cbo_QLN_maNCC.Text.Trim();
                // MessageBox.Show("Tên Nhà Cung Cấp: " + tenNCC);

                // Khởi tạo lớp BLL_NhaCungCap
                BLL_NhaCungCap bll_ncc = new BLL_NhaCungCap();

                // Gọi phương thức trong BLL để lấy thông tin nhà cung cấp theo tên
                var nhaCungCap = bll_ncc.GetNhaCungCapByMa(maNCC);

                // Kiểm tra nếu nhà cung cấp được tìm thấy
                if (nhaCungCap != null)
                {
                    // Cập nhật các TextBox và ComboBox với thông tin của nhà cung cấp
                    txt_QLN_tenNCC.Text = nhaCungCap.TenNCC;
                    txt_QLN_diaChi.Text = nhaCungCap.DiaChi;
                   
                  
                }
                else
                {
                    // Nếu không tìm thấy nhà cung cấp, hiển thị thông báo
                    //MessageBox.Show("Không tìm thấy thông tin nhà cung cấp.");
                }
            }
            else
            {
                // Nếu không có mục nào được chọn trong ComboBox, xóa dữ liệu TextBox và ComboBox
                txt_QLN_diaChi.Clear();
                txt_QLN_tenNCC.Clear();
               
            }
        }

        public void LoadNhaCungCap()
        {
            cbo_QLN_maNCC.DataSource = bll_ncc.GetAllNhaCungCapNames();
            cbo_QLN_maNCC.DisplayMember = "MaNCC";
            cbo_QLN_maNCC.ValueMember= "MaNCC";
        }

        private void VoHieuHoaQLNhap()
        {
            txt_QLN_chucVu.Enabled= false;
            txt_QLN_hotenNV.Enabled= false;
            txt_QLN_maNV.Enabled    = false;
            txt_QLN_MaDonNhap.Enabled = false;
            txt_MaTTB.Enabled = false;
            txt_Ma_TTB.Enabled = false;
            txt_donnhap_maTB.Enabled = false;
            txt_donnhap_tenTB.Enabled = false;
            txt_QLN_diaChi.Enabled = false;
            txt_QLN_tenNCC.Enabled = false;
        }
        private void VoHieuHoa(bool vohieu)
        {
            txt_trangthai_TTB.Enabled = vohieu;
            txt_TrangThai.Enabled = vohieu;
            txt_LoaiPhong.Enabled = vohieu;
        //    cbo_MaPhong.Enabled = vohieu;
             //  cbo_Ten_TTB.Enabled = vohieu;
        //    txt_soluong.Enabled = vohieu;

            btn_XoaTTB.Enabled = vohieu;
            btn_SuaTTB.Enabled = vohieu;
            btn_RefreshTTB.Enabled = vohieu;
           
            btn_XoaCTTTB.Enabled = vohieu;
            btn_CapNhaCTTTB.Enabled = vohieu;
            btn_LamMoiCTTTB.Enabled = vohieu;
        }
        private void DrawPieChart(List<ThongKeDonNhap> thongKeData)
        {
            // Xóa dữ liệu cũ trong Chart
            chart_TK_SlNhap.Series.Clear();

            // Tạo Series mới
            var series = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Số lượng thiết bị",
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie, // Biểu đồ hình tròn
                XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String,
                YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32,
                IsValueShownAsLabel = true  // Hiển thị số lượng nhập trên các lát cắt
            };

            chart_TK_SlNhap.Series.Add(series);

            // Thêm dữ liệu vào Series
            foreach (var item in thongKeData)
            {
                series.Points.AddXY(item.TenThietBi, item.TongSoLuongNhap);
            }

            // Thiết lập hiển thị biểu đồ
            chart_TK_SlNhap.Legends.Clear(); // Xóa legends cũ nếu có
            var legend = new System.Windows.Forms.DataVisualization.Charting.Legend
            {
                Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Right,
                Alignment = StringAlignment.Center
            };
            chart_TK_SlNhap.Legends.Add(legend);

            // Cập nhật tooltip hoặc chú thích cho từng lát cắt
            foreach (var point in series.Points)
            {
                point.ToolTip = $"Thiết bị: {point.AxisLabel}, Số lượng: {point.YValues[0]}";
            }
        }

        private void LoadCboQuy()
        {
            cbo_Quy_TKTTB.Items.Add("Quý 1");
            cbo_Quy_TKTTB.Items.Add("Quý 2");
            cbo_Quy_TKTTB.Items.Add("Quý 3");
            cbo_Quy_TKTTB.Items.Add("Quý 4");

            // Đặt giá trị mặc định
            cbo_Quy_TKTTB.SelectedIndex = 0; // Quý 1
        }

        private void Btn_TK_SoLuongNhap_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ các điều khiển trên form
            int nam = int.Parse(txt_Nam_TK_TTB.Text);  // Năm từ TextBox
            int quy = cbo_Quy_TKTTB.SelectedIndex + 1;  // Quý từ ComboBox (Chỉ số 0-based, cộng 1 để thành 1-based)

            // Tạo đối tượng BLL_YeuCauSuaChua
            BLL_YeuCauSuaChua bll = new BLL_YeuCauSuaChua();

            // Lấy dữ liệu từ BLL
            List<ThongKeDonNhap> thongKeData = bll_ttb.GetThongKeTheoQuy(nam, quy);

            // Đưa dữ liệu vào DataGridView
            dataGV_TK_Quy_TTB.DataSource = thongKeData;
            dataGV_TK_Quy_TTB.Columns["TongSoLuongNhap"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGV_TK_Quy_TTB.Columns["TongSoLuongNhap"].HeaderText = "Tổng số lượng nhập";
            dataGV_TK_Quy_TTB.Columns["MaThietBi"].HeaderText = "Mã thiết bị";
            dataGV_TK_Quy_TTB.Columns["TenThietBi"].HeaderText = "Tên thiết bị";
         
            dataGV_TK_Quy_TTB.Columns["Thang"].Visible = false;
            dataGV_TK_Quy_TTB.Columns["TongTien"].Visible = false;

            // Vẽ biểu đồ
            DrawPieChart(thongKeData);
        }
        private void DrawLineChart(List<ThongKeDonNhap> thongKeData)
        {
            // Xóa dữ liệu cũ trong Chart
            chart_TKTien_TTB.Series.Clear();

            // Tạo Series mới cho biểu đồ đường
            var series = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Tổng Tiền Nhập",
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, // Biểu đồ đường (Line Chart)
                XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String, // Trục X là chuỗi (Tháng)
                YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32 // Trục Y là số nguyên (Tổng Tiền)
            };

            // Thêm Series vào biểu đồ
            chart_TKTien_TTB.Series.Add(series);

            // Thêm dữ liệu vào Series
            foreach (var item in thongKeData)
            {
                // Thêm điểm vào biểu đồ đường, với 'Tháng' là trục X và 'Tổng Tiền' là trục Y
                series.Points.AddXY("Tháng " + item.Thang, item.TongTien);
            }
            // Lấy giá trị lớn nhất trong dữ liệu
            var maxValue = thongKeData.Max(item => item.TongTien);
            double maxValueDouble = (double)maxValue;
            // Đặt giá trị tối thiểu, tối đa và khoảng cách của trục Y
            chart_TKTien_TTB.ChartAreas[0].AxisY.Minimum = 0;
            chart_TKTien_TTB.ChartAreas[0].AxisY.Maximum = maxValueDouble + (maxValueDouble * 0.1); // Thêm 10%
            chart_TKTien_TTB.ChartAreas[0].AxisY.Interval = maxValueDouble / 5; // Chia thành 5 khoảng

            // Tăng kích thước vùng hiển thị biểu đồ
            chart_TKTien_TTB.ChartAreas[0].InnerPlotPosition.Height = 90;
            chart_TKTien_TTB.ChartAreas[0].InnerPlotPosition.Y = 5;

            // Hiển thị giá trị trên các điểm
            series.IsValueShownAsLabel = true;
            series.BorderWidth = 3;
            series.Color = Color.Blue;

            // Xóa lưới dọc và thiết lập màu trục
            chart_TKTien_TTB.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart_TKTien_TTB.ChartAreas[0].AxisX.LineColor = Color.Green;
            chart_TKTien_TTB.ChartAreas[0].AxisY.LineColor = Color.Green;

            // Căn chỉnh nhãn trục X
        //    chart_TKTien_TTB.ChartAreas[0].AxisX.LabelStyle.Angle = 90;

            // Thiết lập hiển thị biểu đồ
            chart_TKTien_TTB.Legends.Clear(); // Xóa legends cũ nếu có
            var legend = new System.Windows.Forms.DataVisualization.Charting.Legend
            {
                Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Right,
                Alignment = StringAlignment.Center
            };
            chart_TKTien_TTB.Legends.Add(legend);

            // Cải thiện hiển thị các điểm trên biểu đồ đường (chấm, màu sắc, đường thẳng, v.v.)
            series.BorderWidth = 3; // Độ dày của đường biểu đồ
            series.Color = Color.Blue; // Màu sắc đường biểu đồ
            series.IsValueShownAsLabel = true; // Hiển thị giá trị tại các điểm trên biểu đồ\


        //    // Tắt lưới và số trên trục Y
        //    chart_TKTien_TTB.ChartAreas[0].AxisY.MajorGrid.Enabled = false;  // Tắt lưới
        //    chart_TKTien_TTB.ChartAreas[0].AxisY.LabelStyle.Enabled = false; // Tắt số trên trục Y
        ////    chart_TKTien_TTB.ChartAreas[0].AxisX.LabelStyle.Angle = 90; // 90 độ để chữ nằm dọc
         chart_TKTien_TTB.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Green;

        //    // Thay đổi màu sắc của các trục X và Y
        //    chart_TKTien_TTB.ChartAreas[0].AxisX.LineColor = Color.Green; // Màu đường trục X
        //    chart_TKTien_TTB.ChartAreas[0].AxisY.LineColor = Color.Green; // Màu đường trục Y
            //                                                              // Đặt trục Y bắt đầu từ 0 và cách nhau 5 triệu
            //chart_TKTien_TTB.ChartAreas[0].AxisY.Minimum = 0; // Đặt giá trị tối thiểu của trục Y là 0
            //chart_TKTien_TTB.ChartAreas[0].AxisY.Interval = 5000000; // Đặt khoảng cách giữa các nhãn trên trục Y là 5 triệu
            //                                                         // Giảm chiều cao trục Y (Thu nhỏ chiều cao của biểu đồ)
            //chart_TKTien_TTB.ChartAreas[0].Position.Height = 50; // Điều chỉnh chiều cao trục Y,
            //                                                     //   /                                                         //   // Điều chỉnh vị trí của trục Y và trục X để tránh trục Y quá gần
            //                                                     //  chart_TKTien_TTB.ChartAreas[0].Position.Y = 10; // Đẩy trục Y xuống dưới, giảm không gian của trục Y
            //                                                     //  chart_TKTien_TTB.ChartAreas[0].Position.X = 10; // Đẩy trục X sang phải nếu cầngiá trị này có thể thay đổi tùy ý\
            //                                                     // Điều chỉnh margin của biểu đồ
            //chart_TKTien_TTB.Margin = new Padding(20); // Giảm hoặc tăng giá trị margin tùy vào nhu cầu
            //                                           // Giảm khoảng cách trên trục X và Y để có không gian cho biểu đồ
            //chart_TKTien_TTB.ChartAreas[0].InnerPlotPosition.Y = 10;  // Điều chỉnh khoảng cách trên cùng
            //chart_TKTien_TTB.ChartAreas[0].InnerPlotPosition.Height = 80;  // Điều chỉnh chiều cao vùng biểu đồ


        }


        private void Btn_TKTongTien_Thang_Click(object sender, EventArgs e)
        {
            try
            {
           //     Lấy giá trị từ các điều khiển trên form
                int nam = int.Parse(txt_nam_TKTheoThang.Text); // Năm từ TextBox

            //    Tạo đối tượng BLL
                BLL_TrangThietBi bll = new BLL_TrangThietBi();

         //       Lấy dữ liệu từ BLL
                List<ThongKeDonNhap> thongKeData = bll.GetThongKeTongTienTheoThang(nam);

          //      Đưa dữ liệu vào DataGridView
                dataGV_TK_TongTien.DataSource = thongKeData;
                dataGV_TK_TongTien.Columns["Thang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; // Tự động vừa với nội dung
                dataGV_TK_TongTien.Columns["TongTien"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;    // Tự động giãn đầy chiều rộng DataGridView
                dataGV_TK_TongTien.Columns["Thang"].HeaderText = "Tháng";
                dataGV_TK_TongTien.Columns["TongTien"].HeaderText = "Tổng tiền";
                dataGV_TK_TongTien.Columns["MaThietBi"].Visible = false;
                dataGV_TK_TongTien.Columns["TenThietBi"].Visible = false;
                dataGV_TK_TongTien.Columns["TongSoLuongNhap"].Visible = false;

       //         Vẽ biểu đồ(nếu cần)
                DrawLineChart(thongKeData);
            }
            catch (FormatException ex)
            {
           //     Xử lý lỗi khi không thể chuyển đổi kiểu dữ liệu(ví dụ: người dùng nhập không phải là số)
                MessageBox.Show("Vui lòng nhập một năm hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
             //   Xử lý các lỗi khác(ví dụ: lỗi kết nối cơ sở dữ liệu, lỗi trong BLL, ...)
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_xuatThongKe_TongTien_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ DataGridView (nếu danh sách thongKeData chưa được lưu trữ)
                var thongKeData = new List<ThongKeDonNhap>();

                foreach (DataGridViewRow row in dataGV_TK_TongTien.Rows)
                {
                    if (row.Cells["Thang"].Value != null && row.Cells["TongTien"].Value != null)
                    {
                        var thang = int.Parse(row.Cells["Thang"].Value.ToString());
                        var tongTien = decimal.Parse(row.Cells["TongTien"].Value.ToString());

                        thongKeData.Add(new ThongKeDonNhap
                        {
                            Thang = thang,
                            TongTien = tongTien
                        });
                    }
                }

                // Kiểm tra dữ liệu
                if (thongKeData.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Gọi hàm xuất Excel
                XuatThongKeRaExcel(thongKeData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xuất dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void XuatThongKeRaExcel(List<ThongKeDonNhap> thongKeData)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Cấu hình hộp thoại lưu file
                saveFileDialog.Filter = "Excel Files|*.xlsx";  // Chỉ cho phép lưu file Excel
                saveFileDialog.Title = "Lưu file Thống Kê";  // Tiêu đề hộp thoại
                saveFileDialog.FileName = "ThongKe_TienNhap.xlsx";  // Tên file mặc định

                // Hiển thị hộp thoại và kiểm tra xem người dùng có chọn file không
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Lấy đường dẫn đầy đủ (bao gồm tên file) mà người dùng đã chọn
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        using (ExcelPackage excel = new ExcelPackage())
                        {
                            // Tạo sheet mới
                            var workSheet = excel.Workbook.Worksheets.Add("Thống Kê");

                            // Thêm thông tin vào các ô trước khi thêm dữ liệu thống kê
                            workSheet.Cells[1, 1].Value = "BỘ CÔNG THƯƠNG";
                            workSheet.Cells[2, 1].Value = "TRƯỜNG ĐẠI HỌC CÔNG THƯƠNG THÀNH PHỐ HỒ CHÍ MINH";
                            workSheet.Cells[4, 1].Value = "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM";
                            workSheet.Cells[5, 1].Value = "Độc lập - Tự do - Hạnh phúc";
                            workSheet.Cells[7, 1].Value = "Ngày " + DateTime.Now.ToString("dd/MM/yyyy");

                            int soThuTu = 1; // Ví dụ: số thứ tự, có thể tăng lên mỗi lần xuất
                            workSheet.Cells[8, 1].Value = "Số: " + soThuTu.ToString("D3"); // D3 sẽ làm số có 3 chữ số, ví dụ "001", "002"


                            // Tiêu đề cột (tạo khoảng cách giữa tiêu đề và dữ liệu)
                            workSheet.Cells[10, 1].Value = "Tháng";
                            workSheet.Cells[10, 2].Value = "Tổng Tiền Nhập";

                            // Dữ liệu
                            for (int i = 0; i < thongKeData.Count; i++)
                            {
                                workSheet.Cells[i + 11, 1].Value = "Tháng " + thongKeData[i].Thang;
                                workSheet.Cells[i + 11, 2].Value = thongKeData[i].TongTien;
                            }

                            // Lưu file Excel vào đường dẫn người dùng đã chọn
                            FileInfo excelFile = new FileInfo(filePath);
                            excel.SaveAs(excelFile);

                            // Thông báo người dùng
                            MessageBox.Show("Xuất dữ liệu thành công: " + filePath, "Thông Báo");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi nếu có
                        MessageBox.Show("Đã xảy ra lỗi khi lưu file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void UpdateFontForAllTextBoxes(System.Windows.Forms.Control container)
        {
            foreach (System.Windows.Forms.Control control in container.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Font = new Font("Arial", 8);
              
                }
                if (control is ComboBox cbo)
                {
                    cbo.Font = new Font("Arial", 8);
                  
                }
              
                if (control is TabControl tab)
                {
                    tab.Font = new Font("Arial", 9, FontStyle.Bold);
                       tab.ForeColor = Color.DarkBlue;
                }
          
            if (control.HasChildren)
                {
                    UpdateFontForAllTextBoxes(control);
                }
            }
        }



        private void Btn_XoaCTTTB_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu có dòng được chọn trong DataGridView
                if (dataGV_CT_TrangThietBi.SelectedRows.Count > 0)
                {
                    string maThietBi = dataGV_CT_TrangThietBi.SelectedRows[0].Cells["MaThietBi"].Value.ToString(); // Mã thiết bị
                    string maPhong = dataGV_CT_TrangThietBi.SelectedRows[0].Cells["MaPhong"].Value.ToString();  // Mã phòng
                    
                    // Hiển thị hộp thoại xác nhận
                    DialogResult confirm = MessageBox.Show("Bạn có chắc chắn muốn ngừng hoạt động chi tiết thiết bị đã chọn?",
                                                           "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.No)
                        return;
                    
                    // Gọi phương thức cập nhật trạng thái
                    bool isUpdated = bll_ttb.CapNhatTrangThaiChiTietTTB(maThietBi, maPhong, "Ngừng Hoạt Động");

                    if (isUpdated)
                    {
                        // Cập nhật tổng số lượng trong TTB
                        bool isSoLuongUpdated = bll_ttb.UpdateTongSoLuong(maThietBi);

                        if (isSoLuongUpdated)
                        {
                            MessageBox.Show("Cập nhật trạng thái chi tiết và tổng số lượng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật trạng thái thành công nhưng không thể cập nhật tổng số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        // Làm mới danh sách chi tiết thiết bị
                        LoadDataCT_TrangThietBi();
                        LoadDuLieuTrangThietBI(); // Làm mới danh sách thiết bị nếu cần
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật trạng thái chi tiết thiết bị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một chi tiết từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Btn_LamMoiCTTTB_Click(object sender, EventArgs e)
        {
            try
            {
                // Xóa sạch nội dung trong các trường nhập liệu
                txt_MaTTB.Text = string.Empty;
                cbo_MaPhong.SelectedIndex = -1; // Bỏ chọn ComboBox Mã Phòng
                txt_soluong.Text = string.Empty;
                txt_TrangThai.Text = string.Empty;
                cbo_Ten_TTB.SelectedIndex = -1;
                txt_LoaiPhong.Text = string.Empty;
                // Đưa con trỏ về trường nhập liệu đầu tiên (nếu cần)
                txt_MaTTB.Focus();

                }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi làm mới: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Btn_CapNhaCTTTB_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu thông tin đầu vào bị thiếu
                if (string.IsNullOrWhiteSpace(txt_MaTTB.Text) || string.IsNullOrWhiteSpace(cbo_MaPhong.Text) || string.IsNullOrWhiteSpace(txt_soluong.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo đối tượng chi tiết trang thiết bị từ thông tin nhập liệu
                CT_TrangThietBi chiTiet = new CT_TrangThietBi
                {
                    MaThietBi = txt_MaTTB.Text,
                    MaPhong = cbo_MaPhong.SelectedValue.ToString(),
                    SoLuong = int.Parse(txt_soluong.Text),
                //    TrangThai = txt_TrangThai.Text,
                };

                // Gọi phương thức cập nhật trong BLL
                bool isUpdated = bll_ttb.UpdateChiTietTrangThietBi(chiTiet);

                if (isUpdated)
                {
                    // Sau khi cập nhật chi tiết, đồng bộ lại tổng số lượng trong bảng TrangThietBi
                    bool isTongSoLuongUpdated = bll_ttb.UpdateTongSoLuong(chiTiet.MaThietBi);

                    if (isTongSoLuongUpdated)
                    {
                        MessageBox.Show("Sửa chi tiết TTB thành công và cập nhật tổng số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataCT_TrangThietBi();
                        LoadDuLieuTrangThietBI(); 
                    }
                    else
                    {
                        MessageBox.Show("Sửa chi tiết thành công, nhưng không thể cập nhật tổng số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    // Làm mới danh sách chi tiết trang thiết bị
                    LoadDataCT_TrangThietBi();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy chi tiết TTB để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Btn_themCTTTB_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_MaTTB.Text) || string.IsNullOrWhiteSpace(cbo_MaPhong.Text) || string.IsNullOrWhiteSpace(txt_soluong.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CT_TrangThietBi chiTiet = new CT_TrangThietBi
                {
                    MaThietBi = txt_MaTTB.Text,
                    MaPhong = cbo_MaPhong.SelectedValue.ToString(),
                    SoLuong = int.Parse(txt_soluong.Text),
                    TrangThai = "Hoạt động"
                };

                // Thêm chi tiết thiết bị
                bool isAdded = bll_ttb.AddChiTietTrangThietBi(chiTiet);

                if (isAdded)
                {
                    // Cập nhật tổng số lượng của trang thiết bị
                    bool isSoLuongUpdated = bll_ttb.UpdateTongSoLuong(chiTiet.MaThietBi);

                    // Cập nhật trạng thái thiết bị nếu tất cả chi tiết đều "Ngừng Hoạt Động"
                   

                    if (isSoLuongUpdated )
                    {
                        MessageBox.Show("Thêm chi tiết TTB thành công, cập nhật  tổng số lượng thiết bị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (isSoLuongUpdated)
                    {
                        MessageBox.Show("Thêm chi tiết TTB thành công và cập nhật tổng số lượng thiết bị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Thêm chi tiết TTB thành công, nhưng không thể cập nhật tổng số lượng thiết bị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    // Làm mới danh sách chi tiết và thiết bị
                    LoadDataCT_TrangThietBi();
                    LoadDuLieuTrangThietBI();
                }
                else
                {
                    MessageBox.Show("Thêm chi tiết TTB thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Btn_RefreshTTB_Click(object sender, EventArgs e)
        {
           // txt_Ma_TTB.Enabled = true;
            txt_Ma_TTB.Text = string.Empty;
            txt_Ten_TTB.Text = string.Empty;
            txt_trangthai_TTB.Text = string.Empty;
     
          //  txt_Ma_TTB.Enabled = true;
            txt_Ten_TTB.Focus();
        }


        private void Btn_XoaTTB_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu có dòng được chọn trong DataGridView
                if (dataGV_TrangThietBi.SelectedRows.Count > 0)
                {
                    string maThietBi = dataGV_TrangThietBi.SelectedRows[0].Cells[0].Value.ToString(); // Chỉnh tên hoặc index cột đúng với DataGridView

                    // Hiển thị hộp thoại xác nhận
                    DialogResult confirm = MessageBox.Show("Bạn có chắc chắn muốn thực hiện thao tác này với thiết bị đã chọn?",
                                                            "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.No)
                        return;

                    // Gọi phương thức xử lý xóa hoặc cập nhật trạng thái
                    bool isProcessed = bll_ttb.XoaHoacCapNhatTrangThai(maThietBi);

                    if (isProcessed)
                    {
                        //  MessageBox.Show("Thao tác thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Sau khi xử lý xong trạng thái thiết bị, cập nhật trạng thái các chi tiết thiết bị liên quan
                        bool isChiTietUpdated = bll_ttb.UpdateTrangThaiChiTietByMaThietBi(maThietBi, "Ngưng Hoạt Động");

                        if (isChiTietUpdated)
                        {
                            MessageBox.Show("Thao tác thành công! ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Thao tác thành công! Nhưng không có chi tiết nào cần cập nhật trạng thái.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        // Làm mới danh sách thiết bị
                        LoadDuLieuTrangThietBI();
                        LoadDataCT_TrangThietBi();
                        // Xóa các trường nhập liệu
                        txt_Ma_TTB.Text = string.Empty;
                        txt_Ten_TTB.Text = string.Empty;
                        txt_trangthai_TTB.Text = string.Empty;
                       
                    }
                    else
                    {
                        MessageBox.Show("Không thể thực hiện thao tác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một thiết bị từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void Btn_SuaTTB_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu mã thiết bị bị bỏ trống
                if (string.IsNullOrWhiteSpace(txt_Ma_TTB.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã thiết bị cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra các trường nhập liệu khác
                if (string.IsNullOrWhiteSpace(txt_Ten_TTB.Text) || string.IsNullOrWhiteSpace(txt_trangthai_TTB.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy thông tin từ giao diện
                TrangThietBi ttb = new TrangThietBi
                {
                    MaThietBi = txt_Ma_TTB.Text,
                    TenThietBi = txt_Ten_TTB.Text,
                    TrangThai = txt_trangthai_TTB.Text
                };

                // Gọi phương thức sửa trong lớp xử lý nghiệp vụ
                bool isUpdated = bll_ttb.UpdateTrangThietBi(ttb);

                if (isUpdated)
                {
                    MessageBox.Show("Cập nhật thiết bị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Làm mới danh sách thiết bị
                    LoadDuLieuTrangThietBI();

                    // Xóa nội dung các trường nhập liệu
                    txt_Ma_TTB.Text = string.Empty;
                    txt_Ten_TTB.Text = string.Empty;
                    txt_trangthai_TTB.Text = string.Empty;
                

                    // Đưa con trỏ về trường nhập liệu đầu tiên
                    txt_Ten_TTB.Focus();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thiết bị cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Btn_Add_TTB_Click(object sender, EventArgs e)
        {
            txt_Ten_TTB.Enabled = true;
           
            try
            {
                txt_Ma_TTB.Text = bll_ttb.GenerateNextMaThietBi();
                if (string.IsNullOrWhiteSpace(txt_Ma_TTB.Text) ||
                    string.IsNullOrWhiteSpace(txt_Ten_TTB.Text)  )
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                TrangThietBi ttb = new TrangThietBi
                {
                    MaThietBi = txt_Ma_TTB.Text,
                    TenThietBi = txt_Ten_TTB.Text,
                    TrangThai = "Hoạt động"
                };

                bll_ttb.AddTrangThietBi(ttb);
                MessageBox.Show("Thêm thiết bị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Làm sạch các trường nhập
                txt_Ma_TTB.Text = string.Empty;
                txt_Ten_TTB.Text = string.Empty;
                txt_trangthai_TTB.Text = string.Empty;
              
                // Tải lại danh sách thiết bị (nếu có)
                LoadDuLieuTrangThietBI();         }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Btn_Them_DSNhap_Click(object sender, EventArgs e)
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
                       // MessageBox.Show("Số lượng cột: " + colCount.ToString());
                       // MessageBox.Show("Số lượng hàng: " + rowCount.ToString());

                        // Kiểm tra xem số cột có hợp lệ không
                        if (colCount != dataGV_DS_Nhap_TTB.Columns.Count)
                        {
                            MessageBox.Show("Số lượng cột trong file Excel không khớp với số cột trong DataGridView.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Xóa dữ liệu cũ trong DataGridView
                        dataGV_DS_Nhap_TTB.Rows.Clear();

                        // Đọc dữ liệu từ worksheet và thêm vào DataGridView
                        for (int row = 8; row <= rowCount; row++) // Bắt đầu từ dòng thứ 8 (dòng tiêu đề có thể nằm ở trên)
                        {
                            var dgvRow = new DataGridViewRow();
                            for (int col = 1; col <= colCount; col++) // Duyệt các cột
                            {
                                // Lấy dữ liệu từ từng ô và thêm vào dòng của DataGridView
                                string cellValue = worksheet.Cells[row, col].Text.Trim();
                                dgvRow.Cells.Add(new DataGridViewTextBoxCell { Value = cellValue });
                            }

                            // Thêm dòng vào DataGridView
                            dataGV_DS_Nhap_TTB.Rows.Add(dgvRow);
                        }

                        // Tính tổng số của cột "Đơn Giá"
                        decimal tongTien = 0;
                        foreach (DataGridViewRow dgvRow in dataGV_DS_Nhap_TTB.Rows)
                        {
                            if (dgvRow.Cells["DonGia"].Value != null)
                            {
                                if (decimal.TryParse(dgvRow.Cells["DonGia"].Value.ToString(), out decimal donGia))
                                {
                                    tongTien += donGia;
                                }
                            }
                        }

                        // Hiển thị tổng tiền vào TextBox
                        txt_tongTienNhap.Text = tongTien.ToString("N2");

                        MessageBox.Show("Dữ liệu đã được nhập thành công từ file Excel!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi nhập dữ liệu từ Excel: " + ex.Message);
                }
            }
        }



        private void SetupDataGridView_DS_Nhap_TTB()
        {
            // Đảm bảo xóa các cột cũ trong DataGridView nếu có
            dataGV_DS_Nhap_TTB.Columns.Clear();

            // Thêm cột vào DataGridView
            dataGV_DS_Nhap_TTB.Columns.Add("STT", "STT");
            dataGV_DS_Nhap_TTB.Columns.Add("MaThietBi", "Mã Thiết Bị");
            dataGV_DS_Nhap_TTB.Columns.Add("TenThietBi", "Tên Thiết Bị");
            dataGV_DS_Nhap_TTB.Columns.Add("SoLuong", "Số Lượng");
            dataGV_DS_Nhap_TTB.Columns.Add("DonGia", "Đơn Giá");
        }

        private void Cbo_TenNhaCungCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu ComboBox không rỗng và có item được chọn
            if (cbo_TenNhaCungCap.SelectedItem != null)
            {
                // Lấy tên nhà cung cấp từ ComboBox
                string tenNCC = cbo_TenNhaCungCap.Text.Trim();
               // MessageBox.Show("Tên Nhà Cung Cấp: " + tenNCC);

                // Khởi tạo lớp BLL_NhaCungCap
                BLL_NhaCungCap bll_ncc = new BLL_NhaCungCap();

                // Gọi phương thức trong BLL để lấy thông tin nhà cung cấp theo tên
                var nhaCungCap = bll_ncc.GetNhaCungCapByName(tenNCC);

                // Kiểm tra nếu nhà cung cấp được tìm thấy
                if (nhaCungCap != null)
                {
                    // Cập nhật các TextBox và ComboBox với thông tin của nhà cung cấp
                    txt_MaNhaCungCap.Text = nhaCungCap.MaNCC;
                    txt_Email_NhaCungCap.Text = nhaCungCap.Email;
                    txt_SoDienThoai_NCC.Text = nhaCungCap.SoDienThoai;

                    // Kiểm tra nếu địa chỉ không rỗng và có ít nhất một phần tử
                    string[] diaChiParts = nhaCungCap.DiaChi?.Split(',');
                    if (diaChiParts != null && diaChiParts.Length >= 4)
                    {
                        txt_Duong_Thon.Text = diaChiParts[0].Trim();      // Đoạn đường/thôn
                        comboBoxXa.Text = diaChiParts[1].Trim();          // Phường/Xã
                        comboBoxHuyen.Text = diaChiParts[2].Trim();       // Quận/Huyện
                        comboBoxTinhThanh.Text = diaChiParts[3].Trim();   // Tỉnh/Thành phố
                    }
                    else
                    {
                        // Thông báo lỗi nếu địa chỉ không đầy đủ hoặc rỗng
                        MessageBox.Show("Địa chỉ không đầy đủ hoặc không đúng định dạng.");
                        txt_Duong_Thon.Clear();
                        comboBoxXa.SelectedIndex = -1;
                        comboBoxHuyen.SelectedIndex = -1;
                        comboBoxTinhThanh.SelectedIndex = -1;
                    }
                }
                else
                {
                    // Nếu không tìm thấy nhà cung cấp, hiển thị thông báo
                  //  MessageBox.Show("Không tìm thấy thông tin nhà cung cấp.");
                }
            }
            else
            {
                // Nếu không có mục nào được chọn trong ComboBox, xóa dữ liệu TextBox và ComboBox
                txt_MaNhaCungCap.Clear();
                txt_Email_NhaCungCap.Clear();
                txt_SoDienThoai_NCC.Clear();
                txt_Duong_Thon.Clear();
                comboBoxXa.SelectedIndex = -1;
                comboBoxHuyen.SelectedIndex = -1;
                comboBoxTinhThanh.SelectedIndex = -1;
            }
        }



        private void LoadCombo_TenNhaCungCap()
        {
            try
            {
                // Khởi tạo BLL_NhaCungCap
                BLL_NhaCungCap bll_ncc = new BLL_NhaCungCap();

                // Lấy tất cả tên nhà cung cấp từ BLL
                List<NhaCungCap> danhSach_TenNCC = bll_ncc.GetAllNhaCungCapNames();

                // Gán danh sách vào ComboBox và thiết lập thuộc tính hiển thị
                cbo_TenNhaCungCap.DataSource = danhSach_TenNCC;
                cbo_TenNhaCungCap.DisplayMember = "TenNCC";  // Thuộc tính để hiển thị
                cbo_TenNhaCungCap.ValueMember = "MaNCC";     // Giá trị để lấy mã nhà cung cấp
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tải danh sách nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void ComboBoxHuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxHuyen.SelectedItem is ComboBoxItem selectedItem)
            {
                int districtId = (int)selectedItem.Value;
                await LoadXaAsync(districtId);
            }
        }

        private async void ComboBoxTinhThanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTinhThanh.SelectedItem is ComboBoxItem selectedItem)
            {
                int provinceId = (int)selectedItem.Value;
                await LoadHuyenAsync(provinceId);
            }
        }

        private void DataGV_CT_TrangThietBi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Kiểm tra nếu người dùng nhấn vào một dòng hợp lệ (không phải header)
                if (e.RowIndex >= 0)
                {
                    btn_themCTTTB.Enabled = true;
                    cbo_Ten_TTB.Enabled=true;
                    txt_soluong.Enabled = true;
                    btn_CapNhaCTTTB.Enabled = true;
                    btn_XoaCTTTB.Enabled = true;
                    btn_LamMoiCTTTB.Enabled = true;
                    // Sử dụng chỉ số cột thay vì tên cột
                    string maThietBi = dataGV_CT_TrangThietBi.Rows[e.RowIndex].Cells["MaThietBi"].Value.ToString(); // Cột 0 là MaThietBi
                    string maPhong = dataGV_CT_TrangThietBi.Rows[e.RowIndex].Cells["MaPhong"].Value.ToString(); // Cột 1 là MaPhong

                    // Gọi BLL để lấy thông tin chi tiết về thiết bị theo Mã thiết bị và Mã phòng
                    PhieuKTTrangThietBi phieu = bll_ttb.GetCT_TrangThietBiByMaThietBiAndMaPhong(maThietBi, maPhong);
                        
                    // Kiểm tra nếu có kết quả
                    if (phieu != null)
                    {
                        // Cập nhật thông tin vào các điều khiển (TextBox, ComboBox)
                        txt_MaTTB.Text = maThietBi;
                        cbo_MaPhong.SelectedValue = maPhong; // Cập nhật ComboBox mã phòng
                        cbo_Ten_TTB.SelectedValue = phieu.TenThietBi; // Cập nhật ComboBox tên thiết bị
                        txt_soluong.Text = phieu.SoLuong.ToString();
                        txt_TrangThai.Text = phieu.TrangThai; // Cập nhật trạng thái nếu cần, có thể thay đổi sau
                    }
                    else
                    {
                        // Nếu không tìm thấy thông tin
                        MessageBox.Show("Không tìm thấy thông tin thiết bị.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show("Lỗi khi xử lý dữ liệu: " + ex.Message);
            }
        }



        private void Cbo_Ten_TTB_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu có mục nào được chọn
            if (cbo_Ten_TTB.SelectedIndex != -1)
            {
                // Lấy tên trang thiết bị từ ComboBox
                string tenThietBi = cbo_Ten_TTB.SelectedValue.ToString();

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

        private void Cbo_MaPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
           if (cbo_MaPhong.SelectedIndex != -1)
    {
        string maPhong = cbo_MaPhong.SelectedValue.ToString(); // Lấy mã phòng đã chọn
        string tenLoaiPhong = bll_ttb.GetTenLoaiPhong(maPhong); // Gọi BLL để lấy tên loại phòng

        if (!string.IsNullOrEmpty(tenLoaiPhong))
        {
            txt_LoaiPhong.Text = tenLoaiPhong; // Hiển thị tên loại phòng

            //// Lấy danh sách trang thiết bị theo mã phòng (hoặc loại phòng)
            //var trangThietBiList = bll_ttb.GetTrangThietBiByMaPhong(maPhong); // Gọi BLL để lấy danh sách trang thiết bị

            //// Cập nhật ComboBox với danh sách trang thiết bị
            //cbo_Ten_TTB.Items.Clear(); // Xóa tất cả các item cũ trong cbo_Ten_TTB

            //if (trangThietBiList.Any()) // Kiểm tra nếu có trang thiết bị
            //{
            //    foreach (var thietBi in trangThietBiList)
            //    {
            //        cbo_Ten_TTB.Items.Add(thietBi); // Thêm từng trang thiết bị vào cbo_Ten_TTB
            //    }
            //}
            //else
            //{
            //    cbo_Ten_TTB.Items.Add("Không có trang thiết bị cho loại phòng này"); // Nếu không có trang thiết bị
            //}
        }
        else
        {
            txt_LoaiPhong.Text = "Không có thông tin loại phòng!"; // Nếu không tìm thấy tên loại phòng
            cbo_Ten_TTB.Items.Clear(); // Xóa tất cả các item cũ trong cbo_Ten_TTB
        }
    }
        }

        private void LoadMaPhongToComboBox()
        {
            // Lấy danh sách phòng từ BLL
            List<Phong> danhSachPhong = bll_ttb.GetAllMaPhong();

            // Gán danh sách mã phòng vào ComboBox
            cbo_MaPhong.DataSource = danhSachPhong;
            cbo_MaPhong.DisplayMember = "MaPhong"; // Hiển thị thuộc tính MaPhong trong ComboBox
            cbo_MaPhong.ValueMember = "MaPhong";   // Giá trị là thuộc tính MaPhong
        }
        private void DataGV_TrangThietBi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng có click vào một hàng hợp lệ không
            if (e.RowIndex >= 0)
            {
                btn_XoaTTB.Enabled = true;
                btn_SuaTTB.Enabled = true;
                btn_RefreshTTB.Enabled = true;
                // Lấy mã thiết bị từ cột đầu tiên của hàng được chọn
                string maThietBi = dataGV_TrangThietBi.Rows[e.RowIndex].Cells[0].Value.ToString();

                // Gọi hàm BLL để lấy thông tin thiết bị theo mã
                TrangThietBi thietBi = bll_ttb.GetTrangThietBiById(maThietBi);

                if (thietBi != null)
                {
                  

                    txt_Ma_TTB.Text = thietBi.MaThietBi;
                    txt_Ten_TTB.Text = thietBi.TenThietBi;

                    txt_trangthai_TTB.Text = thietBi.TrangThai;
                    // Lấy danh sách chi tiết thiết bị theo mã thiết bị từ BLL
                    List<CTTTB_DTO> danhSachChiTiet = bll_ttb.GetChiTietTrangThietBiByMaThietBi(maThietBi);

                    // Gán danh sách vào DataGridView
                    dataGV_CT_TrangThietBi.DataSource = danhSachChiTiet;

                    // Đặt lại tiêu đề cột
                    dataGV_CT_TrangThietBi.Columns["MaThietBi"].HeaderText = "Mã Thiết Bị";
                    dataGV_CT_TrangThietBi.Columns["TenThietBi"].HeaderText = "Tên Thiết Bị";
                    dataGV_CT_TrangThietBi.Columns["MaPhong"].HeaderText = "Phòng";
                    dataGV_CT_TrangThietBi.Columns["SoLuong"].HeaderText = "Số Lượng";
                    dataGV_CT_TrangThietBi.Columns["TrangThai"].HeaderText = "Trạng Thái";

                }
            }
        }

        private async void QuanLyTrangThietBi_Load(object sender, EventArgs e)
        {
            LoadCboMaDonNhap();
            LoadCboMaNCC();
            LoadCboMaNhanVien();
            LoadCboSapXep();
            LoadNhaCungCap();

            LoadDuLieuTrangThietBI();
            LoadMaPhongToComboBox();
            LoadComBo_TenThietBi();
            LoadDataCT_TrangThietBi();
            await LoadTinhThanhAsync();
            LoadCombo_TenNhaCungCap();
            SetupDataGridView_DS_Nhap_TTB();
            VoHieuHoa(false);
            VoHieuHoaQLNhap();
            //
            //QL_Nhap
            LoadThongTinDonNhap();
       //   SetupDataGridView_DonNhap(); // Cấu hình DataGridView
         //  LoadDataDonNhap();

          //  SetupDataGridView_DonNhap();    // Cấu hình DataGridView đơn nhập
           SetupDataGridView_CTDonNhap(); // Cấu hình DataGridView chi tiết đơn nhập
          //  LoadDataDonNhap();
        }
      
        private void LoadComBo_TenThietBi()
        {
            cbo_Ten_TTB.DataSource = bll_ttb.GetAllTrangThietBi();
            cbo_Ten_TTB.ValueMember = "TenThietBi";
            cbo_Ten_TTB.DisplayMember = "TenThietBi";
        }
        private void LoadDuLieuTrangThietBI()
        {
            // Cấu hình các cột cho DataGridView
            dataGV_TrangThietBi.AutoGenerateColumns = false;
            dataGV_TrangThietBi.Columns.Clear();

            // Tạo các cột
            DataGridViewTextBoxColumn colMaThietBi = new DataGridViewTextBoxColumn();
        colMaThietBi.HeaderText = "Mã Thiết Bị";
            colMaThietBi.DataPropertyName = "MaThietBi"; // Tên thuộc tính trong lớp TrangThietBi
            dataGV_TrangThietBi.Columns.Add(colMaThietBi);


            DataGridViewTextBoxColumn colTenThietBi = new DataGridViewTextBoxColumn();
        colTenThietBi.HeaderText = "Tên Thiết Bị";
            colTenThietBi.DataPropertyName = "TenThietBi"; // Tên thuộc tính trong lớp TrangThietBi
            dataGV_TrangThietBi.Columns.Add(colTenThietBi);

            DataGridViewTextBoxColumn colSoLuong = new DataGridViewTextBoxColumn();
        colSoLuong.HeaderText = "Số lượng";
            colSoLuong.DataPropertyName = "SoLuong"; // Tên thuộc tính trong lớp TrangThietBi
            dataGV_TrangThietBi.Columns.Add(colSoLuong);

            DataGridViewTextBoxColumn colTrangThai = new DataGridViewTextBoxColumn();
        colTrangThai.HeaderText = "Trạng Thái";
            colTrangThai.DataPropertyName = "TrangThai"; // Tên thuộc tính trong lớp TrangThietBi
            dataGV_TrangThietBi.Columns.Add(colTrangThai);

            // Lấy danh sách trang thiết bị từ BLL
            List<TrangThietBi> danhSachThietBi = bll_ttb.GetAllTrangThietBi();

        // Gán danh sách vào DataGridView
        dataGV_TrangThietBi.DataSource = danhSachThietBi;
        }

   private void LoadDataCT_TrangThietBi()
{
    try
    {
        List<CTTTB_DTO> danhSachChiTiet = bll_ttb.GetAllChiTietTrangThietBiWithTen();
        dataGV_CT_TrangThietBi.DataSource = danhSachChiTiet;

        // Đặt lại tiêu đề cột
        dataGV_CT_TrangThietBi.Columns["MaThietBi"].HeaderText = "Mã Thiết Bị";
        dataGV_CT_TrangThietBi.Columns["TenThietBi"].HeaderText = "Tên Thiết Bị";
        dataGV_CT_TrangThietBi.Columns["MaPhong"].HeaderText = "Phòng";
        dataGV_CT_TrangThietBi.Columns["SoLuong"].HeaderText = "Số Lượng";
        dataGV_CT_TrangThietBi.Columns["TrangThai"].HeaderText = "Trạng Thái";

        // Sắp xếp lại thứ tự hiển thị của các cột
        
                dataGV_CT_TrangThietBi.Columns["MaPhong"].DisplayIndex = 0; // Phòng ở vị trí thứ 3
                dataGV_CT_TrangThietBi.Columns["TenThietBi"].DisplayIndex = 1; // Tên thiết bị hiển thị đầu tiên
                dataGV_CT_TrangThietBi.Columns["SoLuong"].DisplayIndex = 2; // Số lượng ở vị trí thứ 4
                dataGV_CT_TrangThietBi.Columns["TrangThai"].DisplayIndex = 4; // Trạng thái ở vị trí thứ 5
                dataGV_CT_TrangThietBi.Columns["MaThietBi"].DisplayIndex = 3; // Mã thiết bị ở vị trí thứ 2
            }
    catch (Exception ex)
    {
        MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}


       

        private void txt_maSinhVien_TextChanged_2(object sender, EventArgs e)
        {

        }

        private async Task LoadTinhThanhAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = "https://provinces.open-api.vn/api/p/";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    List<TinhThanh> data = JsonConvert.DeserializeObject<List<TinhThanh>>(responseBody);

                    comboBoxTinhThanh.Items.Clear();
                    foreach (var item in data)
                    {
                        comboBoxTinhThanh.Items.Add(new ComboBoxItem { Text = item.Name, Value = item.Code });
                    }

                    comboBoxTinhThanh.DisplayMember = "Text";
                    comboBoxTinhThanh.ValueMember = "Value";
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private async Task LoadHuyenAsync(int provinceId)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = $"https://provinces.open-api.vn/api/p/{provinceId}?depth=2";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    var provinceData = JsonConvert.DeserializeObject<TinhThanhChiTiet>(responseBody);

                    comboBoxHuyen.Items.Clear();
                    foreach (var item in provinceData.Districts)
                    {
                        comboBoxHuyen.Items.Add(new ComboBoxItem { Text = item.Name, Value = item.Code });
                    }

                    comboBoxHuyen.DisplayMember = "Text";
                    comboBoxHuyen.ValueMember = "Value";
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private async Task LoadXaAsync(int districtId)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = $"https://provinces.open-api.vn/api/d/{districtId}?depth=2";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    var districtData = JsonConvert.DeserializeObject<HuyenChiTiet>(responseBody);

                    comboBoxXa.Items.Clear();
                    foreach (var item in districtData.Wards)
                    {
                        comboBoxXa.Items.Add(new ComboBoxItem { Text = item.Name, Value = item.Code });
                    }

                    comboBoxXa.DisplayMember = "Text";
                    comboBoxXa.ValueMember = "Value";
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
        private void LoadDataDonNhap()
        {
            // Gọi BLL để lấy dữ liệu
          
            List<DonNhapDTO> danhSachDonNhap = bll_ttb.GetAllDonNhapWithDetails();

            // Gán dữ liệu vào DataGridView
            dataGV_QLN_DSDonNhap.DataSource = danhSachDonNhap;
        }

        //Quan_ly_Nhap
        private void SetupDataGridView_DonNhap()
        {
            // Xóa cột cũ
            dataGV_QLN_DSDonNhap.Columns.Clear();

            // Tạo và cấu hình các cột
            dataGV_QLN_DSDonNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaDonNhap",
                HeaderText = "Mã Đơn Nhập",
                DataPropertyName = "MaDonNhap"
            });

            dataGV_QLN_DSDonNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgayNhap",
                HeaderText = "Ngày Nhập",
                DataPropertyName = "NgayNhap"
            });

            dataGV_QLN_DSDonNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TongTien",
                HeaderText = "Tổng Tiền",
                DataPropertyName = "TongTien"
            });

            dataGV_QLN_DSDonNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TrangThai",
                HeaderText = "Trạng Thái",
                DataPropertyName = "TrangThai"
            });

            dataGV_QLN_DSDonNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaNhanVien",
                HeaderText = "Mã Nhân Viên",
                DataPropertyName = "MaNhanVien"
            });

            dataGV_QLN_DSDonNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenNhanVien",
                HeaderText = "Tên Nhân Viên",
                DataPropertyName = "TenNhanVien"
            });

            dataGV_QLN_DSDonNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaNCC",
                HeaderText = "Mã Nhà Cung Cấp",
                DataPropertyName = "MaNCC"
            });

            dataGV_QLN_DSDonNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenNCC",
                HeaderText = "Tên Nhà Cung Cấp",
                DataPropertyName = "TenNCC"
            });

            // Tùy chỉnh thuộc tính hiển thị
            dataGV_QLN_DSDonNhap.AutoGenerateColumns = false;
            dataGV_QLN_DSDonNhap.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGV_QLN_DSDonNhap.MultiSelect = false;
        }


        private void SetupDataGridView_CTDonNhap()
        {
            // Xóa cột cũ
            dataGV_QLN_CTDN.Columns.Clear();

            // Tạo và cấu hình các cột
            dataGV_QLN_CTDN.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaDonNhap",
                HeaderText = "Mã Đơn Nhập",
                DataPropertyName = "MaDonNhap"
            });

            dataGV_QLN_CTDN.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaThietBi",
                HeaderText = "Mã Thiết Bị",
                DataPropertyName = "MaThietBi"
            });

            dataGV_QLN_CTDN.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenThietBi",
                HeaderText = "Tên Thiết Bị",
                DataPropertyName = "TenThietBi"
            });

            dataGV_QLN_CTDN.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoLuong",
                HeaderText = "Số Lượng",
                DataPropertyName = "SoLuong"
            });

            dataGV_QLN_CTDN.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DonGia",
                HeaderText = "Đơn Giá",
                DataPropertyName = "DonGia"
            });

            dataGV_QLN_CTDN.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ThanhTien",
                HeaderText = "Thành Tiền",
                DataPropertyName = "ThanhTien"
            });

            dataGV_QLN_CTDN.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TrangThai",
                HeaderText = "Trạng thái",
                DataPropertyName = "TrangThai"
            });

            // Cấu hình chung
            dataGV_QLN_CTDN.AutoGenerateColumns = false;
            dataGV_QLN_CTDN.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGV_CT_TrangThietBi.MultiSelect = false;
        }

        private void LoadChiTietDonNhap(string maDonNhap)
        {
            // Gọi BLL để lấy danh sách chi tiết đơn nhập theo mã đơn nhập
            List<ChiTietDonNhap_DTO> danhSachChiTiet = bll_ttb.GetChiTietDonNhapByMaDonNhap(maDonNhap);

            // Gán dữ liệu vào DataGridView
            dataGV_QLN_CTDN.DataSource = danhSachChiTiet;
        }

        private void LoadThongTinDonNhap()
        {
            try
            {
                // Lấy dữ liệu từ BLL, bao gồm thông tin từ bảng chính và khóa ngoại
                var danhSachDonNhap = bll_ttb.GetAllThongTinDonNhap(); // BLL trả về danh sách với thông tin từ các bảng liên quan

                // Gán dữ liệu vào DataGridView
                dataGV_QLN_DSDonNhap.DataSource = danhSachDonNhap;

                // Đặt tiêu đề hiển thị cho các cột
                dataGV_QLN_DSDonNhap.Columns["MaDonNhap"].HeaderText = "Mã Đơn Nhập";
                dataGV_QLN_DSDonNhap.Columns["NgayNhap"].HeaderText = "Ngày Nhập";
                dataGV_QLN_DSDonNhap.Columns["TongTien"].HeaderText = "Tổng Tiền";
                dataGV_QLN_DSDonNhap.Columns["TrangThai"].HeaderText = "Trạng Thái";

                // Cột liên kết với bảng Nhà cung cấp
                dataGV_QLN_DSDonNhap.Columns["MaNCC"].HeaderText = "Mã NCC";
                dataGV_QLN_DSDonNhap.Columns["TenNCC"].HeaderText = "Tên Nhà Cung Cấp";
                dataGV_QLN_DSDonNhap.Columns["DiaChi"].HeaderText = "Địa Chỉ NCC";

                // Cột liên kết với bảng Nhân viên
                dataGV_QLN_DSDonNhap.Columns["MaNhanVien"].HeaderText = "Mã Nhân Viên";
                dataGV_QLN_DSDonNhap.Columns["TenNhanVien"].HeaderText = "Họ và Tên NV";
                dataGV_QLN_DSDonNhap.Columns["ChucVu"].HeaderText = "Chức Vụ";
                // Ẩn cột không cần thiết (nếu có)
                dataGV_QLN_DSDonNhap.Columns["MaNhanVien"].Visible = false;
                dataGV_QLN_DSDonNhap.Columns["MaNCC"].Visible = false;
                dataGV_QLN_DSDonNhap.Columns["DiaChi"].Visible = false;
                dataGV_QLN_DSDonNhap.Columns["ChucVu"].Visible = false;
                dataGV_QLN_DSDonNhap.Columns["TrangThai"].Visible = false;

                // Sắp xếp thứ tự hiển thị các cột
                dataGV_QLN_DSDonNhap.Columns["MaDonNhap"].DisplayIndex = 0; // Mã đơn nhập
                dataGV_QLN_DSDonNhap.Columns["TenNCC"].DisplayIndex = 1;    // Tên nhà cung cấp
                dataGV_QLN_DSDonNhap.Columns["DiaChi"].DisplayIndex = 2;    // Địa chỉ nhà cung cấp
                dataGV_QLN_DSDonNhap.Columns["NgayNhap"].DisplayIndex = 3;  // Ngày nhập
                dataGV_QLN_DSDonNhap.Columns["TongTien"].DisplayIndex = 4;  // Tổng tiền
                dataGV_QLN_DSDonNhap.Columns["TrangThai"].DisplayIndex = 5; // Trạng thái
                dataGV_QLN_DSDonNhap.Columns["TenNhanVien"].DisplayIndex = 6;   // Họ và tên nhân viên
                dataGV_QLN_DSDonNhap.Columns["ChucVu"].DisplayIndex = 7;    // Chức vụ
                dataGV_QLN_DSDonNhap.Columns["MaNhanVien"].DisplayIndex = 8;// Mã nhân viên
                dataGV_QLN_DSDonNhap.Columns["MaNCC"].DisplayIndex = 9;     // Mã nhà cung cấp


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DataGV_QLN_DSDonNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu người dùng nhấp vào một dòng hợp lệ (không phải header)
            if (e.RowIndex >= 0)
            {
                // Lấy dòng được chọn
                var selectedRow = dataGV_QLN_DSDonNhap.Rows[e.RowIndex];

                // Gán dữ liệu từ các cột của dòng vào các TextBox
                txt_QLN_MaDonNhap.Text = selectedRow.Cells["MaDonNhap"].Value.ToString();
                cbo_QLN_maNCC.SelectedValue = selectedRow.Cells["MaNCC"].Value.ToString();
                txt_QLN_tenNCC.Text = selectedRow.Cells["TenNCC"].Value.ToString();
                txt_QLN_diaChi.Text = selectedRow.Cells["DiaChi"].Value.ToString();
                picker__QLN_ngayNhap.Value = Convert.ToDateTime(selectedRow.Cells["NgayNhap"].Value);
                txt_QLN_tongTien.Text = selectedRow.Cells["TongTien"].Value.ToString();
                txt_QLN_Trangthai.Text = selectedRow.Cells["TrangThai"].Value.ToString();
                txt_QLN_maNV.Text = selectedRow.Cells["MaNhanVien"].Value.ToString();
                txt_QLN_hotenNV.Text = selectedRow.Cells["TenNhanVien"].Value.ToString();
                txt_QLN_chucVu.Text = selectedRow.Cells["ChucVu"].Value.ToString();
                string maDonNhap = selectedRow.Cells["MaDonNhap"].Value.ToString();

                // Gọi phương thức để tải chi tiết đơn nhập
                LoadChiTietDonNhap(maDonNhap);
            }

            if (dataGV_QLN_DSDonNhap.SelectedRows.Count > 0)
            {
                // Lấy mã đơn nhập từ hàng được chọn
                string maDonNhap = dataGV_QLN_DSDonNhap.SelectedRows[0].Cells["MaDonNhap"].Value.ToString();

                // Gọi phương thức để tải chi tiết đơn nhập
                LoadChiTietDonNhap(maDonNhap);
            }
        }

        private void DataGV_QLN_CTDN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu có dòng được chọn
            if (dataGV_QLN_CTDN.SelectedRows.Count > 0)
            {
                // Lấy dữ liệu từ dòng được chọn
                var selectedRow = dataGV_QLN_CTDN.SelectedRows[0];
                txt_donnhap_maTB.Text = selectedRow.Cells["MaDonNhap"].Value?.ToString();
                txt_donnhap_maTB.Text = selectedRow.Cells["MaThietBi"].Value?.ToString();
                txt_donnhap_tenTB.Text = selectedRow.Cells["TenThietBi"].Value?.ToString();
                txt_donnhap_sl.Text = selectedRow.Cells["SoLuong"].Value?.ToString();
                txt_donhap_dongia.Text = selectedRow.Cells["DonGia"].Value?.ToString();
                txt_TrangThai.Text = selectedRow.Cells["TrangThai"].Value?.ToString();
                // txt.Text = selectedRow.Cells["ThanhTien"].Value?.ToString();
            }
        }

        private void LoadCboMaDonNhap()
        {
            cbo_Loc_QLN_maDonNhap.Items.Add("Tất cả"); // Tùy chọn hiển thị tất cả
            cbo_Loc_QLN_maDonNhap.DataSource = bll_ttb.GetAllDonNhapWithDetails();
            cbo_Loc_QLN_maDonNhap.ValueMember = "MaDonNhap";
            cbo_Loc_QLN_maDonNhap.DisplayMember = "MaDonNhap";
         //   cbo_Loc_QLN_maDonNhap.SelectedIndex = 0;

        }

        private void LoadCboMaNCC()
        {
            cbo_Loc_QLN_NCC.DataSource = bll_ncc.GetAllNhaCungCapNames();
            cbo_Loc_QLN_NCC.ValueMember = "MaNCC";
            cbo_Loc_QLN_NCC.DisplayMember = "TenNCC";
        }

        private void LoadCboMaNhanVien()
        {
            cbo_Loc_QLN_MaNV.DataSource = bll_nv.GetAllMaNhanVien();
            cbo_Loc_QLN_MaNV.ValueMember = "MaNhanVien";
            cbo_Loc_QLN_MaNV.DisplayMember = "HoTen";
        }
        private void LoadCboSapXep()
        {
            // Thêm các tùy chọn vào ComboBox
            cbo_sapXep_QLN.Items.Add("Tổng tiền tăng dần");
            cbo_sapXep_QLN.Items.Add("Tỏng tiền giảm dần");
            cbo_sapXep_QLN.Items.Add("Ngày nhập mới nhất");
            cbo_sapXep_QLN.Items.Add("Ngày nhập cũ nhất");
            // Đặt giá trị mặc định
            cbo_sapXep_QLN.SelectedIndex = 0; // "Tăng dần"
        }
        private void Cbo_sapXep_QLN_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy danh sách dữ liệu hiện tại
            var danhSachDonNhap = bll_ttb.GetAllThongTinDonNhap();

            // Kiểm tra tùy chọn sắp xếp
            switch (cbo_sapXep_QLN.SelectedIndex)
            {
                case 0: // Tổng tiền tăng dần
                    danhSachDonNhap = danhSachDonNhap.OrderBy(d => d.TongTien).ToList();
                    break;
                case 1: // Tổng tiền giảm dần
                    danhSachDonNhap = danhSachDonNhap.OrderByDescending(d => d.TongTien).ToList();
                    break;
                case 2: // Ngày nhập mới nhất
                    danhSachDonNhap = danhSachDonNhap.OrderByDescending(d => d.NgayNhap).ToList();
                    break;
                case 3: // Ngày nhập cũ nhất
                    danhSachDonNhap = danhSachDonNhap.OrderBy(d => d.NgayNhap).ToList();
                    break;
                default:
                    break;
            }

            // Cập nhật dữ liệu vào DataGridView
            dataGV_QLN_DSDonNhap.DataSource = danhSachDonNhap;
        }


        private void Cbo_Loc_QLN_MaNV_SelectedValueChanged(object sender, EventArgs e)
        {
            // Lấy mã nhân viên được chọn
            string maNhanVien = cbo_Loc_QLN_MaNV.SelectedValue.ToString();

            // Lấy dữ liệu từ nguồn
            var danhSachDonNhap = bll_ttb.GetAllThongTinDonNhap();

            if (maNhanVien == "Tất cả")
            {
                // Hiển thị toàn bộ nếu chọn "Tất cả"
                dataGV_QLN_DSDonNhap.DataSource = danhSachDonNhap;
            }
            else
            {
                // Lọc dữ liệu theo mã nhân viên
                var danhSachLoc = danhSachDonNhap.Where(d => d.MaNhanVien == maNhanVien).ToList();
                dataGV_QLN_DSDonNhap.DataSource = danhSachLoc;
            }
        }

        private void Cbo_Loc_QLN_NCC_SelectedValueChanged(object sender, EventArgs e)
        {
            txt_QLN_tenNCC.Enabled = true;
            txt_QLN_diaChi.Enabled = true;
            // Lấy giá trị được chọn từ ComboBox
            string tenNCC = cbo_Loc_QLN_NCC.SelectedValue.ToString();

            // Lấy dữ liệu gốc từ BLL
            var danhSachDonNhap = bll_ttb.GetAllThongTinDonNhap();

            // Nếu chọn "Tất cả", hiển thị toàn bộ dữ liệu
            if (tenNCC == "Tất cả")
            {
                dataGV_QLN_DSDonNhap.DataSource = danhSachDonNhap;
            }
            else
            {
                // Lọc dữ liệu theo Mã Đơn Nhập
                var danhSachLoc = danhSachDonNhap.Where(d => d.MaNCC == tenNCC).ToList();

                // Gán dữ liệu đã lọc vào DataGridView
                dataGV_QLN_DSDonNhap.DataSource = danhSachLoc;
            }
        }

        private void Cbo_Loc_QLN_maDonNhap_SelectedValueChanged(object sender, EventArgs e)
        {
            //// Lấy giá trị được chọn từ ComboBox
            //string maDonNhapDuocChon = cbo_Loc_QLN_maDonNhap.SelectedValue.ToString();

            //// Lấy dữ liệu gốc từ BLL
            //var danhSachDonNhap = bll_ttb.GetAllThongTinDonNhap();

            //// Nếu chọn "Tất cả", hiển thị toàn bộ dữ liệu
            //if (maDonNhapDuocChon == "Tất cả")
            //{
            //    dataGV_QLN_DSDonNhap.DataSource = danhSachDonNhap;
            //}
            //else
            //{
            //    // Lọc dữ liệu theo Mã Đơn Nhập
            //    var danhSachLoc = danhSachDonNhap.Where(d => d.MaDonNhap == maDonNhapDuocChon).ToList();

            //    // Gán dữ liệu đã lọc vào DataGridView
            //    dataGV_QLN_DSDonNhap.DataSource = danhSachLoc;
            //}
        }

        private void comboBoxXa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        ///Thong ____ Ke
        ///

    }
}
