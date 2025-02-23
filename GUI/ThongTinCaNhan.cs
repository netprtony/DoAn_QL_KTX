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




namespace GUI
{
    public partial class ThongTinCaNhan : Form
    {
      //  private TextBox txt_xacnhanMK; // Khai báo biến cho txt_xacnhanMK
        public ThongTinCaNhan()
        {
            InitializeComponent();
            this.Load += ThongTinCaNhan_Load;
            btn_LuuMatKhau.Click += Btn_LuuMatKhau_Click;
            btn_DoiMatKhau.Click += Btn_DoiMatKhau_Click;
            btn_CapNhatTTCN.Click += Btn_CapNhatTTCN_Click;
            btn__ChonAnh.Click += Btn__ChonAnh_Click;
            btn_LuuCapNhat.Click += Btn_LuuCapNhat_Click;
            // Khóa các TextBox khi form hiển thị
            SetTextBoxEnabled(false);
            txt_MatKhau2.Visible = false;
            label_xacnhan.Visible = false;
            txt_MaNhanVien.Enabled = false;
            txt_ChucVu.Enabled = false;
            // Kiểm tra xem panel2 đã được tạo chưa
            if (panel2 == null)
            {
                panel2 = new Panel();
                panel2.Dock = DockStyle.Fill;
                panel2.AutoScroll = true;
                this.Controls.Add(panel2); // Thêm panel2 vào form (hoặc container khác)
            }

            // Danh sách các điều khiển cần di chuyển vào panel2
            List<System.Windows.Forms.Control> controlsToMoveToPanel2 = new List<System.Windows.Forms.Control>();

            // Lặp qua các điều khiển trong form (hoặc container) hiện tại
            foreach (System.Windows.Forms.Control control in this.Controls)
            {
                if (control != panel2) // Không di chuyển panel2 vào chính nó
                {
                    controlsToMoveToPanel2.Add(control);
                }
            }

            // Di chuyển các điều khiển vào panel2
            foreach (System.Windows.Forms.Control control in controlsToMoveToPanel2)
            {
                this.Controls.Remove(control); // Xóa điều khiển khỏi container hiện tại
                panel2.Controls.Add(control); // Thêm điều khiển vào panel2
            }

        }

        private void Btn_LuuCapNhat_Click(object sender, EventArgs e)
        {
            BLL_DangNhap bllDangNhap = new BLL_DangNhap();
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

            // Tạo đối tượng NhanVien và lấy dữ liệu từ các TextBox, ComboBox, RadioButton và DateTimePicker
            NhanVien nhanVienDto = new NhanVien
            {
                MaNhanVien = txt_MaNhanVien.Text, // Nếu có TextBox chứa mã nhân viên, thay thế theo nhu cầu
                HoTen = txt_hoTen1.Text,
                GioiTinh = radio_Nam.Checked ? "Nam" : (radio_Nu.Checked ? "Nữ" : ""), // Xử lý giới tính dựa trên RadioButton
                NgaySinh = dtp_ngaySinh1.Value,
                Email = txt_email1.Text,
                DienThoai = txt_soDienThoai1.Text,
                Hinhanh = label_hinhanh.Text,
                ChucVu = txt_ChucVu.Text,
               
            DiaChiLienLac = diaChiLienLac,
                // Kết hợp thông tin từ các ComboBox về địa chỉ
            };

            // Gọi phương thức UpdateNhanVien từ BLL
            bool isUpdated = bllDangNhap.UpdateNhanVien(nhanVienDto);

            // Kiểm tra kết quả cập nhật và hiển thị thông báo
            if (isUpdated)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo");
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin không thành công.", "Thông báo lỗi");
            }

        }

        private void Btn__ChonAnh_Click(object sender, EventArgs e)
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
                    string imagePath = Path.Combine("D:\\DACN\\DACN_QL_KTX\\DACN_QL_KTX\\DoAn_QL_KTX\\GUI\\Model\\avatars", imageFileName);

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

        private void Btn_CapNhatTTCN_Click(object sender, EventArgs e)
        {
            UnlockComboBoxesForEditing();

        }

