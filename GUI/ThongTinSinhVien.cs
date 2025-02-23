using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using BLL;
using Control;
using DTO;
using Syncfusion.XlsIO;

namespace GUI
{
    public partial class ThongTinSinhVien : Form
    {


        DA_QL_KTXDataContext ql= new DA_QL_KTXDataContext();
        BLL_QL_SinhVien bllSinhVien = new BLL_QL_SinhVien();
        private PictureBox selectedPictureBox;
        private string hinhNhanDienPath;
        private string hinhCCCDTruocPath;
        private string hinhCCCDSauPath;
        ////Cờ thông báo lỗi
        private bool isImageErrorShown = false;
        private FormNV_TuyenSinh _mainForm;
        private string maSinhVien;

        public ThongTinSinhVien()
        {

           
            InitializeComponent();
            panel_fill.AutoScroll = true;
            this.Load += ThongTinSinhVien_Load;
            datagridview_SinhVien.CellClick += Datagridview_SinhVien_CellClick;
            btn_CapNhatSinhVien.Click += Btn_CapNhatSinhVien_Click;
            pictureBox1.Click += PictureBox_Click;
            Picture_CCCDTruoc.Click += PictureBox_Click;
            picture_CCCDSau.Click += PictureBox_Click;
            btn_Chonfile.Click += Btn_Chonfile_Click;
            btn_TimKiem.Click += Btn_TimKiem_Click;
            //btn_excel.Click += Btn_excel_Click;
            //btn_them.Click += Btn_them_Click;
            //btn_taoPhieuDK.Click += Btn_taoPhieuDK_Click;
            cbo_trangthaidangky.SelectedValueChanged += Cbo_trangthaidangky_SelectedValueChanged;
            VoHieuHoa(false);
        }
        // Hàm để vô hiệu hóa tất cả các điều khiển trong form
        public void VoHieuHoa(bool vohieu)
        {
            txt_CCCCTruoc.Enabled = vohieu;
            txt_CCCD.Enabled = vohieu;
            txt_CCCDSau.Enabled = vohieu;
            txt_Email.Enabled = vohieu;
            txt_GhiChu.Enabled = vohieu;
            txt_GioiTinh.Enabled = vohieu;
            txt_HoKhauThuongTru.Enabled = vohieu;
            txt_HoTen.Enabled = vohieu;
            txt_MaSoSinhVien.Enabled = vohieu;
            txt_NoiSinh.Enabled = vohieu;
            picker_NgaySinh.Enabled = vohieu;
            txt_SDT.Enabled = vohieu;
            check_truongPhong.Enabled = vohieu;
            btn_Chonfile.Enabled = vohieu;
            //btn_taoPhieuDK.Enabled = vohieu;
            //btn_them.Enabled = vohieu;

        }
        private void Cbo_trangthaidangky_SelectedValueChanged(object sender, EventArgs e)
        {
            List<SinhVien> sinhVienList = new List<SinhVien>();

            string selectedValue = cbo_trangthaidangky.SelectedItem?.ToString();
            if (selectedValue == null)
                return;

            switch (selectedValue)
            {
                case "Tất cả":
                    sinhVienList = bllSinhVien.GetAllSinhViens();
                    break;

                case "Chưa đăng ký phòng":
                    sinhVienList = bllSinhVien.GetSinhVienChuaDangKyPhong();
                    break;

                case "Đã đăng ký phòng":
                    sinhVienList = bllSinhVien.GetSinhViensDaDangKyPhong();
                    break;

                default:
                    MessageBox.Show("Lựa chọn không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }

            // Hiển thị danh sách sinh viên trong DataGridView
            datagridview_SinhVien.DataSource = sinhVienList;
        }



        // Constructor nhận FormMain
        public ThongTinSinhVien(FormNV_TuyenSinh mainForm) : this()  // Gọi constructor mặc định
        {
           _mainForm=mainForm;
            if (mainForm == null)
            {
                _mainForm = new FormNV_TuyenSinh(); // Khởi tạo form nếu null
                MessageBox.Show("Không thể mở form khi tham chiếu _mainForm là null.");
                return;  // Tránh việc tiếp tục sử dụng _mainForm khi nó là null
            }

            //this.StartPosition = FormStartPosition.Manual;
            //this.Location = _mainForm.Location; // Vị trí của form mẹ
            //this.Size = _mainForm.ClientSize; // Kích thước vùng hiển thị của form mẹ
            
            _mainForm = mainForm;  // Lưu tham chiếu _mainForm
        }

        // Constructor nhận MaSinhVien
        public ThongTinSinhVien(FormNV_TuyenSinh mainForm,string maSinhVien) : this()  // Gọi constructor mặc định
        {
            _mainForm= mainForm;
            this.maSinhVien = maSinhVien;
            LoadThongTinSinhVien(); // Tải thông tin sinh viên
        }

        // Method to load student data
        public void LoadThongTinSinhVien()
        {
            if (string.IsNullOrEmpty(maSinhVien))
            {
                MessageBox.Show("Mã sinh viên không hợp lệ.");
                return;
            }

            // Fetch student details using the MaSinhVien
            SinhVien sinhVien = bllSinhVien.GetSinhVienByMa(maSinhVien);

            // Check if the student was found
            if (sinhVien != null)
            {
                // Fill in the text fields with student data
                txt_MaSoSinhVien.Text = sinhVien.MaSinhVien;
                txt_HoTen.Text = sinhVien.HoTen;
                txt_CCCD.Text = sinhVien.CCCD;
                txt_SDT.Text = sinhVien.SDT;
                txt_Email.Text = sinhVien.Email;
                txt_HoKhauThuongTru.Text = sinhVien.HoKhauThuongTru;
                txt_NoiSinh.Text = sinhVien.NoiSinh;
                txt_GhiChu.Text = sinhVien.GhiChu;
                txt_GioiTinh.Text = sinhVien.GioiTinh;

                // Load images
                LoadImage(sinhVien.HinhNhanDien, pictureBox1);
                LoadImage(sinhVien.HinhCCCDTruoc, Picture_CCCDTruoc);
                LoadImage(sinhVien.HinhCCCDSau, picture_CCCDSau);
            }
            else
            {
                MessageBox.Show("Không tìm thấy sinh viên.");
            }
        }

        // Method to load images into the PictureBox
        private void LoadImage(string imagePath, PictureBox pictureBox)
        {
            string fullImagePath = Path.Combine(@"D:\DACN2\DACN\DACN_QL_KTX\DACN_QL_KTX\avatars\",imagePath);
            if (File.Exists(fullImagePath))
            {
                pictureBox.Image = Image.FromFile(fullImagePath);
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                pictureBox.Image = Properties.Resources.user; // Default image if not found
            }
        }

       

        private void Btn_them_Click(object sender, EventArgs e)
        {
            //btn_taoPhieuDK.Enabled = true;
            //btn_excel.Enabled = false;
            if (datagridview_SinhVien.DataSource is List<SinhVien> sinhVienList && sinhVienList.Any())
            {
                // Duyệt qua từng sinh viên trong danh sách
                foreach (var sinhVien in sinhVienList)
                {
                    // Kiểm tra và loại bỏ ký tự đặc biệt trong CCCD
                    if (!string.IsNullOrEmpty(sinhVien.CCCD))
                    {
                        sinhVien.CCCD = Regex.Replace(sinhVien.CCCD, @"[^\w\d]", "");
                    }
                }

                // Gọi phương thức để thêm danh sách sinh viên vào cơ sở dữ liệu
                bool result = bllSinhVien.ThemDanhSachSinhVien(sinhVienList);

                if (result)
                {
                    MessageBox.Show("Thêm sinh viên vào cơ sở dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Tải lại dữ liệu từ cơ sở dữ liệu
                    datagridview_SinhVien.DataSource = bllSinhVien.GetAllSinhViens();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi thêm sinh viên vào cơ sở dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu sinh viên để thêm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void Btn_taoPhieuDK_Click(object sender, EventArgs e)
        {
            if (datagridview_SinhVien.SelectedRows.Count > 0)
            {
                var selectedRow = datagridview_SinhVien.SelectedRows[0];

                if (selectedRow.DataBoundItem is SinhVien sinhVien)
                {
                    Console.WriteLine($"Sinh viên được chọn: Mã {sinhVien.MaSinhVien}, Họ tên {sinhVien.HoTen}");

                    // Tạo và mở form DangKyLuuTru
                    DangKyLuuTru formDangKyLuuTru = new DangKyLuuTru(_mainForm, sinhVien);
                    _mainForm.OpenChildForm(formDangKyLuuTru);
                   // _mainForm.OpenChildForm(DangKyLuuTru(_mainForm, sinhVien));
                  //  DangKyLuuTru formDangKyLuuTru = new DangKyLuuTru(_mainForm, sinhVien);
                  //  formDangKyLuuTru.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không thể lấy thông tin sinh viên từ hàng được chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để tạo phiếu đăng ký.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void Btn_excel_Click(object sender, EventArgs e)
        {
           // btn_them.Enabled = true;
            btn_CapNhatSinhVien.Enabled = false;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                openFileDialog.Title = "Chọn tệp Excel";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                    {
                        LoadExcelToDataGridView(filePath);
                    }
                    else
                    {
                        MessageBox.Show("Tệp không tồn tại hoặc không hợp lệ.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void LoadExcelToDataGridView(string filePath)
        {
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            IWorkbook workbook = null;

            try
            {
                // Mở tệp Excel
                workbook = application.Workbooks.Open(filePath);

                // Kiểm tra workbook có hợp lệ không
                if (workbook == null)
                {
                    MessageBox.Show("Không thể mở workbook từ tệp đã chọn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra xem workbook có chứa ít nhất một worksheet không
                if (workbook.Worksheets == null || workbook.Worksheets.Count == 0)
                {
                    MessageBox.Show("Workbook không chứa worksheet nào.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy worksheet đầu tiên
                IWorksheet worksheet = workbook.Worksheets[0];

                // Kiểm tra xem worksheet có hợp lệ không
                if (worksheet == null)
                {
                    MessageBox.Show("Không thể truy xuất worksheet đầu tiên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra nếu UsedRange là null hoặc không có dữ liệu
                if (worksheet.UsedRange == null || worksheet.UsedRange.Rows.Length == 0)
                {
                    MessageBox.Show("Không có dữ liệu trong worksheet.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Đọc dữ liệu từ worksheet và tạo danh sách SinhVien
                List<SinhVien> sinhVienList = new List<SinhVien>();

                // Lặp qua các dòng dữ liệu trong worksheet
                int rowCount = worksheet.UsedRange.Rows.Length;

                for (int i = 2; i <= rowCount; i++) // Bắt đầu từ dòng thứ 2 để bỏ qua tiêu đề
                {
                    SinhVien sinhVien = new SinhVien
                    {
                        MaSinhVien = worksheet[i, 1]?.Value?.ToString() ?? string.Empty,
                        HoTen = worksheet[i, 2].Text,
                        CCCD = worksheet[i, 3].Text,
                        Email = worksheet[i, 4].Text,
                        NgaySinh = worksheet[i, 5].DateTime,
                        GioiTinh = worksheet[i, 6].Text,
                        HoKhauThuongTru = worksheet[i, 7].Text,
                        NoiSinh = worksheet[i, 8].Text,
                        GhiChu = worksheet[i, 9].Text,
                        TruongPhong = worksheet[i, 10].Text == "TRUE", // Chuyển đổi giá trị Boolean
                        HinhCCCDTruoc = worksheet[i, 11].Text,
                        HinhCCCDSau = worksheet[i, 12].Text,
                        HinhNhanDien = worksheet[i, 13].Text
                    };

                    sinhVienList.Add(sinhVien);
                }

                // Gán danh sách SinhVien vào DataGridView
                datagridview_SinhVien.DataSource = sinhVienList;

                // Gọi phương thức để định dạng DataGridView
                FormatDataGridView();
            }
            catch (IOException ioEx)
            {
                MessageBox.Show($"Lỗi khi mở tệp: {ioEx.Message}", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (NullReferenceException nullEx)
            {
                MessageBox.Show($"Lỗi tham chiếu null: {nullEx.Message}", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (workbook != null)
                {
                    workbook.Close();
                }
                excelEngine.Dispose();
            }
        }



        private void FormatDataGridView()
        {
            if (datagridview_SinhVien.DataSource != null)
            {
                // Bật chế độ thanh cuộn
                datagridview_SinhVien.ScrollBars = ScrollBars.Both;

                // Không tự động điều chỉnh kích thước cột
                datagridview_SinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                // Đặt chiều rộng cố định cho từng cột
                foreach (DataGridViewColumn column in datagridview_SinhVien.Columns)
                {
                    column.Width = 100; // Đặt kích thước cố định cho mỗi cột
                }

                // Đặt màu nền xen kẽ cho các hàng
                datagridview_SinhVien.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

                // Đặt chiều cao cố định cho các hàng
                datagridview_SinhVien.RowTemplate.Height = 30;

                // Căn chỉnh chế độ chọn hàng (chọn toàn bộ hàng)
                datagridview_SinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // Đặt font chữ
                datagridview_SinhVien.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8);
                datagridview_SinhVien.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8);
                datagridview_SinhVien.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Đặt tên tiêu đề các cột
                datagridview_SinhVien.Columns["MaSinhVien"].HeaderText = "Mã sinh viên";
                datagridview_SinhVien.Columns["HoTen"].HeaderText = "Họ tên";
                datagridview_SinhVien.Columns["CCCD"].HeaderText = "CCCD";
                datagridview_SinhVien.Columns["Email"].HeaderText = "Email";
                datagridview_SinhVien.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                datagridview_SinhVien.Columns["GioiTinh"].HeaderText = "Giới tính";
                datagridview_SinhVien.Columns["HoKhauThuongTru"].HeaderText = "Hộ khẩu thường trú";
                datagridview_SinhVien.Columns["NoiSinh"].HeaderText = "Nơi sinh";
                datagridview_SinhVien.Columns["GhiChu"].HeaderText = "Ghi chú";
                datagridview_SinhVien.Columns["TruongPhong"].HeaderText = "Trưởng phòng";
                datagridview_SinhVien.Columns["HinhCCCDTruoc"].HeaderText = "Hình CCCD trước";
                datagridview_SinhVien.Columns["HinhCCCDSau"].HeaderText = "Hình CCCD sau";
                datagridview_SinhVien.Columns["HinhNhanDien"].HeaderText = "Hình nhận diện";
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để định dạng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //private void Btn_excel_Click(object sender, EventArgs e)
        //{
        //    // Mở hộp thoại chọn tệp Excel
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";
        //    if (openFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        string filePath = openFileDialog.FileName;

        //        // Đọc dữ liệu từ tệp Excel
        //        try
        //        {
        //            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
        //            using (var package = new ExcelPackage(new FileInfo(filePath)))
        //            {
        //                var worksheet = package.Workbook.Worksheets[0]; // Chọn sheet đầu tiên
        //                var rowCount = worksheet.Dimension.Rows;

        //                // Tạo danh sách để lưu dữ liệu nhân viên từ Excel
        //                var nhanVienList = new List<NhanVien>();

        //                // Bắt đầu đọc từ dòng thứ 2, bỏ qua tiêu đề
        //                for (int row = 2; row <= rowCount; row++)
        //                {
        //                    // Đọc dữ liệu từng cột từ Excel
        //                    string maNhanVien = worksheet.Cells[row, 1].Text;
        //                    string hoTen = worksheet.Cells[row, 2].Text;
        //                    string chucVu = worksheet.Cells[row, 3].Text;
        //                    string diaChiLienLac = worksheet.Cells[row, 4].Text;
        //                    string dienThoai = worksheet.Cells[row, 5].Text;
        //                    string email = worksheet.Cells[row, 6].Text;

        //                    int maTaiKhoan;
        //                    if (!int.TryParse(worksheet.Cells[row, 7].Text, out maTaiKhoan))
        //                    {
        //                        maTaiKhoan = 0; // Giá trị mặc định nếu không chuyển đổi được
        //                    }

        //                    string hinhanh = worksheet.Cells[row, 8].Text;
        //                    DateTime ngaySinh;
        //                    if (!DateTime.TryParseExact(worksheet.Cells[row, 9].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngaySinh))
        //                    {
        //                        // Xử lý trường hợp ngày sinh không hợp lệ
        //                        MessageBox.Show("Ngày sinh không hợp lệ ở dòng " + row);
        //                        continue;
        //                    }

        //                    string gioiTinh = worksheet.Cells[row, 10].Text;

        //                    // Thêm vào danh sách nhân viên
        //                    nhanVienList.Add(new sinhvien
        //                    {
        //                        MaNhanVien = maNhanVien,
        //                        HoTen = hoTen,
        //                        ChucVu = chucVu,
        //                        DiaChiLienLac = diaChiLienLac,
        //                        DienThoai = dienThoai,
        //                        Email = email,
        //                        MaTaiKhoan = maTaiKhoan,
        //                        Hinhanh = hinhanh,
        //                        NgaySinh = ngaySinh,
        //                        GioiTinh = gioiTinh
        //                    });
        //                }

        //                // Lưu vào cơ sở dữ liệu qua LINQ to SQL
        //                ql.SinhViens.InsertAllOnSubmit(nhanVienList);
        //                ql.SubmitChanges();

        //                // Hàm này để tải lại dữ liệu vào DataGridView

        //                MessageBox.Show("Dữ liệu đã được nhập và lưu vào cơ sở dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Lỗi khi đọc file Excel: " + ex.Message);
        //        }
        //    }

        //}



            private void Btn_TimKiem_Click(object sender, EventArgs e)
        {
           string maSV=txt_TimMSSV.Text.Trim();
            if(string.IsNullOrEmpty(maSV))
            {
                MessageBox.Show("Vui lòng nhập mã số sinh viên cần tìm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }    
            var sinhvien=bllSinhVien.GetSinhVienByMa(maSV);


            if (sinhvien != null)
            {
                datagridview_SinhVien.DataSource = new List<SinhVien> { sinhvien };
            }
            else
            {
                MessageBox.Show("Không tìm thấy sinh viên với mã số này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                datagridview_SinhVien.DataSource = bllSinhVien.GetAllSinhViens();

            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
           selectedPictureBox=sender as PictureBox;//Luu pictureBox đang chọn
        }

       
        private void Btn_Chonfile_Click(object sender, EventArgs e)
        {

            if (selectedPictureBox == null)
            {
                MessageBox.Show("Vui lòng chọn một khung hình trước khi chọn file.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Chọn hình ảnh";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedImagePath = openFileDialog.FileName;
                    Bitmap resizedImage = new Bitmap(Image.FromFile(selectedImagePath), new Size(168, 209));
                    selectedPictureBox.Image = resizedImage;

                    // Lưu đường dẫn vào đúng biến
                    if (selectedPictureBox == pictureBox1)
                        hinhNhanDienPath = selectedImagePath;
                    else if (selectedPictureBox == Picture_CCCDTruoc)
                        hinhCCCDTruocPath = selectedImagePath;
                    else if (selectedPictureBox == picture_CCCDSau)
                        hinhCCCDSauPath = selectedImagePath;
                }
            }
        }

        private void Btn_CapNhatSinhVien_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem mã sinh viên có được nhập không
            if (string.IsNullOrWhiteSpace(txt_MaSoSinhVien.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime ngaySinh =picker_NgaySinh.Value;
            int tuoi= DateTime.Now.Year- ngaySinh.Year;
            if (ngaySinh > DateTime.Now.AddYears(tuoi)) tuoi--; // Điều chỉnh tuổi nếu chưa đến sinh nhật năm nay
            if (tuoi < 18)
            {
                MessageBox.Show("Sinh viên phải từ 18 tuổi trở lên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // Tạo đối tượng SinhVien mới
            var sinhVien = new SinhVien
            {
                MaSinhVien = txt_MaSoSinhVien.Text,
                HoTen = txt_HoTen.Text,
                CCCD = txt_CCCD.Text,
                SDT = txt_SDT.Text, // Số điện thoại
                NgaySinh = picker_NgaySinh.Value,
                GioiTinh = txt_GioiTinh.Text,
                Email = txt_Email.Text, // Cập nhật trường email
                HoKhauThuongTru = txt_HoKhauThuongTru.Text,
                NoiSinh = txt_NoiSinh.Text,
                GhiChu = txt_GhiChu.Text,
                TruongPhong = check_truongPhong.Checked, // Kiểu boolean
                HinhNhanDien = string.IsNullOrEmpty(hinhNhanDienPath) ? "user.png" : Path.GetFileName(hinhNhanDienPath), // Sử dụng hình ảnh đã chọn
                HinhCCCDTruoc = string.IsNullOrEmpty(hinhCCCDTruocPath) ? txt_CCCCTruoc.Text : Path.GetFileName(hinhCCCDTruocPath),
                HinhCCCDSau = string.IsNullOrEmpty(hinhCCCDSauPath) ? txt_CCCDSau.Text : Path.GetFileName(hinhCCCDSauPath)

            };

            // Gọi phương thức cập nhật từ BLL
            var result = bllSinhVien.UpdateSinhVien(sinhVien);

            if (result)
            {
                MessageBox.Show("Cập nhật thông tin sinh viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cập nhật lại DataGridView
                datagridview_SinhVien.DataSource = bllSinhVien.GetAllSinhViens();
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin sinh viên không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Datagridview_SinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            VoHieuHoa(true);
       //     btn_them.Enabled = false;
            if (e.RowIndex >= 0) // Kiểm tra hàng có hợp lệ không
            {
                var sinhVien = datagridview_SinhVien.Rows[e.RowIndex].DataBoundItem as SinhVien;
                if (sinhVien != null)
                {
                    txt_MaSoSinhVien.Text = sinhVien.MaSinhVien;
                    txt_HoTen.Text = sinhVien.HoTen;
                    txt_CCCD.Text = sinhVien.CCCD;
                    txt_SDT.Text = sinhVien.SDT;
                        picker_NgaySinh.Value = sinhVien.NgaySinh;
                    txt_GioiTinh.Text = sinhVien.GioiTinh;
                    txt_Email.Text = sinhVien.Email;
                    txt_HoKhauThuongTru.Text = sinhVien.HoKhauThuongTru;
                    txt_NoiSinh.Text = sinhVien.NoiSinh;
                    txt_GhiChu.Text = sinhVien.GhiChu;
                    txt_CCCDSau.Text = sinhVien.HinhCCCDSau;
                    txt_CCCCTruoc.Text = sinhVien.HinhCCCDTruoc;
                   check_truongPhong.Checked = Convert.ToBoolean(sinhVien.TruongPhong);// Nếu trường là boolean

                    LoadImageToPictureBox(sinhVien.HinhNhanDien, pictureBox1);
                    LoadImageToPictureBox(sinhVien.HinhCCCDTruoc, Picture_CCCDTruoc);
                    LoadImageToPictureBox(sinhVien.HinhCCCDSau, picture_CCCDSau);
                }
                else
                {
                    MessageBox.Show("Dữ liệu sinh viên không hợp lệ.");
                }
            }
        }



        private void LoadImageToPictureBox(string imageName, PictureBox pictureBox)
        {
            if (string.IsNullOrEmpty(imageName))
            {
                if (!isImageErrorShown)
                {
                    MessageBox.Show("Đường dẫn hình ảnh không hợp lệ.");
                    isImageErrorShown = true; // Đánh dấu là đã thông báo lỗi
                }
                return;
            }

            string imagePath = Path.Combine(@"D:\DACN\DACN_QL_KTX\DACN_QL_KTX\avatars\", imageName);

            //  string imagePath = Path.Combine("GUI", "Resources", imageName);
            // string imagePath = Path.Combine(Application.StartupPath, "GUI", "Resources", imageName);

            try
            {
                if (File.Exists(imagePath))
                {
                    Bitmap resizedImage = new Bitmap(Image.FromFile(imagePath), new Size(168, 209));
                    pictureBox.Image = resizedImage;
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    isImageErrorShown = false; // Reset lại flag nếu hình ảnh hợp lệ

                }
                else
                {
                    pictureBox.Image = null;
                    if (!isImageErrorShown)
                    {
                        MessageBox.Show("Hình ảnh không tồn tại.");
                        isImageErrorShown = true; // Đánh dấu là đã thông báo lỗi
                    }
                }
            }
            catch (Exception ex)
            {
                if (!isImageErrorShown)
                {
                    MessageBox.Show($"Lỗi khi đọc hình ảnh: {ex.Message}");
                    isImageErrorShown = true; // Đánh dấu là đã thông báo lỗi
                }
            }

        }


        private void ThongTinSinhVien_Load(object sender, EventArgs e)
        {
            cbo_trangthaidangky.SelectedIndex = 0;
            // Đặt font chữ
            datagridview_SinhVien.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8);
            datagridview_SinhVien.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8);
            datagridview_SinhVien.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }
        public void LoadThongTin()
        {
            datagridview_SinhVien.DataSource = bllSinhVien.GetAllSinhViens();

            datagridview_SinhVien.Columns["MaSinhVien"].HeaderText = "Mã sinh viên";
            datagridview_SinhVien.Columns["HoTen"].HeaderText = "Họ tên";
            datagridview_SinhVien.Columns["CCCD"].HeaderText = "CCCD";
            datagridview_SinhVien.Columns["Email"].HeaderText = "Email";
            datagridview_SinhVien.Columns["SDT"].HeaderText = "Số điện thoại";
            datagridview_SinhVien.Columns["NgaySinh"].HeaderText = "Ngày sinh";
            datagridview_SinhVien.Columns["GioiTinh"].HeaderText = "Giới tính";
            datagridview_SinhVien.Columns["HoKhauThuongTru"].HeaderText = "Hộ khẩu thường trú";
            datagridview_SinhVien.Columns["NoiSinh"].HeaderText = "Nơi sinh";
            datagridview_SinhVien.Columns["GhiChu"].HeaderText = "Ghi chú";
            datagridview_SinhVien.Columns["TruongPhong"].HeaderText = "Trưởng phòng";
            datagridview_SinhVien.Columns["HinhCCCDTruoc"].HeaderText = "Hình CCCD trước";
            datagridview_SinhVien.Columns["HinhCCCDSau"].HeaderText = "Hình CCCD sau";
            datagridview_SinhVien.Columns["HinhNhanDien"].HeaderText = "Hình nhận diện";
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel_fill_Paint(object sender, PaintEventArgs e)
        {

        }

        private void check_truongPhong_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Lbl15_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
