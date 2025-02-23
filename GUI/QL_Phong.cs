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
using BLL;

namespace GUI
{
    public partial class QL_Phong : Form
    {
        BLL_QL_Phong bll_phong = new BLL_QL_Phong();
        public FormNV_TuyenSinh _mainForm; // Biến để lưu tham chiếu tới FormMain

        public QL_Phong() { }

        public QL_Phong(FormNV_TuyenSinh mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm; // Lưu tham chiếu
            this.Load += QL_Phong_Load;
            treeView_Phong.BringToFront();
            


        }

        private void QL_Phong_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            //    panel_fill.SendToBack();
            panelCapNhat.Width = 0;
            LoadPhongVaoTree();

            TaoDanhSachButtonPhong();
            treeView_Phong.AfterSelect += TreeView_Phong_AfterSelect;
            btn_CapNhat.Click += Btn_CapNhat_Click;
            btn_Huy.Click += Btn_Huy_Click;
            Timer.Tick += Timer_Tick;
            LoadComboLoaiPhong();
            LoadcboPhong();
            panel_button.AutoScroll = true;
            btn_CapNhatThongTin.Click += Btn_CapNhatThongTin_Click;
            cbo_maPhong.SelectedValueChanged += Cbo_maPhong_SelectedValueChanged;
        }

