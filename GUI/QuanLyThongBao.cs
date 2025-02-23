using BLL;
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

namespace GUI
{
    public partial class QuanLyThongBao : Form
    {
        public QuanLyThongBao()
        {
            InitializeComponent();
            btn_LoadFile.Click += Btn_LoadFile_Click;
            btn_DangThongBao.Click += Btn_DangThongBao_Click;
        }

        private void Btn_DangThongBao_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ các controls
                string tieuDe = txt_TenTieuDe.Text.Trim();
                string noiDung = txt_TenNoiDungTB.Text.Trim();
                DateTime ngayTao = dateTime_NgayTao.Value;
                DateTime? ngayHetHan = dateTime_NgayHetHan.Value;
                string TenFile = txt_TenFile.Text.Trim();

                // Kiểm tra tiêu đề và nội dung không được để trống
                if (string.IsNullOrEmpty(tieuDe) || string.IsNullOrEmpty(noiDung))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ tiêu đề và nội dung thông báo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo DTO cho ThongBao
                ThongBao thongBao = new ThongBao
                {
                    TieuDe = tieuDe,
                    NoiDung = noiDung,
                    NgayTao = ngayTao,
                    NgayHetHan = ngayHetHan
                };

                // Tạo DTO cho DangThongBao
                DangThongBao dangThongBao = new DangThongBao
                {
                    MaNhanVien = "NV001", // Mã nhân viên, có thể lấy từ login hoặc tùy chỉnh
                    TieuDe = tieuDe,
                    FileTB = TenFile, // Có thể thêm logic để chọn file nếu cần
                    NgayDang = DateTime.Now
                };

                // Gọi hàm trong BLL
                BLL_QL_ThongBao bllThongBao = new BLL_QL_ThongBao();
                bool ketQua = bllThongBao.ThemThongBaoVaDangThongBao(thongBao, dangThongBao);

                if (ketQua)
                {
                    MessageBox.Show("Đăng thông báo thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Đăng thông báo thất bại. Vui lòng thử lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Btn_LoadFile_Click(object sender, EventArgs e)
        {
            // Mở hộp thoại chọn file
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*"; // Lọc chỉ cho phép chọn file PDF hoặc tất cả file
                openFileDialog.Title = "Chọn file để tải lên";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Lấy đường dẫn file được chọn
                    string filePath = openFileDialog.FileName;

                    try
                    {
                        // Hiển thị nội dung file trong WebBrowser
                        webBrowser_FileThongBao.Navigate(filePath);

                        // Hiển thị tên file trong TextBox
                        txt_TenFile.Text = System.IO.Path.GetFileName(filePath);
                    }
                    catch (Exception ex)
                    {
                        // Thông báo lỗi nếu không tải được file
                        MessageBox.Show("Không thể tải file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

    }
}
