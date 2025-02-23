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
using Control;
using GUI.Model;
using Microsoft.Office.Interop.Word;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.DataVisualization;
using System.Windows.Controls;

namespace GUI
{
    public partial class QuanLyNoiQuy : Form
    {
        private BLL_LoaiNoiQuy bllLoaiNoiQuy;
        private BLL_NoiQuy bllNoiQuy;
        private List<NoiQuy> danhSachNoiQuy;
        private BLL_ViPham bllvipham;
        private int currentIndex = 0; // Vị trí hiện tại
        public QuanLyNoiQuy()
        {
            InitializeComponent();
            this.Load += NoiQuy_Load;
            txt_maSinhVien.TextChanged += Txt_maSinhVien_TextChanged;
            txt_maSinhVien.KeyDown += Txt_maSinhVien_KeyDown;
            cbo_TenLoaiNoiQuy.SelectedIndexChanged += Cbo_TenLoaiNoiQuy_SelectedIndexChanged;
            cbo_tennoiquy.SelectedIndexChanged += Cbo_tennoiquy_SelectedIndexChanged;
            btn_LapPhieuViPham.Click += Btn_LapPhieuViPham_Click;
            btn_XuatPhieuViPham.Click += Btn_XuatPhieuViPham_Click;
            btn_Add_LoaiNoiQuy.Click += Btn_Add_LoaiNoiQuy_Click;
            btn_XoaLoaiNQ.Click += Btn_XoaLoaiNQ_Click;
            btn_Sua_LoaiNoiQuy.Click += Btn_Sua_LoaiNoiQuy_Click;
            btn_ThemNoiQuy.Click += Btn_ThemNoiQuy_Click;
            btn_xoaNoiQuy.Click += Btn_xoaNoiQuy_Click;
            btn_thongke_sl_loaivp.Click += Btn_thongke_sl_loaivp_Click;
            btn_tk_slvp_thephong.Click += Btn_tk_slvp_thephong_Click;
            dataGV_NoiQuy.CellClick += DataGV_NoiQuy_CellClick;
            cbo_Loainoiquy.SelectedIndexChanged += Cbo_Loainoiquy_SelectedIndexChanged;
            btn_UpdateNoiQuy.Click += Btn_UpdateNoiQuy_Click;
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;
            dataGV_DSViPham.CellClick += DataGV_DSViPham_CellClick;
            txt_MSSV_VP.TextChanged += Txt_MSSV_VP_TextChanged;
            txt_manv_lapvp.TextChanged += Txt_manv_lapvp_TextChanged;
            // Khởi tạo bllLoaiNoiQuy
            bllLoaiNoiQuy = new BLL_LoaiNoiQuy();
            bllNoiQuy = new BLL_NoiQuy();
            bllvipham = new BLL_ViPham();
            cbo_ql_LoaiNoiQuy.SelectedIndexChanged += Cbo_ql_LoaiNoiQuy_SelectedIndexChanged;
            txt_Masv_QLNQ.KeyDown += Txt_Masv_QLNQ_KeyDown;
            panel2.AutoScroll = true;


            panel3.AutoScroll = true;
        }

        // Sự kiện nhập mã sinh viên và nhấn Enter
        private void Txt_Masv_QLNQ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string maSinhVien = txt_Masv_QLNQ.Text.Trim();
                    if (!string.IsNullOrEmpty(maSinhVien))
                    {
                        var danhSachViPham = bllvipham.GetViPhamByMaSinhVien(maSinhVien);
                        dataGV_DSViPham.DataSource = danhSachViPham;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }





        private void Cbo_ql_LoaiNoiQuy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedLoaiNoiQuy = (cbo_ql_LoaiNoiQuy.SelectedItem as LoaiNoiQuy)?.TenLoaiNQ;
                if (!string.IsNullOrEmpty(selectedLoaiNoiQuy))
                {
                    var danhSachViPham = bllvipham.GetViPhamByTenLoaiNoiQuy(selectedLoaiNoiQuy);
                    dataGV_DSViPham.DataSource = danhSachViPham;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }




