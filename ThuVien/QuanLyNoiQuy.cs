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

namespace ThuVien
{
    public partial class QuanLyNoiQuy : UserControl
    {
        private BLL_LoaiNoiQuy bllLoaiNoiQuy;

        public QuanLyNoiQuy()
        {
            InitializeComponent();
            bllLoaiNoiQuy = new BLL_LoaiNoiQuy(); // Khởi tạo lớp BLL
            this.Load += QuanLyNoiQuy_Load;
        }

        private void QuanLyNoiQuy_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
        }
        // Hàm load dữ liệu lên DataGridView
        private void LoadDataGridView()
        {
            List<LoaiNoiQuy> danhSachLoaiNoiQuy = bllLoaiNoiQuy.GetAllLoaiNoiQuy(); // Gọi BLL để lấy dữ liệu
            dataGV_DS_LoaiNoiQuy.DataSource = danhSachLoaiNoiQuy; // Gán dữ liệu vào DataGridView
            dataGV_DS_LoaiNoiQuy.Columns["MaLoaiNQ"].HeaderText = "Mã Loại Nội Quy"; // Đặt tên cột
            dataGV_DS_LoaiNoiQuy.Columns["TenLoaiNQ"].HeaderText = "Tên Loại Nội Quy"; // Đặt tên cột
        }
    }
}
