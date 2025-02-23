using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using ZXing;
using System.Windows.Forms;
using DTO;


namespace GUI.Model
{
    public class PdfReport
    {
        public void CreateInvoicePdf(string filePath, string maPhong, decimal tongTien, int chiSoDienCu, int chiSoNuocCu, int chiSoDienMoi, int chiSoNuocMoi, int soDienTieuThu, int soNuocTieuThu)
        {
            iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 20, 20, 20, 20);

            try
            {
                // Lưu file PDF tại filePath
                PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                doc.Open();

                // Phông chữ hỗ trợ Unicode (VietNam)
                string fontPath = @"D:\DACN\fonts\NotoSans-VariableFont_wdth,wght.ttf";
                BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                // Tiêu đề hóa đơn
                iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("HÓA ĐƠN THANH TOÁN", new iTextSharp.text.Font(bf, 14, iTextSharp.text.Font.BOLD));
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);

                doc.Add(new iTextSharp.text.Paragraph(" "));

                // Thông tin phòng
                iTextSharp.text.Paragraph maPhongParagraph = new iTextSharp.text.Paragraph($"Mã Phòng: {maPhong}", new iTextSharp.text.Font(bf, 12));
                maPhongParagraph.Alignment = Element.ALIGN_LEFT;
                doc.Add(maPhongParagraph);

                doc.Add(new iTextSharp.text.Paragraph(" "));

                // Tạo bảng với phông chữ hỗ trợ Unicode
                PdfPTable table = new PdfPTable(4); // 4 cột: Loại Dịch Vụ, Chỉ Số Cũ, Chỉ Số Mới, Lượng Tiêu Thụ
                table.DefaultCell.Phrase = new iTextSharp.text.Phrase("", new iTextSharp.text.Font(bf, 12));

