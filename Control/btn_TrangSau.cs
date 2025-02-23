using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Control
{
    public class btn_TrangSau : Button
    {
        public btn_TrangSau()
        {
            this.Text = ">>";  // Văn bản cho nút "Trang Sau"
            this.Font = new Font(this.Font, FontStyle.Bold); // Chữ in đậm
            this.BackColor = Color.LightGray;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Cursor = Cursors.Hand;

            // Gán sự kiện khi di chuột vào và rời khỏi nút
            this.MouseEnter += (s, e) => this.BackColor = Color.Gray;
            this.MouseLeave += (s, e) => this.BackColor = Color.LightGray;
        }

        public void SetClickAction(Action onClickAction)
        {
            this.Click += (sender, e) => onClickAction();
        }
    }
}
