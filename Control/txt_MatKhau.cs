using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Control
{
    public class txt_MatKhau : UserControl
    {
        private TextBox textBox;
        private Button btnToggleVisibility;
        private bool isPasswordVisible;

        public txt_MatKhau()
        {
            // Cấu hình TextBox cho mật khẩu
            textBox = new TextBox();
            textBox.UseSystemPasswordChar = true; // Ban đầu ẩn mật khẩu
            textBox.Dock = DockStyle.Fill;
            isPasswordVisible = false;

            // Tạo nút để bật/tắt hiển thị mật khẩu
            btnToggleVisibility = new Button();
            btnToggleVisibility.Width = 30;
            btnToggleVisibility.Dock = DockStyle.Right;
            btnToggleVisibility.Cursor = Cursors.Hand;
            btnToggleVisibility.FlatStyle = FlatStyle.Flat;
            btnToggleVisibility.FlatAppearance.BorderSize = 0;
            btnToggleVisibility.Text = "👁"; // Icon mắt

            // Gắn sự kiện nhấn nút để hiển thị/ẩn mật khẩu
            btnToggleVisibility.Click += TogglePasswordVisibility;

            // Thêm TextBox và Button vào UserControl
            this.Controls.Add(textBox);
            this.Controls.Add(btnToggleVisibility);

            // Điều chỉnh kích thước của UserControl
            this.Height = textBox.Height;
            this.Width = 200;
        }

        private void TogglePasswordVisibility(object sender, EventArgs e)
        {
            if (isPasswordVisible)
            {
                textBox.UseSystemPasswordChar = true;
                btnToggleVisibility.Text = "👁";
            }
            else
            {
                textBox.UseSystemPasswordChar = false;
                btnToggleVisibility.Text = "🙈";
            }

            isPasswordVisible = !isPasswordVisible;
        }

        // Property để lấy hoặc đặt giá trị Text của TextBox
        public override string Text
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }
    }
}