        private void Btn_DoiMatKhau_Click(object sender, EventArgs e)
        {
            // Mở khóa txt_MatKhau1, xóa nội dung và yêu cầu nhập mật khẩu mới
            txt_MatKhau1.Enabled = true;
            txt_MatKhau1.Text = ""; // Xóa dữ liệu cũ
            MessageBox.Show("Bạn hãy nhập mật khẩu mới.", "Thông báo");

            // Hiện txt_xacnhanMK và đẩy các nút xuống
            txt_MatKhau2.Visible = true;
            label_xacnhan.Visible = true;
            // Đẩy các nút xuống dưới
            btn_DoiMatKhau.Location = new Point(btn_DoiMatKhau.Location.X, btn_DoiMatKhau.Location.Y + 30); // Đẩy nút Đổi Mật Khẩu xuống
            btn_LuuMatKhau.Location = new Point(btn_LuuMatKhau.Location.X, btn_LuuMatKhau.Location.Y + 30); // Đẩy nút Lưu xuống
        }
        // Hàm cho phép hoặc khóa người dùng thao tác trên các TextBox
        private void SetTextBoxEnabled(bool isEnabled)
        {
            txt_maTK.Enabled = isEnabled;
            txt_tenDangNhap1.Enabled = isEnabled;
            txt_MatKhau1.Enabled = isEnabled;
            
        }
        public string GenerateOtp()
        {
            Random random = new Random();
            int otp = random.Next(100000, 999999); // Tạo OTP 6 chữ số
            return otp.ToString();
        }

        private void Btn_LuuMatKhau_Click(object sender, EventArgs e)
        {
            // Thêm logic để lưu mật khẩu mới ở đây

            int maTaiKhoan = int.Parse(txt_maTK.Text);
            BLL_DangNhap bllDangNhap = new BLL_DangNhap();

            // Gọi phương thức để lấy mật khẩu hiện tại
            string currentPassword = bllDangNhap.GetPasswordByMaTaiKhoan(maTaiKhoan);

            if (currentPassword == null)
            {
                MessageBox.Show("Không tìm thấy tài khoản với mã tài khoản đã cho.");
                return;
            }

            string newPassword = txt_MatKhau1.Text; // TextBox chứa mật khẩu mới
            string confirmPassword = txt_MatKhau2.Text; // Mật khẩu xác nhận
            string hashedCurrentPassword = Password.Create_MD5(newPassword);

            // Kiểm tra mật khẩu mới và mật khẩu xác nhận có khớp nhau không
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu không khớp. Vui lòng kiểm tra lại.", "Thông báo lỗi");
                return; // Dừng lại nếu không khớp
            }

            // Tạo OTP và gửi qua email
            string otp = GenerateOtp();
            string emailAddress = "nhatquyenxt2804@gmail.com"; // Địa chỉ email của người dùng (có thể lấy từ dữ liệu tài khoản)

            // Tạo đối tượng EmailService để gửi OTP
            EmailService emailService = new EmailService();
            emailService.SendOtp(emailAddress, otp); // Gọi phương thức gửi OTP

            // Yêu cầu người dùng nhập mã OTP
            string userEnteredOtp = PromptForOtp(); // Gọi phương thức để người dùng nhập OTP

            if (userEnteredOtp != otp)
            {
                MessageBox.Show("Mã OTP không hợp lệ. Vui lòng thử lại.", "Thông báo lỗi");
                return; // Dừng lại nếu OTP không khớp
            }

            // Gọi phương thức đổi mật khẩu
            bool result = bllDangNhap.ChangePassword(maTaiKhoan, hashedCurrentPassword, newPassword);

            // Thông báo cho người dùng
            if (result)
            {
                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo");
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu không thành công. Vui lòng kiểm tra lại mật khẩu hiện tại.", "Thông báo lỗi");
            }

