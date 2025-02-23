using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Control
{
    namespace Control
    {
        public class btn_CustomRedButton : Button
        {
            private Color defaultColor = Color.DarkRed;  // Màu mặc định
            private Color hoverColor = Color.Red;        // Màu khi di chuột vào

            public btn_CustomRedButton()
            {
                // Cài đặt màu nền, thuộc tính button và font chữ in đậm
                this.BackColor = defaultColor;
                this.FlatStyle = FlatStyle.Flat;
                this.FlatAppearance.BorderSize = 0;
                this.ForeColor = Color.White;
                this.Font = new Font(this.Font, FontStyle.Bold); // Chữ in đậm
                this.Cursor = Cursors.Hand;

                // Gán sự kiện khi di chuột vào và ra khỏi nút
                this.MouseEnter += Btn_CustomRedButton_MouseEnter;
                this.MouseLeave += Btn_CustomRedButton_MouseLeave;
            }

            // Thay đổi màu nền khi di chuột vào nút
            private void Btn_CustomRedButton_MouseEnter(object sender, EventArgs e)
            {
                this.BackColor = hoverColor;
            }

            // Khôi phục màu nền khi rời chuột khỏi nút
            private void Btn_CustomRedButton_MouseLeave(object sender, EventArgs e)
            {
                this.BackColor = defaultColor;
            }
        }
    }
}