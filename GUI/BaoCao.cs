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
using BLL;
using System.Runtime.CompilerServices;
using System.Windows.Forms.DataVisualization.Charting;
using GUI.Model;

namespace GUI
{
    public partial class BaoCao : Form
    {
        private BLL_ThongKe bll_tk;
        
        public BaoCao()
        {
            bll_tk = new BLL_ThongKe(); 
            InitializeComponent();
            btn_ThongKeTyLe_SV_ThongKe.Click += Btn_ThongKeTyLe_SV_ThongKe_Click;
            btn_BC_DS_NoiTru.Click += Btn_BC_DS_NoiTru_Click;
            btn_XuatBaoCao_DSDKNT.Click += Btn_XuatBaoCao_DSDKNT_Click;
            btn_ThongKe_Dien_TheoTang.Click += Btn_ThongKe_Dien_TheoTang_Click;
            btn_tk_nuoc_theo_tang.Click += Btn_tk_nuoc_theo_tang_Click;
        }

        private void Btn_tk_nuoc_theo_tang_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem txt_nam_tk_nuoc_theo_tang có giá trị hợp lệ hay không
            if (int.TryParse(txt_nam_tk_nuoc_theo_tang.Text, out int nam))
            {
                // Gọi phương thức thống kê từ BLL để lấy dữ liệu
                List<TyLeSuDungNuocTheoTang> thongKeNuoc = bll_tk.GetThongKeTiLeSuDungNuoc(nam);

                // Hiển thị dữ liệu vào DataGridView
                datagv_nuoc_theo_tang.DataSource = thongKeNuoc;

                // Vẽ biểu đồ StackedBar
                DrawStackedBarChart(thongKeNuoc);
            }
            else
            {
                // Nếu không phải là số hợp lệ, hiển thị thông báo lỗi
                MessageBox.Show("Vui lòng nhập năm hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DrawStackedBarChart(List<TyLeSuDungNuocTheoTang> thongKeNuoc)
        {
            // Xóa biểu đồ cũ nếu có
            Chart_ThongKe_Nuoc_TheoTang.Series.Clear();

            // Tạo một series mới với kiểu StackedBar
            Series series = new Series("Tỷ lệ sử dụng nước")
            {
                ChartType = SeriesChartType.StackedBar, // Đặt kiểu biểu đồ là StackedBar
                IsValueShownAsLabel = true, // Hiển thị giá trị trên biểu đồ
                LabelFormat = "{0}%" // Định dạng hiển thị giá trị phần trăm
            };

            // Thêm các điểm dữ liệu vào biểu đồ
            foreach (var item in thongKeNuoc)
            {
                // Thêm các điểm X, Y vào biểu đồ (X là tầng, Y là tỷ lệ sử dụng nước)
                series.Points.AddXY($"Tầng {item.Tang}", item.TiLeSuDungNuoc);
            }

            // Thêm series vào biểu đồ
            Chart_ThongKe_Nuoc_TheoTang.Series.Add(series);

            // Thiết lập các thuộc tính cho Chart Area
            Chart_ThongKe_Nuoc_TheoTang.ChartAreas[0].AxisX.Title = "Tầng";
            Chart_ThongKe_Nuoc_TheoTang.ChartAreas[0].AxisY.Title = "Tỷ lệ sử dụng nước (%)";

            // Thiết lập màu sắc cho biểu đồ
         //   Chart_ThongKe_Nuoc_TheoTang.Series[0].Color = System.Drawing.Color.CornflowerBlue;

            // Cập nhật lại biểu đồ
            Chart_ThongKe_Nuoc_TheoTang.Invalidate();
        }

        private void Btn_ThongKe_Dien_TheoTang_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem txt_nam_tk_dien_theo_tang có giá trị hợp lệ hay không
            if (int.TryParse(txt_nam_tk_dien_theo_tang.Text, out int nam))
            {
                // Gọi phương thức thống kê từ BLL để lấy dữ liệu
                List<TyLeSuDungDienTheoTang> thongKeDien = bll_tk.GetThongKeTiLeSuDungDien(nam);

                // Hiển thị dữ liệu vào DataGridView
                DataGV_ThongKe_Dien_TheoTang.DataSource = thongKeDien;

                // Vẽ biểu đồ StackedArea
                DrawDoughnutChart(thongKeDien);
            }
            else
            {
                // Nếu không phải là số hợp lệ, hiển thị thông báo lỗi
                MessageBox.Show("Vui lòng nhập năm hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DrawDoughnutChart(List<TyLeSuDungDienTheoTang> thongKeDien)
        {
            // Xóa biểu đồ cũ nếu có
            Chart_ThongKe_Dien_TheoTang.Series.Clear();

            // Tạo một series mới với kiểu Doughnut
            Series series = new Series("Tỷ lệ sử dụng điện")
            {
                ChartType = SeriesChartType.Doughnut, // Đặt kiểu biểu đồ là Doughnut
                IsValueShownAsLabel = true, // Hiển thị giá trị trên biểu đồ
                LabelFormat = "{0}%" // Định dạng hiển thị giá trị phần trăm
            };

            // Thêm các điểm dữ liệu vào biểu đồ
            foreach (var item in thongKeDien)
            {
                // Thêm các điểm X, Y vào biểu đồ (X là tầng, Y là tỷ lệ sử dụng điện)
                series.Points.AddXY($"Tầng {item.Tang}", item.TiLeSuDungDien);
            }

            // Thêm series vào biểu đồ
            Chart_ThongKe_Dien_TheoTang.Series.Add(series);

            // Thiết lập các thuộc tính cho Chart Area
            Chart_ThongKe_Dien_TheoTang.ChartAreas[0].AxisX.Title = "Tầng";
            Chart_ThongKe_Dien_TheoTang.ChartAreas[0].AxisY.Title = "Tỷ lệ sử dụng điện (%)";

            // Thiết lập màu sắc cho biểu đồ
            Chart_ThongKe_Dien_TheoTang.Series[0].Color = System.Drawing.Color.CornflowerBlue;

            // Cập nhật lại biểu đồ
            Chart_ThongKe_Dien_TheoTang.Invalidate();
        }




        private void Btn_XuatBaoCao_DSDKNT_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị năm từ TextBox
                if (!int.TryParse(txt_Nam_BC_DSNT.Text, out int nam))
                {
                    MessageBox.Show("Vui lòng nhập năm hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra DataGridView có dữ liệu không
                if (data_GV_DS_DKNT.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất báo cáo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Đường dẫn file PDF
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    Title = "Lưu báo cáo danh sách đăng ký nội trú",
                    FileName = $"BaoCao_DanhSachDKNT_{nam}.pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    PdfReport f = new PdfReport();
                    // Gọi hàm xuất PDF
                    f.ExportDanhSachDKNTFromDataGridView(filePath, data_GV_DS_DKNT, nam);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Btn_BC_DS_NoiTru_Click(object sender, EventArgs e)
        {
            // Lấy giá trị năm học từ textbox txt_Nam_BC_DSNT
            int namHoc;
            if (int.TryParse(txt_Nam_BC_DSNT.Text, out namHoc))
            {
                // Lấy danh sách sinh viên đăng ký nội trú
                List<SinhVienXetLuuTru> danhSachSinhVien = bll_tk.GetDanhSachSinhVienXetLuuTru(namHoc);

                if (danhSachSinhVien != null && danhSachSinhVien.Count > 0)
                {
                    // Đặt dữ liệu vào DataGridView
                    data_GV_DS_DKNT.DataSource = danhSachSinhVien;

                    // Thêm cột "Số Thứ Tự" vào DataGridView (nếu chưa có)
                    if (!data_GV_DS_DKNT.Columns.Contains("STT"))
                    {
                        DataGridViewTextBoxColumn sttColumn = new DataGridViewTextBoxColumn();
                        sttColumn.HeaderText = "Số Thứ Tự";
                        sttColumn.Name = "STT";
                        sttColumn.Width = 50;
                        data_GV_DS_DKNT.Columns.Insert(0, sttColumn);
                    }

                    // Điền số thứ tự cho từng hàng
                    for (int i = 0; i < data_GV_DS_DKNT.Rows.Count; i++)
                    {
                        data_GV_DS_DKNT.Rows[i].Cells["STT"].Value = i + 1; // Số thứ tự bắt đầu từ 1
                    }

                    // Đặt lại tên các cột còn lại
                    data_GV_DS_DKNT.Columns[1].HeaderText = "Mã Sinh Viên";
                    data_GV_DS_DKNT.Columns[2].HeaderText = "Tên Sinh Viên";
                    // Giả sử dữ liệu của bạn là kiểu string và chứa ngày tháng
                    foreach (DataGridViewRow row in data_GV_DS_DKNT.Rows)
                    {
                        if (row.Cells[3].Value is string dateStr)
                        {
                            if (DateTime.TryParse(dateStr, out DateTime date))
                            {
                                row.Cells[3].Value = date.ToString("dd/MM/yyyy"); // Định dạng ngày tháng
                            }
                        }

                        if (row.Cells[4].Value is string dateStr2)
                        {
                            if (DateTime.TryParse(dateStr2, out DateTime date2))
                            {
                                row.Cells[4].Value = date2.ToString("dd/MM/yyyy"); // Định dạng ngày tháng
                            }
                        }
                    }


                    data_GV_DS_DKNT.Columns[5].HeaderText = "Mã Phòng";
                    data_GV_DS_DKNT.Columns[6].HeaderText = "Tầng";

                    // Căn chỉnh và cải thiện giao diện
                    data_GV_DS_DKNT.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu sinh viên đăng ký nội trú cho năm học này.");
                }
            }
            else
            {
                MessageBox.Show("Năm học không hợp lệ, vui lòng nhập lại.");
            }
        }



        private void Btn_ThongKeTyLe_SV_ThongKe_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem txt_tk_nam có giá trị hợp lệ hay không
            if (int.TryParse(txt_tk_nam.Text, out int nam))
            {
                // Gọi phương thức thống kê từ BLL để lấy dữ liệu
                List<TyLeSinhVienOTang> thongKeData = bll_tk.GetThongKeTyLeSinhVienTheoNam(nam);

                // Hiển thị dữ liệu vào DataGridView
                dataGV_TK_TyLeSV_Tang.DataSource = thongKeData;

                // Vẽ biểu đồ Pyramid
                DrawPyramidChart(thongKeData);
            }
            else
            {
                // Nếu không phải là số hợp lệ, hiển thị thông báo lỗi
                MessageBox.Show("Vui lòng nhập năm hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DrawPyramidChart(List<TyLeSinhVienOTang> thongKeData)
        {
            // Clear chart cũ nếu có
            chart_TyLeSV_Tang.Series.Clear();

            // Thêm series mới kiểu Funnel (Phễu/Kim tự tháp)
            Series series = new Series("Tỷ Lệ Sinh Viên")
            {
                ChartType = SeriesChartType.Funnel, // Thay đổi kiểu biểu đồ thành Funnel
                IsValueShownAsLabel = true, // Hiển thị giá trị trên cột
                LabelFormat = "{0}%", // Định dạng phần trăm
                BorderWidth = 2,
            };

            // Thêm các điểm dữ liệu vào biểu đồ
            foreach (var item in thongKeData)
            {
                series.Points.AddXY($"Tầng {item.Tang}", item.TyLePhanTram);
            }

            // Thêm series vào chart
            chart_TyLeSV_Tang.Series.Add(series);

            // Thiết lập các thuộc tính cho Chart Area
            chart_TyLeSV_Tang.ChartAreas[0].AxisX.LabelStyle.Angle = -45; // Góc chữ X
            chart_TyLeSV_Tang.ChartAreas[0].AxisX.Title = "Tầng";
            chart_TyLeSV_Tang.ChartAreas[0].AxisY.Title = "Tỷ Lệ (%)";

            // Thiết lập màu sắc cho biểu đồ
            chart_TyLeSV_Tang.Series[0].Color = System.Drawing.Color.Green;

            // Cập nhật lại biểu đồ
            chart_TyLeSV_Tang.Invalidate();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
