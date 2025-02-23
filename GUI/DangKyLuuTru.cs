using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Control;
using BLL;
using DTO;
using System.IO;
using System.Xml.Linq;
namespace GUI
{
    public partial class DangKyLuuTru : Form
    {
        BLL_QL_LuuTru bllQuanLyLuuTru= new BLL_QL_LuuTru();
        BLL_QL_Phong bllPhong= new BLL_QL_Phong();
        private PictureBox selectedPictureBox;

        private FormNV_TuyenSinh _mainForm; // Biến để lưu tham chiếu tới FormMain
      //  private ThongTinLuuTru thongTinLuuTruForm;

        public DangKyLuuTru(FormNV_TuyenSinh mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm; // Lưu tham chiếu

            panel2.AutoScroll = true;
            btn_daCoHoSo.Click += Btn_daCoHoSo_Click;
            LoadCombo_Phong();
          
            Load_LoaiPhong();
            LoadComBo_HinhThucThanhToan();
            btn_DangKi.Click += Btn_DangKi_Click;
            btn_ChonFile.Click += Btn_ChonFile_Click;

            picture_CCCDSau.Click += Picture_Click;
            picture_CCCDTruoc.Click += Picture_Click;
            picture_HinhDaiDien.Click += Picture_Click;

            cbo_LoaiPhong.SelectedIndexChanged += Cbo_LoaiPhong_SelectedIndexChanged;
            cbo_phong.SelectedIndexChanged += Cbo_phong_SelectedIndexChanged;
            rdo_nam.CheckedChanged += Rdo_GioiTinh_CheckedChanged;
            rdo_Nu.CheckedChanged += Rdo_GioiTinh_CheckedChanged;
          //  txt_MaSoSV.Leave += Txt_MaSoSV_Leave;

            txt_MaSoSV.TextChanged += Txt_MaSoSV_TextChanged;
        }

