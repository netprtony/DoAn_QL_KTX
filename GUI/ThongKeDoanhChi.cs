using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;


namespace GUI
{
    public partial class ThongKeDoanhChi : Form
    {
        public ThongKeDoanhChi()
        {
            InitializeComponent();
            this.Load += ThongKeDoanhChi_Load;
            btn_ThongKeChi.Click += Btn_ThongKeChi_Click;
            data_GV_ThongKeChi.CellClick += Data_GV_ThongKeChi_CellClick;
            btn_XuatBaoCaoDoanhChi.Click += Btn_XuatBaoCaoDoanhChi_Click;
        }

        private void Btn_XuatBaoCaoDoanhChi_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ DataGridView (thí dụ bạn có DataGridView gọi là dataGV_ThongKe)
            var data = data_GV_ThongKeChi.DataSource as List<ThongKeChiPhi>; // Thay đổi kiểu dữ liệu này cho phù hợp với lớp của bạn

            if (data == null || data.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo ứng dụng Excel mới
            Excel.Application excelApp = new Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Excel không được cài đặt trên máy tính của bạn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo một workbook và worksheet mới
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];

            // Tiêu đề cột
            worksheet.Cells[1, 1] = "STT";
          
            worksheet.Cells[1, 2] = "Tổng Số Chi Phí";

            worksheet.Cells[1, 3] = "Tổng Phí Yêu Cầu Sửa Chữa";
            worksheet.Cells[1, 4] = "Tổng Phí Nhập Trang Thiết Bị";
            worksheet.Cells[1, 5] = "Tổng Phí Nhập Nguyên Liệu";
           



            // Căn giữa tiêu đề cột
            for (int col = 1; col <= 6; col++)
            {
                Excel.Range headerRange = worksheet.Cells[1, col];
                headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            }

            // Ghi dữ liệu vào các dòng tiếp theo
            int rowIndex = 2;  // Bắt đầu ghi từ dòng thứ 2 (dòng 1 là tiêu đề)
            int stt = 1;  // Số thứ tự bắt đầu từ 1
            foreach (var item in data)
            {
                worksheet.Cells[rowIndex, 1] = stt;
                worksheet.Cells[rowIndex, 2] = item.TongTienSuaChua;
                worksheet.Cells[rowIndex, 3] = item.TongTienNhapTTB;
                worksheet.Cells[rowIndex, 4] = item.TongTienMuaNguyenLieu;
                worksheet.Cells[rowIndex, 5] = item.TongChiPhiTotal;
               

                rowIndex++;
                stt++;
            }

            // Căn giữa dữ liệu trong các cột (nếu cần thiết)
            for (int row = 2; row < rowIndex; row++)
            {
                for (int col = 1; col <= 5; col++)
                {
                    Excel.Range cellRange = worksheet.Cells[row, col];
                    cellRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                }
            }

            // Căn chỉnh chiều rộng cột tự động cho các cột
            worksheet.Columns.AutoFit();

            // Hiển thị Excel
            excelApp.Visible = true;

            // Giải phóng tài nguyên
            Marshal.ReleaseComObject(worksheet);
            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(excelApp);

        }

