using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BLL;
using OfficeOpenXml;
using System.IO;

namespace GUI
{
    public partial class XepPhong : Form
    {
        BLL_QL_SinhVien bllSinhVien= new BLL_QL_SinhVien();
        BLL_QL_Phong bllPhong = new BLL_QL_Phong(); 
        public XepPhong()
        {
            InitializeComponent();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            this.Load += XepPhong_Load;
           // btn_XepPhong.Click += Btn_XepPhong_Click;
            btn_LoadSinhVien.Click += Btn_LoadSinhVien_Click;
            btn_XepPhong_SV.Click += Btn_XepPhong_SV_Click;
            btn_LuuTru_DK.Click += Btn_LuuTru_DK_Click;
            dataGV_XepPhong.CellClick += DataGV_XepPhong_CellClick;
        }

        private void DataGV_XepPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Kiểm tra nếu người dùng nhấn vào một ô hợp lệ (không phải header hoặc ngoài phạm vi)
                if (e.RowIndex >= 0)
                {
                    // Lấy giá trị từ các cột của dòng đã chọn
                    var maSV = dataGV_XepPhong.Rows[e.RowIndex].Cells["MSSV"].Value.ToString();
                    var hoTen = dataGV_XepPhong.Rows[e.RowIndex].Cells["HoTen"].Value.ToString();
                    var gioiTinh = dataGV_XepPhong.Rows[e.RowIndex].Cells["gioitinh"].Value.ToString();
                    var soTang = dataGV_XepPhong.Rows[e.RowIndex].Cells["sotang"].Value.ToString();
                    var maPhong = dataGV_XepPhong.Rows[e.RowIndex].Cells["maphong"].Value.ToString();

                    // Gán giá trị vào các TextBox tương ứng
                    txt_MaSV.Text = maSV;
                    txt_HoTenSV.Text = hoTen;
                    txt_GioiTinh.Text = gioiTinh;
                    txt_SoTang.Text = soTang;
                    txt_maPhong.Text = maPhong;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Btn_LuuTru_DK_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo danh sách sinh viên và danh sách đăng ký phòng
                List<SinhVien> sinhVienList = new List<SinhVien>();
                List<DangKyPhong> dangKyPhongList = new List<DangKyPhong>();

                // Lặp qua tất cả các hàng trong DataGridView SinhViên
                foreach (DataGridViewRow row in dataGV_SinhVien.Rows)
                {
                    if (row.Cells["MSSV"].Value != null)
                    {
                        // Tạo đối tượng SinhVien từ dữ liệu trong DataGridView
                        SinhVien sinhVienDTO = new SinhVien
                        {
                            MaSinhVien = row.Cells["MSSV"].Value.ToString(),
                            HoTen = row.Cells["HoTen"].Value.ToString(),
                            CCCD = row.Cells["CCCD"].Value.ToString(),
                            Email = row.Cells["Email"].Value.ToString(),
                            NgaySinh = Convert.ToDateTime(row.Cells["NgaySinh"].Value),
                            GioiTinh = row.Cells["GioiTinh"].Value.ToString(),
                            HoKhauThuongTru = row.Cells["HoKhau"].Value.ToString(),
                            NoiSinh = row.Cells["NoiSinh"].Value.ToString(),
                            GhiChu = row.Cells["GhiChu"].Value.ToString()
                        };

                        // Kiểm tra và chuyển đổi giá trị "TruongPhong" thành Boolean an toàn
                        bool truongPhong = false;
                        if (row.Cells["TruongPhong"].Value != null)
                        {
                            bool.TryParse(row.Cells["TruongPhong"].Value.ToString(), out truongPhong);
                        }
                        sinhVienDTO.TruongPhong = truongPhong; // Gán giá trị vào đối tượng sinhVienDTO

                        // Lưu sinh viên vào danh sách sinh viên
                        sinhVienList.Add(sinhVienDTO);

                        // Lặp qua tất cả các hàng trong DataGridView XepPhong để tìm các phòng phù hợp với sinh viên này
                        foreach (DataGridViewRow rowPhong in dataGV_XepPhong.Rows)
                        {
                            if (rowPhong.Cells["MSSV"].Value != null && rowPhong.Cells["MSSV"].Value.ToString() == sinhVienDTO.MaSinhVien)
                            {
                                // Lấy mã phòng từ cột "MaPhong" trong DataGridView XepPhong
                                string maPhong = rowPhong.Cells["MaPhong"].Value.ToString(); // Lấy mã phòng từ cột "MaPhong"

                                // Tạo đối tượng DangKyPhong từ dữ liệu trong DataGridView
                                DangKyPhong dangKyPhongDTO = new DangKyPhong
                                {
                                    MaSinhVien = sinhVienDTO.MaSinhVien, // Đảm bảo mã sinh viên trùng khớp
                                    MaPhong = maPhong,                  // Gán mã phòng từ dữ liệu trong DataGridView
                                    NgayDK = DateTime.Now,
                                    NgayBD = DateTime.Now.AddDays(1),    // Ví dụ ngày bắt đầu
                                    NgayKT = DateTime.Now.AddDays(30),   // Ví dụ ngày kết thúc
                                    NV1 = Convert.ToInt32(rowPhong.Cells["nv1"].Value),
                                    NV2 = Convert.ToInt32(rowPhong.Cells["nv2"].Value),
                                    NV3 = Convert.ToInt32(rowPhong.Cells["nv3"].Value),
                                    TrangThai = "Đã đăng ký"
                                };

                                // Lưu đăng ký phòng vào danh sách dangKyPhongList
                                dangKyPhongList.Add(dangKyPhongDTO);
                            }
                        }
                    }
                }

                // Gọi hàm để thêm tất cả sinh viên và đăng ký phòng vào cơ sở dữ liệu
                string result = bllPhong.ThemSinhVienVaDangKyPhong(sinhVienList, dangKyPhongList);
                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show("Lỗi khi lưu trữ đăng ký: " + ex.Message);
            }
        }







