using BLL;
using DTO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.Office.Interop.Word;
using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO; // Để làm việc với file
using ZXing;
using System.Drawing;
using System.Drawing.Drawing2D;



using System.Net.NetworkInformation;


namespace GUI
{
    public partial class LapThe : Form
    {
        public LapThe()
        {
            InitializeComponent();
            txt_maSinhVien.KeyDown += Txt_maSinhVien_KeyDown;
            btn_inTheRaVao.Click += Btn_inTheRaVao_Click;

        }

        private iTextSharp.text.Image CreateRoundedImage(string imagePath, int diameter)
        {
            using (Bitmap originalImage = new Bitmap(imagePath))
            {
                Bitmap roundedImage = new Bitmap(diameter, diameter);
                using (Graphics g = Graphics.FromImage(roundedImage))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (Brush brush = new TextureBrush(originalImage))
                    {
                        GraphicsPath path = new GraphicsPath();
                        path.AddEllipse(0, 0, diameter, diameter);
                        g.FillPath(brush, path);
                    }
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    roundedImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ms.Seek(0, SeekOrigin.Begin);
                    return iTextSharp.text.Image.GetInstance(ms);
                }
            }
        }
        private void Btn_inTheRaVao_Click(object sender, EventArgs e)
        {
            string maSinhVien = txt_maSinhVien.Text.Trim();
            string hoTen = txt_HoVaTen.Text;
            DateTime ngaySinh = dateTime_NgaySinh.Value;
            string gioiTinh = radio_Nam.Checked ? "Nam" : "Nữ";
            string maPhong = txt_MaPhong.Text;

            if (string.IsNullOrEmpty(maSinhVien) || string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(maPhong))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin sinh viên trước khi tạo thẻ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A7, 10, 10, 10, 10);

            try
            {
                string outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{maSinhVien}_TheRaVao.pdf");
                PdfWriter.GetInstance(doc, new FileStream(outputPath, FileMode.Create));

                doc.Open();

                // Đường dẫn đến phông chữ hỗ trợ Unicode
                string fontPath = @"D:\DACN\fonts\NotoSans-VariableFont_wdth,wght.ttf";
                BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                // Thêm logo trường ở phía trên
                string logoPath = @"D:\DACN\DACN_QL_KTX\DACN_QL_KTX\Icon\Logo.jpg";
                if (File.Exists(logoPath))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                    logo.ScaleToFit(50f, 50f);
                    logo.Alignment = Element.ALIGN_CENTER;
                    doc.Add(logo);
                }

                // Thêm tên trường
                iTextSharp.text.Paragraph schoolName = new iTextSharp.text.Paragraph("TRƯỜNG ĐẠI HỌC CÔNG THƯƠNG TP.HCM", new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.BOLD));
                schoolName.Alignment = Element.ALIGN_CENTER;
                doc.Add(schoolName);

                // Thêm tiêu đề "THẺ RA VÀO KÝ TÚC XÁ"
                iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("THẺ RA VÀO KÝ TÚC XÁ", new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.BOLD));
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);
                doc.Add(new iTextSharp.text.Paragraph(" "));

                // Thêm ảnh sinh viên (nếu có)
                BLL_SinhVien bll_sinhvien = new BLL_SinhVien();
                SinhVienDTO sinhVienDTO = bll_sinhvien.GetSinhVienPhong(maSinhVien);
                if (sinhVienDTO != null && !string.IsNullOrEmpty(sinhVienDTO.HinhNhanDien))
                {
                    string imageDirectory = @"D:\DACN\DACN_QL_KTX\DACN_QL_KTX\avatars\";
                    string imagePath = Path.Combine(imageDirectory, sinhVienDTO.HinhNhanDien);
                    if (File.Exists(imagePath))
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
                        img.ScaleToFit(60f, 60f);
                        img.Alignment = Element.ALIGN_CENTER;
                        doc.Add(img);
                        doc.Add(new iTextSharp.text.Paragraph(" "));
                    }
                }

                // Thêm họ tên, in đậm, màu đỏ
                iTextSharp.text.Font redFont = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.BOLD, new BaseColor(255, 0, 0));
                iTextSharp.text.Paragraph hoTenParagraph = new iTextSharp.text.Paragraph(hoTen, redFont);
                hoTenParagraph.Alignment = Element.ALIGN_CENTER;
                doc.Add(hoTenParagraph);

                // Thêm Mã sinh viên, Ngày sinh, Giới tính và Mã phòng
                iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Paragraph maSinhVienParagraph = new iTextSharp.text.Paragraph($"MSSV: {maSinhVien}", font);
                maSinhVienParagraph.Alignment = Element.ALIGN_CENTER;
                doc.Add(maSinhVienParagraph);

                iTextSharp.text.Paragraph maPhongParagraph = new iTextSharp.text.Paragraph($"Số Phòng: {maPhong}", font);
                maPhongParagraph.Alignment = Element.ALIGN_CENTER;
                doc.Add(maPhongParagraph);

                doc.Add(new iTextSharp.text.Paragraph(" "));

                // Tạo mã vạch
                var barcodeWriter = new ZXing.BarcodeWriter();
                barcodeWriter.Format = ZXing.BarcodeFormat.CODE_128;
                var barcodeBitmap = barcodeWriter.Write(maSinhVien);
                using (MemoryStream ms = new MemoryStream())
                {
                    barcodeBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ms.Seek(0, SeekOrigin.Begin);
                    iTextSharp.text.Image barcodeImage = iTextSharp.text.Image.GetInstance(ms);
                    barcodeImage.ScaleToFit(250f, 30f);
                    barcodeImage.Alignment = Element.ALIGN_CENTER;
                    doc.Add(barcodeImage);
                }

                MessageBox.Show("Thẻ ra vào đã được tạo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(outputPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (doc != null)
                {
                    doc.Close();
                }
            }
        }







        private void Txt_maSinhVien_KeyDown(object sender, KeyEventArgs e)
        {
            // Kiểm tra nếu người dùng nhấn Enter
            if (e.KeyCode == Keys.Enter)
            {
                // Lấy mã sinh viên từ ô nhập liệu
                string maSinhVien = txt_maSinhVien.Text.Trim();

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
                        dateTime_NgaySinh.Value = sinhVienDTO.NgaySinh;

                        // Kiểm tra giới tính và chọn radio button tương ứng
                        if (sinhVienDTO.GioiTinh == "Nam")
                        {
                            radio_Nam.Checked = true;
                        }
                        else if (sinhVienDTO.GioiTinh == "Nữ")
                        {
                            radio_Nu.Checked = true;
                        }

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
    }
}