        private void Data_GV_ThongKeChi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu người dùng không chọn header (chỉ cho phép chọn hàng dữ liệu)
            if (e.RowIndex >= 0)
            {
                // Lấy thông tin từ dòng đã chọn
                var selectedRow = data_GV_ThongKeChi.Rows[e.RowIndex];

                // Lấy các giá trị từ dòng đã chọn và gán vào các TextBox
                txt_phi_ycsc.Text = selectedRow.Cells[2].Value.ToString();  // Cột thứ 2: Tổng Tiền Sửa Chữa
                txt_phi_nhapttb.Text = selectedRow.Cells[3].Value.ToString();  // Cột thứ 3: Tổng Tiền Nhập TTB
                txt_phi_nhapnguyenlieu.Text = selectedRow.Cells[4].Value.ToString();  // Cột thứ 4: Tổng Tiền Mua Nguyên Liệu
                txt_tongtien.Text = selectedRow.Cells[1].Value.ToString();
            }

        }

        private void DrawChart(List<ThongKeChiPhi> thongKeChiPhi)
        {
            // Xóa các series cũ nếu có
            char1_ThongKeChi.Series.Clear();

            // Tạo các series mới cho biểu đồ
            Series seriesSuaChua = new Series("Tổng Tiền Sửa Chữa")
            {
                ChartType = SeriesChartType.Line, // Biểu đồ đường
                XValueMember = "Thang", // Lấy giá trị tháng từ ThongKeChiPhi
                YValueMembers = "TongTienSuaChua" // Lấy giá trị tổng tiền sửa chữa
            };

            Series seriesNhapTTB = new Series("Tổng Tiền Nhập TTB")
            {
                ChartType = SeriesChartType.Line, // Biểu đồ đường
                XValueMember = "Thang", // Lấy giá trị tháng từ ThongKeChiPhi
                YValueMembers = "TongTienNhapTTB" // Lấy giá trị tổng tiền nhập TTB
            };

            Series seriesMuaNguyenLieu = new Series("Tổng Tiền Mua Nguyên Liệu")
            {
                ChartType = SeriesChartType.Line, // Biểu đồ đường
                XValueMember = "Thang", // Lấy giá trị tháng từ ThongKeChiPhi
                YValueMembers = "TongTienMuaNguyenLieu" // Lấy giá trị tổng tiền mua nguyên liệu
            };

            // Thêm các series vào Chart
            char1_ThongKeChi.Series.Add(seriesSuaChua);
            char1_ThongKeChi.Series.Add(seriesNhapTTB);
            char1_ThongKeChi.Series.Add(seriesMuaNguyenLieu);

            // Cung cấp dữ liệu cho biểu đồ từ danh sách thống kê
            char1_ThongKeChi.DataSource = thongKeChiPhi;

            // Cập nhật biểu đồ
            char1_ThongKeChi.DataBind();
        }

        private void Btn_ThongKeChi_Click(object sender, EventArgs e)
        {
            // Lấy năm từ TextBox (hoặc có thể từ DateTimePicker nếu bạn sử dụng)
            int nam = int.Parse(txtNam.Text);  // Giả sử bạn có TextBox txtNam để nhập năm

            // Tạo đối tượng BLL_ThongKe
            BLL_ThongKe bllThongKe = new BLL_ThongKe();

            // Lấy danh sách thống kê chi phí từ BLL
            List<ThongKeChiPhi> thongKeChiPhi = bllThongKe.GetThongKeChiPhi(nam);

            // Gán dữ liệu vào DataGridView
            data_GV_ThongKeChi.DataSource = thongKeChiPhi;

            // Thêm cột Tháng vào DataGridView nếu chưa có
            if (!data_GV_ThongKeChi.Columns.Contains("Thang"))
            {
                DataGridViewTextBoxColumn thangColumn = new DataGridViewTextBoxColumn();
                thangColumn.Name = "Thang";
                thangColumn.HeaderText = "Tháng";
                thangColumn.DataPropertyName = "Thang"; // Tương ứng với thuộc tính Thang trong ThongKeChiPhi
                data_GV_ThongKeChi.Columns.Insert(0, thangColumn); // Thêm vào cột đầu tiên
            }

            // Cập nhật lại tiêu đề cột
            data_GV_ThongKeChi.Columns[1].HeaderText = "Tổng Tiền Sửa Chữa";
            data_GV_ThongKeChi.Columns[2].HeaderText = "Tổng Tiền Nhập TTB";
            data_GV_ThongKeChi.Columns[3].HeaderText = "Tổng Tiền Mua Nguyên Liệu";
            data_GV_ThongKeChi.Columns[4].HeaderText = "Tổng Chi Phí Tổng Cộng";

            // Vẽ biểu đồ
            DrawChart(thongKeChiPhi);
        }


        private void ThongKeDoanhChi_Load(object sender, EventArgs e)
        {
            
        }
    }
}