        private void Btn_XepPhong_SV_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Lấy danh sách sinh viên từ DataGridView
                List<SinhVienDTO> danhSachSinhVien = new List<SinhVienDTO>();

                foreach (DataGridViewRow row in dataGV_SinhVien.Rows)
                {
                    if (row.IsNewRow) continue; // Bỏ qua dòng mới chưa nhập dữ liệu

                    // Kiểm tra từng ô trước khi sử dụng
                    string mssv = row.Cells["MSSV"]?.Value?.ToString() ?? string.Empty;
                    string hoTen = row.Cells["HoTen"]?.Value?.ToString() ?? string.Empty;
                    string gioiTinh = row.Cells["GioiTinh"]?.Value?.ToString() ?? string.Empty;
                    int nv1 = int.TryParse(row.Cells["NV1"]?.Value?.ToString(), out int result1) ? result1 : 0;
                    int nv2 = int.TryParse(row.Cells["NV2"]?.Value?.ToString(), out int result2) ? result2 : 0;
                    int nv3 = int.TryParse(row.Cells["NV3"]?.Value?.ToString(), out int result3) ? result3 : 0;

                    // Tạo đối tượng sinh viên nếu tất cả dữ liệu cần thiết đều hợp lệ
                    SinhVienDTO sinhVien = new SinhVienDTO
                    {
                        MSSV = mssv,
                        HoTen = hoTen,
                        GioiTinh = gioiTinh,
                        nv1 = nv1,
                        nv2 = nv2,
                        nv3 = nv3
                    };

                    danhSachSinhVien.Add(sinhVien);
                }