        private void LoadLoaiNoiQuyToComboBox_QL()
        {
            try
            {
                // Khởi tạo đối tượng BLL_ViPham để gọi phương thức GetAllLoaiNoiQuy
                BLL_ViPham bllViPham = new BLL_ViPham();

                // Lấy danh sách loại nội quy từ phương thức GetAllLoaiNoiQuy trong BLL
                List<LoaiNoiQuy> danhSachLoaiNoiQuy = bllViPham.GetAllLoaiNoiQuy();

                // Kiểm tra nếu danh sách có dữ liệu
                if (danhSachLoaiNoiQuy != null && danhSachLoaiNoiQuy.Count > 0)
                {
                    // Gán dữ liệu cho ComboBox
                    cbo_ql_LoaiNoiQuy.DataSource = danhSachLoaiNoiQuy;
                    cbo_ql_LoaiNoiQuy.DisplayMember = "TenLoaiNQ";  // Hiển thị tên loại nội quy
                    cbo_ql_LoaiNoiQuy.ValueMember = "MaLoaiNQ";  // Giá trị mã loại nội quy
                }
                else
                {
                    //MessageBox.Show("Không có dữ liệu loại nội quy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
              //  MessageBox.Show($"Đã xảy ra lỗi khi tải loại nội quy: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void Txt_manv_lapvp_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_manv_lapvp.Text)) // Kiểm tra nếu mã nhân viên không trống
            {
                try
                {
                    string maNhanVien = txt_manv_lapvp.Text.Trim(); // Lấy mã nhân viên từ TextBox và loại bỏ khoảng trắng thừa

                    // Tạo một instance của BLL_NhanVien
                    BLL_NhanVien bllNhanVien = new BLL_NhanVien();

                    // Lấy thông tin chi tiết nhân viên từ mã nhân viên
                    var nhanVien = bllNhanVien.GetNhanVien_MaNV(maNhanVien);

                    if (nhanVien != null)
                    {
                        // Hiển thị thông tin nhân viên trên các TextBox
                        txt_QL_HoTenNV.Text = nhanVien.HoTen;
                        //txt_ChucVuNhanVien.Text = nhanVien.ChucVu;

                        // Nếu cần thêm thông tin, hiển thị tại đây
                        // txt_DienThoai.Text = nhanVien.DienThoai;
                        // txt_Email.Text = nhanVien.Email;
                    }
                    else
                    {
                        // Xóa thông tin cũ nếu không tìm thấy nhân viên
                        txt_QL_HoTenNV.Clear();
                       // txt_ChucVuNhanVien.Clear();

                        MessageBox.Show("Không tìm thấy thông tin nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy thông tin nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Xóa thông tin trên các TextBox nếu mã nhân viên trống
                txt_QL_HoTenNV.Clear();
                //txt_ChucVuNhanVien.Clear();

                MessageBox.Show("Vui lòng nhập mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Txt_MSSV_VP_TextChanged(object sender, EventArgs e)
        {
              // Lấy mã sinh viên từ ô nhập liệu
                string maSinhVien = txt_MSSV_VP.Text.Trim();

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
                    txt_HoTen_VP.Text = sinhVienDTO.HoTen;
                    dateTimeNgaySinh_VP.Value = sinhVienDTO.NgaySinh;

                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

               
            
        }

        private void DataGV_DSViPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Kiểm tra nếu người dùng không click vào header
                if (e.RowIndex >= 0)
                {
                    // Lấy mã Vi Phạm từ cột đầu tiên
                    string maViPham = dataGV_DSViPham.Rows[e.RowIndex].Cells[0].Value.ToString();

                    // Gọi phương thức XemChiTietViPham từ BLL để lấy chi tiết phiếu vi phạm
                    ViPham viPham = bllvipham.XemChiTietViPham(maViPham);

                    // Kiểm tra nếu tìm thấy thông tin phiếu vi phạm
                    if (viPham != null)
                    {
                        // Lấy mã Nội Quy từ phiếu vi phạm
                        int maNoiQuy = viPham.MaNoiQuy;

                        // Lấy tên Nội Quy bằng cách gọi phương thức GetNoiQuyById
                        NoiQuy noiQuy = bllNoiQuy.GetNoiQuyById(maNoiQuy);

                        // Cập nhật các TextBox với thông tin từ phiếu vi phạm và Nội Quy
                        txt_MSSV_VP.Text = viPham.MaSinhVien;
                        txt_TenNoiQuyVP.Text = noiQuy?.TenNoiQuy ?? "N/A"; // Kiểm tra null nếu không có kết quả
                        txt_mota_vp.Text = viPham.MoTa;
                        txt_manv_lapvp.Text = dataGV_DSViPham.Rows[e.RowIndex].Cells[2].Value.ToString();


                        // Hiển thị thông tin chi tiết khác nếu cần
                        // ...
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy phiếu vi phạm với mã: " + maViPham);
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }


        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra chỉ mục của tab hiện tại
            int selectedIndex = tabControl1.SelectedIndex;

            // Tùy vào chỉ mục của tab, bạn sẽ tải dữ liệu khác nhau
            switch (selectedIndex)
            {
                
                
                case 2:
                    // Tải dữ liệu cho Tab 3 (Chẳng hạn là tab thống kê)
                    LoadDataForTab3();
                    break;
                default:
                    break;
            }
        }

        public void LoadDataForTab3()
        {
            LoadDanhSachViPham();
        }
        private void Btn_UpdateNoiQuy_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy mã nội quy từ cột đầu tiên của hàng được chọn trong DataGridView
                if (dataGV_NoiQuy.SelectedRows.Count > 0)
                {
                    int maNoiQuy = Convert.ToInt32(dataGV_NoiQuy.SelectedRows[0].Cells[0].Value);

                    // Tạo đối tượng NoiQuy với thông tin từ các textbox và combobox
                    NoiQuy noiQuy = new NoiQuy
                    {
                        MaNoiQuy = maNoiQuy,
                        TenNoiQuy = txt_TenNoiQuy.Text.Trim(),
                        MucPhatTien = Convert.ToDecimal(txt_MucPhatTien.Text.Trim()),
                        HinhThucXL = txt_HinhThucXuPhat.Text.Trim(),
                        //MaLoaiNQ = Convert.ToInt32(cbo_LoaiNoiQuy.SelectedValue) // Lấy giá trị mã loại nội quy từ combobox
                    };

                    // Gọi phương thức Update từ BLL
                    bool isUpdated = bllNoiQuy.UpdateNoiQuy(noiQuy);

                    if (isUpdated)
                    {
                        MessageBox.Show("Cập nhật nội quy thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData_noiQuy(); // Reload lại dữ liệu trong DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nội quy hoặc cập nhật thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một nội quy để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DataGV_NoiQuy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Kiểm tra xem hàng được chọn có hợp lệ không
                if (e.RowIndex >= 0 && e.RowIndex < dataGV_NoiQuy.Rows.Count)
                {
                    // Lấy mã nội quy từ cột đầu tiên của hàng được chọn
                    int maNoiQuy = Convert.ToInt32(dataGV_NoiQuy.Rows[e.RowIndex].Cells[0].Value);

                    // Gọi hàm để lấy chi tiết nội quy từ mã nội quy
                    NoiQuy noiQuy = bllNoiQuy.GetNoiQuyById(maNoiQuy);

                    if (noiQuy != null)
                    {
                        // Hiển thị thông tin lên các thành phần giao diện
                        txt_TenNoiQuy.Text = noiQuy.TenNoiQuy;
                        txt_MucPhatTien.Text = noiQuy.MucPhatTien.ToString();
                        txt_HinhThucXuPhat.Text = noiQuy.HinhThucXL;

                        // Lấy mã loại nội quy từ mã nội quy
                        int? maLoaiNQ = bllNoiQuy.GetMaLoaiNQByMaNoiQuy(maNoiQuy);

                        if (maLoaiNQ != null)
                        {
                            // Gọi hàm để lấy tên loại nội quy (nếu cần)
                            var tenLoaiNQ = bllNoiQuy.GetTenLoaiNoiQuy(maLoaiNQ.Value); // Hàm tự tạo (nếu cần)

                            // Hiển thị tên loại nội quy trong combobox
                            cbo_Loainoiquy.Text = tenLoaiNQ;
                        }
                        else
                        {
                            cbo_Loainoiquy.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Btn_xoaNoiQuy_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu có dòng được chọn
            if (dataGV_NoiQuy.SelectedRows.Count > 0)
            {
                // Lấy mã nội quy từ cột đầu tiên của dòng được chọn
                int maNoiQuy = Convert.ToInt32(dataGV_NoiQuy.SelectedRows[0].Cells["MaNoiQuy"].Value);

                // Gọi hàm xóa nội quy
                bool result = bllNoiQuy.DeleteNoiQuy(maNoiQuy);

                // Hiển thị thông báo thành công hoặc thất bại
                if (result)
                {
                    MessageBox.Show("Xóa nội quy thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Tải lại dữ liệu sau khi xóa
                    LoadData_noiQuy();
                }
                else
                {
                    MessageBox.Show("Xóa nội quy thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nội quy cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void LoadDanhSachViPham()
        {
            try
            {
                // Gọi hàm từ BLL để lấy danh sách vi phạm
                List<ViPham> danhSachViPham = bllvipham.LayDanhSachViPham();

                // Tắt tự động tạo cột
                dataGV_DSViPham.AutoGenerateColumns = false;

                // Xóa cột cũ nếu có
                dataGV_DSViPham.Columns.Clear();

                // Thêm cột thủ công
                dataGV_DSViPham.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "MaViPham",
                    HeaderText = "Mã Vi Phạm",
                    Width = 100
                });
                dataGV_DSViPham.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "MaNoiQuy",
                    HeaderText = "Mã Nội Quy",
                    Width = 100
                });
                dataGV_DSViPham.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "MaNhanVien",
                    HeaderText = "Mã Nhân Viên",
                    Width = 100
                });
                dataGV_DSViPham.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "MaSinhVien",
                    HeaderText = "Mã Sinh Viên",
                    Width = 100
                });
                dataGV_DSViPham.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "MoTa",
                    HeaderText = "Mô Tả",
                    Width = 200
                });
                dataGV_DSViPham.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "NgayViPham",
                    HeaderText = "Ngày Vi Phạm",
                    Width = 150
                });

                // Gán dữ liệu cho DataGridView
                dataGV_DSViPham.DataSource = danhSachViPham;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show($"Lỗi khi tải danh sách vi phạm: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_tk_slvp_thephong_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy tháng từ ComboBox cbo_thang_tk_slvp_thephong và năm từ TextBox txt_nam_tk_slvp_thephong
                int thang = int.Parse(cbo_thang_tk_slvp_thephong.SelectedItem.ToString()); // Giả sử ComboBox có giá trị tháng
                int nam = int.Parse(txt_nam_tk_slvp_thephong.Text);  // Lấy giá trị năm từ TextBox

                // Khởi tạo lớp BLL_ViPham và gọi phương thức thống kê
                BLL_ViPham bllViPham = new BLL_ViPham();
                List<ThongKeViPhamTheoPhong> danhSachThongKe = bllViPham.ThongKeViPhamTheoPhong(thang, nam);

                // Gọi hàm GetAllMaPhong từ DAL để lấy danh sách phòng từ cơ sở dữ liệu
                List<Phong> danhSachPhong = bllViPham.GetAllMaPhong(); // Lấy danh sách mã phòng từ DAL

                // Đảm bảo rằng các phòng không có vi phạm cũng được thêm vào danh sách thống kê
                foreach (var phong in danhSachPhong)
                {
                    if (!danhSachThongKe.Any(x => x.SoPhong == phong.MaPhong))
                    {
                        // Nếu phòng không có vi phạm, thêm vào danh sách với số lượng vi phạm bằng 0
                        danhSachThongKe.Add(new ThongKeViPhamTheoPhong { SoPhong = phong.MaPhong, SoLuongViPham = 0 });
                    }
                }

                // Sắp xếp danh sách thống kê theo mã phòng nếu cần (tùy chọn)
                danhSachThongKe = danhSachThongKe.OrderBy(x => x.SoPhong).ToList();

                // Hiển thị kết quả vào DataGridView
                dataGV_tk_slvp_thephong.DataSource = danhSachThongKe;

                // Vẽ biểu đồ hình tròn (Pie Chart)
                Chart_tk_slvp_thephong.Series.Clear();
                Chart_tk_slvp_thephong.ChartAreas.Clear();
                System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea("ChartArea1");
                Chart_tk_slvp_thephong.ChartAreas.Add(chartArea);

                // Tạo series cho biểu đồ hình tròn
                System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series("ViPham");
                series.ChartType = SeriesChartType.Pie; // Chọn biểu đồ hình tròn
                series.IsValueShownAsLabel = true; // Hiển thị giá trị trên biểu đồ

                // Thêm các điểm dữ liệu vào biểu đồ
                foreach (var item in danhSachThongKe)
                {
                    series.Points.AddXY(item.SoPhong, item.SoLuongViPham); // Thêm tên phòng và số lượng vi phạm
                }

                // Thêm series vào biểu đồ
                Chart_tk_slvp_thephong.Series.Add(series);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thống kê: {ex.Message}");
            }
        }



        private void Btn_thongke_sl_loaivp_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị tháng và năm từ các control
                int thang = int.Parse(cbo_thang_tk_sl_loaiNQ.SelectedItem.ToString());
                int nam = int.Parse(txt_nam_tk_sl_loaiNQ.Text);

                // Tạo đối tượng BLL và gọi hàm thống kê
                BLL_ViPham bllViPham = new BLL_ViPham();
                List<ThongKeLoaiViPham> thongKeList = bllViPham.ThongKeViPhamTheoThangNam(thang, nam);

                // Lấy danh sách tất cả các loại nội quy (bằng cách gọi BLL_LoaiNoiQuy nếu cần)
                List<LoaiNoiQuy> allLoaiNoiQuy = bllLoaiNoiQuy.GetAllLoaiNoiQuy(); // Phương thức này sẽ lấy danh sách tất cả loại nội quy

                // Đảm bảo rằng tất cả các loại nội quy đều có trong danh sách thống kê
                foreach (var loai in allLoaiNoiQuy)
                {
                    var existing = thongKeList.FirstOrDefault(x => x.TenLoaiNoiQuy == loai.TenLoaiNQ);
                    if (existing == null)
                    {
                        // Nếu loại nội quy không có trong danh sách thống kê, thêm vào với số lượng vi phạm là 0
                        thongKeList.Add(new ThongKeLoaiViPham { TenLoaiNoiQuy = loai.TenLoaiNQ, SoluongViPham = 0 });
                    }
                }

                // Hiển thị dữ liệu lên dataGV_TK_SL_LoaiNQ
                dataGV_TK_SL_LoaiNQ.DataSource = thongKeList;

                // Thiết lập biểu đồ cột cho BieuDo_TK_SL_LoaiNQ
                BieuDo_TK_SL_LoaiNQ.Series.Clear(); // Xóa series cũ
                BieuDo_TK_SL_LoaiNQ.Titles.Clear(); // Xóa tiêu đề cũ
                BieuDo_TK_SL_LoaiNQ.ChartAreas[0].AxisX.Title = "Tên loại nội quy";
                BieuDo_TK_SL_LoaiNQ.ChartAreas[0].AxisY.Title = "Số lượng vi phạm";

                // Khởi tạo Series mới cho biểu đồ
                System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Số lượng vi phạm",
                    ChartType = SeriesChartType.Column, // Kiểu cột
                    XValueType = ChartValueType.String,
                    YValueType = ChartValueType.Int32
                };

                // Thêm dữ liệu vào Series
                foreach (var item in thongKeList)
                {
                    series.Points.AddXY(item.TenLoaiNoiQuy, item.SoluongViPham);
                }

                // Thêm Series vào biểu đồ
                BieuDo_TK_SL_LoaiNQ.Series.Add(series);

                // Thiết lập tiêu đề biểu đồ
                BieuDo_TK_SL_LoaiNQ.Titles.Add($"Thống kê số lượng vi phạm theo loại nội quy - Tháng {thang}/{nam}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}");
            }
        }



        private void Btn_ThemNoiQuy_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbo_Loainoiquy.SelectedItem != null)
                {
                    // Lấy đối tượng LoaiNoiQuy từ ComboBox
                    var loaiNoiQuyChon = (LoaiNoiQuy)cbo_Loainoiquy.SelectedItem;

                    // Lấy tên loại nội quy và mã loại nội quy từ đối tượng
                    string tenLoaiNoiQuy = loaiNoiQuyChon.TenLoaiNQ;
                    int maLoaiNoiQuy = loaiNoiQuyChon.MaLoaiNQ;

                    // Lấy thông tin từ các TextBox
                    string tenNoiQuy = txt_TenNoiQuy.Text.Trim();
                    decimal mucPhatTien = decimal.Parse(txt_MucPhatTien.Text.Trim()); // Chuyển đổi từ TextBox thành decimal
                    string hinhThucXuPhat = txt_HinhThucXuPhat.Text.Trim();

                    // Tạo đối tượng NoiQuy
                    NoiQuy noiQuyMoi = new NoiQuy
                    {
                        TenNoiQuy = tenNoiQuy,
                        MucPhatTien = mucPhatTien,
                        HinhThucXL = hinhThucXuPhat,
                        MaLoaiNQ = maLoaiNoiQuy, // Liên kết với loại nội quy đã chọn
                        TrangThai = 0
                    };

                    // Gọi hàm AddNoiQuy từ BLL để thêm vào cơ sở dữ liệu
                    BLL_NoiQuy bllNoiQuy = new BLL_NoiQuy();
                    bool isSuccess = bllNoiQuy.AddNoiQuy(noiQuyMoi);

                    if (isSuccess)
                    {
                        MessageBox.Show("Thêm nội quy thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Có thể làm mới form hoặc cập nhật danh sách nội quy nếu cần
                    }
                    else
                    {
                        MessageBox.Show("Thêm nội quy thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn loại nội quy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cbo_Loainoiquy_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem có item nào được chọn trong ComboBox không
            if (cbo_Loainoiquy.SelectedItem != null)
            {
                // Lấy đối tượng LoaiNoiQuy đầy đủ từ SelectedItem
                LoaiNoiQuy loaiNoiQuy = (LoaiNoiQuy)cbo_Loainoiquy.SelectedItem;

                // Gọi phương thức từ BLL_LoaiNoiQuy để lấy loại nội quy theo tên
                BLL_LoaiNoiQuy bllLoaiNoiQuy = new BLL_LoaiNoiQuy();
                LoaiNoiQuy loaiNoiQuyDetails = bllLoaiNoiQuy.GetLoaiNoiQuyByNameBLL(loaiNoiQuy.TenLoaiNQ);  // Lấy thông tin chi tiết của loại nội quy

                if (loaiNoiQuyDetails != null)
                {
                    // Nếu tìm thấy, hiển thị các thông tin vào TextBox
                    txt_TenLoaiNoiQuy.Text = loaiNoiQuyDetails.TenLoaiNQ;  // Hiển thị tên loại nội quy vào txt_TenLoaiNoiQuy
                    txt_MotaNoiQuy.Text = loaiNoiQuyDetails.MoTa;  // Hiển thị mô tả vào txt_MotaNoiQuy
                }
                else
                {
                    // Nếu không tìm thấy, bạn có thể để trống các TextBox hoặc hiển thị thông báo
                    txt_TenLoaiNoiQuy.Clear();
                    txt_MotaNoiQuy.Clear();
                    //MessageBox.Show("Không tìm thấy loại nội quy với tên: " + loaiNoiQuy.TenLoaiNQ, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // Nếu không có lựa chọn nào, có thể để trống các TextBox
                txt_TenLoaiNoiQuy.Clear();
                txt_MotaNoiQuy.Clear();
            }

        }

        private void Btn_Sua_LoaiNoiQuy_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem người dùng có chọn dòng nào trong DataGridView không
                if (dataGV_LoaiNoiQuy.CurrentRow != null)
                {
                    // Lấy mã loại nội quy từ cột đầu tiên (MaLoaiNQ) của dòng hiện tại
                    int maLoaiNoiQuy = (int)dataGV_LoaiNoiQuy.CurrentRow.Cells["MaLoaiNQ"].Value;

                    // Lấy dữ liệu từ các TextBox
                    string tenLoaiNoiQuy = txt_TenLoaiNoiQuy.Text;
                    string moTaLoaiNoiQuy = txt_MotaNoiQuy.Text;

                    // Tạo đối tượng LoaiNoiQuy mới với dữ liệu từ giao diện
                    LoaiNoiQuy loaiNoiQuy = new LoaiNoiQuy
                    {
                        MaLoaiNQ = maLoaiNoiQuy,
                        TenLoaiNQ = tenLoaiNoiQuy,
                        MoTa = moTaLoaiNoiQuy
                    };

                    // Gọi hàm cập nhật loại nội quy từ BLL
                    BLL_LoaiNoiQuy bllLoaiNoiQuy = new BLL_LoaiNoiQuy();
                    bool isUpdated = bllLoaiNoiQuy.UpdateLoaiNoiQuy(loaiNoiQuy);

                    if (isUpdated)
                    {
                        MessageBox.Show("Loại nội quy đã được cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataGridView(); // Cập nhật lại DataGridView sau khi sửa
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi cập nhật loại nội quy.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn loại nội quy cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Btn_XoaLoaiNQ_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem người dùng có chọn dòng nào trong DataGridView không
                if (dataGV_LoaiNoiQuy.CurrentRow != null)
                {
                    // Lấy mã loại nội quy từ cột đầu tiên (MaLoaiNQ) của dòng hiện tại
                    int maLoaiNoiQuy = (int)dataGV_LoaiNoiQuy.CurrentRow.Cells["MaLoaiNQ"].Value;

                    // Gọi hàm xóa loại nội quy từ BLL
                    BLL_LoaiNoiQuy bllLoaiNoiQuy = new BLL_LoaiNoiQuy();
                    bool isDeleted = bllLoaiNoiQuy.XoaLoaiNoiQuy(maLoaiNoiQuy);

                    if (isDeleted)
                    {
                        MessageBox.Show("Loại nội quy đã được xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataGridView(); // Cập nhật lại DataGridView sau khi xóa
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra hoặc loại nội quy không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn loại nội quy cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Add_LoaiNoiQuy_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ TextBox
            string tenLoaiNoiQuy = txt_TenLoaiNoiQuy.Text;
            string moTaNoiQuy = txt_MotaNoiQuy.Text;

            // Kiểm tra dữ liệu hợp lệ
            if (string.IsNullOrEmpty(tenLoaiNoiQuy) || string.IsNullOrEmpty(moTaNoiQuy))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo đối tượng LoaiNoiQuy
            LoaiNoiQuy loaiNoiQuy = new LoaiNoiQuy
            {
                TenLoaiNQ = tenLoaiNoiQuy,
                MoTa = moTaNoiQuy
            };

            // Gọi phương thức thêm loại nội quy từ BLL
            bool isAdded = bllLoaiNoiQuy.AddLoaiNoiQuy(loaiNoiQuy);

            // Hiển thị thông báo nếu thêm thành công hoặc thất bại
            if (isAdded)
            {
                MessageBox.Show("Thêm loại nội quy thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGridView(); // Cập nhật lại DataGridView sau khi thêm thành công
            }
            else
            {
                MessageBox.Show("Thêm loại nội quy thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hàm thay thế placeholder trong tài liệu Word
        /// </summary>
        /// <param name="doc">Tài liệu Word</param>
        /// <param name="placeholder">Chuỗi placeholder</param>
        /// <param name="replacementText">Chuỗi thay thế</param>
        private void ReplacePlaceholder(Document doc, string placeholder, string replacementText)
        {
            Find findObject = doc.Content.Find;
            findObject.ClearFormatting();
            findObject.Text = placeholder;
            findObject.Replacement.ClearFormatting();
            findObject.Replacement.Text = replacementText;

            object replaceAll = WdReplace.wdReplaceAll;
            findObject.Execute(Replace: ref replaceAll);
        }
        private void Btn_XuatPhieuViPham_Click(object sender, EventArgs e)
        {
            // Đường dẫn tới mẫu Word
            string templatePath = @"D:\DACN\BienBanViPham.docx";
            string outputPath = @"D:\DACN\BienBanViPham_" + txt_maSinhVien.Text + ".docx";

            // Tạo ứng dụng Word và mở file mẫu
            Microsoft.Office.Interop.Word._Application wordApp = new Microsoft.Office.Interop.Word.Application();
            Document doc = wordApp.Documents.Open(templatePath);

            try
            {
                int MaTK = DangNhap.MaTK;
                BLL_NhanVien nv = new BLL_NhanVien();
                // Lấy thông tin nhân viên bằng mã tài khoản
                NhanVien nhanVien = nv.GetNhanVienDetailsByMaTaiKhoan(MaTK);

                // Lấy dữ liệu từ các control trên giao diện
                string HoVaTenNhanVien = nhanVien.HoTen;
                string ChucVu = nhanVien.ChucVu;
                string maSinhVien = txt_maSinhVien.Text;
                string hoVaTen = txt_HoVaTen.Text;
                string ngaySinh = dateTime_NgaySinh.Value.ToString("dd/MM/yyyy");
                string gioiTinh = radio_Nam.Checked ? "Nam" : "Nữ";
                string maPhong = txt_MaPhong.Text;
                string tenLoaiNoiQuy = cbo_TenLoaiNoiQuy.Text;
                string tenNoiQuy = cbo_tennoiquy.Text;
                string ngayViPham = date_LapPhieu.Value.ToString("dd/MM/yyyy");
                string maNoiQuy = txt_MaNoiQuy.Text;
                string mucPhat = txt_MucPhat.Text;
                string hinhThuc = txt_HinhThuc.Text;
                string moTaViPham = txt_motavipham.Text;

                // Thay thế các placeholder trong tài liệu
                ReplacePlaceholder(doc, "«SoQuyetDinh»", maNoiQuy);
                ReplacePlaceholder(doc, "«Ngay»", date_LapPhieu.Value.Day.ToString());
                ReplacePlaceholder(doc, "«Thang»", date_LapPhieu.Value.Month.ToString());
                ReplacePlaceholder(doc, "«Nam»", date_LapPhieu.Value.Year.ToString());
                ReplacePlaceholder(doc, "«HoVaTen»", hoVaTen);
                ReplacePlaceholder(doc, "«MaSinhVien»", maSinhVien);
                ReplacePlaceholder(doc, "«NgaySinh»", ngaySinh);
                ReplacePlaceholder(doc, "«Giới Tính»", gioiTinh);
                ReplacePlaceholder(doc, "«MaPhong»", maPhong);
                ReplacePlaceholder(doc, "«LoaiViPham»", tenLoaiNoiQuy);
                ReplacePlaceholder(doc, "«TenViPham»", tenNoiQuy);
                ReplacePlaceholder(doc, "«NgayViPham»", ngayViPham);
                ReplacePlaceholder(doc, "«Mô Tả»", moTaViPham);
                ReplacePlaceholder(doc, "«Mức Phạt Tiền»", mucPhat);
                ReplacePlaceholder(doc, "«Hình Thức»", hinhThuc);
                ReplacePlaceholder(doc, "«HoVaTenNhanVien»", HoVaTenNhanVien);
                ReplacePlaceholder(doc, "«ChucVu»", ChucVu);
                // Lưu tài liệu với tên file mới
                doc.SaveAs2(outputPath);
                MessageBox.Show("Biên bản đã được lưu tại: " + outputPath, "Thông báo");

            }
            finally
            {
                // Đóng tài liệu và ứng dụng Word
                doc.Close();
                wordApp.Quit();
            }
        }

        private void Btn_LapPhieuViPham_Click(object sender, EventArgs e)
        {
            try
            {
                int MaTK = DangNhap.MaTK;
                BLL_NhanVien nv = new BLL_NhanVien();
                // Lấy thông tin nhân viên bằng mã tài khoản
                NhanVien nhanVien = nv.GetNhanVienDetailsByMaTaiKhoan(MaTK);

                // Kiểm tra nếu nhân viên tồn tại
                if (nhanVien != null)
                {
                    // Chuyển đổi maNoiQuy từ string sang int
                    if (int.TryParse(txt_MaNoiQuy.Text, out int maNoiQuy))
                    {
                        string maSinhVien = txt_maSinhVien.Text;
                        DateTime ngayViPham = date_LapPhieu.Value;
                        string moTaViPham = txt_motavipham.Text;
                       // MessageBox.Show("Mã Sinh Viên " + maSinhVien);

                        // Tạo đối tượng ViPham với các thông tin từ form
                        ViPham viPham = new ViPham()
                        {
                            MaNoiQuy = maNoiQuy,
                            MaNhanVien = nhanVien.MaNhanVien,
                            MaSinhVien = maSinhVien,
                            NgayViPham = ngayViPham,
                            MoTa = moTaViPham,
                            TrangThai = 0 // Trạng thái bắt đầu là 0 (Chờ xử lý)
                        };

                        // Khởi tạo lớp BLL_ViPham và gọi phương thức LapPhieuViPham
                        BLL_ViPham bllViPham = new BLL_ViPham();
                        bool isAdded = bllViPham.LapPhieuViPham(viPham);

                        // Thông báo kết quả
                        if (isAdded)
                        {
                            MessageBox.Show("Lập phiếu vi phạm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Lập phiếu vi phạm thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã nội quy phải là một số nguyên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên với mã tài khoản này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Cbo_tennoiquy_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu ComboBox không rỗng và có item được chọn
            if (cbo_tennoiquy.SelectedItem != null)
            {
                // Lấy tên nội quy từ ComboBox
                string tenNoiQuy = cbo_tennoiquy.Text.Trim();

                // Khởi tạo lớp BLL_NoiQuy
                BLL_NoiQuy bllNoiQuy = new BLL_NoiQuy();

                // Gọi phương thức trong BLL để lấy thông tin nội quy
                NoiQuy noiQuy = bllNoiQuy.GetNoiQuyByTenNoiQuy(tenNoiQuy);

                // Kiểm tra nếu nội quy được tìm thấy
                if (noiQuy != null)
                {
                    // Cập nhật các TextBox với thông tin của nội quy
                    txt_MaNoiQuy.Text = noiQuy.MaNoiQuy.ToString();
                    txt_HinhThuc.Text = noiQuy.HinhThucXL;
                    txt_MucPhat.Text = noiQuy.MucPhatTien.ToString(); // Hiển thị dưới dạng tiền tệ
                }
                else
                {
                    // Nếu không tìm thấy nội quy, hiển thị thông báo
                    // MessageBox.Show("Không tìm thấy nội quy tương ứng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // Nếu không có mục nào được chọn trong ComboBox, xóa dữ liệu TextBox
                txt_MaNoiQuy.Clear();
                txt_HinhThuc.Clear();
                txt_MucPhat.Clear();
            }
        }

        //load để lập phiếu 
        private void LoadQuanLyNoiQuyToComboBox()
        {
            // Lấy danh sách loại nội quy từ BLL
            List<LoaiNoiQuy> danhSachLoaiNoiQuy = bllLoaiNoiQuy.GetAllLoaiNoiQuy();

            // Kiểm tra nếu danh sách không rỗng
            if (danhSachLoaiNoiQuy != null && danhSachLoaiNoiQuy.Count > 0)
            {
                // Gán danh sách vào ComboBox
                cbo_TenLoaiNoiQuy.DataSource = danhSachLoaiNoiQuy;
                cbo_TenLoaiNoiQuy.DisplayMember = "TenLoaiNQ"; // Tên hiển thị trong ComboBox
                cbo_TenLoaiNoiQuy.ValueMember = "MaLoaiNQ";    // Giá trị ẩn của từng mục trong ComboBox
            }
            else
            {
             //   MessageBox.Show("Không có dữ liệu loại nội quy để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadLoaiNoiQuyToComboBox()
        {
            try
            {
                // Khởi tạo đối tượng BLL_LoaiNoiQuy
                BLL_LoaiNoiQuy bllLoaiNoiQuy = new BLL_LoaiNoiQuy();

                // Lấy tất cả loại nội quy từ BLL
                List<LoaiNoiQuy> danhSachLoaiNoiQuy = bllLoaiNoiQuy.GetAllLoaiNoiQuy();

                // Đưa tên loại nội quy vào ComboBox
                cbo_Loainoiquy.DataSource = danhSachLoaiNoiQuy;  // Gán danh sách vào ComboBox
                cbo_Loainoiquy.DisplayMember = "TenLoaiNQ";  // Hiển thị tên loại nội quy
                cbo_Loainoiquy.ValueMember = "MaLoaiNQ";  // Lưu giá trị mã loại nội quy
            }
            catch (Exception ex)
            {
              //  MessageBox.Show("Có lỗi xảy ra khi tải loại nội quy: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cbo_TenLoaiNoiQuy_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu có mục nào được chọn trong ComboBox
            if (cbo_TenLoaiNoiQuy.SelectedItem != null)
            {
                // Lấy đối tượng LoaiNoiQuy từ ComboBox
                var loaiNoiQuyChon = (LoaiNoiQuy)cbo_TenLoaiNoiQuy.SelectedItem;

                // Lấy tên loại nội quy và mã loại nội quy từ đối tượng
                string tenLoaiNoiQuy = loaiNoiQuyChon.TenLoaiNQ;
                int maLoaiNoiQuy = loaiNoiQuyChon.MaLoaiNQ;

                // Hiển thị thông tin trong MessageBox để debug
                //MessageBox.Show($"Tên loại nội quy: {tenLoaiNoiQuy}\nMã loại nội quy: {maLoaiNoiQuy}",
                //                "Debug",
                //                MessageBoxButtons.OK,
                //                MessageBoxIcon.Information);

                // Kiểm tra nếu mã loại nội quy hợp lệ (không phải null)
                if (maLoaiNoiQuy > 0)
                {
                    // Gọi BLL để lấy danh sách nội quy theo mã loại nội quy
                    var danhSachNoiQuy = bllNoiQuy.GetNoiQuyByMaLoaiNQ(maLoaiNoiQuy);

                    // Kiểm tra nếu danh sách nội quy không rỗng
                    if (danhSachNoiQuy != null && danhSachNoiQuy.Count > 0)
                    {
                        // Gán danh sách nội quy vào ComboBox của nội quy
                        cbo_tennoiquy.DataSource = danhSachNoiQuy;
                        cbo_tennoiquy.DisplayMember = "TenNoiQuy"; // Hiển thị tên nội quy
                        cbo_tennoiquy.ValueMember = "MaNoiQuy";    // Giá trị thực là mã nội quy
                    }
                    else
                    {
                        // Nếu không có dữ liệu nội quy, thông báo cho người dùng
                        cbo_tennoiquy.DataSource = null;
                       // MessageBox.Show("Không có nội quy cho loại nội quy này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // Nếu mã loại nội quy không hợp lệ, thông báo cho người dùng
                   // MessageBox.Show("Mã loại nội quy không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // Nếu không có mục nào được chọn, hiển thị thông báo
               // MessageBox.Show("Vui lòng chọn loại nội quy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    
        ////private void LoadLoaiNoiQuyToComboBox()
        ////{
        ////    // Lấy danh sách loại nội quy từ BLL
        ////    List<LoaiNoiQuy> danhSachLoaiNoiQuy = bllLoaiNoiQuy.GetAllLoaiNoiQuy();

        ////    // Kiểm tra nếu danh sách không rỗng
        ////    if (danhSachLoaiNoiQuy != null && danhSachLoaiNoiQuy.Count > 0)
        ////    {
        ////        // Gán danh sách vào ComboBox
        ////        cbo_TenLoaiNoiQuy.DataSource = danhSachLoaiNoiQuy;
        ////        cbo_TenLoaiNoiQuy.DisplayMember = "TenLoaiNQ"; // Tên hiển thị trong ComboBox
        ////        cbo_TenLoaiNoiQuy.ValueMember = "MaLoaiNQ";    // Giá trị ẩn của từng mục trong ComboBox
        ////    }
        ////    else
        ////    {
        ////        MessageBox.Show("Không có dữ liệu loại nội quy để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ////    }
        ////}
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

        private void Txt_maSinhVien_TextChanged(object sender, EventArgs e)
        {
            //// Kiểm tra nếu người dùng nhấn Enter
            //if (e is KeyEventArgs keyArgs && keyArgs.KeyCode == Keys.Enter)
            //{
            //    // Lấy mã sinh viên từ ô nhập liệu
            //    string maSinhVien = txt_maSinhVien.Text.Trim();

            //    // Kiểm tra nếu mã sinh viên không rỗng
            //    if (!string.IsNullOrEmpty(maSinhVien))
            //    {
            //        // Khởi tạo đối tượng BLL_SinhVien
            //        BLL_SinhVien bll_sinhvien = new BLL_SinhVien();

            //        // Gọi hàm GetSinhVienPhong để lấy thông tin sinh viên
            //        SinhVienDTO sinhVienDTO = bll_sinhvien.GetSinhVienPhong(maSinhVien);

            //        // Kiểm tra nếu sinh viên tồn tại
            //        if (sinhVienDTO != null)
            //        {
            //            // Hiển thị thông tin sinh viên lên các ô tương ứng
            //            txt_HoVaTen.Text = sinhVienDTO.HoTen;
            //            dateTime_NgaySinh.Value = sinhVienDTO.NgaySinh;

            //            // Kiểm tra giới tính và chọn radio button tương ứng
            //            if (sinhVienDTO.GioiTinh == "Nam")
            //            {
            //                radio_Nam.Checked = true;
            //            }
            //            else if (sinhVienDTO.GioiTinh == "Nữ")
            //            {
            //                radio_Nu.Checked = true;
            //            }

            //           // txt_sdt.Text = sinhVienDTO.s;
            //            txt_MaPhong.Text = sinhVienDTO.Phong?.MaPhong;
            //        }
            //        else
            //        {
            //            MessageBox.Show("Không tìm thấy thông tin sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //    }
            //}
        }

        private void NoiQuy_Load(object sender, EventArgs e)
        {
            LoadLoaiNoiQuyToComboBox_QL();
            LoadDataGridView();
            LoadData_noiQuy();
            // Gọi hàm load dữ liệu vào ComboBox
            LoadLoaiNoiQuyToComboBox();
            LoadQuanLyNoiQuyToComboBox();
            //LoadNoiQuyToComboBox();
            LoadDanhSachViPham();

        }

        // Hàm load dữ liệu lên DataGridView
        private void LoadDataGridView()
        {
            List<LoaiNoiQuy> danhSachLoaiNoiQuy = bllLoaiNoiQuy.GetAllLoaiNoiQuy(); // Gọi BLL để lấy dữ liệu
            dataGV_LoaiNoiQuy.DataSource = danhSachLoaiNoiQuy; // Gán dữ liệu vào DataGridView
            dataGV_LoaiNoiQuy.Columns["MaLoaiNQ"].HeaderText = "Mã Loại Nội Quy"; // Đặt tên cột
            dataGV_LoaiNoiQuy.Columns["TenLoaiNQ"].HeaderText = "Tên Loại Nội Quy"; // Đặt tên cột
            dataGV_LoaiNoiQuy.Columns["MoTa"].HeaderText = "Mô Tả "; // Đặt tên cột
        }
        private void LoadData_noiQuy()
        {
            // Giả sử bạn đã có phương thức để lấy danh sách nội quy
            danhSachNoiQuy = bllNoiQuy.GetAllNoiQuy(); // Lấy dữ liệu từ BLL
            dataGV_NoiQuy.DataSource = danhSachNoiQuy; // Gán dữ liệu vào DataGridView
            dataGV_NoiQuy.Columns["MaNoiQuy"].HeaderText = "Mã Nội Quy"; // Đặt tên cột
            dataGV_NoiQuy.Columns["TenNoiQuy"].HeaderText = "TênNội Quy"; // Đặt tên cột
            dataGV_NoiQuy.Columns["HinhThucXL"].HeaderText = "Hình Thức Xử Phạt"; // Đặt tên cột
            ShowData(); // Hiển thị dữ liệu
        }
        private void ShowData()
        {
            // Xóa dữ liệu trong DataGridView trước khi hiển thị dữ liệu mới
            dataGV_NoiQuy.DataSource = null;

            // Lấy 5 bản ghi từ vị trí currentIndex
            var dataToShow = danhSachNoiQuy.Skip(currentIndex).Take(5).ToList();

            // Nếu không có bản ghi nào để hiển thị
            if (dataToShow.Count == 0)
            {
                dataGV_NoiQuy.DataSource = null;
              //  MessageBox.Show("Không có dữ liệu để hiển thị.");
                return;
            }

            // Gán dữ liệu vào DataGridView
            dataGV_NoiQuy.DataSource = dataToShow;

            // Cập nhật thông báo
            UpdateStatus();
        }

        private void UpdateStatus()
        {
           // lblStatus.Text = $"Đang xem bản ghi từ {currentIndex + 1} đến {Math.Min(currentIndex + 5, danhSachNoiQuy.Count)} trong tổng số {danhSachNoiQuy.Count} bản ghi.";
        }

        private void btn_truoc_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex -= 5; // Giảm 5 bản ghi
                ShowData();
            }
            else
            {
                MessageBox.Show("Bạn đang ở đầu danh sách.");
            }

        }

        private void btn_sau_Click(object sender, EventArgs e)
        {
            if (currentIndex + 5 < danhSachNoiQuy.Count)
            {
                currentIndex += 5; // Tăng 5 bản ghi
                ShowData();
            }
            else
            {
                MessageBox.Show("Bạn đang ở cuối danh sách.");
            }
        }

        private void dataGV_LoaiNoiQuy_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGV_LoaiNoiQuy.CurrentRow != null)
            {
                int maLoaiNQ = (int)dataGV_LoaiNoiQuy.CurrentRow.Cells["MaLoaiNQ"].Value;
                LoadDataNoiQuyTheoLoai(maLoaiNQ);
                LoadDataLoaiNoiQuy(maLoaiNQ);
            }
        }
        // Hàm để load dữ liệu của Nội Quy dựa trên mã loại nội quy
        private void LoadDataNoiQuyTheoLoai(int maLoaiNQ)
        {
            // Gọi phương thức trong BLL để lấy danh sách nội quy theo mã loại nội quy
            danhSachNoiQuy = bllNoiQuy.GetNoiQuyByMaLoaiNQ(maLoaiNQ);

            // Hiển thị dữ liệu vào DataGridView của Nội Quy
            dataGV_NoiQuy.DataSource = danhSachNoiQuy;
            dataGV_NoiQuy.Columns["MaNoiQuy"].HeaderText = "Mã Nội Quy"; // Đặt tên cột
            dataGV_NoiQuy.Columns["TenNoiQuy"].HeaderText = "TênNội Quy"; // Đặt tên cột
            dataGV_NoiQuy.Columns["HinhThucXL"].HeaderText = "Hình Thức Xử Phạt"; // Đặt tên cột
            ShowData();
        }
        // Hàm tải dữ liệu nội quy theo loại
        private void LoadDataLoaiNoiQuy(int maLoaiNQ)
        {
            // Lấy nội quy từ BLL dựa trên mã loại nội quy
            var noiQuy = bllLoaiNoiQuy.GetLoaiNoiQuyById(maLoaiNQ);

            // Kiểm tra và hiển thị thông tin nội quy vào các TextBox
            if (noiQuy != null)
            {
                txt_TenLoaiNoiQuy.Text = noiQuy.TenLoaiNQ;
                txt_MotaNoiQuy.Text = noiQuy.MoTa;
            }
            else
            {
                // Nếu không có dữ liệu, xóa trống các TextBox
                txt_TenLoaiNoiQuy.Clear();
                txt_MotaNoiQuy.Clear();
            }
        }

        private void dataGV_NoiQuy_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGV_NoiQuy.CurrentRow != null)
            {
                // Lấy mã nội quy từ dòng được chọn
                int maNoiQuy = (int)dataGV_NoiQuy.CurrentRow.Cells["MaNoiQuy"].Value;

                // Gọi hàm để lấy thông tin nội quy chi tiết
                var noiQuy = bllNoiQuy.GetNoiQuyById(maNoiQuy);

                // Kiểm tra và hiển thị thông tin nội quy vào các TextBox
                if (noiQuy != null)
                {
                    txt_TenNoiQuy.Text = noiQuy.TenNoiQuy;
                    txt_MucPhatTien.Text = noiQuy.MucPhatTien.ToString(); // Định dạng số tiền với 2 chữ số thập phân
                    txt_HinhThucXuPhat.Text = noiQuy.HinhThucXL;
                }
                else
                {
                    // Nếu không có dữ liệu, xóa trống các TextBox
                    txt_TenNoiQuy.Clear();
                    txt_MucPhatTien.Clear();
                    txt_HinhThucXuPhat.Clear();
                }
            }
        }








        private void txt_maSinhVien_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txt_maSinhVien_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void QuanLyNoiQuy_Load(object sender, EventArgs e)
        {

        }

        private void BieuDo_TK_SL_LoaiNQ_Click(object sender, EventArgs e)
        {

        }
    }
}