using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DTO;
using BLL;
using Microsoft.VisualBasic.ApplicationServices;

namespace GUI
{
    public partial class Frm_ChiTietPhong : Form
    {
        public string maPhong;
        BLL_QL_LuuTru bll_luuTru = new BLL_QL_LuuTru();
        public FormNV_TuyenSinh _mainForm;  // Lưu trữ tham chiếu FormMain

        public Frm_ChiTietPhong()
        {
            InitializeComponent();
            this.Load += Frm_ChiTietPhong_Load;

        }

        private void Frm_ChiTietPhong_Load(object sender, EventArgs e)
        {
           // this.WindowState = FormWindowState.Maximized;
        }

        // Constructor nhận tham chiếu FormMain
        public Frm_ChiTietPhong(FormNV_TuyenSinh mainForm) : this()
        {
         
            if (mainForm == null)
            {
                MessageBox.Show("Không thể mở form khi tham chiếu _mainForm là null.");
                return;  // Đảm bảo _mainForm không bị null
            }
            _mainForm = mainForm;  // Lưu tham chiếu
        }

        public Frm_ChiTietPhong(FormNV_TuyenSinh mainForm,string maPhong) : this() // Gọi constructor mặc định
        {
            _mainForm = mainForm;
            this.maPhong = maPhong; // Lưu trữ maPhong
            LoadThongTinSinhVien();
        }

        // Method to load student data from the database
        public void LoadThongTinSinhVien()
        {
            // Lấy danh sách sinh viên trong phòng
            List<DangKyPhong> ds = bll_luuTru.LayThongTinSinhVien(maPhong);

            // Kiểm tra danh sách sinh viên có rỗng không
            if (ds.Count == 0)
            {
                MessageBox.Show("Không có sinh viên nào trong phòng này.");
                return;
            }

            foreach (var sinhVien in ds)
            {
                // Tạo panel cho sinh viên
                Panel panelSinhVien = new Panel
                {
                    Size = new Size(150, 150), // Kích thước panel
                    Margin = new Padding(10)   // Khoảng cách giữa các panel
                };

                // Tạo Button cho mỗi sinh viên
                Button btnSinhVien = new Button
                {
                    Size = new Size(100, 100),
                    Text = "", // Không có văn bản trên button
                    Image = GetImageForSinhVien(sinhVien), // Gán hình ảnh cho button
                    ImageAlign = ContentAlignment.MiddleCenter,
                    TextAlign = ContentAlignment.TopCenter,
                    Tag = sinhVien // Lưu đối tượng sinh viên trong Tag của button
                };

                // Thêm sự kiện click cho button
                btnSinhVien.Click += BtnSinhVien_Click;

                // Tạo Label hiển thị mã sinh viên
                Label lblMaSinhVien = new Label
                {
                    Text = sinhVien.MaSinhVien,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Bottom,
                    Height = 20
                };

                // Thêm Button và Label vào panel
                panelSinhVien.Controls.Add(btnSinhVien);
                panelSinhVien.Controls.Add(lblMaSinhVien);

                // Thêm panel vào FlowLayoutPanel
                flowLayoutPanel1.Controls.Add(panelSinhVien);
            }
        }

        private void BtnSinhVien_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag is DangKyPhong sinhVien)
            {
                if (_mainForm == null)
                {

                    MessageBox.Show("Tham chiếu _mainForm là null. Mở form thông tin sinh viên dưới dạng độc lập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ThongTinSinhVien formThongTin = new ThongTinSinhVien(_mainForm, sinhVien.MaSinhVien);
                    formThongTin.Show(); // Hiển thị form như một cửa sổ riêng
                    return;
                }

                if (!string.IsNullOrEmpty(sinhVien.MaSinhVien))
                {
                    ThongTinSinhVien formThongTin = new ThongTinSinhVien(_mainForm, sinhVien.MaSinhVien);
                    _mainForm.OpenChildForm(formThongTin);
                }
                else
                {
                    ThongTinSinhVien formThongTin = new ThongTinSinhVien();
                    _mainForm.OpenChildForm(formThongTin);
                }
            }
        }


        //// Hàm lấy hình ảnh của sinh viên
        //private Image GetImageForSinhVien(DangKyPhong sinhVien)
        //{
        //    // Đường dẫn tới thư mục avatars
        //    string imagePath = Path.Combine(@"D:\DACN2\DACN\DACN_QL_KTX\DACN_QL_KTX\avatars\", sinhVien.HinhNhanDien);
        //    if (File.Exists(imagePath))
        //    {
        //        return Image.FromFile(imagePath); // Tải ảnh từ đường dẫn
        //    }
        //    else
        //    {
        //        // Nếu không tìm thấy ảnh, sử dụng ảnh mặc định
        //        return Properties.Resources.user;
        //    }
        //}
        private Image GetImageForSinhVien(DangKyPhong sinhVien)
        {
            string imagePath = Path.Combine(@"D:\DACN\DACN_QL_KTX\DACN_QL_KTX\avatars\", sinhVien.HinhNhanDien);

            if (File.Exists(imagePath))
            {
                Image originalImage = Image.FromFile(imagePath);

                // Thay đổi kích thước ảnh để vừa với nút (100x100)
                return ResizeImage(originalImage, 100, 100);
            }
            else
            {
                // Sử dụng ảnh mặc định nếu không tìm thấy ảnh
                return ResizeImage(Properties.Resources.user, 100, 100);
            }
        }

        // Hàm thay đổi kích thước ảnh
        private Image ResizeImage(Image image, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, 0, 0, width, height);
            }
            return resizedImage;
        }


        // Hàm thiết lập ảnh mặc định cho PictureBox
        private void SetDefaultImage(PictureBox picBox)
        {
            string defaultImagePath = Path.Combine(Application.StartupPath, "GUI", "Resources", "user.png");
            if (File.Exists(defaultImagePath))
            {
                picBox.Image = Image.FromFile(defaultImagePath); // Ảnh mặc định
            }
            else
            {
                picBox.Image = Properties.Resources.user; // Ảnh mặc định từ tài nguyên
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Xử lý sự kiện click của label nếu cần
        }
    }
}