                // 2. Lấy danh sách phòng từ BLL
                List<Phong> danhSachPhong = bllPhong.LayDSPhongThuong();
                if (danhSachPhong == null || danhSachPhong.Count == 0)
                {
                    MessageBox.Show("Không có phòng nào trong danh sách phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Gọi hàm phân phòng bằng KMeans
                Dictionary<string, List<SinhVienDTO>> danhSachXepPhong = bllPhong.PhanPhongBangKMeans(danhSachSinhVien, danhSachPhong);

                if (danhSachXepPhong == null || danhSachXepPhong.Count == 0)
                {
                    MessageBox.Show("Kết quả phân phòng không có sinh viên nào được xếp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 4. Tạo và cập nhật DataGridView để hiển thị kết quả phân phòng
                dataGV_XepPhong.Rows.Clear(); // Xóa tất cả các dòng cũ trong DataGridView trước khi thêm mới

                foreach (var item in danhSachXepPhong)
                {
                    string maPhong = item.Key; // Mã phòng
                    foreach (var sinhVien in item.Value)
                    {
                        // Thêm một dòng mới vào DataGridView
                        dataGV_XepPhong.Rows.Add(
                            sinhVien.MSSV,
                            sinhVien.HoTen,
                            sinhVien.GioiTinh,
                             GetTangForPhong(maPhong),// Số tầng (Cần phải tạo một hàm lấy tầng từ mã phòng)
                             maPhong ,// Mã phòng
                            sinhVien.nv1,
                            sinhVien.nv2,
                            sinhVien.nv3
                           
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                // Hiển thị chi tiết lỗi
                MessageBox.Show($"Lỗi: {ex.Message}\nChi tiết: {ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Btn_LoadSinhVien_Click(object sender, EventArgs e)
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
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;
                        var colCount = worksheet.Dimension.Columns;

                        // Hiển thị số lượng cột trong MessageBox
                        MessageBox.Show("Số lượng cột: " + colCount.ToString());
                        MessageBox.Show("Số lượng hàng: " + rowCount.ToString());
                        MessageBox.Show("số cột datagv " + dataGV_SinhVien.Columns.Count);
                        // Kiểm tra số cột trong file Excel và DataGridView
                        if (colCount != dataGV_SinhVien.Columns.Count)
                        {
                            MessageBox.Show("Số lượng cột trong file Excel không khớp với số cột trong DataGridView.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Xóa dữ liệu cũ trong DataGridView
                        dataGV_SinhVien.Rows.Clear();

                        for (int row = 5; row <= rowCount; row++) // Bắt đầu từ dòng 2 để bỏ qua header
                        {
                            // Đọc các giá trị từ mỗi cột trong dòng
                            string mssv = worksheet.Cells[row, 1].Text.Trim();
                            string hoTen = worksheet.Cells[row, 2].Text.Trim();
                            string cccd = worksheet.Cells[row, 3].Text.Trim();
                            string email = worksheet.Cells[row, 4].Text.Trim();
                            string ngaySinh = worksheet.Cells[row, 5].Text.Trim();
                            string gioiTinh = worksheet.Cells[row, 6].Text.Trim();
                            string hoKhau = worksheet.Cells[row, 7].Text.Trim();
                            string noiSinh = worksheet.Cells[row, 8].Text.Trim();
                            string ghiChu = worksheet.Cells[row, 9].Text.Trim();
                            string truongPhong = worksheet.Cells[row, 10].Text.Trim();
                            string hinhCCCDTruoc = worksheet.Cells[row, 11].Text.Trim();
                            string hinhCCCDSau = worksheet.Cells[row, 12].Text.Trim();
                            string hinhNhanDien = worksheet.Cells[row, 13].Text.Trim();
                            string nv1 = worksheet.Cells[row, 14].Text.Trim();
                            string nv2 = worksheet.Cells[row, 15].Text.Trim();
                            string nv3 = worksheet.Cells[row, 16].Text.Trim();

                            // Chuyển đổi giới tính
                           // int gioiTinhInt = (gioiTinh == "Nam") ? 1 : (gioiTinh == "Nữ" ? 0 : -1);

                            // Chuyển đổi NV1
                            int nv1Int = (nv1 == "Yên tĩnh") ? 1 : (nv1 == "Náo nhiệt" ? 2 : 0);

                            // Chuyển đổi NV2
                            int nv2Int = (nv2 == "Thức khuya") ? 1 : (nv2 == "Dậy sớm" ? 2 : 0);

                            // Chuyển đổi NV3
                            int nv3Int = (nv3 == "Thích thể thao") ? 1 : (nv3 == "Thích đọc sách" ? 2 : (nv3 == "Thích tham gia hoạt động ngoại khóa" ? 3 : 0));

                            // Thêm dòng vào DataGridView
                            DataGridViewRow rowData = new DataGridViewRow();
                            rowData.Cells.Add(new DataGridViewTextBoxCell { Value = mssv });
                            rowData.Cells.Add(new DataGridViewTextBoxCell { Value = hoTen });
                            rowData.Cells.Add(new DataGridViewTextBoxCell { Value = cccd });
                            rowData.Cells.Add(new DataGridViewTextBoxCell { Value = email });
                            rowData.Cells.Add(new DataGridViewTextBoxCell { Value = ngaySinh });
                            rowData.Cells.Add(new DataGridViewTextBoxCell { Value = gioiTinh });
                            rowData.Cells.Add(new DataGridViewTextBoxCell { Value = hoKhau });
                            rowData.Cells.Add(new DataGridViewTextBoxCell { Value = noiSinh });
                            rowData.Cells.Add(new DataGridViewTextBoxCell { Value = ghiChu });
                            rowData.Cells.Add(new DataGridViewTextBoxCell { Value = truongPhong });
                            rowData.Cells.Add(new DataGridViewTextBoxCell { Value = hinhCCCDTruoc });
                            rowData.Cells.Add(new DataGridViewTextBoxCell { Value = hinhCCCDSau });
                            rowData.Cells.Add(new DataGridViewTextBoxCell { Value = hinhNhanDien });
                            rowData.Cells.Add(new DataGridViewTextBoxCell { Value = nv1Int });
                            rowData.Cells.Add(new DataGridViewTextBoxCell { Value = nv2Int });
                            rowData.Cells.Add(new DataGridViewTextBoxCell { Value = nv3Int });

                            // Thêm dòng vào DataGridView
                            dataGV_SinhVien.Rows.Add(rowData);
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

        private void DanhSachXepPhong()
        {
            // Kiểm tra nếu DataGridView chưa có cột, thì thêm cột
            if (dataGV_XepPhong.Columns.Count == 0)
            {
                // Tạo 16 cột cho DataGridView
                dataGV_XepPhong.Columns.Add("MSSV", "MSSV");
                dataGV_XepPhong.Columns.Add("HoTen", "Họ Tên");
                dataGV_XepPhong.Columns.Add("gioitinh", "Giới Tính");
                dataGV_XepPhong.Columns.Add("sotang", "Số Tầng");
                dataGV_XepPhong.Columns.Add("maphong", "Mã Phòng");
                dataGV_XepPhong.Columns.Add("nv1", "Nguyện Vọng 1");
                dataGV_XepPhong.Columns.Add("nv2", "Nguyện Vọng 2");
                dataGV_XepPhong.Columns.Add("nv3", "Nguyện Vọng 3");

            }
        }

        private void Btn_XepPhong_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Lấy danh sách sinh viên từ DataGridView
                List<SinhVienDTO> danhSachSinhVien = new List<SinhVienDTO>();

                foreach (DataGridViewRow row in dataGV_SinhVien.Rows)
                {
                    if (row.IsNewRow) continue; // Bỏ qua dòng mới chưa nhập dữ liệu

                    // Kiểm tra từng ô trước khi sử dụng
                    string mssv = row.Cells["MSSV"]?.Value?.ToString() ?? string.Empty;
                    string hoTen = row.Cells["HoTen"]?.Value?.ToString() ?? string.Empty;
                    string gioiTinh = row.Cells["GioiTinh"]?.Value?.ToString() ?? string.Empty;
                    int nv1 = int.TryParse(row.Cells["NV1"]?.Value?.ToString(), out int result1) ? result1 : 0;
                    int nv2 = int.TryParse(row.Cells["NV2"]?.Value?.ToString(), out int result2) ? result2 : 0;
                    int nv3 = int.TryParse(row.Cells["NV3"]?.Value?.ToString(), out int result3) ? result3 : 0;

                    // Tạo đối tượng sinh viên nếu tất cả dữ liệu cần thiết đều hợp lệ
                    SinhVienDTO sinhVien = new SinhVienDTO
                    {
                        MSSV = mssv,
                        HoTen = hoTen,
                        GioiTinh = gioiTinh,
                        nv1 = nv1,
                        nv2 = nv2,
                        nv3 = nv3
                    };

                    danhSachSinhVien.Add(sinhVien);
                }


                // 2. Lấy danh sách phòng từ BLL
                List<Phong> danhSachPhong = bllPhong.LayDSPhongThuong();

                // 3. Gọi hàm phân phòng bằng KMeans
                Dictionary<string, List<SinhVienDTO>> danhSachXepPhong = bllPhong.PhanPhongBangKMeans(danhSachSinhVien, danhSachPhong);

                // 4. Tạo và cập nhật DataGridView để hiển thị kết quả phân phòng
                dataGV_XepPhong.Rows.Clear(); // Xóa tất cả các dòng cũ trong DataGridView trước khi thêm mới

                foreach (var item in danhSachXepPhong)
                {
                    string maPhong = item.Key; // Mã phòng
                    foreach (var sinhVien in item.Value)
                    {
                        // Thêm một dòng mới vào DataGridView
                        dataGV_XepPhong.Rows.Add(
                            sinhVien.MSSV,
                            sinhVien.HoTen,
                            sinhVien.GioiTinh,
                            maPhong, // Mã phòng
                            GetTangForPhong(maPhong) // Số tầng (Cần phải tạo một hàm lấy tầng từ mã phòng)
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private string GetTangForPhong(string maPhong)
        {
            // Gọi phương thức từ BLL để lấy tầng
            return bllPhong.GetTangForPhong(maPhong);
        }


        private void InitializeDataGridViewColumns()
        {
            // Kiểm tra nếu DataGridView chưa có cột, thì thêm cột
            if (dataGV_SinhVien.Columns.Count == 0)
            {
                // Tạo 16 cột cho DataGridView
                dataGV_SinhVien.Columns.Add("MSSV", "MSSV");
                dataGV_SinhVien.Columns.Add("HoTen", "Họ Tên");
                dataGV_SinhVien.Columns.Add("CCCD", "CCCD");
                dataGV_SinhVien.Columns.Add("Email", "Email");
                dataGV_SinhVien.Columns.Add("NgaySinh", "Ngày Sinh");
                dataGV_SinhVien.Columns.Add("GioiTinh", "Giới Tính");
                dataGV_SinhVien.Columns.Add("HoKhau", "Hộ Khẩu");
                dataGV_SinhVien.Columns.Add("NoiSinh", "Nơi Sinh");
                dataGV_SinhVien.Columns.Add("GhiChu", "Ghi Chú");
                dataGV_SinhVien.Columns.Add("TruongPhong", "Trưởng Phòng");
                dataGV_SinhVien.Columns.Add("HinhCCCDTruoc", "Hình CCCD Trước");
                dataGV_SinhVien.Columns.Add("HinhCCCDSau", "Hình CCCD Sau");
                dataGV_SinhVien.Columns.Add("HinhNhanDien", "Hình Nhận Diện");
                dataGV_SinhVien.Columns.Add("NV1", "NV1");
                dataGV_SinhVien.Columns.Add("NV2", "NV2");
                dataGV_SinhVien.Columns.Add("NV3", "NV3");
            }
        }

        private void XepPhong_Load(object sender, EventArgs e)
        {
            LoadSinhVienChuaDangKy();
            InitializeDataGridViewColumns();
            loadDuLieuPhong();
            DanhSachXepPhong();
        }
        // Phần khai báo trong class Form
        private void loadDuLieuPhong()
        {
            try
            {
                // Gọi hàm từ BLL để lấy danh sách phòng
                List<Phong> danhSachPhong = bllPhong.LayDSPhongThuong();

                // Kiểm tra nếu danh sách phòng không null và có dữ liệu
                if (danhSachPhong != null && danhSachPhong.Count > 0)
                {
                    // Gán danh sách phòng vào DataGridView
                    DataGV_Phong.DataSource = danhSachPhong;

                    // Hiển thị các cột cụ thể
                    DataGV_Phong.Columns["MaPhong"].HeaderText = "Mã Phòng";
                    DataGV_Phong.Columns["SoLuongSinhVienToiDa"].HeaderText = "Số Lượng Sinh Viên Tối Đa";
                    DataGV_Phong.Columns["Tang"].HeaderText = "Số Tầng";

                    // Ẩn các cột không cần thiết
                    foreach (DataGridViewColumn col in DataGV_Phong.Columns)
                    {
                        if (col.Name != "MaPhong" && col.Name != "SoLuongSinhVienToiDa" && col.Name != "Tang")
                        {
                            col.Visible = false;
                        }
                    }

                    // Tùy chỉnh độ rộng của cột
                    DataGV_Phong.Columns["MaPhong"].Width = 100;
                    DataGV_Phong.Columns["SoLuongSinhVienToiDa"].Width = 150;
                    DataGV_Phong.Columns["Tang"].Width = 80;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu phòng để hiển thị.");
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }


        private void LoadSinhVienChuaDangKy()
        {
        //    var sinhViens = bllSinhVien.LaySinhVienChuaDangKyPhong();
          //  dataGV_SinhVien.DataSource = sinhViens;
        }

    }
}