        private void Txt_MaSoSV_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_MaSoSV.Text))
            {
               SetControlsEnabled(true);
            }
            else
            {
                SetControlsEnabled(false);

            }
        }

        //private void Txt_MaSoSV_Leave(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txt_MaSoSV.Text))
        //    {
        //        // Nếu có mã sinh viên, cho phép nhập các thông tin khác
        //        SetControlsEnabled(true);
        //        txt_SDT.Enabled = true;
        //    }
        //    else
        //    {
        //        // Nếu chưa có mã sinh viên, disable các trường nhập liệu
        //        SetControlsEnabled(false);
        //    }

        //}

        public DangKyLuuTru(FormNV_TuyenSinh mainForm, SinhVien sinhVien) : this(mainForm)
        {
         //   _mainForm = mainForm; // Lưu tham chiếu

            SetControlsEnabled(false);

           
            if (sinhVien != null)
            {
                // Điền thông tin sinh viên vào các TextBox tương ứng
                txt_MaSoSV.Text = sinhVien.MaSinhVien;
                txt_HoVaTen.Text = sinhVien.HoTen;
                txt_CCCD.Text = sinhVien.CCCD;
                txt_Email.Text = sinhVien.Email;
                txt_NoiSinh.Text = sinhVien.NoiSinh;
                txt_HoKhauThuongTru.Text = sinhVien.HoKhauThuongTru;
             //   txt_SDT.Text = sinhVien.s;
                txt_GhiChu.Text = sinhVien.GhiChu;
                picker_NgaySinh.Value = sinhVien.NgaySinh;

                // Thiết lập giới tính
                if (sinhVien.GioiTinh == "Nam")
                {
                    rdo_nam.Checked = true;
                }
                else if (sinhVien.GioiTinh == "Nu")
                {
                    rdo_Nu.Checked = true;
                }

                // Gán các hình ảnh nếu có
                if (!string.IsNullOrEmpty(sinhVien.HinhNhanDien))
                {
                    string imagePath = Path.Combine("D:\\DoAnChuyenNganh\\Resources", sinhVien.HinhNhanDien);
                    if (File.Exists(imagePath))
                    {
                        picture_HinhDaiDien.Image = Image.FromFile(imagePath);
                        picture_HinhDaiDien.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }

                if (!string.IsNullOrEmpty(sinhVien.HinhCCCDTruoc))
                {
                    string imagePath = Path.Combine("D:\\DoAnChuyenNganh\\Resources", sinhVien.HinhCCCDTruoc);
                    if (File.Exists(imagePath))
                    {
                        picture_CCCDTruoc.Image = Image.FromFile(imagePath);
                        picture_CCCDTruoc.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }

                if (!string.IsNullOrEmpty(sinhVien.HinhCCCDSau))
                {
                    string imagePath = Path.Combine("D:\\DoAnChuyenNganh\\Resources", sinhVien.HinhCCCDSau);
                    if (File.Exists(imagePath))
                    {
                        picture_CCCDSau.Image = Image.FromFile(imagePath);
                        picture_CCCDSau.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
            }
        }


        private void SetControlsEnabled(bool enabled)
        {
            // Đảm bảo chỉ có các điều khiển cần thiết được thay đổi trạng thái
            txt_HoVaTen.Enabled = enabled;
            txt_CCCD.Enabled = enabled;
            txt_Email.Enabled = enabled;
            txt_NoiSinh.Enabled = enabled;
            txt_HoKhauThuongTru.Enabled = enabled;
            txt_SDT.Enabled = enabled;
            txt_GhiChu.Enabled = enabled;
            picker_NgaySinh.Enabled = enabled;
            cbo_LoaiPhong.Enabled = enabled;
            cbo_phong.Enabled = enabled;
           
            picker_NgayDK.Enabled = enabled;
            picker_NgayVao.Enabled = enabled;
            picker_NgayRa.Enabled = enabled;
            cbo_HinhThucThanhToan.Enabled = enabled;
            checkbox_TruongPhong.Enabled = enabled;
            btn_DangKi.Enabled = enabled;
            btn_ChonFile.Enabled = enabled;

            // Disable txt_MaSoSV and btn_daCoHoSo
            txt_MaSoSV.Enabled = true;
            btn_daCoHoSo.Enabled = true;
        }


        private void Cbo_phong_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem giá trị phòng đã chọn có hợp lệ không
            if (cbo_phong.SelectedIndex != -1)
            {
                // Lấy mã phòng đã chọn
                string maPhong = cbo_phong.SelectedValue.ToString();

                //// Lọc các giường còn trống trong phòng
                //List<int?> giuongConTrong = LayGiuongConTrong(maPhong);

                //// Cập nhật ComboBox giường chỉ hiển thị những giường còn trống
                //cbo_giuong.DataSource = giuongConTrong;

                //// Đảm bảo rằng giá trị được chọn trong ComboBox giường hợp lệ
                //if (giuongConTrong.Count > 0)
                //{
                //    cbo_giuong.SelectedIndex = 0; // Chọn giường đầu tiên nếu có giường trống
                //}
                //else
                //{
                //    cbo_giuong.SelectedIndex = -1; // Nếu không có giường trống, không chọn gì
                //}
            }
        }


        //private List<int?> LayGiuongConTrong(string maPhong)
        //{
        //    List<int?> giuongConTrong = new List<int?>();

        //    // Lấy danh sách giường đã được đăng ký trong phòng từ bảng DangKyPhong
        //    var giuongDaDangKy = bllQuanLyLuuTru.LayGiuongDaDangKy(maPhong);

        //    // Kiểm tra nếu giường đã đăng ký là null, nếu null khởi tạo thành danh sách trống
        //    if (giuongDaDangKy == null)
        //    {
        //        giuongDaDangKy = new List<int?>(); // Khởi tạo danh sách trống
        //    }

        //    // Giả sử rằng giường trống trong phòng là giường từ 1 đến N. (Ví dụ phòng có 10 giường)
        //    // Bạn có thể thay thế phần này bằng cách lấy giường từ cơ sở dữ liệu nếu có thông tin giường cụ thể
        //    for (int i = 1; i <= 10; i++) // Giả sử phòng có 10 giường
        //    {
        //        if (!giuongDaDangKy.Contains(i))
        //        {
        //            giuongConTrong.Add(i); // Thêm giường trống vào danh sách
        //        }
        //    }

        //    return giuongConTrong;
        //}


        private void Rdo_GioiTinh_CheckedChanged(object sender, EventArgs e)
        {
            if (rdo_nam.Checked)
            {
                // Nếu chọn Nam, chỉ hiển thị các phòng từ tầng 6 trở lên
                LoadPhongTheoGioiTinh("Nam");
            }
            else if (rdo_Nu.Checked)
            {
                // Nếu chọn Nữ, chỉ hiển thị các phòng từ tầng 1 đến 5
                LoadPhongTheoGioiTinh("Nu");
            }
        }


        private void LoadPhongTheoGioiTinh(string gioiTinh)
        {
            // Lấy danh sách phòng đã lọc theo giới tính từ BLL
            List<Phong> filteredPhongList = bllQuanLyLuuTru.GetPhongTheoGioiTinh(gioiTinh);

            // Cập nhật ComboBox với danh sách phòng đã lọc
            if (filteredPhongList != null && filteredPhongList.Count > 0)
            {
                // Cập nhật ComboBox với danh sách phòng đã lọc
                cbo_phong.DataSource = filteredPhongList;
                cbo_phong.ValueMember = "MaPhong";
                cbo_phong.DisplayMember = "MaPhong";
                cbo_phong.SelectedIndex = -1; // Đặt lại giá trị của combobox
            }
            else
            {
                MessageBox.Show("Không tìm thấy phòng phù hợp với giới tính đã chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbo_phong.DataSource = null;
            }
        }
        //private void Cbo_LoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Kiểm tra nếu có giá trị chọn trong ComboBox
        //        if (cbo_LoaiPhong.SelectedValue != null)
        //        {
        //            string selectedText = cbo_LoaiPhong.SelectedValue.ToString();
        //            if (int.TryParse(selectedText, out int loaiPhong))
        //            {
        //                // Lấy đơn giá từ BLL
        //                decimal donGia = bllQuanLyLuuTru.LayDonGia(loaiPhong);

        //                // Kiểm tra nếu đơn giá hợp lệ
        //                if (donGia != -1)
        //                {
        //                    txt_DonGia.Text = donGia.ToString();
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Không tìm thấy đơn giá cho loại phòng này.");
        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show($"Giá trị chọn không hợp lệ: {selectedText}. Vui lòng chọn lại.");
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Không có giá trị nào được chọn. Vui lòng chọn một loại phòng.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi: " + ex.Message);
        //    }
        //}

        private void Cbo_LoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu có giá trị chọn trong ComboBox Loại Phòng
                if (cbo_LoaiPhong.SelectedValue != null)
                {
                    int selectedLoaiPhong = int.Parse(cbo_LoaiPhong.SelectedValue.ToString());

                    //// Lấy danh sách phòng theo mã loại phòng
                    //List<Phong> danhSachPhong = bllPhong.GetPhongTheoLoai(selectedLoaiPhong);

                    //if (danhSachPhong != null && danhSachPhong.Count > 0)
                    //{
                    //    cbo_phong.DataSource = danhSachPhong;
                    //    cbo_phong.ValueMember = "MaPhong";
                    //    cbo_phong.DisplayMember = "MaPhong";
                    //    cbo_phong.SelectedIndex = -1; // Đặt lại giá trị của combobox để không chọn phòng nào
                    //}
                    //else
                    //{
                    //   // MessageBox.Show("Không tìm thấy phòng phù hợp cho loại phòng đã chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    cbo_phong.DataSource = null;
                    //}

                    // Lấy đơn giá từ loại phòng đã chọnh
                    decimal donGia = bllQuanLyLuuTru.LayDonGia(selectedLoaiPhong);
                    txt_DonGia.Text = donGia.ToString();
                }
                else
                {
                   // MessageBox.Show("Vui lòng chọn một loại phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_ChonFile_Click(object sender, EventArgs e)
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
                    selectedPictureBox.Image = Image.FromFile(selectedImagePath);
                    selectedPictureBox.SizeMode = PictureBoxSizeMode.Zoom;


                    // Lưu đường dẫn vào đúng biến
                    if (selectedPictureBox == picture_HinhDaiDien)
                        hinhNhanDienPath = selectedImagePath;
                    else if (selectedPictureBox == picture_CCCDTruoc)
                        hinhCCCDTruocPath = selectedImagePath;
                    else if (selectedPictureBox == picture_CCCDSau)
                        hinhCCCDSauPath = selectedImagePath;
                }
            }
        }

        private void Picture_Click(object sender, EventArgs e)
        {
            selectedPictureBox = sender as PictureBox;//Luu pictureBox đang chọn
        }

        private string hinhNhanDienPath;
        private string hinhCCCDTruocPath; 
        private string hinhCCCDSauPath;

        private string MaDangKyPhongTuDong()
        {
            string lastMaDangKy = bllQuanLyLuuTru.GetMaxMaPhong(); // Lấy mã đăng ký cuối cùng (VD: DKP009)

            if (string.IsNullOrEmpty(lastMaDangKy))
            {
                return "DKP001"; // Mã đầu tiên nếu chưa có dữ liệu
            }

            try
            {
                // Lấy phần số từ mã đăng ký 
                string numberPart = lastMaDangKy.Substring(3);

                // Kiểm tra nếu phần số là một số hợp lệ
                if (int.TryParse(numberPart, out int lastNumber))
                {
                    // Tăng giá trị số và giữ định dạng 3 chữ số
                    int newNumber = lastNumber + 1;
                    string newMaDangKy = "DKP" + newNumber.ToString("D3");

                    return newMaDangKy; // Trả về mã mới 
                }
                else
                {
                    // Trường hợp nếu phần số không hợp lệ, trả về mã đầu tiên
                    return "DKP001";
                }
            }
            catch (Exception ex)
            {
                // Log lỗi nếu có
                Console.WriteLine("Lỗi khi tạo mã đăng ký phòng tự động: " + ex.Message);
                return "DKP001"; // Trả về mã đầu tiên nếu có lỗi
            }
        }





        private void Btn_DangKi_Click(object sender, EventArgs e)
        {
            // Kiểm tra sinh viên đủ 18 tuổi
            DateTime today = DateTime.Today;
            int age = today.Year - picker_NgaySinh.Value.Year;
            if (picker_NgaySinh.Value.Date > today.AddYears(-age)) age--; // Điều chỉnh tuổi nếu sinh viên chưa đến sinh nhật trong năm nay

            if (age < 15)
            {
                MessageBox.Show("Độ tuổi đăng ký không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra điều kiện ngày bắt đầu ở và ngày đăng ký
            DateTime ngayDK = picker_NgayDK.Value;
            DateTime ngayBD = picker_NgayVao.Value;
            DateTime ngayKT = picker_NgayRa.Value;

            if (ngayBD < ngayDK)
            {
                MessageBox.Show("Ngày bắt đầu ở không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ngayDK >= ngayKT || ngayBD >= ngayKT)
            {
                MessageBox.Show("Ngày kết thúc không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (string.IsNullOrEmpty(txt_MaSoSV.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã sinh viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra số lượng sinh viên trong phòng
            string maPhong = cbo_phong.SelectedValue?.ToString();
            int studentCount = bllQuanLyLuuTru.DemSoSV(maPhong);
            int soSinhVienToiDa = bllPhong.LaySoSVToiDa(maPhong);

            if (studentCount >= soSinhVienToiDa)
            {
                MessageBox.Show("Phòng đã đạt số lượng tối đa ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // T
            SinhVien sv = new SinhVien
            {
                MaSinhVien = txt_MaSoSV.Text,
                HoTen = txt_HoVaTen.Text,
                TruongPhong = checkbox_TruongPhong.Checked,
                NgaySinh = picker_NgaySinh.Value,
                GioiTinh = rdo_nam.Checked ? "Nam" : "Nu",
                NoiSinh = txt_NoiSinh.Text,
                HoKhauThuongTru = txt_HoKhauThuongTru.Text,
                Email = txt_Email.Text,
                SDT=txt_SDT.Text,
                CCCD = txt_CCCD.Text,
                GhiChu = txt_GhiChu.Text,
                HinhNhanDien = string.IsNullOrEmpty(hinhNhanDienPath) ? "user.png" : Path.GetFileName(hinhNhanDienPath),
                HinhCCCDTruoc = string.IsNullOrEmpty(hinhCCCDTruocPath) ? "" : Path.GetFileName(hinhCCCDTruocPath),
                HinhCCCDSau = string.IsNullOrEmpty(hinhCCCDSauPath) ? "" : Path.GetFileName(hinhCCCDSauPath)
            };

            txt_MaPhieu.Text = MaDangKyPhongTuDong();
         
            DangKyPhong dkphong = new DangKyPhong
            {
                MaDangKyPhong = txt_MaPhieu.Text,
                MaSinhVien = sv.MaSinhVien,
                MaPhong = cbo_phong.SelectedValue.ToString(),
                NgayDK = picker_NgayDK.Value,
                NgayBD = picker_NgayVao.Value,
                NgayKT = picker_NgayRa.Value,
                LoaiPhong_=cbo_LoaiPhong.SelectedValue?.ToString(),
                DonGiaPhong=decimal.Parse(txt_DonGia.Text),
                HinhThucThanhToan=cbo_HinhThucThanhToan.SelectedItem.ToString(),
           
             //   Tang = int.Parse(maPhong.Substring(4, 2)),
            };

            bool result = bllQuanLyLuuTru.ThemDangKyPhong(dkphong, sv);

            if (result)
            {
                MessageBox.Show("Đăng ký lưu trú thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi đăng ký lưu trú.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Btn_daCoHoSo_Click(object sender, EventArgs e)
        {
           // _mainForm.LoadThongTinLuuTruForm();
            _mainForm.OpenChildForm(new ThongTinLuuTru()); // Gọi OpenChildForm để mở ThongTinLuuTru
       
        }   

        public void LoadCombo_Phong()
        {
            cbo_phong.DataSource = bllQuanLyLuuTru.GetPhong();
            cbo_phong.ValueMember= "MaPhong";
            cbo_phong.DisplayMember= "MaPhong";
        }
     
        private void LoadComBo_HinhThucThanhToan()
        {
            // Xóa dữ liệu trước đó nếu có
            cbo_HinhThucThanhToan.Items.Clear();

            // Thêm các tùy chọn mặc định
            cbo_HinhThucThanhToan.Items.Add("Thanh toán tiền mặt");
            cbo_HinhThucThanhToan.Items.Add("Thanh toán chuyển khoản");

            // Thiết lập tùy chọn mặc định   là "Thanh Toán tiền mặt"
            cbo_HinhThucThanhToan.SelectedIndex = 0;
        }

        public void Load_LoaiPhong()
        {
            cbo_LoaiPhong.DataSource = bllQuanLyLuuTru.GetLoais();
            cbo_LoaiPhong.ValueMember = "MaLoaiPhong";
            cbo_LoaiPhong.DisplayMember = "TenLoaiPhong";

        }

       
    }
}
