using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GUI
{
    public partial class ThongKeThu : Form
    {
        public ThongKeThu()
        {
            InitializeComponent();
            this.Load += ThongKeThu_Load;
            btn_ThongKeThu.Click += Btn_ThongKeThu_Click;
        }

        private void DrawChart(List<ThongKeChiPhiThu> thongKeList)
        {
            // Xóa series cũ nếu có
            chart_Spline_TT_Thu.Series.Clear();

            // Tạo Series cho từng loại thu
            Series seriesDienNuoc = new Series("Chi phí điện nước")
            {
                ChartType = SeriesChartType.Spline,
                Color = Color.Blue
            };

            Series seriesDichVu = new Series("Chi phí dịch vụ")
            {
                ChartType = SeriesChartType.Spline,
                Color = Color.Red
            };

            // Thêm dữ liệu vào các series
            foreach (var item in thongKeList)
            {
                seriesDienNuoc.Points.AddXY(item.Thang, item.TongThuTienDien);
                seriesDichVu.Points.AddXY(item.Thang, item.TongThuDichVu);
            }

            // Thêm series vào biểu đồ
            chart_Spline_TT_Thu.Series.Add(seriesDienNuoc);
            chart_Spline_TT_Thu.Series.Add(seriesDichVu);
        }

        private void Btn_ThongKeThu_Click(object sender, EventArgs e)
        {
            // Kiểm tra đầu vào
            if (!int.TryParse(txtNam.Text, out int nam))
            {
                MessageBox.Show("Vui lòng nhập năm hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy dữ liệu từ BLL
            BLL_ThongKe bllThongKe = new BLL_ThongKe();
            List<ThongKeChiPhiThu> thongKeList = bllThongKe.GetThongKeChiPhiThu(nam);

            // Kiểm tra dữ liệu rỗng
            if (thongKeList == null || thongKeList.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu cho năm đã chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Hiển thị dữ liệu lên DataGridView
            data_GV_ThongKeThu.DataSource = thongKeList;

            // Đặt lại tiêu đề cột
            if (data_GV_ThongKeThu.Columns.Contains("Thang"))
            {
                data_GV_ThongKeThu.Columns["Thang"].HeaderText = "Tháng";
            }
            if (data_GV_ThongKeThu.Columns.Contains("TongThuTienDien"))
            {
                data_GV_ThongKeThu.Columns["TongThuTienDien"].HeaderText = "Tổng Thu Tiền Điện";
            }
            if (data_GV_ThongKeThu.Columns.Contains("TongThuDichVu"))
            {
                data_GV_ThongKeThu.Columns["TongThuDichVu"].HeaderText = "Tổng Thu Dịch Vụ";
            }

            // Vẽ biểu đồ
            DrawChart(thongKeList);
        }

        private void ThongKeThu_Load(object sender, EventArgs e)
        {
            // Thiết lập mặc định cho form nếu cần
        }
    }
}