        private void Cbo_maPhong_SelectedValueChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu có mục được chọn
            if (cbo_maPhong.SelectedValue != null)
            {
                string maPhong = cbo_maPhong.SelectedValue.ToString(); // Lấy giá trị được chọn
                                                                       // Lấy thông tin phòng từ BLL
                Phong phong = bll_phong.GetPhongTheoMaPhong(maPhong);

                if (phong != null)
                {
                    // Cập nhật thông tin trên giao diện
                    cbo_loaiPhong.SelectedValue = phong.MaLoaiPhong;
                    txt_SVToiDa.Text = phong.SoLuongSinhVienToiDa.ToString();
                    if(phong.TrangThai=="Đang hoạt động")
                    {
                        rdo_hoatDong.Checked = true;
                    }
                    else { rdo_ngungHoatDong.Checked = true; }
                        
                   // rdo_hoatDong.Checked = phong.TrangThai;
                 //   rdo_ngungHoatDong.Checked = !phong.TrangThai;
                 
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Chưa có mục nào được chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Btn_CapNhatThongTin_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra tính hợp lệ của các trường thông tin
                if (cbo_maPhong.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn mã phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbo_loaiPhong.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn loại phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txt_SVToiDa.Text.Trim(), out int soLuongSVToiDa))
                {
                    MessageBox.Show("Số lượng sinh viên tối đa phải là số nguyên hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Kiểm tra số lượng sinh viên tối đa không vượt quá 10
                if (soLuongSVToiDa > 10)
                {
                    MessageBox.Show("Số lượng sinh viên tối đa không thể vượt quá 10.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Lấy giá trị từ các điều khiển
                string maPhong = cbo_maPhong.SelectedValue.ToString();
                int maLoaiPhong = int.Parse(cbo_loaiPhong.SelectedValue.ToString());
                bool trangThai = rdo_hoatDong.Checked; // "Có hoạt động" = true, "Không hoạt động" = false

                // Gọi phương thức BLL để cập nhật thông tin
               bool result = bll_phong.CapNhatPhong(maPhong, maLoaiPhong, trangThai, soLuongSVToiDa);

                // Thông báo kết quả cho người dùng
                if (result)
                {
                    MessageBox.Show("Cập nhật thông tin phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Reload dữ liệu hoặc cập nhật giao diện nếu cần
                    //  LoadDanhSachPhong();
                    //   TaoDanhSachButtonPhong
                    XoaTatCaButtonVaLabelPhong();
                    TaoDanhSachButtonPhong();
                    
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin phòng thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và thông báo cho người dùng
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void TreeView_Phong_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text == "KTX")
            {
                // Nếu là node gốc, gọi hàm TaoDanhSachButtonPhong không có tham số để hiển thị tất cả các phòng
                TaoDanhSachButtonPhong();
            }
            else
            {
                List<string> danhSachPhong = new List<string>();

                if (e.Node.Parent != null && e.Node.Parent.Text.StartsWith("Tầng"))
                {
                    // Hiển thị chỉ một phòng nếu là node phòng
                    danhSachPhong.Add(e.Node.Tag.ToString());
                }
                else if (e.Node.Text.StartsWith("Tầng"))
                {
                    // Hiển thị tất cả các phòng trong tầng nếu là node tầng
                    foreach (TreeNode phongNode in e.Node.Nodes)
                    {
                        danhSachPhong.Add(phongNode.Tag.ToString());
                    }
                }

                // Hiển thị danh sách phòng đã lọc
             
                CapNhatTaoDanhSachButtonPhong(danhSachPhong);
            }
        }

        //public void ReloadForm()
        //{
        //    // Xóa các điều khiển cũ nếu cần
        //      XoaTatCaButtonVaLabelPhong();

        //    // Tải lại TreeView và các button phòng
        //    LoadPhongVaoTree();
        //    TaoDanhSachButtonPhong();

        //    // Đặt lại kích thước panel (nếu cần)
        //    panelCapNhat.Width = 0;
        // //  this.Width = 1191 - 600;

        //}

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (panelCapNhat.Width == 600)
            {
                for (int i = 0; i < 10; i++)
                {
                    panelCapNhat.Width = Panel_capNhat.Width - 60;

                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    panelCapNhat.Width = Panel_capNhat.Width + 60;

                }

            }
            //    Panel_capNhat.Width = 0;
            Timer.Stop();

        }

        private void Btn_Huy_Click(object sender, EventArgs e)
        {

            //    
            Timer.Start();
            //    panelCapNhat.Width = 0;
            //    ReloadForm();
            // this.Width -= 600;

        }

        private void Btn_CapNhat_Click(object sender, EventArgs e)
        {
            Timer.Start();

        }

      

        // Cập nhật hàm TaoDanhSachButtonPhong để nhận danh sách phòng cần hiển thị
        private void CapNhatTaoDanhSachButtonPhong(List<string> danhSachMaPhong)
        {
            // Xóa các button và label hiện có trên form
            XoaTatCaButtonVaLabelPhong();

            int buttonWidth = 70;
            int buttonHeight = 70;
            int margin = 20;
            int startX = 10;
            int x = startX;
            int y = 20;

            int buttonsGioiHan = 10; // Giới hạn số button trên mỗi hàng
            int buttonCount = 0; // Đếm số button trên hàng hiện tại

            Image roomImage = GUI.Properties.Resources.iconKTX;

            foreach (string maPhong in danhSachMaPhong)
            {
                if (!bll_phong.KiemTraTrangThaiPhong(maPhong))
                {
                    continue; // Bỏ qua nếu phòng không hoạt động
                }

                // Tạo Label hiển thị mã phòng phía trên button
                Label lblMaPhong = new Label
                {
                    Text = maPhong,
                    Location = new Point(x, y - 20),
                    Size = new Size(buttonWidth, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                // Tạo button cho mỗi phòng
                Button btnPhong = new Button
                {
                    Image = roomImage,
                    ImageAlign = ContentAlignment.MiddleCenter,
                    TextAlign = ContentAlignment.TopCenter,
                    Size = new Size(buttonWidth, buttonHeight),
                    Location = new Point(x, y)
                };
                btnPhong.Click += (sender, e) => BtnPhong_Click(sender, e, maPhong);

                // Lấy số sinh viên hiện tại trong phòng
                int soSinhVienHienTai = bll_phong.LaySoSinhVien(maPhong);
                int soSinhVienToiDa = bll_phong.LaySoSVToiDa(maPhong);

                // Tạo label hiển thị số sinh viên dưới mỗi button
                Label lblSoSinhVien = new Label
                {
                    Text = $"{soSinhVienHienTai}/{soSinhVienToiDa} SV",
                    Location = new Point(x, y + buttonHeight + 5),
                    Size = new Size(buttonWidth, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                panel_button.Controls.Add(lblMaPhong);
                panel_button.Controls.Add(btnPhong);
                panel_button.Controls.Add(lblSoSinhVien);

                // Cập nhật vị trí X cho button tiếp theo và tăng đếm
                x += buttonWidth + margin;
                buttonCount++;

                // Kiểm tra nếu đã đạt 8 button trên hàng, xuống dòng mới
                if (buttonCount >= buttonsGioiHan)
                {
                    x = startX; // Căn đầu dòng theo giá trị startX
                    y += buttonHeight + margin + 45;
                    buttonCount = 0; // Reset số lượng button trên hàng
                }
            }
        }

        // Hàm xóa tất cả các button và label của phòng
        private void XoaTatCaButtonVaLabelPhong()
        {
            panel_button.Controls.OfType<Button>().ToList().ForEach(btn => panel_button.Controls.Remove(btn));
          
            panel_button.Controls.OfType<Label>().ToList().ForEach(lbl => panel_button.Controls.Remove(lbl));
        }


        public void LoadPhongVaoTree()
        {
            treeView_Phong.Nodes.Clear();// xóa các node hiện tai nếu có

            List<string> dsPhong = bll_phong.DS_Phong();

            //Tạo node gốc
            TreeNode rootNode = new TreeNode("KTX");

            foreach (string maPhong in dsPhong)
            {
                // Tách tầng và phòng từ MaPhong (định dạng: KTX_<Tầng>_<Phòng>)
                string[] parts = maPhong.Split('-');
                if (parts.Length == 3)
                {
                    string tang = parts[1]; // Tầng
                    string phong = parts[2]; // Phòng

                    // Tìm hoặc thêm node tầng
                    TreeNode tangNode = rootNode.Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == $"Tầng {tang}");

                    if (tangNode == null)
                    {
                        tangNode = new TreeNode($"Tầng {tang}");
                        rootNode.Nodes.Add(tangNode);
                    }

                    // Thêm node phòng vào node tầng
                    TreeNode phongNode = new TreeNode($"Phòng {phong}");
                    phongNode.Tag = maPhong; // Gán mã phòng vào Tag nếu cần sử dụng sau này
                    tangNode.Nodes.Add(phongNode);
                }
            }

            // Thêm rootNode vào TreeView
            treeView_Phong.Nodes.Add(rootNode);
            treeView_Phong.ExpandAll(); // Mở rộng tất cả các node    
        }

        private void TaoDanhSachButtonPhong()
        {
            List<string> danhSachMaPhong = bll_phong.DS_Phong();

            int buttonWidth = 70;
            int buttonHeight = 70;
            int margin = 20;
            int startX = 10;
            int x = startX;
            int y = 20;

            int buttonsGioiHan = 8; // Giới hạn số button trên mỗi hàng
            int buttonCount = 0; // Đếm số button trên hàng hiện tại

            Image roomImage = GUI.Properties.Resources.iconKTX;

            foreach (string maPhong in danhSachMaPhong)
            {
                // Kiểm tra trạng thái phòng (chỉ tạo button nếu phòng đang hoạt động)
                if (!bll_phong.KiemTraTrangThaiPhong(maPhong))
                {
                    continue; // Bỏ qua nếu phòng không hoạt động
                }

                // Tạo Label hiển thị mã phòng phía trên button
                Label lblMaPhong = new Label
                {
                    Text = maPhong,
                    Location = new Point(x, y - 20),
                    Size = new Size(buttonWidth, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                // Tạo button cho mỗi phòng
                Button btnPhong = new Button
                {
                    Image = roomImage,
                    ImageAlign = ContentAlignment.MiddleCenter,
                    TextAlign = ContentAlignment.TopCenter,
                    Size = new Size(buttonWidth, buttonHeight),
                    Location = new Point(x, y)
                };
                btnPhong.Click += (sender, e) => BtnPhong_Click(sender, e, maPhong);

                // Lấy số sinh viên hiện tại trong phòng
                int soSinhVienHienTai = bll_phong.LaySoSinhVien(maPhong);
                int soSinhVienToiDa = bll_phong.LaySoSVToiDa(maPhong);

                // Tạo label hiển thị số sinh viên dưới mỗi button
                Label lblSoSinhVien = new Label
                {
                    Text = $"{soSinhVienHienTai}/{soSinhVienToiDa} SV",
                    Location = new Point(x, y + buttonHeight + 5),
                    Size = new Size(buttonWidth, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                panel_button.Controls.Add(lblMaPhong);
                panel_button.Controls.Add(btnPhong);
                panel_button.Controls.Add(lblSoSinhVien);

                // Cập nhật vị trí X cho button tiếp theo và tăng đếm
                x += buttonWidth + margin;
                buttonCount++;

                // Kiểm tra nếu đã đạt 8 button trên hàng, xuống dòng mới
                if (buttonCount >= buttonsGioiHan)
                {
                    x = startX; // Căn đầu dòng theo giá trị startX
                    y += buttonHeight + margin + 45;
                    buttonCount = 0; // Reset số lượng button trên hàng
                }
            }
        }


        private void BtnPhong_Click(object sender, EventArgs e, string maPhong)
        {
            if (_mainForm == null)
            {
                MessageBox.Show("Form chính chưa được khởi tạo. Không thể mở form con.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;  // Dừng lại nếu _mainForm là null
            }

          
            Frm_ChiTietPhong frmChiTietPhong = new Frm_ChiTietPhong(_mainForm, maPhong);
            _mainForm.OpenChildForm(frmChiTietPhong); 
        }

        private void LoadComboLoaiPhong()
        {
            cbo_loaiPhong.DataSource = bll_phong.LayLoaiPhong();
            cbo_loaiPhong.DisplayMember = "TenLoaiPhong";
            cbo_loaiPhong.ValueMember = "MaLoaiPhong";
        }

        private void LoadcboPhong()
        {
            cbo_maPhong.DataSource = bll_phong.GetPhong();
            cbo_maPhong.DisplayMember = "MaPhong";
            cbo_maPhong.ValueMember = "MaPhong";
        }

       
    }
}