                // Tiêu đề bảng
                table.AddCell(new iTextSharp.text.Phrase("Loại Dịch Vụ", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD)));
                table.AddCell(new iTextSharp.text.Phrase("Chỉ Số Cũ", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD)));
                table.AddCell(new iTextSharp.text.Phrase("Chỉ Số Mới", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD)));
                table.AddCell(new iTextSharp.text.Phrase("Lượng Tiêu Thụ", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD)));

                // Điện
                table.AddCell(new iTextSharp.text.Phrase("Điện", new iTextSharp.text.Font(bf, 12)));
                table.AddCell(new iTextSharp.text.Phrase(chiSoDienCu.ToString(), new iTextSharp.text.Font(bf, 12)));
                table.AddCell(new iTextSharp.text.Phrase(chiSoDienMoi.ToString(), new iTextSharp.text.Font(bf, 12)));
                table.AddCell(new iTextSharp.text.Phrase(soDienTieuThu.ToString(), new iTextSharp.text.Font(bf, 12)));

                // Nước
                table.AddCell(new iTextSharp.text.Phrase("Nước", new iTextSharp.text.Font(bf, 12)));
                table.AddCell(new iTextSharp.text.Phrase(chiSoNuocCu.ToString(), new iTextSharp.text.Font(bf, 12)));
                table.AddCell(new iTextSharp.text.Phrase(chiSoNuocMoi.ToString(), new iTextSharp.text.Font(bf, 12)));
                table.AddCell(new iTextSharp.text.Phrase(soNuocTieuThu.ToString(), new iTextSharp.text.Font(bf, 12)));

                doc.Add(table);

                doc.Add(new iTextSharp.text.Paragraph(" "));

                // Tính toán tiền điện và nước
                decimal tongTienDien = soDienTieuThu * 2500;  // Giả sử mỗi kWh điện tính 2500 VNĐ
                decimal tongTienNuoc = soNuocTieuThu * 3000; // Giả sử mỗi m3 nước tính 3000 VNĐ
                decimal tongTienCu = tongTienDien + tongTienNuoc;

                // Thêm tổng tiền
                iTextSharp.text.Paragraph tongTienParagraph = new iTextSharp.text.Paragraph($"Tổng tiền điện và nước: {tongTienCu.ToString("C", new System.Globalization.CultureInfo("vi-VN"))}", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD));
                tongTienParagraph.Alignment = Element.ALIGN_LEFT;
                doc.Add(tongTienParagraph);

                // Tổng cộng
                iTextSharp.text.Paragraph tongTienCuParagraph = new iTextSharp.text.Paragraph($"Tổng tiền phải trả: {tongTien.ToString("C", new System.Globalization.CultureInfo("vi-VN"))}", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD));
                tongTienCuParagraph.Alignment = Element.ALIGN_LEFT;
                doc.Add(tongTienCuParagraph);

                // Kết thúc và lưu hóa đơn
                doc.Close();
                // Mở hóa đơn sau khi tạo
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception ex)
            {
                // Lỗi
                // MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // Hàm tạo phần điện
        private iTextSharp.text.Paragraph CreateElectricitySection(BaseFont bf, int chiSoDienCu, int chiSoDienMoi, int soDienTieuThu)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Điện:");
            sb.AppendLine($"Chỉ số cũ: {chiSoDienCu}");
            sb.AppendLine($"Chỉ số mới: {chiSoDienMoi}");
            sb.AppendLine($"Lượng tiêu thụ: {soDienTieuThu}");
            sb.AppendLine($"Thành tiền: {soDienTieuThu * 2500:C}");  // Giả sử mỗi kWh điện tính 2500 VNĐ

            return new iTextSharp.text.Paragraph(sb.ToString(), new iTextSharp.text.Font(bf, 12));
        }

        // Hàm tạo phần nước
        private iTextSharp.text.Paragraph CreateWaterSection(BaseFont bf, int chiSoNuocCu, int chiSoNuocMoi, int soNuocTieuThu)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Nước:");
            sb.AppendLine($"Chỉ số cũ: {chiSoNuocCu}");
            sb.AppendLine($"Chỉ số mới: {chiSoNuocMoi}");
            sb.AppendLine($"Lượng tiêu thụ: {soNuocTieuThu}");
            sb.AppendLine($"Thành tiền: {soNuocTieuThu * 3000:C}");  // Giả sử mỗi m3 nước tính 3000 VNĐ

            return new iTextSharp.text.Paragraph(sb.ToString(), new iTextSharp.text.Font(bf, 12));
        }


        public void CreateSummaryReport(string filePath, int thang, int nam, string nguoiLapPhieu, List<ThongKeDienNuoc> danhSachThongKe, decimal tongTien)
        {
            // Tạo tài liệu PDF
            iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 20, 20, 20, 20);

            try
            {
                PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                doc.Open();

                // Phông chữ hỗ trợ Unicode (Tiếng Việt)
                string fontPath = @"D:\DACN\fonts\NotoSans-VariableFont_wdth,wght.ttf";
                BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                // Tiêu đề báo cáo
                iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph(
                    $"PHIẾU TỔNG HỢP ĐIỆN NƯỚC THÁNG {thang}/{nam}",
                    new iTextSharp.text.Font(bf, 14, iTextSharp.text.Font.BOLD)
                );
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);

                doc.Add(new iTextSharp.text.Paragraph(" ")); // Dòng trống

                // Thông tin người lập phiếu
                iTextSharp.text.Paragraph infoParagraph = new iTextSharp.text.Paragraph(
                    $"Người lập phiếu: {nguoiLapPhieu}\nNgày lập: {DateTime.Now.ToString("dd/MM/yyyy")}",
                    new iTextSharp.text.Font(bf, 12)
                );
                infoParagraph.Alignment = Element.ALIGN_LEFT;
                doc.Add(infoParagraph);

                doc.Add(new iTextSharp.text.Paragraph(" ")); // Dòng trống

                // Tạo bảng chi tiết
                PdfPTable table = new PdfPTable(12); // Cập nhật số cột thành 13
                table.WidthPercentage = 100;

                // Đặt phông chữ cho bảng
                table.DefaultCell.Phrase = new iTextSharp.text.Phrase("", new iTextSharp.text.Font(bf, 12));

                // Tiêu đề bảng
                table.AddCell(new iTextSharp.text.Phrase("Mã Phòng", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD)));
                table.AddCell(new iTextSharp.text.Phrase("Chỉ Số Điện Cũ", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD)));
                table.AddCell(new iTextSharp.text.Phrase("Chỉ Số Điện Mới", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD)));
                table.AddCell(new iTextSharp.text.Phrase("Số Điện Tiêu Thụ", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD)));
                table.AddCell(new iTextSharp.text.Phrase("Đơn Giá Điện", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD)));
                table.AddCell(new iTextSharp.text.Phrase("Thành Tiền Điện", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD)));
                table.AddCell(new iTextSharp.text.Phrase("Chỉ Số Nước Cũ", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD)));
                table.AddCell(new iTextSharp.text.Phrase("Chỉ Số Nước Mới", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD)));
                table.AddCell(new iTextSharp.text.Phrase("Số Nước Tiêu Thụ", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD)));
                table.AddCell(new iTextSharp.text.Phrase("Đơn Giá Nước", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD)));
                table.AddCell(new iTextSharp.text.Phrase("Thành Tiền Nước", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD)));
                table.AddCell(new iTextSharp.text.Phrase("Tổng Tiền", new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD)));

                // Duyệt danh sách và thêm vào bảng
                foreach (var item in danhSachThongKe)
                {
                    table.AddCell(new iTextSharp.text.Phrase(item.MaPhong, new iTextSharp.text.Font(bf, 12)));
                    table.AddCell(new iTextSharp.text.Phrase(item.ChiSoDienCu.ToString(), new iTextSharp.text.Font(bf, 12)));
                    table.AddCell(new iTextSharp.text.Phrase(item.ChiSoDienMoi.ToString(), new iTextSharp.text.Font(bf, 12)));
                    table.AddCell(new iTextSharp.text.Phrase(item.SoDienTieuThu.ToString(), new iTextSharp.text.Font(bf, 12)));
                    table.AddCell(new iTextSharp.text.Phrase(item.DonGiaDien.ToString("C0", new System.Globalization.CultureInfo("vi-VN")), new iTextSharp.text.Font(bf, 12)));
                    table.AddCell(new iTextSharp.text.Phrase(item.ThanhTienDien.ToString("C0", new System.Globalization.CultureInfo("vi-VN")), new iTextSharp.text.Font(bf, 12)));
                    table.AddCell(new iTextSharp.text.Phrase(item.ChiSoNuocCu.ToString(), new iTextSharp.text.Font(bf, 12)));
                    table.AddCell(new iTextSharp.text.Phrase(item.ChiSoNuocMoi.ToString(), new iTextSharp.text.Font(bf, 12)));
                    table.AddCell(new iTextSharp.text.Phrase(item.SoNuocTieuThu.ToString(), new iTextSharp.text.Font(bf, 12)));
                    table.AddCell(new iTextSharp.text.Phrase(item.DonGiaNuoc.ToString("C0", new System.Globalization.CultureInfo("vi-VN")), new iTextSharp.text.Font(bf, 12)));
                    table.AddCell(new iTextSharp.text.Phrase(item.ThanhTienNuoc.ToString("C0", new System.Globalization.CultureInfo("vi-VN")), new iTextSharp.text.Font(bf, 12)));
                    table.AddCell(new iTextSharp.text.Phrase(item.TongTien.ToString("C0", new System.Globalization.CultureInfo("vi-VN")), new iTextSharp.text.Font(bf, 12)));
                }

                doc.Add(table);

                doc.Add(new iTextSharp.text.Paragraph(" ")); // Dòng trống

                // Tổng cộng tiền
                iTextSharp.text.Paragraph totalParagraph = new iTextSharp.text.Paragraph(
                    $"Tổng tiền thu được trong tháng: {tongTien.ToString("C", new System.Globalization.CultureInfo("vi-VN"))}",
                    new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD)
                );
                totalParagraph.Alignment = Element.ALIGN_RIGHT;
                doc.Add(totalParagraph);

                // Kết thúc và lưu phiếu
                doc.Close();

                // Mở file PDF sau khi tạo
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ThuDonTheoTuan(string filePath, string nguoiLap, List<MonAn> danhSachMonAn, DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            Document doc = new Document(PageSize.A4, 20, 20, 20, 20);

            try
            {
                PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                doc.Open();

                // Font hỗ trợ Unicode
                string fontPath = @"D:\DACN\fonts\NotoSans-VariableFont_wdth,wght.ttf";
                BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font font = new Font(bf, 12);

                // Tiêu đề báo cáo
                Paragraph title = new Paragraph($"THỰC ĐƠN TỪ {ngayBatDau:dd/MM/yyyy} ĐẾN {ngayKetThuc:dd/MM/yyyy}\n", new Font(bf, 14, Font.BOLD));
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);
                doc.Add(new Paragraph("\n"));

                // Thông tin người lập
                doc.Add(new Paragraph($"Người lập: {nguoiLap}\nNgày lập: {DateTime.Now:dd/MM/yyyy}\n", font));
                doc.Add(new Paragraph("\n"));

                // Tạo bảng
                PdfPTable table = new PdfPTable(4);
                table.WidthPercentage = 100;
                table.AddCell(new PdfPCell(new Phrase("Thứ/Ngày", font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new Phrase("Sáng", font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new Phrase("Trưa", font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new Phrase("Chiều", font)) { HorizontalAlignment = Element.ALIGN_CENTER });

                foreach (var monAn in danhSachMonAn)
                {
                    table.AddCell(new PdfPCell(new Phrase(monAn.Ten ?? "", font)));
                    table.AddCell(new PdfPCell(new Phrase(monAn.Sang ?? "", font)));
                    table.AddCell(new PdfPCell(new Phrase(monAn.Trua ?? "", font)));
                    table.AddCell(new PdfPCell(new Phrase(monAn.Chieu ?? "", font)));
                }

                doc.Add(table);
                doc.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Có lỗi xảy ra khi tạo file PDF: {ex.Message}");
            }
        }

        public void ExportDanhSachDKNTFromDataGridView(string filePath, DataGridView dataGridView, int nam)
        {
            // Tạo tài liệu PDF
            iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 20, 20, 20, 20);

            try
            {
                PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                doc.Open();

                // Phông chữ hỗ trợ Unicode (Tiếng Việt)
                string fontPath = @"D:\DACN\fonts\NotoSans-VariableFont_wdth,wght.ttf"; // Đường dẫn phông chữ
                BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                // Tạo bảng với 2 cột
                PdfPTable titleTable = new PdfPTable(2);
                titleTable.WidthPercentage = 100; // Chiều rộng bảng là 100% của trang
                titleTable.SetWidths(new float[] { 50, 50 }); // Chia đều hai cột

                // Tạo phần tiêu đề trái
                iTextSharp.text.Paragraph leftTitle = new iTextSharp.text.Paragraph(
                    "TRƯỜNG ĐẠI HỌC CÔNG THƯƠNG TP. HCM\nTRUNG TÂM KÝ TÚC XÁ SINH VIÊN",
                    new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD)
                );
                PdfPCell leftCell = new PdfPCell();
                leftCell.AddElement(leftTitle);
                leftCell.Border = PdfPCell.NO_BORDER; // Không có viền
                leftCell.HorizontalAlignment = Element.ALIGN_LEFT;

                // Tạo phần tiêu đề phải
                iTextSharp.text.Paragraph rightTitle = new iTextSharp.text.Paragraph(
                    "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM\nĐộc lập - Tự do - Hạnh phúc",
                    new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD)
                );
                PdfPCell rightCell = new PdfPCell();
                rightCell.AddElement(rightTitle);
                rightCell.Border = PdfPCell.NO_BORDER; // Không có viền
                rightCell.HorizontalAlignment = Element.ALIGN_RIGHT;

                // Thêm các ô vào bảng
                titleTable.AddCell(leftCell);
                titleTable.AddCell(rightCell);

                // Thêm bảng tiêu đề vào tài liệu PDF
                doc.Add(titleTable);


                // Tạo tiêu đề chính với biến `nam`
                string namHoc = $"NĂM HỌC {nam}-{nam + 1} (Đợt 1)";
                iTextSharp.text.Paragraph mainTitle = new iTextSharp.text.Paragraph(
                    $"DANH SÁCH SINH VIÊN XÉT LƯU TRÚ KTX\n{namHoc}",
                    new iTextSharp.text.Font(bf, 14, iTextSharp.text.Font.BOLD)
                );
                mainTitle.Alignment = Element.ALIGN_CENTER;
                doc.Add(mainTitle);

                doc.Add(new iTextSharp.text.Paragraph(" ")); // Dòng trống

                // Tạo bảng
                PdfPTable table = new PdfPTable(dataGridView.ColumnCount); // Số cột bằng số cột trong DataGridView
                table.WidthPercentage = 100;

                // Đặt độ rộng cột tự động
                float[] columnWidths = new float[dataGridView.ColumnCount];
                for (int i = 0; i < dataGridView.ColumnCount; i++)
                {
                    columnWidths[i] = 1f; // Mặc định mỗi cột rộng như nhau
                }
                table.SetWidths(columnWidths);

                // Font chữ cho bảng
                iTextSharp.text.Font tableFont = new iTextSharp.text.Font(bf, 11);

                // Thêm tiêu đề cột vào bảng
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    table.AddCell(new PdfPCell(new Phrase(column.HeaderText, tableFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BackgroundColor = new iTextSharp.text.BaseColor(200, 200, 200) // Màu nền xám nhạt
                    });
                }

                // Thêm dữ liệu từ DataGridView vào bảng
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.IsNewRow) continue; // Bỏ qua dòng trống cuối cùng

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        table.AddCell(new PdfPCell(new Phrase(cell.Value?.ToString() ?? "", tableFont))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER
                        });
                    }
                }

                // Thêm bảng vào tài liệu
                doc.Add(table);

                doc.Close();

                // Hiển thị thông báo và mở file
               // System.Windows.MessageBox.Show("Danh sách đã được xuất thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception ex)
            {
               // MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }

}