            SetTextBoxEnabled(false);
        }
        // Phương thức yêu cầu người dùng nhập mã OTP
        private string PromptForOtp()
        {
            string otp = Microsoft.VisualBasic.Interaction.InputBox("Nhập mã OTP đã gửi qua email:", "Xác nhận mã OTP", "", -1, -1);
            return otp;
        }

        private async void ThongTinCaNhan_Load(object sender, EventArgs e)
        {
           // txt_maTK.Text = DangNhap.MaTK.ToString();
            BLL_DangNhap bllDangNhap = new BLL_DangNhap();
            NhanVien nhanVien = await Task.Run(() => bllDangNhap.GetNhanVienByMaTaiKhoan(DangNhap.MaTK));
            TaiKhoan taikhoan = await Task.Run(() => bllDangNhap.GetTaiKhoanByMaTaiKhoan(DangNhap.MaTK));
            await DisplayEmployeeInfo(nhanVien);
            await DisplayTaiKhoanInfo(taikhoan);
            await LoadTinhThanhAsync();
        }
        private async Task<Image> LoadImageAsync(string imageFileName)
        {
            if (string.IsNullOrEmpty(imageFileName))
            {
                MessageBox.Show("Tên tệp hình ảnh rỗng hoặc null.", "Thông báo lỗi");
                return null;
            }

            // Sử dụng đường dẫn tuyệt đối để kiểm tra
            string imagePath = Path.Combine("D:\\DACN\\DACN_QL_KTX\\DACN_QL_KTX\\DoAn_QL_KTX\\GUI\\Model\\avatars", imageFileName);

            if (!File.Exists(imagePath))
            {
                MessageBox.Show("Không tìm thấy tệp hình ảnh.", "Thông báo lỗi");
                return null;
            }

            return Image.FromFile(imagePath);
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

        // Phương thức mở khóa ComboBox để cập nhật
        private async void UnlockComboBoxesForEditing()
        {
            comboBoxTinhThanh.Enabled = true;
            comboBoxHuyen.Enabled = true;
            comboBoxXa.Enabled = true;
            await LoadTinhThanhAsync();
        }

        private async Task DisplayEmployeeInfo(NhanVien nhanVien)
        {
            if (nhanVien != null)
            {
                txt_maTK.Text = DangNhap.MaTK.ToString();
                txt_MaNhanVien.Text = nhanVien.MaNhanVien;
                txt_hoTen1.Text = nhanVien.HoTen;
                txt_ChucVu.Text = nhanVien.ChucVu;
                txt_email1.Text = nhanVien.Email;   
                string imageFileName = nhanVien.Hinhanh; // Lấy tên tệp hình ảnh từ thuộc tính HinhAnh
                picture_Anh.Image = await LoadImageAsync(imageFileName); // Hiển thị ảnh lên picture_Anh
                txt_soDienThoai1.Text = nhanVien.DienThoai;
                // Hiển thị tên hình ảnh lên label_hinhanh
                label_hinhanh.Text = imageFileName; // Gán tên hình ảnh cho label_hinhanh

                // Cài đặt giới tính
                if (nhanVien.GioiTinh == "Nam")
                {
                    radio_Nam.Checked = true;
                }
                else
                {
                    radio_Nu.Checked = true;
                }

                // Thiết lập ngày sinh
                dtp_ngaySinh1.Value = nhanVien.NgaySinh ?? DateTime.Now;

                // Tách địa chỉ và thiết lập các combo box
                SetAddressToComboBoxes(nhanVien.DiaChiLienLac);

                // Tải các tỉnh, huyện, xã
              //  await LoadTinhThanhAsync();
              // await LoadHuyenAsync();
              // await LoadXaAsync();
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin nhân viên.", "Thông báo");
            }
        }
        private async Task DisplayTaiKhoanInfo(TaiKhoan taikhoan)
        {
            if(taikhoan!=null)
            {
                // txt_maTK.Text = DangNhap.MaTK.ToString();
                txt_tenDangNhap1.Text = taikhoan.TenDangNhap;
                txt_MatKhau1.Text = taikhoan.MatKhau;
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

        private async void comboBoxTinhThanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTinhThanh.SelectedItem is ComboBoxItem selectedItem)
            {
                int provinceId = (int)selectedItem.Value;
                await LoadHuyenAsync(provinceId);
            }
        }

        private async void comboBoxHuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxHuyen.SelectedItem is ComboBoxItem selectedItem)
            {
                int districtId = (int)selectedItem.Value;
                await LoadXaAsync(districtId);
            }

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void btn_DoiMatKhau_Click_1(object sender, EventArgs e)
        {

        }

        private void btn__ChonAnh_Click_1(object sender, EventArgs e)
        {

        }

        private void btn_LuuMatKhau_Click_1(object sender, EventArgs e)
        {

        }
    }
}
