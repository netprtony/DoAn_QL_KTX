using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;
namespace GUI
{
    public partial class ThongTinLuuTru : Form
    {
        BLL_QL_LuuTru xuly = new BLL_QL_LuuTru();
        BLL_QL_Phong bll_phong = new BLL_QL_Phong();
        public ThongTinLuuTru()
        {
            InitializeComponent();

            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
           dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
         //   dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
            this.Load += ThongTinLuuTru_Load;
            Load_ComboMaPhong();
            btn_Loc.Click += Btn_Loc_Click;
            btn_TimKiem.Click += Btn_TimKiem_Click;
            //    cbo_maPhong.SelectedIndexChanged += Cbo_maPhong_SelectedIndexChanged;
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void Btn_TimKiem_Click(object sender, EventArgs e)
        {
            string maSV = txt_TimKiemTheo.Text.Trim();
            if (string.IsNullOrEmpty(maSV))
            {
                MessageBox.Show("Vui lòng nhập mã số sinh viên cần tìm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy danh sách sinh viên theo mã
            var sinhvienList = xuly.GetTheoMaSV(maSV);

            if (sinhvienList != null && sinhvienList.Count > 0)
            {
                // Nếu có sinh viên tìm thấy, hiển thị danh sách vào DataGridView
                dataGridView1.DataSource = sinhvienList;
                LoadGridview();
            }
            else
            {
                // Nếu không tìm thấy sinh viên, hiển thị thông báo và danh sách đăng ký phòng mặc định
                MessageBox.Show("Không tìm thấy sinh viên với mã số này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = xuly.GetDangKyPhong_();
                LoadGridview();
            }
        }


        private void Cbo_maPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_maPhong.SelectedValue != null)
            {
                string selectedMaPhong = cbo_maPhong.SelectedValue.ToString();

                // Gọi phương thức lấy dữ liệu theo mã phòng đã chọn và hiển thị trong DataGridView
                dataGridView1.DataSource = xuly.GetDangKyPhongTheoMaPhong(selectedMaPhong);
                LoadGridview();
            }
        }

        private void Btn_Loc_Click(object sender, EventArgs e)
        {
            if (cbo_maPhong.SelectedValue != null)
            {
                string selectedMaPhong = cbo_maPhong.SelectedValue.ToString();

                // Lấy dữ liệu theo mã phòng đã chọn và hiển thị trong DataGridView
                dataGridView1.DataSource = xuly.GetDangKyPhongTheoMaPhong(selectedMaPhong);
                LoadGridview();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn mã phòng để lọc.");
            }
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                {
                    MessageBox.Show("Hàng hoặc cột không hợp lệ.");
                    return;
                }

                // Lấy tên cột
                string columnName = dataGridView1.Columns[e.ColumnIndex]?.Name;
                if (string.IsNullOrEmpty(columnName))
                {
                    MessageBox.Show("Tên cột không hợp lệ.");
                    return;
                }

                // Lấy giá trị mới từ ô được chỉnh sửa
                string duLieuMoi = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex]?.Value?.ToString();
                if (string.IsNullOrEmpty(duLieuMoi))
                {
                    MessageBox.Show("Dữ liệu không thể để trống.");
                    return;
                }

                // Xử lý logic tùy theo tên cột
                MessageBox.Show($"Tên cột: {columnName}, Dữ liệu mới: {duLieuMoi}");

                // Ví dụ: Cập nhật dữ liệu vào cơ sở dữ liệu
                string maDangKyPhong = dataGridView1.Rows[e.RowIndex].Cells["MaDangKyPhong"].Value.ToString();
                bool updateSuccess = xuly.SuaPhieuDangKyPhong(maDangKyPhong, columnName, duLieuMoi);

                MessageBox.Show(updateSuccess ? "Cập nhật thành công!" : "Cập nhật thất bại. Vui lòng thử lại.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        // Phương thức kiểm tra dữ liệu hợp lệ
        private bool ValidateCellData(string columnName, string duLieuMoi, DateTime? ngayDK, DateTime? ngayBD, DateTime? ngayKT)
        {
            try
            {
                switch (columnName)
                {
                    case "NgayDK": // Ngày đăng ký
                        if (DateTime.TryParse(duLieuMoi, out DateTime ngayDangKy))
                        {
                            if (ngayBD.HasValue && ngayDangKy >= ngayBD.Value)
                            {
                                MessageBox.Show("Ngày đăng ký phải trước ngày bắt đầu ở.");
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ngày đăng ký không hợp lệ.");
                            return false;
                        }
                        break;

                    case "NgayBD": // Ngày bắt đầu
                        if (DateTime.TryParse(duLieuMoi, out DateTime ngayBatDau))
                        {
                            if ((ngayDK.HasValue && ngayBatDau <= ngayDK.Value) ||
                                (ngayKT.HasValue && ngayBatDau >= ngayKT.Value))
                            {
                                MessageBox.Show("Ngày bắt đầu phải sau ngày đăng ký và trước ngày kết thúc.");
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ngày bắt đầu không hợp lệ.");
                            return false;
                        }
                        break;

                    case "NgayKT": // Ngày kết thúc
                        if (DateTime.TryParse(duLieuMoi, out DateTime ngayKetThuc))
                        {
                            if (ngayBD.HasValue && ngayKetThuc <= ngayBD.Value)
                            {
                                MessageBox.Show("Ngày kết thúc phải sau ngày bắt đầu.");
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ngày kết thúc không hợp lệ.");
                            return false;
                        }
                        break;

                    case "Giuong": // Giường
                        if (!int.TryParse(duLieuMoi, out _))
                        {
                            MessageBox.Show("Giường phải là số nguyên hợp lệ.");
                            return false;
                        }
                        break;

                    case "Tang": // Tầng
                        if (!int.TryParse(duLieuMoi, out _))
                        {
                            MessageBox.Show("Tầng phải là số nguyên hợp lệ.");
                            return false;
                        }
                        break;

                    default:
                        // Với các cột không cần kiểm tra, mặc định là hợp lệ
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kiểm tra dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        private void ThongTinLuuTru_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = xuly.GetDangKyPhong_();
            LoadGridview();
           
            dataGridView1.CellClick += DataGridView1_CellClick;
        }
        private void LoadGridview()
        {
            // Đặt tên hiển thị cho các cột và thiết lập ReadOnly cho các cột không được phép chỉnh sửa
            dataGridView1.Columns["MaDangKyPhong"].HeaderText = "Mã ĐK phòng";
            dataGridView1.Columns["MaDangKyPhong"].ReadOnly = true;
            dataGridView1.Columns["MaPhong"].HeaderText = "Mã phòng";
            dataGridView1.Columns["MaPhong"].ReadOnly = true;
            dataGridView1.Columns["MaSinhVien"].HeaderText = "Mã SV";
            dataGridView1.Columns["MaSinhVien"].ReadOnly = true;
            dataGridView1.Columns["NgayDK"].HeaderText = "Ngày ĐK";
            dataGridView1.Columns["NgayBD"].HeaderText = "Ngày BĐ ở";
            dataGridView1.Columns["NgayKT"].HeaderText = "Ngày KT ở";
         //   dataGridView1.Columns["Giuong"].HeaderText = "Giường";
            dataGridView1.Columns["Tang"].HeaderText = "Tầng";
            dataGridView1.Columns["HoTen"].HeaderText = "Tên Sinh Viên";
            dataGridView1.Columns["CCCD"].HeaderText = "CCCD";
            dataGridView1.Columns["Email"].HeaderText = "Email";
            dataGridView1.Columns["SDT"].HeaderText = "Số Điện Thoại";
            dataGridView1.Columns["LoaiPhong_"].HeaderText = "Loại Phòng";
            dataGridView1.Columns["DonGiaPhong"].HeaderText = "Đơn giá phòng";
            dataGridView1.Columns["HinhThucThanhToan"].HeaderText = "Hình thức thanh toán ";

            dataGridView1.Columns["NV1"].Visible = false;
            dataGridView1.Columns["NV2"].Visible = false;
            dataGridView1.Columns["NV3"].Visible = false;
            dataGridView1.Columns["TrangThai"].Visible = false;
            dataGridView1.Columns["Phong"].Visible = false;
            dataGridView1.Columns["SinhVien"].Visible = false;
            dataGridView1.Columns["HinhNhanDien"].Visible = false;
            dataGridView1.Columns["Tang"].Visible = false;
            dataGridView1.Columns["MaDangKyPhong"].Visible = false;
            // Thêm cột "Sửa" nếu chưa tồn tại
            if (!dataGridView1.Columns.Contains("btnEdit"))
            {
                DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn
                {
                    HeaderText = "Sửa",
                    Name = "btnEdit",
                    FlatStyle = FlatStyle.Flat,
                    UseColumnTextForButtonValue = true,
                    Text = "✏️"
                };
                dataGridView1.Columns.Add(editColumn);
            }

            // Thêm cột "Xóa" nếu chưa tồn tại
            if (!dataGridView1.Columns.Contains("btnDelete"))
            {
                DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn
                {
                    HeaderText = "Xóa",
                    Name = "btnDelete",
                    FlatStyle = FlatStyle.Flat,
                    UseColumnTextForButtonValue = true,
                    Text = "🗑️"
                };
                dataGridView1.Columns.Add(deleteColumn);
            }

            // Thay đổi vị trí hiển thị của các cột
            dataGridView1.Columns["MaSinhVien"].DisplayIndex = 0; // Mã sinh viên
            dataGridView1.Columns["HoTen"].DisplayIndex = 1; // Tên sinh viên
            dataGridView1.Columns["CCCD"].DisplayIndex = 2; // CCCD
            dataGridView1.Columns["MaPhong"].DisplayIndex = 3; // Mã phòng
            dataGridView1.Columns["LoaiPhong_"].DisplayIndex = 4; // Loại phòng
            dataGridView1.Columns["DonGiaPhong"].DisplayIndex = 5; // Đơn giá phòng
         //   dataGridView1.Columns["Giuong"].DisplayIndex = 6; // Giường
            dataGridView1.Columns["Tang"].DisplayIndex = 7; // Tầng
            dataGridView1.Columns["NgayDK"].DisplayIndex = 8; // Ngày đăng ký
            dataGridView1.Columns["NgayBD"].DisplayIndex = 9; // Ngày bắt đầu
            dataGridView1.Columns["NgayKT"].DisplayIndex = 10; // Ngày kết thúc
            dataGridView1.Columns["Email"].DisplayIndex = 11; // Email
            dataGridView1.Columns["SDT"].DisplayIndex = 12; // Số điện thoại
            dataGridView1.Columns["HinhThucThanhToan"].DisplayIndex = 13; // Hình thức thanh toán
            dataGridView1.Columns["btnEdit"].DisplayIndex = 14; // Cột sửa
            dataGridView1.Columns["btnDelete"].DisplayIndex = 15; // Cột xóa

        }
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                if (e.ColumnIndex == dataGridView1.Columns["btnEdit"].Index)
                {
                    string maDangKyPhong =dataGridView1.Rows[e.RowIndex].Cells["MaDangKyPhong"].Value.ToString();
                    MessageBox.Show("Sửa thông tin cho Mã ĐK phòng: " + maDangKyPhong);
                }
                else if (e.ColumnIndex == dataGridView1.Columns["btnDelete"].Index)
                {
                    DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa hàng này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        string maDKPhong =dataGridView1.Rows[e.RowIndex].Cells["MaDangKyPhong"].Value.ToString();
                        bool deleteSuccess = xuly.XoaPhieuDangKyPhong(maDKPhong);
                        if (deleteSuccess)
                        {
                            MessageBox.Show("Xóa thành công!");
                            dataGridView1.DataSource = xuly.GetDangKyPhong_();
                            LoadGridview();
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại. Vui lòng thử lại.");
                        }
                    }
                }
            }
        }


        private void ThongTinLuuTru_Load_1(object sender, EventArgs e)
        {

        }

        private void Load_ComboMaPhong()
        {
            cbo_maPhong.DataSource=bll_phong.GetPhong();
            cbo_maPhong.ValueMember = "MaPhong";
            cbo_maPhong.DisplayMember= "MaPhong";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
