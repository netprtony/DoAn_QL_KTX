using BLL;
using DTO;
using GUI.Model;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class QuanLyNhanVien : Form
    {
        private BLL_NhanVien bllNhanVien = new BLL_NhanVien();
        private DA_QL_KTXDataContext db = new DA_QL_KTXDataContext();
        public QuanLyNhanVien()
        {
            InitializeComponent();
            LoadNhanVienData();
            dgvNhanVien.CellClick += dgvNhanVien_CellContentClick;
            btn_LuuCapNhat.Click += btn_LuuCapNhat_Click;
            LoadTaiKhoan();
            LoadChucVu();
            txt_matakhoanv.TextChanged += txt_matakhoanv_TextChanged;
            dgvNhanVien.CellClick += dgvNhanVien_CellContentClick;
            MoveControlsToPanel();
        }
        private void MoveControlsToPanel()
        {
            // Kiểm tra nếu panel1 chưa khởi tạo
            if (panel1 == null)
            {
                MessageBox.Show("panel1 chưa được khởi tạo.");
                return;
            }

            // Tạo danh sách tạm thời để lưu các control
            var controlsToMove = new List<System.Windows.Forms.Control>();

            // Lặp qua tất cả các control trong form
            foreach (System.Windows.Forms.Control control in this.Controls)
            {
                // Bỏ qua panel1 để không tự di chuyển chính nó
                if (control != panel1)
                {
                    controlsToMove.Add(control);
                }
            }

            // Di chuyển các control từ danh sách tạm sang panel1
            foreach (System.Windows.Forms.Control control in controlsToMove)
            {
                panel1.Controls.Add(control);
            }

            // Xóa tất cả các control khỏi form (trừ panel1)
            this.Controls.Clear();

            // Thêm lại panel1 vào form
            this.Controls.Add(panel1);
        }
        public async void LoadNhanVienData()
        {
            // Lấy danh sách từ BLL
            List<NhanVien> danhSach = bllNhanVien.GetNhanVien();
            // Lấy danh sách Mã Đăng Ký


            // Gán danh sách vào DataGridView
            dgvNhanVien.DataSource = danhSach;

            // Tự động điều chỉnh kích thước các cột
            dgvNhanVien.AutoResizeColumns();
            Task.Run(() => LoadTinhThanhAsync());
            dgvNhanVien.Columns[10].Visible = false;


        }


        public async void LoadNhanVienData1()
        {
            // Lấy danh sách từ BLL
            List<NhanVien> danhSach = bllNhanVien.GetNhanVien();
            // Lấy danh sách Mã Đăng Ký


            // Gán danh sách vào DataGridView
            dgvNhanVien.DataSource = danhSach;

            // Tự động điều chỉnh kích thước các cột
            dgvNhanVien.AutoResizeColumns();
            Task.Run(() => LoadTinhThanhAsync());



        }

        public void LoadTaiKhoan()
        {
            List<TaiKhoan> ds = bllNhanVien.GetTaiKhoan();
            // Gán danh sách vào DataGridView
            dataGridView_taikhoan.DataSource = ds;

            // Tự động điều chỉnh kích thước các cột
            dataGridView_taikhoan.AutoResizeColumns();
            dataGridView_taikhoan.Columns[2].Visible = false;

        }


        private void SetAddressToComboBoxes(string diaChiLienLac)
        {
            if (string.IsNullOrWhiteSpace(diaChiLienLac))
            {
                MessageBox.Show("Địa chỉ trống hoặc không hợp lệ!", "Thông báo");
                return;
            }

            // Tách địa chỉ bằng dấu phẩy và loại bỏ khoảng trắng
            string[] addressParts = diaChiLienLac.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                 .Select(part => part.Trim())
                                                 .ToArray();

            // Kiểm tra xem chuỗi có đủ các phần Tỉnh, Huyện, Xã không
            if (addressParts.Length >= 3)
            {
                // Gán giá trị trực tiếp cho các ComboBox
                comboBoxTinhThanh.Text = addressParts[2]; // Tỉnh
                comboBoxHuyen.Text = addressParts[1];      // Huyện
                comboBoxXa.Text = addressParts[0];         // Xã

                // Khóa các ComboBox để ngăn người dùng chỉnh sửa
                comboBoxTinhThanh.Enabled = false;
                comboBoxHuyen.Enabled = false;
                comboBoxXa.Enabled = false;
            }
            else
            {
                MessageBox.Show("Địa chỉ không hợp lệ! Địa chỉ phải có định dạng: Tỉnh, Huyện, Xã.", "Thông báo");
            }
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



        private async void comboBoxTinhThanh_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBoxTinhThanh.SelectedItem is ComboBoxItem selectedItem)
            {
                int provinceId = (int)selectedItem.Value;
                await LoadHuyenAsync(provinceId);
            }
        }

        private async void comboBoxHuyen_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBoxHuyen.SelectedItem is ComboBoxItem selectedItem)
            {
                int districtId = (int)selectedItem.Value;
                await LoadXaAsync(districtId);
            }
        }
        
        private async void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu người dùng đã chọn một dòng hợp lệ
            if (e.RowIndex >= 0)
            {
                // Lấy thông tin của nhân viên từ dòng đã chọn
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];

                // Cập nhật thông tin vào các TextBox
                txt_MaNhanVien.Text = row.Cells["MaNhanVien"].Value.ToString(); // Ví dụ: Cột "MaNhanVien"
                txt_hoTen.Text = row.Cells["HoTen"].Value.ToString(); // Cập nhật họ tên
                txt_email1.Text = row.Cells["Email"].Value.ToString(); // Cập nhật email
                txt_soDienThoai1.Text = row.Cells["DienThoai"].Value.ToString(); // Cập nhật số điện thoại
                txt_ChucVu.Text = row.Cells["ChucVu"].Value.ToString(); // Cập nhật chức vụ
                string maTaiKhoan1 = row.Cells["MaTaiKhoan"].Value?.ToString();
                string gioiTinh = row.Cells["GioiTinh"].Value?.ToString();
                if (gioiTinh == "Nam")
                {
                    radio_Nam.Checked = true; // Chọn radio_nam nếu giới tính là Nam
                    radio_Nu.Checked = false; // Hủy chọn radio_nu
                }
                else if (gioiTinh == "Nữ")
                {
                    radio_Nu.Checked = true; // Chọn radio_nu nếu giới tính là Nữ
                    radio_Nam.Checked = false; // Hủy chọn radio_nam
                }
                else
                {
                    radio_Nam.Checked = false; // Không chọn nếu không phải Nam hoặc Nữ
                    radio_Nu.Checked = false;
                }

                // Lấy tên file hình ảnh từ cơ sở dữ liệu
                string hinhAnhFileName = row.Cells["Hinhanh"].Value?.ToString(); // Giả sử cột "HinhAnh" chứa tên file



                if (!string.IsNullOrEmpty(hinhAnhFileName))
                {
                    // Tạo đường dẫn đầy đủ đến thư mục chứa hình ảnh
                    string imagePath = Path.Combine(@"D:\DACN\DACN_QL_KTX\DACN_QL_KTX\avatars\", hinhAnhFileName);

                    // Kiểm tra xem tệp hình ảnh có tồn tại không
                    if (File.Exists(imagePath))
                    {
                        // Hiển thị hình ảnh vào PictureBox
                        picture_Anh.Image = Image.FromFile(imagePath); // Giả sử bạn có một PictureBox tên là PictureBoxImage
                    }
                    else
                    {
                        // Nếu không tìm thấy hình ảnh, có thể đặt hình ảnh mặc định
                        picture_Anh.Image = null; // Hoặc đặt một hình ảnh mặc định
                        MessageBox.Show("Không tìm thấy hình ảnh cho nhân viên.", "Thông báo");
                    }
                }
                else
                {
                    // Nếu không có hình ảnh, có thể đặt hình ảnh mặc định
                    picture_Anh.Image = null; // Hoặc đặt một hình ảnh mặc định
                }

                if (!string.IsNullOrEmpty(maTaiKhoan1))
                {
                    txt_matakhoanv.Text = maTaiKhoan1;
                }
                else
                {
                    MessageBox.Show("Nhân viên chưa có tài khoản.", "Thông báo");
                }
                string diaChiLienLac = row.Cells["DiaChiLienLac"].Value?.ToString();
                if (!string.IsNullOrEmpty(diaChiLienLac))
                {
                    SetAddressToComboBoxes(diaChiLienLac);

                    // Tách địa chỉ thành mã tỉnh, huyện, xã
                    var addressParts = diaChiLienLac.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                     .Select(part => part.Trim())
                                                     .ToArray();

                    if (addressParts.Length >= 3)
                    {
                        // Tải dữ liệu tỉnh/huyện/xã dựa trên địa chỉ
                        string tinhName = addressParts[2];
                        string huyenName = addressParts[1];
                        string xaName = addressParts[0];

                        // Gọi API để tải dữ liệu liên quan
                        await LoadTinhThanhAsync();
                        var tinhItem = comboBoxTinhThanh.Items.Cast<ComboBoxItem>()
                                      .FirstOrDefault(item => item.Text == tinhName);

                        if (tinhItem != null)
                        {
                            comboBoxTinhThanh.SelectedItem = tinhItem;
                            await LoadHuyenAsync((int)tinhItem.Value);

                            var huyenItem = comboBoxHuyen.Items.Cast<ComboBoxItem>()
                                          .FirstOrDefault(item => item.Text == huyenName);

                            if (huyenItem != null)
                            {
                                comboBoxHuyen.SelectedItem = huyenItem;
                                await LoadXaAsync((int)huyenItem.Value);

                                var xaItem = comboBoxXa.Items.Cast<ComboBoxItem>()
                                            .FirstOrDefault(item => item.Text == xaName);

                                if (xaItem != null)
                                {
                                    comboBoxXa.SelectedItem = xaItem;
                                }
                            }
                        }
                    }
                }

                // Lấy MaTaiKhoan từ nhân viên
                string maTaiKhoan = row.Cells["MaTaiKhoan"].Value?.ToString();

                if (!string.IsNullOrEmpty(maTaiKhoan))
                {
                    // Tìm tài khoản từ BLL dựa trên MaTaiKhoan
                    TaiKhoan taiKhoan = bllNhanVien.GetTaiKhoanById(maTaiKhoan);

                    if (taiKhoan != null)
                    {
                        // Hiển thị thông tin tài khoản vào các TextBox và ComboBox
                        txt_tentaikhoan.Text = taiKhoan.TenDangNhap;
                        txt_matkhau.Text = taiKhoan.MatKhau;
                        cbm_chucvu.Text = taiKhoan.VaiTro;
                    }
                    else
                    {
                        // Xóa thông tin cũ nếu không tìm thấy tài khoản
                        txt_tentaikhoan.Text = "";
                        txt_matkhau.Text = "";
                        cbm_chucvu.SelectedIndex = -1; // Không chọn mục nào
                        MessageBox.Show("Không tìm thấy tài khoản tương ứng.", "Thông báo");
                    }
                }
                else
                {
                    // Xóa thông tin cũ nếu nhân viên không có tài khoản
                    txt_tentaikhoan.Text = "";
                    txt_matkhau.Text = "";
                    cbm_chucvu.SelectedIndex = -1; // Không chọn mục nào
                    MessageBox.Show("Nhân viên chưa có tài khoản.", "Thông báo");
                }
            }
        }

        private async void UnlockComboBoxesForEditing()
        {
            comboBoxTinhThanh.Enabled = true;
            comboBoxHuyen.Enabled = true;
            comboBoxXa.Enabled = true;
            await LoadTinhThanhAsync();
        }

        private void btn__ChonAnh_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Chọn ảnh nhân viên";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Hiển thị ảnh trong PictureBox
                    picture_Anh.Image = Image.FromFile(openFileDialog.FileName);

                    // Lấy tên file ảnh từ đường dẫn người dùng đã chọn
                    string imageFileName = Path.GetFileName(openFileDialog.FileName);

                    // Cập nhật tên ảnh vào label_hinhanh
                    label_hinhanh.Text = imageFileName;

                    // Tạo đường dẫn tuyệt đối mới trong thư mục avatars
                    string imagePath = Path.Combine("D:\\DACN\\DACN_QL_KTX\\DACN_QL_KTX\\avatars", imageFileName);

                    // Sao chép file vào thư mục avatars nếu file chưa tồn tại
                    if (!File.Exists(imagePath))
                    {
                        File.Copy(openFileDialog.FileName, imagePath);
                    }

                    // Lưu đường dẫn tuyệt đối vào TextBox hoặc sử dụng cho cơ sở dữ liệu
                    //  txtDuongDanAnh.Text = imagePath;
                }
            }
        }

        private async void btn_capnhat_Click(object sender, EventArgs e)
        {
            UnlockComboBoxesForEditing();
            await LoadTinhThanhAsync();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra tính hợp lệ của các trường
                if (string.IsNullOrWhiteSpace(txt_hoTen.Text) || string.IsNullOrWhiteSpace(txt_soDienThoai1.Text) ||
                    string.IsNullOrWhiteSpace(txt_email1.Text) || string.IsNullOrWhiteSpace(dtp_ngaySinh1.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                    return;
                }

                // Kiểm tra số điện thoại hợp lệ (Giả sử là số điện thoại Việt Nam)
                string phonePattern = @"^(0[3|5|7|8|9])[0-9]{8}$"; // Mẫu số điện thoại Việt Nam
                if (!System.Text.RegularExpressions.Regex.IsMatch(txt_soDienThoai1.Text, phonePattern))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra ngày sinh hợp lệ (Ngày sinh không được lớn hơn ngày hiện tại)
                if (dtp_ngaySinh1.Value >= DateTime.Now)
                {
                    MessageBox.Show("Ngày sinh không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra mã nhân viên hợp lệ (Giả sử mã nhân viên là chuỗi không chứa ký tự đặc biệt)
                string maNhanVienPattern = @"^[A-Za-z0-9]+$"; // Mã nhân viên không được có ký tự đặc biệt
                if (!System.Text.RegularExpressions.Regex.IsMatch(txt_MaNhanVien.Text, maNhanVienPattern))
                {
                    MessageBox.Show("Mã nhân viên không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra định dạng email hợp lệ
                string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                if (!System.Text.RegularExpressions.Regex.IsMatch(txt_email1.Text, emailPattern))
                {
                    MessageBox.Show("Email không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra và chuyển đổi mã tài khoản
                if (!int.TryParse(txt_matakhoanv.Text, out int maTaiKhoan))
                {
                    MessageBox.Show("Mã tài khoản không hợp lệ. Vui lòng nhập số nguyên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                NhanVien newNhanVien = new NhanVien
                {
                    MaNhanVien = txt_MaNhanVien.Text,  // Sử dụng giá trị kiểu chuỗi trực tiếp
                    HoTen = txt_hoTen.Text,            // Tên nhân viên
                    ChucVu = txt_ChucVu.Text,          // Chức vụ
                    DienThoai = txt_soDienThoai1.Text, // Số điện thoại
                    Email = txt_email1.Text,           // Email
                    NgaySinh = dtp_ngaySinh1.Value,    // Ngày sinh
                    GioiTinh = radio_Nam.Checked ? "Nam" : "Nữ", // Giới tính
                    DiaChiLienLac = comboBoxXa.Text + ", " + comboBoxHuyen.Text + ", " + comboBoxTinhThanh.Text, // Địa chỉ
                    Hinhanh = string.IsNullOrWhiteSpace(label_hinhanh.Text) ? "default.png" : label_hinhanh.Text, // Kiểm tra ảnh
                    MaTaiKhoan = maTaiKhoan                                                                                   //MaTaiKhoan = null // Cập nhật đúng giá trị MaTaiKhoan nếu cần
                };

                // Gọi phương thức từ BLL để thêm nhân viên
                bool result = bllNhanVien.AddNhanVien(newNhanVien);

                if (result)
                {
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadNhanVienData(); // Tải lại dữ liệu nhân viên sau khi thêm mới
                }
                else
                {
                    MessageBox.Show("Lỗi khi thêm nhân viên. Vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu người dùng chưa chọn dòng trong DataGridView
            if (dgvNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy mã nhân viên từ dòng đã chọn
            string maNhanVien = dgvNhanVien.SelectedRows[0].Cells["MaNhanVien"].Value.ToString();

            // Xác nhận trước khi xóa
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Gọi phương thức xóa nhân viên từ BLL
                    bool isDeleted = bllNhanVien.XoaNhanVien(maNhanVien);

                    if (isDeleted)
                    {
                        // Nếu xóa thành công, tải lại dữ liệu nhân viên
                        LoadNhanVienData();
                        MessageBox.Show("Nhân viên đã được xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa nhân viên. Vui lòng thử lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra khi xóa nhân viên: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_excel_Click(object sender, EventArgs e)
        {
            // Mở hộp thoại chọn tệp Excel
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                // Đọc dữ liệu từ tệp Excel
                try
                {
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    using (var package = new ExcelPackage(new FileInfo(filePath)))
                    {
                        var worksheet = package.Workbook.Worksheets[0]; // Chọn sheet đầu tiên
                        var rowCount = worksheet.Dimension.Rows;

                        // Tạo danh sách để lưu dữ liệu nhân viên từ Excel
                        var nhanVienList = new List<NhanVien>();

                        // Bắt đầu đọc từ dòng thứ 2, bỏ qua tiêu đề
                        for (int row = 2; row <= rowCount; row++)
                        {
                            // Đọc dữ liệu từng cột từ Excel
                            string maNhanVien = worksheet.Cells[row, 1].Text;
                            string hoTen = worksheet.Cells[row, 2].Text;
                            string chucVu = worksheet.Cells[row, 3].Text;
                            string diaChiLienLac = worksheet.Cells[row, 4].Text;
                            string dienThoai = worksheet.Cells[row, 5].Text;
                            string email = worksheet.Cells[row, 6].Text;

                            int maTaiKhoan;
                            if (!int.TryParse(worksheet.Cells[row, 7].Text, out maTaiKhoan))
                            {
                                maTaiKhoan = 0; // Giá trị mặc định nếu không chuyển đổi được
                            }

                            string hinhanh = worksheet.Cells[row, 8].Text;
                            DateTime ngaySinh;
                            if (!DateTime.TryParseExact(worksheet.Cells[row, 9].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngaySinh))
                            {
                                // Xử lý trường hợp ngày sinh không hợp lệ
                                MessageBox.Show("Ngày sinh không hợp lệ ở dòng " + row);
                                continue;
                            }

                            string gioiTinh = worksheet.Cells[row, 10].Text;

                            // Thêm vào danh sách nhân viên
                            nhanVienList.Add(new NhanVien
                            {
                                MaNhanVien = maNhanVien,
                                HoTen = hoTen,
                                ChucVu = chucVu,
                                DiaChiLienLac = diaChiLienLac,
                                DienThoai = dienThoai,
                                Email = email,
                                MaTaiKhoan = maTaiKhoan,
                                Hinhanh = hinhanh,
                                NgaySinh = ngaySinh,
                                GioiTinh = gioiTinh
                            });
                        }

                        // Lưu vào cơ sở dữ liệu qua LINQ to SQL
                        db.NhanViens.InsertAllOnSubmit(nhanVienList);
                        db.SubmitChanges();

                        LoadNhanVienData();  // Hàm này để tải lại dữ liệu vào DataGridView

                        MessageBox.Show("Dữ liệu đã được nhập và lưu vào cơ sở dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi đọc file Excel: " + ex.Message);
                }
            }
        }

        private void btn_LuuCapNhat_Click(object sender, EventArgs e)
        {
            BLL_NhanVien bllnhanvien = new BLL_NhanVien();
            // Kiểm tra nếu các ComboBox chưa được chọn giá trịg
            string diaChiLienLac = string.Empty;

            if (comboBoxTinhThanh.SelectedItem == null || comboBoxHuyen.SelectedItem == null || comboBoxXa.SelectedItem == null)
            {
                diaChiLienLac = comboBoxXa.Text + ", " + comboBoxHuyen.Text + ", " + comboBoxTinhThanh.Text;
            }
            else
            {
                diaChiLienLac = comboBoxXa.SelectedItem.ToString() + ", " + comboBoxHuyen.SelectedItem.ToString() + ", " + comboBoxTinhThanh.SelectedItem.ToString();
            }

            if (!int.TryParse(txt_matakhoanv.Text, out int maTaiKhoan))
            {
                MessageBox.Show("Mã tài khoản không hợp lệ. Vui lòng nhập số nguyên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo đối tượng NhanVien và lấy dữ liệu từ các TextBox, ComboBox, RadioButton và DateTimePicker
            NhanVien nhanVienDto = new NhanVien
            {
                MaNhanVien = txt_MaNhanVien.Text, // Nếu có TextBox chứa mã nhân viên, thay thế theo nhu cầu
                HoTen = txt_hoTen.Text,
                GioiTinh = radio_Nam.Checked ? "Nam" : (radio_Nu.Checked ? "Nữ" : ""), // Xử lý giới tính dựa trên RadioButton
                NgaySinh = dtp_ngaySinh1.Value,
                Email = txt_email1.Text,
                DienThoai = txt_soDienThoai1.Text,
                Hinhanh = label_hinhanh.Text,
                ChucVu = txt_ChucVu.Text,
                MaTaiKhoan = maTaiKhoan,
                DiaChiLienLac = diaChiLienLac,
                // Kết hợp thông tin từ các ComboBox về địa chỉ
            };

            // Gọi phương thức UpdateNhanVien từ BLL
            bool isUpdated = bllnhanvien.UpdateNhanVien(nhanVienDto);


            // Kiểm tra kết quả cập nhật và hiển thị thông báo
            if (isUpdated)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo");
                LoadNhanVienData1();  // Hàm này để tải lại dữ liệu vào DataGridView
                dgvNhanVien.DataSource = null; // Xóa dữ liệu cũ
                dgvNhanVien.DataSource = bllnhanvien.GetNhanVien(); // Tải dữ liệu mới
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin không thành công.", "Thông báo lỗi");
            }
        }

        private void dataGridView_taikhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng có click vào hàng hợp lệ không
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView_taikhoan.Rows[e.RowIndex];
                // Gán giá trị vào các TextBox và ComboBox
                txt_matakhoanv.Text = row.Cells["MaTaiKhoan"].Value.ToString();
                txt_tentaikhoan.Text = row.Cells["TenDangNhap"].Value?.ToString() ?? ""; // Lấy tài khoản
                txt_matkhau.Text = row.Cells["MatKhau"].Value?.ToString() ?? "";      // Lấy mật khẩu
                cbm_chucvu.Text = row.Cells["VaiTro"].Value?.ToString() ?? "";            // Lấy chức vụ
            }
        }

        private void LoadChucVu()
        {
            // Giả sử bạn có danh sách chức vụ cứng
            List<string> chucVuList = new List<string> { "Admin", "User" };

            cbm_chucvu.Items.Clear();
            cbm_chucvu.Items.AddRange(chucVuList.ToArray());
        }

        public string EncryptMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Chuyển đổi byte[] sang chuỗi hexa
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        private void btn_themtaikhoan_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra tính hợp lệ
                if (string.IsNullOrWhiteSpace(txt_tentaikhoan.Text) || string.IsNullOrWhiteSpace(txt_matkhau.Text) || cbm_chucvu.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo đối tượng TaiKhoan
                TaiKhoan newTaiKhoan = new TaiKhoan
                {
                    TenDangNhap = txt_tentaikhoan.Text.Trim(),
                    MatKhau = txt_matkhau.Text.Trim(),
                    VaiTro = cbm_chucvu.SelectedItem.ToString(),
                    MaHoaDuLieu = EncryptMD5(txt_matkhau.Text.Trim())
                };

                // Gọi phương thức thêm tài khoản từ BLL
                bool result = bllNhanVien.AddTaiKhoan(newTaiKhoan);

                if (result)
                {
                    MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Tải lại danh sách tài khoản
                    LoadTaiKhoan();
                }
                else
                {
                    MessageBox.Show("Không thể thêm tài khoản. Vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_matakhoanv_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txt_matakhoanv.Text, out int maTaiKhoan))
            {
                // Gọi phương thức BLL để lấy tài khoản dựa trên mã tài khoản
                TaiKhoan taiKhoan = bllNhanVien.GetTaiKhoanById(maTaiKhoan.ToString());

                if (taiKhoan != null)
                {
                    // Hiển thị thông tin tài khoản lên các điều khiển
                    txt_tentaikhoan.Text = taiKhoan.TenDangNhap;
                    txt_matkhau.Text = taiKhoan.MatKhau;
                    cbm_chucvu.Text = taiKhoan.VaiTro;
                }
                else
                {
                    // Xóa thông tin nếu không tìm thấy tài khoản
                    txt_tentaikhoan.Text = string.Empty;
                    txt_matkhau.Text = string.Empty;
                    cbm_chucvu.SelectedIndex = -1;
                    MessageBox.Show("Không tìm thấy tài khoản tương ứng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // Nếu giá trị nhập không hợp lệ, xóa thông tin tài khoản
                txt_tentaikhoan.Text = string.Empty;
                txt_matkhau.Text = string.Empty;
                cbm_chucvu.SelectedIndex = -1;
            }
        }

        private void btn_CustomRedButton2_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu người dùng đã chọn một tài khoản trong DataGridView
            if (dataGridView_taikhoan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // Lấy mã tài khoản từ dòng đã chọn và chuyển nó sang kiểu int
            int maTaiKhoan;
            if (!int.TryParse(dataGridView_taikhoan.SelectedRows[0].Cells["MaTaiKhoan"].Value.ToString(), out maTaiKhoan))
            {
                MessageBox.Show("Mã tài khoản không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Xác nhận xóa tài khoản
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Gọi phương thức xóa tài khoản từ BLL
                    bool isDeleted = bllNhanVien.XoaTaiKhoan(maTaiKhoan);

                    if (isDeleted)
                    {
                        MessageBox.Show("Tài khoản đã được xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTaiKhoan(); // Tải lại danh sách tài khoản sau khi xóa
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa tài khoản. Vui lòng thử lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra khi xóa tài khoản: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            // Kiểm tra tính hợp lệ của các trường dữ liệu
            if (string.IsNullOrWhiteSpace(txt_tentaikhoan.Text) || string.IsNullOrWhiteSpace(txt_matkhau.Text) || cbm_chucvu.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin tài khoản.");
                return;
            }

            // Kiểm tra tính hợp lệ của mật khẩu (ví dụ độ dài mật khẩu)
            if (txt_matkhau.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra và lấy giá trị VaiTro từ ComboBox
            string vaiTro = cbm_chucvu.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(vaiTro))
            {
                MessageBox.Show("Vui lòng chọn vai trò người dùng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maTaiKhoan;
            if (!int.TryParse(txt_matakhoanv.Text, out maTaiKhoan))
            {
                MessageBox.Show("Mã tài khoản không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo đối tượng TaiKhoan từ các giá trị nhập vào (không sử dụng MaTaiKhoan)
            TaiKhoan taiKhoanDto = new TaiKhoan
            {
                MaTaiKhoan = maTaiKhoan,
                TenDangNhap = txt_tentaikhoan.Text.Trim(),
                MatKhau = txt_matkhau.Text.Trim(),
                VaiTro = cbm_chucvu.SelectedItem.ToString(),
                MaHoaDuLieu = EncryptMD5(txt_matkhau.Text.Trim())             // Vai trò người dùng
            };

            // Gọi phương thức cập nhật tài khoản từ BLL
            bool isUpdated = bllNhanVien.UpdateTaiKhoan(taiKhoanDto);

            if (isUpdated)
            {
                MessageBox.Show("Cập nhật tài khoản thành công!", "Thông báo");
                LoadTaiKhoan();  // Tải lại danh sách tài khoản sau khi cập nhật
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản không thành công.", "Thông báo lỗi");
            }
        }
    }
}
